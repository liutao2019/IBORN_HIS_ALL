namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class frmLeavePrint
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtBeginDate = new System.Windows.Forms.DateTimePicker();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLeaveType = new System.Windows.Forms.Label();
            this.cmbLeaveType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "病假开始时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "病假结束时间：";
            // 
            // dtBeginDate
            // 
            this.dtBeginDate.Location = new System.Drawing.Point(138, 52);
            this.dtBeginDate.Name = "dtBeginDate";
            this.dtBeginDate.Size = new System.Drawing.Size(140, 21);
            this.dtBeginDate.TabIndex = 2;
            // 
            // dtEndDate
            // 
            this.dtEndDate.Location = new System.Drawing.Point(138, 95);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(140, 21);
            this.dtEndDate.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(203, 131);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btSavet_Click);
            // 
            // lblLeaveType
            // 
            this.lblLeaveType.AutoSize = true;
            this.lblLeaveType.Location = new System.Drawing.Point(67, 20);
            this.lblLeaveType.Name = "lblLeaveType";
            this.lblLeaveType.Size = new System.Drawing.Size(65, 12);
            this.lblLeaveType.TabIndex = 6;
            this.lblLeaveType.Text = "请假类型：";
            this.lblLeaveType.Visible = false;
            // 
            // cmbLeaveType
            // 
            this.cmbLeaveType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbLeaveType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbLeaveType.FormattingEnabled = true;
            this.cmbLeaveType.IsEnter2Tab = false;
            this.cmbLeaveType.IsFlat = false;
            this.cmbLeaveType.IsLike = true;
            this.cmbLeaveType.IsListOnly = false;
            this.cmbLeaveType.IsPopForm = true;
            this.cmbLeaveType.IsShowCustomerList = false;
            this.cmbLeaveType.IsShowID = false;
            this.cmbLeaveType.IsShowIDAndName = false;
            this.cmbLeaveType.Location = new System.Drawing.Point(138, 17);
            this.cmbLeaveType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLeaveType.Name = "cmbLeaveType";
            this.cmbLeaveType.ShowCustomerList = false;
            this.cmbLeaveType.ShowID = false;
            this.cmbLeaveType.Size = new System.Drawing.Size(140, 20);
            this.cmbLeaveType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbLeaveType.TabIndex = 52;
            this.cmbLeaveType.Tag = "";
            this.cmbLeaveType.ToolBarUse = false;
            this.cmbLeaveType.Visible = false;
            // 
            // frmLeavePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 162);
            this.Controls.Add(this.cmbLeaveType);
            this.Controls.Add(this.lblLeaveType);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtBeginDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "frmLeavePrint";
            this.Text = "病假建议";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtBeginDate;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLeaveType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbLeaveType;
    }
}