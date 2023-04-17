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
    public partial class ucSendDrugReportDetail : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucSendDrugReportDetail()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugRepot.Detail";
            this.PriveClassTwos = "0320";
            this.MainTitle = "药房消耗明细查询";
            this.RightAdditionTitle = "";
        }

        //private void ucSendDrugReport_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugRepot.Detail";
        //    this.PriveClassTwos = "0320";
        //    this.MainTitle = "药房消耗明细查询";
        //    this.RightAdditionTitle = "";
        //    this.Init();
        //}
    }
}
