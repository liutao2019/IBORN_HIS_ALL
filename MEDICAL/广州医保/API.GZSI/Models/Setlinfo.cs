using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    /// 门诊/住院结算返回共用
    /// </summary>
    public class SetlInfo
    {
        /// <summary> 
        /// 就诊ID Y 
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary> 
        /// 结算ID Y 
        /// </summary>
        public string setl_id { get; set; }
        /// <summary> 
        /// 人员编号 Y 
        /// </summary>
        public string psn_no { get; set; }
        /// <summary> 
        /// 人员姓名 Y 
        /// </summary>
        public string psn_name { get; set; }
        /// <summary> 
        /// 人员证件类型 Y 
        /// </summary>
        public string psn_cert_type { get; set; }
        /// <summary> 
        /// 证件号码 Y 
        /// </summary>
        public string certno { get; set; }
        /// <summary> 
        /// 性别  
        /// </summary>
        public string gend { get; set; }
        /// <summary> 
        /// 民族  
        /// </summary>
        public string naty { get; set; }
        /// <summary> 
        /// 出生日期  
        /// </summary>
        public string brdy { get; set; }
        /// <summary> 
        /// 年龄  
        /// </summary>
        public string age { get; set; }
        /// <summary> 
        /// 险种类型  
        /// </summary>
        public string insutype { get; set; }
        /// <summary> 
        /// 人员类别 Y 
        /// </summary>
        public string psn_type { get; set; }
        /// <summary> 
        /// 公务员标志 Y 
        /// </summary>
        public string cvlserv_flag { get; set; }
        /// <summary> 
        /// 结算时间  
        /// </summary>
        public string setl_time { get; set; }
        /// <summary> 
        /// 个人结算方式 Y 
        /// </summary>
        public string psn_setlway { get; set; }
        /// <summary> 
        /// 就诊凭证类型  
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary> 
        /// 医疗类别 Y 
        /// </summary>
        public string med_type { get; set; }
        /// <summary> 
        /// 医疗费总额 Y 
        /// </summary>
        public string medfee_sumamt { get; set; }
        /// <summary> 
        /// 全自费金额 Y 
        /// </summary>
        public string fulamt_ownpay_amt { get; set; }
        /// <summary> 
        /// 超限价自费费用 Y 
        /// </summary>
        public string overlmt_selfpay { get; set; }
        /// <summary> 
        /// 先行自付金额 Y 
        /// </summary>
        public string preselfpay_amt { get; set; }
        /// <summary> 
        /// 符合范围金额 Y 
        /// </summary>
        public string inscp_scp_amt { get; set; }
        /// <summary> 
        /// 医保认可费用总额 Y 
        /// </summary>
        public string med_sumfee { get; set; }
        /// <summary> 
        /// 实际支付起付线  
        /// </summary>
        public string act_pay_dedc { get; set; }
        /// <summary> 
        /// 基本医疗保险统筹基金支出 Y 
        /// </summary>
        public string hifp_pay { get; set; }
        /// <summary> 
        /// 基本医疗保险统筹基金支付比例 Y 
        /// </summary>
        public string pool_prop_selfpay { get; set; }
        /// <summary>
        /// 公务员医疗补助资金支出 Y 
        /// </summary>
        public string cvlserv_pay { get; set; }
        /// <summary> 
        /// 企业补充医疗保险基金支出 Y 
        /// </summary>
        public string hifes_pay { get; set; }
        /// <summary> 
        /// 居民大病保险资金支出 Y 
        /// </summary>
        public string hifmi_pay { get; set; }
        /// <summary> 
        /// 职工大额医疗费用补助基金支出 Y 
        /// </summary>
        public string hifob_pay { get; set; }
        /// <summary> 
        /// 伤残人员医疗保障基金支出 Y 
        /// </summary>
        public string hifdm_pay { get; set; }
        /// <summary> 
        /// 医疗救助基金支出 Y 
        /// </summary>
        public string maf_pay { get; set; }
        /// <summary> 
        /// 其他基金支出 Y 
        /// </summary>
        public string oth_pay { get; set; }
        /// <summary> 
        /// 基金支付总额 Y 
        /// </summary>
        public string fund_pay_sumamt { get; set; }
        /// <summary> 
        /// 医院负担金额 Y 
        /// </summary>
        public string hosp_part_amt { get; set; }
        /// <summary> 
        /// 个人负担总金额 Y 
        /// </summary>
        public string psn_part_am { get; set; }
        /// <summary> 
        /// 个人账户支出 Y 
        /// </summary>
        public string acct_pay { get; set; }
        /// <summary>
        /// 现金支付金额 Y 
        /// </summary>
        public string psn_cash_pay { get; set; }
        /// <summary> 
        /// 账户共济支付金额 Y 
        /// </summary>
        public string acct_mulaid_pay { get; set; }
        /// <summary> 
        /// 个人账户支出后余额 Y 
        /// </summary>
        public string balc { get; set; }
        /// <summary> 
        /// 清算经办机构  
        /// </summary>
        public string clr_optins { get; set; }
        /// <summary> 
        /// 清算方式  
        /// </summary>
        public string clr_way { get; set; }
        /// <summary> 
        /// 清算类别 Y 
        /// </summary>
        public string clr_type { get; set; }
        /// <summary> 
        /// 医药机构结算ID Y  
        /// </summary>
        public string medins_setl_id { get; set; }
        /// <summary> 
        /// 备注  
        /// </summary>
        public string memo { get; set; }
    }
}
