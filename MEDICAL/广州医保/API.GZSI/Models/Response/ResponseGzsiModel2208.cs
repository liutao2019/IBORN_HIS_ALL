using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 门诊结算撤销
    /// </summary>
    public class ResponseGzsiModel2208 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            /// <summary>
            /// 结算信息
            /// </summary>
            public SetlInfo setlinfo { get; set; }
            /// <summary>
            /// 基金分项
            /// </summary>
            public List<Setldetail> setldetail { get; set; }
        }

    }
}
