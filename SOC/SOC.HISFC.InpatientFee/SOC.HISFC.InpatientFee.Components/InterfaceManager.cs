using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.InpatientFee.Components
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 住院发票打印接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy GetIBalanceInvoicePrint()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy>(typeof(InterfaceManager), null);
        }

        private static FS.HISFC.BizProcess.Interface.Account.IOperCard IOperCard = null;
        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Account.IOperCard GetIOperCard()
        {
            if (IOperCard == null)
            {
                IOperCard = FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy.Instance.CreateInferface<FS.HISFC.BizProcess.Interface.Account.IOperCard>(typeof(InterfaceManager), null);
            }
            return IOperCard;
        }
    }
}
