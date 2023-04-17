namespace FS.HISFC.Components.Speciment.DataImport
{
    partial class frmAutoDiag
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
            this.rbtBld = new System.Windows.Forms.RadioButton();
            this.rbtOrg = new System.Windows.Forms.RadioButton();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDisType = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblCnt = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbtBld
            // 
            this.rbtBld.AutoSize = true;
            this.rbtBld.Checked = true;
            this.rbtBld.Location = new System.Drawing.Point(242, 63);
            this.rbtBld.Margin = new System.Windows.Forms.Padding(4);
            this.rbtBld.Name = "rbtBld";
            this.rbtBld.Size = new System.Drawing.Size(42, 20);
            this.rbtBld.TabIndex = 97;
            this.rbtBld.TabStop = true;
            this.rbtBld.Text = "血";
            this.rbtBld.UseVisualStyleBackColor = true;
            // 
            // rbtOrg
            // 
            this.rbtOrg.AutoSize = true;
            this.rbtOrg.Location = new System.Drawing.Point(311, 63);
            this.rbtOrg.Margin = new System.Windows.Forms.Padding(4);
            this.rbtOrg.Name = "rbtOrg";
            this.rbtOrg.Size = new System.Drawing.Size(58, 20);
            this.rbtOrg.TabIndex = 96;
            this.rbtOrg.Text = "组织";
            this.rbtOrg.UseVisualStyleBackColor = true;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(242, 22);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(5);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(135, 26);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 94;
            this.label1.Text = "时间:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(72, 22);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(5);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(139, 26);
            this.dtpStartDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStartDate.TabIndex = 93;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(226, 27);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 92;
            this.label11.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 99;
            this.label3.Text = "病种:";
            // 
            // cmbDisType
            // 
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.Location = new System.Drawing.Point(72, 61);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.Size = new System.Drawing.Size(101, 24);
            this.cmbDisType.TabIndex = 98;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(200, 139);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(84, 35);
            this.btnStart.TabIndex = 100;
            this.btnStart.Text = "开始执行";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblCnt
            // 
            this.lblCnt.AutoSize = true;
            this.lblCnt.Location = new System.Drawing.Point(155, 101);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(0, 16);
            this.lblCnt.TabIndex = 101;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(60, 139);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 35);
            this.btnQuery.TabIndex = 102;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // frmAutoDiag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 186);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.lblCnt);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDisType);
            this.Controls.Add(this.rbtBld);
            this.Controls.Add(this.rbtOrg);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label11);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAutoDiag";
            this.Text = "frmAutoDiag";
            this.Load += new System.EventHandler(this.frmAutoDiag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtBld;
        private System.Windows.Forms.RadioButton rbtOrg;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDisType;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblCnt;
        private System.Windows.Forms.Button btnQuery;
    }
}