namespace FS.SOC.HISFC.Components.Pharmacy.Common.Output
{
    partial class ucOutput
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblGetPerson = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbGetPerson = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ncmbOutputType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.nlbTargetDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbTargetDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.nlbOutputType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nPanelDataChooseList = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.nPanelDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.npanelInputInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ngbInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lbConvertUnit = new System.Windows.Forms.Label();
            this.nlbPriveDept = new System.Windows.Forms.Label();
            this.ucDataChooseList1 = new FS.SOC.HISFC.Components.Pharmacy.Base.ucDataChooseList();
            this.neuGroupBox1.SuspendLayout();
            this.nPanelDataChooseList.SuspendLayout();
            this.nPanelDetail.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.ngbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.lblGetPerson);
            this.neuGroupBox1.Controls.Add(this.ncmbGetPerson);
            this.neuGroupBox1.Controls.Add(this.ncmbOutputType);
            this.neuGroupBox1.Controls.Add(this.nlbTargetDept);
            this.neuGroupBox1.Controls.Add(this.ncmbTargetDept);
            this.neuGroupBox1.Controls.Add(this.nlbOutputType);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(864, 56);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 1;
            this.neuGroupBox1.TabStop = false;
            // 
            // lblGetPerson
            // 
            this.lblGetPerson.AutoSize = true;
            this.lblGetPerson.ForeColor = System.Drawing.Color.Blue;
            this.lblGetPerson.Location = new System.Drawing.Point(642, 26);
            this.lblGetPerson.Name = "lblGetPerson";
            this.lblGetPerson.Size = new System.Drawing.Size(53, 12);
            this.lblGetPerson.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblGetPerson.TabIndex = 7;
            this.lblGetPerson.Text = "领药人：";
            // 
            // ncmbGetPerson
            // 
            this.ncmbGetPerson.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbGetPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbGetPerson.FormattingEnabled = true;
            this.ncmbGetPerson.IsEnter2Tab = false;
            this.ncmbGetPerson.IsFlat = false;
            this.ncmbGetPerson.IsLike = true;
            this.ncmbGetPerson.IsListOnly = false;
            this.ncmbGetPerson.IsPopForm = true;
            this.ncmbGetPerson.IsShowCustomerList = false;
            this.ncmbGetPerson.IsShowID = false;
            this.ncmbGetPerson.IsShowIDAndName = false;
            this.ncmbGetPerson.Location = new System.Drawing.Point(696, 23);
            this.ncmbGetPerson.Name = "ncmbGetPerson";
            this.ncmbGetPerson.ShowCustomerList = false;
            this.ncmbGetPerson.ShowID = false;
            this.ncmbGetPerson.Size = new System.Drawing.Size(132, 20);
            this.ncmbGetPerson.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbGetPerson.TabIndex = 6;
            this.ncmbGetPerson.Tag = "";
            this.ncmbGetPerson.ToolBarUse = false;
            // 
            // ncmbOutputType
            // 
            this.ncmbOutputType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbOutputType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbOutputType.FormattingEnabled = true;
            this.ncmbOutputType.IsEnter2Tab = false;
            this.ncmbOutputType.IsFlat = false;
            this.ncmbOutputType.IsLike = true;
            this.ncmbOutputType.IsListOnly = false;
            this.ncmbOutputType.IsPopForm = true;
            this.ncmbOutputType.IsShowCustomerList = false;
            this.ncmbOutputType.IsShowID = false;
            this.ncmbOutputType.IsShowIDAndName = false;
            this.ncmbOutputType.Location = new System.Drawing.Point(72, 23);
            this.ncmbOutputType.Name = "ncmbOutputType";
            this.ncmbOutputType.ShowCustomerList = false;
            this.ncmbOutputType.ShowID = false;
            this.ncmbOutputType.Size = new System.Drawing.Size(212, 20);
            this.ncmbOutputType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbOutputType.TabIndex = 0;
            this.ncmbOutputType.Tag = "";
            this.ncmbOutputType.ToolBarUse = false;
            // 
            // nlbTargetDept
            // 
            this.nlbTargetDept.AutoSize = true;
            this.nlbTargetDept.ForeColor = System.Drawing.Color.Blue;
            this.nlbTargetDept.Location = new System.Drawing.Point(328, 27);
            this.nlbTargetDept.Name = "nlbTargetDept";
            this.nlbTargetDept.Size = new System.Drawing.Size(65, 12);
            this.nlbTargetDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTargetDept.TabIndex = 5;
            this.nlbTargetDept.Text = "目标科室：";
            // 
            // ncmbTargetDept
            // 
            this.ncmbTargetDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbTargetDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbTargetDept.FormattingEnabled = true;
            this.ncmbTargetDept.IsEnter2Tab = false;
            this.ncmbTargetDept.IsFlat = false;
            this.ncmbTargetDept.IsLike = true;
            this.ncmbTargetDept.IsListOnly = false;
            this.ncmbTargetDept.IsPopForm = true;
            this.ncmbTargetDept.IsShowCustomerList = false;
            this.ncmbTargetDept.IsShowID = false;
            this.ncmbTargetDept.IsShowIDAndName = false;
            this.ncmbTargetDept.Location = new System.Drawing.Point(399, 24);
            this.ncmbTargetDept.Name = "ncmbTargetDept";
            this.ncmbTargetDept.ShowCustomerList = false;
            this.ncmbTargetDept.ShowID = false;
            this.ncmbTargetDept.Size = new System.Drawing.Size(212, 20);
            this.ncmbTargetDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbTargetDept.TabIndex = 4;
            this.ncmbTargetDept.Tag = "";
            this.ncmbTargetDept.ToolBarUse = false;
            // 
            // nlbOutputType
            // 
            this.nlbOutputType.AutoSize = true;
            this.nlbOutputType.ForeColor = System.Drawing.Color.Blue;
            this.nlbOutputType.Location = new System.Drawing.Point(7, 27);
            this.nlbOutputType.Name = "nlbOutputType";
            this.nlbOutputType.Size = new System.Drawing.Size(65, 12);
            this.nlbOutputType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbOutputType.TabIndex = 1;
            this.nlbOutputType.Text = "出库类别：";
            // 
            // nPanelDataChooseList
            // 
            this.nPanelDataChooseList.Controls.Add(this.ucDataChooseList1);
            this.nPanelDataChooseList.Dock = System.Windows.Forms.DockStyle.Left;
            this.nPanelDataChooseList.Location = new System.Drawing.Point(0, 56);
            this.nPanelDataChooseList.Name = "nPanelDataChooseList";
            this.nPanelDataChooseList.Size = new System.Drawing.Size(327, 533);
            this.nPanelDataChooseList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nPanelDataChooseList.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(327, 56);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 533);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // nPanelDetail
            // 
            this.nPanelDetail.Controls.Add(this.neuPanel1);
            this.nPanelDetail.Controls.Add(this.ngbInfo);
            this.nPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nPanelDetail.Location = new System.Drawing.Point(330, 56);
            this.nPanelDetail.Name = "nPanelDetail";
            this.nPanelDetail.Size = new System.Drawing.Size(534, 533);
            this.nPanelDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nPanelDetail.TabIndex = 9;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanelDetail);
            this.neuPanel1.Controls.Add(this.npanelInputInfo);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(534, 482);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 11;
            // 
            // neuPanelDetail
            // 
            this.neuPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelDetail.Location = new System.Drawing.Point(0, 20);
            this.neuPanelDetail.Name = "neuPanelDetail";
            this.neuPanelDetail.Size = new System.Drawing.Size(534, 462);
            this.neuPanelDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelDetail.TabIndex = 6;
            // 
            // npanelInputInfo
            // 
            this.npanelInputInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.npanelInputInfo.Location = new System.Drawing.Point(0, 0);
            this.npanelInputInfo.Name = "npanelInputInfo";
            this.npanelInputInfo.Size = new System.Drawing.Size(534, 20);
            this.npanelInputInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npanelInputInfo.TabIndex = 5;
            // 
            // ngbInfo
            // 
            this.ngbInfo.Controls.Add(this.lbConvertUnit);
            this.ngbInfo.Controls.Add(this.nlbPriveDept);
            this.ngbInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbInfo.Location = new System.Drawing.Point(0, 482);
            this.ngbInfo.Name = "ngbInfo";
            this.ngbInfo.Size = new System.Drawing.Size(534, 51);
            this.ngbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbInfo.TabIndex = 10;
            this.ngbInfo.TabStop = false;
            this.ngbInfo.Text = "附加信息";
            // 
            // lbConvertUnit
            // 
            this.lbConvertUnit.AutoSize = true;
            this.lbConvertUnit.ForeColor = System.Drawing.Color.Blue;
            this.lbConvertUnit.Location = new System.Drawing.Point(364, 23);
            this.lbConvertUnit.Name = "lbConvertUnit";
            this.lbConvertUnit.Size = new System.Drawing.Size(203, 12);
            this.lbConvertUnit.TabIndex = 1;
            this.lbConvertUnit.Text = "F12可以在包装单位和最小单位间转换";
            // 
            // nlbPriveDept
            // 
            this.nlbPriveDept.AutoSize = true;
            this.nlbPriveDept.ForeColor = System.Drawing.Color.Blue;
            this.nlbPriveDept.Location = new System.Drawing.Point(10, 23);
            this.nlbPriveDept.Name = "nlbPriveDept";
            this.nlbPriveDept.Size = new System.Drawing.Size(0, 12);
            this.nlbPriveDept.TabIndex = 0;
            // 
            // ucDataChooseList1
            // 
            this.ucDataChooseList1.ChooseCompletedEvent = null;
            this.ucDataChooseList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataChooseList1.Location = new System.Drawing.Point(0, 0);
            this.ucDataChooseList1.Name = "ucDataChooseList1";
            this.ucDataChooseList1.SettingFileName = "";
            this.ucDataChooseList1.Size = new System.Drawing.Size(327, 533);
            this.ucDataChooseList1.TabIndex = 2;
            // 
            // ucOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nPanelDetail);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.nPanelDataChooseList);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucOutput";
            this.Size = new System.Drawing.Size(864, 589);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.nPanelDataChooseList.ResumeLayout(false);
            this.nPanelDetail.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.ngbInfo.ResumeLayout(false);
            this.ngbInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbOutputType;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTargetDept;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbTargetDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbOutputType;
        private FS.FrameWork.WinForms.Controls.NeuPanel nPanelDataChooseList;
        private FS.SOC.HISFC.Components.Pharmacy.Base.ucDataChooseList ucDataChooseList1;
        private System.Windows.Forms.Splitter splitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel nPanelDetail;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox ngbInfo;
        private System.Windows.Forms.Label nlbPriveDept;
        private FS.FrameWork.WinForms.Controls.NeuPanel npanelInputInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelDetail;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblGetPerson;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbGetPerson;
        private System.Windows.Forms.Label lbConvertUnit;
    }
}