using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel2202
    {
        public Mdtrtinfo mdtrtinfo { get; set; }

        public class Mdtrtinfo
        {
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 住院/门诊号
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 就诊方式
            /// </summary>
            public string mdtrt_mode { get; set; }
        }
    }
}
