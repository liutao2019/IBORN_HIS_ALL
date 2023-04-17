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

        #region 变量
        /// <summary>
        /// 账户实体
        /// </summary>
        private FS.HISFC.Models.Account.Account account = null;

        /// <summary>
        /// 卡操作实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
        
        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// 账户交易实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountRecord accountRecord = null;
        
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
        /// 门诊卡号
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;

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
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 办帐户时，是否需要验证有效证件；0-需要，1-不需要
        /// </summary>
        protected string JudgeCredentialCreating = "0";

        /// <summary>
        /// 办账户时是否自动默认密码
        /// {9AE6E29C-1CAB-40b8-8F81-0F8379FEB8C9}
        /// </summary>
        protected bool IsDefaultPassword = true;

        /// <summary>
        /// 是否自己根据规则生成发票号
        /// /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// </summary>
        protected bool isDefaultInvoiceNO = false;

        /// <summary>
        /// 默认银行取现方式(统计到报表什么项目中) add by yerl
        /// </summary>
        private string bankPayType = "";
        /// <summary>
        /// 预约金打印实体
        /// </summary>
        private IPrintPrePayRecipe Iprint = null;
        #endregion

        #region 属性

        #region 打印单据

        /// <summary>
        /// 是否打印创建账户凭证
        /// </summary>
        private bool isPrintCreateBill = false;

        /// <summary>
        /// 是否打印创建账户凭证
        /// </summary>
        [Category("打印设置"), Description("是否打印创建账户凭证")]
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
        /// 是否打印退费凭证
        /// </summary>
        private bool isPrintCancelVacancyBill = false;

        /// <summary>
        /// 是否打印退费凭证
        /// </summary>
        [Category("打印设置"), Description("是否打印退费凭证")]
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
        /// 是否打印缴费凭证
        /// </summary>
        private bool isPrintPrePayBill = false;

        /// <summary>
        /// 是否打印缴费凭证
        /// </summary>
        [Category("打印设置"), Description("是否打印缴费凭证")]
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
        /// 是否打印业务操作凭证
        /// </summary>
        private bool isPrintBusinessBill = false;

        /// <summary>
        /// 是否打印业务操作凭证
        /// </summary>
        [Category("打印设置"), Description("是否打印业务操作凭证,注销、停用、启用、改密码")]
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
        /// 是否开启 取现 套现控制
        /// </summary>
        [Category("控件设置"), Description("是否自己根据规则生成发票号")]
        public bool IsDefaultInvoiceNO
        {
            get { return isDefaultInvoiceNO; }
            set { isDefaultInvoiceNO = value; }
        }
        /// <summary>
        /// 银行卡取现支付方式
        /// </summary>
        [Category("控件设置"), Description("银行卡取现支付方式 默认CH")]
        public string BankPayType { get { return bankPayType; } set { bankPayType = value; } }

        /// <summary>
        /// 是否开启 取现 套现控制
        /// </summary>
        [Category("控件设置"), Description("是否开启 取现 套现控制")]
        public bool BlnIsLeverageControl
        {
            get { return blnIsLeverageControl; }
            set { blnIsLeverageControl = value; }
        }
        /// <summary>
        /// 是否开启 取现 套现控制
        /// </summary>
        bool blnIsLeverageControl = false;

        /// <summary>
        /// 充值提醒限额，默认 1000
        /// </summary>
        [Category("控件设置"), Description("充值提醒限额")]
        public decimal DecLimitePerpayMoney
        {
            get { return decLimitePerpayMoney; }
            set { decLimitePerpayMoney = value; }
        }
        /// <summary>
        /// 充值提醒限额，默认 1000
        /// </summary>
        decimal decLimitePerpayMoney = 1000;



        /// <summary>
        /// 保存后是否自动清屏
        /// </summary>
        bool isSaveClear = false;
        [Category("控件设置"), Description("保存后是否自动清屏")]
        public bool IsSaveClear
        {
            get { return isSaveClear; }
            set { isSaveClear = value; }
        }

        [Description("是否显示就诊卡列表"), Category("设置")]
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

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //填充卡类型
            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            this.cmbCardType.AddItems(al);
            markHelper.ArrayObject = al;
            //证件类型
            this.cmbIdCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
            this.panelPatient.Visible = false;
            this.btnShow.Tag = this.panelPatient.Visible;
            this.ActiveControl = this.txtMarkNo;
            //初始化接口
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
        /// 查找账户信息
        /// </summary>
        private  void GetAccountByMark()
        {  
            //检查账户信息
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


                //起用状态
                if (this.account.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    
                    SetControlState(1);
                }
                //停用状态
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
        /// 显示患者基本信息
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
        /// 根据账户状态设置控件状态
        ///<param name="aMod">0:未建立账户或以前账户已经注销 1:账户启用状态 2:账户停用状态</param>
        /// </summary>
        private void SetControlState(int aMod)
        {
            switch (aMod)
            {
                case 0:
                    {
                        this.toolbarService.SetToolButtonEnabled("新建账户", true);
                        this.toolbarService.SetToolButtonEnabled("收取", false);
                        this.toolbarService.SetToolButtonEnabled("返还", false);
                        // 添加账户取现功能
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        this.toolbarService.SetToolButtonEnabled("取现", false);
                        this.toolbarService.SetToolButtonEnabled("补打", false);
                        this.toolbarService.SetToolButtonEnabled("停用账户", false);
                        this.toolbarService.SetToolButtonEnabled("启用账户", false);
                        this.toolbarService.SetToolButtonEnabled("注销账户", false);
                        this.toolbarService.SetToolButtonEnabled("修改密码", false);
                        this.toolbarService.SetToolButtonEnabled("结清账户", false);
                        this.toolbarService.SetToolButtonEnabled("欠费查询", false);
                        this.txtpay.Enabled = false;
                        this.cmbPayType.Enabled = false;
                        break;
                    }
                case 1:
                    {
                        this.toolbarService.SetToolButtonEnabled("新建账户", false);
                        this.toolbarService.SetToolButtonEnabled("收取", true);
                        this.toolbarService.SetToolButtonEnabled("返还", true);
                        // 添加账户取现功能
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        this.toolbarService.SetToolButtonEnabled("取现", true);
                        this.toolbarService.SetToolButtonEnabled("补打", true);
                        this.toolbarService.SetToolButtonEnabled("停用账户", true);
                        this.toolbarService.SetToolButtonEnabled("启用账户", false);
                        this.toolbarService.SetToolButtonEnabled("注销账户", true);
                        this.toolbarService.SetToolButtonEnabled("修改密码", true);
                        this.toolbarService.SetToolButtonEnabled("结清账户", true);
                        this.toolbarService.SetToolButtonEnabled("欠费查询", true);
                        this.txtpay.Enabled = true;
                        this.cmbPayType.Enabled = true;
                        this.cmbPayType.Focus();
                        break;
                    }
                case 2:
                    {
                        this.toolbarService.SetToolButtonEnabled("新建账户", false);
                        this.toolbarService.SetToolButtonEnabled("收取", false);
                        this.toolbarService.SetToolButtonEnabled("返还", false);
                        // 添加账户取现功能
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        this.toolbarService.SetToolButtonEnabled("取现", false);
                        this.toolbarService.SetToolButtonEnabled("补打", false);
                        this.toolbarService.SetToolButtonEnabled("停用账户", false);
                        this.toolbarService.SetToolButtonEnabled("启用账户", true);
                        this.toolbarService.SetToolButtonEnabled("注销账户", false);
                        this.toolbarService.SetToolButtonEnabled("修改密码", false);
                        this.toolbarService.SetToolButtonEnabled("结清账户", true);
                        this.toolbarService.SetToolButtonEnabled("欠费查询", false);
                        this.txtpay.Enabled = false;
                        this.cmbPayType.Enabled = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// 账户实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.Account GetAccount()
        {
            try
            {
                //账户信息
                account = new FS.HISFC.Models.Account.Account();
                account.ID = accountManager.GetAccountNO();
                account.AccountCard = accountCard;
                //是否取默认密码，系统设置，上线初期一般默认密码。
                //账户密码
                if (!this.IsDefaultPassword)
                {
                    ucEditPassWord uc = new ucEditPassWord(false);
                    FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                    if (uc.FindForm().DialogResult != DialogResult.OK) return null;
                    //加密密码
                    account.PassWord = uc.PwStr;
                }
                else
                {
                    account.PassWord = "000000";
                }


                //是否可用
                account.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                return account;
            }
            catch 
            {
                MessageBox.Show("获取账户信息失败！");
                return null;
            }
        }

        /// <summary>
        /// 得到卡的交易实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.AccountRecord GetAccountRecord()
        {
            try
            {
                //交易信息
                accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                accountRecord.AccountNO = this.account.ID;//帐号
                accountRecord.Patient = accountCard.Patient;//门诊卡号
                accountRecord.FeeDept.ID = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//科室编码
                accountRecord.Oper.ID = accountManager.Operator.ID;//操作员
                accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//操作时间
                accountRecord.IsValid = true;//是否有效
                return accountRecord;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 清空显示数据
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
        /// 清除账户信息
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
        /// 检查是否该用户是否授权
        /// </summary>
        /// <returns></returns>
        private bool IsEmpower()
        {
            AccountEmpower accountEmpower = new AccountEmpower();
            int resultValue = accountManager.QueryAccountEmpowerByEmpwoerCardNO(accountCard.Patient.PID.CardNO, ref accountEmpower);
            if (resultValue < 0)
            {
                MessageBox.Show("查找该用户的授权信息失败！");
                this.txtMarkNo.Text = string.Empty;
                this.txtMarkNo.Focus();
                return false;
            }
            if (resultValue > 0)
            {
                if (accountEmpower.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    MessageBox.Show("该用户已被授权，不能再建立账户！");
                    this.txtMarkNo.Text = string.Empty;
                    this.txtMarkNo.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断证件号是否存在账户
        /// </summary>
        /// <returns></returns>
        private bool ValidIDCard()
        {
            if (this.JudgeCredentialCreating == "0")
            {
                //判断证件号是否存在账户
                ArrayList accountList = accountManager.GetAccountByIdNO(this.txtIdCardNO.Text.Trim(), this.cmbIdCardType.Tag.ToString());
                if (accountList == null)
                {
                    MessageBox.Show("查找患者账户信息失败！");
                    return false;
                }
                //根据证件号查找患者账户信息
                if (accountList.Count > 0)
                {
                    MessageBox.Show("该" + this.cmbIdCardType.Text + "号已存在账户！");
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
        /// 新建立账户
        /// </summary>
        protected virtual void NewAccount()
        {
            try
            {
                if (accountCard == null || accountCard.MarkNO == string.Empty)
                {
                    MessageBox.Show("请输入就诊卡号！", "提示", MessageBoxButtons.OK);
                    return;
                }

                if (this.JudgeCredentialCreating == "0")
                {
                    if (this.txtIdCardNO.Text == string.Empty)
                    {
                        MessageBox.Show("请输入有效证件号！");
                        this.txtIdCardNO.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(this.txtIdCardNO.Text.Trim()))
                {
                    if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIdCardNO.Text.Trim(), ref error) < 0)
                    {
                        if(MessageBox.Show("身份证不合法，" + error + " \r\n是否继续？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                        this.txtIdCardNO.Focus();
                        this.txtIdCardNO.SelectAll();
                        return;
                        }
                    }
                }
                //判断证件号是否存在账户
                if (!ValidIDCard()) return;

                //判断账户是否被授权

                if (!IsEmpower()) return;

                //获取账户实体
                this.account = this.GetAccount();
                if (account == null) return;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //更新患者基本信息
                if (this.txtIdCardNO.Enabled && !string.IsNullOrEmpty(this.txtIdCardNO.Text.Trim()))
                {
                    HISFC.Models.RADT.PatientInfo patient = accountCard.Patient;
                    patient.IDCardType.ID = this.cmbIdCardType.Tag.ToString();
                    patient.IDCard = this.txtIdCardNO.Text.Trim();
                    //根据身份证号获取患者性别
                    FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(patient.IDCard, ref error);
                    if (obj == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error);
                        return;
                    }
                    patient.Sex.ID = obj.ID;
                    //根据患者身份证号获取生日
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
                //插入账户表
                if (accountManager.InsertAccount(this.account) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("建立账户失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //生成账户流水信息
                this.accountRecord = this.GetAccountRecord();
                if (this.accountRecord != null)
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.NewAccount;
                    if (accountManager.InsertAccountRecord(accountRecord) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("建立账户失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("建立账户失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("建立账户成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SetControlState(1);
            }
            catch(Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("建立账户失败！"+e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PrintCreateAccountRecipe(account);

        }
        
        /// <summary>
        /// 根据门诊账户获取卡的交易记录
        /// </summary>
        /// <returns></returns>
        private void GetRecordToFp()
        {
            if (account == null) return;
            List<PrePay> list = this.accountManager.GetPrepayByAccountNOEX(account.ID, "0");// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (list == null)
            {
                MessageBox.Show(this.accountManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.SetAccountRecordToFp(list, this.neuSpread1_Sheet1);
        }

        /// <summary>
        /// 获取账户预交金历史数据
        /// </summary>
        private void GetHistoryRecordToFp()
        {
            if (account == null) return;
            List<PrePay> list = this.accountManager.GetPrepayByAccountNOEX(account.ID, "1");// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (list == null)
            {
                MessageBox.Show(this.accountManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.SetAccountRecordToFp(list, this.spHistory);
        }

        /// <summary>
        /// 显示账户预交金数据
        /// </summary>
        /// <param name="list">预交金数据</param>
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
        /// 显示预交金信息
        /// </summary>
        /// <param name="prepay"></param>
        private void SetFp(PrePay prepay,FarPoint.Win.Spread.SheetView sheet)
        {
            int count = sheet.Rows.Count;
            sheet.Rows.Add(count, 1);
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
        }

        /// <summary>
        /// 显示账户卡信息
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
        /// 输入就诊卡号获取账户信息
        /// </summary>
        private void GetAccountInfo()
        {
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtMarkNo.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("请输入就诊卡号！");
                return;
            }
            int resultValue = accountManager.GetCardByRule(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                this.Clear();
                return;
            }
            //账户授权效验
            if (!this.IsEmpower())
            {
                this.Clear();
                return;
            }
            ClearAccountInfo();
            this.txtMarkNo.Text = accountCard.MarkNO;
            this.cmbCardType.Tag = accountCard.MarkType.ID;
            //显示患者信息
            ShowPatienInfo(accountCard.Patient.PID.CardNO);
            //01 为身份证号，在常数维护中维护
            if (this.cmbIdCardType.Tag != null && this.cmbIdCardType.Tag.ToString() != string.Empty && this.txtIdCardNO.Text.Trim() != string.Empty)
            {
                this.txtIdCardNO.Enabled = false;
                //this.cmbPayType.Focus();
            }
            else
            {
                this.txtIdCardNO.Enabled = true;
                this.cmbIdCardType.Tag = "01";//身份证号
                this.txtIdCardNO.Focus();
            }

            //查找账户信息
            this.GetAccountByMark();
            //预交金记录
            GetRecordToFp();
            //就诊断卡记录
            GetCardRecordToFP();
            //预交金历史记录
            GetHistoryRecordToFp();
        }

        /// <summary>
        /// 回车处理
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
        /// 修改密码
        /// </summary>
        protected virtual void EditPassword()
        {
            if (!ValidAccountCard()) return;
            ucEditPassWord uc = new ucEditPassWord(true);
            uc.Account = this.account;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool ValidAccountCard()
        {
            //if (accountCard == null || string.IsNullOrEmpty(accountCard.MarkNO) || string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))
            if (accountCard == null || string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))            
            {
                MessageBox.Show("请输入就诊卡号！", "提示", MessageBoxButtons.OK);
                this.txtMarkNo.Focus();
                this.txtMarkNo.SelectAll();
                return false;
            }
            account = accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (account == null)
            {
                MessageBox.Show("该卡未建立账户或账户已注销，请建立账户！", "提示");
                return false;
            }
            return true;
        }


        /// <summary>
        /// 支付预交金
        /// </summary>
        protected virtual void AccountPrePay()
        {
            //如果收费员非常快速度敲打回车键，会导致重复收取预交金费用。所以让txtpay失去焦点,同时让txtPay的值变为0
            //this.neuLabel1.Focus();
            string accountPay = this.txtpay.Text;
            //this.txtpay.Text = "0.00";
            string donateAccount = this.txtDonate.Text;

            #region 验证
            if (!this.ValidAccountCard())
            {
                this.txtMarkNo.Focus();
                return;
            }
            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("请选择支付方式！", "提示");
                this.cmbPayType.Focus();
                return;
            }
            decimal money = FS.FrameWork.Function.NConvert.ToDecimal(accountPay);
            decimal donate = FS.FrameWork.Function.NConvert.ToDecimal(donateAccount);
            if (money == 0)
            {
                if (MessageBox.Show("充值金额 " + money.ToString() + "元，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (donate == 0)
                    {
                        MessageBox.Show("充值金额为0，赠送金额不能为0");
                        txtpay.Focus();
                        return;
                    }
                }
                //MessageBox.Show("请输入交费金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //txtpay.Focus();
                //return;
            }
            else if (money > decLimitePerpayMoney)
            {
                if (MessageBox.Show("充值金额 " + money.ToString() + "元 大于 充值限额 【" + decLimitePerpayMoney.ToString() + " 元】 是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    txtpay.Focus();
                    txtpay.SelectAll();
                    return;
                }
            }

            if (MessageBox.Show("是否确认充值！", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                this.txtpay.Focus();
                return;
            }

            #endregion



            #region 预交金实体
            //获取预交金实体
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
                    MessageBox.Show("获取优惠金额出错 "+ errText );
                    this.txtpay.Focus();
                    return;
                }
            }

            #endregion

            #region 更新数据
            //设置事物

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


            if (!accountManager.AccountPrePayManagerEX(prePay, 1))// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(accountManager.Err, "错误");
                this.txtpay.Focus();
                return;
            }
            //if (donate != 0)//lfhm// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            //{
            //    FS.HISFC.Models.Account.Account account1 = new FS.HISFC.Models.Account.Account();
            //    account1 = accountManager.GetAccountVacancyEX(this.ucRegPatientInfo1.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            //    prePay.BaseVacancy = 0;
            //    prePay.FT.PrepayCost = 0;
            //    prePay.PayType.ID = "DC";//支付方式
            //    prePay.PayType.Name = "赠送";
            //    prePay.DonateCost = donate;
            //    prePay.DonateVacancy = account1.DonateAmout + donate;
            //    if (!accountManager.AccountPrePayManagerEX(prePay, 1))// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(accountManager.Err, "错误");
            //        this.txtDonate.Focus();
            //        return;
            //    }
            //}
            FS.FrameWork.Management.PublicTrans.Commit();
             MessageBox.Show("交费 （" + accountPay + "） 成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion

            this.GetAccountByMark();
            this.GetRecordToFp();

            prePay.BaseVacancy = this.account.BaseVacancy;

            #region 打印 
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
        /// 反还预交金
        /// </summary>
        protected virtual void AccountCancelPrePay()
        {
            if (!ValidAccountCard()) return;
            if (neuSpread1_Sheet1.Rows.Count == 0) return;
            if (this.neuSpread1_Sheet1.ActiveRow.Tag == null) return;
            PrePay prePay = this.neuSpread1_Sheet1.ActiveRow.Tag as PrePay;

            #region 验证
            if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
            {
                MessageBox.Show("该笔金额已返还记录，不能返还！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                MessageBox.Show("该笔金额为补打记录，不能返还！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (this.account.BaseVacancy < prePay.BaseCost)
            {
                MessageBox.Show("账户余额不足，不能退此笔费用！");
                return;
            }
                MessageBox.Show(prePay.PayType.ID);
            if (prePay.PayType.ID != "CA")
            {
                MessageBox.Show("只有现金支付的金额才能返还！");
                return;
            }
            #endregion

            if (MessageBox.Show("确认返还此笔金额？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (!this.accountManager.AccountPrePayManager(prePay, 0))
            {
                MessageBox.Show(accountManager.Err, "错误");
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("返还成功，票据号为：" + prePay.InvoiceNO + "金额为：" + prePay.BaseCost.ToString() + "！");
            this.GetAccountByMark();
            this.GetRecordToFp();
        }
             
        /// <summary>
        /// 停用账户
        /// </summary>
        protected virtual void StopAccount()
        {
            if (!ValidAccountCard()) return;
            if (account == null) return;

            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
            {
                MessageBox.Show("该账户已停用，请启用该账户！");
                return;
            }
            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                MessageBox.Show("该账户已注销！");
                return;
            }
            if (MessageBox.Show("确认停用账户？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) return;

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            bool isCancelVacancy = false;
            DialogResult resultValue = MessageBox.Show("停用账户同时，是否结清余额？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (resultValue == DialogResult.Yes)
            {
                isCancelVacancy = true;
                if (!ValidCancelVacancy(accountCard.Patient.PID.CardNO))
                {
                    return;
                }
            }

            //设置事物
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //退费金额
            string MessageStr = string.Empty;
            //是否刷新预交金数据
            bool isFreshPrePay = false;
            try
            {

                #region 在停用账户时是否结清余额
                decimal vacancy = 0;
                //判断账户余额
                int result = accountManager.GetVacancy(accountCard.Patient.PID.CardNO, ref vacancy);
                if (result <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.accountManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                //如果在停用账户时如果账户余额大于０提示是否结清账户
                string errText = string.Empty;
                if (vacancy > 0)
                {
                    //结清账户
                    if (isCancelVacancy)
                    {
                        MessageStr = "应退用户" + vacancy.ToString() + "元！";
                        //刷新账户预交金数据
                        isFreshPrePay = true;
                        if (!this.UpdateAccountVacancy(vacancy, "结清账户", OperTypes.BalanceVacancy, ref errText))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(errText);
                            return;
                        }
                    }
                }
                #endregion

                //更改账户状态
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
                    MessageBox.Show("更新账户授权信息失败！" + accountManager.Err);
                    return;
                }

            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("停用账户失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            
            MessageBox.Show("停用账户成功！\n" + MessageStr, "提示");
            //设置状态
            SetControlState(2);
            //刷新账户信息
            GetAccountByMark();
            if (isFreshPrePay)
            {
                this.GetRecordToFp();
                this.GetHistoryRecordToFp();
            }

        }

        /// <summary>
        /// 结清账户余额
        /// </summary>
        protected virtual void BalanceVacancy()
        {
            if (!this.ValidAccountCard())
            {
                this.txtMarkNo.Focus();
                return;
            }
            decimal vacancy = 0;
            //判断账户余额
            int result = accountManager.GetVacancy(accountCard.Patient.PID.CardNO, ref vacancy);
            if (result <= 0)
            {
                MessageBox.Show(this.accountManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (vacancy <= 0)
            {
                MessageBox.Show("该账户不存在余额，不能结清账户余额！");
                return;
            }
            txtpay.Text = vacancy.ToString();
            #region 新的结清代码

            #region 验证

            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("请选择支付方式！", "提示");
                this.cmbPayType.Focus();
                return;
            }

            else if (vacancy > decLimitePerpayMoney && cmbPayType.Text == "现金")
            {
                if (MessageBox.Show("取现金额 " + vacancy.ToString() + "元 大于 取现限额 【" + decLimitePerpayMoney.ToString() + " 元】 是否继续使用现金取现?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    txtpay.Focus();
                    return;
                }
            }

            if (MessageBox.Show("是否确认结清帐户?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                this.txtpay.Focus();
                return;
            }
            #endregion

            #region 控制套现

            if (blnIsLeverageControl)
            {
                decimal remainCACost = this.GetRemainCACost(accountCard.Patient.PID.CardNO);
                if (remainCACost < vacancy)
                {
                    MessageBox.Show("当前账户现金数不足取现金额！");
                    return;
                }
            }

            #endregion

            //验证授权人账户密码
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;
            #region 取现
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region 预交金实体

            //获取预交金实体
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
                MessageBox.Show(accountManager.Err, "错误");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("取现成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            this.GetAccountByMark();
            this.GetRecordToFp();
            prePay.BaseVacancy = this.account.BaseVacancy;

            #endregion

            #region 打印
            // {48314E1F-72EC-4044-A41A-833C84687A40}
            PrintPrePayRecipe(prePay);
            #endregion
            if (isSaveClear)
            {
                Clear();
            }

            #endregion
            #region 原来的结清代码

            //#region 控制套现

            ////decimal remainCACost = this.GetRemainCACost(accountCard.Patient.PID.CardNO);
            ////if (remainCACost < vacancy)
            ////{
            ////    MessageBox.Show("当前账户现金金额为" + remainCACost.ToString() + "元，余额包含非现金金额，不可全部取现！");
            ////    return;
            ////}

            //#endregion

            //if (MessageBox.Show("确认要结清该账户的余额？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            ////{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            //if (!ValidCancelVacancy(accountCard.Patient.PID.CardNO))
            //{
            //    return;
            //}

            //string errText = string.Empty;
            //bool resultValue = this.UpdateAccountVacancy(vacancy, "结清账户", OperTypes.BalanceVacancy, ref errText);
            //if (!resultValue)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show(errText);
            //    return;
            //}
            //FS.FrameWork.Management.PublicTrans.Commit();
            //MessageBox.Show("应退现金" + vacancy.ToString() + "元！");
            #endregion
        }

        /// <summary>
        /// 启用账户
        /// </summary>
        protected virtual void AginAccount()
        {
           // if (!ValidAccountCard()) return;


            account = accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}

            if (account == null)
            {
                MessageBox.Show("该卡未建立账户或账户已注销，请建立账户！", "提示");
                return;
            }
          

            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                MessageBox.Show("该账户不处于停用状态，不能启用该账户！");
                return;
            }
            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                MessageBox.Show("该账户已注销！");
                return;
            }

            if (MessageBox.Show("确认启用该账户？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            //设置事物
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //更新账户状态
            string errText = string.Empty;
            bool bl = UpdateAccountState(FS.HISFC.Models.Base.EnumValidState.Valid,ref errText);
            if (!bl)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);
                return;
            }
            //更新授权账户状态
            if (accountManager.UpdateEmpowerState(account.ID, FS.HISFC.Models.Base.EnumValidState.Valid, FS.HISFC.Models.Base.EnumValidState.Ignore) < 0)
            {
                 FS.FrameWork.Management.PublicTrans.RollBack();
                 MessageBox.Show("更新授权账户信息失败！");
                 return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("启用账户成功！");
            SetControlState(1);
        }

        /// <summary>
        /// 重打发票
        /// </summary>
        protected virtual void ReprintInvoice()
        {
            try
            {
                if (!ValidAccountCard()) return;
                if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
                if (this.neuSpread1_Sheet1.ActiveRow.Tag == null) return;
                PrePay prePay = (this.neuSpread1_Sheet1.ActiveRow.Tag as PrePay).Clone();

                #region 验证
                if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    MessageBox.Show("该笔费用为返还记录，不能补打！", "提示", MessageBoxButtons.OK);
                    return;
                }
                if (prePay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    MessageBox.Show("该笔费用为补打记录，不能补打！", "提示", MessageBoxButtons.OK);
                    return;
                }
                #endregion

                if (MessageBox.Show("是否确认重打该次凭证？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                #region 更新发票状态
                //更新发票状态
                prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;//补打
                if (accountManager.UpdatePrePayState(prePay) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("该条记录已经进行过返还补打操作，更新状态出错!");
                    return;
                }
                #endregion

                #region 插入作废信息
                //补打
                prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;
                prePay.BaseCost = -prePay.BaseCost;
                prePay.OldInvoice = prePay.InvoiceNO;

                prePay.PrePayOper.ID = this.accountManager.Operator.ID;//add by sung 2009-2-26 {E5178DF3-9C61-43b3-BF61-3EA99A9989E2}
                
                if (accountManager.InsertPrePay(prePay) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废发票出错" + accountManager.Err, "错误");
                    return;
                }
                #endregion

                #region 插入收费信息
                //获取发票号
                string invoiceNO = this.feeIntegrate.GetNewInvoiceNO("A");
                if (invoiceNO == null || invoiceNO == string.Empty)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("获得发票号出错!" + this.feeIntegrate.Err);
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
                    MessageBox.Show("补打印发票失败！");
                    return;
                }
                prePay.PayType.Name = this.cmbPayType.GetNameByID(prePay.PayType.ID);
                #endregion

                //打印票据
                FS.HISFC.Models.Account.AccountRecord accRecord = accountManager.GetAccountRecord(prePay.Patient.PID.CardNO, prePay.InvoiceNO);
                if (accRecord != null)
                {
                    // 交易后余额
                    prePay.BaseVacancy = accRecord.BaseVacancy;
                }
                this.PrintPrePayRecipe(prePay);
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("补打印发票失败！" + ex.Message);
                return;
            }
            GetRecordToFp();
          
        }
        /// <summary>
        /// 补打发票
        /// </summary>
        protected virtual void ReprintInvoiceNotInsert()
        {
            try
            {
                if (!ValidAccountCard()) return;
                if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
                if (this.neuSpread1_Sheet1.ActiveRow.Tag == null) return;
                PrePay prePay = (this.neuSpread1_Sheet1.ActiveRow.Tag as PrePay).Clone();

                if (MessageBox.Show("是否确认补打该次凭证？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //不明白为什么要把它的状态改成ignore 去掉 by yerl
                //FS.HISFC.Models.Base.EnumValidState printState = prePay.ValidState;

                //prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;

                FS.HISFC.Models.Account.AccountRecord accRecord = accountManager.GetAccountRecord(prePay.Patient.PID.CardNO, prePay.InvoiceNO);
                if (accRecord != null)
                {
                    // 交易后余额
                    prePay.BaseVacancy = accRecord.BaseVacancy;
                }
                //打印票据
                prePay.Memo = txtMarkNo.Text;
                prePay.IsHostory = true;
                this.PrintPrePayRecipe(prePay);

                //prePay.ValidState = printState;
            }
            catch (Exception ex)
            {
                MessageBox.Show("补打印发票失败！" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// 注销账户
        /// </summary>
        protected virtual void CancelAccount()
        {
            
            if (!ValidAccountCard()) return;
            if (MessageBox.Show("确认注销该账户？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            //验证密码
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            if (!ValidCancelVacancy(accountCard.Patient.PID.CardNO))
            {
                return;
            }


            decimal vacancy = 0;
            string messStr = string.Empty;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            
            //判断账户余额
            int result = accountManager.GetVacancy(accountCard.Patient.PID.CardNO, ref vacancy);
            if (result <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.accountManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //更新账户余额
            string errText = string.Empty;
            if (vacancy > 0)
            {
                
                if (!UpdateAccountVacancy(vacancy, "结清账户", OperTypes.BalanceVacancy, ref errText))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errText);
                    return;
                }
                messStr = "应退用户" + vacancy.ToString();

            }
            //更新账户状态
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
                MessageBox.Show("更新授权账户状态失败！");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("注销账户成功！" + messStr);
            SetControlState(0);
            this.GetRecordToFp();
            this.GetHistoryRecordToFp();
        }

        /// <summary>
        /// 获取预约金实体
        /// </summary>
        /// <param name="state">实体状态</param>
        /// <returns></returns>
        private PrePay GetPrePay(FS.HISFC.Models.Base.EnumValidState state)
        {
            /// {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
            string invoiceNO = "";
            #region 获取发票号
            if (IsDefaultInvoiceNO) //中大六院获取发票号码
            {
                invoiceNO = this.accountManager.GetNewInvoiceNO("A");
            }
            else
            {
                invoiceNO = this.feeIntegrate.GetNewInvoiceNO("A");
                if (invoiceNO == null || invoiceNO == string.Empty)
                {
                    MessageBox.Show("获得发票号出错!" + this.feeIntegrate.Err);
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
            #region 预交金实体
            HISFC.Models.Account.PrePay prePay = new FS.HISFC.Models.Account.PrePay();
            prePay.Patient = this.ucRegPatientInfo1.GetPatientInfomation();//患者基本信息
            prePay.InvoiceNO = invoiceNO;
            prePay.PayType.ID = this.cmbPayType.Tag.ToString();//支付方式
            prePay.PayType.Name = this.cmbPayType.Text;

            //prePay.Bank = this.cmbPayType.bank.Clone();//开户银行
            if (prePay.PayType.ID != "CA" && !string.IsNullOrEmpty(BankPayType))
            {
                prePay.PayType.ID = BankPayType;//支付方式
                prePay.PayType.Name = "银行卡";
            }

            prePay.PrePayOper.ID = accountManager.Operator.ID;//操作员编号
            prePay.PrePayOper.Name = accountManager.Operator.Name;//操作员姓名
            prePay.ValidState = state;
            prePay.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(txtpay.Text);//预交金
            prePay.PrePayOper.OperTime = accountManager.GetDateTimeFromSysDateTime();//系统时间
            prePay.AccountNO = account.ID; //帐号
            prePay.IsHostory = false; //是否历史数据
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
        /// 获取预约金实体
        /// </summary>
        /// <param name="state">实体状态</param>
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
            #region 获取发票号
            if (IsDefaultInvoiceNO) //中大六院获取发票号码
            {
                invoiceNO = this.accountManager.GetNewInvoiceNO("A");
            }
            else
            {
                invoiceNO = this.feeIntegrate.GetNewInvoiceNO("A");
                if (invoiceNO == null || invoiceNO == string.Empty)
                {
                    MessageBox.Show("获得发票号出错!" + this.feeIntegrate.Err);
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
            #region 预交金实体
            HISFC.Models.Account.PrePay prePay = new FS.HISFC.Models.Account.PrePay();
            prePay.Patient = this.ucRegPatientInfo1.GetPatientInfomation();//患者基本信息
            prePay.InvoiceNO = invoiceNO;
            prePay.PayType.ID = this.cmbPayType.Tag.ToString();//支付方式
            prePay.PayType.Name = this.cmbPayType.Text;

            //prePay.Bank = this.cmbPayType.bank.Clone();//开户银行
            if (prePay.PayType.ID != "CA" && !string.IsNullOrEmpty(BankPayType))
            {
                prePay.PayType.ID = BankPayType;//支付方式
                prePay.PayType.Name = "银行卡";
            }

            prePay.PrePayOper.ID = accountManager.Operator.ID;//操作员编号
            prePay.PrePayOper.Name = accountManager.Operator.Name;//操作员姓名
            prePay.ValidState = state;
            prePay.BaseVacancy = pay;//预交金
            prePay.PrePayOper.OperTime = accountManager.GetDateTimeFromSysDateTime();//系统时间
            prePay.AccountNO = account.ID; //帐号
            prePay.IsHostory = false; //是否历史数据
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
        /// 账户取现 原有的逻辑不对,我对其做了完全的修改 by yerl
        /// {4679504A-CEDA-44a8-8C67-DB7F847C6450}
        /// </summary>
        /// <returns></returns>
        private void AccountTaken()
        {
            //如果收费员非常快速度敲打回车键，会导致重复收取预交金费用。所以让txtpay失去焦点,同时让txtPay的值变为0
            this.neuLabel1.Focus();
            string accountPay = this.txtpay.Text;
            //this.txtpay.Text = "0.00";

            #region 验证
            if (!this.ValidAccountCard())
            {
                this.txtMarkNo.Focus();
                return;
            }
            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("请选择支付方式！", "提示");
                this.cmbPayType.Focus();
                return;
            }
            decimal money = FS.FrameWork.Function.NConvert.ToDecimal(accountPay);
            if (money == 0)
            {
                MessageBox.Show("请输入取现金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpay.Focus();
                return;
            }
            else if (money > this.account.BaseVacancy)
            {
                MessageBox.Show("帐户余额不足！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpay.Focus();
                return;
            }
            else if (money > decLimitePerpayMoney && cmbPayType.Text == "现金")
            {
                if (MessageBox.Show("取现金额 " + money.ToString() + "元 大于 取现限额 【" + decLimitePerpayMoney.ToString() + " 元】 是否继续使用现金取现?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    txtpay.Focus();
                    return;
                }
            }

            if (MessageBox.Show("是否确认取现?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                this.txtpay.Focus();
                return;
            }
            #endregion

            #region 控制套现

            if (blnIsLeverageControl)
            {
                decimal remainCACost = this.GetRemainCACost(accountCard.Patient.PID.CardNO);
                if (remainCACost < money)
                {
                    MessageBox.Show("当前账户现金数不足取现金额！");
                    return;
                }
            }

            #endregion

            //验证授权人账户密码
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;
            #region 取现
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region 预交金实体

            //获取预交金实体
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
                MessageBox.Show(accountManager.Err, "错误");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("取现成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.GetAccountByMark();
            this.GetRecordToFp();
            prePay.BaseVacancy = this.account.BaseVacancy;
            this.txtpay.Text = "0.00";
            this.txtDonate.Text = "0.00";
            #endregion
            #region 打印
            // {48314E1F-72EC-4044-A41A-833C84687A40}
            PrintPrePayRecipe(prePay);
            #endregion
            if (isSaveClear)
            {
                Clear();
            }
        }

        /// <summary>
        /// 根据卡号获得可取现金额
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        private decimal GetRemainCACost(string cardNo)
        {
            decimal remainCACost = 0;

            #region 查询账户中现金金额

            //查询账户交易记录
            List<PrePay> prepayList = this.accountManager.GetPrepayByAccountNOEX(account.ID, "0");// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (prepayList == null)
            {
                MessageBox.Show(this.accountManager.Err);
                return 0;
            }

            //账户中现金数
            decimal accountCostCA = 0;
            for (int i = 0; i < prepayList.Count; i++)
            {
                if (prepayList[i].PayType.ID == "CA")
                {
                    accountCostCA += prepayList[i].BaseCost;
                }
            }
            #endregion

            #region 查询已经取现金额 

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


            #region 查询账户余额

            decimal remain = 0;

            if (this.accountManager.GetVacancy(account.CardNO, ref remain) == -1)
            {
                MessageBox.Show("查询账户余额出错：" + this.accountManager.Err);
                return 0;
            }
            #endregion

            // 可取余额数 <= 所有现金 - 已取现金
            remainCACost = accountCostCA + decHasTaken;
            if (remain < remainCACost)
            {
                remainCACost = remain;
            }


            return remainCACost;
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// 修该账户状态
        /// </summary>
        /// <param name="validState">账户状态</param>
        /// <returns>true 成功 false失败</returns>
        private bool UpdateAccountState(HISFC.Models.Base.EnumValidState validState,ref string errText)
        {
            //更改账户状态
            if (accountManager.UpdateAccountState(account.ID, ((int)validState).ToString()) < 0)
            {
                errText = "更新账户余额失败！" + accountManager.Err;
                return false;
            }

            //是否打印票据
            bool isPrint = false;
            //插入账户交易记录
            accountRecord = this.GetAccountRecord();
            if (accountRecord == null)
            {
                errText = "获取账户操作数据失败！";
                return false;
            }
            switch (validState)
            {
                //停用
                case FS.HISFC.Models.Base.EnumValidState.Invalid:
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.StopAccount;
                        isPrint = true;
                        break;
                    }
                //在用
                case FS.HISFC.Models.Base.EnumValidState.Valid:
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.AginAccount;
                        isPrint = true;
                        break;
                    }
                //注销
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
                errText = "插入交易记录失败！" + accountManager.Err;
                return false;
            }
            if (isPrint)
            {
                this.PrintAccountOperRecipe(accountRecord);
            }
            return true;
        }


        /// <summary>
        /// 更新账户余额
        /// </summary>
        /// <param name="money">金额</param>
        /// <returns>true 成功 false失败</returns>
        private bool UpdateAccountVacancy(decimal money, string reMark, OperTypes opertype, ref string errText)
        {
            //更新账户余额
            if (accountManager.UpdateAccountVacancy(account.ID, money) <= 0)
            {
                errText = "更新账户余额失败！" + accountManager.Err;
                return false;
            }
            //交易实体
            accountRecord = this.GetAccountRecord();

            //结清账户按
            accountRecord.OperType.ID = (int)opertype;
            //退费插如负数
            accountRecord.BaseCost = -money;
            accountRecord.BaseVacancy = 0;
            accountRecord.ReMark = reMark;
            if (accountManager.InsertAccountRecord(accountRecord) < 0)
            {
                errText = "生成交易记录失败！" + accountManager.Err;
                return false;
            }
            //在注销账户和停账户时反还余额打印票据
            if (opertype == OperTypes.BalanceVacancy)
            {
                //更新账户预交金历史数据状态
                if (accountManager.UpdatePrePayHistory(account.ID, false, true) < 0)
                {
                    errText = "更新账户金额失败！" + accountManager.Err;
                    return false;
                }
                PrintCancelVacancyRecipe(accountRecord);
            }
            return true;
        }


        // //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// 返还账户余额判断
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
                MessageBox.Show("查询患者未收费的费用信息失败！" + outPatientManager.Err);
                return false;
            }
            if (al.Count > 0)
            {
                DialogResult diaResult = MessageBox.Show("存在未收费的费用，是否继续返还账户余额！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
        /// 计算未扣费用,按待遇计算
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

        #region 打印
        /// <summary>
        /// 打印建账户凭证
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
                MessageBox.Show("请维护打印票据，查找打印票据失败！");
                return;
            }
            account.AccountCard.Patient.IDCardType.Name = this.cmbIdCardType.Text;
            Iprint.SetValue(tempaccount);
            Iprint.Print();
        }

        /// <summary>
        /// 打印预交金票据
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
                    MessageBox.Show("请维护打印票据，查找打印票据失败！");
                    return;
                }
            }
            Iprint.SetValue(temprePay);
            Iprint.Print();
        }
        /// <summary>
        /// 打印预交金票据// {F69DC114-4EF6-4eb2-8018-0F7192139E27}
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
        /// 打印反还余额票据
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
                MessageBox.Show("请维护打印票据，查找打印票据失败！");
                return;
            }
            Iprint.SetValue(tempaccountRecord);
            Iprint.Print();
        }

        /// <summary>
        /// 打印账户操作票据
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
                MessageBox.Show("请维护打印票据，查找打印票据失败！");
                return;
            }
            Iprint.SetValue(tempaccountRecord);
            Iprint.Print();
        }

        /// <summary>
        ///  初始化接口
        /// </summary>
        private void InitInterface()
        {
            this.iAccountProcessPrepay = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.HISFC.BizProcess.Interface.Account.IAccountProcessPrepay)) as FS.HISFC.BizProcess.Interface.Account.IAccountProcessPrepay;
        }

        #endregion

        #endregion

        #region 事件

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
            toolbarService.AddToolButton("新建账户", "新建账户", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolbarService.AddToolButton("修改密码", "修改密码", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolbarService.AddToolButton("停用账户", "停用账户", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            toolbarService.AddToolButton("启用账户", "启用账户", FS.FrameWork.WinForms.Classes.EnumImageList.K开帐, true, false, null);
            toolbarService.AddToolButton("注销账户", "注销账户", FS.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null);
            toolbarService.AddToolButton("收取", "收取预交金", FS.FrameWork.WinForms.Classes.EnumImageList.Q确认收费, true, false, null);
            toolbarService.AddToolButton("作废凭证", "返还预交金", FS.FrameWork.WinForms.Classes.EnumImageList.Q全退, true, false, null);
            // 添加账户取现功能
            // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
            toolbarService.AddToolButton("取现", "返还指定预交金金额", FS.FrameWork.WinForms.Classes.EnumImageList.T退费, true, false, null);
            //添加补打功能，不走发票号，不插入负记录，只是简单的查询打印，而重打是插负记录，走下一个号
            //{A6C8F37F-0E76-4dad-9FF6-0EAA518AA148}
            toolbarService.AddToolButton("补打", "补打预交金收据", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolbarService.AddToolButton("重打", "重打预交金收据", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolbarService.AddToolButton("清屏", "清屏", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolbarService.AddToolButton("结清账户", "结清账户余额", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolbarService.AddToolButton("欠费查询", "查询应交费用！", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolbarService.AddToolButton("刷卡", "刷卡", FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);


            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "修改密码":
                    {
                        EditPassword();
                        break;
                    }
                case "新建账户":
                    {
                        this.NewAccount();
                        break;
                    }
                case "停用账户":
                    {
                        StopAccount();
                        break;
                    }
                case "启用账户":
                    {
                        AginAccount();
                        break;
                    }
                case "收取":
                    {
                        AccountPrePay();
                        break;
                    }
                case "注销账户":
                    {
                        this.CancelAccount();
                        break;
                    }
                case "作废凭证":
                    {
                        this.AccountCancelPrePay();
                        break;
                    }
                case "重打":
                    {
                        this.ReprintInvoice();
                        break;
                    }
                case "补打":
                    {
                        this.ReprintInvoiceNotInsert();
                        break;
                    }
                case "清屏":
                    {
                        this.Clear();
                        break;
                    }
                case "结清账户":
                    {
                        this.BalanceVacancy();
                        break;
                    }
                case "取现":
                    {
                        // 账户取现
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        AccountTaken();
                        break;
                    }
                case "欠费查询":
                    {
                        // 查询欠费

                        this.QueryUnFeeByCarNo();
                        break;
                    }

                case "刷卡":
                    {
                        string McardNo = "";
                        string error = "";

                        if (Function.OperMCard(ref McardNo, ref error) < 0)
                        {
                            MessageBox.Show("读卡失败，请确认是否正确放置诊疗卡！\n" + error);
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
            //在支付金额中回车
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

       

    }
}
