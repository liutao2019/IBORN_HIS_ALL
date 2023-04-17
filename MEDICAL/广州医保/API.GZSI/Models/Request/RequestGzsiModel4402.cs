using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4402
    {
        /// <summary>
        /// 医嘱信息
        /// </summary>
        public List<Data> data { get; set; }

        /// <summary>
        /// Data类
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 就医流水号 Y
            /// </summary>
            public string mdtrt_sn { get; set; }
            /// <summary>
            /// 就诊ID Y
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 住院床号
            /// </summary>
            public string ipt_bedno { get; set; }
            /// <summary>
            /// 医嘱号
            /// </summary>
            public string drord_no { get; set; }
            /// <summary>
            /// 下达科室代码
            /// </summary>
            public string isu_dept_code { get; set; }
            /// <summary>
            /// 医嘱下达时间
            /// </summary>
            public string drord_isu_no { get; set; }
            /// <summary>
            /// 执行科室代码
            /// </summary>
            public string exe_dept_code { get; set; }
            /// <summary>
            /// 执行科室名称
            /// </summary>
            public string exedept_name { get; set; }
            /// <summary>
            /// 医嘱审核人姓名
            /// </summary>
            public string drord_chker_name { get; set; }
            /// <summary>
            /// 医嘱执行人姓名
            /// </summary>
            public string drord_ptr_name { get; set; }
            /// <summary>
            /// 医嘱组号
            /// </summary>
            public string drord_grpno { get; set; }
            /// <summary>
            /// 医嘱类别
            /// </summary>
            public string drord_type { get; set; }
            /// <summary>
            /// 医嘱项目分类代码
            /// </summary>
            public string drord_item_type { get; set; }
            /// <summary>
            /// 医嘱项目分类名称
            /// </summary>
            public string drord_item_name { get; set; }
            /// <summary>
            /// 医嘱明细代码
            /// </summary>
            public string drord_detl_code { get; set; }
            /// <summary>
            /// 医嘱明细名称
            /// </summary>
            public string drord_detl_name { get; set; }
            /// <summary>
            /// 药物类型代码
            /// </summary>
            public string medn_type_code { get; set; }
            /// <summary>
            /// 药物类型
            /// </summary>
            public string medn_type_name { get; set; }
            /// <summary>
            /// 药物剂型代码
            /// </summary>
            public string drug_dosform { get; set; }
            /// <summary>
            /// 药物剂型名称
            /// </summary>
            public string drug_dosform_name { get; set; }
            /// <summary>
            /// 药品规格
            /// </summary>
            public string drug_spec { get; set; }
            /// <summary>
            /// 发药数量
            /// </summary>
            public string dismed_cnt { get; set; }
            /// <summary>
            /// 发药数量单位
            /// </summary>
            public string dismed_cnt_unt { get; set; }
            /// <summary>
            /// 药物使用-频率
            /// </summary>
            public string medn_use_frqu { get; set; }
            /// <summary>
            /// 药物使用-剂量单位
            /// </summary>
            public string medn_used_dosunt { get; set; }
            /// <summary>
            /// 药物使用-次剂量
            /// </summary>
            public string drug_used_sdose { get; set; }
            /// <summary>
            /// 药物使用-总剂量
            /// </summary>
            public string drug_used_idose { get; set; }
            /// <summary>
            /// 药物使用-途径代码
            /// </summary>
            public string drug_used_way_code { get; set; }
            /// <summary>
            /// 药物使用-途径
            /// </summary>
            public string drug_used_way { get; set; }
            /// <summary>
            /// 用药天数
            /// </summary>
            public string medc_days { get; set; }
            /// <summary>
            /// 用药开始时间
            /// </summary>
            public string medc_begntime { get; set; }
            /// <summary>
            /// 用药停止时间
            /// </summary>
            public string medc_endtime { get; set; }
            /// <summary>
            /// 皮试判别
            /// </summary>
            public string skintst_dicm { get; set; }
            /// <summary>
            /// 草药脚注
            /// </summary>
            public string tcmherb_foote { get; set; }
            /// <summary>
            /// 医嘱停止日期时间
            /// </summary>
            public string drord_endtime { get; set; }
            /// <summary>
            /// 住院科室代码
            /// </summary>
            public string ipt_dept_code { get; set; }
            /// <summary>
            /// 医疗机构组织机构代码
            /// </summary>
            public string medins_orgcode { get; set; }
            /// <summary>
            /// 是否统一采购药品
            /// </summary>
            public string unif_purc_drug_flag { get; set; }
            /// <summary>
            /// 药管平台码
            /// </summary>
            public string drug_mgt_plaf_code { get; set; }
            /// <summary>
            /// 药品采购码
            /// </summary>
            public string drug_purc_code { get; set; }
            /// <summary>
            /// 基本药物标志
            /// </summary>
            public string bas_medn_flag { get; set; }
            /// <summary>
            /// 有效标志 Y
            /// </summary>
            public string vali_flag { get; set; }

            /// <summary>
            /// 病案医嘱明细id Y
            /// </summary>
            public string medcas_drord_detl_id { get; set; }
        }
    }
}
