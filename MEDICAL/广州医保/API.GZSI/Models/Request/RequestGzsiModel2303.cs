using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 住院预结算
    /// </summary>
    public class RequestGzsiModel2303 
    {
        public Data data { get; set;}

        public class Data
        {
            /// <summary>
            ///人员编号 Y 
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///就诊凭证类型 Y 见【4码表说明】
            /// <summary>
            public string mdtrt_cert_type { get; set; }

            /// <summary>
            ///就诊凭证编号  就诊凭证类型为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// <summary>
            public string mdtrt_cert_no { get; set; }

            /// <summary>
            ///医疗费总额 Y 
            /// <summary>
            public string medfee_sumamt { get; set; }

            /// <summary>
            ///就诊ID Y 
            /// <summary>
            public string mdtrt_id { get; set; }

            /// <summary>
            ///险种类型  见【4码表说明】
            /// <summary>
            public string insutype { get; set; }

            /// <summary>
            ///医疗类别 Y 见【4码表说明】
            /// <summary>
            public string med_type { get; set; }

            /// <summary>
            ///个人账户使用标志 Y  1-使用
            /// <summary>
            public string acct_used_flag { get; set; }

            /// <summary>
            ///个人结算方式  Y  0-不使用
            /// <summary>
            public string psn_setlway { get; set; }

            /// <summary>
            ///中途结算标志  Y  见【4码表说明】
            /// <summary>
            public string mid_setl_flag { get; set; }

            /// <summary>
            ///医疗机构订单号或医疗机构就医序列号  1-是
            /// <summary>
            public string order_no { get; set; }

            /// <summary>
            ///就诊方式 Y 0-否
            /// <summary>
            public string mdtrt_mode { get; set; }

            /// <summary>
            ///持卡就诊基本信息  医院订单号
            /// <summary>
            public string hcard_basinfo { get; set; }

            /// <summary>
            ///持卡就诊校验信息  见【4码表说明】
            /// <summary>
            public string hcard_chkinfo { get; set; }
        }
    }
}
