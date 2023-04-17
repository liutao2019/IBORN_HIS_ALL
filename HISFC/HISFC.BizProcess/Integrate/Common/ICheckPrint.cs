using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Common
{
    /// <summary>
    /// ��ӡ������뵥
    /// </summary>
    public interface ICheckPrint : Neusoft.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">סԺ����ʵ��</param>
        /// <param name="orderList">ҽ��ʵ���б�</param>
        void ControlValue(Neusoft.HISFC.Models.RADT.Patient patient,List<Neusoft.HISFC.Models.Order.Inpatient.Order> orderList);
        void Show();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">���ﻼ��ʵ��</param>
        /// <param name="orderList">ҽ��ʵ���б�</param>
        void ControlValue(Neusoft.HISFC.Models.Registration.Register patient, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList);
        /// <summary>
        /// �������
        /// </summary>
        void Reset();
    }
}
