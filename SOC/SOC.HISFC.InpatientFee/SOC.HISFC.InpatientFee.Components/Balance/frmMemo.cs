using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    public partial class frmMemo : Form
    {
        public frmMemo()
        {
            InitializeComponent();
        }

        string errText = string.Empty;
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
        private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
        private void frmMemo_Load(object sender, EventArgs e)
        {
            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);
            if (!string.IsNullOrEmpty(PantientNo))
            {
                this.ucQueryInpatientNo.TextBox.Text = patientNo;
                this.ucQueryInpatientNo.query();
            }
        }


        private string patientNo = "";
        public string PantientNo
        {
            get{return patientNo;}
            set 
            {
                patientNo = value;
            }
        }

        void ucQueryInpatientNo_myEvent()
        {
            QueryByPatientNO(this.ucQueryInpatientNo.InpatientNo, ref errText);
        }
        /// <summary>
        /// 住院号回车处理
        /// </summary>
        protected virtual int QueryByPatientNO(string inpatientNO, ref string errText)
        {
            //回车触发事件
            if (inpatientNO == null || inpatientNO == "")
            {
                errText = "住院号错误，没有找到该患者";
                return -1;
            }
            FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();
            patientInfo = radtMgr.GetPatientInfo(inpatientNO);

            //赋上住院号
            this.ucQueryInpatientNo.TextBox.Text = patientInfo.PID.PatientNO;
            //赋值患者信息
            this.lblName.Text = patientInfo.Name;
            this.lblPact.Text = patientInfo.Pact.Name;
            this.lblInTime.Text = patientInfo.PVisit.InTime.ToString("yyyy-MM-dd");
            this.txtMemo.Text = patientInfo.Memo;

            return 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtMemo.Text.Trim().Length <= 0)
            {
                MessageBox.Show("备注不能为空！");
                this.txtMemo.Focus();
                return;
            }
            string memo = this.txtMemo.Text.Trim();
            if (-1 == this.inpatientFeeManager.UpdateInmainInfoMemo(patientInfo.ID, memo))
            {
                MessageBox.Show("保存备注出错！" + this.inpatientFeeManager.Err);
                return;
            }

            this.Close();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ucQueryInpatientNo.TextBox.Text = "";
            this.lblName.Text = "";
            this.lblPact.Text = "";
            this.lblInTime.Text = "";

            this.txtMemo.Text = "";
            if (-1 == this.inpatientFeeManager.UpdateInmainInfoMemo(patientInfo.ID, this.txtMemo.Text))
            {
                MessageBox.Show("保存备注出错！" + this.inpatientFeeManager.Err);
                return;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
