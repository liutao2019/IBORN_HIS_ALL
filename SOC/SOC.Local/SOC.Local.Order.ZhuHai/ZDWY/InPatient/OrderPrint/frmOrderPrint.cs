using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.Xml;
using System.IO;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.OrderPrint
{
    /// <summary>
    /// 医嘱单打印（续打）
    /// 
    /// 说明：
    /// 1、修改前先看懂代码，不要盲目修改
    /// 2、目前打印应该已经能够兼容很多格式了，各个项目只需调整InitSheet
    /// </summary>
    public partial class frmOrderPrint : FS.FrameWork.WinForms.Forms.BaseStatusBar, FS.HISFC.BizProcess.Interface.IPrintOrder
    {
        public frmOrderPrint()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.fpLongOrder.ActiveSheetChanged += new EventHandler(fpLongOrder_ActiveSheetChanged);
                this.fpShortOrder.ActiveSheetChanged += new EventHandler(fpShortOrder_ActiveSheetChanged);
                this.fpOperateOrder.ActiveSheetChanged += new EventHandler(fpOperateOrder_ActiveSheetChanged);

                this.fpLongOrder.MouseUp += new MouseEventHandler(fpLongOrder_MouseUp);
                fpShortOrder.MouseUp += new MouseEventHandler(fpShortOrder_MouseUp);
                //this.fpOperateOrder.MouseUp += new MouseEventHandler(fpOperateOrder_MouseUp);

                this.tbReset.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
                this.fpLongOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpLongOrder_ColumnWidthChanged);
                this.fpLongOrder.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(fpLongOrder_ColumnDragMoveCompleted);
                this.fpShortOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpShortOrder_ColumnWidthChanged);
                this.fpShortOrder.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(fpShortOrder_ColumnDragMoveCompleted);

                this.nuRowNum.ValueChanged += new EventHandler(nuRowNum_ValueChanged);

                this.tcControl.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);


                this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);
                this.tbPrinter.TextChanged += new EventHandler(tbPrinter_SelectedIndexChanged);
                this.cmbPrintType.SelectedIndexChanged += new EventHandler(cmbPrintType_SelectedIndexChanged);

                cbxPreview.CheckedChanged += new EventHandler(cbxPreview_CheckedChanged);
            }
        }

        #region 变量

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        Function funMgr = new Function();

        /// <summary>
        /// 右键菜单
        /// </summary>
        System.Windows.Forms.ContextMenu popMenu = new ContextMenu();

        /// <summary>
        /// 默认重整时间
        /// </summary>
        private DateTime reformDate = new DateTime(2000, 1, 1);

        /// <summary>
        /// 长嘱列表
        /// </summary>
        ArrayList alLong = new ArrayList();

        /// <summary>
        /// 所有临嘱列表
        /// </summary>
        ArrayList alShort_ALL = new ArrayList();

        /// <summary>
        /// 临嘱列表
        /// </summary>
        ArrayList alShort_Normal = new ArrayList();

        /// <summary>
        /// 手术列表
        /// </summary>
        ArrayList alOperate = new ArrayList();

        /// <summary>
        /// 检查检验医嘱列表
        /// </summary>
        ArrayList alShort_UCUL = new ArrayList();



        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo;

        FarPoint.Win.Spread.SheetSkin sheetSKin_Black = new FarPoint.Win.Spread.SheetSkin("CustomSkin1",
                    System.Drawing.Color.White,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
            //线的颜色
                    System.Drawing.Color.Black,
                    FarPoint.Win.Spread.GridLines.Both,
                    System.Drawing.Color.White,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    true, true, false, true, true);

        FarPoint.Win.Spread.SheetSkin sheetSKin_White = new FarPoint.Win.Spread.SheetSkin("CustomSkin1",
                    System.Drawing.Color.White,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
            //线的颜色
                    System.Drawing.Color.White,
                    FarPoint.Win.Spread.GridLines.Both,
                    System.Drawing.Color.White,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
                    true, true, false, true, true);

        /// <summary>
        /// 打印时是否显示通用名，否则显示药品名
        /// </summary>
        private bool isDisplayRegularName = true;

        /// <summary>
        /// 是否转科自动换页
        /// </summary>
        private bool isShiftDeptNextPag = false;

        /// <summary>
        /// 是否允许隔页打印 如：手术室医嘱
        /// </summary>
        private bool isCanIntervalPrint = false;

        /// <summary>
        /// 长嘱或临嘱查询次数标识
        /// </summary>
        private int orderQueryCount = 1;

        /// <summary>
        /// 是否显示检验检查打印// {1B360F77-7C78-4614-B61A-D9542D57C603}
        /// </summary>
        private bool isShowULOrderPrint = false;


        /// <summary>
        /// 长期医嘱单XML配置
        /// </summary>
        string LongOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\LongOrderPrintSetting.xml";

        /// <summary>
        /// 临时医嘱单XML配置
        /// </summary>
        string shortOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\ShortOrderPrintSetting.xml";


        string operateOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OperateOrderPrintSetting.xml";

        #region 打印设置

        /// <summary>
        /// 医嘱单打印配置
        /// </summary>
        private string fileName = Application.StartupPath + "\\Setting\\ORDERPRINT.xml";

        /// <summary>
        /// 打印机名
        /// </summary>
        private string printerName = string.Empty;

        /// <summary>
        /// 左边距
        /// </summary>
        private int leftValue = 0;

        /// <summary>
        /// 上边距
        /// </summary>
        private int topValue = 0;

        /// <summary>
        /// 每页打印的行数
        /// </summary>
        private int defaultRowNum = 25;

        /// <summary>
        /// 每张纸的高度
        /// </summary>
        private int pageHeight = 890;

        /// <summary>
        /// 打印方式
        /// </summary>
        private EnumPrintType OrderPrintType = EnumPrintType.PrintWhenPatientOut;

        /// <summary>
        /// 当前登录是否管理员
        /// </summary>
        private bool isManager = ((FS.HISFC.Models.Base.Employee)FrameWork.Management.Connection.Operator).IsManager;

        #endregion

        #region 人员打印模式

        /// <summary>
        /// 操作人员打印模式（0 空打印；1 电脑打印）
        /// 开立医生、执行护士、停止医生、停止审核护士
        /// 例如：0000
        /// </summary>
        private string operPrintMode = "1111";

        /// <summary>
        /// 是否打印开立医生
        /// </summary>
        private bool isPrintReciptDoct = true;

        /// <summary>
        /// 是否打印审核护士
        /// </summary>
        private bool isPrintConfirmNurse = true;

        /// <summary>
        /// 是否打印停止医生
        /// </summary>
        private bool isPrintDCDoct = true;

        /// <summary>
        /// 是否打印停止审核护士
        /// </summary>
        private bool isPrintDCConfirmNurse = true;

        #endregion

        #endregion

        #region 方法

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderPrint_Load(object sender, System.EventArgs e)
        {
            #region 设置菜单

            this.tbExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbAllPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbRePrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C重打);
            this.tbQuery.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C查询);
            this.tbReset.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Q取消);
            this.tbSetting.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S设置);

            this.tbDeleteLongProfile.Visible = false;
            this.tbDeleteShortProfile.Visible = false;

            //增加删除配置文件的按钮菜单

            #endregion

            #region 初始化医嘱Farpoint

            this.fpLongOrder.Sheets.Clear();
            this.ucLongOrderBillHeader.Clear();
            this.ucShortOrderBillHeader.Clear();
            this.ucLongOrderBillHeader.SetPatientInfo(this.myPatientInfo);
            this.ucShortOrderBillHeader.SetPatientInfo(this.myPatientInfo);

            FarPoint.Win.Spread.SheetView sheetLong = new FarPoint.Win.Spread.SheetView();
            this.InitSheet(sheetLong, EnumOrderType.Long);
            this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheetLong);

            this.fpShortOrder.Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheetShort = new FarPoint.Win.Spread.SheetView();
            this.InitSheet(sheetShort, EnumOrderType.Short);
            this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheetShort);

            this.fpOperateOrder.Sheets.Clear();
            FarPoint.Win.Spread.SheetView sheetOperate = new FarPoint.Win.Spread.SheetView();
            this.InitSheet(sheetOperate, EnumOrderType.Operate);
            this.fpShortOrder.Sheets.Insert(this.fpOperateOrder.Sheets.Count, sheetOperate);



            this.ucShortOrderBillHeader.Header = "临  时  医  嘱  单";
            this.ucLongOrderBillHeader.Header = "长  期  医  嘱  单";
            this.ucOperateBillHeader.Header = "手  术  医  嘱  单";

            #endregion

            #region 打印设置相关功能是否可用（管理员可用）

            this.tbReset.Visible = true;

            this.ResetCurrentLong.Visible = true;
            this.ResetCurrentShort.Visible = true;
            this.RefreshLong.Visible = isManager;
            this.RefreshShort.Visible = isManager;

            //这个目前没用啊
            this.tbSetting.Visible = false;

            this.fpLongOrder.AllowColumnMove = isManager;
            this.fpShortOrder.AllowColumnMove = isManager;


            this.cmbPrintType.Enabled = isManager;
            this.fpLongOrder.AllowUserZoom = isManager;
            this.fpShortOrder.AllowUserZoom = isManager;
            this.nuLeft.Enabled = isManager;
            this.nuTop.Enabled = isManager;
            this.nuRowNum.Enabled = isManager;

            cbxPreview.Visible = isManager;

            #endregion

            #region 控制参数

            //华南医疗参数：打印时是否显示通用名，否则显示药品名
            try
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                isShiftDeptNextPag = controlIntegrate.GetControlParam<bool>("HNZY32", true, true);


                isShowULOrderPrint = controlIntegrate.GetControlParam<bool>("YZDY01", true, true);// {1B360F77-7C78-4614-B61A-D9542D57C603}

                isCanIntervalPrint = controlIntegrate.GetControlParam<bool>("HNZY33", true, false);

                this.isDisplayRegularName = controlIntegrate.GetControlParam<bool>("HNZY01", true, true);
                operPrintMode = controlIntegrate.GetControlParam<string>("HNZY30", true, "1111");

                isPrintReciptDoct = operPrintMode.Substring(0, 1) == "1" ? true : false;
                isPrintConfirmNurse = operPrintMode.Substring(1, 1) == "1" ? true : false;
                isPrintDCDoct = operPrintMode.Substring(2, 1) == "1" ? true : false;
                isPrintDCConfirmNurse = operPrintMode.Substring(3, 1) == "1" ? true : false;
            }
            catch
            {
                this.isDisplayRegularName = true;
                this.isPrintReciptDoct = true;
                this.isPrintConfirmNurse = true;
                this.isPrintDCDoct = true;
                this.isPrintDCConfirmNurse = true;
            }

            #endregion

            #region 打印参数设置

            //获取系统打印机列表
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                //取打印机名
                string name = System.Text.RegularExpressions.Regex.Replace(PrinterSettings.InstalledPrinters[i], @"在(\s|\S)*上", "").Replace("自动", "");
                this.tbPrinter.Items.Add(name);
            }

            //打印方式列表
            this.cmbPrintType.AddItems(FS.FrameWork.Public.EnumHelper.Current.EnumArrayList<EnumPrintType>());
            if (cmbPrintType.Items.Count > 0)
            {
                cmbPrintType.SelectedIndex = 0;
            }

            //从XML读默认设置
            if (File.Exists(fileName))
            {
                XmlDocument file = new XmlDocument();
                file.Load(fileName);
                XmlNode node = file.SelectSingleNode("ORDERPRINT/医嘱单");
                if (node != null)
                {
                    this.printerName = node.InnerText;
                }

                tbPrinter.SelectedIndexChanged -= new EventHandler(tbPrinter_SelectedIndexChanged);
                this.tbPrinter.TextChanged -= new EventHandler(tbPrinter_SelectedIndexChanged);
                for (int i = 0; i < this.tbPrinter.Items.Count; i++)
                {
                    if (this.tbPrinter.Items[i].ToString() == this.printerName)
                    {
                        this.tbPrinter.SelectedIndex = i;
                        break;
                    }
                }
                tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);
                this.tbPrinter.TextChanged += new EventHandler(tbPrinter_SelectedIndexChanged);

                node = file.SelectSingleNode("ORDERPRINT/左边距");
                if (node != null)
                {
                    this.leftValue = FS.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuLeft.Value = this.leftValue;

                node = file.SelectSingleNode("ORDERPRINT/上边距");
                if (node != null)
                {
                    this.topValue = FS.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuTop.Value = this.topValue;

                node = file.SelectSingleNode("ORDERPRINT/行数");
                if (node != null)
                {
                    this.defaultRowNum = FS.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuRowNum.Value = this.defaultRowNum;

                node = file.SelectSingleNode("ORDERPRINT/预览打印");
                if (node != null)
                {
                    cbxPreview.Checked = FS.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                }

                node = file.SelectSingleNode("ORDERPRINT/打印方式");
                string printType = string.Empty;
                if (node != null)
                {
                    printType = node.InnerText;
                }
                this.cmbPrintType.Text = printType;

                if (printType == FS.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.WhitePaperContinue))
                {
                    this.OrderPrintType = EnumPrintType.WhitePaperContinue;
                }
                else if (printType == FS.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.DrawPaperContinue))
                {
                    this.OrderPrintType = EnumPrintType.DrawPaperContinue;
                }
                else
                {
                    this.OrderPrintType = EnumPrintType.PrintWhenPatientOut;
                }
            }

            #endregion

            if (this.tcControl.SelectedTab == this.tpLong)
            {
                orderQueryCount = 1;
            }
            QueryPatientOrder();
        }

        /// <summary>
        /// 获取当前操作的医嘱单界面
        /// </summary>
        private EnumOrderType OrderType
        {
            get
            {
                if (this.tcControl.SelectedIndex == 0)
                {
                    return EnumOrderType.Long;
                }
                else if (this.tcControl.SelectedIndex == 2)
                {
                    return EnumOrderType.Operate;
                }
                else
                {
                    return EnumOrderType.Short;
                }
            }

        }

        /// <summary>
        /// 获取当前操作的医嘱单界面
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private FS.SOC.Windows.Forms.FpSpread GetFpOrder(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                return this.fpLongOrder;
            }
            else if (orderType == EnumOrderType.Operate)
            {
                return this.fpOperateOrder;
            }
            else
            {
                return this.fpShortOrder;
            }
        }

        /// <summary>
        /// 获取当前的医嘱单表头
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private ucOrderBillHeader GetActBillHeader(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                return this.ucLongOrderBillHeader;
            }
            else if (orderType == EnumOrderType.Operate)
            {
                return this.ucOperateBillHeader;
                //return this.ucShortOrderBillHeader;
            }
            else
            {
                return this.ucShortOrderBillHeader;
            }
        }

        /// <summary>
        /// 获取当前的医嘱单表头
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private Panel GetActPrintPanel(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                return this.pnLongOrder;
            }
            else if (orderType == EnumOrderType.Operate)
            {
                return this.pnOperates;
            }
            else
            {
                return this.pnShortOrder;
            }
        }

        /// <summary>
        /// 获取当前的医嘱单表头
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private Panel GetActPrintPanelPage(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                return this.pnLongPag;
            }
            else if (orderType == EnumOrderType.Operate)
            {
                return this.pnOperatePag;
            }
            else
            {
                return this.pnShortPag;
            }
        }

        #region 调整保存界面格式

        void fpShortOrder_ColumnDragMoveCompleted(object sender, FarPoint.Win.Spread.DragMoveCompletedEventArgs e)
        {
            SaveFPSchema(EnumOrderType.Short);
        }

        void fpLongOrder_ColumnDragMoveCompleted(object sender, FarPoint.Win.Spread.DragMoveCompletedEventArgs e)
        {
            SaveFPSchema(EnumOrderType.Long);
        }

        void fpShortOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            SaveFPSchema(EnumOrderType.Short);
        }

        void fpLongOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            SaveFPSchema(EnumOrderType.Long);
        }

        private void SaveFPSchema(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                this.fpLongOrder.SaveSchema(this.fpLongOrder.ActiveSheet, this.LongOrderSetXML);
                fpLongOrder.ActiveSheet.PrintInfo.ZoomFactor = fpLongOrder.ActiveSheet.ZoomFactor;
            }
            else if (orderType == EnumOrderType.Short)
            {
                this.fpShortOrder.SaveSchema(this.fpShortOrder.ActiveSheet, this.shortOrderSetXML);
                fpShortOrder.ActiveSheet.PrintInfo.ZoomFactor = fpShortOrder.ActiveSheet.ZoomFactor;
            }
            else if (orderType == EnumOrderType.Operate)
            {
                //this.fpOperateOrder.SaveSchema(this.fpOperateOrder.ActiveSheet, this.operateOrderSetXML);
                //fpOperateOrder.ActiveSheet.PrintInfo.ZoomFactor = fpOperateOrder.ActiveSheet.ZoomFactor;
            }
            else
            {
                this.fpLongOrder.SaveSchema(this.fpLongOrder.ActiveSheet, this.LongOrderSetXML);
                fpLongOrder.ActiveSheet.PrintInfo.ZoomFactor = fpLongOrder.ActiveSheet.ZoomFactor;

                this.fpShortOrder.SaveSchema(this.fpShortOrder.ActiveSheet, this.shortOrderSetXML);
                fpShortOrder.ActiveSheet.PrintInfo.ZoomFactor = fpShortOrder.ActiveSheet.ZoomFactor;

                //this.fpOperateOrder.SaveSchema(this.fpOperateOrder.ActiveSheet, this.operateOrderSetXML);
                //fpOperateOrder.ActiveSheet.PrintInfo.ZoomFactor = fpOperateOrder.ActiveSheet.ZoomFactor;
            }
        }

        #endregion

        #region 打印参数设置

        /// <summary>
        /// 设置打印时是否预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbxPreview_CheckedChanged(object sender, EventArgs e)
        {
            this.SetPrintValue("预览打印", "1");
        }

        /// <summary>
        /// 打印模式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPrintType.Text))
            {
                this.SetPrintValue("打印方式", this.cmbPrintType.Text);
            }

            if (cmbPrintType.Text == FS.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.WhitePaperContinue))
            {
                this.OrderPrintType = EnumPrintType.WhitePaperContinue;
            }
            else if (cmbPrintType.Text == FS.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.DrawPaperContinue))
            {
                this.OrderPrintType = EnumPrintType.DrawPaperContinue;
            }
            else
            {
                this.OrderPrintType = EnumPrintType.PrintWhenPatientOut;
            }
        }

        #endregion

        /// <summary>
        /// 初始化医嘱Sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="orderType"></param>
        /// <param name="rowCount">行数</param>
        private void InitSheet(FarPoint.Win.Spread.SheetView sheet, EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                #region 初始化长嘱
                sheet.Reset();
                sheet.ColumnCount = (Int32)EnumCol.Max;
                sheet.ColumnHeader.RowCount = 2;
                sheet.RowCount = 0;
                //sheet.RowCount = rowCount;
                sheet.RowHeader.ColumnCount = 0;
                sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

                FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
                textCellType1.WordWrap = true;

                #region 标题

                //第一行
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.BeginDate).Text = "  开  医  嘱  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).Text = "  医  嘱  内  容  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCDate).Text = "  停  医  嘱  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.OnceDose).Text = "每次量";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Usage).Text = "用法";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Freq).Text = "频次";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Memo).Text = "备注";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ExecNurse).Text = "执行";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Qty).Text = "总量";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Cost).Text = "金额";


                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmDate).Text = "审核日期";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmTime).Text = "审核时间";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCFlag).Text = "停止标记";

                //第二行
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginDate).Text = "日期";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginTime).Text = "时间";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.RecipeDoct).Text = "医师签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmNurse).Text = "护士签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.CombFlag).Text = "组";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ItemName).Text = "  长期医嘱  ";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.OnceDose).Text = "每次量";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Usage).Text = "用法";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Freq).Text = "频次";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Memo).Text = "备注";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDate).Text = "日期";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCTime).Text = "时间";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDoct).Text = "医师签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCConfirmNurse).Text = "护士签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ExecNurse).Text = "执行";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Qty).Text = "总量";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Cost).Text = "金额";

                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmDate).Text = "审核日期";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmTime).Text = "审核时间";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCFlag).Text = "停止标记";

                //行列合并
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.BeginDate).ColumnSpan = 4;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).ColumnSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.OnceDose).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Usage).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Freq).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Memo).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCDate).ColumnSpan = 4;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ExecNurse).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Qty).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Cost).RowSpan = 2;

                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmDate).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmTime).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCFlag).RowSpan = 2;

                //设置可见性
                sheet.ColumnHeader.Columns[(Int32)EnumCol.OnceDose].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.Usage].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.Freq].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.Memo].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.ExecNurse].Visible = false;
                //sheet.ColumnHeader.Columns[(Int32)LongOrderColunms.Qty].Visible = false;
                //sheet.ColumnHeader.Columns[(Int32)LongOrderColunms.Cost].Visible = false;
                sheet.Columns.Get((Int32)EnumCol.CombNO).Visible = false;
                //sheet.Columns.Get((Int32)LongOrderColunms.Max).Visible = false;

                sheet.ColumnHeader.Columns[(Int32)EnumCol.ConfirmTime].Visible = true;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.ConfirmDate].Visible = true;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.DCFlag].Visible = true;

                //设置文字类型
                sheet.Columns.Get((Int32)EnumCol.ItemName).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.OnceDose).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Freq).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Usage).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Memo).CellType = textCellType1;

                //设置宽度
                sheet.Columns.Get((Int32)EnumCol.BeginDate).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.BeginTime).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.CombFlag).Width = 17F;
                sheet.Columns.Get((Int32)EnumCol.ItemName).Width = 250F;
                sheet.Columns.Get((Int32)EnumCol.OnceDose).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.Usage).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.Freq).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.Memo).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.RecipeDoct).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.ConfirmNurse).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.DCDate).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.DCTime).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.DCDoct).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.DCConfirmNurse).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.ExecNurse).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.Qty).Width = 30F;
                sheet.Columns.Get((Int32)EnumCol.Cost).Width = 40F;

                #endregion
                #endregion
            }
            else
            {
                #region 初始化临嘱
                sheet.Reset();
                sheet.ColumnCount = (Int32)EnumCol.Max;
                sheet.ColumnHeader.RowCount = 2;
                sheet.RowCount = 0;
                //sheet.RowCount = rowCount;
                sheet.RowHeader.ColumnCount = 0;
                sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

                FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
                textCellType1.WordWrap = true;

                #region 标题

                //第一行
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.BeginDate).Text = "  开  医  嘱  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.RecipeDoct).Text = "医师签名";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmNurse).Text = "护士签名";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).Text = "  医  嘱  内  容  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCDate).Text = "  执  行  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.OnceDose).Text = "每次量";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Usage).Text = "用法";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Freq).Text = "频次";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Memo).Text = "备注";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ExecNurse).Text = "执行";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Qty).Text = "总量";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Cost).Text = "金额";

                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmDate).Text = "审核日期";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmTime).Text = "审核时间";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCFlag).Text = "停止标记";

                //第二行
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginDate).Text = "日期";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginTime).Text = "时间";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.RecipeDoct).Text = "医师签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmNurse).Text = "护士签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.CombFlag).Text = "组";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ItemName).Text = "  长期医嘱  ";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.OnceDose).Text = "每次量";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Usage).Text = "用法";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Freq).Text = "频次";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Memo).Text = "备注";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDate).Text = "日期";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCTime).Text = "时间";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDoct).Text = "签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCConfirmNurse).Text = "护士签名";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ExecNurse).Text = "执行";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Qty).Text = "总量";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Cost).Text = "金额";

                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmDate).Text = "审核日期";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmTime).Text = "审核时间";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCFlag).Text = "停止标记";

                //行列合并
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.BeginDate).ColumnSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.RecipeDoct).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmNurse).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Freq).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).ColumnSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.OnceDose).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Usage).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Freq).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Memo).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCDate).ColumnSpan = 4;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ExecNurse).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Qty).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Cost).RowSpan = 2;

                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmDate).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmTime).RowSpan = 2;
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCFlag).RowSpan = 2;

                //设置可见性
                sheet.ColumnHeader.Columns[(Int32)EnumCol.ConfirmNurse].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.OnceDose].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.Usage].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.Freq].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.Memo].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.DCTime].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.DCConfirmNurse].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.Memo].Visible = false;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.ExecNurse].Visible = false;
                //sheet.ColumnHeader.Columns[(Int32)EnumCol.Qty].Visible = false;
                //sheet.ColumnHeader.Columns[(Int32)EnumCol.Cost].Visible = false;
                sheet.Columns.Get((Int32)EnumCol.CombNO).Visible = false;

                sheet.ColumnHeader.Columns[(Int32)EnumCol.ConfirmTime].Visible = true;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.ConfirmDate].Visible = true;
                sheet.ColumnHeader.Columns[(Int32)EnumCol.DCFlag].Visible = true;

                //设置文字类型
                sheet.Columns.Get((Int32)EnumCol.ItemName).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.OnceDose).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Freq).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Usage).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Memo).CellType = textCellType1;

                //设置宽度
                sheet.Columns.Get((Int32)EnumCol.BeginDate).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.BeginTime).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.CombFlag).Width = 17F;
                sheet.Columns.Get((Int32)EnumCol.ItemName).Width = 250F;
                sheet.Columns.Get((Int32)EnumCol.OnceDose).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.Usage).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.Freq).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.Memo).Width = 25F;
                sheet.Columns.Get((Int32)EnumCol.RecipeDoct).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.ConfirmNurse).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.DCDate).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.DCTime).Width = 45F;
                sheet.Columns.Get((Int32)EnumCol.DCDoct).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.DCConfirmNurse).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.ExecNurse).Width = 55F;
                sheet.Columns.Get((Int32)EnumCol.Qty).Width = 30F;
                sheet.Columns.Get((Int32)EnumCol.Cost).Width = 40F;

                #endregion
                #endregion
            }
            if (!this.isShowULOrderPrint)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {
                this.tcControl.Controls.Remove(this.tpUCUL);
                this.tbReset.DropDownItems.Remove(ResetUCUL);
                this.tbReset.DropDownItems.Remove(ResetCurrentUCUL);
            }
            else
            {
                if (!this.tcControl.Controls.Contains(this.tpUCUL))
                {
                    this.tcControl.Controls.Add(this.tpUCUL);
                }
                this.tbReset.DropDownItems.Clear();
                this.tbReset.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                            this.ResetLong,
                                                            this.ResetShort,
                                                            this.ResetUCUL,
                                                            this.ResetAll,
                                                            this.ResetCurrentLong,
                                                            this.ResetCurrentShort,
                                                            this.ResetCurrentUCUL,
                                                            this.RefreshLong,
                                                            this.RefreshShort});
            }
            sheet.ActiveSkin = sheetSKin_Black;

            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;
            sheet.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            sheet.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            sheet.SheetCornerStyle.Parent = "HeaderDefault";

            for (int j = 0; j < sheet.ColumnCount; j++)
            {
                if (j != (Int32)EnumCol.ItemName
                    && j != (Int32)EnumCol.Usage
                    && j != (Int32)EnumCol.Memo
                    && j != (Int32)EnumCol.Freq)
                {
                    sheet.Columns[j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                }
                sheet.Columns[j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            #region 计算每行的高度

            //float fpHeaderHeight = 0;

            //fpHeaderHeight += sheet.ColumnHeader.Rows.Get(0).Height;
            //fpHeaderHeight += sheet.ColumnHeader.Rows.Get(1).Height;

            //int rowHeight = FS.FrameWork.Function.NConvert.ToInt32((this.pageHeight - fpHeaderHeight) / rowCount);

            //for (int i = 0; i < rowCount; i++)
            //{
            //    sheet.Rows.Get(i).BackColor = System.Drawing.Color.White;
            //    sheet.Rows.Get(i).Height = rowHeight;
            //}
            #endregion
        }

        #endregion

        #region 查询显示

        /// <summary>
        /// 查询患者医嘱信息
        /// </summary>
        private void QueryPatientOrder()
        {
            if (this.myPatientInfo == null || string.IsNullOrEmpty(myPatientInfo.ID))
            {
                return;
            }

            //这里要搞一下，为啥还没有弹出界面 就有提示框选择了... 应该先显示界面

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询显示医嘱信息,请稍候......");
            Application.DoEvents();

            //try
            //{
            ArrayList alAll = new ArrayList();

            #region 根据医嘱类型分别读取医嘱，以减小负荷量

            if (tcControl.SelectedIndex == 0)
            {
                alLong.Clear();
                alAll = this.orderManager.QueryPrnOrderByOrderType(this.myPatientInfo.ID, "1");//长期医嘱
                //alAll = this.orderManager.QueryPrnOrder(this.myPatientInfo.ID);
                alLong = alAll;
                this.Clear(EnumOrderType.Long);
                this.AddOrderToFP(EnumOrderType.Long, alLong, -1, 0);

                this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder());

                this.SetFPStyle(EnumOrderType.Long);
            }
            else if (tcControl.SelectedIndex == 1)
            {
                alShort_ALL.Clear();
                this.alShort_Normal.Clear();
                alShort_UCUL.Clear();

                alAll = this.orderManager.QueryPrnOrderByOrderType(this.myPatientInfo.ID, "0");//临时医嘱
                if (alAll != null && alAll.Count > 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alAll)
                    {
                        if (inOrder.Item.SysClass.ID.ToString() == "UC"
                            || inOrder.Item.SysClass.ID.ToString() == "UL")
                        {
                            alShort_UCUL.Add(inOrder);
                        }
                        else
                        {
                            alShort_Normal.Add(inOrder);
                        }
                    }
                }
                //alAll = this.orderManager.QueryPrnOrder(this.myPatientInfo.ID);
                alShort_ALL = alAll;
                this.Clear(EnumOrderType.Short);

                if (this.isShowULOrderPrint)// {1B360F77-7C78-4614-B61A-D9542D57C603}
                {
                    if (this.tcControl.SelectedTab == tpShort)
                    {
                        this.AddOrderToFP(EnumOrderType.Short, alShort_Normal, -1, 0);
                    }
                    else if (this.tcControl.SelectedTab == tpUCUL)
                    {
                        this.AddOrderToFP(EnumOrderType.Short, alShort_UCUL, -1, 0);
                    }
                }
                else
                {
                    if (this.tcControl.SelectedTab == tpShort)
                    {
                        this.AddOrderToFP(EnumOrderType.Short, alShort_ALL, -1, 0);
                    }
                }

                this.ucShortOrderBillHeader.SetChangedInfo(this.GetFirstOrder());

                this.SetFPStyle(EnumOrderType.Short);
            }
            else
            {
                //手术相关
                alOperate.Clear();

                alAll = this.orderManager.QueryPrnOperateOrderByInpatientNO(this.myPatientInfo.ID);//临时医嘱

                alOperate = alAll;
                this.Clear(EnumOrderType.Operate);


                this.AddOrderToFP(EnumOrderType.Operate, alOperate, -1, 0);

                this.ucOperateBillHeader.SetChangedInfo(this.GetFirstOrder());

                this.SetFPStyle(EnumOrderType.Operate);


            }

            #endregion

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //}
            //catch (Exception ex)
            //{
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// 设置树形显示
        /// </summary>
        private void SetTreeView()
        {
            if (this.myPatientInfo == null)
            {
                return;
            }

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }

            TreeNode root = new TreeNode();

            root.Text = "住院信息:" + "[" + this.myPatientInfo.Name + "]" + "[" + this.myPatientInfo.PID.PatientNO + "]";

            this.treeView1.Nodes.Add(root);

            root.ImageIndex = 0;

            TreeNode node = new TreeNode();

            node.Text = "[" + this.myPatientInfo.PVisit.InTime.ToShortDateString() + "][" + this.myPatientInfo.PVisit.PatientLocation.Dept.Name + "]";

            node.Tag = this.myPatientInfo;

            node.ImageIndex = 1;

            root.Nodes.Add(node);

            this.treeView1.ExpandAll();
        }

        /// <summary>
        /// 设置Farpoint格式
        /// </summary>
        /// <returns></returns>
        private int SetFPStyle(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                foreach (FarPoint.Win.Spread.SheetView sheet in fpLongOrder.Sheets)
                {
                    if (System.IO.File.Exists(LongOrderSetXML))
                    {
                        fpLongOrder.ReadSchema(sheet, LongOrderSetXML, false);

                        sheet.PrintInfo.ZoomFactor = sheet.ZoomFactor;
                    }
                    if (!((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
                    {
                        for (int i = 0; i < sheet.Columns.Count; i++)
                        {
                            sheet.Columns[i].Resizable = false;
                        }
                    }
                    DealHeight(sheet);
                }
            }
            else if (orderType == EnumOrderType.Operate)
            {
                foreach (FarPoint.Win.Spread.SheetView sheet in this.fpOperateOrder.Sheets)
                {
                    if (System.IO.File.Exists(operateOrderSetXML))
                    {
                        fpOperateOrder.ReadSchema(sheet, operateOrderSetXML, false);

                        sheet.PrintInfo.ZoomFactor = sheet.ZoomFactor;
                    }

                    if (!((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
                    {
                        for (int i = 0; i < sheet.Columns.Count; i++)
                        {
                            sheet.Columns[i].Resizable = false;
                        }
                    }
                    DealHeight(sheet);
                }
            }
            else
            {
                foreach (FarPoint.Win.Spread.SheetView sheet in this.fpShortOrder.Sheets)
                {
                    if (System.IO.File.Exists(shortOrderSetXML))
                    {
                        fpShortOrder.ReadSchema(sheet, shortOrderSetXML, false);

                        sheet.PrintInfo.ZoomFactor = sheet.ZoomFactor;
                    }

                    if (!((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
                    {
                        for (int i = 0; i < sheet.Columns.Count; i++)
                        {
                            sheet.Columns[i].Resizable = false;
                        }
                    }
                    DealHeight(sheet);
                }
            }
            return 1;
        }

        private int DealHeight(FarPoint.Win.Spread.SheetView sheet)
        {
            #region 计算每行的高度

            float fpHeaderHeight = 0;

            fpHeaderHeight += sheet.ColumnHeader.Rows.Get(0).Height;
            fpHeaderHeight += sheet.ColumnHeader.Rows.Get(1).Height;
            if (sheet.RowCount != 0)
            {
                int rowHeight = FS.FrameWork.Function.NConvert.ToInt32((this.pageHeight - fpHeaderHeight) / sheet.RowCount);


                for (int i = 0; i < sheet.RowCount; i++)
                {
                    //sheet.Rows.Get(i).BackColor = System.Drawing.Color.White;
                    sheet.Rows.Get(i).Height = rowHeight;
                }
            }
            #endregion

            return 1;
        }

        #region 清空

        /// <summary>
        /// 清空所有医嘱单内容
        /// </summary>
        private void Clear(EnumOrderType orderType)
        {
            this.GetFpOrder(orderType).Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
            this.InitSheet(sheet, orderType);
            this.GetFpOrder(orderType).Sheets.Add(sheet);

            if (orderType == EnumOrderType.Long)
            {
                this.lblPageLong.Text = "第1页";
            }
            else if (orderType == EnumOrderType.Operate)
            {
                this.lblPageOperatets.Text = "第1页";
            }
            else
            {
                this.lblPageShort.Text = "第1页";
            }
        }

        #endregion

        /// <summary>
        /// 获取打印的时间：开始时间或开立时间
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private DateTime GetPrintDate(FS.HISFC.Models.Order.Order order)
        {
            if (tcControl.SelectedIndex == 0)   //{3066617c-1fcd-4dea-a215-690210bb0f96}
            {
                return order.BeginTime;
            }
            else      //临时医嘱取医生开立时间
            {
                return order.MOTime;
            }
        }

        /// <summary>
        /// 加入到医嘱单显示
        /// </summary>
        /// <param name="isLong">是否长嘱</param>
        /// <param name="alOrder"></param>
        private void AddOrderToFP(EnumOrderType orderType, ArrayList alOrder, int MaxPageNo, int MaxRowNo)
        {
            if (alOrder.Count <= 0)
            {
                return;
            }

            //按照页码、行号、组号、排序号 排序
            alOrder.Sort(new OrderComparer());

            //用于处理转科换页
            string deptCode = "";

            //特殊医嘱类别
            string speOrderType = "";

            //存储最近一个正常开立医嘱类型
            //用来处理 科室医嘱->会诊->科室医嘱 识别 会诊->科室医嘱 需不需要换页
            string deptOrderType = "";

            //用来处理重整医嘱,记录上个医嘱状态，用来跟下一条比较
            FS.HISFC.Models.Order.Inpatient.Order recordOrder = null;

            //有可能医生重整医嘱后，增加一些新开立项目，然后调整顺序，这样打印重整分页就乱了，所以增加重整记录
            Hashtable hsRecord = new Hashtable();

            foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrder)
            {
                //if (inOrder.RowNo >= this.rowNum)
                //{
                //    MessageBox.Show(inOrder.OrderType.Name + "【" + inOrder.Item.Name + "】的实际打印行号为" + (inOrder.RowNo + 1).ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                //}

                if (string.IsNullOrEmpty(deptCode))
                {
                    //deptCode = inOrder.ReciptDept.ID;
                    deptCode = inOrder.Patient.PVisit.PatientLocation.Dept.ID;
                }
                inOrder.Nurse.Name = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(inOrder.Nurse.ID);
                inOrder.ReciptDoctor.Name = inOrder.ReciptDoctor.Name;
                if (inOrder.DCNurse.ID != null && inOrder.DCNurse.ID != "")
                {
                    inOrder.DCNurse.Name = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(inOrder.DCNurse.ID);
                }

                if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    inOrder.Item.Name += orderManager.TransHypotest(inOrder.HypoTest);
                }

                //if (string.IsNullOrEmpty(speOrderType))
                //{
                //    speOrderType = inOrder.SpeOrderType;
                //}

                #region 显示

                //已打印的
                if (inOrder.GetFlag != "0")
                {
                    //新页
                    if (inOrder.PageNo > MaxPageNo)
                    {
                        MaxPageNo = inOrder.PageNo;
                        MaxRowNo = 0;
                    }

                    if (MaxPageNo >= this.GetFpOrder(orderType).Sheets.Count)
                    {
                        FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                        this.InitSheet(sheet, orderType);
                        this.GetFpOrder(orderType).Sheets.Insert(this.GetFpOrder(orderType).Sheets.Count, sheet);
                    }

                    this.AddToRow(true, this.GetFpOrder(orderType).Sheets[MaxPageNo], MaxRowNo, inOrder);

                    if (inOrder.RowNo >= MaxRowNo)
                    {
                        MaxRowNo = inOrder.RowNo + 1;
                    }
                }
                //未打印
                else
                {
                    if ((MaxRowNo % this.defaultRowNum == 0) ||

                        MaxRowNo > defaultRowNum ||

                        //转科也要自动换页
                        (this.isShiftDeptNextPag &&
                        !string.IsNullOrEmpty(deptCode)
                        //&& deptCode.Trim() != inOrder.ReciptDept.ID
                        && deptCode.Trim() != inOrder.Patient.PVisit.PatientLocation.Dept.ID

                        //科室医嘱转到会诊医嘱时，不换页
                        //这里修改需谨慎！！
                        && !(
                        //科室医嘱->空、空->空、会诊医嘱->空
                            string.IsNullOrEmpty(inOrder.SpeOrderType)
                        //科室医嘱->会诊医嘱
                            || (speOrderType.Contains("DEPT") && inOrder.SpeOrderType.Contains("CONS"))
                        //会诊医嘱->科室医嘱（保证还是原科室医嘱)
                            || ((speOrderType.Contains("CONS") || string.IsNullOrEmpty(speOrderType))
                                && inOrder.SpeOrderType.Contains("DEPT")
                                && inOrder.ReciptDept.ID + inOrder.SpeOrderType.Substring(0, 4) == deptOrderType)
                            )
                        )

                        )
                    {
                        MaxPageNo += 1;
                        MaxRowNo = 0;

                        if (MaxPageNo >= this.GetFpOrder(orderType).Sheets.Count)
                        {
                            FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                            this.InitSheet(sheet, orderType);
                            this.GetFpOrder(orderType).Sheets.Insert(this.GetFpOrder(orderType).Sheets.Count, sheet);
                        }
                    }
                    else if (/*moState == 4 **/recordOrder.Status == 4
                        && inOrder.Status != 4)
                    {
                        //if (MessageBox.Show("存在重整医嘱，是否自动换页？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        //{

                        string strRecord = recordOrder.DCOper.OperTime.ToString() + recordOrder.Status.ToString() + recordOrder.DcReason.ID + recordOrder.DcReason.Name;
                        if (!hsRecord.Contains(strRecord))
                        {
                            MaxPageNo += 1;
                            MaxRowNo = 0;

                            if (MaxPageNo >= this.GetFpOrder(orderType).Sheets.Count)
                            {
                                FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                                this.InitSheet(sheet, orderType);
                                this.GetFpOrder(orderType).Sheets.Insert(this.GetFpOrder(orderType).Sheets.Count, sheet);
                            }

                            hsRecord.Add(strRecord, inOrder);
                        }
                        //}
                    }

                    this.AddToRow(false, this.GetFpOrder(orderType).Sheets[MaxPageNo], MaxRowNo, inOrder);
                    MaxRowNo += 1;
                }

                if (this.GetFpOrder(orderType).Sheets[MaxPageNo].Tag == null)
                {
                    this.GetFpOrder(orderType).Sheets[MaxPageNo].Tag = MaxPageNo;
                    this.GetFpOrder(orderType).Sheets[MaxPageNo].SheetName = "第" + (MaxPageNo + 1).ToString() + "页";
                }

                //始终存上一条医嘱的状态
                //moState = inOrder.Status;
                recordOrder = inOrder.Clone();
                #endregion

                if (deptCode != inOrder.Patient.PVisit.PatientLocation.Dept.ID)
                {
                    //deptCode = inOrder.ReciptDept.ID;
                    deptCode = inOrder.Patient.PVisit.PatientLocation.Dept.ID;
                }

                speOrderType = inOrder.SpeOrderType;

                if (inOrder.SpeOrderType.Contains("DEPT"))
                {
                    deptOrderType = inOrder.ReciptDept.ID + inOrder.SpeOrderType.Substring(0, 4);
                }

                if (GetFpOrder(orderType).Sheets[MaxPageNo].RowCount < this.defaultRowNum)
                {
                    GetFpOrder(orderType).Sheets[MaxPageNo].RowCount = defaultRowNum;
                }
            }

            for (int i = 0; i < this.GetFpOrder(orderType).Sheets.Count; i++)
            {
                DrawCombFlag(this.GetFpOrder(orderType).Sheets[i], (Int32)EnumCol.CombNO, (Int32)EnumCol.CombFlag);
            }

            DealCrossPage(orderType);
        }

        /// <summary>
        /// 每行添加
        /// </summary>
        /// <param name="isLong"></param>
        /// <param name="isPint"></param>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        /// <param name="inOrder"></param>
        private void AddToRow(bool isPint, FarPoint.Win.Spread.SheetView sheet, int row, FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            if (sheet.RowCount <= row)
            {
                sheet.AddRows(row, 1);
            }

            if (isPint)
            {
                sheet.Rows[row].BackColor = Color.MistyRose;
            }
            else
            {
                sheet.Rows[row].BackColor = Color.White;
            }


            sheet.SetValue(row, (Int32)EnumCol.BeginDate, GetPrintDate(inOrder).Month.ToString() + "-" + GetPrintDate(inOrder).Day.ToString());
            sheet.SetValue(row, (Int32)EnumCol.BeginTime, GetPrintDate(inOrder).ToShortTimeString());

            sheet.SetValue(row, (Int32)EnumCol.ItemName, GetOrderItemName(inOrder, EnumOrderType.Long));

            if (inOrder.Item.ItemType == EnumItemType.Drug)
            {
                sheet.SetValue(row, (Int32)EnumCol.OnceDose, FS.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce) + inOrder.DoseUnit);
            }
            else
            {
                sheet.SetValue(row, (Int32)EnumCol.OnceDose, FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Item.PriceUnit);
            }

            sheet.SetValue(row, (Int32)EnumCol.Usage, inOrder.Usage.Name);
            sheet.SetValue(row, (Int32)EnumCol.Freq, inOrder.Frequency.ID);
            sheet.SetValue(row, (Int32)EnumCol.Memo, inOrder.Memo);


            sheet.SetValue(row, (Int32)EnumCol.RecipeDoct, inOrder.ReciptDoctor.Name);
            sheet.SetValue(row, (Int32)EnumCol.ConfirmNurse, inOrder.Nurse.Name);

            if (inOrder.OrderType.IsDecompose)
            {
                if (inOrder.DCOper.OperTime > this.reformDate)
                {
                    //sheet.SetValue(row, (Int32)EnumCol.DCDate, inOrder.DCOper.OperTime.Month.ToString() + "-" + inOrder.DCOper.OperTime.Day.ToString());
                    //sheet.SetValue(row, (Int32)EnumCol.DCTime, inOrder.DCOper.OperTime.ToShortTimeString());
                    //{C69EBDD7-61DD-4b23-A7A9-DF1662A6DCD0}
                    sheet.SetValue(row, (Int32)EnumCol.DCDate, inOrder.EndTime.Month.ToString() + "-" + inOrder.DCOper.OperTime.Day.ToString());
                    sheet.SetValue(row, (Int32)EnumCol.DCTime, inOrder.EndTime.ToShortTimeString());
                    sheet.SetValue(row, (Int32)EnumCol.DCDoct, inOrder.DCOper.Name);
                    sheet.SetValue(row, (Int32)EnumCol.DCConfirmNurse, inOrder.DCNurse.Name);
                }
            }
            //中五特殊处理
            //停止列打印审核护士、审核信息
            else if (inOrder.Status != 0
                && inOrder.ConfirmTime > new DateTime(2000, 1, 1))
            {
                sheet.SetValue(row, (Int32)EnumCol.DCDate, inOrder.ConfirmTime.ToString("MM-dd HH:mm"));
                sheet.SetValue(row, (Int32)EnumCol.DCTime, inOrder.ConfirmTime.ToShortTimeString());
                sheet.SetValue(row, (Int32)EnumCol.DCDoct, inOrder.Nurse.Name);
            }

            //if (!inOrder.OrderType.IsDecompose    {de5fac98-bd9c-44c4-844a-5930d230a75d}
            //    && inOrder.Status == 3)
            //{
            //    sheet.SetValue(row, (Int32)EnumCol.DCDoct, inOrder.Nurse.Name + " 取消");
            //}

            sheet.SetValue(row, (Int32)EnumCol.CombNO, inOrder.Combo.ID);

            sheet.Rows[row].Tag = inOrder;
        }

        /// <summary>
        /// 获取显示的医嘱名称
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isLong"></param>
        /// <returns></returns>
        private string GetOrderItemName(FS.HISFC.Models.Order.Inpatient.Order order, EnumOrderType orderType)
        {
            //频次处理
            string strFreq = " " + order.Frequency.ID;
            if (!order.OrderType.IsDecompose)
            {
                if (SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(order.Frequency.ID) <= 1)
                {
                    strFreq = " ";
                }
            }

            //特殊医嘱处理
            string strSpeOrder = "";
            if (order.OrderType.ID == "CD")
            {
                strSpeOrder = " (出院带药)";
            }
            else if (order.OrderType.ID == "BL")
            {
                strSpeOrder = " (补录医嘱)";
            }

            //加急标记
            string strEmg = "";
            if (order.IsEmergency)
            {
                strEmg = " [急] ";
            }

            //嘱托自备标记
            string byoStr = "";
            if (!order.OrderType.IsCharge
                || order.Item.ID == "999")
            {
                if (!order.Item.Name.Contains("自备")
                    && !order.Item.Name.Contains("嘱托"))
                {
                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        byoStr = "[自备]";
                    }
                    else
                    {
                        byoStr = "[嘱托]";
                    }
                }
            }

            //首日量显示
            string strFirstDay = "";
            if (order.OrderType.IsDecompose)
            {
                strFirstDay = " 首日 " + order.FirstUseNum;
            }

            string showName = "";

            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                string itemName = order.Item.Name;
                //是否显示通用名，否则
                if (isDisplayRegularName)
                {
                    itemName = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).NameCollection.RegularName;
                }

                string strQTY = "";

                if (!order.OrderType.IsDecompose)
                {
                    strQTY = "(" + order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + order.Item.PriceUnit + ")";
                }

                string DoseOnce = (FS.FrameWork.Public.String.ToSimpleString(order.DoseOnce)) == "0" ? "" : FS.FrameWork.Public.String.ToSimpleString(order.DoseOnce);

                showName = itemName //项目名称
                   + byoStr //嘱托标记
                   + strEmg //加急标记
                   + " " + DoseOnce + order.DoseUnit //每次量
                   + strQTY //数量
                   + " " + order.Usage.Name //用法
                   + strFreq //频次
                   + " " + order.Memo //备注
                   + strFirstDay //首日量
                   + " " + strSpeOrder; //特殊医嘱标记



            }
            else
            {
                string sysCode = "";
                if (order.Item.ID != "999")
                {
                    sysCode = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SysClass.ID.ToString();
                }

                string strQTY = "";

                if (order.Qty != 1)
                {
                    strQTY = " " + order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + order.Item.PriceUnit;
                }


                #region 旧规则方法
                /*
                //护理、饮食不显示频次
                if (order.Item.Name.Contains("护理")
                    || order.Item.Name.Contains("饮食")
                    || order.Item.Name.Contains("普食")
                    || order.Item.Name.Contains("禁食")
                    || order.Item.ID == "999"
                    || sysCode == "MF" //膳食
                    || sysCode == "UN" //护理级别
                    )
                {
                    showName = order.Item.Name  //项目名称
                        + byoStr //嘱托标记
                        + strEmg //加急标记
                        + strQTY //数量
                        + " " + order.Memo //备注
                        + " " + strSpeOrder; //特殊医嘱标记

                }
                else
                {
                    showName = order.Item.Name //项目名称
                        + byoStr//嘱托标记
                        + strEmg //加急标记
                        + strQTY //数量
                        + strFreq //频次
                        + " " + order.Memo //备注
                        + " " + strSpeOrder; //特殊医嘱标记
                }
                */
                #endregion

                #region 新嘱托名称规则{0E78C368-911F-4522-85CF-0192E7D58A49}
                showName = order.Item.Name //项目名称
                        + byoStr//嘱托标记
                        + strEmg //加急标记
                        + strQTY //数量
                        + strFreq //频次
                        + " " + order.Memo //备注
                        + " " + strSpeOrder; //特殊医嘱标记
                #endregion
            }

            ////作废的临嘱 显示“取消”字样
            //if (!order.OrderType.IsDecompose
            //    && order.Status == 3)
            //{
            //    showName += " 取消";

            //    //string itemName = showName;
            //    //itemName = " 取消".PadLeft(GetLength(itemName), ' ');
            //    //showName = itemName;
            //}

            if (!order.OrderType.IsDecompose
              && order.Status == 3)
            {
                //showName += "【取消】";   //{de5fac98-bd9c-44c4-844a-5930d230a75d}
                //{a2a72d4f-3b6d-4111-81d8-4af3a9eb6670}

                showName += "【医嘱取消，确认医师：" + order.DCOper.Name + "   " + order.EndTime.ToString("yyyy-MM-dd HH:mm") + "】";
            }
            return showName;
        }

        /// <summary>
        /// 转科分页后添加到Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        private void ShiftPage(EnumOrderType orderType)
        {
            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(orderType).ActiveSheet;
            int rowIndex = sheet.ActiveRowIndex;
            if (rowIndex == 0)
            {
                MessageBox.Show("已经是第一行，不可以另起一页！");
                return;
            }

            if (sheet.Rows[rowIndex].Tag == null)
            {
                MessageBox.Show("所选项目为空！");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = sheet.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            if (ord.GetFlag != "0")
            {
                MessageBox.Show("医嘱[" + ord.Item.Name + "]已经打印过，不能另起一页！");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag);

            //获取剩余数据
            ArrayList alShiftOrder = new ArrayList();
            int fromRow = rowIndex;
            for (int sheetIndex = pageNo; sheetIndex < GetFpOrder(orderType).Sheets.Count; sheetIndex++)
            {
                for (int row = fromRow; row < GetFpOrder(orderType).Sheets[sheetIndex].RowCount; row++)
                {
                    if (GetFpOrder(orderType).Sheets[sheetIndex].Rows[row].Tag != null)
                    {
                        alShiftOrder.Add(GetFpOrder(orderType).Sheets[sheetIndex].Rows[row].Tag);
                    }
                }

                fromRow = 0;
            }

            //清空数据
            GetFpOrder(orderType).Sheets.Count = pageNo + 1;
            sheet.RowCount = rowIndex;
            sheet.RowCount = this.defaultRowNum;
            for (int row = rowIndex; row < defaultRowNum; row++)
            {
                sheet.Rows[row].Height = sheet.Rows[0].Height;
            }

            //填充到界面显示
            AddOrderToFP(orderType, alShiftOrder, pageNo, 0);

            return;
        }

        /// <summary>
        /// 画组合号
        /// </summary>
        /// <param name="view"></param>
        /// <param name="combNocol"></param>
        /// <param name="flagCol"></param>
        private void DrawCombFlag(FarPoint.Win.Spread.SheetView view, int combNocol, int flagCol)
        {
            Classes.Function.DrawComboLeft(view, (Int32)EnumCol.CombNO, (Int32)EnumCol.CombFlag);
            view.Columns[(Int32)EnumCol.CombFlag].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            //for (int i = 0; i < view.RowCount; i++)
            //{
            //    if (view.Rows[i].Tag != null)
            //    {
            //        view.Cells[i, (Int32)EnumCol.CombFlag].Text = ((FS.HISFC.Models.Order.Inpatient.Order)view.Rows[i].Tag).SubCombNO.ToString() + view.Cells[i, (Int32)EnumCol.CombFlag].Text;
            //    }
            //}
        }

        #endregion

        #region 打印

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReprint"></param>
        /// <param name="fromLine">打印的起始行</param>
        /// <param name="endLine">打印的截止行</param>
        /// <param name="isDCReprint">是否补打停止信息，否则的话就是打印整行信息</param>
        private int PrintPage(bool isReprint, int fromLine, int endLine, bool isDCRepint)
        {
            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).ActiveSheet;

            if (isReprint)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrd = sheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                if (inOrd.GetFlag == "0")
                {
                    MessageBox.Show("该页医嘱单还未打印，不需要重打！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;
                }
            }

            string errText = "";

            try
            {
                //处理当前页面最后一行有数据，保证能够打印
                int rowCount = sheet.RowCount;

                if (string.IsNullOrEmpty(sheet.Cells[rowCount - 1, (Int32)EnumCol.Max - 1].Text))
                {
                    sheet.Cells[rowCount - 1, (Int32)EnumCol.Max - 1].Text = " ";
                }

                if (fromLine > 0)
                {
                    SetPrintShow(false, fromLine, endLine, isDCRepint);
                }
                else
                {
                    if (isReprint)
                    {
                        SetPrintShow(isReprint, fromLine, endLine, isDCRepint);
                    }
                    else
                    {
                        if (!this.CanPrint(OrderType, ref errText))
                        {
                            if (!string.IsNullOrEmpty(errText))
                            {
                                MessageBox.Show(errText, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            return -1;
                        }

                        if (this.IsPrintAgain())
                        {
                            DialogResult r = MessageBox.Show("该页医嘱单已全部打印完毕，是否需要重新打印？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (r == DialogResult.No)
                            {
                                return -1;
                            }
                            else
                            {
                                isReprint = true;
                                SetPrintShow(true, fromLine, endLine, isDCRepint);
                            }
                        }
                        else
                        {
                            SetPrintShow(false, fromLine, endLine, isDCRepint);
                        }
                    }
                }

                this.GetActPrintPanelPage(OrderType).Dock = DockStyle.None;

                float i = this.GetFpOrder(OrderType).Location.Y;
                float i1 = this.GetFpOrder(OrderType).ActiveSheet.RowHeader.Rows[0].Height;
                float i3 = this.GetFpOrder(OrderType).ActiveSheet.RowHeader.Rows[1].Height;
                float i4 = this.GetFpOrder(OrderType).ActiveSheet.Rows[0].Height * this.GetFpOrder(OrderType).ActiveSheet.RowCount;

                this.GetActPrintPanelPage(OrderType).Location = new Point(GetFpOrder(OrderType).Location.X, this.GetFpOrder(OrderType).Location.Y + (Int32)(this.GetFpOrder(OrderType).ActiveSheet.RowHeader.Rows[0].Height + this.GetFpOrder(OrderType).ActiveSheet.RowHeader.Rows[1].Height + this.GetFpOrder(OrderType).ActiveSheet.Rows[0].Height * this.GetFpOrder(OrderType).ActiveSheet.RowCount) - 15);


                #region 打印

                System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 800, 1100);
                print.SetPageSize(size);
                print.IsCanCancel = false;
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                FS.HISFC.Models.Order.Inpatient.Order firstOrder = sheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (firstOrder.GetFlag == "0")
                {
                    print.IsShowFarPointBorder = true;
                }
                if (isReprint)
                {
                    print.IsShowFarPointBorder = true;
                }

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager
                    || cbxPreview.Checked)
                {
                    print.PrintPreview(this.leftValue, this.topValue, this.GetActPrintPanel(OrderType));
                }
                else
                {
                    print.PrintPage(this.leftValue, this.topValue, this.GetActPrintPanel(OrderType));
                }

                //FS.SOC.Windows.Forms.Print socPrint = new FS.SOC.Windows.Forms.Print();
                //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager
                //    || cbxPreview.Checked)
                //{
                //    socPrint.PrintPageView(GetActPrintPanel(OrderType));
                //}
                //else
                //{
                //    socPrint.PrintPage(GetActPrintPanel(OrderType));
                //}

                //GetFpOrder(OrderType).PrintSheet(GetFpOrder(OrderType).ActiveSheet);

                #endregion

                #region 打印后更新并显示

                if (isReprint
                    || fromLine > 0)
                {
                    this.InitFPShow(GetFpOrder(OrderType).ActiveSheet, false);
                }
                else
                {
                    DialogResult dia;

                    frmNotice frmNotice = new frmNotice();
                    frmNotice.label1.Text = "续打医嘱单是否成功？";
                    frmNotice.ShowDialog();
                    dia = frmNotice.dr;
                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("确定续打没有成功吗？误操作会造成医嘱单出现空行！", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //确定续打没有成功，没话说了
                            this.InitFPShow(GetFpOrder(OrderType).ActiveSheet, false);
                            //return -1;
                        }
                        else
                        {
                            dia = DialogResult.Yes;
                        }
                    }

                    if (dia == DialogResult.Yes)
                    {
                        if (OrderType != EnumOrderType.Operate)
                        {
                            if (this.UpdatePrintFlag(sheet) <= 0)
                            {
                            }
                        }

                        #region 打印完长嘱后查询医嘱页码是否有误

                        int count = this.orderManager.CheckPageRowNoAndGetFlag(this.myPatientInfo.ID, "1");//查询长嘱
                        if (count < 0)
                        {
                            MessageBox.Show("页码提取标志查询出错。");
                        }
                        else if (count > 0)
                        {
                            //MessageBox.Show("注意：有 " + count + " 条医嘱更新页码出错，请联系信息科。");
                        }

                        #endregion

                        this.InitFPShow(GetFpOrder(OrderType).ActiveSheet, true);
                    }
                }
                #endregion

                FS.SOC.Windows.Forms.FpSpread fpSpread = null;

                if (tcControl.SelectedTab == this.tpLong)
                {
                    fpSpread = this.fpLongOrder;
                }
                else if (tcControl.SelectedTab == this.tpOperate)
                {
                    fpSpread = this.fpOperateOrder; ;
                }
                else
                {
                    fpSpread = fpShortOrder;
                }

                int select = fpSpread.ActiveSheetIndex;

                this.QueryPatientOrder();

                fpSpread.ActiveSheetIndex = select;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                this.GetActPrintPanelPage(OrderType).Dock = DockStyle.Bottom;
            }

            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            PrintPage(false, -1, -1, false);
        }

        private bool isTip = true;

        /// <summary>
        /// 打印全部// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        private void AllPrint()
        {
            isTip = false;

            int maxPapeLong = this.GetFpOrder(EnumOrderType.Long).Sheets.Count;
            this.tcControl.SelectedIndex = 0;
            for (int i = 0; i < maxPapeLong; i++)
            {
                this.GetFpOrder(EnumOrderType.Long).ActiveSheetIndex = i;
                PrintPage(false, -1, -1, false);
            }


            this.tcControl.SelectedIndex = 1;
            int maxPapeShort = this.GetFpOrder(EnumOrderType.Short).Sheets.Count;

            for (int i = 0; i < maxPapeShort; i++)
            {
                this.GetFpOrder(EnumOrderType.Short).ActiveSheetIndex = i;
                PrintPage(false, -1, -1, false);
            }


            this.tcControl.SelectedIndex = 2;
            int maxPapeOperate = this.GetFpOrder(EnumOrderType.Operate).Sheets.Count;

            for (int i = 0; i < maxPapeOperate; i++)
            {
                this.GetFpOrder(EnumOrderType.Operate).ActiveSheetIndex = i;
                FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).ActiveSheet;
                if (sheet.RowCount == 0)
                {
                    break;
                }
                PrintPage(false, -1, -1, false);
            }




            isTip = true;
        }
        /// <summary>
        /// 重新打印
        /// </summary>
        private void PrintAgain()
        {
            PrintPage(true, -1, -1, false);
        }

        /// <summary>
        /// 补打单条项目
        /// </summary>
        /// <param name="isDCReprint">是否补打停止信息，否则的话就是打印整行信息</param>
        private void PrintSingleItem(bool isDCRepint)
        {
            int rowIndex = GetFpOrder(OrderType).ActiveSheet.ActiveRowIndex;
            FS.HISFC.Models.Order.Inpatient.Order inOrder = this.GetFpOrder(OrderType).ActiveSheet.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;
            if (inOrder == null)
            {
                return;
            }
            if (inOrder.GetFlag == "0")
            {
                MessageBox.Show("项目[" + inOrder.Item.Name + "]尚未打印,不需要补打！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #region 护士未审核不能打印

            if (inOrder != null
                && !FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                //医嘱状态,0开立，1审核，2执行，3作废，4重整，5需要上级医生审核，6暂存，7预停止

                if (inOrder.Status == 5)
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]需要上级医生审核后，才能打印！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (inOrder.Status == 6)
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]为暂存状态，目前不能打印医嘱单！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ("0,1,2,5,6".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.Nurse.ID))
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]护士还未核对，不能打印医嘱单！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if ("3,4,7".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.DCNurse.ID))
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]护士还未核对，不能打印医嘱单！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion

            if (MessageBox.Show("确定要重打项目[" + inOrder.Item.Name + "]？\r\n\r\n请注意放入医嘱单！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            PrintPage(false, rowIndex, rowIndex, isDCRepint);
        }

        #endregion

        #region 获取打印提示

        /// <summary>
        /// 是否重打 true续打，false重打
        /// 如果当页已经全部打印完毕，则提示重打
        /// </summary>
        /// <returns></returns>
        private bool IsPrintAgain()
        {
            for (int i = 0; i < this.GetFpOrder(OrderType).ActiveSheet.Rows.Count; i++)
            {
                if (this.GetFpOrder(OrderType).ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order order = this.GetFpOrder(OrderType).ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (order == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return false;
                    }

                    if (order.GetFlag == "0")
                    {
                        return false;
                    }
                    else if (order.GetFlag == "1")
                    {
                        if (order.DCOper.OperTime != DateTime.MinValue

                            ////中五特殊处理
                            //&& order.OrderType.IsDecompose
                            )
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 获取页面内医嘱的行数
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private int GetValidRowCount(FarPoint.Win.Spread.SheetView sheet)
        {
            int rowCount = 0;
            for (int row = 0; row < sheet.RowCount; row++)
            {
                if (sheet.Rows[row].Tag != null)
                {
                    rowCount++;
                }
            }

            return rowCount;
        }

        /// <summary>
        /// 是否可以在本页续打
        /// </summary>
        /// <returns></returns>
        private bool CanPrint(EnumOrderType orderType, ref string errText)
        {
            if (orderType == EnumOrderType.Operate)
            {
                return true;
            }

            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(orderType).ActiveSheet;
            if (sheet.Tag == null)
            {
                errText = "获得页码出错！";
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag);

            if (pageNo < 0)
            {
                errText = "获得页码出错！\r\n页码为负数！";
                return false;
            }

            #region 判断是否有护士未审核医嘱

            string warnInfo = "";
            int warnCount = 0;
            for (int row = 0; row < sheet.RowCount; row++)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (inOrder != null)
                {
                    //医嘱状态,0开立，1审核，2执行，3作废，4重整，5需要上级医生审核，6暂存，7预停止

                    if (inOrder.Status == 5)
                    {
                        errText = "医嘱[" + inOrder.Item.Name + "]需要上级医生审核后，才能打印！";
                    }
                    else if (inOrder.Status == 6)
                    {
                        errText = "医嘱[" + inOrder.Item.Name + "]为暂存状态，目前不能打印医嘱单！";
                    }

                    if ("0,1,2,5,6".Contains(inOrder.Status.ToString())
                        && string.IsNullOrEmpty(inOrder.Nurse.ID))
                    {
                        warnInfo += inOrder.Item.Name + "\r\n";
                        warnCount += 1;
                        if (warnCount > 7)
                        {
                            break;
                        }
                    }
                    else if ("3,4,7".Contains(inOrder.Status.ToString())
                        && string.IsNullOrEmpty(inOrder.DCNurse.ID))
                    {
                        warnInfo += inOrder.Item.Name + "\r\n";
                        warnCount += 1;
                        if (warnCount > 7)
                        {
                            break;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(warnInfo)
                && !FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                errText = "以下项目护士还未核对，不能打印！\r\n\r\n" + warnInfo;

                return false;
            }

            #endregion

            #region 判断前页是否打印完全

            //获取已打印的最大页码数
            int maxPageNo = 0;
            int maxRowNo = 0;

            int UCULFlag = 0;//0 非UCUL；1 UCUL；其他全部
            if (this.tcControl.SelectedTab == tpLong)
            {
                UCULFlag = 3;
            }
            else if (tcControl.SelectedTab == tpShort)
            {
                if (isShowULOrderPrint)
                {
                    UCULFlag = 0;
                }
                else
                {
                    UCULFlag = 3;
                }
            }
            else if (tcControl.SelectedTab == tpOperate)
            {
                UCULFlag = 3;
            }
            else
            {
                UCULFlag = 1;
            }

            int rev = this.funMgr.GetPrintInfo(this.myPatientInfo.ID, orderType == EnumOrderType.Long, ref maxPageNo, ref maxRowNo, UCULFlag);
            if (rev == -1)
            {
                errText = orderManager.Err;
                return false;
            }

            if (maxPageNo > -1)
            {
                //隔页打印了
                if (pageNo > maxPageNo + 1)
                {
                    if (isCanIntervalPrint)
                    {
                        if (MessageBox.Show("第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！是否继续打印第" + (pageNo + 1).ToString() + "页？\r\n\r\n继续打印将更新所有前" + (pageNo + 1).ToString() + "页打印标记！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        errText = "第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！\r\n请先打印此页之前的医嘱单！";
                        return false;
                    }
                }
                else if (pageNo == maxPageNo + 1)
                {
                    if (maxRowNo != GetFpOrder(orderType).Sheets[maxPageNo].RowCount - 1
                        && maxRowNo != GetValidRowCount(GetFpOrder(orderType).Sheets[maxPageNo]) - 1)
                    {
                        if (isCanIntervalPrint)
                        {
                            if (MessageBox.Show("第" + (maxPageNo + 1).ToString() + "页尚有未打印医嘱！是否继续打印第" + (pageNo + 1).ToString() + "页？\r\n\r\n继续打印将更新所有前" + (pageNo + 1).ToString() + "页打印标记！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            errText = "第" + (maxPageNo + 1).ToString() + "页尚有未打印医嘱！";
                            return false;
                        }
                    }
                }
            }
            else if (pageNo != 0)
            {
                if (isCanIntervalPrint)
                {
                    if (MessageBox.Show("第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！是否继续打印第" + (pageNo + 1).ToString() + "页？\r\n\r\n继续打印将更新所有前" + (pageNo + 1).ToString() + "页打印标记！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
                else
                {
                    errText = "第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！\r\n请先打印此页之前的医嘱单！";
                    return false;
                }
            }
            #endregion

            if (isTip)
            {
                MessageBox.Show("请确定已放入第" + (pageNo + 1).ToString() + "页医嘱单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return true;
        }

        #endregion

        #region 更新打印标记

        /// <summary>
        /// 更新医嘱页码和提取标志
        /// </summary>
        /// <param name="chgBed"></param>
        /// <returns></returns>
        private int UpdatePrintFlag(FarPoint.Win.Spread.SheetView sheet)
        {
            if (sheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag);

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

            for (int row = 0; row < sheet.Rows.Count; row++)
            {
                if (sheet.Rows[row].Tag != null)
                {
                    inOrder = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (inOrder == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("实体转换出错！");
                        return -1;
                    }

                    if (inOrder.Patient.ID != this.myPatientInfo.ID)
                    {
                        continue;
                    }

                    if (inOrder.GetFlag == "2")
                    {
                        continue;
                    }

                    string newGetFlag = "1";
                    if (inOrder.GetFlag == "0")
                    {
                        newGetFlag = "1";
                        if (inOrder.DCOper.OperTime != DateTime.MinValue)
                        {
                            newGetFlag = "2";
                        }
                    }
                    else if (inOrder.GetFlag == "1")
                    {
                        if (inOrder.DCOper.OperTime != DateTime.MinValue)
                        {
                            newGetFlag = "2";
                        }
                    }


                    if (funMgr.UpdatePrintInfo(myPatientInfo.ID, inOrder.ID, pageNo.ToString(), row.ToString(), newGetFlag, inOrder.GetFlag) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新提取标志出错！\r\n项目：" + inOrder.Item.Name + "\r\n\r\n" + funMgr.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        #endregion

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ArrayList GetOrderByCombId(string combID, EnumOrderType orderType)
        {
            ArrayList alOrder = null;
            if (orderType == EnumOrderType.Long)
            {
                alOrder = alLong;
            }
            else
            {
                alOrder = alShort_ALL;
            }

            ArrayList al = new ArrayList();

            for (int i = 0; i < alOrder.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order ord = alOrder[i] as FS.HISFC.Models.Order.Inpatient.Order;

                if (combID == ord.Combo.ID)
                {
                    al.Add(ord);
                }
            }
            return al;
        }

        /// <summary>
        /// 处理换页组号打印
        /// </summary>
        /// <param name="orderType"></param>
        private void DealCrossPage(EnumOrderType orderType)
        {
            for (int i = 0; i < this.GetFpOrder(orderType).Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.GetFpOrder(orderType).Sheets[i];

                if (view.Rows[view.Rows.Count - 1].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ot = view.Rows[view.Rows.Count - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ot != null)
                    {
                        ArrayList alOrders = this.GetOrderByCombId(ot.Combo.ID, orderType);

                        if (alOrders.Count <= 1)
                        {
                            continue;
                        }
                        else
                        {
                            for (int j = 0; j < view.Rows.Count; j++)
                            {
                                FS.HISFC.Models.Order.Inpatient.Order ot1 = view.Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                if (ot1 != null)
                                {
                                    if (ot1.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)EnumCol.CombFlag, "┏");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)EnumCol.CombFlag, "┗");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (Int32)EnumCol.CombFlag, "┃");
                                    }
                                }

                            }

                            if (i != this.GetFpOrder(orderType).Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.GetFpOrder(orderType).Sheets[i + 1];

                                for (int j = 0; j < viewNext.Rows.Count; j++)
                                {
                                    FS.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)EnumCol.CombFlag, "┏");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)EnumCol.CombFlag, "┗");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (Int32)EnumCol.CombFlag, "┃");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取Sheet第一条医嘱
        /// </summary>
        /// <param name="longOrder"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Order.Inpatient.Order GetFirstOrder()
        {
            if (this.GetFpOrder(OrderType).ActiveSheet.Rows.Count > 0)
            {
                if (this.GetFpOrder(OrderType).ActiveSheet.Rows[0].Tag != null)
                {
                    return this.GetFpOrder(OrderType).ActiveSheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 初始化显示样式
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="isRefresh">是否刷新当前界面的打印标记</param>
        /// <returns></returns>
        private int InitFPShow(FarPoint.Win.Spread.SheetView sheet, bool isRefresh)
        {
            GetActBillHeader(OrderType).SetValueVisible(true);
            GetActBillHeader(OrderType).SetVisible(true);

            sheet.ActiveSkin = sheetSKin_Black;

            for (int row = 0; row < sheet.ColumnHeader.RowCount; row++)
            {
                sheet.ColumnHeader.Rows[row].ForeColor = Color.Black;
            }

            for (int row = 0; row < sheet.RowCount; row++)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (inOrder != null)
                {
                    for (int col = 0; col < sheet.ColumnCount; col++)
                    {
                        sheet.Cells[row, col].ForeColor = Color.Black;
                    }

                    if (isRefresh)
                    {
                        string newGetFlag = "1";
                        if (inOrder.GetFlag == "0")
                        {
                            newGetFlag = "1";
                            if (inOrder.DCOper.OperTime != DateTime.MinValue)
                            {
                                newGetFlag = "2";
                            }
                        }
                        else if (inOrder.GetFlag == "1")
                        {
                            if (inOrder.DCOper.OperTime != DateTime.MinValue)
                            {
                                newGetFlag = "2";
                            }
                        }

                        inOrder.PageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag);
                        inOrder.RowNo = row;

                        inOrder.GetFlag = newGetFlag;
                        sheet.Rows[row].Tag = inOrder;
                    }

                    if (inOrder.GetFlag != "0")
                    {
                        sheet.Rows[row].BackColor = Color.MistyRose;
                    }
                }
            }

            GetActPrintPanelPage(OrderType).Visible = true;

            return 1;
        }

        /// <summary>
        /// 设置打印显示
        /// </summary>
        /// <param name="isReprint">是否整页重打</param>
        /// <param name="singleLine">单独补打的行</param>
        /// <param name="isDCReprint">是否补打停止信息，否则的话就是打印整行信息</param>
        /// <returns></returns>
        private int SetPrintShow(bool isReprint, int fromLine, int endLine, bool isDCReprint)
        {
            FarPoint.Win.Spread.SheetView sheet = this.GetFpOrder(OrderType).ActiveSheet;
            FS.HISFC.Models.Order.Inpatient.Order firstOrder = sheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            sheet.PrintInfo.ShowBorder = true;

            #region 特殊打印

            if (fromLine >= 0)
            {
                GetActBillHeader(OrderType).SetValueVisible(false);
                GetActBillHeader(OrderType).SetVisible(false);

                sheet.PrintInfo.ShowBorder = false;
                GetActPrintPanelPage(OrderType).Visible = false;

                for (int row = 0; row < sheet.ColumnHeader.RowCount; row++)
                {
                    sheet.ColumnHeader.Rows[row].ForeColor = Color.White;
                }

                sheet.ActiveSkin = sheetSKin_White;

                if (endLine < 0)
                {
                    endLine = sheet.RowCount;
                }

                //设置背景色为白色
                for (int row = 0; row < sheet.RowCount; row++)
                {
                    for (int col = 0; col < sheet.ColumnCount; col++)
                    {
                        sheet.Cells[row, col].ForeColor = Color.White;

                        if (row >= fromLine && row <= endLine)
                        {
                            //取消的临嘱打印“取消字样”
                            FS.HISFC.Models.Order.Inpatient.Order inOrder = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                            if (isDCReprint)
                            {
                                if (col == (Int32)EnumCol.DCConfirmNurse
                                    || col == (Int32)EnumCol.DCDate
                                    || col == (Int32)EnumCol.DCTime
                                    || col == (Int32)EnumCol.DCDoct
                                    || col == (Int32)EnumCol.DCFlag)
                                {
                                    sheet.Cells[row, col].ForeColor = Color.Black;

                                    //中五特殊处理
                                    if (!inOrder.OrderType.IsDecompose)
                                    {
                                        if (col == (Int32)EnumCol.DCDate
                                        || col == (Int32)EnumCol.DCTime
                                            )
                                        {
                                            sheet.Cells[row, col].ForeColor = Color.White;
                                        }
                                    }
                                }
                                if (inOrder != null
                                    && inOrder.GetFlag == "1"
                                    && !inOrder.OrderType.IsDecompose
                                    && inOrder.Status == 3)
                                {
                                    //if (col == (Int32)EnumCol.ItemName)
                                    //{
                                    //    sheet.Cells[row, col].ForeColor = Color.Black;

                                    //    string itemName = sheet.Cells[row, col].Text;
                                    //    itemName = "".PadLeft(GetLength(itemName), ' ') + " 取消";
                                    //    sheet.Cells[row, col].Text = itemName;
                                    //}

                                    if (col == (Int32)EnumCol.DCDoct)
                                    {
                                        sheet.Cells[row, col].ForeColor = Color.Black;

                                        string itemName = sheet.Cells[row, col].Text.Replace(" 取消", "");
                                        itemName = "".PadLeft(GetLength(itemName), ' ') + " 取消";
                                        sheet.Cells[row, col].Text = itemName;
                                    }
                                }
                            }
                            else
                            {
                                //补打某条医嘱，就连停止信息也打印出来吧
                                //续打走的也是这里
                                sheet.Cells[row, col].ForeColor = Color.Black;

                                //if (!(col == (Int32)EnumCol.DCConfirmNurse
                                //       || col == (Int32)EnumCol.DCDate
                                //       || col == (Int32)EnumCol.DCTime
                                //       || col == (Int32)EnumCol.DCDoct
                                //       || col == (Int32)EnumCol.DCFlag))
                                //{
                                //    sheet.Cells[row, col].ForeColor = Color.Black;
                                //}
                            }
                        }
                    }
                }
            }
            #endregion

            #region 正常打印
            else
            {
                if (OrderPrintType == EnumPrintType.PrintWhenPatientOut)
                {
                    GetActBillHeader(OrderType).SetValueVisible(true);
                    GetActBillHeader(OrderType).SetVisible(true);
                    GetActPrintPanelPage(OrderType).Visible = true;

                    sheet.ActiveSkin = sheetSKin_Black;
                }
                else
                {
                    //首次打印和重打
                    if (firstOrder.GetFlag == "0"
                        || isReprint)
                    {
                        GetActBillHeader(OrderType).SetValueVisible(true);

                        //印刷续打
                        if (OrderPrintType == EnumPrintType.DrawPaperContinue)
                        {
                            GetActBillHeader(OrderType).SetVisible(false);
                            GetActPrintPanelPage(OrderType).Visible = false;

                            sheet.ActiveSkin = sheetSKin_White;
                        }
                        //白纸续打
                        else if (OrderPrintType == EnumPrintType.WhitePaperContinue)
                        {
                            GetActBillHeader(OrderType).SetVisible(true);
                            GetActPrintPanelPage(OrderType).Visible = true;

                            sheet.ActiveSkin = sheetSKin_Black;
                        }

                        if (isReprint)
                        {
                            //设置背景色为白色
                            for (int row = 0; row < sheet.RowCount; row++)
                            {
                                FS.HISFC.Models.Order.Inpatient.Order inOrder = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                if (inOrder != null)
                                {
                                    if (inOrder.GetFlag == "0")
                                    {
                                        for (int col = 0; col < sheet.ColumnCount; col++)
                                        {
                                            sheet.Cells[row, col].ForeColor = Color.White;
                                        }
                                    }
                                    else if (inOrder.GetFlag == "1"
                                        && inOrder.DCOper.OperTime != DateTime.MinValue)
                                    {
                                        for (int col = 0; col < sheet.ColumnCount; col++)
                                        {
                                            sheet.Cells[row, col].ForeColor = Color.Black;
                                            if (col == (Int32)EnumCol.DCConfirmNurse
                                                || col == (Int32)EnumCol.DCDate
                                                || col == (Int32)EnumCol.DCTime
                                                || col == (Int32)EnumCol.DCDoct
                                                || col == (Int32)EnumCol.DCFlag)
                                            {
                                                sheet.Cells[row, col].ForeColor = Color.White;
                                                //中五特殊处理
                                                //中五特殊处理
                                                if (!inOrder.OrderType.IsDecompose)
                                                {
                                                    if (col == (Int32)EnumCol.DCDate
                                                    || col == (Int32)EnumCol.DCTime
                                                        )
                                                    {
                                                        sheet.Cells[row, col].ForeColor = Color.Black;
                                                    }
                                                }
                                            }

                                            //取消的临嘱打印“取消字样”
                                            FS.HISFC.Models.Order.Inpatient.Order inOrder2 = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                                            if (inOrder2 != null
                                                && !inOrder2.OrderType.IsDecompose
                                                && inOrder2.Status == 3)
                                            {
                                                //if (col == (Int32)EnumCol.ItemName)
                                                //{
                                                //    sheet.Cells[row, col].ForeColor = Color.Black;

                                                //    string itemName = sheet.Cells[row, col].Text;
                                                //    itemName = "".PadLeft(GetLength(itemName), ' ') + " 取消";
                                                //    sheet.Cells[row, col].Text = itemName;
                                                //}

                                                if (col == (Int32)EnumCol.DCDoct)
                                                {
                                                    sheet.Cells[row, col].ForeColor = Color.Black;

                                                    string itemName = sheet.Cells[row, col].Text.Replace(" 取消", "");

                                                    if (!isReprint)
                                                    {
                                                        itemName = "".PadLeft(GetLength(itemName), ' ') + " 取消";
                                                    }
                                                    sheet.Cells[row, col].Text = itemName;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //接着续打，要设置已经打印过的不显示
                    else
                    {
                        GetActBillHeader(OrderType).SetValueVisible(false);
                        GetActBillHeader(OrderType).SetVisible(false);

                        sheet.PrintInfo.ShowBorder = false;
                        GetActPrintPanelPage(OrderType).Visible = false;

                        for (int row = 0; row < sheet.ColumnHeader.RowCount; row++)
                        {
                            sheet.ColumnHeader.Rows[row].ForeColor = Color.White;
                        }

                        sheet.ActiveSkin = sheetSKin_White;

                        //设置背景色为白色
                        for (int row = 0; row < sheet.RowCount; row++)
                        {
                            FS.HISFC.Models.Order.Inpatient.Order inOrder = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                            if (inOrder != null)
                            {
                                if (inOrder.GetFlag == "1")
                                {
                                    if (inOrder.DCOper.OperTime != DateTime.MinValue)
                                    {
                                        for (int col = 0; col < sheet.ColumnCount; col++)
                                        {
                                            sheet.Cells[row, col].ForeColor = Color.Black;
                                            if (col != (Int32)EnumCol.DCConfirmNurse
                                                && col != (Int32)EnumCol.DCDate
                                                && col != (Int32)EnumCol.DCTime
                                                && col != (Int32)EnumCol.DCDoct
                                                && col != (Int32)EnumCol.DCFlag
                                                )
                                            {
                                                sheet.Cells[row, col].ForeColor = Color.White;
                                            }

                                            //中五特殊处理
                                            if (!inOrder.OrderType.IsDecompose)
                                            {
                                                if (col == (Int32)EnumCol.DCDate
                                                || col == (Int32)EnumCol.DCTime
                                                    )
                                                {
                                                    sheet.Cells[row, col].ForeColor = Color.White;
                                                }
                                            }

                                            //取消的临嘱打印“取消字样”
                                            FS.HISFC.Models.Order.Inpatient.Order inOrder3 = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                                            if (inOrder3 != null
                                                && !inOrder3.OrderType.IsDecompose
                                                && inOrder3.Status == 3)
                                            {
                                                //if (col == (Int32)EnumCol.ItemName)
                                                //{
                                                //    sheet.Cells[row, col].ForeColor = Color.Black;

                                                //    string itemName = sheet.Cells[row, col].Text;
                                                //    itemName = "".PadLeft(GetLength(itemName), ' ') + " 取消";
                                                //    sheet.Cells[row, col].Text = itemName;
                                                //}
                                                if (col == (Int32)EnumCol.DCDoct)
                                                {
                                                    sheet.Cells[row, col].ForeColor = Color.Black;

                                                    string itemName = sheet.Cells[row, col].Text.Replace(" 取消", "");
                                                    itemName = "".PadLeft(GetLength(itemName), ' ') + " 取消";
                                                    sheet.Cells[row, col].Text = itemName;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int col = 0; col < sheet.ColumnCount; col++)
                                        {
                                            sheet.Cells[row, col].ForeColor = Color.White;
                                        }
                                    }
                                }
                                else if (inOrder.GetFlag == "2")
                                {
                                    for (int col = 0; col < sheet.ColumnCount; col++)
                                    {
                                        sheet.Cells[row, col].ForeColor = Color.White;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            //设置背景色为白色
            for (int row = 0; row < sheet.RowCount; row++)
            {
                sheet.Rows[row].BackColor = Color.White;

                if (!isPrintReciptDoct)
                {
                    sheet.Cells[row, (Int32)EnumCol.RecipeDoct].Text = "";
                }
                if (!isPrintConfirmNurse)
                {
                    sheet.Cells[row, (Int32)EnumCol.ConfirmNurse].Text = "";
                }

                if (!isPrintDCDoct)
                {
                    sheet.Cells[row, (Int32)EnumCol.DCDoct].Text = "";
                }
                if (!isPrintDCConfirmNurse)
                {
                    sheet.Cells[row, (Int32)EnumCol.DCConfirmNurse].Text = "";
                }
            }

            return 1;
        }

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int GetLength(string str)
        {
            return Encoding.Default.GetByteCount(str);
        }

        #region 重置医嘱

        /// <summary>
        /// 重置当前患者医嘱单打印状态
        /// </summary>
        /// <param name="type">类型：长嘱、临嘱</param>
        /// <param name="pagNo">更新页码 -1表示全部重置</param>
        /// <param name="ResetType">重置类型：全部、部分</param>
        /// <param name="UCULFlag">0 非UCUL；1 UCUL；其他全部</param>
        private void ReSet(EnumOrderType type, int pagNo, string ResetType, int UCULFlag)
        {
            if (this.myPatientInfo == null || this.myPatientInfo.ID == "")
            {
                return;
            }

            string orderType = "ALL";
            string page = pagNo == -1 ? "全部" : "第" + pagNo.ToString() + "页";

            string message = "医嘱单";
            switch (type)
            {
                case EnumOrderType.Long:
                    //message = "长期医嘱单";
                    orderType = "1";
                    break;
                case EnumOrderType.Short:
                    //message = "临时医嘱单";
                    orderType = "0";
                    break;
                default:
                    break;
            }

            #region 重置全部
            if (ResetType == "ALL")  //重置全部
            {
                if ((this.isShowULOrderPrint && UCULFlag == 1) || UCULFlag == 0)// {1B360F77-7C78-4614-B61A-D9542D57C603}
                {
                    DialogResult rr = MessageBox.Show("即将取消" + page + message + "的打印状态，\r\n\r\n重置后所有医嘱都需要重新打印，\r\n\r\n是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (rr == DialogResult.No)
                    {
                        return;
                    }
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (this.funMgr.ResetOrderPrint("-1", "-1", myPatientInfo.ID, orderType, "0", UCULFlag) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("重置失败!" + this.orderManager.Err);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion

            #region 重置当前页
            else if (ResetType == "PART")
            {
                //获取已打印的最大页码数
                int maxPageNo = 0;
                int maxRowNo = 0;
                int rev = this.funMgr.GetPrintInfo(this.myPatientInfo.ID, type == EnumOrderType.Long, ref maxPageNo, ref maxRowNo, UCULFlag);
                if (rev == -1)
                {
                    MessageBox.Show("查询已打印页码出现错误！\r\n" + funMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (pagNo - 1 < maxPageNo)
                {
                    MessageBox.Show("后面存在已打印的医嘱单，请从后往前重置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if ((this.isShowULOrderPrint && UCULFlag == 1) || UCULFlag == 0)// {1B360F77-7C78-4614-B61A-D9542D57C603}
                {
                    DialogResult rr = MessageBox.Show("即将取消" + page + message + "的打印状态，\r\n\r\n重置后该页所有医嘱都需要重新打印，\r\n\r\n是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (rr == DialogResult.No)
                    {
                        return;
                    }
                }

                FarPoint.Win.Spread.SheetView sheet = GetFpOrder(type).ActiveSheet;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                #region 获取当前选中页内明细项目的ID

                StringBuilder buffer = new StringBuilder();

                for (int i = 0; i < sheet.Rows.Count; i++)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = sheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (oT != null)
                    {
                        buffer.Append("'");
                        buffer.Append(oT.ID);
                        buffer.Append("',");
                    }
                }

                string number = buffer.ToString(0, buffer.Length - 1);

                #endregion

                if (this.funMgr.ResetOrderPrintPart("-1", "-1", myPatientInfo.ID, orderType, "0", number, UCULFlag) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("重置失败!" + this.funMgr.Err);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion
            if ((this.isShowULOrderPrint && UCULFlag == 1) || UCULFlag == 0)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {
                MessageBox.Show("重置成功!");
            }

            orderQueryCount = 1;

            FS.SOC.Windows.Forms.FpSpread fpSpread = null;

            if (tcControl.SelectedTab == this.tpLong)
            {
                fpSpread = this.fpLongOrder;
            }
            else
            {
                fpSpread = fpShortOrder;
            }

            int select = fpSpread.ActiveSheetIndex;

            this.QueryPatientOrder();

            fpSpread.ActiveSheetIndex = select;
        }

        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }

            string patientNo = this.ucQueryInpatientNo1.InpatientNo;
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.QueryPatientInfoByInpatientNO(patientNo);

            TreeNode root = new TreeNode();
            root.Text = "住院信息:" + "[" + patientInfo.Name + "]" + "[" + patientInfo.PID.PatientNO + "]";
            this.treeView1.Nodes.Add(root);

            TreeNode node = new TreeNode();
            node.Text = "[" + patientInfo.PVisit.InTime.ToShortDateString() + "][" + patientInfo.PVisit.PatientLocation.Dept.Name + "]";
            node.Tag = patientInfo;
            root.Nodes.Add(node);

            this.treeView1.ExpandAll();

            this.treeView1.SelectedNode = node;

            //if (this.treeView1.Nodes.Count > 0)
            //{
            //    if (this.treeView1.Nodes[0].Nodes.Count > 0)
            //    {
            //        this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
            //    }
            //}
        }

        #region 工具栏

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (this.isShowULOrderPrint)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {
                //打印
                if (e.ClickedItem == this.tbPrint)
                {
                    this.Print();
                }
                //打印全部
                else if (e.ClickedItem == this.tbAllPrint)
                {
                    this.AllPrint();// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
                }
                //续打
                else if (e.ClickedItem == this.tbRePrint)
                {
                    this.PrintAgain();
                }
                //关闭窗体
                else if (e.ClickedItem == this.tbExit)
                {
                    this.Close();
                }
                //重置长嘱
                else if (e.ClickedItem == this.ResetLong)
                {
                    this.ReSet(EnumOrderType.Long, -1, "ALL", 3);
                }
                //重置临嘱
                else if (e.ClickedItem == this.ResetShort)
                {
                    this.ReSet(EnumOrderType.Short, -1, "ALL", 0);
                }
                //重置检查检验医嘱
                else if (e.ClickedItem == this.ResetUCUL)
                {
                    this.ReSet(EnumOrderType.Short, -1, "ALL", 1);
                }
                //重置全部医嘱
                else if (e.ClickedItem == this.ResetAll)
                {
                    this.ReSet(EnumOrderType.All, -1, "ALL", 3);
                }
                //重置当前页长嘱
                else if (e.ClickedItem == this.ResetCurrentLong)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //重置当前页临嘱
                else if (e.ClickedItem == this.ResetCurrentShort)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //重置当前页检查检验医嘱
                else if (e.ClickedItem == this.ResetCurrentUCUL)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //刷新所有长嘱打印状态：全部重置，并更新打印状态
                else if (e.ClickedItem == this.RefreshLong)
                {
                }
                //刷新所有临嘱打印状态：全部重置，并更新打印状态
                else if (e.ClickedItem == this.RefreshShort)
                {
                }
                else if (e.ClickedItem == this.tbDeleteLongProfile)
                {
                    if (System.IO.File.Exists(this.LongOrderSetXML))
                    {
                        System.IO.File.Delete(LongOrderSetXML);
                    }
                }
                else if (e.ClickedItem == this.tbDeleteShortProfile)
                {
                    if (System.IO.File.Exists(this.shortOrderSetXML))
                    {
                        System.IO.File.Delete(shortOrderSetXML);
                    }
                }
                else if (e.ClickedItem == this.tbQuery)
                {
                    QueryPatientOrder();
                }
            }
            else
            {
                //打印
                if (e.ClickedItem == this.tbPrint)
                {
                    this.Print();
                }

                //打印全部
                else if (e.ClickedItem == this.tbAllPrint)
                {
                    this.AllPrint();// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
                }
                //续打
                else if (e.ClickedItem == this.tbRePrint)
                {
                    this.PrintAgain();
                }
                //关闭窗体
                else if (e.ClickedItem == this.tbExit)
                {
                    this.Close();
                }
                //重置长嘱
                else if (e.ClickedItem == this.ResetLong)
                {
                    this.ReSet(EnumOrderType.Long, -1, "ALL", 3);
                }
                //重置临嘱
                else if (e.ClickedItem == this.ResetShort)
                {
                    this.ReSet(EnumOrderType.Short, -1, "ALL", 3);
                }
                //重置全部医嘱
                else if (e.ClickedItem == this.ResetAll)
                {
                    this.ReSet(EnumOrderType.All, -1, "ALL", 3);
                }
                //重置当前页长嘱
                else if (e.ClickedItem == this.ResetCurrentLong)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //重置当前页临嘱
                else if (e.ClickedItem == this.ResetCurrentShort)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //刷新所有长嘱打印状态：全部重置，并更新打印状态
                else if (e.ClickedItem == this.RefreshLong)
                {
                }
                //刷新所有临嘱打印状态：全部重置，并更新打印状态
                else if (e.ClickedItem == this.RefreshShort)
                {
                }
                else if (e.ClickedItem == this.tbDeleteLongProfile)
                {
                    if (System.IO.File.Exists(this.LongOrderSetXML))
                    {
                        System.IO.File.Delete(LongOrderSetXML);
                    }
                }
                else if (e.ClickedItem == this.tbDeleteShortProfile)
                {
                    if (System.IO.File.Exists(this.shortOrderSetXML))
                    {
                        System.IO.File.Delete(shortOrderSetXML);
                    }
                }
                else if (e.ClickedItem == this.tbQuery)
                {
                    QueryPatientOrder();
                }
            }
        }

        #endregion

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                FS.HISFC.Models.RADT.PatientInfo temp = e.Node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (temp == null)
                {
                    temp = this.inPatientMgr.QueryPatientInfoByInpatientNO(((FS.FrameWork.Models.NeuObject)e.Node.Tag).ID);
                }

                if (temp != null)
                {
                    this.ucShortOrderBillHeader.SetPatientInfo(temp);
                    this.ucLongOrderBillHeader.SetPatientInfo(temp);
                    this.ucOperateBillHeader.SetPatientInfo(temp);
                    this.myPatientInfo = temp;
                    orderQueryCount = 1;
                    this.QueryPatientOrder();
                }
            }
        }

        /// <summary>
        /// 保存左边距
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuLeft_ValueChanged(object sender, EventArgs e)
        {
            this.leftValue = FS.FrameWork.Function.NConvert.ToInt32(this.nuLeft.Value);
            this.SetPrintValue("左边距", this.leftValue.ToString());
        }

        /// <summary>
        /// 保存打印行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuRowNum_ValueChanged(object sender, EventArgs e)
        {
            this.defaultRowNum = FS.FrameWork.Function.NConvert.ToInt32(this.nuRowNum.Value);
            this.SetPrintValue("行数", this.defaultRowNum.ToString());
        }

        /// <summary>
        /// 上边距
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuTop_ValueChanged(object sender, EventArgs e)
        {
            this.topValue = FS.FrameWork.Function.NConvert.ToInt32(this.nuTop.Value);
            this.SetPrintValue("上边距", this.topValue.ToString());
        }

        /// <summary>
        /// 选择打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbPrinter.SelectedItem == null)
            {
                this.SetPrintValue("医嘱单", "");
                print.PrintDocument.PrinterSettings.PrinterName = "";
            }
            else
            {
                this.printerName = tbPrinter.SelectedItem.ToString();

                this.SetPrintValue("医嘱单", this.printerName);

                if (!string.IsNullOrEmpty(this.printerName))
                {
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        if (PrinterSettings.InstalledPrinters[i] != null && PrinterSettings.InstalledPrinters[i].ToString().Contains(this.printerName))
                        {
                            print.PrintDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置打印机
        /// </summary>
        private void SetPrintValue(string nodeName, string nodeValue)
        {
            FS.FrameWork.Xml.XML xml = new FS.FrameWork.Xml.XML();
            if (!File.Exists(fileName))
            {
                XmlDocument doc = new XmlDocument();
                xml.CreateRootElement(doc, "ORDERPRINT");
                xml.AddXmlNode(doc, doc.DocumentElement, nodeName, nodeValue);
                doc.Save(fileName);
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode node = doc.SelectSingleNode("ORDERPRINT/" + nodeName);
                if (node != null)
                {
                    node.InnerText = nodeValue;
                }
                else
                {
                    if (nodeValue != null)
                    {
                        xml.AddXmlNode(doc, doc.DocumentElement, nodeName, nodeValue);
                    }
                }
                doc.Save(fileName);
            }
        }

        /// <summary>
        /// 长嘱切换显示页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpLongOrder_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            if (this.fpLongOrder.Sheets.Count > 0 &&
                this.fpLongOrder.ActiveSheet.Tag != null)
            {
                this.lblPageLong.Text = "第" + (FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag) + 1).ToString() + "页";

                this.ucLongOrderBillHeader.SetChangedInfo(GetFirstOrder());
            }
        }

        /// <summary>
        /// 临嘱切换显示页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpShortOrder_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpShortOrder.Sheets.Count > 0 &&
                this.fpShortOrder.ActiveSheet.Tag != null)
            {
                this.lblPageShort.Text = "第" + (FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag) + 1).ToString() + "页";

                this.ucShortOrderBillHeader.SetChangedInfo(GetFirstOrder());
            }
        }

        /// <summary>
        /// 手术切换显示页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpOperateOrder_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpOperateOrder.Sheets.Count > 0 &&
                    this.fpOperateOrder.ActiveSheet.Tag != null)
            {
                this.lblPageOperatets.Text = "第" + (FS.FrameWork.Function.NConvert.ToInt32(this.fpOperateOrder.ActiveSheet.Tag) + 1).ToString() + "页";


                this.ucOperateBillHeader.SetChangedInfo(GetFirstOrder());

            }

        }

        /// <summary>
        /// 长嘱、临嘱切换显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (true)
            {
                this.QueryPatientOrder();
                orderQueryCount++;
            }
            if (isShowULOrderPrint)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {
                if (this.tcControl.SelectedTab == this.tpShort)
                {
                    ShowShortOrder(false, false);
                }
                else if (this.tcControl.SelectedTab == this.tpUCUL)
                {
                    ShowShortOrder(true, false);
                }
                else if (this.tcControl.SelectedTab == this.tpOperate)
                {
                    ShowShortOrder(true, true);
                }
            }
            else
            {
                if (this.tcControl.SelectedTab == this.tpShort)
                {
                    ShowShortOrder(false, false);
                }
                else if (this.tcControl.SelectedTab == this.tpOperate)
                {
                    ShowShortOrder(false, true);
                }
            }
        }

        private void ShowShortOrder(bool isUCUL, bool isOperate)
        {
            this.Clear(EnumOrderType.Short);
            //this.Clear(EnumOrderType.Operate);
            if (isShowULOrderPrint)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {
                if (isUCUL)
                {
                    //增加显示检查检验医嘱单显示
                    this.tpUCUL.Controls.Clear();
                    this.tpUCUL.Controls.Add(pnShortOrder);

                    this.AddOrderToFP(EnumOrderType.Short, alShort_UCUL, -1, 0);
                    this.ucShortOrderBillHeader.Header = "检查检验医嘱单";
                }
                else
                {
                    if (isOperate)
                    {

                        //tpOperate.Controls.Clear();
                        //tpOperate.Controls.Add(pnOperateOrder);
                        this.AddOrderToFP(EnumOrderType.Operate, alOperate, -1, 0);
                        this.ucShortOrderBillHeader.Header = "手  术  医  嘱  单";
                    }
                    else
                    {
                        tpShort.Controls.Clear();
                        tpShort.Controls.Add(pnShortOrder);

                        this.AddOrderToFP(EnumOrderType.Short, alShort_Normal, -1, 0);
                        this.ucShortOrderBillHeader.Header = "临  时  医  嘱  单";
                    }

                }
            }
            else
            {
                if (isOperate)
                {
                    //tpOperate.Controls.Clear();
                    //tpOperate.Controls.Add(pnOperateOrder);
                    this.AddOrderToFP(EnumOrderType.Operate, alOperate, -1, 0);
                    this.ucShortOrderBillHeader.Header = "手  术  医  嘱  单";
                }
                else
                {

                    tpShort.Controls.Clear();
                    tpShort.Controls.Add(pnShortOrder);

                    this.AddOrderToFP(EnumOrderType.Short, alShort_ALL, -1, 0);
                    this.ucShortOrderBillHeader.Header = "临  时  医  嘱  单";
                }


            }

            if (isOperate)
            {
                this.ucOperateBillHeader.SetChangedInfo(this.GetFirstOrder());
                this.SetFPStyle(EnumOrderType.Operate);
            }
            else
            {
                this.ucShortOrderBillHeader.SetChangedInfo(this.GetFirstOrder());
                this.SetFPStyle(EnumOrderType.Short);
            }


        }

        #region 打印

        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpLongOrder_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "补打该条长期医嘱";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);
                this.popMenu.MenuItems.Add(printMenuItem);

                System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                printDateItem.Text = "只补打该条长期医嘱停止时间";
                printDateItem.Click += new EventHandler(printDateItem_Click);
                this.popMenu.MenuItems.Add(printDateItem);

                System.Windows.Forms.MenuItem printAfterItem = new MenuItem();
                printAfterItem.Text = "续打该条医嘱以后的医嘱";
                printAfterItem.Click += new EventHandler(printAfterItem_Click);
                this.popMenu.MenuItems.Add(printAfterItem);

                System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                splitMenuItem.Text = "从该条医嘱往后另起一页";
                splitMenuItem.Click += new EventHandler(splitMenuItem_Click);
                this.popMenu.MenuItems.Add(splitMenuItem);

                System.Windows.Forms.MenuItem mnSetPrinted = new MenuItem();
                mnSetPrinted.Text = "设置该条医嘱已打印";
                mnSetPrinted.Click += new EventHandler(mnSetPrinted_Click);
                this.popMenu.MenuItems.Add(mnSetPrinted);

                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    //还没调试好，先屏蔽吧
                    System.Windows.Forms.MenuItem mnAddBlankLine = new MenuItem();
                    mnAddBlankLine.Text = "增加空白行";
                    mnAddBlankLine.Click += new EventHandler(mnAddBlankLine_Click);
                    this.popMenu.MenuItems.Add(mnAddBlankLine);
                }

                System.Windows.Forms.MenuItem mnPrintStart = new MenuItem();
                mnPrintStart.Text = "下次从此条医嘱开始打印";
                mnPrintStart.Click += new EventHandler(mnPrintStart_Click);
                this.popMenu.MenuItems.Add(mnPrintStart);

                System.Windows.Forms.MenuItem resetPartPage = new MenuItem();
                resetPartPage.Text = "重置医嘱单当前页";
                resetPartPage.Click += new EventHandler(resetPartPage_Click);
                this.popMenu.MenuItems.Add(resetPartPage);

                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    System.Windows.Forms.MenuItem mnDeleteProfile = new MenuItem();
                    mnDeleteProfile.Text = "删除当前类型医嘱的界面显示配置文件";
                    mnDeleteProfile.Click += new EventHandler(mnDeleteProfile_Click);
                    this.popMenu.MenuItems.Add(mnDeleteProfile);
                }

                this.popMenu.Show(this.fpLongOrder, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpShortOrder_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "补打该条临时医嘱";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);
                this.popMenu.MenuItems.Add(printMenuItem);

                System.Windows.Forms.MenuItem splitShortMenuItem = new MenuItem();
                splitShortMenuItem.Text = "从该条医嘱往后另起一页";
                splitShortMenuItem.Click += new EventHandler(splitShortMenuItem_Click);
                this.popMenu.MenuItems.Add(splitShortMenuItem);

                System.Windows.Forms.MenuItem printAfterItem = new MenuItem();
                printAfterItem.Text = "续打该条医嘱以后的医嘱";
                printAfterItem.Click += new EventHandler(printAfterItem_Click);
                this.popMenu.MenuItems.Add(printAfterItem);

                System.Windows.Forms.MenuItem mnSetPrinted = new MenuItem();
                mnSetPrinted.Text = "设置该条医嘱已打印";
                mnSetPrinted.Click += new EventHandler(mnSetPrinted_Click);
                this.popMenu.MenuItems.Add(mnSetPrinted);


                System.Windows.Forms.MenuItem mnAddBlankLine = new MenuItem();
                mnAddBlankLine.Text = "增加空白行";
                mnAddBlankLine.Click += new EventHandler(mnAddBlankLine_Click);
                this.popMenu.MenuItems.Add(mnAddBlankLine);


                System.Windows.Forms.MenuItem mnPrintStart = new MenuItem();
                mnPrintStart.Text = "下次从此条医嘱开始打印";
                mnPrintStart.Click += new EventHandler(mnPrintStart_Click);
                this.popMenu.MenuItems.Add(mnPrintStart);

                System.Windows.Forms.MenuItem resetPartPage = new MenuItem();
                resetPartPage.Text = "重置医嘱单当前页";
                resetPartPage.Click += new EventHandler(resetPartPage_Click);
                this.popMenu.MenuItems.Add(resetPartPage);

                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    System.Windows.Forms.MenuItem mnDeleteProfile = new MenuItem();
                    mnDeleteProfile.Text = "删除当前类型医嘱的界面显示配置文件";
                    mnDeleteProfile.Click += new EventHandler(mnDeleteProfile_Click);
                    this.popMenu.MenuItems.Add(mnDeleteProfile);
                }

                this.popMenu.Show(this.fpShortOrder, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 设置该条医嘱已打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnSetPrinted_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order inOrder = null;
            FS.HISFC.Models.Order.Inpatient.Order preOrder = null;

            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).ActiveSheet;


            //最大页码
            int maxPageNo = 0;
            //当前页码
            int pageNo = 0;
            //行号
            int rowNum = -1;

            inOrder = sheet.Rows[sheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            #region 护士未审核不能打印

            if (inOrder != null
                && !FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                //医嘱状态,0开立，1审核，2执行，3作废，4重整，5需要上级医生审核，6暂存，7预停止

                if (inOrder.Status == 5)
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]需要上级医生审核后，才能打印！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (inOrder.Status == 6)
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]为暂存状态，目前不能打印医嘱单！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ("0,1,2,5,6".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.Nurse.ID))
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]护士还未核对，不能打印医嘱单！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if ("3,4,7".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.DCNurse.ID))
                {
                    MessageBox.Show("医嘱[" + inOrder.Item.Name + "]护士还未核对，不能打印医嘱单！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion

            if (MessageBox.Show("是否设置医嘱【" + inOrder.Item.Name + "】为已打印状态？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "1");
            pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag.ToString());

            if (pageNo > maxPageNo + 1)
            {
                MessageBox.Show("前一页医嘱未打印！\r\n\r\n不能继续设置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sheet.ActiveRowIndex > 0)
            {
                preOrder = sheet.Rows[sheet.ActiveRowIndex - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                rowNum = preOrder.RowNo;
            }
            //上一页
            else if (pageNo > 0)
            {
                preOrder = GetFpOrder(OrderType).Sheets[GetFpOrder(OrderType).ActiveSheetIndex - 1].Rows[GetFpOrder(OrderType).Sheets[GetFpOrder(OrderType).ActiveSheetIndex - 1].RowCount - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
            }

            if (inOrder == null)
            {
                return;
            }

            if (inOrder.PageNo != -1 || inOrder.RowNo != -1)
            {
                MessageBox.Show("该条医嘱已经打印！\r\n\r\n不能继续设置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (preOrder != null && preOrder.RowNo == -1)
            {
                MessageBox.Show("上一条医嘱还未打印，请先设置上一条医嘱的打印状态！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //后面可以修改为 按组更新打印标记，但是要考虑 一个组合在两页的情况
            string updateSQL = @"update met_ipm_order f
                                set f.rowno={3},
                                f.pageno={2},
                                f.get_flag='1'
                                where f.mo_order='{1}'
                                and f.inpatient_no='{0}'";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (orderManager.ExecNoQuery(updateSQL, inOrder.Patient.ID, inOrder.ID, pageNo.ToString(), (rowNum + 1).ToString()) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新打印标记出错！\r\n\r\n" + orderManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("更新成功！\r\n\r\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            inOrder.GetFlag = "1";
            inOrder.PageNo = pageNo;
            inOrder.RowNo = rowNum + 1;

            this.AddToRow(true, sheet, sheet.ActiveRowIndex, inOrder);
        }

        private void mnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (OrderType == EnumOrderType.Long)
            {
                if (System.IO.File.Exists(this.LongOrderSetXML))
                {
                    System.IO.File.Delete(LongOrderSetXML);
                }
            }
            else
            {
                if (System.IO.File.Exists(this.shortOrderSetXML))
                {
                    System.IO.File.Delete(shortOrderSetXML);
                }
            }
        }

        /// <summary>
        ///  重置医嘱单选中页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetPartPage_Click(object sender, EventArgs e)
        {
            //获取页数
            int pagNo = FS.FrameWork.Function.NConvert.ToInt32(this.GetFpOrder(OrderType).ActiveSheet.Tag) + 1;

            int UCULFlag = 0;

            if (this.isShowULOrderPrint)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {

                if (tcControl.SelectedTab == this.tpUCUL)
                {
                    UCULFlag = 1;
                }
                else if (tcControl.SelectedTab == this.tpLong)
                {
                    UCULFlag = 3;

                }
                this.ReSet(OrderType, pagNo, "PART", UCULFlag);
            }
            else
            {
                if (tcControl.SelectedTab == this.tpShort)
                {
                    UCULFlag = 3;
                    this.ReSet(OrderType, pagNo, "PART", UCULFlag);
                }
                else if (tcControl.SelectedTab == this.tpLong)
                {
                    UCULFlag = 3;

                    this.ReSet(OrderType, pagNo, "PART", UCULFlag);
                }
            }


        }

        /// <summary>
        /// 下次从此条医嘱开始打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnPrintStart_Click(object sender, EventArgs e)
        {
            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).ActiveSheet;

            if (sheet.ActiveRowIndex == -1)
            {
                MessageBox.Show("请选中从下次开始打印的医嘱！");
                return;
            }

            int rowIndex = sheet.ActiveRowIndex;
            FS.HISFC.Models.Order.Inpatient.Order orderTemp = sheet.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            if (MessageBox.Show("是否设置医嘱【" + orderTemp.Item.Name + "】以及该医嘱以后的医嘱为未打印状态？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int pageNO = orderTemp.PageNo;
            int rowNO = orderTemp.RowNo;

            string inpatientNO = orderTemp.Patient.ID;

            int flag = (OrderType == EnumOrderType.Long ? 1 : 0);//长期医嘱 1；临时医嘱 0


            if (pageNO == -1 || rowNO == -1)
            {
                MessageBox.Show("该条医嘱已经是未打印状态", "提示", MessageBoxButtons.OK);

                return;
            }

            string updateSQL = @"update met_ipm_order f
                                   set f.rowno = -1, f.pageno = -1, f.get_flag = '0'
                                 where f.inpatient_no = '{0}'
                                   and f.decmps_state = '{3}'
                                   and ((f.pageno = '{1}' and f.rowno >= {2}) or f.pageno > {1})";
            if (this.tcControl.SelectedTab == this.tpUCUL)
            {
                updateSQL += "\r\n" + "   and f.class_code in ('UC', 'UL')";
            }
            else
            {
                updateSQL += "\r\n" + "   and f.class_code not in ('UC', 'UL')";
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (orderManager.ExecNoQuery(updateSQL, inpatientNO, pageNO.ToString(), rowNO.ToString(), flag.ToString()) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新打印标记出错！\r\n\r\n" + orderManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("更新成功！\r\n\r\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.QueryPatientOrder();
        }

        /// <summary>
        /// 增加空白行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnAddBlankLine_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能开发中，敬请期待！");
        }

        /// <summary>
        /// 临时医嘱分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitShortMenuItem_Click(object sender, EventArgs e)
        {
            ShiftPage(EnumOrderType.Short);
            this.SetFPStyle(EnumOrderType.Short);
        }

        /// <summary>
        /// 长期医嘱分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitMenuItem_Click(object sender, EventArgs e)
        {
            ShiftPage(EnumOrderType.Long);
            this.SetFPStyle(EnumOrderType.Long);
        }

        /// <summary>
        /// 右键打印单条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printMenuItem_Click(object sender, EventArgs e)
        {
            this.PrintSingleItem(false);
        }


        /// <summary>
        /// 只补打该条长期医嘱停止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDateItem_Click(object sender, EventArgs e)
        {
            this.PrintSingleItem(true);
        }
        /// <summary>
        /// 续打该条医嘱以后的医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printAfterItem_Click(object sender, EventArgs e)
        {
            int rowIndex = GetFpOrder(OrderType).ActiveSheet.ActiveRowIndex;

            #region 判断之前的是否已经打印完毕

            if (rowIndex != 0)
            {
                for (int row = rowIndex - 1; row > 0; row--)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ordTemp = GetFpOrder(OrderType).ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (ordTemp != null
                        && ordTemp.GetFlag == "0")
                    {
                        MessageBox.Show("前面还有未打印的医嘱，不能续打！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            //判断前一页是否打印完毕
            if (GetFpOrder(OrderType).ActiveSheetIndex > 0)
            {
                FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).Sheets[GetFpOrder(OrderType).ActiveSheetIndex - 1];
                for (int row = sheet.RowCount; row > 0; row--)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ordTemp = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (ordTemp != null
                        && ordTemp.GetFlag == "0")
                    {
                        MessageBox.Show("前面还有未打印的医嘱，不能续打！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            #endregion

            //这里应该有问题：如果选择的不是最后的未打印医嘱，也是能打印的



            FS.HISFC.Models.Order.Inpatient.Order order = this.GetFpOrder(OrderType).ActiveSheet.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;
            if (order == null)
            {
                return;
            }
            if (order.GetFlag != "0")
            {
                MessageBox.Show("项目[" + order.Item.Name + "]已经打印,无法续打！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //打印当前页
            string pageNo = (FS.FrameWork.Function.NConvert.ToInt32(GetFpOrder(OrderType).ActiveSheet.Tag) + 1).ToString();
            MessageBox.Show("请注意放入第" + pageNo + "页医嘱单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (PrintPage(false, rowIndex, -1, false) == -1)
            {
                return;
            }


            //打印后面几页
            int sheetIndex = GetFpOrder(OrderType).ActiveSheetIndex;
            int sheetCount = GetFpOrder(OrderType).Sheets.Count;
            for (int index = sheetIndex + 1; index < sheetCount; index++)
            {
                this.GetFpOrder(OrderType).ActiveSheetIndex = index;

                pageNo = (FS.FrameWork.Function.NConvert.ToInt32(GetFpOrder(OrderType).ActiveSheet.Tag) + 1).ToString();
                MessageBox.Show("请注意放入第" + pageNo + "页医嘱单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (PrintPage(false, -1, -1, false) == -1)
                {
                    return;
                }
            }
        }
        #endregion

        #endregion

        #region IPrintOrder 成员

        void FS.HISFC.BizProcess.Interface.IPrintOrder.Print()
        {
            this.ShowDialog();
        }

        public void SetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                return;
            }

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }
            this.ucQueryInpatientNo1.Text = "";


            this.ucShortOrderBillHeader.SetPatientInfo(patientInfo);
            this.ucLongOrderBillHeader.SetPatientInfo(patientInfo);
            this.ucOperateBillHeader.SetPatientInfo(patientInfo);

            this.myPatientInfo = patientInfo;

            if (this.myPatientInfo != null && !string.IsNullOrEmpty(myPatientInfo.ID))
            {
                this.SetTreeView();
            }
        }

        public void ShowPrintSet()
        {
            return;
        }

        #endregion

        private void frmOrderPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Clear();
        }
    }

    /// <summary>
    /// 打印方式
    /// </summary>
    public enum EnumPrintType
    {
        /// <summary>
        /// 白纸续打
        /// </summary>
        [FS.FrameWork.Public.Description("白纸续打")]
        WhitePaperContinue = 0,

        /// <summary>
        /// 印刷续打
        /// </summary>
        [FS.FrameWork.Public.Description("印刷续打")]
        DrawPaperContinue = 1,

        /// <summary>
        /// 出院打印
        /// </summary>
        [FS.FrameWork.Public.Description("出院打印")]
        PrintWhenPatientOut = 2
    }

    /// <summary>
    /// 医嘱类型枚举
    /// </summary>
    public enum EnumOrderType
    {
        /// <summary>
        /// 长嘱
        /// </summary>
        Long,

        /// <summary>
        /// 临嘱
        /// </summary>
        Short,


        /// <summary>
        /// 手术
        /// </summary>
        Operate,

        /// <summary>
        /// 全部医嘱
        /// </summary>
        All
    }


    /// <summary>
    /// 医嘱比较（根据方号）
    /// 排序优先级：1、页码；2、行号；3、方号；排序号
    /// </summary>
    public class OrderComparer : System.Collections.IComparer
    {
        #region IComparer 成员

        /// <summary>
        /// 比较方号
        /// 排序优先级：
        /// 1、页码；2、行号；3、方号；排序号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.Inpatient.Order order1 = x as FS.HISFC.Models.Order.Inpatient.Order;
                FS.HISFC.Models.Order.Inpatient.Order order2 = y as FS.HISFC.Models.Order.Inpatient.Order;
                int page1 = order1.PageNo == -1 ? 9999 : order1.PageNo;
                int page2 = order2.PageNo == -1 ? 9999 : order2.PageNo;

                if (page1 > page2)
                {
                    return 1;
                }
                else if (page1 == page2)
                {
                    if (order1.RowNo > order2.RowNo)
                    {
                        return 1;
                    }
                    else if (order1.RowNo == order2.RowNo)
                    {
                        if (order1.SubCombNO > order2.SubCombNO)
                        {
                            return 1;
                        }
                        else if (order1.SubCombNO == order2.SubCombNO)
                        {
                            if (order1.SortID > order2.SortID)
                            {
                                return 1;
                            }
                            else if (order1.SortID == order2.SortID)
                            {
                                return 0;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }

    /// <summary>
    /// 医嘱单各列定义
    /// </summary>
    public enum EnumCol
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        [FS.FrameWork.Public.Description("日期")]
        BeginDate = 0,

        /// <summary>
        /// 开始时间
        /// </summary>
        [FS.FrameWork.Public.Description("时间")]
        BeginTime,

        /// <summary>
        /// 开立医生
        /// </summary>
        [FS.FrameWork.Public.Description("医师签名")]
        RecipeDoct,

        /// <summary>
        /// 审核护士
        /// </summary>
        [FS.FrameWork.Public.Description("护士签名")]
        ConfirmNurse,

        /// <summary>
        /// 组标记
        /// </summary>
        [FS.FrameWork.Public.Description("组")]
        CombFlag,

        /// <summary>
        /// 项目名称
        /// </summary>
        [FS.FrameWork.Public.Description("医嘱内容")]
        ItemName,

        /// <summary>
        /// 停止标记
        /// </summary>
        [FS.FrameWork.Public.Description("停止标记")]
        DCFlag,

        /// <summary>
        /// 每次量
        /// </summary>
        [FS.FrameWork.Public.Description("每次量")]
        OnceDose,

        /// <summary>
        /// 频次
        /// </summary>
        [FS.FrameWork.Public.Description("频次")]
        Freq,

        /// <summary>
        /// 用法
        /// </summary>
        [FS.FrameWork.Public.Description("用法")]
        Usage,

        /// <summary>
        /// 备注
        /// </summary>
        [FS.FrameWork.Public.Description("备注")]
        Memo,

        /// <summary>
        /// 审核日期
        /// </summary>
        [FS.FrameWork.Public.Description("审核日期")]
        ConfirmDate,

        /// <summary>
        /// 审核时间
        /// </summary>
        [FS.FrameWork.Public.Description("审核时间")]
        ConfirmTime,

        /// <summary>
        /// 停止日期
        /// </summary>
        [FS.FrameWork.Public.Description("日期")]
        DCDate,

        /// <summary>
        /// 停止时间
        /// </summary>
        [FS.FrameWork.Public.Description("时间")]
        DCTime,

        /// <summary>
        /// 停止医生
        /// </summary>
        [FS.FrameWork.Public.Description("医师签名")]
        DCDoct,

        /// <summary>
        /// 停止审核护士
        /// </summary>
        [FS.FrameWork.Public.Description("护士签名")]
        DCConfirmNurse,

        /// <summary>
        /// 执行护士
        /// </summary>
        [FS.FrameWork.Public.Description("执行护士")]
        ExecNurse,

        /// <summary>
        /// 总量
        /// </summary>
        [FS.FrameWork.Public.Description("总量")]
        Qty,

        /// <summary>
        /// 金额
        /// </summary>
        [FS.FrameWork.Public.Description("金额")]
        Cost,

        /// <summary>
        /// 组号
        /// </summary>
        [FS.FrameWork.Public.Description("组号")]
        CombNO,

        /// <summary>
        /// 列数量
        /// </summary>
        [FS.FrameWork.Public.Description("列数")]
        Max
    }
}