namespace FS.SOC.Local.OutpatientFee.FoSan

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucInvoiceTree));
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.dtpRegEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpRegBegin = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClose = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.panelTree = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.trvInvoice = new System.Windows.Forms.TreeView();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panelTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblInvoiceDate);
            this.pnlLeft.Controls.Add(this.dtpRegEnd);
            this.pnlLeft.Controls.Add(this.dtpRegBegin);
            this.pnlLeft.Controls.Add(this.btnShow);
            this.pnlLeft.Controls.Add(this.btnClose);
            this.pnlLeft.Controls.Add(this.panelTree);
            this.pnlLeft.Controls.Add(this.txtCardNO);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(218, 524);
            this.pnlLeft.TabIndex = 2;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(10, 9);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(65, 12);
            this.lblInvoiceDate.TabIndex = 8;
            this.lblInvoiceDate.Text = "挂号日期：";
            // 
            // dtpRegEnd
            // 
            this.dtpRegEnd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpRegEnd.Location = new System.Drawing.Point(78, 30);
            this.dtpRegEnd.Name = "dtpRegEnd";
            this.dtpRegEnd.Size = new System.Drawing.Size(120, 23);
            this.dtpRegEnd.TabIndex = 7;
            // 
            // dtpRegBegin
            // 
            this.dtpRegBegin.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpRegBegin.Location = new System.Drawing.Point(78, 5);
            this.dtpRegBegin.Name = "dtpRegBegin";
            this.dtpRegBegin.Size = new System.Drawing.Size(120, 23);
            this.dtpRegBegin.TabIndex = 7;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.Location = new System.Drawing.Point(205, 83);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(13, 387);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = ">";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Visible = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.Location = new System.Drawing.Point(202, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(11, 11);
            this.btnClose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnClose.TabIndex = 6;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelTree
            // 
            this.panelTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTree.Controls.Add(this.trvInvoice);
            this.panelTree.Location = new System.Drawing.Point(3, 85);
            this.panelTree.Name = "panelTree";
            this.panelTree.Size = new System.Drawing.Size(215, 433);
            this.panelTree.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.panelTree.TabIndex = 1;
            // 
            // trvInvoice
            // 
            this.trvInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvInvoice.Location = new System.Drawing.Point(0, 0);
            this.trvInvoice.Name = "trvInvoice";
            this.trvInvoice.Size = new System.Drawing.Size(215, 433);
            this.trvInvoice.TabIndex = 0;
            this.trvInvoice.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvInvoice_AfterSelect);
            // 
            // txtCardNO
            // 
            this.txtCardNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(4, 58);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(211, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 0;
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // ucInvoiceTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLeft);
            this.Name = "ucInvoiceTree";
            this.Size = new System.Drawing.Size(218, 524);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panelTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.DateTimePicker dtpRegBegin;
        protected System.Windows.Forms.Button btnShow;
        protected FS.FrameWork.WinForms.Controls.NeuPictureBox btnClose;
        protected FS.FrameWork.WinForms.Controls.NeuPanel panelTree;
        private System.Windows.Forms.TreeView trvInvoice;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNO;
        private System.Windows.Forms.DateTimePicker dtpRegEnd;
    }
}
