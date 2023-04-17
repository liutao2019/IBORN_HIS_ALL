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
    public partial class ucOutputReportDetail : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucOutputReportDetail()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Out.OutputReport.Detail";
            this.PriveClassTwos = "0320";
            this.MainTitle = "出库明细查询";
            this.RightAdditionTitle = "";
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select c.class3_code,c.class3_name,c.class3_meaning_code,c.class3_code,'','' from com_priv_class3 c where c.class2_code = '0320'
";
            this.SumColIndexs = "10,11";
        }

        //private void ucOutputReport_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Out.OutputReport.Detail";
        //    this.PriveClassTwos = "0320";
        //    this.MainTitle = "出库明细查询";
        //    this.RightAdditionTitle = "";
        //    this.Init();
        //}
    }
}
