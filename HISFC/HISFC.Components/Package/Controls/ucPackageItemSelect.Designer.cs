namespace HISFC.Components.Package.Controls
{
    partial class ucPackageItemSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPackageItemSelect));
            this.ucInputItem1 = new FS.HISFC.Components.Common.Controls.ucInputItem();
            this.txtQTY = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.lblQty = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbUnit = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMemo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtQTY)).BeginInit();
            this.SuspendLayout();
            // 
            // ucInputItem1
            // 
            this.ucInputItem1.AlCatagory = ((System.Collections.ArrayList)(resources.GetObject("ucInputItem1.AlCatagory")));
            this.ucInputItem1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucInputItem1.DrugSendType = "A";
            this.ucInputItem1.FeeItem = ((FS.FrameWork.Models.NeuObject)(resources.GetObject("ucInputItem1.FeeItem")));
            this.ucInputItem1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucInputItem1.FontSize = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucInputItem1.InputType = 0;
            this.ucInputItem1.IsDeptUsedFlag = true;
            this.ucInputItem1.IsIncludeMat = false;
            this.ucInputItem1.IsListShowAlways = false;
            this.ucInputItem1.IsShowCategory = true;
            this.ucInputItem1.IsShowInput = true;
            this.ucInputItem1.IsShowSelfMark = true;
            this.ucInputItem1.Location = new System.Drawing.Point(0, 0);
            this.ucInputItem1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ucInputItem1.Name = "ucInputItem1";
            this.ucInputItem1.Patient = null;
            this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.ItemType;
            this.ucInputItem1.ShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.All;
            this.ucInputItem1.Size = new System.Drawing.Size(614, 40);
            this.ucInputItem1.TabIndex = 1;
            this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.All;
            // 
            // txtQTY
            // 
            this.txtQTY.DecimalPlaces = 1;
            this.txtQTY.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQTY.Location = new System.Drawing.Point(666, 9);
            this.txtQTY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtQTY.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(64, 21);
            this.txtQTY.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQTY.TabIndex = 4;
            this.txtQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQty.Location = new System.Drawing.Point(622, 14);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(35, 12);
            this.lblQty.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblQty.TabIndex = 6;
            this.lblQty.Text = "数量:";
            // 
            // cmbUnit
            // 
            this.cmbUnit.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbUnit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbUnit.ForeColor = System.Drawing.Color.Blue;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.IsEnter2Tab = false;
            this.cmbUnit.IsFlat = false;
            this.cmbUnit.IsLike = true;
            this.cmbUnit.IsListOnly = false;
            this.cmbUnit.IsPopForm = true;
            this.cmbUnit.IsShowCustomerList = false;
            this.cmbUnit.IsShowID = false;
            this.cmbUnit.IsShowIDAndName = false;
            this.cmbUnit.Location = new System.Drawing.Point(736, 9);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.ShowCustomerList = false;
            this.cmbUnit.ShowID = false;
            this.cmbUnit.Size = new System.Drawing.Size(60, 20);
            this.cmbUnit.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbUnit.TabIndex = 5;
            this.cmbUnit.Tag = "";
            this.cmbUnit.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.neuLabel1.Location = new System.Drawing.Point(802, 14);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(35, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "备注:";
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMemo.Location = new System.Drawing.Point(843, 9);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(198, 21);
            this.txtMemo.TabIndex = 8;
            // 
            // ucPackageItemSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.ucInputItem1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucPackageItemSelect";
            this.Size = new System.Drawing.Size(1123, 40);
            ((System.ComponentModel.ISupportInitialize)(this.txtQTY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public FS.HISFC.Components.Common.Controls.ucInputItem ucInputItem1;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown txtQTY;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblQty;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbUnit;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.TextBox txtMemo;
    }
}
