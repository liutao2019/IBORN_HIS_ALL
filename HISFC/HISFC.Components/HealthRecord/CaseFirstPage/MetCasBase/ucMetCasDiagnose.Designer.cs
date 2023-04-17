namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    partial class ucMetCasDiagnose
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpEnter1;
        private FarPoint.Win.Spread.SheetView fpEnter1_Sheet1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem2;
        //private Common.Controls.ucDiagnose ucDiagnose1;
        private FS.HISFC.Components.HealthRecord.CaseFirstPage.ucDiagnose ucDiagnose1;

        private System.ComponentModel.IContainer components=null;

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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMetCasDiagnose));
            this.fpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.ucDiagnose1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucDiagnose();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rtxtDiag = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btInsert = new System.Windows.Forms.Button();
            this.lblTipMessage = new System.Windows.Forms.Label();
            this.lblTipMessage2 = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpEnter1
            // 
            this.fpEnter1.About = "3.0.2004.2005";
            this.fpEnter1.AccessibleDescription = "fpEnter1, Sheet1, Row 0, Column 0, ";
            this.fpEnter1.BackColor = System.Drawing.SystemColors.Control;
            this.fpEnter1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            this.fpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEnter1.Location = new System.Drawing.Point(0, 0);
            this.fpEnter1.Name = "fpEnter1";
            this.fpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpEnter1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpEnter1.SelectNone = false;
            this.fpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEnter1_Sheet1});
            this.fpEnter1.ShowListWhenOfFocus = false;
            this.fpEnter1.Size = new System.Drawing.Size(710, 550);
            this.fpEnter1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEnter1.TextTipAppearance = tipAppearance1;
            this.fpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEnter1.EditModeOn += new System.EventHandler(this.fpEnter1_EditModeOn);
            this.fpEnter1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpEnter1_EditChange);
            // 
            // fpEnter1_Sheet1
            // 
            this.fpEnter1_Sheet1.Reset();
            this.fpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEnter1_Sheet1.ColumnCount = 501;
            this.fpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "诊断类别";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ICD10";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "诊断名称";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "入院病情";
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
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "是否拟诊";
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 42F;
            this.fpEnter1_Sheet1.Columns.Get(15).Label = "诊断医师代码";
            this.fpEnter1_Sheet1.Columns.Get(15).Width = 88F;
            this.fpEnter1_Sheet1.Columns.Get(17).Label = "是否拟诊";
            this.fpEnter1_Sheet1.Columns.Get(17).Width = 80F;
            this.fpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpEnter1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpEnter1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpEnter1_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.fpEnter1_Sheet1_CellChanged);
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "删除";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // ucDiagnose1
            // 
            this.ucDiagnose1.AlDiag = ((System.Collections.ArrayList)(resources.GetObject("ucDiagnose1.AlDiag")));
            this.ucDiagnose1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ucDiagnose1.Location = new System.Drawing.Point(162, 81);
            this.ucDiagnose1.Name = "ucDiagnose1";
            this.ucDiagnose1.Size = new System.Drawing.Size(392, 312);
            this.ucDiagnose1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 600);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(896, 550);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(896, 550);
            this.panel4.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.ucDiagnose1);
            this.panel6.Controls.Add(this.fpEnter1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(710, 550);
            this.panel6.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rtxtDiag);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(710, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(186, 550);
            this.panel5.TabIndex = 2;
            // 
            // rtxtDiag
            // 
            this.rtxtDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtDiag.Location = new System.Drawing.Point(0, 0);
            this.rtxtDiag.Name = "rtxtDiag";
            this.rtxtDiag.Size = new System.Drawing.Size(186, 550);
            this.rtxtDiag.TabIndex = 0;
            this.rtxtDiag.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btInsert);
            this.panel2.Controls.Add(this.lblTipMessage);
            this.panel2.Controls.Add(this.lblTipMessage2);
            this.panel2.Controls.Add(this.btDelete);
            this.panel2.Controls.Add(this.btAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 50);
            this.panel2.TabIndex = 2;
            // 
            // btInsert
            // 
            this.btInsert.Location = new System.Drawing.Point(192, 14);
            this.btInsert.Name = "btInsert";
            this.btInsert.Size = new System.Drawing.Size(75, 28);
            this.btInsert.TabIndex = 4;
            this.btInsert.Text = "插入行";
            this.btInsert.UseVisualStyleBackColor = true;
            this.btInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // lblTipMessage
            // 
            this.lblTipMessage.AutoSize = true;
            this.lblTipMessage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTipMessage.ForeColor = System.Drawing.Color.Red;
            this.lblTipMessage.Location = new System.Drawing.Point(340, 11);
            this.lblTipMessage.Name = "lblTipMessage";
            this.lblTipMessage.Size = new System.Drawing.Size(392, 12);
            this.lblTipMessage.TabIndex = 3;
            this.lblTipMessage.Text = "温馨提示：1、自动将ICD诊断名称填写到\"诊断名称\"上,但允许修改";
            // 
            // lblTipMessage2
            // 
            this.lblTipMessage2.AutoSize = true;
            this.lblTipMessage2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTipMessage2.ForeColor = System.Drawing.Color.Red;
            this.lblTipMessage2.Location = new System.Drawing.Point(400, 32);
            this.lblTipMessage2.Name = "lblTipMessage2";
            this.lblTipMessage2.Size = new System.Drawing.Size(246, 12);
            this.lblTipMessage2.TabIndex = 2;
            this.lblTipMessage2.Text = "2、右边内容为，电子病历出院志出院诊断";
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(106, 14);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 28);
            this.btDelete.TabIndex = 1;
            this.btDelete.Text = "删除行";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(20, 14);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 28);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "增加行";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Visible = false;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // ucMetCasDiagnose
            // 
            this.ContextMenu = this.contextMenu1;
            this.Controls.Add(this.panel1);
            this.Name = "ucMetCasDiagnose";
            this.Size = new System.Drawing.Size(896, 600);
            this.Load += new System.EventHandler(this.ucDiagNoseInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtxtDiag;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label lblTipMessage2;
        private System.Windows.Forms.Label lblTipMessage;
        private System.Windows.Forms.Button btInsert;
    }
}
