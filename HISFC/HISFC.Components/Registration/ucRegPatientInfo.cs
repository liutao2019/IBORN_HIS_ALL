using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using System.Collections;


namespace FS.HISFC.Components.Registration
{
    public partial class ucRegPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucRegPatientInfo()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// Managerҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();


        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// Acountҵ���{A97E4C98-8820-45b9-9916-132D784D374B}
        /// </summary>
        //private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// 
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        //private FS.HISFC.Models.Account.PatientAccount patientInfo = null;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
        /// <summary>
        /// �������
        /// </summary>
        private string NormalName = string.Empty;

        /// <summary>
        /// ���￨��
        /// </summary>
        private string cardNO = string.Empty;
        ///// <summary>
        ///// �Ƿ��Ǹ��»�����Ϣ״̬
        ///// </summary>
        //private bool isUpdate = true;
        //�Ƿ���ʾ���水ť
        private bool isShowButton = true;
        /// <summary>
        /// �Ƿ��½����￨��
        /// </summary>
        private bool IsNewCardNo = true;
        /// <summary>
        /// ������
        /// </summary>
        private string cardType = string.Empty;

        /// <summary>
        /// ������
        /// </summary>
        private string markNO = string.Empty;
        
        /// <summary>
        /// ��ʵ��{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
       // private FS.HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// ������ʵ��{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        //private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private bool isBulidCard = false;


        /// <summary>
        /// �Ƿ�ͬʱ���¹Һ���Ϣ
        /// </summary>
        private bool updateRegInfo = false;

        private bool isSDYB = false;

        private int validDays = -1;

        #endregion

        #region ����
        /// <summary>
        /// ���￨��
        /// </summary>
        public string CardNO
        {
            set
            {
                if (this.DesignMode) return;
                cardNO = value;
                if (value == string.Empty)
                    IsNewCardNo = true;
                else
                {
                    IsNewCardNo = false;
                    SetInfo(cardNO);
                }
            }
            get
            {
                return cardNO;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string MarkNO
        {
            get
            {
                return markNO;
            }
            set
            {
                markNO = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string CardType
        {
            get
            {
                return cardType;
            }
            set
            {
                cardType = value;
            }
        }


        /// <summary>
        /// �Ƿ�ͬʱ���¹Һ���Ϣ
        /// </summary>        
        public bool UpdateRegInfo
        {
            get { return this.updateRegInfo; }
            set { this.updateRegInfo = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ���水ť
        /// </summary>
        public bool IsShowButton
        {
            get
            {
                return isShowButton;
            }
            set
            {
                isShowButton = value;
            }
        }

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public bool IsBulidCard
        {
            set
            {
                isBulidCard = value;
            }
        }

        /// <summary>
        /// �Һ���Ч����
        /// </summary>
        public int ValidDays
        {
            set
            {
                validDays = value;
            }
        }

        #endregion

        #region �¼�
        public delegate void SetCardNO(string cardNO);
        /// <summary>
        /// �����껼�߻�����Ϣ��CardNo����ucAccount
        /// </summary>
        public event SetCardNO OnSetCardNO;
        #endregion

        #region ����
        /// <summary>
        /// �������
        /// </summary>
        protected virtual void Clear()
        {
            this.txtName.Text = string.Empty;
            this.txtIDNO.Text = string.Empty;
            this.cmbMarry.Text = string.Empty;
            this.cmbPact.Text = string.Empty;
            this.cmbArea.Text = string.Empty;
            this.cmbCountry.Text = string.Empty;
            this.cmbProfession.Text = string.Empty;
            this.txtLinkMan.Text = string.Empty;
            this.cmbRelation.Text = string.Empty;
            this.cmbLinkAddress.Text = string.Empty;
            this.txtHomePhone.Text = string.Empty;
            this.txtWorkPhone.Text = string.Empty;
            this.cmbArea.Text = string.Empty;
            this.txtName.Enabled = true;
            this.txtIDNO.Enabled = true;
            this.cmbHomeAddress.Text = string.Empty;
            this.cmbWorkAddress.Text = string.Empty;
            //this.txtAge.Text = string.Empty;
            //this.txtAge.ReadOnly = true;
            this.txtHomePhone.Text = string.Empty;
            this.txtLinkPhone.Text = string.Empty;
            this.NormalName = string.Empty;
            this.ckEncrypt.Checked = false;
            this.isSDYB = false;
            this.neuSpread2_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            try
            {
                //�Ա��б�
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
                this.cmbSex.Text = "��";

                //����
                this.cmbNation.AddItems(managerIntegrate.GetConstantList(EnumConstant.NATION));
                this.cmbNation.Text = "����";

                //����״̬

                this.cmbMarry.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());

                //����
                this.cmbCountry.AddItems(managerIntegrate.GetConstantList(EnumConstant.COUNTRY));
                this.cmbCountry.Text = "�й�";

                //ְҵ��Ϣ
                this.cmbProfession.AddItems(managerIntegrate.GetConstantList(EnumConstant.PROFESSION));

                //������λ
                this.cmbWorkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��ϵ����Ϣ

                this.cmbRelation.AddItems(managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //��ϵ�˵�ַ��Ϣ
                this.cmbLinkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��ͥסַ��Ϣ
                this.cmbHomeAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //����
                this.cmbDistrict.AddItems(managerIntegrate.GetConstantList(EnumConstant.DIST));


                //����
                //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                //this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//��������
                this.dtpBirthDay.Value = this.regMgr.GetDateTimeFromSysDateTime().AddYears(1);//��������

                //����
                this.cmbArea.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPact.AddItems(managerIntegrate.GetConstantList(EnumConstant.PACTUNIT));
                this.cmbPact.AddItems(this.feeIntegrate.QueryPactUnitOutPatient());
                this.cmbPact.IsListOnly = true;
                this.btSave.Visible = IsShowButton;
                //֤������
                this.cmbCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
                if(cmbCardType.Items.Count>0)
                    this.cmbCardType.SelectedIndex = 0;

                if (validDays == -1)
                {
                    validDays = FS.FrameWork.Function.NConvert.ToInt32(managerIntegrate.QueryControlerInfo("MZ0014"));

                }
            }
            catch (Exception e)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message);

                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="cardno">���￨��</param>
        private void SetInfo(string cardno)
        {
            if (cardno == string.Empty) return;
            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //this.patientInfo = accountManager.GetPatientInfo(cardno);
            this.patientInfo = this.radtIntegrate.QueryComPatientInfo(cardNO);
            if (this.patientInfo != null)
            {
                SetPatient();
                //����������Ч��
                ArrayList arlRegInfo = this.regMgr.Query(cardNO, regMgr.GetDateTimeFromSysDateTime().AddDays(-validDays));
                this.addRegisterInfo(arlRegInfo);
            }
            else
            {
                this.Clear();
            }
        }

        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            string age = this.regMgr.GetAge(birthday, true);
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = age;
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// 
        private void SetPatient()
        {
            //{C3115ABF-3473-43c8-BB42-89E2D9947B53}
            if (!string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
            {
                //{D5ED0D2A-8928-488c-A1B5-B78693614E1A}
                //this.txtName.Text = this.patientInfo.Name;               //����
                this.txtName.Tag = this.patientInfo.DecryptName;         //��ʵ����   
                this.cmbSex.Text = this.patientInfo.Sex.Name;            //�Ա�
                this.cmbSex.Tag = this.patientInfo.Sex.ID;               //�Ա�
                this.cmbPact.Text = this.patientInfo.Pact.Name;          //��ͬ��λ����
                this.cmbPact.Tag = this.patientInfo.Pact.ID;             //��ͬ��λID
                this.cmbPact.IsListOnly = true;
                this.cmbArea.Tag = this.patientInfo.AreaCode;            //����
                this.cmbCountry.Tag = this.patientInfo.Country.ID;       //����
                this.cmbNation.Tag = this.patientInfo.Nationality.ID;    //����
                //{2A19B7BA-453A-4c09-9F9B-5E7D3DA74E92}
                if (this.patientInfo.Birthday != DateTime.MinValue)
                {
                    this.dtpBirthDay.Value = this.patientInfo.Birthday;      //��������
                    //{A97E4C98-8820-45b9-9916-132D784D374B}
                    //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//����
                    //this.txtAge.Text = this.regMgr.GetAge(this.patientInfo.Birthday);//����
                    this.setAge(this.patientInfo.Birthday);//��ʾ����
                }
                //this.dtpBirthDay.Value = this.patientInfo.Birthday;      //��������
                //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//����
                this.cmbDistrict.Text = this.patientInfo.DIST;            //����
                this.cmbProfession.Tag = this.patientInfo.Profession.ID; //ְҵ
                this.txtIDNO.Text = this.patientInfo.IDCard;             //���֤��
                this.cmbWorkAddress.Text = this.patientInfo.CompanyName; //������λ
                this.txtWorkPhone.Text = this.patientInfo.PhoneBusiness; //��λ�绰
                this.cmbMarry.Tag = this.patientInfo.MaritalStatus.ID.ToString();//����״��
                this.cmbHomeAddress.Text = this.patientInfo.AddressHome;  //��ͥסַ
                this.txtHomePhone.Text = this.patientInfo.PhoneHome;     //��ͥ�绰
                this.txtLinkMan.Text = this.patientInfo.Kin.Name;        //��ϵ�� 
                this.cmbRelation.Tag = this.patientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
                this.cmbLinkAddress.Text = this.patientInfo.Kin.RelationAddress;//��ϵ�˵�ַ
                this.txtLinkPhone.Text = this.patientInfo.Kin.RelationPhone;//��ϵ�˵绰
                this.ckEncrypt.Checked = this.patientInfo.IsEncrypt; //�Ƿ��������
                this.txtMCardNO.Text = this.patientInfo.SSN;//ҽ��֤��
                if (this.ckEncrypt.Checked)
                {
                    this.NormalName = this.patientInfo.NormalName; //����
                    //{D5ED0D2A-8928-488c-A1B5-B78693614E1A}
                    this.txtName.Text = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.NormalName);
                }
                else
                {
                    this.txtName.Text = this.patientInfo.Name;
                }

                if (patientInfo.Pact.ID == "2" || patientInfo.Pact.ID == "3")
                {
                    this.isSDYB = true;
                }
                else
                {
                    this.isSDYB = false;
                }
                
            }
        }

        /// <summary>
        /// ���ݺ���У��
        /// </summary>
        /// <returns></returns>
        protected virtual bool InputValid()
        {
            if (this.txtName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뻼����������������Ϊ�գ�"));
                this.txtName.Focus();
                return false;
            }

            if (this.cmbSex.Tag.ToString() == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뻼���Ա��Ա���Ϊ�գ�"));
                this.cmbSex.Focus();
                return false;
            }

            if (this.cmbPact.Tag.ToString() == string.Empty)
            {
                MessageBox.Show(Language.Msg("�������ͬ��λ����ͬ��λ����Ϊ�գ�"));
                this.cmbPact.Focus();
                return false;
            }

            if (this.cmbPact.Tag.ToString()=="2"||this.cmbPact.Tag.ToString()=="3")
            {
                if (!this.isSDYB)
                {
                    MessageBox.Show(Language.Msg("��Ҫ����ͬ��λ��Ϊҽ�������¹Һţ�"));
                    this.cmbPact.Focus();
                    return false;
                }
            }

            if (!this.ckEncrypt.Checked && this.txtName.Text=="******")
            {
                MessageBox.Show(Language.Msg("�û�������û�м��ܣ���������ȷ�Ļ���������"));
                this.txtName.Focus();
                this.txtName.SelectAll();
                return false;
            }
            //�жϵ�λ�绰����
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWorkPhone.Text, 25))
            {
                MessageBox.Show(Language.Msg("��λ�绰���볤�ȹ���"));
                this.txtWorkPhone.Focus();
                return false;
            }
            //�жϼ�ͥ�绰���볤��
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomePhone.Text, 25))
            {
                MessageBox.Show(Language.Msg("��ͥ�绰���볤�ȹ���"));
                this.txtHomePhone.Focus();
                return false;
            }
            if (this.cmbCardType.SelectedItem!=null && this.cmbCardType.SelectedItem.Name=="���֤")
            {

                //{FD1FD98C-1997-42a4-A046-3EFB15DCA804}
                string errText = string.Empty;
                if ((this.txtIDNO.Text.Trim() != null && this.txtIDNO.Text.Trim() != ""))
                {
                    int returnValue = this.ProcessIDENNO(this.txtIDNO.Text.Trim(), EnumCheckIDNOType.Saveing);
                    if (returnValue < 0)
                    {
                        return false;
                    }
                }

            }


            //�ж���ϵ�绰���볤��
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("��ϵ�˵绰���볤�ȹ���"));
                this.txtLinkPhone.Focus();
                return false;
            }

            //�ж���ϵ�绰���볤��
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text, 16))
            {
                MessageBox.Show(Language.Msg("�������ȹ���"));
                this.txtName.Focus();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkMan.Text, 10))
            {
                MessageBox.Show(Language.Msg("��ϵ����������"));
                this.txtLinkMan.Focus();
                return false;
            }
            //�ж��Ա�
            if (this.cmbSex.Text.Trim() == "")
            {
                MessageBox.Show(Language.Msg("�Ա���Ϊ�գ��������Ա�"));
                this.cmbSex.Focus();
                return false;
            }
            //this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//��������
            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //if (this.dtpBirthDay.Value > this.accountManager.GetDateTimeFromSysDateTime().Date)
            if (this.dtpBirthDay.Value > this.regMgr.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show(Language.Msg("�������ڴ��ڵ�ǰ����,����������!"));
                this.dtpBirthDay.Focus();

                return false;
            }

           
            return true;
        }


        /// <summary>
        /// ��ʾ�ҺŻ�����Ϣ
        /// </summary>
        /// <param name="registerList"></param>
        private void addRegisterInfo(ArrayList registerList)
        {
            //�����
            this.neuSpread2_Sheet1.Rows.Count = 0;

            if (registerList == null || registerList.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < registerList.Count; i++)
            {
                FS.HISFC.Models.Registration.Register reg = registerList[i] as FS.HISFC.Models.Registration.Register;
                if (reg == null)
                {
                    return;
                }

                this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.Rows.Count, 1);
                int index = this.neuSpread2_Sheet1.Rows.Count - 1;

                this.neuSpread2_Sheet1.SetValue(index, 0, i == registerList.Count - 1 ? updateRegInfo : reg.IsSee, false);
                this.neuSpread2_Sheet1.Cells[index, 0].Locked = !updateRegInfo;
                this.neuSpread2_Sheet1.SetValue(index, 1, reg.InvoiceNO, false);
                this.neuSpread2_Sheet1.SetValue(index, 2, reg.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 3, reg.DoctorInfo.SeeDate.ToString(), false);
                this.neuSpread2_Sheet1.SetValue(index, 4, reg.DoctorInfo.Templet.RegLevel.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 5, reg.DoctorInfo.Templet.Dept.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 6, reg.DoctorInfo.Templet.Doct.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 7, reg.RegLvlFee.RegFee + reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);
                this.neuSpread2_Sheet1.SetValue(index, 8, reg.IsSee, false);
                this.neuSpread2_Sheet1.Rows[index].Tag = reg;

            }

        }

       
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <param name="patient"></param>
        //private void GetPatienName(FS.HISFC.Models.Account.PatientAccount patient)
        private void GetPatienName(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //ѡ�����
            if (ckEncrypt.Checked)
            {
                string encryptStr = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Tag.ToString());
                //�ڸ���
                if (!IsNewCardNo)
                {
                    //�������Ϊ��
                    if (this.NormalName == string.Empty)
                    {
                        patient.Name = "******";
                        patient.NormalName = encryptStr;
                    }
                    else
                    {
                        if (encryptStr == this.NormalName)
                        {
                            if (this.txtName.Text == "******")
                            {
                                patient.Name = this.txtName.Text;
                                patient.NormalName = encryptStr;
                            }
                            else
                            {
                                patient.Name = "******";
                                patient.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Text.Trim());
                            }
                        }
                        else
                        {
                            patientInfo.Name = "******";
                            patientInfo.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Text.Trim());
                        }
                    }
                }
                else
                {
                    patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Text.Trim());
                }
            }
            else
            {
                patientInfo.Name = this.txtName.Text;
            }
        }

        /// <summary>
        /// ��û���ʵ��
        /// </summary>
        /// <returns></returns>
         
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        //private FS.HISFC.Models.Account.PatientAccount GetPatientInfomation()
        private FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation()
        {

            //patientInfo = new FS.HISFC.Models.Account.PatientAccount();
            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo.PID.CardNO = cardNO;//���￨��
            patientInfo.Pact.ID = this.cmbPact.Tag.ToString();//��ͬ��λ  
            patientInfo.Pact.Name = this.cmbPact.Text.ToString();//��ͬ��λ����
            this.GetPatienName(patientInfo); //��������       
            patientInfo.Sex.ID = this.cmbSex.Tag.ToString();//�Ա�
            patientInfo.AreaCode = this.cmbArea.Tag.ToString();//����
            patientInfo.Country.ID = this.cmbCountry.Tag.ToString();//����
            patientInfo.Nationality.ID = this.cmbNation.Tag.ToString();//����
            patientInfo.Birthday = this.dtpBirthDay.Value;//��������
            patientInfo.DIST = this.cmbDistrict.Text.ToString();//����
            patientInfo.Profession.ID = this.cmbProfession.Tag.ToString();//ְҵ
            patientInfo.IDCard = this.txtIDNO.Text;//֤����
            patientInfo.IDCardType.ID = this.cmbCardType.Tag.ToString();//֤������
            patientInfo.CompanyName = this.cmbWorkAddress.Text;//������λ
            patientInfo.PhoneBusiness = this.txtHomePhone.Text;//��λ�绰
            patientInfo.PhoneBusiness = this.txtWorkPhone.Text;//��λ�绰
            patientInfo.MaritalStatus.ID = this.cmbMarry.Tag.ToString();//����״�� 
            patientInfo.AddressHome = this.cmbHomeAddress.Text.ToString();//��ͥסַ
            patientInfo.PhoneHome = this.txtHomePhone.Text;//��ͥ�绰
            patientInfo.Kin.Name = this.txtLinkMan.Text;//��ϵ�� 
            patientInfo.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//����ϵ�˹�ϵ
            patientInfo.Kin.RelationAddress = this.cmbLinkAddress.Text;//��ϵ�˵�ַ
            patientInfo.Kin.RelationPhone = this.txtLinkPhone.Text;  //��ϵ�˵绰
            patientInfo.Kin.User01 = this.cmbLinkAddress.Text;//��ϵ�˵�ַ
            patientInfo.Kin.Memo = this.txtLinkPhone.Text; //��ϵ�˵绰
            //patientInfo.Oper.ID = this.accountManager.Operator.ID; //����Ա
            //patientInfo.Oper.OperTime = this.accountManager.GetDateTimeFromSysDateTime();//����ʱ��
            patientInfo.IsEncrypt = this.ckEncrypt.Checked;
            patientInfo.SSN = this.txtMCardNO.Text;
         
            return patientInfo;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public virtual void save()
        {
            if (this.cardNO == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뿨�ţ�Ȼ���ڱ��汣�����ݣ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.InputValid())
            {
                return;
            }

            //�ĵ�����ʵ��
            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            // FS.HISFC.Models.Account.PatientAccount patientInfo = this.GetPatientInfomation();
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.GetPatientInfomation();
            if (patientInfo == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���߻�����Ϣ����Ϊ��"));
                return;
            }            

            #region ��������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new Transaction(this.accountManager.Connection);
            //trans.BeginTransaction();
            ////{DA67A335-E85E-46e1-A672-4DB409BCC11B
            //this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #endregion

            #region ����
            
            #endregion

            #region  ���滼����Ϣ
           

            int returnValue = 0;
            
            returnValue = radtIntegrate.RegisterComPatient(patientInfo);
            if (returnValue <= 0)
            { 
                //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                //MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ�ܣ�") + this.accountManager.Err);
                //FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ�ܣ�") + this.radtIntegrate.Err);
               
                return;
            }

            if (feeIntegrate.UpdatePatientIden(patientInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ�ܣ�") + this.feeIntegrate.Err);
                return;
            }

            //�������һ�ιҺż�¼
            if (this.neuSpread2_Sheet1.RowCount > 0 && updateRegInfo)
            {
                for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread2_Sheet1.Cells[i, 0].Value))
                    {
                        FS.HISFC.Models.Registration.Register register = this.neuSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Registration.Register;
                        register.Name = patientInfo.Name;
                        register.Sex.ID = patientInfo.Sex.ID;
                        register.Birthday = patientInfo.Birthday;
                        register.IDCard = patientInfo.IDCard;
                        register.IDCardType = patientInfo.IDCardType;
                        register.SSN = patientInfo.SSN;
                        int resultValue = this.regMgr.UpdateRegInfo(register);
                        if (resultValue < 0)
                        {
                            FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�����Ʊ��Ϊ" + register.InvoiceNO + "�ĹҺ���Ϣʧ�ܣ�" + regMgr.Err);
                            return;
                        }

                        if (InterfaceManager.GetIADT() != null)
                        {
                            if (InterfaceManager.GetIADT().PatientInfo(patientInfo, register) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(this, "�޸Ļ�����Ϣʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + InterfaceManager.GetIADT().Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;

                            }
                        }

                    }
                }
            }
            else
            {
                if (InterfaceManager.GetIADT() != null)
                {
                    if (InterfaceManager.GetIADT().PatientInfo(patientInfo, new FS.HISFC.Models.Registration.Register()) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this, "�޸Ļ�����Ϣʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + InterfaceManager.GetIADT().Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;

                    }
                }
            }

            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            //��ʾ������Ϣ
            this.SetInfo(cardNO);
            //��cardno��ucaccount
            if (OnSetCardNO != null)
                OnSetCardNO(cardNO);
            IsNewCardNo = false;
            
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ݳɹ���"), FS.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Clear();
         }

        //��ӡ
        /// <summary>
         /// �����ӡ{D2F77BDA-F5E5-48fe-AB73-B7FE6D92E6E2}
        /// </summary>
        public void PrintBar()
        {
            FS.HISFC.BizProcess.Interface.Registration.IPrintBar ip = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar))
            as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
            if (ip == null)//Ĭ��ʵ�ִ�ӡ
            {
                if(string.IsNullOrEmpty(CardNO))
                {
                    MessageBox.Show("������Ϊ�գ����ܴ�ӡ");
                    return;
                }

                FS.FrameWork.WinForms.Controls.ucBaseControl uc = new FS.FrameWork.WinForms.Controls.ucBaseControl();
                FS.FrameWork.WinForms.Controls.NeuPictureBox p = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
                p.Image = FS.FrameWork.WinForms.Classes.CodePrint.GetCode39(CardNO);
                FS.FrameWork.WinForms.Controls.NeuPanel pn = new FS.FrameWork.WinForms.Controls.NeuPanel();
                pn.Controls.Add(p);
                pn.BackColor = Color.White;
                uc.Controls.Add(pn);
                uc.BackColor = Color.White;

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.PrintPage(0, 0, uc);
            }
            else //�ӿ�ʵ�ִ�ӡ
            {
                string errText = string.Empty;
                int returnValue = ip.printBar((patientInfo as FS.HISFC.Models.RADT.Patient), ref errText);
                if (returnValue < 0)
                {
                    MessageBox.Show(errText);
                    return;
                }
            }

        }

        #endregion

        #region �¼�
        private void ucPatientInfo_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.Init();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            ////{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //this.txtAge.Text = this.accountManager.GetAge(this.dtpBirthDay.Value);
            //this.txtAge.Text = this.regMgr.GetAge(this.dtpBirthDay.Value);
            this.setAge(this.dtpBirthDay.Value);
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            DateTime birthDay = this.regMgr.GetDateTimeFromSysDateTime();
            string ageStr = this.txtAge.Text.Trim();
            ageStr = ageStr.Replace("_", "");
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(0, ageStr.IndexOf("��")));
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("��") + 1, ageStr.IndexOf("��") - ageStr.IndexOf("��") - 1));
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("��") + 1, ageStr.IndexOf("��") - ageStr.IndexOf("��") - 1));
            birthDay = birthDay.AddYears(-iyear).AddMonths(-iMonth).AddDays(-iDay - 1);
            this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
            if (birthDay < dtpBirthDay.MinDate || birthDay > dtpBirthDay.MaxDate)
            {
                MessageBox.Show("��������������������룡");
                this.txtAge.Text = "  0�� 0�� 0��";
                this.txtAge.Focus();
                return;
            }
            this.dtpBirthDay.Value = birthDay;
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtIDNO.ContainsFocus)//{FD1FD98C-1997-42a4-A046-3EFB15DCA804}
                {
                    string idNO = txtIDNO.Text.Trim();
                    if (!string.IsNullOrEmpty(idNO))
                    {
                        int returnValue = this.ProcessIDENNO(idNO, EnumCheckIDNOType.BeforeSave);
                        if (returnValue < 0)
                        {
                            return false;
                        }

                    }
                }
                if (btSave.ContainsFocus)
                {
                    btSave_Click(this, null);
                }
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                System.Type[] t = new Type[1];

                t[1] = typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar);
                return t;
            }

        }

        #endregion

       
        /// <summary>
        /// {FD1FD98C-1997-42a4-A046-3EFB15DCA804}
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            string errText = string.Empty;

            //У�����֤��


            //{99BDECD8-A6FC-44fc-9AAA-7F0B166BB752}

            //string idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            string idNOTmp = string.Empty;
            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else
            {
                idNOTmp = idNO;
            }

            //У�����֤��
            int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);



            if (returnValue < 0)
            {
                MessageBox.Show(errText);
                this.txtIDNO.Focus();
                return -1;
            }
            if (!string.IsNullOrEmpty(errText))
            {
                string[] reurnString = errText.Split(',');
                if (enumType == EnumCheckIDNOType.BeforeSave)
                {
                    this.dtpBirthDay.Text = reurnString[1];
                    this.cmbSex.Text = reurnString[2];
                    //this.txtAge.Text = this.regMgr.GetAge(this.dtpBirthDay.Value);
                    this.setAge(this.dtpBirthDay.Value);
                    //this.cmbPayKind.Focus();
                }
                else
                {
                    if (this.dtpBirthDay.Value.Date != (FS.FrameWork.Function.NConvert.ToDateTime(reurnString[1])).Date)
                    {
                        MessageBox.Show("������������������֤�кŵ����ղ���");
                        this.dtpBirthDay.Focus();
                        return -1;
                    }

                    if (this.cmbSex.Text != reurnString[2])
                    {
                        MessageBox.Show("������Ա������֤�кŵ��Ա𲻷�");
                        this.cmbSex.Focus();
                        return -1;
                    }
                }
            }
            return 1;
        }

       
        /// <summary>
        /// �ж����֤//{FD1FD98C-1997-42a4-A046-3EFB15DCA804}���֤��Ϣ
        /// </summary>
        private enum EnumCheckIDNOType
        {
            /// <summary>
            /// ����֮ǰУ��
            /// </summary>
            BeforeSave = 0,

            /// <summary>
            /// ����ʱУ��
            /// </summary>
            Saveing
        }
    }
}
