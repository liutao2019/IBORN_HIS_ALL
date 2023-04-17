namespace FS.HISFC.Components.Privilege
{
    partial class AuthorizeResourceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( AuthorizeResourceForm ) );
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvRole = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.nRoleMenuStrip = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.AddRoleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyRoleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DelRoleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList( this.components );
            this.nTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.UserRes = new System.Windows.Forms.TabPage();
            this.MenuRes = new System.Windows.Forms.TabPage();
            this.DictionaryRes = new System.Windows.Forms.TabPage();
            this.ReportRes = new System.Windows.Forms.TabPage();
            this.WebRes = new System.Windows.Forms.TabPage();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.nRoleMenuStrip.SuspendLayout();
            this.nTabControl1.SuspendLayout();
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
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BackgroundImage = null;
            this.splitContainer1.Font = null;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleDescription = null;
            this.splitContainer1.Panel1.AccessibleName = null;
            resources.ApplyResources( this.splitContainer1.Panel1, "splitContainer1.Panel1" );
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
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
            this.splitContainer1.Panel2.Controls.Add( this.nTabControl1 );
            this.splitContainer1.Panel2.Font = null;
            // 
            // tvRole
            // 
            this.tvRole.AccessibleDescription = null;
            this.tvRole.AccessibleName = null;
            this.tvRole.AllowDrop = true;
            resources.ApplyResources( this.tvRole, "tvRole" );
            this.tvRole.BackgroundImage = null;
            this.tvRole.ContextMenuStrip = this.nRoleMenuStrip;
            this.tvRole.Font = null;
            this.tvRole.HideSelection = false;
            this.tvRole.ImageList = this.imageList1;
            this.tvRole.Name = "tvRole";
            this.tvRole.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvRole.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tvRole_AfterSelect );
            // 
            // nRoleMenuStrip
            // 
            this.nRoleMenuStrip.AccessibleDescription = null;
            this.nRoleMenuStrip.AccessibleName = null;
            resources.ApplyResources( this.nRoleMenuStrip, "nRoleMenuStrip" );
            this.nRoleMenuStrip.BackgroundImage = null;
            this.nRoleMenuStrip.Font = null;
            this.nRoleMenuStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.AddRoleMenu,
            this.ModifyRoleMenu,
            this.DelRoleMenu} );
            this.nRoleMenuStrip.Name = "nRoleMenuStrip";
            this.nRoleMenuStrip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // AddRoleMenu
            // 
            this.AddRoleMenu.AccessibleDescription = null;
            this.AddRoleMenu.AccessibleName = null;
            resources.ApplyResources( this.AddRoleMenu, "AddRoleMenu" );
            this.AddRoleMenu.BackgroundImage = null;
            this.AddRoleMenu.Name = "AddRoleMenu";
            this.AddRoleMenu.ShortcutKeyDisplayString = null;
            this.AddRoleMenu.Click += new System.EventHandler( this.AddRoleMenu_Click );
            // 
            // ModifyRoleMenu
            // 
            this.ModifyRoleMenu.AccessibleDescription = null;
            this.ModifyRoleMenu.AccessibleName = null;
            resources.ApplyResources( this.ModifyRoleMenu, "ModifyRoleMenu" );
            this.ModifyRoleMenu.BackgroundImage = null;
            this.ModifyRoleMenu.Name = "ModifyRoleMenu";
            this.ModifyRoleMenu.ShortcutKeyDisplayString = null;
            this.ModifyRoleMenu.Click += new System.EventHandler( this.ModifyRoleMenu_Click );
            // 
            // DelRoleMenu
            // 
            this.DelRoleMenu.AccessibleDescription = null;
            this.DelRoleMenu.AccessibleName = null;
            resources.ApplyResources( this.DelRoleMenu, "DelRoleMenu" );
            this.DelRoleMenu.BackgroundImage = null;
            this.DelRoleMenu.Name = "DelRoleMenu";
            this.DelRoleMenu.ShortcutKeyDisplayString = null;
            this.DelRoleMenu.Click += new System.EventHandler( this.DelRoleMenu_Click );
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject( "imageList1.ImageStream" )));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName( 0, "权限24.ico" );
            this.imageList1.Images.SetKeyName( 1, "人员.ico" );
            this.imageList1.Images.SetKeyName( 2, "menu.gif" );
            this.imageList1.Images.SetKeyName( 3, "purview.gif" );
            this.imageList1.Images.SetKeyName( 4, "nonpurview.gif" );
            this.imageList1.Images.SetKeyName( 5, "roleparent.gif" );
            this.imageList1.Images.SetKeyName( 6, "folder.gif" );
            // 
            // nTabControl1
            // 
            this.nTabControl1.AccessibleDescription = null;
            this.nTabControl1.AccessibleName = null;
            resources.ApplyResources( this.nTabControl1, "nTabControl1" );
            this.nTabControl1.BackgroundImage = null;
            this.nTabControl1.Controls.Add( this.UserRes );
            this.nTabControl1.Controls.Add( this.MenuRes );
            this.nTabControl1.Controls.Add( this.DictionaryRes );
            this.nTabControl1.Controls.Add( this.ReportRes );
            this.nTabControl1.Controls.Add( this.WebRes );
            this.nTabControl1.Font = null;
            this.nTabControl1.Name = "nTabControl1";
            this.nTabControl1.SelectedIndex = 0;
            this.nTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.nTabControl1.SelectedIndexChanged += new System.EventHandler( this.nTabControl1_SelectedIndexChanged );
            // 
            // UserRes
            // 
            this.UserRes.AccessibleDescription = null;
            this.UserRes.AccessibleName = null;
            resources.ApplyResources( this.UserRes, "UserRes" );
            this.UserRes.BackgroundImage = null;
            this.UserRes.Font = null;
            this.UserRes.Name = "UserRes";
            this.UserRes.UseVisualStyleBackColor = true;
            // 
            // MenuRes
            // 
            this.MenuRes.AccessibleDescription = null;
            this.MenuRes.AccessibleName = null;
            resources.ApplyResources( this.MenuRes, "MenuRes" );
            this.MenuRes.BackgroundImage = null;
            this.MenuRes.Font = null;
            this.MenuRes.Name = "MenuRes";
            this.MenuRes.UseVisualStyleBackColor = true;
            // 
            // DictionaryRes
            // 
            this.DictionaryRes.AccessibleDescription = null;
            this.DictionaryRes.AccessibleName = null;
            resources.ApplyResources( this.DictionaryRes, "DictionaryRes" );
            this.DictionaryRes.BackgroundImage = null;
            this.DictionaryRes.Font = null;
            this.DictionaryRes.Name = "DictionaryRes";
            this.DictionaryRes.UseVisualStyleBackColor = true;
            // 
            // ReportRes
            // 
            this.ReportRes.AccessibleDescription = null;
            this.ReportRes.AccessibleName = null;
            resources.ApplyResources( this.ReportRes, "ReportRes" );
            this.ReportRes.BackgroundImage = null;
            this.ReportRes.Font = null;
            this.ReportRes.Name = "ReportRes";
            this.ReportRes.UseVisualStyleBackColor = true;
            // 
            // WebRes
            // 
            this.WebRes.AccessibleDescription = null;
            this.WebRes.AccessibleName = null;
            resources.ApplyResources( this.WebRes, "WebRes" );
            this.WebRes.BackgroundImage = null;
            this.WebRes.Font = null;
            this.WebRes.Name = "WebRes";
            this.WebRes.UseVisualStyleBackColor = true;
            // 
            // AuthorizeResourceForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources( this, "$this" );
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = null;
            this.Controls.Add( this.splitContainer1 );
            this.Font = null;
            this.Icon = null;
            this.Name = "AuthorizeResourceForm";
            this.Controls.SetChildIndex( this.statusBar1, 0 );
            this.Controls.SetChildIndex( this.splitContainer1, 0 );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.nRoleMenuStrip.ResumeLayout( false );
            this.nTabControl1.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FrameWork.WinForms.Controls.NeuTabControl nTabControl1;
        private System.Windows.Forms.TabPage UserRes;
        private System.Windows.Forms.TabPage MenuRes;
        private System.Windows.Forms.TabPage DictionaryRes;
        private System.Windows.Forms.TabPage ReportRes;
        private System.Windows.Forms.ImageList imageList1;
        private FrameWork.WinForms.Controls.NeuTreeView tvRole;
        private FrameWork.WinForms.Controls.NeuContextMenuStrip nRoleMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AddRoleMenu;
        private System.Windows.Forms.ToolStripMenuItem ModifyRoleMenu;
        private System.Windows.Forms.ToolStripMenuItem DelRoleMenu;
        private System.Windows.Forms.TabPage WebRes;
    }
}