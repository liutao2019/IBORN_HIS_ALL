using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.BizLogic
{
    public class InpatientDayReport : FS.FrameWork.Management.Database
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
            string strSQL = string.Empty;
            string strReturn = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Daybalance.Inpatient.DayBalance", ref strSQL) == -1)
            {
                strSQL = FS.SOC.HISFC.OutpatientFee.Data.AbstractInaptientDayReport.Current.Insert;
            }
            try
            {
                strSQL = string.Format(strSQL, operCode, balancer, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            if (Sql.GetCommonSql("Fee.Daybalance.Inpatient.DayBalanceCancel", ref strSql) == -1)
            {
                strSql = FS.SOC.HISFC.OutpatientFee.Data.AbstractInaptientDayReport.Current.Delete;
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
        public int GetLastBalanceDate(string operCode, ref DateTime lastDate, ref string balanceNO)
        {
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractInaptientDayReport.Current.SelectLastDate, operCode);
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
        public List<FS.FrameWork.Models.NeuObject> QueryBalanceList(DateTime month,string operCode)
        {
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractInaptientDayReport.Current.SelectByMonth, month.ToString("yyyy-MM"),operCode);
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
            string sql = string.Format(FS.SOC.HISFC.OutpatientFee.Data.AbstractInaptientDayReport.Current.SelectDateByBalanceNO, balanceNO);
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
