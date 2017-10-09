using System.Collections.Generic;
using System.Reflection;

namespace MicroStrutLibrary.Infrastructure.Core.Reflection
{
    public interface IAssemblyFinder
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
