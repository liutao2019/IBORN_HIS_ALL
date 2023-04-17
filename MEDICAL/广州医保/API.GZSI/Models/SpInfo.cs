using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    /// 获取人员详细信息返回
    /// </summary>
    public class SpInfo
    {
        #region spinfo节点
        /// <summary>
        /// 个人待遇申请号
        /// </summary>
        [API.GZSI.Common.Display("待遇申请号")]
        public string psn_trt_no { get; set; }
        /// <summary>
        /// 个人待遇申请号类型
        /// </summary>
        [API.GZSI.Common.Display("申请类型编码")]
        public string psn_trt_type { get; set; }
        /// <summary>
        /// 个人待遇申请号类型名称
        /// </summary>
        [API.GZSI.Common.Display("申请类型名称")]
        public string psn_trt_type_name { get; set; }
        /// <summary>
        /// 病种编码
        /// </summary>
        [API.GZSI.Common.Display("病种编码")]
        public string dise_no { get; set; }
        /// <summary>
        /// 病种名称
        /// </summary>
        [API.GZSI.Common.Display("病种名称")]
        public string dise_name { get; set; }
        /// <summary>
        /// 开始时间  yyyy-MM-dd HH:mm:ss  
        /// </summary>
        [API.GZSI.Common.Display("开始时间")]
        public string begntime { get; set; }
        /// <summary>
        /// 结束时间  yyyy-MM-dd HH:mm:ss   
        /// </summary>
        [API.GZSI.Common.Display("结束时间")]
        public string endtime { get; set; }
        #endregion
    }
}
