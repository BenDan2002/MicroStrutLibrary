using MicroStrutLibrary.Infrastructure.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Caching
{
    [ConfigurationEntry(nameof(CacheConfiguration))]
    public class CacheConfiguration
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        public string MemoryCache { get; set; }

        /// <summary>
        /// 分布式缓存
        /// </summary>
        public string DistributedCache { get; set; }
    }

    public class CacheConfigurationRegister : IConfigurationRegister
    {
        public void Register(IServiceCollection services, IConfigurationRoot root)
        {
            services.Configure<CacheConfiguration>(root.GetSectionByEntry<CacheConfiguration>());
        }
    }
}
