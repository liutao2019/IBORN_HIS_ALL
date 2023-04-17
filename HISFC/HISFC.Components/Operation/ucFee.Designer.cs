namespace FS.HISFC.Components.Operation
{
    partial class ucFee
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tvGroup = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.neuSplitter2 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.ucRegistrationTree1 = new FS.HISFC.Components.Operation.ucRegistrationTree(this.components);
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.ucFeeForm1 = new FS.HISFC.Components.Operation.ucFeeForm();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.tvGroup);
            this.neuPanel1.Controls.Add(this.neuSplitter2);
            this.neuPanel1.Controls.Add(this.ucRegistrationTree1);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(225, 714);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // tvGroup
            // 
            this.tvGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvGroup.HideSelection = false;
            this.tvGroup.Location = new System.Drawing.Point(0, 477);
            this.tvGroup.Name = "tvGroup";
            this.tvGroup.Size = new System.Drawing.Size(225, 237);
            this.tvGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvGroup.TabIndex = 4;
            this.tvGroup.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvGroup_NodeMouseDoubleClick);
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuSplitter2.Location = new System.Drawing.Point(0, 474);
            this.neuSplitter2.Name = "neuSplitter2";
            this.neuSplitter2.Size = new System.Drawing.Size(225, 3);
            this.neuSplitter2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter2.TabIndex = 3;
            this.neuSplitter2.TabStop = false;
            // 
            // ucRegistrationTree1
            // 
            this.ucRegistrationTree1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucRegistrationTree1.HideSelection = false;
            this.ucRegistrationTree1.ListType = FS.HISFC.Components.Operation.ucRegistrationTree.EnumListType.Operation;
            this.ucRegistrationTree1.Location = new System.Drawing.Point(0, 105);
            this.ucRegistrationTree1.Name = "ucRegistrationTree1";
            this.ucRegistrationTree1.ShowCanceled = true;
            this.ucRegistrationTree1.Size = new System.Drawing.Size(225, 369);
            this.ucRegistrationTree1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ucRegistrationTree1.TabIndex = 2;
            this.ucRegistrationTree1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ucRegistrationTree1_NodeMouseDoubleClick);
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.Transparent;
            this.neuPanel2.Controls.Add(this.ucQueryInpatientNo1);
            this.neuPanel2.Controls.Add(this.neuDateTimePicker2);
            this.neuPanel2.Controls.Add(this.neuDateTimePicker1);
            this.neuPanel2.Controls.Add(this.neuLabel3);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(225, 105);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
           // this.ucQueryInpatientNo1.IsDeptOnly = true;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(27, 6);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(167, 27);
            this.ucQueryInpatientNo1.TabIndex = 7;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // neuDateTimePicker2
            // 
            this.neuDateTimePicker2.IsEnter2Tab = false;
            this.neuDateTimePicker2.Location = new System.Drawing.Point(87, 66);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(126, 21);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 2;
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(87, 39);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(126, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 2;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(16, 70);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "结束时间：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(16, 43);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "开始时间：";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(225, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 714);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // ucFeeForm1
            // 
            this.ucFeeForm1.BackColor = System.Drawing.Color.White;
            this.ucFeeForm1.DefaultExeDeptIsDeptIn = false;
            this.ucFeeForm1.DefaultStorageDept = "";
            this.ucFeeForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFeeForm1.IsJudgeQty = true;
            this.ucFeeForm1.IsNeedUOOrder = false;
            this.ucFeeForm1.IsOnlyUO = false;
            this.ucFeeForm1.ListType = FS.HISFC.Components.Operation.ucRegistrationTree.EnumListType.Operation;
            this.ucFeeForm1.Location = new System.Drawing.Point(228, 0);
            this.ucFeeForm1.MessageType = FS.HISFC.Models.Base.MessType.Y;
            this.ucFeeForm1.Name = "ucFeeForm1";
            this.ucFeeForm1.Size = new System.Drawing.Size(1084, 714);
            this.ucFeeForm1.TabIndex = 2;
            this.ucFeeForm1.加载项目类别 = FS.HISFC.Components.Common.Controls.EnumShowItemType.Pharmacy;
            this.ucFeeForm1.控件功能 = FS.HISFC.Components.Common.Controls.ucInpatientCharge.FeeTypes.收费;
            // 
            // ucFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucFeeForm1);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucFee";
            this.Size = new System.Drawing.Size(1312, 714);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private ucFeeForm ucFeeForm1;
        private ucRegistrationTree ucRegistrationTree1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.HISFC.Components.Common.Controls.baseTreeView tvGroup;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter2;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
    }
}
