using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// 消费时，密码输入
    /// </summary>
    public partial class ucPassWord : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Account.IPassWord
    {
        public ucPassWord()
        {
            InitializeComponent();
        }

        #region 变量
        private bool validPassWord = false;
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        //FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 费用综合业务层 
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        bool isOK = false;
        #endregion
        #region 属性

        #endregion

        #region 方法
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            string passStr = this.txtpassword.Text.Trim();
            if (passStr == string.Empty)
            {
                MessageBox.Show("请输入用户密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpassword.Focus();
                return false;
            }
            string password = feeIntegrate.GetPassWordByCardNO(patient.PID.CardNO);
            if (password == "-1")
            {
                MessageBox.Show("查询用户密码失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (passStr != password)
            {
                MessageBox.Show("密码输入错误，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpassword.Text = string.Empty;
                txtpassword.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region IPassWord 成员

        /// <summary>
        /// 门诊卡号
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }
        /// <summary>
        /// 验证用户密码
        /// </summary>
        public bool ValidPassWord
        {
            get
            {
                return validPassWord;
            }
        }

        public bool IsOK
        {
            get
            {
                return isOK;
            }
        }

        #endregion

        #region 事件

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOk_Click(this, new EventArgs());
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            validPassWord = this.Valid();
            if (!validPassWord) return;
            isOK = true;
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.FindForm().Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                isOK = false;
                this.FindForm().Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        private void ucPassWord_Load(object sender, EventArgs e)
        {
            this.lblPact.Text = patient.Pact.Name;
            this.lblName.Text = patient.Name;
            this.lblSex.Text = patient.Sex.Name;
            this.lblBirthDay.Text = patient.Birthday.ToString("yyyy-MM-dd");
            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            this.txtpassword.Text = string.Empty;
            this.ActiveControl = this.txtpassword;


        }

        #region IPassWord 成员

        public string CardNO
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}
