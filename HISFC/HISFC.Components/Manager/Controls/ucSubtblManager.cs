using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Manager
{
    /// <summary>
    /// �����㷨ά��
    /// </summary>
    public partial class ucSubtblManager : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSubtblManager()
        {
            InitializeComponent();
        }

        #region ϵͳ�������

        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        Classes.SubtblManager subtblMgr = new Neusoft.HISFC.Components.Manager.Classes.SubtblManager();
        Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region ����

        ArrayList al = new ArrayList();

        Neusoft.FrameWork.WinForms.Forms.ToolBarService toolbar = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        ArrayList alItem = new ArrayList();

        /// <summary>
        /// �����б�
        /// </summary>
        ArrayList alDept = new ArrayList();

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        ArrayList alItems = new ArrayList();
        Neusoft.FrameWork.Public.ObjectHelper itemHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        Neusoft.HISFC.BizProcess.Integrate.Fee feeMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void Init()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����������Ժ�....");
            Application.DoEvents();
            this.initTree();
            this.ucInputItem1.Init();

            alDept = this.managerIntegrate.GetDepartment();
            if (alDept == null)
            {
                MessageBox.Show(managerIntegrate.Err);
            }
            else
            {
                Neusoft.FrameWork.Models.NeuObject allObj = new Neusoft.FrameWork.Models.NeuObject("ROOT", "ȫ��", "");
                alDept.Add(allObj);
                this.deptHelper.ArrayObject = alDept;
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
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

            //���÷�Χ��0 ���1 סԺ��2 ȫ��
            string[] arrayTemp = new string[3]{"����","סԺ","ȫ��"
            };
            comCellType1.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[0].CellType = comCellType1;

            //�鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
            arrayTemp = new string[3] { "ÿ����ȡ", "��һ����ȡ", "�ڶ��������" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType2.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[4].CellType = comCellType2;

            //�շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ��
            arrayTemp = new string[5] { "���̶�����", "�����Ժע", "������Ʒ����", "�����ҽ������", "��Ƶ��" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType3 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType3.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[5].CellType = comCellType3;

            //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
            arrayTemp = new string[3] { "������", "Ӥ��", "��Ӥ��" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType4 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType4.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[6].CellType = comCellType4;

            #endregion

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ��ʼ���÷�TreeView
        /// </summary>
        private void initTree()
        {
            this.tvPatientList1.Nodes.Clear();
            TreeNode root = new TreeNode("�÷�");
            root.ImageIndex = 40;
            this.tvPatientList1.Nodes.Add(root);
            //����÷��б�
            if (al != null)
            {
                al = managerIntegrate.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);
            }

            if (al != null)
            {
                foreach (Neusoft.FrameWork.Models.NeuObject obj in al)
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
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolbar.AddToolButton("ɾ��", "ɾ������", Neusoft.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolbar;
        }

        private void ucSubtblManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.Init();
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
                DialogResult Result = MessageBox.Show("ȷ��ɾ�������ݣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (Result == DialogResult.OK)
                {
                    this.fpSpread1_Sheet1.Rows.Remove(row, 1);
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void tvPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = this.tvPatientList1.SelectedNode;

            if (current == null || current.Parent == null)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

                this.fpSpread1_Sheet1.Tag = null;
            }
            else
            {
                Neusoft.FrameWork.Models.NeuObject usage = current.Tag as Neusoft.FrameWork.Models.NeuObject;

                this.Refresh(usage);
            }
        }

        private void ucInputItem1_SelectedItem(Neusoft.FrameWork.Models.NeuObject sender)
        {
            if (this.ucInputItem1.FeeItem == null)
                return;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)//����Ƿ��ظ�
            {
                Neusoft.FrameWork.Models.NeuObject obj = this.fpSpread1.ActiveSheet.Rows[i].Tag as Neusoft.FrameWork.Models.NeuObject;
                if (obj.Memo == this.ucInputItem1.FeeItem.ID)
                {
                    MessageBox.Show("�Ѵ�����Ŀ" + this.ucInputItem1.FeeItem.Name + "������ѡ��");
                    return;//����ظ� ����
                }
            }
            this.AddItemToFp(this.ucInputItem1.FeeItem, 0);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == 1)
                {
                    this.PopItem(this.alDept, 1);
                }
                else if (this.fpSpread1_Sheet1.ActiveColumnIndex == 2)
                {
                    this.PopItem(this.alItems, 2);
                }
            }
            //DialogResult Result = MessageBox.Show("ȷ��ɾ�������ݣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (Result == DialogResult.OK)
            //{
            //    this.fpSpread1_Sheet1.Rows.Remove(e.Row, 1);
            //}
        }

        #endregion

        #region ����

        /// <summary>
        /// �����Ŀ��farpoint
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="row"></param>
        private void AddItemToFp(Neusoft.FrameWork.Models.NeuObject Item, int row)
        {
            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ���÷�!", "��ʾ");
                return;
            }

            if (Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug) ||
                Item.GetType() == typeof(Neusoft.HISFC.Models.Base.Item))
            {
                Neusoft.HISFC.Models.Base.Item myItem = this.feeMgr.GetItem(Item.ID);
                if (myItem == null)
                {
                    MessageBox.Show(feeMgr.Err);
                    return;
                }
                else if (!myItem.IsValid)
                {
                    MessageBox.Show(myItem.Name + "�Ѿ�ͣ�ã�������ѡ��");
                    return;
                }

                Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
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

                //���÷�Χ��0 ���1 סԺ��3 ȫ��
                this.fpSpread1_Sheet1.Cells[row, 0].Text = "ȫ��";
                //���Ҵ��룬ȫԺͳһ����'ROOT'
                this.fpSpread1_Sheet1.Cells[row, 1].Text = "ȫ��";
                this.fpSpread1_Sheet1.Cells[row, 1].Tag = "ROOT";
                //������Ŀ����
                if (!string.IsNullOrEmpty(myItem.Specs))
                {
                    this.fpSpread1_Sheet1.Cells[row, 2].Text = "��" + myItem.Specs + "��" + myItem.Name + "��" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, 2].Text = myItem.Name + "��" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                }
                this.fpSpread1_Sheet1.Cells[row, 2].Tag = myItem.ID;
                //����
                this.fpSpread1_Sheet1.Cells[row, 3].Text = "1";
                //�鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
                this.fpSpread1_Sheet1.Cells[row, 4].Text = "ÿ����ȡ";
                //�շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ��
                this.fpSpread1_Sheet1.Cells[row, 5].Text = "���̶�����";
                //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
                this.fpSpread1_Sheet1.Cells[row, 6].Text = "������";
                //����Ա
                this.fpSpread1_Sheet1.Cells[row, 7].Text = (this.orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Name;
                //����ʱ��
                this.fpSpread1_Sheet1.Cells[row, 8].Text = this.orderManager.GetSysDateTime().ToString();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.fpSpread1_Sheet1.ActiveColumnIndex == 1)
                    {
                        this.PopItem(this.alDept, 1);
                    }
                    else if (this.fpSpread1_Sheet1.ActiveColumnIndex == 2)
                    {
                        this.PopItem(this.alItems, 2);
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
            Neusoft.FrameWork.Models.NeuObject info = new Neusoft.FrameWork.Models.NeuObject();
            if (Neusoft.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                //����
                if (index == 1)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 1].Tag = info.ID;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 1].Value = info.Name;
                }
                //��Ŀ
                else if (index == 2)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Tag = info.ID;
                    Neusoft.HISFC.Models.Base.Item itemObj = info as Neusoft.HISFC.Models.Base.Item;

                    //������Ŀ����
                    if (!string.IsNullOrEmpty(itemObj.Specs))
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Value = "��" + itemObj.Specs + "��" + itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Value = itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                    }
                    this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag = info;
                }
            }
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <param name="person"></param>
        private void Refresh(Neusoft.FrameWork.Models.NeuObject usage)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);
            }

            try
            {
                this.tabPage1.Text = usage.Name;
                Neusoft.HISFC.Models.Base.Item itemObj = new Neusoft.HISFC.Models.Base.Item();

                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("3", usage.ID, "ROOT");
                if (alItem == null)
                {
                    MessageBox.Show(this.subtblMgr.Err);
                    return;
                }

                this.fpSpread1_Sheet1.Tag = usage;
                if (alItem != null && alItem.Count > 0)
                {
                    foreach (Classes.OrderSubtbl obj in alItem)
                    {
                        itemObj = this.itemHelper.GetObjectFromID(obj.Item.ID) as Neusoft.HISFC.Models.Base.Item;
                        if (itemObj == null)
                        {
                            MessageBox.Show("������Ŀʧ�ܣ�" + obj.Item.Name + obj.Item.ID);
                            break;
                        }

                        this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                        int row = this.fpSpread1_Sheet1.RowCount - 1;
                        this.fpSpread1_Sheet1.Rows[row].Tag = obj.Item;

                        //��Χ
                        this.fpSpread1_Sheet1.Cells[row, 0].Text = (this.fpSpread1_Sheet1.Columns[0].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.Area)];
                        //����
                        this.fpSpread1_Sheet1.Cells[row, 1].Text = this.deptHelper.GetName(obj.Dept_code);
                        this.fpSpread1_Sheet1.Cells[row, 1].Tag = obj.Dept_code;
                        //��Ŀ
                        //this.fpSpread1_Sheet1.Cells[row, 2].Text = itemObj.Name + "��" + itemObj.Specs + "��";
                        if (!string.IsNullOrEmpty(itemObj.Specs))
                        {
                            this.fpSpread1_Sheet1.Cells[row, 2].Text = "��" + itemObj.Specs + "��" + itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, 2].Text = itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                        }

                        this.fpSpread1_Sheet1.Cells[row, 2].Tag = obj.Item.ID;
                        //����
                        this.fpSpread1_Sheet1.Cells[row, 3].Text = obj.Qty.ToString();

                        //�����鷶Χ
                        this.fpSpread1_Sheet1.Cells[row, 4].Text = (this.fpSpread1_Sheet1.Columns[4].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.CombArea)];
                        //������ȡ����
                        this.fpSpread1_Sheet1.Cells[row, 5].Text = (this.fpSpread1_Sheet1.Columns[5].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.FeeRule)];
                        //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
                        this.fpSpread1_Sheet1.Cells[row, 6].Text = (this.fpSpread1_Sheet1.Columns[6].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.LimitType)];
                        //����Ա
                        this.fpSpread1_Sheet1.SetValue(row, 7, managerIntegrate.GetEmployeeInfo(obj.Oper.ID).Name, false);
                        //����ʱ��
                        this.fpSpread1_Sheet1.SetValue(row, 8, obj.OperDate);
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
            #region ��֤���ݷ���ѡ����Ŀ��
            //��֤����
            //if (!this.Valid())
            //{
            //    this.fpSpread1.Focus();
            //    return;
            //}
            #endregion

            this.fpSpread1.StopCellEditing();

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ����Ŀ!", "��ʾ");
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //��ʼ����
            try
            {
                this.subtblMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                //��ȫ��ɾ��
                if (alItem.Count > 0)
                {
                    if (this.subtblMgr.DelSubtblInfo((this.fpSpread1_Sheet1.Tag as Neusoft.FrameWork.Models.NeuObject).ID) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.subtblMgr.Err, "��ʾ");
                        return;
                    }
                }

                Classes.OrderSubtbl objSubtbl = null;
                //��ȫ��ѭ������
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    objSubtbl = new Classes.OrderSubtbl();

                    //���÷�Χ��0 ���1 סԺ��3 ȫ��
                    objSubtbl.Area = this.GetSelectData(i, 0);
                    //�÷����࣬0 ҩƷ���÷���1 ��ҩƷ����Ŀ����
                    objSubtbl.TypeCode = ((Neusoft.FrameWork.Models.NeuObject)this.fpSpread1_Sheet1.Tag).ID;
                    //���Ҵ��룬ȫԺͳһ����'ROOT'
                    objSubtbl.Dept_code = this.fpSpread1_Sheet1.Cells[i, 1].Tag.ToString();
                    //��Ŀ����
                    objSubtbl.Item.ID = (this.fpSpread1_Sheet1.Rows[i].Tag as Neusoft.FrameWork.Models.NeuObject).ID;
                    //����
                    objSubtbl.Qty = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, 3].Text);
                    //�鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
                    objSubtbl.CombArea = this.GetSelectData(i, 4);
                    //�շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ��
                    objSubtbl.FeeRule = this.GetSelectData(i, 5);
                    //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
                    objSubtbl.LimitType = this.GetSelectData(i, 6);
                    //����Ա					
                    objSubtbl.Oper.ID = (this.orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).ID;
                    //����ʱ��
                    objSubtbl.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, 8].Text);

                    if (this.subtblMgr.InsertSubtblInfo(objSubtbl) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.subtblMgr.Err, "��ʾ");
                        return;
                    }
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();

                TreeNode current = this.tvPatientList1.SelectedNode;
                Neusoft.FrameWork.Models.NeuObject usage = current.Tag as Neusoft.FrameWork.Models.NeuObject;
                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("3", usage.ID, "ROOT");
            }
            catch (Exception e)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
            MessageBox.Show("����ɹ�!", "��ʾ");
        }

        /// <summary>
        /// ��֤���ݿ��Ƿ����ظ�������()
        /// </summary>
        /// <param name="ItemID">��ĿID</param>
        /// <returns></returns>
        private bool Valid(string Item_ID)
        {
            try
            {
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    this.fpSpread1.StopCellEditing();
                    for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                    {
                        string ItemID = this.fpSpread1_Sheet1.Cells[i, 0].Tag.ToString();

                        if (ItemID == Item_ID)
                        {
                            this.fpSpread1_Sheet1.Rows[i].BackColor = Color.Red;
                            MessageBox.Show("����Ŀ�Ѵ��ڣ���Ŀ�����ظ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.fpSpread1_Sheet1.Rows[i].BackColor = Color.White;
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
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

        #endregion
    }
}
