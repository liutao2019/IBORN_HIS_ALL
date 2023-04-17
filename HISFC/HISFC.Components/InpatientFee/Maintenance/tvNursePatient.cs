using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class tvNursePatient : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvNursePatient()
        {
            InitializeComponent();
            
        }

        public tvNursePatient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private ArrayList dept = null;
        private ArrayList GetDepts(string nurseCode)
        {
            if (dept == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager man = new FS.HISFC.BizProcess.Integrate.Manager();
                dept = man.QueryDepartment(nurseCode);
            }
            return dept;
        }

        /// <summary>
        /// ³õÊ¼»¯Ê÷
        /// </summary>
        public void Init()
        {
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            ArrayList operDept = this.GetDepts(employee.Nurse.ID);
            
            foreach (FS.FrameWork.Models.NeuObject objDept in operDept)
            {
                ArrayList al = radt.QueryPatient(objDept.ID, FS.HISFC.Models.Base.EnumInState.I);
                al.Insert(0, objDept.Name);
                
                this.SetPatient(al);
            }
            
        }
    }
}
