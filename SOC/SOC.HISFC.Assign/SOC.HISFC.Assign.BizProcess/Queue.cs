using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: SOC队列综合类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Queue : FS.SOC.HISFC.BizProcess.CommonInterface.Common.AbstractBizProcess
    {
        /// <summary>
        /// 保存队列
        /// </summary>
        /// <param name="queueTemplate"></param>
        /// <param name="saveType"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public  int SaveQueue(FS.SOC.HISFC.Assign.Models.Queue queue, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, ref string error)
        {
            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            queueMgr.SetTrans(this.Trans);

            //删除模板
            int result = -1;
            switch (saveType)
            { 
                case FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert:
                case FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update:
                    result = queueMgr.IsExistDoctor(queue);
                    if (result < 0)
                    {
                        this.RollBack();
                        error = "判断医生是否重复失败，原因：" + queueMgr.Err;
                        return -1;
                    }
                    else if (result >= 1)
                    {
                        this.RollBack();
                        error = "医生已存在，不需要重复保存";
                        return 0;
                    }

                    result = queueMgr.IsExistRoomAndConsole(queue);
                    if (result < 0)
                    {
                        this.RollBack();
                        error = "判断诊室诊台是否重复失败，原因：" + queueMgr.Err;
                        return -1;
                    }
                    else if (result >= 1)
                    {
                        this.RollBack();
                        error = "诊室诊台已存在，不需要重复保存";
                        return 0;
                    }

                    result = queueMgr.IsExistQueueName(queue);
                    if (result < 0)
                    {
                        this.RollBack();
                        error = "判断队列名称是否重复失败，原因：" + queueMgr.Err;
                        return -1;
                    }
                    else if (result >= 1)
                    {
                        this.RollBack();
                        error = "队列名称已存在，请核对后保存";
                        return 0;
                    }

                    if (queue.QueueType == FS.SOC.HISFC.Assign.Models.EnumQueueType.RegLevel)
                    {
                        result = queueMgr.IsExistRegLevel(queue);
                        if (result < 0)
                        {
                            this.RollBack();
                            error = "判断挂号级别是否重复失败，原因：" + queueMgr.Err;
                            return -1;
                        }
                        else if (result >= 1)
                        {
                            this.RollBack();
                            error = "挂号级别已存在，请核对后保存";
                            return 0;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert)
            {
                result = queueMgr.Insert(queue);
                if (result < 0)
                {
                    this.RollBack();
                    error = "插入队列失败，原因：" + queueMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update)
            {
                result = queueMgr.IsExistHavePatient(queue);
                if (result < 0)
                {
                    this.RollBack();
                    error = "判断队列是否已存在患者失败，原因：" + queueMgr.Err;
                    return -1;
                }
                else if (result >= 1)
                {
                    //不允许修改医生，级别，诊室，诊台，午别，日期，科室，护士站等，只允许修改顺序号，备注
                    FS.SOC.HISFC.Assign.Models.Queue temp = queueMgr.GetQueue(queue.ID);
                    if (temp == null||string.IsNullOrEmpty(temp.ID))
                    {

                        this.RollBack();
                        error = "获取原始队列失败，原因："+queueMgr.Err;
                        return -1;
                    }

                    if (temp.AssignNurse.ID.Equals(queue.AssignNurse.ID) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改护士站！";
                        return 0;
                    }

                    if (temp.AssignDept.ID.Equals(queue.AssignDept.ID) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改科室！";
                        return 0;
                    }

                    if (temp.Doctor.ID.Equals("") == false && temp.Doctor.ID.Equals(queue.Doctor.ID) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改医生！";
                        return 0;
                    }

                    if (temp.RegLevel.ID.Equals("") == false && temp.RegLevel.ID.Equals(queue.RegLevel.ID) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改级别！";
                        return 0;
                    }

                    if (temp.SRoom.ID.Equals("") == false && temp.SRoom.ID.Equals(queue.SRoom.ID) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改诊室！";
                        return 0;
                    }

                    if (temp.Console.ID.Equals("") == false && temp.Console.ID.Equals(queue.Console.ID) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改诊台！";
                        return 0;
                    }

                    if (temp.Noon.ID.Equals("") == false && temp.Noon.ID.Equals(queue.Noon.ID) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改午别！";
                        return 0;
                    }

                    if (temp.QueueDate.Date.Equals(queue.QueueDate.Date) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改日期！";
                        return 0;
                    }

                    if (temp.IsExpert.Equals(queue.IsExpert) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改专家队列！";
                        return 0;
                    }

                    if (temp.QueueType.Equals(queue.QueueType) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改队列类型！";
                        return 0;
                    }

                    if (temp.IsValid.Equals(queue.IsValid) == false)
                    {
                        this.RollBack();
                        error = "队列已存在患者，不允许修改有效性！";
                        return 0;
                    }
                }

                result = queueMgr.Update(queue);
                if (result < 0)
                {
                    this.RollBack();
                    error = "更新队列失败，原因：" + queueMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Delete)
            {
                result = queueMgr.IsExistHavePatient(queue);
                if (result < 0)
                {
                    this.RollBack();
                    error = "判断队列是否已存在患者失败，原因：" + queueMgr.Err;
                    return -1;
                }
                else if (result >= 1)
                {
                    this.RollBack();
                    error = "队列已存在患者，不允许删除！";
                    return 0;
                }

                //删除模板
                result = queueMgr.Delete(queue.ID);
                if (result < 0)
                {
                    this.RollBack();
                    error = "删除队列失败，原因：" + queueMgr.Err;
                    return -1;
                }
            }

            this.Commit();

            return 1;
        }

        /// <summary>
        /// 获取队列信息
        /// </summary>
        /// <returns></returns>
        public  ArrayList QueryQueue(string nurseID, DateTime date, string deptID,out string error)
        {
            error = "";
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            ArrayList alQueue = null;
            string noonID = CommonController.CreateInstance().GetNoonID(date);
            if (nurseID.Equals(deptID))
            {
                alQueue = queueMgr.QueryValidByNurseID(nurseID, date.Date, noonID);
            }
            else
            {
                alQueue = queueMgr.QueryValidByNurseID(nurseID, date.Date, noonID, deptID);
            }
            error = queueMgr.Err;
            return alQueue;
        }

        /// <summary>
        /// 获取队列数据信息(人数信息)
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryQueueData(string nurseID, DateTime date, string deptID, out string error)
        {
            error = "";
            FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            ArrayList alQueue = null;
            if (nurseID.Equals(deptID))
            {
                alQueue = queueMgr.QueryNurseQueueNum(nurseID, date.Date);
            }
            else
            {
                alQueue = queueMgr.QueryNurseQueueNum(nurseID, date.Date, deptID);
            }
            error = queueMgr.Err;
            return alQueue;
        }

    }
}
