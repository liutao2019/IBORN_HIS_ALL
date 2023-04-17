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
    public partial class ucMatDeptConsuming : Base.ucPrivePowerReport
    {
        public ucMatDeptConsuming()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.DeptType = "L";
            //this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.科室;
            this.MainTitle = "科室领用情况";

            this.SQLIndexs = "SOC.Local.Material.Report.DeptConsum";

            base.OnLoad(e);
        }
    }
}
