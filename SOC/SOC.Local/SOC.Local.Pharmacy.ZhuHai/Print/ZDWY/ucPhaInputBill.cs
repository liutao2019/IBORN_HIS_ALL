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
    public partial class ucPhaInputBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        private string curStockDept = string.Empty;

        private bool isReprint = false;
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucPhaInputBill()
        {
            InitializeComponent();
            PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
            PrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_EndPrint);
        }

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

     
        #region 变量

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        private Hashtable hsTotCostByPage = new Hashtable();

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal wholeSaleCost;
            public decimal retailCost;
        }


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

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblCompany.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblCompany.Location.Y;
            graphics.DrawString(this.lblCompany.Text, this.lblCompany.Font, new SolidBrush(this.lblCompany.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblBillID.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblBillID.Location.Y;
            graphics.DrawString(this.lblBillID.Text, this.lblBillID.Font, new SolidBrush(this.lblBillID.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblInputDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblInputDate.Location.Y;
            graphics.DrawString(this.lblInputDate.Text, this.lblInputDate.Font, new SolidBrush(this.lblInputDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbMemo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbMemo.Location.Y;
            graphics.DrawString(this.nlbMemo.Text, this.nlbMemo.Font, new SolidBrush(this.nlbMemo.ForeColor), additionTitleLocalX, additionTitleLocalY);
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
            if(curPageNO == maxPageNO)
            {
                if(alPrintData.Count % printBill.RowCount == 0)
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
                FarpointHeight =  (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 5);
            }
            #endregion

            #region 页尾绘制
            additionTitleLocalX = this.DrawingMargins.Left + this.lblOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOper.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.lblOper.Text, this.lblOper.Font, new SolidBrush(this.lblOper.ForeColor), additionTitleLocalX, additionTitleLocalY);

            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.curStockDept).DeptType.ID.ToString() == "PI")
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel10.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel10.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.neuLabel10.Text, this.neuLabel10.Font, new SolidBrush(this.neuLabel10.ForeColor), additionTitleLocalX, additionTitleLocalY);

                additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel9.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel9.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.neuLabel9.Text, this.neuLabel9.Font, new SolidBrush(this.neuLabel9.ForeColor), additionTitleLocalX, additionTitleLocalY);

                additionTitleLocalX = this.DrawingMargins.Left + this.nlbDrugFinOper.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.nlbDrugFinOper.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.nlbDrugFinOper.Text, this.nlbDrugFinOper.Font, new SolidBrush(this.nlbDrugFinOper.ForeColor), additionTitleLocalX, additionTitleLocalY);

            }
            else
            {
                //additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel10.Location.X;
                //additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel10.Location.Y + this.neuPanel4.Height + FarpointHeight;
                //graphics.DrawString("主管院长：", this.neuLabel10.Font, new SolidBrush(this.neuLabel10.ForeColor), additionTitleLocalX, additionTitleLocalY);

                //additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel9.Location.X;
                //additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel9.Location.Y + this.neuPanel4.Height + FarpointHeight;
                //graphics.DrawString("经手人：", this.neuLabel9.Font, new SolidBrush(this.neuLabel9.ForeColor), additionTitleLocalX, additionTitleLocalY);

            }

         
            if (this.curPageNO == this.maxPageNO)
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.lblTotPurCost.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblTotPurCost.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.lblTotPurCost.Text, this.lblTotPurCost.Font, new SolidBrush(this.lblTotPurCost.ForeColor), additionTitleLocalX, additionTitleLocalY);

                additionTitleLocalX = this.DrawingMargins.Left + this.lblTotRetail.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblTotRetail.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.lblTotRetail.Text, this.lblTotRetail.Font, new SolidBrush(this.lblTotRetail.ForeColor), additionTitleLocalX, additionTitleLocalY);

                additionTitleLocalX = this.DrawingMargins.Left + this.lblTotDiff.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblTotDiff.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.lblTotDiff.Text, this.lblTotDiff.Font, new SolidBrush(this.lblTotDiff.ForeColor), additionTitleLocalX, additionTitleLocalY);
            }

            additionTitleLocalX = this.DrawingMargins.Left + this.lblCurPur.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblCurPur.Location.Y + this.neuPanel4.Height + FarpointHeight;

            TotCost totCostByPage = (TotCost)hsTotCostByPage[curPageNO];
             
            graphics.DrawString("本页合计：购入金额：" + totCostByPage.purchaseCost.ToString(Function.GetCostDecimalString()), this.lblCurPur.Font, new SolidBrush(this.lblCurPur.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblCurRet.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblCurRet.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString("零售金额：" + totCostByPage.wholeSaleCost.ToString(Function.GetCostDecimalString()), this.lblCurRet.Font, new SolidBrush(this.lblCurRet.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblCurDif.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblCurDif.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString("购零差：" + (totCostByPage.wholeSaleCost - totCostByPage.purchaseCost).ToString(Function.GetCostDecimalString()), this.lblCurDif.Font, new SolidBrush(this.lblCurDif.ForeColor), additionTitleLocalX, additionTitleLocalY);
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
            hsTotCostByPage = new Hashtable();

            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.Input info = (FS.HISFC.Models.Pharmacy.Input)al[0];

            #region label赋值
            //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
            FS.HISFC.Models.Base.Department deptInfo = FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(info.StockDept.ID);

            if (string.IsNullOrEmpty(title))
            {
                //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
                this.lblTitle.Text = string.Format(deptInfo.HospitalName + this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
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

                //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
                if (title.IndexOf("[院区]") != -1)
                {
                    this.lblTitle.Text = this.lblTitle.Text.Replace("[院区]", deptInfo.HospitalName);
                }
            }

            if (info.Quantity < 0)
            {
                this.lblTitle.Text = this.lblTitle.Text.Replace("入库", "内退");
            }

            curStockDept = info.StockDept.ID;

            string company = "";
            if (info.SourceCompanyType == "1")
            {
                company = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.Company.ID);
            }
            else
            {
                company = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            }
            this.lblCompany.Text = "送货单位:" + company;

            this.lblBillID.Text = "入库单:" + info.InListNO;
            this.lblInputDate.Text = "入库日期:" + info.InDate.Year + "." + info.InDate.Month.ToString().PadLeft(2,'0') + "." + info.InDate.Day.ToString().PadLeft(2,'0');
            this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID) + " " + DateTime.Now.ToString();
            this.lblPage.Text = "页:" + inow.ToString() + "/" + icount.ToString();

            #region 新增药品会计显示
            this.nlbDrugFinOper.Visible = (SOC.HISFC.BizProcess.Cache.Common.GetDept(info.StockDept.ID).DeptType.ID.ToString() == "PI");
          
            #endregion

            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumWholeSaleCost = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;
            //中西草
            decimal sumPCZret = 0;
            decimal sumPCZpur = 0;
            //卫生材料
            decimal sumBM = 0;
            //原材料
            decimal sumM = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            int pageNo = 1;
            string memo = string.Empty;
            for (int i = 0; i < al.Count; i++)
            {
                if((i != 0) &&(i% printBill.RowCount == 0))
                {
                    pageNo++;
                    sumRetail = 0;
                    sumWholeSaleCost = 0;
                    sumPurchase = 0;
                    sumDif = 0;
                }
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                //this.neuFpEnter1_Sheet1.Rows[i].Height = 26F;
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.Item.Specs;//规格		
                if (input.Item.PackQty == 0)
                    input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;

                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = Function.GetCostDecimal();
                this.neuFpEnter1_Sheet1.Columns[7].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = nCost;

                if (input.Quantity%input.Item.PackQty != 0)
                {

                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count).ToString();//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = (input.Item.PriceCollection.PurchasePrice / input.Item.PackQty).ToString("F4");
                    //this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (input.Item.PriceCollection.RetailPrice / input.Item.PackQty).ToString("F4");
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (input.Item.PriceCollection.WholeSalePrice / input.Item.PackQty).ToString("F4");

                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count / input.Item.PackQty).ToString("F2");//数量			


                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = input.Item.PriceCollection.PurchasePrice;
                    //this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.Item.PriceCollection.RetailPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.Item.PriceCollection.WholeSalePrice;
                }

                try
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).Product.ProducingArea;
                }
                catch { }

                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.PurchaseCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.WholeSaleCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (input.WholeSaleCost - input.PurchaseCost).ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[i, 11].Value = input.BatchNO;

                if (!string.IsNullOrEmpty(input.Memo))
                {
                    memo = input.Memo;
                }

                if(hsTotCostByPage.Contains(pageNo))
                {
                     TotCost totCostByPage  = (TotCost)hsTotCostByPage[pageNo];
                     totCostByPage.purchaseCost += input.PurchaseCost;
                     totCostByPage.wholeSaleCost += input.WholeSaleCost;
                     totCostByPage.retailCost += input.RetailCost;
                }
                else
                {
                    TotCost totCostByPage = new TotCost();
                    totCostByPage.retailCost += input.RetailCost;
                    totCostByPage.wholeSaleCost += input.WholeSaleCost;
                    totCostByPage.purchaseCost += input.PurchaseCost;
                    hsTotCostByPage.Add(pageNo, totCostByPage);
                }

                sumRetail = sumRetail + input.RetailCost;
                sumWholeSaleCost = sumWholeSaleCost + input.WholeSaleCost;
                sumPurchase = sumPurchase + input.PurchaseCost;
                sumDif = sumWholeSaleCost - sumPurchase;

            }
            //当前页数据
            this.lblCurRet.Text = "零售金额：" + sumWholeSaleCost.ToString(Function.GetCostDecimalString());
            this.lblCurPur.Text = "本页合计：购入金额：" + sumPurchase.ToString(Function.GetCostDecimalString());
            this.lblCurDif.Text = "购零差：" + sumDif.ToString(Function.GetCostDecimalString());

           
            this.nlbMemo.Text = memo;
            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() != "PI" && isReprint)
            {
                this.lblTitle.Text = "(补打)" + this.lblTitle.Text; ;
            }

            //总数据
            this.lblTotPurCost.Text = "总合计：买入金额：" + ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
            this.lblTotRetail.Text = "零售金额：" + ((TotCost)hsTotCost[info.InListNO]).wholeSaleCost.ToString(Function.GetCostDecimalString());
            this.lblTotDiff.Text = "进销差：" + (((TotCost)hsTotCost[info.InListNO]).wholeSaleCost -
                ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString(Function.GetCostDecimalString());
                

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

            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.IsDataAutoExtend = false;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            //p.IsHaveGrid = true;
            //p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            //int height = this.neuPanel5.Height;
            //int ucHeight = this.Height;
            //float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            //this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.货位号)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.物理顺序)
            {
                Base.PrintBill.SortByBillNO(ref alPrintData);
            }

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Input input in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Input i = input.Clone();
                if (hs.Contains(i.InListNO))
                {

                    ArrayList al = (ArrayList)hs[i.InListNO];
                    al.Add(i);

                    TotCost tc = (TotCost)hsTotCost[i.InListNO];
                    tc.purchaseCost += i.PurchaseCost;
                    tc.wholeSaleCost += i.WholeSaleCost;
                    tc.retailCost += i.RetailCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.InListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = i.PurchaseCost;
                    tc.wholeSaleCost = i.WholeSaleCost;
                    tc.retailCost = i.RetailCost;
                    hsTotCost.Add(input.InListNO, tc);
                }
            }

            //分单据打印
            foreach (ArrayList alPrint in hs.Values)
            {
                this.SetPrintData(alPrint, 1, 1, printBill.Title);
                alPrintData = alPrint.Clone() as ArrayList;
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
                //int pageTotNum = alPrint.Count / this.printBill.RowCount;
                //if (alPrint.Count != this.printBill.RowCount * pageTotNum)
                //{
                //    pageTotNum++;
                //}

                ////分页打印
                //for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                //{
                //    ArrayList al = new ArrayList();

                //    for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                //    {
                //        al.Add(alPrint[index]);
                //    }

                //    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                //    this.neuPanel5.Height += (int)rowHeight * al.Count;
                //    this.Height += (int)rowHeight * al.Count;

                //    if (this.printBill.IsNeedPreview)
                //    {
                //        p.PrintPreview(5, 0, this.neuPanel1);
                //    }
                //    else
                //    {
                //        p.PrintPage(5, 0, this.neuPanel1);
                //    }

                //    this.neuPanel5.Height = height;
                //    this.Height = ucHeight;
                //}
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            isReprint = false;
            if (alPrintData != null && alPrintData.Count > 0)
            {
                foreach (FS.HISFC.Models.Pharmacy.Input info in alPrintData)
                {
                    if (!string.IsNullOrEmpty(info.SpecialFlag) && info.SpecialFlag.Contains("补打"))
                    {
                        isReprint = true;
                    }
                }
                string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).StockDept.ID;
                FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
                this.alPrintData = al;
            }
            //this.alPrintData = alPrintData;
            this.printBill = printBill;
            return this.Print();
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion

        private void lblOper_Click(object sender, EventArgs e)
        {

        }


    }
}
