using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel2201 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            public Data data { get; set; }
        }

        /// <summary>
        /// data类
        /// </summary>
        public class Data
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
        }
    }
}
