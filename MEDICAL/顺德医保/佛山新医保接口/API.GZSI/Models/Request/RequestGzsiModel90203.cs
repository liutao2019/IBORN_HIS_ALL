using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 家庭病床登记
    /// </summary>
    public class RequestGzsiModel90203
    {
        public Data data { get; set; }

        public class Data
        {

            /// <summary>
            ///	人员编号	
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///	开始日期	
            /// <summary>
            public string begndate { get; set; }

            /// <summary>
            ///	结束日期	
            /// <summary>
            public string enddate { get; set; }

            /// <summary>
            ///	定点医药机构编号	
            /// <summary>
            public string fixmedins_code { get; set; }

            /// <summary>
            ///	主诊医师代码	
            /// <summary>
            public string chfpdr_code { get; set; }

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
            ///  代办人联系方式  
            /// <summary>
            public string agnter_tel { get; set; }

            /// <summary>
            ///  代办人联系地址  
            /// <summary>
            public string agnter_addr { get; set; }

            /// <summary>
            ///  代办人关系  
            /// <summary>
            public string agnter_rlts { get; set; }


        }
    }
}
