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
    /// [功能描述: 清单明细]<br></br>
    /// [创 建 者: lfhm]<br></br>
    /// [创建时间: 2017-10]<br></br>
    /// </summary>
    public partial class ucInPatientFeeTotalBillByCI : UserControl, FS.HISFC.BizProcess.Interface.RADT.IBillPrint
    {
        /// <summary>
        /// 清单明细
        /// </summary>
        public ucInPatientFeeTotalBillByCI()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.Clear();
        }

        #region 变量


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
        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();


        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();
        
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            
        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);
        
        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
        
        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();


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
        
        #endregion

        #region 信息设置

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


            FS.HISFC.BizLogic.Fee.InPatient patientBll = new FS.HISFC.BizLogic.Fee.InPatient();//{2044475C-E8CE-454B-B328-90EAAC174D1A} 获取从入院登记到入科最大日期
            string visitDate = patientBll.GetMaxKDate(patient.ID);
            this.lblAge.Text += this.outOrderMgr.GetAge(patient.Birthday, false);
            this.lblBedNo.Text += patient.PVisit.PatientLocation.Bed.ID.Length > 4 ? patient.PVisit.PatientLocation.Bed.ID.Substring(4) : patient.PVisit.PatientLocation.Bed.ID;
            if (patient.PVisit.OutTime >= DateTime.MaxValue || patient.PVisit.OutTime <= DateTime.MinValue)
            {
                this.lblDays.Text += "患者未做出院登记";
            }
            else
            {
               
                int days =0;
                if (!string.IsNullOrEmpty(visitDate))//{2044475C-E8CE-454B-B328-90EAAC174D1A} 获取从入院登记到入科最大日期
                {
                    days = this.Days(Convert.ToDateTime(visitDate), patient.PVisit.OutTime);
                }
                else
                    days = this.Days(patient.PVisit.InTime, patient.PVisit.OutTime);
                this.lblDays.Text += days;
            }
            //string deptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(patient.ApplyDept.ID);

            this.lblDept.Text += patient.PVisit.PatientLocation.Dept.Name;

            this.nlbRowCount.Text += rowCount;

            if (!string.IsNullOrEmpty(visitDate))////{2044475C-E8CE-454B-B328-90EAAC174D1A} 获取从入院登记到入科最大日期
            {
                this.lblFeeDate.Text += Convert.ToDateTime(visitDate).ToShortDateString() + "至" + patient.PVisit.OutTime.ToShortDateString();
            }
            else
                this.lblFeeDate.Text += patient.PVisit.InTime.ToShortDateString() + "至" + patient.PVisit.OutTime.ToShortDateString();
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
            this.nlblCardNo.Text += patient.PID.CardNO;////{2044475C-E8CE-454B-B328-90EAAC174D1A}费用汇总清单加入卡号
            if (patient.Sex.ID.ToString() == "F")
            {
                this.lblSex.Text += "女";
            }
            else if (patient.Sex.ID.ToString() == "M")
            {
                this.lblSex.Text += "男";
            }
            else
            {
                this.lblSex.Text += "保密";
            }
            Dictionary<string, List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>> drugDictionary = new Dictionary<string,List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>>();

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
            //添加优惠 赠送金额总合计变量{18653415-C332-4a7f-8772-A1559E5FA88A}
            decimal alldonateCost = 0m;
            decimal alldereateCost = 0m;

            foreach (string recipeNO in drugDictionary.Keys)
            {

                List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeList = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
                feeList = drugDictionary[recipeNO];

                FS.HISFC.Models.Fee.Inpatient.FeeInfo oneFeeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                oneFeeInfo = drugDictionary[recipeNO][0];

                iRow++;
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 2;
                this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 7; //{282B4236-FFD4-4add-BB53-21B438EAFCDB}
                this.neuSpread1_Sheet1.Cells[iRow, 0].Text = oneFeeInfo.User01;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Border = bottomAndtopBorder;
                int currInfoLan = iRow;
                decimal totCost = 0m;
                //添加优惠 赠送金额分组合计变量{18653415-C332-4a7f-8772-A1559E5FA88A}
                decimal donateCost = 0m;
                decimal dereateCost = 0m;
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
                    //添加优惠 赠送金额{18653415-C332-4a7f-8772-A1559E5FA88A}
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Text = obj.FT .DonateCost.ToString ("F2");
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Text = obj.FT.DerateCost.ToString("F2");
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Text = obj.Item.User02.ToString(); ////{282B4236-FFD4-4add-BB53-21B438EAFCDB}
                    totCost += obj.FT.TotCost;
                    donateCost += obj.FT.DonateCost;
                    dereateCost += obj.FT.DerateCost;
                        

                }
                //添加优惠 赠送金额分组合计{18653415-C332-4a7f-8772-A1559E5FA88A}//优惠金额与赠送金额合并为减免 添加折后价 金额更改为原价{275EF519-39A8-4c40-AE81-288EE9DEE944}
                allTotCost += totCost;
                alldereateCost += dereateCost;
                alldonateCost += donateCost;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Text = "合计：" + totCost.ToString("F2") + "  折后价:" + donateCost.ToString("F2") + "  商保:" + dereateCost.ToString("F2");
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[currInfoLan, 2].Border = bottomAndtopBorder;


            }
            //设置最后一行

            iRow++;
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
            //添加优惠 赠送金额 总合计{18653415-C332-4a7f-8772-A1559E5FA88A}//优惠金额与赠送金额合并为减免 添加折后价 金额更改为原价{275EF519-39A8-4c40-AE81-288EE9DEE944}
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "总合计：" + allTotCost.ToString("F2") + "  折后价:" + alldonateCost.ToString("F2") + "  商保:" + alldereateCost.ToString("F2");
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder; ////{282B4236-FFD4-4add-BB53-21B438EAFCDB}


            iRow++;
            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 7;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = "制表人：" + FS.FrameWork.Management.Connection.Operator.Name;
            this.neuSpread1_Sheet1.Cells[iRow, 2].Text = "制表日期：" + orderMgr.GetDateTimeFromSysDateTime().ToLongDateString();
            this.neuSpread1_Sheet1.Cells[iRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
            this.neuSpread1_Sheet1.Cells[iRow, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[iRow, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[iRow, 2].Border = topBorder;
            this.neuSpread1_Sheet1.Cells[iRow, 2].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           


            return 1;
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
            this.nlbRowCount.Text = "记录数：";
            this.lblInvoiceNO.Text = "票据号：";//把发票号更改为票据号
            this.lblPatientNo.Text = "住院号：";
            this.lblAge.Text = "年龄：";
            this.lblBedNo.Text = "床号：";
            this.lblDays.Text = "天数：";
            this.lblDept.Text = "科室：";
            this.lblFeeDate.Text = "统计日期：";
            this.lblName.Text = "姓名：";
            this.lblPact.Text = "结算方式：";
            this.lblSex.Text = "性别：";
            this.lblTitleName.Text = "住院费用清单(商保）";
            this.nlblCardNo.Text = "就诊卡号：";
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

            #region Farpoint绘制
            int drawingWidth = 1000 - this.DrawingMargins.Left - this.DrawingMargins.Right;
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

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize paperSize1 = pageSizeMgr.GetPageSize("A4");
                paperSize = new System.Drawing.Printing.PaperSize("A4", paperSize1.Width, paperSize1.Height);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName,paperSize.Width, paperSize.Height);
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
                MessageBox.Show("打印机报错！" + ex.Message);
            }
        }

        /// <summary>
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {
            int drawingWidth = 1000 - this.DrawingMargins.Left - this.DrawingMargins.Right;
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
        /// 打印
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
                    //protected
                    string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();//看是否需要选打印机  //{C3F605B6-9281-4068-98BB-393ADE0DF06C}
                    if (string.IsNullOrEmpty(printerName))
                    {
                        return 1;
                    }
                    this.PrintDocument.PrinterSettings.PrinterName = printerName;
                    this.PrintDocument.Print();
                }
            }


            return 1;

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

        #endregion


        #region IInpatientBill 成员，补打时用

        /// <summary>
        /// 提供可以选择打印范围的打印方法
        /// </summary>
        private void PrintPage()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPreview();

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
        }


        #endregion

        #region IBillPrint 成员

        /// <summary>
        /// 导出{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int Export(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt =".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                dlg.FileName = patient.Name + "住院费用清单汇总";
                DialogResult result = dlg.ShowDialog();
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

        /// <summary>
        /// 给要导出控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int ShowData(DataTable dt, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            fpSpread1.DataSource = dt;
            return 1;
            //throw new NotImplementedException();
        }

        #endregion
    }
}
