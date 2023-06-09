﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 门诊费用明细信息上传
    /// </summary>
    public class RequestGzsiModel2204
    {
        public List<Feedetail> feedetail { get; set; }

        public class Feedetail
        {
            /// <summary> 
            /// 费用明细流水号 Y 
            /// </summary>
            public string feedetl_sn { get; set; }
            /// <summary> 
            /// 人员编号 Y 
            /// </summary>
            public string psn_no { get; set; }
            /// <summary> 
            /// 就诊ID Y 
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary> 
            /// 收费批次号  
            /// </summary>
            public string chrg_bchno { get; set; }
            /// <summary> 
            /// 病种编号  
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary> 
            /// 处方号  
            /// </summary>
            public string rxno { get; set; }
            /// <summary> 
            /// 外购处方标志 Y 
            /// </summary>
            public string rx_circ_flag { get; set; }
            /// <summary> 
            /// 费用发生日期 Y 
            /// </summary>
            public string fee_ocur_time { get; set; }
            /// <summary> 
            /// 数量 Y 
            /// </summary>
            public string cnt { get; set; }
            /// <summary> 
            /// 单价 Y 
            /// </summary>
            public string pric { get; set; }
            /// <summary> 
            /// 明细项目费用总额 Y 
            /// </summary>
            public string det_item_fee_sumamt { get; set; }
            /// <summary>
            /// 单次剂量描述  
            /// </summary>
            public string sin_dos_dscr { get; set; }
            /// <summary> 
            /// 使用频次描述  
            /// </summary>
            public string used_frqu_dscr { get; set; }
            /// <summary>
            /// 用药周期天数  
            /// </summary>
            public string prd_days { get; set; }
            /// <summary> 
            /// 用药途径描述  
            /// </summary>
            public string medc_way_dscr { get; set; }
            /// <summary> 
            /// 医疗目录编码 Y 
            /// </summary>
            public string med_list_codg { get; set; }
            /// <summary> 
            /// 医疗机构目录编码 Y 
            /// </summary>
            public string medins_list_codg { get; set; }
            /// <summary> 
            /// 医疗机构目录名称 Y 
            /// </summary>
            public string medins_list_name { get; set; }
            /// <summary> 
            /// 开单科室编码 Y 
            /// </summary>
            public string bilg_dept_codg { get; set; }
            /// <summary> 
            /// 开单科室名称 Y 
            /// </summary>
            public string bilg_dept_name { get; set; }
            /// <summary> 
            /// 开单医生编码 Y 
            /// </summary>
            public string bilg_dr_codg { get; set; }
            /// <summary> 
            /// 开单医生姓名 Y 
            /// </summary>
            public string bilg_dr_name { get; set; }
            /// <summary> 
            /// 受单科室编码  
            /// </summary>
            public string acord_dept_codg { get; set; }
            /// <summary> 
            /// 受单科室名称  
            /// </summary>
            public string acord_dept_name { get; set; }
            /// <summary> 
            /// 受单医生编码  
            /// </summary>
            public string orders_dr_code { get; set; }
            /// <summary> 
            /// 受单医生姓名  
            /// </summary>
            public string orders_dr_name { get; set; }
            /// <summary> 
            /// 医院审批标志  
            /// </summary>
            public string hosp_appr_flag { get; set; }
            /// <summary> 
            /// 中药使用方式  
            /// </summary>
            public string tcmdrug_used_way { get; set; }
            /// <summary> 
            /// 外检标志  
            /// </summary>
            public string etip_flag { get; set; }
            /// <summary> 
            /// 外检医院编码  
            /// </summary>
            public string etip_hosp_code { get; set; }
            /// <summary> 
            /// 出院带药标志  
            /// </summary>
            public string dscg_tkdrug_flag { get; set; }
            /// <summary> 
            /// 生育费用标志  
            /// </summary>
            public string matn_fee_flag { get; set; }
            /// <summary> 
            /// 不进行审核标志  
            /// </summary>
            public string unchk_flag { get; set; }
            /// <summary> 
            /// 不进行审核说明  
            /// </summary>
            public string unchk_memo { get; set; }
        }
    }
}
