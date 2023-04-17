﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.ChaoYang
{
    public partial class ucPrintItinerateLarge : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint
    {
        #region 域
        private ArrayList alPrint = new ArrayList();
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizLogic.Order.Order OrderBizLogic = new FS.HISFC.BizLogic.Order.Order(); 
        /// <summary>
        /// 是否补打
        /// </summary>
        private bool isReprint = false;

        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsReprint
        {
            get
            {
                return isReprint;
            }
            set
            {
                isReprint = value;
                this.lblReprint.Visible = value;
            }
        }

        #endregion

        #region 打印处理

        ~ucPrintItinerateLarge()
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
            this.DrawControl(graphics, this, 0, 0);

            //数据FarPoint
            this.DrawControl(graphics, this.neuPanel1, this.neuPanel1.Location.X, this.neuPanel2.Height);

            //设置页码
            this.nlbPageNo.Text = "页：" + curPageNO + "/" + maxPageNO;

            //标题部分
            this.DrawControl(graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel2.Location.Y);

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
                        //if (c.Name == this.lbAge.Name)
                        //{
                        //    graphics.DrawString(
                        //        "第 " + this.curPageNO.ToString() + " 页，共 " + this.maxPageNO.ToString() + " 页",
                        //        this.Font,
                        //        new SolidBrush(Color.Black),
                        //        this.PrintDocument.DefaultPageSettings.PaperSize.Width / 2 - 100,
                        //        this.PrintDocument.DefaultPageSettings.PaperSize.Height - this.PrintDocument.DefaultPageSettings.Margins.Bottom - 20);
                        //}
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

        #region 方法
        public ucPrintItinerateLarge()
        {
            InitializeComponent();
            lblHosName.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        private void ucPrintItinerateLarge_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        public void Init(ArrayList al)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
            FS.HISFC.Models.Nurse.Inject info = null;

            //朝阳本地化过滤掉BID,TID等重复项目，原针剂组合是按执行时间和组合来画组合号的，但是医护站没插入执行时间，固会把多次执行的划分到同一组内
            for(int m=0;m<al.Count;m++)
            {
                for (int n = m+1; n < al.Count; n++)
                {
                  FS.HISFC.Models.Nurse.Inject info1 = (FS.HISFC.Models.Nurse.Inject)al[m];
                  FS.HISFC.Models.Nurse.Inject info2 = (FS.HISFC.Models.Nurse.Inject)al[n];
                  if (info1.Item.Item.Name == info2.Item.Item.Name
                      && info1.Item.Name == info2.Item.Name
                      && info1.Item.Order.Combo.ID == info2.Item.Order.Combo.ID 
                      && (info1.Item.Order.DoseOnce + info1.Item.Order.DoseUnit) == (info2.Item.Order.DoseOnce + info2.Item.Order.DoseUnit)
                      && info1.Item.Order.Frequency.ID == info2.Item.Order.Frequency.ID)
                  {
                      al.Remove(al[n]);
                      continue;
                  }
                }
            }

            for (int i = 0; i < al.Count; i++)
            {
                info = (FS.HISFC.Models.Nurse.Inject)al[i];
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                {
                    this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name;
                }
                //this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Item.Name;
                this.neuSpread1_Sheet1.Cells[0, 1].Tag = info.Item.Order.Combo.ID;//组合号
                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Item.Order.DoseOnce + info.Item.Order.DoseUnit;
                this.neuSpread1_Sheet1.Cells[0, 3].Text = info.Item.Order.Usage.Name;
                this.neuSpread1_Sheet1.Cells[0, 4].Text = info.Item.Order.Frequency.ID;//info.Item.DoseOnce.ToString() + info.Item.DoseUnit.ToString();
                this.neuSpread1_Sheet1.Cells[0, 5].Text = info.Item.Days.ToString("F2").Replace(".00","");
                this.neuSpread1_Sheet1.Cells[0, 4].Tag = info.User03;
                //医嘱备注
                this.neuSpread1_Sheet1.Cells[0, 6].Text = info.Memo + this.OrderBizLogic.TransHypotest(info.Hypotest);//this.GetHyTestInfo(info.Hypotest);
                this.neuSpread1_Sheet1.Cells[0, 7].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 8].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 9].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 10].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 11].Text = " ";
            }
            this.SetComb(al);
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            this.lbName.Text = info.Patient.Name;
            this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lbAge.Text = this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
            this.neuLbDoct.Text = info.Item.Order.Doctor.Name;
            if (string.IsNullOrEmpty(info.Patient.PID.CardNO))
            {
                this.neuLbCardNo.Text = info.Patient.Card.ID;
            }
            else
            {
                this.neuLbCardNo.Text = info.Patient.PID.CardNO;
            }
            if (info.Patient.Sex.ID.ToString() == "M")
            {
                this.lbSex.Text = "男";
            }
            else if (info.Patient.Sex.ID.ToString() == "F")
            {
                this.lbSex.Text = "女";
            }
            else
            {
                this.lbSex.Text = "";
            }
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            #region 设置界面用来配合打印

            //System.Windows.Forms.Control c = this.neuPanel1;
            //c.Width = this.neuPanel1.Width;
            //c.Height = this.neuPanel1.Height;

            #endregion

            //打印机

            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //FS.HISFC.Models.Base.PageSize ps = null;
            //ps = psManager.GetPageSize("MZXSK");
            //if (ps == null)
            //{
            //    ps = new FS.HISFC.Models.Base.PageSize("xsk", 450, 400);
            //    ps.Top = 0;
            //    ps.Left = 0;
            //}
            //print.SetPageSize(ps);
            //print.PrintPage(ps.Left, ps.Top, c);


            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("MZXSK");
            System.Drawing.Printing.PaperSize curPaperSize = new System.Drawing.Printing.PaperSize();
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("xsk", 450, 400);
                pSize.Top = 0;
                pSize.Left = 0;
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

        }

        /// <summary>
        /// 获取皮试信息
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "(免试)";
                case "2":
                    return "(需皮试)";
                case "3":
                    return "(＋)";
                case "4":
                    return "(－)";
                case "0":
                default:
                    return "";
            }
        }

        FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);
        FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, true, false, false);
      
        /// <summary>
        /// 设置组合号
        /// </summary>
        private void SetComb(ArrayList al)
        {
            int myCount = this.neuSpread1_Sheet1.RowCount;
            int i;
            //第一行
            this.neuSpread1_Sheet1.SetValue(0, 1, "┓");
            //最后行
            this.neuSpread1_Sheet1.SetValue(myCount - 1, 1, "┛");
            //中间行
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 1].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 1].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 1].Tag.ToString();

                // 患者当次注射处方信息
                string currentRowInjectTime = this.neuSpread1_Sheet1.Cells[i, 4].Tag.ToString();
                string priorRowInjectTime = this.neuSpread1_Sheet1.Cells[prior, 4].Tag.ToString();
                string nextRowInjectTime = this.neuSpread1_Sheet1.Cells[next, 4].Tag.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ┃
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "┃");
                   
                }
                //  ┛
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "┛");
                    this.neuSpread1_Sheet1.Rows[i].Border = lineBorder1;
                }
                //  ┓
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "┓");
                    this.neuSpread1_Sheet1.Rows[i].Border = lineBorder2;
                    
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "");
                    this.neuSpread1_Sheet1.Rows[i].Border = lineBorder1;
                }
                #endregion
            }
            //把没有组号的去掉
            for (i = 0; i < myCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 1].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "");
                }
            }
            //判断首末行 有组号，且只有自己一组数据的情况
            if (myCount == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 1, "");
            }
            //只有首末两行，那么还要判断组号啊
            if (myCount == 2)
            {
                if (this.neuSpread1_Sheet1.Cells[0, 1].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, 1].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 1, "");
                    this.neuSpread1_Sheet1.SetValue(1, 1, "");
                }
            }
            if (myCount > 2)
            {
                if (this.neuSpread1_Sheet1.GetValue(1, 1).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(1, 1).ToString() != "┛")
                {
                    this.neuSpread1_Sheet1.SetValue(0, 1, "");
                }
                if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 1).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(myCount - 2, 1).ToString() != "┓")
                {
                    this.neuSpread1_Sheet1.SetValue(myCount - 1, 1, "");
                }
            }

        }
        #endregion
    }
}
