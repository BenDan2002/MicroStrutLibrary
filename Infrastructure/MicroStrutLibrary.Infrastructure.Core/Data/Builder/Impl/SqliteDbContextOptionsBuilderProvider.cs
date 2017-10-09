using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MicroStrutLibrary.Infrastructure.Core.Exception;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public class SqliteDbContextOptionsBuilderProvider : IDbContextOptionsBuilderProvider
    {
        public DbContextOptionsBuilder UseDb(DbContextOptionsBuilder builder, DbConnectionStringSettings connectionStringSettings, DbConnectionProviderSettings connectionProviderSettings)
        {
            MicroStrutLibraryExceptionHelper.IsNull(connectionStringSettings, typeof(SqlServerDbContextOptionsBuilderProvider).FullName, LogLevel.Error, "链接字符串配置为空！");
            MicroStrutLibraryExceptionHelper.IsNull(connectionProviderSettings, typeof(SqlServerDbContextOptionsBuilderProvider).FullName, LogLevel.Error, "链接提供者配置为空！");

            return builder.UseSqlite(connectionStringSettings.ConnectionString);
        }
    }
}
