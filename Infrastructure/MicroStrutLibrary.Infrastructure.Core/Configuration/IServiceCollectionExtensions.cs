using System;
using System.Linq;
using MicroStrutLibrary.Infrastructure.Core.Exception;
using MicroStrutLibrary.Infrastructure.Core.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfigurationRoot root)
        {
            MicroStrutLibraryExceptionHelper.IsNull(root, typeof(IServiceCollectionExtensions).FullName, LogLevel.Error, "根配置为空！");

            services.AddSingleton<IConfigurationRoot>(root);

            IServiceProvider provider = services.BuildServiceProvider();

            IAssemblyFinder assemblyFinder = provider.GetRequiredService<IAssemblyFinder>();

            var registerType = typeof(IConfigurationRegister);
            var registerInstances = ReflectionHelper.GetDerivedTypes(assemblyFinder, registerType).Select(type =>
            {
                return (IConfigurationRegister)Activator.CreateInstance(type);
            });

            foreach (var register in registerInstances.ToList())
            {
                register.Register(services, root);
            }

            return services;
        }
    }
}
