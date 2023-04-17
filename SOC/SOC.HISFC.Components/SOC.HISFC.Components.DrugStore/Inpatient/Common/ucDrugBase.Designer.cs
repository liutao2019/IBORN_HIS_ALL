namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    partial class ucDrugBase
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
            this.SuspendLayout();
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 518);
            this.ngbQuerySet.Text = "附加信息";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(0, 0);
            // 
            // npanelDrugMessage
            // 
            this.npanelDrugMessage.Location = new System.Drawing.Point(0, 3);
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Location = new System.Drawing.Point(248, 3);
            // 
            // ngbDrugDetail
            // 
            this.ngbDrugDetail.Location = new System.Drawing.Point(251, 3);
            // 
            // ucDrugBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDrugBase";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
