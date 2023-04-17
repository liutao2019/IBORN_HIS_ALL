namespace FS.HISFC.Components.Operation.OutpatientApplication
{
    partial class ucApplication
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvApply = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtCard = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ucUserText1 = new FS.HISFC.Components.Common.Controls.ucUserText();
            this.neuchops = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ucApplicationForm1 = new FS.HISFC.Components.Operation.OutpatientApplication.ucApplicationForm();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvApply);
            this.splitContainer1.Panel1.Controls.Add(this.neuPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.ucApplicationForm1);
            this.splitContainer1.Size = new System.Drawing.Size(893, 595);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvApply
            // 
            this.tvApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvApply.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvApply.HideSelection = false;
            this.tvApply.ImageIndex = 0;
            this.tvApply.ImageList = this.imageList1;
            this.tvApply.Location = new System.Drawing.Point(0, 112);
            this.tvApply.Name = "tvApply";
            this.tvApply.SelectedImageIndex = 0;
            this.tvApply.Size = new System.Drawing.Size(184, 483);
            this.tvApply.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvApply.TabIndex = 1;
            this.tvApply.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvPatientList1_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.MintCream;
            this.neuPanel1.Controls.Add(this.neuchops);
            this.neuPanel1.Controls.Add(this.txtCard);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.neuDateTimePicker2);
            this.neuPanel1.Controls.Add(this.neuDateTimePicker1);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(184, 112);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // txtCard
            // 
            this.txtCard.IsEnter2Tab = false;
            this.txtCard.Location = new System.Drawing.Point(80, 10);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(116, 21);
            this.txtCard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCard.TabIndex = 4;
            this.txtCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCard_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(13, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(44, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 3;
            this.neuLabel1.Text = "病历号";
            // 
            // neuDateTimePicker2
            // 
            this.neuDateTimePicker2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuDateTimePicker2.IsEnter2Tab = false;
            this.neuDateTimePicker2.Location = new System.Drawing.Point(80, 62);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(126, 23);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 2;
            this.neuDateTimePicker2.ValueChanged += new System.EventHandler(this.neuDateTimePicker1_ValueChanged);
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.neuDateTimePicker1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(80, 35);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(126, 23);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 2;
            this.neuDateTimePicker1.ValueChanged += new System.EventHandler(this.neuDateTimePicker1_ValueChanged);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(9, 66);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(70, 14);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "结束时间:";
            this.neuLabel3.Click += new System.EventHandler(this.neuLabel1_Click);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(9, 39);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(70, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "开始时间:";
            this.neuLabel2.Click += new System.EventHandler(this.neuLabel1_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucUserText1);
            this.splitContainer2.Size = new System.Drawing.Size(1040, 595);
            this.splitContainer2.SplitterDistance = 893;
            this.splitContainer2.TabIndex = 1;
            // 
            // ucUserText1
            // 
            this.ucUserText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserText1.GroupInfo = null;
            this.ucUserText1.Location = new System.Drawing.Point(0, 0);
            this.ucUserText1.Name = "ucUserText1";
            this.ucUserText1.Size = new System.Drawing.Size(143, 595);
            this.ucUserText1.TabIndex = 1;
            // 
            // neuchops
            // 
            this.neuchops.AutoSize = true;
            this.neuchops.Location = new System.Drawing.Point(12, 91);
            this.neuchops.Name = "neuchops";
            this.neuchops.Size = new System.Drawing.Size(84, 16);
            this.neuchops.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuchops.TabIndex = 5;
            this.neuchops.Text = "拟手术时间";
            this.neuchops.UseVisualStyleBackColor = true;
            // 
            // ucApplicationForm1
            // 
            this.ucApplicationForm1.BackColor = System.Drawing.Color.MintCream;
            this.ucApplicationForm1.CheckApplyTime = false;
            this.ucApplicationForm1.CheckDate = true;
            this.ucApplicationForm1.CheckEmergency = true;
            this.ucApplicationForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucApplicationForm1.IsFullConvertToHalf = true;
            this.ucApplicationForm1.IsNew = true;
            this.ucApplicationForm1.IsOwnPrivilege = false;
            this.ucApplicationForm1.IsPrint = false;
            this.ucApplicationForm1.Location = new System.Drawing.Point(0, 0);
            this.ucApplicationForm1.Name = "ucApplicationForm1";
            this.ucApplicationForm1.ParentFormToolBar = null;
            this.ucApplicationForm1.Size = new System.Drawing.Size(705, 595);
            this.ucApplicationForm1.TabIndex = 0;
            // 
            // ucApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "ucApplication";
            this.Size = new System.Drawing.Size(1040, 595);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.HISFC.Components.Operation.OutpatientApplication.ucApplicationForm ucApplicationForm1;
        private FS.HISFC.Components.Common.Controls.baseTreeView tvApply;
        private System.Windows.Forms.ImageList imageList1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCard;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private FS.HISFC.Components.Common.Controls.ucUserText ucUserText1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuchops;
    }
}
