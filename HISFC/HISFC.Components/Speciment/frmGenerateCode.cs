using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmGenerateCode : Form
    {
        protected FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInPatientNo;
        private BarCodeManage barCodeMange;
        private SpecSourceManage sourceManage;

        public frmGenerateCode()
        {
            InitializeComponent();
            ucQueryInPatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            barCodeMange = new BarCodeManage();
            sourceManage = new SpecSourceManage();
        }

        private void PatientInfoMyEvent()
        {
            ArrayList arrPatient = new ArrayList();
            //ucQueryInPatientNo.query();
            arrPatient = ucQueryInPatientNo.InpatientNos;
            if (ucQueryInPatientNo.InpatientNo != null && ucQueryInPatientNo.InpatientNo.Trim() != "")
            {
                SpecSource source = new SpecSource();
                string sequence = "";
                sourceManage.GetNextSequence(ref sequence);
                source.SpecId = Convert.ToInt32(sequence);
                source.InPatientNo = ucQueryInPatientNo.InpatientNo;
                if (sourceManage.InsertSpecSource(source) == -1)
                {
                    MessageBox.Show("生成条码失败！", "条码生成");
                    return;
                }

                txtBarCode.Text = barCodeMange.GetBarCode("S").PadLeft(10, '0');
            }
        }

        private void frmGenerateCode_Load(object sender, EventArgs e)
        {
            ucQueryInPatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.PatientInfoMyEvent);
            ucQueryInPatientNo.Name = "QueryPatientNo";            
            flpQueryInpatientNo.Controls.Add(ucQueryInPatientNo);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}