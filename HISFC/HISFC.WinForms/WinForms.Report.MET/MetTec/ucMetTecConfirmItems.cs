using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.MET.MetTec
{
    public partial class ucMetTecConfirmItems : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetTecConfirmItems()
        {
            InitializeComponent();
            InitCboPatientType();
            InitCboRecipeDept();
        }

        private FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        private ArrayList alDept = new ArrayList();
        private ArrayList alDept1 = new ArrayList();
        private ArrayList alPatient =new ArrayList();
        private string rdeptID = string.Empty;
        private string rdeptName = string.Empty;
        private string deptNo = string.Empty;
        private string patientTypeID = string.Empty;
        private string patientTypeName = string.Empty;


        private void InitCboRecipeDept()
        {
            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "ALL";
            obj1.Name = "全部";
            alDept.Add(obj1);
            alDept1 = deptManager.GetDeptmentAll();
            alDept.AddRange(alDept1);
            this.cboRecipeDept.AddItems(alDept);
            if (this.cboRecipeDept.Items.Count > 0)
            {
                this.cboRecipeDept.SelectedIndex = 0;
            }
        }

        private void InitCboPatientType()
        {
            FS.FrameWork.Models.NeuObject obj3 = new FS.FrameWork.Models.NeuObject();
            obj3.ID = "ALL";
            obj3.Name = "全部";
            this.alPatient.Add(obj3);
            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "ZY";
            obj1.Name = "住院";
            this.alPatient.Add(obj1);
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "MZ";
            obj2.Name = "门诊";
            this.alPatient.Add(obj2);
            this.cboPatientType.AddItems(alPatient);
            this.cboPatientType.SelectedIndex = 0;
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            emp = (FS.HISFC.Models.Base.Employee)dept.Operator;
            return base.OnRetrieve(base.beginTime, base.endTime, rdeptID, patientTypeID);
           // return base.OnRetrieve(base.beginTime, base.endTime, rdeptID, emp.ID,patientTypeID, rdeptName, this.emp.Dept.Name, patientTypeName);
        }

        private void cboRecipeDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdeptID = ((FS.FrameWork.Models.NeuObject)alDept[this.cboRecipeDept.SelectedIndex]).ID.ToString();
            rdeptName = ((FS.FrameWork.Models.NeuObject)alDept[this.cboRecipeDept.SelectedIndex]).Name.ToString();
        }

        private void cboPatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            patientTypeID = ((FS.FrameWork.Models.NeuObject)alPatient[this.cboPatientType.SelectedIndex]).ID.ToString();
            patientTypeName = ((FS.FrameWork.Models.NeuObject)alPatient[this.cboPatientType.SelectedIndex]).Name.ToString();
        }
       
    }
}
