using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.Models
{
    /// <summary>
    /// [功能描述: 查询码基类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-7]<br></br>
    /// </summary>
    public class QueryCode
    {
        private string curSpellCode = "";
        private string curWBCode = "";

        public string SpellCode
        {
            get { return curSpellCode; }
            set { curSpellCode = value; }
        }

        public string WBCode
        {
            get { return curWBCode; }
            set { curWBCode = value; }
        }

        public QueryCode Clone()
        {
            return this.MemberwiseClone() as QueryCode;
        }
    }
}
