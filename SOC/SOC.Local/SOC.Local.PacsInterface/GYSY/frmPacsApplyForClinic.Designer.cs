using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SOC.Local.PacsInterface.GYSY
{
    partial　class frmPacsApplyForClinic
    {

        #region 设计器代码

        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton tbSave;
        private System.Windows.Forms.ToolBarButton tbPrint;
        private System.Windows.Forms.ToolBarButton tbExit;
        private System.Windows.Forms.ImageList imageList32;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        //public pacsInterface.ucPacsApplyForClinic ucPacsApply1;
        private System.ComponentModel.IContainer components;
        private ArrayList alItems = null;
        private System.Windows.Forms.ToolBarButton tbCancel;
        public SOC.Local.PacsInterface.GYSY.ucPacsApplyForClinic ucPacsApplyForClinic1;
        private System.Windows.Forms.ToolBarButton tbPrintView;
        private System.Windows.Forms.ToolBarButton tbDate;
        private System.Windows.Forms.ToolBarButton tbRefresh;
        private FS.HISFC.Models.Registration.Register reg = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="patientInfo"></param>
        public frmPacsApplyForClinic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="patientInfo"></param>
        public frmPacsApplyForClinic(ArrayList Items, FS.HISFC.Models.Registration.Register reg)
        {
            this.alItems = Items;
            this.reg = reg;
            InitializeComponent();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmPacsApplyForClinic));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.tbDate = new System.Windows.Forms.ToolBarButton();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.tbPrintView = new System.Windows.Forms.ToolBarButton();
            this.tbCancel = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.tbExit = new System.Windows.Forms.ToolBarButton();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.tbRefresh = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 613);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(776, 24);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButton1,
																						this.tbSave,
																						this.tbDate,
																						this.tbPrint,
																						this.tbPrintView,
																						this.tbRefresh,
																						this.tbCancel,
																						this.toolBarButton2,
																						this.tbExit});
            this.toolBar1.ButtonSize = new System.Drawing.Size(50, 55);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList32;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(776, 57);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 0;
            this.tbSave.Text = "保  存";
            // 
            // tbDate
            // 
            this.tbDate.ImageIndex = 5;
            this.tbDate.Text = "日  期";
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 1;
            this.tbPrint.Text = "打  印";
            // 
            // tbPrintView
            // 
            this.tbPrintView.ImageIndex = 4;
            this.tbPrintView.Text = "预  览";
            // 
            // tbCancel
            // 
            this.tbCancel.ImageIndex = 3;
            this.tbCancel.Text = "取  消";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbExit
            // 
            this.tbExit.ImageIndex = 2;
            this.tbExit.Text = "退  出";
            // 
            // imageList32
            // 
            this.imageList32.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList32.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tbRefresh
            // 
            this.tbRefresh.ImageIndex = 6;
            this.tbRefresh.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbRefresh.Text = "停止刷新";
            // 
            // frmPacsApplyForClinic
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(776, 637);
            this.Controls.Add(this.toolBar1);
            this.KeyPreview = true;
            this.Name = "frmPacsApplyForClinic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检查申请单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPacsApply_Load);
            this.Controls.SetChildIndex(this.toolBar1, 0);
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.ResumeLayout(false);

        }
        #endregion


        #endregion
    }
}
