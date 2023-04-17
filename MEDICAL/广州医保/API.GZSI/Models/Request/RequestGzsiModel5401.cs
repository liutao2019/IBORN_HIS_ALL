using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 输血信息
    /// </summary>
    public class RequestGzsiModel5401
    {
        public Bilgiteminfo bilgiteminfo { get; set; }
        public class Bilgiteminfo
        {
            
            /// <summary>
            /// 人员编号 
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            /// 检查机构代码
            /// <summary>
            public string exam_org_code { get; set; }

            /// <summary>
            /// 检查机构名称
            /// <summary>
            public string exam_org_name { get; set; }

            /// <summary>
            /// 检查-项目代码
            /// <summary>
            public string exam_item_code { get; set; }

            /// <summary>
            ///检查-项目名称
            /// <summary>
            public string exam_item_name { get; set; }


        }
    }
}
