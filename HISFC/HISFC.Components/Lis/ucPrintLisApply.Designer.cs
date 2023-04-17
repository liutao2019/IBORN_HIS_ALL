namespace UFC.Lis
{
    partial class ucPrintLisApply
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
            this.lbEmc = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPrePrint = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbHosName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbListID = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSampleNam = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbExecDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDiagnose = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbItem = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDoc = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // lbEmc
            // 
            this.lbEmc.AutoSize = true;
            this.lbEmc.ForeColor = System.Drawing.Color.Red;
            this.lbEmc.Location = new System.Drawing.Point(12, 8);
            this.lbEmc.Name = "lbEmc";
            this.lbEmc.Size = new System.Drawing.Size(41, 12);
            this.lbEmc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbEmc.TabIndex = 0;
            this.lbEmc.Text = "加  急";
            this.lbEmc.Visible = false;
            // 
            // lbPrePrint
            // 
            this.lbPrePrint.AutoSize = true;
            this.lbPrePrint.ForeColor = System.Drawing.Color.Red;
            this.lbPrePrint.Location = new System.Drawing.Point(12, 34);
            this.lbPrePrint.Name = "lbPrePrint";
            this.lbPrePrint.Size = new System.Drawing.Size(41, 12);
            this.lbPrePrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPrePrint.TabIndex = 0;
            this.lbPrePrint.Text = "补  打";
            this.lbPrePrint.Visible = false;
            // 
            // lbHosName
            // 
            this.lbHosName.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHosName.Location = new System.Drawing.Point(111, 7);
            this.lbHosName.Name = "lbHosName";
            this.lbHosName.Size = new System.Drawing.Size(377, 38);
            this.lbHosName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbHosName.TabIndex = 1;
            this.lbHosName.Text = "检验申请单";
            this.lbHosName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPatientInfo
            // 
            this.lbPatientInfo.AutoSize = true;
            this.lbPatientInfo.Location = new System.Drawing.Point(12, 62);
            this.lbPatientInfo.Name = "lbPatientInfo";
            this.lbPatientInfo.Size = new System.Drawing.Size(359, 12);
            this.lbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPatientInfo.TabIndex = 2;
            this.lbPatientInfo.Text = "姓名：       性别：    年龄：     住院号：       科室：    ";
            // 
            // lbListID
            // 
            this.lbListID.AutoSize = true;
            this.lbListID.Location = new System.Drawing.Point(12, 88);
            this.lbListID.Name = "lbListID";
            this.lbListID.Size = new System.Drawing.Size(65, 12);
            this.lbListID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbListID.TabIndex = 3;
            this.lbListID.Text = "检验单号：";
            // 
            // lbSampleNam
            // 
            this.lbSampleNam.AutoSize = true;
            this.lbSampleNam.Location = new System.Drawing.Point(217, 88);
            this.lbSampleNam.Name = "lbSampleNam";
            this.lbSampleNam.Size = new System.Drawing.Size(65, 12);
            this.lbSampleNam.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSampleNam.TabIndex = 3;
            this.lbSampleNam.Text = "送检物品：";
            // 
            // lbExecDept
            // 
            this.lbExecDept.AutoSize = true;
            this.lbExecDept.Location = new System.Drawing.Point(396, 88);
            this.lbExecDept.Name = "lbExecDept";
            this.lbExecDept.Size = new System.Drawing.Size(65, 12);
            this.lbExecDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbExecDept.TabIndex = 3;
            this.lbExecDept.Text = "执行科室：";
            // 
            // lbDiagnose
            // 
            this.lbDiagnose.AutoSize = true;
            this.lbDiagnose.Location = new System.Drawing.Point(12, 114);
            this.lbDiagnose.Name = "lbDiagnose";
            this.lbDiagnose.Size = new System.Drawing.Size(53, 12);
            this.lbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDiagnose.TabIndex = 3;
            this.lbDiagnose.Text = "诊  断：";
            // 
            // lbItem
            // 
            this.lbItem.AutoSize = true;
            this.lbItem.Location = new System.Drawing.Point(12, 139);
            this.lbItem.Name = "lbItem";
            this.lbItem.Size = new System.Drawing.Size(65, 12);
            this.lbItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbItem.TabIndex = 3;
            this.lbItem.Text = "送检目的：";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Location = new System.Drawing.Point(217, 208);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(65, 12);
            this.lbDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDate.TabIndex = 3;
            this.lbDate.Text = "送检日期：";
            // 
            // lbDoc
            // 
            this.lbDoc.AutoSize = true;
            this.lbDoc.Location = new System.Drawing.Point(12, 208);
            this.lbDoc.Name = "lbDoc";
            this.lbDoc.Size = new System.Drawing.Size(65, 12);
            this.lbDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDoc.TabIndex = 3;
            this.lbDoc.Text = "开立医生：";
            // 
            // ucPrintLisApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbExecDept);
            this.Controls.Add(this.lbSampleNam);
            this.Controls.Add(this.lbDoc);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lbItem);
            this.Controls.Add(this.lbDiagnose);
            this.Controls.Add(this.lbListID);
            this.Controls.Add(this.lbPatientInfo);
            this.Controls.Add(this.lbHosName);
            this.Controls.Add(this.lbPrePrint);
            this.Controls.Add(this.lbEmc);
            this.Name = "ucPrintLisApply";
            this.Size = new System.Drawing.Size(602, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuLabel lbEmc;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbPrePrint;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbHosName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbPatientInfo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbListID;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSampleNam;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbExecDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbDiagnose;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbItem;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbDate;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbDoc;

    }
}
