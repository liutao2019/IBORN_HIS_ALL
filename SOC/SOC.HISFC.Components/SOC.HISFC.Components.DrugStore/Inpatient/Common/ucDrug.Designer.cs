namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    partial class ucDrug
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
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryInpatientNO = new FS.SOC.HISFC.Components.Common.RADT.ucQueryInpatientNO();
            this.tvMessageBaseTree1 = new FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.tvMessageBaseTree(this.components);
            this.ntbDrugControl = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucDrugDetail1 = new FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.ucDrugDetail();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbPauseRefresh = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbAutoPrintRegister = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ngbQuerySet.SuspendLayout();
            this.npanelDrugMessage.SuspendLayout();
            this.ngbDrugDetail.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.ntbDrugControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Controls.Add(this.ntbDrugControl);
            this.ngbQuerySet.Controls.Add(this.ncbAutoPrintRegister);
            this.ngbQuerySet.Controls.Add(this.ncbPauseRefresh);
            this.ngbQuerySet.Controls.Add(this.nlbInfo);
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 521);
            this.ngbQuerySet.Size = new System.Drawing.Size(1067, 62);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 518);
            // 
            // npanelDrugMessage
            // 
            this.npanelDrugMessage.Controls.Add(this.tvMessageBaseTree1);
            this.npanelDrugMessage.Controls.Add(this.neuGroupBox3);
            this.npanelDrugMessage.Location = new System.Drawing.Point(0, 0);
            this.npanelDrugMessage.Size = new System.Drawing.Size(232, 518);
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Location = new System.Drawing.Point(232, 0);
            this.neuSplitter2.Size = new System.Drawing.Size(3, 518);
            // 
            // ngbDrugDetail
            // 
            this.ngbDrugDetail.Controls.Add(this.ucDrugDetail1);
            this.ngbDrugDetail.Location = new System.Drawing.Point(235, 0);
            this.ngbDrugDetail.Size = new System.Drawing.Size(832, 518);
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuLabel1);
            this.neuGroupBox3.Controls.Add(this.ucQueryInpatientNO);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(232, 65);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 2;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "摆药信息";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(55, 15);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(125, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 42;
            this.neuLabel1.Text = "列表内患者查询请输入";
            // 
            // ucQueryInpatientNO
            // 
            this.ucQueryInpatientNO.DefaultInputType = 0;
            this.ucQueryInpatientNO.InputType = 0;
            this.ucQueryInpatientNO.IsDeptOnly = false;
            this.ucQueryInpatientNO.Location = new System.Drawing.Point(11, 30);
            this.ucQueryInpatientNO.Name = "ucQueryInpatientNO";
            this.ucQueryInpatientNO.PatientInState = "ALL";
            this.ucQueryInpatientNO.ShowState = FS.SOC.HISFC.Components.Common.RADT.enuShowState.All;
            this.ucQueryInpatientNO.Size = new System.Drawing.Size(209, 27);
            this.ucQueryInpatientNO.TabIndex = 1;
            // 
            // tvMessageBaseTree1
            // 
            this.tvMessageBaseTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMessageBaseTree1.HideSelection = false;
            this.tvMessageBaseTree1.ImageIndex = 0;
            this.tvMessageBaseTree1.Location = new System.Drawing.Point(0, 65);
            this.tvMessageBaseTree1.Name = "tvMessageBaseTree1";
            this.tvMessageBaseTree1.SelectedImageIndex = 0;
            this.tvMessageBaseTree1.Size = new System.Drawing.Size(232, 453);
            this.tvMessageBaseTree1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvMessageBaseTree1.TabIndex = 34;
            // 
            // ntbDrugControl
            // 
            this.ntbDrugControl.Controls.Add(this.tabPage1);
            this.ntbDrugControl.Controls.Add(this.tabPage2);
            this.ntbDrugControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.ntbDrugControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.ntbDrugControl.Location = new System.Drawing.Point(4, 14);
            this.ntbDrugControl.Name = "ntbDrugControl";
            this.ntbDrugControl.SelectedIndex = 0;
            this.ntbDrugControl.Size = new System.Drawing.Size(200, 22);
            this.ntbDrugControl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbDrugControl.TabIndex = 44;
            this.ntbDrugControl.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucDrugDetail1
            // 
            this.ucDrugDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDrugDetail1.HightLightDept = "";
            this.ucDrugDetail1.Location = new System.Drawing.Point(3, 17);
            this.ucDrugDetail1.Name = "ucDrugDetail1";
            this.ucDrugDetail1.Size = new System.Drawing.Size(826, 498);
            this.ucDrugDetail1.TabIndex = 0;
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo.Location = new System.Drawing.Point(230, 40);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(197, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 41;
            this.nlbInfo.Text = "您当前选择的是：西药房口服摆药台";
            // 
            // ncbPauseRefresh
            // 
            this.ncbPauseRefresh.AutoSize = true;
            this.ncbPauseRefresh.Checked = true;
            this.ncbPauseRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbPauseRefresh.ForeColor = System.Drawing.Color.Blue;
            this.ncbPauseRefresh.Location = new System.Drawing.Point(22, 40);
            this.ncbPauseRefresh.Name = "ncbPauseRefresh";
            this.ncbPauseRefresh.Size = new System.Drawing.Size(96, 16);
            this.ncbPauseRefresh.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbPauseRefresh.TabIndex = 42;
            this.ncbPauseRefresh.Text = "暂停自动刷新";
            this.ncbPauseRefresh.UseVisualStyleBackColor = true;
            // 
            // ncbAutoPrintRegister
            // 
            this.ncbAutoPrintRegister.AutoSize = true;
            this.ncbAutoPrintRegister.ForeColor = System.Drawing.Color.Blue;
            this.ncbAutoPrintRegister.Location = new System.Drawing.Point(128, 40);
            this.ncbAutoPrintRegister.Name = "ncbAutoPrintRegister";
            this.ncbAutoPrintRegister.Size = new System.Drawing.Size(96, 16);
            this.ncbAutoPrintRegister.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbAutoPrintRegister.TabIndex = 43;
            this.ncbAutoPrintRegister.Text = "自动打印注册";
            this.ncbAutoPrintRegister.UseVisualStyleBackColor = true;
            // 
            // ucDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDrug";
            this.ngbQuerySet.ResumeLayout(false);
            this.ngbQuerySet.PerformLayout();
            this.npanelDrugMessage.ResumeLayout(false);
            this.ngbDrugDetail.ResumeLayout(false);
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.ntbDrugControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.tvMessageBaseTree tvMessageBaseTree1;
        private ucDrugDetail ucDrugDetail1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.SOC.HISFC.Components.Common.RADT.ucQueryInpatientNO ucQueryInpatientNO;
        private FS.FrameWork.WinForms.Controls.NeuTabControl ntbDrugControl;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbPauseRefresh;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbAutoPrintRegister;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
