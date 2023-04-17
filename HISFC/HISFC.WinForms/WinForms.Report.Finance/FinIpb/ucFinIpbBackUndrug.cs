using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpb
{
    public partial class ucFinIpbBackUndrug : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbBackUndrug()
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

