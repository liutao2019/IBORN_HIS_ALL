using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Account;

namespace FS.HISFC.BizProcess.Interface.Account
{
    /// <summary>
    /// ����Ԥ������Ϣ����Ԥ�����Ż���Ϣ
    /// </summary>
    public interface IAccountProcessPrepay
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int GetDerateCost(PrePay p ,ref string errText);
    }
}
