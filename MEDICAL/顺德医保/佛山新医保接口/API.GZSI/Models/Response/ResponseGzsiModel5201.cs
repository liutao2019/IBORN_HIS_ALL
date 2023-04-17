using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5201 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public List<Mdtrtinfo> mdtrtinfo { get; set; }

            public class Mdtrtinfo
            {
                /// <summary>
                ///	就诊ID	
                /// <summary>
                [API.GZSI.Common.Display("就诊ID")]
                public string mdtrt_id { get; set; }

                /// <summary>
                ///	人员编号	
                /// <summary>
                [API.GZSI.Common.Display("人员编号")]
                public string psn_no { get; set; }

                /// <summary>
                ///	人员证件类型	
                /// <summary>
                [API.GZSI.Common.Display("人员证件类型")]
                public string psn_cert_type { get; set; }

                /// <summary>
                ///  证件号码  
                /// <summary>
                [API.GZSI.Common.Display("证件号码")]
                public string certno { get; set; }

                /// <summary>
                ///  人员姓名  
                /// <summary>
                [API.GZSI.Common.Display("人员姓名")]
                public string psn_name { get; set; }

                /// <summary>
                ///  性别  
                /// <summary>
                [API.GZSI.Common.Display("性别")]
                public string gend { get; set; }

                /// <summary>
                ///  民族  
                /// <summary>
                [API.GZSI.Common.Display("民族")]
                public string naty { get; set; }

                /// <summary>
                ///  出生日期  
                /// <summary>
                [API.GZSI.Common.Display("出生日期")]
                public string brdy { get; set; }

                /// <summary>
                ///  年龄  
                /// <summary>
                [API.GZSI.Common.Display("年龄")]
                public string age { get; set; }

                /// <summary>
                ///  联系人姓名  
                /// <summary>
                [API.GZSI.Common.Display("联系人姓名")]
                public string coner_name { get; set; }

                /// <summary>
                ///  联系电话  
                /// <summary>
                [API.GZSI.Common.Display("联系电话")]
                public string tel { get; set; }

                /// <summary>
                ///  险种类型  
                /// <summary>
                [API.GZSI.Common.Display("险种类型")]
                public string insutype { get; set; }

                /// <summary>
                ///  人员类别  
                /// <summary>
                [API.GZSI.Common.Display("人员类别")]
                public string psn_type { get; set; }

                /// <summary>
                ///  医疗救助对象标志  
                /// <summary>
                [API.GZSI.Common.Display("医疗救助对象标志")]
                public string maf_psn_flag { get; set; }

                /// <summary>
                ///  公务员标志  
                /// <summary>
                [API.GZSI.Common.Display("公务员标志")]
                public string cvlserv_flag { get; set; }

                /// <summary>
                ///  灵活就业标志  
                /// <summary>
                [API.GZSI.Common.Display("灵活就业标志")]
                public string flxempe_flag { get; set; }

                /// <summary>
                ///  新生儿标志  
                /// <summary>
                [API.GZSI.Common.Display("新生儿标志")]
                public string nwb_flag { get; set; }

                /// <summary>
                ///  参保机构医保区划  
                /// <summary>
                [API.GZSI.Common.Display("参保机构医保区划")]
                public string insu_optins { get; set; }

                /// <summary>
                ///  单位名称  
                /// <summary>
                [API.GZSI.Common.Display("单位名称")]
                public string emp_name { get; set; }

                /// <summary>
                ///  开始时间  
                /// <summary>
                [API.GZSI.Common.Display("开始时间")]
                public string begntime { get; set; }

                /// <summary>
                ///  结束时间  
                /// <summary>
                [API.GZSI.Common.Display("结束时间")]
                public string endtime { get; set; }

                /// <summary>
                ///  就诊凭证类型  
                /// <summary>
                [API.GZSI.Common.Display("就诊凭证类型")]
                public string mdtrt_cert_type { get; set; }

                /// <summary>
                ///  医疗类别  
                /// <summary>
                [API.GZSI.Common.Display("医疗类别")]
                public string med_type { get; set; }

                /// <summary>
                ///  跨年度住院标志  
                /// <summary>
                [API.GZSI.Common.Display("跨年度住院标志")]
                public string ars_year_ipt_flag { get; set; }

                /// <summary>
                ///  先行支付标志  
                /// <summary>
                [API.GZSI.Common.Display("先行支付标志")]
                public string pre_pay_flag { get; set; }

                /// <summary>
                ///  住院/门诊号  
                /// </summary>
                [API.GZSI.Common.Display("住院/门诊号")]
                public string ipt_otp_no { get; set; }

                /// <summary>
                ///  病历号  
                /// <summary>
                [API.GZSI.Common.Display("病历号")]
                public string medrcdno { get; set; }

                /// <summary>
                ///  主治医生编码  
                /// <summary>
                [API.GZSI.Common.Display("主治医生编码")]
                public string atddr_no { get; set; }

                /// <summary>
                ///  主诊医师姓名  
                /// <summary>
                [API.GZSI.Common.Display("主诊医师姓名")]
                public string chfpdr_name { get; set; }

                /// <summary>
                ///  入院科室编码  
                /// <summary>
                [API.GZSI.Common.Display("入院科室编码")]
                public string adm_dept_codg { get; set; }

                /// <summary>
                ///  入院科室名称  
                /// <summary>
                [API.GZSI.Common.Display("入院科室名称")]
                public string adm_dept_name { get; set; }

                /// <summary>
                ///  入院床位  
                /// <summary>
                [API.GZSI.Common.Display("入院床位")]
                public string adm_bed { get; set; }

                /// <summary>
                ///  住院主诊断代码  
                /// <summary>
                [API.GZSI.Common.Display("住院主诊断代码")]
                public string dscg_maindiag_code { get; set; }

                /// <summary>
                ///  住院主诊断名称  
                /// <summary>
                [API.GZSI.Common.Display("住院主诊断名称")]
                public string dscg_maindiag_name { get; set; }

                /// <summary>
                ///  出院科室编码  
                /// <summary>
                [API.GZSI.Common.Display("出院科室编码")]
                public string dscg_dept_codg { get; set; }

                /// <summary>
                ///  出院科室名称  
                /// <summary>
                [API.GZSI.Common.Display("出院科室名称")]
                public string dscg_dept_name { get; set; }

                /// <summary>
                ///  出院床位  
                /// <summary>
                [API.GZSI.Common.Display("出院床位")]
                public string dscg_bed { get; set; }

                /// <summary>
                ///  离院方式  
                /// <summary>
                [API.GZSI.Common.Display("离院方式")]
                public string dscg_way { get; set; }

                /// <summary>
                ///  主要病情描述  
                /// <summary>
                [API.GZSI.Common.Display("主要病情描述")]
                public string main_cond_dscr { get; set; }

                /// <summary>
                ///  病种编码  
                /// <summary>
                [API.GZSI.Common.Display("病种编码")]
                public string dise_codg { get; set; }

                /// <summary>
                ///  病种名称  
                /// <summary>
                [API.GZSI.Common.Display("病种名称")]
                public string dise_name { get; set; }

                /// <summary>
                ///  手术操作代码  
                /// <summary>
                [API.GZSI.Common.Display("手术操作代码")]
                public string oprn_oprt_code { get; set; }

                /// <summary>
                ///  手术操作名称  
                /// <summary>
                [API.GZSI.Common.Display("手术操作名称")]
                public string oprn_oprt_name { get; set; }

                /// <summary>
                ///  门诊诊断信息  
                /// <summary>
                [API.GZSI.Common.Display("门诊诊断信息")]
                public string otp_diag_info { get; set; }

                /// <summary>
                ///  在院状态  
                /// <summary>
                [API.GZSI.Common.Display("在院状态")]
                public string inhosp_stas { get; set; }

                /// <summary>
                ///  死亡日期  
                /// <summary>
                [API.GZSI.Common.Display("死亡日期")]
                public string die_date { get; set; }

                /// <summary>
                ///  住院天数  
                /// <summary>
                [API.GZSI.Common.Display("住院天数")]
                public string ipt_days { get; set; }

                /// <summary>
                ///  计划生育服务证号  
                /// <summary>
                [API.GZSI.Common.Display("计划生育服务证号")]
                public string fpsc_no { get; set; }

                /// <summary>
                ///  生育类别  
                /// <summary>
                [API.GZSI.Common.Display("生育类别")]
                public string matn_type { get; set; }

                /// <summary>
                ///  计划生育手术类别  
                /// <summary>
                [API.GZSI.Common.Display("计划生育手术类别")]
                public string birctrl_type { get; set; }

                /// <summary>
                ///  晚育标志  
                /// <summary>
                [API.GZSI.Common.Display("晚育标志")]
                public string latechb_flag { get; set; }

                /// <summary>
                ///  孕周数  
                /// <summary>
                [API.GZSI.Common.Display("孕周数")]
                public string geso_val { get; set; }

                /// <summary>
                ///  胎次  
                /// <summary>
                [API.GZSI.Common.Display("胎次")]
                public string fetts { get; set; }

                /// <summary>
                ///  胎儿数  
                /// <summary>
                [API.GZSI.Common.Display("胎儿数")]
                public string fetus_cnt { get; set; }

                /// <summary>
                ///  早产标志  
                /// <summary>
                [API.GZSI.Common.Display("早产标志")]
                public string pret_flag { get; set; }

                /// <summary>
                ///  计划生育手术或生育日期  
                /// <summary>
                [API.GZSI.Common.Display("计划生育手术或生育日期")]
                public string birctrl_matn_date { get; set; }

                /// <summary>
                ///  伴有并发症标志  
                /// <summary>
                [API.GZSI.Common.Display("伴有并发症标志")]
                public string cop_flag { get; set; }

                /// <summary>
                ///  经办人ID  
                /// <summary>
                [API.GZSI.Common.Display("经办人ID")]
                public string opter_id { get; set; }

                /// <summary>
                ///  经办人姓名  
                /// <summary>
                [API.GZSI.Common.Display("经办人姓名")]
                public string opter_name { get; set; }

                /// <summary>
                ///  经办时间  
                /// <summary>
                [API.GZSI.Common.Display("经办时间")]
                public string opt_time { get; set; }

                /// <summary>
                ///  备注  
                /// <summary>
                [API.GZSI.Common.Display("备注")]
                public string memo { get; set; }


            }
        }
    }
}
