using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace GJLocal.HISFC.Components.OpGuide.Fee.IBORN
{
    /// <summary>
    /// [��������: �嵥��ϸ]<br></br>
    /// [�� �� ��: lfhm]<br></br>
    /// [����ʱ��: 2017-10]<br></br>
    /// </summary>
    public partial class ucInPatientFeeTotalAndNurseBill : UserControl, FS.HISFC.BizProcess.Interface.RADT.IBillPrint
    {
        /// <summary>
        /// �嵥��ϸ
        /// </summary>
        public ucInPatientFeeTotalAndNurseBill()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.Clear();
        }

        #region ����


        /// <summary>
        /// ÿҳ������������ǰ���LetterpageRowNum���и߸ı�Ӱ���ҳ
        /// </summary>
        int pageRowNum = 14;

        /// <summary>
        /// ���ҳ��
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// ��ӡ����Ч����,��ѡ��ҳ�뷶Χʱ��Ч
        /// </summary>
        int validRowNum = 0;

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
        /// <summary>
        /// ���д���ӡ����
        /// </summary>
        private ArrayList alPrintData = new ArrayList();


        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// ��ӡ��
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();
        
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            
        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);
        
        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
        
        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();


        
        
        
        #endregion

        #region ��Ϣ����

        public int ShowBill(ArrayList alData, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if(alData.Count <= 0||alData== null)
            {
                return -1;
            }
            alPrintData = alData;
            int rowCount = alData.Count;
            if(patient == null)
            {
                 return -1;
            }
            this.Clear();

            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder bottomAndtopBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
           


            this.lblAge.Text += this.outOrderMgr.GetAge(patient.Birthday, false);
            this.lblBedNo.Text += patient.PVisit.PatientLocation.Bed.ID.Length > 4 ? patient.PVisit.PatientLocation.Bed.ID.Substring(4) : patient.PVisit.PatientLocation.Bed.ID;
            if (patient.PVisit.OutTime >= DateTime.MaxValue || patient.PVisit.OutTime <= DateTime.MinValue)
            {
                this.lblDays.Text += "����δ����Ժ�Ǽ�";
            }
            else
            {
                int days = this.Days(patient.PVisit.InTime, patient.PVisit.OutTime);
                this.lblDays.Text += days;
            }
            //string deptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(patient.ApplyDept.ID);

            this.lblDept.Text += patient.PVisit.PatientLocation.Dept.Name;

            this.nlbRowCount.Text += rowCount;

            this.lblFeeDate.Text += patient.PVisit.InTime.ToShortDateString() + "��" + patient.PVisit.OutTime.ToShortDateString();
            if (string.IsNullOrEmpty(patient.User01))
            {
                this.lblInvoiceNO.Visible = false;
                this.lblInvoiceNO.Text = "";
            }
            else
            {
                this.lblInvoiceNO.Visible = true;
                this.lblInvoiceNO.Text += patient.User01;
            }
            this.lblName.Text += patient.Name;
            this.lblPact.Text += patient.Pact.Name;
            this.lblPatientNo.Text += patient.PID.PatientNO.TrimStart('0');
            if (patient.Sex.ID.ToString() == "F")
            {
                this.lblSex.Text += "Ů";
            }
            else if (patient.Sex.ID.ToString() == "M")
            {
                this.lblSex.Text += "��";
            }
            else
            {
                this.lblSex.Text += "����";
            }
            Dictionary<string, List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>> drugDictionary = new Dictionary<string,List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>>();

            //������������
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in alData)
            {

                List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeList = null;
                if (drugDictionary.ContainsKey(feeInfo.User01))
                {
                    drugDictionary[feeInfo.User01].Add(feeInfo);
                }
                else
                {
                    feeList = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
                    feeList.Add(feeInfo);
                    drugDictionary.Add(feeInfo.User01, feeList);
                }
            }

            if (drugDictionary.Keys.Count <= 0)
            {
                return -1;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            int iRow = -1;
            decimal allTotCost = 0m;
            foreach (string recipeNO in drugDictionary.Keys)
            {

                List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeList = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
                feeList = drugDictionary[recipeNO];

                FS.HISFC.Models.Fee.Inpatient.FeeInfo oneFeeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                oneFeeInfo = drugDictionary[recipeNO][0];

                iRow++;
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 2;
                this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 5;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Text = oneFeeInfo.User01;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Border = bottomAndtopBorder;
                int currInfoLan = iRow;
                decimal totCost = 0m;           
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo obj in feeList)
                {
                    iRow++;
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Text = obj.Item.Name;
                    this.neuSpread1_Sheet1.Cells[iRow, 1].Text = obj.Item.Specs;
                    this.neuSpread1_Sheet1.Cells[iRow, 2].Text = obj.Item.Price.ToString("F2");
                    this.neuSpread1_Sheet1.Cells[iRow, 3].Text = obj.Item.Qty.ToString();
                    this.neuSpread1_Sheet1.Cells[iRow, 4].Text = obj.Item.PriceUnit;
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Text = obj.FT.TotCost.ToString("F2");
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Text = obj.ConfirmOper.Dept.Name;
                    totCost += obj.FT.TotCost;
                }
                allTotCost += totCost;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Text = "�ϼƣ�" + totCost.ToString("F2");
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Border = bottomAndtopBorder;


            }
            //�������һ��

            iRow++;
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "�ܺϼƣ�" + allTotCost.ToString("F2");
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;


            iRow++;
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 5;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "�Ʊ��ˣ�" + FS.FrameWork.Management.Connection.Operator.Name;
            this.neuSpread1_Sheet1.Cells[iRow, 2].Text = "�Ʊ����ڣ�" + orderMgr.GetDateTimeFromSysDateTime().ToLongDateString();
            this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
            this.neuSpread1_Sheet1.Cells[iRow, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[iRow, 2].Border = topBorder;
            this.neuSpread1_Sheet1.Cells[iRow, 2].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           


            return 1;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private int Days(DateTime begDate, DateTime endDate)
        {
            int days = 0;
            if (endDate >= DateTime.MaxValue || endDate <= DateTime.MinValue || begDate >= DateTime.MaxValue || begDate <= DateTime.MinValue)
            {
                return 0;
            }
            for (int i = 1; begDate.AddDays(i).Date <= endDate.Date; i++)
            {
                days++;
            }
            days++;
            if (days == 0)
            {
                days = 1;
            }
            return days;
        }
        public int Print()
        {
            return 1;
        }
        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            this.nlbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.nlbRowCount.Text = "��¼����";
            this.lblInvoiceNO.Text = "��Ʊ�ţ�";
            this.lblPatientNo.Text = "סԺ�ţ�";
            this.lblAge.Text = "���䣺";
            this.lblBedNo.Text = "���ţ�";
            this.lblDays.Text = "������";
            this.lblDept.Text = "���ң�";
            this.lblFeeDate.Text = "ͳ�����ڣ�";
            this.lblName.Text = "������";
            this.lblPact.Text = "���㷽ʽ��";
            this.lblSex.Text = "�Ա�";
            this.lblTitleName.Text = "סԺ�����嵥(���ܣ�";
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

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
            int mainTitleLocalX = this.DrawingMargins.Left + this.nlbTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.nlbTitle.Location.Y;
            graphics.DrawString(this.nlbTitle.Text, this.nlbTitle.Font, new SolidBrush(this.nlbTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int lblTitleNameLocalX = this.DrawingMargins.Left + this.lblTitleName.Location.X;
            int lblTitleNameTitleLoaclY = this.DrawingMargins.Top + this.lblTitleName.Location.Y;
            graphics.DrawString(this.lblTitleName.Text, this.lblTitleName.Font, new SolidBrush(this.lblTitleName.ForeColor), lblTitleNameLocalX, lblTitleNameTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblDept.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblDept.Location.Y;
            graphics.DrawString(this.lblDept.Text, this.lblDept.Font, new SolidBrush(this.lblDept.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblInvoiceNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblInvoiceNO.Location.Y;
            graphics.DrawString(this.lblInvoiceNO.Text, this.lblInvoiceNO.Font, new SolidBrush(this.lblInvoiceNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPatientNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPatientNo.Location.Y;
            graphics.DrawString(this.lblPatientNo.Text, this.lblPatientNo.Font, new SolidBrush(this.lblPatientNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;
            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblAge.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblAge.Location.Y;
            graphics.DrawString(this.lblAge.Text, this.lblAge.Font, new SolidBrush(this.lblAge.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblBedNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblBedNo.Location.Y;
            graphics.DrawString(this.lblBedNo.Text, this.lblBedNo.Font, new SolidBrush(this.lblBedNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblDays.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblDays.Location.Y;
            graphics.DrawString(this.lblDays.Text, this.lblDays.Font, new SolidBrush(this.lblDays.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblFeeDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblFeeDate.Location.Y;
            graphics.DrawString(this.lblFeeDate.Text, this.lblFeeDate.Font, new SolidBrush(this.lblFeeDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPact.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPact.Location.Y;
            graphics.DrawString(this.lblPact.Text, this.lblPact.Font, new SolidBrush(this.lblPact.ForeColor), additionTitleLocalX, additionTitleLocalY);
           
            additionTitleLocalX = this.DrawingMargins.Left + this.lblName.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblName.Location.Y;
            graphics.DrawString(this.lblName.Text, this.lblName.Font, new SolidBrush(this.lblName.ForeColor), additionTitleLocalX, additionTitleLocalY);


            additionTitleLocalX = this.DrawingMargins.Left + this.lblPatientNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPatientNo.Location.Y;
            graphics.DrawString(this.lblPatientNo.Text, this.lblPatientNo.Font, new SolidBrush(this.lblPatientNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblSex.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblSex.Location.Y;
            graphics.DrawString(this.lblSex.Text, this.lblSex.Font, new SolidBrush(this.lblSex.ForeColor), additionTitleLocalX, additionTitleLocalY);

      
            #endregion

            #region Farpoint����
            int drawingWidth = 850 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 1100 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
            //int FarpointHeight = 0;
            //if (curPageNO == maxPageNO)
            //{
            //    if (alPrintData.Count % 29 == 0)
            //    {
            //        FarpointHeight = (int)(this.neuSpread1.Height + this.neuSpread1.Sheets[0].Rows.Default.Height * 29 + 5);
            //    }
            //    else
            //    {
            //        FarpointHeight = (int)(this.neuSpread1.Height + this.neuSpread1.Sheets[0].Rows.Default.Height * (alPrintData.Count % 29) + 5);
            //    }
            //}
            //else
            //{
            //    FarpointHeight = (int)(this.neuSpread1.Height + this.neuSpread1.Sheets[0].Rows.Default.Height * 29 + 5);
            //}
            #endregion

            #region ҳ�����

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPageNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPageNo.Location.Y;

            graphics.DrawString("ҳ�룺" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.nlbPageNo.Font, new SolidBrush(this.nlbPageNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

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

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize paperSize1 = pageSizeMgr.GetPageSize("A4");
                paperSize = new System.Drawing.Printing.PaperSize("A4", paperSize1.Width, paperSize1.Height);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        private void myPrintView()
        {
            //PrintDialog pt = new PrintDialog();
            //pt.Document = this.PrintDocument;
            //pt.AllowCurrentPage = true;
            //pt.AllowSomePages = true;
            //pt.AllowPrintToFile = true; 

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
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

        /// <summary>
        /// ��ӡҳ��ѡ��
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {
            int drawingWidth = 850 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 1100 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = false;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuSpread1_Sheet1.RowHeader.Visible;
            this.neuSpread1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuSpread1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0);

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


        /// <summary>
        /// ��ӡ
        /// </summary>
        public int PrintPreview()
        {
            System.Drawing.Printing.PaperSize paperSize = null;
            //this.SetPaperSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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
                    this.SetPaperSize(paperSize);
                    this.PrintDocument.Print();
                }
            }


            return 1;

        }

        #endregion

        #region ���÷���

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            this.Clear();
        }

        #endregion


        #region IInpatientBill ��Ա������ʱ��

        /// <summary>
        /// �ṩ����ѡ���ӡ��Χ�Ĵ�ӡ����
        /// </summary>
        private void PrintPage()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPreview();

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
        }


        #endregion

        #region IBillPrint ��Ա

        /// <summary>
        /// ����{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int Export(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    neuSpread1_Sheet1.SaveXml(fileName);
                  //  this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.None);

                    MessageBox.Show("��ܰ��ʾ", "�����ɹ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ݷ�������>>" + ex.Message);
                return -1;
            }

            return 0;
        }

        #endregion

        #region IBillPrint ��Ա

        /// <summary>
        /// ��Ҫ�����Ŀؼ�������Դ{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int ShowData(DataTable dt, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
