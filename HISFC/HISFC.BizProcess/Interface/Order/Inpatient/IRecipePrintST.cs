using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizProcess.Interface.Order.Inpatient
{
    #region �鷽��ʵ��

    #endregion

    #region �ӿڶ���

    /// <summary>
    /// ���ﴦ����ӡ�ӿ�
    /// </summary>
    public interface IRecipePrintST
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
        /// ������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// ��ӡ�ӿ�
        /// </summary>
        /// <param name="regObj">�Һ�ʵ��</param>
        /// <param name="reciptDept">������������</param>
        /// <param name="reciptDoct">��������ҽ��</param>
        /// <param name="orderList">����List</param>
        /// <param name="orderSelectList">ѡ�е�List</param>
        /// <param name="IsReprint">�Ƿ񲹴�</param>
        /// <param name="IsPreview">�Ƿ�Ԥ��</param>
        /// <param name="printType">��ӡ����</param>
        /// <param name="obj">Ԥ���ֶ�</param>
        /// <returns></returns>
        int OnInPatientPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, NeuObject reciptDept, NeuObject reciptDoct, ArrayList orderPrintList, ArrayList orderSelectList, bool IsReprint, bool IsPreview, string printType, object obj);

    }

    #endregion
}
