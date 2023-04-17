namespace FS.SOC.HISFC.Components.DCP
{
    partial class frmReportManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( frmReportManager ) );
            this.imageList1 = new System.Windows.Forms.ImageList( this.components );
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbNew = new System.Windows.Forms.ToolStripButton();
            this.tbQuery = new System.Windows.Forms.ToolStripButton();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.tbCorrect = new System.Windows.Forms.ToolStripButton();
            this.tbEligible = new System.Windows.Forms.ToolStripButton();
            this.tbUnEligible = new System.Windows.Forms.ToolStripButton();
            this.tbCancel = new System.Windows.Forms.ToolStripButton();
            this.tbDelete = new System.Windows.Forms.ToolStripButton();
            this.tbClear = new System.Windows.Forms.ToolStripButton();
            this.tbExit = new System.Windows.Forms.ToolStripButton();
            this.panelFill = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject( "imageList1.ImageStream" )));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName( 0, "新建24.ico" );
            this.imageList1.Images.SetKeyName( 1, "查询.ico" );
            this.imageList1.Images.SetKeyName( 2, "保存.ico" );
            this.imageList1.Images.SetKeyName( 3, "下一个.ico" );
            this.imageList1.Images.SetKeyName( 4, "执行24.ico" );
            this.imageList1.Images.SetKeyName( 5, "作废信息24.ico" );
            this.imageList1.Images.SetKeyName( 6, "删除.ico" );
            this.imageList1.Images.SetKeyName( 7, "清空24.ico" );
            this.imageList1.Images.SetKeyName( 8, "退出.ico" );
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Honeydew;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size( 32, 32 );
            this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tbNew,
            this.toolStripSeparator1,
            this.tbQuery,
            this.tbSave,
            this.tbCorrect,
            this.tbEligible,
            this.tbUnEligible,
            this.tbCancel,
            this.tbDelete,
            this.tbClear,
            this.tbExit} );
            this.toolStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size( 687, 56 );
            this.toolStrip1.TabIndex = 2;
            
            // 
            // panelFill
            // 
            this.panelFill.BackColor = System.Drawing.Color.White;
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point( 0, 56 );
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size( 687, 414 );
            this.panelFill.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelFill.TabIndex = 3;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 6, 56 );
            // 
            // frmReportManager
            // 
            this.ClientSize = new System.Drawing.Size( 687, 470 );
            this.Controls.Add( this.panelFill );
            this.Controls.Add( this.toolStrip1 );
            this.KeyPreview = true;
            this.Name = "frmReportManager";
            this.Text = "疾病控制预防管理";
            this.toolStrip1.ResumeLayout( false );
            this.toolStrip1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbNew;
        private System.Windows.Forms.ToolStripButton tbQuery;
        private System.Windows.Forms.ToolStripButton tbSave;
        private System.Windows.Forms.ToolStripButton tbEligible;
        private System.Windows.Forms.ToolStripButton tbCorrect;
        private System.Windows.Forms.ToolStripButton tbUnEligible;
        private System.Windows.Forms.ToolStripButton tbCancel;
        private System.Windows.Forms.ToolStripButton tbDelete;
        private System.Windows.Forms.ToolStripButton tbClear;
        private System.Windows.Forms.ToolStripButton tbExit;
        private FS.FrameWork.WinForms.Controls.NeuPanel panelFill;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;





    }
}
