using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ���ҿ�����]<br></br>
    /// [�� �� ��: Liangjz]<br></br>
    /// [����ʱ��: 2007-03]<br></br>
    /// <˵��>
    ///     1����Ч���Զ������������� IsValidDateFlag ����ΪTrue ValidDateQueryRealTime ����ΪFalse
    ///                 ValidDateCautionColor ������Ч�ھ�ʾ��ɫ ValidDateCautionDays������Ч�ھ�ʾ����
    ///     2����������ޱ����������� IsWarnStore ����ΪTrue  ���赯����ʾ���� IsWarnStoreMessage ΪTrue
    ///                 ������ΪFalse ����������ǰ��ɫ��ʾ WarnStoreColor ���þ���ɫ
    /// </˵��>
    /// <�޸ļ�¼>
    ///     <ʱ��>2007-07</ʱ��>
    ///     <�޸���>Liangjz</�޸���>
    ///     <�޸�����>����Ĭ�ϲ�ѯ�������ù��ܡ�</�޸�����>
    ///     <ʱ��>2007-08-19</ʱ��>
    ///     <�޸���>liangjz</�޸���>
    ///     <�޸�����>���ӿ�����ʵ�ά�����ܡ�</�޸�����>
    ///     <ʱ��>2008-01-01</ʱ��>
    ///     <�޸���>liangjz</�޸���>
    ///     <�޸�����>����ת���ò��򱾵������ļ��ڴ��롣</�޸�����>
    ///     <�ۺ��޸ļ�¼>
    ///        1.ͬ�����¿��ٲ�ѯ�ؼ�ָ���� by Sunjh 2010-8-23 {A115CC11-A5B8-4835-9D2E-41733059C82A}
    ///        2.������Ϳ�������ܴ���������� by Sunjh 2010-8-24 {CCAE2615-E287-4629-A163-41675012998B}
    ///        3.�������ܽ�� by Sunjh 2010-9-27 {73D14439-69E7-492a-905F-461746EDD6E0}
    ///        4.���õ�����ϸ�б���ʾ��������޾���by Sunjh 2010-9-27 {FFF6506C-686B-41c0-8DA8-E07EEF90F028}
    ///        5.�����ϸֱ�Ӳ鿴������¼ by Sunjh 2010-9-27 {32124B47-8129-4bb4-8067-807B8206668E}
    ///     </�ۺ��޸ļ�¼>
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucStorageManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucStorageManager()
        {
            InitializeComponent();
        }

        #region ������

        /// <summary>
        /// ҩƷ��������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ҩƷ���ʰ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper qualityHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���Ͱ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper dosageHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region �����

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept;

        /// <summary>
        /// �Ƿ���������ʾ��ϸ��Ϣ
        /// </summary>
        private bool isShowDrugDetail = true;

        /// <summary>
        /// Xml�����ļ�·��
        /// </summary>
        private string xmlFilePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\PharmacyStorageManager.xml";

        /// <summary>
        /// ҩƷ���ݼ�
        /// </summary>
        private DataTable dtData = null;

        /// <summary>
        /// ������ͼ
        /// </summary>
        private DataView dvData = null;

        /// <summary>
        /// ��ǰ�༭����
        /// </summary>
        private string nowEditColumn = "��λ��";

        /// <summary>
        /// ��λ������ĳ���
        /// </summary>
        private int placeNoLength = 12;

        /// <summary>
        /// ҩƷ��ϸ��Ϣ��ʾ�ؼ�
        /// </summary>
        private FS.HISFC.Components.Pharmacy.Base.ucPharmacyManager DetailDrugUC = null;

        /// <summary>
        /// �Ƿ�ʹ����Ч�ھ�ʾ
        /// </summary>
        private bool isValidDateFlag = false;

        /// <summary>
        /// ��Ч�ھ�ʾ��Ϣ
        /// </summary>
        private Color validDateCautionColor = System.Drawing.Color.Moccasin;

        /// <summary>
        /// ��Ч�ھ�ʾ���� 
        /// </summary>
        private int validDateCautionDays = 90;

        /// <summary>
        /// �������� 
        /// </summary>
        private DateTime sysDate = System.DateTime.MinValue;

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucSetColumn ucColumn = null;

        /// <summary>
        /// �ɱ༭����
        /// </summary>
        private System.Collections.Hashtable hsEditColumn = new Hashtable();

        /// <summary>
        /// ��ת������
        /// </summary>
        private System.Collections.Hashtable hsJumpColumn = new Hashtable();

        /// <summary>
        /// �Ƿ������˳������
        /// </summary>
        private bool isSetJump = false;

        /// <summary>
        /// ҩƷ�����ϸ��Ч�ڻ�ȡ��ʽ True ʵʱ��ȡ ÿ�ε����ѯʱ���»�ȡ False �б���ʾʱ ֱ�ӻ�ȡ��Ч�Ŀ���¼����С��Ч��
        /// </summary>
        private bool validDateQueryRealTime = true;

        /// <summary>
        /// �Ƿ���п�������ޱ���
        /// </summary>
        private bool isWarnStore = false;

        /// <summary>
        /// ��������ޱ���ʱ �Ƿ񵯳�MessageBox��ʾ
        /// </summary>
        private bool isWarnMessge = false;

        /// <summary>
        /// ��������ޱ���ʱ ���徯ʾɫ
        /// </summary>
        private Color warnStoreColor = System.Drawing.Color.Blue;

        /// <summary>
        /// ���ٲ�ѯ�趨
        /// </summary>
        private System.Collections.Hashtable hsQuickQuery = null;

        /// <summary>
        /// ����������Ƽ���
        /// </summary>
        private string[] qualityNameCollection = null;

        /// <summary>
        /// FYҩƷ����Combo
        /// </summary>
        private FarPoint.Win.Spread.CellType.ComboBoxCellType qualityComboCellType = null;
       
        #endregion

        #region ����

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperDept
        {
            set
            {
                this.operDept = value;
            }
        }

        /// <summary>
        /// �Ƿ���������ʾ��ϸ��Ϣ
        /// </summary>
        [Description("�Ƿ���������ʾ��ϸ��Ϣ"), Category("����"), DefaultValue(true)]
        public bool IsShowDrugDetail
        {
            get
            {
                return this.isShowDrugDetail;
            }
            set
            {
                this.isShowDrugDetail = value;
            }
        }

        /// <summary>
        /// ��λ������ĳ���
        /// </summary>
        [Description("��λ������ĳ���"), Category("����"), DefaultValue(12),Browsable(false)]
        public int PlaceNoLength
        {
            get
            {
                return this.placeNoLength;
            }
            set
            {
                this.placeNoLength = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ����Ч�ھ�ʾ
        /// </summary>
        [Description("�Ƿ�ʹ����Ч�ھ�ʾ"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsValidDateFlag
        {
            get
            {
                return isValidDateFlag;
            }
            set
            {
                isValidDateFlag = value;
            }
        }

        /// <summary>
        /// ��Ч�ھ�ʾ��Ϣ��ɫ
        /// </summary>
        [Description("��Ч�ھ�ʾ��ɫ"), Category("����"), Browsable(false)]
        public Color ValidDateCautionColor
        {
            get
            {
                return this.validDateCautionColor;
            }
            set
            {
                this.validDateCautionColor = value;
            }
        }

        /// <summary>
        /// ��Ч�ھ�ʾ���� 
        /// </summary>
        [Description("��Ч�ھ�ʾ����"), Category("����"), DefaultValue(90), Browsable(false)]
        public int ValidDateCautionDays
        {
            get
            {
                return validDateCautionDays;
            }
            set
            {
                validDateCautionDays = value;
            }
        }

        /// <summary>
        /// ҩƷ�����ϸ��Ч�ڻ�ȡ��ʽ 
        /// True ʵʱ��ȡ ÿ�ε����ѯʱ���»�ȡ False �б���ʾʱ ֱ�ӻ�ȡ��Ч�Ŀ���¼����С��Ч��
        /// </summary>
        [Description("ҩƷ�����ϸ��Ч�ڻ�ȡ��ʽ"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool ValidDateQueryRealTime
        {
            get
            {
                return this.validDateQueryRealTime;
            }
            set
            {
                this.validDateQueryRealTime = value;
            }
        }

        /// <summary>
        /// �Ƿ���п�������ޱ���
        /// </summary>
        [Description("�Ƿ���п�������ޱ���"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsWarnStore
        {
            get
            {
                return this.isWarnStore;
            }
            set
            {
                this.isWarnStore = value;
            }
        }

        /// <summary>
        /// ��������ޱ���ʱ �Ƿ񵯳�MessageBox��ʾ
        /// </summary>
        [Description("��������ޱ���ʱ �Ƿ񵯳�MessageBox��ʾ"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsWarnStoreMessage
        {
            get
            {
                return this.isWarnMessge;
            }
            set
            {
                this.isWarnMessge = value;
            }
        }

        /// <summary>
        /// ��������ޱ���ʱ ���徯ʾɫ
        /// </summary>
        [Description("��������ޱ���ʱ ���徯ʾɫ"), Category("����"), Browsable(false)]
        public Color WarnStoreColor
        {
            get
            {
                return this.warnStoreColor;
            }
            set
            {
                this.warnStoreColor = value;
            }
        }

        /// <summary>
        /// ҩƷ���ݼ�
        /// </summary>
        public DataTable DtData
        {
            get 
            {
                return dtData; 
            }
            set 
            { 
                dtData = value; 
            }
        }

        /// <summary>
        /// ������ͼ
        /// </summary>
        public DataView DvData
        {
            get 
            {
                return dvData; 
            }
            set 
            { 
                dvData = value; 
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ˢ��", "ˢ�¿����ϢҩƷ��ʾ", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            toolBarService.AddToolButton("�߼�����", "�Ƿ���ʾ�߼�����", FS.FrameWork.WinForms.Classes.EnumImageList.YԤ��, true, false, null);
            toolBarService.AddToolButton("��ת����", "���ûس���ת����˳��", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            toolBarService.AddToolButton("�鿴��ϸ", "��ѯ��ǰҩƷ�Ŀ����ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);

            toolBarService.AddToolButton("������ѯ���", "���浱ǰ���õĲ�ѯ�����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Z����, true, false, null);
            toolBarService.AddToolButton("ɾ����ѯ���", "ɾ����ǰ��ʾ�Ĳ�ѯ�����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            toolBarService.AddToolButton("���ɾ�����", "���ݻ��߷�ҩ�����Զ��γɾ�����", FS.FrameWork.WinForms.Classes.EnumImageList.F��Ʊ, true, false, null);

            //{756D23D0-C7F5-410f-B05F-8AE31539BBD1} 
            toolBarService.AddToolButton("ҩ�����", "��ѯ��ҩ�����", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);


            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ˢ��")
            {
                this.Refresh(false);
            }
            if (e.ClickedItem.Text == "�߼�����")
            {
                this.panelFilter.Visible = !this.panelFilter.Visible;
            }
            if (e.ClickedItem.Text == "��ת����")
            {
                this.SetColumnJumpOrder(true);
            }
            if (e.ClickedItem.Text == "�鿴��ϸ")
            {
                this.GetData();
            }
            if (e.ClickedItem.Text == "������ѯ���")
            {
                this.SaveQuickQuery();
            }
            if (e.ClickedItem.Text == "ɾ����ѯ���")
            {
                this.DelQuickQuery();
            }
            if (e.ClickedItem.Text == "���ɾ�����")
            {
                this.SetCaution();
            }
            //{756D23D0-C7F5-410f-B05F-8AE31539BBD1} 
            if (e.ClickedItem.Text == "ҩ�����")
            {
                string drugCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.GetColumnIndex("ҩƷ����")].Text;
                if (string.IsNullOrEmpty(drugCode))
                {
                    MessageBox.Show("����ѡ��ҩƷ!");

                    return;
                }
                using (FS.HISFC.Components.Pharmacy.Out.frmEveryStore frm = new FS.HISFC.Components.Pharmacy.Out.frmEveryStore())
                {
                    frm.DrugCode = drugCode;
                    frm.ShowDialog();
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            this.SetFp();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {


            this.Export();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return 1;
        }

        public override int SetPrint(object sender, object neuObject)
        {
            this.SetColumnDisplayOrder();

            return 1;
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ���Ʋ�����ȡ
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.PlaceNoLength = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Max_Place_Code, true, 12);
            this.IsValidDateFlag = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Valid_Warn_Enabled, true, true);
            this.ValidDateCautionDays = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Valid_Warn_Days, true, 90);

            string validWarnColor = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Valid_Warn_Color, true, System.Drawing.Color.Red.ToArgb().ToString());
            this.ValidDateCautionColor = System.Drawing.Color.FromArgb(FS.FrameWork.Function.NConvert.ToInt32(validWarnColor));

            this.ValidDateQueryRealTime = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Valid_Warn_SourceRealTime, true, true);

            this.IsWarnStore = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Store_Warn_Enabled, true, true);
            this.IsWarnStoreMessage = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Store_Warn_Msg, true, true);
            string storeWarnColor = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Store_Warn_Color, true, System.Drawing.Color.Blue.ToArgb().ToString());
            this.warnStoreColor = System.Drawing.Color.FromArgb(FS.FrameWork.Function.NConvert.ToInt32(storeWarnColor));
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        protected virtual void InitData()
        {
            #region ���ݼ�������

            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alItemType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alItemType == null)
            {
                MessageBox.Show(Language.Msg("���ݳ�������ȡҩƷ�������Ʒ�������!") + consManager.Err);
                itemTypeHelper = new FS.FrameWork.Public.ObjectHelper();
                return;
            }

            FS.FrameWork.Models.NeuObject itemTypeObj = new FS.FrameWork.Models.NeuObject();
            itemTypeObj.ID = "ALL";
            itemTypeObj.Name = "ȫ��";

            alItemType.Insert(0, itemTypeObj);

            itemTypeHelper = new FS.FrameWork.Public.ObjectHelper(alItemType);
          
            this.cmbType.AddItems(alItemType);

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList alPerson = personManager.GetEmployeeAll();
            if (alPerson == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��Ա�б�������!") + consManager.Err);
                this.personHelper = new FS.FrameWork.Public.ObjectHelper();
                return;
            }
            this.personHelper = new FS.FrameWork.Public.ObjectHelper(alPerson);

            ArrayList alQuality = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            if (alQuality == null)
            {
                MessageBox.Show(Language.Msg("���ݳ�������ȡҩƷ���ʷ�������!") + consManager.Err);
                this.qualityHelper = new FS.FrameWork.Public.ObjectHelper();
                return;
            }

            this.qualityNameCollection = new string[alQuality.Count];
            int iIndex = 0;
            foreach (FS.FrameWork.Models.NeuObject qualityInfo in alQuality)
            {
                qualityNameCollection[iIndex] = qualityInfo.Name;
                iIndex++;
            }

            FS.FrameWork.Models.NeuObject qualityObj = new FS.FrameWork.Models.NeuObject();
            qualityObj.ID = "ALL";
            qualityObj.Name = "ȫ��";

            alQuality.Insert(0, qualityObj);
            this.qualityHelper = new FS.FrameWork.Public.ObjectHelper(alQuality);
         
            this.cmbQuality.AddItems(alQuality);

            ArrayList alDosage = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
            if (alDosage == null)
            {
                MessageBox.Show(Language.Msg("���ݳ�������ȡҩƷ���ͷ�������") + consManager.Err);
                return;
            }
            this.dosageHelper = new FS.FrameWork.Public.ObjectHelper(alDosage);


            #endregion

            #region Fp���λس���

            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #endregion

            #region ���ٲ�ѯ�趨����

            ArrayList alQuickQuery = consManager.GetList("PhaQuickQuery");
            if (alQuickQuery == null)
            {
                MessageBox.Show(Language.Msg("���ݳ�������ȡ���ٲ�ѯ�趨��������") + consManager.Err);
                return;
            }

            this.hsQuickQuery = new Hashtable();

            foreach (FS.FrameWork.Models.NeuObject info in alQuickQuery)            
            {              
                this.hsQuickQuery.Add(info.ID, info);
            }

            this.cmbQuickQuery.AddItems(alQuickQuery);

            #endregion
        }
     
        #endregion

        #region Ȩ���������

        private System.Collections.Hashtable hsStopManagerPriv = new Hashtable();

        /// <summary>
        /// Ȩ������
        /// </summary>
        protected void PrivManager()
        {
            //{C6AF4B8E-B9D6-4c1e-A5FE-D05F1457E305}
            if (this.cmbDept.SelectedItem != null)
            {
                this.InitStopPriv(this.cmbDept.SelectedItem.ID);
            }
        }

        /// <summary>
        /// ������ӵ�е�Ȩ�� ���� ��������
        /// </summary>
        private void InitDeptList()
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            string operCode = powerDetailManager.Operator.ID;
            List<FS.FrameWork.Models.NeuObject> alDept = powerDetailManager.QueryUserPriv(operCode, "0302");
            if (alDept == null)
            {
                MessageBox.Show(Language.Msg("���ݿ�����Ȩ�޻�ȡȨ�޿��ҷ�������!") + powerDetailManager.Err);
                return;
            }
            this.cmbDept.AddItems(new ArrayList(alDept.ToArray()));

            if (alDept.Count > 0)
                this.cmbDept.SelectedIndex = 0;
        }

        /// <summary>
        /// Ȩ���жϿ���
        /// </summary>
        /// <param name="deptCode"></param>
        private void InitStopPriv(string deptCode)
        {
            bool isPriv = false;
            if (hsStopManagerPriv.ContainsKey(deptCode))
            {
                isPriv = (bool)hsStopManagerPriv[deptCode];
            }
            else
            {
                FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();

                string operCode = powerDetailManager.Operator.ID;

                isPriv = powerDetailManager.JudgeUserPriv(operCode, deptCode, "0302", "02");

                hsStopManagerPriv.Add(deptCode, isPriv);
            }

            this.neuSpread1_Sheet1.Columns[this.GetColumnIndex("ͣ��")].Locked = !isPriv;
        }

        #endregion

        #region DataTable���� ����

        /// <summary>
        /// ����Ĭ�����õ��ݸ�ʽ
        /// </summary>
        protected virtual void InitDefaultDataTable()
        {
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.dtData.Columns.AddRange(new DataColumn[] {
														new DataColumn("�ⷿ����",    dtStr),//0
														new DataColumn("ҩƷ����",    dtStr),//1
														new DataColumn("�Զ�����",    dtStr),//2
														new DataColumn("��Ʒ����",    dtStr),//3
														new DataColumn("���",        dtStr),//4
                                                        new DataColumn("����",        dtStr),
														new DataColumn("���ۼ�",      dtDec),//5
                                                        new DataColumn("�����",      dtDec),
                                                        new DataColumn("�����",      dtStr),
														new DataColumn("�������",    dtDec),//6
														new DataColumn("��С��λ",    dtStr),//7
														new DataColumn("��װ����",    dtDec),//8
														new DataColumn("��װ��λ",    dtStr),//9
														new DataColumn("ͨ����",      dtStr),//10																		
														new DataColumn("��λ��",      dtStr),//11
														new DataColumn("ͣ��",        dtBool),//12
														new DataColumn("��Ϳ����",  dtDec),//13
														new DataColumn("��߿����",  dtDec),//14
														new DataColumn("���̵�",      dtBool),//15
                                                        new DataColumn("�������",    dtStr),
														new DataColumn("��Ч��",      dtDTime),//16
														new DataColumn("�����",    dtDec),//17
														new DataColumn("Ԥ������",    dtDec),//18
														new DataColumn("Ԥ�۽��",    dtDec),//19
														new DataColumn("ҩƷ���",    dtStr),//20
														new DataColumn("ҩƷ����",    dtStr),//21
														new DataColumn("��ע",        dtStr),//22
														new DataColumn("������",      dtStr),//23
														new DataColumn("��������",    dtDTime),//24
														new DataColumn("ƴ����",      dtStr),//25
														new DataColumn("�����",      dtStr),//26																			
														new DataColumn("ͨ����ƴ����",dtStr),//27
														new DataColumn("ͨ���������",dtStr),//28
														new DataColumn("ͨ�����Զ�����",dtStr),//29
				                                        new DataColumn("ҩ��ͣ��",dtBool),//30
				                                        new DataColumn("ȱҩ",dtBool),//31
                                                        new DataColumn("ѧ��",dtStr),
                                                        new DataColumn("����",dtStr),
                                                        new DataColumn("ѧ��ƴ����"),
                                                        new DataColumn("����ƴ����")
                    								});

            this.neuSpread1_Sheet1.DataSource = this.dtData;

            try
            {
                this.neuSpread1_Sheet1.Columns[0].Visible = false;          //�ⷿ����
                this.neuSpread1_Sheet1.Columns[1].Visible = false;          //ҩƷ����
                this.neuSpread1_Sheet1.Columns[27].Visible = false;
                this.neuSpread1_Sheet1.Columns[28].Visible = false;
                this.neuSpread1_Sheet1.Columns[29].Visible = false;
                this.neuSpread1_Sheet1.Columns[30].Visible = false;
                this.neuSpread1_Sheet1.Columns[31].Visible = false;

                this.neuSpread1_Sheet1.Columns[3].Width = 120F;
                this.neuSpread1_Sheet1.Columns[4].Width = 90F;
            }
            catch { }

          //  FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, this.xmlFilePath);

        }

        /// <summary>
        /// ����DataTable
        /// </summary>
        /// <param name="dt">�����õ�DataTable</param>
        protected virtual void SetDataTable()
        {
            this.dtData = new DataTable();

            this.InitDefaultDataTable();

            //if (System.IO.File.Exists(this.xmlFilePath))
            //{
            //    this.dtData = Function.GetDataTableFromXml(this.xmlFilePath);

            //    if (this.dtData == null)
            //    {
            //        this.dtData = new DataTable();

            //        this.InitDefaultDataTable();
            //    }
            //    else
            //    {
            //        this.neuSpread1_Sheet1.DataSource = this.dtData;
            //    }

            //    //Xml�洢�����ô���bug ��һ��ʲôʱ�������� ���⼸�д洢ΪText ���¹��˷�������
            //    if (this.dtData.Columns["�������"].DataType != typeof(System.Decimal) || this.dtData.Columns["��Ϳ����"].DataType != typeof(System.Decimal) ||
            //        this.dtData.Columns["��߿����"].DataType != typeof(System.Decimal))
            //    {
            //        this.dtData.Columns.Clear();

            //        this.InitDefaultDataTable();
            //    }
            //}
            //else
            //{
            //    #region ����Ĭ������DataTable

            //    this.InitDefaultDataTable();
               
            //    #endregion
            //}           

            this.hsEditColumn.Clear();

            if (this.GetColumnIndex("ͣ��") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("ͣ��"), "ͣ��");
            if (this.GetColumnIndex("��Ϳ����") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��Ϳ����"), "��Ϳ����");
            if (this.GetColumnIndex("��߿����") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��߿����"), "��߿����");
            if (this.GetColumnIndex("���̵�") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("���̵�"), "���̵�");
            if (this.GetColumnIndex("��Ч��") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��Ч��"), "��Ч��");
            if (this.GetColumnIndex("��ע") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��ע"), "��ע");
            if (this.GetColumnIndex("ȱҩ") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("ȱҩ"), "ȱҩ");
            if (this.GetColumnIndex("��λ��") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("��λ��"), "��λ��");
            if (this.GetColumnIndex("�������") != -1)
                this.hsEditColumn.Add(this.GetColumnIndex("�������"), "�������");
        }

        /// <summary>
        /// ���ݿ����Ϣ ����DataRow
        /// </summary>
        /// <param name="storage">�����Ϣ</param>
        /// <returns>�ɹ�������������Ϣ</returns>
        protected virtual DataRow SetStorage(FS.HISFC.Models.Pharmacy.Storage storage)
        {
            DataRow row = this.dtData.NewRow();
            try
            {
                row["�ⷿ����"] = storage.StockDept.ID;
                row["ҩƷ����"] = storage.Item.ID;
                row["�Զ�����"] = storage.Item.UserCode;
                row["��Ʒ����"] = storage.Item.Name;
                row["���"] = storage.Item.Specs;
                row["���ۼ�"] = storage.Item.PriceCollection.RetailPrice;
                row["�������"] = storage.StoreQty;

                decimal packQty = Math.Floor(storage.StoreQty / storage.Item.PackQty);
                decimal minQty = storage.StoreQty - packQty * storage.Item.PackQty;
                if (packQty == 0)
                {
                    row["�����"] = string.Format("{0}{1}", minQty, storage.Item.MinUnit);
                }
                else if (minQty == 0)
                {
                    row["�����"] = string.Format("{0}{1}", packQty, storage.Item.PackUnit);
                }
                else
                {
                    row["�����"] = string.Format("{0}{1} {2}{3}", packQty, storage.Item.PackUnit, minQty, storage.Item.MinUnit);
                }

                row["��С��λ"] = storage.Item.MinUnit;
                row["��װ����"] = storage.Item.PackQty;
                row["��װ��λ"] = storage.Item.PackUnit;
                row["ͨ����"] = storage.Item.NameCollection.RegularName;
                row["��λ��"] = storage.PlaceNO;
                row["ͣ��"] = storage.IsStop;
                row["��Ϳ����"] = storage.LowQty;
                row["��߿����"] = storage.TopQty;
                row["���̵�"] = storage.IsCheck;
                row["��Ч��"] = storage.ValidTime;
                row["�����"] = storage.StoreCost;
                row["Ԥ������"] = storage.PreOutQty;
                row["Ԥ�۽��"] = Math.Round(storage.PreOutQty / storage.Item.PackQty * storage.Item.PriceCollection.RetailPrice, 2);
                row["ҩƷ���"] = this.itemTypeHelper.GetName(storage.Item.Type.ID);
                row["ҩƷ����"] = this.qualityHelper.GetName(storage.Item.Quality.ID);
                row["��ע"] = storage.Memo;
                row["������"] = storage.Operation.Oper.ID;
                row["��������"] = storage.Operation.Oper.OperTime;
                row["ƴ����"] = storage.Item.NameCollection.SpellCode;
                row["�����"] = storage.Item.NameCollection.WBCode;
                row["ͨ����ƴ����"] = storage.Item.NameCollection.RegularSpell.SpellCode;
                row["ͨ���������"] = storage.Item.NameCollection.RegularSpell.WBCode;
                row["ͨ�����Զ�����"] = storage.Item.NameCollection.RegularSpell.UserCode;
                row["ҩ��ͣ��"] = storage.Item.IsStop;
                row["ȱҩ"] = storage.Item.IsLack;

                row["�������"] = this.qualityHelper.GetName(storage.ManageQuality.ID);

                row["ѧ��"] = storage.Item.NameCollection.FormalName;
                row["ѧ��ƴ����"] = storage.Item.NameCollection.FormalSpell.SpellCode;
                row["����"] = storage.Item.NameCollection.OtherName;
                row["����ƴ����"] = storage.Item.NameCollection.OtherSpell.SpellCode;

                row["����"] = this.dosageHelper.GetName(storage.Item.DosageForm.ID);

                row["�����"] = storage.Item.PriceCollection.PurchasePrice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("���ݿ����Ϣ�������н��и�ֵʱ��������!") + ex.Message);
            }

            return row;
        }

        /// <summary>
        /// ��������Ϣ ���ؿ����Ϣ
        /// </summary>
        /// <param name="row">DataRow��Ϣ</param>
        /// <returns>�ɹ����ؿ����Ϣ</returns>
        private FS.HISFC.Models.Pharmacy.Storage GetStorageModifyInfo(DataRow row)
        {
            FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();
            try
            {
                storage.StockDept.ID = row["�ⷿ����"].ToString();
                storage.Item.ID = row["ҩƷ����"].ToString();
                storage.Item.UserCode = row["�Զ�����"].ToString();
                storage.Item.Name = row["��Ʒ����"].ToString();
                storage.Item.Specs = row["���"].ToString();
                storage.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(row["���ۼ�"]);
                storage.PlaceNO = row["��λ��"].ToString();
                storage.IsStop = NConvert.ToBoolean(row["ͣ��"]);
                storage.LowQty = NConvert.ToDecimal(row["��Ϳ����"]);
                storage.TopQty = NConvert.ToDecimal(row["��߿����"]);
                storage.IsCheck = NConvert.ToBoolean(row["���̵�"]);
                storage.ValidTime = NConvert.ToDateTime(row["��Ч��"]);
                storage.Memo = row["��ע"].ToString();
                storage.IsLack = NConvert.ToBoolean(row["ȱҩ"]);
                storage.ManageQuality.ID = this.qualityHelper.GetID(row["�������"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("���ݿ����Ϣ�������н��и�ֵʱ��������!") + ex.Message);
            }

            return storage;
        }

        #endregion

        #region ��洦��

        /// <summary>
        /// ˢ�µ�ǰ�����ʾ
        /// </summary>
        /// <param name="isResetDataTable">�Ƿ�����DataTable</param>
        public void Refresh(bool isResetDataTable)
        {
            if (this.cmbDept.SelectedItem != null)
                this.ShowStorageData(this.cmbDept.SelectedItem.ID, isResetDataTable);
            else
                this.ClearData();

            base.Refresh();
        }

        /// <summary>
        /// ���ݿ��ұ�����������Ϣ����DataTable���������� 
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        private void ShowStorageData(string deptCode)
        {
            this.ShowStorageData(deptCode, false);
        }

        /// <summary>
        /// ���ݿ��ұ�����������Ϣ����DataTable����������
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="isReSetDataTable">�Ƿ�����DataTable</param>
        protected virtual void ShowStorageData(string deptCode, bool isReSetDataTable)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ�����ʾ�����Ϣ...���Ժ�"));
            Application.DoEvents();

            ArrayList alStorageData = this.itemManager.QueryStockinfoList(deptCode);
            if (alStorageData == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                MessageBox.Show(Language.Msg("��ȡ���ҿ����Ϣ��������!") + this.itemManager.Err);
                return;
            }

            if (isReSetDataTable)
                this.SetDataTable();
            else
                this.ClearData();

            //�������ܽ�� by Sunjh 2010-9-27 {73D14439-69E7-492a-905F-461746EDD6E0}
            decimal storageCost = 0;

            foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorageData)
            {
                storageCost += storage.StoreQty * storage.Item.PriceCollection.RetailPrice / storage.Item.PackQty;
                if (!this.validDateQueryRealTime)
                    storage.ValidTime = this.GetMinValidDate(storage.StockDept.ID, storage.Item.ID);
                this.dtData.Rows.Add(this.SetStorage(storage));
            }

            this.lblStorageCost.Text = "�����: " + storageCost.ToString("0.00");

            this.dtData.AcceptChanges();

            this.dvData = this.dtData.DefaultView;
            this.dvData.AllowNew = true;
            this.neuSpread1_Sheet1.DataSource = this.dvData;

            try
            {
                if (System.IO.File.Exists(this.xmlFilePath))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.xmlFilePath);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, this.xmlFilePath);
                }

                this.SetDrugFlag(true);
            }
            catch
            {
                MessageBox.Show(Language.Msg("��ȡ�������ļ���Ϣ��������"));
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ���ҿ�����ݱ���
        /// </summary>
        protected virtual void Save()
        {
            this.neuSpread1.StopCellEditing();

            this.dvData.RowFilter = "1=1";
            for (int i = 0; i < this.dvData.Count; i++)
            {
                this.dvData[i].EndEdit();
            }

            DataTable dtModify = this.dtData.GetChanges(DataRowState.Modified);
            if (dtModify == null || dtModify.Rows.Count <= 0)
                return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (DataRow dr in dtModify.Rows)
            {
                FS.HISFC.Models.Pharmacy.Storage storage = this.GetStorageModifyInfo(dr);
                if (storage.LowQty > storage.TopQty)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���治�ܽ��С���" + storage.Item.Name + "����Ϳ�������ܴ��ڿ���������"), "��ʾ");
                    return;
                }

                if (this.itemManager.UpdateStockinfoModifyData(storage) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("������� ���¿��ʧ��") + this.itemManager.Err);
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ�"));
        }

        #endregion

        #region ����       

        /// <summary>
        /// ���ݵ���
        /// </summary>
        protected virtual void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        protected virtual void ClearData()
        {
            if (this.dtData == null)
                this.dtData = new DataTable();

            this.dtData.Clear();

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public new void Focus()
        {
            this.txtQueryCode.Focus();
            this.txtQueryCode.SelectAll();
        }

        /// <summary>
        /// ҩƷ��ϸ��Ϣ����
        /// </summary>
        protected virtual void PopDrugDetail(FS.HISFC.Models.Pharmacy.Item item)
        {
            if (this.DetailDrugUC == null)
            {
                this.DetailDrugUC = new HISFC.Components.Pharmacy.Base.ucPharmacyManager();
                this.DetailDrugUC.ReadOnly = true;
            }
            this.DetailDrugUC.InputType = "UPDATE";
            this.DetailDrugUC.Item = item;

            FS.FrameWork.WinForms.Classes.Function.ShowControl(this.DetailDrugUC);
        }

        /// <summary>
        /// ���������ƻ�ȡ������
        /// </summary>
        /// <param name="colName">������</param>
        /// <returns>�ɹ����������� ʧ�ܷ���-1</returns>
        private int GetColumnIndex(string colName)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Columns[i].Label == colName)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// ��ʾҩ��ͣ�ñ�� ���������� ������Ч����ʾ
        /// </summary>
        ///<param name="isShowMsg">�Ƿ񵯳���ʾ��Ϣ ����ָ��������ޱ�����</param>
        protected virtual void SetDrugFlag(bool isShowMsg)
        {
            if (this.neuSpread1_Sheet1.Rows.Count >= 1)
            { 
                string warnMsg = "";

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.SetRowLabel(i, 0, "");
                    this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.SystemColors.Control;
                    if (this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("ҩ��ͣ��")].Text.ToUpper() == "TRUE")
                    {
                        this.neuSpread1_Sheet1.SetRowLabel(i, 0, "ͣ");
                        this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.White;
                    }

                    #region ��Ч�ھ�ʾ

                    if (this.isValidDateFlag && !this.validDateQueryRealTime)
                    {
                        DateTime tempDate = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ч��")].Text);
                        if (tempDate > this.sysDate.AddDays(this.validDateCautionDays))
                            this.neuSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.White;
                        else
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                    }

                    #endregion

                    #region ��������޾�ʾ
                    /*
                    if (this.isWarnStore)
                    {
                        decimal lowQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ϳ����")].Text);
                        decimal topQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��߿����")].Text);
                        decimal storeQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("�������")].Text);

                        if (lowQty == 0 && topQty == 0)
                        {
                            continue;
                        }
                        
                        if (storeQty < lowQty)
                        {
                            if (this.isWarnMessge)
                            {
                                warnMsg = warnMsg + " " + this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ʒ����")].Text;
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Rows[i].ForeColor = this.warnStoreColor;
                            }
                        }
                    }
                    */
                    #endregion
                }

                //if (this.isWarnStore && this.isWarnMessge)
                //{
                //    if (warnMsg != "" && isShowMsg)
                //    {
                //        MessageBox.Show(Language.Msg("����ҩƷ������������ޣ�\n" + warnMsg));
                //    }
                //}

                //���õ�����ϸ�б���ʾ��������޾��� by Sunjh 2010-9-27 {FFF6506C-686B-41c0-8DA8-E07EEF90F028}
                if (this.isWarnStore && this.isWarnMessge)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    string warnStoreSql = "select b.trade_name as ҩƷ����,b.specs as ���,t.store_sum||t.min_unit as ����� from pha_com_stockinfo t,pha_com_baseinfo b "
                       + "where t.drug_code=b.drug_code and t.store_sum<t.low_sum and t.drug_dept_code='" + this.cmbDept.SelectedItem.ID + "'";
                    string[] argColumns = new string[3] { "150", "80", "90" };
                    FS.HISFC.Components.Pharmacy.Base.ucCommonPopQuery ucPopList = new FS.HISFC.Components.Pharmacy.Base.ucCommonPopQuery(warnStoreSql, argColumns);
                    if (ucPopList.RecordCount > 0 && isShowMsg)
                    {
                        ucPopList.TopInfo = "����ҩƷ������Ϳ������";
                        ucPopList.BottomInfo = "ҩƷ��汨��";
                        ucPopList.Width = 390;
                        ucPopList.Height = 300;
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPopList);
                    }                    
                }
                
                this.SetFp();
            }
        }

        #endregion

        #region ��Ϲ��˴���

        /// <summary>
        /// ���� ֻ����ͨ�������������
        /// </summary>
        protected virtual void CodeFilter()
        {
            if (this.dtData.Rows.Count <= 0)
                return;

            
            string lsFilter =  this.txtQueryCode.Text.Trim();

            lsFilter = FS.FrameWork.Public.String.TakeOffSpecialChar(lsFilter);

            try
            {
                string queryCode = "";
                queryCode = "%" + lsFilter + "%";
                string filter = "";
                this.cmbCondition.Text = FS.FrameWork.Public.String.TakeOffSpecialChar(this.cmbCondition.Text.Trim());
                if (this.cmbCondition.Text != "ȫ��" && this.cmbCondition.Text != "")
                {
                    filter = FS.FrameWork.Public.String.TakeOffSpecialChar( this.cmbCondition.Text.Trim()) + "  LIKE '" + queryCode + "'";
                }
                else
                {
                    filter = Function.GetFilterStr(this.dvData, queryCode);
                }

                this.dvData.RowFilter = filter;

                this.SetDrugFlag(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��Ϲ���
        /// </summary>
        protected virtual void CombinedFilter()
        {
            string filterStr = "";
            if (this.chbStock.Checked)
            {
                if (this.cmbStockFilterCondition.Text != "")
                {
                    decimal stockNum = FS.FrameWork.Function.NConvert.ToDecimal(this.txtStockNum.Text);
                    filterStr = Function.ConnectFilterStr(filterStr, string.Format("������� - Ԥ������ {0} {1}",this.cmbStockFilterCondition.Text,stockNum.ToString()), "and");
                }
            }
            if (this.chbState.Checked == true)
            {
                if (this.cmbState.Text != "")
                {
                    if (this.cmbState.Text == "ͣ��")
                        filterStr =  Function.ConnectFilterStr(filterStr, string.Format("ͣ�� = {0}","true"), "and");
                    else
                        filterStr = Function.ConnectFilterStr(filterStr, string.Format("ͣ�� = {0}", "false"), "and");
                }
            }
            if (this.cmbQuality.Tag != null && this.cmbQuality.Text != "" && this.cmbQuality.Text != "ȫ��")
            {
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ҩƷ���� = '{0}'",this.cmbQuality.Text), "and");
            }

            if (this.cmbType.Tag != null && this.cmbType.Text != "" && this.cmbType.Text != "ȫ��")
            {
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ҩƷ��� = '{0}'",this.cmbType.Text), "and");
            }

            if (this.dvData != null)
            {
                this.dvData.RowFilter = filterStr;

                this.SetDrugFlag(false);
            }
        }

        #endregion

        #region Fp��˳������ Fp����ת����

        /// <summary>
        /// ������
        /// </summary>
        private void SetColumnDisplayOrder()
        {
            if (this.ucColumn == null)
            {
                this.ucColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
                this.ucColumn.DisplayEvent -= new EventHandler(ucColumn_DisplayEvent);
                this.ucColumn.DisplayEvent += new EventHandler(ucColumn_DisplayEvent);
            }

            this.isSetJump = false;

            this.ucColumn.IsShowUpDonw = false;
            this.ucColumn.SetDataTable(this.xmlFilePath, this.neuSpread1_Sheet1);
            this.ucColumn.SetColVisible(true, true, true, false);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucColumn);
        }

        ucEasySet ucJumpSet = null;

        /// <summary>
        /// ��������ת˳��
        /// </summary>
        /// <param name="isPopShow">��ʼ���� �Ƿ񵯳�</param>
        private void SetColumnJumpOrder(bool isPopShow)
        {
            //if (this.ucColumn == null)
            //{
            //    this.ucColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            //    this.ucColumn.DisplayEvent -= new EventHandler(ucColumn_DisplayEvent);
            //    this.ucColumn.DisplayEvent += new EventHandler(ucColumn_DisplayEvent);
            //}

            //this.isSetJump = true;

            //this.ucColumn.SetDataTable(this.xmlFilePath, this.neuSpread1_Sheet1);
            //this.ucColumn.SetColVisible(false, false, false, true);
            //FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����ת˳������";
            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucColumn);

            if (this.ucJumpSet == null)
            {
                this.ucJumpSet = new ucEasySet();

                this.ucJumpSet.SaveFinishedEvent -= new ucEasySet.DataManagerDelegate(ucJumpSet_SaveFinishedEvent);
                this.ucJumpSet.SaveFinishedEvent += new ucEasySet.DataManagerDelegate(ucJumpSet_SaveFinishedEvent);

                this.ucJumpSet.InitDataEvent -= new ucEasySet.DataManagerDelegate(ucJumpSet_InitDataEvent);
                this.ucJumpSet.InitDataEvent += new ucEasySet.DataManagerDelegate(ucJumpSet_InitDataEvent);
            }

            if (isPopShow)
            {
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����ת˳������";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucJumpSet);
            }
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        private int ucJumpSet_InitDataEvent()
        {
            string strErr = "";

            //FS.FrameWork.WinForms.Classes.Function.DefaultValueFilePath = Application.StartupPath + "\\HISDefaultValue.xml";
            
            //ArrayList al = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("PHA", "StorageManagerJump", out strErr);
            ArrayList al = new ArrayList();

            if (this.ucJumpSet == null)
            {
                this.SetColumnJumpOrder(false);
            }

            if (al == null || al.Count == 0)
            {
                for (int i = 0; i < this.ucJumpSet.FpSv.Rows.Count; i++)
                {
                    this.ucJumpSet.FpSv.Cells[i, 1].Value = false;
                }
            }

            int iValue = 0;
            foreach (string strValue in al)
            {
                this.ucJumpSet.FpSv.Cells[iValue, 1].Value = FS.FrameWork.Function.NConvert.ToBoolean(strValue);

                iValue++;
            }
            return 1;
        }

        /// <summary>
        /// ��ת˳�򱣴�
        /// </summary>
        /// <returns></returns>
        private int ucJumpSet_SaveFinishedEvent()
        {
            string strErr = "";

            string[] strValue = new string[this.ucJumpSet.FpSv.Rows.Count];

            //����ת�и�ֵ
            this.hsJumpColumn = new Hashtable();
            bool firstColumn = true;
            for(int i = 0;i < this.ucJumpSet.FpSv.Rows.Count;i++)
            {
                if (this.ucJumpSet.FpSv.Cells[i,1].Value == null)
                {
                    continue;
                }

                strValue[i] = this.ucJumpSet.FpSv.Cells[i, 1].Value.ToString();

                if (!FS.FrameWork.Function.NConvert.ToBoolean(this.ucJumpSet.FpSv.Cells[i, 1].Value))
                {
                    continue;
                }
                string str = this.ucJumpSet.FpSv.Cells[i, 0].Text;

                int iIndex = this.GetColumnIndex(str);
                this.hsJumpColumn.Add(iIndex, str);

                if (firstColumn)
                {
                    this.nowEditColumn = str;
                    firstColumn = false;
                }
            }

            //FS.FrameWork.WinForms.Classes.Function.DefaultValueFilePath = "\\HISDefaultValue.xml";

            //return FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("PHA", "StorageManagerJump", out strErr, strValue);             

            return 1;
        }

        #endregion

        #region Fp����������

        FarPoint.Win.Spread.CellType.TextCellType readonlyTextCell = new FarPoint.Win.Spread.CellType.TextCellType();

        /// <summary>
        /// ����Fpֻ������
        /// </summary>
        private void SetFp()
        {
            this.readonlyTextCell.ReadOnly = true;

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Locked = false;
                if (this.hsEditColumn.Contains(i))
                    continue;
                this.neuSpread1_Sheet1.Columns[i].CellType = this.readonlyTextCell;
            }

            if (this.qualityComboCellType == null)
            {
                this.qualityComboCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                qualityComboCellType.Items = this.qualityNameCollection;
            }

            this.neuSpread1_Sheet1.Columns[this.GetColumnIndex("�������")].CellType = qualityComboCellType;

            if (this.cmbDept.SelectedItem != null && this.cmbDept.SelectedItem.ID != null)
            {
                this.InitStopPriv(this.cmbDept.SelectedItem.ID);
            }
        }

        #endregion

        #region �����ϸ

        /// <summary>
        /// ��ѯ��ǰѡ��ҩƷ�Ŀ����ϸ��Ϣ
        /// </summary>
        protected virtual void GetData()
        {
            this.neuSpread2_Sheet1.Rows.Count = 0;

            if (this.cmbDept.SelectedItem == null)
                return;

            //{DC448FDB-1743-4101-940B-B0994B1EDC2D} by niuxy
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            string drugCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.GetColumnIndex("ҩƷ����")].Text;
            string deptCode = this.cmbDept.SelectedItem.ID;

            ArrayList alStorage = this.itemManager.QueryStorageList(deptCode, drugCode);
            if (alStorage == null)
            {
                FS.FrameWork.WinForms.Classes.Function.MessageBox("��ȡҩƷ�����ϸʧ��", this.itemManager.Err);
            }
            foreach (FS.HISFC.Models.Pharmacy.Storage info in alStorage)
            {
                //if (info.StoreQty <= 0)
                //    continue;

                this.neuSpread2_Sheet1.Rows.Add(0, 1);
                this.neuSpread2_Sheet1.Cells[0, 0].Text = info.BatchNO;
                this.neuSpread2_Sheet1.Cells[0, 1].Text = info.Item.Name;
                this.neuSpread2_Sheet1.Cells[0, 2].Text = info.Item.Specs;
                this.neuSpread2_Sheet1.Cells[0, 3].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.neuSpread2_Sheet1.Cells[0, 4].Text = info.ValidTime.ToString("yyyy-MM-dd");
                this.neuSpread2_Sheet1.Cells[0, 5].Text = info.StoreQty.ToString();
                this.neuSpread2_Sheet1.Cells[0, 6].Text = info.Item.MinUnit;
                this.neuSpread2_Sheet1.Cells[0, 8].Text = info.Memo;

                //this.btnDetailHead.Text = "�����ϸ - " + info.Item.Name + "[" + info.Item.Specs + "]";
            }
        }

        #endregion

        #region ��Ч�ڼ���

        /// <summary>
        /// ��Ч�ڼ�������
        /// 
        /// //{F53FB515-9B8A-48ce-A395-C6A0F69B15DC} Ч���ж� ���Ӷ��ڲ�����������������
        /// </summary>
        private void ValidDateFilter()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڰ�����Ч�ڽ��в���\n����������ҩƷ����ɫ��ʾ"));
            Application.DoEvents();
            
            string deptCode = this.cmbDept.SelectedItem.ID;
            this.dptValidDate.Value = new System.DateTime(dptValidDate.Value.Year, dptValidDate.Value.Month, dptValidDate.Value.Day, 0, 0, 0);
            DateTime minValidDate = System.DateTime.MaxValue;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                string drugCode = this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("ҩƷ����")].Text;

                if (this.validDateQueryRealTime)
                {
                    minValidDate = this.GetMinValidDate(deptCode, drugCode);
                }
                else
                {
                    minValidDate = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, this.GetColumnIndex("��Ч��")].Text);
                }

                if (minValidDate == new DateTime(5000, 1, 1, 0, 0, 0))
                {
                    this.neuSpread1_Sheet1.Rows[i].Visible = false;
                    continue;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[i].Visible = true;
                }

                minValidDate = minValidDate.Date;

                this.neuSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.White;
                switch (this.cmbValidDateFilterCondition.Text)
                {
                    case "<=":
                        if (minValidDate <= this.dptValidDate.Value)
                        {
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                            this.neuSpread1_Sheet1.Rows[i].Visible = true;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Rows[i].Visible = false;
                        }
                        break;
                    case ">=":
                        if (minValidDate >= this.dptValidDate.Value)
                        {
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                            this.neuSpread1_Sheet1.Rows[i].Visible = true;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Rows[i].Visible = false;
                        }
                        break;
                    case "=":
                        if (minValidDate == this.dptValidDate.Value)
                        {
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                            this.neuSpread1_Sheet1.Rows[i].Visible = true;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Rows[i].Visible = false;
                        }
                        break;
                    default:
                        if (minValidDate <= this.dptValidDate.Value)
                        {
                            this.neuSpread1_Sheet1.Rows[i].BackColor = this.validDateCautionColor;
                            this.neuSpread1_Sheet1.Rows[i].Visible = true;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Rows[i].Visible = false;
                        }
                        break;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ��ȡҩƷ��Ч����¼����С��Ч��
        /// </summary>
        protected virtual DateTime GetMinValidDate(string deptCode, string drugCode)
        {
            ArrayList alStorage = this.itemManager.QueryStorageList(deptCode, drugCode);
            if (alStorage == null)
            {
                FS.FrameWork.WinForms.Classes.Function.MessageBox("��ȡҩƷ�����ϸʧ��", this.itemManager.Err);
            }

            //{F53FB515-9B8A-48ce-A395-C6A0F69B15DC} Ч���ж�
            DateTime validDate = new DateTime(5000, 1, 1, 0, 0, 0);
            foreach (FS.HISFC.Models.Pharmacy.Storage info in alStorage)
            {
                if (info.StoreQty <= 0)
                {
                    continue;
                }

                if (info.ValidTime < validDate)
                {
                    validDate = info.ValidTime;
                }
            }

            return validDate;
        }

        #endregion

        #region ���ٲ�ѯ�����趨

        /// <summary>
        /// ���ٲ�ѯ��������
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int SaveQuickQuery()
        {
            /*
             * 1�������ʽ��Type: Quality: State: StoreCondition: StoreQty
            */

            string saveStr = "Type:{0}Quality:{1}State:{2}StoreCondition:{3}StoreQty:{4}";

            string type = this.cmbType.Tag == null ? "" : this.cmbType.Tag.ToString();
            string quality = this.cmbQuality.Tag == null ? "" : this.cmbQuality.Tag.ToString();

            string state = this.cmbState.Text;
            if (!this.chbState.Checked)
            {
                state = "";
            }

            string storeCondition = this.cmbStockFilterCondition.Text;            
            string storeQty = this.txtStockNum.Text;
            if (!this.chbStock.Checked)
            {
                storeCondition = "";
                storeQty = "";
            }

            frmEasyData frm = new frmEasyData();

            frm.EasyLabel = "��ѯ�趨����";
            frm.MaxLength = 18;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
            {
                return -1;
            }

            saveStr = string.Format(saveStr, type, quality, state, storeCondition, storeQty);

            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();

            cons.ID = consManager.GetDateTimeFromSysDateTime().ToString("yyMMddHH:mm:ss");
            cons.Name = frm.EasyData;
            cons.Memo = saveStr;
            cons.IsValid = true;

            if (consManager.SetConstant("PhaQuickQuery", cons) == -1)
            {
                MessageBox.Show(Language.Msg("������ٲ�ѯ�趨��Ϣ��������") + consManager.Err);
                return -1;
            }            

            MessageBox.Show(Language.Msg("����ɹ�"));

            #region ͬ�����¿��ٲ�ѯ�ؼ�ָ���� by Sunjh 2010-8-23 {A115CC11-A5B8-4835-9D2E-41733059C82A}

            ArrayList alQuickQuery = consManager.GetList("PhaQuickQuery");
            if (alQuickQuery == null)
            {
                MessageBox.Show(Language.Msg("���ݳ�������ȡ���ٲ�ѯ�趨��������") + consManager.Err);
                return -1;
            }

            this.hsQuickQuery = new Hashtable();

            foreach (FS.FrameWork.Models.NeuObject info in alQuickQuery)
            {
                this.hsQuickQuery.Add(info.ID, info);
            }

            this.cmbQuickQuery.AddItems(alQuickQuery);

            #endregion

            return 1;
        }

        /// <summary>
        /// ��ȡ���ٲ�ѯ�����趨
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int GetQuickQuery(string quickQueryStr)
        {
            /*
            * 1�������ʽ��Type: Quality: State: StoreCondition: StoreQty;
           */

            int privPos = quickQueryStr.IndexOf("Type:");
            int nextPos = quickQueryStr.IndexOf("Quality:");                       

            //��ȡҩƷ����ѯ�趨
            string queryType = quickQueryStr.Substring(privPos + 5, nextPos - privPos - 5);
            if (queryType != null)
            {
                this.cmbType.Tag = queryType;
            }

            privPos = nextPos;
            nextPos = quickQueryStr.IndexOf("State:");
            //��ȡҩƷ���ʲ�ѯ�趨
            string queryQuality = quickQueryStr.Substring(privPos + 8, nextPos - privPos - 8);
            if (queryQuality != null)
            {
                this.cmbQuality.Tag = queryQuality;
            }

            privPos = nextPos;
            nextPos = quickQueryStr.IndexOf("StoreCondition:");
            //��ȡ���״̬��ѯ�趨
            string queryState = quickQueryStr.Substring(privPos + 6, nextPos - privPos - 6);
            if (queryState != null && queryState != "")
            {                
                this.chbState.Checked = true;
            }
            else
            {                 
                this.chbState.Checked = false;
            }
            this.cmbState.Text = queryState;

            privPos = nextPos;
            nextPos = quickQueryStr.IndexOf("StoreQty:");

            //��ȡ���������ѯ�趨
            string queryCondition = quickQueryStr.Substring(privPos + 15, nextPos - privPos - 15);
            if (queryCondition != null && queryCondition != "")
            {
                this.cmbStockFilterCondition.Text = queryCondition;
                this.chbStock.Checked = true;
            }
            else
            {
                this.chbStock.Checked = false;
            }

            privPos = nextPos;
            string queryStore = quickQueryStr.Substring(privPos + 9);
            if (queryStore != null && queryStore != "")
            {
                this.txtStockNum.Text = queryStore;
                this.chbStock.Checked = true;
            }
            else
            {
                this.chbStock.Checked = false;
            }

            return 1;
        }

        /// <summary>
        /// ɾ�����ٲ�ѯ�趨
        /// </summary>
        /// <returns>ɾ�����ٲ�ѯ�趨</returns>
        public int DelQuickQuery()
        {
            if (this.cmbQuickQuery.Text == "" || this.cmbQuickQuery.Tag == null)
            {
                MessageBox.Show(Language.Msg("����ѡ����ɾ��������"));
                return 0;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ����ǰ��ʾ�Ŀ��ٲ�ѯ�趨��?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return -1;
            }
            if (this.hsQuickQuery.ContainsKey(this.cmbQuickQuery.Tag.ToString()))
            {
                FS.FrameWork.Models.NeuObject quickObj = this.hsQuickQuery[this.cmbQuickQuery.Tag.ToString()] as FS.FrameWork.Models.NeuObject;

                FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

                if (consManager.DelConstant("PhaQuickQuery", quickObj.ID) == -1)
                {
                    MessageBox.Show(Language.Msg("ɾ�����ٲ�ѯ�趨��Ϣ��������") + consManager.Err);
                    return -1;
                }
                //ͬ�����¿��ٲ�ѯ�ؼ�ָ���� by Sunjh 2010-8-23 {A115CC11-A5B8-4835-9D2E-41733059C82A}
                this.cmbQuickQuery.Items.RemoveAt(this.cmbQuickQuery.SelectedIndex);

                MessageBox.Show(Language.Msg("ɾ���ɹ�"));
            }

            return 1;
        }

        #endregion

        #region ����������

        /// <summary>
        /// �Զ����ɾ�����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int SetCaution()
        {
            //{F4D82F23-CCDC-45a6-86A1-95D41EF856B8} �������Ը�ֵ
            if (this.cmbDept.SelectedItem == null)
            {
                return -1;
            }

            using (ucPhaAlter uc = new ucPhaAlter())            
            {
                //{F4D82F23-CCDC-45a6-86A1-95D41EF856B8} �������Ը�ֵ
                uc.DeptCode = this.cmbDept.SelectedItem.ID;
                uc.IsQueryExpandData = true;

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (uc.Result == DialogResult.OK)
                {
                    List<FS.FrameWork.Models.NeuObject> alList = uc.ExpandList;
                    if (alList != null)
                    {
                        System.Collections.Hashtable hsList = new Hashtable();
                        foreach (FS.FrameWork.Models.NeuObject info in alList)
                        {
                            hsList.Add(info.ID,info);
                        }
                        foreach (DataRow dr in this.dtData.Rows)
                        {
                            if (hsList.ContainsKey(dr["ҩƷ����"].ToString()))
                            {
                                FS.FrameWork.Models.NeuObject temp = hsList[dr["ҩƷ����"].ToString()] as FS.FrameWork.Models.NeuObject;
                                dr["��Ϳ����"] = FS.FrameWork.Function.NConvert.ToDecimal(temp.User02);
                                dr["��߿����"] = FS.FrameWork.Function.NConvert.ToDecimal(temp.User03);
                            }
                        }
                    }

                    MessageBox.Show("���������óɹ�");
                }
            }

            return 1;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                if (!this.DesignMode)
                {
                    //if (!FS.HISFC.BizProcess.Integrate.Pharmacy.ChoosePiv("0302"))
                    //    return;

                    this.InitControlParam();

                    this.xmlFilePath = Application.StartupPath + "\\" + this.xmlFilePath;

                    this.InitData();                    

                    this.SetDataTable();

                    this.Focus();

                    this.sysDate = this.itemManager.GetDateTimeFromSysDateTime().Date;

                    this.InitDeptList();

                    this.neuLabel8.AutoSize = false;

                    this.neuLabel8.Width = 180;
                    this.neuLabel8.Height = 34;

                    //��ת�г�ʼ�� ����
                    this.ucJumpSet_InitDataEvent();
                    this.ucJumpSet_SaveFinishedEvent();

                    this.PrivManager();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.CodeFilter();
        }

        private void txtQueryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex > 0)
                {
                    this.neuSpread1_Sheet1.ActiveRowIndex--;
                    return;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.RowCount)
                {
                    this.neuSpread1_Sheet1.ActiveRowIndex++;
                    return;
                }
            }
        }

        private void txtQueryCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //�س�ʱ����ת�Ƶ��༭��
            if (e.KeyChar == (char)13)
            {
                this.neuSpread1_Sheet1.ActiveColumnIndex = this.GetColumnIndex(this.nowEditColumn);
                this.neuSpread1.Focus();
            }
        }

        private void neuSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.neuSpread1.ActiveSheet.RowCount <= 0)
            {
                return;
            }
            int activeRowindex = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            string sHwh = this.neuSpread1.ActiveSheet.Cells[activeRowindex, this.GetColumnIndex("��λ��")].Text;
            if (sHwh.Length >= this.placeNoLength)
            {
                MessageBox.Show("��λ�ų��Ȳ��ܴ���" + this.placeNoLength.ToString() + "λ\n ��鿴���ղ�����Ļ�λ��");
                this.neuSpread1.ActiveSheet.SetActiveCell(e.Row, e.Column);
                return;
            }
            //������Ϳ�������ܴ���������� by Sunjh 2010-8-24 {CCAE2615-E287-4629-A163-41675012998B}
            if (e.Column == this.GetColumnIndex("��Ϳ����") || e.Column == this.GetColumnIndex("��߿����"))
            {
                decimal lowNums = Convert.ToDecimal(this.neuSpread1.ActiveSheet.Cells[activeRowindex, this.GetColumnIndex("��Ϳ����")].Text);
                decimal highNums = Convert.ToDecimal(this.neuSpread1.ActiveSheet.Cells[activeRowindex, this.GetColumnIndex("��߿����")].Text);
                if (lowNums > highNums)
                {
                    MessageBox.Show("��Ϳ�������ܴ�����߿����");
                    this.neuSpread1.ActiveSheet.SetActiveCell(e.Row, e.Column);
                    this.neuSpread1.ActiveSheet.Cells[e.Row, this.GetColumnIndex("��Ϳ����")].Text = "0";
                    return;
                }
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.isShowDrugDetail)
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                    return;
                FS.HISFC.Models.Pharmacy.Item item = this.itemManager.GetItem(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.GetColumnIndex("ҩƷ����")].Text);
                if (item != null)
                {
                    this.PopDrugDetail(item);
                }
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh(false);
        }

        private void chbState_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbState.Enabled = this.chbState.Checked;
        }

        private void chbStock_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbStockFilterCondition.Enabled = this.chbStock.Checked;
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            this.CombinedFilter();
        }

        private void ucColumn_DisplayEvent(object sender, EventArgs e)
        {
            if (this.isSetJump)
            {
                #region ��������ת˳��

                List<string> checkCol = this.ucColumn.GetCheckCol(FS.HISFC.Components.Common.Controls.CheckCol.Set);
                this.hsJumpColumn = new Hashtable();
                bool firstColumn = true;
                foreach (string str in checkCol)
                {
                    int iIndex = this.GetColumnIndex(str);
                    this.hsJumpColumn.Add(iIndex, str);
                    if (firstColumn)
                    {
                        this.nowEditColumn = str;
                        firstColumn = false;
                    }
                }

                #endregion
            }
            else
            {
                #region ��������ʾ

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("����Ӧ��������...���Ժ�"));
                Application.DoEvents();

                try
                {
                    this.Refresh(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ӧ��������ʧ��" + ex.Message);
                }
                finally
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }

                #endregion
            }
        }

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, this.xmlFilePath);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus && keyData == Keys.Enter)
            {
                int i = this.neuSpread1_Sheet1.ActiveColumnIndex;
                if (this.hsJumpColumn.Contains(i))
                {
                    while (i < this.neuSpread1_Sheet1.Columns.Count - 1)
                    {
                        i++;
                        if (this.hsJumpColumn.Contains(i))
                        {
                            this.neuSpread1_Sheet1.ActiveColumnIndex = i;
                            return true;
                        }
                    }
                    this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
                    this.Focus();
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        private void lnkShowDetail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.panelDetail.Visible)
                this.lnkShowDetail.Text = "��ʾ";
            else
                this.lnkShowDetail.Text = "�ر�";

            this.panelDetail.Visible = !this.panelDetail.Visible;
        }

        private void btnValidQuery_Click(object sender, EventArgs e)
        {
            this.ValidDateFilter();
        }

        private void cmbQuickQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbQuickQuery.Tag != null)
            {
                if (this.hsQuickQuery.ContainsKey(this.cmbQuickQuery.Tag.ToString()))
                {
                    FS.FrameWork.Models.NeuObject quickObj = this.hsQuickQuery[this.cmbQuickQuery.Tag.ToString()] as FS.FrameWork.Models.NeuObject;

                    this.GetQuickQuery(quickObj.Memo);
                }
            }
        }

        private void btnDetailHead_Click(object sender, EventArgs e)
        {
            this.GetData();
        }

        /// <summary>
        /// �����ϸֱ�Ӳ鿴������¼ by Sunjh 2010-9-27 {32124B47-8129-4bb4-8067-807B8206668E}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInOutDetail_Click(object sender, EventArgs e)
        {
            string inOutDetailSql = "select * from ("
                 + " select '���' as ����,t.trade_name as ҩƷ����,t.in_num as ����,t.min_unit as ��λ,t.oper_date as ʱ�� "
                 + " from pha_com_input t where t.drug_dept_code='" + this.cmbDept.SelectedItem.ID + "' and t.drug_code='" 
                 + this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.GetColumnIndex("ҩƷ����")].Text + "'"
                 + " and t.oper_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')"
                 + " and t.oper_date<to_date('{1}','yyyy-MM-dd hh24:mi:ss')"
                 + " union all"
                 + " select '����' as ����,t1.trade_name as ҩƷ����,t1.out_num as ����,t1.min_unit as ��λ,t1.oper_date as ʱ�� "
                 + " from pha_com_output t1 where t1.drug_dept_code='" + this.cmbDept.SelectedItem.ID + "' and t1.drug_code='"
                 + this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.GetColumnIndex("ҩƷ����")].Text + "'"
                 + " and t1.oper_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')"
                 + " and t1.oper_date<to_date('{1}','yyyy-MM-dd hh24:mi:ss')"
                 + " )"
                 + " order by ʱ��";

            FS.HISFC.Components.Pharmacy.Base.ucCommonPopQuery ucPopList = new FS.HISFC.Components.Pharmacy.Base.ucCommonPopQuery();
            ucPopList.SqlStr = inOutDetailSql;
            ucPopList.ArgColumnWith = new string[5] { "35", "160", "80", "35", "150" };
            ucPopList.TopInfo = "ҩƷ������ѯ";
            ucPopList.IsShowConditionPanel = true;
            ucPopList.BottomInfo = "";
            ucPopList.Width = 600;
            ucPopList.Height = 500;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPopList);
        }
    }
}
