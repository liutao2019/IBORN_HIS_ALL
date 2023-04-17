using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Interface.Common
{
    /// <summary>
    /// ��ȡ��Ŀ��չ��Ϣ�ӿ�
    /// ��ȡҽ�������Ѷ�����Ϣ
    /// </summary>
    public interface IItemCompareInfo
    {
        /// <summary>
        /// ��ȡҽ��������Ŀ
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="compareItem"></param>
        /// <returns></returns>
        int GetCompareItemInfo(FS.HISFC.Models.Base.Item item, FS.HISFC.Models.Base.PactInfo pactInfo, ref FS.HISFC.Models.SIInterface.Compare compare, ref string strCompareInfo);

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrInfo
        {
            get;
            set;
        }
    }
}
