using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report.SunnyDrug
{
    public partial class ucRates : Base.BaseReport
    {
        public ucRates()
        {

            InitializeComponent();

            this.PriveClassTwos = "0310";
            this.MainTitle = "用药情况统计表";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.SunnyDrug.Rates";

            this.IsNeedDetailData = false ;
        
            this.QueryDataWhenInit = false;
        }
    }
}
