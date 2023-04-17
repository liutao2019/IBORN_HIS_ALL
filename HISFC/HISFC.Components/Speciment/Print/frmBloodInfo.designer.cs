namespace FS.HISFC.Components.Speciment.Print
{
    partial class frmBloodInfo
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
            this.chkSecond = new System.Windows.Forms.CheckBox();
            this.chkFirst = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.chkRad = new System.Windows.Forms.CheckBox();
            this.chkMed = new System.Windows.Forms.CheckBox();
            this.chkTransfer = new System.Windows.Forms.CheckBox();
            this.chkNone = new System.Windows.Forms.CheckBox();
            this.chkOther = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMedComment = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblCancel = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.rbtBeforeRad = new System.Windows.Forms.RadioButton();
            this.rbtAfterRad = new System.Windows.Forms.RadioButton();
            this.pnRad = new System.Windows.Forms.Panel();
            this.pnMed = new System.Windows.Forms.Panel();
            this.rbtAfterMed = new System.Windows.Forms.RadioButton();
            this.rbtBeforeMed = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pnOper = new System.Windows.Forms.Panel();
            this.rbtAfterOper = new System.Windows.Forms.RadioButton();
            this.rbtBeforeOper = new System.Windows.Forms.RadioButton();
            this.chkOperation = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDiagnose = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.pnRad.SuspendLayout();
            this.pnMed.SuspendLayout();
            this.pnOper.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSecond
            // 
            this.chkSecond.AutoSize = true;
            this.chkSecond.Location = new System.Drawing.Point(180, 219);
            this.chkSecond.Margin = new System.Windows.Forms.Padding(4);
            this.chkSecond.Name = "chkSecond";
            this.chkSecond.Size = new System.Drawing.Size(48, 16);
            this.chkSecond.TabIndex = 63;
            this.chkSecond.Text = "复发";
            this.chkSecond.UseVisualStyleBackColor = true;
            // 
            // chkFirst
            // 
            this.chkFirst.AutoSize = true;
            this.chkFirst.Location = new System.Drawing.Point(115, 219);
            this.chkFirst.Margin = new System.Windows.Forms.Padding(4);
            this.chkFirst.Name = "chkFirst";
            this.chkFirst.Size = new System.Drawing.Size(48, 16);
            this.chkFirst.TabIndex = 62;
            this.chkFirst.Text = "原发";
            this.chkFirst.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(39, 220);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 61;
            this.label17.Text = "肿瘤性质：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(27, 48);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 24);
            this.label19.TabIndex = 53;
            this.label19.Text = "采血时所处\r\n的治疗阶段：";
            // 
            // chkRad
            // 
            this.chkRad.AutoSize = true;
            this.chkRad.Location = new System.Drawing.Point(115, 117);
            this.chkRad.Margin = new System.Windows.Forms.Padding(4);
            this.chkRad.Name = "chkRad";
            this.chkRad.Size = new System.Drawing.Size(48, 16);
            this.chkRad.TabIndex = 54;
            this.chkRad.Text = "放疗";
            this.chkRad.UseVisualStyleBackColor = true;
            this.chkRad.CheckedChanged += new System.EventHandler(this.chkRad_CheckedChanged);
            // 
            // chkMed
            // 
            this.chkMed.AutoSize = true;
            this.chkMed.Location = new System.Drawing.Point(115, 152);
            this.chkMed.Margin = new System.Windows.Forms.Padding(4);
            this.chkMed.Name = "chkMed";
            this.chkMed.Size = new System.Drawing.Size(48, 16);
            this.chkMed.TabIndex = 55;
            this.chkMed.Text = "化疗";
            this.chkMed.UseVisualStyleBackColor = true;
            this.chkMed.CheckedChanged += new System.EventHandler(this.chkMed_CheckedChanged);
            // 
            // chkTransfer
            // 
            this.chkTransfer.AutoSize = true;
            this.chkTransfer.Location = new System.Drawing.Point(247, 219);
            this.chkTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.chkTransfer.Name = "chkTransfer";
            this.chkTransfer.Size = new System.Drawing.Size(48, 16);
            this.chkTransfer.TabIndex = 56;
            this.chkTransfer.Text = "转移";
            this.chkTransfer.UseVisualStyleBackColor = true;
            // 
            // chkNone
            // 
            this.chkNone.AutoSize = true;
            this.chkNone.Location = new System.Drawing.Point(115, 47);
            this.chkNone.Margin = new System.Windows.Forms.Padding(4);
            this.chkNone.Name = "chkNone";
            this.chkNone.Size = new System.Drawing.Size(60, 16);
            this.chkNone.TabIndex = 57;
            this.chkNone.Text = "未治疗";
            this.chkNone.UseVisualStyleBackColor = true;
            this.chkNone.CheckedChanged += new System.EventHandler(this.chkNone_CheckedChanged);
            // 
            // chkOther
            // 
            this.chkOther.AutoSize = true;
            this.chkOther.Location = new System.Drawing.Point(115, 187);
            this.chkOther.Margin = new System.Windows.Forms.Padding(4);
            this.chkOther.Name = "chkOther";
            this.chkOther.Size = new System.Drawing.Size(48, 16);
            this.chkOther.TabIndex = 58;
            this.chkOther.Text = "其它";
            this.chkOther.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(39, 325);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 12);
            this.label21.TabIndex = 59;
            this.label21.Text = "备注：";
            // 
            // txtMedComment
            // 
            this.txtMedComment.Location = new System.Drawing.Point(115, 305);
            this.txtMedComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtMedComment.Multiline = true;
            this.txtMedComment.Name = "txtMedComment";
            this.txtMedComment.Size = new System.Drawing.Size(231, 56);
            this.txtMedComment.TabIndex = 60;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(133, 378);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 64;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblCancel
            // 
            this.lblCancel.AutoSize = true;
            this.lblCancel.Location = new System.Drawing.Point(140, 18);
            this.lblCancel.Name = "lblCancel";
            this.lblCancel.Size = new System.Drawing.Size(137, 12);
            this.lblCancel.TabIndex = 65;
            this.lblCancel.Text = "可以点“取消”取消操作";
            this.lblCancel.Visible = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(271, 378);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 66;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Visible = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // rbtBeforeRad
            // 
            this.rbtBeforeRad.AutoSize = true;
            this.rbtBeforeRad.Checked = true;
            this.rbtBeforeRad.Location = new System.Drawing.Point(3, 6);
            this.rbtBeforeRad.Name = "rbtBeforeRad";
            this.rbtBeforeRad.Size = new System.Drawing.Size(35, 16);
            this.rbtBeforeRad.TabIndex = 68;
            this.rbtBeforeRad.TabStop = true;
            this.rbtBeforeRad.Text = "前";
            this.rbtBeforeRad.UseVisualStyleBackColor = true;
            // 
            // rbtAfterRad
            // 
            this.rbtAfterRad.AutoSize = true;
            this.rbtAfterRad.Location = new System.Drawing.Point(44, 6);
            this.rbtAfterRad.Name = "rbtAfterRad";
            this.rbtAfterRad.Size = new System.Drawing.Size(35, 16);
            this.rbtAfterRad.TabIndex = 69;
            this.rbtAfterRad.Text = "后";
            this.rbtAfterRad.UseVisualStyleBackColor = true;
            // 
            // pnRad
            // 
            this.pnRad.Controls.Add(this.rbtAfterRad);
            this.pnRad.Controls.Add(this.rbtBeforeRad);
            this.pnRad.Location = new System.Drawing.Point(203, 110);
            this.pnRad.Name = "pnRad";
            this.pnRad.Size = new System.Drawing.Size(83, 25);
            this.pnRad.TabIndex = 67;
            this.pnRad.Visible = false;
            // 
            // pnMed
            // 
            this.pnMed.Controls.Add(this.rbtAfterMed);
            this.pnMed.Controls.Add(this.rbtBeforeMed);
            this.pnMed.Location = new System.Drawing.Point(203, 145);
            this.pnMed.Name = "pnMed";
            this.pnMed.Size = new System.Drawing.Size(83, 25);
            this.pnMed.TabIndex = 68;
            this.pnMed.Visible = false;
            // 
            // rbtAfterMed
            // 
            this.rbtAfterMed.AutoSize = true;
            this.rbtAfterMed.Location = new System.Drawing.Point(44, 6);
            this.rbtAfterMed.Name = "rbtAfterMed";
            this.rbtAfterMed.Size = new System.Drawing.Size(35, 16);
            this.rbtAfterMed.TabIndex = 69;
            this.rbtAfterMed.Text = "后";
            this.rbtAfterMed.UseVisualStyleBackColor = true;
            // 
            // rbtBeforeMed
            // 
            this.rbtBeforeMed.AutoSize = true;
            this.rbtBeforeMed.Checked = true;
            this.rbtBeforeMed.Location = new System.Drawing.Point(3, 6);
            this.rbtBeforeMed.Name = "rbtBeforeMed";
            this.rbtBeforeMed.Size = new System.Drawing.Size(35, 16);
            this.rbtBeforeMed.TabIndex = 68;
            this.rbtBeforeMed.TabStop = true;
            this.rbtBeforeMed.Text = "前";
            this.rbtBeforeMed.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(315, 219);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 69;
            this.checkBox1.Text = "其它";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pnOper
            // 
            this.pnOper.Controls.Add(this.rbtAfterOper);
            this.pnOper.Controls.Add(this.rbtBeforeOper);
            this.pnOper.Location = new System.Drawing.Point(203, 75);
            this.pnOper.Name = "pnOper";
            this.pnOper.Size = new System.Drawing.Size(83, 25);
            this.pnOper.TabIndex = 71;
            this.pnOper.Visible = false;
            // 
            // rbtAfterOper
            // 
            this.rbtAfterOper.AutoSize = true;
            this.rbtAfterOper.Location = new System.Drawing.Point(44, 6);
            this.rbtAfterOper.Name = "rbtAfterOper";
            this.rbtAfterOper.Size = new System.Drawing.Size(35, 16);
            this.rbtAfterOper.TabIndex = 69;
            this.rbtAfterOper.Text = "后";
            this.rbtAfterOper.UseVisualStyleBackColor = true;
            // 
            // rbtBeforeOper
            // 
            this.rbtBeforeOper.AutoSize = true;
            this.rbtBeforeOper.Checked = true;
            this.rbtBeforeOper.Location = new System.Drawing.Point(3, 6);
            this.rbtBeforeOper.Name = "rbtBeforeOper";
            this.rbtBeforeOper.Size = new System.Drawing.Size(35, 16);
            this.rbtBeforeOper.TabIndex = 68;
            this.rbtBeforeOper.TabStop = true;
            this.rbtBeforeOper.Text = "前";
            this.rbtBeforeOper.UseVisualStyleBackColor = true;
            // 
            // chkOperation
            // 
            this.chkOperation.AutoSize = true;
            this.chkOperation.Location = new System.Drawing.Point(115, 82);
            this.chkOperation.Margin = new System.Windows.Forms.Padding(4);
            this.chkOperation.Name = "chkOperation";
            this.chkOperation.Size = new System.Drawing.Size(48, 16);
            this.chkOperation.TabIndex = 70;
            this.chkOperation.Text = "手术";
            this.chkOperation.UseVisualStyleBackColor = true;
            this.chkOperation.CheckedChanged += new System.EventHandler(this.chkOperation_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 72;
            this.label1.Text = "诊断：";
            // 
            // txtDiagnose
            // 
            //this.txtDiagnose.A = false;
            this.txtDiagnose.ArrowBackColor = System.Drawing.Color.Silver;
            this.txtDiagnose.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.txtDiagnose.FormattingEnabled = true;
            this.txtDiagnose.IsFlat = true;
            this.txtDiagnose.IsLike = true;
            this.txtDiagnose.Location = new System.Drawing.Point(115, 257);
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.PopForm = null;
            this.txtDiagnose.ShowCustomerList = false;
            this.txtDiagnose.ShowID = false;
            this.txtDiagnose.Size = new System.Drawing.Size(231, 22);
            this.txtDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagnose.TabIndex = 74;
            this.txtDiagnose.Tag = "";
            this.txtDiagnose.ToolBarUse = false;
            // 
            // frmBloodInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(426, 414);
            this.Controls.Add(this.txtDiagnose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnOper);
            this.Controls.Add(this.chkOperation);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pnMed);
            this.Controls.Add(this.pnRad);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.lblCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkSecond);
            this.Controls.Add(this.chkFirst);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.chkRad);
            this.Controls.Add(this.chkMed);
            this.Controls.Add(this.chkTransfer);
            this.Controls.Add(this.chkNone);
            this.Controls.Add(this.chkOther);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtMedComment);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBloodInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "血标本采集信息";
            this.Load += new System.EventHandler(this.frmBloodInfo_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBloodInfo_FormClosing);
            this.pnRad.ResumeLayout(false);
            this.pnRad.PerformLayout();
            this.pnMed.ResumeLayout(false);
            this.pnMed.PerformLayout();
            this.pnOper.ResumeLayout(false);
            this.pnOper.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSecond;
        private System.Windows.Forms.CheckBox chkFirst;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox chkRad;
        private System.Windows.Forms.CheckBox chkMed;
        private System.Windows.Forms.CheckBox chkTransfer;
        private System.Windows.Forms.CheckBox chkNone;
        private System.Windows.Forms.CheckBox chkOther;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMedComment;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblCancel;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.RadioButton rbtBeforeRad;
        private System.Windows.Forms.RadioButton rbtAfterRad;
        private System.Windows.Forms.Panel pnRad;
        private System.Windows.Forms.Panel pnMed;
        private System.Windows.Forms.RadioButton rbtAfterMed;
        private System.Windows.Forms.RadioButton rbtBeforeMed;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel pnOper;
        private System.Windows.Forms.RadioButton rbtAfterOper;
        private System.Windows.Forms.RadioButton rbtBeforeOper;
        private System.Windows.Forms.CheckBox chkOperation;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox txtDiagnose;
    }
}