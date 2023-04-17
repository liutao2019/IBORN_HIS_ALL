namespace FS.HISFC.Components.Speciment.Report
{
    partial class ucUserSelect
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
            this.dropSelectItem = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dropSelectBy = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbLj1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.comAndOr = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            //this.ledService1 = new FS.UFC.DrugStore.WebReference.LEDService();
            this.SuspendLayout();
            // 
            // dropSelectItem
            // 
            //this.dropSelectItem.A = false;
            this.dropSelectItem.ArrowBackColor = System.Drawing.Color.Silver;
            this.dropSelectItem.FormattingEnabled = true;
            this.dropSelectItem.IsFlat = true;
            this.dropSelectItem.IsLike = true;
            this.dropSelectItem.Location = new System.Drawing.Point(55, 8);
            this.dropSelectItem.Name = "dropSelectItem";
            this.dropSelectItem.PopForm = null;
            this.dropSelectItem.ShowCustomerList = false;
            this.dropSelectItem.ShowID = false;
            this.dropSelectItem.Size = new System.Drawing.Size(127, 20);
            this.dropSelectItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dropSelectItem.TabIndex = 14;
            this.dropSelectItem.Tag = "";
            this.dropSelectItem.ToolBarUse = false;
            this.dropSelectItem.SelectedIndexChanged += new System.EventHandler(this.dropSelectItem_SelectedIndexChanged);
            // 
            // dropSelectBy
            // 
            //this.dropSelectBy.A = false;
            this.dropSelectBy.ArrowBackColor = System.Drawing.Color.Silver;
            this.dropSelectBy.FormattingEnabled = true;
            this.dropSelectBy.IsFlat = true;
            this.dropSelectBy.IsLike = true;
            this.dropSelectBy.Location = new System.Drawing.Point(289, 8);
            this.dropSelectBy.Name = "dropSelectBy";
            this.dropSelectBy.PopForm = null;
            this.dropSelectBy.ShowCustomerList = false;
            this.dropSelectBy.ShowID = false;
            this.dropSelectBy.Size = new System.Drawing.Size(137, 20);
            this.dropSelectBy.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dropSelectBy.TabIndex = 13;
            this.dropSelectBy.Tag = "";
            this.dropSelectBy.ToolBarUse = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(432, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(433, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "添加";
            // 
            // cmbLj1
            // 
            //this.cmbLj1.A = false;
            this.cmbLj1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLj1.FormattingEnabled = true;
            this.cmbLj1.IsFlat = true;
            this.cmbLj1.IsLike = true;
            this.cmbLj1.Location = new System.Drawing.Point(188, 8);
            this.cmbLj1.Name = "cmbLj1";
            this.cmbLj1.PopForm = null;
            this.cmbLj1.ShowCustomerList = false;
            this.cmbLj1.ShowID = false;
            this.cmbLj1.Size = new System.Drawing.Size(95, 20);
            this.cmbLj1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbLj1.TabIndex = 15;
            this.cmbLj1.Tag = "";
            this.cmbLj1.ToolBarUse = false;
            // 
            // comAndOr
            // 
            //this.comAndOr.A = false;
            this.comAndOr.ArrowBackColor = System.Drawing.Color.Silver;
            this.comAndOr.FormattingEnabled = true;
            this.comAndOr.IsFlat = true;
            this.comAndOr.IsLike = true;
            this.comAndOr.Location = new System.Drawing.Point(3, 8);
            this.comAndOr.Name = "comAndOr";
            this.comAndOr.PopForm = null;
            this.comAndOr.ShowCustomerList = false;
            this.comAndOr.ShowID = false;
            this.comAndOr.Size = new System.Drawing.Size(45, 20);
            this.comAndOr.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.comAndOr.TabIndex = 18;
            this.comAndOr.Tag = "";
            this.comAndOr.ToolBarUse = false;
            // 
            // ledService1
            // 
            //this.ledService1.Credentials = null;
            //this.ledService1.Url = "http://172.16.105.45/LEDService.asmx";
            //this.ledService1.UseDefaultCredentials = false;
            // 
            // ucUserSelect
            // 
            this.Controls.Add(this.comAndOr);
            this.Controls.Add(this.cmbLj1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dropSelectItem);
            this.Controls.Add(this.dropSelectBy);
            this.Name = "ucUserSelect";
            this.Size = new System.Drawing.Size(510, 38);
            this.Load += new System.EventHandler(this.ucUserSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox dropSelectItem;
        private FS.FrameWork.WinForms.Controls.NeuComboBox dropSelectBy;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbLj1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox comAndOr;
        //private FS.HISFC.Components.DrugStore.WebReference.LEDService ledService1;
    }
}
