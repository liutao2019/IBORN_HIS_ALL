using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Interface.Account;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// 健康卡帐户密码
    /// </summary>
    public partial class ucHealthCardEditPassWord : UserControl
    {
        public ucHealthCardEditPassWord(bool isedit)
        {
            this.isEdit = isedit;
            InitializeComponent();
        }

        public ucHealthCardEditPassWord()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 是否修改密码
        /// </summary>
        private bool isEdit=false;

        /// <summary>
        /// 密码
        /// </summary>
        private string pwstr = string.Empty;

        /// <summary>
        /// 旧密码
        /// </summary>
        private string oldstr = string.Empty;

        public bool OpResult = false;
        #endregion

        #region 属性
        /// <summary>
        /// 是否修改密码
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return isEdit;
            }
            set
            {
                isEdit = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string PwStr
        {
            get
            {
                return pwstr;
            }
            set
            {
                pwstr = value;
            }
        }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPwStr
        {
            get
            {
                return oldstr;
            }
            set
            {
                oldstr = value;
            }
        }

        #endregion

        #region 方法

        private bool ValidPwd()
        {
            string newPwdStr = this.txtnewpwd.Text.Trim();
            string confirmPwdStr = this.txtconfirmpwd.Text.Trim();
            if (newPwdStr != confirmPwdStr)
            {
                MessageBox.Show("输入密码与确认密码不相符，请重新输入！");
                this.txtnewpwd.Text = string.Empty;
                this.txtconfirmpwd.Text = string.Empty;
                this.txtoldpwd.Text = string.Empty;
                this.txtnewpwd.Focus();
                return false;
            }
            if (IsEdit)
            {
                string oldPwdStr = this.txtoldpwd.Text.Trim();

                if (oldPwdStr.Trim().Length != 0)
                {
                    //if (!IsNumber(oldPwdStr))
                    //{
                    //    MessageBox.Show("密码必须为0-9的数字！", "提示");
                    //    this.txtnewpwd.Text = string.Empty;
                    //    this.txtconfirmpwd.Text = string.Empty;
                    //    this.txtoldpwd.Text = string.Empty;
                    //    this.txtoldpwd.Focus();
                    //    return false;
                    //}
                }
            }
            if (newPwdStr.Trim().Length != 0)
            {
                //if (!IsNumber(newPwdStr))
                //{
                //    MessageBox.Show("密码必须为0-9的数字！", "提示");
                //    this.txtnewpwd.Text = string.Empty;
                //    this.txtconfirmpwd.Text = string.Empty;
                //    this.txtoldpwd.Text = string.Empty;
                //    this.txtnewpwd.Focus();
                //    return false;
                //}

                if (newPwdStr.Length != 6)
                {
                    MessageBox.Show("密码必须为6位有效数字，请重新输入！", "提示");
                    this.txtnewpwd.Text = string.Empty;
                    this.txtconfirmpwd.Text = string.Empty;
                    this.txtoldpwd.Text = string.Empty;
                    this.txtnewpwd.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Confirm()
        {
            if (!ValidPwd())
            {
                this.OpResult = false;
                return;
            } 
            if (!IsEdit)
            {
                this.PwStr = this.txtnewpwd.Text;
                this.OpResult = true;
                this.FindForm().DialogResult = DialogResult.OK;
            }
            else
            {
                this.OldPwStr = this.txtoldpwd.Text;
                this.PwStr = this.txtnewpwd.Text;
                this.OpResult = true;
                this.FindForm().DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 验证字符串是否为数字
        /// </summary>
        /// <param name="checkStr">效验的字符串</param>
        /// <returns></returns>
        private bool IsNumber(string checkStr)
        {
            bool bl = true;
            foreach (char c in checkStr.ToCharArray())
            {
                if (!char.IsNumber(c))
                {
                    bl = false;
                    break;
                }
            }
            return bl;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region 事件
        private void ucHealthCardEditPassWord_Load(object sender, EventArgs e)
        {
            this.FindForm().Text = "密码输入";
            if (isEdit)
            {
                this.txtoldpwd.Enabled = true ;
                this.ActiveControl = this.txtoldpwd;
            }
            else
            {
                this.txtoldpwd.Enabled = false;
                this.ActiveControl = this.txtnewpwd;
            }
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btok_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void txtconfirmpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Confirm();
        }
        #endregion
    }
}
