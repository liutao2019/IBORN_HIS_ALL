using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Equipment
{
    /// <summary>
    /// IApplyPrint<br></br>
    /// [��������: �豸���������ӡ�ӿ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-12-3]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IExamPrint
    {
        void SetPrintValue(Neusoft.HISFC.Models.Equipment.BuyExam buyExam);

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
