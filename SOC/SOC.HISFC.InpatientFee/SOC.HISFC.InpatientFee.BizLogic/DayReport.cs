using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.InpatientFee.BizLogic
{
    public  class DayReport:FS.FrameWork.Management.Database
    {
        #region 住院收费日结

        /// <summary>
        /// 处理操作员日结
        /// </summary>
        /// <param name="operCode">工号</param>
        /// <param name="balancer">日结人</param>
        /// <param name="beginDate">上次日结时间</param>
        /// <param name="endDate">本次日结截至时间</param>
        /// <returns>-1出错，1成功</returns>
        public int DealInvoiceDayBalance(string operCode, string balancer, DateTime beginTime, DateTime endTime)
        {
          //  {525FF689-3B49-4c45-9F44-D66D2DC7E453}
            string strSQL = string.Empty;
            string strReturn = string.Empty;
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
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.DayBalance", ref strSQL) == -1)
            {
                strSQL = FS.SOC.HISFC.InpatientFee.Data.AbstractDayReport.Current.Insert;
            }
            try
            {
                strSQL = string.Format(strSQL, operCode, balancer, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"),hospitalid,hospitalname);
                if (this.ExecEvent(strSQL, ref strReturn) == -1)
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
        public int DealInvoiceDayBalanceCancel(string operCode, string balanceNO)
        {
            string strReturn = "";
            string strSql = "";

            if (Sql.GetSql("Fee.Daybalance.Inpatient.DayBalanceCancel", ref strSql) == -1)
            {
                strSql = FS.SOC.HISFC.InpatientFee.Data.AbstractDayReport.Current.Delete;
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

        #region 住院收费日结审核

        /// <summary>
        /// 收费日结审核_预交金
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateInpatientDayBalancePrepayForCheck(string operID, string balanceNO)
        {
            string strSql = @"update fin_ipb_prepaystat
                                 set check_oper = '{0}',
                                     check_date = sysdate,
                                     check_flag = '1'
                               where static_no = '{1}'
                                 and check_flag = '0'";

            return UpdateSingleTable("Fee.Inpatient.DayReport.Update.Prepay.Check", operID, balanceNO);
        }

        /// <summary>
        /// 取消收费日结审核_预交金
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateInpatientDayBalancePrepayForCancelCheck(string operID, string balanceNO)
        {
            string strSql = @"update fin_ipb_prepaystat
                                 set check_oper = null,
                                     check_date = null,
                                     check_flag = '0'
                               where check_oper = '{0}'
                                 and static_no = '{1}'
                                 and check_flag = '1'";

            return UpdateSingleTable("Fee.Inpatient.DayReport.Update.Prepay.CancelCheck", operID, balanceNO);
        }

        /// <summary>
        /// 收费日结审核_结算发票
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateInpatientDayBalanceInvoiceForCheck(string operID, string balanceNO)
        {
            string strSql = @"update fin_ipb_daybalance
                                 set check_opcd = '{0}',
                                     check_date = sysdate,
                                     check_flag = '1'
                               where balance_no = '{1}'
                                 and check_flag = '0'";

            return UpdateSingleTable("Fee.Inpatient.DayReport.Update.Invoice.Check", operID, balanceNO);
        }

        /// <summary>
        /// 取消收费日结审核_结算发票
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateInpatientDayBalanceInvoiceForCancelCheck(string operID, string balanceNO)
        {
            string strSql = @"update fin_ipb_daybalance
                                 set check_opcd = null,
                                     check_date = null,
                                     check_flag = '0'
                               where check_opcd = '{0}'
                                 and balance_no = '{1}'
                                 and check_flag = '1'";

            return UpdateSingleTable("Fee.Inpatient.DayReport.Update.Invoice.CancelCheck", operID, balanceNO);
        }

        /// <summary>
        /// 按日期查询已日结数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryBalanceListForCheck(DateTime date)
        {
            string strSql = @"select balance_no,
                                     to_char(t.oper_date, 'yyyy-MM-dd hh24:mi:ss') || ' [' ||
                                     (select e.empl_name
                                        from com_employee e
                                       where e.empl_code = t.oper_code) || ']',
                                     check_flag
                                from fin_ipb_daybalance t
                               where to_char(t.oper_date, 'yyyy-MM-dd') = '{0}'
                               order by oper_date";

            strSql = string.Format(strSql, date.ToString("yyyy-MM-dd"));
            if (this.ExecQuery(strSql) < 0)
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

        #region 查询收费日结信息

        /// <summary>
        /// 根据收款员工号获取上次日结时间(1：成功/0：没有作过日结/-1：失败)
        /// </summary>
        /// <param name="employee">操作员</param>
        /// <param name="lastDate">返回上次日结截止时间</param>
        /// <returns>1：成功/0：没有作过日结/-1：失败</returns>
        public int GetLastBalanceDate(string operCode, ref DateTime lastDate, ref string balanceNO)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractDayReport.Current.SelectLastDate, operCode);
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
        /// 按照月份查询已日结数据
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryBalanceList(DateTime month, string operCode)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractDayReport.Current.SelectByMonth, month.ToString("yyyy-MM"),  operCode);
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

            return null; ;
        }

        public int GetBalance(string balanceNO, ref DateTime dtBeginTime, ref DateTime dtEndTime, ref string opeCode)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractDayReport.Current.SelectDateByBalanceNO, balanceNO);
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
    }
}
