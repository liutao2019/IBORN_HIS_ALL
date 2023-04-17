namespace FS.HISFC.Components.Speciment.Setting
{
    partial class ucContainer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucContainer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imglistSpec = new System.Windows.Forms.ImageList(this.components);
            this.ucBaseControl1 = new FS.FrameWork.WinForms.Controls.ucBaseControl();
            this.grpSpecBoxInfo = new System.Windows.Forms.GroupBox();
            this.dgvBox = new System.Windows.Forms.DataGridView();
            this.grpSpecInfo = new System.Windows.Forms.GroupBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvSpec = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gpBaseInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.chkAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.chkTime = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtOr = new System.Windows.Forms.RadioButton();
            this.rbtAnd = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDisType = new FS.FrameWork.WinForms.Controls.NeuComboBox();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvIceBox = new System.Windows.Forms.TreeView();
            this.grpSpecBoxInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBox)).BeginInit();
            this.grpSpecInfo.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.gpBaseInfo.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglistSpec
            // 
            this.imglistSpec.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistSpec.ImageStream")));
            this.imglistSpec.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistSpec.Images.SetKeyName(0, "group3.ico");
            this.imglistSpec.Images.SetKeyName(1, "group4.ico");
            this.imglistSpec.Images.SetKeyName(2, "group5.ico");
            // 
            // ucBaseControl1
            // 
            this.ucBaseControl1.AutoScroll = true;
            this.ucBaseControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBaseControl1.IsPrint = false;
            this.ucBaseControl1.Location = new System.Drawing.Point(0, 0);
            this.ucBaseControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucBaseControl1.Name = "ucBaseControl1";
            this.ucBaseControl1.Size = new System.Drawing.Size(1033, 797);
            this.ucBaseControl1.TabIndex = 5;
            // 
            // grpSpecBoxInfo
            // 
            this.grpSpecBoxInfo.Controls.Add(this.dgvBox);
            this.grpSpecBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSpecBoxInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpSpecBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.grpSpecBoxInfo.Name = "grpSpecBoxInfo";
            this.grpSpecBoxInfo.Size = new System.Drawing.Size(831, 305);
            this.grpSpecBoxInfo.TabIndex = 6;
            this.grpSpecBoxInfo.TabStop = false;
            this.grpSpecBoxInfo.Text = "标本盒位置信息";
            // 
            // dgvBox
            // 
            this.dgvBox.AllowDrop = true;
            this.dgvBox.AllowUserToAddRows = false;
            this.dgvBox.AllowUserToDeleteRows = false;
            this.dgvBox.AllowUserToResizeColumns = false;
            this.dgvBox.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBox.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBox.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvBox.Location = new System.Drawing.Point(3, 22);
            this.dgvBox.Name = "dgvBox";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBox.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvBox.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvBox.RowTemplate.Height = 23;
            this.dgvBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBox.Size = new System.Drawing.Size(825, 280);
            this.dgvBox.TabIndex = 7;
            this.dgvBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvBox_MouseDown);
            this.dgvBox.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBox_CellDoubleClick);
            this.dgvBox.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBox_CellClick);
            this.dgvBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvBox_DragEnter);
            this.dgvBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvBox_DragDrop);
            // 
            // grpSpecInfo
            // 
            this.grpSpecInfo.Controls.Add(this.splitContainer3);
            this.grpSpecInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSpecInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpSpecInfo.Location = new System.Drawing.Point(0, 0);
            this.grpSpecInfo.Name = "grpSpecInfo";
            this.grpSpecInfo.Size = new System.Drawing.Size(831, 384);
            this.grpSpecInfo.TabIndex = 7;
            this.grpSpecInfo.TabStop = false;
            this.grpSpecInfo.Text = "标本位置信息";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 22);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvSpec);
            this.splitContainer3.Panel2MinSize = 0;
            this.splitContainer3.Size = new System.Drawing.Size(825, 359);
            this.splitContainer3.SplitterDistance = 790;
            this.splitContainer3.TabIndex = 0;
            // 
            // dgvSpec
            // 
            this.dgvSpec.AllowDrop = true;
            this.dgvSpec.AllowUserToAddRows = false;
            this.dgvSpec.AllowUserToDeleteRows = false;
            this.dgvSpec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSpec.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvSpec.Location = new System.Drawing.Point(0, 0);
            this.dgvSpec.Name = "dgvSpec";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSpec.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSpec.RowTemplate.Height = 23;
            this.dgvSpec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSpec.Size = new System.Drawing.Size(790, 359);
            this.dgvSpec.TabIndex = 2;
            this.dgvSpec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvSpec_MouseDown);
            this.dgvSpec.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellDoubleClick);
            this.dgvSpec.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvSpec_DragEnter);
            this.dgvSpec.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvSpec_DragDrop);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpSpecInfo);
            this.splitContainer1.Size = new System.Drawing.Size(831, 797);
            this.splitContainer1.SplitterDistance = 409;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gpBaseInfo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grpSpecBoxInfo);
            this.splitContainer2.Size = new System.Drawing.Size(831, 409);
            this.splitContainer2.SplitterDistance = 100;
            this.splitContainer2.TabIndex = 4;
            // 
            // gpBaseInfo
            // 
            this.gpBaseInfo.Controls.Add(this.chkAll);
            this.gpBaseInfo.Controls.Add(this.label5);
            this.gpBaseInfo.Controls.Add(this.label2);
            this.gpBaseInfo.Controls.Add(this.dtpEnd);
            this.gpBaseInfo.Controls.Add(this.chkTime);
            this.gpBaseInfo.Controls.Add(this.label4);
            this.gpBaseInfo.Controls.Add(this.dtpStart);
            this.gpBaseInfo.Controls.Add(this.label1);
            this.gpBaseInfo.Controls.Add(this.rbtOr);
            this.gpBaseInfo.Controls.Add(this.rbtAnd);
            this.gpBaseInfo.Controls.Add(this.label3);
            this.gpBaseInfo.Controls.Add(this.cmbDisType);
            this.gpBaseInfo.Controls.Add(this.cmbSpecType);
            this.gpBaseInfo.Controls.Add(this.neuLabel7);
            this.gpBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpBaseInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.gpBaseInfo.Margin = new System.Windows.Forms.Padding(4);
            this.gpBaseInfo.Name = "gpBaseInfo";
            this.gpBaseInfo.Padding = new System.Windows.Forms.Padding(4);
            this.gpBaseInfo.Size = new System.Drawing.Size(831, 100);
            this.gpBaseInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gpBaseInfo.TabIndex = 3;
            this.gpBaseInfo.TabStop = false;
            this.gpBaseInfo.Text = "查询条件";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(24, 55);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(91, 20);
            this.chkAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkAll.TabIndex = 106;
            this.chkAll.Text = "加载全部";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(552, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 16);
            this.label5.TabIndex = 105;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(514, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 104;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(805, 21);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(144, 26);
            this.dtpEnd.TabIndex = 103;
            // 
            // chkTime
            // 
            this.chkTime.AutoSize = true;
            this.chkTime.Location = new System.Drawing.Point(544, 24);
            this.chkTime.Name = "chkTime";
            this.chkTime.Size = new System.Drawing.Size(91, 20);
            this.chkTime.TabIndex = 102;
            this.chkTime.Text = "取材时间";
            this.chkTime.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(783, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 101;
            this.label4.Text = "-";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(643, 21);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(134, 26);
            this.dtpStart.TabIndex = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.SkyBlue;
            this.label1.Location = new System.Drawing.Point(142, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 16);
            this.label1.TabIndex = 98;
            this.label1.Text = "天蓝色背景色表示标本盒为特殊项目或者课题专用";
            // 
            // rbtOr
            // 
            this.rbtOr.AutoSize = true;
            this.rbtOr.Location = new System.Drawing.Point(271, 24);
            this.rbtOr.Name = "rbtOr";
            this.rbtOr.Size = new System.Drawing.Size(42, 20);
            this.rbtOr.TabIndex = 97;
            this.rbtOr.Text = "OR";
            this.rbtOr.UseVisualStyleBackColor = true;
            // 
            // rbtAnd
            // 
            this.rbtAnd.AutoSize = true;
            this.rbtAnd.Checked = true;
            this.rbtAnd.Location = new System.Drawing.Point(215, 24);
            this.rbtAnd.Name = "rbtAnd";
            this.rbtAnd.Size = new System.Drawing.Size(50, 20);
            this.rbtAnd.TabIndex = 96;
            this.rbtAnd.TabStop = true;
            this.rbtAnd.Text = "AND";
            this.rbtAnd.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 95;
            this.label3.Text = "病种:";
            // 
            // cmbDisType
            // 
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.Location = new System.Drawing.Point(75, 22);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.Size = new System.Drawing.Size(134, 24);
            this.cmbDisType.TabIndex = 94;
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(405, 22);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(132, 24);
            this.cmbSpecType.TabIndex = 91;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(320, 26);
            this.neuLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(80, 16);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 90;
            this.neuLabel7.Text = "标本类型:";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer4.Size = new System.Drawing.Size(1033, 797);
            this.splitContainer4.SplitterDistance = 198;
            this.splitContainer4.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvIceBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 797);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "冰箱列表";
            // 
            // tvIceBox
            // 
            this.tvIceBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvIceBox.ImageIndex = 0;
            this.tvIceBox.ImageList = this.imglistSpec;
            this.tvIceBox.Location = new System.Drawing.Point(3, 22);
            this.tvIceBox.Name = "tvIceBox";
            this.tvIceBox.SelectedImageIndex = 0;
            this.tvIceBox.Size = new System.Drawing.Size(192, 772);
            this.tvIceBox.TabIndex = 0;
            // 
            // ucContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.splitContainer4);
            this.Controls.Add(this.ucBaseControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucContainer";
            this.Size = new System.Drawing.Size(1033, 797);
            this.Load += new System.EventHandler(this.ucContainer_Load);
            this.grpSpecBoxInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBox)).EndInit();
            this.grpSpecInfo.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.gpBaseInfo.ResumeLayout(false);
            this.gpBaseInfo.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglistSpec;
        private FS.FrameWork.WinForms.Controls.ucBaseControl ucBaseControl1;
        private System.Windows.Forms.GroupBox grpSpecBoxInfo;
        private System.Windows.Forms.GroupBox grpSpecInfo;
        private System.Windows.Forms.DataGridView dgvSpec;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gpBaseInfo;
        private System.Windows.Forms.Label label3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDisType;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tvIceBox;
        private System.Windows.Forms.RadioButton rbtOr;
        private System.Windows.Forms.RadioButton rbtAnd;
        private System.Windows.Forms.DataGridView dgvBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkAll;
    }
}
