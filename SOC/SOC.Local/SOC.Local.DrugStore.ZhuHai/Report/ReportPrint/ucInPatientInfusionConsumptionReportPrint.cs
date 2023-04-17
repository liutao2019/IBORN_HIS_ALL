using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.DrugStore.ZhuHai.Report.ReportPrint
{
    /// <summary>
    /// ����Һ���ı����ӡ
    /// </summary>
    public partial class ucInPatientInfusionConsumptionReportPrint : ucBaseControl
    {
        private string curStockDept = string.Empty;
        private string curAdjustReason = string.Empty;
        /// <summary>
        /// ҩƷ����ӡ��
        /// </summary>
        public ucInPatientInfusionConsumptionReportPrint()
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

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(30, 0, 10, 30);

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

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblQueryTime.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblQueryTime.Location.Y;
            graphics.DrawString(this.lblQueryTime.Text, this.lblQueryTime.Font, new SolidBrush(this.lblQueryTime.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region ҳ�����

       

            #endregion

            #region Farpoint����
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel4.Height - neuPanel2.Height;
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left +this.neuFpEnter1.Location.X, this.DrawingMargins.Top + this.neuPanel4.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
            int FarpointHeight = 0;
          
            FarpointHeight =  (int)(this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 85);
           
            #endregion

            #region ҳβ����


            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPrintTime.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPrintTime.Location.Y + this.neuPanel4.Height + FarpointHeight;
            graphics.DrawString(this.nlbPrintTime.Text, this.nlbPrintTime.Font, new SolidBrush(this.nlbPrintTime.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPage.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPage.Location.Y + this.neuPanel4.Height + FarpointHeight;

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
        private void SetPrintData(DataTable dt, int inow, int icount, string title,string stockDept,DateTime beginTime,DateTime endTime,string deptCode)
        {
            this.neuFpEnter1_Sheet1.Rows.Count = 0;
            if (dt == null||dt.Rows.Count == 0)
            {
                MessageBox.Show("û�д�ӡ������!");
                return;
            }

            try
            {
                this.lblTitle.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptCode) + "סԺ����Һ���ı�";
            }
            catch(Exception ex){}

            this.lblQueryTime.Text = "ͳ��ʱ��:" + beginTime.ToString() + endTime.ToString() + "  ����:" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept) + "   ִ����ҩʦ:        �˶Ի�ʿ:";

            this.nlbPrintTime.Text = DateTime.Now.ToString();
            #region Farpoint��ֵ
            decimal sumPreTotCost = 0;
            decimal curPageTotCost = 0;
            for (int i = 0; i < this.dt.Rows.Count; i++)
            {
                if (i != 0 && i % (printBill.RowCount) == 0)
                {
                    this.neuFpEnter1_Sheet1.AddRows(this.neuFpEnter1_Sheet1.Rows.Count, 1);
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 1].Text = "�ϼƣ�";
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 7].Text = curPageTotCost.ToString("F2");
                    curPageTotCost = 0;
                }
                this.neuFpEnter1_Sheet1.AddRows(this.neuFpEnter1_Sheet1.Rows.Count, 1);

                DataRow dr = dt.Rows[i];

                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 0].Text = (i + 1).ToString();

                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count -1, 1].Text = dr["�Զ�����"].ToString();
                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 2].Text = dr["ҩƷ����"].ToString();
                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 3].Text = dr["���"].ToString();
                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 4].Text = dr["����"].ToString();
                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 5].Text = dr["��λ"].ToString();
                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 6].Text = dr["����"].ToString();
                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 7].Text = dr["���"].ToString();
                curPageTotCost += FS.FrameWork.Function.NConvert.ToDecimal(dr["���"]);
            }
            this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.Rows.Count, 1);
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 1].Text = "�ϼ�";
            this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 7].Text = curPageTotCost.ToString("F2"); 
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

        public FS.SOC.Local.Pharmacy.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.Base.PrintBill();

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

        public int SetPrintData(DataTable dt,string stockDept,DateTime beginTime,DateTime endTime,string deptCode)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                this.dt = dt;
            
            }
            this.SetPrintData(dt, 1, 1, string.Empty,stockDept,beginTime,endTime,deptCode);
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
