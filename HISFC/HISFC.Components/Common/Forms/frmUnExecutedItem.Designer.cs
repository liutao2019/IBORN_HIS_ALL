namespace FS.HISFC.Components.Common.Forms
{
    partial class frmUnExecutedItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FS.FrameWork.WinForms.Controls.NeuLabel lbCardType;
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tbPact = new System.Windows.Forms.TextBox();
            this.tbLevel = new System.Windows.Forms.TextBox();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.tbCardType = new System.Windows.Forms.TextBox();
            this.tbMedicalNO = new System.Windows.Forms.TextBox();
            this.lblMedicalNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblLevel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbIDNO = new System.Windows.Forms.TextBox();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpPackageDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackageDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            lbCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCardType
            // 
            lbCardType.AutoSize = true;
            lbCardType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbCardType.ForeColor = System.Drawing.SystemColors.ControlText;
            lbCardType.Location = new System.Drawing.Point(414, 37);
            lbCardType.Name = "lbCardType";
            lbCardType.Size = new System.Drawing.Size(68, 20);
            lbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbCardType.TabIndex = 28;
            lbCardType.Text = "证件类型:";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnlTop.Controls.Add(this.tbPact);
            this.pnlTop.Controls.Add(this.tbLevel);
            this.pnlTop.Controls.Add(this.tbSex);
            this.pnlTop.Controls.Add(this.tbCardType);
            this.pnlTop.Controls.Add(this.tbMedicalNO);
            this.pnlTop.Controls.Add(this.lblMedicalNO);
            this.pnlTop.Controls.Add(this.lblPact);
            this.pnlTop.Controls.Add(this.lblLevel);
            this.pnlTop.Controls.Add(this.tbPhone);
            this.pnlTop.Controls.Add(this.lblPhone);
            this.pnlTop.Controls.Add(this.tbIDNO);
            this.pnlTop.Controls.Add(this.tbAge);
            this.pnlTop.Controls.Add(this.tbName);
            this.pnlTop.Controls.Add(this.lbName);
            this.pnlTop.Controls.Add(this.lbSex);
            this.pnlTop.Controls.Add(this.lbAge);
            this.pnlTop.Controls.Add(this.lbRegDept);
            this.pnlTop.Controls.Add(lbCardType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(931, 68);
            this.pnlTop.TabIndex = 1;
            // 
            // tbPact
            // 
            this.tbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPact.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPact.Location = new System.Drawing.Point(90, 38);
            this.tbPact.Name = "tbPact";
            this.tbPact.ReadOnly = true;
            this.tbPact.Size = new System.Drawing.Size(101, 19);
            this.tbPact.TabIndex = 46;
            this.tbPact.TabStop = false;
            this.tbPact.Text = "普通";
            // 
            // tbLevel
            // 
            this.tbLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLevel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLevel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbLevel.Location = new System.Drawing.Point(275, 38);
            this.tbLevel.Name = "tbLevel";
            this.tbLevel.ReadOnly = true;
            this.tbLevel.Size = new System.Drawing.Size(119, 19);
            this.tbLevel.TabIndex = 45;
            this.tbLevel.TabStop = false;
            this.tbLevel.Text = "会员";
            // 
            // tbSex
            // 
            this.tbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbSex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSex.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSex.Location = new System.Drawing.Point(460, 11);
            this.tbSex.Name = "tbSex";
            this.tbSex.ReadOnly = true;
            this.tbSex.Size = new System.Drawing.Size(39, 19);
            this.tbSex.TabIndex = 44;
            this.tbSex.TabStop = false;
            this.tbSex.Text = "男";
            // 
            // tbCardType
            // 
            this.tbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbCardType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCardType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCardType.Location = new System.Drawing.Point(488, 37);
            this.tbCardType.Name = "tbCardType";
            this.tbCardType.ReadOnly = true;
            this.tbCardType.Size = new System.Drawing.Size(101, 19);
            this.tbCardType.TabIndex = 43;
            this.tbCardType.TabStop = false;
            this.tbCardType.Text = "身份证";
            // 
            // tbMedicalNO
            // 
            this.tbMedicalNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbMedicalNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMedicalNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMedicalNO.Location = new System.Drawing.Point(90, 12);
            this.tbMedicalNO.Name = "tbMedicalNO";
            this.tbMedicalNO.ReadOnly = true;
            this.tbMedicalNO.Size = new System.Drawing.Size(101, 19);
            this.tbMedicalNO.TabIndex = 25;
            this.tbMedicalNO.TabStop = false;
            this.tbMedicalNO.Tag = "MEDNO";
            this.tbMedicalNO.Text = "00000000001";
            // 
            // lblMedicalNO
            // 
            this.lblMedicalNO.AutoSize = true;
            this.lblMedicalNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalNO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMedicalNO.Location = new System.Drawing.Point(16, 12);
            this.lblMedicalNO.Name = "lblMedicalNO";
            this.lblMedicalNO.Size = new System.Drawing.Size(68, 20);
            this.lblMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMedicalNO.TabIndex = 24;
            this.lblMedicalNO.Text = "就诊卡号:";
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPact.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPact.Location = new System.Drawing.Point(16, 38);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(68, 20);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPact.TabIndex = 40;
            this.lblPact.Text = "合同单位:";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLevel.Location = new System.Drawing.Point(201, 38);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(68, 20);
            this.lblLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLevel.TabIndex = 36;
            this.lblLevel.Text = "会员类型:";
            // 
            // tbPhone
            // 
            this.tbPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPhone.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.Location = new System.Drawing.Point(744, 12);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.ReadOnly = true;
            this.tbPhone.Size = new System.Drawing.Size(127, 19);
            this.tbPhone.TabIndex = 39;
            this.tbPhone.TabStop = false;
            this.tbPhone.Text = "18000000000";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPhone.Location = new System.Drawing.Point(670, 12);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(68, 20);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 38;
            this.lblPhone.Text = "联系电话:";
            // 
            // tbIDNO
            // 
            this.tbIDNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbIDNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIDNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIDNO.Location = new System.Drawing.Point(744, 40);
            this.tbIDNO.Name = "tbIDNO";
            this.tbIDNO.ReadOnly = true;
            this.tbIDNO.Size = new System.Drawing.Size(152, 19);
            this.tbIDNO.TabIndex = 31;
            this.tbIDNO.TabStop = false;
            this.tbIDNO.Text = "999999999999999999";
            // 
            // tbAge
            // 
            this.tbAge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAge.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.Location = new System.Drawing.Point(551, 11);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(50, 19);
            this.tbAge.TabIndex = 35;
            this.tbAge.TabStop = false;
            this.tbAge.Text = "25岁";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.Location = new System.Drawing.Point(275, 12);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(119, 19);
            this.tbName.TabIndex = 27;
            this.tbName.TabStop = false;
            this.tbName.Text = "张三";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbName.Location = new System.Drawing.Point(201, 12);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(68, 20);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 26;
            this.lbName.Text = "患者姓名:";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbSex.Location = new System.Drawing.Point(414, 11);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(40, 20);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 32;
            this.lbSex.Text = "性别:";
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbAge.Location = new System.Drawing.Point(505, 11);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(40, 20);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 34;
            this.lbAge.Text = "年龄:";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbRegDept.Location = new System.Drawing.Point(670, 40);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(68, 20);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 30;
            this.lbRegDept.Text = "证件号码:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.neuLabel1.Location = new System.Drawing.Point(812, 115);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(0, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 52;
            // 
            // fpPackageDetail
            // 
            this.fpPackageDetail.About = "3.0.2004.2005";
            this.fpPackageDetail.AccessibleDescription = "fpPackageDetail, Sheet1, Row 0, Column 0, ";
            this.fpPackageDetail.BackColor = System.Drawing.Color.White;
            this.fpPackageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPackageDetail.EditModePermanent = true;
            this.fpPackageDetail.EditModeReplace = true;
            this.fpPackageDetail.FileName = "";
            this.fpPackageDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackageDetail.IsAutoSaveGridStatus = false;
            this.fpPackageDetail.IsCanCustomConfigColumn = false;
            this.fpPackageDetail.Location = new System.Drawing.Point(0, 68);
            this.fpPackageDetail.Name = "fpPackageDetail";
            this.fpPackageDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackageDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackageDetail_Sheet1});
            this.fpPackageDetail.Size = new System.Drawing.Size(931, 415);
            this.fpPackageDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackageDetail.TabIndex = 53;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackageDetail.TextTipAppearance = tipAppearance1;
            this.fpPackageDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPackageDetail_Sheet1
            // 
            this.fpPackageDetail_Sheet1.Reset();
            this.fpPackageDetail_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackageDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackageDetail_Sheet1.ColumnCount = 8;
            this.fpPackageDetail_Sheet1.RowCount = 0;
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "卡号";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "开单科室";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "开单人";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "编码";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "项目";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "状态";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "开单时间";
            this.fpPackageDetail_Sheet1.Columns.Get(0).Label = "卡号";
            this.fpPackageDetail_Sheet1.Columns.Get(0).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(0).Width = 107F;
            this.fpPackageDetail_Sheet1.Columns.Get(1).Label = "姓名";
            this.fpPackageDetail_Sheet1.Columns.Get(1).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(1).Width = 91F;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Label = "开单科室";
            this.fpPackageDetail_Sheet1.Columns.Get(2).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Width = 95F;
            this.fpPackageDetail_Sheet1.Columns.Get(3).Label = "开单人";
            this.fpPackageDetail_Sheet1.Columns.Get(3).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(3).Width = 90F;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Label = "编码";
            this.fpPackageDetail_Sheet1.Columns.Get(4).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Width = 129F;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Label = "项目";
            this.fpPackageDetail_Sheet1.Columns.Get(5).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Width = 158F;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Label = "状态";
            this.fpPackageDetail_Sheet1.Columns.Get(6).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Width = 102F;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Label = "开单时间";
            this.fpPackageDetail_Sheet1.Columns.Get(7).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Width = 144F;
            this.fpPackageDetail_Sheet1.DefaultStyle.Locked = false;
            this.fpPackageDetail_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPackageDetail_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPackageDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpPackageDetail_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackageDetail_Sheet1.RowHeader.Columns.Get(0).Width = 0F;
            this.fpPackageDetail_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackageDetail_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackageDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPackageDetail.SetActiveViewport(0, 1, 0);
            // 
            // frmUnExecutedItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 483);
            this.Controls.Add(this.fpPackageDetail);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.neuLabel1);
            this.KeyPreview = true;
            this.Name = "frmUnExecutedItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "门诊未执行的项目";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox tbPact;
        private System.Windows.Forms.TextBox tbLevel;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.TextBox tbCardType;
        protected System.Windows.Forms.TextBox tbMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblLevel;
        protected System.Windows.Forms.TextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected System.Windows.Forms.TextBox tbIDNO;
        protected System.Windows.Forms.TextBox tbAge;
        protected System.Windows.Forms.TextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackageDetail;
        private FarPoint.Win.Spread.SheetView fpPackageDetail_Sheet1;
    }
}