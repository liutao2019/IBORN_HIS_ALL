using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;
using SOC.Local.Pharmacy.ZhuHai.Print.ZDWY;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    /// <summary>
    /// 东莞药品入库计划打印
    /// </summary>
    public partial class ucPlanBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucPlanBill()
        {
            InitializeComponent();
            PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
            PrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_EndPrint);
        }

        #region 变量

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }
        FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        private decimal totPurchaseCost = 0;
        private decimal totRetailCost = 0;

        private string drugType = "";


        private Base.PrintBill printBill = new Base.PrintBill();

        private DateTime fOperDate = DateTime.Now;

        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
        #endregion

        #region 打印相关

        void PrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.PrintDocument.PrintController.IsPreview == false)
            {
                printPreviewDialog.Close();
                printPreviewDialog.Dispose();
            }

        }

        void PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            curPageNO = 1;
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

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(0, 0, 10, 30);

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //跳过选择打印范围外的数据
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                this.curPageNO = 1;
                //this.maxPageNO = 1;
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 标题绘制
            int mainTitleLocalX = this.DrawingMargins.Left + this.lblTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.lblTitle.Location.Y;
            graphics.DrawString(this.lblTitle.Text, this.lblTitle.Font, new SolidBrush(this.lblTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblCheckDate.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblCheckDate.Location.Y;
            graphics.DrawString(this.lblCheckDate.Text, this.lblCheckDate.Font, new SolidBrush(this.lblCheckDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region 页码绘制

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPage.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPage.Location.Y;

            graphics.DrawString("页码：" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.lblPage.Font, new SolidBrush(this.lblPage.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel4.Height - neuPanel2.Height;
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel4.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
            int FarpointHeight = 0;
            if (curPageNO == maxPageNO)
            {
                if (alPrintData.Count % printBill.RowCount == 0)
                {
                    FarpointHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 5);
                }
                else
                {
                    FarpointHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * (alPrintData.Count % printBill.RowCount) + 5);
                }
            }
            else
            {
                FarpointHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 5);
            }
            #endregion

            #region 页尾绘制
            if (curPageNO == maxPageNO)
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurPur.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurPur.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.lblCurPur.Text, this.lblCurPur.Font, new SolidBrush(this.lblCurPur.ForeColor), additionTitleLocalX, additionTitleLocalY);

                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurRet.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurRet.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.lblCurRet.Text, this.lblCurRet.Font, new SolidBrush(this.lblCurRet.ForeColor), additionTitleLocalX, additionTitleLocalY);

                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurDif.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurDif.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.lblCurDif.Text, this.lblCurDif.Font, new SolidBrush(this.lblCurDif.ForeColor), additionTitleLocalX, additionTitleLocalY);

                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurPur.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurPur.Location.Y + this.neuPanel4.Height + FarpointHeight;
            }

          
            #endregion

            #region 分页
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                //maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        /// <summary>
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel4.Height - this.neuPanel2.Height;

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = true;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuFpEnter1_Sheet1.RowHeader.Visible;
            this.neuFpEnter1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuFpEnter1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel4.Height, drawingWidth, drawingHeight), 0);

            socPrintPageSelectDialog.MaxPageNO = this.maxPageNO;
            if (this.maxPageNO > 1)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return false;
                }
            }

            return true;
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
                paperSize = new System.Drawing.Printing.PaperSize(printBill.PageSize.ID, printBill.PageSize.Width, printBill.PageSize.Height);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        PrintPreviewDialog printPreviewDialog = null;

        private void myPrintView()
        {
            printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                if (((DialogResult)printPreviewDialog.ShowDialog()) == DialogResult.OK)
                {
                    printPreviewDialog.Close();
                }
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

        #endregion

        /// <summary>
        /// 2打印函数
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region 打印信息设置
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();


            int pageTotNum = alPrintData.Count / this.printBill.RowCount;
            if (alPrintData.Count != this.printBill.RowCount * pageTotNum)
            {
                pageTotNum++;
            }


            decimal alPurhanceCost = 0m;

            decimal alSpecialPurhanceCost = 0m;

            Hashtable hsDrugType = new Hashtable();

            ArrayList allSpecialDrug = consMgr.GetAllList("ALLSPECIALDRUG");

            ArrayList allDrugType = new ArrayList();

            string allSpecialDrugs = string.Empty;

            if (allSpecialDrug != null && allSpecialDrug.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject neuObject in allSpecialDrug)
                {
                    allSpecialDrugs = allSpecialDrugs + neuObject.Name + "|";
                }
            }
            foreach (FS.HISFC.Models.Pharmacy.InPlan inPlanInfo in alPrintData)
            {
                alPurhanceCost += inPlanInfo.PlanQty / inPlanInfo.Item.PackQty * inPlanInfo.Item.PriceCollection.PurchasePrice;
                if (allSpecialDrugs.Contains(inPlanInfo.Item.ID))
                {
                    alSpecialPurhanceCost += inPlanInfo.PlanQty / inPlanInfo.Item.PackQty * inPlanInfo.Item.PriceCollection.PurchasePrice;
                    continue;
                }

                FS.HISFC.Models.Pharmacy.Item itemInfo = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inPlanInfo.Item.ID);

                if (hsDrugType.Contains(itemInfo.Type.ID))
                {
                    decimal curPurhanceCost = FS.FrameWork.Function.NConvert.ToDecimal(hsDrugType[itemInfo.Type.ID]);
                    curPurhanceCost += inPlanInfo.PlanQty / inPlanInfo.Item.PackQty * inPlanInfo.Item.PriceCollection.PurchasePrice;
                    hsDrugType[itemInfo.Type.ID] = curPurhanceCost;
                }
                else
                {
                    allDrugType.Add(itemInfo.Type.ID);
                    hsDrugType.Add(itemInfo.Type.ID, inPlanInfo.PlanQty / inPlanInfo.Item.PackQty * inPlanInfo.Item.PriceCollection.PurchasePrice);
                }
            }

            ucPrintBillTot ucPrintBillTot = new ucPrintBillTot(alPurhanceCost, alSpecialPurhanceCost, hsDrugType, pageTotNum, allDrugType, this.printBill.IsNeedPreview);

            this.SetPrintData(alPrintData, 1, 1, this.printBill.Title);
            if (this.printBill.IsNeedPreview)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintView(null);
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintPage(null);
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 3赋值
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            this.lblTitle.Text = "{0}药品入库计划单";
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.InPlan plan = (FS.HISFC.Models.Pharmacy.InPlan)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(plan.Dept.ID), drugType);
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(plan.Dept.ID));
                }
                if (title.IndexOf("[药品类型]") != -1)
                {
                    string tmpDrugType = "(" + this.drugType + ")";
                    title = title.Replace("[药品类型]", tmpDrugType);
                }
                this.lblTitle.Text = title;
            }

            this.lblCheckDate.Text = "计划日期:" + fOperDate.ToShortDateString();
            //this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
            this.lblPage.Text = "页:" + inow.ToString() + "/" + icount.ToString();


            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;

            FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
            n.DecimalPlaces = 2;
            FarPoint.Win.Spread.CellType.NumberCellType n1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            n1.DecimalPlaces = 0;
        
            this.neuFpEnter1_Sheet1.Columns[4].CellType = n1;
            this.neuFpEnter1_Sheet1.Columns[6].CellType = n;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = n1;
            this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
            this.neuFpEnter1_Sheet1.Columns[10].CellType = n1;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.InPlan info = al[i] as FS.HISFC.Models.Pharmacy.InPlan;
                FS.HISFC.Models.Base.Department departMent = dept.GetDeptmentById(info.Dept.ID);
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).UserCode; //药品自定义码


                FS.HISFC.Models.Pharmacy.Item itemObj = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 2].Text = itemObj.NameCollection.RegularName; //药品名称                    
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 2].Text = itemObj.Name; //药品名称                    
                }

                if (info.Item.PackQty == 0)
                    info.Item.PackQty = 1;
                decimal count = 0;
                count = info.InQty;

                FS.HISFC.Models.Pharmacy.Storage storage = this.storageMgr.GetStockInfoByDrugCode(((FS.HISFC.Models.Base.Employee)this.storageMgr.Operator).Dept.ID, itemObj.ID);

                this.neuFpEnter1_Sheet1.Cells[i, 4].Value = (storage.StoreQty / itemObj.PackQty);

                this.neuFpEnter1_Sheet1.Cells[i, 6].Value = info.Item.PriceCollection.PurchasePrice;
                if (departMent.DeptType.ID.ToString() == "PI")
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (info.PlanQty / info.Item.PackQty).ToString("F3").TrimEnd('0').TrimEnd('.');
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = info.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (info.StockQty / info.Item.PackQty).ToString("F3").TrimEnd('0').TrimEnd('.');
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = info.PlanQty;
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = info.Item.MinUnit;
                    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = info.StockQty;
                }

                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = info.Item.Specs;//规格	

                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = (info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.PurchasePrice).ToString("F2");//预购金额
                sumRetail += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                sumPurchase += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.PurchasePrice;
                this.totRetailCost += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                this.totPurchaseCost += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.PurchasePrice;
            }

            //最后一页显示金额
            if (inow == icount)
            {   
                this.lblCurRet.Text = "零售金额:" + this.totRetailCost.ToString("F2");
                this.lblCurPur.Text = "购入金额:" + this.totPurchaseCost.ToString("F2");
                this.lblCurDif.Text = "购零差:" + (this.totRetailCost - this.totPurchaseCost).ToString("F2");
                this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
            }
            else
            {
                this.lblOper.Text = "";//制表人
                this.lblCurRet.Text = "";// "本页零售金额:" + sumRetail.ToString("F4");
                this.lblCurPur.Text = "";// "本页购入金额:" + sumPurchase.ToString("F4");
                this.lblCurDif.Text = "";// "本页购零差:" + sumDif.ToString("F4");
            }
            //总数据

            #endregion

            this.resetTitleLocation();
        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation()
        {
            this.neuPanel4.Controls.Remove(this.lblTitle);
            int with = 0;
            for (int col = 0; col < this.neuFpEnter1_Sheet1.ColumnCount; col++)
            {
                if (this.neuFpEnter1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuFpEnter1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel4.Width)
            {
                with = this.neuPanel4.Width;
            }
            this.lblTitle.Location = new Point((with - this.lblTitle.Size.Width) / 2, this.lblTitle.Location.Y);
            this.neuPanel4.Controls.Add(this.lblTitle);

        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <returns></returns>
        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }


        #region IPharmacyBillPrint 成员

        public int SetPrintData(ArrayList alPrintData, FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill)
        {
           Base.PrintBill.SortByCustomerCode(ref this.alPrintData);
           string billNo = (alPrintData[0] as FS.HISFC.Models.Pharmacy.InPlan).BillNO;
           string deptNo = (alPrintData[0] as FS.HISFC.Models.Pharmacy.InPlan).Dept.ID;
           SOC.HISFC.BizLogic.Pharmacy.Plan plan = new FS.SOC.HISFC.BizLogic.Pharmacy.Plan();
           List<FS.HISFC.Models.Pharmacy.InPlan> al = plan.MergeInPlan(deptNo, billNo);
           for (int i = 0; i < al.Count; i++)
           {
               this.alPrintData.Add(alPrintData[i]);
           }
           this.printBill = printBill;
           return this.Print();
        }

        #endregion
    }
}
