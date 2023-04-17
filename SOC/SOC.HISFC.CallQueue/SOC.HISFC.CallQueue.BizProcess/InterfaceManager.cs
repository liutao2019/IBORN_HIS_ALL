using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.CallQueue.Interface;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.CallQueue.BizProcess
{
    /// <summary>
    /// 接口管理类
    /// </summary>
    public  class InterfaceManager
    {
        public static ICallSpeak GetICallSpeak()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<ICallSpeak>(typeof(InterfaceManager), new CallSpeak());
        }
    }
}
