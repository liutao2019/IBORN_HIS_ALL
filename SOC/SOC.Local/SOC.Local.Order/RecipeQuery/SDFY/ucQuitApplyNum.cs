using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.RecipeQuery.SDFY
{
    public partial class ucQuitApplyNum : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQuitApplyNum()
        {
            InitializeComponent();
        }

        private decimal dQuitNum = 0;
        public decimal QuitNum
        {
            get
            {
                return this.dQuitNum;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.dQuitNum = this.neuQuitNum.Value;
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
