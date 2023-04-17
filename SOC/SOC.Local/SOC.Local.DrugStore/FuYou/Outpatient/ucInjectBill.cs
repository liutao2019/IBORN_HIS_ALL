using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.Local.DrugStore.FuYou.Outpatient
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
    /// </summary>
    public partial class ucInjectBill : UserControl
    {
        #region 变量

        private int iSet = 8;

        /// <summary>
        /// 当前显示组号
        /// </summary>
        private int showGroupNO = 0;

        /// <summary>
        /// 当前记录的组合号
        /// </summary>
        private string rememberComboNO = "";

        /// <summary>
        /// 合并第一列单元格的起始行号
        /// </summary>
        private int spanRowIndex = 1;

        private decimal zhenChaFee = 0m;

        /// <summary>
        ///常数维护业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
                    
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
        private void PrintAllPage(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
           
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));

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

        private void PrintOnePage(ArrayList alData, int current, int total, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            this.lblZhenChaFee.Visible = false;
            if (zhenChaFee>0)
            {
                this.lblZhenChaFee.Visible = true;
                this.lblZhenChaFee.Text =string.Format( "该患者已支付急诊监护费({0}元)",zhenChaFee);
            }
            try
            {
                spanRowIndex = 0;
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }

                //设置对齐
                for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    }

                    this.neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }

                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Gray, 1, false, false, false, true);
                FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);

                //赋值并打印
                for (int i = 0; i < alData.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)alData[i];

                    FS.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
                    //FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = outpatientFeeMgr.GetFeeItemList(info.RecipeNO, info.SequenceNO);
                    FS.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(info.OrderNO);
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = bottomBorder;


                    //修改组号
                    if (info.CombNO != rememberComboNO)
                    {
                        rememberComboNO = info.CombNO;
                        spanRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                        showGroupNO++;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    }

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = showGroupNO.ToString();

                   
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = info.Item.Name;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;//用量
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(info.Usage.ID)).Name;//用法
                    }
                    catch { }
                    string hypoTest = "";

                    //妇幼 廖科 要求屏蔽 所以不再显示皮试信息
                    //if (order != null)
                    //{
                    //    if (order.HypoTest == 1)
                    //    {
                    //        hypoTest = "免皮试";
                    //    }
                    //    else if (order.HypoTest == 2)
                    //    {
                    //        hypoTest = "需要皮试";
                    //    }
                    //    else if (order.HypoTest == 3)
                    //    {
                    //        hypoTest = "阳性";
                    //    }
                    //    else if (order.HypoTest == 4)
                    //    {
                    //        hypoTest = "阴性";
                    //    }
                    //}
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = hypoTest; //皮试
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = info.Item.Specs;//规格
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = info.Frequency.ID;//次数
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = order.HerbalQty.ToString();// 天数

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Memo;//滴速
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text =info.Operation.ApplyQty.ToString() + info.Item.MinUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = " ";//开始时间
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 11].Text = " ";//执行人

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }

                this.lbInvoiceNo.Text = "发票号：" + drugRecipe.InvoiceNO;
                this.lbCard.Text = drugRecipe.CardNO;
                this.lbName.Text = drugRecipe.PatientName;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //打印时间
                FS.FrameWork.Management.DataBaseManger dataBaseManger = new FS.FrameWork.Management.DataBaseManger();
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 800, 1110 / 3));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }


        public void PrintDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            usageHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            FS.HISFC.BizLogic.Fee.Outpatient feeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

            ArrayList alIM= new ArrayList();    //IM肌注，单独打一张注射单，其他用法打另外一张注射单
            ArrayList alOther = new ArrayList(); //除肌注以外的药品
            ArrayList alSpecial = new ArrayList();//已经维护的需要特别打单的药品

            
            ArrayList alFeeItemList = feeMgr.QueryFeeItemListsByInvoiceNO(drugRecipe.InvoiceNO);
            if (alFeeItemList!=null&&alFeeItemList.Count>0)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFeeItemList)
                {
                    if (item.Item.ID == "F00000040341")   //此处写死了，如果项目加多，可以改为维护，但是lable显示内容要修改
                    {
                        zhenChaFee += item.FT.OwnCost + item.FT.PubCost + item.FT.PayCost;
                    }
                }
            }

              ArrayList alSpecialInjectBill = constMgr.GetAllList("SpecialInjectBill");

            for (int index = alData.Count - 1; index > -1; index--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[index] as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (!SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyOut.Usage.ID))
                {
                    alData.RemoveAt(index);
                }
                else
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = FS.SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
                    if (order != null)
                    {
                        applyOut.CombNO = order.SubCombNO.ToString();
                    }
                    else
                    {
                        applyOut.CombNO = "";
                        return; //手工方不打印,找不到处方信息即认为是手工方
                    }

                    #region 肌注和其他用法分单打印
                    FS.FrameWork.Models.NeuObject neuObject = usageHelper.GetObjectFromID(applyOut.Usage.ID);
                    if (neuObject == null)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
                    if (usage == null)
                    {
                        continue;
                    }

                    //[2011-6-20]zhaozf 有三个术中用的药品要单独打单
                    bool isSpecial = false;
                    if (alSpecialInjectBill!=null&&alSpecialInjectBill.Count>0)
                    {
                        foreach (FS.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
                        {
                            if (cnst.ID==applyOut.Item.ID)
                            {
                                alSpecial.Add(applyOut);
                                isSpecial = true;
                                break;
                            }
                        }
                    }
                    if (!isSpecial)
                    {

                        if (usage.UserCode == "IM")//肌肉注射
                        {
                            alIM.Add(applyOut);
                        }
                        else
                        {
                            alOther.Add(applyOut);
                        }
                    }
                    #endregion
                }
            }

            if (alSpecial.Count>0)
            {
                alSpecial.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(alSpecial, drugRecipe);
            }

            if (alIM.Count > 0)
            {
                //按处方排序
                alIM.Sort(new CompareApplyOutByCombNO());

                PrintAllPage(alIM, drugRecipe);
            }
            if (alOther.Count>0)
            {
                 //按处方排序
                alOther.Sort(new CompareApplyOutByCombNO());

                PrintAllPage(alOther, drugRecipe);
            }
        }     
    }

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
                //nComp = string.Compare(oX.ToString(), oY.ToString());
                nComp = NConvert.ToInt32(oX) - NConvert.ToInt32(oY);
            }

            return nComp;
        }

    }
    #endregion
}
