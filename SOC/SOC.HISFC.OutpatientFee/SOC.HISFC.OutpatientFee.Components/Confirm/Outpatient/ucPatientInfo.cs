using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Confirm.Controls
{
    public partial class ucPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.SOC.HISFC.OutpatientFee.Interface.Confirm.IPatientInfo
    {
        public ucPatientInfo()
        {
            InitializeComponent();
        }

        #region IPatientInfo 成员

        public int Clear()
        {
            return 1;
        }

        public void ShowInfo(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }

            this.lbCardNO.Text = "病例号：" + register.PID.CardNO;
            this.lbPatientName.Text = "姓名：" + register.Name + " " + register.Sex.Name + " " + register.Age;
            this.lbPact.Text = "合同单位：" + register.Pact.Name;
            this.lbAccountVacancy.Text = "账户余额：";

            //挂号信息
            this.lbRegDept.Text = "挂  号：" + register.DoctorInfo.Templet.Dept.Name;
            this.lbRegDate.Text = "时间：" + register.DoctorInfo.SeeDate.ToString();
            this.lbDiagnose.Text = "诊断：" + Function.GetDiagnoseBizProcess().QueryDiagnoseName(register.ID);
        }

        #endregion
    }
}
