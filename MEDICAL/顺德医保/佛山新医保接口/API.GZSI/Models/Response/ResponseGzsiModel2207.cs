using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel2207 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
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
