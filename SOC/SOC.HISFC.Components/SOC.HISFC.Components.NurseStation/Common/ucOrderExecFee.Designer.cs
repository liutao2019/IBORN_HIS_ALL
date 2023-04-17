namespace FS.SOC.HISFC.Components.NurseStation.Common
{
    partial class ucOrderExecFee
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
            this.ngbQuerySet = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncbEmergency = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ncbAST = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbHide = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbNew = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbToDay = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbInvalid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbShort = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbLong = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ngbAdd = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanelDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucOrderExecFeeDetail = new FS.SOC.HISFC.Components.NurseStation.Base.ucOrderExecFeeDetail();
            this.ngbQuerySet.SuspendLayout();
            this.ngbAdd.SuspendLayout();
            this.neuPanelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Controls.Add(this.ncbEmergency);
            this.ngbQuerySet.Controls.Add(this.neuLabel2);
            this.ngbQuerySet.Controls.Add(this.neuLabel1);
            this.ngbQuerySet.Controls.Add(this.ncmbType);
            this.ngbQuerySet.Controls.Add(this.ncbAST);
            this.ngbQuerySet.Controls.Add(this.ncbHide);
            this.ngbQuerySet.Controls.Add(this.ncbNew);
            this.ngbQuerySet.Controls.Add(this.ncbToDay);
            this.ngbQuerySet.Controls.Add(this.ncbInvalid);
            this.ngbQuerySet.Controls.Add(this.ncbValid);
            this.ngbQuerySet.Controls.Add(this.ncbShort);
            this.ngbQuerySet.Controls.Add(this.ncbLong);
            this.ngbQuerySet.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 0);
            this.ngbQuerySet.Name = "ngbQuerySet";
            this.ngbQuerySet.Size = new System.Drawing.Size(1317, 51);
            this.ngbQuerySet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQuerySet.TabIndex = 20;
            this.ngbQuerySet.TabStop = false;
            this.ngbQuerySet.Text = "查询设置";
            // 
            // ncbEmergency
            // 
            this.ncbEmergency.AutoSize = true;
            this.ncbEmergency.Location = new System.Drawing.Point(665, 20);
            this.ncbEmergency.Name = "ncbEmergency";
            this.ncbEmergency.Size = new System.Drawing.Size(48, 16);
            this.ncbEmergency.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbEmergency.TabIndex = 19;
            this.ncbEmergency.Text = "加急";
            this.ncbEmergency.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbEmergency.UseVisualStyleBackColor = true;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(190, 22);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 18;
            this.neuLabel2.Text = "仅查询：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(22, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 17;
            this.neuLabel1.Text = "类别：";
            // 
            // ncmbType
            // 
            this.ncmbType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbType.FormattingEnabled = true;
            this.ncmbType.IsEnter2Tab = false;
            this.ncmbType.IsFlat = false;
            this.ncmbType.IsLike = true;
            this.ncmbType.IsListOnly = false;
            this.ncmbType.IsPopForm = true;
            this.ncmbType.IsShowCustomerList = false;
            this.ncmbType.IsShowID = false;
            this.ncmbType.Location = new System.Drawing.Point(69, 18);
            this.ncmbType.Name = "ncmbType";
            this.ncmbType.PopForm = null;
            this.ncmbType.ShowCustomerList = false;
            this.ncmbType.ShowID = false;
            this.ncmbType.Size = new System.Drawing.Size(116, 20);
            this.ncmbType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbType.TabIndex = 16;
            this.ncmbType.Tag = "";
            this.ncmbType.ToolBarUse = false;
            // 
            // ncbAST
            // 
            this.ncbAST.AutoSize = true;
            this.ncbAST.Location = new System.Drawing.Point(607, 20);
            this.ncbAST.Name = "ncbAST";
            this.ncbAST.Size = new System.Drawing.Size(48, 16);
            this.ncbAST.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbAST.TabIndex = 15;
            this.ncbAST.Text = "皮试";
            this.ncbAST.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbAST.UseVisualStyleBackColor = true;
            // 
            // ncbHide
            // 
            this.ncbHide.AutoSize = true;
            this.ncbHide.Location = new System.Drawing.Point(728, 20);
            this.ncbHide.Name = "ncbHide";
            this.ncbHide.Size = new System.Drawing.Size(48, 16);
            this.ncbHide.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbHide.TabIndex = 14;
            this.ncbHide.Text = "重整";
            this.ncbHide.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbHide.UseVisualStyleBackColor = true;
            // 
            // ncbNew
            // 
            this.ncbNew.AutoSize = true;
            this.ncbNew.Location = new System.Drawing.Point(549, 20);
            this.ncbNew.Name = "ncbNew";
            this.ncbNew.Size = new System.Drawing.Size(48, 16);
            this.ncbNew.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbNew.TabIndex = 13;
            this.ncbNew.Text = "新开";
            this.ncbNew.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbNew.UseVisualStyleBackColor = true;
            // 
            // ncbToDay
            // 
            this.ncbToDay.AutoSize = true;
            this.ncbToDay.Location = new System.Drawing.Point(488, 20);
            this.ncbToDay.Name = "ncbToDay";
            this.ncbToDay.Size = new System.Drawing.Size(48, 16);
            this.ncbToDay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbToDay.TabIndex = 12;
            this.ncbToDay.Text = "当天";
            this.ncbToDay.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbToDay.UseVisualStyleBackColor = true;
            // 
            // ncbInvalid
            // 
            this.ncbInvalid.AutoSize = true;
            this.ncbInvalid.Checked = true;
            this.ncbInvalid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbInvalid.Location = new System.Drawing.Point(427, 20);
            this.ncbInvalid.Name = "ncbInvalid";
            this.ncbInvalid.Size = new System.Drawing.Size(48, 16);
            this.ncbInvalid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbInvalid.TabIndex = 11;
            this.ncbInvalid.Text = "作废";
            this.ncbInvalid.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbInvalid.UseVisualStyleBackColor = true;
            // 
            // ncbValid
            // 
            this.ncbValid.AutoSize = true;
            this.ncbValid.Checked = true;
            this.ncbValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbValid.Location = new System.Drawing.Point(366, 20);
            this.ncbValid.Name = "ncbValid";
            this.ncbValid.Size = new System.Drawing.Size(48, 16);
            this.ncbValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbValid.TabIndex = 10;
            this.ncbValid.Text = "有效";
            this.ncbValid.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbValid.UseVisualStyleBackColor = true;
            // 
            // ncbShort
            // 
            this.ncbShort.AutoSize = true;
            this.ncbShort.Checked = true;
            this.ncbShort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbShort.Location = new System.Drawing.Point(305, 20);
            this.ncbShort.Name = "ncbShort";
            this.ncbShort.Size = new System.Drawing.Size(48, 16);
            this.ncbShort.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbShort.TabIndex = 9;
            this.ncbShort.Text = "临时";
            this.ncbShort.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbShort.UseVisualStyleBackColor = true;
            // 
            // ncbLong
            // 
            this.ncbLong.AutoSize = true;
            this.ncbLong.Checked = true;
            this.ncbLong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbLong.Location = new System.Drawing.Point(244, 20);
            this.ncbLong.Name = "ncbLong";
            this.ncbLong.Size = new System.Drawing.Size(48, 16);
            this.ncbLong.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbLong.TabIndex = 8;
            this.ncbLong.Text = "长期";
            this.ncbLong.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbLong.UseVisualStyleBackColor = true;
            // 
            // ngbAdd
            // 
            this.ngbAdd.Controls.Add(this.nlbInfo);
            this.ngbAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbAdd.Location = new System.Drawing.Point(0, 507);
            this.ngbAdd.Name = "ngbAdd";
            this.ngbAdd.Size = new System.Drawing.Size(1317, 54);
            this.ngbAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbAdd.TabIndex = 22;
            this.ngbAdd.TabStop = false;
            this.ngbAdd.Text = "附加信息";
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo.Location = new System.Drawing.Point(22, 26);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(587, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 12;
            this.nlbInfo.Text = "2001床 刘德华 男 50岁 居民医保 2011-07-20 00:00:00入院 目前住院1天 发生费用1000.00元 余额100.00元";
            // 
            // neuPanelDetail
            // 
            this.neuPanelDetail.Controls.Add(this.ucOrderExecFeeDetail);
            this.neuPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelDetail.Location = new System.Drawing.Point(0, 51);
            this.neuPanelDetail.Name = "neuPanelDetail";
            this.neuPanelDetail.Size = new System.Drawing.Size(1317, 456);
            this.neuPanelDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelDetail.TabIndex = 23;
            // 
            // ucOrderExecFeeDetail
            // 
            this.ucOrderExecFeeDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucOrderExecFeeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ucOrderExecFeeDetail.IsPrint = false;
            this.ucOrderExecFeeDetail.Location = new System.Drawing.Point(0, 0);
            this.ucOrderExecFeeDetail.Name = "ucOrderExecFeeDetail";
            this.ucOrderExecFeeDetail.Size = new System.Drawing.Size(1317, 456);
            this.ucOrderExecFeeDetail.TabIndex = 0;
            // 
            // ucOrderExecFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanelDetail);
            this.Controls.Add(this.ngbAdd);
            this.Controls.Add(this.ngbQuerySet);
            this.Name = "ucOrderExecFee";
            this.Size = new System.Drawing.Size(1317, 561);
            this.ngbQuerySet.ResumeLayout(false);
            this.ngbQuerySet.PerformLayout();
            this.ngbAdd.ResumeLayout(false);
            this.ngbAdd.PerformLayout();
            this.neuPanelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQuerySet;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbEmergency;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbType;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbAST;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbHide;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbNew;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbToDay;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbInvalid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbValid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbShort;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbLong;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbAdd;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanelDetail;
        private FS.SOC.HISFC.Components.NurseStation.Base.ucOrderExecFeeDetail ucOrderExecFeeDetail;
    }
}
