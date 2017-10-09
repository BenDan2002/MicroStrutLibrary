using System;
using System.Reflection;
using MicroStrutLibrary.Infrastructure.Core.Exception;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration
{
    public static class IConfigurationRootExtensions
    {
        public static T GetByEntry<T>(this IConfigurationRoot root) where T : class, new()
        {
            return (T)GetByEntry(root, typeof(T));
        }

        public static object GetByEntry(this IConfigurationRoot root, Type type)
        {
            MicroStrutLibraryExceptionHelper.IsNull(type, typeof(IConfigurationRootExtensions).FullName, LogLevel.Error, $"类型为空！");

            ConfigurationEntryAttribute attribute = type.GetTypeInfo().GetCustomAttribute<ConfigurationEntryAttribute>(false);
            MicroStrutLibraryExceptionHelper.IsNull(attribute, typeof(IConfigurationRootExtensions).FullName, LogLevel.Error, $"属性类型为空！");

            var data = root.GetSection(attribute.Path).Get(type);
            if (data == null && attribute.Default)
            {
                data = Activator.CreateInstance(type);
            }

            return data;
        }

        public static IConfigurationSection GetSectionByEntry<T>(this IConfigurationRoot root) where T : class
        {
            return GetSectionByEntry(root, typeof(T));
        }

        private static IConfigurationSection GetSectionByEntry(IConfigurationRoot root, Type type)
        {
            MicroStrutLibraryExceptionHelper.IsNull(type, typeof(IConfigurationRootExtensions).FullName, LogLevel.Error, $"类型为空！");

            ConfigurationEntryAttribute attribute = type.GetTypeInfo().GetCustomAttribute<ConfigurationEntryAttribute>(false);
            MicroStrutLibraryExceptionHelper.IsNull(attribute, typeof(IConfigurationRootExtensions).FullName, LogLevel.Error, $"属性类型为空！");

            return root.GetSection(attribute.Path);
        }
    }
}
