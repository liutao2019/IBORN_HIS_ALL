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
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
            this.nbtRegister.Click += new EventHandler(nbtRegister_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
        }

        private int CheckValid()
        {
            if (this.ntxtName.Text == "")
            {
                MessageBox.Show("账号不能为空");
                this.ntxtName.Select();
                this.ntxtName.Focus();
                return -1;
            }

            if (this.ntxtPassword.Text == "")
            {
                MessageBox.Show("密码不能为空");
                this.ntxtPassword.Select();
                this.ntxtPassword.Focus();
                return -1;
            }

            if (this.ntxtPassword.Text != this.ntxtRePassword.Text)
            {
                MessageBox.Show("两次录入的密码不一致");
                this.ntxtPassword.Select();
                this.ntxtPassword.Focus();
                return -1;
            }

            return 1;
        }

        private int RegisterAccount()
        {
            if (this.CheckValid()!= 1)
            {
                return -1;
            }
            AccountInfo accountInfo = new AccountInfo();
            accountInfo.AccountID = this.ntxtName.Text;
            accountInfo.Password = FS.HisCrypto.DESCryptoService.DESEncrypt(this.ntxtPassword.Text, FS.FrameWork.Management.Connection.DESKey);
            accountInfo.Telephone = this.ntxtTelephone.Text;
            accountInfo.EMail = this.ntxtEMail.Text;


            AccountManager curAccountManager = new AccountManager();
            if (!((FS.HISFC.Models.Base.Employee)curAccountManager.Operator).IsManager)
            {
                accountInfo.Address = ((FS.HISFC.Models.Base.Employee)curAccountManager.Operator).Dept.Name;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (curAccountManager.InsertAccount(accountInfo) == -1)
            {

                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("注册失败："+curAccountManager.Err);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("注册成功！");
            return 1;
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        void nbtRegister_Click(object sender, EventArgs e)
        {
            this.RegisterAccount();
            this.DialogResult = DialogResult.OK;
        }

        
    }
}
