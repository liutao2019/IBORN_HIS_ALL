using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Forms
{
    public partial class frmAddMsgForm : Form
    {//{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
        public FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
       
        public frmAddMsgForm()
        {
            
            InitializeComponent();
            
        }
        public void Init()
        {
            this.ucMesseageSend1.patient = this.patient;
            this.ucMesseageSend1.Init();
        }


    }
}
