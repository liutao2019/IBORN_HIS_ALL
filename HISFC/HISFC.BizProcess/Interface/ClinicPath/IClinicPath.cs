using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.ClinicPath
{
   public interface IClinicPath
    {
        ///�����Ƿ����ٴ�·��
        /// <param name="inpatientNo">����סԺ��</param>
        /// <returns>�ɹ�����true ʧ�ܷ���false</returns>
        bool PatientIsSelectedPath(string inpatientNo);

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="inpatientNo">����סԺ��</param>
        ///  <returns>���ٴ�·������1 ���ڷ���-1</returns>
        int ClickClinicPath(string inpatientNo);

        /// <summary>
        /// ��ʿվ��Ժ����
        /// </summary>
        /// <param name="inpatientNO">����סԺ��</param>
        /// <returns>�ɹ�����true ʧ�ܷ���false</returns>
        bool PatientOutByNurse(string inpatientNO);

        /// <summary>
        /// ҽ��վ��Ժ����
        /// </summary>
        /// <param name="inpatientNO">����סԺ��</param>
        /// <returns>�ɹ�����true ʧ�ܷ���false</returns>

        bool PatientOutByDoctor(string inpatientNO);
        /// <summary>
        /// ����ҽ��ʱ���Զ��ж�ҽ��������ҽ�����Ƿ���·���У��粻��·���У��򵯳���ʾ����д����ԭ��
        /// </summary>
        /// <param name="inpatientNo">����סԺ��</param>
        /// <param name="orderList">ҽ���б�</param>
        /// <returns>�ɹ�����true ʧ�ܷ���false</returns>

        bool PatientSaveOrder(string inpatientNo, List<FS.HISFC.Models.Order.Inpatient.Order> orderList);
    }
}