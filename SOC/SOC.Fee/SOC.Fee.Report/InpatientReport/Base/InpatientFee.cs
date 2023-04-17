using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FS.SOC.Fee.Report.InpatientReport.Base
{
    /// <summary>
    /// 住院报表业务层
    /// </summary>
    public class InpatientFee : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlID"></param>
        /// <param name="dtResult"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int QuerySQL(string sqlID, out DataTable dtResult, object[] param)
        {
            dtResult = null;

            string strSql = string.Empty;
            if (this.Sql.GetSql(sqlID, ref strSql) == -1)
            {
                this.Err = "没有找到 ID 为 [ " + sqlID + " ] 的SQL语句！";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, param);

                DataSet ds = null;
                if (this.ExecQuery(strSql, ref ds) < 0)
                {
                    this.Err = "查询信息失败！\r\n" + this.Err;
                    return -1;
                }

                dtResult = ds.Tables[0].DefaultView.ToTable();
            }
            catch (Exception objEx)
            {
                this.Err = "查询信息失败！\r\n" + objEx.Message;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 住院收入报表（应收） -- 按患者所在科室
        /// </summary>
        /// <param name="reportCode">报表类型</param>
        /// <param name="strDeptID">科室编码，所有科室时="ALL"</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="dtReport">结果报表</param>
        /// <returns>返回 1 执行成功；</returns>
        public int QueryRealInpatientIncomeByDept(string reportCode, string strDeptID, string strEmpID, DateTime dtBegin, DateTime dtEnd, List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeeStat, out DataTable dtReport)
        {
            dtReport = null;
            int iRes = 1;

            try
            {
                string strSQL = "";

                if (this.Sql.GetSql("SOC.Fee.Report.RealInpatientIncomeByDept", ref strSQL) == -1)
                {
                    strSQL = @"select a.inhos_deptcode deptcode,
       c.dept_name,
       b.fee_stat_name,
       sum(a.tot_cost) totalcost,
       sum(a.eco_cost) ecocost,
       sum(a.deftot_cost) deftalcost
  from fin_ipb_income a
  left outer join fin_com_feecodestat b
    on a.fee_code = b.fee_code
   and b.report_code = '{0}'
  left outer join com_department c
    on a.inhos_deptcode = c.dept_code
 inner join fin_ipb_daybalance d
    on d.balance_no = a.balance_no
   and d.check_flag = 1
   and d.check_date between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and
       to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 where (a.inhos_deptcode = '{3}' or 'ALL' = '{3}')
   and (a.medicalteam_code = '{4}' or 'All' = '{4}')
 group by a.inhos_deptcode, c.dept_name, b.fee_stat_name
 order by c.dept_name";

                }

                strSQL = string.Format(strSQL, reportCode, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), strDeptID, strEmpID);

                DataSet ds = null;
                iRes = this.ExecQuery(strSQL, ref ds);
                if (iRes < 0)
                {
                    this.Err = "查询住院科室费用信息失败！\r\n" + this.Err;
                    iRes = -1;

                    return iRes;
                }
                DataTable dtDeptFee = ds.Tables[0];
                if (dtDeptFee == null || dtDeptFee.Rows.Count <= 0)
                {
                    this.Err = "查询科室费用信息失败！";
                    iRes = -1;

                    return iRes;
                }


                DataTable dtReportTemp = dtDeptFee.DefaultView.ToTable("InHosDeptIncome", true, new string[] { "dept_name" });

                ds.Tables.Clear();
                dtDeptFee.TableName = "dtDeptFee";
                ds.Tables.Add(dtDeptFee);
                ds.Tables.Add(dtReportTemp);

                DataRelation drRelation = new DataRelation("dept", dtReportTemp.Columns["dept_name"], dtDeptFee.Columns["dept_name"]);
                ds.Relations.Add(drRelation);

                // 合计金额
                DataColumn dcTemp = new DataColumn("合计金额", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).totalcost)";
                dtReportTemp.Columns.Add(dcTemp);

                decimal decTemp = 0;

                if (lstFeeStat != null && lstFeeStat.Count > 0)
                {
                    FS.HISFC.Models.Fee.FeeCodeStat FeeStat = null;
                    for (int idx = 0; idx < lstFeeStat.Count; idx++)
                    {
                        FeeStat = lstFeeStat[idx];
                        dcTemp = new DataColumn(FeeStat.FeeStat.Name, typeof(decimal));
                        dcTemp.DefaultValue = 0;

                        dtReportTemp.Columns.Add(dcTemp);

                        foreach (DataRow dr in dtReportTemp.Rows)
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(totalcost)", "dept_name = '" + dr["dept_name"].ToString().Trim() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                        }

                    }
                }

                // 药品比例
                dcTemp = new DataColumn("药品比例", typeof(decimal));
                dcTemp.DefaultValue = 0;
                if (lstFeeStat != null && lstFeeStat.Count >= 3)
                {
                    // 前三例为药口
                    dcTemp.Expression = "(" + lstFeeStat[0].FeeStat.Name + " + " + lstFeeStat[1].FeeStat.Name + " + " + lstFeeStat[2].FeeStat.Name + ") * 100 / 合计金额";
                }
                else
                {
                    dcTemp.Expression = "";
                }
                dtReportTemp.Columns.Add(dcTemp);

                // 零差价
                dcTemp = new DataColumn("零差价", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).deftalcost)";
                dtReportTemp.Columns.Add(dcTemp);



                dtReport = dtReportTemp.DefaultView.ToTable();
                dtReport.Columns[0].ColumnName = "住院科室";

                // 增加最后一行汇总信息;
                DataRow drTemp = dtReport.NewRow();
                for (int idx = 0; idx < dtReport.Columns.Count; idx++)
                {
                    if (idx == 0)
                    {
                        drTemp[idx] = "合计：";
                        continue;
                    }

                    if (dtReport.Columns[idx].ColumnName == "药品比例")
                    {
                        try
                        {
                            if (lstFeeStat != null && lstFeeStat.Count >= 3)
                            {
                                // 前三例为药口
                                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[0].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[1].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[2].FeeStat.Name]);

                                drTemp[idx] = decTemp * 100 / FS.FrameWork.Function.NConvert.ToDecimal(drTemp["合计金额"]);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // 倒数第二列不需合计，药比
                        try
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtReportTemp.Compute("Sum(" + dtReport.Columns[idx].ColumnName + ")", ""));
                            drTemp[idx] = decTemp;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                dtReport.Rows.Add(drTemp);

                iRes = 1;
            }
            catch (Exception objEx)
            {
                this.Err = "查询住院科室收入信息失败！\r\n" + objEx.Message;
                iRes = -1;
                return iRes;
            }

            return iRes;
        }
        
        /// <summary>
        /// 住院收入报表(应收) -- 按住院医生
        /// </summary>
        /// <param name="reportCode">报表类型</param>
        /// <param name="strDeptID">科室编码，所有科室时="ALL"</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="dtReport">结果报表</param>
        /// <returns>返回 1 执行成功；</returns>
        public int QueryRealInpatientIncomeByInhosDoctor(string reportCode, string strDeptID, string doctor, DateTime dtBegin, DateTime dtEnd, List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeeStat, out DataTable dtReport)
        {
            dtReport = null;
            int iRes = 1;

            try
            {
                string strSQL = "";

                if (this.Sql.GetSql("SOC.Fee.Report.InpatientRealIncomeByInhosDoctor", ref strSQL) == -1)
                {
                    strSQL = @"select c.dept_name,
       nvl(e.empl_name, ' ') empl_name,
       b.fee_stat_name,
       sum(a.tot_cost) totalcost,
       sum(a.eco_cost) ecocost,
       sum(a.deftot_cost) deftalcost
  from fin_ipb_income a
  left outer join fin_com_feecodestat b
    on a.fee_code = b.fee_code
   and b.report_code = '{0}'
  left outer join com_department c
    on a.inhos_deptcode = c.dept_code
 inner join fin_ipb_daybalance d
    on d.balance_no = a.balance_no
   and d.check_flag = 1
   and d.check_date between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and
       to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
  left outer join com_employee e
    on a.medicalteam_code = e.empl_code
 where (a.inhos_deptcode = '{3}' or 'ALL' = '{3}')
   and (a.medicalteam_code = '{4}' or 'ALL' = '{4}')
 group by c.dept_name, e.empl_name, b.fee_stat_name
 order by c.dept_name, e.empl_name";

                }

                strSQL = string.Format(strSQL, reportCode, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), strDeptID, doctor);

                DataSet ds = null;
                iRes = this.ExecQuery(strSQL, ref ds);
                if (iRes < 0)
                {
                    this.Err = "查询住院科室费用信息失败！\r\n" + this.Err;
                    iRes = -1;

                    return iRes;
                }
                DataTable dtDeptFee = ds.Tables[0];
                if (dtDeptFee == null || dtDeptFee.Rows.Count <= 0)
                {
                    this.Err = "查询科室费用信息失败！无相关记录";
                    iRes = -1;

                    return iRes;
                }

                DataTable dtReportTemp = dtDeptFee.DefaultView.ToTable("InHosDeptIncome", true, new string[] { "dept_name", "empl_name" });

                ds.Tables.Clear();
                dtDeptFee.TableName = "dtDeptFee";
                ds.Tables.Add(dtDeptFee);
                ds.Tables.Add(dtReportTemp);

                DataRelation drRelation = new DataRelation("dept", new DataColumn[] { dtReportTemp.Columns["dept_name"], dtReportTemp.Columns["empl_name"] }, new DataColumn[] { dtDeptFee.Columns["dept_name"], dtDeptFee.Columns["empl_name"] });
                ds.Relations.Add(drRelation);

                // 合计金额
                DataColumn dcTemp = new DataColumn("合计金额", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).totalcost)";
                dtReportTemp.Columns.Add(dcTemp);

                decimal decTemp = 0;

                if (lstFeeStat != null && lstFeeStat.Count > 0)
                {
                    FS.HISFC.Models.Fee.FeeCodeStat FeeStat = null;
                    for (int idx = 0; idx < lstFeeStat.Count; idx++)
                    {
                        FeeStat = lstFeeStat[idx];
                        dcTemp = new DataColumn(FeeStat.FeeStat.Name, typeof(decimal));
                        dcTemp.DefaultValue = 0;

                        dtReportTemp.Columns.Add(dcTemp);

                        foreach (DataRow dr in dtReportTemp.Rows)
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(totalcost)", "dept_name = '" + dr["dept_name"].ToString().Trim() + "' and empl_name = '" + dr["empl_name"].ToString().Trim() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                        }

                    }
                }

                // 药品比例
                dcTemp = new DataColumn("药品比例", typeof(decimal));
                dcTemp.DefaultValue = 0;
                if (lstFeeStat != null && lstFeeStat.Count >= 3)
                {
                    // 前三例为药口
                    dcTemp.Expression = "(" + lstFeeStat[0].FeeStat.Name + " + " + lstFeeStat[1].FeeStat.Name + " + " + lstFeeStat[2].FeeStat.Name + ") * 100 / 合计金额";
                }
                else
                {
                    dcTemp.Expression = "";
                }
                dtReportTemp.Columns.Add(dcTemp);

                // 零差价
                dcTemp = new DataColumn("零差价", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).deftalcost)";
                dtReportTemp.Columns.Add(dcTemp);



                dtReport = dtReportTemp.DefaultView.ToTable();
                dtReport.Columns[0].ColumnName = "住院科室";
                dtReport.Columns[1].ColumnName = "医生";

                // 增加最后一行汇总信息;
                DataRow drTemp = dtReport.NewRow();
                for (int idx = 0; idx < dtReport.Columns.Count; idx++)
                {
                    if (idx == 0)
                    {
                        drTemp[idx] = "合计：";
                        continue;
                    }

                    if (dtReport.Columns[idx].ColumnName == "药品比例")
                    {
                        try
                        {
                            if (lstFeeStat != null && lstFeeStat.Count >= 3)
                            {
                                // 前三例为药口
                                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[0].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[1].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[2].FeeStat.Name]);

                                drTemp[idx] = decTemp * 100 / FS.FrameWork.Function.NConvert.ToDecimal(drTemp["合计金额"]);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // 倒数第二列不需合计，药比
                        try
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtReportTemp.Compute("Sum(" + dtReport.Columns[idx].ColumnName + ")", ""));
                            drTemp[idx] = decTemp;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                dtReport.Rows.Add(drTemp);

                iRes = 1;
            }
            catch (Exception objEx)
            {
                this.Err = "查询住院科室收入信息失败！\r\n" + objEx.Message;
                iRes = -1;
                return iRes;
            }

            return iRes;
        }


        /// <summary>
        /// 住院收入报表 -- 按患者所在科室
        /// </summary>
        /// <param name="reportCode">报表类型</param>
        /// <param name="strDeptID">科室编码，所有科室时="ALL"</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="dtReport">结果报表</param>
        /// <returns>返回 1 执行成功；</returns>
        public int QueryInpatientIncomeByDept(string reportCode, string strDeptID, string strEmpID, DateTime dtBegin, DateTime dtEnd, List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeeStat, out DataTable dtReport)
        {
            dtReport = null;
            int iRes = 1;

            try
            {
                string strSQL = "";

                if (this.Sql.GetSql("SOC.Fee.Report.InpatientIncomeByDept", ref strSQL) == -1)
                {
                    strSQL = @"select a.inhos_deptcode deptcode,
       c.dept_name,
       b.fee_stat_name,
       sum(a.tot_cost) totalcost,
       sum(a.eco_cost) ecocost,
       sum(a.deftot_cost) deftalcost
  from fin_ipb_income a
  left outer join fin_com_feecodestat b
    on a.fee_code = b.fee_code
   and b.report_code = '{0}'
  left outer join com_department c
    on a.inhos_deptcode = c.dept_code
 inner join fin_ipb_daybalance d
    on d.balance_no = a.balance_no
   and d.check_flag = 1
   and d.check_date between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and
       to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 where (a.inhos_deptcode = '{3}' or 'ALL' = '{3}')
   and (a.medicalteam_code = '{4}' or 'All' = '{4}')
 group by a.inhos_deptcode, c.dept_name, b.fee_stat_name
 order by c.dept_name";

                }

                strSQL = string.Format(strSQL, reportCode, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), strDeptID, strEmpID);

                DataSet ds = null;
                iRes = this.ExecQuery(strSQL, ref ds);
                if (iRes < 0)
                {
                    this.Err = "查询住院科室费用信息失败！\r\n" + this.Err;
                    iRes = -1;

                    return iRes;
                }
                DataTable dtDeptFee = ds.Tables[0];
                if (dtDeptFee == null || dtDeptFee.Rows.Count <= 0)
                {
                    this.Err = "查询科室费用信息失败！";
                    iRes = -1;

                    return iRes;
                }


                DataTable dtReportTemp = dtDeptFee.DefaultView.ToTable("InHosDeptIncome", true, new string[] { "dept_name" });

                ds.Tables.Clear();
                dtDeptFee.TableName = "dtDeptFee";
                ds.Tables.Add(dtDeptFee);
                ds.Tables.Add(dtReportTemp);

                DataRelation drRelation = new DataRelation("dept", dtReportTemp.Columns["dept_name"], dtDeptFee.Columns["dept_name"]);
                ds.Relations.Add(drRelation);

                // 合计金额
                DataColumn dcTemp = new DataColumn("合计金额", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).totalcost)";
                dtReportTemp.Columns.Add(dcTemp);

                decimal decTemp = 0;

                if (lstFeeStat != null && lstFeeStat.Count > 0)
                {
                    FS.HISFC.Models.Fee.FeeCodeStat FeeStat = null;
                    for (int idx = 0; idx < lstFeeStat.Count; idx++)
                    {
                        FeeStat = lstFeeStat[idx];
                        dcTemp = new DataColumn(FeeStat.FeeStat.Name, typeof(decimal));
                        dcTemp.DefaultValue = 0;

                        dtReportTemp.Columns.Add(dcTemp);

                        foreach (DataRow dr in dtReportTemp.Rows)
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(totalcost)", "dept_name = '" + dr["dept_name"].ToString().Trim() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                        }

                    }
                }

                // 药品比例
                dcTemp = new DataColumn("药品比例", typeof(decimal));
                dcTemp.DefaultValue = 0;
                if (lstFeeStat != null && lstFeeStat.Count >= 3)
                {
                    // 前三例为药口
                    dcTemp.Expression = "(" + lstFeeStat[0].FeeStat.Name + " + " + lstFeeStat[1].FeeStat.Name + " + " + lstFeeStat[2].FeeStat.Name + ") * 100 / 合计金额";
                }
                else
                {
                    dcTemp.Expression = "";
                }
                dtReportTemp.Columns.Add(dcTemp);

                // 零差价
                dcTemp = new DataColumn("零差价", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).deftalcost)";
                dtReportTemp.Columns.Add(dcTemp);



                dtReport = dtReportTemp.DefaultView.ToTable();
                dtReport.Columns[0].ColumnName = "住院科室";

                // 增加最后一行汇总信息;
                DataRow drTemp = dtReport.NewRow();
                for (int idx = 0; idx < dtReport.Columns.Count; idx++)
                {
                    if (idx == 0)
                    {
                        drTemp[idx] = "合计：";
                        continue;
                    }

                    if (dtReport.Columns[idx].ColumnName == "药品比例")
                    {
                        try
                        {
                            if (lstFeeStat != null && lstFeeStat.Count >= 3)
                            {
                                // 前三例为药口
                                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[0].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[1].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[2].FeeStat.Name]);

                                drTemp[idx] = decTemp * 100 / FS.FrameWork.Function.NConvert.ToDecimal(drTemp["合计金额"]);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // 倒数第二列不需合计，药比
                        try
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtReportTemp.Compute("Sum(" + dtReport.Columns[idx].ColumnName + ")", ""));
                            drTemp[idx] = decTemp;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                dtReport.Rows.Add(drTemp);

                iRes = 1;
            }
            catch (Exception objEx)
            {
                this.Err = "查询住院科室收入信息失败！\r\n" + objEx.Message;
                iRes = -1;
                return iRes;
            }

            return iRes;
        }

        /// <summary>
        /// 住院收入报表 -- 按住院医生
        /// </summary>
        /// <param name="reportCode">报表类型</param>
        /// <param name="strDeptID">科室编码，所有科室时="ALL"</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="dtReport">结果报表</param>
        /// <returns>返回 1 执行成功；</returns>
        public int QueryInpatientIncomeByInhosDoctor(string reportCode, string strDeptID, string doctor, DateTime dtBegin, DateTime dtEnd, List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeeStat, out DataTable dtReport)
        {
            dtReport = null;
            int iRes = 1;

            try
            {
                string strSQL = "";

                if (this.Sql.GetSql("SOC.Fee.Report.InpatientIncomeByInhosDoctor", ref strSQL) == -1)
                {
                    strSQL = @"select c.dept_name,
       nvl(e.empl_name, ' ') empl_name,
       b.fee_stat_name,
       sum(a.tot_cost) totalcost,
       sum(a.eco_cost) ecocost,
       sum(a.deftot_cost) deftalcost
  from fin_ipb_income a
  left outer join fin_com_feecodestat b
    on a.fee_code = b.fee_code
   and b.report_code = '{0}'
  left outer join com_department c
    on a.inhos_deptcode = c.dept_code
 inner join fin_ipb_daybalance d
    on d.balance_no = a.balance_no
   and d.check_flag = 1
   and d.check_date between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and
       to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
  left outer join com_employee e
    on a.medicalteam_code = e.empl_code
 where (a.inhos_deptcode = '{3}' or 'ALL' = '{3}')
   and (a.medicalteam_code = '{4}' or 'ALL' = '{4}')
 group by c.dept_name, e.empl_name, b.fee_stat_name
 order by c.dept_name, e.empl_name";

                }

                strSQL = string.Format(strSQL, reportCode, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), strDeptID, doctor);

                DataSet ds = null;
                iRes = this.ExecQuery(strSQL, ref ds);
                if (iRes < 0)
                {
                    this.Err = "查询住院科室费用信息失败！\r\n" + this.Err;
                    iRes = -1;

                    return iRes;
                }
                DataTable dtDeptFee = ds.Tables[0];
                if (dtDeptFee == null || dtDeptFee.Rows.Count <= 0)
                {
                    this.Err = "查询科室费用信息失败！无相关记录";
                    iRes = -1;

                    return iRes;
                }

                DataTable dtReportTemp = dtDeptFee.DefaultView.ToTable("InHosDeptIncome", true, new string[] { "dept_name", "empl_name" });

                ds.Tables.Clear();
                dtDeptFee.TableName = "dtDeptFee";
                ds.Tables.Add(dtDeptFee);
                ds.Tables.Add(dtReportTemp);

                DataRelation drRelation = new DataRelation("dept", new DataColumn[] { dtReportTemp.Columns["dept_name"], dtReportTemp.Columns["empl_name"] }, new DataColumn[] { dtDeptFee.Columns["dept_name"], dtDeptFee.Columns["empl_name"] });
                ds.Relations.Add(drRelation);

                // 合计金额
                DataColumn dcTemp = new DataColumn("合计金额", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).totalcost)";
                dtReportTemp.Columns.Add(dcTemp);

                decimal decTemp = 0;

                if (lstFeeStat != null && lstFeeStat.Count > 0)
                {
                    FS.HISFC.Models.Fee.FeeCodeStat FeeStat = null;
                    for (int idx = 0; idx < lstFeeStat.Count; idx++)
                    {
                        FeeStat = lstFeeStat[idx];
                        dcTemp = new DataColumn(FeeStat.FeeStat.Name, typeof(decimal));
                        dcTemp.DefaultValue = 0;

                        dtReportTemp.Columns.Add(dcTemp);

                        foreach (DataRow dr in dtReportTemp.Rows)
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(totalcost)", "dept_name = '" + dr["dept_name"].ToString().Trim() + "' and empl_name = '" + dr["empl_name"].ToString().Trim() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                        }

                    }
                }

                // 药品比例
                dcTemp = new DataColumn("药品比例", typeof(decimal));
                dcTemp.DefaultValue = 0;
                if (lstFeeStat != null && lstFeeStat.Count >= 3)
                {
                    // 前三例为药口
                    dcTemp.Expression = "(" + lstFeeStat[0].FeeStat.Name + " + " + lstFeeStat[1].FeeStat.Name + " + " + lstFeeStat[2].FeeStat.Name + ") * 100 / 合计金额";
                }
                else
                {
                    dcTemp.Expression = "";
                }
                dtReportTemp.Columns.Add(dcTemp);

                // 零差价
                dcTemp = new DataColumn("零差价", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).deftalcost)";
                dtReportTemp.Columns.Add(dcTemp);



                dtReport = dtReportTemp.DefaultView.ToTable();
                dtReport.Columns[0].ColumnName = "住院科室";
                dtReport.Columns[1].ColumnName = "医生";

                // 增加最后一行汇总信息;
                DataRow drTemp = dtReport.NewRow();
                for (int idx = 0; idx < dtReport.Columns.Count; idx++)
                {
                    if (idx == 0)
                    {
                        drTemp[idx] = "合计：";
                        continue;
                    }

                    if (dtReport.Columns[idx].ColumnName == "药品比例")
                    {
                        try
                        {
                            if (lstFeeStat != null && lstFeeStat.Count >= 3)
                            {
                                // 前三例为药口
                                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[0].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[1].FeeStat.Name]);
                                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp[lstFeeStat[2].FeeStat.Name]);

                                drTemp[idx] = decTemp * 100 / FS.FrameWork.Function.NConvert.ToDecimal(drTemp["合计金额"]);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // 倒数第二列不需合计，药比
                        try
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtReportTemp.Compute("Sum(" + dtReport.Columns[idx].ColumnName + ")", ""));
                            drTemp[idx] = decTemp;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                dtReport.Rows.Add(drTemp);

                iRes = 1;
            }
            catch (Exception objEx)
            {
                this.Err = "查询住院科室收入信息失败！\r\n" + objEx.Message;
                iRes = -1;
                return iRes;
            }

            return iRes;
        }
    }
}
