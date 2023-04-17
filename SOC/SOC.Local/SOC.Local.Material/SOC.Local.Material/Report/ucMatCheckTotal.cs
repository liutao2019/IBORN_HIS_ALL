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
    public partial class ucMatCheckTotal : Base.BaseReport
    {
        public ucMatCheckTotal()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            this.DeptType = "PriveDept";
            this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.QueryConditionColIndexs = "0";

            this.MainTitle = "盘点汇总表";

            this.SQLIndexs = "SOC.Local.Material.Report.CheckTotal";

            base.OnLoad(e);

            /*
            if (this.cmbDept.alItems != null && this.cmbDept.alItems.Count > 0)
            {
                FS.HISFC.Models.Base.Employee emp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                this.cmbDept.Tag = emp.Dept.ID;
            }
            */ 
        }
    }
}
