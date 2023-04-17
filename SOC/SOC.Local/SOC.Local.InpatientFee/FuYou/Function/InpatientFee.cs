using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.FuYou.Function
{
    public class InpatientFee : FS.FrameWork.Management.Database
    {
        public InpatientFee()
        {

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
        public int QueryInpatientIncomeByDept(string reportCode, string strDeptID, DateTime dtBegin, DateTime dtEnd, List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeeStat, out DataTable dtReport)
        {
            dtReport = null;
            int iRes = 1;

            try
            {
                string strSQL = "";

                if (this.Sql.GetSql("SOC.Fee.Report.InpatientIncomeByDept", ref strSQL) == -1)
                {
                    //                    strSQL = @"select a.inhos_deptcode deptcode,
                    //       c.dept_name,
                    //       b.fee_stat_name,
                    //       sum(a.tot_cost) totalcost,
                    //       sum(a.eco_cost) ecocost,
                    //       sum(a.deftot_cost) deftalcost
                    //  from fin_ipb_income a
                    //  left outer join fin_com_feecodestat b
                    //    on a.fee_code = b.fee_code
                    //   and b.report_code = '{0}'
                    //  left outer join com_department c
                    //    on a.inhos_deptcode = c.dept_code
                    // inner join fin_ipb_daybalance d
                    //    on d.balance_no = a.balance_no
                    //   and d.check_flag = 1
                    //   and d.check_date between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and
                    //       to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
                    // where (a.inhos_deptcode = '{3}' or 'ALL' = '{3}')
                    // group by a.inhos_deptcode, c.dept_name, b.fee_stat_name
                    // order by c.dept_name";
                    strSQL = @"select (select a.dept_name
          from com_department a
         where a.dept_code = f.inhos_deptcode) as 科室,
       w.fee_stat_name as 收费分类,
       /*(select c.name
          from com_dictionary c
         where c.code = f.fee_code
           and c.type = 'MINFEE') as 最小费用,*/
       sum(f.tot_cost) as 金额
  from fin_ipb_feeinfo f, fin_com_feecodestat w
 where w.fee_code = f.fee_code
   and w.report_code = '{0}'
   and (f.balance_date between
       to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') and
       to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'))
   and f.balance_state = '1'
 group by w.fee_stat_name,/* f.fee_code, */f.inhos_deptcode
 order by f.inhos_deptcode/*, f.fee_code*/";

                }

                strSQL = string.Format(strSQL, reportCode, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));

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


                DataTable dtReportTemp = dtDeptFee.DefaultView.ToTable("InHosDeptIncome", true, new string[] { "科室" });

                ds.Tables.Clear();
                dtDeptFee.TableName = "dtDeptFee";
                ds.Tables.Add(dtDeptFee);
                ds.Tables.Add(dtReportTemp);

                DataRelation drRelation = new DataRelation("dept", dtReportTemp.Columns["科室"], dtDeptFee.Columns["科室"]);
                ds.Relations.Add(drRelation);

                // 合计金额
                DataColumn dcTemp = new DataColumn("合计金额", typeof(decimal));
                dcTemp.DefaultValue = 0;
                dcTemp.Expression = "Sum(Child(dept).金额)";
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
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(金额)", "科室 = '" + dr["科室"].ToString().Trim() + "' and 收费分类 = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                        }

                    }
                }

                // 药品比例
                //dcTemp = new DataColumn("药品比例", typeof(decimal));
                //dcTemp.DefaultValue = 0;
                //if (lstFeeStat != null && lstFeeStat.Count >= 3)
                //{
                //    // 前三例为药口
                //    dcTemp.Expression = "(" + lstFeeStat[0].FeeStat.Name + " + " + lstFeeStat[1].FeeStat.Name + " + " + lstFeeStat[2].FeeStat.Name + ") * 100 / 合计金额";
                //}
                //else
                //{
                //    dcTemp.Expression = "";
                //}
                //dtReportTemp.Columns.Add(dcTemp);

                //// 零差价
                //dcTemp = new DataColumn("零差价", typeof(decimal));
                //dcTemp.DefaultValue = 0;
                //dcTemp.Expression = "Sum(Child(dept).deftalcost)";
                //dtReportTemp.Columns.Add(dcTemp);



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

                    if (idx != dtReport.Columns.Count - 2)
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
        /// 
        /// </summary>
        /// <param name="strDeptID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public int QueryInpatientDeptIncoming(string strDeptID, DateTime dtBegin, DateTime dtEnd, ref DataSet ds)
        {
            int iRes = 1;
            try
            {
                string strSql = @"select (select a.dept_name
          from com_department a
         where a.dept_code = f.inhos_deptcode) as 科室,
       --nvl(sum(decode(w.fee_stat_cate, '01', f.tot_cost)), 0) as 挂号诊疗费,
       nvl(sum(decode(w.fee_stat_cate, '02', f.tot_cost)), 0) as 西药,
       nvl(sum(decode(w.fee_stat_cate, '03', f.tot_cost)), 0) as 中成药,
       nvl(sum(decode(w.fee_stat_cate, '04', f.tot_cost)), 0) as 中草药,
       nvl(sum(decode(w.fee_stat_cate, '05', f.tot_cost)), 0) as 检查费,
       nvl(sum(decode(w.fee_code, '005', f.tot_cost)), 0) as 黑白B超,
       nvl(sum(decode(w.fee_code, '006', f.tot_cost)), 0) as B超,
       nvl(sum(decode(w.fee_code, '007', f.tot_cost)), 0) as 心电图,
       nvl(sum(decode(w.fee_code, '008', f.tot_cost)), 0) as 其它检查费,
       nvl(sum(decode(w.fee_code, '017', f.tot_cost)), 0) as 胃肠镜检查费,
       nvl(sum(decode(w.fee_code, '018', f.tot_cost)), 0) as 五官检查费,
       nvl(sum(decode(w.fee_stat_cate, '06', f.tot_cost)), 0) as 治疗费,
       nvl(sum(decode(w.fee_code, '011', f.tot_cost)), 0) as 注射费,
       nvl(sum(decode(w.fee_code, '024', f.tot_cost)), 0) as 口腔治疗费,
       nvl(sum(decode(w.fee_code, '025', f.tot_cost)), 0) as 五官治疗费,
       nvl(sum(decode(w.fee_code, '026', f.tot_cost)), 0) as 脑电检查费,
       nvl(sum(decode(w.fee_code, '027', f.tot_cost)), 0) as 皮肤科,
       nvl(sum(decode(w.fee_code, '029', f.tot_cost)), 0) as 观察费,
       nvl(sum(decode(w.fee_code, '032', f.tot_cost)), 0) as 其它治疗费,
       nvl(sum(decode(w.fee_code, '036', f.tot_cost)), 0) as 材料治疗费,
       nvl(sum(decode(w.fee_code, '037', f.tot_cost)), 0) as 材料费,
       nvl(sum(decode(w.fee_code, '038', f.tot_cost)), 0) as 观察治疗费,
       nvl(sum(decode(w.fee_code, '039', f.tot_cost)), 0) as 注射治疗费,
       nvl(sum(decode(w.fee_code, '091', f.tot_cost)), 0) as 监护,
       nvl(sum(decode(w.fee_stat_cate, '07', f.tot_cost)), 0) as 手术总费用,
       nvl(sum(decode(w.fee_code, '040', f.tot_cost)), 0) as 手术费,
       nvl(sum(decode(w.fee_code, '041', f.tot_cost)), 0) as 接生手术术费,
       nvl(sum(decode(w.fee_code, '042', f.tot_cost)), 0) as 腹腔镜手术,
       nvl(sum(decode(w.fee_code, '043', f.tot_cost)), 0) as 口腔手术费,
       nvl(sum(decode(w.fee_code, '044', f.tot_cost)), 0) as 五官手术费,
       nvl(sum(decode(w.fee_stat_cate, '08', f.tot_cost)), 0) as 化验总费用,
       nvl(sum(decode(w.fee_code, '045', f.tot_cost)), 0) as 化验费,
       nvl(sum(decode(w.fee_stat_cate, '12', f.tot_cost)), 0) as 床位费,
       nvl(sum(decode(w.fee_stat_cate, '13', f.tot_cost)), 0) as 出车费,
       nvl(sum(decode(w.fee_stat_cate, '15', f.tot_cost)), 0) as 护婴费,
       nvl(sum(decode(w.fee_stat_cate, '17', f.tot_cost)), 0) as 其他费,
       nvl(sum(decode(w.fee_code, '030', f.tot_cost)), 0) as 空调费,
       nvl(sum(decode(w.fee_code, '031', f.tot_cost)), 0) as 护工费,
       nvl(sum(decode(w.fee_code, '046', f.tot_cost)), 0) as 陪住,
       nvl(sum(decode(w.fee_code, '047', f.tot_cost)), 0) as 电费,
       nvl(sum(decode(w.fee_code, '048', f.tot_cost)), 0) as 其他费,
       nvl(sum(decode(w.fee_code, '089', f.tot_cost)), 0) as 工本费,
       nvl(sum(decode(w.fee_code, '090', f.tot_cost)), 0) as 租金,
       nvl(sum(decode(w.fee_code, '092', f.tot_cost)), 0) as 出生证,
       nvl(sum(decode(w.fee_code, '098', f.tot_cost)), 0) as 舍入额,
       nvl(sum(decode(w.fee_stat_cate, '18', f.tot_cost)), 0) as 护理费,
       nvl(sum(decode(w.fee_stat_cate, '19', f.tot_cost)), 0) as 诊查费,
       nvl(sum(decode(w.fee_stat_cate, '20', f.tot_cost)), 0) as X光,
       nvl(sum(decode(w.fee_stat_cate, '21', f.tot_cost)), 0) as CT,
       nvl(sum(decode(w.fee_stat_cate, '22', f.tot_cost)), 0) as 特需服务费,
       nvl(sum(decode(w.fee_code, '022', f.tot_cost)), 0) as 特需服务费,
       nvl(sum(decode(w.fee_code, '023', f.tot_cost)), 0) as 高级床位费,
       nvl(sum(decode(w.fee_stat_cate, '24', f.tot_cost)), 0) as 理疗科,
       nvl(sum(decode(w.fee_stat_cate, '25', f.tot_cost)), 0) as 输血费,
       nvl(sum(decode(w.fee_stat_cate, '26', f.tot_cost)), 0) as 氧气费,
       nvl(sum(decode(w.fee_code, '034', f.tot_cost)), 0) as 氧气费,
       nvl(sum(decode(w.fee_code, '035', f.tot_cost)), 0) as 高压氧舱
  from fin_ipb_feeinfo f, fin_com_feecodestat w, fin_ipr_inmaininfo im
 where f.fee_code = w.fee_code(+)
   and f.inpatient_no = im.inpatient_no(+)
   and w.report_code = 'MZCW'
   and (f.inhos_deptcode = '{0}' or 'ALL' = '{0}')
   and im.out_date between to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') and
       to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
 group by f.inhos_deptcode
 order by f.inhos_deptcode
";
                strSql = string.Format(strSql, strDeptID, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));

                iRes = this.ExecQuery(strSql, ref ds);
                if (iRes < 0)
                {
                    this.Err = "查询住院科室费用信息失败！\r\n" + this.Err;
                    iRes = -1;

                    return iRes;
                }
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
        /// 
        /// </summary>
        /// <param name="strDeptID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public int QueryInpatientDeptIncomingBalance(string strDeptID, DateTime dtBegin, DateTime dtEnd, ref DataSet ds)
        {
            int iRes = 1;
            try
            {
                string strSql = @"select (select a.dept_name
          from com_department a
         where a.dept_code = f.inhos_deptcode) as 科室,
       --nvl(sum(decode(w.fee_stat_cate, '01', f.tot_cost)), 0) as 挂号诊疗费,
       nvl(sum(decode(w.fee_stat_cate, '02', f.tot_cost)), 0) as 西药,
       nvl(sum(decode(w.fee_stat_cate, '03', f.tot_cost)), 0) as 中成药,
       nvl(sum(decode(w.fee_stat_cate, '04', f.tot_cost)), 0) as 中草药,
       nvl(sum(decode(w.fee_stat_cate, '05', f.tot_cost)), 0) as 检查费,
       nvl(sum(decode(w.fee_code, '005', f.tot_cost)), 0) as 黑白B超,
       nvl(sum(decode(w.fee_code, '006', f.tot_cost)), 0) as B超,
       nvl(sum(decode(w.fee_code, '007', f.tot_cost)), 0) as 心电图,
       nvl(sum(decode(w.fee_code, '008', f.tot_cost)), 0) as 其它检查费,
       nvl(sum(decode(w.fee_code, '017', f.tot_cost)), 0) as 胃肠镜检查费,
       nvl(sum(decode(w.fee_code, '018', f.tot_cost)), 0) as 五官检查费,
       nvl(sum(decode(w.fee_stat_cate, '06', f.tot_cost)), 0) as 治疗费,
       nvl(sum(decode(w.fee_code, '011', f.tot_cost)), 0) as 注射费,
       nvl(sum(decode(w.fee_code, '024', f.tot_cost)), 0) as 口腔治疗费,
       nvl(sum(decode(w.fee_code, '025', f.tot_cost)), 0) as 五官治疗费,
       nvl(sum(decode(w.fee_code, '026', f.tot_cost)), 0) as 脑电检查费,
       nvl(sum(decode(w.fee_code, '027', f.tot_cost)), 0) as 皮肤科,
       nvl(sum(decode(w.fee_code, '029', f.tot_cost)), 0) as 观察费,
       nvl(sum(decode(w.fee_code, '032', f.tot_cost)), 0) as 其它治疗费,
       nvl(sum(decode(w.fee_code, '036', f.tot_cost)), 0) as 材料治疗费,
       nvl(sum(decode(w.fee_code, '037', f.tot_cost)), 0) as 材料费,
       nvl(sum(decode(w.fee_code, '038', f.tot_cost)), 0) as 观察治疗费,
       nvl(sum(decode(w.fee_code, '039', f.tot_cost)), 0) as 注射治疗费,
       nvl(sum(decode(w.fee_code, '091', f.tot_cost)), 0) as 监护,
       nvl(sum(decode(w.fee_stat_cate, '07', f.tot_cost)), 0) as 手术总费用,
       nvl(sum(decode(w.fee_code, '040', f.tot_cost)), 0) as 手术费,
       nvl(sum(decode(w.fee_code, '041', f.tot_cost)), 0) as 接生手术术费,
       nvl(sum(decode(w.fee_code, '042', f.tot_cost)), 0) as 腹腔镜手术,
       nvl(sum(decode(w.fee_code, '043', f.tot_cost)), 0) as 口腔手术费,
       nvl(sum(decode(w.fee_code, '044', f.tot_cost)), 0) as 五官手术费,
       nvl(sum(decode(w.fee_stat_cate, '08', f.tot_cost)), 0) as 化验总费用,
       nvl(sum(decode(w.fee_code, '045', f.tot_cost)), 0) as 化验费,
       nvl(sum(decode(w.fee_stat_cate, '12', f.tot_cost)), 0) as 床位费,
       nvl(sum(decode(w.fee_stat_cate, '13', f.tot_cost)), 0) as 出车费,
       nvl(sum(decode(w.fee_stat_cate, '15', f.tot_cost)), 0) as 护婴费,
       nvl(sum(decode(w.fee_stat_cate, '17', f.tot_cost)), 0) as 其他费,
       nvl(sum(decode(w.fee_code, '030', f.tot_cost)), 0) as 空调费,
       nvl(sum(decode(w.fee_code, '031', f.tot_cost)), 0) as 护工费,
       nvl(sum(decode(w.fee_code, '046', f.tot_cost)), 0) as 陪住,
       nvl(sum(decode(w.fee_code, '047', f.tot_cost)), 0) as 电费,
       nvl(sum(decode(w.fee_code, '048', f.tot_cost)), 0) as 其他费,
       nvl(sum(decode(w.fee_code, '089', f.tot_cost)), 0) as 工本费,
       nvl(sum(decode(w.fee_code, '090', f.tot_cost)), 0) as 租金,
       nvl(sum(decode(w.fee_code, '092', f.tot_cost)), 0) as 出生证,
       nvl(sum(decode(w.fee_code, '098', f.tot_cost)), 0) as 舍入额,
       nvl(sum(decode(w.fee_stat_cate, '18', f.tot_cost)), 0) as 护理费,
       nvl(sum(decode(w.fee_stat_cate, '19', f.tot_cost)), 0) as 诊查费,
       nvl(sum(decode(w.fee_stat_cate, '20', f.tot_cost)), 0) as X光,
       nvl(sum(decode(w.fee_stat_cate, '21', f.tot_cost)), 0) as CT,
       nvl(sum(decode(w.fee_stat_cate, '22', f.tot_cost)), 0) as 特需服务费,
       nvl(sum(decode(w.fee_code, '022', f.tot_cost)), 0) as 特需服务费,
       nvl(sum(decode(w.fee_code, '023', f.tot_cost)), 0) as 高级床位费,
       nvl(sum(decode(w.fee_stat_cate, '24', f.tot_cost)), 0) as 理疗科,
       nvl(sum(decode(w.fee_stat_cate, '25', f.tot_cost)), 0) as 输血费,
       nvl(sum(decode(w.fee_stat_cate, '26', f.tot_cost)), 0) as 氧气费,
       nvl(sum(decode(w.fee_code, '034', f.tot_cost)), 0) as 氧气费,
       nvl(sum(decode(w.fee_code, '035', f.tot_cost)), 0) as 高压氧舱
  from fin_ipb_feeinfo f, fin_com_feecodestat w, fin_ipr_inmaininfo im
 where f.fee_code = w.fee_code(+)
   and f.inpatient_no = im.inpatient_no(+)
   and w.report_code = 'MZCW'
   and (f.inhos_deptcode = '{0}' or 'ALL' = '{0}')
   and im.balance_date between to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') and
       to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
 group by f.inhos_deptcode
 order by f.inhos_deptcode";
                strSql = string.Format(strSql, strDeptID, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));

                iRes = this.ExecQuery(strSql, ref ds);
                if (iRes < 0)
                {
                    this.Err = "查询住院科室费用信息失败！\r\n" + this.Err;
                    iRes = -1;

                    return iRes;
                }
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
        /// 
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryFinanceAssorting()
        {
            ArrayList al = new ArrayList();
            string strSql = @"select w.report_code, w.fee_stat_cate, w.fee_stat_name, d.code, d.name
  from fin_com_feecodestat w, com_dictionary d
 where w.report_code = 'MZCW'
   and w.fee_code = d.code
   and d.type = 'MINFEE'
 order by w.fee_stat_cate, d.code";
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "查询财务分类失败！\r\n" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[1].ToString();
                    obj.Name = this.Reader[2].ToString();
                    obj.Memo = this.Reader[0].ToString();
                    obj.User01 = this.Reader[3].ToString();
                    obj.User02 = this.Reader[4].ToString();
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            catch(Exception objEx)
            {
                this.Err = "查询住院科室收入信息失败！\r\n" + objEx.Message;
                this.Reader.Close();
                return null;
            }
            return al;
        }
    }
}
