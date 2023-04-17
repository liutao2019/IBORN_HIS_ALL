using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizProcess.Interface.FeeInterface
{
    // ժҪ:
    //     IConnect ��ժҪ˵����
    public interface IBankTrans
    {
        /// <summary>
        /// 0:�������ͣ�1�����׽��
        /// </summary>
        List<object> InputListInfo { get; set; }

        bool Do();
        /// <summary>
        ///  0:���� 1���˺� 2��pos�� 3�����
        /// </summary>
        List<object> OutputListInfo { get; set; }

    }
    /// <summary>
    /// IConnect ��ժҪ˵����
    /// </summary>
    public interface IMultiScreen
    {
        /// <summary>
        /// 
        /// </summary>
        List<Object> ListInfo { set;get;}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int ShowScreen();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int CloseScreen();
    }

    /// <summary>
    /// �����շ�������ӿ�
    /// </summary>
    public interface IOutScreen
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int ShowInfo(FS.HISFC.Models.Registration.Register register);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int ClearInfo();
    }

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

    #region ҽ���ӿ�
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
        /// �ж�ָ�����ﲡ���Ƿ����ܴ�ҽ��
        /// {199EF4E9-EF21-4067-97A7-9AA97AF74CDE}
        /// </summary>
        /// <param name="r"></param>
        /// <returns>
        /// 0���������ܾ�������ҽ���������ҵ����ޱ�����¼
        /// -1�����������ܾ�������ҽ������
        /// -2��ʧ��
        /// ����ֵ���������ܾ�������ҽ���������ҵ����б�����¼������ֵΪ���ձ���������
        /// </returns>
        int QueryCanMedicare(FS.HISFC.Models.Registration.Register r);
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
        #region {AD6E49F9-7409-48b1-A297-73610F0072C7}
        /// <summary>
        /// ��ѯ�ϴ�������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeDetails">������ϸʵ�弯��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);
        #endregion

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
       
        /// <summary>
        /// ���ҽ���Һ���Ϣ
        /// </summary>
        /// <param name="r">����Һ�ʵ��</param>
        /// <param name="isReadCard">�Ƿ����</param>
        /// <returns></returns>
        //int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r,bool isReadMCard);

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
        #region {AD6E49F9-7409-48b1-A297-73610F0072C7}
        /// <summary>
        /// ��ѯ�ϴ�������ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">������ϸʵ�弯��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails);

        #endregion
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

        #region///���ӷ��� by chenxin 2013-1-7
        /// <summary>
        ///// ȡ��Ԥ����
        ///// </summary>
        ///// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        ///// <param name="error">������Ϣ</param>
        ///// <returns>�ɹ�0��ʧ��-1</returns>
        //int CancelSIBeforeBalance(FS.HISFC.Models.RADT.PatientInfo patient, ref string error);

        //int CancelSIBalance(FS.HISFC.Models.RADT.PatientInfo patient, ref string error);

        string GetCodeScanningVerification(FS.HISFC.Models.Registration.Register r, string codeNum);
        #endregion
        #endregion

    }
    /// <summary>
    /// ҽ���ӿ� ��չ��֧��HIS�ڲ�ҽ�����㣬����̳ɴ˽ӿ�
    /// {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
    /// </summary>
    public interface IMedcareExtend
    {
        /// <summary>
        /// ��ȡ�����ý��㷽ʽ 
        /// </summary>
        bool IsLocalProcess
        {
            get;
            set;
        }
        /// <summary>
        /// HIS�ڲ�ҽ������
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <param name="arlOther">������Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        int LocalBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails, ArrayList arlOther);
    }

    /// <summary>
    /// ҽ���ӿ� ��չ�������ӿڣ�֧��HIS�ڲ��Ĳ�������
    /// {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
    /// </summary>
    public interface IMedcareBuDan
    {     
        /// <summary>
        /// HIS�ڲ�ҽ����������
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
         int BdBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails);
         /// <summary>
         ///Ԥ������¼�籣�Ľ�����Ϣ
         /// </summary>
         /// <param name="r"></param>
         /// <param name="feeDetails"></param>
         /// <returns></returns>
         int BalanceOutpatientAfterPreBalance(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails);
    } 


    #endregion
    
    
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
    /// �����շ��ٴ��ж���Ч�Խӿ�;{DA12F709-B696-4eb9-AD3B-6C9DB7D780CF}
    /// </summary>
    public interface IFeeExtendOutpatient 
    {
        /// <summary>
        /// ��������
        /// </summary>
        System.Data.IDbTransaction Trans
        {
            set;
        }
        
        /// <summary>
        /// ��֤�����շ�����ҵ�������,�����ύǰ,�����жϵķ���
        /// </summary>
        /// <param name="r">���߹Һ�ʵ��</param>
        /// <param name="Invoices">��Ʊʵ�弯��</param>
        /// <param name="feeItemLists">������ϸʵ����</param>
        /// <param name="otherObjects">��������,���ݵ�ǰ��Ŀʵ�ʴ����ж�,Ĭ��: object[0] ֧����ʽʵ�弯�� object[1] ��Ʊ��ϸ����</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        bool IsValid(FS.HISFC.Models.Registration.Register r, ArrayList Invoices, ArrayList feeItemLists, params object[] otherObjects);

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string Err
        {
            get;
        }
    }//{DA12F709-B696-4eb9-AD3B-6C9DB7D780CF}����
    
    
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

    /// <summary>
    /// סԺ�˷����뵥��ӡ�ӿ�
    /// </summary>
    public interface IBackFeeRecipePrint
    {
        FS.HISFC.Models.Registration.Register Patient
        {
            set;
        }

        int SetData(ArrayList alBackFee);

        void Print();
    }
    public interface IBackFeeApplyPrint
    {
        FS.HISFC.Models.RADT.PatientInfo Patient
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
        /// ����Ѻ��Ʊ��ӡ����
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <param name="alPrepay">Ԥ�����ӡʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int SetValue(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alPrepay);

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
        FS.HISFC.Models.Base.EBlanceType IsMidwayBalance
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

    #region �����շ�

    #region ������Ŀѡ��

    ///// <summary>
    ///// ��ѡ����Ŀ�󴥷�
    ///// ���Ӽ۸����������շ�{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
    ///// </summary>
    ///// <param name="itemCode"></param>
    ///// <param name="drugFlag"></param>
    ///// <param name="exeDeptCode"></param>
    //public delegate void WhenGetItem(string itemCode, string drugFlag, string exeDeptCode, decimal price);

    ///// <summary>
    ///// ����ѡ����Ŀ�ؼ��ӿ�
    ///// </summary>
    //public interface IChooseItemForOutpatient 
    //{
    //    /// <summary>
    //    /// ѡ��ǰ��Ŀ
    //    /// </summary>
    //    /// <returns></returns>
    //    int GetSelectedItem();
        
    //    /// <summary>
    //    /// ����������Ŀ��DdataSet
    //    /// </summary>
    //    /// <param name="dsItem">����Ҫ���˵���Ŀ����</param>
    //    void SetDataSet(System.Data.DataSet dsItem);

    //    /// <summary>
    //    /// ����ǰ̨����Ĺ����ַ�
    //    /// </summary>
    //    /// <param name="sender">����Ķ���</param>
    //    /// <param name="inputChar">ǰ̨����Ĺ����ַ�</param>
    //    /// <param name="inputType">����</param>
    //    void SetInputChar(object sender, string inputChar, FS.HISFC.Models.Base.InputTypes inputType);

    //    /// <summary>
    //    /// ���ѡ�е���Ŀ
    //    /// </summary>
    //    /// <param name="item">ѡ�е���Ŀ</param>
    //    /// <returns>�ɹ�1 ʧ�� -1</returns>
    //    int GetSelectedItem(ref FS.HISFC.Models.Base.Item item);

    //    /// <summary>
    //    /// ��������
    //    /// </summary>
    //    /// <param name="p">�ؼ�����</param>
    //    void SetLocation(System.Drawing.Point p);

    //    /// <summary>
    //    /// ����ʽ
    //    /// </summary>
    //    string InputPrev { get; set; }

    //    /// <summary>
    //    /// ���˷�ʽ
    //    /// </summary>
    //    string QueryType { get; set; }

    //    /// <summary>
    //    /// ��ѡ����Ŀ�󴥷�
    //    /// </summary>
    //    event WhenGetItem SelectedItem;

    //    /// <summary>
    //    /// �Ƿ�ģ����ѯ
    //    /// </summary>
    //    bool IsQueryLike { get; set; }

    //    /// <summary>
    //    /// ��ǰ���Ҵ���
    //    /// </summary>
    //    string DeptCode { get; set; }
        
    //    /// <summary>
    //    /// ��Ŀ���
    //    /// </summary>
    //    FS.HISFC.Models.Base.ItemKind ItemKind{ get; set; }

    //    /// <summary>
    //    /// Ĭ��ÿҳ��ʾ9����¼���Ժ������������
    //    /// </summary>
    //    int ItemCount { get; set; }

    //    /// <summary>
    //    /// ��ѡ��һ����Ŀ��ʱ���Ƿ�رմ���
    //    /// </summary>
    //    bool IsSelectAndClose { get; set; }

    //    /// <summary>
    //    /// ��ǰѡ������Ŀʵ��
    //    /// </summary>
    //    FS.HISFC.Models.Base.Item NowItem { get; set; }

    //    /// <summary>
    //    /// ������˺�ĵ���ĿfpSheet
    //    /// </summary>
    //    object ObjectFilterObject { set; get; }

    //    /// <summary>
    //    /// �Ƿ�ѡ����Ŀ
    //    /// </summary>
    //    bool IsSelectItem { get; set; }

    //    /// <summary>
    //    /// ��ʼ������
    //    /// </summary>
    //    /// <returns></returns>
    //    int Init();

    //    /// <summary>
    //    /// �Ƿ�ѡ����Ŀ��ͬʱ�жϿ��
    //    /// </summary>
    //    bool IsJudgeStore
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �����ַ�����������Ŀ��ʽ
    //    /// </summary>
    //    ChooseItemTypes ChooseItemType
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ��һ��
    //    /// </summary>
    //    void NextRow();

    //    /// <summary>
    //    /// ��һҳ
    //    /// </summary>
    //    void NextPage();

    //    /// <summary>
    //    /// ��һ��
    //    /// </summary>
    //    void PriorRow();

    //    /// <summary>
    //    /// ��һҳ
    //    /// </summary>
    //    void PriorPage();

    //    //{E91E0D33-FCC6-4982-BA74-320A6E8A373C}
    //    #region
    //    /// <summary>
    //    /// �Һ�ʵ��
    //    /// </summary>
    //    FS.HISFC.Models.Registration.Register RegPatientInfo
    //    {
    //        get;
    //        set;
    //    }

    //    FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade IGetSiItemGrade
    //    {
    //        get;
    //        set;

    //    }
    //    #endregion
    //}

    ///// <summary>
    ///// �����ַ�����������Ŀ��ʽ
    ///// </summary>
    //public enum ChooseItemTypes 
    //{
    //    /// <summary>
    //    /// ÿ�������ַ�����
    //    /// </summary>
    //    ItemChanging = 0,

    //    /// <summary>
    //    /// �������ַ��س�����
    //    /// </summary>
    //    ItemInputEnd
    //}

    #endregion

    #region ���߻�����Ϣ

    ///// <summary>
    ///// ���߻�����Ϣ�ؼ�
    ///// </summary>
    //public interface IOutpatientInfomation 
    //{
    //    /// <summary>
    //    /// ������л��۱�����Ϣ.
    //    /// </summary>
    //    ArrayList FeeSameDetails { get; set; }
        
    //    /// <summary>
    //    /// ���߻�����Ϣ
    //    /// </summary>
    //    FS.HISFC.Models.Registration.Register PatientInfo
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ��һ�����߻�����Ϣ
    //    /// </summary>
    //    FS.HISFC.Models.Registration.Register PrePatientInfo { get; set; }

    //    /// <summary>
    //    /// ���
    //    /// </summary>
    //    void Clear();

    //    /// <summary>
    //    /// ��ʼ��
    //    /// </summary>
    //    int Init();

    //    /// <summary>
    //    /// ���Ը��������¼�,һ�����ʱ�򴫵�PatientInfo
    //    /// </summary>
    //    event DelegateChangeSomething ChangeFocus;

    //    /// <summary>
    //    /// �����˺�ͬ��λ,���ߺ�ͬ��λ�仯��,Ӱ�������Ŀ�ļ۸��Լ����
    //    /// </summary>
    //    event DelegateChangeSomething PactChanged;

    //    /// <summary>
    //    /// �Ƿ���Ը��Ļ��߻�����Ϣ
    //    /// </summary>
    //    bool IsCanModifyPatientInfo
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ҽ��,������������Ƿ�Ҫ��ȫƥ��
    //    /// </summary>
    //    bool IsDoctDeptCorrect
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �Ƿ��շ�ʱ����ԹҺ�ҽ������
    //    /// </summary>
    //    bool IsRegWhenFee
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �Ƿ�Ĭ�ϵȼ�����
    //    /// </summary>
    //    bool IsClassCodePre
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �Ƿ���Ը��Ļ�����Ϣ
    //    /// </summary>
    //    bool IsCanModifyChargeInfo
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �Ƿ�ֱ���շѻ���
    //    /// </summary>
    //    bool IsRecordDirectFee
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �Ƿ����������Ŀ
    //    /// </summary>
    //    bool IsCanAddItem
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ���ĵ���Ŀ��Ϣ
    //    /// </summary>
    //    ArrayList ModifyFeeDetails
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ���߷�����Ϣ����
    //    /// </summary>
    //    ArrayList FeeDetails
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ��ǰѡ�е��շ������е���Ŀ��Ϣ����
    //    /// </summary>
    //    ArrayList FeeDetailsSelected
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �ı����շ�����ʱ����
    //    /// </summary>
    //    event DelegateChangeSomething RecipeSeqChanged;

    //    /// <summary>
    //    ///���޸Ŀ���ҽ��ʱ����
    //    /// </summary>
    //    event DelegateChangeDoctAndDept SeeDoctChanged;

    //    /// <summary>
    //    /// ���޸Ŀ������ʱ����
    //    /// </summary>
    //    event DelegateChangeDoctAndDept SeeDeptChanaged;

    //    /// <summary>
    //    /// ���������Լ���ͬ��λ����Ҫͬ���۸�ʱ����!
    //    /// </summary>
    //    event DelegateChangeSomething PriceRuleChanaged;

    //    /// <summary>
    //    /// ɾ���շ�����ʱ����
    //    /// </summary>
    //    event DelegateRecipeDeleted RecipeSeqDeleted;

    //    /// <summary>
    //    /// �����뿨����Ч�󴥷�,��ҪΪ�˿�����ʾ�Һ���Ϣ�ؼ���λ�á�
    //    /// </summary>
    //    event DelegateEnter InputedCardAndEnter;

    //    /// <summary>
    //    /// ��ǰ�շ�����
    //    /// </summary>
    //    string RecipeSequence
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ����޸���Ϣ
    //    /// </summary>
    //    void DealModifyDetails();

    //    /// <summary>
    //    /// �����µ��շ�������Ϣ
    //    /// </summary>
    //    void SetNewRecipeInfo();

    //    /// <summary>
    //    /// �����´���
    //    /// </summary>
    //    void AddNewRecipe();

    //    /// <summary>
    //    /// ���»�ùҺ���Ϣ
    //    /// </summary>
    //    void GetRegInfo();

    //    /// <summary>
    //    /// ���ùҺ���Ϣ
    //    /// </summary>
    //    void SetRegInfo();

    //    /// <summary>
    //    /// ��֤�Һ���Ϣ�Ƿ�Ϸ�
    //    /// </summary>
    //    /// <returns>true�Ϸ� false���Ϸ�</returns>
    //    bool IsPatientInfoValid();

    //    #region ·־�� �ۼ��� 2007-8-30
    //    /// <summary>
    //    /// ���ŷ�Ʊ�ۼƽ��
    //    /// </summary>
    //    decimal AddUpCost
    //    {
    //        set;
    //        get;
    //    }
    //    /// <summary>
    //    /// �Ƿ�ʼ�ۼ�
    //    /// </summary>
    //    bool IsBeginAddUpCost
    //    {
    //        set;
    //        get;
    //    }
    //    /// <summary>
    //    /// �Ƿ����ۼƲ���
    //    /// </summary>
    //    bool IsAddUp
    //    {
    //        set;
    //        get;
    //    }
    //    #endregion
    //}

    ///// <summary>
    ///// ����,������ĳЩѡ��
    ///// </summary>
    //public delegate void DelegateChangeSomething();

    ///// <summary>
    ///// ���޸��˴����Ŀ��Һ�ҽ��ʱ����
    ///// </summary>
    //public delegate void DelegateChangeDoctAndDept(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject changeObj);

    ///// <summary>
    ///// ɾ���շ�����ʱ����
    ///// </summary>
    ///// <param name="al"></param>
    ///// <returns></returns>
    //public delegate int DelegateRecipeDeleted(ArrayList al);

    ///// <summary>
    ///// ̸��Car����
    ///// </summary>
    ///// <param name="cardNO"></param>
    ///// <param name="orgNO"></param>
    ///// <param name="cardLocation"></param>
    ///// <param name="cardHeight"></param>
    ///// <returns></returns>
    //public delegate bool DelegateEnter(string cardNO, string orgNO, System.Drawing.Point cardLocation, int cardHeight);

    #endregion

    #region ��Ŀ¼��

    //public interface IOutpatientItemInputAndDisplay
    //{
    //    /// <summary>
    //    /// ��Ŀ���
    //    /// </summary>
    //    FS.HISFC.Models.Base.ItemKind ItemKind { get; set; }
        
    //    /// <summary>
    //    /// ��������
    //    /// </summary>
    //    void AddNewRow();

    //    /// <summary>
    //    /// �����Է�����
    //    /// </summary>
    //    void AddOwnDiagFee();

    //    /// <summary>
    //    /// ���ӹҺŷ�
    //    /// </summary>
    //    void AddRegFee();

    //    /// <summary>
    //    /// ����ָ����
    //    /// </summary>
    //    /// <param name="row">ָ����</param>
    //    void AddRow(int row);

    //    /// <summary>
    //    /// ������Ϣ
    //    /// </summary>
    //    System.Collections.ArrayList ChargeInfoList { get; set; }

    //    /// <summary>
    //    /// �Ƿ���Ч
    //    /// </summary>
    //    bool IsValid { get; set; }

    //    /// <summary>
    //    /// ����
    //    /// </summary>
    //    void Clear();

    //    /// <summary>
    //    /// ���ָ����
    //    /// </summary>
    //    /// <param name="row">ָ����</param>
    //    void ClearRow(int row);

    //    /// <summary>
    //    /// ˢ�º�ͬ��λ
    //    /// </summary>
    //    void RefreshItemForPact();

    //    /// <summary>
    //    /// ͨ�ùҺż���
    //    /// </summary>
    //    string ComRegLevel { get; set; }

    //    /// <summary>
    //    /// Ĭ�ϼ۸�λ
    //    /// </summary>
    //    string DefaultPriceUnit { get; set; }

    //    /// <summary>
    //    /// ������Ŀɾ��ָ����
    //    /// </summary>
    //    /// <param name="feeTemp">ָ����Ŀ</param>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>
    //    int DeleteRow(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeTemp);

    //    /// <summary>
    //    /// ɾ����ý�����
    //    /// </summary>
    //    void DeleteRow();

    //    /// <summary>
    //    /// ������Ϣ
    //    /// </summary>
    //    string ErrText { get; set; }

    //    /// <summary>
    //    /// Ƶ����ʾ״̬, 0 ���� 1 ����
    //    /// </summary>
    //    string FreqDisplayType { get; set; }

    //    /// <summary>
    //    /// �������¼��ķ�����ϸ
    //    /// </summary>
    //    /// <returns>�ɹ� :����¼��ķ�����ϸ</returns>
    //    System.Collections.ArrayList GetFeeItemList();

    //    /// <summary>
    //    /// �������¼��ķ�����ϸ Ϊ���۱���
    //    /// </summary>
    //    /// <returns>�ɹ� ����¼��ķ�����ϸ Ϊ���۱��� ʧ�� null</returns>
    //    System.Collections.ArrayList GetFeeItemListForCharge();

    //    /// <summary>
    //    /// ��ʼ��
    //    /// </summary>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>
    //    int Init();

    //    /// <summary>
    //    /// �Ƿ����������Ŀ
    //    /// </summary>
    //    bool IsCanAddItem { get; set; }

    //    /// <summary>
    //    /// �Ƿ���Ը��Ļ�����ϸ
    //    /// </summary>
    //    bool IsCanModifyCharge { get; set; }

    //    /// <summary>
    //    /// �Ƿ���ع���ҩƷ
    //    /// </summary>
    //    bool IsDisplayLackPha { get; set; }

    //    /// <summary>
    //    /// ÿ�������Ƿ����Ϊ��
    //    /// </summary>
    //    bool IsDoseOnceNull { get; set; }

    //    /// <summary>
    //    /// �Ƿ��ý���
    //    /// </summary>
    //    bool IsFocus { get; set; }
        
    //    /// <summary>
    //    /// �Ƿ���֤���
    //    /// </summary>
    //    bool IsJudgeStore { get; set; }

    //    /// <summary>
    //    /// �Ƿ���ʾ�Է�ҽ��
    //    /// </summary>
    //    bool IsOwnDisplayYB { get; set; }

    //    /// <summary>
    //    /// �Ƿ�������ȡ��
    //    /// </summary>
    //    bool IsQtyToCeiling { get; set; }

    //    /// <summary>
    //    /// ���ĸ���
    //    /// </summary>
    //    void ModifyDays();

    //    /// <summary>
    //    /// ���ļ۸�
    //    /// </summary>
    //    void ModifyPrice();

    //    /// <summary>
    //    /// û�йҺŻ��߿��ŵ�һλ
    //    /// </summary>
    //    string NoRegFlagChar { get; set; }

    //    /// <summary>
    //    /// �Է����ѱ���
    //    /// </summary>
    //    string OwnDiagFeeCode { get; set; }

    //    /// <summary>
    //    /// ���߹Һ���Ϣ
    //    /// </summary>
    //    FS.HISFC.Models.Registration.Register PatientInfo { get; set; }

    //    /// <summary>
    //    /// �۸񾯽�����ɫ
    //    /// </summary>
    //    int PriceWarinningColor { get; set; }

    //    /// <summary>
    //    /// �۸񾯽��߽��
    //    /// </summary>
    //    decimal PriceWarnning { get; set; }

    //    /// <summary>
    //    /// ˢ���±���
    //    /// </summary>
    //    void RefreshNewRate();
        
    //    /// <summary>
    //    ///������Ŀ�б�ˢ�±��� 
    //    /// </summary>
    //    /// <param name="feeDetails">��Ŀ�б�</param>
    //    void RefreshNewRate(System.Collections.ArrayList feeDetails);

    //    /// <summary>
    //    /// ˢ�¿������
    //    /// </summary>
    //    /// <param name="recipeSeq"></param>
    //    /// <param name="obj"></param>
    //    void RefreshSeeDept(string recipeSeq, FS.FrameWork.Models.NeuObject obj);

    //    /// <summary>
    //    /// ˢ�¿���ҽ��
    //    /// </summary>
    //    /// <param name="recipeSeq"></param>
    //    /// <param name="deptCode"></param>
    //    /// <param name="obj"></param>
    //    void RefreshSeeDoc(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject obj);

    //    /// <summary>
    //    /// �Һŷ���Ŀ����
    //    /// </summary>
    //    string RegFeeItemCode { get; set; }

    //    /// <summary>
    //    /// ��ʱ�Һſ���
    //    /// </summary>
    //    string RegisterDept { get; set; }

    //    /// <summary>
    //    /// ���û�ý���
    //    /// </summary>
    //    void SetFocus();

    //    /// <summary>
    //    /// ���ý����������
    //    /// </summary>
    //    void SetFocusToInputCode();

    //    /// <summary>
    //    /// ֹͣStopEditing
    //    /// </summary>
    //    void StopEdit();

    //    /// <summary>
    //    /// ����С��
    //    /// </summary>
    //    void SumLittleCost();

    //    /// <summary>
    //    /// ��ǰ�շ�����
    //    /// </summary>
    //    string RecipeSequence { get; set; }

    //    /// <summary>
    //    /// ��Ŀ�����仯�󴥷�
    //    /// </summary>
    //    event delegateFeeItemListChanged FeeItemListChanged;

    //    /// <summary>
    //    /// �����Ŀ��ʾ��Ϣ
    //    /// </summary>
    //    IOutpatientOtherInfomationLeft LeftControl { get; set; }

    //    IOutpatientOtherInfomationRight RightControl { get;set;}
    // }

    /// <summary>
	/// ��Ŀ�仯����
	/// </summary>
    public delegate void delegateFeeItemListChanged(ArrayList al);
		
    #endregion

    #region �����շ�������ʾ��Ϣ

    //public interface IOutpatientOtherInfomationRight 
    //{
    //    /// <summary>
    //    /// ���
    //    /// </summary>
    //    void Clear();

    //    /// <summary>
    //    /// ����ҩƷ������Ϣ
    //    /// </summary>
    //    FS.FrameWork.Public.ObjectHelper DrugFeeCodeHelper { set; }

    //    /// <summary>
    //    /// ��ʼ��
    //    /// </summary>
    //    /// <returns></returns>
    //    int Init();

    //    /// <summary>
    //    /// ���ô�������ӿ�
    //    /// </summary>
    //    FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy { set; }

    //    /// <summary>
    //    /// ������Ŀ����
    //    /// </summary>
    //    /// <param name="dsItem">��Ŀ����</param>
    //    void SetDataSet(System.Data.DataSet dsItem);

    //    /// <summary>
    //    /// ����ҩƷ������Ϣ
    //    /// </summary>
    //    /// <param name="drugFeeCodeHelper"></param>
    //    void SetFeeCodeIsDrugArrayListObj(FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper);

    //    /// <summary>
    //    /// ���÷�����ʾ��Ϣ
    //    /// </summary>
    //    /// <param name="patient">�ҺŻ��߻�����Ϣ</param>
    //    /// <param name="ft">ǰ̨�����ķ�����Ϣ</param>
    //    /// <param name="feeItemLists">��ǰ�շ���Ϣ</param>
    //    /// <param name="diagLists">�����Ϣ</param>
    //    /// <param name="otherInfomations">������Ϣ</param>
    //    void SetInfomation(FS.HISFC.Models.Registration.Register patient, FS.HISFC.Models.Base.FT ft, System.Collections.ArrayList feeItemLists, System.Collections.ArrayList diagLists, params string[] otherInfomations);
        
    //    /// <summary>
    //    /// ���ô�������ӿ�
    //    /// </summary>
    //    /// <param name="medcareInterfaceProxy">��������ӿ�</param>
    //    void SetMedcareInterfaceProxy(FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy);

    //    /// <summary>
    //    /// ���õ�����Ŀ��ʾ��Ϣ
    //    /// </summary>
    //    /// <param name="f">������Ŀ</param>
    //    void SetSingleFeeItemInfomation(FS.HISFC.Models.Fee.Outpatient.FeeItemList f);
    //    /// <summary>
    //    /// �����Ƿ�Ԥ����
    //    /// </summary>

    //    bool IsPreFee { set;get;}
    //}

    ///// <summary>
    ///// �����շ���ʾ������Ϣ�����
    ///// </summary>
    //public interface IOutpatientOtherInfomationLeft
    //{
    //    /// <summary>
    //    /// ���
    //    /// </summary>
    //    void Clear();

    //    /// <summary>
    //    /// false:���� true:�շ�
    //    /// </summary>
    //    bool IsValidFee { get; set;}

    //    /// <summary>
    //    /// �Ƿ�Ԥ����
    //    /// </summary>
    //    bool IsPreFee { get; set;}
        
    //    /// <summary>
    //    /// ��ʼ��
    //    /// </summary>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>
    //    int Init();

    //    /// <summary>
    //    /// �Ƿ�������ʾ��Ϣ��Ч
    //    /// </summary>
    //    /// <returns>�ɹ� true ʧ�� false</returns>
    //    bool IsValid();

    //    /// <summary>
    //    /// ������Ϣ
    //    /// </summary>
    //    string ErrText { get; set; }

    //    /// <summary>
    //    /// ��ʼ����Ʊ
    //    /// </summary>
    //    /// <returns></returns>
    //    int InitInvoice();

    //    /// <summary>
    //    /// ��ƱԤ������
    //    /// </summary>
    //    string InvoicePreviewType { get; set; }
        
    //    /// <summary>
    //    /// ��Ʊ��ʽ
    //    /// </summary>
    //    string InvoiceType { get; set; }
        
    //    /// <summary>
    //    /// ���·�Ʊ�󴥷�
    //    /// </summary>
    //    event DelegateChangeSomething InvoiceUpdated;

    //    /// <summary>
    //    /// ���߹ҺŻ�����Ϣ
    //    /// </summary>
    //    FS.HISFC.Models.Registration.Register PatientInfo { get; set; }

    //    /// <summary>
    //    /// ˢ����ʾ��Ϣ
    //    /// </summary>
    //    /// <param name="feeItemList"></param>
    //    /// <returns></returns>
    //    int RefreshDisplayInfomation(System.Collections.ArrayList feeItemList);

    //    /// <summary>
    //    /// ���ý���
    //    /// </summary>
    //    void SetFocus();

    //    /// <summary>
    //    /// ���·�Ʊ
    //    /// </summary>
    //    /// <param name="invoiceNO">��Ʊ��</param>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>
    //    int UpdateInvoice(string invoiceNO);

    //    /// <summary>
    //    /// ��Ʊ��
    //    /// </summary>
    //    string InvoiceNO { get; set; }

    //    /// <summary>
    //    /// ��÷�Ʊ��
    //    /// </summary>
    //    string GetInvoiceNO();
    //}

     #endregion

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
        /// <param name="alPayModes">֧����ʽ����</param>
        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
        /// <returns></returns>
        int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList invoiceDetails, ArrayList feeDetails, ArrayList alPayModes, bool isPreview);

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

    #region �����շѵ���ѡ��

    ///// <summary>
    ///// �շѰ�ť����
    ///// </summary>
    ///// <param name="alPayModes">֧����ʽ��Ϣ</param>
    ///// <param name="invoices">��Ʊ��Ϣ��������Ӧ��Ʊ�������Ϣ��ÿ�������Ӧһ����Ʊ��</param>
    ///// <param name="invoiceDetails">��Ʊ��ϸ��Ϣ����Ӧ���ν����ȫ��������ϸ��</param>
    ///// <param name="invoiceFeeItemDetails">��Ʊ������ϸ��Ϣ������Ʊ�����ķ�����ϸ��ÿ�������Ӧ�÷�Ʊ�µķ�����ϸ��</param>
    //public delegate void DelegateFee(ArrayList balancePays, ArrayList invoices, ArrayList invoiceDetails, ArrayList invoiceFeeItemDetails);
  

    /// <summary>
    ///// �շѵ����ؼ�
    ///// </summary>
    //public interface IOutpatientPopupFee 
    //{
    //    /// <summary>
    //    /// ���۴���
    //    /// </summary>
    //    event DelegateChangeSomething ChargeButtonClicked;
        
    //    /// <summary>
    //    /// �շѴ���
    //    /// </summary>
    //    event DelegateFee FeeButtonClicked;
        
    //    /// <summary>
    //    /// �շ���ϸ
    //    /// </summary>
    //    System.Collections.ArrayList FeeDetails { get; set; }
        
    //    /// <summary>
    //    /// ���û�����Ϣ
    //    /// </summary>
    //    FS.HISFC.Models.Base.FT FTFeeInfo { get; }
        
    //    /// <summary>
    //    /// ��ʼ��
    //    /// </summary>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>
    //    int Init();

    //    /// <summary>
    //    /// ��Ʊ�ͷ�Ʊ��ϸ����
    //    /// </summary>
    //    System.Collections.ArrayList InvoiceAndDetails { get; set; }

    //    /// <summary>
    //    /// ��Ʊ��ϸ����
    //    /// </summary>
    //    System.Collections.ArrayList InvoiceDetails { get; set; }

    //    /// <summary>
    //    /// ��Ʊ����
    //    /// </summary>
    //    System.Collections.ArrayList Invoices { get; set; }

    //    //{E6CD2A14-1DCB-4361-834C-9CF9B9DC669A}���һ�����ԣ����水��Ʊ����ķ�����ϸ liuq
    //    /// <summary>
    //    /// ��Ʊ������ϸ����
    //    /// </summary>
    //    ArrayList InvoiceFeeDetails
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// �Ƿ�����ֽ�֧��
    //    /// </summary>
    //    bool IsCashPay { get; set; }

    //    /// <summary>
    //    /// �Ƿ���ȡ����ť
    //    /// </summary>
    //    bool IsPushCancelButton { get; set; }

    //    /// <summary>
    //    /// �Ƿ����˷ѹ���
    //    /// </summary>
    //    bool IsQuitFee { set; }

    //    /// <summary>
    //    /// ������
    //    /// </summary>
    //    decimal LeastCost { set; }

    //    /// <summary>
    //    /// ����ҩƷ���
    //    /// </summary>
    //    decimal OverDrugCost { set; }

    //    /// <summary>
    //    /// �Էѽ��
    //    /// </summary>
    //    decimal OwnCost { get; set; }

    //    /// <summary>
    //    /// ��ǰ���ߺ�ͬ��λ
    //    /// </summary>
    //    FS.HISFC.Models.Base.PactInfo PactInfo { set; }

    //    /// <summary>
    //    /// ���߹ҺŻ�����Ϣ
    //    /// </summary>
    //    FS.HISFC.Models.Registration.Register PatientInfo { set; }

    //    /// <summary>
    //    /// �Ը����
    //    /// </summary>
    //    decimal PayCost { get; set; }

    //    /// <summary>
    //    /// ���ѽ��
    //    /// </summary>
    //    decimal PubCost { get; set; }

    //    /// <summary>
    //    /// ʵ�����
    //    /// </summary>
    //    decimal RealCost { set; }

    //    /// <summary>
    //    /// ���۱�����Ϣ
    //    /// </summary>
    //    /// <returns></returns>
    //    bool SaveCharge();

    //    /// <summary>
    //    /// �����շ���Ϣ
    //    /// </summary>
    //    /// <returns></returns>
    //    bool SaveFee();

    //    /// <summary>
    //    /// �Է�ҩƷ���
    //    /// </summary>
    //    decimal SelfDrugCost { set; }

    //    /// <summary>
    //    /// ���ÿؼ�Ĭ�Ͻ���
    //    /// </summary>
    //    void SetControlFocus();

    //    /// <summary>
    //    /// �ܽ��
    //    /// </summary>
    //    decimal TotCost { get; set; }

    //    /// <summary>
    //    /// �Է��ܽ��
    //    /// </summary>
    //    decimal TotOwnCost { get; set; }

    //    /// <summary>
    //    /// ���ݿ�����
    //    /// </summary>
    //    FS.FrameWork.Management.Transaction Trans { set; }
    //}

    #endregion

    #endregion

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

    public interface IFeeOweMessage
    {
        /// <summary>
        /// Ƿ����ʾ
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="ft">������Ϣ</param>
        /// <param name="feeItemLists">������ϸ</param>
        /// <param name="type">Ƿ����ʾ����</param>
        /// <param name="err">��ʾ��Ϣ</param>
        /// <returns>true:�ɹ� false:�����ڲ�����</returns>
        //{2518013C-40B2-4693-B494-3DE193C002FF} //���Ӵ�����ϸ
        bool FeeOweMessage(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Base.FT ft, System.Collections.ArrayList feeItemLists, ref FS.HISFC.Models.Base.MessType type, ref string err);
    }

    /// <summary>
    /// �������ӡƾ֤{0374EA05-782E-4609-9CDC-03236AB97906}
    /// </summary>
    public interface IPrintSurety
    {
        void SetValue(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        void Print();

        void PrintView();
    }

    /// <summary>
    /// �ִ���
    /// </summary>
    public interface ISplitRecipe
    {
        /// <summary>
        /// �ִ�����
        /// </summary>
        /// <param name="feeItemList">���ü���</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>true�ɹ� falseʧ��</returns>
        bool SplitRecipe(Register r,ArrayList feeItemList, ref string errText);
    }

    ////{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
    /// <summary>
    /// ҽ��վֱ���շ�
    /// </summary>
    public interface IDoctIdirectFee
    {
        /// <summary>
        /// ҽ��վֱ���շѺ���
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeItemLists">������ϸ</param>
        /// <param name="FeeTime">�շ�ʱ��</param>
        /// <param name="errText">������Ϣ</param>
        ///<returns>1�ɹ� 0Ϊ��ͨ���� -1ʧ��</returns>
        int DoctIdirectFee(FS.HISFC.Models.Registration.Register r, ArrayList feeItemLists, DateTime FeeTime, ref string errText);
        
        /// <summary>
        /// ֱ���շѺ����ҽ���շ���Ϣ
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeItemLists">������Ϣ</param>
        /// <param name="alOrder">ҽ����Ϣ</param>
        /// <param name="feeTime">�շ�ʱ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        int UpdateOrderFee(FS.HISFC.Models.Registration.Register r, ArrayList alOrder, DateTime feeTime, ref string errText);

        /// <summary>
        /// ����ҽ����Ϣ
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="order">ҽ��ʵ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        int CancelOrder(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Order.OutPatient.Order order,ref string errText);
    }

    /// <summary>
    /// ��ͬ��λ�Ż���Ŀά����֤�ӿ�
    /// </summary>
    public interface IValidPactItemChoose
    {
        /// <summary>
        /// ��ͬ��λ�Ż���Ŀά����֤
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="baseItem">��Ŀ</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        bool ValidPactItemChoose(string pactCode, FS.HISFC.Models.Base.Item baseItem, ref string errText);
    }

    /// <summary>
    /// �����շ��ܷ���ȡ���ӿ�
    /// </summary>
    public interface IOutPatientFeeRoundOff
    {
        /// <summary>
        /// ��������ϸ��ʽȡ��
        /// {DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
        /// </summary>
        /// <param name="r"></param>
        /// <param name="totCost"></param>
        /// <param name="feeItemList"></param>
        /// <param name="recipeSequence"></param>
        void OutPatientFeeRoundOff(FS.HISFC.Models.Registration.Register r, ref decimal totCost, ref FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList, string recipeSequence);
    }

    /// <summary>
    /// סԺ�շ��ܷ���ȡ���ӿ�
    /// </summary>
    public interface IInPatientFeeRoundOff
    {
        /// <summary>
        /// ��������ϸ��ʽȡ��
        /// {DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
        /// </summary>
        /// <param name="r"></param>
        /// <param name="totCost"></param>
        /// <param name="feeItemList"></param>
        /// <param name="recipeSequence"></param>
        void InPatientFeeRoundOff(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref decimal totCost, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList);
    }

    /// <summary>
    /// Lis�����Թܽӿ�
    /// </summary>
    public interface ILisCalculateTube
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ��������Թܽӿ�
        /// </summary>
        /// <param name="r">������Ϣ</param>
        /// <param name="alFeeItemList">�շ���Ϣ����</param>
        /// <param name="recipeSequence">�շ�����</param>
        /// <param name="owncost">�Թܽ��</param>
        /// <param name="alTubeList">��ȡ�Թܼ���</param>
        int LisCalculateTubeForOutPatient(FS.HISFC.Models.Registration.Register r, ArrayList alFeeItemList, string recipeSequence, ref decimal owncost, ref ArrayList alTubeList);

        /// <summary>
        /// סԺ�����Թܽӿ�
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        int LisCalculateTubeForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo);
    }
}
