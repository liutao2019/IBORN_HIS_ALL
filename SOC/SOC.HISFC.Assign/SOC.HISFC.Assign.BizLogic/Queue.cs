using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Public;
using FS.FrameWork.Models;

namespace FS.SOC.HISFC.Assign.BizLogic
{
     /// <summary>
    /// [功能描述: 队列管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Queue : FS.FrameWork.Management.Database
    {
        #region 单条操作

        /// <summary>
        /// 获取队列号
        /// </summary>
        /// <returns></returns>
        public string GetQueueNo()
        {
            return this.GetSequence("Nurse.GetRecipeNo.Select");
        }

        /// <summary>
        /// 插入模板队列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            string[] strParam;
            try
            {
                if (string.IsNullOrEmpty(queue.ID))
                {
                    queue.ID =this.GetQueueNo();
                }
                strParam = this.myGetParmQueue(queue);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.UpdateSingleTable("SOC.Assign.Queue.Insert", strParam);
        }

        /// <summary>
        /// 修改模板队列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Update(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            //取插入操作的SQL语句
            string[] strParam;

            try
            {
                strParam = this.myGetParmQueue(queue);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.UpdateSingleTable("SOC.Assign.Queue.Update", strParam);
        }

        /// <summary>
        /// 删除队列模板
        /// </summary>
        /// <param name="queueNo"></param>
        /// <returns></returns>
        public int Delete(string queueNo)
        {
            return this.UpdateSingleTable("SOC.Assign.Queue.Delete", queueNo);
        }

        /// <summary>
        /// 获取队列模板信息
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public FS.SOC.HISFC.Assign.Models.Queue GetQueue(string queueID)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Queue.GetByKey", ref where) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.GetByKey]";
                return null;
            }

            try
            {
                where = string.Format(where, queueID);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + "\n" + where;

            ArrayList al = this.query(sql);

            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.SOC.HISFC.Assign.Models.Queue();
            }
            else
            {
                return al[0] as FS.SOC.HISFC.Assign.Models.Queue;
            }
        }

        /// <summary>
        ///  获取相似度最高的队列（自动分诊用）
        /// </summary>
        /// <param name="nurseID">护士站编码</param>
        /// <param name="deptID">科室编码</param>
        /// <param name="noonID">午别编码</param>
        /// <param name="regLevelID">挂号级别编码</param>
        /// <param name="doctID">医生编码</param>
        /// <param name="isExpert">是否专家队列</param>
        /// <returns></returns>
        public FS.SOC.HISFC.Assign.Models.Queue GetQueue(string nurseID, string deptID, string noonID, string regLevelID, string doctID, bool isExpert)
        {
            string sql = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryMinCountQueue", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QueryMinCountQueue]";
                return null;
            }

            try
            {
                sql = string.Format(sql, nurseID, deptID, noonID, regLevelID, string.IsNullOrEmpty(doctID) ? "ALL" : doctID, FS.FrameWork.Function.NConvert.ToInt32(isExpert));
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            ArrayList al = this.query(sql);

            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.SOC.HISFC.Assign.Models.Queue();
            }
            else
            {
                return al[0] as FS.SOC.HISFC.Assign.Models.Queue;
            }
        }

        /// <summary>
        /// 医生叫号时，进诊使用
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="deptID"></param>
        /// <param name="noonID"></param>
        /// <param name="doctID"></param>
        /// <param name="roomID"></param>
        /// <param name="consoleID"></param>
        /// <returns></returns>
        public FS.SOC.HISFC.Assign.Models.Queue GetQueue(string nurseID, string deptID, string noonID, string doctID, string roomID, string consoleID)
        {
            string sql = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.GetQueueByDoctorCall", ref sql) == -1)
            {
                sql = @"
                                select 
                                nurse_cell_code,   --门诊护士站代码
                                       queue_code,   --队列代码
                                       queue_name,   --队列名称
                                       noon_code,   --午别
                                       queue_flag,   --1医生队列/2自定义队列
                                       sort_id,   --显示顺序
                                       valid_flag,   --1有效/0无效
                                       remark,   --备注
                                       oper_code,   --操作员
                                       oper_date,   --操作时间
                                       queue_date,   --队列日期
                                       doct_code ,   --看诊医生
                                       ROOM_ID,
                                       ROOM_NAME,
                                       CONSOLE_CODE,
                                       CONSOLE_NAME,
                                       EXPERT_FLAG,
                                       dept_code,
                                       dept_name,
                                       waiting_count,
                                       reglvl_code,
                                       reglvl_name
                                 from
                                (
                                select * from met_nuo_queue
                                WHERE nurse_cell_code='{0}'
                                   and dept_code = '{1}'
                                   AND trunc(queue_date) =trunc(sysdate)
                                   AND doct_code='{2}'
                                   and QUEUE_FLAG='0'
                                   and WAITING_COUNT>0
                                union all
                                select * from met_nuo_queue
                                WHERE nurse_cell_code='{0}'
                                   and dept_code = '{1}'
                                   AND trunc(queue_date) =trunc(sysdate)
                                   and EXPERT_FLAG='0'
                                   and QUEUE_FLAG='1'
                                   and WAITING_COUNT>0
                                union all
                                select * from met_nuo_queue t
                                WHERE nurse_cell_code='{0}'
                                   and dept_code = '{1}'
                                   AND trunc(queue_date) =trunc(sysdate)
                                   and t.room_id='{3}'
                                   AND t.console_code='{4}'
                                   and QUEUE_FLAG='2'
                                   and WAITING_COUNT>0
                                   )
                                   where VALID_FLAG=fun_get_valid()
                                   and rownum<=1";
            }


            try
            {
                sql = string.Format(sql, nurseID, deptID, doctID, roomID,consoleID);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            ArrayList al = this.query(sql);

            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.SOC.HISFC.Assign.Models.Queue();
            }
            else
            {
                return al[0] as FS.SOC.HISFC.Assign.Models.Queue;
            }
        }
           
        /// <summary>
        /// 判断队列医生是否重复
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>-1 错误 0 不存在 >=1 存在</returns>
        public int IsExistDoctor(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            string sql = "";
            if (this.Sql.GetSql("SOC.Assign.Queue.QueryCountByDoctor", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QueryCountByDoctor]";
                return -1;
            }

            sql = string.Format(sql, queue.ID, queue.AssignNurse.ID, queue.QueueDate.ToString("yyyy-MM-dd"), queue.Noon.ID, queue.Doctor.ID);

            if (this.ExecQuery(sql) > 0)
            {
                if (this.Reader != null && this.Reader.Read())
                {
                    return FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
            }

            return 0;
        }

        /// <summary>
        /// 判断是否存在同名队列
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public int IsExistQueueName(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            string sql = "";
            if (this.Sql.GetSql("SOC.Assign.Queue.QueryCountByQueueName", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错！[SOC.Assign.Queue.QueryCountByQueueName]";
                return -1;
            }
            sql = string.Format(sql, queue.ID, queue.AssignNurse.ID, queue.QueueDate.ToString("yyyy-MM-dd"), queue.Noon.ID, queue.Name);
            if (this.ExecQuery(sql) > 0)
            {
                if (this.Reader != null && this.Reader.Read())
                {
                    return FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());
                }
            }
            return 0;
        }
        /// <summary>
        /// 判断队列诊室诊台是否重复
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>-1 错误 0 不存在 >=1 存在</returns>
        public int IsExistRoomAndConsole(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            string sql = "";
            if (this.Sql.GetSql("SOC.Assign.Queue.QueryCountByCustom", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QueryCountByCustom]";
                return -1;
            }

            sql = string.Format(sql, queue.ID, queue.AssignNurse.ID, queue.QueueDate.ToString("yyyy-MM-dd"), queue.Noon.ID, queue.SRoom.ID, queue.Console.ID);

            if (this.ExecQuery(sql) > 0)
            {
                if (this.Reader != null && this.Reader.Read())
                {
                    return FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
            }

            return 0;
        }

        /// <summary>
        /// 判断队列级别（只判断级别队列）是否重复
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>-1 错误 0 不存在 >=1 存在</returns>
        public int IsExistRegLevel(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            string sql = "";
            if (this.Sql.GetSql("SOC.Assign.Queue.QueryCountByRegLevel", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QueryCountByRegLevel]";
                return -1;
            }

            string patientConditionType = "ALL";
            if (!string.IsNullOrEmpty(queue.PatientConditionType))
            {
                patientConditionType = queue.PatientConditionType;
            }

            sql = string.Format(sql, queue.ID, queue.AssignNurse.ID, queue.QueueDate.ToString("yyyy-MM-dd"), queue.Noon.ID, queue.RegLevel.ID, queue.AssignDept.ID, patientConditionType);

            if (this.ExecQuery(sql) > 0)
            {
                if (this.Reader != null && this.Reader.Read())
                {
                    return FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
            }

            return 0;
        }

        /// <summary>
        /// 判断队列是否已使用
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>-1 错误 0 没有使用 >=1 已使用</returns>
        public int IsExistUsed(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            return 0;
        }

        /// <summary>
        /// 判断队列是否存在患者
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>-1 错误 0 没有 >=1 有</returns>
        public int IsExistHavePatient(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            string sql = "";
            if (this.Sql.GetSql("SOC.Assign.Queue.QueryPatientCount", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QueryPatientCount]";
                return -1;
            }

            sql = string.Format(sql, queue.ID,queue.PatientConditionType.ToString());

            if (this.ExecQuery(sql) > 0)
            {
                if (this.Reader != null && this.Reader.Read())
                {
                    return FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
            }

            return 0;
        }

        /// <summary>
        /// 更新队列中候诊数量
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="num">1 增加一个  －1 减少一个</param>
        /// <returns></returns>
        public int UpdateWaitingNum(string queueCode, int num)
        {
            return this.UpdateSingleTable("SOC.Assign.Queue.UpdateWaitingNum", queueCode, num.ToString());
        }

        /// <summary>
        /// 按护士站id查询分诊模板队列信息（全部人数）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <returns></returns>
        public ArrayList QueryNurseQueueNum(string nurseID, DateTime queueDate)
        {
            ArrayList alQueue = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryAlQueue", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, nurseID, queueDate.Date.ToString("yyyy-MM-dd"));
            }
            catch
            {
                this.Err = "传入参数不对！SOC.Assign.Queue.QueryAlQueue";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();//侯诊数量
                    obj.Memo = this.Reader[2].ToString();//进诊数量
                    obj.User01 = this.Reader[3].ToString();//已诊数量
                    obj.User02 = this.Reader[4].ToString();//到诊数量
                    obj.User03 = this.Reader[5].ToString();//未看诊数量

                    alQueue.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "查询出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            return alQueue;
        }

        public ArrayList QueryNurseQueueNumByPatientConditionType(string nurseID, DateTime queueDate, string patientConditionType)
        {
            ArrayList alQueue = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryAlQueueByPatientConditionType", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, nurseID, queueDate.Date.ToString("yyyy-MM-dd"), patientConditionType);
            }
            catch
            {
                this.Err = "传入参数不对！SOC.Assign.Queue.QueryAlQueueByPatientConditionType";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();//侯诊数量
                    obj.Memo = this.Reader[2].ToString();//进诊数量
                    obj.User01 = this.Reader[3].ToString();//已诊数量
                    obj.User02 = this.Reader[4].ToString();//到诊数量
                    obj.User03 = this.Reader[5].ToString();//未看诊数量

                    alQueue.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "查询出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            return alQueue;
        }

        /// <summary>
        /// 按护士站id查询分诊模板队列信息（全部人数）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <returns></returns>
        public ArrayList QueryNurseQueueNum(string nurseID, DateTime queueDate, string deptID)
        {
            ArrayList alQueue = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryAlQueueDept", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, nurseID, queueDate.Date.ToString("yyyy-MM-dd"), deptID);
            }
            catch
            {
                this.Err = "传入参数不对！SOC.Assign.Queue.QueryAlQueueDept";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();//侯诊数量
                    obj.Memo = this.Reader[2].ToString();//进诊数量
                    obj.User01 = this.Reader[3].ToString();//已诊数量
                    obj.User02 = this.Reader[4].ToString();//到诊数量
                    obj.User03 = this.Reader[5].ToString();//未看诊数量

                    alQueue.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "查询出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            return alQueue;
        }
        #endregion

        #region 多条查询

        /// <summary>
        /// 按护士站id查询分诊模板队列信息（全部）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <returns></returns>
        public ArrayList QueryAllByNurseID(string nurseID, DateTime queueDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryByNurse.All", ref where) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QueryByNurse.All]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, queueDate.Date.ToString("yyyy-MM-dd"));
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + "\n" + where;

            return this.query(sql);
        }

        /// <summary>
        /// 按护士站id查询分诊模板队列信息（有效）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <returns></returns>
        public ArrayList QueryValidByNurseID(string nurseID, DateTime queueDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryByNurse.Valid", ref where) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.Queue.QueryByNurse.Valid]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, queueDate.Date.ToString("yyyy-MM-dd"));
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + "\n" + where;

            return this.query(sql);
        }

        /// <summary>
        /// 按护士站/分诊日期/午别查询分诊队列信息
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        public ArrayList QueryValidByNurseID(string nurseID, DateTime queueDate, string noonID)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QuerySql", ref sql) == -1)
            {
                this.Err = "查询分诊队列信息出错![SOC.Assign.Queue.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryValidByNurseNoon", ref where) == -1)
            {
                this.Err = "查询分诊队列信息出错![SOC.Assign.Queue.QueryValidByNurseNoon]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, queueDate.Date.ToString("yyyy-MM-dd"), noonID);
            }
            catch (Exception e)
            {
                this.Err =  e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.query(sql);
        }

        /// <summary>
        /// 按护士站/分诊日期/午别/科室 查询分诊队列信息
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        public ArrayList QueryValidByNurseID(string nurseID, DateTime queueDate, string noonID, string deptCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QuerySql", ref sql) == -1)
            {
                this.Err = "查询分诊队列信息出错![SOC.Assign.Queue.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.Queue.QueryValidByNurseNoonAndDept", ref where) == -1)
            {
                this.Err = "查询分诊队列信息出错![SOC.Assign.Queue.QueryValidByNurseNoonAndDept]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, queueDate.Date.ToString("yyyy-MM-dd"), noonID, deptCode);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.query(sql);
        }

        #endregion

        /// <summary>
        /// 根据医生或诊室编码查询对应的队列信息
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <param name="doctCode"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public ArrayList QueryValidByDoctAndRoomID(string nurseID, DateTime queueDate, string noonID, string doctCode, string roomID)
        {
            return this.QueryBase("SOC.Assign.Queue.Where.ByDoctAndRoomID", nurseID, queueDate.ToString(), noonID, doctCode, roomID);
        }

        public ArrayList QueryValidByDoctAndRoomID(string nurseID, DateTime queueDate, string noonID, string doctCode, string roomID,string deptID)
        {
            return this.QueryBase("SOC.Assign.Queue.Where.ByDoctAndRoomIDAndDeptID", nurseID, queueDate.ToString(), noonID, doctCode, roomID, deptID);
        }


        #region 特殊查询

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据whereID和参数查询队列信息
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryBase(string whereSQL, params object[] args)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.Queue.QuerySql", ref sql) == -1)
            {
                this.Err = "查询分诊队列信息出错![SOC.Assign.Queue.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql(whereSQL, ref where) == -1)
            {
                this.Err = "查询分诊队列信息出错![" + whereSQL + "]";
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

            return this.query(sql);
        }

        protected string[] myGetParmQueue(FS.SOC.HISFC.Assign.Models.Queue obj)
        {
            string[] strParm ={
									obj.ID,//队列号0
								    obj.AssignNurse.ID,//代码1
									obj.Name,//队列名称2
									obj.Noon.ID,//午别3		
                                    ((int)obj.QueueType).ToString(),//队列类别4
									obj.Order.ToString(),//顺序5
									FS.FrameWork.Function.NConvert.ToInt32(obj.IsValid).ToString(),//是否有效6
									obj.Memo,//备注7
									obj.Oper.ID,//操作员8
									obj.QueueDate.ToString("yyyy-MM-dd HH:mm:ss"),//队列时间9
									obj.Doctor.ID,//医生代码10
									obj.SRoom.ID,//诊室代码11
									obj.SRoom.Name,//诊室名称12
									obj.Console.ID,//诊台代码13
									obj.Console.Name,//诊台名称14
									FS.FrameWork.Function.NConvert.ToInt32(obj.IsExpert).ToString(),//专家15
								    obj.AssignDept.ID,//分诊科室16
									obj.AssignDept.Name,//分诊科室17
                                    obj.RegLevel.ID,//挂号级别18
                                    obj.RegLevel.Name,//挂号级别19
                                    obj.PatientConditionType //患者病情分类：危殆、危急、紧急、次紧急、非紧急
							 };

            return strParm;

        }

        /// <summary>
        /// 根据sql查询队列信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList query(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "查询sql出错!" + sql;
                this.ErrCode = "查询sql出错!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();

            if (this.Reader != null)
            {
                try
                {
                    FS.SOC.HISFC.Assign.Models.Queue queue = null;
                    while (this.Reader.Read())
                    {
                        queue = new FS.SOC.HISFC.Assign.Models.Queue();

                        //所属护士站
                        queue.AssignNurse.ID = this.Reader[0].ToString();
                        //队列代码
                        queue.ID = this.Reader[1].ToString();
                        //队列名称
                        queue.Name = this.Reader[2].ToString();
                        //午别代码
                        queue.Noon.ID = this.Reader[3].ToString();

                        //队列类型[2007/03/27]
                        queue.QueueType = EnumHelper.Current.GetEnum<FS.SOC.HISFC.Assign.Models.EnumQueueType>(this.Reader[4].ToString());

                        //显示顺序
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
                        queue.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[16]);
                        //分诊科室
                        queue.AssignDept.ID = this.Reader[17].ToString();
                        queue.AssignDept.Name = this.Reader[18].ToString();
                        queue.WaitingCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19]);
                        queue.RegLevel.ID = this.Reader[20].ToString();
                        queue.RegLevel.Name = this.Reader[21].ToString();

                        if (this.Reader.FieldCount > 22)
                        {
                            queue.PatientConditionType = Reader[22].ToString();
                        }

                        al.Add(queue);
                    }

                }
                catch (Exception e)
                {
                    this.Err = "查询门诊护士站分诊队列信息出错!" + e.Message;
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
            }

            return al;
        }
        #endregion
    }
}
