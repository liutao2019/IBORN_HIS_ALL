using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.GuangZhou.Controls
{
    /// <summary>
    /// 显示医保登记信息
    /// </summary>
    public partial class ucRegisterInfoOutPatient : System.Windows.Forms.UserControl
    {
        //private bool isSpecialPact = false;

        ///// <summary>
        ///// 是否住院处理的医保（门特）
        ///// </summary>
        //public bool IsSpecialPact
        //{
        //    get { return this.isSpecialPact; }
        //    set { isSpecialPact = value; }
        //}
        bool isOK = false;
        public bool IsOK { get { return isOK; } }

        public ucRegisterInfoOutPatient()
        {
            InitializeComponent();

            this.btnQuery.Click += btnQuery_Click;
            this.btnOK.Click += btnOK_Click;
            this.btnRefresh.Click += btnRefresh_Click;
        }

        private bool isRefreash = false;

        private FS.HISFC.Models.Registration.Register register = null;

        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return register;
            }
            set
            {
                register = value;
            }
        }

        public void Clear()
        {
            this.txtPatientInfo.Text = "姓名 住院号 身份证号";
            this.lbInsuranceNO.Text = "0";
            this.lbName.Text = "0";
            this.lbIDNO.Text = "0";
            this.lbHosCode.Text = "0";
            this.lbDiagnoseID.Text = "0";
            this.lbDiagnoseName.Text = "0";
            this.lbInTime.Text = "0";
            this.lbCompany.Text = "0";
            this.lbAdmitType.Text = "0";
            this.lbPersonType.Text = "0";
            this.lbInDept.Text = "0";
            this.isRefreash = false;
        }

        public int Refreash()
        {
            if (register == null)
            {
                return -1;
            }
            this.txtPatientInfo.Text = string.Format("{0} 门诊号：{1} 身份证号：{2}", register.Name, register.PID.CardNO, register.IDCard);
            IBorn.SI.GuangZhou.OutPatient.GetRegister getRegisterManager = new IBorn.SI.GuangZhou.OutPatient.GetRegister();
            //就诊类别:1.住院2.门诊特定项目3.门诊4.药店  register.SIMainInfo.MedicalType.ID          
            int i = getRegisterManager.CallService(register, ref register, register.IDCard, register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"), this.register.SIMainInfo.MedicalType.ID, register.Name);
            if (i < 0)
            {
                MessageBox.Show(getRegisterManager.ErrorMsg);
                return -1;
            }
            else if (i == 0)
            {
                return 0;
            }
            this.RefreashSuccess();
            return 1;
        }

        private void RefreashSuccess()
        {

            this.lbInsuranceNO.Text = this.register.SIMainInfo.RegNo;
            this.lbName.Text = this.register.Name;
            this.lbIDNO.Text = this.register.IDCard;
            this.lbHosCode.Text = this.register.SIMainInfo.HosNo;
            this.lbDiagnoseID.Text = this.register.SIMainInfo.InDiagnose.ID;
            this.lbDiagnoseName.Text = this.register.SIMainInfo.ClinicDiagNose;
            this.lbInTime.Text = this.register.SIMainInfo.OperDate.ToString();
            this.lbCompany.Text = this.register.CompanyName;
            this.lbAdmitType.Text = Enum.GetName(typeof(IBorn.SI.GuangZhou.Base.EnumMedicallType), FS.FrameWork.Function.NConvert.ToInt32(this.register.SIMainInfo.MedicalType.ID));
            //this.register.SIMainInfo.MedicalType.ID == "2" ? "门诊特定项目" : "门诊";
            this.lbPersonType.Text = Enum.GetName(typeof(IBorn.SI.GuangZhou.Base.EnumPersonType), FS.FrameWork.Function.NConvert.ToInt32(this.register.SIMainInfo.EmplType));
            //this.lbInDept.Text = this.register.Register.PatientAdmit.Dept.ID;
            this.isRefreash = true;
        }

        public int Save()
        {
            if (this.register == null)
            {
                return -1;
            }

            if (isRefreash == false)
            {
                this.Refreash();
                return 0;
            }
            else
            {
                IBorn.SI.GuangZhou.SILocalManager myInterface = new IBorn.SI.GuangZhou.SILocalManager();
                this.register.SIMainInfo.IsValid = true;
                this.register.SIMainInfo.IsBalanced = false;
                //todo:需要先去本地医保登记表查询该就医登记号是否存在有效记录，如果没有才写入。如果有则更新
                int iReturn = myInterface.SaveSIMainInfo(register);
                if (iReturn <= 0)
                {
                    MessageBox.Show("保存医保端的登记信息到本地失败!" + myInterface.Err);
                    return -1;
                }
                return 1;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.register != null)
            {
                //todo:可以考虑加上检查本地是否存有有效的医保登记信息
                this.Refreash();
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Refreash();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (this.register == null)
            {
                return;
            }

            if (isRefreash == false)
            {
                this.Refreash();
                return;
            }
            this.isOK = true;
            this.FindForm().Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.register != null)
            {
                ucGetSiRegInfoOutPatient ucGetSiRegInfoOutPatient = new ucGetSiRegInfoOutPatient();
                ucGetSiRegInfoOutPatient.CurrentReg = this.register;
                ucGetSiRegInfoOutPatient.MedicalTypeKind = this.register.SIMainInfo.MedicalType.ID;//IsSpecialPact ? "2" : "3";
                ucGetSiRegInfoOutPatient.BeginTime = register.DoctorInfo.SeeDate;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucGetSiRegInfoOutPatient);
                if (ucGetSiRegInfoOutPatient.IsOK)
                {
                    this.register = ucGetSiRegInfoOutPatient.SelectedInsReg;
                    this.RefreashSuccess();
                }
            }
        }


    }
}
