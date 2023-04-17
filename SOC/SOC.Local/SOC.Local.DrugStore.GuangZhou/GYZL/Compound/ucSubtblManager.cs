using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Order;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{
    /// <summary>
    /// �������ĸ����㷨ά��
    /// </summary>
    public partial class ucSubtblManagement : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSubtblManagement()
        {
            InitializeComponent();
        }

        #region ϵͳ�������

        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        //FS.HISFC.BizLogic.Order.SubtblManager subtblMgr = new FS.HISFC.BizLogic.Order.SubtblManager();
        FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound.AdditionalItem subtblMgr = new SOC.Local.DrugStore.GuangZhou.GYZL.Compound.AdditionalItem();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region ����

        ArrayList al = new ArrayList();

        /// <summary>
        /// ��ҩƷ��Ŀ
        /// </summary>
        private DataTable dtUndrug = null;


        /// <summary>
        /// ҩƷ�б�
        /// </summary>
        private DataTable dtDrug = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolbar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        ArrayList alItem = new ArrayList();

        /// <summary>
        /// ��ǰά�����÷�����Ŀ
        /// </summary>
        private FS.FrameWork.Models.NeuObject currentUsage = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����б�
        /// </summary>
        //ArrayList alDept = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        IList<string> IDepartmentList = new List<string>();
        /// <summary>
        /// ���Ұ�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ҩƷ��Ŀ�б�
        /// </summary>
        ArrayList alItems = new ArrayList();

        FS.FrameWork.Public.ObjectHelper itemHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        private FS.HISFC.Models.Base.Employee oper = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

        /// <summary>
        /// ����˵���ĵ�
        /// </summary>
        private string explainNote = "���Ĵ���ά������˵��.doc";

        /// <summary>
        /// ����˵���ĵ�
        /// </summary>
        [Category("����"), Description("���Ĵ���ά������˵����Ӧ���ĵ����ƣ�Ĭ�ϡ����Ĵ���ά������˵��.doc��")]
        public string ExplainNote
        {
            get
            {
                return explainNote;
            }
            set
            {
                explainNote = value;
            }
        }

        /// <summary>
        /// �Ƿ����п��Ҹ��Ķ���ʾ
        /// </summary>
        private bool isAllDeptShow = true;

        /// <summary>
        /// �Ƿ����п��Ҹ��Ķ���ʾ
        /// </summary>
        [Category("����"), Description("�Ƿ����п��Ҹ��Ķ���ʾ")]
        public bool IsAllDeptShow
        {
            get
            {
                return isAllDeptShow;
            }
            set
            {
                isAllDeptShow = value;
            }
        }

        /// <summary>
        /// ����˵��
        /// </summary>
        private string useExplain = "";

        /// <summary>
        /// ����˵�� ���ڽ�����ʾ
        /// </summary>
        [Category("����"), Description("����˵�� ���ڽ�����ʾ")]
        public string UseExplain
        {
            get
            {
                return useExplain;
            }
            set
            {
                useExplain = value;
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����������Ժ�....");
            Application.DoEvents();
            this.InitTree();
            this.ucInputItem1.Init();
            
            this.InitItemFp();

            ArrayList alDept = new ArrayList();
            //alDept = this.managerIntegrate.GetDepartment();
            alDept = SOC.HISFC.BizProcess.Cache.Common.GetDept();
            if (alDept == null)
            {
                MessageBox.Show(managerIntegrate.Err);
            }
            else
            {
                FS.HISFC.Models.Base.Const allObj = new FS.HISFC.Models.Base.Const();
                allObj.Name = "ȫ��";
                allObj.ID = "ROOT";
                allObj.UserCode = "QB";
                this.deptHelper.ArrayObject.Add(allObj);
                deptHelper.ArrayObject.AddRange(alDept);
                //alDept.Add(allObj);
                //this.deptHelper.ArrayObject = alDept;
            }

            alItems = new ArrayList(feeMgr.QueryAllItemsList());
            if (alItems == null)
            {
                MessageBox.Show(feeMgr.Err);
            }
            else
            {
                this.itemHelper.ArrayObject = alItems;
            }


            #region �����б�

            #endregion

            this.lblUseExplain.Text = this.useExplain;
            this.pnDesign.Visible = true;
            if (string.IsNullOrEmpty(lblUseExplain.Text))
            {
                this.pnDesign.Visible = false;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region ��ʼ�������б�

            ArrayList al = managerIntegrate.QueryDepartment(oper.Nurse.ID);
            if (al == null || al.Count == 0)
            {
                this.neuListBox1.Items.Add(oper.Dept);
                this.IDepartmentList.Add(oper.Dept.ID);
            }
            else
            {
                for (int i = 0; i < al.Count; i++)
                {
                    try
                    {
                        FS.FrameWork.Models.NeuObject o = al[i] as FS.FrameWork.Models.NeuObject;
                        this.neuListBox1.Items.Add(o);
                        this.IDepartmentList.Add(o.ID);
                    }
                    catch { }
                }
            }

            if (this.neuListBox1.Items.Count > 0)
            {
                this.neuListBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("û��ά����ؿ��һ���վ��ά�������ʹ�øù���");
                return;
            }
            FS.FrameWork.Models.NeuObject objdept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

            #endregion
        }

        /// <summary>
        /// ��ʼ��������Ŀ�б�
        /// </summary>
        private void InitItemFp()
        {
            #region ��ҩƷ
            ArrayList alUndrug = new ArrayList(this.feeMgr.QueryValidItems());
            if (alUndrug == null)
            {
                MessageBox.Show(feeMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //��������
            System.Type dtStr = System.Type.GetType("System.String");

            this.dtUndrug = new DataTable();

            //��myDataTable�ж�����
            this.dtUndrug.Columns.AddRange(new DataColumn[] {													
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("���",     dtStr),
													new DataColumn("���",     dtStr),
													new DataColumn("����",   dtStr),
													new DataColumn("��λ",   dtStr),
													new DataColumn("ƴ����",   dtStr),
													new DataColumn("�����",   dtStr),
													new DataColumn("��Ŀ����",   dtStr)
											        });

            this.dtUndrug.PrimaryKey = new DataColumn[] { this.dtUndrug.Columns["��Ŀ����"] };

            DataRow dRow;

            foreach (FS.HISFC.Models.Fee.Item.Undrug undrugObj in alUndrug)
            {
                dRow = dtUndrug.NewRow();
                dRow["����"] = undrugObj.Name;
                dRow["����"] = undrugObj.UserCode;
                dRow["���"] = undrugObj.Specs;
                dRow["���"] = undrugObj.SysClass.Name;
                dRow["����"] = undrugObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ";
                dRow["��λ"] = undrugObj.PriceUnit;
                dRow["ƴ����"] = undrugObj.SpellCode;
                dRow["�����"] = undrugObj.WBCode;
                dRow["��Ŀ����"] = undrugObj.ID;
                dtUndrug.Rows.Add(dRow);
            }

            this.dtUndrug.DefaultView.Sort = "����";
            this.fpUndrug_Sheet1.DataSource = this.dtUndrug.DefaultView;

            this.fpUndrug_Sheet1.Columns[6].Visible = false;
            this.fpUndrug_Sheet1.Columns[7].Visible = false;
            this.fpUndrug_Sheet1.Columns[8].Visible = false;
            #endregion

            #region ҩƷ�б�

            ArrayList alDrug = new ArrayList(this.phaIntegrate.QueryItemList(true));
            if (alDrug == null)
            {
                MessageBox.Show(phaIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.dtDrug = new DataTable();

            //��myDataTable�ж�����
            this.dtDrug.Columns.AddRange(new DataColumn[] {													
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("���",     dtStr),
													new DataColumn("���",     dtStr),
													new DataColumn("����",   dtStr),
													new DataColumn("��λ",   dtStr),
													new DataColumn("ƴ����",   dtStr),
													new DataColumn("�����",   dtStr),
													new DataColumn("��Ŀ����",   dtStr)
											        });

            this.dtDrug.PrimaryKey = new DataColumn[] { this.dtDrug.Columns["��Ŀ����"] };


            foreach (FS.HISFC.Models.Pharmacy.Item drugObj in alDrug)
            {
                if (drugObj.Type.ID=="P"&&drugObj.IsValid)
                {
                    dRow = dtDrug.NewRow();
                    dRow["����"] = drugObj.Name;
                    dRow["����"] = drugObj.UserCode;
                    dRow["���"] = drugObj.Specs;
                    dRow["���"] = drugObj.SysClass.Name;
                    dRow["����"] = drugObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ";
                    dRow["��λ"] = drugObj.PriceUnit;
                    dRow["ƴ����"] = drugObj.SpellCode;
                    dRow["�����"] = drugObj.WBCode;
                    dRow["��Ŀ����"] = drugObj.ID;
                    dtDrug.Rows.Add(dRow);
                }                
            }

            this.dtDrug.DefaultView.Sort = "����";
            this.fpDrug_Sheet1.DataSource = this.dtDrug.DefaultView;

            this.fpDrug_Sheet1.Columns[6].Visible = false;
            this.fpDrug_Sheet1.Columns[7].Visible = false;
            this.fpDrug_Sheet1.Columns[8].Visible = false;

            #endregion
        }

        /// <summary>
        /// ��ʼ���÷�TreeView
        /// </summary>
        private void InitTree()
        {
            this.tvUsage.Nodes.Clear();
            TreeNode root = new TreeNode("�÷�");
            root.ImageIndex = 40;
            this.tvUsage.Nodes.Add(root);
            //����÷��б�
            if (al != null)
            {
                al = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            }

            if (al != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    node.Tag = obj;
                    node.ImageIndex = 41;
                    root.Nodes.Add(node);
                }
                root.Expand();
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��ʼ��ToolBar
        /// </summary>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolbar.AddToolButton("ɾ��", "ɾ������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolbar.AddToolButton("����˵��", "�鿴����˵��", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            return this.toolbar;
        }

        private void ucSubtblManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.Init();

                this.neuListBox1.SelectedIndexChanged += new EventHandler(neuListBox1_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                if (this.fpSpread1_Sheet1.Rows.Count <= 0)
                    return;

                int row = this.fpSpread1_Sheet1.ActiveRowIndex;
                if (row < 0)
                    return;
                if (Delete(row) != -1)
                {
                    this.fpSpread1_Sheet1.Rows.Remove(row, 1);
                }
            }
            else if (e.ClickedItem.Text == "����˵��")
            {
                try
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\" + explainNote);
                }
                catch
                {
                    MessageBox.Show("�Ҳ���˵���ĵ�������ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private int Delete(int row)
        {
            if (fpSpread1_Sheet1.Rows[row].Locked)
            {
                MessageBox.Show("��û��ɾ��������Ŀ��Ȩ�ޣ�\r\n������������ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }

            DialogResult Result = MessageBox.Show("ȷ��ɾ�������ݣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (Result != DialogResult.OK)
            {
                return -1;
            }

            FS.FrameWork.Models.NeuObject dept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

            OrderSubtblNew objSubtbl = new OrderSubtblNew();

            objSubtbl.ID = this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemCode].Text;

            objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text);

            objSubtbl.Oper.ID = this.fpSpread1_Sheet1.Cells[row , (int)SubtblColumns.Oper].Text;

            objSubtbl.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text);
            FS.HISFC.Models.Base.Item itemObj = (FS.HISFC.Models.Base.Item)itemHelper.GetObjectFromID(objSubtbl.ID);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if(this.subtblMgr.DeleteAdditionalItem(itemObj,dept.ID,false,this.currentUsage.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Refresh(this.currentUsage);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        private void tvPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = this.tvUsage.SelectedNode;

            if (current == null || current.Parent == null)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

                this.fpSpread1_Sheet1.Tag = null;
            }
            else
            {
                FS.FrameWork.Models.NeuObject usage = current.Tag as FS.FrameWork.Models.NeuObject;

                this.Refresh(usage);
            }
        }

        private void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (this.ucInputItem1.FeeItem == null)
            {
                return;
            }

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ���÷�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //if (!oper.IsManager)
            //{
            //    if (isAllowEdit == 0 ||
            //        (isAllowEdit == 2 &&
            //        SOC.HISFC.BizProcess.Cache.Common.GetDept(oper.Dept.ID).DeptType.ID.ToString() != "I"))
            //    {
            //        MessageBox.Show("û��ά����Ȩ�ޣ�");
            //        return;
            //    }
            //}

            //if (isCheckRepeatItem)
            //{
            //    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)//����Ƿ��ظ�
            //    {
            //        FS.FrameWork.Models.NeuObject obj = this.fpSpread1.ActiveSheet.Rows[i].Tag as FS.FrameWork.Models.NeuObject;
            //        if (obj.Memo == this.ucInputItem1.FeeItem.ID)
            //        {
            //            MessageBox.Show("�Ѵ�����Ŀ" + this.ucInputItem1.FeeItem.Name + "������ѡ��");
            //            return;//����ظ� ����
            //        }
            //    }
            //}

            this.AddItemToFp(this.ucInputItem1.FeeItem, 0);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.ItemName)
                {
                    this.PopItem(this.alItems, (int)SubtblColumns.ItemName);
                }
            }
        }

        public override int Export(object sender, object neuObject)
        {
            ExportToExcel();
            return base.Export(sender, neuObject);
        }

        #endregion

        #region ����

        private FS.FrameWork.Models.NeuObject GetDept()
        {
            if (neuListBox1.SelectedItem == null)
            {
                return oper.Dept;
            }
            return this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;
        }

        /// <summary>
        /// �����Ŀ��farpoint
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="row"></param>
        private void AddItemToFp(FS.FrameWork.Models.NeuObject Item, int row)
        {
            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ���÷�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.HISFC.Models.Base.Item myItem = null;

            if (Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug) ||
                Item.GetType() == typeof(FS.HISFC.Models.Base.Item))
            {
                if (itemHelper != null && itemHelper.ArrayObject.Count > 0)
                {
                    myItem = itemHelper.GetObjectFromID(Item.ID) as FS.HISFC.Models.Base.Item;
                }
                if (myItem == null)
                {
                    myItem = this.feeMgr.GetItem(Item.ID);
                    if (myItem == null)
                    {
                        MessageBox.Show(feeMgr.Err);
                        return;
                    }
                }

                if (!myItem.IsValid)
                {
                    MessageBox.Show(myItem.Name + "�Ѿ�ͣ�ã�������ѡ��");
                    return;
                }

                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //�жϸ���Ŀ�Ƿ����
                if (!myItem.IsValid)
                {
                    MessageBox.Show("��Ŀ" + myItem.Name + "�Ѿ�ͣ�ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //��fpSpread1_Sheet1�м�������
                obj.ID = myItem.ID;
                obj.Name = myItem.Name;
                this.fpSpread1.ActiveSheet.Rows.Add(row, 1);

                this.fpSpread1_Sheet1.Rows[row].Tag = myItem;

                
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemCode].Text = myItem.ID;

                //��Ŀ����
                if (!string.IsNullOrEmpty(myItem.Specs))
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = "��" + myItem.Specs + "��" + myItem.Name + "��" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = myItem.Name + "��" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                }
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Tag = myItem.ID;
                //����
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = "1";

                //����
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.UnitPrice].Text = myItem.Price.ToString();

                //��λ
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Unit].Text = myItem.PriceUnit;

                //����Ա
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Oper].Text = (this.orderManager.Operator as FS.HISFC.Models.Base.Employee).ID;
                //����ʱ��
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text = this.orderManager.GetSysDateTime().ToString();

            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.ItemName)
                    {
                        this.PopItem(this.alItems, (int)SubtblColumns.ItemName);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ��������ѡ��
        /// </summary>
        private void PopItem(ArrayList al, int index)
        {
            if (fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Locked)
            {
                return;
            }

            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Locked
                && index == (int)SubtblColumns.ItemName)
            {
                return;
            }

            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                //��Ŀ
                if (index == (int)SubtblColumns.ItemName)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Tag = info.ID;
                    FS.HISFC.Models.Base.Item itemObj = info as FS.HISFC.Models.Base.Item;

                    //������Ŀ����
                    if (!string.IsNullOrEmpty(itemObj.Specs))
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Value = "��" + itemObj.Specs + "��" + itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.ItemName].Value = itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                    }
                    this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag = info;
                }
            }
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <param name="person"></param>
        private void Refresh(FS.FrameWork.Models.NeuObject usage)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);
            }

            try
            {
                //this.tabPage1.Text = usage.Name;
                this.lblDisplay.Text = usage.Name;
                this.currentUsage = usage;
                FS.FrameWork.Models.NeuObject dept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

                FS.HISFC.Models.Base.Item itemObj = new FS.HISFC.Models.Base.Item();

                alItem.Clear();
                alItem = this.subtblMgr.QueryAdditionalItem(false, usage.ID, dept.ID);
                if (alItem == null)
                {
                    MessageBox.Show(this.subtblMgr.Err);
                    return;
                }

                this.fpSpread1_Sheet1.Tag = usage;
                if (alItem != null && alItem.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Item obj in alItem)
                    {
                        itemObj = this.itemHelper.GetObjectFromID(obj.ID).Clone() as FS.HISFC.Models.Base.Item;
                        if (itemObj == null)
                        {
                            MessageBox.Show("������Ŀʧ�ܣ�" + obj.Name + obj.ID);
                            break;
                        }

                        this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                        int row = this.fpSpread1_Sheet1.RowCount - 1;
                        this.fpSpread1_Sheet1.Rows[row].Tag = obj;

                        //��Ŀ����
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemCode].Text = itemObj.ID;

                        if (!string.IsNullOrEmpty(itemObj.Specs))
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = "��" + itemObj.Specs + "��" + itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.ItemName].Text = itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                        }

                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.UnitPrice].Text = itemObj.Price.ToString();

                        //��λ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Unit].Text = itemObj.PriceUnit;

                        //����
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = obj.Qty.ToString();

                        //����Ա
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Oper].Text = obj.User01;

                        //����ʱ��
                        this.fpSpread1_Sheet1.SetValue(row, (int)SubtblColumns.OperTime, obj.User02);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + this.subtblMgr.Err);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveData()
        {
            FS.FrameWork.Models.NeuObject usage = this.currentUsage as FS.FrameWork.Models.NeuObject;

            FS.FrameWork.Models.NeuObject dept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

            this.fpSpread1.StopCellEditing();

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ����Ŀ!", "��ʾ");
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //��ʼ����
            try
            {
                this.subtblMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Base.Item objSubtbl = null;
                //��ȫ��ѭ������
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    objSubtbl = new FS.HISFC.Models.Base.Item();

                    //��Ŀ����
                    objSubtbl.ID = (this.fpSpread1_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject).ID;
                    //����
                    objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Qty].Text);
                    //����Ա					
                    objSubtbl.User01 = (this.orderManager.Operator as FS.HISFC.Models.Base.Employee).ID;
                    //����ʱ��
                    objSubtbl.User02 = this.subtblMgr.GetDateTimeFromSysDateTime().ToString(); ;
                    //ÿ������λ
                    objSubtbl.PriceUnit = this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Unit].Text;

     

                    #endregion
                    if (this.subtblMgr.InsertAdditionalItem(objSubtbl, dept.ID, false, this.currentUsage.ID, "", 0) == -1)
                    {
                        if (this.subtblMgr.UpdateAdditionalItem(objSubtbl, dept.ID, false, this.currentUsage.ID, "", 0) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.subtblMgr.Err, "��ʾ");
                            return;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

               
                alItem.Clear();
                alItem = this.subtblMgr.QueryAdditionalItem(false, usage.ID, dept.ID);
                this.Refresh(usage);
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
            MessageBox.Show("����ɹ�!", "��ʾ");
        }

        /// <summary>
        /// ��ȡ���ڵ�ID
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSelectData(int row, int column)
        {
            for (int j = 0; j < (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items.Length; j++)
            {
                string item = (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[j];

                if (item == this.fpSpread1_Sheet1.Cells[row, column].Text)
                {
                    return j.ToString();
                }
            }
            return "0";
        }


        /// <summary>
        /// ������:"����"��ť�������
        /// </summary>
        private void ExportToExcel()
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û��Ҫ����������!"), "��Ϣ");

                return;
            }

            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.xls";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #region ����

        /// <summary>
        /// ���Ĺ���
        /// </summary>
        private void SubFilter(object sender, EventArgs e)
        {
            string area = this.cmbFilterArea.Text;
            string orderType = this.cmbFilterOrderType.Text;
            string dept = this.cmbFilterDept.Text;
            string item = this.txtFilterItem.Text.Trim();

            //DataView dView = this.fpSpread1_Sheet1.DataSource;
        }

        #endregion


        /// <summary>
        /// ��ҩƷ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.dtUndrug.DefaultView.RowFilter = "���� like '%" + this.txtFilter_Undrug.Text.Trim() + "%' or ���� like '%" +
                this.txtFilter_Undrug.Text.Trim() + "%' or ��Ŀ���� like '%" + this.txtFilter_Undrug.Text.Trim() + "%' or ƴ���� like '%"
                + this.txtFilter_Undrug.Text.Trim() + "%' or ����� like '%" + this.txtFilter_Undrug.Text.Trim() + "%'";
        }

        private void fpUndrug_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.Models.NeuObject undrugObj = new FS.FrameWork.Models.NeuObject();
            undrugObj.ID = this.fpUndrug_Sheet1.GetText(e.Row, 8);
            undrugObj.Name = this.fpUndrug_Sheet1.GetText(e.Row, 0);
            this.Refresh(undrugObj);
        }

        private void fpDrug_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.Models.NeuObject drugObj = new FS.FrameWork.Models.NeuObject();
            drugObj.ID = this.fpDrug_Sheet1.GetText(e.Row, 8);
            drugObj.Name = this.fpDrug_Sheet1.GetText(e.Row, 0);
            this.Refresh(drugObj);
        }

        private void txtFilter_Drug_TextChanged(object sender, EventArgs e)
        {
            this.dtDrug.DefaultView.RowFilter = "���� like '%" + this.txtFilter_Drug.Text.Trim() + "%' or ���� like '%" +
                this.txtFilter_Drug.Text.Trim() + "%' or ��Ŀ���� like '%" + this.txtFilter_Drug.Text.Trim() + "%' or ƴ���� like '%"
                + this.txtFilter_Drug.Text.Trim() + "%' or ����� like '%" + this.txtFilter_Drug.Text.Trim() + "%'";
        }

        private void neuListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh(this.currentUsage);
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    public enum SubtblColumns
    {
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        ItemCode = 0,

        /// <summary>
        /// ��Ŀ���ƣ����
        /// </summary>
        ItemName,

        /// <summary>
        /// ����
        /// </summary>
        Qty,

        /// <summary>
        /// ��λ
        /// </summary>
        Unit,

        /// <summary>
        /// ����
        /// </summary>
        UnitPrice,

        /// <summary>
        /// ����Ա
        /// </summary>
        Oper,

        /// <summary>
        /// ����ʱ��
        /// </summary>
        OperTime,

    }
}
