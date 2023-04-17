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
    public partial class ucSendDrugSortByCost : Base.BaseReport
    {
        public ucSendDrugSortByCost()
        {

            InitializeComponent();

            this.PriveClassTwos = "0310";
            this.MainTitle = "药品使用金额排序(口服和针剂)";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.SunnyDrug.SortByCost";

            this.IsNeedDetailData = false;
            this.QueryDataWhenInit = false;
        }
    }
}
