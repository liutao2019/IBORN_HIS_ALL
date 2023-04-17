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
    /// [功能描述: 住院药房明细单打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-6]<br></br>
    /// 说明：
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

        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// 每页的行数，这个是按照Letter纸张调整的，行高改变影响分页
        /// </summary>
        int pageRowNum = 15;

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
            this.lbPageNO.Visible = false;
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
            FarPoint.Win.LineBorder noneBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, true, true);

            string applyDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            this.nlbTitle.Text = applyDeptName + drugBillClass.Name + "(明细)";

            this.nlbRowCount.Text = "记录数：" + alData.Count.ToString();
            this.nlbBillNO.Text = "单据号：" + drugBillClass.DrugBillNO;
            this.nlbStockDept.Text = "发药科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);

            #region 对同一医嘱按用药时间组合显示

            CompareApplyOutByOrderNO compareApplyOutByOrderNO = new CompareApplyOutByOrderNO();
            alData.Sort(compareApplyOutByOrderNO);

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();

            string orderId = "";//必须得按医嘱流水号排序 
            FS.HISFC.Models.Pharmacy.ApplyOut objLast = null;
            System.Collections.Hashtable hsFrequenceCount = new Hashtable();
            string drugCode = "";//药品编码

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
                    drugCode = obj.Item.ID;
                    objLast = obj;
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.UseTime), dt);
                }
                else if (orderId == obj.OrderNO && !needAdd&&drugCode==obj .Item .ID)//是一个药
                {
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(obj.UseTime), dt) + " " + objLast.User03;
                    objLast.Operation.ApplyQty += obj.Operation.ApplyQty;//数量相加
                    alData.RemoveAt(i);

                }
                else
                {
                    orderId = obj.OrderNO;
                    drugCode = obj.Item.ID;
                    objLast = obj;
                    if (needAdd)
                    {
                        obj.Frequency.Name = "另加";
                    }
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.UseTime), dt);
                }
                if (obj.OrderType.ID != "CZ"&&!string.IsNullOrEmpty(obj .OrderType .ID))
                {
                    objLast.User03 = "";
                }
            }

            #endregion

            #region 按患者排序

            CompareApplyOutByPatient compareApplyOutByPatient = new CompareApplyOutByPatient();
            alData.Sort(compareApplyOutByPatient);
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

                    FS.SOC.Local.DrugStore.ShenZhen.BinHai.Inpatient.PrintInterfaceImplement temp = new PrintInterfaceImplement();
                    string diag = temp.GetDiagnose(info.PatientNO);
                    patientInfo = string.Format("{0}  {1}  住院号：{2}   年龄：{3}    诊断：{4}", bedNO+"床", SOC.Public.String.PadRight(privPatientName, 8, ' '), p.PID.PatientNO, age,diag);//info.PatientNO
                    //<<{EDFE0154-EE68-45c5-A9E1-52BE961CB3EC}
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

                //{EDFE0154-EE68-45c5-A9E1-52BE961CB3EC}每页的第一行都需要患者信息
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
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1.SetCellValue(0, iRow, "货位号", SOC.Local.DrugStore.ShenZhen.Common.Function.GetStockPlaceNO(info));//货位号
                this.neuSpread1.SetCellValue(0, iRow, "编码", item.GBCode);
                this.neuSpread1.SetCellValue(0, iRow, "药品名称", info.Item.Name + "(" + item.NameCollection.EnglishName + ")");
                #region 针剂解释
                #endregion
                this.neuSpread1.SetCellValue(0, iRow, "规格", info.Item.Specs);
                if (string.IsNullOrEmpty(info.Usage.Name))
                {
                    this.neuSpread1.SetCellValue(0, iRow, "用法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, iRow, "用法", info.Usage.Name);
                }
                //FS.HISFC.Models.Base.Const Usage = SOC.HISFC.BizProcess.Cache.Common.GetUsage(info.Usage.ID);
                //if
                //this.neuSpread1.SetCellValue(0, iRow, "用法", Usage.Memo);
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
                decimal applyQty = info.Operation.ApplyQty;
                string unit = info.Item.MinUnit;
                decimal price = 0m;

                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty), (int)info.Item.PackQty, out outMinQty);
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
                            info.Item.PackUnit = item.PackUnit;
                        }
                        catch { }
                    }
                }
                if (outPackQty == 0)
                {
                    applyQty = info.Operation.ApplyQty;
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
                //if (drugBillClass.ID == "R")
                //{
                //     string drugTime = string.Empty;

                //     this.neuSpread1.SetCellValue(0, iRow, "用药时间", info.UseTime.Month.ToString() + "-" + info.UseTime.Day.ToString() + " " + info.UseTime.Hour.ToString() + ":00");
                //}
                //else
                //{
                    #region 用药时间解释
                    string drugTime = string.Empty;
                    if (info.User03.Length == 6)
                    {
                        drugTime = info.User03.Substring(0, 2).ToString() + "-" + info.User03.Substring(2, 2).ToString() + " " + info.User03.Substring(4, 2).ToString() + ":00";
                    }
                    else
                    {
                        drugTime = info.User03;
                    }
                    #endregion
                    this.neuSpread1.SetCellValue(0, iRow, "用药时间", drugTime);
                    //this.neuSpread1.SetCellValue(0, iRow, "备注", info.Memo);
                //}

                //>>{EDFE0154-EE68-45c5-A9E1-52BE961CB3EC}备注显示申请人+申请科室20121122KJL
				//info.Operation.ApplyOper.ID + "/" + 
                this.neuSpread1.SetCellValue(0, iRow, "备注", SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.RecipeInfo.Dept.ID));
                //<<
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
                    if (i == 1 || i == 2)//&& (iRow == 0 || iRow == 1)
                    {
                        FarPoint.Win.LineBorder tempBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.White, 1, true, true, true, true);
                        this.neuSpread1_Sheet1.Cells.Get(iRow, i).Border = tempBorder;
                    }
                    else if (i == 3)
                    {
                        FarPoint.Win.LineBorder tempBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, true, false);
                        this.neuSpread1_Sheet1.Cells.Get(iRow, i).Border = tempBorder;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells.Get(iRow, i).Border = noneBorder;
                    }
                }

                iRow++;
            }
            #endregion

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            #region 设置底部文字

            if (drugBillClass.ID == "R")
            {
                neuLabel3.Text = "申请退药人：";
                neuLabel1.Text = "退药人：";
                neuLabel2.Text = "核准人：            会计：                        部门总经理：";

            }
            else
            {
                neuLabel3.Text = "调配：";
                neuLabel1.Text = "发药：";
                neuLabel2.Text = "领药：";
            
            
            }
           
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
                    pageSize = new FS.HISFC.Models.Base.PageSize("InPatientDrugDetail", 850, 550);
                }
            }

            //打印边距处理：将维护的上边距作为下边距，这样控制页尾打印的空白，保证数据完全打印
            print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, 0, pageSize.Top);

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


                oX = o1.BedNO + o1.PatientName + this.GetFrequencySortNO(o1.Frequency) + this.GetOrderNo(o1) + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + this.GetFrequencySortNO(o2.Frequency) + this.GetOrderNo(o2) + o2.UseTime.ToString();

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
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细;
            }
        }

        #endregion
    }
}
