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
            this.lblBlood.Text = patient.BloodType.Name ;//Ѫ�� 
            //this.lblOutStat.Text = managerBizProcess.GetConstansObj(FS.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//��Ժ���
            this.lblInDate1.Text = patient.PVisit.InTime.ToShortDateString();//סԺ����
            //this.lblOutDate2.Text = patient.PVisit.OutTime.ToShortDateString();//��Ժ����
            this.lblBedNo1.Text = patient.PVisit.PatientLocation.Bed.ID;//����
            this.lblName1.Text = patient.Name;//����
            this.lblPatientNo1.Text = patient.PID.PatientNO;//סԺ��
            this.lblDept.Text = patient.PVisit.PatientLocation.NurseCell.Name;//����
            this.lblAllergyInfo.Text = patient.AllergyInfo;
            this.lblDial.Text = patient.MainDiagnose;//סԺ��������
            this.lblDays.Text = days+"��";
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
                this.lblIsHaveBaby.Text = "��";
            }
            else
            {
                this.lblIsHaveBaby.Text = "��";
            }
            //this.lblInStat.Text = patient.PVisit.Circs.Name;
            //this.lblInWay.Text = patient.PVisit.InSource.Name;
            if (patient.PVisit.Circs.ID == "1")
            {
                this.lblInStat.Text = "һ��";
            }
            else if (patient.PVisit.InSource.ID == "2")
            {
                this.lblInStat.Text = "��";
            }
            else if (patient.PVisit.InSource.ID == "3")
            {
                this.lblInStat.Text = "Σ";
            }

            if (patient.PVisit.InSource.ID == "1")
            {
                this.lblInWay.Text = "����";
            }
            else if (patient.PVisit.InSource.ID == "2")
            {
                this.lblInWay.Text = "����";
            }
            else if (patient.PVisit.InSource.ID == "3")
            {
                this.lblInWay.Text = "ת��";
            }
            else if (patient.PVisit.InSource.ID == "4")
            {
                this.lblInWay.Text = "תԺ";
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
