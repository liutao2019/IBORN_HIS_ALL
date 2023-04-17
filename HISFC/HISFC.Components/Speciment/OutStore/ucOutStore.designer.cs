namespace FS.HISFC.Components.Speciment.OutStore
{
    partial class ucOutStore
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
            this.ucBaseControl1 = new FS.FrameWork.WinForms.Controls.ucBaseControl();
            this.ggrpCondition = new System.Windows.Forms.GroupBox();
            this.rbtSpecIdImport = new System.Windows.Forms.RadioButton();
            this.flpQueryCondition = new System.Windows.Forms.FlowLayoutPanel();
            this.rbtPatient = new System.Windows.Forms.RadioButton();
            this.rbtDiagnose = new System.Windows.Forms.RadioButton();
            this.rbtSpecSource = new System.Windows.Forms.RadioButton();
            this.grpQueryResult = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ggrpCondition.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucBaseControl1
            // 
            this.ucBaseControl1.AutoScroll = true;
            this.ucBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseControl1.IsPrint = false;
            this.ucBaseControl1.Location = new System.Drawing.Point(0, 0);
            this.ucBaseControl1.Name = "ucBaseControl1";
            this.ucBaseControl1.Size = new System.Drawing.Size(1275, 851);
            this.ucBaseControl1.TabIndex = 2;
            // 
            // ggrpCondition
            // 
            this.ggrpCondition.Controls.Add(this.rbtSpecIdImport);
            this.ggrpCondition.Controls.Add(this.flpQueryCondition);
            this.ggrpCondition.Controls.Add(this.rbtPatient);
            this.ggrpCondition.Controls.Add(this.rbtDiagnose);
            this.ggrpCondition.Controls.Add(this.rbtSpecSource);
            this.ggrpCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.ggrpCondition.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ggrpCondition.Location = new System.Drawing.Point(3, 3);
            this.ggrpCondition.Name = "ggrpCondition";
            this.ggrpCondition.Size = new System.Drawing.Size(1100, 130);
            this.ggrpCondition.TabIndex = 3;
            this.ggrpCondition.TabStop = false;
            this.ggrpCondition.Text = "查询条件：";
            // 
            // rbtSpecIdImport
            // 
            this.rbtSpecIdImport.AutoSize = true;
            this.rbtSpecIdImport.Location = new System.Drawing.Point(6, 104);
            this.rbtSpecIdImport.Name = "rbtSpecIdImport";
            this.rbtSpecIdImport.Size = new System.Drawing.Size(106, 20);
            this.rbtSpecIdImport.TabIndex = 4;
            this.rbtSpecIdImport.Text = "标本号导入";
            this.rbtSpecIdImport.UseVisualStyleBackColor = true;
            this.rbtSpecIdImport.CheckedChanged += new System.EventHandler(this.rbt_Changed);
            // 
            // flpQueryCondition
            // 
            this.flpQueryCondition.Location = new System.Drawing.Point(118, 17);
            this.flpQueryCondition.Name = "flpQueryCondition";
            this.flpQueryCondition.Size = new System.Drawing.Size(976, 109);
            this.flpQueryCondition.TabIndex = 3;
            // 
            // rbtPatient
            // 
            this.rbtPatient.AutoSize = true;
            this.rbtPatient.Location = new System.Drawing.Point(6, 77);
            this.rbtPatient.Name = "rbtPatient";
            this.rbtPatient.Size = new System.Drawing.Size(90, 20);
            this.rbtPatient.TabIndex = 2;
            this.rbtPatient.Text = "病人信息";
            this.rbtPatient.UseVisualStyleBackColor = true;
            this.rbtPatient.CheckedChanged += new System.EventHandler(this.rbt_Changed);
            // 
            // rbtDiagnose
            // 
            this.rbtDiagnose.AutoSize = true;
            this.rbtDiagnose.Location = new System.Drawing.Point(6, 49);
            this.rbtDiagnose.Name = "rbtDiagnose";
            this.rbtDiagnose.Size = new System.Drawing.Size(58, 20);
            this.rbtDiagnose.TabIndex = 1;
            this.rbtDiagnose.Text = "诊断";
            this.rbtDiagnose.UseVisualStyleBackColor = true;
            this.rbtDiagnose.CheckedChanged += new System.EventHandler(this.rbt_Changed);
            // 
            // rbtSpecSource
            // 
            this.rbtSpecSource.AutoSize = true;
            this.rbtSpecSource.Checked = true;
            this.rbtSpecSource.Location = new System.Drawing.Point(6, 24);
            this.rbtSpecSource.Name = "rbtSpecSource";
            this.rbtSpecSource.Size = new System.Drawing.Size(74, 20);
            this.rbtSpecSource.TabIndex = 0;
            this.rbtSpecSource.TabStop = true;
            this.rbtSpecSource.Text = "标本源";
            this.rbtSpecSource.UseVisualStyleBackColor = true;
            this.rbtSpecSource.CheckedChanged += new System.EventHandler(this.rbt_Changed);
            // 
            // grpQueryResult
            // 
            this.grpQueryResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpQueryResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpQueryResult.Location = new System.Drawing.Point(3, 139);
            this.grpQueryResult.Name = "grpQueryResult";
            this.grpQueryResult.Size = new System.Drawing.Size(1265, 705);
            this.grpQueryResult.TabIndex = 4;
            this.grpQueryResult.TabStop = false;
            this.grpQueryResult.Text = "查询结果";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.ggrpCondition);
            this.flowLayoutPanel1.Controls.Add(this.grpQueryResult);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1275, 851);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ucOutStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ucBaseControl1);
            this.Name = "ucOutStore";
            this.Size = new System.Drawing.Size(1275, 851);
            this.Load += new System.EventHandler(this.ucOutStore_Load);
            this.ggrpCondition.ResumeLayout(false);
            this.ggrpCondition.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.ucBaseControl ucBaseControl1;
        private System.Windows.Forms.GroupBox ggrpCondition;
        private System.Windows.Forms.RadioButton rbtPatient;
        private System.Windows.Forms.RadioButton rbtDiagnose;
        private System.Windows.Forms.RadioButton rbtSpecSource;
        private System.Windows.Forms.FlowLayoutPanel flpQueryCondition;
        private System.Windows.Forms.GroupBox grpQueryResult;
        private System.Windows.Forms.RadioButton rbtSpecIdImport;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;


    }
}
