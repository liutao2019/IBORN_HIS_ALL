using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel3469
    {
        /// <summary>
        /// 医师撤销
        /// </summary>
        public Doctorinfo doctorinfo { get; set; }

        public class Doctorinfo
        {
            /// <summary> 
            /// 医院科室代码 Y 
            /// </summary>
            public string hosp_dept_code { get; set; }
            /// <summary> 
            /// 医师编码 Y 
            /// </summary>
            public string dr_codg { get; set; }
            /// <summary> 
            /// <summary> 
            /// 备注 
            /// </summary>
            public string memo { get; set; }
        }
    }
}
