using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.InPatient
{
    /// <summary>
    /// [功能描述: 住院药房明细单打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、作为一个例子保留下来，不要更改
    /// 2、各项目如果修改不大的话，可以考虑继承方式
    /// </summary>
    public partial class ucDetailDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// 明细打印摆药单
        /// </summary>
        public ucDetailDrugBill()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
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
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();
        #endregion

        #region 摆药单的通用方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.nlbTitle.Text = "住院药房明细摆药单";
            this.nlbRowCount.Text = "记录数：";
            this.nlbBillNO.Text = "单据号：";
            this.nlbStockDept.Text = "发药科室：";
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
                this.maxPageNO = 1;
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 标题绘制
            int mainTitleLocalX = this.DrawingMargins.Left + this.nlbTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.nlbTitle.Location.Y;
            graphics.DrawString(this.nlbTitle.Text, this.nlbTitle.Font, new SolidBrush(this.nlbTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.nlbNurseCell.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.nlbNurseCell.Location.Y;

            graphics.DrawString(this.nlbNurseCell.Text, this.nlbNurseCell.Font, new SolidBrush(this.nlbNurseCell.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbBillNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbBillNO.Location.Y;

            graphics.DrawString(this.nlbBillNO.Text, this.nlbBillNO.Font, new SolidBrush(this.nlbBillNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbStockDept.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbStockDept.Location.Y;

            graphics.DrawString(this.nlbStockDept.Text, this.nlbStockDept.Font, new SolidBrush(this.nlbStockDept.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;

            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
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
                maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        /// <summary>
        /// 设置列
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");

        }

        /// <summary>
        /// 这实在没有意义，和汇总单统一罢了
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        private void ShowBillData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowDetailData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            //单元格线形
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder noneBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);

            string applyDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            string sendType = this.GetSendType(alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut);

            if (drugBillClass.ID == "R")
            {
                this.nlbTitle.Text = "退药单";
            }
            else if (drugBillClass.ID == "O")
            {
                this.nlbTitle.Text = "出院带药";
            }
            else
            {
                this.nlbTitle.Text = applyDeptName + drugBillClass.Name + "(明细)" + "(" + sendType + ")";
            }

            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width)/2,this.nlbTitle.Location.Y);

            this.nlbRowCount.Text = "记录数：" + alData.Count.ToString();
            this.nlbBillNO.Text = "单据号：" + drugBillClass.DrugBillNO;
            this.nlbStockDept.Text = "发药科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);
            this.nlbNurseCell.Text = "病区：" + applyDeptName;

            //#region 对同一医嘱按用药时间组合显示

            if (drugBillClass.ID != "O"&&drugBillClass.ID!="P")
            {
                try
                {
                    alData.Sort(new CompareApplyOutByOrderNO());
                }
                catch { }
            }
            


            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();

            string orderId = "";//必须得按医嘱流水号排序 
            FS.HISFC.Models.Pharmacy.ApplyOut objLast = null;
            System.Collections.Hashtable hsFrequenceCount = new Hashtable();

            //合并，计算服药次数
            for (int i = alData.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = (alData[i] as FS.HISFC.Models.Pharmacy.ApplyOut);
                obj.User01 = FS.FrameWork.Function.NConvert.ToInt32(!SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(obj.StockDept.ID, obj.Item.ID)).ToString();
                bool needAdd = false;
                if (hsFrequenceCount.Contains(obj.OrderNO))
                {
                    int count = (int)hsFrequenceCount[obj.OrderNO];
                    count = count + 1;
                    if ((count > this.GetFrequencyCount(obj.Frequency.ID)) && obj.OrderType.ID == "CZ" && drugBillClass.ID != "R")
                    {
                        needAdd = true;
                    }
                    if (count == this.GetFrequencyCount(obj.Frequency.ID) + 1)
                    {
                        hsFrequenceCount[obj.OrderNO] = 1;
                    }
                    else
                    {
                        hsFrequenceCount[obj.OrderNO] = count;
                    }
                }
                else
                {
                    int count = 1;
                    hsFrequenceCount[obj.OrderNO] = count;
                }

                if (orderId == "")
                {
                    orderId = obj.OrderNO;
                    objLast = obj;
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.UseTime), dt);
                }
                else if (orderId == obj.OrderNO && !needAdd)//是一个药
                {
                    //此下3行导致明细摆药单相同药品合并 不知道干啥用的 先屏蔽 BY FZC 2014-10-03
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(obj.UseTime), dt) + " " + objLast.User03;
                    objLast.Operation.ApplyQty += obj.Operation.ApplyQty * obj.Days;//数量相加
                    alData.RemoveAt(i);
                    //obj.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(obj.UseTime), dt);
                }
                else
                {
                    orderId = obj.OrderNO;
                    objLast = obj;
                    if (needAdd)
                    {
                        obj.Frequency.Name = "另加";
                    }
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.UseTime), dt);
                }
                if (obj.OrderType.ID != "CZ")
                {
                    objLast.User03 = "";
                }
            }

            //#endregion

            #region 按患者排序

            if (drugBillClass.ID != "O"&&drugBillClass.ID != "P")
            {
                //CompareApplyOutByPatient com2 = new CompareApplyOutByPatient();
                //by han-zf 2014-10-25 中心药房摆药单补打报错
                try
                {
                    //alData.Sort(new CompareApplyOutByOrderNO());
                    alData.Sort(new CompareApplyOutByPatient());
                }
                catch { }
               
            }
            #endregion

            this.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

            //患者姓名
            string privPatientName = "";
            string samePatient = "";
            //行数
            int iRow = 0;
            //患者床号 姓名可以和药品同行，这个决定是否要新增一行显示药品
            //bool isNeedAddRow = true;

            #region 设置数据
            string patientInfo = "";
            this.nlbRowCount.Text = "记录数：" + alData.Count.ToString();

            if (drugBillClass.ID != "O"&&drugBillClass.ID != "P")
            {
                try
                {
                    CompareApplyOutByOrderNO com1 = new CompareApplyOutByOrderNO();
                    alData.Sort(com1);
                }
                catch { };
            }
            DateTime dtFirstPrintTime = DateTime.MinValue;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
                {
                    this.nlbReprint.Visible = false;
                    if (!this.nlbTitle.Text.Contains("补打"))
                    {
                        this.nlbTitle.Text = this.nlbReprint.Text + this.nlbTitle.Text;
                    }
                }
                else
                {
                    this.nlbReprint.Visible = false;
                }
                dtFirstPrintTime = info.Operation.ExamOper.OperTime;
                string bedNO = info.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }

                FS.HISFC.Models.RADT.PatientInfo p = inpatientManager.GetPatientInfoByPatientNO(info.PatientNO);

                if (drugBillClass.ID == "R")
                {
                    
                    if (p != null && p.PVisit.InState.ID.ToString() == "2")
                    {
                        bedNO = "*" + bedNO;
                    }
                }
                //患者不同时需要插入一行患者信息
                if (samePatient != info.PatientNO)
                {
                    string age = "";
                    try
                    {
                        age = inpatientManager.GetAge(inpatientManager.GetPatientInfoByPatientNO(info.PatientNO).Birthday);
                    }
                    catch { }

                    privPatientName = info.PatientName;
                    samePatient = info.PatientNO;
                    patientInfo = string.Format("{0}  {1}住院号：{2}   年龄：{3}", bedNO, SOC.Public.String.PadRight(privPatientName, 8, ' '), p.PID.PatientNO, age);
                    //如果不是在本页的第一行，需要患者信息
                    if (iRow % this.pageRowNum != 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                        this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        iRow++;
                    }
                }

                //每页的第一行都需要患者信息
                if (iRow % this.pageRowNum == 0)
                {
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);

                    this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    iRow++;

                }
                //药品信息
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                string valueable = string.Empty;
                string morethandoseonce = string.Empty;
                if (info.OrderType.ID == "CZ")
                {
                    valueable = "+";
                }
                else
                {
                    valueable = "  ";
                }
                if (!FS.FrameWork.Function.NConvert.ToBoolean(info.User01))
                {
                    valueable += "●";
                }

                int times = info.User03.Split(' ').Length;
                
                if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).OnceDose > info.DoseOnce)
                {
                    morethandoseonce = "△";
                }
                this.neuSpread1.SetCellValue(0, iRow, "货位号", info.PlaceNO);
                this.neuSpread1.SetCellValue(0, iRow, "编码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                this.neuSpread1_Sheet1.Cells[iRow, 2].ColumnSpan = 3;
                this.neuSpread1.SetCellValue(0, iRow, "床号", valueable + info.Item.Name);
                this.neuSpread1.SetCellValue(0, iRow, "规格", info.Item.Specs);
                if (drugBillClass.ID != "R")
                {
                    if (string.IsNullOrEmpty(info.Usage.Name))
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "用法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
                    }
                    else
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "用法", info.Usage.Name);
                    }
                    //频度
                    try
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "频次", info.Frequency.ID.ToLower());
                        if (info.Frequency.Name == "另加")
                        {
                            this.neuSpread1.SetCellValue(0, iRow, "频次", "另" + info.Frequency.ID.ToLower());
                        }
                    }
                    catch { }
                    //this.neuSpread1.SetCellValue(0, iRow, "每次用量", info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit);
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Text = morethandoseonce + info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                    this.neuSpread1_Sheet1.Columns[8].Label = "每次用量";
                }

                //总量
                decimal applyQty = info.Operation.ApplyQty * info.Days;
                string unit = info.Item.MinUnit;
                decimal price = 0m;

                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
                if (string.IsNullOrEmpty(info.Item.PackUnit))
                {
                    if (info.Item.PackQty == 1)
                    {
                        info.Item.PackUnit = info.Item.MinUnit;
                    }
                    else
                    {
                        try
                        {
                            info.Item.PackUnit = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).PackUnit;
                        }
                        catch { }
                    }
                }
                if (outPackQty == 0)
                {
                    applyQty = info.Operation.ApplyQty * info.Days;
                    unit = info.Item.MinUnit;
                    price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 6);
                }
                else if (outMinQty == 0)
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit;
                    price = info.Item.PriceCollection.RetailPrice;
                }
                else
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit + outMinQty.ToString() + info.Item.MinUnit;
                    price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 6);
                }

                if (info.OrderType.ID == "CZ" && SOC.HISFC.BizProcess.Cache.Order.GetFrequency(info.Frequency.ID).Times.Length != times)
                {
                    unit = unit + "△";
                }
                this.neuSpread1.SetCellValue(0, iRow, "总量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + " " + unit);

                //服药次数
                if (drugBillClass.ID == "R")
                {
                    this.neuSpread1.SetCellValue(0, iRow, "使用时间", "");
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, iRow, "使用时间", info.User03);
                    this.neuSpread1.SetCellValue(0, iRow, "备注", info.Memo);
                }
                this.neuSpread1.SetCellValue(0, iRow, "单价", price.ToString("F4").TrimEnd('0').TrimEnd('.'));

                this.neuSpread1.SetCellValue(0, iRow, "金额", (info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F4").TrimEnd('0').TrimEnd('.'));


                for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells.Get(iRow, i).Border = noneBorder;
                }

                iRow++;
            }
            #endregion

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            #region 设置底部文字
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    int index = this.neuSpread1_Sheet1.Rows.Count;
                    totPageNO = (int)Math.Ceiling((double)index / pageRowNum);
                    for (int page = totPageNO; page > 0; page--)
                    {
                        if (page == totPageNO)
                        {

                            this.neuSpread1_Sheet1.AddRows(index, 2);
                            //打印单底部文字
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "   执行配药师：                            核对护士：                         ";
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("宋体", 10f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                            this.neuSpread1_Sheet1.Cells[index + 1, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                            this.neuSpread1_Sheet1.Cells[index + 1, 0].Text = "配药时间：" + dtFirstPrintTime.ToString() + "                " + "打印时间：" + DateTime.Now;
                            this.neuSpread1_Sheet1.Cells[index + 1, 0].Font = new Font("宋体", 10f);
                            this.neuSpread1_Sheet1.Cells[index + 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                            //标记页码，补打选择页码时用
                            this.neuSpread1_Sheet1.Rows[index].Tag = page;
                            continue;
                        }

                        //标记页码，补打选择页码时用
                        this.neuSpread1_Sheet1.Rows[index].Tag = page;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
            FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 0).Border = lineBorder3;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.ResumeLayout(true);
        }

        /// <summary>
        /// 获取发送类型
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetSendType(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string sendType = string.Empty;
            switch (applyOut.SendType)
            { 
                case 1:
                    sendType = "集中";
                    break;
                case 2:
                    sendType = "临时";
                    break;
                case 4:
                    sendType = "紧急";
                    break;
            }
            return sendType;
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("InPatientDrugBillD", 870, 550);
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

        /// <summary>
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = 870- this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;

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
        private void PrintPage()
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

        }

        #endregion

        #region 明细单的特殊方法

        /// <summary>
        /// 获取频次代表的每天次数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            return 1000;

            //南庄不分行
            if (string.IsNullOrEmpty(frequencyID))
            {
                return 1000;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//每天一次
            {
                return 1;
            }
            else if (id == "bid")//每天两次
            {
                return 2;
            }
            else if (id == "tid")//每天三次
            {
                return 3;
            }
            else if (id == "hs")//睡前
            {
                return 1;
            }
            else if (id == "qn")//每晚一次
            {
                return 1;
            }
            else if (id == "qid")//每天四次
            {
                return 4;
            }
            else if (id == "pcd")//晚餐后
            {
                return 1;
            }
            else if (id == "pcl")//午餐后
            {
                return 1;
            }
            else if (id == "pcm")//早餐后
            {
                return 1;
            }
            else if (id == "prn")//必要时服用
            {
                return 1;
            }
            else if (id == "遵医嘱")
            {
                return 1;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                return 100;
            }
        }

        /// <summary>
        /// 按用药时间/当前时间 组合显示
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        private string FormatDateTime(DateTime dt, DateTime sysdate)
        {
            try
            {
                if (sysdate.Date.AddDays(-1) == dt.Date)
                {
                    return "昨" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date == dt.Date)
                {
                    return "今" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date.AddDays(1) == dt.Date)
                {
                    return "明" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date.AddDays(2) == dt.Date)
                {
                    return "后" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else
                {
                    if (dt.Month == sysdate.Month)
                    {
                        return dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');

                    }
                }
            }
            catch
            {
                return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
            }
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
            this.Clear();
            this.SetFormat();
            this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");
        }

        /// <summary>
        /// 提供没有范围选择的打印
        /// 一般在摆药保存时调用
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        public void PrintData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            this.PrintPage();
        }

        #endregion

        #region 排序类
        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOutByPatient : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            //public int Compare(object x, object y)
            //{
            //    FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
            //    FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

            //    string oX = "";          //患者姓名
            //    string oY = "";          //患者姓名


            //    oX = o1.BedNO + o1.PatientName + this.GetFrequencySortNO(o1.Frequency) + o1.UseTime.ToString();
            //    oY = o2.BedNO + o2.PatientName + this.GetFrequencySortNO(o2.Frequency) + o2.UseTime.ToString(); 

            //    return string.Compare(oX, oY);
            //}
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名

                //FZC ADD 临嘱排在前面
                #region

                string oo1 = string.Empty;
                string oo2 = string.Empty;
                if (o1.OrderType.ID == "CZ")
                {
                    oo1 = "2";
                }
                else
                {
                    oo1 = "1";
                }

                if (o2.OrderType.ID == "LZ")
                {
                    oo2 = "1";
                }
                else
                {
                    oo2 = "2";
                }

                #endregion

                oX = o1.BedNO + o1.PatientName + oo1 + o1.User01 + this.GetFrequencySortNO(o1.Frequency) + this.GetOrderNo(o1) + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + oo2 + o2.User01 + this.GetFrequencySortNO(o2.Frequency) + this.GetOrderNo(o2) + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }

            private string GetOrderNo(FS.HISFC.Models.Pharmacy.ApplyOut app)
            {
                string id = app.Item.ID.ToString();
                return id;
            }
            private string GetFrequencySortNO(FS.HISFC.Models.Order.Frequency f)
            {
                string id = f.ID.ToLower();
                string sortNO = "";
                if (id == "qd")
                {
                    sortNO = "1";
                }
                else if (id == "bid")
                {
                    sortNO = "2";
                }
                else if (id == "tid")
                {
                    sortNO = "3";
                }
                else
                {
                    sortNO = "4";
                }
                if (f.Name == "另加")
                {
                    sortNO = "9999" + sortNO;
                }
                else
                {
                    sortNO = "0000" + sortNO;
                }
                return sortNO;
            }

        }

        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOutByOrderNO : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = new FS.HISFC.Models.Pharmacy.ApplyOut();
                FS.HISFC.Models.Pharmacy.ApplyOut o2  = new FS.HISFC.Models.Pharmacy.ApplyOut();
                if((x is  FS.HISFC.Models.Pharmacy.ApplyOut)&&(y is FS.HISFC.Models.Pharmacy.ApplyOut))
                {
                    o1 =  (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                    o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                }
                string oX = "1";          //患者姓名
                string oY = "1";          //患者姓名

                //FZC ADD 临嘱排在前面
                #region

                string oo1 = string.Empty;
                string oo2 = string.Empty;
                if (o1.OrderType != null &&o1.OrderType.ID == "CZ")
                {
                    oo1 = "2";
                }
                else
                {
                    oo1 = "1";
                }

                if (o1.OrderType != null && o2.OrderType.ID == "LZ")
                {
                    oo2 = "1";
                }
                else
                {
                    oo2 = "2";
                }

                #endregion

                oX = o1.BedNO + o1.PatientName + oo1 + o1.OrderNO + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + oo2 + o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }
        }

        private class CompareApplyOutByValuableOrderNO : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名


                oX = o1.User01 + o1.OrderNO + o1.UseTime.ToString();
                oY = o2.User01 + o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }
        }

        #endregion

        #region IInpatientBill 成员，补打时用

        /// <summary>
        /// 提供摆药单数据显示的方法
        /// 一般在摆药单补打时调用
        /// </summary>
        /// <param name="alData">出库申请applyout</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">库存科室</param>
        public void ShowData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 提供可以选择打印范围的打印方法
        /// </summary>
        public void Print()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPage();

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
        }

        /// <summary>
        /// 设置Dock属性，补打时用
        /// </summary>
        public DockStyle WinDockStyle
        {
            get
            {
                return this.Dock;
            }
            set
            {
                this.Dock = value;
            }
        }

        /// <summary>
        /// 单据类型，补打时用
        /// </summary>
        public SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细;
            }
        }

        #endregion
    }
}
