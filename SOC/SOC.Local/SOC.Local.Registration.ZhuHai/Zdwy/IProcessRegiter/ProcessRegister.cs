using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.IProcessRegiter
{
    /// <summary>
    /// 挂号保存时处理接口（事务中）
    /// 用于处理自动分诊
    /// </summary>
    public class ProcessRegister : FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter
    {
        FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();

        FS.HISFC.BizLogic.Manager.Department departmentBizLogic = new FS.HISFC.BizLogic.Manager.Department();

        string whereSQL = @"where trunc(queue_date) = trunc(to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')) --队列日期
                           and dept_code = '{1}' --科室
                           and noon_code = '{2}' --午别
                           and valid_flag = '1'
                           and met_nuo_queue.nurse_cell_code is not null
                           and met_nuo_queue.expert_flag!='1' --非专家号才自动分诊
                           /*and exists (select 1
                                  from fin_opr_schema t
                                 where t.dept_code = met_nuo_queue.dept_code
                                      --and t.doct_code = f.doct_code --这里允许排班到诊室
                                   and t.noon_code = met_nuo_queue.noon_code
                                   and trunc(t.see_date) = trunc(met_nuo_queue.queue_date)
                                   and t.valid_flag = '1'
                                      --and t.reg_lmt-t.reged>0  --这里可以用来限制只有挂号有限额时才分诊
                                   and t.reglevl_code = '{3}')*/
                         order by (select count(d.clinic_code)
                                     from met_nuo_assignrecord d
                                    where d.queue_code = met_nuo_queue.queue_code
                                      and d.assign_flag = '1'),waiting_count
                        ";

        /// <summary>
        /// 当前自动分诊的队列信息
        /// </summary>
        private FS.HISFC.Models.Nurse.Queue currentQueue = null;

        #region IProcessRegiter 成员

        public int SaveBegin(ref FS.HISFC.Models.Registration.Register regObj, ref string errText)
        {
            if (regObj.TranType == FS.HISFC.Models.Base.TransTypes.Positive)
            {
                //在这里在挂号保存前就分配医生的目的是为了挂号票上能够扣挂号限额等
                //*******

                currentQueue = null;

                if (regObj.TranType != FS.HISFC.Models.Base.TransTypes.Positive)
                {
                    return 0;
                }

                #region 自动分配医生

                DateTime dtReg = regObj.DoctorInfo.SeeDate;
                if (dtReg < new DateTime(2000, 1, 1))
                {
                    dtReg = assignMgr.GetDateTimeFromSysDateTime();
                }

                //挂号没有选择医生时自动分配医生或诊室（根据队列）
                if (string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                {
                    string execSQL = string.Format(whereSQL, dtReg.ToString(), regObj.DoctorInfo.Templet.Dept.ID, regObj.DoctorInfo.Templet.Noon.ID, regObj.DoctorInfo.Templet.RegLevel.ID);
                    ArrayList alQueue = queueMgr.QueryBase(execSQL);
                    if (alQueue == null)
                    {
                        errText = "查询自动分诊的队列信息出错！\r\n" + queueMgr.Err;
                        return -1;
                    }
                    else if (alQueue.Count > 0)
                    {
                        currentQueue = alQueue[0] as FS.HISFC.Models.Nurse.Queue;
                        if (currentQueue != null
                            && currentQueue.Doctor != null
                            && !string.IsNullOrEmpty(currentQueue.Doctor.ID))
                        {
                            //中五要求挂号未选择到科室的，就分诊到诊室
                            //regObj.DoctorInfo.Templet.Doct.ID = currentQueue.Doctor.ID;
                        }
                    }


                    //regObj.DoctorInfo.Templet.Doct.ID = this.assignMgr.ExecSqlReturnOne(string.Format(strSQL, dtReg.ToString(), regObj.DoctorInfo.Templet.Dept.ID, regObj.DoctorInfo.Templet.Noon.ID, regObj.DoctorInfo.Templet.RegLevel.ID));
                    //if (regObj.DoctorInfo.Templet.Doct.ID == "-1")
                    //{
                    //    regObj.DoctorInfo.Templet.Doct.ID = "";
                    //}
                }

                if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                {
                    regObj.DoctorInfo.Templet.Doct.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.DoctorInfo.Templet.Doct.ID);
                }

                #endregion
            }

            return 1;
        }

        /// <summary>
        /// 在事物中
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int SaveEnd(ref FS.HISFC.Models.Registration.Register regObj, ref string errText)
        {

            #region 插入分诊

            if (regObj.TranType == FS.HISFC.Models.Base.TransTypes.Positive)
            {
                //说明没有自动分诊到某个队列
                if (currentQueue == null)
                {
                    if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                    {
                        currentQueue = this.queueMgr.GetQueueByDoct(regObj.DoctorInfo.Templet.Doct.ID, regObj.DoctorInfo.SeeDate.Date, regObj.DoctorInfo.Templet.Noon.ID);
                    }
                }

                //没有找到队列，则不自动分诊
                if (currentQueue == null
                    || string.IsNullOrEmpty(currentQueue.ID))
                {
                    return 1;
                }

                #region 自动分诊

                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

                #region 赋值

                DateTime dtNow = assignMgr.GetDateTimeFromSysDateTime();

                //挂号信息
                assign.Register = regObj;

                assign.TriageDept = regObj.DoctorInfo.Templet.Dept.ID;

                //分诊时间按照挂号时间处理，
                assign.TirageTime = regObj.DoctorInfo.SeeDate;
                if (assign.TirageTime < new DateTime(2000, 1, 1))
                {
                    assign.TirageTime = dtNow;
                }

                assign.Oper.OperTime = dtNow;

                assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;

                #region 获取队列信息

                assign.Queue = currentQueue;

                //五院特殊流程，挂号医生不一定取队列里面的医生，而是以挂号时是否选择医生为准
                assign.Queue.Doctor.ID = regObj.DoctorInfo.Templet.Doct.ID;
                if (!string.IsNullOrEmpty(assign.Queue.Doctor.ID))
                {
                    assign.Queue.Doctor.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(assign.Queue.Doctor.ID);
                }

                //if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                //{
                //    assign.Queue.Doctor.ID = regObj.DoctorInfo.Templet.Doct.ID;
                //}

                #endregion

                //设置分诊护士ID
                assign.Queue.AssignDept.ID = regObj.DoctorInfo.Templet.Dept.ID;
                //设置分诊护士Name
                assign.Queue.AssignDept.Name = regObj.DoctorInfo.Templet.Dept.Name;
                //设置看诊日期
                assign.SeeTime = assign.TirageTime.Date;

                #region 获取分诊护士站

                //FS.HISFC.Models.Base.DepartmentStat departmentStat = departmentBizLogic.GetNurseStationFromDeptAndMystatCode(regObj.DoctorInfo.Templet.Dept, "14");

                //string assignNurseID = string.Empty;
                //if (departmentStat != null && departmentStat.PardepCode == "AAAA")
                //{
                //    assignNurseID = departmentStat.DeptCode;
                //}
                //else if (departmentStat != null)
                //{
                //    assignNurseID = departmentStat.PardepCode;
                //}
                //else
                //{
                //    assignNurseID = regObj.DoctorInfo.Templet.Dept.ID;
                //}

                //assign.TriageDept = assignNurseID;

                assign.TriageDept = currentQueue.Dept.ID;

                #endregion


                #region 处理按照诊室排队序号

                string Type = "";
                string Subject = "";

                if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID)
                    && SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(regObj.DoctorInfo.Templet.Doct.ID) != null)
                {
                    Type = "1";//医生
                    Subject = regObj.DoctorInfo.Templet.Doct.ID;
                }
                else if (!string.IsNullOrEmpty(currentQueue.SRoom.ID))
                {
                    Type = "5";//诊室
                    Subject = currentQueue.SRoom.ID;
                }

                //只有诊室队列才重新获取排序号，医生队列，在挂号界面已经获取了，此处重取会跳号
                if (Type == "5")
                {
                    //更新看诊序号
                    if (this.regMgr.UpdateSeeNo(Type, regObj.DoctorInfo.SeeDate, Subject, currentQueue.Noon.ID) == -1)
                    {
                        errText = this.regMgr.Err;
                        return -1;
                    }

                    int seeNo = 0;

                    //获取看诊序号		
                    if (this.regMgr.GetSeeNo(Type, regObj.DoctorInfo.SeeDate, Subject, currentQueue.Noon.ID, ref seeNo) == -1)
                    {
                        errText = this.regMgr.Err;
                        return -1;
                    }

                    string strSQL = @"update fin_opr_register r set r.seeno={0} where r.clinic_code='{1}'";

                    strSQL = string.Format(strSQL, seeNo, regObj.ID);

                    if (assignMgr.ExecNoQuery(strSQL) == -1)
                    {
                        errText = assignMgr.Err;
                        return -1;
                    }
                    regObj.DoctorInfo.SeeNO = seeNo;
                }

                #endregion

                assign.SeeNO = regObj.DoctorInfo.SeeNO;

                #endregion

                if (assignMgr.Insert(assign) == -1)
                {
                    errText = assignMgr.Err;
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
                       assign.Register.ID,
                       assign.SeeNO,
                       assign.Register.PID.CardNO,
                       assign.Register.DoctorInfo.SeeDate.ToString(),
                       assign.Register.Name,
                       assign.Register.Sex.ID,
                       assign.Register.Pact.PayKind.ID,
                       FS.FrameWork.Function.NConvert.ToInt32(assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency),
                       FS.FrameWork.Function.NConvert.ToInt32(assign.Register.RegType),
                       assign.Register.DoctorInfo.Templet.Dept.ID,
                       assign.Register.DoctorInfo.Templet.Dept.Name,
                       assign.Queue.Name,
                       assign.Queue.SRoom.ID,
                       assign.Queue.ID,
                       assign.Queue.SRoom.Name,
                       assign.Queue.Doctor.ID,
                       assign.SeeTime.ToString(),
                       (int)assign.TriageStatus,
                       assign.TriageDept,
                       assign.TirageTime.ToString(),
                       assign.InTime.ToString(),
                       assign.OutTime.ToString(),
                       assign.Oper.ID,
                       assign.Oper.OperTime.ToString(),
                       assign.Queue.Console.ID,
                       assign.Queue.Console.Name,
                       assign.Register.DoctorInfo.Templet.RegLevel.ID,
                       assign.Register.DoctorInfo.Templet.RegLevel.Name,
                       assign.Register.OrderNO.ToString()
                       );

                assignMgr.ExecNoQuery(sql);

                #endregion

                regObj.TriageOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                regObj.IsTriage = true;
                regObj.TriageOper.OperTime = dtNow;

                //3.队列数量增加1
                if (assignMgr.UpdateQueue(assign.Queue.ID, "1") == -1)
                {
                    errText = assignMgr.Err;
                    return -1;
                }

                //if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                //{
                //4、更新挂号信息表，置分诊标志
                if (this.regMgr.Update(regObj.ID, regObj.TriageOper.ID, regObj.TriageOper.OperTime) < 0)
                {
                    errText = regMgr.Err;
                    return -1;
                }
                //}0

                //{993C4984-D7C6-462c-A554-6BA7251E3D4B}
                try
                {
                    if (Queue_Insert(regObj.ID, regObj.Name, assign.SeeNO, assign.Register.DoctorInfo.Templet.Dept.ID, assign.Register.DoctorInfo.Templet.Dept.Name, assign.Queue.Doctor.ID, assign.Queue.Doctor.Name) < 0)
                    {
                        //errText = "排队叫号分诊失败！";
                        //return -1;
                    }
                }
                catch(Exception ex)
                {
 
                }

                #endregion
            }

            #endregion
            
            return 1;
        }

        #endregion

        #region DLL嵌套调用

        /// <summary>
        /// 登录成功，如果诊室屏是开启的，将显示该诊室对应的医生个人信息介绍，具体的信息在后台管理里的医生管理进行
        /// </summary>
        /// <param name="DOCTOR_ID"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Login", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Login(String DOCTOR_ID);

        /// <summary>
        /// 成功会清除当前对应的诊室屏的医生相关个人以及排队信息
        /// </summary>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Logout", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Logout();

        /// <summary>
        /// 医生呼叫功能
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <param name="NEXT_ID"></param>
        /// <param name="NEXT_NAME"></param>
        /// <param name="NEXT_QUEUE_NUM"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallNext", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_CallNext(String CURR_ID, String CURR_NAME, int QUEUE_NUM, String NEXT_ID, String NEXT_NAME, int NEXT_QUEUE_NUM);

        /// <summary>
        /// 医生结诊功能
        /// </summary>
        /// <param name="CURR_ID">Int Queue_CallEndcall(String CURR_ID)</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallEndcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_CallEndcall(String CURR_ID);

        /// <summary>
        /// 过号呼叫标识
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_NoCall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_NoCall(String CURR_ID, String CURR_NAME, int QUEUE_NUM);

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Cancel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Cancel(String CURR_ID);

        /// <summary>
        /// 重新待诊
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Update", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Update(String CURR_ID);

        /// <summary>
        /// 插入分诊信息
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <param name="DEPART_ID">科室编码</param>
        /// <param name="DEPART_NAME">科室名称</param>
        /// <param name="DOCTOR_ID">医生编码</param>
        /// <param name="DOCTOR_NAME">医生名称</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Insert", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Insert(String CURR_ID, String CURR_NAME, int QUEUE_NUM, String DEPART_ID, String DEPART_NAME, String DOCTOR_ID, String DOCTOR_NAME);

        #endregion
    }
}