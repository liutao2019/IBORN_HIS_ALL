using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neusoft.Privilege.ServiceContracts.Contract;
using Neusoft.Privilege.ServiceContracts.Model.Impl;
//using Neusoft.Framework;
//using Neusoft.WinForms.Forms;
using Neusoft.Privilege.WinForms.Forms;
namespace Neusoft.Privilege.WinForms
{
    public partial class ZZZZAddMenuForm : InputBaseForm
    {
        public ZZZZAddMenuForm(Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem parent)
        {
            InitializeComponent();

            _parent = parent;
            Init();
        }

        public ZZZZAddMenuForm(Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem menu, Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem parent)
        {
            InitializeComponent();

            _orig = menu;
            _parent = parent;
            Init();
        }

        private Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _orig;
        private Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _parent;
        private Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _current;

        public Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem Current
        {
            get { return _current; }            
        }

        private void Init()
        {
            this.InitShortCutList();
            this.btnOK.Click += new EventHandler(btnOK_Click);
            
            //�����Ӳ˵�
            if (_orig == null)
            {
                this.txtParentName.Text = _parent.Name;
            }
            else//�޸Ĳ˵�
            {
                this.txtMenuName.Text = _orig.Name;
                this.txtParentName.Text = _parent.Name;
                this.cmbShortcut.Text = _orig.Shortcut;
                this.txtTooltips.Text = _orig.Tooltip;
                this.txtDllName.Text = _orig.DllName;
                this.txtWinName.Text = _orig.WinName;
                this.txtTreeDllName.Text = _orig.TreeDllName;
                this.txtTreeName.Text = _orig.TreeName;
                this.txtParam.Text = _orig.Param;
                //this.cmbControlType.SelectedIndex = (string.IsNullOrEmpty(_orig.ControlType)?0:(_orig.ControlType=="Form"?1:(_orig.ControlType=="Control"?2:3)));
                this.cmbShowType.SelectedIndex = (_orig.ShowType=="Show"?1:(_orig.ShowType=="ShowDialog"?0:(_orig.ShowType=="Web"?2:0)));
                this.chbEnabled.Checked = _orig.Enabled;
            }

            //this.cmbShowType.SelectedIndex = 0;
        }        

        void btnOK_Click(object sender, EventArgs e)
        {
            //����˵�
            if (Check() == -1) return;

            Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem _menu = GetValue();

            try
            {
                IPrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    _current = _proxy.SaveMenuItem(_menu);
                }
            }
            catch (Exception ex)
            {
                _current = null;
                MessageBox.Show(ex.Message, "��ʾ");
                return;
            }
            MessageBox.Show("����ɹ�!", "��ʾ");

            //this.Close();
        }

        private int Check()
        {
            if (string.IsNullOrEmpty(this.txtMenuName.Text.Trim()))
            {
                MessageBox.Show("�˵����Ʋ���Ϊ��!", "��ʾ");
                this.txtMenuName.Focus();
                return -1;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtMenuName.Text.Trim(), 60))
            {
                MessageBox.Show("�˵����Ƴ��Ȳ��ܳ���30������!", "��ʾ");
                this.txtMenuName.Focus();
                return -1;
            }

            if (this.cmbShortcut.Text.Trim() != "" && cmbShortcut.SelectedIndex == -1)
            {
                MessageBox.Show("�޴˿�ݼ�!", "��ʾ");
                this.cmbShortcut.Focus();
                return -1;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtTooltips.Text.Trim(), 50))
            {
                MessageBox.Show("������ʾ���Ȳ��ܳ���25������!", "��ʾ");
                this.txtTooltips.Focus();
                return -1;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtDllName.Text.Trim(), 50))
            {
                MessageBox.Show("���ó������Ƴ��Ȳ��ܳ���50����ĸ!", "��ʾ");
                this.txtDllName.Focus();
                return -1;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtWinName.Text.Trim(), 100))
            {
                MessageBox.Show("���ÿؼ����Ƴ��Ȳ��ܳ���100����ĸ!", "��ʾ");
                this.txtWinName.Focus();
                return -1;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtTreeDllName.Text.Trim(), 50))
            {
                MessageBox.Show("���ؼ��������Ƴ��Ȳ��ܳ���50����ĸ!", "��ʾ");
                this.txtTreeDllName.Focus();
                return -1;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtTreeName.Text.Trim(), 100))
            {
                MessageBox.Show("���ؼ����Ƴ��Ȳ��ܳ���100����ĸ!", "��ʾ");
                this.txtTreeName.Focus();
                return -1;
            }

            if (!Neusoft.Framework.Util.StringHelper.ValidMaxLengh(this.txtParam.Text.Trim(), 100))
            {
                MessageBox.Show("����������Ȳ��ܳ���100����ĸ!", "��ʾ");
                this.txtParam.Focus();
                return -1;
            }

            return 0;
        }

        private Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem GetValue()
        {
            if(_current == null)
                _current = new Neusoft.Privilege.ServiceContracts.Model.Impl.MenuItem();

            if (_orig != null)
            {
                _current.Id = _orig.Id;
                _current.Order = _orig.Order;
            }
            _current.Name = txtMenuName.Text.Trim();
            _current.ParentId = _parent.Id;
            _current.Shortcut = cmbShortcut.Text.Trim();
            _current.Tooltip = txtTooltips.Text.Trim();
            _current.DllName = txtDllName.Text.Trim();
            _current.WinName = txtWinName.Text.Trim();
            _current.TreeDllName = txtTreeDllName.Text.Trim();
            _current.TreeName = txtTreeName.Text.Trim();
            _current.Param = txtParam.Text.Trim();
            //_current.ControlType = cmbControlType.Text == "" ? "" : (cmbControlType.Text == "����" ? "Form" : (cmbControlType.Text == "�ؼ�" ? "Control" : "Report"));
            _current.ShowType = cmbShowType.Text == "" ? "" : (cmbShowType.Text == "ģʽ����" ? "ShowDialog" : (cmbShowType.Text == "��ģʽ����" ? "Show" : "Web"));
            _current.Enabled = chbEnabled.Checked;
            _current.UserId = ((Facade.Context.Operator as NeuPrincipal).Identity as NeuIdentity).User.Id;
            _current.OperDate = Common.Util.GetDateTime();
            _current.Layer = Convert.ToString(long.Parse(_parent.Layer) + 1);

            return _current;
        }

        private void InitShortCutList()
        {
            string[] _names = Enum.GetNames(typeof(Shortcut));
            AutoCompleteStringCollection _autoCollection = new AutoCompleteStringCollection();

            foreach (string _name in _names)
            {
                this.cmbShortcut.Items.Add(_name);
                _autoCollection.Add(_name);
            }

            this.cmbShortcut.AutoCompleteCustomSource = _autoCollection;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                Control _control = this.ActiveControl;
                if (_control != null && _control.Name == "cmbShortcut")
                {
                    this.txtTooltips.Focus();
                }
                else if (_control != null && _control.Name == "cmbShowType")
                {
                    this.chbEnabled.Focus();
                }
                //else if (_control != null && _control.Name == "cmbControlType")
                //{
                //    this.cmbShowType.Focus();
                //}
                else if (_control != null && _control.Name == "chbEnabled")
                {
                    this.btnOK.Focus();
                }
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}

