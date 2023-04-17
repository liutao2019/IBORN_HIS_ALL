namespace FS.SOC.Local.GYZL.PubReport.Components
{
    partial class frmMidBalanceBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMidBalanceBill));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.tbCDFee = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbbOpenAccount = new System.Windows.Forms.ToolBarButton();
            this.tbFresh = new System.Windows.Forms.ToolBarButton();
            this.toolBarbtnOpenAccount = new System.Windows.Forms.ToolBarButton();
            this.tbQuit = new System.Windows.Forms.ToolBarButton();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.dTBegin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dTEnd = new System.Windows.Forms.DateTimePicker();
            this.btnOpenAccount = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ucBalanceBill1 = new FS.SOC.Local.GYZL.PubReport.Components.ucMidBalanceBill();
            this.label1 = new System.Windows.Forms.Label();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 613);
            this.statusBar1.Size = new System.Drawing.Size(864, 24);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbPrint,
            this.tbCDFee,
            this.toolBarButton1,
            this.tbbOpenAccount,
            this.tbFresh,
            this.toolBarbtnOpenAccount,
            this.tbQuit});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList2;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(864, 57);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 0;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Text = "打 印(P)";
            // 
            // tbCDFee
            // 
            this.tbCDFee.ImageIndex = 5;
            this.tbCDFee.Name = "tbCDFee";
            this.tbCDFee.Text = "收  费";
            this.tbCDFee.Visible = false;
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbOpenAccount
            // 
            this.tbbOpenAccount.ImageIndex = 1;
            this.tbbOpenAccount.Name = "tbbOpenAccount";
            this.tbbOpenAccount.Text = "开 帐(O)";
            this.tbbOpenAccount.Visible = false;
            // 
            // tbFresh
            // 
            this.tbFresh.ImageIndex = 2;
            this.tbFresh.Name = "tbFresh";
            this.tbFresh.Text = "清 屏";
            // 
            // toolBarbtnOpenAccount
            // 
            this.toolBarbtnOpenAccount.Name = "toolBarbtnOpenAccount";
            this.toolBarbtnOpenAccount.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbQuit
            // 
            this.tbQuit.ImageIndex = 3;
            this.tbQuit.Name = "tbQuit";
            this.tbQuit.Text = " 退 出";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "打印清单.png");
            this.imageList2.Images.SetKeyName(1, "收费.png");
            this.imageList2.Images.SetKeyName(2, "开帐.png");
            this.imageList2.Images.SetKeyName(3, "清屏.png");
            this.imageList2.Images.SetKeyName(4, "退出.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dTBegin);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dTEnd);
            this.panel1.Controls.Add(this.btnOpenAccount);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ucQueryInpatientNo1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 580);
            this.panel1.TabIndex = 1;
            // 
            // dTBegin
            // 
            this.dTBegin.CustomFormat = "yyyy-MM-dd";
            this.dTBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTBegin.Location = new System.Drawing.Point(256, 14);
            this.dTBegin.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dTBegin.Name = "dTBegin";
            this.dTBegin.ShowUpDown = true;
            this.dTBegin.Size = new System.Drawing.Size(112, 21);
            this.dTBegin.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(200, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "费用日期";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(374, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "至";
            // 
            // dTEnd
            // 
            this.dTEnd.CustomFormat = "yyy-MM-dd";
            this.dTEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTEnd.Location = new System.Drawing.Point(392, 14);
            this.dTEnd.Name = "dTEnd";
            this.dTEnd.ShowUpDown = true;
            this.dTEnd.Size = new System.Drawing.Size(112, 21);
            this.dTEnd.TabIndex = 10;
            // 
            // btnOpenAccount
            // 
            this.btnOpenAccount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenAccount.Location = new System.Drawing.Point(624, 12);
            this.btnOpenAccount.Name = "btnOpenAccount";
            this.btnOpenAccount.Size = new System.Drawing.Size(64, 23);
            this.btnOpenAccount.TabIndex = 4;
            this.btnOpenAccount.Text = "开账(&O)";
            this.btnOpenAccount.Visible = false;
            this.btnOpenAccount.Click += new System.EventHandler(this.btnOpenAccount_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(517, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "执  行";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.ucBalanceBill1);
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(864, 512);
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(864, 2);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // ucBalanceBill1
            // 
            this.ucBalanceBill1.BackColor = System.Drawing.Color.White;
            this.ucBalanceBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBalanceBill1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBalanceBill1.IsBalance = false;
            this.ucBalanceBill1.Location = new System.Drawing.Point(0, 0);
            this.ucBalanceBill1.Name = "ucBalanceBill1";
            this.ucBalanceBill1.Size = new System.Drawing.Size(864, 512);
            this.ucBalanceBill1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(864, 2);
            this.label1.TabIndex = 0;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;

            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(8, 8);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(190, 32);
            this.ucQueryInpatientNo1.TabIndex = 1;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // frmMidBalanceBill
            // 
            this.ClientSize = new System.Drawing.Size(864, 637);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar1);
            this.Name = "frmMidBalanceBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中途结算清单";
            this.Load += new System.EventHandler(this.frmMidBalanceBill_Load);
            this.Controls.SetChildIndex(this.toolBar1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        //private FS.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private System.Windows.Forms.Panel panel2;
        private ucMidBalanceBill ucBalanceBill1;
        private System.Windows.Forms.ToolBarButton tbPrint;
        private System.Windows.Forms.ToolBarButton tbFresh;
        private System.Windows.Forms.ToolBarButton toolBarbtnOpenAccount;
        private System.Windows.Forms.ToolBarButton tbQuit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnOpenAccount;
        private System.Windows.Forms.ToolBarButton tbbOpenAccount;
        private System.Windows.Forms.ToolBarButton tbCDFee;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        protected System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList2;
        protected System.Windows.Forms.DateTimePicker dTBegin;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.DateTimePicker dTEnd;

    }
}