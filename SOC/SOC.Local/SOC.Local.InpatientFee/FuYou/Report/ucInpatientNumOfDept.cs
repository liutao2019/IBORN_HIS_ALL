using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.FuYou.Report
{
    public partial class ucInpatientNumOfDept : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucInpatientNumOfDept()
        {
            InitializeComponent();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            return base.OnRetrieve("I");
        }
    }
}
