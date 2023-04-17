using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.BizProcess
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        #region 消息发送接口

        /// <summary>
        /// 获取保存接口，默认为 Interface.SaveItem
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList> GetISaveAllPactInfo()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList>>(typeof(InterfaceManager), null);
        }

        #endregion
    }
}
