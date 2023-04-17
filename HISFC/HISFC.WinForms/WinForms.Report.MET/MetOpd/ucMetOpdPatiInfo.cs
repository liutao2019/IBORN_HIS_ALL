using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.MET.MetOpd
{
    public partial class ucMetOpdPatiInfo : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetOpdPatiInfo()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
                return -1;
            return base.OnRetrieve(this.beginTime ,this.endTime ,employee.Dept.ID);
        }
    }
}
