using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Check
{
    /**
     *  CheckFlag 0 ��ӯ�� 1 ��ӯ 2 �̿�
     * 
     **/
    /// <summary>
    /// [��������: ҩƷ�̵����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸�>
    ///     <ʱ��>2007-07-16</ʱ��>
    ///     <�޸���>Liangjz</�޸���>
    ///     <�޸�����>
    ///             1 ����ȫ�̹���
    ///             2 ��������ʱ,���Ӷ�ͣ��ҩƷ/���Ϊ��ҩƷ�Ĵ���.
    ///     </�޸�����>
    ///     <ʱ��>2007-11-29</ʱ��>
    ///     <�޸���>Liangjz</�޸���>
    ///     <�޸�����>
    ///             1 �Ƿ������̵����Ϊ���ݲ��ſ�泣����ȡ �����ݿ��Ʋ��������ж�                 
    ///     </�޸�����>
    ///     1.ȫ���̵������Ż�by Sunjh 2010-11-4 {00FB5141-3969-4e06-9F53-42005FFBD00F}
    /// </�޸�>
    /// </summary>
    public partial class ucCheckManager : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer,
                                            FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucCheckManager()
        {
            InitializeComponent();
        }

        #region �����

        private DataTable dt = new DataTable();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �̵���־ҵ���{0A34566D-E154-47a4-BCB1-2437CC877F63}
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.CheckLog checkLogManager = new FS.HISFC.BizLogic.Pharmacy.CheckLog();

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private System.Collections.Hashtable hsItem = new Hashtable();

        /// <summary>
        /// �̵�ʵ����Ϣ
        /// </summary>
        private System.Collections.Hashtable hsCheck = new Hashtable();

        /// <summary>
        /// �����̵���Ϣ
        /// {98F0BF7A-5F41-4de3-884F-B38E71B41A8C}
        /// </summary>
        private Hashtable htSpecialCheck = new Hashtable();

        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        private bool isAllowEdit = true;

        /// <summary>
        /// ���̵㵥��
        /// </summary>
        private string newCheckNO = "";

        /// <summary>
        /// ��ǰ�̵㵥��ʼ�̵�ʱ��
        /// 
        /// //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} ���ӿ�ʼ�̵�ʱ���¼
        /// </summary>
        private DateTime currentBillBeginCheckDate;

        /// <summary>
        /// �Ƿ���¼��ʱ����ϵͳ���ʿ��
        /// 
        /// {F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} 
        /// </summary>
        private bool isUpdateFStoreRealTime = false;

        /// <summary>
        /// �Ƿ񴰿��̵�
        /// </summary>
        private bool isWindowCheck = false;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private string nowOperCheckNO = "";

        /// <summary>
        /// �Ƿ������̵�
        /// </summary>
        private bool isBatch = false;

        /// <summary>
        /// ����������̵��ʶλ
        /// {98F0BF7A-5F41-4de3-884F-B38E71B41A8C}
        /// </summary>
        private bool isBatchInitial = false;

        /// <summary>
        /// ��ʷ�̵㵥��ȡ״̬ 0 ���� 1 ��� 2 ���
        /// </summary>
        private string historyListState = "1";

        /// <summary>
        /// �Ƿ񰴻�λ�������̵㵥
        /// </summary>
        private bool isSortByPlaceCode = true;
        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        [Description("������� ���ݲ�ͬҽԺ��������"), Category("����"), DefaultValue("�̵㵥")]
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
        /// �Ƿ�����༭
        /// </summary>
        [Description("�Ƿ�������̵����ݽ����޸ı༭"), Category("����"), DefaultValue(true)]
        public bool IsAllowEdit
        {
            get
            {
                return this.isAllowEdit;
            }
            set
            {
                this.isAllowEdit = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.MinNum].Locked = !value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PackNum].Locked = !value;
            }
        }

        /// <summary>
        /// �Ƿ񴰿��̵� �����̵�ʹ���������湦��
        /// </summary>
        [Description("�Ƿ񴰿��̵� �����̵�ʹ���������湦��"), Category("����"), DefaultValue(false)]
        public bool IsWindowCheck
        {
            get
            {
                return this.isWindowCheck;
            }
            set
            {
                this.isWindowCheck = value;
            }
        }

        /// <summary>
        /// �Ƿ��̵������ڹ��� �����ڹ�����ʾ�б�
        /// </summary>
        [Description("�Ƿ��̵������ڹ��� �����ڹ�����ʾ�б�"), Category("����"), DefaultValue(true)]
        public bool IsCheckManager
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

        /// <summary>
        /// �Ƿ���ʾ�̵㵥�б�
        /// </summary>
        [Description("�Ƿ���ʾ�̵㵥�б�"), Category("����"), DefaultValue(false)]
        public bool IsShowCheckList
        {
            get
            {
                return this.ucDrugList1.ShowTreeView;
            }
            set
            {
                this.ucDrugList1.ShowTreeView = value;

                this.SetToolButton(value);
            }
        }

        /// <summary>
        /// ��ʷ�̵㵥��ȡʱ�Ƿ�ֻȡ���״̬ 
        /// </summary>
        [Description("��ʷ�̵㵥��ȡʱ�Ƿ�ֻȡ���״̬ True ȡ���״̬ False ȡ���״̬"), Category("����"), DefaultValue(true)]
        public bool IsHistoryCStoreState
        {
            get
            {
                if (this.historyListState == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.historyListState = "1";
                else
                    this.historyListState = "2";
            }
        }

        /// <summary>
        /// �Ƿ񰴻�λ�������̵㵥
        /// </summary>
        [Description("�Ƿ񰴻�λ�������̵㵥������ΪTrue���ջ�λ����������ΪFalse���շ����Ⱥ�˳������"), Category("����"), DefaultValue(true)]
        public bool IsSortByPlaceCode
        {
            get
            {
                return this.isSortByPlaceCode;
            }
            set
            {
                this.isSortByPlaceCode = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��    ��", "��¼��ǰ����γ��̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolBarService.AddToolButton("��������", "��������γ��̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);
            toolBarService.AddToolButton("�̵�ģ��", "����ģ���γ��̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.Z����, true, false, null);
            toolBarService.AddToolButton("��ʷ�̵�", "������ʷ�̵��¼ ", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            toolBarService.AddToolButton("�̵㸽��", "����̵㸽��ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.J����, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "��ʾ�̵㵥�б�", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("��������", "���������̵���Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null);
            toolBarService.AddToolButton("ɾ    ��", "ɾ����ǰѡ��ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("ȫ    ��", "���ݵ�ǰ���ʿ������̵���", FS.FrameWork.WinForms.Classes.EnumImageList.J���, true, false, null);
            //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} ���ӿ�ʼ�̵�ʱ���¼
            toolBarService.AddToolButton( "��ʼ�̵�", "��¼��ʼ�ֹ��̵�ҩƷʱ���", FS.FrameWork.WinForms.Classes.EnumImageList.K����, true, false, null );

            toolBarService.AddToolButton("���", "�����̵�����µ�ǰ��� ����ӯ��", FS.FrameWork.WinForms.Classes.EnumImageList.P�̵�����, true, false, null);
            toolBarService.AddToolButton("���", "���ϵ�ǰ�̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} ���ӿ�ʼ�̵�ʱ���¼
            if (e.ClickedItem.Text == "��ʼ�̵�")
            {
                this.RecordCheckTime();
            }
            if (e.ClickedItem.Text == "���")
            {
                this.CheckCStore(this.privDept.ID,this.nowOperCheckNO);

                this.ShowCheckList();
            }
            if (e.ClickedItem.Text == "���")
            {
                this.CancelCheck(this.privDept.ID, this.nowOperCheckNO);

                this.ShowCheckList();
            }
            if (e.ClickedItem.Text == "ɾ    ��")
            {
                this.DeleteData();
            }
            if (e.ClickedItem.Text == "ȫ    ��")
            {
                this.FstoreSetAStore();
            }
            if (e.ClickedItem.Text == "��������")
            {
                this.AddSave(this.privDept.ID, this.nowOperCheckNO);

                if (this.tvList.Nodes.Count > 0)
                    this.tvList.SelectedNode = this.tvList.Nodes[0];
            }
            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.Clear();

                if (!this.IsShowCheckList)
                {
                    this.IsShowCheckList = true;
                }
            }
            if (e.ClickedItem.Text == "��    ��")
            {
                this.CheckClose();
            }
            if (e.ClickedItem.Text == "�̵㸽��")
            {
                this.GroupCheckAdd();
            }
            if (e.ClickedItem.Text == "��ʷ�̵�")
            {
                this.GroupCheckCloseHistory();
            }
            if (e.ClickedItem.Text == "��������")
            {
                this.GroupCheckCloseType();
            }
            if (e.ClickedItem.Text == "�̵�ģ��")
            {
                this.GroupCheckCloseStencil();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save(this.privDept.ID, this.nowOperCheckNO) != 1)
                return -1;

            if (!this.IsShowCheckList)
            {
                this.ShowCheckList();

                this.IsShowCheckList = true;
            }

            if (this.tvList.Nodes.Count > 0)
            {
                this.tvList.SelectedNode = this.tvList.Nodes[0];
            }

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (Function.IPrint == null)
            {
                Function.IPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint;
            }

            if (Function.IPrint != null)
            {
                Function.IPrint.SetData(this.GetAllData(), FS.HISFC.BizProcess.Interface.Pharmacy.BillType.Check);

                Function.IPrint.Print();
            }
            return 1;
        }

        public override int SetPrint(object sender, object neuObject)
        {
            return 1;
        }

        /// <summary>
        /// ���ù�������ť״̬
        /// </summary>
        /// <param name="isShowList">�Ƿ���ʾ�̵㵥�б�</param>
        protected void SetToolButton(bool isShowList)
        {
            this.toolBarService.SetToolButtonEnabled("�� �� ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("�̵㸽��", isShowList);
            this.toolBarService.SetToolButtonEnabled("��    ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("��������", isShowList);
            this.toolBarService.SetToolButtonEnabled("ɾ    ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��������", !isShowList);
            this.toolBarService.SetToolButtonEnabled("�̵�ģ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("���ͷ���", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��ʷ�̵�", !isShowList);

        }

        #endregion

        #region �̵��б��ʼ��

        /// <summary>
        /// �̵㵥�б������
        /// </summary>
        private tvCheckList tvList = null;

        /// <summary>
        /// �̵㵥�б��ʼ��
        /// </summary>
        protected void InitCheckList()
        {
            this.tvList = new tvCheckList();
            this.ucDrugList1.TreeView = this.tvList;

            this.tvList.AfterSelect -= new TreeViewEventHandler(tvList_AfterSelect);
            this.tvList.AfterSelect += new TreeViewEventHandler(tvList_AfterSelect);

            this.ucDrugList1.Caption = "�̵㵥�б�";

            this.ShowCheckList();

            this.ucDrugList1.ShowTreeView = true;
        }

        /// <summary>
        /// �̵㵥�б���ʾ
        /// </summary>
        private void ShowCheckList()
        {
            FS.FrameWork.Models.NeuObject operObj = new FS.FrameWork.Models.NeuObject();
            operObj.ID = "ALL";
            operObj.Name = "������Ա";

            this.tvList.ShowCheckList(this.privDept, "0", operObj);
        }

        #endregion

        #region ��ʼ��DataTable��Fp����

        /// <summary>
        /// DataTable��ʼ��
        /// </summary>
        private void InitDataTable()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            //��myDataTable�������     //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
            this.dt.Columns.AddRange(new DataColumn[] {
                                                                        new DataColumn("��λ��",      dtStr),
                                                                        new DataColumn("�Զ�����",	  dtStr),
                                                                        new DataColumn("��Ʒ����",	  dtStr),
                                                                        new DataColumn("���",        dtStr),
                                                                        new DataColumn("��װ����",    dtDec),
                                                                        new DataColumn("����",		  dtStr),
                                                                        new DataColumn("��Ч��",	  dtStr),
                                                                        new DataColumn("���ۼ�",      dtDec),
                                                                        new DataColumn("�̵�����1",	  dtDec),
                                                                        new DataColumn("��װ��λ",    dtStr),
                                                                        new DataColumn("�̵�����2",	  dtDec),
                                                                        new DataColumn("��С��λ",	  dtStr),
                                                                        new DataColumn("�̵���",    dtDec),
                                                                        new DataColumn("���ʿ��",    dtDec),
                                                                        new DataColumn("��λ",		  dtStr),
                                                                        new DataColumn("�̵���",	  dtDec),
                                                                        new DataColumn("ӯ������",	  dtDec),
                                                                        new DataColumn("ӯ�����",	  dtDec),
                                                                        new DataColumn("��ע",        dtStr),
                                                                        new DataColumn("ӯ�����",    dtDec),
                                                                        new DataColumn("�Ƿ񸽼�",	  dtBol),
                                                                        new DataColumn("��ˮ��",      dtStr),
                                                                        new DataColumn("ҩƷ����",	  dtStr),
                                                                        new DataColumn("ƴ����",      dtStr),
                                                                        new DataColumn("�����",      dtStr),
                                                                        new DataColumn("ͨ����ƴ����",dtStr),
                                                                        new DataColumn("ͨ���������",dtStr),
                                                                    });
            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;
            this.dt.CaseSensitive = true;

            //�趨���ڶ�DataView�����ظ��м���������
            DataColumn[] keys = new DataColumn[3];
            keys[0] = this.dt.Columns["ҩƷ����"];
            keys[1] = this.dt.Columns["��λ��"];
            keys[2] = this.dt.Columns["����"];
            this.dt.PrimaryKey = keys;

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;

            this.SetFormat();
        }

        /// <summary>
        /// ��ʽ��FarPoint
        /// </summary>
        public void SetFormat()
        {
            float unitWidth = 36F;
            float numWidth = 70F;

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numberCellType1 = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            numberCellType1.MinimumValue = 0;

            //���λس���
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            //ʹֱ��ѡ�оͿ��Ա༭
            FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            t.ReadOnly = true;

            this.neuSpread1_Sheet1.DefaultStyle.CellType = t;

            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 40F;

            if (this.isAllowEdit)
            {
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PackNum].CellType = numberCellType1;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.MinNum].CellType = numberCellType1;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PackNum].CellType = numberCellType1;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.MinNum].CellType = numberCellType1;
            }

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PlaceNO].Width = 50F;                 //��λ��
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.UserCode].Width = 60F;                //�Զ�����
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.TradeName].Width = 130F;              //��Ʒ����
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Specs].Width = 70F;                   //���
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.RetailPrice].Width = 60F;          
            
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PackNum].Width = numWidth;            //�̵�����1
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PackUnit].Width = unitWidth;          //��װ��λ
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.MinNum].Width = numWidth;             //�̵�����2
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.MinUnit].Width = unitWidth;           //��С��λ

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckQty].Width = numWidth;           //�̵���
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.FStoreQty].Width = numWidth;          //���ʿ��
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckUnit].Width = unitWidth;         //�̵��浥λ
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckCost].Width = numWidth;          //�̵���
            //   //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckCount].Width = numWidth;          //ӯ������   
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckCost1].Width = numWidth;          //ӯ�����

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.BatchNO].Visible = this.isBatch;             //����
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ValidDate].Visible = false;           //��Ч��

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.IsAdd].Visible = false;               //�Ƿ񸽼�

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckNO].Visible = false;			    //��ˮ��
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.DrugNO].Visible = false;			    //ҩƷ����            
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.RegularSpell].Visible = false;        //ͨ����ƴ����
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.RegularWB].Visible = false;           //ͨ���������
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.WBCode].Visible = false;              //�����
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.SpellCode].Visible = false;           //ƴ����

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.MinNum].BackColor = System.Drawing.Color.SeaShell;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PackNum].BackColor = System.Drawing.Color.SeaShell;

            //���ÿ�������
            this.neuSpread1_Sheet1.SetColumnAllowAutoSort((int)ColumnSet.CheckFlag, true);

            this.SetFlag();
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void InitData()
        {
            List<FS.HISFC.Models.Pharmacy.Item> alItem = this.itemManager.QueryItemList();
            if (alItem == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ������Ϣ��������"));
                return;
            }
            foreach (FS.HISFC.Models.Pharmacy.Item info in alItem)
            {
                this.hsItem.Add(info.ID, info);
            }


            FS.FrameWork.Management.ControlParam ctrlMagager = new ControlParam();

			string ctrlStr = ctrlMagager.QueryControlerInfo("510001");
			if (ctrlStr == "1")
                this.isBatch = true;
			else
				this.isBatch = false;

            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //��ÿⷿ���Ʋ������ж϶Ըÿⷿ�Ƿ����Ź���
            this.isBatch = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Check_With_Batch, true, false);
            
            //�Ƿ��̵����Ϊ���ݲ��ſ��������û�ȡ
            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.DeptConstant deptConst = consManager.QueryDeptConstant(this.privDept.ID);
            this.isBatch = deptConst.IsBatch;
            //{98F0BF7A-5F41-4de3-884F-B38E71B41A8C}���ӿ��԰�ҩƷ����ά��ҩƷ�Ƿ������̵�,����ԭʼ��ʶλ
            this.isBatchInitial = this.isBatch;
            //��ʷ�̵㵥��ȡʱ�Ƿ�ֻȡ���״̬
            this.IsHistoryCStoreState = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Check_History_State, true, true);

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.BatchNO].Visible = this.isBatch;             //����

            //���԰�ҩƷ����ά��ҩƷ�Ƿ������̵�{98F0BF7A-5F41-4de3-884F-B38E71B41A8C}
            //this.ucDrugList1.ShowDeptStorage(this.privDept.ID, this.isBatch, 0);
            this.ucDrugList1.ShowDeptStorageWithSpecialCheck(this.privDept.ID, this.isBatch, 0);
        }

        /// <summary>
        /// ����Fp���
        /// </summary>
        protected void SetFlag()
        {
            try
            {
                //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ��ɽ�����һ������
                if (NConvert.ToDecimal(this.neuLbTotalCost.Text) < 0)
                {
                    this.neuLbTotalCostSign.ForeColor = System.Drawing.Color.Red;
                }
                else if (NConvert.ToDecimal(this.neuLbTotalCost.Text) > 0)
                {
                    this.neuLbTotalCostSign.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    this.neuLbTotalCostSign.ForeColor = System.Drawing.Color.Black;
                }

                if (NConvert.ToDecimal(this.neuLbWinCost.Text) < 0)
                {
                    this.neuLbWinCostSign.ForeColor = System.Drawing.Color.Red;
                }
                else if (NConvert.ToDecimal(this.neuLbWinCost.Text) > 0)
                {
                    this.neuLbWinCostSign.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    this.neuLbWinCostSign.ForeColor = System.Drawing.Color.Black;
                }

                if (NConvert.ToDecimal(this.neuLbLoseCost.Text) < 0)
                {
                    this.neuLbLoseCostSign.ForeColor = System.Drawing.Color.Red;
                }
                else if (NConvert.ToDecimal(this.neuLbLoseCost.Text) > 0)
                {
                    this.neuLbLoseCostSign.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    this.neuLbLoseCostSign.ForeColor = System.Drawing.Color.Black;
                }

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.CheckQty].Text) > NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.FStoreQty].Text))
                    {
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Blue;
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.CheckFlag].Text = "1";
                    }
                    else if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.CheckQty].Text) < NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.FStoreQty].Text))
                    {
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.CheckFlag].Text = "2";
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Black;
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.CheckFlag].Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("��ʽ��Fpӯ����ɫ��ʾʱ��������" + ex.Message));
                return;
            }
        }

        /// <summary>
        /// �����ݱ�����������
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        protected int AddDataToTable(FS.HISFC.Models.Pharmacy.Check check)
        {
            string judgeCheckID = "-1";
            if (check.ID != null && check.ID != "")
            {
                judgeCheckID = check.ID;
            }
            else   //����Ч�̵㵥���ý����ж�
            {
                if (!check.IsAdd)
                {
                    if (this.itemManager.JudgeCheckState(check.Item.ID, this.privDept.ID, "0", judgeCheckID))
                    {
                        MessageBox.Show(check.Item.Name + Language.Msg("  �Դ���δ��������̵㵥 ���ܼ������з����̵�"));
                        return -1;
                    }
                }
            }

            try
            {
                decimal checkFlag = 0;
                if (check.AdjustQty > check.FStoreQty)      //��ӯ
                {
                    checkFlag = 1;
                }
                else if (check.AdjustQty < check.FStoreQty) //�̿�
                {
                    checkFlag = 2;
                }
                if (check.PlaceNO == "" || check.PlaceNO == null)
                {
                    check.PlaceNO = "0";
                }
                //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
                decimal winLosCost = System.Math.Round((check.AdjustQty - check.FStoreQty) / check.Item.PackQty * check.Item.PriceCollection.RetailPrice, 2);

                this.dt.Rows.Add(new object[] { 
                                                check.PlaceNO,                              //��λ��
                                                check.Item.NameCollection.UserCode,         //�Զ�����
                                                check.Item.NameCollection.Name,             //��Ʒ����
                                                check.Item.Specs,                           //���
                                                check.Item.PackQty,                         //��װ����
                                                check.BatchNO,                              //����
                                                check.ValidTime,                            //��Ч��
                                                check.Item.PriceCollection.RetailPrice,     //���ۼ�
                                                check.PackQty,                              //�̵�����1
                                                check.Item.PackUnit,                        //��װ��λ
                                                check.MinQty,                               //�̵�����2
                                                check.Item.MinUnit,                         //��С��λ
                                                check.AdjustQty,                            //�̵���
                                                check.FStoreQty,                            //���ʿ��
                                                check.Item.MinUnit,                         //��λ
                                                System.Math.Round(check.AdjustQty / check.Item.PackQty * check.Item.PriceCollection.RetailPrice,2),      //�̵���                                           
                                                 check.AdjustQty-check.FStoreQty,//ӯ������
                                                winLosCost,

                                                check.Memo,
                                                checkFlag,
                                                check.IsAdd,
                                                check.ID,
                                                check.Item.ID,
                                                check.Item.NameCollection.SpellCode,
                                                check.Item.NameCollection.WBCode,
                                                check.Item.NameCollection.RegularSpell.SpellCode,
                                                check.Item.NameCollection.RegularSpell.WBCode
                                           });
                //  {B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
                //  ��ɽ�����WinLoseCost()
                this.WinLoseCost(winLosCost);
            }
            catch (System.Data.ConstraintException eCons)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ҩƷ�Ѵ��� �����ظ����"));
                return -1;
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + e.Message));

                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + ex.Message));

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// {B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ��ɽ��
        /// �ô˺���ʵ��ӯ���ܶ��ӯ���̿��ܶ�
        /// </summary>
        private void WinLoseCost(decimal count)
        {
            this.neuLbTotalCost.Text = (NConvert.ToDecimal(this.neuLbTotalCost.Text) + count).ToString();
            if (count > 0)
            {
                this.neuLbWinCost.Text = (NConvert.ToDecimal(this.neuLbWinCost.Text) + count).ToString();
            }
            else
            {
                this.neuLbLoseCost.Text = (NConvert.ToDecimal(this.neuLbLoseCost.Text) + count).ToString();
            }
        }

        /// <summary>
        /// ��DataTable�ڻ�ȡ��Ŀ��Ϣ
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected FS.HISFC.Models.Pharmacy.Check GetDataFromTable(DataRow dr)
        {
            string key = dr["ҩƷ����"].ToString() + dr["����"].ToString() + dr["��λ��"].ToString();
            if (this.hsCheck.Contains(key))
            {
                FS.HISFC.Models.Pharmacy.Check check = this.hsCheck[key] as FS.HISFC.Models.Pharmacy.Check;

                check.PackQty = NConvert.ToDecimal(dr["�̵�����1"]);
                check.MinQty = NConvert.ToDecimal(dr["�̵�����2"]);

                check.AdjustQty = check.PackQty * check.Item.PackQty + check.MinQty;
                check.Memo = dr["��ע"].ToString();

                return check;
            }

            return null;
        }

        /// <summary>
        /// {98F0BF7A-5F41-4de3-884F-B38E71B41A8C}
        /// ��ʼ�������̵��ϣ��
        /// </summary>
        private void InitSpecialCheck()
        {
            List<FS.HISFC.Models.Pharmacy.CheckSpecial> spList = this.itemManager.QueryCheckSpecial(this.privDept.ID);
            if (spList == null)
            {
                MessageBox.Show("���������̵��¼ʧ�ܣ�" + this.itemManager.Err);
                return;
            }
            foreach (FS.HISFC.Models.Pharmacy.CheckSpecial special in spList)
            {
                this.htSpecialCheck.Add(special.DrugQuality.ID, special);
            }
        }

        #endregion

        #region �������� / �̵㸽��

        /// <summary>
        /// �ж��Ƿ���Լ���������������
        /// </summary>
        /// <returns>�������</returns>
        public bool JudgeContinue(string msg)
        {
            if (this.neuSpread1_Sheet1.Rows.Count != 0)
            {
                DialogResult result;
                //��ʾ�û�ѡ���Ƿ�������ɣ���������������ԭ����
                result = MessageBox.Show(Language.Msg(msg + " �������ǰ���ݣ��Ƿ����"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                if (result == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected virtual void GroupCheckCloseType()
        {
            //�ж��Ƿ����������������
            if (!this.JudgeContinue("��������"))
            {
                return;
            }

            this.Clear();

            ////����ѡ��ҩƷ���ҩƷ���ʴ���
            HISFC.Components.Pharmacy.Check.ucTypeOrQualityChoose uc = new ucTypeOrQualityChoose( true );
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

            switch (uc.ResultFlag)
            {
                case "0":           //ȡ��
                    break;
                case "1":           //ҩƷ���/ҩƷ����
                    this.CheckCloseByType(this.privDept.ID, uc.DrugType, uc.DrugQuality, this.isBatch,uc.IsCheckZeroStock,uc.IsCheckStopDrug);
                    break;
                case "2":           //ȫ��ҩƷ����
                    this.CheckCloseByTotal(this.privDept.ID, this.isBatch,uc.IsCheckZeroStock,uc.IsCheckStopDrug);
                    break;
            }
        }

        /// <summary>
        /// �̵�ģ����������
        /// </summary>
        protected virtual void GroupCheckCloseStencil()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("ʹ��ģ�潫�����ǰ������ �Ƿ����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return;
            }

            this.Clear();

            ArrayList alStencilDetail = Function.ChooseDrugStencil(this.privDept.ID, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Check);
            if (alStencilDetail != null && alStencilDetail.Count > 0)
            {
                System.Collections.Hashtable hsDrugStencil = new Hashtable();
                foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in alStencilDetail)
                {
                    hsDrugStencil.Add(info.Item.ID, null);
                }

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڸ�����ѡ�̵�ģ����з��ʴ���...");
                Application.DoEvents();

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
                
                //�˴�ȡ���Ļ�λ��������
                ArrayList alItem = this.itemManager.QueryStorageList(this.privDept.ID, this.isBatch);

                foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
                {
                    if (hsDrugStencil.ContainsKey(item.ID))
                    {
                        this.AddCheckData(this.privDept.ID, item.ID, item.User01, item.User02, this.isBatch);
                    }
                }

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }
        
        /// <summary>
        /// ������ʷ�̵����� ֻȡ������̵㵥
        /// </summary>
        protected virtual void GroupCheckCloseHistory()
        {
            if (!this.JudgeContinue("������ʷ�̵㵥"))
            {
                return;
            }

            this.Clear();

            List<FS.HISFC.Models.Pharmacy.Check> alList = this.itemManager.QueryCheckList(this.privDept.ID,this.historyListState, "ALL");
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�̵㵥�б�������" + this.itemManager.Err));
                return;
            }

            foreach (FS.HISFC.Models.Pharmacy.Check check in alList)
            {
                //��÷�����Ա����
                FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(check.FOper.ID);
                if (employee == null)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg("��÷�����Ա��Ϣʱ������Ա����Ϊ" + check.FOper.ID + "����Ա������"));
                    return;
                }
                check.FOper.Name = employee.Name;

                check.User01 = check.CheckNO;

                if (check.CheckName == "")          //�̵㵥��/�̵㵥����
                    check.ID = check.CheckNO;
                else
                    check.ID = check.CheckName;
                check.Name = employee.Name;         //������
                
            }

            FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
            string[] label = { "���ݺ�", "������" };
            float[] width = { 120F, 100F };
            bool[] visible = { true, true, false, false, false, false };
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(alList.ToArray()), ref selectObj) == 0)
            {
                return;
            }
            else
            {
                ArrayList alDetail = this.itemManager.QueryCheckDetailByCheckCode(this.privDept.ID, selectObj.User01);
                if (alDetail == null)
                {
                    MessageBox.Show(Language.Msg("�����̵㵥��ȡ�̵���ϸ�б�������"));
                    return;
                }

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڸ�����ѡ�̵㵥���з��ʴ���..."));
                Application.DoEvents();

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();

                int i = 1;
                foreach (FS.HISFC.Models.Pharmacy.Check checkInfo in alDetail)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, alDetail.Count);
                    Application.DoEvents();

                    this.AddCheckData(this.privDept.ID, checkInfo.Item.ID, checkInfo.BatchNO, checkInfo.PlaceNO,this.isBatch);
                }

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

        }

        /// <summary>
        /// �̵㸽��
        /// </summary>
        protected virtual void GroupCheckAdd()
        {
            //��ǰ�б��ڵ����Ч���̵㵥
            if (this.tvList.SelectedNode == null || this.tvList.SelectedNode.Parent == null)
                return;

            if (this.dt.Rows.Count <= 0)
            {
                MessageBox.Show(Language.Msg("���ȶ���ӦҩƷ���з��ʴ���\n�޷�ֱ�ӽ��̵㸽��ҩƷ�����̵㵥"));
                return;
            }

            HISFC.Components.Pharmacy.Check.ucCheckAdd uc = new ucCheckAdd();

            uc.IsShowAddCheckBox = true;
            uc.IsShowButton = false;            

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

            if (uc.Result == DialogResult.OK)
            {
                this.AddCheckAdd(uc.ChooseData);
            }
            else
            {
                return;
            }

        }

        #endregion

        #region ���ʷ���

        /// <summary>
        /// ����
        /// </summary>
        protected void CheckClose()
        {
            this.Clear();

            this.IsShowCheckList = false;

            this.ucDrugList1.SetFocusSelect();
        }

        /// <summary>
        /// �ֹ����ҩƷ���ʼ�¼
        /// </summary>
        /// <param name="deptNO"></param>
        /// <param name="drugNO"></param>
        /// <param name="batchNO"></param>
        /// <param name="placeNO"></param>
        /// <param name="isBatch"></param>
        protected int AddCheckData(string deptNO, string drugNO, string batchNO, string placeNO, bool isBatch)
        {
            string key = drugNO + batchNO + placeNO;
            if (this.hsCheck.ContainsKey(key))
            {
                MessageBox.Show(Language.Msg("���ҩƷ�Ѵ���,�����ظ����"));
                return -1;
            }

            FS.HISFC.Models.Pharmacy.Check check = this.itemManager.CheckCloseByDrug(deptNO, drugNO, batchNO, isBatch);
            if (check == null)
            {
                MessageBox.Show(Language.Msg("���ҩƷ����ʧ�� " + this.itemManager.Err));
                return -1;
            }

            if (this.AddDataToTable(check) == 1)
            {
                //��λ���Է��ʻ�ȡ��Ϊ׼
                this.hsCheck.Add(drugNO + batchNO + check.PlaceNO, check);
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                return 1;
            }

            return -1;
        }

        /// <summary>
        /// ����ҩƷ�������ֱ�ӽ����̵����
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="drugType">ҩƷ���</param>
        /// <param name="drugQuality">ҩƷ����</param>
        /// <param name="isBatch">�Ƿ����Ź���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1������������0</returns>
        protected int CheckCloseByType(string deptNO, string drugType, string drugQuality, bool isBatch,bool isCheckZeroStock,bool isCheckStopDrug)
        {
            //���ԭ����
            this.Clear();

            try
            {	//����ҩƷ���ҩƷ���ʽ��з���
                ArrayList alDetail = this.itemManager.CheckCloseByTypeQuality(deptNO, drugType, drugQuality, isBatch,isCheckZeroStock,isCheckStopDrug);
                if (alDetail == null)
                {
                    MessageBox.Show(Language.Msg("����ҩƷ���/���ʽ�����������ʧ��" + this.itemManager.Err));
                    return -1;
                }

                if (alDetail.Count == 0)
                {
                    MessageBox.Show(Language.Msg("��ѡ�������޿��ҩƷ" + this.itemManager.Err));
                    return -1;
                }

                this.ShowCheckDetail(alDetail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �Ա��ⷿ����ҩƷ���з��ʴ���
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="isBatch">�Ƿ����Ź���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int CheckCloseByTotal(string deptNO, bool isBatch,bool isCheckZeroStock,bool isCheckStopDrug)
        {
            //���ԭ����
            this.Clear();

            try
            {		
                //������ҩƷ���з��ʴ���
                ArrayList alDetail = this.itemManager.CheckCloseByTotal(deptNO, isBatch,isCheckZeroStock,isCheckStopDrug);
                if (alDetail == null)
                {
                    MessageBox.Show(Language.Msg("�Ա��������п��ҩƷ�����������ʴ���ʧ��" + this.itemManager.Err));
                    return -1;
                }
                if (alDetail.Count == 0)
                {
                    MessageBox.Show(Language.Msg("��ѡ�������޿��ҩƷ" + this.itemManager.Err));
                    return -1;
                }

                //��FarPoint����ʾ��ϸ
                this.ShowCheckDetail(alDetail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("����ʧ�ܣ�" + ex.Message));
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �����̵㸽����Ϣ
        /// </summary>
        /// <param name="alAddDetail"></param>
        /// <returns></returns>
        protected int AddCheckAdd(List<FS.HISFC.Models.Pharmacy.Check> alAddDetail)
        {
            string errStr = "";
            foreach (FS.HISFC.Models.Pharmacy.Check check in alAddDetail)
            {
                if (this.hsCheck.ContainsKey(check.Item.ID + check.BatchNO + check.PlaceNO))
                {
                    if (errStr == "")
                        errStr = "�����ظ�ֵ ���Զ�����";
                    errStr = errStr + check.Item.Name;
                    continue;
                }

                bool isHave = false;
                foreach (DataRow dr in this.dt.Rows)
                {
                    if (dr["ҩƷ����"].ToString() == check.Item.ID)
                    {
                        isHave = true;
                        break;
                    }
                }
                check.Item = this.itemManager.GetItem(check.Item.ID);
                if (check.Item == null)
                {
                    MessageBox.Show(Language.Msg("����ҩƷ������Ϣʱ����" + this.itemManager.Err));
                    return -1;
                }

                if (!isHave)
                {
                    MessageBox.Show(Language.Msg("���ȶ� " + check.Item.Name + " �����̵����\n�޷�ֱ�ӽ�ҩƷ�����̵㵥"));
                    return 0;
                }
               
                if (this.AddDataToTable(check) == 1)
                {
                    this.hsCheck.Add(check.Item.ID + check.BatchNO + check.PlaceNO, check);
                }
                else
                {
                    return -1;
                }
            }

            if (errStr != "")
            {
                MessageBox.Show(Language.Msg(errStr));
            }

            return 1;
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʾ�̵���ϸ��Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="checkCode">�̵㵥��</param>
        public void ShowCheckDetail(string deptNO, string checkNO)
        {
            ArrayList alDetail = new ArrayList();

            alDetail = this.itemManager.QueryCheckDetailByCheckCode(deptNO, checkNO);
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return;
            }

            if (!this.isSortByPlaceCode)
            {
                NoSort noSort = new NoSort();

                alDetail.Sort(noSort);
            }

            this.ShowCheckDetail(alDetail);

            //�ύ�仯 ���ݵ��ż���ʱ �������ݶ����ѱ������ ��δ�����޸�
            this.dt.AcceptChanges();
        }

        /// <summary>
        /// ���FarPoint
        /// </summary>
        /// <param name="alDetail">check��̬����</param>
        public int ShowCheckDetail(ArrayList alDetail)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ����̵���ϸ��Ϣ..."));
            Application.DoEvents();

            try
            {
                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();

                int i = 1;
                foreach (FS.HISFC.Models.Pharmacy.Check check in alDetail)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, alDetail.Count);
                    Application.DoEvents();

                    #region ��ȡҩƷ������Ϣ

                    if (this.hsItem.ContainsKey(check.Item.ID))
                    {
                        check.Item = this.hsItem[check.Item.ID] as FS.HISFC.Models.Pharmacy.Item;

                        //ȫ���̵������Ż� by Sunjh 2010-11-4 {00FB5141-3969-4e06-9F53-42005FFBD00F}
                        if (check.Item.IsStop)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        MessageBox.Show(check.Item.ID);
                        check.Item = this.itemManager.GetItem(check.Item.ID);
                        if (check.Item == null)
                        {
                            Function.ShowMsg("����ҩƷ������Ϣʱ����" + this.itemManager.Err);
                            return -1;
                        }
                    }

                    #endregion

                    if (this.isWindowCheck)
                    {
                        check.MinQty = 0;               //[�̵���С����
                        check.PackQty = 0;              //[�̵��װ����
                    }

                    if (this.AddDataToTable(check) == -1)
                    {
                        return -1;
                    }

                    this.hsCheck.Add(check.Item.ID + check.BatchNO + check.PlaceNO, check);
                }
            }
            catch (Exception ex)
            {
                Function.ShowMsg(ex.Message);
                return -1;
            }
            finally
            {
                this.SetFlag();

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            return 1;
        }

        /// <summary>
        /// ����б�  
        /// </summary>
        public void Clear()
        {
            this.dt.Rows.Clear();
            this.dt.AcceptChanges();

            this.hsCheck.Clear();

            this.nowOperCheckNO = "";
            this.newCheckNO = "";

            this.txtFilter.Text = "";

            this.neuLbTotalCost.Text = "0";
            this.neuLbWinCost.Text = "0";
            this.neuLbLoseCost.Text = "0";
        }

        /// <summary>
        /// ɾ��һ�����ʼ�¼
        /// </summary>
        public void DeleteData()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0) 
                return;

            DialogResult result;
            //��ʾ�û��Ƿ�ȷ��ɾ��
            result = MessageBox.Show(Language.Msg("ȷ��ɾ����ǰ��¼?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);

            if (result == DialogResult.No)
            {
                return;
            }

            this.neuSpread1.StopCellEditing();

            int iRemove = this.neuSpread1_Sheet1.ActiveRowIndex;

            string key = this.neuSpread1_Sheet1.Cells[iRemove, (int)ColumnSet.DrugNO].Text +
                this.neuSpread1_Sheet1.Cells[iRemove, (int)ColumnSet.BatchNO].Text +
                this.neuSpread1_Sheet1.Cells[iRemove, (int)ColumnSet.PlaceNO].Text;

            //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
            //��ɽ�µ�ɾ��һ��ʱ����ӯ������е���ֵ����ȥ
            if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[iRemove, (int)ColumnSet.CheckCost1].Text) > 0)
            {
                this.neuLbWinCost.Text = (NConvert.ToDecimal(this.neuLbWinCost.Text) - NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[iRemove, (int)ColumnSet.CheckCost1].Text)).ToString();
            }
            else
            {
                this.neuLbLoseCost.Text = (NConvert.ToDecimal(this.neuLbLoseCost.Text) - NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[iRemove, (int)ColumnSet.CheckCost1].Text)).ToString();
            }
            this.neuLbTotalCost.Text = (NConvert.ToDecimal(this.neuLbTotalCost.Text) - NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[iRemove, (int)ColumnSet.CheckCost1].Text)).ToString();

            if (this.hsCheck.ContainsKey(key))
                this.hsCheck.Remove(key);
            
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);

            this.neuSpread1.StartCellEditing(null, false);
        }

        /// <summary>
        /// ��ȡ�����̵����ݴ�ӡ
        /// </summary>
        /// <returns></returns>
        private ArrayList GetAllData()
        {
            ArrayList alDetail = new ArrayList();

            this.dt.DefaultView.RowFilter = "1=1";
            foreach (DataRow dr in this.dt.Rows)
            {
                FS.HISFC.Models.Pharmacy.Check check = this.GetDataFromTable(dr);
                if (check == null)
                {
                    MessageBox.Show(Language.Msg("�����ݱ��ڻ�ȡCheckʵ��ʧ��"));
                    return null;
                }

                check.StockDept = this.privDept;        //����

                alDetail.Add(check);
            }

            return alDetail;
        }

        /// <summary>
        /// ��ȡ��ǰ�����仯�����ݣ�����Check��̬����
        /// </summary>
        /// <param name="flag">������־Modify(���ӡ�����)��Del(ɾ��)</param>
        /// <returns>�ɹ����ط����䶯�����顢ʧ�ܷ���null</returns>
        private ArrayList GetModify(string flag)
        {
            this.dt.DefaultView.RowFilter = "1=1";
            for (int i = 0; i < this.dt.DefaultView.Count; i++)
            {
                this.dt.DefaultView[i].EndEdit();
            }

            DataTable dtChange = new DataTable();
            switch (flag)
            {
                case "Modify":		//��ȡ�䶯(���ӡ��޸�)����
                    dtChange = this.dt.GetChanges(DataRowState.Added | DataRowState.Modified);
                    break;
                case "Del":			//��ȡɾ������
                    dtChange = this.dt.GetChanges(DataRowState.Deleted);
                    break;
                default:
                    return null;
            }
            //�ޱ䶯����
            if (dtChange == null)
                return null;

            ArrayList alDetail = new ArrayList();

            //����ɾ�����ݣ��ع��仯
            if (flag == "Del")
            {
                dtChange.RejectChanges();
            }

            string errDrug = "";
            try
            {
                //������ݼ���Check�̵�ʵ��
                foreach (DataRow dr in dtChange.Rows)
                {
                    errDrug = dr["ҩƷ����"].ToString() + dr["��Ʒ����"].ToString();

                    if (flag == "Del" && dr["��ˮ��"].ToString() == "")
                        return null;

                    FS.HISFC.Models.Pharmacy.Check check = this.GetDataFromTable(dr);

                    if (check == null)
                    {
                        MessageBox.Show(Language.Msg("�����ݱ��ڻ�ȡCheckʵ��ʧ��"));
                        return null;
                    }

                    check.StockDept = this.privDept;        //����

                    alDetail.Add(check);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message + "\n " + errDrug));
                return null;
            }

            return alDetail;
        }

        /// <summary>
        ///�Է��ʡ��̵���̽��б��棬�����̵���ϸ��
        /// </summary>
        /// <param name="deptNO">�ⷿ����</param>
        /// <param name="checkNO">�̵㵥��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int Save(string deptNO, string checkNO)
        {
            if (this.dt.Rows.Count <= 0)
            {
                return 0;
            }

            if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
            {
                this.SumCheckNumAndCost( this.neuSpread1_Sheet1.ActiveRowIndex );
            }

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            string errDrug = "";

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б���.���Ժ�...");
            Application.DoEvents();

            try
            {
                //�̵㵥��
                if (checkNO != "" && checkNO != null)
                {
                    FS.HISFC.Models.Pharmacy.Check check = this.itemManager.GetCheckStat(deptNO, checkNO);
                    if (check == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMsg(Language.Msg("�����̵㵥�Ż�ȡ�̵㵥ͳ����Ϣ��������"));
                        return -1;
                    }
                    if (check.State != "0")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMsg(Language.Msg("�̵㵥�ѽ����� ���˳�����"));
                        return -1;
                    }
                }

                //���½��̵㵥���̵�ͳ�Ʊ��������
                if (checkNO == "" || checkNO == null)
                {
                    #region ���½��̵㵥���̵�ͳ�Ʊ��������

                    if (this.newCheckNO != "")
                    {	//�����ʺ����ε������ʱ��������ȡ�̵㵥�ż������̵�ͳ�Ʊ�
                        checkNO = this.newCheckNO;
                    }
                    else
                    {
                        //��ȡ���̵㵥��
                        checkNO = this.itemManager.GetCheckCode(deptNO);
                        if (checkNO == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("��ȡ���̵㵥��ʧ��" + this.itemManager.Err);
                            return -1;
                        }

                        //���������ɵ��̵㵥��
                        this.newCheckNO = checkNO;
                       
                        FS.HISFC.Models.Pharmacy.Check info = new FS.HISFC.Models.Pharmacy.Check();

                        info.CheckNO = checkNO;				            //�̵㵥��
                        info.StockDept = this.privDept;			        //�ⷿ����
                        info.State = "0";					            //����״̬
                        info.User01 = "0";						        //�̿����
                        info.User02 = "0";						        //��ӯ���

                        info.FOper.ID = this.itemManager.Operator.ID;   //������
                        info.FOper.OperTime = sysTime;				    //����ʱ��
                        info.Operation.Oper = info.FOper;               //������

                        if (this.itemManager.InsertCheckStatic(info) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("����̵�ͳ�Ʊ�ʧ��" + this.itemManager.Err);
                            return -1;
                        }
                    }

                    #endregion
                }

                //DataSet�ڻ�÷����䶯������
                ArrayList modifyList = this.GetModify("Modify");
                ArrayList delList = this.GetModify("Del");

                if (modifyList != null)
                {
                    #region �Է����䶯�ļ�¼���и���

                    foreach (FS.HISFC.Models.Pharmacy.Check info in modifyList)
                    {
                        errDrug = info.Item.Name;

                        info.CheckNO = checkNO;			                        //�̵㵥��
                        info.State = "0";				                        //�̵�״̬ ����

                        info.Operation.Oper.ID = this.itemManager.Operator.ID;   //������Ϣ
                        info.Operation.Oper.OperTime = sysTime;			        //����ʱ��

                        //���������ݸ��ֶΣ���ˮ�ţ���FarPointȡ����Ϊ�գ���Ϊ��1
                        if (info.ID == "")
                        {
                            info.ID = "-1";
                        }

                        //�Ƚ��и��²����������ʧ�������
                        int parm = this.itemManager.UpdateCheckDetail(info);
                        //���̵���ϸ���������
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("�����̵���ϸʱ����" + this.itemManager.Err);
                            return -1;
                        }
                        else
                        {
                            if (parm == 0)
                            {
                                //���̵���ϸ���������
                                if (this.itemManager.InsertCheckDetail(info) != 1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    Function.ShowMsg("����̵���ϸʱ����" + this.itemManager.Err);
                                    return -1;
                                }
                            }
                        }

                        //�����̵���־{0A34566D-E154-47a4-BCB1-2437CC877F63}
                        parm = this.checkLogManager.InsertCheckLogs(info);
                        if (parm < 0) 
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("�����̵���־����!" + this.checkLogManager.Err);

                            return -1;
                        }//{0A34566D-E154-47a4-BCB1-2437CC877F63}����������
                    }
                    #endregion
                }
                if (delList != null)
                {		
                    #region ��ɾ���ļ�¼����ɾ��

                    foreach (FS.HISFC.Models.Pharmacy.Check info in delList)
                    {
                        //���̵���ϸ��¼ɾ��
                        if (this.itemManager.DeleteCheckDetail(info.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("ɾ���̵���ϸʱ����" + this.itemManager.Err);
                            return -1;
                        }
                    }
                    #endregion
                }

                //�����̵�ӯ������ӯ��������ӯ�����
                if (this.itemManager.SaveCheck(deptNO, checkNO) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("����ʵ���̴�����ʱ����" + this.itemManager.Err);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMsg(ex.Message + "  \n" + errDrug);
                return -1;
            }

            //�ύ����
            FS.FrameWork.Management.PublicTrans.Commit();

            Function.ShowMsg("����ɹ�");

            return 1;
        }

        /// <summary>
        ///�Է��ʡ��̵���̽����������棬�����̵���ϸ��
        /// </summary> 
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int AddSave(string deptNO, string chekNO)
        {
            if (this.dt.Rows.Count <= 0)
                return 0;

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            string errDrug = "";

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б���.���Ժ�...");
            Application.DoEvents();

            try
            {
                ArrayList modifyList = this.GetModify("Modify");

                if (modifyList != null)
                {
                    #region �Է����䶯�ļ�¼���и���

                    foreach (FS.HISFC.Models.Pharmacy.Check info in modifyList)
                    {
                        errDrug = info.Item.Name;
				
                        info.CheckNO = chekNO;			        //�̵㵥��		
                        info.State = "0";				        //�̵�״̬ ����

                        info.Operation.Oper.ID = this.itemManager.Operator.ID;	//������
                        info.Operation.Oper.OperTime = sysTime;			        //����ʱ��
                        info.CStoreQty = info.AdjustQty;

                        //���̵���ϸ���������						
                        int parm = this.itemManager.UpdateCheckDetailAddSave(info);
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("�����̵���ϸʱ����" + this.itemManager.Err);
                            return -1;
                        }
                    }

                    //�����̵�ӯ������ӯ��������ӯ�����
                    if (this.itemManager.SaveCheck(deptNO, chekNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMsg("����ʵ���̴�����ʱ����" + this.itemManager.Err);
                        return -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMsg(ex.Message + "  \n" + errDrug);
                return -1;
            }

            //�ύ����
            FS.FrameWork.Management.PublicTrans.Commit();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڵ�������...���Ժ�");
            Application.DoEvents();

            this.AutoExport();

            this.Clear();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show(Language.Msg("����ɹ�"), "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            return 1;
        }

        /// <summary>
        /// ����ǰ��ѯ���ݰ�Excel��ʽ����
        /// </summary>
        public void Export()
        {
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                    return;

                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڵ��� ���Ժ�...");
                    Application.DoEvents();

                    fileName = dlg.FileName;
                    this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ����ǰ��ѯ���ݰ�Excel��ʽ�Զ�����
        /// </summary>
        public void AutoExport()
        {
            try
            {
                this.dt.DefaultView.RowFilter = "1=1";

                DateTime dt = this.itemManager.GetDateTimeFromSysDateTime();

                string fileDir = @"c:\Check";
                if (!System.IO.Directory.Exists(fileDir))
                    System.IO.Directory.CreateDirectory(fileDir);
                string fileName = @"c:\Check\" + dt.ToString("MMdd-HHmm-ss") + ".xls";

                this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Filter()
        {
            if (this.dt.DefaultView == null)
                return;

            //��ù�������
            string queryCode = "";
            if (this.ckFlur.Checked)		//ģ����ѯ
                queryCode = "%" + this.txtFilter.Text.Trim() + "%";
            else
                queryCode = this.txtFilter.Text.Trim() + "%";

            try
            {               
                this.dt.DefaultView.RowFilter = Function.GetFilterStr(this.dt.DefaultView, queryCode);

                this.SetFlag();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �̵������̵������
        /// </summary>
        private void SumCheckNumAndCost(int rowIndex)
        {
            //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D}  ��¼ԭʼ���ʿ����
            decimal originalCheckQty = NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckQty].Text );

            //�̵��װ����
            decimal iPackNum = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.PackNum].Text);
            //�̵���С����
            decimal jMinNum = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.MinNum].Text);
            //��װ����
            decimal kPackQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.DrugPackQty].Text);
            //���ۼ�
            decimal price = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.RetailPrice].Text);

            //���˿����ɽ�������ʱ������lFStoreQty
            //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
            decimal lFSoreQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.FStoreQty].Text);

            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckQty].Text = (iPackNum * kPackQty + jMinNum).ToString();	//�̵���
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost].Text = Math.Round((iPackNum + jMinNum / kPackQty) * price, 2).ToString();

            // {B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ��ɽ�����һ������
            //��ԭ�����ܽ���ȥ������Ľ��
            if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text) > 0)
            {
                this.neuLbWinCost.Text = (NConvert.ToDecimal(this.neuLbWinCost.Text) - NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text)).ToString();
            }
            else
            {
                this.neuLbLoseCost.Text = (NConvert.ToDecimal(this.neuLbLoseCost.Text) - NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text)).ToString();
            }
            this.neuLbTotalCost.Text = (NConvert.ToDecimal(this.neuLbTotalCost.Text) - NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text)).ToString();
            //ʵ�ֻس�ʱ���Ը���ӯ���ܶӯ��������ӯ�����ĸ���
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCount].Text = (iPackNum * kPackQty + jMinNum - lFSoreQty).ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text = Math.Round((iPackNum * kPackQty + jMinNum - lFSoreQty) / kPackQty * price, 2).ToString();
            //�ü����Ľ���ټ��ϸ��ĺ�Ľ��õ����Ľ��
            if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text) > 0)
            {
                this.neuLbWinCost.Text = (NConvert.ToDecimal(neuLbWinCost.Text) + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text)).ToString();
            }
            else
            {
                this.neuLbLoseCost.Text = (NConvert.ToDecimal(neuLbLoseCost.Text) + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text)).ToString();
            }
            this.neuLbTotalCost.Text = Math.Round(Convert.ToDecimal(neuLbTotalCost.Text) + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckCost1].Text), 2).ToString();

            this.SetFlag();

            #region {F2DA66B0-0AB4-4656-BB21-97CB731ABA4D}  ���·���������ͳ���̵��ڼ�������ӯ��

            decimal newCheckQty = NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.CheckQty].Text );

            if (originalCheckQty != newCheckQty)            //�̵����������仯ʱ
            {
                string drugNO = this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.DrugNO].Text;
                string batchNO = this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.BatchNO].Text;
                string placeNO = this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.PlaceNO].Text;

                string key = drugNO + batchNO + placeNO;

                if (this.hsCheck.ContainsKey( key ))
                {
                    FS.HISFC.Models.Pharmacy.Check info = this.hsCheck[key] as FS.HISFC.Models.Pharmacy.Check;
                    if (info != null)
                    {
                        if (info.IsAdd == true)       //����ҩƷ ���·�������ʱ��Ҫ�ҵ�����Ŀ
                        {
                            #region ����ҩƷ�Ĵ���

                            for (int i = rowIndex + 1; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                            {
                                drugNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.DrugNO].Text;
                                batchNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.BatchNO].Text;
                                placeNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.PlaceNO].Text;
                                key = drugNO + batchNO + placeNO;

                                FS.HISFC.Models.Pharmacy.Check temp = this.hsCheck[key] as FS.HISFC.Models.Pharmacy.Check;
                                if (temp.Item.ID == info.Item.ID && temp.BatchNO == info.BatchNO)
                                {
                                    if (temp.IsAdd == false)            //�Ǹ���ҩƷ
                                    {
                                        rowIndex = i;
                                        info = temp;
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int i = rowIndex - 1; i >= 0; i--)
                            {
                                drugNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.DrugNO].Text;
                                batchNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.BatchNO].Text;
                                placeNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.PlaceNO].Text;
                                key = drugNO + batchNO + placeNO;

                                FS.HISFC.Models.Pharmacy.Check temp = this.hsCheck[key] as FS.HISFC.Models.Pharmacy.Check;
                                if (temp.Item.ID == info.Item.ID && temp.BatchNO == info.BatchNO)
                                {
                                    if (temp.IsAdd == false)
                                    {
                                        rowIndex = i;
                                        info = temp;
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }

                        #endregion
                        }

                        if (this.isUpdateFStoreRealTime == false)
                        {
                            //��ȡ��ǰ���
                            decimal storageNum = 0;
                            batchNO = info.BatchNO == "ALL" ? null : info.BatchNO;

                            if (this.itemManager.GetStorageNum( this.privDept.ID, info.Item.ID, batchNO, out storageNum ) == -1)
                            {
                                MessageBox.Show( "������ʿ�淢������" + this.itemManager.Err );
                                return;
                            }

                            info.FStoreQty = storageNum;
                            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.FStoreQty].Text = storageNum.ToString();
                        }

                        //�������������
                        decimal inoutQty = 0;
                        if (this.itemManager.ComputeInOutQty( this.privDept.ID, info.Item.ID, this.currentBillBeginCheckDate, out inoutQty ) == -1)
                        {
                            MessageBox.Show( "�����̵��ڼ������������������" + this.itemManager.Err );
                            return;
                        }
                        if (inoutQty != 0)
                        {
                            this.neuSpread1_Sheet1.RowHeader.Rows[rowIndex].BackColor = System.Drawing.Color.RosyBrown;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.RowHeader.Rows[rowIndex].BackColor = System.Drawing.Color.White;
                        }
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// ��ʼ�̵�ʱ���¼
        /// 
        /// {F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} 
        /// </summary>
        private void RecordCheckTime()
        {
            DateTime sysDate = this.itemManager.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Base.ExtendInfo info = new FS.HISFC.Models.Base.ExtendInfo();
            info.ExtendClass = FS.HISFC.Models.Base.EnumExtendClass.DEPT;
            info.Item.ID = this.privDept.ID + "-" + this.nowOperCheckNO;
            info.PropertyCode = "BeginCheck";
            info.PropertyName = "��ʼ�̵�ʱ��";

            info.DateProperty = sysDate;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.FrameWork.Management.ExtendParam extendParamManager = new ExtendParam();                    

            if (extendParamManager.DeleteComExtInfo( FS.HISFC.Models.Base.EnumExtendClass.DEPT, info.Item.ID, info.PropertyCode ) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( "ɾ����ʼ�̵�ʱ���¼��Ϣ��������" + extendParamManager.Err );
                return;
            }

            if (extendParamManager.InsertComExtInfo( info ) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( "���ɿ�ʼ�̵�ʱ���¼��Ϣ��������" + extendParamManager.Err );
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show( "ʱ���¼�ɹ�" );
        }

        /// <summary>
        /// ���ݷ��ʿ������̵���
        /// </summary>
        protected void FstoreSetAStore()
        {
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            foreach (DataRow dr in this.dt.Rows)
            {
                int fsQty = FS.FrameWork.Function.NConvert.ToInt32(dr["���ʿ��"]);
                int packQty = FS.FrameWork.Function.NConvert.ToInt32(dr["��װ����"]);
                int checkMinQty = 0;
                int checkPackQty = System.Math.DivRem(fsQty, packQty, out checkMinQty);

                dr["�̵�����1"] = checkPackQty.ToString();
                dr["�̵�����2"] = checkMinQty.ToString();

                dr["�̵���"] = dr["���ʿ��"];
            }

            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.SumCheckNumAndCost(i);
            }
        }

        /// <summary>
        /// ���԰�ҩƷ����ά��ҩƷ�Ƿ������̵�
        /// {98F0BF7A-5F41-4de3-884F-B38E71B41A8C}
        /// </summary>
        /// <param name="drugCode"></param>
        private int SetBatchCheckFlag(string drugCode)
        {
            if (this.isBatchInitial && this.isBatch)
            {
                return 1;
            }
            FS.HISFC.Models.Pharmacy.Item drugItem = this.hsItem[drugCode] as FS.HISFC.Models.Pharmacy.Item;
            string quality = drugItem.Quality.ID;
            if (this.dt.Rows.Count > 0 && this.isBatch != this.htSpecialCheck.ContainsKey(quality))
            {
                MessageBox.Show("����ӵ�ҩƷ" + (this.isBatch ? "�������̵�" : "���������̵�") + "�����ܼ��벻ͬ�̵㷽ʽ��ҩƷ");
                return -1;
            }
            this.isBatch = this.htSpecialCheck.ContainsKey(quality);
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.BatchNO].Visible = this.isBatch;
            return 1;
        }

        #endregion

        #region ���/���

        /// <summary>
        /// �Է����̵㵥���н�⴦��
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="checkCode">�̵㵥��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int CancelCheck(string deptNO, string checkNO)
        {
            //�統ǰ����������򷵻�
            if (this.dt.Rows.Count == 0)
            {
                return -1;
            }

            DialogResult result;
            //��ʾ�û�ѡ���Ƿ����
            result = MessageBox.Show(Language.Msg("ȷ�Ͻ��н�������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return -1;
            }

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��н�⴦��.���Ժ�...");
            Application.DoEvents();
            try
            {
                int i = this.itemManager.CancelCheck(deptNO, checkNO);
                //���δ�ɹ�����
                if (i == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("������ʧ��" + this.itemManager.Err);
                    return -1;
                }
                if (i == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("���ݷ����仯��ˢ�£�" + this.itemManager.Err);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMsg("������ʧ��" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            Function.ShowMsg("�������ɹ�");

            return 1;
        }

        /// <summary>
        /// ���̵���н�����
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="checkCode">�̵㵥��</param>
        /// <returns>�ɹ�����1��ʧ�ܷ��أ�1</returns>
        public int CheckCStore(string deptNO, string checkNO)
        {
            //�統ǰ����������򷵻�
            if (this.dt.Rows.Count == 0)
            {
                return -1;
            }

            //��ȡ�Ƿ������̵�;��ȡ��ǰ��ʾ���̵㵥�Ƿ������̵㣬��ǰͨ����ϸ���������ֶ��жϣ��Ժ����ͳ�Ʊ��ڼ��ֶ�
            bool isBatch;
            DataRow row = this.dt.Rows[0];
            if (row["����"].ToString() == "ALL")
                isBatch = false;
            else
                isBatch = true;

            DialogResult result;
            //��ʾ�û�ѡ���Ƿ����
            result = MessageBox.Show(Language.Msg("ȷ�Ͻ��н�������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return -1;
            }

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��н�洦��.���Ժ�...");
            Application.DoEvents();
            try
            {
                if (this.itemManager.ExecProcedurgCheckCStore(deptNO, checkNO, isBatch) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("������ʧ��" + this.itemManager.Err);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMsg("������ʧ��" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            Function.ShowMsg("�������ɹ�");

            return 1;
        }

        #endregion

        #region �¼�

        private void ucCheckManager_Load(object sender, EventArgs e)
        {
            this.InitDataTable();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
                Application.DoEvents();

                this.InitData();

                this.InitCheckList();

                //{98F0BF7A-5F41-4de3-884F-B38E71B41A8C}���ݲ�ͬҩƷ���ʰ������̵�
                this.InitSpecialCheck();

                this.SetToolButton(true);

                //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} �̵��ڼ��������ϸ��Ϣ��ʾ
                this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler( neuSpread1_CellDoubleClick );
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.isUpdateFStoreRealTime = ctrlIntegrate.GetControlParam<bool>( FS.HISFC.BizProcess.Integrate.PharmacyConstant.Check_UpdateFStore_RealTime, true, false );
                //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} 
                
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                //�ñ���Ϊ��̬���������ĵط������и�ֵ������
                Function.IPrint = null;
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D}  ������ϸ������ʾ
            if (e.RowHeader)
            {
                if (this.neuSpread1_Sheet1.RowHeader.Rows[e.Row].BackColor == System.Drawing.Color.RosyBrown)
                {
                    string drugCode = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.DrugNO].Text;

                    DataSet ds = this.itemManager.ComputeInOutDetailForCheck( this.privDept.ID, drugCode, this.currentBillBeginCheckDate );
                    if (ds != null)
                    {
                        using (FS.FrameWork.WinForms.Controls.ucBaseControl uc = new FS.FrameWork.WinForms.Controls.ucBaseControl())
                        {
                            uc.Width = 500;
                            uc.Height = 300;
                            
                            FarPoint.Win.Spread.SheetView sv = new FarPoint.Win.Spread.SheetView();
                            sv.DataSource = ds;
                            sv.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
                            sv.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;

                            FS.FrameWork.WinForms.Controls.NeuSpread fs = new FS.FrameWork.WinForms.Controls.NeuSpread();
                            fs.Sheets.Add( sv );

                            fs.BackColor = System.Drawing.Color.White;
                            

                            fs.Dock = DockStyle.Fill;

                            uc.Controls.Add( fs );

                            FS.FrameWork.WinForms.Classes.Function.PopShowControl( uc );
                        }
                    }
                }
            }
        }     

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                return;
            }

            if (e.KeyData == Keys.Up)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.neuSpread1.Focus();
                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.PackNum;
            }
        }

        // �������̵�����1���̵�����2������̵���
        private void fpSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == (int)ColumnSet.PackNum || e.Column == (int)ColumnSet.MinNum)
            {
                this.SumCheckNumAndCost(e.Row);                
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:

                    #region �س���ת

                    if (this.neuSpread1.ContainsFocus)
                    {
                        int i = this.neuSpread1_Sheet1.ActiveRowIndex;
                        int j = this.neuSpread1_Sheet1.ActiveColumnIndex;
                        if (j == (int)ColumnSet.MinNum || j == (int)ColumnSet.PackNum)
                        {
                            if (j == (int)ColumnSet.PackNum)
                            {
                                this.neuSpread1_Sheet1.SetActiveCell(i, (int)ColumnSet.MinNum, false);
                            }
                            else
                            {
                                if (j == (int)ColumnSet.MinNum)
                                {
                                    if (i < this.neuSpread1_Sheet1.Rows.Count - 1)
                                    {
                                        this.neuSpread1_Sheet1.ActiveRowIndex++;
                                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.PackNum, false);
                                    }
                                    else
                                    {
                                        this.txtFilter.Focus();
                                        this.txtFilter.SelectAll();
                                    }
                                }
                            }

                            this.SumCheckNumAndCost(i);
                        }
                    }

                    #endregion

                    break;
                case Keys.F5:

                    this.txtFilter.Focus();
                    this.txtFilter.SelectAll();

                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node != null && e.Node.Parent != null)
            {
                FS.HISFC.Models.Pharmacy.Check check = e.Node.Tag as FS.HISFC.Models.Pharmacy.Check;
                
                //{F2DA66B0-0AB4-4656-BB21-97CB731ABA4D} ���ӿ�ʼ�̵�ʱ���¼
                FS.FrameWork.Management.ExtendParam extendParamManager = new ExtendParam();
                this.currentBillBeginCheckDate = extendParamManager.GetComExtInfoDateTime( FS.HISFC.Models.Base.EnumExtendClass.DEPT, "BeginCheck", this.privDept.ID + "-" + check.CheckNO );
                if (this.currentBillBeginCheckDate == System.DateTime.MinValue)
                {
                    this.currentBillBeginCheckDate = check.FOper.OperTime;
                }

                this.nowOperCheckNO = check.CheckNO;

                this.ShowCheckDetail(this.privDept.ID, check.CheckNO);
            }
        }

        #endregion

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Clear();
            
            if (e != null && e.Parent != null)
            {
                FS.HISFC.Models.Pharmacy.Check check = e.Tag as FS.HISFC.Models.Pharmacy.Check;

                this.nowOperCheckNO = check.CheckNO;

                this.ShowCheckDetail(this.privDept.ID, check.CheckNO);
            }
            return base.OnSetValue(neuObject, e);
        }

        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (activeRow < 0)
                return;

            string drugCode = sv.Cells[activeRow, 0].Text;
            string batchNo = sv.Cells[activeRow, 3].Text;
            string placeCode = sv.Cells[activeRow, 4].Text;

            //���԰�ҩƷ����ά��ҩƷ�Ƿ������̵�{98F0BF7A-5F41-4de3-884F-B38E71B41A8C}
            if (this.SetBatchCheckFlag(drugCode) < 0)
            {
                return;
            }

            this.AddCheckData(this.privDept.ID,drugCode, batchNo, placeCode, this.isBatch);
            //��ɽ�µ�����SetFlag()
            //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
            this.SetFlag();
        }

        #region ��������

        public class NoSort : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.Check c1 = x as FS.HISFC.Models.Pharmacy.Check;
                FS.HISFC.Models.Pharmacy.Check c2 = y as FS.HISFC.Models.Pharmacy.Check;

                return NConvert.ToInt32(c1.ID) - NConvert.ToInt32(c2.ID);
            }

            #endregion
        }

        #endregion

        #region ������

        private enum ColumnSet
        {
            /// <summary>
            /// ��λ��		
            /// </summary>
            PlaceNO,        
            /// <summary>
            /// �Զ�����	
            /// </summary>
            UserCode,
            /// <summary>
            /// ��Ʒ����	
            /// </summary>
            TradeName,
            /// <summary>
            /// ���		
            /// </summary>
            Specs,
            /// <summary>
            /// ��װ����
            /// </summary>
            DrugPackQty,
            /// <summary>
            /// ����		
            /// </summary>
            BatchNO,
            /// <summary>
            /// ��Ч��		
            /// </summary>
            ValidDate,
            /// <summary>
            /// ���ۼ�		
            /// </summary>
            RetailPrice,          
            /// <summary>
            /// �̵�����1 ��װ����	
            /// </summary>
            PackNum,
            /// <summary>
            /// ��װ��λ	
            /// </summary>
            PackUnit,
            /// <summary>
            /// �̵�����2 ��С��λ	
            /// </summary>
            MinNum,
            /// <summary>
            /// ��С��λ	
            /// </summary>
            MinUnit,
            /// <summary>
            /// �̵���
            /// </summary>
            CheckQty,
            /// <summary>
            /// ���ʿ��	
            /// </summary>
            FStoreQty,
            /// <summary>
            /// ��λ ��С��λ
            /// </summary>
            CheckUnit,
            /// <summary>
            /// �̵���
            /// </summary>
            CheckCost,
            /// <summary>
            /// ӯ������     //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
            /// </summary>
            CheckCount,//lvshy
            /// <summary>
            /// ӯ�����     //{B465E3E5-A81C-46f3-B893-13CE12EA7390}  ����ӯ��������ʾ
            /// </summary>
            CheckCost1,//lvshy
            /// <summary>
            /// ��ע
            /// </summary>
            Memo,
            /// <summary>
            /// ӯ����� 0 ��ӯ�� 1 ��Ӯ 2 �̿�
            /// </summary>
            CheckFlag,
            /// <summary>
            /// �Ƿ񸽼�	
            /// </summary>
            IsAdd,
            /// <summary>
            /// ��ˮ��	
            /// </summary>
            CheckNO,
            /// <summary>
            /// ҩƷ����	
            /// </summary>
            DrugNO,
            /// <summary>
            /// ƴ����		
            /// </summary>
            SpellCode,
            /// <summary>
            /// �����		
            /// </summary>
            WBCode,
            /// <summary>
            /// ͨ����ƴ����
            /// </summary>
            RegularSpell,
            /// <summary>
            /// ͨ���������
            /// </summary>
            RegularWB
        }

        #endregion       

    
        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint);

                return printType;
            }
        }

        #endregion

        #region IPreArrange ��Ա

        bool isPreArrange = false;

        public int PreArrange()
        {
            this.isPreArrange = true;

            string class2Priv = "0305";
            //���ݽ�水ť����λ���жϴ������� ��ʾ���ʱ �̵��� ���� �̵���� 
            if (this.toolBarService.GetToolButton("���").Owner != null && this.toolBarService.GetToolButton("���").Owner.Visible)      //���
            {
                class2Priv = "0306";            //�̵���
            }
            else
            {
                class2Priv = "0305";            //�̵����
            }

            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept(class2Priv, ref testPrivDept);

            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                return -1;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return -1;
            }

            this.privDept = testPrivDept;

            //if (this.isCheckPartition)
            //{
            //    base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name + "�� �̵����¼��");
            //}
            //else
            //{
            //    base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);
            //}

            base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

            return 1;
        }

        #endregion
    }
}
