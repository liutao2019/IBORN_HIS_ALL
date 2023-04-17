using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4205
    {
        #region 自费病人门诊就诊信息(节点标识：mdtrtinfo)
        public Mdtrtinfo mdtrtinfo { get; set; }
        public class Mdtrtinfo
        {
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
            /// 	主要病情描述	
            /// </summary>
            public string main_cond_dscr { get; set; }
            /// <summary>
            /// 	病种编码	
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary>
            /// 	病种名称	
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 	计划生育手 术类别	
            /// </summary>
            public string birctrl_type { get; set; }
            /// <summary>
            /// 	计划生育手术或生育日期	
            /// </summary>
            public string birctrl_matn_date { get; set; }
            /// <summary>
            /// 	生育类别	
            /// </summary>
            public string matn_type { get; set; }
            /// <summary>
            /// 	孕周数	
            /// </summary>
            public string geso_val { get; set; }
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
            public string elec_bill_chkcode { get; set; }
            /// <summary>
            /// 	字段扩展	
            /// </summary>
            public string exp_content { get; set; }

        }
        #endregion

        #region 自费病人门诊诊断信息(节点标识：diseinfo)
        public List<Diseinfo> diseinfo { get; set; }
        public class Diseinfo
        {
            /// <summary>
            /// 	诊断类别	
            /// </summary>
            public string diag_type { get; set; }
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
            /// 	诊断医生编码	
            /// </summary>
            public string diag_dr_code { get; set; }
            /// <summary>
            /// 	诊断医生姓名	
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

        #region 自费病人门诊费用明细信息(节点标识：feedetail)
        public List<Feedetail> feedetail { get; set; }
        public class Feedetail
        {
            /// <summary>
            /// 	医药机构就诊ID	
            /// </summary>
            public string fixmedins_mdtrt_id { get; set; }
            /// <summary>
            /// 	医疗类别	
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 	记账流水号	
            /// </summary>
            public string bkkp_sn { get; set; }
            /// <summary>
            /// 	费用发生时间	
            /// </summary>
            public string fee_ocur_time { get; set; }
            /// <summary>
            /// 	定点医药机构编号	
            /// </summary>
            public string fixmedins_code { get; set; }
            /// <summary>
            /// 	定点医药机构名称	
            /// </summary>
            public string fixmedins_name { get; set; }
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
            /// 	医药机构目 录编码	
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
            public string bilg_dr_code { get; set; }
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
            public string acord_dr_code { get; set; }
            /// <summary>
            /// 	受单医生姓名	
            /// </summary>
            public string acord_dr_name { get; set; }
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
            /// 	单次剂量描 述	
            /// </summary>
            public string sin_dos_dscr { get; set; }
            /// <summary>
            /// 	使用频次描述	
            /// </summary>
            public string used_frqu_dscr { get; set; }
            /// <summary>
            /// 	周期天数	
            /// </summary>
            public string prd_days { get; set; }
            /// <summary>
            /// 	用药途径描述	
            /// </summary>
            public string medc_way_dscr { get; set; }
            /// <summary>
            /// 	备注	
            /// </summary>
            public Memo memo { get; set; }
            /// <summary>
            /// 	全自费金额	
            /// </summary>
            public string fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 	超限价金额	
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
            /// 	处方号	
            /// </summary>
            public string rxno { get; set; }

        }

        public class Memo
        {
            /// <summary>
            /// 	医院审批标志	
            /// </summary>
            public string hosp_appr_flag { get; set; }

            /// <summary>
            /// 	发票号	
            /// </summary>
            public string invoice_no { get; set; }

            /// <summary>
            /// 	备注	
            /// </summary>
            public string memo { get; set; }
        }
        #endregion
    }
}
