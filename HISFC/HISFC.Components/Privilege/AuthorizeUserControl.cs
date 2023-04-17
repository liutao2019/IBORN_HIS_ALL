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
    public partial class AuthorizeUserControl : UserControl
    {
        public Role currentRole = new Role();

        public AuthorizeUserControl()
        {
            InitializeComponent();

        }

        public void AddUser()
        {
            AddUserForm frmUser = new AddUserForm(currentRole);
            frmUser.ShowDialog();
            LoadUser();
        }

        public void ModifyUser()
        {
            if (nTreeListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��Ҫ�޸ĵ�����" ) );
                return;
            }
            AddUserForm frmUser = new AddUserForm(nTreeListView1.SelectedItems[0].Tag as User, currentRole);

            frmUser.ShowDialog();
            LoadUser();
        }

        public void DeleteUser()
        {
            if (nTreeListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��Ҫ�޸ĵ�����" ) );
                return;
            }

            if ((nTreeListView1.SelectedItems[0].Tag as User).Id == "admin")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ϵͳĬ���û�����ɾ��" ) );
                return;
            }

            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("ȷ����Ҫɾ����ǰѡ���������"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            PrivilegeService proxy = Common.Util.CreateProxy();
            using (proxy as IDisposable)
            {
                int ret = proxy.DeleteAuthority(nTreeListView1.SelectedItems[0].Tag as User);
            }
            LoadUser();
        }

        private void LoadUser()
        {
            PrivilegeService _proxy = Common.Util.CreateProxy();

            try
            {
                IList<User> _users=null;
                using (_proxy as IDisposable)
                {
                    _users = _proxy.QueryUsers(currentRole.ID);
                }
                InitTreeListView(_users);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
        }

        private void InitTreeListView(IList<User> users)
        {
            nTreeListView1.Items.Clear();

            foreach (User _user in users)
            {
                TreeListViewItem _item = NewItem(_user);
                _item.Tag = _user;
                this.nTreeListView1.Items.Add(_item);
            }
        }

        private TreeListViewItem NewItem(User user)
        {
            TreeListViewItem item = new TreeListViewItem(user.PersonId);
            item.SubItems.AddRange( new string[] {user.Name,user.Account,
                                         (user.IsLock? FS.FrameWork.Management.Language.Msg("��"):FS.FrameWork.Management.Language.Msg("��")),user.Description } );
            item.Tag = user;
            item.ImageIndex = 0;
            return item;
        }

        private void AuthorizeUserControl_Load(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void tmspAdd_Click(object sender, EventArgs e)
        {
            AddUser();
        }

        private void tmspModify_Click(object sender, EventArgs e)
        {
            ModifyUser();
        }

        private void tmspDelete_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void nTreeListView1_DoubleClick(object sender, EventArgs e)
        {
            ModifyUser();
        }

    }
}
