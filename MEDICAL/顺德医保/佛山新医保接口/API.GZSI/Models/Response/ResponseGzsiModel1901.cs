using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel1901 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            public Fileinfo fileinfo { get; set; }
        }

        /// <summary>
        /// fileinfo类
        /// </summary>
        public class Fileinfo
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
            /// dld_endtime
            /// </summary>
            public string dld_endtime { get; set; }
        }
    }
}
