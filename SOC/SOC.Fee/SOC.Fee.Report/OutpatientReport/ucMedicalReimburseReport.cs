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
    /// 门诊统筹报表
    /// </summary>
    public partial class ucMedicalReimburseReport : FS.SOC.Fee.Report.Base.ucReportBase
    {
        public ucMedicalReimburseReport()
        {
            InitializeComponent();
        }

        protected override DataTable Query(string strSQL, object[] param)
        {
            DataTable dtTable = null;
            switch (strSQL)
            {
                case "1":
                    dtTable = QueryByDept(param);
                    break;

                case "2":
                    dtTable = QueryByDoct(param);
                    break;
            }

            return dtTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable QueryByDept(object[] param)
        {
            DataTable dtReg = base.Query("SOC.Fee.Report.Outpatient.MedicalReimburseRegcountByDept", param);

            if (dtReg == null || dtReg.Rows.Count <= 0)
            {
                return dtReg;
            }

            DataTable dtDeptFee = base.Query("SOC.Fee.Report.Outpatient.MedicalReimburseByDept", param);
            if (dtDeptFee == null || dtDeptFee.Rows.Count < 0)
            {
                return dtReg;
            }

            // 处理无挂号人次，但有费用信息的科室
            DataRow[] drArr = null;
            string strTemp = null;
            string strTempOld = null;
            string strTemp1 = null;
            string strTempOld1 = null;
            DataRow drReg = null;
            foreach (DataRow row in dtDeptFee.Rows)
            {
                strTemp = row["dept_name"].ToString().Trim();
                if ((strTemp == strTempOld) || string.IsNullOrEmpty(strTemp))
                {
                    continue;
                }

                drArr = dtReg.Select("dept_name = '" + strTemp + "'");
                if (drArr == null || drArr.Length <= 0)
                {
                    drReg = dtReg.NewRow();
                    drReg["dept_name"] = strTemp;
                    drReg["reg_count"] = 0;
                    drReg["reg_count_eco"] = 0;

                    dtReg.Rows.Add(drReg);
                }
                strTempOld = strTemp;
            }

            DataSet ds = new DataSet();
            ds.Tables.Clear();
            dtReg.TableName = "dtReg";
            dtDeptFee.TableName = "dtDeptFee";
            ds.Tables.Add(dtReg);
            ds.Tables.Add(dtDeptFee);

            dtReg.PrimaryKey = new DataColumn[] { dtReg.Columns["dept_name"] };

            // 拼接合计金额 -- 处方总金额 -- 自费金额
            DataRelation drDeptFee = new DataRelation("deptFee", dtReg.PrimaryKey, new DataColumn[] { dtDeptFee.Columns["dept_name"] });
            ds.Relations.Add(drDeptFee);
            //// 处方总金额
            DataColumn dcTemp = new DataColumn("处方总金额", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dcTemp.Expression = "sum(Child(deptFee).totalcost)";
            dtReg.Columns.Add(dcTemp);
            //// 自费金额
            dcTemp = new DataColumn("自费金额", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dcTemp.Expression = "sum(Child(deptFee).owncost) + sum(Child(deptFee).paycost)";
            dtReg.Columns.Add(dcTemp);

            // 拼接费用类别 -- 统筹
            DataTable dtTemp = dtDeptFee.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtTemp.DefaultView.Sort = "print_order";
            DataView dvTemp = dtTemp.DefaultView;

            decimal decTemp = 0;
            foreach (DataRowView drv in dvTemp)
            {
                dcTemp = new DataColumn(drv["fee_stat_name"].ToString(), typeof(string));
                dcTemp.DefaultValue = 0;
                dtReg.Columns.Add(dcTemp);

                foreach (DataRow dr in dtReg.Rows)
                {
                    decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(pubcost)", "dept_name = '" + dr["dept_name"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                    dr[dcTemp.ColumnName] = decTemp;
                }
            }

            dtReg.Columns["dept_name"].ColumnName = "科室";
            dtReg.Columns["reg_count"].ColumnName = "挂号人数";
            dtReg.Columns["reg_count_eco"].ColumnName = "免挂号人数";

            return dtReg.DefaultView.ToTable();


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable QueryByDoct(object[] param)
        {
            DataTable dtReg = base.Query("SOC.Fee.Report.Outpatient.MedicalReimburseRegcountByDoct", param);

            if (dtReg == null || dtReg.Rows.Count <= 0)
            {
                return dtReg;
            }

            DataTable dtDeptFee = base.Query("SOC.Fee.Report.Outpatient.MedicalReimburseByDoct", param);
            if (dtDeptFee == null || dtDeptFee.Rows.Count < 0)
            {
                return dtReg;
            }

            // 处理无挂号人次，但有费用信息的科室
            DataRow[] drArr = null;
            string strTemp = null;
            string strTempOld = null;
            string strTemp1 = null;
            string strTempOld1 = null;
            DataRow drReg = null;
            foreach (DataRow row in dtDeptFee.Rows)
            {
                strTemp = row["dept_name"].ToString().Trim();
                strTemp1 = row["empl_name"].ToString().Trim();
                if ((strTemp == strTempOld && strTemp1 == strTempOld1) || (string.IsNullOrEmpty(strTemp) && string.IsNullOrEmpty(strTemp1)))
                {
                    continue;
                }

                drArr = dtReg.Select("dept_name = '" + strTemp + "' and empl_name = '" + strTemp1 + "'");
                if (drArr == null || drArr.Length <= 0)
                {
                    drReg = dtReg.NewRow();
                    drReg["dept_name"] = strTemp;
                    drReg["empl_name"] = strTemp1;
                    drReg["reg_count"] = 0;
                    drReg["reg_count_eco"] = 0;

                    dtReg.Rows.Add(drReg);
                }
                strTempOld = strTemp;
                strTempOld1 = strTemp1;
            }

            DataSet ds = new DataSet();
            ds.Tables.Clear();
            dtReg.TableName = "dtReg";
            dtDeptFee.TableName = "dtDeptFee";
            ds.Tables.Add(dtReg);
            ds.Tables.Add(dtDeptFee);

            dtReg.PrimaryKey = new DataColumn[] { dtReg.Columns["dept_name"], dtReg.Columns["empl_name"] };

            // 拼接合计金额 -- 处方总金额 -- 自费金额
            DataRelation drDeptFee = new DataRelation("deptFee", dtReg.PrimaryKey, new DataColumn[] { dtDeptFee.Columns["dept_name"], dtDeptFee.Columns["empl_name"] });
            ds.Relations.Add(drDeptFee);
            //// 处方总金额
            DataColumn dcTemp = new DataColumn("处方总金额", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dcTemp.Expression = "sum(Child(deptFee).totalcost)";
            dtReg.Columns.Add(dcTemp);
            //// 自费金额
            dcTemp = new DataColumn("自费金额", typeof(decimal));
            dcTemp.DefaultValue = 0;
            dcTemp.Expression = "sum(Child(deptFee).owncost) + sum(Child(deptFee).paycost)";
            dtReg.Columns.Add(dcTemp);

            // 拼接费用类别 -- 统筹
            DataTable dtTemp = dtDeptFee.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtTemp.DefaultView.Sort = "print_order";
            DataView dvTemp = dtTemp.DefaultView;

            decimal decTemp = 0;
            foreach (DataRowView drv in dvTemp)
            {
                dcTemp = new DataColumn(drv["fee_stat_name"].ToString(), typeof(string));
                dcTemp.DefaultValue = 0;
                dtReg.Columns.Add(dcTemp);

                foreach (DataRow dr in dtReg.Rows)
                {
                    decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDeptFee.Compute("Sum(pubcost)", "dept_name = '" + dr["dept_name"].ToString() + "' and empl_name = '" + dr["empl_name"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                    dr[dcTemp.ColumnName] = decTemp;
                }
            }

            dtReg.Columns["dept_name"].ColumnName = "科室";
            dtReg.Columns["empl_name"].ColumnName = "医生";
            dtReg.Columns["reg_count"].ColumnName = "挂号人数";
            dtReg.Columns["reg_count"].DataType = typeof(string);
            dtReg.Columns["reg_count_eco"].ColumnName = "免挂号人数";

            return dtReg.DefaultView.ToTable();


        }
    }
}
