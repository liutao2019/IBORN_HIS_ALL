namespace FS.HISFC.Components.Privilege
{
    partial class AuthorizeUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( AuthorizeUserControl ) );
            System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            this.nTreeListView1 = new FS.FrameWork.WinForms.Controls.NeuTreeListView( this.components );
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.cmpUser = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.tmspAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tmspModify = new System.Windows.Forms.ToolStripMenuItem();
            this.tmspDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList( this.components );
            this.cmpUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // nTreeListView1
            // 
            this.nTreeListView1.AccessibleDescription = null;
            this.nTreeListView1.AccessibleName = null;
            resources.ApplyResources( this.nTreeListView1, "nTreeListView1" );
            this.nTreeListView1.BackgroundImage = null;
            this.nTreeListView1.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5} );
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.nTreeListView1.Comparer = treeListViewItemCollectionComparer1;
            this.nTreeListView1.ContextMenuStrip = this.cmpUser;
            this.nTreeListView1.Font = null;
            this.nTreeListView1.Name = "nTreeListView1";
            this.nTreeListView1.SmallImageList = this.imageList1;
            this.nTreeListView1.UseCompatibleStateImageBehavior = false;
            this.nTreeListView1.DoubleClick += new System.EventHandler( this.nTreeListView1_DoubleClick );
            // 
            // columnHeader1
            // 
            resources.ApplyResources( this.columnHeader1, "columnHeader1" );
            // 
            // columnHeader2
            // 
            resources.ApplyResources( this.columnHeader2, "columnHeader2" );
            // 
            // columnHeader3
            // 
            resources.ApplyResources( this.columnHeader3, "columnHeader3" );
            // 
            // columnHeader4
            // 
            resources.ApplyResources( this.columnHeader4, "columnHeader4" );
            // 
            // columnHeader5
            // 
            resources.ApplyResources( this.columnHeader5, "columnHeader5" );
            // 
            // cmpUser
            // 
            this.cmpUser.AccessibleDescription = null;
            this.cmpUser.AccessibleName = null;
            resources.ApplyResources( this.cmpUser, "cmpUser" );
            this.cmpUser.BackgroundImage = null;
            this.cmpUser.Font = null;
            this.cmpUser.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tmspAdd,
            this.tmspModify,
            this.tmspDelete} );
            this.cmpUser.Name = "cmpUser";
            this.cmpUser.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // tmspAdd
            // 
            this.tmspAdd.AccessibleDescription = null;
            this.tmspAdd.AccessibleName = null;
            resources.ApplyResources( this.tmspAdd, "tmspAdd" );
            this.tmspAdd.BackgroundImage = null;
            this.tmspAdd.Name = "tmspAdd";
            this.tmspAdd.ShortcutKeyDisplayString = null;
            this.tmspAdd.Click += new System.EventHandler( this.tmspAdd_Click );
            // 
            // tmspModify
            // 
            this.tmspModify.AccessibleDescription = null;
            this.tmspModify.AccessibleName = null;
            resources.ApplyResources( this.tmspModify, "tmspModify" );
            this.tmspModify.BackgroundImage = null;
            this.tmspModify.Name = "tmspModify";
            this.tmspModify.ShortcutKeyDisplayString = null;
            this.tmspModify.Click += new System.EventHandler( this.tmspModify_Click );
            // 
            // tmspDelete
            // 
            this.tmspDelete.AccessibleDescription = null;
            this.tmspDelete.AccessibleName = null;
            resources.ApplyResources( this.tmspDelete, "tmspDelete" );
            this.tmspDelete.BackgroundImage = null;
            this.tmspDelete.Name = "tmspDelete";
            this.tmspDelete.ShortcutKeyDisplayString = null;
            this.tmspDelete.Click += new System.EventHandler( this.tmspDelete_Click );
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject( "imageList1.ImageStream" )));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName( 0, "user_gray.png" );
            // 
            // AuthorizeUserControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources( this, "$this" );
            this.BackgroundImage = null;
            this.Controls.Add( this.nTreeListView1 );
            this.Font = null;
            this.Name = "AuthorizeUserControl";
            this.Load += new System.EventHandler( this.AuthorizeUserControl_Load );
            this.cmpUser.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private FrameWork.WinForms.Controls.NeuTreeListView nTreeListView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ImageList imageList1;
        private FrameWork.WinForms.Controls.NeuContextMenuStrip cmpUser;
        private System.Windows.Forms.ToolStripMenuItem tmspAdd;
        private System.Windows.Forms.ToolStripMenuItem tmspModify;
        private System.Windows.Forms.ToolStripMenuItem tmspDelete;
    }
}
