using System.Collections.Generic;
using MicroStrutLibrary.Infrastructure.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    [ConfigurationEntry(nameof(DbConnectionConfiguration))]
    public class DbConnectionConfiguration
    {
        public List<DbConnectionStringSettings> ConnectionStrings { get; set; } = new List<DbConnectionStringSettings>();
        public List<DbConnectionProviderSettings> Providers { get; set; } = new List<DbConnectionProviderSettings>();
    }

    public class DbConnectionConfigurationRegister : IConfigurationRegister
    {
        public void Register(IServiceCollection services, IConfigurationRoot root)
        {
            services.Configure<DbConnectionConfiguration>(root.GetSectionByEntry<DbConnectionConfiguration>());
        }
    }

    /// <summary>
    /// 链接提供者配置
    /// </summary>
    public class DbConnectionProviderSettings
    {
        /// <summary>
        /// 提供者名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 提供者类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 原生提供者类型
        /// </summary>
        public string OriginType { get; set; }

        /// <summary>
        /// 提供者参数
        /// </summary>
        public string Parameter { get; set; }
    }

    /// <summary>
    /// 连接字符串配置
    /// </summary>
    public class DbConnectionStringSettings
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 提供者名称
        /// </summary>
        public string ProviderName { get; set; }
    }
}
