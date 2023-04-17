using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4201
    {

     
            /// <summary>
            /// 结算信息
            /// </summary>
            public List<Feedetail> feedetail { get; set; }

            public class Feedetail
			{  /// <summary>
			   /// 	就医流水号	 
			   /// </summary>
				public string mdtrt_sn { get; set; }
				/// <summary>
				/// 	住院/门诊号	 
				/// </summary>
				public string ipt_otp_no { get; set; }
				/// <summary>
				/// 	医疗类别	 
				/// </summary>
				public string med_type { get; set; }
				/// <summary>
				/// 	收费批次号	 
				/// </summary>
				public string chrg_bchno { get; set; }
				/// <summary>
				/// 	费用明细流水号	 
				/// </summary>
				public string feedetl_sn { get; set; }
				/// <summary>
				/// 	人员证件类型	 
				/// </summary>
				public string psn_cert_type { get; set; }
				/// <summary>
				/// 	证件号码	 
				/// </summary>
				public string certno { get; set; }
				/// <summary>
				/// 	人员姓名	 
				/// </summary>
				public string psn_name { get; set; }
				/// <summary>
				/// 	费用发生时间	 
				/// </summary>
				public string fee_ocur_time { get; set; }
				/// <summary>
				/// 	数量	 
				/// </summary>
				public string cnt { get; set; }
				/// <summary>
				/// 	单价	 
				/// </summary>
				public string pric { get; set; }
				/// <summary>
				/// 	明细项目费用总额	 
				/// </summary>
				public string det_item_fee_sumamt { get; set; }
				/// <summary>
				/// 	医疗目录编码	 
				/// </summary>
				public string med_list_codg { get; set; }
				/// <summary>
				/// 	医药机构目录编码	 
				/// </summary>
				public string medins_list_codg { get; set; }
				/// <summary>
				/// 	医药机构目录名称	 
				/// </summary>
				public string medins_list_name { get; set; }
				/// <summary>
				/// 	医疗收费项目类别	 
				/// </summary>
				public string med_chrgitm_type { get; set; }
				/// <summary>
				/// 	商品名	 
				/// </summary>
				public string prodname { get; set; }
				/// <summary>
				/// 	开单科室编码	 
				/// </summary>
				public string bilg_dept_codg { get; set; }
				/// <summary>
				/// 	开单科室名称	 
				/// </summary>
				public string bilg_dept_name { get; set; }
				/// <summary>
				/// 	开单医生编码	 
				/// </summary>
				public string bilg_dr_codg { get; set; }
				/// <summary>
				/// 	开单医师姓名	 
				/// </summary>
				public string bilg_dr_name { get; set; }
				/// <summary>
				/// 	受单科室编码	 
				/// </summary>
				public string acord_dept_codg { get; set; }
				/// <summary>
				/// 	受单科室名称	 
				/// </summary>
				public string acord_dept_name { get; set; }
				/// <summary>
				/// 	受单医生编码	 
				/// </summary>
				public string orders_dr_code { get; set; }
				/// <summary>
				/// 	受单医生姓名	 
				/// </summary>
				public string orders_dr_name { get; set; }
				/// <summary>
				/// 	中药使用方式	 
				/// </summary>
				public string tcmdrug_used_way { get; set; }
				/// <summary>
				/// 	外检标志	 
				/// </summary>
				public string etip_flag { get; set; }
				/// <summary>
				/// 	外检医院编码	 
				/// </summary>
				public string etip_hosp_code { get; set; }
				/// <summary>
				/// 	出院带药标志	 
				/// </summary>
				public string dscg_tkdrug_flag { get; set; }
				/// <summary>
				/// 	备注	 
				/// </summary>
				public string memo { get; set; }
			}

			/// <summary>
			/// 结算信息
			/// </summary>
			public Mdtrtinfo mdtrtinfo { get; set; }

            public class Mdtrtinfo
			{   /// <summary>
				/// 	就诊ID	 
				/// </summary>
				public string mdtrt_id { get; set; }
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
				/// 	联系人姓名	 
				/// </summary>
				public string coner_name { get; set; }
				/// <summary>
				/// 	联系电话	 
				/// </summary>
				public string tel { get; set; }
				/// <summary>
				/// 	联系地址	 
				/// </summary>
				public string addr { get; set; }
				/// <summary>
				/// 	开始时间	 
				/// </summary>
				public string begntime { get; set; }
				/// <summary>
				/// 	结束时间	 
				/// </summary>
				public string endtime { get; set; }
				/// <summary>
				/// 	医疗类别	 
				/// </summary>
				public string med_type { get; set; }
				/// <summary>
				/// 	病历号	 
				/// </summary>
				public string medrcdno { get; set; }
				/// <summary>
				/// 	主治医生编码	 
				/// </summary>
				public string atddr_no { get; set; }
				/// <summary>
				/// 	主治医师姓名	 
				/// </summary>
				public string atddr_name { get; set; }
				/// <summary>
				/// 	入院诊断描述	 
				/// </summary>
				public string adm_dise_dscr { get; set; }
				/// <summary>
				/// 	入院科室编码	 
				/// </summary>
				public string adm_dept_code { get; set; }
				/// <summary>
				/// 	入院科室名称	 
				/// </summary>
				public string adm_dept_name { get; set; }
				/// <summary>
				/// 	入院床位	 
				/// </summary>
				public string adm_bed { get; set; }
				/// <summary>
				/// 	主诊断代码	 
				/// </summary>
				public string dscg_dise_code { get; set; }
				/// <summary>
				/// 	主诊断名称	 
				/// </summary>
				public string dscg_dise_name { get; set; }
				/// <summary>
				/// 	出院科室编码	 
				/// </summary>
				public string dscg_dept_code { get; set; }
				/// <summary>
				/// 	出院科室名称	 
				/// </summary>
				public string dscg_dept_name { get; set; }
				/// <summary>
				/// 	出院床位	 
				/// </summary>
				public string dscg_bed { get; set; }
				/// <summary>
				/// 	离院方式	 
				/// </summary>
				public string dscg_way { get; set; }
				/// <summary>
				/// 	主要病情描述	 
				/// </summary>
				public string main_cond_desc { get; set; }
				/// <summary>
				/// 	病种编码	 
				/// </summary>
				public string dise_no { get; set; }
				/// <summary>
				/// 	病种名称	 
				/// </summary>
				public string dise_name { get; set; }
				/// <summary>
				/// 	手术操作代码	 
				/// </summary>
				public string oprn_oprt_code { get; set; }
				/// <summary>
				/// 	手术操作名称	 
				/// </summary>
				public string oprn_oprt_name { get; set; }
				/// <summary>
				/// 	门诊诊断信息	 
				/// </summary>
				public string op_dise_info { get; set; }
				/// <summary>
				/// 	在院状态	 
				/// </summary>
				public string inhosp_stas { get; set; }
				/// <summary>
				/// 	死亡日期	 
				/// </summary>
				public string die_ { get; set; }
				/// <summary>
				/// 	住院天数	 
				/// </summary>
				public string ipt_days { get; set; }
				/// <summary>
				/// 	计划生育服务证号	 
				/// </summary>
				public string fpsc_no { get; set; }
				/// <summary>
				/// 	生育类别	 
				/// </summary>
				public string matn_type { get; set; }
				/// <summary>
				/// 	计划生育手术类别	 
				/// </summary>
				public string birctrl_type { get; set; }
				/// <summary>
				/// 	晚育标志	 
				/// </summary>
				public string latechb_flag { get; set; }
				/// <summary>
				/// 	孕周数	 
				/// </summary>
				public string geso_val { get; set; }
				/// <summary>
				/// 	胎次	 
				/// </summary>
				public string fetts { get; set; }
				/// <summary>
				/// 	胎儿数	 
				/// </summary>
				public string fetus_cnt { get; set; }
				/// <summary>
				/// 	早产标志	 
				/// </summary>
				public string premature_flag { get; set; }
				/// <summary>
				/// 	计划生育手术或生育日期	 
				/// </summary>
				public string birctrl_or_matn_time { get; set; }
				/// <summary>
				/// 	伴有并发症标志	 
				/// </summary>
				public string cmplct_flag { get; set; }
				/// <summary>
				/// 	有效标志	 
				/// </summary>
				public string vali_flag { get; set; }
			}

			/// <summary>
			/// 结算信息
			/// </summary>
			public List<Diseinfo> diseinfo { get; set; }

            public class Diseinfo
			{   /// <summary>
				/// 	就诊ID	 
				/// </summary>
				public string mdtrt_id { get; set; }
				/// <summary>
				/// 	出入院诊断类别	 
				/// </summary>
				public string inout_dise_type { get; set; }
				/// <summary>
				/// 	诊断类别	 
				/// </summary>
				public string dise_type { get; set; }
				/// <summary>
				/// 	主诊断标志	 
				/// </summary>
				public string maindise_flag { get; set; }
				/// <summary>
				/// 	诊断排序号	 
				/// </summary>
				public string dise_srt_no { get; set; }
				/// <summary>
				/// 	诊断代码	 
				/// </summary>
				public string dise_code { get; set; }
				/// <summary>
				/// 	诊断名称	 
				/// </summary>
				public string dise_name { get; set; }
				/// <summary>
				/// 	入院病情	 
				/// </summary>
				public string adm_cond { get; set; }
				/// <summary>
				/// 	诊断科室	 
				/// </summary>
				public string dise_dept { get; set; }
				/// <summary>
				/// 	诊断医生编码	 
				/// </summary>
				public string dise_dor_no { get; set; }
				/// <summary>
				/// 	诊断医生姓名	 
				/// </summary>
				public string dise_dor_name { get; set; }
				/// <summary>
				/// 	诊断时间	 
				/// </summary>
				public string dise_time { get; set; }
			}

		}

}
