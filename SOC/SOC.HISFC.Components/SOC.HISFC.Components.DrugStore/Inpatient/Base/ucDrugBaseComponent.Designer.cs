namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Base
{
    partial class ucDrugBaseComponent
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
            this.ngbQuerySet = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.npanelDrugMessage = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSplitter2 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.ngbDrugDetail = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.SuspendLayout();
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 0);
            this.ngbQuerySet.Name = "ngbQuerySet";
            this.ngbQuerySet.Size = new System.Drawing.Size(1067, 65);
            this.ngbQuerySet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQuerySet.TabIndex = 0;
            this.ngbQuerySet.TabStop = false;
            this.ngbQuerySet.Text = "查询设置";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 65);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(1067, 3);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // npanelDrugMessage
            // 
            this.npanelDrugMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.npanelDrugMessage.Location = new System.Drawing.Point(0, 68);
            this.npanelDrugMessage.Name = "npanelDrugMessage";
            this.npanelDrugMessage.Size = new System.Drawing.Size(248, 515);
            this.npanelDrugMessage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npanelDrugMessage.TabIndex = 2;
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Location = new System.Drawing.Point(248, 68);
            this.neuSplitter2.Name = "neuSplitter2";
            this.neuSplitter2.Size = new System.Drawing.Size(3, 515);
            this.neuSplitter2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter2.TabIndex = 3;
            this.neuSplitter2.TabStop = false;
            // 
            // ngbDrugDetail
            // 
            this.ngbDrugDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ngbDrugDetail.Location = new System.Drawing.Point(251, 68);
            this.ngbDrugDetail.Name = "ngbDrugDetail";
            this.ngbDrugDetail.Size = new System.Drawing.Size(816, 515);
            this.ngbDrugDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbDrugDetail.TabIndex = 4;
            this.ngbDrugDetail.TabStop = false;
            this.ngbDrugDetail.Text = "摆药信息";
            // 
            // ucDrugBaseComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ngbDrugDetail);
            this.Controls.Add(this.neuSplitter2);
            this.Controls.Add(this.npanelDrugMessage);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.ngbQuerySet);
            this.Name = "ucDrugBaseComponent";
            this.Size = new System.Drawing.Size(1067, 583);
            this.ResumeLayout(false);

        }

        #endregion

        public FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQuerySet;
        public FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        public FS.FrameWork.WinForms.Controls.NeuPanel npanelDrugMessage;
        public FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter2;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox ngbDrugDetail;
    }
}
