using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Outpatient
{
    /// <summary>
    /// 门诊单据式结算请求Feedetail
    /// </summary>
    public class Feedetail
    {
        public List<FeedetailRow> rows { get; set; }
    }

    public class FeedetailRow
    {
        #region 费用信息feedetail
        /// <summary>
        /// 记账流水号
        /// </summary>
        public string feedetl_sn { get; set; }
        /// <summary>
        /// 病种编号
        /// </summary>
        public string dise_no { get; set; }
        /// <summary>
        /// 处方/医嘱号
        /// </summary>
        public string rx_drord_no { get; set; }
        /// <summary>
        /// 费用发生日期 
        /// </summary>
        public string fee_ocur_date { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string cnt { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string pric { get; set; }
        /// <summary>
        /// 明细项目费用总额
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
        /// 用药周期天数 10
        /// </summary>
        public string prd_days { get; set; }
        /// <summary>
        /// 用药途径描述
        /// </summary>
        public string medc_way_dscr { get; set; }
        /// <summary>
        /// 医疗目录编码
        /// </summary>
        public string med_list_codg { get; set; }
        /// <summary>
        /// 医疗机构目录编码 
        /// </summary>
        public string medins_milist_codg { get; set; }
        /// <summary>
        /// 医疗机构目录名称
        /// </summary>
        public string medins_list_name { get; set; }
        /// <summary>
        /// 开单科室编码
        /// </summary>
        public string bilg_dept_codg { get; set; }
        /// <summary>
        /// 开单科室名称
        /// </summary>
        public string bilg_dept_name { get; set; }
        /// <summary>
        /// 开单医生编码
        /// </summary>
        public string bilg_dr_codg { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
        public string bilg_dr_name { get; set; }
        /// <summary>
        /// 受单科室编码
        /// </summary>
        public string orders_dept_code { get; set; }
        /// <summary>
        /// 受单科室名称
        /// </summary>
        public string orders_dept_name { get; set; }
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
        public string extins_flag { get; set; }
        /// <summary>
        /// 外检医院编码
        /// </summary>
        public string extins_hosp_code { get; set; }
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
        #endregion
    }
}
