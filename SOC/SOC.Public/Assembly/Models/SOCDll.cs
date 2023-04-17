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
    public class SOCDll
    {
        private string name = "";

        /// <summary>
        /// 程序集信息
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

    }
}
