using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 医执人员信息查询
    /// </summary>
    public class RequestGzsiModel5102
    {
        #region data节点
        public Data data { get; set; }
        public class Data
        {
            /// <summary>
            /// 执业人员分类 Y
            /// </summary>
            public string prac_psn_type { get; set; }

            /// <summary>
            /// 人员证件类型
            /// </summary>
            public string psn_cert_type { get; set; }

            /// <summary>
            /// 证件号码
            /// </summary>
            public string certno { get; set; }

            /// <summary>
            /// 执业人员姓名
            /// </summary>
            public string prac_psn_name { get; set; }

            /// <summary>
            /// 执业人员代码
            /// </summary>
            public string prac_psn_code { get; set; }

        }

        #endregion
    }
}
