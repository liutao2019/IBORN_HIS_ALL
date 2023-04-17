using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.IHE
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
        int RegInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// Ԥ�Ǽ�סԺ����
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int PreRegInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        int PreRegOutpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �Ǽǳ�Ժ����
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int RegOutPatient(Neusoft.HISFC.Models.Registration.Register patient);

        /// <summary>
        /// ����סԺ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int UpdatePatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ����סԺ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int TransferPatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ȡ��ת��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelTransferPatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ��Ժ����ת��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int InPatientToOutpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int OutpatientToInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �ϲ�����
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="patient2"></param>
        /// <returns></returns>
        int MergeInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.RADT.PatientInfo patient2);

        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int DischargeInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ȡ��ԤԼ�ǼǵĻ���
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelPreRegInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �޷���Ժ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelRegPatient(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �Ǽ��ٻ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int CancelDischargePatientMessage(Neusoft.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ����סԺ����ת�����HL7��Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Neusoft.HISFC.Models.Registration.Register ProduceInpatientToOutPatientMessage(Neusoft.HISFC.Models.RADT.PatientInfo patient);

    }
}
