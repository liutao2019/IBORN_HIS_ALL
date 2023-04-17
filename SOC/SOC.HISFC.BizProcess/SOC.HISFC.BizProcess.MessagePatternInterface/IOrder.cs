using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePatternInterface
{
    /// <summary>
    /// [功能描述: 业务消息模式：医嘱处理]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface IOrder
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string Err
        {
            get;
            set;
        }

        ///// <summary>
        ///// 发送消息（费用，医嘱，发药申请）
        ///// </summary>
        ///// <param name="patientInfo">患者信息</param>
        ///// <param name="alFeeInfo">费用或医嘱或发药申请信息</param>
        ///// <param name="isPositive">正流程或负流程</param>
        ///// <returns></returns>
        //int SendFeeInfo(FS.HISFC.Models.Registration.Register regInfo, ArrayList alObj, bool isPositive);

        /// <summary>
        /// 发送消息药品
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="alFeeInfo">费用或医嘱或发药申请信息</param>
        /// <param name="isPositive">正流程或负流程</param>
        /// <returns></returns>
        int SendDrugInfo(object patientInfo, ArrayList alFeeInfo, bool isPositive);


        ///// <summary>
        ///// 发送消息（费用）
        ///// </summary>
        ///// <param name="regInfo"></param>
        ///// <param name="alobj"></param>
        ///// <param name="isPositive"></param>
        ///// <returns></returns>
        int SendFeeInfo(object regInfo, ArrayList alObj, bool isPositive);

        /// <summary>
        /// 发送消息（费用）
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="alObj"></param>
        /// <param name="isPositive"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        int SendFeeInfo(object regInfo, ArrayList alObj, bool isPositive ,string flag);






    }
}
