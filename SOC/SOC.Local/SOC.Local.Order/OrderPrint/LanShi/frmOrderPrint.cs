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
    /// ҽ������ӡ������
    /// </summary>
    public partial class frmOrderPrint : Neusoft.FrameWork.WinForms.Forms.BaseStatusBar, Neusoft.HISFC.BizProcess.Interface.IPrintOrder
    {
        public frmOrderPrint()
        {
            InitializeComponent();
        }

        #region ����

        Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        Neusoft.HISFC.BizLogic.Pharmacy.Item myItem = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
        Neusoft.HISFC.BizLogic.RADT.InPatient inPatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.BizLogic.Manager.Person PersonManger = new Neusoft.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ҩƷ������Ϣ������
        /// </summary>
        Neusoft.FrameWork.Public.ObjectHelper phaItemHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        System.Windows.Forms.ContextMenu menu = new ContextMenu();

        Function funMgr = new Function();

        /// <summary>
        /// Ĭ������ʱ��
        /// </summary>
        private DateTime reformDate = new DateTime(2000, 1, 1);
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
        private Neusoft.HISFC.Models.RADT.PatientInfo pInfo;

        /// <summary>
        /// ���߻�����Ϣ
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
        /// ���Ʋ�������
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
        /// </summary>
        private bool isDisplayRegularName = true;

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
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            this.ucOrderBillHeader1.Header = "��  ʱ  ҽ  ��  ��";
            this.ucOrderBillHeader2.Header = "��  ��  ҽ  ��  ��";
            ArrayList alPha = new ArrayList(this.myItem.QueryItemAvailableList());
            this.phaItemHelper.ArrayObject = alPha;

            InitOrderPrint();

            this.InitLongSheet(this.neuSpread1_Sheet1);
            this.InitShortSheet(this.neuSpread2_Sheet1);

            this.tbExit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbRePrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C�ش�);
            this.tbQuery.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);

            this.tbReset.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbReset.Visible = true;
            this.tbReset.Enabled = true;
            this.tbSetting.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S����);
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
            //sheet.ColumnHeader.Columns.Get(1).Width = 50;
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

            sheet.Columns.Get((Int32)ShortOrderColunms.MoTime).Label = "ʱ��";

            textCellType2.WordWrap = true;
            sheet.Columns.Get((Int32)ShortOrderColunms.ItemName).CellType = textCellType2;

            //sheet.Columns.Get((Int32)ShortOrderColunms.Unit).Locked = false;

            //sheet.Columns.Get(6).Label = "����";

            //sheet.Columns.Get(7).Label = "ʱ��";

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
        }

        #endregion

        #region ��ѯ��ʾ

        /// <summary>
        /// ��ѯ����ҽ����Ϣ
        /// </summary>
        private void QueryPatientOrder()
        {
            if (this.pInfo == null || string.IsNullOrEmpty(this.pInfo.ID))
            {
                return;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");
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
                    //����ҽ��
                    alLong.Add(obj);
                }
                else
                {
                    //��ʱҽ��
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
        /// ��ʾ������Ϣ
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
        /// ����������ʾ
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

            root.Text = "סԺ��Ϣ:" + "[" + this.pInfo.Name + "]" + "[" + this.pInfo.PID.PatientNO + "]";

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

        #region ���

        /// <summary>
        /// �������ҽ��������
        /// </summary>
        private void Clear()
        {
            #region ����
            this.neuSpread1_Sheet1.Columns.Count = (Int32)LongOrderColunms.Max;
            #region �������
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

            #region ����һ��Sheet
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

            #region ����
            this.neuSpread2_Sheet1.Columns.Count = (Int32)ShortOrderColunms.Max;

            #region �������
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

            #region ����һ��Sheet
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

                        this.neuSpread1_Sheet1.Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());


                        if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
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
                            //???Ŀ�ĺ��ڣ�
                            if (order.Item.SysClass.ID.ToString() != "UN" && order.Item.SysClass.ID.ToString() != "MF")
                            {
                                this.neuSpread1_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.ItemName, order.Item.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.Memo);
                            }
                            else
                            {
                                if (order.Item.Name.IndexOf("����") < 0 && order.Item.Name.IndexOf("ʳ") < 0)
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
                    this.neuSpread1_Sheet1.SheetName = "��" + (pageNo + 1).ToString() + "ҳ";
                }
                #endregion

                #region ����ҳ
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                    this.InitLongSheet(sheet);
                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);

                    foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alTemp)
                    {
                        if (order.RowNo >= this.rowNum)
                        {
                            MessageBox.Show(order.OrderType.Name + "��" + order.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (order.RowNo+1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[order.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        sheet.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());

                        if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
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
                                if (order.Item.Name.IndexOf("����") < 0 && order.Item.Name.IndexOf("ʳ") < 0)
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

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread1.Sheets[MaxPageNo];

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

                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());


                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
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
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
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

                         //�Ƿ���ʾͨ����������
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
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
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

            //��Ҫ����
            if (this.alRecord.Count >= 0)
            {
                //��������һ�ܺ���Զ���ҳ
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

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.Sheets[sheet].Tag) + 1;
            int MaxRowNo = -1;

            if (this.neuSpread1.Sheets[sheet].Rows[row].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.neuSpread1.Sheets[sheet].Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

            MessageBox.Show("����ҽ�����ҳ����ע�⣡", "��ʾ");

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "�Ѿ���ӡ������������һҳ��");
                return;
            }

            #endregion

            #region ��ȡʣ������

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

            #region �������

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

            #region ����һ��Sheet

            if (this.neuSpread1.Sheets.Count > 1)
            {
                for (int j = this.neuSpread1.Sheets.Count - 1; j > sheet; j--)
                {
                    this.neuSpread1.Sheets.RemoveAt(j);
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

            this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread1.Sheets[MaxPageNo];

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

                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, newsheet);

                    activeSheet = newsheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)LongOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                         //�Ƿ���ʾͨ����������
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
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
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

                        //�Ƿ���ʾͨ����������
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
                            if (oTemp.Item.Name.IndexOf("����") < 0 && oTemp.Item.Name.IndexOf("ʳ") < 0)
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

                        this.neuSpread2_Sheet1.Rows[order.RowNo].BackColor = Color.MistyRose;
                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoDate, order.MOTime.Month.ToString() + "-" + order.MOTime.Day.ToString());
                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)LongOrderColunms.MoTime, order.MOTime.ToShortTimeString());

                        if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(order.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (order.OrderType.ID == "CD")
                                    {
                                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + (order.Frequency.ID + " ") + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + order.Memo);

                                    }
                                    //else if (order.OrderType.ID == "BL")
                                    //{
                                    //    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + /*"(��¼ҽ��)" +*/ " " + order.Memo);
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
                                        this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + order.Memo);
                                    }
                                    //else if (order.OrderType.ID == "BL")
                                    //{
                                    //    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit +/* "(��¼ҽ��)" +*/ " " + order.Memo);
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
                                    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + (order.Item.ID == "999" ? "" : (order.Frequency.ID + " ")) + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + order.Memo);
                                }
                                //else if (order.OrderType.ID == "BL")
                                //{
                                //    this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Usage.Name + " " + order.DoseOnce.ToString() + (order.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + /*"(��¼ҽ��)" + */" " + order.Memo);
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
                            if (order.Item.MinFee.ID == "037" || order.Item.MinFee.ID == "038" || order.Item.Name.IndexOf("��Ƥ") >= 0)
                            {
                                this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                            }
                            else
                            {
                                this.neuSpread2_Sheet1.SetValue(order.RowNo, (Int32)ShortOrderColunms.ItemName, order.Item.Name + " " + " " + order.Memo + " " + GetEmergencyTip(order.IsEmergency));
                            }

                            //��ҩƷҲ��ʾ�����͵�λ
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
                    this.neuSpread2_Sheet1.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "ҳ";
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
                            MessageBox.Show(o.OrderType.Name + "��" + o.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + o.RowNo.ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
                            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        }

                        sheet.Rows[o.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.MoDate, o.MOTime.Month.ToString() + "-" + o.MOTime.Day.ToString());
                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.MoTime, o.MOTime.ToShortTimeString());

                        if (o.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(o.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                            //�Ƿ���ʾͨ����������
                            if (isDisplayRegularName)
                            {
                                if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                {
                                    if (o.OrderType.ID == "CD")
                                    {
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + o.Usage.Name + " " + (o.Frequency.ID + " ") + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + o.Memo);
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
                                        sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + (o.Item.ID == "999" ? "" : (o.Frequency.ID + " ")) + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + o.Memo);
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
                                    sheet.SetValue(o.RowNo, (Int32)ShortOrderColunms.ItemName, o.Item.Name + " " + o.Usage.Name + " " + (o.Item.ID == "999" ? "" : (o.Frequency.ID + " ")) + o.DoseOnce.ToString() + (o.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + o.Memo);
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
                            
                            //��ҩƷҲ��ʾ�����͵�λ
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

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread2.Sheets[MaxPageNo];


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

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
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
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
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
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
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

                        //��ҩƷҲ��ʾ�����͵�λ
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

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
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
                                    activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
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
                                activeSheet.SetValue(activeRow, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Frequency.ID + " ") + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
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
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        private void AddObjectToFpShortAfter()
        {

            #region �������

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNo = new Hashtable();
            ArrayList alPageNo = new ArrayList();

            int MaxPageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag) + 1;
            int MaxRowNo = -1;

            int row = this.neuSpread2.ActiveSheet.ActiveRowIndex;

            if (this.neuSpread2.ActiveSheet.Rows[row].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            Neusoft.HISFC.Models.Order.Inpatient.Order ord = this.neuSpread2.ActiveSheet.Rows[row].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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

            #region �������

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

            #region ����һ��Sheet

            if (this.neuSpread2.Sheets.Count > 1)
            {
                for (int j = this.neuSpread2.Sheets.Count - 1; j > this.neuSpread2.ActiveSheetIndex; j--)
                {
                    this.neuSpread2.Sheets.RemoveAt(j);
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

            this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread2.Sheets[MaxPageNo];


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
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(sheet);

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNo++;
                    activeSheet.SheetName = "��" + (Neusoft.FrameWork.Function.NConvert.ToInt32(MaxPageNo)).ToString() + "ҳ";

                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoDate, oTemp.MOTime.Month.ToString() + "-" + oTemp.MOTime.Day.ToString());
                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.MoTime, oTemp.MOTime.ToShortTimeString());

                    if (oTemp.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaItemHelper.GetObjectFromID(oTemp.Item.ID) as Neusoft.HISFC.Models.Pharmacy.Item;

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(��¼ҽ��)" + */" " + oTemp.Memo);
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
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(��¼ҽ��)" +*/ " " + oTemp.Memo);
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
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(��¼ҽ��)" + */" " + oTemp.Memo);
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
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(��¼ҽ��)" +*/ " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
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

                        //�Ƿ���ʾͨ����������
                        if (isDisplayRegularName)
                        {
                            if (phaItem != null && !string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                if (oTemp.OrderType.ID == "CD")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, phaItem.NameCollection.RegularName + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit +/* "(��¼ҽ��)" + */" " + oTemp.Memo);
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
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                                }
                                else if (oTemp.OrderType.ID == "BL")
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit +/* "(��¼ҽ��)" + */" " + oTemp.Memo);
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
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + "(��Ժ��ҩ)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString() + (oTemp.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit + /*"(��¼ҽ��)" +*/ " " + oTemp.Memo);
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
                        if (oTemp.Item.MinFee.ID == "037" || oTemp.Item.MinFee.ID == "038" || oTemp.Item.Name.IndexOf("��Ƥ") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
                        }
                        else
                        {
                            if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNo) % rowNum, (Int32)ShortOrderColunms.ItemName, oTemp.Item.Name + " " + (oTemp.Item.ID == "999" ? "" : (oTemp.Frequency.ID + " ")) + oTemp.Qty.ToString() + oTemp.Unit +/* "(��¼ҽ��)" + */" " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency));
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

            int pag = 0;

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
                            }

                            //�����ش���ʾ������
                            this.SetRePrintContentsForLong();

                            //������ʾ���߸ı���Ϣ
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
        /// ��������Ŀ
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
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.RecipeDoct, "");//ִ�����ڲ���
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmNurse, "");//ִ��ʱ�䲻��
                        this.neuSpread2.ActiveSheet.SetValue(i, (Int32)ShortOrderColunms.ConfirmDate, "");//ִ��ʱ�䲻��
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

        #region ��ȡ��ӡ��ʾ

        /// <summary>
        /// ��ȡ��ӡ��ʾ
        /// </summary>
        /// <returns></returns>
        private bool GetIsPrintAgainForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return false;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return false;
                    }
                }
            }

            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                errText = "���ҳ�����";
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "���ҳ�����";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.pInfo.ID, "1");

            if (pageNo > maxPageNo + 1)
            {
                errText = "��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ��";
                return false;
            }

            /*
            if(pageNo == maxPageNo + 1 && maxPageNo != -1)
            {
                int maxRowNo = this.orderManager.GetMaxRowNoByPageNo(this.pInfo.ID,"1",maxPageNo.ToString());

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
                    errText = "��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ����";
                    return false;
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
            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return false;
                    }
                }
            }

            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                errText = "���ҳ�����";
                return false;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "���ҳ�����";
                return false;
            }

            int maxPageNo = this.orderManager.GetMaxPageNo(this.pInfo.ID, "0");

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
                    errText = "��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ����";
                    return false;
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
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();


            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
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
                        MessageBox.Show("ʵ��ת������");
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
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "1", "0") <= 0)
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
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
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
        private int UpdateOrderForShort()
        {
            Neusoft.HISFC.BizLogic.Order.Order myOrder = new Neusoft.HISFC.BizLogic.Order.Order();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
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
                        MessageBox.Show("ʵ��ת������");
                        return -1;
                    }

                    if (oT.GetFlag == "0")
                    {
                        if (myOrder.UpdatePageNoAndRowNo(this.pInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҳ�����" + oT.Item.Name);
                            return -1;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", "0") <= 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������ȡ��־����" + oT.Item.Name);
                                return -1;
                            }
                        }
                        else
                        {
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "1", "0") <= 0)
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
                            if (myOrder.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
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
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

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
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return;
                    }

                    if (oT.GetFlag == "0")
                    {
                        //this.neuSpread2.ActiveSheet.SetValue(i,2,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//ִ�����ڲ���
                        //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//ִ��ʱ�䲻��
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
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//ִ�����ڲ���
                            //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//ִ��ʱ�䲻��
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
        /// ���ó���ҽ���ش���ʾ����
        /// </summary>
        private void SetRePrintContentsForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
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
        /// ������ʱҽ���ش���ʾ����
        /// </summary>
        private void SetRePrintContentsForShort()
        {
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            int pageNo = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
                return;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    Neusoft.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as Neusoft.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("ʵ��ת������");
                        return;
                    }

                    //this.neuSpread2.ActiveSheet.SetValue(i,2,"");
                    //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.Unit,"");
                    //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.RecipeDoct,"");//ִ�����ڲ���
                    //this.neuSpread2.ActiveSheet.SetValue(i,(Int32)ShortOrderColunms.ConfirmNurse,"");//ִ��ʱ�䲻��
                    //this.neuSpread2.ActiveSheet.SetValue(i,8,"");//ִ��ʱ�䲻��
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
        /// ����Fp��ʽ
        /// </summary>
        /// <param name="longOrder">�Ƿ���</param>
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
        /// ������ʾ
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
                        //����״��������Ѵ�ӡ���в�����ʾ
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
        /// ������ʾ
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
                        //����״��������Ѵ�ӡ���в�����ʾ
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
            Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatient.QueryPatientInfoByInpatientNO(patientNo);

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
            //    Neusoft.FrameWork.Models.NeuObject info = alInTimes[i] as Neusoft.FrameWork.Models.NeuObject;

            //    if (i == 0)
            //    {
            //        root.Text = "סԺ��Ϣ:" + "[" + info.Name + "]" + "[" + patientNo + "]";
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

        #region ����ҽ��

        /// <summary>
        /// ���õ�ǰ����ҽ������ӡ״̬
        /// </summary>
        private void ReSet(EnumOrderType type)
        {
            if (this.pInfo == null || this.pInfo.ID == "")
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
            if (this.orderManager.ResetOrderPrint("-1", "-1", pInfo.ID, orderType, "0") == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ��!" + this.orderManager.Err);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("���óɹ�!");

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
        /// ѡ��
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
            this.AddObjectToFpLongAfter(this.neuSpread1.ActiveSheetIndex,this.neuSpread1.ActiveSheet.ActiveRowIndex);
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
        /// ��ʾҳ��
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
        private void neuSpread1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.menu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "�����������ҽ��";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);

                System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                printDateItem.Text = "ֻ�����������ҽ��ֹͣʱ��";
                printDateItem.Click += new EventHandler(printDateItem_Click);

                System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                splitMenuItem.Text = "�Ӹ���ҽ����������һҳ";
                splitMenuItem.Click += new EventHandler(splitMenuItem_Click);

                this.menu.MenuItems.Add(printMenuItem);
                this.menu.MenuItems.Add(printDateItem);
                this.menu.MenuItems.Add(splitMenuItem);
                this.menu.Show(this.neuSpread1, new Point(e.X, e.Y));
            }
        }
        /// <summary>
        /// �Ҽ���ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
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
                this.menu.Show(this.neuSpread2, new Point(e.X, e.Y));
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

        #region IPrintOrder ��Ա

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
    /// </summary>
    public class AlSort : System.Collections.IComparer
    {
        #region IComparer ��Ա

        /// <summary>
        /// �ȽϷ���
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
        MoDate,

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        MoTime,

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