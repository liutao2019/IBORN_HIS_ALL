namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    partial class ucDocDiagnoseInput
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnDel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnAdd = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ucDiagNoseInput1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucDiagNoseInput();
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuGroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.btnDel);
            this.neuGroupBox1.Controls.Add(this.btnAdd);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(711, 46);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保 存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(271, 17);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnDel.TabIndex = 0;
            this.btnDel.Text = "删 除";
            this.btnDel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Visible = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(104, 17);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "增 加";
            this.btnAdd.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ucDiagNoseInput1
            // 
            this.ucDiagNoseInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDiagNoseInput1.IsCas = true;
            this.ucDiagNoseInput1.IsDoubt = true;
            this.ucDiagNoseInput1.IsUseDeptICD = false;
            this.ucDiagNoseInput1.Location = new System.Drawing.Point(0, 0);
            this.ucDiagNoseInput1.Name = "ucDiagNoseInput1";
            this.ucDiagNoseInput1.Size = new System.Drawing.Size(711, 450);
            this.ucDiagNoseInput1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.ucDiagNoseInput1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 450);
            this.panel1.TabIndex = 2;
            // 
            // ucDocDiagnoseInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucDocDiagnoseInput";
            this.Size = new System.Drawing.Size(711, 496);
            this.Load += new System.EventHandler(this.ucDocDiagNoseInput_Load);
            this.neuGroupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuButton btnDel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnAdd;
        private ucDiagNoseInput ucDiagNoseInput1;
        private System.Windows.Forms.Panel panel1;
    }
}
