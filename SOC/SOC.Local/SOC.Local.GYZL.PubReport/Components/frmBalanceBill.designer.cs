namespace FS.SOC.Local.GYZL.PubReport.Components
{
    partial class frmBalanceBill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ucBalanceBill ucBalanceBill1;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBalanceBill));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.tbCDFee = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbbOpenAccount = new System.Windows.Forms.ToolBarButton();
            this.tbFresh = new System.Windows.Forms.ToolBarButton();
            this.toolBarbtnOpenAccount = new System.Windows.Forms.ToolBarButton();
            this.tbQuit = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtZLSP = new System.Windows.Forms.TextBox();
            this.txtGJ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenAccount = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.fpCost = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCost_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucBalanceBill1 = new FS.SOC.Local.GYZL.PubReport.Components.ucBalanceBill();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCost_Sheet1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            //this.statusBar1.Location = new System.Drawing.Point(0, 613);
            //this.statusBar1.Size = new System.Drawing.Size(1164, 24);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbSave,
            this.tbPrint,
            this.tbCDFee,
            this.toolBarButton1,
            this.tbbOpenAccount,
            this.tbFresh,
            this.toolBarbtnOpenAccount,
            this.tbQuit});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(1164, 57);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 1;
            this.tbSave.Name = "tbSave";
            this.tbSave.Text = "预算(&S)";
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 0;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Text = "打 单(P)";
            // 
            // tbCDFee
            // 
            this.tbCDFee.ImageIndex = 1;
            this.tbCDFee.Name = "tbCDFee";
            this.tbCDFee.Text = "收  费";
            this.tbCDFee.Visible = false;
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbOpenAccount
            // 
            this.tbbOpenAccount.ImageIndex = 2;
            this.tbbOpenAccount.Name = "tbbOpenAccount";
            this.tbbOpenAccount.Text = "开 帐(O)";
            // 
            // tbFresh
            // 
            this.tbFresh.ImageIndex = 3;
            this.tbFresh.Name = "tbFresh";
            this.tbFresh.Text = "清 屏";
            // 
            // toolBarbtnOpenAccount
            // 
            this.toolBarbtnOpenAccount.Name = "toolBarbtnOpenAccount";
            this.toolBarbtnOpenAccount.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbQuit
            // 
            this.tbQuit.ImageIndex = 4;
            this.tbQuit.Name = "tbQuit";
            this.tbQuit.Text = " 退 出";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "打印清单.png");
            this.imageList1.Images.SetKeyName(1, "收费.png");
            this.imageList1.Images.SetKeyName(2, "开帐.png");
            this.imageList1.Images.SetKeyName(3, "清屏.png");
            this.imageList1.Images.SetKeyName(4, "退出.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.txtZLSP);
            this.panel1.Controls.Add(this.txtGJ);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnOpenAccount);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ucQueryInpatientNo1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1164, 580);
            this.panel1.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.Location = new System.Drawing.Point(226, 7);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 16);
            this.lblInfo.TabIndex = 9;
            // 
            // txtZLSP
            // 
            this.txtZLSP.Location = new System.Drawing.Point(632, 15);
            this.txtZLSP.Name = "txtZLSP";
            this.txtZLSP.Size = new System.Drawing.Size(100, 21);
            this.txtZLSP.TabIndex = 8;
            this.txtZLSP.Visible = false;
            this.txtZLSP.TextChanged += new System.EventHandler(this.txtZLSP_TextChanged);
            // 
            // txtGJ
            // 
            this.txtGJ.Location = new System.Drawing.Point(472, 13);
            this.txtGJ.Name = "txtGJ";
            this.txtGJ.Size = new System.Drawing.Size(72, 21);
            this.txtGJ.TabIndex = 7;
            this.txtGJ.Visible = false;
            this.txtGJ.TextChanged += new System.EventHandler(this.txtGJ_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(560, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "肿瘤审批：";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(416, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "高检费：";
            this.label3.Visible = false;
            // 
            // btnOpenAccount
            // 
            this.btnOpenAccount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenAccount.Location = new System.Drawing.Point(305, 12);
            this.btnOpenAccount.Name = "btnOpenAccount";
            this.btnOpenAccount.Size = new System.Drawing.Size(64, 23);
            this.btnOpenAccount.TabIndex = 4;
            this.btnOpenAccount.Text = "开账(&O)";
            this.btnOpenAccount.Visible = false;
            this.btnOpenAccount.Click += new System.EventHandler(this.btnOpenAccount_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(235, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "打单(&P)";
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1164, 512);
            this.panel2.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1164, 510);
            this.splitContainer1.SplitterDistance = 388;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.fpCost);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(388, 510);
            this.panel4.TabIndex = 0;
            // 
            // fpCost
            // 
            this.fpCost.About = "3.0.2004.2005";
            this.fpCost.AccessibleDescription = "fpCost, Sheet1, Row 0, Column 0, ";
            this.fpCost.BackColor = System.Drawing.Color.White;
            this.fpCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpCost.FileName = "";
            this.fpCost.Font = new System.Drawing.Font("宋体", 9F);
            this.fpCost.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCost.IsAutoSaveGridStatus = false;
            this.fpCost.IsCanCustomConfigColumn = false;
            this.fpCost.Location = new System.Drawing.Point(0, 0);
            this.fpCost.Name = "fpCost";
            this.fpCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpCost.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCost_Sheet1});
            this.fpCost.Size = new System.Drawing.Size(388, 510);
            this.fpCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpCost.TabIndex = 100;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCost.TextTipAppearance = tipAppearance1;
            this.fpCost.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpCost_Sheet1
            // 
            this.fpCost_Sheet1.Reset();
            this.fpCost_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCost_Sheet1.ColumnCount = 5;
            this.fpCost_Sheet1.RowCount = 0;
            this.fpCost_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用科目";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "总金额";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "记账";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "自费";
            this.fpCost_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpCost_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCost_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpCost_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpCost_Sheet1.Columns.Get(0).Label = "选择";
            this.fpCost_Sheet1.Columns.Get(0).Width = 30F;
            this.fpCost_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpCost_Sheet1.Columns.Get(1).Label = "费用科目";
            this.fpCost_Sheet1.Columns.Get(1).Locked = true;
            this.fpCost_Sheet1.Columns.Get(1).Width = 150F;
            this.fpCost_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.fpCost_Sheet1.Columns.Get(2).Label = "总金额";
            this.fpCost_Sheet1.Columns.Get(2).Locked = true;
            this.fpCost_Sheet1.Columns.Get(2).Width = 65F;
            this.fpCost_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpCost_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpCost_Sheet1.RowHeader.Columns.Get(0).Width = 25F;
            this.fpCost_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpCost_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCost_Sheet1.Rows.Default.Height = 25F;
            this.fpCost_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpCost_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpCost_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpCost.SetActiveViewport(0, 1, 0);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucBalanceBill1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(772, 510);
            this.panel3.TabIndex = 0;
            // 
            // ucBalanceBill1
            // 
            this.ucBalanceBill1.BackColor = System.Drawing.Color.White;
            this.ucBalanceBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBalanceBill1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBalanceBill1.InPatientNo = null;
            this.ucBalanceBill1.IsBalance = false;
            this.ucBalanceBill1.Location = new System.Drawing.Point(0, 0);
            this.ucBalanceBill1.Name = "ucBalanceBill1";
            this.ucBalanceBill1.Size = new System.Drawing.Size(772, 510);
            this.ucBalanceBill1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1164, 2);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1164, 2);
            this.label1.TabIndex = 0;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(8, 8);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(190, 32);
            this.ucQueryInpatientNo1.TabIndex = 1;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // frmBalanceBill
            // 
            this.ClientSize = new System.Drawing.Size(1164, 637);
            this.Controls.Add(this.panel1);
            //this.Controls.Add(this.toolBar1);
            this.Name = "frmBalanceBill";
            //this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结算清单";
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBalanceBill_Load);
            //this.Controls.SetChildIndex(this.toolBar1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            //this.Controls.SetChildIndex(this.statusBar1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCost_Sheet1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        //private FS.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private System.Windows.Forms.Panel panel2;
        
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolBarButton tbPrint;
        private System.Windows.Forms.ToolBarButton tbFresh;
        private System.Windows.Forms.ToolBarButton toolBarbtnOpenAccount;
        private System.Windows.Forms.ToolBarButton tbQuit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnOpenAccount;
        private System.Windows.Forms.ToolBarButton tbbOpenAccount;
        private System.Windows.Forms.ToolBarButton tbCDFee;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZLSP;
        private System.Windows.Forms.TextBox txtGJ;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpCost;
        protected FarPoint.Win.Spread.SheetView fpCost_Sheet1;
        private System.Windows.Forms.ToolBarButton tbSave;
        private System.Windows.Forms.Label lblInfo;
    }
}