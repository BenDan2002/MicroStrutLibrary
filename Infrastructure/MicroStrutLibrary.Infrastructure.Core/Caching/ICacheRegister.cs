using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Caching
{
    /// <summary>
    /// 缓存配置器接口
    /// </summary>
    public interface ICacheRegister
    {
        /// <summary>
        /// 配置缓存
        /// </summary>
        /// <param name="services"></param>
        void Configure(IServiceCollection services);
    }
}
