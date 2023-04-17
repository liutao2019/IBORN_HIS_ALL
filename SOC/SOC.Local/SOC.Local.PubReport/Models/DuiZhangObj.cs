using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.PubReport.Models
{
    public class DuiZhangObj : FS.FrameWork.Models.NeuObject
    {
        public DuiZhangObj()
        {
            this.GYPubFee = 0;
            this.GYTotFee = 0;
            this.SFPubFee = 0;
            this.SFTotFee = 0;
            this.ComparePubFee = 0;
            this.CompareTotFee = 0;
        }






        /// <summary>
        /// 公医处发票号
        /// </summary>
        public string GYInvoiceNo;

        /// <summary>
        /// 公医处日期
        /// </summary>
        public DateTime GYOperDate = DateTime.MinValue;

        /// <summary>
        /// 公医处操作员
        /// </summary>
        public string GYOperCode;

        /// <summary>
        /// 公医处总金额
        /// </summary>
        public decimal GYTotFee;

        /// <summary>
        /// 公医处记帐金额
        /// </summary>
        public decimal GYPubFee;



        /// <summary>
        /// 收费处发票号
        /// </summary>
        public string SFInvoiceNo;

        /// <summary>
        /// 收费处日期
        /// </summary>
        public DateTime SFOperDate = DateTime.MinValue;

        /// <summary>
        /// 收费处操作员
        /// </summary>
        public string SFOperCode;

        /// <summary>
        /// 收费处总金额
        /// </summary>
        public decimal SFTotFee;

        /// <summary>
        /// 收费处记帐金额
        /// </summary>
        public decimal SFPubFee;


        public decimal ComparePubFee;
        public decimal CompareTotFee;
        public string Pact_Code;
        public string Pact_Name;
        public string MCardNo;
        public string Name;
        public string Memo = "true";

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new PubReport Clone()
        {
            PubReport obj = base.Clone() as PubReport;
            return obj;
        }
    }
}
