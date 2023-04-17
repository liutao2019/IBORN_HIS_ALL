using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 获取人员详细信息
    /// </summary>
    public class ResponseGzsiModel1160 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output对象
        /// </summary>
        public class Output
        {
            public List<SpInfo> spinfo { get; set; }
        }
        
    }
}
