using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace SOC.Fee.DayBalance.Manager
{
    /// <summary>
    /// 住院收费日结报表
    /// </summary>
    public class InpatientDayBalanceManage : FS.FrameWork.Management.Database
    {
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
            return 1;
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
        /// 获取发票金额（待遇算法dll）
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtBillMoney"></param>
        /// <returns></returns>
        public int QueryDayBalanceBillMoneyForDllname(string operID, string beginDate, string endDate, out DataTable dtBillMoney)
        {
            dtBillMoney = null;
            string strSQL = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.QueryDayBalanceBillMoneyForDllname", ref strSQL) == -1)
            {
                strSQL = @"select (select a.dll_name  from fin_com_pactunitinfo a where pact_code= t.pact_code and a.valid_state='1') dllname ,
       sum(t.tot_cost) totalcost,
       sum(t.own_cost + t.pay_cost) owncost,
       sum(t.pub_cost) pubcost,
       sum(t.eco_cost) ecocost,
       sum(t.der_cost) dercost,
       sum(t.prepay_cost) prepaycost,
       sum(t.change_totcost) changecost,
       sum(t.supply_cost) supplycost,
       sum(t.return_cost) returncost
  from fin_ipb_balancehead t
 where t.balance_opercode = '{0}'
 and t.balance_date >= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
 and t.balance_date <= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
 group by t.pact_code";
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
        /// 获取日结发票数据
        /// </summary>
        /// <param name="employeeID">收款员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayInvoiceDataNew(string employeeID, string dateBegin,string dateEnd, ref DataSet dsResult)
        {
            string SQL = "";
            if (this.Sql.GetSql("Local.Inpatient.GetDayInvoiceDataNew.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 预交作废票据子号
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        public string GetOutReceiptBack(string OperID,string BeginDate, string EndDate)
        {
            string strRet = "";
            string strSQL = "";
            ArrayList List = new ArrayList();

            strSQL = @"            select print_invoiceno from fin_ipb_balancehead
where  balance_opercode='{0}'
and balance_date >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and balance_date <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')
and trans_type='2'
order by print_invoiceno asc";


            try
            {
                strSQL = string.Format(strSQL, OperID, BeginDate, EndDate);

                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    strRet += Reader[0].ToString() + "|";
                }

                if (strRet.Length >= 1)
                {
                    strRet = strRet.Substring(0, strRet.Length - 1);
                }
            }
            catch { }
            return strRet;
        }


        /// <summary>
        /// 获取日结退费金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="cancelMoney">返回的退费金额</param>
        /// <returns>1成功-1失败</returns>
        public int GetDayBalanceCancelMoney(string employeeID, string dateBegin, string dateEnd, ref decimal cancelMoney)
        {
            string SQL = "";
            if (this.Sql.GetSql("Local.Inpatient.GetDayInvoiceCancelMoney", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                cancelMoney = FS.FrameWork.Function.NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 按支付方式查找金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalancePayTypeMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            string SQL = "";
            if (this.Sql.GetSql("Local.Inpatient.GetPayTypeMoney", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取合同单位报销金额
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryDayBalancePactPubMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            string SQL = "";

            if (this.Sql.GetSql("Local.Inpatient.QueryDayBalancePactPubMoney", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 查询冲销预交金金额
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryDayBalancePrepayMoney(string employeeID, string dateBegin, string dateEnd, ref decimal prepayMoney)
        {
            string SQL = "";

            if (this.Sql.GetSql("Local.Inpatient.QueryDayBalancePrepayMoney", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                prepayMoney = FS.FrameWork.Function.NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            
            return 1;
        }

        /// <summary>
        /// 获取日结之后的发票数据
        /// </summary>
        /// <param name="employeeID">收款员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayInvoiceDataNewReprint(string employeeID, string dateBegin, string dateEnd, ref DataSet dsResult)
        {
            string SQL = "";
            if (this.Sql.GetSql("Local.Inpatient.GetDayInvoiceDataNewReprint.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取日结之后的退费金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="cancelMoney">返回的退费金额</param>
        /// <returns>1成功-1失败</returns>
        public int GetDayBalanceCancelMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref decimal cancelMoney)
        {
            string SQL = "";
            if (this.Sql.GetSql("Local.Inpatient.GetDayInvoiceCancelMoneyReprint", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                cancelMoney = FS.FrameWork.Function.NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 查询日结之后的冲销预交金金额
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryDayBalancePrepayMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref decimal prepayMoney)
        {
            string SQL = "";

            if (this.Sql.GetSql("Local.Inpatient.QueryDayBalancePrepayMoneyReprint", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                prepayMoney = FS.FrameWork.Function.NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 按支付方式查找日结之后的金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalancePayTypeMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            string SQL = "";
            if (this.Sql.GetSql("Local.Inpatient.GetPayTypeMoneyReprint", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取日结之后的合同单位报销金额
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryDayBalancePactPubMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            string SQL = "";

            if (this.Sql.GetSql("Local.Inpatient.QueryDayBalancePactPubMoneyReprint", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

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
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 获取最近一次日结数据
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        public int QueryLastBalanceRecord(string operID, out FS.FrameWork.Models.NeuObject balance)
        {
            balance = null;
            string strSql = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.DayBalance.GetLastBalance", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.DayBalance.GetLastBalance 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, operID, operID);

                int intReturn = this.ExecQuery(strSql);
                if (intReturn == -1)
                {
                    this.Err = "执行SQL语句失败 " + this.Err;
                    return -1;
                }

                balance = new FS.FrameWork.Models.NeuObject();
                while (this.Reader.Read())
                {
                    balance.ID = this.Reader[0].ToString();
                    balance.Name = this.Reader[1].ToString();
                    balance.Memo = this.Reader[2].ToString();
                    balance.User01 = this.Reader[3].ToString();
                    balance.User02 = this.Reader[4].ToString();
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

        #region 取消日结
        /// <summary>
        /// 取消日结
        /// </summary>
        /// <param name="balanceNo">日结号</param>
        /// <param name="beginDate">日结开始时间</param>
        /// <param name="endDate">日结结束时间</param>
        /// <returns></returns>
        public int UnDoOperDayBalance(string balanceNo, string beginDate, string endDate)
        {
            string strSql = string.Empty;

            #region 删除收入中间表
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.UnDoDayBalance.DeleteIncom", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.UnDoDayBalance.DeleteIncom 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo);
                if (this.ExecNoQuery(strSql) == -1)
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
            #endregion

            #region 更新业务表
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.UnDoDayBalance.Update", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.UnDoDayBalance.Update 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo, beginDate, endDate);
                if (this.ExecNoQuery(strSql) == -1)
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
            #endregion

            #region 删除日结数据
            if (this.Sql.GetSql("Fee.Daybalance.Inpatient.UnDoDayBalance.DeleteDayBalance", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Inpatient.UnDoDayBalance.DeleteDayBalance 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo);
                if (this.ExecNoQuery(strSql) == -1)
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
            #endregion
            return 1;
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

    }
}
