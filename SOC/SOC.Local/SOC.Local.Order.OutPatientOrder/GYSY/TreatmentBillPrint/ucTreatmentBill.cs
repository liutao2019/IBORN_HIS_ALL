using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;
using Neusoft.SOC.Local.Order.OutPatientOrder.Common;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.TreatmentBillPrint
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式 xiaohf 2012年6月28日19:52:42
    /// </summary>
    public partial class ucTreatmentBill : UserControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        #region 变量
        /// <summary>
        /// 每页显示的条数
        /// </summary>
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

        /// <summary>
        /// 诊查费
        /// </summary>
        private decimal zhenChaFee = 0m;

        /// <summary>
        /// 医院名称
        /// </summary>
        string strHosName = string.Empty;

        /// <summary>
        ///常数维护业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Constant constMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public ucTreatmentBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印所有院注射单
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        private void PrintAllPage(ArrayList alData, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct,bool isPreview)
        {
            try
            {
                //获取医院名称
                strHosName = this.constMgr.GetHospitalName();
                ArrayList alPrint = new ArrayList();
                int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));
                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, isPreview);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, isPreview);
                    }
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
            GetHospLogo();
            this.neuLabel2.Text = strHosName + "院注单";
            this.lblZhenChaFee.Visible = false;
            if (zhenChaFee > 0)
            {
                this.lblZhenChaFee.Visible = true;
                this.lblZhenChaFee.Text = string.Format("该患者已支付急诊监护费({0}元)", zhenChaFee);
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

                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
                FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);

                //赋值并打印
                for (int i = 0; i < alData.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)alData[i];

                    Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
                    //Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = outpatientFeeMgr.GetFeeItemList(info.RecipeNO, info.SequenceNO);
                    //Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order(); //Common.Function.GetOrder(info.OrderNO);
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = bottomBorder;


                    //修改组号
                    if (order.Combo.ID != rememberComboNO)
                    {
                        rememberComboNO = order.Combo.ID;
                        spanRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                        showGroupNO++;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 3].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 6].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 7].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 10].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 11].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    }

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = showGroupNO.ToString();


                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;//用量
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//用法
                    }
                    catch { }
                    string hypoTest = "";

                    //妇幼 廖科 要求屏蔽 所以不再显示皮试信息
                    if (order != null)
                    {
                        hypoTest = this.outOrderMgr.TransHypotest(order.HypoTest);
                        //if (order.HypoTest == 1)
                        //{
                        //    hypoTest = "免皮试";
                        //}
                        //else if (order.HypoTest == 2)
                        //{
                        //    hypoTest = "需要皮试";
                        //}
                        //else if (order.HypoTest == 3)
                        //{
                        //    hypoTest = "阳性";
                        //}
                        //else if (order.HypoTest == 4)
                        //{
                        //    hypoTest = "阴性";
                        //}
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = hypoTest; //皮试
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.Item.Specs;//规格
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = order.Frequency.ID;//次数
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = order.HerbalQty.ToString();// 天数

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Memo;//滴速
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = order.Qty.ToString() + order.DoseUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = " ";//开始时间
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 11].Text = " ";//执行人

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }

                //this.lbInvoiceNo.Text = "发票号：" + drugRecipe.InvoiceNO;
                this.lbCard.Text = regObj.Card.ID;
                this.lbName.Text = regObj.Name;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //打印时间
                Neusoft.FrameWork.Management.DataBaseManger dataBaseManger = new Neusoft.FrameWork.Management.DataBaseManger();

                this.lbAge.Text = dataBaseManger.GetAge(regObj.Birthday);
                this.lbSex.Text = regObj.Sex.Name;
                this.lbPage.Text = "第" + current.ToString() + "页" + "/" + "共" + total.ToString() + "页";

                ////增加fp下面的内容
                //this.neuDoctName.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.Name);//医生姓名 
                //this.neuChargeOper.Text = "收费员：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.FeeOper.ID); ;//收费员

                PrintPage(isPreview);
            }
            catch { }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage(bool isPreview)
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 575, 800));
            print.PrintDocument.DefaultPageSettings.Landscape = true;
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            //管理员或者是补打就调用预览打印
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager || isPreview)
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
            Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();
            Neusoft.HISFC.BizLogic.Manager.Constant constantMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();
            usageHelper.ArrayObject = constantMgr.GetAllList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);
            //Neusoft.HISFC.BizLogic.Fee.Outpatient feeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

            ArrayList alIM = new ArrayList();    //IM肌注，单独打一张注射单，其他用法打另外一张注射单
            ArrayList alOther = new ArrayList(); //除肌注以外的药品
            ArrayList alSpecial = new ArrayList();//已经维护的需要特别打单的药品


            //ArrayList alFeeItemList = feeMgr.QueryFeeItemListsByInvoiceNO(drugRecipe.InvoiceNO);

            ArrayList alSpecialInjectBill = constMgr.GetAllList("TreatmentItem");

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                //治疗用法打印
                if (SOC.HISFC.BizProcess.Cache.Common.IsCure(order.Usage.ID))
                {
                     alSpecial.Add(order);
                }
                //常数维护的项目打印
                    if (alSpecialInjectBill != null && alSpecialInjectBill.Count > 0)
                    {
                        foreach (Neusoft.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
                        {
                            if (cnst.ID == order.Item.ID)
                            {
                                alSpecial.Add(order);
                                //isSpecial = true;
                                break;
                            }
                        }
                    }
                   
                //}

            }
            if (alSpecial.Count > 0)
            {
                alSpecial.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(alSpecial, regObj, reciptDept, reciptDoct,isPreview);
            }
          
            return 1;
        }


        public void PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
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
            Common.ComFunc cf = new ComFunc();
            string erro = "出错";
            string imgpath = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
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
