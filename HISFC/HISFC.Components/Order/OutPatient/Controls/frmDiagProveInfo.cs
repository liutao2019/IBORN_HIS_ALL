using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    public partial class frmDiagProveInfo : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmDiagProveInfo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public string CaseMain;
        public string CaseNow;
        public string Opinions;

        public void SetInfo(FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory, FS.HISFC.Models.Order.OutPatient.DiagExtend diagExtend)
        {
            if (diagExtend != null)
            {
                //this.rchCaseMain.Text = diagExtend.CaseMain;
                //this.rchCaseNow.Text = diagExtend.CaseNow;
                this.rchOpinions.Text = diagExtend.Opinions ;
            }
            else
            {
                //this.rchCaseMain.Text = caseHistory.CaseMain;
                //this.rchCaseNow.Text = caseHistory.CaseNow;
                this.rchOpinions.Text = caseHistory.CaseMain + "；" + caseHistory.CaseNow + "；" + caseHistory.User01;
            }
        }


        private void btSavet_Click(object sender, EventArgs e)
        {
            //CaseMain = this.rchCaseMain.Text;
            //CaseNow = this.rchCaseNow.Text;
            Opinions = this.rchOpinions.Text;
            //if (string.IsNullOrEmpty(CaseMain))
            //{
            //    MessageBox.Show("主诉不可为空！");
            //    return;
            //}
            //if (string.IsNullOrEmpty(CaseNow))
            //{
            //    MessageBox.Show("现病史不可为空！");
            //    return;
            //}
            if (string.IsNullOrEmpty(Opinions))
            {
                MessageBox.Show("治疗意见不可为空！");
                return;
            }
            this.Hide();
        }

        private void btSavet_Click(object sender, FormClosedEventArgs e)
        {
            //CaseMain = this.rchCaseMain.Text;
            //CaseNow = this.rchCaseNow.Text;
            Opinions = this.rchOpinions.Text;
            //if (string.IsNullOrEmpty(CaseMain))
            //{
            //    MessageBox.Show("主诉不可为空！");
            //    return;
            //}
            //if (string.IsNullOrEmpty(CaseNow))
            //{
            //    MessageBox.Show("现病史不可为空！");
            //    return;
            //}
            if (string.IsNullOrEmpty(Opinions))
            {
                MessageBox.Show("治疗意见不可为空！");
                return;
            }
            this.Hide();
        }
    }
}
