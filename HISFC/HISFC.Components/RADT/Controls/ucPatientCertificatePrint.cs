using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.RADT.Controls
{
    public partial class ucPatientCertificatePrint : UserControl
    {

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        FS.HISFC.BizProcess.Integrate.Manager managerBizProcess = new FS.HISFC.BizProcess.Integrate.Manager();

        //FS.HISFC.BizLogic.RADT.InPatient inpatientRadt = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        public ucPatientCertificatePrint()
        {
            InitializeComponent();
        }


        public int SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DateTime currTime = DateTime.Now;
            string days = (currTime.Date - patient.PVisit.InTime.Date).TotalDays.ToString();
            this.lblBlood.Text = patient.BloodType.Name ;//血型 
            //this.lblOutStat.Text = managerBizProcess.GetConstansObj(FS.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//出院情况
            this.lblInDate1.Text = patient.PVisit.InTime.ToShortDateString();//住院日期
            //this.lblOutDate2.Text = patient.PVisit.OutTime.ToShortDateString();//出院日期
            this.lblBedNo1.Text = patient.PVisit.PatientLocation.Bed.ID;//床号
            this.lblName1.Text = patient.Name;//姓名
            this.lblPatientNo1.Text = patient.PID.PatientNO;//住院号
            this.lblDept.Text = patient.PVisit.PatientLocation.NurseCell.Name;//病区
            this.lblAllergyInfo.Text = patient.AllergyInfo;
            this.lblDial.Text = patient.MainDiagnose;//住院主表的诊断
            this.lblDays.Text = days+"天";
            this.lbHouseDocName.Text = patient.PVisit.AdmittingDoctor.Name;
            this.lbChargeDocName.Text = patient.PVisit.AttendingDoctor.Name;
            this.lbChiefDocName.Text = patient.PVisit.ConsultingDoctor.Name;
            this.lblPactName.Text = patient.Pact.Name;
            this.lblDutyNurseName.Text = patient.PVisit.AdmittingNurse.Name;
            this.lblTend.Text = patient.Disease.Tend.Name;
            //this.lblDieteticMark.Text = patient.Disease.Memo;
            this.lblMemo.Text = patient.Memo;
            if (patient.IsHasBaby)
            {
                this.lblIsHaveBaby.Text = "有";
            }
            else
            {
                this.lblIsHaveBaby.Text = "无";
            }
            //this.lblInStat.Text = patient.PVisit.Circs.Name;
            //this.lblInWay.Text = patient.PVisit.InSource.Name;
            if (patient.PVisit.Circs.ID == "1")
            {
                this.lblInStat.Text = "一般";
            }
            else if (patient.PVisit.InSource.ID == "2")
            {
                this.lblInStat.Text = "急";
            }
            else if (patient.PVisit.InSource.ID == "3")
            {
                this.lblInStat.Text = "危";
            }

            if (patient.PVisit.InSource.ID == "1")
            {
                this.lblInWay.Text = "门诊";
            }
            else if (patient.PVisit.InSource.ID == "2")
            {
                this.lblInWay.Text = "急诊";
            }
            else if (patient.PVisit.InSource.ID == "3")
            {
                this.lblInWay.Text = "转科";
            }
            else if (patient.PVisit.InSource.ID == "4")
            {
                this.lblInWay.Text = "转院";
            }
            this.lblPrintTime.Text = currTime.ToString();
            return 1;
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
            return 1;
        }
        public int PrintPreview()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            
            print.PrintPreview(0, 0, this);  print.PrintPage(0, 0, this);
            return 1;
        }


    }
}
