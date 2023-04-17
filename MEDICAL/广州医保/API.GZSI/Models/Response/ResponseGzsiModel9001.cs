using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 登录签到
    /// </summary>
    public class ResponseGzsiModel9001 : ResponseBase
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
        /// 签到结果 Result
        /// </summary>
        public class Result
        {
            /// 签到时间 yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string sign_time { get; set; }
            /// <summary>
            /// 签到流水号
            /// </summary>
            public string sign_no { get; set; }
        }
    }
}
