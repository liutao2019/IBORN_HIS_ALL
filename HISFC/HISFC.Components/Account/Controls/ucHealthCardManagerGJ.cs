using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.FrameWork.Models;
using FS.HISFC.BizProcess.Interface.Account;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// 健康卡管理
    /// </summary>
    public partial class ucHealthCardManagerGJ : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucHealthCardManagerGJ()
        {
            InitializeComponent();
            this.txtMarkNO.KeyDown+=new KeyEventHandler(txtMarkNO_KeyDown);
        }

        #region 变量

        ///// <summary>
        ///// HealthCard业务层
        ///// </summary>
        //private FS.HISFC.BizLogic.HealthCard.HealthCardManager healthCardManager = new FS.HISFC.BizLogic.HealthCard.HealthCardManager();

        ///// <summary>
        ///// 健康卡实体
        ///// </summary>
        //private FS.HISFC.BizLogic.HealthCard.HealthCard healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();

        /// <summary>
        /// Manager业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        
        /// <summary>
        /// Acount业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper markHelper = new FS.FrameWork.Public.ObjectHelper();
        
        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 在输入过程中是否动态查找患者信息
        /// </summary>
        private bool isSelectPatientByEnter = true;

        /// <summary>
        /// 数据是否只在本地处理，不往数据中心发送
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        private bool isLocalOperation = true;

        private NeuObject cardTypeObj = new NeuObject();
        private string healthCardNo = string.Empty;
        private FS.HISFC.Models.Account.AccountCard accountCard = null;
        private PatientInfo Patient = null;
        private RHINCardServiceImplService.CardRequestType cardRequest = new RHINCardServiceImplService.CardRequestType();

        private HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 变更记录业务层
        /// </summary>
        HISFC.BizProcess.Integrate.Function functionIntegrate = new FS.HISFC.BizProcess.Integrate.Function();
        
        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 系统参数控制
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
       
        /// <summary>
        /// 患者信息
        /// </summary>
        private HISFC.Models.RADT.PatientInfo oldPatient = new FS.HISFC.Models.RADT.PatientInfo();
        
        /// <summary>
        /// 是否可以修改最近一次挂号记录
        /// </summary>
        private bool isCanEditLastRegInfo = false;
        
        #endregion

        #region 属性

        #region 输入控制属性
        [Category("输入设置"), Description("姓名是否必须输入！")]
        public bool IsInputName
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputName;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputName = value;
            }
        }

        [Category("输入设置"), Description("性别是否必须输入！")]
        public bool IsInputSex
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputSex;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputSex = value;
            }
        }

        [Category("输入设置"), Description("结算类别是否必须输入！")]
        public bool IsInputPact
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputPact;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputPact = value;
            }
        }

        [Category("输入设置"), Description("医保证号是否必须输入！")]
        public bool IsInputSiNo
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputSiNo; 
            }
            set 
            {
                this.ucRegPatientInfo1.IsInputSiNo = value;
            }
        }

        [Category("输入设置"), Description("出生日期是否必须输入！")]
        public bool IsInputBirthDay
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputBirthDay; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputBirthDay = value;
            }
        }

        [Category("输入设置"), Description("证件类型是否必须输入！")]
        public bool IsInputIDEType
        {
            get 
            { 
                return this.ucRegPatientInfo1.IsInputIDEType; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputIDEType = value;
            }
        }

        [Category("输入设置"), Description("证件号是否必须输入！")]
        public bool IsInputIDENO
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputIDENO; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputIDENO = value;
            }
        }
        /// <summary>
        /// 不能同时为空项目  0 = 不控制 1 = 控制 身份证与电话号码
        /// </summary>
        [Category("修改控制"), Description("不能同时为空项目  0 = 不控制 1 = 控制 身份证与电话号码")]
        public int IMustInpubByOne
        {
            get { return this.ucRegPatientInfo1.IMustInpubByOne; }
            set { this.ucRegPatientInfo1.IMustInpubByOne = value; }

        }

        #endregion

        [Category("控件设置"), Description("是否按照必录项跳转输入焦点 True:是 False:否")]
        public bool IsMustInputTabIndex
        {
            get
            {
                return this.ucRegPatientInfo1.IsMustInputTabIndex;
            }
            set
            {
                this.ucRegPatientInfo1.IsMustInputTabIndex = value;
            }
        }

        [Category("控件设置"),Description("在输入过程中是否动态查找患者信息 True:是 False:否")]
        public bool IsSelectPatientByEnter
        {
            get 
            { 
                return isSelectPatientByEnter;
            }
            set 
            { 
                isSelectPatientByEnter = value; 
            }
        }

        /// <summary>
        /// 数据是否只在本地处理，不往数据中心发送
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        [Category("控件设置"), Description("数据是否只在本地处理，不往数据中心发送 True:是 False:否")]
        public bool IsLocalOperation
        {
            get
            {
                return isLocalOperation;
            }
            set
            {
                isLocalOperation = value;
            }
        }

        [Category("控件设置"), Description("false:就诊卡号做物理卡号 true:就诊卡号与物理卡号不同")]
        public bool CardWay
        {
            get
            {
                return this.ucRegPatientInfo1.CardWay;
            }
            set
            {
                this.ucRegPatientInfo1.CardWay = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 查询患者信息
        /// </summary>
        protected virtual int QueryPatientInfo()
        {
            if (this.cmbIdCardType.SelectedItem == null || string.IsNullOrEmpty(this.txtIdCardNO.Text))
            {
                return -1;
            }
            List<AccountCard> list = new List<AccountCard>();
            if (this.cmbIdCardType.SelectedItem.ID == "09")
            {
                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                int returnValue = 0;
                returnValue = accountManager.GetCardByRule(this.txtIdCardNO.Text.Trim(), ref accountCard);
                if (returnValue == 1)
                {
                    list.Add(accountCard);
                }
                
            }
            //查找患者信息
            else{
                list = accountManager.GetMarkListFromIdenno(this.cmbIdCardType.SelectedItem.ID, this.txtIdCardNO.Text.Trim(),
                                                                          this.cardTypeObj.ID);
            }
            
            if (list == null)
            {
                MessageBox.Show(accountManager.Err);
                return -1;
            }
            if (list.Count > 0)
            {
                string cardNO = list[0].Patient.PID.CardNO;
                this.ucRegPatientInfo1.CardNO = cardNO;
                this.Patient = managerIntegrate.QueryComPatientInfo(cardNO);
            }
            else
            {
                ////先获取健康卡类型和卡号
                ///*FS.HISFC.BizLogic.HealthCard.HealthCard*/ healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();
                //healthCard.DepartmentCode = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                //healthCard.IdType = this.cmbIdCardType.SelectedItem.ID;
                //healthCard.Id = this.txtIdCardNO.Text.Trim();
                //if (healthCardManager.HadCard(healthCard) == 4)
                //{
                //    //从区域平台去健康卡的信息
                //    int getResult;
                //    getResult = healthCardManager.GetPatientInfo(healthCard);
                //    if (getResult == 3)
                //    {
                //        FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                //        accountCard.MarkNO = healthCard.CardNumber;
                //        accountCard.MarkType.ID = "Health_CARD";//healthCard.CardType;
                //        accountCard.MarkStatus = MarkOperateTypes.Begin;
                //        accountCard.ReFlag = "0";
                //        accountCard.CreateOper.ID = accountManager.Operator.ID;

                //        try
                //        {
                //            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //            string error = "";
                //            if (this.BulidCard(accountCard, ref error) == -1)
                //            {
                //                FS.FrameWork.Management.PublicTrans.RollBack();
                //                MessageBox.Show("建立患者信息失败，原因：" + error);
                //                return -1;
                //            }

                //            if (accountManager.InsertIdenInfo(this.Patient) == -1)
                //            {
                //                if (this.accountManager.DBErrCode == 1)
                //                {
                //                    if (accountManager.UpdateIdenInfo(this.Patient) == -1)
                //                    {
                //                        FS.FrameWork.Management.PublicTrans.RollBack();
                //                        MessageBox.Show("插入证件号码表出错！" + accountManager.Err);
                //                        return -1;
                //                    }
                //                }
                //            }

                //            //加入照片
                //            if (healthCard.Photo != null)
                //            {
                //                if (accountManager.UpdatePhoto(this.Patient, healthCard.Photo) == -1)
                //                {
                //                    FS.FrameWork.Management.PublicTrans.RollBack();
                //                    MessageBox.Show("更新图片出错！" + accountManager.Err);
                //                    return -1;
                //                }
                //            }

                //            FS.FrameWork.Management.PublicTrans.Commit();
                //        }
                //        catch (Exception ex)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("获取患者信息失败，请重试或者去发卡界面录入患者信息！");
                //            return -1;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("获取患者信息失败，请重试或者去发卡界面录入患者信息！");
                //        return -1;
                //    }

                //    //成功取得患者

                //    list = accountManager.GetMarkListFromIdenno(this.cmbIdCardType.SelectedItem.ID, this.txtIdCardNO.Text.Trim(),
                //                                                                  this.cardTypeObj.ID);
                //    if (list == null)
                //    {
                //        MessageBox.Show(accountManager.Err);
                //        return -1;
                //    }
                //    if (list.Count > 0)
                //    {
                //        string cardNO = list[0].Patient.PID.CardNO;
                //        this.ucRegPatientInfo1.CardNO = cardNO;
                //        this.Patient = managerIntegrate.QueryComPatientInfo(cardNO);
                //    }

                //    //this.ucRegPatientInfo1.CardNO = this.Patient.PID.CardNO;
                //}
                //else
                //{
                //    MessageBox.Show("获取患者信息失败，请确定信息录入是否正确！");
                //    return 1;
                //}
                
            }

            if (this.spcard.Rows.Count > 0)
            {
                this.spcard.Rows.Remove(0, spcard.Rows.Count);
            }

            int rowIndex = 0;
            foreach (HISFC.Models.Account.AccountCard tempCard in list)
            {
                this.spcard.Rows.Add(this.spcard.Rows.Count, 1);
                rowIndex = this.spcard.Rows.Count - 1;
                this.spcard.Cells[rowIndex, 0].Text = tempCard.MarkNO;
                this.spcard.Cells[rowIndex, 1].Text = markHelper.GetName(tempCard.MarkType.ID);
                this.spcard.Cells[rowIndex, 2].Text = tempCard.IsValid.ToString();
            }
            return 1;
        }



        /// <summary>
        /// 回车事件焦点的转换
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((!(ActiveControl is Button) && keyData == Keys.Enter))
            {
                if (this.ActiveControl.Name == "txtIdCardNO")
                {
                    this.QueryPatientInfo();
                    return true;
                }
         
            }
            //cmbIdCardType
            return false;
        }
        #endregion

        #region 事件

        private void txtMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Enter)
            //{
            //    QueryPaitentInfoByMarkNO();
            //}
        }

        private void ucHealthCardManager_Load(object sender, EventArgs e)
        {
            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            if (al == null)
            {
                MessageBox.Show("查找就诊卡类型失败");
                return;
            }
            markHelper.ArrayObject = al;
            foreach (NeuObject conObj in al)
            {
                if (conObj.Name.Contains("健康"))
                {
                    this.cardTypeObj = conObj;
                    break;
                }
            }
            this.cmbIdCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));

            this.ucRegPatientInfo1.IsLocalOperation = this.isLocalOperation;
            this.spcard.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("锁卡", "锁卡", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            toolbarService.AddToolButton("解锁", "解锁", FS.FrameWork.WinForms.Classes.EnumImageList.K开帐, true, false, null);
            toolbarService.AddToolButton("挂失", "挂失", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            toolbarService.AddToolButton("解挂失", "解挂失", FS.FrameWork.WinForms.Classes.EnumImageList.K开帐, true, false, null);
            toolbarService.AddToolButton("修改密码", "修改密码", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolbarService.AddToolButton("密码重置", "密码重置", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolbarService.AddToolButton("注销", "注销", FS.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null);
            return toolbarService;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.QueryPatientInfo();
        }


        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() == 1)
            {
                MessageBox.Show("更新患者信息成功");
            }
            else
            {
                MessageBox.Show("更新患者信息失败");
            }
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "锁卡":
                    {
                        this.LockCard();
                        break;
                    }
                case "解锁":
                    {
                        this.UnLockCard();
                        break;
                    }
                case "挂失":
                    {
                        this.LostCard();
                        break;
                    }
                case "解挂失":
                    {
                        this.UnLostCard();
                        break;
                    }
                case "修改密码":
                    {
                        this.ChangePassword();
                        break;
                    }
                case "密码重置":
                    {
                        this.ResetPassword();
                        break;
                    }
                case "注销":
                    {
                        this.LogoutCard();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void LockCard()
        {
            if (this.GetHealthCardNO())
            {
                RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();
                RHINCardServiceImplService.GeneralResponse generalResponse = cardService.lockCard(this.cardRequest);
                if (generalResponse.status.Equals("0"))
                {
                    MessageBox.Show("锁卡成功！");
                             
                }
                else
                {
                    MessageBox.Show("锁卡失败，请联系管理员！");
                }
            }
        }

        private void UnLockCard()
        {
            if (this.GetHealthCardNO())
            {
                RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();
                RHINCardServiceImplService.GeneralResponse generalResponse = cardService.unlockCard(this.cardRequest);
                if (generalResponse.status.Equals("0"))
                {
                    MessageBox.Show("解锁成功！");
                }
                else
                {
                    MessageBox.Show("解锁失败，请联系管理员！");
                }
            }
        }

        private void LostCard()
        {
            if (this.GetHealthCardNO())
            {
                RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();
                RHINCardServiceImplService.GeneralResponse generalResponse = cardService.lostCard(this.cardRequest);
                if (generalResponse.status.Equals("0"))
                {
                    MessageBox.Show("挂失成功！");
                }
                else
                {
                    MessageBox.Show("挂失失败，请联系管理员！");
                }
            }
        }

        private void UnLostCard()
        {
            if (this.GetHealthCardNO())
            {
                RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();
                RHINCardServiceImplService.GeneralResponse generalResponse = cardService.unlostCard(this.cardRequest);
                if (generalResponse.status.Equals("0"))
                {
                    MessageBox.Show("解挂失成功！");
                }
                else
                {
                    MessageBox.Show("解挂失失败，请联系管理员！");
                }
            }
        }

        private void ChangePassword()
        {
            if (this.GetHealthCardNO())
            {
                RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();

                RHINCardServiceImplService.PasswordRequestType passwordRequest = new RHINCardServiceImplService.PasswordRequestType();
                passwordRequest.authObject = this.cardRequest.authObject;
                passwordRequest.card = this.cardRequest.card;

                ucHealthCardEditPassWord uc = new ucHealthCardEditPassWord(true);
                FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);

                if (uc.OpResult)
                {
                    RHINCardServiceImplService.CheckPasswordRequestType checkPasswordRequest = new RHINCardServiceImplService.CheckPasswordRequestType();
                    checkPasswordRequest.authObject = this.cardRequest.authObject;
                    checkPasswordRequest.card = this.cardRequest.card;
                    checkPasswordRequest.card.passWord = uc.OldPwStr;

                    RHINCardServiceImplService.GeneralResponse generalResponse = cardService.checkPassword(checkPasswordRequest);
                    if (generalResponse.status.Equals("0"))
                    {
                        passwordRequest.newPassword = uc.PwStr;
                        RHINCardServiceImplService.GeneralResponse generalResponseChange = cardService.changePassword(passwordRequest);
                        if (generalResponseChange.status.Equals("0"))
                        {
                            MessageBox.Show("修改密码成功！");
                        }
                        else
                        {
                            MessageBox.Show("修改密码失败，请联系信息科！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("旧密码输入错误，请重新输入！");
                    }
                }
            }
        }

        private void ResetPassword()
        {
            if (this.GetHealthCardNO())
            {
                RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();

                RHINCardServiceImplService.PasswordRequestType passwordRequest = new RHINCardServiceImplService.PasswordRequestType();
                passwordRequest.authObject = this.cardRequest.authObject;
                passwordRequest.card = this.cardRequest.card;

                ucHealthCardEditPassWord uc = new ucHealthCardEditPassWord(false);
                FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                if (uc.OpResult)
                {
                    passwordRequest.newPassword = uc.PwStr;
                    RHINCardServiceImplService.GeneralResponse generalResponse = cardService.resetPassword(passwordRequest);
                    if (generalResponse.status.Equals("0"))
                    {
                        MessageBox.Show("重置密码成功！");
                    }
                    else
                    {
                        MessageBox.Show("重置密码失败，请联系信息科！");
                    }
                }
            }
        }

        private void LogoutCard()
        {
            if (this.GetHealthCardNO())
            {
                RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();
                RHINCardServiceImplService.GeneralResponse generalResponse = cardService.logoutCard(this.cardRequest);
                if (generalResponse.status.Equals("0"))
                {
                    MessageBox.Show("注销成功！");
                }
                else
                {
                    MessageBox.Show("注销失败，请联系信息科！");
                }
            }
        }

        private bool GetHealthCardNO()
        {

            if (this.spcard.Rows.Count == 0)
            {
                MessageBox.Show("未找到健康卡！");
                return false;
            }
            else if (this.spcard.Rows.Count == 1)
            {
                this.healthCardNo = this.spcard.Cells[0, 0].Text;
            }
            else
            {
                int activeRowIndex = this.spcard.ActiveRowIndex;
                if (activeRowIndex >= 0)
                {
                    this.healthCardNo = this.spcard.Cells[activeRowIndex, 0].Text;
                }
                else
                {
                    MessageBox.Show("请选择健康卡！");
                    return false;
                }
            }
            this.accountCard = this.accountManager.GetAccountCard(healthCardNo, this.cardTypeObj.ID);
            this.Patient = this.radtIntegrate.QueryComPatientInfo(this.accountCard.Patient.PID.CardNO);
            List<AccountCard> listAccountCard = new List<AccountCard>();
            listAccountCard = this.accountManager.GetMarkList(this.accountCard.Patient.PID.CardNO, this.cardTypeObj.ID, "1");

            this.cardRequest = new RHINCardServiceImplService.CardRequestType();
            RHINCardServiceImplService.CardType cardType = new RHINCardServiceImplService.CardType();
            cardType.type = "0";
            cardType.number = this.accountCard.MarkNO;
            //cardType.verfyNumber = this.accountCard.SecurityCode;
            if (listAccountCard != null && listAccountCard.Count > 0)
            {
                cardType.verfyNumber = listAccountCard[0].SecurityCode;
            }         
            RHINCardServiceImplService.CardType idCardType = new RHINCardServiceImplService.CardType();
            idCardType.type = this.Patient.IDCardType.ID;
            idCardType.number = this.Patient.IDCard;

            RHINCardServiceImplService.SimplePersonType applyPerson = new RHINCardServiceImplService.SimplePersonType();
            applyPerson.id = idCardType;
            applyPerson.name = this.Patient.Name;

            cardRequest.authObject = this.getAuthObject();
            cardRequest.card = cardType;
            cardRequest.applyPerson = applyPerson;

            return true;
        }

        private RHINCardServiceImplService.CommonCardAuthObject getAuthObject()
        {
            RHINCardServiceImplService.CommonCardAuthObject authObject = new RHINCardServiceImplService.CommonCardAuthObject();
            authObject.InstitutionCode = "455350760";
            authObject.departmentCode = "0001";
            //				authObject.staffNo = var.User.ID;
            //				authObject.Name = var.User.Name;
            authObject.staffNo = "0001";
            authObject.Name = "测试";
            authObject.role = "455350760_001";
            authObject.passWord = "888888";
            return authObject;
        }

        private string getPlatformMaritalStatus(string maritalStatus)
        {
            string result = string.Empty;
            if (maritalStatus == "S")
            {
                result = "10";
            }
            else if (maritalStatus == "M")
            {
                result = "20";
            }
            else if (maritalStatus == "W")
            {
                result = "30";
            }
            else if (maritalStatus == "D")
            {
                result = "40";
            }
            else if (maritalStatus == "R")
            {
                result = "90";
            }
            else if (maritalStatus == "A")
            {
                result = "90";
            }
            return result;
        }

        private RHINCardServiceImplService.PersonType getPersonType(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            RHINCardServiceImplService.PersonType personType = new RHINCardServiceImplService.PersonType();

            RHINCardServiceImplService.CardType idCardType = this.getIDCardType(accountCard);

            //				personType.cards = new RHINCardServiceImplService.CardType[] { cardType };
            personType.ids = new RHINCardServiceImplService.CardType[] { idCardType };

            personType.name = accountCard.Patient.Name;
            personType.gender = this.getPlatformSexCode(accountCard.Patient.Sex.ID.ToString());
            personType.birthDaySpecified = true;
            personType.birthDay = accountCard.Patient.Birthday;
            personType.nationality = "";
            personType.nation = "";
            personType.maritalStatus = this.getPlatformMaritalStatus(accountCard.Patient.MaritalStatus.ID.ToString());
            personType.educationLevel = "";
            personType.occupationalCategory = "";
            personType.telephoneNumber = accountCard.Patient.PhoneHome;
            personType.mobilePhoneNumber = accountCard.Patient.PhoneHome;
            personType.emailAddress = accountCard.Patient.Email;

            RHINCardServiceImplService.AddressType addressOfResidence = new RHINCardServiceImplService.AddressType();
            addressOfResidence.houseNumber = accountCard.Patient.AddressHome;
            personType.addressOfResidence = addressOfResidence;
            //			personType.addressRegisteredResidence  = "";

            RHINCardServiceImplService.ContactPersonType contactPersonType = new RHINCardServiceImplService.ContactPersonType();
            contactPersonType.Name = accountCard.Patient.Kin.Name;
            contactPersonType.relationship = "";
            //			contactPersonType.ids = "";
            contactPersonType.telephoneNumber = accountCard.Patient.Kin.RelationPhone;
            contactPersonType.mobilePhoneNumber = accountCard.Patient.Kin.RelationPhone;
            RHINCardServiceImplService.AddressType addressOfContact = new RHINCardServiceImplService.AddressType();
            addressOfContact.houseNumber = accountCard.Patient.Kin.RelationAddress;
            contactPersonType.Address = addressOfContact;
            personType.contactPerson = contactPersonType;

            RHINCardServiceImplService.EmployerType employerType = new RHINCardServiceImplService.EmployerType();
            employerType.name = accountCard.Patient.CompanyName;
            //			employerType.address = "";
            employerType.telephoneNumber = accountCard.Patient.PhoneBusiness;
            personType.asEmployer = employerType;

            return personType;
        }

        private RHINCardServiceImplService.CardType getIDCardType(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            RHINCardServiceImplService.CardType idCardType = new RHINCardServiceImplService.CardType();
            idCardType.type = accountCard.Patient.IDCardType.ID;
            idCardType.number = accountCard.Patient.IDCard;
            return idCardType;
        }

        private string getPlatformSexCode(string sexCode)
        {
            string result = string.Empty;
            if (sexCode == "U")
            {
                result = "0";
            }
            else if (sexCode == "M")
            {
                result = "1";
            }
            else if (sexCode == "F")
            {
                result = "2";
            }
            else if (sexCode == "O")
            {
                result = "9";
            }
            return result;
        }

        private int Save()
        {

            #region 处理本院数据
            if (!this.ucRegPatientInfo1.InputValid()) return 0;

            HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #region 更新患者基本信息
            int resultValue = radtIntegrate.RegisterComPatient(patient);
            if (resultValue <= 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存患者信息失败！" + accountManager.Err);
                return 0;
            }

            if (accountManager.InsertPatientPactInfo(patient) < 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存患者多个合同单位信息失败！" + accountManager.Err);
                return 0;
            }

            //更新最近一次挂号记录
            if (isCanEditLastRegInfo)
            {
                if (MessageBox.Show("是否修改最近一次挂号信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    resultValue = regIntegrate.UpdateRegByPatientInfo(patient);
                    if (resultValue < 0)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新挂号信息失败：" + regIntegrate.Err);
                        return 0;
                    }
                }
            }

            #endregion

            //变更记录由触发器代替

            resultValue = functionIntegrate.SaveChange<HISFC.Models.RADT.Patient>(false, false, patient.PID.CardNO, oldPatient, patient);
            if (resultValue < 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("生成变更记录失败！");
                return 0;
            }

 

            FS.FrameWork.Management.PublicTrans.Commit();

            string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");

          /*  if (register == "0")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存患者信息成功！"), FS.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
 
            }
           */
            #endregion 
            #region 健康卡处理
            if (this.GetHealthCardNO())
            {
                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                int returnValue = 0;
                returnValue = accountManager.GetCardByRule(this.accountCard.MarkNO, ref accountCard);

                if (returnValue == 1)
                {
                    if (accountCard.MarkType.ID.Contains("Health_CARD"))
                    {
                        //List<AccountCard> lstChangeCard = new List<AccountCard>();

                        RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();

                        RHINCardServiceImplService.QueryPersonRequestType queryPersonRequest = new RHINCardServiceImplService.QueryPersonRequestType();

                        RHINCardServiceImplService.CardType cardType = new RHINCardServiceImplService.CardType();
                        cardType.type = "0";
                        cardType.number = accountCard.MarkNO;
                        cardType.verfyNumber = accountCard.SecurityCode;

                        queryPersonRequest.authObject = this.getAuthObject();
                        queryPersonRequest.card = cardType;

                        RHINCardServiceImplService.PersonType personType = this.getPersonType(accountCard);
                        RHINCardServiceImplService.QuestPersonResponseType questPersonResponse = cardService.queryPerson(queryPersonRequest);

                        questPersonResponse = cardService.queryPerson(queryPersonRequest);
                        RHINCardServiceImplService.GeneralResponse generalResponse;


                        if (questPersonResponse.status.Equals("0"))
                        {
                            RHINCardServiceImplService.NewCardRequestType newCardRequest = new RHINCardServiceImplService.NewCardRequestType();
                            newCardRequest.authObject = this.getAuthObject();
                            newCardRequest.newCard = cardType;
                            newCardRequest.person = personType;
                            newCardRequest.secrecyLevel = "0";
                            newCardRequest.onlineEnquiry = "1";
                            newCardRequest.accessToEHR = "1";
                            generalResponse = cardService.updatePerson(newCardRequest);

                        }
                    }
                }
            
            }
            
            
            #endregion

            return 1;
        }



        

        #endregion 
    }
        
}
