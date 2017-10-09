using System.Linq;
using MicroStrutLibrary.Infrastructure.Core.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Dependency
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 使用所有程序集中的服务注册器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegisters(this IServiceCollection services)
        {
            //获取服务提供者
            var serviceProvider = services.BuildServiceProvider();

            //获取程序集查找器
            var assemblyFinder = serviceProvider.GetRequiredService<IAssemblyFinder>();

            //注册器类型
            var registerType = typeof(IServiceRegister);

            //获取注册器实例
            var registerInstances = ReflectionHelper.GetDerivedTypes(assemblyFinder, registerType).Select(type =>
            {
                return (IServiceRegister)ActivatorUtilities.CreateInstance(serviceProvider, type);  //实现的注册器可以支持构造函数依赖注入
            });

            //注册
            foreach (var register in registerInstances.ToList())
            {
                register.Register(services);
            }

            return services;
        }
    }
}
