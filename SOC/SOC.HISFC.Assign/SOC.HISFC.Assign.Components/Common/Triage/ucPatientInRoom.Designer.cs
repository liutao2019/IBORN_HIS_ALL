namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    partial class ucPatientInRoom
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
            this.txtCard = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.txtRegDate = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.txtDept = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.txtQueue = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbConsole = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbRoom = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCard
            // 
            this.txtCard.Label = "病 历 号";
            this.txtCard.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCard.Location = new System.Drawing.Point(27, 19);
            this.txtCard.MaxLength = 32767;
            this.txtCard.Name = "txtCard";
            this.txtCard.ReadOnly = true;
            this.txtCard.Size = new System.Drawing.Size(324, 29);
            this.txtCard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCard.TabIndex = 0;
            this.txtCard.TextBoxLeft = 82;
            // 
            // txtName
            // 
            this.txtName.Label = "患者姓名";
            this.txtName.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtName.Location = new System.Drawing.Point(27, 54);
            this.txtName.MaxLength = 32767;
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(324, 29);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            this.txtName.TextBoxLeft = 82;
            // 
            // txtRegDate
            // 
            this.txtRegDate.Label = "挂号日期";
            this.txtRegDate.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRegDate.Location = new System.Drawing.Point(27, 89);
            this.txtRegDate.MaxLength = 32767;
            this.txtRegDate.Name = "txtRegDate";
            this.txtRegDate.ReadOnly = true;
            this.txtRegDate.Size = new System.Drawing.Size(324, 29);
            this.txtRegDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRegDate.TabIndex = 2;
            this.txtRegDate.TextBoxLeft = 82;
            // 
            // txtDept
            // 
            this.txtDept.Label = "挂号科室";
            this.txtDept.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDept.Location = new System.Drawing.Point(27, 124);
            this.txtDept.MaxLength = 32767;
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(324, 29);
            this.txtDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDept.TabIndex = 3;
            this.txtDept.TextBoxLeft = 82;
            // 
            // txtQueue
            // 
            this.txtQueue.Label = "分诊队列";
            this.txtQueue.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtQueue.Location = new System.Drawing.Point(27, 159);
            this.txtQueue.MaxLength = 32767;
            this.txtQueue.Name = "txtQueue";
            this.txtQueue.ReadOnly = true;
            this.txtQueue.Size = new System.Drawing.Size(324, 29);
            this.txtQueue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQueue.TabIndex = 4;
            this.txtQueue.TextBoxLeft = 82;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.cmbConsole);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.cmbRoom);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.txtCard);
            this.neuPanel1.Controls.Add(this.txtQueue);
            this.neuPanel1.Controls.Add(this.txtName);
            this.neuPanel1.Controls.Add(this.txtDept);
            this.neuPanel1.Controls.Add(this.txtRegDate);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(367, 293);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 5;
            // 
            // cmbConsole
            // 
            this.cmbConsole.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbConsole.Enabled = false;
            this.cmbConsole.FormattingEnabled = true;
            this.cmbConsole.IsEnter2Tab = false;
            this.cmbConsole.IsFlat = false;
            this.cmbConsole.IsLike = true;
            this.cmbConsole.IsListOnly = false;
            this.cmbConsole.IsPopForm = true;
            this.cmbConsole.IsShowCustomerList = false;
            this.cmbConsole.IsShowID = false;
            this.cmbConsole.Location = new System.Drawing.Point(108, 236);
            this.cmbConsole.Name = "cmbConsole";
            this.cmbConsole.PopForm = null;
            this.cmbConsole.ShowCustomerList = false;
            this.cmbConsole.ShowID = false;
            this.cmbConsole.Size = new System.Drawing.Size(231, 20);
            this.cmbConsole.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbConsole.TabIndex = 8;
            this.cmbConsole.Tag = "";
            this.cmbConsole.ToolBarUse = false;
            this.cmbConsole.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbConsole_KeyDown);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(30, 239);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            this.neuLabel2.Text = "诊    台";
            // 
            // cmbRoom
            // 
            this.cmbRoom.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbRoom.Enabled = false;
            this.cmbRoom.FormattingEnabled = true;
            this.cmbRoom.IsEnter2Tab = false;
            this.cmbRoom.IsFlat = false;
            this.cmbRoom.IsLike = true;
            this.cmbRoom.IsListOnly = false;
            this.cmbRoom.IsPopForm = true;
            this.cmbRoom.IsShowCustomerList = false;
            this.cmbRoom.IsShowID = false;
            this.cmbRoom.Location = new System.Drawing.Point(108, 199);
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.PopForm = null;
            this.cmbRoom.ShowCustomerList = false;
            this.cmbRoom.ShowID = false;
            this.cmbRoom.Size = new System.Drawing.Size(231, 20);
            this.cmbRoom.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRoom.TabIndex = 6;
            this.cmbRoom.Tag = "";
            this.cmbRoom.ToolBarUse = false;
            this.cmbRoom.SelectedIndexChanged += new System.EventHandler(this.cmbRoom_SelectedIndexChanged);
            this.cmbRoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRoom_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(30, 202);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 5;
            this.neuLabel1.Text = "诊    室";
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(52, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "确  定";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(251, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "退  出";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucPatientInRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPatientInRoom";
            this.Size = new System.Drawing.Size(367, 351);
            this.Load += new System.EventHandler(this.ucInRoom_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtCard;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtRegDate;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtDept;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtQueue;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbConsole;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRoom;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
    }
}
