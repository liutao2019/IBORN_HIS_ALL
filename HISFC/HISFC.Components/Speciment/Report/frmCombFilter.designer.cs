namespace FS.HISFC.Components.Speciment.Report
{
    partial class frmCombFilter
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.grbCurrentCol = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbValue2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbValue1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbLj1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbLj2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtnOr = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnAnd = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.btnClear = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1.SuspendLayout();
            this.grbCurrentCol.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(11, 7);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "显示行：";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(318, 26);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // grbCurrentCol
            // 
            this.grbCurrentCol.Controls.Add(this.btnClear);
            this.grbCurrentCol.Controls.Add(this.cmbValue2);
            this.grbCurrentCol.Controls.Add(this.cmbValue1);
            this.grbCurrentCol.Controls.Add(this.cmbLj1);
            this.grbCurrentCol.Controls.Add(this.cmbLj2);
            this.grbCurrentCol.Controls.Add(this.btnCancel);
            this.grbCurrentCol.Controls.Add(this.btnOK);
            this.grbCurrentCol.Controls.Add(this.neuLabel3);
            this.grbCurrentCol.Controls.Add(this.neuLabel2);
            this.grbCurrentCol.Controls.Add(this.rbtnOr);
            this.grbCurrentCol.Controls.Add(this.rbtnAnd);
            this.grbCurrentCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbCurrentCol.Location = new System.Drawing.Point(0, 26);
            this.grbCurrentCol.Name = "grbCurrentCol";
            this.grbCurrentCol.Size = new System.Drawing.Size(318, 178);
            this.grbCurrentCol.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grbCurrentCol.TabIndex = 3;
            this.grbCurrentCol.TabStop = false;
            this.grbCurrentCol.Text = "当前列名";
            // 
            // cmbValue2
            // 
            //this.cmbValue2.A = false;
            this.cmbValue2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbValue2.FormattingEnabled = true;
            this.cmbValue2.IsFlat = true;
            this.cmbValue2.IsLike = true;
            this.cmbValue2.Location = new System.Drawing.Point(135, 74);
            this.cmbValue2.Name = "cmbValue2";
            this.cmbValue2.PopForm = null;
            this.cmbValue2.ShowCustomerList = false;
            this.cmbValue2.ShowID = false;
            this.cmbValue2.Size = new System.Drawing.Size(159, 20);
            this.cmbValue2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbValue2.TabIndex = 15;
            this.cmbValue2.Tag = "";
            this.cmbValue2.ToolBarUse = false;
            // 
            // cmbValue1
            // 
            //this.cmbValue1.A = false;
            this.cmbValue1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbValue1.FormattingEnabled = true;
            this.cmbValue1.IsFlat = true;
            this.cmbValue1.IsLike = true;
            this.cmbValue1.Location = new System.Drawing.Point(135, 21);
            this.cmbValue1.Name = "cmbValue1";
            this.cmbValue1.PopForm = null;
            this.cmbValue1.ShowCustomerList = false;
            this.cmbValue1.ShowID = false;
            this.cmbValue1.Size = new System.Drawing.Size(159, 20);
            this.cmbValue1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbValue1.TabIndex = 14;
            this.cmbValue1.Tag = "";
            this.cmbValue1.ToolBarUse = false;
            // 
            // cmbLj1
            // 
            //this.cmbLj1.A = false;
            this.cmbLj1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLj1.FormattingEnabled = true;
            this.cmbLj1.IsFlat = true;
            this.cmbLj1.IsLike = true;
            this.cmbLj1.Location = new System.Drawing.Point(28, 21);
            this.cmbLj1.Name = "cmbLj1";
            this.cmbLj1.PopForm = null;
            this.cmbLj1.ShowCustomerList = false;
            this.cmbLj1.ShowID = false;
            this.cmbLj1.Size = new System.Drawing.Size(88, 20);
            this.cmbLj1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbLj1.TabIndex = 13;
            this.cmbLj1.Tag = "";
            this.cmbLj1.ToolBarUse = false;
            // 
            // cmbLj2
            // 
            //this.cmbLj2.A = false;
            this.cmbLj2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLj2.FormattingEnabled = true;
            this.cmbLj2.IsFlat = true;
            this.cmbLj2.IsLike = true;
            this.cmbLj2.Location = new System.Drawing.Point(28, 74);
            this.cmbLj2.Name = "cmbLj2";
            this.cmbLj2.PopForm = null;
            this.cmbLj2.ShowCustomerList = false;
            this.cmbLj2.ShowID = false;
            this.cmbLj2.Size = new System.Drawing.Size(88, 20);
            this.cmbLj2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbLj2.TabIndex = 12;
            this.cmbLj2.Tag = "";
            this.cmbLj2.ToolBarUse = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(219, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(132, 143);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(30, 126);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(131, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 3;
            this.neuLabel3.Text = "用 * 代表任意多个字符";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(30, 108);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(107, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "用 ? 代表单个字符";
            // 
            // rbtnOr
            // 
            this.rbtnOr.AutoSize = true;
            this.rbtnOr.Location = new System.Drawing.Point(135, 52);
            this.rbtnOr.Name = "rbtnOr";
            this.rbtnOr.Size = new System.Drawing.Size(53, 16);
            this.rbtnOr.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnOr.TabIndex = 1;
            this.rbtnOr.Text = "或(&O)";
            this.rbtnOr.UseVisualStyleBackColor = true;
            this.rbtnOr.CheckedChanged += new System.EventHandler(this.rbtnOr_CheckedChanged);
            // 
            // rbtnAnd
            // 
            this.rbtnAnd.AutoSize = true;
            this.rbtnAnd.Checked = true;
            this.rbtnAnd.Location = new System.Drawing.Point(63, 52);
            this.rbtnAnd.Name = "rbtnAnd";
            this.rbtnAnd.Size = new System.Drawing.Size(53, 16);
            this.rbtnAnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnAnd.TabIndex = 0;
            this.rbtnAnd.TabStop = true;
            this.rbtnAnd.Text = "与(&A)";
            this.rbtnAnd.UseVisualStyleBackColor = true;
            this.rbtnAnd.CheckedChanged += new System.EventHandler(this.rbtnAnd_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(41, 143);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "取消过滤";
            this.btnClear.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmCombFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 204);
            this.Controls.Add(this.grbCurrentCol);
            this.Controls.Add(this.neuPanel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCombFilter";
            this.Text = "自定义自动筛选方式";
            this.Load += new System.EventHandler(this.frmCombFilter_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.grbCurrentCol.ResumeLayout(false);
            this.grbCurrentCol.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grbCurrentCol;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnOr;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnAnd;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbLj2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbValue2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbValue1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbLj1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnClear;
    }
}