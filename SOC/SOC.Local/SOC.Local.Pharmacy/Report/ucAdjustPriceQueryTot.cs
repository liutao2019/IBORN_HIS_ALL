using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.Pharmacy.Report
{
    public partial class ucAdjustPriceQueryTot:Neusoft.SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucAdjustPriceQueryTot()
        {
            InitializeComponent();

            this.PriveClassTwos = "0304";
            this.MainTitle = "调价汇总表";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.AdjustPrice.Tot";


        }
    }
}
