using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel1201
    {
        #region medinsinfo节点
        public Medinsinfo medinsinfo { get; set; }
        public class Medinsinfo
        {
            /// <summary>
            /// 定点医疗服务机构类型
            /// </summary>
            public string fixmedins_type { get; set; }
            /// <summary>
            /// 定点医药机构名称
            /// </summary>
            public string fixmedins_name { get; set; }
            /// <summary>
            /// 定点医药机构编号
            /// </summary>
            public string fixmedins_code { get; set; }
        }
        #endregion
    }
}
