namespace IBorn.SI.MedicalInsurance.InPatient
{
    partial class ucPrint
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRegNO = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucQueryInfo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.ucInPatientFeeDetailBill1 = new IBorn.SI.MedicalInsurance.BaseControls.ucInPatientFeeDetailBill();
            this.ucInvoicePrint1 = new IBorn.SI.MedicalInsurance.BaseControls.ucInvoicePrint();
            this.ucInPatientInfo1 = new IBorn.SI.MedicalInsurance.BaseControls.ucInPatientInfo();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucQueryInfo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtRegNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 44);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(334, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "就医登记号：";
            this.label1.Visible = false;
            // 
            // txtRegNO
            // 
            this.txtRegNO.Location = new System.Drawing.Point(423, 11);
            this.txtRegNO.Name = "txtRegNO";
            this.txtRegNO.Size = new System.Drawing.Size(194, 21);
            this.txtRegNO.TabIndex = 0;
            this.txtRegNO.Visible = false;
            this.txtRegNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRegNO_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 176);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(807, 424);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucInPatientFeeDetailBill1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(799, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "清单";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucInvoicePrint1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(799, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "发票";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.ucInPatientFeeDetailBill1.Size = new System.Drawing.Size(793, 392);
            this.ucInPatientFeeDetailBill1.TabIndex = 0;
            // 
            // ucInvoicePrint1
            // 
            this.ucInvoicePrint1.BackColor = System.Drawing.Color.White;
            this.ucInvoicePrint1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInvoicePrint1.Location = new System.Drawing.Point(3, 3);
            this.ucInvoicePrint1.Name = "ucInvoicePrint1";
            this.ucInvoicePrint1.Size = new System.Drawing.Size(793, 392);
            this.ucInvoicePrint1.TabIndex = 0;
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
            this.ucInPatientInfo1.Size = new System.Drawing.Size(807, 132);
            this.ucInPatientInfo1.TabIndex = 1;
            // 
            // ucPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ucInPatientInfo1);
            this.Controls.Add(this.panel1);
            this.Name = "ucPrint";
            this.Size = new System.Drawing.Size(807, 600);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private BaseControls.ucInPatientInfo ucInPatientInfo1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegNO;
        private BaseControls.ucInvoicePrint ucInvoicePrint1;
        private BaseControls.ucInPatientFeeDetailBill ucInPatientFeeDetailBill1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInfo;
    }
}
