namespace FS.HISFC.Components.OutpatientFee.Controls
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
            this.lblCardNO = new System.Windows.Forms.Label();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.dtpRegEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpRegBegin = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClose = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.panelTree = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.trvInvoice = new System.Windows.Forms.TreeView();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panelTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.label1);
            this.pnlLeft.Controls.Add(this.txtName);
            this.pnlLeft.Controls.Add(this.lblCardNO);
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
            // lblCardNO
            // 
            this.lblCardNO.AutoSize = true;
            this.lblCardNO.BackColor = System.Drawing.Color.Transparent;
            this.lblCardNO.ForeColor = System.Drawing.Color.Blue;
            this.lblCardNO.Location = new System.Drawing.Point(3, 62);
            this.lblCardNO.Name = "lblCardNO";
            this.lblCardNO.Size = new System.Drawing.Size(71, 12);
            this.lblCardNO.TabIndex = 9;
            this.lblCardNO.Text = "卡号\\病历号";
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
            this.btnShow.Location = new System.Drawing.Point(205, 114);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(13, 356);
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
            this.panelTree.Location = new System.Drawing.Point(3, 114);
            this.panelTree.Name = "panelTree";
            this.panelTree.Size = new System.Drawing.Size(215, 404);
            this.panelTree.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.panelTree.TabIndex = 1;
            // 
            // trvInvoice
            // 
            this.trvInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvInvoice.Location = new System.Drawing.Point(0, 0);
            this.trvInvoice.Name = "trvInvoice";
            this.trvInvoice.Size = new System.Drawing.Size(215, 404);
            this.trvInvoice.TabIndex = 0;
            this.trvInvoice.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvInvoice_AfterSelect);
            // 
            // txtCardNO
            // 
            this.txtCardNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(78, 56);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(134, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 0;
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(78, 85);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(134, 23);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 10;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(45, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "姓名";
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
        private System.Windows.Forms.Label lblCardNO;
        private System.Windows.Forms.Label label1;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
    }
}
