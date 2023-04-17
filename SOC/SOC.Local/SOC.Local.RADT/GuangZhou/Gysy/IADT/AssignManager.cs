using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.GuangZhou.Gysy.IADT
{
    class AssignManager
    {
        public int InsertAssignRecord(FS.HISFC.Models.Registration.Register register, ref string errInfo)
        {
            FS.SOC.HISFC.Assign.BizLogic.Assign assignBizLogic = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.HISFC.BizLogic.Manager.Department departmentBizLogic = new FS.HISFC.BizLogic.Manager.Department();

            if (register.DoctorInfo.Templet.RegLevel.IsEmergency)
            {
                return 1;
            }

            DateTime dtNow = assignBizLogic.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Registration.Register reg = register as FS.HISFC.Models.Registration.Register;
            FS.SOC.HISFC.Assign.Models.Assign assign = new FS.SOC.HISFC.Assign.Models.Assign();

            //挂号信息
            assign.Register = reg;
            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            assign.TriageDept = reg.DoctorInfo.Templet.Dept.ID;
            assign.TirageTime = dtNow;
            assign.Oper.OperTime = dtNow;
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            assign.Queue.AssignDept = assign.Register.DoctorInfo.Templet.Dept;
            //设置分诊护士ID
            assign.Queue.AssignNurse.ID = reg.DoctorInfo.Templet.Dept.ID;
            //设置分诊护士Name
            assign.Queue.AssignNurse.Name = reg.DoctorInfo.Templet.Dept.Name;
            //设置看诊日期
            assign.SeeTime = dtNow.Date;
            DepartmentStat departmentStat = departmentBizLogic.GetNurseStationFromDeptAndMystatCode(reg.DoctorInfo.Templet.Dept, "14");
            string assignNurseID = string.Empty;
            if (departmentStat != null && departmentStat.PardepCode == "AAAA")
            {
                assignNurseID = departmentStat.DeptCode;
            }
            else if (departmentStat != null)
            {
                assignNurseID = departmentStat.PardepCode;
            }
            else
            {
                assignNurseID = reg.DoctorInfo.Templet.Dept.ID;
            }
            if (!string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID))
            {
                assign.Queue.Doctor.ID = reg.DoctorInfo.Templet.Doct.ID;
            }
            assign.TriageDept = assignNurseID;
            string seeNO = string.Empty;
            //if (this.assignBizProcess.GetNurseSeeNOForReg(reg.DoctorInfo.Templet.RegLevel.IsExpert, string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID) ? "2" : "1", !string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID) ? assign.Register.DoctorInfo.Templet.Doct.ID : assignNurseID, ref seeNO, out errInfo) < 0)
            //{

            //    return -1;
            //}
            //  assign.SeeNO = seeNO;
            assign.SeeNO = reg.DoctorInfo.SeeNO.ToString();
            int ret = assignBizLogic.Insert(assign);
            errInfo = assignBizLogic.Err;
            return ret;
        }
    }
}
