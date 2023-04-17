namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDrugBase));
            this.ucRecipeDetail1 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.ucRecipeDetail(this.components);
            this.ucDrugTree1 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.ucRecipeTree();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.ngbRecipeDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ucDrugTree1);
            // 
            // ngbRecipeDetail
            // 
            this.ngbRecipeDetail.Controls.Add(this.ucRecipeDetail1);
            this.ngbRecipeDetail.Location = new System.Drawing.Point(0, 72);
            this.ngbRecipeDetail.Size = new System.Drawing.Size(775, 520);
            // 
            // ngbAdd
            // 
            this.ngbAdd.Location = new System.Drawing.Point(0, 592);
            this.ngbAdd.Size = new System.Drawing.Size(775, 50);
            // 
            // ngbPatientInfo
            // 
            this.ngbPatientInfo.Size = new System.Drawing.Size(775, 72);
            // 
            // ucRecipeDetail1
            // 
            this.ucRecipeDetail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucRecipeDetail1.Decimals = 2;
            this.ucRecipeDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRecipeDetail1.EnumQtyShowType = SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.Function.EnumQtyShowType.最小单位;
            this.ucRecipeDetail1.IsPrint = false;
            this.ucRecipeDetail1.Location = new System.Drawing.Point(3, 17);
            this.ucRecipeDetail1.Name = "ucRecipeDetail1";
            this.ucRecipeDetail1.Size = new System.Drawing.Size(769, 500);
            this.ucRecipeDetail1.TabIndex = 0;
            // 
            // ucDrugTree1
            // 
            this.ucDrugTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDrugTree1.DrugRecipe = null;
            this.ucDrugTree1.Location = new System.Drawing.Point(0, 0);
            this.ucDrugTree1.Name = "ucDrugTree1";
            this.ucDrugTree1.Size = new System.Drawing.Size(233, 642);
            this.ucDrugTree1.TabIndex = 0;
            this.ucDrugTree1.TabPage1Name = "未打印/未配药";
            this.ucDrugTree1.TabPage2Name = "已配药/已发药";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // ucDrugManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDrugManager";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ngbRecipeDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected ucRecipeDetail ucRecipeDetail1;
        protected ucRecipeTree ucDrugTree1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}
