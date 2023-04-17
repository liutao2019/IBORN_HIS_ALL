using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Common
{
    /// <summary>
    /// [��������: LISҽ���ӿ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2007-05-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface ILis
    {

        /// <summary>
        /// ��ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        int PlaceOrder(Neusoft.HISFC.Models.Order.Order order);

        /// <summary>
        /// �����ҽ��
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        int PlaceOrder(ICollection<Neusoft.HISFC.Models.Order.Order> orders);

        /// <summary>
        /// ���ҽ����Ŀ�Ƿ���Կ���
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool CheckOrder(Neusoft.HISFC.Models.Order.Order order);

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        int SetPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Connect();

        /// <summary>
        /// ���ݿ�ر�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Disconnect();

        /// <summary>
        /// �ύ
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// �ع�
        /// </summary>
        /// <returns></returns>
        int Rollback();

        /// <summary>
        /// ��ҽ����ʾ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int ShowResult(string id);

        /// <summary>
        /// ��ѯ������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        string[] QueryResult(string inpatientNo);

        /// <summary>
        /// ��ʾ���
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        int ShowResultByPatient(string inpatientNo);


        /// <summary>
        /// �������Ƿ��Ѿ�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsReportValid(string id);


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

    }

}
