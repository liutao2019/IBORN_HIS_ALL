using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 家庭病床登记查询
    /// </summary>
    public class ResponseGzsiModel90103 : ResponseBase
    {
        public List<Result> result { get; set; }

        public class Result
        {
            /// <summary>
            ///	待遇申报明细流水号	
            /// <summary>
            public string trt_dcla_detl_sn { get; set; }

            /// <summary>
            ///	人员编号	
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///	人员证件类型	
            /// <summary>
            public string psn_cert_type { get; set; }

            /// <summary>
            ///	证件号码	
            /// <summary>
            public string certno { get; set; }

            /// <summary>
            ///	人员姓名	
            /// <summary>
            public string psn_name { get; set; }

            /// <summary>
            ///	定点医药机构编号	
            /// <summary>
            public string fixmedins_code { get; set; }

            /// <summary>
            ///	定点医药机构名称	
            /// <summary>
            public string fixmedins_name { get; set; }

            /// <summary>
            ///	主诊医师代码	
            /// <summary>
            public string chfpdr_code { get; set; }

            /// <summary>
            ///	主诊医师姓名	
            /// <summary>
            public string chfpdr_name { get; set; }

            /// <summary>
            ///	开始日期	
            /// <summary>
            public string begndate { get; set; }

            /// <summary>
            ///	结束日期	
            /// <summary>
            public string enddate { get; set; }

            /// <summary>
            ///	疾病病情描述	
            /// <summary>
            public string dise_cond_dscr { get; set; }

            /// <summary>
            ///	申请理由	
            /// <summary>
            public string appy_rea { get; set; }

            /// <summary>
            ///	备注	
            /// <summary>
            public string memo { get; set; }

            /// <summary>
            ///	代办人姓名	
            /// <summary>
            public string agnter_name { get; set; }

            /// <summary>
            ///	代办人证件类型	
            /// <summary>
            public string agnter_cert_type { get; set; }

            /// <summary>
            ///	代办人证件号码	
            /// <summary>
            public string agnter_certno { get; set; }

            /// <summary>
            ///	代办人电话	
            /// <summary>
            public string agnter_tel { get; set; }

            /// <summary>
            ///	代办人关系	
            /// <summary>
            public string agnter_rlts { get; set; }

            /// <summary>
            ///	代办人联系地址	
            /// <summary>
            public string agnter_addr { get; set; }

            /// <summary>
            ///	有效标志	
            /// <summary>
            public string vali_flag { get; set; }

            /// <summary>
            ///  险种类型  
            /// <summary>
            public string insutype { get; set; }

            /// <summary>
            ///  单位编号  
            /// <summary>
            public string emp_no { get; set; }

            /// <summary>
            ///  单位名称  
            /// <summary>
            public string emp_name { get; set; }

            /// <summary>
            ///  事件流水号  
            /// <summary>
            public string evtsn { get; set; }


        }
    }
}
