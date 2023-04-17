using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.HISFC.BizLogic.Privilege.Service;



namespace FS.HISFC.Components.Privilege
{
    public partial class AuthorizeResourceControl : UserControl
    {
        public string pageJudge = String.Empty;
        public Role currentRole = null;
        public Role parentRole = null;
        public List<Role> ChildList = new List<Role>();
        private RoleResourceMapping currentRoleResource = null;
        private List<RoleResourceMapping> currentRoleResourcList = null;
        private string MenuPageJudge = "MenuRes";
        private string WebPageJudge = "WebRes";
        private string UserPageJudge = "UserRes";
        private string DictionaryPageJudge = "DictionaryRes";
        private string ReportPageJudge = "ReportRes";


        public AuthorizeResourceControl()
        {
            InitializeComponent();
            nTreeListView1.Columns[nTreeListView1.Columns.Count - 1].Width = 1024;
            nTreeListView1.OwnerDraw = false;
            //nTreeListView1.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(ListView_ColumnHeader);
            //nTreeListView1.DrawItem += new DrawListViewItemEventHandler(ListView_DrawItem);
            //nTreeListView1.DrawSubItem += new DrawListViewSubItemEventHandler(ListView_DrawSubItem);
        }
        private static void ListView_ColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

            TreeListView current = sender as TreeListView;

            Graphics g = e.Graphics;
            Rectangle ColumnTitleBack = e.Bounds;
            ColumnTitleBack.Width = current.Size.Width;
            ColumnTitleBack.Height = ColumnTitleBack.Height - 1;

            e.Graphics.FillRectangle(new SolidBrush(FS.FrameWork.WinForms.Classes.Function.GetSysColor(FS.FrameWork.WinForms.Classes.EnumSysColor.LightBlue)), ColumnTitleBack);

            Font font = e.Font;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Far;
            Color fontcolor = System.Drawing.Color.Black;
            g.DrawString(e.Header.Text, font, new SolidBrush(fontcolor), e.Bounds, sf);
        }

        private static void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;	//����ϵͳĬ�Ϸ�ʽ������

        }

        private static void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;	//����ϵͳĬ�Ϸ�ʽ��������

        }

        public void AddRoleRes()
        {
            if (nTreeListView1.ItemsCount == 0 || nTreeListView1.SelectedItems.Count == 0)
            {
                //����Ĭ�ϸ��ڵ�
                currentRoleResource = new RoleResourceMapping();
                currentRoleResource.ParentId = "root";
                currentRoleResource.Id = "root";
                currentRoleResource.OrderNumber = 0;
                currentRoleResource.ValidState = "1";
            }
            else
            {
                currentRoleResource = nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping;
            }

            AddAuthorizeForm addAuthorizeForm = new AddAuthorizeForm(currentRole, parentRole, currentRoleResource, pageJudge);
            addAuthorizeForm.ShowDialog();
            if (addAuthorizeForm.DialogResult == DialogResult.OK)
            {
                AuthorizeResourceControl_Load(null, null);
            }
        }

        public void DelettAllRoleRes()
        {
            if (nTreeListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫɾ���Ľڵ㣡");
                return;
            }
            if (nTreeListView1.SelectedIndices[0] == 0)
            {
                MessageBox.Show("������ɾ�����ڵ㣡");
                return;

            }
            if (MessageBox.Show("�Ƿ�Ҫɾ���ýڵ�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            if (nTreeListView1.SelectedItems[0].Items.Count > 0)
            {
                if (MessageBox.Show("ɾ���ڵ�,��ɾ���������ڵ�,�Ƿ����?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            }

            PrivilegeService _proxy = Common.Util.CreateProxy();
            using (_proxy as IDisposable)
            {
                List<RoleResourceMapping> deleteList = GetALLItem(nTreeListView1.SelectedItems[0]);
                if (deleteList == null)
                {
                    deleteList = new List<RoleResourceMapping>();
                }
                deleteList.Add(nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping);
                try
                {
                    FrameWork.Management.PublicTrans.BeginTransaction();
                    _proxy.DeleteRoleResource(deleteList);
                    FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception e)
                {
                    FrameWork.Management.PublicTrans.RollBack();
                    throw e;
                }
            }

            LoadRoleResource();
        }

        public void DelettRoleRes()
        {
            if (nTreeListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫɾ���Ľڵ㣡");
                return;
            }
            if (nTreeListView1.SelectedIndices[0] == 0)
            {
                MessageBox.Show("������ɾ�����ڵ㣡");
                return;

            }
            if (MessageBox.Show("�Ƿ�Ҫɾ���ýڵ�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            PrivilegeService _proxy = Common.Util.CreateProxy();
            using (_proxy as IDisposable)
            {
                List<RoleResourceMapping> deleteList = GetALLItem(nTreeListView1.SelectedItems[0]);
                if (deleteList == null)
                {
                    try
                    {
                        FrameWork.Management.PublicTrans.BeginTransaction();
                        _proxy.DeleteRoleResource(nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping, ChildList);
                        FrameWork.Management.PublicTrans.Commit();
                    }
                    catch(Exception e)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        throw e;
                    }
                }
                else
                {
                    MessageBox.Show("��ɾ�����ӽڵ����ɾ���ýڵ㣡");
                    return;
                }

            }

            AuthorizeResourceControl_Load(null, null);
        }

        public void ModifyRoleRes()
        {
            if (nTreeListView1.SelectedIndices.Count == 0)
            {
                return;
            }
            if (nTreeListView1.SelectedIndices[0] == 0)
            {
                return;
            }
            currentRoleResource = nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping;
            AddAuthorizeForm updateAuthorizeForm = new AddAuthorizeForm(currentRole, parentRole, currentRoleResource, pageJudge, "UpdateRes");
            updateAuthorizeForm.ShowDialog();
            if (updateAuthorizeForm.DialogResult == DialogResult.OK)
            {
                LoadRoleResource();
            }
        }

        public void CopyParentRoleRes()
        {
            if (pageJudge == MenuPageJudge || pageJudge == WebPageJudge)
            {
                if (parentRole != null)
                {
                    InsertParentMenu(parentRole, currentRole, pageJudge);
                    LoadRoleResource();
                }
            }

        }

        //�����Ƿ����ӽڵ�
        List<RoleResourceMapping> GetALLItem(TreeListViewItem parent)
        {
            List<RoleResourceMapping> list = null;
            if ((parent != null) && (parent.Items.Count > 0))
            {
                list = new List<RoleResourceMapping>();
                foreach (TreeListViewItem item in parent.Items)
                {
                    list.Add(item.Tag as RoleResourceMapping);
                    List<RoleResourceMapping> childRoleResourceMappings = GetALLItem(item);
                    if ((childRoleResourceMappings != null) && (childRoleResourceMappings.Count > 0))
                    {
                        for (int count = 0; count < childRoleResourceMappings.Count; count++)
                        {
                            list.Add(childRoleResourceMappings[count]);
                        }
                    }
                }
            }
            return list;
        }

        private void LoadRoleResource()
        {
            nTreeListView1.Items.Clear();
            PrivilegeService _proxy = Common.Util.CreateProxy();
            using (_proxy as IDisposable)
            {
                if (currentRole != null)
                {
                    currentRoleResourcList = _proxy.QueryByTypeRoleId(pageJudge, currentRole.ID);
                }
            }



            TreeListViewItem rootNode = null;

            if (pageJudge == MenuPageJudge )
            {
                rootNode = new TreeListViewItem("��Ȩ�Ĳ˵���", 0);
            }
            if ( pageJudge == WebPageJudge)
            {
                rootNode = new TreeListViewItem("��ȨWeb�˵���",6);
            }
            if (pageJudge == UserPageJudge)
            {
                rootNode = new TreeListViewItem("��Ȩ���û���", 0);
            }
            if (pageJudge == DictionaryPageJudge)
            {
                rootNode = new TreeListViewItem("��Ȩ�ĳ�����", 3);
            }
            if (pageJudge == ReportPageJudge)
            {
                rootNode = new TreeListViewItem("��Ȩ�ı���", 0);
            }
            currentRoleResource = new RoleResourceMapping();

            currentRoleResource.ParentId = "root";
            currentRoleResource.Id = "root";
            currentRoleResource.OrderNumber = 0;
            currentRoleResource.ValidState = "1";
            rootNode.Tag = currentRoleResource;
            nTreeListView1.Items.Add(rootNode);

            if (currentRoleResourcList == null || currentRoleResourcList.Count == 0)
            {
                return;
            }

            ConstructTreeNode(rootNode, currentRoleResourcList);
            nTreeListView1.ExpandAll();
        }

        private void ConstructTreeNode(TreeListViewItem parent, List<RoleResourceMapping> currentRoleResourcList)
        {
            List<TreeListViewItem> childs = null;
            if (parent != null)
            {
                childs = new List<TreeListViewItem>();
                RoleResourceMapping parentRoleResourceMapping = parent.Tag as RoleResourceMapping;
                List<RoleResourceMapping> childRoleResourceList = new List<RoleResourceMapping>();

                foreach (RoleResourceMapping roleResourceMapping in currentRoleResourcList)
                {
                    if (roleResourceMapping.ParentId == parentRoleResourceMapping.Id)
                    {
                        childRoleResourceList.Add(roleResourceMapping);
                    }
                }

                for (int j = 1; j < childRoleResourceList.Count; j++)
                {
                    for (int i = 0; i < childRoleResourceList.Count - j; i++)
                    {
                        if (childRoleResourceList[i].OrderNumber > childRoleResourceList[i + 1].OrderNumber)
                        {
                            RoleResourceMapping roleResourceChange = new RoleResourceMapping();
                            roleResourceChange = childRoleResourceList[i];
                            childRoleResourceList[i] = childRoleResourceList[i + 1];
                            childRoleResourceList[i + 1] = roleResourceChange;
                            ;
                        }
                    }
                }

                foreach (RoleResourceMapping sequenceRoleResource in childRoleResourceList)
                {

                    TreeListViewItem item = new TreeListViewItem(sequenceRoleResource.Name, 1);
                    item.Tag = sequenceRoleResource;
                    item.SubItems.AddRange(GetRoleResInfo(sequenceRoleResource));
                    childs.Add(item);
                }

                foreach (TreeListViewItem item in childs)
                {
                    parent.Items.Add(item);
                    ConstructTreeNode(item, currentRoleResourcList);
                    initImage(item);
                }

            }
        }

        /// <summary>
        /// ��ʼ����ͼƬ
        /// </summary>
        /// <param name="item"></param>
        private void initImage(TreeListViewItem item)
        {
            
            if (pageJudge == MenuPageJudge)
            {
                if (item.ChildrenCount != 0)
                {
                    item.ImageIndex = 1;
                }
                else
                {
                    item.ImageIndex = 2;
                }
            }
            else if (pageJudge == DictionaryPageJudge)
            {
                if (item.ChildrenCount != 0)
                {
                    item.ImageIndex = 4;
                }
                else
                {
                    item.ImageIndex = 5;
                }
            }
            else
                if (pageJudge == WebPageJudge)
                {
                    if (item.ChildrenCount != 0)
                    {
                        item.ImageIndex = 7;
                    }
                    else
                    {
                        item.ImageIndex = 8;
                    }
                }
                else
            {
                item.ImageIndex = -1;
            }


        }

        private string[] GetRoleResInfo(RoleResourceMapping sequenceRoleRes)
        {
            string[] sequenceRoleResString = null;
            string ValidStateString = string.Empty;
            if (sequenceRoleRes.Resource.Id != null)
            {

                if (sequenceRoleRes.ValidState == "1")
                {
                    ValidStateString = "����";
                }
                else
                {
                    ValidStateString = "����";
                }
                sequenceRoleResString = new string[] { sequenceRoleRes.Resource.Name, sequenceRoleRes.Resource.WinName, sequenceRoleRes.Resource.DllName, sequenceRoleRes.Parameter, ValidStateString, sequenceRoleRes.OperCode, sequenceRoleRes.OperDate.ToString() };

            }
            else
            {
                sequenceRoleResString = new string[] { " ", " ", " ", sequenceRoleRes.Parameter, ValidStateString, sequenceRoleRes.OperCode, sequenceRoleRes.OperDate.ToString() };


            }

            return sequenceRoleResString;
        }

        private void InitMenuStrip(String Name)
        {
            AddResItem.Text = "���" + Name;
            ModifyResItem.Text = "�޸�" + Name;
            RemoveResItem.Text = "ɾ��" + Name;
            copyItems.Text = "���Ƹ�����ɫ��Ȩ";

            if (nTreeListView1.ItemsCount > 1)
            {
                copyItems.Enabled = false;
            }
            else
            {
                copyItems.Enabled = true;
            }

        }

        private void UpRoleRes()
        {
            if ((nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping).OrderNumber == 0)
            {
                MessageBox.Show("�����ƶ���");
                return;
            }

            List<RoleResourceMapping> newRoleResList = new List<RoleResourceMapping>();

            newRoleResList.Add(nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping);
            newRoleResList.Add(GetParentSamelevelNode(nTreeListView1.SelectedItems[0], nTreeListView1.SelectedItems[0].PrevVisibleItem).Tag as RoleResourceMapping);

            PrivilegeService _proxy = Common.Util.CreateProxy();
            using (_proxy as IDisposable)
            {
                try
                {
                    FrameWork.Management.PublicTrans.BeginTransaction();
                    _proxy.MoveSequence(newRoleResList);
                    FrameWork.Management.PublicTrans.Commit();
                }
                catch
                {
                    FrameWork.Management.PublicTrans.RollBack();
                    throw;
                }
            }

            LoadRoleResource();
        }

        private void DownRoleRes()
        {

            if ((nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping).OrderNumber == (GetSamelevelCount(nTreeListView1.SelectedItems[0]) - 1))
            {
                MessageBox.Show("�����ƶ���");
                return;
            }

            List<RoleResourceMapping> newRoleResList = new List<RoleResourceMapping>();
            newRoleResList.Add(nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping);
            newRoleResList.Add(GetChildSamelevelNode(nTreeListView1.SelectedItems[0], nTreeListView1.SelectedItems[0].NextVisibleItem).Tag as RoleResourceMapping);

            PrivilegeService _proxy = Common.Util.CreateProxy();
            using (_proxy as IDisposable)
            {
                try
                {
                    FrameWork.Management.PublicTrans.BeginTransaction();
                    _proxy.MoveSequence(newRoleResList);
                    FrameWork.Management.PublicTrans.Commit();
                }
                catch
                {
                    FrameWork.Management.PublicTrans.RollBack();
                    throw;
                }
            }

            LoadRoleResource();
        }

        private TreeListViewItem GetParentSamelevelNode(TreeListViewItem currentItem, TreeListViewItem parentItem)
        {
            while (currentItem.Level != parentItem.Level)
            {
                parentItem = parentItem.Parent;
            }

            return parentItem;
        }

        private TreeListViewItem GetChildSamelevelNode(TreeListViewItem currentItem, TreeListViewItem childItem)
        {
            while (currentItem.Level != childItem.Level)
            {
                childItem = childItem.NextVisibleItem;
            }

            return childItem;
        }

        private int GetSamelevelCount(TreeListViewItem currentItem)
        {
            int count = 0;
            foreach (RoleResourceMapping roleRes in currentRoleResourcList)
            {
                if ((currentItem.Tag as RoleResourceMapping).ParentId == roleRes.ParentId)
                {
                    count++;
                }
            }

            return count;
        }

        private void InsertParentMenu(Role parentRole, Role currentRole, String judgePage)
        {
            PrivilegeService proxy = Common.Util.CreateProxy();
            using (proxy as IDisposable)
            {
                try
                {
                    FrameWork.Management.PublicTrans.BeginTransaction();
                    proxy.CopyParentRes(parentRole, currentRole, judgePage);
                    FrameWork.Management.PublicTrans.Commit();
                }
                catch(Exception e)
                {
                    FrameWork.Management.PublicTrans.RollBack();
                    throw e;
                }
            }
        }


        private void AuthorizeResourceControl_Load(object sender, EventArgs e)
        {
            if (pageJudge == WebPageJudge)
            {
                nTreeListView1.Columns[2].Text = "URL";
                nTreeListView1.Columns[3].Width = 0;
                nTreeListView1.Columns[4].Width = 0;
            }

            LoadRoleResource();
            if (pageJudge == MenuPageJudge || pageJudge == WebPageJudge)
            {
                InitMenuStrip("�˵�");
            }
            if (pageJudge == UserPageJudge)
            {
                InitMenuStrip("�û�");
            }
            if (pageJudge == DictionaryPageJudge)
            {
                InitMenuStrip("����");

            }
            if (pageJudge == ReportPageJudge)
            {
                InitMenuStrip("����");
            }

        }

        private void AddResItem_Click(object sender, EventArgs e)
        {
            AddRoleRes();
        }

        private void ModifyResItem_Click(object sender, EventArgs e)
        {
            ModifyRoleRes();
        }

        private void RemoveResItem_Click(object sender, EventArgs e)
        {
            DelettRoleRes();
        }

        private void upResItem_Click(object sender, EventArgs e)
        {
            UpRoleRes();
        }

        private void downResItem_Click(object sender, EventArgs e)
        {
            DownRoleRes();
        }

        private void copyItems_Click(object sender, EventArgs e)
        {
            CopyParentRoleRes();
        }

        private void nTreeListView1_DoubleClick(object sender, EventArgs e)
        {
            ModifyRoleRes();
        }

        private void nTreeListView1_DragDrop(object sender, DragEventArgs e)
        {
            //List<RoleResourceMapping> newRoleResList = new List<RoleResourceMapping>();
            //newRoleResList.Add(((sender as TreeListViewItem).Tag as RoleResourceMapping));
            //newRoleResList.Add(nTreeListView1.SelectedItems[0].Tag as RoleResourceMapping);

            //PrivilegeService _proxy = Common.Util.CreateProxy();
            //using (_proxy as IDisposable)
            //{
            //    _proxy.MoveSequence(newRoleResList);
            //}

            //LoadRoleResource();
        }





    }
}
