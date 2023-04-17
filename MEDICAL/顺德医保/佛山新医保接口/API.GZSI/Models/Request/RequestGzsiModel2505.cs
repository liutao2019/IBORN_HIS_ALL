using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 人员定点备案
    /// </summary>
    public class RequestGzsiModel2505
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 人员编号 Y  
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 联系电话   
            /// <summary>
            public string tel { get; set; }

            /// <summary>
            /// 联系地址  
            /// <summary>
            public string addr { get; set; }

            /// <summary>
            /// 业务申请类型  Y  
            /// <summary>
            public string biz_appy_type { get; set; }

            /// <summary>
            /// 开始日期
            /// <summary>
            public string begndate { get; set; }

            /// <summary>
            /// 结束日期
            /// <summary>
            public string enddate { get; set; }

            /// <summary>
            /// 代办人姓名  
            /// <summary>
            public string agnter_name { get; set; }

            /// <summary>
            /// 代办人证件类型  
            /// <summary>
            public string agnter_cert_type { get; set; }

            /// <summary>
            /// 代办人证件号码  
            /// <summary>
            public string agnter_certno { get; set; }

            /// <summary>
            /// 代办人联系方式  
            /// <summary>
            public string agnter_tel { get; set; }

            /// <summary>
            /// 代办人联系地址  
            /// <summary>
            public string agnter_addr { get; set; }

            /// <summary>
            /// 代办人关系 
            /// <summary>
            public string agnter_rlts { get; set; }

            /// <summary>
            /// 定点排序号 Y 
            /// <summary>
            public string fix_srt_no { get; set; }

            /// <summary>
            /// 定点医药机构编号 Y  
            /// <summary>
            public string fixmedins_code { get; set; }

            /// <summary>
            /// 定点医药机构名称  Y
            /// <summary>
            public string fixmedins_name { get; set; }

            /// <summary>
            /// 备注 
            /// <summary>
            public string memo { get; set; }
        }
    }
}
