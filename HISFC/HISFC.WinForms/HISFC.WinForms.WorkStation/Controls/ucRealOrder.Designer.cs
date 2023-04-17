namespace Neusoft.HISFC.WinForms.WorkStation.Controls
{
    partial class ucRealOrder
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
            this.components = new System.ComponentModel.Container();
            this.tvDoctorPatientList1 = new Neusoft.HISFC.Components.Order.Controls.tvDoctorPatientList();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbAddOrder = new System.Windows.Forms.ToolStripButton();
            this.tbDelOrder = new System.Windows.Forms.ToolStripButton();
            this.tbComboOrder = new System.Windows.Forms.ToolStripButton();
            this.tbCancelOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHerbal = new System.Windows.Forms.ToolStripButton();
            this.tbCheck = new System.Windows.Forms.ToolStripButton();
            this.tbOperation = new System.Windows.Forms.ToolStripButton();
            this.tbDiseaseReport = new System.Windows.Forms.ToolStripButton();
            this.tbChooseDoct = new System.Windows.Forms.ToolStripButton();
            this.tbSaveOrder = new System.Windows.Forms.ToolStripButton();
            this.tbExitOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbRetidyOrder = new System.Windows.Forms.ToolStripButton();
            this.tbDcAllLongOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tbQueryOrder = new System.Windows.Forms.ToolStripButton();
            this.tbFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tbValid = new System.Windows.Forms.ToolStripMenuItem();
            this.tbInValid = new System.Windows.Forms.ToolStripMenuItem();
            this.tbToday = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPrintOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tbLisResultPrint = new System.Windows.Forms.ToolStripButton();
            this.tbPacsResultPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tbEMR = new System.Windows.Forms.ToolStripButton();
            this.tb1Exit = new System.Windows.Forms.ToolStripButton();
            this.tbLevelOrder = new System.Windows.Forms.ToolStripButton();
            this.tbAssayCure = new System.Windows.Forms.ToolStripButton();
            this.ucOrder1 = new Neusoft.HISFC.Components.Order.Controls.ucOrder();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucQueryInpatientNo1 = new Neusoft.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panelTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panelToolBar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 76);
            this.panel1.Size = new System.Drawing.Size(1028, 591);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucQueryInpatientNo1);
            this.panel2.Size = new System.Drawing.Size(168, 591);
            this.panel2.Controls.SetChildIndex(this.neuTextBox1, 0);
            this.panel2.Controls.SetChildIndex(this.ucQueryInpatientNo1, 0);
            this.panel2.Controls.SetChildIndex(this.panelTree, 0);
            this.panel2.Controls.SetChildIndex(this.btnClose, 0);
            this.panel2.Controls.SetChildIndex(this.btnShow, 0);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(171, 0);
            this.panelMain.Size = new System.Drawing.Size(857, 591);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Size = new System.Drawing.Size(857, 591);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // panelTree
            // 
            this.panelTree.Controls.Add(this.tvDoctorPatientList1);
            this.panelTree.Size = new System.Drawing.Size(168, 570);
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.BackColor = System.Drawing.Color.White;
            this.neuTextBox1.Size = new System.Drawing.Size(168, 21);
            // 
            // lblSet
            // 
            this.lblSet.Location = new System.Drawing.Point(975, 650);
            // 
            // btnShow
            // 
            this.btnShow.Size = new System.Drawing.Size(13, 10933);
            // 
            // panelToolBar
            // 
            this.panelToolBar.Controls.Add(this.toolStrip1);
            this.panelToolBar.Size = new System.Drawing.Size(1028, 51);
            // 
            // tvDoctorPatientList1
            // 
            this.tvDoctorPatientList1.Checked = Neusoft.HISFC.Components.Common.Controls.tvPatientList.enuChecked.None;
            this.tvDoctorPatientList1.Direction = Neusoft.HISFC.Components.Common.Controls.tvPatientList.enuShowDirection.Ahead;
            this.tvDoctorPatientList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDoctorPatientList1.Font = new System.Drawing.Font("Arial", 9F);
            this.tvDoctorPatientList1.HideSelection = false;
            this.tvDoctorPatientList1.ImageIndex = 0;
            this.tvDoctorPatientList1.IsShowContextMenu = true;
            this.tvDoctorPatientList1.IsShowCount = true;
            this.tvDoctorPatientList1.IsShowNewPatient = true;
            this.tvDoctorPatientList1.IsShowPatientNo = true;
            this.tvDoctorPatientList1.Location = new System.Drawing.Point(0, 0);
            this.tvDoctorPatientList1.Name = "tvDoctorPatientList1";
            this.tvDoctorPatientList1.SelectedImageIndex = 0;
            this.tvDoctorPatientList1.ShowType = Neusoft.HISFC.Components.Common.Controls.tvPatientList.enuShowType.Bed;
            this.tvDoctorPatientList1.Size = new System.Drawing.Size(168, 570);
            this.tvDoctorPatientList1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvDoctorPatientList1.TabIndex = 0;
            this.tvDoctorPatientList1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDoctorPatientList1_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbRefresh,
            this.tbGroup,
            this.toolStripSeparator6,
            this.tbAddOrder,
            this.tbDelOrder,
            this.tbComboOrder,
            this.tbCancelOrder,
            this.toolStripSeparator1,
            this.tsbHerbal,
            this.tbCheck,
            this.tbOperation,
            this.tbDiseaseReport,
            this.tbChooseDoct,
            this.tbSaveOrder,
            this.tbExitOrder,
            this.toolStripSeparator5,
            this.tbRetidyOrder,
            this.tbDcAllLongOrder,
            this.toolStripSeparator7,
            this.tbQueryOrder,
            this.tbFilter,
            this.tbPrintOrder,
            this.toolStripSeparator8,
            this.tbLisResultPrint,
            this.tbPacsResultPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1028, 51);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked_1);
            // 
            // tbRefresh
            // 
            this.tbRefresh.Image = global::Neusoft.HISFC.WinForms.WorkStation.Properties.Resources.召回;
            this.tbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRefresh.Name = "tbRefresh";
            this.tbRefresh.Size = new System.Drawing.Size(36, 48);
            this.tbRefresh.Text = "刷新";
            this.tbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbGroup
            // 
            this.tbGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(57, 48);
            this.tbGroup.Text = "组套管理";
            this.tbGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 51);
            // 
            // tbAddOrder
            // 
            this.tbAddOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAddOrder.Name = "tbAddOrder";
            this.tbAddOrder.Size = new System.Drawing.Size(33, 48);
            this.tbAddOrder.Text = "开立";
            this.tbAddOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbAddOrder.ToolTipText = "医嘱开立";
            // 
            // tbDelOrder
            // 
            this.tbDelOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDelOrder.Name = "tbDelOrder";
            this.tbDelOrder.Size = new System.Drawing.Size(33, 48);
            this.tbDelOrder.Text = "删除";
            this.tbDelOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbComboOrder
            // 
            this.tbComboOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbComboOrder.Name = "tbComboOrder";
            this.tbComboOrder.Size = new System.Drawing.Size(33, 48);
            this.tbComboOrder.Text = "组合";
            this.tbComboOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbComboOrder.ToolTipText = "组合医嘱";
            // 
            // tbCancelOrder
            // 
            this.tbCancelOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCancelOrder.Name = "tbCancelOrder";
            this.tbCancelOrder.Size = new System.Drawing.Size(57, 48);
            this.tbCancelOrder.Text = "取消组合";
            this.tbCancelOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // tsbHerbal
            // 
            this.tsbHerbal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHerbal.Name = "tsbHerbal";
            this.tsbHerbal.Size = new System.Drawing.Size(33, 48);
            this.tsbHerbal.Text = "草药";
            this.tsbHerbal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbCheck
            // 
            this.tbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCheck.Name = "tbCheck";
            this.tbCheck.Size = new System.Drawing.Size(33, 48);
            this.tbCheck.Text = "检查";
            this.tbCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbCheck.ToolTipText = "检查申请单";
            // 
            // tbOperation
            // 
            this.tbOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbOperation.Name = "tbOperation";
            this.tbOperation.Size = new System.Drawing.Size(33, 48);
            this.tbOperation.Text = "手术";
            this.tbOperation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbOperation.ToolTipText = "手术申请";
            // 
            // tbDiseaseReport
            // 
            this.tbDiseaseReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDiseaseReport.Name = "tbDiseaseReport";
            this.tbDiseaseReport.Size = new System.Drawing.Size(69, 48);
            this.tbDiseaseReport.Text = "传染病报告";
            this.tbDiseaseReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbChooseDoct
            // 
            this.tbChooseDoct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbChooseDoct.Name = "tbChooseDoct";
            this.tbChooseDoct.Size = new System.Drawing.Size(45, 83);
            this.tbChooseDoct.Text = "选医师";
            this.tbChooseDoct.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tbChooseDoct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbChooseDoct.Visible = false;
            // 
            // tbSaveOrder
            // 
            this.tbSaveOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSaveOrder.Name = "tbSaveOrder";
            this.tbSaveOrder.Size = new System.Drawing.Size(33, 48);
            this.tbSaveOrder.Text = "保存";
            this.tbSaveOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbSaveOrder.ToolTipText = "保存医嘱";
            // 
            // tbExitOrder
            // 
            this.tbExitOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExitOrder.Name = "tbExitOrder";
            this.tbExitOrder.Size = new System.Drawing.Size(57, 48);
            this.tbExitOrder.Text = "退出医嘱";
            this.tbExitOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbExitOrder.ToolTipText = "退出医嘱开立";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 51);
            // 
            // tbRetidyOrder
            // 
            this.tbRetidyOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRetidyOrder.Name = "tbRetidyOrder";
            this.tbRetidyOrder.Size = new System.Drawing.Size(57, 48);
            this.tbRetidyOrder.Text = "重整医嘱";
            this.tbRetidyOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbDcAllLongOrder
            // 
            this.tbDcAllLongOrder.BackColor = System.Drawing.Color.Honeydew;
            this.tbDcAllLongOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDcAllLongOrder.Name = "tbDcAllLongOrder";
            this.tbDcAllLongOrder.Size = new System.Drawing.Size(57, 48);
            this.tbDcAllLongOrder.Text = "长嘱全停";
            this.tbDcAllLongOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbDcAllLongOrder.ToolTipText = "长嘱全停";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 51);
            // 
            // tbQueryOrder
            // 
            this.tbQueryOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbQueryOrder.Name = "tbQueryOrder";
            this.tbQueryOrder.Size = new System.Drawing.Size(33, 48);
            this.tbQueryOrder.Text = "查询";
            this.tbQueryOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbFilter
            // 
            this.tbFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAll,
            this.tbValid,
            this.tbInValid,
            this.tbToday,
            this.tbNew});
            this.tbFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(42, 48);
            this.tbFilter.Text = "过滤";
            this.tbFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbAll
            // 
            this.tbAll.Name = "tbAll";
            this.tbAll.Size = new System.Drawing.Size(118, 22);
            this.tbAll.Text = "全部医嘱";
            // 
            // tbValid
            // 
            this.tbValid.Name = "tbValid";
            this.tbValid.Size = new System.Drawing.Size(118, 22);
            this.tbValid.Text = "有效医嘱";
            // 
            // tbInValid
            // 
            this.tbInValid.Name = "tbInValid";
            this.tbInValid.Size = new System.Drawing.Size(118, 22);
            this.tbInValid.Text = "作废医嘱";
            // 
            // tbToday
            // 
            this.tbToday.Name = "tbToday";
            this.tbToday.Size = new System.Drawing.Size(118, 22);
            this.tbToday.Text = "当天医嘱";
            // 
            // tbNew
            // 
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(118, 22);
            this.tbNew.Text = "未审医嘱";
            this.tbNew.ToolTipText = "未审核的医嘱";
            // 
            // tbPrintOrder
            // 
            this.tbPrintOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrintOrder.Name = "tbPrintOrder";
            this.tbPrintOrder.Size = new System.Drawing.Size(33, 48);
            this.tbPrintOrder.Text = "打印";
            this.tbPrintOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 51);
            // 
            // tbLisResultPrint
            // 
            this.tbLisResultPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbLisResultPrint.Name = "tbLisResultPrint";
            this.tbLisResultPrint.Size = new System.Drawing.Size(57, 48);
            this.tbLisResultPrint.Text = "检验结果";
            this.tbLisResultPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbPacsResultPrint
            // 
            this.tbPacsResultPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPacsResultPrint.Name = "tbPacsResultPrint";
            this.tbPacsResultPrint.Size = new System.Drawing.Size(57, 48);
            this.tbPacsResultPrint.Text = "检查结果";
            this.tbPacsResultPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 51);
            // 
            // tbEMR
            // 
            this.tbEMR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbEMR.Name = "tbEMR";
            this.tbEMR.Size = new System.Drawing.Size(33, 48);
            this.tbEMR.Text = "病历";
            this.tbEMR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tb1Exit
            // 
            this.tb1Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb1Exit.Name = "tb1Exit";
            this.tb1Exit.Size = new System.Drawing.Size(57, 48);
            this.tb1Exit.Text = "退出窗口";
            this.tb1Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbLevelOrder
            // 
            this.tbLevelOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbLevelOrder.Name = "tbLevelOrder";
            this.tbLevelOrder.Size = new System.Drawing.Size(36, 48);
            this.tbLevelOrder.Text = "层级";
            this.tbLevelOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbLevelOrder.ToolTipText = "层级医嘱";
            // 
            // tbAssayCure
            // 
            this.tbAssayCure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAssayCure.Name = "tbAssayCure";
            this.tbAssayCure.Size = new System.Drawing.Size(36, 48);
            this.tbAssayCure.Text = "化疗";
            this.tbAssayCure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ucOrder1
            // 
            this.ucOrder1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucOrder1.Checkslipno = null;
            this.ucOrder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrder1.IsFullConvertToHalf = true;
            this.ucOrder1.IsPrint = false;
            this.ucOrder1.IsShowPopMenu = true;
            this.ucOrder1.Location = new System.Drawing.Point(0, 0);
            this.ucOrder1.Name = "ucOrder1";
            this.ucOrder1.Size = new System.Drawing.Size(849, 566);
            this.ucOrder1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucOrder1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(849, 566);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "医嘱";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(0, 21);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = Neusoft.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(168, 570);
            this.ucQueryInpatientNo1.TabIndex = 1;
            this.ucQueryInpatientNo1.myEvent += new Neusoft.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // ucRealOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.IsUseDefaultBar = false;
            this.Name = "ucRealOrder";
            this.Size = new System.Drawing.Size(1028, 667);
            this.Load += new System.EventHandler(this.ucRealOrder_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panelTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panelToolBar.ResumeLayout(false);
            this.panelToolBar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Neusoft.HISFC.Components.Order.Controls.tvDoctorPatientList tvDoctorPatientList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbComboOrder;
        private System.Windows.Forms.ToolStripButton tbCancelOrder;
        private System.Windows.Forms.ToolStripButton tbExitOrder;
        private System.Windows.Forms.ToolStripButton tb1Exit;
        protected System.Windows.Forms.ToolStripButton tbAddOrder;
        protected System.Windows.Forms.ToolStripButton tbDelOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tbFilter;
        private System.Windows.Forms.ToolStripMenuItem tbAll;
        private System.Windows.Forms.ToolStripMenuItem tbValid;
        private System.Windows.Forms.ToolStripMenuItem tbInValid;
        private System.Windows.Forms.ToolStripMenuItem tbToday;
        private System.Windows.Forms.ToolStripButton tbSaveOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbQueryOrder;
        private System.Windows.Forms.ToolStripButton tbPrintOrder;
        private System.Windows.Forms.TabPage tabPage1;
        private Neusoft.HISFC.Components.Order.Controls.ucOrder ucOrder1;
        private System.Windows.Forms.ToolStripMenuItem tbNew;
        private System.Windows.Forms.ToolStripButton tbCheck;
        private System.Windows.Forms.ToolStripButton tbOperation;
        private System.Windows.Forms.ToolStripButton tbGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbHerbal;
        private System.Windows.Forms.ToolStripButton tbRefresh;
        private System.Windows.Forms.ToolStripButton tbChooseDoct;
        private System.Windows.Forms.ToolStripButton tbRetidyOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tbAssayCure;
        private Neusoft.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private System.Windows.Forms.ToolStripButton tbDiseaseReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tbLisResultPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tbPacsResultPrint;
        private System.Windows.Forms.ToolStripButton tbLevelOrder;
        private System.Windows.Forms.ToolStripButton tbDcAllLongOrder;
        private System.Windows.Forms.ToolStripButton tbEMR;


    }
}