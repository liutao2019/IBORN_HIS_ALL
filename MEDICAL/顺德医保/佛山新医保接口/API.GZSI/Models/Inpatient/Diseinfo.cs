using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Inpatient
{
    /// <summary>
    /// 广州医保门诊单据式结算请求diseinfo
    /// </summary>
    //public class Diseinfo
    //{
    //    public List<DiseinfoRow> rows { get; set; }
    //}

    public class Diseinfo
    {
        #region 诊断信息diseinfo
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 诊断类别
        /// </summary>
        public string diag_type { get; set; }
        /// <summary>
        /// 主诊断标志
        /// </summary>
        public string maindiag_flag { get; set; }
        /// <summary>
        /// 诊断排序号
        /// </summary>
        public string diag_srt_no { get; set; }
        /// <summary>
        /// 诊断代码
        /// </summary>
        public string diag_code { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string diag_name { get; set; }
        /// <summary>
        /// 入院病情
        /// </summary>
        public string adm_cond { get; set; }
        /// <summary>
        /// 诊断科室
        /// </summary>
        public string diag_dept { get; set; }
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
        public string diag_time { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string vali_flag { get; set; }
        #endregion
    }
}
