namespace FS.SOC.HISFC.Components.DCP
{
    partial class frmReportManagerClinic
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
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
            this.imageList1.Images.SetKeyName( 0, "�½�24.ico" );
            this.imageList1.Images.SetKeyName( 1, "��ѯ.ico" );
            this.imageList1.Images.SetKeyName( 2, "����.ico" );
            this.imageList1.Images.SetKeyName( 3, "��һ��.ico" );
            this.imageList1.Images.SetKeyName( 4, "ִ��24.ico" );
            this.imageList1.Images.SetKeyName( 5, "������Ϣ24.ico" );
            this.imageList1.Images.SetKeyName( 6, "ɾ��.ico" );
            this.imageList1.Images.SetKeyName( 7, "���24.ico" );
            this.imageList1.Images.SetKeyName( 8, "�˳�.ico" );
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
            // tbNew
            // 
            this.tbNew.Image = ((System.Drawing.Image)(resources.GetObject( "tbNew.Image" )));
            this.tbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size( 36, 53 );
            this.tbNew.Text = "�½�";
            this.tbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbQuery
            // 
            this.tbQuery.Image = ((System.Drawing.Image)(resources.GetObject( "tbQuery.Image" )));
            this.tbQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size( 36, 53 );
            this.tbQuery.Text = "��ѯ";
            this.tbQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbSave
            // 
            this.tbSave.Image = ((System.Drawing.Image)(resources.GetObject( "tbSave.Image" )));
            this.tbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size( 36, 53 );
            this.tbSave.Text = "����";
            this.tbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbCorrect
            // 
            this.tbCorrect.Image = ((System.Drawing.Image)(resources.GetObject( "tbCorrect.Image" )));
            this.tbCorrect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCorrect.Name = "tbCorrect";
            this.tbCorrect.Size = new System.Drawing.Size( 36, 53 );
            this.tbCorrect.Text = "����";
            this.tbCorrect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbEligible
            // 
            this.tbEligible.Image = ((System.Drawing.Image)(resources.GetObject( "tbEligible.Image" )));
            this.tbEligible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbEligible.Name = "tbEligible";
            this.tbEligible.Size = new System.Drawing.Size( 36, 53 );
            this.tbEligible.Text = "�ϸ�";
            this.tbEligible.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbUnEligible
            // 
            this.tbUnEligible.Image = ((System.Drawing.Image)(resources.GetObject( "tbUnEligible.Image" )));
            this.tbUnEligible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbUnEligible.Name = "tbUnEligible";
            this.tbUnEligible.Size = new System.Drawing.Size( 48, 53 );
            this.tbUnEligible.Text = "���ϸ�";
            this.tbUnEligible.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbCancel
            // 
            this.tbCancel.Image = ((System.Drawing.Image)(resources.GetObject( "tbCancel.Image" )));
            this.tbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCancel.Name = "tbCancel";
            this.tbCancel.Size = new System.Drawing.Size( 36, 53 );
            this.tbCancel.Text = "����";
            this.tbCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbDelete
            // 
            this.tbDelete.Image = ((System.Drawing.Image)(resources.GetObject( "tbDelete.Image" )));
            this.tbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDelete.Name = "tbDelete";
            this.tbDelete.Size = new System.Drawing.Size( 36, 53 );
            this.tbDelete.Text = "ɾ��";
            this.tbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbClear
            // 
            this.tbClear.Image = ((System.Drawing.Image)(resources.GetObject( "tbClear.Image" )));
            this.tbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbClear.Name = "tbClear";
            this.tbClear.Size = new System.Drawing.Size( 36, 53 );
            this.tbClear.Text = "����";
            this.tbClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbExit
            // 
            this.tbExit.Image = ((System.Drawing.Image)(resources.GetObject( "tbExit.Image" )));
            this.tbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExit.Name = "tbExit";
            this.tbExit.Size = new System.Drawing.Size( 36, 53 );
            this.tbExit.Text = "�˳�";
            this.tbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            this.Text = "��������Ԥ������";
            this.Load += new System.EventHandler( this.frmReportManager_Load );
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
