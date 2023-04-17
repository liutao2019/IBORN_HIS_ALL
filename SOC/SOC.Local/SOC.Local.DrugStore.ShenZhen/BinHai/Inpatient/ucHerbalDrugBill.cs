using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Inpatient
{
    /// <summary>
    /// [功能描述: 住院药房草药摆药单本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-06]<br></br>
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
        }

        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        


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
        }


        /// <summary>
        /// Fp设置
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreHerbalDrugBill.xml");
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
            int rowIndex = 0;
            //列号：
            int colIndex = 0;
            //总药价
            decimal drugListTotalPrice = 0;
            #endregion

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();


            #region 设置数据

            //单据号
            this.nlblBillNO.Text = "单号：" + drugBillClass.DrugBillNO;

            //记录同组合的记录数：即一个草原方的药味数
            System.Collections.Hashtable hsComboRowCount = new Hashtable();
            FS.HISFC.Models.Pharmacy.ApplyOut lastInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (hsComboRowCount.Contains(info.CombNO))
                {
                    int qty = (int)hsComboRowCount[info.CombNO];
                    qty = qty + 1;
                    hsComboRowCount[info.CombNO] = qty;
                }
                else
                {
                    hsComboRowCount.Add(info.CombNO, 1);
                }

                string bedNO = info.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }

                if (combNO != info.CombNO)
                {
                    if (colIndex == this.neuSpread1_Sheet1.ColumnCount / 2)
                    {
                        colIndex = 0;
                        rowIndex++;
                    }
                    if (!string.IsNullOrEmpty(combNO))
                    {
                        this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);
                        this.SetRecipeInfo(rowIndex, FS.FrameWork.Function.NConvert.ToInt32(hsComboRowCount[lastInfo.CombNO]), lastInfo.Days);
                        rowIndex++;
                    }

                    this.SetPatientInfo(rowIndex, bedNO, info.PatientName, info.PatientNO, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID), SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(lastInfo.RecipeInfo.ID));

                    if (rowIndex == 0)
                    {
                        this.neuSpread1_Sheet1.Cells[rowIndex, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[rowIndex, 0].Border = topBorder;
                    }
                    rowIndex++;
                    
                    for (int colIndex1 = 0; colIndex < this.neuSpread1_Sheet1.ColumnCount; colIndex1++)
                    {
                        this.neuSpread1_Sheet1.Cells[rowIndex, colIndex1].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                    }

                    rowIndex++;
                    colIndex = 0;

                    combNO = info.CombNO;

                }
                if (info.Item.BaseDose == 0)
                {
                    info.Item.BaseDose = 1;
                }
                if (colIndex == 0)
                {
                    //行的第一个药
                    this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);

                    this.neuSpread1.SetCellValue(0, rowIndex, "编码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                    this.neuSpread1.SetCellValue(0,rowIndex,"名称",info.Item.Name);
                    string onceDose = info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                    this.neuSpread1.SetCellValue(0, rowIndex, "每次用量", onceDose);
                    string cost = (info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F2");
                    this.neuSpread1.SetCellValue(0, rowIndex, "金额", cost);
                    this.neuSpread1.SetCellValue(0, rowIndex, "用法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));

                    colIndex = this.neuSpread1_Sheet1.ColumnCount / 2;
                }
                else
                {

                    //行的第二个药
                    this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);
                    
                    //注意列名称前面的空格
                    this.neuSpread1.SetCellValue(0, rowIndex, " 编码 ", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                    this.neuSpread1.SetCellValue(0, rowIndex, " 名称 ", info.Item.Name);
                    string onceDose = info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;
                    this.neuSpread1.SetCellValue(0, rowIndex, " 每次用量 ", onceDose);
                    string cost = (info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F2");
                    this.neuSpread1.SetCellValue(0, rowIndex, " 金额 ", cost);
                    this.neuSpread1.SetCellValue(0, rowIndex," 用法 ", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));

                    colIndex = this.neuSpread1_Sheet1.ColumnCount;
                }

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
                decimal drugPrice = info.Operation.ApplyQty/ info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                //金额
                //总药价
                drugListTotalPrice += FS.FrameWork.Function.NConvert.ToDecimal(drugPrice.ToString("F2"));

                //重新开始一行
                if (colIndex == this.neuSpread1_Sheet1.ColumnCount)
                {
                    colIndex = 0;
                    rowIndex++;
                }
                lastInfo = info;
            }
            #endregion

            rowIndex = this.neuSpread1_Sheet1.RowCount;
            this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);
            this.SetRecipeInfo(rowIndex, FS.FrameWork.Function.NConvert.ToInt32(hsComboRowCount[lastInfo.CombNO]), lastInfo.Days);

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            this.ResumeLayout(true);
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            this.lbPageNO.Visible = true;
            //启用华南打印基类打印
            FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();

            //获取维护的纸张
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("InPatientDrugHerbal");
                //指定打印处理，default说明使用默认打印机的处理
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                //没有维护时默认一个纸张
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("InPatientDrugHerbal", 850, 1100);
                }
            }

            //打印边距处理：将维护的上边距作为下边距，这样控制页尾打印的空白，保证数据完全打印
            print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, 10, pageSize.Top);

            //纸张处理
            print.PaperName = pageSize.Name;
            print.PaperHeight = pageSize.Height;
            print.PaperWidth = pageSize.Width;

            //打印机名称
            print.PrinterName = pageSize.Printer;

            //页码显示
            this.lbPageNO.Tag = "页码：{0}/{1}";
            print.PageNOControl = this.lbPageNO;

            //页眉控件，首页打印
            print.HeaderControl = this.neuPanel1;
            //页脚控件，最后一页打印
            print.FooterControl = this.panel1;

            //如果不是补打，不显示页码选择
            print.IsShowPageNOChooseDialog = !string.IsNullOrEmpty(this.nlbFirstPrintTime.Text);

            //管理员使用预览功能
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPageView(this);
            }
            else
            {
                print.PrintPage(this);
            }

            this.lbPageNO.Visible = false;
        }
        #endregion

        #region 草药摆药单的特殊方法

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="rowIndex">患者信息所在行</param>
        /// <param name="bedNO">床号</param>
        /// <param name="patientName">患者姓名</param>
        /// <param name="patientNO">住院流水号</param>
        /// <param name="applyDeptName">申请科室</param>
        /// <param name="recipeDoctor">开放医生</param>
        private void SetPatientInfo(int rowIndex, string bedNO, string patientName, string patientNO, string applyDeptName, string recipeDoctor)
        {
            for (int colIndex = 0; colIndex < this.neuSpread1_Sheet1.ColumnCount; colIndex++)
            {
                if (this.neuSpread1_Sheet1.Columns[colIndex].Width > 0 && this.neuSpread1_Sheet1.Columns[colIndex].Visible)
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, colIndex].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1 - colIndex;
                    this.neuSpread1_Sheet1.Cells[rowIndex, colIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Font = new Font("宋体", 11F, FontStyle.Bold);

                    string showValue = 
                        applyDeptName
                         + "    " + bedNO + "床"
                         + "    姓名：" + patientName
                         + "        住院ID：" + patientNO
                         + "        开方医生：" + recipeDoctor
                        ;

                    this.neuSpread1.Sheets[0].Cells[rowIndex, colIndex].Text = showValue;
                    break;
                }
            }
        }

        /// <summary>
        /// 设置处方信息
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="comboRowCount">药味数</param>
        /// <param name="days">剂数</param>
        private void SetRecipeInfo(int rowIndex, int comboRowCount,decimal days)
        {
            for (int colIndex = 0; colIndex < this.neuSpread1_Sheet1.ColumnCount; colIndex++)
            {
                if (this.neuSpread1_Sheet1.Columns[colIndex].Width > 0 && this.neuSpread1_Sheet1.Columns[colIndex].Visible)
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, colIndex].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1 - colIndex;
                    this.neuSpread1_Sheet1.Cells[rowIndex, colIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Font = new Font("宋体", 11F, FontStyle.Bold);
                    this.neuSpread1.Sheets[0].Cells[rowIndex, colIndex].Text = "药味数：" + comboRowCount.ToString() + "剂数：" + days.ToString("F0");
                    break;
                }
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
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreTotDrugBill.xml");
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
            this.PrintPage();
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
