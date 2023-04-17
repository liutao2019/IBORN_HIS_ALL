using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Common.Base
{
    public partial class frmCheckPassWord : Form
    {
        public frmCheckPassWord(string operNO)
        {
            InitializeComponent();

            this.ntxtEmployNO.Text = operNO;
            this.ntxtEmployNO.ReadOnly = true;

            this.nbtOK.Click += new EventHandler(nbtOK_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);

            this.Load += new EventHandler(frmCheckPassWord_Load);
        }

        void frmCheckPassWord_Load(object sender, EventArgs e)
        {
            this.ntxtPassword.Select();
            this.ntxtPassword.Focus();
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        void nbtOK_Click(object sender, EventArgs e)
        {
            if (this.CheckPassword(this.ntxtEmployNO.Text, this.ntxtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.ntxtPassword.Select();
                this.ntxtPassword.Focus();
            }
        }

       
        public bool CheckPassword(string operNO, string password)
        {
            FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
            string accessPassword = personMgr.ExecSqlReturnOne(string.Format("select t.password from priv_com_user t where t.personid = '{0}'", operNO), "");
            if (accessPassword == "-1")
            {
                System.Windows.Forms.MessageBox.Show("验证用户失败：\n" + personMgr.Err);
            }
            else if (accessPassword == "")
            {
                System.Windows.Forms.MessageBox.Show("用户不存在！");
            }
            else if (FS.HisCrypto.DESCryptoService.DESDecrypt(accessPassword, "Core_H_N") == password)
            {
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("用户或密码错误！");
            }

            return false;
        }
    }
}
