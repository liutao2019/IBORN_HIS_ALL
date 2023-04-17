using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace SOC.Fee.DayBalance.Manager
{
    class PrepayDayBalance:FS.FrameWork.Management.Database
    {

        /// <summary>
        /// 获取日结序号(1：成功/-1：失败),所有日结可以都走一个序号
        /// </summary>
        /// <param name="sequence">返回日结序列号</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetBalanceSequence(ref string sequence)
        {
            // 获取日结序号
            sequence = this.GetSequence("Local.Clinic.Function.CreateClinicDayBalance.GetInsertSequence");
            if (sequence == null)
            {
                this.Err = "获取流水号失败！" + this.Err;
                return -1;
            }
            return 1;
        }

        #region  门诊账户预交金日结
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <returns></returns>
        public int GetAccBalanceDate(string operCode, ref DateTime LastBalanceDate, ref DateTime CurDate)
        {
            string sql = "";
            if (this.Sql.GetSql("ACC.Daybalance.Getoperdate", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode);
            while (this.Reader.Read())
            {
                LastBalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0]);
                CurDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1]);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <returns></returns>
        public int GetAccBalanceInvoice(string operCode, string LastBalanceDate, string CurDate, string balanceFlag, ref int invoiceCount, ref string beginInvoice, ref string endInvoice)
        {
            string sql = "";
            if (this.Sql.GetSql("ACC.Daybalance.GetAccBalanceInvoice", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, LastBalanceDate, CurDate, balanceFlag);
            while (this.Reader.Read())
            {
                invoiceCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                beginInvoice = this.Reader[1].ToString();
                endInvoice = this.Reader[2].ToString();
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 通过结算序号获取结算开始时间和结束时间
        /// </summary>
        /// <param name="balanceNO">结算序号</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetAccBalanceBeginAndEndTime(string balanceNO, ref string beginTime, ref string endTime)
        {
            string sql = "";
            if (this.Sql.GetSql("ACC.Daybalance.GetAccBalanceBeginAndEndTime", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, balanceNO);
            while (this.Reader.Read())
            {
                beginTime = this.Reader[0].ToString();
                endTime = this.Reader[1].ToString();
            }
            this.Reader.Close();
            return 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="totCost"></param>
        /// <param name="cash"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetAccBalanceCost(string operCode, string lastDate, string curDate, string balanceFlag, out decimal totCost, out  decimal cash, out decimal pos)
        {
            string sql = "";
            totCost = 0;
            cash = 0;
            pos = 0;
            if (this.Sql.GetSql("ACC.Daybalance.GetAccBalanceCost", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, lastDate, curDate, balanceFlag);
            while (this.Reader.Read())
            {
                totCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                cash = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                pos = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="quitCost"></param>
        /// <returns></returns>
        public int GetAccQuitCost(string operCode, string lastDate, string curDate, out decimal quitCost)
        {
            string sql = "";
            quitCost = 0;
            if (this.Sql.GetSql("ACC.Daybalance.GetAccQuitCost", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, lastDate, curDate);
            while (this.Reader.Read())
            {
                quitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 获取历时日结记录
        /// </summary>
        /// <param name="operCode">收费员工号</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>成功返回1，不成功返回-1</returns>
        public int GetAccBalanceHistory(string operCode, string begin, string end, ref System.Data.DataSet dsResult)
        {
            int intReturn = 0;
            string sql = "";
            intReturn = this.Sql.GetSql("ACC.Daybalance.GetAccBalanceHistory", ref sql);
            try
            {
                sql = string.Format(sql, operCode, begin, end);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecQuery(sql, ref dsResult);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="leftCost"></param>
        /// <param name="totCost"></param>
        /// <param name="quitCost"></param>
        /// <param name="ca"></param>
        /// <param name="pos"></param>
        /// <param name="checkFlag"></param>
        /// <param name="checkOpcd"></param>
        /// <param name="checkDate"></param>
        /// <returns></returns>
        public int SaveBalance(string balanceNO, string operCode, string lastDate, string curDate, decimal leftCost, decimal totCost, decimal quitCost, decimal ca, decimal pos, string checkFlag, string checkOpcd, string checkDate, string beginInvoice, string endInvoice)
        {
            return this.UpdateSingleTable("ACC.Daybalance.SaveBalance", balanceNO, lastDate, curDate, operCode, leftCost.ToString(), totCost.ToString(), quitCost.ToString(), ca.ToString(), pos.ToString(), checkFlag, checkOpcd, checkDate, beginInvoice, endInvoice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opercode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateBalanceFlag(string opercode, string lastDate, string curDate, string balanceNO)
        {
            return this.UpdateSingleTable("ACC.Daybalance.UpdateBalanceFlag", opercode, lastDate, curDate, balanceNO);
        }
        #endregion

        #region 住院预交金日结      

        /// <summary>
        /// 获取上次日结时间和当前时间
        /// </summary>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <returns></returns>
        public int GetPrepayDayBalanceDate(ref DateTime LastBalanceDate, ref DateTime CurDate)
        {
            try
            {
                string strSQL = "";

                if (this.Sql.GetSql("Fee.FeeReport.GetMaxtime", ref strSQL) == -1)
                {
                    this.Err = "获得最大时间出错!";
                    return -1;
                }
                else
                {
                    strSQL = string.Format(strSQL, this.Operator.ID);
                }
                this.ExecQuery(strSQL, this.Operator.ID);
                while (this.Reader.Read())
                {
                    LastBalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0]);
                    CurDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1]);
                }
                this.Reader.Close();
            }
            catch
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 获取预交金日结记录
        /// </summary>
        /// <param name="Oper"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public int GetPrepayBalanceHistory(string operCode, string Begin, string End, ref DataSet dsResult)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Fee.FeeReport.GetPrepayStatListByDate", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                else
                {
                    strSql = string.Format(strSql, operCode, Begin, End);
                }

                this.ExecQuery(strSql, ref dsResult);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <returns></returns>
        public int GetPrepayBalanceInvoice(string operCode, string LastBalanceDate, string CurDate, ref int invoiceCount, ref string beginInvoice, ref string endInvoice)
        {
            string sql = "";
            if (this.Sql.GetSql("PrePay.Daybalance.GetPrepayBalanceInvoice", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, LastBalanceDate, CurDate);
            while (this.Reader.Read())
            {
                invoiceCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                beginInvoice = this.Reader[1].ToString();
                endInvoice = this.Reader[2].ToString();
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 查询使用的预交金发票
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int GetPrepayBalanceInvoiceUsedNew(string operCode, string LastBalanceDate, string CurDate, ref DataSet dsResult)
        {
            string sql = "";
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }

            if (this.Sql.GetSql("PrePay.Daybalance.GetPrepayBalanceInvoiceUsedNew", ref sql) == -1)
            {
                return -1;
            }

            try
            {

                sql = string.Format(sql, operCode, LastBalanceDate, CurDate);

                if (this.ExecQuery(sql, ref dsResult) == -1)
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            
            return 1;
        }

        /// <summary>
        /// 获取作废单据号
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <param name="invalidrecipeno"></param>
        /// <returns></returns>
        public int GetPrepayBalanceInvalidInvoice(string operCode, string LastBalanceDate, string CurDate, ref ArrayList invalidrecipeno)
        {
            string sql = "";
            //string temp = "";
            if (this.Sql.GetSql("PrePay.Daybalance.GetPrepayBalanceInvalidInvoice", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, LastBalanceDate, CurDate);
            while (this.Reader.Read())
            {
                //invoiceCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                //beginInvalidInvoice = this.Reader[1].ToString();
                //endInvalidInvoice = this.Reader[2].ToString();
                //temp = this.Reader[0].ToString();
                invalidrecipeno.Add(this.Reader[0].ToString());

            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="totCost"></param>
        /// <param name="cash"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetPrepayBalanceCost(string operCode, string lastDate, string curDate, out decimal totCost, out  decimal cash, out decimal pos,out decimal chCost,out decimal orCost,out decimal fgCost )
        {
            string sql = "";
            totCost = 0;
            cash = 0;
            pos = 0;
            chCost = 0;
            orCost = 0;
            fgCost = 0;
            if (this.Sql.GetSql("Prepay.Daybalance.GetPrepayBalanceCost", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, lastDate, curDate);
            while (this.Reader.Read())
            {
                totCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                cash = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                pos = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                chCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                orCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                fgCost= FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
            }
            this.Reader.Close();
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
                return -1;
            }
            this.ExecQuery(sql, operCode, lastDate, curDate);
            while (this.Reader.Read())
            {
                quitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            this.Reader.Close();
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
                return -1;
            }
            this.ExecQuery(sql, operCode, lastDate, curDate);
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
            return 1;
        }
  
        /// <summary>
        /// 插入日报表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertPrepayStat(Object.PrepayDayBalance obj)
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

        public int DeletePrepayStat(string balanceNO, string operCode)
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.FeeReport.DeletePrepayStat", ref strSql) == -1) return -1;

            strSql = string.Format(strSql, balanceNO, operCode);

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除预交金日结
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeletePrepayStat(string static_no)
        {
            string strSql = @"delete from FIN_IPB_PREPAYSTAT  where static_no='{0}'";
            
            try
            {
                strSql = string.Format(strSql, static_no);
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 通过预交金序号获得日结名细
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public Object.PrepayDayBalance GetPrepayDayBalance(string strID)
        {

            Object.PrepayDayBalance obj = new Object.PrepayDayBalance();
            string strSql = "";
            if (this.Sql.GetSql("Fee.FeeReport.GetPrepayStatListBystaticNo", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, strID);
                if (this.ExecQuery(strSql) == -1) return null;
                if (this.Reader.Read())
                {
                    obj.ID = this.Reader[0].ToString();
                    obj.BeginDate = this.Reader[1].ToString();
                    obj.EndDate =  this.Reader[2].ToString();
                    obj.BalancOper.ID = this.Reader[3].ToString();
                    obj.BalancOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                    obj.RealCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    obj.QuitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    obj.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    obj.CACost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                    obj.POSCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    obj.CHCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                    obj.ORCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                    obj.FGCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                    obj.BeginInvoice = this.Reader[13].ToString();
                    obj.EndInvoice = this.Reader[14].ToString();
                    obj.PrepayNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15]);
                    obj.QuitNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16]);
                    obj.BalancOper.Dept.ID = this.Reader[17].ToString();
                    obj.CheckFlag = this.Reader[18].ToString();
                    obj.CheckOper.ID = this.Reader[19].ToString();
                    obj.CheckOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20]);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;

            }
            return obj;

        }


        /// <summary>
        /// 通过工号和日结截止时间段获得日结名细
        /// </summary>
        /// <param name="operCode">员工工号，所有员工为all</param>
        /// <param name="begin">最小日结截止时间</param>
        /// <param name="end">最大日结截止时间</param>
        /// <param name="isShow">是否显示已日结</param>
        /// <returns></returns>
        public ArrayList GetPrepayDayBalanceByTime(string operCode, string begin, string end, string isShow)
        {

            Object.PrepayDayBalance obj = new Object.PrepayDayBalance();
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("Fee.FeeReport.GetPrepayDayBalanceByTime", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = string.Format(strSql, operCode, begin,end,isShow);
                if (this.ExecQuery(strSql) == -1) return null;
                while (this.Reader.Read())
                {
                    obj = new Object.PrepayDayBalance();
                    obj.ID = this.Reader[0].ToString();
                    obj.BeginDate = this.Reader[1].ToString();
                    obj.EndDate = this.Reader[2].ToString();
                    obj.BalancOper.ID = this.Reader[3].ToString();
                    obj.BalancOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                    obj.RealCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    obj.QuitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    obj.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    obj.CACost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                    obj.POSCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    obj.CHCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                    obj.ORCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                    obj.FGCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                    obj.BeginInvoice = this.Reader[13].ToString();
                    obj.EndInvoice = this.Reader[14].ToString();
                    obj.PrepayNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15]);
                    obj.QuitNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16]);
                    obj.BalancOper.Dept.ID = this.Reader[17].ToString();
                    obj.CheckFlag = this.Reader[18].ToString();
                    obj.CheckOper.ID = this.Reader[19].ToString();
                    obj.CheckOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20]);
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                string Err = ex.Message;
                return null;
            }
            return al;

        }

        #region 更新住院收款员的预交金审核状态(1：成功/-1：失败)
        /// <summary>
        /// 更新住院收款员的预交金审核状态(1：成功/-1：失败)
        /// </summary>
        /// <param name="employeeID">审核人工号</param>
        /// <param name="balanceNO">结算序号</param>
        /// <param name="dtNow">审核时间</param>
        /// <returns>1：成功/-1：失败</returns>
        public int updatePrepayCheckFlag(string employeeID, string balanceNO, string dtNow)
        {
            int intReturn = 0;
            string sql = "";
            intReturn = this.Sql.GetSql("Fee.FeeReport.updatePrepayCheckFlag", ref sql);
            try
            {
                sql = string.Format(sql, employeeID, balanceNO, dtNow);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }
            intReturn = this.ExecQuery(sql);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region 取消住院收款员的预交金审核状态(1：成功/-1：失败)
        /// <summary>
        /// 取消住院收款员的预交金审核状态(1：成功/-1：失败)
        /// </summary>
        /// <param name="balanceNO">结算序号</param>
        /// <returns>1：成功/-1：失败</returns>
        public int updateCancelPrepayCheckFlag(string balanceNO)
        {
            int intReturn = 0;
            string sql = "";
            intReturn = this.Sql.GetSql("Fee.FeeReport.updateCancelPrepayCheckFlag", ref sql);
            try
            {
                sql = string.Format(sql, balanceNO);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }
            intReturn = this.ExecQuery(sql);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion
       

        /// <summary>
        /// 获得全院预交金明细
        /// </summary>
        /// <param name="Begin">开始时间</param>
        /// <param name="End">结束时间</param>
        /// <returns></returns>
        public DataSet GetFeeAllPrepayCost(string Begin, string End)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Fee.FeeReport.GetFeeAllCost", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return null;
                }
                else
                {
                    strSql = string.Format(strSql, Begin, End);
                }

                this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return ds;

        }

        /// <summary>
        /// 获取收住院预交金明细
        /// </summary>
        /// <param name="Oper"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public int GetPrepayBalanceIncomeHistory(string operCode, string Begin, string End, ref DataSet dsResult)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Fee.FeeReport.GetPrepayBalanceIncomeHistory", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                else
                {
                    strSql = string.Format(strSql, operCode, Begin, End);
                }

                this.ExecQuery(strSql, ref dsResult);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取退住院预交金明细
        /// </summary>
        /// <param name="Oper"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public int GetPrepayBalanceQuitHistory(string operCode, string Begin, string End, ref DataSet dsResult)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Fee.FeeReport.GetPrepayBalanceQuitHistory", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                else
                {
                    strSql = string.Format(strSql, operCode, Begin, End);
                }

                this.ExecQuery(strSql, ref dsResult);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取住院预交金支付明细
        /// </summary>
        /// <param name="Oper"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public int GetPrepayBalanceQuitDetail(string operCode, string Begin, string End, ref DataSet dsResult)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Fee.FeeReport.GetPrepayBalanceQuitDetail", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                else
                {
                    strSql = string.Format(strSql, operCode, Begin, End);
                }

                this.ExecQuery(strSql, ref dsResult);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return 1;
        }
        #endregion



        #region 广医四院预交金日结
        /// <summary>
        /// 获取日结金额
        /// </summary>
        /// <param name="BeginDate">起始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <param name="OperID">操作员标识</param>
        /// <param name="payMode">支付类型</param>
        /// <returns></returns>
        public string GetPrepayCost(string BeginDate, string EndDate, string OperID,string payMode)
        {
            string strCost = "";
            string strSQL = "";

            strSQL = @"select sum(prepay_cost) from FIN_IPB_INPREPAY
where  OPER_DATE >= to_date('{0}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and oper_code='{2}'
and pay_way in ('{3}')"; 

            strSQL = string.Format(strSQL, BeginDate, EndDate, OperID, payMode);
            strCost = this.ExecSqlReturnOne(strSQL);

            return strCost;
        }

        /// <summary>
        /// 预交张数
        /// </summary>
        /// <returns></returns>
        public string GetReceiptNumByState(string BeginDate, string EndDate, string state, string OperID)
        {
            string strRet = "";
            string strSQL = "";

            if (this.Sql.GetSql("Fee.FeeReport.GetReceiptNumByState", ref strSQL) == -1)
            {
                strSQL = @" 						select count(*) from Fin_Ipb_Inprepay
where   oper_code='{0}'
and OPER_DATE >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')
and prepay_state in('{3}')
--and old_recipeNo is null
and prepay_cost > 0
and ext_flag <> '2'";
            }
            strSQL = string.Format(strSQL, OperID, BeginDate, EndDate, state);
            strRet = this.ExecSqlReturnOne(strSQL);

            return strRet;
        }


        /// <summary>
        /// 预交退费张数
        /// </summary>
        /// <returns></returns>
        public string GetReceiptNumByState1(string BeginDate, string EndDate, string state, string OperID)
        {
            string strRet = "";
            string strSQL = "";
             strSQL = @" 						select count(*) from Fin_Ipb_Inprepay
where   oper_code='{0}'
and OPER_DATE >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')
and prepay_state in('{3}')
and prepay_cost < 0
and ext_flag <> '2'";

            strSQL = string.Format(strSQL, OperID, BeginDate, EndDate, state);
            strRet = this.ExecSqlReturnOne(strSQL);

            return strRet;
        }

        /// <summary>
        /// 预交实际票据子号
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="state"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        public string GetOutReceiptNormal(string BeginDate, string EndDate, string state, string OperID)
        {
            string strRet = "";
            string strSQL = "";
            ArrayList List = new ArrayList();

            strSQL = @"            select RECEIPT_NO from Fin_Ipb_Inprepay
where  oper_code='{0}'
and OPER_DATE >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')
and PREPAY_STATE in('{3}')
and ext_flag <> '2'
and prepay_cost > 0
order by RECEIPT_NO asc";


            try
            {
                strSQL = string.Format(strSQL, OperID, BeginDate, EndDate, state);

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
        /// 预交作废票据子号
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="state"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        public string GetOutReceiptBack(string BeginDate, string EndDate, string state, string OperID)
        {
            string strRet = "";
            string strSQL = "";
            ArrayList List = new ArrayList();

            strSQL = @"            select RECEIPT_NO from Fin_Ipb_Inprepay
where  oper_code='{0}'
and OPER_DATE >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')
and PREPAY_STATE in('{3}')
and ext_flag <> '2'
and prepay_cost < 0
order by RECEIPT_NO asc";


            try
            {
                strSQL = string.Format(strSQL, OperID, BeginDate, EndDate, state);

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
        /// 获取预交金日结
        /// </summary>
        /// <param name="Oper"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public DataSet GetPrepayStatListByDate(string Oper, string Begin, string End)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Fee.FeeReport.GetPrepayStatListByDate", ref strSql) == -1)
                {
                    strSql = @" 
select static_no,begin_date,end_date from FIN_IPB_PREPAYSTAT
where oper_code='{0}'
AND  end_date >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
AND  end_date <= to_date('{2}','yyyy-mm-dd HH24:mi:ss') ";
                }
                else
                {
                    strSql = string.Format(strSql, Oper, Begin, End);
                }

                this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return ds;
        }


        /// 操作员实收日报
        /// </summary>
        /// <param name="Oper"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public DataSet GetPrepayItemListByDate(string Oper, string Begin, string End)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Fee.FeeReport.GetPrepayItemListByDate", ref strSql) == -1)
                {
                    strSql = @" 
select to_char(static_no),begin_date,end_date from FIN_IPB_DAYREPORT
where  oper_code='{0}'
AND  end_date >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
AND  end_date <= to_date('{2}','yyyy-mm-dd HH24:mi:ss') ";
                }
                else
                {
                    strSql = string.Format(strSql, Oper, Begin, End);
                }

                this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 获取日结最大时间
        /// </summary>
        /// <returns></returns>
        public string GetMaxTime()
        {
            #region Sql参数
            //select max(end_date) from FIN_IPB_PREPAYSTAT
            #endregion
            string strCost = "";
            try
            {
                string strSQL = "";

                if (this.Sql.GetSql("Fee.FeeReport.GetMaxtime", ref strSQL) == -1)
                {
                    strSQL = @"		select max(end_date) from FIN_IPB_PREPAYSTAT 
 			where  PARENT_CODE= fun_get_parentcode  
			and CURRENT_CODE= fun_get_currentcode  
			and OPER_CODE='{0}'	";
                }
                else
                {
                    strSQL = string.Format(strSQL, this.Operator.ID);
                }
                strCost = this.ExecSqlReturnOne(strSQL);
            }
            catch { }

            return strCost;
        }


        /// <summary>
        /// 转押金
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        public string GetTransCost(string BeginDate, string EndDate, string OperID)
        {
            string strRet = "";
            string strSQL = "";

            if (this.Sql.GetSql("Fee.FeeReport.GetTransCost", ref strSQL) == -1)
            {
                strSQL = @" 			select sum(prepay_cost) from FIN_IPB_INPREPAY 
where
PARENT_CODE= fun_get_parentcode  
and CURRENT_CODE= fun_get_currentcode   
and  TRANS_FLAG=2 and trans_code='{0}'
and trans_date >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and trans_date <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')";
            }
            strSQL = string.Format(strSQL, OperID, BeginDate, EndDate);
            strRet = this.ExecSqlReturnOne(strSQL);

            return strRet;
        }

        /// <summary>
        /// 获得票据区间的最小号
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        public string GetMinReceiptNo(string BeginDate, string EndDate, string OperID)
        {
            string strRet = "";
            string strSQL = "";

            if (this.Sql.GetSql("Fee.FeeReport.GetMinReceipt", ref strSQL) == -1)
            {
                strSQL = @"select min(receipt_no) from FIN_IPB_INPREPAY
where  PARENT_CODE= fun_get_parentcode  
and CURRENT_CODE= fun_get_currentcode  
and oper_code='{0}'
and OPER_DATE>= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE<= to_date('{2}','yyyy-mm-dd HH24:mi:ss')";
            }
            strSQL = string.Format(strSQL, OperID, BeginDate, EndDate);
            strRet = this.ExecSqlReturnOne(strSQL);

            return strRet;
        }


        /// <summary>
        /// 预交张数
        /// </summary>
        /// <returns></returns>
        public string GetReceiptNum(string BeginDate, string EndDate, string OperID)
        {
            string strRet = "";
            string strSQL = "";

            if (this.Sql.GetSql("Fee.FeeReport.GetReceiptNum", ref strSQL) == -1)
            {
                strSQL = @"select count(*) from Fin_Ipb_Inprepay
where  PARENT_CODE= fun_get_parentcode  
and CURRENT_CODE= fun_get_currentcode 
and oper_code='{0}'
and OPER_DATE >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')";
            }
            strSQL = string.Format(strSQL, OperID, BeginDate, EndDate);
            strRet = this.ExecSqlReturnOne(strSQL);

            return strRet;
        }


        /// <summary>
        /// 预交作废票据子号
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        public string GetOutReceipt(string BeginDate, string EndDate, string OperID)
        {
            string strRet = "";
            string strSQL = "";
            ArrayList List = new ArrayList();

            if (this.Sql.GetSql("Fee.FeeReport.GetOutReceipt", ref strSQL) == -1)
            {
                strSQL = @"	select receipt_no from Fin_Ipb_Inprepay
where   oper_code='{0}'
and OPER_DATE >= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')
and PREPAY_STATE=1";
            }
            else
            {
                strSQL = string.Format(strSQL, OperID, BeginDate, EndDate);
            }
            this.ExecQuery(strSQL);
            while (this.Reader.Read())
            {
                strRet += Reader[0].ToString() + " " + "|" + " ";
            }
            return strRet;
        }

        /// 获得票据区间的最大号
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        public string GetMaxReceiptNo(string BeginDate, string EndDate, string OperID)
        {
            string strRet = "";
            string strSQL = "";

            if (this.Sql.GetSql("Fee.FeeReport.GetMaxReceipt", ref strSQL) == -1)
            {
                strSQL = @"select max(receipt_no) from FIN_IPB_INPREPAY
where   oper_code='{0}'
and OPER_DATE>= to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE<= to_date('{2}','yyyy-mm-dd HH24:mi:ss')";
            }
            strSQL = string.Format(strSQL, OperID, BeginDate, EndDate);
            strRet = this.ExecSqlReturnOne(strSQL);

            return strRet;
        }


        /// <summary>
        /// 获得预交金明细
        /// </summary>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <param name="Oper"></param>
        /// <returns></returns>
        public DataSet GetPrepayList(string Begin, string End, string Oper)
        {
            string strSql = "", strWhere = "";
            System.Data.DataSet ds = new DataSet();
            try
            {
                strSql = @" select decode(a.prepay_state,'0',a.receipt_no,'1',decode(a.old_recipeno,null,a.receipt_no,
(select receipt_no from fin_ipb_inprepay i where i.inpatient_no = a.inpatient_no and i.receipt_no = a.OLD_RECIPENO and receipt_no is not null and rownum = 1))
,a.receipt_no) 实际票据号,
to_char(a.happen_no)as 发生序号,
(select fk.patient_no from fin_ipr_inmaininfo fk where fk.inpatient_no=a.inpatient_no ) as 住院号,
a.name 姓名,a.prepay_cost prepay_cost,
 decode(a.pay_way,'CA','现金','CD','刷卡','CH','支票','PO','汇票' ) as 支付方式,
 (select com_department.dept_name from com_department 
where com_department.dept_code=a.Dept_Code 
 )||(select ' 转 '||com_department.dept_name from com_department 
where com_department.dept_code=(select t.new_dept_code dept from fin_ipr_shiftapply t
where t.nurse_cell_code = '8017'
and   t.inpatient_no = a.inpatient_no and rownum =1)
 )  as 科室 ,
decode(BALANCE_STATE,'0','未结算','1','已结算','2','已转结') 结算状态,
decode(PREPAY_STATE,'0','收取','1','作废','2','重打') 状态,
(select distinct com_employee.empl_name from com_employee 
where com_employee.empl_code=a.oper_code 
)as 操作员 ,
a.oper_date as 操作日期
from  Fin_Ipb_Inprepay a
Where  OPER_DATE >= to_date('{0}','yyyy-mm-dd HH24:mi:ss')
and OPER_DATE <= to_date('{1}','yyyy-mm-dd HH24:mi:ss')";
             
                    strSql = string.Format(strSql, Begin, End);
               
                if (Oper != "1")
                {
                   strWhere=@" and oper_code='{0}' ";
                   strWhere = string.Format(strWhere, Oper);
                   strSql += " " + strWhere;
                }
                strSql += " order by 实际票据号, 姓名,发生序号";
                this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 获得日结名细
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public SOC.Fee.DayBalance.Object.PrepayDayBalance GetPrepayStatListBystaticNo(string strID)
        {

            SOC.Fee.DayBalance.Object.PrepayDayBalance obj = new SOC.Fee.DayBalance.Object.PrepayDayBalance();
            string strSql = @"select * from FIN_IPB_PREPAYSTAT
where  static_no='{0}'";
            try
            {
                strSql = string.Format(strSql, strID);
                if (this.ExecQuery(strSql) == -1) return null;
                if (this.Reader.Read())
                {
                    obj.BalancOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader["oper_date"].ToString());
                    obj.User01 = Reader["oper_code"].ToString();//操作员
                    obj.BeginDate = Reader["begin_date"].ToString();
                    obj.EndDate = Reader["end_date"].ToString();
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

            }
            return obj;

        }

        #endregion
    }
}
