namespace IBorn.SI.MedicalInsurance.InPatient
{
    partial class ucBabyPrint
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucQueryInfo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ucInPatientFeeDetailBill1 = new IBorn.SI.MedicalInsurance.BaseControls.ucInPatientFeeDetailBabyBill();
            this.ucInPatientInfo1 = new IBorn.SI.MedicalInsurance.BaseControls.ucInPatientInfo();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.ucQueryInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 44);
            this.panel1.TabIndex = 0;
            // 
            // ucQueryInfo
            // 
            this.ucQueryInfo.DefaultInputType = 0;
            this.ucQueryInfo.InputType = 0;
            this.ucQueryInfo.IsDeptOnly = true;
            this.ucQueryInfo.Location = new System.Drawing.Point(16, 11);
            this.ucQueryInfo.Name = "ucQueryInfo";
            this.ucQueryInfo.PatientInState = "ALL";
            this.ucQueryInfo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInfo.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInfo.TabIndex = 2;
            this.ucQueryInfo.Load += new System.EventHandler(this.ucQueryInfo_Load);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 176);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(793, 424);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucInPatientFeeDetailBill1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(785, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "清单";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(229, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "是否过滤零元项目";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ucInPatientFeeDetailBill1
            // 
            this.ucInPatientFeeDetailBill1.BackColor = System.Drawing.Color.White;
            this.ucInPatientFeeDetailBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInPatientFeeDetailBill1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucInPatientFeeDetailBill1.IsFullConvertToHalf = true;
            this.ucInPatientFeeDetailBill1.IsPrint = false;
            this.ucInPatientFeeDetailBill1.Location = new System.Drawing.Point(3, 3);
            this.ucInPatientFeeDetailBill1.Name = "ucInPatientFeeDetailBill1";
            this.ucInPatientFeeDetailBill1.ParentFormToolBar = null;
            this.ucInPatientFeeDetailBill1.Size = new System.Drawing.Size(779, 392);
            this.ucInPatientFeeDetailBill1.TabIndex = 0;
            // 
            // ucInPatientInfo1
            // 
            this.ucInPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucInPatientInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucInPatientInfo1.IsFullConvertToHalf = true;
            this.ucInPatientInfo1.IsPrint = false;
            this.ucInPatientInfo1.Location = new System.Drawing.Point(0, 44);
            this.ucInPatientInfo1.Name = "ucInPatientInfo1";
            this.ucInPatientInfo1.ParentFormToolBar = null;
            this.ucInPatientInfo1.Size = new System.Drawing.Size(793, 132);
            this.ucInPatientInfo1.TabIndex = 1;
            // 
            // ucBabyPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ucInPatientInfo1);
            this.Controls.Add(this.panel1);
            this.Name = "ucBabyPrint";
            this.Size = new System.Drawing.Size(793, 600);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private BaseControls.ucInPatientInfo ucInPatientInfo1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private BaseControls.ucInPatientFeeDetailBabyBill ucInPatientFeeDetailBill1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInfo;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
