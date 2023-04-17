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
    /// [功能描述: 住院药房草药摆药单本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-02]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucHerbalDrugBillPrintByPatient : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucHerbalDrugBillPrintByPatient()
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

        /// <summary>
        /// 住院患者管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        #endregion

        #region 摆药单的通用方法

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.nlblBillNO.Text = "单号：";
            this.nlbStockDeptName.Text = string.Empty;
            this.neuSpread1_Sheet1.Rows.Count = 0;
            
        }

        /// <summary>
        /// 这实在没有意义，和汇总单统一罢了
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        private void ShowBillData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept,int totPage)
        {
            this.Clear();
            this.ShowDetailData(alData, drugBillClass, stockDept,totPage);
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept,int totPage)
        {
            this.SuspendLayout();

            #region 变量

            if (alData.Count%pageCount == 0)
            {
                totPage = (int)(alData.Count / pageCount);
            }
            else
            {
                totPage = (int)(alData.Count / pageCount) + 1;
            }

            //申请科室
            string applyDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).ApplyDept.ID);

            this.nlbNurseCellName.Text = applyDeptName + "           ";

            this.nlbStockDeptName.Text = "发药科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).StockDept.ID);

            this.nlbOperName.Text = "操作员：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName((alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).Operation.ExamOper.ID);

            if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            {
                this.nlbReprint.Visible = false;
                if (!this.lbTitle.Text.Contains("补打"))
                {
                    this.lbTitle.Text = this.nlbReprint.Text + this.lbTitle.Text;
                }
            }
            else
            {
                this.nlbReprint.Visible = false;
            }

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


            #region 设置数据

            //单据号
        

            System.Collections.Hashtable hsCombo = new Hashtable();

            FS.HISFC.Models.Pharmacy.ApplyOut lastInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

            ArrayList allPatient = new ArrayList();

            int index = 0;

            decimal curPatientTotCost = 0m;

            DateTime dtDrugedDate = new DateTime();

            int pageNo = 1;


            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                //this.lbInsureDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);
                if (!allPatient.Contains(info.PatientNO))
                {
                    dtDrugedDate = info.Operation.ExamOper.OperTime;

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.GetPatientInfoByPatientNO(info.PatientNO);

                    if (allPatient.Count != 0)
                    {
                        this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

                        this.nlbPrintDate.Text = "打印时间：" + DateTime.Now.ToString();

                        this.nlbDrugDate.Text = "配药时间：" + dtDrugedDate.ToString();

                        this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

                        SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

                        this.nlbPageNO.Text = "第" + pageNo + "页，" + "共" + totPage + "页";
                        this.SetPanelBottomVisible((pageNo == totPage));
                        this.PrintPage();
                        pageNo++;
                    }

                    this.nlblBillNO.Text = "单号：" + drugBillClass.DrugBillNO;

                    curPatientTotCost = 0m;

                    #region 患者信息
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].ColumnSpan = 3;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "  " + patientInfo.Name + "     " + inPatientMgr.GetAge(patientInfo.Birthday);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Font = new Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                    allPatient.Add(info.PatientNO);
                    index++;
                    if (index == 12)
                    {
                        this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

                        this.nlbPrintDate.Text = "打印时间：" + DateTime.Now.ToString();

                        this.nlbDrugDate.Text = "配药时间：" + dtDrugedDate.ToString();

                        this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

                        SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

                        this.nlbPageNO.Text = "第" + pageNo + "页，" + "共" + totPage + "页";

                        this.SetPanelBottomVisible((pageNo == totPage));

                        this.PrintPage();
                        this.neuSpread1_Sheet1.Rows.Count = 0;
                        pageNo++;
                    }
                    #endregion
                  
                }

                #region 赋值显示

                if (index == 13)
                {
                    this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

                    this.nlbPrintDate.Text = "打印时间：" + DateTime.Now.ToString();

                    this.nlbDrugDate.Text = "配药时间：" + dtDrugedDate.ToString();

                    this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

                    SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

                    this.nlbPageNO.Text = "第" + pageNo + "页，" + "共" + totPage + "页";
                    this.SetPanelBottomVisible((pageNo == totPage));

                    this.PrintPage();
                    this.neuSpread1_Sheet1.Rows.Count = 0;
                    pageNo++;
                }

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = index.ToString();

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text = info.Item.Name;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = info.Item.Specs;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = info.DoseOnce + info.Item.DoseUnit;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Text = info.Frequency.ID;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = info.Operation.ApplyQty + info.Item.MinUnit;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = (info.Item.PriceCollection.RetailPrice / info.Item.PackQty).ToString("F2");

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = ((info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ExamQty).ToString("F2");

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = info.CombNO;

                curPatientTotCost += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * info.Operation.ApplyQty;

                this.nlbDays.Text = "剂数：" + info.Days;

                index++;
                #endregion
               
            }
            #endregion


          

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            this.nlbTotCost.Text = curPatientTotCost.ToString("F2");

            this.nlbPrintDate.Text = "打印时间：" + DateTime.Now.ToString();

            this.nlbDrugDate.Text = "配药时间：" + dtDrugedDate.ToString();

            this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 9, 2);

            this.nlbPageNO.Text = "第" + pageNo + "页，" + "共" + totPage + "页";
            this.SetPanelBottomVisible((pageNo == totPage));
            this.PrintPage();
            this.neuSpread1_Sheet1.Rows.Count = 0;

            this.ResumeLayout(true);
        }

        private void SetPanelBottomVisible(bool  isVilible)
        {
            foreach (Control c in this.panel3.Controls)
            {
                if (c is Label)
                {
                    c.Visible = isVilible;
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            this.Dock = DockStyle.None;

            this.SuspendLayout();
            this.panel2.Dock = DockStyle.None;
            this.panel3.Dock = DockStyle.None;
            this.panel2.Height = (int)(this.neuSpread1.Sheets[0].RowHeader.Rows[0].Height + this.neuSpread1_Sheet1.Rows.Count * this.neuSpread1_Sheet1.Rows.Default.Height + 5);
            this.panel3.Location = new Point(0, this.panel1.Height + this.panel2.Height);
            this.ResumeLayout();

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
            print.SetPageSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }

            this.panel2.Height = 373;

            this.Dock = DockStyle.Fill;
        }


        /// <summary>
        /// 获取纸张
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillH", "ALL");
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
            #region 计算页码

            int totPage = 0;
            ArrayList allPatient = new ArrayList();
            Hashtable hsPatientData = new Hashtable();
            int index = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (!allPatient.Contains(info.PatientNO))
                {
                    index = 1;
                    totPage++;
                    ArrayList allPatientData = new ArrayList();
                    allPatient.Add(info.PatientNO);
                    allPatientData.Add(info);
                    hsPatientData.Add(info.PatientNO, allPatientData);
                }
                else
                {
                    ArrayList allPatientData = hsPatientData[info.PatientNO] as ArrayList;
                    allPatientData.Add(info);
                }
                index++;

                if (index == 12)
                {
                    index = 1;
                    totPage++;
                }
            }
            #endregion
           
            this.ShowBillData(alData, drugBillClass, stockDept,totPage);
          
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

                string oX = o1.OrderNO +  o1.CombNO;          //患者姓名
                string oY = o2.OrderNO +  o2.CombNO;          //患者姓名

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
        public void ShowData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept,int totPage)
        {
            this.ShowBillData(alData, drugBillClass, stockDept,totPage);
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
