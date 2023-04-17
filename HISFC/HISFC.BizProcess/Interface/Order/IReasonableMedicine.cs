using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Registration;
using System.Drawing;
using System.Collections;

namespace FS.HISFC.BizProcess.Interface.Order
{
    /// <summary>
    /// ������ҩ�ӿڶ���
    /// </summary>
    public interface IReasonableMedicine
    {
        /* ������������
         * 1������ҩƷ����ʾҪ����ʾ
         * 2�����ҩƷ�����л���˫���У��ٴ���ʾҪ����ʾ
         * 3��ÿ����һ��ҩƷ���ύ������ҩ�������
         * 4������ʱ����ͳһ���
         * 5���Ҽ����Բ鿴������ҩ�Ĺ�����Ϣ�˵�
         * 
         * ��������
         * 1����ͯ���и�����������ҩ��ʾ
         * 2������ʷ��ʾ
         * 
         * */

        /// <summary>
        /// ����վ���
        /// </summary>
        FS.HISFC.Models.Base.ServiceTypes StationType
        {
            get;
            set;
        }

        /// <summary>
        /// ������ҩ�����Ƿ����
        /// </summary>
        bool PassEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string Err
        {
            get;
            set;
        }

        /// <summary>
        /// ������ҩϵͳ��ʼ��
        /// </summary>
        /// <param name="logEmpl">��½��Ա</param>
        /// <param name="logDept">��½����</param>
        /// <param name="workStationType">����վ����</param>
        /// <returns>0 ��ʼ��ʧ�� 1 ��ʼ���ɹ�</returns>
        int PassInit(FS.FrameWork.Models.NeuObject logEmpl, FS.FrameWork.Models.NeuObject logDept, string workStationType);

        /// <summary>
        /// ���ô��뻼�߻�����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int PassSetPatientInfo(FS.HISFC.Models.RADT.Patient patient, FS.FrameWork.Models.NeuObject recipeDoct);

        /// <summary>
        /// ��ʾ����ҩƷҪ����ʾ
        /// </summary>
        /// <param name="order">������ҽ������Ϣ</param>
        /// <param name="LeftTop">���Ͻ�����</param>
        /// <param name="RightButton">���½�����</param>
        /// <returns></returns>
        int PassShowSingleDrugInfo(FS.HISFC.Models.Order.Order order, System.Drawing.Point LeftTop, Point RightButton, bool isFirst);

        /// <summary>
        /// �����Ƿ���ʾҪ����ʾ��
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        int PassShowFloatWindow(bool isShow);

        /// <summary>
        /// ҩƷͳһ���
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <param name="alOrder">������ҽ�����б�</param>
        /// <param name="isSave">�Ƿ񱣴�</param>
        /// <returns></returns>
        int PassDrugCheck(ArrayList alOrder, bool isSave);

        /// <summary>
        /// ������ҩ���ܳ�ʼ��ˢ��
        /// </summary>
        /// <returns></returns>
        int PassRefresh();

        /// <summary>
        /// ������ҩ���ܹر�
        /// </summary>
        /// <returns></returns>
        int PassClose();

        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <param name="order">ҽ��</param>
        /// <param name="queryType">��ѯ�������</param>
        /// <param name="alShowMemu">��ѯ�˵��б�</param>
        /// <returns></returns>
        int PassShowOtherInfo(FS.HISFC.Models.Order.Order order, FS.FrameWork.Models.NeuObject queryType, ref ArrayList alShowMemu);

        /// <summary>
        /// ��ȡҩƷ������Ϣ
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int PassShowWarnDrug(FS.HISFC.Models.Order.Order order);

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="diagnoseList"></param>
        /// <returns></returns>
        int PassSetDiagnoses(ArrayList diagnoseList);

        #region �ɵ�����

        ///// <summary>
        ///// PASS���ܵ���
        ///// </summary>
        ///// <param name="commandType"></param>
        ///// <returns></returns>
        //int DoCommand(int commandType);

        ///// <summary>
        ///// PASSϵͳ�����Ƿ���Ч�Լ���
        ///// </summary>
        ///// <param name="queryItemNo"></param>
        ///// <returns></returns>
        //int PassGetStateIn(string queryItemNo);


        //int PassGetWarnFlag(string orderId);
        //int PassGetWarnInfo(string orderId, string flag);
        //int PassInit(string userID, string userName, string deptID, string deptName, int stationType, bool isJudgeLocalSetting);
        //void PassQueryDrug(string drugCode, string drugName, string doseUnit, string useName, int left, int top, int right, int bottom);
        //int PassQuitIn();
        //int PassSaveCheck(PatientInfo patient, List<FS.HISFC.Models.Order.Inpatient.Order> listOrder, int checkType);
        //int PassSaveCheck(Register patient, List<FS.HISFC.Models.Order.OutPatient.Order> listOrder, int checkType);
        //int PassSetDrug(string drugCode, string drugName, string doseUnit, string routeName);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="docID"></param>
        ///// <param name="docName"></param>
        //void PassSetPatientInfo(PatientInfo patient, string docID, string docName);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="docID"></param>
        ///// <param name="docName"></param>
        //void PassSetPatientInfo(Register patient, string docID, string docName);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="order"></param>
        //void PassSetRecipeInfo(FS.HISFC.Models.Order.Inpatient.Order order);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="order"></param>
        //void PassSetRecipeInfo(FS.HISFC.Models.Order.OutPatient.Order order);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="isShow"></param>
        //void ShowFloatWin(bool isShow);

        ///// <summary>
        ///// ������Ϣ
        ///// </summary>
        //string Err
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// ������ҩ�����Ƿ����
        ///// </summary>
        //bool PassEnabled
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 0 ��ʼ��PASSʧ�� 1��ʼ��PASS����ͨ��
        ///// </summary>
        //int StationType
        //{
        //    get;
        //    set;
        //}

        #endregion
    }
}
