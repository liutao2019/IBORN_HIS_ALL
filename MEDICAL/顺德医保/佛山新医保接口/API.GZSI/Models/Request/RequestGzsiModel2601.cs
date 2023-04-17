using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 冲正交易
    /// </summary>
    public class RequestGzsiModel2601 
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 人员编号 Y  
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 原发送方报文ID Y  
            /// <summary>
            public string omsgid { get; set; }

            /// <summary>
            /// 原交易编号 Y  
            /// <summary>
            public string oinfno { get; set; }
        }
    }
}
