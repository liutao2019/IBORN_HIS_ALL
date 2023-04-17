using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// ��ӡҽ�����ӿ�
    /// </summary>
    public interface IPrintOrder
    {
        /// <summary>
        /// ���û���
        /// </summary>
        /// <param name="patientInfo"></param>
        void SetPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo);
        /// <summary>
        /// ֱ�Ӵ�ӡ
        /// </summary>
        void Print();
        /// <summary>
        /// ��ӡ����
        /// </summary>
        void ShowPrintSet();
    }
}
