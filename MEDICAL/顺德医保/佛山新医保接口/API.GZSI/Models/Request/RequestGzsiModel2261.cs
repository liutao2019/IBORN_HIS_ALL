﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 门诊单据式结算撤销
    /// </summary>
    public class RequestGzsiModel2261
    {
        public Mdtrtinfo mdtrtinfo { get; set; }

        public class Mdtrtinfo
        {
            /// <summary>
            /// 结算ID Y
            /// </summary>
            public string setl_id { get; set; }
            /// <summary>
            /// 就诊ID Y
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no { get; set; }
        }
    }
}
