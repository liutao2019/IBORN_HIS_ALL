using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.Local.CallQueue.ZhuHai.ZDWY.INurseAssign
{
    public class NurseAssign : FS.SOC.HISFC.CallQueue.Interface.INurseAssign
    {
        FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign nurseAssignMgr = new FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign();
        FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();

        FS.HISFC.BizLogic.Nurse.Assign assignManager = new FS.HISFC.BizLogic.Nurse.Assign();

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
            DateTime dtNow = nurseAssignMgr.GetDateTimeFromSysDateTime();

            if (noon == null || string.IsNullOrEmpty(noon.ID))
            {
                noon = new FS.HISFC.Models.Base.Noon();
                noon.ID = CommonController.CreateInstance().GetNoonID(dtNow);//午别
            }
            string roomID = "";
            if (room is FS.HISFC.Models.Nurse.Seat)
            {
                roomID = ((FS.HISFC.Models.Nurse.Seat)room).PRoom.ID;
            }
            else
            {
                roomID = room.ID;
            }
            string errinfo = string.Empty;
            //自动叫号
            if (register == null || string.IsNullOrEmpty(register.ID))
            {
                ArrayList alPatient = assignMgr.QueryByDoctOrRoomForNurse(nurse.ID, dept.ID, dtNow.Date, ((FS.HISFC.Models.Base.Employee)assignMgr.Operator).ID, roomID);

                if (alPatient == null)
                {
                    err = "查询分诊患者信息出错！\r\n" + assignMgr.Err;
                    return -1;
                }

                //过滤掉医师科室的患者，避免叫到同一个护士站不同的科室。{74E6DF15-A803-4ACC-B618-0C05E271AA97}
                ArrayList validPatients = new ArrayList();
                foreach (FS.SOC.HISFC.Assign.Models.Assign assign in alPatient)
                {
                    if (assign.Queue.AssignDept.ID != dept.ID)
                    {
                        continue;
                    }

                    validPatients.Add(assign);
                }

                // {74E6DF15-A803-4ACC-B618-0C05E271AA97}
                if (validPatients.Count == 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "自动叫号失败！\r\n当前无待诊患者！", System.Windows.Forms.ToolTipIcon.Info);

                    return 1;
                }

                // {74E6DF15-A803-4ACC-B618-0C05E271AA97}
                foreach (FS.SOC.HISFC.Assign.Models.Assign assign in validPatients)
                {
                    FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign = new FS.SOC.HISFC.CallQueue.Models.NurseAssign();
                    nurseAssign.PatientID = assign.Register.ID;
                    nurseAssign.PatientSeeNO = assign.Register.DoctorInfo.SeeNO.ToString();
                    nurseAssign.PatientName = assign.Register.Name;
                    nurseAssign.PatientCardNO = assign.Register.PID.CardNO;
                    nurseAssign.PatientSex = assign.Register.Sex.ID.ToString();
                    nurseAssign.Room.ID = roomID;
                    nurseAssign.Room.Name = room.Name;
                    nurseAssign.Dept.ID = dept.ID;
                    nurseAssign.Dept.Name = dept.Name;
                    nurseAssign.Nurse.ID = nurse.ID;
                    nurseAssign.Nurse.Name = nurse.Name;
                    nurseAssign.Console.ID = console.ID;
                    nurseAssign.Console.Name = console.Name;
                    nurseAssign.Noon.ID = noon.ID;
                    nurseAssign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;


                    #region 自动叫号时判断只允许叫进来一个患者
                    ArrayList alCalled = assignManager.QueryCalledList(FS.FrameWork.Management.Connection.Operator.ID, assign.Register.ID);
                    if (alCalled != null)
                    {
                        foreach (FS.HISFC.Models.Nurse.Assign assObj in alCalled)
                        {
                            err = "已经叫号的患者【" + assObj.Register.Name + "[" + assObj.SeeNO.ToString() + "]】未看诊结束，不能继续叫号！\r\n\r\n请先处理完已经叫号的患者！";
                        }
                        return -1;
                    }

                    #endregion


                    int i = nurseAssignMgr.Insert(nurseAssign);
                    err = nurseAssignMgr.Err;
                    if (i <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        // 更新进诊标志，未分诊过的，需要更新分诊信息。{8D54CCC6-4FE7-4228-8FC6-57375F7387BB}
                        int result = 1;
                        if (assign.TriageStatus != FS.HISFC.Models.Nurse.EnuTriageStatus.Out)
                        {
                            object[] args = {
                                                assign.Register.ID, 
                                                room.ID,
                                                room.Name, 
                                                FS.FrameWork.Management.Connection.Operator.ID,
                                                dtNow , 
                                                (int)FS.HISFC.Models.Nurse.EnuTriageStatus.In
                                            };
                            result = this.UpdateAssignStaus(assign.Register.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.In, true, args, ref errinfo);
                        }
                        if (result <= 0)
                        {
                            err = errinfo;
                            return -1;
                        }

                        // {d205b726-1589-483a-bb5f-d533715c8354} 由于BalloonTip 的提醒方式不够突出和明显，
                        // 医生在点击后，如果注意不到时，会多次再点叫号，导致公共队列中有多个患者被叫号，其它医师叫不了号。
                        // 为更明确的提示给用户，这里使用MessageBox 弹出窗口。
                        //FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3,  "提示", "叫号成功，已经将该患者【" + assign.Register.Name + "】添加到叫号队列",System.Windows.Forms.ToolTipIcon.Info);
                        System.Windows.Forms.MessageBox.Show(
                            "患者【" + assign.Register.Name + "】叫号成功！",
                            "叫号提示",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Exclamation);

                        register = assign.Register;
                        return 1;//只取第一个
                    }
                }

                return 1;
            }
            //选择患者叫号
            else
            {
                //整合NurseAssign实体
                FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign = new FS.SOC.HISFC.CallQueue.Models.NurseAssign();
                nurseAssign.PatientID = register.ID;
                nurseAssign.PatientSeeNO = register.DoctorInfo.SeeNO.ToString();
                nurseAssign.PatientName = register.Name;
                nurseAssign.PatientCardNO = register.PID.CardNO;
                nurseAssign.PatientSex = register.Sex.ID.ToString();
                nurseAssign.Room.ID = roomID;
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
                int i = nurseAssignMgr.Insert(nurseAssign);

                // 更新进诊标志，未分诊过的，需要更新分诊信息。{c67085a4-240b-463f-b984-86f13022d506}
                int result = 1;
                if (!register.IsSee)
                {
                    object[] args = {
                                        register.ID, 
                                        room.ID,
                                        room.Name, 
                                        FS.FrameWork.Management.Connection.Operator.ID,
                                        dtNow , 
                                        (int)FS.HISFC.Models.Nurse.EnuTriageStatus.In
                                    };
                    result = this.UpdateAssignStaus(register.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.In, true, args, ref errinfo);
                }
                if (result <= 0)
                {
                    err = errinfo;
                    return -1;
                }
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + register.Name + "】叫号成功！", System.Windows.Forms.ToolTipIcon.Info);

                return 1;
            }
        }

        /// <summary>
        /// 更新分诊状态
        /// </summary>
        /// <param name="clinicno"></param>
        /// <param name="triagerStatus"></param>
        /// <param name="errinfo"></param>
        /// <param name="isAssignInUpdateCallInfo">进诊状态时是否更新分诊信息。</param>
        /// <param name="updateAssignParms">进诊时更新的参数数组，</param>
        /// <paramref name="isAssignInUpdateCallInfo"/>为True时传入。</param>
        /// <returns></returns>
        /// {8D54CCC6-4FE7-4228-8FC6-57375F7387BB}
        public int UpdateAssignStaus(string clinicno, FS.HISFC.Models.Nurse.EnuTriageStatus triagerStatus, bool isAssignInUpdateCallInfo, object[] updateAssignParms, ref string errinfo)
        {
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.SOC.HISFC.Assign.Models.Assign assignRecord = new FS.SOC.HISFC.Assign.Models.Assign();
            FS.HISFC.BizLogic.Nurse.Assign assignManager = new FS.HISFC.BizLogic.Nurse.Assign();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            assignManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            queueMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            assignRecord = assignMgr.QueryByClinicID(clinicno);
            int result = 0;
            switch (assignRecord.TriageStatus)
            {
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Delay:
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Out:
                    result = 1;
                    break;
                default:
                    result = assignManager.UpdateAssignFlag(clinicno, FS.FrameWork.Management.Connection.Operator.ID, triagerStatus);
                    break;
            }
            if (result <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errinfo = assignMgr.Err;
                return -1;
            }

            // 当为进诊、需要更新进诊信息时，更新进诊信息。{8D54CCC6-4FE7-4228-8FC6-57375F7387BB}
            if (triagerStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In && isAssignInUpdateCallInfo)
            {
                if (assignManager.UpdateAssignAfterCall(updateAssignParms) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errinfo = assignMgr.Err;
                    return -1;
                }
            }

            result = queueMgr.UpdateWaitingNum(assignRecord.Queue.ID, -1);
            if (result <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errinfo = queueMgr.Err;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
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

        #endregion

        #region INurseAssign 成员

        public int Init(FS.FrameWork.Models.NeuObject doct, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
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

        //{8225C046-D7AE-4228-9BFE-1D933C731A04}
        public int ReCall(string tmp)
        {
            return 1;
        }

        public int CancelCall(string tmp)
        {
            return 1;
        }

        #endregion
    }
}
