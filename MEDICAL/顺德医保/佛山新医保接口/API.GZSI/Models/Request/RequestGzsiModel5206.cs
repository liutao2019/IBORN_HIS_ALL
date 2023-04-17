using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel5206
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            ///人员编号
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///累计年月
            /// <summary>
            public string cum_ym { get; set; }

        }
    }
}
