namespace FS.SOC.HISFC.Components.Pharmacy.Common.Fin
{
    partial class ucFinBill
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
            this.ngbItemBaseInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nlblFinNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lnbCloseFin = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.nlbMemo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbNeedInvoiceNO = new System.Windows.Forms.CheckBox();
            this.ngbItemBaseInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ngbItemBaseInfo
            // 
            this.ngbItemBaseInfo.Controls.Add(this.cbNeedInvoiceNO);
            this.ngbItemBaseInfo.Controls.Add(this.nlblFinNO);
            this.ngbItemBaseInfo.Controls.Add(this.lnbCloseFin);
            this.ngbItemBaseInfo.Controls.Add(this.nlbMemo);
            this.ngbItemBaseInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbItemBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.ngbItemBaseInfo.Name = "ngbItemBaseInfo";
            this.ngbItemBaseInfo.Size = new System.Drawing.Size(759, 55);
            this.ngbItemBaseInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbItemBaseInfo.TabIndex = 10;
            this.ngbItemBaseInfo.TabStop = false;
            this.ngbItemBaseInfo.Text = "记账号信息";
            // 
            // nlblFinNO
            // 
            this.nlblFinNO.AutoSize = true;
            this.nlblFinNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblFinNO.ForeColor = System.Drawing.Color.Blue;
            this.nlblFinNO.Location = new System.Drawing.Point(125, 27);
            this.nlblFinNO.Name = "nlblFinNO";
            this.nlblFinNO.Size = new System.Drawing.Size(12, 12);
            this.nlblFinNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblFinNO.TabIndex = 37;
            this.nlblFinNO.Text = "1";
            // 
            // lnbCloseFin
            // 
            this.lnbCloseFin.AutoSize = true;
            this.lnbCloseFin.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnbCloseFin.Location = new System.Drawing.Point(183, 27);
            this.lnbCloseFin.Name = "lnbCloseFin";
            this.lnbCloseFin.Size = new System.Drawing.Size(65, 12);
            this.lnbCloseFin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lnbCloseFin.TabIndex = 36;
            this.lnbCloseFin.TabStop = true;
            this.lnbCloseFin.Text = "记账号清零";
            // 
            // nlbMemo
            // 
            this.nlbMemo.AutoSize = true;
            this.nlbMemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbMemo.ForeColor = System.Drawing.Color.Blue;
            this.nlbMemo.Location = new System.Drawing.Point(18, 27);
            this.nlbMemo.Name = "nlbMemo";
            this.nlbMemo.Size = new System.Drawing.Size(109, 12);
            this.nlbMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbMemo.TabIndex = 32;
            this.nlbMemo.Text = "目前最大记账号：";
            // 
            // cbNeedInvoiceNO
            // 
            this.cbNeedInvoiceNO.AutoSize = true;
            this.cbNeedInvoiceNO.Checked = true;
            this.cbNeedInvoiceNO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNeedInvoiceNO.Location = new System.Drawing.Point(306, 25);
            this.cbNeedInvoiceNO.Name = "cbNeedInvoiceNO";
            this.cbNeedInvoiceNO.Size = new System.Drawing.Size(144, 16);
            this.cbNeedInvoiceNO.TabIndex = 38;
            this.cbNeedInvoiceNO.Text = "核准前需要录入发票号";
            this.cbNeedInvoiceNO.UseVisualStyleBackColor = true;
            // 
            // ucFinBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ngbItemBaseInfo);
            this.Name = "ucFinBill";
            this.Size = new System.Drawing.Size(759, 60);
            this.ngbItemBaseInfo.ResumeLayout(false);
            this.ngbItemBaseInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox ngbItemBaseInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbMemo;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel lnbCloseFin;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblFinNO;
        private System.Windows.Forms.CheckBox cbNeedInvoiceNO;
    }
}
