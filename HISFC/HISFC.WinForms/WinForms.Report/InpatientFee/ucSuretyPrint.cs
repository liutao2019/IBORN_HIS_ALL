using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucSuretyPrint : UserControl,FS.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety
    {
        public ucSuretyPrint()
        {
            InitializeComponent();
        }

        #region 域
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region IPrintSurety 成员

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print() ;
            p.PrintPage(0, 0, this.neuPanel1);
        }

        public void PrintView()
        {
            throw new NotImplementedException();
        }

        public void SetValue(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = new FS.HISFC.Models.RADT.PatientInfo();
            FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
            FS.FrameWork.Public.ObjectHelper suretyHelper = new FS.FrameWork.Public.ObjectHelper();
            ArrayList alSuretyType = FS.HISFC.Models.Fee.SuretyTypeEnumService.List();
            suretyHelper.ArrayObject = alSuretyType;
            ArrayList alDept = this.managerIntegrate.GetDepartment();
            deptHelper.ArrayObject = alDept;

            patientInfoTemp = this.radtIntegrate.GetPatientInfomation(patientInfo.ID);


            this.lblInvoiceNO.Text = patientInfo.Surety.InvoiceNO;
            this.lblname.Text = patientInfoTemp.Name;
            this.lblPatientNO.Text = patientInfoTemp.PID.PatientNO;
            this.lblDept.Text = deptHelper.GetName(patientInfo.PVisit.PatientLocation.Dept.ID);
            this.lblSuretyCost.Text = patientInfo.Surety.SuretyCost.ToString();
            this.lblSuretyPerson.Text = patientInfo.Surety.SuretyPerson.Name;
            this.lblSuretyType.Text = suretyHelper.GetName(patientInfo.Surety.SuretyType.ID.ToString());



        }

        #endregion
    }
}
