using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint
{
    /// <summary>
    /// ��ݸҩƷ��ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPharmacyOutputSummaryByDeptPrint : ucBaseControl
    {
        private string curStockDept = string.Empty;
        private string curAdjustReason = string.Empty;
        /// <summary>
        /// ҩƷ����ӡ��
        /// </summary>
        public ucPharmacyOutputSummaryByDeptPrint()
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
        private DataTable dt = new DataTable();

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

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(30, 0, 80, 30);

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
            int mainTitleLocalX = this.DrawingMargins.Left + (this.Width -  this.lblTitle.Width)/2;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.lblTitle.Location.Y;
            graphics.DrawString(this.lblTitle.Text, this.lblTitle.Font, new SolidBrush(this.lblTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.label3.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.label3.Location.Y;
            graphics.DrawString(this.label3.Text, this.label3.Font, new SolidBrush(this.label3.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPrintDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPrintDate.Location.Y;
            graphics.DrawString(this.nlbPrintDate.Text, this.nlbPrintDate.Font, new SolidBrush(this.nlbPrintDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblBeginDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblBeginDate.Location.Y;
            graphics.DrawString(this.lblBeginDate.Text, this.label3.Font, new SolidBrush(this.lblBeginDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.label5.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.label5.Location.Y;
            graphics.DrawString(this.label5.Text, this.label5.Font, new SolidBrush(this.label5.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblEndTime.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblEndTime.Location.Y;
            graphics.DrawString(this.lblEndTime.Text, this.lblEndTime.Font, new SolidBrush(this.lblEndTime.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region ҳ�����

       

            #endregion

            #region Farpoint����
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = this.printBill.PageSize.Height - 2 * this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel4.Height - neuPanel3.Height;
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left + this.neuFpEnter1.Location.X, this.DrawingMargins.Top + this.neuPanel3.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
            int FarpointHeight = 0;
          
            FarpointHeight =  (int)(this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + this.neuFpEnter1_Sheet1.ColumnHeader.Rows[0].Height);
           
            #endregion

            #region ҳβ����

         
            additionTitleLocalX = this.DrawingMargins.Left + this.label2.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.label2.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.label2.Text, this.label2.Font, new SolidBrush(this.label2.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbOper.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.nlbOper.Text, this.nlbOper.Font, new SolidBrush(this.nlbOper.ForeColor), additionTitleLocalX, additionTitleLocalY);


            additionTitleLocalX = this.DrawingMargins.Left + this.lblPage.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPage.Location.Y;

            graphics.DrawString("ҳ�룺" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.lblPage.Font, new SolidBrush(this.lblPage.ForeColor), additionTitleLocalX, additionTitleLocalY);

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
            int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel3.Height - this.neuPanel2.Height;

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
            if (string.IsNullOrEmpty(printBill.PageSize.Printer))
            {
            }
            else
            {
                this.PrintDocument.PrinterSettings.PrinterName = printBill.PageSize.Printer;
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
        private void SetPrintData(DataTable dt, int inow, int icount, string title,DateTime beginTime,DateTime endTime)
        {
            this.neuFpEnter1_Sheet1.Rows.Count = 0;
            if (dt == null||dt.Rows.Count == 0)
            {
                MessageBox.Show("û�д�ӡ������!");
                return;
            }

            this.lblBeginDate.Text = beginTime.ToShortDateString();
            this.lblEndTime.Text = endTime.ToShortDateString();
            this.nlbPrintDate.Text = ajustMgr.GetDateTimeFromSysDateTime().ToShortDateString();
            #region Farpoint��ֵ
            for (int i = 0; i < this.dt.Rows.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                DataRow dr = dt.Rows[i];
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = dr["�������"].ToString();
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = dr["���"].ToString();
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = dr["������"].ToString();
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = dr["���۽��"].ToString();
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = dr["�������"].ToString();
            }
            #endregion
            this.nlbOper.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(ajustMgr.Operator.ID);
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

        public Base.PrintBill printBill = new Base.PrintBill();

        /// <summary>
        /// IBillPrint��ԱPrint
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region ��ӡ��Ϣ����

            #endregion

            #region ��ҳ��ӡ

            //�ֵ��ݴ�ӡ
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
            #endregion

            return 1;
        }

        public int SetPrintData(DataTable dt,DateTime beginTime,DateTime endTime)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                this.dt = dt;
            
            }
            this.SetPrintData(dt, 1, 1, string.Empty,beginTime,endTime);
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
