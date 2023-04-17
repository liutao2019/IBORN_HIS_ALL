using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    ///  门诊/住院结算共用返回广州医保基金分项setldetail
    /// </summary>
    public class Setldetail
    {
        #region 基金分项setldetail
        /// <summary> 
        /// 就诊ID 
        /// <summary> 
        public string mdtrt_id { get; set; }
        /// <summary> 
        /// 结算事件ID 
        /// <summary> 
        public string setl_id { get; set; }
        /// <summary> 
        /// 基金支付类型 
        /// <summary> 
        public string fund_pay_type { get; set; }
        /// <summary> 
        /// 基金支付名称 
        /// <summary> 
        public string fund_pay_type_name { get; set; }
        /// <summary> 
        /// 符合范围金额 
        /// <summary> 
        public string inscp_scp_amt { get; set; }
        /// <summary> 
        /// 本次可支付限额金额 
        /// <summary> 
        public string crt_payb_lmt_amt { get; set; }
        /// <summary> 
        /// 基金支付金额 
        /// <summary> 
        public string fund_payamt { get; set; }
        /// <summary> 
        /// 结算过程信息 
        /// <summary> 
        public string setl_proc_info { get; set; }
        #endregion
    }
}
