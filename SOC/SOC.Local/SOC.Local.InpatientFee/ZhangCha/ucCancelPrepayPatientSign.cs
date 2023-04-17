using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ZhangCha
{
    public partial class ucCancelPrepayPatientSign : UserControl
    {
        public ucCancelPrepayPatientSign()
        {
            InitializeComponent();
        }

        public void SetValue(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            if (patientInfo.IsEncrypt)
            {
                this.lblName.Text = patientInfo.DecryptName;
            }
            else
            {
                this.lblName.Text = patientInfo.Name;
            }

            this.lblPatientNo.Text = patientInfo.PID.ID;
            this.lbldate.Text = prepay.PrepayOper.OperTime.ToString();
            this.lblCardNO.Text = patientInfo.PID.CardNO;
            this.lblMoney.Text = Math.Abs(prepay.FT.PrepayCost).ToString("F2") + "元";
            this.lblCaption.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(Math.Abs(prepay.FT.PrepayCost));
            this.lblOper.Text = prepay.PrepayOper.ID.Substring(2);
            this.lblHospitalName.Text = (new FS.HISFC.BizProcess.Integrate.Manager()).GetHospitalName();

            switch (prepay.PayType.ID)
            {
                case "CA":
                    this.lblPayMode.Text = "现金";
                    break;
                case "CD":
                    this.lblPayMode.Text = "银行卡";
                    break;
                case "CH":
                    this.lblPayMode.Text = "支票";
                    break;
                case "RC":
                    this.lblPayMode.Text = "减免";
                    break;
                case "YS":
                    this.lblPayMode.Text = "帐户支付";
                    break;
                default:
                    this.lblPayMode.Text = "其他";
                    break; 
            }

            this.lblDeptName.Text = patientInfo.PVisit.PatientLocation.Dept.Name;

        }

    }
}
