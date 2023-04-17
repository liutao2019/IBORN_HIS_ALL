using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.MetOpd
{
    public partial class ucMetOpdPlan :Common .ucQueryBaseForDataWindow 
    {
        public ucMetOpdPlan()
        {
            InitializeComponent();
        }
        protected override int RetrieveMain(params object[] args)
        {
            if (this.GetQueryTime() == -1)
                return -1;
            return base.RetrieveMain(this.beginTime ,this.endTime );
        }
    }
}
