using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report
{
    public partial class ucWorkLoadReportMZ : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucWorkLoadReportMZ()
        {
            InitializeComponent();
            this.QueryDataWhenInit = false;
            this.SQLIndexs = "Pharmacy.NewReport.WorkLoad.WorkLoadReport";
            this.PriveClassTwos = "0320";
            this.MainTitle = "药房配发药工作量查询";
            this.RightAdditionTitle = "";
           
        }

        //private void ucWorkLoadReport_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.WorkLoad.WorkLoadReport";
        //    this.PriveClassTwos = "0320";
        //    this.MainTitle = "药房配发药工作量查询";
        //    this.RightAdditionTitle = "";
           
        //    this.Init();
        //}
    }
}
