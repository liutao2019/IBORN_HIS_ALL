using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.CallQueue.InterfaceImplement
{
    public class NurseAssign:FS.SOC.HISFC.CallQueue.Interface.INurseAssign
    {
        #region INurseAssign 成员

        /// <summary>
        /// 插入叫号申请信息
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dept"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            //整合NurseAssign实体
            FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign = new FS.SOC.HISFC.CallQueue.Models.NurseAssign();
            nurseAssign.PatientID = register.ID;
            nurseAssign.PatientSeeNO = register.DoctorInfo.SeeNO.ToString();
            nurseAssign.PatientName = register.Name;
            nurseAssign.PatientCardNO = register.PID.CardNO;
            nurseAssign.PatientSex = register.Sex.ID.ToString();
            nurseAssign.Room.ID = room.ID;
            nurseAssign.Room.Name = room.Name;
            nurseAssign.Dept.ID = dept.ID;
            nurseAssign.Dept.Name = dept.Name;
            nurseAssign.Nurse.ID = nurse.ID;
            nurseAssign.Nurse.Name = nurse.Name;
            nurseAssign.Console.ID = console.ID;
            nurseAssign.Console.Name = console.Name;
            nurseAssign.Noon.ID = noon.ID;
            nurseAssign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

            //插入叫号申请
            FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign nurseAssignMgr = new FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign();
            int i = nurseAssignMgr.Insert(nurseAssign);
            err = nurseAssignMgr.Err;

            return i;
        }

        /// <summary>
        /// 叫号（根据护士站进行叫号）
        /// 获取所有需要叫号的申请信息
        /// </summary>
        public void Call(string nurseCode, string noonID)
        {
            //只叫号本护士站的，当前午别的病人
            FS.SOC.HISFC.CallQueue.BizProcess.NurseAssign.CreateInstance().CallAssign(nurseCode, CommonController.CreateInstance().GetNoon(noonID));
        }

        //{8225C046-D7AE-4228-9BFE-1D933C731A04}
        public int ReCall(string tmp)
        {
            return 1;
        }

        public int CancelCall(string tmp)
        {
            return 1;
        }

        public int Close(FS.FrameWork.Models.NeuObject doct, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            return 1;
        }

        public int DelayCall(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            return 1;
        }

        public int DiagOut(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            return 1;
        }

        public int Init(FS.FrameWork.Models.NeuObject doct, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            return 1;
        }

        #endregion
    }
}
