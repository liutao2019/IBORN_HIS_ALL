using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 获取人员基本信息
    /// </summary>
    public class RequestGzsiModel1101 
    {
        public Data data { get; set; }

        /// <summary>
        /// Data类
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 就诊凭证类型 Y 见【4码表说明】
            /// </summary>
            public string mdtrt_cert_type { get; set; }

            /// <summary>
            /// 就诊凭证编码 Y
            ///01 - 电子凭证令牌
            ///02 - 身份证号
            ///03 - 社会保障卡卡号
            /// </summary>
            public string mdtrt_cert_no { get; set; }

            /// <summary>
            /// 卡识别码 Y 就诊凭证类型为“03”时必填
            /// </summary>
            public string card_sn { get; set; }

            /// <summary>
            /// 开始时间  获取历史参保信息时传入
            /// </summary>
            public string begntime { get; set; }

            /// <summary>
            /// 人员证件类型 Y
            /// </summary>
            public string psn_cert_type { get; set; }

            /// <summary>
            /// 证件号码 
            /// </summary>
            public string certno { get; set; }

            /// <summary>
            /// 人员姓名
            /// </summary>
            public string psn_name { get; set; }
        }
    }
}
