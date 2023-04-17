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
    /// [功能描述: 住院药房出院带药处方单本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// </summary>    
    public partial class ucNormalDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        public ucNormalDrugBill()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

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
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreNorDrugBill.xml");
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
                this.nlblBillNO.Text = "单号：" + drugBillClass.DrugBillNO;// (drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO) + "   补打";
            }
            else
            {
                this.nlblBillNO.Text = "单号：" + drugBillClass.DrugBillNO;// (drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO);
            }
            this.nlblRowCount.Text = "记录数：" + alData.Count.ToString();

            //行数
            int iRow = 0;

            //合计金额
            decimal drugListTotalPrice = 0;

            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut(); 

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
                    this.nlblAge.Text = "年龄：" + inpatientManager.GetAge(patient.Birthday);
                    if (!string.IsNullOrEmpty(info.BedNO) && info.BedNO.Length > 4)
                    {
                        this.nlblBedNO.Text = "床号：" + info.BedNO.Substring(4);
                    }
                    this.nlblSex.Text = "性别：" + patient.Sex.Name;
                }


                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);

                //药品名称
                FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {

                    this.neuSpread1.SetCellValue(0, iRow, "药品名称", item.NameCollection.RegularName);
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, iRow, "药品名称", item.Name);
                }
                //this.neuSpread1.SetCellValue(0, iRow, "药品名称", info.Item.Name);
                this.neuSpread1.SetCellValue(0, iRow, "规格", info.Item.Specs);

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
                this.neuSpread1.SetCellValue(0, iRow, "数量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);               
                this.neuSpread1.SetCellValue(0, iRow, "货位号", itemMgr.GetPlaceNO(info.StockDept.ID, info.Item.ID));
                this.neuSpread1.SetCellValue(0, iRow, "使用方法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));

                //服药次数
                this.neuSpread1.SetCellValue(0, iRow, "服药次数", Common.Function.GetFrequenceName(info.Frequency));

                //每次用量
                string doseOnce = Common.Function.GetOnceDose(info);
                this.neuSpread1.SetCellValue(0, iRow, "每次用量", doseOnce);
                //金额
                decimal drugTotalPrice = Math.Round(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 6);
                //总药价
                drugListTotalPrice += drugTotalPrice;
                this.neuSpread1.SetCellValue(0, iRow, "金额", drugTotalPrice.ToString("F4"));
                //开单医生
                this.neuSpread1.SetCellValue(0, iRow, "开单医生", SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID));
                //用药说明
                this.neuSpread1.SetCellValue(0, iRow, "用药说明", info.Memo);

                iRow++;
            }

            this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
            this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = 10;
            string operName = inpatientManager.Operator.Name;
            this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells[iRow, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
            this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new Font("宋体", 10f);
            this.neuSpread1_Sheet1.Cells[iRow, 0].Text = string.Format("发药：            核对：                       合计金额：{0}  打印时间：{1}", drugListTotalPrice.ToString(), inpatientManager.GetDateTimeFromSysDateTime().ToString());
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
            this.Dock = DockStyle.None;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
            print.SetPageSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(10, 10, this);
            }
            else
            {
                print.PrintPage(10, 10, this);
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
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillN", dept);
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
            if (alData == null || alData.Count == 0)
            {
                return;
            }
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
