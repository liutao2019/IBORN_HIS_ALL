using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5304 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public List<Refmedin> refmedin { get; set; }

            public class Refmedin
            {
                /// <summary>
                ///  险种类型 
                /// <summary> 
                [API.GZSI.Common.Display("险种类型")]  
                public string insutype { get; set; }

                /// <summary>
                ///  申报来源 
                /// <summary>  
                [API.GZSI.Common.Display("申报来源")] 
                public string dcla_souc { get; set; }

                /// <summary>
                ///  人员编号 
                /// <summary>  
                [API.GZSI.Common.Display("人员编号")] 
                public string psn_no { get; set; }

                /// <summary>
                ///  人员证件类型 
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
                ///  出生日期 
                /// <summary> 
                [API.GZSI.Common.Display("出生日期")]  
                public string brdy { get; set; }

                /// <summary>
                ///  联系电话 
                /// <summary> 
                [API.GZSI.Common.Display("联系电话")]  
                public string tel { get; set; }

                /// <summary>
                ///  联系地址 
                /// <summary>  
                [API.GZSI.Common.Display("联系地址")] 
                public string addr { get; set; }

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
                ///  诊断代码 
                /// <summary>   
                [API.GZSI.Common.Display("诊断代码")]
                public string diag_code { get; set; }

                /// <summary>
                ///  诊断名称 
                /// <summary>
                [API.GZSI.Common.Display("诊断名称")]   
                public string diag_name { get; set; }

                /// <summary>
                ///  疾病病情描述 
                /// <summary>
                [API.GZSI.Common.Display("疾病病情描述")]   
                public string dise_cond_dscr { get; set; }

                /// <summary>
                ///  转往定点医药机构编号 
                /// <summary> 
                [API.GZSI.Common.Display("转往定点医药机构编号")]  
                public string reflin_medins_no { get; set; }

                /// <summary>
                ///  转往医院名称 
                /// <summary>
                [API.GZSI.Common.Display("转往医院名称")]   
                public string reflin_medins_name { get; set; }

                /// <summary>
                ///  异地标志 
                /// <summary> 
                [API.GZSI.Common.Display("异地标志")]  
                public string out_flag { get; set; }

                /// <summary>
                ///  转院日期 
                /// <summary>
                [API.GZSI.Common.Display("转院日期")]   
                public string refl_date { get; set; }

                /// <summary>
                ///  转院原因 
                /// <summary>
                [API.GZSI.Common.Display("人员编号")]   
                public string refl_rea { get; set; }

                /// <summary>
                ///  开始日期 
                /// <summary>
                [API.GZSI.Common.Display("开始日期")]   
                public string begndate { get; set; }

                /// <summary>
                ///  结束日期 
                /// <summary> 
                [API.GZSI.Common.Display("结束日期")]  
                public string enddate { get; set; }

                /// <summary>
                ///  医院同意转院标志 
                /// <summary> 
                [API.GZSI.Common.Display("医院同意转院标志")]  
                public string hosp_agre_refl_flag { get; set; }

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

            }
        }
    }
}
