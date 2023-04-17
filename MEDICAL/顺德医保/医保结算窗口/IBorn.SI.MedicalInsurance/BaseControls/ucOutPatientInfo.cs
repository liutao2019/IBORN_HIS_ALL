using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.MedicalInsurance.FoShan.BaseControls
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
            var mainDiagnoses = diagManager.QueryMainDiagnose(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (mainDiagnoses != null && mainDiagnoses.Count > 0)
            {
                FS.HISFC.Models.HealthRecord.Diagnose mainDiagnose = mainDiagnoses[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                if (mainDiagnose != null)
                {
                    this.txtDiagnose.Text = mainDiagnose.DiagInfo.ICD10.Name;
                }
            }
            //todo:需优化，代码需整合挪移
            string sql = string.Format(@"select i.reg_no,j.invoice_no from fin_ipr_siinmaininfo i left join GZSI_HIS_MZJS j 
on i.inpatient_no=j.clinic_code and i.reg_no=j.jydjh and i.invoice_no=j.invoice_no  and j.valid_flag='1' and j.invoice_no='{1}'
where i.valid_flag='1' and i.inpatient_no='{0}'  and i.reg_no is not null", register.ID, register.SIMainInfo.InvoiceNo);
            DataSet dsRes = new DataSet();
            pactManager.ExecQuery(sql, ref dsRes);
            string regNO = string.Empty;
            string invoiceNO = string.Empty;
            if (dsRes != null)
            {
                var dtRow = dsRes.Tables[0];
                if (dtRow.Rows.Count > 0)
                {
                    regNO = dtRow.Rows[0]["reg_no"].ToString();
                    invoiceNO = dtRow.Rows[0]["invoice_no"].ToString();
                }
            }
            if (string.IsNullOrEmpty(regNO))
            {
                lbSIInfo.Text = "未关联医保登记";
            }
            else if (string.IsNullOrEmpty(invoiceNO))
            {
                lbSIInfo.Text = string.Format("医保登记号:{0}  未关联医保结算", regNO);
            }
            else
            {
                lbSIInfo.Text = string.Format("医保登记号:{0}  医保已结算", regNO);
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
