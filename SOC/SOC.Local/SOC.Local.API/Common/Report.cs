using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace FS.SOC.Local.API.Common
{
    public class Report : FS.FrameWork.Management.Database
    {
        public Report()
        {
        }

        #region 根据起止时间获取门诊挂号票据
        public int GetRegInvoiceInfo(DateTime start, DateTime end, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetRegInvoiceInfoByDate";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据起止发票号获取门诊挂号票据
        public int GetRegInvoiceInfo(string startNO, string endNO, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetRegInvoiceInfoByInvoice";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, startNO, endNO);
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据起止时间获取门诊收费票据
        public int GetFeeInvoiceInfo(DateTime start, DateTime end, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetFeeInvoiceInfoByDate";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据起止发票号获取门诊收费票据
        public int GetFeeInvoiceInfo(string startNO, string endNO, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetFeeInvoiceInfoByInvoice";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, startNO, endNO);
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据起止日期获取挂号汇总
        public int GetRegSummary(DateTime start, DateTime end, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetRegSummaryByDate";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据科室和起止日期获取挂号明细
        public int GetRegDetail(string deptID, DateTime start, DateTime end, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetRegDetailByDeptAndDate";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, deptID, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据起止日期获取挂号员缴款汇总
        public int GetRegOperPayment(DateTime start, DateTime end, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetRegOperPaymentByDate";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据操作员和起止日期获取挂号缴款明细
        public int GetRegOperPaymentDetail(string operCode, DateTime start, DateTime end, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetRegOperPaymentDetail";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, operCode, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据操作员和起止日期获取挂号员日缴款报表科室统计
        public int GetRegOperPaymentDeptDetail(string operCode, DateTime start, DateTime end, ref DataSet dsResult)
        {
            string sqlID = "GYZL.GetRegOperPaymentDeptDetail";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return -1;
            }
            sql = string.Format(sql, operCode, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据操作员和起止日期获取挂号缴款作废发票
        public ArrayList GetRegOperInvalidReg(string operCode, DateTime start, DateTime end)
        {
            DataSet dsResult = new DataSet();
            string sqlID = "GYZL.GetRegOperInvalidReg";
            string sql = null;
            if (this.Sql.GetCommonSql(sqlID, ref sql) == -1)
            {
                this.Err = "没有找到" + sqlID + "字段!";
                return null;
            }
            sql = string.Format(sql, operCode, start.ToString(), end.ToString());
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return null;
            }
            ArrayList invalidList = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = null;
            foreach (DataRow dr in dsResult.Tables[0].Rows)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.Name = dr[0].ToString();
                invalidList.Add(obj);
            }
            return invalidList;
        }
        #endregion

        #region 获取挂号员列表
        public ArrayList GetRegOperList()
        {
            DataSet dsResult = new DataSet();
            ArrayList list = new ArrayList();
            string sql = @"select e.empl_code, e.empl_name
                             from com_employee e, 
                                 (select distinct a.oper_code
                                    from fin_opr_register a) f
                                   where e.empl_code = f.oper_code";
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return null;
            }
            FS.FrameWork.Models.NeuObject obj = null;
            foreach(DataRow dr in dsResult.Tables[0].Rows)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = dr[0].ToString();
                obj.Name = dr[1].ToString();
                list.Add(obj);
            }
            return list;
        }
        #endregion
        
        private string sqlIndex = string.Empty;

        public string SqlIndex
        {
            get
            {
                return this.sqlIndex;
            }
            set
            {
                this.sqlIndex = value;
            }
        }

        public DataSet QueryDataSet()
        {
            DataSet ds = new DataSet();
            if (this.ExecQuery(this.SqlIndex, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }
    }
}
