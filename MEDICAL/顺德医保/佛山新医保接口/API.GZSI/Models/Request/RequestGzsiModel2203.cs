using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel2203
    {
        public Mdtrtinfo mdtrtinfo { get; set; }
        public List<Diseinfo> diseinfo { get; set; }

        /// <summary>
        /// mdtrtinfo类
        /// </summary>
        public class Mdtrtinfo
        {
            /// <summary>
            /// 就诊 ID 
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 医疗类别
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public string begntime { get; set; }
            /// <summary>
            /// 主要病情描述
            /// </summary>
            public string main_cond_dscr { get; set; }
            /// <summary>
            /// 病种编码
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary>
            /// 病种名称
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 生育手术类别
            /// </summary>
            public string matn_type { get; set; }
            /// <summary>
            /// 计划生育手术类别
            /// </summary>
            public string birctrl_type { get; set; }
            /// <summary>
            /// 计划生育手术或生育日期
            /// </summary>
            public string birctrl_matn_date { get; set; }
            /// <summary>
            /// 是否第三方责任申请 0-否，1-是 
            /// </summary>
            public string ttp_resp { get; set; }
            /// <summary>
            /// 终止妊娠月数
            /// </summary>
            public string expi_gestation_nub_of_m { get; set; }
        }

        /// <summary>
        /// diseinfo类
        /// </summary>
        public class Diseinfo
        {
            /// <summary>
            /// 诊断类别
            /// </summary>
            public string diag_type { get; set; }
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
            /// <summary>
            /// 主诊断标志 1-是 0-否
            /// </summary>
            public string maindiag_flag { get; set; }
        }
    }
}
