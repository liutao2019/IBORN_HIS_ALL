using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 医生变更
    /// </summary>
    public class RequestGzsiModel3468
    {
        public Doctorinfo doctorinfo { get; set; }

        public class Doctorinfo
        {
            /// <summary> 
            /// 医师编码 Y 
            /// </summary>
            public string dr_codg { get; set; }
            /// <summary> 
            /// 医师姓名 Y 
            /// </summary>
            public string dr_name { get; set; }
            /// <summary> 
            /// 执业医师编号 
            /// </summary>
            public string prac_dr_id { get; set; }
            /// <summary> 
            /// 医师执业类别 
            /// </summary>
            public string dr_prac_type { get; set; }
            /// <summary> 
            /// 医师专业技术职务 
            /// </summary>
            public string dr_pro_tech_duty { get; set; }
            /// <summary> 
            /// 医师执业范围代码 
            /// </summary>
            public string dr_prac_scp_code { get; set; }
            /// <summary> 
            /// 专业编号 
            /// </summary>
            public string pro_code { get; set; }
            /// <summary> 
            /// 是否申报为本市专家库成员 
            /// </summary>
            public string dcl_prof_flag { get; set; }
            /// <summary> 
            /// 医师执业情况 
            /// </summary>
            public string practice_code { get; set; }
            /// <summary> 
            /// 医师职称编号 
            /// </summary>
            public string dr_profttl_code { get; set; }
            /// <summary> 
            /// 个人能力简介 
            /// </summary>
            public string psn_itro { get; set; }
            /// <summary> 
            /// 多点执业标志 Y
            /// </summary>
            public string mul_prac_flag { get; set; }
            /// <summary> 
            /// 主执业机构标志 Y
            /// </summary>
            public string main_pracins_flag { get; set; }
            /// <summary> 
            /// 医院科室编码 Y
            /// </summary>
            public string hosp_dept_code { get; set; }
            /// <summary> 
            /// 定岗标志 
            /// </summary>
            public string bind_flag { get; set; }
            /// <summary> 
            /// 是否医保专家库成员 
            /// </summary>
            public string siprof_flag { get; set; }
            /// <summary> 
            /// 是否本地申报专家 
            /// </summary>
            public string loclprof_flag { get; set; }
            /// <summary> 
            /// 是否医保医师 
            /// </summary>
            public string hi_dr_flag { get; set; }
            /// <summary> 
            /// 证件类型 
            /// </summary>
            public string cert_type { get; set; }
            /// <summary> 
            /// 证件号码 
            /// </summary>
            public string certno { get; set; }
            /// <summary> 
            /// 备注 
            /// </summary>
            public string memo { get; set; }

            public List<Mgtinfo> mgtinfo { get; set; }
        }

        public class Mgtinfo
        {
            /// </summary>
            /// 医师编码 
            /// </summary>
            public string dr_codg { get; set; }
            /// <summary>
            /// 医疗服务类型
            /// </summary>
            public string hi_serv_type { get; set; }
            /// 医疗服务类型名称 
            /// </summary>
            public string hi_serv_name { get; set; }
            /// <summary>
            /// 服务状态
            /// </summary>
            public string hi_serv_stas { get; set; }
            /// 医疗服务开始时间 
            /// </summary>
            public string begndate { get; set; }
            /// <summary>
            /// 医疗服务结束时间
            /// </summary>
            public string enddate { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string memo { get; set; }
        }
    }
}
