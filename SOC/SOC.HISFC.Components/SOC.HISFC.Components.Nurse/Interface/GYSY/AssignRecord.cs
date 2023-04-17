using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Nurse;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.Components.Nurse.Interface.GYSY
{
    class AssignRecord : FS.HISFC.BizProcess.Integrate.IntegrateBase, FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT
    {
        FS.SOC.HISFC.Assign.BizLogic.Assign assignBizLogic = new FS.SOC.HISFC.Assign.BizLogic.Assign();

        FS.SOC.HISFC.Assign.BizProcess.Assign assignBizProcess = new FS.SOC.HISFC.Assign.BizProcess.Assign();

        FS.HISFC.BizLogic.Manager.Department departmentBizLogic = new FS.HISFC.BizLogic.Manager.Department();

        #region IADT 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;
        public string Err
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }

        public int AssignInfo(FS.HISFC.Models.Nurse.Assign assign, bool positive, int state)
        {
            return 1;
        }

        public int Balance(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool positive)
        {
            return 1;
        }

        public int PatientInfo(FS.HISFC.Models.RADT.Patient patient, object patientInfo)
        {
            return 1;
        }

        public int Prepay(FS.HISFC.Models.RADT.PatientInfo patient, System.Collections.ArrayList alprepay, string flag)
        {
            return 1;
        }

        public int QueryBookingNumber(System.Collections.ArrayList alSchema)
        {
            return 1;
        }

        /// <summary>
        /// 挂号的时候插入分诊系统
        /// </summary>
        /// <param name="register"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        public int Register(object register, bool positive)
        {
            if (!positive)
                return 0;


            this.SetDB(assignBizLogic);

            FS.HISFC.Models.Registration.Register reg = register as FS.HISFC.Models.Registration.Register;
            FS.SOC.HISFC.Assign.Models.Assign assign = new FS.SOC.HISFC.Assign.Models.Assign();
            
            //挂号信息
            assign.Register = reg;
            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            assign.TriageDept = reg.DoctorInfo.Templet.Dept.ID;
            assign.TirageTime = this.assignBizLogic.GetDateTimeFromSysDateTime();
            assign.Oper.OperTime = this.assignBizLogic.GetDateTimeFromSysDateTime();
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            assign.Queue.AssignDept = assign.Register.DoctorInfo.Templet.Dept;
            //设置分诊护士ID
            assign.Queue.AssignNurse.ID  = reg.DoctorInfo.Templet.Dept.ID;
            //设置分诊护士Name
            assign.Queue.AssignNurse.Name = reg.DoctorInfo.Templet.Dept.Name;
            //设置看诊日期
            assign.SeeTime = this.assignBizLogic.GetDateTimeFromSysDateTime().Date;
            DepartmentStat departmentStat = departmentBizLogic.GetNurseStationFromDeptAndMystatCode(reg.DoctorInfo.Templet.Dept,"14");
            string assignNurseID = string.Empty;
            if (departmentStat!=null&&departmentStat.PardepCode == "AAAA")
            {
                 assignNurseID = departmentStat.DeptCode;
            }
            else if (departmentStat != null)
            {
                assignNurseID = departmentStat.PardepCode;
            }
            else {
                assignNurseID = reg.DoctorInfo.Templet.Dept.ID;
            }
            if (!string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID))
            {
                assign.Queue.Doctor.ID=reg.DoctorInfo.Templet.Doct.ID;
            }
            assign.TriageDept = assignNurseID;
            string seeNO = string.Empty;
            //if (this.assignBizProcess.GetNurseSeeNOForReg(reg.DoctorInfo.Templet.RegLevel.IsExpert, string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID) ? "2" : "1", !string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID) ? assign.Register.DoctorInfo.Templet.Doct.ID : assignNurseID, ref seeNO, out errInfo) < 0)
            //{

            //    return -1;
            //}
          //  assign.SeeNO = seeNO;
            assign.SeeNO = reg.DoctorInfo.SeeNO.ToString();


            return assignBizLogic.Insert(assign);
        }

        #endregion
    }
}
