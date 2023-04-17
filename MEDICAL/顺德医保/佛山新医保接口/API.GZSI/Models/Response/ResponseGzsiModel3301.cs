using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel3301 : ResponseBase
    {
        public List<Output> output { get; set; }

        public class Output
        {
            /// <summary>
            /// 上传成功标志 
            /// </summary>
            public string CODE_SUCCESS { get; set; }
            /// <summary>
            /// 上传失败标志 
            /// </summary>
            public string CODE_ERROR { get; set; }
        }
        
    }
}
