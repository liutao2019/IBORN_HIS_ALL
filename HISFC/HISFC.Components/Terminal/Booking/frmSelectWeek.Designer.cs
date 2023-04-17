namespace FS.HISFC.Components.Terminal.Booking
{
	partial class frmSelectWeek
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
            this.components = new System.ComponentModel.Container ( );
            this.neuComboBox1 = new FS.FrameWork.WinForms.Controls.NeuComboBox ( this.components );
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel ( );
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel ( );
            this.neuButtonCancel = new FS.FrameWork.WinForms.Controls.NeuButton ( );
            this.neuButtonOK = new FS.FrameWork.WinForms.Controls.NeuButton ( );
            this.lblMessage = new FS.FrameWork.WinForms.Controls.NeuLabel ( );
            this.SuspendLayout ( );
            // 
            // neuComboBox1
            // 
            this.neuComboBox1.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.neuComboBox1.FormattingEnabled = true;
            this.neuComboBox1.IsEnter2Tab = false;
            this.neuComboBox1.IsFlat = true;
            this.neuComboBox1.IsLike = true;
            this.neuComboBox1.Items.AddRange ( new object [ ] {
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六",
            "星期日"} );
            this.neuComboBox1.Location = new System.Drawing.Point ( 85 , 70 );
            this.neuComboBox1.Name = "neuComboBox1";
            this.neuComboBox1.PopForm = null;
            this.neuComboBox1.ShowCustomerList = false;
            this.neuComboBox1.ShowID = false;
            this.neuComboBox1.Size = new System.Drawing.Size ( 121 , 20 );
            this.neuComboBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuComboBox1.TabIndex = 0;
            this.neuComboBox1.Tag = "";
            this.neuComboBox1.ToolBarUse = false;
            this.neuComboBox1.SelectedIndexChanged += new System.EventHandler ( this.neuComboBox1_SelectedIndexChanged );
            this.neuComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler ( this.neuComboBox1_KeyDown );
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point ( 18 , 20 );
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size ( 281 , 12 );
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "选择复制星期模板，可以选择与当前星期不同的模板";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point ( 50 , 73 );
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size ( 29 , 12 );
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "选择";
            // 
            // neuButtonCancel
            // 
            this.neuButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuButtonCancel.Location = new System.Drawing.Point ( 189 , 121 );
            this.neuButtonCancel.Name = "neuButtonCancel";
            this.neuButtonCancel.Size = new System.Drawing.Size ( 75 , 23 );
            this.neuButtonCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButtonCancel.TabIndex = 3;
            this.neuButtonCancel.Text = "取消";
            this.neuButtonCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButtonCancel.UseVisualStyleBackColor = true;
            this.neuButtonCancel.Click += new System.EventHandler ( this.neuButtonCancel_Click );
            // 
            // neuButtonOK
            // 
            this.neuButtonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuButtonOK.Location = new System.Drawing.Point ( 45 , 121 );
            this.neuButtonOK.Name = "neuButtonOK";
            this.neuButtonOK.Size = new System.Drawing.Size ( 75 , 23 );
            this.neuButtonOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButtonOK.TabIndex = 4;
            this.neuButtonOK.Text = "确定";
            this.neuButtonOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButtonOK.UseVisualStyleBackColor = true;
            this.neuButtonOK.Click += new System.EventHandler ( this.neuButtonOK_Click );
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblMessage.Location = new System.Drawing.Point ( 223 , 74 );
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size ( 0 , 12 );
            this.lblMessage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMessage.TabIndex = 5;
            // 
            // frmSelectWeek
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 313 , 181 );
            this.Controls.Add ( this.lblMessage );
            this.Controls.Add ( this.neuButtonOK );
            this.Controls.Add ( this.neuButtonCancel );
            this.Controls.Add ( this.neuLabel2 );
            this.Controls.Add ( this.neuLabel1 );
            this.Controls.Add ( this.neuComboBox1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectWeek";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "复制模板信息";
            this.ResumeLayout ( false );
            this.PerformLayout ( );

		}

		#endregion

		private FS.FrameWork.WinForms.Controls.NeuComboBox neuComboBox1;
		private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
		private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
		private FS.FrameWork.WinForms.Controls.NeuButton neuButtonCancel;
		private FS.FrameWork.WinForms.Controls.NeuButton neuButtonOK;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblMessage;
	}
}