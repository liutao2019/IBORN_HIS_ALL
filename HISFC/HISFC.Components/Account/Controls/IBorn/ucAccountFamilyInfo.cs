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

        #region 变量
        /// <summary>
        /// 账户实体
        /// </summary>
        public FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
        /// <summary>
        /// 民族
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper NationHelp = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 证件类型
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper();

        
        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// 联系绑定人的基本信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private AccountCard linkAccounttCard = new AccountCard();
        /// <summary>
        /// 已绑定人的基本信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private AccountFamilyInfo linkedAccountFamilyInfo = new AccountFamilyInfo();
        
        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 费用综合业务层 
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        
        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 绑定人信息
        /// </summary>
        HISFC.Models.RADT.PatientInfo linkPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 被绑定人信息
        /// </summary>
        HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();


        /// <summary>
        ///患者基本信息
        /// </summary>
        public HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        string error = string.Empty;
        /// <summary>
        /// 入出转
        /// </summary>
        HISFC.BizProcess.Integrate.RADT radtInteger = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper markHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 预交金处理（优惠处理）
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Account.IAccountProcessPrepay iAccountProcessPrepay = null;

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();



        /// <summary>
        /// 是否自己根据规则生成发票号
        /// /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// </summary>
        protected bool isDefaultInvoiceNO = false;


        /// <summary>
        /// 查询账户时是否需要验证密码
        /// /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// </summary>
        protected bool isVerifyPSW = false;


        /// <summary>
        /// 切换查询类型ID
        /// </summary>
        private int switchID = 0;

        /// <summary>
        /// 所有的账户类型
        /// </summary>
        private ArrayList alAccountType = new ArrayList();
        #endregion


        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {

            this.rtxtOwnInfo.Text = patientInfo.Name + "  " + this.accountManager.GetAge(this.patientInfo.Birthday) + "   " + patientInfo.Sex.Name +
                "\n证件号：" + patientInfo.IDCard + "\n电话：" + patientInfo.PhoneHome + "\n家庭地址：" + patientInfo.AddressHome;
            this.lblFamilyInfo2.Text = "";
            this.account = this.accountManager.GetAccountByCardNoEX(this.patientInfo.PID.CardNO);
            this.GetAccountFamilyInfo();
            this.cmbLinkCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
            this.cmbLinkCardType.Text = "身份证";
            IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper(this.cmbLinkCardType.alItems);
            //关系

            this.cmbOwnRelation.AddItems(managerIntegrate.GetConstantList("FRelative"));

            this.cmbLinkRelation1.AddItems(managerIntegrate.GetConstantList("FRelative"));
            this.cmbLinkRelation2.AddItems(managerIntegrate.GetConstantList("FRelative"));
            //性别列表
            this.cmbLinkSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbLinkSex.Text = "男";//{9AE6E29C-1CAB-40b8-8F81-0F8379FEB8C9}

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
        /// 已绑定成员信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void GetAccountFamilyInfo()
        {
            List<AccountFamilyInfo> accountFamilyInfoList = new List<AccountFamilyInfo>();
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                this.lblFamilyInfo1.Text = "该患者没有绑定家庭";
                this.lblFamilyInfo1.ForeColor = Color.Red;
                this.cmbLinkRelation1.Enabled = true;
                this.btSave1.Visible = true;
                this.cmbLinkRelation1.Tag = "";
                return;
            }
            if (string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
            {
                MessageBox.Show("请输入绑定人的信息！" + this.accountManager.Err);
                return;
            }
            if (this.accountManager.GetFamilyInfoByCode(this.patientInfo.FamilyCode, "1", out accountFamilyInfoList) <= 0)
            {
                MessageBox.Show("获取家庭成员信息失败！" + this.accountManager.Err);
                accountFamilyInfoList = null;
                return;
            }
            if (accountFamilyInfoList == null)
            {
                MessageBox.Show("获取家庭成员信息失败！" + this.accountManager.Err);
                return;
            }
            if (this.patientInfo != null)
            {
                this.SetPatientFamilyInfo();
            }

        }
       


        /// <summary>
        /// 清除绑定人的信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void ClearLinkInfo()
        {
            this.linkedAccountFamilyInfo = new AccountFamilyInfo();
            this.linkPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.linkAccounttCard = new AccountCard();
            this.txtLinkName.Text = string.Empty;
            this.txtLinkName.Tag = string.Empty;
            this.cmbLinkSex.Tag = "M";
            this.cmbLinkSex.Text = "男";
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
                this.lblFamilyInfo1.Text = "该患者没有绑定家庭！";
            }
        }

        /// <summary>
        /// 解除成员关系// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void UndateFamilyInfo()
        {
            if (this.linkedAccountFamilyInfo == null||string.IsNullOrEmpty(this.linkedAccountFamilyInfo.ID))
            {
                MessageBox.Show("请双击选择解除绑定的家庭成员信息！");
                return;
            }
            if (this.linkedAccountFamilyInfo.LinkedCardNO == this.patientInfo.PID.CardNO)
            {
            }
            if (MessageBox.Show("是否退出该家庭？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
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
                MessageBox.Show("解除关系失败！"+this.accountManager.Err);
                return;
            }
            if (!string.IsNullOrEmpty(this.linkedAccountFamilyInfo.LinkedCardNO))
            {
                if (this.accountManager.UpdatePatientFamilyCode(this.linkedAccountFamilyInfo.LinkedCardNO, "", "") <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新家庭号失败！" + this.accountManager.Err);
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            if (this.linkedAccountFamilyInfo.LinkedCardNO == this.patientInfo.PID.CardNO)
            {
                this.patientInfo.FamilyCode = "";
                this.patientInfo.FamilyName = "";

            }
            MessageBox.Show("解除成功！" );

            if (this.sheetView1.Rows.Count > 0)
            {
                this.sheetView1.Rows.Remove(0, this.sheetView1.Rows.Count);
            }
            this.GetAccountFamilyInfo();
            linkedAccountFamilyInfo = new AccountFamilyInfo();//避免重复操作
            //this.ClearLinkInfo();
        }
        /// <summary>
        /// 新建成员信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void SaveFamilyInfo(string relationCode, string relationName,int index)
        {
            if (string.IsNullOrEmpty(this.txtLinkName.Text))
            {
                MessageBox.Show("请输入成员信息！");
                return;
            }

            if (index == 1 && string.IsNullOrEmpty(relationCode))
            {
                this.cmbLinkRelation1.Focus();
                MessageBox.Show("请选择家庭成员的角色！");
                return;
            }

            if (index == 2 && string.IsNullOrEmpty(relationCode))
            {
                this.cmbLinkRelation2.Focus();
                MessageBox.Show("请选择家庭成员的角色！");
                return;
            }
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
            {

                MessageBox.Show("请输入需要绑定人的信息！");
                return;

            }

            if (string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode) 
                && string.IsNullOrEmpty(this.patientInfo.FamilyCode)
                && !string.IsNullOrEmpty(this.linkPatientInfo.PID.CardNO))//联系人和被联系人的信息不为空，但家庭都为空提示
            {
                MessageBox.Show("请先新建家庭！");
                return;
            }
            else if ( string.IsNullOrEmpty(this.patientInfo.FamilyCode)
                && string.IsNullOrEmpty(this.linkPatientInfo.PID.CardNO))//联系人的信息为空，被联系人信息不为空，但被联系人的家庭为空提示
            {
                MessageBox.Show("请先新建家庭！");
                return;
            }
            if (!string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode) && !string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                MessageBox.Show("该成员已在" + linkPatientInfo.FamilyName);
                return;// {0304EC3C-ECA4-4b90-8040-5EBEC93F2EA5}
            }
            FS.HISFC.Models.Account.Account linkAccount = new FS.HISFC.Models.Account.Account();

            if (!string.IsNullOrEmpty(linkAccounttCard.Patient.PID.CardNO))
            {
                //List<AccountFamilyInfo> accountFamilyInfo2List = new List<AccountFamilyInfo>();

                //if (this.accountManager.GetLinkedFamilyInfo(linkAccounttCard.Patient.PID.CardNO, "1", out accountFamilyInfo2List) <= 0)
                //{
                //    MessageBox.Show("获取成员信息失败！");
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
                //        MessageBox.Show("获取信息失败！");
                //        return;
                //    }
                //    MessageBox.Show("该成员已在" + patientInfo.FamilyName);
                //    return;
                //}

                linkAccount = this.accountManager.GetAccountByCardNoEX(linkAccounttCard.Patient.PID.CardNO);

                if (linkAccount == null)
                {
                    if (MessageBox.Show("该成员没有办理账户，是否继续操作？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
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
            //如果联系人的家庭号不为空，被联系人的家庭号为空就插入被联系人的信息
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
            //如果联系人的家庭号为空，被联系人的家庭号不为空就插入联系人的信息，联系人变为被联系人
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
                    MessageBox.Show("插入成员信息失败！" + this.accountManager.Err);
                    return;
                }

                if (!string.IsNullOrEmpty(linkAccounttCard.Patient.PID.CardNO))
                {
                    if (this.accountManager.UpdatePatientFamilyCode(accountFamilyInfo.LinkedCardNO, accountFamilyInfo.FamilyCode, accountFamilyInfo.FamilyName) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新联系人的家庭号失败！" + this.accountManager.Err);
                        return;
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.GetAccountFamilyInfo();//刷新列表
                this.ClearLinkInfo();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("新建成员信息失败！" + e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// 显示关联人的患者信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountCardList"></param>
        private void SetPatientsInfoToFp(List<AccountCard> accountCardList)
        {
            this.neuTabControl1.SelectedIndex = 0;
            this.sheetView2.Rows.Count = 0;
            if (accountCardList == null)
            {
                MessageBox.Show("获取患者基本信息失败！");
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
        /// 显示被联系人的家庭成员信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountFamilyInfoList"></param>
        private void SetPatientFamilyInfoToFp(List<AccountFamilyInfo> accountFamilyInfoList)
        {
            this.neuTabControl1.SelectedIndex = 1;
            this.sheetView1.Rows.Count = 0;
            if (accountFamilyInfoList == null)
            {
                MessageBox.Show("获取已绑定成员信息失败！");
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
        /// 显示联系人的家庭成员信息// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653} lfhm
        /// </summary>
        /// <param name="accountFamilyInfoList"></param>
        private void SetLinkedPatientFamilyInfoToFp(List<AccountFamilyInfo> accountFamilyInfoList)
        {
            this.sheetView3.Rows.Count = 0;
            if (accountFamilyInfoList == null)
            {
                MessageBox.Show("获取已绑定成员信息失败！");
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
        /// 显示预交金信息
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
                    sheet.Cells[count, 1].Text = "收取";
                }
                else
                {
                    if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                    {
                        sheet.Cells[count, 1].Text = "返还";

                    }
                    else if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                    {
                        sheet.Cells[count, 1].Text = "结清余额";
                    }
                    else
                    {
                        sheet.Cells[count, 1].Text = "收取";
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
                //sheet.Cells[count, 5].Text = prepay.PayType.ID == "CA" ? "现金" : "银行卡";
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
        /// 输入就诊卡号获取联系人基本信息
        /// </summary>
        private void GetLinkedPatientInfo()
        {

            this.ClearInfo();
            FS.HISFC.Models.Account.AccountCard accountCard1 = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtInput.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("请输入就诊卡号！");
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

                MessageBox.Show("查找不到患者信息!");
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
        /// 设置联系人的绑定信息显示
        /// </summary>
        public void SetLinkFamilyInfo()// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653}
        {
            if (!string.IsNullOrEmpty(linkPatientInfo.FamilyCode))
            {
                this.lblFamilyInfo2.Text = "家庭名称：" + linkPatientInfo.FamilyName;
                this.lblFamilyInfo2.ForeColor = Color.Blue;
                List<AccountFamilyInfo> accountFamilyInfoList = new List<AccountFamilyInfo>();

                if (this.accountManager.GetFamilyInfoByCode(this.linkPatientInfo.FamilyCode, "1", out accountFamilyInfoList) <= 0)
                {
                    MessageBox.Show("获取家庭成员信息失败！" + this.accountManager.Err);
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
                    this.lblFamilyInfo1.Text = "该患者没有绑定家庭,是否绑定到" + this.linkPatientInfo.FamilyName;
                    this.lblFamilyInfo1.ForeColor = Color.Red;
                    this.cmbLinkRelation1.Tag = "";
                    this.btSave1.Visible = true;
                    this.cmbLinkRelation1.Enabled = true;
                }
                this.SetLinkedPatientFamilyInfoToFp(accountFamilyInfoList);

            }
            else if (!string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                this.lblFamilyInfo2.Text = "该患者没有绑定家庭,是否绑定到" + this.patientInfo.FamilyName;
                this.lblFamilyInfo2.ForeColor = Color.Red;
                this.cmbLinkRelation2.Tag = "";
                this.btSave2.Visible = true;
                this.cmbLinkRelation2.Enabled = true;
            }
            else
            {
                this.lblFamilyInfo2.Text = "该患者没有绑定家庭！";
                this.lblFamilyInfo2.ForeColor = Color.Red;
                this.cmbLinkRelation2.Tag = "";
                this.btSave2.Visible = true;
                this.cmbLinkRelation2.Enabled = true;
            }

        }

        /// <summary>
        /// 设置被联系人的绑定信息显示
        /// </summary>
        public void SetPatientFamilyInfo()// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653}
        {
            if (!string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                this.lblFamilyInfo1.Text = "家庭名称：" + patientInfo.FamilyName;
                this.lblFamilyInfo1.ForeColor = Color.Blue;
                List<AccountFamilyInfo> accountFamilyInfoList = new List<AccountFamilyInfo>();

                if (this.accountManager.GetFamilyInfoByCode(this.patientInfo.FamilyCode, "1", out accountFamilyInfoList) <= 0)
                {
                    MessageBox.Show("获取家庭成员信息失败！" + this.accountManager.Err);
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
                    this.lblFamilyInfo2.Text = "该患者没有绑定家庭,是否绑定到" + this.patientInfo.FamilyName;
                    this.lblFamilyInfo2.ForeColor = Color.Red;
                    this.cmbLinkRelation2.Tag = "";
                    this.btSave2.Visible = true;
                    this.cmbLinkRelation2.Enabled = true;
                }
                this.SetPatientFamilyInfoToFp(accountFamilyInfoList);

            }
            else if (!string.IsNullOrEmpty(this.linkPatientInfo.FamilyCode))
            {
                this.lblFamilyInfo2.Text = "该患者没有绑定家庭,是否绑定到" + this.linkPatientInfo.FamilyName;
                this.lblFamilyInfo2.ForeColor = Color.Red;
                this.cmbLinkRelation2.Tag = "";
                this.btSave2.Visible = true;
                this.cmbLinkRelation2.Enabled = true;
            }
            else
            {
                this.lblFamilyInfo1.Text = "该患者没有绑定家庭！";
                this.lblFamilyInfo1.ForeColor = Color.Red;
                this.cmbLinkRelation1.Tag = "";
                this.btSave1.Visible = true;
                this.cmbLinkRelation1.Enabled = true;
            }


        }
        /// <summary>
        /// 根据条件查询患者信息
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
                MessageBox.Show("请输入" + this.lbInputName.Text);
                this.txtInput.Focus();
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查找患者信息，请稍后...");
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
        /// 回车处理
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
                MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

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
            //在支付方式中回车
            //if (this.cmbPayType.Focused)
            //{
            //    if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            //    {
            //        MessageBox.Show("请选择支付方式！", "提示");
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
        /// 根据ID获得名称0:民族 1:证件类型
        /// </summary>
        /// <param name="ID">民族ID</param>
        /// <param name="aMod">0:民族 1:证件类型</param>
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



        #region 事件


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

        #region IInterfaceContainer 成员

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
        /// 根据年龄算生日
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
                MessageBox.Show("年龄输入过大请重新输入！");
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
            return string.Format("{0}岁{1}月{2}天", year <= 0 ? "___" : year.ToString().PadLeft(3, '_'), year <= 0 && month <= 0 ? "__" : month.ToString().PadLeft(2, '_'), day.ToString().PadLeft(2, '_'));
        }

        public void GetAgeNumber(string age, ref int year, ref int month, ref int day)
        {
            year = 0;
            month = 0;
            day = 0;
            age = age.Replace("_", "");
            int ageIndex = age.IndexOf("岁");
            int monthIndex = age.IndexOf("月");
            int dayIndex = age.IndexOf("天");

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
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, monthIndex));//只有月
            }
            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
            if (ageIndex < 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, dayIndex));//只有日
            }
            if (ageIndex >= 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, dayIndex - ageIndex - 1));//只有年，日
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
                        this.lbInputName.Text = "就诊卡号：";
                        this.lbInputName.Tag = this.switchID;
                        break;
                    }
                case 1:
                    {
                        this.lbInputName.Text = "姓    名：";
                        this.lbInputName.Tag = this.switchID;
                        break;
                    }
                case 2:
                    {
                        this.lbInputName.Text = "证件号码：";
                        this.lbInputName.Tag = this.switchID;
                        break;
                    }
                case 3:
                    {
                        this.lbInputName.Text = "电话号码：";
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
        /// 清除绑定人的信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        private void ClearInfo()
        {
            this.linkedAccountFamilyInfo = new AccountFamilyInfo();
            this.linkAccounttCard = new AccountCard();
            this.txtLinkName.Text = string.Empty;
            this.txtLinkName.Tag = string.Empty;
            this.cmbLinkSex.Tag = "M";
            this.cmbLinkSex.Text = "男";
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
                this.lblFamilyInfo1.Text = "该患者没有绑定家庭！";
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
                        MessageBox.Show("日期输入错误！请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("日期输入错误！请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("日期输入错误！" + ex.Message, "警告", MessageBoxButtons.OK);
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
                MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

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
                MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

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
                MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

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
                        iMonth = dt.AddMonths(-1).Month + iMonth;//用当前的月份减
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
                MessageBox.Show("刷卡失败!" + error);// {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}
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
                MessageBox.Show("请选择家庭角色!");
                return;
            }
            if (!string.IsNullOrEmpty(this.patientInfo.FamilyCode))
            {
                MessageBox.Show("该患者已存在家庭，不允许重复建立!");
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
                patientFamilyInfo.FamilyName = this.patientInfo.Name + "家庭";
                this.patientInfo.FamilyName = patientFamilyInfo.FamilyName;
                this.patientInfo.FamilyCode = patientFamilyInfo.FamilyCode;// {0304EC3C-ECA4-4b90-8040-5EBEC93F2EA5}


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (patientFamilyInfo != null)
                {
                    if (this.accountManager.InsertAccountFamilyInfo(patientFamilyInfo) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入成员信息失败！" + this.accountManager.Err);
                        return;
                    }

                }

                if (this.accountManager.UpdatePatientFamilyCode(this.patientInfo.PID.CardNO, this.patientInfo.FamilyCode,this.patientInfo.FamilyName) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新家庭号失败！" + this.accountManager.Err);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("新建成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.GetAccountFamilyInfo();//刷新列表
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
