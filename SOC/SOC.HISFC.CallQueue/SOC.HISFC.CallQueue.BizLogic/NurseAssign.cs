using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.CallQueue.BizLogic
{
    /// <summary>
    /// 护士分诊叫号业务层
    /// </summary>
    public class NurseAssign : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获得插入参数列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected object[] myGetParmInsertQueue(FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign)
        {
            object[] strParm ={	
								   nurseAssign.ID,//0
                                   nurseAssign.PatientID,
                                   nurseAssign.PatientSeeNO,
                                   nurseAssign.PatientCardNO,
                                   nurseAssign.PatientName,
                                   nurseAssign.PatientSex,//5
                                   nurseAssign.Dept.ID,
                                   nurseAssign.Dept.Name,
                                   nurseAssign.Nurse.ID,
                                   nurseAssign.Nurse.Name,
                                   nurseAssign.Room.ID,//10
                                   nurseAssign.Room.Name,
                                   nurseAssign.Console.ID,
                                   nurseAssign.Console.Name,
                                   nurseAssign.Noon.ID,
                                   nurseAssign.CallClass,//15
                                   nurseAssign.Name,
                                   nurseAssign.Memo,
                                   nurseAssign.Oper.ID,
                                   nurseAssign.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss")
							 };

            return strParm;

        }

        public int Insert(FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Nurse.CallQueue.Insert", ref strSQL) == -1)
            {
                this.Err = "找不到语句Nurse.CallQueue.Insert";
                this.WriteErr();
                return -1;
            }
            try
            {
                strSQL = String.Format(strSQL, this.myGetParmInsertQueue(nurseAssign));
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除队列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Nurse.CallQueue.Delete", ref strSQL) == -1)
            {
                this.Err = "找不到语句Nurse.CallQueue.Delete";
                this.WriteErr();
                return -1;
            }
            try
            {
                strSQL = String.Format(strSQL, id);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 根据患者删除对应的队列
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int DeleteByPatientID(string patientID)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Nurse.CallQueue.DeleteByPatientID", ref strSQL) == -1)
            {
                this.Err = "找不到语句Nurse.CallQueue.DeleteByPatientID";
                this.WriteErr();
                return -1;
            }
            try
            {
                strSQL = String.Format(strSQL, patientID);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 根据护士站和午别查找对应的叫号申请信息
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="noon"></param>
        /// <returns></returns>
        public List<FS.SOC.HISFC.CallQueue.Models.NurseAssign> Query(string nurseCode, FS.FrameWork.Models.NeuObject noon)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Nurse.CallQueue.Query.ByNurseCodeAndNoon", ref strSQL) == -1)
            {
                this.Err = "找不到语句Nurse.CallQueue.Query.ByNurseCodeAndNoon";
                this.WriteErr();
                return null;
            }
            strSQL = string.Format(strSQL, nurseCode, noon.ID);

            return this.query(strSQL);
        }

        /// <summary>
        /// 根据sql查询队列信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.SOC.HISFC.CallQueue.Models.NurseAssign> query(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "基本sql出错!" + sql;
                this.ErrCode = "基本sql出错!" + sql;
                return null;
            }

            List<FS.SOC.HISFC.CallQueue.Models.NurseAssign> list = null;
            try
            {
                list = new List<FS.SOC.HISFC.CallQueue.Models.NurseAssign>();
                FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign = null;
                while (this.Reader.Read())
                {
                    nurseAssign = new FS.SOC.HISFC.CallQueue.Models.NurseAssign();
                    nurseAssign.ID = this.Reader[0].ToString();//0
                    nurseAssign.PatientID = this.Reader[1].ToString();
                    nurseAssign.PatientSeeNO = this.Reader[2].ToString();
                    nurseAssign.PatientCardNO = this.Reader[3].ToString();
                    nurseAssign.PatientName = this.Reader[4].ToString();
                    nurseAssign.PatientSex = this.Reader[5].ToString();//5
                    nurseAssign.Dept.ID = this.Reader[6].ToString();
                    nurseAssign.Dept.Name = this.Reader[7].ToString();
                    nurseAssign.Nurse.ID = this.Reader[8].ToString();
                    nurseAssign.Nurse.Name = this.Reader[9].ToString();
                    nurseAssign.Room.ID = this.Reader[10].ToString();
                    nurseAssign.Room.Name = this.Reader[11].ToString();
                    nurseAssign.Console.ID = this.Reader[12].ToString();
                    nurseAssign.Console.Name = this.Reader[13].ToString();//10
                    nurseAssign.Noon.ID = this.Reader[14].ToString();
                    nurseAssign.CallClass = this.Reader[15].ToString();
                    nurseAssign.Name = this.Reader[16].ToString();
                    nurseAssign.Memo = this.Reader[17].ToString();
                    nurseAssign.Oper.ID = this.Reader[18].ToString();
                    nurseAssign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[19].ToString());//15

                    list.Add(nurseAssign);
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return list;
        }
    }
}
