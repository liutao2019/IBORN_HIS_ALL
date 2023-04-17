using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientGuide
{
    public partial class frmPopPubCostBill : frmBaseForm
    {
        public frmPopPubCostBill()
        {
            InitializeComponent();
        }

        public frmPopPubCostBill(Control c)
            : base(c)
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            base.OnLoad(e);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
