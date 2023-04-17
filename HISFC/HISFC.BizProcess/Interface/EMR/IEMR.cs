using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.EMR
{
    /// <summary>
    /// ��Ⱦ������ӿ���
    /// </summary>
    public interface IEMR
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrMsg
        {
            get;
        }

        /// <summary>
        /// ���Ӳ�������ӿ�
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="patientType">�������ͣ�0�������1סԺ</param>
        /// <returns></returns>
        int EMRRegister(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Base.ServiceTypes patientType);
    }
}
