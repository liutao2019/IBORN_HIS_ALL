using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.SubFeeSet
{
    /// <summary>
    /// 附材收取日志表（用于记录已进行首次收取附材的医嘱）
    /// </summary>
    class SubFeeLog
    {
        /// <summary>
        /// 住院流水号
        /// </summary>
        private string inpatientNo;

        /// <summary>
        /// 住院流水号
        /// </summary>
        public string InpatientNo
        {
            get
            {
                return inpatientNo;
            }
            set
            {
                inpatientNo = value;
            }
        }

        /// <summary>
        /// 医嘱流水号
        /// </summary>
        private string moOrder;

        /// <summary>
        /// 医嘱流水号
        /// </summary>
        public string MoOrder
        {
            get
            {
                return moOrder;
            }
            set
            {
                moOrder = value;
            }
        }

        /// <summary>
        /// 操作员
        /// </summary>
        private string operCode;

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperCode
        {
            get
            {
                return operCode;
            }
            set
            {
                operCode = value;
            }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        private DateTime operDate;

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return operDate;
            }
            set
            {
                operDate = value;
            }
        }
    }
}
