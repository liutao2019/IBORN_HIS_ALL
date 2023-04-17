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
    public partial class ucOutPrint : UserControl
    {

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        FS.HISFC.BizProcess.Integrate.Manager managerBizProcess = new FS.HISFC.BizProcess.Integrate.Manager();

        //FS.HISFC.BizLogic.RADT.InPatient inpatientRadt = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        public ucOutPrint()
        {
            InitializeComponent();
        }

        //Ĭ�ϴ�ӡ������Ժ֪ͨ��
        string nameFlag = "0";

        /// <summary>
        /// ֪ͨ����ӡ����
        /// </summary>
        public string NameFlag
        {
            get
            {
                return nameFlag;
            }
            set 
            {
                nameFlag = value;
                //switch (nameFlag)
                //{
                //    case "0":
                //        this.neuLabel1.Text = "��Ժ";
                //        break;
                //    case "1":
                //        this.neuLabel1.Text = "תԺ";
                //        //this.Paint += new PaintEventHandler(MyDrawRectangle);
                //        //this.BorderStyle = BorderStyle.FixedSingle;
                //        this.AddPanelRect();
                //        break;
                //    case "2":
                //        this.neuLabel1.Text = "ת��";
                //        break;
                //    default:
                //        break;
                //}
                //this.neuLabel1.Text += "����֪ͨ��";

                

            }
        }

        public int SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                System.IO.MemoryStream me = new System.IO.MemoryStream(((FS.HISFC.Models.Base.Hospital)deptMgr.Hospital).HosLogoImage);
                this.picbLogo.Image = Image.FromStream(me);
            }
            catch
            {
            }

            this.lblBlood.Text = patient.BloodType.Name ;//Ѫ�� 
            this.lblOutStat.Text = managerBizProcess.GetConstansObj(FS.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//��Ժ���
            this.lblOutDate1.Text = patient.PVisit.OutTime.ToShortDateString();//��Ժ����
            this.lblOutDate2.Text = patient.PVisit.OutTime.ToShortDateString();//��Ժ����
            this.lblBedNo1.Text = patient.PVisit.PatientLocation.Bed.ID;//����
            this.lblBedNo2.Text = patient.PVisit.PatientLocation.Bed.ID;//����
            this.lblName1.Text = patient.Name;//����
            this.lblName2.Text = patient.Name;//����
            this.lblPatientNo1.Text = patient.PID.PatientNO;//סԺ��
            this.lblPatientNo2.Text = patient.PID.PatientNO;//סԺ��
            this.lblDept.Text = patient.PVisit.PatientLocation.NurseCell.Name;//����

            this.lblDial.Text = patient.MainDiagnose;//סԺ��������

            ///ҽ��ҽ���ĳ�Ժ���
            //string strDial = string.Empty;
            //string strMainDial = string.Empty;
            //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            //{
            //    if (diag.DiagInfo.DiagType.ID.Equals("14"))//��Ժ���
            //    {
            //        strDial += diag.DiagInfo.ICD10.Name + "��";
            //    }

            //    if (diag.DiagInfo.DiagType.ID.Equals("1"))//��Ժ���
            //    {
            //        strMainDial += diag.DiagInfo.ICD10.Name + "��";
            //    }
            //}

            //if (strDial.Length > 0)
            //{
            //    this.lblDial.Text = strDial.Substring(0, strDial.Length - 1); //��Ժ���
            //}
            //else if (strMainDial.Length > 0)
            //{
            //    this.lblDial.Text = strMainDial.Substring(0, strMainDial.Length - 1); 
            //}
            return 1;
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            //��ӱ��� zhao.chf
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            print.PrintPage(0, 0, this);
            return 1;
        }
        public int PrintPreview()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            print.PrintPreview(0, 0, this);
            return 1;
        }


    }
}
