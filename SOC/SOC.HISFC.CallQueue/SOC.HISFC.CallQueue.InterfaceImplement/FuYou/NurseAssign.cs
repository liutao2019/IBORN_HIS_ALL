using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;

namespace Neusoft.SOC.HISFC.CallQueue.InterfaceImplement.FuYou
{
    public class NurseAssign : Neusoft.HISFC.BizProcess.Interface.Nurse.INurseAssign
    {
        public NurseAssign()
        { 
            
        }

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
        public int Insert(Neusoft.HISFC.Models.Registration.Register register, Neusoft.FrameWork.Models.NeuObject dept, Neusoft.FrameWork.Models.NeuObject nurse, Neusoft.FrameWork.Models.NeuObject room, Neusoft.FrameWork.Models.NeuObject console, Neusoft.HISFC.Models.Base.Noon noon, ref string err)
        {
            //return new Neusoft.SOC.HISFC.CallQueue.InterfaceImplement.NurseAssign().Insert(register, dept, nurse, room, console, noon, ref err);
            Neusoft.SOC.HISFC.CallQueue.BizLogic.NurseAssign nurseAssignMgr = new Neusoft.SOC.HISFC.CallQueue.BizLogic.NurseAssign();
            //整合NurseAssign实体
            Neusoft.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign = new Neusoft.SOC.HISFC.CallQueue.Models.NurseAssign();
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
            nurseAssign.Oper.ID = Neusoft.FrameWork.Management.Connection.Operator.ID;

            //插入叫号申请
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
            new Neusoft.SOC.HISFC.CallQueue.InterfaceImplement.NurseAssign().Call(nurseCode, noonID);
        }

        #endregion
    }
}
