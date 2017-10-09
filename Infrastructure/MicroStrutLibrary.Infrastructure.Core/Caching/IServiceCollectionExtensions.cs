using System;
using System.Collections.Generic;
using MicroStrutLibrary.Infrastructure.Core.Exception;
using MicroStrutLibrary.Infrastructure.Core.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MicroStrutLibrary.Infrastructure.Core.Caching
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCaching(this IServiceCollection services, IConfigurationRoot root)
        {
            MicroStrutLibraryExceptionHelper.IsNull(root, typeof(IServiceCollectionExtensions).FullName, LogLevel.Error, "根配置为空！");

            IServiceProvider provider = services.BuildServiceProvider();
            IOptions<CacheConfiguration> cacheConfigurationOptions = provider.GetRequiredService<IOptions<CacheConfiguration>>();

            Dictionary<string, Type> registerTypes = GetRegisterTypes(provider);

            ICacheRegister memoryCacheInstance = GetMemoryCacheInstance(provider, registerTypes, cacheConfigurationOptions);
            memoryCacheInstance.Configure(services);

            ICacheRegister distributedCacheInstance = GetDistributedCacheInstance(provider, registerTypes, cacheConfigurationOptions);
            distributedCacheInstance.Configure(services);

            return services;
        }

        private static Dictionary<string, Type> GetRegisterTypes(IServiceProvider provider)
        {
            IAssemblyFinder assemblyFinder = provider.GetRequiredService<IAssemblyFinder>();

            IEnumerable<Type> registerTypes = ReflectionHelper.GetDerivedTypes(assemblyFinder, typeof(ICacheRegister));

            Dictionary<string, Type> result = new Dictionary<string, Type>();

            foreach (Type registerType in registerTypes)
            {
                TypeNameAttribute attr = ReflectionHelper.GetTypeName(registerType);
                if (attr != null)
                {
                    result.Add(attr.Name, registerType);
                }
            }

            return result;
        }

        private static ICacheRegister GetMemoryCacheInstance(IServiceProvider provider, Dictionary<string, Type> registerTypes, IOptions<CacheConfiguration> cacheConfigurationOptions)
        {
            string memoryCacheName = cacheConfigurationOptions.Value.MemoryCache;
            if (string.IsNullOrEmpty(memoryCacheName))
            {
                memoryCacheName = ReflectionHelper.GetTypeName(typeof(MemoryCacheRegister)).Name;
            }

            Type registerType = null;
            if (registerTypes.ContainsKey(memoryCacheName))
            {
                registerType = registerTypes[memoryCacheName];
            }
            else
            {
                registerType = Type.GetType(memoryCacheName);
            }

            return (ICacheRegister)ActivatorUtilities.CreateInstance(provider, registerType);
        }

        private static ICacheRegister GetDistributedCacheInstance(IServiceProvider provider, Dictionary<string, Type> registerTypes, IOptions<CacheConfiguration> cacheConfigurationOptions)
        {
            string distributedCacheName = cacheConfigurationOptions.Value.MemoryCache;
            if (string.IsNullOrEmpty(distributedCacheName))
            {
                distributedCacheName = ReflectionHelper.GetTypeName(typeof(DistributedMemoryCacheRegister)).Name;
            }

            Type registerType = null;
            if (registerTypes.ContainsKey(distributedCacheName))
            {
                registerType = registerTypes[distributedCacheName];
            }
            else
            {
                registerType = Type.GetType(distributedCacheName);
            }

            return (ICacheRegister)ActivatorUtilities.CreateInstance(provider, registerType);
        }
    }
}
