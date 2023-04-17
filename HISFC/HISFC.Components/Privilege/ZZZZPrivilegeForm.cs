using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using Neusoft.Privilege.ServiceContracts.Contract;
using System.ServiceModel;
using Neusoft.Privilege.ServiceContracts.Model;
using Neusoft.Privilege.ServiceContracts.Model.Impl;
using Neusoft.Privilege.WinForms.Forms;

namespace Neusoft.Privilege.WinForms
{
    public partial class ZZZZPrivilegeForm : PermissionBaseForm
    {
        public ZZZZPrivilegeForm()
        {
            InitializeComponent();

            this.InitToolBar();
            
            this.Load += new EventHandler(frmPriv_Load);
            this.MainToolStrip.ItemClicked += new ToolStripItemClickedEventHandler(MainToolStrip_ItemClicked);
            this.tvRole.AfterSelect += new TreeViewEventHandler(tvRole_AfterSelect);
            this.tvRole.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvRole_NodeMouseClick);
            this.tvRole.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(tvRole_NodeMouseDoubleClick);

            this.lvUser.DoubleClick += new EventHandler(lvUser_DoubleClick);

            this.tvMenu.AfterCheck += new TreeViewEventHandler(tvMenu_AfterCheck);
            this.tvMenu.BeforeCheck += new TreeViewCancelEventHandler(tvMenu_BeforeCheck);
            //�����Ĳ˵��¼�
            AddRoleMenu.Click += new EventHandler(AddRoleMenu_Click);
            DelRoleMenu.Click += new EventHandler(DelRoleMenu_Click);
            ModifyRoleMenu.Click += new EventHandler(ModifyRoleMenu_Click);
            AddUserItem.Click += new EventHandler(AddUserItem_Click);
            ModifyUserItem.Click += new EventHandler(ModifyUserItem_Click);
            RemoveUserItem.Click += new EventHandler(RemoveUserItem_Click);

            btnSave.Click += new EventHandler(btnSave_Click);
            btnReset.Click += new EventHandler(btnReset_Click);
            btnSelectNone.Click += new EventHandler(btnSelectNone_Click);
            btnSelectAll.Click += new EventHandler(btnSelectAll_Click);
        }                          

        #region �����Ĳ˵������¼�
        void RemoveUserItem_Click(object sender, EventArgs e)
        {
            RemoveUser();
        }

        void ModifyUserItem_Click(object sender, EventArgs e)
        {
            ModifyUser();
        }

        void AddUserItem_Click(object sender, EventArgs e)
        {
            AddUser();
        }        
        
        void ModifyRoleMenu_Click(object sender, EventArgs e)
        {
            ModifyRole();
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DelRoleMenu_Click(object sender, EventArgs e)
        {
            DelRole();
        }

        /// <summary>
        /// ���ӽ�ɫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddRoleMenu_Click(object sender, EventArgs e)
        {
            AddRole();
        }
        #endregion


        void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "���ӽ�ɫ":
                    AddRole();
                    break;
                case "ɾ����ɫ":
                    DelRole();
                    break;
                case "�����û�":
                    AddUser();
                    break;
                case "ɾ���û�":
                    RemoveUser();
                    break;
                case "�˳�":
                    this.Close();
                    break;
            }
        }

        private IList<IRole> _roles = new List<IRole>();
        private IList<Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem> _menus;
        private List<string> _selectedMenus = new List<string>();


        private void InitToolBar()
        {
            ToolBarService _toolBarService = new ToolBarService();
            _toolBarService.AddToolButton("���ӽ�ɫ", "", (Image)Resource.����16, true, false, null);
            _toolBarService.AddToolButton("ɾ����ɫ", "", (Image)Resource.ɾ��16, true, false, null);
            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("�����û�", "", (Image)Resource.�޸�16, true, false, null);
            _toolBarService.AddToolButton("ɾ���û�", "", (Image)Resource.�ٻ�16, true, false, null);
            _toolBarService.AddToolButton("�����û�", "", (Image)Resource.��ѯ16, true, false, null);
            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("�˳�", "", (Image)Resource.�˳�16, true, false, null);
            
            this.MainToolStrip.Items.AddRange(_toolBarService.GetToolStripButtons());
            this.MainToolStrip.Items[0].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[1].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[3].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[4].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[5].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[7].TextImageRelation = TextImageRelation.ImageAboveText;
        }

        void frmPriv_Load(object sender, EventArgs e)
        {            
            this.LoadRole();

            this.InitMenu();
            this.AddRootMenuNode();

            this.LoadUser();             
        }

        private void LoadUser()
        {
            IPrivilegeService _proxy = Common.Util.CreateProxy();
            
            try
            {
                IList<IUser> _users;
                using (_proxy as IDisposable)
                {
                    _users = _proxy.QueryUser();
                }
                InitUserList(_users);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
        }

        private void InitUserList(IList<IUser> users)
        {
            lvUser.Items.Clear();

            foreach (IUser _user in users)
            {
                ListViewItem _item = NewItem(_user);
                this.lvUser.Items.Add(_item);
            }
        }

        private ListViewItem NewItem(IUser user)
        {
            ListViewItem _item = new ListViewItem(new string[] { user.PersonId,user.Name,user.Account,
                                         (user.IsLock?"��":"��"),user.Description});
            _item.Tag = user;
            _item.ImageIndex = 1;
            _item.Group = lvUser.Groups[1];

            return _item;
        }

        private void InitMenu()
        {
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();

                using (_proxy as IDisposable)
                {
                    if ((Facade.Context.Operator as NeuPrincipal).CurrentRole.Id == "roleadmin")
                    {
                        _menus = _proxy.QueryMenu();
                    }
                    else
                    {
                        _menus = _proxy.QueryMenu((Facade.Context.Operator as NeuPrincipal).CurrentRole.Id);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
        }

        private void AddRootMenuNode()
        {
            tvMenu.Nodes.Clear();

            //���ɸ�����
            foreach (Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _menu in _menus)
            {
                if (_menu.ParentId == "ROOT")//��һ��Ϊ����
                {
                    TreeNode _node = new TreeNode(_menu.Name);
                    _node.Tag = _menu;
                    _node.ImageIndex = 0;
                    _node.SelectedImageIndex = 0;
                    
                    this.tvMenu.Nodes.Add(_node);

                    AddSubMenuNode(_menu.Id, _node);                    
                }
            }

            tvMenu.ExpandAll();
        }

        private void AddSubMenuNode(string parentID, TreeNode parent)
        {
            foreach (Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _menu in _menus)
            {
                if (_menu.ParentId == parentID)
                {
                    TreeNode _child = new TreeNode(_menu.Name);
                    _child.Tag = _menu;
                    _child.ImageIndex = 1;
                    _child.SelectedImageIndex = 1;
                    parent.Nodes.Add(_child);

                    AddSubMenuNode(_menu.Id, _child);
                }
            }
        }

        private void LoadRole()
        {            
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();

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

            NeuPrincipal _principal = (NeuPrincipal)Facade.Context.Operator;
            TreeNode _root = NewNode(_principal.CurrentRole,6);                
            this.tvRole.Nodes.Add(_root);

            this.AddRoleNode(_root, _principal.CurrentRole.Id);
            _root.Expand();
        }

        private void AddRoleNode(TreeNode parent, string parentId)
        {
            if (_roles != null)
            {
                foreach (IRole _item in _roles)
                {
                    if (_item.ParentId == parentId)
                    {
                        TreeNode _node = NewNode(_item,5);
                        parent.Nodes.Add(_node);

                        AddRoleNode(_node, _item.Id);
                    }
                }
            }
        }

        private TreeNode NewNode(IRole role,int index)
        {
            TreeNode _node = new TreeNode(role.Name);
            _node.Tag = role;
            _node.ImageIndex = index;
            _node.SelectedImageIndex = index;

            return _node;
        }

        void tvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode _current = tvRole.SelectedNode;

            IRole _role = (IRole)_current.Tag;

            QueryUser(_role);
            QueryMenu(_role.Id);
        }

        void tvRole_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ModifyRole();
        }

        void tvRole_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvRole.SelectedNode = e.Node;
                tvRole.ContextMenuStrip = nRoleMenuStrip;
            }
        }

        private void QueryUser(IRole role)
        {
            IList<IUser> _objs = null;
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    _objs = _proxy.QueryUser(role.Id);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            lvUser.Groups[0].Tag = role;
            lvUser.Groups[0].Header = role.Name + "�Ѱ����û�";

            //�ָ���ʼ״̬
            foreach (ListViewItem _item in lvUser.Items)
            {
                if (_item.Group == lvUser.Groups[1]) continue;

                _item.ImageIndex = 1;
                _item.Group = lvUser.Groups[1];
            }

            foreach (IUser _user in _objs)
            {
                foreach (ListViewItem _item in lvUser.Items)
                {
                    if (_user.Id == (_item.Tag as IUser).Id)
                    {
                        _item.ImageIndex = 0;
                        _item.Group = lvUser.Groups[0];
                        break;
                    }
                }
            }
        }

        private void QueryMenu(string roleId)
        {
            IPrivilegeService _proxy = Common.Util.CreateProxy();
            IList<Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem> _objs;
            using (_proxy as IDisposable)
            {
                _objs = _proxy.QueryMenu(roleId);
            }

            tvMenu.AfterCheck -= this.tvMenu_AfterCheck;
            tvMenu.BeforeCheck -= this.tvMenu_BeforeCheck;
            //�ָ���ʼ״̬
            GetAuthorizableNodes();

            foreach (Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _obj in _objs)
            {
                TreeNode _findNode = SeekNode(tvMenu.Nodes, _obj.Id);
                if (_findNode != null)
                {
                    _findNode.Checked = true;                    
                    //CheckParentNode(_findNode);
                }
            }

            tvMenu.AfterCheck += this.tvMenu_AfterCheck;
            tvMenu.BeforeCheck += this.tvMenu_BeforeCheck;
        }

        /// <summary>
        /// ��ȡ����Ȩ�Ĳ˵���
        /// </summary>
        private void GetAuthorizableNodes()
        {
            TreeNode _node = tvRole.SelectedNode;
            if (_node == null) return;

            if (_node.Level == 0)//��һ���������޸�,��Ϊ�㲻������Ȩ��,Ҳ���ܼ����Լ���Ȩ��,����û�п���Ȩ��Ȩ��
            {
                UncheckNodes(tvMenu.Nodes, false);
            }
            else if (_node.Level == 1)//��һ�����������Ȩ,��Ϊ����ȫ����
            {
                UncheckNodes(tvMenu.Nodes, true);
            }
            else
            {
                UncheckNodes(tvMenu.Nodes, false);

                try
                {
                    IPrivilegeService _proxy = Common.Util.CreateProxy();
                    IList<Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem> _parentMenus;
                    using (_proxy as IDisposable)
                    {
                        _parentMenus = _proxy.QueryMenu((_node.Parent.Tag as IRole).Id);
                    }

                    foreach (Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _menu in _parentMenus)
                    {
                        TreeNode _findNode = SeekNode(tvMenu.Nodes, _menu.Id);
                        if (_findNode != null)
                        {
                            _findNode.SelectedImageIndex = 3;
                            _findNode.ImageIndex = 3;
                        }
                    }
                }
                catch (Exception ex)
                {                    
                    MessageBox.Show(ex.Message, "��ʾ");
                }
            }
        }

        private void UncheckNodes(TreeNodeCollection nodes,bool isAuthorizable)
        {
            foreach (TreeNode _node in nodes)
            {
                _node.Checked = false;

                if (isAuthorizable)
                {
                    _node.ImageIndex = 3;
                    _node.SelectedImageIndex = 3;
                }
                else
                {
                    _node.ImageIndex = 4;
                    _node.SelectedImageIndex = 4;
                }

                UncheckNodes(_node.Nodes, isAuthorizable);
            }
        }

        private TreeNode SeekNode(TreeNodeCollection childs,string id)
        {
            foreach (TreeNode _node in childs)
            {
                if ((_node.Tag as Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem).Id == id)
                {
                    return _node;
                }

                if (_node.Nodes.Count > 0)
                {
                    TreeNode _child = SeekNode(_node.Nodes, id);
                    if (_child != null) return _child;
                }
            }

            return null;
        }

        private void CheckParentNode(TreeNode node, bool isSelected)
        {
            while (node.Parent != null)
            {
                node.Parent.Checked = isSelected;
                node = node.Parent;
            }
        }

        private void CheckChildNode(TreeNode node, bool isSelected)
        {
            foreach (TreeNode _child in node.Nodes)
            {
                if (_child.ImageIndex == 3)//������Ȩ,�Ž��м��,����After��BeforeCheck�ǵĻ�
                {
                    _child.Checked = isSelected;

                    CheckChildNode(_child, isSelected);
                }
            }
        }

        private void AddRole()
        {
            TreeNode _current = tvRole.SelectedNode;
            if (_current == null) return;

            AddRoleForm _frmAddRole = new AddRoleForm((IRole)_current.Tag);
            _frmAddRole.ShowDialog();
            IRole _role = _frmAddRole.Current;

            if (_role != null)
            {                
                TreeNode _node = NewNode(_role,5);
                _current.Nodes.Add(_node);
            }

            _frmAddRole.Dispose();
        }

        private void DelRole()
        {
            TreeNode _node = this.tvRole.SelectedNode;
            if (_node == null) return;

            if ((_node.Tag as IRole).Id == "roleadmin")
            {
                MessageBox.Show("�ý�ɫΪϵͳĬ�Ͻ�ɫ,����ɾ��!", "��ʾ");
                return;
            }

            if (MessageBox.Show("�Ƿ�Ҫɾ���ý�ɫ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            if (_node.Nodes.Count > 0)
            {
                if (MessageBox.Show("ɾ����ɫ,��ɾ����������ɫ,�Ƿ����?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            }
            
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    _proxy.RemoveRole((_node.Tag as IRole).Id);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"��ʾ");
                return;
            }
                        
            _node.Parent.Nodes.Remove(_node);
        }

        private void ModifyRole()
        {
            TreeNode _node = this.tvRole.SelectedNode;
            if (_node == null) return;

            if ((_node.Tag as IRole).Id == "roleadmin")
            {
                MessageBox.Show("�ý�ɫΪϵͳĬ�Ͻ�ɫ,�����޸�!", "��ʾ");
                return;
            }

            AddRoleForm _frmAddRole = new AddRoleForm((IRole)_node.Parent.Tag, (IRole)_node.Tag);
            _frmAddRole.ShowDialog();
            IRole _role = _frmAddRole.Current;

            if (_role != null)
            {
                TreeNode _parent = _node.Parent;
                int _index = _parent.Nodes.IndexOf(_node);
                TreeNodeCollection _childs = _node.Nodes;
                _parent.Nodes.Remove(_node);
                
                TreeNode _current = NewNode(_role,5);
                foreach (TreeNode _child in _childs)
                {
                    _current.Nodes.Add(_child);
                }

                _parent.Nodes.Insert(_index, _current);
            }

            _frmAddRole.Dispose();
        }

        void lvUser_DoubleClick(object sender, EventArgs e)
        {
            ModifyUser();
        }

        private void AddUser()
        {
            TreeNode _node = tvRole.SelectedNode;
            if (_node == null) return;

            ZZZZAddUserForm _frmAddUser = new ZZZZAddUserForm(_node.Tag as IRole);
            _frmAddUser.ShowDialog();
            List<IUser> _newModify = _frmAddUser.NewModifyUser;

            foreach (IUser _user in _newModify)
            {
                foreach (ListViewItem _item in lvUser.Items)
                {
                    if ((_item.Tag as IUser).Id == _user.Id)
                    {
                        lvUser.Items.Remove(_item);
                    }
                }

                ListViewItem _currentItem = NewItem(_user);
                lvUser.Items.Add(_currentItem);
            }
            
            tvRole_AfterSelect(null, null);
        }

        private void ModifyUser()
        {
            TreeNode _node = tvRole.SelectedNode;
            if (_node == null) return;

            ListView.SelectedListViewItemCollection _selected = lvUser.SelectedItems;
            if (_selected.Count == 0) return;

            ZZZZAddUserForm _frmAddUser = new ZZZZAddUserForm(_selected[0].Tag as IUser, _node.Tag as IRole);
            _frmAddUser.ShowDialog();
            List<IUser> _newModify = _frmAddUser.NewModifyUser;

            foreach (IUser _user in _newModify)
            {
                foreach (ListViewItem _item in lvUser.Items)
                {
                    if ((_item.Tag as IUser).Id == _user.Id)
                    {
                        lvUser.Items.Remove(_item);
                    }
                }

                ListViewItem _currentItem = NewItem(_user);
                lvUser.Items.Add(_currentItem);
            }

            tvRole_AfterSelect(null, null);
        }

        private void RemoveUser()
        {
            ListView.SelectedListViewItemCollection _selected = lvUser.SelectedItems;
            if (_selected.Count == 0) return;

            if ((_selected[0].Tag as IUser).Id == "admin")
            {
                MessageBox.Show("���û�ΪϵͳĬ���û�,����ɾ��!", "��ʾ");
                return;
            }

            if (MessageBox.Show("�Ƿ�Ҫɾ�����û�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                        
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    _proxy.RemoveUser((_selected[0].Tag as IUser).Id);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            lvUser.Items.Remove(_selected[0]);
        }

        #region �˵���Ȩ
        void btnSelectAll_Click(object sender, EventArgs e)
        {
            Check(tvMenu.Nodes, true);
        }

        private void Check(TreeNodeCollection childs, bool isChecked)
        {
            foreach (TreeNode _node in childs)
            {
                _node.Checked = isChecked;
                Check(_node.Nodes, isChecked);
            }
        }

        void btnSelectNone_Click(object sender, EventArgs e)
        {
            Check(tvMenu.Nodes, false);
        }

        void btnReset_Click(object sender, EventArgs e)
        {
            TreeNode _current = tvRole.SelectedNode;

            IRole _role = (IRole)_current.Tag;

            QueryMenu(_role.Id);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            TreeNode _current = tvRole.SelectedNode;
            if (_current == null) return;

            List<string> roles = new List<string>();
            roles.Add((_current.Tag as IRole).Id);

            List<string> menus = new List<string>();
            menus = GetSelectedMenuID(tvMenu.Nodes);

            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    _proxy.AddRoleMenuMap(roles, menus);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ʾ");
                return;
            }

            MessageBox.Show("����ɹ�!", "��ʾ");
        }

        private List<string> GetSelectedMenuID(TreeNodeCollection nodes)
        {
            List<string> _menus = new List<string>();

            foreach (TreeNode _node in nodes)
            {
                if (_node.Checked)
                {
                    _menus.Add((_node.Tag as Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem).Id);

                    //����ӽڵ�ѡ�н��
                    _menus.AddRange(GetSelectedMenuID(_node.Nodes));
                }
            }

            return _menus;
        }

        void tvMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //
            tvMenu.AfterCheck -= this.tvMenu_AfterCheck;
            tvMenu.BeforeCheck -= this.tvMenu_BeforeCheck;

            //�¼��ڵ㴦��,���ϼ���ͬѡ��
            CheckChildNode(e.Node, e.Node.Checked);

            //�ϼ��ڵ㴦��
            if (e.Node.Checked)
            {
                CheckParentNode(e.Node, e.Node.Checked);
            }
            else
            {
                //���ͬ���ڵ㶼ûѡ��,����ȡ��ѡ��
                TreeNode _parent = e.Node.Parent;
                if (_parent != null)
                {
                    bool _unSelected = true;
                    foreach (TreeNode _node in _parent.Nodes)
                    {
                        if (_node.Checked)
                        {
                            _unSelected = false;
                            break;
                        }
                    }

                    if (_unSelected)//���ͬ���ڵ㶼ûѡ��,����ȡ��ѡ��
                    {
                        CheckParentNode(e.Node, e.Node.Checked);
                    }
                }
            }

            tvMenu.AfterCheck += this.tvMenu_AfterCheck;
            tvMenu.BeforeCheck += this.tvMenu_BeforeCheck;

            //���õ���,���ÿ���            
            /*
            //�����ڵ㴦��
            if (e.Node.Checked)
            {
                //ѡ�е�ǰ�ڵ㣬�����и��ڵ�ѡ��
                //CheckParentNode(e.Node);
                if (e.Node.Parent != null)
                    e.Node.Parent.Checked = true;                
            }
            else
            {
                TreeNode _parent = e.Node.Parent;
                if (_parent != null)
                {
                    bool _unSelected = true;
                    foreach (TreeNode _node in _parent.Nodes)
                    {
                        if (_node.Checked)
                        {
                            _unSelected = false;
                            break;
                        }
                    }

                    if (_unSelected)//���ͬ���ڵ㶼ûѡ��,����ȡ��ѡ��
                    {
                        _parent.Checked = false;
                    }
                }
            }             
            */
        }

        void tvMenu_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode _selected = tvRole.SelectedNode;
            if (_selected == null)
            {
                e.Cancel = true;
                return;
            }
            if (_selected.Level == 0)//��һ���������޸�,��Ϊ�㲻������Ȩ��,Ҳ���ܼ����Լ���Ȩ��
            {
                e.Cancel = true;
            }
            else if (_selected.Level == 1)//��һ�����������Ȩ,��Ϊ����ȫ����
            {
            }
            else//�����,������֤ѡ��Ľڵ��Ƿ�����һ�����Ӽ�,���ǲ�������Ȩ
            {
                try
                {
                    IList<Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem> _parentMenus;
                    IPrivilegeService _proxy = Common.Util.CreateProxy();

                    using (_proxy as IDisposable)
                    {
                        _parentMenus = _proxy.QueryMenu((_selected.Parent.Tag as IRole).Id);
                    }

                    bool _isContained = false;

                    foreach (Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _menu in _parentMenus)
                    {
                        if (_menu.Id == (e.Node.Tag as Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem).Id)
                        {
                            _isContained = true;
                            break;
                        }
                    }

                    if (!_isContained) e.Cancel = true;
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.Show(ex.Message, "��ʾ");
                }
            }
        }
          
        #endregion
    }
}

