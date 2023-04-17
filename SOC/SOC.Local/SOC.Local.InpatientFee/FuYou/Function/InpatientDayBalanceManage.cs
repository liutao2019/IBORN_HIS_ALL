using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace FS.SOC.Local.InpatientFee.FuYou.Function
{
    /// <summary>
    /// 住院收费日结报表
    /// </summary>
    public class InpatientDayBalanceManage : FS.FrameWork.Management.Database
    {
        #region 获取结算日结数据
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
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.GetMaxtime", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.GetMaxtime 的SQL语句";

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
        }

        /// <summary>
        /// 查询各类费用
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtCostStat"></param>
        /// <returns></returns>
        public int QueryDayBalanceCostByStat(string operID, string beginDate, string endDate, out DataTable dtCostStat)
        {
            dtCostStat = null;
            string strSQL = string.Empty;
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryFeeCostByStat", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryFeeCostByStat 的SQL语句";

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
                    dtCostStat = ds.Tables[0];
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
        /// 获取有效票据数
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="effectiveBill"></param>
        /// <returns></returns>
        public int QueryDayBalanceEffectiveBill(string operID, string beginDate, string endDate, out string effectiveBill)
        {
            effectiveBill = null;
            string strSQL = string.Empty;
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceEffectiveBill", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryDayBalanceEffectiveBill 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, beginDate, endDate, operID);

                if (this.ExecQuery(strSQL) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    effectiveBill = this.Reader[0].ToString();
                }

                this.Reader.Close();
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
        /// 获取有效票据号码
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="effectiveBill"></param>
        /// <returns></returns>
        public int QueryDayBalanceEffectiveInvoiceNo(string operID, string beginDate, string endDate, ref DataSet ds)
        {
            string strSQL = string.Empty;
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceEffectiveInvoiceNo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryDayBalanceEffectiveInvoiceNo 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, beginDate, endDate, operID);

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    this.WriteErr();
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
        /// 获取作废票据数
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="uneffectiveBill"></param>
        /// <returns></returns>
        public int QueryDayBalanceUneffectiveBill(string operID, string beginDate, string endDate, out string uneffectiveBill)
        {
            uneffectiveBill = null;
            string strSQL = string.Empty;
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceUneffectiveBill", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryDayBalanceUneffectiveBill 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, beginDate, endDate, operID);

                if (this.ExecQuery(strSQL) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    uneffectiveBill = this.Reader[0].ToString();
                }

                this.Reader.Close();
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
        /// 获取作废票据号码
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="effectiveBill"></param>
        /// <returns></returns>
        public int QueryDayBalanceUneffectiveInvoiceNo(string operID, string beginDate, string endDate, ref DataSet ds)
        {
            string strSQL = string.Empty;
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceUneffectiveInvoiceNo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryDayBalanceUneffectiveInvoiceNo 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, beginDate, endDate, operID);

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    this.WriteErr();
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
        /// 获取作废票据总金额
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="quitCost"></param>
        /// <returns></returns>
        public int QueryDayBalanceUneffectiveBillMoney(string operID, string beginDate, string endDate, out decimal quitCost)
        {
            string sql = "";
            quitCost = 0;
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceUneffectiveBillMoney", ref sql) == -1)
            {
                this.Err = "未找到ID为Fee.Daybalance.Inpatient.QueryDayBalanceUneffectiveBillMoney的sql语句";
                return -1;
            }
            try
            {
                if (this.ExecQuery(sql, beginDate, endDate, operID) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    return -1;
                }
                while (this.Reader.Read())
                {
                    quitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.Reader.Close();
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

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceBillMoney", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryDayBalanceBillMoney 的SQL语句";

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
        /// 获取住院发票明细信息
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtInvoice"></param>
        /// <returns></returns>
        public int QueryDayBalanceInvoiceDetial(string operID, string beginDate, string endDate, out DataTable dtInvoice)
        {
            dtInvoice = null;
            string strSQL = "";

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryInvoiceDetial", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryInvoiceDetial 的SQL语句";

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
                    dtInvoice = ds.Tables[0];
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
        /// 获取发票结算现金和刷卡金额
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="CACost"></param>
        /// <param name="CDCost"></param>
        /// <returns></returns>
        public int QueryDayBalanceCACDMoney(string operID, string beginDate, string endDate, ref ArrayList al)
        {
            string sql = "";
            FS.FrameWork.Models.NeuObject obj = null;
            al = null;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceCACDMoney", ref sql) == -1)
            {
                this.Err = "未找到ID为Fee.Daybalance.Inpatient.QueryDayBalanceCACDMoney的sql语句";
                return -1;
            }
            try
            {
                if (this.ExecQuery(sql, operID, beginDate, endDate) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    return -1;
                }
                al = new ArrayList();
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.Memo = this.Reader[2].ToString();
                    obj.User01 = this.Reader[3].ToString();
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.Reader.Close();
                return -1;
            }
            return 1;
        }
        #endregion

        #region 根据时间范围获取相应的日结记录（非明细）
        /// <summary>
        /// 根据时间范围获取相应的日结记录（非明细）
        /// </summary>
        /// <param name="operID">操作员信息</param>
        /// <param name="beginDate">起始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <param name="lstBalanceRecord">返回的日结记录数组</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetBalanceRecord(string operID, string beginDate, string endDate, out List<FS.FrameWork.Models.NeuObject> lstBalanceRecord)
        {
            lstBalanceRecord = null;
            int intReturn = 0;
            string strSQL = string.Empty;
            // 获取查询语句
            intReturn = this.Sql.GetSql("Fee.Daybalance.Inpatient.GetBalanceRecord", ref strSQL);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operID, beginDate, endDate);

                intReturn = this.ExecQuery(strSQL);
                if (intReturn == -1)
                {
                    this.Err = "执行SQL语句失败" + this.Err;
                    return -1;
                }
                lstBalanceRecord = new List<FS.FrameWork.Models.NeuObject>();
                FS.FrameWork.Models.NeuObject balanceRecord = null;
                while (this.Reader.Read())
                {
                    balanceRecord = new FS.FrameWork.Models.NeuObject();
                    balanceRecord.ID = this.Reader[0].ToString();
                    balanceRecord.Name = this.Reader[1].ToString();
                    balanceRecord.Memo = this.Reader[2].ToString();
                    balanceRecord.User01 = this.Reader[3].ToString();
                    lstBalanceRecord.Add(balanceRecord);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.Reader.Close();
                return -1;
            }

            return 1;
        }
        #endregion

        #region 收费日结
        /// <summary>
        /// 处理操作员日结
        /// </summary>
        /// <param name="operCode">工号</param>
        /// <param name="balancer">日结人</param>
        /// <param name="beginDate">上次日结时间</param>
        /// <param name="endDate">本次日结截至时间</param>
        /// <returns>-1出错，1成功</returns>
        public int DealOperDayBalance(string operCode, string balancer, string beginDate, string endDate)
        {
            string strSQL = string.Empty;
            string strReturn = string.Empty;

            if (Sql.GetSql("Fee.Daybalance.Inpatient.DayBalance", ref strSQL) == -1)
            {
                this.Err = "执行存储过程失败，没有找到sql：Fee.Daybalance.Inpatient.DayBalance";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, operCode, balancer, beginDate, endDate);
                if (this.ExecEvent(strSQL, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[1]) == -1)
                {
                    this.Err = str[0];
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

        #region 日结审核
        /// <summary>
        /// 查询日结列表
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="checkType"></param>
        /// <param name="dtDayBalance"></param>
        /// <returns></returns>
        public int QueryDayBalanceList(string operID, string beginDate, string endDate, string checkType, out DataTable dtDayBalance)
        {
            dtDayBalance = null;
            string strSQL = @"";

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceList", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.QueryDayBalanceList 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, beginDate, endDate, operID, checkType);
                DataSet ds = new DataSet();

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    return -1;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    dtDayBalance = ds.Tables[0];
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
        /// 更新审核状态
        /// </summary>
        /// <param name="balanceID">结算单号</param>
        /// <param name="checker">审核人</param>
        /// <param name="checkdate">审核日期</param>
        /// <param name="isCheck">是否审核 1 = 审核</param>
        /// <returns></returns>
        public int UpdateDayBalanceCheck(string balanceID, string checker, string checkdate, string isCheck)
        {
            string strSQL = @"";
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.UpdateDayBalanceCheck", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.UpdateDayBalanceCheck 的SQL语句";

                return -1;
            }

            try
            {
                if (isCheck == "1")
                {
                    strSQL = string.Format(strSQL, "1", checker, checkdate, balanceID);
                }
                else
                {
                    strSQL = string.Format(strSQL, "0", checker, checkdate, balanceID);
                }

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

        #endregion

        #region 获取押金日结数据
        /// <summary>
        /// 获取有效押金收据数
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <param name="invoiceCount"></param>
        /// <returns></returns>
        public int GetPrepayBalanceInvoiceCount(string operCode, string LastBalanceDate, string CurDate, ref int invoiceCount, ref string beginInvoice, ref string endInvoice)
        {
            string sql = "";
            if (this.Sql.GetSql("PrePay.Daybalance.GetPrepayBalanceInvoice", ref sql) == -1)
            {
                this.Err = "未找到ID为PrePay.Daybalance.GetPrepayBalanceInvoice的sql语句";
                return -1;
            }
            try
            {
                if (this.ExecQuery(sql, operCode, LastBalanceDate, CurDate) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    return -1;
                }
                while (this.Reader.Read())
                {
                    invoiceCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                    beginInvoice = this.Reader[1].ToString();
                    endInvoice = this.Reader[2].ToString();
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                WriteErr();
                this.Reader.Close();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取作废押金收据数
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <param name="invoiceCount"></param>
        /// <returns></returns>
        public int GetPrepayBalanceQuitInvoiceCount(string operCode, string LastBalanceDate, string CurDate, out int invoiceQuitCount)
        {
            invoiceQuitCount = 0;
            string sql = "";
            if (this.Sql.GetSql("PrePay.Daybalance.GetPrepayBalanceQuitInvoice", ref sql) == -1)
            {
                this.Err = "未找到ID为PrePay.Daybalance.GetPrepayBalanceQuitInvoice的sql语句";
                return -1;
            }
            try
            {
                if (this.ExecQuery(sql, operCode, LastBalanceDate, CurDate) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    return -1;
                }
                while (this.Reader.Read())
                {
                    invoiceQuitCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                WriteErr();
                this.Reader.Close();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 取一段时间内退预交金金额
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="quitCost"></param>
        /// <returns></returns>
        public int GetPrepayQuitCost(string operCode, string lastDate, string curDate, out decimal quitCost)
        {
            string sql = "";
            quitCost = 0;
            if (this.Sql.GetSql("Prepay.Daybalance.GetPrepayQuitCost", ref sql) == -1)
            {
                this.Err = "未找到ID为Prepay.Daybalance.GetPrepayQuitCost的sql语句";
                return -1;
            }
            try
            {
                if (this.ExecQuery(sql, operCode, lastDate, curDate) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    return -1;
                }
                while (this.Reader.Read())
                {
                    quitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.Reader.Close();
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取有效票据号
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <returns></returns>
        public int GetPrepayBalanceInvoiceNo(string operCode, string lastDate, string curDate, ref DataSet ds)
        {
            string strSQL = string.Empty;
            if (this.Sql.GetSql("PrePay.Daybalance.GetPrepayBalanceInvoiceNo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: PrePay.Daybalance.GetPrepayBalanceInvoiceNo 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operCode, lastDate, curDate);

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    this.WriteErr();
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
        /// 获取作废票据号
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <returns></returns>
        public int GetPrepayBalanceQuitInvoiceNo(string operCode, string lastDate, string curDate, ref DataSet ds)
        {
            string strSQL = string.Empty;
            if (this.Sql.GetSql("PrePay.Daybalance.GetPrepayBalanceQuitInvoiceNo", ref strSQL) == -1)
            {
                this.Err = "没有找到索引为: PrePay.Daybalance.GetPrepayBalanceQuitInvoiceNo 的SQL语句";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, operCode, lastDate, curDate);

                if (this.ExecQuery(strSQL, ref ds) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    this.WriteErr();
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
        /// 获取预交总金额
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="totCost"></param>
        /// <param name="cash"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetPrepayBalanceCost(string operCode, string lastDate, string curDate, out decimal totCost, out decimal cash, out decimal pos)
        {
            string sql = "";
            totCost = 0;
            cash = 0;
            pos = 0;
            if (this.Sql.GetSql("Prepay.Daybalance.GetPrepayBalanceCost", ref sql) == -1)
            {
                this.Err = "没有找到索引为: Prepay.Daybalance.GetPrepayBalanceCost 的SQL语句";
                return -1;
            }
            try
            {
                if (this.ExecQuery(sql, operCode, lastDate, curDate) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    this.WriteErr();
                    return -1;
                }
                while (this.Reader.Read())
                {
                    totCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                    cash = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                    pos = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                this.Reader.Close();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 取一段时间内预交金支付金额
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="quitCost"></param>
        /// <returns></returns>
        public int GetPrepayPayCost(string operCode, string lastDate, string curDate, out decimal prepayPay, out  decimal cash, out decimal pos, out decimal chCost, out decimal orCost, out decimal fgCost)
        {
            string sql = "";
            prepayPay = 0;
            cash = 0;
            pos = 0;
            chCost = 0;
            orCost = 0;
            fgCost = 0;
            if (this.Sql.GetSql("Prepay.Daybalance.GetPrepayPayCost", ref sql) == -1)
            {
                this.Err = "没有找到索引为: Prepay.Daybalance.GetPrepayPayCost的SQL语句";
                return -1;
            }
            try
            {
                if (this.ExecQuery(sql, operCode, lastDate, curDate) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    this.WriteErr();
                    return -1;
                }
                while (this.Reader.Read())
                {
                    prepayPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                    cash = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                    pos = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    chCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    orCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    fgCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                this.Reader.Close();
                return -1;
            }
            return 1;
        }
        #endregion

        #region 押金日结
        /// <summary>
        /// 插入日报表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertPrepayStat(Models.PrepayDayBalance obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.FeeReport.InsertPrepayStat", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql,
                    obj.BeginDate,
                    obj.EndDate,
                    this.Operator.ID,
                    obj.RealCost,
                    obj.QuitCost,
                    obj.TotCost,
                    obj.CACost,
                    obj.POSCost,
                    obj.CHCost,
                    obj.ORCost,
                    obj.FGCost,
                    obj.BeginInvoice,
                    obj.EndInvoice,
                    obj.PrepayNum,
                    obj.QuitNum,
                    (this.Operator as FS.HISFC.Models.Base.Employee).Dept.ID,
                    obj.CheckFlag);
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
        /// 获取预交金日结记录
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <param name="lstPrePayRecord"></param>
        /// <returns></returns>
        public int GetPrepayBalanceHistory(string operCode, string Begin, string End, out List<FS.FrameWork.Models.NeuObject> lstPrePayRecord)
        {
            lstPrePayRecord = null;
            string strSql = "";
            if (this.Sql.GetSql("Fee.FeeReport.GetPrepayStatListByDate", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.FeeReport.GetPrepayStatListByDate 的SQL语句" + this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, operCode, Begin, End);

                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = "执行SQL语句失败！";
                    return -1;
                }
                lstPrePayRecord = new List<FS.FrameWork.Models.NeuObject>();
                FS.FrameWork.Models.NeuObject prePayRecord = null;
                while (this.Reader.Read())
                {
                    prePayRecord = new FS.FrameWork.Models.NeuObject();
                    prePayRecord.ID = this.Reader[0].ToString();
                    prePayRecord.Name = this.Reader[1].ToString();
                    prePayRecord.Memo = this.Reader[2].ToString();
                    lstPrePayRecord.Add(prePayRecord);
                }

                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.Reader.Close();
                return -1;
            }
            return 1;
        }
        #endregion
    }
}
