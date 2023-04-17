using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 门诊单据式结算返回
    /// </summary>
    public class ResponseGzsiModel2260 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            /// <summary>
            /// 结算信息
            /// </summary>
            public SetlInfo setlInfo { get; set; }
            /// <summary>
            /// 基金分项 
            /// </summary>
            private Setldetail setldetail { get; set; }
            /// <summary>
            /// 违规费用明细
            /// </summary>
            private Voladetail voladetail { get; set; }
            /// <summary>
            /// 门诊单据式结算返回detlcutinfo
            /// </summary>
            private Outpatient.Detlcutinfo detlcutinfo { get; set; }
        }
    }
}
