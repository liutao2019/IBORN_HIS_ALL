using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Project
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.nbtLogin.Click += new EventHandler(nbtLogin_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);

            string val = "";
            val = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Login", "AccountName", val);
            this.ntxtName.Text = val;

        }


        private AccountInfo curAccountInfo = new AccountInfo();

        public AccountInfo CurLoginAccountInfo
        {
            get { return curAccountInfo; }
            set { curAccountInfo = value; }
        }

        void nbtLogin_Click(object sender, EventArgs e)
        {
            if (this.ntxtName.Text == "")
            {
                MessageBox.Show("请录入账号");
                this.ntxtName.Select();
                this.ntxtName.Focus();
                return;
            }

            if (this.ntxtPassword.Text == "")
            {
                MessageBox.Show("请输入密码");
                this.ntxtPassword.Select();
                this.ntxtPassword.Focus();
                return;
            }

            AccountManager curAccountManager = new AccountManager();
            curAccountInfo = curAccountManager.GetAccountInfo(this.ntxtName.Text);
            if (curAccountInfo == null)
            {
                MessageBox.Show("账号不存在，如果有疑问请与系统管理员联系");
                this.ntxtName.Select();
                this.ntxtName.Focus();
                return;
            }
            if (curAccountInfo.State == "2")
            {
                MessageBox.Show("账号已停用，如果有疑问请与系统管理员联系");
                this.ntxtName.Select();
                this.ntxtName.Focus();
                return;
            }
            if (FS.HisCrypto.DESCryptoService.DESEncrypt(this.ntxtPassword.Text, FS.FrameWork.Management.Connection.DESKey) != curAccountInfo.Password)
            {
                curAccountInfo = null;
                MessageBox.Show("密码不正确");
                this.ntxtPassword.Select();
                this.ntxtPassword.Focus();
                return;
            }

            string val = this.ntxtName.Text;
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Login", "AccountName", val);
            this.Hide();
        }


        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
