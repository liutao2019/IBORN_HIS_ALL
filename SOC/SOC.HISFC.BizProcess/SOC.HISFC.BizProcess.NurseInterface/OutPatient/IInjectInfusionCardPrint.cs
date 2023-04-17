using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient
{
    /// <summary>
    /// 门诊输液卡打印
    /// </summary>
    public interface IInjectInfusionCardPrint
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        FS.HISFC.Models.Registration.Register RegInfo
        {
            set;
        }

        /// <summary>
        /// 是否补打
        /// </summary>
        bool IsReprint
        {
            set;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="alPrintData"></param>
        int Print(ArrayList alPrintData);
    }
}
