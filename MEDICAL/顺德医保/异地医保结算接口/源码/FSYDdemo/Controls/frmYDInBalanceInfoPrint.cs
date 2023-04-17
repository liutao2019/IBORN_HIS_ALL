using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FoShanYDSI.Controls
{
    public partial class frmYDInBalanceInfoPrint : Form
    {
        public frmYDInBalanceInfoPrint()
        {
            InitializeComponent();
        }

        public void SetValue(FS.HISFC.Models.RADT.PatientInfo p, FoShanYDSI.Objects.SIPersonInfo ps)
        {
            this.ucYDInBalanceInfo1.SetValues(p, ps);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.ucYDInBalanceInfo1.Print();
        }
    }
}
