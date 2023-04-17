namespace FS.SOC.Local.OutpatientFee.FoSi.Forms
{
    partial class frmBalanceAddUp
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
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmdCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmdOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tbReturnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbRealOwnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPubCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCount = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbOwnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.cmdCancel);
            this.neuPanel4.Controls.Add(this.cmdOK);
            this.neuPanel4.Controls.Add(this.tbReturnCost);
            this.neuPanel4.Controls.Add(this.neuLabel6);
            this.neuPanel4.Controls.Add(this.tbRealOwnCost);
            this.neuPanel4.Controls.Add(this.neuLabel7);
            this.neuPanel4.Controls.Add(this.tbPubCost);
            this.neuPanel4.Controls.Add(this.neuLabel4);
            this.neuPanel4.Controls.Add(this.txtCount);
            this.neuPanel4.Controls.Add(this.tbOwnCost);
            this.neuPanel4.Controls.Add(this.neuLabel1);
            this.neuPanel4.Controls.Add(this.neuLabel2);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(541, 177);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(534, 167);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(50, 23);
            this.cmdCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmdCancel.TabIndex = 11;
            this.cmdCancel.Text = "取消";
            this.cmdCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdOK.Location = new System.Drawing.Point(405, 121);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(80, 30);
            this.cmdOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "确定";
            this.cmdOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // tbReturnCost
            // 
            this.tbReturnCost.BackColor = System.Drawing.Color.White;
            this.tbReturnCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbReturnCost.ForeColor = System.Drawing.Color.Red;
            this.tbReturnCost.IsEnter2Tab = false;
            this.tbReturnCost.Location = new System.Drawing.Point(268, 80);
            this.tbReturnCost.Name = "tbReturnCost";
            this.tbReturnCost.ReadOnly = true;
            this.tbReturnCost.Size = new System.Drawing.Size(86, 29);
            this.tbReturnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbReturnCost.TabIndex = 4;
            this.tbReturnCost.Text = "0.00";
            this.tbReturnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel6.Location = new System.Drawing.Point(194, 87);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(75, 15);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 8;
            this.neuLabel6.Text = "找零金额:";
            // 
            // tbRealOwnCost
            // 
            this.tbRealOwnCost.BackColor = System.Drawing.Color.White;
            this.tbRealOwnCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbRealOwnCost.IsEnter2Tab = false;
            this.tbRealOwnCost.Location = new System.Drawing.Point(84, 80);
            this.tbRealOwnCost.Name = "tbRealOwnCost";
            this.tbRealOwnCost.Size = new System.Drawing.Size(86, 29);
            this.tbRealOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbRealOwnCost.TabIndex = 0;
            this.tbRealOwnCost.Text = "0.00";
            this.tbRealOwnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRealOwnCost.TextChanged += new System.EventHandler(this.tbRealOwnCost_TextChanged);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel7.Location = new System.Drawing.Point(12, 87);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(75, 15);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 6;
            this.neuLabel7.Text = "实收现金:";
            // 
            // tbPubCost
            // 
            this.tbPubCost.BackColor = System.Drawing.Color.White;
            this.tbPubCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbPubCost.IsEnter2Tab = false;
            this.tbPubCost.Location = new System.Drawing.Point(86, 27);
            this.tbPubCost.Name = "tbPubCost";
            this.tbPubCost.ReadOnly = true;
            this.tbPubCost.Size = new System.Drawing.Size(86, 29);
            this.tbPubCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPubCost.TabIndex = 2;
            this.tbPubCost.Text = "0.00";
            this.tbPubCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel4.Location = new System.Drawing.Point(12, 34);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(75, 15);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 4;
            this.neuLabel4.Text = "记账金额:";
            // 
            // txtCount
            // 
            this.txtCount.BackColor = System.Drawing.Color.White;
            this.txtCount.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtCount.IsEnter2Tab = false;
            this.txtCount.Location = new System.Drawing.Point(450, 28);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(66, 29);
            this.txtCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCount.TabIndex = 5;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // tbOwnCost
            // 
            this.tbOwnCost.BackColor = System.Drawing.Color.White;
            this.tbOwnCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbOwnCost.IsEnter2Tab = false;
            this.tbOwnCost.Location = new System.Drawing.Point(268, 27);
            this.tbOwnCost.Name = "tbOwnCost";
            this.tbOwnCost.ReadOnly = true;
            this.tbOwnCost.Size = new System.Drawing.Size(86, 29);
            this.tbOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOwnCost.TabIndex = 3;
            this.tbOwnCost.Text = "0.00";
            this.tbOwnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel1.Location = new System.Drawing.Point(402, 35);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(45, 15);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "张数:";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel2.Location = new System.Drawing.Point(194, 34);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(75, 15);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "自费金额:";
            // 
            // frmBalanceAddUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(541, 177);
            this.Controls.Add(this.neuPanel4);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBalanceAddUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "累计收费";
            this.Load += new System.EventHandler(this.frmBalanceAddUp_Load);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbReturnCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbRealOwnCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbPubCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbOwnCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuButton cmdOK;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCount;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton cmdCancel;
    }
}