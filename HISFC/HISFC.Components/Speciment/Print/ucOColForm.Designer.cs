namespace FS.HISFC.Components.Speciment.Print
{
    partial class ucOColForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("未取样本");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("已取消");
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvApply = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOperDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDiagnose = new System.Windows.Forms.Label();
            this.lblMainDoc = new System.Windows.Forms.Label();
            this.lblOperAdd = new System.Windows.Forms.Label();
            this.lblOperTime = new System.Windows.Forms.Label();
            this.lblInHosNum = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucBaseControl1 = new FS.FrameWork.WinForms.Controls.ucBaseControl();
            this.ucOperCard1 = new FS.HISFC.Components.Speciment.Print.ucOperCard();
            this.neuPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.splitContainer1);
            this.neuPanel1.Controls.Add(this.ucBaseControl1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1200, 850);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvApply);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 850);
            this.splitContainer1.SplitterDistance = 297;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 70;
            // 
            // tvApply
            // 
            this.tvApply.CheckBoxes = true;
            this.tvApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvApply.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvApply.HideSelection = false;
            this.tvApply.Location = new System.Drawing.Point(0, 171);
            this.tvApply.Margin = new System.Windows.Forms.Padding(4);
            this.tvApply.Name = "tvApply";
            treeNode1.Name = "tnNotGet";
            treeNode1.Tag = "0";
            treeNode1.Text = "未取样本";
            treeNode2.Name = "节点0";
            treeNode2.Text = "已取消";
            this.tvApply.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.tvApply.Size = new System.Drawing.Size(295, 677);
            this.tvApply.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvApply.TabIndex = 48;
            this.tvApply.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvApply_AfterSelect);
            this.tvApply.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvApply_NodeMouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbOperDept);
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.neuLabel2);
            this.panel1.Controls.Add(this.dtpStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 171);
            this.panel1.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 44;
            this.label2.Text = "手术科室:";
            // 
            // cmbOperDept
            // 
            //this.cmbOperDept.A = false;
            this.cmbOperDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOperDept.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOperDept.FormattingEnabled = true;
            this.cmbOperDept.IsFlat = true;
            this.cmbOperDept.IsLike = true;
            this.cmbOperDept.Location = new System.Drawing.Point(95, 13);
            this.cmbOperDept.Margin = new System.Windows.Forms.Padding(5);
            this.cmbOperDept.Name = "cmbOperDept";
            this.cmbOperDept.PopForm = null;
            this.cmbOperDept.ShowCustomerList = false;
            this.cmbOperDept.ShowID = false;
            this.cmbOperDept.Size = new System.Drawing.Size(204, 24);
            this.cmbOperDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbOperDept.TabIndex = 45;
            this.cmbOperDept.Tag = "";
            this.cmbOperDept.ToolBarUse = false;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEnd.Location = new System.Drawing.Point(114, 120);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(183, 26);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 4;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(4, 69);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(88, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "手术时间段";
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dtpStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStart.Location = new System.Drawing.Point(114, 59);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(183, 26);
            this.dtpStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStart.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(896, 848);
            this.tabControl1.TabIndex = 70;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.neuPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(888, 819);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "执行单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // neuPanel2
            // 
            this.neuPanel2.AutoScroll = true;
            this.neuPanel2.Controls.Add(this.groupBox2);
            this.neuPanel2.Location = new System.Drawing.Point(7, 4);
            this.neuPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(874, 1346);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 69;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDiagnose);
            this.groupBox2.Controls.Add(this.lblMainDoc);
            this.groupBox2.Controls.Add(this.lblOperAdd);
            this.groupBox2.Controls.Add(this.lblOperTime);
            this.groupBox2.Controls.Add(this.lblInHosNum);
            this.groupBox2.Controls.Add(this.lblName);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.label34);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(874, 125);
            this.groupBox2.TabIndex = 88;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "基本信息";
            // 
            // lblDiagnose
            // 
            this.lblDiagnose.AutoSize = true;
            this.lblDiagnose.Location = new System.Drawing.Point(561, 30);
            this.lblDiagnose.Name = "lblDiagnose";
            this.lblDiagnose.Size = new System.Drawing.Size(64, 16);
            this.lblDiagnose.TabIndex = 103;
            this.lblDiagnose.Text = "label22";
            // 
            // lblMainDoc
            // 
            this.lblMainDoc.AutoSize = true;
            this.lblMainDoc.Location = new System.Drawing.Point(125, 60);
            this.lblMainDoc.Name = "lblMainDoc";
            this.lblMainDoc.Size = new System.Drawing.Size(64, 16);
            this.lblMainDoc.TabIndex = 102;
            this.lblMainDoc.Text = "label22";
            // 
            // lblOperAdd
            // 
            this.lblOperAdd.AutoSize = true;
            this.lblOperAdd.Location = new System.Drawing.Point(317, 60);
            this.lblOperAdd.Name = "lblOperAdd";
            this.lblOperAdd.Size = new System.Drawing.Size(64, 16);
            this.lblOperAdd.TabIndex = 101;
            this.lblOperAdd.Text = "label22";
            // 
            // lblOperTime
            // 
            this.lblOperTime.AutoSize = true;
            this.lblOperTime.Location = new System.Drawing.Point(584, 60);
            this.lblOperTime.Name = "lblOperTime";
            this.lblOperTime.Size = new System.Drawing.Size(64, 16);
            this.lblOperTime.TabIndex = 100;
            this.lblOperTime.Text = "label22";
            // 
            // lblInHosNum
            // 
            this.lblInHosNum.AutoSize = true;
            this.lblInHosNum.Location = new System.Drawing.Point(325, 29);
            this.lblInHosNum.Name = "lblInHosNum";
            this.lblInHosNum.Size = new System.Drawing.Size(64, 16);
            this.lblInHosNum.TabIndex = 99;
            this.lblInHosNum.Text = "label22";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(118, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 16);
            this.lblName.TabIndex = 98;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(482, 30);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 16);
            this.label17.TabIndex = 97;
            this.label17.Text = "诊断：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(482, 60);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 16);
            this.label24.TabIndex = 77;
            this.label24.Text = "手术时间：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(23, 60);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 16);
            this.label19.TabIndex = 40;
            this.label19.Text = "主刀医生：";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(211, 60);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(72, 16);
            this.label32.TabIndex = 24;
            this.label32.Text = "手术室：";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(23, 29);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(88, 16);
            this.label34.TabIndex = 21;
            this.label34.Text = "病人姓名：";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(214, 30);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(104, 16);
            this.label35.TabIndex = 19;
            this.label35.Text = "住院流水号：";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(18, 94);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(552, 16);
            this.label29.TabIndex = 59;
            this.label29.Text = "说明：肿物(T)  癌旁(P)  癌栓(E)  正常(N) 子瘤(S)  淋巴结(L)  转移(M)";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucOperCard1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(888, 819);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "标签格式预览";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucBaseControl1
            // 
            this.ucBaseControl1.AutoScroll = true;
            this.ucBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseControl1.IsPrint = false;
            this.ucBaseControl1.Location = new System.Drawing.Point(0, 0);
            this.ucBaseControl1.Margin = new System.Windows.Forms.Padding(4);
            this.ucBaseControl1.Name = "ucBaseControl1";
            this.ucBaseControl1.Size = new System.Drawing.Size(1200, 850);
            this.ucBaseControl1.TabIndex = 67;
            // 
            // ucOperCard1
            // 
            this.ucOperCard1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucOperCard1.Location = new System.Drawing.Point(0, 0);
            this.ucOperCard1.Margin = new System.Windows.Forms.Padding(4);
            this.ucOperCard1.Name = "ucOperCard1";
            this.ucOperCard1.Size = new System.Drawing.Size(198, 106);
            this.ucOperCard1.TabIndex = 0;
            // 
            // ucOColForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.neuPanel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucOColForm";
            this.Size = new System.Drawing.Size(1200, 850);
            this.Load += new System.EventHandler(this.ucOColForm_Load);
            this.neuPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.ucBaseControl ucBaseControl1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOperDept;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStart;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvApply;
        private System.Windows.Forms.Label lblInHosNum;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDiagnose;
        private System.Windows.Forms.Label lblMainDoc;
        private System.Windows.Forms.Label lblOperAdd;
        private System.Windows.Forms.Label lblOperTime;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label29;
        private ucOperCard ucOperCard1;


    }
}
