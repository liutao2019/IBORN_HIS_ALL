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
using FS.HISFC.Models.Privilege;
//using FS.WinForms.Forms;

//using FS.Framework;



namespace FS.HISFC.Components.Privilege
{
    public partial class AddUserForm : FS.HISFC.Components.Privilege.InputBaseForm
    {
        private IList<Role> roles = new List<Role>();
        private IList<Organization> organizations = new List<Organization>();
        private Role currentRole = null;
        private IList<Person> persons = new List<Person>();
        private List<User> newModifyUser = new List<User>();
        private IList<Role> allRolesOfUser = new List<Role>();
        ////��ǰ��ɫ��֯�ṹ��ϵ
        //private Dictionary<Role, List<Organization>> roleOrgMapping = new Dictionary<Role, List<Organization>>();
        //��ǰ��ɫ��֯�ṹ��ϵ
        private Dictionary<String, List<String>> roleOrgDictionary = new Dictionary<String, List<String>>();
        //��֯�ṹ��
        private Dictionary<String, TreeNode> orgTreeDictionary = new Dictionary<String, TreeNode>();
        //��ɫ��
        private Dictionary<String, TreeNode> roleTreeDictionary = new Dictionary<String, TreeNode>();
        //�޸�ʱ�����������û���Ϣ
        private User origin = null;
        //��ǰ��ȡ�ؼ����û���Ϣ
        private User currentUser = null;

        public AddUserForm(Role _currentRole)
        {
            currentRole = _currentRole;
            InitializeComponent();
            FS.FrameWork.WinForms.Classes.Function.SetTabControlStyle(this.nTabControl1);
        }

        public AddUserForm(User user, Role _currentRole)
        {
            currentRole = _currentRole;
            origin = user;
            InitializeComponent();
            FS.FrameWork.WinForms.Classes.Function.SetTabControlStyle(this.nTabControl1);
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
        //private void LoadOrg()
        //{
        //    try
        //    {
        //        PrivilegeService _proxy = Common.Util.CreateProxy();
        //        using (_proxy as IDisposable)
        //        {
        //            organizations = _proxy.QueryUnit("HIS");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message, "��ʾ");
        //        return;
        //    }
        //}

        private void LoadRole()
        {
            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    //roles = _proxy.QueryChildRole(((Role)((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).CurrentGroup).ID);
                    roles = _proxy.QueryChildRole(currentRole.ID);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            //TreeNode _root = NewNode((Role)((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).CurrentGroup);
            //roles.Add((Role)((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).CurrentGroup);
            //roleTreeDictionary.Add(FS.FrameWork.Management.Connection.Operator.ID, _root);
            //tvRole.Nodes.Add(_root);

            TreeNode _root = NewNode(currentRole);
            roles.Add(currentRole);
            roleTreeDictionary.Add(currentRole.ID, _root);
            tvRole.Nodes.Add(_root);
            AddSubRoleNode((_root.Tag as Role), _root);
            _root.Expand();
        }

        private int LoadPerson()
        {
            PrivilegeService proxy = Common.Util.CreateProxy();

            try
            {
                IList<string> keys;
                using (proxy as IDisposable)
                {
                    keys = proxy.QueryAppID();
                    foreach (string _key in keys)
                    {
                        IList<Person> collection = proxy.QueryPerson(_key);
                        foreach (Person person in collection)
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

        //private void LoadTreeView()
        //{
        //    tvOrganization.Nodes.Clear();

        //    //���õ�ǰ���ڵ�Tag
        //    if (organizations != null)
        //    {
        //        Organization rootOrg = organizations[0];


        //        TreeNode rootNode = new TreeNode();
        //        rootNode.Text = rootOrg.Department.Name.ToString();
        //        rootNode.Tag = rootOrg;
        //        rootNode.Expand();
        //        tvOrganization.Nodes.Add(rootNode);

        //        //ɾ�����ڵ㣬׼�������ӽڵ�
        //        organizations.RemoveAt(0);

        //        SetChildNode(rootNode, organizations);
        //    }

        //}

        private void SetChildNode(TreeNode parentNode, IList<Organization> organizations)
        {
            List<TreeNode> childs = null;
            if (parentNode != null)
            {
                childs = new List<TreeNode>();
                Organization parentOrg = parentNode.Tag as Organization;
                List<Organization> childOrgList = new List<Organization>();

                foreach (Organization org in organizations)
                {
                    if (org.ParentId == parentOrg.ID)
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
                            Organization orgChange = null;
                            orgChange = childOrgList[i];
                            childOrgList[i] = childOrgList[i + 1];
                            childOrgList[i + 1] = orgChange;
                        }
                    }
                }

                foreach (Organization newOrg in childOrgList)
                {

                    TreeNode newNode = new TreeNode();
                    newNode.Text = newOrg.Department.Name;
                    newNode.Tag = newOrg;
                    childs.Add(newNode);
                }

                foreach (TreeNode node in childs)
                {
                    parentNode.Nodes.Add(node);
                    orgTreeDictionary.Add((node.Tag as Organization).ID, node);
                    SetChildNode(node, organizations);
                }

            }

        }

        private void AddSubRoleNode(Role parent, TreeNode parentNode)
        {
            foreach (Role _role in roles)
            {
                if (_role.ParentId == parent.ID)
                {
                    TreeNode _node = NewNode(_role);
                    _node.Tag = _role;
                    roleTreeDictionary.Add(_role.ID, _node);
                    parentNode.Nodes.Add(_node);

                    AddSubRoleNode(_role, _node);
                }
            }
        }

        private TreeNode NewNode(Role role)
        {
            TreeNode _node = new TreeNode(role.Name);
            _node.Tag = role;
            _node.Name = role.ID;
            return _node;
        }

        private string GetDefaultPassword()
        {
            string _value = System.Configuration.ConfigurationManager.AppSettings["DefaultPassword"];
            if (string.IsNullOrEmpty(_value))
            {
                //return "-";
                //{D515E09B-E299-47e0-BF19-EDFDB6E4C775}
                return "F07ZpVACvxE=";
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
        private User IsExistUser(string appId, string personId)
        {
            User _user = null;
            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
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

        private void ModifyUser(User user)
        {
            this.txtUserName.Text = user.Name;
            this.txtUserName.Tag = user.AppId + "||" + user.PersonId;
            this.txtAccount.Text = user.Account;
            this.chbOriginPass.Checked = false;
            this.chbLock.Checked = user.IsLock;
            this.txtMemo.Text = user.Description;
            //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
            this.chbManager.Checked = user.IsManager;

        }

        //�ж��������ֵ����Ƿ����Iorg
        private bool JudgeOrg(String checkedOrgId)
        {
            return roleOrgDictionary[(tvRole.SelectedNode.Tag as Role).ID].Contains(checkedOrgId);
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
                else
                {
                    roleTreeDictionary[roleId].Checked = false;
                }
            }
        }

        //��õ�ǰ�û���Ϣ
        private User GetValue()
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
                currentUser.Password =GetDefaultPassword();
            }

            currentUser.IsLock = chbLock.Checked;
            currentUser.Description = txtMemo.Text.Trim();
            currentUser.operId = FS.FrameWork.Management.Connection.Operator.ID;
            currentUser.OperDate = FrameWork.Function.NConvert.ToDateTime(new FrameWork.Management.DataBaseManger().GetSysDateTime());
            //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
            currentUser.IsManager = this.chbManager.Checked;

            return currentUser;
        }

        private void GetRoleOrgMapping()
        {
            PrivilegeService proxy = Common.Util.CreateProxy();
            using (proxy as IDisposable)
            {
                roleOrgDictionary = proxy.QueryAuthorityRoleOrg(origin);
            }
        }

        private int Save()
        {
            int ret = -1;
            GetValue();
            if (Check())
            {
   
                PrivilegeService proxy = Common.Util.CreateProxy();
                using (proxy as IDisposable)
                {
                    try
                    {
                        FrameWork.Management.PublicTrans.BeginTransaction();
                        ret = proxy.SaveAuthorityRoleOrg(currentUser, roleOrgDictionary);
                        FrameWork.Management.PublicTrans.Commit();

                    }
                    catch (Exception e)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        ret=-1;
                    }
                }

            }
            return ret;
        }

        private bool Check()
        {
            if (string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ���¼�û�" ), "��ʾ" );
                this.btnSelectUser.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtAccount.Text.Trim()))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��¼�ʻ�����Ϊ��!" ), "��ʾ" );
                this.txtAccount.Focus();
                return false;
            }

            if (!FrameWork.Public.String.ValidMaxLengh(this.txtAccount.Text.Trim(), 20))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��¼�ʻ����Ȳ��ܳ���20���ַ�!" ), "��ʾ" );
                this.txtAccount.Focus();
                return false;
            }
            //����û�û����Ȩ��ɫ����ʾ
            if (roleOrgDictionary.Count == 0)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "û�и��û���Ȩ��ɫ" ), "��ʾ" );
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

                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    User _user = _proxy.GetUserByAccount(txtAccount.Text.Trim());
                    if (_user != null && _user.Id != userId)
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�õ�¼�ʻ��Ѿ�����,��ʹ�������ʻ�!" ), "��ʾ" );
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return false;
            }

            /*if (!FS.Framework.Util.StringHelper.ValidMaxLengh(this.txtPassword.Text.Trim(), 20))
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

            if (!FrameWork.Public.String.ValidMaxLengh(this.txtMemo.Text.Trim(), 256))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ע���Ȳ��ܳ���128������!" ), "��ʾ" );
                this.txtMemo.Focus();
                return false;
            }

            return true;
        }

        private void DeleteUser()
        {
            if (origin.Id == "admin")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ϵͳĬ���û�����ɾ��" ) );
                return;
            }

            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�Ҫɾ�����û�?"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            PrivilegeService proxy = Common.Util.CreateProxy();
            using (proxy as IDisposable)
            {
                int ret = proxy.DeleteAuthority(origin);
                if (ret >= 0)
                {
                    this.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
            
        }

        #endregion


        #region �¼�
        private void AddUserForm_Load(object sender, EventArgs e)
        {
            LoadRole();
            LoadPerson();
            //LoadOrg();
            //LoadTreeView();
            //�޸�ʱ��ʼ������
            if (origin != null)
            {
                ModifyUser(origin);
                GetRoleOrgMapping();
                InitRoleTreeChecked();
                btnDeleteUser.Enabled = true;
            }
        }

        private void btnSelectUser_Click(object sender, EventArgs e)
        {
            SelectItemForm<Person> frmSelect = new SelectItemForm<Person>();
            frmSelect.Id = "Id";
            frmSelect.Value = "Name";
            frmSelect.Description = "Remark";
            frmSelect.SecondKey = "AppId";
            frmSelect.InitItem(persons);
            frmSelect.ShowDialog();

            if (frmSelect.DialogResult == DialogResult.OK)
            {
                Person person = frmSelect.SelectedItem;
                this.txtUserName.Text = person.Name;

                ///�жϸ��û��Ƿ��Ѿ�����,������Ӽ��������û�
                User user = IsExistUser(person.AppId, person.Id);

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
                    //����û�������ɫ
                    roleOrgDictionary = new Dictionary<string, List<string>>();
                    //��ʼ����ɫ����Checked����
                    InitRoleTreeChecked();

                    this.txtUserName.Text = person.Name;
                    this.txtUserName.Tag = person.AppId + "||" + person.Id;
                    this.txtAccount.Text = person.Id;
                    this.chbOriginPass.Checked = true;
                    this.chbLock.Checked = false;
                    this.txtMemo.Text = "";

                    ////���û�,Ĭ��һ����ɫ,�ѵ�ǰ��ɫ��������ΪĬ�Ͻ�ɫ
                    //allRolesOfUser.Add(currentRole);

                    this.txtAccount.Focus();
                    this.txtAccount.SelectAll();
                }
            }

            frmSelect.Dispose();
        }

        private void tvRole_AfterSelect(object sender, TreeViewEventArgs e)
        {

            ////��ǰ��ɫû��checked��������ѡ��
            //if (tvRole.SelectedNode.Checked == false)
            //{
            //    tvOrganization.Enabled = false;
            //}
            //else
            //{
            //    tvOrganization.Enabled = true;
            //}

            //��ʼ���ڵ�checkedֵ
            //foreach (KeyValuePair<String, TreeNode> pairTree in orgTreeDictionary)
            //{
            //    tvOrganization.AfterCheck -= tvOrganization_AfterCheck;
            //    pairTree.Value.Checked = false;
            //    tvOrganization.AfterCheck += tvOrganization_AfterCheck;
            //}

            //ѡ��ͬȨ�ޣ�������ѡ�����֯�ṹ
            if (roleOrgDictionary != null&&roleOrgDictionary.Count!=0)
            {
                //�����ɫ��֯�ṹ�ֵ��ﲻ�����ý�ɫ���Ͳ�ȥ��������֯��Ԫ�����ã�ֱ�ӷ��ء�
                if (!roleOrgDictionary.ContainsKey((e.Node.Tag as Role).ID))
                {
                    return;
                }

                foreach (String org in roleOrgDictionary[(e.Node.Tag as Role).ID])
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
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ���ɫ" ) );
                return;
            }
            ////�ж���֯�ṹ�Ƿ���ڵ㣬������Checked
            //if ((e.Node.Tag as Organization).ID == null)
            //{
            //    tvOrganization.AfterCheck -= tvOrganization_AfterCheck;
            //    e.Node.Checked = false;
            //    tvOrganization.AfterCheck += tvOrganization_AfterCheck;
            //    return;
            //}
            if (e.Node.Checked == true)
            {
                if (!JudgeOrg((e.Node.Tag as Organization).ID))
                {
                    roleOrgDictionary[(tvRole.SelectedNode.Tag as Role).ID].Add((e.Node.Tag as Organization).ID);
                }
            }
            else
            {
                if (JudgeOrg((e.Node.Tag as Organization).ID))
                {
                    roleOrgDictionary[(tvRole.SelectedNode.Tag as Role).ID].Remove((e.Node.Tag as Organization).ID);
                }
            }
        }

        private void tvRole_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvRole.SelectedNode = e.Node;

            if (e.Node.Checked == true)
            {
                //������ɫ����֯�ṹ��Ӧ��
                if (!roleOrgDictionary.ContainsKey((e.Node.Tag as Role).ID))
                {
                    roleOrgDictionary.Add(((e.Node.Tag as Role).ID), new List<String>());
                }
            }
            if (e.Node.Checked == false)
            {
                if (roleOrgDictionary.ContainsKey((e.Node.Tag as Role).ID))
                {
                    roleOrgDictionary.Remove((e.Node.Tag as Role).ID);
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
            if (Save() == 0)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "����ɹ�" ), "��Ϣ" );
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("����ʧ��", "��Ϣ");
            }
        }

        private void nTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            //if (nTabControl1.SelectedTab.Name == "tpRoleInfo")
            //{
            //    btnDetail.Visible = true;

            //}
            //else
            //{
            //    btnDetail.Visible = false;
            //}
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }
        #endregion


    }
}