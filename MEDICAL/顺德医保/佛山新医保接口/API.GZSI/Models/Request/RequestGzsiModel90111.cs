using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel90111
    {
        #region Data节点
        public Data data { get; set; }

        public class Data
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
            /// <summary>
            /// 基层医疗机构标志
            /// </summary>
            public string grst_hosp_flag { get; set; }
            /// <summary>
            /// 医院所属区划
            /// </summary>
            public string fix_blng_admdvs { get; set; }
            /// <summary>
            /// 页码
            /// </summary>
            public string page_no { get; set; }
        }
        #endregion
    }
}
