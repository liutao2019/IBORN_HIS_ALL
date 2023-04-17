using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.DCP
{
    /// <summary>
    /// ��Ⱦ������ӿ���
    /// </summary>
    public interface IDCP
    {
        /// <summary>
        /// ���ߴ�Ⱦ������
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="patientType">��������</param>
        /// <returns></returns>
        int RegisterDiseaseReport(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Base.ServiceTypes patientType);

        /// <summary>
        /// ������д��Ⱦ������
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="patient"></param>
        /// <param name="patientType"></param>
        /// <param name="diagName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        int CheckDiseaseReport(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Base.ServiceTypes patientType, string diagName, out string msg);

        /// <summary>
        /// ��¼ʱ�Ƿ���ʾ���ϸ�ı��濨
        /// </summary>
        /// <param name="patientType"></param>
        /// <returns></returns>
        int LoadNotice(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.Base.ServiceTypes patientType);
    }
}
