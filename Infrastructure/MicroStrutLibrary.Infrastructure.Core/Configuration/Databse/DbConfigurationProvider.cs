using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using MicroStrutLibrary.Infrastructure.Core.Data;
using MicroStrutLibrary.Infrastructure.Core.Exception;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration.Database
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private DbSourceConfiguration config;

        public DbConfigurationProvider(DbSourceConfiguration config)
        {
            this.config = config;
        }

        public override void Load()
        {
            MicroStrutLibraryExceptionHelper.IsNull(this.config.TableName, this.GetType().FullName, LogLevel.Error, "TableName配置为空。");
            MicroStrutLibraryExceptionHelper.IsNull(this.config.ConnectionString, this.GetType().FullName, LogLevel.Error, "ConnectionString。");
            MicroStrutLibraryExceptionHelper.IsNull(this.config.ConnectionProvider, this.GetType().FullName, LogLevel.Error, "ConnectionProvider。");

            var connectionProviderType = Type.GetType(this.config.ConnectionProvider.Type);
            var connectionProviderInstance = (IDbFactoryProvider)Activator.CreateInstance(connectionProviderType);
            var connectionProviderFactory = connectionProviderInstance.GetFactory(this.config.ConnectionProvider.Parameter);

            List<DbConfigurationInfo> list = new List<DbConfigurationInfo>();

            using (DbConnection connection = connectionProviderFactory.CreateConnection())
            {
                var command = connection.CreateCommand();

                command.CommandText = $"SELECT APP_CODE AS {nameof(DbConfigurationInfo.AppCode)},CONFIG_CODE AS {nameof(DbConfigurationInfo.ConfigCode)},CONFIG_CONTENT AS {nameof(DbConfigurationInfo.ConfigContent)} FROM {this.config.TableName} WHERE  APP_CODE = @emtpyCode OR APP_CODE = @appCode ORDER BY APP_CODE DESC";

                var emptyParameter = command.CreateParameter();

                emptyParameter.ParameterName = "emtpyCode";
                emptyParameter.Value = "";

                var appParameter = command.CreateParameter();

                appParameter.ParameterName = "appCode";
                appParameter.Value = this.config.AppCode ?? "";

                command.Parameters.Add(emptyParameter);
                command.Parameters.Add(appParameter);

                //Open
                connection.ConnectionString = this.config.ConnectionString.ConnectionString;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    var appCodeIndex = reader.GetOrdinal(nameof(DbConfigurationInfo.AppCode));
                    var configCodeIndex = reader.GetOrdinal(nameof(DbConfigurationInfo.ConfigCode));
                    var configContentIndex = reader.GetOrdinal(nameof(DbConfigurationInfo.ConfigContent));

                    while (reader.Read())
                    {
                        //此处约定，特定优于一般，因此后面来的一般不予考虑
                        if (!list.Exists(conf => conf.ConfigCode == reader.GetString(configCodeIndex)))
                        {
                            list.Add(new DbConfigurationInfo
                            {
                                AppCode = reader.GetString(appCodeIndex),
                                ConfigCode = reader.GetString(configCodeIndex),
                                ConfigContent = reader.GetString(configContentIndex)
                            });
                        }
                    }
                }
            }

            //转换
            this.Data = new JsonConfigurationParser().Parse(list.ToDictionary(o => o.ConfigCode, o => o.ConfigContent));
        }
    }
}
