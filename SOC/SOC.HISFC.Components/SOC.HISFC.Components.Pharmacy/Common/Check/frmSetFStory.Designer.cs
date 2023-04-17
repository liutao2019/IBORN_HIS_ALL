namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    partial class frmSetFStory
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
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nbtFAsValid = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtFAsAll = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbManagerBatch = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ntxtDays = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ncbDaysValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbGlobalValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuTreeView3 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuTreeView2 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuTreeView1 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.nbtFAsValid);
            this.neuGroupBox2.Controls.Add(this.nbtFAsAll);
            this.neuGroupBox2.Controls.Add(this.nbtCancel);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 418);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(502, 57);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 9;
            this.neuGroupBox2.TabStop = false;
            // 
            // nbtFAsValid
            // 
            this.nbtFAsValid.Location = new System.Drawing.Point(116, 22);
            this.nbtFAsValid.Name = "nbtFAsValid";
            this.nbtFAsValid.Size = new System.Drawing.Size(81, 23);
            this.nbtFAsValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtFAsValid.TabIndex = 20;
            this.nbtFAsValid.Text = "按标志封账";
            this.nbtFAsValid.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtFAsValid.UseVisualStyleBackColor = true;
            // 
            // nbtFAsAll
            // 
            this.nbtFAsAll.Location = new System.Drawing.Point(244, 22);
            this.nbtFAsAll.Name = "nbtFAsAll";
            this.nbtFAsAll.Size = new System.Drawing.Size(119, 23);
            this.nbtFAsAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtFAsAll.TabIndex = 21;
            this.nbtFAsAll.Text = "按范围和标志封账";
            this.nbtFAsAll.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtFAsAll.UseVisualStyleBackColor = true;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(410, 22);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 22;
            this.nbtCancel.Text = "返回";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.ncbManagerBatch);
            this.neuGroupBox1.Controls.Add(this.ntxtDays);
            this.neuGroupBox1.Controls.Add(this.ncbDaysValid);
            this.neuGroupBox1.Controls.Add(this.ncbGlobalValid);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 339);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(502, 79);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 10;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Tag = "";
            this.neuGroupBox1.Text = "标志设置";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(361, 27);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(125, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 21;
            this.neuLabel1.Text = "（此项不选则为一年）";
            // 
            // ncbManagerBatch
            // 
            this.ncbManagerBatch.AutoSize = true;
            this.ncbManagerBatch.Location = new System.Drawing.Point(254, 55);
            this.ncbManagerBatch.Name = "ncbManagerBatch";
            this.ncbManagerBatch.Size = new System.Drawing.Size(144, 16);
            this.ncbManagerBatch.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbManagerBatch.TabIndex = 20;
            this.ncbManagerBatch.Text = "本次封账需要分批盘点";
            this.ncbManagerBatch.UseVisualStyleBackColor = true;
            // 
            // ntxtDays
            // 
            this.ntxtDays.IsEnter2Tab = false;
            this.ntxtDays.Location = new System.Drawing.Point(134, 24);
            this.ntxtDays.Name = "ntxtDays";
            this.ntxtDays.Size = new System.Drawing.Size(39, 21);
            this.ntxtDays.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtDays.TabIndex = 19;
            this.ntxtDays.Text = "90";
            this.ntxtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ncbDaysValid
            // 
            this.ncbDaysValid.AutoSize = true;
            this.ncbDaysValid.Checked = true;
            this.ncbDaysValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbDaysValid.Location = new System.Drawing.Point(31, 26);
            this.ncbDaysValid.Name = "ncbDaysValid";
            this.ncbDaysValid.Size = new System.Drawing.Size(330, 16);
            this.ncbDaysValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbDaysValid.TabIndex = 18;
            this.ncbDaysValid.Text = "库存为零并且    　     天内无出入库操作的不进行封账";
            this.ncbDaysValid.UseVisualStyleBackColor = true;
            // 
            // ncbGlobalValid
            // 
            this.ncbGlobalValid.AutoSize = true;
            this.ncbGlobalValid.Checked = true;
            this.ncbGlobalValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbGlobalValid.Location = new System.Drawing.Point(31, 55);
            this.ncbGlobalValid.Name = "ncbGlobalValid";
            this.ncbGlobalValid.Size = new System.Drawing.Size(204, 16);
            this.ncbGlobalValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbGlobalValid.TabIndex = 16;
            this.ncbGlobalValid.Text = "排除在药品基本信息中停用的药品";
            this.ncbGlobalValid.UseVisualStyleBackColor = true;
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuPanel2);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(502, 339);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 11;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "范围选择";
            // 
            // neuPanel2
            // 
            this.neuPanel2.AutoScroll = true;
            this.neuPanel2.Controls.Add(this.neuTreeView3);
            this.neuPanel2.Controls.Add(this.neuTreeView2);
            this.neuPanel2.Controls.Add(this.neuTreeView1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(3, 17);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(496, 319);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuTreeView3
            // 
            this.neuTreeView3.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuTreeView3.HideSelection = false;
            this.neuTreeView3.Location = new System.Drawing.Point(328, 0);
            this.neuTreeView3.Name = "neuTreeView3";
            this.neuTreeView3.Size = new System.Drawing.Size(164, 319);
            this.neuTreeView3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView3.TabIndex = 2;
            // 
            // neuTreeView2
            // 
            this.neuTreeView2.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuTreeView2.HideSelection = false;
            this.neuTreeView2.Location = new System.Drawing.Point(164, 0);
            this.neuTreeView2.Name = "neuTreeView2";
            this.neuTreeView2.Size = new System.Drawing.Size(164, 319);
            this.neuTreeView2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView2.TabIndex = 1;
            // 
            // neuTreeView1
            // 
            this.neuTreeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuTreeView1.HideSelection = false;
            this.neuTreeView1.Location = new System.Drawing.Point(0, 0);
            this.neuTreeView1.Name = "neuTreeView1";
            this.neuTreeView1.Size = new System.Drawing.Size(164, 319);
            this.neuTreeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView1.TabIndex = 0;
            // 
            // frmSetFStory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 475);
            this.Controls.Add(this.neuGroupBox3);
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.neuGroupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetFStory";
            this.ShowInTaskbar = false;
            this.Text = "封账方式";
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox3.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtFAsAll;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbDaysValid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbGlobalValid;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtDays;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtFAsValid;
        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView2;
        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbManagerBatch;
        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;

    }
}