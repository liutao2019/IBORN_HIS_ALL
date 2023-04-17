using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.Models
{
    /// <summary>
    /// [功能描述: 医保药品对照维护]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-7]<br></br>
    /// </summary>
    public class DrugCompare:Item
    {
        private string curDrugSpecs = "";


        public string DrugSpecs
        {
            get { return curDrugSpecs; }
            set { curDrugSpecs = value; }
        }

      
        public DrugCompare Clone()
        {
            DrugCompare dc = base.Clone() as DrugCompare;

            return dc;
        }
    }
}
