using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 目录对照撤销
    /// </summary>
    public class RequestGzsiModel3302
    {
        public Catalog catalog { get; set; }

        public class Catalog
        {
            /// <summary>
            /// 定点医药机构编号 Y
            /// <summary>
            public string fixmedins_code { get; set; }

            /// <summary>
            /// 定点医药机构目录编号
            public string fixmedins_hilist_id { get; set; }

            /// <summary>
            /// 医疗目录类别 Y
            /// <summary>
            public string list_type { get; set; }

            /// <summary>
            /// 医疗目录编码 Y
            /// <summary>
            public string med_list_codg { get; set; }
        }
    }
}
