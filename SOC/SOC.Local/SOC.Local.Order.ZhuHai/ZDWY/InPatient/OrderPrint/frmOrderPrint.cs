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
    /// ҽ������ӡ������
    /// 
    /// ˵����
    /// 1���޸�ǰ�ȿ������룬��ҪäĿ�޸�
    /// 2��Ŀǰ��ӡӦ���Ѿ��ܹ����ݺܶ��ʽ�ˣ�������Ŀֻ�����InitSheet
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

        #region ����

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        Function funMgr = new Function();

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
        /// ���������б�
        /// </summary>
        ArrayList alShort_ALL = new ArrayList();

        /// <summary>
        /// �����б�
        /// </summary>
        ArrayList alShort_Normal = new ArrayList();

        /// <summary>
        /// �����б�
        /// </summary>
        ArrayList alOperate = new ArrayList();

        /// <summary>
        /// ������ҽ���б�
        /// </summary>
        ArrayList alShort_UCUL = new ArrayList();



        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo;

        FarPoint.Win.Spread.SheetSkin sheetSKin_Black = new FarPoint.Win.Spread.SheetSkin("CustomSkin1",
                    System.Drawing.Color.White,
                    System.Drawing.Color.Empty,
                    System.Drawing.Color.Empty,
            //�ߵ���ɫ
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
            //�ߵ���ɫ
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
        /// ������������ѯ������ʶ
        /// </summary>
        private int orderQueryCount = 1;

        /// <summary>
        /// �Ƿ���ʾ�������ӡ// {1B360F77-7C78-4614-B61A-D9542D57C603}
        /// </summary>
        private bool isShowULOrderPrint = false;


        /// <summary>
        /// ����ҽ����XML����
        /// </summary>
        string LongOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\LongOrderPrintSetting.xml";

        /// <summary>
        /// ��ʱҽ����XML����
        /// </summary>
        string shortOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\ShortOrderPrintSetting.xml";


        string operateOrderSetXML = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OperateOrderPrintSetting.xml";

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
        /// ÿҳ��ӡ������
        /// </summary>
        private int defaultRowNum = 25;

        /// <summary>
        /// ÿ��ֽ�ĸ߶�
        /// </summary>
        private int pageHeight = 890;

        /// <summary>
        /// ��ӡ��ʽ
        /// </summary>
        private EnumPrintType OrderPrintType = EnumPrintType.PrintWhenPatientOut;

        /// <summary>
        /// ��ǰ��¼�Ƿ����Ա
        /// </summary>
        private bool isManager = ((FS.HISFC.Models.Base.Employee)FrameWork.Management.Connection.Operator).IsManager;

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
            #region ���ò˵�

            this.tbExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbAllPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbRePrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C�ش�);
            this.tbQuery.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);
            this.tbReset.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbSetting.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S����);

            this.tbDeleteLongProfile.Visible = false;
            this.tbDeleteShortProfile.Visible = false;

            //����ɾ�������ļ��İ�ť�˵�

            #endregion

            #region ��ʼ��ҽ��Farpoint

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



            this.ucShortOrderBillHeader.Header = "��  ʱ  ҽ  ��  ��";
            this.ucLongOrderBillHeader.Header = "��  ��  ҽ  ��  ��";
            this.ucOperateBillHeader.Header = "��  ��  ҽ  ��  ��";

            #endregion

            #region ��ӡ������ع����Ƿ���ã�����Ա���ã�

            this.tbReset.Visible = true;

            this.ResetCurrentLong.Visible = true;
            this.ResetCurrentShort.Visible = true;
            this.RefreshLong.Visible = isManager;
            this.RefreshShort.Visible = isManager;

            //���Ŀǰû�ð�
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

            #region ���Ʋ���

            //����ҽ�Ʋ�������ӡʱ�Ƿ���ʾͨ������������ʾҩƷ��
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
                    this.defaultRowNum = FS.FrameWork.Function.NConvert.ToInt32(node.InnerText);
                }
                this.nuRowNum.Value = this.defaultRowNum;

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

            #endregion

            if (this.tcControl.SelectedTab == this.tpLong)
            {
                orderQueryCount = 1;
            }
            QueryPatientOrder();
        }

        /// <summary>
        /// ��ȡ��ǰ������ҽ��������
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
        /// ��ȡ��ǰ������ҽ��������
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
        /// ��ȡ��ǰ��ҽ������ͷ
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
        /// ��ȡ��ǰ��ҽ������ͷ
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
        /// ��ȡ��ǰ��ҽ������ͷ
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

        #region ������������ʽ

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

        #region ��ӡ��������

        /// <summary>
        /// ���ô�ӡʱ�Ƿ�Ԥ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbxPreview_CheckedChanged(object sender, EventArgs e)
        {
            this.SetPrintValue("Ԥ����ӡ", "1");
        }

        /// <summary>
        /// ��ӡģʽѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// ��ʼ��ҽ��Sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="orderType"></param>
        /// <param name="rowCount">����</param>
        private void InitSheet(FarPoint.Win.Spread.SheetView sheet, EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                #region ��ʼ������
                sheet.Reset();
                sheet.ColumnCount = (Int32)EnumCol.Max;
                sheet.ColumnHeader.RowCount = 2;
                sheet.RowCount = 0;
                //sheet.RowCount = rowCount;
                sheet.RowHeader.ColumnCount = 0;
                sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

                FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
                textCellType1.WordWrap = true;

                #region ����

                //��һ��
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.BeginDate).Text = "  ��  ҽ  ��  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).Text = "  ҽ  ��  ��  ��  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCDate).Text = "  ͣ  ҽ  ��  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.OnceDose).Text = "ÿ����";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Usage).Text = "�÷�";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Freq).Text = "Ƶ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Memo).Text = "��ע";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ExecNurse).Text = "ִ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Qty).Text = "����";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Cost).Text = "���";


                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmDate).Text = "�������";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmTime).Text = "���ʱ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCFlag).Text = "ֹͣ���";

                //�ڶ���
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginDate).Text = "����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginTime).Text = "ʱ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.RecipeDoct).Text = "ҽʦǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmNurse).Text = "��ʿǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.CombFlag).Text = "��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ItemName).Text = "  ����ҽ��  ";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.OnceDose).Text = "ÿ����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Usage).Text = "�÷�";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Freq).Text = "Ƶ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Memo).Text = "��ע";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDate).Text = "����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCTime).Text = "ʱ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDoct).Text = "ҽʦǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCConfirmNurse).Text = "��ʿǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ExecNurse).Text = "ִ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Qty).Text = "����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Cost).Text = "���";

                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmDate).Text = "�������";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmTime).Text = "���ʱ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCFlag).Text = "ֹͣ���";

                //���кϲ�
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

                //���ÿɼ���
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

                //������������
                sheet.Columns.Get((Int32)EnumCol.ItemName).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.OnceDose).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Freq).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Usage).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Memo).CellType = textCellType1;

                //���ÿ��
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
                #region ��ʼ������
                sheet.Reset();
                sheet.ColumnCount = (Int32)EnumCol.Max;
                sheet.ColumnHeader.RowCount = 2;
                sheet.RowCount = 0;
                //sheet.RowCount = rowCount;
                sheet.RowHeader.ColumnCount = 0;
                sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

                FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
                textCellType1.WordWrap = true;

                #region ����

                //��һ��
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.BeginDate).Text = "  ��  ҽ  ��  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.RecipeDoct).Text = "ҽʦǩ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmNurse).Text = "��ʿǩ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.CombFlag).Text = "  ҽ  ��  ��  ��  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCDate).Text = "  ִ  ��  ";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.OnceDose).Text = "ÿ����";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Usage).Text = "�÷�";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Freq).Text = "Ƶ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Memo).Text = "��ע";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ExecNurse).Text = "ִ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Qty).Text = "����";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.Cost).Text = "���";

                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmDate).Text = "�������";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.ConfirmTime).Text = "���ʱ��";
                sheet.ColumnHeader.Cells.Get(0, (Int32)EnumCol.DCFlag).Text = "ֹͣ���";

                //�ڶ���
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginDate).Text = "����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.BeginTime).Text = "ʱ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.RecipeDoct).Text = "ҽʦǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmNurse).Text = "��ʿǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.CombFlag).Text = "��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ItemName).Text = "  ����ҽ��  ";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.OnceDose).Text = "ÿ����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Usage).Text = "�÷�";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Freq).Text = "Ƶ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Memo).Text = "��ע";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDate).Text = "����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCTime).Text = "ʱ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCDoct).Text = "ǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCConfirmNurse).Text = "��ʿǩ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ExecNurse).Text = "ִ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Qty).Text = "����";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.Cost).Text = "���";

                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmDate).Text = "�������";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.ConfirmTime).Text = "���ʱ��";
                sheet.ColumnHeader.Cells.Get(1, (Int32)EnumCol.DCFlag).Text = "ֹͣ���";

                //���кϲ�
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

                //���ÿɼ���
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

                //������������
                sheet.Columns.Get((Int32)EnumCol.ItemName).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.OnceDose).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Freq).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Usage).CellType = textCellType1;
                sheet.Columns.Get((Int32)EnumCol.Memo).CellType = textCellType1;

                //���ÿ��
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

            #region ����ÿ�еĸ߶�

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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");
            Application.DoEvents();

            //try
            //{
            ArrayList alAll = new ArrayList();

            #region ����ҽ�����ͷֱ��ȡҽ�����Լ�С������

            if (tcControl.SelectedIndex == 0)
            {
                alLong.Clear();
                alAll = this.orderManager.QueryPrnOrderByOrderType(this.myPatientInfo.ID, "1");//����ҽ��
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

                alAll = this.orderManager.QueryPrnOrderByOrderType(this.myPatientInfo.ID, "0");//��ʱҽ��
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
                //�������
                alOperate.Clear();

                alAll = this.orderManager.QueryPrnOperateOrderByInpatientNO(this.myPatientInfo.ID);//��ʱҽ��

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
            //    MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        /// <summary>
        /// ����Farpoint��ʽ
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
            #region ����ÿ�еĸ߶�

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

        #region ���

        /// <summary>
        /// �������ҽ��������
        /// </summary>
        private void Clear(EnumOrderType orderType)
        {
            this.GetFpOrder(orderType).Sheets.Clear();

            FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
            this.InitSheet(sheet, orderType);
            this.GetFpOrder(orderType).Sheets.Add(sheet);

            if (orderType == EnumOrderType.Long)
            {
                this.lblPageLong.Text = "��1ҳ";
            }
            else if (orderType == EnumOrderType.Operate)
            {
                this.lblPageOperatets.Text = "��1ҳ";
            }
            else
            {
                this.lblPageShort.Text = "��1ҳ";
            }
        }

        #endregion

        /// <summary>
        /// ��ȡ��ӡ��ʱ�䣺��ʼʱ�����ʱ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private DateTime GetPrintDate(FS.HISFC.Models.Order.Order order)
        {
            if (tcControl.SelectedIndex == 0)   //{3066617c-1fcd-4dea-a215-690210bb0f96}
            {
                return order.BeginTime;
            }
            else      //��ʱҽ��ȡҽ������ʱ��
            {
                return order.MOTime;
            }
        }

        /// <summary>
        /// ���뵽ҽ������ʾ
        /// </summary>
        /// <param name="isLong">�Ƿ���</param>
        /// <param name="alOrder"></param>
        private void AddOrderToFP(EnumOrderType orderType, ArrayList alOrder, int MaxPageNo, int MaxRowNo)
        {
            if (alOrder.Count <= 0)
            {
                return;
            }

            //����ҳ�롢�кš���š������ ����
            alOrder.Sort(new OrderComparer());

            //���ڴ���ת�ƻ�ҳ
            string deptCode = "";

            //����ҽ�����
            string speOrderType = "";

            //�洢���һ����������ҽ������
            //�������� ����ҽ��->����->����ҽ�� ʶ�� ����->����ҽ�� �費��Ҫ��ҳ
            string deptOrderType = "";

            //������������ҽ��,��¼�ϸ�ҽ��״̬����������һ���Ƚ�
            FS.HISFC.Models.Order.Inpatient.Order recordOrder = null;

            //�п���ҽ������ҽ��������һЩ�¿�����Ŀ��Ȼ�����˳��������ӡ������ҳ�����ˣ���������������¼
            Hashtable hsRecord = new Hashtable();

            foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrder)
            {
                //if (inOrder.RowNo >= this.rowNum)
                //{
                //    MessageBox.Show(inOrder.OrderType.Name + "��" + inOrder.Item.Name + "����ʵ�ʴ�ӡ�к�Ϊ" + (inOrder.RowNo + 1).ToString() + "���������õ�ÿҳ�������" + this.rowNum.ToString() + ",����ϵ��Ϣ�ƣ�", "����");
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

                #region ��ʾ

                //�Ѵ�ӡ��
                if (inOrder.GetFlag != "0")
                {
                    //��ҳ
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
                //δ��ӡ
                else
                {
                    if ((MaxRowNo % this.defaultRowNum == 0) ||

                        MaxRowNo > defaultRowNum ||

                        //ת��ҲҪ�Զ���ҳ
                        (this.isShiftDeptNextPag &&
                        !string.IsNullOrEmpty(deptCode)
                        //&& deptCode.Trim() != inOrder.ReciptDept.ID
                        && deptCode.Trim() != inOrder.Patient.PVisit.PatientLocation.Dept.ID

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
                        //if (MessageBox.Show("��������ҽ�����Ƿ��Զ���ҳ��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
                    this.GetFpOrder(orderType).Sheets[MaxPageNo].SheetName = "��" + (MaxPageNo + 1).ToString() + "ҳ";
                }

                //ʼ�մ���һ��ҽ����״̬
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
        /// ÿ�����
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
            //�������⴦��
            //ֹͣ�д�ӡ��˻�ʿ�������Ϣ
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
            //    sheet.SetValue(row, (Int32)EnumCol.DCDoct, inOrder.Nurse.Name + " ȡ��");
            //}

            sheet.SetValue(row, (Int32)EnumCol.CombNO, inOrder.Combo.ID);

            sheet.Rows[row].Tag = inOrder;
        }

        /// <summary>
        /// ��ȡ��ʾ��ҽ������
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isLong"></param>
        /// <returns></returns>
        private string GetOrderItemName(FS.HISFC.Models.Order.Inpatient.Order order, EnumOrderType orderType)
        {
            //Ƶ�δ���
            string strFreq = " " + order.Frequency.ID;
            if (!order.OrderType.IsDecompose)
            {
                if (SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(order.Frequency.ID) <= 1)
                {
                    strFreq = " ";
                }
            }

            //����ҽ������
            string strSpeOrder = "";
            if (order.OrderType.ID == "CD")
            {
                strSpeOrder = " (��Ժ��ҩ)";
            }
            else if (order.OrderType.ID == "BL")
            {
                strSpeOrder = " (��¼ҽ��)";
            }

            //�Ӽ����
            string strEmg = "";
            if (order.IsEmergency)
            {
                strEmg = " [��] ";
            }

            //�����Ա����
            string byoStr = "";
            if (!order.OrderType.IsCharge
                || order.Item.ID == "999")
            {
                if (!order.Item.Name.Contains("�Ա�")
                    && !order.Item.Name.Contains("����"))
                {
                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        byoStr = "[�Ա�]";
                    }
                    else
                    {
                        byoStr = "[����]";
                    }
                }
            }

            //��������ʾ
            string strFirstDay = "";
            if (order.OrderType.IsDecompose)
            {
                strFirstDay = " ���� " + order.FirstUseNum;
            }

            string showName = "";

            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                string itemName = order.Item.Name;
                //�Ƿ���ʾͨ����������
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

                showName = itemName //��Ŀ����
                   + byoStr //���б��
                   + strEmg //�Ӽ����
                   + " " + DoseOnce + order.DoseUnit //ÿ����
                   + strQTY //����
                   + " " + order.Usage.Name //�÷�
                   + strFreq //Ƶ��
                   + " " + order.Memo //��ע
                   + strFirstDay //������
                   + " " + strSpeOrder; //����ҽ�����



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


                #region �ɹ��򷽷�
                /*
                //������ʳ����ʾƵ��
                if (order.Item.Name.Contains("����")
                    || order.Item.Name.Contains("��ʳ")
                    || order.Item.Name.Contains("��ʳ")
                    || order.Item.Name.Contains("��ʳ")
                    || order.Item.ID == "999"
                    || sysCode == "MF" //��ʳ
                    || sysCode == "UN" //������
                    )
                {
                    showName = order.Item.Name  //��Ŀ����
                        + byoStr //���б��
                        + strEmg //�Ӽ����
                        + strQTY //����
                        + " " + order.Memo //��ע
                        + " " + strSpeOrder; //����ҽ�����

                }
                else
                {
                    showName = order.Item.Name //��Ŀ����
                        + byoStr//���б��
                        + strEmg //�Ӽ����
                        + strQTY //����
                        + strFreq //Ƶ��
                        + " " + order.Memo //��ע
                        + " " + strSpeOrder; //����ҽ�����
                }
                */
                #endregion

                #region ���������ƹ���{0E78C368-911F-4522-85CF-0192E7D58A49}
                showName = order.Item.Name //��Ŀ����
                        + byoStr//���б��
                        + strEmg //�Ӽ����
                        + strQTY //����
                        + strFreq //Ƶ��
                        + " " + order.Memo //��ע
                        + " " + strSpeOrder; //����ҽ�����
                #endregion
            }

            ////���ϵ����� ��ʾ��ȡ��������
            //if (!order.OrderType.IsDecompose
            //    && order.Status == 3)
            //{
            //    showName += " ȡ��";

            //    //string itemName = showName;
            //    //itemName = " ȡ��".PadLeft(GetLength(itemName), ' ');
            //    //showName = itemName;
            //}

            if (!order.OrderType.IsDecompose
              && order.Status == 3)
            {
                //showName += "��ȡ����";   //{de5fac98-bd9c-44c4-844a-5930d230a75d}
                //{a2a72d4f-3b6d-4111-81d8-4af3a9eb6670}

                showName += "��ҽ��ȡ����ȷ��ҽʦ��" + order.DCOper.Name + "   " + order.EndTime.ToString("yyyy-MM-dd HH:mm") + "��";
            }
            return showName;
        }

        /// <summary>
        /// ת�Ʒ�ҳ����ӵ�Fp
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        private void ShiftPage(EnumOrderType orderType)
        {
            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(orderType).ActiveSheet;
            int rowIndex = sheet.ActiveRowIndex;
            if (rowIndex == 0)
            {
                MessageBox.Show("�Ѿ��ǵ�һ�У�����������һҳ��");
                return;
            }

            if (sheet.Rows[rowIndex].Tag == null)
            {
                MessageBox.Show("��ѡ��ĿΪ�գ�");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = sheet.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            if (ord.GetFlag != "0")
            {
                MessageBox.Show("ҽ��[" + ord.Item.Name + "]�Ѿ���ӡ������������һҳ��");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag);

            //��ȡʣ������
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

            //�������
            GetFpOrder(orderType).Sheets.Count = pageNo + 1;
            sheet.RowCount = rowIndex;
            sheet.RowCount = this.defaultRowNum;
            for (int row = rowIndex; row < defaultRowNum; row++)
            {
                sheet.Rows[row].Height = sheet.Rows[0].Height;
            }

            //��䵽������ʾ
            AddOrderToFP(orderType, alShiftOrder, pageNo, 0);

            return;
        }

        /// <summary>
        /// ����Ϻ�
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

        #region ��ӡ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReprint"></param>
        /// <param name="fromLine">��ӡ����ʼ��</param>
        /// <param name="endLine">��ӡ�Ľ�ֹ��</param>
        /// <param name="isDCReprint">�Ƿ񲹴�ֹͣ��Ϣ������Ļ����Ǵ�ӡ������Ϣ</param>
        private int PrintPage(bool isReprint, int fromLine, int endLine, bool isDCRepint)
        {
            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).ActiveSheet;

            if (isReprint)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrd = sheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                if (inOrd.GetFlag == "0")
                {
                    MessageBox.Show("��ҳҽ������δ��ӡ������Ҫ�ش�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;
                }
            }

            string errText = "";

            try
            {
                //����ǰҳ�����һ�������ݣ���֤�ܹ���ӡ
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
                                MessageBox.Show(errText, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            return -1;
                        }

                        if (this.IsPrintAgain())
                        {
                            DialogResult r = MessageBox.Show("��ҳҽ������ȫ����ӡ��ϣ��Ƿ���Ҫ���´�ӡ��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

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


                #region ��ӡ

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

                #region ��ӡ����²���ʾ

                if (isReprint
                    || fromLine > 0)
                {
                    this.InitFPShow(GetFpOrder(OrderType).ActiveSheet, false);
                }
                else
                {
                    DialogResult dia;

                    frmNotice frmNotice = new frmNotice();
                    frmNotice.label1.Text = "����ҽ�����Ƿ�ɹ���";
                    frmNotice.ShowDialog();
                    dia = frmNotice.dr;
                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("ȷ������û�гɹ�������������ҽ�������ֿ��У�", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //ȷ������û�гɹ���û��˵��
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

                        #region ��ӡ�곤�����ѯҽ��ҳ���Ƿ�����

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
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            PrintPage(false, -1, -1, false);
        }

        private bool isTip = true;

        /// <summary>
        /// ��ӡȫ��// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
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
        /// ���´�ӡ
        /// </summary>
        private void PrintAgain()
        {
            PrintPage(true, -1, -1, false);
        }

        /// <summary>
        /// ��������Ŀ
        /// </summary>
        /// <param name="isDCReprint">�Ƿ񲹴�ֹͣ��Ϣ������Ļ����Ǵ�ӡ������Ϣ</param>
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
                MessageBox.Show("��Ŀ[" + inOrder.Item.Name + "]��δ��ӡ,����Ҫ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #region ��ʿδ��˲��ܴ�ӡ

            if (inOrder != null
                && !FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                //ҽ��״̬,0������1��ˣ�2ִ�У�3���ϣ�4������5��Ҫ�ϼ�ҽ����ˣ�6�ݴ棬7Ԥֹͣ

                if (inOrder.Status == 5)
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]��Ҫ�ϼ�ҽ����˺󣬲��ܴ�ӡ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (inOrder.Status == 6)
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]Ϊ�ݴ�״̬��Ŀǰ���ܴ�ӡҽ������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ("0,1,2,5,6".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.Nurse.ID))
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]��ʿ��δ�˶ԣ����ܴ�ӡҽ������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if ("3,4,7".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.DCNurse.ID))
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]��ʿ��δ�˶ԣ����ܴ�ӡҽ������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion

            if (MessageBox.Show("ȷ��Ҫ�ش���Ŀ[" + inOrder.Item.Name + "]��\r\n\r\n��ע�����ҽ������", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            PrintPage(false, rowIndex, rowIndex, isDCRepint);
        }

        #endregion

        #region ��ȡ��ӡ��ʾ

        /// <summary>
        /// �Ƿ��ش� true����false�ش�
        /// �����ҳ�Ѿ�ȫ����ӡ��ϣ�����ʾ�ش�
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
                        MessageBox.Show("ʵ��ת������");
                        return false;
                    }

                    if (order.GetFlag == "0")
                    {
                        return false;
                    }
                    else if (order.GetFlag == "1")
                    {
                        if (order.DCOper.OperTime != DateTime.MinValue

                            ////�������⴦��
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
        /// ��ȡҳ����ҽ��������
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
        /// �Ƿ�����ڱ�ҳ����
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
                errText = "���ҳ�����";
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag);

            if (pageNo < 0)
            {
                errText = "���ҳ�����\r\nҳ��Ϊ������";
                return false;
            }

            #region �ж��Ƿ��л�ʿδ���ҽ��

            string warnInfo = "";
            int warnCount = 0;
            for (int row = 0; row < sheet.RowCount; row++)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (inOrder != null)
                {
                    //ҽ��״̬,0������1��ˣ�2ִ�У�3���ϣ�4������5��Ҫ�ϼ�ҽ����ˣ�6�ݴ棬7Ԥֹͣ

                    if (inOrder.Status == 5)
                    {
                        errText = "ҽ��[" + inOrder.Item.Name + "]��Ҫ�ϼ�ҽ����˺󣬲��ܴ�ӡ��";
                    }
                    else if (inOrder.Status == 6)
                    {
                        errText = "ҽ��[" + inOrder.Item.Name + "]Ϊ�ݴ�״̬��Ŀǰ���ܴ�ӡҽ������";
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
                errText = "������Ŀ��ʿ��δ�˶ԣ����ܴ�ӡ��\r\n\r\n" + warnInfo;

                return false;
            }

            #endregion

            #region �ж�ǰҳ�Ƿ��ӡ��ȫ

            //��ȡ�Ѵ�ӡ�����ҳ����
            int maxPageNo = 0;
            int maxRowNo = 0;

            int UCULFlag = 0;//0 ��UCUL��1 UCUL������ȫ��
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
                //��ҳ��ӡ��
                if (pageNo > maxPageNo + 1)
                {
                    if (isCanIntervalPrint)
                    {
                        if (MessageBox.Show("��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ���Ƿ������ӡ��" + (pageNo + 1).ToString() + "ҳ��\r\n\r\n������ӡ����������ǰ" + (pageNo + 1).ToString() + "ҳ��ӡ��ǣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        errText = "��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ��\r\n���ȴ�ӡ��ҳ֮ǰ��ҽ������";
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
                            if (MessageBox.Show("��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ�����Ƿ������ӡ��" + (pageNo + 1).ToString() + "ҳ��\r\n\r\n������ӡ����������ǰ" + (pageNo + 1).ToString() + "ҳ��ӡ��ǣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            errText = "��" + (maxPageNo + 1).ToString() + "ҳ����δ��ӡҽ����";
                            return false;
                        }
                    }
                }
            }
            else if (pageNo != 0)
            {
                if (isCanIntervalPrint)
                {
                    if (MessageBox.Show("��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ���Ƿ������ӡ��" + (pageNo + 1).ToString() + "ҳ��\r\n\r\n������ӡ����������ǰ" + (pageNo + 1).ToString() + "ҳ��ӡ��ǣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
                else
                {
                    errText = "��" + (maxPageNo + 2).ToString() + "ҳҽ������δ��ӡ��\r\n���ȴ�ӡ��ҳ֮ǰ��ҽ������";
                    return false;
                }
            }
            #endregion

            if (isTip)
            {
                MessageBox.Show("��ȷ���ѷ����" + (pageNo + 1).ToString() + "ҳҽ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private int UpdatePrintFlag(FarPoint.Win.Spread.SheetView sheet)
        {
            if (sheet.Tag == null)
            {
                MessageBox.Show("���ҳ�����");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag);

            if (pageNo < 0)
            {
                MessageBox.Show("���ҳ�����");
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
                        MessageBox.Show("ʵ��ת������");
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
                        MessageBox.Show("������ȡ��־����\r\n��Ŀ��" + inOrder.Item.Name + "\r\n\r\n" + funMgr.Err);
                        return -1;
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
        /// ����ҳ��Ŵ�ӡ
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
                                        view.SetValue(j, (Int32)EnumCol.CombFlag, "��");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (Int32)EnumCol.CombFlag, "��");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (Int32)EnumCol.CombFlag, "��");
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
                                            viewNext.SetValue(j, (Int32)EnumCol.CombFlag, "��");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (Int32)EnumCol.CombFlag, "��");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (Int32)EnumCol.CombFlag, "��");
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
        /// ��ȡSheet��һ��ҽ��
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
        /// ��ʼ����ʾ��ʽ
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="isRefresh">�Ƿ�ˢ�µ�ǰ����Ĵ�ӡ���</param>
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
        /// ���ô�ӡ��ʾ
        /// </summary>
        /// <param name="isReprint">�Ƿ���ҳ�ش�</param>
        /// <param name="singleLine">�����������</param>
        /// <param name="isDCReprint">�Ƿ񲹴�ֹͣ��Ϣ������Ļ����Ǵ�ӡ������Ϣ</param>
        /// <returns></returns>
        private int SetPrintShow(bool isReprint, int fromLine, int endLine, bool isDCReprint)
        {
            FarPoint.Win.Spread.SheetView sheet = this.GetFpOrder(OrderType).ActiveSheet;
            FS.HISFC.Models.Order.Inpatient.Order firstOrder = sheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            sheet.PrintInfo.ShowBorder = true;

            #region �����ӡ

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

                //���ñ���ɫΪ��ɫ
                for (int row = 0; row < sheet.RowCount; row++)
                {
                    for (int col = 0; col < sheet.ColumnCount; col++)
                    {
                        sheet.Cells[row, col].ForeColor = Color.White;

                        if (row >= fromLine && row <= endLine)
                        {
                            //ȡ����������ӡ��ȡ��������
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

                                    //�������⴦��
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
                                    //    itemName = "".PadLeft(GetLength(itemName), ' ') + " ȡ��";
                                    //    sheet.Cells[row, col].Text = itemName;
                                    //}

                                    if (col == (Int32)EnumCol.DCDoct)
                                    {
                                        sheet.Cells[row, col].ForeColor = Color.Black;

                                        string itemName = sheet.Cells[row, col].Text.Replace(" ȡ��", "");
                                        itemName = "".PadLeft(GetLength(itemName), ' ') + " ȡ��";
                                        sheet.Cells[row, col].Text = itemName;
                                    }
                                }
                            }
                            else
                            {
                                //����ĳ��ҽ��������ֹͣ��ϢҲ��ӡ������
                                //�����ߵ�Ҳ������
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

            #region ������ӡ
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
                    //�״δ�ӡ���ش�
                    if (firstOrder.GetFlag == "0"
                        || isReprint)
                    {
                        GetActBillHeader(OrderType).SetValueVisible(true);

                        //ӡˢ����
                        if (OrderPrintType == EnumPrintType.DrawPaperContinue)
                        {
                            GetActBillHeader(OrderType).SetVisible(false);
                            GetActPrintPanelPage(OrderType).Visible = false;

                            sheet.ActiveSkin = sheetSKin_White;
                        }
                        //��ֽ����
                        else if (OrderPrintType == EnumPrintType.WhitePaperContinue)
                        {
                            GetActBillHeader(OrderType).SetVisible(true);
                            GetActPrintPanelPage(OrderType).Visible = true;

                            sheet.ActiveSkin = sheetSKin_Black;
                        }

                        if (isReprint)
                        {
                            //���ñ���ɫΪ��ɫ
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
                                                //�������⴦��
                                                //�������⴦��
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

                                            //ȡ����������ӡ��ȡ��������
                                            FS.HISFC.Models.Order.Inpatient.Order inOrder2 = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                                            if (inOrder2 != null
                                                && !inOrder2.OrderType.IsDecompose
                                                && inOrder2.Status == 3)
                                            {
                                                //if (col == (Int32)EnumCol.ItemName)
                                                //{
                                                //    sheet.Cells[row, col].ForeColor = Color.Black;

                                                //    string itemName = sheet.Cells[row, col].Text;
                                                //    itemName = "".PadLeft(GetLength(itemName), ' ') + " ȡ��";
                                                //    sheet.Cells[row, col].Text = itemName;
                                                //}

                                                if (col == (Int32)EnumCol.DCDoct)
                                                {
                                                    sheet.Cells[row, col].ForeColor = Color.Black;

                                                    string itemName = sheet.Cells[row, col].Text.Replace(" ȡ��", "");

                                                    if (!isReprint)
                                                    {
                                                        itemName = "".PadLeft(GetLength(itemName), ' ') + " ȡ��";
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
                    //��������Ҫ�����Ѿ���ӡ���Ĳ���ʾ
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

                        //���ñ���ɫΪ��ɫ
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

                                            //�������⴦��
                                            if (!inOrder.OrderType.IsDecompose)
                                            {
                                                if (col == (Int32)EnumCol.DCDate
                                                || col == (Int32)EnumCol.DCTime
                                                    )
                                                {
                                                    sheet.Cells[row, col].ForeColor = Color.White;
                                                }
                                            }

                                            //ȡ����������ӡ��ȡ��������
                                            FS.HISFC.Models.Order.Inpatient.Order inOrder3 = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                                            if (inOrder3 != null
                                                && !inOrder3.OrderType.IsDecompose
                                                && inOrder3.Status == 3)
                                            {
                                                //if (col == (Int32)EnumCol.ItemName)
                                                //{
                                                //    sheet.Cells[row, col].ForeColor = Color.Black;

                                                //    string itemName = sheet.Cells[row, col].Text;
                                                //    itemName = "".PadLeft(GetLength(itemName), ' ') + " ȡ��";
                                                //    sheet.Cells[row, col].Text = itemName;
                                                //}
                                                if (col == (Int32)EnumCol.DCDoct)
                                                {
                                                    sheet.Cells[row, col].ForeColor = Color.Black;

                                                    string itemName = sheet.Cells[row, col].Text.Replace(" ȡ��", "");
                                                    itemName = "".PadLeft(GetLength(itemName), ' ') + " ȡ��";
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

            //���ñ���ɫΪ��ɫ
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
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int GetLength(string str)
        {
            return Encoding.Default.GetByteCount(str);
        }

        #region ����ҽ��

        /// <summary>
        /// ���õ�ǰ����ҽ������ӡ״̬
        /// </summary>
        /// <param name="type">���ͣ�����������</param>
        /// <param name="pagNo">����ҳ�� -1��ʾȫ������</param>
        /// <param name="ResetType">�������ͣ�ȫ��������</param>
        /// <param name="UCULFlag">0 ��UCUL��1 UCUL������ȫ��</param>
        private void ReSet(EnumOrderType type, int pagNo, string ResetType, int UCULFlag)
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
                    //message = "����ҽ����";
                    orderType = "1";
                    break;
                case EnumOrderType.Short:
                    //message = "��ʱҽ����";
                    orderType = "0";
                    break;
                default:
                    break;
            }

            #region ����ȫ��
            if (ResetType == "ALL")  //����ȫ��
            {
                if ((this.isShowULOrderPrint && UCULFlag == 1) || UCULFlag == 0)// {1B360F77-7C78-4614-B61A-D9542D57C603}
                {
                    DialogResult rr = MessageBox.Show("����ȡ��" + page + message + "�Ĵ�ӡ״̬��\r\n\r\n���ú�����ҽ������Ҫ���´�ӡ��\r\n\r\n�Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (rr == DialogResult.No)
                    {
                        return;
                    }
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (this.funMgr.ResetOrderPrint("-1", "-1", myPatientInfo.ID, orderType, "0", UCULFlag) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ��!" + this.orderManager.Err);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion

            #region ���õ�ǰҳ
            else if (ResetType == "PART")
            {
                //��ȡ�Ѵ�ӡ�����ҳ����
                int maxPageNo = 0;
                int maxRowNo = 0;
                int rev = this.funMgr.GetPrintInfo(this.myPatientInfo.ID, type == EnumOrderType.Long, ref maxPageNo, ref maxRowNo, UCULFlag);
                if (rev == -1)
                {
                    MessageBox.Show("��ѯ�Ѵ�ӡҳ����ִ���\r\n" + funMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (pagNo - 1 < maxPageNo)
                {
                    MessageBox.Show("��������Ѵ�ӡ��ҽ��������Ӻ���ǰ���ã�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if ((this.isShowULOrderPrint && UCULFlag == 1) || UCULFlag == 0)// {1B360F77-7C78-4614-B61A-D9542D57C603}
                {
                    DialogResult rr = MessageBox.Show("����ȡ��" + page + message + "�Ĵ�ӡ״̬��\r\n\r\n���ú��ҳ����ҽ������Ҫ���´�ӡ��\r\n\r\n�Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (rr == DialogResult.No)
                    {
                        return;
                    }
                }

                FarPoint.Win.Spread.SheetView sheet = GetFpOrder(type).ActiveSheet;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                #region ��ȡ��ǰѡ��ҳ����ϸ��Ŀ��ID

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
                    MessageBox.Show("����ʧ��!" + this.funMgr.Err);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion
            if ((this.isShowULOrderPrint && UCULFlag == 1) || UCULFlag == 0)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {
                MessageBox.Show("���óɹ�!");
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
            if (this.isShowULOrderPrint)// {1B360F77-7C78-4614-B61A-D9542D57C603}
            {
                //��ӡ
                if (e.ClickedItem == this.tbPrint)
                {
                    this.Print();
                }
                //��ӡȫ��
                else if (e.ClickedItem == this.tbAllPrint)
                {
                    this.AllPrint();// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
                }
                //����
                else if (e.ClickedItem == this.tbRePrint)
                {
                    this.PrintAgain();
                }
                //�رմ���
                else if (e.ClickedItem == this.tbExit)
                {
                    this.Close();
                }
                //���ó���
                else if (e.ClickedItem == this.ResetLong)
                {
                    this.ReSet(EnumOrderType.Long, -1, "ALL", 3);
                }
                //��������
                else if (e.ClickedItem == this.ResetShort)
                {
                    this.ReSet(EnumOrderType.Short, -1, "ALL", 0);
                }
                //���ü�����ҽ��
                else if (e.ClickedItem == this.ResetUCUL)
                {
                    this.ReSet(EnumOrderType.Short, -1, "ALL", 1);
                }
                //����ȫ��ҽ��
                else if (e.ClickedItem == this.ResetAll)
                {
                    this.ReSet(EnumOrderType.All, -1, "ALL", 3);
                }
                //���õ�ǰҳ����
                else if (e.ClickedItem == this.ResetCurrentLong)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //���õ�ǰҳ����
                else if (e.ClickedItem == this.ResetCurrentShort)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //���õ�ǰҳ������ҽ��
                else if (e.ClickedItem == this.ResetCurrentUCUL)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //ˢ�����г�����ӡ״̬��ȫ�����ã������´�ӡ״̬
                else if (e.ClickedItem == this.RefreshLong)
                {
                }
                //ˢ������������ӡ״̬��ȫ�����ã������´�ӡ״̬
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
                //��ӡ
                if (e.ClickedItem == this.tbPrint)
                {
                    this.Print();
                }

                //��ӡȫ��
                else if (e.ClickedItem == this.tbAllPrint)
                {
                    this.AllPrint();// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
                }
                //����
                else if (e.ClickedItem == this.tbRePrint)
                {
                    this.PrintAgain();
                }
                //�رմ���
                else if (e.ClickedItem == this.tbExit)
                {
                    this.Close();
                }
                //���ó���
                else if (e.ClickedItem == this.ResetLong)
                {
                    this.ReSet(EnumOrderType.Long, -1, "ALL", 3);
                }
                //��������
                else if (e.ClickedItem == this.ResetShort)
                {
                    this.ReSet(EnumOrderType.Short, -1, "ALL", 3);
                }
                //����ȫ��ҽ��
                else if (e.ClickedItem == this.ResetAll)
                {
                    this.ReSet(EnumOrderType.All, -1, "ALL", 3);
                }
                //���õ�ǰҳ����
                else if (e.ClickedItem == this.ResetCurrentLong)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //���õ�ǰҳ����
                else if (e.ClickedItem == this.ResetCurrentShort)
                {
                    resetPartPage_Click(new object(), new EventArgs());
                }
                //ˢ�����г�����ӡ״̬��ȫ�����ã������´�ӡ״̬
                else if (e.ClickedItem == this.RefreshLong)
                {
                }
                //ˢ������������ӡ״̬��ȫ�����ã������´�ӡ״̬
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
                    this.ucOperateBillHeader.SetPatientInfo(temp);
                    this.myPatientInfo = temp;
                    orderQueryCount = 1;
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
            this.defaultRowNum = FS.FrameWork.Function.NConvert.ToInt32(this.nuRowNum.Value);
            this.SetPrintValue("����", this.defaultRowNum.ToString());
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
            if (tbPrinter.SelectedItem == null)
            {
                this.SetPrintValue("ҽ����", "");
                print.PrintDocument.PrinterSettings.PrinterName = "";
            }
            else
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

                this.ucLongOrderBillHeader.SetChangedInfo(GetFirstOrder());
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

                this.ucShortOrderBillHeader.SetChangedInfo(GetFirstOrder());
            }
        }

        /// <summary>
        /// �����л���ʾҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpOperateOrder_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpOperateOrder.Sheets.Count > 0 &&
                    this.fpOperateOrder.ActiveSheet.Tag != null)
            {
                this.lblPageOperatets.Text = "��" + (FS.FrameWork.Function.NConvert.ToInt32(this.fpOperateOrder.ActiveSheet.Tag) + 1).ToString() + "ҳ";


                this.ucOperateBillHeader.SetChangedInfo(GetFirstOrder());

            }

        }

        /// <summary>
        /// �����������л���ʾ�¼�
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
                    //������ʾ������ҽ������ʾ
                    this.tpUCUL.Controls.Clear();
                    this.tpUCUL.Controls.Add(pnShortOrder);

                    this.AddOrderToFP(EnumOrderType.Short, alShort_UCUL, -1, 0);
                    this.ucShortOrderBillHeader.Header = "������ҽ����";
                }
                else
                {
                    if (isOperate)
                    {

                        //tpOperate.Controls.Clear();
                        //tpOperate.Controls.Add(pnOperateOrder);
                        this.AddOrderToFP(EnumOrderType.Operate, alOperate, -1, 0);
                        this.ucShortOrderBillHeader.Header = "��  ��  ҽ  ��  ��";
                    }
                    else
                    {
                        tpShort.Controls.Clear();
                        tpShort.Controls.Add(pnShortOrder);

                        this.AddOrderToFP(EnumOrderType.Short, alShort_Normal, -1, 0);
                        this.ucShortOrderBillHeader.Header = "��  ʱ  ҽ  ��  ��";
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
                    this.ucShortOrderBillHeader.Header = "��  ��  ҽ  ��  ��";
                }
                else
                {

                    tpShort.Controls.Clear();
                    tpShort.Controls.Add(pnShortOrder);

                    this.AddOrderToFP(EnumOrderType.Short, alShort_ALL, -1, 0);
                    this.ucShortOrderBillHeader.Header = "��  ʱ  ҽ  ��  ��";
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

                System.Windows.Forms.MenuItem printAfterItem = new MenuItem();
                printAfterItem.Text = "�������ҽ���Ժ��ҽ��";
                printAfterItem.Click += new EventHandler(printAfterItem_Click);
                this.popMenu.MenuItems.Add(printAfterItem);

                System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                splitMenuItem.Text = "�Ӹ���ҽ����������һҳ";
                splitMenuItem.Click += new EventHandler(splitMenuItem_Click);
                this.popMenu.MenuItems.Add(splitMenuItem);

                System.Windows.Forms.MenuItem mnSetPrinted = new MenuItem();
                mnSetPrinted.Text = "���ø���ҽ���Ѵ�ӡ";
                mnSetPrinted.Click += new EventHandler(mnSetPrinted_Click);
                this.popMenu.MenuItems.Add(mnSetPrinted);

                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    //��û���Ժã������ΰ�
                    System.Windows.Forms.MenuItem mnAddBlankLine = new MenuItem();
                    mnAddBlankLine.Text = "���ӿհ���";
                    mnAddBlankLine.Click += new EventHandler(mnAddBlankLine_Click);
                    this.popMenu.MenuItems.Add(mnAddBlankLine);
                }

                System.Windows.Forms.MenuItem mnPrintStart = new MenuItem();
                mnPrintStart.Text = "�´δӴ���ҽ����ʼ��ӡ";
                mnPrintStart.Click += new EventHandler(mnPrintStart_Click);
                this.popMenu.MenuItems.Add(mnPrintStart);

                System.Windows.Forms.MenuItem resetPartPage = new MenuItem();
                resetPartPage.Text = "����ҽ������ǰҳ";
                resetPartPage.Click += new EventHandler(resetPartPage_Click);
                this.popMenu.MenuItems.Add(resetPartPage);

                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    System.Windows.Forms.MenuItem mnDeleteProfile = new MenuItem();
                    mnDeleteProfile.Text = "ɾ����ǰ����ҽ���Ľ�����ʾ�����ļ�";
                    mnDeleteProfile.Click += new EventHandler(mnDeleteProfile_Click);
                    this.popMenu.MenuItems.Add(mnDeleteProfile);
                }

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

                System.Windows.Forms.MenuItem printAfterItem = new MenuItem();
                printAfterItem.Text = "�������ҽ���Ժ��ҽ��";
                printAfterItem.Click += new EventHandler(printAfterItem_Click);
                this.popMenu.MenuItems.Add(printAfterItem);

                System.Windows.Forms.MenuItem mnSetPrinted = new MenuItem();
                mnSetPrinted.Text = "���ø���ҽ���Ѵ�ӡ";
                mnSetPrinted.Click += new EventHandler(mnSetPrinted_Click);
                this.popMenu.MenuItems.Add(mnSetPrinted);


                System.Windows.Forms.MenuItem mnAddBlankLine = new MenuItem();
                mnAddBlankLine.Text = "���ӿհ���";
                mnAddBlankLine.Click += new EventHandler(mnAddBlankLine_Click);
                this.popMenu.MenuItems.Add(mnAddBlankLine);


                System.Windows.Forms.MenuItem mnPrintStart = new MenuItem();
                mnPrintStart.Text = "�´δӴ���ҽ����ʼ��ӡ";
                mnPrintStart.Click += new EventHandler(mnPrintStart_Click);
                this.popMenu.MenuItems.Add(mnPrintStart);

                System.Windows.Forms.MenuItem resetPartPage = new MenuItem();
                resetPartPage.Text = "����ҽ������ǰҳ";
                resetPartPage.Click += new EventHandler(resetPartPage_Click);
                this.popMenu.MenuItems.Add(resetPartPage);

                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    System.Windows.Forms.MenuItem mnDeleteProfile = new MenuItem();
                    mnDeleteProfile.Text = "ɾ����ǰ����ҽ���Ľ�����ʾ�����ļ�";
                    mnDeleteProfile.Click += new EventHandler(mnDeleteProfile_Click);
                    this.popMenu.MenuItems.Add(mnDeleteProfile);
                }

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
            FS.HISFC.Models.Order.Inpatient.Order inOrder = null;
            FS.HISFC.Models.Order.Inpatient.Order preOrder = null;

            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).ActiveSheet;


            //���ҳ��
            int maxPageNo = 0;
            //��ǰҳ��
            int pageNo = 0;
            //�к�
            int rowNum = -1;

            inOrder = sheet.Rows[sheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            #region ��ʿδ��˲��ܴ�ӡ

            if (inOrder != null
                && !FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                //ҽ��״̬,0������1��ˣ�2ִ�У�3���ϣ�4������5��Ҫ�ϼ�ҽ����ˣ�6�ݴ棬7Ԥֹͣ

                if (inOrder.Status == 5)
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]��Ҫ�ϼ�ҽ����˺󣬲��ܴ�ӡ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (inOrder.Status == 6)
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]Ϊ�ݴ�״̬��Ŀǰ���ܴ�ӡҽ������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ("0,1,2,5,6".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.Nurse.ID))
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]��ʿ��δ�˶ԣ����ܴ�ӡҽ������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if ("3,4,7".Contains(inOrder.Status.ToString())
                    && string.IsNullOrEmpty(inOrder.DCNurse.ID))
                {
                    MessageBox.Show("ҽ��[" + inOrder.Item.Name + "]��ʿ��δ�˶ԣ����ܴ�ӡҽ������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion

            if (MessageBox.Show("�Ƿ�����ҽ����" + inOrder.Item.Name + "��Ϊ�Ѵ�ӡ״̬��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            maxPageNo = this.orderManager.GetMaxPageNo(this.myPatientInfo.ID, "1");
            pageNo = FS.FrameWork.Function.NConvert.ToInt32(sheet.Tag.ToString());

            if (pageNo > maxPageNo + 1)
            {
                MessageBox.Show("ǰһҳҽ��δ��ӡ��\r\n\r\n���ܼ������ã�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sheet.ActiveRowIndex > 0)
            {
                preOrder = sheet.Rows[sheet.ActiveRowIndex - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                rowNum = preOrder.RowNo;
            }
            //��һҳ
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

            if (orderManager.ExecNoQuery(updateSQL, inOrder.Patient.ID, inOrder.ID, pageNo.ToString(), (rowNum + 1).ToString()) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���´�ӡ��ǳ���\r\n\r\n" + orderManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("���³ɹ���\r\n\r\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);


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
        ///  ����ҽ����ѡ��ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetPartPage_Click(object sender, EventArgs e)
        {
            //��ȡҳ��
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
        /// �´δӴ���ҽ����ʼ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnPrintStart_Click(object sender, EventArgs e)
        {
            FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).ActiveSheet;

            if (sheet.ActiveRowIndex == -1)
            {
                MessageBox.Show("��ѡ�д��´ο�ʼ��ӡ��ҽ����");
                return;
            }

            int rowIndex = sheet.ActiveRowIndex;
            FS.HISFC.Models.Order.Inpatient.Order orderTemp = sheet.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            if (MessageBox.Show("�Ƿ�����ҽ����" + orderTemp.Item.Name + "���Լ���ҽ���Ժ��ҽ��Ϊδ��ӡ״̬��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int pageNO = orderTemp.PageNo;
            int rowNO = orderTemp.RowNo;

            string inpatientNO = orderTemp.Patient.ID;

            int flag = (OrderType == EnumOrderType.Long ? 1 : 0);//����ҽ�� 1����ʱҽ�� 0


            if (pageNO == -1 || rowNO == -1)
            {
                MessageBox.Show("����ҽ���Ѿ���δ��ӡ״̬", "��ʾ", MessageBoxButtons.OK);

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
                MessageBox.Show("���´�ӡ��ǳ���\r\n\r\n" + orderManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("���³ɹ���\r\n\r\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.QueryPatientOrder();
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
            ShiftPage(EnumOrderType.Short);
            this.SetFPStyle(EnumOrderType.Short);
        }

        /// <summary>
        /// ����ҽ����ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitMenuItem_Click(object sender, EventArgs e)
        {
            ShiftPage(EnumOrderType.Long);
            this.SetFPStyle(EnumOrderType.Long);
        }

        /// <summary>
        /// �Ҽ���ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printMenuItem_Click(object sender, EventArgs e)
        {
            this.PrintSingleItem(false);
        }


        /// <summary>
        /// ֻ�����������ҽ��ֹͣʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDateItem_Click(object sender, EventArgs e)
        {
            this.PrintSingleItem(true);
        }
        /// <summary>
        /// �������ҽ���Ժ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printAfterItem_Click(object sender, EventArgs e)
        {
            int rowIndex = GetFpOrder(OrderType).ActiveSheet.ActiveRowIndex;

            #region �ж�֮ǰ���Ƿ��Ѿ���ӡ���

            if (rowIndex != 0)
            {
                for (int row = rowIndex - 1; row > 0; row--)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ordTemp = GetFpOrder(OrderType).ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (ordTemp != null
                        && ordTemp.GetFlag == "0")
                    {
                        MessageBox.Show("ǰ�滹��δ��ӡ��ҽ������������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            //�ж�ǰһҳ�Ƿ��ӡ���
            if (GetFpOrder(OrderType).ActiveSheetIndex > 0)
            {
                FarPoint.Win.Spread.SheetView sheet = GetFpOrder(OrderType).Sheets[GetFpOrder(OrderType).ActiveSheetIndex - 1];
                for (int row = sheet.RowCount; row > 0; row--)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ordTemp = sheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (ordTemp != null
                        && ordTemp.GetFlag == "0")
                    {
                        MessageBox.Show("ǰ�滹��δ��ӡ��ҽ������������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            #endregion

            //����Ӧ�������⣺���ѡ��Ĳ�������δ��ӡҽ����Ҳ���ܴ�ӡ��



            FS.HISFC.Models.Order.Inpatient.Order order = this.GetFpOrder(OrderType).ActiveSheet.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;
            if (order == null)
            {
                return;
            }
            if (order.GetFlag != "0")
            {
                MessageBox.Show("��Ŀ[" + order.Item.Name + "]�Ѿ���ӡ,�޷�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //��ӡ��ǰҳ
            string pageNo = (FS.FrameWork.Function.NConvert.ToInt32(GetFpOrder(OrderType).ActiveSheet.Tag) + 1).ToString();
            MessageBox.Show("��ע������" + pageNo + "ҳҽ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (PrintPage(false, rowIndex, -1, false) == -1)
            {
                return;
            }


            //��ӡ���漸ҳ
            int sheetIndex = GetFpOrder(OrderType).ActiveSheetIndex;
            int sheetCount = GetFpOrder(OrderType).Sheets.Count;
            for (int index = sheetIndex + 1; index < sheetCount; index++)
            {
                this.GetFpOrder(OrderType).ActiveSheetIndex = index;

                pageNo = (FS.FrameWork.Function.NConvert.ToInt32(GetFpOrder(OrderType).ActiveSheet.Tag) + 1).ToString();
                MessageBox.Show("��ע������" + pageNo + "ҳҽ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (PrintPage(false, -1, -1, false) == -1)
                {
                    return;
                }
            }
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
        /// ����
        /// </summary>
        Operate,

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
    /// ҽ�������ж���
    /// </summary>
    public enum EnumCol
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
        /// ����ҽ��
        /// </summary>
        [FS.FrameWork.Public.Description("ҽʦǩ��")]
        RecipeDoct,

        /// <summary>
        /// ��˻�ʿ
        /// </summary>
        [FS.FrameWork.Public.Description("��ʿǩ��")]
        ConfirmNurse,

        /// <summary>
        /// ����
        /// </summary>
        [FS.FrameWork.Public.Description("��")]
        CombFlag,

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        [FS.FrameWork.Public.Description("ҽ������")]
        ItemName,

        /// <summary>
        /// ֹͣ���
        /// </summary>
        [FS.FrameWork.Public.Description("ֹͣ���")]
        DCFlag,

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
        /// �������
        /// </summary>
        [FS.FrameWork.Public.Description("�������")]
        ConfirmDate,

        /// <summary>
        /// ���ʱ��
        /// </summary>
        [FS.FrameWork.Public.Description("���ʱ��")]
        ConfirmTime,

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
        [FS.FrameWork.Public.Description("ҽʦǩ��")]
        DCDoct,

        /// <summary>
        /// ֹͣ��˻�ʿ
        /// </summary>
        [FS.FrameWork.Public.Description("��ʿǩ��")]
        DCConfirmNurse,

        /// <summary>
        /// ִ�л�ʿ
        /// </summary>
        [FS.FrameWork.Public.Description("ִ�л�ʿ")]
        ExecNurse,

        /// <summary>
        /// ����
        /// </summary>
        [FS.FrameWork.Public.Description("����")]
        Qty,

        /// <summary>
        /// ���
        /// </summary>
        [FS.FrameWork.Public.Description("���")]
        Cost,

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
}