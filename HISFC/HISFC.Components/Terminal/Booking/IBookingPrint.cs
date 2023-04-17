using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Terminal.Booking
{
    /// <summary>
    /// 医技预约单打印接口
    /// </summary>
    public interface IBookingPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// 医技预约实体
        /// </summary>
        /// <param name="obj"></param>
        void SetValue(FS.HISFC.Models.Terminal.MedTechBookApply obj);
        /// <summary>
        /// 清空界面
        /// </summary>
        void Reset();
    }
}
