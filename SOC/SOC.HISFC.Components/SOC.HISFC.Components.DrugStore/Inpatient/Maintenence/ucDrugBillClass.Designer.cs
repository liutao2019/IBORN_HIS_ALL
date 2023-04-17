namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Maintenence
{
    partial class ucDrugBillClass
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("药品类别", 0, 0);
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvPutDrugBill1 = new FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.lvDrugBillClass(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbxPutType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtMark = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbxIsValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbxPrinttype = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Controls.Add(this.tabPage2);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(794, 169);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 0;
            this.neuTabControl1.SelectedIndexChanged += new System.EventHandler(this.neuTabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvPutDrugBill1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(786, 144);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "摆药单列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvPutDrugBill1
            // 
            this.lvPutDrugBill1.AllowColumnReorder = true;
            this.lvPutDrugBill1.CheckBoxes = true;
            this.lvPutDrugBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPutDrugBill1.FullRowSelect = true;
            this.lvPutDrugBill1.GridLines = true;
            this.lvPutDrugBill1.Location = new System.Drawing.Point(3, 3);
            this.lvPutDrugBill1.Name = "lvPutDrugBill1";
            this.lvPutDrugBill1.Size = new System.Drawing.Size(780, 138);
            this.lvPutDrugBill1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPutDrugBill1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lvPutDrugBill1.TabIndex = 0;
            this.lvPutDrugBill1.UseCompatibleStateImageBehavior = false;
            this.lvPutDrugBill1.View = System.Windows.Forms.View.Details;
            this.lvPutDrugBill1.SelectedIndexChanged += new System.EventHandler(this.lvPutDrugBill1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.neuGroupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(786, 144);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "摆药单设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.btnSave);
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.cbxPutType);
            this.neuGroupBox1.Controls.Add(this.txtMark);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.cbxIsValid);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.txtName);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.cbxPrinttype);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(780, 138);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "摆药单信息";
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(589, 109);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存摆药单";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(364, 50);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(53, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 11;
            this.neuLabel4.Text = "摆药类型";
            // 
            // cbxPutType
            // 
            this.cbxPutType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbxPutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPutType.FormattingEnabled = true;
            this.cbxPutType.IsFlat = false;
            this.cbxPutType.IsLike = true;
            this.cbxPutType.Location = new System.Drawing.Point(423, 47);
            this.cbxPutType.Name = "cbxPutType";
            this.cbxPutType.PopForm = null;
            this.cbxPutType.ShowCustomerList = false;
            this.cbxPutType.ShowID = false;
            this.cbxPutType.Size = new System.Drawing.Size(230, 20);
            this.cbxPutType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbxPutType.TabIndex = 10;
            this.cbxPutType.Tag = "";
            this.cbxPutType.ToolBarUse = false;
            // 
            // txtMark
            // 
            this.txtMark.Location = new System.Drawing.Point(96, 73);
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(635, 21);
            this.txtMark.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMark.TabIndex = 7;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(61, 76);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(29, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "备注";
            // 
            // cbxIsValid
            // 
            this.cbxIsValid.AutoSize = true;
            this.cbxIsValid.Location = new System.Drawing.Point(659, 23);
            this.cbxIsValid.Name = "cbxIsValid";
            this.cbxIsValid.Size = new System.Drawing.Size(72, 16);
            this.cbxIsValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbxIsValid.TabIndex = 5;
            this.cbxIsValid.Text = "是否有效";
            this.cbxIsValid.UseVisualStyleBackColor = true;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(37, 50);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "打印类型";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(96, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(557, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 3;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(25, 23);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "摆药单名称";
            // 
            // cbxPrinttype
            // 
            this.cbxPrinttype.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbxPrinttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrinttype.FormattingEnabled = true;
            this.cbxPrinttype.IsFlat = true;
            this.cbxPrinttype.IsLike = true;
            this.cbxPrinttype.Location = new System.Drawing.Point(96, 47);
            this.cbxPrinttype.Name = "cbxPrinttype";
            this.cbxPrinttype.PopForm = null;
            this.cbxPrinttype.ShowCustomerList = false;
            this.cbxPrinttype.ShowID = false;
            this.cbxPrinttype.Size = new System.Drawing.Size(230, 20);
            this.cbxPrinttype.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbxPrinttype.TabIndex = 1;
            this.cbxPrinttype.Tag = "";
            this.cbxPrinttype.ToolBarUse = false;
            // 
            // ucDrugBill
            // 
            this.Controls.Add(this.neuTabControl1);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDrugBill";
            this.Size = new System.Drawing.Size(794, 393);
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private FS.SOC.HISFC.Components.DrugStore.Inpatient.Base.lvDrugBillClass lvPutDrugBill1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbxPrinttype;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbxIsValid;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMark;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbxPutType;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;

    }
}
