using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.WinForms.WorkStation.Controls
{
    public partial class tvPatientList : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvPatientList()
        {
            InitializeComponent();

            //
            this.AddPatient();
        }

        private void AddPatient()
        { 
                ArrayList al = new ArrayList();
                al.Add("分管患者|patient");
                
                try
                {
                    ArrayList al1 = new ArrayList();
                    al1 = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientByEmpl( FS.FrameWork.Management.Connection.Operator.ID, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID );
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "查找分管患者出错\n！" + ex.Message + FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.Err );
                }

                al.Add("本科室患者|DeptPatient");
                try
                {
                    ArrayList al1 = new ArrayList();
                    al1 = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientByDept( ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID );
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "查找科室患者出错\n！" + ex.Message + FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.Err );
                }

              

                al.Add("周内出院患者|OutPatient");

                try
                {
                    ArrayList al1 = new ArrayList();
                    al1 = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientByDept( ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, 7 );
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查找周内出院患者出错\n！" + ex.Message + FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.Err);
                }

                this.SetPatient(al);
              
           
        }

    }
}
