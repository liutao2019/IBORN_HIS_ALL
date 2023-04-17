using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient
{
    public partial class frmQueryPatientInfoNew 
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.tcMain = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tbpageName = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCardID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbSexF = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbSexM = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbDimness = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbpagePatientNo = new System.Windows.Forms.TabPage();
            this.txtPatientNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btSearch = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtSame = new System.Windows.Forms.RadioButton();
            this.rbtLike = new System.Windows.Forms.RadioButton();
            this.neuCheckBox2 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btNoSelect = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btSelect = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.spNewPatientInfo = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.sheetNewPatient = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNewPatient = new System.Windows.Forms.Label();
            this.pnButton = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tcMain.SuspendLayout();
            this.tbpageName.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbpagePatientNo.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spNewPatientInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetNewPatient)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbpageName);
            this.tcMain.Controls.Add(this.tbpagePatientNo);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(381, 129);
            this.tcMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tcMain.TabIndex = 0;
            // 
            // tbpageName
            // 
            this.tbpageName.Controls.Add(this.label3);
            this.tbpageName.Controls.Add(this.txtCardID);
            this.tbpageName.Controls.Add(this.neuLabel4);
            this.tbpageName.Controls.Add(this.groupBox3);
            this.tbpageName.Controls.Add(this.label2);
            this.tbpageName.Controls.Add(this.label1);
            this.tbpageName.Controls.Add(this.txtName);
            this.tbpageName.Controls.Add(this.neuLabel2);
            this.tbpageName.Controls.Add(this.neuLabel1);
            this.tbpageName.Location = new System.Drawing.Point(4, 22);
            this.tbpageName.Name = "tbpageName";
            this.tbpageName.Padding = new System.Windows.Forms.Padding(3);
            this.tbpageName.Size = new System.Drawing.Size(373, 103);
            this.tbpageName.TabIndex = 0;
            this.tbpageName.Text = "姓名和性别";
            this.tbpageName.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "身份证号：";
            // 
            // txtCardID
            // 
            this.txtCardID.IsEnter2Tab = false;
            this.txtCardID.Location = new System.Drawing.Point(138, 40);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(146, 21);
            this.txtCardID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardID.TabIndex = 10;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(122, 42);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(0, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbSexF);
            this.groupBox3.Controls.Add(this.rbSexM);
            this.groupBox3.Controls.Add(this.rbDimness);
            this.groupBox3.Location = new System.Drawing.Point(138, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(146, 29);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // rbSexF
            // 
            this.rbSexF.AutoSize = true;
            this.rbSexF.Location = new System.Drawing.Point(50, 10);
            this.rbSexF.Name = "rbSexF";
            this.rbSexF.Size = new System.Drawing.Size(35, 16);
            this.rbSexF.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbSexF.TabIndex = 3;
            this.rbSexF.Text = "女";
            this.rbSexF.UseVisualStyleBackColor = true;
            // 
            // rbSexM
            // 
            this.rbSexM.AutoSize = true;
            this.rbSexM.Location = new System.Drawing.Point(9, 10);
            this.rbSexM.Name = "rbSexM";
            this.rbSexM.Size = new System.Drawing.Size(35, 16);
            this.rbSexM.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbSexM.TabIndex = 2;
            this.rbSexM.Text = "男";
            this.rbSexM.UseVisualStyleBackColor = true;
            // 
            // rbDimness
            // 
            this.rbDimness.AutoSize = true;
            this.rbDimness.Checked = true;
            this.rbDimness.Location = new System.Drawing.Point(91, 10);
            this.rbDimness.Name = "rbDimness";
            this.rbDimness.Size = new System.Drawing.Size(47, 16);
            this.rbDimness.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbDimness.TabIndex = 4;
            this.rbDimness.TabStop = true;
            this.rbDimness.Text = "不详";
            this.rbDimness.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "性    别：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "姓    名：";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(138, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(146, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 5;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(119, 69);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(0, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(119, 19);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(0, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            // 
            // tbpagePatientNo
            // 
            this.tbpagePatientNo.Controls.Add(this.txtPatientNo);
            this.tbpagePatientNo.Controls.Add(this.neuLabel3);
            this.tbpagePatientNo.Location = new System.Drawing.Point(4, 22);
            this.tbpagePatientNo.Name = "tbpagePatientNo";
            this.tbpagePatientNo.Padding = new System.Windows.Forms.Padding(3);
            this.tbpagePatientNo.Size = new System.Drawing.Size(373, 103);
            this.tbpagePatientNo.TabIndex = 1;
            this.tbpagePatientNo.Text = "住院号";
            this.tbpagePatientNo.UseVisualStyleBackColor = true;
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.BackColor = System.Drawing.Color.White;
            this.txtPatientNo.IsEnter2Tab = false;
            this.txtPatientNo.Location = new System.Drawing.Point(133, 43);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.Size = new System.Drawing.Size(125, 21);
            this.txtPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNo.TabIndex = 1;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(74, 48);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "住院号：";
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(393, 91);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(97, 34);
            this.btSearch.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btSearch.TabIndex = 8;
            this.btSearch.Text = "开始查找";
            this.btSearch.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.nbtSearch_Click);
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.neuPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel1.Controls.Add(this.groupBox2);
            this.neuPanel1.Controls.Add(this.tcMain);
            this.neuPanel1.Controls.Add(this.btSearch);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(803, 131);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtSame);
            this.groupBox2.Controls.Add(this.rbtLike);
            this.groupBox2.Controls.Add(this.neuCheckBox2);
            this.groupBox2.Location = new System.Drawing.Point(393, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(97, 72);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // rbtSame
            // 
            this.rbtSame.AutoSize = true;
            this.rbtSame.Checked = true;
            this.rbtSame.Location = new System.Drawing.Point(23, 44);
            this.rbtSame.Name = "rbtSame";
            this.rbtSame.Size = new System.Drawing.Size(47, 16);
            this.rbtSame.TabIndex = 15;
            this.rbtSame.TabStop = true;
            this.rbtSame.Text = "相等";
            this.rbtSame.UseVisualStyleBackColor = true;
            // 
            // rbtLike
            // 
            this.rbtLike.AutoSize = true;
            this.rbtLike.Location = new System.Drawing.Point(23, 19);
            this.rbtLike.Name = "rbtLike";
            this.rbtLike.Size = new System.Drawing.Size(47, 16);
            this.rbtLike.TabIndex = 14;
            this.rbtLike.Text = "相似";
            this.rbtLike.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rbtLike.UseVisualStyleBackColor = true;
            // 
            // neuCheckBox2
            // 
            this.neuCheckBox2.AutoSize = true;
            this.neuCheckBox2.BackColor = System.Drawing.Color.Transparent;
            this.neuCheckBox2.Location = new System.Drawing.Point(123, 17);
            this.neuCheckBox2.Name = "neuCheckBox2";
            this.neuCheckBox2.Size = new System.Drawing.Size(60, 16);
            this.neuCheckBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox2.TabIndex = 13;
            this.neuCheckBox2.Text = "旧系统";
            this.neuCheckBox2.UseVisualStyleBackColor = false;
            // 
            // btNoSelect
            // 
            this.btNoSelect.AutoSize = true;
            this.btNoSelect.Location = new System.Drawing.Point(685, 10);
            this.btNoSelect.Name = "btNoSelect";
            this.btNoSelect.Size = new System.Drawing.Size(97, 30);
            this.btNoSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btNoSelect.TabIndex = 18;
            this.btNoSelect.Text = "不选，新患者";
            this.btNoSelect.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btNoSelect.UseVisualStyleBackColor = true;
            this.btNoSelect.Click += new System.EventHandler(this.btNoSelect_Click);
            // 
            // btSelect
            // 
            this.btSelect.AutoSize = true;
            this.btSelect.Location = new System.Drawing.Point(575, 10);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(99, 30);
            this.btSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btSelect.TabIndex = 17;
            this.btSelect.Text = "选中已有患者";
            this.btSelect.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.spNewPatientInfo);
            this.neuPanel2.Controls.Add(this.panel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 131);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(803, 209);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 13;
            // 
            // spNewPatientInfo
            // 
            this.spNewPatientInfo.About = "3.0.2004.2005";
            this.spNewPatientInfo.AccessibleDescription = "spNewPatientInfo, Sheet1";
            this.spNewPatientInfo.BackColor = System.Drawing.Color.White;
            this.spNewPatientInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spNewPatientInfo.FileName = "";
            this.spNewPatientInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spNewPatientInfo.IsAutoSaveGridStatus = false;
            this.spNewPatientInfo.IsCanCustomConfigColumn = false;
            this.spNewPatientInfo.Location = new System.Drawing.Point(0, 26);
            this.spNewPatientInfo.Name = "spNewPatientInfo";
            this.spNewPatientInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spNewPatientInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetNewPatient});
            this.spNewPatientInfo.Size = new System.Drawing.Size(803, 183);
            this.spNewPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.spNewPatientInfo.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.spNewPatientInfo.TextTipAppearance = tipAppearance1;
            this.spNewPatientInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spNewPatientInfo.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spNewPatientInfo_CellClick);
            // 
            // sheetNewPatient
            // 
            this.sheetNewPatient.Reset();
            this.sheetNewPatient.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetNewPatient.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetNewPatient.ColumnCount = 17;
            this.sheetNewPatient.RowCount = 0;
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 0).Value = "床号";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 2).Value = "性别";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 3).Value = "住院号";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 4).Value = "出生日期";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 5).Value = "当前病区";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 6).Value = "状态";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 7).Value = "身份证号码";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 8).Value = "联系电话";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 9).Value = "出生地址";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 10).Value = "工作单位";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 11).Value = "入院日期";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 12).Value = "出院日期";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 13).Value = "单位及地址";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 14).Value = "联系人";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 15).Value = "住院次数";
            this.sheetNewPatient.ColumnHeader.Cells.Get(0, 16).Value = "记账号";
            this.sheetNewPatient.Columns.Get(0).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(0).Label = "床号";
            this.sheetNewPatient.Columns.Get(0).Visible = false;
            this.sheetNewPatient.Columns.Get(0).Width = 54F;
            this.sheetNewPatient.Columns.Get(1).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(1).Label = "姓名";
            this.sheetNewPatient.Columns.Get(1).Locked = true;
            this.sheetNewPatient.Columns.Get(1).Width = 100F;
            this.sheetNewPatient.Columns.Get(2).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(2).Label = "性别";
            this.sheetNewPatient.Columns.Get(2).Locked = true;
            this.sheetNewPatient.Columns.Get(2).Width = 50F;
            this.sheetNewPatient.Columns.Get(3).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(3).Label = "住院号";
            this.sheetNewPatient.Columns.Get(3).Locked = true;
            this.sheetNewPatient.Columns.Get(3).Width = 75F;
            this.sheetNewPatient.Columns.Get(4).Label = "出生日期";
            this.sheetNewPatient.Columns.Get(4).Locked = true;
            this.sheetNewPatient.Columns.Get(4).Width = 81F;
            this.sheetNewPatient.Columns.Get(5).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(5).Label = "当前病区";
            this.sheetNewPatient.Columns.Get(5).Locked = true;
            this.sheetNewPatient.Columns.Get(5).Width = 87F;
            this.sheetNewPatient.Columns.Get(6).Label = "状态";
            this.sheetNewPatient.Columns.Get(6).Locked = true;
            this.sheetNewPatient.Columns.Get(6).Width = 85F;
            this.sheetNewPatient.Columns.Get(7).CellType = textCellType1;
            this.sheetNewPatient.Columns.Get(7).Label = "身份证号码";
            this.sheetNewPatient.Columns.Get(7).Locked = true;
            this.sheetNewPatient.Columns.Get(7).Width = 150F;
            this.sheetNewPatient.Columns.Get(8).CellType = textCellType2;
            this.sheetNewPatient.Columns.Get(8).Label = "联系电话";
            this.sheetNewPatient.Columns.Get(8).Locked = true;
            this.sheetNewPatient.Columns.Get(8).Width = 90F;
            this.sheetNewPatient.Columns.Get(9).Label = "出生地址";
            this.sheetNewPatient.Columns.Get(9).Locked = true;
            this.sheetNewPatient.Columns.Get(9).Width = 127F;
            this.sheetNewPatient.Columns.Get(10).Label = "工作单位";
            this.sheetNewPatient.Columns.Get(10).Locked = true;
            this.sheetNewPatient.Columns.Get(10).Width = 150F;
            this.sheetNewPatient.Columns.Get(11).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(11).Label = "入院日期";
            this.sheetNewPatient.Columns.Get(11).Locked = true;
            this.sheetNewPatient.Columns.Get(11).Width = 104F;
            this.sheetNewPatient.Columns.Get(12).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(12).Label = "出院日期";
            this.sheetNewPatient.Columns.Get(12).Locked = true;
            this.sheetNewPatient.Columns.Get(12).Width = 104F;
            this.sheetNewPatient.Columns.Get(13).CellType = textCellType3;
            this.sheetNewPatient.Columns.Get(13).Label = "单位及地址";
            this.sheetNewPatient.Columns.Get(13).Locked = true;
            this.sheetNewPatient.Columns.Get(13).Width = 150F;
            this.sheetNewPatient.Columns.Get(14).Label = "联系人";
            this.sheetNewPatient.Columns.Get(14).Locked = true;
            this.sheetNewPatient.Columns.Get(14).Width = 66F;
            this.sheetNewPatient.Columns.Get(15).AllowAutoSort = true;
            this.sheetNewPatient.Columns.Get(15).Label = "住院次数";
            this.sheetNewPatient.Columns.Get(15).Locked = true;
            this.sheetNewPatient.Columns.Get(15).Width = 79F;
            this.sheetNewPatient.Columns.Get(16).Label = "记账号";
            this.sheetNewPatient.Columns.Get(16).Locked = true;
            this.sheetNewPatient.Columns.Get(16).Visible = false;
            this.sheetNewPatient.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.sheetNewPatient.RowHeader.Columns.Default.Resizable = false;
            this.sheetNewPatient.RowHeader.Columns.Get(0).AllowAutoSort = true;
            this.sheetNewPatient.RowHeader.Columns.Get(0).Width = 27F;
            this.sheetNewPatient.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetNewPatient.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.sheetNewPatient.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.sheetNewPatient.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetNewPatient.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spNewPatientInfo.SetViewportLeftColumn(0, 0, 1);
            this.spNewPatientInfo.SetActiveViewport(0, 1, 0);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.panel1.Controls.Add(this.lblNewPatient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 26);
            this.panel1.TabIndex = 2;
            // 
            // lblNewPatient
            // 
            this.lblNewPatient.AutoSize = true;
            this.lblNewPatient.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNewPatient.ForeColor = System.Drawing.Color.Red;
            this.lblNewPatient.Location = new System.Drawing.Point(8, 7);
            this.lblNewPatient.Name = "lblNewPatient";
            this.lblNewPatient.Size = new System.Drawing.Size(65, 12);
            this.lblNewPatient.TabIndex = 0;
            this.lblNewPatient.Text = "住院记录:0";
            // 
            // pnButton
            // 
            this.pnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnButton.Controls.Add(this.label5);
            this.pnButton.Controls.Add(this.label4);
            this.pnButton.Controls.Add(this.btNoSelect);
            this.pnButton.Controls.Add(this.btSelect);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 340);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(803, 48);
            this.pnButton.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(5, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(330, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "若列表中不存在当前患者，请点击【不选，新患者】按钮";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(5, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(512, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "若列表中存在当前入院患者的入院记录，请在列表中选中患者点击【选中已有患者】按钮";
            // 
            // frmQueryPatientInfoNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 388);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.pnButton);
            this.Controls.Add(this.neuPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQueryPatientInfoNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病案查找";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmQueryPatientInfo_KeyDown);
            this.tcMain.ResumeLayout(false);
            this.tbpageName.ResumeLayout(false);
            this.tbpageName.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tbpagePatientNo.ResumeLayout(false);
            this.tbpagePatientNo.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spNewPatientInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetNewPatient)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl tcMain;
        private System.Windows.Forms.TabPage tbpagePatientNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuButton btSearch;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread spNewPatientInfo;
        private FarPoint.Win.Spread.SheetView sheetNewPatient;
        private System.Windows.Forms.TabPage tbpageName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbDimness;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbSexF;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbSexM;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btNoSelect;
        private FS.FrameWork.WinForms.Controls.NeuButton btSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtSame;
        private System.Windows.Forms.RadioButton rbtLike;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox2;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNewPatient;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Label label3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardID;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private Label label4;
        private Label label5;
    }
}