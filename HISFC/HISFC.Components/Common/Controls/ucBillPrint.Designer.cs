namespace FS.HISFC.Components.Common.Controls
{
    partial class ucBillPrint
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
            this.gbQuery = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblInvioceNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtBegTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.gbTreeView = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.trvPatients = new System.Windows.Forms.TreeView();
            this.gbTreeButtom = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.gbTreeTop = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.plReport = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSplitter2 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.gbBillInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbNurse = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ucQueryInfo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.gbQuery.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.gbTreeView.SuspendLayout();
            this.plReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbQuery
            // 
            this.gbQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.gbQuery.Controls.Add(this.panel2);
            this.gbQuery.Controls.Add(this.panel1);
            this.gbQuery.Controls.Add(this.ucQueryInfo);
            this.gbQuery.Controls.Add(this.panel4);
            this.gbQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbQuery.Location = new System.Drawing.Point(0, 0);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Size = new System.Drawing.Size(1022, 58);
            this.gbQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQuery.TabIndex = 4;
            this.gbQuery.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbNurse);
            this.panel2.Controls.Add(this.neuLabel1);
            this.panel2.Location = new System.Drawing.Point(732, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 35);
            this.panel2.TabIndex = 8;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.neuLabel1.ForeColor = System.Drawing.Color.Black;
            this.neuLabel1.Location = new System.Drawing.Point(18, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 3;
            this.neuLabel1.Text = "护士站：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblInvioceNo);
            this.panel1.Location = new System.Drawing.Point(246, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 35);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 3;
            this.label1.Text = "发票号：";
            // 
            // lblInvioceNo
            // 
            this.lblInvioceNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInvioceNo.IsEnter2Tab = false;
            this.lblInvioceNo.Location = new System.Drawing.Point(77, 8);
            this.lblInvioceNo.Name = "lblInvioceNo";
            this.lblInvioceNo.Size = new System.Drawing.Size(125, 21);
            this.lblInvioceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInvioceNo.TabIndex = 4;
            this.lblInvioceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lblInvioceNo_KeyDown);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dtBegTime);
            this.panel4.Controls.Add(this.neuLabel3);
            this.panel4.Location = new System.Drawing.Point(489, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(214, 35);
            this.panel4.TabIndex = 7;
            // 
            // dtBegTime
            // 
            this.dtBegTime.IsEnter2Tab = false;
            this.dtBegTime.Location = new System.Drawing.Point(73, 8);
            this.dtBegTime.Name = "dtBegTime";
            this.dtBegTime.Size = new System.Drawing.Size(129, 21);
            this.dtBegTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegTime.TabIndex = 11;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.neuLabel3.ForeColor = System.Drawing.Color.Black;
            this.neuLabel3.Location = new System.Drawing.Point(28, 13);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 3;
            this.neuLabel3.Text = "日期：";
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.gbTreeView);
            this.pLeft.Controls.Add(this.gbTreeButtom);
            this.pLeft.Controls.Add(this.gbTreeTop);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLeft.Location = new System.Drawing.Point(0, 58);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(210, 512);
            this.pLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pLeft.TabIndex = 6;
            this.pLeft.Visible = false;
            // 
            // gbTreeView
            // 
            this.gbTreeView.Controls.Add(this.trvPatients);
            this.gbTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTreeView.Location = new System.Drawing.Point(0, 0);
            this.gbTreeView.Name = "gbTreeView";
            this.gbTreeView.Size = new System.Drawing.Size(210, 512);
            this.gbTreeView.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTreeView.TabIndex = 5;
            this.gbTreeView.TabStop = false;
            // 
            // trvPatients
            // 
            this.trvPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvPatients.Location = new System.Drawing.Point(3, 17);
            this.trvPatients.Name = "trvPatients";
            this.trvPatients.Size = new System.Drawing.Size(204, 492);
            this.trvPatients.TabIndex = 1;
            this.trvPatients.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvPatients_AfterSelect);
            this.trvPatients.Click += new System.EventHandler(this.trvPatients_Click);
            // 
            // gbTreeButtom
            // 
            this.gbTreeButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbTreeButtom.Location = new System.Drawing.Point(0, 512);
            this.gbTreeButtom.Name = "gbTreeButtom";
            this.gbTreeButtom.Size = new System.Drawing.Size(210, 0);
            this.gbTreeButtom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTreeButtom.TabIndex = 4;
            this.gbTreeButtom.TabStop = false;
            // 
            // gbTreeTop
            // 
            this.gbTreeTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTreeTop.Location = new System.Drawing.Point(0, 0);
            this.gbTreeTop.Name = "gbTreeTop";
            this.gbTreeTop.Size = new System.Drawing.Size(210, 0);
            this.gbTreeTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTreeTop.TabIndex = 3;
            this.gbTreeTop.TabStop = false;
            // 
            // plReport
            // 
            this.plReport.Controls.Add(this.neuSplitter2);
            this.plReport.Controls.Add(this.gbBillInfo);
            this.plReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plReport.Location = new System.Drawing.Point(210, 58);
            this.plReport.Name = "plReport";
            this.plReport.Size = new System.Drawing.Size(812, 512);
            this.plReport.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plReport.TabIndex = 7;
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuSplitter2.Location = new System.Drawing.Point(0, 509);
            this.neuSplitter2.Name = "neuSplitter2";
            this.neuSplitter2.Size = new System.Drawing.Size(812, 3);
            this.neuSplitter2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter2.TabIndex = 11;
            this.neuSplitter2.TabStop = false;
            // 
            // gbBillInfo
            // 
            this.gbBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBillInfo.Location = new System.Drawing.Point(0, 0);
            this.gbBillInfo.Name = "gbBillInfo";
            this.gbBillInfo.Size = new System.Drawing.Size(812, 512);
            this.gbBillInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbBillInfo.TabIndex = 12;
            this.gbBillInfo.TabStop = false;
            // 
            // cmbNurse
            // 
            this.cmbNurse.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbNurse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbNurse.FormattingEnabled = true;
            this.cmbNurse.IsEnter2Tab = false;
            this.cmbNurse.IsFlat = false;
            this.cmbNurse.IsLike = true;
            this.cmbNurse.IsListOnly = false;
            this.cmbNurse.IsPopForm = true;
            this.cmbNurse.IsShowCustomerList = false;
            this.cmbNurse.IsShowID = false;
            this.cmbNurse.IsShowIDAndName = false;
            this.cmbNurse.Location = new System.Drawing.Point(67, 9);
            this.cmbNurse.Name = "cmbNurse";
            this.cmbNurse.ShowCustomerList = false;
            this.cmbNurse.ShowID = false;
            this.cmbNurse.Size = new System.Drawing.Size(144, 20);
            this.cmbNurse.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbNurse.TabIndex = 11;
            this.cmbNurse.Tag = "";
            this.cmbNurse.ToolBarUse = false;
            this.cmbNurse.SelectedIndexChanged += new System.EventHandler(this.cmbNurse_SelectedIndexChanged);
            // 
            // ucQueryInfo
            // 
            this.ucQueryInfo.DefaultInputType = 0;
            this.ucQueryInfo.InputType = 0;
            this.ucQueryInfo.IsDeptOnly = true;
            this.ucQueryInfo.Location = new System.Drawing.Point(14, 17);
            this.ucQueryInfo.Name = "ucQueryInfo";
            this.ucQueryInfo.PatientInState = "ALL";
            this.ucQueryInfo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInfo.Size = new System.Drawing.Size(175, 27);
            this.ucQueryInfo.TabIndex = 5;
            // 
            // ucBillPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.Controls.Add(this.plReport);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.gbQuery);
            this.Name = "ucBillPrint";
            this.Size = new System.Drawing.Size(1022, 570);
            this.Load += new System.EventHandler(this.ucBillPrint_Load);
            this.gbQuery.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.gbTreeView.ResumeLayout(false);
            this.plReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbQuery;
        public FS.FrameWork.WinForms.Controls.NeuTextBox lblInvioceNo;
        public FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private ucQueryInpatientNo ucQueryInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegTime;
        private FS.FrameWork.WinForms.Controls.NeuPanel pLeft;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTreeView;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTreeButtom;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTreeTop;
        private FS.FrameWork.WinForms.Controls.NeuPanel plReport;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbBillInfo;
        private System.Windows.Forms.TreeView trvPatients;
        private System.Windows.Forms.Panel panel2;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbNurse;
    }
}
