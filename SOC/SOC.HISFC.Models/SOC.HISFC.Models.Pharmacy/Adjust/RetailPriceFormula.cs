using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Models.Pharmacy.Adjust
{
    /// <summary>
    /// [功能描述: 药品零售价调价价格生成公式实体]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2012-12]<br></br>
    /// </summary>
    public class RetailPriceFormula : FS.FrameWork.Models.NeuObject
    {
        FS.FrameWork.Models.NeuObject drugType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 药品类别
        /// </summary>
        public FS.FrameWork.Models.NeuObject DrugType
        {
            get { return drugType; }
            set { drugType = value; }
        }

        private string priceType = "";

        /// <summary>
        /// 价格形式
        /// 0购入价；1批发价；2批发价
        /// </summary>
        public string PriceType
        {
            get { return priceType; }
            set { priceType = value; }
        }

        private decimal priceLower = 0;

        /// <summary>
        /// 价格区间的下限
        /// </summary>
        public decimal PriceLower
        {
            get { return priceLower; }
            set { priceLower = value; }
        }

        private decimal priceUpper = 0;

        /// <summary>
        /// 价格区间的上限
        /// </summary>
        public decimal PriceUpper
        {
            get { return priceUpper; }
            set { priceUpper = value; }
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

        private string fomulaType = "";
        /// <summary>
        /// 是否基本药物 0 非基药 1基药 2疫苗
        /// </summary>
        public string FomulaType
        {
            get { return fomulaType; }
            set { fomulaType = value; }
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

        public new RetailPriceFormula Clone()
        {
            RetailPriceFormula rf = base.Clone() as RetailPriceFormula;

            rf.DrugType = this.DrugType.Clone();

            return rf;
        }
    }
}
