namespace FS.HISFC.Components.Speciment.OutStore
{
    partial class ucQueryDiagnose
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDia = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "诊断：";
            // 
            // cmbDia
            // 
            //this.cmbDia.A = false;
            this.cmbDia.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDia.FormattingEnabled = true;
            this.cmbDia.IsFlat = true;
            this.cmbDia.IsLike = true;
            this.cmbDia.Location = new System.Drawing.Point(96, 20);
            this.cmbDia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDia.Name = "cmbDia";
            this.cmbDia.PopForm = null;
            this.cmbDia.ShowCustomerList = false;
            this.cmbDia.ShowID = false;
            this.cmbDia.Size = new System.Drawing.Size(223, 24);
            this.cmbDia.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDia.TabIndex = 1;
            this.cmbDia.Tag = "";
            this.cmbDia.ToolBarUse = false;
            // 
            // ucQueryDiagnose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbDia);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucQueryDiagnose";
            this.Size = new System.Drawing.Size(381, 73);
            this.Load += new System.EventHandler(this.ucQueryDiagnose_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDia;
    }
}
