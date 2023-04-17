using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 输血信息
    /// </summary>
    public class RequestGzsiModel5203
    {
        public Data data { get; set; }
        public class Data
        {
            
            /// <summary>
            /// 人员编号 
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 结算 ID  
            /// <summary>
            public string setl_id { get; set; }

            /// <summary>
            /// 就诊 ID 
            /// <summary>
            public string mdtrt_id 	{ get; set; }


        }
    }
}
