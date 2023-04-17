using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel2205
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 就诊 ID 
            /// <summary>
            public string mdtrt_id { get; set; }

            /// <summary>
            /// 收费批次号 传入“0000”删除所有未结算明细 
            /// <summary>
            public string chrg_bchno { get; set; }

            /// <summary>
            /// 人员编号
            /// <summary>
            public string psn_no { get; set; }
        }
    }
}
