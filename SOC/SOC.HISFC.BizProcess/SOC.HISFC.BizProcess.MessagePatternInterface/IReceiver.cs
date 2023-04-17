using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePatternInterface
{
    public interface IReceiver 
    {
        /// <summary>
        /// 处理接收消息
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        int Receive(object o, ref object ackMessage,ref string errInfo);
    }
}
