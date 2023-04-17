using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Material;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material.Check
{
    public partial class ucCheckManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucCheckManager()
        {
            InitializeComponent();
        }

        #region ��
        //����DataSet
        //private DataSet myDataSet = new DataSet();
        private DataTable myDataTable = new DataTable();

        //private DataView myDataView;

        //Item������
        private FS.HISFC.BizLogic.Material.MetItem myItem = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// store������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store storeMgr = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// ComCompany������
        /// </summary>
        private FS.HISFC.BizLogic.Material.ComCompany companyMgr = new FS.HISFC.BizLogic.Material.ComCompany();

        //�洢�����ɵ��̵㵥��
        //private string newCheckCode = "";

        /// <summary>
        /// �Ƿ�����˫��ɾ��
        /// </summary>
        private bool allowDel = false;

        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        private bool allowEdit = true;

        /// <summary>
        /// �Ƿ񴰿��̵�
        /// </summary>
        private bool isWindowCheck = false;

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private System.Collections.Hashtable hsItem = new Hashtable();

        /// <summary>
        /// ��ǰ�����Ŀ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private string nowOperCheckNO = "";

        /// <summary>
        /// ������˾������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper companyHelper = null;

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper producerHelper = null;

        /// <summary>
        /// ӯ��״̬������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper profitHelper = null;

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ��·��ʵ��̵㵥
        /// </summary>
        //public bool IsNewCheckCode
        //{
        //    set
        //    {
        //        if (value)
        //            newCheckCode = "";
        //    }
        //}

        /// <summary>
        /// �Ƿ�����˫��ɾ��
        /// </summary>
        public bool AllowDel
        {
            get
            {
                return allowDel;
            }
            set
            {
                this.allowDel = value;
            }
        }
        /// <summary>
        /// �Ƿ�������̵��������б༭
        /// </summary>
        public bool AllowEdit
        {
            get
            {
                return allowEdit;
            }
            set
            {
                allowEdit = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string LebelTitle
        {
            set
            {
                this.lbTitle.Text = value;
            }
        }
        /// <summary>
        /// �Ƿ񴰿��̵�
        /// </summary>
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
        /// �Ƿ���ʾ�̵㵥�б�
        /// </summary>
        [Description("�Ƿ���ʾ�̵㵥�б�"), Category("����"), DefaultValue(false)]
        public bool IsShowCheckList
        {
            get
            {
                return this.ucMaterialItemList1.ShowTreeView;
            }
            set
            {
                this.ucMaterialItemList1.ShowTreeView = value;

                this.SetToolButton(value);
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��    ��", "��¼��ǰ����γ��̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolBarService.AddToolButton("��������", "��������γ��̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);
            //toolBarService.AddToolButton("�̵�ģ��", "����ģ���γ��̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null);
            toolBarService.AddToolButton("��ʷ�̵�", "������ʷ�̵��¼ ", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            //toolBarService.AddToolButton("�̵㸽��", "����̵㸽��ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.C����, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "��ʾ�̵㵥�б�", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            /// {9ADAD904-B8B5-4f94-88A9-AF690A98D1BF}
            //toolBarService.AddToolButton("��������", "���������̵���Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.O����, true, false, null);
            toolBarService.AddToolButton("ɾ    ��", "ɾ����ǰѡ��ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("ȫ    ��", "���ݵ�ǰ���ʿ������̵���", FS.FrameWork.WinForms.Classes.EnumImageList.J���, true, false, null);
            toolBarService.AddToolButton("���", "�����̵�����µ�ǰ��� ����ӯ��", FS.FrameWork.WinForms.Classes.EnumImageList.P�̵�����, true, false, null);
            toolBarService.AddToolButton("���", "���ϵ�ǰ�̵㵥", FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "���")
            {
                this.CheckCStore(this.privDept.ID, this.nowOperCheckNO);

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
            ///{B954FF22-CCDB-4d58-B233-9FFD0EC95410}
            if (e.ClickedItem.Text == "��������")
            {
                this.GroupCheckCloseType();
            }

            //{9ADAD904-B8B5-4f94-88A9-AF690A98D1BF}
            //if (e.ClickedItem.Text == "��������")
            //{
            //    this.AddSave(this.privDept.ID, this.nowOperCheckNO);

            //    if (this.tvList.Nodes.Count > 0)
            //        this.tvList.SelectedNode = this.tvList.Nodes[0];
            //}
            ///{E10FCFDC-BE18-40c5-B357-5B4A347B78BE}
            if (e.ClickedItem.Text == "ȫ    ��")
            {
                this.FstoreSetAStore();
            }

            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.ClearData();

                if (!this.IsShowCheckList)
                {
                    this.IsShowCheckList = true;
                }
            }
            if (e.ClickedItem.Text == "��    ��")
            {
                this.CheckClose();
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPreview(40, 10, this.neuPanel2);

            return base.OnPrint(sender, neuObject);
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
            //this.toolBarService.SetToolButtonEnabled("�̵㸽��", isShowList);
            this.toolBarService.SetToolButtonEnabled("��    ��", isShowList);
            ///{9ADAD904-B8B5-4f94-88A9-AF690A98D1BF}
            //this.toolBarService.SetToolButtonEnabled("��������", isShowList);
            this.toolBarService.SetToolButtonEnabled("ɾ    ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��������", !isShowList);
            //this.toolBarService.SetToolButtonEnabled("�̵�ģ��", !isShowList);
            //this.toolBarService.SetToolButtonEnabled("���ͷ���", !isShowList);
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
            this.ucMaterialItemList1.TreeView = this.tvList;

            this.tvList.AfterSelect -= new TreeViewEventHandler(tvList_AfterSelect);
            this.tvList.AfterSelect += new TreeViewEventHandler(tvList_AfterSelect);

            this.ucMaterialItemList1.Caption = "�̵㵥�б�";

            this.ShowCheckList();

            this.ucMaterialItemList1.ShowTreeView = true;
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

        //{B954FF22-CCDB-4d58-B233-9FFD0EC95410}
        #region ��������

        /// <summary>
        /// ��������
        /// </summary>
        protected virtual void GroupCheckCloseType()
        {
            //�ж��Ƿ����������������
            if (this.JudgeContinue() == -1)
            {
                return;
            }

            this.ClearData();

            //����ѡ�����ʿ�Ŀ����
            FS.HISFC.Components.Material.Check.ucTypeOrQualityChoose uc = new ucTypeOrQualityChoose(true);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

            switch (uc.ResultFlag)
            {
                case "0":                    //ȡ��
                    break;
                case "1":                   //���ʿ�Ŀ
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڶԿ�����ʽ��з��� ���Ժ�...");
                    Application.DoEvents();

                    this.CheckCloseByType(this.privDept.ID, uc.KindType, false, uc.IsCheckZeroStock, uc.IsCheckStopMaterial);

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    break;
                case "2":                  //ȫ��ҩƷ����
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڶԿ�����ʽ��з��� ���Ժ�...");
                    Application.DoEvents();

                    this.CheckCloseByTotal(this.privDept.ID, false, uc.IsCheckZeroStock, uc.IsCheckStopMaterial);

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    break;
            }
        }

        #endregion

        # region ����

        /// <summary>
        /// ��ʼ��DataSet
        /// </summary>
        private void InitDataTable()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.myDataTable.Columns.AddRange(new DataColumn[] {
																	new DataColumn("�̵���ˮ��",  dtStr),//0
																	new DataColumn("�̵㵥��",      dtStr),//1
																	new DataColumn("�ֿ����",	  dtStr),//2
																	new DataColumn("��Ʒ����",	  dtStr),//3
																	new DataColumn("��Ʒ����",	  dtStr),//4
																	new DataColumn("���۽��",        dtDec),//5
																	new DataColumn("������",	  dtStr),//6
																	new DataColumn("���",		  dtStr),//7
																	new DataColumn("��λ���",	      dtStr),//8
																	new DataColumn("�����Ч��",    dtStr),//9
																	new DataColumn("������˾",	  dtStr),//10
																	new DataColumn("��������",    dtStr),//11
																	new DataColumn("���ʿ������",    dtDec),//12
																	new DataColumn("ʵ���̴�����",    dtDec),//13
																	new DataColumn("���������",    dtDec),//14
                                                                    new DataColumn("ӯ������",    dtDec),//15
                                                                    new DataColumn("������λ",        dtStr),//16
																	new DataColumn("ӯ�����",    dtStr),//17
																	new DataColumn("�̵�״̬",    dtStr),//18
																	new DataColumn("����Ա",    dtStr),//19
																	new DataColumn("��������",      dtStr),//20
																	new DataColumn("ƴ����",	  dtStr),//21
																	new DataColumn("�����",	  dtStr),//22
																	new DataColumn("�Զ�����",	  dtStr) //23

																	
			});
            this.myDataTable.DefaultView.AllowNew = true;
            this.myDataTable.DefaultView.AllowEdit = true;
            this.myDataTable.DefaultView.AllowDelete = true;

            //this.myDataSet.Tables.Add(this.myDataTable);
            //this.myDataView = new DataView(this.myDataTable);

            //�趨���ڶ�DataView�����ظ��м���������
            DataColumn[] keys = new DataColumn[3];
            //keys[0] = this.myDataTable.Columns["�̵㵥��"];
            keys[0] = this.myDataTable.Columns["�ֿ����"];
            keys[1] = this.myDataTable.Columns["��Ʒ����"];
            keys[2] = this.myDataTable.Columns["���۽��"];
            this.myDataTable.PrimaryKey = keys;

            this.neuSpread1_Sheet1.DataSource = this.myDataTable.DefaultView;

            this.SetFormat();
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void InitData()
        {
            List<FS.HISFC.Models.Material.MaterialItem> alItem = this.myItem.GetMetItemList();
            if (alItem == null)
            {
                MessageBox.Show("�������ʻ�����Ϣ��������");
                return;
            }
            foreach (FS.HISFC.Models.Material.MaterialItem info in alItem)
            {
                this.hsItem.Add(info.ID, info);
            }
            this.ucMaterialItemList1.ShowDeptStorage(this.privDept.ID, false, false);
        }

        /// <summary>
        /// ��ʽ��FarPoint
        /// </summary>
        public void SetFormat()
        {
            //FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            //���λس���
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //���ɱ༭
            FarPoint.Win.Spread.CellType.TextCellType cReadOnlyType = new FarPoint.Win.Spread.CellType.TextCellType();
            cReadOnlyType.ReadOnly = true;
            //�ɱ༭
            FarPoint.Win.Spread.CellType.TextCellType cWriteType = new FarPoint.Win.Spread.CellType.TextCellType();
            cWriteType.ReadOnly = false;
            //�Ƿ�������̵��������б༭
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //�û���Ŀؼ�
            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType cEditType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            cEditType.MinimumValue = 0;
            //FarPoint.Win.Spread.CellType.NumberCellType cEditType = new FarPoint.Win.Spread.CellType.NumberCellType();
            if (this.allowEdit)
            {
                cEditType.ReadOnly = false;
            }
            else
            {
                cEditType.ReadOnly = true;
            }

            /// <summary>
            /// �̵���ˮ��CheckNo	0
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckNo].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckNo].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckNo].Width = 70F;

            /// <summary>
            /// �̵㵥��CheckCode	1
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckCode].Width = 70F;

            /// <summary>
            /// �ֿ����StorageCode	2
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StorageCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StorageCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StorageCode].Width = 70F;

            /// <summary>
            /// ��Ʒ����ItemCode	3
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ItemCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ItemCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ItemCode].Width = 70F;

            /// <summary>
            /// ��Ʒ����ItemName	4
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ItemName].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ItemName].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ItemName].Width = 70F;

            /// <summary>
            /// ���۽��SaleCost	5
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.SaleCost].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.SaleCost].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.SaleCost].Width = 70F;

            /// <summary>
            /// �����ţ��������̵㣺ALL StockNo	6	
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StockNo].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StockNo].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StockNo].Width = 70F;

            /// <summary>
            /// ���Specs		7
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Specs].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Specs].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Specs].Width = 70F;

            /// <summary>
            /// ��λ���PlaceCode	8
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PlaceCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PlaceCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.PlaceCode].Width = 70F;

            /// <summary>
            /// �����Ч��ValidDate	9
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ValidDate].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ValidDate].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ValidDate].Width = 70F;

            /// <summary>
            /// ������˾Company	10
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Company].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Company].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Company].Width = 70F;

            /// <summary>
            /// ��������Factory	11
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Factory].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Factory].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.Factory].Width = 70F;

            /// <summary>
            /// ���ʿ������(��С��λ)FstoreNum	12
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.FstoreNum].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.FstoreNum].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.FstoreNum].Width = 80F;

            /// <summary>
            /// ʵ���̴�����(��С��λ)AdjustNum	13
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.AdjustNum].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.AdjustNum].CellType = cEditType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.AdjustNum].Width = 80F;

            /// <summary>
            /// ���������(��С��λ)CstoreNum	14
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CstoreNum].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CstoreNum].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CstoreNum].Width = 70F;

            /// <summary>
            /// ӯ������ProfitLossNum	15
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ProfitLossNum].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ProfitLossNum].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ProfitLossNum].Width = 70F;

            /// <summary>
            /// ������λStatUnit	16
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StatUnit].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StatUnit].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.StatUnit].Width = 70F;

            /// <summary>
            /// ӯ�����(0�̿���1��ӯ��2��ӯ��)ProfitFlag	17
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ProfitFlag].Visible = true;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ProfitFlag].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ProfitFlag].Width = 70F;

            /// <summary>
            /// �̵�״̬(0���ʣ�1��棻2ȡ��)CheckState	18
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckState].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckState].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.CheckState].Width = 70F;

            /// <summary>
            /// ����ԱOperCode      19
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.OperCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.OperCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.OperCode].Width = 70F;

            /// <summary>
            /// ��������OperDate	20
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.OperDate].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.OperDate].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.OperDate].Width = 70F;

            /// <summary>
            /// ƴ����SpellCode		21
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.SpellCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.SpellCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.SpellCode].Width = 70F;

            /// <summary>
            /// �����WBCode		22
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.WBCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.WBCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.WBCode].Width = 70F;

            /// <summary>
            /// �Զ�����UserCode    23
            /// </summary>
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.UserCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.UserCode].CellType = cReadOnlyType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.UserCode].Width = 70F;
        }

        ///{E10FCFDC-BE18-40c5-B357-5B4A347B78BE}
        /// <summary>
        /// ���ݷ��ʿ������̵���
        /// </summary>
        protected void FstoreSetAStore()
        {
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            foreach (DataRow dr in this.myDataTable.Rows)
            {
                dr["ʵ���̴�����"] = dr["���ʿ������"];
            }

            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
        }

        #region ���ʼ�������/���ݲ���


        /// <summary>
        /// ����
        /// </summary>
        protected void CheckClose()
        {
            this.ClearData();

            this.IsShowCheckList = false;

            this.ucMaterialItemList1.SetFocusSelect();
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        private void initHelper()
        {
            if (this.companyHelper == null)
            {
                ArrayList alCompany = this.companyMgr.QueryCompany("1", "1");
                this.companyHelper = new FS.FrameWork.Public.ObjectHelper(alCompany);
            }
            if (this.producerHelper == null)
            {
                ArrayList alProducer = this.companyMgr.QueryCompany("0", "1");
                this.producerHelper = new FS.FrameWork.Public.ObjectHelper(alProducer);
            }
            if (this.profitHelper == null)
            {
                ArrayList alPorfit = new ArrayList();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = "0";
                obj.Name = "�̿�";
                alPorfit.Add(obj);
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = "1";
                obj.Name = "��ӯ";
                alPorfit.Add(obj);
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = "2";
                obj.Name = "��ӯ��";
                alPorfit.Add(obj);
                this.profitHelper = new FS.FrameWork.Public.ObjectHelper(alPorfit);
            }
        }

        /// <summary>
        /// �ֹ����һ����Ʒ�����̵�
        /// </summary>
        /// <param name="checkInfo"></param>
        private void AddData(FS.HISFC.Models.Material.Check checkInfo)
        {
            try
            {
                if (checkInfo == null)
                {
                    MessageBox.Show("δ�ҵ���Ч�̵���Ϣ");
                    return;
                }
                this.myDataTable.Rows.Add(new object[]{
                                                        checkInfo.ID,                                                             //�̵���ˮ��
                                                        checkInfo.CheckCode,                                                      //�̵㵥�� 
                                                        checkInfo.StoreHead.StoreBase.StockDept.ID,                               //�ֿ����	
                                                        checkInfo.StoreHead.StoreBase.Item.ID,                                    //��Ʒ����	
                                                        checkInfo.StoreHead.StoreBase.Item.Name,                                  //��Ʒ����	
                                                        checkInfo.StoreHead.StoreBase.AvgSalePrice,                               //���۽�� 
                                                        checkInfo.StoreHead.StoreBase.StockNO,                                    //������	
                                                        checkInfo.StoreHead.StoreBase.Item.Specs,                                 //���	  
                                                        checkInfo.StoreHead.StoreBase.PlaceNO,                                    //��λ���
                                                        checkInfo.StoreHead.StoreBase.ValidTime,                                  //�����Ч��
                                                        this.companyHelper.GetName(checkInfo.StoreHead.StoreBase.Company.ID),     //������˾	
                                                        this.producerHelper.GetName(checkInfo.StoreHead.StoreBase.Producer.ID),   //�������� 
                                                        checkInfo.FStoreNum,                                                      //���ʿ������
                                                        checkInfo.AdjustNum,                                                      //ʵ���̴�����
                                                        checkInfo.CStoreNum,                                                      //���������
                                                        checkInfo.ProfitLossNum,                                                  //ӯ������                                                                   
                                                        checkInfo.StoreHead.StoreBase.Item.MinUnit,                               //������λ                                                                   
                                                        this.profitHelper.GetName(checkInfo.ProfitFlag),                          //ӯ�����
                                                        checkInfo.CheckState,                                                     //�̵�״̬
                                                        checkInfo.Oper.ID,                                                        //����Ա
                                                        checkInfo.Oper.OperTime,                                                  //��������
                                                        checkInfo.StoreHead.StoreBase.Item.SpellCode,                             //ƴ����
                                                        checkInfo.StoreHead.StoreBase.Item.WbCode,                                //�����
                                                        checkInfo.StoreHead.StoreBase.Item.UserCode                               //�Զ�����
													  }
                                       );
                this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.RowCount - 1, 0);
            }
            catch (ConstraintException cex)
            {
                MessageBox.Show("��������Ѵ��ڣ������ظ����");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("����̵���Ϣʧ�ܣ�" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// �ֹ����һ����Ʒ�����̵�
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="batchNo">���ţ��粻��������Ϊall</param>
        /// <param name="isBatch">�Ƿ����Ź���</param>
        public void AddData(string deptCode, string itemCode, string checkCode, DateTime dateBegin, DateTime dateEnd, string batchNo, string placeCode, bool isBatch)
        {

            FS.HISFC.Models.Material.Check check;
            try
            {
                //�����Ƿ����ظ����
                String[] tempFind = new string[3];
                tempFind[0] = itemCode;			//����
                tempFind[1] = placeCode;	//��λ��
                tempFind[2] = batchNo;			//����
                DataRow findRow = this.myDataTable.Rows.Find(tempFind);
                if (findRow != null)
                {
                    MessageBox.Show("��������Ѵ��ڣ������ظ����");
                    return;
                }

                check = this.myItem.CheckCloseByDrug(deptCode, itemCode, checkCode, dateBegin, dateEnd, batchNo, isBatch);
                if (check == null)
                {
                    MessageBox.Show("������ʷ���ʧ��" + this.myItem.Err);
                    return;
                }

                if (check.ID == null || check.ID == "")
                {
                    //check.PackNum = FS.FrameWork.Function.NConvert.ToDecimal(Math.Floor(Convert.ToDouble(check.FStoreNum / check.Item.PackQty)));
                    //check.MinNum = check.FStoreNum - check.PackNum * check.Item.PackQty;
                    //check.AdjustNum = check.FStoreNum;
                }

                //������ݽ���farpoint
                this.myDataTable.Rows.Add(new object[]{
                                                          //check.ID,									//0 �̵���ˮ��
                                                          //check.PlaceCode,							//1 ��λ��
                                                          //check.Item.ID,							//2 ҩƷ����
                                                          //check.Item.UserCode,						//3  �Զ�����
                                                          //check.Item.Name,							//4 ҩƷ����
                                                          //check.Item.Specs,							//5 ���
                                                          //check.Item.PackQty,						//6 ��װ����
                                                          //check.BatchNo,							//7 ����
                                                          //check.Item.UnitPrice,                     //8�۸�
                                                          //check.Item.MinUnit,						//9 ��λ
                                                          //check.Item.PackUnit,						//10 ��װ��λ
                                                          //check.Item.MinUnit,						//11 ��С��λ
                                                          //check.LastNum,							//12 
                                                          //check.InNum,								//13
                                                          //check.OutNum,								//14 
                                                          //check.InMoney,							//15 
                                                          //check.OutMoney,							//16 
                                                          //check.FStoreNum,							//17 ���ʿ��
                                                          //check.FstoreMoney,						//18 
                                                          //check.AdjustNum,							//19 �̵�����
                                                          //check.ValidDate,							//20 ��Ч��
                                                          //Math.Round(check.AdjustNum * check.Item.UnitPrice,4),//21�̵���
                                                          //check.Item.Factory.Name,					//22 ��������
                                                          //check.Item.Factory.ID,					//23 ���ű���
                                                          //check.Item.SpellCode,					//24 ƴ����
                                                          //check.Item.WBCode						//25 �����
													  }
                    );
                this.SetFormat();
                this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.RowCount - 1, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        ///{B954FF22-CCDB-4d58-B233-9FFD0EC95410}
        /// <summary>
        /// �������ʿ�Ŀֱ�ӽ����̵����
        /// </summary>
        /// <param name="deptNO">�ⷿ����</param>
        /// <param name="kindType">���ʿ�Ŀ</param>
        /// <param name="isCheckZeroStock"></param>
        /// <param name="isCheckStopDrug"></param>
        /// <returns></returns>
        protected int CheckCloseByType(string deptNO, string kindType, bool isBatch, bool isCheckZeroStock, bool isCheckStopMaterial)
        {
            //���ԭ����
            this.ClearData();

            try
            {   //�������ʿ�Ŀ���з���
                ArrayList alDetail = this.myItem.CheckCloseByKind(deptNO, kindType, isBatch, isCheckZeroStock, isCheckStopMaterial);
                if (alDetail == null)
                {
                    MessageBox.Show(Language.Msg("����ҩƷ���/���ʽ�����������ʧ��" + this.myItem.Err));
                    return -1;
                }

                if (alDetail.Count == 0)
                {
                    MessageBox.Show(Language.Msg("��ѡ�������޿��ҩƷ" + this.myItem.Err));
                    return -1;
                }
                //�ж��Ƿ���δ�����̵㵥�а�����Ŀ������Щ��Ŀ���й���{B954FF22-CCDB-4d58-B233-9FFD0EC95410}
                ArrayList alCheck = new ArrayList();
                foreach (FS.HISFC.Models.Material.Check checkTemp in alDetail)
                {
                    int iReturn = this.IsUnchecked(checkTemp.StoreHead.StoreBase.Item.ID, checkTemp.StoreHead.StoreBase.AvgSalePrice);
                    if (iReturn == 0)
                    {
                        alCheck.Add(checkTemp);
                        continue;
                    }
                }
                //------------------------
                this.ShowCheckDetail(alCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �Ա��ⷿ�������ʽ��з��ʴ���{B954FF22-CCDB-4d58-B233-9FFD0EC95410}
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="isBatch">�Ƿ����Ź���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int CheckCloseByTotal(string deptCode, bool isBatch, bool isCheckZeroStock, bool isCheckStopStock)
        {
            //���ԭ����
            this.ClearData();
            ArrayList al = new ArrayList();
            try
            {		//������ҩƷ���з��ʴ���
                al = this.myItem.CheckCloseByTotal(deptCode, isBatch, isCheckZeroStock, isCheckStopStock);
                if (al == null)
                {
                    MessageBox.Show(this.myItem.Err);
                    return -1;
                }
                if (al.Count == 0)
                {
                    MessageBox.Show("��ѡ�������޿��ҩƷ" + this.myItem.Err);
                    return -1;
                }
                //�ж��Ƿ���δ�����̵㵥�а�����Ŀ������Щ��Ŀ���й���{B954FF22-CCDB-4d58-B233-9FFD0EC95410}
                ArrayList alCheck = new ArrayList();                
                foreach (FS.HISFC.Models.Material.Check checkTemp in al)
                {
                    int iReturn = this.IsUnchecked(checkTemp.StoreHead.StoreBase.Item.ID, checkTemp.StoreHead.StoreBase.AvgSalePrice);
                    if (iReturn == 0)
                    {
                        alCheck.Add(checkTemp);
                        continue;
                    }
                }
                //------------------------
                //��FarPoint����ʾ��ϸ
                this.ShowCheckDetail(alCheck);
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ʧ�ܣ�" + ex.Message);
                return -1;
            }
            return 1;
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
            result = MessageBox.Show("ȷ��ɾ����ǰ��¼?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return;
            }
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
        }

        #endregion

        /// <summary>
        /// ��ȡ��ǰ�����仯�����ݣ�����Check��̬����
        /// </summary>
        /// <param name="flag">������־Modify(���ӡ�����)��Del(ɾ��)��All(����)</param>
        /// <returns>�ɹ����ط����䶯�����顢ʧ�ܷ���null</returns>
        private ArrayList GetUpdateInfo(string flag)
        {
            this.neuSpread1.StopCellEditing();
            foreach (DataRow dr in this.myDataTable.Rows)
            {
                dr.EndEdit();
            }

            ArrayList al = new ArrayList();
            DataTable dataChange = new DataTable();
            switch (flag)
            {
                case "Modify":		//��ȡ�䶯(���ӡ��޸�)����
                    dataChange = this.myDataTable.GetChanges(DataRowState.Modified | DataRowState.Added);
                    break;
                case "Del":			//��ȡɾ������
                    dataChange = this.myDataTable.GetChanges(DataRowState.Deleted);
                    break;
                case "All":         //�����������
                    dataChange = this.myDataTable;
                    break;
                default:
                    return null;
            }
            //�ޱ䶯����
            if (dataChange == null)
                return null;
            //����ɾ�����ݣ��ع��仯
            if (flag == "Del")
            {
                dataChange.RejectChanges();
            }

            try
            {
                //������ݼ���Check�̵�ʵ��
                foreach (DataRow row in dataChange.Rows)
                {
                    try
                    {

                        //��FarPoint�ڻ������
                        FS.HISFC.Models.Material.Check checkInfo = new FS.HISFC.Models.Material.Check();

                        checkInfo.ID = row["�̵���ˮ��"].ToString();	                                                    		//�̵���ˮ��      
                        checkInfo.CheckCode = row["�̵㵥��"].ToString();                                                           //�̵㵥�� 
                        checkInfo.StoreHead.StoreBase.StockDept.ID = row["�ֿ����"].ToString();                                    //�ֿ����	
                        checkInfo.StoreHead.StoreBase.Item.ID = row["��Ʒ����"].ToString();                                         //��Ʒ����	
                        checkInfo.StoreHead.StoreBase.Item.Name = row["��Ʒ����"].ToString();                                       //��Ʒ����	
                        checkInfo.StoreHead.StoreBase.AvgSalePrice = FrameWork.Function.NConvert.ToDecimal(row["���۽��"].ToString());   //���۽�� 
                        checkInfo.StoreHead.StoreBase.StockNO = row["������"].ToString();                                         //������	
                        checkInfo.StoreHead.StoreBase.Item.Specs = row["���"].ToString();                                          //���	  
                        checkInfo.StoreHead.StoreBase.PlaceNO = row["��λ���"].ToString();                                         //��λ���
                        checkInfo.StoreHead.StoreBase.ValidTime = FrameWork.Function.NConvert.ToDateTime(row["�����Ч��"].ToString());   //�����Ч��
                        checkInfo.StoreHead.StoreBase.Company.ID = this.companyHelper.GetID(row["������˾"].ToString());            //������˾	
                        checkInfo.StoreHead.StoreBase.Producer.ID = this.producerHelper.GetID(row["��������"].ToString());          //�������� 
                        checkInfo.FStoreNum = FrameWork.Function.NConvert.ToDecimal(row["���ʿ������"].ToString());                      //���ʿ������
                        checkInfo.AdjustNum = FrameWork.Function.NConvert.ToDecimal(row["ʵ���̴�����"].ToString());                      //ʵ���̴�����
                        checkInfo.CStoreNum = FrameWork.Function.NConvert.ToDecimal(row["���������"].ToString());                      //���������
                        //checkInfo.ProfitLossNum = FrameWork.Function.NConvert.ToDecimal(row["ӯ������"].ToString());                      //ӯ������        
                        checkInfo.ProfitLossNum = Math.Abs(checkInfo.FStoreNum - checkInfo.AdjustNum);                              //ӯ������ 
                        checkInfo.StoreHead.StoreBase.Item.MinUnit = row["������λ"].ToString();                                    //������λ
                        //checkInfo.ProfitFlag = this.profitHelper.GetID(row["ӯ�����"].ToString());                                 //ӯ�����
                        if (checkInfo.FStoreNum > checkInfo.AdjustNum)//�̿�
                        {
                            checkInfo.ProfitFlag = "0";
                        }
                        else if (checkInfo.FStoreNum < checkInfo.AdjustNum)//��ӯ
                        {
                            checkInfo.ProfitFlag = "1";
                        }
                        else
                        {
                            checkInfo.ProfitFlag = "2";
                        }
                        checkInfo.CheckState = row["�̵�״̬"].ToString();                                                          //�̵�״̬
                        //checkInfo.Oper.ID = row["����Ա"].ToString();                                                             
                        checkInfo.Oper.ID = this.myItem.Operator.ID;                                                                //����Ա     
                        //checkInfo.Oper.OperTime = FrameWork.Function.NConvert.ToDateTime(row["��������"].ToString());                     
                        checkInfo.Oper.OperTime = this.myItem.GetDateTimeFromSysDateTime();                                         //��������  
                        checkInfo.StoreHead.StoreBase.Item.SpellCode = row["ƴ����"].ToString();                                    //ƴ����
                        checkInfo.StoreHead.StoreBase.Item.WbCode = row["�����"].ToString();                                       //�����
                        checkInfo.StoreHead.StoreBase.Item.UserCode = row["�Զ�����"].ToString();                                   //�Զ�����
                        if (checkInfo.ProfitFlag == "0")    //ӯ�����
                        {
                            checkInfo.CheckLossCost = checkInfo.ProfitLossNum * checkInfo.StoreHead.StoreBase.AvgSalePrice;
                        }
                        else if (checkInfo.ProfitFlag == "1")
                        {
                            checkInfo.CheckProfitCost = checkInfo.ProfitLossNum * checkInfo.StoreHead.StoreBase.AvgSalePrice;
                        }
                        //����info���붯̬����
                        al.Add(checkInfo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(row["��Ʒ����"].ToString() + row["���"].ToString() + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n ");
            }
            return al;
        }

        /// <summary>
        /// ��ʾ�̵���ϸ��Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="checkCode">�̵㵥��</param>
        public void ShowCheckDetail(string deptCode, string checkCode)
        {
            ArrayList al = new ArrayList();

            al = this.myItem.GetCheckDetailByCheckCode(deptCode, checkCode);
            if (al == null)
            {
                MessageBox.Show(this.myItem.Err);
                return;
            }
            this.ShowCheckDetail(al);
            //�ύ�仯
            this.myDataTable.AcceptChanges();
            //��ʽ��FaoPoint
            this.SetFormat();
        }

        /// <summary>
        /// ���FarPoint
        /// </summary>
        /// <param name="al">check��̬����</param>
        public void ShowCheckDetail(ArrayList al)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����̵���ϸ��Ϣ...");
            Application.DoEvents();
            try
            {
                FS.HISFC.Models.Material.Check check;

                //�������ȡ���� ��ô�ٶȻ����
                //				this.neuSpread1_Sheet1.DataSource = null;

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();

                for (int i = 0; i < al.Count; i++)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, al.Count);
                    Application.DoEvents();

                    check = al[i] as FS.HISFC.Models.Material.Check;

                    //��ȡҩƷ��Ϣ
                    FS.HISFC.Models.Material.MaterialItem item = new MaterialItem();
                    if (this.hsItem.ContainsKey(check.StoreHead.StoreBase.Item.ID))
                    {
                        item = this.hsItem[check.StoreHead.StoreBase.Item.ID] as FS.HISFC.Models.Material.MaterialItem;
                    }
                    else
                    {
                        item = this.myItem.GetMetItemByMetID(check.StoreHead.StoreBase.Item.ID);
                        if (item == null)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("���ػ�����Ϣʱ����" + this.myItem.Err);
                            return;
                        }
                    }
                    check.StoreHead.StoreBase.Item = item;
                    this.AddData(check);
                    //if (this.isWindowCheck)
                    //{
                    //    //check.MinNum = 0;
                    //    //check.PackNum = 0;
                    //}

                    //if (check.ID == null || check.ID == "")
                    //{
                    //    //check.PackNum = FS.FrameWork.Function.NConvert.ToDecimal(Math.Floor(Convert.ToDouble(check.FStoreNum / check.Item.PackQty)));
                    //    //check.MinNum = check.FStoreNum - check.PackNum * check.Item.PackQty;
                    //    //check.AdjustNum = check.FStoreNum;
                    //}

                    //this.myDataTable.Rows.Add(new object[]{
                    //                                          //check.ID,									//0 �̵���ˮ��
                    //                                          //check.PlaceCode,							//1 ��λ��
                    //                                          //check.Item.ID,							//2 ҩƷ����
                    //                                          //check.Item.UserCode,						//3  �Զ�����
                    //                                          //check.Item.Name,							//4 ҩƷ����
                    //                                          //check.Item.Specs,							//5 ���
                    //                                          //check.Item.PackQty,						//6 ��װ����
                    //                                          //check.BatchNo,							//7 ����
                    //                                          //check.Item.Price,                         //8 �۸�
                    //                                          //check.Item.MinUnit,						//9 ��λ
                    //                                          //check.Item.PackUnit,						//10 ��װ��λ
                    //                                          //check.Item.MinUnit,						//11 ��С��λ
                    //                                          //check.LastNum,							//12 
                    //                                          //check.InNum,								//13
                    //                                          //check.OutNum,								//14 
                    //                                          //check.InMoney,							//15 
                    //                                          //check.OutMoney,							//16 
                    //                                          //check.FStoreNum,							//17 ���ʿ��
                    //                                          //check.FstoreMoney,						//18 
                    //                                          //check.AdjustNum,							//19 �̵�����
                    //                                          //check.ValidDate,							//20 ��Ч��
                    //                                          //Math.Round(check.AdjustNum * check.Item.UnitPrice,4),//21�̵���
                    //                                          //check.Item.Factory.Name,					//22 ��������
                    //                                          //check.Item.Factory.ID,					//23 ���ű���
                    //                                          //check.Item.SpellCode,					//24 ƴ����
                    //                                          //check.Item.WBCode						//25 �����
                    //                                      }
                    //    );
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            }

            //			this.neuSpread1_Sheet1.DataSource = this.myDataView;

            //��ʽ��
            this.SetFormat();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        ///�Է��ʡ��̵���̽��б��棬�����̵���ϸ��
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="checkCode">�̵㵥��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int Save(string deptCode, string checkCode)
        {

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            if (this.myDataTable.Rows.Count == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            //string nowOperDrug = "";

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б���.���Ժ�...");
            Application.DoEvents();
            try
            {
                this.myItem.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                DateTime sysTime = this.myItem.GetDateTimeFromSysDateTime();
                FS.HISFC.Models.Material.Check checkInfo = new FS.HISFC.Models.Material.Check();
                //ȡ���е��̵��¼
                ArrayList alAllCheck = this.GetUpdateInfo("All");
                //�̿����
                decimal lossCost = 0;
                //��ӯ���
                decimal profitCost = 0;
                foreach (FS.HISFC.Models.Material.Check tmpCheck in alAllCheck)
                {
                    lossCost += tmpCheck.CheckLossCost;
                    profitCost += tmpCheck.CheckProfitCost;
                }
                #region ���������̵���ܱ�
                if (checkCode == "" || checkCode == null)
                {
                    #region ���½��̵㵥���̵�ͳ�Ʊ��������
                    //if (this.newCheckCode != "")
                    //{	//�����ʺ����ε������ʱ��������ȡ�̵㵥�ż������̵�ͳ�Ʊ�
                    //    checkCode = this.newCheckCode;
                    //}
                    //else
                    //{
                    //��ȡ���̵㵥��
                    checkCode = this.myItem.GetCheckCode(deptCode);
                    //���������ɵ��̵㵥��
                    //this.newCheckCode = checkCode;
                    if (checkCode == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("��ȡ���̵㵥��ʧ��" + this.myItem.Err);
                        return -1;
                    }
                    checkInfo.CheckCode = checkCode;        //�̵㵥��
                    checkInfo.StoreHead.StoreBase.StockDept.ID = deptCode;  //�ⷿ����
                    checkInfo.CheckState = "0";      //����״̬
                    checkInfo.FOper.ID = this.myItem.Operator.ID;//������
                    checkInfo.FOper.OperTime = sysTime;				//����ʱ��
                    checkInfo.CheckLossCost = lossCost;           //�̿����
                    checkInfo.CheckProfitCost = profitCost;         //��ӯ���
                    checkInfo.Oper.ID = this.myItem.Operator.ID;   //����Ա
                    checkInfo.Oper.OperTime = sysTime;                //����ʱ��
                    checkInfo.StoreHead.StoreBase.StockNO = "ALL";    //������
                    if (this.myItem.InsertCheckStatic(checkInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("����̵�ͳ�Ʊ�ʧ��" + this.myItem.Err);
                        return -1;
                    }
                    //}

                    #endregion
                }
                else
                {
                    #region ���Ѵ��ڵ��̵㵥���̵�ͳ�Ʊ��������
                    checkInfo.CheckCode = checkCode;
                    checkInfo.StoreHead.StoreBase.StockDept.ID = deptCode;  //�ⷿ����
                    checkInfo.CheckState = "0";      //����״̬
                    checkInfo.FOper.ID = this.myItem.Operator.ID;//������
                    checkInfo.FOper.OperTime = sysTime;				//����ʱ��
                    checkInfo.CheckLossCost = lossCost;           //�̿����
                    checkInfo.CheckProfitCost = profitCost;         //��ӯ���
                    checkInfo.Oper.ID = this.myItem.Operator.ID;  //����Ա
                    checkInfo.Oper.OperTime = sysTime;             //����ʱ��
                    checkInfo.StoreHead.StoreBase.StockNO = "ALL";  //������
                    if (this.myItem.UpdateCheckStatic(checkInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("�����̵�ͳ�Ʊ�ʧ��" + this.myItem.Err);
                        return -1;
                    }
                    #endregion
                }
                #endregion
                //DataSet�ڻ�÷����䶯������
                ArrayList modifyList = this.GetUpdateInfo("Modify");
                ArrayList delList = this.GetUpdateInfo("Del");

                if (modifyList != null)
                {
                    #region �Է����䶯�ļ�¼���и���
                    foreach (FS.HISFC.Models.Material.Check info in modifyList)
                    {
                        //��ȡ����ҩƷ��Ϣ
                        //FS.HISFC.Models.Material.MaterialItem item = new MaterialItem();
                        //item = this.myItem.QueryMetItemAllByID(info.StoreHead.StoreBase.Item.ID);
                        //if (item == null)
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //    MessageBox.Show("��ȡ����ҩƷ��Ϣʱ����" + this.myItem.Err);
                        //    return -1;
                        //}
                        //nowOperDrug = item.Name;
                        //info.StoreHead.StoreBase.Item = item;					//����ҩƷʵ��
                        //info.CheckCode = checkCode;			//�̵㵥��
                        //info.StoreHead.StoreBase.StockDept.ID = deptCode;			//�ⷿ����
                        //info.CheckState = "0";				//�̵�״̬ ����
                        info.FOper.ID = this.myItem.Operator.ID;	//������
                        info.FOper.OperTime = sysTime;			//����ʱ��
                        info.CheckCode = checkCode;
                        info.StoreHead.StoreBase.StockDept.ID = deptCode;
                        //���������ݸ��ֶΣ���ˮ�ţ���FarPointȡ����Ϊ�գ���Ϊ��1
                        if (info.ID == "")
                        {
                            info.ID = "-1";
                        }
                        //�Ƚ��и��²����������ʧ�������
                        int parm = this.myItem.UpdateCheckDetail(info);
                        //���̵���ϸ���������
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("�����̵���ϸʱ����" + this.myItem.Err);
                            return -1;
                        }
                        else
                        {
                            if (parm == 0)
                            {
                                //���̵���ϸ���������
                                if (this.myItem.InsertCheckDetail(info) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    MessageBox.Show("����̵���ϸʱ����" + this.myItem.Err);
                                    return -1;
                                }
                            }
                        }
                    }
                    #endregion
                }
                if (delList != null)
                {		//
                    #region ��ɾ���ļ�¼����ɾ��
                    foreach (FS.HISFC.Models.Material.Check info in delList)
                    {
                        //���̵���ϸ��¼ɾ��
                        if (this.myItem.DeleteCheckDetail(info.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("ɾ���̵���ϸʱ����" + this.myItem.Err);
                            return -1;
                        }
                    }
                    #endregion
                }

                //�����̵�ӯ������ӯ��������ӯ�����
                //if (this.myItem.SaveCheck(deptCode, checkCode) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //    MessageBox.Show("����ʵ���̴�����ʱ����" + this.myItem.Err);
                //    return -1;
                //}
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message + "  \n");
                return -1;
            }
            //�ύ���񡢸�ʽ��farpoint
            FS.FrameWork.Management.PublicTrans.Commit();
            this.SetFormat();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show("����ɹ�");
            return 1;
        }

        ///{9ADAD904-B8B5-4f94-88A9-AF690A98D1BF}
        /// <summary>
        ///�Է��ʡ��̵���̽����������棬�����̵���ϸ��
        /// </summary> 
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        //public int AddSave(string deptCode, string checkCode)
        //{
        //    //��������
        //    FS.FrameWork.Management.PublicTrans.BeginTransaction();

        //    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
        //    //t.BeginTransaction();

        //    if (this.myDataTable.Rows.Count == 0)
        //    {
        //        FS.FrameWork.Management.PublicTrans.RollBack();
        //        return -1;
        //    }

        //    string nowOperDrug = "";

        //    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б���.���Ժ�...");
        //    Application.DoEvents();
        //    try
        //    {
        //        this.myItem.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
        //        DateTime sysTime = this.myItem.GetDateTimeFromSysDateTime();//DataSet�ڻ�÷����䶯������
        //        ArrayList modifyList = this.GetUpdateInfo("Modify");
        //        if (modifyList != null)
        //        {
        //            #region �Է����䶯�ļ�¼���и���
        //            foreach (FS.HISFC.Models.Material.Check info in modifyList)
        //            {
        //                //��ȡ����ҩƷ��Ϣ
        //                FS.HISFC.Models.Material.MaterialItem item = new MaterialItem();
        //                item = this.myItem.GetMetItemByMetID(info.StoreHead.StoreBase.Item.ID);
        //                if (item == null)
        //                {
        //                    FS.FrameWork.Management.PublicTrans.RollBack();
        //                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //                    MessageBox.Show("��ȡ����ҩƷ��Ϣʱ����" + this.myItem.Err);
        //                    return -1;
        //                }

        //                nowOperDrug = item.Name;

        //                info.StoreHead.StoreBase.Item = item;					//����ҩƷʵ��						
        //                info.CheckCode = checkCode;			//�̵㵥��			
        //                info.StoreHead.StoreBase.StockDept.ID = deptCode;			//�ⷿ����
        //                info.CheckState = "0";				//�̵�״̬ ����
        //                info.FOper.ID = this.myItem.Operator.ID;	//������
        //                info.FOper.OperTime = sysTime;			//����ʱ��

        //                //���̵���ϸ���������						
        //                int parm = this.myItem.UpdateCheckDetailAddSave(info);
        //                if (parm == -1)
        //                {
        //                    FS.FrameWork.Management.PublicTrans.RollBack();
        //                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //                    MessageBox.Show("�����̵���ϸʱ����" + this.myItem.Err);
        //                    return -1;
        //                }
        //            }
        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        FS.FrameWork.Management.PublicTrans.RollBack();
        //        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //        MessageBox.Show(ex.Message + "  \n" + nowOperDrug);
        //        return -1;
        //    }
        //    //�ύ���񡢸�ʽ��farpoint
        //    FS.FrameWork.Management.PublicTrans.Commit();

        //    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڵ�������...���Ժ�");
        //    Application.DoEvents();

        //    this.AutoExport();

        //    this.myDataTable.Clear();
        //    this.SetFormat();

        //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //    MessageBox.Show("����ɹ�", "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        //    return 1;
        //}

        /// <summary>
        /// ����б�  
        /// </summary>
        public void ClearData()
        {
            //���DataSet
            //this.myDataSet.Tables[0].Clear();
            //this.myDataSet.AcceptChanges();
            this.myDataTable.Clear();
            this.myDataTable.AcceptChanges();
            //��ʽ��FarPoint
            this.SetFormat();
            this.neuTextBox1.Text = "";
            this.nowOperCheckNO = "";
        }

        /// <summary>
        /// �ж��Ƿ���Լ���������������
        /// </summary>
        /// <returns>������з���1 ��ֹ���أ�1</returns>
        public int JudgeContinue()
        {
            if (this.neuSpread1_Sheet1.Rows.Count != 0)
            {
                DialogResult result;
                //��ʾ�û�ѡ���Ƿ�������ɣ���������������ԭ����
                result = MessageBox.Show("�������ʽ������ǰ���ݣ��Ƿ����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                if (result == DialogResult.No)
                {
                    return -1;
                }
            }
            return 1;
        }

        #region ���/���
        /// <summary>
        /// �Է����̵㵥���н�⴦��
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="checkCode">�̵㵥��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int CancelCheck(string deptCode, string checkCode)
        {
            //�統ǰ����������򷵻�
            if (this.myDataTable.Rows.Count == 0)
            {
                return -1;
            }
            DialogResult result;
            //��ʾ�û�ѡ���Ƿ����
            result = MessageBox.Show("ȷ�Ͻ��н�������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return -1;
            }
            //��������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��н�⴦��.���Ժ�...");
            Application.DoEvents();
            try
            {
                this.myItem.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                int i = this.myItem.CancelCheck(deptCode, checkCode);
                //���δ�ɹ�����
                if (i == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("������ʧ��" + this.myItem.Err);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("������ʧ��" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.SetFormat();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("�������ɹ�");
            return 1;
        }


        /// <summary>
        /// ���̵���н�����
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="checkCode">�̵㵥��</param>
        /// <returns>�ɹ�����1��ʧ�ܷ��أ�1</returns>
        public int CheckCStore(string deptCode, string checkCode)
        {
            //�統ǰ����������򷵻�
            if (this.myDataTable.Rows.Count == 0)
            {
                return -1;
            }
            if (string.IsNullOrEmpty(checkCode))
            {
                MessageBox.Show("��ȡ�̵㵥��ʧ��");
                return -1;
            }
            DialogResult result;
            //��ʾ�û�ѡ���Ƿ����
            result = MessageBox.Show("ȷ�Ͻ��н�������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return -1;
            }
            //��������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��н�洦��.���Ժ�...");
            Application.DoEvents();
            try
            {
                this.myItem.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (this.myItem.CheckCStore(deptCode, checkCode) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("������ʧ��" + this.myItem.Err);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("������ʧ��" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.SetFormat();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.ucMaterialItemList1.ShowDeptStorage(this.privDept.ID, false);
            this.ClearData();
            MessageBox.Show("�������ɹ�");
            return 1;
        }

        #endregion

        public int Print()
        {
            //			Local.GyHis.Pharmacy.ucPhaCheck uc = new Local.GyHis.Pharmacy.ucPhaCheck();
            //
            //			uc.Decimals = 2;
            //			uc.MaxRowNo = 8;
            //
            //			uc.PrintCheck(this.myDataTable,this.myPrivDept.ID,
            //				"",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),this.myItem.Operator.ID);
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
                    fileName = dlg.FileName;
                    this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����ǰ��ѯ���ݰ�Excel��ʽ�Զ�����
        /// </summary>
        public void AutoExport()
        {
            try
            {
                this.myDataTable.DefaultView.RowFilter = "1=1";

                DateTime dt = this.myItem.GetDateTimeFromSysDateTime();
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
        /// �̵������̵������
        /// </summary>
        private void SumCheckNumAndCost(int rowIndex)
        {
            //�̵�����
            decimal adjustNum = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.AdjustNum].Text);
            //�̵���С����
            decimal fStoreNum = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.FstoreNum].Text);

            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ProfitLossNum].Text = Math.Abs(adjustNum - fStoreNum).ToString();	//�̵���
            //this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.AdjustCost].Text = Math.Round((iPackNum + jMinNum / kPackQty) * price, 4).ToString();
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //����������ɫ
            this.SetFlag();
        }

        /// <summary>
        /// ���ݿ����Ϣ��ȡ�̵�ʵ��
        /// </summary>
        /// <param name="storeHead"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Material.Check GetCheck(StoreHead storeHead)
        {
            FS.HISFC.Models.Material.Check checkInfo = new FS.HISFC.Models.Material.Check();
            checkInfo.StoreHead = storeHead;
            checkInfo.CheckCode = "";
            checkInfo.FStoreNum = storeHead.StoreBase.StoreQty;
            checkInfo.AdjustNum = 0;
            checkInfo.ProfitLossNum = storeHead.StoreBase.StoreQty;
            checkInfo.CheckState = "0";
            return checkInfo;
        }

        /// <summary>
        /// ����Fp���
        /// </summary>
        protected void SetFlag()
        {
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.AdjustNum].Text) > NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.FstoreNum].Text))
                    {
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Blue;
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ProfitFlag].Text = "��ӯ";
                    }
                    else if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.AdjustNum].Text) < NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.FstoreNum].Text))
                    {
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ProfitFlag].Text = "�̿�";
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Black;
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ProfitFlag].Text = "��ӯ��";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("��ʽ��ӯ����ɫ��ʾʱ��������" + ex.Message));
                return;
            }
        }

        #endregion

        #region �¼�

        private void ucCheckManager_Load(object sender, EventArgs e)
        {
            //��ʼ�����ݱ�
            this.InitDataTable();
            //��ʼ��
            if (!this.DesignMode)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
                Application.DoEvents();

                //��ʼ��������
                this.initHelper();
                //��ʼ��������Ŀ��Ϣ
                this.InitData();

                this.InitCheckList();

                this.SetToolButton(true);

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.myDataTable.DefaultView == null)
                return;
            //��ù�������
            string queryCode = "";
            if (this.neuCheckBox1.Checked)		//ģ����ѯ
                queryCode = "%" + this.neuTextBox1.Text.Trim() + "%";
            else
                queryCode = this.neuTextBox1.Text.Trim() + "%";

            //modify by zhaoyang 2009-06-20 ע�͵������ڵ����� {38322DE1-5E53-4e37-B003-4CD638872EC0}
            string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                "(����� LIKE '" + queryCode + "') OR " +
                //"(ͨ����ƴ���� LIKE '" + queryCode + "') OR " +
                //"(ͨ��������� LIKE '" + queryCode + "') OR " +
                "(�Զ����� LIKE '" + queryCode + "') ";
            try
            {
                this.myDataTable.DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.SetFormat();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
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
                this.neuSpread1_Sheet1.ActiveColumnIndex = 10;
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //�粻����ɾ���򷵻�
            if (!this.allowDel)
                return;
            //��ǰ���Ϊ���򷵻�
            if (e.ColumnHeader)
                return;
            //ɾ��
            this.DeleteData();
        }

        // �������̵�����1���̵�����2������̵���
        private void fpSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == (int)ColumnSet.AdjustNum)
            {
                this.SumCheckNumAndCost(e.Row);
            }
            this.SetFlag();
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ClearData();

            if (e.Node != null && e.Node.Parent != null)
            {
                FS.HISFC.Models.Material.Check check = e.Node.Tag as FS.HISFC.Models.Material.Check;

                this.nowOperCheckNO = check.CheckCode;

                this.ShowCheckDetail(this.privDept.ID, check.CheckCode);

                this.SetFlag();
            }
        }

        #region ����س����������¼�
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
                        //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                        //��������
                        if (j == 13)
                        {

                            if (i < this.neuSpread1_Sheet1.Rows.Count - 1)
                            {
                                this.neuSpread1_Sheet1.ActiveRowIndex++;
                                this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, 13, false);
                            }
                            else
                            {
                                this.neuTextBox1.Focus();
                                this.neuTextBox1.SelectAll();
                            }

                            this.SumCheckNumAndCost(i);
                            
                            //							//������ת������̵���ļ���
                            //							if (this.neuSpread1_Sheet1.Cells[i,10].Text == "")
                            //								this.neuSpread1_Sheet1.Cells[i,10].Text = "0";
                            //							if (this.neuSpread1_Sheet1.Cells[i,12].Text == "")
                            //								this.neuSpread1_Sheet1.Cells[i,12].Text = "0";
                            //							decimal iPackNum = Convert.ToDecimal(this.neuSpread1_Sheet1.Cells[i,10].Text);
                            //							decimal jMinNum = Convert.ToDecimal(this.neuSpread1_Sheet1.Cells[i,12].Text);
                            //							decimal kPackQty = Convert.ToDecimal(this.neuSpread1_Sheet1.Cells[i,6].Text);
                            //							this.neuSpread1_Sheet1.Cells[i,14].Text = (iPackNum * kPackQty + jMinNum).ToString();
                        }
                    }

                    #endregion

                    break;
                case Keys.F5:

                    this.neuTextBox1.Focus();
                    this.neuTextBox1.SelectAll();

                    break;
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        /// <summary>
        /// ���fpѡ����Ʒ�¼�
        /// </summary>
        /// <param name="sv">SheetView</param>
        /// <param name="activeRow">ѡ�е���</param>
        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (activeRow < 0)
                return;
            //{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            //string itemCode = sv.Cells[activeRow, 0].Text;
            string itemCode = sv.Cells[activeRow, 11].Text;
            decimal salePrice = FrameWork.Function.NConvert.ToDecimal(sv.Cells[activeRow, 3].Text);
            //{B954FF22-CCDB-4d58-B233-9FFD0EC95410}�ж��Ƿ���δ�����̵㵥�а���ѡ����Ŀ
            int iReturn = this.IsUnchecked(itemCode, salePrice);
            if (iReturn == -1)
            {
                MessageBox.Show("��ȡ" + sv.Cells[activeRow, 1].Text + "���̵���Ϣʧ��:" + this.myItem.Err);

                return;
            }
            else if (iReturn > 0)
            {
                MessageBox.Show(sv.Cells[activeRow, 1].Text + "����δ�����̵㵥�������ٴ����");

                return;
            }
            //------------------------
            FS.HISFC.Models.Material.StoreHead storeHead = this.storeMgr.GetStoreHead(this.privDept.ID, itemCode, salePrice);
            if (storeHead == null)
            {
                MessageBox.Show("��ȡ" + sv.Cells[activeRow, 1].Text + "�Ŀ�������Ϣʧ��:" + this.storeMgr.Err);
                return;
            }
            FS.HISFC.Models.Material.MaterialItem item = this.hsItem[itemCode] as FS.HISFC.Models.Material.MaterialItem;
            if (item == null)
            {
                MessageBox.Show("������Ʒ��Ϣʧ�ܣ������´򿪸ô���");
                return;
            }
            storeHead.StoreBase.Item = item;
            FS.HISFC.Models.Material.Check checkInfo = this.GetCheck(storeHead);
            this.AddData(checkInfo);
            this.SetFlag();
        }

        /// <summary>
        /// �ж��Ƿ���δ�����̵㵥�а���ѡ����Ŀ{B954FF22-CCDB-4d58-B233-9FFD0EC95410}
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="salePrice"></param>
        /// <returns></returns>
        private int IsUnchecked(string itemCode, decimal salePrice)
        {
            ArrayList alCheck = this.myItem.GetCheckDetail(itemCode, salePrice, "0");
            if (alCheck == null)
            {
                return -1;
            }

            return alCheck.Count;
        }

        #endregion

        #region ������

        private enum ColumnSet
        {
            /// <summary>
            /// �̵���ˮ��	0
            /// </summary>
            CheckNo,
            /// <summary>
            /// �̵㵥��	1
            /// </summary>
            CheckCode,
            /// <summary>
            /// �ֿ����	2
            /// </summary>
            StorageCode,
            /// <summary>
            /// ��Ʒ����	3
            /// </summary>
            ItemCode,
            /// <summary>
            /// ��Ʒ����	4
            /// </summary>
            ItemName,
            /// <summary>
            /// ���۽��	5
            /// </summary>
            SaleCost,
            /// <summary>
            /// �����ţ��������̵㣺ALL	6	
            /// </summary>
            StockNo,
            /// <summary>
            /// ���		7
            /// </summary>
            Specs,
            /// <summary>
            /// ��λ���	8
            /// </summary>
            PlaceCode,
            /// <summary>
            /// �����Ч��	9
            /// </summary>
            ValidDate,
            /// <summary>
            /// ������˾	10
            /// </summary>
            Company,
            /// <summary>
            /// ��������	11
            /// </summary>
            Factory,
            /// <summary>
            /// ���ʿ������(��С��λ)	12
            /// </summary>
            FstoreNum,
            /// <summary>
            /// ʵ���̴�����(��С��λ)	13
            /// </summary>
            AdjustNum,
            /// <summary>
            /// ���������(��С��λ)	14
            /// </summary>
            CstoreNum,
            /// <summary>
            /// ӯ������	15
            /// </summary>
            ProfitLossNum,
            /// <summary>
            /// ������λ	16
            /// </summary>
            StatUnit,
            /// <summary>
            /// ӯ�����(0�̿���1��ӯ��2��ӯ��)	17
            /// </summary>
            ProfitFlag,
            /// <summary>
            /// �̵�״̬(0���ʣ�1��棻2ȡ��)	18
            /// </summary>
            CheckState,
            /// <summary>
            /// ����Ա      19
            /// </summary>
            OperCode,
            /// <summary>
            /// ��������	20
            /// </summary>
            OperDate,
            /// <summary>
            /// ƴ����		21
            /// </summary>
            SpellCode,
            /// <summary>
            /// �����		22
            /// </summary>
            WBCode,
            /// <summary>
            /// �Զ�����    23
            /// </summary>
            UserCode
        }

        #endregion

        #region �ӿڳ�Ա

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                //printType[0] = typeof(FS.HISFC.BizProcess.Integrate.PharmacyInterface.IBillPrint);

                return printType;
            }
        }

        #endregion

        #region IPreArrange ��Ա

        bool isPreArrange = false;

        public int PreArrange()
        {
            this.isPreArrange = true;

            string class2Priv = "0505";
            //���ݽ�水ť����λ���жϴ������� ��ʾ���ʱ �̵��� ���� �̵���� 
            if (this.toolBarService.GetToolButton("���").Owner != null && this.toolBarService.GetToolButton("���").Owner.Visible)      //���
            {
                class2Priv = "0507";            //�̵���
            }
            else
            {
                class2Priv = "0505";            //�̵����
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

            base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

            return 1;
        }

        #endregion

        #endregion
    }
}
