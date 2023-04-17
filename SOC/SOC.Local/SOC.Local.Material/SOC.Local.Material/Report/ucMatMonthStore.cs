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
    public partial class ucMatMonthStore : Base.ucPrivePowerReport
    {
        public ucMatMonthStore()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.ncmbTime.Visible = true;
            this.DeptType = "PriveDept";
            this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.其他;
            this.MainTitle = "月结明细表";

            this.SQLIndexs = "SOC.Local.Material.Report.MonthStore.Detail";

            base.OnLoad(e);
        }
    }
}
