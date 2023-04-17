using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gyzl.IOutpatientGuide
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
    }
}
