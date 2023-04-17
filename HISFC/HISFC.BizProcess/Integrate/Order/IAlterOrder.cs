using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ҽ����Ϣ����ӿ�]<br></br>
    /// [�� �� ��: Dorian]<br></br>
    /// [����ʱ��: 2008��01��22]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IAlterOrder
    {
        /// <summary>
        /// סԺҽ����Ϣ���  
        /// �˷����ڴ������ orderList��û��ҽ����ˮ��
        /// 
        /// {76FBAEE1-C996-41b4-9D77-F6CE457F6518}  ����ԭ����Ϊ���ӱ���ǰ���������������
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="recipeDoc">����ҽʦ</param>
        /// <param name="orderList">ҽ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        int AlterOrderOnSaving(Neusoft.HISFC.Models.RADT.PatientInfo patient,Neusoft.FrameWork.Models.NeuObject recipeDoc,Neusoft.FrameWork.Models.NeuObject recipeDept,ref List<Neusoft.HISFC.Models.Order.Inpatient.Order> orderList);

        /// <summary>
        /// סԺҽ����Ϣ��� 
        ///  {76FBAEE1-C996-41b4-9D77-F6CE457F6518}  ����ԭ����Ϊ���ӱ���ǰ���������������
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="recipeDoc">����ҽʦ</param>
        /// <param name="orderList">ҽ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        int AlterOrderOnSaved(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.FrameWork.Models.NeuObject recipeDoc, Neusoft.FrameWork.Models.NeuObject recipeDept, ref List<Neusoft.HISFC.Models.Order.Inpatient.Order> orderList);

        /// <summary>
        /// סԺҽ����Ϣ���
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="recipeDoc">����ҽʦ</param>
        /// <param name="order">ҽ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        int AlterOrder(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.FrameWork.Models.NeuObject recipeDoc,Neusoft.FrameWork.Models.NeuObject recipeDept, ref Neusoft.HISFC.Models.Order.Inpatient.Order order);

        /// <summary>
        /// סԺҽ����Ϣ���{48E6BB8C-9EF0-48a4-9586-05279B12624D}
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="recipeDoc">����ҽʦ</param>
        /// <param name="orderList">ҽ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        int AlterOrder(Neusoft.HISFC.Models.Registration.Register patient, Neusoft.FrameWork.Models.NeuObject recipeDoc,Neusoft.FrameWork.Models.NeuObject recipeDept, ref List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList);

        /// <summary>
        /// סԺҽ����Ϣ���{48E6BB8C-9EF0-48a4-9586-05279B12624D}
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="recipeDoc">����ҽʦ</param>
        /// <param name="order">ҽ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        int AlterOrder(Neusoft.HISFC.Models.Registration.Register patient, Neusoft.FrameWork.Models.NeuObject recipeDoc,Neusoft.FrameWork.Models.NeuObject recipeDept, ref Neusoft.HISFC.Models.Order.OutPatient.Order order);
    }
}
