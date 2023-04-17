using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.Local.DrugStore.FSY.Outpatient
{
    public partial class ucHerbalDrugList : UserControl
    {
        public ucHerbalDrugList()
        {
            InitializeComponent();
        }

        #region 变量


        #endregion

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
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            this.neuPanel3.Dock = DockStyle.None;
            this.neuPanel2.Dock = DockStyle.None;
            this.panel2.Dock = DockStyle.None;
            int addHeight = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows[0].Height);
            int addHeight1 = addHeight * this.neuSpread1_Sheet1.Rows.Count;
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
    
            this.neuPanel2.Dock = DockStyle.None;
            this.neuPanel2.Location = new Point(this.panel1.Location.X, this.panel1.Height +　addHeight1);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            this.panel1.Dock = DockStyle.Top;
            this.panel2.Dock = DockStyle.Fill;
            this.neuPanel2.Dock = DockStyle.Bottom;
            this.neuPanel3.Dock = DockStyle.Fill;
            int height = this.panel1.Height + this.neuPanel2.Height + addHeight1;
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 480, height));
        
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        /// <summary>
        /// 清除控件文字
        /// </summary>
        private void Clear()
        {
            lbName.Text = "";
            lbRecipe.Text = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public void ShowData(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime, int pageNO, int maxPageNO)
        {

            #region 明细信息
            decimal totCost = 0;

            decimal days = 0;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.Rows.Add(0, FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(System.Convert.ToDouble(alData.Count / 2) + 2)));

            //总行数，不要增加，增加需要分页，减少时注意最后一行的汇总信息
            //this.neuSpread1_Sheet1.RowCount = 13;
            int index = 0;
            string memo = "";
            for (int rowIndex = 0; rowIndex < alData.Count && index < alData.Count; rowIndex++)
            {
                //第一个药
                ApplyOut applyOut1 = alData[index] as ApplyOut;

                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = applyOut1.Item.Name;
                this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = applyOut1.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.MinUnit;
                this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut1.Usage.ID);

                FS.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(applyOut1.OrderNO);
                if (order != null && (order.Memo.Contains("自煎") || order.Memo.Contains("代煎") || order.Memo.Contains("方")))
                {
                    if (string.IsNullOrEmpty(memo))
                    {
                        memo = order.Memo;
                    }
                    else if (!memo.Contains(order.Memo))
                    {
                        if (order.Memo.Contains(memo))
                        {
                            memo = order.Memo;
                        }
                        else
                        {
                            memo = memo.TrimEnd(',') + "," + order.Memo;
                        }
                    }
                }
                days = applyOut1.Days;
                if (applyOut1.Item.PackQty == 0)
                {
                    applyOut1.Item.PackQty = 1;
                }
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut1.Item.PriceCollection.RetailPrice * (applyOut1.Operation.ApplyQty * applyOut1.Days / applyOut1.Item.PackQty)).ToString("F2"));
                index++;

                //第二个药
                if (index < alData.Count)
                {
                    ApplyOut applyOut2 = alData[index] as ApplyOut;

                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = applyOut2.Item.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = applyOut2.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut2.Item.MinUnit;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut2.Usage.ID);

                    order = Common.Function.GetOrder(applyOut2.OrderNO);
                    if (order != null && (order.Memo.Contains("自煎") || order.Memo.Contains("代煎") || order.Memo.Contains("方")))
                    {
                        if (string.IsNullOrEmpty(memo))
                        {
                            memo = order.Memo;
                        }
                        else if (!memo.Contains(order.Memo))
                        {
                            if (order.Memo.Contains(memo))
                            {
                                memo = order.Memo;
                            }
                            else
                            {
                                memo = memo.TrimEnd(',') + "," + order.Memo;
                            }
                        }
                    }
                    
                    if (applyOut2.Item.PackQty == 0)
                    {
                        applyOut2.Item.PackQty = 1;
                    }
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut2.Item.PriceCollection.RetailPrice * (applyOut2.Operation.ApplyQty * applyOut2.Days / applyOut2.Item.PackQty)).ToString("F2"));

                    index++;
                }
            }

            //最后一行
            if (maxPageNO > 1)
            {
                this.lbRecipe.Text = "处方号：" + drugRecipe.RecipeNO + "  " + pageNO.ToString() + "/" + maxPageNO.ToString();
                //分页的提示
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).RowSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Value = "注意：本方包含 " + maxPageNO.ToString() + " 页" + "  共 " + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " 剂  " + memo + "总金额："+ totCost.ToString();
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new Font("宋体", 11F, FontStyle.Bold);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Value = "共 " + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " 剂  " + memo + "总金额：" + totCost.ToString();
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new Font("宋体", 11F, FontStyle.Bold);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            this.nlbTotCost.Text = "总药价: " + totCost.ToString() + "元";
            this.nlbPrintTime.Text = "医生: " + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID) + "  "+printTime.ToString();
            #endregion
        }

        public int Print(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime)
        {
            this.Clear();

            if (alData == null || drugRecipe == null)
            {
                return -1;
            }

            #region 标题信息
            //姓名
            lbName.Text = "姓名：" + drugRecipe.PatientName;
            
            //卡号
            lblCardNo.Text = drugRecipe.CardNO;
            
            //处方号
            lbRecipe.Text = drugRecipe.RecipeNo;

            //诊断
            this.lblDiagnose.Text = "诊断：" +  diagnose;

            //年龄
            this.lblAge.Text = "年龄：" + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);

            //性别
            this.lblSex.Text ="性别："+ drugRecipe.Sex.Name;

            //医生
            this.lblDoc.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);

            //打印时间
            this.lblPrintDate.Text = (new FS.FrameWork.Management.DataBaseManger()).GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            #endregion

            //分页
            //decimal maxPageNO = Math.Ceiling((decimal)alData.Count / 25);
            //for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            //{
            //    int count = 25;
            //    if (count + pageNO * 25 > alData.Count)
            //    {
            //        count = alData.Count - pageNO * 25;
            //    }
            //    ArrayList alOnePage = new ArrayList();
            //    alOnePage = alData.GetRange(pageNO * 25, count);
            //    this.ShowData(alOnePage, diagnose, drugRecipe, drugTerminal, hospitalName, printTime, pageNO + 1, (int)maxPageNO);

            //    this.PrintPage();
            this.ShowData(alData,diagnose,drugRecipe,drugTerminal,hospitalName,printTime,1,1);
            this.PrintPage();     
            return 0;
            }
        }


    }
   
    

