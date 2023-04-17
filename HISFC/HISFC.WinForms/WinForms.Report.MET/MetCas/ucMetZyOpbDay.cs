using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.MET.MetCas
{
    public partial class ucMetZyOpbDay : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetZyOpbDay()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {           
            return base.OnRetrieve(dtpBeginTime.Value.Date);
        }

    }
}
