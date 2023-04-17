using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 出院撤销
    /// </summary>
    public class RequestGzsiModel2405
    {
        /// <summary>
        /// 就诊ID Y  
        /// <summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 人员编号 Y  
        /// <summary>
        public string psn_no { get; set; }
    }
}
