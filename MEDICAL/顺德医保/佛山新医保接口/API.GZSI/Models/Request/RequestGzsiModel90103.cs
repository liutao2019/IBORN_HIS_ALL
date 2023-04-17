using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 家庭病床登记查询
    /// </summary>
    public class RequestGzsiModel90103
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
