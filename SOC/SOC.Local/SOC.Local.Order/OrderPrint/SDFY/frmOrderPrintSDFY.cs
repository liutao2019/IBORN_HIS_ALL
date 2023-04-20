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

namespace FS.SOC.Local.Order.OrderPrint.SDFY
{
    /// <summary>
    /// ҽ������ӡ����ɽ��˳�������ױ���Ժ��
    /// </summary>
    public partial class frmOrderPrintSDFY  : FS.FrameWork.WinForms.Forms.BaseStatusBar, FS.HISFC.BizProcess.Interface.IPrintOrder
    {
        public frmOrderPrintSDFY()
        {
            InitializeComponent();
        }

        #region ����

        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.Pharmacy.Item myItem = new FS.HISFC.BizLogic.Pharmacy.Item();
        FS.HISFC.BizLogic.RADT.InPatient inPatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Manager.Person PersonManger = new FS.HISFC.BizLogic.Manager.Person();

        Function funMgr = new Function();

        /// <summary>
        /// ҩƷ������Ϣ������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper phaItemHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        System.Windows.Forms.ContextMenu menu = new ContextMenu();

        /// <summary>
        /// �жϺ����ʱ���
        /// </summary>
        DateTime reformDate = new DateTime(2000, 1, 1);

        ArrayList alLong = new ArrayList();
        ArrayList alShort = new ArrayList();

        /// <summary>
        /// ����ҽ���б�
        /// </summary>
        ArrayList alRecord = new ArrayList();

        /// <summary>
        /// �洢��ӡ��ͬ����ʱʹ�õĴ�ӡ���ͺ�
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

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo;

        /// <summary>
        /// ���Ʋ�������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
        /// </summary>
        private bool isDisplayRegularName = true;

        string pathNameLongOrderPrint = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\LongOrderPrintSetting.xml";

        string pathNameShortOrderPrint = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\ShortOrderPrintSetting.xml";

        #endregion

        #region ����

        #region ��ʼ��

        /// <summary>
        /// LOAD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderPrint_Load(object sender, System.EventArgs e)
        {
            this.ucOrderBillHeader1.Header = "��  ʱ  ҽ  ��  ��";
            this.ucOrderBillHeader2.Header = "��  ��  ҽ  ��  ��";
            ArrayList alPha = new ArrayList(this.myItem.QueryItemAvailableList());
            this.phaItemHelper.ArrayObject = alPha;

            InitOrderPrint();

            this.InitLongSheet(ref this.fpLongOrder_Sheet1);
            this.InitShortSheet(ref this.fpShortOrder_Sheet1);

            this.tbExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbRePrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C�ش�);
            this.tbQuery.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);

            this.tbReset.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbReset.Visible = true;
            this.tbReset.Enabled = true;

            this.tbSetting.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S����);
            this.tbSetting.Visible = false;

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);

            this.fpLongOrder.ActiveSheetChanged += new EventHandler(fpSpread2_ActiveSheetChanged);
            this.fpShortOrder.ActiveSheetChanged += new EventHandler(fpShortOrder_ActiveSheetChanged);

            this.fpLongOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpLongOrder_ColumnWidthChanged);
            this.fpShortOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpShortOrder_ColumnWidthChanged); 
            
            this.tbReset.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
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

            if (this.orderManager.ResetOrderPrint("-1", "-1", myPatientInfo.ID, orderType, "0") == -1)
            {
                MessageBox.Show("����ʧ��!" + this.orderManager.Err);
                return;
            }

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

            this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));
            this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));
            this.QueryPatientOrder();

            MessageBox.Show("���óɹ�!");

        }

        #endregion

        void fpShortOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
            {
                this.fpShortOrder.SaveSchema(this.fpShortOrder.ActiveSheet, pathNameShortOrderPrint);
            }
        }

        void fpLongOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).IsManager)
            {
                this.fpLongOrder.SaveSchema(this.fpLongOrder.ActiveSheet, pathNameLongOrderPrint);
            }
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void InitOrderPrint()
        {
            //����ҽ�Ʋ�������ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
            try
            {
                this.isDisplayRegularName = controlIntegrate.GetControlParam<bool>("HNZY01", true, true);
            }
            catch
            {
                this.isDisplayRegularName = true;
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
        private void InitLongSheet(ref FarPoint.Win.Spread.SheetView sheet)
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
            sheet.ColumnHeader.Rows[1].Height = 40F;

            #region ����
            sheet.ColumnHeader.Cells.Get(0, 0).Text = " �� ʼ ";
            sheet.ColumnHeader.Cells.Get(1, 0).Text = "�� ��";
            sheet.ColumnHeader.Cells.Get(1, 1).Text = "ʱ ��";
            sheet.ColumnHeader.Cells.Get(1, 2).Text = "ҽ ʦ ǩ ��";
            sheet.ColumnHeader.Cells.Get(1, 3).Text = "ִ�л�ʿǩ��";
            sheet.ColumnHeader.Cells.Get(0, 4).Text = "���";
            sheet.ColumnHeader.Cells.Get(0, 5).Text = "�� �� ҽ ��";
            sheet.ColumnHeader.Cells.Get(0, 6).Text = "ͣ ֹ";
            sheet.ColumnHeader.Cells.Get(1, 6).Text = "�� ��";
            sheet.ColumnHeader.Cells.Get(1, 7).Text = "ʱ ��";
            sheet.ColumnHeader.Cells.Get(1, 8).Text = "ҽ ʦ ǩ ��";
            sheet.ColumnHeader.Cells.Get(1, 9).Text = "ִ�л�ʿǩ��";
            sheet.ColumnHeader.Cells.Get(0, 10).Text = "ǩ��ȷ��";
            sheet.ColumnHeader.Cells.Get(1, 10).Text = "ʱ ��";
            sheet.ColumnHeader.Cells.Get(1, 11).Text = "ǩ ��";


            sheet.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 4;
            sheet.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 4;
            sheet.ColumnHeader.Cells.Get(0, 10).ColumnSpan = 2;

            sheet.Columns.Get((Int32)LongOrderColunms.MoDate).Width = 40F;
            sheet.Columns.Get((Int32)LongOrderColunms.MoDate).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.MoTime).Width = 40F;
            sheet.Columns.Get((Int32)LongOrderColunms.MoTime).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.RecipeDoct).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.RecipeDoct).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.ConfirmNurse).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.ConfirmNurse).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.CombFlag).Width = 51F;
            sheet.Columns.Get((Int32)LongOrderColunms.CombFlag).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).Width = 200F;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.DCDate).Width = 40F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDate).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.DCTime).Width = 40F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCTime).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.DCDoct).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCDoct).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.DCConfirmNurse).Width = 55F;
            sheet.Columns.Get((Int32)LongOrderColunms.DCConfirmNurse).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.PatientSignDate).Width = 43F;
            sheet.Columns.Get((Int32)LongOrderColunms.PatientSignDate).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)LongOrderColunms.PatientSignNote).Width = 66F;
            sheet.Columns.Get((Int32)LongOrderColunms.PatientSignNote).Font = new Font("����", 9F);


            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            #endregion

            //sheet.Columns.Get((Int32)LongOrderColunms.Qty).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;

            textCellType1.WordWrap = true;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).CellType = textCellType1;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            sheet.Columns.Get((Int32)LongOrderColunms.ItemName).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            sheet.Columns.Get((Int32)LongOrderColunms.CombFlag).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            sheet.Columns.Get((Int32)LongOrderColunms.CombNO).Visible = false;
            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            #region ����ÿ�еĸ߶�

            //ҽ�����ĸ߶�
            //int fpLongHeight = 910;
            int fpLongHeight = 850;

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

            if (System.IO.File.Exists(pathNameLongOrderPrint))
            {
                this.fpShortOrder.ReadSchema(sheet, pathNameLongOrderPrint, false);
            }
        }

        /// <summary>
        /// ��ʼ����ʱҽ��Sheet
        /// </summary>
        /// <param name="sheet"></param>
        private void InitShortSheet(ref FarPoint.Win.Spread.SheetView sheet)
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
            sheet.ColumnHeader.Rows[1].Height = 40F;

            #region ����

            sheet.ColumnHeader.Cells.Get(0, 0).Text = "�� ��";
            sheet.ColumnHeader.Cells.Get(0, 1).Text = "ʱ ��";
            sheet.ColumnHeader.Cells.Get(0, 2).Text = "ҽ ʦ ǩ ��";

            sheet.ColumnHeader.Cells.Get(0, 3).Text = "�� ʱ ҽ ��";
            sheet.ColumnHeader.Cells.Get(0, 4).Text = "";
            sheet.ColumnHeader.Cells.Get(0, 5).Text = "ִ �� ʱ ��";
            sheet.ColumnHeader.Cells.Get(0, 6).Text = "ִ�л�ʿǩ��";
            sheet.ColumnHeader.Cells.Get(0, 7).Text = "ǩ��ȷ��";
            sheet.ColumnHeader.Cells.Get(1, 7).Text = "ʱ��";
            sheet.ColumnHeader.Cells.Get(1, 8).Text = "ǩ��";

            sheet.ColumnHeader.Cells.Get(0, 0).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 1).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 3).ColumnSpan = 2;
            //sheet.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 6).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 7).ColumnSpan = 2;

            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.Rows.Get(0).Height = 26F;
            sheet.ColumnHeader.Rows.Get(1).Height = 26F;

            #endregion

            textCellType2.WordWrap = true;
            textCellType2.Multiline = true;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).CellType = textCellType2;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmDate).CellType = textCellType2;

            sheet.Columns.Get((Int32)ShortOrderColunms.CombNO).Visible = false;
            sheet.RowHeader.Columns.Default.Resizable = true;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";

            sheet.Columns.Get((Int32)ShortOrderColunms.MoDate).Width = 45F;
            sheet.Columns.Get((Int32)ShortOrderColunms.MoDate).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.MoTime).Width = 54F;
            sheet.Columns.Get((Int32)ShortOrderColunms.MoTime).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.RecipeDoct).Width = 58F;
            sheet.Columns.Get((Int32)ShortOrderColunms.RecipeDoct).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.CombFlag).Width = 20F;
            sheet.Columns.Get((Int32)ShortOrderColunms.CombFlag).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).Width = 300F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmDate).Width = 58F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmDate).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmNurse).Width = 68F;
            sheet.Columns.Get((Int32)ShortOrderColunms.ConfirmNurse).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.PatientSignDate).Width = 46F;
            sheet.Columns.Get((Int32)ShortOrderColunms.PatientSignDate).Font = new Font("����", 9F);
            sheet.Columns.Get((Int32)ShortOrderColunms.PatientSignNote).Width = 80F;
            sheet.Columns.Get((Int32)ShortOrderColunms.PatientSignNote).Font = new Font("����", 9F);

            #region ����ÿ�еĸ߶�

            int fpLongHeight = 850;

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

            if (System.IO.File.Exists(pathNameShortOrderPrint))
            {
                this.fpShortOrder.ReadSchema(sheet, pathNameShortOrderPrint, false);
                //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(sheet, pathNameShortOrderPrint);
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
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");
            Application.DoEvents();

            ArrayList alAll = new ArrayList();

            alAll = this.orderManager.QueryPrnOrder(this.myPatientInfo.ID);

            //this.AddSkinTestResult(alAll);

            alLong.Clear();
            alShort.Clear();

            foreach (object obj in alAll)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
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

            FS.HISFC.BizLogic.RADT.InPatient myInpatient = new FS.HISFC.BizLogic.RADT.InPatient();

            this.alRecord = new ArrayList();//myInpatient.QueryRecord("4",this.myPatientInfo.ID);

            if (this.alRecord != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in this.alRecord)
                {
                    if (FS.FrameWork.Function.NConvert.ToDateTime(obj.Name) > this.reformDate)
                    {
                        //��ȡ����ʱ��
                        this.reformDate = FS.FrameWork.Function.NConvert.ToDateTime(obj.Name);
                    }
                }
            }

            this.Clear();

            this.AddObjectToFpLong(alLong);

            this.AddObjectToFpShort(alShort);

            this.DealDCOrders();

            //this.SetPatient(this.myPatientInfo);

            this.lblPageLong.Visible = true;
            this.lblPageShort.Visible = true;

            this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));
            this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ����ֹͣ����������
        /// </summary>
        private void DealDCOrders()
        {
            FS.HISFC.Models.Order.Inpatient.Order inOrder = null;
            Font font_Italic = new Font(this.fpShortOrder.Font.FontFamily, 9F, FontStyle.Italic);
            Font font_Regular = new Font(this.fpShortOrder.Font.FontFamily, 9F, FontStyle.Regular);

            //�п�
            float width = this.fpShortOrder.ActiveSheet.Columns[(int)ShortOrderColunms.ItemName].Width;

            Graphics g = CreateGraphics();

            SizeF sim = new SizeF();

            for (int index = 0; index < this.fpShortOrder.Sheets.Count; index++)
            {
                for (int i = 0; i < fpShortOrder.Sheets[index].RowCount; i++)
                {
                    inOrder = fpShortOrder.Sheets[index].Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (inOrder != null)
                    {
                        if ("3|4".Contains(inOrder.Status.ToString()))
                        {
                            this.fpShortOrder.Sheets[index].Cells[i, (int)ShortOrderColunms.ItemName].Font = font_Italic;
                            sim = g.MeasureString(this.fpShortOrder.Sheets[index].Cells[i, (int)ShortOrderColunms.ItemName].Text + "��", font_Italic);
                            if (sim.Width < width)
                            {
                                this.fpShortOrder.Sheets[index].Cells[i, (int)ShortOrderColunms.ItemName].Text += "\nȡ�� " + inOrder.DCOper.Name + " " + inOrder.EndTime.ToString();
                            }
                            else
                            {
                                this.fpShortOrder.Sheets[index].Cells[i, (int)ShortOrderColunms.ItemName].Text += " ȡ�� " + inOrder.DCOper.Name + " " + inOrder.EndTime.ToString();
                            }
                        }
                        else
                        {
                            this.fpShortOrder.Sheets[index].Cells[i, (int)ShortOrderColunms.ItemName].Font = font_Regular;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ����Ƥ�Խ����ʾ
        /// </summary>
        private void AddSkinTestResult(ArrayList alOrder)
        {
            foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in alOrder)
            {
                orderObj.Item.Name += funMgr.TransHypotest(orderObj.Item.ID, orderObj.HypoTest);
            }
        }

        private string SkinTestResult(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            return " " + funMgr.TransHypotest(inOrder.Item.ID, inOrder.HypoTest) + " ";
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

            //this.treeView1.SelectedNode = node;
            //this.treeView1_AfterSelect(new object(), new TreeViewEventArgs(node));
        }

        #region ���

        /// <summary>
        /// �������ҽ��������
        /// </summary>
        private void Clear()
        {
            #region ����
            this.fpLongOrder_Sheet1.Columns.Count = (Int32)LongOrderColunms.Max;
            #region �������
            //for (int j = 0; j < this.fpLongOrder_Sheet1.Rows.Count; j++)
            //{
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.MoDate, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.MoTime, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.RecipeDoct, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.ConfirmNurse, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.CombFlag, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.ItemName, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCDate, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCTime, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCDoct, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.DCConfirmNurse, "");
            //    this.fpLongOrder_Sheet1.SetValue(j, (Int32)LongOrderColunms.CombNO, "");
            //}
            #endregion

            #region ����һ��Sheet
            if (this.fpLongOrder.Sheets.Count > 1)
            {
                for (int j = this.fpLongOrder.Sheets.Count - 1; j > 0; j--)
                {
                    this.fpLongOrder.Sheets.RemoveAt(j);
                }
            }

            int count = this.fpLongOrder_Sheet1.RowCount;
            this.fpLongOrder_Sheet1.RowCount = 0;
            this.fpLongOrder_Sheet1.RowCount = count;

            this.InitLongSheet(ref this.fpLongOrder_Sheet1);

            #endregion

            #endregion

            #region ����
            this.fpShortOrder_Sheet1.Columns.Count = (Int32)ShortOrderColunms.Max;

            #region �������
            //for (int j = 0; j < this.fpShortOrder_Sheet1.Rows.Count; j++)
            //{
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.MoDate, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.MoTime, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.RecipeDoct, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ItemName, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ConfirmDate, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ConfirmNurse, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.PatientSignDate, "");
            //    this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.PatientSignNote, "");
            //}
            #endregion

            #region ����һ��Sheet
            if (this.fpShortOrder.Sheets.Count > 1)
            {
                for (int j = this.fpShortOrder.Sheets.Count - 1; j > 0; j--)
                {
                    this.fpShortOrder.Sheets.RemoveAt(j);
                }
            }

            count = this.fpShortOrder_Sheet1.RowCount;
            this.fpShortOrder_Sheet1.RowCount = 0;
            this.fpShortOrder_Sheet1.RowCount = count;

            this.InitShortSheet(ref this.fpShortOrder_Sheet1);

            #endregion

            #endregion
        }
        #endregion

        /// <summary>
        /// ��ӵ�����Fp
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectToFpLong(ArrayList al)
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

            bool reformPrint = false;

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
                        if (order.MOTime > this.reformDate)
                        {
                            reformPrint = true;
                        }

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo + 1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        this.fpLongOrder_Sheet1.Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());


                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                                else
                                {
                                    this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                            }
                            else
                            {
                                this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                            }
                        }
                        else
                        {
                            //???Ŀ�ĺ��ڣ�
                            if (order.Item.SysClass.ID.ToString() != "UN" && order.Item.SysClass.ID.ToString() != "MF" && order.Item.MinFee.ID != "008" && order.Item.MinFee.ID != "038")
                            {
                                this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Frequency.ID + " " + order.Memo);
                            }
                            else
                            {
                                if (order.Item.Name.IndexOf("����") < 0 && order.Item.Name.IndexOf("ʳ") < 0)
                                {
                                    this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Frequency.ID + " " + order.Memo);
                                }
                                else
                                {
                                    this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Memo);
                                }
                            }

                            if (order.Item.ID != "999")
                            {
                                this.fpLongOrder_Sheet1.Cells[order.RowNo, (Int32)LongOrderColunms.ItemName].Text += " " + order.Qty.ToString() + order.Item.PriceUnit;
                            }
                        }
                        this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.RecipeDoct, order.ReciptDoctor.Name);
                        this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.DCOper.OperTime > this.reformDate)
                        {
                            this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCDate, order.DCOper.OperTime.Month.ToString() + "-" + order.DCOper.OperTime.Day.ToString());
                            this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCTime, order.DCOper.OperTime.ToShortTimeString());
                            this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCDoct, order.DCOper.Name);
                            this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.DCConfirmNurse, order.DCNurse.Name);
                        }
                        this.fpLongOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.CombNO, order.Combo.ID);

                        this.fpLongOrder_Sheet1.Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Function.DrawCombo1(this.fpLongOrder_Sheet1, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                    this.AddCombNo(this.fpLongOrder_Sheet1, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);

                    this.fpLongOrder_Sheet1.Tag = pageNo;
                    this.fpLongOrder_Sheet1.SheetName = "��" + (pageNo + 1).ToString() + "ҳ";
                }
                #endregion

                #region ����ҳ
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                    this.InitLongSheet(ref sheet);
                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);

                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {
                        if (order.MOTime > this.reformDate)
                        {
                            reformPrint = true;
                        }

                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo + 1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[order.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());

                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                                }
                            }
                            else
                            {
                                sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + " " + order.Usage.Name + " " + order.Memo);
                            }
                        }
                        else
                        {
                            if (order.Item.SysClass.ID.ToString() != "UN" && order.Item.SysClass.ID.ToString() != "MF" && order.Item.MinFee.ID != "008" && order.Item.MinFee.ID != "038")
                            {
                                sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Frequency.ID + " " + order.Memo);
                            }
                            else
                            {
                                if (order.Item.Name.IndexOf("����") < 0 && order.Item.Name.IndexOf("ʳ") < 0)
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Frequency.ID + " " + order.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Memo);
                                }
                            }

                            if (order.Item.ID != "999")
                            {
                                sheet.Cells[order.RowNo, (Int32)LongOrderColunms.ItemName].Text += " " + order.Qty.ToString() + order.Item.PriceUnit;
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
                    this.AddCombNo(sheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);

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
            int activeSheetIndex = -1;
            int activeRowIndex = -1;

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

                //���������ҽ�������Զ���ҳ
                if (oTemp.MOTime > this.reformDate && this.reformDate != DateTime.MinValue && activeSheetIndex == -1 && !reformPrint)
                {
                    activeSheetIndex = this.fpLongOrder.Sheets.Count - 1;
                    activeRowIndex = (iniIndex + MaxRowNo) % rowNum;
                }

                activeRow = (iniIndex + MaxRowNo) % rowNum;
                if (activeRow == 0 && (iniIndex + MaxRowNo) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(ref sheet);

                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());


                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF" && oTemp.Item.MinFee.ID != "008" && oTemp.Item.MinFee.ID != "038")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo);
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.Cells[activeRow, (Int32)LongOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Item.PriceUnit;
                            //activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Qty, oTemp.Qty.ToString());
                            //activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.Unit, oTemp.Item.PriceUnit);
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
                    this.AddCombNo(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF" && oTemp.Item.MinFee.ID != "008" && oTemp.Item.MinFee.ID != "038")
                        {
                            activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo);
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.Cells[activeRow, (Int32)LongOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Item.PriceUnit;
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
                    this.AddCombNo(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
            }

            if (activeSheetIndex >= 0)
            {
                this.AddObjectToFpLongAfter(activeSheetIndex, activeRowIndex);
            }

            #endregion

            DealLongOrderCrossPage();
        }

        /// <summary>
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        private void AddObjectToFpLongAfter()
        {
            DateTime now = this.orderManager.GetDateTimeFromSysDateTime().Date;//��ǰϵͳʱ��

            #region �������

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Tag) + 1;
            int MaxRowNo = -1;

            if (this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Rows[this.fpLongOrder.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

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

            for (int i = this.fpLongOrder.ActiveSheet.ActiveRowIndex; i < this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Rows.Count; i++)
            {
                if (this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Rows[i].Tag != null)
                {
                    alPageNull.Add(this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Rows[i].Tag);
                }
            }

            for (int i = this.fpLongOrder.ActiveSheetIndex + 1; i < this.fpLongOrder.Sheets.Count; i++)
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

            for (int j = this.fpLongOrder.ActiveSheet.ActiveRowIndex; j < this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Rows.Count; j++)
            {
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.MoDate, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.MoTime, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.CombFlag, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.ItemName, "");
                //this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.Qty, "");
                //this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.Unit, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.RecipeDoct, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, (Int32)LongOrderColunms.ConfirmNurse, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, 8, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].SetValue(j, 9, "");
                this.fpLongOrder.Sheets[this.fpLongOrder.ActiveSheetIndex].Rows[j].Tag = null;
            }

            #endregion

            #region ����һ��Sheet

            if (this.fpLongOrder.Sheets.Count > 1)
            {
                for (int j = this.fpLongOrder.Sheets.Count - 1; j > this.fpLongOrder.ActiveSheetIndex; j--)
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

            this.InitLongSheet(ref orgSheet);

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

                    this.InitLongSheet(ref newsheet);

                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.CombFlag, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Nurse.Name);
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF" && oTemp.Item.MinFee.ID != "008" && oTemp.Item.MinFee.ID != "038")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo);
                            }
                        }
                    }
                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ConfirmNurse, oTemp.DCOper.OperTime.ToShortTimeString());
                        }
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 8, oTemp.DCOper.Name);
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 9, oTemp.DCNurse.Name);
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 10, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, 10, (Int32)LongOrderColunms.CombFlag);
                    this.AddCombNo(activeSheet, 10, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.CombFlag, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Nurse.Name);
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF" && oTemp.Item.MinFee.ID != "008" && oTemp.Item.MinFee.ID != "038")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo);
                            }
                        }
                    }
                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.RecipeDoct, oTemp.DCOper.OperTime.Month.ToString() + "-" + oTemp.DCOper.OperTime.Day.ToString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ConfirmNurse, oTemp.DCOper.OperTime.ToShortTimeString());
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 8, oTemp.DCOper.Name);
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 9, oTemp.DCNurse.Name);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, 10, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, 10, (Int32)LongOrderColunms.CombFlag);
                    this.AddCombNo(activeSheet, 10, (Int32)LongOrderColunms.CombFlag);
                }
            }

            #endregion
        }

        /// <summary>
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        private void AddObjectToFpLongAfter(int sheet, int row)
        {
            //�Ȳ���������ҽ����houwb 2011-4-17
            return;
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
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.MoDate, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.MoTime, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.CombFlag, "");
                this.fpLongOrder.Sheets[sheet].SetValue(j, (Int32)LongOrderColunms.ItemName, "");
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

            this.InitLongSheet(ref orgSheet);

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

                    this.InitLongSheet(ref newsheet);

                    this.fpLongOrder.Sheets.Insert(this.fpLongOrder.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);

                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "  " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + "  " + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                        }

                        //activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName].Text += " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF" && oTemp.Item.MinFee.ID != "008" && oTemp.Item.MinFee.ID != "038")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo);
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
                    this.AddCombNo(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                                //activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName].Text += " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                                //activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName].Text += " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                                //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                                //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                            //activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName].Text += " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                            //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Qty, oTemp.DoseOnce.ToString());
                            //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);
                        }
                    }
                    else
                    {
                        if (oTemp.Item.SysClass.ID.ToString() != "UN" && oTemp.Item.SysClass.ID.ToString() != "MF" && oTemp.Item.MinFee.ID != "008" && oTemp.Item.MinFee.ID != "038")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                        }
                        else
                        {
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo);
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
                    this.AddCombNo(activeSheet, (Int32)LongOrderColunms.CombNO, (Int32)LongOrderColunms.CombFlag);
                }
            }

            #endregion
        }

        Hashtable hsCombNo = null;

        /// <summary>
        /// ��������ʾ��˳�¸����������е�����
        /// </summary>
        private void AddCombNo(FarPoint.Win.Spread.SheetView stView, int column, int addColum)
        {
            hsCombNo = new Hashtable();
            int combNo = 0;
            for (int i = 0; i < stView.RowCount; i++)
            {
                //��֤����
                if (string.IsNullOrEmpty(stView.Cells[i, addColum].Text))
                {
                    stView.Cells[i, addColum].Text = "  ";
                }

                if (!string.IsNullOrEmpty(stView.Cells[i, column].Text.Trim()))
                {
                    if (!hsCombNo.Contains(stView.Cells[i, column].Text.Trim()))
                    {
                        combNo += 1;
                        hsCombNo.Add(stView.Cells[i, column].Text.Trim(), null);
                        stView.Cells[i, addColum].Text += "[" + combNo.ToString() + "]";
                    }
                    else
                    {
                        stView.Cells[i, addColum].Text += "[" + combNo.ToString() + "]";
                    }
                }
            }
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

                        this.fpShortOrder_Sheet1.Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());

                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (order.OrderType.ID == "CD")
                                    {
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + " " + order.Frequency.ID + "(��Ժ��ҩ)" + " " + order.Memo);

                                    }
                                    else if (order.OrderType.ID == "BL")
                                    {
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + "(��¼ҽ��)" + " " + order.Memo);
                                    }
                                    else if (order.OrderType.ID == "BZ")
                                    {
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + "(��¼ҽ��)" + " " + order.Memo);
                                    }
                                    else
                                    {
                                        //o.DoseOnce.ToString()+(o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit+" "
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + " " + order.Memo);
                                    }
                                }
                                else
                                {
                                    if (order.OrderType.ID == "CD")
                                    {
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + "(��Ժ��ҩ)" + " " + order.Memo);
                                    }
                                    else if (order.OrderType.ID == "BL")
                                    {
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + "(��¼ҽ��)" + " " + order.Memo);
                                    }
                                    else if (order.OrderType.ID == "BZ")
                                    {
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + "(��¼ҽ��)" + " " + order.Memo);
                                    }
                                    else
                                    {
                                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + " " + order.Memo);
                                    }
                                }
                            }
                            else
                            {
                                if (order.OrderType.ID == "CD")
                                {
                                    this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Frequency.ID + "(��Ժ��ҩ)" + " " + order.Memo);
                                }
                                else if (order.OrderType.ID == "BL")
                                {
                                    this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + "(��¼ҽ��)" + " " + order.Memo);
                                }
                                else if (order.OrderType.ID == "BZ")
                                {
                                    this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + "(��¼ҽ��)" + " " + order.Memo);
                                }
                                else
                                {
                                    this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.DoseOnce.ToString() + (order.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + order.Usage.Name + " " + order.Memo);
                                }
                            }

                            this.fpShortOrder_Sheet1.Cells[order.RowNo, (Int32)ShortOrderColunms.ItemName].Text += " " + order.Qty.ToString() + order.Unit;
                        }
                        else
                        {
                            if (order.Item.MinFee.ID == "037" || order.Item.MinFee.ID == "038" || order.Item.Name.IndexOf("��Ƥ") >= 0)
                            {
                                this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                            }
                            else
                            {
                                if (order.OrderType.ID == "BL")
                                {
                                    this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + "(��¼ҽ��)" + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                                }
                                else if (order.OrderType.ID == "BZ")
                                {
                                    this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + "(��¼ҽ��)" + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                                }
                                else
                                {
                                    this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + this.SkinTestResult(order) + " " + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                                }
                            }

                            //��ҩƷҲ��ʾ�����͵�λ
                            if (order.Item.ID != "999")
                            {
                                this.fpShortOrder_Sheet1.Cells[order.RowNo, (Int32)ShortOrderColunms.ItemName].Text += " " + order.Qty.ToString() + order.Item.PriceUnit;
                                //this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.Qty, order.Qty.ToString());
                                //this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.Unit, order.Item.PriceUnit);
                            }
                        }

                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.RecipeDoct, order.ReciptDoctor.Name);
                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, order.Nurse.Name);

                        if (order.ConfirmTime != DateTime.MinValue)
                        {
                            this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ConfirmDate, order.ConfirmTime.ToString("MM-dd HH:mm"));
                        }

                        if (order.DCOper.OperTime != DateTime.MinValue)
                        {
                            //this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.DCFlage, "DC");
                            //this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.DCDoct, order.DCOper.Name);
                        }


                        this.fpShortOrder_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.CombNO, order.Combo.ID);
                        this.fpShortOrder_Sheet1.Rows[order.RowNo].Tag = order;

                        if (pageNo == MaxPageNo && order.RowNo > MaxRowNo)
                        {
                            MaxRowNo = order.RowNo;
                        }
                    }

                    Function.DrawCombo1(this.fpShortOrder_Sheet1, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                    //this.AddCombNo(this.fpShortOrder_Sheet1, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);

                    this.fpShortOrder_Sheet1.Tag = pageNo;
                    this.fpShortOrder_Sheet1.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
                }
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(ref sheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);

                    foreach (FS.HISFC.Models.Order.Inpatient.Order oTemp in alTemp)
                    {
                        if (oTemp.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(oTemp.OrderType.Name + "��" + oTemp.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + oTemp.RowNo.ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[oTemp.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                        if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (oTemp.OrderType.ID == "CD")
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                    }
                                    else if (oTemp.OrderType.ID == "BL")
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                    }
                                    else if (oTemp.OrderType.ID == "BZ")
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                    }
                                    else
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                                    }
                                }
                                else
                                {
                                    if (oTemp.OrderType.ID == "CD")
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                    }
                                    else if (oTemp.OrderType.ID == "BL")
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                    }
                                    else if (oTemp.OrderType.ID == "BZ")
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                    }
                                    else
                                    {
                                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                                    }
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                                }
                            }

                            sheet.Cells[oTemp.RowNo, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Unit;
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Qty, o.Qty.ToString());
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.Unit, o.Unit);
                        }
                        else
                        {
                            if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                            {
                                sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "BL")
                                {
                                    sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                                }
                                else
                                {
                                    sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                                }
                            }

                            //��ҩƷҲ��ʾ�����͵�λ
                            if (oTemp.Item.ID != "999")
                            {
                                sheet.Cells[oTemp.RowNo, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Item.PriceUnit;
                                //this.fpShortOrder_Sheet1.SetValue(o.RowNo, (Int32)ShortOrderColunms.Qty, o.Qty.ToString());
                                //this.fpShortOrder_Sheet1.SetValue(o.RowNo, (Int32)ShortOrderColunms.Unit, o.Item.PriceUnit);
                            }
                        }

                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                        if (oTemp.ConfirmTime != DateTime.MinValue)
                        {
                            sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.ToString("MM-dd HH:mm"));
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmDate, o.ConfirmTime.Month.ToString() + "-" + o.ConfirmTime.Day.ToString());
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ConfirmTime, o.ConfirmTime.ToShortTimeString());
                        }

                        if (oTemp.DCOper.OperTime != DateTime.MinValue)
                        {
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.DCFlage, "DC");
                            //sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.DCDoct, o.DCOper.Name);
                        }
                        sheet.SetValue(oTemp.RowNo, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                        sheet.Rows[oTemp.RowNo].Tag = oTemp;

                        if (pageNo == MaxPageNo && oTemp.RowNo > MaxRowNo)
                        {
                            MaxRowNo = oTemp.RowNo;
                        }
                    }

                    Function.DrawCombo1(sheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                    //this.AddCombNo(sheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);

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

                    this.InitShortSheet(ref sheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.Cells[activeRow, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Unit;
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Unit);
                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }

                        //��ҩƷҲ��ʾ�����͵�λ
                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.Cells[activeRow, (Int32)ShortOrderColunms.ItemName].Text += oTemp.Qty.ToString() + oTemp.Item.PriceUnit;
                            //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.ToString("MM-dd HH:mm"));
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.Month.ToString() + "-" + oTemp.ConfirmTime.Day.ToString());
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ConfirmTime.ToShortTimeString());
                    }
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                    //this.AddCombNo(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Memo);
                            }
                        }

                        activeSheet.Cells[activeRow, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Unit;
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Unit);

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.Cells[activeRow, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Item.PriceUnit;
                            //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.ToString("MM-dd HH:mm"));
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ConfirmTime.Month.ToString() + "-" + oTemp.ConfirmTime.Day.ToString());
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ConfirmTime.ToShortTimeString());
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.DCFlage, "DC");
                        //activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[activeRow].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                    //this.AddCombNo(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
            }

            #endregion

            DealShortOrderCrossPage();
        }

        /// <summary>
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        private void AddObjectToFpShortAfter()
        {

            #region �������

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag) + 1;
            int MaxRowNo = -1;

            int row = this.fpShortOrder.ActiveSheet.ActiveRowIndex;

            if (this.fpShortOrder.ActiveSheet.Rows[row].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = this.fpShortOrder.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

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

            for (int i = this.fpShortOrder.ActiveSheet.ActiveRowIndex; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    alPageNull.Add(this.fpShortOrder.ActiveSheet.Rows[i].Tag);
                }
            }

            for (int i = this.fpShortOrder.ActiveSheetIndex + 1; i < this.fpShortOrder.Sheets.Count; i++)
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

            for (int j = row; j < this.fpShortOrder.ActiveSheet.Rows.Count; j++)
            {
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.MoDate, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.MoTime, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.RecipeDoct, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.CombFlag, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ItemName, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ConfirmDate, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.ConfirmNurse, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.PatientSignDate, "");
                this.fpShortOrder_Sheet1.SetValue(j, (Int32)ShortOrderColunms.PatientSignNote, "");
                this.fpShortOrder.ActiveSheet.Rows[j].Tag = null;
            }

            #endregion

            #region ����һ��Sheet

            if (this.fpShortOrder.Sheets.Count > 1)
            {
                for (int j = this.fpShortOrder.Sheets.Count - 1; j > this.fpShortOrder.ActiveSheetIndex; j--)
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

            this.InitShortSheet(ref orgSheet);

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
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(ref sheet);

                    this.fpShortOrder.Sheets.Insert(this.fpShortOrder.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                            }
                        }

                        //activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.DoseOnce.ToString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit);

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }
                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Item.PriceUnit;
                            //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);


                    if (oTemp.ExecOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.ToString("MM-dd HH:mm"));
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.ToString("MM-dd HH:mm"));
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                        // activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.Name);
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCFlage, "DC");
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                    //this.AddCombNo(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as FS.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                            else
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BZ")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                                }
                            }
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo);
                            }
                        }

                        //activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.DoseOnce.ToString() + oTemp.DoseUnit;

                    }
                    else
                    {
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else if (oTemp.OrderType.ID == "BZ")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + "(��¼ҽ��)" + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                            else
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + this.SkinTestResult(oTemp) + " " + oTemp.Frequency.ID + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                            }
                        }

                        if (oTemp.Item.ID != "999")
                        {
                            activeSheet.Cells[(iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName].Text += " " + oTemp.Qty.ToString() + oTemp.Item.PriceUnit;
                            //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Qty, oTemp.Qty.ToString());
                            //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.Unit, oTemp.Item.PriceUnit);
                        }
                    }


                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.RecipeDoct, oTemp.ReciptDoctor.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmNurse, oTemp.Nurse.Name);


                    if (oTemp.ExecOper.OperTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.ToString("MM-dd HH:mm"));
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmDate, oTemp.ExecOper.OperTime.Month.ToString() + "-" + oTemp.ExecOper.OperTime.Day.ToString());
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ConfirmTime, oTemp.ExecOper.OperTime.ToShortTimeString());
                    }

                    if (oTemp.DCOper.OperTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCFlage, "DC");
                        //activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.DCDoct, oTemp.DCOper.Name);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.CombNO, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNo) % rowNum].Tag = oTemp;

                    Function.DrawCombo1(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
                    //this.AddCombNo(activeSheet, (Int32)ShortOrderColunms.CombNO, (Int32)ShortOrderColunms.CombFlag);
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
            p.SetPageSize(size);
            p.IsCanCancel = false;

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

                                //���һ��Ϊ�գ�Ҳ��ӡ
                                if (string.IsNullOrEmpty(this.fpLongOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)LongOrderColunms.DCConfirmNurse].Text))
                                {
                                    this.fpLongOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)LongOrderColunms.DCConfirmNurse].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                                    this.fpLongOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)LongOrderColunms.DCConfirmNurse].Text = ".";
                                }
                            }

                            //�����ش���ʾ������
                            this.SetRePrintContentsForLong();

                            //������ʾ���߸ı���Ϣ
                            this.ucOrderBillHeader2.SetChangedInfo(this.GetFirstOrder(true));

                            this.neuPanel2.Dock = DockStyle.None;

                            this.neuPanel2.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum));

                            //p.ShowPrintPageDialog();
                            p.PrintPage(this.leftValue, this.topValue, this.panel6);
                            //p.PrintPreview(this.leftValue, this.topValue, this.panel6);
                            this.neuPanel2.Dock = DockStyle.Bottom;

                            //p.PrintPreview(this.leftValue, this.topValue, this.panel6);

                            this.ucOrderBillHeader2.SetVisible(true);

                            this.SetVisibleForLong(Color.Black, true);

                            SetStyleForFp(true, Color.White, BorderStyle.None);

                            this.QueryPatientOrder();

                            return;
                        }
                    }

                    #endregion

                    #region ����

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
                            //��һ��û�д�ӡ
                            if (this.GetFirstOrder(true).PageNo == -1)
                            {
                                this.SetVisibleForLong(Color.Black, false);

                                SetStyleForFp(true, Color.Black, BorderStyle.FixedSingle);

                                //���һ��Ϊ�գ�Ҳ��ӡ
                                if (string.IsNullOrEmpty(this.fpLongOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)LongOrderColunms.DCConfirmNurse].Text))
                                {
                                    this.fpLongOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)LongOrderColunms.DCConfirmNurse].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                                    this.fpLongOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)LongOrderColunms.DCConfirmNurse].Text = ".";
                                }
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

                    this.neuPanel2.Dock = DockStyle.None;

                    this.neuPanel2.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum));
                    //p.ShowPrintPageDialog();
                    p.PrintPage(this.leftValue, this.topValue, this.panel6);
                    this.neuPanel2.Dock = DockStyle.Bottom;

                    SetStyleForFp(true, Color.White, BorderStyle.None);

                    DialogResult dia = DialogResult.No;

                    frmNotice.label1.Text = "������ҽ�����Ƿ�ɹ�?";

                    if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                    {
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
                        else
                        {
                            if (this.UpdateOrderForLong() <= 0)
                            {
                                this.ucOrderBillHeader2.SetValueVisible(true);

                                this.ucOrderBillHeader2.SetVisible(true);

                                this.SetVisibleForLong(Color.Black, true);

                                this.QueryPatientOrder();

                                return;
                            }
                        }
                    }

                    this.ucOrderBillHeader2.SetValueVisible(true);

                    this.ucOrderBillHeader2.SetVisible(true);

                    this.SetVisibleForLong(Color.Black, true);

                    this.QueryPatientOrder();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    this.ucOrderBillHeader2.SetValueVisible(true);

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

                                //���һ��Ϊ�գ�Ҳ��ӡ
                                if (string.IsNullOrEmpty(this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.PatientSignNote].Text))
                                {
                                    this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.PatientSignNote].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                                    this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.PatientSignNote].Text = ".";
                                }

                                //if (string.IsNullOrEmpty(this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.DCDoct].Text))
                                //{
                                //    this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.DCDoct].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                                //    this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.DCDoct].Text = ".";
                                //}
                            }

                            this.SetRePrintContentsForShort();

                            this.ucOrderBillHeader1.SetChangedInfo(this.GetFirstOrder(false));

                            this.neuPanel1.Dock = DockStyle.None;

                            this.neuPanel1.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum));

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

                                //���һ��Ϊ�գ�Ҳ��ӡ
                                if (string.IsNullOrEmpty(this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.PatientSignNote].Text))
                                {
                                    this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.PatientSignNote].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                                    this.fpShortOrder.ActiveSheet.Cells[this.rowNum - 1, (Int32)ShortOrderColunms.PatientSignNote].Text = ".";
                                }
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

                    this.neuPanel1.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum));

                    p.PrintPage(this.leftValue, this.topValue, this.panel7);
                    this.neuPanel1.Dock = DockStyle.Bottom;

                    SetStyleForFp(false, Color.White, BorderStyle.None);

                    if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                    {
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
                            if (this.UpdateOrderForShort() <= 0)
                            {
                                this.ucOrderBillHeader1.SetValueVisible(true);

                                this.ucOrderBillHeader1.SetVisible(true);

                                this.SetVisibleForShort(Color.Black, true);

                                this.QueryPatientOrder();

                                return;
                            }
                        }
                    }

                    this.ucOrderBillHeader1.SetValueVisible(true);

                    this.ucOrderBillHeader1.SetVisible(true);

                    this.SetVisibleForShort(Color.Black, true);

                    this.QueryPatientOrder();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    this.ucOrderBillHeader1.SetValueVisible(true);

                    this.ucOrderBillHeader1.SetVisible(true);

                    this.SetVisibleForShort(Color.Black, true);

                    this.QueryPatientOrder();
                }
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
            p.SetPageSize(size);
            p.IsCanCancel = false;

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

                        this.neuPanel2.Dock = DockStyle.None;

                        this.neuPanel2.Location = new Point(this.fpLongOrder.Location.X, this.fpLongOrder.Location.Y + (Int32)(this.fpLongOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpLongOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpLongOrder.ActiveSheet.Rows[0].Height * this.rowNum));
                        //this.neuPanel2.Location = new Point(this.fpLongOrder.Location.X, 910);
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

                        this.neuPanel1.Dock = DockStyle.None;

                        this.neuPanel1.Location = new Point(this.fpShortOrder.Location.X, this.fpShortOrder.Location.Y + (Int32)(this.fpShortOrder.ActiveSheet.RowHeader.Rows[0].Height + this.fpShortOrder.ActiveSheet.RowHeader.Rows[1].Height + this.fpShortOrder.ActiveSheet.Rows[0].Height * this.rowNum));
                        //this.neuPanel1.Location = new Point(this.fpShortOrder.Location.X, 910);
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
        /// ��������Ŀ
        /// </summary>
        private void PrintSingleItem()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            p.SetPageSize(size);
            p.IsCanCancel = false;

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
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
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

                p.PrintPreview((Int32)ShortOrderColunms.CombNO, 24, this.panel6);

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
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                    }
                    else
                    {
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        //this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");//ִ�����ڲ���
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");//ִ��ʱ�䲻��
                        this.fpShortOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");//ִ��ʱ�䲻��
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
        /// ��������Ŀֹͣʱ��
        /// </summary>
        private void PrintSingleDate()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 790, 1100);
            p.SetPageSize(size);
            p.IsCanCancel = false;

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
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
                    }
                    else
                    {
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Qty, "");
                        //this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.Unit, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 8, "");
                        this.fpLongOrder.ActiveSheet.SetValue(i, 9, "");
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

        #region ��ȡ��ӡ��ʾ

        /// <summary>
        /// �Ƿ�����
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
        /// ��ȡ��ӡ��ʾ
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
                }
            }

            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                errText = "���ҳ�����";
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "���ҳ�����";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "1");

            if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
            {
                if (pageNo > maxPageNo + 1)
                {
                    errText = "��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ��";
                    return false;
                }
            }

            /*
            if(pageNo == maxPageNo + 1 && maxPageNo != -1)
            {
                int maxRowNo = this.orderManager.GetMaxRowNoByPageNo(this.myPatientInfo.ID,"1",maxPageNo.ToString());

                if(maxRowNo != 26)
                {
                    errText = "��"+(maxPageNo+1).ToString()+"ҳҽ������δ������";
                    return false;
                }
            }
            */

            if (pageNo == maxPageNo + 1 && maxPageNo != -1)
            {
                bool canprintflag = true;
                for (int j = 0; j < this.rowNum; j++)
                {
                    if (this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag != null)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.Sheets[maxPageNo].Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        if (oT.PageNo != maxPageNo)
                        {
                            canprintflag = false;
                            break;
                        }

                    }
                }

                if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                {
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
                }
            }

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

            if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
            {
                if (pageNo > maxPageNo + 1)
                {
                    errText = "��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ��";
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
                if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                {
                    if (!canprintflag)
                    {
                        errText = "��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ����";
                        return false;
                    }
                }
            }

            MessageBox.Show("��ȷ���ѷ����" + (pageNo + 1).ToString() + "ҳ��ʱҽ������");

            return true;
        }

        #endregion

        #region ���´�ӡ���

        /// <summary>
        /// ����ҽ��ҳ�����ȡ��־
        /// </summary>
        /// <param name="chgBed"></param>
        /// <returns></returns>
        private int UpdateOrderForLong()
        {
            FS.HISFC.BizLogic.Order.Order myOrder = new FS.HISFC.BizLogic.Order.Order();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            if (this.fpLongOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            for (int i = 0; i < this.fpLongOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpLongOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

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
        private int UpdateOrderForShort()
        {
            FS.HISFC.BizLogic.Order.Order myOrder = new FS.HISFC.BizLogic.Order.Order();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.fpShortOrder.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            for (int i = 0; i < this.fpShortOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpShortOrder.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.fpShortOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

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
                        this.ucOrderBillHeader1.SetValueVisible(false);
                    }
                    else
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

                    //this.fpLongOrder.ActiveSheet.SetValue(i,2,"");
                    //this.fpLongOrder.ActiveSheet.SetValue(i,3,"");
                    //this.fpLongOrder.ActiveSheet.SetValue(i,8,"");
                    //this.fpLongOrder.ActiveSheet.SetValue(i,9,"");
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
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

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
                if (color == Color.White)
                {
                    if (this.OrderPrintType != PrintType.PrintWhenPatientOut)
                    {
                        //����״��������Ѵ�ӡ���в�����ʾ
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.fpLongOrder.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        if (oT != null)
                        {
                            //������ӡ��ֹͣδ��ӡ
                            if (oT.GetFlag == "1")
                            {
                                if (oT.DCOper.OperTime != DateTime.MinValue)
                                {
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.MoDate, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.MoTime, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                }
                                else
                                {
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.MoDate, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.MoTime, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                    this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
                                }
                            }
                            //ֹͣ�Ѵ�ӡ
                            else if (oT.GetFlag == "2")
                            {
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.MoDate, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.MoTime, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombFlag, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ItemName, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.RecipeDoct, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.ConfirmNurse, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDate, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCTime, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCDoct, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.DCConfirmNurse, "");
                                this.fpLongOrder.ActiveSheet.SetValue(i, (Int32)LongOrderColunms.CombNO, "");
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
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.PatientSignDate, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.PatientSignNote, "");
                                }
                                else
                                {
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.PatientSignDate, "");
                                    this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.PatientSignNote, "");
                                }
                            }
                            else if (oT.GetFlag == "2")
                            {
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoDate, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.MoTime, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.CombFlag, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ItemName, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.PatientSignDate, "");
                                this.fpShortOrder_Sheet1.SetValue(i, (Int32)ShortOrderColunms.PatientSignNote, "");
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
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatient.QueryPatientInfoByInpatientNO(patientNo);

            TreeNode root = new TreeNode();
            root.Text = "סԺ��Ϣ:" + "[" + patientInfo.Name + "]" + "[" + patientInfo.PID.PatientNO + "]";
            this.treeView1.Nodes.Add(root);

            TreeNode node = new TreeNode();
            node.Text = "[" + patientInfo.PVisit.InTime.ToShortDateString() + "][" + patientInfo.PVisit.PatientLocation.Dept.Name + "]";
            node.Tag = patientInfo;
            root.Nodes.Add(node);

            //ArrayList alInTimes = this.inPatient.QueryInpatientNOByPatientNO(patientNo);

            //if (alInTimes == null)
            //{
            //    MessageBox.Show("��ѯ����סԺ��Ϣ����");
            //    return;
            //}

            //TreeNode root = new TreeNode();

            //for (int i = 0; i < alInTimes.Count; i++)
            //{
            //    FS.FrameWork.Models.NeuObject info = alInTimes[i] as FS.FrameWork.Models.NeuObject;

            //    if (i == 0)
            //    {
            //        root.Text = "סԺ��Ϣ:" + "[" + info.Name + "]" + "[" + patientNo + "]";
            //        this.treeView1.Nodes.Add(root);
            //        root.ImageIndex = 0;
            //    }

            //    TreeNode node = new TreeNode();

            //    node.Text = "[" + FS.FrameWork.Function.NConvert.ToDateTime(info.User03).ToShortDateString() + "][" + info.User02 + "]";

            //    node.Tag = info;

            //    node.ImageIndex = 1;

            //    root.Nodes.Add(node);
            //}

            this.treeView1.ExpandAll();
        }

        #region ������

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
                    temp = this.inPatient.QueryPatientInfoByInpatientNO(((FS.FrameWork.Models.NeuObject)e.Node.Tag).ID);
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
        /// ��ʱҽ����ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitShortMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpShortAfter();
        }

        /// <summary>
        /// ����ҽ����ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpLongAfter();
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
                        p.PrintDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
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
        /// ��ʾҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread2_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            this.lblPageLong.Text = (FS.FrameWork.Function.NConvert.ToInt32(this.fpLongOrder.ActiveSheet.Tag) + 1).ToString();

            this.ucOrderBillHeader2.SetChangedInfo(GetFirstOrder(true));
        }


        void fpShortOrder_ActiveSheetChanged(object sender, EventArgs e)
        {
            this.lblPageShort.Text = (FS.FrameWork.Function.NConvert.ToInt32(this.fpShortOrder.ActiveSheet.Tag) + 1).ToString();

            this.ucOrderBillHeader1.SetChangedInfo(GetFirstOrder(false));
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
                return "�Ӽ�";
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
        private void fpSpread2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.menu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "���������ʱҽ��";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);

                System.Windows.Forms.MenuItem splitShortMenuItem = new MenuItem();
                splitShortMenuItem.Text = "�Ӹ���ҽ����������һҳ";
                splitShortMenuItem.Click += new EventHandler(splitShortMenuItem_Click);

                this.menu.MenuItems.Add(printMenuItem);
                this.menu.MenuItems.Add(splitShortMenuItem);
                this.menu.Show(this.fpShortOrder, new Point(e.X, e.Y));
            }
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

        /// <summary>
        /// ����ҽ��������
        /// </summary>
        public enum LongOrderColunms
        {
            /// <summary>
            /// ��ʼ����
            /// </summary>
            MoDate = 0,

            /// <summary>
            /// ��ʼʱ��
            /// </summary>
            MoTime,

            /// <summary>
            /// ����ҽ��
            /// </summary>
            RecipeDoct,

            /// <summary>
            /// ��˻�ʿ
            /// </summary>
            ConfirmNurse,

            /// <summary>
            /// ����
            /// </summary>
            CombFlag,

            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemName,

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
            /// ǩ��ȷ��ʱ��
            /// </summary>
            PatientSignDate,

            /// <summary>
            /// ���˻����ǩ��
            /// </summary>
            PatientSignNote,

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
            MoDate,

            /// <summary>
            /// ��ʼʱ��
            /// </summary>
            MoTime,

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
            /// ���ʱ��
            /// </summary>
            ConfirmDate,

            /// <summary>
            /// ��˻�ʿ
            /// </summary>
            ConfirmNurse,

            /// <summary>
            /// ����ǩ��ʱ��
            /// </summary>
            PatientSignDate,

            /// <summary>
            /// ����ǩ������
            /// </summary>
            PatientSignNote,

            /// <summary>
            /// ���
            /// </summary>
            CombNO,

            /// <summary>
            /// ������
            /// </summary>
            Max
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
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
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
            return;
        }

        #endregion

        private void cmbPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPrintType.Text))
            {
                this.SetPrintValue("��ӡ��ʽ", this.cmbPrintType.Text);
            }

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

        private void fpShortOrder_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Graphics g = CreateGraphics();
            ////g.PageUnit = GraphicsUnit.Millimeter;
            //SizeF sim = g.MeasureString(this.fpShortOrder.ActiveSheet.Cells[e.Row, (int)ShortOrderColunms.ItemName].Text + "��", this.fpShortOrder.Font);

            //MessageBox.Show("�п��:" + this.fpShortOrder.ActiveSheet.Columns[(int)ShortOrderColunms.ItemName].Width.ToString());

            //MessageBox.Show("���ƿ��:" + sim.Width.ToString());
        }
    }
}