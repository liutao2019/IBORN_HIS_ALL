using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ZhuHai.Report
{
    public partial class ucOutPatientDrugReturnDetail : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucOutPatientDrugReturnDetail()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.OutPatientDrugStore.DrugReturnDetail";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "6";
            this.MainTitle = "药房退药明细报表";
            this.RightAdditionTitle = "";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9";
        }
    }
}
