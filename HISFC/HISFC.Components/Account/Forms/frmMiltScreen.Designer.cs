namespace FS.HISFC.Components.Account.Forms
{
    partial class frmMiltScreen
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

            if (disposing)
            {
                if (this.dsItem != null)
                {
                    this.dsItem.Clear();
                    this.dsItem.Dispose();
                    this.dsItem = null;

                    System.GC.Collect();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMiltScreen));
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbDrugCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbTotCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbReturnCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbRealOwnCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPaientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDrugCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPayTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPayCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel3
            // 
            this.neuPanel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("neuPanel3.BackgroundImage")));
            this.neuPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.neuPanel3.Controls.Add(this.neuLabel9);
            this.neuPanel3.Controls.Add(this.neuLabel10);
            this.neuPanel3.Controls.Add(this.neuPanel4);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(1024, 768);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel10.ForeColor = System.Drawing.Color.White;
            this.neuLabel10.Location = new System.Drawing.Point(368, 630);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(624, 97);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 3;
            this.neuLabel10.Text = "祝您身体健康";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel9.Font = new System.Drawing.Font("宋体", 80.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel9.ForeColor = System.Drawing.Color.White;
            this.neuLabel9.Location = new System.Drawing.Point(107, 94);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(426, 107);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 2;
            this.neuLabel9.Text = "工号001";
            // 
            // neuPanel4
            // 
            this.neuPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.neuPanel4.Controls.Add(this.neuLabel1);
            this.neuPanel4.Controls.Add(this.tbDrugCost);
            this.neuPanel4.Controls.Add(this.tbTotCost);
            this.neuPanel4.Controls.Add(this.tbReturnCost);
            this.neuPanel4.Controls.Add(this.tbRealOwnCost);
            this.neuPanel4.Controls.Add(this.tbPayCost);
            this.neuPanel4.Controls.Add(this.lblPaientInfo);
            this.neuPanel4.Controls.Add(this.neuLabel8);
            this.neuPanel4.Controls.Add(this.lbDrugCost);
            this.neuPanel4.Controls.Add(this.neuLabel6);
            this.neuPanel4.Controls.Add(this.neuLabel7);
            this.neuPanel4.Controls.Add(this.lblPayTitle);
            this.neuPanel4.Location = new System.Drawing.Point(3, 118);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(1024, 768);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Red;
            this.neuLabel1.Location = new System.Drawing.Point(589, 665);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(423, 56);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 25;
            this.neuLabel1.Text = "祝您身体健康！";
            // 
            // tbDrugCost
            // 
            this.tbDrugCost.AutoSize = true;
            this.tbDrugCost.BackColor = System.Drawing.Color.Transparent;
            this.tbDrugCost.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbDrugCost.Location = new System.Drawing.Point(182, 683);
            this.tbDrugCost.Name = "tbDrugCost";
            this.tbDrugCost.Size = new System.Drawing.Size(91, 35);
            this.tbDrugCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbDrugCost.TabIndex = 24;
            this.tbDrugCost.Text = "0.00";
            this.tbDrugCost.Visible = false;
            // 
            // tbTotCost
            // 
            this.tbTotCost.AutoSize = true;
            this.tbTotCost.BackColor = System.Drawing.Color.Transparent;
            this.tbTotCost.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTotCost.Location = new System.Drawing.Point(463, 290);
            this.tbTotCost.Name = "tbTotCost";
            this.tbTotCost.Size = new System.Drawing.Size(238, 97);
            this.tbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbTotCost.TabIndex = 23;
            this.tbTotCost.Text = "0.00";
            // 
            // tbReturnCost
            // 
            this.tbReturnCost.AutoSize = true;
            this.tbReturnCost.BackColor = System.Drawing.Color.Transparent;
            this.tbReturnCost.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbReturnCost.ForeColor = System.Drawing.Color.Red;
            this.tbReturnCost.Location = new System.Drawing.Point(737, 141);
            this.tbReturnCost.Name = "tbReturnCost";
            this.tbReturnCost.Size = new System.Drawing.Size(238, 97);
            this.tbReturnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbReturnCost.TabIndex = 22;
            this.tbReturnCost.Text = "0.00";
            // 
            // tbRealOwnCost
            // 
            this.tbRealOwnCost.AutoSize = true;
            this.tbRealOwnCost.BackColor = System.Drawing.Color.Transparent;
            this.tbRealOwnCost.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRealOwnCost.ForeColor = System.Drawing.Color.Red;
            this.tbRealOwnCost.Location = new System.Drawing.Point(271, 141);
            this.tbRealOwnCost.Name = "tbRealOwnCost";
            this.tbRealOwnCost.Size = new System.Drawing.Size(238, 97);
            this.tbRealOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbRealOwnCost.TabIndex = 21;
            this.tbRealOwnCost.Text = "0.00";
            // 
            // lblPaientInfo
            // 
            this.lblPaientInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPaientInfo.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPaientInfo.ForeColor = System.Drawing.Color.Black;
            this.lblPaientInfo.Location = new System.Drawing.Point(21, 9);
            this.lblPaientInfo.Name = "lblPaientInfo";
            this.lblPaientInfo.Size = new System.Drawing.Size(986, 108);
            this.lblPaientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPaientInfo.TabIndex = 16;
            this.lblPaientInfo.Text = "请注意核对基本信息！";
            this.lblPaientInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel8.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel8.Location = new System.Drawing.Point(0, 290);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(527, 97);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 14;
            this.neuLabel8.Text = "出生年月：";
            // 
            // lbDrugCost
            // 
            this.lbDrugCost.AutoSize = true;
            this.lbDrugCost.BackColor = System.Drawing.Color.Transparent;
            this.lbDrugCost.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDrugCost.Location = new System.Drawing.Point(12, 683);
            this.lbDrugCost.Name = "lbDrugCost";
            this.lbDrugCost.Size = new System.Drawing.Size(178, 35);
            this.lbDrugCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDrugCost.TabIndex = 10;
            this.lbDrugCost.Text = "药品总额:";
            this.lbDrugCost.Visible = false;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel6.Location = new System.Drawing.Point(494, 141);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(333, 97);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 8;
            this.neuLabel6.Text = "性别：";
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel7.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel7.Location = new System.Drawing.Point(0, 141);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(333, 97);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 6;
            this.neuLabel7.Text = "姓名：";
            // 
            // lblPayTitle
            // 
            this.lblPayTitle.AutoSize = true;
            this.lblPayTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPayTitle.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPayTitle.Location = new System.Drawing.Point(0, 447);
            this.lblPayTitle.Name = "lblPayTitle";
            this.lblPayTitle.Size = new System.Drawing.Size(527, 97);
            this.lblPayTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPayTitle.TabIndex = 2;
            this.lblPayTitle.Text = "联系电话：";
            // 
            // tbPayCost
            // 
            this.tbPayCost.AutoSize = true;
            this.tbPayCost.BackColor = System.Drawing.Color.Transparent;
            this.tbPayCost.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPayCost.Location = new System.Drawing.Point(439, 447);
            this.tbPayCost.Name = "tbPayCost";
            this.tbPayCost.Size = new System.Drawing.Size(238, 97);
            this.tbPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPayCost.TabIndex = 19;
            this.tbPayCost.Text = "0.00";
            // 
            // frmMiltScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.neuPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMiltScreen";
            this.Text = "门诊外屏";
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDrugCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPaientInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel tbDrugCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel tbTotCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel tbReturnCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel tbRealOwnCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel tbPayCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPayTitle;
    }
}
