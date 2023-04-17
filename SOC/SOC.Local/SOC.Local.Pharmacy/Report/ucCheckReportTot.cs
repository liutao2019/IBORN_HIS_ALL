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
    public partial class ucCheckReportTot : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucCheckReportTot()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Check.Tot";
            this.PriveClassTwos = "0305";
            this.MainTitle = "盘点汇总查询";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.其他;
        }

        //private void ucCheckReportTot_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Check.Tot";
        //    this.PriveClassTwos = "0305";
        //    this.MainTitle = "盘点汇总查询";
        //    this.RightAdditionTitle = "";
        //    this.ShowTypeName = myDeptType.其他;

        //    this.Init();
        //}
    }
}
