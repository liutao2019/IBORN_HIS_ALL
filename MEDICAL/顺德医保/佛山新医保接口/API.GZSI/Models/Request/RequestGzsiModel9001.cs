using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 登录签到
    /// </summary>
    public class RequestGzsiModel9001 
    {
        public Data signIn { get; set; }

        /// <summary>
        /// Data类
        /// </summary>
        public class Data
        {
            /// <summary>
            /// MAC地址 Y
            /// </summary>
            public string mac { get; set; }
            /// <summary>
            /// IP地址 Y
            /// </summary>
            public string ip { get; set; }
            /// <summary>
            /// 操作员编号 Y
            /// </summary>
            public string opter_no { get; set; }
        }
    }
}
