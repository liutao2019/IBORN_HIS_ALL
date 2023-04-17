﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Inpatient
{
    /// <summary>
    /// 广州医保住院费用明细
    /// </summary>
    public class Feedetail
    {
        public List<FeedetailRow> rows { get; set; }
    }
    public class FeedetailRow
    {
        #region 费用明细信息feedetail


        /// <summary>
        /// 费用明细流水号 Y 单次就诊内唯一
        /// <summary>
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 原费用流水号  退单时传入被退单的费用明细流水号
        /// <summary>
        public string init_feedetl_sn { get; set; }

        /// <summary>
        /// 就诊ID Y 
        /// <summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 医嘱号  
        /// <summary>
        public string drord_no { get; set; }

        /// <summary>
        /// 人员编号 Y 
        /// <summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 医疗类别 Y 见【4码表说明】
        /// <summary>
        public string med_type { get; set; }

        /// <summary>
        /// 费用发生时间 Y 格式：yyyy-MM-dd HH:mm:ss
        /// <summary>
        public string fee_ocur_time { get; set; }

        /// <summary>
        /// 数量 Y 退单时数量填写负数
        /// <summary>
        public string cnt { get; set; }

        /// <summary>
        /// 单价 Y 
        /// <summary>
        public string pric { get; set; }

        /// <summary>
        /// 明细项目费用总额 Y 
        /// <summary>
        public string det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 医疗目录编码 Y 
        /// <summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 医药机构目录编码 Y 
        /// <summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 医药机构目录名称 Y 
        /// <summary>
        public string medins_list_name { get; set; }

        /// <summary>
        /// 开单科室编码 Y 
        /// <summary>
        public string bilg_dept_codg { get; set; }

        /// <summary>
        /// 开单科室名称 Y 
        /// <summary>
        public string bilg_dept_name { get; set; }

        /// <summary>
        /// 开单医生编码 Y 按照标准编码填写
        /// <summary>
        public string bilg_dr_codg { get; set; }

        /// <summary>
        /// 开单医生姓名 Y 
        /// <summary>
        public string bilg_dr_name { get; set; }

        /// <summary>
        /// 受单科室编码  
        /// <summary>
        public string acord_dept_codg { get; set; }

        /// <summary>
        /// 受单科室名称  
        /// <summary>
        public string acord_dept_name { get; set; }

        /// <summary>
        /// 受单医生编码  同开单医生
        /// <summary>
        public string orders_dr_code { get; set; }

        /// <summary>
        /// 受单医生姓名  
        /// <summary>
        public string orders_dr_name { get; set; }

        /// <summary>
        /// 医院审批标志  见【4码表说明】
        /// <summary>
        public string hosp_appr_flag { get; set; }

        /// <summary>
        /// 中药使用方式  见【4码表说明】
        /// <summary>
        public string tcmdrug_used_way { get; set; }

        /// <summary>
        /// 外检标志  1-是 0-否
        /// <summary>
        public string etip_flag { get; set; }

        /// <summary>
        /// 外检医院编码  按照标准编码填写
        /// <summary>
        public string etip_hosp_code { get; set; }

        /// <summary>
        /// 出院带药标志  1-是 0-否
        /// <summary>
        public string dscg_tkdrug_flag { get; set; }

        /// <summary>
        /// 生育费用标志  1-是 0-否
        /// <summary>
        public string matn_fee_flag { get; set; }

        /// <summary>
        /// 用药频次描述  
        /// <summary>
        public string frqu_dscr { get; set; }

        /// <summary>
        /// 用药周期天数  
        /// <summary>
        public string prd_days { get; set; }

        /// <summary>
        /// 用药途径描述  
        /// <summary>
        public string medc_way_dscr { get; set; }

        /// <summary>
        /// 单次剂量  
        /// <summary>
        public string sin_dos { get; set; }

        /// <summary>
        /// 单次剂量单位  
        /// <summary>
        public string sin_dosunt { get; set; }

        /// <summary>
        /// 使用天数  
        /// <summary>
        public string used_days { get; set; }

        /// <summary>
        /// 发药总量  
        /// <summary>
        public string dismed_amt { get; set; }

        /// <summary>
        /// 发药总量单位  
        /// <summary>
        public string dismed_unt { get; set; }

        /// <summary>
        /// 不进行审核标志  2-医师确认不需审核
        /// <summary>
        public string unchk_flag { get; set; }

        /// <summary>
        /// 不进行审核说明  unchk_flag=2时候，原因必填
        /// <summary>
        public string unchk_memo { get; set; }

        /// <summary>
        /// 备注  
        /// <summary>
        public string memo { get; set; }
        #endregion
    }
}
