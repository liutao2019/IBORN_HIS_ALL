using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    /// 广州医保获取人员基本信息返回
    /// 
    /// </summary>
    public class Baseinfo
    {
        #region Baseinfo节点
        /// <summary>
        /// 人员编号
        /// </summary>
        [API.GZSI.Common.Display("人员编号")]
        public string psn_no { get; set; }
        /// <summary>
        /// 人员证件类型
        /// </summary>
        [API.GZSI.Common.Display("证件类型编号")]
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        [API.GZSI.Common.Display("证件号码")]
        public string certno { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        [API.GZSI.Common.Display("姓名")]
        public string psn_name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [API.GZSI.Common.Display("性别")]
        public string gend { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [API.GZSI.Common.Display("民族")]
        public string naty { get; set; }
        /// <summary>
        /// 出生日期 yyyy-MM-dd
        /// </summary>
        [API.GZSI.Common.Display("出生日期")]
        public string brdy { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [API.GZSI.Common.Display("年龄")]
        public string age { get; set; }
        #endregion
    }
}
