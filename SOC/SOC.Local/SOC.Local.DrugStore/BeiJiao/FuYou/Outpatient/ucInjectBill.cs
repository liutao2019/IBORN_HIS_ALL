using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.DrugStore.FuYou.Outpatient
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
    /// </summary>
    public partial class ucInjectBill : UserControl
    {
        #region 变量

        private int iSet = 8;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ucInjectBill()
        {
            InitializeComponent();
        }      

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="alData"></param>
        private void PrintAllPage(ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
           
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount, drugRecipe);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, drugRecipe);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("打印出错!" + e.Message);
                return;
            }
        }

        private void PrintOnePage(ArrayList alData, int current, int total, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            try
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }
                

                //接瓶次数
                int jpNum = 1;
                //赋值并打印
                for (int i = 0; i < alData.Count; i++)
                {
                    Neusoft.HISFC.Models.Pharmacy.ApplyOut info = (Neusoft.HISFC.Models.Pharmacy.ApplyOut)alData[i];

                    Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
                    //Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = outpatientFeeMgr.GetFeeItemList(info.RecipeNO, info.SequenceNO);
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(info.OrderNO);
                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    if (info.CombNO.Length <= 2)
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text = info.CombNO;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text = info.CombNO.Substring(info.CombNO.Length - 2, 2);
                    }
                    this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Name;

                    this.neuSpread1_Sheet1.Cells[0, 2].Text = info.DoseOnce.ToString().TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;//用量
                    this.neuSpread1_Sheet1.Cells[0, 3].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID);//用法
                    string hypoTest = "";
                    if (order != null)
                    {
                        if (order.HypoTest == 1)
                        {
                            hypoTest = "";
                        }
                        else if (order.HypoTest == 2)
                        {
                            hypoTest = "需要皮试";
                        }
                        else if (order.HypoTest == 3)
                        {
                            hypoTest = "阳性";
                        }
                        else if (order.HypoTest == 4)
                        {
                            hypoTest = "阴性";
                        }
                    }
                    this.neuSpread1_Sheet1.Cells[0, 4].Text = hypoTest; //皮试
                    this.neuSpread1_Sheet1.Cells[0, 5].Text = info.Item.Specs;//规格
                    this.neuSpread1_Sheet1.Cells[0, 6].Text = Common.Function.GetFrequenceName(info.Frequency);//次数
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[0, 7].Text = order.HerbalQty.ToString();// 天数
                        this.neuSpread1_Sheet1.Cells[0, 8].Text = order.Memo;//滴速
                    }
                    this.neuSpread1_Sheet1.Cells[0, 9].Text = "";//开始时间
                    this.neuSpread1_Sheet1.Cells[0, 10].Text = "";//执行人
                }

                this.lbInvoiceNo.Text = "发票号：" + drugRecipe.InvoiceNO;
                this.lbCard.Text = drugRecipe.CardNO;
                this.lbName.Text = drugRecipe.PatientName;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //打印时间
                Neusoft.FrameWork.Management.DataBaseManger dataBaseManger = new Neusoft.FrameWork.Management.DataBaseManger();
                this.lbAge.Text = dataBaseManger.GetAge(drugRecipe.Age);
                this.lbSex.Text = drugRecipe.Sex.Name;
                this.lbPage.Text = "第" + current.ToString() + "页" + "/" + "共" + total.ToString() + "页";
                
                //增加fp下面的内容
                this.neuDoctName.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);//医生姓名 
                this.neuChargeOper.Text = "收费员：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.FeeOper.ID); ;//收费员

                PrintPage();
            }
            catch { }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 800, 1100 / 2));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }


        public void PrintDrugBill(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            for (int index = alData.Count - 1; index > -1; index--)
            {
                Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[index] as Neusoft.HISFC.Models.Pharmacy.ApplyOut;
                if (!SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyOut.Usage.ID))
                {
                    alData.RemoveAt(index);
                }
            }
            if (alData.Count > 0)
            {
                PrintAllPage(alData, drugRecipe);
            }
        }

    }
}
