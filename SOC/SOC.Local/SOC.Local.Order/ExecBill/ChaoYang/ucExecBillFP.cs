using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.Order.ExecBill.ChaoYang
{
    /// <summary>
    /// 执行单打印
    /// </summary>
    public partial class ucExecBillFP : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        public ucExecBillFP()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
        }

        #region IPrintTransFusion 成员

        Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        Neusoft.HISFC.BizLogic.Manager.Bed manager = new Neusoft.HISFC.BizLogic.Manager.Bed();
        string speOrderType = "";

        ArrayList curValues = null; //当前显示的数据

        /// <summary>
        /// 是否补打
        /// </summary>
        bool isRePrint = false;

        List<Neusoft.HISFC.Models.RADT.PatientInfo> myPatients = null;
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
                                string printText = spread.ActiveSheet.Cells[curPageFirstRowIndex, 1].Text;
                                string printText2 = spread.ActiveSheet.Cells[curPageFirstRowIndex - 1, 1].Text;
                                if (printText == printText2)
                                {

                                    graphics.DrawString(
                                       printText,
                                       spread.Font,
                                       new SolidBrush(Color.Black),
                                     locationX + c.Location.X + spread.ActiveSheet.Columns[0].Width,
                                        locationY + c.Location.Y + spread.ActiveSheet.RowHeader.Rows[0].Height + spread.ActiveSheet.Rows[curPageFirstRowIndex].Height);
                                    //姓名
                                    printText = spread.ActiveSheet.Cells[curPageFirstRowIndex, 2].Text;

                                    graphics.DrawString(
                                       printText,
                                       spread.Font,
                                       new SolidBrush(Color.Black),
                                     locationX + c.Location.X + spread.ActiveSheet.Columns[0].Width + spread.ActiveSheet.Columns[1].Width,
                                        locationY + c.Location.Y + spread.ActiveSheet.RowHeader.Rows[0].Height + spread.ActiveSheet.Rows[curPageFirstRowIndex].Height);
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
                //if (this.dwMain.RowCount <= 0)
                //{
                //    return;
                //}

                ////数据窗口打印
                //dwMain.Print(true, true);

                Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;

                Neusoft.HISFC.BizLogic.Manager.PageSize pgMgr = new Neusoft.HISFC.BizLogic.Manager.PageSize();
                Neusoft.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("execBillPaper");
                System.Drawing.Printing.PaperSize curPaperSize = new System.Drawing.Printing.PaperSize();
                if (pSize == null)
                {

                    pSize = new Neusoft.HISFC.Models.Base.PageSize("Letter", 800, 1100);
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
                Neusoft.HISFC.Models.Order.ExecOrder execOrder = new Neusoft.HISFC.Models.Order.ExecOrder();

                ArrayList alItems = new ArrayList();

                for (index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
                {
                    if (this.neuSpread1_Sheet1.Cells[index, 12].Text == "False")
                    {
                        this.neuSpread1_Sheet1.Rows[index].Visible = false;
                    }
                    else
                    {
                        execOrder = this.neuSpread1_Sheet1.Rows[index].Tag as Neusoft.HISFC.Models.Order.ExecOrder;
                        al.Add(execOrder);

                        string str = "";

                        for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
                        {
                            str += this.neuSpread1_Sheet1.Cells[index, j].Text + "|";
                        }

                        str = str.Substring(0, str.Length - 1);

                        alItems.Add(str);
                    }

                }
                //只更新打印的执行单状态
                this.curValues = al;
                //设置“是否打印”列不显示
                this.neuSpread1_Sheet1.Columns[12].Visible = false;
                print.SetPageSize(pSize);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("没有可打印的数据，请重新选择！");
                }
                else
                {
                    if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        this.PrintView(curPaperSize);
                    }
                    else
                    {
                        this.PrintPage(curPaperSize);
                    }

                    //this.pnPrint.Dock = DockStyle.None;
                    //this.pnPrint.Size = new Size(784, 550);
                    ////print.PrintPage(0, 0, pnPrint);
                    //this.pnPrint.Dock = DockStyle.Fill;


                    //ArrayList alPrint = new ArrayList();
                    //int page = 0;

                    //for (int i = 0; i < alItems.Count; i++)
                    //{
                    //    alPrint.Add(alItems[i] as string);

                    //    if (i % 42 == 0 && i != 0)
                    //    {
                    //        ucExecBillPrint ucPrint = new ucExecBillPrint();
                    //        ucPrint.AlOrders = alPrint;
                    //        ucPrint.Title = this.lblTitle.Text;
                    //        ucPrint.TitleDate = this.neuLblExecTime.Text;

                    //        page++;
                    //        ucPrint.Page = page.ToString();
                    //        if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                    //        {
                    //            print.PrintPreview(0, 0, ucPrint);
                    //        }
                    //        else
                    //        {
                    //            print.PrintPage(0, 0, ucPrint);
                    //        }
                    //        alPrint = new ArrayList();
                    //    }
                    //}

                    //alPrint = new ArrayList();

                    //if (alItems.Count > page * 42)
                    //{
                    //    if (page == 0)
                    //    {
                    //        for (int i = page * 42; i < alItems.Count; i++)
                    //        {
                    //            alPrint.Add(alItems[i] as string);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        for (int i = page * 42+1; i < alItems.Count; i++)
                    //        {
                    //            alPrint.Add(alItems[i] as string);
                    //        }
                    //    }

                    //    ucExecBillPrint ucPrint = new ucExecBillPrint();
                    //    ucPrint.AlOrders = alPrint;
                    //    ucPrint.Title = this.lblTitle.Text;
                    //    ucPrint.TitleDate = this.neuLblExecTime.Text;

                    //    page++;
                    //    ucPrint.Page = page.ToString();

                    //    if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                    //    {
                    //        print.PrintPreview(0, 0, ucPrint);
                    //    }
                    //    else
                    //    {
                    //        print.PrintPage(0, 0, ucPrint);
                    //    }
                    //}

                }
                //打印后设置“是否打印”列显示
                this.neuSpread1_Sheet1.Columns[12].Visible = true;

                #region 更新已经打印标记
                if (!this.isRePrint)
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                    //Neusoft.FrameWork.Management.Transaction t = new Neusoft.FrameWork.Management.Transaction(orderManager.Connection);
                    //t.BeginTransaction();

                    this.orderManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                    string itemType = null;
                    for (int i = 0; i < this.curValues.Count; i++)
                    {
                        Neusoft.HISFC.Models.Order.ExecOrder exeord = curValues[i] as Neusoft.HISFC.Models.Order.ExecOrder;
                        //if (exeord.Order.Item.IsPharmacy)
                        if (exeord.Order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            itemType = "1";
                        }
                        else
                        {
                            itemType = "2";
                        }
                        //if (this.orderManager.UpdateExecOrderPrinted(exeord.id, itemType) == -1)
                        //{
                        //    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show("更新打印标记失败!" + orderManager.Err);
                        //    return;
                        //}
                        //通过医嘱流水号更新执行档
                        if (this.orderManager.UpdateExecOrderPrintedByMoOrder(exeord.Order.ID, dt1.ToString(), dt2.ToString(), itemType) == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新打印标记失败!" + orderManager.Err);
                            return;
                        }
                    }
                    Neusoft.FrameWork.Management.PublicTrans.Commit();

                    this.Query(myPatients, usage, dt1, dt2, isRePrint);
                }
                #endregion
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
        /// <param name="patients"></param>
        /// <param name="usagecode"></param>
        /// <param name="dtTime"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtTime, bool isPrinted)
        {
            return;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted)
        {
            this.neuLblExecTime.Text = "执行时间:" + dtBegin.ToString() + "-" + dtEnd.ToString();
            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.isRePrint = isPrinted;
            this.usage = usageCode;
            //给患者列表赋值
            this.myPatients = patients;
            //更改治疗单标题
            //this.dwMain.Modify("t_title.text= " + "'" + this.Tag.ToString() + "'");

            this.lblTitle.Text = this.Tag.ToString();

            #region {0A2D3FF0-85B0-4322-8E0A-9E11D02344EF}
            try
            {
                Neusoft.HISFC.BizProcess.Integrate.Manager managerMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();
                string hosname = managerMgr.GetHospitalName();
                //this.dwMain.Modify("t_hospitalname.text= " + "'" + hosname + "'");
            }
            catch { }
            #endregion
            ArrayList alOrder = new ArrayList();
            ArrayList al = new ArrayList();
            string paramPatient = "";
            //获得in的患者id参数
            for (int i = 0; i < patients.Count; i++)
            {
                Neusoft.HISFC.Models.RADT.PatientInfo p = patients[i] as Neusoft.HISFC.Models.RADT.PatientInfo;
                paramPatient = "'" + p.ID + "'," + paramPatient;
                //获得护理分组

                p.PVisit.PatientLocation.Bed.Memo = manager.GetNurseTendGroupFromBed(p.PVisit.PatientLocation.Bed.ID);

                if (p.PVisit.PatientLocation.Bed == null)
                {
                    MessageBox.Show(manager.Err);
                    return;
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


            alOrder = this.orderManager.QueryOrderExecBill(paramPatient, dtBegin, dtEnd, usageCode, isPrinted);//查询医嘱，传入参数

            ArrayList alOrderNew = new ArrayList();

            //不设置，默认查看全部
            if (this.speOrderType.Length <= 0)
            {
                foreach (Neusoft.HISFC.Models.Order.ExecOrder order in alOrder)
                {
                    alOrderNew.Add(order);
                }
            }
            else
            {
                //只要包含一个，就认为可以看到
                string[] speStr = this.speOrderType.Split(',');

                foreach (Neusoft.HISFC.Models.Order.ExecOrder order in alOrder)
                {
                    bool isAdd = false;

                    foreach (string s in speStr)
                    {
                        if (order.Order.SpeOrderType == "" || order.Order.SpeOrderType.IndexOf(s) >= 0)
                        {
                            isAdd = true;
                            break;
                        }
                    }

                    if (isAdd)
                        alOrderNew.Add(order);
                }
            }

            alOrder = alOrderNew;


            Neusoft.HISFC.Models.Order.Inpatient.Order orderObj = new Neusoft.HISFC.Models.Order.Inpatient.Order();
            ArrayList alOrderTemp = new ArrayList();

            foreach (Neusoft.HISFC.Models.Order.ExecOrder exe in alOrder)
            {
                #region 医嘱停用后的不显示了

                orderObj = this.orderManager.QueryOneOrder(exe.Order.ID);
                if (orderObj == null)
                {
                    MessageBox.Show("查询医嘱出错：" + this.orderManager.Err);
                    return;
                }

                if (!exe.IsExec)
                {
                    continue;
                }

                //重整、停止医嘱
                if ("3,4".Contains(orderObj.Status.ToString()))
                {
                    if (exe.DateUse > orderObj.DCOper.OperTime)
                    {
                        continue;
                    }
                }
                #endregion
                alOrderTemp.Add(exe);
            }

            alOrder = alOrderTemp;

            this.curValues = alOrder;

            #region 将同一条医嘱合并在一起
            ArrayList alTemp = alOrder.Clone() as ArrayList;
            alOrder = new ArrayList();
            for (int k = 0; k < alTemp.Count; k++)
            {
                bool isHave = false;

                TimeSpan span;
                string sMing = "";
                for (int j = 0; j < alOrder.Count; j++)
                {
                    sMing = "";
                    if (((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ID == ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.ID)
                    {
                        if (((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Length > 2 &&
                            ((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Substring(0, 2) == "时间")
                        {
                            //特殊频次
                        }
                        else
                        {
                            isHave = true;//包含添加时间
                            span = new TimeSpan(((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                            if (span.Days == 1) sMing = "明";
                            if (span.Days == 2) sMing = "后";
                            if (span.Days > 2) sMing = "[" + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                            ((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Memo += "," + sMing + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');
                            break;
                        }
                    }
                }
                if (!isHave)
                {
                    span = new TimeSpan(((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                    if (span.Days == 1) sMing = "明";
                    if (span.Days == 2) sMing = "后";
                    if (span.Days > 2) sMing = "[" + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                    if (((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.OrderType.Type == Neusoft.HISFC.Models.Order.EnumType.LONG)//临时医嘱暂时不显示执行时间 by zuowy
                        ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo = sMing + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');
                    alOrder.Add(alTemp[k]);
                }
            }

            #endregion

            string Combno = "";
            ArrayList alComb = new ArrayList();

            #region 至组合号
            for (int j = 0; j < alOrder.Count; j++)
            {
                Neusoft.HISFC.Models.Order.ExecOrder obj;

                obj = (Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j];
                for (int kk = 0; kk < patients.Count; kk++)
                {
                    if (((Neusoft.FrameWork.Models.NeuObject)patients[kk]).ID == obj.Order.Patient.ID)
                    {
                        obj.Order.Patient = ((Neusoft.HISFC.Models.RADT.PatientInfo)patients[kk]).Clone();
                        break;
                    }
                }
                //判断组合
                if (obj.Order.Combo.ID != "0" && obj.Order.Combo.ID != "")
                {
                    if (Combno != obj.Order.Combo.ID + obj.DateUse.ToString())
                    {
                        //非组合
                        if (alComb.Count == 1) al.Add((Neusoft.HISFC.Models.Order.ExecOrder)alComb[0]);
                        //组合
                        else if (alComb.Count > 1)
                        {
                            for (int n = 0; n < alComb.Count; n++)
                            {
                                Neusoft.HISFC.Models.Order.ExecOrder objC;
                                objC = (Neusoft.HISFC.Models.Order.ExecOrder)alComb[n];
                                if (n == 0) objC.Order.Combo.Memo = "┏";
                                else if (n == alComb.Count - 1) objC.Order.Combo.Memo = "┗";
                                else objC.Order.Combo.Memo = "┃";
                                al.Add(objC);
                            }
                        }
                        alComb = new ArrayList();
                        alComb.Add(obj);
                        Combno = obj.Order.Combo.ID + obj.DateUse.ToString();
                        if (j == alOrder.Count - 1) al.Add(obj);//最后一条
                    }
                    else
                    {
                        alComb.Add(obj);
                        if (j == alOrder.Count - 1)
                        {
                            for (int row = 0; row < alComb.Count; row++)
                            {
                                Neusoft.HISFC.Models.Order.ExecOrder exe = alComb[row] as Neusoft.HISFC.Models.Order.ExecOrder;
                                if (alComb.Count == 1)
                                {
                                    al.Add(exe);
                                    break;
                                }
                                if (row == 0) exe.Order.Combo.Memo = "┏";
                                else if (row == alComb.Count - 1) exe.Order.Combo.Memo = "┗";
                                else exe.Order.Combo.Memo = "┃";
                                al.Add(exe);
                            }
                        }
                    }
                }
                else al.Add(obj);
            }
            #endregion

            this.SetValues(al);
            return;

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

                ArrayList newList = new ArrayList();
                //alValues.Sort(new ComparerExecOrder());
                this.AddConstsToTable(alValues, ref newList);

                //SQL已经按照sortID、mo_order排序了，这里再根据床号、住院号排序，保证一个患者的药品非药品在一起
                newList.Sort(new ComparerExecOrder());
                this.AddToFP(newList);
                this.AddLine();
            }
        }

        /// <summary>
        /// 画边框
        /// </summary>
        private void AddLine()
        {
            #region 内容

            //只显示下面的边框
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;

                    if (i != this.neuSpread1_Sheet1.RowCount - 1)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "┏" || this.neuSpread1_Sheet1.Cells[i, 3].Text == "┃")
                        {
                            this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder3;
                        }
                        if (this.neuSpread1_Sheet1.RowCount > 1)
                        {
                            if (this.neuSpread1_Sheet1.Cells[i, 1].Text != this.neuSpread1_Sheet1.Cells[i + 1, 1].Text)
                            {
                                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                            }
                        }
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                    }

                    this.neuSpread1_Sheet1.Cells[i, 1].Border = bevelBorder2;
                    this.neuSpread1_Sheet1.Cells[i, 2].Border = bevelBorder2;
                }
            }

            #endregion

            #region 之前的作废
            //if (this.neuSpread1_Sheet1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount - 1; i++)
            //    {
            //        if (i == 0 && i != this.neuSpread1_Sheet1.RowCount - 1)
            //        {
            //            //组标记
            //            if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 3].Text))
            //            {
            //                //是否有两行
            //                if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i + 1, 2].Text))//姓名
            //                {
            //                    //加
            //                    this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
            //                }
            //            }
            //        }
            //        //最后一行加边框
            //        else if (i == this.neuSpread1_Sheet1.RowCount - 1)
            //        {
            //            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = bevelBorder1;
            //        }
            //        else
            //        {
            //            if (this.neuSpread1_Sheet1.Cells[i, 3].Text == "┗")
            //            {
            //                //是否有两行
            //                if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i + 1, 2].Text))//姓名
            //                {
            //                    //加
            //                    this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
            //                }
            //            }
            //            //组标记
            //            else if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 3].Text))
            //            {
            //                //是否有两行
            //                if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i + 1, 2].Text)
            //                    && (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i - 1, 3].Text)
            //                    || this.neuSpread1_Sheet1.Cells[i - 1, 3].Text == "┗"))//姓名
            //                {
            //                    //加
            //                    this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
            //                }
            //            }

            //            if (this.neuSpread1_Sheet1.Cells[i, 1].Text != this.neuSpread1_Sheet1.Cells[i + 1, 1].Text)
            //            {
            //                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
            //            }
            //        }

            //        //合并的边框都画上
            //        this.neuSpread1_Sheet1.Cells[i, 1].Border = bevelBorder2;
            //        this.neuSpread1_Sheet1.Cells[i, 2].Border = bevelBorder2;
            //    }
            //}
            #endregion

            #region 标题

            FarPoint.Win.BevelBorder headerBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, true, false, true);

            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnHeader.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.ColumnHeader.Rows[i].Border = headerBorder;
            }

            #endregion
        }

        Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam con = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

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
            this.neuSpread1_Sheet1.Columns[12].CellType = chb;

            bool bDealItemName = true;

            bDealItemName = con.GetControlParam<bool>("HNHS01", false, false);

            foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in list)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Rows[index].Tag = execOrder;
                //开始时间
                this.neuSpread1_Sheet1.Cells[index, 0].Text = execOrder.Order.MOTime.ToString("dd") + "/" + execOrder.Order.MOTime.ToString("MM");
                //床号
                this.neuSpread1_Sheet1.Cells[index, 1].Text = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                //姓名
                this.neuSpread1_Sheet1.Cells[index, 2].Text = execOrder.Order.Patient.Name;
                if (execOrder.Order.Patient.Name.Length > 3)
                {
                    this.neuSpread1_Sheet1.Cells[index, 2].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }

                //组号
                this.neuSpread1_Sheet1.Cells[index, 3].Text = execOrder.Order.Combo.Memo;
                this.neuSpread1_Sheet1.Cells[index, 3].Tag = execOrder.Order.Combo.ID;
                //名称
                //this.neuSpread1_Sheet1.Cells[index, 4].Text = execOrder.Order.Item.Name + "  " + execOrder.Order.Item.Specs + "  " + execOrder.Order.Memo;
                //每次量
                this.neuSpread1_Sheet1.Cells[index, 8].Text = execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit.TrimEnd(' ');
                //频次
                this.neuSpread1_Sheet1.Cells[index, 6].Text = execOrder.Order.Frequency.ID;
                //用法
                this.neuSpread1_Sheet1.Cells[index, 7].Text = execOrder.Order.Usage.Name;
                //数量
                this.neuSpread1_Sheet1.Cells[index, 5].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.DoseUnit;
                //备注 +皮试
                this.neuSpread1_Sheet1.Cells[index, 9].Text = execOrder.Memo;
                //this.neuSpread1_Sheet1.Cells[index, 9].Text = "";
                //签名
                this.neuSpread1_Sheet1.Cells[index, 10].Text = "";
                //执行时间
                this.neuSpread1_Sheet1.Cells[index, 11].Text = ".";
                //默认全部打印
                this.neuSpread1_Sheet1.Cells[index, 12].Text = "True";
                //药品名称

                if (bDealItemName && execOrder.Order.Item.Name.IndexOf("(") > 0)
                {
                    int indexR = 0;

                    for (int j = 0; j < execOrder.Order.Item.Name.Length; j++)
                    {
                        if (execOrder.Order.Item.Name.Substring(j, 1) == "(")
                        {
                            indexR = j;
                        }
                    }

                    execOrder.Order.Item.Name = execOrder.Order.Item.Name.Substring(0, indexR);
                }

                if (!string.IsNullOrEmpty(execOrder.Order.Memo))
                {
                    execOrder.Order.Memo = "(" + execOrder.Order.Memo + ")";
                }

                if (execOrder.Order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    Neusoft.HISFC.Models.Pharmacy.Item phaitem = (execOrder.Order.Item as Neusoft.HISFC.Models.Pharmacy.Item);

                    //if (phaitem.MinUnit != phaitem.DoseUnit)
                    //{
                    if (execOrder.Order.BeginTime.ToShortDateString() == execOrder.DateUse.ToShortDateString())
                    {
                        itemName = "[新] " + execOrder.Order.Item.Name + execOrder.Order.Memo + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                    }
                    else
                    {
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                    }
                    //}
                    //else
                    //{
                    //    itemName = execOrder.Order.Item.Name + execOrder.Order.Memo;
                    //}
                }
                else
                {
                    if (!string.IsNullOrEmpty(execOrder.Order.Item.Specs))
                    {
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo + "[" + execOrder.Order.Item.Specs + "]";
                    }
                    else
                    {
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo;
                    }
                }
                //名称过长自动换行
                if (System.Text.Encoding.GetEncoding("gb2312").GetBytes(itemName).Length > length)
                {
                    this.neuSpread1_Sheet1.Rows[index].Height = 2 * this.neuSpread1_Sheet1.Rows[index].Height;
                }
                this.neuSpread1_Sheet1.Cells[index, 4].Text = itemName;

                index++;
            }

            for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
            {
                if (j != this.neuSpread1_Sheet1.Columns.Count - 1)
                {
                    this.neuSpread1_Sheet1.Columns[j].Locked = true;
                }
            }
        }

        private void AddConstsToTable(ArrayList list, ref ArrayList newList)
        {
            foreach (Neusoft.HISFC.Models.Order.ExecOrder objExc in list)
            {
                if (!objExc.IsExec)
                {
                    continue;
                }

                //if (!objExc.Order.Item.IsPharmacy)
                if (objExc.Order.Item.ItemType != Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    objExc.Order.DoseOnce = 0;
                    objExc.Order.DoseUnit = "";
                }

                try
                {

                    //if (objExc.Order.Item.IsPharmacy == false)
                    if (objExc.Order.Item.ItemType != Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (objExc.Order.OrderType.Type == Neusoft.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (objExc.Order.IsEmergency == true)
                            {
                                if (objExc.Order.Note != "")
                                    objExc.Memo = "加急[" + objExc.Order.Note + "]";
                                else
                                    objExc.Memo = "加急";
                            }
                            else
                            {
                                if (objExc.Order.Note != "")
                                    objExc.Memo = objExc.Order.Note;
                                else
                                    objExc.Memo += "";
                            }
                        }
                        else
                        {
                            objExc.Order.DoseOnce = objExc.Order.Qty;
                            objExc.Order.DoseUnit = objExc.Order.Unit;
                            objExc.Memo += objExc.Order.Note;
                        }

                        if (objExc.Order.Memo == objExc.Memo) objExc.Memo = "";

                        if (objExc.Order.OrderType.ID == "BL")//补录医嘱,备注显示补录字样。
                            objExc.Order.Memo = objExc.Order.Memo + "[补录]";
                        Neusoft.HISFC.Models.Fee.Item.Undrug objAssets = objExc.Order.Item as Neusoft.HISFC.Models.Fee.Item.Undrug;
                        newList.Add(objExc);
                    }
                    else
                    {
                        if (objExc.Order.OrderType.Type == Neusoft.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (objExc.Order.Note != "")
                                objExc.Memo = objExc.Order.Note;
                            else
                                objExc.Memo += "";
                        }
                        else
                            objExc.Memo += objExc.Order.Note;

                        if (objExc.Order.Memo == objExc.Memo) objExc.Memo = "";
                        //if (objExc.Order.Memo != "" && objExc.Order.Memo.IndexOf("需皮试") != -1)
                        //{
                        try
                        {
                            int hypotest = this.orderManager.QueryOrderHypotest(objExc.Order.ID);
                            if (SOC.Local.Order.Classes.Function.GetPhaItem(objExc.Order.Item.ID) != null && SOC.Local.Order.Classes.Function.GetPhaItem(objExc.Order.Item.ID).IsAllergy)
                            {
                                objExc.Order.Memo += orderManager.TransHypotest((Neusoft.HISFC.Models.Order.EnumHypoTest)hypotest);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("获得皮试信息出错！", "Note");
                        }

                        if (objExc.Order.OrderType.ID == "BL")//补录医嘱,备注显示补录字样。
                            objExc.Order.Memo = objExc.Order.Memo + "[补录]";
                        Neusoft.HISFC.Models.Pharmacy.Item objPharmacy = objExc.Order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                        newList.Add(objExc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        #endregion

        #region 事件



        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 12)
            {
                string combNo = neuSpread1_Sheet1.Cells[e.Row, 3].Tag == null ? "" : neuSpread1_Sheet1.Cells[e.Row, 3].Tag.ToString();
                if (string.IsNullOrEmpty(combNo))
                {
                    return;
                }
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (neuSpread1_Sheet1.Cells[i, 3].Tag != null
                        && neuSpread1_Sheet1.Cells[i, 3].Tag.ToString() == combNo)
                    {
                        this.neuSpread1_Sheet1.Cells[i, 12].Text = neuSpread1_Sheet1.Cells[e.Row, e.Column].Text;
                    }
                }
            }
        }

        #endregion

        #region IPrintTransFusion 成员


        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType)
        {
            return;
        }

        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, bool isFirst)
        {
            this.neuLblExecTime.Text = "执行时间:" + dtBegin.ToString() + "-" + dtEnd.ToString();
            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.isRePrint = isPrinted;

            //给患者列表赋值
            this.myPatients = patients;
            //更改治疗单标题
            //this.dwMain.Modify("t_title.text= " + "'" + this.Tag.ToString() + "'");

            this.lblTitle.Text = this.Tag.ToString();

            try
            {
                Neusoft.HISFC.BizProcess.Integrate.Manager managerMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();
                string hosname = managerMgr.GetHospitalName();
                //this.dwMain.Modify("t_hospitalname.text= " + "'" + hosname + "'");
            }
            catch { }

            ArrayList alOrder = new ArrayList();
            ArrayList al = new ArrayList();
            string paramPatient = "";
            //获得in的患者id参数
            for (int i = 0; i < patients.Count; i++)
            {
                Neusoft.HISFC.Models.RADT.PatientInfo p = patients[i] as Neusoft.HISFC.Models.RADT.PatientInfo;
                paramPatient = "'" + p.ID + "'," + paramPatient;
                //获得护理分组

                p.PVisit.PatientLocation.Bed.Memo = manager.GetNurseTendGroupFromBed(p.PVisit.PatientLocation.Bed.ID);

                if (p.PVisit.PatientLocation.Bed == null)
                {
                    MessageBox.Show(manager.Err);
                    return;
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


            alOrder = this.orderManager.QueryOrderExecBill(paramPatient, dtBegin, dtEnd, usageCode, isPrinted);//查询医嘱，传入参数

            if (alOrder == null) //为提示出错 
            {
                MessageBox.Show(orderManager.Err);
                return;
            }
            else
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order orderObj = new Neusoft.HISFC.Models.Order.Inpatient.Order();
                ArrayList alOrderTemp = new ArrayList();

                foreach (Neusoft.HISFC.Models.Order.ExecOrder exe in alOrder)
                {
                    #region 医嘱停用后的不显示了

                    orderObj = this.orderManager.QueryOneOrder(exe.Order.ID);
                    if (orderObj == null)
                    {
                        MessageBox.Show("查询医嘱出错：" + this.orderManager.Err);
                        return;
                    }

                    if (!exe.IsExec)
                    {
                        continue;
                    }

                    //重整、停止医嘱
                    if ("3,4".Contains(orderObj.Status.ToString()))
                    {
                        if (exe.DateUse > orderObj.DCOper.OperTime)
                        {
                            continue;
                        }
                    }
                    #endregion

                    if (isFirst)
                    {
                        if (exe.Order.BeginTime.ToShortDateString() == exe.DateUse.ToShortDateString())
                        {
                            alOrderTemp.Add(exe);
                        }
                    }
                    else
                    {
                        alOrderTemp.Add(exe);
                    }
                }
                alOrder = alOrderTemp;
            }

            this.curValues = alOrder;

            #region 将同一条医嘱合并在一起
            ArrayList alTemp = alOrder.Clone() as ArrayList;
            alOrder = new ArrayList();
            for (int k = 0; k < alTemp.Count; k++)
            {
                bool isHave = false;

                TimeSpan span;
                string sMing = "";
                for (int j = 0; j < alOrder.Count; j++)
                {
                    sMing = "";
                    if (((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ID == ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.ID)
                    {
                        if (((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Length > 2 &&
                            ((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Substring(0, 2) == "时间")
                        {
                            //特殊频次
                        }
                        else
                        {
                            isHave = true;//包含添加时间
                            span = new TimeSpan(((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                            if (span.Days == 1) sMing = "明";
                            if (span.Days == 2) sMing = "后";
                            if (span.Days > 2) sMing = "[" + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                            ((Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j]).Memo += "," + sMing + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');
                            break;
                        }
                    }
                }
                if (!isHave)
                {
                    span = new TimeSpan(((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                    if (span.Days == 1) sMing = "明";
                    if (span.Days == 2) sMing = "后";
                    if (span.Days > 2) sMing = "[" + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                    if (((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.OrderType.Type == Neusoft.HISFC.Models.Order.EnumType.LONG)//临时医嘱暂时不显示执行时间 by zuowy
                        ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo = sMing + ((Neusoft.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');
                    alOrder.Add(alTemp[k]);
                }
            }

            #endregion

            string Combno = "";
            ArrayList alComb = new ArrayList();

            #region 画组合号
            for (int j = 0; j < alOrder.Count; j++)
            {
                Neusoft.HISFC.Models.Order.ExecOrder obj;

                obj = (Neusoft.HISFC.Models.Order.ExecOrder)alOrder[j];
                for (int kk = 0; kk < patients.Count; kk++)
                {
                    if (((Neusoft.FrameWork.Models.NeuObject)patients[kk]).ID == obj.Order.Patient.ID)
                    {
                        obj.Order.Patient = ((Neusoft.HISFC.Models.RADT.PatientInfo)patients[kk]).Clone();
                        break;
                    }
                }
                //判断组合
                if (obj.Order.Combo.ID != "0" && obj.Order.Combo.ID != "")
                {
                    if (Combno != obj.Order.Combo.ID + obj.DateUse.ToString())
                    {
                        //非组合
                        if (alComb.Count == 1) al.Add((Neusoft.HISFC.Models.Order.ExecOrder)alComb[0]);
                        //组合
                        else if (alComb.Count > 1)
                        {
                            for (int n = 0; n < alComb.Count; n++)
                            {
                                Neusoft.HISFC.Models.Order.ExecOrder objC;
                                objC = (Neusoft.HISFC.Models.Order.ExecOrder)alComb[n];
                                if (n == 0) objC.Order.Combo.Memo = "┏";
                                else if (n == alComb.Count - 1) objC.Order.Combo.Memo = "┗";
                                else objC.Order.Combo.Memo = "┃";
                                al.Add(objC);
                            }
                        }
                        alComb = new ArrayList();
                        alComb.Add(obj);
                        Combno = obj.Order.Combo.ID + obj.DateUse.ToString();
                        if (j == alOrder.Count - 1) al.Add(obj);//最后一条
                    }
                    else
                    {
                        alComb.Add(obj);
                        if (j == alOrder.Count - 1)
                        {
                            for (int row = 0; row < alComb.Count; row++)
                            {
                                Neusoft.HISFC.Models.Order.ExecOrder exe = alComb[row] as Neusoft.HISFC.Models.Order.ExecOrder;
                                if (alComb.Count == 1)
                                {
                                    al.Add(exe);
                                    break;
                                }
                                if (row == 0) exe.Order.Combo.Memo = "┏";
                                else if (row == alComb.Count - 1) exe.Order.Combo.Memo = "┗";
                                else exe.Order.Combo.Memo = "┃";
                                al.Add(exe);
                            }
                        }
                    }
                }
                else al.Add(obj);
            }
            #endregion

            this.SetValues(al);
            return;
        }

        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType, bool isFirst)
        {
            return;
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
                this.neuSpread1_Sheet1.SetValue(i, 12, this.chkAll.Checked);
            }
        }
    }

    public class ComparerExecOrder : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                Neusoft.HISFC.Models.Order.ExecOrder execOrder1 = x as Neusoft.HISFC.Models.Order.ExecOrder;
                Neusoft.HISFC.Models.Order.ExecOrder execOrder2 = y as Neusoft.HISFC.Models.Order.ExecOrder;
                string aa = execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(5,'0') + execOrder1.Order.Patient.ID + execOrder1.Order.SubCombNO.ToString().PadLeft(4, '0') + execOrder1.Order.Combo + execOrder1.Order.ID;
                string bb = execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(5, '0') + execOrder2.Order.Patient.ID + execOrder1.Order.SubCombNO.ToString().PadLeft(4, '0') + execOrder2.Order.Combo + execOrder2.Order.ID;
                return string.Compare(aa, bb);
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
