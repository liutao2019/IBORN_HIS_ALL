namespace GZSI.ApiControls
{
    partial class frmDiagnose
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDiagnose = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbDiagnose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 73);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "诊断信息";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(390, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "取 消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "确 认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "诊断:";
            // 
            // cmbDiagnose
            // 
            this.cmbDiagnose.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDiagnose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDiagnose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDiagnose.FormattingEnabled = true;
            this.cmbDiagnose.IsEnter2Tab = false;
            this.cmbDiagnose.IsFlat = false;
            this.cmbDiagnose.IsLike = true;
            this.cmbDiagnose.IsListOnly = false;
            this.cmbDiagnose.IsPopForm = true;
            this.cmbDiagnose.IsShowCustomerList = false;
            this.cmbDiagnose.IsShowID = false;
            this.cmbDiagnose.IsShowIDAndName = false;
            this.cmbDiagnose.Location = new System.Drawing.Point(101, 26);
            this.cmbDiagnose.Name = "cmbDiagnose";
            this.cmbDiagnose.ShowCustomerList = false;
            this.cmbDiagnose.ShowID = false;
            this.cmbDiagnose.Size = new System.Drawing.Size(201, 24);
            this.cmbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiagnose.TabIndex = 14;
            this.cmbDiagnose.Tag = "";
            this.cmbDiagnose.ToolBarUse = false;
            this.cmbDiagnose.SelectedIndexChanged += new System.EventHandler(this.cmbDiagnose_SelectedIndexChanged);
            this.cmbDiagnose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDiagnose_KeyDown);
            // 
            // frmDiagnose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(226)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(472, 73);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDiagnose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDiagnose";
            this.Load += new System.EventHandler(this.frmDiagnose_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiagnose;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}