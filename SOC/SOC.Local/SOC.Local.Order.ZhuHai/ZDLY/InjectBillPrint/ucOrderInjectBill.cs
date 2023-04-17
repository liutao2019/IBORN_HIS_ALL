using System;
using System.Collections.Generic;
using System.Linq;   //后来添加的
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDLY.InjectBillPrint
{
    /// <summary>
    /// 中大六院注射单打印
    /// </summary>
    public partial class ucOrderInjectBill : UserControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ucOrderInjectBill()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        private int iSet = 6;

        /// <summary>
        /// 合并第一列单元格的起始行号
        /// </summary>
        private int spanRowIndex = 1;

        /// <summary>
        /// 诊查费
        /// </summary>
        private decimal zhenChaFee = 0m;

        /// <summary>
        /// 组合号
        /// </summary>
        private Hashtable comboHash = new Hashtable();

        /// <summary>
        ///常数维护业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Constant constMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 费用业务类
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
        #endregion


        /// <summary>
        /// 打印所有院注射单
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        private void PrintAllPage(ArrayList alData, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {
            GetHospLogo();
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));
                for (int i = 1; i <= icount; i++)
                {
                    //if (i != icount)
                    //{
                    //alPrint = alData.GetRange(iSet * (i - 1), iSet);
                    alPrint = alData;
                    this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, isPreview);
                    //}
                    //else
                    //{
                    //    int num = alData.Count % iSet;
                    //    if (alData.Count % iSet == 0)
                    //    {
                    //        num = iSet;
                    //    }
                    //    //alPrint = alData.GetRange(iSet * (i - 1), num);
                    //    this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct);
                    //}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("打印出错!" + e.Message);
                return;
            }
        }

        /// <summary>
        /// 打印一个院注射单
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="current"></param>
        /// <param name="total"></param>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        private void PrintOnePage(ArrayList alData, int current, int total, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {


            this.comboHash = new Hashtable();

            try
            {
                spanRowIndex = 0;

                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }


                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                decimal tmpCharge = 0.0M;
                this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Border = bottomBorder;

                ArrayList alFee = new ArrayList();
                if (alData.Count > 0)
                {
                    alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(regObj.ID, ((Neusoft.HISFC.Models.Order.OutPatient.Order)alData[0]).ReciptSequence, "ALL");
                }

                //赋值并打印
                for (int i = 0; i < alData.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)alData[i];

                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);


                    if (!comboHash.Contains(order.Combo.ID))
                    {
                        if (alFee != null && alFee.Count >= 1)
                        {
                            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                            {
                                if (itemlist.Item.IsMaterial && itemlist.Order.Combo.ID == order.Combo.ID)
                                {
                                    tmpCharge += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//注射费
                                }

                            }
                        }
                        comboHash.Add(order.Combo.ID, order.Combo.ID);
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = order.SubCombNO.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name + "[" + order.Item.Specs + "]" + outOrderMgr.TransHypotest(order.HypoTest);//药品名+规格
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//用法

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = order.Frequency.Name;//用药时间
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;//用量
                    }
                    catch
                    {
                    }

                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = order.InjectCount.ToString();// 天数
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = " ";//补充用法
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = " ";//嘱托
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Combo.ID.ToString();//嘱托
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = order.Memo;//备注
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));


                    //控制显示
                    if (neuSpread1_Sheet1.RowCount - 1 >= (current - 1) * iSet &&
                        neuSpread1_Sheet1.RowCount - 1 < current * iSet)
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = false;
                    }
                }

                this.lbCard.Text = regObj.PID.CardNO;//流水号

                this.npbBarCode.Image = SOC.Public.Function.CreateBarCode(regObj.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
                this.lbName.Text = regObj.Name;//姓名
                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //打印时间

                this.lbAge.Text = constMgr.GetAge(regObj.Birthday);//年龄
                this.lbSex.Text = regObj.Sex.Name;//性别
                this.lblPage.Text = current.ToString() + "/" + total.ToString() + "页";

                this.lbInvoiceNo.Text = SOC.HISFC.BizProcess.Cache.Common.GetDept(reciptDept.ID).Name;

                ////增加fp下面的内容
                this.neuDoctName.Text = "医生签名：" + reciptDoct.ID + "/" +
                    SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.ID);//医生姓名 
                this.neuCharge.Text = "注射费：" + tmpCharge.ToString();//记账 temp

                //画组合号
                Neusoft.HISFC.Components.Common.Classes.Function.DrawCombo(neuSpread1_Sheet1, 9, 2);
                if (!isPreview)
                {
                    PrintPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印注射单出错！\r\n" + ex.Message);
            }
        }




        private void PrintOnePage(IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, int current, int total, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {


            this.comboHash = new Hashtable();

            try
            {
                spanRowIndex = 0;

                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }


                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                decimal tmpCharge = 0.0M;
                this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Border = bottomBorder;

                //赋值并打印
                for (int i = 0; i < orderList.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)orderList[i];

                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);


                    if (!comboHash.Contains(order.Combo.ID))
                    {
                        ArrayList alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(order.Combo.ID, regObj.ID, "ALL");
                        if (alFee != null && alFee.Count >= 1)
                        {
                            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                            {
                                if (itemlist.Item.IsMaterial)
                                {
                                    tmpCharge += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//注射费
                                }

                            }
                        }
                        comboHash.Add(order.Combo.ID, order.Combo.ID);
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = order.SubCombNO.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name + "[" + order.Item.Specs + "]" + outOrderMgr.TransHypotest(order.HypoTest);//药品名+规格
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//用法

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = order.Frequency.Name;//用药时间
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;//用量
                    }
                    catch
                    {
                    }

                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = order.InjectCount.ToString();// 天数
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = " ";//补充用法
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = " ";//嘱托
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Combo.ID.ToString();//嘱托
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = order.Memo;//备注
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));


                    //控制显示
                    if (neuSpread1_Sheet1.RowCount - 1 >= (current - 1) * iSet &&
                        neuSpread1_Sheet1.RowCount - 1 < current * iSet)
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = false;
                    }
                }

                this.lbCard.Text = regObj.PID.CardNO;//流水号
                this.npbBarCode.Image = SOC.Public.Function.CreateBarCode(regObj.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
                this.lbName.Text = regObj.Name;//姓名

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //打印时间

                this.lbAge.Text = constMgr.GetAge(regObj.Birthday);//年龄
                this.lbSex.Text = regObj.Sex.Name;//性别
                this.lblPage.Text = current.ToString() + "/" + total.ToString() + "页";

                this.lbInvoiceNo.Text = SOC.HISFC.BizProcess.Cache.Common.GetDept(reciptDept.ID).Name;

                ////增加fp下面的内容
                this.neuDoctName.Text = "医生签名：" + reciptDoct.ID + "/" +
                    SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.ID);//医生姓名 
                this.neuCharge.Text = "注射费：" + tmpCharge.ToString();//记账 temp

                //画组合号
                Neusoft.HISFC.Components.Common.Classes.Function.DrawCombo(neuSpread1_Sheet1, 9, 2);
                if (!isPreview)
                {
                    PrintPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印注射单出错！\r\n" + ex.Message);
            }
        }


        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();

            print.IsLandScape = true;

            print.SetPageSize(Neusoft.SOC.Local.Order.ZhuHai.ZDLY.Common.Function.GetPrintPage(true));

            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsDataAutoExtend = false;
            //管理员或者是补打就调用预览打印
            if (Neusoft.SOC.Local.Order.ZhuHai.ZDLY.Common.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }


        #region IOutPatientOrderPrint 成员
        /// <summary>
        /// 实现打印接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IList"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return -1;
        }

        /// <summary>
        /// 医生站打印注射单
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            List<Neusoft.HISFC.Models.Order.OutPatient.Order> dayorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            string type = "Q";

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> injectDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInnerInjectUsage(order.Usage.ID))
                {
                    type = Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetUsageSystemType(order.Usage.ID) == "IVD" ? "IVD" : "Q";

                    if (injectDictionary.ContainsKey(type))
                    {
                        injectDictionary[type].Add(order);
                    }
                    else
                    {
                        dayorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                        dayorderList.Add(order);
                        injectDictionary.Add(type, dayorderList);
                    }
                }
            }
            foreach (string usage in injectDictionary.Keys)
            {
                ArrayList printList = new ArrayList(injectDictionary[usage]);

                printList.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(printList, regObj, reciptDept, reciptDoct, isPreview);
            }

            /*
            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> PrintDic = null;
            foreach (string days in injectDictionary.Keys)
            {
                PrintDic = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
                for (int t = 0; t < injectDictionary[days].Count; t++)
                {
                    if (PrintDic.ContainsKey(injectDictionary[days][t].Usage.ID))
                    {
                        PrintDic[injectDictionary[days][t].Usage.ID].Add(injectDictionary[days][t]);

                    }
                    else
                    {
                        List<Neusoft.HISFC.Models.Order.OutPatient.Order> usageorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                        usageorderList.Add(injectDictionary[days][t]);
                        PrintDic.Add(injectDictionary[days][t].Usage.ID, usageorderList);
                    }
                    
                }

                foreach (string usage in PrintDic.Keys)
                {
                    ArrayList printList = new ArrayList(PrintDic[usage]);

                    printList.Sort(new CompareApplyOutByCombNO());

                    PrintAllPage(printList, regObj, reciptDept, reciptDoct);
                }

            }
            */
            return 1;
        }
        #endregion

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // 单元格重绘
            Pen pp = new Pen(Color.Red);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1);
        }

        private void GetHospLogo()
        {
            try
            {
                System.IO.MemoryStream image = new System.IO.MemoryStream(((Neusoft.HISFC.Models.Base.Hospital)this.outOrderMgr.Hospital).HosLogoImage);
                this.picLogo.Image = Image.FromStream(image);

            }
            catch
            {

            }
        }

        private void ucOrderInjectBill_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.Columns[6].Visible = false;
            this.neuSpread1_Sheet1.Columns[7].Visible = false;
            this.picLogo.Visible = false;
        }

        #region IOutPatientOrderPrint 成员

        public void PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            PrintOnePage(orderList, 1, 1, regObj, reciptDept, reciptDoct, isPreview);
        }

        public void SetPage(string pageStr)
        {
            this.lblPage.Visible = true;
            this.lblPage.Text = pageStr;
            return;
        }
        #endregion
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
            Neusoft.HISFC.Models.Order.OutPatient.Order o1 = (x as Neusoft.HISFC.Models.Order.OutPatient.Order).Clone();
            Neusoft.HISFC.Models.Order.OutPatient.Order o2 = (y as Neusoft.HISFC.Models.Order.OutPatient.Order).Clone();

            Int32 oX = o1.SortID;          //患者姓名
            Int32 oY = o2.SortID;          //患者姓名

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
                nComp = oX - oY;
            }

            return nComp;
        }

    }
    #endregion
}
