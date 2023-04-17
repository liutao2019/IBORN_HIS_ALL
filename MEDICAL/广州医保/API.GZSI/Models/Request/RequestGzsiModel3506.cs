using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel3506
    {
        public Selinfo selinfo { get; set; }
        public class Selinfo
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
            /// 结算ID 　
            /// <summary>
            public string setl_id { get; set; }

            /// <summary>
            /// 人员编号 　
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 人员证件类型 　
            /// <summary>
            public string psn_cert_type { get; set; }

            /// <summary>
            /// 证件号码 　
            /// <summary>
            public string certno { get; set; }

            /// <summary>
            /// 人员姓名 　
            /// <summary>
            public string psn_name { get; set; }

            /// <summary>
            /// 生产批号 Y　
            /// <summary>
            public string manu_lotnum { get; set; }

            /// <summary>
            /// 生产日期 Y　
            /// <summary>
            public string manu_date { get; set; }

            /// <summary>
            /// 有效期止 　
            /// <summary>
            public string expy_end { get; set; }

            /// <summary>
            /// 处方药标志 Y　
            /// <summary>
            public string rx_flag { get; set; }

            /// <summary>
            /// 拆零标志 Y　
            /// <summary>
            public string trdn_flag { get; set; }

            /// <summary>
            /// 最终成交单价 　
            /// <summary>
            public string finl_trns_pric { get; set; }

            /// <summary>
            /// 销售/退货数量 Y　
            /// <summary>
            public string sel_retn_cnt { get; set; }

            /// <summary>
            /// 销售/退货时间 Y　
            /// <summary>
            public string sel_retn_time { get; set; }

            /// <summary>
            /// 销售/退货经办人姓名 Y　
            /// <summary>
            public string sel_retn_opter_name { get; set; }

            /// <summary>
            /// 备注 　
            /// <summary>
            public string memo { get; set; }

            /// <summary>
            /// 商品销售流水号 
            /// <summary>
            public string medins_prod_sel_no { get; set; }

        }
    }
}
