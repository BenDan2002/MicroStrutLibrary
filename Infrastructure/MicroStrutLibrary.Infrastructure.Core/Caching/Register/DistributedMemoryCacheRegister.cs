using MicroStrutLibrary.Infrastructure.Core.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Caching
{
    [TypeName("DistributedMemoryCache", Description = "内存方式分布式缓存")]
    public class DistributedMemoryCacheRegister : ICacheRegister
    {
        public void Configure(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
        }
    }
}
