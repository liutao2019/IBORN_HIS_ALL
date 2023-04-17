namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    partial class frmAlterFeeRate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlterFeeRate));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbQuery = new System.Windows.Forms.ToolBarButton();
            this.toolBarbtnModify = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarbtnHelp = new System.Windows.Forms.ToolBarButton();
            this.toolBarbtnExit = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ucAlterFeeRate1 = new FS.SOC.Local.InpatientFee.GuangZhou.ucAlterFeeRate();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 518);
            this.statusBar1.Size = new System.Drawing.Size(792, 24);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbQuery,
            this.toolBarbtnModify,
            this.toolBarButton1,
            this.toolBarButton2,
            this.toolBarbtnHelp,
            this.toolBarbtnExit});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(792, 57);
            this.toolBar1.TabIndex = 2;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbQuery
            // 
            this.tbQuery.ImageIndex = 0;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Text = "查询(F)";
            // 
            // toolBarbtnModify
            // 
            this.toolBarbtnModify.ImageIndex = 1;
            this.toolBarbtnModify.Name = "toolBarbtnModify";
            this.toolBarbtnModify.Text = "修改费用(&M)";
            this.toolBarbtnModify.ToolTipText = "修改";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 1;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.ImageIndex = 2;
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Text = "修改费用时间";
            // 
            // toolBarbtnHelp
            // 
            this.toolBarbtnHelp.ImageIndex = 4;
            this.toolBarbtnHelp.Name = "toolBarbtnHelp";
            this.toolBarbtnHelp.Text = "帮助(&H)";
            this.toolBarbtnHelp.ToolTipText = "帮助";
            // 
            // toolBarbtnExit
            // 
            this.toolBarbtnExit.ImageIndex = 3;
            this.toolBarbtnExit.Name = "toolBarbtnExit";
            this.toolBarbtnExit.Text = "退出(&X)";
            this.toolBarbtnExit.ToolTipText = "退出";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "查找 副本.png");
            this.imageList1.Images.SetKeyName(1, "手工录入 副本.png");
            this.imageList1.Images.SetKeyName(2, "审核 副本.png");
            this.imageList1.Images.SetKeyName(3, "退出 副本.png");
            this.imageList1.Images.SetKeyName(4, "帮助.png");
            // 
            // ucAlterFeeRate1
            // 
            this.ucAlterFeeRate1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucAlterFeeRate1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucAlterFeeRate1.IsFullConvertToHalf = true;
            this.ucAlterFeeRate1.IsPrint = false;
            this.ucAlterFeeRate1.Location = new System.Drawing.Point(0, 56);
            this.ucAlterFeeRate1.Name = "ucAlterFeeRate1";
            this.ucAlterFeeRate1.Size = new System.Drawing.Size(784, 480);
            this.ucAlterFeeRate1.TabIndex = 0;
            // 
            // frmAlterFeeRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 542);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.ucAlterFeeRate1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 576);
            this.MinimizeBox = false;
            this.Name = "frmAlterFeeRate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改患者费用比例";
            this.Activated += new System.EventHandler(this.frmAlterFeeRate_Activated);
            this.Controls.SetChildIndex(this.ucAlterFeeRate1, 0);
            this.Controls.SetChildIndex(this.toolBar1, 0);
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucAlterFeeRate ucAlterFeeRate1;


        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarbtnModify;      
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarbtnHelp;
        private System.Windows.Forms.ToolBarButton toolBarbtnExit;
        private System.Windows.Forms.ToolBarButton tbQuery;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ImageList imageList1;
    }
}