using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    public partial class frmOrderRefundReason : Form
    {
        public frmOrderRefundReason()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public string refundReason = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            this.refundReason = string.Empty;
            if (!string.IsNullOrEmpty(this.orderRefundReason.Text.ToString()))
            {
                this.refundReason = this.orderRefundReason.Text.ToString();
                this.Hide();
            }
            else
            {
                MessageBox.Show("医嘱退费原因为空，请填写！");
            }           

        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    this.hh = string.Empty;
        //    this.orderRefundReason.Text = string.Empty;
        //    this.Hide();
        //}
    }
}
