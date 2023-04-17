﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Components.InpatientFee
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        private static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT IADT = null;
        /// <summary>
        /// 获取业务操作ADT信息消息收发接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT GetIADT()
        {
            IADT = IADT ?? FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT>(typeof(InterfaceManager), null);

            return IADT;
        }
    }
}
