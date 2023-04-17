using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 医保清算对总账
    /// </summary>
    public class ResponseGzsiModel3201 : ResponseBase
    {

        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            public Stmtinfo stmtinfo { get; set; }
        }

        /// <summary>
        /// stmtinfo类
        /// </summary>
        public class Stmtinfo
        {
            /// <summary>
            /// 清算经办机构 
            /// </summary>
            public string clr_optins { get; set; }
            /// <summary>
            /// 清算方式 
            /// </summary>
            public string clr_way { get; set; }
            /// <summary>
            /// 清算类别 
            /// </summary>
            public string clr_type { get; set; }
            /// <summary>
            /// 对账开始日期 
            /// </summary>
            public string stmt_begndate { get; set; }
            /// <summary>
            /// 对账结束日期 
            /// </summary>
            public string stmt_enddate { get; set; }
            /// <summary>
            /// 对账结果 
            /// </summary>
            public string stmt_rslt { get; set; }
            /// <summary>
            /// 对账结果说明 
            /// </summary>
            public string stmt_rslt_dscr { get; set; }
        }
    }
}
