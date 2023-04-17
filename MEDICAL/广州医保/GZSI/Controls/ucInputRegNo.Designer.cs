namespace GZSI.Controls
{
    partial class ucInputRegNo
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
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbSex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox2 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbRegNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(33, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 14);
            this.label4.TabIndex = 19;
            this.label4.Text = "单位:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(8, 45);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(318, 156);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.tbSex);
            this.tabPage1.Controls.Add(this.tbName);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(310, 131);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "个人信息";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(171, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "性别:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(33, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "姓名:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "身份证号:";
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOk.Location = new System.Drawing.Point(165, 206);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "确定(&O)";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(249, 207);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "取消(&C)";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(68, 13);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(97, 21);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 20;
            this.tbName.Visible = false;
            // 
            // tbSex
            // 
            this.tbSex.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSex.IsEnter2Tab = false;
            this.tbSex.Location = new System.Drawing.Point(212, 11);
            this.tbSex.Name = "tbSex";
            this.tbSex.Size = new System.Drawing.Size(78, 21);
            this.tbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbSex.TabIndex = 21;
            this.tbSex.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.IsEnter2Tab = false;
            this.textBox1.Location = new System.Drawing.Point(68, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(222, 21);
            this.textBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox1.TabIndex = 22;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox2.IsEnter2Tab = false;
            this.textBox2.Location = new System.Drawing.Point(68, 73);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(222, 21);
            this.textBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox2.TabIndex = 23;
            this.textBox2.Visible = false;
            // 
            // tbRegNo
            // 
            this.tbRegNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRegNo.IsEnter2Tab = false;
            this.tbRegNo.Location = new System.Drawing.Point(128, 7);
            this.tbRegNo.Name = "tbRegNo";
            this.tbRegNo.Size = new System.Drawing.Size(174, 21);
            this.tbRegNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbRegNo.TabIndex = 25;
            this.tbRegNo.Visible = false;
            // 
            // cmbType
            // 
            this.cmbType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbType.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbType.IsEnter2Tab = false;
            this.cmbType.IsFlat = false;
            this.cmbType.IsLike = true;
            this.cmbType.IsListOnly = false;
            this.cmbType.IsPopForm = true;
            this.cmbType.IsShowCustomerList = false;
            this.cmbType.IsShowID = false;
            this.cmbType.Location = new System.Drawing.Point(8, 7);
            this.cmbType.Name = "cmbType";
            this.cmbType.PopForm = null;
            this.cmbType.ShowCustomerList = false;
            this.cmbType.ShowID = false;
            this.cmbType.Size = new System.Drawing.Size(114, 24);
            this.cmbType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbType.TabIndex = 26;
            this.cmbType.Tag = "";
            this.cmbType.ToolBarUse = false;
            // 
            // ucInputRegNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.tbRegNo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "ucInputRegNo";
            this.Size = new System.Drawing.Size(343, 241);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbSex;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbRegNo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbType;

    }
}
