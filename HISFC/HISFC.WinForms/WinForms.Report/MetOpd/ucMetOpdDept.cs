using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.MetOpd
{
    public partial class ucMetOpdDept : Common .ucQueryBaseForDataWindow 
    {
        public ucMetOpdDept()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
                return -1;
            return base.OnRetrieve(this.beginTime ,this.endTime );
        }
    }
}
