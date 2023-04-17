using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Components.Manager
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
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Manager.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender));
        }

    }
}
