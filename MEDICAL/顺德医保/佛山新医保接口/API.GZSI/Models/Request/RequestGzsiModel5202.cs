using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel5202
    {
        public Data data { get; set; }

        public class Data
        {
           public string mdtrt_id{get;set;}	//就诊ID
           public string psn_no{get;set;}	//人员编号
        }
    }
}
