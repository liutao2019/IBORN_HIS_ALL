namespace FS.SOC.Local.Pharmacy.ShenZhen.Report
{
    partial class ucDeptMonthQuery
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucPIMonstoreOutQuery1 = new FS.SOC.Local.Pharmacy.ShenZhen.Report.ucPIMonstoreOutQuery();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucPIMonstoreINQuery1 = new FS.SOC.Local.Pharmacy.ShenZhen.Report.ucPIMonstoreINQuery();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucMonstoreCheckStatic1 = new FS.SOC.Local.Pharmacy.ShenZhen.Report.ucMonstoreCheckStatic();
            this.ucMonstoreAdjustStatic1 = new FS.SOC.Local.Pharmacy.ShenZhen.Report.ucMonstoreAdjustStatic();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(641, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "出库统计";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucPIMonstoreOutQuery1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 407);
            this.panel1.TabIndex = 0;
            // 
            // ucPIMonstoreOutQuery1
            // 
            this.ucPIMonstoreOutQuery1.DataView = null;
            this.ucPIMonstoreOutQuery1.DetailDataView = null;
            this.ucPIMonstoreOutQuery1.DetailFilters = null;
            this.ucPIMonstoreOutQuery1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPIMonstoreOutQuery1.Filters = null;
            this.ucPIMonstoreOutQuery1.IsAutoPaper = true;
            this.ucPIMonstoreOutQuery1.IsPrint = false;
            this.ucPIMonstoreOutQuery1.Location = new System.Drawing.Point(0, 0);
            this.ucPIMonstoreOutQuery1.Name = "ucPIMonstoreOutQuery1";
            this.ucPIMonstoreOutQuery1.Size = new System.Drawing.Size(635, 407);
            this.ucPIMonstoreOutQuery1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucPIMonstoreINQuery1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(641, 413);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "入库统计";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucPIMonstoreINQuery1
            // 
            this.ucPIMonstoreINQuery1.DataView = null;
            this.ucPIMonstoreINQuery1.DetailDataView = null;
            this.ucPIMonstoreINQuery1.DetailFilters = null;
            this.ucPIMonstoreINQuery1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPIMonstoreINQuery1.Filters = null;
            this.ucPIMonstoreINQuery1.IsAutoPaper = true;
            this.ucPIMonstoreINQuery1.IsPrint = false;
            this.ucPIMonstoreINQuery1.Location = new System.Drawing.Point(3, 3);
            this.ucPIMonstoreINQuery1.Name = "ucPIMonstoreINQuery1";
            this.ucPIMonstoreINQuery1.Size = new System.Drawing.Size(635, 407);
            this.ucPIMonstoreINQuery1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(649, 438);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucMonstoreAdjustStatic1);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(641, 413);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "调价统计";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucMonstoreCheckStatic1);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(641, 413);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "盘点统计";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ucMonstoreCheckStatic1
            // 
            this.ucMonstoreCheckStatic1.DataView = null;
            this.ucMonstoreCheckStatic1.DetailDataView = null;
            this.ucMonstoreCheckStatic1.DetailFilters = null;
            this.ucMonstoreCheckStatic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMonstoreCheckStatic1.Filters = null;
            this.ucMonstoreCheckStatic1.IsAutoPaper = true;
            this.ucMonstoreCheckStatic1.IsPrint = false;
            this.ucMonstoreCheckStatic1.Location = new System.Drawing.Point(0, 0);
            this.ucMonstoreCheckStatic1.Name = "ucMonstoreCheckStatic1";
            this.ucMonstoreCheckStatic1.Size = new System.Drawing.Size(641, 413);
            this.ucMonstoreCheckStatic1.TabIndex = 0;
            // 
            // ucMonstoreAdjustStatic1
            // 
            this.ucMonstoreAdjustStatic1.DataView = null;
            this.ucMonstoreAdjustStatic1.DetailDataView = null;
            this.ucMonstoreAdjustStatic1.DetailFilters = null;
            this.ucMonstoreAdjustStatic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMonstoreAdjustStatic1.Filters = null;
            this.ucMonstoreAdjustStatic1.IsAutoPaper = true;
            this.ucMonstoreAdjustStatic1.IsPrint = false;
            this.ucMonstoreAdjustStatic1.Location = new System.Drawing.Point(0, 0);
            this.ucMonstoreAdjustStatic1.Name = "ucMonstoreAdjustStatic1";
            this.ucMonstoreAdjustStatic1.Size = new System.Drawing.Size(641, 413);
            this.ucMonstoreAdjustStatic1.TabIndex = 0;
            // 
            // ucDeptMonthQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucDeptMonthQuery";
            this.Size = new System.Drawing.Size(649, 438);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private ucPIMonstoreOutQuery ucPIMonstoreOutQuery1;
        private System.Windows.Forms.TabPage tabPage1;
        private ucPIMonstoreINQuery ucPIMonstoreINQuery1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private ucMonstoreCheckStatic ucMonstoreCheckStatic1;
        private ucMonstoreAdjustStatic ucMonstoreAdjustStatic1;
    }
}
