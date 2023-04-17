using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;

namespace FS.SOC.HISFC.Components.Register.SDFY
{
    public class Function : FS.FrameWork.Management.Database
    {
        public Function()
        {

        }

        #region 医生排班
        /// <summary>
        /// 获取分诊的专家
        /// </summary>
        /// <param name="type">人员类型编码</param>
        /// <param name="deptcode">科室编码</param>
        /// <returns></returns>
        public ArrayList GetEmployeeForArray(FS.HISFC.Models.Base.EnumEmployeeType type, string deptcode, DateTime dt)
        {
            #region 接口说明
            //获得各类型人员列表
            //Manager.Person.GetEmployee.3
            //传入：0 type 人员类型 ,1 dept 科室id
            //传出：人员信息
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("Sdmanager.Person.GetEmployee.ByArray", ref  strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, type, deptcode, dt.ToString());
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
            }
            else
            {
                return null;
            }
            return this.myPersonQuery(strSql);
        }

        /// <summary>
        /// 私有函数，查询人员基本信息
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myPersonQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1) return null;
            try
            {

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.Employee person = new FS.HISFC.Models.Base.Employee();
                    try
                    {
                        person.ID = this.Reader[0].ToString();
                        person.Name = this.Reader[1].ToString();
                        person.SpellCode = this.Reader[2].ToString();
                        person.WBCode = this.Reader[3].ToString();
                        person.Sex.ID = this.Reader[4].ToString();
                        person.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());

                        person.Duty.ID = this.Reader[6].ToString();
                        person.Level.ID = this.Reader[7].ToString();
                        person.GraduateSchool.ID = this.Reader[8].ToString();
                        person.IDCard = this.Reader[9].ToString();
                        person.Dept.ID = this.Reader[10].ToString();
                        person.Nurse.ID = this.Reader[11].ToString();
                        person.EmployeeType.ID = this.Reader[12].ToString();

                        person.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(Reader[13].ToString());
                        person.IsCanModify = FS.FrameWork.Function.NConvert.ToBoolean(Reader[14].ToString());
                        person.IsNoRegCanCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[15].ToString());
                        person.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[16].ToString());
                        person.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());
                        person.UserCode = this.Reader[18].ToString();
                        //{6A8C59DC-91FE-4246-A923-06A011918614}
                        person.Memo = this.Reader[19].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得人员基本信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(person);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得人员基本信息出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 查询某日、某科室出诊专家
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataSet QueryDoctSD(DateTime seeDate, DateTime endDate, string deptID, DateTime seeDateEnd, string noon)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (this.Sql.GetSql("Sdmanager.Person.GetEmployee.ByArray.1", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString(), deptID, seeDateEnd.Date.ToString(), noon);
            }
            catch (Exception e)
            {
                this.Err = "[Sdmanager.Person.GetEmployee.ByArray.1]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql, ref ds) == -1) return null;

            return ds;
        }

        #region {3F42A388-0810-4387-A749-44A79EEE0247}
        /// <summary>
        /// 查询某日全部出诊专家
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet QueryDoct(DateTime seeDate, DateTime endDate)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (this.Sql.GetSql("SDLocal.Registration.Schema.Query.8", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[SDLocal.Registration.Schema.Query.8]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql, ref ds) == -1) return null;

            return ds;
        }
        #endregion

        /// <summary>
        /// 查询某日、某些科室出诊专家
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryDoctByMultiDeptID(DateTime seeDate, DateTime endDate, string deptID)
        {
            string sql = "";
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Registration.Schema schema;

            if (this.Sql.GetSql("SDLocal.Registration.Schema.Query.9", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString(), deptID);
            }
            catch (Exception e)
            {
                this.Err = "[SDLocal.Registration.Schema.Query.9]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询医生排班信息出错!";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    schema = new FS.HISFC.Models.Registration.Schema();
                    schema.Templet.Dept.ID = this.Reader[0].ToString();
                    schema.Templet.Dept.Name = this.Reader[1].ToString();
                    schema.Templet.Doct.ID = this.Reader[2].ToString();
                    schema.Templet.Doct.Name = this.Reader[3].ToString();
                    schema.Templet.Noon.ID = this.Reader[4].ToString();
                    schema.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    schema.Templet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                    schema.Templet.Doct.User01 = this.Reader[7].ToString();
                    schema.Templet.Doct.User02 = this.Reader[8].ToString();
                    schema.Templet.Memo = this.Reader[9].ToString();
                    schema.Templet.Doct.Memo = this.Reader[10].ToString();
                    al.Add(schema);
                }
            }
            catch (Exception e)
            {
                this.Err = "[SDLocal.Registration.Schema.Query.9]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }
        #endregion

        #region 挂号管理
        /// <summary>
        /// 查询患者一段时间内挂的有效号
        /// {FF783D04-EBFE-477b-9603-4B6554B452AA}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList Query(string cardNo, DateTime beginDate, DateTime endDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetSql("SDLocal.Registration.Register.Query.3", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, beginDate.ToString(), endDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[SDLocal.Registration.Register.Query.3]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 挂号查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList QueryRegister(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();
            FS.HISFC.Models.Registration.Register reg = null;

            try
            {
                while (this.Reader.Read())
                {
                    reg = new FS.HISFC.Models.Registration.Register();

                    reg.ID = this.Reader[0].ToString();//序号
                    reg.PID.CardNO = this.Reader[1].ToString();//病历号
                    reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());//挂号日期
                    reg.DoctorInfo.Templet.Noon.ID = this.Reader[3].ToString();
                    reg.Name = this.Reader[4].ToString();
                    reg.IDCard = this.Reader[5].ToString();
                    reg.Sex.ID = this.Reader[6].ToString();

                    reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());//出生日期

                    reg.Pact.PayKind.ID = this.Reader[8].ToString();//结算类别
                    reg.Pact.PayKind.Name = this.Reader[9].ToString();

                    reg.Pact.ID = this.Reader[10].ToString();//合同单位
                    reg.Pact.Name = this.Reader[11].ToString();
                    reg.SSN = this.Reader[12].ToString();

                    reg.DoctorInfo.Templet.RegLevel.ID = this.Reader[13].ToString();//挂号级别
                    reg.DoctorInfo.Templet.RegLevel.Name = this.Reader[14].ToString();

                    reg.DoctorInfo.Templet.Dept.ID = this.Reader[15].ToString();//挂号科室
                    reg.DoctorInfo.Templet.Dept.Name = this.Reader[16].ToString();

                    reg.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());

                    reg.DoctorInfo.Templet.Doct.ID = this.Reader[18].ToString();//看诊医生
                    reg.DoctorInfo.Templet.Doct.Name = this.Reader[19].ToString();

                    reg.RegType = (FS.HISFC.Models.Base.EnumRegType)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[20].ToString());
                    reg.IsFirst = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[21].ToString());

                    reg.RegLvlFee.RegFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());
                    reg.RegLvlFee.ChkFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());
                    reg.RegLvlFee.OwnDigFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString());
                    reg.RegLvlFee.OthFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25].ToString());

                    reg.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());
                    reg.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());
                    reg.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[28].ToString());

                    reg.Status = (FS.HISFC.Models.Base.EnumRegisterStatus)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());

                    reg.InputOper.ID = this.Reader[30].ToString();
                    reg.IsSee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[31].ToString());
                    reg.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[32].ToString());
                    reg.TranType = (FS.HISFC.Models.Base.TransTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString());
                    reg.BalanceOperStat.IsCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[34]);//日结
                    reg.BalanceOperStat.CheckNO = this.Reader[35].ToString();
                    reg.BalanceOperStat.Oper.ID = this.Reader[36].ToString();

                    if (!this.Reader.IsDBNull(37))
                        reg.BalanceOperStat.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[37].ToString());

                    reg.PhoneHome = this.Reader[38].ToString();//联系电话
                    reg.AddressHome = this.Reader[39].ToString();//地址
                    reg.IsFee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[40].ToString());
                    //作废人信息
                    reg.CancelOper.ID = this.Reader[41].ToString();
                    reg.CancelOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());
                    reg.CardType.ID = this.Reader[43].ToString();//证件类型
                    reg.DoctorInfo.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[44].ToString());
                    reg.DoctorInfo.Templet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[45].ToString());
                    //reg.InvoiceNo = this.Reader[50].ToString() ;
                    //reg.InvoiceNO = this.Reader[51].ToString() ; by niuxinyuan
                    reg.InvoiceNO = this.Reader[50].ToString();
                    reg.RecipeNO = this.Reader[51].ToString();

                    reg.DoctorInfo.Templet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[52].ToString());
                    reg.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[53].ToString());
                    reg.DoctorInfo.Templet.ID = this.Reader[54].ToString();
                    reg.InSource.ID = this.Reader[55].ToString();
                    reg.PVisit.InState.ID = this.Reader[56].ToString();
                    reg.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString());
                    reg.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[58].ToString());
                    reg.PVisit.ZG.ID = this.Reader[59].ToString();
                    reg.PVisit.PatientLocation.Bed.ID = this.Reader[60].ToString();

                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    //标识是否是账户流程挂号 1代表是
                    reg.IsAccount = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[61].ToString());

                    //{E26C3EE9-D480-421e-9FD3-7094D8E4E1D0}
                    reg.SeeDoct.Dept.ID = this.Reader[62].ToString(); //看诊科室
                    reg.SeeDoct.ID = this.Reader[63].ToString();//看诊医生
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    reg.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[64].ToString());
                    //{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
                    reg.Mark1 = this.Reader[65].ToString();

                    al.Add(reg);
                }
            }
            catch (Exception e)
            {
                this.Err = "检索挂号信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }
        #endregion

        #region 分诊
        /// <summary>
        /// 根据队列取未用的预留分诊序号
        /// </summary>
        /// <param name="queueID">分诊队列ID</param>
        /// <returns></returns>
        public ArrayList GetBookNumByQueue(string queueID)
        {
            ArrayList alReturn = new ArrayList();

            FS.HISFC.Models.Nurse.Queue queue = this.GetQueueByID(queueID);

            if (!string.IsNullOrEmpty(queue.Memo))
            {
                int iMax = this.QueryMaxSeq(queueID);

                ArrayList alBookNum = new ArrayList();

                for (int i = 0; i < iMax; i++)
                {
                    if (queue.Memo.Contains(i.ToString().Substring(i.ToString().Length - 1)))
                    {
                        alBookNum.Add(i.ToString());
                    }

                }

                string seqlist = string.Empty;

                for (int i = 0; i < alBookNum.Count; i++)
                {
                    if (i < alBookNum.Count - 1)
                    {
                        seqlist = seqlist + alBookNum[i].ToString() + ",";
                    }
                    else
                    {
                        seqlist = seqlist + alBookNum[i].ToString();
                    }
                }

                ArrayList alUsedSeq = this.QuerySeqByQueueID(queueID, seqlist);

                string usedSeq = string.Empty;

                if (alUsedSeq != null && alUsedSeq.Count > 0)
                {
                    for (int i = 0; i < alUsedSeq.Count; i++)
                    {
                        usedSeq = usedSeq + alUsedSeq[i].ToString() + "|";
                    }
                }

                for (int i = 0; i < alBookNum.Count; i++)
                {
                    if (!usedSeq.Contains(alBookNum[i].ToString()))
                    {
                        alReturn.Add(alBookNum[i].ToString());
                    }
                }

            }

            return alReturn;
        }

        /// <summary>
        /// 根据医生编码，队列日期，午别取未用的预留分诊序号
        /// </summary>
        /// <param name="doctID">医生ID</param>
        /// <param name="queDate">队列日期</param>
        /// <param name="noonID">午别ID</param>
        /// <returns></returns>
        public ArrayList GetBookNumForRegistration(string doctID, DateTime queDate, string noonID)
        {
            ArrayList alReturn = new ArrayList();

            FS.HISFC.Models.Nurse.Queue queue = this.GetQueueByDoct(doctID, queDate.Date, noonID);

            if (queue != null)
            {
                alReturn = this.GetBookNumByQueue(queue.ID);
            }

            return alReturn;
        }

        /// <summary>
        /// 按照ID取分诊队列
        /// </summary>
        /// <param name="queueID">分诊队列ID</param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Queue GetQueueByID(string queueID)
        {
            string sql = "", where = "";

            FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

            if (this.Sql.GetSql("Nurse.Queue.Query.1", ref sql) == -1)
            {
                this.Err = "查询分诊队列信息出错![Nurse.Queue.Query.1]";
                return null;
            }

            if (this.Sql.GetSql("Nurse.Queue.Query.7", ref where) == -1)
            {
                this.Err = "查询分诊队列信息出错![Nurse.Queue.Query.7]";
                return null;
            }

            try
            {
                where = string.Format(where, queueID);
            }
            catch (Exception e)
            {
                this.Err = "查询分诊队列信息出错![Nurse.Queue.Query.1+7]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList alQueue = this.Query(sql);

            if (alQueue != null && alQueue.Count > 0)
            {
                queue = alQueue[0] as FS.HISFC.Models.Nurse.Queue;
            }

            return queue;
        }

        /// <summary>
        /// 根据医生编码、队列日期、午别取分诊队列
        /// </summary>
        /// <param name="doctID">医生ID</param>
        /// <param name="queDate">队列日期</param>
        /// <param name="noonID">午别ID</param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Queue GetQueueByDoct(string doctID, DateTime queDate, string noonID)
        {
            string sql = "", where = "";

            FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

            if (this.Sql.GetSql("Nurse.Queue.Query.1", ref sql) == -1)
            {
                this.Err = "查询分诊队列信息出错![Nurse.Queue.Query.1]";
                return null;
            }

            if (this.Sql.GetSql("SD.Nurse.Queue.QueueByDoct.1", ref where) == -1)
            {
                this.Err = "查询分诊队列信息出错![Nurse.Queue.QueueByDoct.1]";
                return null;
            }

            try
            {
                where = string.Format(where, doctID, queDate.ToString(), noonID);
            }
            catch (Exception e)
            {
                this.Err = "查询分诊队列信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList alQueue = this.Query(sql);

            if (alQueue != null && alQueue.Count > 0)
            {
                queue = alQueue[0] as FS.HISFC.Models.Nurse.Queue;
            }

            return queue;
        }

        /// <summary>
        /// 查询队列当前最大看诊数,队列只要赋值:ID
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public int QueryMaxSeq(string queueID)
        {
            string sql = "";

            if (this.Sql.GetSql("Nurse.Assign.Query.3", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.3]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.3]";
                return -1;
            }

            try
            {
                sql = string.Format(sql, queueID);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            string rtn = this.ExecSqlReturnOne(sql, "0");

            if (rtn == "") rtn = "0";

            return FS.FrameWork.Function.NConvert.ToInt32(rtn);
        }

        /// <summary>
        /// 根据sql查询队列信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList Query(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "基本sql出错!" + sql;
                this.ErrCode = "基本sql出错!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

                    //所属护士站
                    queue.Dept.ID = this.Reader[0].ToString();
                    //队列代码
                    queue.ID = this.Reader[1].ToString();
                    //队列名称
                    queue.Name = this.Reader[2].ToString();
                    //午别代码
                    queue.Noon.ID = this.Reader[3].ToString();

                    //队列类型[2007/03/27]
                    queue.User01 = this.Reader[4].ToString();

                    //显示顺序
                    if (!this.Reader.IsDBNull(5))
                        queue.Order = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
                    //是否有效
                    queue.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
                    //备注
                    queue.Memo = this.Reader[7].ToString();
                    //操作员
                    queue.Oper.ID = this.Reader[8].ToString();
                    //操作时间
                    queue.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
                    //队列日期
                    queue.QueueDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
                    //看诊医生
                    queue.Doctor.ID = this.Reader[11].ToString();
                    //诊室
                    queue.SRoom.ID = this.Reader[12].ToString();
                    queue.SRoom.Name = this.Reader[13].ToString();
                    //诊台
                    queue.Console.ID = this.Reader[14].ToString();
                    queue.Console.Name = this.Reader[15].ToString();
                    //专家标志
                    queue.ExpertFlag = this.Reader[16].ToString();
                    //分诊科室
                    queue.AssignDept.ID = this.Reader[17].ToString();
                    queue.AssignDept.Name = this.Reader[18].ToString();
                    queue.WaitingCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19]);

                    al.Add(queue);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询门诊护士站分诊队列信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// 根据分诊队列ID、看诊序号查询序号列表
        /// </summary>
        /// <param name="queueID"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private ArrayList QuerySeqByQueueID(string queueID, string seq)
        {
            string sql = "";

            if (this.Sql.GetSql("SD.Nurse.Assign.QuerySeqByQueueID.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SD.Nurse.Assign.QuerySeqByQueueID.1]";
                this.ErrCode = "查询sql出错,索引为[SD.Nurse.Assign.QuerySeqByQueueID.1]";
                return null;
            }

            try
            {
                sql = string.Format(sql, queueID, seq);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            ArrayList al = new ArrayList();

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "基本sql出错!" + sql;
                this.ErrCode = "基本sql出错!" + sql;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    al.Add(this.Reader[0].ToString());
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询门诊护士站分诊队列信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }
        #endregion

        #region 精伦-身份证读取接口
        //首先，声明通用接口
        [DllImport("sdtapi.dll")]
        public static extern int SDT_OpenPort(int iPortID);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ClosePort(int iPortID);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_PowerManagerBegin(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_AddSAMUser(int iPortID, string pcUserName, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SAMLogin(int iPortID, string pcUserName, string pcPasswd, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SAMLogout(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_UserManagerOK(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ChangeOwnPwd(int iPortID, string pcOldPasswd, string pcNewPasswd, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ChangeOtherPwd(int iPortID, string pcUserName, string pcNewPasswd, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_DeleteSAMUser(int iPortID, string pcUserName, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_StartFindIDCard(int iPortID, ref int pucIIN, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SelectIDCard(int iPortID, ref int pucSN, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadBaseMsg(int iPortID, string pucCHMsg, ref int puiCHMsgLen, string pucPHMsg, ref int puiPHMsgLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadBaseMsgToFile(int iPortID, string fileName1, ref int puiCHMsgLen, string fileName2, ref int puiPHMsgLen, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_WriteAppMsg(int iPortID, ref byte pucSendData, int uiSendLen, ref byte pucRecvData, ref int puiRecvLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_WriteAppMsgOK(int iPortID, ref byte pucData, int uiLen, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_CancelWriteAppMsg(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadNewAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadAllAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_UsableAppMsg(int iPortID, ref byte ucByte, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_GetUnlockMsg(int iPortID, ref byte strMsg, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_GetSAMID(int iPortID, ref byte StrSAMID, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_SetMaxRFByte(int iPortID, byte ucByte, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ResetSAM(int iPortID, int iIfOpen);

        [DllImport("WltRS.dll")]
        public static extern int GetBmp(string file_name, int intf);

        private static int CurPort = 0;
        private static int EdziIfOpen = 1;
        public int GetIDCardInfo(ref string IDCode, ref string Name, ref string Sex, ref DateTime BirthDay, ref string Nation, ref string Adress, ref string Agency, ref DateTime ExpireStart, ref DateTime ExpireEnd, ref string Message)
        {
            bool bUsbPort = false;
            int intOpenPortRtn = 0;
            int rtnTemp = 0;
            int pucIIN = 0;
            int pucSN = 0;
            int puiCHMsgLen = 0;
            int puiPHMsgLen = 0;


            //检测usb口的机具连接，必须先检测usb
            for (int iPort = 1001; iPort <= 1016; iPort++)
            {
                intOpenPortRtn = SDT_OpenPort(iPort);
                if (intOpenPortRtn == 144)
                {
                    CurPort = iPort;
                    bUsbPort = true;
                    break;
                }
            }
            //检测串口的机具连接
            if (!bUsbPort)
            {
                for (int iPort = 1; iPort <= 2; iPort++)
                {
                    intOpenPortRtn = SDT_OpenPort(iPort);
                    if (intOpenPortRtn == 144)
                    {
                        CurPort = iPort;
                        bUsbPort = false;
                        break;
                    }
                }
            }
            if (intOpenPortRtn != 144)
            {
                Message = "端口打开失败，请检测相应的端口或者重新连接读卡器！";
                return -1;
            }
            //在这里，如果您想下一次不用再耗费检查端口的检查的过程，您可以把 EdziPortID 保存下来，可以保存在注册表中，也可以保存在配置文件中，我就不多写了，但是，
            //您要考虑机具连接端口被用户改变的情况哦

            //下面找卡
            rtnTemp = SDT_StartFindIDCard(CurPort, ref pucIIN, EdziIfOpen);
            if (rtnTemp != 159)
            {
                rtnTemp = SDT_StartFindIDCard(CurPort, ref pucIIN, EdziIfOpen);  //再找卡
                if (rtnTemp != 159)
                {
                    rtnTemp = SDT_ClosePort(CurPort);
                    Message = "未放卡或者卡未放好，请重新放卡！";
                    return -1;
                }
            }
            //选卡
            rtnTemp = SDT_SelectIDCard(CurPort, ref pucSN, EdziIfOpen);
            if (rtnTemp != 144)
            {
                rtnTemp = SDT_SelectIDCard(CurPort, ref pucSN, EdziIfOpen);  //再选卡
                if (rtnTemp != 144)
                {
                    rtnTemp = SDT_ClosePort(CurPort);
                    Message = "读卡失败！";
                    return -1;
                }
            }
            //注意，在这里，用户必须有应用程序当前目录的读写权限
            FileInfo objFile = new FileInfo("wz.txt");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }
            //objFile = new FileInfo("zp.bmp");
            //if (objFile.Exists)
            //{
            //    objFile.Attributes = FileAttributes.Normal;
            //    objFile.Delete();
            //}
            objFile = new FileInfo("zp.wlt");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }
            rtnTemp = SDT_ReadBaseMsgToFile(CurPort, "wz.txt", ref puiCHMsgLen, "zp.wlt", ref puiPHMsgLen, EdziIfOpen);

            if (rtnTemp != 144)
            {
                rtnTemp = SDT_ClosePort(CurPort);
                Message = "读卡失败！";
                return -1;
            }
            FileInfo f = new FileInfo("wz.txt");
            FileStream fs = f.OpenRead();
            byte[] bt = new byte[fs.Length];
            fs.Read(bt, 0, (int)fs.Length);
            fs.Close();

            string str = System.Text.UnicodeEncoding.Unicode.GetString(bt);

            Name = System.Text.UnicodeEncoding.Unicode.GetString(bt, 0, 30).Trim();
            Sex = System.Text.UnicodeEncoding.Unicode.GetString(bt, 30, 2).Trim();
            Nation = System.Text.UnicodeEncoding.Unicode.GetString(bt, 32, 4).Trim();
            string strBird = System.Text.UnicodeEncoding.Unicode.GetString(bt, 36, 16).Trim();
            BirthDay = Convert.ToDateTime(strBird.Substring(0, 4) + "年" + strBird.Substring(4, 2) + "月" + strBird.Substring(6) + "日");
            Adress = System.Text.UnicodeEncoding.Unicode.GetString(bt, 52, 70).Trim();
            IDCode = System.Text.UnicodeEncoding.Unicode.GetString(bt, 122, 36).Trim();
            Agency = System.Text.UnicodeEncoding.Unicode.GetString(bt, 158, 30).Trim();
            string strTem = System.Text.UnicodeEncoding.Unicode.GetString(bt, 188, bt.GetLength(0) - 188).Trim();
            ExpireStart = Convert.ToDateTime(strTem.Substring(0, 4) + "年" + strTem.Substring(4, 2) + "月" + strTem.Substring(6, 2) + "日");
            strTem = strTem.Substring(8);
            if (strTem.Trim() != "长期")
            {
                ExpireEnd = Convert.ToDateTime(strTem.Substring(0, 4) + "年" + strTem.Substring(4, 2) + "月" + strTem.Substring(6, 2) + "日");
            }
            else
            {
                ExpireEnd = DateTime.MaxValue;
            }

            return 1;
        }
        #endregion
    }
}
