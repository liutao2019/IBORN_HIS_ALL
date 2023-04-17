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
    public partial class ucAdjustPriceQuery : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucAdjustPriceQuery()
        {
            InitializeComponent();

            this.PriveClassTwos = "0304";
            this.MainTitle = "调价明细表";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.AdjustPrice";


        }
    }
}
