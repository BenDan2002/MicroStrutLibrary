using System;
using System.Collections.Generic;
using System.Text;

namespace MicroStrutLibrary.Infrastructure.Core.Reflection
{
    /// <summary>
    /// 类型名称特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TypeNameAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public TypeNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
