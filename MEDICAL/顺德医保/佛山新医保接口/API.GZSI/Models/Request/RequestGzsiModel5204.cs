using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel5204
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 结算 ID Y
            /// </summary>
            public string setl_id { get; set; }

            /// <summary>
            /// 就诊 ID Y
            /// </summary>
            public string mdtrt_id { get; set; }
        }
    }
}
