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
        /// 代码类别名称	字符	32		Y	(略)
        /// </summary>
        public string aaa101 { get; set; } //	代码类别名称	字符	32		Y	(略)
        /// <summary>
        /// 代码类别	字符	20		Y	(略)
        /// </summary>
        public string aaa100 { get; set; } //	代码类别	字符	20		Y	(略)
        /// <summary>
        /// 代码码值名称	字符	32		Y	(略)
        /// </summary>
        public string aaa103 { get; set; } //	代码码值名称	字符	32		Y	(略)
        /// <summary>
        /// 代码码值	字符	256		Y	(略)
        /// </summary>
        public string aaa102 { get; set; } //	代码码值	字符	256		Y	(略)
        /// <summary>
        /// 预留（扩展）	字符	6	Y		见码表：baa027
        /// </summary>
        public string aaa027 { get; set; } //	预留（扩展）	字符	6	Y		见码表：baa027
        /// <summary>
        /// 预留（扩展）	字符	64			(略)
        /// </summary>
        public string aaa104 { get; set; } //	预留（扩展）	字符	64			(略)
    }
}
