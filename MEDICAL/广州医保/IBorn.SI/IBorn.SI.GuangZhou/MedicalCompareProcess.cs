using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou
{
    /// <summary>
    /// 广州医保对照接口实现
    /// by 飞扬 2019-09-28
    /// </summary>
    public class MedicalCompareProcess : IBorn.SI.BI.ICompare
    {
        
        private string errorMsg;
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return this.errorMsg;
            }
        }

        
    }
}
