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

            this.lblBlood.Text = patient.BloodType.Name ;//血型 
            this.lblOutStat.Text = managerBizProcess.GetConstansObj(FS.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//出院情况
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
            //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            //添加边线 zhao.chf
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
