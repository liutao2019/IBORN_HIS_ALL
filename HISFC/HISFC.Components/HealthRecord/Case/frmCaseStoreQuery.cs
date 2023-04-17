using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Case
{
    public partial class frmCaseStoreQuery : Form
    {
        public frmCaseStoreQuery()
        {
            InitializeComponent();
        }
        private void frmCaseStoreQuery_Load(object sender, EventArgs e)
        {
            ucCaseStoreQuery uc = new ucCaseStoreQuery();
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Controls.Add(uc);
        }


    }
}