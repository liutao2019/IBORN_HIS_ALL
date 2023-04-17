using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Forms
{
    public partial class frmAccountTaken : FS.FrameWork.WinForms.Forms.BaseForm
    {
        #region 成员
        /// <summary>
        /// 取现金额
        /// </summary>
        decimal decTakenMoney = 0;

        #endregion

        #region 属性
        /// <summary>
        /// 取现金额
        /// </summary>
        public decimal TakenMoney
        {
            get { return decTakenMoney; }
            set { txtpay.Text = value.ToString(); }
        }
        public FS.HISFC.Components.Common.Controls.cmbPayType PayType
        {
            get { return cmbPayType; }
        }
        /// <summary>
        /// 取现提醒
        /// </summary>
        public decimal DecLimiteCanclePerpayMoney { get; set; }
        #endregion

        public frmAccountTaken()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            decTakenMoney = FS.FrameWork.Function.NConvert.ToDecimal(this.txtpay.Text.Trim());
            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("请选择支付方式！", "提示");
                this.cmbPayType.Focus();
                return;
            }
            if (decTakenMoney <= 0)
            {
                MessageBox.Show("请输入正确的取现金额!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtpay.Focus();
                return;
            }
            if (decTakenMoney >= DecLimiteCanclePerpayMoney && cmbPayType.Text == "现金")
            {
                DialogResult dr = MessageBox.Show("取现金额大于限制" + DecLimiteCanclePerpayMoney + "元,是否继续用现金取现?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    this.cmbPayType.Focus();
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtpay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdOK_Click(null, null);
            }
        }

        private void frmAccountTaken_Load(object sender, EventArgs e)
        {
            this.txtpay.Focus();
            if (cmbPayType.Items.Count > 0)
                cmbPayType.SelectedIndex = 0;
        }
    }
}
