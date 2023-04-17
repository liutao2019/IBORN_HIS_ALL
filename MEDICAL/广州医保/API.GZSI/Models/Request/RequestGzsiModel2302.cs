using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 住院费用明细撤销
    /// </summary>
    public class RequestGzsiModel2302 
    {
        /// <summary>
        /// 住院费用明细撤销
        /// </summary>
        public FeeInfo feeinfo { get; set; }

        public class FeeInfo
        {
            /// <summary>
            /// 费用流水号  
            /// <summary>
            public string feedetl_sn { get; set; }
            /// <summary>
            /// 就诊ID  
            /// <summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号  
            /// <summary>
            public string psn_no { get; set; }
        }
    }
}
