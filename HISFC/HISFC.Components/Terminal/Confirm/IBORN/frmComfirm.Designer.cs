namespace FS.HISFC.Components.Terminal.Confirm.IBORN
{
    partial class frmComfirm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblItemName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCardNO = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.lblRecipeDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudComfirmQTY = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbExecOper = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudComfirmQTY)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblItemName);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 41);
            this.panel1.TabIndex = 0;
            // 
            // lblItemName
            // 
            this.lblItemName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.lblItemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemName.Font = new System.Drawing.Font("新宋体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemName.ForeColor = System.Drawing.Color.Red;
            this.lblItemName.Location = new System.Drawing.Point(0, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(474, 40);
            this.lblItemName.TabIndex = 1;
            this.lblItemName.Text = "感官系统训练    * 20次";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(474, 1);
            this.panel2.TabIndex = 1;
            // 
            // lblCardNO
            // 
            this.lblCardNO.AutoSize = true;
            this.lblCardNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNO.Location = new System.Drawing.Point(34, 13);
            this.lblCardNO.Name = "lblCardNO";
            this.lblCardNO.Size = new System.Drawing.Size(67, 20);
            this.lblCardNO.TabIndex = 1;
            this.lblCardNO.Text = "卡    号：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(252, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(67, 20);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "姓    名：";
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoctor.Location = new System.Drawing.Point(34, 42);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(79, 20);
            this.lblDoctor.TabIndex = 3;
            this.lblDoctor.Text = "开单医生：";
            // 
            // lblRecipeDate
            // 
            this.lblRecipeDate.AutoSize = true;
            this.lblRecipeDate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecipeDate.Location = new System.Drawing.Point(252, 42);
            this.lblRecipeDate.Name = "lblRecipeDate";
            this.lblRecipeDate.Size = new System.Drawing.Size(79, 20);
            this.lblRecipeDate.TabIndex = 4;
            this.lblRecipeDate.Text = "申请时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(252, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "配 台 人：";
            // 
            // nudComfirmQTY
            // 
            this.nudComfirmQTY.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.nudComfirmQTY.Location = new System.Drawing.Point(117, 71);
            this.nudComfirmQTY.Name = "nudComfirmQTY";
            this.nudComfirmQTY.Size = new System.Drawing.Size(80, 23);
            this.nudComfirmQTY.TabIndex = 6;
            this.nudComfirmQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudComfirmQTY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(34, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "执行次数：";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(243, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "确定执行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(353, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 30);
            this.button2.TabIndex = 10;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(226)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.cmbExecOper);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.lblDoctor);
            this.panel3.Controls.Add(this.lblCardNO);
            this.panel3.Controls.Add(this.lblName);
            this.panel3.Controls.Add(this.lblRecipeDate);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.nudComfirmQTY);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(474, 129);
            this.panel3.TabIndex = 11;
            // 
            // cmbExecOper
            // 
            this.cmbExecOper.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbExecOper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbExecOper.FormattingEnabled = true;
            this.cmbExecOper.IsEnter2Tab = false;
            this.cmbExecOper.IsFlat = false;
            this.cmbExecOper.IsLike = true;
            this.cmbExecOper.IsListOnly = false;
            this.cmbExecOper.IsPopForm = true;
            this.cmbExecOper.IsShowCustomerList = false;
            this.cmbExecOper.IsShowID = false;
            this.cmbExecOper.IsShowIDAndName = false;
            this.cmbExecOper.Location = new System.Drawing.Point(323, 73);
            this.cmbExecOper.Name = "cmbExecOper";
            this.cmbExecOper.ShowCustomerList = false;
            this.cmbExecOper.ShowID = false;
            this.cmbExecOper.Size = new System.Drawing.Size(101, 20);
            this.cmbExecOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbExecOper.TabIndex = 13;
            this.cmbExecOper.Tag = "";
            this.cmbExecOper.ToolBarUse = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gray;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(474, 1);
            this.label7.TabIndex = 12;
            // 
            // frmComfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(474, 211);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmComfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目执行确认";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudComfirmQTY)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCardNO;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Label lblRecipeDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudComfirmQTY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbExecOper;
    }
}