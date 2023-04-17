using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 获取人员基本信息
    /// </summary>
    public class ResponseGzsiModel1101 : ResponseBase
    {
        public List<Output> output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            /// <summary>
            /// 基本信息
            /// </summary>
            public Baseinfo baseinfo { get; set; }
            /// <summary>
            /// 参保信息
            /// </summary>
            public List<Insuinfo> insuinfo { get; set; }
            /// <summary>
            /// 身份信息
            /// </summary>
            public List<Idetinfo> idetinfo { get; set; }
        }

    }
}
