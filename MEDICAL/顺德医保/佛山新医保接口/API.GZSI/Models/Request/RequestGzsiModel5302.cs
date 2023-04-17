using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel5302
    {
        public Data data { get; set; }

        public class Data
        {
            public string psn_no { get; set; }//人员编号
            public string biz_appy_type { get; set; } //业务申请类型
        }
    }
}
