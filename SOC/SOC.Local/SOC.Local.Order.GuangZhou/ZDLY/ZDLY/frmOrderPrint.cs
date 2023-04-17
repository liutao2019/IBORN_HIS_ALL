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

namespace Neusoft.SOC.Local.Order.GuangZhou.OrderPrint.ZDLY
{
    /// <summary>
    /// 医嘱单打印（续打）
    /// </summary>
    public partial class frmOrderPrint : Neusoft.FrameWork.WinForms.Forms.BaseStatusBar, Neusoft.HISFC.BizProcess.Interface.IPrintOrder
    {
        public frmOrderPrint()
        {
            InitializeComponent();
        }

        #region 变量

        Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        //Neusoft.SOC.HISFC.Fee.BizLogic.Undrug undrugManager = new Neusoft.SOC.HISFC.Fee.BizLogic.Undrug();
        Neusoft.HISFC.BizLogic.RADT.InPatient inPatientMgr = new Neusoft.HISFC.BizLogic.RADT.InPatient();

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
        /// 临嘱列表
        /// </summary>
        ArrayList alShort = new ArrayList();
        
        /// <summary>
        /// 重整医嘱列表
        /// </summary>
        ArrayList alRecord = new ArrayList();

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo;

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
        /// 长期医嘱单XML配置
        /// </summary>
        string LongOrderSetXML = Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + @".\LongOrderPrintSetting.xml";

        /// <summary>
        /// 临时医嘱单XML配置
        /// </summary>
        string shortOrderSetXML = Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + @".\ShortOrderPrintSetting.xml";

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
        /// 行数
        /// </summary>
        private int rowNum = 27;

        /// <summary>
        /// 打印方式
        /// </summary>
        private EnumPrintType OrderPrintType = EnumPrintType.PrintWhenPatientOut;

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
            this.tbExit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbRePrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C重打);
            this.tbQuery.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询);

            this.tbReset.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q取消);
            this.tbReset.Visible = true;
            this.tbSetting.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S设置);
            this.tbSetting.Visible = false;

            this.ucShortOrderBillHeader.Header = "临  时  医  嘱  单";
            this.ucLongOrderBillHeader.Header = "长  期  医  嘱  单";

            InitOrderPrint();

            this.fpLongOrder.Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheetLong = new FarPoint.Win.Spread.SheetView();
            this.InitLongSheet(sheetLong);
            this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheetLong);

            this.fpShortOrder.Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheetShort = new FarPoint.Win.Spread.SheetView();
            this.InitShortSheet(sheetShort);
            this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheetShort);

            this.fpLongOrder.ActiveSheetChanged += new EventHandler(fpLongOrder_ActiveSheetChanged);
            this.fpShortOrder.ActiveSheetChanged += new EventHandler(fpShortOrder_ActiveSheetChanged);
            this.fpLongOrder.MouseUp += new MouseEventHandler(fpLongOrder_MouseUp);
            fpShortOrder.MouseUp += new MouseEventHandler(fpShortOrder_MouseUp);

            this.tbReset.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

            this.fpLongOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpLongOrder_ColumnWidthChanged);
            this.fpLongOrder.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(fpLongOrder_ColumnDragMoveCompleted);
            this.fpShortOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpShortOrder_ColumnWidthChanged);
            this.fpShortOrder.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(fpShortOrder_ColumnDragMoveCompleted);

            this.nuRowNum.ValueChanged += new EventHandler(nuRowNum_ValueChanged);

            QueryPatientOrder();
        }

        void fpShortOrder_ColumnDragMoveCompleted(object sender, FarPoint.Win.Spread.DragMoveCompletedEventArgs e)
        {
            this.fpShortOrder.SaveSchema(this.fpShortOrder.ActiveSheet, this.shortOrderSetXML);
        }

        void fpLongOrder_ColumnDragMoveCompleted(object sender, FarPoint.Win.Spread.DragMoveCompletedEventArgs e)
        {
            this.fpLongOrder.SaveSchema(this.fpLongOrder.ActiveSheet, this.LongOrderSetXML);
        }

        void fpShortOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpShortOrder.SaveSchema(this.fpShortOrder.ActiveSheet, this.shortOrderSetXML);
            fpShortOrder.ActiveSheet.PrintInfo.ZoomFactor = fpShortOrder.ActiveSheet.ZoomFactor;
        }

        void fpLongOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpLongOrder.SaveSchema(this.fpLongOrder.ActiveSheet, this.LongOrderSetXML);
            fpLongOrder.ActiveSheet.PrintInfo.ZoomFactor = fpLongOrder.ActiveSheet.ZoomFactor;
        }

        /// <summary>
        /// 初始化基础参数
        /// </summary>
        private void InitOrderPrint()
        {
            #region 控制参数

            //华南医疗参数：打印时是否显示通用名，否则显示药品名
            try
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

                isShiftDeptNextPag = controlIntegrate.GetControlParam<bool>("HNZY32", true, false);

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
            this.cmbPrintType.AddItems(Neusoft.FrameWork.Public.EnumHelper.Current.EnumArrayList<EnumPrintType>());
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

                for (int i = 0; i < this.tbPrinter.Items.Count; i++)
                {
                    if (this.tbPrinter.Items[i].ToString().Contains(this.printerName))
                    {
                        this.tbPrinter.SelectedIndex = i;
                        break;
                    }
                }

                node = file.SelectSingleNode("ORDERPRINT/左边距");
                if (node != null)
                {
                    this.leftValue = Neusoft.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuLeft.Value = this.leftValue;

                node = file.SelectSingleNode("ORDERPRINT/上边距");
                if (node != null)
                {
                    this.topValue = Neusoft.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuTop.Value = this.topValue;

                node = file.SelectSingleNode("ORDERPRINT/行数");
                if (node != null)
                {
                    this.rowNum = Neusoft.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuRowNum.Value = this.rowNum;

                node = file.SelectSingleNode("ORDERPRINT/预览打印");
                if (node != null)
                {
                    cbxPreview.Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                }

                node = file.SelectSingleNode("ORDERPRINT/打印方式");
                string printType = string.Empty;
                if (node != null)
                {
                    printType = node.InnerText;
                }
                this.cmbPrintType.Text = printType;

                if (printType == Neusoft.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.WhitePaperContinue))
                {
                    this.OrderPrintType = EnumPrintType.WhitePaperContinue;
                }
                else if (printType == Neusoft.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.DrawPaperContinue))
                {
                    this.OrderPrintType = EnumPrintType.DrawPaperContinue;
                }
                else
                {
                    this.OrderPrintType = EnumPrintType.PrintWhenPatientOut;
                }
            }

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);
            this.cmbPrintType.SelectedIndexChanged += new EventHandler(cmbPrintType_SelectedIndexChanged);

            cbxPreview.CheckedChanged += new EventHandler(cbxPreview_CheckedChanged);

            #endregion

            #region 界面设置



            #endregion

            if (((Neusoft.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
            {
                this.cmbPrintType.Enabled = true;
                this.fpLongOrder.AllowColumnMove = true;
                fpShortOrder.AllowColumnMove = true;
                this.fpLongOrder.AllowUserZoom = true;
                this.fpShortOrder.AllowUserZoom = true;

                this.nuLeft.Enabled = true;
                this.nuTop.Enabled = true;
                this.nuRowNum.Enabled = true;
            }
            else
            {
                this.cmbPrintType.Enabled = false;
                this.fpLongOrder.AllowColumnMove = false;
                fpShortOrder.AllowColumnMove = false;
                this.fpLongOrder.AllowUserZoom = false;
                this.fpShortOrder.AllowUserZoom = false;

                this.nuLeft.Enabled = false;
                this.nuTop.Enabled = false;
                this.nuRowNum.Enabled = false;
            }
        }

        void cbxPreview_CheckedChanged(object sender, EventArgs e)
        {
            this.SetPrintValue("预览打印", "1");
        }

        void cmbPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPrintType.Text))
            {
                this.SetPrintValue("打印方式", this.cmbPrintType.Text);
            }

            if (cmbPrintType.Text == Neusoft.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.WhitePaperContinue))
            {
                this.OrderPrintType = EnumPrintType.WhitePaperContinue;
            }
            else if(cmbPrintType.Text == Neusoft.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.DrawPaperContinue))
            {
                this.OrderPrintType = EnumPrintType.DrawPaperContinue;
            }
            else
            {
                this.OrderPrintType = EnumPrintType.PrintWhenPatientOut;
            }
        }

        /// <summary>
        /// 初始化长期医嘱Sheet
        /// </summary>
        /// <param name="sheet"></param>
        private void InitLongSheet(FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.Reset();
            sheet.ColumnCount = (Int32)LongOrderColunms.Max;
            sheet.ColumnHeader.RowCount = 2;
            sheet.RowCount = 0;
            sheet.RowCount = this.rowNum;
            sheet.RowHeader.ColumnCount = 0;
            sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            textCellType1.WordWrap = true;

            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, false, true, true);

            for (int i = 0; i < this.rowNum; i++)
            {
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells.Get(i, j).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
            }

            #region 标题

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.BeginDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.BeginDate).Text = " 起   始 ";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.BeginDate).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.BeginTime).Text = "时间";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.RecipeDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.RecipeDoct).Text = "医师签名";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ConfirmNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ConfirmNurse).Text = "护士签名";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.CombFlag).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.CombFlag).Text = "组";
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ItemName).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ItemName).Text = " 长期医嘱 ";


            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.OnceDose).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.OnceDose).Text = "每次量";


            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Memo).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Memo).Text = "备注";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Usage).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Usage).Text = "用法";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Freq).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Freq).Text = "频次";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDate).Text = "停   止";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCDate).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCTime).Text = "时间";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDoct).Text = "医师签名";
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCConfirmNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCConfirmNurse).Text = "护士签名";


            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ExecNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ExecNurse).Text = "执行";


            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;
            sheet.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            #endregion

            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).CellType = textCellType1;

            sheet.Columns.Get((Int32)LongOrderColunms.BeginDate).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.BeginTime).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.CombFlag).Width = 17F;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).Width = 250F;

            sheet.Columns.Get((Int32)LongOrderColunms.OnceDose).Width = 25F;
            sheet.Columns.Get((Int32)LongOrderColunms.Usage).Width = 25F;
            sheet.Columns.Get((Int32)LongOrderColunms.Freq).Width = 25F;
            sheet.Columns.Get((Int32)LongOrderColunms.Memo).Width = 25F;

            sheet.Columns.Get((Int32)LongOrderColunms.RecipeDoct).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.ConfirmNurse).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDate).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCTime).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDoct).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCConfirmNurse).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.ExecNurse).Width = 55F;

            sheet.Columns.Get((Int32)LongOrderColunms.CombNO).Visible = false;

            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            #region 计算每行的高度

            //医嘱单的高度
            //int fpLongHeight = 910;
            int fpLongHeight = 890;

            float fpLongHeaderHeight = 0;

            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(0).Height;
            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(1).Height;

            int rowHeight = Neusoft.FrameWork.Function.NConvert.ToInt32((fpLongHeight - fpLongHeaderHeight) / this.rowNum);

            for (int i = 0; i < this.rowNum; i++)
            {
                sheet.Rows.Get(i).BackColor = System.Drawing.Color.White;
                sheet.Rows.Get(i).Height = rowHeight;
                sheet.Rows.Get(i).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Rows.Get(i).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            #endregion

            sheet.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            sheet.SheetCornerStyle.Parent = "HeaderDefault";
        }

        /// <summary>
        /// 初始化临时医嘱Sheet
        /// </summary>
        /// <param name="sheet"></param>
        private void InitShortSheet(FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.Reset();
            sheet.ColumnCount = (Int32)ShortOrderColunms.Max;
            sheet.ColumnHeader.RowCount = 2;
            sheet.RowCount = 0;
            sheet.RowCount = this.rowNum;
            sheet.RowHeader.ColumnCount = 0;
            sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            textCellType2.WordWrap = true;

            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, false, true, true);

            for (int i = 0; i < this.rowNum; i++)
            {
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells.Get(i, j).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
            }

            #region 标题

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.BeginDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.BeginDate).Text = " 开  立 ";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.BeginDate).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.BeginTime).Text = "时间";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.RecipeDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.RecipeDoct).Text = "医师签名";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.CombFlag).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.CombFlag).Text = "组";
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ItemName).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ItemName).Text = "临时医嘱";




            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.OnceDose).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.OnceDose).Text = "每次量";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Usage).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Usage).Text = "用法";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Freq).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Freq).Text = "频次";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Qty).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Qty).Text = "总量";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Memo).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Memo).Text = "备注";


            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmNurse).Text = "护 士 签 名";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmDate).Text = "执行时间";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.ConfirmDate).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.ConfirmTime).Text = "时间";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCFlage).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCFlage).Text = "取消";
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCDoct).Text = "医生";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ExecOper).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ExecOper).Text = "执行";

            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;

            #endregion

            sheet.Columns.Get((Int32)ShortOrderColunms.BeginTime).Label = "时间";

            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).CellType = textCellType2;
            sheet.Columns.Get((Int32)ShortOrderColunms.CombNO).Visible = false;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCDoct).Visible = false;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCFlage).Visible = false;

            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            sheet.Columns.Get((Int32)ShortOrderColunms.BeginDate).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.BeginTime).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.CombFlag).Width = 17F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).Width = 260F;

            sheet.Columns.Get((Int32)ShortOrderColunms.OnceDose).Width = 25F;
            sheet.Columns.Get((Int32)ShortOrderColunms.Usage).Width = 25F;
            sheet.Columns.Get((Int32)ShortOrderColunms.Freq).Width = 25F;
            sheet.Columns.Get((Int32)ShortOrderColunms.Qty).Width = 25F;
            sheet.Columns.Get((Int32)ShortOrderColunms.Memo).Width = 25F;

            sheet.Columns.Get((Int32)ShortOrderColunms.RecipeDoct).Width = 55F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmNurse).Width = 55F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmDate).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmTime).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCFlage).Width = 40F;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCDoct).Width = 55F;

            #region 计算每行的高度

            int fpLongHeight = 890;

            float fpLongHeaderHeight = 0;

            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(0).Height;
            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(1).Height;

            int rowHeight = Neusoft.FrameWork.Function.NConvert.ToInt32((fpLongHeight - fpLongHeaderHeight) / this.rowNum);

            for (int i = 0; i < this.rowNum; i++)
            {
                sheet.Rows.Get(i).BackColor = System.Drawing.Color.White;
                sheet.Rows.Get(i).Height = rowHeight;
                sheet.Rows.Get(i).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Rows.Get(i).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            #endregion

            sheet.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            sheet.SheetCornerStyle.Parent = "HeaderDefault";

            //不是管理员则隐藏重打按钮
            if (!((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.tbReset.Visible = false;
            }
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

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询显示医嘱信息,请稍候......");
            Application.DoEvents();

            //try
            //{
                ArrayList alAll = new ArrayList();

                alAll = this.orderManager.QueryPrnOrder(this.myPatientInfo.ID);

                alLong.Clear();
                alShort.Clear();

                Neusoft.HISFC.Models.Order.Inpatient.Order order = null;
                foreach (object obj in alAll)
                {
                    order = obj as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    order.Nurse.Name = Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order.Nurse.ID);
                    order.Doctor.Name = order.ReciptDoctor.Name;
                    if (order.DCNurse.ID != null && order.DCNurse.ID != "")
                    {
                        order.DCNurse.Name = Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order.DCNurse.ID);
                    }

                    if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        order.Item.Name += orderManager.TransHypotest(order.HypoTest);
                    }

                    if (order.OrderType.Type == Neusoft.HISFC.Models.Order.EnumType.LONG)
                    {
                        //长期医嘱
                        alLong.Add(obj);
                    }
                    else
                    {
                        //临时医嘱
                        alShort.Add(obj);
                    }
                }

                Neusoft.FrameWork.Management.ExtendParam myInpatient = new Neusoft.FrameWork.Management.ExtendParam();

                this.alRecord = myInpatient.GetComExtInfoListByPatient(Neusoft.HISFC.Models.Base.EnumExtendClass.PATIENT, myPatientInfo.ID);

                this.Clear();

                //this.AddObjectToFpLong(alLong);
                this.AddOrderToFP(true, alLong);
                this.AddOrderToFP(false,alShort);

                this.ucShortOrderBillHeader.SetChangedInfo(this.GetFirstOrder(false));
                this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder(true));


                foreach (FarPoint.Win.Spread.SheetView sv in fpLongOrder.Sheets)
                {
                    if (System.IO.File.Exists(LongOrderSetXML))
                    {
                        fpLongOrder.ReadSchema(sv, LongOrderSetXML, false);

                        sv.PrintInfo.ZoomFactor = sv.ZoomFactor;
                    }
                    if (!((Neusoft.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
                    {
                        for (int i = 0; i < sv.Columns.Count; i++)
                        {
                            sv.Columns[i].Resizable = false;
                        }
                    }
                }
                foreach (FarPoint.Win.Spread.SheetView sv in this.fpShortOrder.Sheets)
                {
                    if (System.IO.File.Exists(shortOrderSetXML))
                    {
                        fpShortOrder.ReadSchema(sv, shortOrderSetXML, false);

                        sv.PrintInfo.ZoomFactor = sv.ZoomFactor;
                    }

                    if (!((Neusoft.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
                    {
                        for (int i = 0; i < sv.Columns.Count; i++)
                        {
                            sv.Columns[i].Resizable = false;
                        }
                    }
                }


                //SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);

                //SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
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

        #region 清空

        /// <summary>
        /// 清空所有医嘱单内容
        /// </summary>
        private void Clear()
        {
            #region 长嘱

            #region 保留一个Sheet
            this.fpLongOrder.Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
            this.InitLongSheet(sheet);
            this.fpLongOrder.Sheets.Add(sheet);
            this.lblPageLong.Text = "第1页";

            #endregion

            #endregion

            #region 临嘱

            #region 保留一个Sheet
            this.fpShortOrder.Sheets.Clear();
            //if (this.fpShortOrder.Sheets.Count > 1)
            //{
            //    for (int j = this.fpShortOrder.Sheets.Count - 1; j >= 0; j--)
            //    {
            //        this.fpShortOrder.Sheets.RemoveAt(j);
            //    }
            //}

            //this.fpShortOrder.Sheets[0].Columns.Count = (Int32)ShortOrderColunms.Max;
            //this.fpShortOrder.Sheets[0].RowCount = 0;
            //this.fpShortOrder.Sheets[0].RowCount = rowNum;

            //this.InitLongSheet(fpShortOrder.Sheets[0]);

            FarPoint.Win.Spread.SheetView sheet1 = new FarPoint.Win.Spread.SheetView();
            this.InitShortSheet(sheet1);
            this.fpShortOrder.Sheets.Add(sheet1);

            this.lblPageShort.Text = "第1页";
            #endregion

            #endregion
        }

        #endregion

        /// <summary>
        /// 获取打印的时间：开始时间或开立时间
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private DateTime GetPrintDate(Neusoft.HISFC.Models.Order.Order order)
        {
            if (true)
            {
                return order.BeginTime;
            }
            else
            {
                return order.MOTime;
            }
        }

        /// <summary>
        /// 加入到医嘱单显示
        /// </summary>
        /// <param name="isLong">是否长嘱</param>
        /// <param name="alOrder"></param>
        private void AddOrderToFP(bool isLong, ArrayList alOrder)
        {
            if (alOrder.Count <= 0)
            {
                return;
            }

            //未打印列表
            ArrayList alPageNull = new ArrayList();
            //已打印列表
            ArrayList alPageNo = new ArrayList();

            //按照页码、行号、组号、排序号 排序
            alOrder.Sort(new OrderComparer());

            int MaxPageNo = -1;
            int MaxRowNo = 0;

            //用于处理转科换页
            string deptCode = "";

            //用来处理重整医嘱
            int moState = -1;

            foreach (Neusoft.HISFC.Models.Order.Inpatient.Order inOrder in alOrder)
            {
                if (inOrder.RowNo >= this.rowNum)
                {
                    MessageBox.Show(inOrder.OrderType.Name + "【" + inOrder.Item.Name + "】的实际打印行号为" + (inOrder.RowNo + 1).ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                }

                if (string.IsNullOrEmpty(deptCode))
                {
                    deptCode = inOrder.ReciptDept.ID;
                }

                #region 长嘱

                if (isLong)
                {
                    //已打印的
                    if (inOrder.GetFlag != "0")
                    {
                        //新页
                        if (inOrder.PageNo > MaxPageNo)
                        {
                            MaxPageNo = inOrder.PageNo;
                            MaxRowNo = 0;
                        }

                        if (MaxPageNo >= this.fpLongOrder.Sheets.Count)
                        {
                            FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                            this.InitLongSheet(sheet);
                            this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);
                        }

                        this.AddToRow(isLong, true, this.fpLongOrder.Sheets[MaxPageNo], MaxRowNo, inOrder);

                        if (inOrder.RowNo >= MaxRowNo)
                        {
                            MaxRowNo = inOrder.RowNo + 1;
                        }
                    }
                    //未打印
                    else
                    {
                        if ((MaxRowNo % this.rowNum == 0) ||
                            //转科也要自动换页
                            (this.isShiftDeptNextPag &&
                            !string.IsNullOrEmpty(deptCode) && deptCode.Trim() != inOrder.ReciptDept.ID))
                        {
                            MaxPageNo += 1;
                            MaxRowNo = 0;

                            if (MaxPageNo >= this.fpLongOrder.Sheets.Count)
                            {
                                FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                                this.InitLongSheet(sheet);
                                this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);
                            }
                        }
                        else if (moState == 4 && inOrder.Status != 4)
                        {
                            if (MessageBox.Show("存在重整医嘱，是否自动换页？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                MaxPageNo += 1;
                                MaxRowNo = 0;

                                if (MaxPageNo >= this.fpLongOrder.Sheets.Count)
                                {
                                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                                    this.InitLongSheet(sheet);
                                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);
                                }
                            }
                        }

                        this.AddToRow(isLong, false, this.fpLongOrder.Sheets[MaxPageNo], MaxRowNo, inOrder);
                        MaxRowNo += 1;
                    }

                    if (this.fpLongOrder.Sheets[MaxPageNo].Tag == null)
                    {
                        this.fpLongOrder.Sheets[MaxPageNo].Tag = MaxPageNo;
                        this.fpLongOrder.Sheets[MaxPageNo].SheetName = "第" + (MaxPageNo + 1).ToString() + "页";
                    }

                    //始终存上一条医嘱的状态
                    moState = inOrder.Status;
                }
                #endregion

                #region 临嘱
                else
                {
                    //已打印的
                    if (inOrder.GetFlag != "0")
                    {
                        //新页
                        if (inOrder.PageNo > MaxPageNo)
                        {
                            MaxPageNo = inOrder.PageNo;
                            MaxRowNo = 0;
                        }

                        if (MaxPageNo >= this.fpShortOrder.Sheets.Count)
                        {
                            FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                            this.InitShortSheet(sheet);
                            this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);
                        }

                        this.AddToRow(isLong, true, this.fpShortOrder.Sheets[MaxPageNo], MaxRowNo, inOrder);

                        if (inOrder.RowNo >= MaxRowNo)
                        {
                            MaxRowNo = inOrder.RowNo + 1;
                        }
                    }
                    //未打印
                    else
                    {
                        if ((MaxRowNo % this.rowNum == 0) ||
                            //转科也要自动换页
                             (this.isShiftDeptNextPag &&
                             !string.IsNullOrEmpty(deptCode) && deptCode.Trim() != inOrder.ReciptDept.ID))
                        {
                            MaxPageNo += 1;
                            MaxRowNo = 0;

                            if (MaxPageNo >= this.fpShortOrder.Sheets.Count)
                            {
                                FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                                this.InitShortSheet(sheet);
                                this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);
                            }
                        }
                        this.AddToRow(isLong, false, this.fpShortOrder.Sheets[MaxPageNo], MaxRowNo, inOrder);
                        MaxRowNo += 1;
                    }

                    if (this.fpShortOrder.Sheets[MaxPageNo].Tag == null)
                    {
                        this.fpShortOrder.Sheets[MaxPageNo].Tag = MaxPageNo;
                        this.fpShortOrder.Sheets[MaxPageNo].SheetName = "第" + (MaxPageNo + 1).ToString() + "页";
                    }
                }
                #endregion

                if (deptCode != inOrder.ReciptDept.ID)
                {
                    deptCode = inOrder.ReciptDept.ID;
                }
            }

            for (int i = 0; i < this.fpLongOrder.Sheets.Count; i++)
            {
                Classes.Function.DrawComboLeft(this.fpLongOrder.Sheets[i], (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
            }

            for (int i = 0; i < this.fpShortOrder.Sheets.Count; i++)
            {
                Classes.Function.DrawComboLeft(this.fpShortOrder.Sheets[i], (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
            }
        }

        /// <summary>
        /// 每行添加
        /// </summary>
        /// <param name="isLong"></param>
        /// <param name="isPint"></param>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        /// <param name="inOrder"></param>
        private void AddToRow(bool isLong, bool isPint, FarPoint.Win.Spread.SheetView sheet, int row,Neusoft.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            #region 长嘱

            if (isLong)
            {
                if (isPint)
                {
                    sheet.Rows[row].BackColor = Color.MistyRose;
                }
                else
                {
                    sheet.Rows[row].BackColor = Color.White;
                }


                sheet.SetValue(row, (Int32)LongOrderColunms.BeginDate, GetPrintDate(inOrder).Month.ToString() + "-" + GetPrintDate(inOrder).Day.ToString());
                sheet.SetValue(row, (Int32)LongOrderColunms.BeginTime, GetPrintDate(inOrder).ToShortTimeString());

                sheet.SetValue(row, (Int32)LongOrderColunms.ItemName, GetOrderItem(inOrder, isLong));

                sheet.SetValue(row, (Int32)LongOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce) + inOrder.DoseUnit);
                sheet.SetValue(row, (Int32)LongOrderColunms.Usage, inOrder.Usage.Name);
                sheet.SetValue(row, (Int32)LongOrderColunms.Freq, inOrder.Frequency.ID);
                sheet.SetValue(row, (Int32)LongOrderColunms.Memo, inOrder.Memo);


                sheet.SetValue(row, (Int32)LongOrderColunms.RecipeDoct, inOrder.Doctor.Name);
                sheet.SetValue(row, (Int32)LongOrderColunms.ConfirmNurse, inOrder.Nurse.Name);

                if (inOrder.DCOper.OperTime > this.reformDate)
                {
                    sheet.SetValue(row, (Int32)LongOrderColunms.DCDate, inOrder.DCOper.OperTime.Month.ToString() + "-" + inOrder.DCOper.OperTime.Day.ToString());
                    sheet.SetValue(row, (Int32)LongOrderColunms.DCTime, inOrder.DCOper.OperTime.ToShortTimeString());
                    sheet.SetValue(row, (Int32)LongOrderColunms.DCDoct, inOrder.DCOper.Name);
                    sheet.SetValue(row, (Int32)LongOrderColunms.DCConfirmNurse, inOrder.DCNurse.Name);
                }

                sheet.SetValue(row, (Int32)LongOrderColunms.CombNO, inOrder.Combo.ID);

                sheet.Rows[row].Tag = inOrder;
            }
            #endregion

            #region 临嘱
            else
            {
                if (isPint)
                {
                    sheet.Rows[row].BackColor = Color.MistyRose;
                }
                else
                {
                    sheet.Rows[row].BackColor = Color.White;
                }

                sheet.SetValue(row, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(inOrder).Month.ToString() + "-" + GetPrintDate(inOrder).Day.ToString());
                sheet.SetValue(row, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(inOrder).ToShortTimeString());

                sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, GetOrderItem(inOrder, isLong));

                sheet.SetValue(row, (Int32)ShortOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce) + inOrder.DoseUnit);
                sheet.SetValue(row, (Int32)ShortOrderColunms.Usage, inOrder.Usage.Name);
                sheet.SetValue(row, (Int32)ShortOrderColunms.Freq, inOrder.Frequency.ID);
                sheet.SetValue(row, (Int32)ShortOrderColunms.Qty, Neusoft.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Unit);
                sheet.SetValue(row, (Int32)ShortOrderColunms.Memo, inOrder.Memo);

                sheet.SetValue(row, (Int32)ShortOrderColunms.RecipeDoct, inOrder.Doctor.Name);
                sheet.SetValue(row, (Int32)ShortOrderColunms.ConfirmNurse, inOrder.Nurse.Name);

                if (inOrder.ConfirmTime != DateTime.MinValue)
                {
                    sheet.SetValue(row, (Int32)ShortOrderColunms.ConfirmDate, inOrder.ConfirmTime.Month.ToString() + "-" + inOrder.ConfirmTime.Day.ToString());
                    sheet.SetValue(row, (Int32)ShortOrderColunms.ConfirmTime, inOrder.ConfirmTime.ToShortTimeString());
                }
                sheet.SetValue(row, (Int32)ShortOrderColunms.CombNO, inOrder.Combo.ID);

                if (inOrder.Status == 3 || inOrder.Status == 4)
                {
                    sheet.SetValue(row, (Int32)ShortOrderColunms.DCFlage, "DC");
                    sheet.SetValue(row, (Int32)ShortOrderColunms.DCDoct, inOrder.DCOper.Name);
                }

                sheet.Rows[row].Tag = inOrder;
            }
            #endregion
        }

        /// <summary>
        /// 添加到长嘱Fp
        /// </summary>
        /// <param name="al"></param>
        [Obsolete("作废", true)]
        private void AddObjectToFpLong(ArrayList alLongOrder)
        {
            #region 为空返回
            if (alLongOrder.Count <= 0)
            {
                return;
            }
            #endregion

            #region 定义变量

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = -1;
            int MaxRowNo = -1;

            #endregion

            #region 按是否打印分组

            foreach (Neusoft.HISFC.Models.Order.Inpatient.Order temp in alLongOrder)
            {
                if (temp.PageNo == -1)
                {
                    alPageNull.Insert(alPageNull.Count, temp);
                }
                else
                {
                    if (!hsPageNo.ContainsKey(temp.PageNo))
                    {
                        alPageNo.Insert(alPageNo.Count, temp.PageNo);

                        hsPageNo.Add(temp.PageNo, new ArrayList());

                        (hsPageNo[temp.PageNo] as ArrayList).Insert((hsPageNo[temp.PageNo] as ArrayList).Count, temp);
                    }
                    else
                    {
                        (hsPageNo[temp.PageNo] as ArrayList).Insert((hsPageNo[temp.PageNo] as ArrayList).Count, temp);
                    }
                }
            }

            #endregion

            #region 将已打印的显示

            for (int i = 0; i < alPageNo.Count; i++)
            {
                int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(alPageNo[i].ToString());

                if (pageNo > MaxPageNo)
                {
                    MaxPageNo = pageNo;
                    MaxRowNo = -1;
                }

                ArrayList alTemp = hsPageNo[pageNo] as ArrayList;

                #region 已打印的第一页
                if (i == 0)
                {
                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "【" + order.Item.Name + "】的实际打印行号为" + (order.RowNo+1).ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        this.fpLongOrder.Sheets[0].Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginDate, GetPrintDate(order).Month.ToString() + "-" + GetPrintDate(order).Day.ToString());
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginTime, GetPrintDate(order).ToShortTimeString());
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, GetOrderItem(order, true));


                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(order.DoseOnce) + order.DoseUnit);
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Usage, order.Usage.Name);
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Freq, order.Frequency.ID);

                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Memo, order.Memo);


                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.RecipeDoct, order.Doctor.Name);
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.DCOper.OperTime > this.reformDate)
                        {
                            this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.DCDate, order.DCOper.OperTime.Month.ToString() + "-" + order.DCOper.OperTime.Day.ToString());
                            this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.DCTime, order.DCOper.OperTime.ToShortTimeString());
                            this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.DCDoct, order.DCOper.Name);
                            this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.DCConfirmNurse, order.DCNurse.Name);
                        }
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.CombNO, order.Combo.ID);

                        this.fpLongOrder.Sheets[0].Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Classes.Function.DrawComboLeft(this.fpLongOrder.Sheets[0], (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);

                    this.fpLongOrder.Sheets[0].Tag = pageNo;
                    this.fpLongOrder.Sheets[0].SheetName = "第" + (pageNo + 1).ToString() + "页";
                }
                #endregion

                #region 其他页
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                    this.InitLongSheet(sheet);
                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);

                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "【" + order.Item.Name + "】的实际打印行号为" + (order.RowNo+1).ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[order.RowNo].BackColor = Color.White;
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.BeginDate, GetPrintDate(order).Month.ToString() + "-" + GetPrintDate(order).Day.ToString());
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.BeginTime, GetPrintDate(order).ToShortTimeString());

                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, GetOrderItem(order, true));


                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(order.DoseOnce) + order.DoseUnit);
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.Usage, order.Usage.Name);
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.Freq, order.Frequency.ID);
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.Memo, order.Memo);

                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.RecipeDoct, order.Doctor.Name);
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.DCOper.OperTime > this.reformDate)
                        {
                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.DCDate, order.DCOper.OperTime.Month.ToString() + "-" + order.DCOper.OperTime.Day.ToString());
                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.DCTime, order.DCOper.OperTime.ToShortTimeString());
                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.DCDoct, order.DCOper.Name);
                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.DCConfirmNurse, order.DCNurse.Name);
                        }

                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.CombNO, order.Combo.ID);

                        sheet.Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Classes.Function.DrawComboLeft(sheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);

                    sheet.Tag = pageNo;
                    sheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }
                #endregion
            }
            #endregion

            #region 显示未打印医嘱

            bool fromOne = true;
            int iniIndex = -1;
            int endIndex = -1;

            if (MaxPageNo == -1)
            {
                MaxPageNo++;
                fromOne = false;
            }

            if (MaxRowNo == -1)
            {
                MaxRowNo++;
            }

            FarPoint.Win.Spread.SheetView activeSheet = this.fpLongOrder.Sheets[MaxPageNo];

            activeSheet.Tag = MaxPageNo++;
            activeSheet.SheetName = "第" + MaxPageNo.ToString() + "页";

            if (fromOne)
            {
                iniIndex = 1;
                endIndex = alPageNull.Count + 1;
            }
            else
            {
                iniIndex = 0;
                endIndex = alPageNull.Count;
            }

            //当前行
            int activeRow = 0;

            for (; iniIndex < endIndex; iniIndex++)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order oTemp;

                if (fromOne)
                {
                    oTemp = alPageNull[iniIndex - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    oTemp = alPageNull[iniIndex] as Neusoft.HISFC.Models.Order.Inpatient.Order;
                }

                activeRow = (iniIndex + MaxRowNo) % rowNum;
                if (activeRow == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(sheet);

                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginDate, this.GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, GetOrderItem(oTemp, true));
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Memo, oTemp.Memo);

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.DCOper.OperTime > this.reformDate)
                    {
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCDate, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCTime, oTemp.DCOper.OperTime.ToShortTimeString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCDoct, oTemp.DCOper.Name);
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCConfirmNurse, oTemp.DCNurse.Name);
                    }

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.CombNO, oTemp.Combo.ID);

                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, GetOrderItem(oTemp, true));


                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Memo, oTemp.Memo);

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.DCOper.OperTime > this.reformDate)
                    {
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCDate, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCTime, oTemp.DCOper.OperTime.ToShortTimeString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCDoct, oTemp.DCOper.Name);
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.DCConfirmNurse, oTemp.DCNurse.Name);
                    }
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.CombNO, oTemp.Combo.ID);

                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
            }

            #endregion

            #region 处理换科打印问题

            if (isShiftDeptNextPag)
            {
                //需要处理
                string deptCode = "";
                //处理重整一周后的自动分页
                for (int i = 0; i < this.fpLongOrder.Sheets.Count; i++)
                {
                    for (int j = 0; j < this.fpLongOrder.Sheets[i].Rows.Count; j++)
                    {
                        Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.Sheets[i].Rows[j].Tag as
                            Neusoft.HISFC.Models.Order.Inpatient.Order;

                        if (order == null)
                            continue;

                        if (order.PageNo >= 0)
                            continue;

                        if (string.IsNullOrEmpty(deptCode))
                        {
                            deptCode = order.ReciptDept.ID;
                        }
                        else if (deptCode.Trim() != order.ReciptDept.ID)
                        {
                            this.AddObjectToFpLongAfter(i, j);
                            deptCode = order.ReciptDept.ID;
                        }
                    }
                }
            }

            #endregion

            #region 重整医嘱分页


            //需要处理
            if (this.alRecord.Count >= 0)
            {
                //处理重整一周后的自动分页
                for (int i = 0; i < this.fpLongOrder.Sheets.Count; i++)
                {
                    for (int j = 0; j < this.fpLongOrder.Sheets[i].Rows.Count; j++)
                    {
                        Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.Sheets[i].Rows[j].Tag as
                            Neusoft.HISFC.Models.Order.Inpatient.Order;

                        if (order == null)
                            continue;

                        if (order.PageNo >= 0)
                            continue;

                        foreach (Neusoft.HISFC.Models.Base.ExtendInfo ext in alRecord)
                        {
                            if (GetPrintDate(order) >= Neusoft.FrameWork.Function.NConvert.ToDateTime(ext.PropertyCode))
                            {
                                if (MessageBox.Show("系统找到重整医嘱记录，时间:" + ext.PropertyCode + "，是否分页？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    this.AddObjectToFpLongAfter(i, j);
                                }

                                alRecord.Remove(ext);
                                break;
                            }
                        }
                    }
                }
            }

            #endregion

            DealLongOrderCrossPage();
        }

        /// <summary>
        /// 获取显示的医嘱名称
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isLong"></param>
        /// <returns></returns>
        private string GetOrderItem(Neusoft.HISFC.Models.Order.Inpatient.Order order, bool isLong)
        {
            string itemName = "";
            if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                //是否显示通用名，否则
                if (isDisplayRegularName)
                {
                    if (order.Item.ID == "999")
                    {
                        itemName = order.Item.Name;// +" " + order.Frequency.ID; //+ " " + order.Usage.Name + " " + order.Memo;
                    }
                    else
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;
                        itemName = phaItem.NameCollection.RegularName;// +" " + order.Frequency.ID;  //+ " " + order.Usage.Name + " " + order.Memo;
                    }
                }

                if(string.IsNullOrEmpty(itemName))
                {
                    itemName = order.Item.Name;
                }

                //if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                //{
                //    //itemName = order.Item.Name + " " + order.DoseOnceDisplay + order.DoseUnit;  // mb 这样写通用名不就取不到了吗
                //    itemName = itemName + " " + order.DoseOnceDisplay + order.DoseUnit;
                //}
                //else
                //{
                //    //itemName = order.Item.Name + " " + order.DoseOnce.ToString("F6").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
                //    itemName = itemName + " " + order.DoseOnce.ToString("F6").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
                //}
            }
            else
            {
                //itemName = order.Item.Name + " " + order.Qty.ToString("F6").TrimEnd('0').TrimEnd('.') + order.Unit;
                itemName = order.Item.Name;
            }

            string addStr = "";
            if (order.OrderType.ID == "CD")
            {
                addStr = "(出院带药)";
            }
            else if (order.OrderType.ID == "BL")
            {
                addStr = "(补录医嘱)";
            }

            string qty = "";
            if (!order.OrderType.IsDecompose && order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                qty = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + order.Unit;
            }

            //若是组套非药项目则显示“复合项”
            //Neusoft.SOC.HISFC.Fee.Models.Undrug oneUndrug = undrugManager.GetUndrug(order.Item.ID);
            Neusoft.HISFC.Models.Fee.Item.Undrug oneUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
            if ( oneUndrug != null && oneUndrug.UnitFlag.Equals("1"))
            {
                itemName += " (复合项)";
            }
            
            //护理、饮食不显示频次
            if (order.Item.Name.Contains("护理") || order.Item.Name.Contains("饮食"))
            {
                itemName += " " + addStr;
                //itemName += " " + order.Usage.Name + " " + qty + " " + addStr + "" + order.Memo;
            }
            else
            {
                //判断是否显示通用名时已经显示了频次，不需要再添加
                itemName += addStr;
                //itemName += " " + order.Usage.Name + " " + qty + " " + addStr + "" + order.Memo;
            }
            itemName += order.IsEmergency ? "[急]" : "";
            return itemName;
        }

        /// <summary>
        /// 转科分页后添加到Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        //[Obsolete("作废", true)]
        private void AddObjectToFpLongAfter(int sheet, int row)
        {
            DateTime now = this.orderManager.GetDateTimeFromSysDateTime().Date;//当前系统时间

            #region 定义变量

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            if (this.fpLongOrder.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("所选项目为空！");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.fpLongOrder.Sheets[sheet].Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

            MessageBox.Show("重整医嘱后分页，请注意！", "提示");

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "已经打印过，不能另起一页！");
                return;
            }

            #endregion

            #region 获取剩余数据

            for (int i = row; i < this.fpLongOrder.Sheets[sheet].Rows.Count; i++)
            {
                if (this.fpLongOrder.Sheets[sheet].Rows[i].Tag != null)
                {
                    alPageNull.Add(this.fpLongOrder.Sheets[sheet].Rows[i].Tag);
                }
            }

            for (int i = sheet + 1; i < this.fpLongOrder.Sheets.Count; i++)
            {
                for (int j = 0; j < this.fpLongOrder.Sheets[i].Rows.Count; j++)
                {
                    if (this.fpLongOrder.Sheets[i].Rows[j].Tag != null)
                    {
                        alPageNull.Add(this.fpLongOrder.Sheets[i].Rows[j].Tag);
                    }
                }
            }

            #endregion

            #region 清空数据

            for (int j = row; j < this.fpLongOrder.Sheets[sheet].Rows.Count; j++)
            {
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.BeginDate, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.BeginTime, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.CombFlag, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ItemName, "");

                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.OnceDose, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Usage, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Freq, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Memo, "");

                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.RecipeDoct, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ConfirmNurse, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, 8, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, 9, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, 10, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.CombNO, "");
                this.fpLongOrder.Sheets[sheet].Rows[j].Tag = null;
            }

            #endregion

            #region 保留一个Sheet

            if (this.fpLongOrder.Sheets.Count > 1)
            {
                for (int j = this.fpLongOrder.Sheets.Count - 1; j > sheet; j--)
                {
                    this.fpLongOrder.Sheets.RemoveAt(j);
                }
            }

            #endregion

            #region 显示未打印医嘱

            int iniIndex = -1;
            int endIndex = -1;

            if (MaxRowNo == -1)
            {
                MaxRowNo++;
            }

            FarPoint.Win.Spread.SheetView orgSheet = new FarPoint.Win.Spread.SheetView();

            this.InitLongSheet(orgSheet);

            this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.fpLongOrder.Sheets[MaxPageNo];

            activeSheet.Tag = MaxPageNo++;
            activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

            iniIndex = 0;
            endIndex = alPageNull.Count;

            for (; iniIndex < endIndex; iniIndex++)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order oTemp;

                oTemp = alPageNull[iniIndex] as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if ((iniIndex + MaxRowNo) % rowNum == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView newsheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(newsheet);

                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, GetOrderItem(oTemp, true));


                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Memo, oTemp.Memo);

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 8, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 9, oTemp.DCOper.OperTime.ToShortTimeString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 10, oTemp.DCOper.Name);
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 11, oTemp.DCNurse.Name);
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, GetOrderItem(oTemp, true));

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Memo, oTemp.Memo);


                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 8, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 9, oTemp.DCOper.OperTime.ToShortTimeString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 10, oTemp.DCOper.Name);
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 11, oTemp.DCNurse.Name);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
            }

            #endregion
        }

        /// <summary>
        /// 添加到Fp
        /// </summary>
        /// <param name="al"></param>
        [Obsolete("作废", true)]
        private void AddObjectToFpShort(ArrayList al)
        {
            #region 为空返回
            if (al.Count <= 0)
            {
                return;
            }
            #endregion

            #region 定义变量
            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = -1;
            int MaxRowNo = -1;
            #endregion

            #region 按是否打印分组
            foreach (Neusoft.HISFC.Models.Order.Inpatient.Order temp in al)
            {
                if (temp.PageNo == -1)
                {
                    alPageNull.Insert(alPageNull.Count, temp);
                }
                else
                {
                    if (!hsPageNo.ContainsKey(temp.PageNo))
                    {
                        alPageNo.Insert(alPageNo.Count, temp.PageNo);

                        hsPageNo.Add(temp.PageNo, new ArrayList());

                        (hsPageNo[temp.PageNo] as ArrayList).Insert((hsPageNo[temp.PageNo] as ArrayList).Count, temp);
                    }
                    else
                    {
                        (hsPageNo[temp.PageNo] as ArrayList).Insert((hsPageNo[temp.PageNo] as ArrayList).Count, temp);
                    }
                }
            }
            #endregion

            #region 将已打印的显示
            for (int i = 0; i < alPageNo.Count; i++)
            {
                int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(alPageNo[i].ToString());

                if (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) > MaxPageNo)
                {
                    MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo);
                    MaxRowNo = -1;
                }

                ArrayList alTemp = hsPageNo[pageNo] as ArrayList;

                if (i == 0)
                {
                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {
                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "【" + order.Item.Name + "】的实际打印行号为" + (order.RowNo + 1).ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        this.fpShortOrder.Sheets[0].Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginDate, GetPrintDate(order).Month.ToString() + "-" + GetPrintDate(order).Day.ToString());
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginTime, GetPrintDate(order).ToShortTimeString());

                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, GetOrderItem(order, false));

                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(order.DoseOnce) + order.DoseUnit);
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Usage, order.Usage.Name);
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Freq, order.Frequency.ID);
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Qty, Neusoft.FrameWork.Public.String.ToSimpleString(order.Qty) + order.Unit);
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Memo, order.Memo);

                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.RecipeDoct, order.Doctor.Name);
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.ConfirmTime != DateTime.MinValue)
                        {
                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmDate, order.ConfirmTime.Month.ToString() + "-" + order.ConfirmTime.Day.ToString());
                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmTime, order.ConfirmTime.ToShortTimeString());
                        }

                        if (order.DCOper.OperTime != DateTime.MinValue)
                        {
                            //this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.DCFlage, "DC");
                            //this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.DCDoct, order.DCOper.Name);
                        }


                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.CombNO, order.Combo.ID);
                        this.fpShortOrder.Sheets[0].Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Classes.Function.DrawComboLeft(this.fpShortOrder.Sheets[0], (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);

                    this.fpShortOrder.Sheets[0].Tag = pageNo;
                    this.fpShortOrder.Sheets[0].SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(sheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);

                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order o in alTemp)
                    {
                        if (o.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(o.OrderType.Name + "【" + o.Item.Name + "】的实际打印行号为" + o.RowNo.ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[o.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(o).Month.ToString() + "-" + GetPrintDate(o).Day.ToString());
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(o).ToShortTimeString());

                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, GetOrderItem(o, false));


                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(o.DoseOnce) + o.DoseUnit);
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Usage, o.Usage.Name);
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Freq, o.Frequency.ID);
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Qty, Neusoft.FrameWork.Public.String.ToSimpleString(o.Qty) + o.Unit);
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Memo, o.Memo);

                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.RecipeDoct, o.Doctor.Name);
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, o.Nurse.Name);

                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmDate, o.ConfirmTime.Month.ToString() + "-" + o.ConfirmTime.Day.ToString());
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmTime, o.ConfirmTime.ToShortTimeString());
                        }

                        if (o.DCOper.OperTime != DateTime.MinValue)
                        {
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.DCFlage, "DC");
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.DCDoct, o.DCOper.Name);
                        }
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.CombNO, o.Combo.ID);
                        sheet.Rows[o.RowNo].Tag = o;

                        if (pageNo == MaxPageNo && o.RowNo > MaxRowNo)
                        {
                            MaxRowNo = o.RowNo;
                        }
                    }

                    Classes.Function.DrawComboLeft(sheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);

                    sheet.Tag = pageNo;
                    sheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }
            }

            #endregion

            #region 显示未打印医嘱

            bool fromOne = true;
            int iniIndex = -1;
            int endIndex = -1;

            if (MaxPageNo == -1)
            {
                MaxPageNo++;
                fromOne = false;
            }

            if (MaxRowNo == -1)
            {
                MaxRowNo++;
            }

            FarPoint.Win.Spread.SheetView activeSheet = this.fpShortOrder.Sheets[MaxPageNo];


            activeSheet.Tag = MaxPageNo++;
            activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

            if (fromOne)
            {
                iniIndex = 1;
                endIndex = alPageNull.Count + 1;
            }
            else
            {
                iniIndex = 0;
                endIndex = alPageNull.Count;
            }

            for (; iniIndex < endIndex; iniIndex++)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order oTemp;

                if (fromOne)
                {
                    oTemp = alPageNull[iniIndex - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    oTemp = alPageNull[iniIndex] as Neusoft.HISFC.Models.Order.Inpatient.Order;
                }
                int activeRow = (iniIndex + MaxRowNo) % rowNum;
                if (activeRow == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(sheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, GetOrderItem(oTemp, false));


                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Memo, oTemp.Memo);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Unit);

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.Month.ToString() + "-" + oTemp.ConfirmTime.Day.ToString());
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ConfirmTime.ToShortTimeString());
                    }
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, GetOrderItem(oTemp, false));

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Memo, oTemp.Memo);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Unit);

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.Month.ToString() + "-" + oTemp.ConfirmTime.Day.ToString());
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ConfirmTime.ToShortTimeString());
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.DCFlage, "DC");
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
            }

            #endregion

            #region 处理换科打印问题

            if (isShiftDeptNextPag)
            {
                //需要处理
                string deptCode = "";
                //处理重整一周后的自动分页
                for (int i = 0; i < this.fpShortOrder.Sheets.Count; i++)
                {
                    for (int j = 0; j < this.fpShortOrder.Sheets[i].Rows.Count; j++)
                    {
                        Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpShortOrder.Sheets[i].Rows[j].Tag as
                            Neusoft.HISFC.Models.Order.Inpatient.Order;

                        if (order == null)
                            continue;

                        if (order.PageNo >= 0)
                            continue;

                        if (string.IsNullOrEmpty(deptCode))
                        {
                            deptCode = order.ReciptDept.ID;
                        }
                        else if (deptCode.Trim() != order.ReciptDept.ID)
                        {
                            this.AddObjectToFpShortAfter(i, j);
                            deptCode = order.ReciptDept.ID;
                        }
                    }
                }
            }

            #endregion

            DealShortOrderCrossPage();
        }

        /// <summary>
        /// 转科分页后添加到Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        //[Obsolete("作废", true)]
        private void AddObjectToFpShortAfter(int sheet, int row)
        {
            #region 定义变量

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            //int row = this.fpShortOrder.Sheets[sheet].ActiveRowIndex;

            if (this.fpShortOrder.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("所选项目为空！");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.fpShortOrder.Sheets[sheet].Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

            if (MessageBox.Show("确定要从" + ord.Item.Name + "开始另起一页吗?此操作不可撤销！", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "已经打印过，不能另起一页！");
                return;
            }

            #endregion

            #region 获取剩余数据

            for (int i = row; i < this.fpShortOrder.Sheets[sheet].Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    alPageNull.Add(this.fpShortOrder.ActiveSheet.Rows[i].Tag);
                }
            }

            for (int i = sheet + 1; i < this.fpShortOrder.Sheets.Count; i++)
            {
                for (int j = 0; j < this.fpShortOrder.Sheets[i].Rows.Count; j++)
                {
                    if (this.fpShortOrder.Sheets[i].Rows[j].Tag != null)
                    {
                        alPageNull.Add(this.fpShortOrder.Sheets[i].Rows[j].Tag);
                    }
                }
            }

            #endregion

            #region 清空数据

            for (int j = row; j < this.fpShortOrder.Sheets[sheet].Rows.Count; j++)
            {
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.BeginDate, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.BeginTime, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ItemName, "");


                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.OnceDose, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Usage, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Freq, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Qty, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Memo, "");

                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.RecipeDoct, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ConfirmNurse, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ConfirmDate, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ConfirmTime, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.DCFlage, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.DCDoct, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.CombNO, "");
                this.fpShortOrder.ActiveSheet.Rows[j].Tag = null;
            }

            #endregion

            #region 保留一个Sheet

            if (this.fpShortOrder.Sheets.Count > 1)
            {
                for (int j = this.fpShortOrder.Sheets.Count - 1; j > sheet; j--)
                {
                    this.fpShortOrder.Sheets.RemoveAt(j);
                }
            }

            #endregion

            #region 显示未打印医嘱

            int iniIndex = -1;
            int endIndex = -1;

            if (MaxRowNo == -1)
            {
                MaxRowNo++;
            }

            FarPoint.Win.Spread.SheetView orgSheet = new FarPoint.Win.Spread.SheetView();

            this.InitShortSheet(orgSheet);

            this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.fpShortOrder.Sheets[MaxPageNo];


            activeSheet.Tag = MaxPageNo++;
            activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

            iniIndex = 0;
            endIndex = alPageNull.Count;

            for (; iniIndex < endIndex; iniIndex++)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order oTemp;

                oTemp = alPageNull[iniIndex] as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if ((iniIndex + MaxRowNo) % rowNum == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView newsheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(newsheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, GetOrderItem(oTemp, false));


                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Memo, oTemp.Memo);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Unit);

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);


                    if (oTemp.ExecOper.OperTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                        // activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.Name);
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                        // activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.Name);
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCFlage, "DC");
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, GetOrderItem(oTemp, false));


                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.OnceDose, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Memo, oTemp.Memo);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, Neusoft.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Unit);

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);


                    if (oTemp.ExecOper.OperTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.Name);
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCFlage, "DC");
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
            }

            #endregion
        }

        #endregion

        #region 打印

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            DialogResult rr = MessageBox.Show("确定要打印该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            print.SetPageSize(size);
            print.IsCanCancel = false;

            string errText = "";
            frmNotice frmNotice = new frmNotice();

            int pagNo = 0;

            #region 长期医嘱
            if (this.tabControl1.SelectedIndex == 0)
            {
                try
                {
                    if (!this.CanPrintForLong(ref errText))
                    {
                        if (!string.IsNullOrEmpty(errText))
                        {
                            MessageBox.Show(errText);
                        }
                        return;
                    }

                    #region 重打
                    if (!this.GetIsPrintAgainForLong())
                    {
                        DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (r == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                            {
                                //设置标题的提示文字的可见性
                                this.SetTitleVisible(true, false);
                                //设置列头的可见性
                                this.SetVisibleForLong(Color.White, false, -1);
                                //设置显示的格式
                                SetStyleForFp(true, Color.White, BorderStyle.None);

                            }
                            else
                            {
                                //设置列头的可见性
                                this.SetVisibleForLong(Color.Black, false, -1);
                                SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                            }

                            //设置重打显示的内容
                            this.SetRePrintContentsForLong();

                            //设置显示患者改变信息
                            this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder(true));

                            this.pnLongPag.Dock = DockStyle.None;

                            this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                            //p.ShowPrintPageDialog();
                            
                            //不管是否管理员，开放预览功能 
                            //if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                            //{
                            //   print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                            //}
                            //else
                            //{
                            //    if (cbxPreview.Checked)
                            //    {
                            //        print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                            //    }
                            //    else
                            //    {
                            //        print.PrintPage(this.leftValue, this.topValue, this.pnLongOrder);
                            //    }
                            //}
                            print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);

                            
                            this.pnLongPag.Dock = DockStyle.Bottom;

                            this.SetTitleVisible(true, true);

                            this.SetVisibleForLong(Color.Black, true, -1);

                            SetStyleForFp(true, Color.White, BorderStyle.None);

                            pagNo = this.fpLongOrder.ActiveSheetIndex;
                            this.QueryPatientOrder();
                            this.fpLongOrder.ActiveSheetIndex = pagNo;

                            return;
                        }
                    }
                    #endregion

                    #region 首次打印
                    else
                    {
                        this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder(true));

                        if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                        {
                            this.SetTitleVisible(true, false);

                            this.SetVisibleForLong(Color.White, false, -1);

                            SetStyleForFp(true, Color.White, BorderStyle.None);
                        }
                        else
                        {
                            if (this.GetFirstOrder(true).PageNo == -1)
                            {
                                this.SetVisibleForLong(Color.Black, false, -1);

                                SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                            }
                            else
                            {
                                SetTitleVisible(true, false);

                                this.SetVisibleForLong(Color.White, false, -1);

                                this.SetStyleForFp(true, Color.White, BorderStyle.None);
                            }
                        }

                        if (OrderPrintType != EnumPrintType.PrintWhenPatientOut)
                        {
                            this.SetPrintContentsForLong();
                        }
                    }
                    #endregion

                    this.pnLongPag.Dock = DockStyle.None;

                    this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);


                    if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                    }
                    else
                    {
                        if (this.cbxPreview.Checked)
                        {
                            print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                        }
                        else
                        {
                            print.PrintPage(this.leftValue, this.topValue, this.pnLongOrder);
                        }
                    }
                    
                    this.pnLongPag.Dock = DockStyle.Bottom;

                    SetStyleForFp(true, Color.White, BorderStyle.None);

                    DialogResult dia;

                    frmNotice.label1.Text = "续打长期医嘱单是否成功?";

                    frmNotice.ShowDialog();

                    dia = frmNotice.dr;

                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("确定续打没有成功吗？误操作会造成医嘱单出现空行！", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //确定续打没有成功，没话说了
                        }
                        else
                        {
                            dia = DialogResult.Yes;
                        }
                    }

                    if (dia == DialogResult.Yes)
                    {
                        //Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                        //出院打印时才允许隔页打印
                        if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
                        {
                            for (int index = 0; index <= this.fpLongOrder.ActiveSheetIndex; index++)
                            {
                                if (this.UpdateOrderForLong(index) <= 0)
                                {
                                    //Neusoft.FrameWork.Management.PublicTrans.RollBack();

                                    SetTitleVisible(true, true);

                                    this.SetVisibleForLong(Color.Black, true, -1);

                                    pagNo = this.fpLongOrder.ActiveSheetIndex;
                                    this.QueryPatientOrder();
                                    this.fpLongOrder.ActiveSheetIndex = pagNo;

                                    return;
                                }
                            }
                        }
                        else
                        {

                            if (this.UpdateOrderForLong(this.fpLongOrder.ActiveSheetIndex) <= 0)
                            {
                                //Neusoft.FrameWork.Management.PublicTrans.RollBack();

                                SetTitleVisible(true, true);

                                this.SetVisibleForLong(Color.Black, true, -1);

                                pagNo = this.fpLongOrder.ActiveSheetIndex;
                                this.QueryPatientOrder();
                                this.fpLongOrder.ActiveSheetIndex = pagNo;

                                return;
                            }
                        }
                        //Neusoft.FrameWork.Management.PublicTrans.Commit();
                    }

                    SetTitleVisible(true, true);

                    this.SetVisibleForLong(Color.Black, true, -1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    SetTitleVisible(true, true);

                    this.SetVisibleForLong(Color.Black, true, -1);
                }

                pagNo = this.fpLongOrder.ActiveSheetIndex;
                this.QueryPatientOrder();
                this.fpLongOrder.ActiveSheetIndex = pagNo;

                #region 打印完长嘱后查询医嘱页码是否有误 by huangchw 2012-09-12
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
            }
            #endregion

            #region 临时医嘱
            else
            {
                try
                {
                    if (!this.CanPrintForShort(ref errText))
                    {
                        if (!string.IsNullOrEmpty(errText))
                        {
                            MessageBox.Show(errText);
                        }
                        return;
                    }

                    if (!this.GetIsPrintAgainForShort())
                    {
                        DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (r == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {

                            if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                            {
                                SetTitleVisible(false, false);
                                this.SetVisibleForShort(Color.White, false, -1);
                                this.SetStyleForFp(false, Color.White, BorderStyle.None);
                            }
                            else
                            {
                                this.SetVisibleForShort(Color.Black, false, -1);

                                this.SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
                            }

                            this.SetRePrintContentsForShort();

                            this.ucShortOrderBillHeader.SetChangedInfo(this.GetFirstOrder(false));

                            this.pnShortPag.Dock = DockStyle.None;

                            this.pnShortPag.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                            //p.ShowPrintPageDialog();
                            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                            }
                            else 
                            {
                                if (cbxPreview.Checked)
                                {
                                    print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                                }
                                else
                                {
                                    print.PrintPage(this.leftValue, this.topValue, this.pnShortOrder);
                                }
                            }
                            //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                            this.pnShortPag.Dock = DockStyle.Bottom;
                            //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);

                            this.SetStyleForFp(false, Color.White, BorderStyle.None);

                            SetTitleVisible(false, true);

                            this.SetVisibleForShort(Color.Black, true, -1);

                            pagNo = this.fpShortOrder.ActiveSheetIndex;
                            this.QueryPatientOrder();
                            this.fpShortOrder.ActiveSheetIndex = pagNo;
                            return;
                        }
                    }
                    else
                    {
                        this.ucShortOrderBillHeader.SetChangedInfo(this.GetFirstOrder(false));

                        if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                        {
                            SetTitleVisible(false, false);

                            this.SetVisibleForShort(Color.White, false, -1);

                            SetStyleForFp(false, Color.White, BorderStyle.None);
                        }
                        else
                        {
                            if (this.GetFirstOrder(false).PageNo == -1)
                            {
                                this.SetVisibleForShort(Color.Black, false, -1);

                                SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
                            }
                            else
                            {
                                SetTitleVisible(false, false);

                                this.SetVisibleForShort(Color.White, false, -1);

                                this.SetStyleForFp(false, Color.White, BorderStyle.None);
                            }
                        }

                        if (OrderPrintType != EnumPrintType.PrintWhenPatientOut)
                        {
                            this.SetPrintContentsForShort();
                        }
                    }

                    this.pnShortPag.Dock = DockStyle.None;

                    this.pnShortPag.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);
                    if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                    }
                    else
                    {
                        if (cbxPreview.Checked)
                        {
                            print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                        }
                        else
                        {
                            print.PrintPage(this.leftValue, this.topValue, this.pnShortOrder);
                        }
                    }
                    this.pnShortPag.Dock = DockStyle.Bottom;

                    SetStyleForFp(false, Color.White, BorderStyle.None);

                    DialogResult dia;

                    frmNotice.label1.Text = "续打临时医嘱单是否成功?";

                    frmNotice.ShowDialog();

                    dia = frmNotice.dr;

                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("确定续打没有成功吗？误操作会造成医嘱单出现空行！", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //确定续打没有成功，没话说了
                        }
                        else
                        {
                            dia = DialogResult.Yes;
                        }
                    }

                    if (dia == DialogResult.Yes)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                        //出院打印时才允许隔页打印
                        if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
                        {
                            for (int index = 0; index <= this.fpShortOrder.ActiveSheetIndex; index++)
                            {
                                if (this.UpdateOrderForShort(index) <= 0)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();

                                    SetTitleVisible(false, true);

                                    this.SetVisibleForShort(Color.Black, true, -1);

                                    pagNo = this.fpShortOrder.ActiveSheetIndex;
                                    this.QueryPatientOrder();
                                    this.fpShortOrder.ActiveSheetIndex = pagNo;

                                    return;
                                }
                            }
                        }
                        else  //非出院打印
                        {
                            if (this.UpdateOrderForShort(this.fpShortOrder.ActiveSheetIndex) <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();

                                SetTitleVisible(false, true);

                                this.SetVisibleForShort(Color.Black, true, -1);

                                pagNo = this.fpShortOrder.ActiveSheetIndex;
                                this.QueryPatientOrder();
                                this.fpShortOrder.ActiveSheetIndex = pagNo;

                                return;
                            }
                        }
                        Neusoft.FrameWork.Management.PublicTrans.Commit();
                    }

                    SetTitleVisible(false, true);

                    this.SetVisibleForShort(Color.Black, true, -1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    SetTitleVisible(false, true);

                    this.SetVisibleForShort(Color.Black, true, -1);
                }

                pagNo = this.fpShortOrder.ActiveSheetIndex;
                this.QueryPatientOrder();
                this.fpShortOrder.ActiveSheetIndex = pagNo;
                
                #region 打印完临嘱后查询医嘱页码是否有误 by huangchw 2012-09-12
                int count = this.orderManager.CheckPageRowNoAndGetFlag(this.myPatientInfo.ID,"0");//查询临嘱
                if (count < 0)
                {
                     MessageBox.Show("页码提取标志查询出错。");
                 }
                 else if (count > 0)
                 {
                     //MessageBox.Show("注意：有 " + count + " 条医嘱更新页码出错，请联系信息科。");
                 }
                 #endregion
            }
            #endregion
        }

        /// <summary>
        /// 重新打印
        /// </summary>
        private void PrintAgain()
        {
            DialogResult rr = MessageBox.Show("确定要打印该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            print.SetPageSize(size);
            print.IsCanCancel = false;

            string errText = "";
            frmNotice frmNotice = new frmNotice();

            #region //长期医嘱
            if (this.tabControl1.SelectedIndex == 0)
            {
                try
                {
                    if (!this.CanPrintForLong(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (r == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                        {
                            //设置标题的提示文字的可见性
                            SetTitleVisible(true, false);
                            //设置列头的可见性
                            this.SetVisibleForLong(Color.White, false, -1);
                            //设置显示的格式
                            SetStyleForFp(true, Color.White, BorderStyle.None);

                        }
                        else
                        {
                            //设置列头的可见性
                            this.SetVisibleForLong(Color.Black, false, -1);
                            SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                        }

                        //设置重打显示的内容
                        this.SetRePrintContentsForLong();

                        //设置显示患者改变信息
                        this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder(true));

                        this.pnLongPag.Dock = DockStyle.None;

                        this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                        if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                        {
                            print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                        }
                        else
                        {
                            if (cbxPreview.Checked)
                            {
                                print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                            }
                            else
                            {
                                print.PrintPage(this.leftValue, this.topValue, this.pnLongOrder);
                            }
                        }

                        this.pnLongPag.Dock = DockStyle.Bottom;

                        SetTitleVisible(true, true);

                        this.SetVisibleForLong(Color.Black, true, -1);

                        SetStyleForFp(true, Color.White, BorderStyle.None);

                        this.QueryPatientOrder();

                        return;

                    }
                }
                catch
                {
                    SetTitleVisible(true, true);

                    this.SetVisibleForLong(Color.Black, true, -1);

                    this.QueryPatientOrder();
                }
            }
            #endregion

            #region //临时医嘱
            else
            {
                try
                {
                    if (!this.CanPrintForShort(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (r == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                        {
                            SetTitleVisible(false, false);
                            this.SetVisibleForShort(Color.White, false, -1);
                            this.SetStyleForFp(false, Color.White, BorderStyle.None);
                        }
                        else
                        {
                            this.SetVisibleForShort(Color.Black, false, -1);
                            this.SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
                        }

                        this.SetRePrintContentsForShort();

                        this.ucShortOrderBillHeader.SetChangedInfo(this.GetFirstOrder(false));

                        this.pnShortPag.Dock = DockStyle.None;

                        this.pnShortPag.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);
                        //this.neuPanel1.Location = new Point(this.fpShortOrder.Location.X, 910);
                        //p.ShowPrintPageDialog();

                        if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                        {
                            print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                        }
                        else
                        {
                            if (cbxPreview.Checked)
                            {
                                print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                            }
                            else
                            {
                                print.PrintPage(this.leftValue, this.topValue, this.pnShortOrder);
                            }
                        }

                        //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                        this.pnShortPag.Dock = DockStyle.Bottom;
                        //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);

                        this.SetStyleForFp(false, Color.White, BorderStyle.None);

                        SetTitleVisible(false, true);

                        this.SetVisibleForShort(Color.Black, true, -1);

                        this.QueryPatientOrder();

                        return;
                    }
                }
                catch
                {
                    SetTitleVisible(false, true);

                    this.SetVisibleForShort(Color.Black, true, -1);

                    this.QueryPatientOrder();
                }
            }
            #endregion
        }

        /// <summary>
        /// 补打单条项目
        /// </summary>
        private void PrintSingleItem()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            print.SetPageSize(size);
            print.IsCanCancel = false;

            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.fpLongOrder.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.fpLongOrder.ActiveSheet.ActiveRowIndex < 0)
                {
                    return;
                }

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("项目:" + order.Item.Name + "尚未打印");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("确定要重打项目:" + order.Item.Name, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                SetTitleVisible(true, false);

                this.SetVisibleForLong(Color.White, false, fpLongOrder.ActiveSheet.ActiveRowIndex);

                this.SetStyleForFp(true, Color.White, BorderStyle.None);

                for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.fpLongOrder.ActiveSheet.ActiveRowIndex)
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");


                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.OnceDose, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Usage, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Freq, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Memo, "");

                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                    else
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                }

                if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(leftValue, topValue, this.pnLongOrder);
                }
                else
                {
                    if (cbxPreview.Checked)
                    {
                        print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                    }
                    else
                    {
                        print.PrintPage(leftValue, topValue, this.pnLongOrder);
                    }
                }

                SetTitleVisible(true, true);

                this.SetVisibleForLong(Color.Black, true, fpLongOrder.ActiveSheet.ActiveRowIndex);

                this.QueryPatientOrder();
            }
            else
            {
                if (this.fpShortOrder.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.fpShortOrder.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.fpShortOrder.ActiveSheet.ActiveRowIndex <= 0)
                {
                    return;
                }

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpShortOrder.ActiveSheet.Rows[this.fpShortOrder.ActiveSheet.ActiveRowIndex].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("项目:" + order.Item.Name + "尚未打印");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("确定要重打项目:" + order.Item.Name, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                SetTitleVisible(false, false);

                this.SetVisibleForShort(Color.White, false, fpShortOrder.ActiveSheet.ActiveRowIndex);

                this.SetStyleForFp(false, Color.White, BorderStyle.None);

                for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.fpShortOrder.ActiveSheet.ActiveRowIndex)
                    {
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");


                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.OnceDose, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Usage, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Freq, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Memo, "");

                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");//执行时间不打
                    }
                    else
                    {
                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");

                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");//执行日期不打
                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");//执行时间不打
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");//执行时间不打
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");//执行时间不打
                    }
                }


                if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(this.leftValue, topValue, this.pnShortOrder);
                }
                else
                {
                    if (cbxPreview.Checked)
                    {
                        print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                    }
                    else
                    {
                        print.PrintPage(this.leftValue, topValue, this.pnShortOrder);
                    }
                }

                SetTitleVisible(false, true);

                this.SetVisibleForShort(Color.Black, true, fpShortOrder.ActiveSheet.ActiveRowIndex);

                this.QueryPatientOrder();
            }
        }

        /// <summary>
        /// 补打单条项目停止时间
        /// </summary>
        private void PrintSingleDate()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            print.SetPageSize(size);
            print.IsCanCancel = false;

            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.fpLongOrder.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.fpLongOrder.ActiveSheet.ActiveRowIndex < 0)
                {
                    return;
                }

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag
                    as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("项目:" + order.Item.Name + "尚未打印");
                    return;
                }

                if (order.Status < 3)
                {
                    MessageBox.Show("项目:" + order.Item.Name + "尚未停止");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("确定要只打印项目:" + order.Item.Name + "的停止时间?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                SetTitleVisible(true, false);

                this.SetVisibleForLong(Color.White, false, fpLongOrder.ActiveSheet.ActiveRowIndex);

                for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.fpLongOrder.ActiveSheet.ActiveRowIndex)
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");


                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.OnceDose, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Usage, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Freq, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Memo, "");

                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                    else
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");


                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.OnceDose, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Usage, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Freq, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Memo, "");

                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                }

                if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(leftValue, topValue, this.pnLongOrder);
                }
                else
                {
                    if (cbxPreview.Checked)
                    {
                        print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                    }
                    else
                    {
                        print.PrintPage(leftValue, topValue, this.pnLongOrder);
                    }
                }

                SetTitleVisible(true, true);

                this.SetVisibleForLong(Color.Black, true, fpLongOrder.ActiveSheet.ActiveRowIndex);

                this.QueryPatientOrder();
            }
        }

        #endregion

        #region 获取打印提示

        /// <summary>
        /// 是否重打 true续打，false重打
        /// </summary>
        /// <returns></returns>
        private bool GetIsPrintAgainForLong()
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return false;
                    }

                    if (oT.GetFlag == "0")
                    {
                        return true;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 是否重打 true续打，false重打
        /// </summary>
        /// <returns></returns>
        private bool GetIsPrintAgainForShort()
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return false;
                    }

                    if (oT.GetFlag == "0")
                    {
                        return true;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取打印提示
        /// </summary>
        /// <returns></returns>
        private bool CanPrintForLong(ref string errText)
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                errText = "获得页码出错！";
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "获得页码出错！\r\n页码为负数！";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "1");

            //出院打印时才允许隔页打印
            if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
            {
                if (pageNo > maxPageNo + 1)
                {
                    if (MessageBox.Show("第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！是否继续打印第" + (pageNo + 1).ToString() + "页？\r\n\r\n继续打印将更新所有前" + (pageNo + 1).ToString() + "页打印标记！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
                //判断上一页是否打印完全
                if (pageNo == maxPageNo + 1 && maxPageNo != -1)
                {
                    bool canprintflag = true;
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = null;
                    for (int j = 0; j < this.rowNum; j++)
                    {
                        if (this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                        {
                            oT = this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        if (MessageBox.Show("第" + (maxPageNo + 1).ToString() + "页尚有未打印医嘱！是否继续打印第" + (pageNo + 1).ToString() + "页？\r\n\r\n继续打印将更新所有前" + (pageNo + 1).ToString() + "页打印标记！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (pageNo > maxPageNo + 1)
                {
                    errText = "第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！";
                    return false;
                }

                //判断上一页是否打印完全
                if (pageNo == maxPageNo + 1 && maxPageNo != -1)
                {
                    bool canprintflag = true;
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = null;
                    for (int j = 0; j < this.rowNum; j++)
                    {
                        if (this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                        {
                            oT = this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        errText = "第" + (maxPageNo + 1).ToString() + "页尚有未打印医嘱！";
                        return false;
                    }
                }
            }

            MessageBox.Show("请确定已放入第" + (pageNo + 1).ToString() + "页长期医嘱单！");

            return true;
        }

        /// <summary>
        /// 获取打印提示
        /// </summary>
        /// <returns></returns>
        private bool CanPrintForShort(ref string errText)
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                errText = "获得页码出错！";
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "获得页码出错！";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "0");

            //出院打印时才允许隔页打印
            if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
            {
                if (pageNo > maxPageNo + 1)
                {
                    if (MessageBox.Show("第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！是否继续打印第" + (pageNo + 1).ToString() + "页？\r\n\r\n继续打印将更新所有前" + (pageNo + 1).ToString() + "页打印标记！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        errText = "已取消打印！";
                        return false;
                    }
                }
                if (pageNo == maxPageNo + 1 && maxPageNo != -1)
                {
                    bool canprintflag = true;
                    for (int j = 0; j < rowNum; j++)
                    {
                        if (this.fpShortOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                        {
                            Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.Sheets[maxPageNo].Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        if (MessageBox.Show("第" + (maxPageNo + 1).ToString() + "页尚有未打印医嘱！是否继续打印第" + (pageNo + 1).ToString() + "页？\r\n\r\n继续打印将更新所有前" + (pageNo + 1).ToString() + "页打印标记！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (pageNo > maxPageNo + 1)
                {
                    errText = "第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！";
                    return false;
                }

                if (pageNo == maxPageNo + 1 && maxPageNo != -1)
                {
                    bool canprintflag = true;
                    for (int j = 0; j < rowNum; j++)
                    {
                        if (this.fpShortOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                        {
                            Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.Sheets[maxPageNo].Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        errText = "第" + (maxPageNo + 1).ToString() + "页尚有未打印医嘱！";
                        return false;
                    }
                }

                MessageBox.Show("请确定已放入第" + (pageNo + 1).ToString() + "页临时医嘱单！");
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
        private int UpdateOrderForLong(int sheetIndex)
        {
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            if (this.fpLongOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = sheetIndex; //Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            Neusoft.HISFC.Models.Order.Inpatient.Order oT = null;
            for (int i = 0; i < this.fpLongOrder.Sheets[sheetIndex].Rows.Count; i++)
            {
                if (this.fpLongOrder.Sheets[sheetIndex].Rows[i].Tag != null)
                {
                    oT = this.fpLongOrder.Sheets[sheetIndex].Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("实体转换出错！");
                        return -1;
                    }

                    if (oT.Patient.ID != this.myPatientInfo.ID)
                    {
                        continue;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //if (myOrder.UpdatePageNoAndRowNo(this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        if (myOrder.UpdatePageRowNoAndGetflag(
                            this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString(), "0", "0") <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新页码出错！" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() ,"2", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "1", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString(), "1", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString(), "2", "1") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                    }
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 更新医嘱页码和提取标志
        /// </summary>
        /// <param name="chgBed"></param>
        /// <returns></returns>
        private int UpdateOrderForShort(int sheetIndex)
        {
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            

            if (this.fpShortOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = sheetIndex;//Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            Neusoft.HISFC.Models.Order.Inpatient.Order oT = null;
            for (int i = 0; i < this.fpShortOrder.Sheets[sheetIndex].Rows.Count; i++)
            {
                if (this.fpShortOrder.Sheets[sheetIndex].Rows[i].Tag != null)
                {
                    oT = this.fpShortOrder.Sheets[sheetIndex].Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT.Patient.ID != this.myPatientInfo.ID)
                    {
                        continue;
                    }

                    if (oT == null)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("实体转换出错！");
                        return -1;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //if (myOrder.UpdatePageNoAndRowNo(this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        if (myOrder.UpdatePageRowNoAndGetflag(
                            this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() , "0", "0") <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新页码出错！" + oT.Item.Name);
                            return -1;
                        }
                        
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() ,"2", "0") <= 0)
                            { 
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "1", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString(), "1", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString(), "2", "1") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                    }

                }//if
            }//for

            Neusoft.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        #endregion

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ArrayList GetLongOrderByCombId(Neusoft.HISFC.Models.Order.Inpatient.Order order)
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.alLong.Count; i++)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order ord = alLong[i] as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if (order.Combo.ID == ord.Combo.ID)
                {
                    al.Add(ord);
                }
            }
            return al;
        }

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ArrayList GetShortOrderByCombId(Neusoft.HISFC.Models.Order.Inpatient.Order order)
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.alShort.Count; i++)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order ord = alShort[i] as Neusoft.HISFC.Models.Order.Inpatient.Order;

                if (order.Combo.ID == ord.Combo.ID)
                {
                    al.Add(ord);
                }
            }

            return al;
        }

        /// <summary>
        /// 处理换页组号打印
        /// </summary>
        private void DealLongOrderCrossPage()
        {
            for (int i = 0; i < this.fpLongOrder.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.fpLongOrder.Sheets[i];

                if (view.Rows[view.Rows.Count - 1].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order ot = view.Rows[view.Rows.Count - 1].Tag
                        as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (ot != null)
                    {
                        ArrayList alOrders = this.GetLongOrderByCombId(ot);

                        if (alOrders.Count <= 1)
                        {
                            continue;
                        }
                        else
                        {
                            for (int j = 0; j < view.Rows.Count; j++)
                            {
                                Neusoft.HISFC.Models.Order.Inpatient.Order ot1 = view.Rows[j].Tag
                                    as Neusoft.HISFC.Models.Order.Inpatient.Order;

                                if (ot1 != null)
                                {
                                    if (ot1.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "┏");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "┗");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "┃");
                                    }
                                }

                            }

                            if (i != this.fpLongOrder.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.fpLongOrder.Sheets[i + 1];

                                for (int j = 0; j < viewNext.Rows.Count; j++)
                                {
                                    Neusoft.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "┏");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "┗");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "┃");
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
        /// 处理换页组号打印
        /// </summary>
        private void DealShortOrderCrossPage()
        {
            for (int i = 0; i < this.fpShortOrder.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.fpShortOrder.Sheets[i];

                if (view.Rows[view.Rows.Count - 1].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order ot = view.Rows[view.Rows.Count - 1].Tag
                        as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (ot != null)
                    {
                        ArrayList alOrders = this.GetShortOrderByCombId(ot);

                        if (alOrders.Count <= 1)
                        {
                            continue;
                        }
                        else
                        {
                            for (int j = 0; j < view.Rows.Count; j++)
                            {
                                Neusoft.HISFC.Models.Order.Inpatient.Order ot1 = view.Rows[j].Tag
                                    as Neusoft.HISFC.Models.Order.Inpatient.Order;

                                if (ot1 != null)
                                {
                                    if (ot1.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "┏");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "┗");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "┃");
                                    }
                                }

                            }

                            if (i != this.fpShortOrder.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.fpShortOrder.Sheets[i + 1];

                                for (int j = 0; j < viewNext.Rows.Count; j++)
                                {
                                    Neusoft.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag
                                        as Neusoft.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "┏");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "┗");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "┃");
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
        /// 设置打印显示
        /// </summary>
        /// <returns></returns>
        private void SetPrintContentsForLong()
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    if (oT.GetFlag == "0")
                    {
                        continue;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.Status != 3)
                        {
                        }
                        else
                        {
                        }
                        SetTitleVisible(true, false);
                    }
                }
            }
        }

        /// <summary>
        /// 设置打印显示
        /// </summary>
        /// <returns></returns>
        private void SetPrintContentsForShort()
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                        //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                        //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//执行日期不打
                        //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//执行时间不打
                        continue;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.Status != 3)
                        {
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoDate,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoTime,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,3,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Qty,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,8,"");
                        }
                        else
                        {
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoDate,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoTime,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,3,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Qty,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//执行日期不打
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//执行时间不打
                        }
                        SetTitleVisible(false, false);
                    }
                    else
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 设置长期医嘱重打显示内容
        /// </summary>
        private void SetRePrintContentsForLong()
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }
                }
            }

            this.lblPageLong.Visible = true;
        }

        /// <summary>
        /// 设置临时医嘱重打显示内容
        /// </summary>
        private void SetRePrintContentsForShort()
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//执行日期不打
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//执行时间不打
                    //this.fpShortOrder.ActiveSheet.SetValue(i,8,"");//执行时间不打
                }
            }

            this.lblPageShort.Visible = true;
        }

        /// <summary>
        /// 获取Sheet第一条医嘱
        /// </summary>
        /// <param name="longOrder"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.Order.Inpatient.Order GetFirstOrder(bool longOrder)
        {
            if (longOrder)
            {
                if (this.fpLongOrder.ActiveSheet.Rows.Count > 0)
                {
                    if (this.fpLongOrder.ActiveSheet.Rows[0].Tag != null)
                    {
                        return this.fpLongOrder.ActiveSheet.Rows[0].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
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
            else
            {
                if (this.fpShortOrder.ActiveSheet.Rows.Count > 0)
                {
                    if (this.fpShortOrder.ActiveSheet.Rows[0].Tag != null)
                    {
                        return this.fpShortOrder.ActiveSheet.Rows[0].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
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
        }

        /// <summary>
        /// 设置Fp格式
        /// </summary>
        /// <param name="longOrder">是否长嘱</param>
        /// <param name="color"></param>
        private void SetStyleForFp(bool longOrder, Color color, System.Windows.Forms.BorderStyle border)
        {
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;

            if (longOrder)
            {
                this.fpLongOrder.ActiveSheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty,
                                    System.Drawing.Color.Empty, color,
                                    FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White,
                                    System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                    System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                    System.Drawing.Color.Empty, true, false, false, true, true);

                if (border == BorderStyle.None)
                {
                    for (int i = 0; i < this.fpLongOrder.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.fpLongOrder.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, false, false, false, false);
                    }
                }
                else
                {
                    for (int i = 0; i < this.fpLongOrder.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.fpLongOrder.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);
                    }

                    this.fpLongOrder.ActiveSheet.ColumnHeader.Cells[1, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);

                    for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
                    {
                        this.fpLongOrder.ActiveSheet.Cells[i, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, false, false, false);

                        if (i == fpLongOrder.ActiveSheet.RowCount - 1)
                        {
                            this.fpLongOrder.ActiveSheet.Cells[i, (int)LongOrderColunms.DCConfirmNurse].Text = "  ";
                        }
                    }
                }
            }
            else
            {
                this.fpShortOrder.ActiveSheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty,
                                                   System.Drawing.Color.Empty, color,
                                                   FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White,
                                                   System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                                   System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                                   System.Drawing.Color.Empty, true, false, false, true, true);

                this.fpShortOrder.BorderStyle = border;

                if (border == BorderStyle.None)
                {
                    for (int i = 0; i < this.fpShortOrder.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.fpShortOrder.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, false, false, false, false);
                    }
                }
                else
                {
                    for (int i = 0; i < this.fpShortOrder.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.fpShortOrder.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);
                    }

                    this.fpShortOrder.ActiveSheet.ColumnHeader.Cells[1, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);


                    for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
                    {
                        this.fpShortOrder.ActiveSheet.Cells[i, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, false, false, false);

                        if (i == fpShortOrder.ActiveSheet.RowCount - 1)
                        {
                            this.fpShortOrder.ActiveSheet.Cells[i, (int)ShortOrderColunms.ExecOper].Text = " ";
                        }
                    }
                }
            }
        }

        private void SetTitleVisible(bool isLong, bool isVisible)
        {
            if (isLong)
            {
                this.ucLongOrderBillHeader.SetVisible(isVisible);
                if (this.OrderPrintType == EnumPrintType.DrawPaperContinue && isVisible)
                {
                    this.ucLongOrderBillHeader.SetVisible(false);
                }
                this.ucLongOrderBillHeader.SetValueVisible(isVisible);
                this.lblPageLong.Visible = isVisible;
                //lblLongSign.Visible = isVisible;
            }
            else
            {
                this.ucShortOrderBillHeader.SetVisible(isVisible);

                if (this.OrderPrintType == EnumPrintType.DrawPaperContinue && isVisible)
                {
                    this.ucShortOrderBillHeader.SetVisible(false);
                }
                this.ucShortOrderBillHeader.SetValueVisible(isVisible);
                this.lblPageShort.Visible = isVisible;
                //this.lblShortSign.Visible = isVisible;
            }
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="color"></param>
        /// <param name="vis"></param>
        private void SetVisibleForLong(Color color, bool vis, int rowIndex)
        {
            for (int i = 0; i < this.fpLongOrder.ActiveSheet.ColumnHeader.RowCount; i++)
            {
                this.fpLongOrder.ActiveSheet.ColumnHeader.Rows[i].ForeColor = color;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                #region 人员信息是否显示打印

                if (!this.isPrintReciptDoct)
                {
                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                }
                if (!this.isPrintConfirmNurse)
                {
                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                }
                if (!this.isPrintDCDoct)
                {
                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                }
                if (!this.isPrintDCConfirmNurse)
                {
                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                }
                #endregion

                this.fpLongOrder.ActiveSheet.Rows[i].BackColor = Color.White;

                if (color == Color.White)
                {
                    if (this.OrderPrintType != EnumPrintType.PrintWhenPatientOut)
                    {
                        //如果套打，则设置已打印的行不再显示
                        Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
                        if (oT != null)
                        {
                            if (i == rowIndex)
                            {
                                continue;
                            }
                            if (oT.GetFlag == "0")
                            {
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                            }
                            else if (oT.GetFlag == "1")
                            {
                                if (oT.DCOper.OperTime != DateTime.MinValue)
                                {
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");


                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.OnceDose, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Usage, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Freq, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Memo, "");

                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                                }
                                else
                                {
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");


                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.OnceDose, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Usage, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Freq, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Memo, "");

                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                    //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                    //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                                }
                            }
                            else if (oT.GetFlag == "2")
                            {
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");


                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.OnceDose, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Usage, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Freq, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.Memo, "");


                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="color"></param>
        /// <param name="vis"></param>
        private void SetVisibleForShort(Color color, bool vis, int rowIndex)
        {
            for (int i = 0; i < this.fpShortOrder.ActiveSheet.ColumnHeader.RowCount; i++)
            {
                this.fpShortOrder.ActiveSheet.ColumnHeader.Rows[i].ForeColor = color;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                #region 人员信息是否显示打印

                if (!this.isPrintReciptDoct)
                {
                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                }
                if (!this.isPrintConfirmNurse)
                {
                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                }
                if (!this.isPrintDCDoct)
                {
                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                }
                if (!this.isPrintDCConfirmNurse)
                {
                }
                #endregion
                this.fpShortOrder.ActiveSheet.Rows[i].BackColor = Color.White;

                if (color == Color.White)
                {
                    if (this.OrderPrintType != EnumPrintType.PrintWhenPatientOut)
                    {
                        //如果套打，则设置已打印的行不再显示
                        Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
                        if (oT != null)
                        {
                            if (i == rowIndex)
                            {
                                continue;
                            }

                            if (oT.GetFlag == "0")
                            {
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                            }
                            else if (oT.GetFlag == "1")
                            {
                                if (oT.DCOper.OperTime != DateTime.MinValue)
                                {
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");


                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.OnceDose, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Usage, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Freq, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Memo, "");

                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                    //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                    //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                                }
                                else
                                {
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");


                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.OnceDose, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Usage, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Freq, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Memo, "");

                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                                }
                            }
                            else if (oT.GetFlag == "2")
                            {
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");


                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.OnceDose, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Usage, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Freq, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Memo, "");

                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                            }
                            else
                            {
                            }
                        }
                    }
                }
                else {
                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                }
            }
        }

        #region 重置医嘱

        /// <summary>
        /// 重置当前患者医嘱单打印状态
        /// </summary>
        private void ReSet(EnumOrderType type)
        {
            if (this.myPatientInfo == null || this.myPatientInfo.ID == "")
            {
                return;
            }

            string message = "医嘱单";
            string orderType = "ALL";
            switch (type)
            {
                case EnumOrderType.Long:
                    message = "长期医嘱单";
                    orderType = "1";
                    break;
                case EnumOrderType.Short:
                    message = "临时医嘱单";
                    orderType = "0";
                    break;
                default:
                    break;
            }

            DialogResult rr = MessageBox.Show("即将取消所有" + message + "的打印状态，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.orderManager.ResetOrderPrint("-1", "-1", myPatientInfo.ID, orderType, "0") == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("重置失败!" + this.orderManager.Err);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("重置成功!");

            for (int sheetIndex = 0; sheetIndex < this.fpLongOrder.Sheets.Count; sheetIndex++)
            {
                this.fpLongOrder.Sheets[sheetIndex].RowCount = 0;
                this.fpLongOrder.Sheets[sheetIndex].RowCount = this.rowNum;
            }
            for (int sheetIndex = 0; sheetIndex < this.fpShortOrder.Sheets.Count; sheetIndex++)
            {
                this.fpShortOrder.Sheets[sheetIndex].RowCount = 0;
                this.fpShortOrder.Sheets[sheetIndex].RowCount = this.rowNum;
            }

            this.ucShortOrderBillHeader.SetPatientInfo(this.myPatientInfo);
            this.ucLongOrderBillHeader.SetPatientInfo(myPatientInfo);
            this.QueryPatientOrder();

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
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }

            string patientNo = this.ucQueryInpatientNo1.InpatientNo;
            Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.QueryPatientInfoByInpatientNO(patientNo);

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
            //打印
            if (e.ClickedItem == this.tbPrint)
            {
                this.Print();
            }
            //续打
            if (e.ClickedItem == this.tbRePrint)
            {
                this.PrintAgain();
            }
            //关闭窗体
            else if (e.ClickedItem == this.tbExit)
            {
                this.Close();
            }
            //else
            //{
            //}

            //这里不好使啊
            //重置长嘱
            else if (e.ClickedItem == this.ResetLong)
            {
                this.ReSet(EnumOrderType.Long);
            }
            //重置临嘱
            else if (e.ClickedItem == this.ResetShort)
            {
                this.ReSet(EnumOrderType.Short);
            }
            //重置全部医嘱
            else if (e.ClickedItem == this.ResetAll)
            {
                this.ReSet(EnumOrderType.All);
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
                Neusoft.HISFC.Models.RADT.PatientInfo temp = e.Node.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
                if (temp == null)
                {
                    temp = this.inPatientMgr.QueryPatientInfoByInpatientNO(((Neusoft.FrameWork.Models.NeuObject)e.Node.Tag).ID);
                }

                if (temp != null)
                {
                    this.ucShortOrderBillHeader.SetPatientInfo(temp);
                    this.ucLongOrderBillHeader.SetPatientInfo(temp);
                    this.myPatientInfo = temp;
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
            this.leftValue = Neusoft.FrameWork.Function.NConvert.ToInt32(this.nuLeft.Value);
            this.SetPrintValue("左边距", this.leftValue.ToString());
        }

        /// <summary>
        /// 保存打印行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuRowNum_ValueChanged(object sender, EventArgs e)
        {
            this.rowNum = Neusoft.FrameWork.Function.NConvert.ToInt32(this.nuRowNum.Value);
            this.SetPrintValue("行数", this.rowNum.ToString());
        }

        /// <summary>
        /// 上边距
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuTop_ValueChanged(object sender, EventArgs e)
        {
            this.topValue = Neusoft.FrameWork.Function.NConvert.ToInt32(this.nuTop.Value);
            this.SetPrintValue("上边距", this.topValue.ToString());
        }

        /// <summary>
        /// 选择打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPrinter_SelectedIndexChanged(object sender, EventArgs e)
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

        /// <summary>
        /// 设置打印机
        /// </summary>
        private void SetPrintValue(string nodeName, string nodeValue)
        {
            Neusoft.FrameWork.Xml.XML xml = new Neusoft.FrameWork.Xml.XML();
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
                this.lblPageLong.Text = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag) + 1).ToString() + "页";

                this.ucLongOrderBillHeader.SetChangedInfo(GetFirstOrder(true));

                //SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
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
                this.lblPageShort.Text = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag) + 1).ToString() + "页";

                this.ucShortOrderBillHeader.SetChangedInfo(GetFirstOrder(false));

                //SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
            }
        }

        /// <summary>
        /// 获得加急状态
        /// </summary>
        /// <param name="isEmr"></param>
        /// <returns></returns>
        private string GetEmergencyTip(bool isEmr)
        {
            if (isEmr)
            {
                return "【急】";
            }
            else
            {
                return "";
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

                System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                printDateItem.Text = "只补打该条长期医嘱停止时间";
                printDateItem.Click += new EventHandler(printDateItem_Click);

                System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                splitMenuItem.Text = "从该条医嘱往后另起一页";
                splitMenuItem.Click += new EventHandler(splitMenuItem_Click);

                this.popMenu.MenuItems.Add(printMenuItem);
                this.popMenu.MenuItems.Add(printDateItem);
                this.popMenu.MenuItems.Add(splitMenuItem);
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

                System.Windows.Forms.MenuItem splitShortMenuItem = new MenuItem();
                splitShortMenuItem.Text = "从该条医嘱往后另起一页";
                splitShortMenuItem.Click += new EventHandler(splitShortMenuItem_Click);

                this.popMenu.MenuItems.Add(printMenuItem);
                this.popMenu.MenuItems.Add(splitShortMenuItem);
                this.popMenu.Show(this.fpShortOrder, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 临时医嘱分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitShortMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpShortAfter(this.fpShortOrder.ActiveSheetIndex, this.fpShortOrder.ActiveSheet.ActiveRowIndex);
        }

        /// <summary>
        /// 长期医嘱分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpLongAfter(this.fpLongOrder.ActiveSheetIndex, this.fpLongOrder.ActiveSheet.ActiveRowIndex);
        }

        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printMenuItem_Click(object sender, EventArgs e)
        {
            this.PrintSingleItem();
        }

        /// <summary>
        /// 只补打该条长期医嘱停止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDateItem_Click(object sender, EventArgs e)
        {
            PrintSingleDate();
        }
        #endregion

        #endregion

        #region IPrintOrder 成员

        void Neusoft.HISFC.BizProcess.Interface.IPrintOrder.Print()
        {
            this.ShowDialog();
        }

        public void SetPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
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
        [Neusoft.FrameWork.Public.Description("白纸续打")]
        WhitePaperContinue = 0,

        /// <summary>
        /// 印刷续打
        /// </summary>
        [Neusoft.FrameWork.Public.Description("印刷续打")]
        DrawPaperContinue = 1,

        /// <summary>
        /// 出院打印
        /// </summary>
        [Neusoft.FrameWork.Public.Description("出院打印")]
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
                Neusoft.HISFC.Models.Order.Inpatient.Order order1 = x as Neusoft.HISFC.Models.Order.Inpatient.Order;
                Neusoft.HISFC.Models.Order.Inpatient.Order order2 = y as Neusoft.HISFC.Models.Order.Inpatient.Order;
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
    /// 长期医嘱单列明
    /// </summary>
    public enum LongOrderColunms
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        [Neusoft.FrameWork.Public.Description("日期")]
        BeginDate = 0,

        /// <summary>
        /// 开始时间
        /// </summary>
        [Neusoft.FrameWork.Public.Description("时间")]
        BeginTime,

        /// <summary>
        /// 开立医生
        /// </summary>
        [Neusoft.FrameWork.Public.Description("开方医生")]
        RecipeDoct,

        /// <summary>
        /// 审核护士
        /// </summary>
        [Neusoft.FrameWork.Public.Description("审核护士")]
        ConfirmNurse,

        /// <summary>
        /// 组标记
        /// </summary>
        [Neusoft.FrameWork.Public.Description("组")]
        CombFlag,

        /// <summary>
        /// 项目名称
        /// </summary>
        [Neusoft.FrameWork.Public.Description("长期医嘱")]
        ItemName,

        /// <summary>
        /// 每次量
        /// </summary>
        [Neusoft.FrameWork.Public.Description("每次量")]
        OnceDose,

        /// <summary>
        /// 频次
        /// </summary>
        [Neusoft.FrameWork.Public.Description("频次")]
        Freq,

        /// <summary>
        /// 用法
        /// </summary>
        [Neusoft.FrameWork.Public.Description("用法")]
        Usage,

        /// <summary>
        /// 备注
        /// </summary>
        [Neusoft.FrameWork.Public.Description("备注")]
        Memo,

        /// <summary>
        /// 停止日期
        /// </summary>
        [Neusoft.FrameWork.Public.Description("日期")]
        DCDate,

        /// <summary>
        /// 停止时间
        /// </summary>
        [Neusoft.FrameWork.Public.Description("时间")]
        DCTime,

        /// <summary>
        /// 停止医生
        /// </summary>
        [Neusoft.FrameWork.Public.Description("停止医生")]
        DCDoct,

        /// <summary>
        /// 停止审核护士
        /// </summary>
        [Neusoft.FrameWork.Public.Description("审核护士")]
        DCConfirmNurse,

        /// <summary>
        /// 执行护士
        /// </summary>
        [Neusoft.FrameWork.Public.Description("执行护士")]
        ExecNurse,

        /// <summary>
        /// 组号
        /// </summary>
        [Neusoft.FrameWork.Public.Description("组号")]
        CombNO,

        /// <summary>
        /// 列数量
        /// </summary>
        [Neusoft.FrameWork.Public.Description("列数")]
        Max
    }

    /// <summary>
    /// 临时医嘱单列明
    /// </summary>
    public enum ShortOrderColunms
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        BeginDate,

        /// <summary>
        /// 开始时间
        /// </summary>
        BeginTime,

        /// <summary>
        /// 组标记
        /// </summary>
        CombFlag,

        /// <summary>
        /// 项目名称
        /// </summary>
        ItemName,


        /// <summary>
        /// 每次量
        /// </summary>
        [Neusoft.FrameWork.Public.Description("每次量")]
        OnceDose,

        /// <summary>
        /// 频次
        /// </summary>
        [Neusoft.FrameWork.Public.Description("频次")]
        Freq,

        /// <summary>
        /// 用法
        /// </summary>
        [Neusoft.FrameWork.Public.Description("用法")]
        Usage,

        /// <summary>
        /// 总量
        /// </summary>
        [Neusoft.FrameWork.Public.Description("总量")]
        Qty,


        /// <summary>
        /// 备注
        /// </summary>
        [Neusoft.FrameWork.Public.Description("备注")]
        Memo,

        /// <summary>
        /// 开立医生
        /// </summary>
        RecipeDoct,

        /// <summary>
        /// 审核护士
        /// </summary>
        ConfirmNurse,

        /// <summary>
        /// 审核日期
        /// </summary>
        ConfirmDate,

        /// <summary>
        /// 审核时间
        /// </summary>
        ConfirmTime,

        /// <summary>
        /// 停止标记
        /// </summary>
        DCFlage,

        /// <summary>
        /// 停止医生
        /// </summary>
        DCDoct,
        
        /// <summary>
        /// 执行者
        /// </summary>
        ExecOper,

        /// <summary>
        /// 组号
        /// </summary>
        CombNO,

        /// <summary>
        /// 列数量
        /// </summary>
        Max
    }
}