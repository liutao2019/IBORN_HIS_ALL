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
        /// <summary>
        /// 文件类型 01：文本、02：图片、03：视频；04：音频；05：其他
        /// </summary>
        public string file_qury_no { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 文件字节流
        /// </summary>
        public string dld_endtime { get; set; }
    }
}
