using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 撤销病床信息
    /// </summary>
    public class RequestGzsiModel3463
    {
        public Bedinfo bedinfo { get; set; }

        public class Bedinfo
        {
            /// <summary>
            /// 医院病床号 Y 
            /// <summary>
            public string hosp_bed_no { get; set; }
            /// <summary>
            /// 备注  
            /// <summary>
            public string memo { get; set; }
        }
    }
}
