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
    /// �������ʻ�����
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

        #region ����
        /// <summary>
        /// �Ƿ��޸�����
        /// </summary>
        private bool isEdit=false;

        /// <summary>
        /// ����
        /// </summary>
        private string pwstr = string.Empty;

        /// <summary>
        /// ������
        /// </summary>
        private string oldstr = string.Empty;

        public bool OpResult = false;
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ��޸�����
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
        /// ����
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
        /// ������
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

        #region ����

        private bool ValidPwd()
        {
            string newPwdStr = this.txtnewpwd.Text.Trim();
            string confirmPwdStr = this.txtconfirmpwd.Text.Trim();
            if (newPwdStr != confirmPwdStr)
            {
                MessageBox.Show("����������ȷ�����벻��������������룡");
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
                    //    MessageBox.Show("�������Ϊ0-9�����֣�", "��ʾ");
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
                //    MessageBox.Show("�������Ϊ0-9�����֣�", "��ʾ");
                //    this.txtnewpwd.Text = string.Empty;
                //    this.txtconfirmpwd.Text = string.Empty;
                //    this.txtoldpwd.Text = string.Empty;
                //    this.txtnewpwd.Focus();
                //    return false;
                //}

                if (newPwdStr.Length != 6)
                {
                    MessageBox.Show("�������Ϊ6λ��Ч���֣����������룡", "��ʾ");
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
        /// ����
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
        /// ��֤�ַ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="checkStr">Ч����ַ���</param>
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

        #region �¼�
        private void ucHealthCardEditPassWord_Load(object sender, EventArgs e)
        {
            this.FindForm().Text = "��������";
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
