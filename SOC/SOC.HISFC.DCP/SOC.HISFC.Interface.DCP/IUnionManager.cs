using System;
using System.Collections.Generic;
using System.Text;
using FS.SOC.HISFC.DCP.Enum;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// UnionManager<br></br>
    /// [��������: ��������Ԥ�������ӿ�]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2008-8-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IUnionManager
    {
        /// <summary>
        /// ��������Ԥ������ӿں���
        /// �½������ϱ�������Ԥ��������
        /// </summary>
        /// <param name="patientType">��������</param>
        /// <returns></returns>
        ReportOperResult RegisterReport(System.Windows.Forms.IWin32Window owner, PatientType patientType);

        /// <summary>
        /// ��������Ԥ������ӿڱ�������
        /// �ϱ�ĳ���߼��������Ԥ��������
        /// </summary>
        /// <param name="patient">����ʵ��</param>
        /// <param name="patientType">��������</param>
        /// <returns></returns>
        ReportOperResult RegisterReport(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, PatientType patientType);

        /// <summary>
        /// ��������Ԥ������ӿں���
        /// �����������ȷ���Ƿ��ϱ�������Ԥ��������
        /// </summary>
        /// <param name="owner">������</param>
        /// <param name="patient">����ʵ��</param>
        /// <param name="patientType">��������</param>
        /// <param name="diagName">�������</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <returns></returns>
        ReportOperResult CheckDisease(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, PatientType patientType, string diagName, out string msg);

        /// <summary>
        /// ��ȡ��������Ԥ���ķ�����Ϣ
        /// </summary>
        /// <returns></returns>
        int GetDCPNotice(System.Windows.Forms.IWin32Window owner, PatientType patientType);
    }
}
