
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

    public partial class ucMatMonthBalance : Base.BaseReport
    {

        public ucMatMonthBalance()
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

            this.QueryDataWhenInit = false;

            this.MainTitle = "月结汇总表";
            this.SQLIndexs = "SOC.Local.Material.Report.Month.Balance";

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
