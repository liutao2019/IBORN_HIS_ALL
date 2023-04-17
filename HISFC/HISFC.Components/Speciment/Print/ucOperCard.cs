using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.HISFC.BizLogic.RADT;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class ucOperCard : UserControl
    {
        private string hisBarCode = "";
        private string inHosNum = "";
        private string name = "";
        private string time = "";
        private string location = "";
        private string diag = "";
        private string printer = "";

        private PatientInfo patientInfo = new PatientInfo();
        private InPatient inPatient = new InPatient();

        public string HisBarCode
        {
            set
            {
                hisBarCode = value;
            }
        }

        public string InHosNum
        {
            set
            {
                inHosNum = value;
            }
        }

        public string Name
        {
            set
            {
                name = value;
            }
        }

        public string Time
        {
            set
            {
                time = value;
            }
        }

        public string Loc
        {
            set
            {
                location = value;
            }
        }

        public string Diagnose
        {
            set
            {
                diag = value;
            }
        }

        public string Printer
        {
            set
            {
                printer = value;
            }
        }
        public ucOperCard()
        {
            InitializeComponent();
        }

        private void SetValue()
        {
            this.patientInfo = inPatient.GetPatientInfoByPatientNO(inHosNum);
            lbl39.Text = "*" + hisBarCode + "*";
            lblBarCode.Text = hisBarCode;
            lblInHosNum.Text = inHosNum;
            //lblLoc.Text = location;
            lblName.Text = name + " (" + patientInfo.PVisit.PatientLocation.Dept.Name + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4,2) + "床)";
            lblDiag.Text = diag;
            //lblTime.Text = time;
        }

        public void Print()
        {
            try
            { 
                this.SetValue();
                //string printerIP = controlMgr.QueryControlInfoByCode("InPaIP").ControlerValue;
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.PrintDocument.PrinterSettings.PrinterName = printer;
                p.IsHaveGrid = true;
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
                page = new FS.HISFC.Models.Base.PageSize("AAA", 196, 118);
                FS.HISFC.Components.Common.Classes.Function.GetPageSize(page, ref p);
                p.PrintPage(0, 0, this.panel1);
                //MessageBox.Show("该病人的入院科室是：" + this.patientInfomation.PVisit.PatientLocation.Dept.Name + "\n 需要打印条码,请注意条码!");
            }
            catch
            { }

        }
    }
}
