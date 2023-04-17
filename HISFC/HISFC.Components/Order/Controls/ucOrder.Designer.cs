namespace FS.HISFC.Components.Order.Controls
{
    partial class ucOrder
    {
        //{D62064E7-6482-4fd4-9E1B-142D3008EB4E}
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
                try
                {
                    this.dataSet.Clear();
                    this.dataSet.Dispose();

                    this.dataSet = null;//最后将dsItem指为null;

                    this.dsAllLong.Clear();
                    this.dsAllLong.Dispose();

                    this.dsAllLong = null;

                    this.dsAllShort.Clear();
                    this.dsAllShort.Dispose();

                    this.dsAllShort = null;
                }
                catch { }
                System.GC.Collect();
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.ucItemSelect1 = new FS.HISFC.Components.Order.Controls.ucItemSelect();
            this.fpOrder = new FarPoint.Win.Spread.FpSpread();
            this.fpOrder_Long = new FarPoint.Win.Spread.SheetView();
            this.fpOrder_Short = new FarPoint.Win.Spread.SheetView();
            this.neuLinkLabel1 = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.panelOrder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDisplay = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnItemInfo = new System.Windows.Forms.Panel();
            this.txtItemInfo = new System.Windows.Forms.RichTextBox();
            this.pnPatient = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPatientLabel1 = new FS.HISFC.Components.Common.Controls.ucPatientLabel();
            this.cbxPatientInfo = new System.Windows.Forms.CheckBox();
            this.lbPatient = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Long)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Short)).BeginInit();
            this.panelOrder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnItemInfo.SuspendLayout();
            this.pnPatient.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucItemSelect1
            // 
            this.ucItemSelect1.BackColor = System.Drawing.Color.White;
            this.ucItemSelect1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucItemSelect1.Location = new System.Drawing.Point(0, 55);
            this.ucItemSelect1.LongOrShort = 0;
            this.ucItemSelect1.Name = "ucItemSelect1";
            this.ucItemSelect1.Size = new System.Drawing.Size(1131, 67);
            this.ucItemSelect1.TabIndex = 0;
            // 
            // fpOrder
            // 
            this.fpOrder.About = "3.0.2004.2005";
            this.fpOrder.AccessibleDescription = "fpOrder, 临时医嘱, Row 0, Column 0, ";
            this.fpOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.fpOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpOrder.Location = new System.Drawing.Point(0, 14);
            this.fpOrder.Name = "fpOrder";
            this.fpOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpOrder_Long,
            this.fpOrder_Short});
            this.fpOrder.Size = new System.Drawing.Size(1131, 190);
            this.fpOrder.TabIndex = 1;
            this.fpOrder.TabStrip.ActiveSheetTab.Font = new System.Drawing.Font("宋体", 10F);
            this.fpOrder.TabStrip.ActiveSheetTab.Size = -1;
            this.fpOrder.TabStrip.DefaultSheetTab.Font = new System.Drawing.Font("宋体", 10F);
            this.fpOrder.TabStrip.DefaultSheetTab.Size = -1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpOrder.TextTipAppearance = tipAppearance1;
            this.fpOrder.ActiveSheetChanged += new System.EventHandler(this.fpOrder_ActiveSheetChanged);
            this.fpOrder.ActiveSheetIndex = 1;
            // 
            // fpOrder_Long
            // 
            this.fpOrder_Long.Reset();
            this.fpOrder_Long.SheetName = "长期医嘱";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpOrder_Long.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpOrder_Long.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpOrder_Long.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Long.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Long.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpOrder_Long.RowHeader.Columns.Default.Resizable = true;
            this.fpOrder_Long.RowHeader.Columns.Get(0).Width = 37F;
            this.fpOrder_Long.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Long.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Long.Rows.Default.Height = 25F;
            this.fpOrder_Long.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Long.SheetCornerStyle.Parent = "CornerDefault";
            this.fpOrder_Long.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpOrder_Long.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpOrder_Short
            // 
            this.fpOrder_Short.Reset();
            this.fpOrder_Short.SheetName = "临时医嘱";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpOrder_Short.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpOrder_Short.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpOrder_Short.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Short.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Short.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpOrder_Short.RowHeader.Columns.Default.Resizable = true;
            this.fpOrder_Short.RowHeader.Columns.Get(0).Width = 37F;
            this.fpOrder_Short.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Short.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Short.Rows.Default.Height = 25F;
            this.fpOrder_Short.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Short.SheetCornerStyle.Parent = "CornerDefault";
            this.fpOrder_Short.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpOrder_Short.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuLinkLabel1
            // 
            this.neuLinkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.neuLinkLabel1.AutoSize = true;
            this.neuLinkLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.neuLinkLabel1.Location = new System.Drawing.Point(221, 188);
            this.neuLinkLabel1.Name = "neuLinkLabel1";
            this.neuLinkLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLinkLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLinkLabel1.TabIndex = 2;
            this.neuLinkLabel1.TabStop = true;
            this.neuLinkLabel1.Text = "保存格式";
            this.neuLinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panelOrder
            // 
            this.panelOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelOrder.Controls.Add(this.panel1);
            this.panelOrder.Controls.Add(this.pnItemInfo);
            this.panelOrder.Controls.Add(this.ucItemSelect1);
            this.panelOrder.Controls.Add(this.pnPatient);
            this.panelOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrder.Location = new System.Drawing.Point(0, 0);
            this.panelOrder.Name = "panelOrder";
            this.panelOrder.Size = new System.Drawing.Size(1131, 405);
            this.panelOrder.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuLinkLabel1);
            this.panel1.Controls.Add(this.fpOrder);
            this.panel1.Controls.Add(this.lblDisplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1131, 204);
            this.panel1.TabIndex = 7;
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDisplay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDisplay.ForeColor = System.Drawing.Color.Red;
            this.lblDisplay.Location = new System.Drawing.Point(0, 0);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(37, 14);
            this.lblDisplay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDisplay.TabIndex = 1;
            this.lblDisplay.Text = "姓名";
            // 
            // pnItemInfo
            // 
            this.pnItemInfo.BackColor = System.Drawing.Color.AliceBlue;
            this.pnItemInfo.Controls.Add(this.txtItemInfo);
            this.pnItemInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnItemInfo.Location = new System.Drawing.Point(0, 326);
            this.pnItemInfo.Name = "pnItemInfo";
            this.pnItemInfo.Size = new System.Drawing.Size(1131, 79);
            this.pnItemInfo.TabIndex = 4;
            this.pnItemInfo.Visible = false;
            // 
            // txtItemInfo
            // 
            this.txtItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItemInfo.ForeColor = System.Drawing.Color.Blue;
            this.txtItemInfo.Location = new System.Drawing.Point(0, 0);
            this.txtItemInfo.Name = "txtItemInfo";
            this.txtItemInfo.Size = new System.Drawing.Size(1131, 79);
            this.txtItemInfo.TabIndex = 1;
            this.txtItemInfo.Text = "";
            // 
            // pnPatient
            // 
            this.pnPatient.BackColor = System.Drawing.Color.AliceBlue;
            this.pnPatient.Controls.Add(this.ucPatientLabel1);
            this.pnPatient.Controls.Add(this.cbxPatientInfo);
            this.pnPatient.Controls.Add(this.lbPatient);
            this.pnPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnPatient.Location = new System.Drawing.Point(0, 0);
            this.pnPatient.Name = "pnPatient";
            this.pnPatient.Size = new System.Drawing.Size(1131, 55);
            this.pnPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnPatient.TabIndex = 3;
            // 
            // ucPatientLabel1
            // 
            this.ucPatientLabel1.Location = new System.Drawing.Point(644, 0);
            this.ucPatientLabel1.Name = "ucPatientLabel1";
            this.ucPatientLabel1.Size = new System.Drawing.Size(487, 55);
            this.ucPatientLabel1.TabIndex = 2;
            // 
            // cbxPatientInfo
            // 
            this.cbxPatientInfo.AutoSize = true;
            this.cbxPatientInfo.Checked = true;
            this.cbxPatientInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxPatientInfo.Location = new System.Drawing.Point(554, 3);
            this.cbxPatientInfo.Name = "cbxPatientInfo";
            this.cbxPatientInfo.Size = new System.Drawing.Size(82, 18);
            this.cbxPatientInfo.TabIndex = 1;
            this.cbxPatientInfo.Text = "更多信息";
            this.cbxPatientInfo.UseVisualStyleBackColor = true;
            // 
            // lbPatient
            // 
            this.lbPatient.AutoSize = true;
            this.lbPatient.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPatient.Location = new System.Drawing.Point(2, 5);
            this.lbPatient.Name = "lbPatient";
            this.lbPatient.Size = new System.Drawing.Size(35, 56);
            this.lbPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPatient.TabIndex = 0;
            this.lbPatient.Text = "姓名\r\ndfd\r\nadf\r\n打发";
            // 
            // ucOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelOrder);
            this.Name = "ucOrder";
            this.Size = new System.Drawing.Size(1131, 405);
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Long)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Short)).EndInit();
            this.panelOrder.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnItemInfo.ResumeLayout(false);
            this.pnPatient.ResumeLayout(false);
            this.pnPatient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ucItemSelect ucItemSelect1;
        public FarPoint.Win.Spread.FpSpread fpOrder;
        private FarPoint.Win.Spread.SheetView fpOrder_Long;
        private FarPoint.Win.Spread.SheetView fpOrder_Short;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel neuLinkLabel1;
        private System.Windows.Forms.Panel panelOrder;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnPatient;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPatient;
        private System.Windows.Forms.Panel pnItemInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDisplay;
        private System.Windows.Forms.RichTextBox txtItemInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbxPatientInfo;
        private FS.HISFC.Components.Common.Controls.ucPatientLabel ucPatientLabel1;



    }
}
