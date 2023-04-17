using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel3504
    {
        public Purcinfo purcinfo { get; set; }
        public class Purcinfo
        {

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
            /// 定点医药机构批次流水号 Y　
            /// <summary>
            public string fixmedins_bchno { get; set; }

            /// <summary>
            /// 供应商名称 Y　
            /// <summary>
            public string spler_name { get; set; }

            /// <summary>
            /// 供应商许可证号 　
            /// <summary>
            public string spler_pmtno { get; set; }

            /// <summary>
            /// 生产日期 Y　
            /// <summary>
            public string manu_date { get; set; }

            /// <summary>
            /// 有效期止 Y　
            /// <summary>
            public string expy_end { get; set; }

            /// <summary>
            /// 最终成交单价 　
            /// <summary>
            public string finl_trns_pric { get; set; }

            /// <summary>
            /// 采购/退货数量 Y　
            /// <summary>
            public string purc_retn_cnt { get; set; }

            /// <summary>
            /// 采购发票编码 　
            /// <summary>
            public string purc_invo_codg { get; set; }

            /// <summary>
            /// 采购发票号 Y　
            /// <summary>
            public string purc_invo_no { get; set; }

            /// <summary>
            /// 处方药标志 Y　
            /// <summary>
            public string rx_flag { get; set; }

            /// <summary>
            /// 采购/退货入库时间 Y　
            /// <summary>
            public string purc_retn_stoin_time { get; set; }

            /// <summary>
            /// 采购/退货经办人姓名 Y　
            /// <summary>
            public string purc_retn_opter_name { get; set; }

            /// <summary>
            /// 备注 　
            /// <summary>
            public string memo { get; set; }

            /// <summary>
            /// 商品采购流水号 
            /// <summary>
            public string medins_prod_purc_no { get; set; }
        }
    }
}
