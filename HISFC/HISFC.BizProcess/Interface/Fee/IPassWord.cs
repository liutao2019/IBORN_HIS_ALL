using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.InterFacePassWord
{
    public interface IPassWord
    {
        
        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        bool ValidPassWord
        {
            get;
        }
        /// <summary>
        /// ���￨��
        /// </summary>
        FS.HISFC.Models.RADT.Patient Patient
        {
            get;
            set;
        }
        /// <summary>
        /// �Ƿ���֤����
        /// </summary>
        bool IsOK
        {
            get;
        }
    }
}
