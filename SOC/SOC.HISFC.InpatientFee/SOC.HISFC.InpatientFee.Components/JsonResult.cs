using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Util
{
    //{962111D3-7FAB-470b-8941-1A6D16FEEFD2}
    public class JsonResult
    {
        public string code { get; set; }
        public string describe { get; set; }
        public List<Result> result { get; set; }

    }
}
