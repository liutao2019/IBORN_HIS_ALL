namespace HISFC.Components.Package.Fee.Forms
{
    partial class frmChoosePackageItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDeleteDetail = new System.Windows.Forms.Button();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dgbChildPackageDetail = new System.Windows.Forms.DataGridView();
            this.DetailColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucPackageItemSelect1 = new HISFC.Components.Package.Controls.ucPackageItemSelect();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbChildPackageDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.btnSave);
            this.neuPanel1.Controls.Add(this.btnDeleteDetail);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 338);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1144, 50);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Location = new System.Drawing.Point(1016, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 35);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteDetail.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteDetail.Location = new System.Drawing.Point(900, 8);
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(110, 35);
            this.btnDeleteDetail.TabIndex = 6;
            this.btnDeleteDetail.Text = "删除明细";
            this.btnDeleteDetail.UseVisualStyleBackColor = true;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.dgbChildPackageDetail);
            this.neuPanel2.Controls.Add(this.ucPackageItemSelect1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1144, 338);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // dgbChildPackageDetail
            // 
            this.dgbChildPackageDetail.AllowUserToAddRows = false;
            this.dgbChildPackageDetail.AllowUserToDeleteRows = false;
            this.dgbChildPackageDetail.AllowUserToResizeColumns = false;
            this.dgbChildPackageDetail.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgbChildPackageDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgbChildPackageDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgbChildPackageDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgbChildPackageDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgbChildPackageDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgbChildPackageDetail.ColumnHeadersHeight = 30;
            this.dgbChildPackageDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DetailColumn1,
            this.DetailColumn2,
            this.DetailColumn3,
            this.DetailColumn4,
            this.DetailColumn5,
            this.DetailColumn6,
            this.DetailColumn7,
            this.DetailColumn8,
            this.DetailColumn9,
            this.DetailColumn10,
            this.DetailColumn11,
            this.DetailColumn12,
            this.DetailColumn13});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgbChildPackageDetail.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgbChildPackageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgbChildPackageDetail.EnableHeadersVisualStyles = false;
            this.dgbChildPackageDetail.Location = new System.Drawing.Point(0, 40);
            this.dgbChildPackageDetail.MultiSelect = false;
            this.dgbChildPackageDetail.Name = "dgbChildPackageDetail";
            this.dgbChildPackageDetail.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbChildPackageDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgbChildPackageDetail.RowHeadersVisible = false;
            this.dgbChildPackageDetail.RowTemplate.Height = 30;
            this.dgbChildPackageDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgbChildPackageDetail.Size = new System.Drawing.Size(1144, 298);
            this.dgbChildPackageDetail.TabIndex = 18;
            // 
            // DetailColumn1
            // 
            this.DetailColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DetailColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.DetailColumn1.Frozen = true;
            this.DetailColumn1.HeaderText = "序号";
            this.DetailColumn1.Name = "DetailColumn1";
            this.DetailColumn1.ReadOnly = true;
            this.DetailColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DetailColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn1.Width = 40;
            // 
            // DetailColumn2
            // 
            this.DetailColumn2.HeaderText = "项目编码";
            this.DetailColumn2.Name = "DetailColumn2";
            this.DetailColumn2.ReadOnly = true;
            this.DetailColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn2.Width = 59;
            // 
            // DetailColumn3
            // 
            this.DetailColumn3.HeaderText = "项目名称";
            this.DetailColumn3.Name = "DetailColumn3";
            this.DetailColumn3.ReadOnly = true;
            this.DetailColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn3.Width = 59;
            // 
            // DetailColumn4
            // 
            this.DetailColumn4.HeaderText = "执行科室";
            this.DetailColumn4.Name = "DetailColumn4";
            this.DetailColumn4.ReadOnly = true;
            this.DetailColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn4.Width = 59;
            // 
            // DetailColumn5
            // 
            this.DetailColumn5.HeaderText = "项目单价";
            this.DetailColumn5.Name = "DetailColumn5";
            this.DetailColumn5.ReadOnly = true;
            this.DetailColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn5.Width = 59;
            // 
            // DetailColumn6
            // 
            this.DetailColumn6.HeaderText = "项目数量";
            this.DetailColumn6.Name = "DetailColumn6";
            this.DetailColumn6.ReadOnly = true;
            this.DetailColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn6.Width = 59;
            // 
            // DetailColumn7
            // 
            this.DetailColumn7.HeaderText = "项目单位";
            this.DetailColumn7.Name = "DetailColumn7";
            this.DetailColumn7.ReadOnly = true;
            this.DetailColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn7.Width = 59;
            // 
            // DetailColumn8
            // 
            this.DetailColumn8.HeaderText = "操作人";
            this.DetailColumn8.Name = "DetailColumn8";
            this.DetailColumn8.ReadOnly = true;
            this.DetailColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn8.Width = 47;
            // 
            // DetailColumn9
            // 
            this.DetailColumn9.HeaderText = "操作日期";
            this.DetailColumn9.Name = "DetailColumn9";
            this.DetailColumn9.ReadOnly = true;
            this.DetailColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn9.Width = 59;
            // 
            // DetailColumn10
            // 
            this.DetailColumn10.HeaderText = "创建人";
            this.DetailColumn10.Name = "DetailColumn10";
            this.DetailColumn10.ReadOnly = true;
            this.DetailColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn10.Width = 47;
            // 
            // DetailColumn11
            // 
            this.DetailColumn11.HeaderText = "创建日期";
            this.DetailColumn11.Name = "DetailColumn11";
            this.DetailColumn11.ReadOnly = true;
            this.DetailColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn11.Width = 59;
            // 
            // DetailColumn12
            // 
            this.DetailColumn12.HeaderText = "备注";
            this.DetailColumn12.Name = "DetailColumn12";
            this.DetailColumn12.ReadOnly = true;
            this.DetailColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn12.Width = 35;
            // 
            // DetailColumn13
            // 
            this.DetailColumn13.HeaderText = "费用类别";
            this.DetailColumn13.Name = "DetailColumn13";
            this.DetailColumn13.ReadOnly = true;
            this.DetailColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn13.Width = 59;
            // 
            // ucPackageItemSelect1
            // 
            this.ucPackageItemSelect1.BackColor = System.Drawing.Color.White;
            this.ucPackageItemSelect1.CurrentChildPackage = null;
            this.ucPackageItemSelect1.CurrentDetail = null;
            this.ucPackageItemSelect1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPackageItemSelect1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucPackageItemSelect1.Location = new System.Drawing.Point(0, 0);
            this.ucPackageItemSelect1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucPackageItemSelect1.Name = "ucPackageItemSelect1";
            this.ucPackageItemSelect1.Size = new System.Drawing.Size(1144, 40);
            this.ucPackageItemSelect1.TabIndex = 17;
            // 
            // frmChoosePackageItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 388);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "frmChoosePackageItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择明细";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgbChildPackageDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.DataGridView dgbChildPackageDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn13;
        private HISFC.Components.Package.Controls.ucPackageItemSelect ucPackageItemSelect1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDeleteDetail;

    }
}