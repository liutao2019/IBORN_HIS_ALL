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
    public partial class ucCStoreQuery : ucSampleRecord
    {
        public ucCStoreQuery()
        {
            InitializeComponent();

            this.PriveClassTwos = "0310,0320";
            this.MainTitle = "药品结存账务";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.CStore.Static";
        }
    }
}
