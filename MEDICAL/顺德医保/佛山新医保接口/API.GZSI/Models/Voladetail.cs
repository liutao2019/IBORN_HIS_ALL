using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    /// 结算返回违规费用明细voladetail
    /// </summary>
    public class Voladetail
    {
        #region 违规费用明细voladetail
        /// <summary> 
        /// 就诊ID 
        /// </summary> 
        public string mdtrt_id { get; set; }
        /// <summary> 
        /// 医疗机构目录编码
        /// </summary> 
        public string medins_list_codg { get; set; }
        /// <summary> 
        /// 医疗机构目录名称 
        /// </summary> 
        public string medins_list_name { get; set; }
        /// <summary> 
        /// 医疗目录编码 
        /// </summary> 
        public string med_list_codg { get; set; }
        /// <summary> 
        /// 违规类型 
        /// </summary> 
        public string vola_type { get; set; }
        /// <summary> 
        /// 违规说明
        /// </summary> 
        public string vola_dscr { get; set; }
        #endregion
    }
}
