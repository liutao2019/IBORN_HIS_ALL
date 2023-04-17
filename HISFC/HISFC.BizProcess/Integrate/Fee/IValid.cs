using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Integrate.FeeInterface
{

    public interface IFeeExtend 
    {
        /// <summary>
        /// ������֤�Ϸ���
        /// </summary>
        /// <param name="feeItemList">��ǰ�շ���Ŀ��Ϣ</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>true�Ϸ� false���Ϸ�</returns>
        bool IsValid(Neusoft.HISFC.Object.Fee.Inpatient.FeeItemList feeItemList, ref string errText);


    }
    
    
    /// <summary>
    /// סԺ�Ǽ���չ��Ϣ
    /// </summary>
    public interface IRegisterExtend 
    {
        /// <summary>
        /// �����ж�����ĺϷ���
        /// </summary>
        /// <returns>�ɹ�: true ʧ��: false</returns>
        bool IsInputValid(System.Windows.Forms.Control errControl, ref string errText);

        /// <summary>
        /// ��ָ����TabIndex�ؼ�֮�󵯳���չ����
        /// </summary>
        /// <param name="tabIndex">ָ����TabIndex</param>
        /// <param name="patient">��ǰ�Ļ��߻�����Ϣʵ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ��: -1</returns>
        int OpenExtendInputWindow(int tabIndex, Neusoft.HISFC.Object.RADT.Patient patient, ref string errText);

        /// <summary>
        /// ����и�����Ϣ¼��,��ô��ø�¼����Ϣ
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ��: -1</returns>
        int GetExtentPatientInfomation(Neusoft.HISFC.Object.RADT.Patient patient, ref string errText);

        /// <summary>
        /// ����и�����Ϣ,���Ҳ���PatientInfoʵ�岢����Ҫ�µ�ҵ������ʱ��,
        /// ��������ظ�,�Լ�д����.
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <param name="t">��ǰ�����ݿ�����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������� 0</returns>
        int InsertOtherInfomation(Neusoft.HISFC.Object.RADT.Patient patient, Neusoft.NFC.Management.Transaction t, ref string errText);

        /// <summary>
        /// ����и�����Ϣ,���Ҳ���PatientInfoʵ�岢����Ҫ�µ�ҵ����µ�ʱ��,
        /// ��������ظ�,�Լ�д����.
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <param name="t">��ǰ�����ݿ�����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������� 0</returns>
        int UpdateOtherInfomation(Neusoft.HISFC.Object.RADT.Patient patient, Neusoft.NFC.Management.Transaction t, ref string errText);

        /// <summary>
        /// ���������Ϣ,���������Ŀؼ���
        /// </summary>
        void ClearOtherInfomation();

        /// <summary>
        /// ��ʼ����չ��Ϣ
        /// </summary>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int InitExtendInfomation(ref string errText);
    }
}
