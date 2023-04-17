using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 科室信息撤销
    /// </summary>
    public class RequestGzsiModel3403
    {
        public Deptinfo deptinfo { get; set; }

        public class Deptinfo
        {
            /// <summary>
            ///医院科室编码 Y 
            /// <summary>
            public string hosp_dept_codg { get; set; }

            /// <summary>
            ///医院科室名称 Y 
            /// <summary>
            public string hosp_dept_name { get; set; }
        }
    }
}
