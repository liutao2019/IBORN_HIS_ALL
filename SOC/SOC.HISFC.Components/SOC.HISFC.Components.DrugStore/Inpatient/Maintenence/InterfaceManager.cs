using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Maintenence
{
    public class InterfaceManager
    {
          /// <summary>
        /// 获取消息发送接口实现
        /// </summary>
        /// <returns></returns>
        public static object GetBizInfoSenderImplement()
        {
            return FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.DrugStore.Inpatient.Maintenence.InterfaceManager), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender));
        }
   
    }
}
