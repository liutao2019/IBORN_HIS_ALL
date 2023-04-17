using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.Controls
{
    public partial class ucPatientInfoBase : UserControl, RecipePrint.Interface.IPatientInfo
    {
        public ucPatientInfoBase()
        {
            InitializeComponent();
        }

        public int SetPatientInfo(FS.HISFC.Models.Registration.Register regObj)
        {
            lblName.Text = regObj.Name;
            lblSex.Text = regObj.Sex.Name;
            lblAge.Text = Function.GetAge(regObj.Birthday);
            lblCardNo.Text = regObj.PID.CardNO;
            lblSeeDept.Text = regObj.PVisit.PatientLocation.Dept.Name;

            string add = "/";
            if (string.IsNullOrEmpty(regObj.AddressHome)
                || string.IsNullOrEmpty(regObj.PhoneHome))
            {
                add = "";
            }
            lblTel.Text = regObj.AddressHome + add + regObj.PhoneHome;

            lblSeeDate.Text = regObj.SeeDoct.OperTime.ToString("yyyy.MM.dd");

            //诊断
            lblDiag.Text = Function.GetDiag(regObj.ID);

            return 1;
        }

        public int SetRecipeDept(string deptName)
        {
            lblSeeDept.Text = deptName;
            return 1;
        }
    }
}
