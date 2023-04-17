namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucOutPatientRecipeItemSelect
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOutPatientRecipeItemSelect));
            this.cmbUnit = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtQTY = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.chkDrugEmerce = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtCombNo = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.ucInputItem1 = new FS.HISFC.Components.Common.Controls.ucInputItem();
            this.ucOrderInputByType1 = new FS.HISFC.Components.Order.OutPatient.Controls.ucOrderInputByType();
            ((System.ComponentModel.ISupportInitialize)(this.txtQTY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCombNo)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbUnit
            // 
            this.cmbUnit.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.cmbUnit, "cmbUnit");
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
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.ShowCustomerList = false;
            this.cmbUnit.ShowID = false;
            this.cmbUnit.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbUnit.Tag = "";
            this.cmbUnit.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            resources.ApplyResources(this.neuLabel1, "neuLabel1");
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // neuLabel2
            // 
            resources.ApplyResources(this.neuLabel2, "neuLabel2");
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // txtQTY
            // 
            this.txtQTY.DecimalPlaces = 1;
            resources.ApplyResources(this.txtQTY, "txtQTY");
            this.txtQTY.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQTY.ValueChanged += new System.EventHandler(this.txtQTY_ValueChanged);
            // 
            // chkDrugEmerce
            // 
            resources.ApplyResources(this.chkDrugEmerce, "chkDrugEmerce");
            this.chkDrugEmerce.ForeColor = System.Drawing.Color.Red;
            this.chkDrugEmerce.Name = "chkDrugEmerce";
            this.chkDrugEmerce.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkDrugEmerce.TabStop = false;
            this.chkDrugEmerce.UseVisualStyleBackColor = true;
            this.chkDrugEmerce.CheckedChanged += new System.EventHandler(this.chkDrugEmerce_CheckedChanged);
            // 
            // txtCombNo
            // 
            resources.ApplyResources(this.txtCombNo, "txtCombNo");
            this.txtCombNo.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtCombNo.Name = "txtCombNo";
            this.txtCombNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // ucInputItem1
            // 
            this.ucInputItem1.AlCatagory = ((System.Collections.ArrayList)(resources.GetObject("ucInputItem1.AlCatagory")));
            this.ucInputItem1.DrugSendType = "A";
            this.ucInputItem1.FeeItem = ((FS.FrameWork.Models.NeuObject)(resources.GetObject("ucInputItem1.FeeItem")));
            resources.ApplyResources(this.ucInputItem1, "ucInputItem1");
            this.ucInputItem1.FontSize = null;
            this.ucInputItem1.InputType = 0;
            this.ucInputItem1.IsDeptUsedFlag = true;
            this.ucInputItem1.IsIncludeMat = false;
            this.ucInputItem1.IsListShowAlways = false;
            this.ucInputItem1.IsShowCategory = true;
            this.ucInputItem1.IsShowInput = true;
            this.ucInputItem1.IsShowSelfMark = true;
            this.ucInputItem1.Name = "ucInputItem1";
            //this.ucInputItem1.PactInfo = null;
            this.ucInputItem1.Patient = null;
            this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.ItemType;
            this.ucInputItem1.ShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.OutPharmacy;
            this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.All;
            // 
            // ucOrderInputByType1
            // 
            this.ucOrderInputByType1.BackColor = System.Drawing.Color.White;
            this.ucOrderInputByType1.IsQtyChanged = false;
            this.ucOrderInputByType1.IsUndrugShowFrequency = false;
            resources.ApplyResources(this.ucOrderInputByType1, "ucOrderInputByType1");
            this.ucOrderInputByType1.Name = "ucOrderInputByType1";
            this.ucOrderInputByType1.Order = null;
            this.ucOrderInputByType1.UseDays = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ucOutPatientRecipeItemSelect
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtCombNo);
            this.Controls.Add(this.chkDrugEmerce);
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.ucOrderInputByType1);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.ucInputItem1);
            this.Name = "ucOutPatientRecipeItemSelect";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.txtQTY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCombNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public FS.HISFC.Components.Common.Controls.ucInputItem ucInputItem1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbUnit;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        public ucOrderInputByType ucOrderInputByType1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown txtQTY;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkDrugEmerce;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown txtCombNo;


    }
}