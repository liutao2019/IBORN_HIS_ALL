namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucPatientCase
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
            try
            {
                CacheManager.OutOrderMgr.Dispose();
                this.tempCaseModule.Dispose();
                this.caseHistory.Dispose();
                System.GC.Collect();
            }
            catch { }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPatientCase));
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.trvlsbl = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.chkMemo = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkGms = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkCheck = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkOld = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkNow = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkMain = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtCheck = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.txtGms = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.txtOld = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.txtNow = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.txtMain = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.ucDiagnose1 = new FS.HISFC.Components.Common.Controls.ucDiagnose();
            this.txtDiagnose3 = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.txtDiagnose2 = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.txtDiagnose = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.txtMemo = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkGm = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkCrb = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox4 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.trvblmb = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblmzh = new System.Windows.Forms.Label();
            this.lblage = new System.Windows.Forms.Label();
            this.lbllb = new System.Windows.Forms.Label();
            this.lblsex = new System.Windows.Forms.Label();
            this.lblks = new System.Windows.Forms.Label();
            this.lblname = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grmb = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btNew = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.chkIsPerson = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grbl = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnNewbl = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnprint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnSavebl = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cMenu = new FS.FrameWork.WinForms.Controls.NeuContexMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucUserText1 = new FS.HISFC.Components.Common.Controls.ucUserText();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSplitter2 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuGroupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grmb.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grbl.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(224, 8);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(149, 24);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "门 诊 病 历";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.trvlsbl);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(3, 72);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(171, 491);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 3;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "历史病历";
            // 
            // trvlsbl
            // 
            this.trvlsbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvlsbl.HideSelection = false;
            this.trvlsbl.ImageIndex = 0;
            this.trvlsbl.ImageList = this.imageList2;
            this.trvlsbl.Location = new System.Drawing.Point(3, 17);
            this.trvlsbl.Name = "trvlsbl";
            this.trvlsbl.SelectedImageIndex = 0;
            this.trvlsbl.Size = new System.Drawing.Size(165, 471);
            this.trvlsbl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.trvlsbl.TabIndex = 0;
            this.trvlsbl.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvlsbl_AfterSelect);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "打印输液卡16.GIF");
            this.imageList2.Images.SetKeyName(1, "打印执行单16.GIF");
            this.imageList2.Images.SetKeyName(2, "病历16.GIF");
            this.imageList2.Images.SetKeyName(3, "安排16.GIF");
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuGroupBox3.Controls.Add(this.chkMemo);
            this.neuGroupBox3.Controls.Add(this.chkGms);
            this.neuGroupBox3.Controls.Add(this.chkCheck);
            this.neuGroupBox3.Controls.Add(this.chkOld);
            this.neuGroupBox3.Controls.Add(this.chkNow);
            this.neuGroupBox3.Controls.Add(this.chkMain);
            this.neuGroupBox3.Controls.Add(this.txtCheck);
            this.neuGroupBox3.Controls.Add(this.txtGms);
            this.neuGroupBox3.Controls.Add(this.txtOld);
            this.neuGroupBox3.Controls.Add(this.txtNow);
            this.neuGroupBox3.Controls.Add(this.txtMain);
            this.neuGroupBox3.Controls.Add(this.ucDiagnose1);
            this.neuGroupBox3.Controls.Add(this.txtDiagnose3);
            this.neuGroupBox3.Controls.Add(this.txtDiagnose2);
            this.neuGroupBox3.Controls.Add(this.txtDiagnose);
            this.neuGroupBox3.Controls.Add(this.txtMemo);
            this.neuGroupBox3.Controls.Add(this.neuLabel14);
            this.neuGroupBox3.Controls.Add(this.neuLabel13);
            this.neuGroupBox3.Controls.Add(this.chkGm);
            this.neuGroupBox3.Controls.Add(this.chkCrb);
            this.neuGroupBox3.Controls.Add(this.neuLabel12);
            this.neuGroupBox3.Controls.Add(this.neuLabel11);
            this.neuGroupBox3.Controls.Add(this.neuLabel10);
            this.neuGroupBox3.Controls.Add(this.neuLabel9);
            this.neuGroupBox3.Controls.Add(this.neuLabel8);
            this.neuGroupBox3.Location = new System.Drawing.Point(3, 111);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(670, 481);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 4;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "病历";
            // 
            // chkMemo
            // 
            this.chkMemo.AutoSize = true;
            this.chkMemo.Checked = true;
            this.chkMemo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMemo.Location = new System.Drawing.Point(7, 446);
            this.chkMemo.Name = "chkMemo";
            this.chkMemo.Size = new System.Drawing.Size(60, 16);
            this.chkMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkMemo.TabIndex = 23;
            this.chkMemo.Text = "常用语";
            this.chkMemo.UseVisualStyleBackColor = true;
            this.chkMemo.Visible = false;
            // 
            // chkGms
            // 
            this.chkGms.AutoSize = true;
            this.chkGms.Checked = true;
            this.chkGms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGms.Location = new System.Drawing.Point(7, 211);
            this.chkGms.Name = "chkGms";
            this.chkGms.Size = new System.Drawing.Size(60, 16);
            this.chkGms.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkGms.TabIndex = 25;
            this.chkGms.Text = "常用语";
            this.chkGms.UseVisualStyleBackColor = true;
            this.chkGms.Visible = false;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoSize = true;
            this.chkCheck.Checked = true;
            this.chkCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheck.Location = new System.Drawing.Point(8, 262);
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.Size = new System.Drawing.Size(60, 16);
            this.chkCheck.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkCheck.TabIndex = 25;
            this.chkCheck.Text = "常用语";
            this.chkCheck.UseVisualStyleBackColor = true;
            this.chkCheck.Visible = false;
            // 
            // chkOld
            // 
            this.chkOld.AutoSize = true;
            this.chkOld.Checked = true;
            this.chkOld.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOld.Location = new System.Drawing.Point(6, 152);
            this.chkOld.Name = "chkOld";
            this.chkOld.Size = new System.Drawing.Size(60, 16);
            this.chkOld.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkOld.TabIndex = 14;
            this.chkOld.Text = "常用语";
            this.chkOld.UseVisualStyleBackColor = true;
            this.chkOld.Visible = false;
            // 
            // chkNow
            // 
            this.chkNow.AutoSize = true;
            this.chkNow.Checked = true;
            this.chkNow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNow.Location = new System.Drawing.Point(6, 94);
            this.chkNow.Name = "chkNow";
            this.chkNow.Size = new System.Drawing.Size(60, 16);
            this.chkNow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkNow.TabIndex = 12;
            this.chkNow.Text = "常用语";
            this.chkNow.UseVisualStyleBackColor = true;
            this.chkNow.Visible = false;
            // 
            // chkMain
            // 
            this.chkMain.AutoSize = true;
            this.chkMain.Checked = true;
            this.chkMain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMain.Location = new System.Drawing.Point(6, 35);
            this.chkMain.Name = "chkMain";
            this.chkMain.Size = new System.Drawing.Size(60, 16);
            this.chkMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkMain.TabIndex = 10;
            this.chkMain.Text = "常用语";
            this.chkMain.UseVisualStyleBackColor = true;
            this.chkMain.Visible = false;
            // 
            // txtCheck
            // 
            this.txtCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheck.Location = new System.Drawing.Point(74, 246);
            this.txtCheck.Name = "txtCheck";
            this.txtCheck.Size = new System.Drawing.Size(579, 47);
            this.txtCheck.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCheck.TabIndex = 45;
            this.txtCheck.Text = "";
            this.txtCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyDown);
            // 
            // txtGms
            // 
            this.txtGms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGms.Location = new System.Drawing.Point(74, 189);
            this.txtGms.Name = "txtGms";
            this.txtGms.Size = new System.Drawing.Size(579, 47);
            this.txtGms.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtGms.TabIndex = 44;
            this.txtGms.Text = "";
            this.txtGms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyDown);
            // 
            // txtOld
            // 
            this.txtOld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOld.Location = new System.Drawing.Point(74, 130);
            this.txtOld.Name = "txtOld";
            this.txtOld.Size = new System.Drawing.Size(579, 47);
            this.txtOld.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOld.TabIndex = 43;
            this.txtOld.Text = "";
            this.txtOld.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyDown);
            // 
            // txtNow
            // 
            this.txtNow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNow.Location = new System.Drawing.Point(74, 73);
            this.txtNow.Name = "txtNow";
            this.txtNow.Size = new System.Drawing.Size(579, 47);
            this.txtNow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNow.TabIndex = 42;
            this.txtNow.Text = "";
            this.txtNow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyDown);
            // 
            // txtMain
            // 
            this.txtMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMain.Location = new System.Drawing.Point(74, 17);
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(579, 47);
            this.txtMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMain.TabIndex = 41;
            this.txtMain.Text = "";
            this.txtMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyDown);
            // 
            // ucDiagnose1
            // 
            this.ucDiagnose1.AlDiag = ((System.Collections.ArrayList)(resources.GetObject("ucDiagnose1.AlDiag")));
            this.ucDiagnose1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucDiagnose1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ucDiagnose1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucDiagnose1.IsShowICDCategory = true;
            this.ucDiagnose1.Location = new System.Drawing.Point(74, 428);
            this.ucDiagnose1.Name = "ucDiagnose1";
            this.ucDiagnose1.Size = new System.Drawing.Size(579, 100);
            this.ucDiagnose1.TabIndex = 38;
            // 
            // txtDiagnose3
            // 
            this.txtDiagnose3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiagnose3.Location = new System.Drawing.Point(74, 382);
            this.txtDiagnose3.Name = "txtDiagnose3";
            this.txtDiagnose3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtDiagnose3.Size = new System.Drawing.Size(579, 35);
            this.txtDiagnose3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagnose3.TabIndex = 40;
            this.txtDiagnose3.Text = "";
            this.txtDiagnose3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiagnose3_KeyDown);
            this.txtDiagnose3.Enter += new System.EventHandler(this.txtDiagnose3_Enter);
            this.txtDiagnose3.Leave += new System.EventHandler(this.txtDiagnose3_Leave);
            this.txtDiagnose3.TextChanged += new System.EventHandler(this.txtDiagnose3_TextChanged);
            // 
            // txtDiagnose2
            // 
            this.txtDiagnose2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiagnose2.Location = new System.Drawing.Point(74, 341);
            this.txtDiagnose2.Name = "txtDiagnose2";
            this.txtDiagnose2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtDiagnose2.Size = new System.Drawing.Size(579, 35);
            this.txtDiagnose2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagnose2.TabIndex = 39;
            this.txtDiagnose2.Text = "";
            this.txtDiagnose2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiagnose2_KeyDown);
            this.txtDiagnose2.Enter += new System.EventHandler(this.txtDiagnose2_Enter);
            this.txtDiagnose2.Leave += new System.EventHandler(this.txtDiagnose2_Leave);
            this.txtDiagnose2.TextChanged += new System.EventHandler(this.txtDiagnose2_TextChanged);
            // 
            // txtDiagnose
            // 
            this.txtDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiagnose.Location = new System.Drawing.Point(74, 300);
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtDiagnose.Size = new System.Drawing.Size(579, 35);
            this.txtDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagnose.TabIndex = 32;
            this.txtDiagnose.Text = "";
            this.txtDiagnose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiagnose_KeyDown);
            this.txtDiagnose.Enter += new System.EventHandler(this.txtDiagnose_Enter);
            this.txtDiagnose.Leave += new System.EventHandler(this.txtDiagnose_Leave);
            this.txtDiagnose.TextChanged += new System.EventHandler(this.txtDiagnose_TextChanged);
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.EnableAutoDragDrop = true;
            this.txtMemo.Location = new System.Drawing.Point(74, 426);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtMemo.Size = new System.Drawing.Size(579, 49);
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMemo.TabIndex = 33;
            this.txtMemo.Text = "";
            this.txtMemo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMemo_KeyUp);
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel14.Location = new System.Drawing.Point(4, 428);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(63, 14);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 21;
            this.neuLabel14.Text = "备　注：";
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoEllipsis = true;
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel13.Location = new System.Drawing.Point(5, 311);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(63, 14);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 20;
            this.neuLabel13.Text = "诊　断：";
            // 
            // chkGm
            // 
            this.chkGm.AutoSize = true;
            this.chkGm.ForeColor = System.Drawing.Color.Red;
            this.chkGm.Location = new System.Drawing.Point(581, 277);
            this.chkGm.Name = "chkGm";
            this.chkGm.Size = new System.Drawing.Size(72, 16);
            this.chkGm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkGm.TabIndex = 8;
            this.chkGm.Text = "是否过敏";
            this.chkGm.UseVisualStyleBackColor = true;
            this.chkGm.Visible = false;
            // 
            // chkCrb
            // 
            this.chkCrb.AutoSize = true;
            this.chkCrb.BackColor = System.Drawing.Color.Transparent;
            this.chkCrb.ForeColor = System.Drawing.Color.Red;
            this.chkCrb.Location = new System.Drawing.Point(574, 253);
            this.chkCrb.Name = "chkCrb";
            this.chkCrb.Size = new System.Drawing.Size(84, 16);
            this.chkCrb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkCrb.TabIndex = 7;
            this.chkCrb.Text = "是否传染病";
            this.chkCrb.UseVisualStyleBackColor = false;
            this.chkCrb.Visible = false;
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel12.Location = new System.Drawing.Point(9, 242);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(63, 14);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 16;
            this.neuLabel12.Text = "查  体：";
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel11.Location = new System.Drawing.Point(9, 191);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(56, 14);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 8;
            this.neuLabel11.Text = "过敏史:";
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel10.Location = new System.Drawing.Point(9, 132);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(56, 14);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 6;
            this.neuLabel10.Text = "既往史:";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel9.Location = new System.Drawing.Point(12, 75);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(56, 14);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 4;
            this.neuLabel9.Text = "现病史:";
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel8.Location = new System.Drawing.Point(14, 18);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(49, 14);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 2;
            this.neuLabel8.Text = "主 诉:";
            // 
            // neuGroupBox4
            // 
            this.neuGroupBox4.Controls.Add(this.trvblmb);
            this.neuGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox4.Location = new System.Drawing.Point(3, 72);
            this.neuGroupBox4.Name = "neuGroupBox4";
            this.neuGroupBox4.Size = new System.Drawing.Size(171, 491);
            this.neuGroupBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox4.TabIndex = 5;
            this.neuGroupBox4.TabStop = false;
            this.neuGroupBox4.Text = "病历模板";
            // 
            // trvblmb
            // 
            this.trvblmb.AllowDrop = true;
            this.trvblmb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvblmb.HideSelection = false;
            this.trvblmb.ImageIndex = 0;
            this.trvblmb.ImageList = this.imageList2;
            this.trvblmb.Location = new System.Drawing.Point(3, 17);
            this.trvblmb.Name = "trvblmb";
            this.trvblmb.SelectedImageIndex = 0;
            this.trvblmb.Size = new System.Drawing.Size(165, 471);
            this.trvblmb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.trvblmb.TabIndex = 0;
            this.trvblmb.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvblmb_NodeMouseDoubleClick);
            this.trvblmb.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvblmb_DragDrop);
            this.trvblmb.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvblmb_DragEnter);
            this.trvblmb.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvblmb_ItemDrag);
            this.trvblmb.DragOver += new System.Windows.Forms.DragEventHandler(this.trvblmb_DragOver);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblmzh);
            this.groupBox1.Controls.Add(this.lblage);
            this.groupBox1.Controls.Add(this.lbllb);
            this.groupBox1.Controls.Add(this.lblsex);
            this.groupBox1.Controls.Add(this.lblks);
            this.groupBox1.Controls.Add(this.lblname);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(3, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(670, 66);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息";
            // 
            // lblmzh
            // 
            this.lblmzh.AutoSize = true;
            this.lblmzh.Location = new System.Drawing.Point(390, 47);
            this.lblmzh.Name = "lblmzh";
            this.lblmzh.Size = new System.Drawing.Size(0, 12);
            this.lblmzh.TabIndex = 11;
            // 
            // lblage
            // 
            this.lblage.AutoSize = true;
            this.lblage.Location = new System.Drawing.Point(390, 19);
            this.lblage.Name = "lblage";
            this.lblage.Size = new System.Drawing.Size(0, 12);
            this.lblage.TabIndex = 10;
            // 
            // lbllb
            // 
            this.lbllb.AutoSize = true;
            this.lbllb.Location = new System.Drawing.Point(223, 46);
            this.lbllb.Name = "lbllb";
            this.lbllb.Size = new System.Drawing.Size(0, 12);
            this.lbllb.TabIndex = 9;
            // 
            // lblsex
            // 
            this.lblsex.AutoSize = true;
            this.lblsex.Location = new System.Drawing.Point(224, 18);
            this.lblsex.Name = "lblsex";
            this.lblsex.Size = new System.Drawing.Size(0, 12);
            this.lblsex.TabIndex = 8;
            // 
            // lblks
            // 
            this.lblks.AutoSize = true;
            this.lblks.Location = new System.Drawing.Point(59, 46);
            this.lblks.Name = "lblks";
            this.lblks.Size = new System.Drawing.Size(0, 12);
            this.lblks.TabIndex = 7;
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Location = new System.Drawing.Point(59, 18);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(0, 12);
            this.lblname.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(337, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "年  龄:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(180, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "类别:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(181, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "性别:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "科室:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(337, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "门诊号:";
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Controls.Add(this.tabPage2);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(185, 592);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 6;
            this.neuTabControl1.SelectedIndexChanged += new System.EventHandler(this.neuTabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.neuGroupBox4);
            this.tabPage1.Controls.Add(this.grmb);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(177, 566);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "病历模板";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grmb
            // 
            this.grmb.Controls.Add(this.btNew);
            this.grmb.Controls.Add(this.chkIsPerson);
            this.grmb.Controls.Add(this.btnSave);
            this.grmb.Dock = System.Windows.Forms.DockStyle.Top;
            this.grmb.Location = new System.Drawing.Point(3, 3);
            this.grmb.Name = "grmb";
            this.grmb.Size = new System.Drawing.Size(171, 69);
            this.grmb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grmb.TabIndex = 7;
            this.grmb.TabStop = false;
            this.grmb.Text = "操作";
            // 
            // btNew
            // 
            this.btNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btNew.Location = new System.Drawing.Point(2, 17);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(81, 24);
            this.btNew.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btNew.TabIndex = 3;
            this.btNew.Text = "新建模板(&N)";
            this.btNew.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btNew.UseVisualStyleBackColor = true;
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // chkIsPerson
            // 
            this.chkIsPerson.AutoSize = true;
            this.chkIsPerson.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIsPerson.ForeColor = System.Drawing.Color.Red;
            this.chkIsPerson.Location = new System.Drawing.Point(7, 47);
            this.chkIsPerson.Name = "chkIsPerson";
            this.chkIsPerson.Size = new System.Drawing.Size(143, 17);
            this.chkIsPerson.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkIsPerson.TabIndex = 2;
            this.chkIsPerson.Text = "是否保存为科室模板";
            this.chkIsPerson.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(85, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 24);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "存模板(&M)";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.neuGroupBox2);
            this.tabPage2.Controls.Add(this.grbl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(177, 566);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "历史病历";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grbl
            // 
            this.grbl.Controls.Add(this.btnNewbl);
            this.grbl.Controls.Add(this.btnprint);
            this.grbl.Controls.Add(this.btnSavebl);
            this.grbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbl.Location = new System.Drawing.Point(3, 3);
            this.grbl.Name = "grbl";
            this.grbl.Size = new System.Drawing.Size(171, 69);
            this.grbl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grbl.TabIndex = 8;
            this.grbl.TabStop = false;
            this.grbl.Text = "操作";
            // 
            // btnNewbl
            // 
            this.btnNewbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewbl.Location = new System.Drawing.Point(8, 11);
            this.btnNewbl.Name = "btnNewbl";
            this.btnNewbl.Size = new System.Drawing.Size(79, 27);
            this.btnNewbl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnNewbl.TabIndex = 2;
            this.btnNewbl.Text = "新建(&W)";
            this.btnNewbl.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnNewbl.UseVisualStyleBackColor = true;
            this.btnNewbl.Click += new System.EventHandler(this.btnNewbl_Click);
            // 
            // btnprint
            // 
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprint.Location = new System.Drawing.Point(8, 39);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(79, 27);
            this.btnprint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnprint.TabIndex = 1;
            this.btnprint.Text = "打  印(&P)";
            this.btnprint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnprint.UseVisualStyleBackColor = true;
            this.btnprint.Visible = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnSavebl
            // 
            this.btnSavebl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSavebl.Location = new System.Drawing.Point(89, 11);
            this.btnSavebl.Name = "btnSavebl";
            this.btnSavebl.Size = new System.Drawing.Size(79, 27);
            this.btnSavebl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSavebl.TabIndex = 0;
            this.btnSavebl.Text = "保存(&S)";
            this.btnSavebl.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSavebl.UseVisualStyleBackColor = true;
            this.btnSavebl.Click += new System.EventHandler(this.btnSavebl_Click);
            // 
            // cMenu
            // 
            this.cMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            this.cMenu.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "修改名称";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "删除模板";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // neuPanel1
            // 
            this.neuPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuPanel1.Controls.Add(this.ucUserText1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(178, 592);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 6;
            // 
            // ucUserText1
            // 
            this.ucUserText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserText1.GroupInfo = null;
            this.ucUserText1.Location = new System.Drawing.Point(0, 0);
            this.ucUserText1.Name = "ucUserText1";
            this.ucUserText1.Size = new System.Drawing.Size(174, 588);
            this.ucUserText1.TabIndex = 0;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.BackColor = System.Drawing.SystemColors.Desktop;
            this.neuSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuSplitter1.Location = new System.Drawing.Point(178, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 592);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 7;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuTabControl1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.neuPanel2.Location = new System.Drawing.Point(856, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(185, 592);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 9;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuLabel1);
            this.neuPanel3.Controls.Add(this.groupBox1);
            this.neuPanel3.Controls.Add(this.neuGroupBox3);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(178, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(678, 592);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 10;
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.BackColor = System.Drawing.SystemColors.Desktop;
            this.neuSplitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuSplitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.neuSplitter2.Location = new System.Drawing.Point(853, 0);
            this.neuSplitter2.Name = "neuSplitter2";
            this.neuSplitter2.Size = new System.Drawing.Size(3, 592);
            this.neuSplitter2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter2.TabIndex = 11;
            this.neuSplitter2.TabStop = false;
            // 
            // ucPatientCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.neuSplitter2);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPatientCase";
            this.Size = new System.Drawing.Size(1041, 592);
            this.Load += new System.EventHandler(this.ucPatientCase_Load);
            this.Enter += new System.EventHandler(this.ucPatientCase_Enter);
            this.Leave += new System.EventHandler(this.ucPatientCase_Leave);
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.neuGroupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grmb.ResumeLayout(false);
            this.grmb.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.grbl.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuTreeView trvlsbl;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkMain;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkOld;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkGm;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkCrb;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox4;
        private FS.FrameWork.WinForms.Controls.NeuTreeView trvblmb;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.Label lblmzh;
        private System.Windows.Forms.Label lblage;
        private System.Windows.Forms.Label lbllb;
        private System.Windows.Forms.Label lblsex;
        private System.Windows.Forms.Label lblks;
        private FS.FrameWork.WinForms.Controls.NeuContexMenu cMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grmb;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtMemo;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkIsPerson;
        private FS.FrameWork.WinForms.Controls.NeuButton btNew;
        private System.Windows.Forms.MenuItem menuItem2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grbl;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSavebl;
        private FS.FrameWork.WinForms.Controls.NeuButton btnprint;
        private System.Windows.Forms.ImageList imageList2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.HISFC.Components.Common.Controls.ucUserText ucUserText1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter2;
        private FS.HISFC.Components.Common.Controls.ucDiagnose ucDiagnose1;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtDiagnose;
        private FS.FrameWork.WinForms.Controls.NeuButton btnNewbl;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtDiagnose3;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtDiagnose2;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtMain;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtGms;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtOld;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtNow;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtCheck;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkNow;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkGms;

        
         
    }
}
