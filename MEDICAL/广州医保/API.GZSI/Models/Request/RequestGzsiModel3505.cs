using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel3505
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
            /// 开方医师证件类型 　
            /// <summary>
            public string prsc_dr_cert_type { get; set; }

            /// <summary>
            /// 开方医师证件号码 　
            /// <summary>
            public string prsc_dr_certno { get; set; }

            /// <summary>
            /// 开方医师姓名 Y　
            /// <summary>
            public string prsc_dr_name { get; set; }

            /// <summary>
            /// 药师证件类型 　
            /// <summary>
            public string phar_cert_type { get; set; }

            /// <summary>
            /// 药师证件号码 　
            /// <summary>
            public string phar_certno { get; set; }

            /// <summary>
            /// 药师姓名 Y　
            /// <summary>
            public string phar_name { get; set; }

            /// <summary>
            /// 药师执业资格证号 Y　
            /// <summary>
            public string phar_prac_cert_no { get; set; }

            /// <summary>
            /// 医保费用结算类型 　
            /// <summary>
            public string hi_feesetl_type { get; set; }

            /// <summary>
            /// 结算ID 　
            /// <summary>
            public string setl_id { get; set; }

            /// <summary>
            /// 就医流水号 Y
            /// <summary>
            public string mdtrt_sn { get; set; }

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
            /// 处方号 
            /// <summary>
            public string rxno { get; set; }

            /// <summary>
            /// 外购处方标志 
            /// <summary>
            public string rx_circ_flag { get; set; }

            /// <summary>
            /// 零售单据号 Y
            /// <summary>
            public string rtal_docno { get; set; }

            /// <summary>
            /// 销售出库单据号 
            /// <summary>
            public string stoout_no { get; set; }

            /// <summary>
            /// 批次号 
            /// <summary>
            public string bchno { get; set; }

            /// <summary>
            /// 药品追溯码 
            /// <summary>
            public string drug_trac_codg { get; set; }

            /// <summary>
            /// 药品条形码 
            /// <summary>
            public string drug_prod_barc { get; set; }

            /// <summary>
            /// 货架位 
            /// <summary>
            public string shelf_posi { get; set; }

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
        }
    }
}
