using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.RADT.BizProcess
{
    /// <summary>
    /// [功能描述: RADT接口管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取ADT接口
        /// </summary>
        /// <returns></returns>
        public static IADT GetIADT()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<IADT>(typeof(InterfaceManager), null);
        }
    }
}
