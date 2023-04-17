using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Fee.Report.OutpatientReport
{
    /// <summary>
    /// 门诊科室收入汇总表
    /// </summary>
    public partial class ucOutPatientIncomeSummary : FS.SOC.Fee.Report.Base.ucReportBase
    {
        public ucOutPatientIncomeSummary()
        {
            InitializeComponent();
        }

        private void Init()
        {
        }


        protected override DataTable Query(string strSQL, object[] param)
        {
            DataTable dtTable = null;
            switch (strSQL)
            {
                case "1":
                    dtTable = QueryOutpatientRealIncomeByDept(param);
                    break;

                case "2":
                    dtTable = QueryOutpatientRealIncomeByDoctor(param);
                    break;
            }

            return dtTable;
        }

        /// <summary>
        /// 门诊收入报表 -- 按科室
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="strDeptID">科室编码，所有科室时="ALL"</param>
        /// <returns>返回 结果报表</returns>
        public DataTable QueryOutpatientRealIncomeByDept(object[] param)
        {

            DataTable dtReg = base.Query("SOC.Fee.Report.Outpatient.RegcountByDept", param);

            if (dtReg == null || dtReg.Rows.Count <= 0)
            {
                return dtReg;
            }

            DataTable dtDeptFee = base.Query("SOC.Fee.Report.Outpatient.RealIncomeByDept", param);

            if (dtDeptFee == null || dtDeptFee.Rows.Count < 0)
            {
                return dtReg;
            }

            // 处理无挂号人次，但有费用信息的科室
            DataRow[] drArr1 = null;
            string strTemp = null;
            string strTempOld = null;
            string strTemp1 = null;
            string strTempOld1 = null;
            DataRow drReg = null;
            foreach (DataRow row in dtDeptFee.Rows)
            {
                strTemp = row["dept_code"].ToString().Trim();
                strTemp1 = row["科室名称"].ToString().Trim();
                if ((strTemp == strTempOld && strTemp1 == strTempOld1) || (string.IsNullOrEmpty(strTemp) && string.IsNullOrEmpty(strTemp1)))
                {
                    continue;
                }

                drArr1 = dtReg.Select("dept_code = '" + strTemp + "' and 科室名称 = '" + strTemp1 + "'");
                if (drArr1 == null || drArr1.Length <= 0)
                {
                    drReg = dtReg.NewRow();
                    drReg["dept_code"] = strTemp;
                    drReg["科室名称"] = strTemp1;
                    dtReg.Rows.Add(drReg);
                }
                strTempOld = strTemp;
                strTempOld1 = strTemp1;
            }

            DataTable dtColumn = dtDeptFee.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtColumn.DefaultView.Sort = "print_order";
            dtColumn = dtColumn.DefaultView.ToTable();


            DataSet ds = new DataSet();
            ds.Tables.Clear();
            dtReg.TableName = "dtReg";
            dtDeptFee.TableName = "dtDeptFee";
            ds.Tables.Add(dtReg);
            ds.Tables.Add(dtDeptFee);

            // 增加合计金额 = 所有分类费用之和
            DataColumn dcTotal = new DataColumn("合计金额", typeof(decimal));
            dcTotal.DefaultValue = 0;
            dtReg.Columns.Add(dcTotal);

            decimal decTemp = 0;
            string strFilter = "";
            DataColumn dcTemp = null;
            decimal decDrug = 0;

            foreach (DataRow drColumn in dtColumn.Rows)
            {
                string ColName = drColumn["fee_stat_name"].ToString();
                dcTemp = new DataColumn(ColName, typeof(decimal));
                dcTemp.DefaultValue = 0;

                dtReg.Columns.Add(dcTemp);

                decTemp = 0;
                strFilter = "";
                decDrug = 0;

                DataRow drRes = null;
                for (int idx = 0; idx < dtReg.Rows.Count; idx++)
                {
                    drRes = dtReg.Rows[idx];
                    strFilter = "dept_code = '" + drRes["dept_code"].ToString() + "' and fee_stat_name = '" + ColName + "'";
                    decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(totalcost)", strFilter));
                    drRes[ColName] = decTemp;
                }

            }
            int maxRow = dtColumn.Rows.Count ;
            string tempExpression = "";
            for (int i = 0; i < maxRow-1; i++)
            {
                tempExpression += dtColumn.Rows[i]["fee_stat_name"].ToString() + " + "; 
            }
            tempExpression+= dtColumn.Rows[maxRow-1]["fee_stat_name"].ToString();
            dcTotal.Expression = tempExpression;

            // 增加药品比例
            DataColumn dcDrugRax = new DataColumn("药比", typeof(decimal));
            dcDrugRax.DefaultValue = 0;
            dtReg.Columns.Add(dcDrugRax);


            decimal phaTotal = 0;
            string name1 = dtColumn.Rows[0]["fee_stat_name"].ToString();
            string name2 = dtColumn.Rows[1]["fee_stat_name"].ToString();
            string name3 = dtColumn.Rows[2]["fee_stat_name"].ToString();
            for (int j = 0; j < dtReg.Rows.Count;j++ )
            {
                phaTotal = FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j][name1]) + FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j][name2]) + FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j][name3]);
                if (FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j]["合计金额"]) == 0)
                {
                    dtReg.Rows[j]["药比"] = 0;
                }
                else
                {
                    dtReg.Rows[j]["药比"] = phaTotal * 100 / FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j]["合计金额"]);
                }
            }

            // 增加西药零差价比例
            dcTemp = new DataColumn("西药差价", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dtReg.Columns.Add(dcTemp);
            // 增加中成药零差价比例
            dcTemp = new DataColumn("中成药差价", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dtReg.Columns.Add(dcTemp);

            DataRow[] drArr = null;
            int  temp = 0;
            decimal decCostPrice = 0;

            foreach (DataRow drTempDept in dtReg.Rows)
            {
                drArr = dtDeptFee.Select("dept_code = '" + drTempDept["dept_code"].ToString() + "'");
                if (drArr == null || drArr.Length <= 0)
                {
                    continue;
                }

                foreach (DataRow dr in drArr)
                {
                    temp = FS.FrameWork.Function.NConvert.ToInt32(dr["print_order"]);
                    if (temp == 1)
                    {
                        decCostPrice = FS.FrameWork.Function.NConvert.ToDecimal(dr["西药差价"]);
                        drTempDept["西药差价"] = decCostPrice;
                    }
                    else if(temp==2)
                    {
                        decCostPrice = FS.FrameWork.Function.NConvert.ToDecimal(dr["中成药差价"]);
                        drTempDept["中成药差价"] = decCostPrice;
                    }
                }
            }

            DataTable dtReport = null;
            dtReport = dtReg.DefaultView.ToTable();
            dtReport.Columns.Remove("dept_code");

            return dtReport;
        }

        /// <summary>
        /// 门诊收入报表 -- 按医生
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="strDeptID">科室编码，所有科室时="ALL"</param>
        /// <returns>返回 结果报表</returns>
        public DataTable QueryOutpatientRealIncomeByDoctor(object[] param)
        {

            DataTable dtReg = base.Query("SOC.Fee.Report.Outpatient.RegcountByDoct", param);

            if (dtReg == null || dtReg.Rows.Count <= 0)
            {
                return dtReg;
            }

            DataTable dtDeptFee = base.Query("SOC.Fee.Report.Outpatient.RealIncomeByDoct", param);

            if (dtDeptFee == null || dtDeptFee.Rows.Count < 0)
            {
                return dtReg;
            }

            // 处理无挂号人次，但有费用信息的科室与医生
            DataRow[] drArr1 = null;
            string strTemp = null;
            string strTempOld = null;
            string strTemp1 = null;
            string strTempOld1 = null;
            DataRow drReg = null;
            foreach (DataRow row in dtDeptFee.Rows)
            {
                strTemp = row["dept_code"].ToString().Trim();
                strTemp1 = row["doct_code"].ToString().Trim();
                if ((strTemp == strTempOld && strTemp1 == strTempOld1) || (string.IsNullOrEmpty(strTemp) && string.IsNullOrEmpty(strTemp1)))
                {
                    continue;
                }

                drArr1 = dtReg.Select("dept_code = '" + strTemp + "' and doct_code = '" + strTemp1 + "'");
                if (drArr1 == null || drArr1.Length <= 0)
                {
                    drReg = dtReg.NewRow();
                    drReg["dept_code"] = strTemp;
                    drReg["科室名称"] = row["科室名称"].ToString().Trim();
                    drReg["doct_code"] = strTemp1;
                    drReg["医生"] = row["医生"].ToString().Trim();

                    dtReg.Rows.Add(drReg);
                }
                strTempOld = strTemp;
                strTempOld1 = strTemp1;
            }

            DataTable dtColumn = dtDeptFee.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtColumn.DefaultView.Sort = "print_order";
            dtColumn = dtColumn.DefaultView.ToTable();


            DataSet ds = new DataSet();
            ds.Tables.Clear();
            dtReg.TableName = "dtReg";
            dtDeptFee.TableName = "dtDeptFee";
            ds.Tables.Add(dtReg);
            ds.Tables.Add(dtDeptFee);

            // 增加合计金额 = 所有分类费用之和
            DataColumn dcTotal = new DataColumn("合计金额", typeof(decimal));
            dcTotal.DefaultValue = 0;
            dtReg.Columns.Add(dcTotal);

            decimal decTemp = 0;
            string strFilter = "";
            DataColumn dcTemp = null;
            decimal decDrug = 0;

            foreach (DataRow drColumn in dtColumn.Rows)
            {
                string ColName = drColumn["fee_stat_name"].ToString();
                dcTemp = new DataColumn(ColName, typeof(decimal));
                dcTemp.DefaultValue = 0;

                dtReg.Columns.Add(dcTemp);

                decTemp = 0;
                strFilter = "";
                decDrug = 0;

                DataRow drRes = null;
                for (int idx = 0; idx < dtReg.Rows.Count; idx++)
                {
                    drRes = dtReg.Rows[idx];
                    strFilter = "dept_code = '" + drRes["dept_code"].ToString() + "'and doct_code = '" + drRes["doct_code"].ToString() + "' and fee_stat_name = '" + ColName + "'";
                    decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(totalcost)", strFilter));
                    drRes[ColName] = decTemp;
                }

            }
            int maxRow = dtColumn.Rows.Count;
            string tempExpression = "";
            for (int i = 0; i < maxRow - 1; i++)
            {
                tempExpression += dtColumn.Rows[i]["fee_stat_name"].ToString() + " + ";
            }
            tempExpression += dtColumn.Rows[maxRow - 1]["fee_stat_name"].ToString();
            dcTotal.Expression = tempExpression;

            // 增加药品比例
            DataColumn dcDrugRax = new DataColumn("药比", typeof(decimal));
            dcDrugRax.DefaultValue = 0;
            dtReg.Columns.Add(dcDrugRax);


            decimal phaTotal = 0;
            string name1 = dtColumn.Rows[0]["fee_stat_name"].ToString();
            string name2 = dtColumn.Rows[1]["fee_stat_name"].ToString();
            string name3 = dtColumn.Rows[2]["fee_stat_name"].ToString();
            for (int j = 0; j < dtReg.Rows.Count; j++)
            {
                phaTotal = FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j][name1]) + FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j][name2]) + FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j][name3]);
                if (FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j]["合计金额"]) == 0)
                {
                    dtReg.Rows[j]["药比"] = 0;
                }
                else
                {
                    dtReg.Rows[j]["药比"] = phaTotal * 100 / FS.FrameWork.Function.NConvert.ToDecimal(dtReg.Rows[j]["合计金额"]);
                }
            }

            // 增加西药零差价比例
            dcTemp = new DataColumn("西药差价", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dtReg.Columns.Add(dcTemp);
            // 增加中成药零差价比例
            dcTemp = new DataColumn("中成药差价", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dtReg.Columns.Add(dcTemp);

            DataRow[] drArr = null;
            int temp = 0;
            decimal decCostPrice = 0;

            foreach (DataRow drTempDept in dtReg.Rows)
            {
                drArr = dtDeptFee.Select("dept_code = '" + drTempDept["dept_code"].ToString() + "'and doct_code = '" + drTempDept["doct_code"].ToString() + "'");
                if (drArr == null || drArr.Length <= 0)
                {
                    continue;
                }

                foreach (DataRow dr in drArr)
                {
                    temp = FS.FrameWork.Function.NConvert.ToInt32(dr["print_order"]);
                    if (temp == 1)
                    {
                        decCostPrice = FS.FrameWork.Function.NConvert.ToDecimal(dr["西药差价"]);
                        drTempDept["西药差价"] = decCostPrice;
                    }
                    else if (temp == 2)
                    {
                        decCostPrice = FS.FrameWork.Function.NConvert.ToDecimal(dr["中成药差价"]);
                        drTempDept["中成药差价"] = decCostPrice;
                    }
                }
            }

            DataTable dtReport = null;
            dtReport = dtReg.DefaultView.ToTable();
            dtReport.Columns.Remove("dept_code");
            dtReport.Columns.Remove("doct_code");

            return dtReport;
        }
        


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }
    }
}
