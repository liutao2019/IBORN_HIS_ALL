using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.CancerReport
{
    public partial class frmCaseInput : Form
    {
        public frmCaseInput()
        {
            InitializeComponent();
        }
        public string StrCase
        {
            get
            {
                return this.rtxtCase.Text;
            }
            set
            {
                this.rtxtCase.Text = value;
            }
        }

        public int State = 0;
        public string StrTitle
        {
            set
            {
                this.lbTitle.Text = value;
            }
        }
        private void bttOk_Click(object sender, System.EventArgs e)
        {
            if (this.rtxtCase.Text == "")
            {
                return;
            }
            this.Close();
        }

        private void frmCaseInput_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.rtxtCase.Text == "")
            {
                //
                return;
            }
        }

        private void bttCancel_Click(object sender, System.EventArgs e)
        {
            State = 1;
            this.Close();
        }
    }
}
