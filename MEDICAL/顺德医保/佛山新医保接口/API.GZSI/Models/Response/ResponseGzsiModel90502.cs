using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 对数明细信息查询接口
    /// </summary>
    public class ResponseGzsiModel90502 : ResponseBase
    {
        public Output output { get; set; }
        public class Output
        {
            /// <summary>
            /// 	总行数	 
            /// </summary>
            public string totalRow { get; set; }
            /// <summary>
            /// 	总页数	 
            /// </summary>
            public string totalPage { get; set; }

            public List<Setlinfo> setlinfo { get; set; }
            public class Setlinfo
            {
                /// <summary>
                /// 	顺序号	 
                /// </summary>
                public string seqno { get; set; }
                /// <summary>
                /// 	结算ID	 
                /// </summary>
                public string setl_id { get; set; }
                /// <summary>
                /// 	就诊ID	 
                /// </summary>
                public string mdtrt_id { get; set; }
                /// <summary>
                /// 	人员编号	 
                /// </summary>
                public string psn_no { get; set; }
                /// <summary>
                /// 	人员姓名	 
                /// </summary>
                public string psn_name { get; set; }
                /// <summary>
                /// 	人员证件类型	 
                /// </summary>
                public string psn_cert_type { get; set; }
                /// <summary>
                /// 	证件号码	 
                /// </summary>
                public string certno { get; set; }
                /// <summary>
                /// 	性别	 
                /// </summary>
                public string gend { get; set; }
                /// <summary>
                /// 	民族	 
                /// </summary>
                public string naty { get; set; }
                /// <summary>
                /// 	出生日期	 
                /// </summary>
                public string brdy { get; set; }
                /// <summary>
                /// 	年龄	 
                /// </summary>
                public string age { get; set; }
                /// <summary>
                /// 	险种类型	 
                /// </summary>
                public string insutype { get; set; }
                /// <summary>
                /// 	人员类别	 
                /// </summary>
                public string psn_type { get; set; }
                /// <summary>
                /// 	公务员标志	 
                /// </summary>
                public string cvlserv_flag { get; set; }
                /// <summary>
                /// 	灵活就业标志	 
                /// </summary>
                public string flxempe_flag { get; set; }
                /// <summary>
                /// 	新生儿标志	 
                /// </summary>
                public string nwb_flag { get; set; }
                /// <summary>
                /// 	参保机构医保区划	 
                /// </summary>
                public string insu_optins { get; set; }
                /// <summary>
                /// 	单位名称	 
                /// </summary>
                public string emp_name { get; set; }
                /// <summary>
                /// 	定点医药机构编号	 
                /// </summary>
                public string fixmedins_code { get; set; }
                /// <summary>
                /// 	定点医药机构名称	 
                /// </summary>
                public string fixmedins_name { get; set; }
                /// <summary>
                /// 	医院等级	 
                /// </summary>
                public string hosp_lv { get; set; }
                /// <summary>
                /// 	定点归属机构	 
                /// </summary>
                public string fix_blng_admdvs { get; set; }
                /// <summary>
                /// 	开始日期	 
                /// </summary>
                public string begndate { get; set; }
                /// <summary>
                /// 	结束日期	 
                /// </summary>
                public string enddate { get; set; }
                /// <summary>
                /// 	结算时间	 
                /// </summary>
                public string setl_time { get; set; }
                /// <summary>
                /// 	医疗类别	 
                /// </summary>
                public string med_type { get; set; }
                /// <summary>
                /// 	清算类别	 
                /// </summary>
                public string clr_type { get; set; }
                /// <summary>
                /// 	清算方式	 
                /// </summary>
                public string clr_way { get; set; }
                /// <summary>
                /// 	清算经办机构	 
                /// </summary>
                public string clr_optins { get; set; }
                /// <summary>
                /// 	二级清算类别	 
                /// </summary>
                public string clr_type_lv2 { get; set; }
                /// <summary>
                /// 	医疗费总额	 
                /// </summary>
                public string medfee_sumamt { get; set; }
                /// <summary>
                /// 	全自费金额	 
                /// </summary>
                public string fulamt_ownpay_amt { get; set; }
                /// <summary>
                /// 	超限价自费费用	 
                /// </summary>
                public string overlmt_selfpay { get; set; }
                /// <summary>
                /// 	先行自付金额	 
                /// </summary>
                public string preselfpay_amt { get; set; }
                /// <summary>
                /// 	符合政策范围金额	 
                /// </summary>
                public string inscp_amt { get; set; }
                /// <summary>
                /// 	实际支付起付线	 
                /// </summary>
                public string act_pay_dedc { get; set; }
                /// <summary>
                /// 	基金支付总额	 
                /// </summary>
                public string fund_pay_sumamt { get; set; }
                /// <summary>
                /// 	个人支付金额	 
                /// </summary>
                public string psn_pay { get; set; }
                /// <summary>
                /// 	个人账户支出	 
                /// </summary>
                public string acct_pay { get; set; }
                /// <summary>
                /// 	现金支付金额	 
                /// </summary>
                public string cash_payamt { get; set; }
                /// <summary>
                /// 	余额	 
                /// </summary>
                public string balc { get; set; }
                /// <summary>
                /// 	个人账户共济支付金额	 
                /// </summary>
                public string acct_mulaid_pay { get; set; }
                /// <summary>
                /// 	年度	 
                /// </summary>
                public string year { get; set; }
                /// <summary>
                /// 	病种编码	 
                /// </summary>
                public string dise_no { get; set; }
                /// <summary>
                /// 	病种名称	 
                /// </summary>
                public string dise_name { get; set; }
                /// <summary>
                /// 	医疗机构对账标志	 
                /// </summary>
                public string medins_stmt_flag { get; set; }
                /// <summary>
                /// 	退费结算标志	 
                /// </summary>
                public string refd_setl_flag { get; set; }
                /// <summary>
                /// 	清算标志	 
                /// </summary>
                public string clr_flag { get; set; }
                /// <summary>
                /// 	身份认定行政区划	 
                /// </summary>
                public string ide_admdvs { get; set; }
                /// <summary>
                /// 	支付地点类别	 
                /// </summary>
                public string pay_loc { get; set; }
                /// <summary>
                /// 	发票号	 
                /// </summary>
                public string invono { get; set; }

                /// <summary>
                ///	统筹基金支出	 
                /// <summary>
                public string hifp_pay { get; set; }
                /// <summary>
                ///	公务员医疗补助资金支出	 
                /// <summary>
                public string cvlserv_pay { get; set; }
                /// <summary>
                ///	补充医疗保险基金支出	 
                /// <summary>
                public string hifes_pay { get; set; }
                /// <summary>
                ///	大病补充医疗保险基金支出	 
                /// <summary>
                public string hifmi_pay { get; set; }
                /// <summary>
                ///	大额医疗补助基金支出	 
                /// <summary>
                public string hifob_pay { get; set; }
                /// <summary>
                ///	伤残人员医疗保障基金支出	 
                /// <summary>
                public string hifdm_pay { get; set; }
                /// <summary>
                ///	医疗救助基金支出	 
                /// <summary>
                public string maf_pay { get; set; }
                /// <summary>
                ///	其它基金支付	 
                /// <summary>
                public string othfund_pay { get; set; }

                /// <summary>
                ///	生育备案结束时间 
                /// <summary>
                public string matnEnddate { get; set; }

                /// <summary>
                ///	产检医院变更标志	 
                /// <summary>
                public string birHospChangFlag { get; set; }

                /// <summary>
                ///	生育类型	 
                /// <summary>
                public string birctrl_type { get; set; }

            }

            public List<Setldetail> setldetail { get; set; }
            public class Setldetail
            {
                /// <summary>
                /// 	结算ID	 
                /// </summary>
                public string setl_id { get; set; }
                /// <summary>
                /// 	就诊ID	 
                /// </summary>
                public string mdtrt_id { get; set; }
                /// <summary>
                /// 	基金支付类型	 
                /// </summary>
                public string fund_pay_type { get; set; }
                /// <summary>
                /// 	统筹区基金支付类型	 
                /// </summary>
                public string poolarea_fund_pay_type { get; set; }
                /// <summary>
                /// 	基金支付金额	 
                /// </summary>
                public string fund_payamt { get; set; }

            }
        }
    }
}
