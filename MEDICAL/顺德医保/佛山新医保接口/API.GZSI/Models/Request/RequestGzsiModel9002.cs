using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 登录签退
    /// </summary>
    public class RequestGzsiModel9002
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
        /// 签到流水号 Y
        /// </summary>
        public string sign_no { get; set; }
        /// <summary>
        /// 操作员编号 Y
        /// </summary>
        public string opter_no { get; set; }
    }
}
