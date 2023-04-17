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
    /// ��ݸҩƷ��ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaAdjustBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        private string curStockDept = string.Empty;
        private string curAdjustReason = string.Empty;
        /// <summary>
        /// ҩƷ����ӡ��
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

     
        #region ����

        /// <summary>
        /// ���д���ӡ����
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// ���۹�����
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Adjust ajustMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Adjust();
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

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(0, 0, 20, 40);

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

            #region ҳ�����

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPage.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPage.Location.Y;

            graphics.DrawString("ҳ�룺" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.lblPage.Font, new SolidBrush(this.lblPage.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint����
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel4.Height - neuPanel2.Height;
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel4.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
            int FarpointHeight = 0;
          
            FarpointHeight =  (int)(this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 30);
           
            #endregion

            #region ҳβ����
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

            if (al.Count <= 0)
            {
                MessageBox.Show("û�д�ӡ������!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.AdjustPrice info = (FS.HISFC.Models.Pharmacy.AdjustPrice)al[0];

            #region label��ֵ
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[������]") != -1)
                {


                    this.lblTitle.Text = title.Replace("[������]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));

                }
                else
                {
                    this.lblTitle.Text = title;
                }
            }

            if (info.StockDept.ID == this.curStockDept)
            {
                this.lblBillID.Text = "���۵��ţ�" + info.ID;
                this.lblAdjustResson.Text = "����ԭ��" + this.curAdjustReason;
            }
            else
            {
                this.lblBillID.Text = string.Empty;
                this.lblAdjustResson.Text = string.Empty;
            }

            this.lblAdjustDate.Text = "�������ڣ�" + info.InureTime.Year + "." + info.InureTime.Month.ToString().PadLeft(2, '0') + "." + info.InureTime.Day.ToString().PadLeft(2, '0');
            this.lblOper.Text = "�Ƶ��ˣ�" + info.Operation.Oper.Name;
            this.lblPage.Text = "ҳ��" + inow.ToString() + "/" + icount.ToString();

            #endregion

            #region farpoint��ֵ
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
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ajustInfo.Item.ID).UserCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = ajustInfo.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = ajustInfo.Item.Specs;//���		
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
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 0].Text = "�ϼ�";
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 8].Value = sumPreTotCost;
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 10].Value = sumAfterTotCost;
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 11].Value = sumAfterTotCost - sumPreTotCost;
            

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

            #endregion

            #region ��ҳ��ӡ


            //�����ʱ������ж��ŵ��ݣ��ֿ�
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

            //�ֵ��ݴ�ӡ
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
