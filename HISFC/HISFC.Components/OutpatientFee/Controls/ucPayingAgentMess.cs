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
        /// 构造函数
        /// </summary>
        public ucPayingAgentMess()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 账户信息
        /// </summary>
        private FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
        
        /// <summary>
        /// 账号信息
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;


        /// <summary>
        /// 当前患者的卡号
        /// </summary>
        private string cardNo = string.Empty;
        /// <summary>
        /// 当前患者的卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        private ArrayList alAccountType = new ArrayList();
        /// <summary>
        /// 代付人信息
        /// </summary>
        private HISFC.Models.RADT.PatientInfo empowerPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 代付人信息
        /// </summary>
        public HISFC.Models.RADT.PatientInfo EmpowerPatient
        {
            get { return empowerPatient; }
            set { empowerPatient = value; }
        }/// <summary>
        /// 是否代付
        /// </summary>
        private bool isPayForAnother = false;
        /// <summary>
        /// 是否代付
        /// </summary>
        public bool IsPayForAnother
        {
            get { return isPayForAnother; }
            set { isPayForAnother = value; }
        }
        /// <summary>
        /// 帐户类型编码
        /// </summary>
        public string AccountTypeCode
        {
            get { return this.cmbAccountType.Tag.ToString(); }
            set { this.cmbAccountType.Tag = value; }
        }
        decimal totOwnCost = 0;
        /// <summary>
        /// 自费金额
        /// </summary>
        public decimal TotOwnCost
        {
            get { return totOwnCost; }
            set { totOwnCost = value; }
        }
        /// <summary>
        /// 当前账户
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

        #region 函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() < 0)
            {
                MessageBox.Show("初始化失败！");
                return;
            }
            this.ckSelect.CheckedChanged += new EventHandler(ckSelect_CheckedChanged);
            base.OnLoad(e);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {

            //账户类型
            alAccountType = managerIntegrate.GetAccountTypeList();

            if (alAccountType == null)
            {
                MessageBox.Show("帐户类型加载失败！");
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
                    this.txtBaseVacancy.Text = "基本账户余额：" + accountDetail.BaseVacancy.ToString("F2");
                    this.txtDonateVacancy.Text = "赠送账户余额：" + accountDetail.DonateVacancy.ToString("F2");
                }
                else
                {
                    this.txtBaseVacancy.Text = "基本账户余额：0.00";
                    this.txtDonateVacancy.Text = "赠送账户余额：0.00";
                }
            }

            return 1;
        }
        /// <summary>
        /// 信息清空
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
        /// 是否代付选择
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
                this.txtTip.Text = "当前账户信息：";
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
                        this.txtBaseVacancy.Text = "基本账户余额：" + accountDetail.BaseVacancy.ToString("F2");
                        this.txtDonateVacancy.Text = "赠送账户余额：" + accountDetail.DonateVacancy.ToString("F2");
                    }
                    else
                    {
                        this.txtBaseVacancy.Text = "基本账户余额：0.00";
                        this.txtDonateVacancy.Text = "赠送账户余额：0.00";
                    }
                }


            }
            else
            {
                account = null;
                accountCard = null;
                this.txtTip.Text = "代付人信息：";
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
        /// 回车处理
        /// </summary>
        protected virtual void ExecCmdKey()
        {
            if (this.txtCardNo.Focused)
            {
                GetAccountInfo();
            }
        }

        /// <summary>
        /// 输入就诊卡号获取账户信息
        /// </summary>
        private void GetAccountInfo()
        {
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtCardNo.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("请输入就诊卡号！");
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
        /// 显示代付人信息
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
                sexName = "女";
            }
            else if (accountCard.Patient.Sex.ID.ToString() == "M")
            {
                sexName = "男";
            }
            else
            {
                sexName = "";
            }
            this.txtName.Text = accountCard.Patient.Name;
            this.txtAge.Text = this.accountManager.GetAge(accountCard.Patient.Birthday);
            this.txtSex.Text = sexName;
            this.txtPhone.Text = "联系电话：" + telephone;
            this.txtHome.Text = "现住址：" + accountCard.Patient.AddressHome;
            this.txtIDNO.Text = "证件号码：" + accountCard.Patient.IDCard;

            account = this.accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);

            if (account == null)
            {
                this.txtBaseVacancy.Text = "基本账户余额：0.00";
                this.txtDonateVacancy.Text = "赠送账户余额：0.00" ;

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
                    this.txtBaseVacancy.Text = "基本账户余额：" + accountDetail.BaseVacancy.ToString("F2");
                    this.txtDonateVacancy.Text = "赠送账户余额：" + accountDetail.DonateVacancy.ToString("F2");
                }
                else
                {
                    this.txtBaseVacancy.Text = "基本账户余额：0.00";
                    this.txtDonateVacancy.Text = "赠送账户余额：0.00";
                }
            }
        }
        #endregion

        private void cmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account == null)
            {
                this.txtBaseVacancy.Text = "基本账户余额：0.00";
                this.txtDonateVacancy.Text = "赠送账户余额：0.00";
            }
            else
            {
                FS.HISFC.Models.Account.AccountDetail accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail>();
                accountDetailList = this.accountManager.GetAccountDetail(account.ID, this.cmbAccountType.Tag.ToString(), "1");
                if (accountDetailList.Count > 0)
                {
                    accountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                    this.txtBaseVacancy.Text = "基本账户余额：" + accountDetail.BaseVacancy.ToString("F2");
                    this.txtDonateVacancy.Text = "赠送账户余额：" + accountDetail.DonateVacancy.ToString("F2");
                }
                else
                {
                    this.txtBaseVacancy.Text = "基本账户余额：0.00";
                    this.txtDonateVacancy.Text = "赠送账户余额：0.00";
                }
            }

        }
    }
}
