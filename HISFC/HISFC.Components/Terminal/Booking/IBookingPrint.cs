using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Terminal.Booking
{
    /// <summary>
    /// ҽ��ԤԼ����ӡ�ӿ�
    /// </summary>
    public interface IBookingPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// ҽ��ԤԼʵ��
        /// </summary>
        /// <param name="obj"></param>
        void SetValue(FS.HISFC.Models.Terminal.MedTechBookApply obj);
        /// <summary>
        /// ��ս���
        /// </summary>
        void Reset();
    }
}
