using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common
{
    /// <summary>
    /// [功能描述: 门诊配发药]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、所有的配发药都继承这个，旨在统一界面风格
    /// 2、不允许在此写配发药等独自业务逻辑，旨在减少各种配发药之间的代码耦合性，达到修改配药不影响发药之目的  
    /// </summary>
    public partial class ucDrugBase : Base.ucDrugBaseComponent
    {

        public ucDrugBase()
        {
            InitializeComponent();

            this.notifyIcon1.MouseClick += new MouseEventHandler(notifyIcon1_MouseClick);
        }


        #region 变量属性


        private bool isCheckPrivePower = false;

        /// <summary>
        /// 是否判断发药权限
        /// </summary>
        [Description("是否判断发药权限"), Category("设置"), Browsable(true)]
        public bool IsCheckPrivePower
        {
            get { return isCheckPrivePower; }
            set { isCheckPrivePower = value; }
        }

        private bool isChooseDeptWhenCheckPrive = false;

        /// <summary>
        /// 权限检测时是否需要用户选择科室
        /// </summary>
        [Description("权限检测时是否需要用户选择科室"), Category("设置"), Browsable(true)]
        public bool IsChooseDeptWhenCheckPrive
        {
            get { return isChooseDeptWhenCheckPrive; }
            set { isChooseDeptWhenCheckPrive = value; }
        }

        private string privePowerString = "0320+M1";

        [Description("使用窗口需要的权限,如：0320+M1"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }

        protected FS.FrameWork.Models.NeuObject priveDept;

        /// <summary>
        /// 权限科室，当前获取、显示数据的科室
        /// </summary>
        [Description("权限科室"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject PriveDept
        {
            get { return priveDept; }
            set { priveDept = value; this.ucDrugTree1.PriveDept = value; }
        }


        private FS.FrameWork.Models.NeuObject stockDept;

        /// <summary>
        /// 当前操作数据保存、扣库等动作的科室
        /// </summary>
        [Description("扣库科室"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject StockDept
        {
            get { return stockDept; }
            set { stockDept = value; }
        }


        private FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal;

        /// <summary>
        /// 配药台
        /// </summary>
        [Description("配药台"), Category("非设置"), Browsable(false)]
        public FS.HISFC.Models.Pharmacy.DrugTerminal DrugTerminal
        {
            get { return drugTerminal; }
            set { drugTerminal = value; }
        }

        private FS.HISFC.Models.Pharmacy.DrugTerminal sendTerminal;

        /// <summary>
        /// 配药台
        /// </summary>
        [Description("发药窗"), Category("非设置"), Browsable(false)]
        public FS.HISFC.Models.Pharmacy.DrugTerminal SendTerminal
        {
            get { return sendTerminal; }
            set { sendTerminal = value; }
        }

        private Function.EnumOutpatintDrugOperType enumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;

        /// <summary>
        /// 操作状态
        /// 对于空闲状态才可以进行下一步操作
        /// </summary>
        [Description("操作状态"), Category("非设置"), Browsable(false)]
        public Function.EnumOutpatintDrugOperType EnumOutpatintDrugOperType
        {
            get
            {
                //需要检测处方树也是否在查询操作
                if (this.ucDrugTree1.EnumOutpatintDrugOperType != Function.EnumOutpatintDrugOperType.空闲)
                {
                    return this.ucDrugTree1.EnumOutpatintDrugOperType;
                }
                if (this.IsAtomizationOnOper)
                {
                    return enumOutpatintDrugOperType;
                }
                else
                {
                    return Function.EnumOutpatintDrugOperType.空闲;
                }
            }
            set
            {
                enumOutpatintDrugOperType = value;
                //需要设置处方树
                this.ucDrugTree1.EnumOutpatintDrugOperType = value;
                try
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.FindForm()).statusBar1.Panels[1].Text = "           状态：" + value.ToString() + "中...        ";
                }
                catch { }
            }
        }


        private int decimals = 2;

        [Description("金额小数位数"), Category("设置"), Browsable(true)]
        public int Decimals
        {
            get
            {
                return decimals;
            }
            set
            {
                this.ucRecipeDetail1.Decimals = value;
                decimals = value;
            }
        }


        private bool isAfterSave = false;

        /// <summary>
        /// 保存时是否调用接口IOutpatientDrug、IOutpatientLED
        /// </summary>
        [Description("保存后是否调用接口IOutpatientDrug、IOutpatientLED"), Category("设置"), Browsable(true)]
        public bool IsAfterSave
        {
            get { return isAfterSave; }
            set { isAfterSave = value; }
        }

        private bool isSetWorkLoad = false;

        /// <summary>
        /// 是否计工作量
        /// </summary>
        [Description("是否计工作量"), Category("设置"), Browsable(true)]
        public bool IsSetWorkLoad
        {
            get { return isSetWorkLoad; }
            set { isSetWorkLoad = value; }
        }

        /// <summary>
        /// 原子化各操作
        /// </summary>
        bool isAtomizationOnOper = true;

        /// <summary>
        /// 是否原子化各操作，Oracle可以尝试设置为false
        /// </summary>
        [Description("是否原子化各操作"), Category("设置"), Browsable(true)]
        public bool IsAtomizationOnOper
        {
            get { return isAtomizationOnOper; }
            set { isAtomizationOnOper = value; }
        }


        private bool isOtherDeptDrug = false;

        /// <summary>
        /// 是否属于它科配发药，必须检查操作员是否有当前科室和对方科室的权限
        /// </summary>
        [Description("是否它科配发药，将检查是否有双方科室的权限"), Category("设置"), Browsable(true)]
        public bool IsOtherDeptDrug
        {
            get { return isOtherDeptDrug; }
            set
            {
                isOtherDeptDrug = value;
                this.IsCheckPrivePower = true;
            }
        }

        /// <summary>
        /// 自动保存
        /// </summary>
        private bool isAutoSaveAfterQuery = false;

        /// <summary>
        /// 处方号查询到明细后是否自动保存
        /// </summary>
        [Description("处方号查询到明细后是否自动保存"), Category("设置"), Browsable(true)]
        public bool IsAutoSaveAfterQuery
        {
            get { return isAutoSaveAfterQuery; }
            set { isAutoSaveAfterQuery = value; }
        }

        /// <summary>
        /// 自动刷新后是否显示操作列表
        /// </summary>
        private bool isShowOperList = true;

        /// <summary>
        /// 自动刷新后是否显示操作列表
        /// </summary>
        [Description("自动刷新后是否显示操作列表"), Category("设置"), Browsable(true)]
        public bool IsShowOperList
        {
            get { return isShowOperList; }
            set { isShowOperList = value; }
        }

        #endregion

        #region 接口

        FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow myIOutpatientShow;
        /// <summary>
        /// 患者信息接口
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow IOutpatientShow
        {
            get
            {
                if (this.DesignMode)
                {
                    return null;
                }
                if (myIOutpatientShow == null)
                {
                    try
                    {
                        myIOutpatientShow = (InterfaceManager.GetOutpatientInfoLocalComponent() as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow);
                    }
                    catch
                    {
                        myIOutpatientShow = null;
                    }
                    if (myIOutpatientShow == null)
                    {
                        myIOutpatientShow = new ucPatientInfo();
                    }
                }
                if (myIOutpatientShow != null && this.ngbPatientInfo.Controls.Count == 0)
                {
                    Control c = myIOutpatientShow as Control;
                    if (c != null)
                    {
                        c.Dock = DockStyle.Fill;
                    }
                    this.ngbPatientInfo.Controls.Add(c);
                }
                return myIOutpatientShow;
            }
        }


        FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug myIOutpatientPrint;
        /// <summary>
        /// 打印接口
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug IOutpatientPrint
        {
            get
            {
                if (this.DesignMode)
                {
                    return null;
                }
                if (myIOutpatientPrint == null)
                {
                    try
                    {
                        myIOutpatientPrint = (InterfaceManager.GetOutpatientPrint() as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug);
                    }
                    catch
                    {
                        myIOutpatientPrint = null;
                    }
                }

                return myIOutpatientPrint;
            }
        }

        /// <summary>
        /// 记录是否已经初始化过工作量接口
        /// </summary>
        bool isCreateWorkLoad = false;

        SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Local.Interface.IOutpatientWorkLoadBatch myIOutpatientWorkLoad;
        /// <summary>
        /// 工作量接口
        /// </summary>
        public SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Local.Interface.IOutpatientWorkLoadBatch IOutpatientWorkLoad
        {
            get
            {
                if (this.DesignMode)
                {
                    return null;
                }
                if (myIOutpatientWorkLoad == null && !isCreateWorkLoad)
                {
                    try
                    {
                        isCreateWorkLoad = true;
                        myIOutpatientWorkLoad = (InterfaceManager.GetOutpatientWorkLoad() as SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Local.Interface.IOutpatientWorkLoadBatch);
                    }
                    catch
                    {
                        myIOutpatientWorkLoad = null;
                    }
                }

                return myIOutpatientWorkLoad;
            }
        }

        /// <summary>
        /// 记录是否已经初始化LED接口
        /// </summary>
        private bool isCreateLED = false;

        FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientLED myIOutpatientLED;
        /// <summary>
        /// LED大屏显示接口
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientLED IOutpatientLED
        {
            get
            {
                if (this.DesignMode)
                {
                    return null;
                }
                if (myIOutpatientLED == null && !isCreateLED)
                {
                    try
                    {
                        this.isCreateLED = true;
                        myIOutpatientLED = (InterfaceManager.GetOutpatientLED() as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientLED);
                    }
                    catch
                    {
                        myIOutpatientLED = null;
                    }
                }

                return myIOutpatientLED;
            }
        }


        #endregion

        #region 配发药公用的工具栏

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        public FS.FrameWork.WinForms.Forms.ToolBarService ToolBarService
        {
            get { return toolBarService; }
            set { toolBarService = value; }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("打印处方", "手工打印或补打处方", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("打印标签", "手工打印或补打标签", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("打印清单", "手工打印或补打清单", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("打印药袋", "手工打印或补打药袋", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "打印处方")
            {
                this.Print("Recipe");
            }
            else if (e.ClickedItem.Text == "打印标签")
            {
                this.Print("DrugLabel");

            }
            else if (e.ClickedItem.Text == "打印清单")
            {
                this.Print("DrugBill");
            }
            else if (e.ClickedItem.Text == "打印药袋")
            {
                this.Print("DrugBag");
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 按键
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                this.ucDrugTree1.ChangeQueryType();
            }
            else if (keyData == Keys.F10)
            {
                this.ucDrugTree1.FocusBillNO();
            }
            else if (keyData == Keys.F11)
            {
                this.ucDrugTree1.SelectTree(0);
            }
            else if (keyData == Keys.F12)
            {
                this.ucDrugTree1.SelectTree(1);
            }


            //屏蔽系统的F10
            if (keyData == Keys.F10)
            {
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.ucDrugTree1.FocusBillNO();
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region 提示信息

        void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }

        /// <summary>
        /// 气泡提示信息
        /// </summary>
        protected void ShowBalloonTip(int timeOut, string title, string tipText, ToolTipIcon toolTipIcon)
        {
            if (this.DesignMode)
            {
                return;
            }
            if (this.ParentForm != null)
            {
                this.ParentForm.ShowInTaskbar = true;
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(timeOut, title, tipText, toolTipIcon);
            }
        }

        /// <summary>
        /// 气泡提示信息
        /// </summary>
        protected void ShowBalloonTip(string tipText)
        {
            this.ShowBalloonTip(5, "提示：", tipText, ToolTipIcon.Info);
        }

        /// <summary>
        /// 提供在状态栏第一panel显示信息的功能
        /// </summary>
        /// <param name="text">显示信息的文本</param>
        protected void ShowStatusBarTip(string text)
        {
            if (this.ParentForm != null)
            {
                if (this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).statusBar1.Panels[1].Text = "       " + text + "       ";
                }
            }
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        protected void ShowMessage(string text)
        {
            ShowMessage(text, MessageBoxIcon.Information);
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        protected void ShowMessage(string text, MessageBoxIcon messageBoxIcon)
        {
            string caption = "";
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }

            MessageBox.Show(text, caption, MessageBoxButtons.OK, messageBoxIcon);
        }

        #endregion

        #region 打印或补打

        /// <summary>
        /// 打印，此函数和调用接口中不更新打印状态
        /// 可能存在RecipeState=0的打印后也不更新状态情况，这个在具体的配发药界面控制
        /// </summary>
        /// <param name="type"></param>
        private void Print(string type)
        {
            if (this.ucDrugTree1.DrugRecipe != null)
            {
                if (this.IOutpatientPrint == null)
                {
                    this.ShowMessage("没有实现打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug", MessageBoxIcon.Error);
                }
                else
                {
                    List<FS.HISFC.Models.Pharmacy.ApplyOut> listData = this.ucRecipeDetail1.GetSelectInfo();
                    System.Collections.ArrayList alData = new System.Collections.ArrayList(listData);

                    FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal = this.SendTerminal;
                    if (drugTerminal == null)
                    {
                        drugTerminal = this.DrugTerminal;
                    }

                    if (type == "DrugBill")
                    {
                        this.IOutpatientPrint.PrintDrugBill(alData, this.ucDrugTree1.DrugRecipe, drugTerminal);
                    }
                    else if (type == "DrugLabel")
                    {
                        this.IOutpatientPrint.PrintDrugLabel(alData, this.ucDrugTree1.DrugRecipe, drugTerminal);
                    }
                    else if (type == "Recipe")
                    {
                        this.IOutpatientPrint.PrintRecipe(alData, this.ucDrugTree1.DrugRecipe, drugTerminal);
                    }
                    else if (type == "DrugBag")
                    {
                        this.IOutpatientPrint.PrintDrugBag(alData, this.ucDrugTree1.DrugRecipe, drugTerminal);
                    }
                }
            }
        }

        #endregion
    }
}
