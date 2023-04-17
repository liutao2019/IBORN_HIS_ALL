﻿namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Components
{
    partial class ucBedFeeItemModify
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ckbBabyRelation = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckbTimeRelation = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.DateTimeBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.DateTimeBox();
            this.ntxtQty = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.ntxtPrice = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.cmbItemInfo = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbValidState = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ckOutFeeFlag = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckExtFlag = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ckbContinue = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbUseLimit = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(19, 32);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "项目名称:";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(19, 59);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "单    价:";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(19, 86);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 2;
            this.neuLabel3.Text = "数    量:";
            // 
            // ckbBabyRelation
            // 
            this.ckbBabyRelation.AutoSize = true;
            this.ckbBabyRelation.Location = new System.Drawing.Point(16, 159);
            this.ckbBabyRelation.Name = "ckbBabyRelation";
            this.ckbBabyRelation.Size = new System.Drawing.Size(72, 16);
            this.ckbBabyRelation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckbBabyRelation.TabIndex = 5;
            this.ckbBabyRelation.Text = "婴儿相关";
            this.ckbBabyRelation.UseVisualStyleBackColor = true;
            // 
            // ckbTimeRelation
            // 
            this.ckbTimeRelation.AutoSize = true;
            this.ckbTimeRelation.Location = new System.Drawing.Point(153, 167);
            this.ckbTimeRelation.Name = "ckbTimeRelation";
            this.ckbTimeRelation.Size = new System.Drawing.Size(72, 16);
            this.ckbTimeRelation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckbTimeRelation.TabIndex = 6;
            this.ckbTimeRelation.Text = "时间相关";
            this.ckbTimeRelation.UseVisualStyleBackColor = true;
            this.ckbTimeRelation.CheckedChanged += new System.EventHandler(this.ckbTimeRelation_CheckedChanged);
            // 
            // dtBegin
            // 
            this.dtBegin.Checked = false;
            this.dtBegin.CustomFormat = "";
            this.dtBegin.Enabled = false;
            this.dtBegin.Image = null;
            this.dtBegin.Location = new System.Drawing.Point(79, 109);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.ShowCheckBox = true;
            this.dtBegin.Size = new System.Drawing.Size(232, 21);
            this.dtBegin.TabIndex = 3;
            this.dtBegin.Value = new System.DateTime(((long)(0)));
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(19, 113);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(59, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 4;
            this.neuLabel4.Text = "开始时间:";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(19, 140);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(59, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 4;
            this.neuLabel5.Text = "结束时间:";
            // 
            // dtEnd
            // 
            this.dtEnd.Checked = false;
            this.dtEnd.Enabled = false;
            this.dtEnd.Image = null;
            this.dtEnd.Location = new System.Drawing.Point(79, 136);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.ShowCheckBox = true;
            this.dtEnd.Size = new System.Drawing.Size(232, 21);
            this.dtEnd.TabIndex = 4;
            this.dtEnd.Value = new System.DateTime(((long)(0)));
            // 
            // ntxtQty
            // 
            this.ntxtQty.AllowNegative = false;
            this.ntxtQty.IsAutoRemoveDecimalZero = false;
            this.ntxtQty.IsEnter2Tab = false;
            this.ntxtQty.Location = new System.Drawing.Point(79, 82);
            this.ntxtQty.Name = "ntxtQty";
            this.ntxtQty.NumericPrecision = 4;
            this.ntxtQty.NumericScaleOnFocus = 2;
            this.ntxtQty.NumericScaleOnLostFocus = 2;
            this.ntxtQty.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ntxtQty.SetRange = new System.Drawing.Size(-1, -1);
            this.ntxtQty.Size = new System.Drawing.Size(232, 21);
            this.ntxtQty.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtQty.TabIndex = 2;
            this.ntxtQty.Text = "0.00";
            this.ntxtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntxtQty.UseGroupSeperator = true;
            this.ntxtQty.ZeroIsValid = true;
            // 
            // ntxtPrice
            // 
            this.ntxtPrice.AllowNegative = false;
            this.ntxtPrice.Enabled = false;
            this.ntxtPrice.IsAutoRemoveDecimalZero = false;
            this.ntxtPrice.IsEnter2Tab = false;
            this.ntxtPrice.Location = new System.Drawing.Point(79, 55);
            this.ntxtPrice.Name = "ntxtPrice";
            this.ntxtPrice.NumericPrecision = 12;
            this.ntxtPrice.NumericScaleOnFocus = 4;
            this.ntxtPrice.NumericScaleOnLostFocus = 4;
            this.ntxtPrice.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ntxtPrice.SetRange = new System.Drawing.Size(-1, -1);
            this.ntxtPrice.Size = new System.Drawing.Size(232, 21);
            this.ntxtPrice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtPrice.TabIndex = 0;
            this.ntxtPrice.TabStop = false;
            this.ntxtPrice.Text = "0.0000";
            this.ntxtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntxtPrice.UseGroupSeperator = true;
            this.ntxtPrice.ZeroIsValid = false;
            // 
            // cmbItemInfo
            // 
            this.cmbItemInfo.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbItemInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemInfo.FormattingEnabled = true;
            this.cmbItemInfo.IsEnter2Tab = false;
            this.cmbItemInfo.IsFlat = false;
            this.cmbItemInfo.IsLike = true;
            this.cmbItemInfo.IsListOnly = false;
            this.cmbItemInfo.IsPopForm = true;
            this.cmbItemInfo.IsShowCustomerList = false;
            this.cmbItemInfo.IsShowID = false;
            this.cmbItemInfo.Location = new System.Drawing.Point(79, 28);
            this.cmbItemInfo.Name = "cmbItemInfo";
            this.cmbItemInfo.PopForm = null;
            this.cmbItemInfo.ShowCustomerList = false;
            this.cmbItemInfo.ShowID = false;
            this.cmbItemInfo.Size = new System.Drawing.Size(232, 20);
            this.cmbItemInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbItemInfo.TabIndex = 1;
            this.cmbItemInfo.Tag = "";
            this.cmbItemInfo.ToolBarUse = false;
            this.cmbItemInfo.SelectedIndexChanged += new System.EventHandler(this.cmbItemInfo_SelectedIndexChanged);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(173, 218);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(59, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 12;
            this.neuLabel6.Text = "状　　态:";
            // 
            // cmbValidState
            // 
            this.cmbValidState.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbValidState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValidState.FormattingEnabled = true;
            this.cmbValidState.IsEnter2Tab = false;
            this.cmbValidState.IsFlat = false;
            this.cmbValidState.IsLike = true;
            this.cmbValidState.IsListOnly = false;
            this.cmbValidState.IsPopForm = true;
            this.cmbValidState.IsShowCustomerList = false;
            this.cmbValidState.IsShowID = false;
            this.cmbValidState.Location = new System.Drawing.Point(233, 214);
            this.cmbValidState.Name = "cmbValidState";
            this.cmbValidState.PopForm = null;
            this.cmbValidState.ShowCustomerList = false;
            this.cmbValidState.ShowID = false;
            this.cmbValidState.Size = new System.Drawing.Size(73, 20);
            this.cmbValidState.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbValidState.TabIndex = 7;
            this.cmbValidState.Tag = "";
            this.cmbValidState.ToolBarUse = false;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.cmbUseLimit);
            this.neuGroupBox1.Controls.Add(this.neuLabel7);
            this.neuGroupBox1.Controls.Add(this.ckOutFeeFlag);
            this.neuGroupBox1.Controls.Add(this.ckExtFlag);
            this.neuGroupBox1.Controls.Add(this.cmbValidState);
            this.neuGroupBox1.Controls.Add(this.neuLabel6);
            this.neuGroupBox1.Controls.Add(this.ckbBabyRelation);
            this.neuGroupBox1.Location = new System.Drawing.Point(5, 8);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(317, 249);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 14;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "等级属性";
            // 
            // ckOutFeeFlag
            // 
            this.ckOutFeeFlag.AutoSize = true;
            this.ckOutFeeFlag.Location = new System.Drawing.Point(148, 188);
            this.ckOutFeeFlag.Name = "ckOutFeeFlag";
            this.ckOutFeeFlag.Size = new System.Drawing.Size(72, 16);
            this.ckOutFeeFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckOutFeeFlag.TabIndex = 14;
            this.ckOutFeeFlag.Text = "出院计费";
            this.ckOutFeeFlag.UseVisualStyleBackColor = true;
            // 
            // ckExtFlag
            // 
            this.ckExtFlag.AutoSize = true;
            this.ckExtFlag.Location = new System.Drawing.Point(16, 188);
            this.ckExtFlag.Name = "ckExtFlag";
            this.ckExtFlag.Size = new System.Drawing.Size(84, 16);
            this.ckExtFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckExtFlag.TabIndex = 13;
            this.ckExtFlag.Text = "非在院计费";
            this.ckExtFlag.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(91, 263);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOk_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(170, 263);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ckbContinue
            // 
            this.ckbContinue.AutoSize = true;
            this.ckbContinue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbContinue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ckbContinue.Location = new System.Drawing.Point(13, 266);
            this.ckbContinue.Name = "ckbContinue";
            this.ckbContinue.Size = new System.Drawing.Size(76, 16);
            this.ckbContinue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckbContinue.TabIndex = 23;
            this.ckbContinue.Text = "连续录入";
            this.ckbContinue.UseVisualStyleBackColor = true;
            // 
            // cmbUseLimit
            // 
            this.cmbUseLimit.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbUseLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUseLimit.FormattingEnabled = true;
            this.cmbUseLimit.IsEnter2Tab = false;
            this.cmbUseLimit.IsFlat = false;
            this.cmbUseLimit.IsLike = true;
            this.cmbUseLimit.IsListOnly = false;
            this.cmbUseLimit.IsPopForm = true;
            this.cmbUseLimit.IsShowCustomerList = false;
            this.cmbUseLimit.IsShowID = false;
            this.cmbUseLimit.Location = new System.Drawing.Point(74, 214);
            this.cmbUseLimit.Name = "cmbUseLimit";
            this.cmbUseLimit.PopForm = null;
            this.cmbUseLimit.ShowCustomerList = false;
            this.cmbUseLimit.ShowID = false;
            this.cmbUseLimit.Size = new System.Drawing.Size(77, 20);
            this.cmbUseLimit.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbUseLimit.TabIndex = 15;
            this.cmbUseLimit.Tag = "";
            this.cmbUseLimit.ToolBarUse = false;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(14, 218);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(59, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 16;
            this.neuLabel7.Text = "使用限制:";
            // 
            // ucBedFeeItemModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckbContinue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbItemInfo);
            this.Controls.Add(this.ntxtPrice);
            this.Controls.Add(this.ntxtQty);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.dtBegin);
            this.Controls.Add(this.ckbTimeRelation);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucBedFeeItemModify";
            this.Size = new System.Drawing.Size(325, 297);
            this.Load += new System.EventHandler(this.ucBedFeeItemModify_load);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckbBabyRelation;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckbTimeRelation;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.DateTimeBox dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox ntxtQty;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox ntxtPrice;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbItemInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbValidState;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckbContinue;
        private FS.FrameWork.WinForms.Controls.DateTimeBox dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckOutFeeFlag;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckExtFlag;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbUseLimit;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
    }
}
