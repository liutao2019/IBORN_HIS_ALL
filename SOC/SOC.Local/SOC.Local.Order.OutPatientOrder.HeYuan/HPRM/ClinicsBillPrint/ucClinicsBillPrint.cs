using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.ClinicsBillPrint
{
    public partial class ucClinicsBillPrint : UserControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucClinicsBillPrint()
        {
            InitializeComponent();

            unDrugList.AddRange(new string[] { "UL", "UC", "UZ", "UO" });

            drugList.AddRange(new string[] { "P", "PCZ", "PCC" });
        }

        #region 业务层
        /// <summary>
        /// 门诊医嘱业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 管理业务层
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();


        /// <summary>
        /// 科室分类维护
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager deptStat = new Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager();

        #endregion

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register myReg;

        /// <summary>
        /// 药品
        /// </summary>
        List<string> drugList = new List<string>();

        /// <summary>
        /// 非药品
        /// </summary>
        List<string> unDrugList = new List<string>();

        /// <summary>
        /// 员工显示工号的位数
        /// </summary>
        private int showEmplLength = -1;
        Neusoft.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IList, bool isPreview)
        {
            //设置开具日期
            this.lblSeeYear.Text = IList[0].MOTime.Date.ToString("yyyy");
            this.lblSeeMonth.Text = IList[0].MOTime.Date.ToString("MM");
            this.lblSeeDay.Text = IList[0].MOTime.Date.ToString("dd");
            int i = 0;
            //释放farpoint原有数据...
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                //int tempRowCount = neuSpread1_Sheet1.RowCount;
                this.neuSpread1_Sheet1.RemoveRows(0, neuSpread1_Sheet1.RowCount);
                //this.neuSpread1_Sheet1.Rows.Add(0, tempRowCount);
            }
            decimal totPrice = 0;

            string feeSeq = "";
            ArrayList alSubAndOrder = null;

            #region 查询所有收费项目（附材）

            alSubAndOrder = new ArrayList();
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order OutPatientOrder in IList)
            {
                if (!feeSeq.Contains(OutPatientOrder.ReciptSequence))
                {
                    ArrayList al = outFeeMgr.QueryFeeDetailByClinicCodeAndRecipeSeq(OutPatientOrder.Patient.ID, OutPatientOrder.ReciptSequence, "ALL");
                    if (al == null)
                    {
                        //errInfo = outpatientFeeMgr.Err;
                        //return -1;
                        al = new ArrayList();
                    }

                    alSubAndOrder.AddRange(al);
                    feeSeq += "|" + OutPatientOrder.ReciptSequence + "|";
                }
            }

            #endregion

            //材料费
            decimal subFee = 0;

            Hashtable hsCombID = new Hashtable();

            alSubAndOrder.Sort(new OrderCompare());

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order OutPatientOrder in IList)
            {
                if (hsCombID.Contains(OutPatientOrder.Combo.ID))
                {
                    continue;
                }
                else
                {
                    hsCombID.Add(OutPatientOrder.Combo.ID, null);
                }

                #region 处理附材显示
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
                {
                    if (feeItem.Order.Combo.ID == OutPatientOrder.Combo.ID
                        //&& feeItem.Order.ID != OutPatientOrder.ID
                        )
                    {
                        decimal unitPrice1 = 0;
                        //非检查，非药品，非检验
                        this.neuSpread1_Sheet1.Rows.Add(i, 1);
                        this.neuSpread1_Sheet1.Cells[i, 1].Text = feeItem.Item.Name;
                        neuSpread1_Sheet1.Cells[i, 0].Text = "";//组合标记
                        decimal orgPrice1 = 0;
                        decimal price1 = feeItem.Item.Price;
                        //feeManagement.GetPrice(OutPatientOrder.Item.ID, this.myReg, 0, OutPatientOrder.Item.Price, OutPatientOrder.Item.ChildPrice, OutPatientOrder.Item.SpecialPrice, 0, ref orgPrice);
                        this.neuSpread1_Sheet1.Cells[i, 2].Text = Neusoft.FrameWork.Public.String.ToSimpleString(feeItem.Item.Qty);
                        this.neuSpread1_Sheet1.Cells[i, 3].Text = feeItem.Item.PriceUnit;
                        this.neuSpread1_Sheet1.Cells[i, 4].Text = Neusoft.FrameWork.Public.String.ToSimpleString(price1) + "元";
                        unitPrice1 = price1 * feeItem.Item.Qty;

                        //用费用表的金额，防止出现3位小数点金额和收费那里不对应
                        totPrice += feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost;

                        this.neuSpread1_Sheet1.Cells[i, 5].Text = Neusoft.FrameWork.Public.String.ToSimpleString(unitPrice1) + "元";
                        if (feeItem.Item.IsMaterial)
                        {
                            subFee += feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost;
                            neuSpread1_Sheet1.Cells[i, 0].Font = new Font(neuSpread1.Font.FontFamily, neuSpread1.Font.Size, FontStyle.Italic);
                            neuSpread1_Sheet1.Rows[i].Visible = false;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[i, 6].Text = feeItem.Order.Combo.ID;//组合号
                        }

                        //this.lblPhaDoc.Text = feeItem.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + feeItem.ReciptDoctor.Name;

                        i++;
                    }
                }

                #endregion


                this.lblSeeDept.Text = OutPatientOrder.ReciptDept.Name;

                this.lblPhaDoc.Text = OutPatientOrder.ReciptDoctor.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + OutPatientOrder.ReciptDoctor.Name;

                //i++;
            }
            //this.lblTotalPrice.Text = totPrice.ToString();
            lblTotalPrice.Text = "合计：" + Neusoft.FrameWork.Public.String.ToSimpleString(totPrice) + "元\r\n（材料费：" + Neusoft.FrameWork.Public.String.ToSimpleString(subFee) + "元）";

            lblPrintDate.Text = outFeeMgr.GetDateTimeFromSysDateTime().ToString();

            //画组合号
            Neusoft.HISFC.Components.Common.Classes.Function.DrawComboLeft(neuSpread1_Sheet1, 6, 0);
            if (i > 0 && !isPreview)
            {
                PrintPage();
            }
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;
            if (this.myReg == null)
            {
                return;
            }
            try
            {

                if (showEmplLength == -1)
                {
                    Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                    showEmplLength = controlMgr.GetControlParam<int>("HN0002", true, 6);
                }
                //年龄按照统一格式
                this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false);
                if (this.myReg.Sex.Name == "男")
                {
                    this.chkMale.Checked = true;
                    this.chkFemale.Checked = false;
                }
                else
                {
                    this.chkMale.Checked = false;
                    this.chkFemale.Checked = true;
                }
                if (this.myReg.Pact.PayKind.ID == "01")
                {
                    lbFeeType.Text = "自费";
                }
                else if (this.myReg.Pact.PayKind.ID == "02")
                {
                    lbFeeType.Text = "医保";
                }
                else
                {
                    lbFeeType.Text = "公费";
                }
                this.lblCardNo.Text = myReg.PID.CardNO;
                this.chkMale.Text = "男";
                this.chkFemale.Text = "女";

                lblName.Text = myReg.Name;

                this.npbBarCode.Image = this.CreateBarCode(myReg.PID.CardNO);

                if (myReg.AddressHome != null && myReg.AddressHome.Length > 0)
                {
                    this.lblTel.Text = myReg.AddressHome + "/" + myReg.PhoneHome;
                }
                else
                {
                    this.lblTel.Text = myReg.PhoneHome;
                }
            }
            catch
            { }
        }

        #region 私有方法
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
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int GetLength(string str)
        {
            return Encoding.Default.GetByteCount(str);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="judPrint">初打OR补打</param>
        private void PrintPage()
        {
            //Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 570, 790));
            //print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsDataAutoExtend = false;
            //if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager||judPrint=="BD")
            //{
            //    print.PrintPreview(5, 5, this);
            //}
            //else
            //{
            //    print.PrintPage(5, 5, this);
            //}
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.SetPageSize(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common.Function.getPrintPage(false));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }



        #endregion

        #region IOutPatientOrderPrint 成员

        /// <summary>
        /// 医嘱保存完的后续操作
        /// </summary>
        /// <param name="regObj">挂号实体</param>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="reciptDoct">开立医生</param>
        /// <param name="IList">医嘱列表</param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);
            return 1;
        }

        /// <summary>
        /// 门诊处方保存完的后续操作
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        public void PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion

        #region IOutPatientOrderPrint 成员


        public void SetPage(string pageStr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IOutPatientOrderPrint 成员

        void Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint.PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> List, bool isPreview)
        {
            throw new NotImplementedException();
        }

        int Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint.PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            throw new NotImplementedException();
        }

        int Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint.PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, bool isPreview)
        {
            throw new NotImplementedException();
        }

        void Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint.SetPage(string pageStr)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class OrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList fee1 = x as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList fee2 = y as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;

                return string.Compare(fee1.Order.Combo.ID + Neusoft.FrameWork.Function.NConvert.ToInt32(fee1.Item.IsMaterial).ToString() + fee1.Order.SortID.ToString(), fee2.Order.Combo.ID + Neusoft.FrameWork.Function.NConvert.ToInt32(fee2.Item.IsMaterial).ToString() + fee2.Order.SortID.ToString());
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
