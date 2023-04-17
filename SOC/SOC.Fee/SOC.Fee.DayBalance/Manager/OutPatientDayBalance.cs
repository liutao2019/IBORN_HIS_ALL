using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace SOC.Fee.DayBalance.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientDayBalance : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获取日结汇总数据明细
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public int QueryDayBalanceSumary(string startDate, string endDate, out DataTable dtResult)
        {
            dtResult = null;
            string strSql = string.Empty;

            if (this.Sql.GetSql("Local.Clinic.GetDayBalanceDataMZRJ.SelectSumary", ref strSql) == -1)
            {
                this.Err = "查找SQL语句失败！Local.Clinic.GetDayBalanceDataMZRJ.SelectSumary";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, startDate, endDate);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult = null;
            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }

            dtResult = dsResult.Tables[0];

            return 1;
        }
        /// <summary>
        /// 获取日结公费、支付方式汇总信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtPayResult"></param>
        /// <param name="dtPubResult"></param>
        /// <returns></returns>
        public int QueryDayBalanceSumaryPubPay(string startDate, string endDate, out DataTable dtPayResult, out DataTable dtPubResult)
        {
            dtPayResult = null;
            dtPubResult = null;

            string strSql = string.Empty;
            if (this.Sql.GetSql("Local.Clinic.GetPayTypeMoneySumary", ref strSql) == -1)
            {
                this.Err = "查找SQL语句失败！Local.Clinic.GetPayTypeMoneySumary";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, startDate, endDate);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult = null;
            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }

            dtPayResult = dsResult.Tables[0];

            if (this.Sql.GetSql("Local.Clinic.QueryDayBalancePactPubMoneySumary", ref strSql) == -1)
            {
                this.Err = "查找SQL语句失败！Local.Clinic.QueryDayBalancePactPubMoneySumary";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, startDate, endDate);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            dsResult = null;

            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }

            dtPubResult = dsResult.Tables[0];

            return 1;
        }
        /// <summary>
        /// 获取日结记录
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="arlRecords"></param>
        /// <returns></returns>
        public int QueryDayBalanceRecord(string oper, string startDate, string endDate, out ArrayList arlRecords)
        {
            arlRecords = null;

            string strSql = string.Empty;
            if (this.Sql.GetSql("Local.Clinic.QueryDayBalanceRecord", ref strSql) == -1)
            {
                this.Err = "查找SQL语句失败！Local.Clinic.QueryDayBalanceRecord";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, oper, startDate, endDate);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult = null;
            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }

            if (dsResult == null || dsResult.Tables.Count <= 0)
            {
                return 1;
            }

            DataTable dtResult = dsResult.Tables[0];
            FS.FrameWork.Models.NeuObject objRecord = null;
            arlRecords = new ArrayList();
            foreach (DataRow dr in dtResult.Rows)
            {
                objRecord = new FS.FrameWork.Models.NeuObject();
                objRecord.ID = dr["balance_no"].ToString().Trim();
                objRecord.Memo = dr["oper_code"].ToString().Trim();
                objRecord.User01 = dr["begin_date"].ToString().Trim();
                objRecord.User02 = dr["end_date"].ToString().Trim();
                objRecord.Name = dr["begin_date"].ToString().Trim() + "-" + dr["end_date"].ToString().Trim();
                arlRecords.Add(objRecord);
            }
            return 1;
        }
        /// <summary>
        /// 获取银行卡结算明细信息
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNo"></param>
        /// <param name="payMode"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public int QueryDayBalnaceDetialByBank(string oper, string startDate, string endDate, string payMode, out DataTable dtResult)
        {
            dtResult = null;

            string strSql = string.Empty;
            if (this.Sql.GetSql("Local.Clinic.QueryDayBalanceDetialByBank", ref strSql) == -1)
            {
                this.Err = "查找SQL语句失败！Local.Clinic.QueryDayBalanceDetialByBank";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, oper,startDate, endDate, payMode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult = null;
            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }

            if (dsResult == null || dsResult.Tables.Count <= 0)
            {
                return 1;
            }

            dtResult = dsResult.Tables[0];
            return 1;
        }
        /// <summary>
        ///  按日获取银行卡结算汇总
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNo"></param>
        /// <param name="payMode"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public int QueryDayBalnaceDetialByBankByDay(string oper, string startDate, string endDate, string payMode, out DataTable dtResult)
        {
            dtResult = null;

            string strSql = string.Empty;
            if (this.Sql.GetSql("Local.Clinic.QueryDayBalanceDetialByBankByDay", ref strSql) == -1)
            {
                this.Err = "查找SQL语句失败！Local.Clinic.QueryDayBalanceDetialByBankByDay";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, oper, startDate, endDate, payMode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult = null;
            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }

            if (dsResult == null || dsResult.Tables.Count <= 0)
            {
                return 1;
            }

            dtResult = dsResult.Tables[0];
            return 1;
        }
        /// <summary>
        ///  获取未日结银行卡结算汇总
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNo"></param>
        /// <param name="payMode"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public int QueryDayBalnaceDetialByNotBalance(string oper, string payMode, out DataTable dtResult)
        {
            dtResult = null;

            string strSql = string.Empty;
            if (this.Sql.GetSql("Local.Clinic.QueryDayBalanceDetialByNotBalance", ref strSql) == -1)
            {
                this.Err = "查找SQL语句失败！Local.Clinic.QueryDayBalanceDetialByNotBalance";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, oper, payMode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult = null;
            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }

            if (dsResult == null || dsResult.Tables.Count <= 0)
            {
                return 1;
            }

            dtResult = dsResult.Tables[0];
            return 1;
        }
        /// <summary>
        ///  按发票号判断发票的支付方式
        /// </summary>
        /// <param name="balanceNo"> 发票号</param>
        /// <returns></returns>
        public string GetBankByBalanceNo(string BalanceNo)
        {
            string bankName = null;
            if(BalanceNo ==null)
            {
                return null;
            }
            string sql = @"select
                           a.invoice_no ,
                           a.mode_code 
                           from fin_opb_paymode a
                           where a.balance_flag = '0'
                           and a.invoice_no ='{0}'";
            try
            {
                sql = string.Format(sql,BalanceNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet dsResult = null;
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return null;
            }
            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                if (bankName == "NH" || bankName =="JH")
                {
                    return bankName;
                }
                bankName = dr["mode_code"].ToString().Trim();
            }
            return bankName;
        }
    }
}
