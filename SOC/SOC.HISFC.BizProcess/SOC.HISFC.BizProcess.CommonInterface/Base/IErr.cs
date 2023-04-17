using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.CommonInterface.Base
{
    /// <summary>
    /// 错误信息接口
    /// </summary>
    public interface IErr
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string Err
        {
            get;
            set;
        }
    }
}
