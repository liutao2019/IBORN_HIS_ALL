namespace FS.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient
{
    partial class ucPatientList
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("123");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("12");
            this.lvPatientList = new FS.FrameWork.WinForms.Controls.NeuListView();
            this.iteminfo = new System.Windows.Forms.ColumnHeader();
            this.patientNO = new System.Windows.Forms.ColumnHeader();
            this.name = new System.Windows.Forms.ColumnHeader();
            this.sex = new System.Windows.Forms.ColumnHeader();
            this.dept = new System.Windows.Forms.ColumnHeader();
            this.ideno = new System.Windows.Forms.ColumnHeader();
            this.address = new System.Windows.Forms.ColumnHeader();
            this.indate = new System.Windows.Forms.ColumnHeader();
            this.instate = new System.Windows.Forms.ColumnHeader();
            this.pact = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lvPatientList
            // 
            this.lvPatientList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.lvPatientList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.iteminfo,
            this.patientNO,
            this.name,
            this.sex,
            this.dept,
            this.ideno,
            this.address,
            this.indate,
            this.instate,
            this.pact});
            this.lvPatientList.FullRowSelect = true;
            this.lvPatientList.GridLines = true;
            this.lvPatientList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPatientList.Location = new System.Drawing.Point(3, 2);
            this.lvPatientList.MultiSelect = false;
            this.lvPatientList.Name = "lvPatientList";
            this.lvPatientList.ShowItemToolTips = true;
            this.lvPatientList.Size = new System.Drawing.Size(639, 472);
            this.lvPatientList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lvPatientList.TabIndex = 1;
            this.lvPatientList.UseCompatibleStateImageBehavior = false;
            this.lvPatientList.View = System.Windows.Forms.View.Details;
            // 
            // iteminfo
            // 
            this.iteminfo.Text = "患者信息";
            this.iteminfo.Width = 150;
            // 
            // patientNO
            // 
            this.patientNO.Text = "住院号";
            this.patientNO.Width = 80;
            // 
            // name
            // 
            this.name.Text = "姓名";
            this.name.Width = 80;
            // 
            // sex
            // 
            this.sex.Text = "性别";
            this.sex.Width = 40;
            // 
            // dept
            // 
            this.dept.Text = "住院科室";
            this.dept.Width = 100;
            // 
            // ideno
            // 
            this.ideno.Text = "身份证号";
            this.ideno.Width = 100;
            // 
            // address
            // 
            this.address.Text = "家庭住址";
            this.address.Width = 200;
            // 
            // indate
            // 
            this.indate.Text = "入院日期";
            this.indate.Width = 150;
            // 
            // instate
            // 
            this.instate.Text = "状态";
            this.instate.Width = 150;
            // 
            // pact
            // 
            this.pact.Text = "合同单位";
            this.pact.Width = 150;
            // 
            // ucPatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.Controls.Add(this.lvPatientList);
            this.Name = "ucPatientList";
            this.Size = new System.Drawing.Size(645, 477);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuListView lvPatientList;
        private System.Windows.Forms.ColumnHeader patientNO;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader sex;
        private System.Windows.Forms.ColumnHeader dept;
        private System.Windows.Forms.ColumnHeader ideno;
        private System.Windows.Forms.ColumnHeader address;
        private System.Windows.Forms.ColumnHeader indate;
        private System.Windows.Forms.ColumnHeader instate;
        private System.Windows.Forms.ColumnHeader iteminfo;
        private System.Windows.Forms.ColumnHeader pact;

    }
}
