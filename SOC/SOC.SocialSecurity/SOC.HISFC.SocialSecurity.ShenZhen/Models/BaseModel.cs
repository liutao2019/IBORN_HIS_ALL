using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.Models
{
    /// <summary>
    /// [功能描述: 实体基类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-7]<br></br>
    /// </summary>
    public class BaseModel
    {
        private string curID = "";
        private string curName = "";

        public string ID
        {
            get { return this.curID; }
            set { this.curID = value; }
        }

        public string Name
        {
            get { return this.curName; }
            set { this.curName = value; }
        }

        public BaseModel Clone()
        {
            return this.MemberwiseClone() as BaseModel;
        }
    }
}
