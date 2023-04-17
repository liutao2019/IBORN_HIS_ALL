using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmPatientView : Form
    {

        public delegate void SelectPatientInfoDelagate(object sender);

        public event SelectPatientInfoDelagate SelectedPatientinfo = null;
        FS.HISFC.BizLogic.RADT.InPatient Inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        public frmPatientView()
        {
            InitializeComponent();
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);
        }

        void ucQueryInpatientNo1_myEvent()
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            ArrayList alPatientInfo = new ArrayList();
            alPatientInfo.Add(patientInfo);
            this.SetPatientInfo(alPatientInfo);
        }

        public void SetPatientInfo(ArrayList alPatient)
        {

            this.neuSpread1_Sheet1.RowCount = alPatient.Count;
            for (int i = 0; i < alPatient.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = alPatient[i] as FS.HISFC.Models.RADT.PatientInfo;

                this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.patientNO].Text = patientInfo.PID.PatientNO;
                this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.name].Text = patientInfo.Name;
                this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.dept].Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.nurseCell].Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
                this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.inDate].Text = patientInfo.PVisit.InTime.ToString();
                this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.outDate].Text = patientInfo.PVisit.OutTime.ToString();
                //{B9F0D007-928A-4e96-805E-95F1C034718D}
                //this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.instate].Text = patientInfo.PVisit.InState.ID.ToString();
                this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.instate].Text = patientInfo.PVisit.InState.Name.ToString();
                this.neuSpread1_Sheet1.Rows[i].Tag = patientInfo; 

            }


        }

        protected virtual int QueryPatientInfoByDept()
        {
            if (this.cmbInhosDept.SelectedItem == null || string.IsNullOrEmpty(this.cmbInhosDept.Tag.ToString()))
            {
                return -1;
            }
            DateTime fromDate = this.dtpFromDate.Value.Date;
            DateTime toDate = this.dtpToDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            ArrayList alInpatientNos= this.Inpatient.PatientQuery(this.cmbInhosDept.Tag.ToString(), fromDate, toDate);
            this.SetPatientInfo(alInpatientNos);
            return 1;
        }
        // {F417D766-19C0-4d3e-AB72-D774058B497E}
        protected virtual int QueryPatientInfoByOutDate()
        {
            string deptCode = string.Empty;
            if (string.IsNullOrEmpty(this.cmbInhosDept.Tag.ToString()))
            {
                deptCode = "ALL";
            }
            else
            {
                deptCode = this.cmbInhosDept.Tag.ToString();
            }
            DateTime fromDate = this.dtpFromDate.Value.Date;
            DateTime toDate = this.dtpToDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            ArrayList alInpatientNos = this.Inpatient.PatientQueryByDeptAndOutDate(deptCode, fromDate, toDate);
            this.SetPatientInfo(alInpatientNos);
            return 1;
        }
        protected virtual int QueryPatientInfoByName()
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show("请输入姓名");
                this.txtName.Focus();
                return -1;
            }

            ArrayList alInpatientNos = this.Inpatient.PatientQueryByName(this.txtName.Text);

            this.SetPatientInfo(alInpatientNos);
            return 1;
        }

        private void btQueryByName_Click(object sender, EventArgs e)
        {
            this.QueryPatientInfoByName();

        }

        private void btQueryByDept_Click(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            if (this.ckOutDate.Checked)// {F417D766-19C0-4d3e-AB72-D774058B497E}
            {
                this.QueryPatientInfoByOutDate();
            }
            else
            {
                this.QueryPatientInfoByDept();
            }
        }

        protected virtual int InitDept()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

            ArrayList alDeptList = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            this.cmbInhosDept.AddItems(alDeptList);

            this.dtpFromDate.Value = DateTime.Now.AddDays(-7);// {78E78281-8B9D-41d8-9D94-9EC43DB43FD7}


            if (this.ckOutDate.Checked)// {F417D766-19C0-4d3e-AB72-D774058B497E}
            {
                this.neuLabel1.Text = "出院时间：";
            }
            else
            {
                this.neuLabel1.Text = "入院时间：";
            }

            return 1;
        }


        protected override void OnLoad(EventArgs e)
        {
            this.InitDept();

            // {F417D766-19C0-4d3e-AB72-D774058B497E}
            this.ckOutDate.CheckedChanged += new EventHandler(ckOutDate_CheckedChanged);
            base.OnLoad(e);

        }

        private enum EnumCol
        {
            patientNO,
            name,
            dept,
            nurseCell,
            inDate,
            outDate,
            instate
        }

        private void ckOutDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckOutDate.Checked)
            {
                this.neuLabel1.Text = "出院时间：";
            }
            else
            {
                this.neuLabel1.Text = "入院时间：";
            }
        }
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.SelectedPatientinfo != null)
            {
                FS.HISFC.Models.RADT.PatientInfo p = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.RADT.PatientInfo;

                this.SelectedPatientinfo(p);
            }

            this.DialogResult = DialogResult.OK;
        }
    }

    
}
