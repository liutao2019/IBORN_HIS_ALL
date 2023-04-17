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

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ҩƷģ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸ļ�¼>
    ///    1.����ҩƷģ���Զ������� by Sunjh 2010-8-25 {510B1973-959C-4ebf-9CEB-F850393B1819}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucDrugStencil : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugStencil()
        {
            InitializeComponent();

            this.neuSpread1.AutoSortedColumn += new FarPoint.Win.Spread.AutoSortedColumnEventHandler(neuSpread1_AutoSortedColumn);
        }

        void neuSpread1_AutoSortedColumn(object sender, FarPoint.Win.Spread.AutoSortedColumnEventArgs e)
        {
            if (this.neuSpread1_Sheet1.Models.Data is FarPoint.Win.Spread.Model.IDataSourceSupport)
            {
                int activeRow = this.neuSpread1_Sheet1.ActiveRowIndex;
                int modelRow = this.neuSpread1_Sheet1.GetModelRowFromViewRow(activeRow);
                int dataRow = ((FarPoint.Win.Spread.Model.IDataSourceSupport)this.neuSpread1_Sheet1.Models.Data).GetDataRowFromModelRow(modelRow);

                if (dataRow != -1)
                {
                    this.neuSpread1.BindingContext[this.dt.DefaultView].Position = dataRow;
                }
            }
  
        }
       
        #region �����

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// ���β�����ģ����Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject stencil = new FS.FrameWork.Models.NeuObject();
      
        /// <summary>
        /// ��ǰ������ģ������
        /// </summary>
        private string stencilTypeID = "";

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
      
        /// <summary>
        /// ҩƷҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ���б�
        /// </summary>
        protected bool IsShowList
        {
            get
            {
                return this.ucChooseList1.IsShowTree;
            }
            set
            {
                this.ucChooseList1.IsShowTree = value;

                this.SetTooButton(value);
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            #region {9768C6B1-5F8C-484c-AFBC-0B2D8CC55400}
            toolBarService.AddToolButton("ģ  ��", "ģ  ��", FS.FrameWork.WinForms.Classes.EnumImageList.Mģ��, true, false, null); 
            #endregion
            toolBarService.AddToolButton("��  ��", "�½�ģ��", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);            
            toolBarService.AddToolButton("ɾ����ϸ", "ɾ����ǰѡ���ģ����ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("ɾ��ģ��", "ɾ����ǰѡ���ģ��", FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
            toolBarService.AddToolButton( "�Զ�����", "������Ʒ���ƻ�����Զ��������к�", FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null );

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ģ  ��")
            {
                this.IsShowList = true;
            }
            if (e.ClickedItem.Text == "��  ��")
            {
                this.New();
            }
            if (e.ClickedItem.Text == "ɾ����ϸ")
            {
                this.DelDetail();
            }
            if (e.ClickedItem.Text == "ɾ��ģ��")
            {
                this.DelStencil();
            }
            if (e.ClickedItem.Text == "�Զ�����")
            {
                //����ҩƷģ���Զ������� by Sunjh 2010-8-25 {510B1973-959C-4ebf-9CEB-F850393B1819}
                this.neuSpread1_Sheet1.AutoSortColumn(2,true,false);
                this.neuSpread1_Sheet1.SetColumnAllowAutoSort(2, false);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {           
            this.Save();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                if (this.neuSpread1.Export() == 1)
                {
                    MessageBox.Show(Language.Msg("�����ɹ�"));
                }
            }

            return 1;
        }

        /// <summary>
        /// ���ù�������ť��ʾ
        /// </summary>
        /// <param name="isShowList"></param>
        protected void SetTooButton(bool isShowList)
        {
            this.toolBarService.SetToolButtonEnabled("ģ  ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��  ��", isShowList);
        }

        #endregion

        #region ��ʼ����Fp����

        /// <summary>
        /// DataSet��ʼ��
        /// </summary>
        private void InitDataSet()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtInt = System.Type.GetType("System.Int32");

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {
                                                                    new DataColumn("��Ʒ����",      dtStr),
                                                                    new DataColumn("���",			dtStr),
                                                                    new DataColumn("˳���",		dtInt),
                                                                    new DataColumn("����Ա",		dtStr),
                                                                    new DataColumn("����ʱ��",      dtStr),
                                                                    new DataColumn("ҩƷ����",		dtStr),                                                                    
                                                                    new DataColumn("ƴ����",		dtStr),
                                                                    new DataColumn("�����",		dtStr),
                                                                    new DataColumn("�Զ�����",      dtStr),
                                                                    new DataColumn("ͨ����ƴ����",	dtStr),
                                                                    new DataColumn("ͨ���������",	dtStr)
            });
            //�趨���ڶ�DataView�����ظ��м���������
            DataColumn[] keys = new DataColumn[1];
            keys[0] = this.dt.Columns["ҩƷ����"];
            this.dt.PrimaryKey = keys;
            //���ݰ�
            this.neuSpread1_Sheet1.DataSource = this.dt;

            this.SetFormat();
        }

        /// <summary>
        /// Fp��ʽ������
        /// </summary>
        private void SetFormat()
        {
            //����F5�� F5��������ת
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.F5, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);


            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColDrugNO].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRegularSpell].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRegularWBCode].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSortNO].Locked = false;

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCell = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            markNumCell.DecimalPlaces = 0;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSortNO].CellType = markNumCell;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        protected void InitData()
        {
            this.ucChooseList1.TvList.ImageList = this.ucChooseList1.TvList.deptImageList;

            ArrayList al = FS.HISFC.Models.Pharmacy.DrugStencilEnumService.List();
            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                TreeNode node = new TreeNode(info.Name);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                node.Tag = info.ID;

                this.ucChooseList1.TvList.Nodes.Add(node);
            }

            this.ucChooseList1.ShowPharmacyList();

            this.ucChooseList1.ListTitle = "ģ���б�";
        }

        /// <summary>
        /// �¼���ʼ��
        /// </summary>
        protected void InitEvent()
        { 
            this.ucChooseList1.TvList.AfterLabelEdit += new NodeLabelEditEventHandler(TvList_AfterLabelEdit);
            this.ucChooseList1.TvList.AfterSelect += new TreeViewEventHandler(TvList_AfterSelect);
            this.ucChooseList1.TvList.DoubleClick += new EventHandler(TvList_DoubleClick);

            this.ucChooseList1.ChooseDataEvent += new ucChooseList.ChooseDataHandler(ucChooseList1_ChooseDataEvent);            
        }
     
        #endregion

        #region ����

        /// <summary>
        /// ����ҩƷ��Ϣ ����DataRow
        /// </summary>
        /// <param name="drugStencil">ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����һ��DataRow��Ϣ</returns>
        private System.Data.DataRow GetDataRow(FS.HISFC.Models.Pharmacy.DrugStencil drugStencil)
        {
            FS.HISFC.Models.Pharmacy.Item item = this.itemManager.GetItem(drugStencil.Item.ID);
            DataRow dr = this.dt.NewRow();
            if (item != null)
            {
                dr["ҩƷ����"] = item.ID;
                dr["��Ʒ����"] = item.Name;
                dr["���"] = item.Specs;
                dr["˳���"] = drugStencil.SortNO;
                dr["����Ա"] = drugStencil.Oper.ID;
                dr["����ʱ��"] = drugStencil.Oper.OperTime.ToString();
                dr["ƴ����"] = item.NameCollection.SpellCode;
                dr["�����"] = item.NameCollection.WBCode;
                dr["�Զ�����"] = item.NameCollection.UserCode;
                dr["ͨ����ƴ����"] = item.NameCollection.RegularSpell.SpellCode;
                dr["ͨ���������"] = item.NameCollection.RegularSpell.WBCode;
            }
            else
            {
                MessageBox.Show("ҩƷ�б����Ҳ���ģ���е�" + drugStencil.Item.ID + drugStencil.Item.Name + "���ݣ����ֹ�ɾ����");
                dr["ҩƷ����"] = drugStencil.Item.ID;
                dr["��Ʒ����"] = drugStencil.Item.Name;
                dr["���"] = drugStencil.Item.Specs;
                dr["˳���"] = drugStencil.SortNO;
                dr["����Ա"] = drugStencil.Oper.ID;
                dr["����ʱ��"] = drugStencil.Oper.OperTime.ToString();
                dr["ƴ����"] = drugStencil.Item.NameCollection.SpellCode;
                dr["�����"] = drugStencil.Item.NameCollection.WBCode;
                dr["�Զ�����"] = drugStencil.Item.NameCollection.UserCode;
                dr["ͨ����ƴ����"] = drugStencil.Item.NameCollection.RegularSpell.SpellCode;
                dr["ͨ���������"] = drugStencil.Item.NameCollection.RegularSpell.WBCode;
            }
            return dr;
        }

        /// <summary>
        /// �������ݼ���Ϣ��ȡģ��ʵ����Ϣ
        /// </summary>
        /// <param name="dr">���ݱ���Ϣ</param>
        /// <returns>�ɹ�����ģ��ʵ����Ϣ</returns>
        private FS.HISFC.Models.Pharmacy.DrugStencil GetDrugStencil(DataRow dr)
        {
            FS.HISFC.Models.Pharmacy.DrugStencil drugStencil = new FS.HISFC.Models.Pharmacy.DrugStencil();
            drugStencil.Dept = this.privDept;
            drugStencil.OpenType.ID = this.stencilTypeID;
            drugStencil.Stencil = this.stencil;
            drugStencil.Item.ID = dr["ҩƷ����"].ToString();
            drugStencil.Item.Name = dr["��Ʒ����"].ToString();
            drugStencil.Item.Specs = dr["���"].ToString();
            drugStencil.SortNO = FS.FrameWork.Function.NConvert.ToInt32(dr["˳���"]);

            return drugStencil;
        }

        /// <summary>
        /// �����б���ʾ
        /// </summary>
        public int ShowList()
        {
            try
            {
                ArrayList alList = this.consManager.QueryDrugStencilList(this.privDept.ID);
                if (alList == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ������ģ���б�������" + this.consManager.Err));
                    return -1;
                }

                foreach (TreeNode rootNode in this.ucChooseList1.TvList.Nodes)
                {
                    rootNode.Nodes.Clear();
                }
                TreeNode parentNode = new TreeNode();
                foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in alList)
                {
                    parentNode = this.GetParentNode(info.OpenType);
                    TreeNode node = new TreeNode(info.Stencil.Name + "[" + info.Stencil.ID + "]");
                    node.ImageIndex = 4;
                    node.SelectedImageIndex = 5;
                    node.Tag = info;
                    parentNode.Nodes.Add(node);
                }

                this.ucChooseList1.TvList.ExpandAll();

                this.IsShowList = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����ָ��ģ�����ͻ�ȡ�����ڵ�
        /// </summary>
        /// <param name="drugOpenEnumService">ģ������</param>
        /// <returns>�ɹ����ظ����͵Ľڵ�</returns>
        private TreeNode GetParentNode(FS.HISFC.Models.Pharmacy.DrugStencilEnumService drugStencilEnumService)
        {
            foreach (TreeNode tempNode in this.ucChooseList1.TvList.Nodes)
            {
                if (tempNode.Tag != null && tempNode.Tag.ToString() == drugStencilEnumService.ID)
                    return tempNode;
            }
            TreeNode node = new TreeNode(drugStencilEnumService.Name);
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
            node.Tag = drugStencilEnumService.ID;
            this.ucChooseList1.TvList.Nodes.Add(node);
            return node;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isFpFocus">�Ƿ�����Fp����</param>
        protected void SetFocus(bool isFpFocus)
        {
            if (isFpFocus)
            {
                this.neuSpread1.Select();
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColSortNO;
            }
            else
            {
                if (this.IsShowList)
                {
                    this.txtFilter.Focus();
                    this.txtFilter.SelectAll();
                }
                else
                {
                    this.ucChooseList1.Select();
                    this.ucChooseList1.SetFoucs();
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="drugCode">����ӵ�ҩƷ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddData(string drugNO)
        {
            FS.HISFC.Models.Pharmacy.Item itemTemp = this.itemManager.GetItem(drugNO);
            if (itemTemp == null)
                return -1;

            return this.AddData(itemTemp);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="item">����ӵ�ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddData(FS.HISFC.Models.Pharmacy.Item item)
        {
            try
            {
                string[] keys = new string[] { item.ID };
                if (this.dt.Rows.Find(keys) != null)
                {
                    MessageBox.Show(Language.Msg(item.Name + "����ӵ�ģ����"));
                    this.SetFocus(false);
                    return -1;
                }
                FS.HISFC.Models.Pharmacy.DrugStencil drugStencil = new FS.HISFC.Models.Pharmacy.DrugStencil();
                drugStencil.Dept = this.privDept;
                drugStencil.OpenType.ID = this.stencilTypeID;
                drugStencil.Item = item;

                this.dt.Rows.Add(this.GetDataRow(drugStencil));

                this.SetFocus(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// Fp�������
        /// </summary>
        public void Clear()
        {
            if (this.dt != null)
                this.dt.Rows.Clear();
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Filter()
        {
            try
            {
                string str = "%" + this.txtFilter.Text + "%";
               this.dt.DefaultView.RowFilter = Function.GetFilterStr(this.dt.DefaultView,str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }      

        /// <summary>
        /// ����ģ��
        /// </summary>
        public void New()
        {
            if (this.ucChooseList1.TvList.SelectedNode != null && this.ucChooseList1.TvList.SelectedNode.Parent == null)
            {
                TreeNode node = new TreeNode();

                FS.HISFC.Models.Pharmacy.DrugStencil drugStencil = new FS.HISFC.Models.Pharmacy.DrugStencil();
                drugStencil.Dept = this.privDept;
                drugStencil.OpenType.ID = this.stencilTypeID;

                this.stencil = new FS.FrameWork.Models.NeuObject();
                this.stencil.Name = "�½�ģ��";
                drugStencil.Stencil = this.stencil;

                node.Text = "�½�ģ��";
                node.Tag = drugStencil;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;

                this.ucChooseList1.TvList.SelectedNode.Nodes.Add(node);
                this.ucChooseList1.TvList.SelectedNode = node;

                this.ucChooseList1.TvList.LabelEdit = true;
                node.BeginEdit();
            }
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="stencilCode">ģ�����</param>
        public void ShowData(string stencilNO)
        {
            if (stencilNO == null || stencilNO == "")
                return;

            ArrayList al = this.consManager.QueryDrugStencil(stencilNO);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("����ģ������ȡģ����ϸ��Ϣ��������\n" + this.consManager.Err));
                return;
            }
            this.dt.Rows.Clear();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ʾģ����ϸ ���Ժ�...");
            Application.DoEvents();

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in al)
            {
                this.stencil = info.Stencil;

                this.dt.Rows.Add(this.GetDataRow(info));
            }

            this.dt.AcceptChanges();

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;
        }

        /// <summary>
        /// ɾ��ģ����ϸ
        /// </summary>
        public void DelDetail()
        {
            if (this.dt.Rows.Count <= 0)
                return;

            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ��������¼��?"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
                return;

            string durgNO = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text;
            int parma = this.consManager.DelDrugStencil(this.stencil.ID, durgNO);
            if (parma == -1)
            {
                MessageBox.Show(Language.Msg("ɾ��ʧ��"));
                return;
            }
            DataRow drFind = this.dt.Rows.Find(new string[] { durgNO });
            if (drFind != null)
            {
                this.dt.Rows.Remove(drFind);
            }

            MessageBox.Show("ɾ���ɹ�");

            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                this.ShowList();
            }
        }

        /// <summary>
        /// ɾ��ģ��
        /// </summary>
        public void DelStencil()
        {
            if (this.dt.Rows.Count <= 0)
                return;

            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ����ģ����?\n ע�� �˲������ɻָ�"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
                return;

            if (this.consManager.DelDrugStencil(this.stencil.ID) == -1)
            {
                MessageBox.Show(Language.Msg("ɾ��ʧ��"));
                return;
            }
            TreeNode tempNode = this.ucChooseList1.TvList.SelectedNode;

            this.ucChooseList1.TvList.Nodes.Remove(tempNode);

            MessageBox.Show("ɾ���ɹ�");
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            if (this.dt.Rows.Count <= 0)
                return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.neuSpread1.StopCellEditing();

            for (int i = 0; i < this.dt.DefaultView.Count; i++)
            {
                this.dt.DefaultView[i].EndEdit();
            }

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.stencil == null || this.stencil.ID == "")
            {
                this.stencil.ID = this.consManager.GetNewStencilNO();
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���ģ�� ���Ժ�...");
            Application.DoEvents();

            DataTable dtChange = this.dt.GetChanges(System.Data.DataRowState.Modified | System.Data.DataRowState.Added);
            if (dtChange != null && dtChange.Rows.Count > 0)
            {
                foreach (DataRow dr in dtChange.Rows)
                {
                    FS.HISFC.Models.Pharmacy.DrugStencil temp = this.GetDrugStencil(dr);
                    if (temp == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(Language.Msg("��DataSet�ڻ�ȡ�仯����Ϣ��������"));
                        return;
                    }
                    if (this.consManager.SetDrugStencil(temp) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(Language.Msg("����ģ����Ϣʧ��" + this.consManager.Err));
                        return;
                    }
                }
            }

            this.dt.AcceptChanges();

            FS.FrameWork.Management.PublicTrans.Commit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show(Language.Msg("����ɹ�"));

            this.IsShowList = true;

            this.Clear();

            this.ShowList();

            if (this.ucChooseList1.TvList.SelectedNode != null)
            {
                if (this.ucChooseList1.TvList.SelectedNode.Parent != null)
                {
                    this.ucChooseList1.TvList.SelectedNode = this.ucChooseList1.TvList.SelectedNode.Parent;
                }
            }
        }

        #endregion

        private void ucDrugStencil_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() == "DEVENV")
            {
                return;
            }
            else
            {

                this.InitDataSet();

                this.InitData();

                this.InitEvent();

                this.privDept = ((FS.HISFC.Models.Base.Employee)(this.itemManager.Operator)).Dept;

                try
                {
                    this.ShowList();
                }
                catch { }
            }
        }      

        private void ucChooseList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRowIndex)
        {
            string drugNO = sv.Cells[activeRowIndex, 0].Text;

            this.AddData(drugNO);
        }

        private void TvList_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        e.Node.EndEdit(false);
                    }
                    else
                    {
                        e.CancelEdit = true;
                        MessageBox.Show(Language.Msg("������Ч�ַ�!����������"));
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show(Language.Msg("ģ�����Ʋ���Ϊ��"));
                    e.Node.BeginEdit();
                }

                this.stencil.Name = e.Label;
                if (this.stencil.ID != "")
                {
                    if (this.consManager.UpdateDrugStencilName(this.stencil.ID, e.Label.ToString()) == -1)
                    {
                        MessageBox.Show(Language.Msg("ģ�������޸�ʧ��" + this.consManager.Err));
                        return;
                    }
                }
                else
                {
                    this.stencil.Name = e.Label.ToString();
                }
            }
        }

        private void TvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node.Parent != null)
            {
                this.stencilTypeID = e.Node.Parent.Tag.ToString();
                if (e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugStencil != null)
                {
                    this.stencil = (e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugStencil).Stencil;
                    this.ShowData(this.stencil.ID);
                }
            }
            else
            {
                this.stencilTypeID = e.Node.Tag.ToString();
            }
        }

        private void TvList_DoubleClick(object sender, EventArgs e)
        {
            if (this.ucChooseList1.TvList.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.DrugStencil != null)
            {
                this.IsShowList = false;
                this.txtFilter.Text = "";
            }
        }        

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.neuSpread1.Select();
                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColSortNO;
            }
            if (e.KeyCode == Keys.Up)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex == 0)
                    this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                else
                    this.neuSpread1_Sheet1.ActiveRowIndex--;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex == this.neuSpread1_Sheet1.Rows.Count - 1)
                    this.neuSpread1_Sheet1.ActiveRowIndex = 0;
                else
                    this.neuSpread1_Sheet1.ActiveRowIndex++;

                e.Handled = true;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.neuSpread1_Sheet1.ActiveRowIndex == this.neuSpread1_Sheet1.Rows.Count - 1)
                    {
                        this.SetFocus(false);
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.ActiveRowIndex++;
                    }
                }
            }

            if (keyData == Keys.F5)
            {
                this.SetFocus(false);
            }
            return base.ProcessDialogKey(keyData);
        }

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
            /// ˳���
            /// </summary>
            ColSortNO,
            /// <summary>
            /// ����Ա
            /// </summary>
            ColOperNO,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            ColOperTime,
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColDrugNO,
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
            /// ͨ����ƴ����
            /// </summary>
            ColRegularSpell,
            /// <summary>
            /// ͨ���������
            /// </summary>
            ColRegularWBCode
        }                               

    }
}
