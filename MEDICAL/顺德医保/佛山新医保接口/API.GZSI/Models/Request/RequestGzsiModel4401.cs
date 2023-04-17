using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 病案信息上传实体
    /// </summary>
    public class RequestGzsiModel4401
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public Baseinfo baseinfo { get; set; }
        /// <summary>
        /// 诊断信息
        /// </summary>
        public List<Diseinfo> diseinfo { get; set; }
        /// <summary>
        /// 手术信息
        /// </summary>
        public List<Oprninfo> oprninfo { get; set; }
        /// <summary>
        /// 重症监护信息
        /// </summary>
        public List<Icuinfo> icuinfo { get; set; }

        /// <summary>
        /// 病案首页基本信息类
        /// </summary>
        public class Baseinfo
        {
            /// <summary>
            /// 就医流水号
            /// </summary>
            public string mdtrt_sn { get; set; }
            /// <summary>
            /// 就诊 ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            ///人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 患者住院次数
            /// </summary>
            public string patn_ipt_cnt { get; set; }
            /// <summary>
            /// 住院/门诊号  
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 病案号 Y
            /// </summary>
            public string medcasno { get; set; }
            /// <summary>
            /// 人员姓名 Y
            /// </summary>
            public string psn_name { get; set; }
            /// <summary>
            /// 性别 
            /// </summary>
            public string gend { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public string brdy { get; set; }
            /// <summary>
            /// 国籍
            /// </summary>
            public string ntly { get; set; }
            /// <summary>
            /// 国籍名称
            /// </summary>
            public string ntly_name { get; set; }
            /// <summary>
            /// 新生儿出生体重（g）
            /// </summary>
            public string nwb_bir_wt { get; set; }
            /// <summary>
            ///  新生儿入院体重（g）
            /// </summary>
            public string nwb_adm_wt { get; set; }
            /// <summary>
            /// 出生地
            /// </summary>
            public string birplc { get; set; }
            /// <summary>
            /// 籍贯
            /// </summary>
            public string napl { get; set; }
            /// <summary>
            /// 民族名称
            /// </summary>
            public string naty_name { get; set; }
            /// <summary>
            /// 民族
            /// </summary>
            public string naty { get; set; }
            /// <summary>
            /// 证件号码
            /// </summary>
            public string certno { get; set; }
            /// <summary>
            /// 职业
            /// </summary>
            public string prfs { get; set; }
            /// <summary>
            /// 婚姻状况类别名称
            /// </summary>
            public string mrg_name { get; set; }
            /// <summary>
            /// 婚姻状况类别代码
            /// </summary>
            public string mrg_stas { get; set; }
            /// <summary>
            /// 现住址-邮政编码
            /// </summary>
            public string curr_addr_poscode { get; set; }
            /// <summary>
            /// 现住址
            /// </summary>
            public string curr_addr { get; set; }
            /// <summary>
            /// 个人联系电话号码
            /// </summary>
            public string psn_tel { get; set; }
            /// <summary>
            /// 户口地址- 省（自治区、直辖 市） 
            /// </summary>
            public string resd_addr_prov  { get; set; }
            /// <summary>
            /// 户口地址-市（地区） 
            /// </summary>
            public string resd_addr_city { get; set; }
            /// <summary>
            /// 户口地址- 县（区）
            /// </summary>
            public string resd_addr_coty { get; set; }
            /// <summary>
            /// 户口地址- 乡（镇、街道办事处）
            /// </summary>
            public string resd_addr_subd { get; set; }
            /// <summary>
            /// 户口地址-村（街、路、弄等） 
            /// </summary>
            public string resd_addr_vil { get; set; }
            /// <summary>
            /// 户口地址-门牌号码 
            /// </summary>
            public string resd_addr_housnum { get; set; }
            /// <summary>
            /// 户口地址- 邮政编码 
            /// </summary>
            public string resd_addr_poscode { get; set; }
            /// <summary>
            /// 户口地址
            /// </summary>
            public string resd_addr { get; set; }
            /// <summary>
            /// 工作单位联系电话
            /// </summary>
            public string empr_tel { get; set; }
            /// <summary>
            /// 工作单位- 邮政编码 
            /// </summary>
            public string empr_poscode { get; set; }
            /// <summary>
            /// 工作单位及地址
            /// </summary>
            public string empr_addr { get; set; }
            /// <summary>
            /// 联系人电话-号码
            /// </summary>
            public string coner_tel { get; set; }
            /// <summary>
            /// 联系人姓名
            /// </summary>
            public string coner_name { get; set; }
            /// <summary>
            /// 联系人地址
            /// </summary>
            public string coner_addr { get; set; }
            /// <summary>
            /// 与联系人关系代码
            /// </summary>
            public string coner_rlts_code  { get; set; }
            /// <summary>
            /// 入院途径名称
            /// </summary>
            public string adm_way_name { get; set; }
            /// <summary>
            /// 入院途径
            /// </summary>
            public string adm_way_code { get; set; }
            /// <summary>
            /// 治疗类别名称
            /// </summary>
            public string trt_type_name { get; set; }
            /// <summary>
            /// 治疗类别
            /// </summary>
            public string trt_type { get; set; }
            /// <summary>
            /// 入院科别
            /// </summary>
            public string adm_caty { get; set; }
            /// <summary>
            /// 入院病房
            /// </summary>
            public string adm_ward { get; set; }
            /// <summary>
            /// 入院日期
            /// </summary>
            public string adm_date { get; set; }
            /// <summary>
            /// 出院日期
            /// </summary>
            public string dscg_date { get; set; }
            /// <summary>
            /// 出院科别
            /// </summary>
            public string dscg_caty { get; set; }
            /// <summary>
            /// 转科科别名称
            /// </summary>
            public string refldept_caty_name { get; set; }
            /// <summary>
            /// 出院病房
            /// </summary>
            public string dscg_ward { get; set; }
            /// <summary>
            /// 住院天数
            /// </summary>
            public string ipt_days { get; set; }
            /// <summary>
            /// 药物过敏标志
            /// </summary>
            public string drug_dicm_flag  { get; set; }
            /// <summary>
            /// 过敏药物名称
            /// </summary>
            public string dicm_drug_name { get; set; }
            /// <summary>
            /// 死亡患者尸检标志
            /// </summary>
            public string die_autp_flag { get; set; }
            /// <summary>
            /// ABO 血型代码
            /// </summary>
            public string abo_code { get; set; }
            /// <summary>
            /// ABO 血型
            /// </summary>
            public string abo_name { get; set; }
            /// <summary>
            /// Rh 血型代码 
            /// </summary>
            public string rh_code { get; set; }
            /// <summary>
            /// RH 血型 
            /// </summary>
            public string rh_name { get; set; }
            /// <summary>
            /// 死亡标识
            /// </summary>
            public string die_code { get; set; }
            /// <summary>
            /// 科主任姓名
            /// </summary>
            public string deptdrt_name { get; set; }
            /// <summary>
            /// 主任( 副主任)医师姓名 
            /// </summary>
            public string chfdr_Name { get; set; }
            /// <summary>
            /// 主治医生姓名
            /// </summary>
            public string atddr_name { get; set; }
            /// <summary>
            /// 主诊医师姓名
            /// </summary>
            public string chfpdr_name { get; set; }
            /// <summary>
            /// 住院医师姓名
            /// </summary>
            public string ipt_dr_name { get; set; }
            /// <summary>
            /// 责任护士姓名
            /// </summary>
            public string resp_nurs_name  { get; set; }
            /// <summary>
            /// 进修医师姓名
            /// </summary>
            public string train_dr_name { get; set; }
            /// <summary>
            /// 实习医师姓名
            /// </summary>
            public string intn_dr_name { get; set; }
            /// <summary>
            /// 编码员姓名
            /// </summary>
            public string codr_name { get; set; }
            /// <summary>
            /// 质控医师姓名
            /// </summary>
            public string qltctrl_dr_name  { get; set; }
            /// <summary>
            /// 质控护士姓名
            /// </summary>
            public string qltctrl_nurs_name { get; set; }
            /// <summary>
            /// 住院病例病案质量名称
            /// </summary>
            public string medcas_qlt_name  { get; set; }
            /// <summary>
            /// 住院病例病案质量代码 
            /// </summary>
            public string medcas_qlt_code  { get; set; }
            /// <summary>
            /// 质控日期
            /// </summary>
            public string medcas_qlt_date { get; set; }
            /// <summary>
            /// 离院方式名称
            /// </summary>
            public string dscg_way_name { get; set; }
            /// <summary>
            /// 离院方式
            /// </summary>
            public string dscg_way { get; set; }
            /// <summary>
            /// 拟接收医疗机构代码
            /// </summary>
            public string acp_medins_code { get; set; }
            /// <summary>
            /// 拟接收医疗机构名称
            /// </summary>
            public string acp_medins_name { get; set; }
            /// <summary>
            /// 是否有出院31天内再住院计划 
            /// </summary>
            public string dscg_31days_rinp_flag { get; set; }
            /// <summary>
            /// 再住院计划目的
            /// </summary>
            public string dscg_31days_rinp_pup { get; set; }
            /// <summary>
            /// 损伤、中毒的外部原因 
            /// </summary>
            public string damg_intx_ext_rea { get; set; }
            /// <summary>
            /// 损伤、中毒的外部原因疾病编码 
            /// </summary>
            public string damg_intx_ext_rea_disecode { get; set; }
            /// <summary>
            /// 颅脑损伤患者入院前昏迷时间 
            /// </summary>
            public string brn_damg_bfadm_coma_dura { get; set; }
            /// <summary>
            /// 颅脑损伤患者入院后昏迷时间
            /// </summary>
            public string brn_damg_afadm_coma_dura { get; set; }
            /// <summary>
            /// 呼吸机使用时间
            /// </summary>
            public string vent_used_time  { get; set; }
            /// <summary>
            /// 确诊日期
            /// </summary>
            public string cnfm_date { get; set; }
            /// <summary>
            /// 患者疾病诊断对照
            /// </summary>
            public string patn_dise_diag_crsp  { get; set; }
            /// <summary>
            /// 住院患者疾病诊断对照代码 
            /// </summary>
            public string patn_dise_diag_crsp_code { get; set; }
            /// <summary>
            /// 住院患者诊断符合情况  
            /// </summary>
            public string ipt_patn_diag_inscp { get; set; }
            /// <summary>
            /// 住院患者诊断符合情况代码 
            /// </summary>
            public string ipt_patn_diag_inscp_code  { get; set; }
            /// <summary>
            /// 出院治疗结果
            /// </summary>
            public string dscg_trt_rslt { get; set; }
            /// <summary>
            /// 出院治疗结果代码
            /// </summary>
            public string dscg_trt_rslt_code  { get; set; }
            /// <summary>
            /// 医疗机构组织机构代码 
            /// </summary>
            public string medins_orgcode { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public string age { get; set; }
            /// <summary>
            /// 过敏源
            /// </summary>
            public string aise { get; set; }
            /// <summary>
            /// 研究生实习医师
            /// </summary>
            public string pote_intn_dr_name  { get; set; }
            /// <summary>
            /// 乙肝表面抗原（HBsAg） 
            /// </summary>
            public string hbsag { get; set; }
            /// <summary>
            /// 丙型肝炎抗体 （HCV-Ab）
            /// </summary>
            public string hcv_ab  { get; set; }
            /// <summary>
            /// 艾滋病毒抗体（hiv-ab）
            /// </summary>
            public string hiv_ab  { get; set; }
            /// <summary>
            /// 抢救次数
            /// </summary>
            public string resc_cnt { get; set; }
            /// <summary>
            /// 抢救成功次数
            /// </summary>
            public string resc_succ_cnt { get; set; }
            /// <summary>
            /// 手术、治疗、检查、诊断为本院第一例
            /// </summary>
            public string hosp_dise_fsttime { get; set; }
            /// <summary>
            /// 医保付费方式名称
            /// </summary>
            public string hif_pay_way_name { get; set; }
            /// <summary>
            /// 医保付费方式代码
            /// </summary>
            public string hif_pay_way_code { get; set; }
            /// <summary>
            /// 医疗费用支付方式
            /// </summary>
            public string med_fee_paymtd_name   { get; set; }
            /// <summary>
            /// 医疗费用支付方式代码
            /// </summary>
            public string medfee_paymtd_code { get; set; }
            /// <summary>
            /// 自付金额
            /// </summary>
            public string selfpay_amt { get; set; }
            /// <summary>
            /// 总费用
            /// </summary>
            public string medfee_sumamt { get; set; }
            /// <summary>
            /// 一般医疗服务费
            /// </summary>
            public string ordn_med_servfee { get; set; }
            /// <summary>
            /// 一般治疗操作费
            /// </summary>
            public string ordn_trt_oprt_fee { get; set; }
            /// <summary>
            /// 护理费
            /// </summary>
            public string nurs_fee { get; set; }
            /// <summary>
            /// 综合医疗服务类其他费用 
            /// </summary>
            public string com_med_serv_oth_fee { get; set; }
            /// <summary>
            /// 病理诊断费
            /// </summary>
            public string palg_diag_fee { get; set; }
            /// <summary>
            /// 实验室诊断费
            /// </summary>
            public string lab_diag_fee { get; set; }
            /// <summary>
            /// 影像学诊断费
            /// </summary>
            public string rdhy_diag_fee { get; set; }
            /// <summary>
            /// 临床诊断项目费
            /// </summary>
            public string clnc_dise_fee { get; set; }
            /// <summary>
            /// 非手术治疗项目费
            /// </summary>
            public string nsrgtrt_item_fee { get; set; }
            /// <summary>
            /// 临床物理治疗费
            /// </summary>
            public string clnc_phys_trt_fee  { get; set; }
            /// <summary>
            /// 手术治疗费
            /// </summary>
            public string rgtrt_trt_fee { get; set; }
            /// <summary>
            /// 麻醉费
            /// </summary>
            public string anst_fee { get; set; }
            /// <summary>
            /// 手术费
            /// </summary>
            public string rgtrt_fee { get; set; }
            /// <summary>
            /// 康复费
            /// </summary>
            public string rhab_fee { get; set; }
            /// <summary>
            /// 中医治疗费
            /// </summary>
            public string tcm_trt_fee { get; set; }
            /// <summary>
            /// 西药费
            /// </summary>
            public string wm_fee { get; set; }
            /// <summary>
            /// 抗菌药物费用
            /// </summary>
            public string abtl_medn_fee { get; set; }
            /// <summary>
            /// 中成药费
            /// </summary>
            public string tcmpat_fee { get; set; }
            /// <summary>
            /// 中草药费
            /// </summary>
            public string tcmherb_fee { get; set; }
            /// <summary>
            /// 血费
            /// </summary>
            public string blo_fee { get; set; }
            /// <summary>
            /// 白蛋白类制品费
            /// </summary>
            public string albu_fee { get; set; }
            /// <summary>
            /// 球蛋白类制品费
            /// </summary>
            public string glon_fee { get; set; }
            /// <summary>
            /// 凝血因子类制品费
            /// </summary>
            public string clotfac_fee { get; set; }
            /// <summary>
            /// 细胞因子类制品费
            /// </summary>
            public string cyki_fee { get; set; }
            /// <summary>
            /// 检查用一次性医用材料费
            /// </summary>
            public string exam_dspo_matl_fee { get; set; }
            /// <summary>
            /// 治疗用一次性医用材料费
            /// </summary>
            public string trt_dspo_matl_fee { get; set; }
            /// <summary>
            /// 手术用一次性医用材料费
            /// </summary>
            public string oprn_dspo_matl_fee { get; set; }
            /// <summary>
            /// 其他费
            /// </summary>
            public string oth_fee { get; set; }
            /// <summary>
            /// 有效标志 Y
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 病案首页诊断信息类
        /// </summary>
        public class Diseinfo
        {
            /// <summary>
            /// 病理号
            /// </summary>
            public string palg_no { get; set; }
            /// <summary>
            /// 住院患者疾病诊断类型代码 
            /// </summary>
            public string ipt_patn_disediag_type_code  { get; set; }
            /// <summary>
            /// 疾病诊断类型
            /// </summary>
            public string disediag_type { get; set; }
            /// <summary>
            /// 是否主要诊断
            /// </summary>
            public string maindiag_flag { get; set; }
            /// <summary>
            /// 疾病诊断代码
            /// </summary>
            public string diag_code { get; set; }
            /// <summary>
            /// 疾病诊断名称
            /// </summary>
            public string diag_name { get; set; }
            /// <summary>
            /// 院内疾病诊断代码
            /// </summary>
            public string inhosp_diag_code  { get; set; }
            /// <summary>
            /// 院内疾病诊断名称
            /// </summary>
            public string inhosp_diag_name  { get; set; }
            /// <summary>
            /// 入院疾病病情名称
            /// </summary>
            public string adm_dise_cond_name  { get; set; }
            /// <summary>
            /// 入院疾病病情代码
            /// </summary>
            public string adm_dise_cond_code  { get; set; }
            /// <summary>
            /// 入院时情况
            /// </summary>
            public string adm_cond { get; set; }
            /// <summary>
            /// 入院时情况代码
            /// </summary>
            public string adm_cond_code { get; set; }
            /// <summary>
            /// 最高诊断依据
            /// </summary>
            public string high_diag_evid  { get; set; }
            /// <summary>
            /// 分化程度
            /// </summary>
            public string bkup_deg { get; set; }
            /// <summary>
            /// 分化程度代码
            /// </summary>
            public string bkup_deg_code { get; set; }
            /// <summary>
            /// 有效标志 Y
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 病案首页手术信息类
        /// </summary>
        public class Oprninfo
        {
            /// <summary>
            /// 手术操作日期 
            /// </summary>
            public string oprn_oprt_date { get; set; }
            /// <summary>
            /// 手术/ 操作名称
            /// </summary>
            public string oprn_oprt_name  { get; set; }
            /// <summary>
            /// 手术/ 操作-代码
            /// </summary>
            public string oprn_oprt_code  { get; set; }
            /// <summary>
            /// 手术序列号
            /// </summary>
            public string oprn_oprt_sn { get; set; }
            /// <summary>
            /// 手术级别代码 
            /// </summary>
            public string oprn_lv_code { get; set; }
            /// <summary>
            /// 手术级别名称
            /// </summary>
            public string oprn_lv_name { get; set; }
            /// <summary>
            /// 手术者姓名
            /// </summary>
            public string oper_name { get; set; }
            /// <summary>
            /// 助手 I 姓名
            /// </summary>
            public string asit_1_name { get; set; }
            /// <summary>
            /// 助手 II 姓名
            /// </summary>
            public string asit_name2 { get; set; }
            /// <summary>
            /// 手术切口愈合等级
            /// </summary>
            public string sinc_heal_lv { get; set; }
            /// <summary>
            /// 手术切口愈合等级代码 
            /// </summary>
            public string sinc_heal_lv_code  { get; set; }
            /// <summary>
            /// 麻醉-方法名称 
            /// </summary>
            public string anst_mtd_name { get; set; }
            /// <summary>
            /// 麻醉-方法代码 
            /// </summary>
            public string anst_mtd_code { get; set; }
            /// <summary>
            /// 麻醉医师姓名 
            /// </summary>
            public string anst_dr_name { get; set; }
            /// <summary>
            /// 手术操作部位
            /// </summary>
            public string oprn_oper_part  { get; set; }
            /// <summary>
            /// 手术操作部位代码
            /// </summary>
            public string oprn_oper_part_code { get; set; }
            /// <summary>
            ///  手术持续时间
            /// </summary>
            public string oprn_con_time { get; set; }
            /// <summary>
            /// 麻醉分级名称 
            /// </summary>
            public string anst_lv_name { get; set; }
            /// <summary>
            /// 麻醉分级代码 
            /// </summary>
            public string anst_lv_code { get; set; }
            /// <summary>
            /// 手术患者类型 
            /// </summary>
            public string oprn_patn_type  { get; set; }
            /// <summary>
            /// 手术患者类型代码 
            /// </summary>
            public string oprn_patn_type_code { get; set; }
            /// <summary>
            /// 是否主要手术 
            /// </summary>
            public string main_oprn_flag  { get; set; }
            /// <summary>
            /// 麻醉 ASA 分级代码
            /// </summary>
            public string anst_asa_lv_code  { get; set; }
            /// <summary>
            /// 麻醉 ASA 分级名称
            /// </summary>
            public string anst_asa_lv_name  { get; set; }
            /// <summary>
            /// 麻醉药物代码
            /// </summary>
            public string anst_medn_code  { get; set; }
            /// <summary>
            /// 麻醉药物名称 
            /// </summary>
            public string anst_medn_name { get; set; }
            /// <summary>
            /// 麻醉药物剂量
            /// </summary>
            public string anst_medn_dos { get; set; }
            /// <summary>
            /// 计量单位
            /// </summary>
            public string unt { get; set; }
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
            public string anst_copn_code  { get; set; }
            /// <summary>
            /// 麻醉合并症名称 
            /// </summary>
            public string anst_copn_name  { get; set; }
            /// <summary>
            /// 麻醉合并症描述
            /// </summary>
            public string anst_copn_dscr { get; set; }
            /// <summary>
            /// 入复苏室时间
            /// </summary>
            public string pacu_begntime { get; set; }
            /// <summary>
            /// 出复苏室时间 
            /// </summary>
            public string pacu_endtime { get; set; }
            /// <summary>
            /// 是否择取消手术
            /// </summary>
            public string canc_oprn_flag  { get; set; }
            /// <summary>
            /// 有效标志 Y
            /// </summary>
            public string vali_flag { get; set; }
        }

        /// <summary>
        /// 病案首页重症监护信息类
        /// </summary>
        public class Icuinfo
        {
            /// <summary>
            /// 重症监护室代码
            /// </summary>
            public string icu_codeid { get; set; }
            /// <summary>
            /// 监护室进入日期时间 
            /// </summary>
            public string inpool_icu_time { get; set; }
            /// <summary>
            /// 监护室退出日期时间
            /// </summary>
            public string out_icu_time { get; set; }
            /// <summary>
            /// 医疗机构组织机构代码 
            /// </summary>
            public string medins_orgcode  { get; set; }
            /// <summary>
            /// 护理等级代码 
            /// </summary>
            public string nurscare_lv_code  { get; set; }
            /// <summary>
            /// 护理等级名称 
            /// </summary>
            public string nurscare_lv_name { get; set; }
            /// <summary>
            ///  护理天数 
            /// </summary>
            public string nurscare_days { get; set; }
            /// <summary>
            /// 是否重返重症监护室
            /// </summary>
            public string back_icu { get; set; }
            /// <summary>
            /// 有效标志 Y
            /// </summary>
            public string vali_flag { get; set; }
        }
    }
}
