using System;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EntityDbNameAttribute: Attribute
    {
        public string Name { get; private set; }

        public EntityDbNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
