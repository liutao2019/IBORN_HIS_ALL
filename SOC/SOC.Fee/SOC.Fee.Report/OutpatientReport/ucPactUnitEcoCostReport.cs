using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Fee.Report.Base;

namespace SOC.Fee.Report.OutpatientReport
{
    /// <summary>
    /// 合作医疗减免报表
    /// </summary>
    public partial class ucPactUnitEcoCostReport : ucReportBase
    {

        public ucPactUnitEcoCostReport()
        {
            InitializeComponent();
        }

        protected override DataTable Query(string strSQL, object[] param)
        {
            DataTable dtTable = null;
            switch (strSQL)
            {
                case "1":
                    dtTable = QueryEcoCostBySsdw(param);
                    break;

                case "2":
                    dtTable = QueryEcoCostByDept(param);
                    break;

                case "3":
                    dtTable = QueryEcoCostByDoct(param);
                    break;
            }

            return dtTable;
        }

        protected override void DoWithShow(ref DataTable dtResult)
        {
            

            base.DoWithShow(ref dtResult);

        }

        /// <summary>
        /// 合作医疗报表按合作单位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable QueryEcoCostBySsdw(object[] param)
        {
            DataTable dtRegCount = base.Query("SOC.Fee.Report.Outpatient.PactUnitEcoCostRegcountBySsdw", param);
            if (dtRegCount == null || dtRegCount.Rows.Count <= 0)
            {
                return dtRegCount;
            }

            bool blnHasNullRow = false;
            bool blnHasErrData = false;
            DataRow drRow = null;
            int idx = 0;
            int iRowCount = dtRegCount.Rows.Count;

            for (idx = 0; idx < iRowCount; idx++)
            {
                drRow = dtRegCount.Rows[idx];
                if (string.IsNullOrEmpty(drRow["ssdw"].ToString().Trim()))
                {
                    blnHasNullRow = true;
                    drRow["ssdw"] = " ";
                }
            }
            if (!blnHasNullRow)
            {
                drRow = dtRegCount.NewRow();
                drRow["ssdw"] = " ";
                drRow["regcount"] = 0;

                dtRegCount.Rows.Add(drRow);
            }

            DataTable dtData = base.Query("SOC.Fee.Report.Outpatient.PactUnitEcoCostBySsdw", param);
            if (dtData == null)
            {
                return dtRegCount;
            }
            else
            {
                foreach (DataRow dr in dtData.Rows)
                {
                    if (string.IsNullOrEmpty(dr["ssdw"].ToString().Trim()))
                    {
                        dr["ssdw"] = " ";
                    }
                }
            }

            List<string> lstColumn = new List<string>();
            lstColumn.Add("处方总金额");
            lstColumn.Add("医保减免");
            lstColumn.Add("自费");

            // HZ01 统计代码，print_order=1必须为挂号费， print_order=2必须为诊金
            DataTable dtStatName = dtData.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtStatName.DefaultView.Sort = "print_order";
            DataView dvStatName = dtStatName.DefaultView;

            lstColumn.Add(dvStatName[0]["fee_stat_name"].ToString()); // 挂号费
            lstColumn.Add(dvStatName[1]["fee_stat_name"].ToString()); // 诊金

            lstColumn.Add("医院减免");

            for (idx = 2; idx < dvStatName.Count; idx++)
            {
                lstColumn.Add(dvStatName[idx]["fee_stat_name"].ToString());
            }

            DataColumn dcTemp = null;
            decimal decTemp = 0;
            for(idx = 0; idx < lstColumn.Count; idx++)
            {
                dcTemp = new DataColumn(lstColumn[idx], typeof(decimal));
                dcTemp.DefaultValue = 0;

                dtRegCount.Columns.Add(dcTemp);

                decTemp = 0;

                foreach (DataRow dr in dtRegCount.Rows)
                {
                    switch (idx)
                    {
                        case 0:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(totalcost)", "ssdw = '" + dr["ssdw"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 1:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(pubcost)", "ssdw = '" + dr["ssdw"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 2:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(owncost)", "ssdw = '" + dr["ssdw"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 3:
                        case 4:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(owncost)", "ssdw = '" + dr["ssdw"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 5:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(ecocost)", "ssdw = '" + dr["ssdw"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        default:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(ecocost)", "ssdw = '" + dr["ssdw"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;
                    }
                }
            }

            dtRegCount.Columns["ssdw"].ColumnName = "合作单位";
            dtRegCount.Columns["regcount"].ColumnName = "挂号人数";
            return dtRegCount;


        }
        /// <summary>
        /// 合作医疗报表按科室
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable QueryEcoCostByDept(object[] param)
        {
            DataTable dtRegCount = base.Query("SOC.Fee.Report.Outpatient.PactUnitEcoCostRegcountByDept", param);
            if (dtRegCount == null || dtRegCount.Rows.Count <= 0)
            {
                return dtRegCount;
            }

            bool blnHasNullRow = false;
            bool blnHasErrData = false;
            DataRow drRow = null;
            int idx = 0;
            int iRowCount = dtRegCount.Rows.Count;

            for (idx = 0; idx < iRowCount; idx++)
            {
                drRow = dtRegCount.Rows[idx];
                if (string.IsNullOrEmpty(drRow["dept_name"].ToString().Trim()))
                {
                    blnHasNullRow = true;
                    drRow["dept_name"] = " ";
                }
            }
            //if (!blnHasNullRow)
            //{
            //    drRow = dtRegCount.NewRow();
            //    drRow["dept_name"] = " ";
            //    drRow["regcount"] = 0;

            //    dtRegCount.Rows.Add(drRow);
            //}

            DataTable dtData = base.Query("SOC.Fee.Report.Outpatient.PactUnitEcoCostByDept", param);
            if (dtData == null)
            {
                return dtRegCount;
            }
            else
            {
                foreach (DataRow dr in dtData.Rows)
                {
                    if (string.IsNullOrEmpty(dr["dept_name"].ToString().Trim()))
                    {
                        dr["dept_name"] = " ";
                    }
                }
            }

            List<string> lstColumn = new List<string>();
            lstColumn.Add("处方总金额");
            lstColumn.Add("医保减免");
            lstColumn.Add("自费");

            // HZ01 统计代码，print_order=1必须为挂号费， print_order=2必须为诊金
            DataTable dtStatName = dtData.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtStatName.DefaultView.Sort = "print_order";
            DataView dvStatName = dtStatName.DefaultView;

            lstColumn.Add(dvStatName[0]["fee_stat_name"].ToString()); // 挂号费
            lstColumn.Add(dvStatName[1]["fee_stat_name"].ToString()); // 诊金

            lstColumn.Add("医院减免");

            for (idx = 2; idx < dvStatName.Count; idx++)
            {
                lstColumn.Add(dvStatName[idx]["fee_stat_name"].ToString());
            }

            DataColumn dcTemp = null;
            decimal decTemp = 0;
            for (idx = 0; idx < lstColumn.Count; idx++)
            {
                dcTemp = new DataColumn(lstColumn[idx], typeof(decimal));
                dcTemp.DefaultValue = 0;

                dtRegCount.Columns.Add(dcTemp);

                decTemp = 0;

                foreach (DataRow dr in dtRegCount.Rows)
                {
                    switch (idx)
                    {
                        case 0:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(totalcost)", "dept_name = '" + dr["dept_name"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 1:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(pubcost)", "dept_name = '" + dr["dept_name"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 2:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(owncost)", "dept_name = '" + dr["dept_name"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 3:
                        case 4:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(owncost)", "dept_name = '" + dr["dept_name"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 5:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(ecocost)", "dept_name = '" + dr["dept_name"].ToString() + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        default:
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(ecocost)", "dept_name = '" + dr["dept_name"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'"));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;
                    }
                }
            }

            dtRegCount.Columns["dept_name"].ColumnName = "科室";
            dtRegCount.Columns["regcount"].ColumnName = "挂号人数";
            return dtRegCount;
        }

        /// <summary>
        /// 合作医疗报表按医生患者
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable QueryEcoCostByDoct(object[] param)
        {
            DataTable dtRegCount = base.Query("SOC.Fee.Report.Outpatient.PactUnitEcoCostRegcountByDoct", param);
            if (dtRegCount == null || dtRegCount.Rows.Count <= 0)
            {
                return dtRegCount;
            }

            bool blnHasNullRow = false;
            bool blnHasErrData = false;
            DataRow drRow = null;
            int idx = 0;
            int iRowCount = dtRegCount.Rows.Count;

            //for (idx = 0; idx < iRowCount; idx++)
            //{
            //    drRow = dtRegCount.Rows[idx];
            //    if (string.IsNullOrEmpty(drRow["dept_name"].ToString().Trim()))
            //    {
            //        blnHasNullRow = true;
            //        drRow["dept_name"] = " ";
            //    }
            //}
            //if (!blnHasNullRow)
            //{
            //    drRow = dtRegCount.NewRow();
            //    drRow["dept_name"] = " ";
            //    drRow["regcount"] = 0;

            //    dtRegCount.Rows.Add(drRow);
            //}

            DataTable dtData = base.Query("SOC.Fee.Report.Outpatient.PactUnitEcoCostByDoct", param);
            if (dtData == null)
            {
                return dtRegCount;
            }
            //else
            //{
            //    foreach (DataRow dr in dtData.Rows)
            //    {
            //        if (string.IsNullOrEmpty(dr["dept_name"].ToString().Trim()))
            //        {
            //            dr["dept_name"] = " ";
            //        }
            //    }
            //}

            List<string> lstColumn = new List<string>();
            lstColumn.Add("处方总金额");
            lstColumn.Add("医保减免");
            lstColumn.Add("自费");

            // HZ01 统计代码，print_order=1必须为挂号费， print_order=2必须为诊金
            DataTable dtStatName = dtData.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtStatName.DefaultView.Sort = "print_order";
            DataView dvStatName = dtStatName.DefaultView;

            lstColumn.Add(dvStatName[0]["fee_stat_name"].ToString()); // 挂号费
            lstColumn.Add(dvStatName[1]["fee_stat_name"].ToString()); // 诊金

            lstColumn.Add("医院减免");

            for (idx = 2; idx < dvStatName.Count; idx++)
            {
                lstColumn.Add(dvStatName[idx]["fee_stat_name"].ToString());
            }

            DataColumn dcTemp = null;
            decimal decTemp = 0;
            string strFilter = "";
            for (idx = 0; idx < lstColumn.Count; idx++)
            {
                dcTemp = new DataColumn(lstColumn[idx], typeof(decimal));
                dcTemp.DefaultValue = 0;

                dtRegCount.Columns.Add(dcTemp);

                decTemp = 0;

                foreach (DataRow dr in dtRegCount.Rows)
                {
                    switch (idx)
                    {
                        case 0:
                            strFilter = "dept_name = '" + dr["dept_name"].ToString() + "' and doct_name = '" + dr["doct_name"].ToString() + "' and card_no = '" + dr["card_no"].ToString() + "'";
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(totalcost)", strFilter));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 1:
                            strFilter = "dept_name = '" + dr["dept_name"].ToString() + "' and doct_name = '" + dr["doct_name"].ToString() + "' and card_no = '" + dr["card_no"].ToString() + "'";
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(pubcost)", strFilter));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 2:
                            strFilter = "dept_name = '" + dr["dept_name"].ToString() + "' and doct_name = '" + dr["doct_name"].ToString() + "' and card_no = '" + dr["card_no"].ToString() + "'";
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(owncost)", strFilter));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 3:
                        case 4:
                            strFilter = "dept_name = '" + dr["dept_name"].ToString() + "' and doct_name = '" + dr["doct_name"].ToString() + "' and card_no = '" + dr["card_no"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'";
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(owncost)", strFilter));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        case 5:
                            strFilter = "dept_name = '" + dr["dept_name"].ToString() + "' and doct_name = '" + dr["doct_name"].ToString() + "' and card_no = '" + dr["card_no"].ToString() + "'";
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(ecocost)", strFilter));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;

                        default:
                            strFilter = "dept_name = '" + dr["dept_name"].ToString() + "' and doct_name = '" + dr["doct_name"].ToString() + "' and card_no = '" + dr["card_no"].ToString() + "' and fee_stat_name = '" + dcTemp.ColumnName + "'";
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtData.Compute("Sum(ecocost)", strFilter));
                            dr[dcTemp.ColumnName] = decTemp;
                            break;
                    }
                }
            }


            dtRegCount.Columns["dept_name"].ColumnName = "科室";
            dtRegCount.Columns["doct_name"].ColumnName = "医生";
            dtRegCount.Columns["name"].ColumnName = "姓名";
            dtRegCount.Columns["regcount"].ColumnName = "挂号人数";
            dtRegCount.Columns["card_no"].ColumnName = "病历号";
            return dtRegCount;
        }

    }
}
