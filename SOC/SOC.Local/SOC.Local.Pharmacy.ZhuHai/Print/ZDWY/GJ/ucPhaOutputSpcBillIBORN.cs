using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.GJ
{
    /// <summary>
    /// 东莞药库报废单据打印
    /// </summary>
    public partial class ucPhaOutputSpcBillBORN : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药库报废打印单
        /// </summary>
        public ucPhaOutputSpcBillBORN()
        {
            InitializeComponent();
            //C6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
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
        private bool isReprint = false;
        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();
        //C6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
        System.Drawing.Printing.PrintPageEventArgs printPageEventArgs = null;

        FS.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        //C6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        private Hashtable hsTotCostByPage = new Hashtable();

        /// <summary>
        /// 是否售价
        /// </summary>
        private bool isRetailPrice = false;

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

        #region 入库单打印
        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">制单人</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            FS.HISFC.Models.Pharmacy.Output info = (FS.HISFC.Models.Pharmacy.Output)al[0];

            #region label赋值


            //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
            FS.HISFC.Models.Base.Department deptInfo = FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(info.StockDept.ID);

            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {

                FS.HISFC.Models.Admin.PowerLevelClass3 privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey("0320", info.PrivType);

                title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
                title = title.Replace("[院区]", deptInfo.HospitalName);
                if (privClass3 != null)
                {
                    title = title.Replace("[操作类型]", privClass3.Class3Name);
                }

                this.lblTitle.Text = title;
            }
            //{FEFCF857-9BB1-4bdf-B1E7-EF27C5080030}
            this.lblCompany.Text = "申请科室: " + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "出库单号: " + info.OutListNO;
            this.lblInputDate.Text = "出库日期: " + info.OutDate.ToString();
            this.lblPrintTime.Text = "制单日期: " + DateTime.Now;
            this.lblOper.Text = "制单人:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "页：" + inow.ToString() + "/" + icount.ToString();
            //this.lblApplicant.Text = "申请人：";// +BillPrintFun.GetStockManagerName(info.StockDept.ID); //BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);
            this.lblApplicant.Visible = true;
            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumWholeSaleCost = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;

            decimal sumDrugWholeCost = 0;
            decimal sumDrugPurCost = 0;

            decimal sumBWholeCost = 0;
            decimal sumBPurCost = 0;

            int pageNo = 1;

            this.neuFpEnter1_Sheet1.RowCount = 0;
          
            for (int i = 0; i < al.Count; i++)
            {
                if ((i != 0) && (i % printBill.RowCount == 0))
                {
                    pageNo++;
                }

                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                this.lblApplicant.Text = "申请人：" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(output.GetPerson);
                
                //this.neuFpEnter1_Sheet1.Cells[i, 0].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = (i + 1).ToString();

                FS.HISFC.Models.Pharmacy.Item itemObj = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text = itemObj.NameCollection.RegularName; //药品名称                    
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text = itemObj.Name; //药品名称                    
                }
                
                //this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.Item.Specs;//规格		
                //this.neuFpEnter1_Sheet1.Cells[i, 3].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(output.Producer.ID);//生产厂家

                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.BatchNO;//批号
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.ValidTime.ToShortDateString();//有效期
                if (output.Item.PackQty == 0)
                    output.Item.PackQty = 1;
                decimal count = 0;
                count = output.Quantity;
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                FarPoint.Win.Spread.CellType.NumberCellType n2 = new FarPoint.Win.Spread.CellType.NumberCellType();
                n2.DecimalPlaces = 2;

                //FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                //nCost.DecimalPlaces = Function.GetCostDecimal();
                //this.neuFpEnter1_Sheet1.Columns[5].CellType = nCost;

                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n2;

                if (SOC.HISFC.BizProcess.Cache.Common.GetDept(output.StockDept.ID).DeptType.ID.ToString() == "P")
                {
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "购入价(元)";
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "购入金额(元)";
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (count).ToString();//数量
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = output.Item.MinUnit;//单位				
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.PriceCollection.RetailPrice / output.Item.PackQty).ToString("F4");
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.RetailCost.ToString("F2");
                }
                else
                {
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "购入价(元)";
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "购入金额(元)";
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (count / output.Item.PackQty).ToString("F2");//数量	
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = output.Item.PackUnit;//单位		
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Text = output.PriceCollection.PurchasePrice.ToString("F4");
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.PurchaseCost.ToString("F2");


                }
           
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.Memo;



                if (hsTotCostByPage.Contains(pageNo))
                {
                    TotCost totCostByPage = (TotCost)hsTotCostByPage[pageNo];
                    totCostByPage.purchaseCost += output.PurchaseCost;
                    totCostByPage.wholeSaleCost += output.WholeSaleCost;
                    totCostByPage.retailCost += output.RetailCost;
                }
                else
                {
                    TotCost totCostByPage = new TotCost();
                    totCostByPage.retailCost += output.RetailCost;
                    totCostByPage.wholeSaleCost += output.WholeSaleCost;
                    totCostByPage.purchaseCost += output.PurchaseCost;
                    hsTotCostByPage.Add(pageNo, totCostByPage);
                }

                sumRetail += output.RetailCost;
                sumWholeSaleCost += output.WholeSaleCost;
                sumPurchase += output.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }
            //当前页数据
            //this.lblCurRet.Text = "买入金额合计：" + sumRetail.ToString("F4");
            //this.lblCurPur.Text = "零售金额合计：" + sumPurchase.ToString("F4");
            this.lblCurRet.Text = "";//"零售金额合计：" + sumRetail.ToString(Function.GetCostDecimalString());
            this.lblCurPur.Text = "本页合计：购入金额：" + sumPurchase.ToString("F2");
            this.lblCurDif.Text = "";//"购零差合计：" + sumDif.ToString(Function.GetCostDecimalString());

            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() != "PI" && isReprint)
            {
                this.lblTitle.Text = "(补打)" + this.lblTitle.Text; ;
            }

            #endregion

            this.lblTotPurCost.Text = "总合计：购入总金额：" + ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString("F2");

            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() == "PI")
            {
                this.neuLabel9.Visible = true;
                this.lblApplicant.Visible = true;
                //this.neuLabel2.Visible = true;
                //this.neuLabel3.Visible = true;
            }
            else
            {
                this.neuLabel9.Visible = true;
                this.lblApplicant.Visible = false;
                //this.neuLabel2.Visible = false;
                //this.neuLabel3.Visible = false;
            }
            this.resetTitleLocation();

        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation()
        {
            this.neuPanel3.Controls.Remove(this.lblTitle);
            int with = 0;
            for (int col = 0; col < this.neuFpEnter1_Sheet1.ColumnCount; col++)
            {
                if (this.neuFpEnter1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuFpEnter1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel3.Width)
            {
                with = this.neuPanel3.Width;
            }
            this.lblTitle.Location = new Point((with - this.lblTitle.Size.Width) / 2, this.lblTitle.Location.Y);
            this.neuPanel3.Controls.Add(this.lblTitle);

        }

        #endregion

        #region 打印相关
        //{BAC6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
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

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(10, 10, 5, 15);

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();


        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            printPageEventArgs = e;
            //跳过选择打印范围外的数据
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                this.curPageNO = 1;
                //this.maxPageNO = 1;
                e.HasMorePages = true;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 标题绘制
            int mainTitleLocalX = this.DrawingMargins.Left + this.lblTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.lblTitle.Location.Y;
            graphics.DrawString(this.lblTitle.Text, this.lblTitle.Font, new SolidBrush(this.lblTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            //int additionTitleLocalX = this.DrawingMargins.Left + this.lblCompany.Location.X;
            //int additionTitleLocalY = this.DrawingMargins.Top + this.lblCompany.Location.Y;
            //graphics.DrawString(this.lblCompany.Text, this.lblCompany.Font, new SolidBrush(this.lblCompany.ForeColor), additionTitleLocalX, additionTitleLocalY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblBillID.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblBillID.Location.Y;
            graphics.DrawString(this.lblBillID.Text, this.lblBillID.Font, new SolidBrush(this.lblBillID.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblInputDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblInputDate.Location.Y;
            graphics.DrawString(this.lblInputDate.Text, this.lblInputDate.Font, new SolidBrush(this.lblInputDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            additionTitleLocalX = this.DrawingMargins.Left + this.lblPrintTime.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPrintTime.Location.Y;
            graphics.DrawString(this.lblPrintTime.Text, this.lblPrintTime.Font, new SolidBrush(this.lblPrintTime.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblCompany.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblCompany.Location.Y;
            graphics.DrawString(this.lblCompany.Text, this.lblCompany.Font, new SolidBrush(this.lblCompany.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region 页码绘制

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPage.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPage.Location.Y;
            graphics.DrawString("页码：" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.lblPage.Font, new SolidBrush(this.lblPage.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            //int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel3.Height - neuPanel2.Height; 
            int drawingHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount);
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel3.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
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
            additionTitleLocalX = this.DrawingMargins.Left + this.lblOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOper.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.lblOper.Text, this.lblOper.Font, new SolidBrush(this.lblOper.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblApplicant.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblApplicant.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.lblApplicant.Text, this.lblApplicant.Font, new SolidBrush(this.lblApplicant.ForeColor), additionTitleLocalX, additionTitleLocalY);

            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel9.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel9.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.neuLabel9.Text, this.neuLabel9.Font, new SolidBrush(this.neuLabel9.ForeColor), additionTitleLocalX, additionTitleLocalY);


            if (this.curPageNO == this.maxPageNO)
            {
                if (this.isRetailPrice)
                {
                    //additionTitleLocalX = this.DrawingMargins.Left + this.lblTotRetailCost.Location.X;
                    //additionTitleLocalY = this.DrawingMargins.Top + this.lblTotRetailCost.Location.Y + this.neuPanel3.Height + FarpointHeight;
                    //graphics.DrawString(this.lblTotRetailCost.Text, this.lblTotRetailCost.Font, new SolidBrush(this.lblTotRetailCost.ForeColor), additionTitleLocalX, additionTitleLocalY);
                }
                else
                {
                    additionTitleLocalX = this.DrawingMargins.Left + this.lblTotPurCost.Location.X;
                    additionTitleLocalY = this.DrawingMargins.Top + this.lblTotPurCost.Location.Y + this.neuPanel3.Height + FarpointHeight;
                    graphics.DrawString(this.lblTotPurCost.Text, this.lblTotPurCost.Font, new SolidBrush(this.lblTotPurCost.ForeColor), additionTitleLocalX, additionTitleLocalY);
                }

                //additionTitleLocalX = this.DrawingMargins.Left + this.lblTotDif.Location.X;
                //additionTitleLocalY = this.DrawingMargins.Top + this.lblTotDif.Location.Y + this.neuPanel3.Height + FarpointHeight;
                //graphics.DrawString(this.lblTotDif.Text, this.lblTotDif.Font, new SolidBrush(this.lblTotDif.ForeColor), additionTitleLocalX, additionTitleLocalY);
            }
            if (this.isRetailPrice)
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurRet.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurRet.Location.Y + this.neuPanel3.Height + FarpointHeight;
                TotCost totCostByPage = (TotCost)hsTotCostByPage[curPageNO];
                graphics.DrawString("本页合计：售价金额：" + totCostByPage.retailCost.ToString("F2"), this.lblCurPur.Font, new SolidBrush(this.lblCurPur.ForeColor), additionTitleLocalX, additionTitleLocalY);

            }
            else
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurPur.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurPur.Location.Y + this.neuPanel3.Height + FarpointHeight;
                TotCost totCostByPage = (TotCost)hsTotCostByPage[curPageNO];
                graphics.DrawString("本页合计：购入金额：" + totCostByPage.purchaseCost.ToString("F2"), this.lblCurPur.Font, new SolidBrush(this.lblCurPur.ForeColor), additionTitleLocalX, additionTitleLocalY);
            }

            //additionTitleLocalX = this.DrawingMargins.Left + this.lblCurDif.Location.X;
            //additionTitleLocalY = this.DrawingMargins.Top + this.lblCurDif.Location.Y + this.neuPanel3.Height + FarpointHeight;
            //graphics.DrawString("购零差：" + (totCostByPage.retailCost - totCostByPage.purchaseCost).ToString(Function.GetCostDecimalString()), this.lblCurDif.Font, new SolidBrush(this.lblCurDif.ForeColor), additionTitleLocalX, additionTitleLocalY);
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
            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            //int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel3.Height - this.neuPanel2.Height;
            int drawingHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount);

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = true;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuFpEnter1_Sheet1.RowHeader.Visible;
            this.neuFpEnter1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuFpEnter1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel3.Height, drawingWidth, drawingHeight), 0);

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

        #endregion 

        #region IPharmacyBill 成员

        private Base.PrintBill printBill = new Base.PrintBill();

        /// <summary>
        /// IBillPrint成员Print
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            //{BAC6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
            #region 打印信息设置
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.IsDataAutoExtend = false;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            //p.IsHaveGrid = true;
            //p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            int height = this.neuPanel3.Height;//{BAC6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.货位号)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.物理顺序)
            {
                Base.PrintBill.SortByBillNO(ref alPrintData);
            }

            decimal sumWholeSaleCost = 0.0m;
            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Output output in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Output o = output.Clone();
                if (hs.Contains(o.OutListNO))
                {

                    ArrayList al = (ArrayList)hs[o.OutListNO];
                    al.Add(o);
                    //{BAC6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
                    TotCost tc = (TotCost)hsTotCost[o.OutListNO];
                    tc.purchaseCost += o.PurchaseCost;
                    tc.wholeSaleCost += o.WholeSaleCost;
                    tc.retailCost += o.RetailCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.OutListNO, al);
                    //{BAC6B62B-0B69-41a9-A2F0-F3AD4E4EB318}
                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.PurchaseCost;
                    tc.wholeSaleCost = o.WholeSaleCost;
                    tc.retailCost = o.RetailCost;
                    hsTotCost.Add(o.OutListNO, tc);
                }

                sumWholeSaleCost += output.PurchaseCost;

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

                // int fromPage = 0;
                //int toPage = 0;
                //int copyCount = 1;
                //frmSelectPages frmSelect = new frmSelectPages();
                //frmSelect.PageCount = pageTotNum;
                //frmSelect.SetPages();
                //DialogResult dRsult = frmSelect.ShowDialog();
                //if (dRsult == DialogResult.OK)
                //{
                //    fromPage = frmSelect.FromPage - 1;
                //    toPage = frmSelect.ToPage;
                //    copyCount = frmSelect.CopyCount;
                //}
                //else
                //{
                //    return 0;
                //}

                //for (int i = 0; i < copyCount; i++)
                //{

                //    //分页打印
                //    for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                //    {
                //        ArrayList al = new ArrayList();

                //        for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                //        {
                //            al.Add(alPrint[index]);
                //        }

                //        this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                //        this.neuFpEnter1.Height += (int)rowHeight * al.Count;
                //        this.Height += (int)rowHeight * al.Count;

                //        if (pageNow == pageTotNum - 1)
                //        {
                //            this.lblTotPurCost.Visible = true;
                //            this.lblTotPurCost.Text = "总合计：购入总金额：" + sumWholeSaleCost.ToString("F2");
                //        }
                //        else
                //        {
                //            this.lblTotPurCost.Visible = false;
                //        }

                //        if (this.printBill.IsNeedPreview)
                //        {
                //            p.PrintPreview(5, 0, this.neuPanel1);
                //        }
                //        else
                //        {
                //            p.PrintPage(5, 0, this.neuPanel1);
                //        }

                //        this.neuFpEnter1.Height = height;
                //        this.Height = ucHeight;
                //    }
                //}
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            isReprint = false;
            if (this.alPrintData == null || alPrintData.Count == 0)
            {
                return 1;
            }
            foreach (FS.HISFC.Models.Pharmacy.Output info in alPrintData)
            {
                if (!string.IsNullOrEmpty(info.SpecialFlag) && info.SpecialFlag.Contains("补打"))
                {
                    isReprint = true;

                }
            }
            FS.HISFC.Models.Pharmacy.Output output = alPrintData[0] as FS.HISFC.Models.Pharmacy.Output;
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            ArrayList al = itemMgr.QueryOutputInfo(output.StockDept.ID, output.OutListNO, output.State);
            if (al == null || al.Count == 0)
            {
                return 1;
            }
            this.alPrintData = al;
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
