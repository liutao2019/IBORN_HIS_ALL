using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanSiDetailUpload.Model
{
    /// <summary>
    /// 返回结果对象
    /// </summary>
    public class ResultHead
    {
        /// <summary>
        /// 1:执行成功;否则:失败
        /// </summary>
        private string code;

        /// <summary>
        /// 1:执行成功;否则:失败
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        private string message;

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// 回话ID
        /// </summary>
        private string sessionId;

        /// <summary>
        /// 回话ID
        /// </summary>
        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        /// <summary>
        /// Output段
        /// </summary>
        private string outPutXML;

        /// <summary>
        /// Output段
        /// </summary>
        public string OutPutXML
        {
            get { return outPutXML; }
            set { outPutXML = value; }
        }
    }
}
