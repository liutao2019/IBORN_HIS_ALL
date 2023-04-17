using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 登录签退
    /// </summary>
    public class ResponseGzsiModel9002 : ResponseBase
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
            /// 签退时间 yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string sign_time { get; set; }
        }
    }
}
