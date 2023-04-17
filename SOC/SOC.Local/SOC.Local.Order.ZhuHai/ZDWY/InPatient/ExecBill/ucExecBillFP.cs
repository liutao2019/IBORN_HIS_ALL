using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.ExecBill
{
    /// <summary>
    /// 执行单打印
    /// </summary>
    public partial class ucExecBillFP : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        public ucExecBillFP()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
        }

        #region IPrintTransFusion 成员

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.Manager.Bed manager = new FS.HISFC.BizLogic.Manager.Bed();

        FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
        string speOrderType = "";

        ArrayList curValues = null; //当前显示的数据

        static Dictionary<string, string> lisPrint = new Dictionary<string, string>();
        /// <summary>
        /// 是否补打
        /// </summary>
        bool isRePrint = false;

        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usage = "";

        #endregion

        #region 打印处理 by cube 2011-06-18

        ~ucExecBillFP()
        {
            this.PrintDocument.Dispose();
        }

        /// <summary>
        /// 项目列表配置文件
        /// </summary>
        protected string sheetXMLFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "ucExecBillFPItem.xml";

        /// <summary>
        /// 当前打印页的页码
        /// 程序自动计算的
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// 本次打印最大页码
        /// 程序自动计算的
        /// </summary>
        private int maxPageNO = 1;

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;

            //数据FarPoint
            this.DrawControl(graphics, this.panel1, this.panel1.Location.X, this.panel2.Height);

            //标题部分
            this.DrawControl(graphics, this.panel2, this.panel2.Location.X, this.panel2.Location.Y);

            #region 分页
            if (this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        private int DrawControl(Graphics graphics, Control control, int locationX, int locationY)
        {
            //控件层次越深消耗越大
            foreach (Control c in control.Controls)
            {
                if (c.Visible && c.Height > 0)
                {
                    if (c is FarPoint.Win.Spread.FpSpread && ((FarPoint.Win.Spread.FpSpread)c).Sheets.Count > 0)
                    {
                        FarPoint.Win.Spread.FpSpread spread = ((FarPoint.Win.Spread.FpSpread)c);
                        int drawingWidth = c.Size.Width;
                        int drawingHeight = c.Size.Height;
                        if (this.curPageNO == 1)
                        {
                            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
                            printInfo.ShowRowHeaders = spread.Sheets[0].RowHeader.Visible;
                            printInfo.ShowBorder = false;
                            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
                            spread.ActiveSheet.PrintInfo = printInfo;
                            int count = spread.GetOwnerPrintPageCount(graphics, new Rectangle(
                                locationX + c.Location.X,
                                locationY + c.Location.Y,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY - 20 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                                ), spread.ActiveSheetIndex);
                            maxPageNO = count;
                        }

                        int[] pageRowQty = spread.GetOwnerPrintRowPageBreaks(graphics, new Rectangle(
                               locationX + c.Location.X,
                               locationY + c.Location.Y,
                               this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX,
                               this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY - 20 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                               ), spread.ActiveSheetIndex, true);

                        spread.OwnerPrintDraw(graphics, new Rectangle(
                           locationX + c.Location.X,
                                locationY + c.Location.Y,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY - 20 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                            ), spread.ActiveSheetIndex, this.curPageNO);

                        if (this.curPageNO > 1)
                        {
                            try
                            {
                                int prePageRowQty = pageRowQty[this.curPageNO - 2];

                                int curPageFirstRowIndex = prePageRowQty;

                                //床号
                                string printText = spread.ActiveSheet.Cells[curPageFirstRowIndex, (int)EnumCol.BedID].Tag.ToString();
                                string printText2 = spread.ActiveSheet.Cells[curPageFirstRowIndex - 1, (int)EnumCol.BedID].Tag.ToString();

                                if (printText == printText2)
                                {
                                    graphics.DrawString(
                                       printText,
                                       spread.Font,
                                       new SolidBrush(Color.Black),
                                     locationX + c.Location.X /*+ spread.ActiveSheet.Columns[0].Width*/,
                                        locationY + c.Location.Y + spread.ActiveSheet.RowHeader.Rows[0].Height /*+ spread.ActiveSheet.Rows[curPageFirstRowIndex].Height*/);
                                    //姓名
                                    printText = spread.ActiveSheet.Cells[curPageFirstRowIndex, (int)EnumCol.PatientName].Tag.ToString();

                                    graphics.DrawString(
                                       printText,
                                       spread.Font,
                                       new SolidBrush(Color.Black),
                                     locationX + c.Location.X + spread.ActiveSheet.Columns[0].Width /*+ spread.ActiveSheet.Columns[(int)EnumCol.BedID].Width*/
                                                                                                                                             ,
                                        locationY + c.Location.Y + spread.ActiveSheet.RowHeader.Rows[0].Height /*+ spread.ActiveSheet.Rows[curPageFirstRowIndex].Height*/);
                                }
                            }
                            catch { }
                        }

                    }
                    else if (c is Panel)
                    {
                        graphics.FillRectangle(new SolidBrush(c.BackColor), locationX + c.Location.X, locationY + c.Location.Y, c.Width, c.Height);
                    }
                    else
                    {
                        graphics.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), locationX + c.Location.X, locationY + c.Location.Y);
                        if (c.Name == neuLblExecTime.Name)
                        {
                            graphics.DrawString(
                                "第 " + this.curPageNO.ToString() + " 页，共 " + this.maxPageNO.ToString() + " 页",
                                neuLblExecTime.Font,
                                new SolidBrush(neuLblExecTime.ForeColor),
                                this.PrintDocument.DefaultPageSettings.PaperSize.Width / 2 - 100,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - this.PrintDocument.DefaultPageSettings.Margins.Bottom - 20);
                        }
                    }
                    if (c.Controls.Count > 0)
                    {
                        this.DrawControl(graphics, c, locationX + c.Location.X, locationY + c.Location.Y);
                    }
                }
            }

            return control.Height;
        }

        protected void PrintView(System.Drawing.Printing.PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.myPrintView();
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("Letter", 800, 1100);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        private void myPrintView()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印机报错！" + ex.Message);
            }
        }

        protected void PrintPage(System.Drawing.Printing.PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.PrintDocument.Print();
        }

        protected void PrintPage()
        {
            this.SetPaperSize(null);
            this.PrintDocument.Print();
        }

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        #endregion

        #region 方法

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            try
            {

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("execBillPaper");
                System.Drawing.Printing.PaperSize curPaperSize = new System.Drawing.Printing.PaperSize();
                if (pSize == null)
                {

                    pSize = new FS.HISFC.Models.Base.PageSize("Letter", 800, 1100);
                }

                curPaperSize.PaperName = pSize.Name;
                curPaperSize.Height = pSize.Height;
                curPaperSize.Width = pSize.Width;
                this.PrintDocument.DefaultPageSettings.Margins.Bottom = pSize.Top;
                if (!string.IsNullOrEmpty(pSize.Printer))
                {
                    this.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
                }

                //设置不打印的行不显示
                int index = 0;
                ArrayList al = new ArrayList();
                FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();

                ArrayList alListItem = new ArrayList();

                for (index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
                {
                    if (neuSpread1_Sheet1.Rows[index].Tag == null)
                    {
                        continue;
                    }

                    if (this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Text == "False")
                    {
                        this.neuSpread1_Sheet1.Rows[index].Visible = false;
                    }
                    else
                    {
                        execOrder = this.neuSpread1_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.ExecOrder;

                        //用User03存储组合标记信息
                        execOrder.User03 = neuSpread1_Sheet1.Cells[index, (int)EnumCol.CombFlag].Text;

                        al.Add(execOrder);

                        string str = "";

                        for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
                        {
                            str += this.neuSpread1_Sheet1.Cells[index, j].Text + "|";
                        }

                        str = str.Substring(0, str.Length - 1);

                        alListItem.Add(str);
                    }
                }

                //只更新打印的执行单状态
                this.curValues = al;
                //设置“是否打印”列不显示
                this.neuSpread1_Sheet1.Columns[(int)EnumCol.IsPrint].Visible = false;

                print.SetPageSize(pSize);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("没有可打印的数据，请重新选择！");
                }
                else
                {
                    #region 测试完整打印

                    //this.AddToFP(al);
                    //this.AddLine();

                    //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    //{
                    //    print.PrintPreview(pnPrint);
                    //    //this.PrintView(curPaperSize);
                    //}
                    //else
                    //{
                    //    print.PrintPage(0, 0, pnPrint);
                    //    //this.PrintPage(curPaperSize);
                    //}
                    #endregion

                    int pageCount = (Int32)Math.Ceiling(al.Count / 23M);

                    #region 分页打印

                    ArrayList alPrint = new ArrayList();
                    int page = 1;
                    for (int i = 0; i < al.Count; i++)
                    {
                        alPrint.Add(al[i]);

                        if (i == al.Count - 1
                            || alPrint.Count == 23)
                        {
                            AddToFP(alPrint);
                            AddLine();

                            lblFoot.Text = "第" + page.ToString() + "页  共" + pageCount.ToString() + "页";

                            pnFoot.Dock = DockStyle.None;
                            //pnFoot.BringToFront();
                            pnFoot.Location = new Point(pnPrint.Location.X, (Int32)Math.Ceiling(pnPrint.Location.Y + neuSpread1_Sheet1.RowHeader.Rows[0].Height + neuSpread1_Sheet1.Rows[0].Height * 22 + panel2.Height));

                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(pnPrint);
                            }
                            else
                            {
                                print.PrintPage(0, 0, pnPrint);
                            }
                            alPrint.Clear();
                            page += 1;
                        }
                    }
                    #endregion

                    //pnFoot.Dock = DockStyle.None;
                    ////pnFoot.BringToFront();
                    //pnFoot.Location = new Point(pnPrint.Location.X, (Int32)Math.Ceiling(pnPrint.Location.Y + neuSpread1_Sheet1.RowHeader.Rows[0].Height + neuSpread1_Sheet1.Rows[0].Height * 22 + panel2.Height));

                    //for (int page = 0; page < pageCount; page++)
                    //{
                    //    for (index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
                    //    {
                    //        if (index >= page * 23 && index < (page + 1) * 23)
                    //        {
                    //            neuSpread1_Sheet1.Rows[index].Visible = true;
                    //        }
                    //        else
                    //        {
                    //            neuSpread1_Sheet1.Rows[index].Visible = false;
                    //        }
                    //    }

                    //    lblFoot.Text = "第" + (page + 1).ToString() + "页  共" + pageCount.ToString() + "页";

                    //    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    //    {
                    //        print.PrintPreview(pnPrint);
                    //    }
                    //    else
                    //    {
                    //        print.PrintPage(0, 0, pnPrint);
                    //    }

                    //}

                    pnFoot.Dock = DockStyle.Bottom;
                }
                //打印后设置“是否打印”列显示
                this.neuSpread1_Sheet1.Columns[(int)EnumCol.IsPrint].Visible = true;

                #region 更新已经打印标记
                if (!this.isRePrint)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(orderManager.Connection);
                    //t.BeginTransaction();

                    this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    string itemType = null;
                    for (int i = 0; i < this.curValues.Count; i++)
                    {
                        FS.HISFC.Models.Order.ExecOrder exeord = curValues[i] as FS.HISFC.Models.Order.ExecOrder;
                        //if (exeord.Order.Item.IsPharmacy)
                        if (exeord.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            itemType = "1";
                        }
                        else
                        {
                            itemType = "2";
                        }
                        //通过医嘱流水号更新执行档
                        if (this.orderManager.UpdateExecOrderPrintedByMoOrder(exeord.Order.ID, dt1.ToString(), dt2.ToString(), itemType) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新打印标记失败!" + orderManager.Err);
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                #endregion

                this.neuSpread1_Sheet1.RowCount = 0;
                if (!((FS.HISFC.Models.Base.Employee)orderManager.Operator).IsManager)
                {
                    this.Query(myPatients, usage, dt1, dt2, isRePrint, this.orderType, this.isFirst);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintSet()
        {
            print.ShowPrintPageDialog();
            this.Print();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alValues"></param>
        protected void SetValues(ArrayList alValues)
        {
            // curValues = alValues;
            if (alValues != null)
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RowCount = 0;
                }

                alValues.Sort(new ComparerExecOrder());
                //this.AddConstsToTable(alValues, ref newList);
                this.AddToFP(alValues);
                this.AddLine();

                this.neuSpread1_Sheet1.Columns[(int)EnumCol.IsPrint].Locked = false;
            }
        }

        /// <summary>
        /// 画边框
        /// </summary>
        /// <param name="isDraw">是否画 边框</param>
        private void AddLine()
        {
            #region 内容

            //只显示下面的边框
            FarPoint.Win.BevelBorder buttonBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            //无边框
            FarPoint.Win.BevelBorder noBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            FS.HISFC.Components.Common.Classes.Function.DrawComboLeft(neuSpread1_Sheet1, (int)EnumCol.CombNo, (int)EnumCol.CombFlag);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
                {
                    neuSpread1_Sheet1.Cells[index, (int)EnumCol.CombFlag].Text = neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Tag.ToString() + neuSpread1_Sheet1.Cells[index, (int)EnumCol.CombFlag].Text;

                    //对于打印时，重新赋值组合标记信息
                    try
                    {
                        FS.HISFC.Models.Order.ExecOrder execOrder = this.neuSpread1_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.ExecOrder;
                        if (execOrder != null
                            && !string.IsNullOrEmpty(execOrder.User03))
                        {
                            neuSpread1_Sheet1.Cells[index, (int)EnumCol.CombFlag].Text = execOrder.User03;
                        }
                    }
                    catch { }

                    this.neuSpread1_Sheet1.Rows[index].Border = noBorder;

                    if (index != this.neuSpread1_Sheet1.RowCount - 1)
                    {
                        if (neuSpread1_Sheet1.Cells[index, (int)EnumCol.BedID].Tag != null
                            && neuSpread1_Sheet1.Cells[index + 1, (int)EnumCol.BedID].Tag != null
                            && neuSpread1_Sheet1.Cells[index, (int)EnumCol.BedID].Tag.ToString() != neuSpread1_Sheet1.Cells[index + 1, (int)EnumCol.BedID].Tag.ToString())
                        {
                            this.neuSpread1_Sheet1.Rows[index].Border = buttonBorder;
                        }
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[index].Border = buttonBorder;
                    }
                }
            }

            #endregion

            #region 标题

            FarPoint.Win.BevelBorder topBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, true, false, true);

            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnHeader.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.ColumnHeader.Rows[i].Border = topBorder;
            }

            #endregion
        }

        Hashtable hsPatientInfo = new Hashtable();

        /// <summary>
        /// 添加到显示
        /// </summary>
        /// <param name="list"></param>
        private void AddToFP(ArrayList list)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            if (list == null || list.Count == 0)
            {
                return;
            }
            int index = 0;
            string itemName = "";
            int length = System.Text.Encoding.GetEncoding("gb2312").GetBytes("呵呵哈哈哈哈哈哈哈哈哈呵呵呵哈哈哈").Length;
            FarPoint.Win.Spread.CellType.CheckBoxCellType chb = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

            //勾选打印
            this.neuSpread1_Sheet1.Columns[(int)EnumCol.IsPrint].CellType = chb;

            //存储住院流水号
            string inpatientNo = "";

            //组合号
            string combNo = "";

            hsPatientInfo.Clear();

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in list)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);

                //同一个患者的不同组项目，用空行间隔
                //if (inpatientNo == execOrder.Order.Patient.ID
                //    && combNo != execOrder.Order.Combo.ID)
                //{
                //    index++;
                //    this.neuSpread1_Sheet1.Rows.Add(index, 1);
                //}

                //开始时间
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.MOTime].Text = execOrder.Order.MOTime.ToString("dd") + "/" + execOrder.Order.MOTime.ToString("MM");
                #region 床号、姓名 每个患者只显示一个

                if (!hsPatientInfo.Contains(execOrder.Order.Patient.ID))
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    FS.HISFC.BizLogic.RADT.InPatient inMgr = new FS.HISFC.BizLogic.RADT.InPatient();
                    patientInfo = inMgr.QueryPatientInfoByInpatientNO(execOrder.Order.Patient.ID);
                    if (patientInfo != null)
                    {
                        execOrder.Order.Patient = patientInfo;
                        hsPatientInfo.Add(patientInfo.ID, patientInfo);
                    }
                }
                else
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = hsPatientInfo[execOrder.Order.Patient.ID] as FS.HISFC.Models.RADT.PatientInfo;
                    execOrder.Order.Patient = patientInfo;
                }


                //床号
                neuSpread1_Sheet1.Cells[index, (int)EnumCol.BedID].Tag = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);

                //姓名
                neuSpread1_Sheet1.Cells[index, (int)EnumCol.PatientName].Tag = execOrder.Order.Patient.Name;
                //住院流水号
                neuSpread1_Sheet1.Cells[index, (int)EnumCol.CombFlag].Tag = execOrder.Order.Patient.ID;

                //床号、姓名 每个患者只显示一个
                if (inpatientNo != execOrder.Order.Patient.ID)
                {
                    //床号
                    neuSpread1_Sheet1.Cells[index, (int)EnumCol.BedID].Text = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);

                    //姓名
                    neuSpread1_Sheet1.Cells[index, (int)EnumCol.PatientName].Text = execOrder.Order.Patient.Name;

                    inpatientNo=execOrder.Order.Patient.ID;
                }

                #endregion

                //组号
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.SubCombNo].Text = (execOrder.Order.OrderType.IsDecompose ? "长" : "临") + execOrder.Order.SubCombNO.ToString();

                //组标记
                //this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.CombFlag].Text = execOrder.Order.Combo.Memo;

                //名称
                if (execOrder.Order.IsEmergency)
                {
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.ItemName].Text = "[急]" + execOrder.Order.Item.Name;//添加加急标志
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.ItemName].Text = execOrder.Order.Item.Name;
                }

                //数量
                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.DoseOnce].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.DoseUnit + "/" + execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit.TrimEnd(' ');
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.DoseOnce].Text = execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit + "/" + execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit.TrimEnd(' ');
                }
                //每次量
                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.Qty].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.DoseUnit;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.Qty].Text = execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit;
                }

                #region 同组的用法、频次等只打印一个

                if (combNo != execOrder.Order.Combo.ID)
                {
                    //用法
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.Usage].Text = execOrder.Order.Usage.Name;
                    //频次
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.Freq].Text = execOrder.Order.Frequency.ID;
                    //执行时间点
                    //this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.UseTime].Text = execOrder.Memo;
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.UseTime].Text = hsMorder_useTime[execOrder.Order.ID];

                    combNo = execOrder.Order.Combo.ID;
                }
                #endregion

                //备注
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.Memo].Text = execOrder.Order.Memo;

                //签名
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.Sign].Text = "";
                //签名的执行时间
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.SignExecTime].Text = " ";
                //默认全部打印
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Text = "True";

                //组合号
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.CombNo].Text = execOrder.Order.Combo.ID;

                //用来显示 新开立和临嘱
                neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Tag = "";
                if (execOrder.Order.OrderType.IsDecompose)
                {
                    if (execOrder.Order.MOTime.Date == this.dt1.Date)
                    {
                        neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Tag = "新";
                    }
                }
                else
                {
                    neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Tag = "临";
                }

                #region 停用信息

                if (execOrder.Order.Status == 3 || execOrder.Order.Status == 4)
                {
                    if (execOrder.DateUse >= execOrder.Order.EndTime)
                    {
                        neuSpread1_Sheet1.Rows[index].ForeColor = Color.Red;
                        this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.Memo].Text = neuSpread1_Sheet1.Cells[index, (int)EnumCol.Memo].Text;
                    }
                }

                #endregion

                this.neuSpread1_Sheet1.Rows[index].Tag = execOrder;

                index++;
            }

            for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
            {
                if (j != this.neuSpread1_Sheet1.Columns.Count - 1)
                {
                    this.neuSpread1_Sheet1.Columns[j].Locked = true;
                }
            }

            this.lblPrintTime.Text = this.orderManager.GetDateTimeFromSysDateTime().ToString();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 同组一起勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)EnumCol.IsPrint)
            {
                string combNo = neuSpread1_Sheet1.Cells[e.Row, (int)EnumCol.CombNo].Text == null ? "" : neuSpread1_Sheet1.Cells[e.Row, (int)EnumCol.CombNo].Text.ToString();
                if (string.IsNullOrEmpty(combNo))
                {
                    return;
                }
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (neuSpread1_Sheet1.Cells[i, (int)EnumCol.CombNo].Text != null
                        && neuSpread1_Sheet1.Cells[i, (int)EnumCol.CombNo].Text.ToString() == combNo)
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.IsPrint].Text = neuSpread1_Sheet1.Cells[e.Row, e.Column].Text;
                    }
                }
            }
        }

        #endregion

        #region IPrintTransFusion 成员

        /// <summary>
        /// 作废的执行档是否打印
        /// </summary>
        private bool dcIsPrint = false;

        /// <summary>
        /// 作废的执行档是否打印
        /// </summary>
        public bool DCIsPrint
        {
            get
            {
                return dcIsPrint;
            }
            set
            {
                dcIsPrint = value;
            }
        }

        /// <summary>
        /// 未收费是否允许打印
        /// </summary>
        private bool noFeeIsPrint = false;

        /// <summary>
        /// 未收费是否允许打印
        /// </summary>
        public bool NoFeeIsPrint
        {
            get
            {
                return noFeeIsPrint;
            }
            set
            {
                noFeeIsPrint = value;
            }
        }

        /// <summary>
        /// 退费后的是否打印
        /// </summary>
        private bool quitFeeIsPrint = true;

        /// <summary>
        /// 退费后的是否打印
        /// </summary>
        public bool QuitFeeIsPrint
        {
            get
            {
                return quitFeeIsPrint;
            }
            set
            {
                quitFeeIsPrint = value;
            }
        }

        /// <summary>
        /// 获取日期定位显示：明、昨、..日
        /// </summary>
        /// <param name="dateUse"></param>
        /// <param name="printDate"></param>
        /// <returns></returns>
        private string GetDateSpan(DateTime dateUse, DateTime printDate)
        {
            int hour = dateUse.Hour;
            if (hour == 0)
            {
                hour = 24;
                dateUse = dateUse.AddDays(-1);
            }

            TimeSpan span = new TimeSpan(dateUse.Date.Ticks - printDate.Date.Ticks);

            if (span.Days == -1)
            {
                return "昨" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }
            else if (span.Days == 1)
            {
                return "明" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }
            if (span.Days == 2)
            {
                return "后" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }
            if (span.Days ==0)
            {
                return "" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }

            return "[" + dateUse.Day.ToString() + "日]" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
        }

        /// <summary>
        /// 医嘱类型 all全部；1 长嘱；0 临嘱
        /// </summary>
        private string orderType;

        /// <summary>
        /// 存储《moder,useTime》
        /// </summary>
        Dictionary<string, string> hsMorder_useTime = new Dictionary<string, string>();

        /// <summary>
        /// 是否首日
        /// </summary>
        private bool isFirst;

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usagecode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isRePrint"></param>
        /// <param name="orderType"></param>
        /// <param name="isFirst"></param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isRePrint, string orderType, bool isFirst)
        {
            this.neuLblExecTime.Text = "执行时间:" + dtBegin.ToString("MM月dd日") + "-" + dtEnd.ToString("MM月dd日");

            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.isRePrint = isRePrint;
            this.usage = usagecode;

            this.isFirst = isFirst;
            this.orderType = orderType;

            //给患者列表赋值
            this.myPatients = patients;

            this.lblTitle.Text = this.Tag.ToString();

            ArrayList alOrder = new ArrayList();

            string paramPatient = "";

            Dictionary<string, FS.HISFC.Models.RADT.PatientInfo> hsPatientInfo = new Dictionary<string, FS.HISFC.Models.RADT.PatientInfo>();

            //获得in的患者id参数
            for (int i = 0; i < patients.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo p = patients[i] as FS.HISFC.Models.RADT.PatientInfo;
                paramPatient = "'" + p.ID + "'," + paramPatient;
                //获得护理分组
                //p.PVisit.PatientLocation.Bed.Memo = manager.GetNurseTendGroupFromBed(p.PVisit.PatientLocation.Bed.ID);

                this.lblDept.Text = "病区：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(p.PVisit.PatientLocation.Dept.ID);

                if (p.PVisit.PatientLocation.Bed == null)
                {
                    MessageBox.Show(manager.Err);
                    return;
                }

                if (!hsPatientInfo.ContainsKey(p.ID))
                {
                    hsPatientInfo.Add(p.ID, p);
                }
            }

            if (paramPatient == "")
            {
                paramPatient = "''";
            }
            else
            {
                paramPatient = paramPatient.Substring(0, paramPatient.Length - 1);//去掉后面的逗号
            }

            alOrder = this.orderManager.QueryOrderExecBill(paramPatient, dtBegin, dtEnd, usagecode, isRePrint);//查询医嘱，传入参数

            ArrayList alOrderNew = new ArrayList();


            FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();
            ArrayList alOrderTemp = new ArrayList();

            hsMorder_useTime.Clear();

            foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in alOrder)
            {
                #region 处理停用、退费、首日量等特殊医嘱

                //医嘱停用后会自动作废停止时间后的执行档，所以只需要判断执行档是否有效即可
                if (!dcIsPrint && !exeOrder.IsValid)
                {
                    continue;
                }

                //长嘱
                if (orderType == "1")
                {
                    if (!exeOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }
                //临嘱
                else if (orderType == "0")
                {
                    if (exeOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }

                //只打印首日量
                if (isFirst)
                {
                    if (exeOrder.Order.MOTime.Date != exeOrder.DateUse.Date)
                    {
                        continue;
                    }
                }

                #endregion

                exeOrder.Order.Patient.Name = hsPatientInfo[exeOrder.Order.Patient.ID].Name;

                #region 停用信息
                string strValid_execOrder = "";
                if (exeOrder.Order.Status == 3)
                {
                    if (exeOrder.DateUse >= exeOrder.Order.EndTime)
                    {
                        strValid_execOrder = "[停]";
                    }
                }

                #endregion

                #region 记录医嘱使用时间

                //长嘱
                if (exeOrder.Order.OrderType.IsDecompose

                    //临嘱每天只有一个频次
                    || SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(exeOrder.Order.Frequency.ID) <= 1)
                {
                    if (!hsMorder_useTime.ContainsKey(exeOrder.Order.ID))
                    {
                        hsMorder_useTime.Add(exeOrder.Order.ID, GetDateSpan(exeOrder.DateUse, dtBegin) + strValid_execOrder);

                        alOrderTemp.Add(exeOrder);
                    }
                    else
                    {
                        hsMorder_useTime[exeOrder.Order.ID] += "、" + GetDateSpan(exeOrder.DateUse, dtBegin) + strValid_execOrder;
                    }
                }
                //临嘱BID等 显示两个时间点
                else
                {
                    FS.HISFC.Models.Order.Frequency freqObj = SOC.HISFC.BizProcess.Cache.Order.GetFrequency(exeOrder.Order.Frequency.ID);
                    for (int i = 0; i < freqObj.Times.Length; i++)
                    {
                        DateTime time = Convert.ToDateTime(freqObj.Times[i]);
                        if (!hsMorder_useTime.ContainsKey(exeOrder.Order.ID))
                        {
                            hsMorder_useTime.Add(exeOrder.Order.ID, GetDateSpan(time, dtBegin) + strValid_execOrder);

                            alOrderTemp.Add(exeOrder);
                        }
                        else
                        {
                            hsMorder_useTime[exeOrder.Order.ID] += "、" + GetDateSpan(time, dtBegin) + strValid_execOrder;
                        }
                    }
                    //string execTime = this.orderManager.ExecSqlReturnOne(string.Format("select exec_times from met_ipm_order where mo_order='{0}'", exeOrder.Order.ID), "");
                    //if (!string.IsNullOrEmpty(execTime))
                    //{
                        //string[] times = execTime.Split('-');
                        //for (int i = 0; i < times.Length; i++)
                        //{
                        //    DateTime dt = exeOrder.DateUse.Date.AddHours(FS.FrameWork.Function.NConvert.ToInt32(times[i].Substring(0, times[i].IndexOf(':'))));
                        //    if (!hsMorder_useTime.ContainsKey(exeOrder.Order.ID))
                        //    {
                        //        hsMorder_useTime.Add(exeOrder.Order.ID, GetDateSpan(dt, dtBegin) + strValid_execOrder);

                        //        alOrderTemp.Add(exeOrder);
                        //    }
                        //    else
                        //    {
                        //        hsMorder_useTime[exeOrder.Order.ID] += "、" + GetDateSpan(dt, dtBegin) + strValid_execOrder;
                        //    }
                    //    }
                    //}
                }

                #endregion
            }

            alOrder = alOrderTemp;

            this.curValues = alOrder;

            #region 旧的作废

            /*
            #region 医嘱过滤

            foreach (FS.HISFC.Models.Order.ExecOrder exe in alOrder)
            {
                #region 医嘱停用后的不显示了

                //医嘱停用后会自动作废停止时间后的执行档，所以只需要判断执行档是否有效即可
                if (!dcIsPrint && !exe.IsValid)
                {
                    continue;
                }

                //长嘱
                if (orderType == "1")
                {
                    if (!exe.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }
                //临嘱
                else if (orderType == "0")
                {
                    if (exe.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }

                //只打印首日量
                if (isFirst)
                {
                    if (exe.Order.MOTime.Date != exe.DateUse.Date)
                    {
                        continue;
                    }
                }
                #endregion
                alOrderTemp.Add(exe);
            }

            alOrder = alOrderTemp;

            #endregion

            this.curValues = alOrder;

            #region 将同一条医嘱合并在一起

            ArrayList alTemp = alOrder.Clone() as ArrayList;
            alOrder = new ArrayList();
            for (int k = 0; k < alTemp.Count; k++)
            {
                bool isHave = false;

                FS.HISFC.Models.Order.ExecOrder execOrd = ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]);

                #region 停用信息
                string strValid_execOrd = "";
                if (execOrd.Order.Status == 3 || execOrd.Order.Status == 4)
                {
                    if (execOrd.DateUse >= execOrd.Order.EndTime)
                    {
                        strValid_execOrd = "[停]";
                    }
                }

                #endregion

                TimeSpan span;
                string sMing = "";
                for (int j = 0; j < alOrder.Count; j++)
                {
                    sMing = "";
                    FS.HISFC.Models.Order.ExecOrder execOrder = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]);
                    if (execOrder.Order.ID == execOrd.Order.ID)
                    {
                        #region 停用信息
                        string strValid_execOrder = "";
                        if (execOrder.Order.Status == 3 || execOrder.Order.Status == 4)
                        {
                            if (execOrder.DateUse >= execOrder.Order.EndTime)
                            {
                                strValid_execOrder = "[停]";
                            }
                        }

                        #endregion

                        if ((execOrder.Order.Memo.Length > 2 &&
                                execOrder.Order.Memo.Substring(0, 2) == "时间"
                             )
                             || execOrder.Order.Frequency.Times.Length > (int)EnumCol.IsPrint)
                        {
                            isHave = true;
                            break;
                            //特殊频次
                        }
                        else
                        {
                            isHave = true;//包含添加时间

                            span = new TimeSpan(execOrd.DateUse.Date.Ticks - dtBegin.Date.Ticks);
                            if (span.Days == 1)
                            {
                                sMing = "明";
                            }
                            if (span.Days == 2)
                            {
                                sMing = "后";
                            }
                            if (span.Days > 2)
                            {
                                sMing = "[" + execOrd.DateUse.Day.ToString() + "日]";
                            }

                            if (execOrd.DateUse.Hour <= 12)
                            {
                                execOrder.Memo += "," + sMing + execOrd.DateUse.Hour.ToString() + "a" + strValid_execOrder;
                            }
                            else
                            {
                                execOrder.Memo += "," + sMing + (execOrd.DateUse.Hour - 12).ToString() + "p" + strValid_execOrder;
                            }
                            break;
                        }
                    }
                }
                if (!isHave)
                {

                    span = new TimeSpan(execOrd.DateUse.Date.Ticks - dtBegin.Date.Ticks);
                    if (span.Days == 1) sMing = "明";
                    if (span.Days == 2) sMing = "后";
                    if (span.Days > 2) sMing = "[" + execOrd.DateUse.Day.ToString() + "日]";
                    if (execOrd.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)//临时医嘱暂时不显示执行时间
                    {
                        if (execOrd.DateUse.Hour <= 12)
                        {
                            execOrd.Memo += sMing + execOrd.DateUse.Hour.ToString() + "a" + strValid_execOrd;
                        }
                        else
                        {
                            execOrd.Memo += sMing + (execOrd.DateUse.Hour - 12).ToString() + "p" + strValid_execOrd;
                        }

                        if (execOrd.Order.Frequency.Times.Length > 12)
                        {
                            execOrd.Memo = string.Empty;
                        }

                    }
                    else
                    {
                        //临时打印时间点
                        for (int i = 0; i < execOrd.Order.Frequency.Times.Length; i++)
                        {
                            DateTime time = Convert.ToDateTime(execOrd.Order.Frequency.Times[i]);
                            if (time.Hour <= 12)
                            {
                                execOrd.Memo += sMing + time.Hour.ToString() + "a," + strValid_execOrd;
                            }
                            else
                            {
                                execOrd.Memo += sMing + time.Hour.ToString() + "p," + strValid_execOrd;
                            }
                        }

                        execOrd.Memo = execOrd.Memo.TrimEnd(',');

                    }
                    alOrder.Add(alTemp[k]);
                }
            }

            #endregion
             * */
            #endregion

            this.SetValues(alOrder);
        }

        public void SetSpeOrderType(string speStr)
        {
            this.speOrderType = speStr;
            this.speOrderType = "";
        }

        #endregion

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.SetValue(i, (int)EnumCol.IsPrint, this.chkAll.Checked);
            }
        }

        private void ucExecBillFP_Load(object sender, EventArgs e)
        {
            if (lisPrint.Keys.Count == 0)
            {
                ArrayList list = constant.GetAllList("LisPrintItem");
                foreach (FS.FrameWork.Models.NeuObject obj in list)
                {
                    if (!lisPrint.ContainsKey(obj.ID))
                    {
                        lisPrint.Add(obj.ID, obj.ID);
                    }
                }
            }

            if (this.neuSpread1_Sheet1 != null)
            {
                if (System.IO.File.Exists(this.sheetXMLFileName))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.sheetXMLFileName);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, this.sheetXMLFileName);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();
            for (int index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
            {
                if (this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Text == "True")
                {
                    execOrder = this.neuSpread1_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.ExecOrder;
                    al.Add(execOrder);
                }
            }

            #region 检验单处理
            if (al.Count > 0)
            {

                string patientNO = string.Empty;
                ucLisBillPrint bill = new ucLisBillPrint();
                if (al.Count > 0)
                {
                    patientNO = (al[0] as FS.HISFC.Models.Order.ExecOrder).Order.Patient.ID;
                }
                Dictionary<string, List<FS.HISFC.Models.Order.Inpatient.Order>> lisPrint = new Dictionary<string, List<FS.HISFC.Models.Order.Inpatient.Order>>();
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Order.ExecOrder exeord = al[i] as FS.HISFC.Models.Order.ExecOrder;

                    if (patientNO != exeord.Order.Patient.ID)
                    {
                        foreach (string ApplyNo in lisPrint.Keys)
                        {
                            List<FS.HISFC.Models.Order.Inpatient.Order> alPrint = lisPrint[ApplyNo];
                            bill.SetPatientInfo(alPrint[0].Patient);
                            bill.SetPrintValue(alPrint, false);
                        }


                        patientNO = exeord.Order.Patient.ID;
                        lisPrint = new Dictionary<string, List<FS.HISFC.Models.Order.Inpatient.Order>>();

                    }

                    FS.HISFC.Models.Order.Inpatient.Order orderObj = this.orderManager.QueryOneOrder(exeord.Order.ID);
                    if (orderObj == null)
                    {
                        MessageBox.Show("查询医嘱出错：" + this.orderManager.Err);
                        return;
                    }
                    if (!lisPrint.ContainsKey(orderObj.ApplyNo))
                    {
                        List<FS.HISFC.Models.Order.Inpatient.Order> list = new List<FS.HISFC.Models.Order.Inpatient.Order>();
                        list.Add(orderObj);
                        lisPrint.Add(orderObj.ApplyNo, list);
                    }
                    else
                    {
                        lisPrint[orderObj.ApplyNo].Add(orderObj);
                    }


                    if (i == al.Count - 1)
                    {
                        foreach (string ApplyNo in lisPrint.Keys)
                        {
                            List<FS.HISFC.Models.Order.Inpatient.Order> alPrint = lisPrint[ApplyNo];
                            bill.SetPatientInfo(alPrint[0].Patient);
                            bill.SetPrintValue(alPrint, false);
                        }
                    }


                }



            }
            #endregion
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();
            for (int index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
            {
                this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Text = "False";
                execOrder = this.neuSpread1_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.ExecOrder;
                if (lisPrint.ContainsKey(execOrder.Order.Item.ID))
                {
                    this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.IsPrint].Text = "True";
                }
            }
        }

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, this.sheetXMLFileName);
        }
    }

    public class ComparerExecOrder : IComparer
    {
        #region IComparer 成员

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.ExecOrder execOrder1 = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder execOrder2 = y as FS.HISFC.Models.Order.ExecOrder;

                if (execOrder1.Equals(execOrder2))
                {
                    return 0;
                }
                //string aa = manager.GetBed(execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID.ToString().PadLeft(4, '0') + execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID;
                //string bb = manager.GetBed(execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID.ToString().PadLeft(4, '0') + execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID;

                string aa = execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID;
                string bb = execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID;

                string cc = execOrder1.Order.SubCombNO.ToString().PadLeft(5, '0') + execOrder1.DateUse.ToString() + execOrder1.Order.SortID.ToString() + execOrder1.Order.Combo.ID + execOrder1.Order.ID;
                string dd = execOrder2.Order.SubCombNO.ToString().PadLeft(5, '0') + execOrder2.DateUse.ToString() + execOrder2.Order.SortID.ToString() + execOrder2.Order.Combo.ID + execOrder2.Order.ID;

                if (string.Compare(aa, bb) > 0)
                {
                    return 1;
                }
                else if (string.Compare(aa, bb) == 0)
                {
                    return string.Compare(cc, dd);
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
    /// 执行单列设置
    /// </summary>
    enum EnumCol
    {
        /// <summary>
        /// 开立时间
        /// </summary>
        MOTime,// = 0,

        /// <summary>
        /// 床号
        /// </summary>
        BedID,// = 1,

        /// <summary>
        /// 患者姓名
        /// </summary>
        PatientName,// = 2,

        /// <summary>
        /// 组号
        /// </summary>
        SubCombNo,// = 3,

        /// <summary>
        /// 组标记
        /// </summary>
        CombFlag,// = 3,

        /// <summary>
        /// 项目名称
        /// </summary>
        ItemName,// = 4,

        /// <summary>
        /// 每次用量
        /// </summary>
        DoseOnce,// = 5,

        /// <summary>
        /// 用法
        /// </summary>
        Usage,// = 6,

        /// <summary>
        /// 频次
        /// </summary>
        Freq,//= 7,

        /// <summary>
        /// 数量
        /// </summary>
        Qty,//= 8,

        /// <summary>
        /// 使用时间
        /// </summary>
        UseTime,//= 9,

        /// <summary>
        /// 备注
        /// </summary>
        Memo,//= 10,

        /// <summary>
        /// 签名
        /// </summary>
        Sign,// = 11,

        /// <summary>
        /// 签名执行时间
        /// </summary>
        SignExecTime,// = 12,

        /// <summary>
        /// 是否打印 勾选
        /// </summary>
        IsPrint,//= 13,

        /// <summary>
        /// 组合号
        /// </summary>
        CombNo// = 14
    }
}
