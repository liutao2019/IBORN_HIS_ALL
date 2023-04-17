using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Nurse;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.Components.Nurse.Interface.ZDLY
{
    /// <summary>
    /// 中大六院自动分诊
    /// </summary>
    class AssignRecord : FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT
    {
        FS.SOC.HISFC.Assign.BizLogic.Assign assignBizLogic = new FS.SOC.HISFC.Assign.BizLogic.Assign();
        FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        FS.HISFC.BizLogic.Manager.Department departmentBizLogic = new FS.HISFC.BizLogic.Manager.Department();

        FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();

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
        /// 挂号没有选择医生时，根据候诊队列人数自动分配一个医生
        /// </summary>
        string strSQL = @"select f.doct_code from met_nuo_queue f
                       where trunc(f.queue_date)=trunc(to_date('{0}','yyyy-mm-dd hh24:mi:ss'))--队列日期
                        and f.dept_code='{1}'--科室
                        and f.noon_code='{2}'--午别
                        and f.valid_flag='1'
                        and exists (select 1 from fin_opr_schema t
                        where t.dept_code=f.dept_code
                        and t.doct_code=f.doct_code
                        and t.noon_code=f.noon_code
                        and trunc(t.see_date)=trunc(f.queue_date)
                        and t.reglevl_code='{3}')";



        /// <summary>
        /// 挂号的时候插入分诊系统
        /// </summary>
        /// <param name="register"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        public int Register(object register, bool positive)
        {
            if (!positive)
            {
                return 0;
            }

            FS.HISFC.Models.Registration.Register reg = register as FS.HISFC.Models.Registration.Register;

            FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

            DateTime dtNow = assignBizLogic.GetDateTimeFromSysDateTime();

            //挂号信息
            assign.Register = reg;

            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;

            assign.TriageDept = reg.DoctorInfo.Templet.Dept.ID;

            assign.TirageTime = dtNow;

            assign.Oper.OperTime = dtNow;

            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

            //挂号没有选择医生时自动分配医生
            if (string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID)

                //周末不自动分配
                && dtNow.DayOfWeek != DayOfWeek.Saturday
                && dtNow.DayOfWeek != DayOfWeek.Sunday)
            {
                reg.DoctorInfo.Templet.Doct.ID = this.assignBizLogic.ExecSqlReturnOne(string.Format(strSQL, dtNow.ToString(), reg.DoctorInfo.Templet.Dept.ID, reg.DoctorInfo.Templet.Noon.ID, reg.DoctorInfo.Templet.RegLevel.ID));
                if (reg.DoctorInfo.Templet.Doct.ID == "-1")
                {
                    reg.DoctorInfo.Templet.Doct.ID = "";
                }
            }

            FS.HISFC.Models.Nurse.Queue queue = this.queueMgr.GetQueueByDoct(reg.DoctorInfo.Templet.Doct.ID, dtNow.Date, reg.DoctorInfo.Templet.Noon.ID);

            assign.Queue = queue;

            if (!string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID))
            {
                assign.Queue.Doctor.ID = reg.DoctorInfo.Templet.Doct.ID;
            }

            assign.Queue.AssignDept = assign.Register.DoctorInfo.Templet.Dept;
            //设置分诊护士ID
            assign.Queue.Dept.ID = reg.DoctorInfo.Templet.Dept.ID;
            //设置分诊护士Name
            assign.Queue.Dept.Name = reg.DoctorInfo.Templet.Dept.Name;
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

            assign.TriageDept = assignNurseID;

            assign.SeeNO = reg.DoctorInfo.SeeNO;


            if (this.assignMgr.Insert(assign) == -1)
            {
                this.errInfo = assignBizLogic.Err;
                return -1;
            }

            //3、更新挂号信息表，置分诊标志
            if (regMgr.Update(reg.ID, FS.FrameWork.Management.Connection.Operator.ID, dtNow) == -1)
            {
                this.errInfo = regMgr.Err;
                return -1;
            }
            reg.TriageOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            reg.IsTriage = true;

            //4.队列数量增加1
            if (assignMgr.UpdateQueue(assign.Queue.ID, "1") == -1)
            {
                this.errInfo = assignMgr.Err;
                return -1;
            }
            return 1;
        }

        #endregion
    }
}
