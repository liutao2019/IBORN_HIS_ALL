namespace HISFC.Components.Package.Fee.Controls
{
    partial class ucInvoiceTree
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCardNO = new System.Windows.Forms.Label();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.panelTree = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.trvInvoice = new System.Windows.Forms.TreeView();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCardNO
            // 
            this.lblCardNO.AutoSize = true;
            this.lblCardNO.BackColor = System.Drawing.Color.Transparent;
            this.lblCardNO.ForeColor = System.Drawing.Color.Blue;
            this.lblCardNO.Location = new System.Drawing.Point(2, 63);
            this.lblCardNO.Name = "lblCardNO";
            this.lblCardNO.Size = new System.Drawing.Size(71, 12);
            this.lblCardNO.TabIndex = 15;
            this.lblCardNO.Text = "卡号\\病历号";
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(9, 10);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(65, 12);
            this.lblInvoiceDate.TabIndex = 14;
            this.lblInvoiceDate.Text = "收据日期：";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEnd.Location = new System.Drawing.Point(77, 31);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(134, 23);
            this.dtpEnd.TabIndex = 13;
            // 
            // dtpBegin
            // 
            this.dtpBegin.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpBegin.Location = new System.Drawing.Point(77, 6);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(134, 23);
            this.dtpBegin.TabIndex = 12;
            // 
            // panelTree
            // 
            this.panelTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTree.Controls.Add(this.trvInvoice);
            this.panelTree.Location = new System.Drawing.Point(2, 110);
            this.panelTree.Name = "panelTree";
            this.panelTree.Size = new System.Drawing.Size(215, 409);
            this.panelTree.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.panelTree.TabIndex = 11;
            // 
            // trvInvoice
            // 
            this.trvInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvInvoice.Location = new System.Drawing.Point(0, 0);
            this.trvInvoice.Name = "trvInvoice";
            this.trvInvoice.Size = new System.Drawing.Size(215, 409);
            this.trvInvoice.TabIndex = 0;
            // 
            // txtCardNO
            // 
            this.txtCardNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(77, 57);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(134, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 10;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(77, 84);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(134, 23);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 16;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(44, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "姓名";
            // 
            // ucInvoiceTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblCardNO);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpBegin);
            this.Controls.Add(this.panelTree);
            this.Controls.Add(this.txtCardNO);
            this.Name = "ucInvoiceTree";
            this.Size = new System.Drawing.Size(218, 524);
            this.panelTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCardNO;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        protected FS.FrameWork.WinForms.Controls.NeuPanel panelTree;
        private System.Windows.Forms.TreeView trvInvoice;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private System.Windows.Forms.Label label1;
    }
}
