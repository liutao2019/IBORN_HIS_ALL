using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    /// 广州医保患者信息获取
    /// </summary>
    public class Idetinfo
    {
        #region idetinfo节点
        /// <summary>
        /// 人员身份类别
        /// </summary>
        [API.GZSI.Common.Display("人员身份类别")]
        public string psn_idet_type { get; set; }
        /// <summary>
        /// 人员类别等级 
        /// </summary>
        [API.GZSI.Common.Display("人员类别等级")]
        public string psn_type_lv { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [API.GZSI.Common.Display("备注")]
        public string memo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [API.GZSI.Common.Display("开始时间")]
        public string begntime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [API.GZSI.Common.Display("结束时间")]
        public string endtime { get; set; }
        #endregion
    }
}
