using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// 信息接口
    /// </summary>
    public interface IBase
    {
        int MsgCode
        {
            get;
            set;
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        string Msg
        {
            get;
            set;
        }
    }
}
