using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Common
{
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,AllowMultiple = true)]
    public class DisplayAttribute : System.Attribute
    {
        public DisplayAttribute(string name)
        {
            this.name = name;
        }

        private string name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
