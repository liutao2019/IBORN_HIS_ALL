using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 获取人员详细信息
    /// </summary>
    public class RequestGzsiModel1160 
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 开始时间 yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string begntime { get; set; }
            /// <summary>
            /// 医疗类别 Y 见【4码表说明】
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 持卡就诊基本信息
            /// </summary>
            public string hcard_basinfo { get; set; }
            /// <summary>
            /// 持卡就诊校验信息
            /// </summary>
            public string hcard_chkinfo { get; set; }
        }
    }
}
