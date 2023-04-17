namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    partial class ucGJOutPatientInfoBill
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ndtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ndtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTreeView1 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ucMain2 = new GJLocal.HISFC.Components.OpGuide.RegistionExtend.ucMain();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.ucMain2);
            this.splitContainer1.Size = new System.Drawing.Size(1083, 797);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 0;
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
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.ndtpEnd);
            this.splitContainer2.Panel1.Controls.Add(this.ndtpBegin);
            this.splitContainer2.Panel1.Controls.Add(this.neuLabel2);
            this.splitContainer2.Panel1.Controls.Add(this.neuLabel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.neuTreeView1);
            this.splitContainer2.Size = new System.Drawing.Size(244, 797);
            this.splitContainer2.SplitterDistance = 197;
            this.splitContainer2.TabIndex = 0;
            // 
            // ndtpEnd
            // 
            this.ndtpEnd.CustomFormat = "yyyy-MM-dd";
            this.ndtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpEnd.IsEnter2Tab = false;
            this.ndtpEnd.Location = new System.Drawing.Point(83, 58);
            this.ndtpEnd.Name = "ndtpEnd";
            this.ndtpEnd.Size = new System.Drawing.Size(112, 21);
            this.ndtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpEnd.TabIndex = 3;
            // 
            // ndtpBegin
            // 
            this.ndtpBegin.CustomFormat = "yyyy-MM-dd";
            this.ndtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpBegin.IsEnter2Tab = false;
            this.ndtpBegin.Location = new System.Drawing.Point(83, 22);
            this.ndtpBegin.Name = "ndtpBegin";
            this.ndtpBegin.Size = new System.Drawing.Size(112, 21);
            this.ndtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpBegin.TabIndex = 2;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(13, 62);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "结束时间：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(13, 26);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "开始时间：";
            // 
            // neuTreeView1
            // 
            this.neuTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTreeView1.HideSelection = false;
            this.neuTreeView1.Location = new System.Drawing.Point(0, 0);
            this.neuTreeView1.Name = "neuTreeView1";
            this.neuTreeView1.Size = new System.Drawing.Size(244, 596);
            this.neuTreeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView1.TabIndex = 0;
            this.neuTreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.neuTreeView1_NodeMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "门诊号：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(83, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // ucMain2
            // 
            this.ucMain2.AutoScroll = true;
            this.ucMain2.AutoSize = true;
            this.ucMain2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucMain2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucMain2.Clinc_code = "";
            this.ucMain2.DtReg = "";
            this.ucMain2.IsFillBill = true;
            this.ucMain2.IsFullConvertToHalf = true;
            this.ucMain2.IsPrint = false;
            this.ucMain2.Location = new System.Drawing.Point(-3, 3);
            this.ucMain2.MinimumSize = new System.Drawing.Size(771, 1200);
            this.ucMain2.Name = "ucMain2";
            this.ucMain2.ParentFormToolBar = null;
            this.ucMain2.Size = new System.Drawing.Size(771, 1200);
            this.ucMain2.TabIndex = 0;
            // 
            // ucGJOutPatientInfoBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucGJOutPatientInfoBill";
            this.Size = new System.Drawing.Size(1083, 797);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ucMain ucMain2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpBegin;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}
