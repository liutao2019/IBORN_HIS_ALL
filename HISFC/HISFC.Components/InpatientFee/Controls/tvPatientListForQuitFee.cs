using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Controls
{
    public partial class tvPatientListForQuitFee : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvPatientListForQuitFee()
        {
            InitializeComponent();
            #region {7655A89B-5996-4651-BAB4-62B53AACA6CF}
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return;
            }
            #endregion
            this.Refresh();
        }

        public tvPatientListForQuitFee(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public string ShowType = "In";

        public string ShowDept = "1";

        FS.HISFC.BizProcess.Integrate.RADT manager = null;
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        private ArrayList depts = null;
        private ArrayList GetDepts(string nurseCode)
        {
            if (depts == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager m = new FS.HISFC.BizProcess.Integrate.Manager();
                depts = m.QueryDepartment(nurseCode);

            }
            return depts;
        }

        public new void Refresh()
        {
            this.BeginUpdate();
            this.Nodes.Clear();
            if (manager == null)
                manager = new FS.HISFC.BizProcess.Integrate.RADT();

            ArrayList al = new ArrayList();//患者列表

            addPatientList(al);

            //显示所有患者列表
            this.SetPatient(al);

            this.EndUpdate();

        }

        /// <summary>
        /// 根据病区站得到欠费患者
        /// </summary>
        /// <param name="al"></param>
        private void addPatientList(ArrayList al)
        {
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
             ArrayList alDept = this.GetDepts(employee.Nurse.ID);

             ArrayList al1 = new ArrayList();
             foreach (FS.FrameWork.Models.NeuObject objDept in alDept)
             {
                 string deptName = objDept.Name;
                 string deptCode = objDept.ID;
                 al1.Clear();
                 al1 = this.radtManager.QueryQuitFeePatientByNurseCell(deptCode);
                 if (al1 != null && al1.Count > 0)
                 {
                     if (ShowDept == "1")//显示科室 
                     {
                         //应该显示患者科室名称
                         FS.HISFC.Models.RADT.PatientInfo p = al1[0] as FS.HISFC.Models.RADT.PatientInfo;
                         al.Add(p.PVisit.PatientLocation.Dept.Name);

                     }
                     al.AddRange(al1);
                 }
             }
        }

    }
}
