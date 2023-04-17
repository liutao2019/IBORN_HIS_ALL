using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.Assign.BizProcess.InterfaceImplement;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取分诊队列接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT GetIADTImplement()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT>(typeof(InterfaceManager), null);
        }

        /// <summary>
        /// 获取分诊队列接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender GetISenderImplement()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender>(typeof(InterfaceManager), null);
        }

        /// <summary>
        /// 获取叫号接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.CallQueue.Interface.INurseAssign GetCallQueueImplement()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.CallQueue.Interface.INurseAssign>(typeof(InterfaceManager), null);
        }

        /// <summary>
        /// 获取挂号更新接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.Registration.Register> GetISaveRegister()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.Registration.Register>>(typeof(InterfaceManager), new SaveRegister());
        }

        /// <summary>
        /// 获取挂号更新接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.SOC.HISFC.Assign.Models.Room> GetISaveRoom()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.SOC.HISFC.Assign.Models.Room>>(typeof(InterfaceManager), new SaveRoom());
        }

        /// <summary>
        /// 获取挂号更新接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.Nurse.Seat> GetISaveConsole()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.Nurse.Seat>>(typeof(InterfaceManager), new SaveConsole());
        }
    }
}
