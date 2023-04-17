namespace  FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucOutPatientCaseQuery
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOutPatientCaseQuery));
            this.lbltitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.lblDept = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCardNO = new System.Windows.Forms.Label();
            this.lblTips5 = new System.Windows.Forms.Label();
            this.lblTips4 = new System.Windows.Forms.Label();
            this.lblTips3 = new System.Windows.Forms.Label();
            this.lblTips2 = new System.Windows.Forms.Label();
            this.lblTips1 = new System.Windows.Forms.Label();
            this.tbChiefComplaint = new System.Windows.Forms.TextBox();
            this.lblTips6 = new System.Windows.Forms.Label();
            this.lblTips7 = new System.Windows.Forms.Label();
            this.lblTips12 = new System.Windows.Forms.Label();
            this.lblTips11 = new System.Windows.Forms.Label();
            this.lblTips10 = new System.Windows.Forms.Label();
            this.lblTips9 = new System.Windows.Forms.Label();
            this.lblTips8 = new System.Windows.Forms.Label();
            this.lbmemo2 = new System.Windows.Forms.Label();
            this.tbPresentIllness = new System.Windows.Forms.TextBox();
            this.tbPastHistory = new System.Windows.Forms.TextBox();
            this.tbAllergicHistory = new System.Windows.Forms.TextBox();
            this.tbPhysicalExam = new System.Windows.Forms.TextBox();
            this.tbMemo = new System.Windows.Forms.TextBox();
            this.tbTreatment = new System.Windows.Forms.TextBox();
            this.tbmemo2 = new System.Windows.Forms.TextBox();
            this.tvHistoryCase = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fpHistory = new FarPoint.Win.Spread.FpSpread();
            this.fpDiagnose_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.Panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDiagnose_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbltitle
            // 
            this.lbltitle.BackColor = System.Drawing.Color.Transparent;
            this.lbltitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbltitle.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltitle.ForeColor = System.Drawing.Color.Blue;
            this.lbltitle.Location = new System.Drawing.Point(8, 8);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(782, 47);
            this.lbltitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbltitle.TabIndex = 2;
            this.lbltitle.Text = "门诊电子病历";
            this.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel2
            // 
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.lblDept);
            this.Panel2.Controls.Add(this.lblAge);
            this.Panel2.Controls.Add(this.lblSex);
            this.Panel2.Controls.Add(this.lblName);
            this.Panel2.Controls.Add(this.lblCardNO);
            this.Panel2.Controls.Add(this.lblTips5);
            this.Panel2.Controls.Add(this.lblTips4);
            this.Panel2.Controls.Add(this.lblTips3);
            this.Panel2.Controls.Add(this.lblTips2);
            this.Panel2.Controls.Add(this.lblTips1);
            this.Panel2.Location = new System.Drawing.Point(8, 56);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(782, 49);
            this.Panel2.TabIndex = 12;
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(648, 28);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(76, 15);
            this.lblDept.TabIndex = 9;
            this.lblDept.Text = "普外科   ";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(391, 28);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(94, 15);
            this.lblAge.TabIndex = 8;
            this.lblAge.Text = "1岁        ";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(77, 28);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(86, 15);
            this.lblSex.TabIndex = 7;
            this.lblSex.Text = "男______  ";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(391, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(151, 15);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Micheal Jackson   ";
            // 
            // lblCardNO
            // 
            this.lblCardNO.AutoSize = true;
            this.lblCardNO.Font = new System.Drawing.Font("宋体", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNO.ForeColor = System.Drawing.Color.Blue;
            this.lblCardNO.Location = new System.Drawing.Point(77, 6);
            this.lblCardNO.Name = "lblCardNO";
            this.lblCardNO.Size = new System.Drawing.Size(97, 15);
            this.lblCardNO.TabIndex = 5;
            this.lblCardNO.Text = "0000000001";
            // 
            // lblTips5
            // 
            this.lblTips5.AutoSize = true;
            this.lblTips5.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips5.Location = new System.Drawing.Point(587, 28);
            this.lblTips5.Name = "lblTips5";
            this.lblTips5.Size = new System.Drawing.Size(39, 15);
            this.lblTips5.TabIndex = 4;
            this.lblTips5.Text = "科室";
            // 
            // lblTips4
            // 
            this.lblTips4.AutoSize = true;
            this.lblTips4.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips4.Location = new System.Drawing.Point(330, 28);
            this.lblTips4.Name = "lblTips4";
            this.lblTips4.Size = new System.Drawing.Size(39, 15);
            this.lblTips4.TabIndex = 3;
            this.lblTips4.Text = "年龄";
            // 
            // lblTips3
            // 
            this.lblTips3.AutoSize = true;
            this.lblTips3.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips3.Location = new System.Drawing.Point(7, 28);
            this.lblTips3.Name = "lblTips3";
            this.lblTips3.Size = new System.Drawing.Size(39, 15);
            this.lblTips3.TabIndex = 2;
            this.lblTips3.Text = "性别";
            // 
            // lblTips2
            // 
            this.lblTips2.AutoSize = true;
            this.lblTips2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips2.Location = new System.Drawing.Point(330, 6);
            this.lblTips2.Name = "lblTips2";
            this.lblTips2.Size = new System.Drawing.Size(39, 15);
            this.lblTips2.TabIndex = 1;
            this.lblTips2.Text = "姓名";
            // 
            // lblTips1
            // 
            this.lblTips1.AutoSize = true;
            this.lblTips1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips1.ForeColor = System.Drawing.Color.Blue;
            this.lblTips1.Location = new System.Drawing.Point(7, 6);
            this.lblTips1.Name = "lblTips1";
            this.lblTips1.Size = new System.Drawing.Size(55, 15);
            this.lblTips1.TabIndex = 0;
            this.lblTips1.Text = "病历号";
            // 
            // tbChiefComplaint
            // 
            this.tbChiefComplaint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbChiefComplaint.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbChiefComplaint.Location = new System.Drawing.Point(158, 108);
            this.tbChiefComplaint.Multiline = true;
            this.tbChiefComplaint.Name = "tbChiefComplaint";
            this.tbChiefComplaint.Size = new System.Drawing.Size(632, 34);
            this.tbChiefComplaint.TabIndex = 14;
            // 
            // lblTips6
            // 
            this.lblTips6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips6.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips6.ForeColor = System.Drawing.Color.Blue;
            this.lblTips6.Location = new System.Drawing.Point(8, 108);
            this.lblTips6.Margin = new System.Windows.Forms.Padding(0);
            this.lblTips6.Name = "lblTips6";
            this.lblTips6.Size = new System.Drawing.Size(150, 34);
            this.lblTips6.TabIndex = 13;
            this.lblTips6.Text = "主诉";
            this.lblTips6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTips7
            // 
            this.lblTips7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips7.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips7.ForeColor = System.Drawing.Color.Blue;
            this.lblTips7.Location = new System.Drawing.Point(8, 142);
            this.lblTips7.Margin = new System.Windows.Forms.Padding(0);
            this.lblTips7.Name = "lblTips7";
            this.lblTips7.Size = new System.Drawing.Size(150, 34);
            this.lblTips7.TabIndex = 18;
            this.lblTips7.Text = "现病史";
            this.lblTips7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTips12
            // 
            this.lblTips12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips12.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips12.ForeColor = System.Drawing.Color.Blue;
            this.lblTips12.Location = new System.Drawing.Point(8, 312);
            this.lblTips12.Margin = new System.Windows.Forms.Padding(0);
            this.lblTips12.Name = "lblTips12";
            this.lblTips12.Size = new System.Drawing.Size(150, 34);
            this.lblTips12.TabIndex = 20;
            this.lblTips12.Text = "治疗方案";
            this.lblTips12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTips11
            // 
            this.lblTips11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips11.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips11.ForeColor = System.Drawing.Color.Blue;
            this.lblTips11.Location = new System.Drawing.Point(8, 278);
            this.lblTips11.Margin = new System.Windows.Forms.Padding(0);
            this.lblTips11.Name = "lblTips11";
            this.lblTips11.Size = new System.Drawing.Size(150, 34);
            this.lblTips11.TabIndex = 19;
            this.lblTips11.Text = "嘱托";
            this.lblTips11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTips10
            // 
            this.lblTips10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips10.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips10.ForeColor = System.Drawing.Color.Blue;
            this.lblTips10.Location = new System.Drawing.Point(8, 244);
            this.lblTips10.Margin = new System.Windows.Forms.Padding(0);
            this.lblTips10.Name = "lblTips10";
            this.lblTips10.Size = new System.Drawing.Size(150, 34);
            this.lblTips10.TabIndex = 15;
            this.lblTips10.Text = "查体";
            this.lblTips10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTips9
            // 
            this.lblTips9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips9.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips9.ForeColor = System.Drawing.Color.Blue;
            this.lblTips9.Location = new System.Drawing.Point(8, 210);
            this.lblTips9.Margin = new System.Windows.Forms.Padding(0);
            this.lblTips9.Name = "lblTips9";
            this.lblTips9.Size = new System.Drawing.Size(150, 34);
            this.lblTips9.TabIndex = 16;
            this.lblTips9.Text = "过敏史";
            this.lblTips9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTips8
            // 
            this.lblTips8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips8.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips8.ForeColor = System.Drawing.Color.Blue;
            this.lblTips8.Location = new System.Drawing.Point(8, 176);
            this.lblTips8.Margin = new System.Windows.Forms.Padding(0);
            this.lblTips8.Name = "lblTips8";
            this.lblTips8.Size = new System.Drawing.Size(150, 34);
            this.lblTips8.TabIndex = 17;
            this.lblTips8.Text = "既往史";
            this.lblTips8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbmemo2
            // 
            this.lbmemo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbmemo2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbmemo2.ForeColor = System.Drawing.Color.Blue;
            this.lbmemo2.Location = new System.Drawing.Point(8, 346);
            this.lbmemo2.Margin = new System.Windows.Forms.Padding(0);
            this.lbmemo2.Name = "lbmemo2";
            this.lbmemo2.Size = new System.Drawing.Size(150, 34);
            this.lbmemo2.TabIndex = 21;
            this.lbmemo2.Text = "备注";
            this.lbmemo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPresentIllness
            // 
            this.tbPresentIllness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPresentIllness.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPresentIllness.Location = new System.Drawing.Point(158, 142);
            this.tbPresentIllness.Multiline = true;
            this.tbPresentIllness.Name = "tbPresentIllness";
            this.tbPresentIllness.Size = new System.Drawing.Size(632, 34);
            this.tbPresentIllness.TabIndex = 22;
            // 
            // tbPastHistory
            // 
            this.tbPastHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPastHistory.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPastHistory.Location = new System.Drawing.Point(158, 176);
            this.tbPastHistory.Multiline = true;
            this.tbPastHistory.Name = "tbPastHistory";
            this.tbPastHistory.Size = new System.Drawing.Size(632, 34);
            this.tbPastHistory.TabIndex = 23;
            // 
            // tbAllergicHistory
            // 
            this.tbAllergicHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAllergicHistory.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAllergicHistory.Location = new System.Drawing.Point(158, 210);
            this.tbAllergicHistory.Multiline = true;
            this.tbAllergicHistory.Name = "tbAllergicHistory";
            this.tbAllergicHistory.Size = new System.Drawing.Size(632, 34);
            this.tbAllergicHistory.TabIndex = 24;
            // 
            // tbPhysicalExam
            // 
            this.tbPhysicalExam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPhysicalExam.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhysicalExam.Location = new System.Drawing.Point(158, 244);
            this.tbPhysicalExam.Multiline = true;
            this.tbPhysicalExam.Name = "tbPhysicalExam";
            this.tbPhysicalExam.Size = new System.Drawing.Size(632, 34);
            this.tbPhysicalExam.TabIndex = 25;
            // 
            // tbMemo
            // 
            this.tbMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMemo.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMemo.Location = new System.Drawing.Point(158, 278);
            this.tbMemo.Multiline = true;
            this.tbMemo.Name = "tbMemo";
            this.tbMemo.Size = new System.Drawing.Size(632, 34);
            this.tbMemo.TabIndex = 26;
            // 
            // tbTreatment
            // 
            this.tbTreatment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTreatment.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTreatment.Location = new System.Drawing.Point(158, 312);
            this.tbTreatment.Multiline = true;
            this.tbTreatment.Name = "tbTreatment";
            this.tbTreatment.Size = new System.Drawing.Size(632, 34);
            this.tbTreatment.TabIndex = 27;
            // 
            // tbmemo2
            // 
            this.tbmemo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbmemo2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbmemo2.Location = new System.Drawing.Point(158, 346);
            this.tbmemo2.Multiline = true;
            this.tbmemo2.Name = "tbmemo2";
            this.tbmemo2.Size = new System.Drawing.Size(632, 34);
            this.tbmemo2.TabIndex = 28;
            // 
            // tvHistoryCase
            // 
            this.tvHistoryCase.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvHistoryCase.HideSelection = false;
            this.tvHistoryCase.Location = new System.Drawing.Point(792, 8);
            this.tvHistoryCase.Name = "tvHistoryCase";
            this.tvHistoryCase.Size = new System.Drawing.Size(223, 372);
            this.tvHistoryCase.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvHistoryCase.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fpHistory);
            this.panel1.Location = new System.Drawing.Point(8, 386);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 260);
            this.panel1.TabIndex = 32;
            // 
            // fpHistory
            // 
            this.fpHistory.About = "3.0.2004.2005";
            this.fpHistory.AccessibleDescription = "fpHistory, Sheet1, Row 0, Column 0, ";
            this.fpHistory.BackColor = System.Drawing.Color.Honeydew;
            this.fpHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpHistory.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpHistory.Location = new System.Drawing.Point(0, 0);
            this.fpHistory.Name = "fpHistory";
            this.fpHistory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpHistory.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpDiagnose_Sheet1});
            this.fpHistory.Size = new System.Drawing.Size(1007, 257);
            this.fpHistory.TabIndex = 10;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpHistory.TextTipAppearance = tipAppearance1;
            this.fpHistory.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpDiagnose_Sheet1
            // 
            this.fpDiagnose_Sheet1.Reset();
            this.fpDiagnose_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpDiagnose_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpDiagnose_Sheet1.ColumnCount = 9;
            this.fpDiagnose_Sheet1.RowCount = 0;
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "诊断类型";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "描述";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ICD10";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "诊断名称";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "疑诊";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "初诊";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "诊断日期";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "诊断医生代码";
            this.fpDiagnose_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "诊断医生";
            this.fpDiagnose_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDiagnose_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDiagnose_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpDiagnose_Sheet1.Columns.Get(0).Label = "诊断类型";
            this.fpDiagnose_Sheet1.Columns.Get(0).Width = 90F;
            this.fpDiagnose_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.fpDiagnose_Sheet1.Columns.Get(1).Label = "描述";
            this.fpDiagnose_Sheet1.Columns.Get(1).Width = 37F;
            this.fpDiagnose_Sheet1.Columns.Get(3).Label = "诊断名称";
            this.fpDiagnose_Sheet1.Columns.Get(3).Width = 176F;
            this.fpDiagnose_Sheet1.Columns.Get(4).CellType = checkBoxCellType2;
            this.fpDiagnose_Sheet1.Columns.Get(4).Label = "疑诊";
            this.fpDiagnose_Sheet1.Columns.Get(5).CellType = checkBoxCellType3;
            this.fpDiagnose_Sheet1.Columns.Get(5).Label = "初诊";
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2020, 6, 18, 15, 41, 14, 509);
            dateTimeCellType1.TimeDefault = new System.DateTime(2020, 6, 18, 15, 41, 14, 509);
            this.fpDiagnose_Sheet1.Columns.Get(6).CellType = dateTimeCellType1;
            this.fpDiagnose_Sheet1.Columns.Get(6).Label = "诊断日期";
            this.fpDiagnose_Sheet1.Columns.Get(6).Width = 120F;
            this.fpDiagnose_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDiagnose_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpDiagnose_Sheet1.RowHeader.Columns.Get(0).Width = 27F;
            this.fpDiagnose_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDiagnose_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDiagnose_Sheet1.Rows.Default.Height = 25F;
            this.fpDiagnose_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDiagnose_Sheet1.SheetCornerStyle.Locked = false;
            this.fpDiagnose_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpDiagnose_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpDiagnose_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucOutPatientCaseQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvHistoryCase);
            this.Controls.Add(this.tbmemo2);
            this.Controls.Add(this.tbTreatment);
            this.Controls.Add(this.tbMemo);
            this.Controls.Add(this.tbPhysicalExam);
            this.Controls.Add(this.tbAllergicHistory);
            this.Controls.Add(this.tbPastHistory);
            this.Controls.Add(this.tbPresentIllness);
            this.Controls.Add(this.lblTips7);
            this.Controls.Add(this.lblTips12);
            this.Controls.Add(this.lblTips11);
            this.Controls.Add(this.lblTips10);
            this.Controls.Add(this.lblTips9);
            this.Controls.Add(this.lblTips8);
            this.Controls.Add(this.lbmemo2);
            this.Controls.Add(this.tbChiefComplaint);
            this.Controls.Add(this.lblTips6);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.lbltitle);
            this.Name = "ucOutPatientCaseQuery";
            this.Size = new System.Drawing.Size(1041, 649);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDiagnose_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel lbltitle;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCardNO;
        private System.Windows.Forms.Label lblTips5;
        private System.Windows.Forms.Label lblTips4;
        private System.Windows.Forms.Label lblTips3;
        private System.Windows.Forms.Label lblTips2;
        private System.Windows.Forms.Label lblTips1;
        private System.Windows.Forms.TextBox tbChiefComplaint;
        private System.Windows.Forms.Label lblTips6;
        private System.Windows.Forms.Label lblTips7;
        private System.Windows.Forms.Label lblTips12;
        private System.Windows.Forms.Label lblTips11;
        private System.Windows.Forms.Label lblTips10;
        private System.Windows.Forms.Label lblTips9;
        private System.Windows.Forms.Label lblTips8;
        private System.Windows.Forms.Label lbmemo2;
        private System.Windows.Forms.TextBox tbPresentIllness;
        private System.Windows.Forms.TextBox tbPastHistory;
        private System.Windows.Forms.TextBox tbAllergicHistory;
        private System.Windows.Forms.TextBox tbPhysicalExam;
        private System.Windows.Forms.TextBox tbMemo;
        private System.Windows.Forms.TextBox tbTreatment;
        private System.Windows.Forms.TextBox tbmemo2;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvHistoryCase;
        private System.Windows.Forms.Panel panel1;
        private FarPoint.Win.Spread.FpSpread fpHistory;
        private FarPoint.Win.Spread.SheetView fpDiagnose_Sheet1;

    }
}
