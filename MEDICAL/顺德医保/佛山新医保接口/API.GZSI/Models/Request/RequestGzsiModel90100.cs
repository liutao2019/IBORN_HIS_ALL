using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
        /// <summary>
    /// 缴费查询
    /// </summary>
    public class RequestGzsiModel90100
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
        }
    }
}
