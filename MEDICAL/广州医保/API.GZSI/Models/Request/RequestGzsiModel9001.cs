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
        public Data data { get; set; }

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
            /// 登录账号 Y
            /// </summary>
            public string userid { get; set; }
            /// <summary>
            /// 操作员编号 Y
            /// </summary>
            public string opter_no { get; set; }
            /// <summary>
            /// 登录密码 Y
            /// </summary>
            public string password { get; set; }
            /// <summary>
            /// 登录时间 Y yyyymmddhh24miss
            /// </summary>
            public string currenttime { get; set; }
        }
    }
}
