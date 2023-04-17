using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel4101A : ResponseBase
    {
        public Output output { get; set; }
        public class Output
        {
            /// <summary>
            ///清单流水号
            /// <summary>
            public string setl_list_id { get; set; }
        }
    }
}
