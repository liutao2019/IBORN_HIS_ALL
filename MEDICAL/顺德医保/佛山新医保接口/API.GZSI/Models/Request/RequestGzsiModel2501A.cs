using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel2501A
    {
        #region  请求

        public Refmedin refmedin { get; set; }
        public class Refmedin
        {

            /// <summary>
            ///人员编号 Y 
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///险种类型 Y 见【4码表说明】
            /// <summary>
            public string insutype { get; set; }

            /// <summary>
            ///联系电话  
            /// <summary>
            public string tel { get; set; }

            /// <summary>
            ///联系地址
            /// <summary>
            public string addr { get; set; }

            /// <summary>
            ///参保机构医保区划5
            /// <summary>
            public string insu_optins { get; set; }

            /// <summary>
            ///诊断代码
            /// <summary>
            public string diag_code { get; set; }

            /// <summary>
            ///诊断名称  
            /// <summary>
            public string diag_name { get; set; }

            /// <summary>
            ///疾病病情描述  
            /// <summary>
            public string dise_cond_dscr { get; set; }

            /// <summary>
            ///转往定点医药机构编号
            /// <summary>
            public string reflin_medins_no { get; set; }

            /// <summary>
            ///转往医院名称10
            /// <summary>
            public string reflin_medins_name { get; set; }

            /// <summary>
            ///就医地行政区划  
            /// <summary>
            public string mdtrtarea_admdvs { get; set; }

            /// <summary>
            ///医院同意转院标志
            /// <summary>
            public string hosp_agre_refl_flag { get; set; }

            /// <summary>
            ///转院类型
            /// <summary>
            public string refl_type { get; set; }

            /// <summary>
            ///转院日期
            /// <summary>
            public string refl_date { get; set; }

            /// <summary>
            ///转院原因15
            /// <summary>
            public string refl_rea { get; set; }

            /// <summary>
            ///转院意见 
            /// <summary>
            public string refl_opnn { get; set; }

            /// <summary>
            ///开始日期
            /// <summary>
            public string begndate { get; set; }

            /// <summary>
            ///结束日期
            /// <summary>
            public string enddate { get; set; }

            /// <summary>
            ///转院前就诊id
            /// <summary>
            public string refl_old_mdtrt_id { get; set; }

        }
        #endregion
    }
}
