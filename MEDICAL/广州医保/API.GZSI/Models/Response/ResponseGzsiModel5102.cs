using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 医执人员信息查询
    /// </summary>
    public class ResponseGzsiModel5102:ResponseBase
    {
        #region Feedetail节点
        public List<Feedetail> feedetail { get; set; }
        public class Feedetail
        {
            /// <summary>
            ///人员证件类型
            /// <summary>
            public string psn_cert_type { get; set; }

            /// <summary>
            ///证件号码
            /// <summary>
            public string certno { get; set; }

            /// <summary>
            ///执业人员自编号
            /// <summary>
            public string prac_psn_no { get; set; }

            /// <summary>
            ///执业人员代码
            /// <summary>
            public string prac_psn_code { get; set; }

            /// <summary>
            ///执业人员姓名
            /// <summary>
            public string prac_psn_name { get; set; }

            /// <summary>
            ///执业人员分类
            /// <summary>
            public string prac_psn_type { get; set; }

            /// <summary>
            ///执业人员资格证书编码
            /// <summary>
            public string prac_psn_cert { get; set; }

            /// <summary>
            ///执业证书编号
            /// <summary>
            public string prac_cert_no { get; set; }

            /// <summary>
            ///医保医师标志
            /// <summary>
            public string hi_dr_flag { get; set; }

            /// <summary>
            ///开始时间
            /// <summary>
            public string begntime { get; set; }

            /// <summary>
            ///结束时间
            /// <summary>
            public string endtime { get; set; }

            /// <summary>
            ///变更原因
            /// <summary>
            public string chg_rea { get; set; }
        }
        #endregion
    }
}