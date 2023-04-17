using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.Order.OutPatient
{
    /// <summary>
    /// [功能描述：门诊诊断证明和病假条业务类]
    /// [创 建 者：]
    /// [创建时间：]
    /// </summary>
    public class DiagExtend : FS.FrameWork.Management.Database
    {
        public DiagExtend()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 内部私有方法

        /// <summary>
        /// 根据SQL查询诊断证明和病假条信息
        /// </summary>
        /// <param name="wheSql">Whe子句</param>
        /// <returns>成功返回诊断证明和病假条信息实体 失败返回null</returns>
        private ArrayList QueryDiagExtends(string wheSql, params string[] args)
        {
            string strSql = "";
            string selSql = "";
            //取SELECT子句
            selSql = this.GetCommonSqlForSelectAllDiagExtends();

            //取WHERE子句
            try
            {
                if (!string.IsNullOrEmpty(wheSql))
                {
                    if (this.Sql.GetCommonSql(wheSql, ref wheSql) == -1)
                    {
                        this.Err = "没有找到" + wheSql + "字段!";
                        return null;
                    }
                    strSql = selSql + "\r\n" + wheSql;
                    strSql = string.Format(strSql, args);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            ArrayList diagExtendList = new ArrayList();

            //执行Sql语句 
            try
            {
                this.ExecQuery(strSql);

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend = new FS.HISFC.Models.Order.OutPatient.DiagExtend();
                    diagExtend.ClinicCode = this.Reader[0].ToString();//门诊流水号
                    diagExtend.CardNo = this.Reader[1].ToString();//门诊号
                    diagExtend.ProveNo = this.Reader[2].ToString();//门诊诊断证明书序号
                    diagExtend.LeaveNo = this.Reader[3].ToString();//门诊病假条序号
                    diagExtend.LeaveDays = this.Reader[4].ToString();//门诊病假天数
                    diagExtend.LeaveStart = this.Reader[5].ToString();//门诊病假开始时间
                    diagExtend.LeaveEnd = this.Reader[6].ToString();//门诊病假结束时间
                    diagExtend.ValidFlag = this.Reader[7].ToString();//有效状态
                    diagExtend.ProvePrintDate = this.Reader[8].ToString();//门诊诊断证明打印时间
                    diagExtend.LeavePrintDate = this.Reader[9].ToString();//门诊病假建议书打印时间
                    diagExtend.CaseMain = this.Reader[10].ToString();//主诉
                    diagExtend.CaseNow = this.Reader[11].ToString();//现病史
                    diagExtend.Opinions = this.Reader[12].ToString();//治疗意见
                    diagExtend.LeaveType = this.Reader[13].ToString();//请假类型
                    diagExtendList.Add(diagExtend);
                }
                return diagExtendList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新单表操作
        /// </summary>
        /// <param name="sqlIndex">SQL语句索引</param>
        /// <param name="args">参数</param>
        /// <returns>成功: >= 1 失败 -1 没有更新到数据 0</returns>
        private int UpdateSingleTable(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update语句

            //获得Where语句
            if (this.Sql.GetCommonSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "没有找到索引为:" + sqlIndex + "的SQL语句";

                return -1;
            }
            sql = string.Format(sql, args);
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 获得诊断证明和病假条信息字符串数组
        /// </summary>
        /// <param name="prepay">诊断证明和病假条信息实体</param>
        /// <returns>成功: 诊断证明和病假条信息字符串数组 失败: null</returns>
        private string[] GetDiagExtendParams(FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend)
        {
            string[] args ={
                               //门诊流水号
                               diagExtend.ClinicCode,
                               //门诊号
                               diagExtend.CardNo,
							   //诊断证明序号
							   diagExtend.ProveNo,
							   //病假条序号
							   diagExtend.LeaveNo,
                                //病假天数
							   diagExtend.LeaveDays,
                                //病假开始时间
							   diagExtend.LeaveStart,
                                 //病假结束时间
							   diagExtend.LeaveEnd,
                               //有效状态
							   diagExtend.ValidFlag,
                               //诊断证明书打印时间
                               diagExtend.ProvePrintDate,
                               //病假建议打印时间
                               diagExtend.LeavePrintDate,
                               //主诉
                               diagExtend.CaseMain,
                               //现病史
                               diagExtend.CaseNow,
                               //治疗意见
                               diagExtend.Opinions,
                               //请假类型
                               diagExtend.LeaveType
						   };

            return args;
        }

        /// <summary>
        /// 获取检索MET_CAS_DiagExtend的全部数据的sql
        /// </summary>
        /// <returns></returns>
        private string GetCommonSqlForSelectAllDiagExtends()
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Extend.SelectAllDiagExtend", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }

        #endregion

        #region 增删改

        /// <summary>
        /// 插入诊断证明和病假条信息
        /// </summary>
        /// <param name="prepay">诊断证明和病假条信息实体</param>
        /// <returns>成功: 1 失败 -1 没有插入数据 0</returns>
        public int InsertDiagExtend(FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend)
        {
            string[] parms = new string[7];
            parms = this.GetDiagExtendParams(diagExtend);
            return this.UpdateSingleTable("Order.OutPatient.Extend.InsertDiagExtend", parms);
        }

        /// <summary>
        /// 更新诊断证明和病假条信息
        /// </summary>
        /// <param name="prepay">诊断证明和病假条信息实体</param>
        /// <returns></returns>
        public int UpdateDiagExtend(FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend)
        {
            return this.UpdateSingleTable("Order.OutPatient.Extend.UpdateDiagExtend", this.GetDiagExtendParams(diagExtend));
        }

        /// <summary>
        /// 更新诊断证明和病假条信息通过病假序号
        /// </summary>
        /// <param name="prepay">诊断证明和病假条信息实体</param>
        /// <returns></returns>
        public int UpdateDiagExtendByLeaveNo(FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend)
        {
            return this.UpdateSingleTable("Order.OutPatient.Extend.UpdateDiagExtendByLeaveNo", this.GetDiagExtendParams(diagExtend));
        }


        /// <summary>
        /// 删除诊断证明和病假条信息
        /// </summary>
        /// <param name="prepay">诊断证明和病假条信息实体</param>
        /// <returns></returns>
        public int DeleteDiagExtend(FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend)
        {
            return this.UpdateSingleTable("Order.OutPatient.Extend.DeleteDiagExtend", this.GetDiagExtendParams(diagExtend));
        }

        #endregion

        #region 查询函数

        /// <summary>
        /// 根据门诊流水号、门诊号取诊断证明和病假条信息
        /// </summary>
        /// <param name="prepay">诊断证明和病假条信息实体</param>
        /// <return></return></returns>
        public FS.HISFC.Models.Order.OutPatient.DiagExtend QueryByClinicCodCardNo(string clinicCode, string cardNo)
        {
            ArrayList al = this.QueryDiagExtends("Order.OutPatient.Extend.QueryByClinicCodeCardNo", clinicCode, cardNo);

            if (al == null || al.Count == 0)
            {
                return null;
            }

            return al[0] as FS.HISFC.Models.Order.OutPatient.DiagExtend;
        }

        #endregion
    }
}
