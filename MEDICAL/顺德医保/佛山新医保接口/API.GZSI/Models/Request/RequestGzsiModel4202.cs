using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4202
    {
        #region 自费病人就诊信息(节点标识：ownPayPatnMdtrtD)
        public OwnPayPatnMdtrtD ownPayPatnMdtrtD { get; set; }
        public class OwnPayPatnMdtrtD {
            /// <summary>
            /// 	医药机构就诊ID	
            /// </summary>
            public string fixmedins_mdtrt_id { get; set; }
            /// <summary>
            /// 	定点医药机构编号	
            /// </summary>
            public string fixmedins_code { get; set; }
            /// <summary>
            /// 	定点医药机构名称	
            /// </summary>
            public string fixmedins_name { get; set; }
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
            /// 	住院/门诊号	
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 	病历号	
            /// </summary>
            public string medrcdno { get; set; }
            /// <summary>
            /// 	主诊医师代码	
            /// </summary>
            public string chfpdr_code { get; set; }
            /// <summary>
            /// 	入院诊断描述	
            /// </summary>
            public string adm_diag_dscr { get; set; }
            /// <summary>
            /// 	入院科室编码	
            /// </summary>
            public string adm_dept_codg { get; set; }
            /// <summary>
            /// 	入院科室名称	
            /// </summary>
            public string adm_dept_name { get; set; }
            /// <summary>
            /// 	入院床位	
            /// </summary>
            public string adm_bed { get; set; }
            /// <summary>
            /// 	病区床位	
            /// </summary>
            public string wardarea_bed { get; set; }
            /// <summary>
            /// 	转科室标志	
            /// </summary>
            public string traf_dept_flag { get; set; }
            /// <summary>
            /// 	出院主诊断代码	
            /// </summary>
            public string dscg_maindiag_code { get; set; }
            /// <summary>
            /// 	出院科室编码	
            /// </summary>
            public string dscg_dept_codg { get; set; }
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
            public string main_cond_dscr { get; set; }
            /// <summary>
            /// 	病种编号	
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
            public string otp_diag_info { get; set; }
            /// <summary>
            /// 	在院状态	
            /// </summary>
            public string inhosp_stas { get; set; }
            /// <summary>
            /// 	死亡日期	
            /// </summary>
            public string die_date { get; set; }
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
            /// 	计划生育手 术类别	
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
            public string pret_flag { get; set; }
            /// <summary>
            /// 	妊娠时间	
            /// </summary>
            public string prey_time { get; set; }
            /// <summary>
            /// 	计划生育手术或生育日期	
            /// </summary>
            public string birctrl_matn_date { get; set; }
            /// <summary>
            /// 	伴有并发症标志	
            /// </summary>
            public string cop_flag { get; set; }
            /// <summary>
            /// 	有效标志	
            /// </summary>
            public string vali_flag { get; set; }
            /// <summary>
            /// 	备注	
            /// </summary>
            public string memo { get; set; }
            /// <summary>
            /// 	经办人ID	
            /// </summary>
            public string opter_id { get; set; }
            /// <summary>
            /// 	经办人姓名	
            /// </summary>
            public string opter_name { get; set; }
            /// <summary>
            /// 	经办时间	
            /// </summary>
            public string opt_time { get; set; }
            /// <summary>
            /// 	主诊医师姓名	
            /// </summary>
            public string chfpdr_name { get; set; }
            /// <summary>
            /// 	住院主诊断名称	
            /// </summary>
            public string dscg_maindiag_name { get; set; }
            /// <summary>
            /// 	医疗总费用	
            /// </summary>
            public string medfee_sumamt { get; set; }
            /// <summary>
            /// 	电子票据代码	
            /// </summary>
            public string elec_bill_code { get; set; }
            /// <summary>
            /// 	电子票据号码	
            /// </summary>
            public string elec_billno_code { get; set; }
            /// <summary>
            /// 	电子票据校验码	
            /// </summary>
            public string elec_bil_chkcode { get; set; }
            /// <summary>
            /// 	扩展字段	
            /// </summary>
            public string exp_content { get; set; }

        }
        #endregion

        #region 表自费病人诊断信息(节点标识：ownPayPatnDiagListD)
        public List<OwnPayPatnDiagListD> ownPayPatnDiagListD { get; set; }
        public class OwnPayPatnDiagListD {
            /// <summary>
            /// 	出入院诊断类别	
            /// </summary>
            public string inout_diag_type { get; set; }
            /// <summary>
            /// 	诊断类别	
            /// </summary>
            public string diag_type { get; set; }
            /// <summary>
            /// 	主诊断标志	
            /// </summary>
            public string maindiag_flag { get; set; }
            /// <summary>
            /// 	诊断排序号	
            /// </summary>
            public string diag_srt_no { get; set; }
            /// <summary>
            /// 	诊断代码	
            /// </summary>
            public string diag_code { get; set; }
            /// <summary>
            /// 	诊断名称	
            /// </summary>
            public string diag_name { get; set; }
            /// <summary>
            /// 	诊断科室	
            /// </summary>
            public string diag_dept { get; set; }
            /// <summary>
            /// 	诊断医师代码	
            /// </summary>
            public string diag_dr_code { get; set; }
            /// <summary>
            /// 	诊断医师姓名	
            /// </summary>
            public string diag_dr_name { get; set; }
            /// <summary>
            /// 	诊断时间	
            /// </summary>
            public string diag_time { get; set; }
            /// <summary>
            /// 	有效标志	
            /// </summary>
            public string vali_flag { get; set; }

        }
        #endregion
    }
}
