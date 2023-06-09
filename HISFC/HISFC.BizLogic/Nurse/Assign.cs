using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Nurse
{
    /// <summary>
    /// 分诊管理类
    /// </summary>
    public class Assign : FS.FrameWork.Management.Database
    {
        public Assign()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        protected FS.HISFC.Models.Nurse.Assign assign = null;

        //protected ArrayList al = null;

        #region 基础函数

        /// <summary>
        /// 查询分诊信息
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryAssignBase(string whereSql, params object[] args)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql(whereSql, ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[" + whereSql + "]";
                this.ErrCode = "查询sql出错,索引为[" + whereSql + "]";
                return null;
            }

            try
            {
                where = string.Format(where, args);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 分诊信息基本查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList Query(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询分诊信息出错!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    #region 赋值
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    //所属护士站代码
                    assign.Queue.Dept.ID = this.Reader[0].ToString();

                    //门诊号
                    this.assign.Register.ID = this.Reader[2].ToString();

                    //看诊序号
                    this.assign.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    this.assign.Register.DoctorInfo.SeeNO = this.assign.SeeNO;

                    //病历号
                    this.assign.Register.PID.CardNO = this.Reader[4].ToString();
                    this.assign.Register.Card.ID = this.assign.Register.PID.CardNO;
                    //挂号日期
                    this.assign.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    //患者姓名
                    this.assign.Register.Name = this.Reader[6].ToString();
                    //性别
                    this.assign.Register.Sex.ID = this.Reader[7].ToString();
                    this.assign.Register.Sex.ID = this.assign.Register.Sex.ID;
                    //结算类别
                    this.assign.Register.Pact.PayKind.ID = this.Reader[8].ToString();
                    //是否急诊
                    this.assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    //是否预约
                    this.assign.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]));
                    //看诊科室
                    this.assign.Queue.Dept.ID = this.Reader[11].ToString();
                    this.assign.Queue.Dept.Name = this.Reader[12].ToString();

                    this.assign.Register.DoctorInfo.Templet.Dept = this.assign.Queue.Dept.Clone();
                    //看诊诊室
                    this.assign.Queue.SRoom.ID = this.Reader[14].ToString();
                    this.assign.Queue.SRoom.Name = this.Reader[16].ToString();
                    //分诊队列
                    this.assign.Queue.ID = this.Reader[15].ToString();
                    this.assign.Queue.Name = this.Reader[13].ToString();
                    //看诊医生
                    this.assign.Queue.Doctor.ID = this.Reader[17].ToString();

                    this.assign.Register.DoctorInfo.Templet.Doct = this.assign.Queue.Doctor.Clone();

                    //看诊时间
                    this.assign.SeeTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    //分诊状态
                    this.assign.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                                                    FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19].ToString());

                    //分诊科室
                    this.assign.TriageDept = this.Reader[20].ToString();
                    //分诊时间
                    this.assign.TirageTime = this.Reader.GetDateTime(21);
                    //进诊时间
                    if (!this.Reader.IsDBNull(22))
                        this.assign.InTime = this.Reader.GetDateTime(22);
                    //出诊时间
                    if (!this.Reader.IsDBNull(23))
                        this.assign.OutTime = this.Reader.GetDateTime(23);
                    //操作员
                    this.assign.Oper.ID = this.Reader[24].ToString();
                    this.assign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
                    //诊台信息
                    this.assign.Queue.Console.ID = this.Reader[26].ToString();
                    this.assign.Queue.Console.Name = this.Reader[27].ToString();

                    if (Reader.FieldCount > 28)
                    {
                        assign.Register.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28]);
                    }
                    #endregion

                    al.Add(this.assign);
                }
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询分诊信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return al;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 根据状态查询最后一个进诊患者
        /// </summary>
        /// <param name="nuerseID">午别</param>
        /// <param name="today"></param>
        /// <param name="queueID">分诊队列ID</param>
        /// <param name="status">分诊状态</param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryLastAssignPatient(string nuerseID, DateTime today, string queueID, string status)
        {
            ArrayList al = this.QueryAssignBase("Nurse.Assign.Query.LastAssign", nuerseID, today, queueID, FS.FrameWork.Function.NConvert.ToInt32(status));
            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.Nurse.Assign;
            }
            return null;
        }

        /// <summary>
        /// 叫号后更新叫号状态
        /// </summary>
        /// <param name="consoleCode">诊台号码</param>
        /// <param name="clinicID">门诊号</param>
        /// <param name="outDate">诊出日期</param>
        /// <param name="doctID">医生代码</param>
        /// <returns></returns>
        public int UpdateByCalled(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            string sql = "";
            //{A614F96F-CF88-4ac8-81F1-FE21B7B08E59}
            if (this.Sql.GetCommonSql("Nurse.Assign.Update.ByCalled", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, outDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assign.Update.ByCalled]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 按时间、队列、分诊科室查询分诊信息(还要求是有效号)
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="queueID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime today, string queueID,
            FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.2", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.2]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.2]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, today.ToString(), queueID, (int)status);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 批量查询 按时间、队列、分诊科室查询分诊信息(还要求是有效号)
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="queueID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList BatchQuery(string nurseID, DateTime today, string queueID, params FS.HISFC.Models.Nurse.EnuTriageStatus[] status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.BatchQuery", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.BatchQuery]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.BatchQuery]";
                return null;
            }

            try
            {
                string strStatus = "''";
                for (int i = 0; i < status.Length; i++)
                {
                    strStatus = strStatus + ",'" + ((Int32)status[i]).ToString() + "'";
                }

                where = string.Format(where, nurseID, today.ToString(), queueID, strStatus);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 按时间、诊室、分诊科室查询进诊信息
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime today, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.4", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.4]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.4]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, today.ToString(), (int)status);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 按时间、诊室、分诊科室查询进诊信息
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryUnionRegister(string nurseID, DateTime today, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.QuerySecond.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.QuerySecond.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.QuerySecond.1]";
                return null;
            }


            try
            {
                sql = string.Format(sql, nurseID, today.ToString(), (int)status);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }


            return this.QuerySecond(sql);
        }

        /// <summary>
        /// 查询队列当前最大看诊数,队列只要赋值:ID
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public int Query(FS.FrameWork.Models.NeuObject queue)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.3", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.3]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.3]";
                return -1;
            }

            try
            {
                sql = string.Format(sql, queue.ID);
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
        /// 按看诊诊室查询进诊患者
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public ArrayList Query(string deptID, string roomID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.5", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.5]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.5]";
                return null;
            }

            FS.HISFC.BizLogic.Nurse.Dept dept = new Dept();

            string nurseID = dept.GetNurseByDeptID(deptID);

            try
            {
                where = string.Format(where, nurseID, roomID);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 按看诊诊台查询进诊患者
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public ArrayList QueryByConsole(string deptID, string consoleID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.7", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.7]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.7]";
                return null;
            }

            FS.HISFC.BizLogic.Nurse.Dept dept = new Dept();

            string nurseID = dept.GetNurseByDeptID(deptID);

            try
            {
                where = string.Format(where, nurseID, consoleID);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 查询已看诊患者
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public ArrayList Query(DateTime begin, DateTime end, string doctID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.6", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.6]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.6]";
                return null;
            }

            try
            {
                where = string.Format(where, begin.ToString(), end.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueCode"></param>
        /// <returns></returns>
        public ArrayList QueryByQueueCode(string queueCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.9", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.9]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.9]";
                return null;
            }

            try
            {
                where = string.Format(where, queueCode);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 查找已经分诊但且已经过了午别的分诊记录
        /// </summary>
        /// <param name="strFromDate">起始时间</param>
        /// <param name="strToDate">结束时间</param>
        /// <param name="nurseID">队列代码</param>
        /// <param name="noonID">午别代码</param>
        /// <returns></returns>
        public ArrayList QueryUnInSee(string strFromDate, string strToDate, string nurseID, string noonID)
        {

            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.QueryUnInSee.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.QueryUnInSee.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.QueryUnInSee.1]";
                return null;
            }
            try
            {
                sql = string.Format(sql, strFromDate, strToDate, nurseID, noonID);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询分诊信息出错!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    #region 赋值
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    this.assign.Register.ID = this.Reader[0].ToString();//门诊号
                    this.assign.Queue.ID = this.Reader[1].ToString();//队列ID

                    #endregion

                    al.Add(this.assign);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询分诊信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;

        }

        protected ArrayList QuerySecond(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询分诊信息出错!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    #region 赋值
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    //门诊号
                    this.assign.Register.ID = this.Reader[2].ToString();

                    //看诊序号

                    this.assign.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    this.assign.Register.DoctorInfo.SeeNO = this.assign.SeeNO;

                    //病历号
                    this.assign.Register.PID.CardNO = this.Reader[4].ToString();
                    this.assign.Register.Card.ID = this.assign.Register.PID.CardNO;
                    //挂号日期
                    this.assign.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    //患者姓名
                    this.assign.Register.Name = this.Reader[6].ToString();
                    //性别
                    this.assign.Register.Sex.ID = this.Reader[7].ToString();
                    this.assign.Register.Sex.ID = this.assign.Register.Sex.ID;
                    //结算类别
                    this.assign.Register.Pact.PayKind.ID = this.Reader[8].ToString();
                    //是否急诊
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    //this.assign.Register.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    this.assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    //是否预约
                    this.assign.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]));
                    //看诊科室
                    this.assign.Queue.Dept.ID = this.Reader[11].ToString();
                    this.assign.Queue.Dept.Name = this.Reader[12].ToString();

                    this.assign.Register.DoctorInfo.Templet.Dept = this.assign.Queue.Dept.Clone();
                    //看诊诊室
                    this.assign.Queue.SRoom.ID = this.Reader[14].ToString();
                    this.assign.Queue.SRoom.Name = this.Reader[16].ToString();
                    //分诊队列
                    this.assign.Queue.ID = this.Reader[15].ToString();
                    this.assign.Queue.Name = this.Reader[13].ToString();

                    //看诊医生
                    this.assign.Queue.Doctor.ID = this.Reader[17].ToString();

                    this.assign.Register.DoctorInfo.Templet.Doct = this.assign.Queue.Doctor.Clone();

                    //看诊时间
                    this.assign.SeeTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    //分诊状态
                    this.assign.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                                                    FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19].ToString());

                    //分诊科室
                    this.assign.TriageDept = this.Reader[20].ToString();
                    //分诊时间
                    this.assign.TirageTime = this.Reader.GetDateTime(21);
                    //进诊时间
                    if (!this.Reader.IsDBNull(22))
                        this.assign.InTime = this.Reader.GetDateTime(22);
                    //出诊时间
                    if (!this.Reader.IsDBNull(23))
                        this.assign.OutTime = this.Reader.GetDateTime(23);
                    //操作员
                    this.assign.Oper.ID = this.Reader[24].ToString();
                    this.assign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
                    //诊台信息
                    this.assign.Queue.Console.ID = this.Reader[26].ToString();
                    this.assign.Queue.Console.Name = this.Reader[27].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.ID = this.Reader[28].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.Name = this.Reader[29].ToString();
                    this.assign.Register.DoctorInfo.Templet.Doct.Name = this.Reader[30].ToString();
                    #endregion

                    al.Add(this.assign);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询分诊信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// 根据病历号查询该患者今天的最后一次分诊信息。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign Query(DateTime dt, string cardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.8", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.8]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.8]";
                return null;
            }

            try
            {
                where = string.Format(where, cardNo, dt);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = (FS.HISFC.Models.Nurse.Assign)al[0];
            return info;
        }

        /// <summary>
        /// 根据科室获取所属护理站代码
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string QueryNurseByDept(string deptCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Dept.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Dept.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Dept.Query.1]";
                return "";
            }
            sql = string.Format(sql, deptCode);

            return this.Sql.ExecSqlReturnOne(sql);
        }

        /// <summary>
        /// 根据门诊流水号，分诊标志获取一个唯一分诊信息
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryByClinicID(string clinicCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.10", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.7]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.7]";
                return null;
            }

            try
            {
                where = string.Format(where, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            return (FS.HISFC.Models.Nurse.Assign)al[0];
        }

        /// <summary>
        /// 根据状态查询分诊患者
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">截至时间</param>
        /// <param name="consoleID">诊台代码</param>
        /// <param name="state">状态 1.进诊患者   2.已诊患者</param>
        /// <returns>ArrayList (分诊实体数组)</returns>
        public ArrayList QueryAssignPatientByState(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.Query.ByState", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.ByState]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.ByState]";
                return null;
            }

            try
            {
                //state = "(" + state + ")";
                sql = string.Format(sql, beginTime, endTime, consoleID, state);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryAssReg(sql);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">截至时间</param>
        /// <param name="consoleID">诊台代码</param>
        /// <param name="state">状态 1.进诊患者   2.已诊患者</param>
        /// <returns>ArrayList (分诊实体数组)</returns>
        public ArrayList QueryPatient(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID)
        {
            string sql = "";
            //进诊患者
            if (state == "1")
            {
                if (this.Sql.GetCommonSql("Nurse.Assign.Query.11", ref sql) == -1)
                {
                    this.Err = "查询sql出错,索引为[Nurse.Assign.Query.11]";
                    this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.11]";
                    return null;
                }

                try
                {
                    sql = string.Format(sql, beginTime, endTime, consoleID);
                }
                catch (Exception e)
                {
                    this.Err = "字符转换出错!" + e.Message;
                    this.ErrCode = e.Message;
                    return null;
                }
            }
            else if (state == "2")
            {
                if (this.Sql.GetCommonSql("Nurse.Assign.Query.13", ref sql) == -1)
                {
                    this.Err = "查询sql出错,索引为[Nurse.Assign.Query.13]";
                    this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.13]";
                    return null;
                }

                try
                {
                    sql = string.Format(sql, beginTime, endTime, doctID);
                }
                catch (Exception e)
                {
                    this.Err = "字符转换出错!" + e.Message;
                    this.ErrCode = e.Message;
                    return null;
                }
            }

            return this.QueryAssReg(sql);
        }

        /// <summary>
        /// 给门诊医生用的去挂号和分诊的集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList QueryAssReg(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询分诊信息出错!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    #region 赋值
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    //门诊号
                    this.assign.Register.ID = this.Reader[2].ToString();

                    //看诊序号

                    this.assign.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    this.assign.Register.DoctorInfo.SeeNO = this.assign.SeeNO;

                    //病历号
                    this.assign.Register.PID.CardNO = this.Reader[4].ToString();
                    this.assign.Register.Card.ID = this.assign.Register.PID.CardNO;
                    //挂号日期
                    this.assign.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    //患者姓名
                    this.assign.Register.Name = this.Reader[6].ToString();
                    //性别
                    this.assign.Register.Sex.ID = this.Reader[7].ToString();
                    this.assign.Register.Sex.ID = this.assign.Register.Sex.ID;
                    //结算类别
                    this.assign.Register.Pact.PayKind.ID = this.Reader[8].ToString();
                    //是否急诊
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    //this.assign.Register.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    this.assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());

                    //是否预约
                    //switch( FS.HISFC.Models.Base.EnumRegType
                    this.assign.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]));
                    //看诊科室
                    this.assign.Queue.Dept.ID = this.Reader[11].ToString();
                    this.assign.Queue.Dept.Name = this.Reader[12].ToString();

                    this.assign.Register.DoctorInfo.Templet.Dept = this.assign.Queue.Dept.Clone();
                    //看诊诊室
                    this.assign.Queue.SRoom.ID = this.Reader[14].ToString();
                    this.assign.Queue.SRoom.Name = this.Reader[16].ToString();
                    //分诊队列
                    this.assign.Queue.ID = this.Reader[15].ToString();
                    this.assign.Queue.Name = this.Reader[13].ToString();
                    //看诊医生
                    this.assign.Queue.Doctor.ID = this.Reader[17].ToString();

                    //					this.assign.Register.RegDoct = this.assign.Queue.Doctor.Clone() ;

                    //看诊时间
                    this.assign.SeeTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    //分诊状态
                    this.assign.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                        FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19].ToString());

                    //分诊科室
                    this.assign.TriageDept = this.Reader[20].ToString();
                    //分诊时间
                    this.assign.TirageTime = this.Reader.GetDateTime(21);
                    //进诊时间
                    if (!this.Reader.IsDBNull(22))
                        this.assign.InTime = this.Reader.GetDateTime(22);
                    //出诊时间
                    if (!this.Reader.IsDBNull(23))
                        this.assign.OutTime = this.Reader.GetDateTime(23);
                    //操作员
                    this.assign.Oper.ID = this.Reader[24].ToString();
                    this.assign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
                    //诊台信息
                    this.assign.Queue.Console.ID = this.Reader[26].ToString();
                    this.assign.Queue.Console.Name = this.Reader[27].ToString();
                    //挂号信息
                    this.assign.Register.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[28]);
                    this.assign.Register.Pact.PayKind.ID = this.Reader[29].ToString();
                    this.assign.Register.Pact.PayKind.Name = this.Reader[30].ToString();
                    this.assign.Register.Pact.ID = this.Reader[31].ToString();
                    this.assign.Register.Pact.Name = this.Reader[32].ToString();
                    this.assign.Register.AddressHome = this.Reader[33].ToString();
                    this.assign.Register.PhoneHome = this.Reader[34].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.ID = this.Reader[35].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.Name = this.Reader[36].ToString();
                    this.assign.Register.DoctorInfo.Templet.Doct.ID = this.Reader[37].ToString();
                    this.assign.Register.DoctorInfo.Templet.Doct.Name = this.Reader[38].ToString();
                    //					this.assign.Register.BeginTime = FS.neuFC.Function.NConvert.ToDateTime(this.Reader[39]);
                    //					this.assign.Register.EndTime = FS.neuFC.Function.NConvert.ToDateTime(this.Reader[40]);
                    #endregion
                    this.assign.Register.IsAccount = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[39].ToString());
                    assign.Register.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[40]);
                    assign.Register.IsFee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[41]);
                    assign.Register.PVisit.InState.ID = this.Reader[42].ToString();//{BF3E846D-5961-4a8e-A188-C77739C7438A}加入患者状态，用于医生站留观的判断
                    //{2296D82A-7E97-4d94-90B7-052403F52DD4}增加是否分诊，用于医生站更新患者分诊状态判断
                    assign.Register.IsTriage = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[43].ToString());
                    if (this.Reader.FieldCount >= 45)
                        assign.Register.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[44].ToString());
                    if (this.Reader.FieldCount >= 46)
                        assign.Register.DoctorInfo.Templet.RegLevel.ID = Reader[45].ToString();
                    if (this.Reader.FieldCount >= 47)
                        assign.Register.DoctorInfo.Templet.RegLevel.Name = Reader[46].ToString();
                    if (this.Reader.FieldCount >= 48)
                        assign.Register.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[47]);

                    al.Add(this.assign);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询分诊信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }

        /// <summary>
        /// 根据门诊号判断患者是否已经诊出
        /// </summary>
        /// <param name="clinicCode">门诊号</param>
        /// <returns>0 诊出 1 未诊出 -1 查询出错</returns>
        public int JudgeOut(string clinicCode)
        {
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicCode);
            if (info == null || info.Register.ID == null || info.Register.ID == "")
            {
                return -1;//查询出错
            }
            if (info.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out)
            {
                return 0;//已经诊出
            }
            else
            {
                return 1;//未诊出
            }
        }

        /// <summary>
        /// 根据门诊号判断患者是否已经诊出
        /// </summary>
        /// <param name="clinicCode">门诊号</param>
        /// <returns>大于等于1：分诊队列中有患者  0： 没有  -1:查询出错</returns>
        public int JudgeInQueue(string clinicCode)
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetCommonSql("Nurse.Assign.QueryByCinic.1", ref strSql);
            if (returnValue == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.QueryByCinic.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.QueryByCinic.1]";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "字符串组成错误" + e.Message;
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));

        }

        /// <summary>
        /// 查询已看诊患者
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public ArrayList QueryCard(DateTime begin, DateTime end, string cardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.12", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.12]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.12]";
                return null;
            }

            try
            {
                where = string.Format(where, begin.ToString(), end.ToString(), cardNo);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        #endregion

        #region 业务

        /// <summary>
        /// 更新患者的分诊医生、诊台等信息
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.Nurse.Assign assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.8", ref sql) == -1)
            {
                sql = @"update met_nuo_assignrecord tt
                        set tt.queue_code = '{1}',
                            tt.queue_name = '{2}',
                            tt.doct_code = '{3}',
		                    tt.room_id = '{4}',
		                    tt.room_name = '{5}',
		                    tt.console_code = '{6}',
		                    tt.console_name = '{7}',
		                    tt.reglvl_code = '',  --分诊/取消分诊 清空挂号级别
		                    tt.reglvl_name = ''   --分诊/取消分诊 清空挂号级别
                        where tt.clinic_code = '{0}'";
            }
            sql = string.Format(sql,
                assign.Register.ID,  //门诊号
                assign.Queue.ID, //队列代码
                assign.Queue.Name, //队列名
                assign.Queue.Doctor.ID, //看诊医生code
                assign.Queue.SRoom.ID, //诊室号
                assign.Queue.SRoom.Name, //诊室名称
                assign.Queue.Console.ID, //诊台号
                assign.Queue.Console.Name //诊台名称
                );

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 插入新的分诊记录
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Nurse.Assign assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Insert.1", ref sql) == -1) return -1;

            try
            {
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
            }
            catch (Exception e)
            {
                this.Err = "插入分诊信息表出错![Nurse.Assign.Insert.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);

        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.Nurse.Assign assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Delete.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, assign.Register.ID);
            }
            catch (Exception e)
            {
                this.Err = "删除分诊信息表出错![Nurse.Assgin.Delete.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 根据CLINIC_CODE删除分诊记录
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int Delete(string clinicCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Delete.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "删除分诊信息表出错![Nurse.Assgin.Delete.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 入诊室(更新标志，入室时间)
        /// </summary>
        /// <param name="room"></param>
        /// <param name="inDate"></param>
        /// <returns></returns>
        public int Update(string clinicID, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, DateTime inDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, room.ID, room.Name, console.ID, console.Name, inDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assgin.Update.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            //增加诊台中count
            if (this.UpdateConsole(console.ID, "1") == -1)
            {
                this.ErrCode = "更新诊台就诊患者数量出错";
                return -1;
            }
            //根据clinicID获取队列信息
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicID);
            //减少队列中count
            if (this.UpdateQueue(info.Queue.ID, "-1") == -1)
            {
                this.ErrCode = "更新诊台就诊患者数量出错";
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 诊出(门诊医生直接点击诊出专用)
        /// </summary>
        /// <param name="consoleCode">诊台号码</param>
        /// <param name="clinicID">门诊号</param>
        /// <param name="outDate">诊出日期</param>
        /// <returns></returns>
        public int Update(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            //根据clinicID获取队列信息
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicID);
            if (info == null)
            {
                this.ErrCode = "更新诊台就诊患者数量出错";
                return -1;
            }
            //减少队列中count
            if (this.UpdateQueue(info.Queue.ID, "-1") == -1)
            {
                this.ErrCode = "更新诊台就诊患者数量出错";
                return -1;
            }

            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, outDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assgin.Update.2]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            int ret = this.ExecNoQuery(sql);

            //if (ret > 0)
            //{
            //    if (consoleCode == "")
            //    {
            //        consoleCode = info.Queue.Console.ID;
            //    }
            //    //减少诊台中count
            //    return this.UpdateConsole(consoleCode, "-1");
            //}
            //else
            //{
            return ret;//出错,或者没有更新到
            //}
        }

        /// <summary>
        /// 到诊
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="arriveDate"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public int UpdateArrive(string clinicCode, DateTime arriveDate, string doctID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.7", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicCode, arriveDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assgin.Update.7]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 诊出(门诊医生保存医嘱后专用)
        /// </summary>
        /// <param name="consoleCode">诊台号码</param>
        /// <param name="clinicID">门诊号</param>
        /// <param name="outDate">诊出日期</param>
        /// <returns></returns>
        public int UpdateSaved(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, outDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assgin.Update.2]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            int ret = this.ExecNoQuery(sql);
            //if (ret > 0)
            //{
            //    //减少诊台中count
            //    return this.UpdateConsole(consoleCode, "-1");
            //}
            //else
            //{
            return ret;//出错,或者没有更新到
            //}
        }

        /// <summary>
        /// 取消进诊（置回标志1）
        /// </summary>
        /// <param name="clinicID"></param>
        /// <returns></returns>
        public int CancelIn(string clinicID, string ConsoleCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.3", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assgin.Update.3]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            //减少诊台中count
            if (this.UpdateConsole(ConsoleCode, "-1") == -1)
            {
                this.ErrCode = "更新诊台就诊患者数量出错";
                return -1;
            }

            //根据clinicID获取队列信息
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicID);
            //增加队列中count
            if (this.UpdateQueue(info.Queue.ID, "1") == -1)
            {
                this.ErrCode = "更新诊台就诊患者数量出错";
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 如果要停用诊台先判断诊台中是否有患者.
        /// 只判断要停用的诊台
        /// </summary>
        /// <param name="seatID">诊台编码</param>
        /// <param name="dateTime">进诊时间</param>
        /// <returns>true:有人；false:没人</returns>
        public bool ExistPatient(string seatID, string inTime)
        {
            string strsql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.ConsoleExistPatient", ref strsql) == -1)
            {
                return true;
            }
            try
            {
                strsql = string.Format(strsql, seatID, inTime);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return true;
            }

            string retv = this.ExecSqlReturnOne(strsql);
            if (FS.FrameWork.Function.NConvert.ToInt32(retv.Trim()) > 0 /* || retv == null */ )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 更新分诊状态
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="doctID"></param>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int UpdateAssignAfterCall(params object[] objs)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.AfterCall", ref sql) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return -1;
            }

            try
            {
                sql = string.Format(sql, objs);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assign.Update.AssignFlag]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新分诊状态
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="doctID"></param>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int UpdateAssignFlag(string clinicCode, string doctID, FS.HISFC.Models.Nurse.EnuTriageStatus assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.AssignFlag", ref sql) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return -1;
            }

            try
            {
                sql = string.Format(sql, clinicCode, doctID, ((Int32)assign).ToString());
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assign.Update.AssignFlag]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 统一更新患者标记
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public int UpdateAssignFlag(int assignflag, FS.FrameWork.Models.NeuObject dept, string doctID, string seeDate, int nowAssignflag)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.UpdateAssignFlag", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, assignflag, seeDate.ToString(), dept.ID, doctID, nowAssignflag);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assgin.Update.4]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #region 更改实际看诊顺序

        /// <summary>
        ///  更改实际看诊顺序
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="seq"></param>
        /// <param name="strnum">改变的量。 如-1则表示减少一个</param>
        /// <returns></returns>
        public int Update(string clinicCode, string seq, int num)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.5", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicCode, seq, num);
            }
            catch (Exception e)
            {
                this.Err = "更新出错![Nurse.Assign.Update.5]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion

        #region 不知道干什么用

        /// <summary>
        /// 更新看诊标志
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="seeDate"></param>
        /// <param name="dept"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public int Update(string clinicID, DateTime seeDate, FS.FrameWork.Models.NeuObject dept, string doctID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.4", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, seeDate.ToString(), dept.ID, dept.Name, doctID, clinicID);
            }
            catch (Exception e)
            {
                this.Err = "更新分诊信息表出错![Nurse.Assgin.Update.4]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }


        #endregion
        #endregion

        #region 公共

        #region 队列中患者数量

        /// <summary>
        /// 更新队列中候诊数量
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="num">1 增加一个  －1 减少一个</param>
        /// <returns></returns>
        public int UpdateQueue(string queueCode, string num)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Queue.Update.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, queueCode, num);
            }
            catch (Exception e)
            {
                this.Err = "更新出错![Nurse.Queue.Update.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion

        #region 诊台中患者数量

        /// <summary>
        /// 更新诊台中的正在看诊的数量
        /// </summary>
        /// <param name="consoleCode"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int UpdateConsole(string consoleCode, string num)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Seat.Update.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, consoleCode, num);
            }
            catch (Exception e)
            {
                this.Err = "更新出错![Nurse.Seat.Update.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion
        #endregion

        #region 根据医嘱表查询已诊患者的信息

        /// <summary>
        /// 根据遗嘱表查询已诊患者的信息
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="DoctID"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(DateTime fromtime, DateTime totime, string DoctID)
        {
            FS.HISFC.Models.Registration.Register reg = new FS.HISFC.Models.Registration.Register();
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Order.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Order.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Order.Query.1]";
                return null;
            }

            try
            {
                sql = string.Format(sql, fromtime.ToString(), totime.ToString(), DoctID);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryOrder(sql);
        }

        /// <summary>
        /// 根据卡号查询遗嘱表已诊患者的信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="fromtime"></param>
        /// <param name="totime"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string cardNo, DateTime fromtime, DateTime totime)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Order.Query.2", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Order.Query.2]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Order.Query.2]";
                return null;
            }

            try
            {
                sql = string.Format(sql, cardNo, fromtime.ToString(), totime.ToString());
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryOrder(sql);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList QueryOrder(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询分诊信息出错!" + sql;
                return null;
            }
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Registration.Register reg = new FS.HISFC.Models.Registration.Register();
            try
            {
                while (this.Reader.Read())
                {
                    reg = new FS.HISFC.Models.Registration.Register();
                    reg.DoctorInfo.Templet.Doct.ID = this.Reader[0].ToString();
                    reg.DoctorInfo.Templet.Doct.Name = this.Reader[1].ToString();
                    reg.PID.CardNO = this.Reader[2].ToString();
                    reg.Name = this.Reader[3].ToString();
                    reg.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                    reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5]);
                    reg.DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
                    reg.DoctorInfo.Templet.Dept.Name = this.Reader[7].ToString();
                    reg.Sex.ID = this.Reader[8].ToString();
                    //reg.User01 = this.Reader[9].ToString();
                    reg.SeeDoct.Dept.ID = this.Reader[9].ToString();
                    reg.SeeDoct.ID = this.Reader[10].ToString();

                    al.Add(reg);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询患者信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }

        #endregion

        #region 自动进诊的查询

        /// <summary>
        /// 查询当前时间,当前队列中的最先进诊的候诊信息
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryWait(string queueCode, DateTime begin, DateTime end)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Auto.Query.1", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Auto.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Auto.Query.1]";
                return null;
            }

            try
            {
                where = string.Format(where, queueCode, begin, end);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = (FS.HISFC.Models.Nurse.Assign)al[0];
            return info;
        }

        /// <summary>
        /// 查询队列中正在看诊的信息
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryIn(string queueCode, DateTime begin, DateTime end)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Auto.Query.2", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Auto.Query.2]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Auto.Query.2]";
                return null;
            }

            try
            {
                where = string.Format(where, queueCode, begin, end);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = (FS.HISFC.Models.Nurse.Assign)al[0];
            return info;
        }

        /// <summary>
        /// 查询某诊台看诊人数
        /// </summary>
        /// <param name="consoleCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int QueryConsoleNum(string consoleCode, DateTime begin, DateTime end, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            int i = -1;
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Auto.ConsoleNum", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Auto.ConsoleNum]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Auto.ConsoleNum]";
                return -1;
            }
            try
            {
                sql = string.Format(sql, begin, end, consoleCode, (int)status);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询分诊信息出错!" + sql;
                return -1;
            }
            try
            {
                while (this.Reader.Read())
                {
                    i = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询分诊信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return i;

        }

        #endregion

        #region 优化查询

        /// <summary>
        /// 查询挂号信息 精简查询
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QuerySimpleRegInfo(string whereIndex, params string[] args)
        {
            //查询主SQL
            string sql = @"SELECT t.clinic_code,   --门诊号
                                   t.name,   --患者姓名
                                   t.sex_code,   --性别
                                   t.paykind_code,   --结算类别
                                   r.seeno,   --看诊序号 
                                   t.queue_code,   --队列代码assign.Queue.ID
                                   t.doct_code,   --看诊医生assign.Queue.Doctor.ID
                                   t.room_id,   --出诊诊室assign.Queue.SRoom.ID
                                   t.console_code,--诊台信息assign.Queue.Console.ID
                                   t.nurse_cell_code,   --分诊科室assign.TriageDept
                                   t.assign_flag,   --1分诊/2进诊/3诊出
                                   r.card_no, --门诊病历号
                                   r.order_no, --每日序号
                                   r.reglevl_code,
                                   r.reglevl_name,
                                   r.oper_date,
                                    r.reg_date,
                                    r.ynbook --预约
                              FROM met_nuo_assignrecord  t ,fin_opr_register r
                                where t.clinic_code = r.clinic_code
                             ";
            if (this.Sql.GetCommonSql(whereIndex, ref whereIndex) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return null;
            }

            try
            {
                sql = sql + "\r\n" + whereIndex;

                sql = string.Format(sql, args);

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                ArrayList al = new ArrayList();

                FS.HISFC.Models.Nurse.Assign assignObj = null;
                while (this.Reader.Read())
                {
                    assignObj = new FS.HISFC.Models.Nurse.Assign();
                    assignObj.Register.ID = this.Reader[0].ToString();//门诊流水号
                    assignObj.Register.Name = this.Reader[1].ToString();//姓名
                    assignObj.Register.Sex.ID = this.Reader[2].ToString();//性别
                    assignObj.Register.Pact.PayKind.ID = Reader[3].ToString();//结算类别
                    assignObj.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[4]);//看诊序号
                    assignObj.Register.DoctorInfo.SeeNO = assignObj.SeeNO;

                    assignObj.Queue.ID = Reader[5].ToString();//队列代码
                    assignObj.Queue.Doctor.ID = this.Reader[6].ToString(); //看诊医生

                    assignObj.Queue.SRoom.ID = this.Reader[7].ToString();//诊室
                    assignObj.Queue.Console.ID = Reader[8].ToString();//诊台
                    assignObj.TriageDept = Reader[9].ToString();//分诊科室
                    assignObj.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                                                    FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
                    assignObj.Register.PID.CardNO = Reader[11].ToString();//病历号
                    assignObj.Register.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[12].ToString());//病历号
                    assignObj.Register.DoctorInfo.Templet.RegLevel.ID = Reader[13].ToString();
                    assignObj.Register.DoctorInfo.Templet.RegLevel.Name = Reader[14].ToString();

                    assignObj.Register.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15]);
                    assignObj.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[16]);
                    assignObj.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());
                    al.Add(assignObj);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;

                return null;
            }
            finally
            {
                if (this.Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 按诊区、诊室、分诊状态查询进诊患者
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="roomID"></param>
        /// <param name="assignFlag"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignByAssignFlag(string deptID, string roomID, string assignFlag)
        {
            return this.QueryAssignBase("Nurse.Assign.Query.SimpleAssignByAssignFlag", deptID, roomID, assignFlag);
        }

        /// <summary>
        /// 根据状态查询分诊患者
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">截至时间</param>
        /// <param name="consoleID">诊台代码</param>
        /// <param name="state">状态 1.进诊患者   2.已诊患者</param>
        /// <returns>ArrayList (分诊实体数组)</returns>
        public ArrayList QuerySimpleAssignPatientByState(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID, string dept_code)
        {
            return this.QuerySimpleRegInfo("Nurse.Assign.QuerySimple.ByState", beginTime.ToString(), endTime.ToString(), consoleID, state, doctID, dept_code);
        }

        /// <summary>
        /// 按医生查询
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="state"></param>
        /// <param name="doctID"></param>
        ///  <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignPatientByDoctID(DateTime beginTime, DateTime endTime, String state, string doctID, string deptID)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.Query.QuerySimpleAssignPatientByDoctID.New", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.QuerySimpleAssignPatientByDoctID.New]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.QuerySimpleAssignPatientByDoctID.New]";
                return null;
            }

            try
            {
                sql = string.Format(sql, beginTime, endTime, doctID, state);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryAssReg(sql);
        }

        /// <summary>
        /// 按科室查询
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="state"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignPatientByDeptID(DateTime beginTime, DateTime endTime, String state, string deptID)
        {

            string sql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.Query.QuerySimpleAssignPatientByDeptID.New", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.QuerySimpleAssignPatientByDeptID.New]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.QuerySimpleAssignPatientByDeptID.New]";
                return null;
            }

            try
            {
                sql = string.Format(sql, beginTime, endTime, deptID, state);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }


            return this.QueryAssReg(sql);
        }
        #endregion

        /// <summary>
        /// 查询当天已经叫号未看诊患者
        /// </summary>
        /// <param name="doctCode"></param>
        /// <param name="clinicCode">当前叫号的患者，单个患者可以重复叫号</param>
        /// <returns></returns>
        public ArrayList QueryCalledList(string doctCode, string clinicCode)
        {
            string sql = "";
            string whereSQL = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.QueryCalledList", ref whereSQL) == -1)
            {
                whereSQL = @" where oper_code = '{0}'
                               and trunc(oper_date) = trunc(sysdate)
                               and assign_flag in ('2', '4')
                               and clinic_code!='{1}'";
            }

            try
            {
                sql = sql + "\r\n" + whereSQL;
                sql = string.Format(sql, doctCode, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.Query(sql);
        }
    }
}
