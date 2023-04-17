using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ת������ؼ��ӿ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2007-1-25]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface ITransferDeptApplyable
    {
        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo);
        /// <summary>
        /// ����ת�����
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject Dept { get;}

    
        /// <summary>
        /// ��ʾ
        /// </summary>
         System.Windows.Forms.DialogResult  ShowDialog();
    }
}
