using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.GJ
{
    /// <summary>
    /// ��ݸҩƷ��ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaApplyInputBillIBORN : ucBaseControl, Base.IPharmacyBillPrint
    {
        private string curStockDept = string.Empty;

        private bool isReprint = false;
        /// <summary>
        /// ҩƷ����ӡ��
        /// </summary>
        public ucPhaApplyInputBillIBORN()
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

     
        #region ����

        /// <summary>
        /// ���д���ӡ����
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// ���ݵ��ܽ��
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

        #region ��ӡ���
        /// <summary>
        /// ��ǰ��ӡҳ��ҳ��
        /// �����Զ������
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// ���δ�ӡ���ҳ��
        /// �����Զ������
        /// </summary>
        private int maxPageNO = 1;

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(0, 0, 10, 30);

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //����ѡ���ӡ��Χ�������
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

            #region �������
            int mainTitleLocalX = this.DrawingMargins.Left + this.lblTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.lblTitle.Location.Y;
            graphics.DrawString(this.lblTitle.Text, this.lblTitle.Font, new SolidBrush(this.lblTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblTargetDept.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblTargetDept.Location.Y;
            graphics.DrawString(this.lblTargetDept.Text, this.lblTargetDept.Font, new SolidBrush(this.lblTargetDept.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblBillID.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblBillID.Location.Y;
            graphics.DrawString(this.lblBillID.Text, this.lblBillID.Font, new SolidBrush(this.lblBillID.ForeColor), additionTitleLocalX, additionTitleLocalY);
            
            additionTitleLocalX = this.DrawingMargins.Left + this.lblPrintDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPrintDate.Location.Y;
            graphics.DrawString(this.lblPrintDate.Text, this.lblPrintDate.Font, new SolidBrush(this.lblPrintDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblInputDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblInputDate.Location.Y;
            graphics.DrawString(this.lblInputDate.Text, this.lblInputDate.Font, new SolidBrush(this.lblInputDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            //additionTitleLocalX = this.DrawingMargins.Left + this.nlbMemo.Location.X;
            //additionTitleLocalY = this.DrawingMargins.Top + this.nlbMemo.Location.Y;
            //graphics.DrawString(this.nlbMemo.Text, this.nlbMemo.Font, new SolidBrush(this.nlbMemo.ForeColor), additionTitleLocalX, additionTitleLocalY);
            #endregion

            #region ҳ�����

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPage.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPage.Location.Y;

            graphics.DrawString("ҳ�룺" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.lblPage.Font, new SolidBrush(this.lblPage.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint����
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            //int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel4.Height - neuPanel2.Height;
            int drawingHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount);
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel4.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
            int FarpointHeight = 0;
            if(curPageNO == maxPageNO)
            {
                if(alPrintData.Count % printBill.RowCount == 0)
                {
                    FarpointHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount+ 5);
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

            #region ҳβ����
            additionTitleLocalX = this.DrawingMargins.Left + this.lblOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOper.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.lblOper.Text, this.lblOper.Font, new SolidBrush(this.lblOper.ForeColor), additionTitleLocalX, additionTitleLocalY);


            additionTitleLocalX = this.DrawingMargins.Left + this.lblBuyer.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblBuyer.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.lblBuyer.Text, this.lblBuyer.Font, new SolidBrush(this.lblBuyer.ForeColor), additionTitleLocalX, additionTitleLocalY);

            //additionTitleLocalX = this.DrawingMargins.Left + this.nlbDrugFinOper.Location.X;
            //additionTitleLocalY = this.DrawingMargins.Top + this.nlbDrugFinOper.Location.Y + this.neuPanel4.Height + FarpointHeight;
            //graphics.DrawString(this.nlbDrugFinOper.Text, this.nlbDrugFinOper.Font, new SolidBrush(this.nlbDrugFinOper.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblCurPur.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblCurPur.Location.Y + this.neuPanel4.Height + FarpointHeight;
            TotCost totCostByPage = (TotCost)hsTotCostByPage[curPageNO];
            graphics.DrawString("��ҳ�ϼƣ��ۼ۽�" + totCostByPage.retailCost.ToString("F2"), this.lblCurPur.Font, new SolidBrush(this.lblCurPur.ForeColor), additionTitleLocalX, additionTitleLocalY);


         
            if (this.curPageNO == this.maxPageNO)
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.lblTotPurCost.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblTotPurCost.Location.Y + this.neuPanel4.Height + FarpointHeight;
                graphics.DrawString(this.lblTotPurCost.Text, this.lblTotPurCost.Font, new SolidBrush(this.lblTotPurCost.ForeColor), additionTitleLocalX, additionTitleLocalY);

             }

          
            #endregion

            #region ��ҳ
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
        /// ��ӡҳ��ѡ��
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            //int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel4.Height - this.neuPanel2.Height;
            int drawingHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount);

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
                MessageBox.Show("��ӡ������" + ex.Message);
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

        #region ��ⵥ��ӡ
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="i">�ڼ�ҳ</param>
        /// <param name="count">��ҳ��</param>
        /// <param name="title">����</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            FarPoint.Win.LineBorder border1 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, false, false, true);
            
            hsTotCostByPage = new Hashtable();

            if (al.Count <= 0)
            {
                MessageBox.Show("û�д�ӡ������!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)al[0];

            #region label��ֵ

            //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
            FS.HISFC.Models.Base.Department deptInfo = FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(info.ApplyDept.ID);

            if (string.IsNullOrEmpty(title))
            {
                //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
                //this.lblTitle.Text = string.Format(this.consMgr.Hospital.Name + this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyDept.ID));
                this.lblTitle.Text = string.Format(deptInfo.HospitalName + this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyDept.ID));
            }
            else
            {
                if (title.IndexOf("[������]") != -1)
                {


                    this.lblTitle.Text = title.Replace("[������]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyDept.ID));

                }
                else
                {
                    this.lblTitle.Text = title;
                }

                if (title.IndexOf("[Ժ��]") != -1)
                {
                    this.lblTitle.Text = this.lblTitle.Text.Replace("[Ժ��]", deptInfo.HospitalName);
                }
            }
            this.lblTargetDept.Text = "��Դ���ң�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID);
        
            this.neuFpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Border = border1;
            this.neuFpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            curStockDept = info.StockDept.ID;

            this.lblBillID.Text = "���뵥��:" + info.BillNO;
            this.lblInputDate.Text = "��������:" + info.Operation.ApplyOper.OperTime;
            this.lblOper.Text = "�Ʊ���:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID) ;
            this.lblPage.Text = "ҳ:" + inow.ToString() + "/" + icount.ToString();
            this.lblPrintDate.Text = "�Ƶ�����:" + DateTime.Now.ToString();

            #endregion

            #region farpoint��ֵ
            decimal sumPurchase = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            int pageNo = 1;
            int index = 1;
            //string memo = string.Empty;
            for (int i = 0; i < al.Count; i++)
            {
                if((i != 0) &&(i% printBill.RowCount== 0))
                {
                    pageNo++;
                    sumPurchase = 0;
                }
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = al[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.Item itemInfo = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                this.lblBuyer.Text = "�����ˣ�";
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = index.ToString();

                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = applyOut.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = applyOut.Item.Specs;//���	
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(itemInfo.Product.Producer.ID);
                decimal packqty = applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                if (packqty == Math.Ceiling(applyOut.Operation.ApplyQty / applyOut.Item.PackQty))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (applyOut.Operation.ApplyQty / applyOut.Item.PackQty).ToString();
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (applyOut.Operation.ApplyQty / applyOut.Item.PackQty).ToString("F2");
                }

                this.neuFpEnter1_Sheet1.Cells[i, 6].Text = itemInfo.PackUnit;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = Function.GetCostDecimal();

                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;

                this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "�ۼ�(Ԫ)";
                this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "�ۼ۽��(Ԫ)";
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = itemInfo.PriceCollection.RetailPrice;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (itemInfo.PriceCollection.RetailPrice * packqty).ToString("F2");

                
                if(hsTotCostByPage.Contains(pageNo))
                {
                     TotCost totCostByPage  = (TotCost)hsTotCostByPage[pageNo];
                     totCostByPage.purchaseCost += itemInfo.PriceCollection.PurchasePrice * packqty;
                     totCostByPage.wholeSaleCost += itemInfo.PriceCollection.WholeSalePrice * packqty;
                     totCostByPage.retailCost += itemInfo.PriceCollection.RetailPrice * packqty;
                }
                else
                {
                    TotCost totCostByPage = new TotCost();
                    totCostByPage.retailCost += itemInfo.PriceCollection.PurchasePrice * packqty;
                    totCostByPage.wholeSaleCost += itemInfo.PriceCollection.WholeSalePrice * packqty;
                    totCostByPage.purchaseCost += itemInfo.PriceCollection.RetailPrice * packqty;
                    hsTotCostByPage.Add(pageNo, totCostByPage);
                }
                index++;
                sumPurchase += itemInfo.PriceCollection.RetailPrice * packqty;

            }
            //��ǰҳ����
            this.lblCurPur.Text = "��ҳ�ϼƣ��ۼ۽�" + sumPurchase.ToString("F2");


            //������
            this.lblTotPurCost.Text = "�ܺϼƣ��ۼ��ܽ�" + ((TotCost)hsTotCost[info.BillNO]).retailCost.ToString("F2");
            #endregion

            this.resetTitleLocation();

        }

        /// <summary>
        /// �������ñ���λ��
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

        #region IPharmacyBill ��Ա

        private Base.PrintBill printBill = new Base.PrintBill();

        /// <summary>
        /// IBillPrint��ԱPrint
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region ��ӡ��Ϣ����

            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.IsDataAutoExtend = false;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            //p.IsHaveGrid = true;
            //p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region ��ҳ��ӡ
            //int height = this.neuPanel5.Height;
            //int ucHeight = this.Height;
            //float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            //this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.��λ��)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.����˳��)
            {
                Base.PrintBill.SortByBillNO(ref alPrintData);
            }

            //�����ʱ������ж��ŵ��ݣ��ֿ�
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Item itemInfo = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                decimal packqty = applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                
                if (hs.Contains(applyOut.BillNO))
                {

                    ArrayList al = (ArrayList)hs[applyOut.BillNO];
                    al.Add(applyOut);

                    TotCost tc = (TotCost)hsTotCost[applyOut.BillNO];
                    tc.purchaseCost += itemInfo.PriceCollection.PurchasePrice * packqty;
                    tc.wholeSaleCost += itemInfo.PriceCollection.WholeSalePrice * packqty;
                    tc.retailCost += itemInfo.PriceCollection.RetailPrice * packqty;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(applyOut);
                    hs.Add(applyOut.BillNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = itemInfo.PriceCollection.PurchasePrice * packqty;
                    tc.wholeSaleCost = itemInfo.PriceCollection.WholeSalePrice * packqty;
                    tc.retailCost = itemInfo.PriceCollection.RetailPrice * packqty;
                    hsTotCost.Add(applyOut.BillNO, tc);
                }
            }

            //�ֵ��ݴ�ӡ
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

                ////��ҳ��ӡ
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

            this.alPrintData = alPrintData;
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
