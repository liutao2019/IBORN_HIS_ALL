using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 报告明细信息查询
    /// </summary>
    public class RequestGzsiModel5402
    {
        #region rptdetailinfo节点
        public Rptdetailinfo rptdetailinfo { get; set; }
        public class Rptdetailinfo
        {
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 报告单号 Y
            /// </summary>
            public string rpotc_no { get; set; }

            /// <summary>
            /// 机构编号 Y
            /// </summary>
            public string fixmedins_code { get; set; }
        }
        #endregion
    }
}
