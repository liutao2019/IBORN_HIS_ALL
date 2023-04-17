using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizLogic
{    
    /// <summary>
    /// [功能描述: 诊室管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Room : FS.FrameWork.Management.Database
    {
        private string GetSqlRoomInfo()
        {
            string strSql = "";
            if (this.Sql.GetSql("SOC.Assign.Room.SelectAll", ref strSql) == -1) return null;
            return strSql;
        }

        #region 多条操作

        /// <summary>
        /// 根据护理站代码和科室代码获取诊室列表
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <returns></returns>
        public ArrayList QueryRoomsByDept( string deptCode)
        {
            return this.QueryRoomsByDept("ALL", deptCode);
        }

        /// <summary>
        /// 根据护理站代码和科室代码获取诊室列表
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <returns></returns>
        public ArrayList QueryRoomsByDept(string NurseNo, string deptCode)
        {
            string strSql1 = "";
            string strSql2 = "";
            //获得项目明细的SQL语句
            strSql1 = this.GetSqlRoomInfo();
            if (this.Sql.GetSql("SOC.Assign.Room.QueryRoomsByNurseAndDept", ref strSql2) == -1) return null;
            strSql1 = strSql1 + " " + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, NurseNo, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return this.queryRooms(strSql1);
        }

        /// <summary>
        /// 获取护士站下其他诊室信息（排除本id）
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <param name="deptCode"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public ArrayList QueryOtherRoomsByDept(string NurseNo, string deptCode,string roomid)
        {
            string strSql1 = "";
            string strSql2 = "";
            //获得项目明细的SQL语句
            strSql1 = this.GetSqlRoomInfo();
            if (this.Sql.GetSql("SOC.Assign.Room.QueryOtherRoomsByDept", ref strSql2) == -1)
            {
                strSql2 = @" Where (NURSE_CELL_CODE = '{0}' or '{0}' = 'ALL') -- VARCHAR2(12) N   分诊科室
                                AND (DEPT_CODE = '{1}' OR '{1}' = 'ALL')
                                and ROOM_ID <> '{2}'
                                order by SORT_ID";
                //return null;
            }
            strSql1 = strSql1 + " " + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, NurseNo, deptCode, roomid);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return this.queryRooms(strSql1);
        }

        /// <summary>
        /// 根据护理站代码和科室代码获取诊室列表
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <returns></returns>
        public ArrayList QueryValidRoomsByDept(string deptCode)
        {
            return this.QueryValidRoomsByDept("ALL", deptCode);
        }

        /// <summary>
        /// 根据护理站代码和科室代码获取诊室列表
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <returns></returns>
        public ArrayList QueryValidRoomsByDept(string NurseNo, string deptCode)
        {
            string strSql1 = "";
            string strSql2 = "";
            //获得项目明细的SQL语句
            strSql1 = this.GetSqlRoomInfo();
            if (this.Sql.GetSql("SOC.Assign.Room.QueryValidRoomsByNurseAndDept", ref strSql2) == -1) return null;
            strSql1 = strSql1 + " " + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, NurseNo, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return this.queryRooms(strSql1);
        }

        /// <summary>
        /// 转化为实体
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList queryRooms(string strSql)
        {
            if (this.ExecQuery(strSql) < 0)
            {
                this.Err = "查询诊室失败，" + this.Err;
                this.ErrCode = this.ErrCode;
                this.WriteErr();
                return null;
            }

            ArrayList al = new ArrayList();
            FS.SOC.HISFC.Assign.Models.Room objRoom;
            try
            {
                while (this.Reader.Read())
                {
                    objRoom = new FS.SOC.HISFC.Assign.Models.Room();
                    objRoom.Dept.ID = this.Reader[0].ToString();//分诊科室
                    objRoom.ID = this.Reader[1].ToString();//诊室代码
                    objRoom.Name = this.Reader[2].ToString();//诊室名称
                    objRoom.InputCode = this.Reader[3].ToString();//助记码
                    objRoom.IsValid = this.Reader[4].ToString();//是否有效
                    objRoom.Sort = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//序号
                    objRoom.Oper.ID = this.Reader[6].ToString();//操作员
                    objRoom.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7]);//操作时间
                    objRoom.Nurse.ID = this.Reader[8].ToString();//分诊护士站
                    al.Add(objRoom);
                }
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
            return al;
        }

        #endregion

        #region 单条操作

        /// <summary>
        /// 获取诊室ID
        /// </summary>
        /// <returns></returns>
        public string GetRoomID()
        {
            return this.GetSequence("Nurse.Seat.GetSeq");
        }

        /// <summary>
        /// 获取诊室信息
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public FS.SOC.HISFC.Assign.Models.Room GetRoom(string roomID)
        {
            string strSql = "";
            if (this.Sql.GetSql("SOC.Assign.Room.GetRoomByID", ref strSql) == -1)
            {
                strSql = @"SELECT 
	                                    DEPT_CODE,
	                                    ROOM_ID,
	                                    ROOM_NAME,
	                                    INPUT_CODE,
	                                    VALID_FLAG,
	                                    SORT_ID,
	                                    OPER_CODE,
	                                    OPER_DATE,
	                                    NURSE_CELL_CODE
                                    FROM MET_NUO_ROOM
                                    WHERE ROOM_ID='{0}'";
            }
            try
            {
                strSql = string.Format(strSql, roomID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }


            ArrayList al= this.queryRooms(strSql);
            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.SOC.HISFC.Assign.Models.Room();
            }
            else
            {
                return al[0] as FS.SOC.HISFC.Assign.Models.Room;
            }
        }

        /// <summary>
        /// 插入一条
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Assign.Models.Room info)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            string[] strParam;
            if (this.Sql.GetSql("SOC.Assign.Room.Insert", ref strSQL) == -1)
            {
                this.Err = "没有找到字段=SOC.Assign.Room.Insert";
                return -1;
            }
            try
            {
                if (string.IsNullOrEmpty(info.ID))
                {
                    info.ID = this.GetRoomID();
                }

                strParam = this.myGetParmRoomInfo(info);
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
        /// 更新登记信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Update(FS.SOC.HISFC.Assign.Models.Room info)
        {
            string sql = "";
            string[] strParam;
            if (this.Sql.GetSql("SOC.Assign.Room.Update", ref sql) == -1) return -1;

            try
            {
                if (info.ID == null) return -1;
                strParam = this.myGetParmRoomInfo(info);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql, strParam);
        }

        /// <summary>
        /// 删除诊室信息(根据诊室代码)
        /// </summary>
        /// <param name="nurseNo"></param>
        /// <returns></returns>
        public int Delete(string roomID)
        {
            string strSql = "";
            if (this.Sql.GetSql("SOC.Assign.Room.Delete", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, roomID);
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
        /// 是否存在已经使用的队列
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        public int IsExistUsed(string roomID)
        {
            string strsql = "";
            if (this.Sql.GetSql("SOC.Assign.Room.IsExistUsed", ref strsql) == -1)
            {
                this.Err = "得到SOC.Assign.Room.IsExistUsed失败";
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, roomID);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
        }

        #endregion

        #region 私有方法

        protected string[] myGetParmRoomInfo(FS.SOC.HISFC.Assign.Models.Room obj)
        {
            string[] strParm ={	
								 obj.Dept.ID,
								 obj.ID,
								 obj.Name,
								 obj.InputCode,
								 obj.IsValid,
								 obj.Sort.ToString(),
								 obj.Oper.ID,
								 obj.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
								 obj.Nurse.ID
							 };

            return strParm;

        }

        #endregion
    }
}
