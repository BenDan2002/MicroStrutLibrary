using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using System.IO;

namespace MicroStrutLibrary.Infrastructure.Core.Reflection
{
    public class DefaultAssemblyFinder : IAssemblyFinder
    {
        private IEnumerable<string> assembiles;

        public DefaultAssemblyFinder(IEnumerable<string> assembiles)
        {
            this.assembiles = assembiles;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            var dependencyContext = DependencyContext.Default;
            var entryAssembly = Assembly.GetEntryAssembly();
            var entryAssemblyName = entryAssembly.GetName();

            var entryLibrary = dependencyContext.CompileLibraries.SingleOrDefault(o =>
            {
                return o.Name.ToLower() == entryAssemblyName.Name.ToLower();
            });

            var referencedAssemblies = dependencyContext.CompileLibraries.Where(cl =>
            {
                return
                    cl.Assemblies.Any(assembly =>
                    {
                        return cl.Name.Equals(Path.GetFileNameWithoutExtension(assembly), StringComparison.OrdinalIgnoreCase);
                    })
                    &&
                    entryLibrary.Dependencies.Any(l =>
                    {
                        return cl.Name.Equals(l.Name, StringComparison.OrdinalIgnoreCase) && cl.Version == l.Version;
                    });
            }).Select(cl =>
            {
                return Assembly.Load(cl.Name);
            }).Where(assembly =>
            {
                return !assembly.IsDynamic;
            });

            yield return entryAssembly;

            foreach (var assembly in referencedAssemblies)
            {
                yield return assembly;
            }
        }
    }
}
