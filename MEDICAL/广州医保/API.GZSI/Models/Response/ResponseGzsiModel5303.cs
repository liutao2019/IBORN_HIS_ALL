using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5303 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            public List<Data> data { get; set; }
        }

        /// <summary>
        /// data类
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 就诊ID Y
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 人员证件类型 Y
            /// </summary>
            public string psn_cert_type { get; set; }
            /// <summary>
            /// 证件号码 Y
            /// </summary>
            public string certno { get; set; }
            /// <summary>
            /// 人员姓名 Y
            /// </summary>
            public string psn_name { get; set; }
            /// <summary>
            /// 性别 Y
            /// </summary>
            public string gend { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public string brdy { get; set; }
            /// <summary>
            /// 年龄 Y
            /// </summary>
            public string age { get; set; }
            /// <summary>
            /// 险种类型 Y
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 开始日期 Y
            /// </summary>
            public string begndate { get; set; }
            /// <summary>
            /// 医疗类别 Y
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 住院/门诊号 
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 异地标志
            /// </summary>
            public string out_flag { get; set; }
        }
    }
}
