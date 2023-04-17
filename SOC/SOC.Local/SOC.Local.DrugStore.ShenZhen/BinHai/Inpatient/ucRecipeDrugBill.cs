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
    /// [功能描述: 住院药房出院带药处方单本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-06]<br></br>
    /// 说明：
    /// </summary>    
    public partial class ucRecipeDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        public ucRecipeDrugBill()
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
            for (int index = 0; index < this.neuPanel1.Controls.Count; index++)
            {
                this.neuPanel1.Controls[index].Text = "";
            }
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 初始化Fp
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreRecipeDrugBill.xml");
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

            this.SuspendLayout();

            #region 设置数据
            //单据号
            if (drugBillClass.ApplyState != "0")
            {
                this.nlblBillNO.Text = "单号：" + drugBillClass.DrugBillNO + "   补打";
            }
            else
            {
                this.nlblBillNO.Text = "单号：" + drugBillClass.DrugBillNO;
            }
            this.nlblRowCount.Text = "记录数：" + alData.Count.ToString();

            //行数
            int rowIndex = 0;

            //合计金额
            decimal drugListTotalPrice = 0;

            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                this.lbTitle.Text = "住院处方(" + drugBillClass.Name + ")";
                if (patient == null || string.IsNullOrEmpty(patient.ID))
                {
                    patient = inpatientManager.GetPatientInfoByPatientNO(info.PatientNO);
                    //病案号
                    this.lbPationNO.Text = "住院流水号：" + info.PatientNO;
                    //病人姓名
                    this.lbName.Text = "姓名：" + patient.Name;
                    //病区
                    this.nlblPatientDept.Text = "申请科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);
                    this.nlblAge.Text = "年龄：" + Common.Function.GetAge(patient.Birthday);
                    if (!string.IsNullOrEmpty(info.BedNO) && info.BedNO.Length > 4)
                    {
                        this.nlblBedNO.Text = "床号：" + info.BedNO.Substring(4);
                    }
                    this.nlblSex.Text = "性别：" + patient.Sex.Name;
                }


                this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);

                //药品名称
                this.neuSpread1.SetCellValue(0, rowIndex, "药品名称", info.Item.Name);
                this.neuSpread1.SetCellValue(0, rowIndex, "规格", info.Item.Specs);

                //总量
                decimal applyQty = info.Operation.ApplyQty;
                string unit = info.Item.MinUnit;
                decimal price = 0m;

                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty), (int)info.Item.PackQty, out outMinQty);
                if (outPackQty == 0)
                {
                    applyQty = info.Operation.ApplyQty;
                    unit = info.Item.MinUnit;
                    price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 4);
                }
                else if (outMinQty == 0)
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit;
                    price = info.Item.PriceCollection.RetailPrice;
                }
                else
                {
                    applyQty = info.Operation.ApplyQty;
                    unit = info.Item.MinUnit;
                    price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 4);
                }
                this.neuSpread1.SetCellValue(0, rowIndex, "数量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);
                this.neuSpread1.SetCellValue(0, rowIndex, "货位号", info.PlaceNO);
                this.neuSpread1.SetCellValue(0, rowIndex, "使用方法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));

                //服药次数
                this.neuSpread1.SetCellValue(0, rowIndex, "服药次数", SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(info.Frequency.ID));

                //每次用量
                this.neuSpread1.SetCellValue(0, rowIndex, "每次用量", info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit);
                //金额
                decimal drugTotalPrice = Math.Round(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 6);
                //总药价
                drugListTotalPrice += drugTotalPrice;
                this.neuSpread1.SetCellValue(0, rowIndex, "金额", drugTotalPrice.ToString("F4"));
                //开单医生
                this.neuSpread1.SetCellValue(0, rowIndex, "开单医生", SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID));
                //用药说明
                this.neuSpread1.SetCellValue(0, rowIndex, "用药说明", info.Memo);

                rowIndex++;
            }

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            #endregion

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
                pageSize = pageSizeMgr.GetPageSize("InPatientDrugDetail");
                //指定打印处理，default说明使用默认打印机的处理
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                //没有维护时默认一个纸张
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("InPatientDrugDetail", 850, 1100);
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
            print.IsShowPageNOChooseDialog = false;

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
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreNorDrugBill.xml");
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
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方;
            }
        }

        #endregion

    }
}
