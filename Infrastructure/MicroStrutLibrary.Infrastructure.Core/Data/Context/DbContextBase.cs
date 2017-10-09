using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options) : base(options)
        {
        }
    }

    /// <summary>
    /// 数据库上下文扩展类
    /// </summary>
    public static class DatabaseUtility
    {
        private static ConcurrentBag<Tuple<string, string, IDbFactoryProvider>> providers;

        static DatabaseUtility()
        {
            providers = new ConcurrentBag<Tuple<string, string, IDbFactoryProvider>>();
        }

        public static void InitDbProviders(DbConnectionConfiguration configuration)
        {
            foreach (DbConnectionStringSettings connectionStringSettings in configuration.ConnectionStrings)
            {
                DbConnectionProviderSettings providerSettings = configuration.Providers.SingleOrDefault(s => s.Name == connectionStringSettings.ProviderName);

                Type providerType = Type.GetType(providerSettings.OriginType);
                IDbFactoryProvider provider = (IDbFactoryProvider)Activator.CreateInstance(providerType);

                providers.Add(new Tuple<string, string, IDbFactoryProvider>(connectionStringSettings.Name, connectionStringSettings.ConnectionString, provider));
            }
        }

        /// <summary>
        /// 执行返回数据表
        /// </summary>
        /// <param name="context">数据库上下文</param>
        /// <param name="sqlString">执行语句</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页的大小</param>
        /// <returns>数据表</returns>
        public static DataTable ExecuteDataTable(string dbName, string sqlString, CommandType commandType = CommandType.Text, int pageIndex = 1, int pageSize = 10)
        {
            DataTable table = new DataTable();

            var tuple = providers.Single(p => p.Item1 == dbName);
            DbProviderFactory providerFactory = tuple.Item3.GetFactory(null);

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = tuple.Item2;
            DbCommand command = providerFactory.CreateCommand();
            command.Connection = connection;

            command.CommandType = commandType;
            command.CommandText = sqlString;
            command.CommandTimeout = 0;

            //打开连接
            connection.Open();

            using (IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                int fieldCount = dr.FieldCount;
                for (int i = 0; i < fieldCount; i++)
                {
                    table.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
                }

                object[] values = new object[fieldCount];
                int currentIndex = 0;
                int startIndex = pageSize * (pageIndex - 1);

                try
                {
                    table.BeginLoadData();
                    while (dr.Read())
                    {
                        if (startIndex > currentIndex++)
                            continue;

                        if (pageSize > 0 && (currentIndex - startIndex) > pageSize)
                            break;

                        dr.GetValues(values);
                        table.LoadDataRow(values, true);
                    }
                }
                finally
                {
                    table.EndLoadData();
                    dr.Close();
                }
            }

            return table;
        }
    }
}
