using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 目录对照作废
    /// </summary>
    public class RequestGzsiModel3360
    {
        public Catalog catalog { get; set; }

        public class Catalog
        {
            /// <summary>
            /// 医疗机构目录编号 Y
            /// <summary>
            public string fixmedins_hilist_id { get; set; }
            /// <summary>
            /// 医疗机构目录名称 Y
            /// <summary>
            public string fixmedins_hilist_name { get; set; }
            /// <summary>
            /// 医疗目录类别 Y
            /// <summary>
            public string list_type { get; set; }
            /// <summary>
            /// 医疗目录编码 Y
            /// <summary>
            public string med_list_codg { get; set; }
            /// <summary>
            /// 结束日期 Y
            /// <summary>
            public string enddate { get; set; }
        }
    }
}
