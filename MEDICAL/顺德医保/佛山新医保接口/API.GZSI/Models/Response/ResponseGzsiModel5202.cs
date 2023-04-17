using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5202 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public List<Diseinfo> diseinfo { get; set; }

            public class Diseinfo
            {
                /// <summary>
                ///	诊断信息ID	
                /// <summary>
                [API.GZSI.Common.Display("诊断信息ID")]
                public string diag_info_id { get; set; }

                /// <summary>
                ///  就诊ID  
                /// <summary>
                [API.GZSI.Common.Display("就诊ID")]
                public string mdtrt_id { get; set; }

                /// <summary>
                ///  人员编号  
                /// <summary>
                [API.GZSI.Common.Display("人员编号")]
                public string psn_no { get; set; }

                /// <summary>
                ///  出入院诊断类别  
                /// <summary>
                [API.GZSI.Common.Display("出入院诊断类别")]
                public string inout_diag_type { get; set; }

                /// <summary>
                ///  诊断类别  
                /// <summary>
                [API.GZSI.Common.Display("诊断类别")]
                public string diag_type { get; set; }

                /// <summary>
                ///  主诊断标志  
                /// <summary>
                [API.GZSI.Common.Display("主诊断标志")]
                public string maindiag_flag { get; set; }

                /// <summary>
                ///  诊断排序号  
                /// <summary>
                [API.GZSI.Common.Display("诊断排序号")]
                public string diag_srt_no { get; set; }

                /// <summary>
                ///  诊断代码  
                /// <summary>
                [API.GZSI.Common.Display("诊断代码")]
                public string diag_code { get; set; }

                /// <summary>
                ///  诊断名称  
                /// <summary>
                [API.GZSI.Common.Display("诊断名称")]
                public string diag_name { get; set; }

                /// <summary>
                ///  入院病情  
                /// <summary>
                [API.GZSI.Common.Display("入院病情")]
                public string adm_cond { get; set; }

                /// <summary>
                ///  诊断科室  
                /// <summary>
                [API.GZSI.Common.Display("诊断科室")]
                public string diag_dept { get; set; }

                /// <summary>
                ///  诊断医生编码  
                /// <summary>
                [API.GZSI.Common.Display("诊断医生编码")]
                public string dise_dor_no { get; set; }

                /// <summary>
                ///  诊断医生姓名  
                /// <summary>
                [API.GZSI.Common.Display("诊断医生姓名")]
                public string dise_dor_name { get; set; }

                /// <summary>
                ///  诊断时间  
                /// <summary>
                [API.GZSI.Common.Display("诊断时间")]
                public string diag_time { get; set; }

                /// <summary>
                ///  经办人ID  
                /// <summary>
                [API.GZSI.Common.Display("经办人ID")]
                public string opter_id { get; set; }

                /// <summary>
                ///  经办人姓名  
                /// <summary>
                [API.GZSI.Common.Display("经办人姓名")]
                public string opter_name { get; set; }

                /// <summary>
                ///  经办时间  
                /// <summary>
                [API.GZSI.Common.Display("经办时间")]
                public string opt_time { get; set; }



            }
        }
    }
}
