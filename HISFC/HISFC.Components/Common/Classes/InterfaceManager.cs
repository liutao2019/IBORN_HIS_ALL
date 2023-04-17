using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Common.Classes
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class InterfaceManager
    {

        private static FS.HISFC.BizProcess.Interface.Account.IOperCard IOperCard = null;
        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.BizProcess.Interface.Account.IOperCard GetIOperCard()
        {
            if (IOperCard == null)
            {
                IOperCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(InterfaceManager), typeof(FS.HISFC.BizProcess.Interface.Account.IOperCard)) as FS.HISFC.BizProcess.Interface.Account.IOperCard;
            }
            return IOperCard;
        }
    }
}
