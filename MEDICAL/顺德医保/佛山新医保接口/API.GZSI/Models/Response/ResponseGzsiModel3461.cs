using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel3461 : ResponseBase
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
            /// <summary>
            /// 医院病床号
            /// </summary>
            public string hosp_bed_no { get; set; }
            /// <summary>
            /// 备注 错误信息描述：上传失败，错误原因
            /// </summary>
            public string memo { get; set; }
        }
        
    }
}
