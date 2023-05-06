using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HISTIMEJOB
{
    /// <summary>
    /// 计算住院发生 未清费用报表 2012 02 26 by song.xf
    /// </summary>
    public class PateintFee : Neusoft.FrameWork.Management.Database, IJob
    {
        #region IJob 成员

        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public int Start()
        {
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.BuildPatientFee(DateTime.Now.AddDays(-1))==-1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "执行统计在院病人医疗费用出错"+this.Err;
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.BuildBoadFee(DateTime.Now.AddDays(-1)) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "执行统计在院病人膳食费用出错!"+this.Err;
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        #endregion
 

        /// <summary>
        /// 查询每日发生额，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryDailyFee(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintFee.QueryDailyFee.QueryDailyFee", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintFee.QueryDailyFee.QueryDailyFee";
                return null;
            }
            sql = string.Format(sql, statDate.Date.ToString(), statDate.Date.AddDays(1).AddSeconds(-1).ToString());
            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询每日结算额，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryDailyBalanceFee(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintFee.QueryDailyBalanceFee", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintFee.QueryDailyBalanceFee";
                return null;
            }
            sql = string.Format(sql, statDate.Date.ToString(), statDate.Date.AddDays(1).AddSeconds(-1).ToString());

            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询未结费用，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryUnbanlceFee(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintPrepay.QueryNobanlceFee", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintPrepay.QueryNobanlceFee";
                return null;
            }
            sql = string.Format(sql, statDate.Date.AddDays(1).AddSeconds(-1).ToString());

            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statDate"></param>
        /// <param name="ds"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private int InsertPatientFee(DateTime statDate, DataSet ds, string flag)
        {
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintPrepay.InsertPatientFee", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintPrepay.InsertPatientFee";
                return -1;
            }
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string sqlE = string.Format(sql, statDate.Date.AddHours(12).ToString(), dr[0].ToString(), dr[1].ToString(),
                        dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), flag);

                    if (this.ExecNoQuery(sqlE) < 1)
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return 1;

        }


        /// <summary>
        /// 查询每日发生额，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryDailyBoadFee(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintFee.QueryDailyBoadFee", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintFee.QueryDailyBoadFee";
                return null;
            }
            sql = string.Format(sql, statDate.Date.ToString(), statDate.Date.AddDays(1).AddSeconds(-1).ToString());
            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询每日结算额，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryDailyBalanceBoadFee(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintFee.QueryDailyBalanceBoadFee", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintFee.QueryDailyBalanceBoadFee";
                return null;
            }
            sql = string.Format(sql, statDate.Date.ToString(), statDate.Date.AddDays(1).AddSeconds(-1).ToString());

            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询未结费用，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryUnbanlceBoadFee(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintFee.QueryUnbanlceBoadFee", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintFee.QueryUnbanlceBoadFee";
                return null;
            }
            sql = string.Format(sql, statDate.Date.AddDays(1).AddSeconds(-1).ToString());

            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public int BuildPatientFee(DateTime statDate)
        {
           
            //当天发生费用信息
            DataSet dsDailyFee = this.QueryDailyFee(statDate);

            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientFee(statDate, dsDailyFee, "1") == -1)
            {
                return -1;
            }

            //当天结算信息
            dsDailyFee = this.QueryDailyBalanceFee(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
           
            if (this.InsertPatientFee(statDate, dsDailyFee, "2") == -1)
            {
                return -1;
            }

            //截止当天未清费用
            dsDailyFee = this.QueryUnbanlceFee(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientFee(statDate, dsDailyFee, "0") == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 统计膳食费用
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public int BuildBoadFee(DateTime statDate)
        {
            //当天发生费用信息
            DataSet dsDailyFee = this.QueryDailyBoadFee(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientFee(statDate, dsDailyFee, "3") == -1)
            {
                return -1;
            }

            //当天结算信息
            dsDailyFee = this.QueryDailyBalanceBoadFee(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientFee(statDate, dsDailyFee, "4") == -1)
            {
                return -1;
            }

            //截止当天未清费用
            dsDailyFee = this.QueryUnbanlceBoadFee(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientFee(statDate, dsDailyFee, "5") == -1)
            {
                return -1;
            }

            return 1;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class PateintPrepay : Neusoft.FrameWork.Management.Database, IJob
    {

        #region IJob 成员

        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public int Start()
        {
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.BuildPatientPrepay(DateTime.Now.AddDays(-1)) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "执行统计预交金费用出错!" + this.Err;
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            return -1;
        }

        #endregion


        /// <summary>
        /// 查询预交金每日发生额，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryDailyPrepay(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintPrepay.QueryDailyPrepay", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintPrepay.QueryDailyPrepay";
                return null;
            }
            sql = string.Format(sql, statDate.Date.ToString(), statDate.Date.AddDays(1).AddSeconds(-1).ToString());

            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询预交金每日结算额，按照患者汇总
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryDailyBalancePrepay(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintPrepay.QueryDailyBalancePrepay", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintPrepay.QueryDailyBalancePrepay";
                return null;
            }
            sql = string.Format(sql, statDate.Date.ToString(), statDate.Date.AddDays(1).AddSeconds(-1).ToString());

            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询未清预交金
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public DataSet QueryUnbalancePrepay(DateTime statDate)
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintPrepay.QueryUnbalancePrepay", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintPrepay.QueryUnbalancePrepay";
                return null;
            }
            sql = string.Format(sql, statDate.Date.AddDays(1).AddSeconds(-1).ToString());

            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statDate"></param>
        /// <param name="ds"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private int InsertPatientPrepay(DateTime statDate, DataSet ds, string flag)
        {
            string sql = "";
            if (this.Sql.GetSql("PatientBill.PateintPrepay.InsertPatientPrepay", ref sql) == -1)
            {
                this.Err = "没有找到sql：PatientBill.PateintPrepay.InsertPatientPrepay";
                return -1;
            }
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string sqlE = string.Format(sql, statDate.Date.AddHours(12).ToString(), dr[0].ToString(), dr[1].ToString(),
                        dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), flag);

                    if (this.ExecNoQuery(sqlE) < 1)
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statDate"></param>
        /// <returns></returns>
        public int BuildPatientPrepay(DateTime statDate)
        {
            //当天发生费用信息
            DataSet dsDailyFee = this.QueryDailyPrepay(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientPrepay(statDate, dsDailyFee, "1") == -1)
            {
                return -1;
            }

            //当天结算信息
            dsDailyFee = this.QueryDailyBalancePrepay(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientPrepay(statDate, dsDailyFee, "2") == -1)
            {
                return -1;
            }

            //截止当天未清费用
            dsDailyFee = this.QueryUnbalancePrepay(statDate);
            if (dsDailyFee == null)
            {
                return -1;
            }
            if (this.InsertPatientPrepay(statDate, dsDailyFee, "0") == -1)
            {
                return -1;
            }
            return 1;
        }
    }
}
