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
    public partial class ucAntibacterialDrugSortByNum : Base.BaseReport
    {
        public ucAntibacterialDrugSortByNum()
        {

            InitializeComponent();

            this.PriveClassTwos = "0310";
            this.MainTitle = "抗菌药物使用量排序";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.SunnyDrug.AntibacterialDrug.SortByNum";

            this.IsNeedDetailData = false;
          
            this.QueryDataWhenInit = false;
        }
    }
}
