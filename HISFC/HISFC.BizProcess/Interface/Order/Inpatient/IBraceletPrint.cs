using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizProcess.Interface.Order.Inpatient
{


    #region �ӿڶ���

    /// <summary>
    /// �����ӡ�ӿ�
    /// </summary>
    public interface IBraceletPrint
    {


        /// <summary>
        /// ������Ϣ
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo myPatientInfo
        {
            get;
            set;
        }
        /// <summary>
        /// ��ӡ�ӿ�
        /// </summary>
        /// <returns></returns>
        void Print();

    }

    #endregion
}
