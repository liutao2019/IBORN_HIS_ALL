using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms; 

namespace FS.HISFC.Components.Order.Controls
{
    partial class ucConsultation
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
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucConsultation));
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelFun = new System.Windows.Forms.Panel();
            this.lblHosName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLblInpaientNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkCreateOrder = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkOuthos = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.cmbDoctor = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.rtbResult = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.neuLabel26 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtPreConsultation = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel25 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDoc = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel22 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbAppDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel23 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel21 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel20 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtConsultation = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.rtbSource = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.neuLabel19 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel18 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblBedID = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkEmergency = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chkCommon = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label16 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnPatientInfo = new System.Windows.Forms.Panel();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryInpatientNO = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.cbxOutPrint = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ucUserText1 = new FS.HISFC.Components.Common.Controls.ucUserText();
            this.Panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.PrintNotice = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuButton4 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton3 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.lblState = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkTemplet = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuPanel1.SuspendLayout();
            this.panelFun.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.pnPatientInfo.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.AllowDrop = true;
            this.neuPanel1.Controls.Add(this.panelFun);
            this.neuPanel1.Controls.Add(this.pnPatientInfo);
            this.neuPanel1.Controls.Add(this.splitter1);
            this.neuPanel1.Controls.Add(this.ucUserText1);
            this.neuPanel1.Controls.Add(this.Panel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(929, 548);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // panelFun
            // 
            this.panelFun.AutoScroll = true;
            this.panelFun.BackColor = System.Drawing.Color.White;
            this.panelFun.Controls.Add(this.lblHosName);
            this.panelFun.Controls.Add(this.neuLblInpaientNo);
            this.panelFun.Controls.Add(this.chkCreateOrder);
            this.panelFun.Controls.Add(this.chkOuthos);
            this.panelFun.Controls.Add(this.panel2);
            this.panelFun.Controls.Add(this.neuGroupBox1);
            this.panelFun.Controls.Add(this.lblBedID);
            this.panelFun.Controls.Add(this.chkEmergency);
            this.panelFun.Controls.Add(this.chkCommon);
            this.panelFun.Controls.Add(this.lblDept);
            this.panelFun.Controls.Add(this.label16);
            this.panelFun.Controls.Add(this.label14);
            this.panelFun.Controls.Add(this.label12);
            this.panelFun.Controls.Add(this.lblTitle);
            this.panelFun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFun.Location = new System.Drawing.Point(177, 35);
            this.panelFun.Name = "panelFun";
            this.panelFun.Size = new System.Drawing.Size(752, 372);
            this.panelFun.TabIndex = 2;
            // 
            // lblHosName
            // 
            this.lblHosName.Font = new System.Drawing.Font("宋体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHosName.Location = new System.Drawing.Point(17, 33);
            this.lblHosName.Name = "lblHosName";
            this.lblHosName.Size = new System.Drawing.Size(715, 24);
            this.lblHosName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblHosName.TabIndex = 46;
            this.lblHosName.Text = "测试医院";
            this.lblHosName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLblInpaientNo
            // 
            this.neuLblInpaientNo.AutoSize = true;
            this.neuLblInpaientNo.Font = new System.Drawing.Font("宋体", 12F);
            this.neuLblInpaientNo.Location = new System.Drawing.Point(18, 158);
            this.neuLblInpaientNo.Name = "neuLblInpaientNo";
            this.neuLblInpaientNo.Size = new System.Drawing.Size(88, 16);
            this.neuLblInpaientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblInpaientNo.TabIndex = 44;
            this.neuLblInpaientNo.Text = "0000022222";
            // 
            // chkCreateOrder
            // 
            this.chkCreateOrder.AutoSize = true;
            this.chkCreateOrder.Checked = true;
            this.chkCreateOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateOrder.Font = new System.Drawing.Font("宋体", 12F);
            this.chkCreateOrder.Location = new System.Drawing.Point(552, 130);
            this.chkCreateOrder.Name = "chkCreateOrder";
            this.chkCreateOrder.Size = new System.Drawing.Size(123, 20);
            this.chkCreateOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkCreateOrder.TabIndex = 42;
            this.chkCreateOrder.Text = "能否开立医嘱";
            this.chkCreateOrder.UseVisualStyleBackColor = true;
            this.chkCreateOrder.CheckedChanged += new System.EventHandler(this.chkCreateOrder_CheckedChanged);
            // 
            // chkOuthos
            // 
            this.chkOuthos.AutoSize = true;
            this.chkOuthos.Checked = true;
            this.chkOuthos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOuthos.Font = new System.Drawing.Font("宋体", 12F);
            this.chkOuthos.Location = new System.Drawing.Point(552, 108);
            this.chkOuthos.Name = "chkOuthos";
            this.chkOuthos.Size = new System.Drawing.Size(91, 20);
            this.chkOuthos.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkOuthos.TabIndex = 41;
            this.chkOuthos.Text = "院外会诊";
            this.chkOuthos.UseVisualStyleBackColor = true;
            this.chkOuthos.CheckedChanged += new System.EventHandler(this.chkOuthos_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(24, 585);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 2);
            this.panel2.TabIndex = 18;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.AllowDrop = true;
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.txtResult);
            this.neuGroupBox1.Controls.Add(this.txtSource);
            this.neuGroupBox1.Controls.Add(this.cmbDoctor);
            this.neuGroupBox1.Controls.Add(this.cmbDept);
            this.neuGroupBox1.Controls.Add(this.rtbResult);
            this.neuGroupBox1.Controls.Add(this.neuLabel26);
            this.neuGroupBox1.Controls.Add(this.dtPreConsultation);
            this.neuGroupBox1.Controls.Add(this.neuLabel25);
            this.neuGroupBox1.Controls.Add(this.lblDoc);
            this.neuGroupBox1.Controls.Add(this.neuLabel22);
            this.neuGroupBox1.Controls.Add(this.cmbAppDept);
            this.neuGroupBox1.Controls.Add(this.neuLabel23);
            this.neuGroupBox1.Controls.Add(this.dtEnd);
            this.neuGroupBox1.Controls.Add(this.neuLabel21);
            this.neuGroupBox1.Controls.Add(this.dtBegin);
            this.neuGroupBox1.Controls.Add(this.neuLabel20);
            this.neuGroupBox1.Controls.Add(this.dtConsultation);
            this.neuGroupBox1.Controls.Add(this.rtbSource);
            this.neuGroupBox1.Controls.Add(this.neuLabel19);
            this.neuGroupBox1.Controls.Add(this.neuLabel14);
            this.neuGroupBox1.Controls.Add(this.neuLabel15);
            this.neuGroupBox1.Controls.Add(this.neuLabel18);
            this.neuGroupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.neuGroupBox1.Location = new System.Drawing.Point(17, 178);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(715, 799);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 19;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.neuGroupBox1_Paint);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(82, 412);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(587, 342);
            this.txtResult.TabIndex = 48;
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(81, 18);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(587, 311);
            this.txtSource.TabIndex = 47;
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.IsEnter2Tab = false;
            this.cmbDoctor.IsFlat = false;
            this.cmbDoctor.IsLike = true;
            this.cmbDoctor.IsListOnly = false;
            this.cmbDoctor.IsPopForm = true;
            this.cmbDoctor.IsShowCustomerList = false;
            this.cmbDoctor.IsShowID = false;
            this.cmbDoctor.IsShowIDAndName = false;
            this.cmbDoctor.Location = new System.Drawing.Point(304, 763);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.ShowCustomerList = false;
            this.cmbDoctor.ShowID = false;
            this.cmbDoctor.Size = new System.Drawing.Size(121, 24);
            this.cmbDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoctor.TabIndex = 5;
            this.cmbDoctor.Tag = "";
            this.cmbDoctor.ToolBarUse = false;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(82, 763);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(121, 24);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 3;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // rtbResult
            // 
            this.rtbResult.Enabled = false;
            this.rtbResult.Location = new System.Drawing.Point(10, 472);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(66, 123);
            this.rtbResult.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rtbResult.TabIndex = 19;
            this.rtbResult.Text = "";
            this.rtbResult.Visible = false;
            // 
            // neuLabel26
            // 
            this.neuLabel26.AutoSize = true;
            this.neuLabel26.Location = new System.Drawing.Point(6, 415);
            this.neuLabel26.Name = "neuLabel26";
            this.neuLabel26.Size = new System.Drawing.Size(88, 16);
            this.neuLabel26.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel26.TabIndex = 18;
            this.neuLabel26.Text = "会诊意见：";
            // 
            // dtPreConsultation
            // 
            this.dtPreConsultation.Checked = false;
            this.dtPreConsultation.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtPreConsultation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPreConsultation.IsEnter2Tab = false;
            this.dtPreConsultation.Location = new System.Drawing.Point(496, 339);
            this.dtPreConsultation.Name = "dtPreConsultation";
            this.dtPreConsultation.ShowCheckBox = true;
            this.dtPreConsultation.Size = new System.Drawing.Size(175, 26);
            this.dtPreConsultation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtPreConsultation.TabIndex = 17;
            // 
            // neuLabel25
            // 
            this.neuLabel25.AutoSize = true;
            this.neuLabel25.Location = new System.Drawing.Point(420, 342);
            this.neuLabel25.Name = "neuLabel25";
            this.neuLabel25.Size = new System.Drawing.Size(88, 16);
            this.neuLabel25.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel25.TabIndex = 16;
            this.neuLabel25.Text = "申请日期：";
            // 
            // lblDoc
            // 
            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(334, 342);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(40, 16);
            this.lblDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoc.TabIndex = 15;
            this.lblDoc.Text = "未知";
            // 
            // neuLabel22
            // 
            this.neuLabel22.AutoSize = true;
            this.neuLabel22.Location = new System.Drawing.Point(260, 342);
            this.neuLabel22.Name = "neuLabel22";
            this.neuLabel22.Size = new System.Drawing.Size(88, 16);
            this.neuLabel22.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel22.TabIndex = 14;
            this.neuLabel22.Text = "申请医师：";
            // 
            // cmbAppDept
            // 
            this.cmbAppDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbAppDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbAppDept.FormattingEnabled = true;
            this.cmbAppDept.IsEnter2Tab = false;
            this.cmbAppDept.IsFlat = false;
            this.cmbAppDept.IsLike = true;
            this.cmbAppDept.IsListOnly = false;
            this.cmbAppDept.IsPopForm = true;
            this.cmbAppDept.IsShowCustomerList = false;
            this.cmbAppDept.IsShowID = false;
            this.cmbAppDept.IsShowIDAndName = false;
            this.cmbAppDept.Location = new System.Drawing.Point(83, 339);
            this.cmbAppDept.Name = "cmbAppDept";
            this.cmbAppDept.ShowCustomerList = false;
            this.cmbAppDept.ShowID = false;
            this.cmbAppDept.Size = new System.Drawing.Size(153, 24);
            this.cmbAppDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbAppDept.TabIndex = 13;
            this.cmbAppDept.Tag = "";
            this.cmbAppDept.ToolBarUse = false;
            // 
            // neuLabel23
            // 
            this.neuLabel23.AutoSize = true;
            this.neuLabel23.Location = new System.Drawing.Point(5, 342);
            this.neuLabel23.Name = "neuLabel23";
            this.neuLabel23.Size = new System.Drawing.Size(88, 16);
            this.neuLabel23.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel23.TabIndex = 12;
            this.neuLabel23.Text = "申请科室：";
            // 
            // dtEnd
            // 
            this.dtEnd.Checked = false;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(498, 374);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(175, 26);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 11;
            // 
            // neuLabel21
            // 
            this.neuLabel21.AutoSize = true;
            this.neuLabel21.Location = new System.Drawing.Point(420, 379);
            this.neuLabel21.Name = "neuLabel21";
            this.neuLabel21.Size = new System.Drawing.Size(88, 16);
            this.neuLabel21.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel21.TabIndex = 10;
            this.neuLabel21.Text = "截至日期：";
            // 
            // dtBegin
            // 
            this.dtBegin.Checked = false;
            this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(83, 374);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(153, 26);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 9;
            // 
            // neuLabel20
            // 
            this.neuLabel20.AutoSize = true;
            this.neuLabel20.Location = new System.Drawing.Point(5, 379);
            this.neuLabel20.Name = "neuLabel20";
            this.neuLabel20.Size = new System.Drawing.Size(88, 16);
            this.neuLabel20.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel20.TabIndex = 8;
            this.neuLabel20.Text = "授权日期：";
            // 
            // dtConsultation
            // 
            this.dtConsultation.Checked = false;
            this.dtConsultation.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtConsultation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtConsultation.IsEnter2Tab = false;
            this.dtConsultation.Location = new System.Drawing.Point(505, 763);
            this.dtConsultation.Name = "dtConsultation";
            this.dtConsultation.ShowCheckBox = true;
            this.dtConsultation.Size = new System.Drawing.Size(166, 26);
            this.dtConsultation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtConsultation.TabIndex = 7;
            // 
            // rtbSource
            // 
            this.rtbSource.Location = new System.Drawing.Point(10, 37);
            this.rtbSource.Name = "rtbSource";
            this.rtbSource.Size = new System.Drawing.Size(51, 91);
            this.rtbSource.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rtbSource.TabIndex = 1;
            this.rtbSource.Text = "";
            this.rtbSource.Visible = false;
            // 
            // neuLabel19
            // 
            this.neuLabel19.AutoSize = true;
            this.neuLabel19.Location = new System.Drawing.Point(429, 766);
            this.neuLabel19.Name = "neuLabel19";
            this.neuLabel19.Size = new System.Drawing.Size(88, 16);
            this.neuLabel19.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel19.TabIndex = 6;
            this.neuLabel19.Text = "会诊日期：";
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Location = new System.Drawing.Point(5, 18);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(88, 16);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 0;
            this.neuLabel14.Text = "会诊摘要：";
            // 
            // neuLabel15
            // 
            this.neuLabel15.AutoSize = true;
            this.neuLabel15.Location = new System.Drawing.Point(6, 766);
            this.neuLabel15.Name = "neuLabel15";
            this.neuLabel15.Size = new System.Drawing.Size(88, 16);
            this.neuLabel15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel15.TabIndex = 2;
            this.neuLabel15.Text = "会诊科室：";
            // 
            // neuLabel18
            // 
            this.neuLabel18.AutoSize = true;
            this.neuLabel18.Location = new System.Drawing.Point(229, 766);
            this.neuLabel18.Name = "neuLabel18";
            this.neuLabel18.Size = new System.Drawing.Size(88, 16);
            this.neuLabel18.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel18.TabIndex = 4;
            this.neuLabel18.Text = "会诊医师：";
            // 
            // lblBedID
            // 
            this.lblBedID.AutoSize = true;
            this.lblBedID.Font = new System.Drawing.Font("宋体", 12F);
            this.lblBedID.Location = new System.Drawing.Point(605, 158);
            this.lblBedID.Name = "lblBedID";
            this.lblBedID.Size = new System.Drawing.Size(56, 16);
            this.lblBedID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBedID.TabIndex = 18;
            this.lblBedID.Text = "加24床";
            // 
            // chkEmergency
            // 
            this.chkEmergency.AutoSize = true;
            this.chkEmergency.Font = new System.Drawing.Font("宋体", 12F);
            this.chkEmergency.Location = new System.Drawing.Point(459, 130);
            this.chkEmergency.Name = "chkEmergency";
            this.chkEmergency.Size = new System.Drawing.Size(91, 20);
            this.chkEmergency.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkEmergency.TabIndex = 14;
            this.chkEmergency.Text = "紧急会诊";
            this.chkEmergency.UseVisualStyleBackColor = true;
            this.chkEmergency.CheckedChanged += new System.EventHandler(this.chkEmergency_CheckedChanged);
            // 
            // chkCommon
            // 
            this.chkCommon.AutoSize = true;
            this.chkCommon.Checked = true;
            this.chkCommon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommon.Font = new System.Drawing.Font("宋体", 12F);
            this.chkCommon.Location = new System.Drawing.Point(459, 108);
            this.chkCommon.Name = "chkCommon";
            this.chkCommon.Size = new System.Drawing.Size(91, 20);
            this.chkCommon.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkCommon.TabIndex = 13;
            this.chkCommon.Text = "普通会诊";
            this.chkCommon.UseVisualStyleBackColor = true;
            this.chkCommon.CheckedChanged += new System.EventHandler(this.chkCommon_CheckedChanged);
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("宋体", 12F);
            this.lblDept.Location = new System.Drawing.Point(379, 158);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(120, 16);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 8;
            this.lblDept.Text = "结直肠肛门外科";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F);
            this.label16.Location = new System.Drawing.Point(292, 158);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 16);
            this.label16.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label16.TabIndex = 6;
            this.label16.Text = "102岁";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F);
            this.label14.Location = new System.Drawing.Point(252, 158);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label14.TabIndex = 4;
            this.label14.Text = "女";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F);
            this.label12.Location = new System.Drawing.Point(119, 158);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 16);
            this.label12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label12.TabIndex = 2;
            this.label12.Text = "默罕默德林肯";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(17, 71);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(715, 24);
            this.lblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "会　诊　记　录　单";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnPatientInfo
            // 
            this.pnPatientInfo.Controls.Add(this.lblPatientInfo);
            this.pnPatientInfo.Controls.Add(this.ucQueryInpatientNO);
            this.pnPatientInfo.Controls.Add(this.cbxOutPrint);
            this.pnPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnPatientInfo.Location = new System.Drawing.Point(177, 0);
            this.pnPatientInfo.Name = "pnPatientInfo";
            this.pnPatientInfo.Size = new System.Drawing.Size(752, 35);
            this.pnPatientInfo.TabIndex = 3;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Enabled = false;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.Black;
            this.lblPatientInfo.Location = new System.Drawing.Point(278, 13);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(437, 14);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 17;
            this.lblPatientInfo.Text = "住院号输入框内[F2]可以切换查询方式：住院号、床号、姓名等!";
            // 
            // ucQueryInpatientNO
            // 
            this.ucQueryInpatientNO.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ucQueryInpatientNO.DefaultInputType = 0;
            this.ucQueryInpatientNO.Enabled = false;
            this.ucQueryInpatientNO.Font = new System.Drawing.Font("宋体", 10F);
            this.ucQueryInpatientNO.InputType = 0;
            this.ucQueryInpatientNO.IsDeptOnly = true;
            this.ucQueryInpatientNO.Location = new System.Drawing.Point(97, 4);
            this.ucQueryInpatientNO.Name = "ucQueryInpatientNO";
            this.ucQueryInpatientNO.PatientInState = "ALL";
            this.ucQueryInpatientNO.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNO.Size = new System.Drawing.Size(179, 27);
            this.ucQueryInpatientNO.TabIndex = 16;
            this.ucQueryInpatientNO.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNO_myEvent);
            // 
            // cbxOutPrint
            // 
            this.cbxOutPrint.AutoSize = true;
            this.cbxOutPrint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxOutPrint.ForeColor = System.Drawing.Color.Red;
            this.cbxOutPrint.Location = new System.Drawing.Point(6, 9);
            this.cbxOutPrint.Name = "cbxOutPrint";
            this.cbxOutPrint.Size = new System.Drawing.Size(95, 20);
            this.cbxOutPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbxOutPrint.TabIndex = 15;
            this.cbxOutPrint.Text = "出院打印";
            this.cbxOutPrint.UseVisualStyleBackColor = true;
            this.cbxOutPrint.CheckedChanged += new System.EventHandler(this.cbxOutPrint_CheckedChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(173, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 407);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // ucUserText1
            // 
            this.ucUserText1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucUserText1.GroupInfo = null;
            this.ucUserText1.Location = new System.Drawing.Point(0, 0);
            this.ucUserText1.Name = "ucUserText1";
            this.ucUserText1.Size = new System.Drawing.Size(173, 407);
            this.ucUserText1.TabIndex = 0;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.White;
            this.Panel1.Controls.Add(this.PrintNotice);
            this.Panel1.Controls.Add(this.fpSpread1);
            this.Panel1.Controls.Add(this.neuButton4);
            this.Panel1.Controls.Add(this.neuButton3);
            this.Panel1.Controls.Add(this.neuButton2);
            this.Panel1.Controls.Add(this.neuButton1);
            this.Panel1.Controls.Add(this.lblState);
            this.Panel1.Controls.Add(this.chkTemplet);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 407);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(929, 141);
            this.Panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.Panel1.TabIndex = 0;
            this.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // PrintNotice
            // 
            this.PrintNotice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintNotice.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PrintNotice.ForeColor = System.Drawing.Color.Red;
            this.PrintNotice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PrintNotice.Location = new System.Drawing.Point(821, 8);
            this.PrintNotice.Name = "PrintNotice";
            this.PrintNotice.Size = new System.Drawing.Size(82, 23);
            this.PrintNotice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.PrintNotice.TabIndex = 7;
            this.PrintNotice.Text = "打印通知单";
            this.PrintNotice.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.PrintNotice.UseVisualStyleBackColor = true;
            this.PrintNotice.Click += new System.EventHandler(this.PrintNotice_Click);
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1";
            this.fpSpread1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 40);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(929, 101);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpSpread1_SelectionChanged);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 9;
            this.fpSpread1_Sheet1.RowCount = 1;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "会诊科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "会诊专家";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "申请人";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "申请日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "会诊原因及病理诊断";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "会诊结果";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "状态";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "能否开立医嘱";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "审核人";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "会诊科室";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 132F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "会诊专家";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 132F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "申请人";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 132F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2007, 1, 9, 11, 29, 20, 0);
            dateTimeCellType1.TimeDefault = new System.DateTime(2007, 1, 9, 11, 29, 20, 0);
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = dateTimeCellType1;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "申请日期";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 132F;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "会诊原因及病理诊断";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 132F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "会诊结果";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 132F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "状态";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 132F;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "能否开立医嘱";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 132F;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuButton4
            // 
            this.neuButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.neuButton4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuButton4.ForeColor = System.Drawing.Color.Red;
            this.neuButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuButton4.Location = new System.Drawing.Point(739, 8);
            this.neuButton4.Name = "neuButton4";
            this.neuButton4.Size = new System.Drawing.Size(75, 23);
            this.neuButton4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton4.TabIndex = 5;
            this.neuButton4.Text = "打印(&P)";
            this.neuButton4.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton4.UseVisualStyleBackColor = true;
            this.neuButton4.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // neuButton3
            // 
            this.neuButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.neuButton3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuButton3.ForeColor = System.Drawing.Color.Red;
            this.neuButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuButton3.Location = new System.Drawing.Point(649, 8);
            this.neuButton3.Name = "neuButton3";
            this.neuButton3.Size = new System.Drawing.Size(75, 23);
            this.neuButton3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton3.TabIndex = 4;
            this.neuButton3.Text = "删除(&D)";
            this.neuButton3.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton3.UseVisualStyleBackColor = true;
            this.neuButton3.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // neuButton2
            // 
            this.neuButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.neuButton2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuButton2.ForeColor = System.Drawing.Color.Red;
            this.neuButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuButton2.Location = new System.Drawing.Point(472, 8);
            this.neuButton2.Name = "neuButton2";
            this.neuButton2.Size = new System.Drawing.Size(75, 23);
            this.neuButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton2.TabIndex = 3;
            this.neuButton2.Text = "保存(&S)";
            this.neuButton2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton2.UseVisualStyleBackColor = true;
            this.neuButton2.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // neuButton1
            // 
            this.neuButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.neuButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuButton1.ForeColor = System.Drawing.Color.Red;
            this.neuButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuButton1.Location = new System.Drawing.Point(559, 8);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(75, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 2;
            this.neuButton1.Text = "新增(&C)";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("宋体", 12F);
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblState.Location = new System.Drawing.Point(101, 9);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(72, 16);
            this.lblState.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblState.TabIndex = 1;
            this.lblState.Text = "会诊状态";
            // 
            // chkTemplet
            // 
            this.chkTemplet.AutoSize = true;
            this.chkTemplet.Font = new System.Drawing.Font("宋体", 12F);
            this.chkTemplet.Location = new System.Drawing.Point(6, 8);
            this.chkTemplet.Name = "chkTemplet";
            this.chkTemplet.Size = new System.Drawing.Size(91, 20);
            this.chkTemplet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkTemplet.TabIndex = 0;
            this.chkTemplet.Text = "会诊模板";
            this.chkTemplet.UseVisualStyleBackColor = true;
            this.chkTemplet.CheckedChanged += new System.EventHandler(this.chkTemplet_CheckedChanged);
            // 
            // ucConsultation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucConsultation";
            this.Size = new System.Drawing.Size(929, 548);
            this.neuPanel1.ResumeLayout(false);
            this.panelFun.ResumeLayout(false);
            this.panelFun.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.pnPatientInfo.ResumeLayout(false);
            this.pnPatientInfo.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// 重绘neuGroupBox1 边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuGroupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.neuGroupBox1.BackColor);

            Rectangle Rtg_LT = new Rectangle();
            Rectangle Rtg_RT = new Rectangle();
            Rectangle Rtg_LB = new Rectangle();
            Rectangle Rtg_RB = new Rectangle();
            Rtg_LT.X = 0; Rtg_LT.Y = 7; Rtg_LT.Width = 10; Rtg_LT.Height = 10;
            Rtg_RT.X = e.ClipRectangle.Width - 11; Rtg_RT.Y = 7; Rtg_RT.Width = 10; Rtg_RT.Height = 10;
            Rtg_LB.X = 0; Rtg_LB.Y = e.ClipRectangle.Height - 11; Rtg_LB.Width = 10; Rtg_LB.Height = 10;
            Rtg_RB.X = e.ClipRectangle.Width - 11; Rtg_RB.Y = e.ClipRectangle.Height - 11; Rtg_RB.Width = 10; Rtg_RB.Height = 10;

            System.Drawing.Color color = System.Drawing.Color.White;
            Pen Pen_AL = new Pen(color, 1);
            Pen_AL.Color = color;
            Brush brush = new HatchBrush(HatchStyle.Divot, color);

            e.Graphics.DrawString(this.neuGroupBox1.Text, this.neuGroupBox1.Font, brush, 6, 0);
            e.Graphics.DrawArc(Pen_AL, Rtg_LT, 180, 90);
            e.Graphics.DrawArc(Pen_AL, Rtg_RT, 270, 90);
            e.Graphics.DrawArc(Pen_AL, Rtg_LB, 90, 90);
            e.Graphics.DrawArc(Pen_AL, Rtg_RB, 0, 90);
            e.Graphics.DrawLine(Pen_AL, 5, 7, 6, 7);
            e.Graphics.DrawLine(Pen_AL, e.Graphics.MeasureString(this.neuGroupBox1.Text, this.neuGroupBox1.Font).Width + 3, 7, e.ClipRectangle.Width - 7, 7);
            e.Graphics.DrawLine(Pen_AL, 0, 13, 0, e.ClipRectangle.Height - 7);
            e.Graphics.DrawLine(Pen_AL, 6, e.ClipRectangle.Height - 1, e.ClipRectangle.Width - 7, e.ClipRectangle.Height - 1);
            e.Graphics.DrawLine(Pen_AL, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 7, e.ClipRectangle.Width - 1, 13);
        } 

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private System.Windows.Forms.Panel panelFun;
        private System.Windows.Forms.Splitter splitter1;
        private FS.HISFC.Components.Common.Controls.ucUserText ucUserText1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel label12;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel label16;
        private FS.FrameWork.WinForms.Controls.NeuLabel label14;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBedID;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkEmergency;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkCommon;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox rtbSource;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtConsultation;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel19;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoctor;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel18;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel15;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel20;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDoc;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel22;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbAppDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel23;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel21;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtPreConsultation;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel25;
        private System.Windows.Forms.Panel panel2;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox rtbResult;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel26;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkCreateOrder;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkOuthos;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton4;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton3;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton2;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblState;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkTemplet;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FS.FrameWork.WinForms.Controls.NeuPanel Panel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblInpaientNo;
        private System.Windows.Forms.Panel pnPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbxOutPrint;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblHosName;
        private FS.FrameWork.WinForms.Controls.NeuButton PrintNotice;
        private TextBox txtResult;
        private TextBox txtSource;
    }
}
