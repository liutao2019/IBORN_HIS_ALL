using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucCallDate : UserControl
    {
        public ucCallDate()
        {
            InitializeComponent();
        }

        private void neuBtnOK_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();
        }
    }
}
