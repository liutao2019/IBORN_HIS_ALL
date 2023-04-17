using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class frmAlterFeeRate : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmAlterFeeRate()
        {
            InitializeComponent();
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            switch (this.toolBar1.Buttons.IndexOf(e.Button))
            {
                case 0:
                    this.ucAlterFeeRate1.Query();
                    break;
                case 1:
                    this.ucAlterFeeRate1.Modify();
                    break;
                case 3:
                    this.ucAlterFeeRate1.ModifyFeeDate();
                    break;
                case 5:
                    this.Close();
                    break;
            }
        }

        private void frmAlterFeeRate_Activated(object sender, System.EventArgs e)
        {
            this.ucAlterFeeRate1.Focus();
        }
    }
}
