using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Function;

namespace Neusoft.UFC.Material.Plan
{
    public partial class ucBuyPlan : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucBuyPlan()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private Neusoft.NFC.Object.NeuObject privDept = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        private FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

        /// <summary>
        /// ���ڼ����վ���������������
        /// </summary>
        private int outday = 30;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private Neusoft.NFC.Public.ObjectHelper deptHelper = new Neusoft.NFC.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private Neusoft.NFC.Public.ObjectHelper personHelper = new Neusoft.NFC.Public.ObjectHelper();

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        private Neusoft.NFC.Public.ObjectHelper produceHelpter = new Neusoft.NFC.Public.ObjectHelper();

        /// <summary>
        /// ���ƻ�������
        /// </summary>
        private Neusoft.HISFC.Management.Material.Plan planManager = new Neusoft.HISFC.Management.Material.Plan();

        /// <summary>
        /// ��Ʒ������Ϣ������
        /// </summary>
        private Neusoft.HISFC.Management.Material.MetItem itemManager = new Neusoft.HISFC.Management.Material.MetItem();

        /// <summary>
        /// �洢�ƻ�����
        /// </summary>
        private System.Collections.Hashtable hsPlanData = new Hashtable();

        /// <summary>
        /// �Ƿ�Լƻ�����Ϊ0������Ч���ж� 
        /// </summary>
        private bool isJudgeValid = true;

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private string nowBillNO = "";

        private string comId = "";

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
        /// �Ƿ񰴹�����˾��ʾ�б�
        /// </summary>
        private bool isVisibleCom = true;

        bool isApprove = false;

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
            }
        }


        /// <summary>
        /// �жϵ�ǰ�򿪴����Ƿ�����˴���
        /// </summary>
        public bool IsCheck
        {
            get
            {
                return this.isCheck;
            }
            set
            {
                this.isCheck = value;
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
        /// �Ƿ񰴹�����˾��ʾ�б�
        /// </summary>
        public bool IsVisibleCom
        {
            get
            {
                return this.isVisibleCom;
            }
            set
            {
                this.isVisibleCom = value;
            }
        }

        #endregion

        #region ������

        private Neusoft.NFC.Interface.Forms.ToolBarService toolBarService = new Neusoft.NFC.Interface.Forms.ToolBarService();

        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�� ʾ ��", "��ʾ��", Neusoft.NFC.Interface.Classes.EnumImageList.A����, true, false, null);
            
            toolBarService.AddToolButton("�� �� ��", "�ƻ����б�", Neusoft.NFC.Interface.Classes.EnumImageList.A��Ϣ, true, false, null);
            toolBarService.AddToolButton("ɾ    ��", "ɾ����ǰѡ��ļƻ�ҩƷ", Neusoft.NFC.Interface.Classes.EnumImageList.Aɾ��, true, false, null);
            toolBarService.AddToolButton("����ɾ��", "ɾ�������ƻ���", Neusoft.NFC.Interface.Classes.EnumImageList.Aȡ��, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "�ɹ����б�", Neusoft.NFC.Interface.Classes.EnumImageList.F����, true, false, null);            
            
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ɾ    ��":
                    this.DeleteData();
                    break;
                case "ɾ������":
                    this.DeleteDataByBill(this.privDept.ID, this.nowBillNO);
                    break;
                case "�� �� ��":
                    this.tvList.ShowInPlanList(this.privDept, "2");
                    this.IsShowList = true;
                    break;
                case "�� �� ��":
                    this.tvList.ShowStockPlanList(this.privDept, "3");
                    this.IsShowList = true;
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
                case "��    ӡ":
                    break;
                case "��    ��":
                    this.Export();
                    break;
                case "δ �� ��":
                    this.tvList.ShowStockPlanList(this.privDept, "3");
                    this.IsShowList = true;
                    break;
                case "�� �� ��":
                    this.tvList.ShowStockPlanList(this.privDept, "5");
                    this.IsShowList = true;
                    break;
                case "�� ʾ ��":
                    this.ucMaterialItemList1.Visible = !this.ucMaterialItemList1.Visible;
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
            Neusoft.NFC.Interface.Classes.Print print = new Neusoft.NFC.Interface.Classes.Print();

            print.PrintPreview(40, 10, this.neuPanel1);
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
            this.toolBarService.SetToolButtonEnabled("��    ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("����ɾ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("�� ʾ ��", !isShowList);

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
            Neusoft.HISFC.Management.Manager.Department deptManager = new Neusoft.HISFC.Management.Manager.Department();
            ArrayList deptAll = deptManager.GetDeptmentAll();
            if (deptAll == null)
            {
                MessageBox.Show("������ÿ����б����" + deptManager.Err);
                return;
            }
            this.deptHelper.ArrayObject = deptAll;
            //��ò���Ա����
            Neusoft.HISFC.Management.Manager.Person personManager = new Neusoft.HISFC.Management.Manager.Person();
            ArrayList personAl = personManager.GetEmployeeAll();
            if (personAl == null)
            {
                MessageBox.Show("��ȡȫ����Ա�б����!" + personManager.Err);
                return;
            }
            this.personHelper.ArrayObject = personAl;
            //��ȡ��������
            Neusoft.HISFC.Management.Material.ComCompany company = new Neusoft.HISFC.Management.Material.ComCompany();
            ArrayList produceAl = company.QueryCompany("0", "A");
            if (produceAl == null)
            {
                MessageBox.Show("��ȡ���������б����!" + company.Err);
                return;
            }
            this.produceHelpter.ArrayObject = produceAl;

            #endregion

            this.ucMaterialItemList1.ShowMaterialList();
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
														  new DataColumn("ѡ��",dtBol),
														  new DataColumn("��Ʒ���",      dtStr),//--liuxq
														  new DataColumn("��Ʒ����",	  dtStr),
														  new DataColumn("���",        dtStr),
														  new DataColumn("�ƻ������",  dtDec),
														  new DataColumn("�ƻ�����",	  dtDec),
														  new DataColumn("��λ",        dtStr),
														  new DataColumn("�ƻ����",	  dtDec),
														  new DataColumn("�ɹ�����",	  dtDec),
														  new DataColumn("�ɹ����",	  dtDec),
														  new DataColumn("���ƿ��",	  dtDec),
														  new DataColumn("ȫԺ���",	  dtDec),
														  new DataColumn("��������",	  dtDec),														  
														  new DataColumn("��������",    dtStr),
														  new DataColumn("������˾",    dtStr),
														  new DataColumn("��Ʊ��",      dtStr),//liuxq add
														  new DataColumn("��˾����",    dtStr),
														  new DataColumn("��ע",        dtStr),
														  new DataColumn("��Ʒ����",	  dtStr),
														  new DataColumn("ƴ����",      dtStr),
														  new DataColumn("�����",      dtStr),
														  new DataColumn("�Զ�����",    dtStr)
													  });

            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;

            this.neuSpread1_Sheet1.Columns.Get(13).Visible = false;

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

            FarPoint.Win.Spread.CellType.CheckBoxCellType chkCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.isCheck].Width = 5F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColKind].Width = 10F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradeName].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpecs].Width = 50F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPurchasePrice].Width = 50F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].Width = 50F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanCost].Width = 50F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColStockNum].Width = 50F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColStockCost].Width = 50F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUnit].Width = 10F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColCompany].Width = 20F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColInvoiceNo].Width = 10F;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.isCheck].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColKind].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColItemNO].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColAllStockNum].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOutTotal].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOwnStockNum].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColCompanyID].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColCompany].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColInvoiceNo].Visible = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.isCheck].CellType = chkCell;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.isCheck].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColStockNum].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColCompany].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColInvoiceNo].Locked = false;


            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColStockNum].BackColor = System.Drawing.Color.SeaShell;
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
            if (this.IsCheck == false)
            {
                this.tvList.ShowInPlanList(this.privDept, "2");
            }
            else
            {
                if (this.IsVisibleCom == false)
                {
                    this.tvList.ShowBuyPlanList(this.privDept, "3");
                }
                else
                {
                    this.tvList.ShowStockPlanList(this.privDept, "3");
                }

            }

        }


        #endregion

        #region ����

        /// <summary>
        /// �����ݱ��ڼ�������
        /// </summary>
        /// <param name="inPlan"></param>
        private int AddDataToTable(Neusoft.HISFC.Object.Material.InputPlan inPlan)
        {
            try
            {
                if (inPlan.PlanPrice == 0)
                    inPlan.PlanPrice = inPlan.StoreBase.PriceCollection.RetailPrice;

                decimal planCost = inPlan.PlanNum / inPlan.StoreBase.Item.PackQty * inPlan.PlanPrice;
                if (this.produceHelpter != null)
                    inPlan.Producer.Name = this.produceHelpter.GetName(inPlan.Producer.ID);

                this.dt.Rows.Add(new object[] { 
												  this.isApprove,
												  inPlan.StoreBase.Item.MaterialKind.Name,//---liuxq
												  inPlan.StoreBase.Item.Name,                           //��Ʒ����
												  inPlan.StoreBase.Item.Specs,                          //���
												  inPlan.PlanPrice,										//�ƻ������
												  inPlan.PlanNum / inPlan.StoreBase.Item.PackQty,       //�ƻ�����
												  inPlan.StoreBase.Item.PackUnit,                       //��λ
												  planCost,												//�ƻ���� 
												  inPlan.PlanNum / inPlan.StoreBase.Item.PackQty,       //�ɹ�����												
												  planCost,												//�ɹ���� 
												  inPlan.StoreSum / inPlan.StoreBase.Item.PackQty,      //���ƿ��
												  inPlan.StoreTotsum / inPlan.StoreBase.Item.PackQty,   //ȫԺ���
												  inPlan.OutputSum / inPlan.StoreBase.Item.PackQty,     //��������												  
												  inPlan.Producer.Name,									//��������
												  inPlan.Company.Name,
												  inPlan.InvoiceNo,//��Ʊ��liuxq add
												  inPlan.Company.ID,
												  inPlan.Memo,											//��ע
												  inPlan.StoreBase.Item.ID,                             //��Ʒ����
												  inPlan.StoreBase.Item.SpellCode,						//ƴ����
												  inPlan.StoreBase.Item.WbCode,						//�����
												  inPlan.StoreBase.Item.UserCode						//�Զ�����
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
                    if (NConvert.ToDecimal(dr["�ɹ�����"]) < 0)
                    {
                        MessageBox.Show("������" + dr["��Ʒ����"].ToString() + " �ɹ�������");
                        return false;
                    }
                    //					if (this.isCheckNumZero && (NConvert.ToDecimal(dr["�ƻ�����"]) == 0))
                    //					{
                    //						MessageBox.Show("������ " + dr["��Ʒ����"].ToString() + " �ɹ����� �ɹ�������Ϊ��");
                    //						return false;
                    //					}
                    //					if ((dr["������˾"].ToString()) == "")
                    //					{
                    //						MessageBox.Show("������" + dr["��Ʒ����"].ToString() + " �Ĺ�����˾��");
                    //						return false;
                    //					}
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
            node.Tag = new Neusoft.HISFC.Object.Material.InputPlan();

            this.tvList.Nodes[0].Nodes.Insert(0, node);

            //ѡ�д��½ڵ�
            this.tvList.SelectedNode = node;

            //�л�����Ʒ�����б�
            this.IsShowList = false;

            this.ucMaterialItemList1.SetFocusSelect();
        }


        /// <summary>
        /// ����Ʒʵ�����
        /// </summary>
        /// <param name="item">��Ʒʵ��</param>
        /// <param name="totOutQty">��������</param>
        /// <param name="averageOutQty">�վ�����</param>
        /// <param name="planQty">���ݾ������Զ����ɵļƻ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(Neusoft.HISFC.Object.Material.MaterialItem item, decimal totOutQty, decimal averageOutQty, decimal planQty)
        {
            if (this.hsPlanData.ContainsKey(item.ID))
            {
                MessageBox.Show("����Ʒ����ӵ��ƻ����� ͬһƷ����Ʒ�����ظ����");
                return 0;
            }

            //��ȡȫԺ�����
            //			decimal itemSum = 0, itemTotSum = 0;
            //
            //			if (this.planManager.FindSum(this.privDept.ID, item.ID, ref itemSum, ref itemTotSum) == -1)
            //			{
            //				MessageBox.Show("��ȡ��" + item.Name + "���������ʱ��������" + this.planManager.Err);
            //				return -1;
            //			}

            Neusoft.HISFC.Object.Material.InputPlan inPlan = new Neusoft.HISFC.Object.Material.InputPlan();

            inPlan.StoreBase.Item = item;
            //			inPlan.StoreTotsum = itemTotSum;
            //			inPlan.StoreSum = itemSum;
            inPlan.PlanNum = planQty;
            inPlan.PlanPrice = inPlan.StoreBase.Item.UnitPrice;
            inPlan.StockNum = planQty;

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
        public int AddDrugData(Neusoft.HISFC.Object.Material.MaterialItem item, decimal planQty)
        {
            return this.AddDrugData(item, 0, 0, planQty);
        }


        /// <summary>
        /// ����Ʒʵ�����
        /// </summary>
        /// <param name="item">��Ʒʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(Neusoft.HISFC.Object.Material.MaterialItem item)
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
            #region ��ʱ�������ݾ��������ɼƻ������룬�պ����ʵ�����������ӡ�lichao
            /*if (this.dt.Rows.Count > 0)
			{
				DialogResult result;
				result = MessageBox.Show("�����������ɽ������ǰ���ƻ������ݣ��Ƿ����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);

				if (result == DialogResult.No)
					return;
			}

			//�������
			this.Clear();

			try
			{
				ArrayList alDetail = new ArrayList();

				if (alterFlag == "1")
				{
					#region �������� ���������Ĳ��� ������������Ϣ
					using (ucPhaAlter uc = new ucPhaAlter())
					{
						uc.DeptCode = this.privDept.ID;
						uc.SetData();
						uc.Focus();
						Neusoft.NFC.Interface.Classes.Function.PopShowControl(uc);

						if (uc.ApplyInfo != null)
						{
							alDetail = uc.ApplyInfo;
						}
					}
					#endregion
				}
				else
				{
					alDetail = this.itemManager.FindByAlter("0", this.privDept.ID, System.DateTime.MinValue, System.DateTime.MaxValue);
					if (alDetail == null)
					{
						MessageBox.Show("��������������ִ����Ϣ����δ��ȷִ��\n" + this.itemManager.Err);
						return;
					}
				}

				if (alDetail.Count == 0)
				{
					MessageBox.Show("��������������Ʒ�ƻ���Ϣ");
					return;
				}

				Neusoft.HISFC.Object.Material.MaterialItem item = new Neusoft.HISFC.Object.Material.MaterialItem();

				foreach (Neusoft.NFC.Object.NeuObject temp in alDetail)
				{
					item = this.itemManager.QueryMetItemAllByID(temp.ID);
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
			}*/
            #endregion
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

			ArrayList alOpenDetail = Function.ChooseDrugStencil(this.privDept.ID,Neusoft.HISFC.Object.Pharmacy.EnumDrugStencil.Plan);

			if (alOpenDetail != null && alOpenDetail.Count > 0)
			{             
				Neusoft.NFC.Interface.Classes.Function .ShowWaitForm("���ڸ�����ѡģ���ɼƻ���Ϣ...");
				Application.DoEvents();
				//�ȼ��ؿ����Ϣ��Hs ��֤ģ�����˳��
				System.Collections.Hashtable hsStoreDrug = new Hashtable();

				ArrayList alItem = this.itemManager.QueryItemAvailableList(false);
				foreach (Neusoft.HISFC.Object.Material.MaterialItem item in alItem)
				{
					hsStoreDrug.Add(item.ID, item);
				}

				int i = 0;
				foreach (Neusoft.HISFC.Object.Material.MaterialItem info in alOpenDetail)
				{
					Neusoft.NFC.Interface.Classes.Function .ShowWaitForm(i, alOpenDetail.Count);
					Application.DoEvents();

					if (hsStoreDrug.Contains(info.Item.ID))
					{
						this.AddDrugData(hsStoreDrug[info.Item.ID] as Neusoft.HISFC.Object.Pharmacy.Item);
					}

					i++;
				}

				Neusoft.NFC.Interface.Classes.Function .HideWaitForm();

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

            ArrayList alDetail = new ArrayList();

            //ȡ���ƻ��е�����
            if (this.IsCheck == false)
            {
                alDetail = this.planManager.QueryInPlanDetail(privDept, billNO);
            }
            else
            {
                alDetail = this.planManager.QueryInPlanDetailCom(privDept, billNO, this.comId);
            }

            if (alDetail == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return -1;
            }

            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("������ʾ�ƻ���ϸ ���Ժ�...");
            Application.DoEvents();

            foreach (Neusoft.HISFC.Object.Material.InputPlan info in alDetail)
            {
                //��������ɹ��ƻ������ݲ���ʾ 
                if (info.State != "2" && info.State != "3" && info.State != "5")
                    continue;

                info.StoreBase.Item = this.itemManager.QueryMetItemAllByID(info.StoreBase.Item.ID);
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
                    Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
                    return -1;
                }
            }

            Neusoft.NFC.Interface.Classes.Function.HideWaitForm();

            this.SetSum();

            return 1;
        }


        /// <summary>
        /// ���üƻ���Ϣ��ʾ
        /// </summary>
        /// <param name="inPlan"></param>
        private void SetPlanInfo(Neusoft.HISFC.Object.Material.InputPlan inPlan)
        {
            this.lbPlanBill.Text = "���ݺ�:" + inPlan.PlanListCode;

            this.lbPlanInfo.Text = "�ƻ�����: " + this.privDept.Name + " �ƻ���: " + inPlan.StoreBase.Operation.ApplyOper.ID;
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

            Neusoft.NFC.Management.PublicTrans.BeginTransaction();

            //Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction(Neusoft.NFC.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(Neusoft.NFC.Management.PublicTrans.Trans);

            try
            {
                int parm = this.planManager.DeleteInputPlan(deptCode, billCode);
                if (parm == -1)
                {
                    Neusoft.NFC.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.planManager.Err);
                    return -1;
                }
                else
                    if (parm != this.dt.Rows.Count)
                    { //������
                        Neusoft.NFC.Management.PublicTrans.RollBack();
                        MessageBox.Show("���ݷ����䶯����ˢ�´���");
                        return -1;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Neusoft.NFC.Management.PublicTrans.Commit();

            this.tvList.ShowStockPlanList(this.privDept, "2");

            return 1;
        }


        /// <summary>
        /// ����ɹ��ƻ���
        /// </summary>
        public int Save()
        {
            if (this.dt.Rows.Count <= 0)
                return -1;
            if (!this.IsValid())
                return -1;

            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڽ��б��� ���Ժ�...");
            Application.DoEvents();

            //ϵͳʱ��
            DateTime sysTime = this.planManager.GetDateTimeFromSysDateTime();

            //�������ݿ⴦������
            Neusoft.NFC.Management.PublicTrans.BeginTransaction();

            //Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction(Neusoft.NFC.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(Neusoft.NFC.Management.PublicTrans.Trans);

            int iCount = 1;

            foreach (DataRow dr in this.dt.Rows)
            {
                Neusoft.NFC.Interface.Classes.Function.ShowWaitForm(iCount, this.dt.Rows.Count);
                Application.DoEvents();

                #region ���ƻ���ֵ ����

                //�Լƻ�����Ϊ0�Ĳ����д���
                if (NConvert.ToDecimal(dr["�ƻ�����"]) == 0)
                    continue;
                if (!NConvert.ToBoolean(dr["ѡ��"]))
                    continue;
                else
                {
                    if ((dr["������˾"].ToString()) == "")
                    {
                        MessageBox.Show("������" + dr["��Ʒ����"].ToString() + " �Ĺ�����˾��");
                        return -1;
                    }
                }

                Neusoft.HISFC.Object.Material.InputPlan inPlan = this.hsPlanData[dr["��Ʒ����"].ToString()] as Neusoft.HISFC.Object.Material.InputPlan;

                inPlan.PlanListCode = this.nowBillNO;												 //�ƻ�����
                inPlan.PlanType = this.planType;
                inPlan.StockNum = NConvert.ToDecimal(dr["�ɹ�����"]) * inPlan.StoreBase.Item.PackQty;//�ɹ�����
                inPlan.StockOper.ID = this.planManager.Operator.ID;									 //�ɹ�Ա
                inPlan.StockTime = sysTime;															 //������Ϣ
                inPlan.StoreBase.Operation.Oper.ID = inPlan.StockOper.ID;							 //����Ա
                inPlan.Memo = dr["��ע"].ToString();												 //��ע
                inPlan.Company.ID = dr["��˾����"].ToString();										 //��˾����
                inPlan.Company.Name = dr["������˾"].ToString();								     //������˾
                inPlan.InvoiceNo = dr["��Ʊ��"].ToString();//��Ʊ��liuxq add

                if (this.planManager.UpdatePlanForStock(inPlan.StorageCode, inPlan.PlanListCode, inPlan.PlanNo, inPlan.StockNum, inPlan.StoreBase.Operation.ApplyOper.ID, sysTime, "3", inPlan.Company.ID, inPlan.Company.Name, inPlan.InvoiceNo) == -1)
                {
                    Neusoft.NFC.Management.PublicTrans.RollBack();
                    Function.ShowMsg(inPlan.StoreBase.Item.Name + "����ʧ�� " + this.planManager.Err);
                    return -1;
                }

                #endregion

                iCount++;
            }

            Neusoft.NFC.Management.PublicTrans.Commit();

            Function.ShowMsg("����ɹ�");
            //�������
            this.Clear();

            this.tvList.ShowInPlanList(this.privDept, "2");

            return 1;
        }


        /// <summary>
        /// ��˲ɹ��ƻ���
        /// </summary>
        public int SaveCheck()
        {
            if (this.dt.Rows.Count <= 0)
                return -1;
            if (!this.IsValid())
                return -1;

            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڽ��б��� ���Ժ�...");
            Application.DoEvents();

            //ϵͳʱ��
            DateTime sysTime = this.planManager.GetDateTimeFromSysDateTime();

            //�������ݿ⴦������
            Neusoft.NFC.Management.PublicTrans.BeginTransaction();

            //Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction(Neusoft.NFC.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(Neusoft.NFC.Management.PublicTrans.Trans);

            int iCount = 1;
            string saveState = "5";
            foreach (DataRow dr in this.dt.Rows)
            {
                Neusoft.NFC.Interface.Classes.Function.ShowWaitForm(iCount, this.dt.Rows.Count);
                Application.DoEvents();

                #region ���ƻ���ֵ ����

                //�Լƻ�����Ϊ0�Ĳ����д���
                if (NConvert.ToDecimal(dr["�ƻ�����"]) == 0)
                    continue;

                Neusoft.HISFC.Object.Material.InputPlan inPlan = this.hsPlanData[dr["��Ʒ����"].ToString()] as Neusoft.HISFC.Object.Material.InputPlan;

                inPlan.PlanListCode = this.nowBillNO;												 //�ƻ�����
                if (this.IsFinance == false)//�ƻ���״̬�ж� 4:��Ҫ����������� 5:����Ҫ����������� 
                {
                    saveState = "5";
                    inPlan.State = "5";
                }
                else
                {
                    saveState = "4";
                    inPlan.State = "4";
                }
                inPlan.PlanType = this.planType;													 //�ɹ�����				
                inPlan.StockNum = NConvert.ToDecimal(dr["�ɹ�����"]) * inPlan.StoreBase.Item.PackQty;//�ɹ�����
                inPlan.StockOper.ID = this.planManager.Operator.ID;									 //�ɹ�Ա
                inPlan.StockTime = sysTime;															 //������Ϣ
                inPlan.StoreBase.Operation.Oper.ID = inPlan.StockOper.ID;							 //����Ա
                inPlan.Memo = dr["��ע"].ToString();												 //��ע

                if (this.planManager.UpdateBuyPlanState(inPlan.StorageCode, inPlan.PlanListCode, inPlan.PlanNo, saveState, inPlan.StoreBase.Operation.ApplyOper.ID, sysTime) == -1)
                {
                    Neusoft.NFC.Management.PublicTrans.RollBack();
                    Function.ShowMsg(inPlan.StoreBase.Item.Name + "����ʧ�� " + this.planManager.Err);
                    return -1;
                }

                #endregion

                iCount++;
            }

            Neusoft.NFC.Management.PublicTrans.Commit();

            Function.ShowMsg("����ɹ�");
            //�������
            this.Clear();

            this.tvList.ShowBuyPlanList(this.privDept, "3");

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


        #endregion

        #region �¼�

        public void ToolBarClicked(string parm)
        {/*
            switch (parm)
            {
                case "ɾ��":
                    this.DeleteData();
                    break;
                case "ɾ������":
                    this.DeleteDataByBill(this.privDept.ID, this.nowBillNO);
                    break;
                case "�ƻ���":
                    this.tvList.ShowInPlanList(this.privDept, "2");
                    this.IsShowList = true;
                    break;
                case "�ɹ���":
                    this.tvList.ShowStockPlanList(this.privDept, "3");
                    this.IsShowList = true;
                    break;
                case "����":
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
                case "��ӡ":
                    break;
                case "����":
                    this.Export();
                    break;
                case "�˳�":
                    frmInPlan tempFrm = new frmInPlan();
                    tempFrm.Close();
                    break;

                case "δ���":
                    this.tvList.ShowStockPlanList(this.privDept, "3");
                    this.IsShowList = true;
                    break;
                case "�����":
                    this.tvList.ShowStockPlanList(this.privDept, "5");
                    this.IsShowList = true;
                    break;
                case "��ʾ��":
                    this.ucMaterialItemList1.Visible = !this.ucMaterialItemList1.Visible;
                    break;
            }
            */
        }

        private void ucInPlan_Load(object sender, System.EventArgs e)
        {
            this.InitDataTable();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                Neusoft.NFC.Object.NeuObject testPrivDept = new Neusoft.NFC.Object.NeuObject();
                int parma = Neusoft.UFC.Common.Classes.Function.ChoosePivDept("0511", ref testPrivDept);

                if (parma == -1)            //��Ȩ��
                {
                    MessageBox.Show("���޴˴��ڲ���Ȩ��");
                    return;
                }
                else if (parma == 0)       //�û�ѡ��ȡ��
                {
                    return;
                }

                this.privDept = testPrivDept;

                Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
                Application.DoEvents();

                this.InitData();

                this.InitPlanList();

                Neusoft.NFC.Interface.Classes.Function.HideWaitForm();

            }
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node != null && e.Node.Parent != null)
            {
                Neusoft.NFC.Object.NeuObject inPlanObj = e.Node.Tag as Neusoft.NFC.Object.NeuObject;

                this.nowBillNO = inPlanObj.ID;
                this.comId = inPlanObj.Name;

                this.ShowPlanData(this.privDept.ID, inPlanObj.ID);
            }
        }

        private void tvList_DoubleClick(object sender, EventArgs e)
        {
            if (this.tvList.SelectedNode != null && this.tvList.SelectedNode.Parent != null)
            {
                Neusoft.NFC.Object.NeuObject inPlanObj = this.tvList.SelectedNode.Tag as Neusoft.NFC.Object.NeuObject;

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
                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColStockNum)
                {
                    decimal stockQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColStockNum].Text);
                    decimal planPrice = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPurchasePrice].Text);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColStockCost].Value = stockQty * planPrice;

                    this.SetSum();
                }

                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColStockNum)
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

                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColCompany)
                {
                    Neusoft.HISFC.Management.Material.ComCompany company = new Neusoft.HISFC.Management.Material.ComCompany();
                    ArrayList alCompany = company.QueryCompany("1", "A");
                    Neusoft.NFC.Object.NeuObject infoCompany = new Neusoft.NFC.Object.NeuObject();
                    Neusoft.NFC.Interface.Classes.Function.ChooseItem(alCompany, ref infoCompany);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColCompany].Value = infoCompany.Name;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColCompanyID].Value = infoCompany.ID;
                    this.neuSpread1_Sheet1.ActiveColumnIndex++;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (activeRow < 0)
                return;

            string itemCode = sv.Cells[activeRow, 0].Text;

            Neusoft.HISFC.Object.Material.MaterialItem item = this.itemManager.QueryMetItemAllByID(itemCode);
            if (item == null)
            {
                MessageBox.Show(this.itemManager.Err);
            }

            if (this.AddDrugData(item) == 1)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                this.SetFocus(true);
            }
        }

        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == (int)ColumnSet.ColCompany)
            {
                bool ifCheck;
                Neusoft.HISFC.Management.Material.ComCompany company = new Neusoft.HISFC.Management.Material.ComCompany();
                ArrayList alCompany = company.QueryCompany("1", "A");
                Neusoft.NFC.Object.NeuObject infoCompany = new Neusoft.NFC.Object.NeuObject();
                Neusoft.NFC.Interface.Classes.Function.ChooseItem(alCompany, ref infoCompany);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColCompany].Value = infoCompany.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColCompanyID].Value = infoCompany.ID;
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    ifCheck = Neusoft.NFC.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value.ToString());
                    if (ifCheck)
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCompany].Value = infoCompany.Name;
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCompanyID].Value = infoCompany.ID;
                    }

                }
            }
            if (e.Column == (int)ColumnSet.ColProduce)
            {
                Neusoft.HISFC.Management.Material.ComCompany company = new Neusoft.HISFC.Management.Material.ComCompany();
                ArrayList alCompany = company.QueryCompany("0", "A");
                Neusoft.NFC.Object.NeuObject infoCompany = new Neusoft.NFC.Object.NeuObject();
                Neusoft.NFC.Interface.Classes.Function.ChooseItem(alCompany, ref infoCompany);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColProduce].Value = infoCompany.Name;
                //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex,(int)ColumnSet.ColProduceID].Value = infoCompany.ID;	
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// �Ƿ�ѡ��
            /// </summary>
            isCheck,
            /// <summary>
            /// ��Ʒ���
            /// </summary>
            ColKind,
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
            /// �ɹ�����
            /// </summary>
            ColStockNum,
            /// <summary>
            /// �ɹ����
            /// </summary>
            ColStockCost,
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
            /// ������˾
            /// </summary>
            ColCompany,
            /// <summary>
            /// ��Ʊ��liuxq add
            /// </summary> 
            ColInvoiceNo,
            /// <summary>
            /// ����˾����
            /// </summary>
            ColCompanyID,
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
            ColUserCode

        }
    }
}
