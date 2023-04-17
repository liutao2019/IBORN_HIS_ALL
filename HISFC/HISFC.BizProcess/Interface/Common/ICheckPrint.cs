using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Common
{
    /// <summary>
    /// 打印检查申请单
    /// </summary>
    public interface ICheckPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">住院患者实体</param>
        /// <param name="orderList">医嘱实体列表</param>
        void ControlValue(FS.HISFC.Models.RADT.Patient patient,List<FS.HISFC.Models.Order.Inpatient.Order> orderList);
        void Show();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">门诊患者实体</param>
        /// <param name="orderList">医嘱实体列表</param>
        void ControlValue(FS.HISFC.Models.Registration.Register patient, List<FS.HISFC.Models.Order.OutPatient.Order> orderList);
        /// <summary>
        /// 清空数据
        /// </summary>
        void Reset();
    }
}
