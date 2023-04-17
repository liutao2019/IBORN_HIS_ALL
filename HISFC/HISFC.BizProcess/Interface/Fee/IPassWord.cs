using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.InterFacePassWord
{
    public interface IPassWord
    {
        
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <returns></returns>
        bool ValidPassWord
        {
            get;
        }
        /// <summary>
        /// 门诊卡号
        /// </summary>
        FS.HISFC.Models.RADT.Patient Patient
        {
            get;
            set;
        }
        /// <summary>
        /// 是否验证密码
        /// </summary>
        bool IsOK
        {
            get;
        }
    }
}
