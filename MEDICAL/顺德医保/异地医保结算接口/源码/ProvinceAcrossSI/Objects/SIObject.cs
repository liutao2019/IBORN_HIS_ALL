using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProvinceAcrossSI.Objects
{
    /// <summary>
    /// 医保基类
    /// </summary>
    public class SIObject
    {
        private string id;

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private string mark;

        public string Mark
        {
            get { return this.mark; }
            set { this.mark = value; }
        }
    }
}
