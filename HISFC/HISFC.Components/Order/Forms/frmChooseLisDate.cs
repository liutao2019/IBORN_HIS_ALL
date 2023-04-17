using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Forms
{
    //{F5D2CE04-8B2F-4099-A1B9-78E83BF393E7}
    public partial class frmChooseLisDate : Form
    {
        public frmChooseLisDate()
        {
            InitializeComponent();
            this.dtEnd.Value = DateTime.Now;
        }

        #region  属性
        public DateTime dt;
        #endregion

        #region 方法

        

        private int Save()
        {
            dt = this.dtEnd.Value;
            this.Close();
            return 1;
        }
        #endregion

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

    }
}
