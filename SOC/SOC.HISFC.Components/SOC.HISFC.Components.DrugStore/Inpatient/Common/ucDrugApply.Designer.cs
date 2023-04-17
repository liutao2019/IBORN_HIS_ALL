namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    partial class ucDrugApply
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
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbRecipeDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbRadix = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncmbDrug = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtPatientNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbDruged = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbApply = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ucDrugDetail1 = new FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.ucDrugDetail();
            this.rbPatientList = new System.Windows.Forms.RadioButton();
            this.rbDrugBillList = new System.Windows.Forms.RadioButton();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbStockDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbBlue = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbConcentratedSendInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncbUnSend = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ngbRadix = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbUnapplyIn = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbApplyIned = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.tvMessageBaseTree = new FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.tvMessageBaseTree(this.components);
            this.ncbShowInvalidData = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbShowIQuitData = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ngbQuerySet.SuspendLayout();
            this.npanelDrugMessage.SuspendLayout();
            this.ngbDrugDetail.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.ngbRadix.SuspendLayout();
            this.SuspendLayout();
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Controls.Add(this.ncbShowIQuitData);
            this.ngbQuerySet.Controls.Add(this.ncbShowInvalidData);
            this.ngbQuerySet.Controls.Add(this.nlbConcentratedSendInfo);
            this.ngbQuerySet.Controls.Add(this.neuLabel12);
            this.ngbQuerySet.Controls.Add(this.nlbBlue);
            this.ngbQuerySet.Controls.Add(this.nlbStockDept);
            this.ngbQuerySet.Controls.Add(this.neuLabel6);
            this.ngbQuerySet.Controls.Add(this.rbDrugBillList);
            this.ngbQuerySet.Controls.Add(this.rbPatientList);
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 523);
            this.ngbQuerySet.Size = new System.Drawing.Size(1004, 81);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 520);
            this.neuSplitter1.Size = new System.Drawing.Size(1004, 3);
            // 
            // npanelDrugMessage
            // 
            this.npanelDrugMessage.Controls.Add(this.tvMessageBaseTree);
            this.npanelDrugMessage.Controls.Add(this.ngbRadix);
            this.npanelDrugMessage.Controls.Add(this.neuGroupBox1);
            this.npanelDrugMessage.Controls.Add(this.neuGroupBox3);
            this.npanelDrugMessage.Location = new System.Drawing.Point(0, 0);
            this.npanelDrugMessage.Size = new System.Drawing.Size(263, 520);
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Location = new System.Drawing.Point(263, 0);
            this.neuSplitter2.Size = new System.Drawing.Size(3, 520);
            // 
            // ngbDrugDetail
            // 
            this.ngbDrugDetail.Controls.Add(this.ucDrugDetail1);
            this.ngbDrugDetail.Location = new System.Drawing.Point(266, 0);
            this.ngbDrugDetail.Size = new System.Drawing.Size(738, 520);
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.cmbRecipeDept);
            this.neuGroupBox3.Controls.Add(this.neuLabel11);
            this.neuGroupBox3.Controls.Add(this.neuLabel10);
            this.neuGroupBox3.Controls.Add(this.ncbRadix);
            this.neuGroupBox3.Controls.Add(this.ncmbDrug);
            this.neuGroupBox3.Controls.Add(this.neuLabel8);
            this.neuGroupBox3.Controls.Add(this.ntxtPatientNO);
            this.neuGroupBox3.Controls.Add(this.cmbDept);
            this.neuGroupBox3.Controls.Add(this.neuLabel4);
            this.neuGroupBox3.Controls.Add(this.neuLabel2);
            this.neuGroupBox3.Controls.Add(this.neuDateTimePicker2);
            this.neuGroupBox3.Controls.Add(this.neuLabel1);
            this.neuGroupBox3.Controls.Add(this.neuDateTimePicker1);
            this.neuGroupBox3.Controls.Add(this.neuLabel3);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(263, 210);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 12;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "查询设置";
            // 
            // cmbRecipeDept
            // 
            this.cmbRecipeDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbRecipeDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRecipeDept.FormattingEnabled = true;
            this.cmbRecipeDept.IsEnter2Tab = false;
            this.cmbRecipeDept.IsFlat = false;
            this.cmbRecipeDept.IsLike = true;
            this.cmbRecipeDept.IsListOnly = false;
            this.cmbRecipeDept.IsPopForm = true;
            this.cmbRecipeDept.IsShowCustomerList = false;
            this.cmbRecipeDept.IsShowID = false;
            this.cmbRecipeDept.IsShowIDAndName = false;
            this.cmbRecipeDept.Location = new System.Drawing.Point(83, 49);
            this.cmbRecipeDept.Name = "cmbRecipeDept";
            this.cmbRecipeDept.ShowCustomerList = false;
            this.cmbRecipeDept.ShowID = false;
            this.cmbRecipeDept.Size = new System.Drawing.Size(165, 20);
            this.cmbRecipeDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRecipeDept.TabIndex = 32;
            this.cmbRecipeDept.Tag = "";
            this.cmbRecipeDept.ToolBarUse = false;
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(18, 49);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(65, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 31;
            this.neuLabel11.Text = "开立科室：";
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(16, 187);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(65, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 30;
            this.neuLabel10.Text = "消耗方式：";
            // 
            // ncbRadix
            // 
            this.ncbRadix.AutoSize = true;
            this.ncbRadix.Location = new System.Drawing.Point(83, 186);
            this.ncbRadix.Name = "ncbRadix";
            this.ncbRadix.Size = new System.Drawing.Size(60, 16);
            this.ncbRadix.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbRadix.TabIndex = 28;
            this.ncbRadix.Text = "基数药";
            this.ncbRadix.UseVisualStyleBackColor = true;
            // 
            // ncmbDrug
            // 
            this.ncmbDrug.ArrowBackColor = System.Drawing.Color.Silver;
            this.ncmbDrug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbDrug.IsEnter2Tab = false;
            this.ncmbDrug.IsFlat = false;
            this.ncmbDrug.IsLike = true;
            this.ncmbDrug.IsListOnly = false;
            this.ncmbDrug.IsPopForm = true;
            this.ncmbDrug.IsShowCustomerList = false;
            this.ncmbDrug.IsShowID = false;
            this.ncmbDrug.IsShowIDAndName = false;
            this.ncmbDrug.Location = new System.Drawing.Point(83, 158);
            this.ncmbDrug.Name = "ncmbDrug";
            this.ncmbDrug.ShowCustomerList = false;
            this.ncmbDrug.ShowID = false;
            this.ncmbDrug.Size = new System.Drawing.Size(165, 20);
            this.ncmbDrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDrug.TabIndex = 27;
            this.ncmbDrug.Tag = "";
            this.ncmbDrug.ToolBarUse = false;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(16, 162);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(65, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 26;
            this.neuLabel8.Text = "药品名称：";
            // 
            // ntxtPatientNO
            // 
            this.ntxtPatientNO.IsEnter2Tab = false;
            this.ntxtPatientNO.Location = new System.Drawing.Point(83, 130);
            this.ntxtPatientNO.Name = "ntxtPatientNO";
            this.ntxtPatientNO.Size = new System.Drawing.Size(165, 21);
            this.ntxtPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtPatientNO.TabIndex = 17;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(83, 18);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(165, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 20;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(16, 22);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 19;
            this.neuLabel4.Text = "申请科室：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(16, 132);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 18;
            this.neuLabel2.Text = "住 院 号：";
            // 
            // neuDateTimePicker2
            // 
            this.neuDateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker2.IsEnter2Tab = false;
            this.neuDateTimePicker2.Location = new System.Drawing.Point(83, 102);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(165, 21);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 16;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(16, 105);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 15;
            this.neuLabel1.Text = "结束时间：";
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(83, 75);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(165, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 14;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(16, 79);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 13;
            this.neuLabel3.Text = "开始时间：";
            // 
            // ncbDruged
            // 
            this.ncbDruged.AutoSize = true;
            this.ncbDruged.Location = new System.Drawing.Point(197, 24);
            this.ncbDruged.Name = "ncbDruged";
            this.ncbDruged.Size = new System.Drawing.Size(60, 16);
            this.ncbDruged.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbDruged.TabIndex = 24;
            this.ncbDruged.Text = "已执行";
            this.ncbDruged.UseVisualStyleBackColor = true;
            // 
            // ncbApply
            // 
            this.ncbApply.AutoSize = true;
            this.ncbApply.Checked = true;
            this.ncbApply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbApply.Location = new System.Drawing.Point(84, 24);
            this.ncbApply.Name = "ncbApply";
            this.ncbApply.Size = new System.Drawing.Size(108, 16);
            this.ncbApply.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbApply.TabIndex = 23;
            this.ncbApply.Text = "已发送(未摆药)";
            this.ncbApply.UseVisualStyleBackColor = true;
            // 
            // ucDrugDetail1
            // 
            this.ucDrugDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDrugDetail1.HightLightDept = "";
            this.ucDrugDetail1.Location = new System.Drawing.Point(3, 17);
            this.ucDrugDetail1.Name = "ucDrugDetail1";
            this.ucDrugDetail1.Size = new System.Drawing.Size(732, 500);
            this.ucDrugDetail1.TabIndex = 1;
            // 
            // rbPatientList
            // 
            this.rbPatientList.AutoSize = true;
            this.rbPatientList.Checked = true;
            this.rbPatientList.ForeColor = System.Drawing.Color.Blue;
            this.rbPatientList.Location = new System.Drawing.Point(20, 23);
            this.rbPatientList.Name = "rbPatientList";
            this.rbPatientList.Size = new System.Drawing.Size(119, 16);
            this.rbPatientList.TabIndex = 0;
            this.rbPatientList.TabStop = true;
            this.rbPatientList.Text = "列表显示病区患者";
            this.rbPatientList.UseVisualStyleBackColor = true;
            // 
            // rbDrugBillList
            // 
            this.rbDrugBillList.AutoSize = true;
            this.rbDrugBillList.ForeColor = System.Drawing.Color.Blue;
            this.rbDrugBillList.Location = new System.Drawing.Point(178, 23);
            this.rbDrugBillList.Name = "rbDrugBillList";
            this.rbDrugBillList.Size = new System.Drawing.Size(131, 16);
            this.rbDrugBillList.TabIndex = 1;
            this.rbDrugBillList.Text = "列表显示药房摆药单";
            this.rbDrugBillList.UseVisualStyleBackColor = true;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel6.Location = new System.Drawing.Point(18, 53);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(149, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 19;
            this.neuLabel6.Text = "有发药人表示已经发药保存";
            // 
            // nlbStockDept
            // 
            this.nlbStockDept.AutoSize = true;
            this.nlbStockDept.ForeColor = System.Drawing.Color.Blue;
            this.nlbStockDept.Location = new System.Drawing.Point(524, 53);
            this.nlbStockDept.Name = "nlbStockDept";
            this.nlbStockDept.Size = new System.Drawing.Size(11, 12);
            this.nlbStockDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbStockDept.TabIndex = 21;
            this.nlbStockDept.Text = "*";
            // 
            // nlbBlue
            // 
            this.nlbBlue.AutoSize = true;
            this.nlbBlue.ForeColor = System.Drawing.Color.Blue;
            this.nlbBlue.Location = new System.Drawing.Point(853, 24);
            this.nlbBlue.Name = "nlbBlue";
            this.nlbBlue.Size = new System.Drawing.Size(89, 12);
            this.nlbBlue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbBlue.TabIndex = 22;
            this.nlbBlue.Text = "蓝色为紧急发送";
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.ForeColor = System.Drawing.Color.Black;
            this.neuLabel12.Location = new System.Drawing.Point(340, 25);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(185, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 23;
            this.neuLabel12.Text = "黑色正常申请数据（发药、退药）";
            // 
            // nlbConcentratedSendInfo
            // 
            this.nlbConcentratedSendInfo.AutoSize = true;
            this.nlbConcentratedSendInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbConcentratedSendInfo.Location = new System.Drawing.Point(176, 53);
            this.nlbConcentratedSendInfo.Name = "nlbConcentratedSendInfo";
            this.nlbConcentratedSendInfo.Size = new System.Drawing.Size(227, 12);
            this.nlbConcentratedSendInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbConcentratedSendInfo.TabIndex = 24;
            this.nlbConcentratedSendInfo.Text = "系统员已于2012-10-10 10:10:10集中发送";
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ncbUnSend);
            this.neuGroupBox1.Controls.Add(this.ncbApply);
            this.neuGroupBox1.Controls.Add(this.ncbDruged);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 210);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(263, 55);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 38;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "状态设置";
            // 
            // ncbUnSend
            // 
            this.ncbUnSend.AutoSize = true;
            this.ncbUnSend.Checked = true;
            this.ncbUnSend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbUnSend.Location = new System.Drawing.Point(18, 24);
            this.ncbUnSend.Name = "ncbUnSend";
            this.ncbUnSend.Size = new System.Drawing.Size(60, 16);
            this.ncbUnSend.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbUnSend.TabIndex = 29;
            this.ncbUnSend.Text = "未发送";
            this.ncbUnSend.UseVisualStyleBackColor = true;
            // 
            // ngbRadix
            // 
            this.ngbRadix.Controls.Add(this.neuLabel9);
            this.ngbRadix.Controls.Add(this.ncbUnapplyIn);
            this.ngbRadix.Controls.Add(this.ncbApplyIned);
            this.ngbRadix.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbRadix.Location = new System.Drawing.Point(0, 265);
            this.ngbRadix.Name = "ngbRadix";
            this.ngbRadix.Size = new System.Drawing.Size(263, 55);
            this.ngbRadix.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbRadix.TabIndex = 39;
            this.ngbRadix.TabStop = false;
            this.ngbRadix.Text = "基药管理";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(16, 22);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(65, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 31;
            this.neuLabel9.Text = "已用基药：";
            // 
            // ncbUnapplyIn
            // 
            this.ncbUnapplyIn.AutoSize = true;
            this.ncbUnapplyIn.Enabled = false;
            this.ncbUnapplyIn.Location = new System.Drawing.Point(83, 20);
            this.ncbUnapplyIn.Name = "ncbUnapplyIn";
            this.ncbUnapplyIn.Size = new System.Drawing.Size(108, 16);
            this.ncbUnapplyIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbUnapplyIn.TabIndex = 29;
            this.ncbUnapplyIn.Text = "未请领(已消耗)";
            this.ncbUnapplyIn.UseVisualStyleBackColor = true;
            // 
            // ncbApplyIned
            // 
            this.ncbApplyIned.AutoSize = true;
            this.ncbApplyIned.Enabled = false;
            this.ncbApplyIned.Location = new System.Drawing.Point(197, 20);
            this.ncbApplyIned.Name = "ncbApplyIned";
            this.ncbApplyIned.Size = new System.Drawing.Size(60, 16);
            this.ncbApplyIned.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbApplyIned.TabIndex = 30;
            this.ncbApplyIned.Text = "已请领";
            this.ncbApplyIned.UseVisualStyleBackColor = true;
            // 
            // tvMessageBaseTree
            // 
            this.tvMessageBaseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMessageBaseTree.HideSelection = false;
            this.tvMessageBaseTree.ImageIndex = 0;
            this.tvMessageBaseTree.Location = new System.Drawing.Point(0, 320);
            this.tvMessageBaseTree.Name = "tvMessageBaseTree";
            this.tvMessageBaseTree.SelectedImageIndex = 0;
            this.tvMessageBaseTree.Size = new System.Drawing.Size(263, 200);
            this.tvMessageBaseTree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvMessageBaseTree.TabIndex = 40;
            // 
            // ncbShowInvalidData
            // 
            this.ncbShowInvalidData.AutoSize = true;
            this.ncbShowInvalidData.ForeColor = System.Drawing.Color.Red;
            this.ncbShowInvalidData.Location = new System.Drawing.Point(548, 23);
            this.ncbShowInvalidData.Name = "ncbShowInvalidData";
            this.ncbShowInvalidData.Size = new System.Drawing.Size(132, 16);
            this.ncbShowInvalidData.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbShowInvalidData.TabIndex = 30;
            this.ncbShowInvalidData.Text = "显示红色作废的申请";
            this.ncbShowInvalidData.UseVisualStyleBackColor = true;
            // 
            // ncbShowIQuitData
            // 
            this.ncbShowIQuitData.AutoSize = true;
            this.ncbShowIQuitData.ForeColor = System.Drawing.Color.Red;
            this.ncbShowIQuitData.Location = new System.Drawing.Point(706, 23);
            this.ncbShowIQuitData.Name = "ncbShowIQuitData";
            this.ncbShowIQuitData.Size = new System.Drawing.Size(96, 16);
            this.ncbShowIQuitData.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbShowIQuitData.TabIndex = 31;
            this.ncbShowIQuitData.Text = "只显示退药单";
            this.ncbShowIQuitData.UseVisualStyleBackColor = true;
            // 
            // ucDrugApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDrugApply";
            this.Size = new System.Drawing.Size(1004, 604);
            this.ngbQuerySet.ResumeLayout(false);
            this.ngbQuerySet.PerformLayout();
            this.npanelDrugMessage.ResumeLayout(false);
            this.ngbDrugDetail.ResumeLayout(false);
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ngbRadix.ResumeLayout(false);
            this.ngbRadix.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtPatientNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbDruged;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbApply;
        private ucDrugDetail ucDrugDetail1;
        private System.Windows.Forms.RadioButton rbDrugBillList;
        private System.Windows.Forms.RadioButton rbPatientList;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDrug;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbRadix;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbStockDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbBlue;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbConcentratedSendInfo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRecipeDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbUnSend;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox ngbRadix;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbUnapplyIn;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbApplyIned;
        private FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.tvMessageBaseTree tvMessageBaseTree;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbShowInvalidData;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbShowIQuitData;
    }
}
