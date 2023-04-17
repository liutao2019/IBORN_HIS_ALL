namespace FS.HISFC.Components.Speciment.Report
{
    partial class ucUserOrder
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dropOrderBy = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dropOrderField = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(267, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(362, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dropOrderBy
            // 
            //this.dropOrderBy.A = false;
            this.dropOrderBy.ArrowBackColor = System.Drawing.Color.Silver;
            this.dropOrderBy.FormattingEnabled = true;
            this.dropOrderBy.IsFlat = true;
            this.dropOrderBy.IsLike = true;
            this.dropOrderBy.Location = new System.Drawing.Point(163, 3);
            this.dropOrderBy.Name = "dropOrderBy";
            this.dropOrderBy.PopForm = null;
            this.dropOrderBy.ShowCustomerList = false;
            this.dropOrderBy.ShowID = false;
            this.dropOrderBy.Size = new System.Drawing.Size(86, 20);
            this.dropOrderBy.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dropOrderBy.TabIndex = 9;
            this.dropOrderBy.Tag = "";
            this.dropOrderBy.ToolBarUse = false;
            // 
            // dropOrderField
            // 
            //this.dropOrderField.A = false;
            this.dropOrderField.ArrowBackColor = System.Drawing.Color.Silver;
            this.dropOrderField.FormattingEnabled = true;
            this.dropOrderField.IsFlat = true;
            this.dropOrderField.IsLike = true;
            this.dropOrderField.Location = new System.Drawing.Point(0, 3);
            this.dropOrderField.Name = "dropOrderField";
            this.dropOrderField.PopForm = null;
            this.dropOrderField.ShowCustomerList = false;
            this.dropOrderField.ShowID = false;
            this.dropOrderField.Size = new System.Drawing.Size(157, 20);
            this.dropOrderField.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dropOrderField.TabIndex = 10;
            this.dropOrderField.Tag = "";
            this.dropOrderField.ToolBarUse = false;
            // 
            // ucUserOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dropOrderField);
            this.Controls.Add(this.dropOrderBy);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Name = "ucUserOrder";
            this.Size = new System.Drawing.Size(437, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private FS.FrameWork.WinForms.Controls.NeuComboBox dropOrderBy;
        private FS.FrameWork.WinForms.Controls.NeuComboBox dropOrderField;
    }
}
