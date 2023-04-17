using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.EMR.ZDLY.Controls
{
    public partial class ucPatientInfo : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientInfo()
        {
            InitializeComponent();
        }
        Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.PatientInfoLogic clspatient = new Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.PatientInfoLogic(); 
        private void ucPatientInfo_Load(object sender, EventArgs e)
        {
            InitData();

        }

        private void InitData()
        {
            
            this.gridControl1.DataSource =clspatient.PatientInfo();
        }
    }
}
