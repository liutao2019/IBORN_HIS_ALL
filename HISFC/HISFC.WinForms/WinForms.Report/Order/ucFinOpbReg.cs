using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Order
{
    public partial class ucFinOpbReg : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinOpbReg()
        {
            InitializeComponent();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value);
        }

        private void d_localreport_reg_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.dtpBeginTime.Value = new DateTime(now.Year, now.Month, now.Day, 00, 00, 00);
            this.dtpEndTime.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        }
    }
}
