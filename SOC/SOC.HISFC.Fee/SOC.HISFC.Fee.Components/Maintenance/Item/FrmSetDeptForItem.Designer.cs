namespace FS.SOC.HISFC.Fee.Components.Maintenance.Item
{
    partial class FrmSetDeptForItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblTxt = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkFilter = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btn_cancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btn_ok = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.btnClose = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ckChooseAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add( this.neuSpread1 );
            this.neuPanel1.Controls.Add( this.groupBox1 );
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size( 514, 397 );
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point( 0, 44 );
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange( new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1} );
            this.neuSpread1.Size = new System.Drawing.Size( 514, 353 );
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font( "宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)) );
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.LightBlue;
            this.neuPanel2.Controls.Add( this.btnClose );
            this.neuPanel2.Controls.Add( this.btn_cancel );
            this.neuPanel2.Controls.Add( this.btn_ok );
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point( 0, 403 );
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size( 514, 46 );
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // lblTxt
            // 
            this.lblTxt.AutoSize = true;
            this.lblTxt.ForeColor = System.Drawing.Color.Blue;
            this.lblTxt.Location = new System.Drawing.Point( 356, 17 );
            this.lblTxt.Name = "lblTxt";
            this.lblTxt.Size = new System.Drawing.Size( 59, 12 );
            this.lblTxt.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTxt.TabIndex = 3;
            this.lblTxt.Text = "neuLabel1";
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.ForeColor = System.Drawing.Color.Blue;
            this.chkFilter.Location = new System.Drawing.Point( 200, 16 );
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size( 72, 16 );
            this.chkFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkFilter.TabIndex = 2;
            this.chkFilter.Text = "过滤选中";
            this.chkFilter.UseVisualStyleBackColor = true;
            this.chkFilter.CheckedChanged += new System.EventHandler( this.chkFilter_CheckedChanged );
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point( 324, 11 );
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size( 75, 23 );
            this.btn_cancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "重选";
            this.btn_cancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler( this.btn_cancel_Click );
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point( 224, 11 );
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size( 75, 23 );
            this.btn_ok.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "保存";
            this.btn_ok.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler( this.btn_ok_Click );
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.lblTxt );
            this.groupBox1.Controls.Add( this.txtFilter );
            this.groupBox1.Controls.Add( this.ckChooseAll );
            this.groupBox1.Controls.Add( this.chkFilter );
            this.groupBox1.Controls.Add( this.neuLabel1 );
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point( 0, 0 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 514, 44 );
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point( 11, 17 );
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size( 41, 12 );
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "过  滤";
            // 
            // txtFilter
            // 
            this.txtFilter.IsEnter2Tab = false;
            this.txtFilter.Location = new System.Drawing.Point( 59, 13 );
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size( 133, 21 );
            this.txtFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFilter.TabIndex = 1;
            this.txtFilter.TextChanged += new System.EventHandler( this.txtFilter_TextChanged );
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point( 427, 11 );
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size( 75, 23 );
            this.btnClose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
            // 
            // ckChooseAll
            // 
            this.ckChooseAll.AutoSize = true;
            this.ckChooseAll.ForeColor = System.Drawing.Color.Blue;
            this.ckChooseAll.Location = new System.Drawing.Point( 278, 16 );
            this.ckChooseAll.Name = "ckChooseAll";
            this.ckChooseAll.Size = new System.Drawing.Size( 48, 16 );
            this.ckChooseAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckChooseAll.TabIndex = 2;
            this.ckChooseAll.Text = "全选";
            this.ckChooseAll.UseVisualStyleBackColor = true;
            this.ckChooseAll.CheckedChanged += new System.EventHandler( this.ckChooseAll_CheckedChanged );
            // 
            // FrmSetDeptForItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size( 514, 449 );
            this.Controls.Add( this.neuPanel2 );
            this.Controls.Add( this.neuPanel1 );
            this.Name = "FrmSetDeptForItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择开立科室";
            this.neuPanel1.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout( false );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuButton btn_cancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btn_ok;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTxt;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtFilter;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnClose;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckChooseAll;
    }
}