using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder
{
    /// <summary>
    /// 住院医生站精麻处方打印接口
    /// </summary>
    public interface IInpatientBill
    {
        FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set;
        }

        /// <summary>
        /// 打印
        /// </summary>
        void PrintRecipe();

        /// <summary>
        /// 验证是否有需要打印的麻精处方
        /// </summary>
        bool Valid();
    }
}
