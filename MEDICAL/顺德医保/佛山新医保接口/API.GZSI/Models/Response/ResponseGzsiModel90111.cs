using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel90111 : ResponseBase
    {
        public Output output { get; set; }
        public class Output
        {
            public List<Data> data { get; set; }

            /// <summary>
            /// 总页数
            /// </summary>
            public string totalRow { get; set; }
            /// <summary>
            /// 总行数
            /// </summary>
            public string totalPage { get; set; }

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
                /// 统一社会信用代码
                /// </summary>
                public string uscc { get; set; }
                /// <summary>
                /// 医院等级
                /// </summary>
                public string hosp_lv { get; set; }
                /// <summary>
                /// 基层医疗机构标志
                /// </summary>
                public string grst_hosp_flag { get; set; }
                /// <summary>
                /// 医疗机构等级
                /// </summary>
                public string medinslv { get; set; }

            }
        }
    }
}
