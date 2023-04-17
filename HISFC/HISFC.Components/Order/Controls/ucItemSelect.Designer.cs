namespace FS.HISFC.Components.Order.Controls
{
    partial class ucItemSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucItemSelect));
            this.cmbOrderType1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ucInputItem1 = new FS.HISFC.Components.Common.Controls.ucInputItem();
            this.lblQty = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbUnit = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtQuantity = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.panelEndDate = new System.Windows.Forms.Panel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCombNo = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucOrderInputByType1 = new FS.HISFC.Components.Order.Controls.ucOrderInputByType();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).BeginInit();
            this.panelEndDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCombNo)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbOrderType1
            // 
            this.cmbOrderType1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOrderType1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbOrderType1.FormattingEnabled = true;
            this.cmbOrderType1.IsEnter2Tab = false;
            this.cmbOrderType1.IsFlat = false;
            this.cmbOrderType1.IsLike = true;
            this.cmbOrderType1.IsListOnly = false;
            this.cmbOrderType1.IsPopForm = true;
            this.cmbOrderType1.IsShowCustomerList = false;
            this.cmbOrderType1.IsShowID = false;
            this.cmbOrderType1.IsShowIDAndName = false;
            this.cmbOrderType1.Location = new System.Drawing.Point(3, 3);
            this.cmbOrderType1.Name = "cmbOrderType1";
            this.cmbOrderType1.ShowCustomerList = false;
            this.cmbOrderType1.ShowID = false;
            this.cmbOrderType1.Size = new System.Drawing.Size(73, 20);
            this.cmbOrderType1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOrderType1.TabIndex = 0;
            this.cmbOrderType1.Tag = "";
            this.cmbOrderType1.ToolBarUse = false;
            // 
            // ucInputItem1
            // 
            this.ucInputItem1.AlCatagory = ((System.Collections.ArrayList)(resources.GetObject("ucInputItem1.AlCatagory")));
            this.ucInputItem1.DrugSendType = "A";
            this.ucInputItem1.FeeItem = ((FS.FrameWork.Models.NeuObject)(resources.GetObject("ucInputItem1.FeeItem")));
            this.ucInputItem1.FontSize = null;
            this.ucInputItem1.InputType = 0;
            this.ucInputItem1.IsDeptUsedFlag = false;
            this.ucInputItem1.IsIncludeMat = false;
            this.ucInputItem1.IsListShowAlways = false;
            this.ucInputItem1.IsShowCategory = true;
            this.ucInputItem1.IsShowInput = true;
            this.ucInputItem1.IsShowSelfMark = true;
            this.ucInputItem1.Location = new System.Drawing.Point(73, -6);
            this.ucInputItem1.Name = "ucInputItem1";
            //this.ucInputItem1.PactInfo = null;
            this.ucInputItem1.Patient = null;
            this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.ItemType;
            this.ucInputItem1.ShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.All;
            this.ucInputItem1.Size = new System.Drawing.Size(443, 39);
            this.ucInputItem1.TabIndex = 2;
            this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.All;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(516, 8);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(35, 12);
            this.lblQty.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblQty.TabIndex = 2;
            this.lblQty.Text = "数量:";
            // 
            // cmbUnit
            // 
            this.cmbUnit.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.IsEnter2Tab = false;
            this.cmbUnit.IsFlat = false;
            this.cmbUnit.IsLike = true;
            this.cmbUnit.IsListOnly = false;
            this.cmbUnit.IsPopForm = true;
            this.cmbUnit.IsShowCustomerList = false;
            this.cmbUnit.IsShowID = false;
            this.cmbUnit.IsShowIDAndName = false;
            this.cmbUnit.Location = new System.Drawing.Point(600, 5);
            this.cmbUnit.MaxLength = 20;
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.ShowCustomerList = false;
            this.cmbUnit.ShowID = false;
            this.cmbUnit.Size = new System.Drawing.Size(43, 20);
            this.cmbUnit.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbUnit.TabIndex = 4;
            this.cmbUnit.Tag = "";
            this.cmbUnit.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(699, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label2.TabIndex = 6;
            this.label2.Text = "开始:";
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(733, 4);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(144, 21);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 6;
            this.dtBegin.TabStop = false;
            // 
            // txtQuantity
            // 
            this.txtQuantity.DecimalPlaces = 1;
            this.txtQuantity.Location = new System.Drawing.Point(550, 4);
            this.txtQuantity.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(49, 21);
            this.txtQuantity.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelEndDate
            // 
            this.panelEndDate.Controls.Add(this.dtEnd);
            this.panelEndDate.Controls.Add(this.neuLabel5);
            this.panelEndDate.Location = new System.Drawing.Point(697, 40);
            this.panelEndDate.Name = "panelEndDate";
            this.panelEndDate.Size = new System.Drawing.Size(178, 24);
            this.panelEndDate.TabIndex = 13;
            this.panelEndDate.Visible = false;
            // 
            // dtEnd
            // 
            this.dtEnd.Checked = false;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(34, 1);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.ShowCheckBox = true;
            this.dtEnd.Size = new System.Drawing.Size(144, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 1;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(1, 5);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(35, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 0;
            this.neuLabel5.Text = "停止:";
            // 
            // txtCombNo
            // 
            this.txtCombNo.Font = new System.Drawing.Font("宋体", 9F);
            this.txtCombNo.Location = new System.Drawing.Point(38, 39);
            this.txtCombNo.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.txtCombNo.Name = "txtCombNo";
            this.txtCombNo.Size = new System.Drawing.Size(38, 21);
            this.txtCombNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCombNo.TabIndex = 6;
            this.txtCombNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCombNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCombNo.Leave += new System.EventHandler(this.txtCombNo_Leave);
            this.txtCombNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCombNo_KeyPress);
            this.txtCombNo.Enter += new System.EventHandler(this.txtCombNo_Enter);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9F);
            this.neuLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.neuLabel2.Location = new System.Drawing.Point(3, 44);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(35, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            this.neuLabel2.Text = "组号:";
            // 
            // ucOrderInputByType1
            // 
            this.ucOrderInputByType1.BackColor = System.Drawing.Color.White;
            this.ucOrderInputByType1.IsQtyChanged = false;
            this.ucOrderInputByType1.IsUndrugShowFrequency = true;
            this.ucOrderInputByType1.Location = new System.Drawing.Point(72, 30);
            this.ucOrderInputByType1.Name = "ucOrderInputByType1";
            this.ucOrderInputByType1.Order = null;
            this.ucOrderInputByType1.Size = new System.Drawing.Size(1024, 41);
            this.ucOrderInputByType1.TabIndex = 5;
            // 
            // ucItemSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtCombNo);
            this.Controls.Add(this.panelEndDate);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.dtBegin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucOrderInputByType1);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.cmbOrderType1);
            this.Controls.Add(this.ucInputItem1);
            this.Name = "ucItemSelect";
            this.Size = new System.Drawing.Size(1024, 70);
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).EndInit();
            this.panelEndDate.ResumeLayout(false);
            this.panelEndDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCombNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOrderType1;
        public FS.HISFC.Components.Common.Controls.ucInputItem ucInputItem1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblQty;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbUnit;
        private ucOrderInputByType ucOrderInputByType1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown txtQuantity;
        private System.Windows.Forms.Panel panelEndDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown txtCombNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}
