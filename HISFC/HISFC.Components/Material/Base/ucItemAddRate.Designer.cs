namespace FS.HISFC.Components.Material.Base
{
    partial class ucItemAddRate
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.FpMetItemAddRule = new FS.FrameWork.WinForms.Controls.NeuFpEnter();
            this.FpMetItemAddRule_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ucMaterialItemList1 = new FS.HISFC.Components.Material.Base.ucMaterialItemList();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpMetItemAddRule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpMetItemAddRule_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ucMaterialItemList1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(228, 498);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(228, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 498);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.FpMetItemAddRule);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(231, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(463, 498);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // FpMetItemAddRule
            // 
            this.FpMetItemAddRule.About = "2.5.2007.2005";
            this.FpMetItemAddRule.AccessibleDescription = "FpMetItemAddRule";
            this.FpMetItemAddRule.BackColor = System.Drawing.SystemColors.Control;
            this.FpMetItemAddRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpMetItemAddRule.EditModePermanent = true;
            this.FpMetItemAddRule.EditModeReplace = true;
            this.FpMetItemAddRule.Location = new System.Drawing.Point(0, 0);
            this.FpMetItemAddRule.Name = "FpMetItemAddRule";
            this.FpMetItemAddRule.SelectNone = false;
            this.FpMetItemAddRule.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpMetItemAddRule_Sheet1});
            this.FpMetItemAddRule.ShowListWhenOfFocus = false;
            this.FpMetItemAddRule.Size = new System.Drawing.Size(463, 498);
            this.FpMetItemAddRule.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FpMetItemAddRule.TextTipAppearance = tipAppearance1;
            this.FpMetItemAddRule.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FpMetItemAddRule_EditChange);
            // 
            // FpMetItemAddRule_Sheet1
            // 
            this.FpMetItemAddRule_Sheet1.Reset();
            this.FpMetItemAddRule_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpMetItemAddRule_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpMetItemAddRule_Sheet1.ColumnCount = 9;
            this.FpMetItemAddRule_Sheet1.RowCount = 0;
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "���ʱ���";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "��������";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "���";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "�۸�";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "��λ";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "�Ӽ۷�ʽ";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "�Ӽ���";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "���ӷ�";
            this.FpMetItemAddRule_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Status";
            this.FpMetItemAddRule_Sheet1.Columns.Get(0).Label = "���ʱ���";
            this.FpMetItemAddRule_Sheet1.Columns.Get(0).Locked = true;
            this.FpMetItemAddRule_Sheet1.Columns.Get(1).Label = "��������";
            this.FpMetItemAddRule_Sheet1.Columns.Get(1).Width = 100F;
            this.FpMetItemAddRule_Sheet1.Columns.Get(2).Label = "���";
            this.FpMetItemAddRule_Sheet1.Columns.Get(2).Width = 80F;
            this.FpMetItemAddRule_Sheet1.Columns.Get(3).Label = "�۸�";
            this.FpMetItemAddRule_Sheet1.Columns.Get(3).Width = 80F;
            this.FpMetItemAddRule_Sheet1.Columns.Get(5).Label = "�Ӽ۷�ʽ";
            this.FpMetItemAddRule_Sheet1.Columns.Get(5).Width = 100F;
            this.FpMetItemAddRule_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpMetItemAddRule_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.FpMetItemAddRule_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.FpMetItemAddRule_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.FpMetItemAddRule.SetActiveViewport(1, 0);
            // 
            // ucMaterialItemList1
            // 
            this.ucMaterialItemList1.DataTable = null;
            this.ucMaterialItemList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMaterialItemList1.FilterField = null;
            this.ucMaterialItemList1.Location = new System.Drawing.Point(0, 0);
            this.ucMaterialItemList1.Name = "ucMaterialItemList1";
            this.ucMaterialItemList1.ShowStop = false;
            this.ucMaterialItemList1.ShowTreeView = false;
            this.ucMaterialItemList1.Size = new System.Drawing.Size(228, 498);
            this.ucMaterialItemList1.TabIndex = 0;
            // 
            // ucItemAddRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucItemAddRate";
            this.Size = new System.Drawing.Size(694, 498);
            this.Load += new System.EventHandler(this.ucItemAddRate_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FpMetItemAddRule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpMetItemAddRule_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private ucMaterialItemList ucMaterialItemList1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        
        private FS.FrameWork.WinForms.Controls.NeuFpEnter FpMetItemAddRule;
        private FarPoint.Win.Spread.SheetView FpMetItemAddRule_Sheet1;
        
        
    }
}
