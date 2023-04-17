using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4701
    {
        /// <summary>
        /// 入院信息
        /// </summary>
        public Adminfo adminfo { get; set; }

        /// <summary>
        /// 诊断信息
        /// </summary>
        public List<Diseinfo> diseinfo { get; set; }

        /// <summary>
        /// 病程信息
        /// </summary>
        public Coursinfo coursrinfo { get; set; }

        /// <summary>
        /// 手术信息
        /// </summary>
        public List<Oprninfo> oprninfo { get; set; }

        /// <summary>
        /// 病情抢救信息
        /// </summary>
        public List<Rescinfo> rescinfo { get; set; }

        /// <summary>
        /// 死亡信息
        /// </summary>
        public Dieinfo dieinfo { get; set; }

        /// <summary>
        /// 出院小结
        /// </summary>
        public Dscginfo dscginfo { get; set; }


        #region 类定义
        /// <summary>
        /// 入院信息类
        /// </summary>
        public class Adminfo
        {
            /// <summary>
            /// 就诊 ID  Y
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 住院号 Y
            /// </summary>
            public string mdtrtsn { get; set; }
            /// <summary>
            /// 住院流水号 Y
            /// </summary>
            public string mdtrt_sn { get; set; }
            /// <summary>
            /// 姓名 Y
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 性别 Y
            /// </summary>
            public string gend { get; set; }
            /// <summary>
            /// 年龄 
            /// </summary>
            public string age { get; set; }
            /// <summary>
            /// 入院记录流水号 Y
            /// </summary>
            public string adm_rec_no { get; set; }
            /// <summary>
            /// 住院流水号 Y
            /// </summary>
            public string psn_adm_no { get; set; }
            /// <summary>
            /// 病区名称 Y
            /// </summary>
            public string wardarea_name { get; set; }
            /// <summary>
            /// 科室代码 Y
            /// </summary>
            public string dept_code { get; set; }
            /// <summary>
            /// 科室名称 Y
            /// </summary>
            public string dept_name { get; set; }
            /// <summary>
            /// 病床号 Y
            /// </summary>
            public string bedno { get; set; }
            /// <summary>
            /// 入院日期时间 Y
            /// </summary>
            public string adm_time { get; set; }
            /// <summary>
            /// 病史陈述者姓名 Y
            /// </summary>
            public string illhis_stte_name { get; set; }
            /// <summary>
            /// 陈述者与患者关系代码 Y
            /// </summary>
            public string illhis_stte_rltl { get; set; }
            /// <summary>
            /// 陈述内容是否可靠标识 Y 
            /// </summary>
            public string stte_rele { get; set; }
            /// <summary>
            /// 主诉 Y
            /// </summary>
            public string chfcomp { get; set; }
            /// <summary>
            /// 现病史 Y
            /// </summary>
            public string dise_now { get; set; }
            /// <summary>
            /// 一般健康状况标志 Y
            /// </summary>
            public string hlcon { get; set; }
            /// <summary>
            /// 疾病史（含外伤） Y
            /// </summary>
            public string dise_his { get; set; }
            /// <summary>
            /// 患者传染性标志 Y
            /// </summary>
            public string ifet { get; set; }
            /// <summary>
            /// 传染病史 Y
            /// </summary>
            public string ifet_his { get; set; }
            /// <summary>
            /// 预防接种史 Y
            /// </summary>
            public string prev_vcnt { get; set; }
            /// <summary>
            /// 手术史 Y
            /// </summary>
            public string oprn_his { get; set; }
            /// <summary>
            /// 输血史 Y
            /// </summary>
            public string bld_his { get; set; }
            /// <summary>
            /// 过敏史 Y
            /// </summary>
            public string algs_his { get; set; }
            /// <summary>
            /// 个人史 Y
            /// </summary>
            public string psn_his { get; set; }
            /// <summary>
            /// 婚育史 Y
            /// </summary>
            public string mrg_his { get; set; }
            /// <summary>
            /// 月经史 Y
            /// </summary>
            public string mena_his { get; set; }
            /// <summary>
            /// 家族史 Y
            /// </summary>
            public string fmhis { get; set; }
            /// <summary>
            /// 体 格 检 查 -- 体 温（℃） Y
            /// </summary>
            public string physexm_tprt { get; set; }
            /// <summary>
            /// 体格检查 -- 脉率（次 /mi 数字） Y
            /// </summary>
            public string physexm_pule { get; set; }
            /// <summary>
            /// 体格检查--呼吸频率 Y
            /// </summary>
            public string physexm_vent_frqu { get; set; }
            /// <summary>
            /// 体格检查 -- 收缩压 （mmHg）  Y
            /// </summary>
            public string physexm_systolic_pre { get; set; }
            /// <summary>
            /// 体格检查 -- 舒张压 （mmHg）Y
            /// </summary>
            public string physexm_dstl_pre { get; set; }
            /// <summary>
            /// 体格检查 -- 身 高（cm） Y
            /// </summary>
            public string physexm_height  { get; set; }
            /// <summary>
            /// 体格检查 -- 体 重（kg） Y
            /// </summary>
            public string physexm_wt { get; set; }
            /// <summary>
            /// 体格检查 -- 一般状况 检查结果 Y
            /// </summary>
            public string physexm_ordn_stas { get; set; }
            /// <summary>
            /// 体格检查 -- 皮肤和粘膜检查结果 Y
            /// </summary>
            public string physexm_skin_musl { get; set; }
            /// <summary>
            /// 体格检查 -- 全身浅表淋巴结检查结果 Y
            /// </summary>
            public string physexm_spef_lymph  { get; set; }
            /// <summary>
            /// 体格检查 -- 头部及其器官检查结果 Y
            /// </summary>
            public string physexm_head { get; set; }
            /// <summary>
            /// 体格检查 -- 颈部检查结果  Y
            /// </summary>
            public string physexm_neck { get; set; }
            /// <summary>
            /// 体格检查 -- 胸部检查结果  Y
            /// </summary>
            public string physexm_chst { get; set; }
            /// <summary>
            /// 体格检查 -- 腹部检查结果 Y
            /// </summary>
            public string physexm_abd { get; set; }
            /// <summary>
            /// 体格检查 -- 肛门指诊检查结果描述 Y
            /// </summary>
            public string physexm_finger_exam  { get; set; }
            /// <summary>
            /// 体格检查 -- 外生殖器检查结果 Y
            /// </summary>
            public string physexm_genital_area  { get; set; }
            /// <summary>
            /// 体格检查 -- 脊柱检查结果 Y 
            /// </summary>
            public string physexm_spin { get; set; }
            /// <summary>
            /// 体格检查 -- 四肢检查结果 Y 
            /// </summary>
            public string physexm_all_fors  { get; set; }
            /// <summary>
            /// 体格检查 -- 神经系统检查结果  Y
            /// </summary>
            public string nersys { get; set; }
            /// <summary>
            /// 专科情况 Y
            /// </summary>
            public string spcy_info { get; set; }
            /// <summary>
            /// 辅助检查结果 Y
            /// </summary>
            public string asst_exam_rslt  { get; set; }
            /// <summary>
            /// 中医“四诊”观察结果描述 
            /// </summary>
            public string tcm4d_rslt { get; set; }
            /// <summary>
            /// 辨证分型代码 
            /// </summary>
            public string syddclft { get; set; }
            /// <summary>
            /// 辩证分型名称 
            /// </summary>
            public string syddclft_name { get; set; }
            /// <summary>
            /// 治则治法 
            /// </summary>
            public string prnp_trt { get; set; }
            /// <summary>
            /// 接诊医生编号 Y
            /// </summary>
            public string rec_doc_code { get; set; }
            /// <summary>
            /// 接诊医生姓名 Y
            /// </summary>
            public string rec_doc_name { get; set; }
            /// <summary>
            /// 住院医师编号 Y
            /// </summary>
            public string ipdr_code { get; set; }
            /// <summary>
            /// 住院医师姓名 Y
            /// </summary>
            public string ipdr_name { get; set; }
            /// <summary>
            /// 主任医师编号 Y
            /// </summary>
            public string chfdr_code { get; set; }
            /// <summary>
            /// 主任医师姓名 Y
            /// </summary>
            public string chfdr_name { get; set; }
            /// <summary>
            /// 主诊医师代码 Y
            /// </summary>
            public string chfpdr_code { get; set; }
            /// <summary>
            /// 主诊医师姓名 Y
            /// </summary>
            public string chfpdr_name { get; set; }
            /// <summary>
            /// 主治医师编号 Y
            /// </summary>
            public string atddr_code { get; set; }
            /// <summary>
            /// 主治医师姓名 Y
            /// </summary>
            public string atddr_name { get; set; }
            /// <summary>
            /// 主要症状 Y
            /// </summary>
            public string main_symp { get; set; }
            /// <summary>
            /// 入院原因 Y
            /// </summary>
            public string adm_rea { get; set; }
            /// <summary>
            /// 入院途径 Y
            /// </summary>
            public string adm_way { get; set; }
            /// <summary>
            /// 评分值(Apgar) Y
            /// </summary>
            public string apgr { get; set; }
            /// <summary>
            /// 饮食情况 Y
            /// </summary>
            public string diet_info { get; set; }
            /// <summary>
            /// 发育程度 Y
            /// </summary>
            public string growth_deg { get; set; }
            /// <summary>
            /// 精神状态正常标志 Y
            /// </summary>
            public string mtl_stas_norm { get; set; }
            /// <summary>
            /// 睡眠状况 Y
            /// </summary>
            public string slep_info { get; set; }
            /// <summary>
            /// 特殊情况 Y
            /// </summary>
            public string sp_info { get; set; }
            /// <summary>
            /// 心理状态 Y
            /// </summary>
            public string mind_info { get; set; }
            /// <summary>
            /// 营养状态 Y
            /// </summary>
            public string nurt { get; set; }
            /// <summary>
            /// 自理能力 Y
            /// </summary>
            public string self_ablt { get; set; }
            /// <summary>
            /// 护理观察项目名称 Y
            /// </summary>
            public string nurscare_obsv_item_name { get; set; }
            /// <summary>
            /// 护理观察结果 Y
            /// </summary>
            public string nurscare_obsv_rslt  { get; set; }
            /// <summary>
            /// 吸烟标志 Y
            /// </summary>
            public string smoke { get; set; }
            /// <summary>
            /// 停止吸烟天数 Y
            /// </summary>
            public string stop_smok_days { get; set; }
            /// <summary>
            /// 吸烟状况 Y
            /// </summary>
            public string smok_info { get; set; }
            /// <summary>
            /// 日吸烟量（支） Y
            /// </summary>
            public string smok_day { get; set; }
            /// <summary>
            /// 饮酒标志 Y
            /// </summary>
            public string drnk { get; set; }
            /// <summary>
            /// 饮酒频率 Y
            /// </summary>
            public string drnk_frqu { get; set; }
            /// <summary>
            /// 日饮酒量（mL） Y
            /// </summary>
            public string drnk_day { get; set; }
            /// <summary>
            /// 评估日期时间
            /// </summary>
            public string eval_time { get; set; }
            /// <summary>
            /// 责任护士姓名 Y
            /// </summary>
            public string resp_nurs_name  { get; set; }
            /// <summary>
            /// 有效标志 Y
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 诊断信息类
        /// </summary>
        public class Diseinfo
        {
            /// <summary>
            /// 出入院诊断分类 Y
            /// </summary>
            public string inout_diag_type { get; set; }
            /// <summary>
            /// 主诊断标志 Y
            /// </summary>
            public string maindiag_flag { get; set; }
            /// <summary>
            /// 诊断序列号 Y
            /// </summary>
            public string diag_seq { get; set; }
            /// <summary>
            /// 诊断日期时间 Y
            /// </summary>
            public string diag_time { get; set; }
            /// <summary>
            /// 西医诊断编码 Y
            /// </summary>
            public string wm_diag_code { get; set; }
            /// <summary>
            /// 西医诊断名称 Y
            /// </summary>
            public string wm_diag_name { get; set; }
            /// <summary>
            /// 中医病名代码
            /// </summary>
            public string tcm_dise_code { get; set; }
            /// <summary>
            /// 中医病名
            /// </summary>
            public string tcm_dise_name { get; set; }
            /// <summary>
            /// 中医证候代码
            /// </summary>
            public string tcmsymp_code { get; set; }
            /// <summary>
            /// 中医证候 
            /// </summary>
            public string tcmsymp { get; set; }
            /// <summary>
            /// 有效标志 Y 
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 病程信息类
        /// </summary>
        public class Coursinfo
        {
            /// <summary>
            /// 科室代码
            /// </summary>
            public string dept_code { get; set; }
            /// <summary>
            /// 科室名称
            /// </summary>
            public string dept_name { get; set; }
            /// <summary>
            /// 病区名称
            /// </summary>
            public string wardarea_name { get; set; }
            /// <summary>
            /// 病床号
            /// </summary>
            public string bedno { get; set; }
            /// <summary>
            /// 记录日期时间
            /// </summary>
            public string rcd_time { get; set; }
            /// <summary>
            /// 主诉
            /// </summary>
            public string chfcomp { get; set; }
            /// <summary>
            /// 病例特点
            /// </summary>
            public string cas_ftur { get; set; }
            /// <summary>
            /// 中医“四诊”观察结果
            /// </summary>
            public string tcm4d_rslt { get; set; }
            /// <summary>
            /// 诊断依据 
            /// </summary>
            public string dise_evid { get; set; }
            /// <summary>
            /// 初步诊断-西医诊断编码 
            /// </summary>
            public string prel_wm_diag_code { get; set; }
            /// <summary>
            /// 初步诊断-西医诊断名称
            /// </summary>
            public string prel_wm_dise_name  { get; set; }
            /// <summary>
            /// 初步诊断-中医病名代码
            /// </summary>
            public string prel_tcm_diag_code { get; set; }
            /// <summary>
            /// 初步诊断-中医病名
            /// </summary>
            public string prel_tcm_dise_name { get; set; }
            /// <summary>
            /// 初步诊断-中医证候代码
            /// </summary>
            public string prel_tcmsymp_code  { get; set; }
            /// <summary>
            /// 初步诊断-中医证候
            /// </summary>
            public string prel_tcmsymp { get; set; }
            /// <summary>
            /// 鉴别诊断-西医诊断编码
            /// </summary>
            public string finl_wm_diag_code { get; set; }
            /// <summary>
            /// 鉴别诊断-西医诊断名称 
            /// </summary>
            public string finl_wm_diag_name { get; set; }
            /// <summary>
            /// 鉴别诊断-中医病名代码
            /// </summary>
            public string finl_tcm_dise_code  { get; set; }
            /// <summary>
            /// 鉴别诊断-中医病名
            /// </summary>
            public string finl_tcm_dise_name { get; set; }
            /// <summary>
            /// 鉴别诊断-中医证候代码 
            /// </summary>
            public string finl_tcmsymp_code { get; set; }
            /// <summary>
            /// 鉴别诊断-中医证候
            /// </summary>
            public string finl_tcmsymp { get; set; }
            /// <summary>
            /// 诊疗计划
            /// </summary>
            public string dise_plan { get; set; }
            /// <summary>
            /// 治则治法
            /// </summary>
            public string prnp_trt { get; set; }
            /// <summary>
            /// 住院医师编号
            /// </summary>
            public string ipdr_code { get; set; }
            /// <summary>
            /// 住院医师姓名
            /// </summary>
            public string ipdr_name { get; set; }

            /// <summary>
            /// 住院医师编号
            /// </summary>
            public string ipt_dr_code { get; set; }
            /// <summary>
            /// 住院医师姓名
            /// </summary>
            public string ipt_dr_name { get; set; }

            /// <summary>
            /// 上级医师姓名
            /// </summary>
            public string prnt_doc_name { get; set; }
            /// <summary>
            /// 有效标志
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 手术记录类
        /// </summary>
        public class Oprninfo
        {

            /// <summary>
            /// 手术申请单号 Y
            /// </summary>
            public string oprn_appy_id { get; set; }

            /// <summary>
            /// 手术序列号 Y
            /// </summary>
            public string oprn_seq { get; set; }

            /// <summary>
            /// ABO血型代 Y
            /// </summary>
            public string blotype_abobfpn_inhosp_ifet { get; set; }

            /// <summary>
            /// 手术日期 Y
            /// </summary>
            public string oprn_time { get; set; }

            /// <summary>
            /// 手术分类代码 Y
            /// </summary>
            public string oprn_type_code { get; set; }

            /// <summary>
            /// 手术分类名称 Y
            /// </summary>
            public string oprn_type_name { get; set; }

            /// <summary>
            /// 术前诊断代码 Y
            /// </summary>
            public string bfpn_diag_code { get; set; }

            /// <summary>
            /// 术前是否发生院内感染 Y
            /// </summary>
            public string bfpn_inhosp_ifet { get; set; }
            /// <summary>
            /// 术后诊断代码
            /// </summary>
            public string afpn_diag_code  { get; set; }
            /// <summary>
            /// 术后诊断名称
            /// </summary>
            public string afpn_diag_name { get; set; }
            /// <summary>
            /// 手术切口愈合等级代码 Y
            /// </summary>
            public string sinc_heal_lv { get; set; }
            /// <summary>
            /// 手术切口愈合等级
            /// </summary>
            public string sinc_heal_lv_code  { get; set; }
            /// <summary>
            /// 是否重返手术（明确定义）
            /// </summary>
            public string back_oprn { get; set; }
            /// <summary>
            /// 是否择期 
            /// </summary>
            public string selv { get; set; }
            /// <summary>
            /// 是否预防使用抗菌药物
            /// </summary>
            public string prev_abtl_mednme { get; set; }
            /// <summary>
            ///  预防使用抗菌药物天数 
            /// </summary>
            public string abtl_medn_days { get; set; }
            /// <summary>
            /// 手术操作代码
            /// </summary>
            public string oprn_oprt_code  { get; set; }
            /// <summary>
            /// 手术操作名称
            /// </summary>
            public string oprn_oprt_name { get; set; }
            /// <summary>
            /// 手术级别代码 Y
            /// </summary>
            public string oprn_lv_code { get; set; }
            /// <summary>
            /// 手术级别名称
            /// </summary>
            public string oprn_lv_name { get; set; }
            /// <summary>
            /// 麻醉-方法代码
            /// </summary>
            public string anst_mtd_code { get; set; }
            /// <summary>
            ///  麻醉-方法名称 
            /// </summary>
            public string anst_mtd_name { get; set; }
            /// <summary>
            /// 麻醉分级代码 Y
            /// </summary>
            public string anst_lv_code { get; set; }
            /// <summary>
            ///  麻醉分级名称
            /// </summary>
            public string anst_lv_name { get; set; }
            /// <summary>
            /// 麻醉执行科室代码 Y
            /// </summary>
            public string exe_anst_dept_code { get; set; }
            /// <summary>
            /// 麻醉执行科室名称
            /// </summary>
            public string exe_anst_dept_name { get; set; }
            /// <summary>
            /// 麻醉效果
            /// </summary>
            public string anst_efft { get; set; }
            /// <summary>
            /// 手术开始时间
            /// </summary>
            public string oprn_begntime { get; set; }
            /// <summary>
            /// 手术结束时间
            /// </summary>
            public string oprn_endtime { get; set; }
            /// <summary>
            /// 是否无菌手术
            /// </summary>
            public string oprn_asps { get; set; }
            /// <summary>
            /// 无菌手术是否感染
            /// </summary>
            public string oprn_asps_ifet  { get; set; }
            /// <summary>
            /// 手术后情况
            /// </summary>
            public string afpn_info { get; set; }
            /// <summary>
            /// 是否手术合并症
            /// </summary>
            public string oprn_merg { get; set; }
            /// <summary>
            /// 是否手术并发
            /// </summary>
            public string oprn_conc { get; set; }
            /// <summary>
            /// 手术执行科室代码
            /// </summary>
            public string oprn_anst_dept_code { get; set; }
            /// <summary>
            /// 手术执行科室名称
            /// </summary>
            public string oprn_anst_dept_name { get; set; }
            /// <summary>
            /// 病理检查
            /// </summary>
            public string palg_dise { get; set; }
            /// <summary>
            /// 其他医学处置
            /// </summary>
            public string oth_med_dspo { get; set; }
            /// <summary>
            /// 是否超出标准手术时间 
            /// </summary>
            public string out_std_oprn_time { get; set; }
            /// <summary>
            /// 手术者姓名 
            /// </summary>
            public string oprn_oper_name { get; set; }
            /// <summary>
            /// 助手I姓名 
            /// </summary>
            public string oprn_asit_name1 { get; set; }
            /// <summary>
            ///  助手Ⅱ姓名
            /// </summary>
            public string oprn_asit_name2  { get; set; }
            /// <summary>
            /// 麻醉医师姓名
            /// </summary>
            public string anst_dr_name { get; set; }
            /// <summary>
            /// 麻醉ASA分级代码 
            /// </summary>
            public string anst_asa_lv_code { get; set; }
            /// <summary>
            /// 麻醉ASA分级名称 
            /// </summary>
            public string anst_asa_lv_name { get; set; }
            /// <summary>
            /// 麻醉药物代码
            /// </summary>
            public string anst_medn_code  { get; set; }
            /// <summary>
            /// 麻醉药物名称
            /// </summary>
            public string anst_medn_name  { get; set; }
            /// <summary>
            /// 麻醉药物剂量
            /// </summary>
            public string anst_medn_dos { get; set; }
            /// <summary>
            /// 计量单位
            /// </summary>
            public string anst_dosunt { get; set; }
            /// <summary>
            /// 麻醉开始时间
            /// </summary>
            public string anst_begntime { get; set; }
            /// <summary>
            /// 麻醉结束时间
            /// </summary>
            public string anst_endtime { get; set; }
            /// <summary>
            /// 麻醉合并症代码
            /// </summary>
            public string anst_merg_symp_code { get; set; }
            /// <summary>
            /// 麻醉合并症名称
            /// </summary>
            public string anst_merg_symp  { get; set; }
            /// <summary>
            /// 麻醉合并症描述
            /// </summary>
            public string anst_merg_symp_dscr { get; set; }
            /// <summary>
            /// 入复苏室时间
            /// </summary>
            public string pacu_begntime { get; set; }
            /// <summary>
            /// 出复苏室时间
            /// </summary>
            public string pacu_endtime { get; set; }
            /// <summary>
            /// 是否择期手术
            /// </summary>
            public string oprn_selv { get; set; }
            /// <summary>
            /// 是否择取消手术 
            /// </summary>
            public string canc_oprn { get; set; }
            /// <summary>
            /// 有效标志 Y
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 病情抢救记录类
        /// </summary>
        public class Rescinfo
        {
            /// <summary>
            /// 科室代码
            /// </summary>
            public string dept { get; set; }
            /// <summary>
            /// 科室名称
            /// </summary>
            public string dept_name { get; set; }
            /// <summary>
            /// 病区名称
            /// </summary>
            public string wardarea_name { get; set; }
            /// <summary>
            /// 病床号
            /// </summary>
            public string bedno { get; set; }
            /// <summary>
            /// 疾病诊断名称
            /// </summary>
            public string diag_name { get; set; }
            /// <summary>
            /// 疾病诊断编码
            /// </summary>
            public string diag_code { get; set; }
            /// <summary>
            /// 疾病诊断名称
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 疾病诊断编码
            /// </summary>
            public string dise_code { get; set; }
            /// <summary>
            /// 病情变化情况
            /// </summary>
            public string cond_chg { get; set; }
            /// <summary>
            /// 抢救措施
            /// </summary>
            public string resc_mes { get; set; }
            /// <summary>
            /// 手术及操作编码
            /// </summary>
            public string oprn_oprt_code  { get; set; }
            /// <summary>
            /// 手术及操作名称
            /// </summary>
            public string oprn_oprt_name  { get; set; }
            /// <summary>
            /// 手术及操作目标部位名称 
            /// </summary>
            public string oprn_oper_part { get; set; }
            /// <summary>
            /// 介入物名称
            /// </summary>
            public string itvt_name { get; set; }
            /// <summary>
            /// 操作方法 
            /// </summary>
            public string oprt_mtd { get; set; }
            /// <summary>
            /// 操作次数
            /// </summary>
            public string oprt_cnt { get; set; }
            /// <summary>
            /// 抢救开始日期时间
            /// </summary>
            public string resc_begntime { get; set; }
            /// <summary>
            /// 抢救结束日期时间
            /// </summary>
            public string resc_endtime { get; set; }
            /// <summary>
            /// 检查/检验项目名称 
            /// </summary>
            public string dise_item_name  { get; set; }
            /// <summary>
            /// 检查/检验结果 
            /// </summary>
            public string dise_ccls { get; set; }
            /// <summary>
            /// 检查/检验定量结果 
            /// </summary>
            public string dise_ccls_qunt { get; set; }
            /// <summary>
            /// 检查/检验结果代码
            /// </summary>
            public string dise_ccls_code  { get; set; }
            /// <summary>
            /// 注意事项
            /// </summary>
            public string mnan { get; set; }
            /// <summary>
            /// 参加抢救人员名单
            /// </summary>
            public string resc_psn_list { get; set; }
            /// <summary>
            /// 专业技术职务类别代码 
            /// </summary>
            public string proftechttl_code  { get; set; }
            /// <summary>
            /// 医师编号
            /// </summary>
            public string doc_code { get; set; }
            /// <summary>
            /// 医师姓名
            /// </summary>
            public string dr_name { get; set; }
            /// <summary>
            /// 有效标志
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 死亡信息类
        /// </summary>
        public class Dieinfo
        {/// <summary>
            /// 科室代码
            /// </summary>
            public string dept { get; set; }
            /// <summary>
            /// 科室名称
            /// </summary>
            public string dept_name { get; set; }
            /// <summary>
            /// 病区名称
            /// </summary>
            public string wardarea_name { get; set; }
            /// <summary>
            /// 病床号
            /// </summary>
            public string bedno { get; set; }
            /// <summary>
            /// 入院日期时间
            /// </summary>
            public string adm_time { get; set; }
            /// <summary>
            /// 入院诊断编码
            /// </summary>
            public string adm_dise { get; set; }
            /// <summary>
            /// 入院情况 
            /// </summary>
            public string adm_info { get; set; }
            /// <summary>
            /// 诊疗过程描述
            /// </summary>
            public string trt_proc_dscr { get; set; }
            /// <summary>
            /// 死亡日期时间
            /// </summary>
            public string die_time { get; set; }
            /// <summary>
            /// 直接死亡原因名称
            /// </summary>
            public string die_drt_rea { get; set; }
            /// <summary>
            /// 直接死亡原因编码
            /// </summary>
            public string die_drt_rea_code { get; set; }
            /// <summary>
            /// 死亡诊断名称
            /// </summary>
            public string die_dise_name { get; set; }
            /// <summary>
            /// 死亡诊断编码
            /// </summary>
            public string die_diag_code { get; set; }
            /// <summary>
            /// 家属是否同意尸体解剖标志 
            /// </summary>
            public string agre_corp_dset { get; set; }
            /// <summary>
            /// 住院医师姓名
            /// </summary>
            public string ipdr_name { get; set; }
            /// <summary>
            /// 主治医师编号
            /// </summary>
            public string atddr_code { get; set; }
            /// <summary>
            /// 主治医师姓名
            /// </summary>
            public string atddr_name { get; set; }
            /// <summary>
            /// 主诊医师代码
            /// </summary>
            public string chfpdr_code { get; set; }
            /// <summary>
            /// 主诊医师姓名
            /// </summary>
            public string chfpdr_name { get; set; }
            /// <summary>
            /// 主任医师姓名
            /// </summary>
            public string chfdr_name { get; set; }
            /// <summary>
            /// 签字日期时间
            /// </summary>
            public string sign_time { get; set; }
            /// <summary>
            /// 有效标志
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 出院小结类
        /// </summary>
        public class Dscginfo
        {
            /// <summary>
            /// 出院日期
            /// </summary>
            public string dscg_date { get; set; }
            /// <summary>
            /// 入院（初步）诊断
            /// </summary>
            public string adm_dise_dscr { get; set; }
            /// <summary>
            /// 出院诊断
            /// </summary>
            public string dscg_dise_dscr  { get; set; }
            /// <summary>
            /// 入院情况
            /// </summary>
            public string adm_info { get; set; }
            /// <summary>
            /// 诊治经过及结果（含手术日期名称及结果）
            /// </summary>
            public string trt_proc_rslt_dscr { get; set; }
            /// <summary>
            /// 出院情况（含治疗效果）
            /// </summary>
            public string dscg_info { get; set; }
            /// <summary>
            /// 出院医嘱
            /// </summary>
            public string dscg_drord { get; set; }
            /// <summary>
            /// 科别
            /// </summary>
            public string dept { get; set; }
            /// <summary>
            /// 记录医师 
            /// </summary>
            public string rec_doc { get; set; }
            /// <summary>
            /// 主要药品名称
            /// </summary>
            public string main_drug_name { get; set; }
            /// <summary>
            /// 其他重要信息
            /// </summary>
            public string oth_imp_info { get; set; }
            /// <summary>
            /// 有效标志
            /// </summary>
            public string vali_flag { get; set; }
            /// <summary>
            /// 科别
            /// </summary>
            public string caty { get; set; }
        }
        #endregion 
    }
}
