namespace FS.HISFC.WinForms.WorkStation
{
    partial class frmOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrder));
            this.tvDoctorPatientList1 = new FS.HISFC.Components.Order.Controls.tvDoctorPatientList();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbPackage = new System.Windows.Forms.ToolStripButton();
            this.tbItem = new System.Windows.Forms.ToolStripButton();
            this.tbAddOrder = new System.Windows.Forms.ToolStripButton();
            this.tbMTOrder = new System.Windows.Forms.ToolStripButton();
            this.tbInfectionReport = new System.Windows.Forms.ToolStripButton();
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
            tbUCULOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tbToday = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPrintOrder = new System.Windows.Forms.ToolStripButton();
            this.tbRecipePrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tbLisResultPrint = new System.Windows.Forms.ToolStripButton();
            tbResultPrint = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbPacsResultPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tbEMR = new System.Windows.Forms.ToolStripButton();
            this.tblEditPacsApply = new System.Windows.Forms.ToolStripButton();
            this.tb1Exit = new System.Windows.Forms.ToolStripButton();
            this.tbPass = new System.Windows.Forms.ToolStripButton();
            this.tbMSG = new System.Windows.Forms.ToolStripButton();
            this.tbPrintAgain = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbBatchDeal = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbPostOperat = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSwitchDept = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLeaveHos = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTreatmentType = new System.Windows.Forms.ToolStripMenuItem();
            tbDead = new System.Windows.Forms.ToolStripMenuItem();
            tbPreOperat = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLevelOrder = new System.Windows.Forms.ToolStripButton();
            this.tbAssayCure = new System.Windows.Forms.ToolStripButton();
            this.ucOrder1 = new FS.HISFC.Components.Order.Controls.ucOrder();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.tbChooseUL = new System.Windows.Forms.ToolStripButton();
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
            this.panel1.Size = new System.Drawing.Size(1028, 561);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucQueryInpatientNo1);
            this.panel2.Size = new System.Drawing.Size(168, 561);
            this.panel2.Controls.SetChildIndex(this.neuTextBox1, 0);
            this.panel2.Controls.SetChildIndex(this.ucQueryInpatientNo1, 0);
            this.panel2.Controls.SetChildIndex(this.panelTree, 0);
            this.panel2.Controls.SetChildIndex(this.btnClose, 0);
            this.panel2.Controls.SetChildIndex(this.btnShow, 0);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(171, 0);
            this.panelMain.Size = new System.Drawing.Size(857, 561);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Size = new System.Drawing.Size(857, 561);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // panelTree
            // 
            this.panelTree.Controls.Add(this.tvDoctorPatientList1);
            this.panelTree.Size = new System.Drawing.Size(168, 540);
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
            this.btnShow.Size = new System.Drawing.Size(13, 12527);
            // 
            // panelToolBar
            // 
            this.panelToolBar.Controls.Add(this.toolStrip1);
            this.panelToolBar.Size = new System.Drawing.Size(1028, 57);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 643);
            this.statusBar1.Size = new System.Drawing.Size(1028, 24);
            // 
            // tvDoctorPatientList1
            // 
            this.tvDoctorPatientList1.Checked = FS.HISFC.Components.Common.Controls.tvPatientListByDoc.enuChecked.None;
            this.tvDoctorPatientList1.Direction = FS.HISFC.Components.Common.Controls.tvPatientListByDoc.enuShowDirection.Ahead;
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
            this.tvDoctorPatientList1.ShowType = FS.HISFC.Components.Common.Controls.tvPatientListByDoc.enuShowType.Bed;
            this.tvDoctorPatientList1.Size = new System.Drawing.Size(168, 540);
            this.tvDoctorPatientList1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvDoctorPatientList1.TabIndex = 0;
            this.tvDoctorPatientList1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDoctorPatientList1_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbRefresh,
            this.tbGroup,
            this.toolStripSeparator6,
            this.tbPackage,
            this.tbItem,
            this.tbAddOrder,
            this.tbMTOrder,
            this.tbInfectionReport,
            this.tbDelOrder,
            this.tbComboOrder,
            this.tbCancelOrder,
            this.toolStripSeparator1,
            this.tsbHerbal,
            this.tbChooseUL,
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
            this.tbRecipePrint,
            this.toolStripSeparator8,
            this.tbLisResultPrint,
            this.tbResultPrint,
            this.tbPacsResultPrint,
            this.toolStripSeparator9,
            this.tbEMR,
            this.tblEditPacsApply,
            this.tb1Exit,
            this.tbPass,
            this.tbMSG,
            this.tbPrintAgain,
            this.tbBatchDeal});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1028, 56);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked_1);
            // 
            // tbRefresh
            // 
            this.tbRefresh.Image = global::FS.HISFC.WinForms.WorkStation.Properties.Resources.召回;
            this.tbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRefresh.Name = "tbRefresh";
            this.tbRefresh.Size = new System.Drawing.Size(36, 53);
            this.tbRefresh.Text = "刷新";
            this.tbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbGroup
            // 
            this.tbGroup.Image = ((System.Drawing.Image)(resources.GetObject("tbGroup.Image")));
            this.tbGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(60, 53);
            this.tbGroup.Text = "组套管理";
            this.tbGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 56);
            // 
            // tbPackage
            // 
            this.tbPackage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPackage.Name = "tbPackage";
            this.tbPackage.Size = new System.Drawing.Size(36, 53);
            this.tbPackage.Text = "套餐查看";
            this.tbPackage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbPackage.ToolTipText = "套餐查看";

            // 
            // tbItem
            // 
            this.tbItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(36, 53);
            this.tbItem.Text = "未执行项目";
            this.tbItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbItem.ToolTipText = "未执行项目";
            // 
            // tbAddOrder
            // 
            this.tbAddOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbAddOrder.Image")));
            this.tbAddOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAddOrder.Name = "tbAddOrder";
            this.tbAddOrder.Size = new System.Drawing.Size(36, 53);
            this.tbAddOrder.Text = "开立";
            this.tbAddOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbAddOrder.ToolTipText = "医嘱开立";
            // 
            // tbMTOrder
            // 
            this.tbMTOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbMTOrder.Name = "tbMTOrder";
            this.tbMTOrder.Size = new System.Drawing.Size(60, 53);
            this.tbMTOrder.Text = "医技预约";
            this.tbMTOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbMTOrder.ToolTipText = "医技预约";
            // 
            // tbInfectionReport
            // 
            this.tbInfectionReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbInfectionReport.Name = "tbInfectionReport";
            this.tbInfectionReport.Size = new System.Drawing.Size(60, 53);
            this.tbInfectionReport.Text = "院感上报";
            this.tbInfectionReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbInfectionReport.ToolTipText = "院感上报";
            // 
            // tbDelOrder
            // 
            this.tbDelOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbDelOrder.Image")));
            this.tbDelOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDelOrder.Name = "tbDelOrder";
            this.tbDelOrder.Size = new System.Drawing.Size(36, 53);
            this.tbDelOrder.Text = "删除";
            this.tbDelOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbComboOrder
            // 
            this.tbComboOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbComboOrder.Image")));
            this.tbComboOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbComboOrder.Name = "tbComboOrder";
            this.tbComboOrder.Size = new System.Drawing.Size(36, 53);
            this.tbComboOrder.Text = "组合";
            this.tbComboOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbComboOrder.ToolTipText = "组合医嘱";
            // 
            // tbCancelOrder
            // 
            this.tbCancelOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbCancelOrder.Image")));
            this.tbCancelOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCancelOrder.Name = "tbCancelOrder";
            this.tbCancelOrder.Size = new System.Drawing.Size(60, 53);
            this.tbCancelOrder.Text = "取消组合";
            this.tbCancelOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // tsbHerbal
            // 
            this.tsbHerbal.Image = ((System.Drawing.Image)(resources.GetObject("tsbHerbal.Image")));
            this.tsbHerbal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHerbal.Name = "tsbHerbal";
            this.tsbHerbal.Size = new System.Drawing.Size(36, 53);
            this.tsbHerbal.Text = "草药";
            this.tsbHerbal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbCheck
            // 
            this.tbCheck.Image = ((System.Drawing.Image)(resources.GetObject("tbCheck.Image")));
            this.tbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCheck.Name = "tbCheck";
            this.tbCheck.Size = new System.Drawing.Size(36, 53);
            this.tbCheck.Text = "检查";
            this.tbCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbCheck.ToolTipText = "检查申请单";
            // 
            // tbOperation
            // 
            this.tbOperation.Image = ((System.Drawing.Image)(resources.GetObject("tbOperation.Image")));
            this.tbOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbOperation.Name = "tbOperation";
            this.tbOperation.Size = new System.Drawing.Size(36, 53);
            this.tbOperation.Text = "手术";
            this.tbOperation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbOperation.ToolTipText = "手术申请";
            // 
            // tbDiseaseReport
            // 
            this.tbDiseaseReport.Image = ((System.Drawing.Image)(resources.GetObject("tbDiseaseReport.Image")));
            this.tbDiseaseReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDiseaseReport.Name = "tbDiseaseReport";
            this.tbDiseaseReport.Size = new System.Drawing.Size(72, 53);
            this.tbDiseaseReport.Text = "传染病报告";
            this.tbDiseaseReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbChooseDoct
            // 
            this.tbChooseDoct.Image = ((System.Drawing.Image)(resources.GetObject("tbChooseDoct.Image")));
            this.tbChooseDoct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbChooseDoct.Name = "tbChooseDoct";
            this.tbChooseDoct.Size = new System.Drawing.Size(48, 53);
            this.tbChooseDoct.Text = "选医师";
            this.tbChooseDoct.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tbChooseDoct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbChooseDoct.Visible = false;
            // 
            // tbSaveOrder
            // 
            this.tbSaveOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbSaveOrder.Image")));
            this.tbSaveOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSaveOrder.Name = "tbSaveOrder";
            this.tbSaveOrder.Size = new System.Drawing.Size(36, 53);
            this.tbSaveOrder.Text = "保存";
            this.tbSaveOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbSaveOrder.ToolTipText = "保存医嘱";
            // 
            // tbExitOrder
            // 
            this.tbExitOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbExitOrder.Image")));
            this.tbExitOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExitOrder.Name = "tbExitOrder";
            this.tbExitOrder.Size = new System.Drawing.Size(60, 53);
            this.tbExitOrder.Text = "退出医嘱";
            this.tbExitOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbExitOrder.ToolTipText = "退出医嘱开立";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 56);
            // 
            // tbRetidyOrder
            // 
            this.tbRetidyOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbRetidyOrder.Image")));
            this.tbRetidyOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRetidyOrder.Name = "tbRetidyOrder";
            this.tbRetidyOrder.Size = new System.Drawing.Size(60, 53);
            this.tbRetidyOrder.Text = "重整医嘱";
            this.tbRetidyOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbDcAllLongOrder
            // 
            this.tbDcAllLongOrder.BackColor = System.Drawing.Color.Honeydew;
            this.tbDcAllLongOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbDcAllLongOrder.Image")));
            this.tbDcAllLongOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDcAllLongOrder.Name = "tbDcAllLongOrder";
            this.tbDcAllLongOrder.Size = new System.Drawing.Size(60, 53);
            this.tbDcAllLongOrder.Text = "长嘱全停";
            this.tbDcAllLongOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbDcAllLongOrder.ToolTipText = "长嘱全停";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 56);
            // 
            // tbQueryOrder
            // 
            this.tbQueryOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbQueryOrder.Image")));
            this.tbQueryOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbQueryOrder.Name = "tbQueryOrder";
            this.tbQueryOrder.Size = new System.Drawing.Size(36, 53);
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
            this.tbNew,
            tbUCULOrder});
            this.tbFilter.Image = ((System.Drawing.Image)(resources.GetObject("tbFilter.Image")));
            this.tbFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(45, 53);
            this.tbFilter.Text = "过滤";
            this.tbFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbAll
            // 
            this.tbAll.Name = "tbAll";
            this.tbAll.Size = new System.Drawing.Size(124, 22);
            this.tbAll.Text = "全部医嘱";
            // 
            // tbValid
            // 
            this.tbValid.Name = "tbValid";
            this.tbValid.Size = new System.Drawing.Size(124, 22);
            this.tbValid.Text = "有效医嘱";
            // 
            // tbInValid
            // 
            this.tbInValid.Name = "tbInValid";
            this.tbInValid.Size = new System.Drawing.Size(124, 22);
            this.tbInValid.Text = "作废医嘱";
            // 
            // tbToday
            // 
            this.tbToday.Name = "tbToday";
            this.tbToday.Size = new System.Drawing.Size(124, 22);
            this.tbToday.Text = "当天医嘱";
            // 
            // tbNew
            // 
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(124, 22);
            this.tbNew.Text = "未审医嘱";
            this.tbNew.ToolTipText = "未审核的医嘱";
            // 
            // tbUCULOrder
            // 
            this.tbUCULOrder.Name = "tbUCULOrder";
            this.tbUCULOrder.Size = new System.Drawing.Size(124, 22);
            this.tbUCULOrder.Text = "检查检验医嘱";
            this.tbUCULOrder.ToolTipText = "检查检验医嘱";
            // 
            // tbPrintOrder
            // 
            this.tbPrintOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbPrintOrder.Image")));
            this.tbPrintOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrintOrder.Name = "tbPrintOrder";
            this.tbPrintOrder.Size = new System.Drawing.Size(36, 53);
            this.tbPrintOrder.Text = "打印";
            this.tbPrintOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 56);
            // 
            // tbLisResultPrint
            // 
            this.tbLisResultPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbLisResultPrint.Image")));
            this.tbLisResultPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbLisResultPrint.Name = "tbLisResultPrint";
            this.tbLisResultPrint.Size = new System.Drawing.Size(60, 53);
            this.tbLisResultPrint.Text = "检验结果";
            this.tbLisResultPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbResultPrint
            // 
            this.tbResultPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {});
            resources.ApplyResources(this.tbResultPrint, "tbResultPrint");
            this.tbResultPrint.Name = "tbResultPrint";
            this.tbResultPrint.Text = "医疗结果";
            this.tbResultPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbPacsResultPrint
            // 
            this.tbPacsResultPrint.Image = ((System.Drawing.Image)(resources.GetObject("tbPacsResultPrint.Image")));
            this.tbPacsResultPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPacsResultPrint.Name = "tbPacsResultPrint";
            this.tbPacsResultPrint.Size = new System.Drawing.Size(60, 53);
            this.tbPacsResultPrint.Text = "检查结果";
            this.tbPacsResultPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 56);
            // 
            // tbEMR
            // 
            this.tbEMR.Image = ((System.Drawing.Image)(resources.GetObject("tbEMR.Image")));
            this.tbEMR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbEMR.Name = "tbEMR";
            this.tbEMR.Size = new System.Drawing.Size(36, 53);
            this.tbEMR.Text = "病历";
            this.tbEMR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tblEditPacsApply
            // 
            this.tblEditPacsApply.BackColor = System.Drawing.Color.Honeydew;
            this.tblEditPacsApply.Image = ((System.Drawing.Image)(resources.GetObject("tblEditPacsApply.Image")));
            this.tblEditPacsApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblEditPacsApply.Name = "tblEditPacsApply";
            this.tblEditPacsApply.Size = new System.Drawing.Size(72, 53);
            this.tblEditPacsApply.Text = "编辑申请单";
            this.tblEditPacsApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tb1Exit
            // 
            this.tb1Exit.Image = ((System.Drawing.Image)(resources.GetObject("tb1Exit.Image")));
            this.tb1Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb1Exit.Name = "tb1Exit";
            this.tb1Exit.Size = new System.Drawing.Size(60, 53);
            this.tb1Exit.Text = "退出窗口";
            this.tb1Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbPass
            // 
            this.tbPass.Image = ((System.Drawing.Image)(resources.GetObject("tbPass.Image")));
            this.tbPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(60, 53);
            this.tbPass.Text = "合理用药";
            this.tbPass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbPass.ToolTipText = "合理用药监控";

            // 
            // tbMSG
            // 
            this.tbMSG.Image = ((System.Drawing.Image)(resources.GetObject("tbPass.Image")));
            this.tbMSG.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbMSG.Name = "tbMSG";
            this.tbMSG.Size = new System.Drawing.Size(60, 53);
            this.tbMSG.Text = "消息发送";
            this.tbMSG.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbMSG.ToolTipText = "消息发送";

            // 
            // tbPrintAgain
            // 
            this.tbPrintAgain.Image = ((System.Drawing.Image)(resources.GetObject("tbPrintAgain.Image")));
            this.tbPrintAgain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrintAgain.Name = "tbPrintAgain";
            this.tbPrintAgain.Size = new System.Drawing.Size(57, 53);
            this.tbPrintAgain.Text = "打印预览";
            this.tbPrintAgain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbRecipePrint
            // 
            this.tbRecipePrint.Image = ((System.Drawing.Image)(resources.GetObject("tbPrintOrder.Image")));
            this.tbRecipePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRecipePrint.Name = "tbRecipePrint";
            this.tbRecipePrint.Size = new System.Drawing.Size(36, 53);
            this.tbRecipePrint.Text = "处方打印";
            this.tbRecipePrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        
            // 
            // tbBatchDeal
            // 
            this.tbBatchDeal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbLeaveHos,
            this.tbSwitchDept,
            this.tbDead,
            tbPreOperat,
            this.tbPostOperat,
            this.tbTreatmentType
            });
            this.tbBatchDeal.Image = ((System.Drawing.Image)(resources.GetObject("tbBatchDeal.Image")));
            this.tbBatchDeal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbBatchDeal.Name = "tbBatchDeal";
            this.tbBatchDeal.Size = new System.Drawing.Size(69, 53);
            this.tbBatchDeal.Text = "相关医嘱";
            this.tbBatchDeal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbPostOperat
            // 
            this.tbPostOperat.Name = "tbPostOperat";
            this.tbPostOperat.Size = new System.Drawing.Size(124, 22);
            this.tbPostOperat.Text = "术后医嘱";
            // 
            // tbSwitchDept
            // 
            this.tbSwitchDept.Name = "tbSwitchDept";
            this.tbSwitchDept.Size = new System.Drawing.Size(124, 22);
            this.tbSwitchDept.Text = "转科医嘱";
            // 
            // tbLeaveHos
            // 
            this.tbLeaveHos.Name = "tbLeaveHos";
            this.tbLeaveHos.Size = new System.Drawing.Size(124, 22);
            this.tbLeaveHos.Text = "出院医嘱";


            // 
            // tbTreatmentType
            // 
            this.tbTreatmentType.Name = "tbTreatmentType";
            this.tbTreatmentType.Size = new System.Drawing.Size(124, 22);
            this.tbTreatmentType.Text = "修改待遇类型";

            // 
            // tbDead
            // 
            this.tbDead.Name = "tbDead";
            this.tbDead.Size = new System.Drawing.Size(124, 22);
            this.tbDead.Text = "死亡医嘱";

            // 
            // tbPreOperat
            // 
            this.tbPreOperat.Name = "tbPreOperat";
            this.tbPreOperat.Size = new System.Drawing.Size(124, 22);
            this.tbPreOperat.Text = "术前医嘱";
            // 
            // tbLevelOrder
            // 
            this.tbLevelOrder.Image = ((System.Drawing.Image)(resources.GetObject("tbLevelOrder.Image")));
            this.tbLevelOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbLevelOrder.Name = "tbLevelOrder";
            this.tbLevelOrder.Size = new System.Drawing.Size(36, 48);
            this.tbLevelOrder.Text = "层级";
            this.tbLevelOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbLevelOrder.ToolTipText = "层级医嘱";
            // 
            // tbAssayCure
            // 
            this.tbAssayCure.Image = ((System.Drawing.Image)(resources.GetObject("tbAssayCure.Image")));
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
            this.ucOrder1.EnabledPass = true;
            this.ucOrder1.IsFullConvertToHalf = true;
            this.ucOrder1.IsPrint = false;
            this.ucOrder1.IsShowPopMenu = true;
            this.ucOrder1.Location = new System.Drawing.Point(0, 0);
            this.ucOrder1.Name = "ucOrder1";
            this.ucOrder1.PatientType = FS.HISFC.Components.Order.Controls.ReciptPatientType.DeptPatient;
            this.ucOrder1.Size = new System.Drawing.Size(849, 535);
            this.ucOrder1.TabIndex = 0;
            // 
            // tabPage1
            //
            this.tabPage1.Controls.Add(this.ucOrder1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(849, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "医嘱";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucQueryInpatientNo1.InputType = 0;
            //this.ucQueryInpatientNo1.IsDeptOnly = false;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(0, 21);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(168, 540);
            this.ucQueryInpatientNo1.TabIndex = 1;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // tbChooseUL
            // 
            this.tbChooseUL.Image = ((System.Drawing.Image)(resources.GetObject("tbChooseUL.Image")));
            this.tbChooseUL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbChooseUL.Name = "tbChooseUL";
            this.tbChooseUL.Size = new System.Drawing.Size(60, 53);
            this.tbChooseUL.Text = "检验项目";
            this.tbChooseUL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 667);
            this.IsShowToolBar = false;
            this.IsUseDefaultBar = false;
            this.Name = "frmOrder";
            this.Text = "医嘱管理主窗口";
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOrder_FormClosed);
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

        private FS.HISFC.Components.Order.Controls.tvDoctorPatientList tvDoctorPatientList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbComboOrder;
        private System.Windows.Forms.ToolStripButton tbCancelOrder;
        private System.Windows.Forms.ToolStripButton tbExitOrder;
        private System.Windows.Forms.ToolStripButton tb1Exit;
        protected System.Windows.Forms.ToolStripButton tbPackage;
        protected System.Windows.Forms.ToolStripButton tbItem;
        protected System.Windows.Forms.ToolStripButton tbAddOrder;
        protected System.Windows.Forms.ToolStripButton tbMTOrder;
        protected System.Windows.Forms.ToolStripButton tbInfectionReport;
        protected System.Windows.Forms.ToolStripButton tbDelOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tbFilter;
        private System.Windows.Forms.ToolStripMenuItem tbAll;
        private System.Windows.Forms.ToolStripMenuItem tbValid;
        private System.Windows.Forms.ToolStripMenuItem tbInValid;
        private System.Windows.Forms.ToolStripMenuItem tbUCULOrder;
        private System.Windows.Forms.ToolStripMenuItem tbToday;
        private System.Windows.Forms.ToolStripButton tbSaveOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbQueryOrder;
        private System.Windows.Forms.ToolStripButton tbPrintOrder;
        private System.Windows.Forms.ToolStripButton tbRecipePrint;
        private System.Windows.Forms.TabPage tabPage1;
        private FS.HISFC.Components.Order.Controls.ucOrder ucOrder1;
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
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private System.Windows.Forms.ToolStripButton tbDiseaseReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tbLisResultPrint;
        private System.Windows.Forms.ToolStripDropDownButton tbResultPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tbPacsResultPrint;
        private System.Windows.Forms.ToolStripButton tbLevelOrder;
        private System.Windows.Forms.ToolStripButton tbDcAllLongOrder;
        private System.Windows.Forms.ToolStripButton tbEMR;
        private System.Windows.Forms.ToolStripButton tblEditPacsApply;
        private System.Windows.Forms.ToolStripButton tbPass;
        private System.Windows.Forms.ToolStripDropDownButton tbPrintAgain;
        private System.Windows.Forms.ToolStripDropDownButton tbBatchDeal;
        private System.Windows.Forms.ToolStripMenuItem tbPostOperat;
        private System.Windows.Forms.ToolStripMenuItem tbSwitchDept;
        private System.Windows.Forms.ToolStripMenuItem tbLeaveHos;
        private System.Windows.Forms.ToolStripMenuItem tbTreatmentType;
        private System.Windows.Forms.ToolStripMenuItem tbPreOperat;
        private System.Windows.Forms.ToolStripMenuItem tbDead;
        private System.Windows.Forms.ToolStripButton tbChooseUL;
        private System.Windows.Forms.ToolStripButton tbMSG;


    }
}