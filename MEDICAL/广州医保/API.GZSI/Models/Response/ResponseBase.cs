using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 接口返回实体基本
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// 交易状态码
        /// </summary>
        public string infcode{ get; set; }
        /// <summary>
        /// 类型签名
        /// </summary>
        public string signtype { get; set; }
        /// <summary>
        /// 数字签名信息
        /// </summary>
        public string cainfo { get; set; }
        /// <summary>
        /// 接收方报文 ID 
        /// </summary>
        public string inf_refmsgid { get; set; }
        /// <summary>
        /// 接收报文时间 
        /// </summary>
        public string refmsg_time { get; set; }
        /// <summary>
        /// 响应报文时间
        /// </summary>
        public string respond_time { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string err_msg { get; set; }
        /// <summary>
        /// 提示信息 
        /// </summary>
        public string warn_msg { get; set; }
    }
}
