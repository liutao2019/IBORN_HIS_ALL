namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PrePayIn
{
    public partial class ucOutPatientNotice : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintInHosNotice
    {
        public ucOutPatientNotice()
        {
            InitializeComponent();
        }

        #region 变量


        #endregion


        #region IPrintInHosNotice 成员

        public int Print()
        {
            this.BackgroundImage = null;
            new FS.FrameWork.WinForms.Classes.Print().PrintPage(0, 0, this);
            return 1;
        }

        public int PrintView()
        {
            this.BackgroundImage = null;
            new FS.FrameWork.WinForms.Classes.Print().PrintPreview(this);
            return 1;
        }

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo prePatientInfo)
        {
            this.Clear();

            if (null == prePatientInfo) return 0;
            this.label0.Text = FS.FrameWork.Management.Connection.Hospital.Name + "入院登记表";
            FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
            this.lblAge.Text = dbMgr.GetAge(prePatientInfo.Birthday);
            this.lblDiagnosis.Text = prePatientInfo.ClinicDiagnose;
            this.lblForegift.Text = prePatientInfo.FT.PrepayCost.ToString() + "元";
            this.lblGender.Text = prePatientInfo.Sex.Name;
            this.lblInDate.Text = prePatientInfo.PVisit.InTime.ToString("yyyy年MM月dd日");
            this.lblInDept.Text = prePatientInfo.PVisit.PatientLocation.Dept.Name;
            this.lblIndications.Text = prePatientInfo.Memo;
            this.lblInstate.Text = prePatientInfo.PVisit.Circs.Name;
            this.lblName.Text = prePatientInfo.Name;
            this.lblSeeDept.Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;

            return 1;
        }

        private void Clear()
        {
            this.lblAge.Text = string.Empty;
            this.lblDiagnosis.Text = string.Empty;
            this.lblForegift.Text = string.Empty;
            this.lblGender.Text = string.Empty;
            this.lblInDate.Text = string.Empty;
            this.lblInDept.Text = string.Empty;
            this.lblIndications.Text = string.Empty;
            this.lblInstate.Text = string.Empty;
            this.lblName.Text = string.Empty;
            this.lblSeeDept.Text = string.Empty;
        }

        #endregion
    }
}
