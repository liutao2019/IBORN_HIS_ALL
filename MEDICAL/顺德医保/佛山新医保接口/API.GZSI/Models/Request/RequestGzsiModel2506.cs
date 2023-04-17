using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 人员取消定点备案
    /// </summary>
    public class RequestGzsiModel2506
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 备案流水号
            /// </summary>
            public string trt_dcla_detl_sn { get; set; }

            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string memo { get; set; }
        }
    }

    
}
