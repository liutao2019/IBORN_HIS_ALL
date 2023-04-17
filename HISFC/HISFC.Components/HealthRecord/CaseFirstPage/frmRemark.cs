using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    public partial class frmRemark : Form
    {
        public frmRemark()
        {
            InitializeComponent();
        }
        private FS.HISFC.Models.HealthRecord.Base caseBase = new FS.HISFC.Models.HealthRecord.Base();

        public FS.HISFC.Models.HealthRecord.Base CaseBase
        {
            get
            {
                return caseBase;
            }
            set
            {
                caseBase = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.caseBase.PatientInfo.Memo = this.txtRemark.Text.Trim();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

    }
}
