using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel3507
    {
        public Data data { get; set; }
        public class Data
        {

            /// <summary>
            /// 定点医药机构批次流水号 Y　
            /// <summary>
            public string fixmedins_bchno { get; set; }

            /// <summary>
            /// 进销存数据类型 Y　
            /// <summary>
            public string inv_data_type { get; set; }
        }
    }
}
