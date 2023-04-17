using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neusoft.WinForms.Forms;
using Neusoft.Privilege.ServiceContracts.Model.Impl;
using Neusoft.Framework;
using Neusoft.Privilege.ServiceContracts.Model;
using Neusoft.Privilege.ServiceContracts.Contract;

namespace Neusoft.Privilege.WinForms
{
    public partial class AddUserForm : InputBaseForm
    {
        private IList<IRole> roles = new List<IRole>();
        private IList<IOrganization> organizations = new List<IOrganization>();
        private IRole currentRole = null;
        private IList<IPerson> persons = new List<IPerson>();
        private List<IUser> newModifyUser = new List<IUser>();
        private IList<IRole> allRolesOfUser = new List<IRole>();
        ////��ǰ��ɫ��֯�ṹ��ϵ
        //private Dictionary<IRole, List<IOrganization>> roleOrgMapping = new Dictionary<IRole, List<IOrganization>>();
        //��ǰ��ɫ��֯�ṹ��ϵ
        private Dictionary<String, List<String>> roleOrgDictionary = new Dictionary<String, List<String>>();
        //��֯�ṹ��
        private Dictionary<String, TreeNode> orgTreeDictionary = new Dictionary<String, TreeNode>();
        //��ɫ��
        private Dictionary<String, TreeNode> roleTreeDictionary = new Dictionary<String, TreeNode>();
        //�޸�ʱ�����������û���Ϣ
        private IUser origin = null;
        //��ǰ��ȡ�ؼ����û���Ϣ
        private IUser currentUser = null;

        public AddUserForm(IRole _currentRole)
        {
            currentRole = _currentRole;
            InitializeComponent();
        }

        public AddUserForm(IUser user, IRole _currentRole)
        {
            currentRole = _currentRole;
            origin = user;
            InitializeComponent();
        }

        public void AddUser()
        {
            AddUserForm_Load(null, null);
        }


        #region ˽�з���
        /// <summary>
        ///  ������֯�ṹ
        /// </summary>
        /// 
        private void LoadOrg()
        {
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    organizations = _proxy.QueryUnit("HIS");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
        }

        private void LoadRole()
        {
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    roles = _proxy.QueryChildRole((Facade.Context.Operator as NeuPrincipal).CurrentRole.Id);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            TreeNode _root = NewNode((Facade.Context.Operator as NeuPrincipal).CurrentRole);
            roles.Add((Facade.Context.Operator as NeuPrincipal).CurrentRole);
            roleTreeDictionary.Add((Facade.Context.Operator as NeuPrincipal).CurrentRole.Id,_root);
            tvRole.Nodes.Add(_root);
            AddSubRoleNode((_root.Tag as IRole), _root);
            _root.Expand();
        }

        private int LoadPerson()
        {
            IPrivilegeService proxy = Common.Util.CreateProxy();

            try
            {
                IList<string> keys;
                using (proxy as IDisposable)
                {
                    keys = proxy.QueryAppID();
                    foreach (string _key in keys)
                    {
                        IList<IPerson> collection = proxy.QueryPerson(_key);
                        foreach (IPerson person in collection)
                        {
                            persons.Add(person);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            return 0;
        }

        private void LoadTreeView()
        {
            tvOrganization.Nodes.Clear();

            //���õ�ǰ���ڵ�Tag
            IOrganization rootOrg = organizations[0];

            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootOrg.Department.Name.ToString();
            rootNode.Tag = rootOrg;
            rootNode.Expand();
            tvOrganization.Nodes.Add(rootNode);

            //ɾ�����ڵ㣬׼�������ӽڵ�
            organizations.RemoveAt(0);

            SetChildNode(rootNode, organizations);

        }

        private void SetChildNode(TreeNode parentNode, IList<IOrganization> organizations)
        {
            List<TreeNode> childs = null;
            if (parentNode != null)
            {
                childs = new List<TreeNode>();
                IOrganization parentOrg = parentNode.Tag as IOrganization;
                List<IOrganization> childOrgList = new List<IOrganization>();

                foreach (IOrganization org in organizations)
                {
                    if (org.ParentId == parentOrg.Id)
                    {
                        childOrgList.Add(org);
                    }

                }

                for (int j = 1; j < childOrgList.Count; j++)
                {
                    for (int i = 0; i < childOrgList.Count - j; i++)
                    {
                        if (childOrgList[i].OrderNumber > childOrgList[i + 1].OrderNumber)
                        {
                            IOrganization orgChange = null;
                            orgChange = childOrgList[i];
                            childOrgList[i] = childOrgList[i + 1];
                            childOrgList[i + 1] = orgChange;
                        }
                    }
                }

                foreach (IOrganization newOrg in childOrgList)
                {

                    TreeNode newNode = new TreeNode();
                    newNode.Text = newOrg.Department.Name;
                    newNode.Tag = newOrg;
                    childs.Add(newNode);
                }

                foreach (TreeNode node in childs)
                {
                    parentNode.Nodes.Add(node);
                    orgTreeDictionary.Add((node.Tag as IOrganization).Id, node);
                    SetChildNode(node, organizations);
                }

            }

        }

        private void AddSubRoleNode(IRole parent, TreeNode parentNode)
        {
            foreach (IRole _role in roles)
            {
                if (_role.ParentId == parent.Id)
                {
                    TreeNode _node = NewNode(_role);
                    _node.Tag = _role;
                    roleTreeDictionary.Add(_role.Id, _node);
                    parentNode.Nodes.Add(_node);

                    AddSubRoleNode(_role, _node);
                }
            }
        }

        private TreeNode NewNode(IRole role)
        {
            TreeNode _node = new TreeNode(role.Name);
            _node.Tag = role;
            _node.Name = role.Id;
            return _node;
        }

        private string GetDefaultPassword()
        {
            string _value = System.Configuration.ConfigurationManager.AppSettings["DefaultPassword"];
            if (string.IsNullOrEmpty(_value))
            {
                return "his";
            }
            else
            {
                return _value;
            }
        }

        private bool JudgeSameLevel(TreeNode parentNode)
        {
            int count = 0;
            foreach (TreeNode roleNode in parentNode.Nodes)
            {
                if (roleNode.Checked == true)
                {
                    count++;
                }
            }

            if (count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //�жϵ�ǰ�û��Ƿ��ظ���
        private IUser IsExistUser(string appId, string personId)
        {
            IUser _user = null;
            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    _user = _proxy.GetUserByPersonId(appId, personId);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return null;
            }

            return _user;
        }

        private void ModifyUser(IUser user)
        {
            this.txtUserName.Text = user.Name;
            this.txtUserName.Tag = user.AppId + "||" + user.PersonId;
            this.txtAccount.Text = user.Account;
            this.chbOriginPass.Checked = false;
            this.chbLock.Checked = user.IsLock;
            this.txtMemo.Text = user.Description;

        }

        //�ж��������ֵ����Ƿ����Iorg
        private bool JudgeOrg(String checkedOrgId)
        {
            return roleOrgDictionary[(tvRole.SelectedNode.Tag as IRole).Id].Contains(checkedOrgId);
        }
        //�жϽ�ɫ�Ƿ����
        private bool JudgeRole(String checkedRoleId)
        {
            return roleOrgDictionary.ContainsKey(checkedRoleId);
        }

        //��ʼ����ɫ��checked����
        private void InitRoleTreeChecked()
        {
            foreach (String roleId in roleTreeDictionary.Keys)
            {
                if (JudgeRole(roleId))
                {
                    roleTreeDictionary[roleId].Checked = true;
                }
            }
        }

        //��õ�ǰ�û���Ϣ
        private IUser GetValue()
        {
            currentUser = new User();

            if (origin != null)
            {
                currentUser.Id = origin.Id;
                currentUser.Password = origin.Password;
            }

            currentUser.Name = txtUserName.Text.Trim();
            if (txtUserName.Tag != null)
            {
                string[] _array = txtUserName.Tag.ToString().Split(new char[] { '|', '|' }, StringSplitOptions.RemoveEmptyEntries);
                currentUser.AppId = _array[0];
                currentUser.PersonId = _array[1];
            }
            currentUser.Account = txtAccount.Text.Trim();

            if (chbOriginPass.Checked)
            {
                currentUser.Password = Common.Util.CreateHash(GetDefaultPassword());
            }

            currentUser.IsLock = chbLock.Checked;
            currentUser.Description = txtMemo.Text.Trim();
            //currentUser.UserId = ((Facade.Context.Operator as NeuPrincipal).Identity as NeuIdentity).User.Id;
            //currentUser.OperDate = Common.Util.GetDateTime();

            return currentUser;
        }

        private void GetRoleOrgMapping()
        {
            IPrivilegeService proxy = Common.Util.CreateProxy();
            using (proxy as IDisposable)
            {
                roleOrgDictionary = proxy.QueryAuthorityRoleOrg(origin);
            }
        }

        private int Save()
        {
            GetValue();
            if (Check())
            {
                IPrivilegeService proxy = Common.Util.CreateProxy();
                using (proxy as IDisposable)
                {
                    int ret = proxy.SaveAuthorityRoleOrg(currentUser, roleOrgDictionary);
                    return ret;
                }
            }
            return 0;
        }

        private bool Check()
        {
            if (string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                MessageBox.Show("��ѡ���û���Ա!", "��ʾ");
                this.btnSelectUser.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtAccount.Text.Trim()))
            {
                MessageBox.Show("��¼�ʻ�����Ϊ��!", "��ʾ");
                this.txtAccount.Focus();
                return false;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtAccount.Text.Trim(), 20))
            {
                MessageBox.Show("��¼�ʻ����Ȳ��ܳ���20���ַ�!", "��ʾ");
                this.txtAccount.Focus();
                return false;
            }
            //����û�û����Ȩ��ɫ����ʾ
            if (roleOrgDictionary.Count == 0)
            {
                MessageBox.Show("û�и��û���Ȩ��ɫ��", "��ʾ");
                return false;
            }

            //��֤�ʻ��Ƿ��Ѿ�����
            try
            {
                string userId = "";

                if (currentUser != null)
                    userId = currentUser.Id;
                else if (origin != null)
                    userId = origin.Id;

                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    IUser _user = _proxy.GetUserByAccount(txtAccount.Text.Trim());
                    if (_user != null && _user.Id != userId)
                    {
                        MessageBox.Show("�õ�¼�ʻ��Ѿ�����,��ʹ�������ʻ�!", "��ʾ");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return false;
            }

            /*if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtPassword.Text.Trim(), 20))
            {
                MessageBox.Show("��¼����Ȳ��ܳ���20���ַ�!", "��ʾ");
                this.txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() != txtConfirmPass.Text.Trim())
            {
                MessageBox.Show("ȷ�Ͽ���͵�¼�����,����������!", "��ʾ");
                txtConfirmPass.Focus();
                return false;
            }*/

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtMemo.Text.Trim(), 256))
            {
                MessageBox.Show("��ע���Ȳ��ܳ���128������!", "��ʾ");
                this.txtMemo.Focus();
                return false;
            }

            return true;
        }


        #endregion


        #region �¼�
        private void AddUserForm_Load(object sender, EventArgs e)
        {
            LoadRole();
            LoadPerson();
            LoadOrg();
            LoadTreeView();
            //�޸�ʱ��ʼ������
            if (origin != null)
            {
                ModifyUser(origin);
                GetRoleOrgMapping();
                InitRoleTreeChecked();
            }
        }

        private void btnSelectUser_Click(object sender, EventArgs e)
        {
            SelectItemForm<IPerson> frmSelect = new SelectItemForm<IPerson>();
            frmSelect.Id = "Id";
            frmSelect.Value = "Name";
            frmSelect.Description = "Remark";
            frmSelect.SecondKey = "AppId";
            frmSelect.InitItem(persons);
            frmSelect.ShowDialog();

            if (frmSelect.DialogResult == DialogResult.OK)
            {
                IPerson person = frmSelect.SelectedItem;
                this.txtUserName.Text = person.Name;

                ///�жϸ��û��Ƿ��Ѿ�����,������Ӽ��������û�
                IUser user = IsExistUser(person.AppId, person.Id);

                if (user != null)
                {
                    ModifyUser(user);
                    origin = user;

                    //����û�������ɫ
                    GetRoleOrgMapping();
                    //��ʼ����ɫ����Checked����
                    InitRoleTreeChecked();
                }
                else
                {
                    origin = null;

                    this.txtUserName.Text = person.Name;
                    this.txtUserName.Tag = person.AppId + "||" + person.Id;
                    this.txtAccount.Text = person.Id;
                    this.chbOriginPass.Checked = true;
                    this.chbLock.Checked = false;
                    this.txtMemo.Text = "";

                    //���û�,Ĭ��һ����ɫ,�ѵ�ǰ��ɫ��������ΪĬ�Ͻ�ɫ
                    allRolesOfUser.Add(currentRole);

                    this.txtAccount.Focus();
                    this.txtAccount.SelectAll();
                }
            }

            frmSelect.Dispose();
        }

        private void tvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {

            //��ǰ��ɫû��checked��������ѡ��
            if (tvRole.SelectedNode.Checked == false)
            {
                tvOrganization.Enabled = false;
            }
            else
            {
                tvOrganization.Enabled = true;
            }

            //��ʼ���ڵ�checkedֵ
            foreach (KeyValuePair<String, TreeNode> pairTree in orgTreeDictionary)
            {
                tvOrganization.AfterCheck -= tvOrganization_AfterCheck;
                pairTree.Value.Checked = false;
                tvOrganization.AfterCheck += tvOrganization_AfterCheck;
            }

            //ѡ��ͬȨ�ޣ�������ѡ�����֯�ṹ
            if (roleOrgDictionary != null&&roleOrgDictionary.Count!=0)
            {
                //�����ɫ��֯�ṹ�ֵ��ﲻ�����ý�ɫ���Ͳ�ȥ��������֯��Ԫ�����ã�ֱ�ӷ��ء�
                if(!roleOrgDictionary.ContainsKey((e.Node.Tag as IRole).Id))
                {
                    return;
                }

                foreach (String org in roleOrgDictionary[(e.Node.Tag as IRole).Id])
                {
                    if(org==null)
                    {
                        continue;
                    }
                    if (orgTreeDictionary.ContainsKey(org))
                    {
                        orgTreeDictionary[org].Checked = true;
                    }
                }
            }
        }

        private void tvOrganization_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //û��ѡ���ɫ���򷵻ء�
            if (tvRole.SelectedNode == null)
            {
                MessageBox.Show("��ѡ���ɫ��");
                return;
            }
            //�ж���֯�ṹ�Ƿ���ڵ㣬������Checked
            if ((e.Node.Tag as IOrganization).Id == null)
            {
                tvOrganization.AfterCheck -= tvOrganization_AfterCheck;
                e.Node.Checked = false;
                tvOrganization.AfterCheck += tvOrganization_AfterCheck;
                return;
            }
            if (e.Node.Checked == true)
            {
                if (!JudgeOrg((e.Node.Tag as IOrganization).Id))
                {
                    roleOrgDictionary[(tvRole.SelectedNode.Tag as IRole).Id].Add((e.Node.Tag as IOrganization).Id);
                }
            }
            else
            {
                if (JudgeOrg((e.Node.Tag as IOrganization).Id))
                {
                    roleOrgDictionary[(tvRole.SelectedNode.Tag as IRole).Id].Remove((e.Node.Tag as IOrganization).Id);
                }
            }
        }

        private void tvRole_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvRole.SelectedNode = e.Node;

            if (e.Node.Checked == true)
            {
                //������ɫ����֯�ṹ��Ӧ��
                if (!roleOrgDictionary.ContainsKey((e.Node.Tag as IRole).Id))
                {
                    roleOrgDictionary.Add(((e.Node.Tag as IRole).Id), new List<String>());
                }
            }
            if (e.Node.Checked == false)
            {
                if (roleOrgDictionary.ContainsKey((e.Node.Tag as IRole).Id))
                {
                    roleOrgDictionary.Remove((e.Node.Tag as IRole).Id);
                }
            }
            tvRole_AfterSelect(sender, e);
        }

        private void btnChanel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save() == 1)
            {
                MessageBox.Show("����ɹ�", "��Ϣ");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            //��ִ�б���������ѵ�ǰ״̬�Ľ�ɫ��Ϣ���û���Ϣ�����¸�Form�С�
            if (Save() == 1)
            {
                List<String> roleIdList = new List<string>();
                foreach (String roleId in roleOrgDictionary.Keys)
                {
                    roleIdList.Add(roleId);
                }
                AddUserChildForm frmUserChild = new AddUserChildForm(currentUser, roleIdList);

                frmUserChild.ShowDialog();
            
            }

        }

        private void nTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (nTabControl1.SelectedTab.Name == "tpRoleInfo")
            {
                btnDetail.Visible = true;
            }
            else
            {
                btnDetail.Visible = false;
            }
        }

        #endregion
    }
}