namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.InjectBillPrint
{
    partial class ucInjectBillPrint2
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpreadItems = new FarPoint.Win.Spread.FpSpread();
            this.fpSpreadItemsSheet = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.neuPanelItems = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.npbBarCode = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.lblnation = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblBirthday = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSeeDate = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSeeDept = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItemsSheet)).BeginInit();
            this.neuPanelItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpSpreadItems
            // 
            this.fpSpreadItems.About = "3.0.2004.2005";
            this.fpSpreadItems.AccessibleDescription = "fpSpreadItems, fpSpreadItemsSheet, Row 0, Column 0, 组号";
            this.fpSpreadItems.BackColor = System.Drawing.Color.White;
            this.fpSpreadItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpSpreadItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadItems.Font = new System.Drawing.Font("宋体", 10.5F);
            this.fpSpreadItems.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpreadItems.Location = new System.Drawing.Point(0, 0);
            this.fpSpreadItems.Margin = new System.Windows.Forms.Padding(4);
            this.fpSpreadItems.Name = "fpSpreadItems";
            this.fpSpreadItems.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpreadItems.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpreadItemsSheet});
            this.fpSpreadItems.Size = new System.Drawing.Size(549, 366);
            this.fpSpreadItems.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpreadItems.TextTipAppearance = tipAppearance1;
            this.fpSpreadItems.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // fpSpreadItemsSheet
            // 
            this.fpSpreadItemsSheet.Reset();
            this.fpSpreadItemsSheet.SheetName = "fpSpreadItemsSheet";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpreadItemsSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpreadItemsSheet.ColumnCount = 7;
            this.fpSpreadItemsSheet.RowCount = 11;
            this.fpSpreadItemsSheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, false, false, false, false, false);
            this.fpSpreadItemsSheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpreadItemsSheet.DefaultStyle.BackColor = System.Drawing.Color.White;
            textCellType1.Multiline = true;
            textCellType1.WordWrap = true;
            this.fpSpreadItemsSheet.DefaultStyle.CellType = textCellType1;
            this.fpSpreadItemsSheet.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpreadItemsSheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpreadItemsSheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpreadItemsSheet.RowHeader.Visible = false;
            this.fpSpreadItemsSheet.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpreadItemsSheet.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpreadItemsSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(3, 582);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(560, 1);
            this.panel3.TabIndex = 173;
            this.panel3.Visible = false;
            // 
            // neuPanelItems
            // 
            this.neuPanelItems.Controls.Add(this.fpSpreadItems);
            this.neuPanelItems.Location = new System.Drawing.Point(3, 209);
            this.neuPanelItems.Name = "neuPanelItems";
            this.neuPanelItems.Size = new System.Drawing.Size(549, 366);
            this.neuPanelItems.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelItems.TabIndex = 186;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(3, 202);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 1);
            this.panel1.TabIndex = 336;
            // 
            // npbBarCode
            // 
            this.npbBarCode.Location = new System.Drawing.Point(334, 20);
            this.npbBarCode.Name = "npbBarCode";
            this.npbBarCode.Size = new System.Drawing.Size(150, 39);
            this.npbBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbBarCode.TabIndex = 348;
            this.npbBarCode.TabStop = false;
            this.npbBarCode.Visible = false;
            // 
            // lblnation
            // 
            this.lblnation.AutoSize = true;
            this.lblnation.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblnation.Location = new System.Drawing.Point(372, 176);
            this.lblnation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblnation.Name = "lblnation";
            this.lblnation.Size = new System.Drawing.Size(91, 14);
            this.lblnation.TabIndex = 370;
            this.lblnation.Text = "            ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("黑体", 10.5F);
            this.label11.Location = new System.Drawing.Point(243, 176);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 14);
            this.label11.TabIndex = 369;
            this.label11.Text = "国籍/Nationality:";
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblBirthday.Location = new System.Drawing.Point(120, 176);
            this.lblBirthday.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(119, 14);
            this.lblBirthday.TabIndex = 368;
            this.lblBirthday.Text = "   1990.06.25   ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("黑体", 10.5F);
            this.label5.Location = new System.Drawing.Point(7, 176);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 14);
            this.label5.TabIndex = 367;
            this.label5.Text = "出生年月/D.O.B:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(137, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 26);
            this.label4.TabIndex = 366;
            this.label4.Text = "Injection Schedule";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Black;
            this.labelTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTitle.Location = new System.Drawing.Point(128, 32);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(212, 27);
            this.labelTitle.TabIndex = 365;
            this.labelTitle.Text = "注 射 治 疗 单";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSeeDate
            // 
            this.labelSeeDate.AutoSize = true;
            this.labelSeeDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.labelSeeDate.ForeColor = System.Drawing.Color.Black;
            this.labelSeeDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSeeDate.Location = new System.Drawing.Point(372, 130);
            this.labelSeeDate.Name = "labelSeeDate";
            this.labelSeeDate.Size = new System.Drawing.Size(147, 14);
            this.labelSeeDate.TabIndex = 364;
            this.labelSeeDate.Text = "     2014.05.08     ";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblSex.ForeColor = System.Drawing.Color.Black;
            this.lblSex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSex.Location = new System.Drawing.Point(372, 152);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(147, 14);
            this.lblSex.TabIndex = 363;
            this.lblSex.Text = "        女/F        ";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("黑体", 10.5F);
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(292, 130);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 14);
            this.label31.TabIndex = 362;
            this.label31.Text = "日期/Date:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblName.Location = new System.Drawing.Point(120, 152);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(148, 14);
            this.lblName.TabIndex = 359;
            this.lblName.Text = "      孙悟空      ";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblCardNo.ForeColor = System.Drawing.Color.Black;
            this.lblCardNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardNo.Location = new System.Drawing.Point(372, 102);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(112, 14);
            this.lblCardNo.TabIndex = 361;
            this.lblCardNo.Text = "   00184023    ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("黑体", 10.5F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(7, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 356;
            this.label6.Text = "姓名/Name:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("黑体", 10.5F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(243, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 14);
            this.label12.TabIndex = 358;
            this.label12.Text = "病历号/Record No:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("黑体", 10.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(7, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 14);
            this.label10.TabIndex = 357;
            this.label10.Text = "科别/Dept:";
            // 
            // lblSeeDept
            // 
            this.lblSeeDept.AutoSize = true;
            this.lblSeeDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblSeeDept.ForeColor = System.Drawing.Color.Black;
            this.lblSeeDept.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSeeDept.Location = new System.Drawing.Point(120, 130);
            this.lblSeeDept.Name = "lblSeeDept";
            this.lblSeeDept.Size = new System.Drawing.Size(147, 14);
            this.lblSeeDept.TabIndex = 360;
            this.lblSeeDept.Text = "       皮肤科       ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("黑体", 10.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(299, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 371;
            this.label7.Text = "性别/Sex:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GJLocal.HISFC.Components.OpGuide.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(34, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 90);
            this.pictureBox1.TabIndex = 374;
            this.pictureBox1.TabStop = false;
            // 
            // ucInjectBillPrint2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.npbBarCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblnation);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblBirthday);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelSeeDate);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblSeeDept);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.neuPanelItems);
            this.Controls.Add(this.panel3);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucInjectBillPrint2";
            this.Size = new System.Drawing.Size(550, 800);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItemsSheet)).EndInit();
            this.neuPanelItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel panel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelItems;
        private FarPoint.Win.Spread.FpSpread fpSpreadItems;
        private FarPoint.Win.Spread.SheetView fpSpreadItemsSheet;
        private System.Windows.Forms.Panel panel1;
        protected FS.FrameWork.WinForms.Controls.NeuPictureBox npbBarCode;
        private System.Windows.Forms.Label lblnation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblBirthday;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSeeDate;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSeeDept;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

