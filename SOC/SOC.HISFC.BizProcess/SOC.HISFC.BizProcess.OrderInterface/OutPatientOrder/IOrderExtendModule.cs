using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder
{
    /// <summary>
    /// 门诊医生站扩展按钮功能，提供三个扩展按钮
    /// 按钮1 只能在开立模式下使用
    /// 按钮2 任何时候都可以使用
    /// 按钮3 只能在非开立模式下使用
    /// </summary>
    public interface IOrderExtendModule
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
        /// 扩展按钮1的功能实现
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="regObj"></param>
        /// <param name="alOutOrders"></param>
        /// <returns></returns>
        int DoOrderExtend1(IWin32Window owner, FS.HISFC.Models.Registration.Register regObj, ArrayList alOutOrders);

        /// <summary>
        /// 扩展按钮2的功能实现
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="regObj"></param>
        /// <param name="alOutOrders"></param>
        /// <returns></returns>
        int DoOrderExtend2(IWin32Window owner, FS.HISFC.Models.Registration.Register regObj, ArrayList alOutOrders);

        /// <summary>
        /// 扩展按钮3的功能实现
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="regObj"></param>
        /// <param name="alOutOrders"></param>
        /// <returns></returns>
        int DoOrderExtend3(IWin32Window owner, FS.HISFC.Models.Registration.Register regObj, ArrayList alOutOrders);
    }
}
