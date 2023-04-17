using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.CallQueue.InterfaceImplement.ZDLY
{
    /// <summary>
    /// FS.SOC.HISFC.CallQueue.Interface.INurseAssign
    /// </summary>
    public class NurseAssign : FS.HISFC.BizProcess.Interface.Nurse.INurseAssign
    {

        #region 变量

        FS.HISFC.BizProcess.Integrate.Manager manaIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();


        FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();


        FS.HISFC.BizLogic.Nurse.Assign assign = new FS.HISFC.BizLogic.Nurse.Assign();


        private static bool isInit = false;

        #endregion


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

            DateTime sysTime = assign.GetDateTimeFromSysDateTime().Date;

            if (string.IsNullOrEmpty(room.ID))
            {
                err = "无排班队列信息！";
                return -1;
            }

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

            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            nurseAssignMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            assign.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (nurseAssignMgr.Insert(nurseAssign) < 0)
            {
                err = nurseAssignMgr.Err;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;

            }

            if (nurseAssignMgr.Insert(nurseAssign) < 0)
            {
                err = nurseAssignMgr.Err;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;

            }


            //叫号时更新进诊标记
            if (assign.Update(register.ID, room, console, assign.GetDateTimeFromSysDateTime()) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                err =assign.Err;
                return -1;
            }


            #region 医生站屏幕显示

            FS.FrameWork.Models.NeuObject ipAddress = manaIntegrate.GetConstansObj("AssignRoomIP", room.ID);
            if (string.IsNullOrEmpty(ipAddress.Memo))
            {
                err = "未设置诊室【" + room.Name + "】进诊患者显示屏的IP!";
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            try
            {

                int result = 0;
                LEDDLL.SetTransMode(1, 3);
                result=LEDDLL.SetNetworkPara(1, ipAddress.Memo.ToCharArray());

                if (result == 0)
                {
                    err = "设置网络参数失败！";
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return - 1;
                }

                if (!isInit)
                {
                    LEDDLL.SendScreenPara(1, 2, 128, 32);
                    LEDDLL.SetPower(1, 0);
                    isInit = true;
                }
                //LEDDLL.SetPower(1, 1);
                //LEDDLL.SendControl(1, 1, IntPtr.Zero);



                LEDDLL.StartSend();  //初始化数据结构
                LEDDLL.AddControl(1, 2);        //参数依次为：屏号，单双色
                LEDDLL.AddProgram(1, 1, 0);     //参数依次为：屏号，节目号，节目播放时间


                result = LEDDLL.AddFileArea(1, 1, 1, 0, 0, 128, 16);
                if (result != 1)
                {
                    err="添加文件区域失败";
                    return -1;
                }

                //result=LEDDLL.AddQuitText(1, 1, 1, 0, 0, 128, 16, 255, "宋体", 12, 0, 0, 0, "当前患者："+register.Name);
                result = LEDDLL.AddFileString(1, 1, 1, 1, "请" + register.Name+"进诊", "宋体", 12, 255, false, false, false, 1, 128, 16, 41, 255, 100, 3, 1);  //闪烁3次
               
                if (result == 2)
                {
                    err = "添加动态文本错误！";
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;

                }
                result = LEDDLL.AddFileString(1, 1, 1, 2, "请" + register.Name + "进诊", "宋体", 12, 255, false, false, false, 1, 128, 16, 1, 255, 1, 255, 1);


                string next = "祝您健康！";

                FS.HISFC.Models.Nurse.Queue queue = this.queueMgr.GetQueueByDoct(register.DoctorInfo.Templet.Doct.ID, sysTime, noon.ID);
                if (!string.IsNullOrEmpty(queue.ID))
                {
                    ArrayList al = assign.Query(nurse.ID, sysTime, queue.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
                    if(al.Count>0)
                    {
                        next = "请" + (al[0] as FS.HISFC.Models.Nurse.Assign).Register.Name+"候诊";
                    }

                    ArrayList inList = assign.Query(nurse.ID, sysTime, queue.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.In);
                   
                    foreach (FS.HISFC.Models.Nurse.Assign inAssign in inList)
                    {
                        if (inAssign.Register.ID != register.ID)
                        {
                            int rev = assign.UpdateAssignFlag(inAssign.Register.ID, assign.Operator.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Delay);
                        }
                    }


                }


                LEDDLL.AddQuitText(1, 1, 2, 0, 16, 128, 16, 255, "宋体", 12, 0, 0, 0,next);
                LEDDLL.SendControl(1, 1, IntPtr.Zero);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }


            #endregion


            FS.FrameWork.Management.PublicTrans.Commit();


            return 1;
        }

        /// <summary>
        /// 叫号（根据护士站进行叫号）
        /// 获取所有需要叫号的申请信息
        /// </summary>
        public void Call(string nurseCode, string noonID)
        {
            FS.SOC.HISFC.CallQueue.BizProcess.NurseAssign.CreateInstance().CallAssign(nurseCode, CommonController.CreateInstance().GetNoon(noonID));
        }

        #endregion
    }
}
