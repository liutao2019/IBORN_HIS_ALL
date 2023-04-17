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
    /// ҽ������ӡ������
    /// </summary>
    public partial class frmOrderPrint : Neusoft.FrameWork.WinForms.Forms.BaseStatusBar, Neusoft.HISFC.BizProcess.Interface.IPrintOrder
    {
        public frmOrderPrint()
        {
            InitializeComponent();
        }

        #region ����

        Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        //Neusoft.SOC.HISFC.Fee.BizLogic.Undrug undrugManager = new Neusoft.SOC.HISFC.Fee.BizLogic.Undrug();
        Neusoft.HISFC.BizLogic.RADT.InPatient inPatientMgr = new Neusoft.HISFC.BizLogic.RADT.InPatient();

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
        private Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo;

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
        string LongOrderSetXML = Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + @".\LongOrderPrintSetting.xml";

        /// <summary>
        /// ��ʱҽ����XML����
        /// </summary>
        string shortOrderSetXML = Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + @".\ShortOrderPrintSetting.xml";

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
        private int rowNum = 27;

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
            this.tbExit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbRePrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C�ش�);
            this.tbQuery.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);

            this.tbReset.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbReset.Visible = true;
            this.tbSetting.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S����);
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
        /// ��ʼ����������
        /// </summary>
        private void InitOrderPrint()
        {
            #region ���Ʋ���

            //����ҽ�Ʋ�������ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
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

            #region ��ӡ��������

            //��ȡϵͳ��ӡ���б�
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                //ȡ��ӡ����
                string name = System.Text.RegularExpressions.Regex.Replace(PrinterSettings.InstalledPrinters[i], @"��(\s|\S)*��", "").Replace("�Զ�", "");
                this.tbPrinter.Items.Add(name);
            }

            //��ӡ��ʽ�б�
            this.cmbPrintType.AddItems(Neusoft.FrameWork.Public.EnumHelper.Current.EnumArrayList<EnumPrintType>());
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
                    this.leftValue = Neusoft.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuLeft.Value = this.leftValue;

                node = file.SelectSingleNode("ORDERPRINT/�ϱ߾�");
                if (node != null)
                {
                    this.topValue = Neusoft.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuTop.Value = this.topValue;

                node = file.SelectSingleNode("ORDERPRINT/����");
                if (node != null)
                {
                    this.rowNum = Neusoft.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuRowNum.Value = this.rowNum;

                node = file.SelectSingleNode("ORDERPRINT/Ԥ����ӡ");
                if (node != null)
                {
                    cbxPreview.Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                }

                node = file.SelectSingleNode("ORDERPRINT/��ӡ��ʽ");
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

            #region ��������



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
            this.SetPrintValue("Ԥ����ӡ", "1");
        }

        void cmbPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPrintType.Text))
            {
                this.SetPrintValue("��ӡ��ʽ", this.cmbPrintType.Text);
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

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.BeginDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.BeginDate).Text = " ��   ʼ ";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.BeginDate).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.BeginTime).Text = "ʱ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.RecipeDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.RecipeDoct).Text = "ҽʦǩ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ConfirmNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ConfirmNurse).Text = "��ʿǩ��";

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

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDate).Text = "ͣ   ֹ";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCDate).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, (Int32)LongOrderColunms.DCTime).Text = "ʱ��";

            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDoct).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCDoct).Text = "ҽʦǩ��";
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCConfirmNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.DCConfirmNurse).Text = "��ʿǩ��";


            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ExecNurse).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)LongOrderColunms.ExecNurse).Text = "ִ��";


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

            #region ����ÿ�еĸ߶�

            //ҽ�����ĸ߶�
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

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.CombFlag).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.CombFlag).Text = "��";
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

            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmDate).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, (Int32)ShortOrderColunms.ConfirmDate).Text = "ִ��ʱ��";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.ConfirmDate).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, (Int32)ShortOrderColunms.ConfirmTime).Text = "ʱ��";

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

            #region ����ÿ�еĸ߶�

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

            //���ǹ���Ա�������ش�ť
            if (!((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.tbReset.Visible = false;
            }
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

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");
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
                        //����ҽ��
                        alLong.Add(obj);
                    }
                    else
                    {
                        //��ʱҽ��
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
            //    MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //}
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

            //������������ҽ��
            int moState = -1;

            foreach (Neusoft.HISFC.Models.Order.Inpatient.Order inOrder in alOrder)
            {
                if (inOrder.RowNo >= this.rowNum)
                {
                    MessageBox.Show(inOrder.OrderType.Name + "��" + inOrder.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (inOrder.RowNo + 1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                }

                if (string.IsNullOrEmpty(deptCode))
                {
                    deptCode = inOrder.ReciptDept.ID;
                }

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
                            if (MessageBox.Show("��������ҽ�����Ƿ��Զ���ҳ��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
                        this.fpShortOrder.Sheets[MaxPageNo].SheetName = "��" + (MaxPageNo + 1).ToString() + "ҳ";
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
        /// ÿ�����
        /// </summary>
        /// <param name="isLong"></param>
        /// <param name="isPint"></param>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        /// <param name="inOrder"></param>
        private void AddToRow(bool isLong, bool isPint, FarPoint.Win.Spread.SheetView sheet, int row,Neusoft.HISFC.Models.Order.Inpatient.Order inOrder)
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
        /// ��ӵ�����Fp
        /// </summary>
        /// <param name="al"></param>
        [Obsolete("����", true)]
        private void AddObjectToFpLong(ArrayList alLongOrder)
        {
            #region Ϊ�շ���
            if (alLongOrder.Count <= 0)
            {
                return;
            }
            #endregion

            #region �������

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = -1;
            int MaxRowNo = -1;

            #endregion

            #region ���Ƿ��ӡ����

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

            #region ���Ѵ�ӡ����ʾ

            for (int i = 0; i < alPageNo.Count; i++)
            {
                int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(alPageNo[i].ToString());

                if (pageNo > MaxPageNo)
                {
                    MaxPageNo = pageNo;
                    MaxRowNo = -1;
                }

                ArrayList alTemp = hsPageNo[pageNo] as ArrayList;

                #region �Ѵ�ӡ�ĵ�һҳ
                if (i == 0)
                {
                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo+1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
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
                    this.fpLongOrder.Sheets[0].SheetName = "��" + (pageNo + 1).ToString() + "ҳ";
                }
                #endregion

                #region ����ҳ
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                    this.InitLongSheet(sheet);
                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);

                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo+1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
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
                    sheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
                }
                #endregion
            }
            #endregion

            #region ��ʾδ��ӡҽ��

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
            activeSheet.SheetName = "��" + MaxPageNo.ToString() + "ҳ";

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

            //��ǰ��
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
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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

            #region �����ƴ�ӡ����

            if (isShiftDeptNextPag)
            {
                //��Ҫ����
                string deptCode = "";
                //��������һ�ܺ���Զ���ҳ
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

            #region ����ҽ����ҳ


            //��Ҫ����
            if (this.alRecord.Count >= 0)
            {
                //��������һ�ܺ���Զ���ҳ
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
                                if (MessageBox.Show("ϵͳ�ҵ�����ҽ����¼��ʱ��:" + ext.PropertyCode + "���Ƿ��ҳ��", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
        /// ��ȡ��ʾ��ҽ������
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isLong"></param>
        /// <returns></returns>
        private string GetOrderItem(Neusoft.HISFC.Models.Order.Inpatient.Order order, bool isLong)
        {
            string itemName = "";
            if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                //�Ƿ���ʾͨ����������
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
                //    //itemName = order.Item.Name + " " + order.DoseOnceDisplay + order.DoseUnit;  // mb ����дͨ��������ȡ��������
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
                addStr = "(��Ժ��ҩ)";
            }
            else if (order.OrderType.ID == "BL")
            {
                addStr = "(��¼ҽ��)";
            }

            string qty = "";
            if (!order.OrderType.IsDecompose && order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                qty = order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + order.Unit;
            }

            //�������׷�ҩ��Ŀ����ʾ�������
            //Neusoft.SOC.HISFC.Fee.Models.Undrug oneUndrug = undrugManager.GetUndrug(order.Item.ID);
            Neusoft.HISFC.Models.Fee.Item.Undrug oneUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
            if ( oneUndrug != null && oneUndrug.UnitFlag.Equals("1"))
            {
                itemName += " (������)";
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

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            if (this.fpLongOrder.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.fpLongOrder.Sheets[sheet].Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

            MessageBox.Show("����ҽ�����ҳ����ע�⣡", "��ʾ");

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
            activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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
        /// ��ӵ�Fp
        /// </summary>
        /// <param name="al"></param>
        [Obsolete("����", true)]
        private void AddObjectToFpShort(ArrayList al)
        {
            #region Ϊ�շ���
            if (al.Count <= 0)
            {
                return;
            }
            #endregion

            #region �������
            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = -1;
            int MaxRowNo = -1;
            #endregion

            #region ���Ƿ��ӡ����
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

            #region ���Ѵ�ӡ����ʾ
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
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo + 1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
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
                    this.fpShortOrder.Sheets[0].SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
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
                            MessageBox.Show(o.OrderType.Name + "��" + o.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + o.RowNo.ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
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
                    sheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
                }
            }

            #endregion

            #region ��ʾδ��ӡҽ��

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
            activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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

            #region �����ƴ�ӡ����

            if (isShiftDeptNextPag)
            {
                //��Ҫ����
                string deptCode = "";
                //��������һ�ܺ���Զ���ҳ
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

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            //int row = this.fpShortOrder.Sheets[sheet].ActiveRowIndex;

            if (this.fpShortOrder.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.fpShortOrder.Sheets[sheet].Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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
            activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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

                            //p.ShowPrintPageDialog();
                            
                            //�����Ƿ����Ա������Ԥ������ 
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
                        //Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                        //��Ժ��ӡʱ�������ҳ��ӡ
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
                        Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                        //��Ժ��ӡʱ�������ҳ��ӡ
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
                        else  //�ǳ�Ժ��ӡ
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

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

                Neusoft.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.ActiveSheet.Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag
                    as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

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
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            if (this.fpLongOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = sheetIndex; //Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
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
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() ,"2", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
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
        /// ����ҽ��ҳ�����ȡ��־
        /// </summary>
        /// <param name="chgBed"></param>
        /// <returns></returns>
        private int UpdateOrderForShort(int sheetIndex)
        {
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            

            if (this.fpShortOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = sheetIndex;//Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
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
                        MessageBox.Show("ʵ��ת������");
                        return -1;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //if (myOrder.UpdatePageNoAndRowNo(this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        if (myOrder.UpdatePageRowNoAndGetflag(
                            this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() , "0", "0") <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }
                        
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            //if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            if (myOrder.UpdatePageRowNoAndGetflag(
                                this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString() ,"2", "0") <= 0)
                            { 
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
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
        /// �õ���ϴ����������ʾ
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
        /// �õ���ϴ����������ʾ
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
        /// ����ҳ��Ŵ�ӡ
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
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
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
                                    Neusoft.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)LongOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
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
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
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
                                    Neusoft.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag
                                        as Neusoft.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as Neusoft.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "��");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as Neusoft.HISFC.Models.Order.Inpatient.Order).Combo.ID)
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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return;
                    }
                }
            }

            this.lblPageLong.Visible = true;
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

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return;
                    }

                    //this.fpShortOrder.ActiveSheet.SetValue(i,2,"");
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//ִ�����ڲ���
                    //this.fpShortOrder.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//ִ��ʱ�䲻��
                    //this.fpShortOrder.ActiveSheet.SetValue(i,8,"");//ִ��ʱ�䲻��
                }
            }

            this.lblPageShort.Visible = true;
        }

        /// <summary>
        /// ��ȡSheet��һ��ҽ��
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
        /// ����Fp��ʽ
        /// </summary>
        /// <param name="longOrder">�Ƿ���</param>
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

        #region ����ҽ��

        /// <summary>
        /// ���õ�ǰ����ҽ������ӡ״̬
        /// </summary>
        private void ReSet(EnumOrderType type)
        {
            if (this.myPatientInfo == null || this.myPatientInfo.ID == "")
            {
                return;
            }

            string message = "ҽ����";
            string orderType = "ALL";
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

            DialogResult rr = MessageBox.Show("����ȡ������" + message + "�Ĵ�ӡ״̬���Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.orderManager.ResetOrderPrint("-1", "-1", myPatientInfo.ID, orderType, "0") == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ��!" + this.orderManager.Err);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();

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
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("סԺ�Ŵ���û���ҵ��û���", 111);
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

            //���ﲻ��ʹ��
            //���ó���
            else if (e.ClickedItem == this.ResetLong)
            {
                this.ReSet(EnumOrderType.Long);
            }
            //��������
            else if (e.ClickedItem == this.ResetShort)
            {
                this.ReSet(EnumOrderType.Short);
            }
            //����ȫ��ҽ��
            else if (e.ClickedItem == this.ResetAll)
            {
                this.ReSet(EnumOrderType.All);
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
        /// ������߾�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuLeft_ValueChanged(object sender, EventArgs e)
        {
            this.leftValue = Neusoft.FrameWork.Function.NConvert.ToInt32(this.nuLeft.Value);
            this.SetPrintValue("��߾�", this.leftValue.ToString());
        }

        /// <summary>
        /// �����ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuRowNum_ValueChanged(object sender, EventArgs e)
        {
            this.rowNum = Neusoft.FrameWork.Function.NConvert.ToInt32(this.nuRowNum.Value);
            this.SetPrintValue("����", this.rowNum.ToString());
        }

        /// <summary>
        /// �ϱ߾�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nuTop_ValueChanged(object sender, EventArgs e)
        {
            this.topValue = Neusoft.FrameWork.Function.NConvert.ToInt32(this.nuTop.Value);
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
        /// �����л���ʾҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpLongOrder_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            if (this.fpLongOrder.Sheets.Count > 0 &&
                this.fpLongOrder.ActiveSheet.Tag != null)
            {
                this.lblPageLong.Text = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag) + 1).ToString() + "ҳ";

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
                this.lblPageShort.Text = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag) + 1).ToString() + "ҳ";

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

                System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                printDateItem.Text = "ֻ�����������ҽ��ֹͣʱ��";
                printDateItem.Click += new EventHandler(printDateItem_Click);

                System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                splitMenuItem.Text = "�Ӹ���ҽ����������һҳ";
                splitMenuItem.Click += new EventHandler(splitMenuItem_Click);

                this.popMenu.MenuItems.Add(printMenuItem);
                this.popMenu.MenuItems.Add(printDateItem);
                this.popMenu.MenuItems.Add(splitMenuItem);
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

                System.Windows.Forms.MenuItem splitShortMenuItem = new MenuItem();
                splitShortMenuItem.Text = "�Ӹ���ҽ����������һҳ";
                splitShortMenuItem.Click += new EventHandler(splitShortMenuItem_Click);

                this.popMenu.MenuItems.Add(printMenuItem);
                this.popMenu.MenuItems.Add(splitShortMenuItem);
                this.popMenu.Show(this.fpShortOrder, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// ��ʱҽ����ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitShortMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpShortAfter(this.fpShortOrder.ActiveSheetIndex, this.fpShortOrder.ActiveSheet.ActiveRowIndex);
        }

        /// <summary>
        /// ����ҽ����ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpLongAfter(this.fpLongOrder.ActiveSheetIndex, this.fpLongOrder.ActiveSheet.ActiveRowIndex);
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
    /// ��ӡ��ʽ
    /// </summary>
    public enum EnumPrintType
    {
        /// <summary>
        /// ��ֽ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("��ֽ����")]
        WhitePaperContinue = 0,

        /// <summary>
        /// ӡˢ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("ӡˢ����")]
        DrawPaperContinue = 1,

        /// <summary>
        /// ��Ժ��ӡ
        /// </summary>
        [Neusoft.FrameWork.Public.Description("��Ժ��ӡ")]
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
    /// ����ҽ��������
    /// </summary>
    public enum LongOrderColunms
    {
        /// <summary>
        /// ��ʼ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("����")]
        BeginDate = 0,

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        [Neusoft.FrameWork.Public.Description("ʱ��")]
        BeginTime,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        [Neusoft.FrameWork.Public.Description("����ҽ��")]
        RecipeDoct,

        /// <summary>
        /// ��˻�ʿ
        /// </summary>
        [Neusoft.FrameWork.Public.Description("��˻�ʿ")]
        ConfirmNurse,

        /// <summary>
        /// ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("��")]
        CombFlag,

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("����ҽ��")]
        ItemName,

        /// <summary>
        /// ÿ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("ÿ����")]
        OnceDose,

        /// <summary>
        /// Ƶ��
        /// </summary>
        [Neusoft.FrameWork.Public.Description("Ƶ��")]
        Freq,

        /// <summary>
        /// �÷�
        /// </summary>
        [Neusoft.FrameWork.Public.Description("�÷�")]
        Usage,

        /// <summary>
        /// ��ע
        /// </summary>
        [Neusoft.FrameWork.Public.Description("��ע")]
        Memo,

        /// <summary>
        /// ֹͣ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("����")]
        DCDate,

        /// <summary>
        /// ֹͣʱ��
        /// </summary>
        [Neusoft.FrameWork.Public.Description("ʱ��")]
        DCTime,

        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        [Neusoft.FrameWork.Public.Description("ֹͣҽ��")]
        DCDoct,

        /// <summary>
        /// ֹͣ��˻�ʿ
        /// </summary>
        [Neusoft.FrameWork.Public.Description("��˻�ʿ")]
        DCConfirmNurse,

        /// <summary>
        /// ִ�л�ʿ
        /// </summary>
        [Neusoft.FrameWork.Public.Description("ִ�л�ʿ")]
        ExecNurse,

        /// <summary>
        /// ���
        /// </summary>
        [Neusoft.FrameWork.Public.Description("���")]
        CombNO,

        /// <summary>
        /// ������
        /// </summary>
        [Neusoft.FrameWork.Public.Description("����")]
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
        [Neusoft.FrameWork.Public.Description("ÿ����")]
        OnceDose,

        /// <summary>
        /// Ƶ��
        /// </summary>
        [Neusoft.FrameWork.Public.Description("Ƶ��")]
        Freq,

        /// <summary>
        /// �÷�
        /// </summary>
        [Neusoft.FrameWork.Public.Description("�÷�")]
        Usage,

        /// <summary>
        /// ����
        /// </summary>
        [Neusoft.FrameWork.Public.Description("����")]
        Qty,


        /// <summary>
        /// ��ע
        /// </summary>
        [Neusoft.FrameWork.Public.Description("��ע")]
        Memo,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        RecipeDoct,

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