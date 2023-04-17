namespace FS.WinForms.Report.Order
{
    partial class ucOrderShowForQuery
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
            this.lbBedName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbNurseCellName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.lblFreeCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPatientNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucOrderShow1 = new FS.HISFC.Components.Order.Controls.ucOrderShow();
            this.tvInhos = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbBedName);
            this.panel1.Controls.Add(this.lbNurseCellName);
            this.panel1.Controls.Add(this.ucQueryInpatientNo);
            this.panel1.Controls.Add(this.lblFreeCost);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblDept);
            this.panel1.Controls.Add(this.lblPatientNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 29);
            this.panel1.TabIndex = 0;
            // 
            // lbBedName
            // 
            this.lbBedName.AutoSize = true;
            this.lbBedName.Location = new System.Drawing.Point(651, 12);
            this.lbBedName.Name = "lbBedName";
            this.lbBedName.Size = new System.Drawing.Size(41, 12);
            this.lbBedName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbBedName.TabIndex = 5;
            this.lbBedName.Text = "床号：";
            // 
            // lbNurseCellName
            // 
            this.lbNurseCellName.AutoSize = true;
            this.lbNurseCellName.Location = new System.Drawing.Point(768, 12);
            this.lbNurseCellName.Name = "lbNurseCellName";
            this.lbNurseCellName.Size = new System.Drawing.Size(41, 12);
            this.lbNurseCellName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbNurseCellName.TabIndex = 4;
            this.lbNurseCellName.Text = "病区：";
            // 
            // ucQueryInpatientNo
            // 
            this.ucQueryInpatientNo.DefaultInputType = 0;
            this.ucQueryInpatientNo.InputType = 1;
            this.ucQueryInpatientNo.IsDeptOnly = true;
            this.ucQueryInpatientNo.Location = new System.Drawing.Point(385, 27);
            this.ucQueryInpatientNo.Name = "ucQueryInpatientNo";
            this.ucQueryInpatientNo.PatientInState = "ALL";
            this.ucQueryInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo.Size = new System.Drawing.Size(201, 27);
            this.ucQueryInpatientNo.TabIndex = 1;
            this.ucQueryInpatientNo.Visible = false;
            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo_myEvent);
            // 
            // lblFreeCost
            // 
            this.lblFreeCost.AutoSize = true;
            this.lblFreeCost.Location = new System.Drawing.Point(523, 12);
            this.lblFreeCost.Name = "lblFreeCost";
            this.lblFreeCost.Size = new System.Drawing.Size(65, 12);
            this.lblFreeCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblFreeCost.TabIndex = 3;
            this.lblFreeCost.Text = "可用余额：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(23, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(65, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 0;
            this.lblName.Text = "患者姓名：";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(368, 12);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(65, 12);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 2;
            this.lblDept.Text = "住院科室：";
            // 
            // lblPatientNO
            // 
            this.lblPatientNO.AutoSize = true;
            this.lblPatientNO.Location = new System.Drawing.Point(223, 12);
            this.lblPatientNO.Name = "lblPatientNO";
            this.lblPatientNO.Size = new System.Drawing.Size(53, 12);
            this.lblPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientNO.TabIndex = 1;
            this.lblPatientNO.Text = "住院号：";
            // 
            // ucOrderShow1
            // 
            this.ucOrderShow1.AutoQuitFeeApply = false;
            this.ucOrderShow1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucOrderShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderShow1.IsAutoSaveColumnProperty = true;
            this.ucOrderShow1.IsCheckOrder = false;
            this.ucOrderShow1.IsFoldSubtblShow = false;
            this.ucOrderShow1.IsFullConvertToHalf = true;
            this.ucOrderShow1.IsPrint = false;
            this.ucOrderShow1.IsShowAll = true;
            this.ucOrderShow1.Location = new System.Drawing.Point(0, 0);
            this.ucOrderShow1.Name = "ucOrderShow1";
            this.ucOrderShow1.ParentFormToolBar = null;
            this.ucOrderShow1.Size = new System.Drawing.Size(730, 487);
            this.ucOrderShow1.TabIndex = 1;
            // 
            // tvInhos
            // 
            this.tvInhos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvInhos.Location = new System.Drawing.Point(0, 0);
            this.tvInhos.Name = "tvInhos";
            this.tvInhos.Size = new System.Drawing.Size(202, 487);
            this.tvInhos.TabIndex = 0;
            this.tvInhos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvInhos_AfterSelect);
            this.tvInhos.Click += new System.EventHandler(this.tvInhos_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvInhos);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucOrderShow1);
            this.splitContainer1.Size = new System.Drawing.Size(936, 487);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 2;
            // 
            // ucOrderShowQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "ucOrderShowQuery";
            this.Size = new System.Drawing.Size(936, 516);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FS.HISFC.Components.Order.Controls.ucOrderShow ucOrderShow1;
        protected FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbBedName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbNurseCellName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblFreeCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientNO;
        private System.Windows.Forms.TreeView tvInhos;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
