using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Management;

namespace UFC.Pharmacy
{
    /// <summary>
    /// [��������: ������������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucIMAInOutBase : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucIMAInOutBase()
        {
            InitializeComponent();

            this.txtFilter.Width = 120;

            this.lbTotCost.Location = new Point(188, 15);

            this.lbInfo.Location = new Point(548, 15);            
        }

        public delegate void SetToolButtonVisibleHandler(bool isShowApply, bool isShowIn, bool isShowOut, bool isShowStock, bool isShowDel, bool isShowExport, bool isShowImport);

        public delegate void AddToolButtonHandler(string text, string toolstrip, System.Drawing.Image image,int location,bool isAddSeparator, System.EventHandler e);

        public delegate void DataChangedHandler(Neusoft.NFC.Object.NeuObject changeData, object param);

        public delegate void FpKeyHandler(Keys key);

        public event DataChangedHandler BeginTargetChanged;

        public event DataChangedHandler EndTargetChanged;

        public event DataChangedHandler BeginPersonChanged;

        public event DataChangedHandler EndPersonChanged;

        public event DataChangedHandler BeginPrivChanged;

        public event DataChangedHandler EndPrivChanged;

        public event FpKeyHandler FpKeyEvent;

        public event AddToolButtonHandler AddToolButtonEvent;

        public event SetToolButtonVisibleHandler SetToolButtonVisibleEvent;

        /// <summary>
        /// Ȩ�޹�����
        /// </summary>
        protected Neusoft.HISFC.Management.Manager.UserPowerDetailManager powerDetailManager = new Neusoft.HISFC.Management.Manager.UserPowerDetailManager();

        #region �����

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.NFC.Object.NeuObject operInfo = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject deptInfo = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// �����ļ�����·��
        /// </summary>
        private string filePath = "";

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        private Neusoft.NFC.Object.NeuObject class2Priv = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ��ǰѡ��Ĳ�������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject privType = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ����Ŀ�����
        /// </summary>
        private Neusoft.NFC.Object.NeuObject targetDept = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ����Ŀ��������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject targetPerson = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ���������ļ��ڵ�Ŀ�굥λ��Ϣ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject localTargetDept = null;

        /// <summary>
        /// ���������ļ��ڵ�Ŀ����Ա��Ϣ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject localTargetPerson = null;

        /// <summary>
        /// �Ƿ��FarPoint��ʾ������� 
        /// </summary>
        private bool isClear = true;

        /// <summary>
        /// Ȩ�޼���(����Ȩ�� �û���������)
        /// </summary>
        private List<Neusoft.NFC.Object.NeuObject> privList = new List<Neusoft.NFC.Object.NeuObject>();

        /// <summary>
        /// Ŀ�����
        /// </summary>
        ArrayList alTargetDept = new ArrayList();

        /// <summary>
        /// Ŀ��������
        /// </summary>
        ArrayList alTargetPerson = new ArrayList();

        /// <summary>
        /// �˳�ʱ�Ƿ񱣴�Ȩ������
        /// </summary>
        private bool isSaveDefaultPriv = true;

        /// <summary>
        /// ������λ����Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        private bool isSetDefaultTargetDept = true;

        /// <summary>
        /// ��ҩ������Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        private bool isSetDefaultTargetPerson = false;

        #endregion

        #region �ⲿ�������� ����������

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        [Description("������ʾ��ʾ��Ϣ"),Category("����")]
        public string ShowInfo
        {
            get
            {
                return this.lbInfo.Text;
            }
            set
            {
                this.lbInfo.Text = value;
            }
        }

        /// <summary>
        /// �Ƿ�����Բ������Ŀ�굥λ����ѡ����Ӧ
        /// </summary>
        [Description("�Ƿ����ѡ���������Ŀ�굥λ"), Category("����")]
        public bool LabelValid
        {
            get
            {
                return this.lnbTarget.Enabled;
            }
            set
            {
                this.lnbTarget.Enabled = value;         //Ŀ�굥λ
                this.lnbTargetPerson.Enabled = value;         //������
            }
        }

        /// <summary>
        /// �����ļ�·������
        /// </summary>
        [Description("�����ļ�����·�� ��Ե�ַ(��������Ŀ¼��"), Category("����")]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;                
            }
        }

        /// <summary>
        /// ��Ŀ�굥λ����ʾ����
        /// </summary>
        [Description("��Ŀ�굥λ����ʾ����"), Category("����")]
        public string LinkLabelTarget
        {
            get
            {
                return this.lnbTarget.Text;
            }
            set
            {
                this.lnbTarget.Text = value;
            }
        }

        /// <summary>
        /// �������˵���ʾ����
        /// </summary>
        [Description("�������˵���ʾ����"), Category("����")]
        public string LinkLabelPerson
        {
            get
            {
                return this.lnbTargetPerson.Text;
            }
            set
            {
                this.lnbTargetPerson.Text = value;
            }
        }

        /// <summary>
        /// �˳�ʱ�Ƿ񱣴�Ȩ������
        /// </summary>
        [Description("�˳�ʱ�Ƿ񱣴�Ȩ������"), Category("����"),DefaultValue(true)]
        public bool IsSaveDefaultPriv
        {
            get
            {
                return this.isSaveDefaultPriv;
            }
            set
            {
                this.isSaveDefaultPriv = value;
            }
        }

        /// <summary>
        /// ������λ����Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        [Description("������λ����Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ"), Category("����"),DefaultValue(true)]
        public bool IsSetDefaultTargetDept
        {
            get
            {
                return isSetDefaultTargetDept;
            }
            set
            {
                isSetDefaultTargetDept = value;
            }
        }

        /// <summary>
        /// ��ҩ������Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        [Description("��ҩ������Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ"), Category("����"), DefaultValue(false)]
        public bool IsSetDefaultTargetPerson
        {
            get
            {
                return isSetDefaultTargetPerson;
            }
            set
            {
                isSetDefaultTargetPerson = value;
            }
        }

        #endregion

        #region �ڲ�ʹ������ ������ʹ��

        /// <summary>
        /// �Ƿ���ʾ�ϲ���������Panel
        /// </summary>
        internal bool IsShowInputPanel
        {
            get
            {
                return this.panelItemManager.Visible;
            }
            set
            {
                this.panelItemManager.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��Ŀѡ��Panel
        /// </summary>
        internal bool IsShowItemSelectpanel
        {
            get
            {
                return this.panelItemSelect.Visible;
            }
            set
            {
                this.panelItemSelect.Visible = value;
            }
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        internal Neusoft.NFC.Object.NeuObject Class2Priv
        {
            get
            {
                return this.class2Priv;
            }
            set
            {
                this.class2Priv = value;
            }
        }

        /// <summary>
        /// ������  ID ���� Name ���� Memo ϵͳ����
        /// </summary>
        internal Neusoft.NFC.Object.NeuObject PrivType
        {
            get
            {
                return this.privType;
            }
            set
            {
                this.privType = value;
                if (value != null)
                {
                    this.txtPrivType.Text = value.Name;
                    this.cmbPrivType.SelectedIndexChanged -= new EventHandler(cmbPrivType_SelectedIndexChanged);
                    this.cmbPrivType.Text = value.Name;
                    this.cmbPrivType.SelectedIndexChanged += new EventHandler(cmbPrivType_SelectedIndexChanged);
                }
            }
        }

        /// <summary>
        /// Ŀ�굥λ ID ���� Name ���� Memo 0 Ŀ�굥λΪ�ڲ����� 1 Ŀ�굥λΪ�ⲿ������˾
        /// </summary>
        internal Neusoft.NFC.Object.NeuObject TargetDept
        {
            get
            {
                return targetDept;
            }
            set
            {
                this.targetDept = value;
                if (value != null)
                {
                    this.txtTargetDept.Text = this.targetDept.Name;
                    this.cmbTargetDept.Text = value.Name;
                }
            }
        }

        /// <summary>
        /// Ŀ���� ID ���� Name ����
        /// </summary>
        internal Neusoft.NFC.Object.NeuObject TargetPerson
        {
            get
            {
                return this.targetPerson;
            }
            set
            {
                this.targetPerson = value;
                if (value != null)
                {
                    this.txtTargetPerson.Text = this.targetPerson.Name;
                    this.cmbTargetPerson.Text = value.Name;
                }
            }
        }

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        internal Neusoft.NFC.Object.NeuObject OperInfo
        {
            get
            {
                return this.operInfo;
            }
            set
            {
                this.operInfo = value;
            }
        }

        /// <summary>
        /// ��ǰ��½����
        /// </summary>
        internal Neusoft.NFC.Object.NeuObject DeptInfo
        {
            get
            {
                return this.deptInfo;
            }
            set
            {
                this.deptInfo = value;
            }
        }

        /// <summary>
        /// �Ƿ��FarPoint��ʾ������� 
        /// </summary>
        internal bool IsClear
        {
            get
            {
                return this.isClear;
            }
            set
            {
                this.isClear = value;
            }
        }

        /// <summary>
        /// �ܽ����ʾ
        /// </summary>
        internal string TotCostInfo
        {
            get
            {
                return this.lbTotCost.Text;
            }
            set
            {
                this.lbTotCost.Text = value;
            }
        }

        #endregion

        #region ������

        private Neusoft.NFC.Interface.Forms.ToolBarService toolBarService = new Neusoft.NFC.Interface.Forms.ToolBarService();

        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ɾ  ��", "ɾ����ϸ��Ϣ", Neusoft.NFC.Interface.Classes.EnumImageList.Aɾ��, true, false, null);
            toolBarService.AddToolButton("���뵥", "��ʾ������Ϣ", Neusoft.NFC.Interface.Classes.EnumImageList.A�½�, true, false, null);
            toolBarService.AddToolButton("��ⵥ", "��ʾ��ⵥ��", Neusoft.NFC.Interface.Classes.EnumImageList.A��Ϣ, true, false, null);
            toolBarService.AddToolButton("���ⵥ", "��ʾ���ⵥ��", Neusoft.NFC.Interface.Classes.EnumImageList.A�޸�, true, false, null);
            toolBarService.AddToolButton("�ɹ���", "��ʾ�ɹ�����", Neusoft.NFC.Interface.Classes.EnumImageList.A������Ϣ, true, false, null);
            toolBarService.AddToolButton("��  ��", "���������Ϣ", Neusoft.NFC.Interface.Classes.EnumImageList.A���, true, false, null);
            return toolBarService;
        }

        /// <summary>
        /// ���Ӱ�ť  ԭToolBarService��ʽ �÷�ʽ�޷���Ӱ�ť 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toolTrip"></param>
        /// <param name="imngeInex"></param>
        /// <param name="isEnabled"></param>
        /// <param name="isChecked"></param>
        /// <param name="e"></param>
        public void AddToolBarButton(string text,string toolTrip,int imngeInex,bool isEnabled,bool isChecked,EventHandler e)
        {
            this.toolBarService.AddToolButton(text, toolTrip, imngeInex, isEnabled, isChecked, e);          
        }

        /// <summary>
        /// ���Ӱ�ť
        /// </summary>
        /// <param name="text">��ť����</param>
        /// <param name="toolTrip">��ʾ��Ϣ</param>
        /// <param name="imageEnum">��ťͼ��</param>
        /// <param name="location">λ������</param>
        /// <param name="isAddSeparator">�Ƿ��ٰ�ťǰ�����ӷָ���</param>
        /// <param name="eFun">��ť�������ί��</param>
        public void AddToolBarButton(string text, string toolTrip, Neusoft.NFC.Interface.Classes.EnumImageList imageEnum,int location,bool isAddSeparator,System.EventHandler eFun)
        {
            if (this.AddToolButtonEvent != null)
            {
                System.Drawing.Image image = Neusoft.NFC.Interface.Classes.Function.GetImage(imageEnum);
                this.AddToolButtonEvent(text, toolTrip, image, location, isAddSeparator, eFun);
            }
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "���뵥")
            {
                this.OnApplyList();
            }
            if (e.ClickedItem.Text == "��ⵥ")
            {
                this.OnInList();
            }
            if (e.ClickedItem.Text == "���ⵥ")
            {
                this.OnOutList();
            }
            if (e.ClickedItem.Text == "�ɹ���")
            {
                this.OnStockList();
            }
            if (e.ClickedItem.Text == "ɾ  ��")
            {
                this.OnDelete();
            }
            if (e.ClickedItem.Text == "��  ��")
            {
                this.OnImport();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ���ù�������ť��ʾ���  ԭToolBarService��ʽ �ú����޷�ʵ����Ӧ����
        /// </summary>
        /// <param name="isShowApplyButton"></param>
        /// <param name="isShowInButton"></param>
        /// <param name="isShowOutButton"></param>
        /// <param name="isShowStockButton"></param>
        /// <param name="isShowDelButton"></param>
        /// <param name="isShowImportButton"></param>
        public virtual void SetToolBarButton(bool isShowApplyButton, bool isShowInButton, bool isShowOutButton, bool isShowStockButton, bool isShowDelButton,bool isShowImportButton)
        {
            this.toolBarService.SetToolButtonEnabled("���뵥", isShowApplyButton);
            this.toolBarService.SetToolButtonEnabled("��ⵥ", isShowInButton);
            this.toolBarService.SetToolButtonEnabled("���ⵥ", isShowOutButton);
            this.toolBarService.SetToolButtonEnabled("�ɹ���", isShowStockButton);
            this.toolBarService.SetToolButtonEnabled("ɾ  ��", isShowDelButton);
            this.toolBarService.SetToolButtonEnabled("��  ��", isShowImportButton);
        }

        /// <summary>
        /// ���ù�������ť��ʾ��� ԭToolBarService��ʽ �ú����޷�ʵ����Ӧ����
        /// </summary>
        /// <param name="isShowApplyButton"></param>
        /// <param name="isShowInButton"></param>
        /// <param name="isShowOutButton"></param>
        /// <param name="isShowStockButton"></param>
        /// <param name="isShowDelButton"></param>
        public virtual void SetToolBarButton(bool isShowApplyButton, bool isShowInButton, bool isShowOutButton, bool isShowStockButton, bool isShowDelButton)
        {
            this.SetToolBarButton(isShowApplyButton, isShowInButton, isShowOutButton, isShowStockButton, isShowDelButton, false);
        }

        /// <summary>
        /// ���ù�������ť��ʾ���
        /// </summary>
        /// <param name="isShowApplyButton"></param>
        /// <param name="isShowInButton"></param>
        /// <param name="isShowOutButton"></param>
        /// <param name="isShowStockButton"></param>
        /// <param name="isShowDelButton"></param>
        /// <param name="isShowExport"></param>
        /// <param name="isShowImport"></param>
        public virtual void SetToolBarButtonVisible(bool isShowApplyButton, bool isShowInButton, bool isShowOutButton, bool isShowStockButton, bool isShowDelButton,bool isShowExport,bool isShowImport)
        {
            if (this.SetToolButtonVisibleEvent != null)
            {
                this.SetToolButtonVisibleEvent(isShowApplyButton, isShowInButton, isShowOutButton, isShowStockButton, isShowDelButton,isShowExport, isShowImport);
            }
        }


        /// <summary>
        /// ���뵥����Ϣ
        /// </summary>
        internal virtual void OnApplyList()
        {
            
        }

        /// <summary>
        /// ��ⵥ����Ϣ
        /// </summary>
        internal virtual void OnInList()
        {
 
        }

        /// <summary>
        /// ���ⵥ����Ϣ
        /// </summary>
        internal virtual void OnOutList()
        {

        }

        /// <summary>
        /// �ɹ�������Ϣ
        /// </summary>
        internal virtual void OnStockList()
        {
 
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        internal virtual void OnDelete()
        {
 
        }

        /// <summary>
        /// ���ݵ���
        /// </summary>
        internal virtual void OnImport()
        {
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        internal virtual int OnExport()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("����ɹ�"));
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.OnExport();

            return base.Export(sender, neuObject);
        }

        #endregion

        /// <summary>
        /// �����Ŀ��ʾ¼��UC
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddItemInputUC(Neusoft.NFC.Interface.Controls.ucBaseControl ucItemInput)
        {
            if (ucItemInput == null)
            {
                this.panelItemManager.Visible = false;
            }
            else
            {
                this.panelItemManager.Controls.Clear();

                this.panelItemManager.Controls.Add(ucItemInput);

                this.panelItemManager.Size = ucItemInput.Size;

                ucItemInput.Dock = DockStyle.Fill;
            }

            return 1;
        }

        /// <summary>
        /// ����Ŀ�����
        /// </summary>
        /// <param name="isCompany">Ŀ�굥λ�Ƿ�Ϊ������˾ True ������˾ False Ժ�ڿ��� ��ΪTrueʱʣ�����������</param>
        /// <param name="isPrivInOut">�Ƿ�ά�������������б� ��ΪTrueʱ �������Ͳ���������</param>
        /// <param name="companyType">������˾���� ����/ҩƷ/�豸</param>
        /// <param name="deptType">Ժ�ڿ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SetTargetDept(bool isCompany,bool isPrivInOut,Neusoft.HISFC.Object.IMA.EnumModuelType companyType, Neusoft.HISFC.Object.Base.EnumDepartmentType deptType)
        {
            this.alTargetDept.Clear();

            this.lnbTarget.Visible = true;
            this.txtTargetDept.Visible = true;
            this.cmbTargetDept.Visible = true;

            if (isCompany)
            {
                #region ���ع�����˾

                switch (companyType)
                {                   
                    case Neusoft.HISFC.Object.IMA.EnumModuelType.Equipment:         //�豸
                        break;
                    case Neusoft.HISFC.Object.IMA.EnumModuelType.Material:          //����
                        break;
                    case Neusoft.HISFC.Object.IMA.EnumModuelType.Phamacy:           //ҩƷ
                        Neusoft.HISFC.Management.Pharmacy.Constant phaConstantManager = new Neusoft.HISFC.Management.Pharmacy.Constant();
                        this.alTargetDept = phaConstantManager.QueryCompany("1");
                        if (this.alTargetDept == null)
                        {
                            MessageBox.Show(Neusoft.NFC.Management.Language.Msg("����ҩƷ������˾�б�ʧ��" + phaConstantManager.Err));
                            return -1;
                        }
                        break;
                }

                foreach (Neusoft.NFC.Object.NeuObject info in this.alTargetDept)
                {
                    info.Memo = "1";
                }

                #endregion
            }
            else
            {
                if (isPrivInOut)        //Ȩ�޿���
                {
                    #region ȡ���Ȩ�޿���

                    ArrayList tempAl;
                    Neusoft.HISFC.Management.Manager.PrivInOutDept privInOutManager = new Neusoft.HISFC.Management.Manager.PrivInOutDept();
                    tempAl = privInOutManager.GetPrivInOutDeptList(this.deptInfo.ID, this.class2Priv.ID);
                    if (tempAl == null)
                    {
                        MessageBox.Show(Neusoft.NFC.Management.Language.Msg(privInOutManager.Err));
                        return -1;
                    }
                    //��privInOutDeptת��Ϊneuobject�洢
                    Neusoft.NFC.Object.NeuObject offerInfo;
                    Neusoft.HISFC.Object.Base.PrivInOutDept privInOutDept;
                    for (int i = 0; i < tempAl.Count; i++)
                    {
                        privInOutDept = tempAl[i] as Neusoft.HISFC.Object.Base.PrivInOutDept;
                        offerInfo = new Neusoft.NFC.Object.NeuObject();
                        offerInfo.ID = privInOutDept.Dept.ID;			    //������λ����
                        offerInfo.Name = privInOutDept.Dept.Name;		    //������λ����
                        offerInfo.Memo = privInOutDept.Memo;		    //��ע

                        this.alTargetDept.Add(offerInfo);
                    }					

                    #endregion
                }
                else
                {
                    #region ���ݿ�������ȡԺ�ڿ���

                    Neusoft.HISFC.Integrate.Manager managerIntegrate = new Neusoft.HISFC.Integrate.Manager();
                    this.alTargetDept = managerIntegrate.GetDepartment(deptType);
                    if (this.alTargetDept == null)
                    {
                        MessageBox.Show(Neusoft.NFC.Management.Language.Msg("���ݿ�������ȡ�����б�������") + managerIntegrate.Err);
                        return -1;
                    }
                    foreach (Neusoft.NFC.Object.NeuObject info in this.alTargetDept)
                    {
                        info.Memo = "0";
                    }

                    #endregion
                }
            }

            if (this.isSetDefaultTargetDept)
            {
                if (this.targetDept.ID == "")
                {
                    if (this.alTargetDept.Count > 0)
                    {
                        if (this.localTargetDept != null && this.localTargetDept.ID != "")
                        {
                            this.TargetDept = this.localTargetDept;
                            this.localTargetDept = null;
                        }
                        else
                        {
                            this.TargetDept = this.alTargetDept[0] as Neusoft.NFC.Object.NeuObject;
                        }
                    }
                }             
            }

            this.cmbTargetDept.AddItems(this.alTargetDept);
            this.cmbTargetDept.SelectedIndexChanged -= new EventHandler(cmbTargetPerson_SelectedIndexChanged);
            this.cmbTargetDept.Text = this.targetDept.Name;
            this.cmbTargetDept.SelectedIndexChanged += new EventHandler(cmbTargetPerson_SelectedIndexChanged);

            return 1;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="isAllEmployee">������Ա ����ΪTrueʱ employeeType����������</param>
        /// <param name="employeeType">��Ա���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SetTargetPerson(bool isAllEmployee,Neusoft.HISFC.Object.Base.EnumEmployeeType employeeType)
        {
            this.alTargetPerson.Clear();

            this.lnbTargetPerson.Visible = true;
            this.txtTargetPerson.Visible = true;
            this.cmbTargetPerson.Visible = true;

            Neusoft.HISFC.Integrate.Manager managerIntegrate = new Neusoft.HISFC.Integrate.Manager();
            if (isAllEmployee)
                this.alTargetPerson = managerIntegrate.QueryEmployeeAll();
            else
                this.alTargetPerson = managerIntegrate.QueryEmployee(employeeType);

            if (this.alTargetPerson == null)
            {
                MessageBox.Show(Neusoft.NFC.Management.Language.Msg("������Ա����ȡ��Ա�б�������") + managerIntegrate.Err);
                return -1;
            }

            if (this.isSetDefaultTargetPerson)
            {
                if (this.targetPerson.ID == "")
                {
                    if (this.alTargetPerson.Count > 0)
                    {
                        if (this.localTargetPerson != null && this.localTargetPerson.ID != "")
                        {
                            this.TargetPerson = this.localTargetPerson;
                            this.localTargetPerson = null;
                        }
                        else
                        {
                            this.TargetPerson = this.alTargetPerson[0] as Neusoft.NFC.Object.NeuObject;
                        }
                    }
                }
            }

            this.cmbTargetPerson.AddItems(this.alTargetPerson);           
            this.cmbTargetPerson.Text = this.TargetPerson.Name;

            return 1;
        }

        /// <summary>
        /// ����Ȩ�����
        /// </summary>
        /// <param name="isUseLocal">�Ƿ�Ĭ��Ϊ��һ��ѡ���Ȩ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SetPrivType(bool isUseLocal)
        {
            if (this.privList.Count == 0)
            {
                if (this.InitPrivType() == -1)
                {
                    return -1;
                }
            }

            this.FilterPriv(ref this.privList);

            if (this.privList.Count == 0)
            {
                MessageBox.Show(Language.Msg("�����κβ���Ȩ�� ������ظ�������ϵ��ȡ��Ȩ"));
                return -1;
            }

            this.cmbPrivType.AddItems(new ArrayList(this.privList.ToArray()));

            if (isUseLocal)
            {
                #region ��ȡ���������ļ���ȡ��һ��ѡ���Ȩ��

                try
                {
                    if (System.IO.File.Exists(Application.StartupPath + this.filePath))
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(Application.StartupPath + this.filePath);
                        System.Xml.XmlNode operNode = doc.SelectSingleNode("/Setting/DeptInfo[@DeptID = '" + this.deptInfo.ID + "']/OperInfo[@OperID = '" + this.operInfo.ID + "']");
                        if (operNode != null)
                        {
                            #region �ҵ��ò���Ա��Ϣ

                            string xmlPrivId = operNode.ChildNodes[0].Attributes["ID"].Value.ToString();
                            foreach (Neusoft.NFC.Object.NeuObject temp in this.privList)
                            {
                                if (temp.ID == xmlPrivId)
                                {
                                    //����Ա�Ծ����ϴε����Ȩ��  ���ò���������ʾ
                                    this.PrivType = temp;

                                    //���ù�����λ
                                    this.targetDept.ID = operNode.ChildNodes[1].Attributes["ID"].Value.ToString();
                                    this.targetDept.Name = operNode.ChildNodes[1].Attributes["Name"].Value.ToString();
                                    this.txtTargetDept.Text = this.targetDept.Name;
                                    //���汾�������ļ��ڵĹ�����λ��Ϣ
                                    this.localTargetDept = this.targetDept;

                                    this.targetPerson.ID = operNode.ChildNodes[2].Attributes["ID"].Value.ToString();
                                    this.targetPerson.Name = operNode.ChildNodes[2].Attributes["Name"].Value.ToString();
                                    this.txtTargetPerson.Text = this.targetPerson.Name;
                                    //���汾�������ļ��ڵ���������Ϣ 
                                    this.localTargetPerson = this.targetPerson;

                                    return 1;
                                }
                            }
                            #endregion

                            if (this.PrivType.ID == "")
                            {
                                Neusoft.NFC.Object.NeuObject info = this.privList[0];
                                this.PrivType = info;
                            }
                        }
                        else
                        {
                            Neusoft.NFC.Object.NeuObject info = this.privList[0];
                            this.PrivType = info;
                        }
                    }
                    else
                    {
                        Neusoft.NFC.Object.NeuObject info = this.privList[0];
                        this.PrivType = info;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg(ex.Message));
                    return -1;
                }

                #endregion
            }
            else
            {
                Neusoft.NFC.Object.NeuObject info = this.privList[0];
                this.PrivType = info;
            }

            return 1;
        }

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void InitControlParam()
        {
            Neusoft.HISFC.Integrate.Common.ControlParam ctrlParamIntegrate = new Neusoft.HISFC.Integrate.Common.ControlParam();

            this.IsSaveDefaultPriv = ctrlParamIntegrate.GetControlParam<bool>(Neusoft.HISFC.Integrate.PharmacyConstant.In_Save_Priv, true, true);
        }

        #region Ȩ�޳�ʼ��������

        /// <summary>
        /// ��ʼ��Ȩ������
        /// </summary>
        /// <returns>�ɹ�����Ȩ��ѡ�񷵻�1 ���򷵻�-1</returns>
        protected int InitPrivType()
        {
            #region ��Ч���ж�

            if (this.Class2Priv.ID == "")
            {
                MessageBox.Show(Language.Msg("δ��ȡ��ǰ�����Ķ���Ȩ�ޱ���"));
                return -1;
            }
            if (this.DeptInfo.ID == "")
            {
                MessageBox.Show(Language.Msg("δ��ȡ��ǰ�������ұ���"));
                return -1;
            }
            if (this.OperInfo.ID == "")
            {
                MessageBox.Show(Language.Msg("δ��ȡ��ǰ����Ա����"));
                return -1;
            }

            #endregion

            #region ��ȡ��ǰ����Ա���е�Ȩ�޼���

            this.privList = this.powerDetailManager.QueryUserPrivCollection(this.OperInfo.ID, this.Class2Priv.ID, this.DeptInfo.ID);
            if (this.privList == null)
            {
                MessageBox.Show(Language.Msg("��ȡ����Ա����Ȩ�޼���ʱ����\n" + this.powerDetailManager.Err));
                return -1;
            }

            #endregion

            #region ��ȡ����Ȩ�޺�����

            Neusoft.HISFC.Object.Admin.PowerLevelClass3 privClass3;

            Neusoft.HISFC.Management.Manager.PowerLevelManager powerLevelManager = new Neusoft.HISFC.Management.Manager.PowerLevelManager();
            foreach (Neusoft.NFC.Object.NeuObject info in this.privList)
            {
                privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey(this.Class2Priv.ID, info.ID);
                if (privClass3 == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ����Ȩ�޺��������\n��������Ȩ�޺��������" + powerLevelManager.Err));
                    return -1;
                }
                info.Memo = privClass3.Class3MeaningCode;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// Ȩ�޹���
        /// </summary>
        protected virtual void FilterPriv(ref List<Neusoft.NFC.Object.NeuObject> privList)
        {
        }

        /// <summary>
        /// Ȩ��ѡ�� 
        /// </summary>
        /// <returns>1 �ɹ�ѡ����Ȩ�� 0 δѡ���κ�Ȩ�޻�Ȩ��δ�����仯 -1 ��������</returns>
        public int SelectPriv()
        {
            if (this.privList.Count == 0)
                this.InitPrivType();

            Neusoft.NFC.Object.NeuObject tempPriv = new Neusoft.NFC.Object.NeuObject();
            if (Neusoft.NFC.Interface.Classes.Function.ChooseItem(new ArrayList(this.privList.ToArray()), ref tempPriv) == 0)
            {
                return 0;
            }
            else
            {
                if (tempPriv.ID == this.privType.ID)
                {
                    return 0;
                }
                else
                {
                    this.privType = tempPriv;
                    return 1;
                }
            }
        }

        /// <summary>
        /// �������Ա���һ��ѡ��Ĳ������͡�Ŀ�굥λд�������ļ�
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SavePriv()
        {
            try
            {
                if (this.filePath == "")
                {
                    return 1;
                }

                Neusoft.NFC.Xml.XML myXml = new Neusoft.NFC.Xml.XML();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlElement root;
                if (System.IO.File.Exists(Application.StartupPath + this.filePath))
                {		//�ļ��Ѵ���
                    doc.Load(Application.StartupPath + this.filePath);
                    root = (System.Xml.XmlElement)doc.SelectSingleNode("Setting");
                }
                else
                {											//�ļ�������
                    //���ø����
                    root = myXml.CreateRootElement(doc, "Setting", "1.0");
                }

                System.Xml.XmlNode deptNode = doc.SelectSingleNode("/Setting/DeptInfo[@DeptID = '" + this.deptInfo.ID + "']");
                if (deptNode != null)           //���ڿ��ҽڵ�
                {
                    System.Xml.XmlNode operNode = doc.SelectSingleNode("/Setting/DeptInfo[@DeptID = '" + this.deptInfo.ID + "']/OperInfo[@OperID = '" + this.operInfo.ID + "']");
                    if (operNode != null)       //���ڲ���Ա�ڵ�
                    {
                        //�����������
                        operNode.ChildNodes[0].Attributes["ID"].Value = this.PrivType.ID;
                        operNode.ChildNodes[0].Attributes["Name"].Value = this.privType.Name;
                        //����Ŀ�����
                        operNode.ChildNodes[1].Attributes["ID"].Value = this.targetDept.ID;
                        operNode.ChildNodes[1].Attributes["Name"].Value = this.targetDept.Name;
                        //����������
                        operNode.ChildNodes[2].Attributes["ID"].Value = this.targetPerson.ID;
                        operNode.ChildNodes[2].Attributes["Name"].Value = this.targetPerson.Name;
                    }
                    else                       //�����ڲ���Ա�ڵ�
                    {
                        #region ��Ӳ���Ա�ڵ�

                        //�ڸÿ�����δ�ҵ��ò���Ա��ʷ��Ϣ ��Ӹò���Ա��Ϣ
                        System.Xml.XmlElement xmlNewOper = doc.CreateElement("OperInfo");
                        xmlNewOper.SetAttribute("OperID", this.operInfo.ID);
                        //�����������ӽڵ� ����ֵ
                        System.Xml.XmlElement xmlPriv = doc.CreateElement("PrivType");
                        xmlPriv.SetAttribute("ID", this.privType.ID);
                        xmlPriv.SetAttribute("Name", this.privType.Name);
                        xmlNewOper.AppendChild(xmlPriv);
                        //��ӹ�����λ�ӽڵ� ����ֵ
                        System.Xml.XmlElement xmlOffer = doc.CreateElement("TargetDept");
                        xmlOffer.SetAttribute("ID", this.targetDept.ID);
                        xmlOffer.SetAttribute("Name", this.targetDept.Name);
                        xmlNewOper.AppendChild(xmlOffer);
                        //����������ӽڵ� ����ֵ
                        System.Xml.XmlElement xmlPerson = doc.CreateElement("TargetPerson");
                        xmlPerson.SetAttribute("ID", this.targetPerson.ID);
                        xmlPerson.SetAttribute("Name", this.targetPerson.Name);
                        xmlNewOper.AppendChild(xmlPerson);

                        //���ù���
                        ((System.Xml.XmlElement)deptNode).AppendChild(xmlNewOper);

                        #endregion
                    }
                }
                else
                {
                    #region ��ӿ��ҽڵ�

                    //�ÿ���δ�ҵ� ��Ӹÿ��ҡ��ò���Ա��������Ϣ ����������� ������λ
                    System.Xml.XmlElement xmlNewDept = doc.CreateElement("DeptInfo");
                    xmlNewDept.SetAttribute("DeptID", this.deptInfo.ID);

                    //��Ӹò���Ա��Ϣ
                    System.Xml.XmlElement xmlNewOper1 = doc.CreateElement("OperInfo");
                    xmlNewOper1.SetAttribute("OperID", this.operInfo.ID);
                    //�����������ӽڵ� ����ֵ
                    System.Xml.XmlElement xmlPriv1 = doc.CreateElement("PrivType");
                    xmlPriv1.SetAttribute("ID", this.privType.ID);
                    xmlPriv1.SetAttribute("Name", this.privType.Name);
                    //��ӹ�����λ�ӽڵ� ����ֵ
                    System.Xml.XmlElement xmlOffer1 = doc.CreateElement("TargetDept");
                    xmlOffer1.SetAttribute("ID", this.targetDept.ID);
                    xmlOffer1.SetAttribute("Name", this.targetDept.Name);
                    //����������ӽڵ� ����ֵ
                    System.Xml.XmlElement xmlPerson = doc.CreateElement("TargetPerson");
                    xmlPerson.SetAttribute("ID", this.targetPerson.ID);
                    xmlPerson.SetAttribute("Name", this.targetPerson.Name);

                    xmlNewOper1.AppendChild(xmlPriv1);
                    xmlNewOper1.AppendChild(xmlOffer1);
                    xmlNewOper1.AppendChild(xmlPerson);

                    xmlNewDept.AppendChild(xmlNewOper1);

                    root.AppendChild(xmlNewDept);

                    #endregion
                }

                doc.Save(Application.StartupPath + this.filePath);

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="filterData"></param>
        protected virtual void Filter(string filterData)
        {
 
        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Clear()
        {
            this.TargetDept = new Neusoft.NFC.Object.NeuObject();

            this.TargetPerson = new Neusoft.NFC.Object.NeuObject();

            this.ShowInfo = "��ʾ��Ϣ:";

            this.TotCostInfo = "�ܽ��:";

            this.lnbTargetPerson.Visible = false;
            this.txtTargetPerson.Visible = false;
            this.lnbTarget.Visible = false;
            this.txtTargetDept.Visible = false;
        }

        /// <summary>
        /// ����TargetPerson����
        /// </summary>
        public void SetPersonFocus()
        {
            this.lnbTargetPerson.Select();
            this.lnbTargetPerson.Focus();
        }

        /// <summary>
        /// ����TargetDept����
        /// </summary>
        public void SetDeptFocus()
        {
            this.lnbTarget.Select();
            this.lnbTarget.Focus();
        }

        /// <summary>
        /// ����¼��б�
        /// </summary>
        private void ClearEvent()
        {
            //���FpKeyEvent���¼�ί���б�
            if (this.FpKeyEvent != null)
            {
                Delegate[] methodDelegate = this.FpKeyEvent.GetInvocationList();
                foreach (Delegate tempDelegate in methodDelegate)
                {
                    UFC.Pharmacy.ucIMAInOutBase.FpKeyHandler tempKeyHandler = (UFC.Pharmacy.ucIMAInOutBase.FpKeyHandler)tempDelegate;

                    this.FpKeyEvent -= tempKeyHandler;
                }
            }
            //���BeginTarget�¼�ί��
            if (this.BeginTargetChanged != null)
            {
                Delegate[] beginTargetdDelegate = this.BeginTargetChanged.GetInvocationList();
                foreach (Delegate tempDelegate in beginTargetdDelegate)
                {
                    UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler tempHandler = (UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler)tempDelegate;

                    this.BeginTargetChanged -= tempHandler;
                }
            }
            //���EndTarget�¼�ί��
            if (this.EndTargetChanged != null)
            {
                Delegate[] endTargetdDelegate = this.EndTargetChanged.GetInvocationList();
                foreach (Delegate tempDelegate in endTargetdDelegate)
                {
                    UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler tempHandler = (UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler)tempDelegate;

                    this.EndTargetChanged -= tempHandler;
                }
            }
            //���BeginPerson�¼�ί��
            if (this.BeginPersonChanged != null)
            {
                Delegate[] beginPersonDelegate = this.BeginPersonChanged.GetInvocationList();
                foreach (Delegate tempDelegate in beginPersonDelegate)
                {
                    UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler tempHandler = (UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler)tempDelegate;

                    this.BeginPersonChanged -= tempHandler;
                }
            }
            //���EndPerson�¼�ί��
            if (this.EndPersonChanged != null)
            {
                Delegate[] endPersondDelegate = this.EndPersonChanged.GetInvocationList();
                foreach (Delegate tempDelegate in endPersondDelegate)
                {
                    UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler tempHandler = (UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler)tempDelegate;

                    this.EndPersonChanged -= tempHandler;
                }
            }
            //���BeginPriv�¼�ί��
            if (this.BeginPrivChanged != null)
            {
                Delegate[] beginPrivDelegate = this.BeginPrivChanged.GetInvocationList();
                foreach (Delegate tempDelegate in beginPrivDelegate)
                {
                    UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler tempHandler = (UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler)tempDelegate;

                    this.BeginPrivChanged -= tempHandler;
                }
            }
            //���EndPriv�¼�ί��
            if (this.EndPrivChanged != null)
            {
                Delegate[] endPrivDelegate = this.EndPrivChanged.GetInvocationList();
                foreach (Delegate tempDelegate in endPrivDelegate)
                {
                    UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler tempHandler = (UFC.Pharmacy.ucIMAInOutBase.DataChangedHandler)tempDelegate;

                    this.EndPrivChanged -= tempHandler;
                }
            }
        }

        #endregion

        #region �����¼�

        /// <summary>
        /// Ŀ����Ҹ���ǰ�����¼�
        /// </summary>
        /// <param name="changeData">���ĵ�����</param>
        /// <param name="param">��չ��Ϣ</param>
        protected virtual void OnBeginTargetChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            if (this.BeginTargetChanged != null)
                this.BeginTargetChanged(changeData, param);
        }

        /// <summary>
        /// Ŀ����Ҹ��ĺ󴥷��¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnEndTargetChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            if (this.EndTargetChanged != null)
                this.EndTargetChanged(changeData, param);
        }

        /// <summary>
        /// Ŀ����Ա����ǰ�����¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnBeginPersonChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            if (this.BeginPersonChanged != null)
                this.BeginPersonChanged(changeData, param);
        }

        /// <summary>
        /// Ŀ����Ա���ĺ󴥷��¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnEndPersonChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            if (this.EndPersonChanged != null)
                this.EndPersonChanged(changeData, param);
        }

        /// <summary>
        /// ������ǰ�����¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnBeginPrivChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            if (this.BeginPrivChanged != null)
                this.BeginPrivChanged(changeData, param);
        }

        /// <summary>
        /// �����ĺ󴥷��¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnEndPrivChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            if (this.EndPrivChanged != null)
                this.EndPrivChanged(changeData, param);
        }

        /// <summary>
        /// Fp�ڰ����¼�
        /// </summary>
        /// <param name="key"></param>
        protected virtual void OnFpKey(Keys key)
        {
            if (this.FpKeyEvent != null)
                this.FpKeyEvent(key);
        }

        #endregion

        private void lnbTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //����ѡ�񴰿�
            Neusoft.NFC.Object.NeuObject info = new Neusoft.NFC.Object.NeuObject();
            if (Neusoft.NFC.Interface.Classes.Function.ChooseItem(this.alTargetDept, ref info) == 0)
            {
                return;
            }
            else
            {           
                if (info.ID == this.targetDept.ID)		//������Ͳ������仯�򷵻�
                {
                    return;
                }
                else
                {
                    this.OnBeginTargetChanged(info, null);

                    this.TargetDept = info;

                    this.OnEndTargetChanged(info,null);
                }
            }
        }

        private void lnbPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.alTargetPerson == null)
            {
                Neusoft.HISFC.Integrate.Manager managerIntegrate = new Neusoft.HISFC.Integrate.Manager();
                this.alTargetPerson = managerIntegrate.QueryEmployeeAll();
            }
            //����ѡ�񴰿�
            Neusoft.NFC.Object.NeuObject info = new Neusoft.NFC.Object.NeuObject();
            if (Neusoft.NFC.Interface.Classes.Function.ChooseItem(this.alTargetPerson, ref info) == 0)
            {
                return;
            }
            else
            {              
                if (info.ID == this.targetPerson.ID)		//������Ͳ������仯�򷵻�
                {
                    return;
                }
                else
                {
                    this.OnBeginPersonChanged(info, null);

                    this.TargetPerson = info;

                    this.OnEndPersonChanged(info, null);
                }
            }         
        }

        private void lnbPrivType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.privList.Count == 0)
            {
                this.InitPrivType();
            }
            //����ѡ�񴰿�
            Neusoft.NFC.Object.NeuObject info = new Neusoft.NFC.Object.NeuObject();
            if (Neusoft.NFC.Interface.Classes.Function.ChooseItem(new ArrayList(this.privList.ToArray()), ref info) == 0)
            {
                return;
            }
            else
            {              
                if (info.ID == this.privType.ID)		//������Ͳ������仯�򷵻�
                {
                    return;
                }
                else
                {
                    this.OnBeginPrivChanged(info, null);

                    this.ClearEvent();

                    this.PrivType = info;

                    this.OnEndPrivChanged(info, null);
                }
            }           
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                this.OnFpKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter(this.txtFilter.Text);
        }

        private void cmbPrivType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Neusoft.NFC.Object.NeuObject info in this.privList)
            {
                if (info.ID == this.cmbPrivType.Tag.ToString())
                {
                    this.OnBeginPrivChanged(info, null);

                    this.ClearEvent();

                    this.PrivType = info;

                    this.OnEndPrivChanged(info, null);

                    break;
                }
            }
        }

        private void cmbTargetDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Neusoft.NFC.Object.NeuObject info in this.alTargetDept)
            {
                if (info.ID == this.cmbTargetDept.Tag.ToString())
                {
                    this.OnBeginTargetChanged(info, null);

                    this.TargetDept = info;

                    this.OnEndTargetChanged(info, null);
                }
            }
        }

        private void cmbTargetPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Neusoft.NFC.Object.NeuObject info in this.alTargetPerson)
            {
                if (info.ID == this.cmbTargetPerson.Tag.ToString())
                {
                    this.OnBeginPersonChanged(info, null);

                    this.TargetPerson = info;

                    this.OnEndPersonChanged(info, null);

                    break;
                }
            }
        }

    }
}
