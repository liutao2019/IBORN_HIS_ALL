using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.Models
{
    /// <summary>
    /// [功能描述: 操作员信息]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-7]<br></br>
    /// </summary>
    public class Oper
    {
        private string curID = "";

        public string ID
        {
            get { return curID; }
            set { curID = value; }
        }
        private string curName = "";

        public string Name
        {
            get { return curName; }
            set { curName = value; }
        }
        private string curOperTime = "";

        public string OperTime
        {
            get { return curOperTime; }
            set { curOperTime = value; }
        }

        public Oper Clone()
        {
            return this.MemberwiseClone() as Oper;
        }

    }
}
