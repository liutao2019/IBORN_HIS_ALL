namespace FS.HISFC.Components.Speciment
{
    partial class frmGenerateCode
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
            this.flpQueryInpatientNo = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.nudNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSendDoc = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDisType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudNum)).BeginInit();
            this.SuspendLayout();
            // 
            // flpQueryInpatientNo
            // 
            this.flpQueryInpatientNo.Location = new System.Drawing.Point(73, 4);
            this.flpQueryInpatientNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flpQueryInpatientNo.Name = "flpQueryInpatientNo";
            this.flpQueryInpatientNo.Size = new System.Drawing.Size(267, 45);
            this.flpQueryInpatientNo.TabIndex = 0;
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(168, 200);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(196, 26);
            this.txtBarCode.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(293, 248);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 31);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印条码";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // nudNum
            // 
            this.nudNum.Location = new System.Drawing.Point(199, 248);
            this.nudNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudNum.Name = "nudNum";
            this.nudNum.Size = new System.Drawing.Size(87, 26);
            this.nudNum.TabIndex = 3;
            this.nudNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 204);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "条形码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 163);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 48;
            this.label4.Text = "送存医生：";
            // 
            // cmbSendDoc
            // 
            //this.cmbSendDoc.A = false;
            this.cmbSendDoc.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSendDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSendDoc.FormattingEnabled = true;
            this.cmbSendDoc.IsFlat = true;
            this.cmbSendDoc.IsLike = true;
            this.cmbSendDoc.Location = new System.Drawing.Point(168, 157);
            this.cmbSendDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSendDoc.Name = "cmbSendDoc";
            this.cmbSendDoc.PopForm = null;
            this.cmbSendDoc.ShowCustomerList = false;
            this.cmbSendDoc.ShowID = false;
            this.cmbSendDoc.Size = new System.Drawing.Size(149, 24);
            this.cmbSendDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbSendDoc.TabIndex = 47;
            this.cmbSendDoc.Tag = "";
            this.cmbSendDoc.ToolBarUse = false;
            // 
            // cmbDept
            // 
            //this.cmbDept.A = false;
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsFlat = true;
            this.cmbDept.IsLike = true;
            this.cmbDept.Location = new System.Drawing.Point(168, 111);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(149, 24);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDept.TabIndex = 46;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 45;
            this.label2.Text = "送存科室：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(60, 248);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 31);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 74;
            this.label3.Text = "病种：";
            // 
            // cmbDisType
            // 
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.Location = new System.Drawing.Point(168, 57);
            this.cmbDisType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.Size = new System.Drawing.Size(160, 24);
            this.cmbDisType.TabIndex = 73;
            // 
            // frmGenerateCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 313);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDisType);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSendDoc);
            this.Controls.Add(this.cmbDept);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudNum);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.flpQueryInpatientNo);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "frmGenerateCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标本库条形码生成";
            this.Load += new System.EventHandler(this.frmGenerateCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpQueryInpatientNo;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.NumericUpDown nudNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSendDoc;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDisType;
    }
}