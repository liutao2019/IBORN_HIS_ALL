using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SOC.Fee.DayBalance.Manager
{
    /// <summary>
    /// 住院收费、预交金日结报表
    /// </summary>
    public class InpatientDayBalanceManage_ZC : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 查询历史日结数据
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtHistoryBalanceData"></param>
        /// <returns></returns>
        public int QueryHistoryBalanceData(string operID, string beginDate, string endDate, out DataTable dtHistoryBalanceData)
        {
            dtHistoryBalanceData = null;
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.QueryHistoryDayBalanceInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.QueryHistoryDayBalanceInfo 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operID, beginDate, endDate);
                DataSet ds = new DataSet();

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    return -1;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    dtHistoryBalanceData = ds.Tables[0];
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 查询历结数据
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <param name="dtBalanceData"></param>
        /// <returns></returns>
        public int QueryDayBalanceDataByBalanceNO(string balanceNO, out DataTable dtBalanceData)
        {
            dtBalanceData = null;
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.QueryDayBalanceDataByBalnaceNO", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.QueryDayBalanceDataByBalnaceNO 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, balanceNO);
                DataSet ds = new DataSet();

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    return -1;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    dtBalanceData = ds.Tables[0];
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 获取最近一次日结时间
        /// </summary>
        /// <param name="operID">日结者</param>
        /// <param name="strLastDayBalanceDate"></param>
        /// <param name="strCurrentDate"></param>
        /// <returns></returns>
        public int GetLastDayBalanceDate(string operID, out string strLastDayBalanceDate, out string strCurrentDate)
        {
            strLastDayBalanceDate = null;
            strCurrentDate = null;

            string strSQL = string.Empty;
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.GetMaxtime_ZC", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.GetMaxtime_ZC 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operID);
                if (this.ExecQuery(strSQL) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    strLastDayBalanceDate = this.Reader[0] != DBNull.Value ? Convert.ToDateTime(this.Reader[0]).ToString("yyyy-MM-dd HH:mm:ss") : "";
                    strCurrentDate = this.Reader[1] != DBNull.Value ? Convert.ToDateTime(this.Reader[1]).ToString("yyyy-MM-dd HH:mm:ss") : "";
                }
                this.Reader.Close();
                return 1;
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取发票金额
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtBillMoney"></param>
        /// <returns></returns>
        public int QueryDayBalanceBillMoney(string operID, string beginDate, string endDate, out DataTable dtBillMoney)
        {
            dtBillMoney = null;
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.QueryDayBalanceBillMoney", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.QueryDayBalanceBillMoney 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operID, beginDate, endDate);
                DataSet ds = new DataSet();

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    return -1;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    dtBillMoney = ds.Tables[0];
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取预交金信息
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtPrepayMoney"></param>
        /// <returns></returns>
        public int QueryDayBalancePrepayMoney(string operID, string beginDate, string endDate, out DataTable dtPrepayMoney)
        {
            dtPrepayMoney = null;
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.QueryDayBalancePrepayMoney", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.QueryDayBalancePrepayMoney 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operID, beginDate, endDate);
                DataSet ds = new DataSet();

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    return -1;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    dtPrepayMoney = ds.Tables[0];
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取结算支付方式
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtBalancePayMode"></param>
        /// <returns></returns>
        public int QueryDayBalanceBalancePayMode(string operID, string beginDate, string endDate, out DataTable dtBalancePayMode)
        {
            dtBalancePayMode = null;
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.QueryBalancePayMode", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.QueryBalancePayMode 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operID, beginDate, endDate);
                DataSet ds = new DataSet();

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    return -1;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    dtBalancePayMode = ds.Tables[0];
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 保存日结记录
        /// </summary>
        /// <param name="paramArr"></param>
        /// <returns></returns>
        public int SaveDayBalanceRecord(string[] paramArr)
        {
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.SaveDayBalanceRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.SaveDayBalanceRecord 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, paramArr);

                if (this.ExecNoQuery(strSQL) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取日结号
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int GetBalanceNO(out string balanceNO)
        {
            string strSQL = string.Empty;
            balanceNO = "";
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.GetBalanceNO", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.GetBalanceNO 的SQL语句";

                return -1;
            }

            try
            {
                DataSet ds = new DataSet();
                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    return -1;
                }

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    balanceNO = ds.Tables[0].Rows[0][0].ToString();
                }

                if (string.IsNullOrEmpty(balanceNO))
                {
                    balanceNO = "1";
                }

            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 更新日结状态
        /// </summary>
        /// <param name="balnaceNO"></param>
        /// <param name="balanceOper"></param>
        /// <param name="oper"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int UpdateDayBalanceFlag(string balnaceNO, string balanceOper, string oper, string beginDate, string endDate)
        {
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.ZC.UpdateBalanceFlag", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.ZC.UpdateBalanceFlag 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, balnaceNO, balanceOper, oper, beginDate, endDate);

                if (this.ExecNoQuery(strSQL) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 日结生成住院实收明细信息，方便出报表
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <param name="balanceOper"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int BuildFeeIncome(string balanceNO, string balanceOper, string beginDate, string endDate)
        {
            string strSQL = string.Empty;

            if (this.Sql.GetSql("SOC.Fee.Inpatient.BuildFeeIncome", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: SOC.Fee.Inpatient.BuildFeeIncome 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, balanceNO, balanceOper, endDate);

                if (this.ExecNoQuery(strSQL) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }

    }
}
