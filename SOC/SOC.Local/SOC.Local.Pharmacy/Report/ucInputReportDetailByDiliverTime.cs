using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report
{
    public partial class ucInputReportDetailByDiliverTime : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucInputReportDetailByDiliverTime()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InByDiliv.Detail";
            this.PriveClassTwos = "0310";
            this.MainTitle = "入库汇总查询按送单日期";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.单位;
        }
        //private void ucInputReportDetailByDiliverTime_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.InByDiliv.Detail";
        //    this.PriveClassTwos = "0310";
        //    this.MainTitle = "入库汇总查询按送单日期";
        //    this.RightAdditionTitle = "";
        //    this.ShowTypeName = myDeptType.单位;

        //    this.Init();
        //}
    }
}
