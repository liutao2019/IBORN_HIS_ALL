namespace FS.HISFC.Components.Speciment.Print
{
    partial class ucBColForm
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
            this.components = new System.ComponentModel.Container();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSendDoc = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.flpQueryInpatientNo = new System.Windows.Forms.FlowLayoutPanel();
            this.grpCol = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSpecNum = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpCol.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 83;
            this.label4.Text = "送存医生:";
            // 
            // cmbSendDoc
            // 
            //this.cmbSendDoc.A = false;
            this.cmbSendDoc.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSendDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSendDoc.FormattingEnabled = true;
            this.cmbSendDoc.IsFlat = true;
            this.cmbSendDoc.IsLike = true;
            this.cmbSendDoc.Location = new System.Drawing.Point(99, 80);
            this.cmbSendDoc.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSendDoc.Name = "cmbSendDoc";
            this.cmbSendDoc.PopForm = null;
            this.cmbSendDoc.ShowCustomerList = false;
            this.cmbSendDoc.ShowID = false;
            this.cmbSendDoc.Size = new System.Drawing.Size(118, 24);
            this.cmbSendDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbSendDoc.TabIndex = 82;
            this.cmbSendDoc.Tag = "";
            this.cmbSendDoc.ToolBarUse = false;
            this.cmbSendDoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSendDoc_KeyDown);
            // 
            // cmbDept
            // 
            //this.cmbDept.A = false;
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsFlat = true;
            this.cmbDept.IsLike = true;
            this.cmbDept.Location = new System.Drawing.Point(100, 38);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(117, 24);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDept.TabIndex = 81;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            this.cmbDept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDept_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 80;
            this.label2.Text = "送存科室:";
            // 
            // flpQueryInpatientNo
            // 
            this.flpQueryInpatientNo.Location = new System.Drawing.Point(15, 112);
            this.flpQueryInpatientNo.Margin = new System.Windows.Forms.Padding(4);
            this.flpQueryInpatientNo.Name = "flpQueryInpatientNo";
            this.flpQueryInpatientNo.Size = new System.Drawing.Size(227, 44);
            this.flpQueryInpatientNo.TabIndex = 75;
            this.flpQueryInpatientNo.Visible = false;
            // 
            // grpCol
            // 
            this.grpCol.Controls.Add(this.label5);
            this.grpCol.Controls.Add(this.txtCardNo);
            this.grpCol.Controls.Add(this.label9);
            this.grpCol.Controls.Add(this.label8);
            this.grpCol.Controls.Add(this.label3);
            this.grpCol.Controls.Add(this.label1);
            this.grpCol.Controls.Add(this.flpQueryInpatientNo);
            this.grpCol.Controls.Add(this.label4);
            this.grpCol.Controls.Add(this.cmbSendDoc);
            this.grpCol.Controls.Add(this.label2);
            this.grpCol.Controls.Add(this.cmbDept);
            this.grpCol.Location = new System.Drawing.Point(9, 4);
            this.grpCol.Margin = new System.Windows.Forms.Padding(4);
            this.grpCol.Name = "grpCol";
            this.grpCol.Padding = new System.Windows.Forms.Padding(4);
            this.grpCol.Size = new System.Drawing.Size(494, 196);
            this.grpCol.TabIndex = 87;
            this.grpCol.TabStop = false;
            this.grpCol.Text = "标本收集信息";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(224, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(264, 16);
            this.label8.TabIndex = 86;
            this.label8.Text = "(按空格键选择，回车跳到下一选项)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 16);
            this.label3.TabIndex = 85;
            this.label3.Text = "(按空格键选择，回车跳到下一选项)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 16);
            this.label1.TabIndex = 84;
            this.label1.Text = "(输入病历号后，按回车保存)";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.lblBarCode);
            this.neuPanel1.Controls.Add(this.lblName);
            this.neuPanel1.Controls.Add(this.label6);
            this.neuPanel1.Controls.Add(this.lblSpecNum);
            this.neuPanel1.Controls.Add(this.lblType);
            this.neuPanel1.Controls.Add(this.label7);
            this.neuPanel1.Location = new System.Drawing.Point(8, 208);
            this.neuPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(499, 112);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 88;
            // 
            // lblBarCode
            // 
            this.lblBarCode.AutoSize = true;
            this.lblBarCode.Location = new System.Drawing.Point(355, 26);
            this.lblBarCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(0, 16);
            this.lblBarCode.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(121, 75);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 16);
            this.lblName.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "病人姓名:";
            // 
            // lblSpecNum
            // 
            this.lblSpecNum.AutoSize = true;
            this.lblSpecNum.Location = new System.Drawing.Point(121, 26);
            this.lblSpecNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpecNum.Name = "lblSpecNum";
            this.lblSpecNum.Size = new System.Drawing.Size(0, 16);
            this.lblSpecNum.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(13, 26);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(96, 16);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "住院流水号:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(267, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 79;
            this.label7.Text = "条形码:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(266, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 16);
            this.label9.TabIndex = 87;
            this.label9.Text = "(门诊病人专用)";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(75, 163);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(167, 26);
            this.txtCardNo.TabIndex = 88;
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 89;
            this.label5.Text = "门诊号:";
            // 
            // ucBColForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(511, 337);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.grpCol);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ucBColForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "798";
            this.Text = "血标本采集单";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ucBColForm_Load);
            this.Shown += new System.EventHandler(this.ucBColForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ucBColForm_FormClosing);
            this.grpCol.ResumeLayout(false);
            this.grpCol.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSendDoc;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flpQueryInpatientNo;
        private System.Windows.Forms.GroupBox grpCol;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBarCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSpecNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;


    }
}
