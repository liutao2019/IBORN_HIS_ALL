using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 入院办理返回
    /// </summary>
    public class ResponseGzsiModel2401 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// Output类
        /// </summary>
        public class Output
        {
            public Result result { get; set; }
        }

        /// <summary>
        /// result类
        /// </summary>
        public class Result
        {
            /// <summary>
            ///就诊ID 
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            ///违规类型
            /// </summary>
            public string vola_type { get; set; }
            /// <summary>
            ///违规说明
            /// </summary>
            public string vola_dscr { get; set; }

        }

        
    }
}
