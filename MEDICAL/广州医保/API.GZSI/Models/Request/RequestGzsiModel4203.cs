using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4203
    {
        #region 自费病人就诊信息(节点标识：input)

        /// <summary>
        /// 	医药机构就诊ID	
        /// </summary>
        public string fixmedins_mdtrt_id { get; set; }
        /// <summary>
        /// 	定点医药机构编号	
        /// </summary>
        public string fixmedins_code { get; set; }
        /// <summary>
        /// 	完成标志	
        /// </summary>
        public string cplt_flag { get; set; }

        #endregion
    }
}
