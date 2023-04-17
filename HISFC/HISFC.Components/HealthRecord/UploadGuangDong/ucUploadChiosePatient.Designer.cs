namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
	partial class ucUploadChiosePatient
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbCaseBase = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSucceedUploadNum = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatientNo = new System.Windows.Forms.TextBox();
            this.cbInputPatientNo = new System.Windows.Forms.CheckBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbCaseBase);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lblSucceedUploadNum);
            this.splitContainer1.Panel1.Controls.Add(this.txtPatientNo);
            this.splitContainer1.Panel1.Controls.Add(this.cbInputPatientNo);
            this.splitContainer1.Panel1.Controls.Add(this.neuLabel3);
            this.splitContainer1.Panel1.Controls.Add(this.neuLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.dtEnd);
            this.splitContainer1.Panel1.Controls.Add(this.dtBegin);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.neuFpEnter1);
            this.splitContainer1.Size = new System.Drawing.Size(770, 558);
            this.splitContainer1.SplitterDistance = 114;
            this.splitContainer1.TabIndex = 0;
            // 
            // cbCaseBase
            // 
            this.cbCaseBase.AutoSize = true;
            this.cbCaseBase.Checked = true;
            this.cbCaseBase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaseBase.Location = new System.Drawing.Point(603, 55);
            this.cbCaseBase.Name = "cbCaseBase";
            this.cbCaseBase.Size = new System.Drawing.Size(96, 16);
            this.cbCaseBase.TabIndex = 17;
            this.cbCaseBase.Text = "上传首页数据";
            this.cbCaseBase.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "温馨提示：天蓝色记录为填写了病案首页记录！";
            // 
            // lblSucceedUploadNum
            // 
            this.lblSucceedUploadNum.AutoSize = true;
            this.lblSucceedUploadNum.Location = new System.Drawing.Point(295, 84);
            this.lblSucceedUploadNum.Name = "lblSucceedUploadNum";
            this.lblSucceedUploadNum.Size = new System.Drawing.Size(0, 12);
            this.lblSucceedUploadNum.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSucceedUploadNum.TabIndex = 15;
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.Location = new System.Drawing.Point(247, 18);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.Size = new System.Drawing.Size(517, 21);
            this.txtPatientNo.TabIndex = 14;
            this.txtPatientNo.Visible = false;
            // 
            // cbInputPatientNo
            // 
            this.cbInputPatientNo.AutoSize = true;
            this.cbInputPatientNo.Location = new System.Drawing.Point(7, 20);
            this.cbInputPatientNo.Name = "cbInputPatientNo";
            this.cbInputPatientNo.Size = new System.Drawing.Size(222, 16);
            this.cbInputPatientNo.TabIndex = 13;
            this.cbInputPatientNo.Text = "输入住院号上传，住院号以“,”隔开";
            this.cbInputPatientNo.UseVisualStyleBackColor = true;
            this.cbInputPatientNo.CheckedChanged += new System.EventHandler(this.cbInputPatientNo_CheckedChanged);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(301, 56);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 12;
            this.neuLabel3.Text = "结束时间：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(12, 56);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(89, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 11;
            this.neuLabel2.Text = "出院开始时间：";
            // 
            // dtEnd
            // 
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(372, 52);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(164, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 10;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "";
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(118, 52);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(166, 21);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 9;
            // 
            // neuFpEnter1
            // 
            this.neuFpEnter1.About = "3.0.2004.2005";
            this.neuFpEnter1.AccessibleDescription = "neuFpEnter1, Sheet1";
            this.neuFpEnter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.neuFpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuFpEnter1.EditModePermanent = true;
            this.neuFpEnter1.EditModeReplace = true;
            this.neuFpEnter1.Location = new System.Drawing.Point(0, 0);
            this.neuFpEnter1.Name = "neuFpEnter1";
            this.neuFpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuFpEnter1.SelectNone = false;
            this.neuFpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuFpEnter1_Sheet1});
            this.neuFpEnter1.ShowListWhenOfFocus = false;
            this.neuFpEnter1.Size = new System.Drawing.Size(770, 440);
            this.neuFpEnter1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuFpEnter1.TextTipAppearance = tipAppearance1;
            // 
            // neuFpEnter1_Sheet1
            // 
            this.neuFpEnter1_Sheet1.Reset();
            this.neuFpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuFpEnter1_Sheet1.ColumnCount = 8;
            this.neuFpEnter1_Sheet1.RowCount = 0;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "住院号";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "入院科室";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "出院科室";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "出院日期";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "管床医生";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "上传标志";
            this.neuFpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuFpEnter1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 34F;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "住院号";
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 79F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "姓名";
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 62F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "出院科室";
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 107F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "出院日期";
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 83F;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "管床医生";
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 62F;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "上传标志";
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 65F;
            this.neuFpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuFpEnter1.SetActiveViewport(0, 1, 0);
            // 
            // ucUploadChiosePatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucUploadChiosePatient";
            this.Size = new System.Drawing.Size(770, 558);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
        private System.Windows.Forms.TextBox txtPatientNo;
        private System.Windows.Forms.CheckBox cbInputPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSucceedUploadNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbCaseBase;
	}
}
