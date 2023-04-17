using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.OutpatientFee.Controls.SIBalance
{
    public partial class ucOutPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 诊断业务层
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        public string currentJYDJH = "";

        public ucOutPatientInfo()
        {
            InitializeComponent();
        }

        void InitData()
        {
            var sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
            this.cmbSex.AddItems(sexListTemp);

            var doctList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctList == null)
            {
                MessageBox.Show("初始化医生列表出错!" + this.managerIntegrate.Err);
                return;
            }
            FS.HISFC.Models.Base.Employee pNone = new FS.HISFC.Models.Base.Employee();
            pNone.ID = "999";
            pNone.Name = "无归属";
            pNone.SpellCode = "WGS";
            pNone.UserCode = "999";
            doctList.Add(pNone);
            this.cmbDoct.AddItems(doctList);

            var regDeptList = this.managerIntegrate.GetDeptmentAllValid();
            if (regDeptList == null)
            {
                MessageBox.Show("初始化挂号科室出错!" + this.managerIntegrate.Err);
                return;
            }
            this.cmbRegDept.AddItems(regDeptList);

            var pactList = pactManager.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + this.pactManager.Err);
                return;
            }
            this.cmbPact.AddItems(pactList);
        }

        //{7784630A-37D7-4eb9-95C5-8D2CD37AC2FB}
        public void setCardNo(string card_no)
        {
            this.tbCardNO.Text = card_no;
        }

        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.Clear();
            if (register == null)
            {
                return;
            }
            //this.tbMCardNO.Text = register.SSN;
            this.tbName.Text = register.Name;
            this.tbAge.Text = this.pactManager.GetAge(register.Birthday);
            this.cmbSex.Tag = register.Sex.ID.ToString();
            this.cmbPact.Tag = register.Pact.ID;
            this.tbCardNO.Text = register.PID.CardNO;
            this.cmbRegDept.Tag = register.SeeDoct.Dept.ID;
            this.cmbDoct.Tag = register.SeeDoct.ID;
            this.txtIDCardNO.Text = register.IDCard;
            var mainDiagnoses = diagManager.QueryDiagnoseByInpatientNOAndPersonType(register.ID,"0");
            if (mainDiagnoses != null && mainDiagnoses.Count > 0)
            {
                FS.HISFC.Models.HealthRecord.Diagnose mainDiagnose = mainDiagnoses[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                if (mainDiagnose != null)
                {
                    this.txtDiagnose.Text = mainDiagnose.DiagInfo.ICD10.Name;
                }
            }
        }

        public void Clear()
        {
            this.tbName.Text = string.Empty;
            this.tbAge.Text = string.Empty;
            this.cmbSex.Tag = "";
            this.cmbPact.Tag = "";
            this.tbCardNO.Text = string.Empty;
            this.cmbRegDept.Tag = "";
            this.cmbDoct.Tag = "";
            this.txtIDCardNO.Text = string.Empty;
            this.lbSIInfo.Text = string.Empty;
            this.txtDiagnose.Text = string.Empty;
        }

        private void ucOutPatientInfo_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Clear();
                InitData();
            }
        }
        public Action<string> GetRegisterByCardNO;
        private void tbCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && GetRegisterByCardNO != null)
            {
                GetRegisterByCardNO(tbCardNO.Text);
            }
        }
    }
}
