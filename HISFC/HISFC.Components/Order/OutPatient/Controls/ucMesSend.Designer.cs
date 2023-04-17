namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucMesSend
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.fpOrder = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.btQuery = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpOrder
            // 
            this.fpOrder.About = "3.0.2004.2005";
            this.fpOrder.AccessibleDescription = "fpOrder, Sheet1, Row 0, Column 0, ";
            this.fpOrder.BackColor = System.Drawing.Color.White;
            this.fpOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpOrder.FileName = "";
            this.fpOrder.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpOrder.IsAutoSaveGridStatus = false;
            this.fpOrder.IsCanCustomConfigColumn = false;
            this.fpOrder.Location = new System.Drawing.Point(0, 64);
            this.fpOrder.Name = "fpOrder";
            this.fpOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpOrder_Sheet1});
            this.fpOrder.Size = new System.Drawing.Size(970, 192);
            this.fpOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpOrder.TabIndex = 6;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpOrder.TextTipAppearance = tipAppearance2;
            this.fpOrder.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpOrder_Sheet1
            // 
            this.fpOrder_Sheet1.Reset();
            this.fpOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpOrder_Sheet1.ColumnCount = 5;
            this.fpOrder_Sheet1.RowCount = 0;
            this.fpOrder_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "卡号";
            this.fpOrder_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "电话";
            this.fpOrder_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "医生姓名";
            this.fpOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "发送短信";
            this.fpOrder_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpOrder_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(0).Label = "卡号";
            this.fpOrder_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(0).Width = 119F;
            this.fpOrder_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(1).Label = "姓名";
            this.fpOrder_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(1).Width = 87F;
            this.fpOrder_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(2).Label = "电话";
            this.fpOrder_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(2).Width = 115F;
            this.fpOrder_Sheet1.Columns.Get(3).Label = "医生姓名";
            this.fpOrder_Sheet1.Columns.Get(3).Width = 121F;
            this.fpOrder_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(4).Label = "发送短信";
            this.fpOrder_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpOrder_Sheet1.Columns.Get(4).Width = 93F;
            this.fpOrder_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpOrder_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpOrder_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpOrder_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Sheet1.Rows.Default.Height = 25F;
            this.fpOrder_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpOrder_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // txtCardNo
            // 
            this.txtCardNo.ForeColor = System.Drawing.Color.Black;
            this.txtCardNo.Location = new System.Drawing.Point(81, 12);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(128, 21);
            this.txtCardNo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "卡号/姓名：";
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPatientInfo.Location = new System.Drawing.Point(6, 43);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(82, 14);
            this.lblPatientInfo.TabIndex = 6;
            this.lblPatientInfo.Text = "患者信息：";
            // 
            // btQuery
            // 
            this.btQuery.Location = new System.Drawing.Point(235, 12);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(75, 23);
            this.btQuery.TabIndex = 8;
            this.btQuery.Text = "查  询";
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btQuery);
            this.groupBox1.Controls.Add(this.lblPatientInfo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCardNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(970, 64);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // ucMesSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpOrder);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucMesSend";
            this.Size = new System.Drawing.Size(970, 256);
            this.Load += new System.EventHandler(this.ucOutPatientMessageSend_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuSpread fpOrder;
        protected FarPoint.Win.Spread.SheetView fpOrder_Sheet1;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Button btQuery;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
