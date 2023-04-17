namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    partial class ucTreeViewChooseList
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
            this.TreeView = new FS.SOC.HISFC.Components.Common.Base.baseTreeView();
            this.ngbChooseList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).BeginInit();
            this.neuPaneFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ngbChooseList
            // 
            this.ngbChooseList.Size = new System.Drawing.Size(302, 598);
            // 
            // neuDataListSpread
            // 
            this.neuDataListSpread.Size = new System.Drawing.Size(296, 541);
            // 
            // neuDataListSpread_Sheet1
            // 
            this.neuDataListSpread_Sheet1.Reset();
            this.neuDataListSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuDataListSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuDataListSpread_Sheet1.ColumnCount = 0;
            this.neuDataListSpread_Sheet1.RowCount = 0;
            this.neuDataListSpread_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default;
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuDataListSpread_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuDataListSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.RowHeader.Visible = false;
            this.neuDataListSpread_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Range;
            this.neuDataListSpread_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionRenderer;
            this.neuDataListSpread_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Cell;
            this.neuDataListSpread_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.SheetCornerStyle.Locked = false;
            this.neuDataListSpread_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.neuDataListSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuDataListSpread.SetActiveViewport(0, 1, 1);
            // 
            // ntxtFilter
            // 
            this.ntxtFilter.Size = new System.Drawing.Size(56, 21);
            // 
            // ncmbDrugType
            // 
            this.ncmbDrugType.Location = new System.Drawing.Point(177, 10);
            // 
            // nlbDrugType
            // 
            this.nlbDrugType.Location = new System.Drawing.Point(130, 14);
            // 
            // TreeView
            // 
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.HideSelection = false;
            this.TreeView.Location = new System.Drawing.Point(0, 0);
            this.TreeView.Name = "TreeView";
            this.TreeView.Size = new System.Drawing.Size(302, 598);
            this.TreeView.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.TreeView.TabIndex = 8;
            // 
            // ucTreeViewChooseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TreeView);
            this.Name = "ucTreeViewChooseList";
            this.Size = new System.Drawing.Size(302, 598);
            this.Controls.SetChildIndex(this.ngbChooseList, 0);
            this.Controls.SetChildIndex(this.TreeView, 0);
            this.ngbChooseList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).EndInit();
            this.neuPaneFilter.ResumeLayout(false);
            this.neuPaneFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public SOC.HISFC.Components.Common.Base.baseTreeView TreeView;


    }
}
