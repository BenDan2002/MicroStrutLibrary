using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroStrutLibrary.Infrastructure.Core.Exception;
using Microsoft.Extensions.Logging;

namespace MicroStrutLibrary.Infrastructure.Core.Reflection
{
    public static class ReflectionHelper
    {
        private static IEnumerable<Assembly> GetReferencedAssemblies(IAssemblyFinder finder, Type type)
        {
            MicroStrutLibraryExceptionHelper.IsNull(finder, typeof(ReflectionHelper).FullName, LogLevel.Error, "类型查找器为空！");
            MicroStrutLibraryExceptionHelper.IsNull(type, typeof(ReflectionHelper).FullName, LogLevel.Error, "类型为空！");

            Assembly typeAssembly = type.GetTypeInfo().Assembly;

            return finder.GetAssemblies().Where(assembly =>
            {
                if (assembly.IsDynamic)
                {
                    return false;
                }

                if (assembly.FullName == typeAssembly.FullName)
                {
                    return true;
                }

                return assembly.GetReferencedAssemblies().Any(assemblyName =>
                {
                    return assemblyName.FullName == typeAssembly.FullName;
                });
            });
        }

        public static IEnumerable<Type> GetDerivedTypes(IAssemblyFinder finder, Type type)
        {
            MicroStrutLibraryExceptionHelper.IsNull(finder, typeof(ReflectionHelper).FullName, LogLevel.Error, "类型查找器为空！");
            MicroStrutLibraryExceptionHelper.IsNull(type, typeof(ReflectionHelper).FullName, LogLevel.Error, "类型为空！");

            //获取可用程序集
            IEnumerable<Assembly> assemblies = GetReferencedAssemblies(finder, type);

            //获取可用类型
            return GetDerivedTypes(assemblies, type);
        }

        public static IEnumerable<Type> GetDerivedTypes(IEnumerable<Assembly> assemblies, Type type)
        {
            MicroStrutLibraryExceptionHelper.IsNull(assemblies, typeof(ReflectionHelper).FullName, LogLevel.Error, "可用程序集为空！");
            MicroStrutLibraryExceptionHelper.IsNull(type, typeof(ReflectionHelper).FullName, LogLevel.Error, "类型为空！");

            //获取可用类型
            return assemblies.SelectMany(assembly =>
            {
                return assembly.GetTypes().Where(t =>
                {
                    if (type == t)
                    {
                        return false;
                    }

                    TypeInfo tInfo = t.GetTypeInfo();

                    if (tInfo.IsAbstract || !tInfo.IsClass || !tInfo.IsPublic)
                    {
                        return false;
                    }

                    return type.IsAssignableFrom(t);
                });
            });
        }

        public static List<Tuple<string, string, Type>> GetTypeNameList(IAssemblyFinder finder, Type type)
        {
            MicroStrutLibraryExceptionHelper.IsNull(finder, typeof(ReflectionHelper).FullName, LogLevel.Error, "类型查找器为空！");
            MicroStrutLibraryExceptionHelper.IsNull(type, typeof(ReflectionHelper).FullName, LogLevel.Error, "类型为空！");

            //获取可用程序集
            IEnumerable<Assembly> assemblies = GetReferencedAssemblies(finder, type);

            //获取可用类型
            return GetTypeNameList(assemblies, type);
        }

        public static List<Tuple<string, string, Type>> GetTypeNameList(IEnumerable<Assembly> assemblies, Type type)
        {
            List<Tuple<string, string, Type>> result = new List<Tuple<string, string, Type>>();

            IEnumerable<Type> typeList = GetDerivedTypes(assemblies, type);

            foreach (Type t in typeList)
            {
                try
                {
                    TypeNameAttribute attribute = GetTypeName(t);
                    result.Add(new Tuple<string, string, Type>(attribute.Name, attribute.Description, t));
                }
                catch
                {
                }
            }

            return result;
        }
        /// <summary>
        /// 获取类型名称特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeNameAttribute GetTypeName(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return type.GetTypeInfo().GetCustomAttribute<TypeNameAttribute>(false);
        }
    }
}
