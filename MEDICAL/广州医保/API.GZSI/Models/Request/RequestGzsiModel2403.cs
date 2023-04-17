using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 入院信息变更
    /// </summary>
    public class RequestGzsiModel2403
    {
        /// <summary>
        /// 就诊信息
        /// </summary>
        public Mdtrtinfo mdtrtinfo { get; set; }

        /// <summary>
        /// 代办人信息
        /// </summary>
        public Agnterinfo agnterinfo { get; set; }

        /// <summary>
        /// 诊断信息
        /// </summary>
        public List<Inpatient.Diseinfo> diseinfo { get; set; }

        public class Mdtrtinfo
        {
            /// <summary>
            ///就诊ID Y
            /// <summary>
            public string mdtrt_id { get; set; }

            /// <summary>
            ///人员编号 Y
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///险种类型 
            /// <summary>
            public string insutype { get; set; }

            /// <summary>
            ///联系人姓名 
            /// <summary>
            public string coner_name { get; set; }

            /// <summary>
            ///联系电话 
            /// <summary>
            public string tel { get; set; }

            /// <summary>
            ///就诊凭证类型 Y
            /// <summary>
            public string mdtrt_cert_type { get; set; }

            /// <summary>
            ///就诊凭证编号 
            /// <summary>
            public string mdtrt_cert_no { get; set; }

            /// <summary>
            ///医疗类别 Y
            /// <summary>
            public string med_type { get; set; }

            /// <summary>
            ///住院/门诊号 Y
            /// <summary>
            public string ipt_otp_no { get; set; }

            /// <summary>
            ///病历号 
            /// <summary>
            public string medrcdno { get; set; }

            /// <summary>
            ///主治医生编码 Y
            /// <summary>
            public string atddr_no { get; set; }

            /// <summary>
            ///主治医师姓名 Y
            /// <summary>
            public string chfpdr_name { get; set; }

            /// <summary>
            ///入院诊断描述 Y
            /// <summary>
            public string adm_diag_dscr { get; set; }

            /// <summary>
            ///入院科室编码 Y
            /// <summary>
            public string adm_dept_codg { get; set; }

            /// <summary>
            ///入院科室名称 Y
            /// <summary>
            public string adm_dept_name { get; set; }

            /// <summary>
            ///入院床位 Y
            /// <summary>
            public string adm_bed { get; set; }

            /// <summary>
            ///住院主诊断代码 Y
            /// <summary>
            public string dscg_maindiag_code { get; set; }

            /// <summary>
            ///住院主诊断名称 Y
            /// <summary>
            public string dscg_maindiag_name { get; set; }

            /// <summary>
            ///主要病情描述 
            /// <summary>
            public string main_cond_dscr { get; set; }

            /// <summary>
            ///临床试验标志 
            /// <summary>
            public string clnc_flag { get; set; }

            /// <summary>
            ///病种编码 
            /// <summary>
            public string dise_codg { get; set; }

            /// <summary>
            ///病种名称 
            /// <summary>
            public string dise_name { get; set; }

            /// <summary>
            ///手术操作代码 
            /// <summary>
            public string oprn_oprt_code { get; set; }

            /// <summary>
            ///手术操作名称 
            /// <summary>
            public string oprn_oprt_name { get; set; }

            /// <summary>
            ///计划生育服务证号 
            /// <summary>
            public string fpsc_no { get; set; }

            /// <summary>
            ///生育类别 
            /// <summary>
            public string matn_type { get; set; }

            /// <summary>
            ///计划生育手术类别 
            /// <summary>
            public string birctrl_type { get; set; }

            /// <summary>
            ///晚育标志 
            /// <summary>
            public string latechb_flag { get; set; }

            /// <summary>
            ///孕周数 
            /// <summary>
            public string geso_val { get; set; }

            /// <summary>
            ///胎次 
            /// <summary>
            public string fetts { get; set; }

            /// <summary>
            ///胎儿数 
            /// <summary>
            public string fetus_cnt { get; set; }

            /// <summary>
            ///早产标志 
            /// <summary>
            public string pret_flag { get; set; }

            /// <summary>
            ///计划生育手术或生育日期 
            /// <summary>
            public string birctrl_matn_date { get; set; }

            /// <summary>
            ///预付款 
            /// <summary>
            public string advpay { get; set; }
        }

        public class Agnterinfo
        {
            /// <summary>
            /// 代办人姓名 
            /// <summary>
            public string agnter_name { get; set; }

            /// <summary>
            /// 代办人关系 
            /// <summary>
            public string agnter_rlts { get; set; }

            /// <summary>
            /// 代办人证件类型 
            /// <summary>
            public string agnter_cert_type { get; set; }

            /// <summary>
            /// 代办人证件号码 
            /// <summary>
            public string agnter_certno { get; set; }

            /// <summary>
            /// 代办人联系电话 
            /// <summary>
            public string agnter_tel { get; set; }

            /// <summary>
            /// 代办人联系地址 
            /// <summary>
            public string agnter_addr { get; set; }

            /// <summary>
            /// 代办人照片 
            /// <summary>
            public string agnter_photo { get; set; }
        }
    }
}
