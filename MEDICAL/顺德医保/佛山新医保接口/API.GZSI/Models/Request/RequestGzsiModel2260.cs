using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 门诊单据式结算
    /// </summary>
    public class RequestGzsiModel2260 
    {
        public Mdtrtinfo mdtrtinfo { get; set; }
        public Agnterinfo agnterinfo { get; set; }
        public Diseinfo diseinfo { get; set; }
        public Feedetail feedetail { get; set; }

        public class Mdtrtinfo
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 就诊凭证类型
            /// </summary>
            public string mdtrt_cert_type { get; set; }
            /// <summary>
            /// 就诊凭证编号
            /// </summary>
            public string mdtrt_cert_no { get; set; }
            /// <summary>
            /// 持卡就诊基本信息
            /// </summary>
            public string hcard_basinfo { get; set; }
            /// <summary>
            /// 持卡就诊校验信息 
            /// </summary>
            public string hcard_chkinfo { get; set; }
            /// <summary>
            /// 就诊方式
            /// </summary>
            public string mdtrt_mode { get; set; }
            /// <summary>
            /// 结算标志 0：试算、1：结算
            /// </summary>
            public string setl_flag { get; set; }
            /// <summary>
            /// 险种类型 8 见【4码表说明】
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 医疗类别 见【4码表说明】
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 开始时间 yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string begntime { get; set; }
            /// <summary>
            /// 结束时间 yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string endtime { get; set; }
            /// <summary>
            /// 住院/门诊号
            /// </summary>
            public string ipt_op_no { get; set; }
            /// <summary>
            /// 科室编码
            /// </summary>
            public string adm_dept_code { get; set; }
            /// <summary>
            /// 科室名称 14
            /// </summary>
            public string adm_dept_name { get; set; }
            /// <summary>
            /// 医疗机构订单号或医疗机构就医序列号
            /// </summary>
            public string order_no { get; set; }
            /// <summary>
            /// 主要病情描述
            /// </summary>
            public string main_cond_desc { get; set; }
            /// <summary>
            /// 病种编码
            /// </summary>
            public string dise_no { get; set; }
            /// <summary>
            /// 病种名称
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 主诊断编码
            /// </summary>
            public string dscg_maindiag_code { get; set; }
            /// <summary>
            /// 主诊断名称
            /// </summary>
            public string dscg_maindiag_name { get; set; }
            /// <summary>
            /// 临床试验标志 Y 0-否  1-是
            /// </summary>
            public string clnc_flag { get; set; }
            /// <summary>
            /// 手术操作代码
            /// </summary>
            public string oprn_oprt_code { get; set; }
            /// <summary>
            /// 手术操作名称
            /// </summary>
            public string oprn_oprt_name { get; set; }
            /// <summary>
            /// 门诊诊断信息
            /// </summary>
            public string op_dise_info { get; set; }
            /// <summary>
            /// 发票号
            /// </summary>
            public string invono { get; set; }
            /// <summary>
            /// 计划生育手术类别 生育门诊按需录入。见【4码表说明】
            /// </summary>
            public string birctrl_type { get; set; }
            /// <summary>
            /// 计划生育手术或生育日期
            /// </summary>
            public string birctrl_or_matn_time { get; set; }
        }

        public class Agnterinfo
        {
            /// <summary>
            /// 代办人姓名
            /// </summary>
            public string agnter_name { get; set; }
            /// <summary>
            /// 代办人关系
            /// </summary>
            public string agnter_rlts { get; set; }
            /// <summary>
            /// 代办人证件类型
            /// </summary>
            public string agnter_cert_type { get; set; }
            /// <summary>
            /// 代办人证件号码
            /// </summary>
            public string agnter_certno { get; set; }
            /// <summary>
            /// 代办人联系电话
            /// </summary>
            public string agnter_tel { get; set; }
            /// <summary>
            /// 代办人联系地址
            /// </summary>
            public string agnter_addr { get; set; }
            /// <summary>
            /// 代办人照片
            /// </summary>
            public string agnter_photo { get; set; }
        }

        public class Diseinfo
        {
            public List<DiseinfoRow> rows { get; set; }

            public class DiseinfoRow
            {
                #region 诊断信息diseinfo
                /// <summary>
                /// 诊断类别
                /// </summary>
                public string dise_type { get; set; }
                /// <summary>
                /// 诊断排序号
                /// </summary>
                public string dise_srt_no { get; set; }
                /// <summary>
                /// 诊断代码
                /// </summary>
                public string dise_code { get; set; }
                /// <summary>
                /// 诊断名称
                /// </summary>
                public string dise_name { get; set; }
                /// <summary>
                /// 诊断科室
                /// </summary>
                public string dise_dept { get; set; }
                /// <summary>
                /// 诊断医生编码
                /// </summary>
                public string dise_dor_no { get; set; }
                /// <summary>
                /// 诊断医生姓名
                /// </summary>
                public string dise_dor_name { get; set; }
                /// <summary>
                /// 诊断时间 yyyy-MM-dd HH:mm:ss
                /// </summary>
                public string dise_time { get; set; }
                #endregion
            }
        }

        public class Feedetail
        {
            public List<FeedetailRow> rows { get; set; }

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
    }
}
