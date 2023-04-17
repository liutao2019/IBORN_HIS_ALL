using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
   
    public class ResponseGzsiModel5401:ResponseBase
    {
        public List<Bilgiteminfo> bilgiteminfo { get; set; }
        public class Bilgiteminfo
        {

            /// <summary>
            ///  人员编号
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///  报告单号
            /// <summary>
            public string rpotc_no { get; set; }

            /// <summary>
            ///  报告日期
            /// <summary>
            public string rpt_date { get; set; }

            /// <summary>
            ///  报告单类别代码
            /// <summary>
            public string rpotc_type_code { get; set; }

            /// <summary>
            ///  机构编号
            /// <summary>
            public string fixmedins_code { get; set; }

            /// <summary>
            ///  检查报告单名称
            /// <summary>
            public string exam_rpotc_name { get; set; }



            /// <summary>
            ///  检查结果阳性标志
            /// <summary>
            public string exam_rslt_poit_flag { get; set; }

            /// <summary>
            ///  检查/检验结果异常标志
            /// <summary>
            public string exam_rslt_abn { get; set; }

            /// <summary>
            /// 检查结论
            /// <summary>
            public string examCcls { get; set; }





        }
    }
}
