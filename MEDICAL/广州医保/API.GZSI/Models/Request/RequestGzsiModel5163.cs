using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel5163
    {
        /// <summary>
        /// 条件 
        /// </summary>
        public CataLog catalog { get; set; }

        public class CataLog
        {
            /// <summary>
            /// 医疗机构目录编号
            /// </summary>
            public string fixmedins_hilist_id { get; set; }

            /// <summary>
            /// 医疗机构目录名称
            /// </summary>
            public string fixmedins_hilist_name { get; set; }

            /// <summary>
            /// 医疗目录类别
            /// </summary>
            public string list_type { get; set; }

            /// <summary>
            /// 医疗目录编码 
            /// </summary>
            public string med_list_codg { get; set; }

            /// <summary>
            /// 审核状态 
            /// </summary>
            public string chk_stas { get; set; }

            /// <summary>
            /// 开始行数 
            /// </summary>
            public string begnrow { get; set; }

        }
    }
}