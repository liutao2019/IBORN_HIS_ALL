using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 目录对照上传
    /// </summary>
    public class RequestGzsiModel3301
    {
        public List<Catalogcompin> catalogcompin { get; set; }

        public class Catalogcompin
        {
            /// <summary>
            /// 定点医药机构目录编号 Y
            /// <summary>
            public string fixmedins_hilist_id { get; set; }
            public string fixmedinsHilistId { get; set; }
            /// <summary>
            /// 定点医药目录名称 Y
            /// <summary>
            public string fixmedins_hilist_name { get; set; }
            public string fixmedinsHilistName { get; set; }
            /// <summary>
            /// 医疗目录类别 Y
            /// <summary>
            public string list_type { get; set; }
            public string listType { get; set; }
            /// <summary>
            /// 医疗目录编码 Y
            /// <summary>
            public string med_list_codg { get; set; }
            public string medListCodg { get; set; }
            /// <summary>
            /// 开始日期 Y
            /// <summary>
            public string begndate { get; set; }

            /// <summary>
            /// 结束日期 
            /// <summary>
            public string enddate { get; set; }

            /// <summary>
            /// 药监本位码 
            /// <summary>
            public string drugadm_strdcod { get; set; }

            /// <summary>
            /// 通用名编号 
            /// <summary>
            public string genname_no { get; set; }

            /// <summary>
            /// 批准文号 Y
            /// <summary>
            public string aprvno { get; set; }

            /// <summary>
            /// 剂型 
            /// <summary>
            public string dosform { get; set; }

            /// <summary>
            /// 除外内容 
            /// <summary>
            public string exept_cont { get; set; }

            /// <summary>
            /// 项目内涵 
            /// <summary>
            public string item_cont { get; set; }

            /// <summary>
            /// 计价单位 
            /// <summary>
            public string prcunt { get; set; }

            /// <summary>
            /// 规格 
            /// <summary>
            public string spec { get; set; }

            /// <summary>
            /// 包装规格 
            /// <summary>
            public string pacspec { get; set; }

            /// <summary>
            /// 单价 
            /// <summary>
            public string pric { get; set; }

            /// <summary>
            /// 备注 
            /// <summary>
            public string memo { get; set; }
            public string exctCont { get; set; }
        }
    }
}
