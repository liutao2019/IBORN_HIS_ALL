using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 结算清单上传实体
    /// </summary>
    public class RequestGzsiModel4101
    {
        /// <summary>
        /// 结算清单信息
        /// </summary>
        public Setlinfo setlinfo { get; set; }
        /// <summary>
        /// 基金支付信息
        /// </summary>
        public List<Payinfo> payinfo { get; set; }
        /// <summary>
        /// 门诊慢特病诊断信息
        /// </summary>
        public List<Opspdiseinfo> opspdiseinfo { get; set; }
        /// <summary>
        /// 住院诊断信息
        /// </summary>
        public List<Diseinfo> diseinfo { get; set; }
        /// <summary>
        /// 收费项目信息
        /// </summary>
        public List<Iteminfo> iteminfo { get; set; }
        /// <summary>
        /// 手术操作信息
        /// </summary>
        public List<Oprninfo> oprninfo { get; set; }
        /// <summary>
        /// 重症监护信息
        /// </summary>
        public List<Icuinfo> icuinfo { get; set; }

        /// <summary>
        /// 结算清单信息类
        /// </summary>
        public class Setlinfo
        {
            public string mdtrt_id {get;set;}  //1 就诊ID   Y
            public string setl_id {get;set;}  //2 结算ID   Y
            public string fixmedins_name {get;set;}  //3 定点医药机构名称   Y
            public string fixmedins_code {get;set;}  //4 定点医药机构编号   Y
            public string hi_setl_lv {get;set;}  //5 医保结算等级   
            public string hi_no {get;set;}  //6 医保编号   
            public string medcasno {get;set;}  //7 病案号   Y
            public string dcla_time {get;set;}  //8 申报时间   
            public string psn_name {get;set;}  //9 人员姓名   Y
            public string gend {get;set;}  //10 性别   Y
            public string brdy {get;set;}  //11 出生日期   Y　
            public string age {get;set;}  //12 年龄   
            public string ntly {get;set;}  //13 国籍   Y　
            public string nwb_age {get;set;}  //14 （年龄不足1周岁）年龄   
            public string naty {get;set;}  //15 民族   Y　
            public string patn_cert_type {get;set;}  //16 患者证件类别   Y
            public string certno {get;set;}  //17 证件号码   Y
            public string prfs { get; set; }  //18 职业   Y　
            public string curr_addr {get;set;}  //19 现住址   
            public string emp_name {get;set;}  //20 单位名称   
            public string emp_addr {get;set;}  //21 单位地址   
            public string emp_tel {get;set;}  //22 单位电话   
            public string poscode {get;set;}  //23 邮编   
            public string coner_name {get;set;}  //24 联系人姓名   Y　
            public string patn_rlts {get;set;}  //25 与患者关系   Y　
            public string coner_addr {get;set;}  //26 联系人地址   Y　
            public string coner_tel {get;set;}  //27 联系人电话   Y　
            public string hi_type {get;set;}  //28 医保类型   Y　
            public string insuplc {get;set;}  //29 参保地   Y　
            public string sp_psn_type {get;set;}  //30 特殊人员类型   
            public string nwb_adm_type {get;set;}  //31 新生儿入院类型   
            public string nwb_bir_wt {get;set;}  //32 新生儿出生体重   
            public string nwb_adm_wt {get;set;}  //33 新生儿入院体重   
            public string opsp_diag_caty {get;set;}  //34 门诊慢特病诊断科别   
            public string opsp_mdtrt_date {get;set;}  //35 门诊慢特病就诊日期   
            public string ipt_med_type {get;set;}  //36 住院医疗类型   Y　
            public string adm_way {get;set;}  //37 入院途径   
            public string trt_type {get;set;}  //38 治疗类别   
            public string adm_time {get;set;}  //39 入院时间   
            public string adm_caty {get;set;}  //40 入院科别   Y　
            public string refldept_dept {get;set;}  //41 转科科别   
            public string dscg_time {get;set;}  //42 出院时间   
            public string dscg_caty {get;set;}  //43 出院科别   Y　
            public string act_ipt_days {get;set;}  //44 实际住院天数   
            public string otp_wm_dise {get;set;}  //45 门（急）诊西医诊断   
            public string wm_dise_code {get;set;}  //46 西医诊断疾病代码   
            public string otp_tcm_dise {get;set;}  //47 门（急）诊中医诊断   
            public string tcm_dise_code {get;set;}  //48 中医诊断代码   
            public string diag_code_cnt {get;set;}  //49 诊断代码计数   
            public string oprn_oprt_code_cnt {get;set;}  //50 手术操作代码计数   
            public string vent_used_dura {get;set;}  //51 呼吸机使用时长   
            public string pwcry_bfadm_coma_dura {get;set;}  //52 颅脑损伤患者入院前昏迷时长 
            public string pwcry_afadm_coma_dura {get;set;}  //53 颅脑损伤患者入院后昏迷时长   
            public string bld_cat {get;set;}  //54 输血品种   
            public string bld_amt {get;set;}  //55 输血量   
            public string bld_unt {get;set;}  //56 输血计量单位   
            public string spga_nurscare_days {get;set;}  //57 特级护理天数   
            public string lv1_nurscare_days {get;set;}  //58 一级护理天数   
            public string scd_nurscare_days {get;set;}  //59 二级护理天数   
            public string lv3_nurscare_days {get;set;}  //60 三级护理天数   
            public string dscg_way {get;set;}  //61 离院方式   
            public string acp_medins_name {get;set;}  //62 拟接收机构名称    
            public string acp_optins_code {get;set;}  //63 拟接收机构代码   
            public string bill_code {get;set;}  //64 票据代码   Y
            public string bill_no {get;set;}  //65 票据号码   Y
            public string biz_sn {get;set;}  //66 业务流水号   Y
            public string days_rinp_flag_31 {get;set;}  //67 出院31天内再住院计划标志   
            public string days_rinp_pup_31 {get;set;}  //68 出院31天内再住院目的   
            public string chfpdr_name {get;set;}  //69 主诊医师姓名   
            public string chfpdr_code {get;set;}  //70 主诊医师代码   
            public string setl_begn_date {get;set;}  //71 结算开始日期   Y　
            public string setl_end_date {get;set;}  //72 结算结束日期   Y　
            public string psn_selfpay {get;set;}  //73 个人自付   Y　
            public string psn_ownpay {get;set;}  //74 个人自费   Y　
            public string acct_pay {get;set;}  //75 个人账户支出   Y　
            public string psn_cashpay {get;set;}  //76 个人现金支付   Y　
            public string hi_paymtd {get;set;}  //77 医保支付方式   Y　
            public string hsorg {get;set;}  //78 医保机构   Y
            public string hsorg_opter {get;set;}  //79 医保机构经办人   Y
            public string medins_fill_dept {get;set;}  //80 医疗机构填报部门   Y　
            public string medins_fill_psn { get; set; }  //81 医疗机构填报人   Y　
            public string prfs_name { get; set; }  //82 职业   　
        }

        /// <summary>
        /// 基金支付信息
        /// </summary>
        public class Payinfo
        {
            public string fund_pay_type { get; set; }  //1 基金支付类型   Y
            public string fund_payamt { get; set; }  //2 基金支付金额   Y
        }

        /// <summary>
        /// 门诊慢特病诊断信息
        /// </summary>
        public class Opspdiseinfo
        {
            public string diag_name { get; set; }  //1    诊断名称   Y
            public string diag_code { get; set; }  //2    诊断代码   Y
            public string oprn_oprt_name { get; set; }  //3    手术操作名称   
            public string oprn_oprt_code { get; set; }  //4    手术操作代码   

        }

        /// <summary>
        /// 住院诊断信息
        /// </summary>
        public class Diseinfo
        {
            public string maindiag_flag {get;set;} //是否主诊断 Y
            public string diag_type { get; set; }  //1 诊断类别   　Y
            public string diag_code { get; set; }  //2 诊断代码   　Y
            public string diag_name { get; set; }  //3 诊断名称   　Y
            public string adm_cond_type { get; set; }  //4 入院病情类型   
        }

        /// <summary>
        /// 收费项目信息
        /// </summary>
        public class Iteminfo
        {
            public string med_chrgitm { get; set; }  //1    医疗收费项目   Y
            public string amt { get; set; }  //2    金额   Y
            public string claa_sumfee { get; set; }  //3    甲类费用合计   Y
            public string clab_amt { get; set; }  //4    乙类金额   Y
            public string fulamt_ownpay_amt { get; set; }  //5    全自费金额   Y
            public string oth_amt { get; set; }  //6    其他金额   Y

        }

        /// <summary>
        /// 手术操作信息
        /// </summary>
        public class Oprninfo
        {
            public string oprn_oprt_type { get; set; }  //1 手术操作类别   Y
            public string oprn_oprt_name { get; set; }  //2 手术操作名称   Y
            public string oprn_oprt_code { get; set; }  //3 手术操作代码   Y
            public string oprn_oprt_date { get; set; }  //4 手术操作日期   Y
            public string anst_way { get; set; }  //5 麻醉方式   
            public string oper_dr_name { get; set; }  //6 术者医师姓名   Y
            public string oper_dr_code { get; set; }  //7 术者医师代码   Y
            public string anst_dr_name { get; set; }  //8 麻醉医师姓名   
            public string anst_dr_code { get; set; }  //9 麻醉医师代码   
        }

        /// <summary>
        /// 重症监护信息
        /// </summary>
        public class Icuinfo
        {
            public string scs_cutd_ward_type { get; set; }  //1    重症监护病房类型   Y
            public string scs_cutd_inpool_time { get; set; }  //2    重症监护进入时间   Y
            public string scs_cutd_exit_time { get; set; }  //3    重症监护退出时间   Y
            public string scs_cutd_sum_dura { get; set; }  //4    重症监护合计时长   Y
        }
    }
}
