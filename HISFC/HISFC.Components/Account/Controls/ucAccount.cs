using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data; 
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.HISFC.BizProcess.Interface.Account;
using FS.HISFC.Components.Account.Forms;
using FS.HISFC.BizProcess.Interface.Fee;

namespace FS.HISFC.Components.Account.Controls
{
    public partial class ucAccount : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucAccount()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// �˻�ʵ��
        /// </summary>
        private FS.HISFC.Models.Account.Account account = null;

        /// <summary>
        /// ������ʵ��
        /// </summary>
        private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
        
        /// <summary>
        /// �˻�ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// �˻�����ʵ��
        /// </summary>
        private FS.HISFC.Models.Account.AccountRecord accountRecord = null;
        
        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �����ۺ�ҵ��� 
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        
        /// <summary>
        /// ���￨��
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string error = string.Empty;
        
        /// <summary>
        /// ���ת
        /// </summary>
        HISFC.BizProcess.Integrate.RADT radtInteger = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper markHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// Ԥ�������Żݴ���
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Account.IAccountProcessPrepay iAccountProcessPrepay = null;

        /// <summary>
        /// �������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// ���ʻ�ʱ���Ƿ���Ҫ��֤��Ч֤����0-��Ҫ��1-����Ҫ
        /// </summary>
        protected string JudgeCredentialCreating = "0";

        /// <summary>
        /// ���˻�ʱ�Ƿ��Զ�Ĭ������
        /// {9AE6E29C-1CAB-40b8-8F81-0F8379FEB8C9}
        /// </summary>
        protected bool IsDefaultPassword = true;

        /// <summary>
        /// �Ƿ��Լ����ݹ������ɷ�Ʊ��
        /// /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// </summary>
        protected bool isDefaultInvoiceNO = false;

        /// <summary>
        /// Ĭ������ȡ�ַ�ʽ(ͳ�Ƶ�����ʲô��Ŀ��) add by yerl
        /// </summary>
        private string bankPayType = "";
        /// <summary>
        /// ԤԼ���ӡʵ��
        /// </summary>
        private IPrintPrePayRecipe Iprint = null;
        #endregion

        #region ����

        #region ��ӡ����

        /// <summary>
        /// �Ƿ��ӡ�����˻�ƾ֤
        /// </summary>
        private bool isPrintCreateBill = false;

        /// <summary>
        /// �Ƿ��ӡ�����˻�ƾ֤
        /// </summary>
        [Category("��ӡ����"), Description("�Ƿ��ӡ�����˻�ƾ֤")]
        public bool IsPrintCreateBill
        {
            get
            {
                return isPrintCreateBill;
            }
            set
            {
                isPrintCreateBill = value;
            }
        }


        /// <summary>
        /// �Ƿ��ӡ�˷�ƾ֤
        /// </summary>
        private bool isPrintCancelVacancyBill = false;

        /// <summary>
        /// �Ƿ��ӡ�˷�ƾ֤
        /// </summary>
        [Category("��ӡ����"), Description("�Ƿ��ӡ�˷�ƾ֤")]
        public bool IsPrintCancelVacancyBill
        {
            get
            {
                return isPrintCancelVacancyBill;
            }
            set
            {
                isPrintCancelVacancyBill = value;
            }
        }

        /// <summary>
        /// �Ƿ��ӡ�ɷ�ƾ֤
        /// </summary>
        private bool isPrintPrePayBill = false;

        /// <summary>
        /// �Ƿ��ӡ�ɷ�ƾ֤
        /// </summary>
        [Category("��ӡ����"), Description("�Ƿ��ӡ�ɷ�ƾ֤")]
        public bool IsPrintPrePayBill
        {
            get
            {
                return isPrintPrePayBill;
            }
            set
            {
                isPrintPrePayBill = value;
            }
        }

        /// <summary>
        /// �Ƿ��ӡҵ�����ƾ֤
        /// </summary>
        private bool isPrintBusinessBill = false;

        /// <summary>
        /// �Ƿ��ӡҵ�����ƾ֤
        /// </summary>
        [Category("��ӡ����"), Description("�Ƿ��ӡҵ�����ƾ֤,ע����ͣ�á����á�������")]
        public bool IsPrintBusinessBill
        {
            get
            {
                return isPrintBusinessBill;
            }
            set
            {
                isPrintBusinessBill = value;
            }
        }

        #endregion

        /// <summary>
        /// �Ƿ��� ȡ�� ���ֿ���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��Լ����ݹ������ɷ�Ʊ��")]
        public bool IsDefaultInvoiceNO
        {
            get { return isDefaultInvoiceNO; }
            set { isDefaultInvoiceNO = value; }
        }
        /// <summary>
        /// ���п�ȡ��֧����ʽ
        /// </summary>
        [Category("�ؼ�����"), Description("���п�ȡ��֧����ʽ Ĭ��CH")]
        public string BankPayType { get { return bankPayType; } set { bankPayType = value; } }

        /// <summary>
        /// �Ƿ��� ȡ�� ���ֿ���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��� ȡ�� ���ֿ���")]
        public bool BlnIsLeverageControl
        {
            get { return blnIsLeverageControl; }
            set { blnIsLeverageControl = value; }
        }
        /// <summary>
        /// �Ƿ��� ȡ�� ���ֿ���
        /// </summary>
        bool blnIsLeverageControl = false;

        /// <summary>
        /// ��ֵ�����޶Ĭ�� 1000
        /// </summary>
        [Category("�ؼ�����"), Description("��ֵ�����޶�")]
        public decimal DecLimitePerpayMoney
        {
            get { return decLimitePerpayMoney; }
            set { decLimitePerpayMoney = value; }
        }
        /// <summary>
        /// ��ֵ�����޶Ĭ�� 1000
        /// </summary>
        decimal decLimitePerpayMoney = 1000;



        /// <summary>
        /// ������Ƿ��Զ�����
        /// </summary>
        bool isSaveClear = false;
        [Category("�ؼ�����"), Description("������Ƿ��Զ�����")]
        public bool IsSaveClear
        {
            get { return isSaveClear; }
            set { isSaveClear = value; }
        }

        [Description("�Ƿ���ʾ���￨�б�"), Category("����")]
        public bool IsShowCardSheet
        {
            get
            {
                return this.spcard.Visible;
            }
            set
            {
                this.spcard.Visible = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            //��俨����
            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            this.cmbCardType.AddItems(al);
            markHelper.ArrayObject = al;
            //֤������
            this.cmbIdCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
            this.panelPatient.Visible = false;
            this.btnShow.Tag = this.panelPatient.Visible;
            this.ActiveControl = this.txtMarkNo;
            //��ʼ���ӿ�
            this.InitInterface();
            ucRegPatientInfo1.Enabled = false;
            ucRegPatientInfo1.IsShowTitle = false;

            if (cmbPayType.Items.Count > 0)
            {
                this.cmbPayType.SelectedIndex = 0;
            }
            
            // 
            // {15148270-E4C9-4724-8227-524C9C0A3076}
            this.JudgeCredentialCreating = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.AccountConstant.JudgeCredentialCreating, false);
            //{9AE6E29C-1CAB-40b8-8F81-0F8379FEB8C9}
            this.IsDefaultPassword = controlParamIntegrate.GetControlParam<bool>("S00033", false);

          
              //this.lblDefualt.Visible = this.IsDefaultPassword;
            
        }

        /// <summary>
        /// �����˻���Ϣ
        /// </summary>
        private  void GetAccountByMark()
        {  
            //����˻���Ϣ
            this.account = this.accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            
            if (this.account != null)
            {
                this.txtVacancy.Text = this.account.BaseVacancy.ToString();
                this.txtDonateAmout.Text = this.account.DonateVacancy.ToString();
                this.txtLimit.Text = this.account.Limit.ToString();
                this.txtCoupon.Text = this.account.CouponVacancy.ToString();
                decimal decVacancy = 0;
                decimal decUnFeeMoney = 0;
                string strMsg = "";
                decVacancy = this.account.BaseVacancy;
                int iRes = feeIntegrate.QueryUnFeeByCarNo(this.accountCard.Patient.PID.CardNO, out decUnFeeMoney, out strMsg);
                if (iRes <= 0)
                {
                    MessageBox.Show(strMsg);
                }
                if (decUnFeeMoney > 0)
                {
                    lblUnFeeMoney.Text = decUnFeeMoney.ToString();
                    if (decUnFeeMoney > decVacancy)
                    {
                        this.lblNeedPayMoney.Text = (decUnFeeMoney - decVacancy).ToString();
                    }
                    else
                    {
                        this.lblNeedPayMoney.Text = "";
                    }
                }


                //����״̬
                if (this.account.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    
                    SetControlState(1);
                }
                //ͣ��״̬
                else if(this.account.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    SetControlState(2);
                }
            }
            else
            {
                SetControlState(0);
                this.txtVacancy.Text = string.Empty;
                this.txtDonateAmout.Text = string.Empty;
            }
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        private void ShowPatienInfo(string CardNo)
        {
            this.ucRegPatientInfo1.CardNO = CardNo;
            HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            this.txtName.Text=patient.Name;
            this.txtSex.Text=patient.Sex.Name;
            this.txtAge.Text = accountManager.GetAge(patient.Birthday);
            this.txtIdCardNO.Text = patient.IDCard;
            txtMarkNo.Text = CardNo;
            this.cmbIdCardType.Tag = patient.IDCardType.ID;
            FS.FrameWork.Models.NeuObject tempObj = null;
            tempObj = managerIntegrate.GetConstant(HISFC.Models.Base.EnumConstant.NATION.ToString(), patient.Nationality.ID);
            if (tempObj != null)
            {
                this.txtNation.Text = tempObj.Name;
            }
            tempObj = managerIntegrate.GetConstant(HISFC.Models.Base.EnumConstant.COUNTRY.ToString(), patient.Country.ID);
            if (tempObj != null)
            {
                this.txtCountry.Text = tempObj.Name;
            }
            this.txtsiNo.Text = patient.SSN;
        }

        /// <summary>
        /// �����˻�״̬���ÿؼ�״̬
        ///<param name="aMod">0:δ�����˻�����ǰ�˻��Ѿ�ע�� 1:�˻�����״̬ 2:�˻�ͣ��״̬</param>
        /// </summary>
        private void SetControlState(int aMod)
        {
            switch (aMod)
            {
                case 0:
                    {
                        this.toolbarService.SetToolButtonEnabled("�½��˻�", true);
                        this.toolbarService.SetToolButtonEnabled("��ȡ", false);
                        this.toolbarService.SetToolButtonEnabled("����", false);
                        // ����˻�ȡ�ֹ���
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        this.toolbarService.SetToolButtonEnabled("ȡ��", false);
                        this.toolbarService.SetToolButtonEnabled("����", false);
                        this.toolbarService.SetToolButtonEnabled("ͣ���˻�", false);
                        this.toolbarService.SetToolButtonEnabled("�����˻�", false);
                        this.toolbarService.SetToolButtonEnabled("ע���˻�", false);
                        this.toolbarService.SetToolButtonEnabled("�޸�����", false);
                        this.toolbarService.SetToolButtonEnabled("�����˻�", false);
                        this.toolbarService.SetToolButtonEnabled("Ƿ�Ѳ�ѯ", false);
                        this.txtpay.Enabled = false;
                        this.cmbPayType.Enabled = false;
                        break;
                    }
                case 1:
                    {
                        this.toolbarService.SetToolButtonEnabled("�½��˻�", false);
                        this.toolbarService.SetToolButtonEnabled("��ȡ", true);
                        this.toolbarService.SetToolButtonEnabled("����", true);
                        // ����˻�ȡ�ֹ���
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        this.toolbarService.SetToolButtonEnabled("ȡ��", true);
                        this.toolbarService.SetToolButtonEnabled("����", true);
                        this.toolbarService.SetToolButtonEnabled("ͣ���˻�", true);
                        this.toolbarService.SetToolButtonEnabled("�����˻�", false);
                        this.toolbarService.SetToolButtonEnabled("ע���˻�", true);
                        this.toolbarService.SetToolButtonEnabled("�޸�����", true);
                        this.toolbarService.SetToolButtonEnabled("�����˻�", true);
                        this.toolbarService.SetToolButtonEnabled("Ƿ�Ѳ�ѯ", true);
                        this.txtpay.Enabled = true;
                        this.cmbPayType.Enabled = true;
                        this.cmbPayType.Focus();
                        break;
                    }
                case 2:
                    {
                        this.toolbarService.SetToolButtonEnabled("�½��˻�", false);
                        this.toolbarService.SetToolButtonEnabled("��ȡ", false);
                        this.toolbarService.SetToolButtonEnabled("����", false);
                        // ����˻�ȡ�ֹ���
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        this.toolbarService.SetToolButtonEnabled("ȡ��", false);
                        this.toolbarService.SetToolButtonEnabled("����", false);
                        this.toolbarService.SetToolButtonEnabled("ͣ���˻�", false);
                        this.toolbarService.SetToolButtonEnabled("�����˻�", true);
                        this.toolbarService.SetToolButtonEnabled("ע���˻�", false);
                        this.toolbarService.SetToolButtonEnabled("�޸�����", false);
                        this.toolbarService.SetToolButtonEnabled("�����˻�", true);
                        this.toolbarService.SetToolButtonEnabled("Ƿ�Ѳ�ѯ", false);
                        this.txtpay.Enabled = false;
                        this.cmbPayType.Enabled = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// �˻�ʵ��
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.Account GetAccount()
        {
            try
            {
                //�˻���Ϣ
                account = new FS.HISFC.Models.Account.Account();
                account.ID = accountManager.GetAccountNO();
                account.AccountCard = accountCard;
                //�Ƿ�ȡĬ�����룬ϵͳ���ã����߳���һ��Ĭ�����롣
                //�˻�����
                if (!this.IsDefaultPassword)
                {
                    ucEditPassWord uc = new ucEditPassWord(false);
                    FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                    if (uc.FindForm().DialogResult != DialogResult.OK) return null;
                    //��������
                    account.PassWord = uc.PwStr;
                }
                else
                {
                    account.PassWord = "000000";
                }


                //�Ƿ����
                account.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                return account;
            }
            catch 
            {
                MessageBox.Show("��ȡ�˻���Ϣʧ�ܣ�");
                return null;
            }
        }

        /// <summary>
        /// �õ����Ľ���ʵ��
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.AccountRecord GetAccountRecord()
        {
            try
            {
                //������Ϣ
                accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                accountRecord.AccountNO = this.account.ID;//�ʺ�
                accountRecord.Patient = accountCard.Patient;//���￨��
                accountRecord.FeeDept.ID = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//���ұ���
                accountRecord.Oper.ID = accountManager.Operator.ID;//����Ա
                accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//����ʱ��
                accountRecord.IsValid = true;//�Ƿ���Ч
                return accountRecord;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// �����ʾ����
        /// </summary>
        private void Clear()
        {
            this.txtMarkNo.Text = string.Empty;
            this.txtMarkNo.Tag = string.Empty;
            this.cmbCardType.Tag = string.Empty;
            this.cmbCardType.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.cmbIdCardType.Tag = string.Empty;
            this.cmbIdCardType.Text = string.Empty;
            this.txtIdCardNO.Text = string.Empty;
            this.txtNation.Text = string.Empty;
            this.txtCountry.Text = string.Empty;
            this.txtsiNo.Text = string.Empty;
            this.accountCard = null;
            ClearAccountInfo();
            this.txtMarkNo.Focus();
            
            this.lblUnFeeMoney.Text = "";
            this.lblNeedPayMoney.Text = "";

            if (this.cmbPayType.Items.Count > 0)
            {
                this.cmbPayType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// ����˻���Ϣ
        /// </summary>
        private void ClearAccountInfo()
        {
            this.txtVacancy.Text = string.Empty;
            this.txtVacancy.Text = string.Empty;
            this.cmbPayType.Text = string.Empty;
            this.cmbPayType.Tag = string.Empty;
            this.txtDonateAmout.Text = string.Empty;
            this.txtpay.Text = string.Empty;
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            }
            if (this.spcard.Rows.Count > 0)
            {
                this.spcard.Rows.Remove(0, this.spcard.Rows.Count);
            }

            if (this.spHistory.Rows.Count > 0)
            {
                this.spHistory.Rows.Remove(0, this.spHistory.Rows.Count);
            }

            this.account = null;
            accountRecord = null;
        }

        /// <summary>
        /// ����Ƿ���û��Ƿ���Ȩ
        /// </summary>
        /// <returns></returns>
        private bool IsEmpower()
        {
            AccountEmpower accountEmpower = new AccountEmpower();
            int resultValue = accountManager.QueryAccountEmpowerByEmpwoerCardNO(accountCard.Patient.PID.CardNO, ref accountEmpower);
            if (resultValue < 0)
            {
                MessageBox.Show("���Ҹ��û�����Ȩ��Ϣʧ�ܣ�");
                this.txtMarkNo.Text = string.Empty;
                this.txtMarkNo.Focus();
                return false;
            }
            if (resultValue > 0)
            {
                if (accountEmpower.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    MessageBox.Show("���û��ѱ���Ȩ�������ٽ����˻���");
                    this.txtMarkNo.Text = string.Empty;
                    this.txtMarkNo.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �ж�֤�����Ƿ�����˻�
        /// </summary>
        /// <returns></returns>
        private bool ValidIDCard()
        {
            if (this.JudgeCredentialCreating == "0")
            {
                //�ж�֤�����Ƿ�����˻�
                ArrayList accountList = accountManager.GetAccountByIdNO(this.txtIdCardNO.Text.Trim(), this.cmbIdCardType.Tag.ToString());
                if (accountList == null)
                {
                    MessageBox.Show("���һ����˻���Ϣʧ�ܣ�");
                    return false;
                }
                //����֤���Ų��һ����˻���Ϣ
                if (accountList.Count > 0)
                {
                    MessageBox.Show("��" + this.cmbIdCardType.Text + "���Ѵ����˻���");
                    return false;
                }
            }
            else
            {
                if (this.accountCard != null)
                {
                    
                }


            }
            return true;
        }
    
        /// <summary>
        /// �½����˻�
        /// </summary>
        protected virtual void NewAccount()
        {
            try
            {
                if (accountCard == null || accountCard.MarkNO == string.Empty)
                {
                    MessageBox.Show("��������￨�ţ�", "��ʾ", MessageBoxButtons.OK);
                    return;
                }

                if (this.JudgeCredentialCreating == "0")
                {
                    if (this.txtIdCardNO.Text == string.Empty)
                    {
                        MessageBox.Show("��������Ч֤���ţ�");
                        this.txtIdCardNO.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(this.txtIdCardNO.Text.Trim()))
                {
                    if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIdCardNO.Text.Trim(), ref error) < 0)
                    {
                        if(MessageBox.Show("���֤���Ϸ���" + error + " \r\n�Ƿ������", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                        this.txtIdCardNO.Focus();
                        this.txtIdCardNO.SelectAll();
                        return;
                        }
                    }
                }
                //�ж�֤�����Ƿ�����˻�
                if (!ValidIDCard()) return;

                //�ж��˻��Ƿ���Ȩ

                if (!IsEmpower()) return;

                //��ȡ�˻�ʵ��
                this.account = this.GetAccount();
                if (account == null) return;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //���»��߻�����Ϣ
                if (this.txtIdCardNO.Enabled && !string.IsNullOrEmpty(this.txtIdCardNO.Text.Trim()))
                {
                    HISFC.Models.RADT.PatientInfo patient = accountCard.Patient;
                    patient.IDCardType.ID = this.cmbIdCardType.Tag.ToString();
                    patient.IDCard = this.txtIdCardNO.Text.Trim();
                    //�������֤�Ż�ȡ�����Ա�
                    FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(patient.IDCard, ref error);
                    if (obj == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error);
                        return;
                    }
                    patient.Sex.ID = obj.ID;
                    //���ݻ������֤�Ż�ȡ����
                    string birthdate = Class.Function.GetBirthDayFromIdNO(patient.IDCard, ref error);
                    if (birthdate == "-1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error);
                        return;
                    }
                    patient.Birthday = FrameWork.Function.NConvert.ToDateTime(birthdate);
                    patient.Age = accountManager.GetAge(patient.Birthday);
                    if (radtInteger.UpdatePatientInfo(patient) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(radtInteger.Err);
                        return;
                    }

                }
                //�����˻���
                if (accountManager.InsertAccount(this.account) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����˻�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //�����˻���ˮ��Ϣ
                this.accountRecord = this.GetAccountRecord();
                if (this.accountRecord != null)
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.NewAccount;
                    if (accountManager.InsertAccountRecord(accountRecord) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����˻�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����˻�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����˻��ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SetControlState(1);
            }
            catch(Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�����˻�ʧ�ܣ�"+e.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PrintCreateAccountRecipe(account);

        }
        
        /// <summary>
        /// ���������˻���ȡ���Ľ��׼�¼
        /// </summary>
        /// <returns></returns>
        private void GetRecordToFp()
        {
            if (account == null) return;
            List<PrePay> list = this.accountManager.GetPrepayByAccountNOEX(account.ID, "0");// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (list == null)
            {
                MessageBox.Show(this.accountManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.SetAccountRecordToFp(list, this.neuSpread1_Sheet1);
        }

        /// <summary>
        /// ��ȡ�˻�Ԥ������ʷ����
        /// </summary>
        private void GetHistoryRecordToFp()
        {
            if (account == null) return;
            List<PrePay> list = this.accountManager.GetPrepayByAccountNOEX(account.ID, "1");// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (list == null)
            {
                MessageBox.Show(this.accountManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.SetAccountRecordToFp(list, this.spHistory);
        }

        /// <summary>
        /// ��ʾ�˻�Ԥ��������
        /// </summary>
        /// <param name="list">Ԥ��������</param>
        /// <param name="sheet">sheetView</param>
        private void SetAccountRecordToFp(List<PrePay> list, FarPoint.Win.Spread.SheetView sheet)
        {
            int count = sheet.Rows.Count;
            if (count > 0)
            {
                sheet.Rows.Remove(0, count);
            }
            foreach (PrePay prepay in list)
            {
                SetFp(prepay, sheet);
            }
        }

        /// <summary>
        /// ��ʾԤ������Ϣ
        /// </summary>
        /// <param name="prepay"></param>
        private void SetFp(PrePay prepay,FarPoint.Win.Spread.SheetView sheet)
        {
            int count = sheet.Rows.Count;
            sheet.Rows.Add(count, 1);
            sheet.Cells[count, 0].Text = prepay.InvoiceNO;
            if (prepay.BaseCost > 0)
            {
                sheet.Cells[count, 1].Text = "��ȡ";
            }
            else
            {
                if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    sheet.Cells[count, 1].Text = "����";

                }
                else if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    sheet.Cells[count, 1].Text = "�������";
                }
                else
                {
                    sheet.Cells[count, 1].Text = "��ȡ";
                }
            }
            if (prepay.ValidState !=  FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                sheet.Cells[count, 1].ForeColor = Color.Red;
            }
            //if (prepay.PayType.ID == "DC")
            //{
            //    sheet.Cells[count, 2].Text = prepay.DonateCost.ToString();
            //}
            //else
            //{
            //    sheet.Cells[count, 2].Text = prepay.FT.PrepayCost.ToString();
            //}
            sheet.Cells[count, 2].Text = prepay.BaseCost.ToString();
            sheet.Cells[count, 3].Text = prepay.DonateCost.ToString();
            sheet.Cells[count, 4].Text = prepay.PrePayOper.OperTime.ToString();
            //
            FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
            empl = managerIntergrate.GetEmployeeInfo(prepay.PrePayOper.ID);

            if (empl == null)
            { prepay.PrePayOper.Name = ""; }
            else
            {
                prepay.PrePayOper.Name = empl.Name;
            }
            sheet.Cells[count, 5].Text = prepay.PrePayOper.Name;
            sheet.Rows[count].Tag = prepay;
            ArrayList all = managerIntegrate.GetConstantList("PAYMODES");
            //sheet.Cells[count, 5].Text = prepay.PayType.ID == "CA" ? "�ֽ�" : "���п�";
            foreach (FS.HISFC.Models.Base.Const cons in all)
            {
                if (prepay.PayType.ID == cons.ID)
                {
                    prepay.PayType.Name = cons.Name;
                    break;
                }
            }
            sheet.Cells[count, 6].Text = prepay.PayType.Name;
            sheet.Cells[count, 7].Text = prepay.BaseVacancy.ToString();
            sheet.Cells[count, 8].Text = prepay.DonateVacancy.ToString();
        }

        /// <summary>
        /// ��ʾ�˻�����Ϣ
        /// </summary>
        private void GetCardRecordToFP()
        {
            if (this.spcard.Rows.Count > 0)
            {
                this.spcard.Rows.Remove(0, spcard.Rows.Count);
            }

            List<HISFC.Models.Account.AccountCard> list = accountManager.GetMarkList(accountCard.Patient.PID.CardNO);
            if (list == null && list.Count == 0) return;
            int rowIndex = 0;
            foreach (HISFC.Models.Account.AccountCard tempCard in list)
            {
                this.spcard.Rows.Add(this.spcard.Rows.Count, 1);
                rowIndex = this.spcard.Rows.Count - 1;
                this.spcard.Cells[rowIndex, 0].Text = tempCard.MarkNO;
                this.spcard.Cells[rowIndex, 1].Text = markHelper.GetName(tempCard.MarkType.ID);
                this.spcard.Cells[rowIndex, 2].Text = tempCard.IsValid.ToString();
            }
        }

        /// <summary>
        /// ������￨�Ż�ȡ�˻���Ϣ
        /// </summary>
        private void GetAccountInfo()
        {
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtMarkNo.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("��������￨�ţ�");
                return;
            }
            int resultValue = accountManager.GetCardByRule(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                this.Clear();
                return;
            }
            //�˻���ȨЧ��
            if (!this.IsEmpower())
            {
                this.Clear();
                return;
            }
            ClearAccountInfo();
            this.txtMarkNo.Text = accountCard.MarkNO;
            this.cmbCardType.Tag = accountCard.MarkType.ID;
            //��ʾ������Ϣ
            ShowPatienInfo(accountCard.Patient.PID.CardNO);
            //01 Ϊ���֤�ţ��ڳ���ά����ά��
            if (this.cmbIdCardType.Tag != null && this.cmbIdCardType.Tag.ToString() != string.Empty && this.txtIdCardNO.Text.Trim() != string.Empty)
            {
                this.txtIdCardNO.Enabled = false;
                //this.cmbPayType.Focus();
            }
            else
            {
                this.txtIdCardNO.Enabled = true;
                this.cmbIdCardType.Tag = "01";//���֤��
                this.txtIdCardNO.Focus();
            }

            //�����˻���Ϣ
            this.GetAccountByMark();
            //Ԥ�����¼
            GetRecordToFp();
            //����Ͽ���¼
            GetCardRecordToFP();
            //Ԥ������ʷ��¼
            GetHistoryRecordToFp();
        }

        /// <summary>
        /// �س�����
        /// </summary>
        protected virtual void ExecCmdKey() 
        {
            if (this.txtMarkNo.Focused)
            {
                GetAccountInfo();
                //this.QueryUnFeeByCarNo();
                this.cmbPayType.SelectedIndex = 0;
                this.txtpay.Focus();
                return;
            }
            //��֧����ʽ�лس�
            //if (this.cmbPayType.Focused)
            //{
            //    if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            //    {
            //        MessageBox.Show("��ѡ��֧����ʽ��", "��ʾ");
            //        return;
            //    }
            //    this.txtpay.Focus();
            //    this.txtpay.SelectAll();
            //    return;
            //}
            //{4390EB8E-AC45-4eb1-ADD3-7C96FEECFD93}
            //if (this.txtIdCardNO.Focused)
            //{
            //    NewAccount();
            //}
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        protected virtual void EditPassword()
        {
            if (!ValidAccountCard()) return;
            ucEditPassWord uc = new ucEditPassWord(true);
            uc.Account = this.account;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
        }

        /// <summary>
        /// ��֤
        /// </summary>
        /// <returns></returns>
        private bool ValidAccountCard()
        {
            //if (accountCard == null || string.IsNullOrEmpty(accountCard.MarkNO) || string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))
            if (accountCard == null || string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))            
            {
                MessageBox.Show("��������￨�ţ�", "��ʾ", MessageBoxButtons.OK);
                this.txtMarkNo.Focus();
                this.txtMarkNo.SelectAll();
                return false;
            }
            account = accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (account == null)
            {
                MessageBox.Show("�ÿ�δ�����˻����˻���ע�����뽨���˻���", "��ʾ");
                return false;
            }
            return true;
        }


        /// <summary>
        /// ֧��Ԥ����
        /// </summary>
        protected virtual void AccountPrePay()
        {
            //����շ�Ա�ǳ����ٶ��ô�س������ᵼ���ظ���ȡԤ������á�������txtpayʧȥ����,ͬʱ��txtPay��ֵ��Ϊ0
            //this.neuLabel1.Focus();
            string accountPay = this.txtpay.Text;
            //this.txtpay.Text = "0.00";
            string donateAccount = this.txtDonate.Text;

            #region ��֤
            if (!this.ValidAccountCard())
            {
                this.txtMarkNo.Focus();
                return;
            }
            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("��ѡ��֧����ʽ��", "��ʾ");
                this.cmbPayType.Focus();
                return;
            }
            decimal money = FS.FrameWork.Function.NConvert.ToDecimal(accountPay);
            decimal donate = FS.FrameWork.Function.NConvert.ToDecimal(donateAccount);
            if (money == 0)
            {
                if (MessageBox.Show("��ֵ��� " + money.ToString() + "Ԫ���Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (donate == 0)
                    {
                        MessageBox.Show("��ֵ���Ϊ0�����ͽ���Ϊ0");
                        txtpay.Focus();
                        return;
                    }
                }
                //MessageBox.Show("�����뽻�ѽ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //txtpay.Focus();
                //return;
            }
            else if (money > decLimitePerpayMoney)
            {
                if (MessageBox.Show("��ֵ��� " + money.ToString() + "Ԫ ���� ��ֵ�޶� ��" + decLimitePerpayMoney.ToString() + " Ԫ�� �Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    txtpay.Focus();
                    txtpay.SelectAll();
                    return;
                }
            }

            if (MessageBox.Show("�Ƿ�ȷ�ϳ�ֵ��", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                this.txtpay.Focus();
                return;
            }

            #endregion



            #region Ԥ����ʵ��
            //��ȡԤ����ʵ��
            HISFC.Models.Account.PrePay prePay = GetPrePayEX(FS.HISFC.Models.Base.EnumValidState.Valid,1);
            if (prePay == null)
            {
                this.txtpay.Focus();
                return;
            }

            string errText = string.Empty;
            if (this.iAccountProcessPrepay != null)
            {
                int returnValue = this.iAccountProcessPrepay.GetDerateCost(prePay, ref errText);
                if (returnValue < 0)
                {
                    MessageBox.Show("��ȡ�Żݽ����� "+ errText );
                    this.txtpay.Focus();
                    return;
                }
            }

            #endregion

            #region ��������
            //��������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


            if (!accountManager.AccountPrePayManagerEX(prePay, 1))// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(accountManager.Err, "����");
                this.txtpay.Focus();
                return;
            }
            //if (donate != 0)//lfhm// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            //{
            //    FS.HISFC.Models.Account.Account account1 = new FS.HISFC.Models.Account.Account();
            //    account1 = accountManager.GetAccountVacancyEX(this.ucRegPatientInfo1.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            //    prePay.BaseVacancy = 0;
            //    prePay.FT.PrepayCost = 0;
            //    prePay.PayType.ID = "DC";//֧����ʽ
            //    prePay.PayType.Name = "����";
            //    prePay.DonateCost = donate;
            //    prePay.DonateVacancy = account1.DonateAmout + donate;
            //    if (!accountManager.AccountPrePayManagerEX(prePay, 1))// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(accountManager.Err, "����");
            //        this.txtDonate.Focus();
            //        return;
            //    }
            //}
            FS.FrameWork.Management.PublicTrans.Commit();
             MessageBox.Show("���� ��" + accountPay + "�� �ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion

            this.GetAccountByMark();
            this.GetRecordToFp();

            prePay.BaseVacancy = this.account.BaseVacancy;

            #region ��ӡ 
            this.PrintPrePayRecipeEX(prePay);// {F69DC114-4EF6-4eb2-8018-0F7192139E27}
            #endregion

            //this.cmbPayType.Tag = string.Empty;
            this.txtpay.Text = "0.00";
            this.txtDonate.Text = "0.00";

            IOutpatientEvaluation patientEvaluation = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IOutpatientEvaluation)) as IOutpatientEvaluation;
            if (patientEvaluation != null)
            {
                patientEvaluation.WorkAppriaseEvaluation(prePay.Patient.PID.CardNO, prePay.Patient.Name);
            }

            if (isSaveClear)
            {
                this.Clear();
            }
        }

        /// <summary>
        /// ����Ԥ����
        /// </summary>
        protected virtual void AccountCancelPrePay()
        {
            if (!ValidAccountCard()) return;
            if (neuSpread1_Sheet1.Rows.Count == 0) return;
            if (this.neuSpread1_Sheet1.ActiveRow.Tag == null) return;
            PrePay prePay = this.neuSpread1_Sheet1.ActiveRow.Tag as PrePay;

            #region ��֤
            if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
            {
                MessageBox.Show("�ñʽ���ѷ�����¼�����ܷ�����", "��ʾ", MessageBoxButtons.OK);
                return;
            }
            if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                MessageBox.Show("�ñʽ��Ϊ�����¼�����ܷ�����", "��ʾ", MessageBoxButtons.OK);
                return;
            }
            if (this.account.BaseVacancy < prePay.BaseCost)
            {
                MessageBox.Show("�˻����㣬�����˴˱ʷ��ã�");
                return;
            }
                MessageBox.Show(prePay.PayType.ID);
            if (prePay.PayType.ID != "CA")
            {
                MessageBox.Show("ֻ���ֽ�֧���Ľ����ܷ�����");
                return;
            }
            #endregion

            if (MessageBox.Show("ȷ�Ϸ����˱ʽ�", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (!this.accountManager.AccountPrePayManager(prePay, 0))
            {
                MessageBox.Show(accountManager.Err, "����");
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�����ɹ���Ʊ�ݺ�Ϊ��" + prePay.InvoiceNO + "���Ϊ��" + prePay.BaseCost.ToString() + "��");
            this.GetAccountByMark();
            this.GetRecordToFp();
        }
             
        /// <summary>
        /// ͣ���˻�
        /// </summary>
        protected virtual void StopAccount()
        {
            if (!ValidAccountCard()) return;
            if (account == null) return;

            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
            {
                MessageBox.Show("���˻���ͣ�ã������ø��˻���");
                return;
            }
            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                MessageBox.Show("���˻���ע����");
                return;
            }
            if (MessageBox.Show("ȷ��ͣ���˻���", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) return;

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            bool isCancelVacancy = false;
            DialogResult resultValue = MessageBox.Show("ͣ���˻�ͬʱ���Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (resultValue == DialogResult.Yes)
            {
                isCancelVacancy = true;
                if (!ValidCancelVacancy(accountCard.Patient.PID.CardNO))
                {
                    return;
                }
            }

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //�˷ѽ��
            string MessageStr = string.Empty;
            //�Ƿ�ˢ��Ԥ��������
            bool isFreshPrePay = false;
            try
            {

                #region ��ͣ���˻�ʱ�Ƿ�������
                decimal vacancy = 0;
                //�ж��˻����
                int result = accountManager.GetVacancy(accountCard.Patient.PID.CardNO, ref vacancy);
                if (result <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.accountManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                //�����ͣ���˻�ʱ����˻������ڣ���ʾ�Ƿ�����˻�
                string errText = string.Empty;
                if (vacancy > 0)
                {
                    //�����˻�
                    if (isCancelVacancy)
                    {
                        MessageStr = "Ӧ���û�" + vacancy.ToString() + "Ԫ��";
                        //ˢ���˻�Ԥ��������
                        isFreshPrePay = true;
                        if (!this.UpdateAccountVacancy(vacancy, "�����˻�", OperTypes.BalanceVacancy, ref errText))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(errText);
                            return;
                        }
                    }
                }
                #endregion

                //�����˻�״̬
                bool bl = UpdateAccountState(FS.HISFC.Models.Base.EnumValidState.Invalid,ref errText);
                if (!bl)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errText);
                    return;
                }
                if (accountManager.UpdateEmpowerState(account.ID, FS.HISFC.Models.Base.EnumValidState.Ignore, FS.HISFC.Models.Base.EnumValidState.Valid) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����˻���Ȩ��Ϣʧ�ܣ�" + accountManager.Err);
                    return;
                }

            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ͣ���˻�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            
            MessageBox.Show("ͣ���˻��ɹ���\n" + MessageStr, "��ʾ");
            //����״̬
            SetControlState(2);
            //ˢ���˻���Ϣ
            GetAccountByMark();
            if (isFreshPrePay)
            {
                this.GetRecordToFp();
                this.GetHistoryRecordToFp();
            }

        }

        /// <summary>
        /// �����˻����
        /// </summary>
        protected virtual void BalanceVacancy()
        {
            if (!this.ValidAccountCard())
            {
                this.txtMarkNo.Focus();
                return;
            }
            decimal vacancy = 0;
            //�ж��˻����
            int result = accountManager.GetVacancy(accountCard.Patient.PID.CardNO, ref vacancy);
            if (result <= 0)
            {
                MessageBox.Show(this.accountManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (vacancy <= 0)
            {
                MessageBox.Show("���˻������������ܽ����˻���");
                return;
            }
            txtpay.Text = vacancy.ToString();
            #region �µĽ������

            #region ��֤

            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("��ѡ��֧����ʽ��", "��ʾ");
                this.cmbPayType.Focus();
                return;
            }

            else if (vacancy > decLimitePerpayMoney && cmbPayType.Text == "�ֽ�")
            {
                if (MessageBox.Show("ȡ�ֽ�� " + vacancy.ToString() + "Ԫ ���� ȡ���޶� ��" + decLimitePerpayMoney.ToString() + " Ԫ�� �Ƿ����ʹ���ֽ�ȡ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    txtpay.Focus();
                    return;
                }
            }

            if (MessageBox.Show("�Ƿ�ȷ�Ͻ����ʻ�?", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                this.txtpay.Focus();
                return;
            }
            #endregion

            #region ��������

            if (blnIsLeverageControl)
            {
                decimal remainCACost = this.GetRemainCACost(accountCard.Patient.PID.CardNO);
                if (remainCACost < vacancy)
                {
                    MessageBox.Show("��ǰ�˻��ֽ�������ȡ�ֽ�");
                    return;
                }
            }

            #endregion

            //��֤��Ȩ���˻�����
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;
            #region ȡ��
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region Ԥ����ʵ��

            //��ȡԤ����ʵ��
            PrePay prePay = GetPrePayEX(FS.HISFC.Models.Base.EnumValidState.Ignore,0);
            if (prePay == null)
            {
                this.txtpay.Focus();
                return;
            }
            #endregion

            if (!accountManager.AccountPrePayManager(prePay, 0))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(accountManager.Err, "����");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("ȡ�ֳɹ�!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);


            this.GetAccountByMark();
            this.GetRecordToFp();
            prePay.BaseVacancy = this.account.BaseVacancy;

            #endregion

            #region ��ӡ
            // {48314E1F-72EC-4044-A41A-833C84687A40}
            PrintPrePayRecipe(prePay);
            #endregion
            if (isSaveClear)
            {
                Clear();
            }

            #endregion
            #region ԭ���Ľ������

            //#region ��������

            ////decimal remainCACost = this.GetRemainCACost(accountCard.Patient.PID.CardNO);
            ////if (remainCACost < vacancy)
            ////{
            ////    MessageBox.Show("��ǰ�˻��ֽ���Ϊ" + remainCACost.ToString() + "Ԫ�����������ֽ������ȫ��ȡ�֣�");
            ////    return;
            ////}

            //#endregion

            //if (MessageBox.Show("ȷ��Ҫ������˻�����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            ////{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            //if (!ValidCancelVacancy(accountCard.Patient.PID.CardNO))
            //{
            //    return;
            //}

            //string errText = string.Empty;
            //bool resultValue = this.UpdateAccountVacancy(vacancy, "�����˻�", OperTypes.BalanceVacancy, ref errText);
            //if (!resultValue)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show(errText);
            //    return;
            //}
            //FS.FrameWork.Management.PublicTrans.Commit();
            //MessageBox.Show("Ӧ���ֽ�" + vacancy.ToString() + "Ԫ��");
            #endregion
        }

        /// <summary>
        /// �����˻�
        /// </summary>
        protected virtual void AginAccount()
        {
           // if (!ValidAccountCard()) return;


            account = accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}

            if (account == null)
            {
                MessageBox.Show("�ÿ�δ�����˻����˻���ע�����뽨���˻���", "��ʾ");
                return;
            }
          

            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                MessageBox.Show("���˻�������ͣ��״̬���������ø��˻���");
                return;
            }
            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                MessageBox.Show("���˻���ע����");
                return;
            }

            if (MessageBox.Show("ȷ�����ø��˻���", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //�����˻�״̬
            string errText = string.Empty;
            bool bl = UpdateAccountState(FS.HISFC.Models.Base.EnumValidState.Valid,ref errText);
            if (!bl)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);
                return;
            }
            //������Ȩ�˻�״̬
            if (accountManager.UpdateEmpowerState(account.ID, FS.HISFC.Models.Base.EnumValidState.Valid, FS.HISFC.Models.Base.EnumValidState.Ignore) < 0)
            {
                 FS.FrameWork.Management.PublicTrans.RollBack();
                 MessageBox.Show("������Ȩ�˻���Ϣʧ�ܣ�");
                 return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�����˻��ɹ���");
            SetControlState(1);
        }

        /// <summary>
        /// �ش�Ʊ
        /// </summary>
        protected virtual void ReprintInvoice()
        {
            try
            {
                if (!ValidAccountCard()) return;
                if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
                if (this.neuSpread1_Sheet1.ActiveRow.Tag == null) return;
                PrePay prePay = (this.neuSpread1_Sheet1.ActiveRow.Tag as PrePay).Clone();

                #region ��֤
                if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    MessageBox.Show("�ñʷ���Ϊ������¼�����ܲ���", "��ʾ", MessageBoxButtons.OK);
                    return;
                }
                if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    MessageBox.Show("�ñʷ���Ϊ�����¼�����ܲ���", "��ʾ", MessageBoxButtons.OK);
                    return;
                }
                #endregion

                if (MessageBox.Show("�Ƿ�ȷ���ش�ô�ƾ֤��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                #region ���·�Ʊ״̬
                //���·�Ʊ״̬
                prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;//����
                if (accountManager.UpdatePrePayState(prePay) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������¼�Ѿ����й������������������״̬����!");
                    return;
                }
                #endregion

                #region ����������Ϣ
                //����
                prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;
                prePay.BaseCost = -prePay.BaseCost;
                prePay.OldInvoice = prePay.InvoiceNO;

                prePay.PrePayOper.ID = this.accountManager.Operator.ID;//add by sung 2009-2-26 {E5178DF3-9C61-43b3-BF61-3EA99A9989E2}
                
                if (accountManager.InsertPrePay(prePay) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���Ϸ�Ʊ����" + accountManager.Err, "����");
                    return;
                }
                #endregion

                #region �����շ���Ϣ
                //��ȡ��Ʊ��
                string invoiceNO = this.feeIntegrate.GetNewInvoiceNO("A");
                if (invoiceNO == null || invoiceNO == string.Empty)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��÷�Ʊ�ų���!" + this.feeIntegrate.Err);
                    return;
                }
                //prePay.OldInvoice = invoiceNO;
                prePay.InvoiceNO = invoiceNO;
                prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                prePay.BaseCost = Math.Abs(prePay.BaseCost);

                prePay.PrePayOper.ID = this.accountManager.Operator.ID;//add by sung 2009-2-26 {E5178DF3-9C61-43b3-BF61-3EA99A9989E2}

                if (accountManager.InsertPrePay(prePay) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ӡ��Ʊʧ�ܣ�");
                    return;
                }
                prePay.PayType.Name = this.cmbPayType.GetNameByID(prePay.PayType.ID);
                #endregion

                //��ӡƱ��
                FS.HISFC.Models.Account.AccountRecord accRecord = accountManager.GetAccountRecord(prePay.Patient.PID.CardNO, prePay.InvoiceNO);
                if (accRecord != null)
                {
                    // ���׺����
                    prePay.BaseVacancy = accRecord.BaseVacancy;
                }
                this.PrintPrePayRecipe(prePay);
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ӡ��Ʊʧ�ܣ�" + ex.Message);
                return;
            }
            GetRecordToFp();
          
        }
        /// <summary>
        /// ����Ʊ
        /// </summary>
        protected virtual void ReprintInvoiceNotInsert()
        {
            try
            {
                if (!ValidAccountCard()) return;
                if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
                if (this.neuSpread1_Sheet1.ActiveRow.Tag == null) return;
                PrePay prePay = (this.neuSpread1_Sheet1.ActiveRow.Tag as PrePay).Clone();

                if (MessageBox.Show("�Ƿ�ȷ�ϲ���ô�ƾ֤��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //������ΪʲôҪ������״̬�ĳ�ignore ȥ�� by yerl
                //FS.HISFC.Models.Base.EnumValidState printState = prePay.ValidState;

                //prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;

                FS.HISFC.Models.Account.AccountRecord accRecord = accountManager.GetAccountRecord(prePay.Patient.PID.CardNO, prePay.InvoiceNO);
                if (accRecord != null)
                {
                    // ���׺����
                    prePay.BaseVacancy = accRecord.BaseVacancy;
                }
                //��ӡƱ��
                prePay.Memo = txtMarkNo.Text;
                prePay.IsHostory = true;
                this.PrintPrePayRecipe(prePay);

                //prePay.ValidState = printState;
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ӡ��Ʊʧ�ܣ�" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// ע���˻�
        /// </summary>
        protected virtual void CancelAccount()
        {
            
            if (!ValidAccountCard()) return;
            if (MessageBox.Show("ȷ��ע�����˻���", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No) return;
            //��֤����
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            if (!ValidCancelVacancy(accountCard.Patient.PID.CardNO))
            {
                return;
            }


            decimal vacancy = 0;
            string messStr = string.Empty;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            
            //�ж��˻����
            int result = accountManager.GetVacancy(accountCard.Patient.PID.CardNO, ref vacancy);
            if (result <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.accountManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //�����˻����
            string errText = string.Empty;
            if (vacancy > 0)
            {
                
                if (!UpdateAccountVacancy(vacancy, "�����˻�", OperTypes.BalanceVacancy, ref errText))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errText);
                    return;
                }
                messStr = "Ӧ���û�" + vacancy.ToString();

            }
            //�����˻�״̬
            bool bl = UpdateAccountState(FS.HISFC.Models.Base.EnumValidState.Ignore,ref errText);
            if (!bl)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);
                return;
            }
            if (accountManager.UpdateEmpowerState(account.ID, FS.HISFC.Models.Base.EnumValidState.Extend, FS.HISFC.Models.Base.EnumValidState.Valid) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("������Ȩ�˻�״̬ʧ�ܣ�");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("ע���˻��ɹ���" + messStr);
            SetControlState(0);
            this.GetRecordToFp();
            this.GetHistoryRecordToFp();
        }

        /// <summary>
        /// ��ȡԤԼ��ʵ��
        /// </summary>
        /// <param name="state">ʵ��״̬</param>
        /// <returns></returns>
        private PrePay GetPrePay(FS.HISFC.Models.Base.EnumValidState state)
        {
            /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
            string invoiceNO = "";
            #region ��ȡ��Ʊ��
            if (IsDefaultInvoiceNO) //�д���Ժ��ȡ��Ʊ����
            {
                invoiceNO = this.accountManager.GetNewInvoiceNO("A");
            }
            else
            {
                invoiceNO = this.feeIntegrate.GetNewInvoiceNO("A");
                if (invoiceNO == null || invoiceNO == string.Empty)
                {
                    MessageBox.Show("��÷�Ʊ�ų���!" + this.feeIntegrate.Err);
                    this.txtpay.Focus();
                    return null;
                }
            }
            #endregion
            decimal vacancy = 0;
            int result = accountManager.GetVacancy(this.ucRegPatientInfo1.CardNO, ref vacancy);
            if (result <= 0)
            {
                return null;
            }
            #region Ԥ����ʵ��
            HISFC.Models.Account.PrePay prePay = new FS.HISFC.Models.Account.PrePay();
            prePay.Patient = this.ucRegPatientInfo1.GetPatientInfomation();//���߻�����Ϣ
            prePay.InvoiceNO = invoiceNO;
            prePay.PayType.ID = this.cmbPayType.Tag.ToString();//֧����ʽ
            prePay.PayType.Name = this.cmbPayType.Text;

            //prePay.Bank = this.cmbPayType.bank.Clone();//��������
            if (prePay.PayType.ID != "CA" && !string.IsNullOrEmpty(BankPayType))
            {
                prePay.PayType.ID = BankPayType;//֧����ʽ
                prePay.PayType.Name = "���п�";
            }

            prePay.PrePayOper.ID = accountManager.Operator.ID;//����Ա���
            prePay.PrePayOper.Name = accountManager.Operator.Name;//����Ա����
            prePay.ValidState = state;
            prePay.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(txtpay.Text);//Ԥ����
            prePay.PrePayOper.OperTime = accountManager.GetDateTimeFromSysDateTime();//ϵͳʱ��
            prePay.AccountNO = account.ID; //�ʺ�
            prePay.IsHostory = false; //�Ƿ���ʷ����
            prePay.BaseVacancy = vacancy + FS.FrameWork.Function.NConvert.ToDecimal(txtpay.Text);//lfhm// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            prePay.Memo = txtMarkNo.Text;

            FS.HISFC.Models.Account.Account account1 = new FS.HISFC.Models.Account.Account();// {A38BE2BE-6C83-4cdc-86EB-B016C434B98F}
            account1 = accountManager.GetAccountVacancyEX(this.ucRegPatientInfo1.CardNO);
            prePay.DonateCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDonate.Text);
            prePay.DonateVacancy = account1.DonateVacancy + FS.FrameWork.Function.NConvert.ToDecimal(this.txtDonate.Text);
           
            #endregion
            return prePay;
        }
        /// <summary>
        /// ��ȡԤԼ��ʵ��
        /// </summary>
        /// <param name="state">ʵ��״̬</param>
        /// <returns></returns>
        private PrePay GetPrePayEX(FS.HISFC.Models.Base.EnumValidState state,int mode)
        {
            decimal pay = FS.FrameWork.Function.NConvert.ToDecimal(txtpay.Text);
            decimal donate = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDonate.Text);
            if (mode == 0)
            {
                pay = -pay;
                donate = -donate;
            }
            /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
            string invoiceNO = "";
            #region ��ȡ��Ʊ��
            if (IsDefaultInvoiceNO) //�д���Ժ��ȡ��Ʊ����
            {
                invoiceNO = this.accountManager.GetNewInvoiceNO("A");
            }
            else
            {
                invoiceNO = this.feeIntegrate.GetNewInvoiceNO("A");
                if (invoiceNO == null || invoiceNO == string.Empty)
                {
                    MessageBox.Show("��÷�Ʊ�ų���!" + this.feeIntegrate.Err);
                    this.txtpay.Focus();
                    return null;
                }
            }
            #endregion
            decimal vacancy = 0;
            int result = accountManager.GetVacancy(this.ucRegPatientInfo1.CardNO, ref vacancy);
            if (result <= 0)
            {
                return null;
            }
            #region Ԥ����ʵ��
            HISFC.Models.Account.PrePay prePay = new FS.HISFC.Models.Account.PrePay();
            prePay.Patient = this.ucRegPatientInfo1.GetPatientInfomation();//���߻�����Ϣ
            prePay.InvoiceNO = invoiceNO;
            prePay.PayType.ID = this.cmbPayType.Tag.ToString();//֧����ʽ
            prePay.PayType.Name = this.cmbPayType.Text;

            //prePay.Bank = this.cmbPayType.bank.Clone();//��������
            if (prePay.PayType.ID != "CA" && !string.IsNullOrEmpty(BankPayType))
            {
                prePay.PayType.ID = BankPayType;//֧����ʽ
                prePay.PayType.Name = "���п�";
            }

            prePay.PrePayOper.ID = accountManager.Operator.ID;//����Ա���
            prePay.PrePayOper.Name = accountManager.Operator.Name;//����Ա����
            prePay.ValidState = state;
            prePay.BaseVacancy = pay;//Ԥ����
            prePay.PrePayOper.OperTime = accountManager.GetDateTimeFromSysDateTime();//ϵͳʱ��
            prePay.AccountNO = account.ID; //�ʺ�
            prePay.IsHostory = false; //�Ƿ���ʷ����
            prePay.BaseVacancy = vacancy + pay;//lfhm// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            prePay.Memo = txtMarkNo.Text;

            FS.HISFC.Models.Account.Account account1 = new FS.HISFC.Models.Account.Account();// {A38BE2BE-6C83-4cdc-86EB-B016C434B98F}
            account1 = accountManager.GetAccountVacancyEX(this.ucRegPatientInfo1.CardNO);
            prePay.DonateCost = donate;
            prePay.DonateVacancy = account1.DonateVacancy + donate;

            #endregion
            return prePay;
        }
        /// <summary>
        /// �˻�ȡ�� ԭ�е��߼�����,�Ҷ���������ȫ���޸� by yerl
        /// {4679504A-CEDA-44a8-8C67-DB7F847C6450}
        /// </summary>
        /// <returns></returns>
        private void AccountTaken()
        {
            //����շ�Ա�ǳ����ٶ��ô�س������ᵼ���ظ���ȡԤ������á�������txtpayʧȥ����,ͬʱ��txtPay��ֵ��Ϊ0
            this.neuLabel1.Focus();
            string accountPay = this.txtpay.Text;
            //this.txtpay.Text = "0.00";

            #region ��֤
            if (!this.ValidAccountCard())
            {
                this.txtMarkNo.Focus();
                return;
            }
            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("��ѡ��֧����ʽ��", "��ʾ");
                this.cmbPayType.Focus();
                return;
            }
            decimal money = FS.FrameWork.Function.NConvert.ToDecimal(accountPay);
            if (money == 0)
            {
                MessageBox.Show("������ȡ�ֽ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpay.Focus();
                return;
            }
            else if (money > this.account.BaseVacancy)
            {
                MessageBox.Show("�ʻ����㣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpay.Focus();
                return;
            }
            else if (money > decLimitePerpayMoney && cmbPayType.Text == "�ֽ�")
            {
                if (MessageBox.Show("ȡ�ֽ�� " + money.ToString() + "Ԫ ���� ȡ���޶� ��" + decLimitePerpayMoney.ToString() + " Ԫ�� �Ƿ����ʹ���ֽ�ȡ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    txtpay.Focus();
                    return;
                }
            }

            if (MessageBox.Show("�Ƿ�ȷ��ȡ��?", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                this.txtpay.Focus();
                return;
            }
            #endregion

            #region ��������

            if (blnIsLeverageControl)
            {
                decimal remainCACost = this.GetRemainCACost(accountCard.Patient.PID.CardNO);
                if (remainCACost < money)
                {
                    MessageBox.Show("��ǰ�˻��ֽ�������ȡ�ֽ�");
                    return;
                }
            }

            #endregion

            //��֤��Ȩ���˻�����
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;
            #region ȡ��
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region Ԥ����ʵ��

            //��ȡԤ����ʵ��
            PrePay prePay = GetPrePayEX(FS.HISFC.Models.Base.EnumValidState.Invalid,0);
            if (prePay == null)
            {
                this.txtpay.Focus();
                return;
            }
            #endregion

            if (!accountManager.AccountPrePayManagerEX(prePay, 0))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(accountManager.Err, "����");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("ȡ�ֳɹ�!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.GetAccountByMark();
            this.GetRecordToFp();
            prePay.BaseVacancy = this.account.BaseVacancy;
            this.txtpay.Text = "0.00";
            this.txtDonate.Text = "0.00";
            #endregion
            #region ��ӡ
            // {48314E1F-72EC-4044-A41A-833C84687A40}
            PrintPrePayRecipe(prePay);
            #endregion
            if (isSaveClear)
            {
                Clear();
            }
        }

        /// <summary>
        /// ���ݿ��Ż�ÿ�ȡ�ֽ��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        private decimal GetRemainCACost(string cardNo)
        {
            decimal remainCACost = 0;

            #region ��ѯ�˻����ֽ���

            //��ѯ�˻����׼�¼
            List<PrePay> prepayList = this.accountManager.GetPrepayByAccountNOEX(account.ID, "0");// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (prepayList == null)
            {
                MessageBox.Show(this.accountManager.Err);
                return 0;
            }

            //�˻����ֽ���
            decimal accountCostCA = 0;
            for (int i = 0; i < prepayList.Count; i++)
            {
                if (prepayList[i].PayType.ID == "CA")
                {
                    accountCostCA += prepayList[i].BaseCost;
                }
            }
            #endregion

            #region ��ѯ�Ѿ�ȡ�ֽ�� 

            decimal decHasTaken = 0;
            List<AccountRecord> accountRecord = this.accountManager.GetAccountRecordList(cardNo, "16");
            if (accountRecord != null && accountRecord.Count > 0)
            {
                foreach (AccountRecord ar in accountRecord)
                {
                    decHasTaken += ar.BaseCost;
                }
            }


            #endregion


            #region ��ѯ�˻����

            decimal remain = 0;

            if (this.accountManager.GetVacancy(account.CardNO, ref remain) == -1)
            {
                MessageBox.Show("��ѯ�˻�������" + this.accountManager.Err);
                return 0;
            }
            #endregion

            // ��ȡ����� <= �����ֽ� - ��ȡ�ֽ�
            remainCACost = accountCostCA + decHasTaken;
            if (remain < remainCACost)
            {
                remainCACost = remain;
            }


            return remainCACost;
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// �޸��˻�״̬
        /// </summary>
        /// <param name="validState">�˻�״̬</param>
        /// <returns>true �ɹ� falseʧ��</returns>
        private bool UpdateAccountState(HISFC.Models.Base.EnumValidState validState,ref string errText)
        {
            //�����˻�״̬
            if (accountManager.UpdateAccountState(account.ID, ((int)validState).ToString()) < 0)
            {
                errText = "�����˻����ʧ�ܣ�" + accountManager.Err;
                return false;
            }

            //�Ƿ��ӡƱ��
            bool isPrint = false;
            //�����˻����׼�¼
            accountRecord = this.GetAccountRecord();
            if (accountRecord == null)
            {
                errText = "��ȡ�˻���������ʧ�ܣ�";
                return false;
            }
            switch (validState)
            {
                //ͣ��
                case FS.HISFC.Models.Base.EnumValidState.Invalid:
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.StopAccount;
                        isPrint = true;
                        break;
                    }
                //����
                case FS.HISFC.Models.Base.EnumValidState.Valid:
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.AginAccount;
                        isPrint = true;
                        break;
                    }
                //ע��
                case FS.HISFC.Models.Base.EnumValidState.Ignore:
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.CancelAccount;
                        isPrint = true;
                        break;
                    }
            }
            accountRecord.BaseCost = 0;
            accountRecord.BaseVacancy = 0;
            accountRecord.ReMark = string.Empty;
            if (accountManager.InsertAccountRecord(accountRecord) < 0)
            {
                errText = "���뽻�׼�¼ʧ�ܣ�" + accountManager.Err;
                return false;
            }
            if (isPrint)
            {
                this.PrintAccountOperRecipe(accountRecord);
            }
            return true;
        }


        /// <summary>
        /// �����˻����
        /// </summary>
        /// <param name="money">���</param>
        /// <returns>true �ɹ� falseʧ��</returns>
        private bool UpdateAccountVacancy(decimal money, string reMark, OperTypes opertype, ref string errText)
        {
            //�����˻����
            if (accountManager.UpdateAccountVacancy(account.ID, money) <= 0)
            {
                errText = "�����˻����ʧ�ܣ�" + accountManager.Err;
                return false;
            }
            //����ʵ��
            accountRecord = this.GetAccountRecord();

            //�����˻���
            accountRecord.OperType.ID = (int)opertype;
            //�˷Ѳ��縺��
            accountRecord.BaseCost = -money;
            accountRecord.BaseVacancy = 0;
            accountRecord.ReMark = reMark;
            if (accountManager.InsertAccountRecord(accountRecord) < 0)
            {
                errText = "���ɽ��׼�¼ʧ�ܣ�" + accountManager.Err;
                return false;
            }
            //��ע���˻���ͣ�˻�ʱ��������ӡƱ��
            if (opertype == OperTypes.BalanceVacancy)
            {
                //�����˻�Ԥ������ʷ����״̬
                if (accountManager.UpdatePrePayHistory(account.ID, false, true) < 0)
                {
                    errText = "�����˻����ʧ�ܣ�" + accountManager.Err;
                    return false;
                }
                PrintCancelVacancyRecipe(accountRecord);
            }
            return true;
        }


        // //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// �����˻�����ж�
        /// </summary>
        /// <returns></returns>
        private bool ValidCancelVacancy(string cardNO)
        {
            if (string.IsNullOrEmpty(cardNO))
            {
                return false;
            }
            ArrayList al = outPatientManager.GetAccountNoFeeFeeItemList(account.CardNO);
            if (al == null)
            {
                MessageBox.Show("��ѯ����δ�շѵķ�����Ϣʧ�ܣ�" + outPatientManager.Err);
                return false;
            }
            if (al.Count > 0)
            {
                DialogResult diaResult = MessageBox.Show("����δ�շѵķ��ã��Ƿ���������˻���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (diaResult == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// ����δ�۷���,����������
        /// </summary>
        private void QueryUnFeeByCarNo()
        {
            if (this.accountCard == null || this.accountCard.Patient == null || string.IsNullOrEmpty(this.accountCard.Patient.PID.CardNO))
            {
                lblUnFeeMoney.Text = "";
                return;
            }
            decimal decUnFeeMoney = 0;
            decimal decVacancy = 0;
            string strMsg = "";
            this.account = this.accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}

            if (this.account != null)
            {
                decVacancy = this.account.BaseVacancy;
            }

            int iRes = feeIntegrate.QueryUnFeeByCarNo(this.accountCard.Patient.PID.CardNO, out decUnFeeMoney, out strMsg);
            if (iRes <= 0)
            {
                MessageBox.Show(strMsg);
            }
            if (decUnFeeMoney > 0)
            {
                lblUnFeeMoney.Text = decUnFeeMoney.ToString();
                if (decUnFeeMoney > decVacancy)
                    this.lblNeedPayMoney.Text = (decUnFeeMoney - decVacancy).ToString();
            }
            else
            {
                lblUnFeeMoney.Text = "";
                this.lblNeedPayMoney.Text = "";
            }
        }

        #region ��ӡ
        /// <summary>
        /// ��ӡ���˻�ƾ֤
        /// </summary>
        /// <param name="account"></param>
        private void PrintCreateAccountRecipe(HISFC.Models.Account.Account tempaccount)
        {
            if (!this.isPrintCreateBill)
            {
                return;
            }
            IPrintCreateAccount Iprint = FS.FrameWork.WinForms.Classes.
                UtilInterface.CreateObject(this.GetType(), typeof(IPrintCreateAccount)) as IPrintCreateAccount;
            if (Iprint == null)
            {
                MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                return;
            }
            account.AccountCard.Patient.IDCardType.Name = this.cmbIdCardType.Text;
            Iprint.SetValue(tempaccount);
            Iprint.Print();
        }

        /// <summary>
        /// ��ӡԤ����Ʊ��
        /// </summary>
        /// <param name="temprePay"></param>
        private void PrintPrePayRecipe(HISFC.Models.Account.PrePay temprePay)
        {
            if (!this.isPrintPrePayBill)
            {
                return;
            }
            if (Iprint == null)
            {
                Iprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<IPrintPrePayRecipe>(this.GetType());
                if (Iprint == null)
                {
                    MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                    return;
                }
            }
            Iprint.SetValue(temprePay);
            Iprint.Print();
        }
        /// <summary>
        /// ��ӡԤ����Ʊ��// {F69DC114-4EF6-4eb2-8018-0F7192139E27}
        /// </summary>
        /// <param name="prePay"></param>
        private void PrintPrePayRecipeEX(HISFC.Models.Account.PrePay prePay)
        {
            ucPrePayPrint ucPrePayPrint = new ucPrePayPrint();
            FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
            account = accountManager.GetAccountByCardNoEX(this.ucRegPatientInfo1.CardNO);
            
            HISFC.Models.Account.AccountRecord tempaccountRecord = new FS.HISFC.Models.Account.AccountRecord();
            decimal money = FS.FrameWork.Function.NConvert.ToDecimal(this.txtpay.Text);
            decimal donate = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDonate.Text);
            tempaccountRecord.BaseCost = money;
            tempaccountRecord.DonateCost = donate;
            account.AccountRecord.Add(tempaccountRecord);
            account.CardNO = this.ucRegPatientInfo1.CardNO;
            account.Name = this.ucRegPatientInfo1.patientInfo.Name;
            account.ID = prePay.InvoiceNO;
            account.User01 = prePay.PrePayOper.OperTime.ToString();
            ucPrePayPrint.PrintSetValue1(account);
            ucPrePayPrint.Print();
        }
        /// <summary>
        /// ��ӡ�������Ʊ��
        /// </summary>
        /// <param name="tempaccount"></param>
        private void PrintCancelVacancyRecipe(HISFC.Models.Account.AccountRecord tempaccountRecord)
        {
            tempaccountRecord.Memo = txtMarkNo.Text;
            if (!this.isPrintCancelVacancyBill)
            {
                return;
            }
            IPrintCancelVacancy Iprint = FS.FrameWork.WinForms.Classes.
             UtilInterface.CreateObject(this.GetType(), typeof(IPrintCancelVacancy)) as IPrintCancelVacancy;
            if (Iprint == null)
            {
                MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                return;
            }
            Iprint.SetValue(tempaccountRecord);
            Iprint.Print();
        }

        /// <summary>
        /// ��ӡ�˻�����Ʊ��
        /// </summary>
        /// <param name="tempaccountRecord"></param>
        private void PrintAccountOperRecipe(HISFC.Models.Account.AccountRecord tempaccountRecord)
        {
            if (!this.isPrintBusinessBill)
            {
                return;
            }
            IPrintOperRecipe Iprint = FS.FrameWork.WinForms.Classes.
            UtilInterface.CreateObject(this.GetType(), typeof(IPrintOperRecipe)) as IPrintOperRecipe;
            if (Iprint == null)
            {
                MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                return;
            }
            Iprint.SetValue(tempaccountRecord);
            Iprint.Print();
        }

        /// <summary>
        ///  ��ʼ���ӿ�
        /// </summary>
        private void InitInterface()
        {
            this.iAccountProcessPrepay = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.HISFC.BizProcess.Interface.Account.IAccountProcessPrepay)) as FS.HISFC.BizProcess.Interface.Account.IAccountProcessPrepay;
        }

        #endregion

        #endregion

        #region �¼�

        private void btnPay_Click(object sender, EventArgs e)
        {
            this.AccountPrePay();
        }

        private void txtpay_Enter(object sender, EventArgs e)
        {
            txtpay.SelectAll();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("�½��˻�", "�½��˻�", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolbarService.AddToolButton("�޸�����", "�޸�����", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolbarService.AddToolButton("ͣ���˻�", "ͣ���˻�", FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolbarService.AddToolButton("�����˻�", "�����˻�", FS.FrameWork.WinForms.Classes.EnumImageList.K����, true, false, null);
            toolbarService.AddToolButton("ע���˻�", "ע���˻�", FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
            toolbarService.AddToolButton("��ȡ", "��ȡԤ����", FS.FrameWork.WinForms.Classes.EnumImageList.Qȷ���շ�, true, false, null);
            toolbarService.AddToolButton("����ƾ֤", "����Ԥ����", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��, true, false, null);
            // ����˻�ȡ�ֹ���
            // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
            toolbarService.AddToolButton("ȡ��", "����ָ��Ԥ������", FS.FrameWork.WinForms.Classes.EnumImageList.T�˷�, true, false, null);
            //��Ӳ����ܣ����߷�Ʊ�ţ������븺��¼��ֻ�Ǽ򵥵Ĳ�ѯ��ӡ�����ش��ǲ帺��¼������һ����
            //{A6C8F37F-0E76-4dad-9FF6-0EAA518AA148}
            toolbarService.AddToolButton("����", "����Ԥ�����վ�", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            toolbarService.AddToolButton("�ش�", "�ش�Ԥ�����վ�", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            toolbarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolbarService.AddToolButton("�����˻�", "�����˻����", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolbarService.AddToolButton("Ƿ�Ѳ�ѯ", "��ѯӦ�����ã�", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            toolbarService.AddToolButton("ˢ��", "ˢ��", FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);


            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�޸�����":
                    {
                        EditPassword();
                        break;
                    }
                case "�½��˻�":
                    {
                        this.NewAccount();
                        break;
                    }
                case "ͣ���˻�":
                    {
                        StopAccount();
                        break;
                    }
                case "�����˻�":
                    {
                        AginAccount();
                        break;
                    }
                case "��ȡ":
                    {
                        AccountPrePay();
                        break;
                    }
                case "ע���˻�":
                    {
                        this.CancelAccount();
                        break;
                    }
                case "����ƾ֤":
                    {
                        this.AccountCancelPrePay();
                        break;
                    }
                case "�ش�":
                    {
                        this.ReprintInvoice();
                        break;
                    }
                case "����":
                    {
                        this.ReprintInvoiceNotInsert();
                        break;
                    }
                case "����":
                    {
                        this.Clear();
                        break;
                    }
                case "�����˻�":
                    {
                        this.BalanceVacancy();
                        break;
                    }
                case "ȡ��":
                    {
                        // �˻�ȡ��
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        AccountTaken();
                        break;
                    }
                case "Ƿ�Ѳ�ѯ":
                    {
                        // ��ѯǷ��

                        this.QueryUnFeeByCarNo();
                        break;
                    }

                case "ˢ��":
                    {
                        string McardNo = "";
                        string error = "";

                        if (Function.OperMCard(ref McardNo, ref error) < 0)
                        {
                            MessageBox.Show("����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���\n" + error);
                            return;
                        }
                        else
                        {
                            txtMarkNo.Text = ";" + McardNo;
                            txtMarkNo.Focus();
                            ExecCmdKey();
                        }

                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (FrameWork.Function.NConvert.ToBoolean(btnShow.Tag))
            {
                this.panelPatient.Visible = false;
                this.btnShow.Tag = false;
            }
            else
            {
                this.panelPatient.Visible = true;
                this.btnShow.Tag = true;
            }
        }

        private void ucAccount_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ExecCmdKey();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtpay_KeyDown(object sender, KeyEventArgs e)
        {
            //��֧������лس�
            //if (e.KeyData == Keys.Enter)
            //{
            //    if (this.txtpay.ContainsFocus)
            //    {
            //        this.AccountPrePay();
            //        return;
            //    }
            //}
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] vtype = new Type[5];
                vtype[0] = typeof(IPrintCreateAccount);
                vtype[1] = typeof(IPrintPrePayRecipe);
                vtype[2] = typeof(IPrintCancelVacancy);
                vtype[3] = typeof(IPrintOperRecipe);
                vtype[4] = typeof(FS.HISFC.BizProcess.Interface.Account.IAccountProcessPrepay);
                return vtype;
            }
        }

        #endregion

       

    }
}
