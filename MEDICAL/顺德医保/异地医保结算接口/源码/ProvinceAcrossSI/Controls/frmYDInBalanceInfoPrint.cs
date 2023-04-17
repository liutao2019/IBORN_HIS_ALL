using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProvinceAcrossSI.Controls
{
    public partial class frmYDInBalanceInfoPrint : Form
    {
        public frmYDInBalanceInfoPrint()
        {
            InitializeComponent();
        }

        public void SetValue(FS.HISFC.Models.RADT.PatientInfo p, ProvinceAcrossSI.Objects.SIPersonInfo ps)
        {
            //this.ucYDInBalanceInfo1.SetValues(p, ps);
            this.ucProvinceAcrossBalanceBill1.SetValues(p, ps);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //this.ucYDInBalanceInfo1.Print();
            this.ucProvinceAcrossBalanceBill1.Print();
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
