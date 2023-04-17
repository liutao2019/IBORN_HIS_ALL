using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: SOC诊台综合类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Console : FS.SOC.HISFC.BizProcess.CommonInterface.Common.AbstractBizProcess
    {
        /// <summary>
        /// 保存诊台
        /// </summary>
        /// <param name="room"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int SaveConsole(FS.HISFC.Models.Nurse.Seat console, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, ref string error)
        {
            if (console == null)
            {
                error = "诊台为空！";
                return -1;
            }

            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Console consoleMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
            consoleMgr.SetTrans(this.Trans);
            if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert)
            {
                if (consoleMgr.Insert(console) <= 0)
                {
                    this.RollBack();
                    error = consoleMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update)
            {
                if (consoleMgr.Update(console) <= 0)
                {
                    this.RollBack();
                    error = consoleMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Delete)
            {
                //判断诊室是否被排班
                int result1 = consoleMgr.IsExsitUsedByQueue(console.ID);
                if (result1 < 0)
                {
                    this.RollBack();
                    error = "查询诊台失败" + consoleMgr.Err;

                    return -1;
                }
                else if (result1 >= 1)
                {
                    this.RollBack();
                    error = "在以后的时间里，该诊台在队列维护中已经被维护，不能删除";
                    return -1;
                }

                if (consoleMgr.DeleteByConsole(console.ID) == -1)
                {
                    this.RollBack();
                    error = "删除诊台失败！\n请与系统管理员联系。" + consoleMgr.Err;
                    return -1;
                }
            }

            if (InterfaceManager.GetISaveConsole().SaveCommitting(saveType, console) <= 0)
            {
                this.RollBack();
                error = InterfaceManager.GetISaveConsole().Err;
                return -1;
            }
            this.Commit();

            return 1;
        }

        /// <summary>
        /// 获取诊室下有效诊台
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryOtherValid(string roomid,string consoleid, ref string error)
        {
            FS.SOC.HISFC.Assign.BizLogic.Console conslMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
            ArrayList al = conslMgr.QueryOtherValid(roomid,consoleid);
            error = conslMgr.Err;
            return al;
        }
    }
}
