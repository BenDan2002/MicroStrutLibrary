using System;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConfigurationEntryAttribute: Attribute
    {
        public string Path { get; private set; }
        public bool Default { get; set; } = true;

        public ConfigurationEntryAttribute(string path)
        {
            this.Path = path;
        }
    }
}
