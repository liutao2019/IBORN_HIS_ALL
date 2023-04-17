using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient
{
    public partial class frmPatientInfo : Form
    {
        public frmPatientInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �Ƿ�ȷ��
        /// 0:ȡ����1��ȷ�ϣ�2���˳�
        /// </summary>
        public string IsOK = "0";

        private void btOK_Click(object sender, EventArgs e)
        {
            this.IsOK = "1";
            this.Close();
        }

        private void btCancle_Click(object sender, EventArgs e)
        {
            this.IsOK = "0";
            this.Close();
        }

        private void btQuit_Click(object sender, EventArgs e)
        {
            this.IsOK = "2";
            this.Close();
        }

        public void Init(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtName.Text = patientInfo.Name;
            this.txtSex.Text = patientInfo.Sex.Name;
            this.txtIdentNo.Text = patientInfo.IDCard;
            this.txtInDate.Text = patientInfo.PVisit.InTime.ToString("yyyy-MM-dd");
            this.txtBirthday.Text = patientInfo.Birthday.ToString("yyyy-MM-dd");
            this.txtLinkMan.Text = patientInfo.Kin.Name;
            this.txtWorkAdress.Text = patientInfo.CompanyName;
        }

        private void frmPatientInfo_Load(object sender, EventArgs e)
        {

        }
    }
}