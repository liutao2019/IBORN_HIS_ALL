using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5204 : ResponseBase
    {
        /// <summary>
        /// 输出
        /// </summary>
        public List<Output> output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            /// <summary>
            ///就诊ID  Y
            /// </summary>
            [API.GZSI.Common.Display("就诊ID")]
            public string mdtrt_id { get; set; }
            /// <summary>
            ///结算ID  Y
            /// </summary>
            [API.GZSI.Common.Display("结算ID")]
            public string setl_id { get; set; }
            /// <summary>
            /// 费用明细流水号  Y
            /// </summary>
            [API.GZSI.Common.Display("费用明细流水号")]
            public string feedetl_sn { get; set; }
            /// <summary>
            /// 处方/医嘱号
            /// </summary>
            [API.GZSI.Common.Display("处方/医嘱号")]
            public string rx_drord_no { get; set; }
            /// <summary>
            /// 医疗类别  Y
            /// </summary>
            [API.GZSI.Common.Display("医疗类别")]
            public string med_type { get; set; }
            /// <summary>
            /// 费用发生时间  Y
            /// </summary>
            [API.GZSI.Common.Display("费用发生时间")]
            public string fee_ocur_time { get; set; }
            /// <summary>
            /// 数量  Y
            /// </summary>
            [API.GZSI.Common.Display("数量")]
            public string cnt { get; set; }
            /// <summary>
            /// 单价  Y
            /// </summary>
            [API.GZSI.Common.Display("单价")]
            public string pric { get; set; }
            /// <summary>
            /// 单次剂量描述
            /// </summary>
            [API.GZSI.Common.Display("单次剂量描述")]
            public string sin_dos_dscr { get; set; }
            /// <summary>
            /// 使用频次描述
            /// </summary>
            [API.GZSI.Common.Display("使用频次描述")]
            public string used_frqu_dscr { get; set; }
            /// <summary>
            /// 周期天述数
            /// </summary>
            [API.GZSI.Common.Display("周期天述数")]
            public string prd_days { get; set; }
            /// <summary>
            /// 用药途径描述
            /// </summary>
            [API.GZSI.Common.Display("用药途径描述")]
            public string medc_way_dscr { get; set; }
            /// <summary>
            /// 明细项目费用总额  Y
            /// </summary>
            [API.GZSI.Common.Display("明细项目费用总额")]
            public string det_item_fee_sumamt { get; set; }
            /// <summary>
            /// 定价上限金额  Y
            /// </summary>
            [API.GZSI.Common.Display("定价上限金额")]
            public string pric_uplmt_amt { get; set; }
            /// <summary>
            /// 自付比例
            /// </summary>
            [API.GZSI.Common.Display("自付比例")]
            public string selfpay_prop { get; set; }
            /// <summary>
            /// 全自费金额
            /// </summary>
            [API.GZSI.Common.Display("全自费金额")]
            public string fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 超限价金额
            /// </summary>
            [API.GZSI.Common.Display("超限价金额")]
            public string overlmt_amt { get; set; }
            /// <summary>
            /// 先行自付金额
            /// </summary>
            [API.GZSI.Common.Display("先行自付金额")]
            public string preselfpay_amt { get; set; }
            /// <summary>
            /// 符合政策范围金额
            /// </summary>
            [API.GZSI.Common.Display("符合政策范围金额")]
            public string inscp_scp_amt { get; set; }
            /// <summary>
            /// 收费项目等级  Y
            /// </summary>
            [API.GZSI.Common.Display("收费项目等级")]
            public string chrgitm_lv { get; set; }
            /// <summary>
            /// 医保目录编码  Y
            /// </summary>
            [API.GZSI.Common.Display("医保目录编码")]
            public string hilist_code { get; set; }
            /// <summary>
            /// 医保目录名称  Y
            /// </summary>
            [API.GZSI.Common.Display("医保目录名称")]
            public string hilist_name { get; set; }
            /// <summary>
            /// 目录类别  Y
            /// </summary>
            [API.GZSI.Common.Display("目录类别")]
            public string list_type { get; set; }
            /// <summary>
            /// 医疗目录编码  Y
            /// </summary>
            [API.GZSI.Common.Display("医疗目录编码")]
            public string med_list_codg { get; set; }
            /// <summary>
            /// 医药机构目录编码  Y
            /// </summary>
            [API.GZSI.Common.Display("医药机构目录编码")]
            public string medins_list_codg { get; set; }
            /// <summary>
            /// 医药机构目录名称  Y
            /// </summary>
            [API.GZSI.Common.Display("医药机构目录名称")]
            public string medins_list_name { get; set; }
            /// <summary>
            /// 医疗收费项目类别  Y
            /// </summary>
            [API.GZSI.Common.Display("医疗收费项目类别")]
            public string med_chrgitm_type { get; set; }
            /// <summary>
            /// 商品名
            /// </summary>
            [API.GZSI.Common.Display("商品名")]
            public string prodname { get; set; }
            /// <summary>
            /// 规格
            /// </summary>
            [API.GZSI.Common.Display("规格")]
            public string spec { get; set; }
            /// <summary>
            /// 剂型名称  Y
            /// </summary>
            [API.GZSI.Common.Display("剂型名称")]
            public string dosform_name { get; set; }
            /// <summary>
            /// 开单科室编码
            /// </summary>
            [API.GZSI.Common.Display("开单科室编码")]
            public string bilg_dept_codg { get; set; }
            /// <summary>
            /// 开单科室名称
            /// </summary>
            [API.GZSI.Common.Display("开单科室名称")]
            public string bilg_dept_name { get; set; }
            /// <summary>
            /// 开单医生编码
            /// </summary>
            [API.GZSI.Common.Display("开单医生编码")]
            public string bilg_dr_codg { get; set; }
            /// <summary>
            /// 开单医师姓名
            /// </summary>
            [API.GZSI.Common.Display("开单医师姓名")]
            public string bilg_dr_name { get; set; }
            /// <summary>
            /// 受单科室编码
            /// </summary>
            [API.GZSI.Common.Display("受单科室编码")]
            public string acord_dept_codg { get; set; }
            /// <summary>
            /// 受单科室名称
            /// </summary>
            [API.GZSI.Common.Display("受单科室名称")]
            public string acord_dept_name { get; set; }
            /// <summary>
            /// 受单医生编码
            /// </summary>
            [API.GZSI.Common.Display("受单医生编码")]
            public string orders_dr_code { get; set; }
            /// <summary>
            /// 受单医生姓名
            /// </summary>
            [API.GZSI.Common.Display("受单医生姓名")]
            public string orders_dr_name { get; set; }
            /// <summary>
            /// 限制使用标志
            /// </summary>
            [API.GZSI.Common.Display("限制使用标志")]
            public string lmt_used_flag { get; set; }
            /// <summary>
            /// 医院制剂标志
            /// </summary>
            [API.GZSI.Common.Display("医院制剂标志")]
            public string hosp_prep_flag { get; set; }
            /// <summary>
            /// 医院审批标志
            /// </summary>
            [API.GZSI.Common.Display("医院审批标志")]
            public string hosp_appr_flag { get; set; }
            /// <summary>
            /// 中药使用方式
            /// </summary>
            [API.GZSI.Common.Display("中药使用方式")]
            public string tcmdrug_used_way { get; set; }
            /// <summary>
            /// 生产地类别
            /// </summary>
            [API.GZSI.Common.Display("生产地类别")]
            public string prodplac_type { get; set; }
            /// <summary>
            /// 基本药物标志
            /// </summary>
            [API.GZSI.Common.Display("基本药物标志")]
            public string bas_medn_flag { get; set; }
            /// <summary>
            /// 医保谈判药品标志
            /// </summary>
            [API.GZSI.Common.Display("医保谈判药品标志")]
            public string hi_nego_drug_flag { get; set; }
            /// <summary>
            /// 儿童用药标志
            /// </summary>
            [API.GZSI.Common.Display("儿童用药标志")]
            public string chld_medc_flag { get; set; }
            /// <summary>
            /// 外检标志
            /// </summary>
            [API.GZSI.Common.Display("外检标志")]
            public string etip_flag { get; set; }
            /// <summary>
            /// 外检医院编码
            /// </summary>
            [API.GZSI.Common.Display("外检医院编码")]
            public string etip_hosp_code { get; set; }
            /// <summary>
            /// 出院带药标志
            /// </summary>
            [API.GZSI.Common.Display("出院带药标志")]
            public string dscg_tkdrug_flag { get; set; }
            /// <summary>
            /// 目录特项标志
            /// </summary>
            [API.GZSI.Common.Display("目录特项标志")]
            public string list_sp_item_flag { get; set; }
            /// <summary>
            /// 生育费用标志
            /// </summary>
            [API.GZSI.Common.Display("生育费用标志")]
            public string matn_fee_flag { get; set; }
            /// <summary>
            /// 直报标志
            /// </summary>
            [API.GZSI.Common.Display("直报标志")]
            public string drt_reim_flag { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            [API.GZSI.Common.Display("备注")]
            public string memo { get; set; }
            /// <summary>
            /// 经办人ID
            /// </summary>
            [API.GZSI.Common.Display("经办人ID")]
            public string opter_id { get; set; }
            /// <summary>
            /// 经办人姓名
            /// </summary>
            [API.GZSI.Common.Display("经办人姓名")]
            public string opter_name { get; set; }
            /// <summary>
            /// 经办时间
            /// </summary>
            [API.GZSI.Common.Display("经办时间")]
            public string opt_time { get; set; }

        }
    }
}
