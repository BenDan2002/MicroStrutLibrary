using System.Linq;
using AutoMapper;
using MicroStrutLibrary.Infrastructure.Core.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.AutoMapper
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 添加默认程序集查找器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            //获取服务提供者
            var serviceProvider = services.BuildServiceProvider();

            //获取程序集查找器
            var assemblyFinder = serviceProvider.GetRequiredService<IAssemblyFinder>();

            //映射配置类型
            var profileType = typeof(Profile);

            //获取配置器实例
            var profileInstances = ReflectionHelper.GetDerivedTypes(assemblyFinder, profileType).Select(type =>
            {
                return (Profile)ActivatorUtilities.CreateInstance(serviceProvider, type);  //实现的注册器可以支持构造函数依赖注入
            });

            //初始化
            Mapper.Initialize(config =>
            {
                foreach (var profile in profileInstances.ToList())
                {
                    config.AddProfile(profile);
                }
            });

            return services;
        }
    }
}
