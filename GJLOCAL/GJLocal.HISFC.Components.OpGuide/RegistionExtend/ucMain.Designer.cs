namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    partial class ucMain
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnCTitle = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lbTypeShow = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbHoldPage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCurrentPage = new System.Windows.Forms.Label();
            this.btNextPage = new System.Windows.Forms.Button();
            this.btLastPage = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.cbN = new System.Windows.Forms.CheckBox();
            this.cbG = new System.Windows.Forms.CheckBox();
            this.cbD = new System.Windows.Forms.CheckBox();
            this.lbID = new System.Windows.Forms.Label();
            this.lbDateTimeReg = new System.Windows.Forms.Label();
            this.ucConsultation1 = new GJLocal.HISFC.Components.OpGuide.RegistionExtend.ucConsultation();
            this.ucNerve11 = new GJLocal.HISFC.Components.OpGuide.RegistionExtend.ucNerve1();
            this.ucGeneral11 = new GJLocal.HISFC.Components.OpGuide.RegistionExtend.ucGeneral1();
            this.ucDentist11 = new GJLocal.HISFC.Components.OpGuide.RegistionExtend.ucDentist1();
            this.ucBackPage1 = new GJLocal.HISFC.Components.OpGuide.RegistionExtend.ucBackPage();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnCTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.ucBackPage1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbHoldPage);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.lbCurrentPage);
            this.splitContainer1.Panel2.Controls.Add(this.btNextPage);
            this.splitContainer1.Panel2.Controls.Add(this.btLastPage);
            this.splitContainer1.Panel2.Controls.Add(this.btCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btSave);
            this.splitContainer1.Panel2.Controls.Add(this.cbN);
            this.splitContainer1.Panel2.Controls.Add(this.cbG);
            this.splitContainer1.Panel2.Controls.Add(this.cbD);
            this.splitContainer1.Size = new System.Drawing.Size(927, 1037);
            this.splitContainer1.SplitterDistance = 970;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(29, 62);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ucConsultation1);
            this.splitContainer2.Panel1.Controls.Add(this.pnCTitle);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucNerve11);
            this.splitContainer2.Panel2.Controls.Add(this.ucGeneral11);
            this.splitContainer2.Panel2.Controls.Add(this.ucDentist11);
            this.splitContainer2.Size = new System.Drawing.Size(840, 976);
            this.splitContainer2.SplitterDistance = 382;
            this.splitContainer2.TabIndex = 1;
            // 
            // pnCTitle
            // 
            this.pnCTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnCTitle.Controls.Add(this.lbDateTimeReg);
            this.pnCTitle.Controls.Add(this.lbID);
            this.pnCTitle.Controls.Add(this.label6);
            this.pnCTitle.Controls.Add(this.lbTypeShow);
            this.pnCTitle.Controls.Add(this.label5);
            this.pnCTitle.Controls.Add(this.label4);
            this.pnCTitle.Controls.Add(this.label3);
            this.pnCTitle.Controls.Add(this.label2);
            this.pnCTitle.Controls.Add(this.lbTitle);
            this.pnCTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnCTitle.Location = new System.Drawing.Point(0, 0);
            this.pnCTitle.Name = "pnCTitle";
            this.pnCTitle.Size = new System.Drawing.Size(838, 93);
            this.pnCTitle.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(464, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = " For Office Use Only";
            // 
            // lbTypeShow
            // 
            this.lbTypeShow.AutoSize = true;
            this.lbTypeShow.Location = new System.Drawing.Point(125, 29);
            this.lbTypeShow.Name = "lbTypeShow";
            this.lbTypeShow.Size = new System.Drawing.Size(11, 12);
            this.lbTypeShow.TabIndex = 5;
            this.lbTypeShow.Text = "G";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "患者保密健康記錄";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Confidential Patient Health Record";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "請書寫工整";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(18, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "PLEASE PRINT";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(217, 51);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(238, 16);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "PERSONAL HISTORY  個人資料";
            // 
            // lbHoldPage
            // 
            this.lbHoldPage.AutoSize = true;
            this.lbHoldPage.Location = new System.Drawing.Point(515, 16);
            this.lbHoldPage.Name = "lbHoldPage";
            this.lbHoldPage.Size = new System.Drawing.Size(11, 12);
            this.lbHoldPage.TabIndex = 9;
            this.lbHoldPage.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(497, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "/";
            // 
            // lbCurrentPage
            // 
            this.lbCurrentPage.AutoSize = true;
            this.lbCurrentPage.Location = new System.Drawing.Point(479, 16);
            this.lbCurrentPage.Name = "lbCurrentPage";
            this.lbCurrentPage.Size = new System.Drawing.Size(11, 12);
            this.lbCurrentPage.TabIndex = 7;
            this.lbCurrentPage.Text = "1";
            // 
            // btNextPage
            // 
            this.btNextPage.Location = new System.Drawing.Point(552, 11);
            this.btNextPage.Name = "btNextPage";
            this.btNextPage.Size = new System.Drawing.Size(75, 23);
            this.btNextPage.TabIndex = 6;
            this.btNextPage.Text = "NextPage";
            this.btNextPage.UseVisualStyleBackColor = true;
            this.btNextPage.Click += new System.EventHandler(this.btNextPage_Click);
            // 
            // btLastPage
            // 
            this.btLastPage.Location = new System.Drawing.Point(381, 11);
            this.btLastPage.Name = "btLastPage";
            this.btLastPage.Size = new System.Drawing.Size(75, 23);
            this.btLastPage.TabIndex = 5;
            this.btLastPage.Text = "LastPage";
            this.btLastPage.UseVisualStyleBackColor = true;
            this.btLastPage.Click += new System.EventHandler(this.btLastPage_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(638, 11);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(286, 11);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 3;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cbN
            // 
            this.cbN.AutoSize = true;
            this.cbN.Location = new System.Drawing.Point(129, 13);
            this.cbN.Name = "cbN";
            this.cbN.Size = new System.Drawing.Size(30, 16);
            this.cbN.TabIndex = 2;
            this.cbN.Text = "N";
            this.cbN.UseVisualStyleBackColor = true;
            this.cbN.CheckedChanged += new System.EventHandler(this.cbN_CheckedChanged);
            // 
            // cbG
            // 
            this.cbG.AutoSize = true;
            this.cbG.Location = new System.Drawing.Point(79, 13);
            this.cbG.Name = "cbG";
            this.cbG.Size = new System.Drawing.Size(30, 16);
            this.cbG.TabIndex = 1;
            this.cbG.Text = "G";
            this.cbG.UseVisualStyleBackColor = true;
            this.cbG.CheckedChanged += new System.EventHandler(this.cbG_CheckedChanged);
            // 
            // cbD
            // 
            this.cbD.AutoSize = true;
            this.cbD.Location = new System.Drawing.Point(29, 13);
            this.cbD.Name = "cbD";
            this.cbD.Size = new System.Drawing.Size(30, 16);
            this.cbD.TabIndex = 0;
            this.cbD.Text = "D";
            this.cbD.UseVisualStyleBackColor = true;
            this.cbD.CheckedChanged += new System.EventHandler(this.cbD_CheckedChanged);
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(689, 13);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(23, 12);
            this.lbID.TabIndex = 8;
            this.lbID.Text = "ID:";
            // 
            // lbDateTimeReg
            // 
            this.lbDateTimeReg.AutoSize = true;
            this.lbDateTimeReg.Location = new System.Drawing.Point(591, 13);
            this.lbDateTimeReg.Name = "lbDateTimeReg";
            this.lbDateTimeReg.Size = new System.Drawing.Size(41, 12);
            this.lbDateTimeReg.TabIndex = 9;
            this.lbDateTimeReg.Text = "日期：";
            // 
            // ucConsultation1
            // 
            this.ucConsultation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucConsultation1.Clinc_code = "";
            this.ucConsultation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucConsultation1.IsFullConvertToHalf = true;
            this.ucConsultation1.IsPrint = false;
            this.ucConsultation1.Location = new System.Drawing.Point(0, 93);
            this.ucConsultation1.MinimumSize = new System.Drawing.Size(651, 329);
            this.ucConsultation1.Name = "ucConsultation1";
            this.ucConsultation1.ParentFormToolBar = null;
            this.ucConsultation1.Size = new System.Drawing.Size(838, 329);
            this.ucConsultation1.TabIndex = 0;
            // 
            // ucNerve11
            // 
            this.ucNerve11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucNerve11.Clinic_code = "";
            this.ucNerve11.IsFullConvertToHalf = true;
            this.ucNerve11.IsPrint = false;
            this.ucNerve11.Location = new System.Drawing.Point(13, 3);
            this.ucNerve11.MinimumSize = new System.Drawing.Size(756, 590);
            this.ucNerve11.Name = "ucNerve11";
            this.ucNerve11.ParentFormToolBar = null;
            this.ucNerve11.Size = new System.Drawing.Size(756, 590);
            this.ucNerve11.TabIndex = 2;
            // 
            // ucGeneral11
            // 
            this.ucGeneral11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucGeneral11.Clinic_code = "";
            this.ucGeneral11.IsFullConvertToHalf = true;
            this.ucGeneral11.IsPrint = false;
            this.ucGeneral11.Location = new System.Drawing.Point(13, 9);
            this.ucGeneral11.MinimumSize = new System.Drawing.Size(756, 632);
            this.ucGeneral11.Name = "ucGeneral11";
            this.ucGeneral11.ParentFormToolBar = null;
            this.ucGeneral11.Size = new System.Drawing.Size(756, 632);
            this.ucGeneral11.TabIndex = 1;
            // 
            // ucDentist11
            // 
            this.ucDentist11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucDentist11.Clinic_code = "";
            this.ucDentist11.IsFullConvertToHalf = true;
            this.ucDentist11.IsPrint = false;
            this.ucDentist11.Location = new System.Drawing.Point(3, 3);
            this.ucDentist11.MinimumSize = new System.Drawing.Size(756, 610);
            this.ucDentist11.Name = "ucDentist11";
            this.ucDentist11.ParentFormToolBar = null;
            this.ucDentist11.Size = new System.Drawing.Size(794, 618);
            this.ucDentist11.TabIndex = 0;
            // 
            // ucBackPage1
            // 
            this.ucBackPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucBackPage1.Clinic_code = "";
            this.ucBackPage1.IsFullConvertToHalf = true;
            this.ucBackPage1.IsPrint = false;
            this.ucBackPage1.Location = new System.Drawing.Point(110, -1);
            this.ucBackPage1.Name = "ucBackPage1";
            this.ucBackPage1.ParentFormToolBar = null;
            this.ucBackPage1.Size = new System.Drawing.Size(645, 427);
            this.ucBackPage1.TabIndex = 0;
            // 
            // ucMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucMain";
            this.Size = new System.Drawing.Size(927, 1037);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.pnCTitle.ResumeLayout(false);
            this.pnCTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btNextPage;
        private System.Windows.Forms.Button btLastPage;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.CheckBox cbN;
        private System.Windows.Forms.CheckBox cbG;
        private System.Windows.Forms.CheckBox cbD;
        private System.Windows.Forms.Label lbHoldPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCurrentPage;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ucConsultation ucConsultation1;
        private ucBackPage ucBackPage1;
        private ucDentist1 ucDentist11;
        private ucNerve1 ucNerve11;
        private ucGeneral1 ucGeneral11;
        private System.Windows.Forms.Panel pnCTitle;
        private System.Windows.Forms.Label lbTypeShow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label lbDateTimeReg;
    }
}
