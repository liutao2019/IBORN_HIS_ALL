using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Account;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// ��Ȩ��Ϣ�Ľ������޸�
    /// </summary>
    public partial class ucEmpowerInfo : UserControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="obj">��Ȩʵ��</param>
        public ucEmpowerInfo(AccountEmpower obj, bool isedit)
        {
            InitializeComponent();
            accountEmpower = obj;
            isEdit = isedit;
        }

        #region ����
        /// <summary>
        /// ��Ȩ��Ϣʵ��
        /// </summary>
        AccountEmpower accountEmpower = new AccountEmpower();

        /// <summary>
        /// �ʻ�������
        /// </summary>
        HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
 
        /// <summary>
        /// �Ƿ���Ϣ
        /// </summary>
        private bool isEdit = false;
        #endregion

        #region ����
        /// <summary>
        /// �ʻ���Ȩ��Ϣ
        /// </summary>
        public AccountEmpower AccountEmpower
        {
            get
            {
                return accountEmpower;
            }
            set
            {
                accountEmpower = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// У������
        /// </summary>
        /// <returns></returns>
        private bool ValidPassword()
        {
            string newpwd = this.txtnewpwd.Text.Trim();
            string confirmpwd = this.txtconfirmpwd.Text.Trim();
            if (newpwd == string.Empty)
            {
                MessageBox.Show("���������룡");
                return false;
            }
            if (newpwd.Length != 6)
            {
                MessageBox.Show("�������Ϊ6λ����Ч���֣�");
                return false;
            }
            if (newpwd != confirmpwd)
            {
                MessageBox.Show("������������벻��������������룡");
                return false;
            }
            if (!IsNumber(newpwd))
            {
                MessageBox.Show("���������벻�Ϸ����������룡");
                this.txtnewpwd.Focus();
                return false;

            }
            if (!IsNumber(confirmpwd))
            {
                MessageBox.Show("���������벻�Ϸ����������룡");
                return false;
            }
            return true;

        }

        private bool IsNumber(string str)
        {
            foreach (char c in str.ToCharArray())
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// У��
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            decimal money = NConvert.ToDecimal(this.txtMoney.Text);
            if (money <= 0)
            {
                MessageBox.Show("��������Ȩ�޶");
                this.txtMoney.Focus();
                return false;
            }

            if (isEdit)
            {
                if (accountEmpower.PassWord != this.txtoldpw.Text.Trim())
                {
                    MessageBox.Show("ԭʼ�������벻��ȷ���������룡");
                    this.txtnewpwd.Focus();
                    return false;
                }
            }
            if (ckflag.Checked)
            {
                if (!ValidPassword())
                {
                    this.txtnewpwd.Text = string.Empty;
                    this.txtconfirmpwd.Text = string.Empty;
                    this.txtnewpwd.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///  �޸���Ȩ��Ϣ
        /// </summary>
        protected virtual void UpdateEmpowerInfo()
        {
            decimal feeMoney = 0m;//���ѽ��
            decimal limit = 0m;//�޶�
            decimal vacancy = 0m;//���
            //ˢ����Ȩ��Ϣ
            int resultVlaue = accountManager.QueryEmpower(accountEmpower.AccountNO, accountEmpower.EmpowerCard.Patient.PID.CardNO, ref accountEmpower);
            if (resultVlaue <= 0)
            {
                MessageBox.Show("������Ȩ��Ϣʧ�ܣ�" + accountManager.Err);
                return;
            }
            limit = NConvert.ToDecimal(this.txtMoney.Text);
            //���ѽ��
            feeMoney = accountEmpower.EmpowerLimit - accountEmpower.Vacancy;
            //��ǰ��Ȩ�޶������ѽ������
            vacancy = limit - feeMoney;
            if (vacancy < 0)
            {
                MessageBox.Show("���ý��" + feeMoney.ToString() + "Ԫ������Ȩ�޶�" + limit.ToString() + "Ԫ\n�����޸���Ȩ�޶");
                this.txtMoney.Focus();
                this.txtMoney.SelectAll();
                return;
            }
            accountEmpower.Vacancy = vacancy;
            accountEmpower.EmpowerLimit = limit;
            //�޸�����
            if (ckflag.Checked)
            {
                accountEmpower.PassWord = this.txtnewpwd.Text.Trim();
            }
            if (accountManager.UpdateEmpower(accountEmpower) < 0)
            {
                MessageBox.Show("������Ȩ��Ϣʧ�ܣ�");
            }
            else
            {
                MessageBox.Show("������Ȩ��Ϣ�ɹ���");
                this.FindForm().DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region �¼�
        private void ucEmpowerInfo_Load(object sender, EventArgs e)
        {
            if (!isEdit)
            {
                this.txtoldpw.Enabled = false;
                this.ckflag.Visible = false;
            }
            else
            {
                this.ckflag.Checked = false;
                this.ckflag.Visible = true;
            }
            this.txtMoney.Text = accountEmpower.EmpowerLimit.ToString();
            this.txtoldpw.Text = accountEmpower.PassWord;
            this.ActiveControl = this.txtMoney;
            this.txtMoney.SelectAll();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        protected virtual void Save()
        {
            //ˢ����Ȩ��Ϣ
            
            if (!Valid()) return;
            //�½��ʻ���Ϣ
            if (!isEdit)
            {
                accountEmpower.EmpowerLimit = NConvert.ToDecimal(this.txtMoney.Text);
                accountEmpower.PassWord = this.txtnewpwd.Text.Trim();
                this.FindForm().DialogResult = DialogResult.OK;
            }
            //�޸��ʻ���Ϣ
            else
            {
                UpdateEmpowerInfo();
            }

        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void txtconfirmpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Save();
            }
        }

        private void ckflag_CheckedChanged(object sender, EventArgs e)
        {
            this.txtnewpwd.Enabled = this.ckflag.Checked;
            this.txtoldpw.Enabled = this.ckflag.Checked;
            this.txtconfirmpwd.Enabled = this.ckflag.Checked;
            if (this.ckflag.Checked)
            {
                this.txtnewpwd.Focus();
            }
        }

        #endregion
    }
}
