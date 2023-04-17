using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Inpatient
{
    /// <summary>
    /// [功能描述: 住院药房草药摆药单本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-02]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucHerbalDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucHerbalDrugBill()
        {
            InitializeComponent();
            this.neuSpread1.MouseUp += new MouseEventHandler(neuSpread1_MouseUp);
        }

        #region 列枚举
        enum ColSet
        {
            ColDrugNO1,
            ColTradeName1,
            ColQty1,
            ColUnit1,
            ColCost1,
            ColSpeUsage1,

            ColDrugNO2,
            ColTradeName2,
            ColQty2,
            ColUnit2,
            ColCost2,
            ColSpeUsage2,

            ColEnd
        }

        #endregion

        #region 变量

        /// <summary>
        /// 每页打印条数
        /// </summary>
        private int pageCount = 2;

        /// <summary>
        /// 最大页码
        /// </summary>
        int totPageNO = 0;
        /// <summary>
        /// 是否为同一患者标识
        /// </summary>
        private string patientIdFlag = string.Empty;
        /// <summary>
        /// 同一患者总药价（合计金额）
        /// </summary>
        private decimal patientTotalPrice = 0;
        /// <summary>
        /// 右键菜单
        /// </summary>
        System.Windows.Forms.ContextMenu popMenu = new ContextMenu();
        #endregion

        #region 摆药单的通用方法

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.nlblBillNO.Text = "单号：";
            this.nlbStockDeptName.Text = "发药科室：";
            this.nlbFirstPrintTime.Text = "首次打印：";
            this.nlbPrintTime.Text = "打印时间：";
            this.neuSpread1_Sheet1.Rows.Count = 0;
            for (int i = this.neuSpread1.Sheets.Count-1; i > 0; i--)
            {
                this.neuSpread1.Sheets.Remove(this.neuSpread1.Sheets[i]);
            }
        }

        /// <summary>
        /// Fp设置
        /// </summary>
        private void SetFormat(FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.DefaultStyle.Locked = true;

            sheet.Rows.Default.Height = 30;
            sheet.Columns.Default.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            sheet.Columns.Default.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            sheet.Columns.Count = (int)ColSet.ColEnd;

            FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
            sheet.Columns[(int)ColSet.ColDrugNO1].Width = 60F;
            sheet.Columns[(int)ColSet.ColDrugNO1].Visible = false;
            sheet.Columns[(int)ColSet.ColTradeName1].Width = 110F;
            sheet.Columns[(int)ColSet.ColTradeName1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            sheet.ColumnHeader.Columns[(int)ColSet.ColTradeName1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            sheet.Columns[(int)ColSet.ColQty1].Width = 60F;
            sheet.Columns[(int)ColSet.ColUnit1].Width = 34F;
            sheet.Columns[(int)ColSet.ColUnit1].Visible = false;
            sheet.Columns[(int)ColSet.ColCost1].Width = 60F;
            sheet.Columns[(int)ColSet.ColCost1].CellType = n;
            sheet.Columns[(int)ColSet.ColCost1].Visible = false;
            sheet.Columns[(int)ColSet.ColSpeUsage1].Width = 130F;
            sheet.Columns[(int)ColSet.ColDrugNO2].Width = 60F;
            sheet.Columns[(int)ColSet.ColDrugNO2].Visible = false;
            sheet.Columns[(int)ColSet.ColTradeName2].Width = 110F;
            sheet.Columns[(int)ColSet.ColTradeName2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            sheet.ColumnHeader.Columns[(int)ColSet.ColTradeName2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            sheet.Columns[(int)ColSet.ColQty2].Width = 60F;
            sheet.Columns[(int)ColSet.ColUnit2].Width = 34F;
            sheet.Columns[(int)ColSet.ColUnit2].Visible = false;
            sheet.Columns[(int)ColSet.ColCost2].Width = 60F;
            sheet.Columns[(int)ColSet.ColCost2].CellType = n;
            sheet.Columns[(int)ColSet.ColCost2].Visible = false;
            sheet.Columns[(int)ColSet.ColSpeUsage2].Width = 90F;

            sheet.DefaultStyle.Font = new Font("宋体", 10.5F);
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
            this.patientTotalPrice = 0;
            //处方组合号
            string combNO = string.Empty;
            //处方组合集合
            Dictionary<string, FS.HISFC.Models.Pharmacy.ApplyOut> DApplyOut = new Dictionary<string, FS.HISFC.Models.Pharmacy.ApplyOut>();
            int sheetNo = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (string.IsNullOrEmpty(combNO))
                {
                    combNO = info.CombNO;
                }
                if (combNO.Trim() != info.CombNO)
                {
                    ArrayList alApplyOut = new ArrayList();
                    foreach (KeyValuePair<string, FS.HISFC.Models.Pharmacy.ApplyOut> applyOutInfo in DApplyOut)
                    {
                        alApplyOut.Add(applyOutInfo.Value);
                    }
                    //编号>1的处方
                    if (sheetNo > 1)
                    {
                        FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                        this.InitSheet(sheet);
                        this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);
                        this.ShowDetailData(sheet, alApplyOut, drugBillClass, stockDept);
                        sheetNo++;
                    }
                    //第一张处方
                    else
                    {
                        this.ShowDetailData(this.neuSpread1_Sheet1, alApplyOut, drugBillClass, stockDept);
                        sheetNo++;
                    }
                    combNO = info.CombNO;
                    DApplyOut.Clear();
                    DApplyOut.Add(info.ID, info);
                }
                else
                {
                    DApplyOut.Add(info.ID, info);
                }
                
            }
            //处理最后一组组合
            if (DApplyOut.Count>0)
            {
                ArrayList alApplyOut = new ArrayList();
                foreach (KeyValuePair<string, FS.HISFC.Models.Pharmacy.ApplyOut> applyOutInfo in DApplyOut)
                {
                    alApplyOut.Add(applyOutInfo.Value);
                }
                if (sheetNo>1)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();
                    this.InitSheet(sheet);
                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);
                    this.ShowDetailData(sheet, alApplyOut, drugBillClass, stockDept);
                }
                else
                {
                    this.ShowDetailData(this.neuSpread1_Sheet1, alApplyOut, drugBillClass, stockDept);
                }
            }
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(FarPoint.Win.Spread.SheetView sheet,ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            FarPoint.Win.LineBorder border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);


            this.SuspendLayout();

            #region 变量

            this.nlbStockDeptName.Text = "发药科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);

            //按处方排序
            alData.Sort(new CompareApplyOutByCombNO());

            //处方组合号
            string combNO = string.Empty;
            //行号
            int iRow = 0;
            //列号：
            int iCol = 0;
            //总药价
            decimal drugListTotalPrice = 0;
            decimal drugListHalfPrice1 = 0;
            decimal drugListHalfPrice2 = 0;
            decimal drugListHalfPrice = 0;
            decimal lastDrugTotalPrice = 0;
            int t = 0;
            #endregion

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sheet)).BeginInit();


            #region 设置数据

            //单据号
            this.nlblBillNO.Text = "单号：" + drugBillClass.DrugBillNO; //(drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO);

            System.Collections.Hashtable hsCombo = new Hashtable();
            FS.HISFC.Models.Pharmacy.ApplyOut lastInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                //this.lbInsureDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);

                 
                if (hsCombo.Contains(info.CombNO))
                {
                    int qty = (int)hsCombo[info.CombNO];
                    qty = qty + 1;
                    hsCombo[info.CombNO] = qty;
                }
                else
                {
                    hsCombo.Add(info.CombNO, 1);
                }


                //this.lbApplyDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyOper.Dept.ID);             


                string bedNO = info.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }

                if (combNO != info.CombNO)
                {
                    if (iCol == (int)ColSet.ColEnd / 2)
                    {
                        iCol = 0;
                        iRow++;
                    }

                    #region 已改为分页打印，所以不需要再重复添加表头行
                    //if (!string.IsNullOrEmpty(combNO))
                    //{
                    //    AddNewRow(sheet,iRow, false);
                    //    sheet.Rows[iRow].Border = topBorder;
                    //    //sheet.Cells[iRow, (int)ColSet.ColDrugNO1].Text = "药味数：";
                    //    //sheet.Cells[iRow, (int)ColSet.ColTradeName1].Text = hsCombo[lastInfo.CombNO].ToString();
                    //    //sheet.Cells[iRow, (int)ColSet.ColQty2].Text = "剂数：";
                    //    //sheet.Cells[iRow, (int)ColSet.ColCost2].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    //    //sheet.Cells[iRow, (int)ColSet.ColCost2].Text = lastInfo.Days.ToString();
                    //    //sheet.Cells[iRow, (int)ColSet.ColQty2].Font = new Font("宋体", 11F, FontStyle.Bold);
                    //    //sheet.Cells[iRow, (int)ColSet.ColCost2].Font = new Font("宋体", 11F, FontStyle.Bold);

                    //    //sheet.Cells[iRow, (int)ColSet.ColTradeName1].Text = "药味数：";
                    //    //sheet.Cells[iRow, (int)ColSet.ColQty1].Text = hsCombo[lastInfo.CombNO].ToString();
                    //    //sheet.Cells[iRow, (int)ColSet.ColQty2].Text = "剂数：";
                    //    //sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = lastInfo.Days.ToString();
                    //    //sheet.Cells[iRow, (int)ColSet.ColQty2].Font = new Font("宋体", 11F, FontStyle.Bold);
                    //    //sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Font = new Font("宋体", 11F, FontStyle.Bold);

                    //    sheet.Cells[iRow, (int)ColSet.ColTradeName1].Text = "药味数：";
                    //    sheet.Cells[iRow, (int)ColSet.ColQty1].Text = hsCombo[lastInfo.CombNO].ToString();
                    //    sheet.Cells[iRow, (int)ColSet.ColSpeUsage1].Text = "剂数：";
                    //    sheet.Cells[iRow, (int)ColSet.ColTradeName2].Text = lastInfo.Days.ToString();
                    //    sheet.Cells[iRow, (int)ColSet.ColQty2].Text = "金额：";
                    //    sheet.Cells[iRow, (int)ColSet.ColQty2].Font = new Font("宋体", 11F, FontStyle.Bold);
                    //    sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = drugListHalfPrice.ToString("F2");
                    //    sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Font = new Font("宋体", 11F, FontStyle.Bold);

                    //    iRow++;
                    //}

                    #endregion

                    AddNewRow(sheet, iRow, true);

                    sheet.Cells[iRow, 0].Text =
                        string.Format("{3}    {0}床    姓名：{1}    住院ID：{2}    开方医生：{4}", bedNO, info.PatientName, info.PatientNO, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID), SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID));
                    t++;
                    if (iRow == 0)
                    {
                        sheet.Cells[iRow, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
                    }
                    else
                    {
                        sheet.Cells[iRow, 0].Border = topBorder;
                    }
                    iRow++;
                    this.AddLabelRow(sheet,iRow);
                    for (int colIndex = 0; colIndex < (int)ColSet.ColEnd; colIndex++)
                    {
                        sheet.Cells[iRow, colIndex].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                    }

                    iRow++;
                    iCol = 0;

                    combNO = info.CombNO;

                }
                if (info.Item.BaseDose == 0)
                {
                    info.Item.BaseDose = 1;
                }
                if (iCol == 0)
                {
                    this.AddNewRow(sheet, iRow, false);
                    sheet.Cells[iRow, (int)ColSet.ColDrugNO1].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID);
                    sheet.Cells[iRow, (int)ColSet.ColTradeName1].Text = info.Item.Name;
                    if (info.DoseOnce <= 0 || info.Item.MinUnit == info.Item.DoseUnit || Math.Round(info.Operation.ApplyQty, 0) == info.Operation.ApplyQty)
                    {
                        sheet.Cells[iRow, (int)ColSet.ColQty1].Text = (info.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                        sheet.Cells[iRow, (int)ColSet.ColUnit1].Text = info.Item.MinUnit;
                    }
                    else
                    {
                        sheet.Cells[iRow, (int)ColSet.ColQty1].Text = (info.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                        sheet.Cells[iRow, (int)ColSet.ColUnit1].Text = info.Item.DoseUnit;
                    }
                    sheet.Cells[iRow, (int)ColSet.ColCost1].Value = (info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F2");
                    
                    decimal drugHalfPrice1 = info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                    drugListHalfPrice1 = FS.FrameWork.Function.NConvert.ToDecimal(drugHalfPrice1.ToString("F2"));
                    
                    sheet.Cells[iRow, (int)ColSet.ColSpeUsage1].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID);
                    iCol = (int)ColSet.ColEnd / 2;
                }
                else
                {
                    sheet.Cells[iRow, (int)ColSet.ColDrugNO2].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID);
                    sheet.Cells[iRow, (int)ColSet.ColTradeName2].Text = info.Item.Name;
                    //sheet.Cells[iRow, (int)ColSet.ColQty2].Text = info.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit;
                    if (info.DoseOnce <= 0 || info.Item.MinUnit == info.Item.DoseUnit || Math.Round(info.Operation.ApplyQty, 0) == info.Operation.ApplyQty)
                    {
                        sheet.Cells[iRow, (int)ColSet.ColQty2].Text = (info.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                        sheet.Cells[iRow, (int)ColSet.ColUnit2].Text = info.Item.DoseUnit;
                    }
                    else
                    {
                        sheet.Cells[iRow, (int)ColSet.ColQty2].Text = (info.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                        sheet.Cells[iRow, (int)ColSet.ColUnit2].Text = info.Item.DoseUnit;
                    }
                    sheet.Cells[iRow, (int)ColSet.ColCost2].Value = (info.Operation.ApplyQty  / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F2");

                    decimal drugHalfPrice2 = info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                    drugListHalfPrice2 = FS.FrameWork.Function.NConvert.ToDecimal(drugHalfPrice2.ToString("F2"));

                    sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID);
                    iCol = (int)ColSet.ColEnd;
                }

                drugListHalfPrice = drugListHalfPrice1 + drugListHalfPrice2; 

                //打印时间，首次打印也是在保存后打印的，info.State不会等于0，而drugBillClass.ApplyState在调用该控件前处理过
                if (drugBillClass.ApplyState != "0")
                {
                    this.nlbFirstPrintTime.Text = "首次打印：" + info.Operation.ExamOper.OperTime.ToString();
                    this.nlbPrintTime.Text = "打印时间：" + DateTime.Now;
                }
                else
                {
                    this.nlbPrintTime.Text = "打印时间：" + info.Operation.ExamOper.OperTime.ToString();
                    this.nlbFirstPrintTime.Text = "";
                }


                //单价
                decimal drugPrice = info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                //金额
                //总药价
                drugListTotalPrice += FS.FrameWork.Function.NConvert.ToDecimal(drugPrice.ToString("F2"));

                //列号等于5时，重新开始一行
                if (iCol == (int)ColSet.ColEnd)
                {
                    iCol = 0;
                    iRow++;
                }
                lastInfo = info;

                //最后一组数据的信息
                decimal lastDrugPrice = lastInfo.Operation.ApplyQty / lastInfo.Item.PackQty * lastInfo.Item.PriceCollection.RetailPrice;
                lastDrugTotalPrice += FS.FrameWork.Function.NConvert.ToDecimal(lastDrugPrice.ToString("F2"));
            
                //同一患者所涉及的分页中处方费用同一计算到合计金额
                if (string.IsNullOrEmpty(patientIdFlag))
                {
                    patientIdFlag = info.PatientNO;
                }
                if (patientIdFlag == info.PatientNO)
                {
                    patientTotalPrice += FS.FrameWork.Function.NConvert.ToDecimal(drugPrice.ToString("F2"));
                }
                else
                {
                    patientIdFlag = info.PatientNO;
                    patientTotalPrice = 0;
                    patientTotalPrice += FS.FrameWork.Function.NConvert.ToDecimal(drugPrice.ToString("F2"));
                }
            }
            #endregion

            iRow = sheet.RowCount;
            AddNewRow(sheet, iRow, false);
            //sheet.Rows[iRow].Border = topBorder;
            //sheet.Cells[iRow, (int)ColSet.ColTradeName1].Text = "药味数：";
            //sheet.Cells[iRow, (int)ColSet.ColQty1].Text = hsCombo[lastInfo.CombNO].ToString();
            //sheet.Cells[iRow, (int)ColSet.ColQty2].Text = "剂数：";
            //sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = lastInfo.Days.ToString();
            //sheet.Cells[iRow, (int)ColSet.ColQty2].Font = new Font("宋体", 11F, FontStyle.Bold);
            //sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Font = new Font("宋体", 11F, FontStyle.Bold);

            sheet.Rows[iRow].Border = topBorder;
            sheet.Cells[iRow, (int)ColSet.ColTradeName1].Text = "药味数：";
            //sheet.Cells[iRow, (int)ColSet.ColTradeName1].Font = new Font("宋体", 11F, FontStyle.Bold);
            sheet.Cells[iRow, (int)ColSet.ColQty1].Text = hsCombo[lastInfo.CombNO].ToString();
            //sheet.Cells[iRow, (int)ColSet.ColQty1].Font = new Font("宋体", 11F, FontStyle.Bold);
            sheet.Cells[iRow, (int)ColSet.ColSpeUsage1].Text = "剂数：";
           // sheet.Cells[iRow, (int)ColSet.ColSpeUsage1].Font = new Font("宋体", 11F, FontStyle.Bold);
            sheet.Cells[iRow, (int)ColSet.ColTradeName2].Text = lastInfo.Days.ToString();
           // sheet.Cells[iRow, (int)ColSet.ColTradeName2].Font = new Font("宋体", 11F, FontStyle.Bold);
            sheet.Cells[iRow,(int)ColSet.ColQty2].Text="金额：";
            if (t > 1) //多付草药的情况下
            {
                sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = string.Format("{0}", drugListHalfPrice1);
            }
            else //只有一付草药的情况下
            {
                sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = string.Format("{0}", lastDrugTotalPrice);
            }
            
            sheet.Cells[iRow, (int)ColSet.ColQty2].Font = new Font("宋体", 11F, FontStyle.Bold);
            sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Font = new Font("宋体", 11F, FontStyle.Bold);

            ////设置底部数据
            iRow = sheet.RowCount;
            this.AddNewRow(sheet, iRow, true);
            sheet.Cells[iRow, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
            sheet.Cells[iRow, 0].Text = string.Format("配方人：             核方人：              金额合计：{0}", patientTotalPrice);
            
            #region 统一同一患者所有"金额合计"
            foreach(FarPoint.Win.Spread.SheetView sheet1 in this.neuSpread1.Sheets)
            {
                string patientInfo = sheet1.ActiveCell.Text;
                if (patientInfo.Contains(this.patientIdFlag))
                {
                    sheet1.Cells[sheet1.RowCount-1, 0].Text = string.Format("配方人：             核方人：              金额合计：{0}", patientTotalPrice);
                }
            }
            #endregion
            
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sheet)).EndInit();

            this.pageCount = hsCombo.Count;
            this.ResumeLayout(true);
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            this.Dock = DockStyle.None;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
            print.SetPageSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(15, 10, this);
            }
            else
            {
                print.PrintPage(15, 10, this);
            }

            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 获取纸张
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillH", dept);
            //自适应纸张
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 800;

                    int curHeight = 0;

                    int addHeight = (this.neuSpread1.ActiveSheet.RowCount - 1) *
                        (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    int additionAddHeight = 180;

                    paperSize.Width = width;
                    paperSize.Height = (addHeight + curHeight + additionAddHeight);

                    this.Height = paperSize.Height;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("设置纸张出错>>" + ex.Message);
                }
            }
            if (!string.IsNullOrEmpty(paperSize.Printer) && paperSize.Printer.ToLower() == "default")
            {
                paperSize.Printer = "";
            }
            return paperSize;
        }
        #endregion

        #region 草药摆药单的特殊方法
        /// <summary>
        /// 添加行方法
        /// </summary>
        /// <param name="iRow">第几行</param>
        /// <param name="isSpan">是否合并列</param>
        private void AddNewRow(FarPoint.Win.Spread.SheetView sheet,int iRow, bool isSpan)
        {
            sheet.Rows.Add(iRow, 1);
            if (isSpan)
            {
                sheet.Cells[iRow, 0].ColumnSpan = (int)ColSet.ColEnd;
            }
            sheet.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }

        /// <summary>
        /// 添加处方列标题行
        /// </summary>
        /// <param name="iRow"></param>
        private void AddLabelRow(FarPoint.Win.Spread.SheetView sheet, int iRow)
        {
            sheet.Rows.Add(iRow, 1);
            sheet.Cells[iRow, (int)ColSet.ColDrugNO1].Text = "编码";
            sheet.Cells[iRow, (int)ColSet.ColTradeName1].Text = "名称";
            sheet.Cells[iRow, (int)ColSet.ColQty1].Text = "每剂量";
            sheet.Cells[iRow, (int)ColSet.ColSpeUsage1].Text = "用法";
            sheet.Cells[iRow, (int)ColSet.ColDrugNO2].Text = "编码";
            sheet.Cells[iRow, (int)ColSet.ColTradeName2].Text = "名称";
            sheet.Cells[iRow, (int)ColSet.ColQty2].Text = "每剂量";
            sheet.Cells[iRow, (int)ColSet.ColSpeUsage2].Text = "用法";
            sheet.Rows[iRow].Height = 20f;
            sheet.Rows[iRow].Font = new Font("宋体", 9f);
        }

        /// <summary>
        /// 初始化新添加sheet
        /// </summary>
        /// <param name="sheet"></param>
        private void InitSheet(FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.Reset();
            sheet.SheetName = "第" + (this.neuSpread1.Sheets.Count + 1) + "页";
            sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            sheet.ColumnCount = 8;
            sheet.RowCount = 0;
            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, false, false);
            sheet.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            sheet.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            sheet.ColumnHeader.Cells.Get(0, 2).Value = "每剂量";
            sheet.ColumnHeader.Cells.Get(0, 3).Value = "用法";
            sheet.ColumnHeader.Cells.Get(0, 4).Value = "编码";
            sheet.ColumnHeader.Cells.Get(0, 5).Value = "名称";
            sheet.ColumnHeader.Cells.Get(0, 6).Value = "每剂量";
            sheet.ColumnHeader.Cells.Get(0, 7).Value = "用法";
            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.Columns.Get(0).Label = "编码";
            sheet.Columns.Get(0).Width = 0F;
            sheet.Columns.Get(1).Label = "名称";
            sheet.Columns.Get(1).Width = 104F;
            sheet.Columns.Get(3).Label = "用法";
            sheet.Columns.Get(3).Width = 122F;
            sheet.Columns.Get(4).Label = "编码";
            sheet.Columns.Get(4).Width = 0F;
            sheet.Columns.Get(5).Label = "名称";
            sheet.Columns.Get(5).Width = 82F;
            sheet.Columns.Get(7).Label = "用法";
            sheet.Columns.Get(7).Width = 83F;
            sheet.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            sheet.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            sheet.RowHeader.Columns.Default.Resizable = false;
            sheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.RowHeader.Visible = false;
            sheet.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            sheet.SheetCornerStyle.Locked = false;
            sheet.SheetCornerStyle.Parent = "HeaderDefault";
            sheet.VisualStyles = FarPoint.Win.VisualStyles.Off;
            sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;

            this.SetFormat(sheet);
        }
        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "打印该页处方";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);
                this.popMenu.MenuItems.Add(printMenuItem);

                this.popMenu.Show(this.neuSpread1, new Point(e.X, e.Y));
            }
        }
        /// <summary>
        /// 右键打印当前页处方
        /// </summary>
        private void printMenuItem_Click(object sender, EventArgs e)
        {
            this.PrintPage();
        }
        #endregion

        #region 公用方法

        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
            this.Clear();
            this.SetFormat(this.neuSpread1_Sheet1);
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
            if (alData == null || alData.Count == 0)
            {
                return;
            }
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.PrintPage();
        }

        #endregion

        #region 排序类

        /// <summary>
        /// 按处方排序类
        /// </summary>
        public class CompareApplyOutByCombNO : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = o1.CombNO;          //患者姓名
                string oY = o2.CombNO;          //患者姓名

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
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
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 提供可以选择打印范围的打印方法
        /// </summary>
        public void Print()
        {
            //循环打印每张处方
            foreach (FarPoint.Win.Spread.SheetView sheet in this.neuSpread1.Sheets)
            {
                this.neuSpread1.ActiveSheet = sheet;
                this.PrintPage();
            }
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
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药;
            }
        }

        #endregion

    }

}
