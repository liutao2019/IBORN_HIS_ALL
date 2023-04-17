using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 商品盘存上传
    /// </summary>
    public class RequestGzsiModel3501
    {
        public Invinfo invinfo { get; set; }
        public class Invinfo
        {
            #region invinfo

            /// <summary>
            /// 医疗目录编码 Y　
            /// <summary>
            public string med_list_codg { get; set; }

            /// <summary>
            /// 定点医药机构目录编号 Y　
            /// <summary>
            public string fixmedins_hilist_id { get; set; }

            /// <summary>
            /// 定点医药机构目录名称 Y　
            /// <summary>
            public string fixmedins_hilist_name { get; set; }

            /// <summary>
            /// 处方药标志 Y　
            /// <summary>
            public string rx_flag { get; set; }

            /// <summary>
            /// 盘存日期 Y　
            /// <summary>
            public string invdate { get; set; }

            /// <summary>
            /// 库存数量 Y　
            /// <summary>
            public string inv_cnt { get; set; }

            /// <summary>
            /// 生产批号 　
            /// <summary>
            public string manu_lotnum { get; set; }

            /// <summary>
            /// 定点医药机构批次流水号 Y　
            /// <summary>
            public string fixmedins_bchno { get; set; }

            /// <summary>
            /// 生产日期 Y　
            /// <summary>
            public string manu_date { get; set; }

            /// <summary>
            /// 有效期止 Y　
            /// <summary>
            public string expy_end { get; set; }

            /// <summary>
            /// 备注 　
            /// <summary>
            public string memo { get; set; }

            #endregion
        }
        
    }
}
