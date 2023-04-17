using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel90203 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public Result result { get; set; }

            public class Result
            {
                /// <summary>
                ///待遇申报明细流水号
                /// <summary>
                public string trt_dcla_detl_sn { get; set; }
            }

        }
    }
}
