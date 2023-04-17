using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ChaoYang
{
    public partial class ucCancelPrepayPatientSign : UserControl
    {
        public ucCancelPrepayPatientSign()
        {
            InitializeComponent();
        }

        public void SetValue(FS.HISFC.Models.RADT.PatientInfo patientInfo,FS.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            if (patientInfo.IsEncrypt)
            {
                this.lblName.Text = patientInfo.DecryptName;
            }
            else
            {
                this.lblName.Text = patientInfo.Name;
            }
            this.lbldate.Text = prepay.PrepayOper.OperTime.ToString();
            this.lblCardNO.Text = patientInfo.PID.CardNO;
            this.lblMoney.Text = Math.Abs(prepay.FT.PrepayCost).ToString("F2") + "元";
            this.lblCaption.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(prepay.FT.ReturnCost);
            this.lblOper.Text = prepay.PrepayOper.ID.Substring(2);
            this.lblHospitalName.Text = (new FS.HISFC.BizProcess.Integrate.Manager()).GetHospitalName();
            this.lblVacancy.Text = (patientInfo.FT.PrepayCost+ prepay.FT.PrepayCost).ToString("F2") + "元";
        }

    }
}
