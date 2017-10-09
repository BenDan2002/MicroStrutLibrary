using MicroStrutLibrary.Infrastructure.Core.Data;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration.Database
{
    /// <summary>
    /// 数据配置选项
    /// </summary>
    [ConfigurationEntry(nameof(DbSourceConfiguration), Default = false)]
    public class DbSourceConfiguration
    {
        /// <summary>
        /// 应用代码
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public DbConnectionStringSettings ConnectionString { get; set; }

        /// <summary>
        /// 链接字符串提供者
        /// </summary>
        public DbConnectionProviderSettings ConnectionProvider { get; set; }
    }
}
