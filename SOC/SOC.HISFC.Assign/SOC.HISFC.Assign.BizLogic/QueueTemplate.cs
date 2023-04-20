using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Public;

namespace FS.SOC.HISFC.Assign.BizLogic
{
    /// <summary>
    /// [功能描述: 队列模板管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class QueueTemplate : FS.FrameWork.Management.Database
    {
        #region 单条操作

        /// <summary>
        /// 插入模板队列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Assign.Models.QueueTemplate queueTemplate)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            string[] strParam;
            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.Insert", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为SOC.Assign.QueueTemplate.Insert";
                return -1;
            }

            try
            {
                if (string.IsNullOrEmpty(queueTemplate.ID))
                {
                    queueTemplate.ID = new Queue().GetQueueNo();
                }
                strParam = this.myGetParmQueueTemplate(queueTemplate);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL, strParam);
        }

        /// <summary>
        /// 修改模板队列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Update(FS.SOC.HISFC.Assign.Models.QueueTemplate queueTemplate)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            string[] strParam;
            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.Update", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为SOC.Assign.QueueTemplate.Update";
                return -1;
            }

            try
            {
                strParam = this.myGetParmQueueTemplate(queueTemplate);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL, strParam);
        }

        /// <summary>
        /// 删除队列模板
        /// </summary>
        /// <param name="queueNo"></param>
        /// <returns></returns>
        public int Delete(string queueNo)
        {
            string strSql = "";
            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.Delete", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, queueNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获取队列模板信息
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public FS.SOC.HISFC.Assign.Models.QueueTemplate GetQueueTemplate(string queueID)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.GetByKey", ref where) == -1)
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
                return new FS.SOC.HISFC.Assign.Models.QueueTemplate();
            }
            else
            {
                return al[0] as FS.SOC.HISFC.Assign.Models.QueueTemplate;
            }
        }

        /// <summary>
        /// 判断模板是否重复
        /// </summary>
        /// <param name="queueTemplate"></param>
        /// <returns>-1 错误 0 不存在 >=1 存在</returns>
        public int IsExist(FS.SOC.HISFC.Assign.Models.QueueTemplate queueTemplate)
        {
            string sql = "";
            if (queueTemplate.QueueType == FS.SOC.HISFC.Assign.Models.EnumQueueType.Custom)
            {
                if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryCountByCustom", ref sql) == -1)
                {
                    this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QueryCountByCustom]";
                    return -1;
                }

                sql = string.Format(sql, queueTemplate.ID, queueTemplate.AssignNurse.ID, (int)queueTemplate.WeekDay, queueTemplate.Noon.ID, queueTemplate.SRoom.ID, queueTemplate.Console.ID);

            }
            else if (queueTemplate.QueueType == FS.SOC.HISFC.Assign.Models.EnumQueueType.Doctor)
            {
                if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryCountByDoctor", ref sql) == -1)
                {
                    this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QueryCountByDoctor]";
                    return -1;
                }

                sql = string.Format(sql, queueTemplate.ID, queueTemplate.AssignNurse.ID, (int)queueTemplate.WeekDay, queueTemplate.Noon.ID, queueTemplate.Doctor.ID);
            }
            else if (queueTemplate.QueueType == FS.SOC.HISFC.Assign.Models.EnumQueueType.RegLevel)
            {
                if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryCountByRegLevel", ref sql) == -1)
                {
                    this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QueryCountByRegLevel]";
                    return -1;
                }

                sql = string.Format(sql, queueTemplate.ID, queueTemplate.AssignNurse.ID, (int)queueTemplate.WeekDay, queueTemplate.Noon.ID, queueTemplate.RegLevel.ID, queueTemplate.AssignDept.ID);
            }

            if (this.ExecQuery(sql) > 0)
            {
                if (this.Reader != null&&this.Reader.Read())
                {
                    return FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
            }

            return 0;
        }

        /// <summary>
        /// 判断是否存在同名模板
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public int IsExistQueueName(FS.SOC.HISFC.Assign.Models.QueueTemplate queue)
        {
            string sql = "";
            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryCountByQueueName", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错！[SOC.Assign.QueueTemplate.QueryCountByQueueName]";
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

        #endregion

        #region 多条查询

        /// <summary>
        /// 按护士站id查询分诊模板队列信息（全部）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <returns></returns>
        public ArrayList QueryAllByNurseID(string nurseID)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryByNurse.All", ref where) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QueryByNurse.All]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID);
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
        public ArrayList QueryValidByNurseID(string nurseID)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryByNurse.Valid", ref where) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QueryByNurse.Valid]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID);
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
        /// 按护士站id查询分诊模板队列信息（全部）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <returns></returns>
        public ArrayList QueryAllByNurseID(string nurseID,DayOfWeek weekDay)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryByNurse.WeekDay", ref where) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QueryByNurse.WeekDay]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID,(int)weekDay);
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
        public ArrayList QueryValidByNurseID(string nurseID, DayOfWeek weekDay)
        {
            string sql = "", where = "";

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QuerySql", ref sql) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QuerySql]";
                return null;
            }

            if (this.Sql.GetSql("SOC.Assign.QueueTemplate.QueryByNurse.WeekDay.Valid", ref where) == -1)
            {
                this.Err = "查询SQL语句出错![SOC.Assign.QueueTemplate.QueryByNurse.WeekDay.Valid]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, (int)weekDay);
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

        #endregion

        #region 私有方法

        protected string[] myGetParmQueueTemplate(FS.SOC.HISFC.Assign.Models.QueueTemplate obj)
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
									obj.QueueDate.ToString(),//队列时间9
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
                                    ((int)obj.WeekDay).ToString(),//存储星期几20
                                    obj.PatientConditionType//患者病情级别
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

            ArrayList al= new ArrayList();

            if (this.Reader != null)
            {
                try
                {
                    FS.SOC.HISFC.Assign.Models.QueueTemplate queueTemplate = null;
                    while (this.Reader.Read())
                    {
                        queueTemplate = new FS.SOC.HISFC.Assign.Models.QueueTemplate();

                        //所属护士站
                        queueTemplate.AssignNurse.ID = this.Reader[0].ToString();
                        //队列代码
                        queueTemplate.ID= this.Reader[1].ToString();
                        //队列名称
                        queueTemplate .Name= this.Reader[2].ToString();
                        //午别代码
                        queueTemplate .Noon.ID= this.Reader[3].ToString();

                        //队列类型[2007/03/27]
                        queueTemplate.QueueType = EnumHelper.Current.GetEnum<FS.SOC.HISFC.Assign.Models.EnumQueueType>(this.Reader[4].ToString());

                        //显示顺序
                        queueTemplate.Order = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
                        //是否有效
                        queueTemplate.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
                        //备注
                        queueTemplate.Memo = this.Reader[7].ToString();
                        //操作员
                        queueTemplate.Oper.ID = this.Reader[8].ToString();
                        //操作时间
                        queueTemplate.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
                        //队列日期
                        queueTemplate.QueueDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
                        //看诊医生
                        queueTemplate.Doctor.ID = this.Reader[11].ToString();
                        //诊室
                        queueTemplate.SRoom.ID = this.Reader[12].ToString();
                        queueTemplate.SRoom.Name = this.Reader[13].ToString();
                        //诊台
                        queueTemplate.Console.ID = this.Reader[14].ToString();
                        queueTemplate.Console.Name = this.Reader[15].ToString();
                        //专家标志
                        queueTemplate.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[16]);
                        //分诊科室
                        queueTemplate.AssignDept.ID = this.Reader[17].ToString();
                        queueTemplate.AssignDept.Name = this.Reader[18].ToString();
                        queueTemplate.WaitingCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19]);
                        queueTemplate.RegLevel.ID = this.Reader[20].ToString();
                        queueTemplate.RegLevel.Name = this.Reader[21].ToString();
                        queueTemplate.WeekDay = EnumHelper.Current.GetEnum<DayOfWeek>(this.Reader[22].ToString()); //星期几
                        if (this.Reader.FieldCount > 23)
                        {
                            queueTemplate.PatientConditionType = this.Reader[23].ToString();//患者病情级别
                        }
                        al.Add(queueTemplate);
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
