using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Linq;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient
{
    /// <summary>
    /// [功能描述: 住院药房出院带药处方单本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// </summary>    
    public partial class ucAnestheticByPatientDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        public ucAnestheticByPatientDrugBill()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

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
            this.neuLabel4.Text = "规格：";
            this.neuLabel5.Text = "总量：";
        }

        /// <summary>
        /// 初始化Fp
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreAnesDrugBill.xml");
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
            string orderId = "";//必须得按医嘱流水号排序 
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Pharmacy.ApplyOut objLast = null;
            System.Collections.Hashtable hsFrequenceCount = new Hashtable();
            alData.Sort(new CompareApplyOutByOrderNO());

            //合并，计算服药次数
            for (int i = alData.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = (alData[i] as FS.HISFC.Models.Pharmacy.ApplyOut);
                obj.User01 = FS.FrameWork.Function.NConvert.ToInt32(!FS.SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(obj.StockDept.ID, obj.Item.ID)).ToString();
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

            int index = 1;
            //下7行 因为医院要求毒麻方 按药品汇总 更改 BY FZC 2014-10-03
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            //{
            //    this.SetDetailInfo(drugBillClass,info,index,alData.Count);
            //    this.PrintPage();
            //    this.Clear();
            //    index++;
            //}

            //将申请数据转换为ILIST便于过滤
            IList<FS.HISFC.Models.Pharmacy.ApplyOut> applyList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut a in alData)
            {
                applyList.Add(a);
            }
            if (applyList.Count < 1)
            {
                return;
            }
            //按药品号分组
            var listByDrug = applyList.GroupBy(o => o.Item.ID);
            int pageNum = 0;
            foreach (var drugGroup in listByDrug)
            {
                if (drugGroup.Count() > 10)//单方最多能打10个药
                {
                    int yu = 0;
                    int num = System.Math.DivRem(drugGroup.Count(), 10, out yu);
                    if (yu == 0)
                    {
                        pageNum += num;
                    }
                    else
                    {
                        pageNum += (num + 1);
                    }
                }
                else
                {
                    pageNum++;
                }
            }
            foreach (var drugGroup in listByDrug)
            {
                if (drugGroup.Count() <= 10)
                {
                    this.SetDetailData(drugBillClass, drugGroup.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>(), index, pageNum);
                    this.PrintPage();
                    this.Clear();
                    index++;
                }
                else
                {
                    int yu = 0;
                    int num = System.Math.DivRem(drugGroup.Count(), 10, out yu);
                    IList<FS.HISFC.Models.Pharmacy.ApplyOut> applyListSplit = null;
                    
                    for (int i = 0; i < num; i++)
                    {
                        applyListSplit = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        for (int j = 0; j < 10; j++)
                        {
                            applyListSplit.Add(drugGroup.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>()[j + i * 10]);
                        }
                        this.SetDetailData(drugBillClass, applyListSplit, index, pageNum);
                        this.PrintPage();
                        this.Clear();
                        index++;
                    }

                    if (yu != 0)
                    {
                        applyListSplit = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        for (int k = 0; k < yu; k++)
                        {
                            applyListSplit.Add(drugGroup.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>()[k + num * 10]);
                        }

                        this.SetDetailData(drugBillClass, applyListSplit.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>(), index, pageNum);
                        this.PrintPage();
                        this.Clear();
                        index++;
                    }
                }
            }
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

        /// <summary>
        /// 新版的能按药品汇总的毒麻明细打印方法
        /// </summary>
        /// <param name="drugBillClass"></param>
        /// <param name="applyList"></param>
        /// <param name="curPageNO"></param>
        /// <param name="TotPageNO"></param>
        private void SetDetailData(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, IList<FS.HISFC.Models.Pharmacy.ApplyOut> applyList, int curPageNO, int TotPageNO)
        {

            string sendType = this.GetSendType(applyList[0] as FS.HISFC.Models.Pharmacy.ApplyOut);

            #region 判断是否出院带药、补打
            if (drugBillClass.ID.Contains("O"))
            {
                this.lbTitle.Text = "(出院带药)" + applyList[0].Item.Name;//+ "(" + sendType + ")";
                // {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
            }
            else
            {
                this.lbTitle.Text = applyList[0].Item.Name;//+ "(" + sendType + ")";
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            {                
                if (!this.lbTitle.Text.Contains("补打"))
                {
                    this.lbTitle.Text = "(补打)" + this.lbTitle.Text;
                }
                this.nlbReprint.Visible = false;
            }
            else
            {
                this.nlbReprint.Visible = false;
            }
            #endregion

            #region 设置打印样式
            this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            this.nlbSpecs.Text = applyList[0].Item.Specs.PadRight(30, ' ');

            this.nlbPageNO.Text = "第" + curPageNO + "页，" + "共" + TotPageNO + "页";

            this.nlbDrugDate.Text = "配药时间：" + applyList[0].Operation.ExamOper.OperTime.ToString();

            this.nlbPrintDate.Text = "打印时间" + inPatientMgr.GetDateTimeFromSysDateTime();

            //先计算总量 判断发药单位
            decimal totalNum = 0;
            foreach (var apply in applyList)
            {
                totalNum += apply.Operation.ApplyQty * apply.Days;
            }

            int outMinQty = 0;
            int outPackQty = System.Math.DivRem((int)totalNum, (int)applyList[0].Item.PackQty, out outMinQty);

            string total = string.Empty;
            if (outMinQty == 0)
            {
                this.nlbUnit.Text = outPackQty + applyList[0].Item.PackUnit;
            }
            else
            {
                this.nlbUnit.Text = totalNum + applyList[0].Item.MinUnit;
            }

            #endregion

            for (int i = 0; i < applyList.Count; i++)
            {
                this.neuSpread1_Sheet1.Rows.Count++;
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.QueryPatientInfoByInpatientNONew(applyList[i].PatientNO);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "日期", applyList[i].Operation.ExamOper.OperTime.ToShortDateString());

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "序号", i + 1);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "性别", patientInfo.Sex.Name);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "年龄", inPatientMgr.GetAge(patientInfo.Birthday));

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "科室", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(patientInfo.PVisit.PatientLocation.Dept.ID));

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "身份证号", patientInfo.IDCard);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "住院号", patientInfo.PID.PatientNO);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "诊断", Common.Function.GetInpatientDiagnose(patientInfo.PID.ID).Replace("-1", string.Empty));

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "处方医生", applyList[i].RecipeInfo.ID);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "数量", applyList[i].Operation.ApplyQty);
                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "姓名", patientInfo.Name);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "审核人", string.Empty);

            }
            this.neuPanel12.Height = this.neuSpread1_Sheet1.Rows.Count * 15;
        }

        /// <summary>
        /// 这个是以前的方法 只能打一条药品
        /// </summary>
        /// <param name="drugBillClass"></param>
        /// <param name="info"></param>
        /// <param name="curPageNO"></param>
        /// <param name="TotPageNO"></param>
        private void SetDetailInfo(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.HISFC.Models.Pharmacy.ApplyOut info, int curPageNO, int TotPageNO)
        {

            if (drugBillClass.ID.Contains("O"))
            {
                this.lbTitle.Text = "(出院带药)" + info.Item.Name;
            }
            else
            {
                this.lbTitle.Text = info.Item.Name;
            }

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

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.QueryPatientInfoByInpatientNONew(info.PatientNO);

            this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            this.nlbSpecs.Text = info.Item.Specs.PadRight(30, ' ');

            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //总量
            decimal applyQty = info.Operation.ApplyQty;
            string unit = info.Item.MinUnit;
            decimal price = 0m;

            int outMinQty;
            int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
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

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "日期", info.Operation.ExamOper.OperTime.ToShortDateString());

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "序号", "1");

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "性别", patientInfo.Sex.Name);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "年龄", inPatientMgr.GetAge(patientInfo.Birthday));

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "科室", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(patientInfo.PVisit.PatientLocation.Dept.ID));

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "身份证号", patientInfo.IDCard);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "住院号", patientInfo.PID.PatientNO);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "诊断", Common.Function.GetInpatientDiagnose(patientInfo.PID.ID).Replace("-1", string.Empty));

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "处方医生", info.RecipeInfo.ID);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "数量", applyQty);
            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "姓名", patientInfo.Name);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "审核人", string.Empty);

            this.nlbPageNO.Text = "第" + curPageNO + "页，" + "共" + TotPageNO + "页";

            this.nlbDrugDate.Text = "配药时间：" + info.Operation.ExamOper.OperTime.ToString();

            this.nlbPrintDate.Text = "打印时间" + inPatientMgr.GetDateTimeFromSysDateTime();

            this.nlbUnit.Text = unit;
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
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillN", "ALL");
            //自适应纸张
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 870;

                    //int curHeight = 0;

                    //int addHeight = (this.neuSpread1.ActiveSheet.RowCount - 1) *
                    //    (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    //int additionAddHeight = 180;

                    paperSize.Width = width;
                    paperSize.Height = 550;//(addHeight + curHeight + additionAddHeight);
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
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方;
            }
        }

        #endregion

    }

    /// <summary>
    /// 排序类
    /// </summary>
    public class CompareApplyOutByOrderNO : IComparer
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


            oX = o1.BedNO + o1.PatientName + o1.OrderNO + o1.UseTime.ToString();
            oY = o2.BedNO + o2.PatientName + o2.OrderNO + o2.UseTime.ToString();

            return string.Compare(oX, oY);
        }
    }
}
