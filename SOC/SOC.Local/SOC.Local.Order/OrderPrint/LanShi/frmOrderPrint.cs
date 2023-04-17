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

namespace Neusoft.SOC.Local.InOrder.OrderPrint.LanShi
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

        Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        Neusoft.HISFC.BizLogic.Pharmacy.Item myItem = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
        Neusoft.HISFC.BizLogic.RADT.InPatient inPatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.BizLogic.Manager.Person PersonManger = new Neusoft.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 药品基本信息帮助类
        /// </summary>
        Neusoft.FrameWork.Public.ObjectHelper phaItemHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 右键菜单
        /// </summary>
        System.Windows.Forms.ContextMenu menu = new ContextMenu();

        Function funMgr = new Function();

        /// <summary>
        /// 默认重整时间
        /// </summary>
        private DateTime reformDate = new DateTime(2000, 1, 1);
        ArrayList alLong = new ArrayList();
        ArrayList alShort = new ArrayList();
        
        /// <summary>
        /// 重整医嘱列表
        /// </summary>
        ArrayList alRecord = new ArrayList();

        /// <summary>
        /// 存储打印不同类型时使用的打印机型号
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
        private string printType = "出院打印";

        /// <summary>
        /// 打印方式
        /// </summary>
        private PrintType OrderPrintType = PrintType.PrintWhenPatientOut;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo pInfo;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public Neusoft.HISFC.Models.RADT.PatientInfo PInfo
        {
            get
            {
                return this.pInfo;
            }
            set
            {
                this.pInfo = value;

                if (this.pInfo != null && !string.IsNullOrEmpty(this.pInfo.ID))
                {
                    this.SetTreeView();
                }
            }
        }

        /// <summary>
        /// 控制参数管理
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 打印时是否显示通用名，否则显示药品名
        /// </summary>
        private bool isDisplayRegularName = true;

        #endregion

        #region 方法

        #region 初始化

        /// <summary>
        /// LOAD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderPrint_Load(object sender, System.EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            this.ucOrderBillHeader1.Header = "临  时  医  嘱  单";
            this.ucOrderBillHeader2.Header = "长  期  医  嘱  单";
            ArrayList alPha = new ArrayList(this.myItem.QueryItemAvailableList());
            this.phaItemHelper.ArrayObject = alPha;

            InitOrderPrint();

            this.InitLongSheet(this.neuSpread1_Sheet1);
            this.InitShortSheet(this.neuSpread2_Sheet1);

            this.tbExit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbRePrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C重打);
            this.tbQuery.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询);

            this.tbReset.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q取消);
            this.tbReset.Visible = true;
            this.tbReset.Enabled = true;
            this.tbSetting.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S设置);
            this.tbSetting.Visible = false;

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);
            //this.nuRowNum.ValueChanged+=new EventHandler(nuRowNum_ValueChanged);

            this.neuSpread1.ActiveSheetChanged += new EventHandler(fpSpread2_ActiveSheetChanged);
            //this.neuSpread1.MouseUp += new MouseEventHandler(fpSpread2_MouseUp);
            this.neuSpread2.ActiveSheetChanged += new EventHandler(neuSpread2_ActiveSheetChanged);

            this.tbReset.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

            QueryPatientOrder();
        }

        /// <summary>
        /// 初始化基础参数
        /// </summary>
        private void InitOrderPrint()
        {
            //华南医疗参数：打印时是否显示通用名，否则显示药品名
            try
            {
                this.isDisplayRegularName = controlIntegrate.GetControlParam<bool>("HNZY01", true, true);
            }
            catch
            {
                this.isDisplayRegularName = true;
            }

            //获取系统打印机列表
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                //取打印机名
                string name = System.Text.RegularExpressions.Regex.Replace(PrinterSettings.InstalledPrinters[i], @"在(\s|\S)*上", "").Replace("自动", "");
                this.tbPrinter.Items.Add(name);
            }

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);

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

                node = file.SelectSingleNode("ORDERPRINT/打印方式");
                if (node != null)
                {
                    this.printType = node.InnerText;
                }
                this.cmbPrintType.Text = this.printType;

                if (this.printType == "白纸套打")
                {
                    this.OrderPrintType = PrintType.WhitePaperContinue;
                }
                else if (this.printType == "印刷套打")
                {
                    this.OrderPrintType = PrintType.DrawPaperContinue;
                }
                else
                {
                    this.OrderPrintType = PrintType.PrintWhenPatientOut;
                }
            }

            for (int i = 0; i < this.tbPrinter.Items.Count; i++)
            {
                if (this.tbPrinter.Items[i].ToString().Contains(this.printerName))
                {
                    this.tbPrinter.SelectedIndex = i;
                    break;
                }
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
            sheet.RowCount = this.rowNum;
            sheet.RowHeader.ColumnCount = 0;
            sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, false, true, true);

            for (int i = 0; i < this.rowNum; i++)
            {
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells.Get(i, j).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
            }

            #region 标题
            sheet.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 0).Text = " 起   始 ";
            //sheet.ColumnHeader.Columns.Get(1).Width = 50;
            sheet.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).Text = "组";
            sheet.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 3).Text = " 医 嘱 内 容 ";
            sheet.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 4).Text = "数量";
            sheet.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 5).Text = "单位";
            sheet.ColumnHeader.Cells.Get(0, 6).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 6).Text = "医生";

            sheet.ColumnHeader.Cells.Get(0, 7).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 7).Text = "护士";

            sheet.ColumnHeader.Cells.Get(0, 8).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 8).Text = " 停   止 ";

            sheet.ColumnHeader.Cells.Get(0, 10).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 10).Text = "医生";
            sheet.ColumnHeader.Cells.Get(0, 11).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 11).Text = "护士";

            sheet.ColumnHeader.Cells.Get(1, 0).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, 1).Text = "时间";

            sheet.ColumnHeader.Cells.Get(1, 8).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, 9).Text = "时间";
            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;
            sheet.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            sheet.Columns.Get(0).Label = "日期";
            sheet.Columns.Get(0).Width = 64F;
            sheet.Columns.Get(1).Label = "时间";

            #endregion

            sheet.Columns.Get((Int32)LongOrderColunms.Qty).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;

            textCellType1.WordWrap = true;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).CellType = textCellType1;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Columns.Get((Int32)LongOrderColunms.MoDate).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.MoTime).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.CombFlag).Width = 17F;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).Width = 250F;
            sheet.Columns.Get((Int32)LongOrderColunms.Qty).Width = 35F;
            sheet.Columns.Get((Int32)LongOrderColunms.Unit).Width = 35F;
            sheet.Columns.Get((Int32)LongOrderColunms.RecipeDoct).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.ConfirmNurse).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDate).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCTime).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDoct).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCConfirmNurse).Width = 55F;

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
            sheet.RowCount = this.rowNum;
            sheet.RowHeader.ColumnCount = 0;
            sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, false, true, true);

            for (int i = 0; i < this.rowNum; i++)
            {
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells.Get(i, j).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
            }

            #region 标题

            sheet.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 0).Text = " 开  立 ";
            sheet.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).Text = "组";
            sheet.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 3).Text = "医嘱内容";

            sheet.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 4).Text = "数量";

            sheet.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 5).Text = "单位";

            sheet.ColumnHeader.Cells.Get(0, 6).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 6).Text = "医生";

            sheet.ColumnHeader.Cells.Get(0, 7).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 7).Text = "护士";

            sheet.ColumnHeader.Cells.Get(0, 8).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 8).Text = "执行时间";

            sheet.ColumnHeader.Cells.Get(0, 10).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 10).Text = "取消";

            sheet.ColumnHeader.Cells.Get(0, 11).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 11).Text = "医生";

            sheet.ColumnHeader.Cells.Get(1, 0).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, 1).Text = "时间";
            sheet.ColumnHeader.Cells.Get(1, 8).Text = "日期";
            sheet.ColumnHeader.Cells.Get(1, 9).Text = "时间";

            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;

            #endregion

            sheet.Columns.Get((Int32)ShortOrderColunms.MoTime).Label = "时间";

            textCellType2.WordWrap = true;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).CellType = textCellType2;

            //sheet.Columns.Get((Int32)ShortOrderColunms.Unit).Locked = false;

            //sheet.Columns.Get(6).Label = "日期";

            //sheet.Columns.Get(7).Label = "时间";

            sheet.Columns.Get((Int32)ShortOrderColunms.CombNO).Visible = false;
            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            sheet.Columns.Get((Int32)ShortOrderColunms.MoDate).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.MoTime).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.CombFlag).Width = 17F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).Width = 260F;
            sheet.Columns.Get((Int32)ShortOrderColunms.Qty).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.Unit).Width = 45F;
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
        }

        #endregion

        #region 查询显示

        /// <summary>
        /// 查询患者医嘱信息
        /// </summary>
        private void QueryPatientOrder()
        {
            if (this.pInfo == null || string.IsNullOrEmpty(this.pInfo.ID))
            {
                return;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询显示医嘱信息,请稍候......");
            Application.DoEvents();

            ArrayList alAll = new ArrayList();

            alAll = this.orderManager.QueryPrnOrder(this.pInfo.ID);

            alLong.Clear();
            alShort.Clear();

            foreach (object obj in alAll)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order order = obj as Neusoft.HISFC.Models.Order.Inpatient.Order;
                order.Nurse.Name = PersonManger.GetPersonByID(order.Nurse.ID).Name;
                order.Doctor.Name = order.ReciptDoctor.Name;
                if (order.DCNurse.ID != null && order.DCNurse.ID != "")
                {
                    order.DCNurse.Name = PersonManger.GetPersonByID(order.DCNurse.ID).Name;
                }

                order.Item.Name += funMgr.TransHypotest(order.Item.ID, order.HypoTest);

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

            for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
            {
                for (int j = 0; j < this.neuSpread1.Sheets[i].Rows.Count; j++)
                {
                    this.neuSpread1.Sheets[i].Rows[j].BackColor = Color.White;
                }
            }

            for (int i = 0; i < this.neuSpread2.Sheets.Count; i++)
            {
                for (int j = 0; j < this.neuSpread2.Sheets[i].Rows.Count; j++)
                {
                    this.neuSpread2.Sheets[i].Rows[j].BackColor = Color.White;
                }
            }

            Neusoft.FrameWork.Management.ExtendParam  myInpatient = new Neusoft.FrameWork.Management.ExtendParam();

            this.alRecord = myInpatient.GetComExtInfoListByPatient(Neusoft.HISFC.Models.Base.EnumExtendClass.PATIENT, pInfo.ID);

            this.Clear();

            this.AddObjectToFpLong(alLong);

            this.AddObjectToFpShort(alShort);

            this.SetPatientInfo(this.pInfo);

            this.lblPageLong.Visible = true;
            this.lblPageShort.Visible = true;

            this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));
            this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 显示患者信息
        /// </summary>
        /// <param name="info"></param>
        private void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo info)
        {
            if (info == null)
            {
                info = new Neusoft.HISFC.Models.RADT.PatientInfo();
            }

            this.ucOrderBillHeader1.PInfo = info;
            this.ucOrderBillHeader2.PInfo = info;
            this.pInfo = info;
        }

        /// <summary>
        /// 设置树形显示
        /// </summary>
        private void SetTreeView()
        {
            if (this.pInfo == null)
            {
                return;
            }

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }

            TreeNode root = new TreeNode();

            root.Text = "住院信息:" + "[" + this.pInfo.Name + "]" + "[" + this.pInfo.PID.PatientNO + "]";

            this.treeView1.Nodes.Add(root);

            root.ImageIndex = 0;

            TreeNode node = new TreeNode();

            node.Text = "[" + this.pInfo.PVisit.InTime.ToShortDateString() + "][" + this.pInfo.PVisit.PatientLocation.Dept.Name + "]";

            node.Tag = this.pInfo;

            node.ImageIndex = 1;

            root.Nodes.Add(node);

            this.treeView1.ExpandAll();

            //this.treeView1.SelectedNode = node;
            //this.treeView1_AfterSelect(new object(), new TreeViewEventArgs(node));
        }

        #region 清空

        /// <summary>
        /// 清空所有医嘱单内容
        /// </summary>
        private void Clear()
        {
            #region 长嘱
            this.neuSpread1_Sheet1.Columns.Count = (Int32)LongOrderColunms.Max;
            #region 清空数据
            for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
            {
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.MoDate, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.MoTime, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.CombFlag, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.ItemName, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.Qty, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.Unit, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.RecipeDoct, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.ConfirmNurse, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCDate, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCTime, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCDoct, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCConfirmNurse, "");
                this.neuSpread1_Sheet1.SetValue(j, (Int32)LongOrderColunms.CombNO, "");
            }
            #endregion

            #region 保留一个Sheet
            if (this.neuSpread1.Sheets.Count > 1)
            {
                for (int j = this.neuSpread1.Sheets.Count - 1; j > 0; j--)
                {
                    this.neuSpread1.Sheets.RemoveAt(j);
                }
            }
            #endregion

            this.InitLongSheet(this.neuSpread1_Sheet1);

            #endregion

            #region 临嘱
            this.neuSpread2_Sheet1.Columns.Count = (Int32)ShortOrderColunms.Max;

            #region 清空数据
            for (int j = 0; j < this.neuSpread2_Sheet1.Rows.Count; j++)
            {
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.MoDate, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.MoTime, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ItemName, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.Qty, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.Unit, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.RecipeDoct, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ConfirmNurse, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ConfirmDate, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ConfirmTime, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.DCFlage, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.DCDoct, "");
                this.neuSpread2_Sheet1.SetValue(j, (Int32)ShortOrderColunms.CombNO, "");

            }
            #endregion

            #region 保留一个Sheet
            if (this.neuSpread2.Sheets.Count > 1)
            {
                for (int j = this.neuSpread2.Sheets.Count - 1; j > 0; j--)
                {
                    this.neuSpread2.Sheets.RemoveAt(j);
                }
            }
            #endregion

            this.InitShortSheet(this.neuSpread2_Sheet1);

            #endregion
        }
        #endregion

        /// <summary>
        /// 添加到长嘱Fp
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectToFpLong(ArrayList al)
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

            bool reformPrint = false;

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

                        this.neuSpread1_Sheet1.Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());


                        if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //是否显示通用名，否则
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Usage.Name + " " + order.Memo);
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Usage.Name + " " + order.Memo);
                                }
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Usage.Name + " " + order.Memo);
                            }

                            this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.DoseOnce.ToString());
                            this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                        }
                        else
                        {
                            //???目的何在？
                            if (order.Item.SysClass.ID.ToString() != "UN" && order.Item.SysClass.ID.ToString() != "MF")
                            {
                                this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Memo);
                            }
                            else
                            {
                                if (order.Item.Name.IndexOf("护理") < 0 && order.Item.Name.IndexOf("食") < 0)
                                {
                                    this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Memo);
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Memo);
                                }
                            }

                            if (order.Item.ID != "999")
                            {
                                this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.Qty.ToString());
                                this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, order.Item.PriceUnit);
                            }
                        }

                        this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.RecipeDoct, order.Doctor.Name);
                        this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.DCOper.OperTime > this.reformDate)
                        {
                            this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCDate, order.DCOper.OperTime.Month.ToString() + "-" + order.DCOper.OperTime.Day.ToString());
                            this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCTime, order.DCOper.OperTime.ToShortTimeString());
                            this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCDoct, order.DCOper.Name);
                            this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCConfirmNurse, order.DCNurse.Name);
                        }
                        this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.CombNO, order.Combo.ID);

                        this.neuSpread1_Sheet1.Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Function.DrawCombo1(this.neuSpread1_Sheet1, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);

                    this.neuSpread1_Sheet1.Tag = pageNo;
                    this.neuSpread1_Sheet1.SheetName = "第" + (pageNo + 1).ToString() + "页";
                }
                #endregion

                #region 其他页
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                    this.InitLongSheet(sheet);
                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);

                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {
                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "【" + order.Item.Name + "】的实际打印行号为" + (order.RowNo+1).ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[order.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());

                        if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //是否显示通用名，否则
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Usage.Name + " " + order.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Usage.Name + " " + order.Memo);
                                }
                            }
                            else
                            {
                                sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Usage.Name + " " + order.Memo);
                            }

                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.DoseOnce.ToString());
                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                        }
                        else
                        {
                            if (order.Item.SysClass.ID.ToString() != "UN" && order.Item.SysClass.ID.ToString() != "MF")
                            {
                                sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Memo);
                            }
                            else
                            {
                                if (order.Item.Name.IndexOf("护理") < 0 && order.Item.Name.IndexOf("食") < 0)
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Memo);
                                }
                            } 
                            
                            if (order.Item.ID != "999")
                            {
                                this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.Qty.ToString());
                                this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, order.Item.PriceUnit);
                            }
                        }

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

                    Function.DrawCombo1(sheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);

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

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread1.Sheets[MaxPageNo];

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

                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());


                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                        }

                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("护理") < 0 && oTemp.Item.Name.IndexOf("食") < 0)
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo);
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

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

                    Function.DrawCombo1(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                         //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                        }

                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("护理") < 0 && oTemp.Item.Name.IndexOf("食") < 0)
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo);
                            }
                        } 
                        
                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

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

                    Function.DrawCombo1(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
            }

            //需要处理
            if (this.alRecord.Count >= 0)
            {
                //处理重整一周后的自动分页
                for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
                {
                    for (int j = 0; j < this.neuSpread1.Sheets[i].Rows.Count; j++)
                    {
                        Neusoft.HISFC.Models.Order.Inpatient.Order order = this.neuSpread1.Sheets[i].Rows[j].Tag as
                            Neusoft.HISFC.Models.Order.Inpatient.Order;

                        if (order == null)
                            continue;

                        if (order.PageNo >= 0)
                            continue;

                        foreach(Neusoft.HISFC.Models.Base.ExtendInfo ext in alRecord)
                        {
                            if (order.MOTime >= Neusoft.FrameWork.Function.NConvert.ToDateTime(ext.PropertyCode))
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
        /// 转科分页后添加到Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        private void AddObjectToFpLongAfter(int sheet, int row)
        {
            DateTime now = this.orderManager.GetDateTimeFromSysDateTime().Date;//当前系统时间

            #region 定义变量

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            if (this.neuSpread1.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("所选项目为空！");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.neuSpread1.Sheets[sheet].Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

            MessageBox.Show("重整医嘱后分页，请注意！", "提示");

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "已经打印过，不能另起一页！");
                return;
            }

            #endregion

            #region 获取剩余数据

            for (int i = row; i < this.neuSpread1.Sheets[sheet].Rows.Count; i++)
            {
                if (this.neuSpread1.Sheets[sheet].Rows[i].Tag != null)
                {
                    alPageNull.Add(this.neuSpread1.Sheets[sheet].Rows[i].Tag);
                }
            }

            for (int i = sheet + 1; i < this.neuSpread1.Sheets.Count; i++)
            {
                for (int j = 0; j < this.neuSpread1.Sheets[i].Rows.Count; j++)
                {
                    if (this.neuSpread1.Sheets[i].Rows[j].Tag != null)
                    {
                        alPageNull.Add(this.neuSpread1.Sheets[i].Rows[j].Tag);
                    }
                }
            }

            #endregion

            #region 清空数据

            for (int j = row; j < this.neuSpread1.Sheets[sheet].Rows.Count; j++)
            {
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.MoDate, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.MoTime, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.CombFlag, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ItemName, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Qty, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Unit, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.RecipeDoct, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ConfirmNurse, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, 8, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, 9, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, 10, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, 11, "");
                this.neuSpread1.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.CombNO, "");
                this.neuSpread1.Sheets[sheet].Rows[j].Tag = null;
            }

            #endregion

            #region 保留一个Sheet

            if (this.neuSpread1.Sheets.Count > 1)
            {
                for (int j = this.neuSpread1.Sheets.Count - 1; j > sheet; j--)
                {
                    this.neuSpread1.Sheets.RemoveAt(j);
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

            this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread1.Sheets[MaxPageNo];

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

                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                         //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);

                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + "  " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + "  " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Usage.Name + " " + oTemp.Memo);
                        }

                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("护理") < 0 && oTemp.Item.Name.IndexOf("食") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo);
                            }
                        }
                    }

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

                    Function.DrawCombo1(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName);
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name);
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name);
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);
                        }
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("护理") < 0 && oTemp.Item.Name.IndexOf("食") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo);
                            }
                        }
                    }

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

                    Function.DrawCombo1(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
            }

            #endregion
        }

        /// <summary>
        /// 添加到Fp
        /// </summary>
        /// <param name="al"></param>
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

                        this.neuSpread2_Sheet1.Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());

                        if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //是否显示通用名，否则
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (order.OrderType.ID == "CD")
                                    {
                                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + (order.Frequency.ID + " ") + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + order.Memo);

                                    }
                                    //else if (order.OrderType.ID == "BL")
                                    //{
                                    //    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + /*"(补录医嘱)" +*/ " " + order.Memo);
                                    //}
                                    else
                                    {
                                        //o.DoseOnce.ToString()+(o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit+" "
                                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + (order.Frequency.ID + " ") + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Memo);
                                    }
                                }
                                else
                                {
                                    if (order.OrderType.ID == "CD")
                                    {
                                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + order.Memo);
                                    }
                                    //else if (order.OrderType.ID == "BL")
                                    //{
                                    //    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit +/* "(补录医嘱)" +*/ " " + order.Memo);
                                    //}
                                    else
                                    {
                                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + " " + order.Usage.Name + " " + (order.Frequency.ID + " ") + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Memo);
                                    }
                                }
                            }
                            else
                            {
                                if (order.OrderType.ID == "CD")
                                {
                                    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + order.Memo);
                                }
                                //else if (order.OrderType.ID == "BL")
                                //{
                                //    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + /*"(补录医嘱)" + */" " + order.Memo);
                                //}
                                else
                                {
                                    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + " " + order.Usage.Name + " " + (order.Frequency.ID + " ") + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Memo);
                                }
                            }

                            this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.Qty, order.Qty.ToString());
                            this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.Unit, order.Unit);
                        }
                        else
                        {
                            if (order.Item.MinFee.ID == "037" || order.Item.MinFee.ID == "038" || order.Item.Name.IndexOf("备皮") >= 0)
                            {
                                this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                            }
                            else
                            {
                                this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                            }

                            //非药品也显示数量和单位
                            if (order.Item.ID != "999")
                            {
                                this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.Qty, order.Qty.ToString());
                                this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.Unit, order.Item.PriceUnit);
                            }
                        }

                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.RecipeDoct, order.Doctor.Name);
                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.ConfirmTime != DateTime.MinValue)
                        {
                            this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmDate, order.ConfirmTime.Month.ToString() + "-" + order.ConfirmTime.Day.ToString());
                            this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmTime, order.ConfirmTime.ToShortTimeString());
                        }

                        if (order.DCOper.OperTime != DateTime.MinValue)
                        {
                            this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.DCFlage, "DC");
                            this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.DCDoct, order.DCOper.Name);
                        }


                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.CombNO, order.Combo.ID);
                        this.neuSpread2_Sheet1.Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Function.DrawCombo1(this.neuSpread2_Sheet1, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);

                    this.neuSpread2_Sheet1.Tag = pageNo;
                    this.neuSpread2_Sheet1.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(sheet);

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order o in alTemp)
                    {
                        if (o.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(o.OrderType.Name + "【" + o.Item.Name + "】的实际打印行号为" + o.RowNo.ToString() + "，大于设置的每页最大行数" + this.rowNum.ToString() + ",请联系信息科！", "警告");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[o.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.MoDate, o.MOTime.Month.ToString() + "-" + o.MOTime.Day.ToString());
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.MoTime, o.MOTime.ToShortTimeString());

                        if (o.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(o.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //是否显示通用名，否则
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (o.OrderType.ID == "CD")
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + o.Usage.Name + " " + (o.Frequency.ID + " ") + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + o.Memo);
                                    }
                                    else
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + o.Usage.Name + " " + (o.Frequency.ID + " ") + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Memo);
                                    }
                                }
                                else
                                {
                                    if (o.OrderType.ID == "CD")
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + (o.Item.ID == "999" ? "" : (o.Frequency.ID + " ")) + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + o.Memo);
                                    }
                                    else
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + (o.Frequency.ID + " ") + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Memo);
                                    }
                                }
                            }
                            else
                            {
                                if (o.OrderType.ID == "CD")
                                {
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + (o.Item.ID == "999" ? "" : (o.Frequency.ID + " ")) + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + o.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + (o.Frequency.ID + " ") + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Memo);
                                }
                            }

                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Qty, o.Qty.ToString());
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Unit, o.Unit);
                        }
                        else
                        {
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency));
                            
                            //非药品也显示数量和单位
                            if (o.Item.ID != "999")
                            {
                                this.neuSpread2_Sheet1.SetValue(o.RowNo, (Int32)ShortOrderColunms.Qty, o.Qty.ToString());
                                this.neuSpread2_Sheet1.SetValue(o.RowNo, (Int32)ShortOrderColunms.Unit, o.Item.PriceUnit);
                            }
                        }

                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.RecipeDoct, o.Doctor.Name);
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, o.Nurse.Name);

                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmDate, o.ConfirmTime.Month.ToString() + "-" + o.ConfirmTime.Day.ToString());
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmTime, o.ConfirmTime.ToShortTimeString());
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmDate, o.ExecOper.Name);
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

                    Function.DrawCombo1(sheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);

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

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread2.Sheets[MaxPageNo];


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

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Unit);
                    }
                    else
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));

                        //非药品也显示数量和单位
                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.Month.ToString() + "-" + oTemp.ConfirmTime.Day.ToString());
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ConfirmTime.ToShortTimeString());
                    }
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Unit);

                    }
                    else
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.Month.ToString() + "-" + oTemp.ConfirmTime.Day.ToString());
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ConfirmTime.ToShortTimeString());
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.DCFlage, "DC");
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
            }

            #endregion

            DealShortOrderCrossPage();

        }

        /// <summary>
        /// 转科分页后添加到Fp
        /// </summary>
        private void AddObjectToFpShortAfter()
        {

            #region 定义变量

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag) + 1;
            int MaxRowNo = -1;

            int row = this.neuSpread2.ActiveSheet.ActiveRowIndex;

            if (this.neuSpread2.ActiveSheet.Rows[row].Tag == null)
            {
                MessageBox.Show("所选项目为空！");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.neuSpread2.ActiveSheet.Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            for (int i = this.neuSpread2.ActiveSheet.ActiveRowIndex; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    alPageNull.Add(this.neuSpread2.ActiveSheet.Rows[i].Tag);
                }
            }

            for (int i = this.neuSpread2.ActiveSheetIndex + 1; i < this.neuSpread2.Sheets.Count; i++)
            {
                for (int j = 0; j < this.neuSpread2.Sheets[i].Rows.Count; j++)
                {
                    if (this.neuSpread2.Sheets[i].Rows[j].Tag != null)
                    {
                        alPageNull.Add(this.neuSpread2.Sheets[i].Rows[j].Tag);
                    }
                }
            }

            #endregion

            #region 清空数据

            for (int j = row; j < this.neuSpread2.ActiveSheet.Rows.Count; j++)
            {
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.MoDate, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.MoTime, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ItemName, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Qty, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Unit, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.RecipeDoct, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ConfirmNurse, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ConfirmDate, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.ConfirmTime, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.DCFlage, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.DCDoct, "");
                this.neuSpread2.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.CombNO, "");
                this.neuSpread2.ActiveSheet.Rows[j].Tag = null;
            }

            #endregion

            #region 保留一个Sheet

            if (this.neuSpread2.Sheets.Count > 1)
            {
                for (int j = this.neuSpread2.Sheets.Count - 1; j > this.neuSpread2.ActiveSheetIndex; j--)
                {
                    this.neuSpread2.Sheets.RemoveAt(j);
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

            this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread2.Sheets[MaxPageNo];


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
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(sheet);

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "第" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "页";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(补录医嘱)" + */" " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(补录医嘱)" +*/ " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(补录医嘱)" + */" " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("备皮") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(补录医嘱)" +*/ " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }
                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);


                    if (oTemp.ExecOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                        // activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.Name);
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                        // activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.Name);
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCFlage, "DC");
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //是否显示通用名，否则
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit +/* "(补录医嘱)" + */" " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit +/* "(补录医嘱)" + */" " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(补录医嘱)" +*/ " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit);

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("备皮") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit +/* "(补录医嘱)" + */" " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }


                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.Doctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);


                    if (oTemp.ExecOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.Name);
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCFlage, "DC");
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
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
            p.SetPageSize(size);
            p.IsCanCancel = false;

            string errText = "";
            frmNotice frmNotice = new frmNotice();

            int pag = 0;

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

                    if (!this.GetIsPrintAgainForLong())
                    {
                        DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (r == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (this.OrderPrintType == PrintType.DrawPaperContinue)
                            {
                                //设置标题的提示文字的可见性
                                this.ucOrderBillHeader2.SetVisible(false);
                                //设置列头的可见性
                                this.SetVisibleForLong(Color.White, false);
                                //设置显示的格式
                                SetStyleForFp(true, Color.White, BorderStyle.None);

                            }
                            else
                            {
                                //设置列头的可见性
                                this.SetVisibleForLong(Color.Black, false);
                                SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                            }

                            //设置重打显示的内容
                            this.SetRePrintContentsForLong();

                            //设置显示患者改变信息
                            this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                            this.neuPanel2.Dock = DockStyle.None;

                            this.neuPanel2.Location = new Point(this.neuSpread1.Location.X, this.neuSpread1.Location.Y + (Int32)(this.neuSpread1.ActiveSheet.RowHeader.Rows[0].Height + this.neuSpread1.ActiveSheet.RowHeader.Rows[1].Height + this.neuSpread1.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                            //p.ShowPrintPageDialog();
                            p.PrintPage(this.leftValue, this.topValue, this.panel6);
                            //p.PrintPreview(this.leftValue, this.topValue, this.panel6);
                            this.neuPanel2.Dock = DockStyle.Bottom;

                            //p.PrintPreview(this.leftValue, this.topValue, this.panel6);

                            this.ucOrderBillHeader2.SetVisible(true);

                            this.SetVisibleForLong(Color.Black, true);

                            SetStyleForFp(true, Color.White, BorderStyle.None);

                            pag = neuSpread1.ActiveSheetIndex;
                            this.QueryPatientOrder();
                            this.neuSpread1.ActiveSheetIndex = pag;

                            return;
                        }
                    }
                    else
                    {
                        this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                        if (this.OrderPrintType == PrintType.DrawPaperContinue)
                        {
                            this.ucOrderBillHeader2.SetVisible(false);

                            this.SetVisibleForLong(Color.White, false);

                            SetStyleForFp(true, Color.White, BorderStyle.None);
                        }
                        else
                        {
                            if (this.GetFirstOrder(true).PageNo == -1)
                            {
                                this.SetVisibleForLong(Color.Black, false);

                                SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                            }
                            else
                            {
                                this.ucOrderBillHeader2.SetVisible(false);

                                this.SetVisibleForLong(Color.White, false);

                                this.SetStyleForFp(true, Color.White, BorderStyle.None);
                            }
                        }


                        if (OrderPrintType != PrintType.PrintWhenPatientOut)
                        {
                            this.SetPrintContentsForLong();
                        }
                    }

                    this.neuPanel2.Dock = DockStyle.None;

                    this.neuPanel2.Location = new Point(this.neuSpread1.Location.X, this.neuSpread1.Location.Y + (Int32)(this.neuSpread1.ActiveSheet.RowHeader.Rows[0].Height + this.neuSpread1.ActiveSheet.RowHeader.Rows[1].Height + this.neuSpread1.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                    //p.ShowPrintPageDialog();
                    p.PrintPage(this.leftValue, this.topValue, this.panel6);
                    this.neuPanel2.Dock = DockStyle.Bottom;
                    //p.PrintPreview(this.leftValue, this.topValue, this.panel6);

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
                        if (this.UpdateOrderForLong() <= 0)
                        {
                            this.ucOrderBillHeader2.SetValueVisible(true);

                            this.ucOrderBillHeader2.SetVisible(true);

                            this.SetVisibleForLong(Color.Black, true);

                            pag = neuSpread1.ActiveSheetIndex;
                            this.QueryPatientOrder();
                            neuSpread1.ActiveSheetIndex = pag;

                            return;
                        }
                    }

                    this.ucOrderBillHeader2.SetValueVisible(true);

                    this.ucOrderBillHeader2.SetVisible(true);

                    this.SetVisibleForLong(Color.Black, true);

                    pag = neuSpread1.ActiveSheetIndex;
                    this.QueryPatientOrder();
                    neuSpread1.ActiveSheetIndex = pag;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    this.ucOrderBillHeader2.SetVisible(true);

                    this.SetVisibleForLong(Color.Black, true);

                    pag = neuSpread1.ActiveSheetIndex;
                    this.QueryPatientOrder();
                    neuSpread1.ActiveSheetIndex = pag;
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

                    if (!this.GetIsPrintAgainForShort())
                    {
                        DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (r == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {

                            if (this.OrderPrintType == PrintType.DrawPaperContinue)
                            {
                                this.ucOrderBillHeader1.SetVisible(false);
                                this.SetVisibleForShort(Color.White, false);
                                this.SetStyleForFp(false, Color.White, BorderStyle.None);
                            }
                            else
                            {
                                this.SetVisibleForShort(Color.Black, false);

                                this.SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
                            }

                            this.SetRePrintContentsForShort();

                            this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));

                            this.neuPanel1.Dock = DockStyle.None;

                            this.neuPanel1.Location = new Point(this.neuSpread2.Location.X, this.neuSpread2.Location.Y + (Int32)(this.neuSpread2.ActiveSheet.RowHeader.Rows[0].Height + this.neuSpread2.ActiveSheet.RowHeader.Rows[1].Height + this.neuSpread2.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                            //p.ShowPrintPageDialog();
                            p.PrintPage(this.leftValue, this.topValue, this.panel7);
                            //p.PrintPreview(this.leftValue, this.topValue, this.panel7);
                            this.neuPanel1.Dock = DockStyle.Bottom;
                            //p.PrintPreview(this.leftValue, this.topValue, this.panel7);

                            this.SetStyleForFp(false, Color.White, BorderStyle.None);

                            this.ucOrderBillHeader1.SetVisible(true);

                            this.SetVisibleForShort(Color.Black, true);



                            pag = neuSpread2.ActiveSheetIndex;
                            this.QueryPatientOrder();
                            neuSpread2.ActiveSheetIndex = pag;

                            return;
                        }
                    }
                    else
                    {
                        this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));

                        if (this.OrderPrintType == PrintType.DrawPaperContinue)
                        {
                            this.ucOrderBillHeader1.SetVisible(false);

                            this.SetVisibleForShort(Color.White, false);

                            SetStyleForFp(false, Color.White, BorderStyle.None);
                        }
                        else
                        {
                            if (this.GetFirstOrder(false).PageNo == -1)
                            {
                                this.SetVisibleForShort(Color.Black, false);

                                SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
                            }
                            else
                            {
                                this.ucOrderBillHeader1.SetVisible(false);

                                this.SetVisibleForShort(Color.White, false);

                                this.SetStyleForFp(false, Color.White, BorderStyle.None);
                            }
                        }

                        if (OrderPrintType != PrintType.PrintWhenPatientOut)
                        {
                            this.SetPrintContentsForShort();
                        }
                    }

                    this.neuPanel1.Dock = DockStyle.None;

                    this.neuPanel1.Location = new Point(this.neuSpread2.Location.X, this.neuSpread2.Location.Y + (Int32)(this.neuSpread2.ActiveSheet.RowHeader.Rows[0].Height + this.neuSpread2.ActiveSheet.RowHeader.Rows[1].Height + this.neuSpread2.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                    //p.ShowPrintPageDialog();
                    p.PrintPage(this.leftValue, this.topValue, this.panel7);
                    //p.PrintPreview(this.leftValue, this.topValue, this.panel7);
                    this.neuPanel1.Dock = DockStyle.Bottom;
                    //p.PrintPreview(this.leftValue, this.topValue, this.panel7);

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
                        if (this.UpdateOrderForShort() <= 0)
                        {
                            this.ucOrderBillHeader1.SetValueVisible(true);

                            this.ucOrderBillHeader1.SetVisible(true);

                            this.SetVisibleForShort(Color.Black, true);

                            pag = neuSpread2.ActiveSheetIndex;
                            this.QueryPatientOrder();
                            neuSpread2.ActiveSheetIndex = pag;

                            return;
                        }
                    }

                    this.ucOrderBillHeader1.SetValueVisible(true);

                    this.ucOrderBillHeader1.SetVisible(true);

                    this.SetVisibleForShort(Color.Black, true);

                    pag = neuSpread2.ActiveSheetIndex;
                    this.QueryPatientOrder();
                    neuSpread2.ActiveSheetIndex = pag;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    this.ucOrderBillHeader1.SetVisible(true);

                    this.SetVisibleForShort(Color.Black, true);

                    pag = neuSpread2.ActiveSheetIndex;
                    this.QueryPatientOrder();
                    neuSpread2.ActiveSheetIndex = pag;
                }
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
            p.SetPageSize(size);
            p.IsCanCancel = false;

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
                        if (this.OrderPrintType == PrintType.DrawPaperContinue)
                        {
                            //设置标题的提示文字的可见性
                            this.ucOrderBillHeader2.SetVisible(false);
                            //设置列头的可见性
                            this.SetVisibleForLong(Color.White, false);
                            //设置显示的格式
                            SetStyleForFp(true, Color.White, BorderStyle.None);

                        }
                        else
                        {
                            //设置列头的可见性
                            this.SetVisibleForLong(Color.Black, false);
                            SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                        }

                        //设置重打显示的内容
                        this.SetRePrintContentsForLong();

                        //设置显示患者改变信息
                        this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                        this.neuPanel2.Dock = DockStyle.None;

                        this.neuPanel2.Location = new Point(this.neuSpread1.Location.X, this.neuSpread1.Location.Y + (Int32)(this.neuSpread1.ActiveSheet.RowHeader.Rows[0].Height + this.neuSpread1.ActiveSheet.RowHeader.Rows[1].Height + this.neuSpread1.ActiveSheet.Rows[0].Height * this.rowNum) - 15);
                        //this.neuPanel2.Location = new Point(this.neuSpread1.Location.X, 910);
                        //p.ShowPrintPageDialog();
                        p.PrintPage(this.leftValue, this.topValue, this.panel6);
                        this.neuPanel2.Dock = DockStyle.Bottom;
                        //p.PrintPreview(this.leftValue, this.topValue, this.panel6);

                        this.ucOrderBillHeader2.SetVisible(true);

                        this.SetVisibleForLong(Color.Black, true);

                        SetStyleForFp(true, Color.White, BorderStyle.None);

                        this.QueryPatientOrder();

                        return;

                    }
                }
                catch
                {
                    this.ucOrderBillHeader2.SetVisible(true);

                    this.SetVisibleForLong(Color.Black, true);

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
                        if (this.OrderPrintType == PrintType.DrawPaperContinue)
                        {
                            this.ucOrderBillHeader1.SetVisible(false);
                            this.SetVisibleForShort(Color.White, false);
                            this.SetStyleForFp(false, Color.White, BorderStyle.None);
                        }
                        else
                        {
                            this.SetVisibleForShort(Color.Black, false);
                            this.SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
                        }

                        this.SetRePrintContentsForShort();

                        this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));

                        this.neuPanel1.Dock = DockStyle.None;

                        this.neuPanel1.Location = new Point(this.neuSpread2.Location.X, this.neuSpread2.Location.Y + (Int32)(this.neuSpread2.ActiveSheet.RowHeader.Rows[0].Height + this.neuSpread2.ActiveSheet.RowHeader.Rows[1].Height + this.neuSpread2.ActiveSheet.Rows[0].Height * this.rowNum) - 15);
                        //this.neuPanel1.Location = new Point(this.neuSpread2.Location.X, 910);
                        //p.ShowPrintPageDialog();
                        p.PrintPage(this.leftValue, this.topValue, this.panel7);
                        //p.PrintPreview(this.leftValue, this.topValue, this.panel7);
                        this.neuPanel1.Dock = DockStyle.Bottom;
                        //p.PrintPreview(this.leftValue, this.topValue, this.panel7);

                        this.SetStyleForFp(false, Color.White, BorderStyle.None);

                        this.ucOrderBillHeader1.SetVisible(true);

                        this.SetVisibleForShort(Color.Black, true);

                        this.QueryPatientOrder();

                        return;
                    }
                }
                catch
                {
                    this.ucOrderBillHeader1.SetVisible(true);

                    this.SetVisibleForShort(Color.Black, true);

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
            p.SetPageSize(size);
            p.IsCanCancel = false;

            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.neuSpread1.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.neuSpread1.ActiveSheet.ActiveRowIndex < 0)
                {
                    return;
                }

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.neuSpread1.ActiveSheet.Rows[this.neuSpread1.ActiveSheet.ActiveRowIndex].Tag
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

                DialogResult r;

                r = MessageBox.Show("确定要重打项目:" + order.Item.Name, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                this.ucOrderBillHeader2.SetVisible(false);

                this.ucOrderBillHeader2.SetValueVisible(false);

                this.SetVisibleForLong(Color.White, false);

                this.lblPageLong.Visible = false;

                for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.neuSpread1.ActiveSheet.ActiveRowIndex)
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 8, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 9, "");
                    }
                    else
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 8, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 9, "");
                    }
                }

                p.PrintPreview((Int32)ShortOrderColunms.CombNO, 24, this.panel6);

                this.ucOrderBillHeader2.SetValueVisible(true);

                this.ucOrderBillHeader2.SetVisible(true);

                this.SetVisibleForLong(Color.Black, true);

                this.QueryPatientOrder();
            }
            else
            {
                if (this.neuSpread2.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.neuSpread2.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.neuSpread2.ActiveSheet.ActiveRowIndex <= 0)
                {
                    return;
                }

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.neuSpread2.ActiveSheet.Rows[this.neuSpread2.ActiveSheet.ActiveRowIndex].Tag
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

                DialogResult r;

                r = MessageBox.Show("确定要重打项目:" + order.Item.Name, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                this.ucOrderBillHeader1.SetVisible(false);

                this.ucOrderBillHeader1.SetValueVisible(false);

                this.SetVisibleForShort(Color.White, false);

                this.lblPageShort.Visible = false;

                for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.neuSpread2.ActiveSheet.ActiveRowIndex)
                    {
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                    }
                    else
                    {
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");//执行日期不打
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");//执行时间不打
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");//执行时间不打
                    }
                }

                p.PrintPreview(5, 16, this.panel7);

                this.ucOrderBillHeader1.SetValueVisible(true);

                this.ucOrderBillHeader1.SetVisible(true);

                this.SetVisibleForShort(Color.Black, true);

                this.QueryPatientOrder();
            }
        }

        /// <summary>
        /// 补打单条项目停止时间
        /// </summary>
        private void PrintSingleDate()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            p.SetPageSize(size);
            p.IsCanCancel = false;

            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.neuSpread1.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.neuSpread1.ActiveSheet.ActiveRowIndex < 0)
                {
                    return;
                }

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.neuSpread1.ActiveSheet.Rows[this.neuSpread1.ActiveSheet.ActiveRowIndex].Tag
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

                this.ucOrderBillHeader2.SetVisible(false);

                this.ucOrderBillHeader2.SetValueVisible(false);

                this.SetVisibleForLong(Color.White, false);

                this.lblPageLong.Visible = false;

                for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.neuSpread1.ActiveSheet.ActiveRowIndex)
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 8, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 9, "");
                    }
                    else
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 8, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 9, "");
                    }
                }

                p.PrintPreview((Int32)ShortOrderColunms.CombNO, 24, this.panel6);

                this.ucOrderBillHeader2.SetValueVisible(true);

                this.ucOrderBillHeader2.SetVisible(true);

                this.SetVisibleForLong(Color.Black, true);

                this.QueryPatientOrder();
            }
        }

        #endregion

        #region 获取打印提示

        /// <summary>
        /// 获取打印提示
        /// </summary>
        /// <returns></returns>
        private bool GetIsPrintAgainForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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
        private bool GetIsPrintAgainForShort()
        {
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return false;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return false;
                    }
                }
            }

            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                errText = "获得页码出错！";
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "获得页码出错！";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.pInfo.ID, "1");

            if (pageNo > maxPageNo + 1)
            {
                errText = "第" + (maxPageNo + 2).ToString() + "页医嘱单尚未打印！";
                return false;
            }

            /*
            if(pageNo == maxPageNo + 1 && maxPageNo != -1)
            {
                int maxRowNo = this.orderManager.GetMaxRowNoByPageNo(this.pInfo.ID,"1",maxPageNo.ToString());

                if(maxRowNo != 26)
                {
                    errText = "第"+(maxPageNo+1).ToString()+"页医嘱单尚未打满！";
                    return false;
                }
            }
            */

            if (pageNo == maxPageNo + 1 && maxPageNo != -1)
            {
                bool canprintflag = true;
                for (int j = 0; j < this.rowNum; j++)
                {
                    if (this.neuSpread1.Sheets[maxPageNo].Rows[j].Tag != null)
                    {
                        Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.Sheets[maxPageNo].Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            MessageBox.Show("请确定已放入第" + (pageNo + 1).ToString() + "页长期医嘱单！");

            return true;
        }

        /// <summary>
        /// 获取打印提示
        /// </summary>
        /// <returns></returns>
        private bool CanPrintForShort(ref string errText)
        {
            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return false;
                    }
                }
            }

            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                errText = "获得页码出错！";
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "获得页码出错！";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.pInfo.ID, "0");

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
                    if (this.neuSpread2.Sheets[maxPageNo].Rows[j].Tag != null)
                    {
                        Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.Sheets[maxPageNo].Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            return true;
        }

        #endregion

        #region 更新打印标记

        /// <summary>
        /// 更新医嘱页码和提取标志
        /// </summary>
        /// <param name="chgBed"></param>
        /// <returns></returns>
        private int UpdateOrderForLong()
        {
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();


            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("实体转换出错！");
                        return -1;
                    }

                    if (oT.Patient.ID != this.pInfo.ID)
                    {
                        continue;
                    }

                    if (oT.GetFlag == "0")
                    {
                        if (myOrder.UpdatePageNoAndRowNo(this.pInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新页码出错！" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "1", "0") <= 0)
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
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
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
        private int UpdateOrderForShort()
        {
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT.Patient.ID != this.pInfo.ID)
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
                        if (myOrder.UpdatePageNoAndRowNo(this.pInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新页码出错！" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "1", "0") <= 0)
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
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
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
            for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.neuSpread1.Sheets[i];

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

                            if (i != this.neuSpread1.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.neuSpread1.Sheets[i + 1];

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
            for (int i = 0; i < this.neuSpread2.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.neuSpread2.Sheets[i];

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

                            if (i != this.neuSpread2.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.neuSpread2.Sheets[i + 1];

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
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

                        this.lblPageLong.Visible = false;
                        this.ucOrderBillHeader2.SetValueVisible(false);
                    }
                    else
                    {
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
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //this.neuSpread2.ActiveSheet.SetValue(i,2,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//执行日期不打
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//执行时间不打
                        continue;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.Status != 3)
                        {
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoDate,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoTime,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,2,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,3,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Qty,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,8,"");
                        }
                        else
                        {
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoDate,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoTime,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,2,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,3,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Qty,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//执行日期不打
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//执行时间不打
                        }

                        this.lblPageShort.Visible = false;
                        this.ucOrderBillHeader1.SetValueVisible(false);
                    }
                    else
                    {
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoDate,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoTime,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,2,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,3,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Qty,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,8,"");
                    }
                }
            }
        }

        /// <summary>
        /// 设置长期医嘱重打显示内容
        /// </summary>
        private void SetRePrintContentsForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    //this.neuSpread1.ActiveSheet.SetValue(i,2,"");
                    //this.neuSpread1.ActiveSheet.SetValue(i,3,"");
                    //this.neuSpread1.ActiveSheet.SetValue(i,8,"");
                    //this.neuSpread1.ActiveSheet.SetValue(i,9,"");
                }
            }

            this.lblPageLong.Visible = true;
        }

        /// <summary>
        /// 设置临时医嘱重打显示内容
        /// </summary>
        private void SetRePrintContentsForShort()
        {
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    //this.neuSpread2.ActiveSheet.SetValue(i,2,"");
                    //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                    //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//执行日期不打
                    //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//执行时间不打
                    //this.neuSpread2.ActiveSheet.SetValue(i,8,"");//执行时间不打
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
                if (this.neuSpread1.ActiveSheet.Rows.Count > 0)
                {
                    if (this.neuSpread1.ActiveSheet.Rows[0].Tag != null)
                    {
                        return this.neuSpread1.ActiveSheet.Rows[0].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
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
                if (this.neuSpread2.ActiveSheet.Rows.Count > 0)
                {
                    if (this.neuSpread2.ActiveSheet.Rows[0].Tag != null)
                    {
                        return this.neuSpread2.ActiveSheet.Rows[0].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
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
            p.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;

            if (longOrder)
            {
                this.neuSpread1.ActiveSheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty,
                                    System.Drawing.Color.Empty, color,
                                    FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White,
                                    System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                    System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                    System.Drawing.Color.Empty, true, false, false, true, true);

                if (border == BorderStyle.None)
                {
                    for (int i = 0; i < this.neuSpread1.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.neuSpread1.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, false, false, false, false);
                    }
                }
                else
                {
                    for (int i = 0; i < this.neuSpread1.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.neuSpread1.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);
                    }

                    this.neuSpread1.ActiveSheet.ColumnHeader.Cells[1, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);

                    for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                    {
                        this.neuSpread1.ActiveSheet.Cells[i, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, false, false, false);
                    }
                }
            }
            else
            {
                this.neuSpread2.ActiveSheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty,
                                                   System.Drawing.Color.Empty, color,
                                                   FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White,
                                                   System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                                   System.Drawing.Color.Empty, System.Drawing.Color.Empty,
                                                   System.Drawing.Color.Empty, true, false, false, true, true);

                this.neuSpread2.BorderStyle = border;

                if (border == BorderStyle.None)
                {
                    for (int i = 0; i < this.neuSpread2.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.neuSpread2.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, false, false, false, false);
                    }
                }
                else
                {
                    for (int i = 0; i < this.neuSpread2.ActiveSheet.ColumnHeader.Columns.Count; i++)
                    {
                        this.neuSpread2.ActiveSheet.ColumnHeader.Cells[0, i].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);
                    }

                    this.neuSpread2.ActiveSheet.ColumnHeader.Cells[1, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, false);


                    for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
                    {
                        this.neuSpread2.ActiveSheet.Cells[i, 0].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered,
                            SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, false, false, false);
                    }
                }
            }
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="color"></param>
        /// <param name="vis"></param>
        private void SetVisibleForLong(Color color, bool vis)
        {
            for (int i = 0; i < this.neuSpread1.ActiveSheet.ColumnHeader.RowCount; i++)
            {
                this.neuSpread1.ActiveSheet.ColumnHeader.Rows[i].ForeColor = color;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (color == Color.White)
                {
                    if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                    {
                        //如果套打，则设置已打印的行不再显示
                        Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
                        if (oT != null)
                        {
                            if (oT.GetFlag == "1")
                            {
                                if (oT.DCOper.OperTime != DateTime.MinValue)
                                {
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.MoDate, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.MoTime, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.Qty, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.Unit, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                    //this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                    //this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                    //this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                    //this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.MoDate, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.MoTime, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.Qty, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.Unit, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                    this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                                }
                            }
                            else if (oT.GetFlag == "2")
                            {
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.MoDate, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.MoTime, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.Qty, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.Unit, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                this.neuSpread1_Sheet1.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                            }
                            else
                            {
                            }
                        }
                    }
                }
                this.neuSpread1.ActiveSheet.Rows[i].BackColor = Color.White;
            }
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="color"></param>
        /// <param name="vis"></param>
        private void SetVisibleForShort(Color color, bool vis)
        {
            for (int i = 0; i < this.neuSpread2.ActiveSheet.ColumnHeader.RowCount; i++)
            {
                this.neuSpread2.ActiveSheet.ColumnHeader.Rows[i].ForeColor = color;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (color == Color.White)
                {
                    if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                    {
                        //如果套打，则设置已打印的行不再显示
                        Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;
                        if (oT != null)
                        {
                            if (oT.GetFlag == "1")
                            {
                                if (oT.DCOper.OperTime != DateTime.MinValue)
                                {
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                    //this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                    //this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                                }
                                else
                                {
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                    this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                                }
                            }
                            else if (oT.GetFlag == "2")
                            {
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                this.neuSpread2_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                            }
                            else
                            {
                            }
                        }
                    }
                }
                this.neuSpread2.ActiveSheet.Rows[i].BackColor = Color.White;
            }
        }

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
            Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatient.QueryPatientInfoByInpatientNO(patientNo);

            TreeNode root = new TreeNode();
            root.Text = "住院信息:" + "[" + patientInfo.Name + "]" + "[" + patientInfo.PID.PatientNO + "]";
            this.treeView1.Nodes.Add(root);

            TreeNode node = new TreeNode();
            node.Text = "[" + patientInfo.PVisit.InTime.ToShortDateString() + "][" + patientInfo.PVisit.PatientLocation.Dept.Name + "]";
            node.Tag = patientInfo;
            root.Nodes.Add(node);

            //ArrayList alInTimes = this.inPatient.QueryInpatientNOByPatientNO(patientNo);

            //if (alInTimes == null)
            //{
            //    MessageBox.Show("查询患者住院信息出错！");
            //    return;
            //}

            //TreeNode root = new TreeNode();

            //for (int i = 0; i < alInTimes.Count; i++)
            //{
            //    Neusoft.FrameWork.Models.NeuObject info = alInTimes[i] as Neusoft.FrameWork.Models.NeuObject;

            //    if (i == 0)
            //    {
            //        root.Text = "住院信息:" + "[" + info.Name + "]" + "[" + patientNo + "]";
            //        this.treeView1.Nodes.Add(root);
            //        root.ImageIndex = 0;
            //    }

            //    TreeNode node = new TreeNode();

            //    node.Text = "[" + Neusoft.FrameWork.Function.NConvert.ToDateTime(info.User03).ToShortDateString() + "][" + info.User02 + "]";

            //    node.Tag = info;

            //    node.ImageIndex = 1;

            //    root.Nodes.Add(node);
            //}

            this.treeView1.ExpandAll();
        }

        #region 工具栏

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tbPrint)
            {
                this.Print();
            }
            if (e.ClickedItem == this.tbRePrint)
            {
                this.PrintAgain();
            }
            else if (e.ClickedItem == this.tbExit)
            {
                this.Close();
            }
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

        #region 重置医嘱

        /// <summary>
        /// 重置当前患者医嘱单打印状态
        /// </summary>
        private void ReSet(EnumOrderType type)
        {
            if (this.pInfo == null || this.pInfo.ID == "")
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
            if (this.orderManager.ResetOrderPrint("-1", "-1", pInfo.ID, orderType, "0") == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("重置失败!" + this.orderManager.Err);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("重置成功!");

            for (int sheetIndex = 0; sheetIndex < this.neuSpread1.Sheets.Count; sheetIndex++)
            {
                this.neuSpread1.Sheets[sheetIndex].RowCount = 0;
                this.neuSpread1.Sheets[sheetIndex].RowCount = this.rowNum;
            }
            for (int sheetIndex = 0; sheetIndex < this.neuSpread2.Sheets.Count; sheetIndex++)
            {
                this.neuSpread2.Sheets[sheetIndex].RowCount = 0;
                this.neuSpread2.Sheets[sheetIndex].RowCount = this.rowNum;
            }

            this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));
            this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));
            this.QueryPatientOrder();

        }

        #endregion

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
                Neusoft.FrameWork.Models.NeuObject obj = e.Node.Tag as Neusoft.FrameWork.Models.NeuObject;

                Neusoft.HISFC.Models.RADT.PatientInfo temp = this.inPatient.QueryPatientInfoByInpatientNO(obj.ID);

                if (temp != null)
                {
                    this.ucOrderBillHeader1.PInfo = temp;
                    this.ucOrderBillHeader2.PInfo = temp;
                    this.pInfo = temp;
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
        /// 临时医嘱分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitShortMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpShortAfter();
        }

        /// <summary>
        /// 长期医嘱分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpLongAfter(this.neuSpread1.ActiveSheetIndex,this.neuSpread1.ActiveSheet.ActiveRowIndex);
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
                        p.PrintDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
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
        /// 显示页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread2_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            this.lblPageLong.Text = (Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag) + 1).ToString();

            this.ucOrderBillHeader2.SetChangedInfo(GetFirstOrder(true));
        }


        void neuSpread2_ActiveSheetChanged(object sender, EventArgs e)
        {
            this.lblPageShort.Text = (Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag) + 1).ToString();

            this.ucOrderBillHeader1.SetChangedInfo(GetFirstOrder(false));
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
                return "加急";
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
        private void neuSpread1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.menu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "补打该条长期医嘱";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);

                System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                printDateItem.Text = "只补打该条长期医嘱停止时间";
                printDateItem.Click += new EventHandler(printDateItem_Click);

                System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                splitMenuItem.Text = "从该条医嘱往后另起一页";
                splitMenuItem.Click += new EventHandler(splitMenuItem_Click);

                this.menu.MenuItems.Add(printMenuItem);
                this.menu.MenuItems.Add(printDateItem);
                this.menu.MenuItems.Add(splitMenuItem);
                this.menu.Show(this.neuSpread1, new Point(e.X, e.Y));
            }
        }
        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.menu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "补打该条临时医嘱";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);

                System.Windows.Forms.MenuItem splitShortMenuItem = new MenuItem();
                splitShortMenuItem.Text = "从该条医嘱往后另起一页";
                splitShortMenuItem.Click += new EventHandler(splitShortMenuItem_Click);

                this.menu.MenuItems.Add(printMenuItem);
                this.menu.MenuItems.Add(splitShortMenuItem);
                this.menu.Show(this.neuSpread2, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 只补打该条长期医嘱停止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDateItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion
        
        /// <summary>
        /// 打印方式
        /// </summary>
        private enum PrintType
        {
            /// <summary>
            /// 白纸续打
            /// </summary>
            WhitePaperContinue = 0,

            /// <summary>
            /// 印刷续打
            /// </summary>
            DrawPaperContinue = 1,

            /// <summary>
            /// 出院打印
            /// </summary>
            PrintWhenPatientOut = 2
        }

        #region IPrintOrder 成员

        void Neusoft.HISFC.BizProcess.Interface.IPrintOrder.Print()
        {
            this.ShowDialog();
        }

        public void SetPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
            }

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }
            this.ucQueryInpatientNo1.Text = "";


            this.ucOrderBillHeader1.PInfo = patientInfo;
            this.ucOrderBillHeader2.PInfo = patientInfo;
            this.PInfo = patientInfo;
        }

        public void ShowPrintSet()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void frmOrderPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Clear();
        }
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
    /// </summary>
    public class AlSort : System.Collections.IComparer
    {
        #region IComparer 成员

        /// <summary>
        /// 比较方号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            Neusoft.HISFC.Models.Order.Inpatient.Order orderInfox = x as Neusoft.HISFC.Models.Order.Inpatient.Order;
            Neusoft.HISFC.Models.Order.Inpatient.Order orderInfoy = y as Neusoft.HISFC.Models.Order.Inpatient.Order;
            if (orderInfox.SortID > orderInfoy.SortID)
            {
                return 1;
            }
            else if (orderInfox.SortID == orderInfoy.SortID)
            {
                return 0;
            }
            else
            {
                return -1;
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
        MoDate = 0,

        /// <summary>
        /// 开始时间
        /// </summary>
        MoTime,

        /// <summary>
        /// 组标记
        /// </summary>
        CombFlag,

        /// <summary>
        /// 项目名称
        /// </summary>
        ItemName,

        /// <summary>
        /// 数量
        /// </summary>
        Qty,

        /// <summary>
        /// 单位
        /// </summary>
        Unit,

        /// <summary>
        /// 开立医生
        /// </summary>
        RecipeDoct,

        /// <summary>
        /// 审核护士
        /// </summary>
        ConfirmNurse,

        /// <summary>
        /// 停止日期
        /// </summary>
        DCDate,

        /// <summary>
        /// 停止时间
        /// </summary>
        DCTime,

        /// <summary>
        /// 停止医生
        /// </summary>
        DCDoct,

        /// <summary>
        /// 停止审核护士
        /// </summary>
        DCConfirmNurse,

        /// <summary>
        /// 组号
        /// </summary>
        CombNO,

        /// <summary>
        /// 列数量
        /// </summary>
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
        MoDate,

        /// <summary>
        /// 开始时间
        /// </summary>
        MoTime,

        /// <summary>
        /// 组标记
        /// </summary>
        CombFlag,

        /// <summary>
        /// 项目名称
        /// </summary>
        ItemName,

        /// <summary>
        /// 数量
        /// </summary>
        Qty,

        /// <summary>
        /// 单位
        /// </summary>
        Unit,

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
        /// 组号
        /// </summary>
        CombNO,

        /// <summary>
        /// 列数量
        /// </summary>
        Max
    }
}