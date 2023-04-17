namespace FS.HISFC.Components.Account.Forms
{
    partial class frmMedicalPackage
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
            this.plMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbValid = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.rbMemo = new System.Windows.Forms.RichTextBox();
            this.tbMony = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.plBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbTip = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plBottomLeft = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plBottomRight = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.plMain.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plBottomRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMain
            // 
            this.plMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plMain.Controls.Add(this.plBottom);
            this.plMain.Controls.Add(this.cmbValid);
            this.plMain.Controls.Add(this.label6);
            this.plMain.Controls.Add(this.rbMemo);
            this.plMain.Controls.Add(this.tbMony);
            this.plMain.Controls.Add(this.tbName);
            this.plMain.Controls.Add(this.label5);
            this.plMain.Controls.Add(this.label4);
            this.plMain.Controls.Add(this.label1);
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plMain.Location = new System.Drawing.Point(0, 0);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(592, 187);
            this.plMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plMain.TabIndex = 28;
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
            this.cmbValid.Location = new System.Drawing.Point(519, 10);
            this.cmbValid.Name = "cmbValid";
            this.cmbValid.ShowCustomerList = false;
            this.cmbValid.ShowID = false;
            this.cmbValid.Size = new System.Drawing.Size(54, 27);
            this.cmbValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbValid.TabIndex = 41;
            this.cmbValid.Tag = "";
            this.cmbValid.ToolBarUse = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(478, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 19);
            this.label6.TabIndex = 40;
            this.label6.Text = "状态:";
            // 
            // rbMemo
            // 
            this.rbMemo.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.rbMemo.Location = new System.Drawing.Point(89, 64);
            this.rbMemo.Name = "rbMemo";
            this.rbMemo.Size = new System.Drawing.Size(484, 50);
            this.rbMemo.TabIndex = 39;
            this.rbMemo.Text = "";
            // 
            // tbMony
            // 
            this.tbMony.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tbMony.IsEnter2Tab = false;
            this.tbMony.Location = new System.Drawing.Point(354, 12);
            this.tbMony.Name = "tbMony";
            this.tbMony.Size = new System.Drawing.Size(108, 25);
            this.tbMony.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbMony.TabIndex = 38;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 64);
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
            this.label4.Location = new System.Drawing.Point(310, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 19);
            this.label4.TabIndex = 29;
            this.label4.Text = "金额：";
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
            // plBottom
            // 
            this.plBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plBottom.Controls.Add(this.lbTip);
            this.plBottom.Controls.Add(this.plBottomLeft);
            this.plBottom.Controls.Add(this.plBottomRight);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plBottom.Location = new System.Drawing.Point(0, 134);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(588, 49);
            this.plBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottom.TabIndex = 42;
            // 
            // lbTip
            // 
            this.lbTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTip.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.lbTip.ForeColor = System.Drawing.Color.Red;
            this.lbTip.Location = new System.Drawing.Point(0, 0);
            this.lbTip.Name = "lbTip";
            this.lbTip.Size = new System.Drawing.Size(383, 45);
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
            this.plBottomLeft.Size = new System.Drawing.Size(383, 45);
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
            this.plBottomRight.Location = new System.Drawing.Point(383, 0);
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmMedicalPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 187);
            this.Controls.Add(this.plMain);
            this.KeyPreview = true;
            this.Name = "frmMedicalPackage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建套包";
            this.plMain.ResumeLayout(false);
            this.plMain.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plBottomRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel plMain;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbValid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rbMemo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbMony;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottom;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTip;
        private FS.FrameWork.WinForms.Controls.NeuLabel plBottomLeft;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottomRight;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
    }
}