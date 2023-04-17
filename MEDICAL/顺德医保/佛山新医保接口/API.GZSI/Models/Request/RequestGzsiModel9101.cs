using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class RequestGzsiModel9101 
    {
        public FsUploadIn fsUploadIn { get; set; }

        public class FsUploadIn
        {
            /// <summary>
            /// 文件数据
            /// </summary>
            public List<int> @in { get; set; }
            /// <summary>
            /// 文件名
            /// </summary>
            public string filename { get; set; }
            /// <summary>
            /// 医药机构编号
            /// </summary>
            public string fixmedins_code { get; set; }
        }
    }
}
