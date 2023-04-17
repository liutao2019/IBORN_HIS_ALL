using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface
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
        void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo);
        /// <summary>
        /// ����ת�����
        /// </summary>
        FS.FrameWork.Models.NeuObject Dept { get;}

    
        /// <summary>
        /// ��ʾ
        /// </summary>
         System.Windows.Forms.DialogResult  ShowDialog();
    }
}
