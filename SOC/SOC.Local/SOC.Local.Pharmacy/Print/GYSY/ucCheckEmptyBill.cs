using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.Print.GYSY
{
    public partial class ucCheckEmptyBill : UserControl, Base.IPharmacyBillPrint
    {
        public ucCheckEmptyBill()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.neuFpEnter1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuFpEnter1_ColumnWidthChanged);
        }

        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        void neuFpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuFpEnter1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaCheckEmptyBillSetting.xml");
        }

        #region 打印处理 by cube 2011-06-18

        ~ucCheckEmptyBill()
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

        private DateTime printTime = new DateTime();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

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
                this.maxPageNO = 1;
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 标题绘制
            int mainTitleLocalX = this.DrawingMargins.Left + this.lblTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.lblTitle.Location.Y;
            graphics.DrawString(this.lblTitle.Text, this.lblTitle.Font, new SolidBrush(this.lblTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblFOperTime.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblFOperTime.Location.Y;

            graphics.DrawString(this.lblFOperTime.Text, this.lblFOperTime.Font, new SolidBrush(this.lblFOperTime.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblBillNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblBillNO.Location.Y;

            graphics.DrawString(this.lblBillNO.Text, this.lblBillNO.Font, new SolidBrush(this.lblBillNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.panelTitle.Height;
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.panelTitle.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

            #endregion

            #region 页码绘制

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPageNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPageNo.Location.Y;

            graphics.DrawString("页码：" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.nlbPageNo.Font, new SolidBrush(this.nlbPageNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

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
                maxPageNO = 1;
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
            int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.panelTitle.Height;
            
            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = true;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuFpEnter1_Sheet1.RowHeader.Visible;
            this.neuFpEnter1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuFpEnter1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.panelTitle.Height, drawingWidth, drawingHeight), 0);

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

        #region 打印主函数
       

        private Base.PrintBill printBill = new Base.PrintBill();

        ArrayList alPrintData = new ArrayList();

        #region 打印
        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">标题</param>
        private void SetPrintData(ArrayList al, string title)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            FS.HISFC.Models.Pharmacy.Check info = (FS.HISFC.Models.Pharmacy.Check)al[0];

            #region label赋值

            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    title = title.Replace("[库存科室]", SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }

                this.lblTitle.Text = title;
            }

            this.lblBillNO.Text = "单号: " + info.CheckNO;

            FS.SOC.HISFC.BizLogic.Pharmacy.Check itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
            FS.HISFC.Models.Pharmacy.Check checkStat = itemMgr.GetCheckStat(info.StockDept.ID, info.CheckNO);
            this.lblFOperTime.Text = "封账时间:" + checkStat.FOper.OperTime.ToString();


            #endregion

            #region farpoint赋值

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Check checkDetail = al[i] as FS.HISFC.Models.Pharmacy.Check;
                this.neuFpEnter1.SetCellValue(0, i, "货位号", checkDetail.PlaceNO);
                this.neuFpEnter1.SetCellValue(0, i, "编码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(checkDetail.Item.ID));

                FS.HISFC.Models.Pharmacy.Item itemObj = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(checkDetail.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.neuFpEnter1.SetCellValue(0, i, "名称", itemObj.NameCollection.RegularName); //药品自定义码                    
                }
                else
                {
                    this.neuFpEnter1.SetCellValue(0, i, "名称", itemObj.Name); //药品自定义码                    
                }
                
                //this.neuFpEnter1.SetCellValue(0, i, "名称", checkDetail.Item.Name);
                this.neuFpEnter1.SetCellValue(0, i, "规格", checkDetail.Item.Specs);
                this.neuFpEnter1.SetCellValue(0, i, "盘点数量1", checkDetail.PackQty);
                this.neuFpEnter1.SetCellValue(0, i, "包装单位", checkDetail.Item.PackUnit);
                this.neuFpEnter1.SetCellValue(0, i, "盘点数量2", checkDetail.MinQty);
                this.neuFpEnter1.SetCellValue(0, i, "最小单位", checkDetail.Item.MinUnit);
                this.neuFpEnter1.SetCellValue(0, i, "批号", checkDetail.BatchNO);
                this.neuFpEnter1.SetCellValue(0, i, "购入价", checkDetail.Item.PriceCollection.PurchasePrice);
                this.neuFpEnter1.SetCellValue(0, i, "零售价", checkDetail.Item.PriceCollection.RetailPrice);
                this.neuFpEnter1.SetCellValue(0, i, "摘要", " ");

                string strQty = "";
                int qtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                decimal qtyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                qtyInt = (int)(checkDetail.FStoreQty / checkDetail.Item.PackQty);
                qtyRe = checkDetail.FStoreQty - qtyInt * checkDetail.Item.PackQty;
                if (qtyInt > 0)
                {
                    strQty += qtyInt.ToString() + checkDetail.Item.PackUnit;
                }
                if (qtyRe > 0)
                {
                    strQty += qtyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + checkDetail.Item.MinUnit;
                }

                this.neuFpEnter1.SetCellValue(0, i, "封账数量", strQty);
             
            }

            //总数据

            #endregion

        }


        #endregion

        #endregion

        /// <summary>
        /// IBillPrint成员Print
        /// </summary>
        /// <returns></returns>
        public int Print()
        {

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.货位号)
            {
                Base.PrintBill.SortByCustomerCode(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.物理顺序)
            {
                Base.PrintBill.SortByCustomerCode(ref alPrintData);
            }

            this.SetPrintData(alPrintData, this.printBill.Title);

            System.Drawing.Printing.PaperSize curPaperSize=new System.Drawing.Printing.PaperSize();
            curPaperSize.PaperName = this.printBill.PageSize.Name;
            curPaperSize.Height = this.printBill.PageSize.Height;
            curPaperSize.Width = this.printBill.PageSize.Width;
            this.DrawingMargins.Bottom = this.printBill.PageSize.Top;

            if (this.printBill.IsNeedPreview)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintView(curPaperSize);
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintPage(curPaperSize);
                }
            }
          
            return 1;
        }

        #region IPharmacyBillPrint 成员

        public int SetPrintData(System.Collections.ArrayList alPrintData, FS.SOC.Local.Pharmacy.Base.PrintBill printBill)
        {
            if (alPrintData == null)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowControl(this);
                return 0;
            }
            this.neuFpEnter1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PhaCheckEmptyBillSetting.xml");
            this.printBill = printBill;
            this.alPrintData = alPrintData;

            return Print();
        }

        #endregion

    }
}
