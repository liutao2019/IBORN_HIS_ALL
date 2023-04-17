namespace FS.WinForms.Report.Order
{
    partial class ucOrderShowQuery
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
            this.ucOrderShow1 = new FS.HISFC.Components.Order.Controls.ucOrderShow();
            this.ucQueryInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.lbBedName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbNurseCellName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblFreeCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPatientNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(776, 79);
            this.panel1.TabIndex = 0;
            // 
            // ucOrderShow1
            // 
            this.ucOrderShow1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucOrderShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderShow1.IsPrint = false;
            this.ucOrderShow1.Location = new System.Drawing.Point(0, 79);
            this.ucOrderShow1.Name = "ucOrderShow1";
            this.ucOrderShow1.Size = new System.Drawing.Size(776, 437);
            this.ucOrderShow1.TabIndex = 1;
            // 
            // ucQueryInpatientNo
            // 
            this.ucQueryInpatientNo.InputType = 0;
            this.ucQueryInpatientNo.Location = new System.Drawing.Point(14, 13);
            this.ucQueryInpatientNo.Name = "ucQueryInpatientNo";
            this.ucQueryInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo.Size = new System.Drawing.Size(183, 27);
            this.ucQueryInpatientNo.TabIndex = 1;
            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo_myEvent);
            // 
            // lbBedName
            // 
            this.lbBedName.AutoSize = true;
            this.lbBedName.Location = new System.Drawing.Point(620, 49);
            this.lbBedName.Name = "lbBedName";
            this.lbBedName.Size = new System.Drawing.Size(41, 12);
            this.lbBedName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbBedName.TabIndex = 5;
            this.lbBedName.Text = "床号：";
            // 
            // lbNurseCellName
            // 
            this.lbNurseCellName.AutoSize = true;
            this.lbNurseCellName.Location = new System.Drawing.Point(515, 49);
            this.lbNurseCellName.Name = "lbNurseCellName";
            this.lbNurseCellName.Size = new System.Drawing.Size(41, 12);
            this.lbNurseCellName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbNurseCellName.TabIndex = 4;
            this.lbNurseCellName.Text = "病区：";
            // 
            // lblFreeCost
            // 
            this.lblFreeCost.AutoSize = true;
            this.lblFreeCost.Location = new System.Drawing.Point(398, 49);
            this.lblFreeCost.Name = "lblFreeCost";
            this.lblFreeCost.Size = new System.Drawing.Size(65, 12);
            this.lblFreeCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblFreeCost.TabIndex = 3;
            this.lblFreeCost.Text = "可用余额：";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(267, 49);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(65, 12);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 2;
            this.lblDept.Text = "住院科室：";
            // 
            // lblPatientNO
            // 
            this.lblPatientNO.AutoSize = true;
            this.lblPatientNO.Location = new System.Drawing.Point(148, 49);
            this.lblPatientNO.Name = "lblPatientNO";
            this.lblPatientNO.Size = new System.Drawing.Size(53, 12);
            this.lblPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientNO.TabIndex = 1;
            this.lblPatientNO.Text = "住院号：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(17, 49);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(65, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 0;
            this.lblName.Text = "患者姓名：";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucOrderShow1);
            this.Controls.Add(this.panel1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(776, 516);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}
