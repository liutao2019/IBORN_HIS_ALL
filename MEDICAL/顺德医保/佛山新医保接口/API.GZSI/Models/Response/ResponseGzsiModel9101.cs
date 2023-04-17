using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class ResponseGzsiModel9101 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            /// <summary>
            /// 文件查询号
            /// </summary>
            public string file_qury_no { get; set; }
            /// <summary>
            /// 文件名称
            /// </summary>
            public string filename { get; set; }
            /// <summary>
            /// 医药机构编号
            /// </summary>
            public string fixmedins_code { get; set; }
            /// <summary>
            /// 下载截止时间 yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string dld_endtime { get; set; }
        }
    }
}
