using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder
{
    /// <summary>
    /// 临床路径接口
    /// </summary>
    public interface ILCPInterface
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 获取该患者路径状态
        /// </summary>
        /// <param name="patInfo"></param>
        /// <returns></returns>
        FS.FrameWork.Models.NeuObject LCP_GetCPState(FS.HISFC.Models.RADT.PatientInfo patInfo);

        /// <summary>
        /// 新患者注册
        /// </summary>
        /// <param name="patInfo"></param>
        /// <returns></returns>
        FS.FrameWork.Models.NeuObject LCP_RegPatient(FS.HISFC.Models.RADT.PatientInfo patInfo);

        /// <summary>
        /// 下达医嘱
        /// </summary>
        /// <param name="patInfo"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        FS.FrameWork.Models.NeuObject LCP_SetOrder(FS.HISFC.Models.RADT.PatientInfo patInfo, ArrayList alOrder);
    }
}
