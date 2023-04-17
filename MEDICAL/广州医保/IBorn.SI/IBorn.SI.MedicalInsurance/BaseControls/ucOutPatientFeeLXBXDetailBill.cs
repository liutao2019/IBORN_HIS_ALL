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
    /// [功能描述: 门诊零星报销清单汇总清单本地实例化]<br></br>
    /// [创 建 者: lzd]<br></br>
    /// [创建时间: 2020-08]<br></br>
    /// {130C7CAA-7CB1-49b9-84DC-BE0775140BF3}
    /// </summary>
    public partial class ucOutPatientFeeLXBXDetailBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 汇总清单
        /// </summary>
        public ucOutPatientFeeLXBXDetailBill()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.exportDatable = CreateDataTable();// {2961D24B-E987-4ed5-B2B6-373847410ED2}
            this.Clear();
        }

        #region 变量

        DataTable exportDatable = new DataTable();// {2961D24B-E987-4ed5-B2B6-373847410ED2}
        FS.HISFC.Models.RADT.PatientInfo pinfo;
        /// <summary>
        /// 每页的行数，这个是按照LetterpageRowNum，行高改变影响分页
        /// </summary>
        int pageRowNum = 14;

        /// <summary>
        /// 最大页码
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// 打印的有效行数,当选择页码范围时有效
        /// </summary>
        int validRowNum = 0;

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


        //FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        //private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 常数业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();





        #endregion

        #region 医保价格合计信息
        public decimal total, sbzfje, grzfje;

        #endregion
        /// <summary>
        /// 计算天数
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
        #region 信息设置

        public int ShowBill(ArrayList alData, FS.HISFC.Models.RADT.PatientInfo pinfo,string dept)
        {
            this.Clear();
            if (alData.Count <= 0 || alData == null)
            {
                return -1;
            }
            if (pinfo == null)
            {
                return -1;
            }
            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder bottomAndtopBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);


            this.pinfo = pinfo;
            this.lblAge.Text += this.constantMgr.GetAge(pinfo.Birthday, false);
            this.lblDept.Text += dept;//.Templet.Dept.Name
           // this.lbclincode.Text += register.ID;
            this.lblName.Text += pinfo.Name;
            this.lblPact.Text += pinfo.Pact.Name;
            this.lblPatientNo.Text += pinfo.PID.CardNO;
            if (pinfo.Sex.ID.ToString() == "F")
            {
                this.lblSex.Text += "女";
            }
            else if (pinfo.Sex.ID.ToString() == "M")
            {
                this.lblSex.Text += "男";
            }
            else
            {
                this.lblSex.Text += "保密";
            }
            Dictionary<string, List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>> drugDictionary = new Dictionary<string, List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>>();

            //按费用类别分类
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
                this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 4;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Text = oneFeeInfo.User01;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Text = obj.User01.ToString();
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Text = obj.FeeOper.OperTime.ToString("yyyy-MM-dd");
                    totCost += obj.FT.TotCost;
                    //{2961D24B-E987-4ed5-B2B6-373847410ED2}
                    exportDatable.Rows.Add(new object[] { iRow.ToString(), obj.FeeOper.OperTime.ToString("yyyy-MM-dd"), obj.Item.Name, obj.Item.Specs, obj.Item.Price.ToString("F2"), obj.Item.Qty.ToString(), obj.Item.PriceUnit, obj.FT.TotCost.ToString("F2"), obj.User01.ToString() });
                }
                allTotCost += totCost;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Text = "合计：" + totCost.ToString("F2");
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Border = bottomAndtopBorder;


            }
            //设置最后一行

            iRow++;
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "总合计：" + allTotCost.ToString("F2"); //+ " 统筹支付金额：" + sbzfje.ToString("F2") + " 个人支付金额：" + grzfje.ToString("F2");
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
            // {2961D24B-E987-4ed5-B2B6-373847410ED2}

            //iRow++;
            //this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            //this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 2;
            //this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 4;
            //this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "制表人：" + FS.FrameWork.Management.Connection.Operator.Name;
            //this.neuSpread1_Sheet1.Cells[iRow, 2].Text = "制表日期：" + constantMgr.GetDateTimeFromSysDateTime().ToLongDateString();
            //this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
            //this.neuSpread1_Sheet1.Cells[iRow, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells[iRow, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells[iRow, 2].Border = topBorder;
            //this.neuSpread1_Sheet1.Cells[iRow, 2].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            

            return 1;
        }
        // {2961D24B-E987-4ed5-B2B6-373847410ED2}
        private DataTable  CreateDataTable()
        {
            DataTable tblDatas = new DataTable("FeeTable");
            tblDatas.Columns.Add("序号", Type.GetType("System.String"));
            tblDatas.Columns.Add("费用时间", Type.GetType("System.String"));
            tblDatas.Columns.Add("项目名称", Type.GetType("System.String"));
            //tblDatas.Columns[0].AutoIncrement = true;
            //tblDatas.Columns[0].AutoIncrementSeed = 1;
            //tblDatas.Columns[0].AutoIncrementStep = 1;

            tblDatas.Columns.Add("规格", Type.GetType("System.String"));
            tblDatas.Columns.Add("单价", Type.GetType("System.String"));
            tblDatas.Columns.Add("数量", Type.GetType("System.String"));
            tblDatas.Columns.Add("单位", Type.GetType("System.String"));
            tblDatas.Columns.Add("金额", Type.GetType("System.String"));
            tblDatas.Columns.Add("费用类别", Type.GetType("System.String"));
            return tblDatas;
            
        }

        public int Print()
        {
            return 1;
        }
        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.nlbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.nlbRowCount.Text = "";// "记录数：";
            this.lblInvoiceNO.Text = "";// "发票号：";
            this.lblPatientNo.Text = "医疗卡号：";
            this.lblAge.Text = "年龄：";
            // this.lblBedNo.Text = "床号：";
            //this.lblDays.Text = "天数：";
            this.lblDept.Text = "挂号科室：";
            //this.lblFeeDate.Text = "统计日期：";
            this.lblName.Text = "姓名：";
            this.lblPact.Text = "结算方式：";
            this.lblSex.Text = "性别：";
           // this.lbclincode.Text = "门诊号：";

            this.lblTitleName.Text = "门诊费用清单(明细）";
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //跳过选择打印范围外的数据
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

            #region 标题绘制
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

            //additionTitleLocalX = this.DrawingMargins.Left + this.lbclincode.Location.X;
            //additionTitleLocalY = this.DrawingMargins.Top + this.lbclincode.Location.Y;
            //graphics.DrawString(this.lbclincode.Text, this.lbclincode.Font, new SolidBrush(this.lbclincode.ForeColor), additionTitleLocalX, additionTitleLocalY);

            
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

            #region Farpoint绘制
            int drawingWidth = 850 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 1100 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

            #endregion

            #region 页码绘制

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPageNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPageNo.Location.Y;

            graphics.DrawString("页码：" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.nlbPageNo.Font, new SolidBrush(this.nlbPageNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region 分页
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
        /// 打印机选择
        /// </summary>
        /// <returns></returns>
        private  string ChoosePrinter()
        {
            FS.HISFC.Components.Common.Forms.frmChoosePrinter frmPrint = new FS.HISFC.Components.Common.Forms.frmChoosePrinter();
            frmPrint.Init();
            frmPrint.StartPosition = FormStartPosition.CenterParent;
            frmPrint.ShowDialog();

            //{3E7EFECA-5375-420b-A435-323463A0E56C}
            //如果用户取消，则返回空值
            if (frmPrint.DialogResult == DialogResult.Cancel)
            {
                return string.Empty;
            }

            return frmPrint.PrinterName;
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
        //{901FA638-3A3D-497e-9BE2-7E1EB5512215}
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
                MessageBox.Show("打印机报错！" + ex.Message);
            }
        }

        /// <summary>
        /// 打印页码选择
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

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        /// <summary>
        /// 打印
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

        #region 公用方法

        /// <summary>
        /// 初始化设置
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


        #region IInpatientBill 成员，补打时用

        /// <summary>
        /// 提供可以选择打印范围的打印方法
        /// </summary>
        public int PrintPage()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPreview();

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            return 1;
        }


        #endregion

        #region IBillPrint 成员

        /// <summary>
        /// 导出{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}// {2961D24B-E987-4ed5-B2B6-373847410ED2}
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
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                dlg.FileName = pinfo.Name + "门诊费用明细清单";
                DialogResult result = dlg.ShowDialog();
                this.fpSpread1.DataSource = exportDatable;
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    MessageBox.Show("导出成功", "温馨提示");
                    //this.ShowBalloonTip(5000, "温馨提示", "导出成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出数据发生错误>>" + ex.Message);
                return -1;
            }
            return 1;
            // throw new NotImplementedException();
        }

        #endregion

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        #region IBillPrint 成员

        ///// <summary>
        ///// 给要导出的数据源赋值{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <returns></returns>
        //public int ShowData(DataTable dt, FS.HISFC.Models.RADT.PatientInfo patient)
        //{
        //    fpSpread1.DataSource = dt;
        //    return 1;
        //    //throw new NotImplementedException();
        //}

        #endregion

        private void fpSpread1_CellClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

         /// <summary>
        /// 给要导出的数据源赋值{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}// {2961D24B-E987-4ed5-B2B6-373847410ED2}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int ShowData(DataTable dt, FS.HISFC.Models.RADT.PatientInfo pinfo)
        {
            fpSpread1.DataSource = dt;// {75ECD815-8FC2-4f58-8E64-A5BD928D3935}
            this.pinfo = pinfo;
            return 1;
            //throw new NotImplementedException();
        }

       
    }
}
