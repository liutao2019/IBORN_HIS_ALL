using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYZL.Report
{
    public class InpatientFee
    {
        private string inpatientNo;

        /// <summary>
        /// 住院流水号
        /// </summary>
        public string InpatientNo
        {
            set { this.inpatientNo = value; }
            get { return this.inpatientNo; }
        }

        private string feeDate;

        /// <summary>
        /// 费用日期
        /// </summary>
        public string FeeDate
        {
            set { this.feeDate = value; }
            get { return this.feeDate; }
        }

        private FeeType type;

        /// <summary>
        /// 费用类别(应收|已结)
        /// </summary>
        public FeeType Type
        {
            set { this.type = value; }
            get { return this.type; }
        }

        private decimal fee;
        
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal Fee
        {
            set { this.fee = value; }
            get { return this.fee; }
        }
    }

    public enum FeeType
    {
        应收,
        已结,
        期初
    }
}
