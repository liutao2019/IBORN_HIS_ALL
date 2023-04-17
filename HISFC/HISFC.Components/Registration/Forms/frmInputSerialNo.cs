using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration.Forms
{
    public partial class frmInputSerialNo : Form
    {
        public frmInputSerialNo()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Event_KeyDown);
            this.tbSerialNo.KeyDown += new KeyEventHandler(Event_KeyDown);
        }
        public string SerialNo { get { return this.tbSerialNo.Text; } set { this.tbSerialNo.Text = value; } }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbSerialNo.Text))
            {
                MessageBox.Show("请输入流水号");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void Event_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }
    }
}
