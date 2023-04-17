using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 出院撤销
    /// </summary>
    public class RequestGzsiModel2405
    {
        /// <summary>
        /// 撤销出院入参
        /// </summary>
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 就诊ID Y  
            /// <summary>
            public string mdtrt_id { get; set; }

            /// <summary>
            /// 人员编号 Y  
            /// <summary>
            public string psn_no { get; set; }
        }
    }
}
