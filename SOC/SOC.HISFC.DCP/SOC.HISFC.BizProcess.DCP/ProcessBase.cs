using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.DCP
{
    public class ProcessBase
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string errorMsg;

        /// <summary>
        /// ������Ϣ
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
