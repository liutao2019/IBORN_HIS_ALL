namespace IBorn.SI.MedicalInsurance.OutPatient
{
    partial class ucOutPrint
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
            this.searchTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ucOutPatientFeeDetailBill1 = new IBorn.SI.MedicalInsurance.BaseControls.ucOutPatientFeeDetailBill();
            this.ucInvoicePrint1 = new IBorn.SI.MedicalInsurance.BaseControls.ucInvoicePrint();
            this.ucOutPatientInfo1 = new IBorn.SI.MedicalInsurance.BaseControls.ucOutPatientInfo();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.searchTxt);
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
            this.label1.Location = new System.Drawing.Point(336, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "就医登记号：";
            this.label1.Visible = false;
            // 
            // txtRegNO
            // 
            this.txtRegNO.Location = new System.Drawing.Point(425, 11);
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
            this.tabControl1.Location = new System.Drawing.Point(0, 135);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(807, 465);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucOutPatientFeeDetailBill1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(799, 439);
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
            this.tabPage1.Size = new System.Drawing.Size(799, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "发票";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // searchTxt
            // 
            this.searchTxt.Location = new System.Drawing.Point(90, 12);
            this.searchTxt.Name = "searchTxt";
            this.searchTxt.Size = new System.Drawing.Size(194, 21);
            this.searchTxt.TabIndex = 2;
            this.searchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTxt_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "检索患者：";
            // 
            // ucOutPatientFeeDetailBill1
            // 
            this.ucOutPatientFeeDetailBill1.BackColor = System.Drawing.Color.White;
            this.ucOutPatientFeeDetailBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOutPatientFeeDetailBill1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucOutPatientFeeDetailBill1.IsFullConvertToHalf = true;
            this.ucOutPatientFeeDetailBill1.IsPrint = false;
            this.ucOutPatientFeeDetailBill1.Location = new System.Drawing.Point(3, 3);
            this.ucOutPatientFeeDetailBill1.Name = "ucOutPatientFeeDetailBill1";
            this.ucOutPatientFeeDetailBill1.ParentFormToolBar = null;
            this.ucOutPatientFeeDetailBill1.Size = new System.Drawing.Size(793, 433);
            this.ucOutPatientFeeDetailBill1.TabIndex = 0;
            // 
            // ucInvoicePrint1
            // 
            this.ucInvoicePrint1.BackColor = System.Drawing.Color.White;
            this.ucInvoicePrint1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInvoicePrint1.Location = new System.Drawing.Point(3, 3);
            this.ucInvoicePrint1.Name = "ucInvoicePrint1";
            this.ucInvoicePrint1.Size = new System.Drawing.Size(793, 433);
            this.ucInvoicePrint1.TabIndex = 0;
            // 
            // ucOutPatientInfo1
            // 
            this.ucOutPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucOutPatientInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucOutPatientInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucOutPatientInfo1.IsFullConvertToHalf = true;
            this.ucOutPatientInfo1.IsPrint = false;
            this.ucOutPatientInfo1.Location = new System.Drawing.Point(0, 44);
            this.ucOutPatientInfo1.Name = "ucOutPatientInfo1";
            this.ucOutPatientInfo1.ParentFormToolBar = null;
            this.ucOutPatientInfo1.Size = new System.Drawing.Size(807, 91);
            this.ucOutPatientInfo1.TabIndex = 0;
            // 
            // ucOutPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ucOutPatientInfo1);
            this.Controls.Add(this.panel1);
            this.Name = "ucOutPrint";
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegNO;
        private BaseControls.ucInvoicePrint ucInvoicePrint1;
        private BaseControls.ucOutPatientFeeDetailBill ucOutPatientFeeDetailBill1;
        private BaseControls.ucOutPatientInfo ucOutPatientInfo1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchTxt;
    }
}
