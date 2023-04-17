using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Common
{
    /// <summary>
    /// ��ӡ������뵥
    /// </summary>
    public interface ICheckPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">סԺ����ʵ��</param>
        /// <param name="orderList">ҽ��ʵ���б�</param>
        void ControlValue(FS.HISFC.Models.RADT.Patient patient,List<FS.HISFC.Models.Order.Inpatient.Order> orderList);
        void Show();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">���ﻼ��ʵ��</param>
        /// <param name="orderList">ҽ��ʵ���б�</param>
        void ControlValue(FS.HISFC.Models.Registration.Register patient, List<FS.HISFC.Models.Order.OutPatient.Order> orderList);
        /// <summary>
        /// �������
        /// </summary>
        void Reset();
    }
}
