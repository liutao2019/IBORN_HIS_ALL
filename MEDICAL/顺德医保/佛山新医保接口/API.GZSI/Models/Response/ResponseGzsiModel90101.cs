using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 生育登记查询
    /// </summary>
    public class ResponseGzsiModel90101 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public List<Result> result { get; set; }

            public class Result
            {
                /// <summary>
                ///  待遇申报明细流水号  
                /// <summary>
                [API.GZSI.Common.Display("待遇申报明细流水号")]
                public string trt_dcla_detl_sn { get; set; }

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
                ///  定点医药机构编号  
                /// <summary>
                [API.GZSI.Common.Display("定点医药机构编号")]
                public string fixmedins_code { get; set; }

                /// <summary>
                ///  定点医药机构名称  
                /// <summary>
                [API.GZSI.Common.Display("定点医药机构名称")]
                public string fixmedins_name { get; set; }

                /// <summary>
                ///  联系电话  
                /// <summary>
                [API.GZSI.Common.Display("联系电话")]
                public string tel { get; set; }

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
                ///  生育类别  
                /// <summary>
                [API.GZSI.Common.Display("生育类别")]
                public string matn_type { get; set; }

                /// <summary>
                ///  生育待遇申报人员类别  
                /// <summary>
                [API.GZSI.Common.Display("生育待遇申报人员类别")]
                public string matn_trt_dclaer_type { get; set; }

                /// <summary>
                ///  计划生育服务证号  
                /// <summary>
                [API.GZSI.Common.Display("计划生育服务证号")]
                public string fpsc_no { get; set; }

                /// <summary>
                ///  末次月经日期  
                /// <summary>
                [API.GZSI.Common.Display("末次月经日期")]
                public string last_mena_date { get; set; }

                /// <summary>
                ///  预计生育日期  
                /// <summary>
                [API.GZSI.Common.Display("预计生育日期")]
                public string plan_matn_date { get; set; }

                /// <summary>
                ///  开始时间  
                /// <summary>
                [API.GZSI.Common.Display("开始时间")]
                public string begndate { get; set; }

                /// <summary>
                ///  结束日期  
                /// <summary>
                [API.GZSI.Common.Display("结束日期")]
                public string enddate { get; set; }

                /// <summary>
                ///  配偶姓名  
                /// <summary>
                [API.GZSI.Common.Display("配偶姓名")]
                public string spus_name { get; set; }

                /// <summary>
                ///  配偶证件类型  
                /// <summary>
                [API.GZSI.Common.Display("配偶证件类型")]
                public string spus_cert_type { get; set; }

                /// <summary>
                ///  配偶证件号码  
                /// <summary>
                [API.GZSI.Common.Display("配偶证件号码")]
                public string spus_certno { get; set; }

                /// <summary>
                ///  代办人姓名  
                /// <summary>
                [API.GZSI.Common.Display("代办人姓名")]
                public string agnter_name { get; set; }

                /// <summary>
                ///  代办人证件类型  
                /// <summary>
                [API.GZSI.Common.Display("代办人证件类型")]
                public string agnter_cert_type { get; set; }

                /// <summary>
                ///  代办人证件号码  
                /// <summary>
                [API.GZSI.Common.Display("代办人证件号码")]
                public string agnter_certno { get; set; }

                /// <summary>
                ///  代办人方式  
                /// <summary>
                [API.GZSI.Common.Display("代办人方式")]
                public string agnter_tel { get; set; }

                /// <summary>
                ///  代办人关系  
                /// <summary>
                [API.GZSI.Common.Display("代办人关系")]
                public string agnter_rlts { get; set; }

                /// <summary>
                ///  代办人联系地址  
                /// <summary>
                [API.GZSI.Common.Display("代办人联系地址")]
                public string agnter_addr { get; set; }

                /// <summary>
                ///  有效标志  
                /// <summary>
                [API.GZSI.Common.Display("有效标志")]
                public string vali_flag { get; set; }

                /// <summary>
                ///  申报时间  
                /// <summary>
                [API.GZSI.Common.Display("申报时间")]
                public string dcladate { get; set; }

                /// <summary>
                ///  险种类型  
                /// <summary>
                [API.GZSI.Common.Display("险种类型")]
                public string insu_type { get; set; }

                /// <summary>
                ///  单位名称  
                /// <summary>
                [API.GZSI.Common.Display("单位名称")]
                public string emp_name { get; set; }

                /// <summary>
                ///  单位编号  
                /// <summary>
                [API.GZSI.Common.Display("单位编号")]
                public string emp_no { get; set; }

                /// <summary>
                ///  事件流水号  
                /// <summary>
                [API.GZSI.Common.Display("事件流水号")]
                public string evtsn { get; set; }

            }
        }
    }
}
