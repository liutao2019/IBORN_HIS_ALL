using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Equipment
{
    /// <summary>
    /// IOutRePrint<br></br>
    /// [��������: �豸���ⵥ����ӿ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-03-19]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IOutRePrint
    {
        void SetPrintData(List<FS.HISFC.Models.Equipment.Output> outputList);

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <returns>>�ɹ� 1 ʧ�� -1</returns>
        int PrintView();

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Print();
    }
}
