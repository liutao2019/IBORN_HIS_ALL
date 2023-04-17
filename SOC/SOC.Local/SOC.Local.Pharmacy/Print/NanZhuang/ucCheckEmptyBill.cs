using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.Print.NanZhuang
{
    public partial class ucCheckEmptyBill : UserControl, Base.IPharmacyBillPrint
    {
        public ucCheckEmptyBill()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.neuFpEnter1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuFpEnter1_ColumnWidthChanged);
        }

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

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;          
         
            //标题部分
            this.DrawControl(graphics, this.neuPanel1, this.neuPanel1.Location.X, this.neuPanel1.Location.Y);

            //数据FarPoint
            this.DrawControl(graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel1.Height);

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
                        spread.OwnerPrintDraw(graphics, new Rectangle(
                           locationX + c.Location.X,
                                locationY + c.Location.Y,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY - 20 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                            ), spread.ActiveSheetIndex, this.curPageNO);

                    }
                    else if (c is Panel)
                    {
                        graphics.FillRectangle(new SolidBrush(c.BackColor), locationX + c.Location.X, locationY + c.Location.Y, c.Width, c.Height);
                    }
                    else
                    {
                        graphics.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), locationX + c.Location.X, locationY + c.Location.Y);
                        if (c.Name == this.lblPage.Name)
                        {
                            graphics.DrawString(
                                "第 " + this.curPageNO.ToString() + " 页，共 " + this.maxPageNO.ToString() + " 页",
                                this.lblPage.Font,
                                new SolidBrush(this.lblPage.ForeColor),
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

        #region 打印主函数

        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        /// <summary>
        /// 总金额
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

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
                this.neuFpEnter1.SetCellValue(0, i, "名称", checkDetail.Item.Name);
                this.neuFpEnter1.SetCellValue(0, i, "规格", checkDetail.Item.Specs);
                this.neuFpEnter1.SetCellValue(0, i, "盘点数量1", " ");
                this.neuFpEnter1.SetCellValue(0, i, "包装单位", checkDetail.Item.PackUnit);
                this.neuFpEnter1.SetCellValue(0, i, "盘点数量1", " ");
                this.neuFpEnter1.SetCellValue(0, i, "最小单位", checkDetail.Item.MinUnit);
                this.neuFpEnter1.SetCellValue(0, i, "批号", checkDetail.BatchNO);
                this.neuFpEnter1.SetCellValue(0, i, "购入价", checkDetail.Item.PriceCollection.PurchasePrice);
                this.neuFpEnter1.SetCellValue(0, i, "零售价", checkDetail.Item.PriceCollection.RetailPrice);
                this.neuFpEnter1.SetCellValue(0, i, "摘要", " ");
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
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.物理顺序)
            {
                Base.PrintBill.SortByBillNO(ref alPrintData);
            }

            this.SetPrintData(alPrintData, this.printBill.Title);

            System.Drawing.Printing.PaperSize curPaperSize=new System.Drawing.Printing.PaperSize();
            curPaperSize.PaperName = this.printBill.PageSize.Name;
            curPaperSize.Height = this.printBill.PageSize.Height;
            curPaperSize.Width = this.printBill.PageSize.Width;
            this.PrintDocument.DefaultPageSettings.Margins.Bottom = this.printBill.PageSize.Top;

            if (this.printBill.IsNeedPreview)
            {
                this.PrintView(curPaperSize);
            }
            else
            {
                this.PrintPage(curPaperSize);
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
