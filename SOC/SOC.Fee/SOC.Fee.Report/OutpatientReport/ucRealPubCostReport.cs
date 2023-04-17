using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Fee.Report.Base;
using System.Collections;

namespace SOC.Fee.Report.OutpatientReport
{
    /// <summary>
    /// 门诊居民医保病人结算情况报表
    /// </summary>
    public partial class ucRealPubCostReport : ucReportBase
    {
        /// <summary>
        /// 合同单位设置，格式：全部|ALL;居民医保|'3','4'...
        /// </summary>
        [Category("报表设置"), Description("合同单位设置")]
        public string PactUnit
        {
            get { return strPactUnit; }
            set { strPactUnit = value; }
        }
        string strPactUnit = string.Empty;

        public ucRealPubCostReport()
        {
            InitializeComponent();
        }

        protected override void Init()
        {
            if (string.IsNullOrEmpty(strPactUnit))
                return;

            string[] strPactArr = strPactUnit.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (strPactArr == null || strPactArr.Length <= 0)
                return;

            string[] strArr = null;
            FS.HISFC.Models.Base.PactInfo pactUnit = null;
            ArrayList arlPact = new ArrayList();
            foreach (string str in strPactArr)
            {
                if (string.IsNullOrEmpty(str))
                    continue;

                strArr = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (strArr == null || strArr.Length != 2)
                    continue;

                pactUnit = new FS.HISFC.Models.Base.PactInfo();
                pactUnit.ID = strArr[1];
                pactUnit.Name = strArr[0];

                arlPact.Add(pactUnit);
            }

            if (arlPact != null && arlPact.Count > 0)
            {
                base.SetPactUnit(arlPact);
            }
            
        }

        protected override DataTable Query(string strSQL, object[] param)
        {
            DataTable dtResult = null;
            switch (strSQL)
            {
                case "1":
                    dtResult = QueryPubCostByDept(param);
                    break;
                case "2":
                    dtResult = QueryPubCostByPaitent(param);
                    break;

                default :
                    dtResult = base.Query(strSQL, param);
                    break;
            }

            return dtResult;
        }

        /// <summary>
        /// 按科室
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable QueryPubCostByDept(object[] param)
        {
            DataTable dtTotal = base.Query("SOC.Fee.Report.Outpatient.QueryPubCostByDept", param);

            if (dtTotal == null || dtTotal.Rows.Count <= 0)
            {
                return dtTotal;
            }

            DataTable dtDetial = base.Query("SOC.Fee.Report.Outpatient.QueryPubCostByDeptDetial", param);

            DataTable dtColumn = dtDetial.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtColumn.DefaultView.Sort = "print_order";

            DataColumn dcDrugRax = new DataColumn("药品比率", typeof(decimal));
            dcDrugRax.DefaultValue = 0;
            dtTotal.Columns.Add(dcDrugRax);

            decimal decTemp = 0;
            string strFilter = "";
            DataColumn dcTemp = null;
            decimal decDrug = 0;

            DataRow drColumn = null;
            for (int idx = 0; idx < dtColumn.Rows.Count; idx++)
            {
                drColumn = dtColumn.Rows[idx];
                string ColName = drColumn["fee_stat_name"].ToString();
                dcTemp = new DataColumn(ColName, typeof(decimal));
                dcTemp.DefaultValue = 0;

                dtTotal.Columns.Add(dcTemp);

                decTemp = 0;
                strFilter = "";
                decDrug = 0;

                foreach (DataRow drRes in dtTotal.Rows)
                {
                    strFilter = "dept_code = '" + drRes["dept_code"].ToString() + "' and pact_name = '" + drRes["合同单位"].ToString() + "' and fee_stat_name = '" + ColName + "'";
                    decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDetial.Compute("Sum(pubcost)", strFilter));
                    drRes[ColName] = decTemp;

                    if (idx < 3)
                    {
                        drRes[dcDrugRax.ColumnName] = FS.FrameWork.Function.NConvert.ToDecimal(dtDetial.Compute("Sum(totalcost)", strFilter)) + 
                            FS.FrameWork.Function.NConvert.ToDecimal(drRes[dcDrugRax.ColumnName].ToString());
                    }
                }
            }

            foreach (DataRow drRes in dtTotal.Rows)
            {
                drRes[dcDrugRax.ColumnName] = FS.FrameWork.Function.NConvert.ToDecimal(drRes[dcDrugRax.ColumnName].ToString()) * 100 / FS.FrameWork.Function.NConvert.ToDecimal(drRes["处方金额"].ToString());
            }

            dtTotal.Columns.Remove("dept_code");
            dtTotal.Columns.Remove("药品比率");
            dtTotal.AcceptChanges();
            return dtTotal;
        }
        /// <summary>
        /// 按患者
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable QueryPubCostByPaitent(object[] param)
        {
            DataTable dtTotal = base.Query("SOC.Fee.Report.Outpatient.QueryPubCostByPatient", param);

            if (dtTotal == null || dtTotal.Rows.Count <= 0)
            {
                return dtTotal;
            }

            DataTable dtDetial = base.Query("SOC.Fee.Report.Outpatient.QueryPubCostByPatientDetial", param);

            DataTable dtColumn = dtDetial.DefaultView.ToTable(true, new string[] { "fee_stat_name", "print_order" });
            dtColumn.DefaultView.Sort = "print_order";

            DataColumn dcDrugRax = new DataColumn("药品比率", typeof(decimal));
            dcDrugRax.DefaultValue = 0;
            dtTotal.Columns.Add(dcDrugRax);

            decimal decTemp = 0;
            string strFilter = "";
            DataColumn dcTemp = null;
            decimal decDrug = 0;

            DataRow drColumn = null;
            for (int idx = 0; idx < dtColumn.Rows.Count; idx++)
            {
                drColumn = dtColumn.Rows[idx];
                string ColName = drColumn["fee_stat_name"].ToString();
                dcTemp = new DataColumn(ColName, typeof(decimal));
                dcTemp.DefaultValue = 0;

                dtTotal.Columns.Add(dcTemp);

                decTemp = 0;
                strFilter = "";
                decDrug = 0;

                foreach (DataRow drRes in dtTotal.Rows)
                {
                    strFilter = "dept_code = '" + drRes["dept_code"].ToString() + "' and card_no = '" + drRes["card_no"].ToString() + "' and pact_name = '" + drRes["合同单位"].ToString() + "' and fee_stat_name = '" + ColName + "'";
                    decTemp = FS.FrameWork.Function.NConvert.ToDecimal(dtDetial.Compute("Sum(pubcost)", strFilter));
                    drRes[ColName] = decTemp;

                    if (idx < 3)
                    {
                        drRes[dcDrugRax.ColumnName] = FS.FrameWork.Function.NConvert.ToDecimal(dtDetial.Compute("Sum(totalcost)", strFilter)) +
                            FS.FrameWork.Function.NConvert.ToDecimal(drRes[dcDrugRax.ColumnName].ToString());
                    }
                }
            }

            foreach (DataRow drRes in dtTotal.Rows)
            {
                drRes[dcDrugRax.ColumnName] = FS.FrameWork.Function.NConvert.ToDecimal(drRes[dcDrugRax.ColumnName].ToString()) * 100 / FS.FrameWork.Function.NConvert.ToDecimal(drRes["处方金额"].ToString());
            }

            dtTotal.Columns.Remove("dept_code");
            dtTotal.Columns.Remove("card_no");
            dtTotal.Columns.Remove("药品比率");
            dtTotal.AcceptChanges();
            return dtTotal;
        }

        
    }
}
