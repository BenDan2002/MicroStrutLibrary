using MicroStrutLibrary.Infrastructure.Core.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public class SqlServerDbContextOptionsBuilderProvider : IDbContextOptionsBuilderProvider
    {
        public DbContextOptionsBuilder UseDb(DbContextOptionsBuilder builder, DbConnectionStringSettings connectionStringSettings, DbConnectionProviderSettings connectionProviderSettings)
        {
            MicroStrutLibraryExceptionHelper.IsNull(connectionStringSettings, typeof(SqlServerDbContextOptionsBuilderProvider).FullName, LogLevel.Error, "链接字符串配置为空！");
            MicroStrutLibraryExceptionHelper.IsNull(connectionProviderSettings, typeof(SqlServerDbContextOptionsBuilderProvider).FullName, LogLevel.Error, "链接提供者配置为空！");

            //SQLServer 2008 R2以以下版本
            builder.UseSqlServer(connectionStringSettings.ConnectionString, optionBuilder => optionBuilder.UseRowNumberForPaging());

            return builder.UseSqlServer(connectionStringSettings.ConnectionString);
        }
    }
}
