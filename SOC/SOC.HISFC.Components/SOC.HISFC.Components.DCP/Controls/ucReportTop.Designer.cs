namespace FS.SOC.HISFC.Components.DCP.Controls
{
    partial class ucReportTop
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
            this.gbReportTop = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lbPID = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labelClinic = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.lbState = new System.Windows.Forms.Label();
            this.lb102 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.gbReportTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbReportTop
            // 
            this.gbReportTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbReportTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbReportTop.Controls.Add(this.lbPID);
            this.gbReportTop.Controls.Add(this.labelClinic);
            this.gbReportTop.Controls.Add(this.lbID);
            this.gbReportTop.Controls.Add(this.label104);
            this.gbReportTop.Controls.Add(this.lbState);
            this.gbReportTop.Controls.Add(this.lb102);
            this.gbReportTop.Controls.Add(this.label69);
            this.gbReportTop.Font = new System.Drawing.Font("宋体", 1F);
            this.gbReportTop.Location = new System.Drawing.Point(3, 3);
            this.gbReportTop.Name = "gbReportTop";
            this.gbReportTop.Size = new System.Drawing.Size(747, 71);
            this.gbReportTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbReportTop.TabIndex = 100;
            this.gbReportTop.TabStop = false;
            // 
            // lbPID
            // 
            this.lbPID.AutoSize = true;
            this.lbPID.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lbPID.Location = new System.Drawing.Point(576, 51);
            this.lbPID.Name = "lbPID";
            this.lbPID.Size = new System.Drawing.Size(0, 14);
            this.lbPID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPID.TabIndex = 107;
            // 
            // labelClinic
            // 
            this.labelClinic.AutoSize = true;
            this.labelClinic.Font = new System.Drawing.Font("宋体", 9F);
            this.labelClinic.Location = new System.Drawing.Point(505, 51);
            this.labelClinic.Name = "labelClinic";
            this.labelClinic.Size = new System.Drawing.Size(65, 12);
            this.labelClinic.TabIndex = 106;
            this.labelClinic.Text = "门诊卡号：";
            this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lbID.Location = new System.Drawing.Point(346, 51);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(0, 14);
            this.lbID.TabIndex = 105;
            this.lbID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("宋体", 9F);
            this.label104.Location = new System.Drawing.Point(275, 51);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(65, 12);
            this.label104.TabIndex = 104;
            this.label104.Text = "卡片编号：";
            this.label104.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.lbState.ForeColor = System.Drawing.Color.Red;
            this.lbState.Location = new System.Drawing.Point(91, 51);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(0, 14);
            this.lbState.TabIndex = 103;
            this.lbState.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lb102
            // 
            this.lb102.AutoSize = true;
            this.lb102.BackColor = System.Drawing.Color.Transparent;
            this.lb102.Font = new System.Drawing.Font("宋体", 9F);
            this.lb102.Location = new System.Drawing.Point(26, 51);
            this.lb102.Name = "lb102";
            this.lb102.Size = new System.Drawing.Size(65, 12);
            this.lb102.TabIndex = 102;
            this.lb102.Text = "报卡类型：";
            this.lb102.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label69.Location = new System.Drawing.Point(179, 9);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(282, 20);
            this.label69.TabIndex = 101;
            this.label69.Text = "中华人民共和国传染病报告卡";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucReportTop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.Controls.Add(this.gbReportTop);
            this.Name = "ucReportTop";
            this.Size = new System.Drawing.Size(753, 77);
            this.gbReportTop.ResumeLayout(false);
            this.gbReportTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbReportTop;
        private System.Windows.Forms.Label labelClinic;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Label lb102;
        private System.Windows.Forms.Label label69;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPID;
    }
}
