namespace FS.HISFC.WinForms.DrugStore
{
    partial class frmOutpatientDrug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( frmOutpatientDrug ) );
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefreshWay = new System.Windows.Forms.ToolStripButton();
            this.tsbDrugList = new System.Windows.Forms.ToolStripButton();
            this.tsbRecipe = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMsg = new System.Windows.Forms.ToolStripLabel();
            this.ucClinicTree1 = new FS.HISFC.Components.DrugStore.Outpatient.ucClinicTree();
            this.ucClinicDrug1 = new FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.AccessibleDescription = null;
            this.statusBar1.AccessibleName = null;
            resources.ApplyResources( this.statusBar1, "statusBar1" );
            this.statusBar1.BackgroundImage = null;
            this.statusBar1.Font = null;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AccessibleDescription = null;
            this.splitContainer1.AccessibleName = null;
            resources.ApplyResources( this.splitContainer1, "splitContainer1" );
            this.splitContainer1.BackgroundImage = null;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Font = null;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleDescription = null;
            this.splitContainer1.Panel1.AccessibleName = null;
            resources.ApplyResources( this.splitContainer1.Panel1, "splitContainer1.Panel1" );
            this.splitContainer1.Panel1.BackgroundImage = null;
            this.splitContainer1.Panel1.Controls.Add( this.ucClinicTree1 );
            this.splitContainer1.Panel1.Font = null;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleDescription = null;
            this.splitContainer1.Panel2.AccessibleName = null;
            resources.ApplyResources( this.splitContainer1.Panel2, "splitContainer1.Panel2" );
            this.splitContainer1.Panel2.BackgroundImage = null;
            this.splitContainer1.Panel2.Controls.Add( this.ucClinicDrug1 );
            this.splitContainer1.Panel2.Font = null;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = null;
            this.toolStrip1.AccessibleName = null;
            resources.ApplyResources( this.toolStrip1, "toolStrip1" );
            this.toolStrip1.BackColor = System.Drawing.Color.Honeydew;
            this.toolStrip1.BackgroundImage = null;
            this.toolStrip1.Font = null;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size( 36, 36 );
            this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbQuery,
            this.toolStripSeparator1,
            this.tsbPrint,
            this.tsbPause,
            this.toolStripSeparator2,
            this.tsbRefreshWay,
            this.tsbDrugList,
            this.tsbRecipe,
            this.toolStripSeparator4,
            this.tsbSave,
            this.toolStripSeparator3,
            this.tsbExit,
            this.toolStripSeparator5,
            this.tsMsg} );
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler( this.toolStrip1_ItemClicked );
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.AccessibleDescription = null;
            this.tsbRefresh.AccessibleName = null;
            resources.ApplyResources( this.tsbRefresh, "tsbRefresh" );
            this.tsbRefresh.BackgroundImage = null;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Padding = new System.Windows.Forms.Padding( 5, 0, 5, 0 );
            // 
            // tsbQuery
            // 
            this.tsbQuery.AccessibleDescription = null;
            this.tsbQuery.AccessibleName = null;
            resources.ApplyResources( this.tsbQuery, "tsbQuery" );
            this.tsbQuery.BackgroundImage = null;
            this.tsbQuery.Name = "tsbQuery";
            this.tsbQuery.Padding = new System.Windows.Forms.Padding( 5, 0, 5, 0 );
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AccessibleDescription = null;
            this.toolStripSeparator1.AccessibleName = null;
            resources.ApplyResources( this.toolStripSeparator1, "toolStripSeparator1" );
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // tsbPrint
            // 
            this.tsbPrint.AccessibleDescription = null;
            this.tsbPrint.AccessibleName = null;
            resources.ApplyResources( this.tsbPrint, "tsbPrint" );
            this.tsbPrint.BackgroundImage = null;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Padding = new System.Windows.Forms.Padding( 5, 0, 5, 0 );
            // 
            // tsbPause
            // 
            this.tsbPause.AccessibleDescription = null;
            this.tsbPause.AccessibleName = null;
            resources.ApplyResources( this.tsbPause, "tsbPause" );
            this.tsbPause.BackgroundImage = null;
            this.tsbPause.Name = "tsbPause";
            this.tsbPause.Padding = new System.Windows.Forms.Padding( 5, 0, 5, 0 );
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AccessibleDescription = null;
            this.toolStripSeparator2.AccessibleName = null;
            resources.ApplyResources( this.toolStripSeparator2, "toolStripSeparator2" );
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // tsbRefreshWay
            // 
            this.tsbRefreshWay.AccessibleDescription = null;
            this.tsbRefreshWay.AccessibleName = null;
            resources.ApplyResources( this.tsbRefreshWay, "tsbRefreshWay" );
            this.tsbRefreshWay.BackgroundImage = null;
            this.tsbRefreshWay.Name = "tsbRefreshWay";
            // 
            // tsbDrugList
            // 
            this.tsbDrugList.AccessibleDescription = null;
            this.tsbDrugList.AccessibleName = null;
            resources.ApplyResources( this.tsbDrugList, "tsbDrugList" );
            this.tsbDrugList.BackgroundImage = null;
            this.tsbDrugList.Name = "tsbDrugList";
            // 
            // tsbRecipe
            // 
            this.tsbRecipe.AccessibleDescription = null;
            this.tsbRecipe.AccessibleName = null;
            resources.ApplyResources( this.tsbRecipe, "tsbRecipe" );
            this.tsbRecipe.BackgroundImage = null;
            this.tsbRecipe.Name = "tsbRecipe";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AccessibleDescription = null;
            this.toolStripSeparator4.AccessibleName = null;
            resources.ApplyResources( this.toolStripSeparator4, "toolStripSeparator4" );
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // tsbSave
            // 
            this.tsbSave.AccessibleDescription = null;
            this.tsbSave.AccessibleName = null;
            resources.ApplyResources( this.tsbSave, "tsbSave" );
            this.tsbSave.BackgroundImage = null;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Padding = new System.Windows.Forms.Padding( 5, 0, 5, 0 );
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AccessibleDescription = null;
            this.toolStripSeparator3.AccessibleName = null;
            resources.ApplyResources( this.toolStripSeparator3, "toolStripSeparator3" );
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // tsbExit
            // 
            this.tsbExit.AccessibleDescription = null;
            this.tsbExit.AccessibleName = null;
            resources.ApplyResources( this.tsbExit, "tsbExit" );
            this.tsbExit.BackgroundImage = null;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Padding = new System.Windows.Forms.Padding( 5, 0, 5, 0 );
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AccessibleDescription = null;
            this.toolStripSeparator5.AccessibleName = null;
            resources.ApplyResources( this.toolStripSeparator5, "toolStripSeparator5" );
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // tsMsg
            // 
            this.tsMsg.AccessibleDescription = null;
            this.tsMsg.AccessibleName = null;
            resources.ApplyResources( this.tsMsg, "tsMsg" );
            this.tsMsg.BackgroundImage = null;
            this.tsMsg.ForeColor = System.Drawing.Color.Red;
            this.tsMsg.Name = "tsMsg";
            // 
            // ucClinicTree1
            // 
            this.ucClinicTree1.AccessibleDescription = null;
            this.ucClinicTree1.AccessibleName = null;
            resources.ApplyResources( this.ucClinicTree1, "ucClinicTree1" );
            this.ucClinicTree1.ApproveDept = ((FS.FrameWork.Models.NeuObject)(resources.GetObject( "ucClinicTree1.ApproveDept" )));
            this.ucClinicTree1.BackColor = System.Drawing.Color.FloralWhite;
            this.ucClinicTree1.BackgroundImage = null;
            this.ucClinicTree1.Font = null;
            this.ucClinicTree1.IsAutoPrint = false;
            this.ucClinicTree1.IsFindToAdd = false;
            this.ucClinicTree1.IsPrint = false;
            this.ucClinicTree1.IsShowFeeData = true;
            this.ucClinicTree1.Name = "ucClinicTree1";
            this.ucClinicTree1.OperDept = ((FS.FrameWork.Models.NeuObject)(resources.GetObject( "ucClinicTree1.OperDept" )));
            this.ucClinicTree1.OperInfo = ((FS.FrameWork.Models.NeuObject)(resources.GetObject( "ucClinicTree1.OperInfo" )));
            this.ucClinicTree1.ProcessMessageEvent += new FS.HISFC.Components.DrugStore.Outpatient.ucClinicTree.ProcessMessageHandler( this.ucClinicTree1_ProcessMessageEvent );
            this.ucClinicTree1.MyTreeSelectEvent += new FS.HISFC.Components.DrugStore.Outpatient.ucClinicTree.MyTreeSelectHandler( this.ucClinicTree1_MyTreeSelectEvent );
            // 
            // ucClinicDrug1
            // 
            this.ucClinicDrug1.AccessibleDescription = null;
            this.ucClinicDrug1.AccessibleName = null;
            resources.ApplyResources( this.ucClinicDrug1, "ucClinicDrug1" );
            this.ucClinicDrug1.ApproveDept = ((FS.FrameWork.Models.NeuObject)(resources.GetObject( "ucClinicDrug1.ApproveDept" )));
            this.ucClinicDrug1.BackColor = System.Drawing.Color.FloralWhite;
            this.ucClinicDrug1.BackgroundImage = null;
            this.ucClinicDrug1.Font = null;
            this.ucClinicDrug1.FpBorder = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucClinicDrug1.IsPrint = false;
            this.ucClinicDrug1.LabelBackColor = System.Drawing.Color.FloralWhite;
            this.ucClinicDrug1.Name = "ucClinicDrug1";
            this.ucClinicDrug1.OperDept = ((FS.FrameWork.Models.NeuObject)(resources.GetObject( "ucClinicDrug1.OperDept" )));
            this.ucClinicDrug1.OperInfo = ((FS.FrameWork.Models.NeuObject)(resources.GetObject( "ucClinicDrug1.OperInfo" )));
            this.ucClinicDrug1.EndSave += new System.EventHandler( this.ucClinicDrug1_EndSave );
            // 
            // frmOutpatientDrug
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources( this, "$this" );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add( this.splitContainer1 );
            this.Controls.Add( this.toolStrip1 );
            this.Font = null;
            this.Icon = null;
            this.Name = "frmOutpatientDrug";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex( this.toolStrip1, 0 );
            this.Controls.SetChildIndex( this.statusBar1, 0 );
            this.Controls.SetChildIndex( this.splitContainer1, 0 );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.toolStrip1.ResumeLayout( false );
            this.toolStrip1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPause;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripButton tsbRefreshWay;
        private System.Windows.Forms.ToolStripButton tsbRecipe;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbDrugList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.HISFC.Components.DrugStore.Outpatient.ucClinicTree ucClinicTree1;
        private FS.HISFC.Components.DrugStore.Outpatient.ucClinicDrug ucClinicDrug1;
        private System.Windows.Forms.ToolStripLabel tsMsg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}