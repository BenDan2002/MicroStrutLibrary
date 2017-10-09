using MicroStrutLibrary.Infrastructure.Core.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Caching
{
    [TypeName("MemoryCache", Description = "内存方式本地缓存")]
    public class MemoryCacheRegister : ICacheRegister
    {
        public void Configure(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}
