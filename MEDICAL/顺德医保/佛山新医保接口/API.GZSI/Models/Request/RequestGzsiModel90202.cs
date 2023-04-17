using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 家庭医生签约登记
    /// </summary>
    public class RequestGzsiModel90202
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
            ///  医师代码  
            /// <summary>
            public string dr_code { get; set; }

            /// <summary>
            ///  签约承诺  
            /// <summary>
            public string sign_prse { get; set; }

            /// <summary>
            ///  备注  
            /// <summary>
            public string memo { get; set; }

            /// <summary>
            ///  代办人联系地址  
            /// <summary>
            public string agnter_addr { get; set; }

            /// <summary>
            ///  代办人证件类型  
            /// <summary>
            public string agnter_cert_type { get; set; }

            /// <summary>
            ///  代办人证件号码  
            /// <summary>
            public string agnter_certno { get; set; }

            /// <summary>
            ///  代办人姓名  
            /// <summary>
            public string agnter_name { get; set; }

            /// <summary>
            ///  代办人关系  
            /// <summary>
            public string agnter_rlts { get; set; }

            /// <summary>
            ///  代办人电话  
            /// <summary>
            public string agnter_tel { get; set; }


        }
    }
}
