namespace FS.HISFC.Components.Terminal.Confirm
{
    partial class ucCancelInpatientConfirm
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.txtPact);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.txtName);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.ucQueryInpatientNo1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(685, 68);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel4.Location = new System.Drawing.Point(17, 44);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(173, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "��ɫ��ɫ������ȷ����ɵ���Ŀ";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Red;
            this.neuLabel3.Location = new System.Drawing.Point(236, 44);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(173, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "�����ɫ������ȷ��ȡ������Ŀ";
            // 
            // txtPact
            // 
            this.txtPact.Enabled = false;
            this.txtPact.IsEnter2Tab = false;
            this.txtPact.Location = new System.Drawing.Point(481, 15);
            this.txtPact.Name = "txtPact";
            this.txtPact.Size = new System.Drawing.Size(100, 21);
            this.txtPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPact.TabIndex = 4;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(419, 20);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "��ͬ��λ:";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(290, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(236, 20);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(35, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "����:";
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.IsDeptOnly = true;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(13, 11);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.InHos;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 0;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Controls.Add(this.fpSpread1);
            this.neuPanel2.Controls.Add(this.splitter1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 68);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(685, 489);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 3);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(685, 226);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 3;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 15;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ҽ����";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ҽ��ִ����ˮ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "��ˮ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "��Ŀ����";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "��Ŀ����";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "ȷ������";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "��λ";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ȷ����";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ȷ�Ͽ���";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ȷ��ʱ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "������";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "��������ˮ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "ִ���豸";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "ִ�м�ʦ";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "���";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "ҽ����";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "ҽ��ִ����ˮ��";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 67F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "��Ŀ����";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 81F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��Ŀ����";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 86F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "ȷ��ʱ��";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 98F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "������";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 76F;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 25F;
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetViewportLeftColumn(0, 0, 2);
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(0, 229);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(685, 260);
            this.fpSpread1.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 14;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ҽ����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ҽ����ˮ��";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "��ˮ��";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "��Ŀ����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "��Ŀ����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "ȡ������";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "��λ";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ȷ����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ȷ�Ͽ���";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ȷ��ʱ��";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "������";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "��������ˮ��";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "ִ����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "ִ���豸";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "ҽ����";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 68F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "ҽ����ˮ��";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 71F;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "��Ŀ����";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 135F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "ȡ������";
            this.fpSpread1_Sheet1.Columns.Get(5).Locked = false;
            this.fpSpread1_Sheet1.Columns.Get(9).Label = "ȷ��ʱ��";
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 92F;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 25F;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(685, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // ucCancelInpatientConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCancelInpatientConfirm";
            this.Size = new System.Drawing.Size(685, 557);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Splitter splitter1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
    }
}
