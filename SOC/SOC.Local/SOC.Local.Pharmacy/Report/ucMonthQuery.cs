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
    public partial class ucMonthQuery : Base.BaseReport
    {
        public ucMonthQuery()
        {
            InitializeComponent();

            this.PriveClassTwos = "0310,0320";
            this.MainTitle = "药品月结汇总表";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.Month.Static";

            
        }
    }
}
