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
    public partial class ucInputDetail : Base.ucPrivePowerReport
    {
        public ucInputDetail()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.DeptType = "PriveDept";
            this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.公司;
            this.MainTitle = "入库明细表";

            this.SQLIndexs = "SOC.Local.Material.Report.InputDetail";

            base.OnLoad(e);
        }
    }
}
