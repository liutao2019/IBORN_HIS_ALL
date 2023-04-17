using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
	public  class ResponseGzsiModel5260 : ResponseBase
    {
		public Output output { get; set; }
		public class Output {
			public Setlinfo setlinfo { get; set; }
			public List<Setldetail> setldetail { get; set; }
			public List<Funddetail> funddetail { get; set; }
			public SetlProcInfo_390 setlProcInfo_390 { get; set; }
			public SetlProcInfo_310_510 setlProcInfo_310_510 { get; set; }
			public HospHelpInfo hospHelpInfo { get; set; }
			public Cumulative cumulative { get; set; }
		}

        public class Setlinfo {
			///<summary>
			///	结算ID	
			///<summary>
			public string setl_id { get; set; }
			///<summary>
			///	就诊ID	
			///<summary>
			public string mdtrt_id { get; set; }
			///<summary>
			///	人员编号	
			///<summary>
			public string psn_no { get; set; }
			///<summary>
			///	人员姓名	
			///<summary>
			public string psn_name { get; set; }
			///<summary>
			///	人员证件类型	
			///<summary>
			public string psn_cert_type { get; set; }
			///<summary>
			///	证件号码	
			///<summary>
			public string certno { get; set; }
			///<summary>
			///	性别	
			///<summary>
			public string gend { get; set; }
			///<summary>
			///	民族	
			///<summary>
			public string naty { get; set; }
			///<summary>
			///	出生日期	
			///<summary>
			public string brdy { get; set; }
			///<summary>
			///	年龄	
			///<summary>
			public string age { get; set; }
			///<summary>
			///	险种类型	
			///<summary>
			public string insutype { get; set; }
			///<summary>
			///	人员类别	
			///<summary>
			public string psn_type { get; set; }
			///<summary>
			///	公务员标志	
			///<summary>
			public string cvlserv_flag { get; set; }
			///<summary>
			///	参保机构医保区划	
			///<summary>
			public string insu_optins { get; set; }
			///<summary>
			///	单位名称	
			///<summary>
			public string emp_name { get; set; }
			///<summary>
			///	支付地点类别	
			///<summary>
			public string pay_loc { get; set; }
			///<summary>
			///	定点医疗机构编号	
			///<summary>
			public string fixmedins_code { get; set; }
			///<summary>
			///	定点医疗机构名称	
			///<summary>
			public string fixmedins_name { get; set; }
			///<summary>
			///	结算时间	
			///<summary>
			public string setl_time { get; set; }
			///<summary>
			///	就诊凭证类型	
			///<summary>
			public string mdtrt_cert_type { get; set; }
			///<summary>
			///	医疗类别	
			///<summary>
			public string med_type { get; set; }
			///<summary>
			///	医疗费总额	
			///<summary>
			public string medfee_sumamt { get; set; }
			///<summary>
			///	全自费金额	
			///<summary>
			public string ownpay_amt { get; set; }
			///<summary>
			///	超限价自费费用	
			///<summary>
			public string overlmt_selfpay { get; set; }
			///<summary>
			///	先行自付金额	
			///<summary>
			public string preselfpay_amt { get; set; }
			///<summary>
			///	符合范围金额	
			///<summary>
			public string inscp_scp_amt { get; set; }
			///<summary>
			///	实际支付起付线	
			///<summary>
			public string act_pay_dedc { get; set; }
			///<summary>
			///	基本医疗保险统筹基金支出	
			///<summary>
			public string hifp_pay { get; set; }
			///<summary>
			///	基本医疗统筹比例自付	
			///<summary>
			public string pool_prop_selfpay { get; set; }
			///<summary>
			///	医保认可费用总额	
			///<summary>
			public string med_sumfee { get; set; }
			///<summary>
			///	公务员医疗补助基金支出	
			///<summary>
			public string cvlserv_pay { get; set; }
			///<summary>
			///	补充医疗保险基金支出	
			///<summary>
			public string hifes_pay { get; set; }
			///<summary>
			///	大病补充医疗保险基金支出	
			///<summary>
			public string hifmi_pay { get; set; }
			///<summary>
			///	大额医疗补助基金支出	
			///<summary>
			public string hifob_pay { get; set; }
			///<summary>
			///	伤残人员医疗保障基金支出	
			///<summary>
			public string hifdm_pay { get; set; }
			///<summary>
			///	医疗救助基金支出	
			///<summary>
			public string maf_pay { get; set; }
			///<summary>
			///	现金支付中医院负担金额	
			///<summary>
			public string ownpay_hosp_part { get; set; }
			///<summary>
			///	其他基金支出	
			///<summary>
			public string oth_pay { get; set; }
			///<summary>
			///	基金支付总额	
			///<summary>
			public string fund_pay_sumamt { get; set; }
			///<summary>
			///	个人支付金额	
			///<summary>
			public string psn_pay { get; set; }
			///<summary>
			///	个人账户支出	
			///<summary>
			public string acct_pay { get; set; }
			///<summary>
			///	现金支付金额	
			///<summary>
			public string cash_payamt { get; set; }
			///<summary>
			///	年度	
			///<summary>
			public string year { get; set; }
			///<summary>
			///	清算经办机构	
			///<summary>
			public string clr_optins { get; set; }
			///<summary>
			///	清算类别	
			///<summary>
			public string clr_type { get; set; }
			///<summary>
			///	住院/门诊号	
			///<summary>
			public string ipt_op_no { get; set; }
			///<summary>
			///	门诊诊断信息	
			///<summary>
			public string op_dise_info { get; set; }
			///<summary>
			///	病种编码	
			///<summary>
			public string dise_no { get; set; }
			///<summary>
			///	病种名称	
			///<summary>
			public string dise_name { get; set; }
			///<summary>
			///	发票号	
			///<summary>
			public string invono { get; set; }
			///<summary>
			///	手术操作代码	
			///<summary>
			public string oprn_oprt_code { get; set; }
			///<summary>
			///	手术操作名称	
			///<summary>
			public string oprn_oprt_name { get; set; }
			///<summary>
			///	生育类别	
			///<summary>
			public string matn_type { get; set; }
			///<summary>
			///	计划生育手术类别	
			///<summary>
			public string birctrl_type { get; set; }
			///<summary>
			///	入院诊断描述	
			///<summary>
			public string adm_dise_dscr { get; set; }
			///<summary>
			///	入院科室编码	
			///<summary>
			public string adm_dept_code { get; set; }
			///<summary>
			///	入院科室名称	
			///<summary>
			public string adm_dept_name { get; set; }
			///<summary>
			///	入院床位	
			///<summary>
			public string adm_bed { get; set; }
			///<summary>
			///	住院主诊断代码	
			///<summary>
			public string dscg_dise_code { get; set; }
			///<summary>
			///	住院主诊断名称	
			///<summary>
			public string dscg_dise_name { get; set; }
			///<summary>
			///	主要病情描述	
			///<summary>
			public string main_cond_desc { get; set; }
			///<summary>
			///	住院天数	
			///<summary>
			public string ipt_days { get; set; }
			///<summary>
			///	计划生育服务证号	
			///<summary>
			public string fpsc_no { get; set; }
			///<summary>
			///	晚育标志	
			///<summary>
			public string latechb_flag { get; set; }
			///<summary>
			///	孕周数	
			///<summary>
			public string geso_val { get; set; }
			///<summary>
			///	胎次	
			///<summary>
			public string fetts { get; set; }
			///<summary>
			///	胎儿数	
			///<summary>
			public string fetus_cnt { get; set; }
			///<summary>
			///	早产标志	
			///<summary>
			public string premature_flag { get; set; }
			///<summary>
			///	经办人	
			///<summary>
			public string opter { get; set; }
			///<summary>
			///	经办人姓名	
			///<summary>
			public string opter_name { get; set; }
			///<summary>
			///	经办时间	
			///<summary>
			public string opt_time { get; set; }
			///<summary>
			///	计划生育手术或生育日期	
			///<summary>
			public string birctrl_or_matn_time { get; set; }
			///<summary>
			///	医院级别	
			///<summary>
			public string hosp_live { get; set; }
			///<summary>
			///	开始时间	
			///<summary>
			public string begintime { get; set; }
			///<summary>
			///	结束时间	
			///<summary>
			public string endtime { get; set; }
			///<summary>
			///	零报扣减费用	
			///<summary>
			public string det_amt { get; set; }

		}
		public class Setldetail {
			///<summary>
			///	医疗收费项目类别	
			///<summary>
			public string med_chrgitm_type { get; set; }
			///<summary>
			///	项目总金额	
			///<summary>
			public string item_sumamt { get; set; }
			///<summary>
			///	项目甲类金额	
			///<summary>
			public string item_claa_amt { get; set; }
			///<summary>
			///	项目乙类金额	
			///<summary>
			public string item_clab_amt { get; set; }
			///<summary>
			///	项目自费金额	
			///<summary>
			public string item_ownpay_amt { get; set; }
			///<summary>
			///	项目其他金额	
			///<summary>
			public string item_oth_amt { get; set; }

			public string overlmt_selfpay { get; set; }
		}
		public class Funddetail {
			///<summary>
			///	结算ID	
			///<summary>
			public string setl_id { get; set; }
			///<summary>
			///	就诊ID	
			///<summary>
			public string mdtrt_id { get; set; }
			///<summary>
			///	基金支付类型	
			///<summary>
			public string fund_pay_type { get; set; }
			///<summary>
			///	符合范围金额	
			///<summary>
			public string inscp_scp_amt { get; set; }
			///<summary>
			///	本次可支付限额金额	
			///<summary>
			public string crt_payb_lmt_amt { get; set; }
			///<summary>
			///	基金支付金额	
			///<summary>
			public string fund_payamt { get; set; }
			///<summary>
			///	结算过程信息	
			///<summary>
			public string setl_proc_info { get; set; }
		}
		public class SetlProcInfo_390
		{
			///<summary>
			///	床位费个人现金支付	
			///<summary>
			public string bed_pay_cach { get; set; }
			///<summary>
			///	自费费用个人现金支付	
			///<summary>
			public string part_pay_cach { get; set; }
			///<summary>
			///	起付标准以下费用 个人现金支付	
			///<summary>
			public string qfbzx_pay_cach { get; set; }
			///<summary>
			///	基本医疗共付段 个人现金支付	
			///<summary>
			public string jbylgf_pay_cash { get; set; }
			///<summary>
			///	超限额以上费用(城乡大病支付) 个人现金支付	
			///<summary>
			public string over_limit_cach_390 { get; set; }
			///<summary>
			///	超限额以上费用 个人现金支付	
			///<summary>
			public string over_limit_cach { get; set; }
			///<summary>
			///	基本医疗共付段 基本医疗保险	
			///<summary>
			public string jbylgf_basic_pay { get; set; }
			///<summary>
			///	基本医疗共付段 基本药物补助支付	
			///<summary>
			public string jbylgf_med_pay { get; set; }
			///<summary>
			///	超限额以上费用(城乡大病支付)  城乡大病Ⅱ	
			///<summary>
			public string over_limit_sup_pay_390 { get; set; }
			///<summary>
			///	现金支付合计	
			///<summary>
			public string sum_cach { get; set; }
			///<summary>
			///	基本医疗保险合计	
			///<summary>
			public string sum_jbyl { get; set; }
			///<summary>
			///	基本药物补助合计	
			///<summary>
			public string sum_med { get; set; }
			///<summary>
			///	城乡大病Ⅱ合计	
			///<summary>
			public string sum_sup { get; set; }
			///<summary>
			///	床位费合计	
			///<summary>
			public string sum_bed_pay { get; set; }
			///<summary>
			///	自费费用合计	
			///<summary>
			public string sum_own_pay { get; set; }
			///<summary>
			///	部分项目自付费用合计	
			///<summary>
			public string sum_part_pay { get; set; }
			///<summary>
			///	基本医疗共付段合计	
			///<summary>
			public string sum_jbylgf_pay { get; set; }
			///<summary>
			///	重大疾病共付段合计	
			///<summary>
			public string sum_major_pay { get; set; }
			///<summary>
			///	超限额以上费用合计	
			///<summary>
			public string sum_over_limit { get; set; }
			///<summary>
			///	超限额以上费用 (城乡大病支付)合计	
			///<summary>
			public string sum_over_limit_390 { get; set; }
			///<summary>
			///	总计	
			///<summary>
			public string sum_all_pay { get; set; }

		}
		public class SetlProcInfo_310_510
		{
			///<summary>
			///	床位费个人现金支付	
			///<summary>
			public string bed_pay_cach { get; set; }
			///<summary>
			///	自费费用个人现金支付	
			///<summary>
			public string own_pay_cach { get; set; }
			///<summary>
			///	部分项目自付费用 个人现金支付	
			///<summary>
			public string part_pay_cach { get; set; }
			///<summary>
			///	起付标准以下费用 个人现金支付	
			///<summary>
			public string qfbzx_pay_cach { get; set; }
			///<summary>
			///	基本医疗共付段 个人现金支付	
			///<summary>
			public string jbylgf_pay_cash { get; set; }
			///<summary>
			///	重大疾病共付段 个人现金支付	
			///<summary>
			public string over_limit_cach { get; set; }
			///<summary>
			///	基本医疗共付段 基本医疗保险支付	
			///<summary>
			public string jbylgf_basic_pay { get; set; }
			///<summary>
			///	基本医疗共付段 基本药物补助支付	
			///<summary>
			public string jbylgf_med_pay { get; set; }
			///<summary>
			///	重大疾病共付段 基本医疗保险支付	
			///<summary>
			public string major_basic_pay { get; set; }
			///<summary>
			///	现金支付合计	
			///<summary>
			public string sum_cach { get; set; }
			///<summary>
			///	基本医疗保险合计	
			///<summary>
			public string sum_jbyl { get; set; }
			///<summary>
			///	基本药物补助合计	
			///<summary>
			public string sum_med { get; set; }
			///<summary>
			///	床位费合计	
			///<summary>
			public string sum_bed_pay { get; set; }
			///<summary>
			///	自费费用合计	
			///<summary>
			public string sum_own_pay { get; set; }
			///<summary>
			///	部分项目自付费用合计	
			///<summary>
			public string sum_part_pay { get; set; }
			///<summary>
			///	起付标准以下费用合计	
			///<summary>
			public string sum_qfbzx_pay { get; set; }
			///<summary>
			///	基本医疗共付段合计	
			///<summary>
			public string sum_jbylgf_pay { get; set; }
			///<summary>
			///	重大疾病共付段合计	
			///<summary>
			public string sum_major_pay { get; set; }
			///<summary>
			///	超限额以上费用合计	
			///<summary>
			public string sum_over_limit { get; set; }
			///<summary>
			///	 总计	
			///<summary>
			public string sum_all_pay { get; set; }

		}
		public class HospHelpInfo
		{
			///<summary>
			///	医疗救助金额（含商业保险救助金额）	
			///<summary>
			public string hosp_help_pay { get; set; }
			///<summary>
			///	救助后个人现金支付	
			///<summary>
			public string help_cach { get; set; }
		}
		public class Cumulative
		{
			///<summary>
			///	本年度医疗救助累计	
			///<summary>
			public string now_year_cumulative { get; set; }
			///<summary>
			///	本年度累计 职工基本医疗保险已支付	
			///<summary>
			public string insutype310_pay { get; set; }
			///<summary>
			///	本年度累计 重大疾病医疗补助基金已支付	
			///<summary>
			public string fund_major_pay { get; set; }
			///<summary>
			///	本年度累计 补充医疗保险基金已支付	
			///<summary>
			public string insutype370_pay { get; set; }
			///<summary>
			///	本年度累计 个人自付医疗费用（职工）	
			///<summary>
			public string psn_hops310_pay { get; set; }
			///<summary>
			///	职工医保基金已累计支付当月普通门诊医疗费用	
			///<summary>
			public string mz_current_month_pay { get; set; }
			///<summary>
			///	职工医保基金已累计支付当季度本病种医疗费用	
			///<summary>
			public string zgybjj_current_season { get; set; }
			///<summary>
			///	城乡居民医保基金已累计支付当季度本病种医疗费用	
			///<summary>
			public string jmybjj_current_season { get; set; }
			///<summary>
			///	年度累计 城乡居民基本医疗保险基金已支付	
			///<summary>
			public string insutype390_pay { get; set; }
			///<summary>
			///	本年度累计 城乡大病基金已支付	
			///<summary>
			public string insutype392_pay { get; set; }
			///<summary>
			///	本年度累计 城乡个人自付医疗费用	
			///<summary>
			public string psn_hops390_pay { get; set; }
			///<summary>
			///	城乡居民医保基金已累计支付本年度普通门诊医疗费用	
			///<summary>
			public string mz_type_pay { get; set; }
			///<summary>
			///	是否享受医疗救助待遇	
			///<summary>
			public string med_assis_treat { get; set; }
			///<summary>
			///	社会医疗救助待遇类别	
			///<summary>
			public string soc_med_assis_treat { get; set; }
			///<summary>
			///	商业保险医疗救助金额	
			///<summary>
			public string med_assis_amount_insur_cnt { get; set; }
			///<summary>
			///	本年度商业保险医疗救助累计金额	
			///<summary>
			public string accu_amt_med_year { get; set; }
			///<summary>
			///	个人所得税大病医疗专项附加扣除费用	
			///<summary>
			public string spe_add_dedu_tax { get; set; }

		}

	}
}
