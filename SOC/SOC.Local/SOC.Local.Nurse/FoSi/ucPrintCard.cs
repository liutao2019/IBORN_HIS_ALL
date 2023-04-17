using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Nurse.FoSi
{
    public partial class ucPrintCard : System.Windows.Forms.UserControl, Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint
    {
        #region 域
        private ArrayList alPrint = new ArrayList();
        int height = 0;
        int width = 0;
        private Neusoft.HISFC.BizLogic.Nurse.Inject injectMgr = new Neusoft.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.PageSize psManager = new Neusoft.HISFC.BizLogic.Manager.PageSize();
        Neusoft.HISFC.Models.Base.PageSize pageSize;
        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList=new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();

        Neusoft.HISFC.BizLogic.Fee.Outpatient feeManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        #endregion

        #region 打印处理

        ~ucPrintCard()
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

            ////数据FarPoint
            //this.DrawControl(graphics, this, 0, 0);

            ////数据FarPoint
            //this.DrawControl(graphics, this.neuPanel1, this.neuPanel1.Location.X, this.neuPanel2.Height);

            ////底部部分
            //this.DrawControl(graphics, this.neuPanel3, this.neuPanel3.Location.X, height-this.neuPanel3.Height);

            ////标题部分
            //this.DrawControl(graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel2.Location.Y);

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
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY- 40 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                                ), spread.ActiveSheetIndex);
                            maxPageNO = count;
                        }
                        spread.OwnerPrintDraw(graphics, new Rectangle(
                           locationX + c.Location.X,
                                locationY + c.Location.Y,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY -40 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                            ), spread.ActiveSheetIndex, this.curPageNO);

                    }
                    else if (c is Panel)
                    {
                        graphics.FillRectangle(new SolidBrush(c.BackColor), locationX + c.Location.X, locationY + c.Location.Y, c.Width, c.Height);
                    }
                    else if (c is Label && Color.Black.A == c.BackColor.A && Color.Black.B == c.BackColor.B && Color.Black.G == c.BackColor.G)
                    {
                        graphics.FillRectangle(new SolidBrush(c.BackColor), locationX + c.Location.X, locationY + c.Location.Y, c.Width, c.Height);
                    }
                    else if (c is System.Windows.Forms.PictureBox)
                    {
                        graphics.DrawImage(((System.Windows.Forms.PictureBox)c).Image, c.Location);
                    }
                    else
                    {
                        graphics.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), locationX + c.Location.X, locationY + c.Location.Y);
                    }
                    //if (c.Controls.Count > 0)
                    //{
                    //    this.DrawControl(graphics, c, locationX + c.Location.X, locationY + c.Location.Y);
                    //}
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
                paperSize = new System.Drawing.Printing.PaperSize("MZZSBQ", 350, 350);
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
        public ucPrintCard()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        private void ucPrintItinerateLarge_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        public void Init(ArrayList al)
        {

            this.neuSpread1_Sheet1.RowCount = 0;
            Neusoft.HISFC.Models.Nurse.Inject info = null;
            int rowcout = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuSpread1_Sheet1.RowCount++;
                rowcout = this.neuSpread1_Sheet1.RowCount;
                info = (Neusoft.HISFC.Models.Nurse.Inject)al[i];
                if (info.Item.Item.Name != null && info.Item.Item.Name != "") //药品名
                {
                    this.neuSpread1_Sheet1.Cells[rowcout-1, 0].Text = "▲" + info.Item.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[rowcout-1, 0].Text = "▲" + info.Item.Name;
                }
                string usagename = info.Item.Order.Usage.Name;

                if(i==0)
                {
                    this.neuSpread1_Sheet1.Cells[rowcout - 1, 1].Text = info.Item.Order.Usage.Name;//用法
                    ////this.neuSpread1_Sheet1.Cells[rowcout - 1, 1].Text = info.Item.Order.Frequency.ID.ToLower();//频次
                }
                //this.neuSpread1_Sheet1.RowCount++;
                ///this.neuSpread1_Sheet1.Cells[rowcout, 0].ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[rowcout-1, 2].Text =float.Parse(info.Item.Order.DoseOnce.ToString()).ToString() + info.Item.Order.DoseUnit;
            }
            //if (rowcout < 4)
            //{
            //    int j = 4 - rowcout;
            //    this.neuSpread1_Sheet1.RowCount = this.neuSpread1_Sheet1.RowCount + j;
            //    this.neuSpread1_Sheet1.Cells[4, 0].Text = "      ";
            //}

            this.lbname.Text = info.Patient.Name; //病人姓名
            if (string.IsNullOrEmpty(info.Patient.PID.CardNO))
            {
                this.npbBarCode.Image = this.CreateBarCode(info.Patient.Card.ID); //病历号条码
                this.neuLblCardNo.Text = info.Patient.Card.ID.TrimStart('0');//病历号
            }
            this.Print();
            //Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            //print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;

            //Neusoft.HISFC.BizLogic.Manager.PageSize pgMgr = new Neusoft.HISFC.BizLogic.Manager.PageSize();
            //Neusoft.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("MZZSBQ");
            //System.Drawing.Printing.PaperSize curPaperSize = new System.Drawing.Printing.PaperSize();
            //if (pSize == null)
            //{
            //    pSize = new Neusoft.HISFC.Models.Base.PageSize("MZZSBQ", 812, 583);
            //    pSize.Top = 0;
            //    pSize.Left = 0;
            //}

            //curPaperSize.PaperName = pSize.Name;
            //curPaperSize.Height = pSize.Height;
            //curPaperSize.Width = pSize.Width;
            //this.height = pSize.Height;
            //this.width = pSize.Width;
            //this.PrintDocument.DefaultPageSettings.Margins.Bottom = pSize.Top;

            //print.SetPageSize(pSize);

            //if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    this.PrintView(curPaperSize);
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(pSize.Printer))
            //    {
            //        this.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
            //    }

            //    this.PrintPage(curPaperSize);
            //}

        }
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = psManager.GetPageSize("MZZSBQ");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new Neusoft.HISFC.Models.Base.PageSize("MZZSBQ", 350, 350);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            //try
            //{
            //    //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
            //    Neusoft.FrameWork.WinForms.Classes.Print.ResumePrintJob();
            //}
            //catch { }
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }
        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = false;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }
        #endregion
    }
}
