using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.IHE
{
    /// <summary>
    /// [����������ADT�ӿ�]
    /// [�� �� �ߣ�Ѧ�Ľ�]
    /// [����ʱ�䣺2010-03-08]
    /// </summary>
    public interface IADT
    {
        /// <summary>
        /// �Ǽ�סԺ����
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int RegInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// Ԥ�Ǽ�סԺ����
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int PreRegInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        int PreRegOutpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �Ǽǳ�Ժ����
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int RegOutPatient(FS.HISFC.Models.Registration.Register patient);

        /// <summary>
        /// ����סԺ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int UpdatePatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ����סԺ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int TransferPatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ȡ��ת��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelTransferPatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ��Ժ����ת��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int InPatientToOutpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int OutpatientToInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �ϲ�����
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="patient2"></param>
        /// <returns></returns>
        int MergeInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.RADT.PatientInfo patient2);

        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int DischargeInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ȡ��ԤԼ�ǼǵĻ���
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelPreRegInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �޷���Ժ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelRegPatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �Ǽ��ٻ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelDischargePatientMessage(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ����סԺ����ת�����HL7��Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        FS.HISFC.Models.Registration.Register ProduceInpatientToOutPatientMessage(FS.HISFC.Models.RADT.PatientInfo patient);

    }
}
