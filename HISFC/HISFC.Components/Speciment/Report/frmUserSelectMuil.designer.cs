namespace FS.HISFC.Components.Speciment.Report
{
    partial class frmUserSelectMuil
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FS.HISFC.Components.Speciment.Report.ucUserSelect.selectObject selectObject2 = new FS.HISFC.Components.Speciment.Report.ucUserSelect.selectObject();
            this.bntSelect = new System.Windows.Forms.Button();
            this.bntReturn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ucUserSelectControl = new FS.HISFC.Components.Speciment.Report.ucUserSelect();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bntSelect
            // 
            this.bntSelect.Location = new System.Drawing.Point(553, 46);
            this.bntSelect.Name = "bntSelect";
            this.bntSelect.Size = new System.Drawing.Size(75, 23);
            this.bntSelect.TabIndex = 1;
            this.bntSelect.Text = "查找";
            this.bntSelect.UseVisualStyleBackColor = true;
            this.bntSelect.Click += new System.EventHandler(this.bntSelect_Click);
            // 
            // bntReturn
            // 
            this.bntReturn.Location = new System.Drawing.Point(553, 87);
            this.bntReturn.Name = "bntReturn";
            this.bntReturn.Size = new System.Drawing.Size(75, 23);
            this.bntReturn.TabIndex = 2;
            this.bntReturn.Text = "返回";
            this.bntReturn.UseVisualStyleBackColor = true;
            this.bntReturn.Click += new System.EventHandler(this.bntReturn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ucUserSelectControl);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(534, 256);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // ucUserSelectControl
            // 
            this.ucUserSelectControl.ArrAlLj = null;
            this.ucUserSelectControl.ArrListItem = null;
            this.ucUserSelectControl.Location = new System.Drawing.Point(3, 3);
            this.ucUserSelectControl.Name = "ucUserSelectControl";
            this.ucUserSelectControl.SelectObjectTemp = selectObject2;
            this.ucUserSelectControl.Size = new System.Drawing.Size(510, 45);
            this.ucUserSelectControl.TabIndex = 0;
            this.ucUserSelectControl.TableInfo = null;
            // 
            // frmUserSelectMuil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 281);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.bntReturn);
            this.Controls.Add(this.bntSelect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserSelectMuil";
            this.Text = "多列复合条件过滤";
            this.Load += new System.EventHandler(this.frmUserSelectMuil_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntSelect;
        private System.Windows.Forms.Button bntReturn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ucUserSelect ucUserSelectControl;
    }
}