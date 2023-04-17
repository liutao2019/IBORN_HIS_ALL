namespace FS.HISFC.Components.InpatientFee.Fee
{
    partial class ucCharge_new
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
            FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
            FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtBedNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblDoct = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDoct = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtAlarm = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblAlarm = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtLeft = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblLeft = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtInDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblInDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInTime = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblInTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryPatientInfo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.ucInpatientCharge_new1 = new FS.HISFC.Components.Common.Controls.ucInpatientCharge_new();
            lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPact
            // 
            lblPact.AutoSize = true;
            lblPact.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lblPact.Location = new System.Drawing.Point(582, 13);
            lblPact.Name = "lblPact";
            lblPact.Size = new System.Drawing.Size(66, 13);
            lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lblPact.TabIndex = 5;
            lblPact.Text = "合同单位:";
            // 
            // neuLabel1
            // 
            neuLabel1.AutoSize = true;
            neuLabel1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            neuLabel1.Location = new System.Drawing.Point(790, 13);
            neuLabel1.Name = "neuLabel1";
            neuLabel1.Size = new System.Drawing.Size(40, 13);
            neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            neuLabel1.TabIndex = 31;
            neuLabel1.Text = "年龄:";
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.neuPanel1.Controls.Add(this.txtBedNO);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.txtAge);
            this.neuPanel1.Controls.Add(neuLabel1);
            this.neuPanel1.Controls.Add(this.linkLabel1);
            this.neuPanel1.Controls.Add(this.cmbDept);
            this.neuPanel1.Controls.Add(this.label1);
            this.neuPanel1.Controls.Add(this.txtName);
            this.neuPanel1.Controls.Add(this.lblDoct);
            this.neuPanel1.Controls.Add(this.cmbDoct);
            this.neuPanel1.Controls.Add(this.txtAlarm);
            this.neuPanel1.Controls.Add(this.lblAlarm);
            this.neuPanel1.Controls.Add(this.txtLeft);
            this.neuPanel1.Controls.Add(this.lblLeft);
            this.neuPanel1.Controls.Add(this.txtPact);
            this.neuPanel1.Controls.Add(lblPact);
            this.neuPanel1.Controls.Add(this.txtInDept);
            this.neuPanel1.Controls.Add(this.lblInDept);
            this.neuPanel1.Controls.Add(this.txtInTime);
            this.neuPanel1.Controls.Add(this.lblInTime);
            this.neuPanel1.Controls.Add(this.lbName);
            this.neuPanel1.Controls.Add(this.ucQueryPatientInfo);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(941, 96);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // txtBedNO
            // 
            this.txtBedNO.BackColor = System.Drawing.Color.White;
            this.txtBedNO.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBedNO.IsEnter2Tab = false;
            this.txtBedNO.Location = new System.Drawing.Point(284, 40);
            this.txtBedNO.Name = "txtBedNO";
            this.txtBedNO.ReadOnly = true;
            this.txtBedNO.Size = new System.Drawing.Size(121, 22);
            this.txtBedNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBedNO.TabIndex = 34;
            this.txtBedNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(244, 42);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(40, 13);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 33;
            this.neuLabel2.Text = "床号:";
            // 
            // txtAge
            // 
            this.txtAge.BackColor = System.Drawing.Color.White;
            this.txtAge.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAge.IsEnter2Tab = false;
            this.txtAge.Location = new System.Drawing.Point(827, 9);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(50, 22);
            this.txtAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAge.TabIndex = 32;
            this.txtAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Font = new System.Drawing.Font("宋体", 9F);
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(213, 14);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(25, 15);
            this.linkLabel1.TabIndex = 30;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "F11";
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(94, 66);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(118, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 16;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "物资扣库科室：";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(284, 10);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(121, 22);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 2;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDoct
            // 
            this.lblDoct.AutoSize = true;
            this.lblDoct.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoct.Location = new System.Drawing.Point(409, 42);
            this.lblDoct.Name = "lblDoct";
            this.lblDoct.Size = new System.Drawing.Size(66, 13);
            this.lblDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoct.TabIndex = 9;
            this.lblDoct.Text = "开方医生:";
            // 
            // cmbDoct
            // 
            this.cmbDoct.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDoct.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbDoct.FormattingEnabled = true;
            this.cmbDoct.IsEnter2Tab = false;
            this.cmbDoct.IsFlat = false;
            this.cmbDoct.IsLike = true;
            this.cmbDoct.IsListOnly = false;
            this.cmbDoct.IsPopForm = true;
            this.cmbDoct.IsShowCustomerList = false;
            this.cmbDoct.IsShowID = false;
            this.cmbDoct.IsShowIDAndName = false;
            this.cmbDoct.Location = new System.Drawing.Point(474, 40);
            this.cmbDoct.Name = "cmbDoct";
            this.cmbDoct.ShowCustomerList = false;
            this.cmbDoct.ShowID = false;
            this.cmbDoct.Size = new System.Drawing.Size(103, 20);
            this.cmbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoct.TabIndex = 10;
            this.cmbDoct.Tag = "";
            this.cmbDoct.ToolBarUse = false;
            this.cmbDoct.SelectedIndexChanged += new System.EventHandler(this.cmbDoct_SelectedIndexChanged);
            this.cmbDoct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDoct_KeyDown);
            // 
            // txtAlarm
            // 
            this.txtAlarm.BackColor = System.Drawing.Color.White;
            this.txtAlarm.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlarm.IsEnter2Tab = false;
            this.txtAlarm.Location = new System.Drawing.Point(647, 39);
            this.txtAlarm.Name = "txtAlarm";
            this.txtAlarm.ReadOnly = true;
            this.txtAlarm.Size = new System.Drawing.Size(137, 22);
            this.txtAlarm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAlarm.TabIndex = 12;
            this.txtAlarm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAlarm
            // 
            this.lblAlarm.AutoSize = true;
            this.lblAlarm.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAlarm.Location = new System.Drawing.Point(595, 42);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(53, 13);
            this.lblAlarm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblAlarm.TabIndex = 11;
            this.lblAlarm.Text = "警戒线:";
            // 
            // txtLeft
            // 
            this.txtLeft.BackColor = System.Drawing.Color.White;
            this.txtLeft.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLeft.IsEnter2Tab = false;
            this.txtLeft.Location = new System.Drawing.Point(827, 38);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.ReadOnly = true;
            this.txtLeft.Size = new System.Drawing.Size(99, 22);
            this.txtLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLeft.TabIndex = 14;
            this.txtLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLeft.Location = new System.Drawing.Point(790, 42);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(40, 13);
            this.lblLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLeft.TabIndex = 13;
            this.lblLeft.Text = "余额:";
            // 
            // txtPact
            // 
            this.txtPact.BackColor = System.Drawing.Color.White;
            this.txtPact.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPact.IsEnter2Tab = false;
            this.txtPact.Location = new System.Drawing.Point(647, 9);
            this.txtPact.Name = "txtPact";
            this.txtPact.ReadOnly = true;
            this.txtPact.Size = new System.Drawing.Size(137, 22);
            this.txtPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPact.TabIndex = 6;
            this.txtPact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtInDept
            // 
            this.txtInDept.BackColor = System.Drawing.Color.White;
            this.txtInDept.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInDept.IsEnter2Tab = false;
            this.txtInDept.Location = new System.Drawing.Point(75, 38);
            this.txtInDept.Name = "txtInDept";
            this.txtInDept.ReadOnly = true;
            this.txtInDept.Size = new System.Drawing.Size(137, 22);
            this.txtInDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInDept.TabIndex = 8;
            this.txtInDept.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblInDept
            // 
            this.lblInDept.AutoSize = true;
            this.lblInDept.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInDept.Location = new System.Drawing.Point(10, 42);
            this.lblInDept.Name = "lblInDept";
            this.lblInDept.Size = new System.Drawing.Size(66, 13);
            this.lblInDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInDept.TabIndex = 7;
            this.lblInDept.Text = "住院科室:";
            // 
            // txtInTime
            // 
            this.txtInTime.BackColor = System.Drawing.Color.White;
            this.txtInTime.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInTime.IsEnter2Tab = false;
            this.txtInTime.Location = new System.Drawing.Point(474, 9);
            this.txtInTime.Name = "txtInTime";
            this.txtInTime.ReadOnly = true;
            this.txtInTime.Size = new System.Drawing.Size(103, 22);
            this.txtInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInTime.TabIndex = 4;
            this.txtInTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblInTime
            // 
            this.lblInTime.AutoSize = true;
            this.lblInTime.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInTime.Location = new System.Drawing.Point(409, 13);
            this.lblInTime.Name = "lblInTime";
            this.lblInTime.Size = new System.Drawing.Size(66, 13);
            this.lblInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInTime.TabIndex = 3;
            this.lblInTime.Text = "入院时间:";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(244, 13);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(40, 13);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 1;
            this.lbName.Text = "姓名:";
            // 
            // ucQueryPatientInfo
            // 
            this.ucQueryPatientInfo.DefaultInputType = 0;
            this.ucQueryPatientInfo.InputType = 0;
            this.ucQueryPatientInfo.IsDeptOnly = true;
            this.ucQueryPatientInfo.Location = new System.Drawing.Point(14, 5);
            this.ucQueryPatientInfo.Name = "ucQueryPatientInfo";
            this.ucQueryPatientInfo.PatientInState = "ALL";
            this.ucQueryPatientInfo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.AfterArrived;
            this.ucQueryPatientInfo.Size = new System.Drawing.Size(198, 27);
            this.ucQueryPatientInfo.TabIndex = 0;
            this.ucQueryPatientInfo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryPatientInfo_myEvent);
            // 
            // ucInpatientCharge_new1
            // 
            this.ucInpatientCharge_new1.DefaultExeDept = null;
            this.ucInpatientCharge_new1.DefaultExeDeptIsDeptIn = false;
            this.ucInpatientCharge_new1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInpatientCharge_new1.DrugStorageDept = FS.HISFC.Components.Common.Controls.EnumDrugStorageDept.CurrentLoginDept;
            this.ucInpatientCharge_new1.IsJudgeQty = true;
            this.ucInpatientCharge_new1.IsJudgeValid = false;
            this.ucInpatientCharge_new1.IsShowGroupTree = true;
            this.ucInpatientCharge_new1.IsSplitUndrugCombItem = false;
            this.ucInpatientCharge_new1.Location = new System.Drawing.Point(0, 96);
            this.ucInpatientCharge_new1.MessageType = FS.HISFC.Models.Base.MessType.Y;
            this.ucInpatientCharge_new1.Name = "ucInpatientCharge_new1";
            this.ucInpatientCharge_new1.RecipeDoctCode = null;
            this.ucInpatientCharge_new1.RowCount = 0;
            this.ucInpatientCharge_new1.Size = new System.Drawing.Size(941, 520);
            this.ucInpatientCharge_new1.TabIndex = 1;
            this.ucInpatientCharge_new1.加载项目类别 = FS.HISFC.Components.Common.Controls.EnumShowItemType.All;
            this.ucInpatientCharge_new1.控件功能 = FS.HISFC.Components.Common.Controls.ucInpatientCharge_new.FeeTypes.收费;
            // 
            // ucCharge_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucInpatientCharge_new1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCharge_new";
            this.Size = new System.Drawing.Size(941, 616);
            this.Load += new System.EventHandler(this.ucCharge_new_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInTime;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInDept;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtLeft;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblLeft;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAlarm;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblAlarm;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoct;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDoct;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPact;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private System.Windows.Forms.Label label1;
        private FS.HISFC.Components.Common.Controls.ucInpatientCharge_new ucInpatientCharge_new1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAge;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBedNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}
