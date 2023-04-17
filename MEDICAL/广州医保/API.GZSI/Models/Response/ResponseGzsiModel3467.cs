using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel3467 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            public List<Result> result { get; set; }
        }

        /// <summary>
        /// result类
        /// </summary>
        public class Result
        {
            /// 医师编码
            /// </summary>
            public string dr_codg { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string memo { get; set; }
        }
    }
}
