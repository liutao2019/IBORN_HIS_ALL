namespace FS.Report.Finance.FinOpb
{
    partial class ucFinOpbExecdeptDrugDetail
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
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTextBox1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTextBox2 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTextBox3 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.plLeft.SuspendLayout();
            this.plRight.SuspendLayout();
            this.plMain.SuspendLayout();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plRightTop.SuspendLayout();
            this.plRightBottom.SuspendLayout();
            this.gbMid.SuspendLayout();
            this.SuspendLayout();
            // 
            // plLeft
            // 
            this.plLeft.Size = new System.Drawing.Size(0, 419);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Size = new System.Drawing.Size(1024, 419);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Size = new System.Drawing.Size(0, 33);
            // 
            // plMain
            // 
            this.plMain.Size = new System.Drawing.Size(1024, 464);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.neuTextBox1);
            this.plTop.Controls.Add(this.neuTextBox3);
            this.plTop.Controls.Add(this.neuLabel5);
            this.plTop.Controls.Add(this.neuTextBox2);
            this.plTop.Controls.Add(this.neuLabel3);
            this.plTop.Controls.Add(this.neuLabel4);
            this.plTop.Size = new System.Drawing.Size(1024, 40);
            this.plTop.Controls.SetChildIndex(this.neuLabel4, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel3, 0);
            this.plTop.Controls.SetChildIndex(this.neuTextBox2, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel5, 0);
            this.plTop.Controls.SetChildIndex(this.neuTextBox3, 0);
            this.plTop.Controls.SetChildIndex(this.neuTextBox1, 0);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            // 
            // plBottom
            // 
            this.plBottom.Size = new System.Drawing.Size(1024, 424);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 5);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Size = new System.Drawing.Size(0, 386);
            // 
            // plRightTop
            // 
            this.plRightTop.Size = new System.Drawing.Size(1024, 416);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 416);
            this.slTop.Size = new System.Drawing.Size(1024, 3);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point(0, 419);
            this.plRightBottom.Size = new System.Drawing.Size(1024, 0);
            // 
            // gbMid
            // 
            this.gbMid.Size = new System.Drawing.Size(1016, 38);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1556, 9);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_opb_execdept_drug_detail";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "Report\\finopb.pbd;Report\\finopb.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(1024, 416);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.OliveDrab;
            this.neuLabel3.Location = new System.Drawing.Point(445, 16);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "科室名称：";
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.IsEnter2Tab = false;
            this.neuTextBox1.Location = new System.Drawing.Point(510, 13);
            this.neuTextBox1.Name = "neuTextBox1";
            this.neuTextBox1.Size = new System.Drawing.Size(100, 21);
            this.neuTextBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox1.TabIndex = 5;
            this.neuTextBox1.TextChanged += new System.EventHandler(this.neuTextBox1_TextChanged);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.OliveDrab;
            this.neuLabel4.Location = new System.Drawing.Point(616, 16);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "项目名称：";
            // 
            // neuTextBox2
            // 
            this.neuTextBox2.IsEnter2Tab = false;
            this.neuTextBox2.Location = new System.Drawing.Point(687, 13);
            this.neuTextBox2.Name = "neuTextBox2";
            this.neuTextBox2.Size = new System.Drawing.Size(100, 21);
            this.neuTextBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox2.TabIndex = 7;
            this.neuTextBox2.TextChanged += new System.EventHandler(this.neuTextBox1_TextChanged);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.OliveDrab;
            this.neuLabel5.Location = new System.Drawing.Point(793, 16);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 8;
            this.neuLabel5.Text = "费用名称：";
            // 
            // neuTextBox3
            // 
            this.neuTextBox3.IsEnter2Tab = false;
            this.neuTextBox3.Location = new System.Drawing.Point(864, 12);
            this.neuTextBox3.Name = "neuTextBox3";
            this.neuTextBox3.Size = new System.Drawing.Size(100, 21);
            this.neuTextBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox3.TabIndex = 9;
            this.neuTextBox3.TextChanged += new System.EventHandler(this.neuTextBox1_TextChanged);
            // 
            // ucFinOpbExecdeptDrugDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.IsLeftVisible = false;
            this.MainDWDataObject = "d_fin_opb_execdept_drug_detail";
            this.MainDWLabrary = "Report\\finopb.pbd;Report\\finopb.pbl";
            this.Name = "ucFinOpbExecdeptDrugDetail";
            this.Size = new System.Drawing.Size(1024, 464);
            this.plLeft.ResumeLayout(false);
            this.plRight.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plRightTop.ResumeLayout(false);
            this.plRightBottom.ResumeLayout(false);
            this.gbMid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
    }
}
