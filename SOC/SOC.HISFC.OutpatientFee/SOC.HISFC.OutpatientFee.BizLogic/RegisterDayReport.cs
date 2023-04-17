using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.BizLogic
{
    /// <summary>
    /// [功能描述: 挂号日结报表管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-5]<br></br>
    /// </summary>
    public class RegisterDayReport : FS.FrameWork.Management.Database
    {
        #region 挂号日结

        /// <summary>
        /// 挂号日结表头信息
        /// </summary>
        /// <param name="dayReport"></param>
        /// <returns></returns>
        public int InsertDayReport(FS.HISFC.Models.Registration.DayReport dayReport, ref string errText)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.DayReport.Insert.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, dayReport.ID, dayReport.BeginDate.ToString(), dayReport.EndDate.ToString(),
                    dayReport.SumCount, dayReport.SumRegFee, dayReport.SumChkFee, dayReport.SumDigFee,
                    dayReport.SumOthFee, dayReport.SumOwnCost, dayReport.SumPayCost, dayReport.SumPubCost,
                    dayReport.Oper.ID, dayReport.Oper.OperTime.ToString(), dayReport.Memo, dayReport.SumCardFee, dayReport.SumCaseFee);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.DayReport.Insert.2]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                errText = this.Err;
                return -1;
            }

            if (this.ExecNoQuery(sql) == -1)
            {
                errText = "执行Registration.DayReport.Insert.2出错!" + this.Err;
                return -1;
            }

            foreach (FS.HISFC.Models.Registration.DayDetail detail in dayReport.Details)
            {
                if (this.InsertDayReportDetail(detail, errText) == -1)
                {
                    errText = "插入挂号日结明细表出错" + this.Err;
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// 挂号日结费用明细
        /// </summary>
        /// <param name="dayDetail"></param>
        /// <returns></returns>
        public int InsertDayReportDetail(FS.HISFC.Models.Registration.DayDetail dayDetail, string errText)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.DayReport.Insert.Detail.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, dayDetail.ID, dayDetail.OrderNO, dayDetail.BeginRecipeNo,
                    dayDetail.EndRecipeNo, dayDetail.Count, dayDetail.RegFee, dayDetail.ChkFee, dayDetail.DigFee,
                    dayDetail.OthFee, dayDetail.OwnCost, dayDetail.PayCost, dayDetail.PubCost, (int)dayDetail.Status,
                    dayDetail.CardFee, dayDetail.CaseFee);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.DayReport.Insert.Detail.2]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                errText = this.Err;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 删除日结信息
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int DeleteDayReport(string operID, string balanceNO)
        {
            return this.ExecNoQuery(FS.SOC.HISFC.OutpatientFee.Data.AbstractRegisterDayReport.Current.Delete, operID, balanceNO);
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <returns></returns>
        public string GetDayReportID()
        {
            return this.GetSequence("Registration.DayReport.GetSequence");
        }

        /// <summary>
        /// 取消挂号日结
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateRegisterForCancelDayBalance(string operID, string balanceNO)
        {
            return UpdateSingleTable("Registration.Register.Update.CancelDayBalance", operID, balanceNO);
        }

        /// <summary>
        /// 挂号日结
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="OperID"></param>
        /// <param name="BalanceID"></param>
        /// <returns></returns>
        public int UpdateRegisterForDayBalance(DateTime begin, DateTime end, string operID, string balanceID)
        {
            return UpdateSingleTable("Registration.Register.Update.DayBalance", begin.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), operID, balanceID);
        }

        /// <summary>
        /// 取消挂号费日结
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateRegisterFeeForCancelDayBalance(string operID, string balanceNO)
        {
            return UpdateSingleTable("Registration.Register.Update.CancelAccountCardFee", operID, balanceNO);
        }

        /// <summary>
        /// 挂号日结
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="OperID"></param>
        /// <param name="BalanceID"></param>
        /// <returns></returns>
        public int UpdateRegisterFeeForDayBalance(DateTime begin, DateTime end, string operID, string balanceID)
        {
            return UpdateSingleTable("Registration.Register.Update.AccountCardFee", begin.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), operID, balanceID);
        }

        #endregion

        #region 查询挂号日结信息

        /// <summary>
        /// 根据收款员工号获取上次日结时间(1：成功/0：没有作过日结/-1：失败)
        /// </summary>
        /// <param name="employee">操作员</param>
        /// <param name="lastDate">返回上次日结截止时间</param>
        /// <returns>1：成功/0：没有作过日结/-1：失败</returns>
        public int GetLastBalanceDate(string operCode, ref DateTime lastDate, ref string balanceNO)
        {
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractRegisterDayReport.Current.SelectLastDate, operCode);
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
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractRegisterDayReport.Current.SelectLastFeeDate, operCode);
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
        public List<FS.FrameWork.Models.NeuObject> QueryBalanceList(DateTime month, string operCode)
        {
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractRegisterDayReport.Current.SelectByMonth, month.ToString("yyyy-MM"), operCode);
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
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractRegisterDayReport.Current.SelectDateByBalanceNO, balanceNO);
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

        #region 挂号日结审核

        /// <summary>
        /// 挂号日结审核
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateRegisterDayBalanceForCheck(string operID, string balanceNO)
        {
            string sql;

            sql = @"update fin_opr_daybalance
                       set invoice_check_opcd = '{0}',
                           invoice_check_date = sysdate,
                           invoice_check_flag = '1'
                     where balance_no = '{1}'
                       and invoice_check_flag = '0'";

            return UpdateSingleTable("Registration.DayReport.Update.Check", operID, balanceNO);
        }

        /// <summary>
        /// 取消挂号日结审核
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateRegisterDayBalanceForCancelCheck(string operID, string balanceNO)
        {
            string sql;

            sql = @"update fin_opr_daybalance
                       set invoice_check_opcd = null,
                           invoice_check_date = null,
                           invoice_check_flag = '0'
                     where invoice_check_opcd = '{0}'
                       and balance_no = '{1}'
                       and invoice_check_flag = '1'";

            return UpdateSingleTable("Registration.DayReport.Update.CancelCheck", operID, balanceNO);
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
                                  t.invoice_check_flag
                             from fin_opr_daybalance t
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
