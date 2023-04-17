namespace FS.HISFC.Components.Speciment.InStore
{
    partial class ucBatchIn
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
            this.ucBaseControl1 = new FS.FrameWork.WinForms.Controls.ucBaseControl();
            this.grpQueryInfo = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbApplyNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtOr = new System.Windows.Forms.RadioButton();
            this.rbtAnd = new System.Windows.Forms.RadioButton();
            this.chkOut = new System.Windows.Forms.CheckBox();
            this.chkBacked = new System.Windows.Forms.CheckBox();
            this.chkIn = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDisType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbSpecType1 = new System.Windows.Forms.ComboBox();
            this.cmbOrgType1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbReturn = new System.Windows.Forms.CheckBox();
            this.cbNoBack = new System.Windows.Forms.CheckBox();
            this.cbSomeTimes = new System.Windows.Forms.CheckBox();
            this.grpQueryInfo.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucBaseControl1
            // 
            this.ucBaseControl1.AutoScroll = true;
            this.ucBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseControl1.IsPrint = false;
            this.ucBaseControl1.Location = new System.Drawing.Point(0, 0);
            this.ucBaseControl1.Name = "ucBaseControl1";
            this.ucBaseControl1.Size = new System.Drawing.Size(1263, 750);
            this.ucBaseControl1.TabIndex = 1;
            // 
            // grpQueryInfo
            // 
            this.grpQueryInfo.Controls.Add(this.cbSomeTimes);
            this.grpQueryInfo.Controls.Add(this.cbNoBack);
            this.grpQueryInfo.Controls.Add(this.cbReturn);
            this.grpQueryInfo.Controls.Add(this.label5);
            this.grpQueryInfo.Controls.Add(this.tbApplyNum);
            this.grpQueryInfo.Controls.Add(this.label1);
            this.grpQueryInfo.Controls.Add(this.rbtOr);
            this.grpQueryInfo.Controls.Add(this.rbtAnd);
            this.grpQueryInfo.Controls.Add(this.chkOut);
            this.grpQueryInfo.Controls.Add(this.chkBacked);
            this.grpQueryInfo.Controls.Add(this.chkIn);
            this.grpQueryInfo.Controls.Add(this.label4);
            this.grpQueryInfo.Controls.Add(this.label3);
            this.grpQueryInfo.Controls.Add(this.cmbDisType);
            this.grpQueryInfo.Controls.Add(this.cmbSpecType1);
            this.grpQueryInfo.Controls.Add(this.cmbOrgType1);
            this.grpQueryInfo.Controls.Add(this.label2);
            this.grpQueryInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQueryInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpQueryInfo.Location = new System.Drawing.Point(0, 0);
            this.grpQueryInfo.Name = "grpQueryInfo";
            this.grpQueryInfo.Size = new System.Drawing.Size(1263, 100);
            this.grpQueryInfo.TabIndex = 4;
            this.grpQueryInfo.TabStop = false;
            this.grpQueryInfo.Text = "查询条件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(490, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 16);
            this.label5.TabIndex = 52;
            this.label5.Text = "(输入申请单号后按Enter查询)";
            // 
            // tbApplyNum
            // 
            this.tbApplyNum.Location = new System.Drawing.Point(363, 56);
            this.tbApplyNum.Name = "tbApplyNum";
            this.tbApplyNum.Size = new System.Drawing.Size(123, 26);
            this.tbApplyNum.TabIndex = 51;
            this.tbApplyNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbApplyNum_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 50;
            this.label1.Text = "申请单号：";
            // 
            // rbtOr
            // 
            this.rbtOr.AutoSize = true;
            this.rbtOr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtOr.Location = new System.Drawing.Point(1040, 26);
            this.rbtOr.Name = "rbtOr";
            this.rbtOr.Size = new System.Drawing.Size(42, 20);
            this.rbtOr.TabIndex = 49;
            this.rbtOr.Text = "Or";
            this.rbtOr.UseVisualStyleBackColor = true;
            // 
            // rbtAnd
            // 
            this.rbtAnd.AutoSize = true;
            this.rbtAnd.Checked = true;
            this.rbtAnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtAnd.Location = new System.Drawing.Point(984, 26);
            this.rbtAnd.Name = "rbtAnd";
            this.rbtAnd.Size = new System.Drawing.Size(50, 20);
            this.rbtAnd.TabIndex = 48;
            this.rbtAnd.TabStop = true;
            this.rbtAnd.Text = "And";
            this.rbtAnd.UseVisualStyleBackColor = true;
            // 
            // chkOut
            // 
            this.chkOut.AutoSize = true;
            this.chkOut.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOut.Location = new System.Drawing.Point(919, 26);
            this.chkOut.Name = "chkOut";
            this.chkOut.Size = new System.Drawing.Size(59, 20);
            this.chkOut.TabIndex = 47;
            this.chkOut.Text = "借出";
            this.chkOut.UseVisualStyleBackColor = true;
            // 
            // chkBacked
            // 
            this.chkBacked.AutoSize = true;
            this.chkBacked.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkBacked.Location = new System.Drawing.Point(838, 26);
            this.chkBacked.Name = "chkBacked";
            this.chkBacked.Size = new System.Drawing.Size(75, 20);
            this.chkBacked.TabIndex = 47;
            this.chkBacked.Text = "已归还";
            this.chkBacked.UseVisualStyleBackColor = true;
            // 
            // chkIn
            // 
            this.chkIn.AutoSize = true;
            this.chkIn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIn.Location = new System.Drawing.Point(725, 26);
            this.chkIn.Name = "chkIn";
            this.chkIn.Size = new System.Drawing.Size(107, 20);
            this.chkIn.TabIndex = 47;
            this.chkIn.Text = "无借出记录";
            this.chkIn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(626, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 46;
            this.label4.Text = "在库状态：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(247, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 45;
            this.label3.Text = "标本类型：";
            // 
            // cmbDisType
            // 
            //this.cmbDisType.A = false;
            this.cmbDisType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDisType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.IsFlat = true;
            this.cmbDisType.IsLike = true;
            this.cmbDisType.Location = new System.Drawing.Point(113, 24);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.PopForm = null;
            this.cmbDisType.ShowCustomerList = false;
            this.cmbDisType.ShowID = false;
            this.cmbDisType.Size = new System.Drawing.Size(123, 24);
            this.cmbDisType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDisType.TabIndex = 44;
            this.cmbDisType.Tag = "";
            this.cmbDisType.ToolBarUse = false;
            // 
            // cmbSpecType1
            // 
            this.cmbSpecType1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSpecType1.FormattingEnabled = true;
            this.cmbSpecType1.Location = new System.Drawing.Point(476, 24);
            this.cmbSpecType1.Name = "cmbSpecType1";
            this.cmbSpecType1.Size = new System.Drawing.Size(133, 24);
            this.cmbSpecType1.TabIndex = 4;
            // 
            // cmbOrgType1
            // 
            this.cmbOrgType1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOrgType1.FormattingEnabled = true;
            this.cmbOrgType1.Location = new System.Drawing.Point(339, 24);
            this.cmbOrgType1.Name = "cmbOrgType1";
            this.cmbOrgType1.Size = new System.Drawing.Size(113, 24);
            this.cmbOrgType1.TabIndex = 4;
            this.cmbOrgType1.SelectedIndexChanged += new System.EventHandler(this.orgTypeSelectedIndexChanged1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "病种类型：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpQueryInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Size = new System.Drawing.Size(1263, 750);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 5;
            // 
            // cbReturn
            // 
            this.cbReturn.AutoSize = true;
            this.cbReturn.Checked = true;
            this.cbReturn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReturn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbReturn.Location = new System.Drawing.Point(113, 61);
            this.cbReturn.Name = "cbReturn";
            this.cbReturn.Size = new System.Drawing.Size(59, 20);
            this.cbReturn.TabIndex = 54;
            this.cbReturn.Text = "归还";
            this.cbReturn.UseVisualStyleBackColor = true;
            // 
            // cbNoBack
            // 
            this.cbNoBack.AutoSize = true;
            this.cbNoBack.Checked = true;
            this.cbNoBack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNoBack.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbNoBack.Location = new System.Drawing.Point(188, 61);
            this.cbNoBack.Name = "cbNoBack";
            this.cbNoBack.Size = new System.Drawing.Size(75, 20);
            this.cbNoBack.TabIndex = 55;
            this.cbNoBack.Text = "不归还";
            this.cbNoBack.UseVisualStyleBackColor = true;
            // 
            // cbSomeTimes
            // 
            this.cbSomeTimes.AutoSize = true;
            this.cbSomeTimes.Checked = true;
            this.cbSomeTimes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSomeTimes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSomeTimes.Location = new System.Drawing.Point(269, 61);
            this.cbSomeTimes.Name = "cbSomeTimes";
            this.cbSomeTimes.Size = new System.Drawing.Size(91, 20);
            this.cbSomeTimes.TabIndex = 56;
            this.cbSomeTimes.Text = "多次出库";
            this.cbSomeTimes.UseVisualStyleBackColor = true;
            // 
            // ucBatchIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ucBaseControl1);
            this.Name = "ucBatchIn";
            this.Size = new System.Drawing.Size(1263, 750);
            this.Load += new System.EventHandler(this.ucBatchIn_Load);
            this.grpQueryInfo.ResumeLayout(false);
            this.grpQueryInfo.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.ucBaseControl ucBaseControl1;
        private System.Windows.Forms.GroupBox grpQueryInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDisType;
        private System.Windows.Forms.ComboBox cmbSpecType1;
        private System.Windows.Forms.ComboBox cmbOrgType1;
        private System.Windows.Forms.RadioButton rbtOr;
        private System.Windows.Forms.RadioButton rbtAnd;
        private System.Windows.Forms.CheckBox chkOut;
        private System.Windows.Forms.CheckBox chkBacked;
        private System.Windows.Forms.CheckBox chkIn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbApplyNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbReturn;
        private System.Windows.Forms.CheckBox cbNoBack;
        private System.Windows.Forms.CheckBox cbSomeTimes;

    }
}
