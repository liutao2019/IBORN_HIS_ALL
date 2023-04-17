namespace FS.SOC.Local.GYZL.PubReport.Components
{
    partial class frmPublicBill
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
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbInHos = new System.Windows.Forms.ToolBarButton();
            this.tbBal = new System.Windows.Forms.ToolBarButton();
            this.tbPerson = new System.Windows.Forms.ToolBarButton();
            this.tbStatic = new System.Windows.Forms.ToolBarButton();
            this.tbMake = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbSelect = new System.Windows.Forms.ToolBarButton();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.tbModify = new System.Windows.Forms.ToolBarButton();
            this.tbDelete = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.tbHelp = new System.Windows.Forms.ToolBarButton();
            this.tbExit = new System.Windows.Forms.ToolBarButton();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.ucPublicBill1 = new ucPublicBill();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbInHos,
            this.tbBal,
            this.tbPerson,
            this.tbStatic,
            this.tbMake,
            this.toolBarButton1,
            this.tbSelect,
            this.tbModify,
            this.tbDelete,
            this.toolBarButton2,
            this.tbPrint,
            this.tbHelp,
            this.tbExit});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList32;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(984, 41);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.Visible = false;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbInHos
            // 
            this.tbInHos.ImageIndex = 33;
            this.tbInHos.Name = "tbInHos";
            this.tbInHos.Text = "在院患者";
            // 
            // tbBal
            // 
            this.tbBal.ImageIndex = 35;
            this.tbBal.Name = "tbBal";
            this.tbBal.Text = "结算患者";
            // 
            // tbPerson
            // 
            this.tbPerson.ImageIndex = 31;
            this.tbPerson.Name = "tbPerson";
            this.tbPerson.Text = "单独患者";
            // 
            // tbStatic
            // 
            this.tbStatic.ImageIndex = 0;
            this.tbStatic.Name = "tbStatic";
            this.tbStatic.Text = "统计(&S)";
            // 
            // tbMake
            // 
            this.tbMake.ImageIndex = 15;
            this.tbMake.Name = "tbMake";
            this.tbMake.Text = "生成托收";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbSelect
            // 
            this.tbSelect.DropDownMenu = this.contextMenu1;
            this.tbSelect.ImageIndex = 18;
            this.tbSelect.Name = "tbSelect";
            this.tbSelect.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tbSelect.Text = "选择(&C)";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "全选所有";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "全选已经统计";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "取消全选所有";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "取消选择已经统计";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // tbModify
            // 
            this.tbModify.ImageIndex = 2;
            this.tbModify.Name = "tbModify";
            this.tbModify.Text = "修改(&M)";
            // 
            // tbDelete
            // 
            this.tbDelete.ImageIndex = 1;
            this.tbDelete.Name = "tbDelete";
            this.tbDelete.Text = "删除(&D)";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 12;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Text = "打印(&P)";
            // 
            // tbHelp
            // 
            this.tbHelp.ImageIndex = 13;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.Text = "帮助(&H)";
            // 
            // tbExit
            // 
            this.tbExit.ImageIndex = 14;
            this.tbExit.Name = "tbExit";
            this.tbExit.Text = "退出(&X)";
            // 
            // imageList32
            // 
            this.imageList32.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList32.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ucPublicBill1
            // 
            this.ucPublicBill1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPublicBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPublicBill1.IsFullConvertToHalf = true;
            this.ucPublicBill1.IsPrint = false;
            this.ucPublicBill1.Location = new System.Drawing.Point(0, 41);
            this.ucPublicBill1.Name = "ucPublicBill1";
            this.ucPublicBill1.Size = new System.Drawing.Size(984, 711);
            this.ucPublicBill1.TabIndex = 1;
            // 
            // frmPublicBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucPublicBill1);
            this.Controls.Add(this.toolBar1);
            this.Name = "frmPublicBill";
            this.Size = new System.Drawing.Size(984, 752);
            this.Load += new System.EventHandler(this.frmPublicBill_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ImageList imageList32;
        private System.Windows.Forms.ToolBarButton tbInHos;
        private System.Windows.Forms.ToolBarButton tbBal;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton tbPrint;
        private System.Windows.Forms.ToolBarButton tbModify;
        private System.Windows.Forms.ToolBarButton tbDelete;
        private System.Windows.Forms.ToolBarButton tbStatic;
        private System.Windows.Forms.ToolBarButton tbHelp;
        private System.Windows.Forms.ToolBarButton tbPerson;
        private System.Windows.Forms.ToolBarButton tbExit;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton tbMake;
        private System.Windows.Forms.ToolBarButton tbSelect;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private ucPublicBill ucPublicBill1;
    }
}