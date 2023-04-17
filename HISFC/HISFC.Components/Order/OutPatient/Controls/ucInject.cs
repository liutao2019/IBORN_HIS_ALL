using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// �����㷨ά��
    /// </summary>
    public partial class ucInject : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInject()
        {
            InitializeComponent();
        }

        #region ����

        ArrayList al = new ArrayList();

        FS.FrameWork.WinForms.Forms.ToolBarService toolbar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        ArrayList alItem = new ArrayList();
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����������Ժ�....");
            Application.DoEvents();
            this.initTree();
            this.ucInputItem1.Init();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
                al = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE);
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
            return this.toolbar;
        }

        private void ucInject_Load(object sender, EventArgs e)
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
            this.saveData();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                    return;
                int row = this.neuSpread1_Sheet1.ActiveRowIndex;
                if (row < 0)
                    return;
                DialogResult Result = MessageBox.Show("ȷ��ɾ�������ݣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (Result == DialogResult.OK)
                {
                    this.neuSpread1_Sheet1.Rows.Remove(row, 1);
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void tvPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = this.tvPatientList1.SelectedNode;

            if (current == null || current.Parent == null)
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);

                this.neuSpread1_Sheet1.Tag = null;
            }
            else
            {
                FS.FrameWork.Models.NeuObject usage = current.Tag as FS.FrameWork.Models.NeuObject;

                this.refresh(usage);
            }
        }

        private void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (this.ucInputItem1.FeeItem == null) return;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)//����Ƿ��ظ�
            {
                FS.FrameWork.Models.NeuObject obj = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.FrameWork.Models.NeuObject;
                if (obj.Memo == this.ucInputItem1.FeeItem.ID)
                {
                    MessageBox.Show("�Ѵ�����Ŀ" + this.ucInputItem1.FeeItem.Name + "������ѡ��");
                    return;//����ظ� ����
                }
            }
            this.AddItemToFp(this.ucInputItem1.FeeItem, 0);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DialogResult Result = MessageBox.Show("ȷ��ɾ�������ݣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (Result == DialogResult.OK)
            {
                this.neuSpread1_Sheet1.Rows.Remove(e.Row, 1);
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �����Ŀ��farpoint
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="row"></param>
        private void AddItemToFp(object Item, int row)
        {
            if (this.neuSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ���÷�!", "��ʾ");
                return;

            }

            if (Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug) ||
                Item.GetType() == typeof(FS.HISFC.Models.Base.Item))
            {
                FS.HISFC.Models.Base.Item myItem = Item as FS.HISFC.Models.Base.Item;
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //�жϸ���Ŀ�Ƿ����
                if (!myItem.IsValid)
                {
                    MessageBox.Show("��Ŀ" + myItem.Name + "�Ѿ�ͣ�ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //��neuSpread1_Sheet1�м�������
                obj.ID = myItem.ID;
                obj.Name = myItem.Name;
                this.neuSpread1.ActiveSheet.Rows.Add(row, 1);
                this.neuSpread1_Sheet1.Cells[row, 0].Text = myItem.Name;
                this.neuSpread1_Sheet1.Cells[row, 0].Tag = myItem.ID;
                this.neuSpread1_Sheet1.Rows[row].Tag = obj;
                this.neuSpread1_Sheet1.Cells[row, 1].Text = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Name;
                this.neuSpread1_Sheet1.Cells[row, 1].Tag = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).ID;
                this.neuSpread1_Sheet1.Cells[row, 2].Text = CacheManager.OutOrderMgr.GetSysDateTime();
            }
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <param name="person"></param>
        private void refresh(FS.FrameWork.Models.NeuObject usage)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }

            try
            {
                //this.tabPage1.Title = usage.Name;
                this.tabPage1.Text = usage.Name;
                this.neuSpread1_Sheet1.Tag = usage;

                alItem.Clear();
                alItem = CacheManager.FeeIntegrate.GetInjectInfoByUsage(usage.ID);// this.myOutPatient.GetInjectInfoByUsage(usage.ID);
                if (alItem != null && alItem.Count > 0)
                {
                    foreach (FS.HISFC.Models.Order.OrderSubtbl obj in alItem)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        int row = this.neuSpread1_Sheet1.RowCount - 1;
                        this.neuSpread1_Sheet1.Rows[row].Tag = obj.Item;
                        //��Ŀ����
                        this.neuSpread1_Sheet1.Cells[row, 0].Text = obj.Item.Name;//(row,0,obj.User01);
                        this.neuSpread1_Sheet1.Cells[row, 0].Tag = obj.Item.ID;//(row,0,obj.Memo);						
                        //����Ա
                        this.neuSpread1_Sheet1.SetValue(row, 1, SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(obj.Oper.ID).Name, false);
                        //����ʱ��
                        this.neuSpread1_Sheet1.SetValue(row, 2, obj.OperDate);

                        string qtyRule = (this.neuSpread1_Sheet1.Columns[3].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[obj.QtyRule];

                        this.neuSpread1_Sheet1.Cells[row, 3].Text = qtyRule;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + CacheManager.ConManager.Err);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void saveData()
        {
            #region ��֤���ݷ���ѡ����Ŀ��
            //��֤����
            //if (!this.Valid())
            //{
            //    this.neuSpread1.Focus();
            //    return;
            //}
            #endregion

            this.neuSpread1.StopCellEditing();

            if (this.neuSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ����Ŀ!", "��ʾ");
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //��ʼ����
            try
            {
                CacheManager.ConManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (alItem.Count > 0)
                {
                    if (CacheManager.FeeIntegrate.DelInjectInfo((this.neuSpread1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(CacheManager.FeeIntegrate.Err, "��ʾ");
                        return;
                    }
                }


                FS.HISFC.Models.Order.OrderSubtbl objInject = null;
                //ѭ������
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    objInject = new FS.HISFC.Models.Order.OrderSubtbl();

                    //�÷�����
                    objInject.Usage.ID = (this.neuSpread1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID;
                    //�÷�����
                    objInject.Usage.Name = (this.neuSpread1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).Name;
                    //��Ŀ����
                    objInject.Item.ID = (this.neuSpread1_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject).ID;
                    //��Ŀ����
                    objInject.Item.Name = this.neuSpread1_Sheet1.Cells[i, 0].Text.ToString();
                    //����Ա					
                    objInject.Oper.ID = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).ID;

                    for (int j = 0; j < (this.neuSpread1_Sheet1.Columns[3].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items.Length; j++)
                    {
                        string item = (this.neuSpread1_Sheet1.Columns[3].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[j];

                        if (item == this.neuSpread1_Sheet1.Cells[i, 3].Text)
                        {
                            objInject.QtyRule = j;
                            break;
                        }
                    }

                    if (CacheManager.FeeIntegrate.InsertInjectInfo(objInject) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(CacheManager.ConManager.Err, "��ʾ");
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                TreeNode current = this.tvPatientList1.SelectedNode;
                FS.FrameWork.Models.NeuObject usage = current.Tag as FS.FrameWork.Models.NeuObject;
                alItem.Clear();
                alItem = CacheManager.FeeIntegrate.GetInjectInfoByUsage(usage.ID);
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
        /// ��֤���ݿ��Ƿ����ظ�������()
        /// </summary>
        /// <param name="ItemID">��ĿID</param>
        /// <returns></returns>
        private bool Valid(string Item_ID)
        {
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    this.neuSpread1.StopCellEditing();
                    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        string ItemID = this.neuSpread1_Sheet1.Cells[i, 0].Tag.ToString();

                        if (ItemID == Item_ID)
                        {
                            this.neuSpread1_Sheet1.Rows[i].BackColor = Color.Red;
                            MessageBox.Show("����Ŀ�Ѵ��ڣ���Ŀ�����ظ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.neuSpread1_Sheet1.Rows[i].BackColor = Color.White;
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

        #endregion
    }
}
