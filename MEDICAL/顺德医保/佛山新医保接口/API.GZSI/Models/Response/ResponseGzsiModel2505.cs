using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 人员定点备案
    /// </summary>
    public class ResponseGzsiModel2505 : ResponseBase
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
        /// Result类
        /// </summary>
        public class Result
        {
            /// <summary>
            /// 待遇申报明细流水号
            /// </summary>
            public string trt_dcla_detl_sn  { get; set; }
        }
    }
}
