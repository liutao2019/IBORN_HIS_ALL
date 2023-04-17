using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 文件下载
    /// </summary>
    public class RequestGzsiModel9102 
    {
        public FsDownloadIn fsDownloadIn { get; set; }

        public class FsDownloadIn
        {
            /// <summary>
            /// 文件查询号
            /// </summary>
            public string file_qury_no { get; set; }
            /// <summary>
            /// 文件名称
            /// </summary>
            public string filename { get; set; }

            public string fixmedins_code { get; set; }
        }
    }
}
