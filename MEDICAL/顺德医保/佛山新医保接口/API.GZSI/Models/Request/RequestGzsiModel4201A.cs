using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4201A
    {
        #region 明细信息(节点标识：fsiOwnpayPatnFeeListDDTO)
        public List<FsiOwnpayPatnFeeListDDTO> fsiOwnpayPatnFeeListDDTO { get; set; }
        public class FsiOwnpayPatnFeeListDDTO
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
            /// 	单次剂量描述	
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
            /// 	超限价自费金额	
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
