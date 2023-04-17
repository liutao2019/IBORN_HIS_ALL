using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder
{
    /// <summary>
    /// 审核医嘱接口
    /// </summary>
    public interface IConfirmOrder
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
        /// <param name="alOrders"></param>
        /// <returns></returns>
        int AfterRefreshOrder(ref ArrayList alOrders);

        /// <summary>
        /// 审核保存前接口
        /// </summary>
        /// <param name="alOrders"></param>
        /// <returns></returns>
        int BeforeConfirmOrder(ref ArrayList alOrders);

        /// <summary>
        /// 审核保存后接口
        /// </summary>
        /// <param name="alOrders"></param>
        /// <returns></returns>
        int AfterConfirmOrder(ref ArrayList alOrders);
    }
}
