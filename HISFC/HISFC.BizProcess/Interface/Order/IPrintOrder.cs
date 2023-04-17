using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface
{
    /// <summary>
    /// 打印医嘱单接口
    /// </summary>
    public interface IPrintOrder
    {
        /// <summary>
        /// 设置患者
        /// </summary>
        /// <param name="patientInfo"></param>
        void SetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo);
        /// <summary>
        /// 直接打印
        /// </summary>
        void Print();
        /// <summary>
        /// 打印设置
        /// </summary>
        void ShowPrintSet();
    }
}
