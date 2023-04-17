using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.LSYY.Inpatient
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
        }

        #region 变量

        /// <summary>
        /// 每页的行数，这个是按照Letter纸张调整的，行高改变影响分页
        /// </summary>
        int pageRowNum = 34;

        /// <summary>
        /// 最大页码
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// 打印的有效行数,当选择页码范围时有效
        /// </summary>
        int validRowNum = 0;

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
            this.nlbFirstPrintTime.Text = "首次打印：";
            this.nlbStockDept.Text = "发药科室：";
            this.nlbPrintTime.Text = "打印时间：";

            this.neuSpread1_Sheet1.Rows.Count = 0;
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

            this.nlbTitle.Text = applyDeptName + drugBillClass.Name + "(明细)";

            this.nlbRowCount.Text = "记录数：" + alData.Count.ToString();
            this.nlbBillNO.Text = "单据号：" + drugBillClass.DrugBillNO;
            this.nlbStockDept.Text = "发药科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);

            #region 对同一医嘱按用药时间组合显示

            CompareApplyOutByOrderNO com1 = new CompareApplyOutByOrderNO();
            alData.Sort(com1);

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();

            string orderId = "";//必须得按医嘱流水号排序 
            FS.HISFC.Models.Pharmacy.ApplyOut objLast = null;
            System.Collections.Hashtable hsFrequenceCount = new Hashtable();

            //合并，计算服药次数
            for (int i = alData.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = (alData[i] as FS.HISFC.Models.Pharmacy.ApplyOut);
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
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(obj.UseTime), dt) + " " + objLast.User03;
                    objLast.Operation.ApplyQty += obj.Operation.ApplyQty * obj.Days;//数量相加
                    alData.RemoveAt(i);

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

            #endregion

            #region 按患者排序

            CompareApplyOutByPatient com2 = new CompareApplyOutByPatient();
            alData.Sort(com2);
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

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                string bedNO = info.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                if (drugBillClass.ID == "R")
                {
                    FS.HISFC.Models.RADT.PatientInfo p= inpatientManager.GetPatientInfoByPatientNO(info.PatientNO);
                    if (p != null && p.PVisit.InState.ID.ToString()=="2")
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
                    patientInfo = string.Format("{0}  {1}住院流水号：{2}   年龄：{3}", bedNO, SOC.Public.String.PadRight(privPatientName,8,' '), info.PatientNO, age);
                    //如果不是在本页的第一行，需要患者信息
                    if (iRow % this.pageRowNum != 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                        this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].Border = topBorder;
                        this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
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
                    iRow++;

                }
                //药品信息
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1.SetCellValue(0, iRow, "货位号", info.PlaceNO);
                this.neuSpread1.SetCellValue(0, iRow, "编码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                this.neuSpread1.SetCellValue(0, iRow, "名称", info.Item.Name);
                this.neuSpread1.SetCellValue(0, iRow, "规格", info.Item.Specs);
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
                this.neuSpread1.SetCellValue(0, iRow, "每次用量", info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit);


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
                this.neuSpread1.SetCellValue(0, iRow, "总量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);

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
                this.neuSpread1.SetCellValue(0, iRow, "开方医生", SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.RecipeInfo.ID));
                this.neuSpread1.SetCellValue(0, iRow, "单价", price.ToString("F4").TrimEnd('0').TrimEnd('.'));
                this.neuSpread1.SetCellValue(0, iRow, "金额", (info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice).ToString("F4").TrimEnd('0').TrimEnd('.'));


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

                            this.neuSpread1_Sheet1.AddRows(index, 1);
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;

                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "发药：            核对：            领药：              页：" + page.ToString() + "/" + totPageNO.ToString();
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("宋体", 10f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                            this.neuSpread1_Sheet1.Cells[index, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
                            //标记页码，补打选择页码时用
                            this.neuSpread1_Sheet1.Rows[index].Tag = page;
                            continue;
                        }
                        this.neuSpread1_Sheet1.AddRows(page * pageRowNum, 1);

                        this.neuSpread1_Sheet1.Cells[page * pageRowNum, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
                        this.neuSpread1_Sheet1.Cells[page * pageRowNum, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        this.neuSpread1_Sheet1.Cells[page * pageRowNum, 0].Text = "页：" + page.ToString() + "/" + totPageNO.ToString();

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
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillD", dept);
            //自适应纸张
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 800;

                    int curHeight = 0;

                    int addHeight = this.validRowNum * (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    int additionAddHeight = 120;

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


                oX = o1.BedNO + o1.PatientName + this.GetFrequencySortNO(o1.Frequency)+this.GetOrderNo(o1)+ o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + this.GetFrequencySortNO(o2.Frequency)+this.GetOrderNo(o2) + o2.UseTime.ToString();

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
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名


                oX = o1.OrderNO + o1.UseTime.ToString();
                oY = o2.OrderNO + o2.UseTime.ToString();

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

            SOC.Windows.Forms.PrintPageSelectDialog printPageSelectDialog = new SOC.Windows.Forms.PrintPageSelectDialog();
            printPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
            printPageSelectDialog.MaxPageNO = this.totPageNO;
            printPageSelectDialog.ShowDialog();

            //开始页码为0，说明用户取消打印
            if (printPageSelectDialog.FromPageNO == 0)
            {
                return;
            }

            //打印全部
            if (printPageSelectDialog.FromPageNO == 1 && printPageSelectDialog.ToPageNO == this.totPageNO)
            {
                this.PrintPage();
                return;
            }

            //选择了页
            int curPageNO = 1;
            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                if (this.neuSpread1_Sheet1.Rows[rowIndex].Tag != null)
                {
                    curPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows[rowIndex].Tag) + 1;
                }
                if (curPageNO >= printPageSelectDialog.FromPageNO && curPageNO <= printPageSelectDialog.ToPageNO)
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = false;
                    this.validRowNum--;
                }
            }

            this.PrintPage();

            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
            }

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
