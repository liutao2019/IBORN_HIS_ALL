namespace FS.HISFC.Components.Order.Controls
{
    partial class ucMesseageSend
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtExecBillName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtcard = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtname = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textphone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbTemplate = new System.Windows.Forms.ComboBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(9, 8);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "就诊卡号：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(171, 8);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "患者姓名：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(9, 37);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 3;
            this.neuLabel3.Text = "联系电话：";
            // 
            // txtExecBillName
            // 
            this.txtExecBillName.IsEnter2Tab = false;
            this.txtExecBillName.Location = new System.Drawing.Point(3, 79);
            this.txtExecBillName.Multiline = true;
            this.txtExecBillName.Name = "txtExecBillName";
            this.txtExecBillName.Size = new System.Drawing.Size(377, 141);
            this.txtExecBillName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtExecBillName.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(296, 221);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "发  送";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtcard
            // 
            this.txtcard.IsEnter2Tab = false;
            this.txtcard.Location = new System.Drawing.Point(72, 5);
            this.txtcard.Name = "txtcard";
            this.txtcard.Size = new System.Drawing.Size(99, 21);
            this.txtcard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtcard.TabIndex = 9;
            // 
            // txtname
            // 
            this.txtname.IsEnter2Tab = false;
            this.txtname.Location = new System.Drawing.Point(241, 5);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(139, 21);
            this.txtname.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtname.TabIndex = 10;
            // 
            // textphone
            // 
            this.textphone.IsEnter2Tab = false;
            this.textphone.Location = new System.Drawing.Point(72, 34);
            this.textphone.Name = "textphone";
            this.textphone.Size = new System.Drawing.Size(99, 21);
            this.textphone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textphone.TabIndex = 11;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(9, 60);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 12;
            this.neuLabel4.Text = "输入内容：";
            // 
            // cbTemplate
            // 
            this.cbTemplate.FormattingEnabled = true;
            this.cbTemplate.Location = new System.Drawing.Point(241, 34);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(139, 20);
            this.cbTemplate.TabIndex = 13;
            this.cbTemplate.SelectedValueChanged += new System.EventHandler(this.cbTemplate_SelectedValueChanged);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(174, 37);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 14;
            this.neuLabel5.Text = "模 板 名：";
            // 
            // ucMesseageSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.cbTemplate);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.textphone);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.txtcard);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtExecBillName);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Name = "ucMesseageSend";
            this.Size = new System.Drawing.Size(383, 245);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtExecBillName;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtcard;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtname;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textphone;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private System.Windows.Forms.ComboBox cbTemplate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
    }
}
