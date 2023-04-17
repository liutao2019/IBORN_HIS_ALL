using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    /// <summary>
    /// 东莞药品入库单据打印
    /// </summary>
    public partial class ucPhaAdjustBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        private string curStockDept = string.Empty;
        private string curAdjustReason = string.Empty;
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucPhaAdjustBill()
        {
            InitializeComponent();
            PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
            PrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_EndPrint);
        }

        void PrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (this.PrintDocument.PrintController.IsPreview == false)
            //{
            //    printPreviewDialog.Close();
            //    printPreviewDialog.Dispose();
            //}
            
        }

        void PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            curPageNO = 1;
        }

     
        #region 变量

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// 调价管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Adjust ajustMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Adjust();
        #endregion

        #region 打印相关
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

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(0, 0, 20, 40);

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

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblBillID.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblBillID.Location.Y;
            graphics.DrawString(this.lblBillID.Text, this.lblBillID.Font, new SolidBrush(this.lblBillID.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblAdjustDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblAdjustDate.Location.Y;
            graphics.DrawString(this.lblAdjustDate.Text, this.lblAdjustDate.Font, new SolidBrush(this.lblAdjustDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblAdjustResson.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblAdjustResson.Location.Y;
            graphics.DrawString(this.lblAdjustResson.Text, this.lblAdjustResson.Font, new SolidBrush(this.lblAdjustResson.ForeColor), additionTitleLocalX, additionTitleLocalY);
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
          
            FarpointHeight =  (int)(this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 30);
           
            #endregion

            #region 页尾绘制
            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel10.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel10.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.neuLabel10.Text, this.neuLabel10.Font, new SolidBrush(this.neuLabel10.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.label1.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.label1.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.label1.Text, this.label1.Font, new SolidBrush(this.label1.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel9.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel9.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.neuLabel9.Text, this.neuLabel9.Font, new SolidBrush(this.neuLabel9.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.label2.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.label2.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.label2.Text, this.label2.Font, new SolidBrush(this.label2.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOper.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.lblOper.Text, this.lblOper.Font, new SolidBrush(this.lblOper.ForeColor), additionTitleLocalX, additionTitleLocalY);


            additionTitleLocalX = this.DrawingMargins.Left + this.nlbDrugFinOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbDrugFinOper.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.nlbDrugFinOper.Text, this.nlbDrugFinOper.Font, new SolidBrush(this.nlbDrugFinOper.ForeColor), additionTitleLocalX, additionTitleLocalY);

         

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
                //socPrintPageSelectDialog.ShowDialog();
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

        #region 入库单打印
        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {

            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.AdjustPrice info = (FS.HISFC.Models.Pharmacy.AdjustPrice)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {


                    this.lblTitle.Text = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));

                }
                else
                {
                    this.lblTitle.Text = title;
                }
            }

            if (info.StockDept.ID == this.curStockDept)
            {
                this.lblBillID.Text = "调价单号：" + info.ID;
                this.lblAdjustResson.Text = "调价原因：" + this.curAdjustReason;
            }
            else
            {
                this.lblBillID.Text = string.Empty;
                this.lblAdjustResson.Text = string.Empty;
            }

            this.lblAdjustDate.Text = "调价日期：" + info.InureTime.Year + "." + info.InureTime.Month.ToString().PadLeft(2, '0') + "." + info.InureTime.Day.ToString().PadLeft(2, '0');
            this.lblOper.Text = "制单人：" + info.Operation.Oper.Name;
            this.lblPage.Text = "页：" + inow.ToString() + "/" + icount.ToString();

            #endregion

            #region farpoint赋值
            decimal sumPreTotCost = 0;
            decimal sumAfterTotCost = 0;
            this.neuFpEnter1_Sheet1.RowCount = 0;
            string memo = string.Empty;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = Function.GetCostDecimal();
                this.neuFpEnter1_Sheet1.Columns[3].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[6].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[11].CellType = nCost;

                FS.HISFC.Models.Pharmacy.AdjustPrice ajustInfo = al[i] as FS.HISFC.Models.Pharmacy.AdjustPrice;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ajustInfo.Item.ID).UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = ajustInfo.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = ajustInfo.Item.Specs;//规格		
                //if (ajustInfo.StockDept.ID == this.curStockDept)
                //{
                decimal stockQty = FS.FrameWork.Function.NConvert.ToDecimal(ajustInfo.StoreQty);
                this.neuFpEnter1_Sheet1.Cells[i, 3].Value = (stockQty / ajustInfo.Item.PackQty);
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = ajustInfo.Item.PackUnit;
                this.neuFpEnter1_Sheet1.Cells[i, 6].Value = ajustInfo.Item.PriceCollection.WholeSalePrice;
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = ajustInfo.Item.PriceCollection.RetailPrice;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = ajustInfo.Item.PriceCollection.WholeSalePrice * (stockQty / ajustInfo.Item.PackQty);
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = ajustInfo.AfterRetailPrice;
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = ajustInfo.AfterRetailPrice * (stockQty / ajustInfo.Item.PackQty);
                this.neuFpEnter1_Sheet1.Cells[i, 11].Value = (ajustInfo.AfterRetailPrice - ajustInfo.Item.PriceCollection.WholeSalePrice) * (stockQty / ajustInfo.Item.PackQty);


                sumPreTotCost += ajustInfo.Item.PriceCollection.WholeSalePrice * (stockQty / ajustInfo.Item.PackQty);
                sumAfterTotCost += ajustInfo.AfterRetailPrice * (stockQty / ajustInfo.Item.PackQty);
            }
            this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.Rows.Count, 1);
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 0].Text = "合计";
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 8].Value = sumPreTotCost;
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 10].Value = sumAfterTotCost;
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 11].Value = sumAfterTotCost - sumPreTotCost;
            

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
        #endregion

        #region IPharmacyBill 成员

        private Base.PrintBill printBill = new Base.PrintBill();

        /// <summary>
        /// IBillPrint成员Print
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region 打印信息设置

            #endregion

            #region 分页打印


            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            ArrayList allDept = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.AdjustPrice info in this.alPrintData)
            {
                FS.HISFC.Models.Pharmacy.AdjustPrice i = info.Clone() as FS.HISFC.Models.Pharmacy.AdjustPrice;
                if (hs.Contains(i.StockDept.ID))
                {

                    ArrayList al = (ArrayList)hs[i.StockDept.ID];
                    al.Add(i);
                }
                else
                {
                    if (i.StockDept.ID != this.curStockDept)
                    {
                        allDept.Add(i.StockDept.ID);
                    }
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.StockDept.ID, al);
                }
            }

            allDept.Insert(0, this.curStockDept);

            //分单据打印
            foreach (string deptId in allDept)
            {
                if (!hs.Contains(deptId))
                {
                    continue;
                }
                this.neuFpEnter1_Sheet1.Rows.Count = 0;

                this.SetPrintData(hs[deptId] as ArrayList, 1, 1, printBill.Title);

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
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (alPrintData != null && alPrintData.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.AdjustPrice adjustPriceInfo = alPrintData[0] as FS.HISFC.Models.Pharmacy.AdjustPrice;
                this.curStockDept = adjustPriceInfo.StockDept.ID;
                this.curAdjustReason = adjustPriceInfo.FileNO;
                this.alPrintData = new ArrayList();
                ArrayList allAdjustData = ajustMgr.QueryAdjustPriceInfoDetailList(adjustPriceInfo.ID);
                foreach (FS.HISFC.Models.Pharmacy.AdjustPrice info in allAdjustData)
                {
                    this.alPrintData.Add(info);
                }
            }
            this.printBill = printBill;
         
            return this.Print();
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion

    }
}
