using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GZ.Components.Bespeak;
using FS.HISFC.Models.RADT;

namespace FS.WinForms.Report.OutpatientFee
{
    public partial class ucClinicQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
         #region 自动生成的代码
        private System.Windows.Forms.TabControl tabPageQuery;
        private System.Windows.Forms.TabPage tabPageRegisteQuery;
		private System.Windows.Forms.CheckBox checkBoxRegisteDate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerRegisteFrom;
		private System.Windows.Forms.DateTimePicker dateTimePickerRegisteTo;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBoxCardCode;
		private FS.FrameWork.WinForms.Controls.NeuTextBox textBoxPatientName;
		private System.Windows.Forms.Label label6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBoxAgeAndSex;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
		private FS.FrameWork.WinForms.Controls.NeuTextBox textBoxIDCard;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBoxBornDate;
        private System.Windows.Forms.Panel panelRegistCondition;
		private System.Windows.Forms.Panel panelRegistRecord;
		private FarPoint.Win.Spread.FpSpread fpSpreadRegistRecord;
        private FarPoint.Win.Spread.SheetView SheetRegistRecord;
        private System.Windows.Forms.TabPage tabPageInvoiceQuery;
		private FarPoint.Win.Spread.FpSpread fpSpreadFeeDetail;
        private FarPoint.Win.Spread.SheetView SheetFeeDetail;
        private TabPage tabPageOrderQuery;
        private FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory ucOrderHistory1;
        private TabPage tabPagePatientInfo;
        private FS.HISFC.Components.Account.Controls.ucRegPatientInfo ucRegPatientInfo1;
		private System.ComponentModel.IContainer components;
        //{4270B15D-35E1-4f95-874E-D552E65BBD26}
        private FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCaseQuery ucoutemr = new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCaseQuery();
		public ucClinicQuery()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.tabPageQuery = new System.Windows.Forms.TabControl();
            this.tabPageRegisteQuery = new System.Windows.Forms.TabPage();
            this.panelRegistRecord = new System.Windows.Forms.Panel();
            this.fpSpreadRegistRecord = new FarPoint.Win.Spread.FpSpread();
            this.SheetRegistRecord = new FarPoint.Win.Spread.SheetView();
            this.panelRegistCondition = new System.Windows.Forms.Panel();
            this.tabPagePatientInfo = new System.Windows.Forms.TabPage();
            this.ucRegPatientInfo1 = new FS.HISFC.Components.Account.Controls.ucRegPatientInfo();
            this.tabPageInvoiceQuery = new System.Windows.Forms.TabPage();
            this.fpSpreadFeeDetail = new FarPoint.Win.Spread.FpSpread();
            this.SheetFeeDetail = new FarPoint.Win.Spread.SheetView();
            this.tabPageOrderQuery = new System.Windows.Forms.TabPage();
            this.ucOrderHistory1 = new FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory();
            this.tabPageInPatientOrder = new System.Windows.Forms.TabPage();
            this.tabPageEMRQuery = new System.Windows.Forms.TabPage();
            this.ucEMRQuery1 = new FS.HISFC.Components.Order.Controls.ucEMRQuery();
            this.tabPageOBIS = new System.Windows.Forms.TabPage();
            this.ucOBISBrowser1 = new FS.HISFC.Components.Order.Controls.ucOBISBrowser();
            this.tabPageLisResult = new System.Windows.Forms.TabPage();
            this.ucLisResQuery1 = new FS.HISFC.Components.Order.Controls.ucLisResQuery();
            this.tabPagePacsResult = new System.Windows.Forms.TabPage();
            this.ucPacsResQuery1 = new FS.HISFC.Components.Order.Controls.ucPacsResQuery();
            this.tabPagePackageQuery = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBoxBornDate = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxIDCard = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAgeAndSex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPatientName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCardCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerRegisteTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerRegisteFrom = new System.Windows.Forms.DateTimePicker();
            this.checkBoxRegisteDate = new System.Windows.Forms.CheckBox();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tabPageOutEMR = new System.Windows.Forms.TabPage(); //{4270B15D-35E1-4f95-874E-D552E65BBD26}
            this.tabPageQuery.SuspendLayout();
            this.tabPageRegisteQuery.SuspendLayout();
            this.panelRegistRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadRegistRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetRegistRecord)).BeginInit();
            this.tabPagePatientInfo.SuspendLayout();
            this.tabPageInvoiceQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadFeeDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetFeeDetail)).BeginInit();
            this.tabPageOrderQuery.SuspendLayout();
            this.tabPageEMRQuery.SuspendLayout();
            this.tabPageOBIS.SuspendLayout();
            this.tabPageLisResult.SuspendLayout();
            this.tabPagePacsResult.SuspendLayout();
            this.tabPagePackageQuery.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageQuery
            // 
            this.tabPageQuery.Controls.Add(this.tabPageRegisteQuery);
            this.tabPageQuery.Controls.Add(this.tabPagePatientInfo);
            this.tabPageQuery.Controls.Add(this.tabPageInvoiceQuery);
            this.tabPageQuery.Controls.Add(this.tabPageOrderQuery);
            this.tabPageQuery.Controls.Add(this.tabPageInPatientOrder);
            this.tabPageQuery.Controls.Add(this.tabPageEMRQuery);
            this.tabPageQuery.Controls.Add(this.tabPageOBIS);
            this.tabPageQuery.Controls.Add(this.tabPageLisResult);
            this.tabPageQuery.Controls.Add(this.tabPagePacsResult);
            this.tabPageQuery.Controls.Add(this.tabPagePackageQuery);
            this.tabPageQuery.Controls.Add(this.tabPageOutEMR); //{4270B15D-35E1-4f95-874E-D552E65BBD26}
            this.tabPageQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageQuery.Location = new System.Drawing.Point(0, 0);
            this.tabPageQuery.Name = "tabPageQuery";
            this.tabPageQuery.SelectedIndex = 0;
            this.tabPageQuery.Size = new System.Drawing.Size(1234, 377);
            this.tabPageQuery.TabIndex = 2;
            // 
            // tabPageRegisteQuery
            // 
            this.tabPageRegisteQuery.Controls.Add(this.panelRegistRecord);
            this.tabPageRegisteQuery.ImageIndex = 0;
            this.tabPageRegisteQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageRegisteQuery.Name = "tabPageRegisteQuery";
            this.tabPageRegisteQuery.Size = new System.Drawing.Size(1226, 351);
            this.tabPageRegisteQuery.TabIndex = 0;
            this.tabPageRegisteQuery.Text = "挂号信息查询";
            this.tabPageRegisteQuery.UseVisualStyleBackColor = true;
            // 
            // panelRegistRecord
            // 
            this.panelRegistRecord.Controls.Add(this.fpSpreadRegistRecord);
            this.panelRegistRecord.Controls.Add(this.panelRegistCondition);
            this.panelRegistRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegistRecord.Location = new System.Drawing.Point(0, 0);
            this.panelRegistRecord.Name = "panelRegistRecord";
            this.panelRegistRecord.Size = new System.Drawing.Size(1226, 351);
            this.panelRegistRecord.TabIndex = 2;
            // 
            // fpSpreadRegistRecord
            // 
            this.fpSpreadRegistRecord.About = "3.0.2004.2005";
            this.fpSpreadRegistRecord.AccessibleDescription = "fpSpreadRegistRecord, Sheet1, Row 0, Column 0, ";
            this.fpSpreadRegistRecord.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpreadRegistRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadRegistRecord.Location = new System.Drawing.Point(0, 0);
            this.fpSpreadRegistRecord.Name = "fpSpreadRegistRecord";
            this.fpSpreadRegistRecord.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpreadRegistRecord.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SheetRegistRecord});
            this.fpSpreadRegistRecord.Size = new System.Drawing.Size(1226, 351);
            this.fpSpreadRegistRecord.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpreadRegistRecord.TextTipAppearance = tipAppearance1;
            this.fpSpreadRegistRecord.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpreadRegistRecord_CellDoubleClick);
            this.fpSpreadRegistRecord.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpreadRegistRecord_CellClick);
            // 
            // SheetRegistRecord
            // 
            this.SheetRegistRecord.Reset();
            this.SheetRegistRecord.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SheetRegistRecord.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SheetRegistRecord.ColumnCount = 11;
            this.SheetRegistRecord.RowCount = 1;
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 0).Value = "病历号码";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 1).Value = "患者姓名";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 2).Value = "挂号日期";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 3).Value = "结算类别";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 4).Value = "合同单位";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 5).Value = "挂号级别";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 6).Value = "挂号科室";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 7).Value = "挂号医生";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 8).Value = "看诊序号";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 9).Value = "是否有效";
            this.SheetRegistRecord.ColumnHeader.Cells.Get(0, 10).Value = "退号时间";
            this.SheetRegistRecord.Columns.Get(0).CellType = textCellType1;
            this.SheetRegistRecord.Columns.Get(0).Label = "病历号码";
            this.SheetRegistRecord.Columns.Get(0).Width = 100F;
            this.SheetRegistRecord.Columns.Get(1).CellType = textCellType2;
            this.SheetRegistRecord.Columns.Get(1).Label = "患者姓名";
            this.SheetRegistRecord.Columns.Get(1).Width = 80F;
            this.SheetRegistRecord.Columns.Get(2).CellType = textCellType3;
            this.SheetRegistRecord.Columns.Get(2).Label = "挂号日期";
            this.SheetRegistRecord.Columns.Get(2).Width = 150F;
            this.SheetRegistRecord.Columns.Get(3).CellType = textCellType4;
            this.SheetRegistRecord.Columns.Get(3).Label = "结算类别";
            this.SheetRegistRecord.Columns.Get(3).Width = 80F;
            this.SheetRegistRecord.Columns.Get(4).CellType = textCellType5;
            this.SheetRegistRecord.Columns.Get(4).Label = "合同单位";
            this.SheetRegistRecord.Columns.Get(4).Width = 80F;
            this.SheetRegistRecord.Columns.Get(5).CellType = textCellType6;
            this.SheetRegistRecord.Columns.Get(5).Label = "挂号级别";
            this.SheetRegistRecord.Columns.Get(5).Width = 80F;
            this.SheetRegistRecord.Columns.Get(6).CellType = textCellType7;
            this.SheetRegistRecord.Columns.Get(6).Label = "挂号科室";
            this.SheetRegistRecord.Columns.Get(6).Width = 100F;
            this.SheetRegistRecord.Columns.Get(7).Label = "挂号医生";
            this.SheetRegistRecord.Columns.Get(7).Width = 77F;
            this.SheetRegistRecord.Columns.Get(8).CellType = textCellType8;
            this.SheetRegistRecord.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.SheetRegistRecord.Columns.Get(8).Label = "看诊序号";
            this.SheetRegistRecord.Columns.Get(8).Width = 76F;
            this.SheetRegistRecord.Columns.Get(9).CellType = textCellType9;
            this.SheetRegistRecord.Columns.Get(9).Label = "是否有效";
            this.SheetRegistRecord.Columns.Get(9).Width = 86F;
            this.SheetRegistRecord.Columns.Get(10).CellType = textCellType10;
            this.SheetRegistRecord.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.SheetRegistRecord.Columns.Get(10).Label = "退号时间";
            this.SheetRegistRecord.Columns.Get(10).Width = 82F;
            this.SheetRegistRecord.RowHeader.Columns.Default.Resizable = false;
            this.SheetRegistRecord.RowHeader.Columns.Get(0).Width = 25F;
            this.SheetRegistRecord.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.SheetRegistRecord.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.SheetRegistRecord.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.SheetRegistRecord.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.SheetRegistRecord.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panelRegistCondition
            // 
            this.panelRegistCondition.Location = new System.Drawing.Point(14, 61);
            this.panelRegistCondition.Name = "panelRegistCondition";
            this.panelRegistCondition.Size = new System.Drawing.Size(1226, 48);
            this.panelRegistCondition.TabIndex = 5;
            // 
            // tabPagePatientInfo
            // 
            this.tabPagePatientInfo.Controls.Add(this.ucRegPatientInfo1);
            this.tabPagePatientInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPagePatientInfo.Name = "tabPagePatientInfo";
            this.tabPagePatientInfo.Size = new System.Drawing.Size(1226, 351);
            this.tabPagePatientInfo.TabIndex = 3;
            this.tabPagePatientInfo.Text = "患者基本信息查询";
            this.tabPagePatientInfo.UseVisualStyleBackColor = true;
            // 
            // ucRegPatientInfo1
            // 
            this.ucRegPatientInfo1.AutoCardNo = "";
            this.ucRegPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucRegPatientInfo1.CardNO = "";
            this.ucRegPatientInfo1.CardWay = false;
            this.ucRegPatientInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRegPatientInfo1.IMustInpubByOne = 0;
            this.ucRegPatientInfo1.IsAutoBuildCardNo = false;
            this.ucRegPatientInfo1.IsEditMode = false;
            this.ucRegPatientInfo1.IsEnableEntry = true;
            this.ucRegPatientInfo1.IsEnableIDENO = true;
            this.ucRegPatientInfo1.IsEnableIDEType = true;
            this.ucRegPatientInfo1.IsEnablePact = true;
            this.ucRegPatientInfo1.IsEnableSiNO = true;
            this.ucRegPatientInfo1.IsEnableVip = true;
            this.ucRegPatientInfo1.IsFullConvertToHalf = true;
            this.ucRegPatientInfo1.IsInputBirthDay = true;
            this.ucRegPatientInfo1.IsInputIDENO = true;
            this.ucRegPatientInfo1.IsInputIDEType = true;
            this.ucRegPatientInfo1.IsInputName = true;
            this.ucRegPatientInfo1.IsInputPact = true;
            this.ucRegPatientInfo1.IsInputPHONE = true;
            this.ucRegPatientInfo1.IsInputSex = true;
            this.ucRegPatientInfo1.IsInputSiNo = true;
            this.ucRegPatientInfo1.IsInSequentialOrder = true;
            this.ucRegPatientInfo1.IsJudgePact = true;
            this.ucRegPatientInfo1.IsJudgePactByIdno = true;
            this.ucRegPatientInfo1.IsJumpHomePhone = true;
            this.ucRegPatientInfo1.IsLocalOperation = true;
            this.ucRegPatientInfo1.IsMustInputTabIndex = true;
            this.ucRegPatientInfo1.IsMutilPactInfo = true;
            this.ucRegPatientInfo1.IsPrint = true;
            this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter = true;
            this.ucRegPatientInfo1.IsShowTitle = false;
            this.ucRegPatientInfo1.IsTreatment = true;
            this.ucRegPatientInfo1.IsValidHospitalStaff = "";
            this.ucRegPatientInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucRegPatientInfo1.McardNO = "";
            this.ucRegPatientInfo1.Name = "ucRegPatientInfo1";
            this.ucRegPatientInfo1.ParentFormToolBar = null;
            this.ucRegPatientInfo1.Size = new System.Drawing.Size(1226, 351);
            this.ucRegPatientInfo1.TabIndex = 0;
            // 
            // tabPageInvoiceQuery
            // 
            this.tabPageInvoiceQuery.Controls.Add(this.fpSpreadFeeDetail);
            this.tabPageInvoiceQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageInvoiceQuery.Name = "tabPageInvoiceQuery";
            this.tabPageInvoiceQuery.Size = new System.Drawing.Size(1226, 351);
            this.tabPageInvoiceQuery.TabIndex = 1;
            this.tabPageInvoiceQuery.Text = "门诊发票信息查询";
            this.tabPageInvoiceQuery.UseVisualStyleBackColor = true;
            // 
            // fpSpreadFeeDetail
            // 
            this.fpSpreadFeeDetail.About = "3.0.2004.2005";
            this.fpSpreadFeeDetail.AccessibleDescription = "fpSpreadFeeDetail, Sheet1, Row 0, Column 0, ";
            this.fpSpreadFeeDetail.BackColor = System.Drawing.Color.Transparent;
            this.fpSpreadFeeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadFeeDetail.Location = new System.Drawing.Point(0, 0);
            this.fpSpreadFeeDetail.Name = "fpSpreadFeeDetail";
            this.fpSpreadFeeDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpreadFeeDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SheetFeeDetail});
            this.fpSpreadFeeDetail.Size = new System.Drawing.Size(1226, 351);
            this.fpSpreadFeeDetail.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpreadFeeDetail.TextTipAppearance = tipAppearance2;
            // 
            // SheetFeeDetail
            // 
            this.SheetFeeDetail.Reset();
            this.SheetFeeDetail.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SheetFeeDetail.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SheetFeeDetail.ColumnCount = 27;
            this.SheetFeeDetail.RowCount = 1;
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 0).Value = "发票号码";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 1).Value = "划价收费";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 2).Value = "状态";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 3).Value = "录入来源";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 4).Value = "项目编码";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 5).Value = "项目名称";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 6).Value = "组";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 7).Value = "组名";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 8).Value = "内部序号";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 9).Value = "序列号";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 10).Value = "规格";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 11).Value = "数量";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 12).Value = "单价";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 13).Value = "总金额";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 14).Value = "自付金额";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 15).Value = "记帐金额";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 16).Value = "开单科室";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 17).Value = "开单医生";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 18).Value = "录入人";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 19).Value = "录入时间";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 20).Value = "执行科室";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 21).Value = "收款员";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 22).Value = "收费日期";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 23).Value = "确认执行时间";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 24).Value = "确认执行科室";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 25).Value = "确认执行人";
            this.SheetFeeDetail.ColumnHeader.Cells.Get(0, 26).Value = "确认执行时间";
            this.SheetFeeDetail.Columns.Get(0).Label = "发票号码";
            this.SheetFeeDetail.Columns.Get(0).Width = 105F;
            this.SheetFeeDetail.Columns.Get(1).Label = "划价收费";
            this.SheetFeeDetail.Columns.Get(1).Width = 90F;
            this.SheetFeeDetail.Columns.Get(3).Label = "录入来源";
            this.SheetFeeDetail.Columns.Get(3).Width = 90F;
            this.SheetFeeDetail.Columns.Get(4).Label = "项目编码";
            this.SheetFeeDetail.Columns.Get(4).Width = 100F;
            this.SheetFeeDetail.Columns.Get(5).Label = "项目名称";
            this.SheetFeeDetail.Columns.Get(5).Width = 100F;
            this.SheetFeeDetail.Columns.Get(6).Label = "组";
            this.SheetFeeDetail.Columns.Get(6).Width = 40F;
            this.SheetFeeDetail.Columns.Get(7).Label = "组名";
            this.SheetFeeDetail.Columns.Get(7).Width = 70F;
            this.SheetFeeDetail.Columns.Get(8).Label = "内部序号";
            this.SheetFeeDetail.Columns.Get(8).Width = 80F;
            this.SheetFeeDetail.Columns.Get(9).Label = "序列号";
            this.SheetFeeDetail.Columns.Get(9).Width = 70F;
            this.SheetFeeDetail.Columns.Get(13).Label = "总金额";
            this.SheetFeeDetail.Columns.Get(13).Width = 80F;
            this.SheetFeeDetail.Columns.Get(14).Label = "自付金额";
            this.SheetFeeDetail.Columns.Get(14).Width = 90F;
            this.SheetFeeDetail.Columns.Get(15).Label = "记帐金额";
            this.SheetFeeDetail.Columns.Get(15).Width = 90F;
            this.SheetFeeDetail.Columns.Get(16).Label = "开单科室";
            this.SheetFeeDetail.Columns.Get(16).Width = 90F;
            this.SheetFeeDetail.Columns.Get(17).Label = "开单医生";
            this.SheetFeeDetail.Columns.Get(17).Width = 90F;
            this.SheetFeeDetail.Columns.Get(18).Label = "录入人";
            this.SheetFeeDetail.Columns.Get(18).Width = 100F;
            this.SheetFeeDetail.Columns.Get(19).Label = "录入时间";
            this.SheetFeeDetail.Columns.Get(19).Width = 90F;
            this.SheetFeeDetail.Columns.Get(20).Label = "执行科室";
            this.SheetFeeDetail.Columns.Get(20).Width = 90F;
            this.SheetFeeDetail.Columns.Get(21).Label = "收款员";
            this.SheetFeeDetail.Columns.Get(21).Width = 80F;
            this.SheetFeeDetail.Columns.Get(22).Label = "收费日期";
            this.SheetFeeDetail.Columns.Get(22).Width = 90F;
            this.SheetFeeDetail.Columns.Get(23).Label = "确认执行时间";
            this.SheetFeeDetail.Columns.Get(23).Width = 120F;
            this.SheetFeeDetail.Columns.Get(24).Label = "确认执行科室";
            this.SheetFeeDetail.Columns.Get(24).Width = 120F;
            this.SheetFeeDetail.Columns.Get(25).Label = "确认执行人";
            this.SheetFeeDetail.Columns.Get(25).Width = 120F;
            this.SheetFeeDetail.Columns.Get(26).Label = "确认执行时间";
            this.SheetFeeDetail.Columns.Get(26).Width = 120F;
            this.SheetFeeDetail.DataAutoSizeColumns = false;
            this.SheetFeeDetail.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.SheetFeeDetail.RowHeader.Columns.Default.Resizable = false;
            this.SheetFeeDetail.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.SheetFeeDetail.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.SheetFeeDetail.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.SheetFeeDetail.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.SheetFeeDetail.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabPageOrderQuery
            // 
            this.tabPageOrderQuery.Controls.Add(this.ucOrderHistory1);
            this.tabPageOrderQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageOrderQuery.Name = "tabPageOrderQuery";
            this.tabPageOrderQuery.Size = new System.Drawing.Size(1226, 351);
            this.tabPageOrderQuery.TabIndex = 2;
            this.tabPageOrderQuery.Text = "门诊医嘱信息查询";
            this.tabPageOrderQuery.UseVisualStyleBackColor = true;
            // 
            // ucOrderHistory1
            // 
            this.ucOrderHistory1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucOrderHistory1.DefaultQueryDays = 90;
            this.ucOrderHistory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderHistory1.IsFullConvertToHalf = true;
            this.ucOrderHistory1.IsPrint = false;
            this.ucOrderHistory1.IsShowCopyAllClick = false;
            this.ucOrderHistory1.Location = new System.Drawing.Point(0, 0);
            this.ucOrderHistory1.Name = "ucOrderHistory1";
            this.ucOrderHistory1.ParentFormToolBar = null;
            this.ucOrderHistory1.Size = new System.Drawing.Size(1226, 351);
            this.ucOrderHistory1.TabIndex = 0;
            // 
            // tabPageInPatientOrder
            // 
            this.tabPageInPatientOrder.Location = new System.Drawing.Point(4, 22);
            this.tabPageInPatientOrder.Name = "tabPageInPatientOrder";
            this.tabPageInPatientOrder.Size = new System.Drawing.Size(1226, 351);
            this.tabPageInPatientOrder.TabIndex = 4;
            this.tabPageInPatientOrder.Text = "住院医嘱查询";
            this.tabPageInPatientOrder.UseVisualStyleBackColor = true;
            // 
            // tabPageEMRQuery
            // 
            this.tabPageEMRQuery.Controls.Add(this.ucEMRQuery1);
            this.tabPageEMRQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageEMRQuery.Name = "tabPageEMRQuery";
            this.tabPageEMRQuery.Size = new System.Drawing.Size(1226, 351);
            this.tabPageEMRQuery.TabIndex = 9;
            this.tabPageEMRQuery.Text = "病历信息查询";
            this.tabPageEMRQuery.UseVisualStyleBackColor = true;
            // 
            // ucEMRQuery1
            // 
            this.ucEMRQuery1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucEMRQuery1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEMRQuery1.IsFullConvertToHalf = true;
            this.ucEMRQuery1.IsPrint = false;
            this.ucEMRQuery1.Location = new System.Drawing.Point(0, 0);
            this.ucEMRQuery1.Name = "ucEMRQuery1";
            this.ucEMRQuery1.ParentFormToolBar = null;
            this.ucEMRQuery1.Patient = null;
            this.ucEMRQuery1.Register = null;
            this.ucEMRQuery1.Size = new System.Drawing.Size(1226, 351);
            this.ucEMRQuery1.TabIndex = 0;
            // 
            // tabPageOBIS
            // 
            this.tabPageOBIS.Controls.Add(this.ucOBISBrowser1);
            this.tabPageOBIS.Location = new System.Drawing.Point(4, 22);
            this.tabPageOBIS.Name = "tabPageOBIS";
            this.tabPageOBIS.Size = new System.Drawing.Size(1226, 351);
            this.tabPageOBIS.TabIndex = 8;
            this.tabPageOBIS.Text = "围产信息";
            this.tabPageOBIS.UseVisualStyleBackColor = true;
            // 
            // ucOBISBrowser1
            // 
            this.ucOBISBrowser1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucOBISBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOBISBrowser1.IsFullConvertToHalf = true;
            this.ucOBISBrowser1.IsPrint = false;
            this.ucOBISBrowser1.Location = new System.Drawing.Point(0, 0);
            this.ucOBISBrowser1.Name = "ucOBISBrowser1";
            this.ucOBISBrowser1.ParentFormToolBar = null;
            this.ucOBISBrowser1.Patient = null;
            this.ucOBISBrowser1.Register = null;
            this.ucOBISBrowser1.Size = new System.Drawing.Size(1226, 351);
            this.ucOBISBrowser1.TabIndex = 0;
            // 
            // tabPageLisResult
            // 
            this.tabPageLisResult.Controls.Add(this.ucLisResQuery1);
            this.tabPageLisResult.Location = new System.Drawing.Point(4, 22);
            this.tabPageLisResult.Name = "tabPageLisResult";
            this.tabPageLisResult.Size = new System.Drawing.Size(1226, 351);
            this.tabPageLisResult.TabIndex = 5;
            this.tabPageLisResult.Text = "检验结果查询";
            this.tabPageLisResult.UseVisualStyleBackColor = true;
            // 
            // ucLisResQuery1
            // 
            this.ucLisResQuery1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucLisResQuery1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLisResQuery1.IsFullConvertToHalf = true;
            this.ucLisResQuery1.IsPrint = false;
            this.ucLisResQuery1.Location = new System.Drawing.Point(0, 0);
            this.ucLisResQuery1.Name = "ucLisResQuery1";
            this.ucLisResQuery1.ParentFormToolBar = null;
            this.ucLisResQuery1.Patient = null;
            this.ucLisResQuery1.Register = null;
            this.ucLisResQuery1.Size = new System.Drawing.Size(1226, 351);
            this.ucLisResQuery1.TabIndex = 1;
            // 
            // tabPagePacsResult
            // 
            this.tabPagePacsResult.Controls.Add(this.ucPacsResQuery1);
            this.tabPagePacsResult.Location = new System.Drawing.Point(4, 22);
            this.tabPagePacsResult.Name = "tabPagePacsResult";
            this.tabPagePacsResult.Size = new System.Drawing.Size(1226, 351);
            this.tabPagePacsResult.TabIndex = 6;
            this.tabPagePacsResult.Text = "检查结果查询";
            this.tabPagePacsResult.UseVisualStyleBackColor = true;
            // 
            // ucPacsResQuery1
            // 
            this.ucPacsResQuery1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPacsResQuery1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPacsResQuery1.IsFullConvertToHalf = true;
            this.ucPacsResQuery1.IsPrint = false;
            this.ucPacsResQuery1.Location = new System.Drawing.Point(0, 0);
            this.ucPacsResQuery1.Name = "ucPacsResQuery1";
            this.ucPacsResQuery1.ParentFormToolBar = null;
            this.ucPacsResQuery1.Patient = null;
            this.ucPacsResQuery1.Register = null;
            this.ucPacsResQuery1.Size = new System.Drawing.Size(1226, 351);
            this.ucPacsResQuery1.TabIndex = 2;
            // 
            // tabPagePackageQuery
            // 
            this.tabPagePackageQuery.Controls.Add(this.panel5);
            this.tabPagePackageQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPagePackageQuery.Name = "tabPagePackageQuery";
            this.tabPagePackageQuery.Size = new System.Drawing.Size(1226, 351);
            this.tabPagePackageQuery.TabIndex = 7;
            this.tabPagePackageQuery.Text = "套餐查询";
            this.tabPagePackageQuery.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1226, 351);
            this.panel5.TabIndex = 3;
            // 
            // textBoxBornDate
            // 
            this.textBoxBornDate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxBornDate.IsEnter2Tab = false;
            this.textBoxBornDate.Location = new System.Drawing.Point(749, 17);
            this.textBoxBornDate.Name = "textBoxBornDate";
            this.textBoxBornDate.ReadOnly = true;
            this.textBoxBornDate.Size = new System.Drawing.Size(120, 21);
            this.textBoxBornDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBoxBornDate.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(678, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "出生日期：";
            // 
            // textBoxIDCard
            // 
            this.textBoxIDCard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxIDCard.IsEnter2Tab = false;
            this.textBoxIDCard.Location = new System.Drawing.Point(955, 17);
            this.textBoxIDCard.Name = "textBoxIDCard";
            this.textBoxIDCard.ReadOnly = true;
            this.textBoxIDCard.Size = new System.Drawing.Size(120, 21);
            this.textBoxIDCard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBoxIDCard.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(888, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "身份证号：";
            // 
            // textBoxAgeAndSex
            // 
            this.textBoxAgeAndSex.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxAgeAndSex.IsEnter2Tab = false;
            this.textBoxAgeAndSex.Location = new System.Drawing.Point(550, 17);
            this.textBoxAgeAndSex.Name = "textBoxAgeAndSex";
            this.textBoxAgeAndSex.ReadOnly = true;
            this.textBoxAgeAndSex.Size = new System.Drawing.Size(100, 21);
            this.textBoxAgeAndSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBoxAgeAndSex.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(479, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "性别年龄：";
            // 
            // textBoxPatientName
            // 
            this.textBoxPatientName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxPatientName.IsEnter2Tab = false;
            this.textBoxPatientName.Location = new System.Drawing.Point(344, 17);
            this.textBoxPatientName.Name = "textBoxPatientName";
            this.textBoxPatientName.ReadOnly = true;
            this.textBoxPatientName.Size = new System.Drawing.Size(120, 21);
            this.textBoxPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBoxPatientName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "姓名：";
            // 
            // textBoxCardCode
            // 
            this.textBoxCardCode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxCardCode.IsEnter2Tab = false;
            this.textBoxCardCode.Location = new System.Drawing.Point(111, 17);
            this.textBoxCardCode.Name = "textBoxCardCode";
            this.textBoxCardCode.ReadOnly = true;
            this.textBoxCardCode.Size = new System.Drawing.Size(120, 21);
            this.textBoxCardCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBoxCardCode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "病历号：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(493, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 11);
            this.label2.TabIndex = 7;
            this.label2.Text = "——";
            // 
            // dateTimePickerRegisteTo
            // 
            this.dateTimePickerRegisteTo.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerRegisteTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRegisteTo.Location = new System.Drawing.Point(513, 9);
            this.dateTimePickerRegisteTo.Name = "dateTimePickerRegisteTo";
            this.dateTimePickerRegisteTo.Size = new System.Drawing.Size(140, 21);
            this.dateTimePickerRegisteTo.TabIndex = 6;
            // 
            // dateTimePickerRegisteFrom
            // 
            this.dateTimePickerRegisteFrom.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerRegisteFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRegisteFrom.Location = new System.Drawing.Point(348, 10);
            this.dateTimePickerRegisteFrom.Name = "dateTimePickerRegisteFrom";
            this.dateTimePickerRegisteFrom.Size = new System.Drawing.Size(140, 21);
            this.dateTimePickerRegisteFrom.TabIndex = 5;
            // 
            // checkBoxRegisteDate
            // 
            this.checkBoxRegisteDate.Checked = true;
            this.checkBoxRegisteDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRegisteDate.Location = new System.Drawing.Point(268, 12);
            this.checkBoxRegisteDate.Name = "checkBoxRegisteDate";
            this.checkBoxRegisteDate.Size = new System.Drawing.Size(74, 19);
            this.checkBoxRegisteDate.TabIndex = 4;
            this.checkBoxRegisteDate.Text = "时间范围";
            this.checkBoxRegisteDate.CheckedChanged += new System.EventHandler(this.checkBoxRegisteDate_CheckedChanged);
            // 
            // txtCardNO
            // 
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(115, 13);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(120, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 45;
            this.txtCardNO.Tag = "CARDNO";
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNO.ForeColor = System.Drawing.Color.Red;
            this.lbCardNO.Location = new System.Drawing.Point(6, 17);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(105, 14);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 44;
            this.lbCardNO.Text = "个人信息检索:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Controls.Add(this.txtCardNO);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.lbCardNO);
            this.panel6.Controls.Add(this.dateTimePickerRegisteTo);
            this.panel6.Controls.Add(this.dateTimePickerRegisteFrom);
            this.panel6.Controls.Add(this.checkBoxRegisteDate);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1234, 97);
            this.panel6.TabIndex = 46;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxAgeAndSex);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxIDCard);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBoxBornDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxCardCode);
            this.groupBox1.Controls.Add(this.textBoxPatientName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(4, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1226, 44);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "个人简略信息";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tabPageQuery);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 97);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1234, 377);
            this.panel7.TabIndex = 47;
            // 
            // tabPageOutEMR //{4270B15D-35E1-4f95-874E-D552E65BBD26}
            // 
            this.tabPageOutEMR.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutEMR.Name = "tabPageOutEMR";
            this.tabPageOutEMR.Size = new System.Drawing.Size(1226, 351);
            this.tabPageOutEMR.TabIndex = 10;
            this.tabPageOutEMR.Text = "门诊电子病历";
            this.tabPageOutEMR.UseVisualStyleBackColor = true;
            // 
            // ucClinicQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Name = "ucClinicQuery";
            this.Size = new System.Drawing.Size(1234, 474);
            this.Load += new System.EventHandler(this.frmClinicQuery_Load);
            this.tabPageQuery.ResumeLayout(false);
            this.tabPageRegisteQuery.ResumeLayout(false);
            this.panelRegistRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadRegistRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetRegistRecord)).EndInit();
            this.tabPagePatientInfo.ResumeLayout(false);
            this.tabPageInvoiceQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadFeeDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheetFeeDetail)).EndInit();
            this.tabPageOrderQuery.ResumeLayout(false);
            this.tabPageEMRQuery.ResumeLayout(false);
            this.tabPageOBIS.ResumeLayout(false);
            this.tabPageLisResult.ResumeLayout(false);
            this.tabPagePacsResult.ResumeLayout(false);
            this.tabPagePackageQuery.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		#endregion

		//
		// 变量
		//
		#region 变量

		#region 枚举
		/// <summary>
		/// 查询方式枚举
		/// </summary>
		enum QueryType
		{
			CardCode = 0,
			PatientName = 1,
			PactCode = 2,
			SeeDepartment = 3,
			SeeDoctor = 4,
			MedicareCode = 5,
			InvoiceCode = 6
		}
		/// <summary>
		/// 查询操作方式枚举
		/// </summary>
		enum OperateType
		{
			EqueQuery = 0,
			LikeQuery = 1
		}
		/// <summary>
		/// 挂号FarPoint字段枚举
		/// </summary>
		enum RegisterField
		{
			/// <summary>
			/// 病历号0
			/// </summary>
			CardCode = 0,
			/// <summary>
			/// 患者姓名1
			/// </summary>
			PatientName = 1,
			/// <summary>
			/// 挂号日期2
			/// </summary>
			RegisteDate = 2,
			/// <summary>
			/// 结算类别3
			/// </summary>
			PayKindName = 3,
			/// <summary>
			/// 合同单位4
			/// </summary>
			PactName = 4,
			/// <summary>
			/// 挂号级别5
			/// </summary>
			RegisteLevel = 5,
			/// <summary>
			/// 挂号科室6
			/// </summary>
			RegisteDepartment = 6,
            /// <summary>
            /// 挂号医生
            /// </summary>
            RegisterDoctor = 7,
            /// <summary>
            /// 看诊序号
            /// </summary>
            SeeNo = 8,
            /// <summary>
            /// 是否有效
            /// </summary>
            Valid = 9,
            /// <summary>
            /// 退号时间
            /// </summary>
            CancelDate = 10,
            /// <summary>
            /// 是否已看诊
            /// </summary>
            YnSee = 11,
		}
		enum FeeDetail
		{
			/// <summary>
			/// 发票号码0
			/// </summary>
			InvoiceCode = 0,
			/// <summary>
			/// 划价收费状态1
			/// </summary>
			ChargeState = 1,
			/// <summary>
			/// 状态2
			/// </summary>
			State = 2,
			/// <summary>
			/// 项目编码3
			/// </summary>
			ItemCode = 3,
			/// <summary>
			/// 项目名称4
			/// </summary>
			ItemName = 4,
			/// <summary>
			/// 项目数量5
			/// </summary>
			ItemCount = 5,
			/// <summary>
			/// 项目单价6
			/// </summary>
			ItemPrice = 6,
			/// <summary>
			/// 总金额7
			/// </summary>
			TotalCost = 7,
			/// <summary>
			/// 自付金额8
			/// </summary>
			OwnCost = 8,
			/// <summary>
			/// 记帐金额9
			/// </summary>
			PubCost = 9,
			/// <summary>
			/// 开单科室10
			/// </summary>
			CreateDepartment = 10,
			/// <summary>
			/// 开单医生11
			/// </summary>
			CreateDoctor = 11,

            /// <summary>
            /// 执行科室[划价\收费时确定]
            /// </summary>
            EXEDPCD = 12,

			/// <summary>
			/// 收款员12
			/// </summary>
			ChargeEmployee = 13,
			/// <summary>
			/// 收款日期13
			/// </summary>
			ChargeDate = 14,
			/// <summary>
			/// 确认执行状态14
			/// </summary>
			ExecuteState = 15,
			/// <summary>
			/// 确认执行科室15
			/// </summary>
			ExecuteDepartment = 16,
			/// <summary>
			/// 确认执行人16
			/// </summary>
			ExecuteEmployee = 17,
			/// <summary>
			/// 确认执行时间17
			/// </summary>
			ExecuteDate = 18
		}
		#endregion 

		/// <summary>
		/// 是否选择了挂号时间限制
		/// </summary>
		bool boolRegisteDate = true;
		/// <summary>
		/// 返回值
		/// </summary>
		int intReturn = 0;
		/// <summary>
		/// 当前挂号信息
		/// </summary>
        FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
		/// <summary>
		/// 查询方式
        /// //{61223200-7CC7-4c5d-A830-922A8127BD5A}
		/// </summary>
		QueryType enumQuery = QueryType.PatientName;
		/// <summary>
		/// 查询操作方式
		/// </summary>
		OperateType enumOperate = OperateType.LikeQuery;
		/// <summary>
		/// 方法类
		/// </summary>
        OutpatientFee.Class.OutPatientQuery function = new FS.WinForms.Report.OutpatientFee.Class.OutPatientQuery();
		/// <summary>
		/// 科室业务层
		/// </summary>
        FS.HISFC.BizLogic.Manager.Department departmentFunction = new FS.HISFC.BizLogic.Manager.Department();
		/// <summary>
		/// 合同单位数组
		/// </summary>
		ArrayList alPact = new ArrayList();
		/// <summary>
		/// 科室数组
		/// </summary>
		ArrayList alDepartment = new ArrayList() ;
		/// <summary>
		/// 医生数组
		/// </summary>
		ArrayList alEmployee = new ArrayList();

        ////{E26C3EE9-D480-421e-9FD3-7094D8E4E1D0}
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.FrameWork.Public.ObjectHelper emplHelper = new FS.FrameWork.Public.ObjectHelper();
		/// <summary>
		/// 选择窗口
		/// </summary>
		//FS.Common.Forms.frmEasyChoose frmChoose;

        //{61223200-7CC7-4c5d-A830-922A8127BD5A}
        FS.WinForms.Report.Order.ucOrderShowForQuery orderQueryShow = new FS.WinForms.Report.Order.ucOrderShowForQuery();
        FS.HISFC.Components.Common.Controls.ucPackageQuery packageQuery = new FS.HISFC.Components.Common.Controls.ucPackageQuery();
        #endregion

		//
		// 属性
		//
		#region 属性
		#region 当前挂号信息
		/// <summary>
		/// 当前挂号信息
		/// </summary>
		public FS.HISFC.Models.Registration.Register Register
		{
			get
			{
				return this.register;
			}
			set
			{
				this.register = value;
				// 病历号码
				this.textBoxCardCode.Text = this.register.PID.CardNO;
				// 门诊号
               // this.textBoxClinicCode.Text = this.register.ID;
				// 姓名
				this.textBoxPatientName.Text = this.register.Name;
				// 性别/年龄
				this.textBoxAgeAndSex.Text = this.register.Sex.Name + "|" + departmentFunction.GetAge(this.register.Birthday);
				// 挂号日期
				//this.textBoxRegisteDate.Text = this.register.DoctorInfo.SeeDate.ToString();
				// 身份证号
				this.textBoxIDCard.Text = this.register.IDCard;
				// 出生日期
				this.textBoxBornDate.Text = this.register.Birthday.Date.ToString();
				// 结算类别: 01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干
                //switch (this.register.Pact.PayKind.ID)
                //{
                //    case "01":
                //        this.textBoxBalanceType.Text = "自费";
                //        break;
                //    case "02":
                //        this.textBoxBalanceType.Text = "医保";
                //        break;
                //    case "03":
                //        this.textBoxBalanceType.Text = "公费";
                //        break;
                //}
                //// 合同单位
                //this.textBoxPactName.Text = this.register.Pact.Name;
                //// 医疗证号
                //this.textBoxMCardCode.Text = this.register.SSN;
                //// 挂号级别
                //this.textBoxRegistLevel.Text = this.register.DoctorInfo.Templet.RegLevel.Name;
                //// 挂号科室
                //this.textBoxRegistDepartment.Text = this.register.DoctorInfo.Templet.Dept.Name;
                //// 挂号医生
                //this.textBoxRegistDoctor.Text = this.register.DoctorInfo.Templet.Doct.Name;
				// 看诊科室
                //{E26C3EE9-D480-421e-9FD3-7094D8E4E1D0}
                //this.textBoxSeeDepartment.Text = this.register.DoctorInfo.Templet.Dept.Name;
         
                //this.textBoxSeeDoctor.Text = this.register.DoctorReceiver.Name;
                //this.textBoxSeeDepartment.Text = this.deptHelper.GetName(this.register.SeeDoct.Dept.ID);

                //this.textBoxSeeDoctor.Text = this.emplHelper.GetName(this.register.SeeDoct.ID);

                //// 挂号发票
                //this.textBoxRegisteInvoice.Text = this.register.InvoiceNO;
                //// 挂号费
                //this.textBoxFeeRegist.Text = this.register.RegLvlFee.RegFee.ToString();
                //// 检查费
                //this.textBoxFeeCheck.Text = this.register.RegLvlFee.ChkFee.ToString();
                //// 诊察费
                //this.textBoxFeeDiagnose.Text = this.register.RegLvlFee.OwnDigFee.ToString();
                //// 附加费
                //this.textBoxFeeOther.Text = this.register.RegLvlFee.OthFee.ToString();
			}
		}
		#endregion
		#endregion

		//
		// 事件
		//
		#region 窗口加载事件
		/// <summary>
		/// 窗口加载事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmClinicQuery_Load(object sender, System.EventArgs e)
		{
			// 科室业务层
			FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
			// 医生业务层
            FS.HISFC.BizLogic.Manager.UserManager userFunction = new FS.HISFC.BizLogic.Manager.UserManager();
			// 常数业务层
            FS.HISFC.BizLogic.Manager.Constant constFunction = new FS.HISFC.BizLogic.Manager.Constant();

			FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在初始化信息...");
            Application.DoEvents();
			// 设置挂号信息FarPoint页自动排序
			this.SheetRegistRecord.SetColumnAllowAutoSort(-1,true);
			// 设置费用信息FarPoint页自动排序
			this.SheetFeeDetail.SetColumnAllowAutoSort(-1,true);

			// 初始化科室
            this.alDepartment = manager.QueryRegDepartment();

            ArrayList alAllDept = manager.GetDepartment();

            ////{E26C3EE9-D480-421e-9FD3-7094D8E4E1D0}
            this.deptHelper.ArrayObject = alAllDept;
			// 初始化医生

            this.alEmployee = manager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);

            ArrayList alAllDoct = manager.QueryEmployeeAll();

            ////{E26C3EE9-D480-421e-9FD3-7094D8E4E1D0}
            this.emplHelper.ArrayObject = alAllDoct;
			// 初始化合同单位
			this.alPact = constFunction.GetAllList("PACTUNIT");

			// 设置起始时间和截止时间
            this.dateTimePickerRegisteTo.Value = departmentFunction.GetDateTimeFromSysDateTime();//.AddMonths(-1);
			this.dateTimePickerRegisteFrom.Value = new DateTime(this.dateTimePickerRegisteTo.Value.Year,
																this.dateTimePickerRegisteTo.Value.Month,
																this.dateTimePickerRegisteTo.Value.Day,
																0,
																0,
																0);
            dateTimePickerRegisteFrom.Value = departmentFunction.GetDateTimeFromSysDateTime().AddMonths(-1);
			// 设置焦点到条件输入框
			this.tabPageRegisteQuery.Focus();
			//this.textBoxPatientCondition.Focus();
            this.ucRegPatientInfo1.IsShowTitle = false;
            this.ucRegPatientInfo1.SetControlEnable(true);

            //{61223200-7CC7-4c5d-A830-922A8127BD5A}
            //住院医嘱查询
            orderQueryShow.Dock = DockStyle.Fill;
            this.tabPageInPatientOrder.Controls.Add(orderQueryShow);

            //套餐查询
            packageQuery.Dock = DockStyle.Fill;
            packageQuery.DetailVisible = true;
            this.panel5.Controls.Add(packageQuery);


            //门诊电子病历//{4270B15D-35E1-4f95-874E-D552E65BBD26}
            ucoutemr.Dock = DockStyle.Fill;
            this.tabPageOutEMR.Controls.Add(ucoutemr);

			FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
		}
		#endregion

		// 挂号信息
		#region 查询条件选择事件
		/// <summary>
		/// 查询条件选择事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        //private void comboBoxPatientCondition_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    this.Clear();
        //    // 设置查询方式枚举
        //    this.enumQuery = (QueryType)this.comboBoxPatientCondition.SelectedIndex;
        //    // 设置焦点到查询操作方式
        //    this.comboBoxOperate.Focus();
        //    // 根据查询方式设置不同的查询效果
        //    #region  屏蔽 [肿瘤医院]

        //    switch (this.enumQuery)
        //    {
        //        case QueryType.PactCode:
        //            // 按合同单位查询,条件输入框只读
        //            this.textBoxPatientCondition.ReadOnly = false;
        //            this.textBoxPatientCondition.AddItems( this.alPact );
        //            break;
        //        case QueryType.SeeDepartment:
        //            // 按挂号科室查询,条件输入框只读
        //            this.textBoxPatientCondition.ReadOnly = false;
        //            this.textBoxPatientCondition.AddItems( this.alDepartment );
        //            break;
        //        case QueryType.SeeDoctor:
        //            // 按开方医生查询,条件输入框只读
        //            this.textBoxPatientCondition.ReadOnly = false;
        //            this.textBoxPatientCondition.AddItems( this.alEmployee);
        //            break;
        //        default:
        //            // 否则条件输入框可写
        //            ArrayList al = new ArrayList();
        //            this.textBoxPatientCondition.AddItems( al );
        //            break;
        //    }

        //    #endregion  
        //}
		#endregion
		#region 查询条件回车事件
		/// <summary>
		/// 查询条件回车事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        //private void comboBoxPatientCondition_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    // 如果回车,那么设置焦点到查询方式
        //    if (e.KeyCode.Equals(Keys.Enter))
        //    {
        //        // 设置焦点到查询方式
        //        this.comboBoxOperate.Focus();
        //        // 设置查询方式枚举
        //        this.enumQuery = (QueryType)this.comboBoxPatientCondition.SelectedIndex;
        //    }
        //}
		#endregion

		#region 查询操作方式选择事件
		/// <summary>
		/// 查询操作方式选择事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        //private void comboBoxOperate_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    // 设置焦点到条件输入框
        //    this.textBoxPatientCondition.Focus();
        //    // 全选内容
        //    this.textBoxPatientCondition.SelectAll();
        //    // 设置查询操作方式
        //    this.enumOperate = (OperateType)this.comboBoxOperate.SelectedIndex;
        //    // 清空查询条件输入框
        //    this.textBoxPatientCondition.Text = "";
        //}
		#endregion
		#region 查询操作方式回车事件
		/// <summary>
		/// 查询操作方式回车事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        //private void comboBoxOperate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    // 如果回车
        //    if (e.KeyCode.Equals(Keys.Enter))
        //    {
        //        // 那么设置焦点到输入框
        //        this.textBoxPatientCondition.Focus();
        //        // 使条件输入框的内容全选
        //        this.textBoxPatientCondition.SelectAll();
        //        // 设置查询操作方式
        //        this.enumOperate = (OperateType)this.comboBoxOperate.SelectedIndex;
        //    }
        //}
		#endregion

		#region 挂号时间复选框选择事件
		/// <summary>
		/// 挂号时间复选框选择事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxRegisteDate_CheckedChanged(object sender, System.EventArgs e)
		{
			// 设置是否有挂号时间限制
			this.boolRegisteDate = this.checkBoxRegisteDate.Checked;

			// 设置时间控件的可用性
			if (this.checkBoxRegisteDate.Checked)
			{
				// 如果有时间限制,那么设置两个时间选择控件可用
				this.dateTimePickerRegisteFrom.Enabled = true;
				this.dateTimePickerRegisteTo.Enabled = true;
			}
			else
			{
				// 否则不可用
				this.dateTimePickerRegisteFrom.Enabled = false;
				this.dateTimePickerRegisteTo.Enabled = false;
			}
		}
		#endregion

		#region 查询条件输入框按键事件
		/// <summary>
		/// 查询条件输入框按键事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxPatientCondition_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode.Equals(Keys.Enter))
			{
				// 如果会车,那么执行查询
				FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在检索信息...");
                Application.DoEvents();
                //先清空原来的挂号信息
                this.Clear();
				// 查询挂号信息
			//	this.QueryRegister();

				FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            #region 屏蔽 [肿瘤医院]

            //else if (e.KeyCode.Equals(Keys.Space))
            //{
            //    // 选择的项目
            //    FS.NFC.Object.NeuObject selectObject = new NeuObject();

            //    // 根据查询类别,显示不同的选择窗口
            //    if (this.enumQuery.Equals(QueryType.PactCode))
            //    {
            //        // 合同单位
            //        //frmChoose = new frmEasyChoose(this.alPact);
            //        // 显示选择窗口
            //        //frmChoose.ShowDialog();
            //        this.textBoxPatientCondition.AddItems( this.alPact );
            //    }
            //    else if (this.enumQuery.Equals(QueryType.SeeDepartment))
            //    {
            //        this.textBoxPatientCondition.AddItems( this.alDepartment );
            //        // 科室
            //        //frmChoose = new frmEasyChoose(this.alDepartment) ;
            //        //frmChoose.ShowDialog();
            //    }
            //    else if (this.enumQuery.Equals(QueryType.SeeDoctor))
            //    {
            //        this.textBoxPatientCondition.AddItems(this.alEmployee);
            //        // 员工
            //        //frmChoose = new frmEasyChoose(this.alEmployee);
            //        //frmChoose.ShowDialog();
            //    }

            //    this.Focus();
            //    // 设置焦点到当前条件输入框
            //    this.textBoxPatientCondition.Focus();

            //    // 显示选择结果
            //    //selectObject = frmChoose.Object;
            //    //if (selectObject != null)
            //    //{
            //    //    // 显示选择的项目名称
            //    //    this.textBoxPatientCondition.Text = selectObject.Name;
            //    //    // 保存Tag为对象
            //    //    this.textBoxPatientCondition.Tag = selectObject;
            //    //}
            //}

            #endregion
		}
		#endregion

		#region 挂号信息FarPoint鼠标双击事件

        /// <summary>
        /// 挂号信息Farpoint鼠标单击事件[单击查看患者信息]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpreadRegistRecord_CellClick( object sender, FarPoint.Win.Spread.CellClickEventArgs e )
        {
            // 显示挂号信息
            this.Register = (FS.HISFC.Models.Registration.Register)this.GetRowTag( this.SheetRegistRecord, e.Row );

        }

		/// <summary>
		/// 挂号信息FarPoint鼠标双击事件[双击查看明细]
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fpSpreadRegistRecord_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			if (this.SheetRegistRecord.RowCount>0)
			{
                this.tabPageQuery.SelectedTab = tabPageInvoiceQuery;
				FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在检索信息...");
                Application.DoEvents();
                //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} 修改综合查询
               // (this.ucOrderHistory1 as FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).Patient = this.Register;
                //(this.ucOrderHistory1 as FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).SetTime();
                //(this.ucOrderHistory1 as FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).Query(this.register.PID.CardNO);
               // orderQueryShow.Patient = this.Register;
                //this.ucRegPatientInfo1.Clear(true);
                //this.ucRegPatientInfo1.CardNO = this.Register.PID.CardNO;
				// 费用明细数据集
				System.Data.DataSet dsResult = new DataSet();
				
				// 显示费用明细
				this.intReturn = function.GetFeeDetailByClinicCode(this.Register.ID, ref dsResult);
				if (this.intReturn == -1)
				{
					FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
					MessageBox.Show("获取费用明细失败!\n" + function.Err);
					return;
				}
				// 设置费用数据源为查询结果
				this.SheetFeeDetail.DataSource = dsResult;
                 FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.SheetFeeDetail,6,6);
                foreach (FarPoint.Win.Spread.Column col in this.SheetFeeDetail.Columns)
                {
                    col.Width = col.GetPreferredWidth();
                }
                for (int i = 0; i < this.SheetFeeDetail.Rows.Count; i++)
                {
                    this.SheetFeeDetail.Rows[i].BackColor = Color.White;

                    if (this.SheetFeeDetail.Cells[i,2].Text == "退费")
                    {
                        this.SheetFeeDetail.Rows[i].BackColor = Color.MistyRose;
                    }
                }
                this.ucoutemr.Clear(); //{4097D044-6360-4895-BFFB-D6E4BBF5AE74}
                this.ucoutemr.RegObj = this.register;
                this.ucoutemr.InitTreeCase();
				FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                
			}
		}
		#endregion

		#region 工具栏按钮单击事件
		/// <summary>
		/// 工具栏按钮单击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
            //if (e.Button.Equals(this.toolBarButtonQuery))
            //{
            //    // 如果单击了查询按钮,那么执行查询
            //    this.QueryRegister();
            //}
            //else if (e.Button.Equals(this.tbExpert))
            //{
            //    // 如果单击了查询按钮,那么执行查询
            //    this.Export();
            //}
            //else if (e.Button.Equals(this.toolBarButtonExit))
            //{
            //    // 如果单击了退出按钮,那么提示用户是否退出
            //    if (DialogResult.Yes.Equals(MessageBox.Show("退出查询?","门诊收费综合查询",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)))
            //    {
            //        //this.Close();
            //    }
            //}
		}
		#endregion

		#region 窗口按键事件
		/// <summary>
		/// 窗口按键事件
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData.Equals(Keys.F8))
			{
				// 如果按F8,那么执行查询
				//this.QueryRegister();
			}
			else if (keyData.Equals(Keys.F9))
			{
				// 如果按F8,那么执行查询
				this.Export();
			}
			else if (keyData.Equals(Keys.F12))
			{
				// 如果F12,那么提示用户是否退出
				if (DialogResult.Yes.Equals(MessageBox.Show("退出查询?","门诊收费综合查询",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)))
				{
					//this.Close();
				}
			}
			return base.ProcessDialogKey (keyData);
		}
		#endregion

		
		//
		// 函数
		//
        // 挂号信息
        #region 清空挂号信息显示
        /// <summary>
        /// 清空挂号信息的显示
        /// </summary>
        private void Clear()
        {
            // 病历号码
            this.textBoxCardCode.Text = "";
            // 门诊号
            //this.textBoxClinicCode.Text = "";
            // 姓名
            this.textBoxPatientName.Text = "";
            // 性别/年龄
            this.textBoxAgeAndSex.Text = "";
            // 挂号日期
           // this.textBoxRegisteDate.Text = "";
            // 身份证号
            this.textBoxIDCard.Text = "";
            // 出生日期
            this.textBoxBornDate.Text = "";
            // 结算类别: 01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干
            //this.textBoxBalanceType.Text = "";
            // 合同单位
            //this.textBoxPactName.Text = "";
            //// 医疗证号
            //this.textBoxMCardCode.Text = "";
            //// 挂号级别
            //this.textBoxRegistLevel.Text = "";
            //// 挂号科室
            //this.textBoxRegistDepartment.Text = "";
            //// 挂号医生
            //this.textBoxRegistDoctor.Text = "";
            //// 看诊科室
            //this.textBoxSeeDepartment.Text = "";
            //// 看诊医生
            //this.textBoxSeeDoctor.Text = "";
            //// 挂号发票
            //this.textBoxRegisteInvoice.Text = "";
            //// 挂号费
            //this.textBoxFeeRegist.Text = "";
            //// 检查费
            //this.textBoxFeeCheck.Text = "";
            //// 诊察费
            //this.textBoxFeeDiagnose.Text = "";
            //// 附加费
            //this.textBoxFeeOther.Text = "";
            this.SheetFeeDetail.RowCount = 0;
            this.SheetRegistRecord.RowCount = 0;
            this.ucRegPatientInfo1.Clear(true);
            this.ucOrderHistory1.Patient = new FS.HISFC.Models.Registration.Register(); //{4097D044-6360-4895-BFFB-D6E4BBF5AE74}
            this.ucoutemr.RegObj = new FS.HISFC.Models.Registration.Register(); //{4270B15D-35E1-4f95-874E-D552E65BBD26}
        }

        #endregion
        #region 判断条件输入框是否合法
        /// <summary>
		/// 判断条件输入框是否合法
		/// </summary>
		/// <returns>1-合法,-1-不合法</returns>
        //private int JudgeInput()
        //{
        //    // 不允许为空
        //    if (this.textBoxPatientCondition.Text == null || this.textBoxPatientCondition.Text.Equals(""))
        //    {
        //        MessageBox.Show("查询条件不允许为空!");
        //        // 设置焦点到条件输入框
        //        this.textBoxPatientCondition.Focus();

        //        return -1;
        //    }

        //    // 成功返回
        //    return 1;
        //}
		#endregion

		#region 获取挂号时间限制
		/// <summary>
		/// 获取挂号时间限制
		/// [参数1: ref DateTime dtFrom - 起始时间]
		/// [参数2: ref DateTime dtTo - 截止时间]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="dtFrom">起始时间</param>
		/// <param name="dtTo">截止时间</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetDateLimited(ref DateTime dtFrom, ref DateTime dtTo)
		{
			// 起始时间不能大于截止时间
			if (this.dateTimePickerRegisteFrom.Value > this.dateTimePickerRegisteTo.Value)
			{
				MessageBox.Show("起始时间不能大于截止时间!");
				// 设置焦点到起始时间控件
				this.dateTimePickerRegisteFrom.Focus();
				return -1;
			}

			// 起始时间
			dtFrom = this.dateTimePickerRegisteFrom.Value;
			// 截止时间
			dtTo = this.dateTimePickerRegisteTo.Value;

			// 成功返回
			return 1;
		}
		#endregion

		#region 查询挂号信息
		/// <summary>
		/// 查询挂号信息
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <returns>1-成功,-1-失败</returns>
        //private int QueryRegister()
        //{
        //    // 挂号实体
        //    FS.HISFC.Models.Registration.Register tempRegister = new FS.HISFC.Models.Registration.Register();
        //    // 返回的挂号实体数组
        //    ArrayList alRegister = new ArrayList();

        //    // 判断输入是否合法
        //    if (this.JudgeInput() == -1)
        //    {
        //        return -1;
        //    }

        //    // 根据不同的查询方式,设置不同的查询信息,执行不同的查询
        //    switch (this.enumQuery)
        //    {
        //        case QueryType.CardCode:
        //            // 病历号
        //            //tempRegister.PID.CardNO = this.textBoxPatientCondition.Text;

        //            //FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
        //            //ReadMarkNO.ReadMarkNO readMarkNoMana = new ReadMarkNO.ReadMarkNO();
        //            //if (readMarkNoMana.ReadMarkNOByRule(tempRegister.PID.CardNO, ref accountCard) < 0)
        //            //{
        //            //    MessageBox.Show(readMarkNoMana.Error);

        //            //    return -1;
        //            //}

                   
        //            //tempRegister.PID.CardNO = accountCard.Patient.PID.CardNO;

        //            this.textBoxPatientCondition.Text = this.textBoxPatientCondition.Text.Trim().PadLeft(10, '0');
        //            tempRegister.PID.CardNO = this.textBoxPatientCondition.Text;

        //            // 执行查询
        //            this.intReturn = this.GetRegisterListByCardCode(tempRegister, alRegister);
                    
        //            break;
        //        case QueryType.PatientName:
        //            // 姓名
        //            tempRegister.Name = this.textBoxPatientCondition.Text;
        //            // 执行查询
        //            this.intReturn = this.GetRegisterListByName(tempRegister, alRegister);
        //            break;
        //        case QueryType.PactCode:
        //            // 合同单位编码
        //            if (this.textBoxPatientCondition.Tag == null)
        //            {
        //                return -1;
        //            }
        //            tempRegister.Pact.ID = this.textBoxPatientCondition.Tag.ToString();
        //            this.intReturn = this.GetRegisterListByPact(tempRegister, alRegister);
        //            break;
        //        case QueryType.SeeDepartment:
        //            // 挂号科室
        //            if (this.textBoxPatientCondition.Tag == null)
        //            {
        //                return -1;
        //            }
        //            tempRegister.DoctorInfo.Templet.Dept.ID = this.textBoxPatientCondition.Tag.ToString();
        //            this.intReturn = this.GetRegisterListByDepartment(tempRegister, alRegister);
        //            break;
        //        case QueryType.SeeDoctor:
        //            // 开方医生
        //            if (this.textBoxPatientCondition.Tag == null)
        //            {
        //                return -1;
        //            }
        //            tempRegister.DoctorInfo.Templet.Doct.ID = this.textBoxPatientCondition.Tag.ToString();
        //            this.intReturn = this.GetRegisterListByDoctor(tempRegister, alRegister);
        //            break;
        //        case QueryType.MedicareCode:
        //            // 医疗证号
        //            tempRegister.SSN= this.textBoxPatientCondition.Text;
        //            this.intReturn = this.GetRegisterListByMCard(tempRegister, alRegister);
        //            break;
        //        case QueryType.InvoiceCode:
        //            // 发票号
        //            tempRegister.Memo = this.textBoxPatientCondition.Text;
        //            this.intReturn = this.GetRegisterListByInvoice(tempRegister, alRegister);
        //            break;
        //    }

        //    // 判断查询结果查询
        //    if (this.intReturn == -1)
        //    {
        //        return -1;
        //    }

        //    // 设置FarPoint
        //    this.SetRegisterFarPoint(alRegister);

        //    // 成功返回
        //    return 1;
        //}
		#endregion

		#region 根据病历号获取挂号信息
		/// <summary>
		/// 根据病历号获取挂号信息
		/// [参数1: FS.HISFC.Models.Registration.Register argRegister - 含有病历号的挂号实体]
		/// [参数2: ArrayList alRegisters - 返回的挂号记录]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="argRegister">含有病历号的挂号实体</param>
		/// <param name="alRegisters">返回的挂号记录</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetRegisterListByCardCode(FS.HISFC.Models.Registration.Register argRegister, ArrayList alRegisters)
		{
			// 起始时间
			System.DateTime dtFrom = DateTime.MinValue;
			// 截止时间
			System.DateTime dtTo = DateTime.MinValue;

			//
			// 判断是否有时间限制,然后判断查询操作方式,执行不同的查询
			//
            //if (this.boolRegisteDate)
            //{
				// 获取时间限制
				this.intReturn = this.GetDateLimited(ref dtFrom, ref dtTo);
				if (this.intReturn == -1)
				{
					return -1;
				}

				// 判断是哪种查询操作方式
                //if (this.enumOperate.Equals(OperateType.EqueQuery))
                //{
					// 精确查询
					this.intReturn = function.GetRegisterListByCardCode(argRegister, alRegisters, dtFrom, dtTo, false);
                //}
                //else
                //{
                //    // 模糊查询
                //    this.intReturn = function.GetRegisterListByCardCode(argRegister, alRegisters, dtFrom, dtTo, false);
                //}
            //}
            //else
            //{
            //    // 判断是哪种查询操作方式
            //    if (this.enumOperate.Equals(OperateType.EqueQuery))
            //    {
            //        // 精确查询
            //        this.intReturn = function.GetRegisterListByCardCode(argRegister, alRegisters, false);
            //    }
            //    else
            //    {
            //        // 模糊查询
            //        this.intReturn = function.GetRegisterListByCardCode(argRegister, alRegisters, false);
            //    }
            //}

			//
			// 判断结果
			//
			if (this.intReturn == -1)
			{
				MessageBox.Show(function.Err, "获取挂号信息失败!");
				return -1;
			}

			//
			// 成功返回
			//
			return 1;
		}
		#endregion
		#region 根据姓名获取挂号信息
		/// <summary>
		/// 根据病历号获取挂号信息
		/// [参数1: FS.HISFC.Models.Registration.Register argRegister - 含有姓名的挂号实体]
		/// [参数2: ArrayList alRegisters - 返回的挂号记录]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="argRegister">含有姓名的挂号实体</param>
		/// <param name="alRegisters">返回的挂号记录</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetRegisterListByName(FS.HISFC.Models.Registration.Register argRegister, ArrayList alRegisters)
		{
			// 起始时间
			System.DateTime dtFrom = DateTime.MinValue;
			// 截止时间
			System.DateTime dtTo = DateTime.MinValue;

			//
			// 判断是否有时间限制,然后判断查询操作方式,执行不同的查询
			//
			if (this.boolRegisteDate)
			{
				// 获取时间限制
				this.intReturn = this.GetDateLimited(ref dtFrom, ref dtTo);
				if (this.intReturn == -1)
				{
					return -1;
				}

				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByName(argRegister, alRegisters, dtFrom, dtTo, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByName(argRegister, alRegisters, dtFrom, dtTo, true);
				}
			}
			else
			{
				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByName(argRegister, alRegisters, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByName(argRegister, alRegisters, true);
				}
			}

			//
			// 判断结果
			//
			if (this.intReturn == -1)
			{
				MessageBox.Show(function.Err, "获取挂号信息失败!");
				return -1;
			}

			//
			// 成功返回
			//
			return 1;
		}
		#endregion
		#region 根据合同单位获取挂号信息
		/// <summary>
		/// 根据合同单位获取挂号信息
		/// [参数1: FS.HISFC.Models.Registration.Register argRegister - 含有姓名的挂号实体]
		/// [参数2: ArrayList alRegisters - 返回的挂号记录]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="argRegister">含有姓名的挂号实体</param>
		/// <param name="alRegisters">返回的挂号记录</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetRegisterListByPact(FS.HISFC.Models.Registration.Register argRegister, ArrayList alRegisters)
		{
			// 起始时间
			System.DateTime dtFrom = DateTime.MinValue;
			// 截止时间
			System.DateTime dtTo = DateTime.MinValue;

			//
			// 判断是否有时间限制,然后判断查询操作方式,执行不同的查询
			//
			if (this.boolRegisteDate)
			{
				// 获取时间限制
				this.intReturn = this.GetDateLimited(ref dtFrom, ref dtTo);
				if (this.intReturn == -1)
				{
					return -1;
				}

				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByPact(argRegister, alRegisters, dtFrom, dtTo, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByPact(argRegister, alRegisters, dtFrom, dtTo, true);
				}
			}
			else
			{
				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByPact(argRegister, alRegisters, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByPact(argRegister, alRegisters, true);
				}
			}

			//
			// 判断结果
			//
			if (this.intReturn == -1)
			{
				MessageBox.Show(function.Err, "获取挂号信息失败!");
				return -1;
			}

			//
			// 成功返回
			//
			return 1;
		}
		#endregion
		#region 根据挂号科室获取挂号信息
		/// <summary>
		/// 根据挂号科室获取挂号信息
		/// [参数1: FS.HISFC.Models.Registration.Register argRegister - 含有姓名的挂号实体]
		/// [参数2: ArrayList alRegisters - 返回的挂号记录]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="argRegister">含有姓名的挂号实体</param>
		/// <param name="alRegisters">返回的挂号记录</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetRegisterListByDepartment(FS.HISFC.Models.Registration.Register argRegister, ArrayList alRegisters)
		{
			// 起始时间
			System.DateTime dtFrom = DateTime.MinValue;
			// 截止时间
			System.DateTime dtTo = DateTime.MinValue;

			//
			// 判断是否有时间限制,然后判断查询操作方式,执行不同的查询
			//
			if (this.boolRegisteDate)
			{
				// 获取时间限制
				this.intReturn = this.GetDateLimited(ref dtFrom, ref dtTo);
				if (this.intReturn == -1)
				{
					return -1;
				}

				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByDepartment(argRegister, alRegisters, dtFrom, dtTo, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByDepartment(argRegister, alRegisters, dtFrom, dtTo, true);
				}
			}
			else
			{
				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByDepartment(argRegister, alRegisters, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByDepartment(argRegister, alRegisters, true);
				}
			}

			//
			// 判断结果
			//
			if (this.intReturn == -1)
			{
				MessageBox.Show(function.Err, "获取挂号信息失败!");
				return -1;
			}

			//
			// 成功返回
			//
			return 1;
		}
		#endregion
		#region 根据开方医生获取挂号信息
		/// <summary>
		/// 根据开方医生获取挂号信息
		/// [参数1: FS.HISFC.Models.Registration.Register argRegister - 含有姓名的挂号实体]
		/// [参数2: ArrayList alRegisters - 返回的挂号记录]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="argRegister">含有姓名的挂号实体</param>
		/// <param name="alRegisters">返回的挂号记录</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetRegisterListByDoctor(FS.HISFC.Models.Registration.Register argRegister, ArrayList alRegisters)
		{
			// 起始时间
			System.DateTime dtFrom = DateTime.MinValue;
			// 截止时间
			System.DateTime dtTo = DateTime.MinValue;

			//
			// 判断是否有时间限制,然后判断查询操作方式,执行不同的查询
			//
			if (this.boolRegisteDate)
			{
				// 获取时间限制
				this.intReturn = this.GetDateLimited(ref dtFrom, ref dtTo);
				if (this.intReturn == -1)
				{
					return -1;
				}

				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByDoctor(argRegister, alRegisters, dtFrom, dtTo, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByDoctor(argRegister, alRegisters, dtFrom, dtTo, true);
				}
			}
			else
			{
				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByDoctor(argRegister, alRegisters, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByDoctor(argRegister, alRegisters, true);
				}
			}

			//
			// 判断结果
			//
			if (this.intReturn == -1)
			{
				MessageBox.Show(function.Err, "获取挂号信息失败!");
				return -1;
			}

			//
			// 成功返回
			//
			return 1;
		}
		#endregion
		#region 根据医疗证号获取挂号信息
		/// <summary>
		/// 根据医疗证号获取挂号信息
		/// [参数1: FS.HISFC.Models.Registration.Register argRegister - 含有姓名的挂号实体]
		/// [参数2: ArrayList alRegisters - 返回的挂号记录]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="argRegister">含有姓名的挂号实体</param>
		/// <param name="alRegisters">返回的挂号记录</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetRegisterListByMCard(FS.HISFC.Models.Registration.Register argRegister, ArrayList alRegisters)
		{
			// 起始时间
			System.DateTime dtFrom = DateTime.MinValue;
			// 截止时间
			System.DateTime dtTo = DateTime.MinValue;

			//
			// 判断是否有时间限制,然后判断查询操作方式,执行不同的查询
			//
			if (this.boolRegisteDate)
			{
				// 获取时间限制
				this.intReturn = this.GetDateLimited(ref dtFrom, ref dtTo);
				if (this.intReturn == -1)
				{
					return -1;
				}

				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByMCard(argRegister, alRegisters, dtFrom, dtTo, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByMCard(argRegister, alRegisters, dtFrom, dtTo, true);
				}
			}
			else
			{
				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByMCard(argRegister, alRegisters, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByMCard(argRegister, alRegisters, true);
				}
			}

			//
			// 判断结果
			//
			if (this.intReturn == -1)
			{
				MessageBox.Show(function.Err, "获取挂号信息失败!");
				return -1;
			}

			//
			// 成功返回
			//
			return 1;
		}
		#endregion
		#region 根据发票号获取挂号信息
		/// <summary>
		/// 根据发票号获取挂号信息
		/// [参数1: FS.HISFC.Models.Registration.Register argRegister - 含有姓名的挂号实体]
		/// [参数2: ArrayList alRegisters - 返回的挂号记录]
		/// [返回: int,1-成功,-1-失败]
		/// </summary>
		/// <param name="argRegister">含有姓名的挂号实体</param>
		/// <param name="alRegisters">返回的挂号记录</param>
		/// <returns>1-成功,-1-失败</returns>
		private int GetRegisterListByInvoice(FS.HISFC.Models.Registration.Register argRegister, ArrayList alRegisters)
		{
			// 起始时间
			System.DateTime dtFrom = DateTime.MinValue;
			// 截止时间
			System.DateTime dtTo = DateTime.MinValue;

			//
			// 判断是否有时间限制,然后判断查询操作方式,执行不同的查询
			//
			if (this.boolRegisteDate)
			{
				// 获取时间限制
				this.intReturn = this.GetDateLimited(ref dtFrom, ref dtTo);
				if (this.intReturn == -1)
				{
					return -1;
				}

				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByInvoice(argRegister, alRegisters, dtFrom, dtTo, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByInvoice(argRegister, alRegisters, dtFrom, dtTo, true);
				}
			}
			else
			{
				// 判断是哪种查询操作方式
				if (this.enumOperate.Equals(OperateType.EqueQuery))
				{
					// 精确查询
					this.intReturn = function.GetRegisterListByInvoice(argRegister, alRegisters, false);
				}
				else
				{
					// 模糊查询
					this.intReturn = function.GetRegisterListByInvoice(argRegister, alRegisters, true);
				}
			}

			//
			// 判断结果
			//
			if (this.intReturn == -1)
			{
				MessageBox.Show(function.Err, "获取挂号信息失败!");
				return -1;
			}

			//
			// 成功返回
			//
			return 1;
		}
		#endregion

		#region 设置挂号信息结果FarPoint
		/// <summary>
		/// 设置挂号信息结果FarPoint
		/// [参数: ArrayList alRegister - 挂号实体数组]
		/// </summary>
		/// <param name="alRegister">挂号实体数组</param>
		private void SetRegisterFarPoint(ArrayList alRegister)
		{
			// 行号
			int row = 0;

			// 清空FarPoint
			this.ResetSheet(this.SheetRegistRecord);

			// 循环插入FarPoint
			foreach(FS.HISFC.Models.Registration.Register r in alRegister)
			{
				// 增加一行
				row = this.InsertRow(this.SheetRegistRecord);
				
				// 病历号
				this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.CardCode, r.PID.CardNO);
				// 患者姓名
				this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.PatientName, r.Name);
				// 挂号日期
				this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.RegisteDate, r.DoctorInfo.SeeDate.ToString());
				// 结算类别
				switch (r.Pact.PayKind.ID)
				{
					case "01":
						this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.PayKindName, "自费");
						break;
					case "02":
						this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.PayKindName, "医保");
						break;
					case "03":
						this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.PayKindName, "公费");
                        break;
                    //case "04":
                    //    this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.PayKindName, "公费退休");
                    //    break;
                    //case "05":
                    //    this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.PayKindName, "公费高干");
                    //    break;
				}
				// 合同单位
				this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.PactName, r.Pact.Name);
				// 挂号级别
				this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.RegisteLevel, r.DoctorInfo.Templet.RegLevel.Name);
				// 挂号科室
				this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.RegisteDepartment, r.DoctorInfo.Templet.Dept.Name);
                // 挂号医生
                this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.RegisterDoctor, r.DoctorInfo.Templet.Doct.Name);
                // 看诊序号
                this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.SeeNo, r.DoctorInfo.SeeNO.ToString());
                //是否有效
                this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.Valid, r.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Valid ? "有效" : "退号");

                //退号时间
                this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.CancelDate, "--"); 

                this.SheetRegistRecord.Rows[row].BackColor = Color.White;

                if (r.Status != FS.HISFC.Models.Base.EnumRegisterStatus.Valid)
                {
                    this.SheetRegistRecord.Rows[row].BackColor = Color.MistyRose;
                    //退号时间
                    this.SetCell(this.SheetRegistRecord, row, (int)RegisterField.CancelDate, r.CancelOper.OperTime.ToString()); 
                }

				// 行Tag
				this.SetRowTag(this.SheetRegistRecord, row, r);
			}
            foreach (FarPoint.Win.Spread.Column col in this.SheetRegistRecord.Columns)
            {
                col.Width = col.GetPreferredWidth();
            }
		}
		#endregion

		#region 清空FarPoint
		/// <summary>
		/// 清空FarPoint
		/// [参数: FarPoint.Win.Spread.SheetView sheet - FarPoint页]
		/// </summary>
		/// <param name="sheet">FarPoint页</param>
		private void ResetSheet(FarPoint.Win.Spread.SheetView sheet)
		{
			sheet.RowCount = 0;
		}
		#endregion
		#region 在FarPoint增加一行
		/// <summary>
		/// 在FarPoint增加一行
		/// [参数: FarPoint.Win.Spread.SheetView sheet - FarPoint页]
		/// [返回: 增加后的行号]
		/// </summary>
		/// <param name="sheet">FarPoint页</param>
		/// <returns>增加后的行号</returns>
		private int InsertRow(FarPoint.Win.Spread.SheetView sheet)
		{
			// 临时行号
			int row = 0;
			// 增加一行
			sheet.AddRows(sheet.RowCount, 1);
			// 获取行号
			row = sheet.RowCount - 1;
			// 返回行号
			return row;
		}
		#endregion
		#region 设置FarPoint一个Cell的值
		/// <summary>
		/// 设置FarPoint一个Cell的值
		/// [参数1: FarPoint.Win.Spread.SheetView sheet - FarPoint页]
		/// [参数2: int row - 行号]
		/// [参数3: int col - 列号]
		/// [参数4: string stringValue - 值]
		/// </summary>
		/// <param name="sheet">FarPoint页</param>
		/// <param name="row">行号</param>
		/// <param name="col">列号</param>
		/// <param name="stringValue">值</param>
		private void SetCell(FarPoint.Win.Spread.SheetView sheet, int row, int col, string stringValue)
		{
			sheet.Cells[row, col].Text = stringValue;
		}
		#endregion
		#region 设置FarPoint某一行的Tag
		/// <summary>
		/// 设置FarPoint某一行的Tag
		/// [参数1: FarPoint.Win.Spread.SheetView sheet - FarPoint页]
		/// [参数2: int row - 行号]
		/// [参数3: System.Object objectTag - Tag值]
		/// </summary>
		/// <param name="sheet">FarPoint页</param>
		/// <param name="row">行号</param>
		/// <param name="objectTag">Tag值</param>
		private void SetRowTag(FarPoint.Win.Spread.SheetView sheet, int row, System.Object objectTag)
		{
			sheet.Rows[row].Tag = objectTag;
		}
		#endregion
		#region 获取FarPoint某一行的Tag
		/// <summary>
		/// 获取FarPoint某一行的Tag
		/// [参数1: FarPoint.Win.Spread.SheetView sheet - FarPoint页]
		/// [参数2: int row - 行号]
		/// [返回: Tag]
		/// </summary>
		/// <param name="sheet">FarPoint页</param>
		/// <param name="row">行号</param>
		/// <returns>Tag</returns>
		private System.Object GetRowTag(FarPoint.Win.Spread.SheetView sheet, int row)
		{
			return sheet.Rows[row].Tag;
		}
		#endregion

		#region 导出
		/// <summary>
		/// 导出
		/// </summary>
		private void Export()
		{
			string Result ="";
			try
			{
				bool ret = false;
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
				saveFileDialog1.Filter = "Excel |.xls";
				if(saveFileDialog1.ShowDialog()==DialogResult.OK)
				{
					if(saveFileDialog1.FileName != "")
					{
						//以Excel 的形式导出数据
						ret = this.fpSpreadFeeDetail.SaveExcel(saveFileDialog1.FileName,FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
					}
					if(ret)
					{
						MessageBox.Show("成功导出数据");
					}
				}
			}
			catch(Exception ee)
			{
				Result = ee.Message;
				MessageBox.Show(Result);
			}
		}
		#endregion 

        protected override int OnQuery(object sender, object neuObject)
        {
            //if (e.Button.Equals(this.toolBarButtonQuery))
            //{
                // 如果单击了查询按钮,那么执行查询
               // this.QueryRegister();
            //}
            //else if (e.Button.Equals(this.tbExpert))
            //{
            //    // 如果单击了查询按钮,那么执行查询
            //    this.Export();
            //}
            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            //if (e.Button.Equals(this.toolBarButtonQuery))
            //{
            //    // 如果单击了查询按钮,那么执行查询
            //    this.QueryRegister();
            //}
            //else if (e.Button.Equals(this.tbExpert))
            //{
                // 如果单击了查询按钮,那么执行查询
                this.Export();
            //}
            return base.Export(sender, neuObject);
        }

        //围产信息查询
        //private void btnOBISQuery_Click(object sender, EventArgs e)
        //{
        //    HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

        //    string QueryStr = this.tbOBISQuery.Text;

        //    if (string.IsNullOrEmpty(QueryStr))
        //    {
        //        return;
        //    }

        //    frmQuery.QueryByName(QueryStr);
        //    frmQuery.ShowDialog();

        //    if (frmQuery.DialogResult == DialogResult.OK)
        //    {
        //        string cardNO = frmQuery.PatientInfo.PID.CardNO;
        //        if (string.IsNullOrEmpty(cardNO))
        //        {
        //            MessageBox.Show("未查询到相关信息！");
        //            return;
        //        }

        //        HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
        //        register.PID.CardNO = cardNO;
        //        this.ucOBISBrowser1.Register = register;
        //    }

        //}



        //检验结果查询
        //private void btnLisQuery_Click(object sender, EventArgs e)
        //{
        //    HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

        //    string QueryStr = this.tbLisQuery.Text;

        //    if (string.IsNullOrEmpty(QueryStr))
        //    {
        //        return;
        //    }

        //    frmQuery.QueryByName(QueryStr);
        //    frmQuery.ShowDialog();

        //    if (frmQuery.DialogResult == DialogResult.OK)
        //    {
        //        string cardNO = frmQuery.PatientInfo.PID.CardNO;
        //        if (string.IsNullOrEmpty(cardNO))
        //        {
        //            MessageBox.Show("未查询到相关信息！");
        //            return;
        //        }

        //        HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
        //        register.PID.CardNO = cardNO;
        //        if (register.DoctorInfo == null)
        //        {
        //            register.DoctorInfo = new FS.HISFC.Models.Registration.Schema();
        //        }
        //        register.DoctorInfo.SeeDate = DateTime.Now;
        //        this.ucLisResQuery1.Register = register;
        //    }

        //}

        //检查结果查询
        //private void btnPacsQuery_Click(object sender, EventArgs e)
        //{
        //    HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

        //    string QueryStr = this.tbPacsQuery.Text;

        //    if (string.IsNullOrEmpty(QueryStr))
        //    {
        //        return;
        //    }

        //    frmQuery.QueryByName(QueryStr);
        //    frmQuery.ShowDialog();

        //    if (frmQuery.DialogResult == DialogResult.OK)
        //    {
        //        string cardNO = frmQuery.PatientInfo.PID.CardNO;
        //        if (string.IsNullOrEmpty(cardNO))
        //        {
        //            MessageBox.Show("未查询到相关信息！");
        //            return;
        //        }

        //        HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
        //        register.PID.CardNO = cardNO;
        //        if(register.DoctorInfo == null)
        //        {
        //            register.DoctorInfo = new FS.HISFC.Models.Registration.Schema();
        //        }
        //        register.DoctorInfo.SeeDate = DateTime.Now;
        //        this.ucPacsResQuery1.Register = register;
        //    }

        //}

        //private void btnPackageQuery_Click(object sender, EventArgs e)
        //{
        //    HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

        //    string QueryStr = this.tbPackageQuery.Text;

        //    if (string.IsNullOrEmpty(QueryStr))
        //    {
        //        return;
        //    }

        //    frmQuery.QueryByName(QueryStr);
        //    frmQuery.ShowDialog();

        //    if (frmQuery.DialogResult == DialogResult.OK)
        //    {
        //        string cardNO = frmQuery.PatientInfo.PID.CardNO;
        //        if (string.IsNullOrEmpty(cardNO))
        //        {
        //            MessageBox.Show("未查询到相关信息！");
        //            return;
        //        }

        //        this.packageQuery.PatientInfo = frmQuery.PatientInfo;
        //    }
        //}

        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox queryControl = sender as TextBox;
                string QueryStr = queryControl.Text.Trim();

                //bool result = Regex.IsMatch(txtCardNO.Text, "^([0-9]{1,})$");
                //if (result)
                //{
                //    result =Regex .IsMatch (txtCardNO .Text ,@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$ ");
                //}
                ////病历号默认进行补全
                if ((sender as TextBox).Name == "txtCardNO")
                {
                    queryPatientList(this.txtCardNO.Text);
                    // GetPatient();
                }
            }
        }

        /// <summary>
        ///  根据条件查询人员信息 //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} 修改综合查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool queryPatientList(string condition)
        {
            this.Clear();
            Form patientForm = new Form();
            ucPatientList patientList = new ucPatientList();
            patientForm.Size = patientList.Size;
            patientForm.Controls.Add(patientList);
            patientList.QueryCondition = condition;
            patientForm.StartPosition = FormStartPosition.Manual;
            patientForm.Location = new Point(PointToScreen(this.txtCardNO.Location).X, PointToScreen(this.txtCardNO.Location).Y + this.txtCardNO.Height * 2);
            patientForm.FormBorderStyle = FormBorderStyle.None;
            patientForm.ShowInTaskbar = false;
            patientList.patientInfo = patientInfoSet;

            if (patientList.patientList != null && patientList.patientList.Count > 0)
            {
                patientForm.ShowDialog();
                return true;
            }
            else
            {
                MessageBox.Show("没有该患者信息！");
                // tbName.Text = patient.Name;
            }

            return false;
        }

        /// <summary>
        ///根据选择的人员信息设置综合查询信息  //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} 修改综合查询
        /// </summary>
        /// <param name="patient"></param>
        private void patientInfoSet(PatientInfo patient)
        {
            // 挂号实体
            FS.HISFC.Models.Registration.Register tempRegister = new FS.HISFC.Models.Registration.Register();
            // 返回的挂号实体数组
            ArrayList alRegister = new ArrayList();
            tempRegister.PID.CardNO = patient.PID.CardNO;

            // 病历号码
            this.textBoxCardCode.Text = patient.PID.CardNO;
            // 门诊号
            // this.textBoxClinicCode.Text = this.register.ID;
            // 姓名
            this.textBoxPatientName.Text = patient.Name;
            // 性别/年龄
            this.textBoxAgeAndSex.Text = patient.Sex.Name + "|" + departmentFunction.GetAge(patient.Birthday);
            // 挂号日期
            //this.textBoxRegisteDate.Text = this.register.DoctorInfo.SeeDate.ToString();
            // 身份证号
            this.textBoxIDCard.Text = patient.IDCard;
            // 出生日期
            this.textBoxBornDate.Text = patient.Birthday.Date.ToString();







            // 执行查询
            this.intReturn = this.GetRegisterListByCardCode(tempRegister, alRegister);
            // 判断查询结果查询
            if (this.intReturn == -1)
            {
                return;
            }
            this.ucRegPatientInfo1.Clear(true);
            this.ucRegPatientInfo1.CardNO = patient.PID.CardNO;
            // 设置FarPoint
            this.SetRegisterFarPoint(alRegister);

            (this.ucOrderHistory1 as FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).SetTime();
            (this.ucOrderHistory1 as FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).Query(patient.PID.CardNO);
            orderQueryShow.patientInfo = patient;

            //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
            this.ucEMRQuery1.Patient = patient;
            this.ucPacsResQuery1.Patient = patient;
            //{1954741E-939E-4c1e-913E-13533E8E7004}
            this.ucOBISBrowser1.IsReadOnly = true;
            this.ucOBISBrowser1.Patient = patient;
            this.ucLisResQuery1.ShowData(tempRegister, this.dateTimePickerRegisteFrom.Value, this.dateTimePickerRegisteTo.Value);
            this.packageQuery.PatientInfo = patient;
            //this.ucoutemr.OutneedSave = false; {4097D044-6360-4895-BFFB-D6E4BBF5AE74}
            this.ucoutemr.Clear();
           if (alRegister.Count > 0)
            {
                alRegister.Sort(new myRegCompare());
                this.ucoutemr.RegObj = alRegister[0] as FS.HISFC.Models.Registration.Register;
                this.ucoutemr.InitTreeCase();
            }
            else
            {
                this.ucoutemr.PatientId = patient.ID;
                this.ucoutemr.InitTreeCase();
            }
               
           
        }

        /// <summary>
        /// 比较器{4097D044-6360-4895-BFFB-D6E4BBF5AE74}
        /// </summary>
        public class myRegCompare : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                DateTime reg1 = ((FS.HISFC.Models.Registration.Register)x).DoctorInfo.SeeDate;
                DateTime reg2 = ((FS.HISFC.Models.Registration.Register)y).DoctorInfo.SeeDate;


                if (reg1 >= reg2)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
                //return ((FS.HISFC.Models.Registration.Register)x).DoctorInfo.SeeDate.CompareTo(((FS.HISFC.Models.Registration.Register)y).DoctorInfo.SeeDate);

            }
        }
    }

    
}
