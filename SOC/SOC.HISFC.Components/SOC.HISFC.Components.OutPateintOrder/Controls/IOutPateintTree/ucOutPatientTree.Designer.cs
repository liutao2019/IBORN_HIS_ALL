namespace FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree
{
    partial class ucOutPatientTree
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
            this.tvOutOrderPatientList1 = new FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList();
            this.cbxNewReg = new System.Windows.Forms.CheckBox();
            this.txtQuery = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblQuery = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbxQuery = new System.Windows.Forms.GroupBox();
            this.gbxQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvOutOrderPatientList1
            // 
            //{014680EC-6381-408b-98FB-A549DAA49B82}
            //this.tvOutOrderPatientList1.Checked = FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList.enuChecked.None;
            //this.tvOutOrderPatientList1.Direction = FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList.enuShowDirection.Ahead;
            this.tvOutOrderPatientList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOutOrderPatientList1.HideSelection = false;
            //this.tvOutOrderPatientList1.IsShowContextMenu = true;
            //this.tvOutOrderPatientList1.IsShowCount = true;
            //this.tvOutOrderPatientList1.IsShowNewPatient = true;
            //this.tvOutOrderPatientList1.IsShowPatientNo = true;
            this.tvOutOrderPatientList1.Location = new System.Drawing.Point(0, 46);
            this.tvOutOrderPatientList1.Name = "tvOutOrderPatientList1";
            //this.tvOutOrderPatientList1.ShowType = FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList.enuShowType.Bed;
            this.tvOutOrderPatientList1.Size = new System.Drawing.Size(188, 464);
            this.tvOutOrderPatientList1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvOutOrderPatientList1.TabIndex = 0;
            // 
            // cbxNewReg
            // 
            this.cbxNewReg.AutoSize = true;
            this.cbxNewReg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxNewReg.ForeColor = System.Drawing.Color.Red;
            this.cbxNewReg.Location = new System.Drawing.Point(9, 46);
            this.cbxNewReg.Name = "cbxNewReg";
            this.cbxNewReg.Size = new System.Drawing.Size(110, 18);
            this.cbxNewReg.TabIndex = 5;
            this.cbxNewReg.Text = "新的挂号记录";
            this.cbxNewReg.UseVisualStyleBackColor = true;
            // 
            // txtQuery
            // 
            this.txtQuery.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQuery.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtQuery.IsEnter2Tab = false;
            this.txtQuery.Location = new System.Drawing.Point(69, 17);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(103, 23);
            this.txtQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQuery.TabIndex = 3;
            this.txtQuery.Text = "输入卡号或姓名";
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQuery.Location = new System.Drawing.Point(6, 20);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(63, 14);
            this.lblQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblQuery.TabIndex = 4;
            this.lblQuery.Text = "病人号：";
            // 
            // gbxQuery
            // 
            this.gbxQuery.Controls.Add(this.cbxNewReg);
            this.gbxQuery.Controls.Add(this.lblQuery);
            this.gbxQuery.Controls.Add(this.txtQuery);
            this.gbxQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxQuery.Location = new System.Drawing.Point(0, 0);
            this.gbxQuery.Name = "gbxQuery";
            this.gbxQuery.Size = new System.Drawing.Size(188, 46);
            this.gbxQuery.TabIndex = 2;
            this.gbxQuery.TabStop = false;
            // 
            // ucOutPatientTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvOutOrderPatientList1);
            this.Controls.Add(this.gbxQuery);
            this.Name = "ucOutPatientTree";
            this.Size = new System.Drawing.Size(188, 510);
            this.gbxQuery.ResumeLayout(false);
            this.gbxQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private tvOutOrderPatientList tvOutOrderPatientList1;
        private System.Windows.Forms.CheckBox cbxNewReg;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtQuery;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblQuery;
        private System.Windows.Forms.GroupBox gbxQuery;
    }
}
