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
    public class SOCClass
    {
        private string fullName = "";
       
        /// <summary>
        /// 类全名称
        /// </summary>
        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                this.fullName = value;
            }
        }

        private SOCDll dll = new SOCDll();

        /// <summary>
        /// dll信息
        /// </summary>
        public SOCDll Dll
        {
            get { return dll; }
            set { dll = value; }
        }
    }
}
