using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 医保清算对明细账
    /// </summary>
    public class RequestGzsiModel3202
    {
        /// <summary>
        /// 清算经办机构 
        /// <summary>
        public string clr_optins { get; set; }
        /// <summary>
        /// 清算方式 
        /// <summary>
        public string clr_way { get; set; }
        /// <summary>
        /// 清算类别 
        /// <summary>
        public string clr_type { get; set; }
        /// <summary>
        /// 文件查询号 
        /// <summary>
        public string file_qury_no { get; set; }
        /// <summary>
        /// 对账开始日期 
        /// <summary>
        public string stmt_begndate { get; set; }
        /// <summary>
        /// 对账结束日期 
        /// <summary>
        public string stmt_enddate { get; set; }
        /// <summary>
        /// 医疗费总额 
        /// <summary>
        public string medfee_sumamt { get; set; }
        /// <summary>
        /// 基金支付总额 
        /// <summary>
        public string fund_pay_sumamt { get; set; }
        /// <summary>
        /// 现金支付金额 
        /// <summary>
        public string cash_payamt { get; set; }
        /// <summary>
        /// 定点医药机构结算笔数 
        /// <summary>
        public string fixmedins_setl_cnt { get; set; }
    }
}
