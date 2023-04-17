using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Models.Pharmacy.Adjust
{
    /// <summary>
    /// [功能描述: 药品零售价调价价格生成公式特殊药品实体]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2012-12]<br></br>
    /// </summary>
    public class SpecialDrugFormula
    {
        private string curID = "";

        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            get { return curID; }
            set { curID = value; }
        }

        private string drugNO = "";

        /// <summary>
        /// 药品编码
        /// </summary>
        public string DrugNO
        {
            get { return drugNO; }
            set { drugNO = value; }
        }

        private string drugName = "";

        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugName
        {
            get { return drugName; }
            set { drugName = value; }
        }

        private string drugSpecs = "";

        /// <summary>
        /// 药品规格
        /// </summary>
        public string DrugSpecs
        {
            get { return drugSpecs; }
            set { drugSpecs = value; }
        }

        private string formula = "";

        /// <summary>
        /// 公式
        /// </summary>
        public string Formula
        {
            get { return formula; }
            set { formula = value; }
        }

        private string validState = "";

        /// <summary>
        /// 有效标记
        /// </summary>
        public string ValidState
        {
            get { return validState; }
            set { validState = value; }
        }

        private string operID = "";

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperID
        {
            get { return operID; }
            set { operID = value; }
        }

        private DateTime operTime = new DateTime();

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime
        {
            get { return operTime; }
            set { operTime = value; }
        }

        public SpecialDrugFormula Clone()
        {
            return this.MemberwiseClone() as SpecialDrugFormula;
        }
    }
}