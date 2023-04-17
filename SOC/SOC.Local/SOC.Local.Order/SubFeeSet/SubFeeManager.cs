using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.SubFeeSet
{
    /// <summary>
    /// 附材收取日志管理（用于记录已进行首次收取附材的医嘱）
    /// </summary>
    class SubFeeManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获得操作的SQL
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="feeLog"></param>
        /// <returns></returns>
        private string MyGetSql(string strSql, SubFeeLog feeLog)
        {
            strSql = string.Format(strSql, feeLog.InpatientNo, feeLog.MoOrder, this.Operator.ID);
            return strSql;
        }

        /// <summary>
        /// 插入附材收费日志记录
        /// </summary>
        /// <param name="feeLog"></param>
        /// <returns></returns>
        public int InsertSubFeeLog(SubFeeLog feeLog)
        {
            return this.ExecNoQuery(this.MyGetSql("Met.Com.FubtblItemFeelog.Insert", feeLog));
        }

        /// <summary>
        /// 更新附材收费日志
        /// </summary>
        /// <param name="feeLog"></param>
        /// <returns></returns>
        public int UpdateSubFeeLog(SubFeeLog feeLog)
        {
            return this.ExecNoQuery(this.MyGetSql("Met.Com.FubtblItemFeelog.Update", feeLog));
        }

        /// <summary>
        /// 删除附材收费日志
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="moOrder"></param>
        /// <returns></returns>
        public int DeleteSubFeeLog(string inpatientNo, string moOrder)
        {
            return this.ExecNoQuery("Met.Com.FubtblItemFeelog.Delete", inpatientNo, moOrder);
        }

        /// <summary>
        /// 查询一条附材收费日志
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="moOrder"></param>
        /// <returns></returns>
        public SubFeeLog QuerySubFeeLog(string inpatientNo, string moOrder)
        {
            if (this.ExecQuery("Met.Com.FubtblItemFeelog.Select", inpatientNo, moOrder) == -1)
            {
                return null;
            }

            try
            {
                SubFeeLog feeLog = null;
                if (this.Reader.Read())
                {
                    feeLog = new SubFeeLog();

                    feeLog.InpatientNo = this.Reader[0].ToString();
                    feeLog.MoOrder = this.Reader[1].ToString();
                    feeLog.OperCode = this.Reader[2].ToString();
                    feeLog.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                }

                return feeLog;
            }
            catch (Exception ex)
            {
                this.Err += ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }
    }
}
