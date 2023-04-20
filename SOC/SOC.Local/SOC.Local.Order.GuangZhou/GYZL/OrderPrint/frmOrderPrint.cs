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

namespace FS.SOC.Local.Order.GuangZhou.GYZL.OrderPrint
{
    /// <summary>
    /// ҽ������ӡ������
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
                this.fpLongOrder.MouseUp += new MouseEventHandler(fpLongOrder_MouseUp);
                fpShortOrder.MouseUp += new MouseEventHandler(fpShortOrder_MouseUp);

                this.tbReset.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

                this.fpLongOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpLongOrder_ColumnWidthChanged);
                this.fpLongOrder.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(fpLongOrder_ColumnDragMoveCompleted);
                this.fpShortOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpShortOrder_ColumnWidthChanged);
                this.fpShortOrder.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(fpShortOrder_ColumnDragMoveCompleted);

                this.nuRowNum.ValueChanged += new EventHandler(nuRowNum_ValueChanged);
            }
        }

        #region ����

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        //FS.SOC.HISFC.Fee.BizLogic.Undrug undrugManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
        FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        System.Windows.Forms.ContextMenu popMenu = new ContextMenu();

        /// <summary>
        /// Ĭ������ʱ��
        /// </summary>
        private DateTime reformDate = new DateTime(2000, 1, 1);

        /// <summary>
        /// �����б�
        /// </summary>
        ArrayList alLong = new ArrayList();

        /// <summary>
        /// �����б�
        /// </summary>
        ArrayList alShort = new ArrayList();
        
        /// <summary>
        /// ����ҽ���б�
        /// </summary>
        ArrayList alRecord = new ArrayList();

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo;

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
        /// </summary>
        private bool isDisplayRegularName = true;

        /// <summary>
        /// �Ƿ�ת���Զ���ҳ
        /// </summary>
        private bool isShiftDeptNextPag = false;

        /// <summary>
        /// �Ƿ������ҳ��ӡ �磺������ҽ��
        /// </summary>
        private bool isCanIntervalPrint = false;

        /// <summary>
        /// ����ҽ����XML����
        /// </summary>
        string LongOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\LongOrderPrintSetting.xml";

        /// <summary>
        /// ��ʱҽ����XML����
        /// </summary>
        string shortOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\ShortOrderPrintSetting.xml";

        #region ��ӡ����

        /// <summary>
        /// ҽ������ӡ����
        /// </summary>
        private string fileName = Application.StartupPath + "\\Setting\\ORDERPRINT.xml";

        /// <summary>
        /// ��ӡ����
        /// </summary>
        private string printerName = string.Empty;

        /// <summary>
        /// ��߾�
        /// </summary>
        private int leftValue = 0;

        /// <summary>
        /// �ϱ߾�
        /// </summary>
        private int topValue = 0;

        /// <summary>
        /// ����
        /// </summary>
        private int rowNum = 25;

        /// <summary>
        /// ��ӡ��ʽ
        /// </summary>
        private EnumPrintType OrderPrintType = EnumPrintType.PrintWhenPatientOut;

        #endregion

        #region ��Ա��ӡģʽ

        /// <summary>
        /// ������Ա��ӡģʽ��0 �մ�ӡ��1 ���Դ�ӡ��
        /// ����ҽ����ִ�л�ʿ��ֹͣҽ����ֹͣ��˻�ʿ
        /// ���磺0000
        /// </summary>
        private string operPrintMode = "1111";

        /// <summary>
        /// �Ƿ��ӡ����ҽ��
        /// </summary>
        private bool isPrintReciptDoct = true;

        /// <summary>
        /// �Ƿ��ӡ��˻�ʿ
        /// </summary>
        private bool isPrintConfirmNurse = true;

        /// <summary>
        /// �Ƿ��ӡֹͣҽ��
        /// </summary>
        private bool isPrintDCDoct = true;

        /// <summary>
        /// �Ƿ��ӡֹͣ��˻�ʿ
        /// </summary>
        private bool isPrintDCConfirmNurse = true;

        #endregion

        #endregion

        #region ����

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderPrint_Load(object sender, System.EventArgs e)
        {
            this.tbExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbRePrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C�ش�);
            this.tbQuery.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);

            this.tbReset.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbReset.Visible = true;
            this.tbSetting.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S����);
            this.tbSetting.Visible = false;

            this.ucShortOrderBillHeader.Header = "��  ʱ  ҽ  ��  ��";
            this.ucLongOrderBillHeader.Header = "��  ��  ҽ  ��  ��";

            InitOrderPrint();

            this.fpLongOrder.Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheetLong = new FarPoint.Win.Spread.SheetView();
            this.InitLongSheet(sheetLong);
            this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheetLong);

            this.fpShortOrder.Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheetShort = new FarPoint.Win.Spread.SheetView();
            this.InitShortSheet(sheetShort);
            this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheetShort);

            if (((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
            {
                this.ResetCurrentLong.Visible = false;
                this.ResetCurrentShort.Visible = false;
                this.RefreshLong.Visible = false;
                this.RefreshShort.Visible = false;
            }

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
        /// ��ʼ����������
        /// </summary>
        private void InitOrderPrint()
        {
            #region ���Ʋ���

            //����ҽ�Ʋ�������ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
            try
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                isShiftDeptNextPag = controlIntegrate.GetControlParam<bool>("HNZY32", true, true);

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

            #region ��ӡ��������

            //��ȡϵͳ��ӡ���б�
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                //ȡ��ӡ����
                string name = System.Text.RegularExpressions.Regex.Replace(PrinterSettings.InstalledPrinters[i], @"��(\s|\S)*��", "").Replace("�Զ�", "");
                this.tbPrinter.Items.Add(name);
            }

            //��ӡ��ʽ�б�
            this.cmbPrintType.AddItems(FS.FrameWork.Public.EnumHelper.Current.EnumArrayList<EnumPrintType>());
            if (cmbPrintType.Items.Count > 0)
            {
                cmbPrintType.SelectedIndex = 0;
            }

            //��XML��Ĭ������
            if (File.Exists(fileName))
            {
                XmlDocument file = new XmlDocument();
                file.Load(fileName);
                XmlNode node = file.SelectSingleNode("ORDERPRINT/ҽ����");
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

                node = file.SelectSingleNode("ORDERPRINT/��߾�");
                if (node != null)
                {
                    this.leftValue = FS.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuLeft.Value = this.leftValue;

                node = file.SelectSingleNode("ORDERPRINT/�ϱ߾�");
                if (node != null)
                {
                    this.topValue = FS.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuTop.Value = this.topValue;

                node = file.SelectSingleNode("ORDERPRINT/����");
                if (node != null)
                {
                    this.rowNum = FS.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuRowNum.Value = this.rowNum;

                node = file.SelectSingleNode("ORDERPRINT/Ԥ����ӡ");
                if (node != null)
                {
                    cbxPreview.Checked = FS.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                }

                node = file.SelectSingleNode("ORDERPRINT/��ӡ��ʽ");
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

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);
            this.cmbPrintType.SelectedIndexChanged += new EventHandler(cmbPrintType_SelectedIndexChanged);

            cbxPreview.CheckedChanged += new EventHandler(cbxPreview_CheckedChanged);

            #endregion

            #region ��������



            #endregion

            if (((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
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
            this.SetPrintValue("Ԥ����ӡ", "1");
        }

        void cmbPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPrintType.Text))
            {
                this.SetPrintValue("��ӡ��ʽ", this.cmbPrintType.Text);
            }

            if (cmbPrintType.Text == FS.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.WhitePaperContinue))
            {
                this.OrderPrintType = EnumPrintType.WhitePaperContinue;
            }
            else if(cmbPrintType.Text == FS.FrameWork.Public.EnumHelper.Current.GetName(EnumPrintType.DrawPaperContinue))
            {
                this.OrderPrintType = EnumPrintType.DrawPaperContinue;
            }
            else
            {
                this.OrderPrintType = EnumPrintType.PrintWhenPatientOut;
            }
        }

        /// <summary>
        /// ��ʼ������ҽ��Sheet
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

            #region ����

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.BeginDate).ColumnSpan = 5;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.BeginDate).Text = " ��   ʼ ";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.BeginDate).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.BeginTime).Text = "ʱ��";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.FirstQty).Text = "���մ���";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.RecipeDoct).RowSpan = 1;
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.RecipeDoct).Text = "ҽʦǩ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ConfirmNurse).RowSpan = 1;
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.ConfirmNurse).Text = "��ʿǩ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.CombFlag).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.CombFlag).Text = "��";
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ItemName).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ItemName).Text = " ����ҽ�� ";


            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.OnceDose).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.OnceDose).Text = "ÿ����";


            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Memo).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Memo).Text = "��ע";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Usage).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Usage).Text = "�÷�";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Freq).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.Freq).Text = "Ƶ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDate).ColumnSpan = 4;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDate).Text = "ͣ   ֹ";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCDate).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCTime).Text = "ʱ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDoct).RowSpan = 1;
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCDoct).Text = "ҽʦǩ��";
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCConfirmNurse).RowSpan = 1;
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCConfirmNurse).Text = "��ʿǩ��";


            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ExecNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ExecNurse).Text = "ִ��";


            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;
            sheet.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            #endregion

            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).CellType = textCellType1;
            sheet.Columns.Get((Int32)LongOrderColunms.OnceDose).CellType = textCellType1;
            sheet.Columns.Get((Int32)LongOrderColunms.Freq).CellType = textCellType1;
            sheet.Columns.Get((Int32)LongOrderColunms.Usage).CellType = textCellType1;
            sheet.Columns.Get((Int32)LongOrderColunms.Memo).CellType = textCellType1;

            sheet.Columns.Get((Int32)LongOrderColunms.BeginDate).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.BeginTime).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.FirstQty).Width = 40F;
            sheet.Columns.Get((Int32)LongOrderColunms.CombFlag).Width = 17F;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).Width = 300F;

            sheet.Columns.Get((Int32)LongOrderColunms.OnceDose).Width = 0;
            sheet.Columns.Get((Int32)LongOrderColunms.Usage).Width = 0;
            sheet.Columns.Get((Int32)LongOrderColunms.Freq).Width = 0;
            sheet.Columns.Get((Int32)LongOrderColunms.Memo).Width = 0;

            sheet.Columns.Get((Int32)LongOrderColunms.RecipeDoct).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.ConfirmNurse).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDate).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCTime).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDoct).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCConfirmNurse).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.ExecNurse).Width = 0;

            sheet.Columns.Get((Int32)LongOrderColunms.CombNO).Visible = false;

            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            #region ����ÿ�еĸ߶�

            //ҽ�����ĸ߶�
            //int fpLongHeight = 910;
            int fpLongHeight = 890;

            float fpLongHeaderHeight = 0;

            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(0).Height;
            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(1).Height;

            int rowHeight = FS.FrameWork.Function.NConvert.ToInt32((fpLongHeight - fpLongHeaderHeight) / this.rowNum);

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
        /// ��ʼ����ʱҽ��Sheet
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

            #region ����

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.BeginDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.BeginDate).Text = " ��  �� ";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.BeginDate).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.BeginTime).Text = "ʱ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.RecipeDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.RecipeDoct).Text = "ҽʦǩ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.CombFlag).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.CombFlag).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.CombFlag).Text = "��ʱҽ��";//"��";
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ItemName).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ItemName).Text = "��ʱҽ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.OnceDose).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.OnceDose).Text = "ÿ����";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Usage).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Usage).Text = "�÷�";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Freq).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Freq).Text = "Ƶ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Qty).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Qty).Text = "����";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Memo).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.Memo).Text = "��ע";


            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmNurse).Text = "�� ʿ ǩ ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmDate).ColumnSpan = 5;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmDate).Text = "��    ��";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.ConfirmDate).Text = "ִ��ʱ��";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.ConfirmTime).Text = "��ʿǩ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCFlage).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCFlage).Text = "ȡ��";
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.DCDoct).Text = "ҽ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ExecOper).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ExecOper).Text = "ִ��";

            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;

            #endregion

            sheet.Columns.Get((Int32)ShortOrderColunms.BeginTime).Label = "ʱ��";

            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).CellType = textCellType2;
            sheet.Columns.Get((Int32)ShortOrderColunms.Qty).CellType = textCellType2;
            sheet.Columns.Get((Int32)ShortOrderColunms.Freq).CellType = textCellType2;
            sheet.Columns.Get((Int32)ShortOrderColunms.Usage).CellType = textCellType2;
            sheet.Columns.Get((Int32)ShortOrderColunms.Memo).CellType = textCellType2;


            sheet.Columns.Get((Int32)ShortOrderColunms.CombNO).Visible = false;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCDoct).Visible = false;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCFlage).Visible = false;

            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            sheet.Columns.Get((Int32)ShortOrderColunms.BeginDate).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.BeginTime).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.CombFlag).Width = 17F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).Width = 400F;

            sheet.Columns.Get((Int32)ShortOrderColunms.OnceDose).Width = 0;
            sheet.Columns.Get((Int32)ShortOrderColunms.Usage).Width = 0;
            sheet.Columns.Get((Int32)ShortOrderColunms.Freq).Width = 0;
            sheet.Columns.Get((Int32)ShortOrderColunms.Qty).Width = 0;
            sheet.Columns.Get((Int32)ShortOrderColunms.Memo).Width = 0;

            sheet.Columns.Get((Int32)ShortOrderColunms.RecipeDoct).Width = 55F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmNurse).Width = 0;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmDate).Width = 60F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmTime).Width = 60F;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCFlage).Width = 0;
            sheet.Columns.Get((Int32)ShortOrderColunms.DCDoct).Width = 0;
            sheet.Columns.Get((Int32)ShortOrderColunms.ExecOper).Width = 0;

            #region ����ÿ�еĸ߶�

            int fpLongHeight = 890;

            float fpLongHeaderHeight = 0;

            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(0).Height;
            fpLongHeaderHeight += sheet.ColumnHeader.Rows.Get(1).Height;

            int rowHeight = FS.FrameWork.Function.NConvert.ToInt32((fpLongHeight - fpLongHeaderHeight) / this.rowNum);

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

            //���ǹ���Ա�������ش�ť
            //if (!((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    this.tbReset.Visible = false;
            //}
        }

        #endregion

        #region ��ѯ��ʾ

        /// <summary>
        /// ��ѯ����ҽ����Ϣ
        /// </summary>
        private void QueryPatientOrder()
        {
            if (this.myPatientInfo == null || string.IsNullOrEmpty(myPatientInfo.ID))
            {
                return;
            }

            //����Ҫ��һ�£�Ϊɶ��û�е������� ������ʾ��ѡ����... Ӧ������ʾ����

            //if (SaveUserDefaultSetting())
            //{
            //    MessageBox.Show(
            //}

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");
            Application.DoEvents();

            //try
            //{
                ArrayList alAll = new ArrayList();

                alAll = this.orderManager.QueryPrnOrder(this.myPatientInfo.ID);

                alLong.Clear();
                alShort.Clear();

                FS.HISFC.Models.Order.Inpatient.Order order = null;
                foreach (object obj in alAll)
                {
                    order = obj as FS.HISFC.Models.Order.Inpatient.Order;

                    order.Nurse.Name = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order.Nurse.ID);
                    order.ReciptDoctor.Name = order.ReciptDoctor.Name;
                    if (order.DCNurse.ID != null && order.DCNurse.ID != "")
                    {
                        order.DCNurse.Name = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order.DCNurse.ID);
                    }

                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        order.Item.Name += orderManager.TransHypotest(order.HypoTest);
                    }

                    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        //����ҽ��
                        alLong.Add(obj);
                    }
                    else
                    {
                        //��ʱҽ��
                        alShort.Add(obj);
                    }
                }

                FS.FrameWork.Management.ExtendParam myInpatient = new FS.FrameWork.Management.ExtendParam();

                this.alRecord = myInpatient.GetComExtInfoListByPatient(FS.HISFC.Models.Base.EnumExtendClass.PATIENT, myPatientInfo.ID);

                this.Clear();

                //this.AddObjectToFpLong(alLong);

                try
                {
                    this.AddOrderToFP(true, alLong);
                    this.AddOrderToFP(false, alShort);

                }
                catch (Exception e)
                {
 
                }


                this.ucShortOrderBillHeader.SetChangedInfo(this.GetFirstOrder(false));
                this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder(true));

                this.SetFPStyle();


                //SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);

                //SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int SetFPStyle()
        {
            foreach (FarPoint.Win.Spread.SheetView sv in fpLongOrder.Sheets)
            {
                if (System.IO.File.Exists(LongOrderSetXML))
                {
                    fpLongOrder.ReadSchema(sv, LongOrderSetXML, false);

                    sv.PrintInfo.ZoomFactor = sv.ZoomFactor;
                }
                if (!((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
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

                if (!((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
                {
                    for (int i = 0; i < sv.Columns.Count; i++)
                    {
                        sv.Columns[i].Resizable = false;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// ����������ʾ
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

            root.Text = "סԺ��Ϣ:" + "[" + this.myPatientInfo.Name + "]" + "[" + this.myPatientInfo.PID.PatientNO + "]";

            this.treeView1.Nodes.Add(root);

            root.ImageIndex = 0;

            TreeNode node = new TreeNode();

            node.Text = "[" + this.myPatientInfo.PVisit.InTime.ToShortDateString() + "][" + this.myPatientInfo.PVisit.PatientLocation.Dept.Name + "]";

            node.Tag = this.myPatientInfo;

            node.ImageIndex = 1;

            root.Nodes.Add(node);

            this.treeView1.ExpandAll();
        }

        #region ���

        /// <summary>
        /// �������ҽ��������
        /// </summary>
        private void Clear()
        {
            #region ����

            #region ����һ��Sheet
            this.fpLongOrder.Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
            this.InitLongSheet(sheet);
            this.fpLongOrder.Sheets.Add(sheet);
            this.lblPageLong.Text = "��1ҳ";

            #endregion

            #endregion

            #region ����

            #region ����һ��Sheet
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

            this.lblPageShort.Text = "��1ҳ";
            #endregion

            #endregion
        }

        #endregion

        /// <summary>
        /// ��ȡ��ӡ��ʱ�䣺��ʼʱ�����ʱ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private DateTime GetPrintDate(FS.HISFC.Models.Order.Order order)
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
        /// ���뵽ҽ������ʾ
        /// </summary>
        /// <param name="isLong">�Ƿ���</param>
        /// <param name="alOrder"></param>
        private void AddOrderToFP(bool isLong, ArrayList alOrder)
        {
            if (alOrder.Count <= 0)
            {
                return;
            }

            //δ��ӡ�б�
            ArrayList alPageNull = new ArrayList();
            //�Ѵ�ӡ�б�
            ArrayList alPageNo = new ArrayList();

            //����ҳ�롢�кš���š������ ����
            alOrder.Sort(new OrderComparer());

            int MaxPageNo = -1;
            int MaxRowNo = 0;

            //���ڴ���ת�ƻ�ҳ
            string deptCode = "";

            //����ҽ�����
            string speOrderType="";

            //�洢���һ����������ҽ������
            //�������� ����ҽ��->����->����ҽ�� ʶ�� ����->����ҽ�� �費��Ҫ��ҳ
            string deptOrderType = "";

            //������������ҽ��
            int moState = -1;

            foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrder)
            {
                if (inOrder.RowNo >= this.rowNum)
                {
                    MessageBox.Show(inOrder.OrderType.Name + "��" + inOrder.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (inOrder.RowNo + 1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                }

                if (string.IsNullOrEmpty(deptCode))
                {
                    deptCode = inOrder.ReciptDept.ID;
                }
                //if (string.IsNullOrEmpty(speOrderType))
                //{
                //    speOrderType = inOrder.SpeOrderType;
                //}

                #region ����

                if (isLong)
                {
                    //�Ѵ�ӡ��
                    if (inOrder.GetFlag != "0")
                    {
                        //��ҳ
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
                    //δ��ӡ
                    else
                    {
                        if ((MaxRowNo % this.rowNum == 0) ||
                            //ת��ҲҪ�Զ���ҳ
                            (this.isShiftDeptNextPag &&
                            !string.IsNullOrEmpty(deptCode)
                            && deptCode.Trim() != inOrder.ReciptDept.ID


                            //����ҽ��ת������ҽ��ʱ������ҳ
                            //�����޸����������
                            && !(
                            //����ҽ��->�ա���->�ա�����ҽ��->��
                                string.IsNullOrEmpty(inOrder.SpeOrderType)
                            //����ҽ��->����ҽ��
                                || (speOrderType.Contains("DEPT") && inOrder.SpeOrderType.Contains("CONS"))
                            //����ҽ��->����ҽ������֤����ԭ����ҽ��)
                                || ((speOrderType.Contains("CONS") || string.IsNullOrEmpty(speOrderType))
                                    && inOrder.SpeOrderType.Contains("DEPT")
                                    && inOrder.ReciptDept.ID + inOrder.SpeOrderType.Substring(0, 4) == deptOrderType)
                                )
                            )
                            )
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
                            //if (MessageBox.Show("��������ҽ�����Ƿ��Զ���ҳ��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            //{
                            MaxPageNo += 1;
                            MaxRowNo = 0;

                            if (MaxPageNo >= this.fpLongOrder.Sheets.Count)
                            {
                                FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                                this.InitLongSheet(sheet);
                                this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);
                            }
                            //}
                        }

                        this.AddToRow(isLong, false, this.fpLongOrder.Sheets[MaxPageNo], MaxRowNo, inOrder);
                        MaxRowNo += 1;
                    }

                    if (this.fpLongOrder.Sheets[MaxPageNo].Tag == null)
                    {
                        this.fpLongOrder.Sheets[MaxPageNo].Tag = MaxPageNo;
                        this.fpLongOrder.Sheets[MaxPageNo].SheetName = "��" + (MaxPageNo + 1).ToString() + "ҳ";
                    }

                    //ʼ�մ���һ��ҽ����״̬
                    moState = inOrder.Status;
                }
                #endregion

                #region ����
                else
                {
                    //�Ѵ�ӡ��
                    if (inOrder.GetFlag != "0")
                    {
                        //��ҳ
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
                    //δ��ӡ
                    else
                    {
                        if ((MaxRowNo % this.rowNum == 0) ||
                            //ת��ҲҪ�Զ���ҳ
                             (this.isShiftDeptNextPag &&
                             !string.IsNullOrEmpty(deptCode)
                             && deptCode.Trim() != inOrder.ReciptDept.ID
                            //����ҽ��ת������ҽ��ʱ������ҳ
                            //�����޸����������
                            && !(
                            //����ҽ��->�ա���->�ա�����ҽ��->��
                                string.IsNullOrEmpty(inOrder.SpeOrderType)
                            //����ҽ��->����ҽ��
                                || (speOrderType.Contains("DEPT") && inOrder.SpeOrderType.Contains("CONS"))
                            //����ҽ��->����ҽ������֤����ԭ����ҽ��)
                                || ((speOrderType.Contains("CONS") || string.IsNullOrEmpty(speOrderType))
                                    && inOrder.SpeOrderType.Contains("DEPT") 
                                    && inOrder.ReciptDept.ID + inOrder.SpeOrderType.Substring(0, 4) == deptOrderType)
                                )
                             )
                            )
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
                        this.fpShortOrder.Sheets[MaxPageNo].SheetName = "��" + (MaxPageNo + 1).ToString() + "ҳ";
                    }
                }
                #endregion

                if (deptCode != inOrder.ReciptDept.ID)
                {
                    deptCode = inOrder.ReciptDept.ID;
                }

                speOrderType = inOrder.SpeOrderType;

                if (inOrder.SpeOrderType.Contains("DEPT"))
                {
                    deptOrderType = inOrder.ReciptDept.ID + inOrder.SpeOrderType.Substring(0, 4);
                }
            }

            for (int i = 0; i < this.fpLongOrder.Sheets.Count; i++)
            {
                DrawCombFlag(this.fpLongOrder.Sheets[i], (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
            }

            for (int i = 0; i < this.fpShortOrder.Sheets.Count; i++)
            {
                Classes.Function.DrawComboLeft(this.fpShortOrder.Sheets[i], (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
            }
        }

        /// <summary>
        /// ÿ�����
        /// </summary>
        /// <param name="isLong"></param>
        /// <param name="isPint"></param>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        /// <param name="inOrder"></param>
        private void AddToRow(bool isLong, bool isPint, FarPoint.Win.Spread.SheetView sheet, int row,FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            #region ����

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

                //sheet.SetValue(row, (Int32)LongOrderColunms.ItemName, GetOrderItem(inOrder, isLong));
                string orderStr = string.Empty;

                orderStr = GetOrderItem(inOrder, isLong);

                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    orderStr += FS.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce) + inOrder.DoseUnit;
                }
                else
                {
                    orderStr += FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Item.PriceUnit;
                }

                orderStr += " " + inOrder.Usage.Name;
                orderStr += " " + inOrder.Frequency.ID;
                orderStr += inOrder.Memo;
                //orderStr += "��" + FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Unit;
                sheet.SetValue(row, (Int32)LongOrderColunms.ItemName, orderStr);

                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    sheet.SetValue(row, (Int32)LongOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce) + inOrder.DoseUnit);
                }
                else
                {
                    sheet.SetValue(row, (Int32)LongOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Item.PriceUnit);
                }

                sheet.SetValue(row, (Int32)LongOrderColunms.Usage, inOrder.Usage.Name);
                sheet.SetValue(row, (Int32)LongOrderColunms.Freq, inOrder.Frequency.ID);
                sheet.SetValue(row, (Int32)LongOrderColunms.Memo, inOrder.Memo);
                sheet.SetValue(row, (Int32)LongOrderColunms.FirstQty, inOrder.FirstUseNum);

                sheet.SetValue(row, (Int32)LongOrderColunms.RecipeDoct, inOrder.ReciptDoctor.Name);
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

            #region ����
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

                //sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, GetOrderItem(inOrder, isLong));
                string orderStr = string.Empty;

                orderStr = GetOrderItem(inOrder, isLong);

                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    orderStr += FS.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce) + inOrder.DoseUnit;
                }
                else
                {
                    orderStr += FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Item.PriceUnit;
                }

                orderStr += " " + inOrder.Usage.Name;
                orderStr += " " + inOrder.Frequency.ID;
                orderStr += inOrder.Memo;
                orderStr += "��" + FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Unit;
                sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, orderStr);

                if (inOrder.Item.ItemType == EnumItemType.Drug)
                {
                    sheet.SetValue(row, (Int32)ShortOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(inOrder.DoseOnce) + inOrder.DoseUnit);
                }
                else
                {
                    sheet.SetValue(row, (Int32)ShortOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Item.PriceUnit);
                }
                sheet.SetValue(row, (Int32)ShortOrderColunms.Usage, inOrder.Usage.Name);
                sheet.SetValue(row, (Int32)ShortOrderColunms.Freq, inOrder.Frequency.ID);
                sheet.SetValue(row, (Int32)ShortOrderColunms.Qty, FS.FrameWork.Public.String.ToSimpleString(inOrder.Qty) + inOrder.Unit);
                sheet.SetValue(row, (Int32)ShortOrderColunms.Memo, inOrder.Memo);

                sheet.SetValue(row, (Int32)ShortOrderColunms.RecipeDoct, inOrder.ReciptDoctor.Name);
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

                    sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, GetOrderItem(inOrder, isLong) + "  [ȡ��]");
                }

                sheet.Rows[row].Tag = inOrder;
            }
            #endregion
        }

        /// <summary>
        /// ��ȡ��ʾ��ҽ������
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isLong"></param>
        /// <returns></returns>
        private string GetOrderItem(FS.HISFC.Models.Order.Inpatient.Order order, bool isLong)
        {
            string itemName = "";
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                itemName = order.Item.Name;
                //�Ƿ���ʾͨ����������
                if (isDisplayRegularName)
                {
                    if (order.Item.ID == "999")
                    {
                        itemName = order.Item.Name;// +" " + order.Frequency.ID; //+ " " + order.Usage.Name + " " + order.Memo;
                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;
                        itemName = phaItem.NameCollection.RegularName;// +" " + order.Frequency.ID;  //+ " " + order.Usage.Name + " " + order.Memo;
                    }
                }

                if(string.IsNullOrEmpty(itemName))
                {
                    itemName = order.Item.Name;
                }
            }
            else
            {
                itemName = order.Item.Name;
            }

            string addStr = "";
            if (order.OrderType.ID == "CD")
            {
                addStr = "(��Ժ��ҩ)";
            }
            else if (order.OrderType.ID == "BL")
            {
                addStr = "(��¼ҽ��)";
            }

            string qty = "";
            if (!order.OrderType.IsDecompose && order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                qty = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + order.Unit;
            }

            //�������׷�ҩ��Ŀ����ʾ�������
            //FS.SOC.HISFC.Fee.Models.Undrug oneUndrug = undrugManager.GetUndrug(order.Item.ID);
            FS.HISFC.Models.Fee.Item.Undrug oneUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
            if ( oneUndrug != null && oneUndrug.UnitFlag.Equals("1"))
            {
                //itemName += " (������)";
            }
            
            //������ʳ����ʾƵ��
            if (order.Item.Name.Contains("����") || order.Item.Name.Contains("��ʳ"))
            {
                itemName += " " + addStr;
                //itemName += " " + order.Usage.Name + " " + qty + " " + addStr + "" + order.Memo;
            }
            else
            {
                //�ж��Ƿ���ʾͨ����ʱ�Ѿ���ʾ��Ƶ�Σ�����Ҫ�����
                itemName += addStr;
                //itemName += " " + order.Usage.Name + " " + qty + " " + addStr + "" + order.Memo;
            }
            itemName += order.IsEmergency ? "[��]" : "";
            return itemName;
        }

        /// <summary>
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        //[Obsolete("����", true)]
        private void AddObjectToFpLongAfter(int sheet, int row)
        {
            DateTime now = this.orderManager.GetDateTimeFromSysDateTime().Date;//��ǰϵͳʱ��

            #region �������

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            if (this.fpLongOrder.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = this.fpLongOrder.Sheets[sheet].Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            //MessageBox.Show("����ҽ�����ҳ����ע�⣡", "��ʾ");

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "�Ѿ���ӡ������������һҳ��");
                return;
            }

            #endregion

            #region ��ȡʣ������

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

            #region �������

            for (int j = row; j < this.fpLongOrder.Sheets[sheet].Rows.Count; j++)
            {
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.BeginDate, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.BeginTime, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.FirstQty, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.CombFlag, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ItemName, "");

                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.OnceDose, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Usage, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Freq, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Memo, "");

                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.RecipeDoct, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ConfirmNurse, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.DCDate, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.DCTime, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.DCDoct, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.CombNO, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.DCConfirmNurse, "");
                this.fpLongOrder.Sheets[sheet].Rows[j].Tag = null;
            }

            #endregion

            #region ����һ��Sheet

            if (this.fpLongOrder.Sheets.Count > 1)
            {
                for (int j = this.fpLongOrder.Sheets.Count - 1; j > sheet; j--)
                {
                    this.fpLongOrder.Sheets.RemoveAt(j);
                }
            }

            #endregion

            #region ��ʾδ��ӡҽ��

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
            activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

            iniIndex = 0;
            endIndex = alPageNull.Count;

            for (; iniIndex < endIndex; iniIndex++)
            {
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;

                if ((iniIndex + MaxRowNo) % rowNum == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView newsheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(newsheet);

                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());


                    string orderStr = string.Empty;

                    orderStr = GetOrderItem(oTemp, false);

                    if (oTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        orderStr += FS.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit;
                    }
                    else
                    {
                        orderStr += FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Item.PriceUnit;
                    }

                    orderStr += oTemp.Usage.Name;
                    orderStr += oTemp.Frequency.ID;
                    orderStr += oTemp.Memo;
                    orderStr += "��" + FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Unit;

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, orderStr);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.FirstQty, oTemp.FirstUseNum);
                    //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, GetOrderItem(oTemp, true));


                    //if (oTemp.Item.ItemType == EnumItemType.Drug)
                    //{
                    //    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    //}
                    //else
                    //{
                    //    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Item.PriceUnit);
                    //}

                    //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Usage, oTemp.Usage.Name);
                    //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Freq, oTemp.Frequency.ID);
                    //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Memo, oTemp.Memo);

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCDate, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCTime, oTemp.DCOper.OperTime.ToShortTimeString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCDoct, oTemp.DCOper.Name);
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCConfirmNurse, oTemp.DCNurse.Name);
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    DrawCombFlag(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, GetOrderItem(oTemp, true));

                    if (oTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    }
                    else
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Item.PriceUnit);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Memo, oTemp.Memo);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.FirstQty, oTemp.FirstUseNum);

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCDate, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCTime, oTemp.DCOper.OperTime.ToShortTimeString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCDoct, oTemp.DCOper.Name);
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.DCConfirmNurse, oTemp.DCNurse.Name);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    DrawCombFlag(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
            }

            #endregion
        }

        private void DrawCombFlag(FarPoint.Win.Spread.SheetView view, int combNocol, int flagCol)
        {
            Classes.Function.DrawComboLeft(view, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
            view.Columns[(Int32)LongOrderColunms.CombFlag].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            for (int i = 0; i < view.RowCount; i++)
            {
                if (view.Rows[i].Tag != null)
                {
                    view.Cells[i, (Int32)LongOrderColunms.CombFlag].Text = ((FS.HISFC.Models.Order.Inpatient.Order)view.Rows[i].Tag).SubCombNO.ToString() + view.Cells[i, (Int32)LongOrderColunms.CombFlag].Text;
                }
            }
        }

        /// <summary>
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        //[Obsolete("����", true)]
        private void AddObjectToFpShortAfter(int sheet, int row)
        {
            #region �������

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            //int row = this.fpShortOrder.Sheets[sheet].ActiveRowIndex;

            if (this.fpShortOrder.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = this.fpShortOrder.Sheets[sheet].Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            if (MessageBox.Show("ȷ��Ҫ��" + ord.Item.Name + "��ʼ����һҳ��?�˲������ɳ�����", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "�Ѿ���ӡ������������һҳ��");
                return;
            }

            #endregion

            #region ��ȡʣ������

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

            #region �������

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

            #region ����һ��Sheet

            if (this.fpShortOrder.Sheets.Count > 1)
            {
                for (int j = this.fpShortOrder.Sheets.Count - 1; j > sheet; j--)
                {
                    this.fpShortOrder.Sheets.RemoveAt(j);
                }
            }

            #endregion

            #region ��ʾδ��ӡҽ��

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
            activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

            iniIndex = 0;
            endIndex = alPageNull.Count;

            for (; iniIndex < endIndex; iniIndex++)
            {
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;

                if ((iniIndex + MaxRowNo) % rowNum == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView newsheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(newsheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    string orderStr = string.Empty;

                    orderStr = GetOrderItem(oTemp, false);

                    if (oTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        orderStr += FS.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit;
                    }
                    else
                    {
                        orderStr += FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Item.PriceUnit;
                    }

                    orderStr += oTemp.Usage.Name;
                    orderStr += oTemp.Frequency.ID;
                    orderStr += oTemp.Memo;
                    orderStr += "��" + FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Unit;

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, orderStr);

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                    activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct].Font = new Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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

                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, GetOrderItem(oTemp, false) + "  [ȡ��]");
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

                    if (oTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(oTemp.DoseOnce) + oTemp.DoseUnit);
                    }
                    else
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.OnceDose, FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Item.PriceUnit);
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Usage, oTemp.Usage.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Freq, oTemp.Frequency.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Memo, oTemp.Memo);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, FS.FrameWork.Public.String.ToSimpleString(oTemp.Qty) + oTemp.Unit);

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, GetOrderItem(oTemp, false) + "  [ȡ��]");
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Classes.Function.DrawComboLeft(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
            }

            #endregion
        }

        #endregion

        #region ��ӡ

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            DialogResult rr = MessageBox.Show("ȷ��Ҫ��ӡ��ҳ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

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

            #region ����ҽ��
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

                    #region �ش�
                    if (!this.GetIsPrintAgainForLong())
                    {
                        DialogResult r = MessageBox.Show("ȷ��Ҫ�ش��ҳ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (r == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                            {
                                //���ñ������ʾ���ֵĿɼ���
                                this.SetTitleVisible(true, false);
                                //������ͷ�Ŀɼ���
                                this.SetVisibleForLong(Color.White, false, -1);
                                //������ʾ�ĸ�ʽ
                                SetStyleForFp(true, Color.White, BorderStyle.None);

                            }
                            else
                            {
                                //������ͷ�Ŀɼ���
                                this.SetVisibleForLong(Color.Black, false, -1);
                                SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                            }

                            //�����ش���ʾ������
                            this.SetRePrintContentsForLong();

                            //������ʾ���߸ı���Ϣ
                            this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder(true));

                            this.pnLongPag.Dock = DockStyle.None;

                            this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

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

                    #region �״δ�ӡ
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


                    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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

                    frmNotice.label1.Text = "������ҽ�����Ƿ�ɹ�?";

                    frmNotice.ShowDialog();

                    dia = frmNotice.dr;

                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("ȷ������û�гɹ�������������ҽ�������ֿ��У�", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //ȷ������û�гɹ���û��˵��
                        }
                        else
                        {
                            dia = DialogResult.Yes;
                        }
                    }

                    if (dia == DialogResult.Yes)
                    {
                        //FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //��Ժ��ӡʱ�������ҳ��ӡ
                        if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
                        {
                            for (int index = 0; index <= this.fpLongOrder.ActiveSheetIndex; index++)
                            {
                                if (this.UpdateOrderForLong(index) <= 0)
                                {
                                    //FS.FrameWork.Management.PublicTrans.RollBack();

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
                                //FS.FrameWork.Management.PublicTrans.RollBack();

                                SetTitleVisible(true, true);

                                this.SetVisibleForLong(Color.Black, true, -1);

                                pagNo = this.fpLongOrder.ActiveSheetIndex;
                                this.QueryPatientOrder();
                                this.fpLongOrder.ActiveSheetIndex = pagNo;

                                return;
                            }
                        }
                        //FS.FrameWork.Management.PublicTrans.Commit();
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

                #region ��ӡ�곤�����ѯҽ��ҳ���Ƿ����� by huangchw 2012-09-12
                int count = this.orderManager.CheckPageRowNoAndGetFlag(this.myPatientInfo.ID, "1");//��ѯ����
                if (count < 0)
                {
                    MessageBox.Show("ҳ����ȡ��־��ѯ����");
                }
                else if (count > 0)
                {
                    //MessageBox.Show("ע�⣺�� " + count + " ��ҽ������ҳ���������ϵ��Ϣ�ơ�");
                }
                #endregion
            }
            #endregion

            #region ��ʱҽ��
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
                        DialogResult r = MessageBox.Show("ȷ��Ҫ�ش��ҳ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

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
                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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
                    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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

                    frmNotice.label1.Text = "������ʱҽ�����Ƿ�ɹ�?";

                    frmNotice.ShowDialog();

                    dia = frmNotice.dr;

                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("ȷ������û�гɹ�������������ҽ�������ֿ��У�", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //ȷ������û�гɹ���û��˵��
                        }
                        else
                        {
                            dia = DialogResult.Yes;
                        }
                    }

                    if (dia == DialogResult.Yes)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //��Ժ��ӡʱ�������ҳ��ӡ
                        if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
                        {
                            for (int index = 0; index <= this.fpShortOrder.ActiveSheetIndex; index++)
                            {
                                if (this.UpdateOrderForShort(index) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();

                                    SetTitleVisible(false, true);

                                    this.SetVisibleForShort(Color.Black, true, -1);

                                    pagNo = this.fpShortOrder.ActiveSheetIndex;
                                    this.QueryPatientOrder();
                                    this.fpShortOrder.ActiveSheetIndex = pagNo;

                                    return;
                                }
                            }
                        }
                        else  //�ǳ�Ժ��ӡ
                        {
                            if (this.UpdateOrderForShort(this.fpShortOrder.ActiveSheetIndex) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();

                                SetTitleVisible(false, true);

                                this.SetVisibleForShort(Color.Black, true, -1);

                                pagNo = this.fpShortOrder.ActiveSheetIndex;
                                this.QueryPatientOrder();
                                this.fpShortOrder.ActiveSheetIndex = pagNo;

                                return;
                            }
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
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
                
                #region ��ӡ���������ѯҽ��ҳ���Ƿ����� by huangchw 2012-09-12
                int count = this.orderManager.CheckPageRowNoAndGetFlag(this.myPatientInfo.ID,"0");//��ѯ����
                if (count < 0)
                {
                     MessageBox.Show("ҳ����ȡ��־��ѯ����");
                 }
                 else if (count > 0)
                 {
                     //MessageBox.Show("ע�⣺�� " + count + " ��ҽ������ҳ���������ϵ��Ϣ�ơ�");
                 }
                 #endregion
            }
            #endregion
        }

        /// <summary>
        /// ���´�ӡ
        /// </summary>
        private void PrintAgain()
        {
            DialogResult rr = MessageBox.Show("ȷ��Ҫ��ӡ��ҳ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            print.SetPageSize(size);
            print.IsCanCancel = false;

            string errText = "";
            frmNotice frmNotice = new frmNotice();

            #region //����ҽ��
            if (this.tabControl1.SelectedIndex == 0)
            {
                try
                {
                    if (!this.CanPrintForLong(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    DialogResult r = MessageBox.Show("ȷ��Ҫ�ش��ҳ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (r == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (this.OrderPrintType == EnumPrintType.DrawPaperContinue)
                        {
                            //���ñ������ʾ���ֵĿɼ���
                            SetTitleVisible(true, false);
                            //������ͷ�Ŀɼ���
                            this.SetVisibleForLong(Color.White, false, -1);
                            //������ʾ�ĸ�ʽ
                            SetStyleForFp(true, Color.White, BorderStyle.None);

                        }
                        else
                        {
                            //������ͷ�Ŀɼ���
                            this.SetVisibleForLong(Color.Black, false, -1);
                            SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                        }

                        //�����ش���ʾ������
                        this.SetRePrintContentsForLong();

                        //������ʾ���߸ı���Ϣ
                        this.ucLongOrderBillHeader.SetChangedInfo(this.GetFirstOrder(true));

                        this.pnLongPag.Dock = DockStyle.None;

                        this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                        if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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

            #region //��ʱҽ��
            else
            {
                try
                {
                    if (!this.CanPrintForShort(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    DialogResult r = MessageBox.Show("ȷ��Ҫ�ش��ҳ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

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

                        if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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
        /// ��������Ŀ
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

                FS.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("��Ŀ:" + order.Item.Name + "��δ��ӡ");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("ȷ��Ҫ�ش���Ŀ:" + order.Item.Name, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

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
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                    }
                    else
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                    }
                }

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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

                FS.HISFC.Models.Order.Inpatient.Order order = this.fpShortOrder.ActiveSheet.Rows[this.fpShortOrder.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("��Ŀ:" + order.Item.Name + "��δ��ӡ");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("ȷ��Ҫ�ش���Ŀ:" + order.Item.Name, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

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
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");//ִ��ʱ�䲻��
                    }
                    else
                    {
                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");

                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");//ִ�����ڲ���
                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");//ִ��ʱ�䲻��
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");//ִ��ʱ�䲻��
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");//ִ��ʱ�䲻��
                    }
                }


                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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
        /// ��������Ŀֹͣʱ��
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

                FS.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag
                    as FS.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("��Ŀ:" + order.Item.Name + "��δ��ӡ");
                    return;
                }

                if (order.Status < 3)
                {
                    MessageBox.Show("��Ŀ:" + order.Item.Name + "��δֹͣ");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("ȷ��Ҫֻ��ӡ��Ŀ:" + order.Item.Name + "��ֹͣʱ��?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

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
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
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

                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                    }
                }

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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

        #region ��ȡ��ӡ��ʾ

        /// <summary>
        /// �Ƿ��ش� true����false�ش�
        /// </summary>
        /// <returns></returns>
        private bool GetIsPrintAgainForLong()
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
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
        /// �Ƿ��ش� true����false�ش�
        /// </summary>
        /// <returns></returns>
        private bool GetIsPrintAgainForShort()
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
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
        /// ��ȡ��ӡ��ʾ
        /// </summary>
        /// <returns></returns>
        private bool CanPrintForLong(ref string errText)
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                errText = "���ҳ�����";
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "���ҳ�����\r\nҳ��Ϊ������";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "1");

            //��Ժ��ӡʱ�������ҳ��ӡ
            if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
            {
                if (pageNo > maxPageNo + 1)
                {
                    if (MessageBox.Show("��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ���Ƿ������ӡ��" + (pageNo + 1).ToString() + "ҳ��\r\n\r\n������ӡ����������ǰ" + (pageNo + 1).ToString() + "ҳ��ӡ��ǣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
                //�ж���һҳ�Ƿ��ӡ��ȫ
                if (pageNo == maxPageNo + 1 && maxPageNo != -1)
                {
                    bool canprintflag = true;
                    FS.HISFC.Models.Order.Inpatient.Order oT = null;
                    for (int j = 0; j < this.rowNum; j++)
                    {
                        if (this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                        {
                            oT = this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        if (MessageBox.Show("��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ�����Ƿ������ӡ��" + (pageNo + 1).ToString() + "ҳ��\r\n\r\n������ӡ����������ǰ" + (pageNo + 1).ToString() + "ҳ��ӡ��ǣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
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
                    errText = "��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ��";
                    return false;
                }

                //�ж���һҳ�Ƿ��ӡ��ȫ
                if (pageNo == maxPageNo + 1 && maxPageNo != -1)
                {
                    bool canprintflag = true;
                    FS.HISFC.Models.Order.Inpatient.Order oT = null;
                    for (int j = 0; j < this.rowNum; j++)
                    {
                        if (this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                        {
                            oT = this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        errText = "��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ����";
                        return false;
                    }
                }
            }

            MessageBox.Show("��ȷ���ѷ����" + (pageNo + 1).ToString() + "ҳ����ҽ������");

            return true;
        }

        /// <summary>
        /// ��ȡ��ӡ��ʾ
        /// </summary>
        /// <returns></returns>
        private bool CanPrintForShort(ref string errText)
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                errText = "���ҳ�����";
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "���ҳ�����";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "0");

            //��Ժ��ӡʱ�������ҳ��ӡ
            if (isCanIntervalPrint && this.OrderPrintType == EnumPrintType.PrintWhenPatientOut)
            {
                if (pageNo > maxPageNo + 1)
                {
                    if (MessageBox.Show("��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ���Ƿ������ӡ��" + (pageNo + 1).ToString() + "ҳ��\r\n\r\n������ӡ����������ǰ" + (pageNo + 1).ToString() + "ҳ��ӡ��ǣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        errText = "��ȡ����ӡ��";
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
                            FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.Sheets[maxPageNo].Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        if (MessageBox.Show("��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ�����Ƿ������ӡ��" + (pageNo + 1).ToString() + "ҳ��\r\n\r\n������ӡ����������ǰ" + (pageNo + 1).ToString() + "ҳ��ӡ��ǣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
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
                    errText = "��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ��";
                    return false;
                }

                if (pageNo == maxPageNo + 1 && maxPageNo != -1)
                {
                    bool canprintflag = true;
                    for (int j = 0; j < rowNum; j++)
                    {
                        if (this.fpShortOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                        {
                            FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.Sheets[maxPageNo].Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                            if (oT.PageNo != maxPageNo)
                            {
                                canprintflag = false;
                                break;
                            }

                        }
                    }
                    if (!canprintflag)
                    {
                        errText = "��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ����";
                        return false;
                    }
                }

                MessageBox.Show("��ȷ���ѷ����" + (pageNo + 1).ToString() + "ҳ��ʱҽ������");
            }

            return true;
        }

        #endregion

        #region ���´�ӡ���

        /// <summary>
        /// ����ҽ��ҳ�����ȡ��־
        /// </summary>
        /// <param name="chgBed"></param>
        /// <returns></returns>
        private int UpdateOrderForLong(int sheetIndex)
        {
            FS.HISFC.BizLogic.Order.Order myOrder = new FS.HISFC.BizLogic.Order.Order();

            if (this.fpLongOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = sheetIndex; //FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.Models.Order.Inpatient.Order oT = null;
            for (int i = 0; i < this.fpLongOrder.Sheets[sheetIndex].Rows.Count; i++)
            {
                if (this.fpLongOrder.Sheets[sheetIndex].Rows[i].Tag != null)
                {
                    oT = this.fpLongOrder.Sheets[sheetIndex].Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ʵ��ת������");
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
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() ,"2", "0") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "1", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString(), "1", "0") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
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
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// ����ҽ��ҳ�����ȡ��־
        /// </summary>
        /// <param name="chgBed"></param>
        /// <returns></returns>
        private int UpdateOrderForShort(int sheetIndex)
        {
            FS.HISFC.BizLogic.Order.Order myOrder = new FS.HISFC.BizLogic.Order.Order();

            

            if (this.fpShortOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = sheetIndex;//FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.Models.Order.Inpatient.Order oT = null;
            for (int i = 0; i < this.fpShortOrder.Sheets[sheetIndex].Rows.Count; i++)
            {
                if (this.fpShortOrder.Sheets[sheetIndex].Rows[i].Tag != null)
                {
                    oT = this.fpShortOrder.Sheets[sheetIndex].Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT.Patient.ID != this.myPatientInfo.ID)
                    {
                        continue;
                    }

                    if (oT == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ʵ��ת������");
                        return -1;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //if (myOrder.UpdatePageNoAndRowNo(this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        if (myOrder.UpdatePageRowNoAndGetflag(
                            this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() , "0", "0") <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }
                        
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() ,"2", "0") <= 0)
                            { 
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "1", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString(), "1", "0") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
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
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                    }

                }//if
            }//for

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        #endregion

        /// <summary>
        /// �õ���ϴ����������ʾ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ArrayList GetLongOrderByCombId(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.alLong.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order ord = alLong[i] as FS.HISFC.Models.Order.Inpatient.Order;

                if (order.Combo.ID == ord.Combo.ID)
                {
                    al.Add(ord);
                }
            }
            return al;
        }

        /// <summary>
        /// �õ���ϴ����������ʾ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ArrayList GetShortOrderByCombId(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.alShort.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order ord = alShort[i] as FS.HISFC.Models.Order.Inpatient.Order;

                if (order.Combo.ID == ord.Combo.ID)
                {
                    al.Add(ord);
                }
            }

            return al;
        }

        /// <summary>
        /// ����ҳ��Ŵ�ӡ
        /// </summary>
        private void DealLongOrderCrossPage()
        {
            for (int i = 0; i < this.fpLongOrder.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.fpLongOrder.Sheets[i];

                if (view.Rows[view.Rows.Count - 1].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ot = view.Rows[view.Rows.Count - 1].Tag
                        as FS.HISFC.Models.Order.Inpatient.Order;

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
                                FS.HISFC.Models.Order.Inpatient.Order ot1 = view.Rows[j].Tag
                                    as FS.HISFC.Models.Order.Inpatient.Order;

                                if (ot1 != null)
                                {
                                    if (ot1.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                    }
                                }

                            }

                            if (i != this.fpLongOrder.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.fpLongOrder.Sheets[i + 1];

                                for (int j = 0; j < viewNext.Rows.Count; j++)
                                {
                                    FS.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
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
        /// ����ҳ��Ŵ�ӡ
        /// </summary>
        private void DealShortOrderCrossPage()
        {
            for (int i = 0; i < this.fpShortOrder.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.fpShortOrder.Sheets[i];

                if (view.Rows[view.Rows.Count - 1].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ot = view.Rows[view.Rows.Count - 1].Tag
                        as FS.HISFC.Models.Order.Inpatient.Order;

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
                                FS.HISFC.Models.Order.Inpatient.Order ot1 = view.Rows[j].Tag
                                    as FS.HISFC.Models.Order.Inpatient.Order;

                                if (ot1 != null)
                                {
                                    if (ot1.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                    }
                                }

                            }

                            if (i != this.fpShortOrder.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.fpShortOrder.Sheets[i + 1];

                                for (int j = 0; j < viewNext.Rows.Count; j++)
                                {
                                    FS.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag
                                        as FS.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
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
        /// ���ô�ӡ��ʾ
        /// </summary>
        /// <returns></returns>
        private void SetPrintContentsForLong()
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
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
        /// ���ô�ӡ��ʾ
        /// </summary>
        /// <returns></returns>
        private void SetPrintContentsForShort()
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                        //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                        //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//ִ�����ڲ���
                        //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//ִ��ʱ�䲻��
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
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)LongOrderColunms.DCDate,"");
                        }
                        else
                        {
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoDate,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.MoTime,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,3,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Qty,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//ִ�����ڲ���
                            //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//ִ��ʱ�䲻��
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
        /// ���ó���ҽ���ش���ʾ����
        /// </summary>
        private void SetRePrintContentsForLong()
        {
            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return;
                    }
                }
            }

            this.lblPageLong.Visible = true;
            this.lblLongPage.Visible = true;
        }

        /// <summary>
        /// ������ʱҽ���ش���ʾ����
        /// </summary>
        private void SetRePrintContentsForShort()
        {
            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return;
                    }

                    //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//ִ�����ڲ���
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//ִ��ʱ�䲻��
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)LongOrderColunms.DCDate,"");//ִ��ʱ�䲻��
                }
            }

            this.lblPageShort.Visible = true;
            lblShortPageFoot.Visible = true;
        }

        /// <summary>
        /// ��ȡSheet��һ��ҽ��
        /// </summary>
        /// <param name="longOrder"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Order.Inpatient.Order GetFirstOrder(bool longOrder)
        {
            if (longOrder)
            {
                if (this.fpLongOrder.ActiveSheet.Rows.Count > 0)
                {
                    if (this.fpLongOrder.ActiveSheet.Rows[0].Tag != null)
                    {
                        return this.fpLongOrder.ActiveSheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;
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
                        return this.fpShortOrder.ActiveSheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;
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
        /// ����Fp��ʽ
        /// </summary>
        /// <param name="longOrder">�Ƿ���</param>
        /// <param name="color"></param>
        private void SetStyleForFp(bool longOrder, Color color, System.Windows.Forms.BorderStyle border)
        {
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

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
                this.lblLongPage.Visible = isVisible;
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
                lblShortPageFoot.Visible = isVisible;
                //this.lblShortSign.Visible = isVisible;
            }
        }

        /// <summary>
        /// ������ʾ
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
                #region ��Ա��Ϣ�Ƿ���ʾ��ӡ

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
                        //����״��������Ѵ�ӡ���в�����ʾ
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
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
        /// ������ʾ
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
                #region ��Ա��Ϣ�Ƿ���ʾ��ӡ

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
                        //����״��������Ѵ�ӡ���в�����ʾ
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
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

                                    //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "".PadLeft(GetStrLength(GetOrderItem(oT, false)), 'a') + "  [ȡ��]");
                                    this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "".PadLeft(GetStrLength(GetOrderItem(oT, false)), ' ') + "  [ȡ��]");

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
                else
                {
                    //�ش�
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (oT != null)
                    {
                        if (i == rowIndex)
                        {
                            continue;
                        }

                        if (oT.GetFlag == "0")
                        {
                            /*
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
                             */
                            this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                            this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                        }
                        else if (oT.GetFlag == "1")
                        {
                            this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                            this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");

                            if (oT.DCOper.OperTime != DateTime.MinValue)
                            {
                                
                            }
                            else
                            {
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                            }
                        }
                        else if (oT.GetFlag == "2")
                        {
                            this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                            this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                        }
                        else
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ�ֽڳ���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private int GetStrLength(string str)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 2;
                }
                else
                {
                    len += 1;
                }
            }

            return len;
        }

        #region ����ҽ��

        /// <summary>
        /// ���õ�ǰ����ҽ������ӡ״̬
        /// </summary>
        /// <param name="type">���ͣ�����������</param>
        /// <param name="pagNo">����ҳ�� -1��ʾȫ������</param>
        private void ReSet(EnumOrderType type, int pagNo)
        {
            if (this.myPatientInfo == null || this.myPatientInfo.ID == "")
            {
                return;
            }

            string orderType = "ALL";
            string page = pagNo == -1 ? "ȫ��" : "��" + pagNo.ToString() + "ҳ";

            string message = "ҽ����";
            switch (type)
            {
                case EnumOrderType.Long:
                    message = "����ҽ����";
                    orderType = "1";
                    break;
                case EnumOrderType.Short:
                    message = "��ʱҽ����";
                    orderType = "0";
                    break;
                default:
                    break;
            }

            DialogResult rr = MessageBox.Show("����ȡ��" + page + message + "�Ĵ�ӡ״̬��\r\n\r\n���ú�����" + message + "����Ҫ���´�ӡ��\r\n\r\n�Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.orderManager.ResetOrderPrint("-1", "-1", myPatientInfo.ID, orderType, "0") == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ��!" + this.orderManager.Err);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("���óɹ�!");

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

        #region �¼�

        /// <summary>
        /// �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("סԺ�Ŵ���û���ҵ��û���", 111);
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
            root.Text = "סԺ��Ϣ:" + "[" + patientInfo.Name + "]" + "[" + patientInfo.PID.PatientNO + "]";
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

        #region ������

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //��ӡ
            if (e.ClickedItem == this.tbPrint)
            {
                this.Print();
            }
            //����
            if (e.ClickedItem == this.tbRePrint)
            {
                this.PrintAgain();
            }
            //�رմ���
            else if (e.ClickedItem == this.tbExit)
            {
                this.Close();
            }
            //else
            //{
            //}
            //���ó���
            else if (e.ClickedItem == this.ResetLong)
            {
                this.ReSet(EnumOrderType.Long, -1);
            }
            //��������
            else if (e.ClickedItem == this.ResetShort)
            {
                this.ReSet(EnumOrderType.Short, -1);
            }
            //����ȫ��ҽ��
            else if (e.ClickedItem == this.ResetAll)
            {
                this.ReSet(EnumOrderType.All, -1);
            }
            //���õ���ҳ����
            else if (e.ClickedItem == this.ResetCurrentLong)
            {
            }
            //���õ���ҳ����
            else if (e.ClickedItem == this.ResetCurrentShort)
            {
            }
            //ˢ�����г�����ӡ״̬��ȫ�����ã������´�ӡ״̬
            else if (e.ClickedItem == this.RefreshLong)
            {
            }
            //ˢ������������ӡ״̬��ȫ�����ã������´�ӡ״̬
            else if (e.ClickedItem == this.RefreshShort)
            {
            }
        }

        #endregion

        /// <summary>
        /// ѡ��
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
                    this.myPatientInfo = temp;
                    this.QueryPatientOrder();
                }
            }
        }

        /// <summary>
        /// ������߾�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuLeft_ValueChanged(object sender, EventArgs e)
        {
            this.leftValue = FS.FrameWork.Function.NConvert.ToInt32(this.nuLeft.Value);
            this.SetPrintValue("��߾�", this.leftValue.ToString());
        }

        /// <summary>
        /// �����ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuRowNum_ValueChanged(object sender, EventArgs e)
        {
            this.rowNum = FS.FrameWork.Function.NConvert.ToInt32(this.nuRowNum.Value);
            this.SetPrintValue("����", this.rowNum.ToString());
        }

        /// <summary>
        /// �ϱ߾�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuTop_ValueChanged(object sender, EventArgs e)
        {
            this.topValue = FS.FrameWork.Function.NConvert.ToInt32(this.nuTop.Value);
            this.SetPrintValue("�ϱ߾�", this.topValue.ToString());
        }

        /// <summary>
        /// ѡ���ӡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.printerName = tbPrinter.SelectedItem.ToString();

            this.SetPrintValue("ҽ����", this.printerName);

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
        /// ���ô�ӡ��
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
        /// �����л���ʾҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpLongOrder_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            if (this.fpLongOrder.Sheets.Count > 0 &&
                this.fpLongOrder.ActiveSheet.Tag != null)
            {
                this.lblPageLong.Text = "��" + (FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag) + 1).ToString() + "ҳ";

                this.ucLongOrderBillHeader.SetChangedInfo(GetFirstOrder(true));

                //SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
            }
        }

        /// <summary>
        /// �����л���ʾҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpShortOrder_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpShortOrder.Sheets.Count > 0 &&
                this.fpShortOrder.ActiveSheet.Tag != null)
            {
                this.lblPageShort.Text = "��" + (FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag) + 1).ToString() + "ҳ";

                this.ucShortOrderBillHeader.SetChangedInfo(GetFirstOrder(false));

                //SetStyleForFp(false, Color.Black, BorderStyle.FixedSingle);
            }
        }

        /// <summary>
        /// ��üӼ�״̬
        /// </summary>
        /// <param name="isEmr"></param>
        /// <returns></returns>
        private string GetEmergencyTip(bool isEmr)
        {
            if (isEmr)
            {
                return "������";
            }
            else
            {
                return "";
            }
        }

        #region ��ӡ

        /// <summary>
        /// �Ҽ���ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpLongOrder_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "�����������ҽ��";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);
                this.popMenu.MenuItems.Add(printMenuItem);

                System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                printDateItem.Text = "ֻ�����������ҽ��ֹͣʱ��";
                printDateItem.Click += new EventHandler(printDateItem_Click);
                this.popMenu.MenuItems.Add(printDateItem);

                System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                splitMenuItem.Text = "�Ӹ���ҽ����������һҳ";
                splitMenuItem.Click += new EventHandler(splitMenuItem_Click);
                this.popMenu.MenuItems.Add(splitMenuItem);

                System.Windows.Forms.MenuItem mnSetPrinted = new MenuItem();
                mnSetPrinted.Text = "���ø���ҽ���Ѵ�ӡ";
                mnSetPrinted.Click += new EventHandler(mnSetPrinted_Click);
                this.popMenu.MenuItems.Add(mnSetPrinted);


                System.Windows.Forms.MenuItem mnAddBlankLine = new MenuItem();
                mnAddBlankLine.Text = "���ӿհ���";
                mnAddBlankLine.Click += new EventHandler(mnAddBlankLine_Click);
                this.popMenu.MenuItems.Add(mnAddBlankLine);

                this.popMenu.Show(this.fpLongOrder, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// �Ҽ���ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpShortOrder_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "���������ʱҽ��";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);
                this.popMenu.MenuItems.Add(printMenuItem);

                System.Windows.Forms.MenuItem splitShortMenuItem = new MenuItem();
                splitShortMenuItem.Text = "�Ӹ���ҽ����������һҳ";
                splitShortMenuItem.Click += new EventHandler(splitShortMenuItem_Click);
                this.popMenu.MenuItems.Add(splitShortMenuItem);

                System.Windows.Forms.MenuItem mnSetPrinted = new MenuItem();
                mnSetPrinted.Text = "���ø���ҽ���Ѵ�ӡ";
                mnSetPrinted.Click += new EventHandler(mnSetPrinted_Click);
                this.popMenu.MenuItems.Add(mnSetPrinted);


                System.Windows.Forms.MenuItem mnAddBlankLine = new MenuItem();
                mnAddBlankLine.Text = "���ӿհ���";
                mnAddBlankLine.Click += new EventHandler(mnAddBlankLine_Click);
                this.popMenu.MenuItems.Add(mnAddBlankLine);

                this.popMenu.Show(this.fpShortOrder, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// ���ø���ҽ���Ѵ�ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnSetPrinted_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            FS.HISFC.Models.Order.Inpatient.Order preOrder = null;

            //���ҳ��
            int maxPageNo = 0;
            //��ǰҳ��
            int pageNo = 0;
            //�к�
            int rowNum = -1;

            if (this.tabControl1.SelectedTab == tpLong)
            {
                order = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                if (MessageBox.Show("�Ƿ����ó���ҽ����" + order.Item.Name + "��Ϊ�Ѵ�ӡ״̬��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "1");
                pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

                if (pageNo > maxPageNo + 1)
                {
                    MessageBox.Show("ǰһҳҽ��δ��ӡ��\r\n\r\n���ܼ������ã�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (fpLongOrder.ActiveSheet.ActiveRowIndex > 0)
                {
                    preOrder = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    rowNum = preOrder.RowNo;
                }
                //��һҳ
                else if (pageNo > 0)
                {
                    preOrder = this.fpLongOrder.Sheets[fpLongOrder.ActiveSheetIndex - 1].Rows[fpLongOrder.Sheets[fpLongOrder.ActiveSheetIndex - 1].RowCount - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                }
            }
            else
            {

                order = this.fpShortOrder.ActiveSheet.Rows[this.fpShortOrder.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (MessageBox.Show("�Ƿ�������ʱҽ����" + order.Item.Name + "��Ϊ�Ѵ�ӡ״̬��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "0");
                pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

                if (pageNo > maxPageNo + 1)
                {
                    MessageBox.Show("ǰһҳҽ��δ��ӡ��\r\n\r\n���ܼ������ã�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (fpShortOrder.ActiveSheet.ActiveRowIndex > 0)
                {
                    preOrder = this.fpShortOrder.ActiveSheet.Rows[this.fpShortOrder.ActiveSheet.ActiveRowIndex - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    rowNum = preOrder.RowNo;
                }
                //��һҳ
                else if (pageNo > 0)
                {
                    preOrder = this.fpShortOrder.Sheets[fpShortOrder.ActiveSheetIndex - 1].Rows[fpShortOrder.Sheets[fpShortOrder.ActiveSheetIndex - 1].RowCount - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                }
            }

            if (order == null)
            {
                return;
            }

            if (order.PageNo != -1 || order.RowNo != -1)
            {
                MessageBox.Show("����ҽ���Ѿ���ӡ��\r\n\r\n���ܼ������ã�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (preOrder != null && preOrder.RowNo == -1)
            {
                MessageBox.Show("��һ��ҽ����δ��ӡ������������һ��ҽ���Ĵ�ӡ״̬��", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //��������޸�Ϊ ������´�ӡ��ǣ�����Ҫ���� һ���������ҳ�����
            string updateSQL = @"update met_ipm_order f
                                set f.rowno={3},
                                f.pageno={2},
                                f.get_flag='1'
                                where f.mo_order='{1}'
                                and f.inpatient_no='{0}'";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (orderManager.ExecNoQuery(updateSQL, order.Patient.ID, order.ID, pageNo.ToString(), (rowNum + 1).ToString()) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���´�ӡ��ǳ���\r\n\r\n" + orderManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("���³ɹ���\r\n\r\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.tabControl1.SelectedTab == tpLong)
            {
                //for (int i = 0; i < fpLongOrder.ActiveSheet.RowCount; i++)
                //{
                //    FS.HISFC.Models.Order.Inpatient.Order inOrder = fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                //    if (inOrder.ID == order.Combo.ID)
                //    {
                        order.GetFlag = "1";
                        order.PageNo = pageNo;
                        order.RowNo = rowNum + 1;

                        this.AddToRow(true, true, this.fpLongOrder.ActiveSheet, this.fpLongOrder.ActiveSheet.ActiveRowIndex, order);
                //    }
                //}
            }
            else
            {
                //for (int i = 0; i < this.fpShortOrder.ActiveSheet.RowCount; i++)
                //{
                //    FS.HISFC.Models.Order.Inpatient.Order inOrder = fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                //    if (inOrder.Combo.ID == order.Combo.ID)
                //    {
                        order.GetFlag = "1";
                        order.PageNo = pageNo;
                        order.RowNo = rowNum + 1;

                        this.AddToRow(true, true, this.fpShortOrder.ActiveSheet, this.fpShortOrder.ActiveSheet.ActiveRowIndex, order);
                //    }
                //}
            }
        }

        /// <summary>
        /// ���ӿհ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnAddBlankLine_Click(object sender, EventArgs e)
        {
            MessageBox.Show("���ܿ����У������ڴ���");
        }

        /// <summary>
        /// ��ʱҽ����ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitShortMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpShortAfter(this.fpShortOrder.ActiveSheetIndex, this.fpShortOrder.ActiveSheet.ActiveRowIndex);
            this.SetFPStyle();
        }

        /// <summary>
        /// ����ҽ����ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpLongAfter(this.fpLongOrder.ActiveSheetIndex, this.fpLongOrder.ActiveSheet.ActiveRowIndex);
            this.SetFPStyle();
        }

        /// <summary>
        /// �Ҽ���ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printMenuItem_Click(object sender, EventArgs e)
        {
            this.PrintSingleItem();
        }


        /// <summary>
        /// ֻ�����������ҽ��ֹͣʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDateItem_Click(object sender, EventArgs e)
        {
            PrintSingleDate();
        }

        #endregion

        #endregion

        #region IPrintOrder ��Ա

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
    /// ��ӡ��ʽ
    /// </summary>
    public enum EnumPrintType
    {
        /// <summary>
        /// ��ֽ����
        /// </summary>
        [FS.FrameWork.Public.Description("��ֽ����")]
        WhitePaperContinue = 0,

        /// <summary>
        /// ӡˢ����
        /// </summary>
        [FS.FrameWork.Public.Description("ӡˢ����")]
        DrawPaperContinue = 1,

        /// <summary>
        /// ��Ժ��ӡ
        /// </summary>
        [FS.FrameWork.Public.Description("��Ժ��ӡ")]
        PrintWhenPatientOut = 2
    }

    /// <summary>
    /// ҽ������ö��
    /// </summary>
    public enum EnumOrderType
    {
        /// <summary>
        /// ����
        /// </summary>
        Long,

        /// <summary>
        /// ����
        /// </summary>
        Short,

        /// <summary>
        /// ȫ��ҽ��
        /// </summary>
        All
    }


    /// <summary>
    /// ҽ���Ƚϣ����ݷ��ţ�
    /// �������ȼ���1��ҳ�룻2���кţ�3�����ţ������
    /// </summary>
    public class OrderComparer : System.Collections.IComparer
    {
        #region IComparer ��Ա

        /// <summary>
        /// �ȽϷ���
        /// �������ȼ���
        /// 1��ҳ�룻2���кţ�3�����ţ������
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
    /// ����ҽ��������
    /// </summary>
    public enum LongOrderColunms
    {
        /// <summary>
        /// ��ʼ����
        /// </summary>
        [FS.FrameWork.Public.Description("����")]
        BeginDate = 0,

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        [FS.FrameWork.Public.Description("ʱ��")]
        BeginTime,

        /// <summary>
        /// ������
        /// </summary>
        [FS.FrameWork.Public.Description("���մ���")]
        FirstQty,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        [FS.FrameWork.Public.Description("����ҽ��")]
        RecipeDoct,

        /// <summary>
        /// ��˻�ʿ
        /// </summary>
        [FS.FrameWork.Public.Description("��˻�ʿ")]
        ConfirmNurse,

        /// <summary>
        /// ����
        /// </summary>
        [FS.FrameWork.Public.Description("��")]
        CombFlag,

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        [FS.FrameWork.Public.Description("����ҽ��")]
        ItemName,

        /// <summary>
        /// ÿ����
        /// </summary>
        [FS.FrameWork.Public.Description("ÿ����")]
        OnceDose,

        /// <summary>
        /// Ƶ��
        /// </summary>
        [FS.FrameWork.Public.Description("Ƶ��")]
        Freq,

        /// <summary>
        /// �÷�
        /// </summary>
        [FS.FrameWork.Public.Description("�÷�")]
        Usage,

        /// <summary>
        /// ��ע
        /// </summary>
        [FS.FrameWork.Public.Description("��ע")]
        Memo,

        /// <summary>
        /// ֹͣ����
        /// </summary>
        [FS.FrameWork.Public.Description("����")]
        DCDate,

        /// <summary>
        /// ֹͣʱ��
        /// </summary>
        [FS.FrameWork.Public.Description("ʱ��")]
        DCTime,

        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        [FS.FrameWork.Public.Description("ֹͣҽ��")]
        DCDoct,

        /// <summary>
        /// ֹͣ��˻�ʿ
        /// </summary>
        [FS.FrameWork.Public.Description("��˻�ʿ")]
        DCConfirmNurse,

        /// <summary>
        /// ִ�л�ʿ
        /// </summary>
        [FS.FrameWork.Public.Description("ִ�л�ʿ")]
        ExecNurse,

        /// <summary>
        /// ���
        /// </summary>
        [FS.FrameWork.Public.Description("���")]
        CombNO,

        /// <summary>
        /// ������
        /// </summary>
        [FS.FrameWork.Public.Description("����")]
        Max
    }

    /// <summary>
    /// ��ʱҽ��������
    /// </summary>
    public enum ShortOrderColunms
    {
        /// <summary>
        /// ��ʼ����
        /// </summary>
        BeginDate,

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        BeginTime,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        RecipeDoct,

        /// <summary>
        /// ����
        /// </summary>
        CombFlag,

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        ItemName,


        /// <summary>
        /// ÿ����
        /// </summary>
        [FS.FrameWork.Public.Description("ÿ����")]
        OnceDose,

        /// <summary>
        /// Ƶ��
        /// </summary>
        [FS.FrameWork.Public.Description("Ƶ��")]
        Freq,

        /// <summary>
        /// �÷�
        /// </summary>
        [FS.FrameWork.Public.Description("�÷�")]
        Usage,

        /// <summary>
        /// ����
        /// </summary>
        [FS.FrameWork.Public.Description("����")]
        Qty,


        /// <summary>
        /// ��ע
        /// </summary>
        [FS.FrameWork.Public.Description("��ע")]
        Memo,

        /// <summary>
        /// ��˻�ʿ
        /// </summary>
        ConfirmNurse,

        /// <summary>
        /// �������
        /// </summary>
        ConfirmDate,

        /// <summary>
        /// ���ʱ��
        /// </summary>
        ConfirmTime,

        /// <summary>
        /// ֹͣ���
        /// </summary>
        DCFlage,

        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        DCDoct,
        
        /// <summary>
        /// ִ����
        /// </summary>
        ExecOper,

        /// <summary>
        /// ���
        /// </summary>
        CombNO,

        /// <summary>
        /// ������
        /// </summary>
        Max
    }
}