using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Util
{
    //{E61C8FDD-81C2-434e-AF60-6B1BF1AF4CA2}
    public class JsonResult
    {
        public string code { get; set; }
        public string describe { get; set; }
        public List<Result> result { get; set; }

    }
}
