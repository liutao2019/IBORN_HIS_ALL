using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: SOC分诊综合类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Assign : FS.SOC.HISFC.BizProcess.CommonInterface.Common.AbstractBizProcess
    {
        #region 获取看诊序号

        /// <summary>
        /// 获取护士站的看诊序号（用于分诊）
        /// </summary>
        /// <param name="isExpert">是否专家</param>
        /// <param name="seeType">看诊序号类别，缺省为医生或科室的自定义码，如果都没有则为N</param>
        /// <param name="seeNO">返回的看诊序号</param>
        /// <param name="error">错误信息</param>
        /// <param name="subjectID">专家为医生部位，普通为护士站编码</param>
        /// <returns>-1 失败 1成功</returns>
        public int GetNurseSeeNO(bool isExpert, string seeType, string subjectID, ref string seeNO, out string error)
        {
            this.BeginTransaction();
            if (this.getNurseSeeNO(isExpert, seeType, subjectID, ref seeNO, out error) < 0)
            {
                this.RollBack();
                return -1;
            }

            this.Commit();

            return 1;
        }

        /// <summary>
        /// 获取护士站的看诊序号（用于挂号自动分诊）
        /// </summary>
        /// <param name="isExpert"></param>
        /// <param name="seeType"></param>
        /// <param name="subjectID"></param>
        /// <param name="seeNO"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int GetNurseSeeNOForReg(bool isExpert, string seeType, string subjectID, ref string seeNO, out string error)
        {
            if (this.getNurseSeeNO(isExpert, seeType, subjectID, ref seeNO, out error) < 0)
            {
                this.RollBack();
                return -1;
            }
            return 1;
        }

        private int getNurseSeeNO(bool isExpert, string seeType, string subjectID, ref string seeNO, out string error)
        {
            error = "";
            if (string.IsNullOrEmpty(seeType))
            {
                if (isExpert)
                {
                    seeType = "1";//医生
                }
                else
                {
                    seeType = "3";//护士站
                }
            }
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            assignMgr.SetTrans(this.Trans);
            //全天走一个号
            DateTime dtNow = assignMgr.GetDateTimeFromSysDateTime();
            string noonID = "A";// CommonController.CreateInstance().GetNoonID(dtNow);
            int i = assignMgr.UpdateSeeNO(seeType, dtNow, subjectID, noonID);
            if (i < 0)
            {
                error = "更新看诊序号失败，原因：" + assignMgr.Err;
                return -1;
            }
            else if (i == 0)
            {
                if (assignMgr.InsertSeeNO(seeType, dtNow, subjectID, noonID) <= 0)
                {
                    error = "插入看诊序号失败，原因：" + assignMgr.Err;
                    return -1;
                }
            }

            //取看诊序号
            int num = 0;
            if (assignMgr.GetSeeNO(seeType, dtNow, subjectID, noonID, ref num) <= 0)
            {
                error = "获取看诊序号失败，原因：" + assignMgr.Err;
                return -1;
            }

            //看诊类别+看诊序号
            seeNO = seeType + num.ToString().PadLeft(4, '0');

            return 1;
        }

        #endregion

        #region 分诊相关

        /// <summary>
        /// 分诊
        /// </summary>
        /// <param name="this.Trans"></param>
        /// <param name="assign"></param>
        /// <param name="TrigeWhereFlag">分诊标志 1.分到队列  2.分到诊台</param>
        /// <param name="error"></param>
        /// <param name="isClearDoctorInfo">当更新为分诊时，是否清空医师。</param>
        /// <param name="isClearRoomInfo">当更新为分诊时，是否清空诊室。</param>
        /// <returns></returns>
        public int Triage(
            FS.SOC.HISFC.Assign.Models.Assign assign,
            bool isTriage,
            bool isClearDoctorInfo,
            bool isClearRoomInfo,
            ref string error)
        {
            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            assignMgr.SetTrans(this.Trans);
            regMgr.SetTrans(this.Trans);
            queueMgr.SetTrans(this.Trans);

            //[2011-10-26]看诊序号改成了字符型，本功能不再使用，注释掉
            // 1、获取队列最大看诊序号
            if (string.IsNullOrEmpty(assign.Register.DoctorInfo.SeeNO.ToString()))
            {
                string seeNO = "";
                FS.HISFC.Models.Registration.RegLevel regLevel = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(assign.Register.DoctorInfo.Templet.RegLevel.ID);
                if (regLevel == null)
                {
                    this.RollBack();
                    error = "获取挂号级别失败";
                    return -1;
                }
                assign.Register.DoctorInfo.Templet.RegLevel = regLevel;
                if (this.getNurseSeeNO(regLevel.IsExpert, null, regLevel.IsExpert ? assign.Register.DoctorInfo.Templet.Doct.ID : assign.Queue.AssignNurse.ID, ref seeNO, out error) < 0)
                {
                    this.RollBack();
                    return -1;
                }

                assign.SeeNO = seeNO;
            }
            else
            {
                assign.SeeNO = assign.Register.DoctorInfo.SeeNO.ToString();
            }

            //2、插入分诊信息表
            if (isTriage)
            {
                if (assignMgr.Update(
                    assign.TriageStatus,
                    assign.Register.ID,
                    isClearDoctorInfo,
                    isClearRoomInfo,
                    assign.Queue.SRoom,
                    assign.Queue.Console,
                    "") <= 0)
                {
                    this.RollBack();
                    error = assignMgr.Err;
                    return -1;
                }
            }
            else
            {
                if (assignMgr.Insert(assign) == -1)
                {
                    this.RollBack();
                    error = assignMgr.Err;
                    return -1;
                }
            }

            //3、更新挂号信息表，置分诊标志
            if (regMgr.Update(assign.Register.ID, FS.FrameWork.Management.Connection.Operator.ID,
                CommonController.CreateInstance().GetSystemTime()) == -1)
            {
                this.RollBack();
                error = regMgr.Err;
                return -1;
            }

            //4.队列数量增加1
            if (queueMgr.UpdateWaitingNum(assign.Queue.ID, -1) == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }

            if (InterfaceManager.GetIADTImplement() != null)
            {
                if (InterfaceManager.GetIADTImplement().AssignInfo(assign, true, 0) < 0)
                {
                    this.RollBack();
                    error = "分诊失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADTImplement().Err;
                    return -1;
                }
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="this.Trans"></param>
        /// <param name="assign"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int CancelTriage(FS.SOC.HISFC.Assign.Models.Assign assign, ref string error)
        {

            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            assignMgr.SetTrans(this.Trans);
            regMgr.SetTrans(this.Trans);
            queueMgr.SetTrans(this.Trans);

            //删除分诊信息
            int rtn = assignMgr.Delete(assign.Register.ID);
            if (rtn == -1)//出错
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }

            if (rtn == 0)
            {
                this.RollBack();
                error = "该分诊信息状态已经发生改变,请刷新屏幕!";
                return -1;
            }
            //恢复挂号信息的分诊状态
            rtn = regMgr.CancelTriage(assign.Register.ID);
            if (rtn == -1)
            {
                this.RollBack();
                error = regMgr.Err;
                return -1;
            }
            //4.队列数量-1
            if (queueMgr.UpdateWaitingNum(assign.Queue.ID, -1) == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }

            if (FS.HISFC.Models.Nurse.EnuTriageStatus.Delay != assign.TriageStatus)
            {

                if (InterfaceManager.GetIADTImplement() != null)
                {
                    if (InterfaceManager.GetIADTImplement().AssignInfo(assign, false, 0) < 0)
                    {
                        this.RollBack();
                        error = "取消分诊失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADTImplement().Err;
                        return -1;
                    }
                }
            }


            this.Commit();

            return 1;
        }

        /// <summary>
        /// 进诊
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int In(FS.SOC.HISFC.Assign.Models.Assign assign, FS.FrameWork.Models.NeuObject roomObj, FS.FrameWork.Models.NeuObject consoleObj, ref string error)
        {

            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            assignMgr.SetTrans(this.Trans);
            queueMgr.SetTrans(this.Trans);

            // {d4ea07b4-2559-4473-ac92-f8076d9ce3b4}
            int rtn = assignMgr.Update(FS.HISFC.Models.Nurse.EnuTriageStatus.In, assign.Register.ID, false, false, roomObj, consoleObj, null);
            if (rtn == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }
            if (rtn == 0)
            {
                this.RollBack();
                error = "该患者分诊状态已经改变,请重新刷新屏幕!";
                return 0;
            }

            //更新候诊人数
            if (queueMgr.UpdateWaitingNum(assign.Queue.ID, 1) == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }

            if (InterfaceManager.GetIADTImplement() != null)
            {
                if (InterfaceManager.GetIADTImplement().AssignInfo(assign, true, 1) == -1)
                {
                    this.RollBack();
                    error = "进诊失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADTImplement().Err;
                    return -1;
                }
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 取消进诊
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="isTriageClearRoomDoctorInfo">更新为分诊状态时，是否清空医师信息。</param>
        /// <param name="isTriageClearRoomInfo">更新为分诊状态时，是否清空诊室信息。</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int CancelIn(
            FS.SOC.HISFC.Assign.Models.Assign assign,
            bool isTriageClearDoctorInfo,
            bool isTriageClearRoomInfo,
            ref string error)
        {
            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            assignMgr.SetTrans(this.Trans);
            queueMgr.SetTrans(this.Trans);

            int rtn = assignMgr.Update(
                FS.HISFC.Models.Nurse.EnuTriageStatus.Triage,
                assign.Register.ID,
                isTriageClearDoctorInfo,
                isTriageClearRoomInfo,
                null, null, null);
            if (rtn == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }
            if (rtn == 0)
            {
                this.RollBack();
                error = "更新分诊状态失败，该分诊信息状态已经改变,请刷新屏幕!流水号" + assign.Register.ID + "  " + assignMgr.Err;
                return -1;
            }

            //更新候诊人数
            if (queueMgr.UpdateWaitingNum(assign.Queue.ID, 1) == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }

            if (InterfaceManager.GetIADTImplement() != null)
            {
                if (InterfaceManager.GetIADTImplement().AssignInfo(assign, false, 1) == -1)
                {
                    this.RollBack();
                    error = "取消进诊失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADTImplement().Err;
                    return -1;
                }
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 自动分诊
        /// </summary>
        /// <param name="this.Trans"></param>
        /// <param name="assign"></param>
        /// <param name="TrigeWhereFlag">分诊标志 1.分到队列  2.分到诊台</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int AutoTriage(FS.FrameWork.Models.NeuObject nurse, DateTime dt, FS.HISFC.Models.Registration.Register reginfo, string TrigeWhereFlag, ref FS.SOC.HISFC.Assign.Models.Assign assign, ref string error)
        {
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            DateTime currentTime = CommonController.CreateInstance().GetSystemTime();

            FS.HISFC.Models.Registration.RegLevel regl = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(reginfo.DoctorInfo.Templet.RegLevel.ID);

            bool isExpert = (regl == null || regl.IsExpert == false) == false;
            /*如果医生为空，那么直接找级别相同的最小队列，如果前者没有，那么就直接找医生和级别都为空的最小队列，如果都没有，返回错误*/
            /*如果医生不为空，先找医生和级别都相同的队列，如果前者没有，那么找医生队列里面的医生相同级别为空的队列，如果前者没有，那么找级别队列里面的医生为空并且级别相同的最小队列，如果前者没有，那么就直接找医生和级别都为空的最小队列，如果都没有，返回错误*/
            FS.SOC.HISFC.Assign.Models.Queue queue = queueMgr.GetQueue(nurse.ID, reginfo.DoctorInfo.Templet.Dept.ID, CommonController.CreateInstance().GetNoonID(dt), reginfo.DoctorInfo.Templet.RegLevel.ID, reginfo.DoctorInfo.Templet.Doct.ID, isExpert);
            if (queue == null)
            {
                error = "获取队列信息失败，原因：" + queueMgr.Err;
                return -1;
            }

            if (string.IsNullOrEmpty(queue.ID))
            {
                error = "自动分诊失败，患者：" + reginfo.Name + " 为找到相对应的队列，请手工分诊";
                return -1;
            }

            //实体转化
            assign.Register = reginfo;
            assign.TriageStatus = TrigeWhereFlag == "1" ? FS.HISFC.Models.Nurse.EnuTriageStatus.Triage : FS.HISFC.Models.Nurse.EnuTriageStatus.In;
            assign.TriageDept = nurse.ID;
            assign.TirageTime = currentTime;
            assign.InTime = currentTime;
            assign.Queue = queue;

            assign.Oper.OperTime = currentTime;
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

            //数据库操作 {7a47cdce-65fc-442d-81fa-f2a597af3e3a}
            if (this.Triage(assign, false, false, false, ref error) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 暂时不看诊
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int NoSee(FS.SOC.HISFC.Assign.Models.Assign assign, ref string error)
        {
            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            assignMgr.SetTrans(this.Trans);
            queueMgr.SetTrans(this.Trans);
            // {d4ea07b4-2559-4473-ac92-f8076d9ce3b4}
            int rtn = assignMgr.Update(FS.HISFC.Models.Nurse.EnuTriageStatus.Delay, assign.Register.ID, false, false, null, null, null);
            if (rtn == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }
            if (rtn == 0)
            {
                this.RollBack();
                error = "更新分诊状态失败，该分诊信息状态已经改变,请刷新屏幕!流水号" + assign.Register.ID + "   " + assignMgr.Err;
                return -1;
            }

            //更新候诊人数
            if (queueMgr.UpdateWaitingNum(assign.Queue.ID, 1) == -1)
            {
                this.RollBack();
                error = assignMgr.Err;
                return -1;
            }

            if (InterfaceManager.GetIADTImplement() != null)
            {
                if (InterfaceManager.GetIADTImplement().AssignInfo(assign, false, 0) < 0)
                {
                    this.RollBack();
                    error = "取消分诊失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADTImplement().Err;
                    return -1;
                }
            }

            this.Commit();
            return 1;
        }

        #endregion

        #region 获取分诊患者信息

        /// <summary>
        /// 获取未看诊患者
        /// </summary>
        /// <param name="nurseID">护士站编码</param>
        /// <param name="today">日期</param>
        /// <param name="noonID">午别</param>
        /// <param name="error">错误信息</param>
        /// <returns>null 错误 其他成功</returns>
        public ArrayList QueryNoSee(string nurseID, DateTime today, string noonID, ref string error)
        {
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            ArrayList alQueryNoSee = assignMgr.QueryNoSee(nurseID, today, noonID);
            error = assignMgr.Err;
            return alQueryNoSee;
        }

        /// <summary>
        /// 获取分诊患者（分诊，进诊，叫号，已看诊）
        /// </summary>
        /// <param name="nurseID">护士站编码</param>
        /// <param name="today">日期</param>
        /// <param name="queueID">队列号</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public ArrayList QueryAssign(string nurseID, DateTime today, string queueID, FS.HISFC.Models.Nurse.EnuTriageStatus status, ref string error)
        {
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            ArrayList alQueryAssign = assignMgr.Query(nurseID, today, queueID, status);
            error = assignMgr.Err;
            return alQueryAssign;
        }

        #endregion

    }
}
