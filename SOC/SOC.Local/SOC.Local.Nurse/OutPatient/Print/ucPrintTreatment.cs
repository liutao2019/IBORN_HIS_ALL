using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.Nurse.Print
{
    public partial class ucPrintTreatment : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Nurse.ITreatmentPrint
    {
        public ucPrintTreatment()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.nlbPatientName.Text = "";
            this.nlbSex.Text = "性别：";
            this.nlbAge.Text = "年龄：";
            this.nlbTime.Text = "时间：";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        private void SetBill(ArrayList alFeeItem)
        {
            if (alFeeItem == null || alFeeItem.Count == 0)
            {
                return;
            }
            ArrayList allItem = new ArrayList();
            string s = "";
            for (int i = 0; i < alFeeItem.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = alFeeItem[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (string.IsNullOrEmpty(feeItem.UndrugComb.ToString()))
                {
                    allItem.Add(feeItem);
                }
                if (s.Contains(feeItem.UndrugComb.ToString()))
                    {
                     
                     }
                else 
                    {
                        allItem.Add(feeItem);
                        s += feeItem.UndrugComb.ToString();
                    }
              
            }
            if (allItem.Count > 0)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = allItem[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
                if (string.IsNullOrEmpty(feeItemList.Patient.Name))
                {
                    FS.HISFC.Models.Registration.Register r = registerMgr.GetByClinic(feeItemList.Patient.ID);
                    if (r != null)
                    {
                        this.nlbPatientName.Text = r.Name;
                        this.nlbSex.Text = "性别：" + r.Sex.Name;
                        this.nlbAge.Text = "年龄：" + registerMgr.GetAge(r.Birthday);
                    }
                }
                else
                {
                    this.nlbPatientName.Text = feeItemList.Patient.Name;
                    this.nlbSex.Text = "性别：" + feeItemList.Patient.Sex.Name;
                    this.nlbAge.Text = "年龄：" + registerMgr.GetAge(feeItemList.Patient.Birthday);
                }
                this.nlbTime.Text = "时间：" + registerMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm");
                //取医院名称
                this.neuLabel1.Text = FS.HISFC.BizProcess.Integrate.Function.GetHosName() + "检验检查单";
                this.neuSpread1_Sheet1.RowCount = alFeeItem.Count;
            }
            int rowIndex = 0;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in allItem)
            {
                if (string.IsNullOrEmpty(f.UndrugComb.ToString()))
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.ItemName].Text = f.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.ItemName].Text = f.UndrugComb.ToString();
                }
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.ComboCode].Text = f.Order.Combo.ID;

                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.Frequency].Text = f.Order.Frequency.Name;
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.Usage].Text = f.Order.Usage.Name;
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.Days].Text = f.Days.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.Number].Text = f.Item.Qty.ToString("F2");
                if (string.IsNullOrEmpty(f.Order.Memo))
                {
                    //FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();
                    //FS.HISFC.Models.Order.OutPatient.Order order = orderMgr.GetOneOrder(f.Patient.ID, f.Order.ID);
                    //if (order != null)
                    //{
                    //    f.Order.Memo = order.Memo;
                    //}
                }
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColPrint.Memo].Text = string.IsNullOrEmpty(f.Order.Memo) ? " " : f.Order.Memo;
             
                rowIndex++;
            }

            //刷新组合号
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.neuSpread1_Sheet1, (int)ColPrint.ComboCode, (int)ColPrint.Combo);
            }

        }

        #region 打印处理 

        ~ucPrintTreatment()
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
            this.DrawControl(graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel1.Height);

            //设置页码
            this.nlbPageNo.Text = "页：" + curPageNO + "/" + maxPageNO;
            //标题部分
            this.DrawControl(graphics, this.neuPanel1, this.neuPanel1.Location.X, this.neuPanel1.Location.Y);

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
                        //graphics.DrawString(
                        //    "第 "+this.curPageNO.ToString()+" 页，共 "+this.maxPageNO.ToString()+" 页",
                        //    neuLblExecTime.Font,
                        //    new SolidBrush(neuLblExecTime.ForeColor),
                        //    this.PrintDocument.DefaultPageSettings.PaperSize.Width/2-100,
                        //    this.PrintDocument.DefaultPageSettings.PaperSize.Height - this.PrintDocument.DefaultPageSettings.Margins.Bottom - 20);
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
                paperSize = new System.Drawing.Printing.PaperSize("xsk", 450, 380);
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

        public int Init()
        {
            this.Clear();

            return 1;
        }

        public int SetData(ArrayList alFeeItem)
        {
            this.Clear();
            this.SetBill(alFeeItem);
            return 1;
        }

        public void PrintBill()
        {
            this.nlbPageNo.Visible = true;
            this.neuSpread1_Sheet1.RowHeaderColumnCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(255, 255, 255), System.Drawing.Color.FromArgb(255, 255, 255), System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(255, 255, 255), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("MZZLD");
            System.Drawing.Printing.PaperSize curPaperSize = new System.Drawing.Printing.PaperSize();
            if (pSize == null)
            {

                pSize = new FS.HISFC.Models.Base.PageSize("xsk", 450, 380);
            }
            curPaperSize.PaperName = pSize.Name;
            curPaperSize.Height = pSize.Height;
            curPaperSize.Width = pSize.Width;
            this.PrintDocument.DefaultPageSettings.Margins.Bottom = pSize.Top;
            
            print.SetPageSize(pSize);

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.PrintView(curPaperSize);
            }
            else
            {
                if (!string.IsNullOrEmpty(pSize.Printer))
                {
                    this.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
                }
                this.PrintPage(curPaperSize);
            }
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(240, 240, 240), System.Drawing.Color.FromArgb(240, 240, 240), System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.RowHeaderColumnCount = 1;
            this.nlbPageNo.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
        }

        enum ColPrint
        {
            ItemName,
            ComboCode,
            Combo,
            Frequency,
            Usage,
            Days,
            Number,
            Memo,
        }
    }
}
