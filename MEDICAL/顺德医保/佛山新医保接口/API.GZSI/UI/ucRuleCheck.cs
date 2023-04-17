using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.UI
{
    /// <summary>
    /// {EA2C0BFD-8ECC-4a37-A26D-6837E1B4ED43} chenw 
    /// 市直事前提醒不规则原因填写
    /// </summary>
    public partial class ucRuleCheck : Form
    {
        #region 属性

        /// <summary>
        /// 不校验原因
        /// </summary>
        public string bka933 { get; set; }

        #endregion 属性

        public ucRuleCheck()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("原因必须输入！！！");
            }
            else
            {
                this.bka933 = this.textBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
