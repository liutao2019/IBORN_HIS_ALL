using System;
using System.Collections.Generic;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取业务操作信息消息收发接口
        /// </summary>
        /// <returns></returns>
        public static object GetBizInfoSenderImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender));
        }

        private static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT IADT = null;
        /// <summary>
        /// 获取业务操作ADT信息消息收发接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT GetIADT()
        {
            if (IADT == null)
            {
                IADT = FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy.Instance.CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT>(typeof(InterfaceManager), null);
            }

            return IADT;
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

        /// <summary>
        /// 获取IReadIDCard接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Account.IReadIDCard GetIReadIDCard()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.HISFC.BizProcess.Interface.Account.IReadIDCard>(typeof(InterfaceManager), null);
        }

        /// <summary>
        /// 获取IReadMCard接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Account.IReadMCard GetIReadMCard()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.HISFC.BizProcess.Interface.Account.IReadMCard>(typeof(InterfaceManager), null);
        }
    }
}
