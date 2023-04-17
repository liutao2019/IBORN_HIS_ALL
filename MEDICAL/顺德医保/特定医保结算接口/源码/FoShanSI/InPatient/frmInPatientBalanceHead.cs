using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FoShanSI.InPatient
{
    /// <summary>
    /// 门诊结算Form
    /// 张琦
    /// 2010-7
    /// </summary>
    public partial class frmInPatientBalanceHead : Form
    {
        public frmInPatientBalanceHead()
        {
            InitializeComponent();
            this.FormClosing+=new FormClosingEventHandler(frmOutPatientBalanceHead_FormClosing);
            
        }

        void frmOutPatientBalanceHead_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ucBalance != null)
            {
                if ((this.Tag == null)||(this.Tag != null&&this.Tag.ToString() == "取消结算"))
                {
                    if (ucBalance.CloseWindow() == -1)
                    {
                        e.Cancel = true;//终止关闭
                        return;
                    }
                }
            }
            
        }

        public ucInPatientBalanceHead ucBalance = null;
    }
}
