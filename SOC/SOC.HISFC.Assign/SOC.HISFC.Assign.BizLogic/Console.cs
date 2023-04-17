using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizLogic
{
    /// <summary>
    /// [功能描述: 诊台管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public  class Console:FS.FrameWork.Management.Database
    {
        #region 单条操作

        /// <summary>
        /// 获取诊室ID
        /// </summary>
        /// <returns></returns>
        public string GetConsoleID()
        {
            return this.GetSequence("Nurse.Seat.GetSeq");
        }


        /// <summary>
        /// 获取诊室信息
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Seat GetConsole(string consoleID)
        {
            string strSql = "";
            if (this.Sql.GetSql("SOC.Assign.Console.GetConsoleByID", ref strSql) == -1)
            {
                strSql = @"SELECT 
	                                    CONSOLE_CODE,
	                                    CONSOLE_NAME,
	                                    INPUT_CODE,
	                                    ROOM_CODE,
	                                    ROOM_NAME,
	                                    VALID_FLAG,
	                                    REMARK,
	                                    OPER_CODE,
	                                    OPER_DATE,
	                                    CURRENT_COUNT
                                    FROM MET_NUO_CONSOLE
                                    WHERE CONSOLE_CODE='{0}'
";
            }
            try
            {
                strSql = string.Format(strSql, consoleID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }


            ArrayList al = this.myGetInfo(strSql);
            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.HISFC.Models.Nurse.Seat();
            }
            else
            {
                return al[0] as FS.HISFC.Models.Nurse.Seat;
            }
        }

        /// <summary>
        /// 插入一条新的诊台信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Nurse.Seat info)
        {
            string sql = "";

            if (this.Sql.GetSql("Nurse.Seat.Insert", ref sql) == -1) return -1;

            try
            {
                if (string.IsNullOrEmpty(info.ID))
                {
                    info.ID = this.GetConsoleID();
                }

                sql = string.Format(sql, info.ID, info.Name, info.PRoom.InputCode, info.PRoom.ID, info.PRoom.Name,
                    info.PRoom.IsValid, info.Memo, info.Oper.ID, info.Oper.OperTime);
            }
            catch (Exception e)
            {
                this.Err = "转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.Nurse.Seat info)
        {
            string sql = "";

            if (this.Sql.GetSql("Nurse.Seat.Update", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, info.ID, info.Name, info.PRoom.InputCode, info.PRoom.ID, info.PRoom.Name,
                    info.PRoom.IsValid, info.Memo, info.Oper.ID, info.Oper.OperTime);
            }
            catch (Exception e)
            {
                this.Err = "转换出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 根据诊台号码删除
        /// </summary>
        /// <param name="strId"></param> 
        /// <returns></returns>
        public int DeleteByConsole(string strId)
        {
            string strSql = "";
            if (this.Sql.GetSql("Nurse.Seat.Delete.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, strId);
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
        /// 根据诊室号码删除若干信息
        /// </summary>
        /// <param name="roomCode"></param>
        /// <returns></returns>
        public int DeleteByRoom(string roomCode)
        {
            string strSql = "";
            if (this.Sql.GetSql("Nurse.Seat.Delete.2", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, roomCode);
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
        /// 查询要删除的诊台是否被队列维护
        /// </summary>
        /// <param name="roomID">诊台</param>
        /// <param name="strDate">系统时间</param>
        /// <returns></returns>
        public int IsExsitUsedByQueue(string consoleID)
        {
            string strsql = "";
            if (this.Sql.GetSql("Nurse.Seat.GetConsoleUsed", ref strsql) == -1)
            {
                this.Err = "得到Nurse.Seat.GetConsoleUsed失败";
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, consoleID,this.GetSysDate());

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
        }

        /// <summary>
        /// 查询met_nuo_assignrecord中是否有符合条件的诊台是否在用
        /// </summary>
        /// <param name="roomID">诊台代码</param>
        /// <returns></returns>
        public int IsExistUsedByAssign(string consoleID)
        {
            string strsql = string.Empty;

            if (this.Sql.GetSql("Nurse.Seat.GetRoomByConsoleID", ref strsql) == -1)
            {
                this.Err = "得到Nurse.Seat.GetRoomByConsoleID失败";
                return -1;
            }

            try
            {
                strsql = string.Format(strsql, consoleID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));

        }

        /// <summary>
        /// 更新诊台中的正在看诊的数量
        /// </summary>
        /// <param name="consoleCode"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int UpdateArrvieNum(string consoleCode, int num)
        {
            string sql = "";

            if (this.Sql.GetSql("Nurse.Seat.Update.1", ref sql) == -1) return -1;

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

        #region 多条操作

        /// <summary>
        /// 根据诊室号码获取诊台
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public ArrayList Query(string roomNo)
        {
            ArrayList al = new ArrayList();
            string strSQL;
            string strWhere = "";
            strSQL = this.getSql();
            if (this.Sql.GetSql("Nurse.Seat.Query.1", ref strWhere) == -1) return null;
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, roomNo);
            al = this.myGetInfo(strSQL);
            return al;
        }


        /// <summary>
        /// 查询科室对应的诊室和诊台信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryRoomAndConsoleByDept(string deptCode)
        {
            string sql = @"SELECT   t.DEPT_CODE, --分诊科室
                                       t.ROOM_ID, --诊室代码
                                       t.ROOM_NAME, --诊室名称
                                       t.INPUT_CODE, --助记码
                                       t.VALID_FLAG, --1有效/0无效
                                       t.SORT_ID, --显示顺序
                                       f.console_code,--诊台编码
                                       f.console_name --诊台名称
                                FROM MET_NUO_CONSOLE f,met_nuo_room t
                                where t.DEPT_CODE = '{0}'-- 分诊科室 
                                and t.VALID_FLAG='1' 
                                and f.room_code = t.room_id
                                and f.valid_flag =  1
                                order by t.sort_id,t.room_id,f.console_code";

            try
            {
                sql = string.Format(sql, deptCode);

                if (this.ExecQuery(sql) < 0)
                {
                    this.Err = "查询诊室失败，" + this.Err;
                    this.ErrCode = this.ErrCode;
                    this.WriteErr();
                    return null;
                }

                ArrayList al = new ArrayList();
                FS.HISFC.Models.Nurse.Seat seat;
                while (this.Reader.Read())
                {
                    seat = new FS.HISFC.Models.Nurse.Seat();
                    seat.PRoom.Dept.ID = Reader[0].ToString();
                    seat.PRoom.ID = Reader[1].ToString();
                    seat.PRoom.Name = Reader[2].ToString();
                    seat.PRoom.InputCode = Reader[3].ToString();
                    seat.PRoom.IsValid = Reader[4].ToString();
                    seat.PRoom.Sort = FS.FrameWork.Function.NConvert.ToInt32(Reader[5]);
                    seat.ID = Reader[6].ToString();
                    seat.Name = Reader[7].ToString();

                    al.Add(seat);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "查询诊室信息赋值错误" + ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 根据诊室号码获取有效诊台
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public ArrayList QueryValid(string roomNo)
        {
            ArrayList al = new ArrayList();
            string strSQL;
            string strWhere = "";
            strSQL = this.getSql();
            if (this.Sql.GetSql("Nurse.Seat.Query.2", ref strWhere) == -1)
            {
                strSQL = @"
                        where room_code = '{0}'
                        and valid_flag =  1
                        order by CONSOLE_CODE
                    ";
            }
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, roomNo);
            al = this.myGetInfo(strSQL);
            return al;
        }

        /// <summary>
        /// 查找除自身外诊台
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="consoleid"></param>
        /// <returns></returns>
        public ArrayList QueryOtherValid(string roomNo, string consoleid)
        {
            ArrayList al = new ArrayList();
            string strSQL;
            string strWhere = "";
            strSQL = this.getSql();
            if (this.Sql.GetSql("Nurse.Seat.Query.3", ref strWhere) == -1)
            {
                strSQL = @"
                        where room_code = '{0}'
                        and valid_flag =  1
                        and console_code<>'{1}'
                        order by CONSOLE_CODE
                    ";
            }
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, roomNo, consoleid);
            al = this.myGetInfo(strSQL);
            return al;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 获取基本SQL语句
        /// </summary>
        /// <returns></returns>
        private string getSql()
        {
            string strSql = "";
            if (this.Sql.GetSql("Nurse.Seat.Query", ref strSql) == -1) return null;
            return strSql;
        }

        /// <summary>
        /// 根据SQL,获取实体数组
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        private ArrayList myGetInfo(string sql)
        {
            //执行查询语句
            if (this.ExecQuery(sql) == -1)
            {
                this.ErrCode = "-1";
                return null;
            }

            if (this.Reader == null)
            {
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                FS.HISFC.Models.Nurse.Seat info=null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Nurse.Seat();

                    info.ID = this.Reader[0].ToString();//诊台代码
                    info.Name = this.Reader[1].ToString();//诊台名称
                    info.PRoom.InputCode = this.Reader[2].ToString();//输入码
                    info.PRoom.ID = this.Reader[3].ToString();//诊室代码
                    info.PRoom.Name = this.Reader[4].ToString();//诊室名称
                    info.PRoom.IsValid = this.Reader[5].ToString();//1有效/0无效
                    info.PRoom.Memo = this.Reader[6].ToString();//备注
                    info.Oper.ID = this.Reader[7].ToString();//操作员
                    info.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);//操作时间
                    info.CurrentCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
                    if (this.Reader.FieldCount > 10)
                    {
                        info.PRoom.Nurse.ID = this.Reader[10].ToString();//护士站
                        info.PRoom.Dept.ID = this.Reader[11].ToString();//分诊台
                    }

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = "-1";
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

        #endregion
    }
}
