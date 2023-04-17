using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.HISFC.BizLogic.Privilege.Service;
using FS.HISFC.BizLogic.Privilege;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.Components.Privilege.Common;
using System.Collections;


namespace FS.HISFC.Components.Privilege
{
    public partial class AuthorizeResourceForm : FS.HISFC.Components.Privilege.PermissionBaseForm
    {
        private IList<Role> _roles = new List<Role>();
        List<Role> childList = new List<Role>();
        AuthorizeResourceControl authorizeResourceControl = null;
        AuthorizeUserControl ucUser = null;

        public AuthorizeResourceForm()
        {
            InitializeComponent();
            FS.FrameWork.WinForms.Classes.Function.SetTabControlStyle(this.nTabControl1);
            this.BackColor = FS.FrameWork.WinForms.Classes.Function.GetSysColor(FS.FrameWork.WinForms.Classes.EnumSysColor.Blue);
            InitToolBar();
            LoadRole();
            InitControl();

            this.MainToolStrip.ItemClicked += new ToolStripItemClickedEventHandler(MainToolStrip_ItemClicked);

        }

        private void InitToolBar()
        {
            MainToolStrip.Items.Clear();
            ToolBarService _toolBarService = new ToolBarService();
            _toolBarService.Clear();
            _toolBarService.AddToolButton("���ӽ�ɫ", "", FS.FrameWork.WinForms.Classes.EnumImageList.J��ɫ���, true, false, null);
            _toolBarService.AddToolButton("ɾ����ɫ", "", FS.FrameWork.WinForms.Classes.EnumImageList.J��ɫɾ��, true, false, null);
            _toolBarService.AddToolSeparator();
            if (nTabControl1.SelectedTab.Name == "UserRes")
            {
                _toolBarService.AddToolButton("�����û�", "", FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա, true, false, null);
                _toolBarService.AddToolButton("�޸��û�", "", FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա�޸�, true, false, null);
                _toolBarService.AddToolButton("�����û�", "", FS.FrameWork.WinForms.Classes.EnumImageList.C������Ա, true, false, null);

            }
            if (nTabControl1.SelectedTab.Name == "MenuRes" || nTabControl1.SelectedTab.Name == "WebRes")
            {
                _toolBarService.AddToolButton("���Ӳ˵�", "", FS.FrameWork.WinForms.Classes.EnumImageList.C�˵����, true, false, null);
                _toolBarService.AddToolButton("ɾ���˵�", "", FS.FrameWork.WinForms.Classes.EnumImageList.C�˵�ɾ��, true, false, null);

            }
            if (nTabControl1.SelectedTab.Name == "DictionaryRes")
            {
                _toolBarService.AddToolButton("���ӳ���", "",FS.FrameWork.WinForms.Classes.EnumImageList.Mģ�����, true, false, null);
                _toolBarService.AddToolButton("ɾ������", "", FS.FrameWork.WinForms.Classes.EnumImageList.Mģ��ɾ��, true, false, null);

            }
            if (nTabControl1.SelectedTab.Name == "ReportRes")
            {
                _toolBarService.AddToolButton("���ӱ���", "", FS.FrameWork.WinForms.Classes.EnumImageList.M��ϸ���, true, false, null);
                _toolBarService.AddToolButton("ɾ������", "", FS.FrameWork.WinForms.Classes.EnumImageList.M��ϸɾ��, true, false, null);

            }
            if (nTabControl1.SelectedTab.Name == "OrgRes")
            {
                _toolBarService.AddToolButton("����Ȩ��", "", FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            }

            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("�˳�", "", FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�, true, false, null);

            ArrayList toolButtons=_toolBarService.GetToolButtons();
            for (int i = 0; i < toolButtons.Count; i++)
            {
                this.MainToolStrip.Items.Add(toolButtons[i] as ToolStripItem);
            }
            for (int i = 0; i < MainToolStrip.Items.Count; i++)
            {
                //{1B10BCB7-8133-4282-8479-9C41FE5A23FD}  ��Դ��Ȩ������������  ��ť������ת��
                if (string.IsNullOrEmpty( this.MainToolStrip.Items[i].Text ) == false)
                {
                    this.MainToolStrip.Items[i].Image.Tag = this.MainToolStrip.Items[i].Text;
                    this.MainToolStrip.Items[i].Text = FS.FrameWork.Management.Language.Msg( this.MainToolStrip.Items[i].Text );

                    this.MainToolStrip.Items[i].TextImageRelation = TextImageRelation.ImageAboveText;
                }
            }
        }

        private void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //{1B10BCB7-8133-4282-8479-9C41FE5A23FD}  ��Դ��Ȩ������������  ��ť������ת��
            string itemText = e.ClickedItem.Image.Tag.ToString();

            switch (itemText)
            {
                case "���ӽ�ɫ":
                    AddRole();
                    break;
                case "ɾ����ɫ":
                    DelRole();
                    break;
                case "�����û�":
                    ucUser.AddUser();
                    break;
                case "�޸��û�":
                    ucUser.ModifyUser();
                    break;
                case "���Ӳ˵�":
                    authorizeResourceControl.AddRoleRes();
                    break;
                case "ɾ���˵�":
                    authorizeResourceControl.DelettRoleRes();
                    break;
                case "���ӳ���":
                    authorizeResourceControl.AddRoleRes();
                    break;
                case "ɾ������":
                    authorizeResourceControl.DelettRoleRes();
                    break;
                case "���ӱ���":
                    authorizeResourceControl.AddRoleRes();
                    break;
                case "ɾ������":
                    authorizeResourceControl.DelettRoleRes();
                    break;
                case "����Ȩ��":
                    //ucOrganization.SaveRoleOrg();
                    break;
                case "�˳�":
                    this.Close();
                    break;
            }
        }

        private void LoadRole()
        {
            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();

                using (_proxy as IDisposable)
                {
                    _roles = _proxy.QueryRole();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            TreeNode _root = NewNode((Role)((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).CurrentGroup, 6);
            this.tvRole.Nodes.Add(_root);

            this.AddRoleNode(_root, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).CurrentGroup.ID);
            _root.Expand();
        }

        private TreeNode NewNode(Role role, int index)
        {
            TreeNode _node = new TreeNode(role.Name);
            _node.Tag = role;
            _node.ImageIndex = index;
            _node.SelectedImageIndex = index;

            return _node;
        }

        private void AddRoleNode(TreeNode parent, string parentId)
        {
            if (_roles != null)
            {
                foreach (Role _item in _roles)
                {
                    if (_item.ParentId == parentId)
                    {
                        TreeNode _node = NewNode(_item, 5);
                        parent.Nodes.Add(_node);

                        AddRoleNode(_node, _item.ID);
                    }
                }
            }
        }

        private void AddRole()
        {
            TreeNode _current = tvRole.SelectedNode;
            if (_current == null) return;

            AddRoleForm _frmAddRole = new AddRoleForm((Role)_current.Tag);
            _frmAddRole.ShowDialog();
            Role _role = _frmAddRole.Current;

            if (_role != null)
            {
                TreeNode _node = NewNode(_role, 5);
                _current.Nodes.Add(_node);
            }

            _frmAddRole.Dispose();
        }

        private void DelRole()
        {
            TreeNode _node = this.tvRole.SelectedNode;
            if (_node == null) return;

            if ((_node.Tag as Role).ID == "roleadmin")
            {
                MessageBox.Show("�ý�ɫΪϵͳĬ�Ͻ�ɫ,����ɾ��!", "��ʾ");
                return;
            }

            if (MessageBox.Show("�Ƿ�Ҫɾ���ý�ɫ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            if (_node.Nodes.Count > 0)
            {
                MessageBox.Show("�ý�ɫ���ӽ�ɫ������ɾ��");
                return;
            }

            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    try
                    {
                        FrameWork.Management.PublicTrans.BeginTransaction();
                        _proxy.RemoveRole(_node.Tag as Role);
                        FrameWork.Management.PublicTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        throw e;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            _node.Parent.Nodes.Remove(_node);
        }

        private void ModifyRole()
        {
            TreeNode _node = this.tvRole.SelectedNode;
            if (_node == null) return;

            if ((_node.Tag as Role).ID == "roleadmin")
            {
                MessageBox.Show("�ý�ɫΪϵͳĬ�Ͻ�ɫ,�����޸�!", "��ʾ");
                return;
            }

            AddRoleForm _frmAddRole = new AddRoleForm((Role)_node.Parent.Tag, (Role)_node.Tag);
            _frmAddRole.ShowDialog();
            Role _role = _frmAddRole.Current;

            if (_role != null)
            {
                TreeNode _parent = _node.Parent;
                int _index = _parent.Nodes.IndexOf(_node);
                TreeNodeCollection _childs = _node.Nodes;
                _parent.Nodes.Remove(_node);

                TreeNode _current = NewNode(_role, 5);
                foreach (TreeNode _child in _childs)
                {
                    _current.Nodes.Add(_child);
                }

                _parent.Nodes.Insert(_index, _current);
            }

            _frmAddRole.Dispose();
        }

        private void InitControl()
        {
            nTabControl1.SelectedTab.Controls.Clear();
            if (nTabControl1.SelectedTab.Name == "OrgRes")
            {
                //ucOrganization = new AuthorizeOrganizationControl();
                //ucOrganization.currentRole = tvRole.SelectedNode.Tag as Role;
                //ucOrganization.Dock = DockStyle.Fill;
                //nTabControl1.SelectedTab.Controls.Add(ucOrganization);
            }
            else if (nTabControl1.SelectedTab.Name == "UserRes")
            {
                ucUser = new AuthorizeUserControl();
                if (tvRole.SelectedNode != null)
                {
                    ucUser.currentRole = tvRole.SelectedNode.Tag as Role;
                    ucUser.Dock = DockStyle.Fill;
                    nTabControl1.SelectedTab.Controls.Add(ucUser);
                }
            }
            else
            {
                authorizeResourceControl = new AuthorizeResourceControl();
                authorizeResourceControl.pageJudge = nTabControl1.SelectedTab.Name.Trim();
                if (tvRole.SelectedNode != null)
                {
                    authorizeResourceControl.currentRole = tvRole.SelectedNode.Tag as Role;
                    if (tvRole.SelectedNode.Parent != null)
                    {
                        authorizeResourceControl.parentRole = tvRole.SelectedNode.Parent.Tag as Role;
                    }
                    childList.Clear();
                    SetChildRoleList(tvRole.SelectedNode.Nodes);
                    authorizeResourceControl.ChildList = childList;
                }
                authorizeResourceControl.Dock = DockStyle.Fill;
                nTabControl1.SelectedTab.Controls.Add(authorizeResourceControl);
            }
        }

        private void AddRoleMenu_Click(object sender, EventArgs e)
        {
            AddRole();
        }

        private void ModifyRoleMenu_Click(object sender, EventArgs e)
        {
            ModifyRole();
        }

        private void DelRoleMenu_Click(object sender, EventArgs e)
        {
            DelRole();
        }

        private void nTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitToolBar();

            InitControl();
        }

        private void tvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            InitControl();
        }

        private void tvRole_DoubleClick(object sender, EventArgs e)
        {
            ModifyRole();
        }

        private void SetChildRoleList(TreeNodeCollection ChildNodes)
        {
            if (ChildNodes == null) return;
            foreach (TreeNode newNode in ChildNodes)
            {
                childList.Add(newNode.Tag as Role);
                SetChildRoleList(newNode.Nodes);
            }
        }


    }
}