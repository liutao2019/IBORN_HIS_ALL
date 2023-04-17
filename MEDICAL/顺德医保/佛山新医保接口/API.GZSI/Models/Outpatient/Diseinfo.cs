using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Outpatient
{
    /// <summary>
    /// 广州医保门诊单据式结算请求diseinfo
    /// </summary>
    public class Diseinfo
    {
        public List<DiseinfoRow> rows { get; set; }
    }
    public class DiseinfoRow
    {
        #region 诊断信息diseinfo
        /// <summary>
        /// 诊断类别
        /// </summary>
        public string dise_type { get; set; }
        /// <summary>
        /// 诊断排序号
        /// </summary>
        public string dise_srt_no { get; set; }
        /// <summary>
        /// 诊断代码
        /// </summary>
        public string dise_code { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string dise_name { get; set; }
        /// <summary>
        /// 诊断科室
        /// </summary>
        public string dise_dept { get; set; }
        /// <summary>
        /// 诊断医生编码
        /// </summary>
        public string dise_dor_no { get; set; }
        /// <summary>
        /// 诊断医生姓名
        /// </summary>
        public string dise_dor_name { get; set; }
        /// <summary>
        /// 诊断时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string dise_time { get; set; }
        #endregion
    }
}
