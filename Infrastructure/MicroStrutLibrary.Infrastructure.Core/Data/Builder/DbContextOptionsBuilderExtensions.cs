using System;
using System.Linq;
using System.Reflection;
using MicroStrutLibrary.Infrastructure.Core.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseDb(this DbContextOptionsBuilder builder, DbConnectionConfiguration configuration, string dbName)
        {
            MicroStrutLibraryExceptionHelper.IsNull(configuration, typeof(DbContextOptionsBuilderExtensions).FullName, LogLevel.Error, "配置为空！");
            MicroStrutLibraryExceptionHelper.IsNullOrEmpty(dbName, typeof(DbContextOptionsBuilderExtensions).FullName, LogLevel.Error, "链接数据库为空！");

            DbConnectionStringSettings connectionStringSettings = configuration.ConnectionStrings.SingleOrDefault(s => s.Name == dbName);
            MicroStrutLibraryExceptionHelper.IsNull(connectionStringSettings, typeof(DbContextOptionsBuilderExtensions).FullName, LogLevel.Error, $"链接数据库{dbName}对应的配置节为空！");
            DbConnectionProviderSettings providerSettings = configuration.Providers.SingleOrDefault(s => s.Name == connectionStringSettings.ProviderName);
            MicroStrutLibraryExceptionHelper.IsNull(providerSettings, typeof(DbContextOptionsBuilderExtensions).FullName, LogLevel.Error, $"链接数据库{dbName}对应的提供者配置为空！");

            Type providerType = Type.GetType(providerSettings.Type);
            IDbContextOptionsBuilderProvider provider;
            if (string.IsNullOrEmpty(providerSettings.Parameter))
            {
                provider = (IDbContextOptionsBuilderProvider)Activator.CreateInstance(providerType);
            }
            else
            {
                provider = (IDbContextOptionsBuilderProvider)Activator.CreateInstance(providerType, providerSettings.Parameter);
            }
            return provider.UseDb(builder, connectionStringSettings, providerSettings);
        }

        public static DbContextOptionsBuilder UseDb<TContext>(this DbContextOptionsBuilder builder, DbConnectionConfiguration configuration) where TContext: DbContextBase
        {
            var attribute = typeof(TContext).GetTypeInfo().GetCustomAttribute<EntityDbNameAttribute>(false);

            MicroStrutLibraryExceptionHelper.IsNull(attribute, typeof(DbContextOptionsBuilderExtensions).FullName, LogLevel.Error, "名称属性为空！");

            return UseDb(builder, configuration, attribute.Name);
        }
    }
}
