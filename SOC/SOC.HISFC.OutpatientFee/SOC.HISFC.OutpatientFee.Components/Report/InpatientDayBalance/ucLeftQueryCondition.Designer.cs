namespace FS.SOC.HISFC.OutpatientFee.Components.Report.InpatientDayBalance
{
    partial class ucLeftQueryCondition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLeftQueryCondition));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("科室(0)");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("科室(0)");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("科室(0)");
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dtBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbBalanceList = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tvDeptList1 = new FS.FrameWork.WinForms.Controls.tvDeptList(this.components);
            this.neuPanel1.SuspendLayout();
            this.gbBalanceList.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.btnQuery);
            this.neuPanel1.Controls.Add(this.dtBeginTime);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(234, 30);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(145, 5);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(60, 23);
            this.btnQuery.TabIndex = 13;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.CustomFormat = "yyyy-MM";
            this.dtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginTime.IsEnter2Tab = false;
            this.dtBeginTime.Location = new System.Drawing.Point(51, 6);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(88, 21);
            this.dtBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBeginTime.TabIndex = 12;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(9, 10);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 11;
            this.neuLabel1.Text = "月份：";
            // 
            // gbBalanceList
            // 
            this.gbBalanceList.Controls.Add(this.tvDeptList1);
            this.gbBalanceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBalanceList.Location = new System.Drawing.Point(0, 30);
            this.gbBalanceList.Name = "gbBalanceList";
            this.gbBalanceList.Size = new System.Drawing.Size(234, 447);
            this.gbBalanceList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbBalanceList.TabIndex = 3;
            this.gbBalanceList.TabStop = false;
            // 
            // tvDeptList1
            // 
            this.tvDeptList1.alDepartments = ((System.Collections.ArrayList)(resources.GetObject("tvDeptList1.alDepartments")));
            this.tvDeptList1.Checked = FS.FrameWork.WinForms.Controls.tvDeptList.enuChecked.None;
            this.tvDeptList1.Direction = FS.FrameWork.WinForms.Controls.tvDeptList.enuShowDirection.Behind;
            this.tvDeptList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDeptList1.ImageIndex = 0;
            this.tvDeptList1.IsShowCount = true;
            this.tvDeptList1.Location = new System.Drawing.Point(3, 17);
            this.tvDeptList1.Name = "tvDeptList1";
            treeNode1.Name = "";
            treeNode1.Text = "科室(0)";
            treeNode2.Name = "";
            treeNode2.Text = "科室(0)";
            treeNode3.Name = "";
            treeNode3.Text = "科室(0)";
            this.tvDeptList1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.tvDeptList1.SelectedImageIndex = 0;
            this.tvDeptList1.ShowType = FS.FrameWork.WinForms.Controls.tvDeptList.enuShowType.None;
            this.tvDeptList1.Size = new System.Drawing.Size(228, 427);
            this.tvDeptList1.TabIndex = 0;
            // 
            // ucLeftQueryCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbBalanceList);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucLeftQueryCondition";
            this.Size = new System.Drawing.Size(234, 477);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.gbBalanceList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbBalanceList;
        private FS.FrameWork.WinForms.Controls.tvDeptList tvDeptList1;
        private System.Windows.Forms.Button btnQuery;

    }
}
