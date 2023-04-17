using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Forms
{
    public partial class frmFirstDayChange : Form
    {
        public frmFirstDayChange()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void frmDCOrder_Load(object sender, EventArgs e)
        {
            this.btnOk.Click += new EventHandler(btnOK_Click);
            this.btnCancle.Click += new EventHandler(btnCancel_Click);
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string firstUseNum = this.txtFirstDay.Text.ToString();
            if (string.IsNullOrEmpty(firstUseNum))
            {
                MessageBox.Show("首日量不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int firstUseNumInt = 0;
            if (!Int32.TryParse(firstUseNum, out firstUseNumInt) || firstUseNumInt < 0 || firstUseNumInt > this.frequency.Times.Length)
            {
                MessageBox.Show("您输入的首日量有误", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch { }
            this.Close();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 首日量
        /// </summary>
        public string FirstUseNum
        {
            get
            {
                if (string.IsNullOrEmpty(this.txtFirstDay.Text))
                {
                    return "0";
                }
                return this.txtFirstDay.Text;
            }
            set
            {
                this.txtFirstDay.Text = value;
            }
        }
        /// <summary>
        /// 频次
        /// </summary>
        private FS.HISFC.Models.Order.Frequency frequency;
        public FS.HISFC.Models.Order.Frequency Frequency
        {
            set
            {
                this.frequency = value;
            }
        }
    }
}
