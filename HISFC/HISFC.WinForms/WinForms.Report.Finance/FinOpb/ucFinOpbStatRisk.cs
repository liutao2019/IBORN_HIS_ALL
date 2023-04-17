using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinOpb
{
    public partial class ucFinOpbStatRisk : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinOpbStatRisk()
        {
            InitializeComponent();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(base.beginTime, base.endTime);
        }


    }
}
