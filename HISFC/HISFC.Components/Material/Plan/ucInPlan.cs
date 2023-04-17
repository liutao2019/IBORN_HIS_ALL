using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Material.Plan
{
    /// <summary>
    /// ͨ������ListState��SaveState���� ���������̴��������Ӷ����������
    /// </summary>
    public partial class ucInPlan : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucInPlan()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// ��ֵ��������
        /// </summary>
        private FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

        /// <summary>
        /// ���ڼ����վ���������������
        /// </summary>
        private int outday = 30;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper produceHelpter = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���ƻ�������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

        /// <summary>
        /// ��Ʒ������Ϣ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// �洢�ƻ�����
        /// </summary>
        private System.Collections.Hashtable hsPlanData = new Hashtable();

        ///// <summary>
        ///// ���ƻ�����Ҫ��˵Ĵ��� ���2��
        ///// </summary>
        //private int inplanExamTimes = 0;

        /// <summary>
        /// �Ƿ�Լƻ�����Ϊ0������Ч���ж� 
        /// </summary>
        private bool isJudgeValid = true;

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private string nowBillNO = "";

        /// <summary>
        /// �ƻ����� ���ƻ������� 0 �ֹ��ƻ� 1 ������ 2 ���� 3 ʱ�� 4 ������ 5 ģ��
        /// </summary>
        private string planType = "0";

        /// <summary>
        /// �Ƿ�Լƻ���Ϊ������ж�
        /// </summary>
        private bool isCheckNumZero = true;

        /// <summary>
        /// �жϵ�ǰ�򿪴����Ƿ�����˴���
        /// </summary>
        private bool isCheck = false;

        /// <summary>
        /// �Ƿ���Ҫ�������
        /// </summary>
        private bool isFinance = false;

        /// <summary>
        /// �Ƿ�ʹ����С��λ���д���
        /// </summary>
        private bool isMinUnit = false;

        /// <summary>
        /// �ƻ�������
        /// </summary>
        private BillTypeEnum billType = BillTypeEnum.PlanList;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime BeginTime;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime EndTime;

        /// <summary>
        /// ��ǰ�������ͣ����ƻ������ƻ����
        /// </summary>
        private EnumWindowFunInPlan winFun = EnumWindowFunInPlan.���ƻ�;

        #endregion

        #region ����

        /// <summary>
        /// ���ڼ����վ��������������� ͳ������
        /// </summary>
        [Description("���ڼ����վ��������������� ͳ������"), Category("����")]
        public int Outday
        {
            get
            {
                return this.outday;
            }
            set
            {
                this.outday = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        [Description("������� ���ݲ�ͬҽԺ��������"), Category("����"), DefaultValue("���ƻ���")]
        public string Title
        {
            get
            {
                return this.lbTitle.Text;
            }
            set
            {
                this.lbTitle.Text = value;
            }
        }

        /// <summary>
        /// �Ƿ�Լƻ�����Ϊ0������Ч���ж�
        /// </summary>
        [Description("�Ƿ�Լƻ�����Ϊ0������Ч���ж�"), Category("����"), DefaultValue(true)]
        public bool IsJudgeValid
        {
            get
            {
                return this.isJudgeValid;
            }
            set
            {
                this.isJudgeValid = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�б���
        /// </summary>
        [Description("�б�ѡ��ؼ��Ƿ���ʾ�б���"), Category("����"), DefaultValue(true)]
        public bool IsShowRowHeader
        {
            get
            {
                return this.ucMaterialItemList1.ShowFpRowHeader;
            }
            set
            {
                this.ucMaterialItemList1.ShowFpRowHeader = value;
            }
        }

        /// <summary>
        /// �Ƿ�����ͨ��������ȷ��ѡ������
        /// </summary>
        [Description("�б�ѡ��ؼ��Ƿ�����ͨ��������ȷ��ѡ������"), Category("����"), DefaultValue(false)]
        public bool IsSelectByNumber
        {
            get
            {
                return this.ucMaterialItemList1.IsUseNumChooseData;
            }
            set
            {
                this.ucMaterialItemList1.IsUseNumChooseData = value;
            }
        }

        /// <summary>
        /// �Ƿ�Լƻ����Ƿ�Ϊ������ж�
        /// </summary>
        [Description("�Ƿ�Լƻ����Ƿ�Ϊ������ж�"), Category("����"), DefaultValue(false)]
        public bool IsCheckNumZero
        {
            get
            {
                return this.isCheckNumZero;
            }
            set
            {
                this.isCheckNumZero = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�ƻ����б�
        /// </summary>
        [Browsable(false)]
        public bool IsShowList
        {
            get
            {
                return this.ucMaterialItemList1.ShowTreeView;
            }
            set
            {
                this.ucMaterialItemList1.ShowTreeView = value;

                this.IsShowLeftPanel = true;
            }
        }

        /// <summary>
        /// �жϵ�ǰ�򿪴����Ƿ�����˴���
        /// </summary>
        [Browsable(false)]
        public bool IsCheck
        {
            get
            {
                return this.isCheck;
            }
            set
            {
                this.isCheck = value;

                this.toolBarService.SetToolButtonEnabled("�������", !value);
                this.toolBarService.SetToolButtonEnabled("�� �� ��", !value);
            }
        }

        /// <summary>
        /// ��ǰ�������ͣ����ƻ������ƻ����
        /// </summary>
        [Description("���ڹ���"), Category("����"), DefaultValue(false)]
        public EnumWindowFunInPlan WinFun
        {
            get
            {
                return this.winFun;
            }
            set
            {
                this.winFun = value;
                if (this.winFun == EnumWindowFunInPlan.���ƻ�)
                {
                    this.IsCheck = false;
                }
                else
                {
                    this.IsCheck = true;
                }
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ�������
        /// </summary>
        public bool IsFinance
        {
            get
            {
                return this.isFinance;
            }
            set
            {
                this.isFinance = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ���Panel
        /// </summary>
        public bool IsShowLeftPanel
        {
            get
            {
                return !this.splitContainer1.Panel1Collapsed;
            }
            set
            {
                this.splitContainer1.Panel1Collapsed = !value;
            }
        }

        #endregion

        #region ״̬�������

        /// <summary>
        /// �����б����״̬
        /// </summary>
        private string listState = "0";

        /// <summary>
        /// ���ݱ���״̬
        /// </summary>
        private string saveState = "1";

        /// <summary>
        /// �����б����״̬
        /// </summary>
        [Description("�����б����״̬"), Category("����"), DefaultValue("0")]
        public string ListState
        {
            get
            {
                return this.listState;
            }
            set
            {
                this.listState = value;
            }
        }

        ///// <summary>
        ///// �������ƻ�����Ҫ��˵Ĵ��������2��
        ///// </summary>
        //[Description("�������ƻ�����Ҫ��˵Ĵ��������2��"), Category("����"), DefaultValue("0")]
        //public int InplanExamTimes
        //{
        //    get
        //    {
        //        return this.inplanExamTimes;
        //    }
        //    set
        //    {
        //        this.inplanExamTimes = value;
        //    }
        //}

        /// <summary>
        /// ���ݱ���״̬
        /// </summary>
        [Description("���ݼ���״̬"), Category("����"), DefaultValue("1")]
        public string SaveState
        {
            get
            {
                return this.saveState;
            }
            set
            {
                this.saveState = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�� ʾ ��", "��ʾ��", FS.FrameWork.WinForms.Classes.EnumImageList.Z����, true, false, null);
            toolBarService.AddToolButton("��    ��", "�½��ƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "�ƻ����б�", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("ɾ    ��", "ɾ����ǰѡ��ļƻ�ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("����ɾ��", "ɾ�������ƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "���뵥�б�", FS.FrameWork.WinForms.Classes.EnumImageList.H����, true, false, null);
            toolBarService.AddToolButton("�������", "�������", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ��Һ��, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "��澯����", FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "����ģ�����ɼƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.R������, true, false, null);
            toolBarService.AddToolButton("��    ��", "���üƻ�����������", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);


            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��    ��":
                    this.New();
                    break;
                case "ɾ    ��":
                    this.DeleteData();
                    break;
                case "����ɾ��":
                    this.DeleteDataByBill(this.privDept.ID, this.nowBillNO);
                    break;
                case "�� �� ��":
                    this.tvList.ShowInPlanList(this.privDept, this.listState);
                    this.IsShowList = true;
                    this.billType = BillTypeEnum.PlanList;
                    break;
                case "�� �� ��":
                    this.ShowApplyInfo(false);
                    break;
                case "�������":
                    this.ShowApplyInfo(true);
                    break;
                case "�� �� ��":
                    AddAlterData("0");
                    break;
                case "�� �� ��":
                    AddAlterData("1");
                    break;
                case "��    ��":
                    if (this.IsCheck == false)
                    {
                        if (this.Save() == 1)
                        {
                            this.IsShowList = true;
                        }
                    }
                    else
                    {
                        if (this.SaveCheck() == 1)
                        {
                            this.IsShowList = true;
                        }
                    }

                    break;
                case "��    ��":
                    if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.BeginTime, ref this.EndTime) == 0)
                        return;
                    break;
                case "δ �� ��":
                    this.tvList.ShowInPlanList(this.privDept, "0");
                    this.IsShowList = true;
                    break;
                case "�� �� ��":
                    this.tvList.ShowInPlanList(this.privDept, "2");
                    this.IsShowList = true;
                    break;
                case "�� ʾ ��":
                    this.IsShowLeftPanel = !this.IsShowLeftPanel;
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.IsCheck == false)
            {
                if (this.Save() == 1)
                {
                    this.IsShowList = true;
                }
            }
            else
            {
                if (this.SaveCheck() == 1)
                {
                    this.IsShowList = true;
                }
            }
            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPreview(40, 10, this.neuPanel1);
            return 1;
        }

        /// <summary>
        /// ���ù�������ť״̬
        /// </summary>
        /// <param name="isShowList">�Ƿ���ʾ�̵㵥�б�</param>
        protected void SetToolButton(bool isShowList)
        {
            this.toolBarService.SetToolButtonEnabled("�� �� ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��    ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("����ɾ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("�� ʾ ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("�� �� ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("�� �� ��", !isShowList);
        }

        #endregion

        #region ���ݱ��ʼ��

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void InitData()
        {
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #region �������ݻ�ȡ

            //��ÿ�������
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList deptAll = deptManager.GetDeptmentAll();
            if (deptAll == null)
            {
                MessageBox.Show("������ÿ����б����" + deptManager.Err);
                return;
            }
            this.deptHelper.ArrayObject = deptAll;
            //��ò���Ա����
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList personAl = personManager.GetEmployeeAll();
            if (personAl == null)
            {
                MessageBox.Show("��ȡȫ����Ա�б����!" + personManager.Err);
                return;
            }
            this.personHelper.ArrayObject = personAl;
            //��ȡ��������
            FS.HISFC.BizLogic.Material.ComCompany company = new FS.HISFC.BizLogic.Material.ComCompany();
            ArrayList produceAl = company.QueryCompany("0", "A");
            if (produceAl == null)
            {
                MessageBox.Show("��ȡ���������б����!" + company.Err);
                return;
            }
            this.produceHelpter.ArrayObject = produceAl;

            #endregion
            //{AFE629CC-8493-4344-9792-8611C0BFA1BD}
            this.ucMaterialItemList1.ShowMaterialList(this.privDept.ID);
        }

        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        private void InitDataTable()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {
														  new DataColumn("��Ʒ����",	  dtStr),
														  new DataColumn("���",        dtStr),
														  new DataColumn("�ƻ������",  dtDec),
														  new DataColumn("�ƻ�����",	  dtDec),
														  new DataColumn("��λ",        dtStr),
														  new DataColumn("�ƻ����",	  dtDec),
														  new DataColumn("���ƿ��",	  dtDec),
														  new DataColumn("ȫԺ���",	  dtDec),
														  new DataColumn("��������",	  dtDec),														  
														  new DataColumn("��������",    dtStr),
														  new DataColumn("��ע",        dtStr),
														  new DataColumn("��Ʒ����",	  dtStr),
														  new DataColumn("ƴ����",      dtStr),
														  new DataColumn("�����",      dtStr),
														  new DataColumn("�Զ�����",    dtStr),
														  new DataColumn("�������",dtStr),
                                                         //{4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
                                                          new DataColumn("�ѹ�/δ��",dtStr)
													  });

            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;

            //�趨���ڶ�DataView�����ظ��м���������
            DataColumn[] keys = new DataColumn[1];
            keys[0] = this.dt.Columns["��Ʒ����"];
            this.dt.PrimaryKey = keys;

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;

            this.SetFormat();
        }

        /// <summary>
        /// Fp��ʽ��
        /// </summary>
        private void SetFormat()
        {
            this.numCellType.DecimalPlaces = 4;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPurchasePrice].CellType = this.numCellType;

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpecs].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPurchasePrice].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanCost].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColStockNum].Width = 80F;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColItemNO].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColAllStockNum].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOutTotal].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOwnStockNum].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColStockNum].Visible = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMemo].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].BackColor = System.Drawing.Color.SeaShell;
            // �ѹ�/δ��{4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsBought].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsBought].Visible = true;
            this.SetColIsBoughtValue();
        }
        //�����ѹ�/δ���е�ֵ {4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
        private void SetColIsBoughtValue()
        {            
            ArrayList al = new ArrayList();
            al.Add(new FS.FrameWork.Models.NeuObject("0", "δ��", ""));
            al.Add(new FS.FrameWork.Models.NeuObject("1", "�ѹ�", ""));

            this.neuSpread1.SetColumnList(this.neuSpread1_Sheet1, (int)ColumnSet.ColIsBought, al);
            this.neuSpread1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(neuSpread1_SetItem);
        }

        #endregion

        #region �б��ʼ��

        /// <summary>
        /// ��ⵥ�б������
        /// </summary>
        private tvPlanList tvList = null;

        /// <summary>
        /// ��ⵥ�б��ʼ��
        /// </summary>
        protected void InitPlanList()
        {
            this.tvList = new tvPlanList();
            this.ucMaterialItemList1.TreeView = this.tvList;

            this.tvList.AfterSelect -= new TreeViewEventHandler(tvList_AfterSelect);
            this.tvList.AfterSelect += new TreeViewEventHandler(tvList_AfterSelect);

            this.tvList.DoubleClick -= new EventHandler(tvList_DoubleClick);
            this.tvList.DoubleClick += new EventHandler(tvList_DoubleClick);

            this.ucMaterialItemList1.Caption = "�ƻ����б�";

            this.ShowPlanList();

            this.ucMaterialItemList1.ShowTreeView = true;
        }

        /// <summary>
        /// ��ⵥ�б���ʾ
        /// </summary>
        private void ShowPlanList()
        {
            this.Clear();

            this.tvList.ShowInPlanList(this.privDept, this.listState);
        }

        /// <summary>
        /// ������Ϣ��ʾ
        /// </summary>
        /// <param name="flag">������Ϣ��ʾ���� 1 ��ʾ������ϸ��Ϣ 0 ��ʾ</param>
        public void ShowApplyInfo(bool isShowApplySum)
        {
            this.Clear();

            this.billType = BillTypeEnum.ApplyList;

            if (isShowApplySum)
            {
                this.tvList.Nodes.Clear();
                this.IsShowLeftPanel = false;
                ShowApplySumData(this.privDept.ID, this.BeginTime, this.EndTime);
            }
            else
            {
                this.tvList.ShowApplyList(this.privDept, this.BeginTime, this.EndTime);
                this.IsShowList = true;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �����ݱ��ڼ�������
        /// </summary>
        /// <param name="inPlan"></param>
        private int AddDataToTable(FS.HISFC.Models.Material.InputPlan inPlan)
        {
            try
            {
                if (inPlan.PlanPrice == 0)
                {
                    inPlan.PlanPrice = inPlan.StoreBase.PriceCollection.RetailPrice;
                }

                decimal planCost = inPlan.PlanNum * inPlan.PlanPrice;
                if (this.produceHelpter != null)
                {
                    inPlan.Producer.Name = this.produceHelpter.GetName(inPlan.Producer.ID);
                }

                #region ȡ���ƿ�� X�����װ��X��С��װ��eg:1��4֧
                string strStoreSum = (Math.Floor(inPlan.StoreSum / inPlan.StoreBase.Item.PackQty)).ToString() + inPlan.StoreBase.Item.PackUnit;
                decimal reQty = Math.Ceiling(inPlan.StoreSum % inPlan.StoreBase.Item.PackQty);
                if (reQty > 0)
                {
                    strStoreSum = strStoreSum + reQty.ToString() + inPlan.StoreBase.Item.MinUnit;
                }
                #endregion

                this.dt.Rows.Add(new object[] { 
												  inPlan.StoreBase.Item.Name,                           //��Ʒ����
												  inPlan.StoreBase.Item.Specs,                          //���
												  inPlan.PlanPrice,										//�ƻ������
												  inPlan.PlanNum,                                       //�ƻ�����
												  inPlan.StoreBase.Item.PackUnit,                       //��λ
												  planCost,												//�ƻ����  
												  inPlan.StoreSum / inPlan.StoreBase.Item.PackQty,      //���ƿ��
												  inPlan.StoreTotsum / inPlan.StoreBase.Item.PackQty,   //ȫԺ���
												  inPlan.OutputSum / inPlan.StoreBase.Item.PackQty,     //��������												  
												  inPlan.Producer.Name,									//��������
												  inPlan.Memo,											//��ע
												  inPlan.StoreBase.Item.ID,                             //��Ʒ����
												  inPlan.StoreBase.Item.SpellCode,						//ƴ����
												  inPlan.StoreBase.Item.WbCode,			            	//�����
												  inPlan.StoreBase.Item.UserCode,						//�Զ�����
												  strStoreSum ,                                         //ȡ���ƿ�� X�����װ��X��С��װ��eg:1��4֧
                                                  inPlan.Extend1                                        //�ѹ�δ��//{4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
											  });
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show("DataTable�ڸ�ֵ��������" + e.Message);

                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("DataTable�ڸ�ֵ��������" + ex.Message);

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ������ƻ���Ʒ��Ϣ
        /// </summary>
        /// <returns>�ɹ���ӷ���1 ʧ�ܷ��أ�1</returns>
        public void Clear()
        {
            this.dt.Rows.Clear();
            this.dt.AcceptChanges();

            this.hsPlanData.Clear();

            this.lbPlanBill.Text = "���ݺ�:";
            this.lbPlanInfo.Text = "�ƻ����� �ƻ���";

            this.txtFilter.Text = "";
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns> </returns>
        private bool IsValid()
        {
            if (this.isJudgeValid)
            {
                foreach (DataRow dr in this.dt.Rows)
                {
                    if (NConvert.ToDecimal(dr["�ƻ�����"]) < 0)
                    {
                        MessageBox.Show("����ȷ���� " + dr["��Ʒ����"].ToString() + " �ƻ�����");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isFpFocus"></param>
        public void SetFocus(bool isFpFocus)
        {
            if (isFpFocus)
            {
                this.neuSpread1.Select();
                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColPlanNum;
            }
            else
            {
                this.ucMaterialItemList1.Select();
                this.ucMaterialItemList1.SetFocusSelect();
            }
        }

        /// <summary>
        /// ������һ�����ƻ���
        /// </summary>
        public void New()
        {
            //�������б��в����½ڵ�
            TreeNode node = new TreeNode();
            node.Text = "�½����ƻ���";
            node.ImageIndex = 4;
            node.SelectedImageIndex = 4;
            node.Tag = new FS.HISFC.Models.Material.InputPlan();

            this.tvList.Nodes[0].Nodes.Insert(0, node);

            //ѡ�д��½ڵ�
            this.tvList.SelectedNode = node;

            //�л�����Ʒ�����б�
            this.IsShowList = false;

            this.ucMaterialItemList1.SetFocusSelect();
            this.SetPlanInfo();
        }

        /// <summary>
        /// ����Ʒʵ�����
        /// </summary>
        /// <param name="item">��Ʒʵ��</param>
        /// <param name="totOutQty">��������</param>
        /// <param name="averageOutQty">�վ�����</param>
        /// <param name="planQty">���ݾ������Զ����ɵļƻ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(FS.HISFC.Models.Material.MaterialItem item, decimal totOutQty, decimal averageOutQty, decimal planQty)
        {
            if (this.hsPlanData.ContainsKey(item.ID))
            {
                MessageBox.Show("����Ʒ����ӵ��ƻ����� ͬһƷ����Ʒ�����ظ����");
                return 0;
            }

            FS.HISFC.Models.Material.InputPlan inPlan = new FS.HISFC.Models.Material.InputPlan();

            #region ��ȡ�����ҿ��

            decimal itemQty;
            if (this.storeManager.GetStoreQty(this.privDept.ID, item.ID, out itemQty) == -1)
            {
                MessageBox.Show("��ȡ" + this.privDept.Name + "������Ʒ���ʧ��");
                return -1;
            }
            inPlan.StoreSum = itemQty;

            #endregion

            #region ��ȡȫԺ���

            decimal itemTotQty;
            if (this.storeManager.GetStoreTotQty(item.ID, out itemTotQty) == -1)
            {
                MessageBox.Show("��ȡȫԺ��Ʒ���ʧ��");
                return -1;
            }
            inPlan.StoreTotsum = itemTotQty;

            #endregion

            inPlan.StoreBase.Item = item;
            //inPlan.StoreTotsum = totOutQty;

            inPlan.PlanNum = planQty;
            inPlan.PlanPrice = inPlan.StoreBase.Item.PackPrice;

            inPlan.StorageCode = this.privDept.ID;
            inPlan.Company = inPlan.StoreBase.Item.Company;
            inPlan.Producer = inPlan.StoreBase.Item.Factory;

            if (this.AddDataToTable(inPlan) == 1)
            {
                this.hsPlanData.Add(inPlan.StoreBase.Item.ID, inPlan);
            }

            this.SetSum();

            return 1;
        }

        /// <summary>
        /// ����Ʒʵ�����
        /// </summary>
        /// <param name="item">��Ʒʵ��</param>
        /// <param name="planQty">���������Զ����ɵļƻ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(FS.HISFC.Models.Material.MaterialItem item, decimal planQty)
        {
            return this.AddDrugData(item, 0, 0, planQty);
        }

        /// <summary>
        /// ����Ʒʵ�����
        /// </summary>
        /// <param name="item">��Ʒʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(FS.HISFC.Models.Material.MaterialItem item)
        {

            return this.AddDrugData(item, 0, 0, 0);
        }

        ///<summary>
        ///������Ʒ�����߼�������
        ///</summary>
        ///<param name="alterFlag">���ɷ�ʽ 0 ������ 1 ������</param>
        ///<returns>�ɹ�����0��ʧ�ܷ��أ�1</returns>
        public void AddAlterData(string alterFlag)
        {
            this.IsShowLeftPanel = false;//liuxq add����ʾ����б�

            #region ��ʱ�������ݾ��������ɼƻ������룬�պ����ʵ�����������ӡ�lichao�������� gengxl
            if (this.dt.Rows.Count > 0)
            {
                DialogResult result;
                result = MessageBox.Show("�����������ɽ������ǰ���ƻ������ݣ��Ƿ����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

                if (result == DialogResult.No)
                    return;
            }

            //�������
            this.Clear();

            try
            {
                ArrayList alDetail = new ArrayList();

                if (alterFlag == "1")//������
                {
                    #region �������� ���������Ĳ��� ������������Ϣ
                    using (ucPhaAlter uc = new ucPhaAlter())
                    {
                        uc.DeptCode = this.privDept.ID;
                        uc.SetData();
                        uc.Focus();
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                        if (uc.ApplyInfo != null)
                        {
                            alDetail = uc.ApplyInfo;
                        }
                    }
                    #endregion
                }
                else//������
                {
                    ///---liuxq----��ʱ���Σ��ҵ����ڷſ�////
                    //					alDetail = this.itemManager.FindByAlter("0", this.privDept.ID, System.DateTime.MinValue, System.DateTime.MaxValue);
                    //					if (alDetail == null)
                    //					{
                    //						MessageBox.Show("��������������ִ����Ϣ����δ��ȷִ��\n" + this.itemManager.Err);
                    //						return;
                    //					}
                    ArrayList al = this.storeManager.FindByAlter("0", this.privDept.ID, DateTime.Now, DateTime.Now, 0, 0);
                    if (al != null)
                    {
                        alDetail = al;
                    }
                }

                if (alDetail.Count == 0)
                {
                    MessageBox.Show("��������������Ʒ�ƻ���Ϣ");
                    return;
                }

                FS.HISFC.Models.Material.MaterialItem item = new FS.HISFC.Models.Material.MaterialItem();

                foreach (FS.FrameWork.Models.NeuObject temp in alDetail)
                {
                    item = this.itemManager.GetMetItemByMetID(temp.ID);
                    if (item == null)
                    {
                        MessageBox.Show("��ȡ��Ʒ������Ϣʧ�ܣ�[" + temp.Name + "]��Ʒ������!");
                        continue;
                    }

                    if (alterFlag == "1")
                        this.AddDrugData(item, NConvert.ToDecimal(temp.User01), NConvert.ToDecimal(temp.User02), NConvert.ToDecimal(temp.User03));
                    else
                        this.AddDrugData(item, NConvert.ToDecimal(temp.User03));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            this.SetPlanInfo();
        }

        /// <summary>
        /// ģ��������ʾ
        /// </summary>
        public void AddStencilData()
        {
            #region ��ʱ�����˹��ܣ���ʱ���ԣʱ���ơ�lichao
            /*DialogResult rs = MessageBox.Show("����ģ�����ɼƻ���Ϣ�������ǰ��ʾ������ �Ƿ����?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
			if (rs == DialogResult.No)
				return;

			this.Clear();

			ArrayList alOpenDetail = Function.ChooseDrugStencil(this.privDept.ID,FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan);

			if (alOpenDetail != null && alOpenDetail.Count > 0)
			{             
				FS.FrameWork.WinForms.Classes.Function .ShowWaitForm("���ڸ�����ѡģ���ɼƻ���Ϣ...");
				Application.DoEvents();
				//�ȼ��ؿ����Ϣ��Hs ��֤ģ�����˳��
				System.Collections.Hashtable hsStoreDrug = new Hashtable();

				ArrayList alItem = this.itemManager.QueryItemAvailableList(false);
				foreach (FS.HISFC.Models.Material.MaterialItem item in alItem)
				{
					hsStoreDrug.Add(item.ID, item);
				}

				int i = 0;
				foreach (FS.HISFC.Models.Material.MaterialItem info in alOpenDetail)
				{
					FS.FrameWork.WinForms.Classes.Function .ShowWaitForm(i, alOpenDetail.Count);
					Application.DoEvents();

					if (hsStoreDrug.Contains(info.Item.ID))
					{
						this.AddDrugData(hsStoreDrug[info.Item.ID] as FS.HISFC.Models.Pharmacy.Item);
					}

					i++;
				}

				FS.FrameWork.WinForms.Classes.Function .HideWaitForm();

				this.SetFocus(true);
			}*/
            #endregion
        }

        /// <summary>
        /// �������ƻ����� ��ȡ���ƻ�����
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <param name="billNO">���ݺ�</param>
        public int ShowPlanData(string privDept, string billNO)
        {
            //������ݡ�
            this.Clear();

            //ȡ���ƻ��е�����
            ArrayList alDetail = this.planManager.QueryInPlanDetail(privDept, billNO);
            if (alDetail == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾ�ƻ���ϸ ���Ժ�...");
            Application.DoEvents();

            foreach (FS.HISFC.Models.Material.InputPlan info in alDetail)
            {
                //��������ɹ��ƻ������ݲ���ʾ 
                //if (info.State != "0" && info.State != "2")
                //    continue;
                if (info.State != this.listState)
                {
                    continue;
                }

                info.StoreBase.Item = this.itemManager.GetMetItemByMetID(info.StoreBase.Item.ID);
                if (info.StoreBase.Item == null)
                {
                    Function.ShowMsg("��ȡ����Ʒ��Ϣ�������� \n" + info.StoreBase.Item.Name);
                    return -1;
                }

                this.SetPlanInfo(info);

                if (this.AddDataToTable(info) == 1)
                {
                    this.hsPlanData.Add(info.StoreBase.Item.ID, info);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.SetSum();

            return 1;
        }

        #region ���ݿ�����������������ƻ�

        /// <summary>
        /// ���ݸ�������������������ƻ�
        /// </summary>
        /// <param name="privDept">������</param>
        /// <returns></returns>
        public int ShowApplySumData(string privDept, DateTime dateBegin, DateTime dateEnd)
        {
            //������ݡ�
            this.Clear();

            //ȡ���ƻ��е�����{CDAF22EE-1D2F-44a1-BA62-169E28A421A4}
            //----------------------------------------------------targetdept-currentdept-priv-extend1-inclass-
            //extend1��"0"����״̬ "1"���ƻ� "2" �������� "3" ȫ������
            ArrayList alList = this.storeManager.QueryApplyListByDept("A", privDept, "0510", "0", "13");
                //this.storeManager.QueryApplySumForPlan(privDept, dateBegin, dateEnd);
            if (alList == null)
            {
                MessageBox.Show(this.storeManager.Err);
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾ�ƻ���ϸ ���Ժ�...");
            Application.DoEvents();
            //����д�����뵥ѡ��Ŀؼ� by yuyun 08-7-28 {285D96FA-06E5-4123-87F3-996674851B87}
            FS.HISFC.Components.Material.Base.ucApplyLists ucLists = new FS.HISFC.Components.Material.Base.ucApplyLists();
            ucLists.Init(alList);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            Control c = ucLists as Control;
            c.Text = "���뵥�б�";
            FS.FrameWork.WinForms.Classes.Function.ShowControl(c);
            
            string listNO = string.Empty;
            string storageDept = string.Empty;

            if (ucLists.ListApply.Count>0)
            {
                foreach (ArrayList al in ucLists.ListApply)
                {
                    listNO += "'" + al[1].ToString() + "',";
                    storageDept += "'" + al[3].ToString() + "',";
                }
            }
            if (listNO.Length <= 0 && storageDept.Length <= 0)
            {                
                return -1;
            }
            listNO = listNO.Substring(0, listNO.Length - 1);
            storageDept = storageDept.Substring(0, storageDept.Length - 1);

            ArrayList alDetail = new ArrayList();
            alDetail = this.storeManager.QueryApplySumForPlan(privDept, storageDept, listNO);

            if (alDetail == null)
            {
                MessageBox.Show(this.storeManager.Err);

                return -1;
            }
            //-------------
            foreach (FS.HISFC.Models.Material.Apply info in alDetail)
            {
                FS.HISFC.Models.Material.InputPlan inPlan = new FS.HISFC.Models.Material.InputPlan();
                FS.HISFC.Models.Material.MaterialItem item = new FS.HISFC.Models.Material.MaterialItem();
                FS.HISFC.BizLogic.Material.Store myStore = new FS.HISFC.BizLogic.Material.Store();

                item = this.itemManager.GetMetItemByMetID(info.Item.ID);
                int i = 0;
                decimal itemSum = 0;
                i = myStore.GetStoreQty(this.privDept.ID, info.Item.ID, out itemSum);
                inPlan.StoreBase.Item = item;

                inPlan.StoreSum = itemSum;

                #region ��ȡȫԺ���

                decimal itemTotQty;
                if (this.storeManager.GetStoreTotQty(item.ID, out itemTotQty) == -1)
                {
                    MessageBox.Show("��ȡȫԺ��Ʒ���ʧ��");
                    return -1;
                }
                inPlan.StoreTotsum = itemTotQty;

                #endregion
                //�ƻ����� = �������� / ��װ����{5499029C-B6EF-4015-A855-15DC67BD9E14}
                inPlan.PlanNum = info.Operation.ApplyQty / inPlan.StoreBase.Item.PackQty;

                //inPlan.PlanPrice = inPlan.StoreBase.Item.UnitPrice;
                inPlan.PlanPrice = inPlan.StoreBase.Item.PackPrice;
                inPlan.StorageCode = this.privDept.ID;
                inPlan.Company = inPlan.StoreBase.Item.Company;
                inPlan.Producer = inPlan.StoreBase.Item.Factory;

                if (this.AddDataToTable(inPlan) == 1)
                {
                    this.hsPlanData.Add(inPlan.StoreBase.Item.ID, inPlan);
                }
            }
            this.neuSpread1_Sheet1.Tag = alDetail;
            //�����뵥�ź�������Ҵ������Ա��Ժ��������״̬{CDAF22EE-1D2F-44a1-BA62-169E28A421A4}
            this.neuSpread1_Sheet1.Columns[0].Tag = listNO;
            this.neuSpread1_Sheet1.Columns[1].Tag = storageDept;            

            this.SetSum();

            return 1;
        }

        /// <summary>
        /// �������뵥���γɼƻ���Ϣ
        /// </summary>
        /// <param name="privDept"></param>
        /// <param name="billNO"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public int ShowApplySingleData(string privDept, string billNO, DateTime dateBegin, DateTime dateEnd)
        {
            //������ݡ�
            this.Clear();

            //ȡ���ƻ��е�����
            ArrayList alDetail = this.storeManager.QueryApplyDetailForPlan(privDept, billNO, "0", dateBegin, dateEnd);
            if (alDetail == null)
            {
                MessageBox.Show(this.storeManager.Err);
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾ�ƻ���ϸ ���Ժ�...");
            Application.DoEvents();

            FS.HISFC.Models.Material.InputPlan inPlan = new FS.HISFC.Models.Material.InputPlan();

            foreach (FS.HISFC.Models.Material.Apply info in alDetail)
            {
                inPlan.StoreBase.Item = this.itemManager.GetMetItemByMetID(info.Item.ID);
                FS.HISFC.BizLogic.Material.Store myStore = new FS.HISFC.BizLogic.Material.Store();

                int i = 0;
                decimal itemSum = 0;
                i = myStore.GetStoreQty(this.privDept.ID, info.Item.ID, out itemSum);

                inPlan.StoreSum = itemSum;

                inPlan.PlanNum = info.Operation.ApplyQty;
                //inPlan.PlanPrice = inPlan.StoreBase.Item.UnitPrice;
                inPlan.PlanPrice = inPlan.StoreBase.Item.PackPrice;
                inPlan.StorageCode = this.privDept.ID;
                inPlan.Company = inPlan.StoreBase.Item.Company;
                inPlan.Producer = inPlan.StoreBase.Item.Factory;

                #region ��ȡȫԺ���
                decimal itemTotQty;
                if (this.storeManager.GetStoreTotQty(info.Item.ID, out itemTotQty) == -1)
                {
                    MessageBox.Show("��ȡȫԺ��Ʒ���ʧ��");
                    return -1;
                }
                inPlan.StoreTotsum = itemTotQty;
                #endregion

                if (this.AddDataToTable(inPlan) == 1)
                {
                    this.hsPlanData.Add(inPlan.StoreBase.Item.ID, inPlan);
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.SetSum();

            return 1;
        }

        /// <summary>
        /// װ����������
        /// </summary>
        /// <returns>����DataTable</returns>
        public void InitApplyData()
        {
            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDate = System.Type.GetType("System.DateTime");

            this.dt = new DataTable();

            this.dt.Columns.AddRange(
                new System.Data.DataColumn[] {
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("���",      dtStr),
												 new DataColumn("����",    dtDec),
												 new DataColumn("��λ",  dtStr),
												 new DataColumn("��������",  dtDec),
												 new DataColumn("������",  dtDec),
												 new DataColumn("��ע",      dtStr),
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("���",    dtStr),												
												 new DataColumn("ƴ����",    dtStr),
												 new DataColumn("�����",    dtStr),
												 new DataColumn("�Զ�����",  dtStr),
												 new DataColumn("�������",  dtDec)
												 
											 }
                );

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dt.Columns["��Ʒ����"];

            this.dt.PrimaryKey = keys;

            this.dt.DefaultView.AllowDelete = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowNew = true;

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;

            this.SetApplyFormat();
        }


        /// <summary>
        /// �������뵥��Ϣ��DataTable����������
        /// </summary>
        /// <param name="apply">������Ϣ</param>
        /// <returns></returns>
        protected virtual int AddApplyToTable(FS.HISFC.Models.Material.Apply apply)
        {
            if (this.dt == null)
            {
                this.InitApplyData();
            }

            try
            {
                FS.HISFC.BizLogic.Material.MetItem managerItem = new FS.HISFC.BizLogic.Material.MetItem();
                apply.Item = managerItem.GetMetItemByMetID(apply.Item.ID);

                decimal price = 0;
                decimal qty = 0;
                decimal cost = 0;
                string unit = "";

                if (this.isMinUnit)
                {
                    qty = apply.Operation.ApplyQty;
                    cost = apply.Operation.ApplyQty * apply.Item.UnitPrice;
                    unit = apply.Item.MinUnit;
                    price = apply.Item.UnitPrice;
                }
                else
                {
                    qty = apply.Operation.ApplyQty / apply.Item.PackQty;
                    cost = apply.Operation.ApplyQty / apply.Item.PackQty * apply.Item.PackPrice;
                    unit = apply.Item.PackUnit;
                    price = apply.Item.PackPrice;
                }
                this.dt.Rows.Add(new object[] { 
												  apply.Item.Name,                                //��Ʒ����
												  apply.Item.Specs,                               //���
												  price,					                      //���ۼ�
												  unit,											  //��װ��λ
												  qty,											  //��������
												  cost,                                           //������
												  apply.Memo,                                     //��ע
												  apply.Item.ID,                                  //��Ʒ����
												  apply.SerialNO,                                 //���												 
												  apply.Item.SpellCode,                          //ƴ����
												  apply.Item.WbCode,                             //�����
												  apply.Item.UserCode                            //�Զ�����                            
											  }
                    );

                this.dt.DefaultView.AllowDelete = true;
                this.dt.DefaultView.AllowEdit = true;
                this.dt.DefaultView.AllowNew = true;
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show("DataTable�ڸ�ֵ��������" + e.Message);

                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("DataTable�ڸ�ֵ��������" + ex.Message);

                return -1;
            }

            return 1;
        }


        /// <summary>
        /// �������뵥�� ��ȡ��������
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <param name="billNO">���ݺ�</param>
        public int ShowApplyData(string privDept, string billNO, DateTime dateBegin, DateTime dateEnd)
        {
            //������ݡ�
            this.Clear();

            //ȡ���ƻ��е�����
            ArrayList alDetail = this.storeManager.QueryApplyDetailForPlan(privDept, billNO, "0", dateBegin, dateEnd);
            if (alDetail == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾ�ƻ���ϸ ���Ժ�...");
            Application.DoEvents();

            foreach (FS.HISFC.Models.Material.Apply info in alDetail)
            {
                if (this.AddApplyToTable(info) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.SetSum();

            return 1;
        }


        /// <summary>
        /// ��ʽ��Fp��ʾ
        /// </summary>
        public virtual void SetApplyFormat()
        {
            if (this.neuSpread1_Sheet1 == null)
                return;

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColItemName].Width = 150F;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColSpecs].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColRetailPrice].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColPackUnit].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColApplyQty].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColApplyCost].Width = 100F;

            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColItemID].Visible = false;           //��Ʒ����
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColNO].Visible = false;               //���
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColDataSource].Visible = false;       //������Դ
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColWBCode].Visible = false;           //�����
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColUserCode].Visible = false;         //�Զ�����

            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColMemo].Width = 200F;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColMemo].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColApplyQty].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ApplyColumnSet.ColApplyQty].BackColor = System.Drawing.Color.SeaShell;
        }


        /// <summary>
        /// ������
        /// </summary>
        private enum ApplyColumnSet
        {
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColItemName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ����
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ��λ
            /// </summary>
            ColPackUnit,
            /// <summary>
            /// ��������
            /// </summary>
            ColApplyQty,
            /// <summary>
            /// ������
            /// </summary>
            ColApplyCost,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ColItemID,
            /// <summary>
            /// ���
            /// </summary>
            ColNO,
            /// <summary>
            /// ������Դ
            /// </summary>
            ColDataSource,
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpellCode,
            /// <summary>
            /// �����
            /// </summary>
            ColWBCode,
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColUserCode
        }

        #endregion

        /// <summary>
        /// ���üƻ���Ϣ��ʾ
        /// </summary>
        /// <param name="inPlan"></param>
        private void SetPlanInfo(FS.HISFC.Models.Material.InputPlan inPlan)
        {
            this.lbPlanBill.Text = "���ݺ�:" + inPlan.PlanListCode;

            this.lbPlanInfo.Text = "�ƻ�����: " + this.privDept.Name + " �ƻ���: " + inPlan.StoreBase.Operation.ApplyOper.ID;
        }
        private void SetPlanInfo()
        {
            this.lbPlanInfo.Text = "�ƻ�����: " + this.privDept.Name + " �ƻ���: " + FrameWork.Management.Connection.Operator.Name;
        }

        /// <summary>
        /// �ƻ��ܽ�����
        /// </summary>
        private void SetSum()
        {
            decimal totCost = 0;

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                totCost += NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColPlanCost].Text);
            }

            this.lbCost.Text = "�ƻ��ܽ��:" + totCost.ToString("N");
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void DeleteData()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 1)
            {
                MessageBox.Show("�ƻ�����ֻ��һ����Ʒ��¼ ��ѡ������ɾ����ʽ���в���");
                return;
            }
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
                return;

            this.neuSpread1.StopCellEditing();

            string drugNO = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColItemNO].Text;
            if (this.hsPlanData.ContainsKey(drugNO))
            {
                this.hsPlanData.Remove(drugNO);
            }

            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);

            this.neuSpread1.StartCellEditing(null, false);
        }

        /// <summary>
        /// �����ƻ�����������ɾ��
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="billCode">���ƻ�����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int DeleteDataByBill(string deptCode, string billCode)
        {
            if (this.nowBillNO == "")
                return 0;

            DialogResult result;
            //��ʾ�û��Ƿ�ȷ��ɾ��
            result = MessageBox.Show("ȷ��ɾ����" + this.nowBillNO + "���ƻ�����\n �˲����޷�����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                int parm = this.planManager.DeleteInputPlan(deptCode, billCode);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.planManager.Err);
                    return -1;
                }
                else
                    if (parm != this.dt.Rows.Count)
                    { //������
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���ݷ����䶯����ˢ�´���");
                        return -1;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.tvList.ShowInPlanList(this.privDept, "0");

            return 1;
        }

        /// <summary>
        /// �������ƻ���- �½�
        /// </summary>
        public int Save()
        {
            if (this.dt.Rows.Count <= 0)
                return -1;
            if (!this.IsValid())
                return -1;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б��� ���Ժ�...");
            Application.DoEvents();

            //ϵͳʱ��
            DateTime sysTime = this.planManager.GetDateTimeFromSysDateTime();

            //�������ݿ⴦������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ������޸ĵ����ƻ���������ɾ��ԭ���ƻ�������

            if (this.nowBillNO != null && this.nowBillNO != "")
            {
                ArrayList alCount = this.planManager.QueryInPlanDetail(this.privDept.ID, this.nowBillNO);

                //ɾ��δ�ɹ���˵ļƻ���Ϣ
                int parm = this.planManager.DeleteInputPlan(this.privDept.ID, this.nowBillNO);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg(this.itemManager.Err);
                    return -1;
                }
                else if (parm < alCount.Count)
                { //������
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("�ƻ���������ͨ���ɹ���ˣ���ˢ�´���");
                    return -1;
                }
            }
            else
            {
                //����������ӵ����ƻ�������ȡ���ƻ�����
                this.nowBillNO = this.planManager.GetPlanNO(this.privDept.ID);
                //���ƻ����ŵĲ���
                if (this.nowBillNO == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("��ȡ�¼ƻ����ų���" + this.itemManager.Err);
                    return -1;
                }
            }

            #endregion

            int iCount = 1;

            foreach (DataRow dr in this.dt.Rows)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(iCount, this.dt.Rows.Count);
                Application.DoEvents();

                #region ���ƻ���ֵ ����

                //�Լƻ�����Ϊ0�Ĳ����д���
                if (NConvert.ToDecimal(dr["�ƻ�����"]) == 0)
                    continue;

                FS.HISFC.Models.Material.InputPlan inPlan = this.hsPlanData[dr["��Ʒ����"].ToString()] as FS.HISFC.Models.Material.InputPlan;

                inPlan.PlanListCode = this.nowBillNO;               //�ƻ�����
                inPlan.PlanNo = iCount;
                //���ڲ����������ƻ����Ƿ���Ҫ���  
                //switch (inplanExamTimes)
                //{
                //    case 0:
                //        inPlan.State = "M";
                //        break;
                //    case 1:
                //        inPlan.State = "F";
                //        break;
                //    case 2:
                //        inPlan.State = "0";
                //        break;
                //    default:
                //        inPlan.State = "0";
                //        break;
                //}                                
                //����״̬
                inPlan.State = this.saveState;
                inPlan.PlanType = this.planType;                    //�ɹ�����

                inPlan.PlanNum = NConvert.ToDecimal(dr["�ƻ�����"]); //* inPlan.StoreBase.Item.PackQty;//�ƻ����� - �洢��������������װ������Ϊ�˷������ƻ����
                inPlan.PlanCost = NConvert.ToDecimal(dr["�ƻ�����"]) * inPlan.PlanPrice;
                inPlan.StoreBase.Operation.ApplyOper.ID = this.planManager.Operator.ID;
                inPlan.StoreBase.Operation.ApplyOper.OperTime = sysTime;                 //������Ϣ

                inPlan.StoreBase.Operation.Oper = inPlan.StoreBase.Operation.ApplyOper;
                inPlan.Memo = dr["��ע"].ToString();                //��ע
                inPlan.Producer = inPlan.StoreBase.Item.Factory;
                ////{4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
                inPlan.Extend1 = dr["�ѹ�/δ��"].ToString();
                if (this.planManager.InsertInputPlan(inPlan) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg(inPlan.StoreBase.Item.Name + "����ʧ�� " + this.planManager.Err);

                    return -1;
                }


                #endregion

                iCount++;
            }
            //{4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
            #region ��Ӧ���뵥��extend1���³�"1" ���������ƻ������뵥����ͨ���뵥���ֿ�
            string listNO = string.Empty;
            string storageDept = string.Empty;

            if (this.neuSpread1_Sheet1.Columns[0].Tag != null && this.neuSpread1_Sheet1.Columns[1].Tag != null)
            {
                listNO = this.neuSpread1_Sheet1.Columns[0].Tag.ToString();
                storageDept = this.neuSpread1_Sheet1.Columns[1].Tag.ToString();

                if (!string.IsNullOrEmpty(listNO) && !string.IsNullOrEmpty(storageDept))
                {
                    if (this.storeManager.UpdateApplyListState(listNO, storageDept, privDept.ID, "1") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMsg("�������뵥״̬ʧ�� " + this.storeManager.Err);

                        return -1;
                    }
                } 
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            Function.ShowMsg("����ɹ�");
            //�������
            this.Clear();

            this.tvList.ShowInPlanList(this.privDept, "0");            

            return 1;
        }

        /// <summary>
        /// �������ƻ���-���
        /// </summary>
        public int SaveCheck()
        {
            if (this.dt.Rows.Count <= 0)
                return -1;
            if (!this.IsValid())
                return -1;

            dt.AcceptChanges();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б��� ���Ժ�...");
            Application.DoEvents();

            //ϵͳʱ��
            DateTime sysTime = this.planManager.GetDateTimeFromSysDateTime();

            //�������ݿ⴦������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int iCount = 1;
            string saveState = "2";//����״̬

            foreach (DataRow dr in this.dt.Rows)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(iCount, this.dt.Rows.Count);
                Application.DoEvents();

                #region ���ƻ���ֵ ����

                //�Լƻ�����Ϊ0�Ĳ����д���
                if (NConvert.ToDecimal(dr["�ƻ�����"]) == 0)
                    continue;

                FS.HISFC.Models.Material.InputPlan inPlan = this.hsPlanData[dr["��Ʒ����"].ToString()] as FS.HISFC.Models.Material.InputPlan;

                inPlan.PlanListCode = this.nowBillNO;               //�ƻ�����

                if (this.IsFinance == false)						//�ƻ���״̬�ж� 1:��Ҫ����������� 2:����Ҫ����������� 
                {
                    saveState = "2";
                    inPlan.State = "2";
                }
                else
                {
                    saveState = "1";
                    inPlan.State = "1";
                }

                saveState = this.saveState;
                inPlan.State = this.saveState;

                inPlan.PlanType = this.planType;                    //�ɹ�����

                inPlan.PlanNum = NConvert.ToDecimal(dr["�ƻ�����"]);// * inPlan.StoreBase.Item.PackQty;//�ƻ�����- �洢��������������װ������Ϊ�˷������ƻ����
                inPlan.PlanCost = NConvert.ToDecimal(dr["�ƻ�����"]) * inPlan.PlanPrice;

                inPlan.StoreBase.Operation.ApplyOper.ID = this.planManager.Operator.ID;
                inPlan.StoreBase.Operation.ApplyOper.OperTime = sysTime;                 //������Ϣ

                inPlan.StoreBase.Operation.Oper = inPlan.StoreBase.Operation.ApplyOper;
                inPlan.Memo = dr["��ע"].ToString();                //��ע

                if (this.planManager.UpdateInputPlan(inPlan) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg(inPlan.StoreBase.Item.Name + "�������ƻ�ʧ�� " + this.planManager.Err);
                    return -1;
                }

                if (this.planManager.UpdateInPlanState(inPlan.StorageCode, inPlan.PlanListCode, inPlan.PlanNo, saveState, inPlan.StoreBase.Operation.ApplyOper.ID, sysTime) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg(inPlan.StoreBase.Item.Name + "����ʧ�� " + this.planManager.Err);
                    return -1;
                }

                #endregion

                iCount++;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            Function.ShowMsg("����ɹ�");
            //�������
            this.Clear();
            //{5A17420D-209C-4862-80BE-97CE0D539C50}
            //this.tvList.ShowInPlanList(this.privDept, "0");
            this.tvList.ShowInPlanList(this.privDept, this.listState);
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public void Filter()
        {
            if (this.dt.DefaultView == null)
                return;

            //��ù�������
            string queryCode = "%" + this.txtFilter.Text.Trim() + "%";

            try
            {
                this.dt.DefaultView.RowFilter = Function.GetFilterStr(this.dt.DefaultView, queryCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��������ΪExcel��ʽ
        /// </summary>
        private void ExportInfo()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����ǰ��ѯ���ݰ�Excel��ʽ����
        /// </summary>
        public void Export()
        {
            this.ExportInfo();
        }

        private void GetDetail(string deptcode, DateTime dtbegin, DateTime dtend, string itemcode)
        {
            DataSet myDataSet = new DataSet();

            try
            {
                FS.HISFC.BizLogic.Manager.Report reportMgr = new FS.HISFC.BizLogic.Manager.Report();
                int parm = reportMgr.ExecQuery("Material.Store.GetApplySumForPlanDetail", ref myDataSet,
                    deptcode, dtbegin.ToString("yyyy-MM-dd HH:mm:ss"), dtend.ToString("yyyy-MM-dd HH:mm:ss"),
                    itemcode);
                if (parm == -1)
                {
                    MessageBox.Show("��ѯ��ϸʧ��");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //��ʽ�����ļ���ַ

            //��farpoint������Դ
            DataView myDataView = new DataView(myDataSet.Tables[0]);
            this.neuSpread1_Sheet2.DataSource = myDataView;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.GrayAreaBackColor = System.Drawing.Color.White;
        }

        #endregion

        #region �¼�

        private void ucInPlan_Load(object sender, System.EventArgs e)
        {
            this.InitDataTable();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
                //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0511", ref testPrivDept);

                //if (parma == -1)            //��Ȩ��
                //{
                //    MessageBox.Show("���޴˴��ڲ���Ȩ��");
                //    return;
                //}
                //else if (parma == 0)       //�û�ѡ��ȡ��
                //{
                //    return;
                //}

                //this.privDept = testPrivDept;

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
                Application.DoEvents();

                this.InitData();

                this.InitPlanList();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            }
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node != null && e.Node.Parent != null)
            {
                FS.FrameWork.Models.NeuObject inPlanObj = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                this.nowBillNO = inPlanObj.ID;

                if (this.billType == BillTypeEnum.PlanList)
                {
                    this.ShowPlanData(this.privDept.ID, inPlanObj.ID);
                }
                else
                {
                    this.ShowApplySingleData(this.privDept.ID, inPlanObj.ID, this.BeginTime, this.EndTime);
                    if (inPlanObj.ID == null)
                    {
                        this.ShowApplySumData(this.privDept.ID, this.BeginTime, this.EndTime);
                    }
                }
            }
        }

        private void tvList_DoubleClick(object sender, EventArgs e)
        {
            if (this.tvList.SelectedNode != null && this.tvList.SelectedNode.Parent != null)
            {
                FS.FrameWork.Models.NeuObject inPlanObj = this.tvList.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;

                this.nowBillNO = inPlanObj.ID;

                if (inPlanObj.Memo == "0")
                {
                    this.IsShowList = false;
                }
            }
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            this.Filter();
        }

        private void txtFilter_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.SetFocus(true);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus && keyData == Keys.Enter)
            {
                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColPlanNum)
                {
                    decimal planQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPlanNum].Text);
                    decimal planPrice = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPurchasePrice].Text);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPlanCost].Value = planQty * planPrice;

                    this.SetSum();
                }

                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColPlanNum)
                {
                    if (this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.Rows.Count - 1)
                    {
                        this.neuSpread1_Sheet1.ActiveRowIndex++;
                    }
                    else
                    {
                        if (this.IsShowList)
                        {
                            this.txtFilter.Select();
                            this.txtFilter.SelectAll();
                        }
                        else
                        {
                            this.SetFocus(false);
                        }
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            //-----by yuyun 08-7-26{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            //string itemCode = sv.Cells[activeRow, 0].Text;
            string itemCode = sv.Cells[activeRow, 10].Text;

            FS.HISFC.Models.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(itemCode);
            if (item == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return;
            }

            if (this.AddDrugData(item) == 1)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                this.SetFocus(true);
            }
        }

        private void neuSpread1_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == (int)ColumnSet.ColPlanNum)
            {
                decimal planQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPlanNum].Text);
                decimal planPrice = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPurchasePrice].Text);

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPlanCost].Value = planQty * planPrice;

                this.SetSum();
            }
        }
        //{4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
        private int neuSpread1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1_Sheet1.ActiveColumnIndex].Text = obj.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1_Sheet1.ActiveColumnIndex].Tag = obj.ID;
            return 0;
        }
        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if (this.neuSpread1.ActiveSheet.SheetName == "����")
            //{
            //    this.GetDetail(this.privDept.ID, this.BeginTime, this.EndTime, this.neuSpread1_Sheet1.Cells[e.Row, 11].Text);
            //}
            //this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
        }

        #region ö��

        /// <summary>
        /// ���ڹ���
        /// </summary>
        public enum EnumWindowFunInPlan
        {
            ���ƻ�,
            ���ƻ����
        }

        #endregion

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0511", ref testPrivDept);

            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show("���޴˴��ڲ���Ȩ��");
                return -1;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return -1;
            }

            this.privDept = testPrivDept;
            return 1;
        }

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���  
            /// </summary>
            ColSpecs,
            /// <summary>
            /// �ƻ������  
            /// </summary>
            ColPurchasePrice,
            /// <summary>
            /// �ƻ�����  
            /// </summary>
            ColPlanNum,
            /// <summary>
            /// ��λ  
            /// </summary>
            ColUnit,
            /// <summary>
            /// �ƻ����  
            /// </summary>
            ColPlanCost,
            /// <summary>
            /// ���ƿ��  
            /// </summary>
            ColOwnStockNum,
            /// <summary>
            /// ȫԺ���  
            /// </summary>
            ColAllStockNum,
            /// <summary>
            /// ��������
            /// </summary>
            ColOutTotal,
            /// <summary>
            /// ��������
            /// </summary>
            ColProduce,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ��Ʒ���� 
            /// </summary>
            ColItemNO,
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpellCode,
            /// <summary>
            /// �����
            /// </summary>
            ColWBCode,
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColUserCode,
            /// <summary>
            /// �������
            /// </summary>
            ColStockNum,
            /// <summary>
            /// �ѹ�/δ��//{4D18D170-A7D7-40d0-BA09-D9DB2E20DD79}
            /// </summary>
            ColIsBought
        }

        /// <summary>
        /// ���뵥������
        /// </summary>
        private enum BillTypeEnum
        {
            /// <summary>
            /// ���뵥�б�
            /// </summary>
            ApplyList,
            /// <summary>
            /// �ƻ����б�
            /// </summary>
            PlanList
        }


    }
}
