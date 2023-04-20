using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Registration.GuangZhou.Zdly.IProcessRegiter
{
    /// <summary>
    /// 挂号保存时处理接口（事务中）
    /// 用于处理自动分诊
    /// </summary>
    class ProcessRegister : FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter
    {
        FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();

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
        /// 挂号没有选择医生时，根据候诊队列人数自动分配一个医生
        /// </summary>
        string strSQL = @"select doct_code from (select f.doct_code from met_nuo_queue f
                       where trunc(f.queue_date)=trunc(to_date('{0}','yyyy-mm-dd hh24:mi:ss'))--队列日期
                        and f.dept_code='{1}'--科室
                        and f.noon_code='{2}'--午别
                        and f.valid_flag='1'
                        and exists (select 1 from fin_opr_schema t
                        where t.dept_code=f.dept_code
                        and t.doct_code=f.doct_code
                        and t.noon_code=f.noon_code
                        and trunc(t.see_date)=trunc(f.queue_date)
                        and t.valid_flag='1'
                        and t.reglevl_code='{3}')
                        order by (select count(d.clinic_code) from met_nuo_assignrecord d
                        where d.queue_code=f.queue_code
                        and d.assign_flag='1'
                        and d.doct_code=f.doct_code
                        and (select sum(reg_fee) from fin_opr_register  aa where aa.clinic_code=d.clinic_code and aa.ynsee='0')>0),
                        (select count(d.clinic_code)
                        from met_nuo_assignrecord d
                        where d.queue_code = f.queue_code
                        and d.doct_code = f.doct_code
                        and (select sum(reg_fee)
                        from fin_opr_register aa
                        where aa.clinic_code = d.clinic_code
                        and aa.ynsee = '0') > 0))
                        where rownum=1";

        /// <summary>
        /// 获取排班信息
        /// </summary>
        string strSQL_scam = @"select doct_code from 
                            (select t.doct_code,t.doct_name,t.dept_code, t.reg_lmt+t.tel_lmt-t.reged-t.tel_reged from fin_opr_schema t where t.reglevl_code='{3}' 
                            and t.doct_type='1'
                            and trunc(t.see_date)=trunc(to_date('{0}','yyyy-MM-dd HH24:mi:ss'))
                            and t.noon_code='{1}'
                            and t.valid_flag=fun_get_valid()
                            and t.dept_code='{2}'
                            order by t.reg_lmt+t.tel_lmt-t.reged-t.tel_reged
                            )tt
                            where rownum=1";


        /// <summary>
        /// 队列信息
        /// </summary>
        FS.HISFC.Models.Nurse.Queue queue = null;

        /// <summary>
        /// 挂号的时候插入分诊系统
        /// </summary>
        /// <param name="register"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        public int Register(object register, bool positive)
        {
            //切记切记！！
            //因为存在预约的情况，所以所有的分诊，都按照挂号时间来处理！！！


            if (!positive)
            {
                return 0;
            }

            FS.HISFC.Models.Registration.Register reg = register as FS.HISFC.Models.Registration.Register;


            //门诊口腔科不分诊，专家号除外
            if (reg.DoctorInfo.Templet.Dept.ID == "3009"
                || (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(reg.DoctorInfo.Templet.Dept.ID).Name.Contains("口腔")
                && SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(reg.DoctorInfo.Templet.Dept.ID).DeptType.ID.ToString() == "C")
                )
            {
                //专家号还是自动分诊
                if (SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(reg.DoctorInfo.Templet.RegLevel.ID).IsExpert)
                {
                }
                else
                {
                    if (reg.DoctorInfo.SeeDate.DayOfWeek != DayOfWeek.Saturday && reg.DoctorInfo.SeeDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        return 1;
                    }
                }
            }

            FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

            DateTime dtNow = assignMgr.GetDateTimeFromSysDateTime();

            queue = new FS.HISFC.Models.Nurse.Queue();

            //挂号信息
            assign.Register = reg;


            assign.TriageDept = reg.DoctorInfo.Templet.Dept.ID;

            //分诊时间按照挂号时间处理，
            assign.TirageTime = reg.DoctorInfo.SeeDate;
            if (assign.TirageTime < new DateTime(2000, 1, 1))
            {
                assign.TirageTime = dtNow;
            }

            assign.Oper.OperTime = dtNow;


            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

            #region 自动分配医生

            //挂号没有选择医生时自动分配医生
            if (string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID)

                //周末不自动分配
                && ((assign.TirageTime.DayOfWeek != DayOfWeek.Saturday
                && assign.TirageTime.DayOfWeek != DayOfWeek.Sunday)||
                dictionaryDate.ContainsKey(assign.TirageTime.Date)
                    )

                //体检号 不分诊
                && (reg.DoctorInfo.Templet.RegLevel.ID != "11"
                && !reg.DoctorInfo.Templet.RegLevel.Name.Contains("体检"))

                //全科为本院体检号 不分诊
                && (reg.DoctorInfo.Templet.RegLevel.ID != "07"
                && !reg.DoctorInfo.Templet.RegLevel.Name.Contains("全科")
                )

                //口腔科不分诊
                //&& assign.TriageDept != "3009"

                //节假日不分诊
                //&& !regMgr.IsEmergency(((FS.HISFC.Models.Base.Employee)assignMgr.Operator).Dept.ID)
                && !regMgr.IsEmergencyHolidays(((FS.HISFC.Models.Base.Employee)assignMgr.Operator).Dept.ID, dtNow)
                )
            {
                reg.DoctorInfo.Templet.Doct.ID = this.assignMgr.ExecSqlReturnOne(string.Format(strSQL, assign.TirageTime.ToString(), reg.DoctorInfo.Templet.Dept.ID, reg.DoctorInfo.Templet.Noon.ID, reg.DoctorInfo.Templet.RegLevel.ID));
                if (reg.DoctorInfo.Templet.Doct.ID == "-1")
                {
                    reg.DoctorInfo.Templet.Doct.ID = "";
                }
            }
            #endregion


            //专科号打印一个医生名字
            if (string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID)
                && SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(reg.DoctorInfo.Templet.RegLevel.ID).Name.Contains("专科"))
            {
                reg.DoctorInfo.Templet.Doct.ID = this.assignMgr.ExecSqlReturnOne(string.Format(strSQL_scam, assign.TirageTime.ToString(), reg.DoctorInfo.Templet.Noon.ID, reg.DoctorInfo.Templet.Dept.ID, reg.DoctorInfo.Templet.RegLevel.ID));
                if (reg.DoctorInfo.Templet.Doct.ID == "-1")
                {
                    reg.DoctorInfo.Templet.Doct.ID = "";
                }
            }
         
            return 1;
        }

        #endregion

        Dictionary<DateTime, FS.FrameWork.Models.NeuObject> dictionaryDate = new Dictionary<DateTime, FS.FrameWork.Models.NeuObject>();

        #region IProcessRegiter 成员

        public int SaveBegin(ref FS.HISFC.Models.Registration.Register regObj, ref string errText)
        {
            if (dictionaryDate.Count == 0)
            {
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList al = constantMgr.GetList("NonHolidays");
                if (al != null)
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        dictionaryDate[FS.FrameWork.Function.NConvert.ToDateTime(obj.Name)] = obj;
                    }
                }
            }
            if (this.Register(regObj, regObj.TranType == FS.HISFC.Models.Base.TransTypes.Positive) == -1)
            {
                errText = errInfo;
                return -1;
            }
            return 1;
        }

        public int SaveEnd(ref FS.HISFC.Models.Registration.Register regObj, ref string errText)
        {
            if (regObj.TranType == FS.HISFC.Models.Base.TransTypes.Positive)
            {

                FS.HISFC.Models.Registration.Register reg = regObj as FS.HISFC.Models.Registration.Register;

                //门诊口腔科不分诊，专家号除外
                if (reg.DoctorInfo.Templet.Dept.ID == "3009"
                    || (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(reg.DoctorInfo.Templet.Dept.ID).Name.Contains("口腔")
                    && SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(reg.DoctorInfo.Templet.Dept.ID).DeptType.ID.ToString() == "C")
                    )
                {
                    //专家号还是自动分诊
                    if (SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(reg.DoctorInfo.Templet.RegLevel.ID).IsExpert)
                    {

                    }
                    else
                    {
                        if (reg.DoctorInfo.SeeDate.DayOfWeek != DayOfWeek.Saturday && reg.DoctorInfo.SeeDate.DayOfWeek != DayOfWeek.Sunday)
                        {
                            return 1;
                        }
                    }
                }

                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

                DateTime dtNow = assignMgr.GetDateTimeFromSysDateTime();

                queue = new FS.HISFC.Models.Nurse.Queue();

                //挂号信息
                assign.Register = reg;


                assign.TriageDept = reg.DoctorInfo.Templet.Dept.ID;

                //分诊时间按照挂号时间处理，
                assign.TirageTime = reg.DoctorInfo.SeeDate;
                if (assign.TirageTime < new DateTime(2000, 1, 1))
                {
                    assign.TirageTime = dtNow;
                }

                assign.Oper.OperTime = dtNow;


                assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

                #region 自动分配医生

                if (!string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID))
                {
                    queue = this.queueMgr.GetQueueByDoct(reg.DoctorInfo.Templet.Doct.ID, reg.DoctorInfo.SeeDate.Date, reg.DoctorInfo.Templet.Noon.ID);
                    if (queue == null)
                    {
                        errInfo = queueMgr.Err;
                        return -1;
                    }
                }
               
                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                #endregion

                assign.Queue = queue;

                if (!string.IsNullOrEmpty(reg.DoctorInfo.Templet.Doct.ID))
                {
                    assign.Queue.Doctor.ID = reg.DoctorInfo.Templet.Doct.ID;
                }


                if (
                    (string.IsNullOrEmpty(queue.ID) || string.IsNullOrEmpty(assign.Queue.Doctor.ID) ) 
                     && assign.TirageTime.DayOfWeek != DayOfWeek.Saturday 
                     && assign.TirageTime.DayOfWeek != DayOfWeek.Sunday
                    && !regMgr.IsEmergencyHolidays(((FS.HISFC.Models.Base.Employee)assignMgr.Operator).Dept.ID, dtNow)
                   )
                {
                 
                    return 1;
                }

                if (dictionaryDate.ContainsKey(assign.TirageTime.Date) == false)
                {
                    if (assign.TirageTime.DayOfWeek == DayOfWeek.Saturday || assign.TirageTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        assign.Queue = new FS.HISFC.Models.Nurse.Queue();
                    }
                }

                //设置分诊护士ID
                assign.Queue.AssignDept.ID = reg.DoctorInfo.Templet.Dept.ID;
                //设置分诊护士Name
                assign.Queue.AssignDept.Name = reg.DoctorInfo.Templet.Dept.Name;
                //设置看诊日期
                assign.SeeTime = assign.TirageTime.Date;

                FS.HISFC.Models.Base.DepartmentStat departmentStat = departmentBizLogic.GetNurseStationFromDeptAndMystatCode(reg.DoctorInfo.Templet.Dept, "14");

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
                assign.Register = reg;


                if (assignMgr.Insert(assign) == -1)
                {
                    this.errInfo = assignMgr.Err;
                    return -1;
                }

                #region 监视问题，先插入临时表

                string sql = @"INSERT INTO met_nuo_assignrecord_temp   --护士分诊记录表
              ( 
                clinic_code,   --门诊号
                see_sequence,   --看诊序号
                card_no,   --病历号
                reg_date,   --挂号日期
            name,   --患者姓名
            sex_code,   --性别
            paykind_code,   --结算类别
            ynurg,   --1急诊/0普通
            ynbook,   --1预约/0普通
            dept_code,   --看诊科室
            dept_name,   --科室名称
            queue_name,   --队列名称
            room_id,   --出诊诊室
            queue_code,   --队列代码
            room_name,   --诊室名称
            doct_code,   --看诊医生
            see_date,   --看诊时间
            assign_flag,   --1分诊/2进诊/3诊出
            nurse_cell_code,   --分诊科室
            triage_date,   --分诊时间
            in_date,   --进诊时间
            out_date,   --出诊时间
            oper_code,   --操作员
            oper_date,  --操作时间
            console_code,--诊台代码
            console_name,--诊台名称
            reglvl_code,-- 挂号级别代码
            reglvl_name,--挂号级别
            order_no --每日顺序号
            )
     VALUES 
          (  
            '{0}',   --门诊号
            '{1}',   --看诊序号
            '{2}',   --病历号
            to_date('{3}','yyyy-mm-dd HH24:mi:ss'),   --挂号日期
            '{4}',   --患者姓名
            '{5}',   --性别
            '{6}',   --结算类别
            '{7}',   --1急诊/0普通
            '{8}',   --1预约/0普通
            '{9}',   --看诊科室
            '{10}',   --科室名称
            '{11}',   --队列名称
            '{12}',   --出诊诊室
            '{13}',   --队列代码
            '{14}',   --诊室名称
            '{15}',   --看诊医生
            to_date('{16}','yyyy-mm-dd HH24:mi:ss'),   --看诊时间
            '{17}',   --1分诊/2进诊/3诊出
            '{18}',   --分诊科室
            to_date('{19}','yyyy-mm-dd HH24:mi:ss'),   --分诊时间
            to_date('{20}','yyyy-mm-dd HH24:mi:ss'),   --进诊时间
            to_date('{21}','yyyy-mm-dd HH24:mi:ss'),   --进诊时间
            '{22}',   --操作员
            to_date('{23}','yyyy-mm-dd HH24:mi:ss'),  --操作时间
            '{24}', --诊台代码
            '{25}', --诊台名称
            '{26}',--挂号级别
            '{27}',--挂号级别名称
            '{28}' --每日顺序号
        )";

                sql = string.Format(sql,
                       assign.Register.ID, assign.SeeNO,
                       assign.Register.PID.CardNO, assign.Register.DoctorInfo.SeeDate.ToString()
                       , assign.Register.Name, assign.Register.Sex.ID, assign.Register.Pact.PayKind.ID,
                       FS.FrameWork.Function.NConvert.ToInt32(assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency)
                       , FS.FrameWork.Function.NConvert.ToInt32(assign.Register.RegType),
                       assign.Register.DoctorInfo.Templet.Dept.ID, assign.Register.DoctorInfo.Templet.Dept.Name,
                       assign.Queue.Name, assign.Queue.SRoom.ID, assign.Queue.ID,
                       assign.Queue.SRoom.Name, assign.Queue.Doctor.ID, assign.SeeTime.ToString(),
                       (int)assign.TriageStatus, assign.TriageDept, assign.TirageTime.ToString(),
                       assign.InTime.ToString(), assign.OutTime.ToString(),
                       assign.Oper.ID, assign.Oper.OperTime.ToString(), assign.Queue.Console.ID,
                       assign.Queue.Console.Name, assign.Register.DoctorInfo.Templet.RegLevel.ID,
                       assign.Register.DoctorInfo.Templet.RegLevel.Name, assign.Register.OrderNO.ToString()
                       );

                assignMgr.ExecNoQuery(sql);

                #endregion

                //3、更新挂号信息表，置分诊标志
                //if (regMgr.Update(reg.ID, FS.FrameWork.Management.Connection.Operator.ID, dtNow) == -1)
                //{
                //    this.errInfo = regMgr.Err;
                //    return -1;
                //}
                //因为这个接口是放到插入挂号记录之前的，所以置状态即可
                reg.TriageOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                reg.IsTriage = true;
                reg.TriageOper.OperTime = dtNow;

                //4.队列数量增加1
                if (assignMgr.UpdateQueue(assign.Queue.ID, "1") == -1)
                {
                    this.errInfo = assignMgr.Err;
                    return -1;
                }

                //if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                //{
                if (this.regMgr.Update(regObj.ID, regObj.TriageOper.ID, regObj.TriageOper.OperTime) < 0)
                {
                    errText = regMgr.Err;
                    return -1;
                }
                //}
            }
            return 1;
        }

        #endregion
    }
}