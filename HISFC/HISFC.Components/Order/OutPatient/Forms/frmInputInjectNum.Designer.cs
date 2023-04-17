namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    partial class frmInputInjectNum
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDoseOnce = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtTimes = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.lblInjectDays = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInJectNum = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.btCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtInJectNum)).BeginInit();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(12, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(82, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "院内注射：";
            // 
            // lblDoseOnce
            // 
            this.lblDoseOnce.AutoSize = true;
            this.lblDoseOnce.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoseOnce.ForeColor = System.Drawing.Color.Red;
            this.lblDoseOnce.Location = new System.Drawing.Point(193, 17);
            this.lblDoseOnce.Name = "lblDoseOnce";
            this.lblDoseOnce.Size = new System.Drawing.Size(67, 14);
            this.lblDoseOnce.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoseOnce.TabIndex = 4;
            this.lblDoseOnce.Text = "每次测试";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 11F);
            this.neuLabel3.Location = new System.Drawing.Point(221, 14);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(123, 15);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "分       瓶注射";
            this.neuLabel3.Visible = false;
            // 
            // txtTimes
            // 
            this.txtTimes.IsEnter2Tab = false;
            this.txtTimes.Location = new System.Drawing.Point(237, 10);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.Size = new System.Drawing.Size(36, 21);
            this.txtTimes.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtTimes.TabIndex = 6;
            this.txtTimes.Visible = false;
            this.txtTimes.TextChanged += new System.EventHandler(this.txtTimes_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(51, 85);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确 定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblInjectDays
            // 
            this.lblInjectDays.AutoSize = true;
            this.lblInjectDays.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInjectDays.Location = new System.Drawing.Point(12, 50);
            this.lblInjectDays.Name = "lblInjectDays";
            this.lblInjectDays.Size = new System.Drawing.Size(82, 14);
            this.lblInjectDays.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInjectDays.TabIndex = 3;
            this.lblInjectDays.Text = "院注天数：";
            // 
            // txtInJectNum
            // 
            this.txtInJectNum.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInJectNum.Location = new System.Drawing.Point(90, 12);
            this.txtInJectNum.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txtInJectNum.Name = "txtInJectNum";
            this.txtInJectNum.Size = new System.Drawing.Size(51, 25);
            this.txtInJectNum.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInJectNum.TabIndex = 0;
            this.txtInJectNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancel.Location = new System.Drawing.Point(185, 85);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取 消";
            this.btCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.ForeColor = System.Drawing.Color.Red;
            this.neuLabel2.Location = new System.Drawing.Point(141, 17);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(22, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            this.neuLabel2.Text = "次";
            // 
            // frmInputInjectNum
            // 
            this.ClientSize = new System.Drawing.Size(322, 114);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.lblDoseOnce);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.txtInJectNum);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtTimes);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.lblInjectDays);
            this.Controls.Add(this.neuLabel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInputInjectNum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入院注次数和注射次数";
            this.Load += new System.EventHandler(this.frmInputInjectNum_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtInJectNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDoseOnce;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtTimes;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInjectDays;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown txtInJectNum;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancel;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}
