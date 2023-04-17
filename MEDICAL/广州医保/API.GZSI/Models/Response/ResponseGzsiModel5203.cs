using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 结算信息查询
    /// </summary>
    /// <summary>
    /// 结算信息查询
    /// </summary>
    public class ResponseGzsiModel5203 : ResponseBase
    {
        public Setlinfo setlinfo { get; set; }
        public class Setlinfo
        {
            ///<summary>
            ///结算 ID 	
            ///<summary>
            public string setl_id { get; set; }

            ///<summary>
            ///就诊 ID 	
            ///<summary>
            public string mdtrt_id { get; set; }

            ///<summary>
            ///人员编号 	
            ///<summary>
            public string psn_no { get; set; }


            ///<summary>
            ///人员姓名 	
            ///<summary>
            public string psn_name { get; set; }

            ///<summary>
            ///人员证件类型 	
            ///<summary>
            public string psn_cert_type { get; set; }

            ///<summary>
            ///证件号码 	
            ///<summary>
            public string certno { get; set; }

            ///<summary>
            ///性别 	
            ///<summary>
            public string gend { get; set; }

            ///<summary>
            ///民族 	
            ///<summary>
            public string naty { get; set; }

            ///<summary>
            ///出生日期 	
            ///<summary>
            public string brdy { get; set; }

            ///<summary>
            ///年龄 	
            ///<summary>
            public string age { get; set; }

            ///<summary>
            ///险种类型 	
            ///<summary>
            public string insutype { get; set; }

            ///<summary>
            ///人员类别 	
            ///<summary>
            public string psn_type { get; set; }

            ///<summary>
            ///公务员标志 	
            ///<summary>
            public string cvlserv_flag { get; set; }

            ///<summary>
            ///灵活就业标志	
            ///<summary>
            public string flxempe_flag { get; set; }

            ///<summary>
            ///新生儿标志 	
            ///<summary>
            public string nwb_flag { get; set; }

            ///<summary>
            ///参保机构医保区划 	
            ///<summary>
            public string insu_optins { get; set; }

            ///<summary>
            ///单位名称 	
            ///<summary>
            public string emp_name { get; set; }

            ///<summary>
            ///支付地点类别 	
            ///<summary>
            public string pay_loc { get; set; }

            ///<summary>
            ///定点医药机构编号 	
            ///<summary>
            public string fixmedins_code { get; set; }

            ///<summary>
            ///定点医药机构名称 	
            ///<summary>
            public string fixmedins_name { get; set; }

            ///<summary>
            ///医院等级 	
            ///<summary>
            public string hosp_lv { get; set; }

            ///<summary>
            ///定点归属机构 	
            ///<summary>
            public string fixmedins_poolarea { get; set; }

            ///<summary>
            ///限价医院等级 	
            ///<summary>
            public string lmtpric_hosp_lv { get; set; }

            ///<summary>
            ///起付线医院等级 	
            ///<summary>
            public string dedc_hosp_lv { get; set; }

            ///<summary>
            ///开始日期 	
            ///<summary>
            public string begndate { get; set; }

            ///<summary>
            ///结束日期 	
            ///<summary>
            public string enddate { get; set; }

            ///<summary>
            ///结算时间 	
            ///<summary>
            public string setl_time { get; set; }

            ///<summary>
            ///就诊凭证类型 	
            ///<summary>
            public string mdtrt_cert_type { get; set; }

            ///<summary>
            ///医疗类别 	
            ///<summary>
            public string med_type { get; set; }

            ///<summary>
            ///清算类别 	
            ///<summary>
            public string clr_type { get; set; }

            ///<summary>
            ///清算方式 	
            ///<summary>
            public string clr_way { get; set; }

            ///<summary>
            ///清算经办机构 	
            ///<summary>
            public string clr_optins { get; set; }

            ///<summary>
            ///医疗费总额 	
            ///<summary>
            public string medfee_sumamt { get; set; }

            ///<summary>
            ///全自费金额 	
            ///<summary>
            public string fulamt_ownpay_amt { get; set; }

            ///<summary>
            ///超限价自费费用 	
            ///<summary>
            public string overlmt_selfpay { get; set; }

            ///<summary>
            ///先行自付金额 	
            ///<summary>
            public string preselfpay_amt { get; set; }

            ///<summary>
            ///符合政策范围金额 	
            ///<summary>
            public string inscp_scp_amt { get; set; }

            ///<summary>
            ///实际支付起付线 	
            ///<summary>
            public string act_pay_dedc { get; set; }

            ///<summary>
            ///基本医疗保险统筹基金支出 	
            ///<summary>
            public string hifp_pay { get; set; }

            ///<summary>
            ///基本医疗保险统筹基金支付比例 	
            ///<summary>
            public string pool_prop_selfpay { get; set; }

            ///<summary>
            ///医保认可费用总额	
            ///<summary>
            public string med_sumfee { get; set; }

            ///<summary>
            ///公务员医疗补助资金支出 	
            ///<summary>
            public string cvlserv_pay { get; set; }

            ///<summary>
            ///企业补充医疗保险基金支出 	
            ///<summary>
            public string hifes_pay { get; set; }

            ///<summary>
            ///居民大病保险资金支出 	
            ///<summary>
            public string hifmi_pay { get; set; }

            ///<summary>
            ///职工大额医疗费用补助基金支出 	
            ///<summary>
            public string hifob_pay { get; set; }

            ///<summary>
            ///伤残人员医疗保障基金支出	
            ///<summary>
            public string hifdm_pay { get; set; }

            ///<summary>
            ///医疗救助基金支出 	
            ///<summary>
            public string maf_pay { get; set; }

            ///<summary>
            ///其他基金支出 	
            ///<summary>
            public string oth_pay { get; set; }

            ///<summary>
            ///基金支付总额 	
            ///<summary>
            public string fund_pay_sumamt { get; set; }

            ///<summary>
            ///个人支付金额 	
            ///<summary>
            public string psn_pay { get; set; }

            ///<summary>
            ///个人账户支出 	
            ///<summary>
            public string acct_pay { get; set; }

            ///<summary>
            ///现金支付金额 	
            ///<summary>
            public string cash_payamt { get; set; }

            ///<summary>
            ///余额 	
            ///<summary>
            public string balc { get; set; }

            ///<summary>
            ///个人账户共济支付金额 	
            ///<summary>
            public string acct_mulaid_pay { get; set; }

            ///<summary>
            ///医药机构结算 ID 	
            ///<summary>
            public string medins_setl_id { get; set; }

            ///<summary>
            ///退费结算标志 	
            ///<summary>
            public string refd_setl_flag { get; set; }

            ///<summary>
            ///年度 	
            ///<summary>
            public string year { get; set; }

            ///<summary>
            ///病种编码 	
            ///<summary>
            public string dise_codg { get; set; }

            ///<summary>
            ///病种名称 	
            ///<summary>
            public string dise_name { get; set; }

            ///<summary>
            ///发票号 	
            ///<summary>
            public string invono { get; set; }

            ///<summary>
            ///经办人 ID 	
            ///<summary>
            public string opter_id { get; set; }

            ///<summary>
            ///经办人姓名 	
            ///<summary>
            public string opter_name { get; set; }

            ///<summary>
            ///经办时间 	
            ///<summary>
            public string opt_time { get; set; }
        }



        public List<Setldetail> setldetail { get; set; }
        public class Setldetail
        {

            ///<summary>
            ///基金支付类型	
            ///<summary>
            public string fund_pay_type { get; set; }

            ///<summary>
            ///基金支付名称	
            ///<summary>
            public string fund_pay_type_name { get; set; }

            ///<summary>
            ///符合范围金额	
            ///<summary>
            public string inscp_scp_amt { get; set; }

            ///<summary>
            ///本次可支付限额金额	
            ///<summary>
            public string crt_payb_lmt_amt { get; set; }

            ///<summary>
            ///基金支付金额	
            ///<summary>
            public string fund_payamt { get; set; }

            ///<summary>
            ///结算过程信息	
            ///<summary>
            public string setl_proc_info { get; set; }
        }
    }
}
