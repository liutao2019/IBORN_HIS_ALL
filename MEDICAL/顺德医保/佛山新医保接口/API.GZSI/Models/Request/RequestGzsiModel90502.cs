using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 对数明细信息查询接口
    /// </summary>
    public class RequestGzsiModel90502
    {
        #region data节点
        public Data data { get; set; }
        public class Data
        {
            /// <summary>
            /// 	结算日期	 
            /// </summary>
            public string setl_time { get; set; }
            /// <summary>
            /// 	险种	 
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 	清算类别	 
            /// </summary>
            public string clr_type { get; set; }
            /// <summary>
            /// 	医疗类别	 
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 	页码	 
            /// </summary>
            public string pageNum { get; set; }

        }
        #endregion
    }
}
