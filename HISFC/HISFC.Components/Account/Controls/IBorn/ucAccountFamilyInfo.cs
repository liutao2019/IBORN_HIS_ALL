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
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    public partial class ucAccountFamilyInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAccountFamilyInfo()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// �˻�ʵ��
        /// </summary>
        public FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper NationHelp = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ֤������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper();

        
        /// <summary>
        /// �˻�ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// ��ϵ���˵Ļ�����Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private AccountCard linkAccounttCard = new AccountCard();
        /// <summary>
        /// �Ѱ��˵Ļ�����Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private AccountFamilyInfo linkedAccountFamilyInfo = new AccountFamilyInfo();
        
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
        /// ������Ϣ
        /// </summary>
        HISFC.Models.RADT.PatientInfo linkPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();


        /// <summary>
        ///���߻�����Ϣ
        /// </summary>
        public HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        }

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
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();



        /// <summary>
        /// �Ƿ��Լ����ݹ������ɷ�Ʊ��
        /// /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// </summary>
        protected bool isDefaultInvoiceNO = false;


        /// <summary>
        /// ��ѯ�˻�ʱ�Ƿ���Ҫ��֤����
        /// /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// </summary>
        protected bool isVerifyPSW = false;


        /// <summary>
        /// �л���ѯ����ID
        /// </summary>
        private int switchID = 0;

        /// <summary>
        /// ���е��˻�����
        /// </summary>
        private ArrayList alAccountType = new ArrayList();
        #endregion


        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {

            this.rtxtOwnInfo.Text = patientInfo.Name + "  " + this.accountManager.GetAge(this.patientInfo.Birthday) + "   " + patientInfo.Sex.Name +
                "\n֤���ţ�" + patientInfo.IDCard + "\n�绰��" + patientInfo.PhoneHome + "\n��ͥ��ַ��" + patientInfo.AddressHome;
            this.lblFamilyInfo2.Text = "";
            this.account = this.accountManager.GetAccountByCardNoEX(this.patientInfo.PID.CardNO);
            this.GetAccountFamilyInfo();
            this.cmbLinkCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
            this.cmbLinkCardType.Text = "���֤";
            IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper(this.cmbLinkCardType.alItems);
            //��ϵ

            this.cmbOwnRelation.AddItems(managerIntegrate.GetConstantList("FRelative"));

            this.cmbLinkRelation1.AddItems(managerIntegrate.GetConstantList("FRelative"));
            this.cmbLinkRelation2.AddItems(managerIntegrate.GetConstantList("FRelative"));
            //�Ա��б�
            this.cmbLinkSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbLinkSex.Text = "��";//{9AE6E29C-1CAB-40b8-8F81-0F8379FEB8C9}

            this.txtYear.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            if (txtYear.Text.Trim() == "")
            {
                this.txtMonth.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged -= new EventHandler(txtBirthDay_TextChanged);

                this.txtMonth.Text = "01"; //DateTime.Now.Month.ToString();
                this.txtDays.Text = "01"; //DateTime.Now.Day.ToString();

                this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);

                this.txtYear.Text = DateTime.Now.Year.ToString();

            }
        }
        /// <summary>
        /// �Ѱ󶨳�Ա��Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void GetAccountFamilyInfo()
        {
            List<AccountFamilyInfo> accountFamilyInfoList = new List<AccountFamilyInfo>();
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                this.lblFamilyInfo1.Text = "�û���û�а󶨼�ͥ";
                this.lblFamilyInfo1.ForeColor = Color.Red;
                this.cmbLinkRelation1.Enabled = true;
                this.btSave1.Visible = true;
                this.cmbLinkRelation1.Tag = "";
                return;
            }
            if (string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
            {
                MessageBox.Show("��������˵���Ϣ��" + this.accountManager.Err);
                return;
            }
            if (this.accountManager.GetFamilyInfoByCode(this.patientInfo.FamilyCode, "1", out accountFamilyInfoList) <= 0)
            {
                MessageBox.Show("��ȡ��ͥ��Ա��Ϣʧ�ܣ�" + this.accountManager.Err);
                accountFamilyInfoList = null;
                return;
            }
            if (accountFamilyInfoList == null)
            {
                MessageBox.Show("��ȡ��ͥ��Ա��Ϣʧ�ܣ�" + this.accountManager.Err);
                return;
            }
            if (this.patientInfo != null)
            {
                this.SetPatientFamilyInfo();
            }

        }
       


        /// <summary>
        /// ������˵���Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void ClearLinkInfo()
        {
            this.linkedAccountFamilyInfo = new AccountFamilyInfo();
            this.linkPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.linkAccounttCard = new AccountCard();
            this.txtLinkName.Text = string.Empty;
            this.txtLinkName.Tag = string.Empty;
            this.cmbLinkSex.Tag = "M";
            this.cmbLinkSex.Text = "��";
            this.cmbLinkCardType.Tag = "01";
            this.txtLinkIDCardNo.Text = string.Empty;
            //this.txtInput.Text = string.Empty;
            //this.cmbOwnRelation.Tag = "";
            //this.cmbOwnRelation.Text = "";
            this.cmbLinkRelation2.Tag = "";
            this.cmbLinkRelation2.Text = "";
            this.txtLinkPhone.Text = string.Empty;
            this.txtLinkHome.Text = string.Empty;
            this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();
            this.lblFamilyInfo2.Text = "";
            this.cmbLinkRelation2.Enabled = true;
            this.btSave2.Visible = true;
            if (this.sheetView3.Rows.Count > 0)
            {
                this.sheetView3.Rows.Remove(0, this.sheetView3.Rows.Count);
            }
            if (this.sheetView2.Rows.Count > 0)
            {
                this.sheetView2.Rows.Remove(0, this.sheetView2.Rows.Count);
            }

            if (string.IsNullOrEmpty(this.patientInfo.FamilyName))
            {
                this.lblFamilyInfo1.Text = "�û���û�а󶨼�ͥ��";
            }
        }

        /// <summary>
        /// �����Ա��ϵ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void UndateFamilyInfo()
        {
            if (this.linkedAccountFamilyInfo == null||string.IsNullOrEmpty(this.linkedAccountFamilyInfo.ID))
            {
                MessageBox.Show("��˫��ѡ�����󶨵ļ�ͥ��Ա��Ϣ��");
                return;
            }
            if (this.linkedAccountFamilyInfo.LinkedCardNO == this.patientInfo.PID.CardNO)
            {
            }
            if (MessageBox.Show("�Ƿ��˳��ü�ͥ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
            string operCode = this.accountManager.Operator.ID;
            DateTime date = this.accountManager.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.accountManager.UpdateAccountFamilyInfoState(this.linkedAccountFamilyInfo.ID, "0", operCode, date.ToString()) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�����ϵʧ�ܣ�"+this.accountManager.Err);
                return;
            }
            if (!string.IsNullOrEmpty(this.linkedAccountFamilyInfo.LinkedCardNO))
            {
                if (this.accountManager.UpdatePatientFamilyCode(this.linkedAccountFamilyInfo.LinkedCardNO, "", "") <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���¼�ͥ��ʧ�ܣ�" + this.accountManager.Err);
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            if (this.linkedAccountFamilyInfo.LinkedCardNO == this.patientInfo.PID.CardNO)
            {
                this.patientInfo.FamilyCode = "";
                this.patientInfo.FamilyName = "";

            }
            MessageBox.Show("����ɹ���" );

            if (this.sheetView1.Rows.Count > 0)
            {
                this.sheetView1.Rows.Remove(0, this.sheetView1.Rows.Count);
            }
            this.GetAccountFamilyInfo();
            linkedAccountFamilyInfo = new AccountFamilyInfo();//�����ظ�����
            //this.ClearLinkInfo();
        }
        /// <summary>
        /// �½���Ա��Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void SaveFamilyInfo(string relationCode, string relationName,int index)
        {
            if (string.IsNullOrEmpty(this.txtLinkName.Text))
            {
                MessageBox.Show("�������Ա��Ϣ��");
                return;
            }

            if (index == 1 && string.IsNullOrEmpty(relationCode))
            {
                this.cmbLinkRelation1.Focus();
                MessageBox.Show("��ѡ���ͥ��Ա�Ľ�ɫ��");
                return;
            }

            if (index == 2 && string.IsNullOrEmpty(relationCode))
            {
                this.cmbLinkRelation2.Focus();
                MessageBox.Show("��ѡ���ͥ��Ա�Ľ�ɫ��");
                return;
            }
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
            {

                MessageBox.Show("��������Ҫ���˵���Ϣ��");
                return;

            }

            if (string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode) 
                && string.IsNullOrEmpty(this.patientInfo.FamilyCode)
                && !string.IsNullOrEmpty(this.linkPatientInfo.PID.CardNO))//��ϵ�˺ͱ���ϵ�˵���Ϣ��Ϊ�գ�����ͥ��Ϊ����ʾ
            {
                MessageBox.Show("�����½���ͥ��");
                return;
            }
            else if ( string.IsNullOrEmpty(this.patientInfo.FamilyCode)
                && string.IsNullOrEmpty(this.linkPatientInfo.PID.CardNO))//��ϵ�˵���ϢΪ�գ�����ϵ����Ϣ��Ϊ�գ�������ϵ�˵ļ�ͥΪ����ʾ
            {
                MessageBox.Show("�����½���ͥ��");
                return;
            }
            if (!string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode) && !string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                MessageBox.Show("�ó�Ա����" + linkPatientInfo.FamilyName);
                return;// {0304EC3C-ECA4-4b90-8040-5EBEC93F2EA5}
            }
            FS.HISFC.Models.Account.Account linkAccount = new FS.HISFC.Models.Account.Account();

            if (!string.IsNullOrEmpty(linkAccounttCard.Patient.PID.CardNO))
            {
                //List<AccountFamilyInfo> accountFamilyInfo2List = new List<AccountFamilyInfo>();

                //if (this.accountManager.GetLinkedFamilyInfo(linkAccounttCard.Patient.PID.CardNO, "1", out accountFamilyInfo2List) <= 0)
                //{
                //    MessageBox.Show("��ȡ��Ա��Ϣʧ�ܣ�");
                //    return;
                //}
                //if (accountFamilyInfo2List.Count > 0)
                //{
                //    AccountFamilyInfo obj = new AccountFamilyInfo();
                //    obj = accountFamilyInfo2List[0] as AccountFamilyInfo;
                //    HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //    patientInfo = managerIntegrate.QueryComPatientInfo(obj.CardNO);
                //    if (patientInfo == null)
                //    {
                //        MessageBox.Show("��ȡ��Ϣʧ�ܣ�");
                //        return;
                //    }
                //    MessageBox.Show("�ó�Ա����" + patientInfo.FamilyName);
                //    return;
                //}

                linkAccount = this.accountManager.GetAccountByCardNoEX(linkAccounttCard.Patient.PID.CardNO);

                if (linkAccount == null)
                {
                    if (MessageBox.Show("�ó�Աû�а����˻����Ƿ����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        linkAccount = new FS.HISFC.Models.Account.Account();
                    }
                }
            }

            string operCode = accountManager.Operator.ID; ;
            DateTime currTime = accountManager.GetDateTimeFromSysDateTime();

            AccountFamilyInfo accountFamilyInfo = new AccountFamilyInfo();
            //�����ϵ�˵ļ�ͥ�Ų�Ϊ�գ�����ϵ�˵ļ�ͥ��Ϊ�վͲ��뱻��ϵ�˵���Ϣ
            if (string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode) && !string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                accountFamilyInfo.LinkedCardNO = linkAccounttCard.Patient.PID.CardNO;
                accountFamilyInfo.LinkedAccountNo = linkAccount.ID;
                accountFamilyInfo.Name = this.txtLinkName.Text;
                accountFamilyInfo.Relation.ID = relationCode;
                accountFamilyInfo.Relation.Name = relationName;
                accountFamilyInfo.Sex.ID = this.cmbLinkSex.Tag.ToString();
                accountFamilyInfo.Sex.Name = this.cmbLinkSex.Text;
                accountFamilyInfo.Birthday = this.dtpBirthDay.Value;
                accountFamilyInfo.CardType.ID = this.cmbLinkCardType.Tag.ToString();
                accountFamilyInfo.CardType.Name = this.cmbLinkCardType.Text;
                accountFamilyInfo.IDCardNo = this.txtLinkIDCardNo.Text;
                accountFamilyInfo.Phone = this.txtLinkPhone.Text;
                accountFamilyInfo.Address = this.txtLinkHome.Text;
                accountFamilyInfo.ValidState = EnumValidState.Valid;
                accountFamilyInfo.CardNO = this.patientInfo.PID.CardNO;
                if (this.account != null)
                {
                    accountFamilyInfo.AccountNo = this.account.ID;
                }
                accountFamilyInfo.CreateEnvironment.ID = operCode;
                accountFamilyInfo.CreateEnvironment.OperTime = currTime;
                accountFamilyInfo.OperEnvironment.ID = operCode;
                accountFamilyInfo.OperEnvironment.OperTime = currTime;
                accountFamilyInfo.FamilyCode = this.patientInfo.FamilyCode;
                accountFamilyInfo.FamilyName = this.patientInfo.FamilyName;
            }
            //�����ϵ�˵ļ�ͥ��Ϊ�գ�����ϵ�˵ļ�ͥ�Ų�Ϊ�վͲ�����ϵ�˵���Ϣ����ϵ�˱�Ϊ����ϵ��
            // {0304EC3C-ECA4-4b90-8040-5EBEC93F2EA5}
            else if (!string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode) && string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {

                accountFamilyInfo.LinkedCardNO = this.patientInfo.PID.CardNO;

                if (this.account != null)
                {
                    accountFamilyInfo.LinkedAccountNo = this.account.ID;
                }
                if (linkAccount != null)
                {
                    accountFamilyInfo.AccountNo = linkAccount.ID;
                }
                accountFamilyInfo.CardNO = linkPatientInfo.PID.CardNO;
                accountFamilyInfo.Name = this.patientInfo.Name;
                accountFamilyInfo.LinkedAccountNo = linkAccount.ID;
                accountFamilyInfo.Relation.ID = relationCode;
                accountFamilyInfo.Relation.Name = relationName;
                accountFamilyInfo.Sex.ID = this.patientInfo.Sex.ID.ToString();
                accountFamilyInfo.Sex.Name = this.patientInfo.Sex.Name;
                accountFamilyInfo.Birthday = this.patientInfo.Birthday;
                accountFamilyInfo.CardType.ID = this.patientInfo.IDCardType.ID;
                accountFamilyInfo.CardType.Name = this.patientInfo.IDCardType.Name;
                accountFamilyInfo.IDCardNo = this.patientInfo.IDCard;
                accountFamilyInfo.Phone = this.patientInfo.PhoneHome;
                accountFamilyInfo.Address = this.patientInfo.AddressHome;
                accountFamilyInfo.ValidState = EnumValidState.Valid;
                accountFamilyInfo.CreateEnvironment.ID = operCode;
                accountFamilyInfo.CreateEnvironment.OperTime = currTime;
                accountFamilyInfo.OperEnvironment.ID = operCode;
                accountFamilyInfo.OperEnvironment.OperTime = currTime;
                accountFamilyInfo.FamilyCode = this.linkPatientInfo.FamilyCode;
                accountFamilyInfo.FamilyName = this.linkPatientInfo.FamilyName;
                this.patientInfo.FamilyCode = this.linkPatientInfo.FamilyCode;
                this.patientInfo.FamilyName = this.linkPatientInfo.FamilyName;


            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                if (this.accountManager.InsertAccountFamilyInfo(accountFamilyInfo) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����Ա��Ϣʧ�ܣ�" + this.accountManager.Err);
                    return;
                }

                if (!string.IsNullOrEmpty(linkAccounttCard.Patient.PID.CardNO))
                {
                    if (this.accountManager.UpdatePatientFamilyCode(accountFamilyInfo.LinkedCardNO, accountFamilyInfo.FamilyCode, accountFamilyInfo.FamilyName) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������ϵ�˵ļ�ͥ��ʧ�ܣ�" + this.accountManager.Err);
                        return;
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.GetAccountFamilyInfo();//ˢ���б�
                this.ClearLinkInfo();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�½���Ա��Ϣʧ�ܣ�" + e.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// ��ʾ�����˵Ļ�����Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountCardList"></param>
        private void SetPatientsInfoToFp(List<AccountCard> accountCardList)
        {
            this.neuTabControl1.SelectedIndex = 0;
            this.sheetView2.Rows.Count = 0;
            if (accountCardList == null)
            {
                MessageBox.Show("��ȡ���߻�����Ϣʧ�ܣ�");
                return;
            }
            if (accountCardList.Count <= 0)
            {
                return;
            }

            int count = 0;
            this.sheetView2.Rows.Count = accountCardList.Count;

            foreach (AccountCard accountCard2 in accountCardList)
            {
                this.sheetView2.Cells[count, 0].Text = accountCard2.Patient.PID.CardNO;
                this.sheetView2.Cells[count, 1].Text = accountCard2.Patient.Name;
                this.sheetView2.Cells[count, 2].Text = accountCard2.Patient.Sex.Name;
                this.sheetView2.Cells[count, 3].Text = this.accountManager.GetAge(accountCard2.Patient.Birthday);
                this.sheetView2.Cells[count, 4].Text = this.GetName(accountCard2.Patient.IDCardType.ID, 1);
                this.sheetView2.Cells[count, 5].Text = accountCard2.Patient.IDCard;
                string telephone = "";
                if (accountCard2.Patient.PhoneHome != null && accountCard2.Patient.PhoneHome != "")
                {
                    telephone = accountCard2.Patient.PhoneHome;
                }
                else if (accountCard2.Patient.Kin.RelationPhone != null && accountCard2.Patient.Kin.RelationPhone != "")
                {
                    telephone = accountCard2.Patient.Kin.RelationPhone;
                }
                else
                {
                    telephone = "";
                }
                this.sheetView2.Cells[count, 6].Text = telephone;
                accountCard2.Patient.PhoneHome = telephone;
                this.sheetView2.Cells[count, 7].Text = accountCard2.Patient.AddressHome;
                this.sheetView2.Rows[count].Tag = accountCard2;
                count++;
            }

        }
        /// <summary>
        /// ��ʾ����ϵ�˵ļ�ͥ��Ա��Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountFamilyInfoList"></param>
        private void SetPatientFamilyInfoToFp(List<AccountFamilyInfo> accountFamilyInfoList)
        {
            this.neuTabControl1.SelectedIndex = 1;
            this.sheetView1.Rows.Count = 0;
            if (accountFamilyInfoList == null)
            {
                MessageBox.Show("��ȡ�Ѱ󶨳�Ա��Ϣʧ�ܣ�");
                return;
            }
            if (accountFamilyInfoList.Count <= 0)
            {
                return;
            }

            int count = 0;
            this.sheetView1.Rows.Count = accountFamilyInfoList.Count;

            foreach (AccountFamilyInfo accountFamilyInfo in accountFamilyInfoList)
            {
                this.sheetView1.Cells[count, 0].Text = accountFamilyInfo.FamilyName;
                this.sheetView1.Cells[count, 1].Text = accountFamilyInfo.Relation.Name;
                this.sheetView1.Cells[count, 2].Text = accountFamilyInfo.LinkedCardNO;
                this.sheetView1.Cells[count, 3].Text = accountFamilyInfo.Name;
                this.sheetView1.Cells[count, 4].Text = accountFamilyInfo.Sex.Name;
                this.sheetView1.Cells[count, 5].Text = this.accountManager.GetAge(accountFamilyInfo.Birthday);
                this.sheetView1.Cells[count, 6].Text = accountFamilyInfo.CardType.Name;
                this.sheetView1.Cells[count, 7].Text = accountFamilyInfo.IDCardNo;
                this.sheetView1.Cells[count, 8].Text = accountFamilyInfo.Phone;
                this.sheetView1.Cells[count, 9].Text = accountFamilyInfo.Address;
                this.sheetView1.Rows[count].Tag = accountFamilyInfo;
                count++;
            }

        }

        /// <summary>
        /// ��ʾ��ϵ�˵ļ�ͥ��Ա��Ϣ// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653} lfhm
        /// </summary>
        /// <param name="accountFamilyInfoList"></param>
        private void SetLinkedPatientFamilyInfoToFp(List<AccountFamilyInfo> accountFamilyInfoList)
        {
            this.sheetView3.Rows.Count = 0;
            if (accountFamilyInfoList == null)
            {
                MessageBox.Show("��ȡ�Ѱ󶨳�Ա��Ϣʧ�ܣ�");
                return;
            }
            if (accountFamilyInfoList.Count <= 0)
            {
                return;
            }

            int count = 0;
            this.sheetView3.Rows.Count = accountFamilyInfoList.Count;

            foreach (AccountFamilyInfo accountFamilyInfo in accountFamilyInfoList)
            {
                this.sheetView3.Cells[count, 0].Text = accountFamilyInfo.FamilyName;
                this.sheetView3.Cells[count, 1].Text = accountFamilyInfo.Relation.Name;
                this.sheetView3.Cells[count, 2].Text = accountFamilyInfo.LinkedCardNO;
                this.sheetView3.Cells[count, 3].Text = accountFamilyInfo.Name;
                this.sheetView3.Cells[count, 4].Text = accountFamilyInfo.Sex.Name;
                this.sheetView3.Cells[count, 5].Text = this.accountManager.GetAge(accountFamilyInfo.Birthday);
                this.sheetView3.Cells[count, 6].Text = accountFamilyInfo.CardType.Name;
                this.sheetView3.Cells[count, 7].Text = accountFamilyInfo.IDCardNo;
                this.sheetView3.Cells[count, 8].Text = accountFamilyInfo.Phone;
                this.sheetView3.Cells[count, 9].Text = accountFamilyInfo.Address;
                this.sheetView3.Rows[count].Tag = accountFamilyInfo;
                count++;
            }

        }
        /// <summary>
        /// ��ʾԤ������Ϣ
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sheet"></param>
        private void SetFp(List<PrePay> list,FarPoint.Win.Spread.SheetView sheet)
        {
            //sheet.Rows.Add(count, 1);
            int count = 0;
            foreach (PrePay prepay in list)
            {
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
                if (prepay.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
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
                count++;
            }
        }


        /// <summary>
        /// ������￨�Ż�ȡ��ϵ�˻�����Ϣ
        /// </summary>
        private void GetLinkedPatientInfo()
        {

            this.ClearInfo();
            FS.HISFC.Models.Account.AccountCard accountCard1 = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtInput.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("��������￨�ţ�");
                return;
            }
            int resultValue = accountManager.GetCardByRule(markNO, ref accountCard1);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                return;
            }
            if (accountCard1 == null || string.IsNullOrEmpty(accountCard1.Patient.PID.CardNO))
            {

                MessageBox.Show("���Ҳ���������Ϣ!");
                return;
            }
            this.txtInput.Text = accountCard1.MarkNO;
            linkPatientInfo = managerIntegrate.QueryComPatientInfo(accountCard1.Patient.PID.CardNO);
            this.txtLinkName.Tag = "0";
            this.txtLinkName.Text = linkPatientInfo.Name;
            this.cmbLinkSex.Tag = linkPatientInfo.Sex.ID;
            this.cmbLinkSex.Text = linkPatientInfo.Sex.Name;
            this.cmbLinkCardType.Tag = linkPatientInfo.IDCardType.ID;
            this.txtLinkIDCardNo.Text = linkPatientInfo.IDCard;
            this.txtLinkPhone.Text = linkPatientInfo.PhoneHome;
            this.txtLinkHome.Text = linkPatientInfo.AddressHome;
            this.dtpBirthDay.Value = linkPatientInfo.Birthday;
            accountCard1.Patient = linkPatientInfo;
            linkAccounttCard = new AccountCard();
            linkAccounttCard = accountCard1;
            List<AccountCard> accountCardList = new List<AccountCard>();
            accountCardList.Add(accountCard1);
            if (linkPatientInfo != null)
            {
                this.SetLinkFamilyInfo();// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653}
            }
            else
            {
                this.lblFamilyInfo2.Text = "";
            }

            this.SetPatientsInfoToFp(accountCardList);
            
        }
        /// <summary>
        /// ������ϵ�˵İ���Ϣ��ʾ
        /// </summary>
        public void SetLinkFamilyInfo()// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653}
        {
            if (!string.IsNullOrEmpty(linkPatientInfo.FamilyCode))
            {
                this.lblFamilyInfo2.Text = "��ͥ���ƣ�" + linkPatientInfo.FamilyName;
                this.lblFamilyInfo2.ForeColor = Color.Blue;
                List<AccountFamilyInfo> accountFamilyInfoList = new List<AccountFamilyInfo>();

                if (this.accountManager.GetFamilyInfoByCode(this.linkPatientInfo.FamilyCode, "1", out accountFamilyInfoList) <= 0)
                {
                    MessageBox.Show("��ȡ��ͥ��Ա��Ϣʧ�ܣ�" + this.accountManager.Err);
                    accountFamilyInfoList = null;
                    return;
                }
                foreach (AccountFamilyInfo obj in accountFamilyInfoList)
                {
                    if (obj.LinkedCardNO == this.linkPatientInfo.PID.CardNO)
                    {
                        this.cmbLinkRelation2.Tag = obj.Relation.ID;
                        this.cmbLinkRelation2.Text = obj.Relation.Name;
                        this.cmbLinkRelation2.Enabled = false;
                        this.btSave2.Visible = false;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(this.patientInfo.FamilyCode))
                {
                    this.lblFamilyInfo1.Text = "�û���û�а󶨼�ͥ,�Ƿ�󶨵�" + this.linkPatientInfo.FamilyName;
                    this.lblFamilyInfo1.ForeColor = Color.Red;
                    this.cmbLinkRelation1.Tag = "";
                    this.btSave1.Visible = true;
                    this.cmbLinkRelation1.Enabled = true;
                }
                this.SetLinkedPatientFamilyInfoToFp(accountFamilyInfoList);

            }
            else if (!string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                this.lblFamilyInfo2.Text = "�û���û�а󶨼�ͥ,�Ƿ�󶨵�" + this.patientInfo.FamilyName;
                this.lblFamilyInfo2.ForeColor = Color.Red;
                this.cmbLinkRelation2.Tag = "";
                this.btSave2.Visible = true;
                this.cmbLinkRelation2.Enabled = true;
            }
            else
            {
                this.lblFamilyInfo2.Text = "�û���û�а󶨼�ͥ��";
                this.lblFamilyInfo2.ForeColor = Color.Red;
                this.cmbLinkRelation2.Tag = "";
                this.btSave2.Visible = true;
                this.cmbLinkRelation2.Enabled = true;
            }

        }

        /// <summary>
        /// ���ñ���ϵ�˵İ���Ϣ��ʾ
        /// </summary>
        public void SetPatientFamilyInfo()// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653}
        {
            if (!string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                this.lblFamilyInfo1.Text = "��ͥ���ƣ�" + patientInfo.FamilyName;
                this.lblFamilyInfo1.ForeColor = Color.Blue;
                List<AccountFamilyInfo> accountFamilyInfoList = new List<AccountFamilyInfo>();

                if (this.accountManager.GetFamilyInfoByCode(this.patientInfo.FamilyCode, "1", out accountFamilyInfoList) <= 0)
                {
                    MessageBox.Show("��ȡ��ͥ��Ա��Ϣʧ�ܣ�" + this.accountManager.Err);
                    accountFamilyInfoList = null;
                    return;
                }
                foreach (AccountFamilyInfo obj in accountFamilyInfoList)
                {
                    if (obj.LinkedCardNO == this.patientInfo.PID.CardNO)
                    {
                        this.cmbLinkRelation1.Tag = obj.Relation.ID;
                        this.cmbLinkRelation1.Text = obj.Relation.Name;
                        this.cmbLinkRelation1.Enabled = false;
                        this.btSave1.Visible = false;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode))
                {
                    this.lblFamilyInfo2.Text = "�û���û�а󶨼�ͥ,�Ƿ�󶨵�" + this.patientInfo.FamilyName;
                    this.lblFamilyInfo2.ForeColor = Color.Red;
                    this.cmbLinkRelation2.Tag = "";
                    this.btSave2.Visible = true;
                    this.cmbLinkRelation2.Enabled = true;
                }
                this.SetPatientFamilyInfoToFp(accountFamilyInfoList);

            }
            else if (!string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode))
            {
                this.lblFamilyInfo2.Text = "�û���û�а󶨼�ͥ,�Ƿ�󶨵�" + this.linkPatientInfo.FamilyName;
                this.lblFamilyInfo2.ForeColor = Color.Red;
                this.cmbLinkRelation2.Tag = "";
                this.btSave2.Visible = true;
                this.cmbLinkRelation2.Enabled = true;
            }
            else
            {
                this.lblFamilyInfo1.Text = "�û���û�а󶨼�ͥ��";
                this.lblFamilyInfo1.ForeColor = Color.Red;
                this.cmbLinkRelation1.Tag = "";
                this.btSave1.Visible = true;
                this.cmbLinkRelation1.Enabled = true;
            }


        }
        /// <summary>
        /// ����������ѯ������Ϣ
        /// </summary>
        /// <param name="patientName"></param>
        /// <param name="sexId"></param>
        /// <param name="pactId"></param>
        /// <param name="CassNo"></param>
        /// <param name="idCardType"></param>
        /// <param name="idCard"></param>
        /// <param name="SSN"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        protected virtual int QueryPatientInfo(string patientName, string sexId, string pactId, string CassNo, string idCardType, string idCard, string SSN,string phone,string cardNo)
        {
            if (string.IsNullOrEmpty(patientName) && string.IsNullOrEmpty(idCard) && string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("������" + this.lbInputName.Text);
                this.txtInput.Focus();
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ��һ�����Ϣ�����Ժ�...");
            Application.DoEvents();

            List<AccountCard> list = accountManager.GetAccountCardEX(patientName, sexId, pactId, CassNo, idCardType, idCard, SSN, phone,cardNo);

            if (list == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(accountManager.Err);
                return -1;
            }

            this.SetPatientsInfoToFp(list);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }
        /// <summary>
        /// �س�����
        /// </summary>
        protected virtual void ExecCmdKey() 
        {
            if (this.txtInput.Focused)
            {
                this.ClearLinkInfo();
                if (this.switchID == 0)
                {
                    this.GetLinkedPatientInfo();
                    this.txtInput.Focus();
                }
                else
                {
                    this.btSelect_Click(null, null);
                    this.txtInput.Focus();
                }
                return;
            }
            try
            {
                if (this.txtYear.Focused)
                {
                    this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                    this.txtMonth.Focus();
                    return;
                }
                else if (this.txtMonth.Focused)
                {
                    this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                    this.txtDays.Focus();
                    return;
                }
                else if (this.txtDays.Focused)
                {
                    this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                    this.txtLinkAge.Focus();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("����ǰ��������ڸ�ʽ�������������룡");

                if (this.txtMonth.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";

                    this.txtMonth.SelectAll();
                }

                if (this.txtDays.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";

                    this.txtDays.SelectAll();
                }


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
        /// ����ID�������0:���� 1:֤������
        /// </summary>
        /// <param name="ID">����ID</param>
        /// <param name="aMod">0:���� 1:֤������</param>
        /// <returns></returns>
        public string GetName(string ID, int aMod)
        {
            if (aMod == 0)
            {
                return NationHelp.GetName(ID);
            }
            else
            {
                return IdCardTypeHelp.GetName(ID);
            }
        }



        #region �¼�


        private void ucAccountIBorn_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ExecCmdKey();
            }
            else if (keyData == Keys.F1)
            {
                this.lbSwitch_Click(null, null);
            }
            return base.ProcessDialogKey(keyData);
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
        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="isUpdateAgeText"></param>
        private void ConvertBirthdayByAge(bool isUpdateAgeText)
        {
            DateTime birthDay = this.accountManager.GetDateTimeFromSysDateTime();
            if (birthDay == null || birthDay < new DateTime(1700, 1, 1))
            {
                return;
            }
            string ageStr = this.txtLinkAge.Text.Trim();
            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;
            this.GetAgeNumber(ageStr, ref iyear, ref iMonth, ref iDay);

            //birthDay = birthDay.AddDays(-iDay).AddMonths(-iMonth).AddYears(-iyear);
            int year = birthDay.Year - iyear;
            int m = birthDay.Month - iMonth;
            if (m <= 0)
            {
                if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month + m;
                }
            }

            int day = birthDay.Day - iDay;
            if (day <= 0)
            {
                if (m > 0)
                {
                    m = m - 1;
                    DateTime dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }
                else if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month - 1;
                    dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }

                if (m <= 0)
                {
                    if (year > 0)
                    {
                        year = year - 1;
                        DateTime dt = new DateTime(year, 1, 1);
                        m = dt.AddYears(1).AddDays(-1).Month + m;
                    }
                }
            }
            else
            {
                DateTime dt = new DateTime(year, m, 1);
                if (day > dt.AddMonths(1).AddDays(-1).Day)
                {
                    day = dt.AddMonths(1).AddDays(-1).Day;
                }
            }

            birthDay = new DateTime(year, m, day);

            if (birthDay < dtpBirthDay.MinDate || birthDay > dtpBirthDay.MaxDate)
            {
                MessageBox.Show("��������������������룡");
                this.txtLinkAge.Text = this.GetAge(0, 0, 0);
                return;
            }
            if (isUpdateAgeText)
            {
                this.txtLinkAge.TextChanged -= new EventHandler(txtLinkAge_TextChanged);
                this.txtLinkAge.Text = this.GetAge(iyear, iMonth, iDay);
                this.dtpBirthDay.Value = birthDay;
                this.txtLinkAge.TextChanged += new EventHandler(txtLinkAge_TextChanged);
            }
            else
            {
                this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
                this.dtpBirthDay.Value = birthDay;
                this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);

                this.txtYear.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                this.txtMonth.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                this.txtYear.Text = this.dtpBirthDay.Value.Year.ToString();
                this.txtMonth.Text = this.dtpBirthDay.Value.Month.ToString();
                this.txtDays.Text = this.dtpBirthDay.Value.Day.ToString();
                this.txtYear.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            }
        }

        public string GetAge(int year, int month, int day)
        {
            return string.Format("{0}��{1}��{2}��", year <= 0 ? "___" : year.ToString().PadLeft(3, '_'), year <= 0 && month <= 0 ? "__" : month.ToString().PadLeft(2, '_'), day.ToString().PadLeft(2, '_'));
        }

        public void GetAgeNumber(string age, ref int year, ref int month, ref int day)
        {
            year = 0;
            month = 0;
            day = 0;
            age = age.Replace("_", "");
            int ageIndex = age.IndexOf("��");
            int monthIndex = age.IndexOf("��");
            int dayIndex = age.IndexOf("��");

            if (ageIndex > 0)
            {
                year = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, ageIndex));
            }
            if (ageIndex >= 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, monthIndex - ageIndex - 1));
            }
            if (ageIndex < 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, monthIndex));//ֻ����
            }
            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
            if (ageIndex < 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, dayIndex));//ֻ����
            }
            if (ageIndex >= 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, dayIndex - ageIndex - 1));//ֻ���꣬��
            }
        }

        private void lbSwitch_Click(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            if (this.switchID == 3)
            {
                this.switchID = 0;
            }
            else
            {
                this.switchID = this.switchID + 1;
            }
            switch (this.switchID)
            {
                case 0:
                    {
                        this.lbInputName.Text = "���￨�ţ�";
                        this.lbInputName.Tag = this.switchID;
                        break;
                    }
                case 1:
                    {
                        this.lbInputName.Text = "��    ����";
                        this.lbInputName.Tag = this.switchID;
                        break;
                    }
                case 2:
                    {
                        this.lbInputName.Text = "֤�����룺";
                        this.lbInputName.Tag = this.switchID;
                        break;
                    }
                case 3:
                    {
                        this.lbInputName.Text = "�绰���룺";
                        this.lbInputName.Tag = this.switchID;
                        break;
                    }
            }

        }

        private void btSelect_Click(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            this.ClearLinkInfo();
            string inputText = this.txtInput.Text.Trim();
            if (this.switchID == 0)
            {
                this.GetLinkedPatientInfo();
            }
            else if (this.switchID == 1)
            {
                QueryPatientInfo(inputText, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,"");
            }
            else if (this.switchID == 2)
            {
                QueryPatientInfo(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, inputText, string.Empty, string.Empty,"");
            }
            else if (this.switchID == 3)
            {
                QueryPatientInfo(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, inputText,"");
            }
        }

        private void btSave_Click(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            string relationCode = this.cmbLinkRelation1.Tag.ToString();
            string relationName = this.cmbLinkRelation1.Text;
            this.SaveFamilyInfo(relationCode, relationName,1);
        }

        private void neuSpread3_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {

            this.ClearInfo();
            if (this.neuSpread3.ActiveSheet != this.sheetView2)
            {
                return;
            }

            if (this.neuSpread3.ActiveSheet == this.sheetView2 && this.sheetView2.ActiveRow == null)
            {
                return;
            }

            if (e.ColumnHeader || this.sheetView2.RowCount == 0) return;
            int row = e.Row;
            if (this.sheetView2.RowCount > 0)
            {
                if (this.sheetView2.Rows[row].Tag != null)
                {
                    linkAccounttCard = new AccountCard();
                    linkAccounttCard = this.sheetView2.Rows[row].Tag as AccountCard;

                    this.txtLinkName.Tag = "0";
                    this.txtLinkName.Text = linkAccounttCard.Patient.Name;
                    this.cmbLinkSex.Tag = linkAccounttCard.Patient.Sex.ID;
                    this.cmbLinkSex.Text = linkAccounttCard.Patient.Sex.Name;
                    this.cmbLinkCardType.Tag = linkAccounttCard.Patient.IDCardType.ID;
                    this.txtLinkIDCardNo.Text = linkAccounttCard.Patient.IDCard;
                    this.txtLinkPhone.Text = linkAccounttCard.Patient.PhoneHome;
                    this.txtLinkHome.Text = linkAccounttCard.Patient.AddressHome;
                    this.dtpBirthDay.Value = linkAccounttCard.Patient.Birthday;
                }
            }

            linkPatientInfo = managerIntegrate.QueryComPatientInfo(linkAccounttCard.Patient.PID.CardNO);
            if (this.linkPatientInfo != null)
            {
                this.SetLinkFamilyInfo();
            }
            
        }


        /// <summary>
        /// ������˵���Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void ClearInfo()
        {
            this.linkedAccountFamilyInfo = new AccountFamilyInfo();
            this.linkAccounttCard = new AccountCard();
            this.txtLinkName.Text = string.Empty;
            this.txtLinkName.Tag = string.Empty;
            this.cmbLinkSex.Tag = "M";
            this.cmbLinkSex.Text = "��";
            this.cmbLinkCardType.Tag = "01";
            this.txtLinkIDCardNo.Text = string.Empty;
            this.cmbOwnRelation.Tag = "";
            this.cmbOwnRelation.Text = "";
            this.cmbLinkRelation2.Tag = "";
            this.cmbLinkRelation2.Text = "";
            this.txtLinkPhone.Text = string.Empty;
            this.txtLinkHome.Text = string.Empty;
            this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();
            this.lblFamilyInfo2.Text = "";
            if (string.IsNullOrEmpty(this.patientInfo.FamilyName))
            {
                this.lblFamilyInfo1.Text = "�û���û�а󶨼�ͥ��";
            }
        }
        private void txtBirthDay_TextChanged(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            try
            {
                if (sender == this.txtYear)
                {
                    if (this.txtYear.Text.Length == "2011".Length)
                    {
                        this.txtMonth.Focus();
                        this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                    }
                }
                else if (sender == this.txtMonth)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(txtMonth.Text))
                        {
                            if (this.txtMonth.Text.Length == "12".Length)
                            {
                                this.txtDays.Focus();
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                            else if (txtMonth.Text.Length > 1
                                && FS.FrameWork.Function.NConvert.ToInt32(txtMonth.Text.Substring(0, 1)) > 1)
                            {
                                txtDays.Focus();
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("��������������������룡", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (sender == this.txtDays)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(txtMonth.Text))
                        {
                            if (this.txtDays.Text.Length == "12".Length)
                            {
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                            else if (txtDays.Text.Length > 1
                                && FS.FrameWork.Function.NConvert.ToInt32(txtDays.Text.Substring(0, 1)) > 3)
                            {
                                txtDays.Focus();
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("��������������������룡", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("�����������" + ex.Message, "����", MessageBoxButtons.OK);
                if (sender == this.txtYear)
                {
                    this.txtYear.Focus();
                }
                else if (sender == this.txtMonth)
                {
                    this.txtMonth.Focus();
                }
                else
                {
                    this.txtDays.Focus();
                }
            }
        }

        private void txtYear_Leave(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            try
            {
                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                this.txtMonth.Focus();
            }
            catch
            {
                MessageBox.Show("����ǰ��������ڸ�ʽ�������������룡");

                if (this.txtMonth.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtMonth.SelectAll();
                }

                if (this.txtDays.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtDays.SelectAll();
                }
            }
        }// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        private void txtMonth_Leave(object sender, EventArgs e)
        {
            try
            {
                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                this.txtDays.Focus();
            }
            catch
            {
                MessageBox.Show("����ǰ��������ڸ�ʽ�������������룡");

                if (this.txtMonth.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtMonth.SelectAll();
                }

                if (this.txtDays.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtDays.SelectAll();
                }
            }

        }
        private void txtDays_Leave(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            try
            {
                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                this.txtLinkAge.Focus();
            }
            catch
            {
                MessageBox.Show("����ǰ��������ڸ�ʽ�������������룡");

                if (this.txtMonth.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtMonth.SelectAll();
                }

                if (this.txtDays.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtDays.SelectAll();
                }
            }
        }
        private void txtLinkAge_TextChanged(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            this.ConvertBirthdayByAge(false);
        }

        private void txtLinkAge_Leave(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            this.ConvertBirthdayByAge(true);
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            if (this.dtpBirthDay.Value == null || this.dtpBirthDay.Value < new DateTime(1700, 1, 1))
            {
                return;
            }
            // string age = this.accountManager.GetAge(this.dtpBirthDay.Value, true);
            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;

            DateTime sysdate = this.accountManager.GetDateTimeFromSysDateTime();
            if (sysdate > this.dtpBirthDay.Value)
            {
                iyear = sysdate.Year - this.dtpBirthDay.Value.Year;
                if (iyear < 0)
                {
                    iyear = 0;
                }
                iMonth = sysdate.Month - this.dtpBirthDay.Value.Month;
                if (iMonth < 0)
                {
                    if (iyear > 0)
                    {
                        iyear = iyear - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month + iMonth;//�õ�ǰ���·ݼ�
                    }

                    if (iMonth < 0)
                    {
                        iMonth = 0;
                    }
                }
                iDay = sysdate.Day - this.dtpBirthDay.Value.Day;
                if (iDay < 0)
                {
                    if (iMonth > 0)
                    {
                        iMonth = iMonth - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year, this.dtpBirthDay.Value.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else if (iyear > 0)
                    {
                        iyear = iyear - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month - 1;
                        dt = new DateTime(this.dtpBirthDay.Value.Year, this.dtpBirthDay.Value.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else
                    {
                        iDay = 0;
                    }
                }
            }


            // this.GetAgeNumber(age, ref iyear, ref iMonth, ref iDay);
            this.txtLinkAge.TextChanged -= new EventHandler(txtLinkAge_TextChanged);
            this.txtLinkAge.Text = this.GetAge(iyear, iMonth, iDay);
            this.txtLinkAge.TextChanged += new EventHandler(txtLinkAge_TextChanged);

            this.txtYear.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
            this.txtMonth.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
            this.txtDays.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
            this.txtYear.Text = this.dtpBirthDay.Value.Year.ToString();
            this.txtMonth.Text = this.dtpBirthDay.Value.Month.ToString();
            this.txtDays.Text = this.dtpBirthDay.Value.Day.ToString();
            this.txtYear.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);

            //this.cmbAgeUnit.SelectedIndexChanged -=new EventHandler(cmbAgeUnit_SelectedIndexChanged);
            //this.cmbAgeUnit.Text = age.Substring(age.Length - 1, 1);
            //this.cmbAgeUnit.SelectedIndexChanged += new EventHandler(cmbAgeUnit_SelectedIndexChanged);
        }

        private void btUnfirndYou_Click(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            this.UndateFamilyInfo();
        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {

            this.ClearLinkInfo();
            this.linkAccounttCard = new AccountCard();
            if (this.neuSpread2.ActiveSheet != this.sheetView1)
            {
                return;
            }

            if (this.neuSpread2.ActiveSheet == this.sheetView1 && this.sheetView1.ActiveRow == null)
            {
                return;
            }

            if (e.ColumnHeader || this.sheetView1.RowCount == 0) return;
            int row = e.Row;
            if (this.sheetView1.RowCount > 0)
            {
                if (this.sheetView1.Rows[row].Tag != null)
                {
                    linkedAccountFamilyInfo = new AccountFamilyInfo();
                    linkedAccountFamilyInfo = this.sheetView1.Rows[row].Tag as AccountFamilyInfo;

                    this.UndateFamilyInfo();
                    //this.txtLinkName.Tag = "1";
                    //this.txtLinkName.Text = linkedAccountFamilyInfo.Name;
                    //this.cmbLinkSex.Tag = linkedAccountFamilyInfo.Sex.ID;
                    //this.cmbLinkSex.Text = linkedAccountFamilyInfo.Sex.Name;
                    //this.cmbLinkCardType.Tag = linkedAccountFamilyInfo.CardType.ID;
                    //this.cmbLinkCardType.Text = linkedAccountFamilyInfo.CardType.Name;
                    //this.txtLinkIDCardNo.Text = linkedAccountFamilyInfo.IDCardNo;
                    //this.txtLinkPhone.Text = linkedAccountFamilyInfo.Phone;
                    //this.txtLinkHome.Text = linkedAccountFamilyInfo.Address;
                    //this.dtpBirthDay.Value = linkedAccountFamilyInfo.Birthday;
                    //this.cmbRelation.Tag = linkedAccountFamilyInfo.Relation.ID;
                }
            }

        }

        private void btClear_Click(object sender, EventArgs e)// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        {
            this.ClearLinkInfo();
        }

        private void btShuaKa_Click(object sender, EventArgs e)
        {
            this.txtInput.Focus();

            string mCardNo = "";
            if (Function.OperMCard(ref mCardNo, ref error) == -1)
            {
                MessageBox.Show("ˢ��ʧ��!" + error);// {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}
                return;
            }
            txtInput.SelectAll();
            txtInput.Text = ";"+mCardNo;
            txtInput.Focus();
            ExecCmdKey();
        }

        private void btNewFamily_Click(object sender, EventArgs e)
        {
            this.ClearLinkInfo();
            if (string.IsNullOrEmpty(this.cmbOwnRelation.Tag.ToString()) || string.IsNullOrEmpty(this.cmbOwnRelation.Text))
            {
                MessageBox.Show("��ѡ���ͥ��ɫ!");
                return;
            }
            if (!string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                MessageBox.Show("�û����Ѵ��ڼ�ͥ���������ظ�����!");
                return;
            }
            if (this.patientInfo != null)
            {

                string operCode = accountManager.Operator.ID; ;
                DateTime currTime = accountManager.GetDateTimeFromSysDateTime();
                AccountFamilyInfo patientFamilyInfo = null;
                string sql = "select SEQ_ACCOUNTFAMILYID.NEXTVAL from dual";
                string familyCode = this.accountManager.ExecSqlReturnOne(sql);
                this.patientInfo.FamilyCode = familyCode;
                patientFamilyInfo = new AccountFamilyInfo();
                patientFamilyInfo.LinkedCardNO = this.patientInfo.PID.CardNO;

                patientFamilyInfo.CardNO = this.patientInfo.PID.CardNO;

                if (this.account != null)
                {
                    patientFamilyInfo.LinkedAccountNo = this.account.ID;
                }
                patientFamilyInfo.Name = this.patientInfo.Name;
                patientFamilyInfo.Relation.ID = this.cmbOwnRelation.Tag.ToString();
                patientFamilyInfo.Relation.Name = this.cmbOwnRelation.Text;
                patientFamilyInfo.Sex.ID = this.patientInfo.Sex.ID.ToString();
                patientFamilyInfo.Sex.Name = this.patientInfo.Sex.Name;
                patientFamilyInfo.Birthday = this.patientInfo.Birthday;
                patientFamilyInfo.CardType.ID = this.patientInfo.IDCardType.ID;
                patientFamilyInfo.CardType.Name = this.patientInfo.IDCardType.Name;
                patientFamilyInfo.IDCardNo = this.patientInfo.IDCard;
                patientFamilyInfo.Phone = this.patientInfo.PhoneHome;
                patientFamilyInfo.Address = this.patientInfo.AddressHome;
                patientFamilyInfo.ValidState = EnumValidState.Valid;
                patientFamilyInfo.CreateEnvironment.ID = operCode;
                patientFamilyInfo.CreateEnvironment.OperTime = currTime;
                patientFamilyInfo.OperEnvironment.ID = operCode;
                patientFamilyInfo.OperEnvironment.OperTime = currTime;
                patientFamilyInfo.FamilyCode = this.patientInfo.FamilyCode;
                patientFamilyInfo.FamilyName = this.patientInfo.Name + "��ͥ";
                this.patientInfo.FamilyName = patientFamilyInfo.FamilyName;
                this.patientInfo.FamilyCode = patientFamilyInfo.FamilyCode;// {0304EC3C-ECA4-4b90-8040-5EBEC93F2EA5}


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (patientFamilyInfo != null)
                {
                    if (this.accountManager.InsertAccountFamilyInfo(patientFamilyInfo) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����Ա��Ϣʧ�ܣ�" + this.accountManager.Err);
                        return;
                    }

                }

                if (this.accountManager.UpdatePatientFamilyCode(this.patientInfo.PID.CardNO, this.patientInfo.FamilyCode,this.patientInfo.FamilyName) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���¼�ͥ��ʧ�ܣ�" + this.accountManager.Err);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�½��ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.GetAccountFamilyInfo();//ˢ���б�
            }
        }

        private void btSave2_Click(object sender, EventArgs e)
        {
            string relationCode = this.cmbLinkRelation2.Tag.ToString();
            string relationName = this.cmbLinkRelation2.Text;
            this.SaveFamilyInfo(relationCode, relationName,2);

        }



    }

}
