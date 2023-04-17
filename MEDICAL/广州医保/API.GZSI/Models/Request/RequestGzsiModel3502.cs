using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel3502
    {
        public Invinfo invinfo { get; set; }
        public class Invinfo
        {

            /// <summary>
            /// 医疗目录编码 Y　
            /// <summary>
            public string med_list_codg { get; set; }

            /// <summary>
            /// 库存变更类型 Y　
            /// <summary>
            public string inv_chg_type { get; set; }

            /// <summary>
            /// 定点医药机构目录编号 Y　
            /// <summary>
            public string fixmedins_hilist_id { get; set; }

            /// <summary>
            /// 定点医药机构目录名称 Y　
            /// <summary>
            public string fixmedins_hilist_name { get; set; }

            /// <summary>
            /// 定点医药机构批次流水号 Y　
            /// <summary>
            public string fixmedins_bchno { get; set; }

            /// <summary>
            /// 单价 Y　
            /// <summary>
            public string pric { get; set; }

            /// <summary>
            /// 数量 Y　
            /// <summary>
            public string cnt { get; set; }

            /// <summary>
            /// 处方药标志 Y　
            /// <summary>
            public string rx_flag { get; set; }

            /// <summary>
            /// 库存变更时间 Y　
            /// <summary>
            public string inv_chg_time { get; set; }

            /// <summary>
            /// 库存变更经办人姓名 　
            /// <summary>
            public string inv_chg_opter_name { get; set; }

            /// <summary>
            /// 备注 　
            /// <summary>
            public string memo { get; set; }

            /// <summary>
            /// 拆零标志 
            /// <summary>
            public string trdn_flag { get; set; }
        }
    }
}
