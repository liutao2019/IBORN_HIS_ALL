using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 入院办理
    /// </summary>
    public class RequestGzsiModel2401
    {
        public Mdtrtinfo mdtrtinfo { get; set; }
        public Agnterinfo agnterinfo { get; set; }
        public List<Diseinfo> diseinfo { get; set; }

        public class Mdtrtinfo
        {
            /// <summary>
            ///人员编号 Y 
            /// </summary>
            public string psn_no { get; set; }

            /// <summary>
            ///险种类型 Y 见【4码表说明】
            /// </summary>
            public string insutype { get; set; }

            /// <summary>
            ///联系人姓名  
            /// </summary>
            public string coner_name { get; set; }

            /// <summary>
            ///联系电话  
            /// </summary>
            public string tel { get; set; }

            /// <summary>
            ///开始时间 Y 入院时间 格式：yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string begntime { get; set; }

            /// <summary>
            ///就诊凭证类型 Y 见【4码表说明】
            /// </summary>
            public string mdtrt_cert_type { get; set; }

            /// <summary>
            ///就诊凭证编号  就诊凭证类型为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// </summary>
            public string mdtrt_cert_no { get; set; }

            /// <summary>
            ///持卡就诊基本信息  
            /// </summary>
            public string hcard_basinfo { get; set; }

            /// <summary>
            ///持卡就诊校验信息  
            /// </summary>
            public string hcard_chkinfo { get; set; }

            /// <summary>
            ///医疗类别 Y 见【4码表说明】
            /// </summary>
            public string med_type { get; set; }

            /// <summary>
            ///住院/门诊号 Y 
            /// </summary>
            public string ipt_otp_no { get; set; }

            /// <summary>
            ///病历号  
            /// </summary>
            public string medrcdno { get; set; }

            /// <summary>
            ///主治医生编码 Y 
            /// </summary>
            public string atddr_no { get; set; }

            /// <summary>
            ///主治医师姓名 Y 
            /// <summary>
            public string chfpdr_name { get; set; }

            /// <summary>
            ///入院诊断描述 Y 
            /// </summary>
            public string adm_diag_dscr { get; set; }

            /// <summary>
            ///入院科室编码 Y 
            /// </summary>
            public string adm_dept_codg { get; set; }

            /// <summary>
            ///入院科室名称 Y 
            /// </summary>
            public string adm_dept_name { get; set; }

            /// <summary>
            ///入院床位 Y 
            /// </summary>
            public string adm_bed { get; set; }

            /// <summary>
            ///住院主诊断代码 Y 
            /// </summary>
            public string dscg_maindiag_code { get; set; }

            /// <summary>
            ///住院主诊断名称 Y 
            /// </summary>
            public string dscg_maindiag_name { get; set; }

            /// <summary>
            ///主要病情描述  
            /// </summary>
            public string main_cond_desc { get; set; }

            /// <summary>
            ///临床试验标志  1-是 0-否
            /// </summary>
            public string clnc_flag { get; set; }

            /// <summary>
            ///急诊标志  1-是 0-否
            /// </summary>
            public string er_flag { get; set; }

            /// <summary>
            ///病种编码  按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_code)、特殊待遇病种目录代码
            /// </summary>
            public string dise_codg { get; set; }

            /// <summary>
            ///病种名称  
            /// </summary>
            public string dise_name { get; set; }

            /// <summary>
            ///手术操作代码  
            /// </summary>
            public string oprn_oprt_code { get; set; }

            /// <summary>
            ///手术操作名称  
            /// </summary>
            public string oprn_oprt_name { get; set; }

            /// <summary>
            ///计划生育服务证号  
            /// </summary>
            public string fpsc_no { get; set; }

            /// <summary>
            ///生育类别  见【4码表说明】
            /// </summary>
            public string matn_type { get; set; }

            /// <summary>
            ///计划生育手术类别  见【4码表说明】
            /// </summary>
            public string birctrl_type { get; set; }

            /// <summary>
            ///晚育标志  1-是 0-否
            /// </summary>
            public string latechb_flag { get; set; }

            /// <summary>
            ///孕周数  
            /// </summary>
            public string geso_val { get; set; }

            /// <summary>
            ///胎次  
            /// </summary>
            public string fetts { get; set; }

            /// <summary>
            ///胎儿数  
            /// </summary>
            public string fetus_cnt { get; set; }

            /// <summary>
            ///早产标志  1-是 0-否
            /// </summary>
            public string pret_flag { get; set; }

            /// <summary>
            ///计划生育手术或生育日期  格式：yyyy-MM-dd
            /// </summary>
            public string birctrl_matn_date { get; set; }

            /// <summary>
            ///预付款  
            /// </summary>
            public string advpay { get; set; }
            /// <summary>
            ///重复住院标志  字符型	3	Y Y	1：是，0：否  
            /// </summary>
            public string repeat_ipt_flag { get; set; }
            /// <summary>
            ///是否第三方责任标志 字符型	6		Y	0-否，1-是  
            /// </summary>
            public string ttp_resp { get; set; }
            /// <summary>
            ///合并结算标志 字符型	6		Y	0-否，1-是  
            /// </summary>
            public string merg_setl_flag { get; set; }
        }

        public class Diseinfo
        {
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 诊断类别
            /// </summary>
            public string diag_type { get; set; }
            /// <summary>
            /// 主诊断标志
            /// </summary>
            public string maindiag_flag { get; set; }
            /// <summary>
            /// 诊断排序号
            /// </summary>
            public string diag_srt_no { get; set; }
            /// <summary>
            /// 诊断代码
            /// </summary>
            public string diag_code { get; set; }
            /// <summary>
            /// 诊断名称
            /// </summary>
            public string diag_name { get; set; }
            /// <summary>
            /// 入院病情
            /// </summary>
            public string adm_cond { get; set; }
            /// <summary>
            /// 诊断科室
            /// </summary>
            public string diag_dept { get; set; }
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
            public string diag_time { get; set; }
            /// <summary>
            /// 有效标志
            /// </summary>
            public string vali_flag { get; set; }
        }
    }
}
