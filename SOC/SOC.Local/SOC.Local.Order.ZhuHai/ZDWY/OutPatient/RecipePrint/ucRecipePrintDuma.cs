using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neusoft.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint
{
    public partial class ucRecipePrintDuma : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.HISFC.BizProcess.Interface.IRecipePrint, Neusoft.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        public ucRecipePrintDuma()
        {
            InitializeComponent();
        }

        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        Neusoft.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new Neusoft.HISFC.BizLogic.Pharmacy.DrugStore();

        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();

        Neusoft.HISFC.BizLogic.Manager.Frequency frequencyManagement = new Neusoft.HISFC.BizLogic.Manager.Frequency();

        Neusoft.HISFC.BizProcess.Integrate.RADT radtMgr = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 药品性质帮助类
        /// </summary>
        Neusoft.FrameWork.Public.ObjectHelper drugQuaulityHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 特殊处方标记
        /// </summary>
        string speRecipeLabel = "";

        /// <summary>
        /// 医嘱排序
        /// </summary>
        OrderCompare orderCompare = new OrderCompare();

        /// <summary>
        /// 门诊处方调剂实体
        /// </summary>
        Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = new Neusoft.HISFC.Models.Pharmacy.DrugRecipe();

        /// <summary>
        /// 药房编码
        /// </summary>
        string drugDept = "";

        /// <summary>
        /// 操作员
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 人员列表
        /// </summary>
        Hashtable hsEmpl = new Hashtable();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private static Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper freHelper = null;

        /// <summary>
        /// 频次帮助类
        /// </summary>
        public Neusoft.FrameWork.Public.ObjectHelper FreHelper
        {
            set
            {
                this.freHelper = value;
            }
        }

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        /// <summary>
        /// 是否打印签名信息？（否则手工签名）
        /// </summary>
        private int isPrintSignInfo = -1;

        /// <summary>
        /// 医嘱数组
        /// </summary>
        public ArrayList alOrder = new ArrayList();

        private ArrayList alTemp = new ArrayList();

        /// <summary>
        /// 存储处方组合号
        /// </summary>
        private Hashtable hsCombID = new Hashtable();

        /// <summary>
        /// 员工显示工号的位数
        /// </summary>
        private int showEmplLength = -1;

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register myReg;

        /// <summary>
        /// 用法列表，用来判断是否是针剂用法
        /// </summary>
        private ArrayList alUsage = null;

        /// <summary>
        /// 用法帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        private void Init()
        {
            if (alUsage == null || alUsage.Count == 0)
            {
                Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();
                alUsage = interMgr.GetConstantList("USAGE");
                if (alUsage == null)
                {
                    MessageBox.Show("获取用法列表出错：" + interMgr.Err);
                    return;
                }

                usageHelper.ArrayObject = alUsage;
            }

            if (printItemNameType == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }

            if (freHelper == null || freHelper.ArrayObject.Count == 0)
            {
                freHelper = new Neusoft.FrameWork.Public.ObjectHelper();
                ArrayList alFrequency = frequencyManagement.GetAll("ROOT");
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    freHelper.ArrayObject = alFrequency;
                }
                else
                {
                    MessageBox.Show("获取频次错误：" + frequencyManagement.Err);
                    return;
                }
            }
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void Query()
        {
            Init();
            if (deptHelper.ArrayObject.Count <= 0)
            {
                Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();
                deptHelper.ArrayObject = interMgr.GetDepartment();
                if (deptHelper.ArrayObject == null)
                {
                    MessageBox.Show(interMgr.Err);
                }
            }

            //从处方表获取
            ArrayList al = this.orderManager.QueryOrderByRecipeNO(this.myReg.ID, this.recipeId);

            //没有的话从费用表获取
            if (al.Count <= 0)
            {
                ArrayList alFees = new ArrayList();
                alFees = this.outPatientManager.QueryFeeDetailFromRecipeNO(this.recipeId);

                if (alFees == null || alFees.Count <= 0)
                {
                    return;
                }
                else
                {
                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFees)
                    {
                        Neusoft.HISFC.Models.Order.OutPatient.Order ot = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                        ot.Item.SysClass = itemlist.Item.SysClass;
                        ot.ReciptNO = itemlist.RecipeNO;
                        ot.DoctorDept = itemlist.RecipeOper.Dept;
                        ot.MOTime = itemlist.ChargeOper.OperTime;
                        ot.StockDept = itemlist.ExecOper.Dept;
                        ot.Patient.Patient.PID.ID = itemlist.Patient.ID;
                        ot.Frequency.ID = itemlist.FreqInfo.ID;
                        ot.Frequency.Name = itemlist.FreqInfo.Name;
                        ot.Usage = itemlist.Order.Usage;
                        ot.Item.Name = itemlist.Name;
                        ot.HerbalQty = itemlist.Days;
                        ot.ReciptDept = itemlist.RecipeOper.Dept;
                        ot.Combo.ID = itemlist.Order.Combo.ID;
                        ot.Qty = itemlist.Item.Qty;
                        ot.Unit = itemlist.Item.PriceUnit;
                        ot.DoseOnce = itemlist.Order.DoseOnce;
                        ot.DoseUnit = itemlist.Order.DoseUnit;
                        ot.Item.Specs = itemlist.Item.Specs;
                        ot.Item.ItemType = itemlist.Item.ItemType;
                        al.Add(ot);
                    }
                }
            }
            if (this.alOrder.Count > 0)
            {
                this.alOrder.Clear();
            }
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in al)
            {
                if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.alOrder.Add(order);
                }
            }
            this.MakaLabel(this.alOrder);
        }

        /// <summary>
        /// 返回组号
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetSubCombNo(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            int subCombNo = 1;
            Hashtable hsCombNo = new Hashtable();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                if (ord.SubCombNO < order.SubCombNO && !hsCombNo.Contains(ord.Combo.ID))
                {
                    hsCombNo.Add(ord.Combo.ID, ord);
                    subCombNo++;
                }
            }

            return subCombNo;
        }

        /// <summary>
        /// 生成显示信息
        /// </summary>
        /// <param name="alOrder"></param>
        private void MakaLabel(ArrayList alo)
        {
            if (alo == null) return;

            alTemp = new ArrayList();
            for (int i = 0; i < alo.Count; i++)
            {
                this.alTemp.Add(alo[i]);
            }
            hsCombID = new Hashtable();

            StringBuilder buffer = new System.Text.StringBuilder();
            //填充数据
            string[] parm = { "  ", "┌", "│", "└" };

            decimal phaMoney = 0m;//药费

            string drugType = "";

            if (showEmplLength == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                showEmplLength = controlMgr.GetControlParam<int>("HN0002", true, 6);
            }

            string feeSeq = "";
            ArrayList alSubAndOrder = null;

            alSubAndOrder = new ArrayList();
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order o in alo)
            {
                if (!feeSeq.Contains(o.ReciptSequence))
                {
                    ArrayList al = outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(o.Patient.ID, o.ReciptSequence, "ALL");
                    if (al == null) al = new ArrayList();

                    alSubAndOrder.AddRange(al);
                    feeSeq += "|" + o.ReciptSequence + "|";
                }

                this.lblSeeDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(o.ReciptDept.ID);
            }

            Hashtable hsCombNo = new Hashtable();
            decimal subFee = 0;

            //按照sortID排序
            alo.Sort(orderCompare);
            for (int i = 0; i < alo.Count; i++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order orderI = alo[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                if (orderI == null) continue;

                if (i == 0)
                {
                    drugType = orderI.Item.SysClass.ID.ToString();
                    this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(orderI.ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

                    if (hsEmpl.Contains(orderI.ReciptDoctor.ID))
                    {
                        oper = hsEmpl[orderI.ReciptDoctor.ID] as Neusoft.HISFC.Models.Base.Employee;
                    }
                    else
                    {
                        oper = this.interMgr.GetEmployeeInfo(orderI.ReciptDoctor.ID);
                    }

                    if (oper != null && oper.UserCode.Length > 0)
                    {
                        this.lblPhaDoc.Text = oper.ID.Substring(6 - showEmplLength, showEmplLength);
                    }
                    else
                    {
                        this.lblPhaDoc.Text = orderI.ReciptDoctor.ID.Substring(6 - showEmplLength, showEmplLength);
                    }
                    this.labelSeeDate.Text = orderI.MOTime.Date.ToString("yyyy.MM.dd");
                    this.myReg = this.regIntegrate.GetByClinic(this.myReg.ID);

                    drugDept = orderI.StockDept.ID;
                }

                string printUsage = "";
                ArrayList alCombOrders = this.GetOrderByCombId(orderI, ref printUsage);
                if (alCombOrders.Count == 0) continue;

                if (!hsCombNo.Contains(orderI.Combo.ID))
                {
                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
                    {
                        if (feeItem.Order.Combo.ID == orderI.Combo.ID)
                        {
                            if (feeItem.Item.IsMaterial)
                            {
                                subFee += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                            }
                            else
                            {
                                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order o in alo)
                                {
                                    if (o.Item.ID == feeItem.Item.ID
                                        && o.ReciptNO == feeItem.RecipeNO
                                        && o.SequenceNO == feeItem.SequenceNO)
                                    {
                                        phaMoney += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    hsCombNo.Add(orderI.Combo.ID, null);
                }

                if (drugType == "PCC") continue;//草药

                Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;
                Neusoft.FrameWork.Models.NeuObject objFre = null;
                string combLabel = "";

                for (int k = 0; k < alCombOrders.Count; k++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order orderK = alCombOrders[k] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    if (alCombOrders.Count == 1)
                    {
                        buffer.Append(parm[0]);
                        combLabel = parm[0];
                    }
                    else
                    {
                        if (k == 0)
                        {
                            buffer.Append(parm[1]);
                            combLabel = parm[1];
                        }
                        else if (k == alCombOrders.Count - 1)
                        {
                            buffer.Append(parm[3]);
                            combLabel = parm[3];
                        }
                        else
                        {
                            buffer.Append(parm[2]);
                            combLabel = parm[2];
                        }
                    }

                    buffer.Append("[");
                    buffer.Append(this.GetSubCombNo(orderK).ToString());
                    buffer.Append("]");

                    if (orderI.Item.ID == "999")
                    {
                        phaItem = orderK.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                    }
                    else
                    {
                        phaItem = phaIntegrate.GetItem(orderK.Item.ID);
                    }

                    string itemName = "";

                    if (phaItem != null)
                    {
                        if (printItemNameType == -1)
                        {
                            Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                            printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
                        }

                        if (printItemNameType == 0)
                        {
                            itemName = phaItem.Name;
                        }
                        else
                        {
                            //2011-6-10 houwb 通用名没有维护时，打印商品名
                            if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                itemName = phaItem.Name;
                            }
                            else
                            {
                                itemName = phaItem.NameCollection.RegularName;
                            }
                        }

                        switch (ZDWY.Function.GetPharmacyQaulity(phaItem))
                        {
                            case 2: //毒麻精一
                                speRecipeLabel = "麻、精一";
                                break;
                            default: continue;
                        }
                    }
                    else
                    {
                        itemName = orderK.Item.Name;
                    }

                    if (orderK.Item.ID == "999" && !itemName.Contains("自备"))
                    {
                        itemName += "[自备]";
                    }
                    buffer.Append(itemName);

                    Neusoft.HISFC.Models.Pharmacy.Item item = orderK.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                    if (orderK.Item.ID != "999" && !itemName.Contains("自备"))
                    {
                        if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderK.Item.ID).SpecialFlag2 != "1")
                        {
                            buffer.Append("    " + phaItem.BaseDose + phaItem.DoseUnit);
                            buffer.Append("x");
                            buffer.Append(orderK.Qty.ToString() + orderK.Unit);
                        }
                    }

                    if (combLabel == parm[0])
                    {
                        buffer.Append("\n");
                        buffer.Append("    用法:");

                        if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderK.Item.ID).SpecialFlag2 != "1")
                        {
                            buffer.Append(" ");
                            buffer.Append(orderK.DoseOnce + orderK.DoseUnit);
                            buffer.Append(" ");
                        }
                        buffer.Append(orderK.Usage.Name);
                        buffer.Append(" ");

                        if ((orderK.Frequency.Name == null || orderK.Frequency.Name.Length <= 0) &&
                            this.freHelper != null && this.freHelper.ArrayObject.Count > 0)
                        {
                            objFre = this.freHelper.GetObjectFromID(orderK.Frequency.ID);
                            if (objFre != null)
                            {
                                buffer.Append(objFre.Name);
                            }
                        }
                        else
                        {
                            buffer.Append(orderK.Frequency.Name);
                        }

                        buffer.Append(" ");
                        buffer.Append(orderK.Memo);

                    }
                    else if (combLabel == parm[3])
                    {
                        buffer.Append("\n");

                        for (int m = 0; m < alCombOrders.Count; m++)
                        {
                            Neusoft.HISFC.Models.Order.OutPatient.Order orderM = alCombOrders[m] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                            if (m == 0)
                            {
                                buffer.Append("    用法:");

                                if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderK.Item.ID).SpecialFlag2 != "1")
                                {
                                    buffer.Append(" ");

                                    bool flag = false;
                                    bool changeDoseOnce = false;
                                    for (int j = 0; j < alCombOrders.Count; j++)
                                    {
                                        Neusoft.HISFC.Models.Order.OutPatient.Order orderJ = alCombOrders[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                                        item = orderJ.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                                        if (orderJ.DoseOnce != item.BaseDose)
                                        {
                                            changeDoseOnce = true;
                                        }
                                    }
                                    for (int j = 0; j < alCombOrders.Count; j++)
                                    {
                                        Neusoft.HISFC.Models.Order.OutPatient.Order orderJ = alCombOrders[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                                        item = orderJ.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                                        if (changeDoseOnce)
                                        {
                                            flag = true;
                                            //如果每次量不等于基础量，则在用法后显示每次量
                                            if (j > 0)
                                            {
                                                buffer.Append("          ");
                                            }
                                            buffer.Append(item.Name);
                                            buffer.Append(orderJ.DoseOnce + orderJ.DoseUnit);
                                            buffer.Append("\n");
                                        }

                                        if (j == alCombOrders.Count - 1 && flag == true)
                                        {
                                            buffer.Remove(buffer.Length - 1, 1);
                                            buffer.Append(" ");
                                        }
                                    }
                                }

                                buffer.Append(orderM.Usage.Name);
                                buffer.Append(" ");

                                if ((orderM.Frequency.Name == null
                                    || orderM.Frequency.Name.Length <= 0)
                                    && this.freHelper != null
                                    && this.freHelper.ArrayObject.Count > 0)
                                {
                                    objFre = this.freHelper.GetObjectFromID(orderM.Frequency.ID);
                                    if (objFre != null)
                                    {
                                        buffer.Append(objFre.Name);
                                    }
                                }
                                else
                                {
                                    buffer.Append(orderM.Frequency.Name);
                                }

                                buffer.Append(" ");
                                buffer.Append(orderM.Memo);
                                buffer.Append("\n");
                            }
                        }
                    }

                    if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(orderK.Usage.ID))
                    {
                        buffer.Append("(余液弃去)");
                    }
                    buffer.Append("\n");
                }
            }

            buffer.Append("\n 以下空白");

            this.lblOrder.Text = buffer.ToString();
            this.lblPhaMoney.Text = Neusoft.FrameWork.Public.String.ToSimpleString(phaMoney) + "元";

            this.drugRecipe = drugManager.GetDrugRecipe(this.drugDept, this.recipeId);
            if (this.drugDept == null)
            {
                MessageBox.Show("查询处方发药信息错误：" + drugManager.Err);
                return;
            }

            this.SetPatientInfo(this.myReg);

            this.lblDrugDoct.Text = "_____________";
            this.labelAuthenDoc.Text = "____________";
            this.lblSendDoct.Text = "_____________";
            this.labelReceiver.Text = "_________________";
            this.labelSentDrugBatchNo.Text = "______________________";

            if (this.isPrintSignInfo == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                isPrintSignInfo = controlMgr.GetControlParam<int>("HNMZ12", true, 0);
            }
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Obsolete("Use Soc.Public.Function.CreateBarCode instead.", true)]
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
        /// 获取字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private string SetToByteLength(string str, int padLength)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 1;
                }
            }

            if (padLength - str.Length - len > 0)
            {
                return str + "".PadRight(padLength - str.Length - len, ' ');
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <param name="usageID">一组统一显示的用法</param>
        /// <returns></returns>
        private ArrayList GetOrderByCombId(Neusoft.HISFC.Models.Order.OutPatient.Order order, ref string usage)
        {
            ArrayList al = new ArrayList();

            Neusoft.HISFC.Models.Order.OutPatient.Order ord = null;
            if (!hsCombID.Contains(order.Combo.ID))
            {
                for (int i = 0; i < this.alTemp.Count; i++)
                {
                    ord = alTemp[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if (order.Combo.ID == ord.Combo.ID)
                    {
                        al.Add(ord);
                    }
                }
                hsCombID.Add(order.Combo.ID, order);
            }

            return al;
        }

        /// <summary>
        /// print the recipe 白纸打印
        /// </summary>
        public void Print()
        {
            this.SetOtherInfo();

            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();

            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;

            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 790));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintDocument.DefaultPageSettings.Landscape = false;

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPage(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        private void SetOtherInfo()
        {
            this.chkPub.Text = "公费";
            this.chkPay.Text = "医保";
            this.chkOwn.Text = "自费";
            this.chkOth.Text = "其他";

            if (this.speRecipeLabel.Length <= 0)
            {
                if (this.alOrder == null || this.alOrder.Count <= 0)
                {
                    return;
                }
                //判断是否急诊？ 妇幼要求在急诊时间段都是打印急诊处方
                if (this.regIntegrate.IsEmergency((this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID, (this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).MOTime))
                {
                    this.speRecipeLabel = "急 诊";
                }
                //儿科和急诊科显示体重信息
                //根据第一条医嘱的开立科室判断 houwb
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("急") >= 0)
                {
                    this.speRecipeLabel = "急 诊";
                }
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("儿") >= 0)
                {
                    this.speRecipeLabel = "儿 科";
                }
            }

            this.lblJS.Visible = this.speRecipeLabel.Length > 0;

            if (this.speRecipeLabel.Length > 0)
            {
                if (this.lbIdenNO.Visible)
                {
                    this.lbIdenNO.Text = this.myReg.IDCard;
                }
            }

            this.lblJS.Text = this.speRecipeLabel;
        }
        /// <summary>
        /// 转换成英文
        /// </summary>
        private void ChangeToEnglish()
        {
            label14.Text = "FeeType:";
            label5.Text = "RecipeNum:";
            labelAuthenDoc.Text = "MedicalNum:";
            label6.Text = "Name:";
            label7.Text = "Sex:";
            label8.Text = "Age:";
            label10.Text = "Dept:";
            label12.Text = "CardNum:";
            label19.Text = "Diagnose:";
            label20.Text = "Phone/Address:";
            label22.Text = "Doctor:";
            label3.Text = "Cost:";
            label24.Text = "Audit Apothecary:";
            label25.Text = "Prepare Apothecary:";
            label27.Text = "Check Apothecary:";

            this.chkOwn.Text = "OwnCost";
            this.chkPub.Text = "PubCost";
            this.chkPay.Text = "PayCost";
            this.chkOth.Text = "Ohter";

            lblJS.Text = "";
        }

        /// <summary>
        /// 从消耗品和医嘱数组中移除医嘱
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="alOrderAndSub"></param>
        private void RemoveOrderFromArray(ArrayList alOrder, ref ArrayList alOrderAndSub)
        {
            if (alOrder == null || alOrder.Count == 0)
            {
                return;
            }
            if (alOrderAndSub == null || alOrderAndSub.Count == 0)
            {
                return;
            }
            ArrayList alTemp = new ArrayList();
            for (int i = 0; i < alOrderAndSub.Count; i++)
            {
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                for (int j = 0; j < alOrder.Count; j++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if ((temp.ReciptNO == item.RecipeNO) && (temp.SequenceNO == item.SequenceNO))
                    {
                        item.Item.MinFee.User03 = "1";
                    }
                }
            }
            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
            {
                if (item.Item.MinFee.User03 != "1")
                {
                    alTemp.Add(item);
                }
            }
            alOrderAndSub = alTemp;
        }

        /// <summary>
        /// 频次信息
        /// </summary>
        Neusoft.HISFC.Models.Order.Frequency frequencyObj = null;

        /// <summary>
        /// 根据频次获得每天剂数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            if (string.IsNullOrEmpty(frequencyID))
            {
                return -1;
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
                if (freHelper != null)
                {
                    frequencyObj = freHelper.GetObjectFromID(frequencyID) as Neusoft.HISFC.Models.Order.Frequency;
                }

                if (frequencyObj == null)
                {
                    ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID.ToLower());

                    if (alFrequency != null && alFrequency.Count > 0)
                    {
                        Neusoft.HISFC.Models.Order.Frequency obj = alFrequency[0] as Neusoft.HISFC.Models.Order.Frequency;
                        string[] str = obj.Time.Split('-');
                        return str.Length;
                    }
                }

                return -1;
            }
        }

        /// <summary>
        /// 预览打印
        /// </summary>
        public void Preview()
        {
            this.SetOtherInfo();

            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();

            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;

            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 790));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintDocument.DefaultPageSettings.Landscape = false;
            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        /// <summary>
        /// 当前处方号
        /// </summary>
        private string recipeId = "";

        public int PrintRecipe()
        {
            this.Print();
            return 1;
        }

        /// <summary>
        /// 当前处方号
        /// </summary>
        public string RecipeNO
        {
            get
            {
                return this.recipeId;
            }
            set
            {
                this.recipeId = value;
                this.speRecipeLabel = "";
                this.Query();
            }
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public int SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;
            if (this.myReg == null) return 1;

            if (drugQuaulityHelper.ArrayObject.Count <= 0)
            {
                //取药品剂型
                ArrayList alDrugQuaulity = interMgr.GetConstantList("DRUGQUALITY");

                if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                {
                    drugQuaulityHelper.ArrayObject = alDrugQuaulity;
                }
            }

            this.lblName.Text = this.myReg.Name;

            if (this.myReg.Pact.PayKind.ID == "03")
            {
                Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
                Neusoft.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
            }

            Neusoft.HISFC.Models.RADT.PatientInfo tmpInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
            if (!string.IsNullOrEmpty(this.myReg.PID.CardNO))
            {
                tmpInfo = this.radtMgr.QueryComPatientInfo(this.myReg.PID.CardNO);
            }
            if (tmpInfo != null)
            {
                this.myReg.IDCard = tmpInfo.IDCard;
                this.myReg.PhoneHome = tmpInfo.PhoneHome;

                //代办人
                labelAgentName.Text = string.Empty;
                labelAgentIdenNO.Text = string.Empty;
                if (!string.IsNullOrEmpty(tmpInfo.Kin.Name))
                {
                    labelAgentName.Text = tmpInfo.Kin.Name;//代办人
                }
                if (!string.IsNullOrEmpty(tmpInfo.Kin.Memo))
                {
                    labelAgentIdenNO.Text = tmpInfo.Kin.Memo;//代办人身份证
                }
            }

            this.labelAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false);
            this.labelGender.Text = this.myReg.Sex.Name;
            this.lblCardNo.Text = myReg.PID.CardNO;
            this.npbBarCode.Image = SOC.Public.Function.CreateBarCode(myReg.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);

            this.chkOwn.Checked = false;
            this.chkPay.Checked = false;
            this.chkPub.Checked = false;
            this.chkOth.Checked = false;

            if (this.myReg.Pact.PayKind.ID == "01")
            {
                this.chkOwn.Checked = true;
            }
            else if (this.myReg.Pact.PayKind.ID == "02")
            {
                this.chkPay.Checked = true;
            }
            else
            {
                this.chkPub.Checked = true;
            }

            if (myReg.AddressHome != null && myReg.AddressHome.Length > 0)
            {
                this.lblTel.Text = myReg.AddressHome + "/" + myReg.PhoneHome;
            }
            else
            {
                this.lblTel.Text = myReg.PhoneHome;
            }

            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string strDiag = "";
            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "、";
                }
            }

            lblDiag.Text = strDiag.TrimEnd('、');

            this.SetOtherInfo();

            return 1;
        }

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        public void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
        }

        public void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
        }

        public void AddAllData(ArrayList al)
        {
        }

        public void AddCombo(ArrayList alCombo)
        {
        }

        public void AddSingle(Neusoft.HISFC.Models.Pharmacy.ApplyOut info)
        {
        }

        public decimal DrugTotNum { set { } }

        public Neusoft.HISFC.Models.RADT.PatientInfo InpatientInfo { get; set; }

        public decimal LabelTotNum { set { } }

        public Neusoft.HISFC.Models.Registration.Register OutpatientInfo { get; set; }

        public int PrintRecipeView(System.Collections.ArrayList alRecipe)
        {
            this.Preview();
            return 1;
        }
    }
}
