using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN
{
    /// <summary>
    /// [功能描述: 住院药房明细单打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、作为一个例子保留下来，不要更改
    /// 2、各项目如果修改不大的话，可以考虑继承方式
    /// </summary>
    public partial class ucDetailDrugBillIBORN : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// 明细打印摆药单
        /// </summary>
        public ucDetailDrugBillIBORN()
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

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();
        #endregion

        #region 摆药单的通用方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.nlbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.nlbRowCount.Text = "记录数：";
            this.nlbBillNO.Text = "摆药单号：";
            this.lblOrderDate.Text = "医嘱时间：";
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

            if (this.nlbReprint.Visible)
            {
                int nlbReprintLocalX = this.DrawingMargins.Left + this.nlbReprint.Location.X;
                int nlbReprintLoaclY = this.DrawingMargins.Top + this.nlbReprint.Location.Y;
                graphics.DrawString(this.nlbReprint.Text, this.nlbReprint.Font, new SolidBrush(this.nlbReprint.ForeColor), nlbReprintLocalX, nlbReprintLoaclY);
            }

            int additionTitleLocalX = this.DrawingMargins.Left + this.nlbNurseCell.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.nlbNurseCell.Location.Y;

            graphics.DrawString(this.nlbNurseCell.Text, this.nlbNurseCell.Font, new SolidBrush(this.nlbNurseCell.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbBillNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbBillNO.Location.Y;

            graphics.DrawString(this.nlbBillNO.Text, this.nlbBillNO.Font, new SolidBrush(this.nlbBillNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblOrderDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOrderDate.Location.Y;

            graphics.DrawString(this.lblOrderDate.Text, this.lblOrderDate.Font, new SolidBrush(this.lblOrderDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;

            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
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
        /// 设置列
        /// </summary>
        //private void SetFormat()
        //{
        //    this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");

        //}

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

            #region farpoint设置
            FarPoint.Win.LineBorder noneBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder noRightBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder noBottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, false);

            #endregion


            string applyDeptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            string sendType = this.GetSendType(alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut);

            if (drugBillClass.ID == "R")
            {
                this.lblTitleName.Text = "退药单(明细)";
            }
            else if (drugBillClass.ID == "O")
            {
                this.lblTitleName.Text = "出院带药(明细)";
            }
            else if (drugBillClass.ID == "L" || drugBillClass.ID == "T" || drugBillClass.ID == "P" || drugBillClass.ID == "TL" || drugBillClass.ID == "A")// {F417D766-19C0-4d3e-AB72-D774058B497E}
            {
                this.lblTitleName.Text = "针剂/外用药摆药单(明细)";
            }
            else
            {
                this.lblTitleName.Text = drugBillClass.Name + "(明细)";//+ "(" + sendType + ")";
            }

            bool isPO = false;
            if (drugBillClass.Name.Contains("口服"))
            {
                isPO = true;
            }
            if (isPO)
            {
                this.neuSpread1_Sheet1.Columns.Get(8).Label = "服药频率";

            }
            else
            {
                //this.neuSpread1_Sheet1.Columns.Get(5).Label = "单价(元)";
            }
            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width) / 2, this.nlbTitle.Location.Y);
            this.lblTitleName.Location = new Point((this.Width - this.lblTitleName.Width) / 2, this.lblTitleName.Location.Y);

            this.nlbBillNO.Text = "摆药单号：" + drugBillClass.DrugBillNO;
            this.nlbNurseCell.Text = "病区：" + applyDeptName;

            //#region 对同一医嘱按用药时间组合显示

            if (drugBillClass.ID != "O" && drugBillClass.ID != "P")
            {
                try
                {
                    alData.Sort(new CompareApplyOutByOrderNO());
                }
                catch { }
            }


            #region 按患者排序


            #endregion

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();

            string orderId = "";//必须得按医嘱流水号排序 

            #region 设置数据

            int count = 0;

            Hashtable hsApplyInfo = new Hashtable();

            ArrayList difPatient = new ArrayList();

            ArrayList allOrderDate = new ArrayList();

            ArrayList patientData = new ArrayList();
            //按患者分组申请信息
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {
                if (hsApplyInfo.Contains(applyInfo.PatientNO))
                {
                    ArrayList alPatientData = hsApplyInfo[applyInfo.PatientNO] as ArrayList;
                    alPatientData.Add(applyInfo);
                }
                else
                {
                    ArrayList alPatientData = new ArrayList();
                    alPatientData.Add(applyInfo);
                    hsApplyInfo.Add(applyInfo.PatientNO, alPatientData);
                    difPatient.Add(applyInfo.PatientNO);
                }

                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                order = orderMgr.QueryOneOrder(applyInfo.OrderNO);
                if (!allOrderDate.Contains(applyInfo.UseTime))
                {
                    allOrderDate.Add(applyInfo.UseTime);
                }
            }

            //行数
            int iRow = 0;
            difPatient.Sort(new CompareStringByPatientNO());
            foreach (string patintid in difPatient)
            {
                patientData = hsApplyInfo[patintid] as ArrayList;

                System.Collections.Hashtable hsTotData = new Hashtable();

                ArrayList newAllData = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in patientData)
                {
                    //{DDFC27A3-159F-4558-96D7-55BB812E44E4}
                    if (hsTotData.Contains(applyOut.OrderNO + applyOut.UseTime.ToShortDateString()))
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = hsTotData[applyOut.OrderNO + applyOut.UseTime.ToShortDateString()] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        applyOutTot.Operation.ApplyQty += applyOut.Operation.ApplyQty;
                        applyOutTot.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(applyOut.UseTime), dt) + "," + applyOutTot.User03;
                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = applyOut.Clone();
                        applyOutTot.User01 = FS.FrameWork.Function.NConvert.ToInt32(!FS.SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(applyOutTot.StockDept.ID, applyOutTot.Item.ID)).ToString();
                        applyOutTot.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(applyOut.UseTime), dt);
                        hsTotData.Add(applyOut.OrderNO + applyOut.UseTime.ToShortDateString(), applyOutTot);
                        newAllData.Add(applyOutTot);
                    }
                }


                this.SuspendLayout();

                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

                //患者姓名
                string privPatientName = "";
                string samePatient = "";

                string patientInfo = "";

                if (drugBillClass.ID != "O" && drugBillClass.ID != "P")
                {
                    try
                    {
                        CompareApplyOutByOrderNO com1 = new CompareApplyOutByOrderNO();
                        newAllData.Sort(com1);
                    }
                    catch { };
                }
                DateTime dtFirstPrintTime = DateTime.MinValue;
                int id = 0;

                count += newAllData.Count;

                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in newAllData)
                {
                    FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                    item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                    if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
                    {
                        this.nlbReprint.Visible = true;
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
                        p = inpatientManager.GetPatientInfoByPatientNO(info.PatientNO);
                        privPatientName = info.PatientName;
                        id = 0;
                        samePatient = info.PatientNO;
                        patientInfo = string.Format("床号：{0}  姓名：{1}  住院号：{2}  年龄：{3}", bedNO, FS.SOC.Public.String.PadRight(privPatientName, 8, ' '), p.PID.PatientNO.TrimStart('0'), age);
                        //如果不是在本页的第一行，需要患者信息
                        if (iRow % this.pageRowNum != 0)
                        {
                            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noBottomBorder;
                            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
                            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                            iRow++;
                        }
                    }

                    //每页的第一行都需要患者信息
                    if (iRow % this.pageRowNum == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(iRow, 1);

                        this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noBottomBorder;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
                        valueable = "";
                    }
                    if (!FS.FrameWork.Function.NConvert.ToBoolean(info.User01))
                    {
                        valueable += "●";
                    }

                    int times = info.User03.Split(',').Length;

                    if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).OnceDose > info.DoseOnce)
                    {
                        //morethandoseonce = "△";
                    }

                    FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                    if (string.IsNullOrEmpty(info.PlaceNO))
                    {
                        info.PlaceNO = itemMgr.GetPlaceNO(info.Clone().StockDept.ID, info.Clone().Item.ID).ToString();//库存管理科室ID,项目ID

                        // {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                    }
                    id += 1;
                    this.neuSpread1.SetCellValue(0, iRow, "序号", id.ToString());
                    //this.neuSpread1_Sheet1.Cells.Get(iRow, 0).Border = noneBorder;
                    this.neuSpread1.SetCellValue(0, iRow, "货位号", info.PlaceNO);
                    //{83A75FE2-89DE-4b11-9BCB-6092837FE537}
                    //长期医嘱明细摆药单上增加每次剂量
                    this.neuSpread1.SetCellValue(0, iRow, "每次量", info.DoseOnce + info.Item.DoseUnit);
                    this.neuSpread1.SetCellValue(0, iRow, "名称", valueable + info.Item.Name);
                    this.neuSpread1.SetCellValue(0, iRow, "规格", info.Item.Specs);
                    this.neuSpread1.SetCellValue(0, iRow, "调配核对", "");
                    this.neuSpread1.SetCellValue(0, iRow, "发药核对", "");

                    string memo = "";
                    if (!string.IsNullOrEmpty(item.Memo))
                    {
                        memo += "[" + item.Memo + "]";
                    }
                    memo += string.IsNullOrEmpty(info.Memo) ? "" : "【" + info.Memo + "】";
                    this.neuSpread1.SetCellValue(0, iRow, "备注", memo);
                    //总量

                    //this.neuSpread1_Sheet1.Cells[iRow, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
                    //this.neuSpread1_Sheet1.Cells[iRow, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;


                    decimal applyQty = info.Operation.ApplyQty;
                    string unit = info.Item.MinUnit;
                    decimal price = info.Item.PriceCollection.RetailPrice;


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
                                info.Item.PackUnit = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).PackUnit;
                            }
                            catch { }
                        }
                    }
                    if (outPackQty == 0)
                    {
                        applyQty = info.Operation.ApplyQty;
                        unit = info.Item.MinUnit;
                        price = info.Item.PriceCollection.RetailPrice / info.Item.PackQty;
                    }
                    else if (outMinQty == 0)
                    {
                        applyQty = outPackQty;
                        unit = info.Item.PackUnit;
                    }
                    else
                    {
                        applyQty = outPackQty;
                        unit = info.Item.PackUnit + outMinQty.ToString() + info.Item.MinUnit;
                    }

                    if (info.OrderType.ID == "CZ" && FS.SOC.HISFC.BizProcess.Cache.Order.GetFrequency(info.Frequency.ID).Times.Length != times)
                    {
                        //unit = unit + "△";
                    }
                    this.neuSpread1.SetCellValue(0, iRow, "总数量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + " " + unit);


                    if (isPO)
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "服药频率", info.Frequency.ID + "(" + info.User03 + ")");


                        if (info.Frequency.Name == "另加")
                        {
                            this.neuSpread1.SetCellValue(0, iRow, "服药频率", "另" + info.Frequency.ID.ToLower() + "(" + info.User03 + ")");
                        }

                    }
                    else
                    {
                        //this.neuSpread1.SetCellValue(0, iRow, "单价(元)", price.ToString("F4").TrimEnd('0').TrimEnd('.'));

                        this.neuSpread1.SetCellValue(0, iRow, "服药频率", info.Frequency.ID + "(" + info.User03 + ")");


                        if (info.Frequency.Name == "另加")
                        {
                            this.neuSpread1.SetCellValue(0, iRow, "服药频率", "另" + info.Frequency.ID.ToLower() + "(" + info.User03 + ")");
                        }

                    }

                    //for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
                    //{
                    //    this.neuSpread1_Sheet1.Cells.Get(iRow, i).Border = noneBorder;
                    //}

                    this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 1].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 2].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 3].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 4].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Border = noBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Border = noBottomBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 9].Border = noBottomBorder;
                    iRow++;
                }


            }

            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            allOrderDate.Sort(new CompareOrderDate());
            DateTime startTime = new DateTime();
            DateTime enTime = new DateTime();
            if (allOrderDate.Count > 0)
            {
                startTime = DateTime.Parse(allOrderDate[0].ToString());

                enTime = DateTime.Parse(allOrderDate[allOrderDate.Count - 1].ToString());
            }
            this.lblOrderDate.Text = "医嘱时间：" + startTime.ToShortDateString() + " 至 " + enTime.ToShortDateString();

            this.nlbRowCount.Text = "记录数：" + count.ToString();

            this.neuSpread1_Sheet1.Columns.Get(0).SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Ascending;
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

                            //this.neuSpread1_Sheet1.AddRows(index, 1);
                            //this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                            //this.neuSpread1_Sheet1.Cells[index, 0].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 0].Text = "一式三联：①白联药房 ②红联护士 ③黄联输送";
                            //this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("宋体", 7f);
                            //this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;



                            this.neuSpread1_Sheet1.AddRows(index, 1);
                            //打印单底部文字


                            //{5B0E232F-CA85-4591-AE00-59E1C0A51B62}
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                            this.neuSpread1_Sheet1.Cells[index, 0].Border = topBorder;
                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "                                                              一式三联：①白联药房 ②红联护士 ③黄联输送 □医嘱审核/□调配药师：           □医嘱核对/□发药药师：           取药人：               接收人：";
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("宋体", 10f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                            this.neuSpread1_Sheet1.Rows[index].Tag = page;
                            continue;
                        }

                        //标记页码，补打选择页码时用
                        this.neuSpread1_Sheet1.Rows[index].Tag = page;
                    }
                }
                this.neuSpread1_Sheet1.Rows.Get(0).Border = topBorder;

            #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
            //this.ResumeLayout(true);
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
                FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize paperSize1 = pageSizeMgr.GetPageSize("InPatientDrugBillD");
                paperSize = new System.Drawing.Printing.PaperSize("InPatientDrugBillD", paperSize1.Width, paperSize1.Height);
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

            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
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
            int hours = dt.Hour;// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
            string hour = "";
            if (hours >= 0 && hours < 12)
            {
                hour = hours + "a";
            }
            else if (hours == 12)
            {
                hour = hours + "p";
            }
            else
            {
                hour = (hours - 12) + "p";
            }
            if (false)
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
            return hour;
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
            this.Clear();
            //this.SetFormat();
            //this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            //this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        //void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        //{
        //    this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");
        //}

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
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = new FS.HISFC.Models.Pharmacy.ApplyOut();
                if ((x is FS.HISFC.Models.Pharmacy.ApplyOut) && (y is FS.HISFC.Models.Pharmacy.ApplyOut))
                {
                    o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                    o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                }
                string oX = "1";          //患者姓名
                string oY = "1";          //患者姓名

                //FZC ADD 临嘱排在前面
                #region

                string oo1 = string.Empty;
                string oo2 = string.Empty;
                if (o1.OrderType != null && o1.OrderType.ID == "CZ")
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
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细;
            }
        }

        #endregion
    }
    /// <summary>
    /// 排序类
    /// </summary>
    public class CompareOrderDate : IComparer
    {
        /// <summary>
        /// 排序方法
        /// </summary>
        public int Compare(object x, object y)
        {
            DateTime o1 = DateTime.Parse(x.ToString());
            DateTime o2 = DateTime.Parse(y.ToString());

            DateTime oX = new DateTime();          //患者姓名
            DateTime oY = new DateTime();          //患者姓名


            oX = o1;
            oY = o2;

            return DateTime.Compare(oX, oY);
        }
    }
}
