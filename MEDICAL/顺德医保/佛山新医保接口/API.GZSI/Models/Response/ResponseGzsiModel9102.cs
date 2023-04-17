using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 文件下载
    /// </summary>
    public class ResponseGzsiModel9102 : ResponseBase
    {
            ///// <summary>
            ///// 文件查询号
            ///// </summary>
            //public string file_qury_no { get; set; }
            ///// <summary>
            ///// 文件名称
            ///// </summary>
            //public string filename { get; set; }
            ///// <summary>
            ///// 文件类型 01：文本、02：图片、03：视频；04：音频；05：其他
            ///// </summary>
            //public string file_type { get; set; }
            ///// <summary>
            ///// 文件后缀
            ///// </summary>
            //public string file_sufx { get; set; }
            ///// <summary>
            ///// 文件字节流
            ///// </summary>
            //public string file_byte { get; set; }
            /// <summary>
            /// 文件字节流
            /// </summary>
            public byte[] output { get; set; }
    }
}
