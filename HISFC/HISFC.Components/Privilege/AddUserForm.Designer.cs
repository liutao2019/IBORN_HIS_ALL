namespace FS.HISFC.Components.Privilege
{
    partial class AddUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( AddUserForm ) );
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvRole = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.nTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpBaseInfo = new System.Windows.Forms.TabPage();
            this.chbManager = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chbOriginPass = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chbLock = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btnSelectUser = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtAccount = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtUserName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.nLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tpRoleInfo = new System.Windows.Forms.TabPage();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnChanel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnDeleteUser = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ContentPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.nTabControl1.SuspendLayout();
            this.tpBaseInfo.SuspendLayout();
            this.tpRoleInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.AccessibleDescription = null;
            this.ContentPanel.AccessibleName = null;
            resources.ApplyResources( this.ContentPanel, "ContentPanel" );
            this.ContentPanel.BackgroundImage = null;
            this.ContentPanel.Controls.Add( this.nTabControl1 );
            this.ContentPanel.Font = null;
            this.ContentPanel.Controls.SetChildIndex( this.nTabControl1, 0 );
            // 
            // TitlePanel
            // 
            this.TitlePanel.AccessibleDescription = null;
            this.TitlePanel.AccessibleName = null;
            resources.ApplyResources( this.TitlePanel, "TitlePanel" );
            this.TitlePanel.BackgroundImage = null;
            this.TitlePanel.Font = null;
            // 
            // BottomPanel
            // 
            this.BottomPanel.AccessibleDescription = null;
            this.BottomPanel.AccessibleName = null;
            resources.ApplyResources( this.BottomPanel, "BottomPanel" );
            this.BottomPanel.BackgroundImage = null;
            this.BottomPanel.Controls.Add( this.btnDeleteUser );
            this.BottomPanel.Controls.Add( this.btnChanel );
            this.BottomPanel.Controls.Add( this.btnSave );
            this.BottomPanel.Font = null;
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
            this.splitContainer1.Font = null;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleDescription = null;
            this.splitContainer1.Panel1.AccessibleName = null;
            resources.ApplyResources( this.splitContainer1.Panel1, "splitContainer1.Panel1" );
            this.splitContainer1.Panel1.BackgroundImage = null;
            this.splitContainer1.Panel1.Controls.Add( this.tvRole );
            this.splitContainer1.Panel1.Font = null;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleDescription = null;
            this.splitContainer1.Panel2.AccessibleName = null;
            resources.ApplyResources( this.splitContainer1.Panel2, "splitContainer1.Panel2" );
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.BackgroundImage = null;
            this.splitContainer1.Panel2.Font = null;
            // 
            // tvRole
            // 
            this.tvRole.AccessibleDescription = null;
            this.tvRole.AccessibleName = null;
            this.tvRole.AllowDrop = true;
            resources.ApplyResources( this.tvRole, "tvRole" );
            this.tvRole.BackgroundImage = null;
            this.tvRole.CheckBoxes = true;
            this.tvRole.Font = null;
            this.tvRole.HideSelection = false;
            this.tvRole.Name = "tvRole";
            this.tvRole.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvRole.AfterCheck += new System.Windows.Forms.TreeViewEventHandler( this.tvRole_AfterCheck );
            this.tvRole.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tvRole_AfterSelect );
            // 
            // nTabControl1
            // 
            this.nTabControl1.AccessibleDescription = null;
            this.nTabControl1.AccessibleName = null;
            resources.ApplyResources( this.nTabControl1, "nTabControl1" );
            this.nTabControl1.BackgroundImage = null;
            this.nTabControl1.Controls.Add( this.tpBaseInfo );
            this.nTabControl1.Controls.Add( this.tpRoleInfo );
            this.nTabControl1.Font = null;
            this.nTabControl1.Name = "nTabControl1";
            this.nTabControl1.SelectedIndex = 0;
            this.nTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nTabControl1.TabIndexChanged += new System.EventHandler( this.nTabControl1_TabIndexChanged );
            this.nTabControl1.SelectedIndexChanged += new System.EventHandler( this.nTabControl1_TabIndexChanged );
            // 
            // tpBaseInfo
            // 
            this.tpBaseInfo.AccessibleDescription = null;
            this.tpBaseInfo.AccessibleName = null;
            resources.ApplyResources( this.tpBaseInfo, "tpBaseInfo" );
            this.tpBaseInfo.BackgroundImage = null;
            this.tpBaseInfo.Controls.Add( this.chbManager );
            this.tpBaseInfo.Controls.Add( this.chbOriginPass );
            this.tpBaseInfo.Controls.Add( this.chbLock );
            this.tpBaseInfo.Controls.Add( this.btnSelectUser );
            this.tpBaseInfo.Controls.Add( this.txtMemo );
            this.tpBaseInfo.Controls.Add( this.txtAccount );
            this.tpBaseInfo.Controls.Add( this.txtUserName );
            this.tpBaseInfo.Controls.Add( this.nLabel6 );
            this.tpBaseInfo.Controls.Add( this.nLabel3 );
            this.tpBaseInfo.Controls.Add( this.nLabel2 );
            this.tpBaseInfo.Font = null;
            this.tpBaseInfo.Name = "tpBaseInfo";
            this.tpBaseInfo.UseVisualStyleBackColor = true;
            // 
            // chbManager
            // 
            this.chbManager.AccessibleDescription = null;
            this.chbManager.AccessibleName = null;
            resources.ApplyResources( this.chbManager, "chbManager" );
            this.chbManager.BackgroundImage = null;
            this.chbManager.Font = null;
            this.chbManager.Name = "chbManager";
            this.chbManager.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chbManager.UseVisualStyleBackColor = true;
            // 
            // chbOriginPass
            // 
            this.chbOriginPass.AccessibleDescription = null;
            this.chbOriginPass.AccessibleName = null;
            resources.ApplyResources( this.chbOriginPass, "chbOriginPass" );
            this.chbOriginPass.BackgroundImage = null;
            this.chbOriginPass.Checked = true;
            this.chbOriginPass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOriginPass.Font = null;
            this.chbOriginPass.Name = "chbOriginPass";
            this.chbOriginPass.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chbOriginPass.UseVisualStyleBackColor = true;
            // 
            // chbLock
            // 
            this.chbLock.AccessibleDescription = null;
            this.chbLock.AccessibleName = null;
            resources.ApplyResources( this.chbLock, "chbLock" );
            this.chbLock.BackgroundImage = null;
            this.chbLock.Font = null;
            this.chbLock.Name = "chbLock";
            this.chbLock.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chbLock.UseVisualStyleBackColor = true;
            // 
            // btnSelectUser
            // 
            this.btnSelectUser.AccessibleDescription = null;
            this.btnSelectUser.AccessibleName = null;
            resources.ApplyResources( this.btnSelectUser, "btnSelectUser" );
            this.btnSelectUser.BackgroundImage = null;
            this.btnSelectUser.Font = null;
            this.btnSelectUser.Name = "btnSelectUser";
            this.btnSelectUser.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSelectUser.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSelectUser.UseVisualStyleBackColor = true;
            this.btnSelectUser.Click += new System.EventHandler( this.btnSelectUser_Click );
            // 
            // txtMemo
            // 
            this.txtMemo.AccessibleDescription = null;
            this.txtMemo.AccessibleName = null;
            resources.ApplyResources( this.txtMemo, "txtMemo" );
            this.txtMemo.BackgroundImage = null;
            this.txtMemo.Font = null;
            this.txtMemo.IsEnter2Tab = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // txtAccount
            // 
            this.txtAccount.AccessibleDescription = null;
            this.txtAccount.AccessibleName = null;
            resources.ApplyResources( this.txtAccount, "txtAccount" );
            this.txtAccount.BackgroundImage = null;
            this.txtAccount.Font = null;
            this.txtAccount.IsEnter2Tab = true;
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // txtUserName
            // 
            this.txtUserName.AccessibleDescription = null;
            this.txtUserName.AccessibleName = null;
            resources.ApplyResources( this.txtUserName, "txtUserName" );
            this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.BackgroundImage = null;
            this.txtUserName.Font = null;
            this.txtUserName.IsEnter2Tab = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // nLabel6
            // 
            this.nLabel6.AccessibleDescription = null;
            this.nLabel6.AccessibleName = null;
            resources.ApplyResources( this.nLabel6, "nLabel6" );
            this.nLabel6.Font = null;
            this.nLabel6.Name = "nLabel6";
            this.nLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // nLabel3
            // 
            this.nLabel3.AccessibleDescription = null;
            this.nLabel3.AccessibleName = null;
            resources.ApplyResources( this.nLabel3, "nLabel3" );
            this.nLabel3.Font = null;
            this.nLabel3.Name = "nLabel3";
            this.nLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // nLabel2
            // 
            this.nLabel2.AccessibleDescription = null;
            this.nLabel2.AccessibleName = null;
            resources.ApplyResources( this.nLabel2, "nLabel2" );
            this.nLabel2.Font = null;
            this.nLabel2.Name = "nLabel2";
            this.nLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // tpRoleInfo
            // 
            this.tpRoleInfo.AccessibleDescription = null;
            this.tpRoleInfo.AccessibleName = null;
            resources.ApplyResources( this.tpRoleInfo, "tpRoleInfo" );
            this.tpRoleInfo.BackgroundImage = null;
            this.tpRoleInfo.Controls.Add( this.splitContainer1 );
            this.tpRoleInfo.Font = null;
            this.tpRoleInfo.Name = "tpRoleInfo";
            this.tpRoleInfo.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleDescription = null;
            this.btnSave.AccessibleName = null;
            resources.ApplyResources( this.btnSave, "btnSave" );
            this.btnSave.BackgroundImage = null;
            this.btnSave.Font = null;
            this.btnSave.Name = "btnSave";
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
            // 
            // btnChanel
            // 
            this.btnChanel.AccessibleDescription = null;
            this.btnChanel.AccessibleName = null;
            resources.ApplyResources( this.btnChanel, "btnChanel" );
            this.btnChanel.BackgroundImage = null;
            this.btnChanel.Font = null;
            this.btnChanel.Name = "btnChanel";
            this.btnChanel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnChanel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnChanel.UseVisualStyleBackColor = true;
            this.btnChanel.Click += new System.EventHandler( this.btnChanel_Click );
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.AccessibleDescription = null;
            this.btnDeleteUser.AccessibleName = null;
            resources.ApplyResources( this.btnDeleteUser, "btnDeleteUser" );
            this.btnDeleteUser.BackgroundImage = null;
            this.btnDeleteUser.Font = null;
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnDeleteUser.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler( this.btnDeleteUser_Click );
            // 
            // AddUserForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources( this, "$this" );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Font = null;
            this.Icon = null;
            this.Name = "AddUserForm";
            this.Load += new System.EventHandler( this.AddUserForm_Load );
            this.ContentPanel.ResumeLayout( false );
            this.BottomPanel.ResumeLayout( false );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.nTabControl1.ResumeLayout( false );
            this.tpBaseInfo.ResumeLayout( false );
            this.tpBaseInfo.PerformLayout();
            this.tpRoleInfo.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private FrameWork.WinForms.Controls.NeuTabControl nTabControl1;
        private System.Windows.Forms.TabPage tpBaseInfo;
        private System.Windows.Forms.TabPage tpRoleInfo;
        private FrameWork.WinForms.Controls.NeuCheckBox chbOriginPass;
        private FrameWork.WinForms.Controls.NeuCheckBox chbLock;
        private FrameWork.WinForms.Controls.NeuButton btnSelectUser;
        private FrameWork.WinForms.Controls.NeuTextBox txtMemo;
        private FrameWork.WinForms.Controls.NeuTextBox txtAccount;
        private FrameWork.WinForms.Controls.NeuTextBox txtUserName;
        private FrameWork.WinForms.Controls.NeuLabel nLabel6;
        private FrameWork.WinForms.Controls.NeuLabel nLabel3;
        private FrameWork.WinForms.Controls.NeuLabel nLabel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FrameWork.WinForms.Controls.NeuTreeView tvRole;
        private FrameWork.WinForms.Controls.NeuButton btnChanel;
        private FrameWork.WinForms.Controls.NeuButton btnSave;
        private FrameWork.WinForms.Controls.NeuButton btnDeleteUser;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chbManager;
    }
}