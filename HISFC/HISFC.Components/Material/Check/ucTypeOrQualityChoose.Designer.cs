namespace FS.HISFC.Components.Material.Check
{
    partial class ucTypeOrQualityChoose
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
            this.tvObject = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btkAll = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ckValidDrug = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckZeroState = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvObject
            // 
            this.tvObject.CheckBoxes = true;
            this.tvObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObject.HideSelection = false;
            this.tvObject.Location = new System.Drawing.Point(0, 0);
            this.tvObject.Name = "tvObject";
            this.tvObject.Size = new System.Drawing.Size(241, 406);
            this.tvObject.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvObject.TabIndex = 1;
            this.tvObject.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvObject_AfterCheck);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.btkAll);
            this.neuPanel1.Controls.Add(this.btnOK);
            this.neuPanel1.Controls.Add(this.btnCancel);
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 406);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(241, 126);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // btkAll
            // 
            this.btkAll.Location = new System.Drawing.Point(6, 6);
            this.btkAll.Name = "btkAll";
            this.btkAll.Size = new System.Drawing.Size(63, 23);
            this.btkAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btkAll.TabIndex = 2;
            this.btkAll.Text = "全部物资";
            this.btkAll.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btkAll.UseVisualStyleBackColor = true;
            this.btkAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(119, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(179, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ckValidDrug);
            this.neuGroupBox1.Controls.Add(this.ckZeroState);
            this.neuGroupBox1.Location = new System.Drawing.Point(6, 35);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(227, 60);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "设置";
            // 
            // ckValidDrug
            // 
            this.ckValidDrug.AutoSize = true;
            this.ckValidDrug.Checked = true;
            this.ckValidDrug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckValidDrug.ForeColor = System.Drawing.Color.Blue;
            this.ckValidDrug.Location = new System.Drawing.Point(6, 38);
            this.ckValidDrug.Name = "ckValidDrug";
            this.ckValidDrug.Size = new System.Drawing.Size(204, 16);
            this.ckValidDrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckValidDrug.TabIndex = 1;
            this.ckValidDrug.Text = "对停用物资(本库房停用)进行封账";
            this.ckValidDrug.UseVisualStyleBackColor = true;
            // 
            // ckZeroState
            // 
            this.ckZeroState.AutoSize = true;
            this.ckZeroState.Checked = true;
            this.ckZeroState.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckZeroState.ForeColor = System.Drawing.Color.Blue;
            this.ckZeroState.Location = new System.Drawing.Point(6, 17);
            this.ckZeroState.Name = "ckZeroState";
            this.ckZeroState.Size = new System.Drawing.Size(168, 16);
            this.ckZeroState.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckZeroState.TabIndex = 0;
            this.ckZeroState.Text = "对库存为零的物资进行封账";
            this.ckZeroState.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.ForeColor = System.Drawing.Color.Red;
            this.neuLabel1.Location = new System.Drawing.Point(3, 99);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(230, 27);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 5;
            this.neuLabel1.Text = "注意：批量封帐会自动过滤未封帐盘点单中的物资项目！";
            // 
            // ucTypeOrQualityChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvObject);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucTypeOrQualityChoose";
            this.Size = new System.Drawing.Size(241, 532);
            this.neuPanel1.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTreeView tvObject;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckValidDrug;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckZeroState;
        private FS.FrameWork.WinForms.Controls.NeuButton btkAll;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}
