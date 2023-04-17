using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// �㼶��ʽ����ҽ�� yangw 20101024
    /// {1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 
    /// </summary>
    public partial class tvItemLevel : FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        public tvItemLevel()
        {
            InitializeComponent();
            this.ImageList = imageList1;
        }

        public tvItemLevel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #region ����
        FS.HISFC.BizLogic.Manager.ItemLevel itemLevelManager = new FS.HISFC.BizLogic.Manager.ItemLevel();

        /// <summary>
        /// �޸�ǰ��������
        /// </summary>
        string labelName = "";
        
        /// <summary>
        /// 0 ȫ����1���2סԺ
        /// </summary>
        private int inOutType = 0;

        public int InOutType
        {
            get 
            { 
                return inOutType; 
            }
            set 
            { 
                inOutType = value; 
            }
        }

        private bool isEdit = false;

        public bool IsEdit
        {
            set
            {
                this.isEdit = value;
            }
            get
            {
                return isEdit;
            }
        }
        

        /// <summary>
        /// �ֲ�Ĵ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject levelClass = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ֲ�Ĵ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject LevelClass
        {
            get { return levelClass; }
            set { levelClass = value; }
        }

        #endregion

        #region ����
        public void RefreshGroupByClass()
        {
            if (string.IsNullOrEmpty(levelClass.ID))
            {
                return;
            }
            this.Nodes.Clear();
            TreeNode rootNode = new TreeNode(levelClass.Name);
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 1;
            rootNode.Tag = null;
            this.Nodes.Add(rootNode);

            #region "������� ��ȡ��ǰ���ҵĿ������� ��ǰ����Ա�ĸ�������  ȫԺ����"
            ArrayList alFolder = this.itemLevelManager.GetAllFirstLVFolder(this.inOutType, levelClass.ID);

            if (alFolder == null)
            {
                return;
            }

            #endregion

            try
            {
                TreeNode node;

                FS.HISFC.Models.Fee.Item.ItemLevel info;
                for (int i = 0; i < alFolder.Count; i++)
                {
                    info = alFolder[i] as FS.HISFC.Models.Fee.Item.ItemLevel;
                    if (info == null)
                    {
                        continue;
                    }
                    node = new TreeNode(info.Name);
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 3;
                    node.Tag = info;
					
                    this.Nodes[0].Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RefreshGroupByClass" + ex.Message);
                return;
            }

            this.Nodes[0].ExpandAll();
            this.Nodes[0].Expand();
        }     
        #endregion

        #region �¼�
        protected override void OnMouseUp(MouseEventArgs e)
        {

            base.OnMouseUp(e);

            if (!this.isEdit)
            {
                return;
            }

            this.SelectedNode = this.GetNodeAt(e.X, e.Y);

            if (this.SelectedNode == null || this.SelectedNode.Tag == null)
                this.LabelEdit = false;
            else
                this.LabelEdit = true;

            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (this.SelectedNode.Tag == null)
                    {
                        ContextMenu m = new ContextMenu();
                        MenuItem AddItem = new MenuItem("�����ļ���");
                        AddItem.Click += new EventHandler(AddItem_Click);
                        m.MenuItems.Add(AddItem);

                        if (this.SelectedNode != this.Nodes[0])
                        {

                            MenuItem deleteItem = new MenuItem("ɾ��");
                            deleteItem.Click += new EventHandler(deleteItem_Click);
                            m.MenuItems.Add(deleteItem);
                        }

                        this.ContextMenu = m;
                        this.ContextMenu.Show(this, new Point(e.X, e.Y));
                    }
                    else
                    {
                        if (this.SelectedNode.Tag.GetType() == typeof(FS.HISFC.Models.Fee.Item.ItemLevel))
                        {
                            //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD} �༶���� yangw 20100916
                            FS.HISFC.Models.Fee.Item.ItemLevel ilTmp = this.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;
                            if (ilTmp.UserCode == "F")
                            {
                                if (this.SelectedNode.Nodes.Count > 0)
                                {
                                    ContextMenu m = new ContextMenu();

                                    MenuItem AddItem = new MenuItem("�����ļ���");
                                    AddItem.Click += new EventHandler(AddItem_Click);
                                    m.MenuItems.Add(AddItem);

                                    this.ContextMenu = m;
                                    this.ContextMenu.Show(this, new Point(e.X, e.Y));
                                }
                                else
                                {
                                    ContextMenu m = new ContextMenu();
                                    MenuItem AddItem = new MenuItem("�����ļ���");
                                    AddItem.Click += new EventHandler(AddItem_Click);
                                    m.MenuItems.Add(AddItem);

                                    MenuItem deleteItem = new MenuItem("ɾ��");
                                    deleteItem.Click += new EventHandler(deleteItem_Click);
                                    m.MenuItems.Add(deleteItem);

                                    this.ContextMenu = m;
                                    this.ContextMenu.Show(this, new Point(e.X, e.Y));
                                }
                            }
                            else
                            {
                                ContextMenu m = new ContextMenu();

                                MenuItem deleteItem = new MenuItem("ɾ��");
                                deleteItem.Click += new EventHandler(deleteItem_Click);
                                m.MenuItems.Add(deleteItem);
                                this.ContextMenu = m;
                                this.ContextMenu.Show(this, new Point(e.X, e.Y));
                            }
                        }
                        else
                        {
                            this.ContextMenu = null;
                        }
                    }
                }
            }
            catch { }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.ContextMenu != null)
            {
                if (this.ContextMenu.MenuItems.Count > 0)
                {
                    this.ContextMenu.MenuItems.Clear();
                }
            }
        }

        void AddItem_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode();
            node.ImageIndex = 2;
            node.SelectedImageIndex = 3;
            FS.HISFC.Models.Fee.Item.ItemLevel itemLevel = new FS.HISFC.Models.Fee.Item.ItemLevel();
            itemLevel.ID = this.itemLevelManager.GetNewFolderID();
            itemLevel.Name = "�½��ļ���";
            itemLevel.InOutType = this.inOutType;
            if (this.SelectedNode == this.Nodes[0])
            {
                itemLevel.Dept.ID = (this.itemLevelManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                itemLevel.Owner = this.itemLevelManager.Operator;
                itemLevel.ParentID = "ROOT";                //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD} �༶���� yangw 20100916
                itemLevel.LevelClass = this.levelClass;
            }
            else
            {//{C2922531-DEE7-43a0-AB7A-CDD7C58691BD} �༶���� yangw 20100916
                FS.HISFC.Models.Fee.Item.ItemLevel itemLevelSelected = this.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;

                itemLevel.Dept = itemLevelSelected.Dept;

                itemLevel.Owner = this.itemLevelManager.Operator;
                itemLevel.ParentID = itemLevelSelected.ID;
                itemLevel.LevelClass = this.levelClass;
            }
            itemLevel.UserCode = "F";
            if (this.itemLevelManager.SetNewFolder(itemLevel) < 0)
            {
                MessageBox.Show("�����ļ���ʧ�ܣ�");
                return;
            }
            node.Text = itemLevel.Name;
            node.Tag = itemLevel;
            this.SelectedNode.Nodes.Add(node);
        }


        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                FS.HISFC.Models.Fee.Item.ItemLevel info = this.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;
                if (info.UserCode == "F")//�ļ���
                {
                    if (MessageBox.Show("�Ƿ�ɾ���ļ���" + info.Name, "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        if (this.itemLevelManager.deleteFolder(info) < 0)
                        {
                            MessageBox.Show(this.itemLevelManager.Err);
                        }
                        this.RefreshGroupByClass();
                    }
                }
                //else
                //{
                //    if (MessageBox.Show("�Ƿ�ɾ������" + info.Name, "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                //    {
                //        if (this.itemLevelManager.DeleteGroup(info) == -1)
                //        {
                //            MessageBox.Show(this.groupManager.Err);
                //        }
                //        this.RefreshGroupByClass();
                //    }
                //}
            }
            catch { }
        }

        //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD} �༶���� yangw 20100916
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            object o = this.SelectedNode.Tag;
            if (o != null)
            {
                if (o.GetType() == typeof(FS.HISFC.Models.Fee.Item.ItemLevel))
                {
                    this.SelectedNode.Nodes.Clear();
                    FS.HISFC.Models.Fee.Item.ItemLevel info = o as FS.HISFC.Models.Fee.Item.ItemLevel;
                    if (info.UserCode == "F")
                    {

                        #region ���ش��ļ������������

                        ArrayList alFolder = this.itemLevelManager.GetAllFolderByFolderID(info.ID);

                        if (alFolder == null)
                        {
                            return;
                        }

                        try
                        {
                            TreeNode node;

                            FS.HISFC.Models.Fee.Item.ItemLevel myGroup;
                            for (int i = 0; i < alFolder.Count; i++)
                            {
                                myGroup = alFolder[i] as FS.HISFC.Models.Fee.Item.ItemLevel;
                                if (info == null)
                                {
                                    continue;
                                }
                                node = new TreeNode(myGroup.Name);
                                node.ImageIndex = 2;
                                node.SelectedImageIndex = 3;
                                node.Tag = myGroup;

						
                                this.SelectedNode.Nodes.Add(node);
                                
                            }
                            //ArrayList alGroup = this.groupManager.GetGroupByFolderID(info.ID);
                            //if (alGroup != null && alGroup.Count > 0)
                            //{
                            //    for (int j = 0; j < alGroup.Count; j++)
                            //    {
                            //        FS.HISFC.Models.Fee.Item.ItemLevel group = alGroup[j] as FS.HISFC.Models.Fee.Item.ItemLevel;
                            //        if (group == null)
                            //        {
                            //            continue;
                            //        }
                            //        TreeNode temNode = new TreeNode(group.Name);
                            //        temNode.ImageIndex = 10;
                            //        temNode.SelectedImageIndex = 11;
                            //        temNode.Tag = group;
                            //        this.SelectedNode.Nodes.Add(temNode);
                            //    }
                            //}

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("RefreshGroupByClass" + ex.Message);
                            return;
                        }

                        this.SelectedNode.Expand();

                        #endregion

                    }
                }
            }
            base.OnAfterSelect(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            if (this.SelectedNode == null)
            {
                this.labelName = "";
            }

            this.labelName = this.SelectedNode.Text;

            base.OnBeforeLabelEdit(e);
        }

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            if (!isEdit)
            {
                return;
            }

            if (e.Label == null)
            {
                this.LabelEdit = false;
                return;
            }

            #region ������������Ȩ�޿��� --2007-11-21 zhangqi

            FS.HISFC.Models.Fee.Item.ItemLevel itemLevel = this.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;

                DialogResult r = MessageBox.Show("�ڵ������Ѿ��޸ģ��Ƿ񱣴棿", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (r == DialogResult.Cancel)
                {
                    this.LabelEdit = false;
                    //{3E29ADED-FB2D-4243-B525-BBDD79D85C2B} 
                    this.SelectedNode.Text = this.labelName;
                    this.RefreshGroupByClass();
                    return;
                }

                if ((this.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel).UserCode == "F")
                {
                    FS.HISFC.Models.Fee.Item.ItemLevel tem = this.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;
                    tem.Name = e.Label;
                    tem.LevelClass = this.levelClass;
                    if (this.itemLevelManager.updateFolder(tem) <= 0)
                    {
                        MessageBox.Show("�ļ������Ƹ���ʧ�ܡ�", "��ʾ");
                    }
                    else
                    {
                        MessageBox.Show("�ļ������Ƹ��³ɹ���", "��ʾ");
                    }
                }
                //else
                //{
                //    string GroupId = (this.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel).ID;
                //    if (groupManager.UpdateGroupName(GroupId, e.Label) > 0)
                //        MessageBox.Show("�������Ƹ��³ɹ�", "��ʾ");
                //    else
                //    {
                //        MessageBox.Show("����ʧ��", "��ʾ");
                //    }
                //}
            #endregion

            this.LabelEdit = false;
          
        }

        //protected override void OnDoubleClick(EventArgs e)
        //{
            
        //    object o = this.SelectedNode.Tag;
        //    if (o != null)
        //    {
        //        //if (o.GetType() == typeof(FS.HISFC.Models.Fee.Item.ItemLevel))
        //        //{
        //        //    FS.HISFC.Models.Fee.Item.ItemLevel info = o as FS.HISFC.Models.Fee.Item.ItemLevel;
        //        //    Forms.frmSelectGroup fSelect = new FS.HISFC.Components.Common.Forms.frmSelectGroup(info);

        //        //    fSelect.InpatientType = this.inpatientType;
        //        //    if (fSelect.ShowDialog() == DialogResult.OK)
        //        //    {
        //        //        try
        //        //        {
        //        //            if (SelectOrder != null)
        //        //                SelectOrder(fSelect.Orders);
        //        //        }
        //        //        catch { }
        //        //    }
        //        //}
        //    }
        //}

        #region ����
        //private void tvDoctorGroup_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        //{
        //    e.Effect = System.Windows.Forms.DragDropEffects.Move;
        //}

        //private void tvDoctorGroup_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        //{
        //    System.Drawing.Point position = new Point(0, 0);
        //    position.X = e.X;
        //    position.Y = e.Y;
        //    position = this.PointToClient(position);
        //    TreeNode dropNode = this.GetNodeAt(position);
        //    this.SelectedNode = dropNode;
        //    this.Focus();
        //}

        //private void tvDoctorGroup_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        //��ʼ����"Drag"����
        //        DoDragDrop((TreeNode)e.Item, DragDropEffects.Move);
        //    }
        //}

        //private void tvDoctorGroup_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        //{
        //    TreeNode temp = new TreeNode();
        //    //�õ�Ҫ�ƶ��Ľڵ�
        //    TreeNode moveNode = (TreeNode)e.Data.GetData(temp.GetType());
        //    //ת������Ϊ�ؼ�treeview������
        //    Point position = new Point(0, 0);
        //    position.X = e.X;
        //    position.Y = e.Y;
        //    position = this.PointToClient(position);

        //    //�õ��ƶ���Ŀ�ĵصĽڵ�
        //    TreeNode aimNode = this.GetNodeAt(position);
        //    if (aimNode == null)//�������� ����
        //    {
        //        return;
        //    }
        //    //			if(aimNode.Parent != moveNode.Parent) //����ͬһ���� ����
        //    //			{
        //    //				if(aimNode.Parent.Parent!= moveNode.Parent.Parent)
        //    //				{
        //    //					return;
        //    //				}
        //    //			}
        //    if (moveNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel == null) //�����׸��ڵ� ����
        //    {
        //        return;
        //    }
        //    if ((moveNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel).UserCode == "F")//���ļ��нڵ� ����
        //    {
        //        return;
        //    }
        //    if (aimNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel == null)//Ŀ��ڵ� �Ǹ��ڵ�
        //    {
        //        return;
        //    }
        //    if ((aimNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel).UserCode != "F")//Ŀ��ڵ㲻���ļ���
        //    {
        //        return;
        //    }
        //    FS.HISFC.Models.Fee.Item.ItemLevel g1 = moveNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;
        //    FS.HISFC.Models.Fee.Item.ItemLevel g2 = aimNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;

        //    if (IsDragEnable(aimNode, moveNode) == true)
        //    {
        //        if (aimNode != moveNode)
        //        {
        //            FS.HISFC.Models.Fee.Item.ItemLevel temGroup = aimNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;
        //            FS.HISFC.Models.Fee.Item.ItemLevel tempGroup = moveNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel;

        //            if (temGroup == null || tempGroup == null)
        //            {
        //                return;
        //            }
        //            //try
        //            //{
        //            //    if (this.groupManager.UpdateGroupFolderID(tempGroup.ID, temGroup.ID) < 0)
        //            //    {
        //            //        MessageBox.Show("�϶����׵��ļ���ʧ�ܡ�");
        //            //        return;
        //            //    }
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    MessageBox.Show("�϶����׵��ļ���ʧ�ܡ�" + ex.Message);
        //            //    return;
        //            //}
        //            this.Nodes.Remove(moveNode);
        //            aimNode.Nodes.Add(moveNode);
        //        }
        //    }
        //}

        ///// <summary>
        ///// �ж��Ƿ�����϶���Ŀ��ڵ㣬��������򷵻�true������Ϊfalse;
        ///// �жϸ����ǣ�Ŀ��ڵ㲻���Ǳ��϶��Ľڵ�ĸ��׽ڵ㣡
        ///// </summary>
        //private bool IsDragEnable(TreeNode aimNode, TreeNode oriNode)
        //{
        //    while (aimNode != null)
        //    {
        //        if (aimNode.Parent != oriNode)
        //        {
        //            aimNode = aimNode.Parent;
        //            IsDragEnable(aimNode, oriNode);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        #endregion
        #endregion
    }
}
