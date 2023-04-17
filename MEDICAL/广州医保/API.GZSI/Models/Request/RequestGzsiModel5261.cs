using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel5261
    {

        public Data data { get; set; }
        public class Data
        {
            /// <summary>
            /// 	人员编号	 
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 	开始时间	 
            /// </summary>
            public string begntime { get; set; }
            /// <summary>
            /// 	结束时间	 
            /// </summary>
            public string endtime { get; set; }

        }
    }
}
