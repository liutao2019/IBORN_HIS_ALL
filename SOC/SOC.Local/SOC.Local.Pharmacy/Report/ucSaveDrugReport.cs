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
    public partial class ucSaveDrugReport : Base.BaseReport
    {
        public ucSaveDrugReport()
        {
            InitializeComponent();

            this.PriveClassTwos = "0310,0320";
            this.MainTitle = "科室节药统计表";
            this.RightAdditionTitle = "";            

            this.SQLIndexs = "SOC.Pharmacy.Report.Out.SaveDrugs";

            this.IsNeedDetailData = false;

            this.QueryDataWhenInit = false;
        }
    }
}
