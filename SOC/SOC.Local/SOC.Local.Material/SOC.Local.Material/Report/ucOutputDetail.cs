using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Material.Report
{
    public partial class ucOutputDetail : Base.ucPrivePowerReport
    {
        public ucOutputDetail()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.DeptType = "PriveDept";
            this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.科室;
            this.MainTitle = "出库明细表";

            this.SQLIndexs = "SOC.Local.Material.Report.OutputDetail";

            base.OnLoad(e);
        }
    }
}
