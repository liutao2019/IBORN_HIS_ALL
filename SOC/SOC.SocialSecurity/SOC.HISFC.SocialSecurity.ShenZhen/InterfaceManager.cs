using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen
{
    /// <summary>
    /// [功能描述: 药库接口管理类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public class InterfaceManager
    {
      

     


        /// <summary>
        /// 获取消息发送接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetBizInfoSenderImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.SocialSecurity.ShenZhen.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender));
        }
    }
}
