using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder
{
    /// <summary>
    /// 医嘱分解接口
    /// </summary>
    public interface IConfirmExecOrder
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string Err
        {
            get;
            set;
        }

        /// <summary>
        /// 刷新审核的医嘱列表后操作接口
        /// </summary>
        /// <param name="alExecOrders"></param>
        /// <returns></returns>
        int AfterRefreshExecOrder(ref ArrayList alExecOrders);

        /// <summary>
        /// 审核保存前接口
        /// </summary>
        /// <param name="alExecOrders"></param>
        /// <returns></returns>
        int BeforeConfirmExecOrder(ref ArrayList alExecOrders);

        /// <summary>
        /// 审核保存后接口
        /// </summary>
        /// <param name="alExecOrders"></param>
        /// <returns></returns>
        int AfterConfirmExecOrder(ref ArrayList alExecOrders);
    }
}
