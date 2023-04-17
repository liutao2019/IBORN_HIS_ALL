using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace IBorn.SI.MedicalInsurance.BaseControls
{
    /// <summary>
    /// [��������: BBҽ���嵥�����嵥����ʵ����]<br></br>
    /// [�� �� ��: lzd]<br></br>{5A04A8EF-06C3-45b9-9E6C-E5D152836257}
    /// [����ʱ��: 2020-07]<br></br>
    ///{3D2F489C-A1A7-483f-A66B-5D4DCA0347DC}
    /// </summary>
    public partial class ucInPatientFeeDetailBabyBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// �����嵥
        /// </summary>
        public ucInPatientFeeDetailBabyBill()
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


        //FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// ��ӡ��
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        //private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        FS.HISFC.Models.RADT.PatientInfo nowpatient; //{75ECD815-8FC2-4f58-8E64-A5BD928D3935}



        #endregion

        #region ҽ���۸�ϼ���Ϣ
        public decimal total, sbzfje, grzfje;

        #endregion
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
        #region ��Ϣ����

        public int ShowBill(DataTable dt, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ShowData(dt, patient);// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
            if (patient == null)
            {
                return -1;
            }
            this.Clear();

            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder bottomAndtopBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            this.lblDept.Text += patient.PVisit.PatientLocation.Dept.Name;

            this.lblFeeDate.Text += patient.PVisit.InTime.ToShortDateString() + "��" + patient.PVisit.OutTime.ToShortDateString();
            this.lbprintdate.Text = "��ӡ���ڣ�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblName.Text += patient.Name;
            this.lblPatientNo.Text += patient.PID.PatientNO;
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
            Dictionary<string, List<DataRow>> drugDictionary = new Dictionary<string, List<DataRow>>();

            //������������
            foreach (DataRow r in dt.Rows)
            {

                List<DataRow> feeList = null;
                if (drugDictionary.ContainsKey(r[0].ToString()))
                {
                    drugDictionary[r[0].ToString()].Add(r);
                }
                else
                {
                    feeList = new List<DataRow>();
                    feeList.Add(r);
                    drugDictionary.Add(r[0].ToString(), feeList);
                }
            }

            if (drugDictionary.Keys.Count <= 0)
            {
                return -1;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            int iRow =-1;
            decimal allTotCost = 0m;
            int index = 1;
            foreach (string recipeNO in drugDictionary.Keys)
            {
                List<DataRow> rowlist = new List<DataRow>();
                rowlist = drugDictionary[recipeNO];
                DataRow  oneFeeInforow = drugDictionary[recipeNO][0];

                iRow++;
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 3;
                this.neuSpread1_Sheet1.Cells[iRow, 4].ColumnSpan = 4;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Text = oneFeeInforow[0].ToString();
                this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Border = bottomAndtopBorder;
                int currInfoLan = iRow;
                decimal totCost = 0m;
                foreach (DataRow dr in rowlist)
                {
                    iRow++;
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Text = index.ToString();//���
                    this.neuSpread1_Sheet1.Cells[iRow, 1].Text = dr[0].ToString();//�������
                    this.neuSpread1_Sheet1.Cells[iRow, 2].Text = dr[1].ToString();//����
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Text = dr[2].ToString();//���// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Text = Convert.ToDecimal(dr[3]).ToString("F4");//ҽ����
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Text = Convert.ToDecimal(dr[4]).ToString("F2");//����
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Text = Convert.ToDecimal(dr[5]).ToString("F2");//���
                    totCost += Convert.ToDecimal(dr[5]);
                    
                    index++;
                }
                allTotCost += totCost;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 4].Text = "�ϼƣ�" + totCost.ToString("F2");
                this.neuSpread1_Sheet1.Cells[currInfoLan, 4].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 4].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[currInfoLan, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 4].Border = bottomAndtopBorder;

            }
            //�������һ��

            iRow++;
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "�ܺϼƣ�" + allTotCost.ToString("F2"); //+ " ͳ��֧����" + sbzfje.ToString("F2") + " ����֧����" + grzfje.ToString("F2");
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;


            //iRow++;
            //this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            //this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 3;
            //this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 4;
            //this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "�Ʊ��ˣ�" + FS.FrameWork.Management.Connection.Operator.Name;
            //this.neuSpread1_Sheet1.Cells[iRow, 3].Text = "�Ʊ����ڣ�" + constantMgr.GetDateTimeFromSysDateTime().ToLongDateString();
            //this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
            //this.neuSpread1_Sheet1.Cells[iRow, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells[iRow, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells[iRow, 3].Border = topBorder;
            //this.neuSpread1_Sheet1.Cells[iRow, 3].Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            return 1;
        }

        public int Print()
        {
            return 1;
        }
        /// <summary>
        /// ����{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int Export()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
                dlg.FileName = nowpatient.Name+ "ҽ��סԺ�����嵥����";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;

                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    MessageBox.Show("�����ɹ�", "��ܰ��ʾ");
                    //this.ShowBalloonTip(5000, "��ܰ��ʾ", "�����ɹ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ݷ�������>>" + ex.Message);
                return -1;
            }
            return 1;
            // throw new NotImplementedException();
        }
        /// <summary>
        /// ����
        /// {8AA24CF1-D42B-4978-9D60-7083330080E5}
        /// </summary>
        public void Clear()
        {
            //this.nlbTitle.Text ="ҽ�ƻ�������"+ FS.FrameWork.Management.Connection.Hospital.Name;
            this.lbhospital.Text = "ҽ�ƻ������ƣ�" + FS.FrameWork.Management.Connection.Hospital.Name;// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
            this.nlbRowCount.Text = "��¼����";
            this.lblPatientNo.Text = "סԺ�ţ�";
            this.lblDept.Text = "���ң�";
            this.lblFeeDate.Text = "ͳ�����ڣ�";
            this.lblName.Text = "������";
            this.lblSex.Text = "�Ա�";
            //this.lblTitleName.Text = "סԺҽ�������嵥(��ϸ��";
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

            int lbhospitalLocalX = this.DrawingMargins.Left + this.lbhospital.Location.X;// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
            int lbhospitalLoaclY = this.DrawingMargins.Top + this.lbhospital.Location.Y;
            graphics.DrawString(this.lbhospital.Text, this.lbhospital.Font, new SolidBrush(this.lbhospital.ForeColor), lbhospitalLocalX, lbhospitalLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblDept.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblDept.Location.Y;
            graphics.DrawString(this.lblDept.Text, this.lblDept.Font, new SolidBrush(this.lblDept.ForeColor), additionTitleLocalX, additionTitleLocalY);

           
            additionTitleLocalX = this.DrawingMargins.Left + this.lblPatientNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPatientNo.Location.Y;
            graphics.DrawString(this.lblPatientNo.Text, this.lblPatientNo.Font, new SolidBrush(this.lblPatientNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;
            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            
            additionTitleLocalX = this.DrawingMargins.Left + this.lblFeeDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblFeeDate.Location.Y;
            graphics.DrawString(this.lblFeeDate.Text, this.lblFeeDate.Font, new SolidBrush(this.lblFeeDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

           
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

            #region Farpoint���� {2961D24B-E987-4ed5-B2B6-373847410ED2}
            int drawingWidth = 850 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 1100 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

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
        /// <summary>
        /// ��ӡ��ѡ��{901FA638-3A3D-497e-9BE2-7E1EB5512215}
        /// </summary>
        /// <returns></returns>
        private string ChoosePrinter()
        {
            FS.HISFC.Components.Common.Forms.frmChoosePrinter frmPrint = new FS.HISFC.Components.Common.Forms.frmChoosePrinter();
            frmPrint.Init();
            frmPrint.StartPosition = FormStartPosition.CenterParent;
            frmPrint.ShowDialog();

            //{3E7EFECA-5375-420b-A435-323463A0E56C}
            //����û�ȡ�����򷵻ؿ�ֵ
            if (frmPrint.DialogResult == DialogResult.Cancel)
            {
                return string.Empty;
            }

            return frmPrint.PrinterName;
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)//{B00AF53E-2C65-4a21-8598-4DE4C851626C}
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
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            this.PrintDocument.PrinterSettings.PrinterName = ChoosePrinter();
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
                MessageBox.Show("��ӡ������" + ex.Message);
            }
        }

        /// <summary>
        /// ��ӡҳ��ѡ��{B00AF53E-2C65-4a21-8598-4DE4C851626C}
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = 850 - this.DrawingMargins.Left - this.DrawingMargins.Right;// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
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

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public int PrintPreview()
        {
            System.Drawing.Printing.PaperSize paperSize = null;
            this.SetPaperSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintView();
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

        protected override void OnPrint(PaintEventArgs e)
        {
            PrintView();
            base.OnPrint(e);
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

        public void SetBillYBFY(decimal total, decimal sbzfje, decimal grzfje)
        {
            this.total = total;
            this.sbzfje = sbzfje;
            this.grzfje = grzfje;
        }
        #endregion


        #region IInpatientBill ��Ա������ʱ��

        /// <summary>
        /// �ṩ����ѡ���ӡ��Χ�Ĵ�ӡ����
        /// </summary>
        public int PrintPage()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPreview();

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            return 1;
        }


        #endregion

        #region IBillPrint ��Ա

        /// <summary>
        /// ����{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
        /// <param name="patient"></param>
        /// <returns></returns>
        //public int Export()
        //{
        //    try
        //    {
        //        string fileName = "";
        //        SaveFileDialog dlg = new SaveFileDialog();
        //        dlg.DefaultExt = ".xls";
        //        dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
        //        DialogResult result = dlg.ShowDialog();
        //        if (result == DialogResult.OK)
        //        {
        //            fileName = dlg.FileName;

        //            fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);

        //            MessageBox.Show("��ܰ��ʾ", "�����ɹ�");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("�������ݷ�������>>" + ex.Message);
        //        return -1;
        //    }

        //    return 0;
        //}

        #endregion

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        #region IBillPrint ��Ա

        /// <summary>
        /// ��Ҫ����������Դ��ֵ{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int ShowData(DataTable dt, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            fpSpread1.DataSource = dt;// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
            nowpatient = patient;
            return 1;
            //throw new NotImplementedException();
        }

        #endregion

        private void fpSpread1_CellClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }
    }
}
