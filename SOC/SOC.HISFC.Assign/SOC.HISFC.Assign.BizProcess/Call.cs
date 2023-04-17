using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: SOC叫号综合类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Call
    {
        /// <summary>
        /// 插入叫号信息
        /// </summary>
        /// <returns></returns>
        public int InsertQueue(FS.FrameWork.Models.NeuObject nurse, FS.SOC.HISFC.Assign.Models.Assign assign, ref string error)
        {
            if (InterfaceManager.GetCallQueueImplement() is FS.SOC.HISFC.CallQueue.Interface.INurseAssign)
            {
                InterfaceManager.GetCallQueueImplement().Insert(assign.Register, assign.Queue.AssignDept, nurse, assign.Queue.SRoom, assign.Queue.Console, CommonController.CreateInstance().GetNoon(CommonController.CreateInstance().GetSystemTime()), ref error);
                return 1;
            }
            else if (InterfaceManager.GetCallQueueImplement() == null)
            {
                error = "没维护接口：FS.SOC.HISFC.CallQueue.Interface.INurseAssign的实现";
                return 0;
            }

            error = "接口实现不是指定类型：FS.SOC.HISFC.CallQueue.Interface.INurseAssign";
            return -1;
        }

        /// <summary>
        /// 叫号
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="noonID"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int CallQueue(string nurseCode, string noonID, ref string errInfo)
        {
            if (InterfaceManager.GetCallQueueImplement() is FS.SOC.HISFC.CallQueue.Interface.INurseAssign)
            {
                InterfaceManager.GetCallQueueImplement().Call(nurseCode, noonID);
                return 1;
            }
            else if (InterfaceManager.GetCallQueueImplement() == null)
            {
                errInfo = "没维护接口：FS.SOC.HISFC.CallQueue.Interface.INurseAssign的实现";
                return 0;
            }

            errInfo = "接口实现不是指定类型：FS.SOC.HISFC.CallQueue.Interface.INurseAssign";
            return -1;
        }
    }
}
