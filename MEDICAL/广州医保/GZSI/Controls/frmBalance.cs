using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GZSI.Controls
{
    public partial class frmBalance : System.Windows.Forms.Form
    {
        public ucBalanceClinic ucBalanceClinic1;
        /// <summary>
        /// 必需的设计器变量
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmBalance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改

        /// 此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.ucBalanceClinic1 = new ucBalanceClinic();
            this.SuspendLayout();
            // 
            // ucBalanceClinic1
            // 
            this.ucBalanceClinic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBalanceClinic1.Location = new System.Drawing.Point(0, 0);
            this.ucBalanceClinic1.Name = "ucBalanceClinic1";
            this.ucBalanceClinic1.Size = new System.Drawing.Size(504, 246);
            this.ucBalanceClinic1.TabIndex = 0;
            // 
            // frmBalance
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(504, 246);
            this.ControlBox = false;
            this.Controls.Add(this.ucBalanceClinic1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBalance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "广州医保结算信息";
            this.Closed += new System.EventHandler(this.frmBalance_Closed);
            this.ResumeLayout(false);

        }
        #endregion

        private void frmBalance_Closed(object sender, System.EventArgs e)
        {

        }
    }
}
