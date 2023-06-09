﻿namespace FS.HISFC.Components.Common.Controls
{
    partial class ucDiagNoseInput
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpEnter1;
        private FarPoint.Win.Spread.SheetView fpEnter1_Sheet1;
        //private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem2;
        private FS.HISFC.Components.Common.Controls.ucDiagnose ucDiagnose1;
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
            this.fpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter();
            this.fpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.ucDiagnose1 = new FS.HISFC.Components.Common.Controls.ucDiagnose();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpEnter1
            // 
            this.fpEnter1.About = "2.5.2007.2005";
            this.fpEnter1.AccessibleDescription = "fpEnter1, Sheet1, Row 0, Column 0, ";
            this.fpEnter1.BackColor = System.Drawing.SystemColors.Control;
            this.fpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            this.fpEnter1.Location = new System.Drawing.Point(0, 0);
            this.fpEnter1.Name = "fpEnter1";
            this.fpEnter1.SelectNone = false;
            this.fpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEnter1_Sheet1});
            this.fpEnter1.ShowListWhenOfFocus = false;
            this.fpEnter1.Size = new System.Drawing.Size(896, 558);
            this.fpEnter1.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEnter1.TextTipAppearance = tipAppearance2;
            this.fpEnter1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpEnter1_EditChange);
            this.fpEnter1.EditModeOn += new System.EventHandler(this.fpEnter1_EditModeOn);
            // 
            // fpEnter1_Sheet1
            // 
            this.fpEnter1_Sheet1.Reset();
            this.fpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEnter1_Sheet1.ColumnCount = 17;
            this.fpEnter1_Sheet1.RowCount = 0;
            this.fpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "诊断类别";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ICD10";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "诊断名称";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "出院情况";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "有无手术";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "30种疾病";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "病理符合";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "分期";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "分级";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "是否疑诊";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "主诊断";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "序号";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "诊断日期";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "入院日期";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "出院日期";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "诊断医师代码";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "诊断医师";
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 42F;
            this.fpEnter1_Sheet1.Columns.Get(15).Label = "诊断医师代码";
            this.fpEnter1_Sheet1.Columns.Get(15).Width = 88F;
            this.fpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpEnter1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpEnter1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpEnter1.SetViewportLeftColumn(0, 3);
            this.fpEnter1.SetActiveViewport(1, 0);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = -1;
            this.menuItem2.Text = "";
            // 
            // ucDiagnose1
            // 
            this.ucDiagnose1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ucDiagnose1.Location = new System.Drawing.Point(152, 176);
            this.ucDiagnose1.Name = "ucDiagnose1";
            this.ucDiagnose1.Size = new System.Drawing.Size(392, 312);
            this.ucDiagnose1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btAdd);
            this.panel1.Controls.Add(this.btDelete);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 558);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 42);
            this.panel1.TabIndex = 2;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(500, 16);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "增加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(597, 16);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 1;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(681, 16);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // ucDiagNoseInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ucDiagnose1);
            this.Controls.Add(this.fpEnter1);
            this.Controls.Add(this.panel1);
            this.Name = "ucDiagNoseInput";
            this.Size = new System.Drawing.Size(896, 600);
            this.Load += new System.EventHandler(this.ucDiagNoseInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btSave;
    }
}
