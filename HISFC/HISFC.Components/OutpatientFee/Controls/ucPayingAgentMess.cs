using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucPayingAgentMess : UserControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucPayingAgentMess()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �˻�ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �˻���Ϣ
        /// </summary>
        private FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
        
        /// <summary>
        /// �˺���Ϣ
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;


        /// <summary>
        /// ��ǰ���ߵĿ���
        /// </summary>
        private string cardNo = string.Empty;
        /// <summary>
        /// ��ǰ���ߵĿ���
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        private ArrayList alAccountType = new ArrayList();
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private HISFC.Models.RADT.PatientInfo empowerPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public HISFC.Models.RADT.PatientInfo EmpowerPatient
        {
            get { return empowerPatient; }
            set { empowerPatient = value; }
        }/// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isPayForAnother = false;
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsPayForAnother
        {
            get { return isPayForAnother; }
            set { isPayForAnother = value; }
        }
        /// <summary>
        /// �ʻ����ͱ���
        /// </summary>
        public string AccountTypeCode
        {
            get { return this.cmbAccountType.Tag.ToString(); }
            set { this.cmbAccountType.Tag = value; }
        }
        decimal totOwnCost = 0;
        /// <summary>
        /// �Էѽ��
        /// </summary>
        public decimal TotOwnCost
        {
            get { return totOwnCost; }
            set { totOwnCost = value; }
        }
        /// <summary>
        /// ��ǰ�˻�
        /// </summary>
        public FS.HISFC.Models.Account.Account CurrentAccountInfo
        {
            get { return account; }
            set
            {
                account = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() < 0)
            {
                MessageBox.Show("��ʼ��ʧ�ܣ�");
                return;
            }
            this.ckSelect.CheckedChanged += new EventHandler(ckSelect_CheckedChanged);
            base.OnLoad(e);
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {

            //�˻�����
            alAccountType = managerIntegrate.GetAccountTypeList();

            if (alAccountType == null)
            {
                MessageBox.Show("�ʻ����ͼ���ʧ�ܣ�");
                return -1;
            }
            this.Clear();
            this.cmbAccountType.AddItems(alAccountType);
            this.cmbAccountType.Tag = "1";

            account = this.accountManager.GetAccountByCardNoEX(CardNo);
            if (account == null)
            {
                this.txtBaseVacancy.Text = string.Empty;
                this.txtDonateVacancy.Text = string.Empty;
            }
            else
            {
                FS.HISFC.Models.Account.AccountDetail accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail>();
                accountDetailList = this.accountManager.GetAccountDetail(account.ID, this.cmbAccountType.Tag.ToString(),"1");
                if (accountDetailList.Count > 0)
                {
                    accountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                    this.txtBaseVacancy.Text = "�����˻���" + accountDetail.BaseVacancy.ToString("F2");
                    this.txtDonateVacancy.Text = "�����˻���" + accountDetail.DonateVacancy.ToString("F2");
                }
                else
                {
                    this.txtBaseVacancy.Text = "�����˻���0.00";
                    this.txtDonateVacancy.Text = "�����˻���0.00";
                }
            }

            return 1;
        }
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        private void Clear()
        {
            this.txtAge.Text = string.Empty;
            this.txtBaseVacancy.Text = string.Empty;
            this.txtDonateVacancy.Text = string.Empty;
            this.txtHome.Text = string.Empty;
            this.txtIDNO.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtPhone.Text = string.Empty;
            this.txtSex.Text = string.Empty;

        }
        /// <summary>
        /// �Ƿ����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.txtCardNo.Enabled = this.ckSelect.Checked;
            this.isPayForAnother = this.ckSelect.Checked;
            if (!this.ckSelect.Checked)
            {
                this.Clear();
                this.EmpowerPatient = null;
                this.txtCardNo.Text = string.Empty;
                this.txtTip.Text = "��ǰ�˻���Ϣ��";
                account = this.accountManager.GetAccountByCardNoEX(CardNo);
                if (account == null)
                {
                    this.txtBaseVacancy.Text = string.Empty;
                    this.txtDonateVacancy.Text = string.Empty;
                }
                else
                {

                    //FS.HISFC.Components.OutpatientFee.Froms.frmDealBalance frmDealBalance = new FS.HISFC.Components.OutpatientFee.Froms.frmDealBalance();
        
                    CurrentAccountInfo = account;
                    FS.HISFC.Models.Account.AccountDetail accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                    System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail>();
                    accountDetailList = this.accountManager.GetAccountDetail(account.ID, this.cmbAccountType.Tag.ToString(),"1");
                    if (accountDetailList.Count > 0)
                    {
                        accountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                        this.txtBaseVacancy.Text = "�����˻���" + accountDetail.BaseVacancy.ToString("F2");
                        this.txtDonateVacancy.Text = "�����˻���" + accountDetail.DonateVacancy.ToString("F2");
                    }
                    else
                    {
                        this.txtBaseVacancy.Text = "�����˻���0.00";
                        this.txtDonateVacancy.Text = "�����˻���0.00";
                    }
                }


            }
            else
            {
                account = null;
                accountCard = null;
                this.txtTip.Text = "��������Ϣ��";
                this.txtBaseVacancy.Text = string.Empty;
                this.txtDonateVacancy.Text = string.Empty;
            }


        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ExecCmdKey();
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// �س�����
        /// </summary>
        protected virtual void ExecCmdKey()
        {
            if (this.txtCardNo.Focused)
            {
                GetAccountInfo();
            }
        }

        /// <summary>
        /// ������￨�Ż�ȡ�˻���Ϣ
        /// </summary>
        private void GetAccountInfo()
        {
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtCardNo.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("��������￨�ţ�");
                return;
            }
            int resultValue = accountManager.GetCardByRule(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                return;
            }

            this.txtCardNo.Text = accountCard.Patient.PID.CardNO;
            if (accountCard.Patient != null)
            {
                this.empowerPatient = accountCard.Patient;
            }
            this.Clear();
            this.SetInfo();
                
        }
        /// <summary>
        /// ��ʾ��������Ϣ
        /// </summary>
        private void SetInfo()
        {
            string telephone = "";

            if (accountCard.Patient.PhoneHome != null && accountCard.Patient.PhoneHome != "")
            {
                telephone = accountCard.Patient.PhoneHome;
            }
            else if (accountCard.Patient.Kin.RelationPhone != null && accountCard.Patient.Kin.RelationPhone != "")
            {
                telephone = accountCard.Patient.Kin.RelationPhone;
            }
            else
            {
                telephone = "";
            }
            string sexName = string.Empty;
            if (accountCard.Patient.Sex.ID.ToString() == "F")
            {
                sexName = "Ů";
            }
            else if (accountCard.Patient.Sex.ID.ToString() == "M")
            {
                sexName = "��";
            }
            else
            {
                sexName = "";
            }
            this.txtName.Text = accountCard.Patient.Name;
            this.txtAge.Text = this.accountManager.GetAge(accountCard.Patient.Birthday);
            this.txtSex.Text = sexName;
            this.txtPhone.Text = "��ϵ�绰��" + telephone;
            this.txtHome.Text = "��סַ��" + accountCard.Patient.AddressHome;
            this.txtIDNO.Text = "֤�����룺" + accountCard.Patient.IDCard;

            account = this.accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);

            if (account == null)
            {
                this.txtBaseVacancy.Text = "�����˻���0.00";
                this.txtDonateVacancy.Text = "�����˻���0.00" ;

            }
            else
            {
                //FS.HISFC.Components.OutpatientFee.Froms.frmDealBalance frmDealBalance = new FS.HISFC.Components.OutpatientFee.Froms.frmDealBalance();
                CurrentAccountInfo = account;
                FS.HISFC.Models.Account.AccountDetail accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail>();
                accountDetailList = this.accountManager.GetAccountDetail(account.ID, this.cmbAccountType.Tag.ToString(),"1");
                if (accountDetailList.Count > 0)
                {
                    accountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                    this.txtBaseVacancy.Text = "�����˻���" + accountDetail.BaseVacancy.ToString("F2");
                    this.txtDonateVacancy.Text = "�����˻���" + accountDetail.DonateVacancy.ToString("F2");
                }
                else
                {
                    this.txtBaseVacancy.Text = "�����˻���0.00";
                    this.txtDonateVacancy.Text = "�����˻���0.00";
                }
            }
        }
        #endregion

        private void cmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account == null)
            {
                this.txtBaseVacancy.Text = "�����˻���0.00";
                this.txtDonateVacancy.Text = "�����˻���0.00";
            }
            else
            {
                FS.HISFC.Models.Account.AccountDetail accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail>();
                accountDetailList = this.accountManager.GetAccountDetail(account.ID, this.cmbAccountType.Tag.ToString(), "1");
                if (accountDetailList.Count > 0)
                {
                    accountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                    this.txtBaseVacancy.Text = "�����˻���" + accountDetail.BaseVacancy.ToString("F2");
                    this.txtDonateVacancy.Text = "�����˻���" + accountDetail.DonateVacancy.ToString("F2");
                }
                else
                {
                    this.txtBaseVacancy.Text = "�����˻���0.00";
                    this.txtDonateVacancy.Text = "�����˻���0.00";
                }
            }

        }
    }
}
