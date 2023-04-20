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

namespace FS.SOC.Local.Order.OrderPrint
{
    /// <summary>
    /// ҽ������ӡ������
    /// </summary>
    public partial class frmOrderPrint : FS.FrameWork.WinForms.Forms.BaseStatusBar, FS.HISFC.BizProcess.Interface.IPrintOrder
    {
        public frmOrderPrint()
        {
            InitializeComponent();
        }

        #region ����

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.Pharmacy.Item phaMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
        FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Manager.Person PersonManger = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ҩƷ������Ϣ������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper phaItemHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        System.Windows.Forms.ContextMenu popMenu = new ContextMenu();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        Function funMgr = new Function();

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
        /// �����Ƿ���ʾƵ��
        /// </summary>
        private bool isShowFreqShortOrder = true;

        /// <summary>
        /// �Ƿ�ת���Զ���ҳ
        /// </summary>
        private bool isShiftDeptNextPag = false;

        string pathNameLongOrderPrint = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\LongOrderPrintSetting.xml";

        string pathNameShortOrderPrint = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\ShortOrderPrintSetting.xml";

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
        private string printType = "��Ժ��ӡ";

        /// <summary>
        /// ��ӡ��ʽ
        /// </summary>
        private PrintType OrderPrintType = PrintType.PrintWhenPatientOut;

        #endregion

        #region ��Ա��ӡģʽ

        /// <summary>
        /// ������Ա��ӡģʽ��0 �մ�ӡ��1 ���Դ�ӡ��
        /// ����ҽ����ִ�л�ʿ��ֹͣҽ����ֹͣ��˻�ʿ
        /// ���磺0000
        /// </summary>
        private string operPrintMode = "";

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

        /// <summary>
        /// �Ƿ������ҳ��ӡ �磺������ҽ��
        /// </summary>
        private bool isCanIntervalPrint = false;

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

            this.ucOrderBillHeader1.Header = "��  ʱ  ҽ  ��  ��";
            this.ucOrderBillHeader2.Header = "��  ��  ҽ  ��  ��";
            ArrayList alPha = new ArrayList(this.phaMgr.QueryItemAvailableList());
            //this.phaItemHelper.ArrayObject = alPha;

            InitOrderPrint();

            //this.InitLongSheet(this.fpLongOrder.Sheets[0]);
            //this.InitShortSheet(this.fpShortOrder.Sheets[0]);

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

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);

            if (((FS.HISFC.Models.Base.Employee)phaMgr.Operator).IsManager)
            {
                this.cmbPrintType.Enabled = true;
            }

            this.tbReset.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

            QueryPatientOrder();
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void InitOrderPrint()
        {
            //����ҽ�Ʋ�������ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
            try
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

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

            //��ȡϵͳ��ӡ���б�
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                //ȡ��ӡ����
                string name = System.Text.RegularExpressions.Regex.Replace(PrinterSettings.InstalledPrinters[i], @"��(\s|\S)*��", "").Replace("�Զ�", "");
                this.tbPrinter.Items.Add(name);
            }

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);

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

                node = file.SelectSingleNode("ORDERPRINT/��ӡ��ʽ");
                if (node != null)
                {
                    this.printType = node.InnerText;
                }
                this.cmbPrintType.Text = this.printType;

                if (this.printType == "��ֽ�״�")
                {
                    this.OrderPrintType = PrintType.WhitePaperContinue;
                }
                else if (this.printType == "ӡˢ�״�")
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
            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, false, true, true);

            for (int i = 0; i < this.rowNum; i++)
            {
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells.Get(i, j).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
            }

            #region ����
            sheet.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 0).Text = " ��   ʼ ";
            sheet.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).Text = "��";
            sheet.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 3).Text = " ҽ �� �� �� ";
            sheet.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 4).Text = "����";
            sheet.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 5).Text = "��λ";
            sheet.ColumnHeader.Cells.Get(0, 6).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 6).Text = "ҽ��";

            sheet.ColumnHeader.Cells.Get(0, 7).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 7).Text = "��ʿ";

            sheet.ColumnHeader.Cells.Get(0, 8).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 8).Text = " ͣ   ֹ ";

            sheet.ColumnHeader.Cells.Get(0, 10).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 10).Text = "ҽ��";
            sheet.ColumnHeader.Cells.Get(0, 11).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 11).Text = "��ʿ";

            sheet.ColumnHeader.Cells.Get(1, 0).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, 1).Text = "ʱ��";

            sheet.ColumnHeader.Cells.Get(1, 8).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, 9).Text = "ʱ��";
            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;
            sheet.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            sheet.Columns.Get(0).Label = "����";
            sheet.Columns.Get(0).Width = 64F;
            sheet.Columns.Get(1).Label = "ʱ��";

            #endregion

            sheet.Columns.Get((Int32)LongOrderColunms.Qty).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;

            textCellType1.WordWrap = true;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).CellType = textCellType1;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Columns.Get((Int32)LongOrderColunms.BeginDate).Width = 45F;
            sheet.Columns.Get((Int32)LongOrderColunms.BeginTime).Width = 45F;
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
            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, false, true, true);

            for (int i = 0; i < this.rowNum; i++)
            {
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells.Get(i, j).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
            }

            #region ����

            sheet.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 0).Text = " ��  �� ";
            sheet.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).Text = "��";
            sheet.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 3).Text = "ҽ������";

            sheet.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 4).Text = "����";

            sheet.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 5).Text = "��λ";

            sheet.ColumnHeader.Cells.Get(0, 6).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 6).Text = "ҽ��";

            sheet.ColumnHeader.Cells.Get(0, 7).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 7).Text = "��ʿ";

            sheet.ColumnHeader.Cells.Get(0, 8).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 8).Text = "ִ��ʱ��";

            sheet.ColumnHeader.Cells.Get(0, 10).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 10).Text = "ȡ��";

            sheet.ColumnHeader.Cells.Get(0, 11).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 11).Text = "ҽ��";

            sheet.ColumnHeader.Cells.Get(1, 0).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, 1).Text = "ʱ��";
            sheet.ColumnHeader.Cells.Get(1, 8).Text = "����";
            sheet.ColumnHeader.Cells.Get(1, 9).Text = "ʱ��";

            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;

            #endregion

            sheet.Columns.Get((Int32)ShortOrderColunms.BeginTime).Label = "ʱ��";

            textCellType2.WordWrap = true;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).CellType = textCellType2;

            //sheet.Columns.Get((Int32)ShortOrderColunms.Unit).Locked = false;

            //sheet.Columns.Get(6).Label = "����";

            //sheet.Columns.Get(7).Label = "ʱ��";

            sheet.Columns.Get((Int32)ShortOrderColunms.CombNO).Visible = false;
            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            sheet.Columns.Get((Int32)ShortOrderColunms.BeginDate).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.BeginTime).Width = 45F;
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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");
            Application.DoEvents();

            try
            {
                ArrayList alAll = new ArrayList();

                alAll = this.orderManager.QueryPrnOrder(this.myPatientInfo.ID);

                alLong.Clear();
                alShort.Clear();

                FS.HISFC.Models.Order.Inpatient.Order order = null;
                foreach (object obj in alAll)
                {
                    order = obj as FS.HISFC.Models.Order.Inpatient.Order;

                    order.Nurse.Name = PersonManger.GetPersonByID(order.Nurse.ID).Name;
                    order.ReciptDoctor.Name = order.ReciptDoctor.Name;
                    if (order.DCNurse.ID != null && order.DCNurse.ID != "")
                    {
                        order.DCNurse.Name = PersonManger.GetPersonByID(order.DCNurse.ID).Name;
                    }

                    order.Item.Name += funMgr.TransHypotest(order.Item.ID, order.HypoTest);

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

                //for (int i = 0; i < this.fpLongOrder.Sheets.Count; i++)
                //{
                //    for (int j = 0; j < this.fpLongOrder.Sheets[i].Rows.Count; j++)
                //    {
                //        this.fpLongOrder.Sheets[i].Rows[j].BackColor = Color.White;
                //    }
                //}

                //for (int i = 0; i < this.fpShortOrder.Sheets.Count; i++)
                //{
                //    for (int j = 0; j < this.fpShortOrder.Sheets[i].Rows.Count; j++)
                //    {
                //        this.fpShortOrder.Sheets[i].Rows[j].BackColor = Color.White;
                //    }
                //}

                FS.FrameWork.Management.ExtendParam myInpatient = new FS.FrameWork.Management.ExtendParam();

                this.alRecord = myInpatient.GetComExtInfoListByPatient(FS.HISFC.Models.Base.EnumExtendClass.PATIENT, myPatientInfo.ID);

                this.Clear();

                //this.AddObjectToFpLong(alLong);
                this.AddOrderToFP(true, alLong);
                this.AddOrderToFP(false,alShort);

                //this.AddObjectToFpShort(alShort);

                this.lblPageLong.Visible = true;
                this.lblPageShort.Visible = true;

                this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));
                this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
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
            //if (this.fpLongOrder.Sheets.Count > 1)
            //{
            //    for (int j = this.fpLongOrder.Sheets.Count - 1; j >= 0; j--)
            //    {
            //        this.fpLongOrder.Sheets.RemoveAt(j);
            //    }
            //}

            //this.fpLongOrder.Sheets[0].Columns.Count = (Int32)LongOrderColunms.Max;
            //this.fpLongOrder.Sheets[0].RowCount = 0;
            //this.fpLongOrder.Sheets[0].RowCount = this.rowNum;

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
                Function.DrawCombo1(this.fpLongOrder.Sheets[i], (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
            }

            for (int i = 0; i < this.fpShortOrder.Sheets.Count; i++)
            {
                Function.DrawCombo1(this.fpShortOrder.Sheets[i], (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
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


                if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                   

                    //�Ƿ���ʾͨ����������
                    if (isDisplayRegularName)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID);
                        if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                        {
                            sheet.SetValue(row, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + inOrder.Frequency.ID + " " + inOrder.Usage.Name + " " + inOrder.Memo);
                        }
                        else
                        {
                            sheet.SetValue(row, (Int32)LongOrderColunms.ItemName, inOrder.Item.Name + " " + inOrder.Frequency.ID + " " + inOrder.Usage.Name + " " + inOrder.Memo);
                        }
                    }
                    else
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.ItemName, inOrder.Item.Name + " " + inOrder.Frequency.ID + " " + inOrder.Usage.Name + " " + inOrder.Memo);
                    }

                    if (!string.IsNullOrEmpty(inOrder.DoseOnceDisplay))
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Qty, inOrder.DoseOnceDisplay);
                    }
                    else
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Qty, inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.'));
                    }

                    if (!string.IsNullOrEmpty(inOrder.DoseUnitDisplay))
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Unit, inOrder.DoseUnitDisplay);
                    }
                    else
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Unit, (inOrder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                }
                else
                {
                    sheet.SetValue(row, (Int32)LongOrderColunms.ItemName, inOrder.Item.Name + " " + inOrder.Frequency.ID + " " + inOrder.Memo);

                    if (inOrder.Item.ID != "999")
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Qty, inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.'));
                        sheet.SetValue(row, (Int32)LongOrderColunms.Unit, inOrder.Item.PriceUnit);
                    }
                }

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


                if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //�Ƿ���ʾͨ����������
                    if (isDisplayRegularName)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID);
                        if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                        {
                            sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + inOrder.Frequency.ID + " " + inOrder.Usage.Name + " " + inOrder.Memo);
                        }
                        else
                        {
                            sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, inOrder.Item.Name + " " + inOrder.Frequency.ID + " " + inOrder.Usage.Name + " " + inOrder.Memo);
                        }
                    }
                    else
                    {
                        sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, inOrder.Item.Name + " " + inOrder.Frequency.ID + " " + inOrder.Usage.Name + " " + inOrder.Memo);
                    }

                    if (!string.IsNullOrEmpty(inOrder.DoseOnceDisplay))
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Qty, inOrder.DoseOnceDisplay);
                    }
                    else
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Qty, inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.'));
                    }

                    if (!string.IsNullOrEmpty(inOrder.DoseUnitDisplay))
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Unit, inOrder.DoseUnitDisplay);
                    }
                    else
                    {
                        sheet.SetValue(row, (Int32)LongOrderColunms.Unit, (inOrder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                }
                else
                {
                    if (isShowFreqShortOrder)
                    {
                        sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, inOrder.Item.Name + " " + inOrder.Frequency.ID + " " + inOrder.Memo);
                    }
                    else
                    {
                        sheet.SetValue(row, (Int32)ShortOrderColunms.ItemName, inOrder.Item.Name + " " + inOrder.Memo);
                    }

                    if (inOrder.Item.ID != "999")
                    {
                        sheet.SetValue(row, (Int32)ShortOrderColunms.Qty, inOrder.Qty.ToString("F4").TrimEnd('0').TrimEnd('.'));
                        sheet.SetValue(row, (Int32)ShortOrderColunms.Unit, inOrder.Item.PriceUnit);
                    }
                }

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
                }

                sheet.Rows[row].Tag = inOrder;
            }
            #endregion
        }

        /// <summary>
        /// ��ӵ�����Fp
        /// </summary>
        /// <param name="al"></param>
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

            foreach (FS.HISFC.Models.Order.Inpatient.Order temp in alLongOrder)
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
                int pageNo = FS.FrameWork.Function.NConvert.ToInt32(alPageNo[i].ToString());

                if (pageNo > MaxPageNo)
                {
                    MaxPageNo = pageNo;
                    MaxRowNo = -1;
                }

                ArrayList alTemp = hsPageNo[pageNo] as ArrayList;

                #region �Ѵ�ӡ�ĵ�һҳ
                if (i == 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo+1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        this.fpLongOrder.Sheets[0].Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginDate, GetPrintDate(order).Month.ToString() + "-" + GetPrintDate(order).Day.ToString());
                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginTime, GetPrintDate(order).ToShortTimeString());


                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                                else
                                {
                                    this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                            }
                            else
                            {
                                this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                            }

                            this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.DoseOnce.ToString());
                            this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                        }
                        else
                        {
                            //???Ŀ�ĺ��ڣ�
                            if (order.Item.SysClass.ID.ToString() != "UN" && order.Item.SysClass.ID.ToString() != "MF")
                            {
                                this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Memo);
                            }
                            else
                            {
                                if (order.Item.Name.IndexOf("����") < 0 && order.Item.Name.IndexOf("ʳ") < 0)
                                {
                                    this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Memo);
                                }
                                else
                                {
                                    this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Memo);
                                }
                            }

                            if (order.Item.ID != "999")
                            {
                                this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.Qty.ToString());
                                this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, order.Item.PriceUnit);
                            }
                        }

                        this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.RecipeDoct, order.ReciptDoctor.Name);
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

                    Function.DrawCombo1(this.fpLongOrder.Sheets[0], (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);

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

                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo+1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[order.RowNo].BackColor = Color.White;
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.BeginDate, GetPrintDate(order).Month.ToString() + "-" + GetPrintDate(order).Day.ToString());
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.BeginTime, GetPrintDate(order).ToShortTimeString());

                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                            }
                            else
                            {
                                sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                            }

                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.DoseOnce.ToString());
                            sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                        }
                        else
                        {
                            if (order.Item.SysClass.ID.ToString() != "UN" && order.Item.SysClass.ID.ToString() != "MF")
                            {
                                sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Memo);
                            }
                            else
                            {
                                if (order.Item.Name.IndexOf("����") < 0 && order.Item.Name.IndexOf("ʳ") < 0)
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Frequency.ID + " " + order.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + order.Memo);
                                }
                            } 
                            
                            if (order.Item.ID != "999")
                            {
                                this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Qty, order.Qty.ToString());
                                this.fpLongOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.Unit, order.Item.PriceUnit);
                            }
                        }

                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.RecipeDoct, order.ReciptDoctor.Name);
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
                    sheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
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
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                if (fromOne)
                {
                    oTemp = alPageNull[iniIndex - 1] as FS.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;
                }

                activeRow = (iniIndex + MaxRowNo) % rowNum;
                if (activeRow == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(sheet);

                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginDate, this.GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());


                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }

                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
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

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                         //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }

                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
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

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                        FS.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.Sheets[i].Rows[j].Tag as
                            FS.HISFC.Models.Order.Inpatient.Order;

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
                        FS.HISFC.Models.Order.Inpatient.Order order = this.fpLongOrder.Sheets[i].Rows[j].Tag as
                            FS.HISFC.Models.Order.Inpatient.Order;

                        if (order == null)
                            continue;

                        if (order.PageNo >= 0)
                            continue;

                        foreach (FS.HISFC.Models.Base.ExtendInfo ext in alRecord)
                        {
                            if (GetPrintDate(order) >= FS.FrameWork.Function.NConvert.ToDateTime(ext.PropertyCode))
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
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
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
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Qty, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.Unit, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.RecipeDoct, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ConfirmNurse, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, 8, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, 9, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, 10, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, 11, "");
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

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                         //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);

                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + "  " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + "  " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }

                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo);
                            }
                        }
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName);
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name);
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name);
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                        }
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo);
                            }
                        }
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
        /// ��ӵ�Fp
        /// </summary>
        /// <param name="al"></param>
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
            foreach (FS.HISFC.Models.Order.Inpatient.Order temp in al)
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
                int pageNo = FS.FrameWork.Function.NConvert.ToInt32(alPageNo[i].ToString());

                if (FS.FrameWork.Function.NConvert.ToInt32(pageNo) > MaxPageNo)
                {
                    MaxPageNo = FS.FrameWork.Function.NConvert.ToInt32(pageNo);
                    MaxRowNo = -1;
                }

                ArrayList alTemp = hsPageNo[pageNo] as ArrayList;

                if (i == 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {
                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo + 1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        this.fpShortOrder.Sheets[0].Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginDate, GetPrintDate(order).Month.ToString() + "-" + GetPrintDate(order).Day.ToString());
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)LongOrderColunms.BeginTime, GetPrintDate(order).ToShortTimeString());

                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (order.OrderType.ID == "CD")
                                    {
                                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + order.Frequency.ID + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + order.Memo);

                                    }
                                    else if (order.OrderType.ID == "BL")
                                    {
                                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + order.Memo);
                                    }
                                    else
                                    {
                                        //o.DoseOnce.ToString()+(o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit+" "
                                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Memo);
                                    }
                                }
                                else
                                {
                                    if (order.OrderType.ID == "CD")
                                    {
                                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.Frequency.ID + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + order.Memo);
                                    }
                                    else if (order.OrderType.ID == "BL")
                                    {
                                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + order.Memo);
                                    }
                                    else
                                    {
                                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Memo);
                                    }
                                }
                            }
                            else
                            {
                                if (order.OrderType.ID == "CD")
                                {
                                    this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.Frequency.ID + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + order.Memo);
                                }
                                else if (order.OrderType.ID == "BL")
                                {
                                    this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + order.Memo);
                                }
                                else
                                {
                                    this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Memo);
                                }
                            }

                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Qty, order.Qty.ToString());
                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Unit, order.Unit);
                        }
                        else
                        {
                            if (order.Item.MinFee.ID == "037" || order.Item.MinFee.ID == "038" || order.Item.Name.IndexOf("��Ƥ") >= 0)
                            {
                                this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                            }
                            else
                            {
                                if (order.OrderType.ID == "BL")
                                {
                                    this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + "(��¼ҽ��)" + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                                }
                                else
                                {
                                    this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                                }
                            }

                            //��ҩƷҲ��ʾ�����͵�λ
                            if (order.Item.ID != "999")
                            {
                                this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Qty, order.Qty.ToString());
                                this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.Unit, order.Item.PriceUnit);
                            }
                        }

                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.RecipeDoct, order.ReciptDoctor.Name);
                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.ConfirmTime != DateTime.MinValue)
                        {
                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmDate, order.ConfirmTime.Month.ToString() + "-" + order.ConfirmTime.Day.ToString());
                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmTime, order.ConfirmTime.ToShortTimeString());
                        }

                        if (order.DCOper.OperTime != DateTime.MinValue)
                        {
                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.DCFlage, "DC");
                            this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.DCDoct, order.DCOper.Name);
                        }


                        this.fpShortOrder.Sheets[0].SetValue(order.RowNo, (Int32)ShortOrderColunms.CombNO, order.Combo.ID);
                        this.fpShortOrder.Sheets[0].Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Function.DrawCombo1(this.fpShortOrder.Sheets[0], (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);

                    this.fpShortOrder.Sheets[0].Tag = pageNo;
                    this.fpShortOrder.Sheets[0].SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
                }
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(sheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);

                    foreach (FS.HISFC.Models.Order.Inpatient.Order o in alTemp)
                    {
                        if (o.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(o.OrderType.Name + "��" + o.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + o.RowNo.ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[o.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(o).Month.ToString() + "-" + GetPrintDate(o).Day.ToString());
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(o).ToShortTimeString());

                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(o.Item.ID) as FS.HISFC.Models.Pharmacy.Item;
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (o.OrderType.ID == "CD")
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + o.Memo);
                                    }
                                    else if (o.OrderType.ID == "BL")
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + o.Usage.Name + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + o.Memo);
                                    }
                                    else
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + o.Usage.Name + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Memo);
                                    }
                                }
                                else
                                {
                                    if (o.OrderType.ID == "CD")
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + o.Memo);
                                    }
                                    else if (o.OrderType.ID == "BL")
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + o.Memo);
                                    }
                                    else
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Memo);
                                    }
                                }
                            }
                            else
                            {
                                if (o.OrderType.ID == "CD")
                                {
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + o.Memo);
                                }
                                else if (o.OrderType.ID == "BL")
                                {
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + o.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + o.DoseOnce.ToString() + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Memo);
                                }
                            }

                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Qty, o.Qty.ToString());
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Unit, o.Unit);
                        }
                        else
                        {
                            if (o.Item.MinFee.ID == "037" || o.Item.MinFee.ID == "038" || o.Item.Name.IndexOf("��Ƥ") >= 0)
                            {
                                sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency));
                            }
                            else
                            {
                                if (o.OrderType.ID == "BL")
                                {
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + "(��¼ҽ��)" + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency));
                                }
                                else
                                {
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency));
                                }
                            }

                            //��ҩƷҲ��ʾ�����͵�λ
                            if (o.Item.ID != "999")
                            {
                                this.fpShortOrder.Sheets[0].SetValue(o.RowNo, (Int32)ShortOrderColunms.Qty, o.Qty.ToString());
                                this.fpShortOrder.Sheets[0].SetValue(o.RowNo, (Int32)ShortOrderColunms.Unit, o.Item.PriceUnit);
                            }
                        }

                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.RecipeDoct, o.ReciptDoctor.Name);
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, o.Nurse.Name);

                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmDate, o.ConfirmTime.Month.ToString() + "-" + o.ConfirmTime.Day.ToString());
                            sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmTime, o.ConfirmTime.ToShortTimeString());
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
                    sheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
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
            activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

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
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                if (fromOne)
                {
                    oTemp = alPageNull[iniIndex - 1] as FS.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;
                }
                int activeRow = (iniIndex + MaxRowNo) % rowNum;
                if (activeRow == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(sheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Unit);
                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }

                        //��ҩƷҲ��ʾ�����͵�λ
                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Unit);

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                        FS.HISFC.Models.Order.Inpatient.Order order = this.fpShortOrder.Sheets[i].Rows[j].Tag as
                            FS.HISFC.Models.Order.Inpatient.Order;

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
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Qty, "");
                this.fpShortOrder.ActiveSheet.SetValue(j, (Int32)ShortOrderColunms.Unit, "");
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

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }
                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginDate, GetPrintDate(oTemp).Month.ToString() + "-" + GetPrintDate(oTemp).Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.BeginTime, GetPrintDate(oTemp).ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(oTemp.Item.ID);
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }


                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
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
                            if (this.OrderPrintType == PrintType.DrawPaperContinue || this.OrderPrintType == PrintType.WhitePaperContinue)
                            {
                                //���ñ������ʾ���ֵĿɼ���
                                this.ucOrderBillHeader2.SetVisible(false);
                                //������ͷ�Ŀɼ���
                                this.SetVisibleForLong(Color.White, false);
                                //������ʾ�ĸ�ʽ
                                SetStyleForFp(true, Color.White, BorderStyle.None);

                            }
                            else
                            {
                                //������ͷ�Ŀɼ���
                                this.SetVisibleForLong(Color.Black, false);
                                SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                            }

                            //�����ش���ʾ������
                            this.SetRePrintContentsForLong();

                            //������ʾ���߸ı���Ϣ
                            this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                            this.pnLongPag.Dock = DockStyle.None;

                            this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                            //p.ShowPrintPageDialog();
                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                            }
                            else
                            {
                                print.PrintPage(this.leftValue, this.topValue, this.pnLongOrder);
                            }
                            //p.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                            this.pnLongPag.Dock = DockStyle.Bottom;

                            //p.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);

                            this.ucOrderBillHeader2.SetVisible(true);

                            this.SetVisibleForLong(Color.Black, true);

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
                        this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                        if (this.OrderPrintType == PrintType.DrawPaperContinue || this.OrderPrintType == PrintType.WhitePaperContinue)
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
                    #endregion

                    this.pnLongPag.Dock = DockStyle.None;

                    this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);


                    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        print.PrintPreview(this.leftValue, this.topValue, this.pnLongOrder);
                    }
                    else
                    {
                        print.PrintPage(this.leftValue, this.topValue, this.pnLongOrder);
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
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //��Ժ��ӡʱ�������ҳ��ӡ
                        if (isCanIntervalPrint && this.OrderPrintType == PrintType.PrintWhenPatientOut)
                        {
                            for (int index = 0; index <= this.fpLongOrder.ActiveSheetIndex; index++)
                            {
                                if (this.UpdateOrderForLong(index) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();

                                    this.ucOrderBillHeader2.SetValueVisible(true);
                                    this.ucOrderBillHeader2.SetVisible(true);

                                    this.SetVisibleForLong(Color.Black, true);

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
                                FS.FrameWork.Management.PublicTrans.RollBack();

                                this.ucOrderBillHeader2.SetValueVisible(true);
                                this.ucOrderBillHeader2.SetVisible(true);

                                this.SetVisibleForLong(Color.Black, true);

                                pagNo = this.fpLongOrder.ActiveSheetIndex;
                                this.QueryPatientOrder();
                                this.fpLongOrder.ActiveSheetIndex = pagNo;

                                return;
                            }
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }

                    this.ucOrderBillHeader2.SetValueVisible(true);

                    this.ucOrderBillHeader2.SetVisible(true);

                    this.SetVisibleForLong(Color.Black, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    this.ucOrderBillHeader2.SetVisible(true);

                    this.ucOrderBillHeader2.SetValueVisible(true);

                    this.SetVisibleForLong(Color.Black, true);
                }

                pagNo = this.fpLongOrder.ActiveSheetIndex;
                this.QueryPatientOrder();
                this.fpLongOrder.ActiveSheetIndex = pagNo;
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

                            this.pnShortPag.Dock = DockStyle.None;

                            this.pnShortPag.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);

                            //p.ShowPrintPageDialog();
                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                            }
                            else 
                            {
                                print.PrintPage(this.leftValue, this.topValue, this.pnShortOrder);
                            }
                            //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                            this.pnShortPag.Dock = DockStyle.Bottom;
                            //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);

                            this.SetStyleForFp(false, Color.White, BorderStyle.None);

                            this.ucOrderBillHeader1.SetVisible(true);

                            this.SetVisibleForShort(Color.Black, true);

                            pagNo = this.fpShortOrder.ActiveSheetIndex;
                            this.QueryPatientOrder();
                            this.fpShortOrder.ActiveSheetIndex = pagNo;

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

                    this.pnShortPag.Dock = DockStyle.None;

                    this.pnShortPag.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);
                    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        print.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                    }
                    else 
                    {
                        print.PrintPage(this.leftValue, this.topValue, this.pnShortOrder);
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
                        if (isCanIntervalPrint && this.OrderPrintType == PrintType.PrintWhenPatientOut)
                        {
                            for (int index = 0; index <= this.fpShortOrder.ActiveSheetIndex; index++)
                            {
                                if (this.UpdateOrderForShort(index) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();

                                    this.ucOrderBillHeader1.SetValueVisible(true);
                                    this.ucOrderBillHeader1.SetVisible(true);

                                    this.SetVisibleForShort(Color.Black, true);

                                    pagNo = this.fpShortOrder.ActiveSheetIndex;
                                    this.QueryPatientOrder();
                                    this.fpShortOrder.ActiveSheetIndex = pagNo;

                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (this.UpdateOrderForShort(this.fpShortOrder.ActiveSheetIndex) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();

                                this.ucOrderBillHeader1.SetValueVisible(true);
                                this.ucOrderBillHeader1.SetVisible(true);

                                this.SetVisibleForShort(Color.Black, true);

                                pagNo = this.fpShortOrder.ActiveSheetIndex;
                                this.QueryPatientOrder();
                                this.fpShortOrder.ActiveSheetIndex = pagNo;

                                return;
                            }
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }

                    this.ucOrderBillHeader1.SetValueVisible(true);

                    this.ucOrderBillHeader1.SetVisible(true);

                    this.SetVisibleForShort(Color.Black, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.ucOrderBillHeader1.SetValueVisible(true);

                    this.ucOrderBillHeader1.SetVisible(true);

                    this.SetVisibleForShort(Color.Black, true);
                }

                pagNo = this.fpShortOrder.ActiveSheetIndex;
                this.QueryPatientOrder();
                this.fpShortOrder.ActiveSheetIndex = pagNo;
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
                        if (this.OrderPrintType == PrintType.DrawPaperContinue)
                        {
                            //���ñ������ʾ���ֵĿɼ���
                            this.ucOrderBillHeader2.SetVisible(false);
                            //������ͷ�Ŀɼ���
                            this.SetVisibleForLong(Color.White, false);
                            //������ʾ�ĸ�ʽ
                            SetStyleForFp(true, Color.White, BorderStyle.None);

                        }
                        else
                        {
                            //������ͷ�Ŀɼ���
                            this.SetVisibleForLong(Color.Black, false);
                            SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);
                        }

                        //�����ش���ʾ������
                        this.SetRePrintContentsForLong();

                        //������ʾ���߸ı���Ϣ
                        this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                        this.pnLongPag.Dock = DockStyle.None;

                        this.pnLongPag.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);
                        print.PrintPage(this.leftValue, this.topValue, this.pnLongOrder);
                        this.pnLongPag.Dock = DockStyle.Bottom;

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

                        this.pnShortPag.Dock = DockStyle.None;

                        this.pnShortPag.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum) - 15);
                        //this.neuPanel1.Location = new Point(this.fpShortOrder.Location.X, 910);
                        //p.ShowPrintPageDialog();
                        print.PrintPage(this.leftValue, this.topValue, this.pnShortOrder);
                        //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);
                        this.pnShortPag.Dock = DockStyle.Bottom;
                        //p.PrintPreview(this.leftValue, this.topValue, this.pnShortOrder);

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

                DialogResult r;

                r = MessageBox.Show("ȷ��Ҫ�ش���Ŀ:" + order.Item.Name, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                this.ucOrderBillHeader2.SetVisible(false);

                this.ucOrderBillHeader2.SetValueVisible(false);

                this.SetVisibleForLong(Color.White, false);

                this.lblPageLong.Visible = false;

                for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.fpLongOrder.ActiveSheet.ActiveRowIndex)
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                    else
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                }

                print.PrintPreview((Int32)ShortOrderColunms.CombNO, 24, this.pnLongOrder);

                this.ucOrderBillHeader2.SetValueVisible(true);

                this.ucOrderBillHeader2.SetVisible(true);

                this.SetVisibleForLong(Color.Black, true);

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

                FS.HISFC.Models.Order.Inpatient.Order order = this.fpShortOrder.ActiveSheet.Rows[this.fpShortOrder.ActiveSheet.ActiveRowIndex].Tag
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

                DialogResult r;

                r = MessageBox.Show("ȷ��Ҫ�ش���Ŀ:" + order.Item.Name, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                this.ucOrderBillHeader1.SetVisible(false);

                this.ucOrderBillHeader1.SetValueVisible(false);

                this.SetVisibleForShort(Color.White, false);

                this.lblPageShort.Visible = false;

                for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.fpShortOrder.ActiveSheet.ActiveRowIndex)
                    {
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                    }
                    else
                    {
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");//ִ�����ڲ���
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");//ִ��ʱ�䲻��
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");//ִ��ʱ�䲻��
                    }
                }

                print.PrintPreview(5, 16, this.pnShortOrder);

                this.ucOrderBillHeader1.SetValueVisible(true);

                this.ucOrderBillHeader1.SetVisible(true);

                this.SetVisibleForShort(Color.Black, true);

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

                this.ucOrderBillHeader2.SetVisible(false);

                this.ucOrderBillHeader2.SetValueVisible(false);

                this.SetVisibleForLong(Color.White, false);

                this.lblPageLong.Visible = false;

                for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
                {
                    if (i != this.fpLongOrder.ActiveSheet.ActiveRowIndex)
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                    else
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                }

                print.PrintPreview((Int32)ShortOrderColunms.CombNO, 24, this.pnLongOrder);

                this.ucOrderBillHeader2.SetValueVisible(true);

                this.ucOrderBillHeader2.SetVisible(true);

                this.SetVisibleForLong(Color.Black, true);

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
            if (isCanIntervalPrint && this.OrderPrintType == PrintType.PrintWhenPatientOut)
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
            if (isCanIntervalPrint && this.OrderPrintType == PrintType.PrintWhenPatientOut)
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

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            if (this.fpLongOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

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
                        if (myOrder.UpdatePageNoAndRowNo(this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "1", "0") <= 0)
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
                            if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
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

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.fpShortOrder.Sheets[sheetIndex].Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.Sheets[sheetIndex].Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

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
                        if (myOrder.UpdatePageNoAndRowNo(this.myPatientInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", "0") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "1", "0") <= 0)
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
                            if (myOrder.UpdateGetFlag(this.myPatientInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
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

                        this.lblPageLong.Visible = false;
                        this.label1.Visible = false;
                        this.ucOrderBillHeader2.SetValueVisible(false);
                    }
                    else
                    {
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

                        this.lblPageShort.Visible = false;
                        this.label1.Visible = false;
                        this.ucOrderBillHeader1.SetValueVisible(false);
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
                    }
                }
            }
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="color"></param>
        /// <param name="vis"></param>
        private void SetVisibleForLong(Color color, bool vis)
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

                if (color == Color.White)
                {
                    if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                    {
                        //����״��������Ѵ�ӡ���в�����ʾ
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        if (oT != null)
                        {
                            if (oT.GetFlag == "1")
                            {
                                if (oT.DCOper.OperTime != DateTime.MinValue)
                                {
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.Qty, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.Unit, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                                }
                                else
                                {
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.Qty, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.Unit, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                    this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                                }
                            }
                            else if (oT.GetFlag == "2")
                            {
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.BeginDate, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.BeginTime, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.Qty, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.Unit, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                this.fpLongOrder.Sheets[0].SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                            }
                            else
                            {
                            }
                        }
                    }
                }
                this.fpLongOrder.ActiveSheet.Rows[i].BackColor = Color.White;
            }
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="color"></param>
        /// <param name="vis"></param>
        private void SetVisibleForShort(Color color, bool vis)
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

                if (color == Color.White)
                {
                    if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                    {
                        //����״��������Ѵ�ӡ���в�����ʾ
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        if (oT != null)
                        {
                            if (oT.GetFlag == "1")
                            {
                                if (oT.DCOper.OperTime != DateTime.MinValue)
                                {
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                    //this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                    //this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                                }
                                else
                                {
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                    this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                                }
                            }
                            else if (oT.GetFlag == "2")
                            {
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.BeginDate, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.BeginTime, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.ConfirmTime, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.DCFlage, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.DCDoct, "");
                                this.fpShortOrder.Sheets[0].SetValue(i, (Int32)ShortOrderColunms.CombNO, "");
                            }
                            else
                            {
                            }
                        }
                    }
                }
                this.fpShortOrder.ActiveSheet.Rows[i].BackColor = Color.White;
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

            this.ucOrderBillHeader1.SetPatientInfo(this.myPatientInfo);
            this.ucOrderBillHeader2.SetPatientInfo(myPatientInfo);
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
                FS.HISFC.Models.RADT.PatientInfo temp = e.Node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (temp == null)
                {
                    temp = this.inPatientMgr.QueryPatientInfoByInpatientNO(((FS.FrameWork.Models.NeuObject)e.Node.Tag).ID);
                }

                if (temp != null)
                {
                    this.ucOrderBillHeader1.SetPatientInfo(temp);
                    this.ucOrderBillHeader2.SetPatientInfo(temp);
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

                this.ucOrderBillHeader2.SetChangedInfo(GetFirstOrder(true));
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

                this.ucOrderBillHeader1.SetChangedInfo(GetFirstOrder(false));
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

        }

        /// <summary>
        /// ֻ�����������ҽ��ֹͣʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDateItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion
        
        /// <summary>
        /// ��ӡ��ʽ
        /// </summary>
        private enum PrintType
        {
            /// <summary>
            /// ��ֽ����
            /// </summary>
            WhitePaperContinue = 0,

            /// <summary>
            /// ӡˢ����
            /// </summary>
            DrawPaperContinue = 1,

            /// <summary>
            /// ��Ժ��ӡ
            /// </summary>
            PrintWhenPatientOut = 2
        }

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


            this.ucOrderBillHeader1.SetPatientInfo(patientInfo);
            this.ucOrderBillHeader2.SetPatientInfo(patientInfo);

            this.myPatientInfo = patientInfo;

            if (this.myPatientInfo != null && !string.IsNullOrEmpty(myPatientInfo.ID))
            {
                this.SetTreeView();
            }
        }

        public void ShowPrintSet()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void frmOrderPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Clear();
        }
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
        BeginDate = 0,

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
        /// ����
        /// </summary>
        Qty,

        /// <summary>
        /// ��λ
        /// </summary>
        Unit,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        RecipeDoct,

        /// <summary>
        /// ��˻�ʿ
        /// </summary>
        ConfirmNurse,

        /// <summary>
        /// ֹͣ����
        /// </summary>
        DCDate,

        /// <summary>
        /// ֹͣʱ��
        /// </summary>
        DCTime,

        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        DCDoct,

        /// <summary>
        /// ֹͣ��˻�ʿ
        /// </summary>
        DCConfirmNurse,

        /// <summary>
        /// ���
        /// </summary>
        CombNO,

        /// <summary>
        /// ������
        /// </summary>
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
        /// ����
        /// </summary>
        Qty,

        /// <summary>
        /// ��λ
        /// </summary>
        Unit,

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
        /// ���
        /// </summary>
        CombNO,

        /// <summary>
        /// ������
        /// </summary>
        Max
    }
}