using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.Assembly.Models
{
    /// <summary>
    /// [功能描述: 程序集相关]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-6]<br></br>
    /// </summary>
    public class SOCPropert
    {
        private string propertName = "";

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name
        {
            get { return propertName; }
            set { propertName = value; }
        }
        private string propertValue = "";

        /// <summary>
        /// 属性值：字符串
        /// </summary>
        public string Value
        {
            get { return propertValue; }
            set { propertValue = value; }
        }
    }
}
