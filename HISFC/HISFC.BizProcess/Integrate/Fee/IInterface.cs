using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FS.HISFC.BizProcess.Interface.FeeInterface;
using System.Data;

namespace FS.HISFC.BizProcess.Integrate.FeeInterface
{
    #region ����

    /* �������Ƶ�Interface��)
    /// <summary>
    /// IConnect ��ժҪ˵����
    /// </summary>
    public interface IMedcareTranscation
    {
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        long Connect();

        /// <summary>
        /// ���ݿ�ر�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        long Disconnect();

        /// <summary>
        /// ���ݿ��ύ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        long Commit();

        /// <summary>
        /// ���ݿ�ع�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        long Rollback();

        /// <summary>
        /// ��ʼ���ݿ�����
        /// </summary>
        void BeginTranscation();
    }
    
    /// <summary>
    /// ҽ���ӿ�
    /// </summary>
    public interface IMedcare : IMedcareTranscation
    {
        /// <summary>
        /// ������������
        /// </summary>
        System.Data.IDbTransaction Trans
        {
            set;
        }
        /// <summary>
        /// ���ñ������ݿ�����
        /// </summary>
        /// <param name="t"></param>
        void SetTrans(System.Data.IDbTransaction t);

        /// <summary>
        /// �������
        /// </summary>
        string ErrCode
        {
            get;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrMsg
        {
            get;
        }

        /// <summary>
        /// �ؼ������������д��
        /// </summary>
        string Description
        {
            get;
        }

        #region ����

        /// <summary>
        /// ��ú�������Ϣ
        /// </summary>
        /// <param name="blackLists">��������Ϣ</param>
        /// <returns>�ɹ� >= 1 ʧ�� -1 û�л������ 0</returns>
        int QueryBlackLists(ref ArrayList blackLists);

        /// <summary>
        /// ��֤��Ժ�����Ƿ����ں�����
        /// </summary>
        /// <param name="patient">��Ժ���߻�����Ϣ</param>
        /// <returns>�ں������� true ���ں�������false</returns>
        bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ��֤���ﻼ���Ƿ����ں�����
        /// </summary>
        /// <param name="r">���ﻼ�߻�����Ϣ</param>
        /// <returns>�ں������� true ���ں�������false</returns>
        bool IsInBlackList(FS.HISFC.Models.Registration.Register r);

        /// <summary>
        /// ��÷�ҩƷ��ϢĿ¼
        /// </summary>
        /// <param name="undrugLists">��ҩƷ��ϢĿ¼</param>
        /// <returns>�ɹ� >= 1 ʧ��: -1 û�л������ 0</returns>
        int QueryUndrugLists(ref ArrayList undrugLists);

        /// <summary>
        /// ���ҩƷ��ϢĿ¼
        /// </summary>
        /// <param name="drugLists">ҩƷ��ϢĿ¼</param>
        /// <returns>�ɹ� >= 1 ʧ��: -1 û�л������ 0</returns>
        int QueryDrugLists(ref ArrayList drugLists);

        #endregion

        #region ����
        /// <summary>
        /// �������ʱ�Ƿ������ϴ�
        /// </summary>
        /// <remarks>true �����ϴ� false �����ϴ�</remarks>
        bool IsUploadAllFeeDetailsOutpatient
        {
            get;
        }

        /// <summary>
        /// ����Һź���
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r);

        /// <summary>
        /// ȡ����Ժ�ǼǷ���
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�м�¼ 0</returns>
        int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r);
        /// <summary>
        /// ���ҽ���Һ���Ϣ
        /// </summary>
        /// <param name="r">����Һ�ʵ��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r);

        /// <summary>
        /// �����ϴ�������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="f">���������ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f);

        /// <summary>
        /// �����ϴ�������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeDetails">������ϸʵ�弯��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);

        /// <summary>
        /// ɾ�������Ѿ��ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="f">������ϸ��Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f);

        /// <summary>
        /// ɾ�����ߵ����з����ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r);

        /// <summary>
        /// ɾ��ָ�����ݼ�����ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫɾ���ķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);

        /// <summary>
        /// �޸ĵ����������ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="f">Ҫ�޸ĵķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f);

        /// <summary>
        /// �޸Ķ����������ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫ�޸ĵķ���ʵ����ϸ����</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);

        /// <summary>
        /// ҽ��Ԥ����
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);

        /// <summary>
        /// ҽ������
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫȡ������Ļ��߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);


        /// <summary>
        /// ȡ������(����)
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫȡ������Ļ��߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);

        #endregion

        #region סԺ

        /// <summary>
        /// ���·�����Ϣ
        /// /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="f">������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�и��µ����� 0</returns>
        int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f);
        
        /// <summary>
        /// ���¼��������ϸ
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣ</param>
        /// <param name="feeItemList">סԺ���õ�����ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList);

        /// <summary>
        /// סԺ�ǼǺ���
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ��߻�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ȡ����Ժ�ǼǷ���
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�м�¼ 0</returns>
        int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ��Ժ�ٻط���
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�м�¼ 0</returns>
        int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ���ҽ��סԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ��Ժ�ǼǷ���
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�м�¼ 0</returns>
        int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// �����ϴ�������ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="f">סԺ������ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f);

        /// <summary>
        /// �����ϴ�������ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">������ϸʵ�弯��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        /// <summary>
        /// ɾ�������Ѿ��ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="f">������ϸ��Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f);

        /// <summary>
        /// ɾ�����ߵ����з����ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// ɾ��ָ�����ݼ�����ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">Ҫɾ���ķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        /// <summary>
        /// �޸ĵ���סԺ���ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="f">Ҫ�޸ĵķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f);

        /// <summary>
        /// �޸Ķ���סԺ���ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">Ҫ�޸ĵķ���ʵ����ϸ����</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        /// <summary>
        /// סԺҽ��Ԥ����
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        /// <summary>
        /// סԺҽ����;����
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        /// <summary>
        /// ҽ������
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">Ҫȡ������Ļ��߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        #endregion

    }


    public interface IFeeExtend 
    {
        /// <summary>
        /// ������֤�Ϸ���
        /// </summary>
        /// <param name="feeItemList">��ǰ�շ���Ŀ��Ϣ</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>true�Ϸ� false���Ϸ�</returns>
        bool IsValid(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList, ref string errText);

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
        int OpenExtendInputWindow(int tabIndex, FS.HISFC.Models.RADT.Patient patient, ref string errText);

        /// <summary>
        /// ����и�����Ϣ¼��,��ô��ø�¼����Ϣ
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ��: -1</returns>
        int GetExtentPatientInfomation(FS.HISFC.Models.RADT.Patient patient, ref string errText);

        /// <summary>
        /// ����и�����Ϣ,���Ҳ���PatientInfoʵ�岢����Ҫ�µ�ҵ������ʱ��,
        /// ��������ظ�,�Լ�д����.
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <param name="t">��ǰ�����ݿ�����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������� 0</returns>
        int InsertOtherInfomation(FS.HISFC.Models.RADT.Patient patient, FS.FrameWork.Management.Transaction t, ref string errText);

        /// <summary>
        /// ����и�����Ϣ,���Ҳ���PatientInfoʵ�岢����Ҫ�µ�ҵ����µ�ʱ��,
        /// ��������ظ�,�Լ�д����.
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <param name="t">��ǰ�����ݿ�����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������� 0</returns>
        int UpdateOtherInfomation(FS.HISFC.Models.RADT.Patient patient, ref string errText);

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

    public interface IBackFeeRecipePrint
    {
        FS.HISFC.Models.Registration.Register Patient
        {
            set;
        }

        int SetData(ArrayList alBackFee);

        void Print();
    }

    #region Ԥ�����ӡ

    /// <summary>
    /// Ԥ����Ʊ��ӡ�ӿ�
    /// </summary>
    public interface IPrepayPrint 
    {
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        System.Data.IDbTransaction Trans
        {
            get;
            set;
        }
        
        /// <summary>
        /// ���÷�Ʊ��ӡ����
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <param name="prepay">Ԥ�����ӡʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int SetValue(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.Prepay prepay);

        /// <summary>
        /// Ԥ�����ӡ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Print();

        /// <summary>
        /// ��յ�ǰ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Clear();

        /// <summary>
        /// ���ñ������ݿ�����
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        void SetTrans(System.Data.IDbTransaction trans);
    }

    #endregion

    #region "סԺ��Ʊ��ӡ"

    public interface IBalanceInvoicePrint
    {
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        System.Data.IDbTransaction Trans
        {
            get;
            set;
        }

        #region liuqiang 2007-8-23 �޸�
        #region �޸�ԭ��
        //>     ���ڴ�ӡ���������⡣���ող�������峬���ġ����԰���ͬ��λά����
        //> 
        //> ������ҽԺҽ�����������������ַ�Ʊ������ͬ��λȫ��ҽ������ͨ��medicaltype��������ͨҽ�������������ա�
        //> ��������£�ͨ����ͬ��λά���÷�ʽ��û�������ˡ�
        //> 
        //> ������ �� ��Ʊ��ӡ�Ľӿ������������ԡ�һ��patientinfo��һ��invoicetype��
        //> ��Ʊ��ӡʱ����������ȶ�patientinfo���и�ֵ���ӿ�ʵ��ʱ����patientinfo������invoicetype��ֵ��
        //> Ȼ�������ȡ��invoicetype��ֵ���������ֵ�ں����ڽ��д�������������balancelist��
        //> 
        //> �㿴������ɶ����û�����⣬������ô���� 
        #endregion
        /// <summary>
        /// �����ۺ�ʵ��
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set;
        }
        /// <summary>
        /// ��Ʊ�������࣬�硰ZY01����ZY02����ZY03����
        /// </summary>
        string InvoiceType
        {
            get;
        } 
        #endregion

        /// <summary>
        /// ����סԺ��Ʊ��ӡ����
        /// </summary>
        /// <param name="patientInfo">סԺ���߻�����Ϣʵ��</param>
        /// <param name="balanceInfo">�����ӡʵ��</param>
        /// <param name="alBalanceList">���������ϸ����</param>
        /// /// <param name="alPayList">֧����ʽ</param>{A9A96DBA-B9D1-4227-9336-B4D5BBC42B4A}
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList, ArrayList alPayList);

        /// <summary>
        /// ����סԺ��ƱԤ����ӡ����
        /// </summary>
        /// <param name="patientInfo">סԺ���߻�����Ϣʵ��</param>
        /// <param name="balanceInfo">�����ӡʵ��</param>
        /// <param name="alBalanceList">���������ϸ����</param>
        /// <param name="alPayList">֧����ʽ</param>{A9A96DBA-B9D1-4227-9336-B4D5BBC42B4A}
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList, ArrayList alPayList);
        /// <summary>
        /// סԺ��Ʊ��ӡ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Print();

        /// <summary>
        /// סԺ��Ʊ��ӡԤ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int PrintPreview();

        /// <summary>
        /// ��յ�ǰ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Clear();

        /// <summary>
        /// ���ñ������ݿ�����
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        void SetTrans(System.Data.IDbTransaction trans);
    }
    public interface IBalanceInvoicePrintmy :IBalanceInvoicePrint
    {
        EBlanceType IsMidwayBalance
        {
            get;
            set;
        }

    }
    #endregion
    #region �߿��ӡ�ӿ�
    public interface IMoneyAlert
    {
        //������Ϣ
        FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get;
            set;
        }
        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        void SetPatientInfo();
    }

    #endregion

    */

    #endregion


    #region �����շ�

    #region ������Ŀѡ��

    /// <summary>
    /// ��ѡ����Ŀ�󴥷�
    /// ���Ӽ۸����������շ�{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
    /// </summary>
    /// <param name="itemCode"></param>
    /// <param name="drugFlag"></param>
    /// <param name="exeDeptCode"></param>
    public delegate void WhenGetItem(string itemCode, string drugFlag, string exeDeptCode, decimal price);

    /// <summary>
    /// ����ѡ����Ŀ�ؼ��ӿ�
    /// </summary>
    public interface IChooseItemForOutpatient 
    {
        /// <summary>
        /// ѡ��ǰ��Ŀ
        /// </summary>
        /// <returns></returns>
        int GetSelectedItem();
        
        /// <summary>
        /// ����������Ŀ��DdataSet
        /// </summary>
        /// <param name="dsItem">����Ҫ���˵���Ŀ����</param>
        void SetDataSet(System.Data.DataSet dsItem);

        /// <summary>
        /// ����ǰ̨����Ĺ����ַ�
        /// </summary>
        /// <param name="sender">����Ķ���</param>
        /// <param name="inputChar">ǰ̨����Ĺ����ַ�</param>
        /// <param name="inputType">����</param>
        void SetInputChar(object sender, string inputChar, FS.HISFC.Models.Base.InputTypes inputType);

        /// <summary>
        /// ���ѡ�е���Ŀ
        /// </summary>
        /// <param name="item">ѡ�е���Ŀ</param>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        int GetSelectedItem(ref FS.HISFC.Models.Base.Item item);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p">�ؼ�����</param>
        void SetLocation(System.Drawing.Point p);

        /// <summary>
        /// ����ʽ
        /// </summary>
        string InputPrev { get; set; }

        /// <summary>
        /// ���˷�ʽ
        /// </summary>
        string QueryType { get; set; }

        /// <summary>
        /// ��ѡ����Ŀ�󴥷�
        /// </summary>
        event WhenGetItem SelectedItem;

        /// <summary>
        /// �Ƿ�ģ����ѯ
        /// </summary>
        bool IsQueryLike { get; set; }

        /// <summary>
        /// ��ǰ���Ҵ���
        /// </summary>
        string DeptCode { get; set; }
        
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        FS.HISFC.Models.Base.ItemKind ItemKind{ get; set; }

        /// <summary>
        /// Ĭ��ÿҳ��ʾ9����¼���Ժ������������
        /// </summary>
        int ItemCount { get; set; }

        /// <summary>
        /// ��ѡ��һ����Ŀ��ʱ���Ƿ�رմ���
        /// </summary>
        bool IsSelectAndClose { get; set; }

        /// <summary>
        /// ��ǰѡ������Ŀʵ��
        /// </summary>
        FS.HISFC.Models.Base.Item NowItem { get; set; }

        /// <summary>
        /// ������˺�ĵ���ĿfpSheet
        /// </summary>
        object ObjectFilterObject { set; get; }

        /// <summary>
        /// �Ƿ�ѡ����Ŀ
        /// </summary>
        bool IsSelectItem { get; set; }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns></returns>
        int Init();

        /// <summary>
        /// �Ƿ�ѡ����Ŀ��ͬʱ�жϿ��
        /// </summary>
        bool IsJudgeStore
        {
            get;
            set;
        }

        /// <summary>
        /// �����ַ�����������Ŀ��ʽ
        /// </summary>
        ChooseItemTypes ChooseItemType
        {
            get;
            set;
        }

        /// <summary>
        /// ��һ��
        /// </summary>
        void NextRow();

        /// <summary>
        /// ��һҳ
        /// </summary>
        void NextPage();

        /// <summary>
        /// ��һ��
        /// </summary>
        void PriorRow();

        /// <summary>
        /// ��һҳ
        /// </summary>
        void PriorPage();

        //{E91E0D33-FCC6-4982-BA74-320A6E8A373C}
        #region
        /// <summary>
        /// �Һ�ʵ��
        /// </summary>
        FS.HISFC.Models.Registration.Register RegPatientInfo
        {
            get;
            set;
        }

        FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade IGetSiItemGrade
        {
            get;
            set;

        }
        #endregion
    }

    /// <summary>
    /// �����ַ�����������Ŀ��ʽ
    /// </summary>
    public enum ChooseItemTypes 
    {
        /// <summary>
        /// ÿ�������ַ�����
        /// </summary>
        ItemChanging = 0,

        /// <summary>
        /// �������ַ��س�����
        /// </summary>
        ItemInputEnd
    }

    #endregion

    #region ���߻�����Ϣ

    /// <summary>
    /// ���߻�����Ϣ�ؼ�
    /// </summary>
    public interface IOutpatientInfomation 
    {
        /// <summary>
        /// �Զ��巽��
        /// </summary>
        void CustomMethod();

        /// <summary>
        /// ������л��۱�����Ϣ.
        /// </summary>
        ArrayList FeeSameDetails { get; set; }
        
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        FS.HISFC.Models.Registration.Register PatientInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ��һ�����߻�����Ϣ
        /// </summary>
        FS.HISFC.Models.Registration.Register PrePatientInfo { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        void Clear();

        /// <summary>
        /// ��ʼ��
        /// </summary>
        int Init();

        /// <summary>
        /// ���Ը��������¼�,һ�����ʱ�򴫵�PatientInfo
        /// </summary>
        event DelegateChangeSomething ChangeFocus;

        /// <summary>
        /// �����˺�ͬ��λ,���ߺ�ͬ��λ�仯��,Ӱ�������Ŀ�ļ۸��Լ����
        /// </summary>
        event DelegateChangeSomething PactChanged;

        /// <summary>
        /// �Ƿ���Ը��Ļ��߻�����Ϣ
        /// </summary>
        bool IsCanModifyPatientInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ҽ��,������������Ƿ�Ҫ��ȫƥ��
        /// </summary>
        bool IsDoctDeptCorrect
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ��շ�ʱ����ԹҺ�ҽ������
        /// </summary>
        bool IsRegWhenFee
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�Ĭ�ϵȼ�����
        /// </summary>
        bool IsClassCodePre
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���Ը��Ļ�����Ϣ
        /// </summary>
        bool IsCanModifyChargeInfo
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�ֱ���շѻ���
        /// </summary>
        bool IsRecordDirectFee
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ����������Ŀ
        /// </summary>
        bool IsCanAddItem
        {
            get;
            set;
        }

        /// <summary>
        /// ���ĵ���Ŀ��Ϣ
        /// </summary>
        ArrayList ModifyFeeDetails
        {
            get;
            set;
        }

        /// <summary>
        /// ���߷�����Ϣ����
        /// </summary>
        ArrayList FeeDetails
        {
            get;
            set;
        }

        /// <summary>
        /// ��ǰѡ�е��շ������е���Ŀ��Ϣ����
        /// </summary>
        ArrayList FeeDetailsSelected
        {
            get;
            set;
        }

        /// <summary>
        /// �ı����շ�����ʱ����
        /// </summary>
        event DelegateChangeSomething RecipeSeqChanged;

        /// <summary>
        ///���޸Ŀ���ҽ��ʱ����
        /// </summary>
        event DelegateChangeDoctAndDept SeeDoctChanged;

        /// <summary>
        /// ���޸Ŀ������ʱ����
        /// </summary>
        event DelegateChangeDoctAndDept SeeDeptChanaged;

        /// <summary>
        /// ���������Լ���ͬ��λ����Ҫͬ���۸�ʱ����!
        /// </summary>
        event DelegateChangeSomething PriceRuleChanaged;

        /// <summary>
        /// ɾ���շ�����ʱ����
        /// </summary>
        event DelegateRecipeDeleted RecipeSeqDeleted;

        /// <summary>
        /// �����뿨����Ч�󴥷�,��ҪΪ�˿�����ʾ�Һ���Ϣ�ؼ���λ�á�
        /// </summary>
        event DelegateEnter InputedCardAndEnter;

        /// <summary>
        /// ��ǰ�շ�����
        /// </summary>
        string RecipeSequence
        {
            get;
            set;
        }

        /// <summary>
        /// ����޸���Ϣ
        /// </summary>
        void DealModifyDetails();

        /// <summary>
        /// �����µ��շ�������Ϣ
        /// </summary>
        void SetNewRecipeInfo();

        /// <summary>
        /// �����´���
        /// </summary>
        void AddNewRecipe();

        /// <summary>
        /// ���»�ùҺ���Ϣ
        /// </summary>
        void GetRegInfo();

        /// <summary>
        /// ���ùҺ���Ϣ
        /// </summary>
        void SetRegInfo();

        /// <summary>
        /// ��֤�Һ���Ϣ�Ƿ�Ϸ�
        /// </summary>
        /// <returns>true�Ϸ� false���Ϸ�</returns>
        bool IsPatientInfoValid();

        #region ·־�� �ۼ��� 2007-8-30
        /// <summary>
        /// ���ŷ�Ʊ�ۼƽ��
        /// </summary>
        decimal AddUpCost
        {
            set;
            get;
        }
        /// <summary>
        /// �Ƿ�ʼ�ۼ�
        /// </summary>
        bool IsBeginAddUpCost
        {
            set;
            get;
        }
        /// <summary>
        /// �Ƿ����ۼƲ���
        /// </summary>
        bool IsAddUp
        {
            set;
            get;
        }
        #endregion
    }

    /// <summary>
    /// ����,������ĳЩѡ��
    /// </summary>
    public delegate void DelegateChangeSomething();

    /// <summary>
    /// ���޸��˴����Ŀ��Һ�ҽ��ʱ����
    /// </summary>
    public delegate void DelegateChangeDoctAndDept(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject changeObj);

    /// <summary>
    /// ɾ���շ�����ʱ����
    /// </summary>
    /// <param name="al"></param>
    /// <returns></returns>
    public delegate int DelegateRecipeDeleted(ArrayList al);

    /// <summary>
    /// ̸��Car����
    /// </summary>
    /// <param name="cardNO"></param>
    /// <param name="orgNO"></param>
    /// <param name="cardLocation"></param>
    /// <param name="cardHeight"></param>
    /// <returns></returns>
    public delegate bool DelegateEnter(string cardNO, string orgNO, System.Drawing.Point cardLocation, int cardHeight);

    #endregion

    #region ��Ŀ¼��

    public interface IOutpatientItemInputAndDisplay
    {
        /// <summary>
        /// �Զ����¼�
        /// </summary>
        event EventHandler CustomEvent;
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        FS.HISFC.Models.Base.ItemKind ItemKind { get; set; }
        
        /// <summary>
        /// ��������
        /// </summary>
        void AddNewRow();

        /// <summary>
        /// �����Է�����
        /// </summary>
        void AddOwnDiagFee();

        /// <summary>
        /// ���ӹҺŷ�
        /// </summary>
        void AddRegFee();

        /// <summary>
        /// ����ָ����
        /// </summary>
        /// <param name="row">ָ����</param>
        void AddRow(int row);

        /// <summary>
        /// ������Ϣ
        /// </summary>
        System.Collections.ArrayList ChargeInfoList { get; set; }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        bool IsValid { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        void Clear();

        /// <summary>
        /// ���ָ����
        /// </summary>
        /// <param name="row">ָ����</param>
        void ClearRow(int row);

        /// <summary>
        /// ˢ�º�ͬ��λ
        /// </summary>
        void RefreshItemForPact();

        /// <summary>
        /// ͨ�ùҺż���
        /// </summary>
        string ComRegLevel { get; set; }

        /// <summary>
        /// Ĭ�ϼ۸�λ
        /// </summary>
        string DefaultPriceUnit { get; set; }

        /// <summary>
        /// ������Ŀɾ��ָ����
        /// </summary>
        /// <param name="feeTemp">ָ����Ŀ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int DeleteRow(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeTemp);

        /// <summary>
        /// ɾ����ý�����
        /// </summary>
        void DeleteRow();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrText { get; set; }

        /// <summary>
        /// Ƶ����ʾ״̬, 0 ���� 1 ����
        /// </summary>
        string FreqDisplayType { get; set; }

        /// <summary>
        /// ҽ�����룬�ú�ͬ��λ�Ķ�����Ϣ�е�ҽ��Ŀ¼�ȼ����ڹ���ʱ��Ŀ�ķ�����Ŀ��ʾ������
        /// �������ҽ����YBPactCode = 2
        /// </summary>
        string YBPactCode { get; set; }
        /// <summary>
        /// �������¼��ķ�����ϸ
        /// </summary>
        /// <returns>�ɹ� :����¼��ķ�����ϸ</returns>
        System.Collections.ArrayList GetFeeItemList();

        /// <summary>
        /// �������¼��ķ�����ϸ Ϊ���۱���
        /// </summary>
        /// <returns>�ɹ� ����¼��ķ�����ϸ Ϊ���۱��� ʧ�� null</returns>
        System.Collections.ArrayList GetFeeItemListForCharge();

        /// <summary>
        /// �������¼��ķ�����ϸ Ϊ���۱���
        /// </summary>
        /// <returns>�ɹ� ����¼��ķ�����ϸ Ϊ���۱��� ʧ�� null</returns>
        System.Collections.ArrayList GetFeeItemListForCharge(bool isGroupDetail);

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Init();

        /// <summary>
        /// �Ƿ����������Ŀ
        /// </summary>
        bool IsCanAddItem { get; set; }

        /// <summary>
        /// �Ƿ���Ը��Ļ�����ϸ
        /// </summary>
        bool IsCanModifyCharge { get; set; }

        /// <summary>
        /// �Ƿ���ع���ҩƷ
        /// </summary>
        bool IsDisplayLackPha { get; set; }
        
        /// <summary>
        /// ÿ�������Ƿ����Ϊ��
        /// </summary>
        bool IsDoseOnceNull { get; set; }

        /// <summary>
        /// �Ƿ����ѡ����Ŀ�շ�
        /// </summary>
         bool IsCanSelectItemAndFee { get; set; }
        /// <summary>
        /// �Ƿ��ý���
        /// </summary>
        bool IsFocus { get; set; }
        
        /// <summary>
        /// �Ƿ���֤���
        /// </summary>
        bool IsJudgeStore { get; set; }

        /// <summary>
        /// �Ƿ���ʾ�Է�ҽ��
        /// </summary>
        bool IsOwnDisplayYB { get; set; }

        /// <summary>
        /// �Ƿ�������ȡ��
        /// </summary>
        bool IsQtyToCeiling { get; set; }

        /// <summary>
        /// ���ĸ���
        /// </summary>
        void ModifyDays();

        /// <summary>
        /// ���ļ۸�
        /// </summary>
        void ModifyPrice();

        /// <summary>
        /// û�йҺŻ��߿��ŵ�һλ
        /// </summary>
        string NoRegFlagChar { get; set; }

        /// <summary>
        /// �Է����ѱ���
        /// </summary>
        string OwnDiagFeeCode { get; set; }

        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        FS.HISFC.Models.Registration.Register PatientInfo { get; set; }

        /// <summary>
        /// �۸񾯽�����ɫ
        /// </summary>
        int PriceWarinningColor { get; set; }

        /// <summary>
        /// �۸񾯽��߽��
        /// </summary>
        decimal PriceWarnning { get; set; }

        /// <summary>
        /// ˢ���±���
        /// </summary>
        void RefreshNewRate();
        
        /// <summary>
        ///������Ŀ�б�ˢ�±��� 
        /// </summary>
        /// <param name="feeDetails">��Ŀ�б�</param>
        void RefreshNewRate(System.Collections.ArrayList feeDetails);

        /// <summary>
        /// ˢ�¿������
        /// </summary>
        /// <param name="recipeSeq"></param>
        /// <param name="obj"></param>
        void RefreshSeeDept(string recipeSeq, FS.FrameWork.Models.NeuObject obj);

        /// <summary>
        /// ˢ�¿���ҽ��
        /// </summary>
        /// <param name="recipeSeq"></param>
        /// <param name="deptCode"></param>
        /// <param name="obj"></param>
        void RefreshSeeDoc(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject obj);

        /// <summary>
        /// �Һŷ���Ŀ����
        /// </summary>
        string RegFeeItemCode { get; set; }

        /// <summary>
        /// ��ʱ�Һſ���
        /// </summary>
        string RegisterDept { get; set; }

        /// <summary>
        /// ���û�ý���
        /// </summary>
        void SetFocus();

        /// <summary>
        /// ���ý����������
        /// </summary>
        void SetFocusToInputCode();

        /// <summary>
        /// ֹͣStopEditing
        /// </summary>
        void StopEdit();

        /// <summary>
        /// ����С��
        /// </summary>
        void SumLittleCost();

        /// <summary>
        /// ��ǰ�շ�����
        /// </summary>
        string RecipeSequence { get; set; }

        /// <summary>
        /// ��Ŀ�����仯�󴥷�
        /// </summary>
        event delegateFeeItemListChanged FeeItemListChanged;

        /// <summary>
        /// �����Ŀ��ʾ��Ϣ
        /// </summary>
        IOutpatientOtherInfomationLeft LeftControl { get; set; }

        IOutpatientOtherInfomationRight RightControl { get;set;}

        /// <summary>
        /// ˢ����Ŀ
        /// </summary>
        void RefreshItem();
        /// <summary>
        /// �շ���Ŀ�б�
        /// </summary>
        DataSet FeeItem { get; set; }

        void PreCountInvos();
     }

    /// <summary>
	/// ��Ŀ�仯����
	/// </summary>
    public delegate void delegateFeeItemListChanged(ArrayList al);
		
    #endregion

    #region �����շ�������ʾ��Ϣ

    public interface IOutpatientOtherInfomationRight 
    {
        /// <summary>
        /// ���
        /// </summary>
        void Clear();

        /// <summary>
        /// ����ҩƷ������Ϣ
        /// </summary>
        FS.FrameWork.Public.ObjectHelper DrugFeeCodeHelper { set; }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        int Init();

        /// <summary>
        /// ���ô�������ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy { set; }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="dsItem">��Ŀ����</param>
        void SetDataSet(System.Data.DataSet dsItem);

        /// <summary>
        /// ����ҩƷ������Ϣ
        /// </summary>
        /// <param name="drugFeeCodeHelper"></param>
        void SetFeeCodeIsDrugArrayListObj(FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper);

        /// <summary>
        /// ���÷�����ʾ��Ϣ
        /// </summary>
        /// <param name="patient">�ҺŻ��߻�����Ϣ</param>
        /// <param name="ft">ǰ̨�����ķ�����Ϣ</param>
        /// <param name="feeItemLists">��ǰ�շ���Ϣ</param>
        /// <param name="diagLists">�����Ϣ</param>
        /// <param name="otherInfomations">������Ϣ</param>
        void SetInfomation(FS.HISFC.Models.Registration.Register patient, FS.HISFC.Models.Base.FT ft, System.Collections.ArrayList feeItemLists, System.Collections.ArrayList diagLists, params string[] otherInfomations);
        
        /// <summary>
        /// ���ô�������ӿ�
        /// </summary>
        /// <param name="medcareInterfaceProxy">��������ӿ�</param>
        void SetMedcareInterfaceProxy(FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy);

        /// <summary>
        /// ���õ�����Ŀ��ʾ��Ϣ
        /// </summary>
        /// <param name="f">������Ŀ</param>
        void SetSingleFeeItemInfomation(FS.HISFC.Models.Fee.Outpatient.FeeItemList f);
        /// <summary>
        /// �����Ƿ�Ԥ����
        /// </summary>

        bool IsPreFee { set;get;}

        /// <summary>
        /// �����Ŀ��ʾ��Ϣ
        /// </summary>
        FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen MultiScreen {  set; }


    }

    /// <summary>
    /// �����շ���ʾ������Ϣ�����
    /// </summary>
    public interface IOutpatientOtherInfomationLeft
    {
        /// <summary>
        /// ���
        /// </summary>
        void Clear();

        /// <summary>
        /// false:���� true:�շ�
        /// </summary>
        bool IsValidFee { get; set;}

        /// <summary>
        /// �Ƿ�Ԥ����
        /// </summary>
        bool IsPreFee { get; set;}
        
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Init();

        /// <summary>
        /// �Ƿ�������ʾ��Ϣ��Ч
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        bool IsValid();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrText { get; set; }

        /// <summary>
        /// ��ʼ����Ʊ
        /// </summary>
        /// <returns></returns>
        int InitInvoice();

        /// <summary>
        /// ��ƱԤ������
        /// </summary>
        string InvoicePreviewType { get; set; }
        
        /// <summary>
        /// ��Ʊ��ʽ
        /// </summary>
        string InvoiceType { get; set; }
        
        /// <summary>
        /// ���·�Ʊ�󴥷�
        /// </summary>
        event DelegateChangeSomething InvoiceUpdated;

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        FS.HISFC.Models.Registration.Register PatientInfo { get; set; }

        /// <summary>
        /// ˢ����ʾ��Ϣ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        int RefreshDisplayInfomation(System.Collections.ArrayList feeItemList);

        /// <summary>
        /// ���ý���
        /// </summary>
        void SetFocus();

        /// <summary>
        /// ���·�Ʊ
        /// </summary>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int UpdateInvoice(string invoiceNO);

        /// <summary>
        /// ��Ʊ��
        /// </summary>
        string InvoiceNO { get; set; }

        /// <summary>
        /// ��÷�Ʊ��
        /// </summary>
        string GetInvoiceNO();

    }

     #endregion

    /*  �����Ƶ�interface��
    #region ���﷢Ʊ��ӡ

    /// <summary>
    /// ���﷢Ʊ��ӡ
    /// </summary>
    public interface IInvoicePrint 
    {
        //{DF484D55-5A9E-4afd-9B82-21EF6DA6E400}
        #region liuqiang 2007-8-23 �޸�
        #region �޸�ԭ��
        //>     ���ڴ�ӡ���������⡣���ող�������峬���ġ����԰���ͬ��λά����
        //> 
        //> ������ҽԺҽ�����������������ַ�Ʊ������ͬ��λȫ��ҽ������ͨ��medicaltype��������ͨҽ�������������ա�
        //> ��������£�ͨ����ͬ��λά���÷�ʽ��û�������ˡ�
        //> 
        //> ������ �� ��Ʊ��ӡ�Ľӿ������������ԡ�һ��patientinfo��һ��invoicetype��
        //> ��Ʊ��ӡʱ����������ȶ�patientinfo���и�ֵ���ӿ�ʵ��ʱ����patientinfo������invoicetype��ֵ��
        //> Ȼ�������ȡ��invoicetype��ֵ���������ֵ�ں����ڽ��д�������������balancelist��
        //> 
        //> �㿴������ɶ����û�����⣬������ô���� 
        #endregion
        /// <summary>
        /// �Һ���Ϣʵ��
        /// </summary>
        FS.HISFC.Models.Registration.Register Register
        {
            set;
        }
        /// <summary>
        /// ��Ʊ�������࣬�硰ZY01����ZY02����ZY03����
        /// </summary>
        string InvoiceType
        {
            get;
        }
        #endregion
        /// <summary>
        /// �ַ�Ʊ��֧����ʽ��� 1 �������� 2 ��������
        /// </summary>
        string SetPayModeType
        {
            set;
        }

        /// <summary>
        /// �ַ�Ʊ���֧����ʽ
        /// </summary>
        string SplitInvoicePayMode
        {
            set;
        }
        /// <summary>
        /// �ؼ������������д��
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// �����Ƿ�ΪԤ��ģʽ
        /// </summary>
        bool IsPreView
        {
            set;
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        System.Data.IDbTransaction Trans
        {
            set;
        }

        /// <summary>
        /// �����Ƿ�ΪԤ��ģʽ
        /// </summary>
        /// <param name="isPreView">trueԤ�� false ��Ԥ��</param>
        void SetPreView(bool isPreView);

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
        int Print();

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="t"></param>
        void SetTrans(System.Data.IDbTransaction trans);

        /// <summary>
        /// ���÷�Ʊ��ӡ����
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="invoice">��Ʊ������Ϣ</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ��Ϣ</param>
        /// <param name="feeDetails">������ϸ��Ϣ</param>
        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
        /// <returns></returns>
        int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList invoiceDetails, ArrayList feeDetails, bool isPreview);

        /// <summary>
        /// ���ô�ӡ��������
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="Invoices">��������Ʊ��Ϣ</param>
        /// <param name="invoiceDetails">���з�Ʊ��ϸ��Ϣ</param>
        /// <param name="feeDetails">���з�����Ϣ</param>
        /// <returns></returns>
        int SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, ArrayList Invoices, ArrayList invoiceDetails, ArrayList feeDetails);

        /// <summary>
        /// ��ӡ��������
        /// </summary>
        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
        int PrintOtherInfomation();
    }
    #endregion

    #region ����ַ�Ʊ

    /// <summary>
    /// ����ַ�Ʊ
    /// </summary>
    public interface ISplitInvoice
    {
        /// <summary>
        /// ����
        /// </summary>
        System.Data.IDbTransaction Trans
        {
            set;
        }
        /// <summary>
        /// ����ַ�Ʊ
        /// </summary>
        /// <param name=" register">���ߵ���Ϣ</param>
        /// <param name="feeItemLists">���ߵ����������ϸ</param>
        /// <returns>�ɹ� �ֺõķ�����ϸ,ÿ��ArrayList����һ��Ӧ�����ɷ�Ʊ�ķ�����ϸ ʧ�� null</returns>
        ArrayList SplitInvoice(FS.HISFC.Models.Registration.Register register, ref ArrayList feeItemLists);
        /// <summary>
        /// ������Ҫʱ����
        /// </summary>
        /// <param name="trans"></param>
        void SetTrans(System.Data.IDbTransaction trans);
    }

    #endregion

    */

    #region �����շѵ���ѡ��

    ///{283BD55B-83E0-4f35-86E8-152CF0C1DA85} ���Ӽ�ͥ�������
    /// <summary>
    /// �շѰ�ť����
    /// </summary>
    /// <param name="alPayModes">֧����ʽ��Ϣ</param>
    /// <param name="invoices">��Ʊ��Ϣ��������Ӧ��Ʊ�������Ϣ��ÿ�������Ӧһ����Ʊ��</param>
    /// <param name="invoiceDetails">��Ʊ��ϸ��Ϣ����Ӧ���ν����ȫ��������ϸ��</param>
    /// <param name="invoiceFeeItemDetails">��Ʊ������ϸ��Ϣ������Ʊ�����ķ�����ϸ��ÿ�������Ӧ�÷�Ʊ�µķ�����ϸ��</param>
    public delegate void DelegateFee(ArrayList balancePays, ArrayList invoices, ArrayList invoiceDetails, ArrayList invoiceFeeItemDetails);

    //��������ʵ�ս��һؽ��{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
    public delegate void DelegateRealCost(string realcost,string returncost);

    /// <summary>
    /// �շѵ����ؼ�
    /// </summary>
    public interface IOutpatientPopupFee 
    {
        //ʵ�ս��{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        event DelegateRealCost RealCostChange;


        /// <summary>
        /// ���۴���
        /// </summary>
        event DelegateChangeSomething ChargeButtonClicked;
        
        /// <summary>
        /// �շѴ���
        /// </summary>
        event DelegateFee FeeButtonClicked;
        
        /// <summary>
        /// �շ���ϸ
        /// </summary>
        System.Collections.ArrayList FeeDetails { get; set; }
        
        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        FS.HISFC.Models.Base.FT FTFeeInfo { get; }
        
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Init();

        /// <summary>
        /// ��Ʊ�ͷ�Ʊ��ϸ����
        /// </summary>
        System.Collections.ArrayList InvoiceAndDetails { get; set; }

        /// <summary>
        /// ��Ʊ��ϸ����
        /// </summary>
        System.Collections.ArrayList InvoiceDetails { get; set; }

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        System.Collections.ArrayList Invoices { get; set; }

        //{E6CD2A14-1DCB-4361-834C-9CF9B9DC669A}���һ�����ԣ����水��Ʊ����ķ�����ϸ liuq
        /// <summary>
        /// ��Ʊ������ϸ����
        /// </summary>
        ArrayList InvoiceFeeDetails
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�����ֽ�֧��
        /// </summary>
        bool IsCashPay { get; set; }

        /// <summary>
        /// �Ƿ���ȡ����ť
        /// </summary>
        bool IsPushCancelButton { get; set; }

        /// <summary>
        /// �Ƿ����˷ѹ���
        /// </summary>
        bool IsQuitFee { set; }

        /// <summary>
        /// �Ƿ����ɹ���δ�˳���
        /// {AFEDD473-052A-4c8a-9EA4-9D002443DF52}
        /// </summary>
        bool IsSuccessFee { get; set; } 

        /// <summary>
        /// �Ƿ�ϵͳ������������
        /// </summary>
        bool IsAutoBankTrans { set; }

        /// <summary>
        /// ������
        /// </summary>
        decimal LeastCost { set; }
        /// <summary>
        /// �����ӿ�
        /// </summary>
        IBankTrans BankTrans { get; set; }
        /// <summary>
        /// ����ҩƷ���
        /// </summary>
        decimal OverDrugCost { set; }

        /// <summary>
        /// �Էѽ��
        /// </summary>
        decimal OwnCost { get; set; }
        /// <summary>
        /// ������
        /// 
        /// ͨ�������㷨�������ܲ����������
        /// {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
        /// </summary>
        decimal RebateRate { get; set; }

        /// <summary>
        /// ��ǰ���ߺ�ͬ��λ
        /// </summary>
        FS.HISFC.Models.Base.PactInfo PactInfo { set; }

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        FS.HISFC.Models.Registration.Register PatientInfo { set; }

        /// <summary>
        /// �Ը����
        /// </summary>
        decimal PayCost { get; set; }

        /// <summary>
        /// ���ѽ��
        /// </summary>
        decimal PubCost { get; set; }

        /// <summary>
        /// ʵ�����
        /// </summary>
        decimal RealCost { set; }

        /// <summary>
        /// ���۱�����Ϣ
        /// </summary>
        /// <returns></returns>
        bool SaveCharge();

        /// <summary>
        /// �����շ���Ϣ
        /// </summary>
        /// <returns></returns>
        bool SaveFee();

        /// <summary>
        /// �Է�ҩƷ���
        /// </summary>
        decimal SelfDrugCost { set; }

        /// <summary>
        /// ���ÿؼ�Ĭ�Ͻ���
        /// </summary>
        void SetControlFocus();

        /// <summary>
        /// �ܽ��
        /// </summary>
        decimal TotCost { get; set; }

        /// <summary>
        /// �Է��ܽ��
        /// </summary>
        decimal TotOwnCost { get; set; }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        FS.FrameWork.Management.Transaction Trans { set; }
    }

    #endregion

    #region �������б�ӿ�
    /// <summary>
    /// �������б�ӿ�
    /// </summary>
    public interface IOutpatientFeeList
    {
        /// <summary>
        /// ���ӻ��߷�Ʊ��Ϣ
        /// </summary>
        /// <param name="arlInvoicesList"></param>
        void AddItems(ArrayList arlInvoicesList);
        /// <summary>
        /// �б�ѡ�����¼�
        /// </summary>
        event DelegatePatientSelected evnPatientSelected;
        /// <summary>
        /// ��ȡ���¼���� iCount �м�¼δ�շѻ����б�
        /// </summary>
        /// <param name="iCount"></param>
        /// <param name="arlRegister"></param>
        void GetSelectRowUpByCount(int iCount, out ArrayList arlRegister);
    }
    /// <summary>
    /// �б�ѡ�����¼�
    /// </summary>
    /// <param name="register"></param>
    public delegate void DelegatePatientSelected(FS.HISFC.Models.Registration.Register register);

    #endregion

    #endregion

    /* �Ƶ�interface��
    #region ҽ��������ť

    /// <summary>
    /// ҽ�����������û��߻�����Ϣ�ӿ�
    /// </summary>
    public interface ISIReadCard 
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        int ReadCard(string pactCode);

        /// <summary>
        /// ���ý���ҽ��������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        int SetSIPatientInfo();
    }

    #endregion
    */

    //#region ö��
    ///// <summary>
    ///// ��������
    ///// </summary>
    //public enum EBlanceType
    //{
    //    /// <summary>
    //    /// ��Ժ����
    //    /// </summary>
    //    Out = 0,
    //    /// <summary>
    //    /// ��;����
    //    /// </summary>
    //    Mid = 1,
    //    /// <summary>
    //    /// Ƿ�ѽ���
    //    /// </summary>
    //    Owe = 2
    //}
    //#endregion

    /* ����  �Ƶ�interface
    #region סԺ���ʴ�ӡ�ӿ�
    /// <summary>
    /// סԺ���ʴ�ӡ�ӿ�
    /// </summary>
    public interface IInpatientChargePrint
    {
        FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get;
            set;
        }

        int SetData(List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemCollection);

        int Print();

        int Preview();
    }
    #endregion


    /// <summary>
    /// ����������Ӧ֢ {6586C3F9-2B89-4597-B9DA-63122A296F22}
    /// </summary>
    public interface IAdptIllnessOutPatient
    {
        
        /// <summary>
        /// �������ﻼ����Ӧ֢
        /// </summary>
        /// <param name="registerObj">���ﻼ��ʵ��</param>
        /// <param name="alOutFeeDetail">�������ʵ������</param>
        /// <returns></returns>
        int SaveOutPatientFeeDetail(FS.HISFC.Models.Registration.Register registerObj ,ref ArrayList alOutFeeDetail);

        /// <summary>
        /// ���ﻼ����Ӧ֢������
        /// </summary>
        /// <param name="registerObj">���ﻼ��ʵ��</param>
        /// <param name="outFeeDetail">�������ʵ��</param>
        /// <returns></returns>
        int ProcessOutPatientFeeDetail(FS.HISFC.Models.Registration.Register registerObj, ref FS.HISFC.Models.Fee.Outpatient.FeeItemList outFeeDetail);

    }

    /// <summary>
    /// ������סԺӦ֢{6586C3F9-2B89-4597-B9DA-63122A296F22}
    /// </summary>
    public interface IAdptIllnessInPatient
    {
        /// <summary>
        /// סԺ������Ӧ֢������
        /// </summary>
        /// <param name="patientObj">סԺ����ʵ��</param>
        /// <param name="alInpatientDetail">סԺ����ʵ������</param>
        /// <returns></returns>
        int SaveInpatientFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientObj, ref ArrayList alInpatientDetail);

        /// <summary>
        /// סԺ������Ӧ֢������
        /// </summary>
        /// <param name="patientObj">סԺ����ʵ��</param>
        /// <param name="alInpatientDetail">סԺ����ʵ��</param>
        /// <returns></returns>
        int ProcessInpatientFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientObj, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList inpatientDetail);

    }

    /// <summary>
    /// ��ȡҽ�����(1��2��3��4�ԷѶ���)
    /// 
    /// {112B7DB5-0462-4432-AD9D-17A7912FFDBE}   ��Ŀҽ�������ʾ�ӿ�
    /// </summary>
    public interface IGetSiItemGrade
    {
        /// <summary>
        /// ������Ŀ�����ȡҽ���ȼ�
        /// </summary>
        /// <param name="hisItemCode">ҽԺ��Ŀ����</param>
        /// <param name="siGrade">ҽԺ��Ŀ����</param>
        /// <returns>ҽ���ȼ�</returns>
        int GetSiItemGrade(string hisItemCode, ref string siGrade);

        /// <summary>
        /// ���ݺ�ͬ
        /// </summary>
        /// <param name="pactID">��ͬ��λ����</param>
        /// <param name="hisItemCode">ҽԺ��Ŀ����</param>
        /// <param name="siGrade">ҽ���ȼ�</param>
        /// <returns></returns>
        int GetSiItemGrade(string pactID, string hisItemCode, ref string siGrade);

    }
    */
}
