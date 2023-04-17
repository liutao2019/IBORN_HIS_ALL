using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 病床上传
    /// </summary>
    public class RequestGzsiModel3461
    {
        public List<Bedinfo> bedinfo { get; set; }

        public class Bedinfo
        {
            /// <summary>
            /// 医院病床号 Y 
            /// <summary>
            public string hosp_bed_no { get; set; }

            /// <summary>
            /// 医院病床类型 Y 
            /// <summary>
            public string hosp_bed_type { get; set; }

            /// <summary>
            /// 病床房间 Y 
            /// <summary>
            public string bed_ward { get; set; }

            /// <summary>
            /// 病床房间位置  
            /// <summary>
            public string hosp_bed_room { get; set; }

            /// <summary>
            /// 医院科室代码 Y 
            /// <summary>
            public string hosp_dept_code { get; set; }

            /// <summary>
            /// 医院科室名称 Y 
            /// <summary>
            public string hosp_dept_name { get; set; }

            /// <summary>
            /// 病区编码  
            /// <summary>
            public string inpa_area_code { get; set; }

            /// <summary>
            /// 病区名称  
            /// <summary>
            public string inpa_area_name { get; set; }

            /// <summary>
            /// 备注  
            /// <summary>
            public string memo { get; set; }
        }
    }
}
