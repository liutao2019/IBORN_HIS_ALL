using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.HISFC.Components.OutpatientFee
{
    public class InterfaceManager
    {
        private static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder IOrder = null;
        /// <summary>
        /// 获取业务操作ADT信息消息收发接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder GetIOrder()
        {
            if (IOrder == null)
            {
                IOrder = ControllerFactroy.Instance.CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder>(typeof(InterfaceManager), null);
            }

            return IOrder;
        }

        /// <summary>
        /// 获取执行科室的接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Fee.IExecDept GetIExecDept()
        {
            return ControllerFactroy.Instance.CreateInferface<FS.HISFC.BizProcess.Interface.Fee.IExecDept>(typeof(InterfaceManager), null);
        }

        /// <summary>
        /// 获取分发票接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.FeeInterface.ISplitInvoice GetISplitInvoice()
        {
            return ControllerFactroy.Instance.CreateInferface<FS.HISFC.BizProcess.Interface.FeeInterface.ISplitInvoice>(typeof(InterfaceManager), null);
        }

        public static FS.HISFC.BizProcess.Interface.Account.IReadIDCard GetIReadIDCard()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.HISFC.BizProcess.Interface.Account.IReadIDCard>(typeof(InterfaceManager), null);
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
