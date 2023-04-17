using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel3401 : ResponseBase
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
            /// 医院科室代码 Y 
            /// <summary>
            public string hosp_dept_code { get; set; }
            /// <summary>
            /// 医院科室名称 Y 
            /// <summary>
            public string hosp_dept_name { get; set; }
            /// <summary>
            /// 备注  
            /// <summary>
            public string memo { get; set; }
        }
    }
}
