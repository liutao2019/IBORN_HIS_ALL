using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.DrugStore.ShenZhen.Common
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
                IOrder = FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy.Instance.CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder>(typeof(InterfaceManager), null);
            }

            return IOrder;
        }


        private static FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender ISender = null;

        /// <summary>
        /// 发消息接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender GetISender()
        {
            if (ISender == null)
            {
                ISender = FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy.Instance.CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender>(typeof(InterfaceManager), null, 2);
            }
            return ISender;

        }

        /// <summary>
        /// 设备状态
        /// </summary>

        private static FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender IMachineSender = null;

        /// <summary>
        /// 发消息接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender GetIMachineSender()
        {
            if (IMachineSender == null)
            {
                IMachineSender = FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy.Instance.CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender>(typeof(InterfaceManager), null, 1);
            }
            return IMachineSender;

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
    }
}
