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
    /// ����������
    /// </summary>
    public partial class ucHealthCardManagerGJ : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucHealthCardManagerGJ()
        {
            InitializeComponent();
            this.txtMarkNO.KeyDown+=new KeyEventHandler(txtMarkNO_KeyDown);
        }

        #region ����

        ///// <summary>
        ///// HealthCardҵ���
        ///// </summary>
        //private FS.HISFC.BizLogic.HealthCard.HealthCardManager healthCardManager = new FS.HISFC.BizLogic.HealthCard.HealthCardManager();

        ///// <summary>
        ///// ������ʵ��
        ///// </summary>
        //private FS.HISFC.BizLogic.HealthCard.HealthCard healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();

        /// <summary>
        /// Managerҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        
        /// <summary>
        /// Acountҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// ������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper markHelper = new FS.FrameWork.Public.ObjectHelper();
        
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// ������������Ƿ�̬���һ�����Ϣ
        /// </summary>
        private bool isSelectPatientByEnter = true;

        /// <summary>
        /// �����Ƿ�ֻ�ڱ��ش��������������ķ���
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
        /// �����¼ҵ���
        /// </summary>
        HISFC.BizProcess.Integrate.Function functionIntegrate = new FS.HISFC.BizProcess.Integrate.Function();
        
        /// <summary>
        /// �ҺŹ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ϵͳ��������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
       
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private HISFC.Models.RADT.PatientInfo oldPatient = new FS.HISFC.Models.RADT.PatientInfo();
        
        /// <summary>
        /// �Ƿ�����޸����һ�ιҺż�¼
        /// </summary>
        private bool isCanEditLastRegInfo = false;
        
        #endregion

        #region ����

        #region �����������
        [Category("��������"), Description("�����Ƿ�������룡")]
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

        [Category("��������"), Description("�Ա��Ƿ�������룡")]
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

        [Category("��������"), Description("��������Ƿ�������룡")]
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

        [Category("��������"), Description("ҽ��֤���Ƿ�������룡")]
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

        [Category("��������"), Description("���������Ƿ�������룡")]
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

        [Category("��������"), Description("֤�������Ƿ�������룡")]
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

        [Category("��������"), Description("֤�����Ƿ�������룡")]
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
        /// ����ͬʱΪ����Ŀ  0 = ������ 1 = ���� ���֤��绰����
        /// </summary>
        [Category("�޸Ŀ���"), Description("����ͬʱΪ����Ŀ  0 = ������ 1 = ���� ���֤��绰����")]
        public int IMustInpubByOne
        {
            get { return this.ucRegPatientInfo1.IMustInpubByOne; }
            set { this.ucRegPatientInfo1.IMustInpubByOne = value; }

        }

        #endregion

        [Category("�ؼ�����"), Description("�Ƿ��ձ�¼����ת���뽹�� True:�� False:��")]
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

        [Category("�ؼ�����"),Description("������������Ƿ�̬���һ�����Ϣ True:�� False:��")]
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
        /// �����Ƿ�ֻ�ڱ��ش��������������ķ���
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        [Category("�ؼ�����"), Description("�����Ƿ�ֻ�ڱ��ش��������������ķ��� True:�� False:��")]
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

        [Category("�ؼ�����"), Description("false:���￨���������� true:���￨���������Ų�ͬ")]
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

        #region ����
        /// <summary>
        /// ��ѯ������Ϣ
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
            //���һ�����Ϣ
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
                ////�Ȼ�ȡ���������ͺͿ���
                ///*FS.HISFC.BizLogic.HealthCard.HealthCard*/ healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();
                //healthCard.DepartmentCode = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                //healthCard.IdType = this.cmbIdCardType.SelectedItem.ID;
                //healthCard.Id = this.txtIdCardNO.Text.Trim();
                //if (healthCardManager.HadCard(healthCard) == 4)
                //{
                //    //������ƽ̨ȥ����������Ϣ
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
                //                MessageBox.Show("����������Ϣʧ�ܣ�ԭ��" + error);
                //                return -1;
                //            }

                //            if (accountManager.InsertIdenInfo(this.Patient) == -1)
                //            {
                //                if (this.accountManager.DBErrCode == 1)
                //                {
                //                    if (accountManager.UpdateIdenInfo(this.Patient) == -1)
                //                    {
                //                        FS.FrameWork.Management.PublicTrans.RollBack();
                //                        MessageBox.Show("����֤����������" + accountManager.Err);
                //                        return -1;
                //                    }
                //                }
                //            }

                //            //������Ƭ
                //            if (healthCard.Photo != null)
                //            {
                //                if (accountManager.UpdatePhoto(this.Patient, healthCard.Photo) == -1)
                //                {
                //                    FS.FrameWork.Management.PublicTrans.RollBack();
                //                    MessageBox.Show("����ͼƬ����" + accountManager.Err);
                //                    return -1;
                //                }
                //            }

                //            FS.FrameWork.Management.PublicTrans.Commit();
                //        }
                //        catch (Exception ex)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("��ȡ������Ϣʧ�ܣ������Ի���ȥ��������¼�뻼����Ϣ��");
                //            return -1;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("��ȡ������Ϣʧ�ܣ������Ի���ȥ��������¼�뻼����Ϣ��");
                //        return -1;
                //    }

                //    //�ɹ�ȡ�û���

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
                //    MessageBox.Show("��ȡ������Ϣʧ�ܣ���ȷ����Ϣ¼���Ƿ���ȷ��");
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
        /// �س��¼������ת��
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

        #region �¼�

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
                MessageBox.Show("���Ҿ��￨����ʧ��");
                return;
            }
            markHelper.ArrayObject = al;
            foreach (NeuObject conObj in al)
            {
                if (conObj.Name.Contains("����"))
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
            toolbarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolbarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.K����, true, false, null);
            toolbarService.AddToolButton("��ʧ", "��ʧ", FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolbarService.AddToolButton("���ʧ", "���ʧ", FS.FrameWork.WinForms.Classes.EnumImageList.K����, true, false, null);
            toolbarService.AddToolButton("�޸�����", "�޸�����", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolbarService.AddToolButton("��������", "��������", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolbarService.AddToolButton("ע��", "ע��", FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
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
                MessageBox.Show("���»�����Ϣ�ɹ�");
            }
            else
            {
                MessageBox.Show("���»�����Ϣʧ��");
            }
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    {
                        this.LockCard();
                        break;
                    }
                case "����":
                    {
                        this.UnLockCard();
                        break;
                    }
                case "��ʧ":
                    {
                        this.LostCard();
                        break;
                    }
                case "���ʧ":
                    {
                        this.UnLostCard();
                        break;
                    }
                case "�޸�����":
                    {
                        this.ChangePassword();
                        break;
                    }
                case "��������":
                    {
                        this.ResetPassword();
                        break;
                    }
                case "ע��":
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
                    MessageBox.Show("�����ɹ���");
                             
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�����ϵ����Ա��");
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
                    MessageBox.Show("�����ɹ���");
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�����ϵ����Ա��");
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
                    MessageBox.Show("��ʧ�ɹ���");
                }
                else
                {
                    MessageBox.Show("��ʧʧ�ܣ�����ϵ����Ա��");
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
                    MessageBox.Show("���ʧ�ɹ���");
                }
                else
                {
                    MessageBox.Show("���ʧʧ�ܣ�����ϵ����Ա��");
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
                            MessageBox.Show("�޸�����ɹ���");
                        }
                        else
                        {
                            MessageBox.Show("�޸�����ʧ�ܣ�����ϵ��Ϣ�ƣ�");
                        }
                    }
                    else
                    {
                        MessageBox.Show("����������������������룡");
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
                        MessageBox.Show("��������ɹ���");
                    }
                    else
                    {
                        MessageBox.Show("��������ʧ�ܣ�����ϵ��Ϣ�ƣ�");
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
                    MessageBox.Show("ע���ɹ���");
                }
                else
                {
                    MessageBox.Show("ע��ʧ�ܣ�����ϵ��Ϣ�ƣ�");
                }
            }
        }

        private bool GetHealthCardNO()
        {

            if (this.spcard.Rows.Count == 0)
            {
                MessageBox.Show("δ�ҵ���������");
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
                    MessageBox.Show("��ѡ�񽡿�����");
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
            authObject.Name = "����";
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

            #region ����Ժ����
            if (!this.ucRegPatientInfo1.InputValid()) return 0;

            HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #region ���»��߻�����Ϣ
            int resultValue = radtIntegrate.RegisterComPatient(patient);
            if (resultValue <= 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���滼����Ϣʧ�ܣ�" + accountManager.Err);
                return 0;
            }

            if (accountManager.InsertPatientPactInfo(patient) < 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���滼�߶����ͬ��λ��Ϣʧ�ܣ�" + accountManager.Err);
                return 0;
            }

            //�������һ�ιҺż�¼
            if (isCanEditLastRegInfo)
            {
                if (MessageBox.Show("�Ƿ��޸����һ�ιҺ���Ϣ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    resultValue = regIntegrate.UpdateRegByPatientInfo(patient);
                    if (resultValue < 0)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���¹Һ���Ϣʧ�ܣ�" + regIntegrate.Err);
                        return 0;
                    }
                }
            }

            #endregion

            //�����¼�ɴ���������

            resultValue = functionIntegrate.SaveChange<HISFC.Models.RADT.Patient>(false, false, patient.PID.CardNO, oldPatient, patient);
            if (resultValue < 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���ɱ����¼ʧ�ܣ�");
                return 0;
            }

 

            FS.FrameWork.Management.PublicTrans.Commit();

            string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");

          /*  if (register == "0")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���滼����Ϣ�ɹ���"), FS.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
 
            }
           */
            #endregion 
            #region ����������
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
