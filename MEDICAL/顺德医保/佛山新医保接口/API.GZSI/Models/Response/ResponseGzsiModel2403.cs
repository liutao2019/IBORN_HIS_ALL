using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 入院信息变更
    /// </summary>
    public class ResponseGzsiModel2403 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            public Result result { get; set; }
        }

        /// <summary>
        /// result类
        /// </summary>
        public class Result
        {
            /// <summary>
            /// 违规类型
            /// <summary>
            public string vola_type { get; set; }
            /// <summary>
            /// 违规说明
            /// <summary>
            public string vola_dscr { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string memo { get; set; }
        }
    }
}
