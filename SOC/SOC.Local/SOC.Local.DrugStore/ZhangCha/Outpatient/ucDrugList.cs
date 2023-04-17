using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using FS.FrameWork.Management;
using System.Collections;
//using FS.HISFC.BizLogic.Pharmacy;
//using FS.HISFC.Models.Pharmacy;
//using FS.HISFC.Models.HealthRecord.EnumServer;
//using FS.SOC.Local.DrugStore.Common;

namespace FS.SOC.Local.DrugStore.ZhangCha.Outpatient
{
    public partial class ucDrugList : UserControl
    {
        public ucDrugList()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                Init();                
            }
        }

        #region 变量

        /// <summary>
        /// 总药价
        /// </summary>
        decimal drugListTotalPrice = 0;

        /// <summary>
        /// 最大行数
        /// </summary>
        private int rowMaxCount = 12;

        /// <summary>
        /// 药库管理类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 药品管理类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// 是否打印
        /// </summary>
        private bool isPrint = true;

        string sendWindows = "";

        private bool isMoreOnePage = false;
        #endregion

        private void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string diagnose,string hospitalName)
        {
            if (al.Count > 0)
            {
                //有效的药品页数
                int pageNO = 1;
                //无效的药品页数
                int pageInValidNO = 1;
                //行数
                int iRow = 1;
                int num = 0;

                #region 整理出用于分页打印的有效的和无效的药品信息列表
                System.Collections.Hashtable hsApplyOut = new Hashtable();
                //有效的药品数据
                Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applyOutPageList = new Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
                List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                //无效的药品数据
                Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applyOutInValidPageList = new Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
                List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutInValidList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in al)
                {
                //    if (applyOut.PrintState.ToString() == "1")
                //    {
                //        this.lbReprint.Visible = true;
                //    }
                //    else
                //    {
                //        this.lbReprint.Visible = false;
                //    }
                if(drugRecipe.RecipeState.ToString()=="0")
                {
                    this.lbReprint.Visible=false;    
                }
                else
                {
                    this.lbReprint.Visible=true;
                }

                    //处理同组合打印
                    if (!hsApplyOut.Contains(applyOut.ID))
                    {
                        hsApplyOut.Add(applyOut.ID, null);

                        ArrayList alTmp = new ArrayList();
                        if(!string.IsNullOrEmpty(applyOut.CombNO))
                        {
                            //这个重新查询后包含多余数据
                            //alTmp = this.itemManager.QueryApplyOutListForClinic(applyOut.CombNO);
                        }
                        if (alTmp == null)
                        {
                            alTmp = new ArrayList();
                        }

                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            applyOutList.Add(applyOut);

                            #region 加入同组合药品
                            //分页
                            if(applyOutList.Count==10)
                            {
                                applyOutPageList.Add(pageNO, applyOutList);
                                applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                                pageNO++;
                            }

                            FS.HISFC.Models.Pharmacy.ApplyOut lastApplyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                            foreach (FS.HISFC.Models.Pharmacy.ApplyOut aTmp in alTmp)
                            {
                                //退费数据状态不一样
                                if(aTmp.ID == applyOut.ID)
                                {
                                    //退费后状态改变，这样从数据库查询的数据就是无效的
                                    if (aTmp.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                                    {
                                        return;
                                    }
                                    continue;
                                }
                               

                                //状态相同
                                if (aTmp.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                                {
                                    
                                    lastApplyOut = aTmp;
                                    aTmp.Item.SpecialFlag1 = "┃";
                                    applyOutList.Add(aTmp);
                                    if (!hsApplyOut.Contains(aTmp.ID))
                                    {
                                        hsApplyOut.Add(aTmp.ID, null);
                                    }
                                    //分页
                                    if (applyOutList.Count == 10)
                                    {
                                        applyOutPageList.Add(pageNO, applyOutList);
                                        applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                                        pageNO++;
                                    }

                                }
                            }
                            if (lastApplyOut != null && lastApplyOut.Item != null && !string.IsNullOrEmpty(lastApplyOut.Item.ID))
                            {
                                applyOut.Item.SpecialFlag1 = "┓";
                                lastApplyOut.Item.SpecialFlag1 = "┛";
                            }
                            #endregion
                        }
                        else
                        {
                            applyOutInValidList.Add(applyOut);
                        }
                    }
                    num++;

                    if (applyOutList.Count == 10 || (num == al.Count && applyOutList.Count > 0))
                    {
                        applyOutPageList.Add(pageNO, applyOutList);
                        applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        pageNO++;
                    }

                    if (applyOutInValidList.Count == 5 || (num == al.Count && applyOutInValidList.Count > 0))
                    {
                        applyOutInValidPageList.Add(pageInValidNO, applyOutInValidList);
                        applyOutInValidList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        pageInValidNO++;
                    }
                }

                #endregion

                FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)al[0];

                #region 设置数据并打印
                foreach (KeyValuePair<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applist in applyOutInValidPageList)
                {

                    ////设置总药价和发药时间
                    drugListTotalPrice = 0;
                    iRow = 1;

                    //不打印作废的数据
                }

                foreach (KeyValuePair<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applist in applyOutPageList)
                {
                    // 设置Lable的值
                    SetLableValue(drugRecipe,diagnose,hospitalName);
                    this.isMoreOnePage = (applyOutPageList.Count > 1);
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applayOut in applist.Value)
                    {
                        SetDrugDeatail(iRow, applayOut, true);
                        iRow++;
                    }

                    //设置总药价和发药时间
                    this.AddLastRow();
                    drugListTotalPrice = 0;
                    iRow = 1;

                    this.Print();
                }
                #endregion

                this.isPrint = true;
            }
        }


        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            if (isPrint == true)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill",400, 400));
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsDataAutoExtend = false;
                try
                {
                    //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
                    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                catch { }
                if(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(5, 5, this);
                }
                else
                {
                    print.PrintPage(5, 5, this);
                }
                this.Clear();
            }

            this.isPrint = true;
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }
        
        /// <summary>
        /// 清除控件文字
        /// </summary>
        private void Clear()
        {
            lbName.Text = "姓名:" ;
            lbCardNo.Text = "病历卡号:";
            lbSex.Text = "性别:";
            lbDiagnose.Text = "诊断:";
            lbAge.Text = "年龄:";
            lbDeptName.Text = "科室名称:";
            lbInvoice.Text = "发票号:";
            lbRecipe.Text = "处方号:";
            lbDoctor.Text = "医师:";
            
            this.sendWindows = "";
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 1)
                {
                    for (int i = 1; i < this.rowMaxCount - 1; i++)
                    {
                        this.neuSpread1_Sheet1.SetText(i, 0, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 1, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 2, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 3, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 4, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 5, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 6, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 7, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 8, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 9, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 10, string.Empty);
                        this.neuSpread1_Sheet1.SetText(i, 11, string.Empty);
                    }
                    this.neuSpread1_Sheet1.SetText(this.rowMaxCount - 1, 0, string.Empty);
                }
            }
            catch 
            {
                this.neuSpread1_Sheet1.RowCount = 0;
                this.neuSpread1_Sheet1.RowCount = this.rowMaxCount;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.Clear();

            this.SetFormat();

           
            if (this.neuSpread1_Sheet1.Rows.Count > this.rowMaxCount)
            {
                this.neuSpread1_Sheet1.Rows.Remove(rowMaxCount, this.neuSpread1_Sheet1.Rows.Count - this.rowMaxCount);
            }

            else if (this.rowMaxCount > this.neuSpread1_Sheet1.Rows.Count)
            {
                this.neuSpread1_Sheet1.Rows.Add(rowMaxCount, this.rowMaxCount - this.neuSpread1_Sheet1.Rows.Count);
            }
            else
            {

            }

        }

        /// <summary>
        /// 设置Lable的值
        /// </summary>
        /// <param name="info"></param>
        private void SetLableValue(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe,string diagnose,string hospitalName)
        {

            //this.neuPanName.Text = hospitalName + "药品清单";
            //姓名
            //lbName.Text = "姓名：" + drugRecipe.PatientName;
            lbName.Text = drugRecipe.PatientName.ToString();
            //病案号
            //this.lbCardNo.Text = "病历卡号：" + drugRecipe.CardNO;
            this.lbCardNo.Text = "病历卡号：" + drugRecipe.CardNO.ToString();
            //发票号
            //lbInvoice.Text = "发票号：" + drugRecipe.InvoiceNO;
            lbInvoice.Text = "";
            //性别
            //lbSex.Text = "性别：" + drugRecipe.Sex.Name;
            lbSex.Text = drugRecipe.Sex.Name.ToString();
            //年龄
            //lbAge.Text = "年龄：" + itemManager.GetAge(drugRecipe.Age);
            lbAge.Text = itemManager.GetAge(drugRecipe.Age);
            //医师
            //this.lbDoctor.Text = "医师：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.lbDoctor.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            //处方号
            this.lbRecipe.Text = "处方号：" + drugRecipe.RecipeNO;
            //科室名称
            lbDeptName.Text = "科室名称：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            //诊断
            lbDiagnose.Text = "诊断："+diagnose.ToString();
            //处方日期
            //lbRecipeDate.Text = "处方日期：" + FS.FrameWork.Function.NConvert.ToDateTime(drugRecipe.FeeOper.OperTime).ToString("yyyy-MM-dd");
            lbRecipeDate.Text = FS.FrameWork.Function.NConvert.ToDateTime(drugRecipe.FeeOper.OperTime).ToString("yyyy-MM-dd");
           
        }

        /// <summary>
        /// 将信息添加到列表中
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="info"></param>
        private void SetDrugDeatail(int iRow, FS.HISFC.Models.Pharmacy.ApplyOut info, bool isValid)
        {
            //自定义码
            this.neuSpread1_Sheet1.SetText(iRow, 0, SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
            //药品名
            this.neuSpread1_Sheet1.SetText(iRow, 1, info.Item.Name);
            //规格
            if (string.IsNullOrEmpty(info.Item.SpecialFlag1))
            {
                info.Item.SpecialFlag1 = "  ";
            }
            this.neuSpread1_Sheet1.SetText(iRow, 2, info.Item.SpecialFlag1 + info.Item.Specs);
            //数量
            int outMinQty;
            int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
            if (outPackQty == 0)
            {
                this.neuSpread1_Sheet1.SetText(iRow, 3, ((int)(info.Operation.ApplyQty * info.Days)).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit);
                //单位
                //this.neuSpread1_Sheet1.SetText(iRow, 4, info.Item.MinUnit);
            }
            else if (outMinQty == 0)
            {
                this.neuSpread1_Sheet1.SetText(iRow, 3, outPackQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.PackUnit);
                //单位
                //this.neuSpread1_Sheet1.SetText(iRow, 4, info.Item.PackUnit);
            }
            else
            {
                this.neuSpread1_Sheet1.SetText(iRow, 3, ((int)(info.Operation.ApplyQty * info.Days)).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit);
                //单位
                //this.neuSpread1_Sheet1.SetText(iRow, 4, info.Item.MinUnit);
            }

            ////单价
            //this.neuSpread1_Sheet1.SetText(iRow, 8, ((decimal)(info.Item.PriceCollection.RetailPrice / info.Item.PackQty)).ToString("0.00"));
            ////金额
            //this.neuSpread1_Sheet1.SetText(iRow, 9, ((decimal)((info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * (info.Operation.ApplyQty * info.Days))).ToString("0.00"));
            //总药价
            this.drugListTotalPrice += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * (info.Operation.ApplyQty * info.Days);

            //用法
            //this.neuSpread1_Sheet1.SetText(iRow, 7, SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
            try
            {
                this.neuSpread1_Sheet1.SetText(iRow, 7, SOC.HISFC.BizProcess.Cache.Common.GetUsage(info.Usage.ID).Name);
            }
            catch { }
            //每次用量
            if (info.DoseOnce == 0)
            {
                this.neuSpread1_Sheet1.SetText(iRow, 8, "遵医嘱");
            }
            else
            {
                this.neuSpread1_Sheet1.SetText(iRow, 8, info.DoseOnce.ToString() + info.Item.DoseUnit);
            }

            if (!string.IsNullOrEmpty(info.Frequency.ID))
            {
                //this.neuSpread1_Sheet1.SetText(iRow, 9, info.Frequency.ID.ToLower());
                //this.neuSpread1_Sheet1.SetText(iRow, 9, SOC.LocalDrugStore.Function.GetFrequenceName((FS.HISFC.Models.Order.Frequency)(info.Frequency)));
                this.neuSpread1_Sheet1.SetText(iRow, 9, info.Frequency.ID);
            }
            try
            {
                this.neuSpread1_Sheet1.Cells[iRow,11].Text = FS.SOC.Local.DrugStore.Common.Function.GetOrder(info.OrderNO).HerbalQty.ToString();
            }
            catch { }
            if (info.CombNO.ToString().Length > 2)
            {
                this.neuSpread1_Sheet1.SetText(iRow, 13, info.CombNO.Substring(info.CombNO.Length-3));
            }
            else
            {
                this.neuSpread1_Sheet1.SetText(iRow, 13, "无");
            }
            if (isMoreOnePage)
            {
                this.neuSpread1_Sheet1.Columns[12].Visible = false;
                this.neuSpread1_Sheet1.Columns[13].Visible = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[12].Visible = true;
                this.neuSpread1_Sheet1.Columns[13].Visible = false;
                FS.SOC.HISFC.Components.Common.Function.DrawCombo(this.neuSpread1_Sheet1, 13, 12);
            }

        }

        /// <summary>
        /// 增加最后一行
        /// </summary>
        private void AddLastRow()
        {
            this.neuSpread1_Sheet1.Models.Span.Add(this.rowMaxCount - 1, 0, 1, 12);
            try
            {
                this.neuSpread1_Sheet1.SetText(this.rowMaxCount - 1, 0, string.Format("总药价：￥{0}               打印时间：{1}    {2}",
                           new object[] { this.drugListTotalPrice.ToString("0.00"), itemManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"), this.sendWindows }));
            }
            catch { }
            FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, true);
            this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Border = lineBorder11;
            this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }

        /// <summary>
        /// FarPoint格式
        /// </summary>
        private void SetFormat()
        {
            //FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder4 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder5 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder6 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder7 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder8 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder9 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder10 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
            //FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);

            //FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            //t.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "编码";
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).Border = lineBorder2;
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "药品名称";
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).Border = lineBorder3;
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "  规格";
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).Border = lineBorder4;
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "数量";
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).Border = lineBorder5;
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "单位";
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).Border = lineBorder8;
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "用法";

            //this.neuSpread1_Sheet1.Cells.Get(0, 6).Border = lineBorder9;
            //this.neuSpread1_Sheet1.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "每次用量";
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).Border = lineBorder10;
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "频次";

            //this.neuSpread1_Sheet1.Cells.Get(0, 8).Border = lineBorder6;
            //this.neuSpread1_Sheet1.Cells.Get(0, 8).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 8).Value = "单价";
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).Border = lineBorder7;
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 9).Value = "金额";
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).Border = lineBorder11;
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 10).Value = "天数";
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).Border = lineBorder11;
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Cells.Get(0, 11).Value = "备注";
            //this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(0).Width = 55F;
            //this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(1).Width = 165F;
            //this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(2).Width = 100F;
            //this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(3).Width = 40F;
            //this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(4).Width = 35F;
            //this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(5).Width = 80F;
            //this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(6).Width = 75F;
            //this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(7).Width = 45F;
            //this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(8).Width = 55F;
            //this.neuSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(9).Width = 65F;
            //this.neuSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(10).Width = 0F;
            //this.neuSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.Columns.Get(11).Width = 100F;
            //this.neuSpread1_Sheet1.Columns.Get(11).CellType = t;
            //this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            //this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
        }

        /// <summary>
        /// 打印配药清单
        /// </summary>
        /// <param name="alData">出库申请实体</param>
        /// <param name="diagnose">诊断</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">终端信息</param>
        /// <returns></returns>
        public int PrintDrugBill(ArrayList alData, string diagnose, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal,string hospitalName)
        {
            if (alData == null || drugRecipe == null)
            {
                return -1;
            }
            this.Init();
            //this.npbBarCode.Image = this.CreateBarCode(drugRecipe.RecipeNO);
            this.AddAllData(alData, drugRecipe,diagnose,hospitalName);

            return 0;
        }

    }
}
