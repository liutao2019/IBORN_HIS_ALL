using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizProcess.Interface.Order
{


    #region �ӿڶ���

    /// <summary>
    /// ���ﲡ����ӡ�ӿ�
    /// </summary>
    public interface IMedicalReportPrint
    {


        /// <summary>
        /// ���߹Һ���ˮ��
        /// </summary>
        string RegId
        {
            get;
            set;
        }


        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        bool IsPrint
        {
            get;
            set;
        }
        /// <summary>
        /// ��ӡ�ӿ�
        /// </summary>
        /// <returns></returns>
        void Print();


        /// <summary>
        /// ��ӡ�ӿ�
        /// </summary>
        /// <returns></returns>
        void PrintView();


    }

    #endregion
}
