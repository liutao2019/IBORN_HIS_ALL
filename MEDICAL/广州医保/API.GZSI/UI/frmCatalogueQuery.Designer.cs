namespace API.GZSI.UI
{
    partial class frmCatalogueQuery
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
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.neuButton4 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton5 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton6 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton3 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResult
            // 
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(0, 118);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(2);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowTemplate.Height = 27;
            this.dgvResult.Size = new System.Drawing.Size(896, 467);
            this.dgvResult.TabIndex = 8;
            // 
            // neuButton4
            // 
            this.neuButton4.Location = new System.Drawing.Point(486, 63);
            this.neuButton4.Name = "neuButton4";
            this.neuButton4.Size = new System.Drawing.Size(186, 36);
            this.neuButton4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton4.TabIndex = 14;
            this.neuButton4.Tag = "1319";
            this.neuButton4.Text = "[1319]医保目录先自付比例信息查询";
            this.neuButton4.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton4.UseVisualStyleBackColor = true;
            // 
            // neuButton5
            // 
            this.neuButton5.Location = new System.Drawing.Point(231, 63);
            this.neuButton5.Name = "neuButton5";
            this.neuButton5.Size = new System.Drawing.Size(186, 36);
            this.neuButton5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton5.TabIndex = 13;
            this.neuButton5.Tag = "1318";
            this.neuButton5.Text = "[1318]医保目录限价信息查询";
            this.neuButton5.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton5.UseVisualStyleBackColor = true;
            // 
            // neuButton6
            // 
            this.neuButton6.Location = new System.Drawing.Point(12, 63);
            this.neuButton6.Name = "neuButton6";
            this.neuButton6.Size = new System.Drawing.Size(186, 36);
            this.neuButton6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton6.TabIndex = 12;
            this.neuButton6.Tag = "1317";
            this.neuButton6.Text = "[1317]医药机构目录匹配信息查询";
            this.neuButton6.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton6.UseVisualStyleBackColor = true;
            // 
            // neuButton3
            // 
            this.neuButton3.Location = new System.Drawing.Point(486, 12);
            this.neuButton3.Name = "neuButton3";
            this.neuButton3.Size = new System.Drawing.Size(186, 36);
            this.neuButton3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton3.TabIndex = 11;
            this.neuButton3.Tag = "1316";
            this.neuButton3.Text = "[1316]医疗目录与医保目录匹配信息查询";
            this.neuButton3.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton3.UseVisualStyleBackColor = true;
            // 
            // neuButton2
            // 
            this.neuButton2.Location = new System.Drawing.Point(231, 12);
            this.neuButton2.Name = "neuButton2";
            this.neuButton2.Size = new System.Drawing.Size(186, 36);
            this.neuButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton2.TabIndex = 10;
            this.neuButton2.Tag = "1312";
            this.neuButton2.Text = "[1312]医保目录信息查询";
            this.neuButton2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton2.UseVisualStyleBackColor = true;
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(12, 12);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(186, 36);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 9;
            this.neuButton1.Tag = "1304";
            this.neuButton1.Text = "[1304]民族药品目录查询";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            // 
            // frmCatalogueQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 585);
            this.Controls.Add(this.neuButton4);
            this.Controls.Add(this.neuButton5);
            this.Controls.Add(this.neuButton6);
            this.Controls.Add(this.neuButton3);
            this.Controls.Add(this.neuButton2);
            this.Controls.Add(this.neuButton1);
            this.Controls.Add(this.dgvResult);
            this.Name = "frmCatalogueQuery";
            this.Text = "目录查询";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResult;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton4;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton5;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton6;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton3;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton2;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;

    }
}