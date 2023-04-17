using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
/// <summary>
    /// 缴费查询
    /// </summary>
    public class ResponseGzsiModel90100 : ResponseBase
    {
        public List<Output> output { get; set; }

        public class Output
        {
            /// <summary>
            ///  统筹区代码  
            /// <summary>
            [API.GZSI.Common.Display("统筹区代码")]
            public string poolarea_no { get; set; }

            /// <summary>
            ///  统筹区名称  
            /// <summary>
            [API.GZSI.Common.Display("统筹区名称")]
            public string poolarea_no_name { get; set; }

            /// <summary>
            ///  险种类型代码  
            /// <summary>
            [API.GZSI.Common.Display("险种类型代码")]
            public string insutype { get; set; }

            /// <summary>
            ///  险种类型名称  
            /// <summary>
            [API.GZSI.Common.Display("险种类型名称")]
            public string insutype_name { get; set; }

            /// <summary>
            ///  缴费类型  
            /// <summary>
            [API.GZSI.Common.Display("缴费类型")]
            public string clct_type { get; set; }

            /// <summary>
            ///  缴费类型名称  
            /// <summary>
            [API.GZSI.Common.Display("缴费类型名称")]
            public string clct_type_name { get; set; }

            /// <summary>
            ///  到账类型  
            /// <summary>
            [API.GZSI.Common.Display("到账类型")]
            public string clct_flag { get; set; }

            /// <summary>
            ///  到账类型名称  
            /// <summary>
            [API.GZSI.Common.Display("到账类型名称")]
            public string clct_flag_name { get; set; }

            /// <summary>
            ///  费款所属期开始日期  
            /// <summary>
            [API.GZSI.Common.Display("费款所属期开始日期")]
            public string accrym_begn { get; set; }

            /// <summary>
            ///  费款所属期结束日期  
            /// <summary>
            [API.GZSI.Common.Display("费款所属期结束日期")]
            public string accrym_end { get; set; }

            /// <summary>
            ///  到账时间  
            /// <summary>
            [API.GZSI.Common.Display("到账时间")]
            public string clct_time { get; set; }

        }
    }
}
