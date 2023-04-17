using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using System.Xml;
using System.Collections;
using FS.HISFC.BizLogic.Fee;
using FS.FrameWork.Models;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    public partial class ucRegPatientInfoIBorn : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRegPatientInfoIBorn()
        {
            InitializeComponent();
        }

        #region �Զ����¼�

        /// <summary>
        /// ���������ComBoxʱ�������¼�
        /// </summary>
        public event HandledEventHandler CmbFoucs;

        /// <summary>
        /// ����ǰUC���㵽�����һ��ʱ�������¼�
        /// </summary>
        public event HandledEventHandler OnFoucsOver;

        /// <summary>
        /// �����뻼����Ϣ�س�ʱ���һ�����Ϣ
        /// </summary>
        public event HandledEventHandler OnEnterSelectPatient;

        #endregion

        #region ����

        #region ҵ������
        /// <summary>
        /// Managerҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �������ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// Acountҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// ���ת
        /// </summary>
        private HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        private PactUnitInfo pactManager = new PactUnitInfo();

        private List<string> pactList = new List<string>();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// ��ͬ��λ��֤�ӿ�
        /// </summary>
        MedcareInterfaceProxy medcareProxy = new MedcareInterfaceProxy();


        /// <summary>
        /// �����ӿ�{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen = null;
        public FS.HISFC.Models.RADT.PatientInfo showPatientInfo = null;

        public FS.HISFC.Models.Base.PactInfo PatientPactInfo = null;

        #endregion

        #region ����
        /// <summary>
        /// ����״̬ʵ��
        /// </summary>
        HISFC.Models.RADT.MaritalStatusEnumService maritalService = new FS.HISFC.Models.RADT.MaritalStatusEnumService();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// ���￨��
        /// </summary>
        private string cardNO = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper NationHelp = null;

        /// <summary>
        /// ֤������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = null;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool isTreatment = false;

        /// <summary>
        /// MPI�ӿ�
        /// </summary>
        //FS.HISFC.BizProcess.Interface.Platform.IEmpiCommutative iEmpi = null;
        /// <summary>
        /// �����Ƿ�ֻ�ڱ��ش��������������ķ���
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        private bool isLocalOperation = true;

        private string mcardNO = string.Empty;

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// ���뿨����� 0��ձ�ʾ�þ��￨����������  ������0Ϊ�࿨
        /// </summary>
        private bool cardWay = true;
        /// <summary>
        /// �쿨ʱ�Ƿ�ʵʱ�жϣ��Ƿ�������Ӧ�ĺ�ͬ��λ
        /// {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
        /// </summary>
        private bool isJudgePact = false;

        /// <summary>
        /// �������֤ʱ�ж������ҽ�����ͬ��λ���ҽ��
        /// </summary>
        private bool isJudgePactByIdno = false;

        /// <summary>
        /// �쿨ʱ�������֤ʱ�ж������ҽ�����ͬ��λ���ҽ��ʱ��ת���ĺ�ͬ��λ
        /// </summary>
        private string changePactId = "";

        /// <summary>
        /// card_no�������Ű쿨ʱ�Ƿ�������card_no
        /// </summary>
        private bool isAutoBuildCardNo = false;
        /// <summary>
        /// ҽ�����ID, ����ԡ�|���ֿ�
        /// {870D6A8C-9B17-4e33-A0B6-DB0F1CF15BAE}
        /// </summary>
        List<string> lstPactID = null;
        #endregion

        #region ������Ʊ���

        #region ����¼����Ŀ
        /// <summary>
        /// �Ƿ������������
        /// </summary>
        private bool isInputName = true;

        /// <summary>
        /// �Ƿ���������Ա�
        /// </summary>
        private bool isInputSex = false;

        /// <summary>
        /// �Ƿ���������ͬ��λ
        /// </summary>
        private bool isInputPact = false;

        /// <summary>
        /// �Ƿ��������ҽ��֤��
        /// </summary>
        private bool isInputSiNo = false;

        /// <summary>
        /// �Ƿ���������������
        /// </summary>
        private bool isInputBirthDay = false;

        /// <summary>
        /// �Ƿ��������֤������
        /// </summary>
        private bool isInputIDEType = false;

        /// <summary>
        /// �Ƿ��������֤����
        /// </summary>
        private bool isInputIDENO = false;

        /// <summary>
        /// �Ƿ����������ϵ�绰
        /// </summary>
        private bool isInputPHONE = false;


        /// <summary>
        /// �Ƿ�������������ϵ��
        /// </summary>
        private bool isInputLinkMan = true;

        /// <summary>
        /// �Ƿ�������������ϵ�˵绰
        /// </summary>
        private bool isInputLinkPhone = true;

        /// <summary>
        /// �Ƿ���밴˳����ת
        /// </summary>
        private bool isInSequentialOrder = false;


        /// <summary>
        ///���֤����ȷ��1��ʾ�Ƿ������2����ʾ��3����ʾ// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        private int isTooltipCARDID = 1;

        #endregion

        #region �Ƿ�����޸�
        /// <summary>
        /// ������Դ�Ƿ�����޸�
        /// </summary>
        private bool isEnablePact = true;

        /// <summary>
        /// ҽ��֤���Ƿ�����޸�
        /// </summary>
        private bool isEnableSiNO = true;

        /// <summary>
        /// �Ƿ�����޸�֤������
        /// </summary>
        private bool isEnableIDEType = true;

        /// <summary>
        /// �Ƿ�����޸�֤����
        /// </summary>
        private bool isEnableIDENO = true;

        /// <summary>
        /// Vip�Ƿ����
        /// </summary>
        private bool isEnableVip = true;

        /// <summary>
        /// ���������Ƿ����
        /// </summary>
        private bool isEnableEntry = true;
        #endregion

        /// <summary>
        /// ����¼��ؼ�
        /// </summary>
        private Hashtable InputHasTable = new Hashtable();

        /// <summary>
        /// �Ƿ�����޸Ŀؼ�
        /// </summary>
        private List<Control> EnableControlList = new List<Control>();

        /// <summary>
        /// �Ƿ��ձ�¼����ת���뽹��
        /// </summary>
        private bool isMustInputTabInde = false;

        /// <summary>
        /// ��Ժְ����ͬ��λ
        /// </summary>
        private string isValidHospitalStaff = "";

        /// <summary>
        /// ��������ؼ����TabIndex
        /// </summary>
        int inpubMaxTabIndex = 0;
        /// <summary>
        /// ����ͬʱΪ����Ŀ
        /// 0 = ������
        /// 1 = ���� ���֤��绰����
        /// </summary>
        private int iMustInpubByOne = 0;

        /// <summary>
        /// card_no�������Ű쿨ʱ�Զ����ɵ�card_no
        /// </summary>
        private string autoCardNo = "";

        ///// <summary>
        ///// ������������·��
        ///// </summary>
        //string filePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "/CasDeptDefaultValue.xml"; 
        #endregion

        #endregion

        #region ����
        /// <summary>
        /// �쿨ʱ�Ƿ�ʵʱ�жϣ��Ƿ�������Ӧ�ĺ�ͬ��λ
        /// {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
        /// </summary>
        public bool IsJudgePact
        {
            get { return isJudgePact; }
            set { isJudgePact = value; }
        }

        /// <summary>
        /// �������֤ʱ�ж������ҽ�����ͬ��λ���ҽ��
        /// </summary>
        public bool IsJudgePactByIdno
        {
            get
            {
                return isJudgePactByIdno;
            }
            set
            {
                isJudgePactByIdno = value;
            }
        }

        /// <summary>
        /// card_no�������Ű쿨ʱ�Ƿ�������card_no
        /// </summary>
        public bool IsAutoBuildCardNo
        {
            get
            {
                return isAutoBuildCardNo;
            }
            set
            {
                isAutoBuildCardNo = value;
            }
        }

        //card_no�������Ű쿨ʱ�Զ����ɵ�card_no
        public string AutoCardNo
        {
            get { return autoCardNo; }
            set { autoCardNo = value; }
        }
        /// <summary>
        /// ��ͬ��λ�ж�
        /// </summary>
        public String IsValidHospitalStaff
        {
            get
            {
                return this.isValidHospitalStaff;
            }
            set
            {
                this.isValidHospitalStaff = value;
            }
        }

        /// <summary>
        /// ���￨��
        /// </summary>
        public string CardNO
        {
            set
            {
                if (this.DesignMode) return;
                cardNO = value;
                if (value != string.Empty)
                {
                    SetInfo(cardNO);
                }
            }
            get
            {
                return cardNO;
            }
        }

        //������￨��
        public string McardNO
        {
            get { return mcardNO; }
            set { mcardNO = value; }
        }

        [Category("�ؼ�����"), Description("�Ƿ��﷢�� True:�� false:��")]
        public bool IsTreatment
        {
            get { return isTreatment; }
            set { isTreatment = value; }
        }

        [Category("�ؼ�����"), Description("�Ƿ��ձ�¼����ת���뽹��")]
        public bool IsMustInputTabIndex
        {
            get
            {
                return isMustInputTabInde;
            }
            set
            {
                isMustInputTabInde = value;
            }
        }


        private bool isJumpHomePhone = false;
        [Category("�ؼ�����"), Description("�Ƿ������ͥ��ַ��ֱ�������绰�ֶ�")]
        public bool IsJumpHomePhone
        {
            get { return isJumpHomePhone; }
            set { isJumpHomePhone = value; }
        }


        /// <summary>
        /// �����Ƿ�ֻ�ڱ��ش��������������ķ���
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
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

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        [Category("�ؼ�����"), Description("false:���￨���������� true:���￨���������Ų�ͬ")]
        public bool CardWay
        {
            get
            {
                return cardWay;
            }
            set
            {
                cardWay = value;
            }
        }
        /// <summary>
        /// ������������Ƿ�����ͨ������֤���Ŷ�̬���һ�����Ϣ True:�� False:��
        /// </summary>
        [Category("�ؼ�����"), Description("������������Ƿ�����ͨ������֤���Ŷ�̬���һ�����Ϣ True:�� False:��")]
        public bool IsSelectPatientByNameIDCardByEnter
        {
            get { return isSelectPatientByNameIDCardByEnter; }
            set { isSelectPatientByNameIDCardByEnter = value; }
        }
        /// <summary>
        /// ������������Ƿ�����ͨ������֤���Ŷ�̬���һ�����Ϣ
        /// </summary>
        private bool isSelectPatientByNameIDCardByEnter = false;

        private bool isMutilPactInfo = false;
        public bool IsMutilPactInfo
        {
            get
            {
                return this.isMutilPactInfo;
            }
            set
            {
                this.isMutilPactInfo = value;
                this.cmbPact.Visible = !this.isMutilPactInfo;
                this.comboBoxPactSelect1.Visible = this.isMutilPactInfo;
            }
        }
         
        private bool isMustInputIDNO = false;
        /// <summary>
        /// �Ƿ�����������֤��
        /// </summary>
        public bool IsMustInputIDNO
        {
            get
            {
                return this.isMustInputIDNO;
            }
            set
            {
                this.isMustInputIDNO = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblIDNO, this.txtIDNO, this.isMustInputIDNO);

            }
        }


        private bool isMustNowHome = false;
        /// <summary>
        /// �Ƿ����������סַ// {0AFA59AE-59B4-4140-83B1-54DA5B98ED5E}
        /// </summary>
        public bool IsMustNowHome
        {
            get
            {
                return this.isMustNowHome;
            }
            set
            {
                this.isMustNowHome = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblNowHome, this.txtNowHome, this.isMustNowHome);

            }
        }
        private FS.FrameWork.WinForms.Controls.NeuComboBox ComPact
        {
            get
            {
                return isMutilPactInfo ? this.comboBoxPactSelect1 : this.cmbPact;
            }
        }
        /// <summary>
        /// �Ƿ��޸Ļ�����Ϣģʽ
        /// </summary>
        public bool IsEditMode
        {
            get { return blnEditMode; }
            set
            {
                blnEditMode = value;
            }
        }
        private bool blnEditMode = false;

        #region �����������
        [Category("�ؼ�����"), Description("�����Ƿ�������룡")]
        public bool IsInputName
        {
            get
            {
                return isInputName;
            }
            set
            {
                isInputName = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblName, this.txtName, value);
            }
        }

        [Category("�ؼ�����"), Description("�Ա��Ƿ�������룡")]
        public bool IsInputSex
        {
            get
            {
                return isInputSex;
            }
            set
            {
                isInputSex = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblSex, this.cmbSex, value);
            }
        }

        [Category("�ؼ�����"), Description("��ͬ��λ�Ƿ�������룡")]
        public bool IsInputPact
        {
            get
            {
                return isInputPact;
            }
            set
            {
                isInputPact = value;
            }
        }

        [Category("�ؼ�����"), Description("ҽ��֤���Ƿ�������룡")]
        public bool IsInputSiNo
        {
            get { return isInputSiNo; }
            set
            {
                isInputSiNo = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblSiNO, this.txtSiNO, value);
            }
        }

        [Category("�ؼ�����"), Description("���������Ƿ�������룡")]
        public bool IsInputBirthDay
        {
            get { return isInputBirthDay; }
            set
            {
                isInputBirthDay = value;
                //this.AddOrRemoveUnitAtMustInputLists(this.lblBirthDay, this.dtpBirthDay, value);
                this.AddOrRemoveUnitAtMustInputLists(this.lblBirthDay, this.txtYear, value);
            }
        }

        [Category("�ؼ�����"), Description("֤�������Ƿ�������룡")]
        public bool IsInputIDEType
        {
            get { return isInputIDEType; }
            set
            {
                isInputIDEType = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblCardType, this.cmbCardType, value);
            }
        }

        [Category("�ؼ�����"), Description("֤�����Ƿ�������룡")]
        public bool IsInputIDENO
        {
            get { return isInputIDENO; }
            set
            {
                isInputIDENO = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblIDNO, this.txtIDNO, value);
            }
        }

        [Category("�ؼ�����"), Description("�绰�Ƿ�������룡")]
        public bool IsInputPHONE
        {
            get { return isInputPHONE; }
            set
            {
                isInputPHONE = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblHomePhone, this.txtHomePhone, value);
            }
        }

        [Category("�ؼ�����"), Description("������ϵ���Ƿ�������룡")]
        public bool IsInputLinkMan
        {
            get { return isInputLinkMan; }
            set
            {
                isInputLinkMan = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblLinkMan, this.txtLinkMan, value);
            }
        }
        [Category("�ؼ�����"), Description("������ϵ�˵绰�Ƿ�������룡")]
        public bool IsInputLinkPhone
        {
            get { return isInputLinkPhone; }
            set
            {
                isInputLinkPhone = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblLinkPhone, this.txtLinkPhone, value);
            }
        }
        [Category("�ؼ�����"), Description("�Ƿ�����->�绰->��ַ->�������룡")]
        public bool IsInSequentialOrder
        {
            get { return isInSequentialOrder; }
            set
            {
                isInSequentialOrder = value;
                this.ChangeTabIdex(value);
            }
        }

        [Category("�ؼ�����"), Description("���֤����ȷ�Ƿ���ʾ��1��ʾ�Ƿ������2����ʾ��3����ʾ")]
        public int IsTooltipCARDID
        {
            get
            {
                return isTooltipCARDID;
            }
            set
            {
                isTooltipCARDID = value;
            }
        }
        #endregion

        #region �Ƿ�����޸Ŀ���
        [Category("�޸Ŀ���"), Description("������Դ�Ƿ�����޸�")]
        public bool IsEnablePact
        {
            get { return isEnablePact; }
            set
            {
                isEnablePact = value;
                AddOrRemoveUnitAtEnableLists(this.ComPact, value);
            }
        }

        [Category("�޸Ŀ���"), Description("ҽ��֤���Ƿ�����޸�")]
        public bool IsEnableSiNO
        {
            get { return isEnableSiNO; }
            set
            {
                isEnableSiNO = value;
                AddOrRemoveUnitAtEnableLists(this.txtSiNO, value);
            }
        }

        [Category("�޸Ŀ���"), Description("�Ƿ�����޸�֤������")]
        public bool IsEnableIDEType
        {
            get { return isEnableIDEType; }
            set
            {
                isEnableIDEType = value;
                AddOrRemoveUnitAtEnableLists(this.cmbCardType, value);
            }
        }

        [Category("�޸Ŀ���"), Description("�Ƿ�����޸�֤����")]
        public bool IsEnableIDENO
        {
            get { return isEnableIDENO; }
            set
            {
                isEnableIDENO = value;
                AddOrRemoveUnitAtEnableLists(this.txtIDNO, value);
            }
        }

        [Category("�޸Ŀ���"), Description("�Ƿ�����޸�Vip��ʶ")]
        public bool IsEnableVip
        {
            get
            {
                return isEnableVip;
            }
            set
            {
                isEnableVip = value;
                AddOrRemoveUnitAtEnableLists(this.ckVip, value);
            }
        }

        [Category("�޸Ŀ���"), Description("�������������Ƿ�����޸�")]
        public bool IsEnableEntry
        {
            get
            {
                return isEnableEntry;
            }
            set
            {
                isEnableEntry = value;
                AddOrRemoveUnitAtEnableLists(this.ckEncrypt, value);
            }
        }
        /// <summary>
        /// ����ͬʱΪ����Ŀ  0 = ������ 1 = ���� ���֤��绰����
        /// </summary>
        [Category("�޸Ŀ���"), Description("����ͬʱΪ����Ŀ  0 = ������ 1 = ���� ���֤��绰����")]
        public int IMustInpubByOne
        {
            get { return this.iMustInpubByOne; }
            set { this.iMustInpubByOne = value; }

        }


        public bool IsShowTitle
        {
            set
            {
                lblshow.Visible = value;
            }
            get
            {
                return lblshow.Visible;
            }
        }
        #endregion

        #endregion

        #region ˽�з���
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
                ////{2C77F7F4-49BF-4a36-BE98-90BF9E1947B8}
                this.cmbSex.Tag = "F";

                //����
                this.cmbNation.AddItems(managerIntegrate.GetConstantList(EnumConstant.NATION));
                this.cmbNation.Text = "����";
                NationHelp = new FS.FrameWork.Public.ObjectHelper(this.cmbNation.alItems);
                //����״̬
                this.cmbMarry.AddItems(HISFC.Models.RADT.MaritalStatusEnumService.List());

                //����
                this.cmbCountry.AddItems(managerIntegrate.GetConstantList(EnumConstant.COUNTRY));
                this.cmbCountry.Text = "�й�";

                //ְҵ��Ϣ
                this.cmbProfession.AddItems(managerIntegrate.GetConstantList(EnumConstant.PROFESSION));

                //������λ
                this.cmbWorkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.WORKNAME));

                //��ϵ����Ϣ
                this.cmbRelation.AddItems(managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //��ϵ�˵�ַ��Ϣ
                this.cmbLinkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��ͥסַ��Ϣ
                this.cmbHomeAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //����
                this.cmbDistrict.AddItems(managerIntegrate.GetConstantList(EnumConstant.DIST));

                //��������
                this.cmbDeveChannel.AddItems(managerIntegrate.QueryConstantList("DeveChannel"));

                //�ͷ�רԱ
                this.cmbCusService.AddItems(managerIntegrate.QueryConstantList("ServiceInfo"));

                //������Դ
                this.cmbPatientSource.AddItems(managerIntegrate.QueryConstantList("PatientSource"));

                this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);

                this.txtYear.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);

                //����
                //this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//��������
                this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
                this.txtAge.Text = this.GetAge(0, 0, 0);// "  0�� 0�� 0��";
                this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
                this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
                //����
                this.cmbArea.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPact.AddItems(managerIntegrate.GetConstantList(EnumConstant.PACTUNIT));
                this.ComPact.AddItems(managerIntegrate.QueryPactUnitOutPatient());
                this.ComPact.Tag = "1";

                //this.cmbPact.Text = "�ֽ�";
                //֤������
                this.cmbCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
                this.cmbCompany.AddItems(managerIntegrate.QueryConstantList("BICOMPANY"));
                this.cmbCardType.Text = "���֤";
                IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper(this.cmbCardType.alItems);

                FS.FrameWork.Management.ControlParam ctlParam = new FS.FrameWork.Management.ControlParam();

                //ȡ������ 0 ��ʾ�ò�����������
                string returnValue = ctlParam.QueryControlerInfo("800006");

                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.isJudgePactByIdno = controlMgr.GetControlParam<bool>("HNMZ94", true, false);

                changePactId = controlMgr.GetControlParam<string>("HNMZ93", true, "");

                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = "0";
                }

                this.McardNO = returnValue;
                CmbEvent();
                SetInputMenu();

                returnValue = ctlParam.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.AccountConstant.SIPactUnitID);
                if (!string.IsNullOrEmpty(returnValue))
                {
                    lstPactID = new List<string>();
                    lstPactID.AddRange(returnValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
                }
                else
                {
                    lstPactID = new List<string>();
                }

                //MPI�ӿ�{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
                //this.iEmpi = FS.HISFC.BizProcess.Integrate.PlatformInstance.GetIEmpiInstance();

                this.AddOrRemoveUnitAtMustInputLists(this.lblPact, this.ComPact, this.isInputPact);

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
        /// ���ݲ����ж��Ƿ�������ת
        /// </summary>
        /// <param name="isNeedChange"></param>
        private void ChangeTabIdex(bool isNeedChange)
        {
            if (isNeedChange)
            {
                foreach (Control control in this.panelControl.Controls)
                {
                    control.TabIndex = 0;
                }

                this.txtName.TabIndex = 1;
                this.cmbSex.TabIndex = 2;
                this.txtYear.TabIndex = 3;
                this.txtMonth.TabIndex = 4;
                this.txtDays.TabIndex = 5;
                this.txtAge.TabIndex = 6;
                this.cmbCardType.TabIndex = 7;
                this.txtIDNO.TabIndex = 8;
                this.cmbArea.TabIndex = 9;
                this.cmbProfession.TabIndex = 10;
                this.cmbPact.TabIndex = 11;// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
                this.txtSiNO.TabIndex = 12;
                this.cmbNation.TabIndex = 13;
                this.cmbCountry.TabIndex = 14;
                this.cmbMarry.TabIndex = 15;
                this.cmbDistrict.TabIndex = 16;
                this.txtHomePhone.TabIndex = 17;
                this.txtNowHome.TabIndex = 18;
                this.txtHomeAddrDoorNo.TabIndex = 19;
                this.txtHomeAddressZip.TabIndex = 20;
                this.cmbHomeAddress.TabIndex = 21;
                this.txtReMark.TabIndex = 22;
                this.cmbWorkAddress.TabIndex = 23;
                this.txtWorkPhone.TabIndex = 24;
                this.txtLinkMan.TabIndex = 25;
                this.cmbRelation.TabIndex = 26;
                this.txtLinkPhone.TabIndex = 27;
                this.cmbLinkAddress.TabIndex = 28;
                this.cmbDeveChannel.TabIndex = 29;
                this.txtEmail.TabIndex = 30;
                this.txtMatherName.TabIndex = 31;
                this.txtOtherCardNo.TabIndex = 32;
                this.cmbCusService.TabIndex = 33;
                this.cmbPatientSource.TabIndex = 34;
                this.txtReferralPerson.TabIndex = 35;
            }
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="cardno">���￨��</param>
        private void SetInfo(string cardno)
        {
            this.Clear(false);
            if (cardno == string.Empty || cardno == null) return;
            this.patientInfo = radtManager.QueryComPatientInfo(cardno);
            if (accountManager.GetPatientPactInfo(this.patientInfo) == -1)
            {
                MessageBox.Show("��ȡһ������ݵ����ݴ���" + accountManager.Err);
                return;
            }
            //����ƽ̨ Ƕ��������{BCE8D830-5FEA-4681-A08A-4BB48D172E20} 
            if (this.patientInfo == null && isLocalOperation == false)
            {
                //if (iEmpi != null )
                //{
                //    this.patientInfo = iEmpi.GetBasePatientinfo(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, cardno);                    
                //}
            }
            if (this.patientInfo != null)
            {
                SetPatient();
            }
            else
            {
                this.Clear(true);
            }
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// 
        private void SetPatient()
        {
            //modify by sung 2009-2-24 {DCAA485E-753C-41ed-ABCF-ECE46CD41B33}
            if (this.patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }
            this.txtName.Text = patientInfo.Name;//��������

            //this.txtName.Text = this.patientInfo.Name;               //����
            //if (this.patientInfo.IsEncrypt)
            //{

            //    this.txtName.Tag = this.patientInfo.DecryptName;         //��ʵ����                  
            //}
            //else
            //{
            //    this.txtName.Tag = null;
            //}
            this.cmbSex.Text = this.patientInfo.Sex.Name;            //�Ա�
            this.cmbSex.Tag = this.patientInfo.Sex.ID;               //�Ա�
            this.ComPact.Text = this.patientInfo.Pact.Name;          //��ͬ��λ����
            this.ComPact.Tag = this.patientInfo.Pact.ID;             //��ͬ��λID
            //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//����̱���˾
            this.cmbCompany.Text = this.patientInfo.Insurance.Name;//��ҵ���չ�˾����
            this.cmbCompany.Tag = this.patientInfo.Insurance.ID;//BICompanyID;//��ҵ���չ�˾id
            
            this.cmbArea.Tag = this.patientInfo.AreaCode;            //����
            this.cmbCountry.Tag = this.patientInfo.Country.ID;       //����
            this.cmbNation.Tag = this.patientInfo.Nationality.ID;    //����
            //{BE0CBF3B-9CE8-42ca-8448-08CCF11755DF}
            //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//����
            if (this.patientInfo.Birthday > DateTime.MinValue)
            {
                //string Ages = this.accountManager.GetAge(this.patientInfo.Birthday);
                //������������ַ����׶�����
                //this.txtAge.Text = Ages.Substring(0, Ages.Length - 1);
                this.dtpBirthDay.Value = this.patientInfo.Birthday;      //��������
            }
            else
            {

                this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();      //��������
            }
            this.cmbDistrict.Text = this.patientInfo.DIST;            //����
            this.cmbProfession.Tag = this.patientInfo.Profession.ID; //ְҵ
            this.txtIDNO.Text = this.patientInfo.IDCard;             //���֤��
            this.cmbWorkAddress.Text = this.patientInfo.CompanyName; //������λ
            this.txtWorkPhone.Text = this.patientInfo.PhoneBusiness; //��λ�绰
            this.cmbMarry.Tag = this.patientInfo.MaritalStatus.ID.ToString();//����״��
            this.cmbHomeAddress.Text = this.patientInfo.AddressHome;  //��ͥסַ
            this.txtNowHome.Text = this.patientInfo.User01;  //��סַ
            this.txtHomePhone.Text = this.patientInfo.PhoneHome;     //��ͥ�绰
            this.txtLinkMan.Text = this.patientInfo.Kin.Name;        //��ϵ�� 
            this.cmbRelation.Tag = this.patientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
            this.cmbLinkAddress.Text = this.patientInfo.Kin.RelationAddress;//��ϵ�˵�ַ
            this.txtLinkPhone.Text = this.patientInfo.Kin.RelationPhone;//��ϵ�˵绰
            this.ckEncrypt.Checked = this.patientInfo.IsEncrypt; //�Ƿ��������
            this.ckVip.Checked = this.patientInfo.VipFlag;//�Ƿ�vip
            this.txtMatherName.Text = this.patientInfo.MatherName;//ĸ������
            this.cmbCardType.Tag = this.patientInfo.IDCardType.ID; //֤������
            this.txtSiNO.Text = this.patientInfo.SSN;//��ᱣ�պ�
            this.txtReMark.Text = this.patientInfo.Memo;//��ע


            // {793CA9DB-FD85-460a-B8B4-971C31FFAD45}
            this.cmbPatientSource.Tag = this.patientInfo.PatientSourceInfo.ID;//������Դ
            this.cmbCusService.Tag = this.patientInfo.ServiceInfo.ID;//�ͷ�רԱ
            this.cmbDeveChannel.Tag = this.patientInfo.ChannelInfo.ID;//��������
            this.txtOtherCardNo.Text = this.patientInfo.OtherCardNo;//��������
            this.txtReferralPerson.Text = this.patientInfo.ReferralPerson;//ת����
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
            cbxChild.Checked = patientInfo.ChildFlag == "1";

            if (this.isMutilPactInfo)
            {
                if (patientInfo.MutiPactInfo != null)
                {
                    if (this.ComPact is ComboBoxPactSelect)
                    {
                        ((ComboBoxPactSelect)this.ComPact).AddValues(new ArrayList(patientInfo.MutiPactInfo));
                    }
                }
            }


            #region added by zhaoyang 2008-10-13
            txtHomeAddressZip.Text = this.patientInfo.HomeZip;//�ʱ�
            txtHomeAddrDoorNo.Text = this.patientInfo.AddressHomeDoorNo;//��ͥ��ַ���ƺ�
            txtEmail.Text = this.patientInfo.Email;//�����ʼ�
            #endregion

            //����ͼƬ
            byte[] photo = null;
            if (this.accountManager.GetIdenInfoPhoto(this.patientInfo.PID.CardNO, this.patientInfo.IDCardType.ID, ref photo) > 0 && photo != null)
            {
                System.IO.MemoryStream me = null;
                try
                {
                    me = new System.IO.MemoryStream(photo);
                    this.pictureBox.Image = Image.FromStream(me);
                    me.Close();
                }
                catch (Exception objEx)
                {
                    // MessageBox.Show("��ȡ������Ƭ��Ϣ����" + objEx.Message);
                }
                finally
                {
                    if (me != null)
                    {
                        me.Close();
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <param name="patient"></param>
        private void GetPatienName(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //ѡ�����
            if (ckEncrypt.Checked)
            {
                string name = string.Empty;
                if (this.txtName.Tag == null || this.txtName.Tag.ToString() == string.Empty)
                {
                    name = this.txtName.Text;
                }
                else
                {
                    name = this.txtName.Tag.ToString();
                }
                string encryptStr = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(name);

                patientInfo.Name = "******";
                patientInfo.NormalName = encryptStr;
                patientInfo.DecryptName = name;
            }
            else
            {
                patientInfo.Name = this.txtName.Text;
            }
        }

        private void CmbEvent()
        {
            foreach (Control c in this.panelControl.Controls)
            {
                c.Enter += new EventHandler(c_Enter);
            }
        }

        /// <summary>
        /// �ؼ���ý���ʱ��Ӧ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c_Enter(object sender, EventArgs e)
        {
            if (sender == this.txtName || sender == this.txtLinkMan || sender == cmbHomeAddress || sender == cmbLinkAddress || sender == this.txtMatherName || sender == this.cmbWorkAddress)
                InputLanguage.CurrentInputLanguage = CHInput;

            else
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            if (CmbFoucs != null)
                this.CmbFoucs(sender, null);

        }

        /// <summary>
        /// ���ݿؼ�����,�ж��Ƿ��ڱ�������ؼ��б��м������ɾ���ÿؼ�
        /// </summary>
        /// <param name="nameControl">���ƿؼ�</param>
        /// <param name="inputControl">����ؼ�</param>
        /// <param name="isMustInput">�Ƿ��������</param>
        private void AddOrRemoveUnitAtMustInputLists(Control nameControl, Control inputControl, bool isMustInput)
        {
            if (isMustInput)
            {
                if (!InputHasTable.ContainsKey(nameControl))
                {
                    InputHasTable.Add(nameControl, inputControl);
                    nameControl.ForeColor = Color.Blue;
                }
            }
            else
            {
                if (InputHasTable.ContainsKey(nameControl))
                {
                    InputHasTable.Remove(nameControl);
                    nameControl.ForeColor = Color.Black;
                }
            }
            inpubMaxTabIndex = 0;
            foreach (DictionaryEntry de in InputHasTable)
            {
                Control c = de.Value as Control;
                //��ȡ����tabIndex
                if (inpubMaxTabIndex < c.TabIndex)
                {
                    inpubMaxTabIndex = c.TabIndex;
                }
            }
        }

        /// <summary>
        /// ���ݿؼ�����,�ж��Ƿ��ڱ�������ؼ��б��м������ɾ���ÿؼ�
        /// </summary>
        /// <param name="enableControl">����ؼ�</param>
        /// <param name="isEnable">�Ƿ���Ա༭</param>
        private void AddOrRemoveUnitAtEnableLists(Control enableControl, bool isEnable)
        {
            if (isEnable)
            {
                if (EnableControlList.Contains(enableControl))
                {
                    EnableControlList.Remove(enableControl);
                    enableControl.Enabled = true;
                }
            }
            if (!isEnable)
            {
                if (!EnableControlList.Contains(enableControl))
                {
                    EnableControlList.Add(enableControl);
                    enableControl.Enabled = false;
                }
            }
        }

        #region ������Ĭ��ֵ����
        ///// <summary>
        ///// ���没��Ĭ�Ͽ���
        ///// </summary>
        ///// <param name="deptCode"></param>
        //private void SaveCasDeptdefautValue(string deptCode)
        //{
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        this.CreateXml();
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(filePath);
        //    XmlNode xn = doc.SelectSingleNode("//DefaultValue");
        //    xn.InnerText = deptCode;
        //    doc.Save(filePath);
        //}

        ///// <summary>
        ///// ����xml
        ///// </summary>
        //private void CreateXml()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml("<setting>  </setting>");
        //    XmlNode xn = doc.DocumentElement;
        //    XmlComment xc = doc.CreateComment("���ﻼ����Ϣ¼�벡����Ĭ��ֵ");
        //    XmlElement xe = doc.CreateElement("DefaultValue");
        //    xn.AppendChild(xc);
        //    xn.AppendChild(xe);
        //    doc.Save(filePath);
        //}

        ///// <summary>
        ///// ��ȡ����Ĭ��ֵ
        ///// </summary>
        ///// <returns></returns>
        //private string ReadCaseDept()
        //{
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        this.CreateXml();
        //        return string.Empty;
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(filePath);
        //    XmlNode xn = doc.SelectSingleNode("//DefaultValue");
        //    return xn.InnerText;
        //}
        #endregion
        #endregion

        #region ����

        /// <summary>
        /// ��ý���
        /// </summary>
        /// <returns></returns>
        public new bool Focus()
        {
            return this.txtName.Focus();
        }

        /// <summary>
        /// �������
        /// </summary>
        public virtual void Clear(bool isClearCardNO)
        {
            if (isClearCardNO)
            {
                this.CardNO = string.Empty;
            }


            foreach (Control c in this.panelControl.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuComboBox ||
                    c is FrameWork.WinForms.Controls.NeuTextBox)
                {
                    c.Text = string.Empty;
                    c.Tag = string.Empty;
                }
            }

            this.txtAge.ReadOnly = false;
            this.ckEncrypt.Checked = false;
            this.cmbCountry.Text = "�й�";

            //{2C77F7F4-49BF-4a36-BE98-90BF9E1947B8}
            //�Ա�Ĭ��Ϊ��
            //this.cmbSex.Text = "";
            //this.cmbSex.Tag = "";
            this.cmbSex.Tag = "F";
            if (this.cmbCardType.alItems.Count > 0 && this.cmbCardType.alItems[0] is NeuObject)
            {
                this.cmbCardType.Tag = (this.cmbCardType.alItems[0] as NeuObject).ID;
            }
            //this.cmbNation.Text = "��";
            this.cmbNation.Tag = "1";
            if (isMutilPactInfo)
            {
                if (this.ComPact is ComboBoxPactSelect)
                {
                    ((ComboBoxPactSelect)this.ComPact).Clear();
                }
            }
            this.ComPact.Tag = "1";
            this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//��������

            this.txtAge.Text = this.GetAge(0, 0, 0);// "  0�� 0�� 0��";
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
            //this.txtYear.Text = DateTime.Now.Year.ToString();
            //this.txtMonth.Text = "1";
            //this.txtDays.Text = "1";
            this.ckVip.Checked = false;
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
            this.cbxChild.Checked = false;
            this.pictureBox.Image = null;
            this.pictureBox.Tag = null;
            this.patientInfo = null;
        }

        /// <summary>
        /// ���ݺ���У��
        /// </summary>
        /// <returns></returns>
        public virtual bool InputValid()
        {
            //�жϱ�������Ŀؼ��Ƿ��Ѿ�������Ϣ
            foreach (DictionaryEntry d in this.InputHasTable)
            {
                if (d.Value is FS.FrameWork.WinForms.Controls.NeuComboBox)
                {
                    if (((Control)d.Value).Tag == null || ((Control)d.Value).Tag.ToString() == string.Empty || ((Control)d.Value).Text.Trim() == string.Empty)
                    {
                        MessageBox.Show(((Control)d.Key).Text.Replace(':', ' ') + Language.Msg("����������Ϣ!"));
                        ((Control)d.Value).Focus();

                        return false;
                    }
                }
                else
                {
                    if (((Control)d.Value).Text == string.Empty)
                    {
                        MessageBox.Show(((Control)d.Key).Text.Replace(':', ' ') + Language.Msg("����������Ϣ!"));
                        ((Control)d.Value).Focus();

                        return false;
                    }
                }
            }
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ ��ͯ�ı�����д��ϵ�˵绰 �Ƕ�ͯ�������д��ϵ�绰 ����ϵ�绰�����ظ�
            if (cbxChild.Checked)
            {
                if (string.IsNullOrEmpty(txtLinkMan.Text) || string.IsNullOrEmpty(txtLinkPhone.Text))
                {
                    string strMsg = Language.Msg("��ϵ�˵绰����������Ϊ�գ�");
                    MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.OK);
                    this.txtLinkPhone.Focus();
                    return false;
                }
                else
                {

                    Regex rx = new Regex(@"^0{0,1}(13[0-9]|14[0-9]|15[7-9]|15[0-6]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}$");
                    if (!rx.IsMatch(txtLinkPhone.Text)) //��ƥ��
                    {
                        txtLinkPhone.Text = ""; //��ɿ�
                        MessageBox.Show("��ϵ���ֻ��Ÿ�ʽ���ԣ����������룡");    //������ʾ
                        txtLinkPhone.Focus();
                        return false;
                    }


                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtHomePhone.Text))
                {
                    string strMsg = Language.Msg("��ϵ�绰����Ϊ�գ�");
                    MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.OK);
                    this.txtHomePhone.Focus();
                    return false;
                }
                else
                {
                    Regex rx = new Regex(@"^0{0,1}(13[0-9]|14[0-9]|15[7-9]|15[0-6]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}$");
                    if (!rx.IsMatch(txtHomePhone.Text)) //��ƥ��
                    {
                        txtHomePhone.Text = ""; //��ɿ�
                        MessageBox.Show("��ϵ���ֻ��Ÿ�ʽ���ԣ����������룡");    //������ʾ
                        txtHomePhone.Focus();
                        return false;
                    }

                    
                }
            }
            if (!string.IsNullOrEmpty(txtHomePhone.Text)) //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ ��ͯ�ı�����д��ϵ�˵绰 �Ƕ�ͯ�������д��ϵ�绰 ����ϵ�绰�����ظ�
            {
                List<FS.HISFC.Models.RADT.PatientInfo> list = new List<FS.HISFC.Models.RADT.PatientInfo>();
                list = this.accountManager.QueryPatientInfoByWhere("", "", txtHomePhone.Text, "");// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}

                if (list != null && list.Count > 0)// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
                {
                    FS.HISFC.Models.RADT.PatientInfo ac = list[0];
                    if (this.patientInfo != null  )
                    {
                        if (string.IsNullOrEmpty(patientInfo.PID.CardNO))
                        {
                            string  strMsg = Language.Msg(string.Format("�Ѵ�����ͬ����ϵ�绰��,������Ϊ��{0}", ac.PID.CardNO));
                            MessageBox.Show(strMsg);
                            return false;
                        }
                        else
                        {
                            if (list.Count > 1)
                            {
                                string strMsg = null;
                                if (list[0].PID.CardNO == patientInfo.PID.CardNO)
                                {

                                    strMsg = Language.Msg(string.Format("�Ѵ�����ͬ����ϵ�绰��,������Ϊ��{0}", list[1].PID.CardNO));
                                }
                                else
                                {
                                    strMsg = Language.Msg(string.Format("�Ѵ�����ͬ����ϵ�绰��,������Ϊ��{0}", list[0].PID.CardNO));
                                }
                                MessageBox.Show(strMsg);
                                return false;
                            }
                            else
                            {
                                if (list[0].PID.CardNO != patientInfo.PID.CardNO)
                                {
                                    string strMsg = Language.Msg(string.Format("�Ѵ�����ͬ����ϵ�绰��,������Ϊ��{0}", list[0].PID.CardNO));
                                    MessageBox.Show(strMsg);
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            string strMsg = Language.Msg(string.Format("�Ѵ�����ͬ����ϵ�绰��,������Ϊ��{0}", list[0].PID.CardNO));
                            MessageBox.Show(strMsg);
                            return false;
                        }
                    }
                    //if (MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    //{
                    //    return false;
                    //}
                }
            }
            if (!this.ckEncrypt.Checked && this.txtName.Text == "******")
            {
                MessageBox.Show(Language.Msg("�û�������û�м��ܣ���������ȷ�Ļ���������"));
                this.txtName.Focus();
                this.txtName.SelectAll();
                return false;
            }

            //
            // ���������жϣ���ֹ�쿨ʱ����ѯ����ʾ������Ϣ�����£�δ��ջ�����Ϣ�Ͱ쿨
            // 
            if (this.patientInfo != null && !this.IsEditMode)
            {
                string patName = this.txtName.Text.Trim();
                if (!string.IsNullOrEmpty(patientInfo.Name) && !string.IsNullOrEmpty(patName))
                {
                    if (patientInfo.Name.Trim() != patName)
                    {
                        string strMsg = Language.Msg("���뻼����������ѡ����������ͬ���°쿨����ջ�����Ϣ��");
                        MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.OK);
                        this.txtIDNO.Focus();
                        return false;
                    }
                }
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text, 50))
            {
                MessageBox.Show(Language.Msg("����¼�볬����"));
                this.txtName.Focus();
                return false;
            }

            //�ж��ַ�����ҽ��֤��
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtSiNO.Text, 20))
            {
                MessageBox.Show(Language.Msg("ҽ��֤��¼�볬����"));
                this.txtSiNO.Focus();
                return false;
            }
            //�ж��ַ���������
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbDistrict.Text, 50))
            {
                MessageBox.Show(Language.Msg("����¼�볬����"));
                this.cmbDistrict.Focus();
                return false;
            }
            //�ж��ַ�����֤����
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtIDNO.Text, 20))
            {
                MessageBox.Show(Language.Msg("֤����¼�볬����"));
                this.txtIDNO.Focus();
                return false;
            }

            //�ж����֤��// {D59C1D74-CE65-424a-9CB3-7F9174664504}
            if (this.cmbCardType.Tag != null && this.cmbCardType.Tag.ToString() == "01" && this.txtIDNO.Text.Trim() != string.Empty)
            {
                string err = string.Empty;
                if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIDNO.Text.Trim(), ref err) < 0)
                {
                    if (isTooltipCARDID == 1)// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
                    {
                        string strMsg = Language.Msg(err + "\r\n�Ƿ������");
                        if (MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            this.txtIDNO.Focus();
                            return false;
                        }
                    }
                    else if (isTooltipCARDID == 2)
                    {

                        string strMsg = Language.Msg(err + "\r\n���������");

                        MessageBox.Show(strMsg, "ϵͳ��ʾ");
                        this.txtIDNO.Focus();
                        return false;
                    }
                    else
                    {
                    }
                }
            }

            if (this.isMutilPactInfo)
            {
                if (this.ComPact is ComboBoxPactSelect)
                {
                    ArrayList arl = ((ComboBoxPactSelect)this.ComPact).GetValues();
                    if (arl == null || arl.Count <= 0)
                    {
                        MessageBox.Show(Language.Msg("��ѡ���ߺ�ͬ��λ��"));
                        this.ComPact.Focus();
                        return false;
                    }
                }
            }


            //�ж��ַ�����������λ
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbWorkAddress.Text, 50))
            {
                MessageBox.Show(Language.Msg("������λ¼�볬����"));
                this.cmbWorkAddress.Focus();
                return false;
            }

            //�жϵ�λ�绰����
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWorkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("��λ�绰����¼�볬��"));
                this.txtWorkPhone.Focus();
                return false;
            }

            //�жϼ�ͥ��ַ����
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbHomeAddress.Text, 100))
            {
                MessageBox.Show(Language.Msg("��ͥ��ַ¼�볬��"));
                this.cmbHomeAddress.Focus();
                return false;
            }

            //�жϼ�ͥ�绰���볤��
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomePhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("��ͥ�绰����¼�볬��"));
                this.txtHomePhone.Focus();
                return false;
            }

            //�ж���ϵ�绰���볤��
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("��ϵ�˵绰����¼�볬��"));
                this.txtLinkPhone.Focus();
                return false;
            }
            //�ж���ϵ��ϵ�˳���
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkMan.Text, 50))
            {
                MessageBox.Show(Language.Msg("��ϵ��¼�볬��"));
                this.txtLinkMan.Focus();
                return false;
            }
            //��ϵ�˵�ַ
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbLinkAddress.Text, 100))
            {
                MessageBox.Show(Language.Msg("��ϵ�˵�ַ¼�볬��"));
                this.cmbLinkAddress.Focus();
                return false;
            }

            //ĸ������
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtMatherName.Text, 50))
            {
                MessageBox.Show(Language.Msg("ĸ������¼�볬��"));
                this.txtMatherName.Focus();
                return false;
            }

            if (this.dtpBirthDay.Value.Date > this.accountManager.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show(Language.Msg("�������ڴ��ڵ�ǰ����,����������!"));
                this.dtpBirthDay.Focus();
                return false;
            }

            #region added by zhaoyang 2008-10-13
            if (string.IsNullOrEmpty(txtEmail.Text) == false)
            {
                Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");//{187A73EB-008A-4A25-A6CB-28CAE0E629A7}�����������ƥ��
                if (r.IsMatch(txtEmail.Text.Trim()) == false)
                {
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    MessageBox.Show("�������������ʽ��������Ļ��������롣");
                    return false;
                }
                //if (NFC.Public.String.isMail(txtEmail.Text.Trim()) == false)
                //{
                //    txtEmail.Focus();
                //    txtEmail.SelectAll();
                //    MessageBox.Show("�������������ʽ��������Ļ��������롣");
                //    return false;
                //}
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtEmail.Text, 50))
                {
                    MessageBox.Show(Language.Msg("��������¼�볬��!"));
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return false;
                }
            }
            //else
            //{
            //    MessageBox.Show(Language.Msg("���䲻��Ϊ��!"));
            //    return false;
            //}
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomeAddrDoorNo.Text, 40))
            {
                MessageBox.Show(Language.Msg("���ƺ�¼�볬����"));
                txtHomeAddrDoorNo.SelectAll();
                txtHomeAddrDoorNo.Focus();
                return false;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomeAddressZip.Text, 6))
            {
                MessageBox.Show(Language.Msg("�ʱ�¼�볬����"));
                txtHomeAddressZip.SelectAll();
                txtHomeAddressZip.Focus();
                return false;
            }
            #endregion

            #region ����ͬʱΪ����Ŀ

            if (iMustInpubByOne != 0)
            {
                if (iMustInpubByOne == 1)
                {
                    if (string.IsNullOrEmpty(txtIDNO.Text.Trim()) && string.IsNullOrEmpty(txtHomePhone.Text.Trim()))
                    {
                        MessageBox.Show(Language.Msg("֤������绰���벻��ͬʱΪ�գ�"));
                        this.txtIDNO.Focus();
                        return false;
                    }
                }
            }

            #endregion

            #region ҽ������ж�֤����
            string strPactID = this.ComPact.Tag == null ? "" : this.ComPact.Tag.ToString().Trim();
            if (!string.IsNullOrEmpty(strPactID))
            {
                if (lstPactID.Contains(strPactID))
                {
                    string strTemp = txtIDNO.Text.Trim();
                    if (string.IsNullOrEmpty(strTemp))
                    {
                        MessageBox.Show(Language.Msg("ҽ�����ߣ�֤���Ų���Ϊ�գ�"));
                        txtIDNO.Focus();
                        return false;
                    }
                }
            }


            #endregion

            #region �쿨ʱ�Ƿ�ʵʱ�жϣ��Ƿ�������Ӧ�ĺ�ͬ��λ
            // �쿨ʱ�Ƿ�ʵʱ�жϣ��Ƿ�������Ӧ�ĺ�ͬ��λ
            // {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
            if (this.isJudgePact)
            {
                FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                r.IDCard = this.txtIDNO.Text.Trim();
                r.Pact.ID = this.ComPact.Tag.ToString();
                r.Pact.Name = this.ComPact.Text.ToString();

                ArrayList al = new ArrayList();
                if (this.isMutilPactInfo)
                {
                    if (this.ComPact is ComboBoxPactSelect)
                    {
                        al = ((ComboBoxPactSelect)this.ComPact).GetValues();
                    }
                }
                al.Add(r.Pact);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.PactInfo pactinfo in al)
                    {
                        if (lstPactID.Contains(pactinfo.ID))
                        {
                            string strTemp = txtIDNO.Text.Trim();
                            if (string.IsNullOrEmpty(strTemp))
                            {
                                MessageBox.Show(Language.Msg("���߶����ݰ���ҽ�����֤���Ų���Ϊ�գ�"));
                                txtIDNO.Focus();
                                return false;
                            }
                        }
                        if (this.ValidHospitalStaff(r) == false)
                        {
                            return false;
                        }
                        r.IDCard = this.txtIDNO.Text.Trim();
                        r.Pact = pactinfo;
                        if (this.InputValid(r) == false)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return this.InputValid(r);
                }


            }
            #endregion
            //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//������̱��ͻ����������̱���˾
            if (this.cmbPact.Tag.ToString() == "2")
            {
                if (string.IsNullOrEmpty(cmbCompany.Tag.ToString()))
                {
                    MessageBox.Show(Language.Msg("�̱��ͻ�����ѡ���̱���˾,����������!"));
                    cmbCompany.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �жϱ�Ժְ����ͬ��λ
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool ValidHospitalStaff(FS.HISFC.Models.Registration.Register r)
        {
            if (string.IsNullOrEmpty(isValidHospitalStaff))
            {
                return true;
            }
            else
            {
                String[] arr = isValidHospitalStaff.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                pactList.AddRange(arr);
                if (pactList.Contains(r.Pact.ID))
                {
                    if (r == null || string.IsNullOrEmpty(r.IDCard))
                    {
                        MessageBox.Show(Language.Msg("��Ժְ��������ϢΪ�ջ����֤����"));
                        return false;
                    }

                    if (string.IsNullOrEmpty(r.Pact.ID))
                    {
                        MessageBox.Show(Language.Msg("��Ժְ��������Ϣ����ȷ"));
                        return false;
                    }
                    string sql = "select count(1) from com_employee where IDENNO='{0}'";

                    FS.FrameWork.Management.DataBaseManger mgrManager = new FS.FrameWork.Management.DataBaseManger();
                    string retStr = mgrManager.ExecSqlReturnOne(string.Format(sql, r.IDCard));

                    int i = FS.FrameWork.Function.NConvert.ToInt32(retStr);

                    if (i <= 0)
                    {
                        MessageBox.Show(Language.Msg("�Ǳ�Ժְ��������ѡ��Ժְ������"));
                        return false;
                    }
                }
                return true;
            }

        }

        /// <summary>
        /// ��ͬ��λ��֤
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool InputValid(FS.HISFC.Models.Registration.Register r)
        {
            if (!string.IsNullOrEmpty(r.IDCard) && !string.IsNullOrEmpty(r.Pact.ID))
            {
                int iRes = this.medcareProxy.SetPactCode(r.Pact.ID);
                if (iRes != 1)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    return false;
                }

                iRes = this.medcareProxy.QueryCanMedicare(r);
                if (iRes == -2)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "���޸ĺ�ͬ��λ \r\n" + this.medcareProxy.ErrMsg), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    this.ComPact.Text = "��ͨ";
                    this.ComPact.Tag = "1";
                    return false;
                }
                else if (iRes == -1)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "���޸ĺ�ͬ��λ\r\n" + this.medcareProxy.ErrMsg), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    this.ComPact.Text = "��ͨ";
                    this.ComPact.Tag = "1";
                    return false;
                }

                ////����2��ҽ���ж���ݷ���������ҽ����һ���ж���ݳ�����Щ��ǰ���ҽ�����Էѻ���Ҳ��ҽ��
                //iRes = this.medcareProxy.QueryCanMedicare(r);
                //if (iRes == -2)
                //{
                //    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "���޸ĺ�ͬ��λ"), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.ComPact.Focus();
                //    this.ComPact.Text = "��ͨ";
                //    this.ComPact.Tag = "1";
                //    return false;
                //}
                //else if (iRes == -1)
                //{
                //    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "���޸ĺ�ͬ��λ\r\n" + this.medcareProxy.ErrMsg), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.ComPact.Focus();
                //    this.ComPact.Text = "��ͨ";
                //    this.ComPact.Tag = "1";
                //    return false;
                //}
            }

            return true;
        }

        /// <summary>
        /// ͨ����ͬ��λ����,��ý������ʵ��
        /// </summary>
        /// <param name="pactID">��ͬ��λ����</param>
        /// <returns>�ɹ�: �������ʵ�� ʧ��: null</returns>
        private PayKind GetPayKindFromPactID(string pactID)
        {
            FS.HISFC.Models.Base.PactInfo pact = this.pactManager.GetPactUnitInfoByPactCode(pactID);
            if (pact == null)
            {
                MessageBox.Show(Language.Msg("��ú�ͬ��λ��ϸ����!"));

                return null;
            }

            return pact.PayKind;
        }

        /// <summary>
        /// ��û���ʵ��
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation()
        {
            //ˢ�»��߻�����Ϣ
            patientInfo = managerIntegrate.QueryComPatientInfo(cardNO);
            //����ƽ̨ Ƕ��������{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
            if (this.patientInfo == null && isLocalOperation == false)
            {
                //if (iEmpi != null)
                //{
                //    this.patientInfo = iEmpi.GetBasePatientinfo(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, cardNO);
                //}
            }
            if (patientInfo == null)
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }

            patientInfo.PID.CardNO = cardNO;//���￨��
            if (this.ComPact.Tag != null && this.ComPact.Tag.ToString() != string.Empty)
                patientInfo.Pact.PayKind = GetPayKindFromPactID(this.ComPact.Tag.ToString());//�������

            patientInfo.Pact.ID = this.ComPact.Tag.ToString();//��ͬ��λ 
            //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//����̱���˾
            patientInfo.Pact.Name = this.ComPact.Text.ToString();//��ͬ��λ����
            patientInfo.Insurance.ID = cmbCompany.Tag.ToString();
            patientInfo.Insurance.Name = cmbCompany.Text;
            //patientInfo.BICompanyID = cmbCompany.Tag.ToString();//��ҵ���չ�˾
            //patientInfo.BICompanyName = cmbCompany.Text;//��ҵ���չ�˾����
            if (!this.isTreatment)
            {
                this.GetPatienName(patientInfo); //��������
                patientInfo.IsEncrypt = this.ckEncrypt.Checked; //�Ƿ����
            }
            else
            {
                this.patientInfo.Name = "������";
                patientInfo.IsEncrypt = false;
            }
            patientInfo.Sex.ID = this.cmbSex.Tag.ToString();//�Ա�
            patientInfo.AreaCode = this.cmbArea.Tag.ToString();//����
            patientInfo.Country.ID = this.cmbCountry.Tag.ToString();//����
            patientInfo.Nationality.ID = this.cmbNation.Tag.ToString();//����
            patientInfo.Birthday = this.dtpBirthDay.Value;//��������
            patientInfo.Age = outpatientManager.GetAge(this.dtpBirthDay.Value);//����
            patientInfo.DIST = this.cmbDistrict.Text.ToString();//����
            patientInfo.Profession.ID = this.cmbProfession.Tag.ToString();//ְҵ
            patientInfo.IDCard = this.txtIDNO.Text;//֤����
            patientInfo.IDCardType.ID = this.cmbCardType.Tag.ToString();//֤������
            patientInfo.IDCardType.Name = this.cmbCardType.Text;
            patientInfo.CompanyName = this.cmbWorkAddress.Text.Trim();//������λ
            patientInfo.PhoneBusiness = this.txtWorkPhone.Text.Trim();//��λ�绰
            patientInfo.MaritalStatus.ID = this.cmbMarry.Tag.ToString();//����״�� 
            patientInfo.AddressHome = this.cmbHomeAddress.Text.Trim();//��ͥסַ
            patientInfo.User01 = this.txtNowHome.Text.Trim();//��סַ
            patientInfo.PhoneHome = this.txtHomePhone.Text.Trim();//��ͥ�绰
            patientInfo.Kin.Name = this.txtLinkMan.Text.Trim();//��ϵ�� 
            patientInfo.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//����ϵ�˹�ϵ
            patientInfo.Kin.RelationAddress = this.cmbLinkAddress.Text;//��ϵ�˵�ַ
            patientInfo.Kin.RelationPhone = this.txtLinkPhone.Text.Trim();  //��ϵ�˵绰
            patientInfo.VipFlag = this.ckVip.Checked; //�Ƿ�vip
            patientInfo.MatherName = this.txtMatherName.Text;//ĸ������
            patientInfo.IsTreatment = this.IsTreatment;//�Ƿ���
            patientInfo.SSN = this.txtSiNO.Text;//��ᱣ�պ�
            patientInfo.Memo = this.txtReMark.Text;//��ע

            // {793CA9DB-FD85-460a-B8B4-971C31FFAD45}
            patientInfo.ReferralPerson = this.txtReferralPerson.Text.Trim();//ת����
            patientInfo.OtherCardNo = this.txtOtherCardNo.Text.Trim();//��������
            patientInfo.PatientSourceInfo.ID = this.cmbPatientSource.Tag.ToString();//������Դ
            patientInfo.PatientSourceInfo.Name = this.cmbPatientSource.Text;
            patientInfo.ServiceInfo.ID = this.cmbCusService.Tag.ToString();//�ͷ�רԱ
            patientInfo.ServiceInfo.Name = this.cmbCusService.Text;
            patientInfo.ChannelInfo.ID = this.cmbDeveChannel.Tag.ToString();//��������
            patientInfo.ChannelInfo.Name = this.cmbDeveChannel.Text;
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
            patientInfo.ChildFlag = cbxChild.Checked ? "1" : "0";


            //һ�������
            if (this.isMutilPactInfo)
            {
                patientInfo.MutiPactInfo = new List<PactInfo>();
                ArrayList al = new ArrayList();
                if (this.ComPact is ComboBoxPactSelect)
                {
                    al = ((ComboBoxPactSelect)this.ComPact).GetValues();
                }
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.PactInfo pactinfo in al)
                    {
                        patientInfo.MutiPactInfo.Add(pactinfo);
                    }
                }
            }

            #region added by zhaoyang 2008-10-13
            patientInfo.HomeZip = this.txtHomeAddressZip.Text.Trim();//�ʱ�
            patientInfo.AddressHomeDoorNo = txtHomeAddrDoorNo.Text.Trim();//��ͥ��ַ���ƺ�
            patientInfo.Email = txtEmail.Text.Trim();//��������
            #endregion
            return patientInfo;
        }

        /// <summary>
        /// ���滼������
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            // ȥ���жϣ����������
            ////��ͨ���߾��￨����
            //if (!IsTreatment)
            //{
            //    if (!InputValid()) return -1;

            //}

            this.patientInfo = this.GetPatientInfomation();

            if (patientInfo.Pact.PayKind.ID == "02")
            {
                ////��������ҽ��������ʱ�������Ա�ҽ��֤�Ų���Ϊ��
                //if (this.txtName.Text == string.Empty || this.txtSiNO.Text == string.Empty
                //    || this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == string.Empty)
                //{
                //    MessageBox.Show("�û�����ҽ�����ߣ��������Ա�ҽ��֤�Ų���Ϊ�գ�", "��ʾ");
                //    return -1;
                //}

                // ��������ҽ��������ʱ�������Ա� ����Ϊ��
                if (this.txtName.Text == string.Empty
                    || this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == string.Empty)
                {
                    MessageBox.Show("�û�����ҽ�����ߣ��������Ա���Ϊ�գ�", "��ʾ");
                    return -1;
                }
            }

            if (string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                //����ƽ̨ ����������{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
                //if (this.iEmpi != null && isLocalOperation == false)
                //{
                //    if (iEmpi.GetDomainID(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, patientInfo, false, ref cardNO) == -1)
                //    {
                //        MessageBox.Show("���������Ļ�ȡ�²������ŷ�������" + iEmpi.Message);
                //        return -1;
                //    }
                //    if (string.IsNullOrEmpty(cardNO))
                //    {
                //        cardNO = outpatientManager.GetAutoCardNO();
                //        cardNO = cardNO.PadLeft(HISFC.BizProcess.Integrate.Common.ControlParam.GetCardNOLen(), '0');
                //    }
                //}
                //else
                //{
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                if (!this.cardWay)
                {
                    cardNO = this.McardNO;
                }
                else if (isAutoBuildCardNo)
                {
                    cardNO = autoCardNo;
                }
                else
                {
                    cardNO = outpatientManager.GetAutoCardNO();
                }

                //cardNO = cardNO.PadLeft(HISFC.BizProcess.Integrate.Common.ControlParam.GetCardNOLen(), '0');
                cardNO = cardNO.PadLeft(10, '0');
                //}
            }
            else
            {
                cardNO = patientInfo.PID.CardNO;
            }
            patientInfo.PID.CardNO = cardNO;

            if (radtManager.RegisterComPatient(patientInfo) < 0)
            {
                MessageBox.Show(radtManager.Err);
                return -1;
            }

            if (accountManager.InsertPatientPactInfo(patientInfo) < 0)
            {
                MessageBox.Show(radtManager.Err);
                return -1;
            }


            if (!string.IsNullOrEmpty(patientInfo.IDCardType.ID) && !string.IsNullOrEmpty(patientInfo.IDCard))
            {

                if (accountManager.InsertIdenInfo(patientInfo) == -1)
                {
                    if (this.accountManager.DBErrCode == 1)
                    {
                        if (accountManager.UpdateIdenInfo(patientInfo) == -1)
                        {
                            MessageBox.Show(accountManager.Err);
                            return -1;
                        }
                    }
                }

                //������Ƭ
                if (this.pictureBox.Image != null)
                {
                    System.IO.MemoryStream m = new System.IO.MemoryStream();
                    if (this.pictureBox.Tag != null)
                    {
                        Image img = Image.FromFile(this.pictureBox.Tag.ToString());
                        if (img != null)
                        {
                            img.Save(m, System.Drawing.Imaging.ImageFormat.Bmp);
                            img.Dispose();
                            if (m != null)
                            {
                                if (accountManager.UpdatePhoto(patientInfo, m.ToArray()) == -1)
                                {
                                    m.Close();
                                    MessageBox.Show(accountManager.Err);
                                    return -1;
                                }

                                m.Close();
                            }
                        }
                    }


                }
            }

            //����ƽ̨ ����������{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
            //if (this.iEmpi != null && isLocalOperation == false)
            //{
            //    if (iEmpi.CreateOrUpdatePatient(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, patientInfo) == -1)
            //    {
            //        MessageBox.Show("��������»�����������Ϣ����" + iEmpi.Message);
            //        return -1;
            //    }
            //}
            return 1;
        }

        /// <summary>
        /// ����ID�������
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

        /// <summary>
        /// ���ݱ�������ؼ���ת���뽹��
        /// </summary>
        private void SetMustInputFocus(Control currentControl)
        {
            if (currentControl == null)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            //������Ӧ���ȵ������������Ŀؼ�
            Control tempControl = this.NextFocusControl(currentControl);
            if (tempControl != null && tempControl.CanFocus)
            {
                tempControl.Focus();
            }
            else
            {
                //�������һ�������ʱ�򴥷����¼�
                if (this.OnFoucsOver != null)
                {
                    this.OnFoucsOver(null, null);
                }
                else
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        /// <summary>
        /// ���ݵ�ǰ��TabIndex������һ��Ӧ�õõ�����Ŀؼ�
        /// </summary>
        /// <param name="CurrentTabIndex"></param>
        /// <returns></returns>
        private Control NextFocusControl(Control currentContol)
        {
            Control tempControl = null;
            foreach (DictionaryEntry de in InputHasTable)
            {
                Control c = de.Value as Control;
                if (currentContol.TabIndex < c.TabIndex)
                {
                    if (tempControl == null)
                    {
                        tempControl = c;
                        continue;
                    }
                    if (tempControl != null && tempControl.TabIndex > c.TabIndex)
                    {
                        tempControl = c;
                    }
                }
            }
            return tempControl;
        }

        /// <summary>
        /// ��ȡ��ǰ�н���ؼ�
        /// </summary>
        /// <returns></returns>
        private Control GetCurrentFoucsControl()
        {
            foreach (Control c in panelControl.Controls)
            {
                if (c.Focused)
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// ���ÿؼ�enable����
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetControlEnable(bool isEnabled)
        {
            foreach (Control c in this.panelControl.Controls)
            {
                c.Enabled = isEnabled;
            }
        }

        /// <summary>
        /// ��ʾ��
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string title)
        {
            this.lblshow.Text = title;
        }


        public int ReadIDCard()
        {
            this.panelControl.Controls.Add(this.pictureBox);


            FS.HISFC.BizProcess.Interface.Account.IReadIDCard readIDCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Account.IReadIDCard)) as FS.HISFC.BizProcess.Interface.Account.IReadIDCard;
            if (readIDCard == null)
            {
                MessageBox.Show("�����֤�ӿ�û��ʵ��");
                return -1;
            }
            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
            string photoFileName = "";
            int rtn = readIDCard.GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
            if (rtn == -1)
            {
                MessageBox.Show("��ȡ���֤����" + message);
                return -1;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                MessageBox.Show("��ȡ���֤ʧ�ܣ���ȷ�Ϸź�λ�ã�" + message);
                return -1;
            }
            this.txtName.Text = name;
            this.txtIDNO.Text = code;
            string error = string.Empty;
            if (code != string.Empty)
            {
                //if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(code, ref error) < 0)
                //{
                //    string strMsg = Language.Msg(error + "\r\n�Ƿ������");
                //    if (MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        return;
                //    }
                //}
                //�������֤�Ż�ȡ�����Ա�
                FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(code, ref error);
                if (obj == null)
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.cmbSex.Tag = obj.ID;
                this.cmbCardType.Text = "���֤";
                this.cmbCardType.Tag = "01";
                //���ݻ������֤�Ż�ȡ����
                string birthdate = Class.Function.GetBirthDayFromIdNO(code, ref error);
                if (birthdate == "-1")
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);
            }
            //this.dtpBirthDay.Value = birth;
            //this.cmbSex.SelectedItem.ID = sex;
            this.cmbHomeAddress.Text = add;
            this.txtHomePhone.Focus();
            try
            {
                System.IO.MemoryStream m = new System.IO.MemoryStream();
                Image img = Image.FromFile(photoFileName);
                img.Save(m, System.Drawing.Imaging.ImageFormat.Bmp);
                img.Dispose();
                this.pictureBox.Image = Image.FromStream(m);
                this.pictureBox.Tag = photoFileName;
                m.Close();
            }
            catch
            { }

            return 1;
        }

        public int AutoReadIDCard()
        {
            FS.HISFC.BizProcess.Interface.Account.IReadIDCard readIDCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Account.IReadIDCard)) as FS.HISFC.BizProcess.Interface.Account.IReadIDCard;
            if (readIDCard == null)
            {

                //MessageBox.Show("�����֤�ӿ�û��ʵ��");
                return -2;
            }
            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
            string photoFileName = "";
            int rtn = readIDCard.GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
            if (rtn == -1)
            {
                if (message.IndexOf("�˿ڴ�ʧ��") >= 0)
                {
                    return -2;
                }
                //MessageBox.Show("��ȡ���֤����" + message);
                return -1;
            }

            this.Clear(true);
            this.panelControl.Controls.Add(this.pictureBox);
            this.txtName.Text = name;
            this.txtIDNO.Text = code;
            string error = string.Empty;
            if (code != string.Empty)
            {
                //if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(code, ref error) < 0)
                //{
                //    string strMsg = Language.Msg(error + "\r\n�Ƿ������");
                //    if (MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        return;
                //    }
                //}
                //�������֤�Ż�ȡ�����Ա�
                FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(code, ref error);
                if (obj == null)
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.cmbSex.Tag = obj.ID;
                this.cmbCardType.Text = "���֤";
                this.cmbCardType.Tag = "01";
                //���ݻ������֤�Ż�ȡ����
                string birthdate = Class.Function.GetBirthDayFromIdNO(code, ref error);
                if (birthdate == "-1")
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);
            }
            //this.dtpBirthDay.Value = birth;
            //this.cmbSex.SelectedItem.ID = sex;
            this.cmbHomeAddress.Text = add;
            this.txtHomePhone.Focus();
            System.IO.MemoryStream m = new System.IO.MemoryStream();
            Image img = Image.FromFile(photoFileName);
            img.Save(m, System.Drawing.Imaging.ImageFormat.Bmp);
            img.Dispose();
            this.pictureBox.Image = Image.FromStream(m);
            this.pictureBox.Tag = photoFileName;
            m.Close();

            return 1;
        }
        #endregion

        #region ���뷨

        /// <summary>
        /// Ĭ�ϵ��������뷨
        /// </summary>
        private InputLanguage CHInput = null;

        /// <summary>
        /// ��ʼ�����뷨�˵�
        /// </summary>
        private void SetInputMenu()
        {

            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                InputLanguage t = InputLanguage.InstalledInputLanguages[i];
                System.Windows.Forms.ToolStripMenuItem m = new ToolStripMenuItem();
                m.Text = t.LayoutName;
                m.Click += new EventHandler(m_Click);
                neuContextMenuStrip1.Items.Add(m);
            }

            this.ReadInputLanguage();
        }

        /// <summary>
        /// ��ȡ��ǰĬ�����뷨
        /// </summary>
        private void ReadInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                // FS.UFC.Common.Classes.Function.CreateFeeSetting();

            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                CHInput = GetInputLanguage(node.Attributes["currentmodel"].Value);

                if (CHInput != null)
                {
                    foreach (ToolStripMenuItem m in neuContextMenuStrip1.Items)
                    {
                        if (m.Text == CHInput.LayoutName)
                        {
                            m.Checked = true;
                        }
                    }
                }

                //��ӵ�������

            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡĬ���������뷨����!" + e.Message);
                return;
            }
        }

        /// <summary>
        /// �������뷨���ƻ�ȡ���뷨
        /// </summary>
        /// <param name="LanName"></param>
        /// <returns></returns>
        private InputLanguage GetInputLanguage(string LanName)
        {
            foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
            {
                if (input.LayoutName == LanName)
                {
                    return input;
                }
            }
            return null;
        }

        /// <summary>
        /// ���浱ǰ���뷨
        /// </summary>
        private void SaveInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                // FS.UFC.Common.Classes.Function.CreateFeeSetting();
            }
            if (CHInput == null)
                return;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                node.Attributes["currentmodel"].Value = CHInput.LayoutName;

                doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("����Ĭ���������뷨����!" + e.Message);
                return;
            }
        }

        private void m_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    this.CHInput = this.GetInputLanguage(m.Text);
                    //�������뷨
                    this.SaveInputLanguage();
                }
                else
                {
                    m.Checked = false;
                }
            }
        }

        #endregion

        #region �¼�
        private void ucPatientInfo_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() != "devenv")
            {
                #region Ȩ���ж�
                FS.HISFC.BizLogic.Manager.UserPowerDetailManager user = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
                NeuObject dept = (accountManager.Operator as HISFC.Models.Base.Employee).Dept;
                //�ж��Ƿ��м���Ȩ��
                this.IsEnableEntry = user.JudgeUserPriv(accountManager.Operator.ID, dept.ID, "5001", "01");

                //VipȨ������
                this.IsEnableVip = user.JudgeUserPriv(accountManager.Operator.ID, dept.ID, "5002", "01");


                #endregion

                this.Init();
                this.ActiveControl = this.txtName;
            }

            // {BDEC89C8-C3BB-433b-8A98-AA65B2FFCEE5}
            this.isJudgePact = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.AccountConstant.BuildCardIsJudgePact, false);
            //������ʾ{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            {
                iMultiScreen = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                        FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen>(this.GetType());
                if (iMultiScreen == null)
                {
                    iMultiScreen = new Forms.frmMiltScreen();

                }
                //iMultiScreen.ListInfo = null;
                //��ʾ��ʼ������
                FS.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as FS.HISFC.Models.Base.Employee;
                System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                lo.Add("");//���߻�����Ϣ
                lo.Add("");//�°쿨��
                lo.Add(currentOperator.ID);//�շ�Ա����
                lo.Add(currentOperator.Name);//�շ�Ա����
                this.iMultiScreen.ListInfo = lo;
                //
                iMultiScreen.ShowScreen();
            }
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
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
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = this.GetAge(iyear, iMonth, iDay);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);

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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (Screen.AllScreens.Length > 1)
                {
                    //ʵʱ������ʾ{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}  
                    this.showPatientInfo = this.GetPatientInfomation();
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add(this.showPatientInfo);//������Ϣ
                    lo.Add("");//����
                    lo.Add("");//�շ�Աid
                    lo.Add("");//�շ�Ա����
                    this.iMultiScreen.ListInfo = lo;
                    //
                }


                //if (isMustInputTabInde)
                //{
                //    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                //    if (txtMatherName.Focused)
                //    {
                //        if (OnFoucsOver != null)
                //        {
                //            OnFoucsOver(null, null);
                //            return true;
                //        }
                //    }
                //    if (this.txtIDNO.Focused)
                //    {
                //        if (this.cmbCardType.Tag.ToString() == "01")
                //        {
                //            string error = string.Empty;
                //            string idNO = this.txtIDNO.Text.Trim();
                //            if (idNO != string.Empty)
                //            {
                //                if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref error) < 0)
                //                {
                //                    MessageBox.Show(error);
                //                    return true;
                //                }
                //                //�������֤�Ż�ȡ�����Ա�
                //                FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(idNO, ref error);
                //                if (obj == null)
                //                {
                //                    MessageBox.Show(error);
                //                    return true;
                //                }
                //                this.cmbSex.Tag = obj.ID;
                //                //���ݻ������֤�Ż�ȡ����
                //                string birthdate = Class.Function.GetBirthDayFromIdNO(idNO, ref error);
                //                if (birthdate == "-1")
                //                {
                //                    MessageBox.Show(error);
                //                    return true;
                //                }
                //                this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);
                //            }
                //        }
                //    }

                //    try
                //    {
                //        if (this.txtYear.Focused)
                //        {
                //            this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                //            this.txtMonth.Focus();
                //            return true;
                //        }
                //        else if (this.txtMonth.Focused)
                //        {
                //            this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                //            this.txtDays.Focus();
                //            return true;
                //        }
                //        else if (this.txtDays.Focused)
                //        {
                //            this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                //            this.txtAge.Focus();
                //            return true;
                //        }
                //    }
                //    catch
                //    {
                //        MessageBox.Show("����ǰ��������ڸ�ʽ�������������룡");

                //        if (this.txtMonth.Focused)
                //        {
                //            this.txtDays.Text = "1";
                //            this.txtMonth.Text = "1";

                //            this.txtMonth.SelectAll();
                //        }

                //        if (this.txtDays.Focused)
                //        {
                //            this.txtDays.Text = "1";
                //            this.txtMonth.Text = "1";

                //            this.txtDays.SelectAll();
                //        }


                //        return true;
                //    }
                //    //����������ת����ַ����ϵ�绰
                //    if (this.txtAge.Focused)
                //    {
                //        this.cmbHomeAddress.Focus();
                //        return true;
                //    }
                //    if (this.cmbHomeAddress.Focused)
                //    {
                //        if (isJumpHomePhone)
                //        {
                //            this.txtHomePhone.Focus();
                //            return true;
                //        }
                //        //else
                //        //{
                //        //    this.txtLinkPhone.Focus();
                //        //}

                //        //return true;
                //    }
                //    if (this.txtHomePhone.Focused)
                //    {
                //        if (isJumpHomePhone)
                //        {
                //            if (OnFoucsOver != null)
                //            {
                //                OnFoucsOver(null, null);
                //                return true;
                //            }
                //        }
                //    }

                //    if (this.cmbSex.Focused)
                //    {
                //        if (this.cmbSex.Text.Trim().Length > 0 && this.cmbSex.Text.Trim().Length < 2)
                //        {
                //            try
                //            {
                //                int intsex = int.Parse(this.cmbSex.Text);
                //                switch (intsex)
                //                {
                //                    case 1:
                //                        this.cmbSex.Tag = "M";
                //                        break;
                //                    case 2:
                //                        this.cmbSex.Tag = "F";
                //                        break;
                //                    default:
                //                        break;
                //                }
                //            }
                //            catch
                //            {

                //            }

                //        }
                //    }
                //}

                //

                Control currentContol = this.GetCurrentFoucsControl();

                if (isMustInputTabInde)
                {
                    SetMustInputFocus(currentContol);
                }
                else
                {
                    SendKeys.Send("{Tab}");
                }

                Application.DoEvents();

                #region ��ѯ������Ϣ
                if (isSelectPatientByNameIDCardByEnter)
                {
                    if (currentContol.Name == "txtName" || currentContol.Name == "txtIDNO")
                    {
                        if (OnEnterSelectPatient != null)
                        {
                            this.OnEnterSelectPatient(null, null);
                        }
                    }
                }
                //else if (inpubMaxTabIndex == currentContol.TabIndex)
                //{
                //    if (OnEnterSelectPatient != null)
                //    {
                //        this.OnEnterSelectPatient(null, null);
                //    }
                //}
                #endregion





                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <returns></returns>
        private void ConvertBirthdayByAge(bool isUpdateAgeText)
        {
            DateTime birthDay = this.accountManager.GetDateTimeFromSysDateTime();
            if (birthDay == null || birthDay < new DateTime(1700, 1, 1))
            {
                return;
            }
            string ageStr = this.txtAge.Text.Trim();
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
                this.txtAge.Text = this.GetAge(0, 0, 0);
                return;
            }
            if (isUpdateAgeText)
            {
                this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
                this.txtAge.Text = this.GetAge(iyear, iMonth, iDay);
                this.dtpBirthDay.Value = birthDay;
                this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
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

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            this.ConvertBirthdayByAge(false);
        }


        private void txtAge_Leave(object sender, EventArgs e)
        {
            this.ConvertBirthdayByAge(true);
        }



        #endregion
        /// <summary>
        /// ��ȡ����֤����Ϣ
        /// </summary>
        /// <param name="idCardType"></param>
        /// <param name="idCardNo"></param>
        public void GetIdCardInfo(out string idCardType, out string idCardNo, out string strCardNo)
        {
            idCardType = this.cmbCardType.Tag.ToString(); //֤������
            idCardNo = this.txtIDNO.Text.Trim();          //֤����
            strCardNo = this.cardNO;
        }

        /// <summary>
        /// ��ȡ���滼����Ϣ// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sexCode"></param>
        /// <param name="phone"></param>
        public void GetPatientInfo(out string name, out string sexCode, out string phone)
        {
            name = this.txtName.Text.Trim();       //����
            sexCode = this.cmbSex.Tag.ToString();  //�Ա���
            phone = this.txtHomePhone.Text.Trim();  //�绰
        }

        private void txtBirthDay_TextChanged(object sender, EventArgs e)
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

        private void txtYear_Leave(object sender, EventArgs e)
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
        }

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

        private void txtDays_Leave(object sender, EventArgs e)
        {
            try
            {
                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                this.txtAge.Focus();
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

        private void txtIDNO_Leave(object sender, EventArgs e)
        {
            try
            {

                if (isJudgePactByIdno)
                {
                    this.JudgePactByIdno();
                }

                if (this.cmbCardType.Tag.ToString() == "01")
                {
                    string error = string.Empty;
                    string idNO = this.txtIDNO.Text.Trim();
                    if (idNO != string.Empty)
                    {

                        if (this.cmbCardType.Tag != null && this.cmbCardType.Tag.ToString() == "01" && this.txtIDNO.Text.Trim() != string.Empty)
                        {
                            string err = string.Empty;
                            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIDNO.Text.Trim(), ref err) < 0)
                            {
                                //if (isTooltipCARDID == 1)// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
                                //{
                                //    string strMsg = Language.Msg(err + "\r\n�Ƿ������");
                                //    if (MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                //    {
                                //        this.txtIDNO.Focus();
                                //        return;
                                //    }
                                //}
                                //else if (isTooltipCARDID == 2)
                                //{

                                //    string strMsg = Language.Msg(err + "");

                                //    MessageBox.Show(strMsg, "ϵͳ��ʾ");
                                //    this.txtIDNO.Focus();
                                //    return;
                                //}
                                //else
                                //{
                                //}

                                return;
                            }
                        }
                        //�������֤�Ż�ȡ�����Ա�
                        FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(idNO, ref error);
                        if (obj == null)
                        {
                            return;
                        }
                        if (!string.IsNullOrEmpty(obj.ID))
                        {
                            this.cmbSex.Tag = obj.ID;
                        }

                        //���ݻ������֤�Ż�ȡ����
                        string birthdate = Class.Function.GetBirthDayFromIdNO(idNO, ref error);
                        if (birthdate == "-1")
                        {
                            return;
                        }
                        if (!string.IsNullOrEmpty(birthdate))
                        {
                            this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);


                        }
                        else
                        {
                            this.dtpBirthDay.Value = DateTime.Now;

                        }

                    }
                }


            }

            catch
            {
                MessageBox.Show("���֤��ʽ����ȷ��");
                //this.txtIDNO.Focus();
                return;
            }
        }

        public void JudgePactByIdno()
        {
            #region �������֤���ж��Ƿ����ܾ���ҽ��
            if (changePactId == "")
            {
                MessageBox.Show("��ά��ת����ҽ��ʱ��ҽ����ͬ��λ");
                return;
            }
            FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
            r.IDCard = this.txtIDNO.Text.Trim();
            r.Pact.ID = this.changePactId.ToString();
            if (!string.IsNullOrEmpty(r.IDCard) && !string.IsNullOrEmpty(r.Pact.ID))
            {
                int iRes = this.medcareProxy.SetPactCode(r.Pact.ID);
                if (iRes != 1)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    return;
                }

                iRes = this.medcareProxy.QueryCanMedicare(r);
                //if (this.medcareProxy.ErrMsg!="")
                //{
                //    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg), "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //������ҽ���򲻱�
                if (iRes == -2 || iRes == -1)
                {
                    return;
                }

                ArrayList al = new ArrayList();
                //����ҽ�����ж��Ƿ��Ƿ���һ�����������
                if (this.isMutilPactInfo)
                {
                    al = ((ComboBoxPactSelect)this.ComPact).GetValues();
                    if (al != null)
                    {
                        //����Ѿ��ڶ����ѡ�����򲻼���,���û�������
                        bool isSelect = false;
                        foreach (FS.HISFC.Models.Base.PactInfo pactinfo in al)
                        {
                            if (pactinfo.ID == this.changePactId)
                            {
                                isSelect = true;
                                break;
                            }
                        }
                        if (!isSelect)
                        {
                            this.comboBoxPactSelect1.Tag = this.changePactId;
                        }
                    }
                }
                //����һ����������̣���Ѻ�ͬ��λ��cmb�ĳɾ���ҽ��
                else
                {
                    this.cmbPact.Tag = this.changePactId;
                }
            }

            #endregion
        }

        private void clearPhotoStrip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�Ƿ�ȷ����ո���Ƭ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                FrameWork.Management.PublicTrans.BeginTransaction();
                this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (this.accountManager.DeletePhoto(this.patientInfo) == -1)
                {
                    MessageBox.Show("ɾ����Ƭʧ��" + this.accountManager.Err);
                    FrameWork.Management.PublicTrans.RollBack();
                }
                FrameWork.Management.PublicTrans.Commit();
                this.pictureBox.Image = null;
                this.pictureBox.Tag = null;
            }
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// 
        public void SetPatient(FS.HISFC.Models.RADT.PatientInfo patInfo)
        {
            //modify by sung 2009-2-24 {DCAA485E-753C-41ed-ABCF-ECE46CD41B33}
            if (patInfo.IsEncrypt)
            {
                patInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patInfo.NormalName);
            }
            this.txtName.Text = patInfo.Name;//��������

            this.cmbSex.Text = patInfo.Sex.Name;            //�Ա�
            this.cmbSex.Tag = patInfo.Sex.ID;               //�Ա�
            //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//����
            if (patInfo.Birthday > DateTime.MinValue)
            {
                this.dtpBirthDay.Value = patInfo.Birthday;      //��������
            }
            else
            {
                this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();      //��������
            }
            //this.cmbDistrict.Text = this.patientInfo.DIST;            //����
            //this.cmbProfession.Tag = this.patientInfo.Profession.ID; //ְҵ
            this.txtIDNO.Text = patInfo.IDCard;             //���֤��
            this.cmbWorkAddress.Text = patInfo.CompanyName; //������λ
            this.txtWorkPhone.Text = patInfo.PhoneBusiness; //��λ�绰
            //this.cmbMarry.Tag = patInfo.MaritalStatus.ID.ToString();//����״��
            this.txtNowHome.Text = patInfo.User01;  //��סַ
            this.cmbHomeAddress.Text = patInfo.AddressHome;  //��ͥסַ
            this.txtHomePhone.Text = patInfo.PhoneHome;     //��ͥ�绰
            this.txtLinkMan.Text = patInfo.Kin.Name;        //��ϵ�� 
            this.cmbLinkAddress.Text = patInfo.Kin.RelationAddress;//��ϵ�˵�ַ
            this.txtLinkPhone.Text = patInfo.Kin.RelationPhone;//��ϵ�˵绰
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
            cbxChild.Checked = patInfo.ChildFlag == "1";

            //this.cmbCardType.Tag = patInfo.IDCardType.ID; //֤������
        }

        /// <summary>
        ///   //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//�������������̱����������̱���˾
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPact.Tag.ToString() == "2")
            {
                lblcompany.Visible = cmbCompany.Visible = true;
                cmbPact.Width = 120;
            }
            else
            {
                lblcompany.Visible = cmbCompany.Visible = false;
                cmbPact.Width = 350;
            }

            PatientPactInfo = this.cmbPact.SelectedItem as FS.HISFC.Models.Base.PactInfo; //{fa76c153-d727-439d-9e93-aa86bfad208b}
        }


    }
}