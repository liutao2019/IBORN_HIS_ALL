using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A05
{
    /// <summary>
    /// 医生叫号
    /// </summary>
    public class DoctorCallPatient 
    {
        private FS.HISFC.BizLogic.Nurse.Queue queueManager = new FS.HISFC.BizLogic.Nurse.Queue();
        private FS.SOC.HISFC.Assign.BizLogic.Queue socQueueManager = new FS.SOC.HISFC.Assign.BizLogic.Queue();
        private FS.HISFC.BizProcess.Integrate.Manager managerAssign = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
        FS.SOC.HISFC.Assign.BizLogic.Console consoleMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
        FS.SOC.HISFC.Assign.BizLogic.Room roomMgr = new FS.SOC.HISFC.Assign.BizLogic.Room();

        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A05 adtA05, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string operCode = adtA05.PV1.GetAttendingDoctor(0).IDNumber.Value;
            if (string.IsNullOrEmpty(operCode))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "医生叫号，原因：医生编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = CommonController.CreateInstance().GetEmployee(operCode);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "医生叫号，原因：传入的医生编码，系统中找不到" + operCode;
                return -1;
            }

            //看诊科室
            string deptID = adtA05.PV1.AssignedPatientLocation.Facility.NamespaceID.Value;
            if (string.IsNullOrEmpty(deptID))
            {
                errInfo = "医生叫号失败，原因：科室编码为空";
                return -1;
            }
            ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept = CommonController.CreateInstance().GetDepartment(deptID);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept == null)
            {
                errInfo = "医生叫号失败，原因：传入的科室编码，系统中找不到" + deptID;
                return -1;
            }

            //诊室
            string roomID = adtA05.PV1.AssignedPatientLocation.PointOfCare.Value;
            if (string.IsNullOrEmpty(roomID))
            {
                errInfo = "医生叫号失败，原因：诊室编码为空";
                return -1;
            }

            //诊台
            string consoleID = adtA05.PV1.AssignedPatientLocation.Bed.Value;
            if (string.IsNullOrEmpty(consoleID))
            {
                errInfo = "医生叫号失败，原因：诊台编码为空";
                return -1;
            }

            //患者编号
            string registerID = adtA05.PV1.VisitNumber.ID.Value;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.managerAssign.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.queueManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.statManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.socQueueManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            roomMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime dtNow = this.queueManager.GetDateTimeFromSysDateTime();
            //取午别-延迟
            FS.HISFC.Models.Base.Noon noon = CommonController.CreateInstance().GetNoon(dtNow);
            if (noon == null || string.IsNullOrEmpty(noon.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "医生叫号失败，原因：当期时间没有维护午别" + dtNow.ToString();
                return -1;
            }

            FS.FrameWork.Models.NeuObject nurse = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept;
            //找护士站
            ArrayList al = statManager.LoadByChildren("14", nurse.ID);
            //查找权限科室对应的护士站
            if (al != null && al.Count > 0)
            {
                nurse = CommonController.CreateInstance().GetDepartment((al[0] as FS.HISFC.Models.Base.DepartmentStat).PardepCode);
            }

            FS.HISFC.Models.Nurse.Assign assign = null;

            if (string.IsNullOrEmpty(registerID))//患者编号为空，说明叫下一个
            {
                //先根据医生找队列
                //如果没有医生队列，就找级别队列
                //如果前者都没有，就找自定义队列（分诊科室+诊室+诊台）
                //根据分诊科室+诊室+诊台找对应的队列
                FS.SOC.HISFC.Assign.Models.Queue queue = socQueueManager.GetQueue(nurse.ID, deptID, noon.ID, operCode, roomID, consoleID);

                assign = this.managerAssign.QueryWait(queue.ID, dtNow.Date, dtNow.Date.AddDays(1).AddSeconds(-1));
                //if (assign == null)//一个人没有，则找临近队列
                //{
                //    errInfo = "医生叫号失败，原因：队列中已无患者";
                //    return -1;
                //}

            }
            else
            {
                assign = managerAssign.QueryAssignByClinicID(registerID);
                if (assign != null && assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "医生叫号失败，原因：患者已诊出" + registerID;
                    return -1;
                }
            }

            if (assign == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "医生叫号失败，原因：没有对应的叫号患者";
                return -1;
            }

            //取诊室诊台
            assign.Queue.SRoom = roomMgr.GetRoom(roomID);
            if (assign.Queue.SRoom == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "医生叫号失败，原因：获取诊室失败"+roomID + roomMgr.Err;
                return -1;
            }

            assign.Queue.Console = consoleMgr.GetConsole(consoleID);
            if (assign.Queue.Console == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "医生叫号失败，原因：获取诊室失败" + consoleID + consoleMgr.Err;
                return -1;
            }

            if (this.managerAssign.Update(assign.Register.ID, assign.Queue.SRoom, assign.Queue.Console, dtNow) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "医生叫号失败，原因：" + this.managerAssign.Err;
                return -1;
            }


            //取队列信息
            string err = "";
            //叫号不判断返回值
            FS.SOC.HISFC.CallQueue.Interface.INurseAssign INurseAssign = ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.CallQueue.Interface.INurseAssign>(this.GetType(), null);
            if (INurseAssign == null)
            {
                INurseAssign = new FS.SOC.HISFC.CallQueue.InterfaceImplement.NurseAssign();
            }

            INurseAssign.Insert(assign.Register, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept, nurse, assign.Queue.SRoom, assign.Queue.Console, noon, ref err);



            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
