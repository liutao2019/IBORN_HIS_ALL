using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Common
{
    /// <summary>
    /// [��������: PACSҽ���ӿ�]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: δ֪]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����'
    ///		�޸�ʱ��='2009-02-12'
    ///		�޸�Ŀ��=''
    ///		�޸�����='ԭ������ӿ�ʲô��û�У����ڳ���һ��'
    ///  />
    /// </summary>
    public interface IPacs
    {
        /// <summary>
        /// ��ҽ��
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        int PlaceOrder(Neusoft.HISFC.Models.Order.Order Order);

        /// <summary>
        /// �����ҽ��
        /// </summary>
        /// <param name="OrderList"></param>
        /// <returns></returns>
        int PlaceOrder(List<Neusoft.HISFC.Models.Order.Order> OrderList);

        /// <summary>
        /// ���ҽ����Ŀ�Ƿ���Կ���
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        bool CheckOrder(Neusoft.HISFC.Models.Order.Order Order);

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        int SetPatient(Neusoft.HISFC.Models.RADT.Patient Patient);

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
        /// <param name="id">���뵥��</param>
        /// <returns></returns>
        int ShowResult(string id);

        /// <summary>
        /// ��ѯ������
        /// </summary>
        /// <param name="PatientNo"></param>
        /// <returns></returns>
        string[] QueryResult(string PatientNo);

        /// <summary>
        /// ��ʾ���
        /// </summary>
        /// <param name="PatientNo"></param>
        /// <returns></returns>
        int ShowResultByPatient(string PatientNo);


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
            set;
            get;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrMsg
        {
            set;
            get;
        }

        /// <summary>
        /// ����ģʽ(1:����2:סԺ)
        /// </summary>
        string OprationMode
        {
            set;
            get;
        }

        /// <summary>
        /// pacs����쿴����1��ͼ��2������
        /// </summary>
        string PacsViewType
        {
            set;
            get;
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        void ShowForm();
    }
}