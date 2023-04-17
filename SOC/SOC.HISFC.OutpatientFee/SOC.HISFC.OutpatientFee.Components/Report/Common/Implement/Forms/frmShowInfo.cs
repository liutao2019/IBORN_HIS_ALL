using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement.Forms
{
    public partial class frmShowInfo : Form
    {
        public frmShowInfo()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(frmShowInfo_FormClosing);
        }

        void frmShowInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Hide();
            }

            return base.ProcessDialogKey(keyData);
        }

    }
}
