namespace FS.HISFC.Components.DrugStore.Outpatient
{
    partial class ucClinicTree
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
            if (this.processRefreshTimer != null)
                this.processRefreshTimer.Dispose();
            if (this.ledRefreshTimer != null)
                this.ledRefreshTimer.Dispose();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ucClinicTree ) );
            this.lbnBillType = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtBillNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.operName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panelSendTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbFeeDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpPrinted = new System.Windows.Forms.TabPage();
            this.tvPrinted = new FS.HISFC.Components.DrugStore.Outpatient.tvClinicTree( this.components );
            this.tpPrinting = new System.Windows.Forms.TabPage();
            this.tvPrinting = new FS.HISFC.Components.DrugStore.Outpatient.tvClinicTree( this.components );
            this.tpDruged = new System.Windows.Forms.TabPage();
            this.tvDruged = new FS.HISFC.Components.DrugStore.Outpatient.tvClinicTree( this.components );
            this.tpSend = new System.Windows.Forms.TabPage();
            this.tvSend = new FS.HISFC.Components.DrugStore.Outpatient.tvClinicTree( this.components );
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.panelSendTime.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tpPrinted.SuspendLayout();
            this.tpPrinting.SuspendLayout();
            this.tpDruged.SuspendLayout();
            this.tpSend.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbnBillType
            // 
            this.lbnBillType.AccessibleDescription = null;
            this.lbnBillType.AccessibleName = null;
            resources.ApplyResources( this.lbnBillType, "lbnBillType" );
            this.lbnBillType.Font = null;
            this.lbnBillType.Name = "lbnBillType";
            this.lbnBillType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbnBillType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lbnBillType_LinkClicked );
            // 
            // neuPanel1
            // 
            this.neuPanel1.AccessibleDescription = null;
            this.neuPanel1.AccessibleName = null;
            resources.ApplyResources( this.neuPanel1, "neuPanel1" );
            this.neuPanel1.BackgroundImage = null;
            this.neuPanel1.Controls.Add( this.txtBillNO );
            this.neuPanel1.Controls.Add( this.lbnBillType );
            this.neuPanel1.Font = null;
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // txtBillNO
            // 
            this.txtBillNO.AccessibleDescription = null;
            this.txtBillNO.AccessibleName = null;
            resources.ApplyResources( this.txtBillNO, "txtBillNO" );
            this.txtBillNO.BackgroundImage = null;
            this.txtBillNO.Font = null;
            this.txtBillNO.IsEnter2Tab = false;
            this.txtBillNO.Name = "txtBillNO";
            this.txtBillNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBillNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.txtBillNO_KeyPress );
            // 
            // neuPanel2
            // 
            this.neuPanel2.AccessibleDescription = null;
            this.neuPanel2.AccessibleName = null;
            resources.ApplyResources( this.neuPanel2, "neuPanel2" );
            this.neuPanel2.BackgroundImage = null;
            this.neuPanel2.Controls.Add( this.operName );
            this.neuPanel2.Controls.Add( this.txtPID );
            this.neuPanel2.Controls.Add( this.neuLabel2 );
            this.neuPanel2.Controls.Add( this.neuLabel1 );
            this.neuPanel2.Controls.Add( this.panelSendTime );
            this.neuPanel2.Font = null;
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // operName
            // 
            this.operName.AccessibleDescription = null;
            this.operName.AccessibleName = null;
            resources.ApplyResources( this.operName, "operName" );
            this.operName.Font = null;
            this.operName.Name = "operName";
            this.operName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // txtPID
            // 
            this.txtPID.AccessibleDescription = null;
            this.txtPID.AccessibleName = null;
            resources.ApplyResources( this.txtPID, "txtPID" );
            this.txtPID.BackgroundImage = null;
            this.txtPID.Font = null;
            this.txtPID.IsEnter2Tab = false;
            this.txtPID.Name = "txtPID";
            this.txtPID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPID.KeyDown += new System.Windows.Forms.KeyEventHandler( this.txtPID_KeyDown );
            this.txtPID.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.txtPID_KeyPress );
            // 
            // neuLabel2
            // 
            this.neuLabel2.AccessibleDescription = null;
            this.neuLabel2.AccessibleName = null;
            resources.ApplyResources( this.neuLabel2, "neuLabel2" );
            this.neuLabel2.Font = null;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AccessibleDescription = null;
            this.neuLabel1.AccessibleName = null;
            resources.ApplyResources( this.neuLabel1, "neuLabel1" );
            this.neuLabel1.Font = null;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // panelSendTime
            // 
            this.panelSendTime.AccessibleDescription = null;
            this.panelSendTime.AccessibleName = null;
            resources.ApplyResources( this.panelSendTime, "panelSendTime" );
            this.panelSendTime.BackgroundImage = null;
            this.panelSendTime.Controls.Add( this.lbFeeDate );
            this.panelSendTime.Font = null;
            this.panelSendTime.Name = "panelSendTime";
            this.panelSendTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // lbFeeDate
            // 
            this.lbFeeDate.AccessibleDescription = null;
            this.lbFeeDate.AccessibleName = null;
            resources.ApplyResources( this.lbFeeDate, "lbFeeDate" );
            this.lbFeeDate.ForeColor = System.Drawing.Color.Blue;
            this.lbFeeDate.Name = "lbFeeDate";
            this.lbFeeDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.AccessibleDescription = null;
            this.neuTabControl1.AccessibleName = null;
            resources.ApplyResources( this.neuTabControl1, "neuTabControl1" );
            this.neuTabControl1.BackgroundImage = null;
            this.neuTabControl1.Controls.Add( this.tpPrinted );
            this.neuTabControl1.Controls.Add( this.tpPrinting );
            this.neuTabControl1.Controls.Add( this.tpDruged );
            this.neuTabControl1.Controls.Add( this.tpSend );
            this.neuTabControl1.Font = null;
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.SelectedIndexChanged += new System.EventHandler( this.neuTabControl1_SelectedIndexChanged );
            // 
            // tpPrinted
            // 
            this.tpPrinted.AccessibleDescription = null;
            this.tpPrinted.AccessibleName = null;
            resources.ApplyResources( this.tpPrinted, "tpPrinted" );
            this.tpPrinted.BackgroundImage = null;
            this.tpPrinted.Controls.Add( this.tvPrinted );
            this.tpPrinted.Font = null;
            this.tpPrinted.Name = "tpPrinted";
            this.tpPrinted.UseVisualStyleBackColor = true;
            // 
            // tvPrinted
            // 
            this.tvPrinted.AccessibleDescription = null;
            this.tvPrinted.AccessibleName = null;
            resources.ApplyResources( this.tvPrinted, "tvPrinted" );
            this.tvPrinted.BackgroundImage = null;
            this.tvPrinted.Font = null;
            this.tvPrinted.HideSelection = false;
            this.tvPrinted.Name = "tvPrinted";
            this.tvPrinted.State = "0";
            this.tvPrinted.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvPrinted.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tv_AfterSelect );
            // 
            // tpPrinting
            // 
            this.tpPrinting.AccessibleDescription = null;
            this.tpPrinting.AccessibleName = null;
            resources.ApplyResources( this.tpPrinting, "tpPrinting" );
            this.tpPrinting.BackgroundImage = null;
            this.tpPrinting.Controls.Add( this.tvPrinting );
            this.tpPrinting.Font = null;
            this.tpPrinting.Name = "tpPrinting";
            this.tpPrinting.UseVisualStyleBackColor = true;
            // 
            // tvPrinting
            // 
            this.tvPrinting.AccessibleDescription = null;
            this.tvPrinting.AccessibleName = null;
            resources.ApplyResources( this.tvPrinting, "tvPrinting" );
            this.tvPrinting.BackgroundImage = null;
            this.tvPrinting.Font = null;
            this.tvPrinting.HideSelection = false;
            this.tvPrinting.Name = "tvPrinting";
            this.tvPrinting.State = "0";
            this.tvPrinting.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvPrinting.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tv_AfterSelect );
            // 
            // tpDruged
            // 
            this.tpDruged.AccessibleDescription = null;
            this.tpDruged.AccessibleName = null;
            resources.ApplyResources( this.tpDruged, "tpDruged" );
            this.tpDruged.BackgroundImage = null;
            this.tpDruged.Controls.Add( this.tvDruged );
            this.tpDruged.Font = null;
            this.tpDruged.Name = "tpDruged";
            this.tpDruged.UseVisualStyleBackColor = true;
            // 
            // tvDruged
            // 
            this.tvDruged.AccessibleDescription = null;
            this.tvDruged.AccessibleName = null;
            resources.ApplyResources( this.tvDruged, "tvDruged" );
            this.tvDruged.BackgroundImage = null;
            this.tvDruged.Font = null;
            this.tvDruged.HideSelection = false;
            this.tvDruged.Name = "tvDruged";
            this.tvDruged.State = "0";
            this.tvDruged.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvDruged.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tv_AfterSelect );
            // 
            // tpSend
            // 
            this.tpSend.AccessibleDescription = null;
            this.tpSend.AccessibleName = null;
            resources.ApplyResources( this.tpSend, "tpSend" );
            this.tpSend.BackgroundImage = null;
            this.tpSend.Controls.Add( this.tvSend );
            this.tpSend.Font = null;
            this.tpSend.Name = "tpSend";
            this.tpSend.UseVisualStyleBackColor = true;
            // 
            // tvSend
            // 
            this.tvSend.AccessibleDescription = null;
            this.tvSend.AccessibleName = null;
            resources.ApplyResources( this.tvSend, "tvSend" );
            this.tvSend.BackgroundImage = null;
            this.tvSend.Font = null;
            this.tvSend.HideSelection = false;
            this.tvSend.Name = "tvSend";
            this.tvSend.State = "0";
            this.tvSend.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvSend.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tv_AfterSelect );
            // 
            // ucClinicTree
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources( this, "$this" );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add( this.neuTabControl1 );
            this.Controls.Add( this.neuPanel2 );
            this.Controls.Add( this.neuPanel1 );
            this.Font = null;
            this.Name = "ucClinicTree";
            this.neuPanel1.ResumeLayout( false );
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout( false );
            this.neuPanel2.PerformLayout();
            this.panelSendTime.ResumeLayout( false );
            this.neuTabControl1.ResumeLayout( false );
            this.tpPrinted.ResumeLayout( false );
            this.tpPrinting.ResumeLayout( false );
            this.tpDruged.ResumeLayout( false );
            this.tpSend.ResumeLayout( false );
            this.ResumeLayout( false );

        }
       
        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLinkLabel lbnBillType;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBillNO;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel panelSendTime;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPID;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel operName;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpPrinted;
        private System.Windows.Forms.TabPage tpPrinting;
        private System.Windows.Forms.TabPage tpDruged;
        private System.Windows.Forms.TabPage tpSend;
        private tvClinicTree tvPrinted;
        private tvClinicTree tvPrinting;
        private tvClinicTree tvDruged;
        private tvClinicTree tvSend;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbFeeDate;
    }
}
