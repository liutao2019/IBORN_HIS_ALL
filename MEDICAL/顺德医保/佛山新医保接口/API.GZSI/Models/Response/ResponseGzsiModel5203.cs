using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5203 : ResponseBase
    {
        /// <summary>
        /// 输出
        /// </summary>
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            /// <summary>
            /// 结算信息
            /// </summary>
            public SetlInfo setlinfo { get; set; }

            /// <summary>
            /// 结算基金分项信息
            /// </summary>
            public List<Setldetail> setldetail { get; set; }

            /// <summary>
            /// 结算信息
            /// </summary>
            public class SetlInfo
            {
                /// <summary> 
                /// 就诊ID Y 
                /// </summary>
                [API.GZSI.Common.Display("就诊ID")]
                public string mdtrt_id { get; set; }
                /// <summary> 
                /// 结算ID Y 
                /// </summary>
                [API.GZSI.Common.Display("结算ID")]
                public string setl_id { get; set; }
                /// <summary> 
                /// 人员编号 Y 
                /// </summary>
                [API.GZSI.Common.Display("人员编号")]
                public string psn_no { get; set; }
                /// <summary> 
                /// 人员姓名 Y 
                /// </summary>
                [API.GZSI.Common.Display("人员姓名")]
                public string psn_name { get; set; }
                /// <summary> 
                /// 人员证件类型 Y 
                /// </summary>
                [API.GZSI.Common.Display("人员证件类型")]
                public string psn_cert_type { get; set; }
                /// <summary> 
                /// 证件号码 Y 
                /// </summary>
                [API.GZSI.Common.Display("证件号码")]
                public string certno { get; set; }
                /// <summary> 
                /// 性别  
                /// </summary>
                [API.GZSI.Common.Display("性别")]
                public string gend { get; set; }
                /// <summary> 
                /// 民族  
                /// </summary>
                [API.GZSI.Common.Display("民族")]
                public string naty { get; set; }
                /// <summary> 
                /// 出生日期  
                /// </summary>
                [API.GZSI.Common.Display("出生日期")]
                public string brdy { get; set; }
                /// <summary> 
                /// 年龄  
                /// </summary>
                [API.GZSI.Common.Display("年龄")]
                public string age { get; set; }
                /// <summary> 
                /// 险种类型  
                /// </summary>
                [API.GZSI.Common.Display("险种类型")]
                public string insutype { get; set; }
                /// <summary> 
                /// 人员类别 Y 
                /// </summary>
                [API.GZSI.Common.Display("人员类别")]
                public string psn_type { get; set; }
                /// <summary> 
                /// 公务员标志 Y 
                /// </summary>
                [API.GZSI.Common.Display("公务员标志")]
                public string cvlserv_flag { get; set; }
                /// <summary> 
                /// 灵活就业标志  
                /// </summary>
                [API.GZSI.Common.Display("灵活就业标志")]
                public string flxempe_flag { get; set; }
                /// <summary> 
                /// 新生儿标志 
                /// </summary>
                [API.GZSI.Common.Display("新生儿标志")]
                public string nwb_flag { get; set; }
                /// <summary> 
                /// 参保机构医保区划  
                /// </summary>
                [API.GZSI.Common.Display("参保机构医保区划")]
                public string insu_optins { get; set; }
                /// <summary> 
                /// 单位名称  
                /// </summary>
                [API.GZSI.Common.Display("单位名称")]
                public string emp_name { get; set; }
                /// <summary> 
                /// 支付地点类别   Y
                /// </summary>
                [API.GZSI.Common.Display("支付地点类别")]
                public string pay_loc { get; set; }
                /// <summary> 
                /// 定点医药机构编号   Y
                /// </summary>
                [API.GZSI.Common.Display("定点医药机构编号")]
                public string fixmedins_code { get; set; }
                /// <summary> 
                /// 定点医药机构名称  Y
                /// </summary>
                [API.GZSI.Common.Display("定点医药机构名称")]
                public string fixmedins_name { get; set; }
                /// <summary> 
                /// 医院等级   
                /// </summary>
                [API.GZSI.Common.Display("医院等级")]
                public string hosp_lv { get; set; }
                /// <summary> 
                /// 定点归属机构 
                /// </summary>
                [API.GZSI.Common.Display("定点归属机构")]
                public string fixmedins_poolarea { get; set; }
                /// <summary> 
                /// 限价医院等级  
                /// </summary>
                [API.GZSI.Common.Display("限价医院等级")]
                public string lmtpric_hosp_lv { get; set; }
                /// <summary> 
                /// 起付线医院等级  
                /// </summary>
                [API.GZSI.Common.Display("起付线医院等级")]
                public string dedc_hosp_lv { get; set; }
                /// <summary> 
                /// 开始日期  Y
                /// </summary>
                [API.GZSI.Common.Display("开始日期")]
                public string begndate { get; set; }
                /// <summary> 
                /// 结束日期  Y
                /// </summary>
                [API.GZSI.Common.Display("结束日期")]
                public string enddate { get; set; }
                /// <summary> 
                /// 结算时间  
                /// </summary>
                [API.GZSI.Common.Display("结算时间")]
                public string setl_time { get; set; }
                /// <summary> 
                /// 个人结算方式 Y 
                /// </summary>
                //public string psn_setlway { get; set; }
                /// <summary> 
                /// 就诊凭证类型  
                /// </summary>
                [API.GZSI.Common.Display("就诊凭证类型")]
                public string mdtrt_cert_type { get; set; }
                /// <summary> 
                /// 医疗类别 Y 
                /// </summary>
                [API.GZSI.Common.Display("医疗类别")]
                public string med_type { get; set; }
                /// <summary> 
                /// 清算类别  Y 
                /// </summary>
                [API.GZSI.Common.Display("清算类别")]
                public string clr_type { get; set; }
                /// <summary> 
                /// 清算方式  
                /// </summary>
                [API.GZSI.Common.Display("清算方式")]
                public string clr_way { get; set; }
                /// <summary> 
                /// 清算经办机构  
                /// </summary>
                [API.GZSI.Common.Display("清算经办机构")]
                public string clr_optins { get; set; }
                /// <summary> 
                /// 医疗费总额 Y 
                /// </summary>
                [API.GZSI.Common.Display("医疗费总额")]
                public string medfee_sumamt { get; set; }
                /// <summary> 
                /// 全自费金额 Y 
                /// </summary>
                [API.GZSI.Common.Display("全自费金额")]
                public string fulamt_ownpay_amt { get; set; }
                /// <summary> 
                /// 超限价自费费用 Y 
                /// </summary>
                [API.GZSI.Common.Display("超限价自费费用")]
                public string overlmt_selfpay { get; set; }
                /// <summary> 
                /// 先行自付金额 Y 
                /// </summary>
                [API.GZSI.Common.Display("先行自付金额")]
                public string preselfpay_amt { get; set; }
                /// <summary> 
                /// 符合范围金额 Y 
                /// </summary>
                [API.GZSI.Common.Display("符合范围金额")]
                public string inscp_scp_amt { get; set; }
                /// <summary> 
                /// 医保认可费用总额 Y 
                /// </summary>
                [API.GZSI.Common.Display("医保认可费用总额")]
                public string med_sumfee { get; set; }
                /// <summary> 
                /// 实际支付起付线  
                /// </summary>
                [API.GZSI.Common.Display("实际支付起付线")]
                public string act_pay_dedc { get; set; }
                /// <summary> 
                /// 基本医疗保险统筹基金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("基本医疗保险统筹基金支出")]
                public string hifp_pay { get; set; }
                /// <summary> 
                /// 基本医疗保险统筹基金支付比例 Y 
                /// </summary>
                [API.GZSI.Common.Display("基本医疗保险统筹基金支付比例")]
                public string pool_prop_selfpay { get; set; }
                /// <summary>
                /// 公务员医疗补助资金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("公务员医疗补助资金支出")]
                public string cvlserv_pay { get; set; }
                /// <summary> 
                /// 企业补充医疗保险基金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("企业补充医疗保险基金支出")]
                public string hifes_pay { get; set; }
                /// <summary> 
                /// 居民大病保险资金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("居民大病保险资金支出")]
                public string hifmi_pay { get; set; }
                /// <summary> 
                /// 职工大额医疗费用补助基金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("职工大额医疗费用补助基金支出")]
                public string hifob_pay { get; set; }
                /// <summary> 
                /// 伤残人员医疗保障基金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("伤残人员医疗保障基金支出")]
                public string hifdm_pay { get; set; }
                /// <summary> 
                /// 医疗救助基金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("医疗救助基金支出")]
                public string maf_pay { get; set; }
                /// <summary> 
                /// 其他基金支出 Y 
                /// </summary>
                [API.GZSI.Common.Display("其他基金支出")]
                public string oth_pay { get; set; }
                /// <summary> 
                /// 基金支付总额 Y 
                /// </summary>
                [API.GZSI.Common.Display("基金支付总额")]
                public string fund_pay_sumamt { get; set; }
                /// <summary> 
                /// 个人支付金额  Y 
                /// </summary>
                [API.GZSI.Common.Display("个人支付金额")]
                public string psn_pay { get; set; }
                /// <summary> 
                /// 个人账户支出  Y 
                /// </summary>
                [API.GZSI.Common.Display("个人账户支出")]
                public string acct_pay { get; set; }
                /// <summary> 
                /// 现金支付金额  Y 
                /// </summary>
                [API.GZSI.Common.Display("现金支付金额")]
                public string cash_payamt { get; set; }
                /// <summary> 
                /// 个人账户支出后余额 Y 
                /// </summary>
                [API.GZSI.Common.Display("个人账户支出后余额")]
                public string balc { get; set; }
                /// <summary> 
                /// 账户共济支付金额 Y 
                /// </summary>
                [API.GZSI.Common.Display("账户共济支付金额")]
                public string acct_mulaid_pay { get; set; }
                /// <summary> 
                /// 医药机构结算ID Y  
                /// </summary>
                [API.GZSI.Common.Display("医药机构结算ID")]
                public string medins_setl_id { get; set; }
                /// <summary> 
                /// 退费结算标志  Y  
                /// </summary>
                [API.GZSI.Common.Display("退费结算标志")]
                public string refd_setl_flag { get; set; }
                /// <summary> 
                /// 年度   Y  
                /// </summary>
                [API.GZSI.Common.Display("年度")]
                public string year { get; set; }
                /// <summary> 
                /// 病种编码  
                /// </summary>
                [API.GZSI.Common.Display("病种编码")]
                public string dise_codg { get; set; }
                /// <summary> 
                /// 病种名称   
                /// </summary>
                [API.GZSI.Common.Display("病种名称")]
                public string dise_name { get; set; }
                /// <summary> 
                /// 发票号   
                /// </summary>
                [API.GZSI.Common.Display("发票号")]
                public string invono { get; set; }
                /// <summary> 
                /// 经办人 ID   
                /// </summary>
                [API.GZSI.Common.Display("经办人ID")]
                public string opter_id { get; set; }
                /// <summary> 
                /// 经办人姓名   
                /// </summary>
                [API.GZSI.Common.Display("经办人姓名")]
                public string opter_name { get; set; }
                /// <summary> 
                ///  经办时间   
                /// </summary>
                [API.GZSI.Common.Display("经办时间")]
                public string opt_time { get; set; }

            }

            /// <summary>
            /// 结算基金分项信息
            /// </summary>
            public class Setldetail
            {
                #region 基金分项setldetail
                /// <summary> 
                /// 基金支付类型 
                /// <summary> 
                public string fund_pay_type { get; set; }
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

    }
}
