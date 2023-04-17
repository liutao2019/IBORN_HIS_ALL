using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using GZSI.ApiManager;

namespace GZSI.ApiControls
{
    public partial class frmDiagnose : Form
    {
        public frmDiagnose()
        {
            InitializeComponent();
        }

        private FS.HISFC.Models.Registration.Register regObj = null;
        public FS.HISFC.Models.Registration.Register RegObj
        {
            set { regObj = value; }
        }

        //诊断信息(icd10码)
        private string diagnoseId = null;
        public string DiagnoseId
        {
            get { return diagnoseId; }
            set { diagnoseId = value; }
        }
        private ArrayList icdList = new ArrayList();
        public ArrayList AlIcdlist
        {
            set { icdList = value; }
        }

        private void cmbDiagnose_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.diagnoseId = this.cmbDiagnose.Tag.ToString();
        }

        private void cmbDiagnose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.diagnoseId = this.cmbDiagnose.Tag.ToString();
                this.regObj.ClinicDiagnose = this.diagnoseId.Trim();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.diagnoseId = this.cmbDiagnose.Tag.ToString();
            this.regObj.ClinicDiagnose = this.diagnoseId.Trim();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.diagnoseId = "";
            this.regObj.ClinicDiagnose = this.diagnoseId;
            this.Close();
        }

        private void frmDiagnose_Load(object sender, EventArgs e)
        {

            if (this.cmbDiagnose.Items.Count == 0)
            {
                if (icdList != null && icdList.Count > 0)
                {
                    this.cmbDiagnose.AddItems(icdList);
                }
                else
                {
                    LocalManager siLocalMgr = new LocalManager();
                    icdList = siLocalMgr.QuerySiDisease();
                    if (icdList != null)
                    {
                        this.cmbDiagnose.AddItems(icdList);
                    }
                }
            }
            this.cmbDiagnose.Focus();

            this.cmbDiagnose.Tag = regObj.ClinicDiagnose;
        }
    }
}
