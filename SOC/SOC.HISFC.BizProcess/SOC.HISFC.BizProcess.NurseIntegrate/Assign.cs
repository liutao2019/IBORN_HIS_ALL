using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.NurseIntegrate
{
    /// <summary>
    /// 分诊业务
    /// </summary>
    public class Assign : FS.HISFC.BizProcess.Integrate.IntegrateBase
    {
        FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();

        FS.HISFC.BizLogic.Registration.Register register = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 按诊区、诊室、分诊状态查询进诊患者
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="roomID"></param>
        /// <param name="assignFlag"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignByAssignFlag(string deptID, string roomID, string assignFlag)
        {
            this.SetDB(assignMgr);
            return assignMgr.QuerySimpleAssignByAssignFlag(deptID, roomID, assignFlag);
        }

        /// <summary>
        /// 根据状态查询分诊患者
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">截至时间</param>
        /// <param name="consoleID">诊台代码</param>
        /// <param name="state">状态 1.进诊患者   2.已诊患者</param>
        /// <returns>ArrayList (分诊实体数组)</returns>
        public ArrayList QuerySimpleAssignPatientByState(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID, string deptCode)
        {
            this.SetDB(assignMgr);
            return assignMgr.QuerySimpleAssignPatientByState(beginTime, endTime, consoleID, state, doctID, deptCode);
        }


        /// <summary>
        /// 根查医生查询分诊患者
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="state"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignPatientByDoctID(DateTime beginTime, DateTime endTime, String state, string doctID, string deptID)
        {
            this.SetDB(assignMgr);
            return assignMgr.QuerySimpleAssignPatientByDoctID(beginTime, endTime, state, doctID, deptID);
        }

        /// <summary>
        /// 根据科室查询患者
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="state"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignPatientByDeptID(DateTime beginTime, DateTime endTime, String state, string deptID)
        {
            this.SetDB(assignMgr);
            return assignMgr.QuerySimpleAssignPatientByDeptID(beginTime, endTime, state, deptID);
        }

        /// <summary>
        /// 更新挂号表中的分诊标记
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="operID"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateRegistrationAssign(string clinicID, string operID, DateTime operDate)
        {
            this.SetDB(assignMgr);
            return register.Update(clinicID, operID, operDate);
        }

    }
}
