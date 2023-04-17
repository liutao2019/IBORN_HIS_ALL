namespace HISFC.Components.Package.Forms
{
    partial class frmPackage
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
            this.components = new System.ComponentModel.Container();
            this.plBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbTip = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plBottomLeft = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plBottomRight = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.plMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rbMemo = new System.Windows.Forms.RichTextBox();
            this.tbUserCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbPackageRange = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPackageType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbValid = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.chkComboFlag = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plBottom.SuspendLayout();
            this.plBottomRight.SuspendLayout();
            this.plMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // plBottom
            // 
            this.plBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plBottom.Controls.Add(this.lbTip);
            this.plBottom.Controls.Add(this.plBottomLeft);
            this.plBottom.Controls.Add(this.plBottomRight);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plBottom.Location = new System.Drawing.Point(0, 176);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(592, 49);
            this.plBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottom.TabIndex = 26;
            // 
            // lbTip
            // 
            this.lbTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTip.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.lbTip.ForeColor = System.Drawing.Color.Red;
            this.lbTip.Location = new System.Drawing.Point(0, 0);
            this.lbTip.Name = "lbTip";
            this.lbTip.Size = new System.Drawing.Size(387, 45);
            this.lbTip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTip.TabIndex = 29;
            this.lbTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plBottomLeft
            // 
            this.plBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plBottomLeft.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plBottomLeft.ForeColor = System.Drawing.Color.Red;
            this.plBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.plBottomLeft.Name = "plBottomLeft";
            this.plBottomLeft.Size = new System.Drawing.Size(387, 45);
            this.plBottomLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottomLeft.TabIndex = 28;
            this.plBottomLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plBottomRight
            // 
            this.plBottomRight.Controls.Add(this.btnCancel);
            this.plBottomRight.Controls.Add(this.btnSave);
            this.plBottomRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.plBottomRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plBottomRight.Location = new System.Drawing.Point(387, 0);
            this.plBottomRight.Name = "plBottomRight";
            this.plBottomRight.Size = new System.Drawing.Size(201, 45);
            this.plBottomRight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottomRight.TabIndex = 27;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(108, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // plMain
            // 
            this.plMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plMain.Controls.Add(this.neuLabel1);
            this.plMain.Controls.Add(this.chkComboFlag);
            this.plMain.Controls.Add(this.cmbValid);
            this.plMain.Controls.Add(this.label6);
            this.plMain.Controls.Add(this.rbMemo);
            this.plMain.Controls.Add(this.tbUserCode);
            this.plMain.Controls.Add(this.tbName);
            this.plMain.Controls.Add(this.cmbPackageRange);
            this.plMain.Controls.Add(this.label2);
            this.plMain.Controls.Add(this.cmbPackageType);
            this.plMain.Controls.Add(this.label5);
            this.plMain.Controls.Add(this.label4);
            this.plMain.Controls.Add(this.label3);
            this.plMain.Controls.Add(this.label1);
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plMain.Location = new System.Drawing.Point(0, 0);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(592, 176);
            this.plMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plMain.TabIndex = 27;
            // 
            // rbMemo
            // 
            this.rbMemo.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.rbMemo.Location = new System.Drawing.Point(89, 109);
            this.rbMemo.Name = "rbMemo";
            this.rbMemo.Size = new System.Drawing.Size(484, 50);
            this.rbMemo.TabIndex = 39;
            this.rbMemo.Text = "";
            // 
            // tbUserCode
            // 
            this.tbUserCode.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tbUserCode.IsEnter2Tab = false;
            this.tbUserCode.Location = new System.Drawing.Point(391, 12);
            this.tbUserCode.Name = "tbUserCode";
            this.tbUserCode.Size = new System.Drawing.Size(86, 25);
            this.tbUserCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbUserCode.TabIndex = 38;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(89, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(210, 25);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 37;
            // 
            // cmbPackageRange
            // 
            this.cmbPackageRange.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPackageRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPackageRange.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPackageRange.FormattingEnabled = true;
            this.cmbPackageRange.IsEnter2Tab = false;
            this.cmbPackageRange.IsFlat = false;
            this.cmbPackageRange.IsLike = true;
            this.cmbPackageRange.IsListOnly = false;
            this.cmbPackageRange.IsPopForm = true;
            this.cmbPackageRange.IsShowCustomerList = false;
            this.cmbPackageRange.IsShowID = false;
            this.cmbPackageRange.IsShowIDAndName = false;
            this.cmbPackageRange.Location = new System.Drawing.Point(391, 46);
            this.cmbPackageRange.Name = "cmbPackageRange";
            this.cmbPackageRange.ShowCustomerList = false;
            this.cmbPackageRange.ShowID = false;
            this.cmbPackageRange.Size = new System.Drawing.Size(182, 27);
            this.cmbPackageRange.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPackageRange.TabIndex = 36;
            this.cmbPackageRange.Tag = "";
            this.cmbPackageRange.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(311, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 35;
            this.label2.Text = "套餐范围：";
            // 
            // cmbPackageType
            // 
            this.cmbPackageType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPackageType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPackageType.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPackageType.FormattingEnabled = true;
            this.cmbPackageType.IsEnter2Tab = false;
            this.cmbPackageType.IsFlat = false;
            this.cmbPackageType.IsLike = true;
            this.cmbPackageType.IsListOnly = false;
            this.cmbPackageType.IsPopForm = true;
            this.cmbPackageType.IsShowCustomerList = false;
            this.cmbPackageType.IsShowID = false;
            this.cmbPackageType.IsShowIDAndName = false;
            this.cmbPackageType.Location = new System.Drawing.Point(89, 46);
            this.cmbPackageType.Name = "cmbPackageType";
            this.cmbPackageType.ShowCustomerList = false;
            this.cmbPackageType.ShowID = false;
            this.cmbPackageType.Size = new System.Drawing.Size(210, 27);
            this.cmbPackageType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPackageType.TabIndex = 33;
            this.cmbPackageType.Tag = "";
            this.cmbPackageType.ToolBarUse = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 30;
            this.label5.Text = "套餐备注：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(311, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 29;
            this.label4.Text = "自定义码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(9, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 27;
            this.label3.Text = "套餐类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 26;
            this.label1.Text = "套餐名称：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(479, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 19);
            this.label6.TabIndex = 40;
            this.label6.Text = "状态:";
            // 
            // cmbValid
            // 
            this.cmbValid.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbValid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbValid.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbValid.FormattingEnabled = true;
            this.cmbValid.IsEnter2Tab = false;
            this.cmbValid.IsFlat = false;
            this.cmbValid.IsLike = true;
            this.cmbValid.IsListOnly = false;
            this.cmbValid.IsPopForm = true;
            this.cmbValid.IsShowCustomerList = false;
            this.cmbValid.IsShowID = false;
            this.cmbValid.IsShowIDAndName = false;
            this.cmbValid.Location = new System.Drawing.Point(519, 12);
            this.cmbValid.Name = "cmbValid";
            this.cmbValid.ShowCustomerList = false;
            this.cmbValid.ShowID = false;
            this.cmbValid.Size = new System.Drawing.Size(54, 27);
            this.cmbValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbValid.TabIndex = 41;
            this.cmbValid.Tag = "";
            this.cmbValid.ToolBarUse = false;
            // 
            // chkComboFlag
            // 
            this.chkComboFlag.AutoSize = true;
            this.chkComboFlag.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.chkComboFlag.ForeColor = System.Drawing.Color.Black;
            this.chkComboFlag.Location = new System.Drawing.Point(89, 80);
            this.chkComboFlag.Name = "chkComboFlag";
            this.chkComboFlag.Size = new System.Drawing.Size(80, 23);
            this.chkComboFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkComboFlag.TabIndex = 42;
            this.chkComboFlag.Text = "组合套餐";
            this.chkComboFlag.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Red;
            this.neuLabel1.Location = new System.Drawing.Point(169, 83);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(404, 17);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 43;
            this.neuLabel1.Text = "组合套餐不能被购买，在购买其他套餐时其子套餐包可以组合至其他套餐中";
            // 
            // frmPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 225);
            this.Controls.Add(this.plMain);
            this.Controls.Add(this.plBottom);
            this.KeyPreview = true;
            this.Name = "frmPackage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建套餐";
            this.plBottom.ResumeLayout(false);
            this.plBottomRight.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            this.plMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel plBottom;
        private FS.FrameWork.WinForms.Controls.NeuPanel plMain;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPackageRange;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPackageType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottomRight;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuLabel plBottomLeft;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbUserCode;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        private System.Windows.Forms.RichTextBox rbMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTip;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbValid;
        private System.Windows.Forms.Label label6;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkComboFlag;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;

    }
}