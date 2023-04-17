using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Neusoft.HISFC.Components.Common.Controls;
using System.Collections.Generic;
using Neusoft.HISFC.Models.HealthRecord.EnumServer;

namespace Neusoft.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 新门诊诊断，支持录入描述诊断，查看、作废历史诊断
    /// 北滘，wangsc, {95DF754D-9A34-4692-B232-0EFF41ECB141}
    /// ucCaseInputForClinic 的摘要说明。
    /// </summary>
    public class ucCaseInputForClinic_back : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public delegate void SaveClickDelegate(object sender, ArrayList alDiag);

        /// <summary>
        /// 单击"保存"按钮时,这个没办法,因为门诊医生站,先录诊断再开医嘱.
        /// 这个在用的时候注册就行
        /// </summary>
        public event SaveClickDelegate SaveClicked;

        private System.Windows.Forms.Panel panel1;
        private Neusoft.FrameWork.WinForms.Controls.NeuFpEnter fpEnter1;
        private FarPoint.Win.Spread.SheetView fpEnter1_Sheet1;
        private Neusoft.HISFC.Components.Common.Controls.ucDiagnose ucDiagnose1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private ucUserText ucUserText1;
        private CheckBox checkBox1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Splitter splitter1;
        private Panel panel5;
        private ucModifyOutPatientHealthInfo ucModifyOutPatientHealthInfo2;
        public Label lblRegInfo;
        private System.ComponentModel.IContainer components;

        public ucCaseInputForClinic_back()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCaseInputForClinic_back));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucDiagnose1 = new Neusoft.HISFC.Components.Common.Controls.ucDiagnose();
            this.fpEnter1 = new Neusoft.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ucUserText1 = new Neusoft.HISFC.Components.Common.Controls.ucUserText();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ucModifyOutPatientHealthInfo2 = new Neusoft.HISFC.Components.Common.Controls.ucModifyOutPatientHealthInfo();
            this.lblRegInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucDiagnose1);
            this.panel1.Controls.Add(this.fpEnter1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 266);
            this.panel1.TabIndex = 3;
            // 
            // ucDiagnose1
            // 
            this.ucDiagnose1.AlDiag = ((System.Collections.ArrayList)(resources.GetObject("ucDiagnose1.AlDiag")));
            this.ucDiagnose1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucDiagnose1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ucDiagnose1.Location = new System.Drawing.Point(193, 78);
            this.ucDiagnose1.Name = "ucDiagnose1";
            this.ucDiagnose1.Size = new System.Drawing.Size(427, 161);
            this.ucDiagnose1.TabIndex = 2;
            this.ucDiagnose1.Visible = false;
            // 
            // fpEnter1
            // 
            this.fpEnter1.About = "3.0.2004.2005";
            this.fpEnter1.AccessibleDescription = "fpEnter1, Sheet1, Row 0, Column 0, ";
            this.fpEnter1.AllowDragFill = true;
            this.fpEnter1.AllowDrop = true;
            this.fpEnter1.BackColor = System.Drawing.Color.Azure;
            this.fpEnter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            this.fpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEnter1.Location = new System.Drawing.Point(0, 0);
            this.fpEnter1.Name = "fpEnter1";
            this.fpEnter1.SelectNone = false;
            this.fpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEnter1_Sheet1});
            this.fpEnter1.ShowListWhenOfFocus = false;
            this.fpEnter1.Size = new System.Drawing.Size(707, 266);
            this.fpEnter1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEnter1.TextTipAppearance = tipAppearance1;
            this.fpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEnter1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpEnter1_ButtonClicked);
            this.fpEnter1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpEnter1_EditChange);
            this.fpEnter1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpEnter1_CellDoubleClick);
            this.fpEnter1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpEnter1_CellClick);
            this.fpEnter1.DragDrop += new System.Windows.Forms.DragEventHandler(this.fpEnter1_DragDrop);
            this.fpEnter1.DragEnter += new System.Windows.Forms.DragEventHandler(this.fpEnter1_DragEnter);
            this.fpEnter1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpEnter1_MouseUp);
            // 
            // fpEnter1_Sheet1
            // 
            this.fpEnter1_Sheet1.Reset();
            this.fpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEnter1_Sheet1.ColumnCount = 11;
            this.fpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "诊断类别";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "描述";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ICD10";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "诊断名称";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "疑诊";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "初诊";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "诊断日期";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "诊断医师代码";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "诊断医师";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "诊断分期";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "诊断分级";
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.fpEnter1_Sheet1.Columns.Get(0).Label = "诊断类别";
            this.fpEnter1_Sheet1.Columns.Get(0).Width = 69F;
            this.fpEnter1_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.fpEnter1_Sheet1.Columns.Get(1).Label = "描述";
            this.fpEnter1_Sheet1.Columns.Get(1).Width = 32F;
            this.fpEnter1_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.fpEnter1_Sheet1.Columns.Get(2).Label = "ICD10";
            this.fpEnter1_Sheet1.Columns.Get(2).Width = 64F;
            this.fpEnter1_Sheet1.Columns.Get(3).Label = "诊断名称";
            this.fpEnter1_Sheet1.Columns.Get(3).Width = 227F;
            this.fpEnter1_Sheet1.Columns.Get(4).CellType = checkBoxCellType2;
            this.fpEnter1_Sheet1.Columns.Get(4).Label = "疑诊";
            this.fpEnter1_Sheet1.Columns.Get(4).Width = 38F;
            this.fpEnter1_Sheet1.Columns.Get(5).CellType = checkBoxCellType3;
            this.fpEnter1_Sheet1.Columns.Get(5).Label = "初诊";
            this.fpEnter1_Sheet1.Columns.Get(5).Visible = false;
            this.fpEnter1_Sheet1.Columns.Get(5).Width = 34F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2008, 1, 3, 19, 50, 13, 0);
            dateTimeCellType1.TimeDefault = new System.DateTime(2008, 1, 3, 19, 50, 13, 0);
            this.fpEnter1_Sheet1.Columns.Get(6).CellType = dateTimeCellType1;
            this.fpEnter1_Sheet1.Columns.Get(6).Label = "诊断日期";
            this.fpEnter1_Sheet1.Columns.Get(6).Width = 82F;
            this.fpEnter1_Sheet1.Columns.Get(7).Label = "诊断医师代码";
            this.fpEnter1_Sheet1.Columns.Get(7).Visible = false;
            this.fpEnter1_Sheet1.Columns.Get(7).Width = 88F;
            this.fpEnter1_Sheet1.Columns.Get(8).CellType = textCellType2;
            this.fpEnter1_Sheet1.Columns.Get(8).Label = "诊断医师";
            this.fpEnter1_Sheet1.Columns.Get(8).Width = 64F;
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
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(315, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 28);
            this.button3.TabIndex = 6;
            this.button3.Text = "保 存 ";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(109, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 28);
            this.button2.TabIndex = 5;
            this.button2.Text = "删 除";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(6, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "增 加";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(418, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 28);
            this.button4.TabIndex = 7;
            this.button4.Text = "历史诊断";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.Honeydew;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(2, 57);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(734, 165);
            this.fpSpread1.TabIndex = 8;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.fpSpread1_Sheet1.ColumnCount = 8;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "诊断类型";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "描述";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ICD10";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "诊断名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "疑诊";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "初诊";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "诊断医师";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "诊断日期";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "诊断类型";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 90F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "描述";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 37F;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "诊断名称";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 176F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "初诊";
            this.fpSpread1_Sheet1.Columns.Get(5).Visible = false;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ForeColor = System.Drawing.Color.Black;
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(4, 40);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(61, 15);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "历史诊断:";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Red;
            this.button5.Location = new System.Drawing.Point(212, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 28);
            this.button5.TabIndex = 10;
            this.button5.Text = "作 废";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(71, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(527, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "红色为已作废的诊断,诊断保存后一天内可以添加和修改,一旦保存,只可以作废,不可以删除";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucUserText1
            // 
            this.ucUserText1.AutoScroll = true;
            this.ucUserText1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucUserText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserText1.GroupInfo = null;
            this.ucUserText1.Location = new System.Drawing.Point(0, 0);
            this.ucUserText1.Name = "ucUserText1";
            this.ucUserText1.Size = new System.Drawing.Size(257, 524);
            this.ucUserText1.TabIndex = 13;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.ForeColor = System.Drawing.Color.Red;
            this.checkBox1.Location = new System.Drawing.Point(94, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "科常用诊断";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.ucUserText1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(711, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 524);
            this.panel2.TabIndex = 16;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(707, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 524);
            this.splitter1.TabIndex = 15;
            this.splitter1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.linkLabel1);
            this.panel4.Controls.Add(this.fpSpread1);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button5);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 266);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(707, 198);
            this.panel4.TabIndex = 13;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ucModifyOutPatientHealthInfo2);
            this.panel5.Controls.Add(this.lblRegInfo);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(707, 60);
            this.panel5.TabIndex = 19;
            // 
            // ucModifyOutPatientHealthInfo2
            // 
            this.ucModifyOutPatientHealthInfo2.BackColor = System.Drawing.Color.White;
            this.ucModifyOutPatientHealthInfo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucModifyOutPatientHealthInfo2.IsVisibleSave = true;
            this.ucModifyOutPatientHealthInfo2.Location = new System.Drawing.Point(0, 33);
            this.ucModifyOutPatientHealthInfo2.Name = "ucModifyOutPatientHealthInfo2";
            this.ucModifyOutPatientHealthInfo2.RegInfo = ((Neusoft.HISFC.Models.Registration.Register)(resources.GetObject("ucModifyOutPatientHealthInfo2.RegInfo")));
            this.ucModifyOutPatientHealthInfo2.Size = new System.Drawing.Size(707, 27);
            this.ucModifyOutPatientHealthInfo2.TabIndex = 0;
            // 
            // lblRegInfo
            // 
            this.lblRegInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRegInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegInfo.Location = new System.Drawing.Point(0, 0);
            this.lblRegInfo.Name = "lblRegInfo";
            this.lblRegInfo.Size = new System.Drawing.Size(707, 33);
            this.lblRegInfo.TabIndex = 12;
            this.lblRegInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(707, 464);
            this.panel3.TabIndex = 17;
            // 
            // ucCaseInputForClinic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.Honeydew;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Name = "ucCaseInputForClinic";
            this.Size = new System.Drawing.Size(968, 524);
            this.Load += new System.EventHandler(this.ucCaseInputForClinic_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        //诊断类别
        private DataTable dtDiagnose = new DataTable();
        private ArrayList diagnoseType = new ArrayList();//诊断类别
        private ArrayList alEmployee = new ArrayList();//人员列表
        private ArrayList alDir = new ArrayList();//需要维护提示的诊断
        public ArrayList alDiag = new ArrayList();//诊断
        #region 将诊断传到病历界面
        public delegate void TransportAlDiag(ArrayList arrayDiagnoses);
        public event TransportAlDiag transportAlDiag;
        public ArrayList listDiagnose = new ArrayList();
        #endregion
        string text = "";
        string textName = "";
        Neusoft.FrameWork.WinForms.Forms.frmEasyChoose frm;
        ucUserText ucUserText = new ucUserText();

        private Neusoft.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        private Neusoft.FrameWork.Public.ObjectHelper levelList = new Neusoft.FrameWork.Public.ObjectHelper();
        private Neusoft.FrameWork.Public.ObjectHelper periodList = new Neusoft.FrameWork.Public.ObjectHelper();
        private Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();
        private Neusoft.HISFC.BizLogic.Manager.Person per = new Neusoft.HISFC.BizLogic.Manager.Person();
        private Neusoft.HISFC.BizLogic.Manager.Constant cont = new Neusoft.HISFC.BizLogic.Manager.Constant();
        private Neusoft.HISFC.BizLogic.Registration.Register myReg = new Neusoft.HISFC.BizLogic.Registration.Register();
        private Neusoft.HISFC.BizLogic.Fee.Account account = new Neusoft.HISFC.BizLogic.Fee.Account();

        Neusoft.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase icdMgr = new Neusoft.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();

        /// <summary>
        /// 科室常用诊断
        /// </summary>
        ArrayList alDeptDiag = new ArrayList();

        /// <summary>
        /// 全部诊断
        /// </summary>
        ArrayList alAllDiag = new ArrayList();

        /// <summary>
        /// 患者住院号或门诊号
        /// </summary>
        private string patientId = "";
        public string PatientId
        {
            get
            {
                return this.patientId;
            }
            set
            {
                this.patientId = value;
                if (value != "")
                {
                    if (this.regInfo != null && this.regInfo.IsFee)
                    {
                        regInfo = this.myReg.GetByClinic(value);
                    }

                    //显示体重等信息
                    this.ucModifyOutPatientHealthInfo2.RegInfo = regInfo;
                    this.ucModifyOutPatientHealthInfo2.IsVisibleSave = false;

                    this.Display();
                    this.HistoryCase();
                    //录入诊断时，默认新增一空行
                    if (this.fpEnter1_Sheet1.RowCount < 1)
                    {
                        this.button1_Click(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// 患者实体
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register regInfo = null;

        /// <summary>
        /// 患者挂号时实体
        /// </summary>
        public Neusoft.HISFC.Models.Registration.Register RegInfo
        {
            get
            {
                return regInfo;
            }
            set
            {
                regInfo = value;
                this.PatientId = regInfo.ID;
            }
        }

        private bool isMustDcpReport=false;
        [Category("参数设置"),Description("是否必须填写传染病报告卡"),DefaultValue(false)]
        public bool IsMustDcpReport
        {
            get
            {
                return isMustDcpReport;
            }
            set
            {
                isMustDcpReport = value;
            }
        }


        private bool isClinicDoctorAdd;
        /// <summary>
        /// 门诊医生站增加诊断时,默认为描述医嘱
        /// </summary>
        public bool IsClinicDoctorAdd
        {
            get
            {
                return this.isClinicDoctorAdd;
            }
            set
            {
                this.isClinicDoctorAdd = value;
            }
        }

        private bool isHealthInfoVisible = true;
        /// <summary>
        /// 是否显示体征录入
        /// </summary>
        public bool IsHealthInfoVisible
        {
            get
            {
                return this.isHealthInfoVisible;
            }
            set
            {
                this.isHealthInfoVisible = value;
                this.ucModifyOutPatientHealthInfo2.Visible = value;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitInfo()
        {
            try
            {
                this.ucUserText = this.ucUserText1;

                //初始化表
                this.InitDiagList();

                //设置下拉列表
                this.initList();

                fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;

                this.ucUserText1.OnDoubleClick += new EventHandler(ucUserText1_OnDoubleClick);

                this.ucUserText1.bSetToolTip = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ucUserText1_OnDoubleClick(object sender, EventArgs e)
        {
            if (this.ucUserText1.GetSelectedNode() == null || this.ucUserText1.GetSelectedNode().Tag == null)
                return;

            //在诊断列表找不到时，默认为描述诊断
            string filter = this.ucUserText1.GetSelectedNode().Tag.ToString();

            if (filter.Length <= 0)
                return;

            if (!this.ucDiagnose1.hsDiags.Contains(filter))
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 1].Value = true;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 2].Locked = true;
            }

            if (System.Convert.ToBoolean(this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 1].Value) == true)
            {
                this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 3, false);
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 2].Text = "MS999";
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 3].Text = this.ucUserText1.GetSelectedNode().Text;//e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
            }
            else
            {
                string data = filter;
                this.ucDiagnose1.isDrag = true;
                this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 2, false);
                this.fpEnter1_Sheet1.SetValue(this.fpEnter1_Sheet1.ActiveRowIndex, 2, data);
                this.ucDiagnose1.isDrag = false;
                this.ucDiagnose1.Filter(data);
                this.GetInfo();
            }
     
        }

        /// <summary>
        /// 选择患者赋值
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e.Tag == null)
            {
                return -1;
            }
            if (e.Tag is Neusoft.HISFC.Models.Registration.Register)
            {
                this.regInfo = e.Tag as Neusoft.HISFC.Models.Registration.Register;
                this.PatientId = regInfo.ID;
            }
            return 1;
        }


        /// <summary>
        /// 初始化列表
        /// </summary>
        private void initList()
        {
            try
            {
                this.fpEnter1.SelectNone = true;
                //获取诊断类别
                diagnoseType = Neusoft.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
                diagnoseTypeHelper.ArrayObject = diagnoseType;
                //				this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1,0,diagnoseType);
                FarPoint.Win.Spread.CellType.ComboBoxCellType type = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                string[] s = new string[diagnoseType.Count];
                for (int i = 0; i < diagnoseType.Count; i++)
                    s[i] = (diagnoseType[i] as Neusoft.FrameWork.Models.NeuObject).Name;
                type.Items = s;
                this.fpEnter1_Sheet1.Columns[0].CellType = type;

                //诊断级别
                FarPoint.Win.Spread.CellType.ComboBoxCellType type1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                ArrayList diagLevellist = cont.GetList(Neusoft.HISFC.Models.Base.EnumConstant.DIAGLEVEL);
                this.levelList.ArrayObject = diagLevellist;
                type = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                s = new string[diagLevellist.Count];
                for (int i = 0; i < diagLevellist.Count; i++)
                    s[i] = (diagLevellist[i] as Neusoft.FrameWork.Models.NeuObject).Name;
                type1.Items = s;
                this.fpEnter1_Sheet1.Columns[10].CellType = type1;


                //诊断分期
                FarPoint.Win.Spread.CellType.ComboBoxCellType type2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                ArrayList diagPeriod = cont.GetList(Neusoft.HISFC.Models.Base.EnumConstant.DIAGPERIOD);
                this.periodList.ArrayObject = diagPeriod;
                s = new string[diagPeriod.Count];
                for (int i = 0; i < diagPeriod.Count; i++)
                    s[i] = (diagPeriod[i] as Neusoft.FrameWork.Models.NeuObject).Name;
                type2.Items = s;
                this.fpEnter1_Sheet1.Columns[9].CellType = type2;

                if (this.alDiag.Count <= 0)
                {
                    if (this.fpEnter1_Sheet1.Rows.Count > 1)
                    {
                        this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.Rows.Count);
                        this.fpEnter1_Sheet1.Rows.Add(0, 1);
                        if (diagnoseType != null && diagnoseType.Count > 0)
                        {
                            this.fpEnter1_Sheet1.Cells[0, 0].Value = diagnoseType[0];
                        }
                        this.fpEnter1_Sheet1.Cells[0, 1].Value = false;
                        this.fpEnter1_Sheet1.Cells[0, 4].Value = true;
                        this.fpEnter1_Sheet1.Cells[0, 5].Value = true;
                        //诊断日期
                        this.fpEnter1_Sheet1.Cells[0, 6].Text = System.DateTime.Now.Date.ToShortDateString();
                        //诊断医生代码
                        this.fpEnter1_Sheet1.Cells[0, 7].Text = this.diagManager.Operator.ID;
                        //诊断医生名称
                        this.fpEnter1_Sheet1.Cells[0, 8].Text = this.diagManager.Operator.Name;

                        this.fpEnter1_Sheet1.Cells[0, 9].Value = "一期";
                        this.fpEnter1_Sheet1.Cells[0, 10].Value = "一级";

                        this.fpEnter1.Focus();
                        this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.Rows.Count - 1, 2, false);
                    }
                    else
                    {
                        this.fpEnter1_Sheet1.Rows.Add(0, 1);
                        this.fpEnter1_Sheet1.Cells[0, 0].Value = s[0];
                        this.fpEnter1_Sheet1.Cells[0, 1].Value = false;
                        this.fpEnter1_Sheet1.Cells[0, 4].Value = true;
                        this.fpEnter1_Sheet1.Cells[0, 5].Value = true;
                        //诊断日期
                        this.fpEnter1_Sheet1.Cells[0, 6].Text = System.DateTime.Now.Date.ToShortDateString();
                        //诊断医生代码
                        this.fpEnter1_Sheet1.Cells[0, 7].Text = this.diagManager.Operator.ID;
                        //诊断医生名称
                        this.fpEnter1_Sheet1.Cells[0, 8].Text = this.diagManager.Operator.Name;

                        this.fpEnter1.Focus();
                        this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.Rows.Count - 1, 2, false);
                    }
                }
                //				this.fpEnter1.SetWidthAndHeight(200,200);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 初始化表
        /// </summary>
        private void InitDiagList()
        {
            alDeptDiag = icdMgr.QueryDeptDiag(((Neusoft.HISFC.Models.Base.Employee)this.per.Operator).Dept.ID);
            if (alDeptDiag == null)
            {
                MessageBox.Show("获取科室常用诊断出错：" + icdMgr.Err);
                return;
            }

            alAllDiag = icdMgr.ICDQuery(ICDTypes.ICD10, QueryTypes.Valid);
            if (alAllDiag == null)
            {
                MessageBox.Show("获取全部诊断出错：" + icdMgr.Err);
                return;
            }
        }


        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.ucDiagnose1.Visible = false;
            }
            else if (keyData == Keys.Up)
            {
                if (this.ucDiagnose1.Visible == true)
                {
                    this.ucDiagnose1.Show();
                    this.ucDiagnose1.SetFocus();
                    this.ucDiagnose1.PriorRow();
                    return true;
                }
            }
            else if (keyData == Keys.Down)
            {
                if (this.ucDiagnose1.Visible == true)
                {
                    this.ucDiagnose1.Focus();
                    this.ucDiagnose1.SetFocus();
                    this.ucDiagnose1.NextRow();
                    return true;
                }
            }
            else if (keyData == Keys.Enter)
            {
                if (this.ucDiagnose1.Visible)
                {
                    this.ucDiagnose1_SelectItem(keyData);
                }
            }
            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCaseInputForClinic_Load(object sender, System.EventArgs e)
        {
            try
            {
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载诊断列表,请稍候......");
                Application.DoEvents();

                this.InitInfo();
                fpEnter1.ShowListWhenOfFocus = true;
                if (this.ucDiagnose1 == null)
                    this.ucDiagnose1 = new Neusoft.HISFC.Components.Common.Controls.ucDiagnose();
                //this.ucDiagnose1.IsUseDeptICD = true;
                this.ucDiagnose1.Init();
                if (this.alDeptDiag != null && this.alDeptDiag.Count > 0)
                {
                    this.ucDiagnose1.AlDiag = this.alDeptDiag;
                }
                else if (this.alAllDiag != null)
                {
                    this.ucDiagnose1.AlDiag = this.alAllDiag;
                }
                this.ucDiagnose1.SelectItem += new Neusoft.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
                this.ucDiagnose1.Visible = false;
                this.SetWidth();
                this.alEmployee = this.per.GetEmployeeAll();
                frm = new Neusoft.FrameWork.WinForms.Forms.frmEasyChoose(this.alEmployee);
                this.alDir = cont.GetList("WARNICD10");

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch
            {
                //			    MessageBox.Show("初始化出错","提示");	 
                //				return;
            }
        }


        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ucDiagnose1_SelectItem(Keys key)
        {
            GetInfo();
            return 0;
        }


        /// <summary>
        /// 得到诊断
        /// </summary>
        /// <returns></returns>
        private int GetInfo()
        {
            try
            {
                Neusoft.HISFC.Models.HealthRecord.ICD item = null;
                if (this.ucDiagnose1.GetItem(ref item) == -1)
                {
                    //MessageBox.Show("获取项目出错!","提示");
                    ucDiagnose1.Visible = false;
                    return -1;
                }
                if (item == null) return -1;
                //ICD诊断名称
                fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 2].Text = item.ID;
                //ICD诊断编码
                fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 3].Text = item.Name;
                //fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex,2].Locked = true;
                //ICD诊断编码
                fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 3].Locked = true;
                ucDiagnose1.Visible = false;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 4);
            }
            catch (Exception ex)
            {
                ucDiagnose1.Visible = false;
                MessageBox.Show(ex.Message);
            }
            return 0;
        }


        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            try
            {
                Neusoft.HISFC.Models.HealthRecord.Diagnose diag = this.fpEnter1_Sheet1.Rows[e.Row].Tag as Neusoft.HISFC.Models.HealthRecord.Diagnose;

                if (diag != null)
                {
                    //if(diag.IsValid == "1")
                    if (!diag.DiagInfo.IsValid)
                    {
                        MessageBox.Show("该诊断已经作废,不允许修改!", "提示");
                        return;
                    }
                }
                if (e.Column == 2)//&&!isLocked) 
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ucDiagnose1.Visible = true;
                    }
                    string str = fpEnter1_Sheet1.ActiveCell.Text;
                    this.ucDiagnose1.Filter(str);
                }
                if (e.Column != 2)
                    this.ucDiagnose1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (this.regInfo.IsSee && this.regInfo.DoctorInfo.SeeDate.AddDays(1) < this.myReg.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("该患者已经看诊超过一天,诊断不允许修改!", "提示");
                return;
            }

            this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.Rows.Count, 1);

            this.fpEnter1_Sheet1.Columns[2].Locked = false;


            if (this.diagnoseType != null && this.diagnoseType.Count > 1)
            {
                if (this.fpEnter1_Sheet1.RowCount == 1)
                {
                    //新增的默认为其他诊断
                    this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 0].Value = this.diagnoseType[0];
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 0].Value = this.diagnoseType[1];
                }
            }

            if (this.isClinicDoctorAdd)
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 1].Value = true;
            }
            else
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 1].Value = false;
            }

            if (System.Convert.ToBoolean(this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 1].Value) == false)
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 3].Locked = true;
            }
            else
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 3].Locked = false;
            }

            //this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count-1,4].Value = true;去掉拟诊的默认打钩
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 4].Value = false;
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 5].Value = true;
            //诊断日期
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 6].Text = System.DateTime.Now.Date.ToShortDateString();
            //诊断医生代码
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 7].Text = this.diagManager.Operator.ID;
            //诊断医生名称
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.Rows.Count - 1, 8].Text = this.diagManager.Operator.Name;
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.RowCount - 1, 9].Text = "一期";
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.RowCount - 1, 10].Text = "一级";

            this.fpEnter1.Focus();
            this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.Rows.Count - 1, 2, false);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            this.closeUcDiagnoseForm();
            this.Delete();
        }


        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            if (this.fpEnter1_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            if (this.fpEnter1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            DialogResult r = MessageBox.Show("确定要删除该诊断吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (r == DialogResult.No)
            {
                return;
            }
            //数据库里存在的 ！= null
            if (this.fpEnter1_Sheet1.Rows[this.fpEnter1_Sheet1.ActiveRowIndex].Tag != null)
            {
                Neusoft.HISFC.Models.HealthRecord.Diagnose diag = this.fpEnter1_Sheet1.Rows[this.fpEnter1_Sheet1.ActiveRowIndex].Tag as Neusoft.HISFC.Models.HealthRecord.Diagnose;
                if (diag != null)
                {
                    MessageBox.Show("该诊断已经保存，只可以作废！", "提示");
                    return;
                }
            }
            this.fpEnter1_Sheet1.Rows.Remove(this.fpEnter1_Sheet1.ActiveRowIndex, 1);
        }

        /// <summary>
        /// /Edit by xingz
        /// </summary>
        public void CancelDiag()
        {
            if (this.fpEnter1_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            if (this.fpEnter1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }
            DialogResult r = MessageBox.Show("确实要作废此诊断吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
            {
                return;
            }
            Neusoft.HISFC.Models.HealthRecord.Diagnose diag1 = this.fpEnter1_Sheet1.Rows[this.fpEnter1_Sheet1.ActiveRowIndex].Tag as Neusoft.HISFC.Models.HealthRecord.Diagnose;
            if (diag1 == null)
            {
                MessageBox.Show("该诊断尚未保存，不需要作废！", "提示");
                return;
            }
            try
            {
                int flag = this.diagManager.CancelDiagnoseSingleForClinic(this.PatientId, diag1.DiagInfo.ICD10.ID, diag1.DiagInfo.HappenNo.ToString());
                if (flag == 1 && this.listDiagnose.Count > 0 && listDiagnose != null)
                {
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in listDiagnose)
                    {
                        if (diag.DiagInfo.HappenNo == diag1.DiagInfo.HappenNo)
                        {
                            listDiagnose.Remove(diag);
                            break;
                        }
                    }

                }
            }
            catch
            {

            }
            //this.fpEnter1_Sheet1.Rows[this.fpEnter1_Sheet1.ActiveRowIndex].BackColor = System.Drawing.Color.MistyRose;
            this.Display();
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        private void SetWidth()
        {

            this.fpEnter1_Sheet1.Columns.Get(0).Width = 74F;

            this.fpEnter1_Sheet1.Columns.Get(1).Width = 39F;

            this.fpEnter1_Sheet1.Columns.Get(2).Width = 64F;

            this.fpEnter1_Sheet1.Columns.Get(3).Width = 230F;


            this.fpEnter1_Sheet1.Columns.Get(4).Width = 38F;

            this.fpEnter1_Sheet1.Columns.Get(5).Width = 38F;

            this.fpEnter1_Sheet1.Columns.Get(6).Width = 82F;

            this.fpEnter1_Sheet1.Columns.Get(7).Width = 88F;

            this.fpEnter1_Sheet1.Columns.Get(8).Width = 64F;

            this.fpEnter1_Sheet1.RowHeader.Columns.Get(0).Width = 21F;
        }

        private void SetHistoryWidth()
        {
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 74F;

            this.fpSpread1_Sheet1.Columns.Get(1).Width = 39F;

            this.fpSpread1_Sheet1.Columns.Get(2).Width = 64F;

            this.fpSpread1_Sheet1.Columns.Get(3).Width = 225F;


            this.fpSpread1_Sheet1.Columns.Get(4).Width = 38F;

            this.fpSpread1_Sheet1.Columns.Get(5).Width = 38F;

            this.fpSpread1_Sheet1.Columns.Get(6).Width = 82F;

            this.fpSpread1_Sheet1.Columns.Get(7).Width = 88F;

            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 21F;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        /// <summary>
        /// 验证诊断
        /// </summary>
        /// <param name="tem"></param>
        /// <returns></returns>
        private int DiagCheck(Neusoft.HISFC.Models.HealthRecord.Diagnose tem)
        {
            if (tem == null)
            {
                MessageBox.Show("诊断实体转换出错！");
                return -1;
            }
            if (tem.DiagInfo.DiagType.ID == "")
            {
                MessageBox.Show("诊断类别代码不能为空！");
                return -1;
            }
            if (tem.DiagInfo.Patient.ID == "")
            {
                MessageBox.Show("患者卡号不能为空！");
                return -1;
            }
            if (tem.DiagInfo.ICD10.ID == "")
            {
                MessageBox.Show("诊断ICD10代码不能为空！");
                return -1;
            }
            if (tem.DiagInfo.ICD10.Name == "")
            {
                MessageBox.Show("诊断ICD10名称不能为空！");
                return -1;
            }
            if (tem.DiagInfo.Doctor.ID == "")
            {
                MessageBox.Show("诊断医生代码不能为空！");
                return -1;
            }
            if (tem.DiagInfo.Doctor.Name == "")
            {
                MessageBox.Show("诊断医生姓名不能为空！");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public int Save()
        {
            if (this.alDiag.Count > 0)
            {
                this.alDiag.Clear();
            }
            if (this.listDiagnose.Count > 0)
            {
                this.listDiagnose.Clear();
            }
            if (this.regInfo.IsSee && this.regInfo.DoctorInfo.SeeDate.AddDays(1) < this.myReg.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("该患者已经看诊超过一天,诊断不允许修改!", "提示");
                return -1;
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            bool isSuccess = true;//是否保存成功
            for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
            {
                Neusoft.HISFC.Models.HealthRecord.Diagnose diag = new Neusoft.HISFC.Models.HealthRecord.Diagnose();
                Neusoft.HISFC.Models.HealthRecord.Diagnose temp = null;
                if (this.fpEnter1_Sheet1.Rows[i].Tag != null)
                {
                    temp = this.fpEnter1_Sheet1.Rows[i].Tag as Neusoft.HISFC.Models.HealthRecord.Diagnose;
                    if (temp.Is30Disease == "0")
                    {
                        alDiag.Remove(temp);
                        continue;
                    }
                    if (temp != null)
                    {
                        diag.DiagInfo.HappenNo = temp.DiagInfo.HappenNo;
                    }
                    else
                    {
                        diag.DiagInfo.HappenNo = -1;
                    }
                }
                else
                {
                    diag.DiagInfo.HappenNo = -1;
                }
                diag.DiagInfo.Patient.ID = this.patientId;
                //诊断类别
                diag.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(this.fpEnter1_Sheet1.Cells[i, 0].Text);
                bool isLocked = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.fpEnter1_Sheet1.GetValue(i, 1));
                diag.DiagInfo.IsMain = isLocked;//借用一下
                //诊断ICD码
                if (isLocked)
                {
                    diag.DiagInfo.ICD10.ID = "MS999";
                }
                else
                    diag.DiagInfo.ICD10.ID = this.fpEnter1_Sheet1.Cells[i, 2].Text;
                if (diag.DiagInfo.ICD10.ID == "" && isLocked == false)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("描述诊断请选择描述标志！", "提示");
                    this.fpEnter1_Sheet1.SetActiveCell(i, 1);
                    return -1;
                }
                Neusoft.FrameWork.Models.NeuObject obj = this.levelList.GetObjectFromName(this.fpEnter1_Sheet1.Cells[i, 10].Text);
                if (obj != null)
                {
                    diag.LevelCode = obj.ID;
                }
                obj = this.periodList.GetObjectFromName(this.fpEnter1_Sheet1.Cells[i, 9].Text);
                if (obj != null)
                {
                    diag.PeriorCode = obj.ID;
                }

                //诊断名称
                diag.DiagInfo.ICD10.Name = this.fpEnter1_Sheet1.Cells[i, 3].Text;
                //是否疑诊
                if (Neusoft.FrameWork.Function.NConvert.ToBoolean(this.fpEnter1_Sheet1.GetValue(i, 4)))
                    diag.DubDiagFlag = "1";
                else
                    diag.DubDiagFlag = "0";
                //诊断日期
                diag.DiagInfo.DiagDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.fpEnter1_Sheet1.GetValue(i, 6));//11
                //输入类别
                diag.OperType = "1";
                diag.DiagInfo.Doctor.ID = this.fpEnter1_Sheet1.Cells[i, 7].Text;
                diag.DiagInfo.Doctor.Name = this.fpEnter1_Sheet1.Cells[i, 8].Text;
                if (this.DiagCheck(diag) < 0)
                {
                    return -1;
                }

                if (temp != null)
                {
                    diag.Is30Disease = temp.Is30Disease;//后增[2008/01/25]
                }
                else
                {
                    diag.Is30Disease = "1";//默认都是有效的呗[2008/01/25]
                }

                int j = this.diagManager.UpdateDiagnoseForClinic(diag);
                if (j == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("诊断：" + diag.DiagInfo.ICD10.Name + "保存失败", "提示");
                    isSuccess = false;
                    return -1;
                }
                else if (j == 0)
                {
                    if (this.diagManager.InsertDiagnose(diag) > 0)
                    {
                        this.Warning(diag);
                    }
                    else
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("诊断：" + diag.DiagInfo.ICD10.Name + "保存失败", "提示");
                        isSuccess = false;
                        return -1;
                    }
                }
                this.listDiagnose.Add(diag);
                this.alDiag.Add(diag);
            }

            //保存体重等体征信息
            int rev = this.ucModifyOutPatientHealthInfo2.Save();
            if (rev == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.ucModifyOutPatientHealthInfo2.ErrInfo);
                return -1;
            }
            else if (rev == 0)
            {
                ucModifyOutPatientHealthInfo2.GetHealthInfo(ref this.regInfo);
            }


            if (isSuccess == true)
            {
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("诊断保存成功", "提示");
            }

            Display();

            //门诊医生站用的[2008/8/27]
            if (this.SaveClicked != null)
            {
                this.SaveClicked(this, isMustDcpReport ? listDiagnose : null);
            }
            //END[2008/8/27]

            return 1;
        }

        /// <summary>
        /// 显示
        /// </summary>
        private void Display()
        {
            if (alDiag.Count > 0)
            {
                this.transportAlDiag(listDiagnose);
            }
            if (regInfo.IsAccount)
            {
                //南庄修改
                this.lblRegInfo.Text = "卡号: " + this.regInfo.PID.CardNO + " 姓名: " + this.regInfo.Name + " 性别: " + this.regInfo.Sex.Name + " 挂号时间: " + this.regInfo.DoctorInfo.SeeDate.ToString() + " 挂号科室: " + this.regInfo.DoctorInfo.Templet.Dept.Name;
            }
            else
            {
                this.lblRegInfo.Text = "卡号: " + this.regInfo.PID.CardNO + " 姓名: " + this.regInfo.Name + " 性别: " + this.regInfo.Sex.Name + " 挂号时间: " + this.regInfo.DoctorInfo.SeeDate.ToString() + " 挂号科室: " + this.regInfo.DoctorInfo.Templet.Dept.Name;
            }
            ArrayList al = null;
            try
            {
                al = this.diagManager.QueryCaseDiagnoseForClinic(this.patientId, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("获得患者的诊断信息出错！" + ex.Message, "提示");
                return;
            }
            if (al == null)
            {
                return;
            }

            //付给当前数组
            this.alDiag = al;
            //如果为空，重取，否则下面出异常
            if (this.diagnoseTypeHelper.ArrayObject.Count <= 0)
            {
                this.diagnoseTypeHelper.ArrayObject = Neusoft.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            }
            //清空
            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.Rows.Count);
            }
            //填充
            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                this.fpEnter1_Sheet1.Rows.Add(0, 1);

                //[2008/01/24]肿瘤医院
                if (diag.Is30Disease == "0")//借用这个字段,代表是否有效诊断
                {
                    this.fpEnter1_Sheet1.Rows[0].BackColor = Color.MistyRose;//无效诊断背景色
                }
                //[2008/01/24]END

                this.fpEnter1_Sheet1.Cells[0, 0].Text = this.diagnoseTypeHelper.GetObjectFromID(diag.DiagInfo.DiagType.ID).Name;//诊断类型
                this.fpEnter1_Sheet1.Cells[0, 1].Value = diag.DiagInfo.IsMain;//是否描述
                this.fpEnter1_Sheet1.Cells[0, 2].Text = diag.DiagInfo.ICD10.ID;//icd码
                this.fpEnter1_Sheet1.Cells[0, 3].Text = diag.DiagInfo.ICD10.Name;//icd名称
                if (diag.DiagInfo.ICD10.ID == "MS999")
                {
                    this.fpEnter1_Sheet1.Cells[0, 3].Locked = false;
                    this.fpEnter1_Sheet1.Cells[0, 2].Locked = true;
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[0, 2].Locked = false;
                    this.fpEnter1_Sheet1.Cells[0, 3].Locked = true;
                }
                this.fpEnter1_Sheet1.Cells[0, 4].Value = Neusoft.FrameWork.Function.NConvert.ToBoolean(diag.DubDiagFlag);//是否疑诊
                this.fpEnter1_Sheet1.Cells[0, 6].Text = diag.DiagInfo.DiagDate.Date.ToShortDateString();//日期
                this.fpEnter1_Sheet1.Cells[0, 7].Text = diag.DiagInfo.Doctor.ID;//代码
                this.fpEnter1_Sheet1.Cells[0, 8].Text = diag.DiagInfo.Doctor.Name;//诊断医生
                if (this.periodList.GetObjectFromID(diag.PeriorCode) != null)
                {
                    this.fpEnter1_Sheet1.Cells[0, 9].Text = this.periodList.GetObjectFromID(diag.PeriorCode).Name;
                }
                if (this.levelList.GetObjectFromID(diag.LevelCode) != null)
                {
                    this.fpEnter1_Sheet1.Cells[0, 10].Text = this.levelList.GetObjectFromID(diag.LevelCode).Name;
                }
                this.fpEnter1_Sheet1.Rows[0].Tag = diag;
            }
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        public void SetFocus()
        {
            this.Focus();

            this.fpEnter1.Focus();

            if (this.fpEnter1_Sheet1.ActiveRowIndex >= 0)
            {
                this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 2, false);
            }
        }

        /// <summary>
        /// 设置文字模板
        /// </summary>
        /// <param name="text"></param>
        public void SetControl(ucUserText text)
        {

            //			this.components.Add(this.fpEnter1_Sheet1);
            //			this.components.Add(this.fpEnter1);
            //			this.components.Add(this.textBox1);
            //			text.SetControl(this.components);
            //			this.fpEnter1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpEnter1_MouseUp);
            this.ucUserText = text;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //8是医师录入
            if (this.fpEnter1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (this.fpEnter1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            if (e.Column != 8) return;
            frm.ShowDialog();
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 8].Text = frm.Object.Name;
            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 7].Text = frm.Object.ID;
        }


        /// <summary>
        /// 诊断录入判断算法，需添加详细内容
        /// </summary>
        /// <param name="diag"></param>
        private void Warning(Neusoft.HISFC.Models.HealthRecord.Diagnose diag)
        {
            //如果是传染病诊断，提示填写传染病报告卡
            if (this.diagManager.IsInfect(diag.DiagInfo.ICD10.ID) == "1")
                //MessageBox.Show("诊断：" + diag.DiagInfo.ICD10.Name + "为传染病诊断，请填写传染病报告卡", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //如果是需要提示的项目，提示维护的内容
            foreach (Neusoft.FrameWork.Models.NeuObject obj in alDir)
            {
                if (obj.ID == diag.DiagInfo.ICD10.ID)
                    MessageBox.Show("诊断：" + diag.DiagInfo.ICD10.Name + "提示：" + obj.Name, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        /// <summary>
        /// 双击删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!e.ColumnHeader && !e.RowHeader)
            {
                this.Delete();
            }
        }


        /// <summary>
        /// 拖放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //在诊断列表找不到时，默认为描述诊断
            string filter = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
            //this.ucDiagnose1.Filter(filter);

            if (!this.ucDiagnose1.hsDiags.Contains(filter))
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 1].Value = true;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 2].Locked = true;
            }

            if (System.Convert.ToBoolean(this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 1].Value) == true)
            {
                this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 3, false);
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 2].Text = "MS999";
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, 3].Text = this.ucUserText1.GetSelectedNode().Text;//e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
            }
            else
            {
                string data = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
                this.ucDiagnose1.isDrag = true;
                this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 2, false);
                this.fpEnter1_Sheet1.SetValue(this.fpEnter1_Sheet1.ActiveRowIndex, 2, data);
                this.ucDiagnose1.isDrag = false;
                this.ucDiagnose1.Filter(data);
                this.GetInfo();
            }
            e.Data.SetData("");
        }


        /// <summary>
        /// 右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.fpEnter1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (this.fpEnter1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            FarPoint.Win.Spread.Model.CellRange c = this.fpEnter1.GetCellFromPixel(0, 0, e.X, e.Y);

            int activeRow = c.Row;

            if (activeRow < 0)
            {
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenu menu = new ContextMenu();

                MenuItem addMenu = new MenuItem("添加到用户常用文本");

                addMenu.Click += new EventHandler(addMenu_Click);

                this.fpEnter1.ContextMenu = menu;

                if (this.fpEnter1_Sheet1.Cells[activeRow, 1].Text == "TRUE")
                {
                    this.text = this.fpEnter1_Sheet1.ActiveCell.Text;
                    this.textName = this.text;
                }
                else
                {
                    this.text = this.fpEnter1_Sheet1.Cells[activeRow, 2].Text.Trim();
                    this.textName = this.fpEnter1_Sheet1.Cells[activeRow, 3].Text.Trim();
                }

                menu.MenuItems.Add(addMenu);

                menu.Show(this.fpEnter1, new Point(e.X, e.Y));
            }
        }


        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMenu_Click(object sender, EventArgs e)
        {
            ucUserTextControl u = new ucUserTextControl();
            Neusoft.HISFC.Models.Base.UserText uT = new Neusoft.HISFC.Models.Base.UserText();
            uT.Text = this.text;
            uT.Name = this.textName;
            uT.RichText = "";
            u.UserText = uT;
            Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(u);
            this.ucUserText.RefreshList();
        }


        /// <summary>
        /// 拖入区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = System.Windows.Forms.DragDropEffects.Copy;
        }
        /// <summary>
        /// 改变描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string isLocked = this.fpEnter1_Sheet1.Cells[e.Row, 1].Value.ToString();

            if (isLocked == "True")
            {
                this.fpEnter1_Sheet1.Cells[e.Row, 3].Locked = false;
                this.fpEnter1_Sheet1.Cells[e.Row, 2].Locked = true;
            }
            else //锁定不让修改
            {
                this.fpEnter1_Sheet1.Cells[e.Row, 3].Locked = true;
                this.fpEnter1_Sheet1.Cells[e.Row, 2].Locked = false;
            }
        }

        /// <summary>
        /// 历史诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, System.EventArgs e)
        {
            this.HistoryCase();
        }
        /// <summary>
        /// 历史诊断
        /// </summary>
        private void HistoryCase()
        {
            DataSet ds = new DataSet();

            //Neusoft.HISFC.Models.Registration.Register r = new Neusoft.HISFC.Models.Registration.Register();

            //r= this.myReg.GetByClinic(this.patientId);

            if (this.regInfo == null)
            {
                MessageBox.Show("查询患者信息出错!" + this.myReg.Err);
                return;
            }

            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);
            }

            //ArrayList al = this.myReg.Query(r.PID.CardNO,new DateTime(2006,5,11,0,0,0));
            ArrayList al = this.myReg.Query(this.regInfo.PID.CardNO, new DateTime(2006, 5, 11, 0, 0, 0));

            foreach (Neusoft.HISFC.Models.Registration.Register rr in al)
            {
                if (rr.DoctorInfo.SeeDate.AddDays(30) < System.DateTime.Now)
                {
                    continue;
                }

                DataSet ds1 = new DataSet();
                this.diagManager.QueryCaseHistoryByID(rr.ID, ref ds1);

                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow row in ds1.Tables[0].Rows)
                        {
                            row[0] = this.diagnoseTypeHelper.GetObjectFromID(row[0].ToString()).Name;
                        }
                    }
                    catch
                    { }
                    ds.Merge(ds1);
                }
            }
            //历史诊断按诊断日期降序排序           
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].DefaultView.Sort = "诊断日期 desc";
                DataSet dsNew = new DataSet();
                DataTable dt = ds.Tables[0].DefaultView.ToTable();
                dsNew.Tables.Add(dt);
                if (dsNew != null && dsNew.Tables.Count > 0 && dsNew.Tables[0].Rows.Count > 0)
                {
                    this.fpSpread1_Sheet1.DataSource = dsNew;
                }
            }

            this.SetHistoryWidth();
        }


        private void button5_Click(object sender, System.EventArgs e)
        {
            this.CancelDiag();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.CheckState == CheckState.Checked)
            {
                this.ucDiagnose1.AlDiag = this.alDeptDiag;
            }
            if (this.checkBox1.CheckState == CheckState.Unchecked)
            {
                this.ucDiagnose1.AlDiag = this.alAllDiag;
            }
            //Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载诊断列表,请稍候......");
            //Application.DoEvents();
            //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }
        private void closeUcDiagnoseForm()
        {
            if (this.ucDiagnose1.Visible == true)
            {
                this.ucDiagnose1.Hide();
            }
        }

        /// <summary>
        /// 历史诊断双击复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rowIndex = this.fpSpread1.ActiveSheet.ActiveRowIndex;
            string diagnoise = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
            if (string.IsNullOrEmpty(diagnoise))
            {
                return;
            }
            else
            {
                int row = this.fpEnter1_Sheet1.RowCount;
                if (row == 0 || (!string.IsNullOrEmpty(this.fpEnter1_Sheet1.Cells[row - 1, 2].Text)))
                {
                    this.fpEnter1_Sheet1.Rows.Add(row, 1);
                }
                row = this.fpEnter1_Sheet1.RowCount;
                this.fpEnter1_Sheet1.Cells[row - 1, 0].Text = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
                if (this.fpSpread1_Sheet1.Cells[rowIndex, 1].Text == "是")
                {
                    this.fpEnter1_Sheet1.Cells[row - 1, 1].Value = true;
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[row - 1, 1].Value = false;
                }

                this.fpEnter1_Sheet1.Cells[row - 1, 2].Text = this.fpSpread1_Sheet1.Cells[rowIndex, 2].Text;
                this.fpEnter1_Sheet1.Cells[row - 1, 3].Text = this.fpSpread1_Sheet1.Cells[rowIndex, 3].Text;
                if (this.fpSpread1_Sheet1.Cells[rowIndex, 4].Text == "是")
                {
                    this.fpEnter1_Sheet1.Cells[row - 1, 4].Value = true;
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[row - 1, 4].Value = false;
                }
                this.fpEnter1_Sheet1.Cells[row - 1, 6].Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.fpEnter1_Sheet1.Cells[row - 1, 7].Text = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).ID;
                this.fpEnter1_Sheet1.Cells[row - 1, 8].Text = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Name;
            }
        }
    }
}
