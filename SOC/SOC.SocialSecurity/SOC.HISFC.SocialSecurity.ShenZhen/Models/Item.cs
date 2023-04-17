using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.Models
{
    /// <summary>
    /// [功能描述: 项目信息]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-7]<br></br>
    /// </summary>
    public class Item:BaseModel
    {
        
        private string curCompareCode = "";
        private string curCompareMemo = "";
        private string curExtend1 = "";

        private QueryCode curQueryCode = new QueryCode();
        private Oper curOper = new Oper();

        public string CompareCode
        {
            get { return curCompareCode; }
            set { curCompareCode = value; }
        }

        public string CompareMemo
        {
            get { return curCompareMemo; }
            set { curCompareMemo = value; }
        }

        public string Extend1
        {
            get { return curExtend1; }
            set { curExtend1 = value; }
        }

        public QueryCode QueryCode
        {
            get { return curQueryCode; }
            set { curQueryCode = value; }
        }

        public Oper Oper
        {
            get { return curOper; }
            set { curOper = value; }
        }   
       
        public new Item Clone()
        {
            Item item = base.Clone() as Item;
            item.QueryCode = this.QueryCode.Clone();
            item.Oper = this.Oper.Clone();

            return item;
        }
    }
}
