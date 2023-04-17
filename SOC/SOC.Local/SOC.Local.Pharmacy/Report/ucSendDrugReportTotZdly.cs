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
    public partial class ucSendDrugReportTotZdly : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucSendDrugReportTotZdly()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugRepot.Tot.Zdly";
            this.PriveClassTwos = "0320";
            this.MainTitle = "药房消耗汇总查询";
            this.RightAdditionTitle = "";
        }

        //private void ucSendDrugReportTot_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugRepot.Tot";
        //    this.PriveClassTwos = "0320";
        //    this.MainTitle = "药房消耗汇总查询";
        //    this.RightAdditionTitle = "";
        //    this.Init();
        //}
    }
}
