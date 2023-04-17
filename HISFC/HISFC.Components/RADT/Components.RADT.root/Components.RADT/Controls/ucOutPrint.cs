using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    public partial class ucOutPrint : UserControl
    {

        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();
        Neusoft.HISFC.BizLogic.Manager.Department deptMgr = new Neusoft.HISFC.BizLogic.Manager.Department();

        Neusoft.HISFC.BizProcess.Integrate.Manager managerBizProcess = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        //Neusoft.HISFC.BizLogic.RADT.InPatient inpatientRadt = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.Models.RADT.PatientInfo patient = new Neusoft.HISFC.Models.RADT.PatientInfo();

        public ucOutPrint()
        {
            InitializeComponent();
        }

        //默认打印正常出院通知到
        string nameFlag = "0";

        /// <summary>
        /// 通知到打印类型
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
                //        this.neuLabel1.Text = "出院";
                //        break;
                //    case "1":
                //        this.neuLabel1.Text = "转院";
                //        //this.Paint += new PaintEventHandler(MyDrawRectangle);
                //        //this.BorderStyle = BorderStyle.FixedSingle;
                //        this.AddPanelRect();
                //        break;
                //    case "2":
                //        this.neuLabel1.Text = "转科";
                //        break;
                //    default:
                //        break;
                //}
                //this.neuLabel1.Text += "结算通知单";

                

            }
        }

        public int SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                System.IO.MemoryStream me = new System.IO.MemoryStream(((Neusoft.HISFC.Models.Base.Hospital)deptMgr.Hospital).HosLogoImage);
                this.picbLogo.Image = Image.FromStream(me);
            }
            catch
            {
            }

            this.lblBlood.Text = patient.BloodType.Name ;//血型 
            this.lblOutStat.Text = managerBizProcess.GetConstansObj(Neusoft.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//出院情况
            this.lblOutDate1.Text = patient.PVisit.OutTime.ToShortDateString();//出院日期
            this.lblOutDate2.Text = patient.PVisit.OutTime.ToShortDateString();//出院日期
            this.lblBedNo1.Text = patient.PVisit.PatientLocation.Bed.ID;//床号
            this.lblBedNo2.Text = patient.PVisit.PatientLocation.Bed.ID;//床号
            this.lblName1.Text = patient.Name;//姓名
            this.lblName2.Text = patient.Name;//姓名
            this.lblPatientNo1.Text = patient.PID.PatientNO;//住院号
            this.lblPatientNo2.Text = patient.PID.PatientNO;//住院号
            this.lblDept.Text = patient.PVisit.PatientLocation.NurseCell.Name;//病区

            this.lblDial.Text = patient.MainDiagnose;//住院主表的诊断

            ///医生医嘱的出院诊断
            //string strDial = string.Empty;
            //string strMainDial = string.Empty;
            //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(patient.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
            //{
            //    if (diag.DiagInfo.DiagType.ID.Equals("14"))//出院诊断
            //    {
            //        strDial += diag.DiagInfo.ICD10.Name + "、";
            //    }

            //    if (diag.DiagInfo.DiagType.ID.Equals("1"))//出院诊断
            //    {
            //        strMainDial += diag.DiagInfo.ICD10.Name + "、";
            //    }
            //}

            //if (strDial.Length > 0)
            //{
            //    this.lblDial.Text = strDial.Substring(0, strDial.Length - 1); //出院诊断
            //}
            //else if (strMainDial.Length > 0)
            //{
            //    this.lblDial.Text = strMainDial.Substring(0, strMainDial.Length - 1); 
            //}
            return 1;
        }

        public int Print()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            Neusoft.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            //添加边线 zhao.chf
            //print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.Border;
            print.PrintPage(0, 0, this);
            return 1;
        }
        public int PrintPreview()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            Neusoft.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            print.PrintPreview(0, 0, this);
            return 1;
        }


    }
}
