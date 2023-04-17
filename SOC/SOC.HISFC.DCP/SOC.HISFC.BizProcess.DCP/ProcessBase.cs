using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.DCP
{
    public class ProcessBase
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        private string errorMsg;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }
            set
            {
                errorMsg = value;
            }
        }


    }
}
