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
    /// [功能描述: 住院药房草药摆药单本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-02]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucHerbalDrugBillIBORNOLD : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucHerbalDrugBillIBORNOLD()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 每页打印条数
        /// </summary>
        private int pageCount = 12;

        /// <summary>
        /// 最大页码
        /// </summary>
        int totPageNO = 0;

        private ArrayList allDataTmp = new ArrayList();

        private FS.FrameWork.Models.NeuObject stockDeptTmp = new FS.FrameWork.Models.NeuObject();

        private FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClassTmp = new FS.HISFC.Models.Pharmacy.DrugBillClass();


        /// <summary>
        /// 住院患者管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
        #endregion

        #region 摆药单的通用方法

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.nlblBillNO.Text = "摆药单号：";
            //this.nlbStockDeptName.Text = string.Empty;
            this.neuSpread1_Sheet1.Rows.Count = 0;
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
            this.allDataTmp = alData.Clone() as ArrayList;
            this.drugBillClassTmp = drugBillClass.Clone();
            this.stockDeptTmp = stockDept.Clone();
            this.ShowDetailData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.SuspendLayout();

            #region 变量

            if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            {
                this.nlbReprint.Visible = true;
            }
            else
            {
                this.nlbReprint.Visible = false;
            }
            this.lbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.lblTitleName.Text = "中药饮片摆药单(明细)";
            string applyDeptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).ApplyDept.ID);

            this.nlbNurseCellName.Text = applyDeptName + "           ";
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();

            textCellType2.Multiline = true;
            textCellType2.WordWrap = true;

            //this.nlbStockDeptName.Text = "发药科室：" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).StockDept.ID);

            this.nlbOperName.Text = "操作员：" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).Operation.ExamOper.ID);

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
            #endregion

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, true, false);
            
            #region 设置数据

            //单据号
            this.nlblBillNO.Text = "摆药单号：" + drugBillClass.DrugBillNO;

            System.Collections.Hashtable hsCombo = new Hashtable();

            FS.HISFC.Models.Pharmacy.ApplyOut lastInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

            ArrayList allPatient = new ArrayList();

            Dictionary<string, List<FS.HISFC.Models.Pharmacy.ApplyOut>> printInfo = new Dictionary<string, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
            
            int index = 0;

            decimal totCost = 0m;

            decimal curPatientTotCost = 0m;

            DateTime dtDrugedDate = new DateTime();

            string show = "";

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (printInfo.ContainsKey(info.PatientNO))
                {
                    printInfo[info.PatientNO].Add(info);
                }
                else
                {
                     List<FS.HISFC.Models.Pharmacy.ApplyOut> list = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                     list.Add(info);
                     printInfo.Add(info.PatientNO, list);          
                }
            }
            totCost = 0m;

            ArrayList allOrderDate = new ArrayList();
            decimal herbalQty = 1;

            foreach (string patientNO in printInfo.Keys)
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.GetPatientInfoByPatientNO(patientNO);
                string bedNO = patientInfo.PVisit.PatientLocation.Bed.ID;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                string info1 = string.Format("床号：{0}  姓名：{1}  住院号：{2}  年龄：{3}", bedNO, patientInfo.Name, patientInfo.PID.PatientNO, inPatientMgr.GetAge(patientInfo.Birthday));
                
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = info1;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                List<FS.HISFC.Models.Pharmacy.ApplyOut> list = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                foreach(FS.HISFC.Models.Pharmacy.ApplyOut info in printInfo[patientNO]) 
                {
                    list.Add(info);
                }
                curPatientTotCost = 0m;
                int rowCount = (int)Math.Ceiling(list.Count / (double)3);
                index = 0;
                for (int i = 0; i < rowCount; i++)
                {
                    if (list.Count >= i * 3 + 1)
                    {
                        index++; 
                        FS.HISFC.Models.Pharmacy.ApplyOut info = list[i * 3 + 0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        info.PlaceNO = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID, info.Item.ID);

                        FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                        order = orderMgr.QueryOneOrder(info.OrderNO);
                        if (!allOrderDate.Contains(info.UseTime))
                        {
                            allOrderDate.Add(info.UseTime);
                        }
                        herbalQty = info.Days;
                        show = "      用法：" + info.Usage.Name + "，" + info.Frequency.Name;
                        string memo = string.IsNullOrEmpty(info.Memo) ? "" : "【" + info.Memo + "】";
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = index.ToString() + ".";

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text = info.Item.Name + memo;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].CellType = textCellType2;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].Text = info.DoseOnce + info.Item.DoseUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = info.Operation.ExamQty + info.Item.MinUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = info.PlaceNO;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Border = topBorder;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                        totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                    }
                    if (list.Count >= i * 3 + 2)
                    {
                        index++;
                        FS.HISFC.Models.Pharmacy.ApplyOut info = list[i * 3 + 1] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        info.PlaceNO = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID, info.Item.ID);

                        FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                        order = orderMgr.QueryOneOrder(info.OrderNO);
                        if (!allOrderDate.Contains(info.UseTime))
                        {
                            allOrderDate.Add(info.UseTime);
                        }
                        string memo = string.IsNullOrEmpty(info.Memo) ? "" : "【" + info.Memo + "】";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Text = index.ToString() + ".";

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = info.Item.Name + memo;
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].CellType = textCellType2;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = info.DoseOnce + info.Item.DoseUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = info.Operation.ExamQty + info.Item.MinUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = info.PlaceNO;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Border = topBorder;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        
                        curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                        totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;

                    }
                    if (list.Count >= i * 3 + 3)
                    {
                        index++; 
                        FS.HISFC.Models.Pharmacy.ApplyOut info = list[i * 3 + 2] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        info.PlaceNO = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID, info.Item.ID);

                        FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                        order = orderMgr.QueryOneOrder(info.OrderNO);
                        if (!allOrderDate.Contains(info.UseTime))
                        {
                            allOrderDate.Add(info.UseTime);
                        }
                        string memo = string.IsNullOrEmpty(info.Memo) ? "" : "【" + info.Memo + "】";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 10].Text = index.ToString() + ".";

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 11].Text = info.Item.Name + memo;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 11].CellType = textCellType2;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 12].Text = info.DoseOnce + info.Item.DoseUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 13].Text = info.Operation.ExamQty + info.Item.MinUnit;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 14].Text = info.PlaceNO;

                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 14].Border = topBorder;

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                        totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
                    }

                }
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = "合计金额：" + curPatientTotCost.ToString("F2") + show + "      ×" + herbalQty + "剂";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
               

            }
            #region 屏蔽
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            //{
            //    days = info.Days.ToString();
            //    if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            //    {
            //        this.nlbReprint.Visible = false;
            //        if (!this.lbTitle.Text.Contains("补打"))
            //        {
            //            this.lbTitle.Text = this.nlbReprint.Text + this.lbTitle.Text;
            //        }
            //    }
            //    else
            //    {
            //        this.nlbReprint.Visible = false;
            //    }
            //    //this.lbInsureDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);
            //    if (!allPatient.Contains(info.PatientNO))
            //    {
            //        dtDrugedDate = info.Operation.ExamOper.OperTime;

            //        FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.GetPatientInfoByPatientNO(info.PatientNO);

            //        if (allPatient.Count != 0)
            //        {
            //            #region 增加合计
	
            //            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = "合计金额：" + curPatientTotCost.ToString("F2");

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            //            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count - 3;

            //            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = "共 " + info.Operation.ExamQty + "剂";

            //            #endregion
            //        }

            //        curPatientTotCost = 0m;

            //        #region 患者信息
            //        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = 3;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "  " + patientInfo.Name + "     " + inPatientMgr.GetAge(patientInfo.Birthday);

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            //        allPatient.Add(info.PatientNO);
            //        index++;
            //        #endregion
                  
            //    }
            //    FS.SOC.HISFC.BizLogic.Pharmacy.InOut item = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            //    if (string.IsNullOrEmpty(info.PlaceNO))
            //    {
            //        info.PlaceNO = item.GetPlaceNO(info.Clone().StockDept.ID, info.Clone().Item.ID).ToString();//库存管理科室ID,项目ID
            //    }
            //    #region 赋值显示
            //    if (index % 2 == 1)
            //    {
            //        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = index.ToString() + ".";

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text = info.Item.Name;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].Text = info.DoseOnce + info.Item.DoseUnit;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = info.Operation.ExamQty + info.Item.MinUnit;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = info.PlaceNO;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Border = topBorder;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                    
            //    }
            //    else
            //    {
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Text = index.ToString() + ".";

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = info.Item.Name;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = info.DoseOnce + info.Item.DoseUnit;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = info.Operation.ExamQty + info.Item.MinUnit;
                    
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = info.PlaceNO;

            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            //    }

            //    curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
            //    totCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty;
            //    index++;
            //    #endregion

                
               
            //}
            //#endregion

            //#region 增加合计

            //this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = 3;
            
            // this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = "合计金额：" + curPatientTotCost.ToString("F2");

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;


            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count - 3;


            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = "共 " + days + " 剂";
            //#endregion
        
            #endregion
           
            this.nlbTotCost.Text = totCost.ToString("F2");
            allOrderDate.Sort(new CompareOrderDate());
            DateTime startTime = new DateTime();
            DateTime enTime = new DateTime();
            if (allOrderDate.Count > 0)
            {
                startTime = DateTime.Parse(allOrderDate[0].ToString());

                enTime = DateTime.Parse(allOrderDate[allOrderDate.Count - 1].ToString());
            }
            this.lblOrderDate.Text = "医嘱时间：" + startTime.ToShortDateString() + " 至 " + enTime.ToShortDateString();
            
            this.nlbPrintDate.Text = "打印时间：" + DateTime.Now.ToString();

            //this.nlbDrugDate.Text = "配药时间：";//+dtDrugedDate.ToString();nlbDrugDate

            //this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            //FS.SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 7, 2);

            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();

            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            #endregion
            this.ResumeLayout(true);
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void PrintPage()
        {
            this.Dock = DockStyle.None;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = new FS.HISFC.Models.Base.PageSize(string.Empty, 860, 1100);
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
                    int width = 850;

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


        #region 公用方法

        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
            this.Clear();
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
            this.ShowData(allDataTmp, drugBillClassTmp, stockDeptTmp);
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
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药;
            }
        }

        #endregion

    }

}
