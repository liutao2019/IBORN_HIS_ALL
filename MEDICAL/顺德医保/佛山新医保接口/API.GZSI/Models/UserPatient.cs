using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    public class UserPatient
    {
        public List<UserPatientRow> Row { get; set; }
    }
    public class UserPatientRow
    {
        #region Baseinfo节点
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 人员证件类型
        /// </summary>
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string certno { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string psn_name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string gend { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string naty { get; set; }
        /// <summary>
        /// 出生日期 yyyy-MM-dd
        /// </summary>
        public string brdy { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }
        #endregion

        //#region insuinfo节点
        ///// <summary>
        ///// 余额
        ///// </summary>
        //public string balc { get; set; }
        ///// <summary>
        ///// 险种类型 
        ///// </summary>
        //public string insutype { get; set; }
        ///// <summary>
        ///// 人员类别
        ///// </summary>
        //public string psn_type { get; set; }
        ///// <summary>
        ///// 公务员标志
        ///// </summary>
        //public string cvlserv_flag { get; set; }
        ///// <summary>
        ///// 参保地医保区划
        ///// </summary>
        //public string insuplc_admdvs { get; set; }
        ///// <summary>
        ///// 单位名称
        ///// </summary>
        //public string emp_name { get; set; }
        //#endregion

        //#region idetinfo节点
        ///// <summary>
        ///// 余额
        ///// </summary>
        //public string psn_idet_type { get; set; }
        ///// <summary>
        ///// 险种类型 
        ///// </summary>
        //public string psn_type_lv { get; set; }
        ///// <summary>
        ///// 人员类别
        ///// </summary>
        //public string memo { get; set; }
        ///// <summary>
        ///// 公务员标志
        ///// </summary>
        //public string begntime { get; set; }
        ///// <summary>
        ///// 参保地医保区划
        ///// </summary>
        //public string endtime { get; set; }
        //#endregion
    }
}
