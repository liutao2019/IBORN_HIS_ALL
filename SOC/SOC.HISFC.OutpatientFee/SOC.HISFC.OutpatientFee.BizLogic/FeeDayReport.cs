using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.OutpatientFee.BizLogic
{    
    /// <summary>
    /// [功能描述: 门诊费用日结管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-5]<br></br>
    /// </summary>
    public class FeeDayReport : FS.FrameWork.Management.Database
    {

        #region 收费日结

        /// <summary>
        /// 处理操作员日结
        /// </summary>
        /// <param name="operCode">工号</param>
        /// <param name="balancer">日结人</param>
        /// <param name="beginDate">上次日结时间</param>
        /// <param name="endDate">本次日结截至时间</param>
        /// <returns>-1出错，1成功</returns>
        public int DealFeeDayBalance(string operCode, string balancer, DateTime beginDate, DateTime endDate)
        {
            string strReturn = "";
            string strSql = "";
            /*strSql = "pkg_rep.prc_opb_daybalance,opercode,22,1,{0}," +
                "begindate,22,1,{1}," +
                "endate,22,1,{2}," +
                "Par_ErrCode,13,2,1," +
                "Par_ErrText,22,2,1";
             * 
             * 
            */
            //{525FF689-3B49-4c45-9F44-D66D2DC7E453}
            FS.HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new Employee();
            }
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new Department();
            }
            string hospitalid = dept.HospitalID;
            string hospitalname = dept.HospitalName;
            if (Sql.GetCommonSql("Fee.Outpatient.Procedurce.DayBalance.New", ref strSql) == -1)
            {
                strSql = FS.SOC.HISFC.OutpatientFee.Data.AbstractFeeDayReport.Current.Insert;
            }

            try
            {

                strSql = string.Format(strSql, operCode, balancer, beginDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"),hospitalid,hospitalname);
                if (this.ExecEvent(strSql, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[0]) == -1)
                {
                    this.Err = str[1];
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 处理操作员日结
        /// </summary>
        /// <param name="operCode">工号</param>
        /// <param name="balancer">日结人</param>
        /// <param name="beginDate">上次日结时间</param>
        /// <param name="endDate">本次日结截至时间</param>
        /// <returns>-1出错，1成功</returns>
        public int DealFeeDayBalanceAndHospital(string operCode, string balancer, DateTime beginDate, DateTime endDate,string hospitalid,string hospitalname)
        {
            string strReturn = "";
            string strSql = "";
            /*strSql = "pkg_rep.prc_opb_daybalance,opercode,22,1,{0}," +
                "begindate,22,1,{1}," +
                "endate,22,1,{2}," +
                "Par_ErrCode,13,2,1," +
                "Par_ErrText,22,2,1";
            */
            if (Sql.GetCommonSql("Fee.Outpatient.Procedurce.DayBalance.New", ref strSql) == -1)
            {
                strSql = FS.SOC.HISFC.OutpatientFee.Data.AbstractFeeDayReport.Current.Insert;
            }

            try
            {

                strSql = string.Format(strSql, operCode, balancer, beginDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"),hospitalid,hospitalname);
                if (this.ExecEvent(strSql, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[0]) == -1)
                {
                    this.Err = str[1];
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 取消日结
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int DealFeeDayBalanceCancel(string operCode, string balanceNO)
        {
            string strReturn = "";
            string strSql = "";

            if (Sql.GetCommonSql("Fee.Outpatient.Procedurce.DayBalanceCancel.New", ref strSql) == -1)
            {
                strSql = FS.SOC.HISFC.OutpatientFee.Data.AbstractFeeDayReport.Current.Delete;
            }

            try
            {
                strSql = string.Format(strSql, operCode, balanceNO);
                if (this.ExecEvent(strSql, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[0]) == -1)
                {
                    this.Err = str[1];
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        #endregion 

        #region 查询收费日结信息

        /// <summary>
        /// 根据收款员工号获取上次日结时间(1：成功/0：没有作过日结/-1：失败) 
        /// </summary>
        /// <param name="employee">操作员</param>
        /// <param name="lastDate">返回上次日结截止时间</param>
        /// <returns>1：成功/0：没有作过日结/-1：失败</returns>
        public int GetLastBalanceDate(string operCode,string hospital_id ,ref DateTime lastDate, ref string balanceNO)
        {
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractFeeDayReport.Current.SelectLastDate, operCode, hospital_id);//{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            if (this.ExecQuery(sql) < 0)
            {
                return -1;
            }
            if (this.Reader != null)
            {
                try
                {
                    if (this.Reader.Read())
                    {
                        lastDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0].ToString());
                        balanceNO = this.Reader[1].ToString();
                    }
                }
                catch (Exception e)
                {
                    this.Err = e.Message + this.Err;
                    return -1;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return 1;

        }


        /// <summary>
        /// 根据收款员工号获取未日结时间(1：成功/0：没有作过日结/-1：失败)
        /// </summary>
        /// <param name="employee">操作员</param>
        /// <param name="lastDate">返回上次日结截止时间</param>
        /// <returns>1：成功/0：没有作过日结/-1：失败</returns>
        public int GetLastFeeDate(string operCode, ref DateTime lastDate)
        {
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractFeeDayReport.Current.SelectLastFeeDate, operCode);
            if (this.ExecQuery(sql) < 0)
            {
                return -1;
            }
            if (this.Reader != null)
            {
                try
                {
                    if (this.Reader.Read())
                    {
                        lastDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    this.Err = e.Message + this.Err;
                    return -1;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return 1;

        }

        /// <summary>
        /// 按照月份查询已日结数据
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryBalanceList(DateTime month, string operCode, string hospitalid) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        {

            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractFeeDayReport.Current.SelectByMonth, month.ToString("yyyy-MM"), operCode, hospitalid);//{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            if (this.ExecQuery(sql) < 0)
            {
                return null;
            }
            if (this.Reader != null)
            {
                List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
                try
                {
                    while (this.Reader.Read())
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        obj.ID = this.Reader[0].ToString();
                        obj.Name = this.Reader[1].ToString();
                        obj.Memo = this.Reader[2].ToString();
                        list.Add(obj);
                    }

                    return list;
                }
                catch (Exception e)
                {
                    this.Err = e.Message + this.Err;
                    return null;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return null;
        }

        public int GetBalance(string balanceNO, ref DateTime dtBeginTime, ref DateTime dtEndTime, ref string opeCode)
        {
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractFeeDayReport.Current.SelectDateByBalanceNO, balanceNO);
            if (this.ExecQuery(sql) < 0)
            {
                return -1;
            }
            if (this.Reader != null)
            {
                try
                {
                    if (this.Reader.Read())
                    {
                        dtBeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        dtEndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());
                        opeCode = this.Reader[3].ToString();
                    }
                }
                catch (Exception e)
                {
                    this.Err = e.Message + this.Err;
                    return -1;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return 1;
        }
            
        #endregion

        #region 收费日结审核

        /// <summary>
        /// 收费日结审核
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateOutpatientDayBalanceForCheck(string operID, string balanceNO)
        {
            string sql;

            sql = @"update fin_opb_dayreport
                       set check_opcd = '{0}',
                           check_date = sysdate,
                           check_flag = '1'
                     where balance_no = '{1}'
                       and check_flag = '0'";

            return UpdateSingleTable("Fee.Outpatient.DayReport.Update.Check", operID, balanceNO);
        }

        /// <summary>
        /// 取消收费日结审核
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateOutpatientDayBalanceForCancelCheck(string operID, string balanceNO)
        {
            string sql;

            sql = @"update fin_opb_dayreport
                       set check_opcd = null,
                           check_date = null,
                           check_flag = '0'
                     where check_opcd = '{0}'
                       and balance_no = '{1}'
                       and check_flag = '1'";

            return UpdateSingleTable("Fee.Outpatient.DayReport.Update.CancelCheck", operID, balanceNO);
        }

        /// <summary>
        /// 按日期查询已日结数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryBalanceListForCheck(DateTime date)
        {
            string sql = @"select balance_no,
                                  to_char(t.oper_date, 'yyyy-MM-dd hh24:mi:ss') || ' [' ||
                                  (select e.empl_name
                                     from com_employee e
                                    where e.empl_code = t.oper_code) || ']',
                                  check_flag
                             from fin_opb_dayreport t
                            where to_char(t.oper_date, 'yyyy-MM-dd') = '{0}'
                            order by oper_date";
            sql = string.Format(sql, date.ToString("yyyy-MM-dd"));
            if (this.ExecQuery(sql) < 0)
            {
                return null;
            }
            if (this.Reader != null)
            {
                List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
                try
                {
                    while (this.Reader.Read())
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        obj.ID = this.Reader[0].ToString();
                        obj.Name = this.Reader[1].ToString();
                        obj.Memo = this.Reader[2].ToString();
                        list.Add(obj);
                    }

                    return list;
                }
                catch (Exception e)
                {
                    this.Err = e.Message + this.Err;
                    return null;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return null;
        }

        #endregion
    }
}
