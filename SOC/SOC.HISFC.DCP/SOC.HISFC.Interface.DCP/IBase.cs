using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// ��Ϣ�ӿ�
    /// </summary>
    public interface IBase
    {
        int MsgCode
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        string Msg
        {
            get;
            set;
        }
    }
}
