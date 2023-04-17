using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 医保清算对总账
    /// </summary>
    public class RequestGzsiModel3201
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 险种类型 Y 见【4码表说明】
            /// <summary>
            public string insutype { get; set; }

            /// <summary>
            /// 清算类别 Y 见【4码表说明】
            /// <summary>
            public string clr_type { get; set; }

            /// <summary>
            /// 清算经办机构 Y 
            /// <summary>
            public string setl_optins { get; set; }

            /// <summary>
            /// 对账开始日期 Y yyyy-MM-dd
            /// <summary>
            public string stmt_begndate { get; set; }

            /// <summary>
            /// 对账结束日期 Y yyyy-MM-dd
            /// <summary>
            public string stmt_enddate { get; set; }

            /// <summary>
            /// 医疗费总额 Y 
            /// <summary>
            public string medfee_sumamt { get; set; }

            /// <summary>
            /// 基金支付总额 Y 
            /// <summary>
            public string fund_pay_sumamt { get; set; }

            /// <summary>
            /// 现金支付金额 Y 
            /// <summary>
            public string acct_pay { get; set; }

            /// <summary>
            /// 定点医疗机构结算笔数 Y 
            /// <summary>
            public string fixmedins_setl_cnt { get; set; }

            /// <summary>
            /// 医保中心退费结算标志
            /// <summary>
            public string REFD_SETL_FLAG { get; set; }
        }
    }
}
