using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    public class CodeInfo
    {
        //以下按明细分列，包含业务信息明细codeinfo
        /// <summary>
        ///  字典类型代码  Y
        /// </summary>
        [API.GZSI.Common.Display("字典类型代码")]
        public string dic_type_code { get; set; }
        /// <summary>
        /// 地方字典值代码   Y
        /// </summary>
        [API.GZSI.Common.Display("地方字典值代码")]
        public string place_dic_val_code { get; set; }
        /// <summary>
        /// 地方字典值名称 Y
        /// </summary>
        [API.GZSI.Common.Display("地方字典值名称")]
        public string place_dic_val_name  { get; set; }
        /// <summary>
        /// 父级字典值代码
        /// </summary>
        [API.GZSI.Common.Display("父级字典值代码")]
        public string prnt_dic_val_code  { get; set; }
        /// <summary>
        /// 医保区划 Y
        /// </summary>
        [API.GZSI.Common.Display("医保区划")]
        public string admdvs { get; set; }
        /// <summary>
        /// 顺序号 
        /// </summary>
        [API.GZSI.Common.Display("顺序号")]
        public string seq { get; set; } 
        /// <summary>
        /// 版本号 
        /// </summary>
        [API.GZSI.Common.Display("版本号")]
        public string ver { get; set; }
    }
}
