using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.OutpatientFee.Forms
{
    public partial class frmReturnReason : Form
    {
        public frmReturnReason()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmWaitingAnswer_Load);
        }
        public string Reason = "";
        private void frmWaitingAnswer_Load(object sender, EventArgs e)
        {
            this.button1.Focus();
            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            Reason = this.rtbReason.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}