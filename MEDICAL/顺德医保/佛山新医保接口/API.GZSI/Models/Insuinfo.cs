using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace API.GZSI.Models
{
    /// <summary>
    /// 广州医保患者信息获取
    /// </summary>
    public class Insuinfo
    {
        #region insuinfo节点
        /// <summary>
        /// 余额
        /// </summary>
        [API.GZSI.Common.Display("账户余额")]
        public string balc { get; set; }
        /// <summary>
        /// 险种类型 
        /// </summary>
        [API.GZSI.Common.Display("险种类型")]
        public string insutype { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        [API.GZSI.Common.Display("人员类别")]
        public string psn_type { get; set; }
        /// <summary>
        /// 公务员标志
        /// </summary>
        [API.GZSI.Common.Display("公务员标志")]
        public string cvlserv_flag { get; set; }
        /// <summary>
        /// 参保地医保区划
        /// </summary>
        [API.GZSI.Common.Display("参保地医保区划")]
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        [API.GZSI.Common.Display("单位名称")]
        public string emp_name { get; set; }
        #endregion
    }
}
