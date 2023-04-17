using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel5201
    {
        public Data data { get; set; }

        public class Data
        {
            public string psn_no { get; set; }	//	人员编号
            public string begntime { get; set; }	//	开始时间
            public string endtime { get; set; }	//	结束时间
            public string med_type { get; set; }	//	医疗类别
            public string mdtrt_id { get; set; }	//	就诊ID
        }
    }
}
