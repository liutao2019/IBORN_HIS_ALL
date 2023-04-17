using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizLogic
{
    /// <summary>
    /// [功能描述: SOC分诊管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Assign : FS.FrameWork.Management.Database
    {

        #region 单条操作

        /// <summary>
        /// 插入新的分诊记录
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            string sql = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Insert", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, assign.Register.ID, assign.SeeNO, assign.Register.PID.CardNO, assign.Register.DoctorInfo.SeeDate.ToString()
                    , assign.Register.Name, assign.Register.Sex.ID, assign.Register.Pact.PayKind.ID, FS.FrameWork.Function.NConvert.ToInt32(assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency)
                    , FS.FrameWork.Function.NConvert.ToInt32(assign.Register.RegType), assign.Register.DoctorInfo.Templet.Dept.ID, assign.Register.DoctorInfo.Templet.Dept.Name, assign.Queue.Name
                    , assign.Queue.SRoom.ID, assign.Queue.ID, assign.Queue.SRoom.Name, assign.Queue.Doctor.ID
                    , assign.SeeTime.ToString(), (int)assign.TriageStatus, assign.TriageDept, assign.TirageTime.ToString()
                    , assign.Oper.ID, assign.Oper.OperTime.ToString(), assign.Queue.Console.ID, assign.Queue.Console.Name, assign.Register.DoctorInfo.Templet.RegLevel.ID, assign.Register.DoctorInfo.Templet.RegLevel.Name, assign.Queue.PatientConditionType);
            }
            catch (Exception e)
            {
                this.Err = "插入分诊信息表出错![SOC.Assign.Assign.Insert]" + e.Message;
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

            if (this.Sql.GetSql("Nurse.Assign.Delete.1", ref sql) == -1) return -1;

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
        /// 根据门诊流水号，分诊标志获取一个唯一分诊信息
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public FS.SOC.HISFC.Assign.Models.Assign QueryByClinicID(string clinicCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.All", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.ByClinicCode", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.ByClinicCode]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.ByClinicCode]";
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

            ArrayList al = this.QueryBase(sql);
            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.SOC.HISFC.Assign.Models.Assign();
            }
            else
            {
                return (FS.SOC.HISFC.Assign.Models.Assign)al[0];
            }
        }

        /// <summary>
        /// 更新状态
        /// 进诊（clinicCode,room,console）
        /// 到诊（clinicCode,doctID）
        /// 诊出（clinicCode,doctID）
        /// 取消进诊（clinicCode,<paramref name="isTriageClearDoctorInfo"/>,<paramref name="isTriageClearRoomInfo"/>）
        /// </summary>
        /// <param name="status"></param>
        /// <param name="clinicCode"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="doctID"></param>
        /// <param name="date"></param>
        /// <param name="isTriageClearDoctorInfo">在取消进诊时使用，更新为分诊状态时是否清空医师信息。</param>
        /// <param name="isTriageClearRoomInfo">在取消进诊时使用，更新为分诊状态时是否清空诊室信息。</param>
        /// <returns></returns>
        public int Update(
            FS.HISFC.Models.Nurse.EnuTriageStatus status, 
            string clinicCode,
            bool isTriageClearDoctorInfo,
            bool isTriageClearRoomInfo, 
            FS.FrameWork.Models.NeuObject room, 
            FS.FrameWork.Models.NeuObject console, 
            string doctID)
        {
            switch (status)
            {
                case FS.HISFC.Models.Nurse.EnuTriageStatus.In://进诊
                    return this.UpdateSingleTable("SOC.Assign.Assign.Update.In", clinicCode, room.ID, room.Name, console.ID, console.Name, this.Operator.ID);
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive://到诊
                    return this.UpdateSingleTable("SOC.Assign.Assign.Update.Arrive", clinicCode, doctID, this.Operator.ID);
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Out://诊出
                    return this.UpdateSingleTable("SOC.Assign.Assign.Update.Out", clinicCode, doctID, this.Operator.ID);
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Triage://分诊
                    // {d4ea07b4-2559-4473-ac92-f8076d9ce3b4}
                    if (isTriageClearDoctorInfo && isTriageClearRoomInfo)
                    {
                        return this.UpdateSingleTable("SOC.Assign.Assign.Update.TriageWithClearRoomDoctor", clinicCode, this.Operator.ID);
                    }
                    else if (isTriageClearDoctorInfo && !isTriageClearRoomInfo)
                    {
                        return this.UpdateSingleTable("SOC.Assign.Assign.Update.TriageWithClearDoctor", clinicCode, this.Operator.ID);
                    }
                    else if (!isTriageClearDoctorInfo && isTriageClearRoomInfo)
                    {
                        return this.UpdateSingleTable("SOC.Assign.Assign.Update.TriageWithClearRoom", clinicCode, this.Operator.ID);
                    }
                    else
                    {
                        return this.UpdateSingleTable("SOC.Assign.Assign.Update.Triage", clinicCode, this.Operator.ID);
                    }
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Delay://未看诊
                    return this.UpdateSingleTable("SOC.Assign.Assign.Update.NoSee", clinicCode, this.Operator.ID);
                default:
                    return -1;
            }
        }

        /// <summary>
        /// 更新看诊序号
        /// </summary>
        /// <param name="Type">1医生 2科室 3护士站 4全院</param>
        /// <param name="seeDate">看诊日期</param>
        /// <param name="Subject">Type=1时,医生代码;Type=2,科室代码;Type=3,护士站代码;Type=4,ALL</param>
        /// <param name="noonID">午别</param>
        /// <returns></returns>
        public int UpdateSeeNO(string Type, DateTime seeDate, string Subject, string noonID)
        {
            return this.UpdateSingleTable("Registration.Register.UpdateSeeSequence", seeDate.Date.ToString("yyyy-MM-dd HH:mm:ss"), Type, Subject, noonID);
        }

        /// <summary>
        /// 插入看诊序号
        /// </summary>
        /// <param name="Type">1医生 2科室 3护士站 4全院</param>
        /// <param name="seeDate">看诊日期</param>
        /// <param name="Subject">Type=1时,医生代码;Type=2,科室代码;Type=3,护士站代码;Type=4,ALL</param>
        /// <param name="noonID">午别</param>
        /// <returns></returns>
        public int InsertSeeNO(string Type, DateTime seeDate, string Subject, string noonID)
        {
            return this.UpdateSingleTable("Registration.Register.InsertSeeSequence", seeDate.Date.ToString("yyyy-MM-dd HH:mm:ss"), Type, Subject, "", "1", noonID);
        }

        /// <summary>
        /// 插入看诊序号
        /// </summary>
        /// <param name="Type">1医生 2科室 3护士站 4全院</param>
        /// <param name="seeDate">看诊日期</param>
        /// <param name="Subject">Type=1时,医生代码;Type=2,科室代码;Type=3,护士站代码;Type=4,ALL</param>
        /// <param name="currentNum">序号</param>
        /// <param name="noonID">午别</param>
        /// <returns></returns>
        public int InsertSeeNOPre(string Type, DateTime seeDate, string Subject, string currentNum, string noonID)
        {
            return this.UpdateSingleTable("Registration.Register.InsertSeeSequence", seeDate.Date.ToString("yyyy-MM-dd HH:mm:ss"), Type, Subject, "", currentNum, noonID);
        }

        /// <summary>
        /// 获得预约表，预约时间段人数
        /// </summary>
        /// <param name="seeDate">看诊日期</param>
        /// <param name="Subject">科室</param>
        /// <param name="noonID">时间点</param>
        /// <returns></returns>
        public int GetSeeNoPre(DateTime current, string deptID, string time, ref int seeNo)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetSql("Registration.Register.getSeeNoPre", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, current.Date.ToString("yyyy-MM-dd"), deptID, time);

                rtn = this.ExecSqlReturnOne(sql, "0");

                seeNo = FS.FrameWork.Function.NConvert.ToInt32(rtn);

                return 1;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 按开始时间获取该段时间内预约人数及预约已看诊人数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="bookNum"></param>
        /// <param name="seeNum"></param>
        /// <returns></returns>
        public int GetBookNums(string deptCode, DateTime dtBegin, ref int bookNum, ref int seeNum)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetSql("Registration.Register.GetBookNums", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, deptCode, dtBegin);

                if (this.ExecQuery(sql) == -1)
                {
                    this.Err = "获取预约相关数据出错!" + sql;
                    return -1;
                }

                while (this.Reader.Read())
                {
                    bookNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());
                    seeNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                }

                return 1;
            }
            catch (Exception e)
            {
                this.Err = "获取预约相关数据出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 按开始时间获取该段时间内现场挂号人数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="bookNum"></param>
        /// <param name="seeNum"></param>
        /// <returns></returns>
        public int GetRegNums(string deptCode, DateTime dtBegin, ref int regNum)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetSql("Registration.Register.GetRegNums", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, deptCode, dtBegin);

                if (this.ExecQuery(sql) == -1)
                {
                    this.Err = "获取现场挂号相关数据出错!" + sql;
                    return -1;
                }

                while (this.Reader.Read())
                {
                    regNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());
                }

                return 1;
            }
            catch (Exception e)
            {
                this.Err = "获取现场挂号相关数据出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 获得患者看诊序号
        /// </summary>
        /// <param name="Type">1医生 2科室 3护士站 4全院</param>
        /// <param name="seeDate">看诊日期</param>
        /// <param name="Subject">Type=1时,医生代码;Type=2,科室代码;Type=3,护士站代码;Type=4,ALL</param>
        /// <param name="noonID">午别</param>
        /// <returns></returns>
        public int GetSeeNO(string Type, DateTime current, string subject, string noonID, ref int seeNo)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetSql("Registration.Register.getSeeNo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, current.Date.ToString("yyyy-MM-dd HH:mm:ss"), Type, subject, noonID);

                rtn = this.ExecSqlReturnOne(sql, "0");

                seeNo = FS.FrameWork.Function.NConvert.ToInt32(rtn);

                return 1;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #endregion

        #region 多条操作

        /// <summary>
        /// 按时间、队列、分诊科室查询分诊信息(还要求是有效号)
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="queueID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime today, string queueID, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.All", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.ByStatusAndValid", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.ByStatusAndValid]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.ByStatusAndValid]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, today.ToString(), queueID, (int)status);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryBase(sql);
        }

        /// <summary>
        /// 按照分诊台、队列、状态查询有效的分诊信息
        /// </summary>
        /// <param name="nurseID">分诊台</param>
        /// <param name="queueCode">队列信息，可以有多个</param>
        /// <param name="dt">队列日期</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, string queueCode, DateTime dt, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.All", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Assign.Where.ByQueueCodeAndStatus", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Where.ByQueueCodeAndStatus]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Where.ByQueueCodeAndStatus]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, queueCode, dt.Date.ToString(), (int)status);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + "\r\n" + where;

            return this.QueryBase(sql);
        }

        /// <summary>
        /// 按时间、分诊科室查询分诊信息(还要求是有效号)
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime today, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.All", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Assign.QueryAll.ByStatusAndValid", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.QueryAll.ByStatusAndValid]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.QueryAll.ByStatusAndValid]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, today.ToString(), (int)status);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryBase(sql);
        }

        /// <summary>
        /// 按时间、分诊科室查询分诊信息
        /// 关联叫号表
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime today, string noonID)
        {
            string sql = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.LinkCallQueue", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.LinkCallQueue]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.LinkCallQueue]";
                return null;
            }

            try
            {
                sql = string.Format(sql, nurseID, today.ToString(), noonID);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryBase(sql);
        }

        /// <summary>
        /// 获取该护士站下的第一个需要叫号的患者（深圳）
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryFirstCall(string nurseCode, string date)
        {
            string sql = "";
            if (this.Sql.GetSql("Nurse.Call.QueryFirst", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[Nurse.Call.QueryFirst]";
                this.ErrCode = "查询sql出错,索引为[Nurse.Call.QueryFirst]";
                return null;
            }

            try
            {
                sql = string.Format(sql, nurseCode, date);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryBase(sql);
        }

        /// <summary>
        /// 查询未看诊的病人
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        public ArrayList QueryNoSee(string nurseID, DateTime today, string noonID)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.All", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.ByNoSee", ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.ByNoSee]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.ByNoSee]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, today.ToString("yyyy-MM-dd HH:mm:ss"), noonID);
            }
            catch (Exception e)
            {
                this.Err = "字符转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryBase(sql);
        }

        /// <summary>
        /// 根据医生或诊室信息查询对应分诊台的未诊患者(分诊、进诊、到诊）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="dt"></param>
        /// <param name="doctCode"></param>
        /// <param name="roomID"></param>
        /// <param name="patientCondition"></param>
        /// <returns></returns>
        public ArrayList QueryByDoctOrRoomForOrder(string nurseID, string deptCode, DateTime dt, string doctCode, string roomID, string patientCondition, bool isAll)
        {
            string strAll = "";
            if (isAll)
            {
                strAll = "ALL";
            }
            return QueryBase("SOC.Assign.Assign.Where.ByDoctAndRoomID.ForOrder", nurseID, dt, doctCode, roomID, patientCondition, strAll, deptCode);
        }

        /// <summary>
        /// 根据医生或诊室信息查询对应分诊台的未诊患者(分诊）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="dt"></param>
        /// <param name="doctCode"></param>
        /// <param name="roomID"></param>
        /// <param name="patientCondition"></param>
        /// <returns></returns>
        public ArrayList QueryByDoctOrRoomForNurse(string nurseID, string deptCode, DateTime dt, string doctCode, string roomID)
        {
            return QueryBase("SOC.Assign.Assign.Where.ByDoctAndRoomID.ForNurse", nurseID, dt, doctCode, roomID, deptCode);
        }

        /// <summary>
        /// 根据医生或诊室及分诊状态查询对应分诊台的未诊患者
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="dt"></param>
        /// <param name="doctCode"></param>
        /// <param name="roomID"></param>
        /// <param name="patientCondition"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Obsolete("暂时无用，为了和医生站列表保持一致，用QueryByDoctOrRoom代替", true)]
        public ArrayList QueryByDoctAndRoomAndStatus(string nurseID, DateTime dt, string doctCode, string roomID, string patientCondition, FS.HISFC.Models.Nurse.EnuTriageStatus status, bool isAll)
        {
            string strAll = "";
            if (isAll)
            {
                strAll = "ALL";
            }
            return QueryBase("SOC.Assign.Assign.Where.ByDoctAndRoomIDAndStatus", nurseID, dt, doctCode, roomID, patientCondition, (int)status, strAll);
        }

        #endregion

        #region 私有方法

        private ArrayList QueryBase(string whereSQL, params object[] args)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.All", ref sql) == -1)
            {
                this.Err = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                this.ErrCode = "查询sql出错,索引为[SOC.Assign.Assign.Query.All]";
                return null;
            }

            if (this.Sql.GetSql(whereSQL, ref where) == -1)
            {
                this.Err = "查询sql出错,索引为[" + whereSQL + "]";
                this.ErrCode = "查询sql出错,索引为[" + whereSQL + "]";
                return null;
            }

            try
            {
                where = string.Format(where, args);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + "\r\n" + where;

            return this.QueryBase(sql);
        }

        /// <summary>
        /// 分诊信息基本查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList QueryBase(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询分诊信息出错!" + "\r\n" + Err + "\r\n" + sql;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                FS.SOC.HISFC.Assign.Models.Assign assign = null;
                while (this.Reader.Read())
                {
                    #region 赋值
                    assign = new FS.SOC.HISFC.Assign.Models.Assign();

                    //门诊号
                    assign.Register.ID = this.Reader[2].ToString();

                    //看诊序号
                    assign.SeeNO = this.Reader[3].ToString();
                    assign.Register.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(assign.SeeNO);

                    //病历号
                    assign.Register.PID.CardNO = this.Reader[4].ToString();
                    assign.Register.Card.ID = assign.Register.PID.CardNO;
                    //挂号日期
                    assign.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    //患者姓名
                    assign.Register.Name = this.Reader[6].ToString();
                    //性别
                    assign.Register.Sex.ID = this.Reader[7].ToString();
                    assign.Register.Sex.ID = assign.Register.Sex.ID;
                    //结算类别
                    assign.Register.Pact.PayKind.ID = this.Reader[8].ToString();
                    //是否急诊
                    assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    //是否预约
                    assign.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]));
                    //看诊科室
                    assign.Queue.AssignDept.ID = this.Reader[11].ToString();
                    assign.Queue.AssignDept.Name = this.Reader[12].ToString();

                    assign.Register.DoctorInfo.Templet.Dept = assign.Queue.AssignDept.Clone();
                    //看诊诊室
                    assign.Queue.SRoom.ID = this.Reader[14].ToString();
                    assign.Queue.SRoom.Name = this.Reader[16].ToString();
                    //分诊队列
                    assign.Queue.ID = this.Reader[15].ToString();
                    assign.Queue.Name = this.Reader[13].ToString();
                    //看诊医生
                    assign.Queue.Doctor.ID = this.Reader[17].ToString();

                    assign.Register.DoctorInfo.Templet.Doct = assign.Queue.Doctor.Clone();

                    //看诊时间
                    assign.SeeTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18]);
                    //分诊状态
                    assign.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                                                    FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19].ToString());

                    //分诊科室
                    assign.TriageDept = this.Reader[20].ToString();
                    //分诊时间
                    assign.TirageTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[21]);
                    //进诊时间
                    assign.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22]);
                    //出诊时间
                    assign.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[23]);
                    //操作员
                    assign.Oper.ID = this.Reader[24].ToString();
                    assign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
                    //诊台信息
                    assign.Queue.Console.ID = this.Reader[26].ToString();
                    assign.Queue.Console.Name = this.Reader[27].ToString();
                    assign.Register.DoctorInfo.Templet.RegLevel.ID = this.Reader[28].ToString();
                    assign.Register.DoctorInfo.Templet.RegLevel.Name = this.Reader[29].ToString();

                    if (Reader.FieldCount > 30)
                    {
                        //病情分类
                        assign.Queue.PatientConditionType = Reader[30].ToString();
                    }
                    if (Reader.FieldCount > 31)
                    {
                        //预约看诊时间
                        assign.Register.DoctorInfo.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31]);
                    }
                    if (Reader.FieldCount > 32)
                    {
                        //初复诊标志
                        assign.Register.IsFirst = FS.FrameWork.Function.NConvert.ToBoolean(Reader[32]);
                    }
                    if (Reader.FieldCount > 33)
                    {
                        //病人号
                        assign.Register.PID.PatientNO = Reader[33].ToString();
                    }
                    if (Reader.FieldCount > 34)
                    {
                        //病人号
                        assign.Register.IDCard = Reader[34].ToString();
                    }
                    #endregion

                    al.Add(assign);
                }

            }
            catch (Exception e)
            {
                this.Err = "查询分诊信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }

            return al;
        }

        #endregion

        #region
        /// <summary>
        /// 根据操作员部门号，获取护士站代码
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public string GetNurseCellCode(string deptId)
        {
            string sql = @"	select d.pardep_code   from com_deptstat d where d.dept_code='{0}' and d.stat_code='14'";
            try
            {
                sql = string.Format(sql, deptId);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.ExecSqlReturnOne(sql, "");

        }

        /// <summary>
        /// 根据护士站代码获取已分诊患者
        /// </summary>
        /// <param name="dtReg"></param>
        /// <param name="nurseCellCode"></param>
        /// <param name="flag">标示 1：分诊 2：进诊 3：出诊</param>
        /// <returns></returns>
        public ArrayList GetPatientByNurseCellCode(string nurseCellCode, string flag, string dtReg)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Assign.Query.All", ref sql) == -1)
            {
                sql = @"SELECT '',   --父级医疗机构编码
       '',   --本级医疗机构编码
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
       oper_date,    --操作时间
       console_code,
       console_name,
       reglvl_code,
       reglvl_name
  FROM met_nuo_assignrecord   --护士分诊记录表";
            }

            if (this.Sql.GetSql("SOC.Assign.Assignrecord.Where", ref where) == -1)
            {
                where = @"where nurse_cell_code='{0}'
	and assign_flag='{1}' 	and to_char(reg_date,'yyyy-MM-dd')='{2}' 	order by in_date ";
            }

            try
            {
                where = string.Format(where, nurseCellCode, flag, dtReg);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + "\n" + where;

            return this.QueryBase(sql);
        }
        #endregion
    }
}
